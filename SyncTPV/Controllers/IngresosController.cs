using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wsROMClases.Models;

namespace SyncTPV.Controllers
{
    public class IngresosController
    {

        public static async Task<ExpandoObject> uploadIngresosLAN()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    List<ClsIngresosModel> ingresosList = IngresoModel.getAllNotSendedToTheServer();
                    List<ExpandoObject> responseRetiros = ClsIngresosModel.saveEntriesAlongWithTheirAmounts(panelInstance, ingresosList);
                    if (responseRetiros != null)
                    {
                        for (int i = 0; i < responseRetiros.Count; i++)
                        {
                            dynamic wr = responseRetiros[i];
                            int idRetiroApp = wr.idIngresoApp;
                            int idRetiroServer = wr.idIngresoServer;
                            IngresoModel.updateServerIdInAnEntry(idRetiroApp, idRetiroServer);
                            MontoIngresoModel.updateSentFieldOfEntryAmounts(idRetiroApp);
                        }
                        value = 1;
                    } else
                    {
                        description = "La respuesta del servidor fue nula";
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

        public static async Task<ExpandoObject> uploadIngresosAPI()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    List<ClsIngresosModel> ingresosList = IngresoModel.getAllNotSendedToTheServer();
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/insertarIngresos", Method.Post);
                    request.AddJsonBody(new
                    {
                        retiros = ingresosList
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content; // Raw content as string
                        var jsonResp = JsonConvert.DeserializeObject<ResponseIngreso>(content);
                        ResponseIngreso respJson = (ResponseIngreso)jsonResp;
                        for (int i = 0; i < respJson.response.Count; i++)
                        {
                            IngresoResponse wr = respJson.response[i];
                            int idRetiroApp = wr.idIngresoApp;
                            int idRetiroServer = wr.idIngresoServer;
                            IngresoModel.updateServerIdInAnEntry(idRetiroApp, idRetiroServer);
                            MontoIngresoModel.updateSentFieldOfEntryAmounts(idRetiroApp);
                        }
                        value = 1;
                        description = "Proceso Finalizado";
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        if (responseHeader.Result.ErrorMessage.Equals("Unable to connect to the remote server"))
                        {
                            value = -404;
                            description = "Proceso Finalizado";
                        }
                        else
                        {
                            value = -500;
                            description = "Proceso Finalizado";
                        }
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


        private class ResponseIngreso
        {
            public List<IngresoResponse> response { get; set; }
        }

        private class IngresoResponse
        {
            public int idIngresoApp { get; set; }
            public int idIngresoServer { get; set; }
        }

    }
}
