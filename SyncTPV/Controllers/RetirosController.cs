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

namespace SyncTPV.Controllers
{
    public class RetirosController
    {

        public static async Task<ExpandoObject> uploadRetirosLAN()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    List<ClsRetirosModel> retirosList = RetiroModel.getAllWithdrawalsNotSendedToTheServer();
                    List<ExpandoObject> responsRetiros = ClsRetirosModel.saveWithdrawalsAlongWithTheirAmounts(panelInstance, retirosList);
                    if (responsRetiros != null)
                    {
                        for (int i = 0; i < responsRetiros.Count; i++)
                        {
                            dynamic rr = responsRetiros[i];
                            int idRetiroApp = rr.idRetiroApp;
                            int idRetiroServer = rr.idRetiroServer;
                            RetiroModel.updateServerIdInAWithdrawal(idRetiroApp, idRetiroServer);
                            MontoRetiroModel.updateSentFieldOfWithdrawalAmounts(idRetiroApp);
                        }
                        value = 1;
                        description = "Proceso Finalizado";
                    }
                    else
                    {
                        description = "La respuesta del servidor fue nula";
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

        public static async Task<ExpandoObject> uploadRetirosAPI()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    List<ClsRetirosModel> retirosList = RetiroModel.getAllWithdrawalsNotSendedToTheServer();
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
                        if (jsonRetiro != null && jsonRetiro.response.Count > 0)
                        {
                            for (int i = 0; i < jsonRetiro.response.Count; i++)
                            {
                                RetiroResponse object1 = jsonRetiro.response[i];
                                int idRetiroApp = object1.idRetiroApp;
                                int idRetiroServer = object1.idRetiroServer;
                                RetiroModel.updateServerIdInAWithdrawal(idRetiroApp, idRetiroServer);
                                MontoRetiroModel.updateSentFieldOfWithdrawalAmounts(idRetiroApp);
                            }
                            value = 1;
                            description = "Proceso Finalizado";
                        }
                        else
                        {
                            description = "La respuesta del servidor fue nula";
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        if (responseHeader.Result.ErrorMessage.Equals("Unable to connect to the remote server"))
                        {
                            value = -404;
                            description = "Tiempo Excedido";
                        }
                        else
                        {
                            value = -500;
                            description = "Algo falló al intentar conectar con la IP del servidor";
                        }
                    }
                }
                catch (Exception ex)
                {
                    SECUDOC.writeLog("Exception: " + ex.ToString());
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

        private class RetiroResponse
        {
            public int idRetiroApp { get; set; }
            public int idRetiroServer { get; set; }
        }

        private class ResponseRetiro
        {
            public List<RetiroResponse> response { get; set; }
        }

    }
}
