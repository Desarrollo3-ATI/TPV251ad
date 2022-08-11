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
    public class ClsClasificationsValueController
    {
        public static async Task<ExpandoObject> downloadAllClasificationsValueAPI()
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
                    var request = new RestRequest("/getAllCasificacionesValores", Method.Post);
                    request.AddJsonBody(new
                    {

                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        ResponseGetClasiValue responseClas = JsonConvert.DeserializeObject<ResponseGetClasiValue>(content);
                        if (responseClas.value == 1)
                        {
                            itemsToEvaluate = responseClas.clasificationsValueList.Count;
                            if (lastId == 0)
                                ClasificationsValueModel.deleteAllClasificationsValue();
                            int count = ClasificationsValueModel.saveAllClasificationsValue(responseClas.clasificationsValueList);
                            if (count == itemsToEvaluate)
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
                    } else
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

        private class ResponseGetClasiValue
        {
            public int value { get; set; }
            public String description { get; set; }
            public List<ClsClasificationsValue> clasificationsValueList { get; set; }
        }

        public static async Task<ExpandoObject> downloadAllClasificationsValueLAN()
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                try
                {
                    int lastId = 0;
                    String comInstance = InstanceSQLSEModel.getStringComInstance();
                    dynamic responseClas = ClsClasificacionesValoresModel.getAllValueClasifications(comInstance);
                    if (responseClas.value == 1)
                    {
                        int itemsToEvaluate = responseClas.clasificationsValueList.Count;
                        if (lastId == 0)
                            ClasificationsValueModel.deleteAllClasificationsValue();
                        int count = ClasificationsValueModel.saveAllClasificationsValueLAN(responseClas.clasificationsValueList);
                        if (count == itemsToEvaluate)
                            value = 1;
                        else description = "No se pudieron guardar todos los valores de clasificaciones en la base de datos local!";
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
