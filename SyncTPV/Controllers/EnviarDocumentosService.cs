using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using wsROMClases.Models;
using wsROMClases.Models.Panel;

namespace SyncTPV.Controllers
{
    public class EnviarDocumentosService
    {

        public async Task<ExpandoObject> startActionSendPayment(int method, int envioDeDatos, int peticiones, int idDocument, bool serverModeLAN)
        {
            dynamic response = new ExpandoObject();
            /** 1 = Clientes, 2 = Documentos, 3 = Posiciones, 4 = Cxc */
            if (method == 1)
            {
                //enviarClientesAdicionales(1, envioDeDatos, peticiones);
            }
            else if (method == 2)
            {
                //enviarDocumentos(method, envioDeDatos, peticiones);
            }
            else if (method == 3)
            {
                //enviarUbicaciones(method, envioDeDatos, peticiones);
            }
            else if (method == 4)
            {
                if (serverModeLAN)
                    response = await enviarCuentasPorCobrarLAN(idDocument, method, envioDeDatos, peticiones);
                else response = await enviarCuentasPorCobrar(idDocument, method, envioDeDatos, peticiones);
            }
            else if (method == 5)
            {
                //enviarMovimientosDeUndocumento(idDocumento, method, envioDeDatos);
            }
            else if (method == 6)
            {
                //enviarAplicarDoctoVenta(idDocumento, envioDeDatos, peticiones);
            }
            return response;
        }

        private async Task<ExpandoObject> enviarCuentasPorCobrar(int idDocumento, int method, int envioDeDatos, int peticiones)
        {
            dynamic response = null;
            await Task.Run(async () =>
            {
                ClsPositionsModel pm = null;
                List<CuentasXCobrarModel> repNotSended = CuentasXCobrarModel.getOneNotSendCxc(idDocumento, 0);
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
                                " \"bankId\":" + 0 + ",\n" +
                                " \"date\":\"" + repNotSended[i].date + "\",\n" +
                                " \"documentFolio\":\"" + repNotSended[i].documentFolio + "\",\n" +
                                " \"status\":" + 0 + ",\n" +
                                " \"userId\":" + user.id + ",\n";
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
                        response = sendRepsToTheServer(repIdApp, locationIdApp, jsonRep, method, envioDeDatos,
                            repNotSended[0], pm);
                    }
                    catch (Exception e)
                    {
                        SECUDOC.writeLog("Exception: " + e.ToString());
                    }
                }
                else
                {
                    response.value = 100;
                    response.description = "Proceso Finalizado";
                    response.method = 7;
                    response.envioDeDatos = envioDeDatos;
                    response.peticiones = peticiones;
                    response.idDocumento = "";
                }
            });
            return response;
        }

        private async Task<ExpandoObject> enviarCuentasPorCobrarLAN(int idDocumento, int method, int envioDeDatos,
            int peticiones)
        {
            dynamic response = null;
            await Task.Run(async () =>
            {
                ClsAccountsReceivableModel ar = null;
                List<CuentasXCobrarModel> repNotSended = CuentasXCobrarModel.getOneNotSendCxc(idDocumento, 0);
                if (repNotSended != null)
                {
                    int repIdApp = 0;
                    int locationIdApp = 0;
                    UserModel user = UserModel.getNameIdAndRutaOfTheUser();
                    for (int i = 0; i < 1; i++)
                    {
                        ar = new ClsAccountsReceivableModel();
                        repIdApp = repNotSended[i].id;
                        String customerCode = CustomerModel.getClaveForAClient(repNotSended[i].customerId);
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
                        response = sendRepsToTheServerLAN(repIdApp, ar, method, envioDeDatos);
                    }
                    catch (Exception e)
                    {
                        SECUDOC.writeLog("Exception: " + e.ToString());
                    }
                }
                else
                {
                    response.value = 100;
                    response.description = "Proceso Finalizado";
                    response.method = 7;
                    response.envioDeDatos = envioDeDatos;
                    response.peticiones = peticiones;
                    response.idDocumento = "";
                }
            });
            return response;
        }

        private ExpandoObject sendRepsToTheServer(int repIdApp, int idLocationApp, String savedData, int method, int envioDeDatos,
            CuentasXCobrarModel repNotSended, ClsPositionsModel pm)
        {
            dynamic response = new ExpandoObject();
            try
            {
                String ip = ConfiguracionModel.getLinkWs();
                // Se define el URL de la web service
                String url = ip;
                url = url.Replace(" ", "%20");
                var client = new RestClient(url);
                var request = new RestRequest("/saveAccountsReceivable", Method.Post);
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
                    //var responseHttp = client.Get<String>(request);
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
                                    response.value = 100;
                                    response.description = description;
                                    response.method = method;
                                    response.envioDeDatos = envioDeDatos;
                                    response.peticiones = 0;
                                    response.idDocumento = "";
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        SECUDOC.writeLog("Exception: " + e.ToString());
                        response.value = 404;
                        response.description = "Error en la respuesta";
                        response.method = method;
                        response.envioDeDatos = envioDeDatos;
                        response.peticiones = 0;
                        response.idDocumento = "";
                    }
                }
                else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                {
                    if (responseHeader.Result.ErrorMessage.Equals("Unable to connect to the remote server"))
                    {
                        response.value = 404;
                        response.description = "Tiempo Excedido";
                        response.method = method;
                        response.envioDeDatos = envioDeDatos;
                        response.peticiones = 0;
                        response.idDocumento = "";
                    }
                    else
                    {
                        response.value = 404;
                        response.description = "Algo falló al intentar conectar con la IP del servidor";
                        response.method = method;
                        response.envioDeDatos = envioDeDatos;
                        response.peticiones = 0;
                        response.idDocumento = "";
                    }
                }
            }
            catch (Exception ex)
            {
                SECUDOC.writeLog("Exception: " + ex.ToString());
                response.value = 404;
                response.description = "Respuesta Incorrecta";
                response.method = method;
                response.envioDeDatos = envioDeDatos;
                response.peticiones = 0;
                response.idDocumento = "";
            }
            return response;
        }

        private ExpandoObject sendRepsToTheServerLAN(int repIdApp, ClsAccountsReceivableModel ar, int method, int envioDeDatos)
        {
            dynamic response = new ExpandoObject();
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
                        response.value = 100;
                        response.description = description;
                        response.method = method;
                        response.envioDeDatos = envioDeDatos;
                        response.peticiones = 0;
                        response.idDocumento = "";
                    }
                }
            }
            catch (Exception e)
            {
                SECUDOC.writeLog("Exception: " + e.ToString());
                response.value = 404;
                response.description = "Error en la respuesta";
                response.method = method;
                response.envioDeDatos = envioDeDatos;
                response.peticiones = 0;
                response.idDocumento = "";
            }
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

    }
}
