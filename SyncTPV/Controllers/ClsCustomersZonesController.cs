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
    public class ClsCustomersZonesController
    {
        public static async Task<ExpandoObject> downloadAllCustomersZones()
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
                    var request = new RestRequest("/DTZONASCLIENTES", Method.Get);
                    request.AddParameter("RUTA", "[TPV]" + UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn()));
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content; // Raw content as string
                        var jsonResp = JsonConvert.DeserializeObject<List<ZonasDeClientesModel>>(content);
                        List<ZonasDeClientesModel> zonesList = (List<ZonasDeClientesModel>)jsonResp;
                        itemsToEvaluate = zonesList.Count;
                        if (lastId == 0)
                            ZonasDeClientesModel.deleteAllCustomersZones();
                        int count = ZonasDeClientesModel.saveAllZones(zonesList);
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

        public static async Task<ExpandoObject> downloadAllCustomersZonesLAN()
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
                    List<ClsZonasDeClientesModel> zonesList = ClsZonasDeClientesModel.getallZonesCustomers(comInstance);
                    if (zonesList != null)
                    {
                        int itemsToEvaluate = zonesList.Count;
                        if (lastId == 0)
                            ZonasDeClientesModel.deleteAllCustomersZones();
                        int count = ZonasDeClientesModel.saveAllZonesLAN(zonesList);
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
