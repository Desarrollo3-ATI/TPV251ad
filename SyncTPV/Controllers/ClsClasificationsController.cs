using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using wsROMClases;

namespace SyncTPV.Controllers
{
    public class ClsClasificationsController
    {
        public static async Task<ExpandoObject> downloadAllClasificationsAPI()
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                try
                {
                    int lastId = 0;
                    int itemsToEvaluate = 0;
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/getAllClasifications", Method.Post);
                    request.AddJsonBody(new
                    {

                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        ResponseGetAllClasifications responseClas = JsonConvert.DeserializeObject<ResponseGetAllClasifications>(content);
                        if (responseClas.value == 1)
                        {
                            if (lastId == 0)
                                ClsClasificationsModel.deleteAllClasifications();
                            lastId = ClsClasificationsModel.saveAllClasifications(responseClas.clasificationsList);
                            value = 1;
                        } else
                        {
                            value = responseClas.value;
                            description = responseClas.description;
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
                    }
                    else
                    {
                        value = -500;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value,
                            responseHeader.Result.ErrorException.Message);
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
                }
            });
            return response;
        }

        private class ResponseGetAllClasifications
        {
            public int value { get; set; }
            public String description { get; set; }
            public List<ClsClasificacionesModel> clasificationsList { get; set; }
        }

        public static async Task<ExpandoObject> downloadAllClasificationsLAN()
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                int lastId = 0;
                try
                {
                    String comInstance = InstanceSQLSEModel.getStringComInstance();
                    dynamic responseClas = ClsClasificacionesModel.getAllClasifications(comInstance);
                    if (responseClas.value == 1)
                    {
                        int itemsToEvaluate = responseClas.clasificationsList.Count;
                        if (lastId == 0)
                            ClsClasificationsModel.deleteAllClasifications();
                        int count = ClsClasificationsModel.saveAllClasificationsLAN(responseClas.clasificationsList);
                        if (itemsToEvaluate == count)
                            value = 1;
                        else description = "No se pudieron guardar todas las clasificaciones en la base de datos local!";
                    } else
                    {
                        value = responseClas.value;
                        description = responseClas.description;
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
