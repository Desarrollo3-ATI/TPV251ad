using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using wsROMClases.Models.Panel;

namespace SyncTPV.Controllers
{
    public class FormasDePagoController
    {
        public static async Task<ExpandoObject> downloadAllFormasDePagoAPI()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    int lastId = 0;
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/DTFORMASPAGOVENTAS", Method.Get);
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        var jsonResp = JsonConvert.DeserializeObject<List<FormasDePagoModel>>(content);
                        List<FormasDePagoModel> fpList = (List<FormasDePagoModel>)jsonResp;
                        int itemsToEvaluate = fpList.Count;
                        if (lastId == 0)
                            FormasDePagoModel.deleteAllFormasDePago();
                        int count = FormasDePagoModel.saveAllFormasDePago(fpList);
                        if (count == itemsToEvaluate)
                            value = 1;
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        value = -400;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, responseHeader.Result.ErrorException.Message);
                    } else if (responseHeader.Result.ResponseStatus == ResponseStatus.TimedOut)
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

        public static async Task<ExpandoObject> downloadAllFormasDePagoLAN()
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
                    List<ClsFormasDePagoModel> fcVentasList = ClsFormasDePagoModel.getAllFormasDePagoVenta(panelInstance);
                    if (fcVentasList != null)
                    {
                        int itemsToEvaluate = fcVentasList.Count;
                        if (lastId == 0)
                            FormasDePagoModel.deleteAllFormasDePago();
                        int count = FormasDePagoModel.saveAllFormasDePagoLAN(fcVentasList);
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
