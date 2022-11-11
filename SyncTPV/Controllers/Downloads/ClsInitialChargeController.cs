using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using SyncTPV.Views;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using wsROMClase;
using wsROMClases.Models.Commercial;

namespace SyncTPV.Controllers.Downloads
{
    public class ClsInitialChargeController
    {
        public static readonly int INITIAL_CHARGE = 0;
        public static readonly int UPDATE_DATA = 1;

        public static async Task<ExpandoObject> doDownloadProcess(FormPrincipal pp, int downloadType, String codigoCaja)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                int documentosPendientes = 0;
                try
                {
                    if (ConfiguracionModel.isLANPermissionActivated())
                    {
                        dynamic pendingDocuments = await validateDocumentsInTheServerLAN();
                        if (pendingDocuments.value == 0)
                        {
                            await intialDownloadProcess(pp, downloadType, true, codigoCaja);
                        }
                        else
                        {
                            documentosPendientes = pendingDocuments.value;
                            description = pendingDocuments.description;
                        }
                    }
                    else
                    {
                        String urlServer = ConfiguracionModel.getLinkWs();
                        dynamic responsePing = await ConfigurationWsController.doPingWs(urlServer);
                        if (responsePing.value == 1)
                        {
                            dynamic responseDocPendientes = validateDocumentsInTheServer(urlServer);
                            if (responseDocPendientes.value == 0)
                            {
                                await intialDownloadProcess(pp, downloadType, false, codigoCaja);
                            }
                            else
                            {
                                documentosPendientes = responseDocPendientes.value;
                                description = responseDocPendientes.description;
                            }
                        }
                        else
                        {
                            value = responsePing.value;
                            description = responsePing.description;
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
                    response.documentosPendientes = documentosPendientes;
                }
            });
            return response;
        }

