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
    public class PreciosEmpresaController
    {
        public static async Task<ExpandoObject> downloadAllPreciosEmpresaAPI()
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
                    var request = new RestRequest("/DTPRECIOSEMPRESA", Method.Get);
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        var jsonResp = JsonConvert.DeserializeObject<List<ClsPreciosEmpresaModel>>(content);
                        List<ClsPreciosEmpresaModel> preciosEmpresaList = (List<ClsPreciosEmpresaModel>)jsonResp;
                        if (preciosEmpresaList != null && preciosEmpresaList.Count > 0)
                        {
                            itemsToEvaluate = preciosEmpresaList.Count;
                            if (lastId == 0)
                                PreciosempresaModel.deleteAllPreciosEmpresa();
                            int count = PreciosempresaModel.saveAllPreciosEmpresa(preciosEmpresaList);
                            if (count == itemsToEvaluate)
                                value = 1;
                            if (itemsToEvaluate > 0)
                                lastId = preciosEmpresaList[preciosEmpresaList.Count - 1].PRECIO_EMPRESA_ID;
                        }
                        else
                        {
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

        public static async Task<ExpandoObject> downloadAllPreciosEmpresaLAN()
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            await Task.Run(async () =>
            {
                String description = "";
                try
                {
                    int lastId = 0;
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    List<ClsPreciosEmpresaModel> preciosEmpresaList = ClsPreciosEmpresaModel.getAllPreciosEmpresaActivated(panelInstance);
                    if (preciosEmpresaList != null && preciosEmpresaList.Count > 0)
                    {
                        int itemsToEvaluate = preciosEmpresaList.Count;
                        if (lastId == 0)
                            PreciosempresaModel.deleteAllPreciosEmpresa();
                        int count = PreciosempresaModel.saveAllPreciosEmpresaLAN(preciosEmpresaList);
                        if (count == itemsToEvaluate)
                            value = 1;
                    }
                    else
                    {
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
