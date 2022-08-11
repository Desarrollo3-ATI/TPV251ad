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
    public class ClsPromotionsController
    {
        public static async Task<ExpandoObject> downloadAllPromotionsAPI()
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
                    var request = new RestRequest("/DTPromociones", Method.Post);
                    request.AddJsonBody(new
                    {
                        lastId = 0,
                        limit = 1000
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        var jsonResp = JsonConvert.DeserializeObject<List<ClsPromocionesModel>>(content);
                        List<ClsPromocionesModel> promosList = (List<ClsPromocionesModel>)jsonResp;
                        if (promosList != null)
                        {
                            itemsToEvaluate = promosList.Count;
                            if (lastId == 0)
                                PromotionsModel.deleteAllPromotions();
                            int count = PromotionsModel.saveAllPromotions(promosList);
                            if (count == itemsToEvaluate)
                                value = 1;
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        value = -400;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, responseHeader.Result.ErrorException.Message);
                    } else if (responseHeader.Result.ResponseStatus == ResponseStatus.TimedOut)
                    {
                        value = -404;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, responseHeader.Result.ErrorException.Message);
                    }
                    else
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

        public static async Task<ExpandoObject> downloadAllPromotionsLAN()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    int lastId = 0;
                    String comInstance = InstanceSQLSEModel.getStringComInstance();
                    List<ClsPromocionesModel> promosList = ClsPromocionesModel.getAllPromos(comInstance, 0, 99999);
                    if (promosList != null)
                    {
                        int itemsToEvaluate = promosList.Count;
                        if (lastId == 0)
                            PromotionsModel.deleteAllPromotions();
                        int count = PromotionsModel.saveAllPromotionsLAN(promosList);
                        if (count == itemsToEvaluate)
                            value = 1;
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
