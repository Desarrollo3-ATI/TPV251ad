using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace SyncTPV.Controllers
{
    public class DatosTicketController
    {
        public static async Task<ExpandoObject> downloadAllDatosTicketAPI()
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
                    var request = new RestRequest("/DatosTicket", Method.Get);
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        var jsonResp = JsonConvert.DeserializeObject<List<DatosTicketModel>>(content);
                        List<DatosTicketModel> dtList = (List<DatosTicketModel>)jsonResp;
                        if (dtList != null && dtList.Count > 0)
                        {
                            if (lastId == 0)
                                DatosTicketModel.deleteAllDatosTicket();
                            lastId = DatosTicketModel.saveAllDatosTicket(dtList);
                            value = 1;
                        } else
                        {
                            description = "No se encontró la información de la empresa y ticket";
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
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> downloadAllDatosTicketLAN()
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
                    ClsDatosTicketModel datosTicket = ClsDatosTicketModel.getDatosTicket(panelInstance);
                    if (datosTicket != null)
                    {
                        if (lastId == 0)
                            DatosTicketModel.deleteAllDatosTicket();
                        lastId = DatosTicketModel.saveAllDatosTicketLAN(datosTicket);
                        value = 1;
                    } else
                    {
                        description = "No se encontró la información de la empresa y ticket";
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

    }
}
