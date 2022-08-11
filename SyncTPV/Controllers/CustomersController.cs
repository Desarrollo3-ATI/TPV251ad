using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Controllers.Downloads;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Dynamic;
using System.Threading.Tasks;
using wsROMClases.Models.Commercial;
using wsROMClases.Models.Panel;

namespace SyncTPV.Controllers
{
    public class CustomersController
    {
        public int lastId = 0;

        public static async Task<ExpandoObject> downloadAllCustomersAPI(int downloadType, int lastId)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                try
                {
                    int itemsToEvaluate = 0;
                    do
                    {
                        String url = ConfiguracionModel.getLinkWs();// + "/DTCLIENTES";
                        url = url.Replace(" ", "%20");
                        var client = new RestClient(url);
                        var request = new RestRequest("/getAllClientes", Method.Post);
                        request.AddJsonBody(new
                        {
                            ruta = "[TPV]" + UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn()),
                            lastId = lastId,
                            limit = 1000
                        });
                        var responseHeader = client.ExecuteAsync(request);
                        if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                        {
                            var content = responseHeader.Result.Content;
                            ResponseGetAllClientes responseClientes = JsonConvert.DeserializeObject<ResponseGetAllClientes>(content);
                            if (responseClientes.value == 1)
                            {
                                List<ClsClienteModel> clientesList = responseClientes.clientesList;
                                if (clientesList != null && clientesList.Count > 0)
                                    itemsToEvaluate = clientesList.Count;
                                if (lastId == 0 && downloadType == ClsInitialChargeController.INITIAL_CHARGE)
                                    CustomerModel.deleteAllCustomersInLocalDb();
                                if (downloadType == ClsInitialChargeController.INITIAL_CHARGE)
                                    lastId = CustomerModel.saveCustomers(clientesList);
                                else
                                {
                                    if (lastId == 0)
                                        CustomerModel.deleteAllCustomersInLocalDb();
                                    lastId = CustomerModel.updateCustomers(clientesList);
                                }
                                value = 1;
                            } else
                            {
                                value = responseClientes.value;
                                description = responseClientes.description;
                            }
                        }
                        else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                        {
                            value = -400;
                            description = MetodosGenerales.getErrorMessageFromNetworkCode(value,
                                responseHeader.Result.ErrorException.Message);
                        } else if (responseHeader.Result.ResponseStatus == ResponseStatus.TimedOut)
                        {
                            value = -404;
                            description = MetodosGenerales.getErrorMessageFromNetworkCode(value,
                                responseHeader.Result.ErrorException.Message);
                        } else
                        {
                            value = -500;
                            description = MetodosGenerales.getErrorMessageFromNetworkCode(value,
                                responseHeader.Result.ErrorException.Message);
                        }
                    } while (itemsToEvaluate > 999 && value == 1);
                }
                catch (Exception ex)
                {
                    SECUDOC.writeLog(ex.ToString());
                    value = -1;
                    description = ex.Message;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                }
            });
            return response;
        }

        private class ResponseGetAllClientes
        {
            public int value { get; set; }

            public String description { get; set; }
            public List<ClsClienteModel> clientesList { get; set; }
        }

        public static async Task<ExpandoObject> getTotalOfCustomersAPI(int lastId, int limit, int withParameters, String parameterName, String parameterValue)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                int total = 0;
                try
                {
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/getTotaOfCustomersTPV", Method.Post);
                    request.AddJsonBody(new
                    {
                        routeCode = "[TPV]" + UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn()),
                        lastId = lastId,
                        limit = limit,
                        withParameters = withParameters,
                        parameterName = parameterName,
                        parameterValue = parameterValue
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        var jsonResp = JsonConvert.DeserializeObject<int>(content);
                        total = (int)jsonResp;
                        value = 1;
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        if (responseHeader.Result.ErrorMessage.Equals("Unable to connect to the remote server"))
                        {
                            value = -404;
                            description = "No pudimos establecer conexión con el servidor";
                        }
                        else
                        {
                            value = -500;
                            description = "Algo falló al negociar información con el servidor";
                        }
                    }
                }
                catch (Exception ex)
                {
                    SECUDOC.writeLog(ex.ToString());
                    value = -1;
                    description = ex.Message;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                    response.total = total;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> getAllCustomersAPI(int lastId, int limit, String parameterName, String parameterValue)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                List<ClsClienteModel> customersList = null;
                try
                {
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/getAllCustomersWithParametersTPV", Method.Post);
                    request.AddJsonBody(new
                    {
                        routeCode = "[TPV]" + UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn()),
                        lastId = lastId,
                        limit = limit,
                        parameterName = parameterName,
                        parameterValue = parameterValue
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        var jsonResp = JsonConvert.DeserializeObject<List<ClsClienteModel>>(content);
                        customersList = (List<ClsClienteModel>)jsonResp;
                        if (customersList != null)
                        {
                            saveOrUpdateCustomers(customersList);
                            value = 1;
                        } else
                        {
                            deleteAllCustomersInLocalDbWithParameters(parameterValue);
                            value = 1;
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        if (responseHeader.Result.ErrorMessage.Equals("Unable to connect to the remote server"))
                        {
                            value = -404;
                            description = "No pudimos establecer conexión con el servidor";
                        }
                        else
                        {
                            value = -500;
                            description = "Algo falló al negociar información con el servidor";
                        }
                    }
                }
                catch (Exception ex)
                {
                    SECUDOC.writeLog(ex.ToString());
                    value = -1;
                    description = ex.Message;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                    response.customersList = customersList;
                }
            });
            return response;
        }

        private static async Task deleteAllCustomersInLocalDbWithParameters(String parameterValue)
        {
            await Task.Run(async () =>
            {
                CustomerModel.deleteAllCustomersInLocalDbWithParameters(parameterValue);
            });
        }

        private static async Task saveOrUpdateCustomers(List<ClsClienteModel> customersList)
        {
            await Task.Run(async () =>
            {
                CustomerModel.updateCustomers(customersList);
            });
        }
        private class ResponseGetAllCustomers
        {
            public List<ClsClienteModel> customersList { get; set; }
        }

        public static async Task<ExpandoObject> getACustomerAPI(int lastIdCustomerPanel)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                ClsClienteModel customerModel = null;
                try
                {
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    RestRequest request = new RestRequest("/getCustomer", Method.Post);
                    request.AddJsonBody(new
                    {
                        idCustomer = lastIdCustomerPanel,
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        var jsonResp = JsonConvert.DeserializeObject<ClsClienteModel>(content);
                        customerModel = (ClsClienteModel)jsonResp;
                        if (customerModel != null) {
                            value = 1; 
                            saveCustomerDownloaded(customerModel, lastIdCustomerPanel);
                            description = "Cliente Guardado Correctamente";
                        } else
                        {
                            value = -2;
                            if (lastIdCustomerPanel >= 0)
                                deleteACustomerNotDownloaded(lastIdCustomerPanel);
                            description = "Cliente No Encontrado en el sistema Comercial Premium";
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        if (responseHeader.Result.ErrorMessage.Equals("Unable to connect to the remote server"))
                        {
                            value = -404;
                            description = "No pudimos encontrar el servidor";
                        }
                        else
                        {
                            value = -500;
                            description = "Error al negociar con el servidor";
                        }
                    }
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    value = -1;
                    description = e.Message;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                    response.customerModel = customerModel;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> getANewCustomerAPI(int idCliente)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                ClsClienteModel customerModel = null;
                try
                {
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    RestRequest request = new RestRequest("/getNewCustomer", Method.Post);
                    request.AddJsonBody(new
                    {
                        idCliente = Math.Abs(idCliente),
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        var jsonResp = JsonConvert.DeserializeObject<ResponseNewCustomer>(content);
                        ResponseNewCustomer responseCustomer = (ResponseNewCustomer)jsonResp;
                        if (responseCustomer.value == 1)
                        {
                            value = responseCustomer.value;
                            customerModel = responseCustomer.customerModel;
                            description = "";
                            saveCustomerDownloaded(customerModel, idCliente);
                        }
                        else
                        {
                            description = responseCustomer.description;                            
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        value = -400;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, responseHeader.Result.ErrorException.Message.ToString());
                    } else if (responseHeader.Result.ResponseStatus == ResponseStatus.TimedOut)
                    {
                        value = -404;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, responseHeader.Result.ErrorException.Message.ToString());
                    } else
                    {
                        value = -500;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, responseHeader.Result.ErrorException.Message.ToString());
                    }
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    value = -1;
                    description = e.Message;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                    response.customerModel = customerModel;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> getANewCustomerLAN(int idCliente)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                ClsClienteModel customerModel = null;
                try
                {
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    String comInstance = InstanceSQLSEModel.getStringComInstance();
                    String codigoComercial = ClsClienteADCModel.getCodeById(panelInstance, Math.Abs(idCliente));
                    if (codigoComercial.Equals(""))
                    {
                        description = "El código para el cliente con Id: " + Math.Abs(idCliente) + " del SyncPanel no fue encontrado";
                    }
                    else
                    {
                        customerModel = ClsCustomersModel.getAllDataFromACustomerByCode(comInstance, codigoComercial);
                        if (customerModel != null)
                        {
                            value = 1;
                            saveCustomerDownloaded(customerModel, idCliente);
                        }
                        else description = "Cliente con " + codigoComercial + " no fue encontrado en el sistema de Comercial Premium." +
                            "\r\nEsperar a que el cliente se suba a comercial de lo contrario solicitar soporte!";
                    }
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    value = -1;
                    description = e.Message;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                    response.customerModel = customerModel;
                }
            });
            return response;
        }

        private class ResponseNewCustomer
        {
            public int value { get; set; }
            public String description { get; set; }
            public ClsClienteModel customerModel { get; set; }
        }

        public static async Task<ExpandoObject> getACustomer(SQLiteConnection db, int lastIdCustomerPanel)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int idInserted = 0;
                String errorMessage = "";
                try
                {
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    // client.Authenticator = new HttpBasicAuthenticator(username, password);
                    RestRequest request = new RestRequest("/getCustomer", Method.Post);
                    request.AddJsonBody(new
                    {
                        idCustomer = lastIdCustomerPanel,
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content; // Raw content as string
                        var jsonResp = JsonConvert.DeserializeObject<ClsClienteModel>(content);
                        ClsClienteModel customer = (ClsClienteModel)jsonResp;
                        if (customer != null)
                        {
                            if (lastIdCustomerPanel < 0)
                            {
                                if (CustomerADCModel.isTheCustomerSendedByIdPanel(Math.Abs(lastIdCustomerPanel)))
                                {
                                    String query = "SELECT " + LocalDatabase.CAMPO_ID_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA +
                                    " WHERE " + LocalDatabase.CAMPO_CLIENTEID_DOC + " = " + lastIdCustomerPanel;
                                    List<int> idsDocuments = DocumentModel.getListIntValue(query);
                                    if (idsDocuments != null)
                                    {
                                        foreach (int idDocument in idsDocuments)
                                        {
                                            query = "UPDATE " + LocalDatabase.TABLA_DOCUMENTOVENTA + " SET " + LocalDatabase.CAMPO_CLIENTEID_DOC +
                                            " = " + customer.CLIENTE_ID + " WHERE " + LocalDatabase.CAMPO_ID_DOC + " = " + idDocument;
                                            DocumentModel.updateARecord(query);
                                        }
                                    }
                                    int lastNewIdClienteLocal = CustomerADCModel.getNewClientIdLocal(db, lastIdCustomerPanel);
                                    CustomerADCModel.deleteClienteNuevoSinDocumentosByPanelId(db, lastIdCustomerPanel);
                                    CustomerModel.deleteACustomer(db, Convert.ToInt32("-" + lastNewIdClienteLocal));
                                    idInserted = CustomerModel.createCustomer(db, customer);
                                }
                                else
                                {

                                }
                            }
                            else
                            {
                                /*String query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " +
                                LocalDatabase.CAMPO_ID_CLIENTE + " = " + idCustomer;
                                int recordsClientes = CustomerModel.getIntValueFromACustomer(query);
                                if (recordsClientes > 1)
                                {
                                    CustomerModel.deleteACustomer(idCustomer);
                                    correctResponse = CustomerModel.createCustomer(customer);
                                }
                                else
                                {
                                    correctResponse = CustomerModel.updateCustomer(db, customer);
                                }*/
                                if (CustomerModel.checkIfTheClientExists(db, customer.CLIENTE_ID))
                                {
                                    idInserted = CustomerModel.updateCustomer(db, customer);
                                }
                                else
                                {
                                    idInserted = CustomerModel.createCustomer(db, customer);
                                }
                            }
                            errorMessage = "Cliente Guardado Correctamente";
                        }
                        else
                        {
                            idInserted = -2;
                            errorMessage = "Cliente No Encontrado en el sistema Comercial Premium";
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        if (responseHeader.Result.ErrorMessage.Equals("Unable to connect to the remote server"))
                        {
                            idInserted = -404;
                            errorMessage = "No pudimos encontrar el servidor";
                        }
                        else
                        {
                            idInserted = -500;
                            errorMessage = "Error al negociar con el servidor";
                        }
                    }
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    idInserted = -1;
                    errorMessage = e.Message;
                }
                finally
                {
                    response.valor = idInserted;
                    response.description = errorMessage;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> getACustomerLAN(SQLiteConnection db, int lastIdCustomerPanel)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int idInserted = 0;
                String errorMessage = "";
                try
                {
                    String comInstance = InstanceSQLSEModel.getStringComInstance();
                    ClsClienteModel cm = null;
                    if (lastIdCustomerPanel < 0)
                    {
                        String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                        String customerCode = ClsClienteModel.getCodeCliente(panelInstance, Math.Abs(lastIdCustomerPanel));
                        cm = ClsCustomersModel.getCustomerByCode(comInstance, customerCode);
                    } else
                    {
                        cm = ClsCustomersModel.getCustomer(comInstance, lastIdCustomerPanel);
                    }
                    if (cm != null)
                    {
                        if (CustomerModel.checkIfTheClientExists(cm.CLIENTE_ID))
                        {
                            /*ClsClienteModel clientes = new ClsClienteModel();
                            clientes.CLIENTE_ID = cm.CLIENTE_ID;
                            clientes.CLAVE = cm.CLAVE;
                            clientes.NOMBRE = cm.NOMBRE;
                            clientes.LIMITE_CREDITO = cm.LIMITE_CREDITO.ToString().Trim();
                            clientes.CONDICIONPAGO = cm.CONDICIONPAGO;
                            clientes.LISTAPRECIO = cm.LISTAPRECIO;
                            clientes.DIRECCION = cm.DIRECCION;
                            clientes.TELEFONO = cm.TELEFONO;
                            clientes.PRECIO_EMPRESA_ID = cm.PRECIO_EMPRESA_ID.ToString().Trim();
                            clientes.ZONA_CLIENTE_ID = cm.ZONA_CLIENTE_ID.ToString().Trim();
                            clientes.REFERENCIA = cm.REFERENCIA;
                            clientes.AVAL = cm.AVAL;
                            clientes.CLAVEQR = cm.CLAVEQR;
                            clientes.rfc = cm.rfc;
                            clientes.curp = cm.curp;
                            clientes.denominacionComercial = cm.denominacionComercial;*/
                            if (lastIdCustomerPanel < 0)
                            {
                                if (CustomerADCModel.isTheCustomerSendedByIdPanel(Math.Abs(lastIdCustomerPanel)))
                                {
                                    String query = "SELECT " + LocalDatabase.CAMPO_ID_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA +
                                    " WHERE " + LocalDatabase.CAMPO_CLIENTEID_DOC + " = " + lastIdCustomerPanel;
                                    List<int> idsDocuments = DocumentModel.getListIntValue(query);
                                    if (idsDocuments != null)
                                    {
                                        foreach (int idDocument in idsDocuments)
                                        {
                                            query = "UPDATE " + LocalDatabase.TABLA_DOCUMENTOVENTA + " SET " + LocalDatabase.CAMPO_CLIENTEID_DOC +
                                            " = " + cm.CLIENTE_ID + " WHERE " + LocalDatabase.CAMPO_ID_DOC + " = " + idDocument;
                                            DocumentModel.updateARecord(query);
                                        }
                                    }
                                    int lastNewIdClienteLocal = CustomerADCModel.getNewClientIdLocal(db, lastIdCustomerPanel);
                                    CustomerADCModel.deleteClienteNuevoSinDocumentosByPanelId(db, lastIdCustomerPanel);
                                    CustomerModel.deleteACustomer(db, Convert.ToInt32("-" + lastNewIdClienteLocal));
                                    idInserted = CustomerModel.createCustomer(db, cm);
                                }
                                else
                                {

                                }
                            } else
                            {
                                /*String query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " +
                                    LocalDatabase.CAMPO_ID_CLIENTE + " = " + lastIdCustomer;
                                int recordsClientes = CustomerModel.getIntValueFromACustomer(query);
                                if (recordsClientes > 1)
                                {
                                    CustomerModel.deleteACustomer(lastIdCustomer);
                                    idInserted = CustomerModel.createCustomer(clientes);
                                }
                                else
                                {
                                    idInserted = CustomerModel.updateCustomer(db, clientes);
                                }*/
                                if (CustomerModel.checkIfTheClientExists(db, cm.CLIENTE_ID))
                                {
                                    idInserted = CustomerModel.updateCustomer(db, cm);
                                }
                                else
                                {
                                    idInserted = CustomerModel.createCustomer(db, cm);
                                }
                            }
                        }
                        else
                        {
                            /*clsClientes clientes = new clsClientes();
                            clientes.CLIENTE_ID = cm.CLIENTE_ID;
                            clientes.CLAVE = cm.CLAVE;
                            clientes.NOMBRE = cm.NOMBRE;
                            clientes.LIMITE_CREDITO = cm.LIMITE_CREDITO.ToString().Trim();
                            clientes.CONDICIONPAGO = cm.CONDICIONPAGO;
                            clientes.LISTAPRECIO = cm.LISTAPRECIO;
                            clientes.DIRECCION = cm.DIRECCION;
                            clientes.TELEFONO = cm.TELEFONO;
                            clientes.PRECIO_EMPRESA_ID = cm.PRECIO_EMPRESA_ID.ToString().Trim();
                            clientes.ZONA_CLIENTE_ID = cm.ZONA_CLIENTE_ID.ToString().Trim();
                            clientes.REFERENCIA = cm.REFERENCIA;
                            clientes.AVAL = cm.AVAL;
                            clientes.CLAVEQR = cm.CLAVEQR;
                            clientes.rfc = cm.rfc;
                            clientes.curp = cm.curp;
                            clientes.denominacionComercial = cm.denominacionComercial;*/
                            if (lastIdCustomerPanel < 0)
                            {
                                if (CustomerADCModel.isTheCustomerSendedByIdPanel(Math.Abs(lastIdCustomerPanel)))
                                {
                                    String query = "SELECT " + LocalDatabase.CAMPO_ID_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA +
                                    " WHERE " + LocalDatabase.CAMPO_CLIENTEID_DOC + " = " + lastIdCustomerPanel;
                                    List<int> idsDocuments = DocumentModel.getListIntValue(query);
                                    if (idsDocuments != null)
                                    {
                                        foreach (int idDocument in idsDocuments)
                                        {
                                            query = "UPDATE " + LocalDatabase.TABLA_DOCUMENTOVENTA + " SET " + LocalDatabase.CAMPO_CLIENTEID_DOC +
                                            " = " + cm.CLIENTE_ID + " WHERE " + LocalDatabase.CAMPO_ID_DOC + " = " + idDocument;
                                            DocumentModel.updateARecord(query);
                                        }
                                    }
                                    int lastNewIdClienteLocal = CustomerADCModel.getNewClientIdLocal(db, lastIdCustomerPanel);
                                    CustomerADCModel.deleteClienteNuevoSinDocumentosByPanelId(db, lastIdCustomerPanel);
                                    CustomerModel.deleteACustomer(db, Convert.ToInt32("-" + lastNewIdClienteLocal));
                                    idInserted = CustomerModel.createCustomer(db, cm);
                                }
                                else
                                {

                                }
                            } else
                            {
                                if (CustomerModel.checkIfTheClientExists(db, cm.CLIENTE_ID))
                                {
                                    idInserted = CustomerModel.updateCustomer(db, cm);
                                }
                                else
                                {
                                    idInserted = CustomerModel.createCustomer(db, cm);
                                }
                            }
                        }
                        errorMessage = "Cliente Guardado Correctamente";
                    }
                    else
                    {
                        idInserted = -2;
                        errorMessage = "Cliente No Encontrado";
                    }
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    idInserted = -1;
                    errorMessage = e.Message;
                }
                finally
                {
                    response.valor = idInserted;
                    response.description = errorMessage;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> getACustomerLAN(int lastIdCustomerPanel)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                ClsClienteModel customerModel = null;
                try
                {
                    String comInstance = InstanceSQLSEModel.getStringComInstance();
                    if (lastIdCustomerPanel < 0)
                    {
                        String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                        String customerCode = ClsClienteModel.getCodeCliente(panelInstance, Math.Abs(lastIdCustomerPanel));
                        customerModel = ClsCustomersModel.getCustomerByCode(comInstance, customerCode);
                    } else
                    {
                        customerModel = ClsCustomersModel.getCustomer(comInstance, lastIdCustomerPanel);
                    }
                    if (customerModel != null)
                    {
                        value = 1;
                        saveCustomerDownloaded(customerModel, lastIdCustomerPanel);
                        description = "Cliente Guardado Correctamente";
                    }
                    else
                    {
                        description = "Cliente No Encontrado en el sistema de Comercial Premium";
                        if (lastIdCustomerPanel >= 0)
                            deleteACustomerNotDownloaded(lastIdCustomerPanel);
                    }
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    value = -1;
                    description = e.Message;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                    response.customerModel = customerModel;
                }
            });
            return response;
        }

        private static async Task deleteACustomerNotDownloaded(int idCustomer)
        {
            await Task.Run(async () =>
            {
                CustomerModel.deleteACustomer(idCustomer);
            });
        }

        private static async Task saveCustomerDownloaded(ClsClienteModel customerModel, int idCustomer)
        {
            await Task.Run(async () =>
            {
                if (CustomerModel.checkIfTheClientExists(customerModel.CLIENTE_ID))
                {
                    if (idCustomer < 0)
                    {
                        if (CustomerADCModel.isTheCustomerSendedByIdPanel(Math.Abs(idCustomer)))
                        {
                            String query = "SELECT " + LocalDatabase.CAMPO_ID_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA +
                            " WHERE " + LocalDatabase.CAMPO_CLIENTEID_DOC + " = " + idCustomer;
                            List<int> idsDocuments = DocumentModel.getListIntValue(query);
                            if (idsDocuments != null)
                            {
                                foreach (int idDocument in idsDocuments)
                                {
                                    DocumentModel.updateCustomer(idDocument, customerModel.CLIENTE_ID, customerModel.CLAVE);
                                }
                            }
                            int lastNewIdClienteLocal = CustomerADCModel.getNewClientIdLocal(idCustomer);
                            CustomerADCModel.deleteClienteNuevoSinDocumentosByPanelId(idCustomer);
                            CustomerModel.deleteACustomer(Convert.ToInt32("-" + lastNewIdClienteLocal));
                            if (CustomerModel.checkIfTheClientExists(customerModel.CLIENTE_ID))
                                CustomerModel.updateCustomer(customerModel);
                            else CustomerModel.createCustomer(customerModel);
                        }
                        else
                        {
                            String query = "SELECT " + LocalDatabase.CAMPO_ID_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA +
                            " WHERE " + LocalDatabase.CAMPO_CLIENTEID_DOC + " = " + idCustomer;
                            List<int> idsDocuments = DocumentModel.getListIntValue(query);
                            if (idsDocuments != null)
                            {
                                foreach (int idDocument in idsDocuments)
                                {
                                    DocumentModel.updateCustomer(idDocument, customerModel.CLIENTE_ID, customerModel.CLAVE);
                                }
                            }
                            int lastNewIdClienteLocal = CustomerADCModel.getNewClientIdLocal(idCustomer);
                            CustomerADCModel.deleteClienteNuevoSinDocumentosByPanelId(idCustomer);
                            CustomerModel.deleteACustomer(Convert.ToInt32("-" + lastNewIdClienteLocal));
                            if (CustomerModel.checkIfTheClientExists(customerModel.CLIENTE_ID))
                                CustomerModel.updateCustomer(customerModel);
                            else CustomerModel.createCustomer(customerModel);
                        }
                    }
                    else
                    {
                        if (CustomerModel.checkIfTheClientExists(customerModel.CLIENTE_ID))
                            CustomerModel.updateCustomer(customerModel);
                        else CustomerModel.createCustomer(customerModel);
                    }
                }
                else
                {
                    if (idCustomer < 0)
                    {
                        if (CustomerADCModel.isTheCustomerSendedByIdPanel(Math.Abs(idCustomer)))
                        {
                            String query = "SELECT " + LocalDatabase.CAMPO_ID_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA +
                            " WHERE " + LocalDatabase.CAMPO_CLIENTEID_DOC + " = " + idCustomer;
                            List<int> idsDocuments = DocumentModel.getListIntValue(query);
                            if (idsDocuments != null)
                            {
                                foreach (int idDocument in idsDocuments)
                                {
                                    DocumentModel.updateCustomer(idDocument, customerModel.CLIENTE_ID, customerModel.CLAVE);
                                }
                            }
                            int lastNewIdClienteLocal = CustomerADCModel.getNewClientIdLocal(idCustomer);
                            CustomerADCModel.deleteClienteNuevoSinDocumentosByPanelId(idCustomer);
                            CustomerModel.deleteACustomer(Convert.ToInt32("-" + lastNewIdClienteLocal));
                            if (CustomerModel.checkIfTheClientExists(customerModel.CLIENTE_ID))
                                CustomerModel.updateCustomer(customerModel);
                            else CustomerModel.createCustomer(customerModel);
                        }
                        else
                        {
                            String query = "SELECT " + LocalDatabase.CAMPO_ID_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA +
                            " WHERE " + LocalDatabase.CAMPO_CLIENTEID_DOC + " = " + idCustomer;
                            List<int> idsDocuments = DocumentModel.getListIntValue(query);
                            if (idsDocuments != null)
                            {
                                foreach (int idDocument in idsDocuments)
                                {
                                    DocumentModel.updateCustomer(idDocument, customerModel.CLIENTE_ID, customerModel.CLAVE);
                                }
                            }
                            int lastNewIdClienteLocal = CustomerADCModel.getNewClientIdLocal(idCustomer);
                            CustomerADCModel.deleteClienteNuevoSinDocumentosByPanelId(idCustomer);
                            CustomerModel.deleteACustomer(Convert.ToInt32("-" + lastNewIdClienteLocal));
                            if (CustomerModel.checkIfTheClientExists(customerModel.CLIENTE_ID))
                                CustomerModel.updateCustomer(customerModel);
                            else CustomerModel.createCustomer(customerModel);
                        }
                    }
                    else
                    {
                        if (CustomerModel.checkIfTheClientExists(customerModel.CLIENTE_ID))
                            CustomerModel.updateCustomer(customerModel);
                        else CustomerModel.createCustomer(customerModel);
                    }
                }
            });
        }

        public static async Task<ExpandoObject> downloadAllCustomersLAN(int downloadType, int lastId)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                try
                {
                    int count = 0;
                    String comInstance = InstanceSQLSEModel.getStringComInstance();
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    String boxCode = UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn());
                    dynamic responseCustomer = ClsCustomersModel.getAllCustomersFromAnUser(comInstance, panelInstance, boxCode, true, lastId, 0);
                    if (responseCustomer.value == 1)
                    {
                        List<ClsClienteModel> customersList = responseCustomer.customersList;
                        if (customersList != null && customersList.Count > 0)
                        {
                            int itemsToEvaluate = customersList.Count;
                            if (lastId == 0 && downloadType == ClsInitialChargeController.INITIAL_CHARGE)
                                CustomerModel.deleteAllCustomersInLocalDb();
                            if (downloadType == ClsInitialChargeController.INITIAL_CHARGE)
                                count = CustomerModel.saveCustomersLAN(customersList);
                            else
                            {
                                count = CustomerModel.updateCustomersLAN(customersList);
                            }
                            if (count == itemsToEvaluate)
                                value = 1;
                        }
                        else description = "No se contró ningún cliente en el sistema Comercial";
                    } else
                    {
                        value = responseCustomer.value;
                        description = responseCustomer.description;
                    }
                } catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    value = -1;
                    description = e.Message;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> getNameLAN(int idCustomer)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                String name = "";
                try
                {
                    String comInstance = InstanceSQLSEModel.getStringComInstance();
                    name = ClsCustomersModel.getName(comInstance, idCustomer);
                    value = 1;
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    value = -1;
                    description = e.Message;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                    response.name = name;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> getDescuentoClienteLAN(int idCustomer)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                double discount = 0;
                try
                {
                    String comInstance = InstanceSQLSEModel.getStringComInstance();
                    discount = ClsCustomersModel.getDescuentoCliente(comInstance, idCustomer);
                    value = 1;
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    value = -1;
                    description = e.Message;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                    response.discount = discount;
                }
            });
            return response;
        }

    }
}