        public static async Task doIndividualDownloadProcess(FrmDownloads fd, FormPrincipal frmPrincipalPrueba, int downloadType, int method, int positionTable,
            bool serverModeLAN, String codigoCaja)
        {
            if (method == 1)
            {
                dynamic responseCustomer = null;
                if (serverModeLAN)
                    responseCustomer = await CustomersController.downloadAllCustomersLAN(downloadType, 0);
                else responseCustomer = await CustomersController.downloadAllCustomersAPI(downloadType, 0);
                if (responseCustomer.value == 1)
                {
                    if (serverModeLAN)
                    {
                        await downloadCustomerIdPublicGeneralTpvLAN();
                        await ClsCustomersZonesController.downloadAllCustomersZonesLAN();
                    }
                    else {
                        await ClsCustomersZonesController.downloadAllCustomersZones();
                        await downloadCustomerIdPublicGeneralTpv(); 
                    }
                    if (fd != null)
                        fd.updateUIAftherDownload(1, true, "La actualización de clientes a finalizado", positionTable);
                } else if (responseCustomer.value == -404 && responseCustomer.value == -400)
                {
                    if (fd != null)
                        fd.updateUIAftherDownload(2, true, responseCustomer.description, positionTable);
                } else if (responseCustomer.value == -500)
                {
                    if (fd != null)
                        fd.updateUIAftherDownload(2, true, responseCustomer.description, positionTable);
                }
                else
                {
                    if (fd != null)
                        fd.updateUIAftherDownload(2, true, responseCustomer.description, positionTable);
                }
            }
            else if (method == 2)
            {
                int response = 0;
                String query = "SELECT " + LocalDatabase.CAMPO_SERVERMODE_CONFIG + " FROM " + LocalDatabase.TABLA_CONFIGURACION + " WHERE " +
            LocalDatabase.CAMPO_ID_CONFIGURACION + " = 1";
                int comInstanceCreated = InstanceSQLSEModel.getIntValue(query); ;
                if (serverModeLAN && comInstanceCreated == 1)
                {
                    /*String comInstance = InstanceSQLSEModel.getStringComInstance();
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    int limit = 100000;
                    await ItemsController.getAllItemsFromSqlServerLAN(comInstance, panelInstance, 0, downloadType, limit);*/
                    response = 1;
                }
                else
                {
                    int lastId = 0;
                    int limit = 100000;
                    double descuentoMaximo = UserModel.getDescuentoMaximo(ClsRegeditController.getIdUserInTurn());
                    int almacenId = UserModel.getAlmacenIdFromTheUser(ClsRegeditController.getIdUserInTurn());
                    dynamic itemResponse = await ItemsController.getAllItemsFromTheServerAPI(downloadType, lastId, 0, limit,
                        descuentoMaximo, almacenId, codigoCaja);
                    response = itemResponse.value;
                }
                int value = 1;
                String message = "";
                if (response == 1)
                    message = "Actualización de Artículos finalizada correctamente";
                else if (response == 2)
                {
                    value = 2;
                    message = "Validar que el concepto Factura al Contado CFDI esté creado en el SyncROM Panel";
                }
                else if (response == -404)
                {
                    value = 2;
                    message = "Tiempo Excedido, Servidor No Encontrado";
                }
                else if (response == -500)
                {
                    value = 2;
                    message = "Algo falló en el Servidor";
                }
                else
                {
                    value = 2;
                    message = "Oops no pudimos procesar la respuesta o no hay datos que descargar";
                }
                if (fd != null)
                    fd.updateUIAftherDownload(value, true, message, positionTable);
            }
            else if (method == 3)
            {
                dynamic responseCredits = 0;
                if (serverModeLAN)
                    responseCredits = await ClsCuentasPorCobrarController.downloadCreditsLAN();
                else responseCredits = await ClsCuentasPorCobrarController.downloadCreditsAPI();
                if (responseCredits.value >= 0)
                {
                    if (fd != null)
                        fd.updateUIAftherDownload(1, true, "La actualización de cuentas por cobrar a finalizado", positionTable);
                }  else
                {
                    if (fd != null)
                        fd.updateUIAftherDownload(2, true, responseCredits.description, positionTable);
                }
            }
            else if (method == 4)
            {
                if (fd != null)
                    fd.updateUIAftherDownload(1, true, "La actualización de motivos de no visita a finalizado", positionTable);
            }
            else if (method == 5)
            {
                if (fd != null)
                    fd.updateUIAftherDownload(1, true, "La actualización de bancos a finalizado", positionTable);
            }
            else if (method == 6)
            {
                dynamic response = null;
                if (serverModeLAN)
                    response  = await ClsClasificationsController.downloadAllClasificationsLAN();
                else response = await ClsClasificationsController.downloadAllClasificationsAPI();
                if (response.value == 1)
                {
                    if (serverModeLAN)
                        await ClsClasificationsValueController.downloadAllClasificationsValueLAN();
                    else await ClsClasificationsValueController.downloadAllClasificationsValueAPI();
                    if (fd != null)
                        fd.updateUIAftherDownload(1, true, "La actualización de clasificaciones a finalizado", positionTable);
                } else if (response.value == -404 || response.value == -400)
                {
                    if (fd != null)
                        fd.updateUIAftherDownload(2, true, response.description, positionTable);
                } else if (response == -500)
                {
                    if (fd != null)
                        fd.updateUIAftherDownload(2, true, response.description, positionTable);
                } else
                {
                    if (serverModeLAN)
                        await ClsClasificationsValueController.downloadAllClasificationsValueLAN();
                    else await ClsClasificationsValueController.downloadAllClasificationsValueAPI();
                    if (fd != null)
                        fd.updateUIAftherDownload(1, true, response.value, positionTable);
                }
            }
            else if (method == 7)
            {
                String messageDownloadFc = "";
                int response = 0;
                if (serverModeLAN)
                {
                    dynamic responseFc = await FormasDeCobroController.downloadAllFormasDeCobroLAN();
                    response = responseFc.value;
                    if (response >= 1)
                    {
                        messageDownloadFc = "La actualización de formas de cobro a finalizado";
                    }
                    else if (response == -404)
                    {
                        messageDownloadFc = "Tiempo Excedido, No pudimos encontrar el servidor verificar";
                    }
                    else if (response == -500)
                    {
                        messageDownloadFc = "Oops algo falló en el servidor";
                    }
                    else
                    {
                        messageDownloadFc = responseFc.description;
                    }
                }
                else
                {
                    dynamic responseFc = await FormasDeCobroController.downloadAllFormasDeCobroAPI(INITIAL_CHARGE);
                    response = responseFc.value;
                    if (response >= 1)
                    {
                        messageDownloadFc = "La actualización de formas de cobro a finalizado";
                    }
                    else if (response == -404)
                    {
                        messageDownloadFc = "Tiempo Excedido, No pudimos encontrar el servidor verificar";
                    }
                    else if (response == -500)
                    {
                        messageDownloadFc = "Oops algo falló en el servidor";
                    }
                    else
                    {
                        messageDownloadFc = responseFc.description;
                    }
                }
                if (fd != null)
                    fd.updateUIAftherDownload(1, true, messageDownloadFc, positionTable);
            }
            else if (method == 8)
            {
                dynamic response = null;
                if (serverModeLAN)
                    response = await ClsClasificationsController.downloadAllClasificationsLAN();
                else response = await ClsClasificationsController.downloadAllClasificationsAPI();
                if (response.value == 1)
                {
                    if (serverModeLAN)
                        await ClsClasificationsValueController.downloadAllClasificationsValueLAN();
                    else await ClsClasificationsValueController.downloadAllClasificationsValueAPI();
                    if (fd != null)
                        fd.updateUIAftherDownload(1, true, "La actualización de valores de clasificaciones a finalizado", positionTable);
                } else if (response.value == -404 || response.value == -400)
                {
                    if (fd != null)
                        fd.updateUIAftherDownload(2, true, response.description, positionTable);
                } else if (response.value == -500)
                {
                    if (fd != null)
                        fd.updateUIAftherDownload(2, true, response.description, positionTable);
                }
                else
                {
                    if (serverModeLAN)
                        await ClsClasificationsValueController.downloadAllClasificationsValueLAN();
                    else await ClsClasificationsValueController.downloadAllClasificationsValueAPI();
                    if (fd != null)
                        fd.updateUIAftherDownload(1, true, response.description, positionTable);
                }
            }
            else if (method == 9)
            {
                dynamic responsePromotions = 0;
                if (serverModeLAN)
                    responsePromotions = await ClsPromotionsController.downloadAllPromotionsLAN();
                else responsePromotions = await ClsPromotionsController.downloadAllPromotionsAPI();
                if (responsePromotions.value >= 0)
                {
                    if (fd != null)
                        await fd.updateUIAftherDownload(1, true, "La actualización de promociones a finalizado", positionTable);
                } else
                {
                    if (fd != null)
                        await fd.updateUIAftherDownload(2, true, responsePromotions.description, positionTable);
                }
            }
            else if (method == 10)
            {
                dynamic response = null;
                if (serverModeLAN)
                    response = await DatosTicketController.downloadAllDatosTicketLAN();
                else response = await DatosTicketController.downloadAllDatosTicketAPI();
                if (fd != null)
                {
                    fd.updateUIAftherDownload(1, true, response.description, positionTable);
                }
            }
            else if (method == 11)
            {
                if (serverModeLAN)
                    await PreciosEmpresaController.downloadAllPreciosEmpresaLAN();
                else await PreciosEmpresaController.downloadAllPreciosEmpresaAPI();
                if (fd != null)
                    fd.updateUIAftherDownload(1, true, "La actualización de precios empresa a finalizado", positionTable);
            }
            else if (method == 12)
            {

                if (fd != null)
                    fd.updateUIAftherDownload(1, true, "Esta descarga no aplica para este sistema", positionTable);
            }
            else if (method == 13)
            {
                if (serverModeLAN)
                    await FormasDePagoController.downloadAllFormasDePagoLAN();
                else await FormasDePagoController.downloadAllFormasDePagoAPI();
                if (fd != null)
                    fd.updateUIAftherDownload(1, true, "La actualización de pedidos call center a finalizado", positionTable);
            }
            else if (method == 14)
            {
                if (serverModeLAN)
                    await ClsCustomersZonesController.downloadAllCustomersZonesLAN();
                else await ClsCustomersZonesController.downloadAllCustomersZones();
                if (fd != null)
                    fd.updateUIAftherDownload(1, true, "La actualización de zonas de clientes a finalizado", positionTable);
            }
            else if (method == 15)
            {
                if (serverModeLAN)
                    await CiudadesController.downloadAllCitiesLAN();
                else await CiudadesController.downloadAllCities();
                if (fd != null)
                    fd.updateUIAftherDownload(1, true, "La actualización de ciudades a finalizado", positionTable);
            }
            else if (method == 16)
            {
                if (serverModeLAN)
                    await EstadosController.downloadAllEstados();
                else await EstadosController.downloadAllEstados();
                if (fd != null)
                    fd.updateUIAftherDownload(1, true, "La actualización de estados a finalizado", positionTable);
            }
            else if (method == 17)
            {
                if (serverModeLAN)
                    await ClsBeneficiarioController.downloadAllBeneficiariosLAN();
                else
                    await ClsBeneficiarioController.downloadAllBeneficiarios();
                if (fd != null)
                    fd.updateUIAftherDownload(1, true, "Esta descarga no aplica para este sistema", positionTable);
            }
            else if (method == 18)
            {
                int value = 1;
                dynamic resp = new ExpandoObject();
                if (serverModeLAN)
                    resp = await CotizacionesMostradorController.downloadAllCotizacionesMostradorLAN();
                else resp = await CotizacionesMostradorController.downloadAllCotizacionesMostrador();
                String message = resp.description;
                if (resp.value == 1)
                {
                    value = 1;
                }
                else if (resp.value == -404)
                {
                    value = 2;
                }
                else if (resp.value == -500)
                {
                    value = 2;
                }
                else
                {
                    value = 2;
                }
                if (fd != null)
                    fd.updateUIAftherDownload(value, true, message, positionTable);
            }
            else if (method == 19)
            {
                dynamic resp = new ExpandoObject();
                if (serverModeLAN)
                    resp = await UnitsOfMeasureAndWeightController.downloadAllUnitsOfMeasureAndWeightLAN();
                else resp = await UnitsOfMeasureAndWeightController.downloadAllUnitsOfMeasureAndWeightAPI();
                int value = 1;
                String message = resp.description;
                if (resp.value == 1)
                {
                    value = 1;
                } else if (resp.value == -404 || resp.value == -400)
                {
                    value = 2;
                } else if (resp.value == -500)
                {
                    value = 2;
                } else if (resp.value == -1)
                {
                    value = 2;
                } else
                {
                    value = 2;
                    message += "Oops algo falló al intentar descargar los datos";
                }
                if (fd != null)
                    fd.updateUIAftherDownload(value, true, message, positionTable);
            }
            else if (method == 20)
            {
                dynamic response = new ExpandoObject();
                if (serverModeLAN)
                    response = await ConversionsUnitsController.downloadAllConversionsUnitsLAN();
                else response = await ConversionsUnitsController.downloadAllConversionsUnitsAPI();
                String message = response.description;
                int value = 1;
                if (response.value == 1)
                {
                    value = 1;
                } else if (response.value == -404 || response.value == -400)
                {
                    value = 2;
                } else if (response.value == -500)
                {
                    value = 2;
                } else if (response.value == -1)
                {
                    value = 2;
                } else
                {
                    value = 2;
                }
                if (fd != null)
                    fd.updateUIAftherDownload(value, true, message, positionTable);
            }
            else if (method == 21)
            {
                if (serverModeLAN)
                {
                    dynamic users = await UsersController.getAllUsersLAN();
                    if (users.value >= 1)
                    {
                        await ClsDatosDistribuidorController.downloadAllDatosDistribuidorLAN();
                        if (fd != null)
                            await fd.updateUIAftherDownload(1, true, "La actualización de usuarios, distribuidor y otros procesos a finalizado", positionTable);
                    } else
                    {
                        if (fd != null)
                            await fd.updateUIAftherDownload(2, true, users.description, positionTable);
                    }
                } else
                {
                    dynamic usersResponse = await UsersController.getAllUsers();
                    if (usersResponse.value >= 1)
                    {
                        await ClsDatosDistribuidorController.downloadAllDatosDistribuidor();
                        if (fd != null)
                            await fd.updateUIAftherDownload(1, true, "La actualización de usuarios, distribuidor y otros procesos a finalizado", positionTable);
                    } else
                    {
                        if (fd != null)
                            await fd.updateUIAftherDownload(2, true, usersResponse.description, positionTable);
                    }
                }
            }
            else if (method == 22)
            {
                int response = await resetAllDataTemporary();
                if (fd != null)
                    fd.updateUIAftherDownload(1, true, "La actualización de datos", positionTable);
                if (frmPrincipalPrueba != null)
                    frmPrincipalPrueba.updateUICloseFat(1, true, "Apertura de Caja Realizado Correctamente");
            }
        }

