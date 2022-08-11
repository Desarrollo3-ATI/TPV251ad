using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncTPV.Controllers
{
    public class ConfigurationsTpvController
    {

        public static async Task<ExpandoObject> createOrUpdateConfigTPVAPI()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    ConfiguracionModel cm = ConfiguracionModel.getConfiguration();
                    if (cm != null)
                    {
                        String link = ConfiguracionModel.getLinkWs();
                        link = link.Replace(" ", "%20");
                        var client = new RestClient(link);
                        //var request = new RestRequest("/ValidaUsuario", Method.Get);
                        var request = new RestRequest("/updateConfigurationTPV", Method.Post);
                        request.AddJsonBody(new
                        {
                            linkAPI = cm.linkAPI,
                            debugMode = cm.debugMode,
                            smsPermission = cm.smsPermission,
                            printPermission = cm.printPermission,
                            serverMode = cm.serverMode,
                            numCopias = cm.numCopias,
                            scalesPermission = cm.scalesPermission,
                            captureWeightsManually = cm.captureWeightsManually,
                            appType = 2,
                            rcId = ClsRegeditController.getIdUserInTurn(),
                            createdAt = MetodosGenerales.getCurrentDateAndHour(),
                            updatedAt = MetodosGenerales.getCurrentDateAndHour(),
                            enterpriseId = cm.enterpriseId
                        });
                        var responseHeader = await client.ExecuteAsync(request);
                        if (responseHeader.ResponseStatus == ResponseStatus.Completed)
                        {
                            var content = responseHeader.Content; // Raw content as string
                            var jsonResp = JsonConvert.DeserializeObject<ResponseConfigTPV>(content);
                            ResponseConfigTPV responseConfig = (ResponseConfigTPV)jsonResp;
                            if (responseConfig != null)
                            {
                                if (responseConfig.configId > 0)
                                {
                                    if (ConfiguracionModel.updateIdServer(responseConfig.configId))
                                    {
                                        value = 1;
                                        description = "Configuración actualizada";
                                    }
                                    else
                                    {
                                        description = "Algo falló al actualizar la configuración";
                                    }
                                }
                            }
                            else description = "La respuesta fue nula";
                        }
                        else if (responseHeader.ResponseStatus == ResponseStatus.Error)
                        {
                            if (responseHeader.ErrorMessage.Equals("Unable to connect to the remote server"))
                            {
                                value = -404;
                                description = "El servidor no estuvo disponible para la conexión";
                            }
                            else
                            {
                                value = -500;
                                description = "Algo falló al intentar establecer la conexión con el servidor";
                            }
                        }
                    }
                } catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    value = -1;
                    description = e.Message;
                } finally
                {
                    response.value = value;
                    response.descripton = description;
                }
            });
            return response;
        }

        private class ResponseConfigTPV
        {
            public int configId { get; set; }
        }

        public static async Task<bool> checkIfUseFiscalValueActivated()
        {
            bool activated = false;
            await Task.Run(async () =>
            {
                if (ConfiguracionModel.useFiscalFieldValueActivated())
                    activated = true;
            });
            return activated;
        }
    }
}
