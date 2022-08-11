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
    public class PanelConfigurationController
    {

        public static async Task<ExpandoObject> getPanelConfigurationAPI()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                ClsConfiguracionModel cm = null;
                try
                {
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/getConfiguration", Method.Post);
                    request.AddJsonBody(new
                    {
                        idConfiguration = 1
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        ResponsePanelConfig responseConfig = JsonConvert.DeserializeObject<ResponsePanelConfig>(content);
                        if (responseConfig.value == 1)
                        {
                            value = 1;
                            cm = responseConfig.cm;
                        }
                        else
                        {
                            value = responseConfig.value;
                            description = responseConfig.description;
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        value = -400;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value,
                            responseHeader.Result.ErrorException.Message);
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.TimedOut)
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
                    response = -1;
                    description = e.Message;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                    response.cm = cm;
                }
            });
            return response;
        }

        private class ResponsePanelConfig
        {
            public int value { get; set; }
            public String description { get; set; }
            public ClsConfiguracionModel cm { get; set; }
        }

        public static async Task<ExpandoObject> getPanelConfigurationLAN()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                ClsConfiguracionModel cm = null;
                try
                {
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    dynamic responseConfig = ClsConfiguracionModel.getPanelConfiguration(panelInstance, 1);
                    if (responseConfig.value == 1)
                    {
                        value = 1;
                        cm = responseConfig.cm;
                    }
                    else
                    {
                        value = responseConfig.value;
                        description = responseConfig.description;
                    }
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    response = -1;
                    description = e.Message;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                    response.cm = cm;
                }
            });
            return response;
        }

    }
}
