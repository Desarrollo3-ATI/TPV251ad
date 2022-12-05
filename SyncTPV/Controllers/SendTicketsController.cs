using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wsROMClases;
using wsROMClases.Models;

namespace SyncTPV.Controllers
{
    public class SendTicketsController
    {
        public async Task<ExpandoObject> handleActionSendIngresosAPI(int idIngresoApp)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                String jsonRetiros = "";
                List<TicketsModel> ticketsList = null;
                TicketsModel im = TicketsModel.getTickets(idIngresoApp);
                if (im != null)
                {
                    /*jsonRetiros += "{\n \"retiros\": [\n";
                    jsonRetiros += "{\n" +
                            " \"id\":" + im.id + ",\n" +
                            " \"number\":" + im.number + ",\n" +
                            " \"userId\":" + im.userId + ",\n" +
                            " \"userCode\":\"" + im.userCode + "\",\n" +
                            " \"dateTime\":\"" + im.dateTime + "\",\n" +
                            " \"concept\":\"" + im.concept + "\",\n" +
                            " \"description\":\"" + im.description + "\",\n" +
                            " \"montos\":[\n";*/
                    try
                    {
                        response = await sendTicketsToWs(im);
                    }
                    catch (Exception e)
                    {
                        SECUDOC.writeLog(e.ToString());
                        response.value = -1;
                        response.description = e.Message + ", Enviando Ingresos!";
                        response.peticiones = 0;
                    }
                    
                }
                else
                {
                    response.value = 100;
                    response.description = "Proceso Finalizado";
                    response.peticiones = 0;
                }
            });
            return response;
        }

        public async Task<ExpandoObject> handleActionSendIngresoLAN(int idIngresoApp)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                List<ClsIngresosModel> ingresosListToSend = null;
                ClsIngresosModel ingresoToSend = null;
                IngresoModel im = IngresoModel.getAnEntryNotSendedToTheServer(idIngresoApp);
                if (im != null)
                {
                    ingresosListToSend = new List<ClsIngresosModel>();
                    if (MontoIngresoModel.checkForEntryAmounts(im.id) > 0)
                    {
                        ingresoToSend = new ClsIngresosModel();
                        ingresoToSend.id = im.id;
                        ingresoToSend.number = im.number;
                        ingresoToSend.userId = im.userId;
                        ingresoToSend.userCode = im.userCode;
                        ingresoToSend.dateTime = im.dateTime;
                        ingresoToSend.concept = im.concept;
                        ingresoToSend.description = im.description;
                        List<ClsMontoIngresoModel> montosToSend = null;
                        ClsMontoIngresoModel montoToSend = null;
                        List<MontoIngresoModel> montosList = MontoIngresoModel.getAllAmountsFromAnEntry(im.id);
                        if (montosList != null)
                        {
                            montosToSend = new List<ClsMontoIngresoModel>();
                            for (int j = 0; j < montosList.Count; j++)
                            {
                                montoToSend = new ClsMontoIngresoModel();
                                montoToSend.id = montosList[j].id;
                                montoToSend.formaCobroId = montosList[j].formaCobroId;
                                montoToSend.importe = montosList[j].importe;
                                if (montosList[j].createdAt != null && !montosList[j].createdAt.Equals(""))
                                    montoToSend.createdAt = Convert.ToDateTime(montosList[j].createdAt);
                                else montoToSend.createdAt = Convert.ToDateTime(MetodosGenerales.getCurrentDateAndHour());
                                if (montosList[j].updatedAt != null && !montosList[j].updatedAt.Equals(""))
                                    montoToSend.updatedAt = Convert.ToDateTime(montosList[j].updatedAt);
                                else montoToSend.updatedAt = Convert.ToDateTime(MetodosGenerales.getCurrentDateAndHour());
                                montosToSend.Add(montoToSend);
                            }
                        }
                        else
                        {
                            montosToSend = new List<ClsMontoIngresoModel>();
                        }
                        ingresoToSend.montos = montosToSend;
                        ingresosListToSend.Add(ingresoToSend);
                        try
                        {
                            response = await sendIngresoLAN(ingresosListToSend);
                        }
                        catch (Exception e)
                        {
                            SECUDOC.writeLog(e.ToString());
                            response.value = -1;
                            response.description = e.Message + ", Enviando Ingresos!";
                            response.peticion = 0;
                        }
                    }
                    else
                    {
                        response.value = 100;
                        response.description = "Proceso Finalizado";
                        response.peticiones = 0;
                    }
                }
                else
                {
                    response.value = 100;
                    response.description = "Proceso Finalizado";
                    response.peticiones = 0;
                }
            });
            return response;
        }

        private class ResponseIngreso
        {
            public List<IngresoResponse> response { get; set; }
        }

        private class IngresoResponse
        {
            public int idIngresoApp { get; set; }
            public int idIngresoServer { get; set; }
        }

        public async Task<ExpandoObject> sendTicketsToWs(TicketsModel TicketsList)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int correctResponse = 0;
                try
                {
                    int itemsToEvaluate = 0;
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    // client.Authenticator = new HttpBasicAuthenticator(username, password);
                    var request = new RestRequest("/insertartickets", Method.Post);
                    request.AddJsonBody(new
                    {
                        tikets = TicketsList
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content; // Raw content as string
                        var jsonResp = JsonConvert.DeserializeObject<ResponseIngreso>(content);
                        ResponseIngreso respJson = (ResponseIngreso)jsonResp;
                        if (respJson != null)
                        {
                            for (int i = 0; i < respJson.response.Count; i++)
                            {
                                IngresoResponse wr = respJson.response[i];
                                int idRetiroApp = wr.idIngresoApp;
                                int idRetiroServer = wr.idIngresoServer;
                                TicketsModel.actualizarIdServer(TicketsList.id, idRetiroServer);
                                //IngresoModel.updateServerIdInAnEntry(idRetiroApp, idRetiroServer);
                                //MontoIngresoModel.updateSentFieldOfEntryAmounts(idRetiroApp);
                            }
                            correctResponse = 1;
                            response.description = "Proceso Finalizado";
                        }
                        else
                        {
                            correctResponse = 0;
                            response.description = "Error en el envio del ticket "+ TicketsList.referencia;
                        }
                        response.value = 100;
                        response.peticion = 0;
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        response.value = -404;
                        response.description = MetodosGenerales.getErrorMessageFromNetworkCode(-404,
                            responseHeader.Result.ErrorException.Message + ", Enviando Ingresos!");
                        response.peticion = 0;
                    }
                    else
                    {
                        response.value = -500;
                        response.description = MetodosGenerales.getErrorMessageFromNetworkCode(-500,
                            responseHeader.Result.ErrorException.Message + ", Enviando Ingresos!");
                        response.peticion = 0;
                    }
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    response.value = -1;
                    response.description = e.Message + ", Enviando Ingresos!";
                    response.peticion = 0;
                    correctResponse = 1;
                }
            });
            return response;
        }

        private async Task<ExpandoObject> sendIngresoLAN(List<ClsIngresosModel> ingresosListToSend)
        {
            dynamic response = new ExpandoObject();
            int correctResponse = 0;
            try
            {
                int itemsToEvaluate = 0;
                String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                List<ExpandoObject> responseIngresos = ClsIngresosModel.saveEntriesAlongWithTheirAmounts(panelInstance, ingresosListToSend);
                try
                {
                    if (responseIngresos != null)
                    {
                        for (int i = 0; i < responseIngresos.Count; i++)
                        {
                            dynamic object1 = responseIngresos[i];
                            int idIngresoApp = object1.idIngresoApp;
                            int idIngresoServer = object1.idIngresoServer;
                            IngresoModel.updateServerIdInAnEntry(idIngresoApp, idIngresoServer);
                            MontoIngresoModel.updateSentFieldOfEntryAmounts(idIngresoApp);
                        }
                        response.value = 100;
                        response.description = "Proceso Finalizado";
                        response.peticion = 0;
                        correctResponse = 1;
                    }
                    else
                    {
                        response.value = 100;
                        response.description = "Respuesta Incorrecta!";
                        response.peticion = 0;
                    }
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog("Exception: " + e.ToString());
                    response.value = -1;
                    response.description = e.Message + ", Enviando Ingresos!";
                    response.peticion = 0;
                }
            }
            catch (Exception e)
            {
                SECUDOC.writeLog(e.ToString());
                response.value = -1;
                response.description = e.Message + ", Enviando Ingresos!";
                response.peticion = 0;
            }
            //response.error = correctResponse;
            return response;
        }

    }
}