        public static async Task<int> resetAllDataTemporary()
        {
            int response = 0;
            await Task.Run(async() =>
            {
                /* Método 22: Apertura de Caja */
                MovimientosModel.deleteAllMovements();
                DocumentModel.deleteAllDocuments();
                FoliosDigitalesModel.deleteAllFoliosDigitales();
                CuentasXCobrarModel.deleteAllAbonosDeClientes();
                //Eliminar posiciones
                //Eliminar datos de clientes no visitados
                FormasDeCobroDocumentoModel.deleteAllFormasDeCobroDocumento();
                MontoRetiroModel.deleteAllMontoRetiros();
                RetiroModel.deleteAllRetiros();
                MontoIngresoModel.deleteAllMontoIngreso();
                IngresoModel.deleteAllIngresos();
                response = 1;
            });
            return response;
        }

        public static async Task<ExpandoObject> downloadCustomerIdPublicGeneralTpv()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    var client = new RestClient(ConfiguracionModel.getLinkWs());
                    var request = new RestRequest("/OBTENERCLIENTETPV", Method.Get);
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        var jsonResp = JsonConvert.DeserializeObject<ClsClienteModel>(content);
                        ClsClienteModel respuesta = (ClsClienteModel)jsonResp;
                        value = respuesta.CLIENTE_ID;
                        if (value != 0)
                            ClsRegeditController.saveIdDefaultCustomer(value);
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
                } catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    value = -1;
                    description = e.Message;
                } finally
                {
                    response.value = value;
                    response.description = description;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> downloadCustomerIdPublicGeneralTpvLAN()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    String comInstance = InstanceSQLSEModel.getStringComInstance();
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    ClsClienteModel clienteTpv = ClsCustomersModel.ObtenerClienteTPV(comInstance, panelInstance);
                    if (clienteTpv != null)
                    {
                        value = clienteTpv.CLIENTE_ID;
                        if (value != 0)
                            ClsRegeditController.saveIdDefaultCustomer(value);
                    }
                } catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    value = -1;
                    description = e.Message;
                } finally
                {
                    response.value = value;
                    response.description = description;
                }
            });
            return response;
        }

        public static ExpandoObject validateDocumentsInTheServer(String linkWs)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
                try
                {
                    var client = new RestClient(linkWs + "/validarDocumentosNoEnviadoDeUnAgente");
                    var request = new RestRequest();
                    request.AddParameter("claveAgente", UserModel.getAStringValueForAnyUser("SELECT " + LocalDatabase.CAMPO_ClAVE_USUARIO + " FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " +
                        LocalDatabase.CAMPO_ID_USUARIO + " = " + ClsRegeditController.getIdUserInTurn()));
                    var responseHeader = client.GetAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        var jsonResp = JsonConvert.DeserializeObject<ResponseDocPendingList>(content);
                        ResponseDocPendingList respuesta = (ResponseDocPendingList)jsonResp;
                        foreach (var resp in respuesta.response)
                        {
                            value = resp.valor;
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        value = -400;
                        description = "Algo falló al intentar negociar con el servidor (Error)";
                    } else if (responseHeader.Result.ResponseStatus == ResponseStatus.TimedOut)
                    {
                        value = -404;
                        description = "No se pudimos establecer conexión con el servidor (Tiempo Excedido)";
                    } else
                    {
                        value = -500;
                        description = "Algó falló en el servidor, ErrorStatus: "+ responseHeader.Result.ResponseStatus;
                    }
                } catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    value = -1;
                    description = e.Message;
                } finally
                {
                    response.value = value;
                    response.description = description;
                }
            return response;
        }

        public static async Task<ExpandoObject> validateDocumentsInTheServerLAN()
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                try
                {
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    String codigoAgente = UserModel.getAStringValueForAnyUser("SELECT " + LocalDatabase.CAMPO_ClAVE_USUARIO + " FROM " + LocalDatabase.TABLA_USUARIO +
                        " WHERE " + LocalDatabase.CAMPO_ID_USUARIO + " = " + ClsRegeditController.getIdUserInTurn());
                    int numdocsNoSends = ClsDocumentoModel.verificarDocsNoEnviadosDeUnAgente(panelInstance, codigoAgente);
                    if (numdocsNoSends > 0)
                    {
                        value = numdocsNoSends;
                        description = "Aún hay documentos pendientes por enviar a comercial";
                    }
                    else if (numdocsNoSends == -1)
                    {
                        value = numdocsNoSends;
                        description = "El Código del agente no existe";
                    }
                    else
                    {
                        value = numdocsNoSends;
                        description = "No hay documentos pendientes de enviar a comercial";
                    }
                } catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    value = -1;
                    description = e.Message;
                } finally
                {
                    response.value = value;
                    response.description = description;
                }
            });
            return response;
        }

        public static async Task intialDownloadProcess(FormPrincipal pp, int downloadType, bool serverModeLAN, String codigoCaja)
        {
            int response = 0; //haz f5
            String description = "";
            await pp.updateUI(5, false, "Descargando Clientes (5%)", 1);
            if (serverModeLAN)
                await CustomersController.downloadAllCustomersLAN(downloadType, 0);
            else await CustomersController.downloadAllCustomersAPI(downloadType, 0);
            await pp.updateUI(7, false, "Descargando Artículos (7%)", 1);
            String query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_INSTANCESQLSE + " WHERE " +
                LocalDatabase.CAMPO_ID_INSTANCESQLSE + " = 2";
            int panelInstanceCreated = InstanceSQLSEModel.getIntValue(query); ;
            if (serverModeLAN && panelInstanceCreated == 1)
            {
                /*String comInstance = InstanceSQLSEModel.getStringComInstance();
                String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                await ItemsController.getAllItemsFromSqlServerLAN(comInstance, panelInstance, 0, downloadType, 0);*/
            } else
            {
                int lastId = 0;
                int limit = 500;
                double descuentoMaximo = UserModel.getDescuentoMaximo(ClsRegeditController.getIdUserInTurn());
                int almacenId = UserModel.getAlmacenIdFromTheUser(ClsRegeditController.getIdUserInTurn());
                dynamic itemResponse = await ItemsController.getAllItemsFromTheServerAPI(downloadType, lastId, 0, limit,
                    descuentoMaximo, almacenId, codigoCaja);
                response = itemResponse.value;
                description = itemResponse.description;
            }
            if (response == 1)
            {
                dynamic responseClasifications = null;
                await pp.updateUI(10, false, "Descargando Clasificaciones (10%)", 1);
                if (serverModeLAN)
                    responseClasifications = await ClsClasificationsController.downloadAllClasificationsLAN();
                else responseClasifications = await ClsClasificationsController.downloadAllClasificationsAPI();
                if (responseClasifications.value >= 0)
                {
                    dynamic responseFormasCobro = null;
                    await pp.updateUI(15, false, "Descarganto Formas de Cobro (15%)", 1);
                    if (serverModeLAN)
                        responseFormasCobro = await FormasDeCobroController.downloadAllFormasDeCobroLAN();
                    else responseFormasCobro = await FormasDeCobroController.downloadAllFormasDeCobroAPI(INITIAL_CHARGE);
                    if (responseFormasCobro.value >= 0)
                    {
                        dynamic responseClasificationValues = null;
                        await pp.updateUI(20, false, "Descargando Valores de las Clasificaciones (20%)", 1);
                        if (serverModeLAN)
                            responseClasificationValues = await ClsClasificationsValueController.downloadAllClasificationsValueLAN();
                        else responseClasificationValues = await ClsClasificationsValueController.downloadAllClasificationsValueAPI();
                        if (responseClasificationValues.value >= 0)
                        {
                            dynamic responsePromotions = null;
                            await pp.updateUI(25, false, "Descargando Promociones (25%)", 1);
                            if (serverModeLAN)
                                responsePromotions = await ClsPromotionsController.downloadAllPromotionsLAN();
                            else responsePromotions = await ClsPromotionsController.downloadAllPromotionsAPI();
                            if (responsePromotions.value >= 0)
                            {
                                dynamic responseTicket = null;
                                await pp.updateUI(30, false, "Descargando Datos ticket (30%)", 1);
                                if (serverModeLAN)
                                    responseTicket = await DatosTicketController.downloadAllDatosTicketLAN();
                                else responseTicket = await DatosTicketController.downloadAllDatosTicketAPI();
                                if (responseTicket.value >= 0)
                                {
                                    dynamic responsePreEmpre = null;
                                    await pp.updateUI(35, false, "Descargando Precios empresa (35%)", 1);
                                    if (serverModeLAN)
                                        responsePreEmpre = await PreciosEmpresaController.downloadAllPreciosEmpresaLAN();
                                    else responsePreEmpre = await PreciosEmpresaController.downloadAllPreciosEmpresaAPI();
                                    if (responsePreEmpre.value >= 0)
                                    {
                                        dynamic responseFormasPago = null;
                                        await pp.updateUI(40, false, "Descargando Formas de pago (40%)", 1);
                                        if (serverModeLAN)
                                            responseFormasPago = await FormasDePagoController.downloadAllFormasDePagoLAN();
                                        else responseFormasPago = await FormasDePagoController.downloadAllFormasDePagoAPI();
                                        if (responseFormasPago.value >= 0)
                                        {
                                            dynamic responseZonas = null;
                                            await pp.updateUI(45, false, "Descargando Zonas de clientes - Clasificación 1 (45%)", 1);
                                            if (serverModeLAN)
                                                responseZonas = await ClsCustomersZonesController.downloadAllCustomersZonesLAN();
                                            else responseZonas = await ClsCustomersZonesController.downloadAllCustomersZones();
                                            if (responseZonas.value >= 0)
                                            {
                                                dynamic responseCities = null;
                                                await pp.updateUI(50, false, "Descargando Ciudades (50%)", 1);
                                                if (serverModeLAN)
                                                    responseCities = await CiudadesController.downloadAllCitiesLAN();
                                                else responseCities = await CiudadesController.downloadAllCities();
                                                if (responseCities.value >= 0)
                                                {
                                                    dynamic responseEstados = null;
                                                    await pp.updateUI(55, false, "Descargando Estados (55%)", 1);
                                                    if (serverModeLAN)
                                                        responseEstados = await EstadosController.downloadAllEstadosLAN();
                                                    else responseEstados = await EstadosController.downloadAllEstados();
                                                    if (responseEstados.value >= 0)
                                                    {
                                                        dynamic responseBene = null;
                                                        await pp.updateUI(60, false, "Descargando Beneficiarios (60%)", 1);
                                                        if (serverModeLAN)
                                                            responseBene = await ClsBeneficiarioController.downloadAllBeneficiariosLAN();
                                                        else responseBene = await ClsBeneficiarioController.downloadAllBeneficiarios();
                                                        if (responseBene.value >= 0)
                                                        {
                                                            dynamic responseCredits = null;
                                                            await pp.updateUI(65, false, "Descargando Creditos (65%)", 1);
                                                            //Pagos Créditos
                                                            if (serverModeLAN)
                                                                responseCredits = await ClsCuentasPorCobrarController.downloadCreditsLAN();
                                                            else responseCredits = await ClsCuentasPorCobrarController.downloadCreditsAPI();
                                                            if (responseCredits.value >= 0)
                                                            {
                                                                //Pedidos Callcenter
                                                                //Pedidos Comercial
                                                                dynamic responseCot = null;
                                                                await pp.updateUI(67, false, "Descargando Cotizaciones de mostrador (67%)", 1);
                                                                if (serverModeLAN)
                                                                    responseCot = await CotizacionesMostradorController.downloadAllCotizacionesMostradorLAN();
                                                                else responseCot = await CotizacionesMostradorController.downloadAllCotizacionesMostrador();
                                                                if (responseCot.value >= 0)
                                                                {
                                                                    dynamic responseUnits = null;
                                                                    await pp.updateUI(70, false, "Descargando Unidades de medida y peso (70%)", 1);
                                                                    if (serverModeLAN)
                                                                        responseUnits = await UnitsOfMeasureAndWeightController.downloadAllUnitsOfMeasureAndWeightLAN();
                                                                    else responseUnits = await UnitsOfMeasureAndWeightController.downloadAllUnitsOfMeasureAndWeightAPI();
                                                                    if (responseUnits.value >= 0)
                                                                    {
                                                                        dynamic reponseConversion = null;
                                                                        await pp.updateUI(75, false, "Descargando Conversiones de unidades (75%)", 1);
                                                                        if (serverModeLAN)
                                                                            reponseConversion = await ConversionsUnitsController.downloadAllConversionsUnitsLAN();
                                                                        else reponseConversion = await ConversionsUnitsController.downloadAllConversionsUnitsAPI();
                                                                        if (reponseConversion.value >= 0)
                                                                        {
                                                                            dynamic responseUser = null;
                                                                            await pp.updateUI(80, false, "Actualizando información del usuario (80%)", 1);
                                                                            //Verificar si hay algún cliente adicional no enviado
                                                                            if (serverModeLAN)
                                                                                responseUser = await UsersController.getAllUsersLAN();
                                                                            else responseUser = await UsersController.getAllUsers();
                                                                            if (responseUser.value >= 0)
                                                                            {
                                                                                dynamic responseDist = null;
                                                                                await pp.updateUI(90, false, "Actualizando datos del distribuidor (90%)", 1);
                                                                                if (serverModeLAN)
                                                                                    responseDist = await ClsDatosDistribuidorController.downloadAllDatosDistribuidorLAN();
                                                                                else responseDist = await ClsDatosDistribuidorController.downloadAllDatosDistribuidor();
                                                                                if (responseDist.value >= 0)
                                                                                {
                                                                                    dynamic reponseCP = null;
                                                                                    await pp.updateUI(92, false, "Actualizando cliente público en general (92%)", 1);
                                                                                    if (serverModeLAN)
                                                                                        reponseCP = await downloadCustomerIdPublicGeneralTpvLAN();
                                                                                    else reponseCP = await downloadCustomerIdPublicGeneralTpv();
                                                                                    if (reponseCP.value >= 0)
                                                                                    {
                                                                                        await pp.updateUI(95, false, "Finalizando proceso de descarga (90%)", 1);
                                                                                        if (downloadType == INITIAL_CHARGE)
                                                                                        {
                                                                                            MovimientosModel.deleteAllMovements();
                                                                                            DocumentModel.deleteAllDocuments();
                                                                                            //Eliminar posiciones
                                                                                            //Eliminar datos de clientes no visitados
                                                                                            FormasDeCobroDocumentoModel.deleteAllFormasDeCobroDocumento();
                                                                                            MontoRetiroModel.deleteAllMontoRetiros();
                                                                                            RetiroModel.deleteAllRetiros();
                                                                                        }
                                                                                        else if (downloadType == UPDATE_DATA)
                                                                                        {
                                                                                            await decreaseExistenceInCaseOfHavingUnsentDocumentsOfSaleType(serverModeLAN,
                                                                                                codigoCaja);
                                                                                        }
                                                                                        await pp.updateUI(100, true, "Proceso Finalizado (100%)", 1);
                                                                                    } else await pp.updateUI(100, true, reponseCP.description + " (92%)", 3);
                                                                                } else await pp.updateUI(100, true, responseDist.description + " (90%)", 3);
                                                                            } else await pp.updateUI(100, true, responseUser.description + " (80%)", 3);
                                                                        } else await pp.updateUI(100, true, reponseConversion.description + " (75%)", 3);
                                                                    } else await pp.updateUI(100, true, responseUnits.description + " (70%)", 3);
                                                                } else await pp.updateUI(100, true, responseCot.description + " (67%)", 3);
                                                            } else await pp.updateUI(100, true, responseCredits.description + " (65%)", 3);
                                                        } else await pp.updateUI(100, true, responseBene.description + " (60%)", 3);
                                                    } else await pp.updateUI(100, true, responseEstados.description + " (55%)", 3);
                                                } else await pp.updateUI(100, true, responseCities.description + " (50%)", 3);
                                            } else await pp.updateUI(100, true, responseZonas.description + " (45%)", 3);
                                        } else await pp.updateUI(100, true, responseFormasPago.description + " (40%)", 3);
                                    } else await pp.updateUI(100, true, responsePreEmpre.description + " (35%)", 3);
                                } else await pp.updateUI(100, true, responseTicket.description + " (30%)", 3);
                            } else await pp.updateUI(100, true, responsePromotions.description + " (25%)", 3);
                        } else await pp.updateUI(100, true, responseClasificationValues.description + " (20%)", 3);
                    } else await pp.updateUI(100, true, responseFormasCobro.description + " (15%)", 3);
                } else await pp.updateUI(100, true, responseClasifications.description + " (10%)", 3);
            }
            else if (response == 2)
                await pp.updateUI(100, true, "No se a dado de alta el concepto Factura a Contado en el PanelROM (7%)", 3);
            else await pp.updateUI(100, true, description, 3);
        }


        public static async Task decreaseExistenceInCaseOfHavingUnsentDocumentsOfSaleType(bool serverModeLAN, String codigoCaja)
        {
            await Task.Run(async () =>
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " + LocalDatabase.CAMPO_ENVIADOALWS_DOC + " = " + 0 +
                        " AND " + LocalDatabase.CAMPO_CANCELADO_DOC + " = " + 0;
                List<DocumentModel> docsList = DocumentModel.getAllDocuments(query);
                if (docsList != null)
                {
                    for (int j = 0; j < docsList.Count; j++)
                    {
                        List<MovimientosModel> listaDeMovs = MovimientosModel.getMovimientosOfTheCurrentDocument(docsList[j].id);
                        if (listaDeMovs != null)
                        {
                            ClsItemModel itemModel = null;
                            dynamic responseGetItem = null;
                            for (int k = 0; k < listaDeMovs.Count; k++)
                            {
                                if (serverModeLAN)
                                {
                                    responseGetItem = await ItemsController.getAnItemFromTheServerLAN(listaDeMovs[k].itemId, null, codigoCaja);
                                    if (responseGetItem.item != null)
                                    {
                                        itemModel = responseGetItem.item;
                                    }
                                } else
                                {
                                    responseGetItem = await ItemsController.getAnItemFromTheServerAPI(listaDeMovs[k].itemId, null, codigoCaja);
                                    if (responseGetItem.item != null)
                                    {
                                        itemModel = responseGetItem.item;
                                    }
                                }
                                MovimientosModel.decreaseOrIncreaseExistenceOfAnItem(docsList[j].id,
                                        itemModel, listaDeMovs[k].capturedUnits,
                                        listaDeMovs[k].capturedUnitId,
                                        listaDeMovs[k].price, listaDeMovs[k].descuentoImporte, docsList[j].tipo_documento,
                                        listaDeMovs[k].id, serverModeLAN);
                            }
                        }
                    }
                }
            });
        }

    }
}
