using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Models;
using System;
using System.Dynamic;
using System.Threading.Tasks;
using wsROMClases;

namespace SyncTPV.Controllers
{
    public class ClsDatosDistribuidorController
    {
        public static async Task<ExpandoObject> downloadAllDatosDistribuidor()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    int lastId = 0;
                    int itemsToEvaluate = 0;
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    // client.Authenticator = new HttpBasicAuthenticator(username, password);
                    var request = new RestRequest("/getAllDataAboutdistributor", Method.Get);
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content; // Raw content as string
                                                                     //var responseHttp = client.PostAsync<String>(request);
                        var jsonResp = JsonConvert.DeserializeObject<ResponseDatosDistribuidor>(content);
                        ResponseDatosDistribuidor distList = (ResponseDatosDistribuidor)jsonResp;
                        if (lastId == 0)
                            DatosDistribuidorModel.deleteAllDistribuidores();
                        lastId = DatosDistribuidorModel.saveAllDistribuidores(distList.response);
                        itemsToEvaluate = distList.response.Count;
                        value = 1;
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        value = -400;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, responseHeader.Result.ErrorException.Message);
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.TimedOut) 
                    {
                        value = -404;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, responseHeader.Result.ErrorException.Message);
                    } else
                    {
                        value = -500;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, responseHeader.Result.ErrorException.Message);
                    }
                }
                catch (Exception e)
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

        public static async Task<ExpandoObject> downloadAllDatosDistribuidorLAN()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    int lastId = 0;
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    ClsDatosDistribuidor ddm = ClsDatosDistribuidor.getAllDataAboutDistributor(panelInstance);
                    if (ddm != null)
                    {
                        if (lastId == 0)
                            DatosDistribuidorModel.deleteAllDistribuidores();
                        value = DatosDistribuidorModel.saveAllDistribuidoresLAN(ddm);
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

    }
}
