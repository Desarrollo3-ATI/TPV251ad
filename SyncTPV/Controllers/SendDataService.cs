using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using wsROMClases;
using wsROMClases.Models;
using wsROMClases.Models.Panel;

namespace SyncTPV.Controllers
{

    public class SendDataService
    {
        private static int TIEMPO_PARAWS = 1000;
        public String idDocumento;
        public int envioDeDatos;
        public int peticiones;
        public int method;

        public async Task<ExpandoObject> sendDataByMethods(int method, int envioDeDatos, int peticiones,
            String idDocumento, bool serverModeLAN)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                this.idDocumento = idDocumento;
                this.envioDeDatos = envioDeDatos;
                this.peticiones = peticiones;
                this.method = method;
                try
                {

                    if (peticiones > 0)
                        TIEMPO_PARAWS = 20000;
                    else if (peticiones == 0)
                        TIEMPO_PARAWS = 10000;
                    /** 1 = Clientes, 2 = Documentos, 3 = Posiciones, 4 = Cxc */
                    if (method == 1)
                    {
                        if (serverModeLAN)
                        {
                            dynamic responseCustomer = await enviarClientesAdicionalesLAN(method, envioDeDatos, peticiones);
                            value = responseCustomer.value;
                            description = responseCustomer.description;
                            method = responseCustomer.method;
                            envioDeDatos = responseCustomer.envioDeDatos;
                            peticiones = responseCustomer.peticiones;
                            idDocumento = responseCustomer.idDocumento;
                        }
                        else
                        {
                            dynamic responseCustomer = await enviarClientesAdicionalesAPI(method, envioDeDatos, peticiones);
                            value = responseCustomer.value;
                            description = responseCustomer.description;
                            method = responseCustomer.method;
                            envioDeDatos = responseCustomer.envioDeDatos;
                            peticiones = responseCustomer.peticiones;
                            idDocumento = responseCustomer.idDocumento;
                        }
                    }
                    else if (method == 2)
                    {
                        if (serverModeLAN)
                        {
                            dynamic responseDocumento = await handleActionSendDocumentsLAN(method, envioDeDatos, peticiones);
                            value = responseDocumento.value;
                            description = responseDocumento.description;
                            method = responseDocumento.method;
                            envioDeDatos = responseDocumento.envioDeDatos;
                            peticiones = responseDocumento.peticiones;
                            idDocumento = responseDocumento.idDocumento;
                        }
                        else
                        {
                            dynamic responseDocumento = await handleActionSendDocumentsAPI(method, envioDeDatos, peticiones);
                            value = responseDocumento.value;
                            description = responseDocumento.description;
                            method = responseDocumento.method;
                            envioDeDatos = responseDocumento.envioDeDatos;
                            peticiones = responseDocumento.peticiones;
                            idDocumento = responseDocumento.idDocumento;
                        }
                    }
                    else if (method == 3)
                    {
                        if (serverModeLAN)
                        {
                            dynamic responseLocation = await enviarUbicacionesLAN(method, envioDeDatos, peticiones);
                            value = responseLocation.value;
                            description = responseLocation.description;
                            method = responseLocation.method;
                            envioDeDatos = responseLocation.envioDeDatos;
                            peticiones = responseLocation.peticiones;
                            idDocumento = responseLocation.idDocumento;
                        }
                        else
                        {
                            dynamic responseLocation = await enviarUbicacionesAPI(method, envioDeDatos, peticiones);
                            value = responseLocation.value;
                            description = responseLocation.description;
                            method = responseLocation.method;
                            envioDeDatos = responseLocation.envioDeDatos;
                            peticiones = responseLocation.peticiones;
                            idDocumento = responseLocation.idDocumento;
                        }
                    }
                    else if (method == 4)
                    {
                        if (serverModeLAN)
                        {
                            dynamic responseCxc = await enviarCuentasPorCobrarLAN(method, envioDeDatos, peticiones);
                            value = responseCxc.value;
                            description = responseCxc.description;
                            method = responseCxc.method;
                            envioDeDatos = responseCxc.envioDeDatos;
                            peticiones = responseCxc.peticiones;
                            idDocumento = responseCxc.idDocumento;
                        }
                        else
                        {
                            dynamic responseCxc = await enviarCuentasPorCobrarAPI(method, envioDeDatos, peticiones);
                            value = responseCxc.value;
                            description = responseCxc.description;
                            method = responseCxc.method;
                            envioDeDatos = responseCxc.envioDeDatos;
                            peticiones = responseCxc.peticiones;
                            idDocumento = responseCxc.idDocumento;
                        }
                    }
                    else if (method == 5)
                    {
                        if (serverModeLAN)
                        {
                            dynamic responseRetiros = await enviarRetirosLAN(method, envioDeDatos, peticiones);
                            value = responseRetiros.value;
                            description = responseRetiros.description;
                            method = responseRetiros.method;
                            envioDeDatos = responseRetiros.envioDeDatos;
                            peticiones = responseRetiros.peticiones;
                            idDocumento = responseRetiros.idDocumento;
                        }
                        else
                        {
                            dynamic responseRetiros = await enviarRetirosAPI(method, envioDeDatos, peticiones);
                            value = responseRetiros.value;
                            description = responseRetiros.description;
                            method = responseRetiros.method;
                            envioDeDatos = responseRetiros.envioDeDatos;
                            peticiones = responseRetiros.peticiones;
                            idDocumento = responseRetiros.idDocumento;
                        }
                    }
                    else if (method == 6)
                    {
                        if (serverModeLAN)
                        {
                            dynamic responseIngresos = await enviarIngresosLAN(method, envioDeDatos, peticiones);
                            value = responseIngresos.value;
                            description = responseIngresos.description;
                            method = responseIngresos.method;
                            envioDeDatos = responseIngresos.envioDeDatos;
                            peticiones = responseIngresos.peticiones;
                            idDocumento = responseIngresos.idDocumento;
                        }
                        else
                        {
                            dynamic responseIngresos = await enviarIngresosAPI(method, envioDeDatos, peticiones);
                            value = responseIngresos.value;
                            description = responseIngresos.description;
                            method = responseIngresos.method;
                            envioDeDatos = responseIngresos.envioDeDatos;
                            peticiones = responseIngresos.peticiones;
                            idDocumento = responseIngresos.idDocumento;
                        }
                    }
                    else if(method == 7)
                    {
                        if (serverModeLAN)
                        {
                            bool ticketerrror = false;
                            //para lan
                            List<int> ticketslistid = TicketsModel.getAllIdsTicketsNotSends();
                            if (ticketslistid != null)
                            {
                                String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                                for (int x = 0; x < ticketslistid.Count; x++)
                                {
                                    TicketsModel tickets = TicketsModel.getTickets(ticketslistid[x]);
                                    if(tickets != null)
                                    {
                                        description = ""+ tickets.referencia;
                                        List<ExpandoObject> responseTickets = ClsTicketsModel.savePanelDataTickets(panelInstance,
                                        tickets.id,
                                        tickets.referencia,
                                        tickets.datos,
                                        tickets.idAgente,
                                        tickets.tipoDocumento,
                                        tickets.fecha,
                                        tickets.idPanel,
                                        tickets.estatus);
                                       
                                        if (responseTickets != null)
                                        {
                                            if(responseTickets.Count > 0)
                                            {
                                                dynamic resultT = responseTickets[0];
                                                TicketsModel.actualizarIdServer(resultT.idIngresoApp, resultT.idIngresoServer);
                                            }
                                            else
                                            {
                                                ticketerrror = true;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            ticketerrror = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (ticketerrror)
                            {
                                value = 100;
                                description =  description+" Error al cargar el ticket";
                                method = 8;
                            }
                            else
                            {
                                value = 100;
                                description = "Procesos Finalizados";
                                method = 8;
                            }
                        }
                        else
                        {
                            dynamic responseIngresos = await enviarTicketsAPI(method, envioDeDatos, peticiones);
                            value = responseIngresos.value;
                            description = responseIngresos.description;
                            method = responseIngresos.method;
                            envioDeDatos = responseIngresos.envioDeDatos;
                            peticiones = responseIngresos.peticiones;
                            idDocumento = responseIngresos.idDocumento;
                        }
                    }
                    else if (method >= 8)
                    {
                        value = 100;
                        description = "Proceso Finalizado";
                        method = 9;
                        idDocumento = "";
                    }

                }
                catch (Exception ex)
                {
                    SECUDOC.writeLog(ex.ToString());
                    value = -1;
                    description = ex.Message;
                    method = 0;
                    envioDeDatos = 0;
                    peticiones = 0;
                    idDocumento = "";
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                    response.method = method;
                    response.envioDeDatos = envioDeDatos;
                    response.peticiones = peticiones;
                    response.idDocumento = idDocumento;
                }
            });
            return response;
        }

        private async Task<ExpandoObject> enviarClientesAdicionalesAPI(int method, int envioDeDatos, int peticiones)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            String idDocumento = "";
            await Task.Run(async () =>
            {
                try
                {
                    List<CustomerADCModel> cadcList = CustomerADCModel.getAllNotSendClientesADC(0);
                    if (cadcList != null)
                    {
                        UserModel user = UserModel.getNameIdAndRutaOfTheUser();
                        for (int i = 0; i < 1; i++)
                        {
                            String clave = CustomerModel.getClaveForAClient(Convert.ToInt32("-" + cadcList[i].id));
                            if (DocumentModel.verifyIfACustomerHaveAnyDocumentWithContext(clave))
                            {
                                response = await enviarClienteAlWs(cadcList[i].id, user.Nombre, cadcList[i].nombre,
                                        cadcList[i].zona, cadcList[i].ciudad, cadcList[i].estado,
                                        cadcList[i].calle, cadcList[i].numero, cadcList[i].colonia,
                                        cadcList[i].poblacion, cadcList[i].referencia,
                                        cadcList[i].telefono, cadcList[i].cp, cadcList[i].email,
                                        cadcList[i].rfc, "", cadcList[i].tipoContribuyente, cadcList[i].codigoRegimenFiscal, cadcList[i].codigoUsoCFDI,
                                        method, envioDeDatos, peticiones);
                            }
                            else
                            {
                                value = 20;
                                description = "Enviando clientes...";
                                method += 1;
                            }
                        }
                    }
                    else
                    {
                        value = 20;
                        description = "Enviando datos...";
                        method = 2;
                    }
                }
                catch (Exception ex)
                {
                    SECUDOC.writeLog(ex.ToString());
                    value = -1;
                    description = ex.Message+", Enviando Clientes!";
                    method = 2;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                    response.method = method;
                    response.envioDeDatos = envioDeDatos;
                    response.peticiones = peticiones;
                    response.idDocumento = idDocumento;
                }
            });
            return response;
        }

        private async Task<ExpandoObject> enviarClientesAdicionalesLAN(int method, int envioDeDatos, int peticiones)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                List<CustomerADCModel> cadcList = CustomerADCModel.getAllNotSendClientesADC(0);
                if (cadcList != null)
                {
                    UserModel user = UserModel.getNameIdAndRutaOfTheUser();
                    for (int i = 0; i < 1; i++)
                    {
                        String clave = CustomerModel.getClaveForAClient(Convert.ToInt32("-" + cadcList[i].id));
                        if (DocumentModel.verifyIfACustomerHaveAnyDocumentWithContext(clave))
                        {
                            response = await enviarClienteLAN(cadcList[i].id, user.Nombre, cadcList[i].nombre,
                                    cadcList[i].zona, cadcList[i].ciudad, cadcList[i].estado,
                                    cadcList[i].calle, cadcList[i].numero, cadcList[i].colonia,
                                    cadcList[i].poblacion, cadcList[i].referencia,
                                    cadcList[i].telefono, cadcList[i].cp, cadcList[i].email,
                                    cadcList[i].rfc, "", method, envioDeDatos, peticiones);
                        }
                        else
                        {
                            response.value = 20;
                            response.description = "Enviando clientes...";
                            response.method = method + 1;
                            response.envioDeDatos = envioDeDatos;
                            response.peticiones = 0;
                            response.idDocumento = "";
                        }
                    }
                }
                else
                {
                    response.value = 20;
                    response.description = "Enviando datos...";
                    response.method = 2;
                    response.envioDeDatos = envioDeDatos;
                    response.peticiones = 0;
                    response.idDocumento = "";
                }
            });
            return response;
        }

        private class ResponseCustomer
        {
            public List<CustomerADCResponse> response { get; set; }
        }

        private class CustomerADCResponse
        {
            public int valor { get; set; }
            public String descripcion { get; set; }
        }

        public async Task<ExpandoObject> obtenerTicketsDelWs(
            bool banderaFecha,String Fecha, String FechaMax, int idAgente, String referencia, String tipo, int limite
            )
        { 
            dynamic response = new ExpandoObject();
            await Task.Run(() => {
                try
                {
                    String errorMessage = "";
                    int itemsToEvaluate = 0;
                    int correctResponse = 0;
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    // client.Authenticator = new HttpBasicAuthenticator(username, password);
                    var request = new RestRequest("/filtrarTicketsWS", Method.Post);
                    request.AddJsonBody(new
                    {
                        banderaFecha = banderaFecha,
                        fecha = Fecha,
                        fechamax = FechaMax,
                        idAgente = idAgente,
                        referencia = referencia,
                        tipo = tipo,
                        limite = limite
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var jsonResp = JsonConvert.DeserializeObject<dynamic>(responseHeader.Result.Content);
                        dynamic routesResponse = (dynamic)jsonResp;
                        response.respuesta = routesResponse.respuesta;
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        response.respuesta = null;
                        if (responseHeader.Result.ErrorMessage.Equals("Unable to connect to the remote server"))
                        {
                            correctResponse = 404;
                            errorMessage = "Tiempo Excedido! " + responseHeader.Result.ErrorMessage;
                        }
                        else
                        {
                            correctResponse = 500;
                            errorMessage = "Tiempo Excedido! " + responseHeader.Result.ErrorMessage;
                        }
                    }
                    response.value = correctResponse;
                    response.description = errorMessage;
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    response.value = -1;
                    response.description = "Exception: " + e.ToString();
                    response.respuesta = null;
                }
            });
            return response;
        }

        public async Task<ExpandoObject> obtenerErroresDelPANELWs()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(() => {
                try
                {
                    String errorMessage = "";
                    int itemsToEvaluate = 0;
                    int correctResponse = 0;
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    // client.Authenticator = new HttpBasicAuthenticator(username, password);
                    var request = new RestRequest("/ObtenerErroresPanelWS", Method.Post);
                    request.AddJsonBody(new
                    {});
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var jsonResp = JsonConvert.DeserializeObject<dynamic>(responseHeader.Result.Content);
                        dynamic routesResponse = (dynamic)jsonResp;
                        response.respuesta = routesResponse.respuesta;
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        response.respuesta = null;
                        if (responseHeader.Result.ErrorMessage.Equals("Unable to connect to the remote server"))
                        {
                            correctResponse = 404;
                            errorMessage = "Tiempo Excedido! " + responseHeader.Result.ErrorMessage;
                        }
                        else
                        {
                            correctResponse = 500;
                            errorMessage = "Tiempo Excedido! " + responseHeader.Result.ErrorMessage;
                        }
                    }
                    response.value = correctResponse;
                    response.description = errorMessage;
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    response.value = -1;
                    response.description = "Exception: " + e.ToString();
                    response.respuesta = null;
                }
            });
            return response;
        }
        private static async Task<ExpandoObject> enviarClienteAlWs(int idClienteLocal, String nombreUsuario, String nombreCliente,
            int clasificationId1, int ciudadId, int estadoId, String calle, String numero, String colonia, String poblacion,
            String referencia, String telefono, String codigoPostal, String email, String rfc, String observations,
            int tipoContribuyente, String codigoRegimenFiscal, String codigoUsoCFDI, int method, int envioDatos, int Peticiones)
        {
            dynamic Realresponse = new ExpandoObject();
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                try
                {
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/insertCustomerAdc", Method.Post);
                    request.AddJsonBody(new
                    {
                        nombreUsuario = nombreUsuario,
                        nombreCliente = nombreCliente,
                        clasificationId1 = clasificationId1,
                        ciudadId = ciudadId,
                        estadoId = estadoId,
                        calle = calle,
                        numero = numero,
                        colonia = colonia,
                        poblacion = poblacion,
                        referencia = referencia,
                        telefono = telefono,
                        codigoPostal = codigoPostal,
                        email = email,
                        rfc = rfc,
                        observations = observations,
                        tipoContribuyente = tipoContribuyente,
                        codigoRegimenFiscal = codigoRegimenFiscal,
                        codigoUsoCFDI = codigoUsoCFDI
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content; // Raw content as string
                        dynamic jsonResp = JsonConvert.DeserializeObject<ExpandoObject>(content);
                        if (jsonResp.value > 0)
                        {
                            int idClienteAdc = jsonResp.idClienteAdc;
                            if (CustomerADCModel.updateEnviadoYComercialId(idClienteAdc, idClienteLocal) > 0)
                            {
                                if (idClienteLocal > 0)
                                {
                                    String idLocalText = "-" + idClienteLocal;
                                    idClienteLocal = Convert.ToInt32(idLocalText);
                                    String idServerText = "-" + idClienteAdc;
                                    idClienteAdc = Convert.ToInt32(idServerText);
                                }
                                DocumentModel.updateCustomerId(idClienteLocal, idClienteAdc);
                            }
                            value = 1;
                        }
                        else
                        {
                            description = jsonResp.description;
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        value = -400;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value,
                            responseHeader.Result.ErrorException.Message);
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.TimedOut)
                    {
                        value = -404;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value,
                            responseHeader.Result.ErrorException.Message);
                    }
                    else
                    {
                        value = -500;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value,
                            responseHeader.Result.ErrorException.Message);
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
                    if (value == 1)
                    {
                        Realresponse.value = 1;
                        Realresponse.description = "Enviando datos...";
                        Realresponse.method = method + 1;
                        Realresponse.envioDeDatos = envioDatos;
                        Realresponse.peticiones = Peticiones;
                        Realresponse.idDocumento = "";
                    }
                    else
                    {
                        Realresponse.value = -1;
                        Realresponse.description = "Enviando datos...";
                        Realresponse.method = method + 1;
                        Realresponse.envioDeDatos = envioDatos;
                        Realresponse.peticiones = Peticiones;
                        Realresponse.idDocumento = "";
                    }
                }
            });
            return Realresponse;
        }

        private async Task<ExpandoObject> enviarClienteLAN(int id, String nombreU, String nombre, int zonaCliId, int ciudadId, int estadoId, String calle,
                                   String numero, String colonia, String poblacion, String referencia, String telefono, String cp, String email, String rfc,
                                   String notas, int method, int envioDeDatos, int peticiones)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                try
                {
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    int tipoContribuyente = 0;
                    String codigoRegimenFiscal = "", codigoUsosCFDI = "";
                    dynamic responseCustomer = ClsClienteADCModel.createClienteADC(panelInstance, nombreU, nombre, zonaCliId, ciudadId, estadoId, calle, numero, colonia,
                    poblacion, referencia, telefono, cp, email, rfc, notas, tipoContribuyente, codigoRegimenFiscal,
                    codigoUsosCFDI);
                    if (responseCustomer.value > 0)
                    {
                        int idCustomerSaved = responseCustomer.idClienteAdc;
                        if (CustomerADCModel.updateEnviadoYComercialId(idCustomerSaved, id) > 0)
                        {
                            response.value = 20;
                            response.description = "Enviando clientes...";
                            response.method = method;
                            response.envioDeDatos = envioDeDatos;
                            response.peticiones = 0;
                            response.idDocumento = "";
                        }
                    }
                    else
                    {
                        response.value = 20;
                        response.description = "No se pudo subir a los clientes";
                        response.method = method;
                        response.envioDeDatos = envioDeDatos;
                        response.peticiones = 0;
                        response.idDocumento = "";
                    }
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog("Exception: " + e.ToString());
                    response.value = -1;
                    response.description = e.Message+", Enviando Clientes!";
                    response.method = method;
                    response.envioDeDatos = envioDeDatos;
                    response.peticiones = 0;
                    response.idDocumento = "";
                }
            });
            return response;
        }

        private async Task<ExpandoObject> handleActionSendDocumentsAPI(int method, int envioDeDatos, int peticiones)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                List<DocumentModel> todosLosDocs = DocumentModel.obtenerTodosLosDocumentosNoEnviadosAlWs(0);
                if (todosLosDocs != null && todosLosDocs.Count > 0)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        if (todosLosDocs[i].idWebService != 0)
                        {
                            /** Enviar todos los movimientos de este documento */
                            response.value = 40;
                            response.description = "Documentos Enviados!";
                            response.method = 3;
                            response.envioDeDatos = envioDeDatos;
                            response.peticiones = 0;
                            response.idDocumento = todosLosDocs[i].id + "-" + todosLosDocs[i].idWebService;
                        }
                        else
                        {
                            if (todosLosDocs[i].cliente_id < 0)
                            {
                                int idAdditionalCustomerServer = CustomerADCModel.getNewClientIdPanel(todosLosDocs[i].cliente_id);
                                response = await armarJSONForDocument(method, envioDeDatos, todosLosDocs[i].id, idAdditionalCustomerServer);
                            }
                            else
                            {
                                response = await armarJSONForDocument(method, envioDeDatos, todosLosDocs[i].id, 0);
                            }
                        }
                    }
                }
                else
                {
                    response.value = 40;
                    response.description = "Enviando datos...";
                    response.method = 3;
                    response.envioDeDatos = envioDeDatos;
                    response.peticiones = peticiones;
                    response.idDocumento = "";
                }
            });
            
            return response;
        }

        private async Task<ExpandoObject> handleActionSendDocumentsLAN(int method, int envioDeDatos, int peticiones)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                List<DocumentModel> todosLosDocs = DocumentModel.obtenerTodosLosDocumentosNoEnviadosAlWs(0);
                if (todosLosDocs != null)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        if (todosLosDocs[i].idWebService != 0)
                        {
                            /** Enviar todos los movimientos de este documento */
                            response.value = 40;
                            response.description = "Documentos Enviados!";
                            response.method = 3;
                            response.envioDeDatos = envioDeDatos;
                            response.peticiones = 0;
                            response.idDocumento = todosLosDocs[i].id + "-" + todosLosDocs[i].idWebService;
                        }
                        else
                        {
                            if (todosLosDocs[i].cliente_id < 0)
                            {
                                int idAdditionalCustomerServer = CustomerADCModel.getNewClientIdPanel(todosLosDocs[i].cliente_id);
                                response = await armarJSONForDocumentLAN(method, envioDeDatos, todosLosDocs[i].id, idAdditionalCustomerServer);
                            }
                            else
                            {
                                response = await armarJSONForDocumentLAN(method, envioDeDatos, todosLosDocs[i].id, 0);
                            }
                        }
                    }
                }
                else
                {
                    response.value = 40;
                    response.description = "Enviando datos...";
                    response.method = 3;
                    response.envioDeDatos = envioDeDatos;
                    response.peticiones = peticiones;
                    response.idDocumento = "";
                }
            });
            return response;
        }

        private async Task<ExpandoObject> armarJSONForDocument(int method, int envioDeDatos, int idDocumentLocal, 
            int idAdditionalCustomerServer)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                String jsonDocument = "";
                String jsonMovement = "";
                String jsonFcDocument = "";
                int idPosition = ClsPositionsModel.getIdPositionFromTheDocument(idDocumentLocal);
                DocumentModel dvm = DocumentModel.getAllDataDocumentNotSent(idDocumentLocal);
                if (dvm != null)
                {
                    jsonDocument +=
                            "{\n";
                    if (idAdditionalCustomerServer == 0)
                    {
                        jsonDocument += " \"claveCliente\": \"" + dvm.clave_cliente + "\",\n" +
                                " \"clienteId\": " + dvm.cliente_id + ",\n";
                    }
                    else
                    {
                        jsonDocument += " \"claveCliente\": \"" + dvm.clave_cliente + "\",\n" +
                                " \"clienteId\": " + idAdditionalCustomerServer + ",\n";
                        dvm.cliente_id = -idAdditionalCustomerServer;
                    }
                    jsonDocument += " \"descuento\": " + dvm.descuento + ",\n" +
                    " \"total\": " + (dvm.total + dvm.descuento) + ",\n" +
                    " \"nombreUsuario\": \"" + dvm.nombreu + "\",\n" +
                    " \"almacenId\": " + dvm.almacen_id + ",\n" +
                    " \"anticipo\": " + dvm.anticipo + ",\n" +
                    " \"tipoDocumento\": " + dvm.tipo_documento + ",\n" +
                    " \"formaCobroId\": " + dvm.forma_cobro_id + ",\n" +
                    " \"factura\": " + dvm.factura + ",\n" +
                    " \"observacion\": \"" + dvm.observacion + "\",\n" +
                    " \"dev\": " + dvm.dev + ",\n" +
                    " \"folioVenta\": \"" + dvm.fventa + "\",\n" +
                    " \"fechaHora\": \"" + dvm.fechahoramov + "\",\n" +
                    " \"usuarioId\": " + dvm.usuario_id + ",\n" +
                    " \"formaCobroIdAbono\": " + dvm.forma_corbo_id_abono + ",\n" +
                    " \"cIddoctoPedidoCC\": " + dvm.ciddoctopedidocc + ",\n";
                    WeightModel wm = new WeightModel();
                    if (UserModel.doYouHavePermissionPrepedido())
                    {
                        bool isPrepedido = DocumentModel.isItDocumentFromPrepedidoSurtido(idDocumentLocal);
                        if (isPrepedido)
                        {
                            String query = "SELECT " + LocalDatabase.CAMPO_ID_MOV + " FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " +
                                LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " + idDocumentLocal;
                            int idMovement = MovimientosModel.getIntValue(query);
                            wm = WeightModel.getAWeight(idMovement);
                            if (wm != null)
                            {
                                jsonDocument += " \"importeExtra1\": " + wm.pesoBruto + ",\n" +
                                " \"importeExtra2\": " + wm.pesoCaja + ",\n" +
                                " \"importeExtra3\": " + wm.pesoPolloLesionado + ",\n" +
                                " \"importeExtra4\": " + wm.pesoPolloMuerto + ",\n" +
                                " \"textoExtra2\": " + wm.pesoPolloBajoDePeso + ",\n" +
                                " \"textoExtra3\": " + wm.pesoPolloGolpeado + ",\n";
                            }
                            else
                            {
                                jsonDocument += " \"importeExtra1\": " + 0 + ",\n" +
                                " \"importeExtra2\": " + 0 + ",\n" +
                                " \"importeExtra3\": " + 0 + ",\n" +
                                " \"importeExtra4\": " + 0 + ",\n" +
                                " \"textoExtra2\": \"\",\n" +
                                " \"textoExtra3\": \"\",\n";
                            }
                        }
                        else
                        {
                            jsonDocument += " \"importeExtra1\": " + 0 + ",\n" +
                                " \"importeExtra2\": " + 0 + ",\n" +
                                " \"importeExtra3\": " + 0 + ",\n" +
                                " \"importeExtra4\": " + 0 + ",\n" +
                                " \"textoExtra2\": \"\",\n" +
                                " \"textoExtra3\": \"\",\n";
                        }
                    }
                    else
                    {
                        jsonDocument += " \"importeExtra1\": " + 0 + ",\n" +
                            " \"importeExtra2\": " + 0 + ",\n" +
                            " \"importeExtra3\": " + 0 + ",\n" +
                            " \"importeExtra4\": " + 0 + ",\n" +
                            " \"textoExtra2\": \"\",\n" +
                            " \"textoExtra3\": \"\",\n";
                    }
                    List<MovimientosModel> movementsList = MovimientosModel.getAllNotSendMovimientosFromADocumnt(idDocumentLocal, 0);
                    if (movementsList != null)
                    {
                        jsonMovement += " \"movementList\": "+ JsonConvert.SerializeObject(movementsList);
                    }
                    List<FormasDeCobroDocumentoModel> fcDocumentoList = FormasDeCobroDocumentoModel.getAllTheWaysToCollectADocument(idDocumentLocal);
                    if (fcDocumentoList != null)
                    {
                        jsonFcDocument = ", \"fcDocumentoList\": "+ JsonConvert.SerializeObject(fcDocumentoList);
                        /*jsonFcDocument = " \"fcDocumentoList\": [\n";
                        for (int i = 0; i < fcDocumentoList.Count; i++)
                        {
                            jsonFcDocument +=
                                    " { \n" +
                                            "  \"id\":" + fcDocumentoList[i].id + ",\n" +
                                            "  \"formaCobroIdAbono\":" + fcDocumentoList[i].formaCobroIdAbono + ",\n" +
                                            "  \"importe\":" + fcDocumentoList[i].importe + ",\n" +
                                            "  \"totalDocumento\":" + fcDocumentoList[i].totalDocumento + ",\n" +
                                            "  \"cambio\":" + fcDocumentoList[i].cambio + ",\n" +
                                            "  \"saldoDocumento\":" + fcDocumentoList[i].saldoDocumento + ",\n" +
                                            "  \"documentoId\":" + fcDocumentoList[i].documentoId + "";
                            if (i == (fcDocumentoList.Count - 1))
                                jsonFcDocument += "\n}\n";
                            else jsonFcDocument += "\n},\n";
                        }*/
                    } else
                    {
                        jsonFcDocument = ", \"fcDocumentoList\": []";
                    }
                        if (jsonDocument.Equals(""))
                    {
                        response.value = 40;
                        response.description = "Enviando documentos...";
                        response.method = method;
                        response.envioDeDatos = envioDeDatos;
                        response.peticiones = peticiones;
                        response.idDocumento = "";
                    }
                    else
                    {
                        try
                        {
                                jsonDocument += jsonMovement;
                                jsonDocument += jsonFcDocument;
                                jsonDocument += "\n}";
                                response = await sendDataToTheServer(method, idDocumentLocal, envioDeDatos, idDocumentLocal, idPosition,
                                dvm, wm, movementsList, fcDocumentoList, jsonDocument);
                        }
                        catch (Exception e)
                        {
                            SECUDOC.writeLog("Exception: " + e.ToString());
                            if (!jsonDocument.Equals("") && jsonMovement.Equals(""))
                            {
                                if (MovimientosModel.checkIfThereAreStillMovementsForTheDocumentInShift(dvm.id))
                                {
                                    if (MovimientosModel.updateAEnviadoMovimientosDeUnDocumento(dvm.id) > 0)
                                    {
                                        /** Enviar todos los movimientos de este documento */
                                        if (DocumentModel.updateAEnviadoUnDocumento(1, dvm.id) > 0)
                                        {
                                            if (DocumentModel.getDocumentType(dvm.id) == 4)
                                            {
                                                response.value = 40;
                                                response.description = "Enviando documentos...";
                                                response.method = method;
                                                response.envioDeDatos = envioDeDatos;
                                                response.peticiones = peticiones += 1;
                                                response.idDocumento = "";
                                            }
                                            else
                                            {
                                                response.value = 40;
                                                response.description = "Enviando documentos...";
                                                response.method = method;
                                                response.envioDeDatos = envioDeDatos;
                                                response.peticiones = peticiones;
                                                response.idDocumento = dvm.id + "-" + 0;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (DocumentModel.updateAEnviadoUnDocumento(1, dvm.id) > 0)
                                        {
                                            if (DocumentModel.getDocumentType(dvm.id) == 4)
                                            {
                                                response.value = 40;
                                                response.description = "Enviando documentos...";
                                                response.method = method;
                                                response.envioDeDatos = envioDeDatos;
                                                response.peticiones = peticiones += 1;
                                                response.idDocumento = "";
                                            }
                                            else
                                            {
                                                response.value = 40;
                                                response.description = "Enviando documentos...";
                                                response.method = method;
                                                response.envioDeDatos = envioDeDatos;
                                                response.peticiones = peticiones;
                                                response.idDocumento = dvm.id + "-" + 0;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (DocumentModel.cancelADocument(dvm.id) > 0)
                                    {
                                        if (DocumentModel.getDocumentType(dvm.id) == 4)
                                        {
                                            response.value = 40;
                                            response.description = "Enviando documentos...";
                                            response.method = method;
                                            response.envioDeDatos = envioDeDatos;
                                            response.peticiones = peticiones += 1;
                                            response.idDocumento = "";
                                        }
                                        else
                                        {
                                            response.value = 40;
                                            response.description = "Enviando documentos...";
                                            response.method = method;
                                            response.envioDeDatos = envioDeDatos;
                                            response.peticiones = peticiones;
                                            response.idDocumento = dvm.id + "-" + 0;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    response.value = 100;
                    response.description = "Enviando datos...";
                    response.method = 3;
                    response.envioDeDatos = envioDeDatos;
                    response.peticiones = peticiones;
                    response.idDocumento = "";
                }
            });
            
            return response;
        }

        private async Task<ExpandoObject> armarJSONForDocumentLAN(int method, int envioDeDatos, int idDocumentLocal, int idAdditionalCustomerServer)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                ClsDocumentoModel dm = null;
                int idPosition = ClsPositionsModel.getIdPositionFromTheDocument(idDocumentLocal);
                DocumentModel dvm = DocumentModel.getAllDataDocumentNotSent(idDocumentLocal);
                if (dvm != null)
                {
                    dm = new ClsDocumentoModel();
                    if (idAdditionalCustomerServer == 0)
                    {
                        dm.claveCliente = dvm.clave_cliente;
                        dm.clienteId = dvm.cliente_id;
                    }
                    else
                    {
                        dm.claveCliente = dvm.clave_cliente;
                        dm.clienteId = -idAdditionalCustomerServer;
                    }
                    dm.descuento = dvm.descuento;
                    dm.total = (dvm.total + dvm.descuento);
                    dm.nombreUsuario = dvm.nombreu;
                    dm.almacenId = dvm.almacen_id;
                    dm.anticipo = dvm.anticipo;
                    dm.tipoDocumento = dvm.tipo_documento;
                    dm.formaCobroId = dvm.forma_cobro_id;
                    dm.factura = dvm.factura;
                    dm.observacion = dvm.observacion.Replace("\'", "").Replace("\"", "");
                    dm.dev = dvm.dev;
                    dm.folioVenta = dvm.fventa;
                    dm.fechaHora = dvm.fechahoramov;
                    dm.usuarioId = dvm.usuario_id;
                    dm.formaCobroIdAbono = dvm.forma_corbo_id_abono;
                    dm.cIddoctoPedidoCC = dvm.ciddoctopedidocc;
                    if (UserModel.doYouHavePermissionPrepedido())
                    {
                        bool isPrepedido = DocumentModel.isItDocumentFromPrepedidoSurtido(idDocumentLocal);
                        if (isPrepedido)
                        {
                            String query = "SELECT " + LocalDatabase.CAMPO_ID_MOV + " FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " +
                                LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " + idDocumentLocal;
                            int idMovement = MovimientosModel.getIntValue(query);
                            WeightModel wm = WeightModel.getAWeight(idMovement);
                            if (wm != null)
                            {
                                dm.importeExtra1 = wm.pesoBruto;
                                dm.importeExtra2 = wm.pesoCaja;
                                dm.importeExtra3 = wm.pesoPolloLesionado;
                                dm.importeExtra4 = wm.pesoPolloMuerto;
                                dm.textoExtra2 = wm.pesoPolloBajoDePeso + "";
                                dm.textoExtra3 = wm.pesoPolloGolpeado + "";
                            }
                            else
                            {
                                dm.importeExtra1 = 0;
                                dm.importeExtra2 = 0;
                                dm.importeExtra3 = 0;
                                dm.importeExtra4 = 0;
                                dm.textoExtra2 = "";
                                dm.textoExtra3 = "";
                            }
                        }
                        else
                        {
                            dm.importeExtra1 = 0;
                            dm.importeExtra2 = 0;
                            dm.importeExtra3 = 0;
                            dm.importeExtra4 = 0;
                            dm.textoExtra2 = "";
                            dm.textoExtra3 = "";
                        }
                    }
                    else
                    {
                        dm.importeExtra1 = 0;
                        dm.importeExtra2 = 0;
                        dm.importeExtra3 = 0;
                        dm.importeExtra4 = 0;
                        dm.textoExtra2 = "";
                        dm.textoExtra3 = "";
                    }
                    List<ClsMovimientosModel> movementsToSend = new List<ClsMovimientosModel>();
                    ClsMovimientosModel movement = null;
                    List<MovimientosModel> movementsList = MovimientosModel.getAllNotSendMovimientosFromADocumnt(idDocumentLocal, 0);
                    if (movementsList != null)
                    {
                        for (int i = 0; i < movementsList.Count; i++)
                        {
                            String observationMov = "";
                            if (!movementsList[i].observations.Equals(""))
                            {
                                observationMov = movementsList[i].observations.Replace("\"", "");
                                observationMov = observationMov.Replace("'", "");
                            }
                            movement = new ClsMovimientosModel();
                            movement.documentId = movementsList[i].documentId;
                            movement.itemCode = movementsList[i].itemCode;
                            movement.itemId = movementsList[i].itemId;
                            movement.baseUnits = movementsList[i].baseUnits;
                            movement.nonConvertibleUnits = movementsList[i].nonConvertibleUnits;
                            movement.capturedUnits = movementsList[i].capturedUnits;
                            movement.nonConvertibleUnitId = movementsList[i].nonConvertibleUnitId;
                            movement.capturedUnitType = movementsList[i].capturesUnitsType;
                            movement.price = movementsList[i].price;
                            movement.total = movementsList[i].total;
                            movement.position = movementsList[i].position;
                            movement.documentType = movementsList[i].documentType;
                            movement.nameUser = movementsList[i].nameUser;
                            movement.invoice = movementsList[i].invoice;
                            movement.discount = (movementsList[i].descuentoPorcentaje + movementsList[i].rateDiscountPromo);
                            movement.observations = observationMov;
                            movement.iddev = movementsList[i].idDev;
                            movement.comments = "";
                            movement.userId = movementsList[i].userId;
                            movementsToSend.Add(movement);
                        }
                    }
                    dm.movementList = movementsToSend;
                    List<FormaCobroDocumentoModel> formasCobrosList = new List<FormaCobroDocumentoModel>();
                    FormaCobroDocumentoModel formaCobroDocumento = null;
                    List<FormasDeCobroDocumentoModel> fcDocumentoList = FormasDeCobroDocumentoModel.getAllTheWaysToCollectADocument(idDocumentLocal);
                    if (fcDocumentoList != null)
                    {
                        for (int i = 0; i < fcDocumentoList.Count; i++)
                        {
                            formaCobroDocumento = new FormaCobroDocumentoModel();
                            formaCobroDocumento.id = fcDocumentoList[i].id;
                            formaCobroDocumento.formaCobroIdAbono = fcDocumentoList[i].formaCobroIdAbono;
                            formaCobroDocumento.importe = fcDocumentoList[i].importe;
                            formaCobroDocumento.totalDocumento = fcDocumentoList[i].totalDocumento;
                            formaCobroDocumento.cambio = fcDocumentoList[i].cambio;
                            formaCobroDocumento.saldoDocumento = fcDocumentoList[i].saldoDocumento;
                            formaCobroDocumento.documentoId = fcDocumentoList[i].documentoId;
                            formasCobrosList.Add(formaCobroDocumento);
                        }
                    }
                    dm.fcDocumentoList = formasCobrosList;
                    if (dm == null)
                    {
                        response.value = 40;
                        response.description = "Enviando documentos...";
                        response.method = method;
                        response.envioDeDatos = envioDeDatos;
                        response.peticiones = peticiones;
                        response.idDocumento = "";
                    }
                    else
                    {
                        try
                        {
                            response = await sendDataToTheServerLAN(dm, idDocumentLocal, idPosition, method, envioDeDatos);
                        }
                        catch (Exception e)
                        {
                            SECUDOC.writeLog("Exception: " + e.ToString());
                            if (dm != null && dm.movementList == null)
                            {
                                if (MovimientosModel.checkIfThereAreStillMovementsForTheDocumentInShift(dvm.id))
                                {
                                    if (MovimientosModel.updateAEnviadoMovimientosDeUnDocumento(dvm.id) > 0)
                                    {
                                        /** Enviar todos los movimientos de este documento */
                                        if (DocumentModel.updateAEnviadoUnDocumento(1, dvm.id) > 0)
                                        {
                                            if (DocumentModel.getDocumentType(dvm.id) == 4)
                                            {
                                                response.value = 40;
                                                response.description = "Enviando documentos...";
                                                response.method = method;
                                                response.envioDeDatos = envioDeDatos;
                                                response.peticiones = peticiones += 1;
                                                response.idDocumento = "";
                                            }
                                            else
                                            {
                                                response.value = 40;
                                                response.description = "Enviando documentos...";
                                                response.method = method;
                                                response.envioDeDatos = envioDeDatos;
                                                response.peticiones = peticiones;
                                                response.idDocumento = dvm.id + "-" + 0;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (DocumentModel.updateAEnviadoUnDocumento(1, dvm.id) > 0)
                                        {
                                            if (DocumentModel.getDocumentType(dvm.id) == 4)
                                            {
                                                response.value = 40;
                                                response.description = "Enviando documentos...";
                                                response.method = method;
                                                response.envioDeDatos = envioDeDatos;
                                                response.peticiones = peticiones += 1;
                                                response.idDocumento = "";
                                            }
                                            else
                                            {
                                                response.value = 40;
                                                response.description = "Enviando documentos...";
                                                response.method = method;
                                                response.envioDeDatos = envioDeDatos;
                                                response.peticiones = peticiones;
                                                response.idDocumento = dvm.id + "-" + 0;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (DocumentModel.cancelADocument(dvm.id) > 0)
                                    {
                                        if (DocumentModel.getDocumentType(dvm.id) == 4)
                                        {
                                            response.value = 40;
                                            response.description = "Enviando documentos...";
                                            response.method = method;
                                            response.envioDeDatos = envioDeDatos;
                                            response.peticiones = peticiones += 1;
                                            response.idDocumento = "";
                                        }
                                        else
                                        {
                                            response.value = 40;
                                            response.description = "Enviando documentos...";
                                            response.method = method;
                                            response.envioDeDatos = envioDeDatos;
                                            response.peticiones = peticiones;
                                            response.idDocumento = dvm.id + "-" + 0;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    response.value = 100;
                    response.description = "Enviando datos...";
                    response.method = 3;
                    response.envioDeDatos = envioDeDatos;
                    response.peticiones = peticiones;
                    response.idDocumento = "";
                }
            });
            
            return response;
        }

        private async Task<ExpandoObject> sendDataToTheServerLAN(ClsDocumentoModel dm, int idDocLocal, int idPosition, int method, int envioDeDatos)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                try
                {
                    int itemsToEvaluate = 0;
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    dynamic respuesta = ClsDocumentoModel.insertNewDocumentLogic(panelInstance, dm);
                    try
                    {
                        int idDocPanel = respuesta.response[0].valor;
                        List<ExpandoObject> jsonArrayFcDocs = respuesta.response[0].fcDocIds;
                        for (int j = 0; j < jsonArrayFcDocs.Count; j++)
                        {
                            dynamic objectFcDoc = jsonArrayFcDocs[j];
                            int idFcDocApp = objectFcDoc.idFcDocApp;
                            int idFcDocServer = objectFcDoc.idFcDocServer;
                            FormasDeCobroDocumentoModel.updateIdServerInAFcDocument(idFcDocApp, idFcDocServer);
                        }
                        if (idDocPanel > 0)
                        {
                            ClsPositionsModel.updateIdDoctoPanelPosition(idPosition, idDocPanel);
                            if (DocumentModel.addIdWebServiceToTheDocument(idDocLocal, idDocPanel) > 0)
                            {
                                if (MovimientosModel.updateAEnviadoMovimientosDeUnDocumento(idDocLocal) > 0)
                                {
                                    /** Enviar todos los movimientos de este documento */
                                    if (DocumentModel.updateAEnviadoUnDocumento(1, idDocLocal) > 0)
                                    {
                                        if (DocumentModel.getDocumentType(idDocLocal) == 4)
                                        {
                                            response.value = 40;
                                            response.description = "Enviando documentos...";
                                            response.method = method;
                                            response.envioDeDatos = envioDeDatos;
                                            response.peticiones = peticiones += 1;
                                            response.idDocumento = "";
                                        }
                                        else
                                        {
                                            response.value = 40;
                                            response.description = "Enviando documentos...";
                                            response.method = method;
                                            response.envioDeDatos = envioDeDatos;
                                            response.peticiones = peticiones;
                                            response.idDocumento = idDocLocal + "-" + idDocPanel;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (respuesta.response[0].descripcion.Equals("Documento guardado anteriormente"))
                            {
                                if (MovimientosModel.updateAEnviadoMovimientosDeUnDocumento(idDocLocal) > 0)
                                {
                                    /** Enviar todos los movimientos de este documento */
                                    if (DocumentModel.updateAEnviadoUnDocumento(1, idDocLocal) > 0)
                                    {
                                        if (DocumentModel.getDocumentType(idDocLocal) == 4)
                                        {
                                            response.value = 40;
                                            response.description = "Enviando documentos...";
                                            response.method = method;
                                            response.envioDeDatos = envioDeDatos;
                                            response.peticiones = peticiones += 1;
                                            response.idDocumento = "";
                                        }
                                        else
                                        {
                                            response.value = 40;
                                            response.description = "Enviando documentos...";
                                            response.method = method;
                                            response.envioDeDatos = envioDeDatos;
                                            response.peticiones = peticiones;
                                            response.idDocumento = idDocLocal + "-" + idDocPanel;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                response.value = 100;
                                response.description = "Respuesta Incorrecta!";
                                response.method = method;
                                response.envioDeDatos = envioDeDatos;
                                response.peticiones = peticiones;
                                response.idDocumento = "";
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        SECUDOC.writeLog("Exception: " + e.ToString());
                        response.value = -1;
                        response.description = e.Message+", Enviando Documentos!";
                        response.method = method;
                        response.envioDeDatos = envioDeDatos;
                        response.peticiones = peticiones;
                        response.idDocumento = "";
                    }
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    response.value = -1;
                    response.description = e.Message + ", Enviando Documentos!";
                    response.method = method;
                    response.envioDeDatos = envioDeDatos;
                    response.peticiones = peticiones;
                    response.idDocumento = "";
                }
            });
            
            return response;
        }

        private class ResponseJSONDocs
        {
            public int value { get; set; }
            public String description { get; set; }
            public int idDocumento { get; set; }
            public int iddev { get; set; }
            public List<ResponseFcDocuments> formasDePagoList { get; set; }
        }

        private class ResponseFcDocuments
        {
            public int idFcDocApp { get; set; }
            public int idFcDocServer { get; set; }
        }

        private async Task<ExpandoObject> sendDataToTheServer(int method, int idDocLocal, int envioDeDatos, int idDocumento, int idPosition,
            DocumentModel dvm, WeightModel wm, List<MovimientosModel> movementsList, List<FormasDeCobroDocumentoModel> fcDocumentoList,
            String jsonToSend)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                //final String savedData = dataJSON;
                String ip = ConfiguracionModel.getLinkWs();
                String url = ip;
                url = url.Replace(" ", "%20");
                var client = new RestClient(url);
                var request = new RestRequest("/insertarDocumentosJSON", Method.Post);
                request.AddJsonBody(jsonToSend);
                var responseHeader = await client.ExecuteAsync(request);
                if (responseHeader.ResponseStatus == ResponseStatus.Completed)
                {
                    var content = responseHeader.Content; // Raw content as string
                    ResponseJSONDocs responseDocument = JsonConvert.DeserializeObject<ResponseJSONDocs>(content);
                    try
                    {
                        if (responseDocument.value == 1)
                        {
                            int idDocumentoPanel = responseDocument.idDocumento;
                            List<ResponseFcDocuments> jsonArrayFcDocs = responseDocument.formasDePagoList;
                            if (jsonArrayFcDocs != null)
                            {
                                for (int j = 0; j < jsonArrayFcDocs.Count; j++)
                                {
                                    ResponseFcDocuments objectFcDoc = jsonArrayFcDocs[j];
                                    int idFcDocApp = objectFcDoc.idFcDocApp;
                                    int idFcDocServer = objectFcDoc.idFcDocServer;
                                    FormasDeCobroDocumentoModel.updateIdServerInAFcDocument(idFcDocApp, idFcDocServer);
                                }
                            }
                            if (idDocumentoPanel > 0)
                            {
                                ClsPositionsModel.updateIdDoctoPanelPosition(idPosition, idDocumentoPanel);
                                if (DocumentModel.addIdWebServiceToTheDocument(idDocLocal, idDocumentoPanel) > 0)
                                {
                                    if (MovimientosModel.updateAEnviadoMovimientosDeUnDocumento(idDocLocal) > 0)
                                    {
                                        /** Enviar todos los movimientos de este documento */
                                        if (DocumentModel.updateAEnviadoUnDocumento(1, idDocLocal) > 0)
                                        {
                                            if (DocumentModel.getDocumentType(idDocLocal) == 4)
                                            {
                                                response.value = 40;
                                                response.description = "Enviando documentos...";
                                                response.method = method;
                                                response.envioDeDatos = envioDeDatos;
                                                response.peticiones = peticiones += 1;
                                                response.idDocumento = "";
                                            }
                                            else
                                            {
                                                response.value = 40;
                                                response.description = "Enviando documentos...";
                                                response.method = method;
                                                response.envioDeDatos = envioDeDatos;
                                                response.peticiones = peticiones;
                                                response.idDocumento = idDocLocal + "-" + idDocumentoPanel;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (responseDocument.description.Equals("Documento guardado anteriormente"))
                                {
                                    if (MovimientosModel.updateAEnviadoMovimientosDeUnDocumento(idDocLocal) > 0)
                                    {
                                        /** Enviar todos los movimientos de este documento */
                                        if (DocumentModel.updateAEnviadoUnDocumento(1, idDocLocal) > 0)
                                        {
                                            if (DocumentModel.getDocumentType(idDocLocal) == 4)
                                            {
                                                response.value = 40;
                                                response.description = "Enviando documentos...";
                                                response.method = method + 1;
                                                response.envioDeDatos = envioDeDatos;
                                                response.peticiones = peticiones += 1;
                                                response.idDocumento = "";
                                            }
                                            else
                                            {
                                                response.value = 40;
                                                response.description = "Enviando documentos...";
                                                response.method = method;
                                                response.envioDeDatos = envioDeDatos;
                                                response.peticiones = peticiones;
                                                response.idDocumento = idDocLocal + "-" + idDocumentoPanel;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    response.value = 100;
                                    response.description = "Respuesta Incorrecta!";
                                    response.method = method;
                                    response.envioDeDatos = envioDeDatos;
                                    response.peticiones = peticiones;
                                    response.idDocumento = "";
                                }
                            }
                        } else
                        {
                            response.value = responseDocument.value;
                            response.description = responseDocument.description;
                            response.method = method;
                            response.envioDeDatos = envioDeDatos;
                            response.peticiones = peticiones;
                            response.idDocumento = "";
                        }
                    }
                    catch (Exception e)
                    {
                        SECUDOC.writeLog(e.ToString());
                        response.value = -1;
                        response.description = e.Message+", Enviando Documentos!";
                        response.method = method;
                        response.envioDeDatos = envioDeDatos;
                        response.peticiones = peticiones;
                        response.idDocumento = "";
                    }
                }
                else if (responseHeader.ResponseStatus == ResponseStatus.Error)
                {
                    response.value = -500;
                    response.description = MetodosGenerales.getErrorMessageFromNetworkCode(-500,
                        responseHeader.ErrorException.Message);
                    response.method = 3;
                    response.envioDeDatos = envioDeDatos;
                    response.peticiones = peticiones;
                    response.idDocumento = "";
                } else
                {
                    response.value = -404;
                    response.description = MetodosGenerales.getErrorMessageFromNetworkCode(-404,
                        responseHeader.ErrorException.Message);
                    response.method = 3;
                    response.envioDeDatos = envioDeDatos;
                    response.peticiones = peticiones;
                    response.idDocumento = "";
                }
            });
            
            return response;
        }

        private async Task<ExpandoObject> enviarUbicacionesAPI(int method, int envioDeDatos, int peticiones)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                List<ClsPositionsModel> posicionesNoEnviadas = ClsPositionsModel.getAllNotSendPositions(0);
                if (posicionesNoEnviadas != null)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        response = await sendLocationToTheWs(posicionesNoEnviadas[i].id, posicionesNoEnviadas[i].latitude,
                                posicionesNoEnviadas[i].longitude, posicionesNoEnviadas[i].customerId,
                                posicionesNoEnviadas[i].date, posicionesNoEnviadas[i].documentType,
                                posicionesNoEnviadas[i].agentId, posicionesNoEnviadas[i].route,
                                posicionesNoEnviadas[i].idDoctoPanel, method, envioDeDatos, peticiones);
                    }
                }
                else
                {
                    response.value = 75;
                    response.description = "Proceso finalizado";
                    response.method = 4;
                    response.envioDeDatos = envioDeDatos;
                    response.peticiones = 0;
                    response.idDocumento = "";
                }
            });
            
            return response;
        }

        private async Task<ExpandoObject> enviarUbicacionesLAN(int method, int envioDeDatos, int peticiones)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                List<ClsPositionsModel> posicionesNoEnviadas = ClsPositionsModel.getAllNotSendPositions(0);
                if (posicionesNoEnviadas != null)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        response = await sendLocationLAN(posicionesNoEnviadas[i].id, posicionesNoEnviadas[i].latitude,
                                posicionesNoEnviadas[i].longitude, posicionesNoEnviadas[i].customerId,
                                posicionesNoEnviadas[i].date, posicionesNoEnviadas[i].documentType,
                                posicionesNoEnviadas[i].agentId, posicionesNoEnviadas[i].route,
                                posicionesNoEnviadas[i].idDoctoPanel, method, envioDeDatos, peticiones);
                    }
                }
                else
                {
                    response.value = 75;
                    response.description = "Proceso finalizado";
                    response.method = 4;
                    response.envioDeDatos = envioDeDatos;
                    response.peticiones = 0;
                    response.idDocumento = "";
                }
            });
            
            return response;
        }

        public async Task<ExpandoObject> sendLocationToTheWs(int id, double latitude, double longitude, int idCliente, String fh, int tipoDoc,
                                    int idAgente, String ruta, int idDocto, int method, int envioDeDatos, int peticiones)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                String ip = ConfiguracionModel.getLinkWs();
                String url = ip + "/insertar_gps?LAT=" + latitude + "&LON=" + longitude + "&CLIENTE_ID=" + idCliente + "&FECHA=" + fh + "&TIPO_MOV=" + tipoDoc + "&VENDEDOR_ID=" + idAgente +
                        "&RUTA=" + ruta + "&idDocto=" + idDocto;
                url = url.Replace(" ", "%20");
                try
                {
                    var client = new RestClient(url);
                    // client.Authenticator = new HttpBasicAuthenticator(username, password);
                    var request = new RestRequest();
                    var responseHeader = client.GetAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content; // Raw content as string
                                                                     //var responseHttp = client.GetAsync<String>(request);
                        var jsonResp = JsonConvert.DeserializeObject<ResponseCustomer>(content);
                        ResponseCustomer jsonUsers = (ResponseCustomer)jsonResp;
                        try
                        {
                            if (ClsPositionsModel.updatePosicionEnviada(id, 0) > 0)
                            {
                                response.value = 75;
                                response.description = "Ubicaciones enviadas";
                                response.method = method;
                                response.envioDeDatos = envioDeDatos;
                                response.peticiones = 0;
                                response.idDocumento = "";
                            }
                        }
                        catch (Exception e)
                        {
                            SECUDOC.writeLog("Exception: " + e.Message);
                            response.value = -1;
                            response.description = e.Message+", Enviando Ubicaciones!";
                            response.method = method;
                            response.envioDeDatos = envioDeDatos;
                            response.peticiones = 0;
                            response.idDocumento = "";
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        response.value = -404;
                        response.description = MetodosGenerales.getErrorMessageFromNetworkCode(-404,
                            responseHeader.Result.ErrorException.Message);
                        response.method = method;
                        response.envioDeDatos = envioDeDatos;
                        response.peticiones = 0;
                        response.idDocumento = "";
                    } else
                    {
                        response.value = -500;
                        response.description = MetodosGenerales.getErrorMessageFromNetworkCode(-500,
                            responseHeader.Result.ErrorException.Message);
                        response.method = method;
                        response.envioDeDatos = envioDeDatos;
                        response.peticiones = 0;
                        response.idDocumento = "";
                    }
                }
                catch (Exception ex)
                {
                    SECUDOC.writeLog("Exception: " + ex.Message);
                    response.value = -1;
                    response.description = ex.Message+", Enviando Ubicaciones!";
                    response.method = method;
                    response.envioDeDatos = envioDeDatos;
                    response.peticiones = 0;
                    response.idDocumento = "";
                }
            });
            
            return response;
        }

        public async Task<ExpandoObject> sendLocationLAN(int id, double latitude, double longitude, int idCliente, String fh, int tipoDoc,
                                    int idAgente, String ruta, int idDocto, int method, int envioDeDatos, int peticiones)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                ClsGpsModel GPS = new ClsGpsModel();
                GPS.customerId = idCliente;
                GPS.agentId = idAgente;
                GPS.route = ruta;
                GPS.latitude = Convert.ToDouble(latitude);
                GPS.longitude = Convert.ToDouble(longitude);
                GPS.date = fh;
                //DateTime aux = Convert.ToDateTime(FECHA);
                //GPS.FECHA = aux.ToString("yyyy-dd-MM HH:mm:ss");
                GPS.documentType = tipoDoc;
                GPS.documentId = idDocto;
                int idLocation = GPS.InsertarUbicacion(panelInstance);
                try
                {
                    if (ClsPositionsModel.updatePosicionEnviada(id, idLocation) > 0)
                    {
                        response.value = 75;
                        response.description = "Ubicaciones enviadas";
                        response.method = method;
                        response.envioDeDatos = envioDeDatos;
                        response.peticiones = 0;
                        response.idDocumento = "";
                    }
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog("Exception: " + e.ToString());
                    response.value = -1;
                    response.description = e.Message+", Enviando Ubicaciones!";
                    response.method = method;
                    response.envioDeDatos = envioDeDatos;
                    response.peticiones = 0;
                    response.idDocumento = "";
                }
            });
           
            return response;
        }

        private async Task<ExpandoObject> enviarCuentasPorCobrarAPI(int method, int envioDeDatos, int peticiones)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                ClsPositionsModel pm = null;
                List<CuentasXCobrarModel> repNotSended = CuentasXCobrarModel.getAllNotSendCxc(0);
                if (repNotSended != null)
                {
                    String jsonRep = "";
                    int repIdApp = 0;
                    int locationIdApp = 0;
                    UserModel user = UserModel.getNameIdAndRutaOfTheUser();
                    for (int i = 0; i < 1; i++)
                    {
                        repIdApp = repNotSended[i].id;
                        String customerCode = CustomerModel.getClaveForAClient(repNotSended[i].customerId);
                        jsonRep += "{\n" +
                                " \"creditDocumentFolio\":\"" + repNotSended[i].creditDocumentFolio + "\",\n" +
                                " \"customerCode\":\"" + repNotSended[i].customerCode + "\",\n" +
                                " \"customerId\":" + repNotSended[i].customerId + ",\n" +
                                " \"amount\":" + repNotSended[i].amount + ",\n" +
                                " \"creditDocumentId\":" + repNotSended[i].creditDocumentId + ",\n" +
                                " \"nameUser\":\"" + repNotSended[i].nameUser + "\",\n" +
                                " \"formOfPaymentIdCredit\":" + repNotSended[i].formOfPaymentIdCredit + ",\n" +
                                " \"reference\":\"" + repNotSended[i].reference + "\",\n" +
                                " \"bankId\":" + repNotSended[i].bankId + ",\n" +
                                " \"date\":\"" + repNotSended[i].date + "\",\n" +
                                " \"documentFolio\":\"" + repNotSended[i].documentFolio + "\",\n" +
                                " \"status\":" + repNotSended[i].status + ",\n" +
                                " \"userId\":" + repNotSended[i].userId + ",\n";
                        pm = ClsPositionsModel.getAllDataForAPositionIdApp(repNotSended[i].id, 9);
                        if (pm != null)
                        {
                            locationIdApp = pm.id;
                            jsonRep += " \"gpsData\":{\n" +
                                    "  \"customerId\":" + pm.customerId + ",\n" +
                                    "  \"agentId\":" + pm.agentId + ",\n" +
                                    "  \"documentType\":" + pm.documentType + ",\n" +
                                    "  \"route\":\"" + pm.route + "\",\n" +
                                    "  \"date\":\"" + pm.date + "\",\n" +
                                    "  \"latitude\":" + pm.latitude + ",\n" +
                                    "  \"longitude\":" + pm.longitude + "\n" +
                                    "}";
                        }
                        else
                        {
                            jsonRep += " \"gpsData\":{\n" +
                                    "}";
                        }
                        jsonRep += "\n}";
                    }
                    try
                    {
                        response = await sendRepsToTheServer(repIdApp, locationIdApp, jsonRep, method, envioDeDatos, repNotSended[0],
                            pm);
                    }
                    catch (Exception e)
                    {
                        SECUDOC.writeLog("Exception: " + e.ToString());
                        response.value = -1;
                        response.description = e.Message+", Enviando Cuentas por Cobrar!";
                        response.method = 5;
                        response.envioDeDatos = envioDeDatos;
                        response.peticiones = peticiones;
                        response.idDocumento = "";
                    }
                }
                else
                {
                    response.value = 95;
                    response.description = "Proceso Finalizado";
                    response.method = 5;
                    response.envioDeDatos = envioDeDatos;
                    response.peticiones = peticiones;
                    response.idDocumento = "";
                }
            });
            
            return response;
        }

        private async Task<ExpandoObject> enviarCuentasPorCobrarLAN(int method, int envioDeDatos, int peticiones)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                ClsAccountsReceivableModel ar = null;
                List<CuentasXCobrarModel> repNotSended = CuentasXCobrarModel.getAllNotSendCxc(0);
                if (repNotSended != null)
                {
                    int repIdApp = 0;
                    int locationIdApp = 0;
                    UserModel user = UserModel.getNameIdAndRutaOfTheUser();
                    for (int i = 0; i < 1; i++)
                    {
                        repIdApp = repNotSended[i].id;
                        String customerCode = CustomerModel.getClaveForAClient(repNotSended[i].customerId);
                        ar = new ClsAccountsReceivableModel();
                        ar.customerCode = customerCode;
                        ar.customerId = repNotSended[i].customerId;
                        ar.userId = user.id;
                        ar.nameUser = user.Nombre;
                        ar.date = repNotSended[i].date;
                        ar.creditDocumentId = repNotSended[i].creditDocumentId;
                        ar.creditDocumentFolio = repNotSended[i].creditDocumentFolio;
                        ar.formOfPaymentIdCredit = repNotSended[i].formOfPaymentIdCredit;
                        ar.amount = repNotSended[i].amount;
                        ar.reference = repNotSended[i].reference;
                        ar.bankId = 0;
                        ar.documentFolio = repNotSended[i].documentFolio;
                        ar.status = 0;
                        ClsGpsModel gpsDataModel = null;
                        ClsPositionsModel pm = ClsPositionsModel.getAllDataForAPositionIdApp(repNotSended[i].id, 9);
                        if (pm != null)
                        {
                            gpsDataModel = new ClsGpsModel();
                            locationIdApp = pm.id;
                            gpsDataModel.customerId = pm.customerId;
                            gpsDataModel.agentId = pm.agentId;
                            gpsDataModel.documentType = pm.documentType;
                            gpsDataModel.route = pm.route;
                            gpsDataModel.date = pm.date;
                            gpsDataModel.latitude = pm.latitude;
                            gpsDataModel.longitude = pm.longitude;
                        }
                        else
                        {
                            gpsDataModel = new ClsGpsModel();
                            gpsDataModel.customerId = 0;
                            gpsDataModel.agentId = 0;
                            gpsDataModel.documentType = 0;
                            gpsDataModel.route = "";
                            gpsDataModel.date = "";
                            gpsDataModel.latitude = 0;
                            gpsDataModel.longitude = 0;
                        }
                        ar.gpsData = gpsDataModel;
                    }
                    try
                    {
                        response = await sendRepsToTheServerLAN(repIdApp, locationIdApp, ar, method, envioDeDatos);
                    }
                    catch (Exception e)
                    {
                        SECUDOC.writeLog("Exception: " + e.ToString());
                        response.value = -1;
                        response.description = e.Message+", Enviando Pagos!";
                        response.method = 5;
                        response.envioDeDatos = envioDeDatos;
                        response.peticiones = peticiones;
                        response.idDocumento = "";
                    }
                }
                else
                {
                    response.value = 95;
                    response.description = "Proceso Finalizado";
                    response.method = 5;
                    response.envioDeDatos = envioDeDatos;
                    response.peticiones = peticiones;
                    response.idDocumento = "";
                }
            });
            
            return response;
        }

        private class CxcResponse
        {
            public int repId { get; set; }
            public int locationId { get; set; }
            public String descripcion { get; set; }
        }

        private class ResponseCxc
        {
            public List<CxcResponse> response { get; set; }
        }

        private async Task<ExpandoObject> sendRepsToTheServer(int repIdApp, int idLocationApp, String savedData, int method, int envioDeDatos,
            CuentasXCobrarModel repNotSended, ClsPositionsModel pm)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () => {
                try
                {
                    String ip = ConfiguracionModel.getLinkWs();
                    // Se define el URL de la web service
                    String url = ip;
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/saveAccountsReceivable", Method.Post);
                    //request.AddParameter("application/json", savedData, ParameterType.RequestBody);
                    request.AddJsonBody(new
                    {
                        creditDocumentFolio = repNotSended.creditDocumentFolio,
                        customerCode = repNotSended.customerCode,
                        customerId = repNotSended.customerId,
                        amount = repNotSended.amount,
                        creditDocumentId = repNotSended.creditDocumentId,
                        nameUser = repNotSended.nameUser,
                        formOfPaymentIdCredit = repNotSended.formOfPaymentIdCredit,
                        reference = repNotSended.reference,
                        bankId = repNotSended.bankId,
                        date = repNotSended.date,
                        documentFolio = repNotSended.documentFolio,
                        status = repNotSended.status,
                        userId = repNotSended.userId,
                        gpsData = pm
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content; // Raw content as string
                        var jsonResp = JsonConvert.DeserializeObject<ResponseCxc>(content);
                        ResponseCxc jsonUsers = (ResponseCxc)jsonResp;
                        try
                        {
                            for (int i = 0; i < jsonUsers.response.Count; i++)
                            {
                                CxcResponse resp = jsonUsers.response[i];
                                int repId = resp.repId;
                                int locationId = resp.locationId;
                                String description = resp.descripcion;
                                if (repId > 0)
                                {
                                    if (CuentasXCobrarModel.updateAEnviadoUnaCxc(repIdApp, repId))
                                    {
                                        //ClsPositionsModel.updatePosicionEnviada(idLocationApp, repId);
                                        response.value = 95;
                                        response.description = description;
                                        response.method = method;
                                        response.envioDeDatos = envioDeDatos;
                                        response.peticiones = peticiones;
                                        response.idDocumento = "";
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            SECUDOC.writeLog( e.ToString());
                            response.value = -1;
                            response.description = e.Message+", Enviando Pagos!";
                            response.method = method;
                            response.envioDeDatos = envioDeDatos;
                            response.peticiones = 0;
                            response.idDocumento = "";
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        response.value = -404;
                        response.description = MetodosGenerales.getErrorMessageFromNetworkCode(-404,
                            responseHeader.Result.ErrorException.Message);
                        response.method = method;
                        response.envioDeDatos = envioDeDatos;
                        response.peticiones = 0;
                        response.idDocumento = "";
                    } else
                    {
                        response.value = -500;
                        response.description = MetodosGenerales.getErrorMessageFromNetworkCode(-500,
                            responseHeader.Result.ErrorException.Message);
                        response.method = method;
                        response.envioDeDatos = envioDeDatos;
                        response.peticiones = 0;
                        response.idDocumento = "";
                    }
                }
                catch (Exception ex)
                {
                    SECUDOC.writeLog("Exception: " + ex.ToString());
                    response.value = -1;
                    response.description = ex.Message+", Enviando Pagos!";
                    response.method = method;
                    response.envioDeDatos = envioDeDatos;
                    response.peticiones = 0;
                    response.idDocumento = "";
                }
            });
            
            return response;
        }

        private async Task<ExpandoObject> sendRepsToTheServerLAN(int repIdApp, int idLocationApp, ClsAccountsReceivableModel ar, int method, int envioDeDatos)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                dynamic myObjectResponse = ClsAccountsReceivableModel.insertDocument(panelInstance, ar.customerCode, ar.customerId, ar.userId, ar.nameUser, ar.date,
                        ar.creditDocumentId, ar.creditDocumentFolio, ar.formOfPaymentIdCredit, ar.amount, ar.reference, ar.bankId, ar.documentFolio, ar.status,
                        ar.gpsData);
                try
                {
                    int repId = myObjectResponse.repId;
                    int locationId = myObjectResponse.locationId;
                    String description = myObjectResponse.descripcion;
                    if (repId > 0)
                    {
                        if (CuentasXCobrarModel.updateAEnviadoUnaCxc(repIdApp, repId))
                        {
                            //ClsPositionsModel.updatePosicionEnviada(idLocationApp, repId);
                            response.value = 95;
                            response.description = description;
                            response.method = method;
                            response.envioDeDatos = envioDeDatos;
                            response.peticiones = peticiones;
                            response.idDocumento = "";
                        }
                    }
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    response.value = -1;
                    response.description = e.Message+", Enviando Pagos!";
                    response.method = method;
                    response.envioDeDatos = envioDeDatos;
                    response.peticiones = 0;
                    response.idDocumento = "";
                }
            });
            
            return response;
        }

        private class RetiroRequest
        {
            public List<RetiroModel> retiros { get; set; }
        }

        private async Task<ExpandoObject> enviarRetirosAPI(int method, int envioDeDatos, int peticiones)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                String jsonRetiros = "";
                List<MontoRetiroModel> montosList = null;
                List<RetiroModel> retirosList = RetiroModel.getAllWithdrawalsNotSentToTheServer();
                if (retirosList != null)
                {
                    jsonRetiros += "{\n \"retiros\": [\n";
                    int itemsWithdrawal = retirosList.Count;
                    int count = 0;
                    for (int i = 0; i < itemsWithdrawal; i++)
                    {
                        if (MontoRetiroModel.checkForWithdrawalAmounts(retirosList[i].id) > 0)
                        {
                            /*jsonRetiros += "{\n" +
                                    " \"id\":" + retirosList[i].id + ",\n" +
                                    " \"number\":" + retirosList[i].number + ",\n" +
                                    " \"idUsuario\":" + retirosList[i].idUsuario + ",\n" +
                                    " \"claveUsuario\":\"" + retirosList[i].claveUsuario + "\",\n" +
                                    " \"fechaHora\":\"" + retirosList[i].fechaHora + "\",\n" +
                                    " \"concept\":\"" + retirosList[i].concept + "\",\n" +
                                    " \"description\":\"" + retirosList[i].description + "\",\n" +
                                    " \"montos\":[\n";*/
                            String jsonMontos = "";
                            montosList = MontoRetiroModel.getAllAmountsFromAWithdrawal(retirosList[i].id);
                            if (montosList != null)
                            {
                                retirosList[i].montos = montosList;
                                /*for (int j = 0; j < montosList.Count; j++)
                                {
                                    jsonMontos += "{\n" +
                                            " \"id\":" + montosList[j].id + ",\n" +
                                            " \"formaCobroId\":" + montosList[j].formaCobroId + ",\n" +
                                            " \"importe\":" + montosList[j].importe + "\n";
                                    if (j == (montosList.Count - 1))
                                        jsonMontos += "}\n]";
                                    else jsonMontos += "},\n";
                                }*/
                            }
                            else
                            {
                                jsonMontos += "]";
                            }
                            if (i == (retirosList.Count - 1))
                                jsonRetiros += jsonMontos + "\n}\n";
                            else jsonRetiros += jsonMontos + "\n},\n";
                        }
                        else
                        {
                            count++;
                        }
                    }
                    jsonRetiros += "]\n}";
                    if (count != itemsWithdrawal)
                    {
                        try
                        {
                            response = await sendRetirosToWs(method, envioDeDatos, retirosList);
                        }
                        catch (Exception ex)
                        {
                            SECUDOC.writeLog("Exception: " + ex.ToString());
                            response.value = -1;
                            response.description = ex.Message+", Enviando Retiros!";
                            response.method = 6;
                            response.envioDeDatos = envioDeDatos;
                            response.peticiones = peticiones;
                            response.idDocumento = "";
                        }
                    }
                    else
                    {
                        response.value = 100;
                        response.description = "Proceso Finalizado";
                        response.method = 6;
                        response.envioDeDatos = envioDeDatos;
                        response.peticiones = peticiones;
                        response.idDocumento = "";
                    }
                }
                else
                {
                    response.value = 95;
                    response.description = "Proceso Finalizado";
                    response.method = 6;
                    response.envioDeDatos = envioDeDatos;
                    response.peticiones = peticiones;
                    response.idDocumento = "";
                }
            });
            
            return response;
        }

        private async Task<ExpandoObject> enviarRetirosLAN(int method, int envioDeDatos, int peticiones)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                List<ClsRetirosModel> retirosListToSend = null;
                ClsRetirosModel retiroToSend = null;
                List<RetiroModel> retirosList = RetiroModel.getAllWithdrawalsNotSentToTheServer();
                if (retirosList != null)
                {
                    retirosListToSend = new List<ClsRetirosModel>();
                    int itemsWithdrawal = retirosList.Count;
                    int count = 0;
                    for (int i = 0; i < itemsWithdrawal; i++)
                    {
                        retiroToSend = new ClsRetirosModel();
                        if (MontoRetiroModel.checkForWithdrawalAmounts(retirosList[i].id) > 0)
                        {
                            retiroToSend.id = retirosList[i].id;
                            retiroToSend.number = retirosList[i].number;
                            retiroToSend.idUsuario = retirosList[i].idUsuario;
                            retiroToSend.claveUsuario = retirosList[i].claveUsuario;
                            retiroToSend.fechaHora = retirosList[i].fechaHora;
                            retiroToSend.concept = retirosList[i].concept;
                            retiroToSend.description = retirosList[i].description;
                            List<ClsMontosRetirosModel> montosToSend = null;
                            ClsMontosRetirosModel montoToSend = null;
                            List<MontoRetiroModel> montosList = MontoRetiroModel.getAllAmountsFromAWithdrawal(retirosList[i].id);
                            if (montosList != null)
                            {
                                montosToSend = new List<ClsMontosRetirosModel>();
                                for (int j = 0; j < montosList.Count; j++)
                                {
                                    montoToSend = new ClsMontosRetirosModel();
                                    montoToSend.id = montosList[j].id;
                                    montoToSend.formaCobroId = montosList[j].formaCobroId;
                                    montoToSend.importe = montosList[j].importe;
                                    if (montosList[j].createdAt != null && !montosList[j].createdAt.Equals(""))
                                        montoToSend.createdAt = Convert.ToDateTime(montosList[j].createdAt);
                                    if (montosList[j].updatedAt != null && !montosList[j].updatedAt.Equals(""))
                                        montoToSend.updatedAt = Convert.ToDateTime(montosList[j].updatedAt);
                                    montosToSend.Add(montoToSend);
                                }
                            }
                            else
                            {
                                montosToSend = new List<ClsMontosRetirosModel>();
                            }
                            retiroToSend.montos = montosToSend;
                        }
                        else
                        {
                            count++;
                        }
                    }
                    retirosListToSend.Add(retiroToSend);
                    if (count != itemsWithdrawal)
                    {
                        try
                        {
                            response = await sendRetirosLAN(method, retirosListToSend, envioDeDatos);
                        }
                        catch (Exception ex)
                        {
                            SECUDOC.writeLog("Exception: " + ex.ToString());
                            response.value = -1;
                            response.description = ex.Message+", Enviando Retiros!";
                            response.method = method;
                            response.envioDeDatos = envioDeDatos;
                            response.peticiones = peticiones;
                            response.idDocumento = "";
                        }
                    }
                    else
                    {
                        response.value = 95;
                        response.description = "Proceso Finalizado";
                        response.method = 6;
                        response.envioDeDatos = envioDeDatos;
                        response.peticiones = peticiones;
                        response.idDocumento = "";
                    }
                }
                else
                {
                    response.value = 95;
                    response.description = "Proceso Finalizado";
                    response.method = 6;
                    response.envioDeDatos = envioDeDatos;
                    response.peticiones = peticiones;
                    response.idDocumento = "";
                }
            });
            
            return response;
        }

        private class RetiroResponse
        {
            public int idRetiroApp { get; set; }
            public int idRetiroServer { get; set; }
        }

        private class ResponseRetiro
        {
            public List<RetiroResponse> response { get; set; }
        }

        private async Task<ExpandoObject> sendRetirosToWs(int method, int envioDeDatos, List<RetiroModel> retirosList)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () => {
                try
                {
                    String ip = ConfiguracionModel.getLinkWs();
                    String url = ip;
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/insertBoxWithdrawals", Method.Post);
                    request.AddJsonBody(new
                    {
                        retiros = retirosList
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var jsonResp = JsonConvert.DeserializeObject<ResponseRetiro>(responseHeader.Result.Content);
                        ResponseRetiro jsonRetiro = (ResponseRetiro)jsonResp;
                        try
                        {
                            if (jsonRetiro != null)
                            {
                                for (int i = 0; i < jsonRetiro.response.Count; i++)
                                {
                                    RetiroResponse object1 = jsonRetiro.response[i];
                                    int idRetiroApp = object1.idRetiroApp;
                                    int idRetiroServer = object1.idRetiroServer;
                                    RetiroModel.updateServerIdInAWithdrawal(idRetiroApp, idRetiroServer);
                                    MontoRetiroModel.updateSentFieldOfWithdrawalAmounts(idRetiroApp);
                                }
                                response.value = 100;
                                response.description = "Proceso Finalizado";
                                response.method = 6;
                                response.envioDeDatos = envioDeDatos;
                                response.peticiones = peticiones;
                                response.idDocumento = "";
                            }
                            else
                            {
                                response.value = 100;
                                response.description = "Respuesta Incorrecta!";
                                response.method = method;
                                response.envioDeDatos = envioDeDatos;
                                response.peticiones = peticiones;
                                response.idDocumento = "";
                            }
                        }
                        catch (Exception e)
                        {
                            SECUDOC.writeLog("Exception: " + e.ToString());
                            response.value = -1;
                            response.description = e.Message+", Enviando Retiros!";
                            response.method = method;
                            response.envioDeDatos = envioDeDatos;
                            response.peticiones = peticiones;
                            response.idDocumento = "";
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        response.value = -404;
                        MetodosGenerales.getErrorMessageFromNetworkCode(-404,
                        responseHeader.Result.ErrorException.Message + ", Enviando Retiros!");
                        response.method = method;
                        response.envioDeDatos = envioDeDatos;
                        response.peticiones = 0;
                        response.idDocumento = "";
                    } else
                    {
                        response.value = -500;
                        response.description = MetodosGenerales.getErrorMessageFromNetworkCode(-500,
                            responseHeader.Result.ErrorException.Message+", Enviando Retiros!");
                        response.method = method;
                        response.envioDeDatos = envioDeDatos;
                        response.peticiones = 0;
                        response.idDocumento = "";
                    }
                }
                catch (Exception ex)
                {
                    SECUDOC.writeLog(ex.ToString());
                    response.value = -1;
                    response.description = ex.Message+", Enviando Retiros!";
                    response.method = method;
                    response.envioDeDatos = envioDeDatos;
                    response.peticiones = peticiones;
                    response.idDocumento = "";
                }
            });
            return response;
        }

        private async Task<ExpandoObject> sendRetirosLAN(int method, List<ClsRetirosModel> retirosList, int envioDeDatos)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                List<ExpandoObject> responseRetiros = ClsRetirosModel.saveWithdrawalsAlongWithTheirAmounts(panelInstance, retirosList);
                try
                {
                    if (responseRetiros != null)
                    {
                        for (int i = 0; i < responseRetiros.Count; i++)
                        {
                            dynamic object1 = responseRetiros[i];
                            int idRetiroApp = object1.idRetiroApp;
                            int idRetiroServer = object1.idRetiroServer;
                            RetiroModel.updateServerIdInAWithdrawal(idRetiroApp, idRetiroServer);
                            MontoRetiroModel.updateSentFieldOfWithdrawalAmounts(idRetiroApp);
                        }
                        response.value = 95;
                        response.description = "Proceso Finalizado";
                        response.method = 6;
                        response.envioDeDatos = envioDeDatos;
                        response.peticiones = peticiones;
                        response.idDocumento = "";
                    }
                    else
                    {
                        response.value = 95;
                        response.description = "Respuesta Incorrecta!";
                        response.method = method;
                        response.envioDeDatos = envioDeDatos;
                        response.peticiones = peticiones;
                        response.idDocumento = "";
                    }
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog("Exception: " + e.ToString());
                    response.value = -1;
                    response.description = e.Message+", Enviando Retiros!";
                    response.method = method;
                    response.envioDeDatos = envioDeDatos;
                    response.peticiones = peticiones;
                    response.idDocumento = "";
                }
            });
            
            return response;
        }

        private async Task<ExpandoObject> enviarIngresosAPI(int method, int envioDeDatos, int peticiones)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                String idDocumento = "";
                try
                {
                    List<int> idsIngresos = IngresoModel.getAllIdsEntriesNotSends();
                    if (idsIngresos != null && idsIngresos.Count > 0)
                    {
                        int count = 0;
                        foreach (int idIngreso in idsIngresos)
                        {
                            SendIngresoController sim = new SendIngresoController();
                            dynamic resp = await sim.handleActionSendIngresosAPI(idIngreso);
                            if (resp.value == 100 || resp.value == 99)
                            {
                                count++;
                            }
                        }
                        if (idsIngresos.Count == count)
                        {
                            value = 99;
                            description = "Proceso Finalizado";
                            method = 7;
                            idDocumento = "";
                        }
                        else
                        {
                            value = 99;
                            description = "Proceso Finalizado";
                            method = 7;
                            idDocumento = "";
                        }
                    }
                    else
                    {
                        value = 99;
                        description = "Todos los registros enviados!";
                        method = 7;
                        idDocumento = "";
                    }
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    value = -1;
                    description = e.Message+", Enviando Ingresos!";
                    method = 7;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                    response.method = method;
                    response.envioDeDatos = envioDeDatos;
                    response.peticiones = peticiones;
                    response.idDocumento = idDocumento;
                }
            });            
            return response;
        }

        private async Task<ExpandoObject> enviarTicketsAPI(int method, int envioDeDatos, int peticiones)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                String idDocumento = "";
                try
                {
                    List<int> idsTickets = TicketsModel.getAllIdsTicketsNotSends();
                    if (idsTickets != null && idsTickets.Count > 0)
                    {
                        int count = 0;
                        foreach (int idsTicket in idsTickets)
                        {
                            SendTicketsController sim = new SendTicketsController();
                            dynamic resp = await sim.handleActionSendIngresosAPI(idsTicket);
                            if (resp.value == 100)
                            {
                                count++;
                            }
                        }
                        if (idsTickets.Count == count)
                        {
                            value = 100;
                            description = "Proceso Finalizado";
                            method = 8;
                            idDocumento = "";
                        }
                        else
                        {
                            value = 100;
                            description = "Proceso Finalizado";
                            method = 8;
                            idDocumento = "";
                        }
                    }
                    else
                    {
                        value = 100;
                        description = "Todos los registros enviados!";
                        method = 8;
                        idDocumento = "";
                    }
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    value = -1;
                    description = e.Message + ", Enviando Tickets!";
                    method = 8;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                    response.method = method;
                    response.envioDeDatos = envioDeDatos;
                    response.peticiones = peticiones;
                    response.idDocumento = idDocumento;
                }
            });
            return response;
        }

        private async Task<ExpandoObject> enviarIngresosLAN(int method, int envioDeDatos, int peticiones)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                List<int> idsIngresos = IngresoModel.getAllIdsEntriesNotSends();
                if (idsIngresos != null && idsIngresos.Count > 0)
                {
                    int count = 0;
                    foreach (int idIngreso in idsIngresos)
                    {
                        SendIngresoController sim = new SendIngresoController();
                        dynamic resp = await sim.handleActionSendIngresoLAN(idIngreso);
                        if (resp.value == 95)
                        {
                            count++;
                        }
                    }
                    if (idsIngresos.Count == count)
                    {
                        response.value = 95;
                        response.description = "Proceso Finalizado";
                        response.method = 7;
                        response.envioDeDatos = envioDeDatos;
                        response.peticiones = peticiones;
                        response.idDocumento = "";
                    }
                    else
                    {
                        response.value = 95;
                        response.description = "Proceso Finalizado";
                        response.method = 7;
                        response.envioDeDatos = envioDeDatos;
                        response.peticiones = peticiones;
                        response.idDocumento = "";
                    }
                }
                else
                {
                    response.value = 95;
                    response.description = "Todos los registros enviados!";
                    response.method = 7;
                    response.envioDeDatos = envioDeDatos;
                    response.peticiones = peticiones;
                    response.idDocumento = "";
                }
            });
            return response;
        }

        public class responseData {
                public int value;
                public String description;
                public int method;
                public int envioDeDatos;
                public int peticiones;
                public String idDocumento;
         }

    }
}
