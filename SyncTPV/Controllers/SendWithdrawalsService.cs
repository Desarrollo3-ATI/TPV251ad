using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using wsROMClases;
using wsROMClases.Models.Panel;

namespace SyncTPV.Controllers
{
    public class SendWithdrawalsService
    {

        public async Task<ExpandoObject> handleActionSendRetiros(int idRetiroApp)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                String jsonRetiros = "";
                List<RetiroModel> retirosList = null;
                List<MontoRetiroModel> montosList = null;
                RetiroModel rm = RetiroModel.getAWithdrawalsNotSentToTheServer(idRetiroApp);
                if (rm != null)
                {
                    
                    if (MontoRetiroModel.checkForWithdrawalAmounts(rm.id) > 0)
                    {
                        /*jsonRetiros += "{\n \"retiros\": [\n";
                        jsonRetiros += "{\n" +
                                " \"id\":" + rm.id + ",\n" +
                                " \"number\":" + rm.number + ",\n" +
                                " \"idUsuario\":" + rm.idUsuario + ",\n" +
                                " \"claveUsuario\":\"" + rm.claveUsuario + "\",\n" +
                                " \"fechaHora\":\"" + rm.fechaHora + "\",\n" +
                                " \"concept\":\"" + rm.concept + "\",\n" +
                                " \"description\":\"" + rm.description + "\",\n" +
                                " \"montos\":[\n";*/
                        String jsonMontos = "";
                        montosList = MontoRetiroModel.getAllAmountsFromAWithdrawal(rm.id);
                        if (montosList != null)
                        {
                            rm.montos = montosList;
                        }
                        else
                        {
                            jsonMontos += "]";
                        }
                        jsonRetiros += jsonMontos + "\n}\n";
                        jsonRetiros += "]\n}";
                        try
                        {
                            retirosList = new List<RetiroModel>();
                            retirosList.Add(rm);
                            response = sendRetirosToWs(retirosList);
                        }
                        catch (Exception e)
                        {
                            SECUDOC.writeLog(e.ToString());
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

        public async Task<ExpandoObject> handleActionSendRetirosLAN(int idRetiroApp)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                List<ClsRetirosModel> retirosListToSend = null;
                ClsRetirosModel retiroToSend = null;
                RetiroModel rm = RetiroModel.getAWithdrawalsNotSentToTheServer(idRetiroApp);
                if (rm != null)
                {
                    retirosListToSend = new List<ClsRetirosModel>();
                    if (MontoRetiroModel.checkForWithdrawalAmounts(rm.id) > 0)
                    {
                        retiroToSend = new ClsRetirosModel();
                        retiroToSend.id = rm.id;
                        retiroToSend.number = rm.number;
                        retiroToSend.idUsuario = rm.idUsuario;
                        retiroToSend.claveUsuario = rm.claveUsuario;
                        retiroToSend.fechaHora = rm.fechaHora;
                        retiroToSend.concept = rm.concept;
                        retiroToSend.description = rm.description;
                        List<ClsMontosRetirosModel> montosToSend = null;
                        ClsMontosRetirosModel montoToSend = null;
                        List<MontoRetiroModel> montosList = MontoRetiroModel.getAllAmountsFromAWithdrawal(rm.id);
                        if (montosList != null)
                        {
                            montosToSend = new List<ClsMontosRetirosModel>();
                            for (int j = 0; j < montosList.Count; j++)
                            {
                                montoToSend = new ClsMontosRetirosModel();
                                montoToSend.id = montosList[j].id;
                                montoToSend.formaCobroId = montosList[j].formaCobroId;
                                montoToSend.importe = montosList[j].importe;
                                if (montosList[j].createdAt != null && !montosList[j].importe.Equals(""))
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
                        retirosListToSend.Add(retiroToSend);
                        try
                        {
                            response = sendRetirosLAN(retirosListToSend);
                        }
                        catch (Exception e)
                        {
                            SECUDOC.writeLog(e.ToString());
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

        private class ResponseWithdrawal
        {
            public List<WithdrawalResponse> response { get; set; }
        }

        private class WithdrawalResponse
        {
            public int idRetiroApp { get; set; }
            public int idRetiroServer { get; set; }
        }

        private ExpandoObject sendRetirosToWs(List<RetiroModel> rm)
        {
            dynamic response = new ExpandoObject();
            int correctResponse = 0;
            try
            {
                int itemsToEvaluate = 0;
                String url = ConfiguracionModel.getLinkWs();
                url = url.Replace(" ", "%20");
                var client = new RestClient(url);
                // client.Authenticator = new HttpBasicAuthenticator(username, password);
                var request = new RestRequest("/insertBoxWithdrawals", Method.Post);
                request.AddJsonBody(new
                {
                    retiros = rm
                });
                var responseHeader = client.ExecuteAsync(request);
                if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                {
                    var content = responseHeader.Result.Content; // Raw content as string
                    //var responseHttp = client.PostAsync<String>(request);
                    var jsonResp = JsonConvert.DeserializeObject<ResponseWithdrawal>(content);
                    ResponseWithdrawal respJson = (ResponseWithdrawal)jsonResp;
                    for (int i = 0; i < respJson.response.Count; i++)
                    {
                        WithdrawalResponse wr = respJson.response[i];
                        int idRetiroApp = wr.idRetiroApp;
                        int idRetiroServer = wr.idRetiroServer;
                        RetiroModel.updateServerIdInAWithdrawal(idRetiroApp, idRetiroServer);
                        MontoRetiroModel.updateSentFieldOfWithdrawalAmounts(idRetiroApp);
                    }
                    response.value = 100;
                    response.description = "Proceso Finalizado";
                    response.peticion = 0;
                    correctResponse = 1;
                }
                else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                {
                    if (responseHeader.Result.ErrorMessage.Equals("Unable to connect to the remote server"))
                    {
                        response.value = 404;
                        response.description = "Proceso Finalizado";
                        response.peticion = 0;
                    }
                    else
                    {
                        response.value = 404;
                        response.description = "Proceso Finalizado";
                        response.peticion = 0;
                    }
                }
            }
            catch (Exception e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            //response.error = correctResponse;
            return response;
        }

        private ExpandoObject sendRetirosLAN(List<ClsRetirosModel> retirosListToSend)
        {
            dynamic response = new ExpandoObject();
            int correctResponse = 0;
            try
            {
                int itemsToEvaluate = 0;
                String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                List<ExpandoObject> responseRetiros = ClsRetirosModel.saveWithdrawalsAlongWithTheirAmounts(panelInstance, retirosListToSend);
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
                    response.value = 100;
                    response.description = "Respuesta Incorrecta!";
                    response.peticion = 0;
                    SECUDOC.writeLog("Exception: " + e.ToString());
                }
            }
            catch (Exception e)
            {
                SECUDOC.writeLog(e.ToString());
                response.value = 100;
                response.description = "Respuesta Incorrecta!";
                response.peticion = 0;
            }
            //response.error = correctResponse;
            return response;
        }

    }
}
