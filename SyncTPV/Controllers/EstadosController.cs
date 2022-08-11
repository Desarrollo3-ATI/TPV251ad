using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using wsROMClases.Models;

namespace SyncTPV.Controllers
{
    public class EstadosController
    {
        public static async Task<ExpandoObject> downloadAllEstados()
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
                    var request = new RestRequest("/DTESTADOS", Method.Get);
                    request.AddParameter("ruta", "[TPV]" + UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn()));
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content; // Raw content as string
                        var jsonResp = JsonConvert.DeserializeObject<List<ClsEstadosModel>>(content);
                        List<ClsEstadosModel> estadosList = (List<ClsEstadosModel>)jsonResp;
                        if (estadosList != null)
                        {
                            if (lastId == 0)
                                EstadosModel.deleteAlEstados();
                            lastId = EstadosModel.saveAllEstados(estadosList);
                            itemsToEvaluate = estadosList.Count;
                            value = 1;
                        }
                        else
                        {
                            description = "No se encontró ningún Estado por descargar";
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
                } finally
                {
                    response.value = value;
                    response.description = description;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> downloadAllEstadosLAN()
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
                    String catsatInstance = InstanceSQLSEModel.getStringCATSATInstance();
                    String boxCode = UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn());
                    List<ClsEstadosModel> estadosList = ClsEstadosModel.getAllStates(catsatInstance, panelInstance, boxCode, true);
                    if (estadosList != null)
                    {
                        int count = 0;
                        int itemsToEvaluate = estadosList.Count;
                        if (lastId == 0)
                            EstadosModel.deleteAlEstados();
                        count = EstadosModel.saveAllEstadosLAN(estadosList);
                        if (count == itemsToEvaluate)
                            response = 1;
                    } else
                    {
                        description = "No se encontró ningún Estado por descargar";
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

        public static async Task<ExpandoObject> getAllEstadosLAN()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                List<ClsEstadosModel> estadosList = null;
                try
                {
                    int lastId = 0;
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    String catsatInstance = InstanceSQLSEModel.getStringCATSATInstance();
                    String boxCode = UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn());
                    estadosList = ClsEstadosModel.getAllStates(catsatInstance, panelInstance, boxCode, true);
                    if (estadosList != null)
                    {
                        value = 1;
                    }
                    else
                    {
                        description = "No se encontró ningún Estado por descargar";
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
                    response.estadosList = estadosList;
                }
            });
            return response;
        }

    }
}
