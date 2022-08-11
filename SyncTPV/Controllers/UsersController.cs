using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Threading;
using System.Threading.Tasks;

namespace SyncTPV.Controllers
{
    public class UsersController
    {

        public static async Task<ExpandoObject> getAllUsers()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () => {
                int value = 0;
                String description = "";
                try {
                    int lastId = 0;
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/getAllAgents", Method.Post);
                    request.AddJsonBody(new
                    {
                        lastId = lastId,
                        limit = 10000
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        int longitud = responseHeader.Result.Content.Length;
                        if (!responseHeader.Result.Content.Contains("value") || !responseHeader.Result.Content.Contains("No existen Agentes de ningún tipo"))
                        {
                            var jsonResp = JsonConvert.DeserializeObject<List<clsAgentes>>(responseHeader.Result.Content);
                            List<clsAgentes> jsonUsers = (List<clsAgentes>)jsonResp;
                            if (jsonUsers != null)
                            {
                                UserModel.deleteAllUsers();
                                lastId = UserModel.saveAllUsers(jsonUsers);
                                if (lastId > 0 && jsonUsers.Count > 0)
                                {
                                    value = 1;
                                    description = "Conexión extablecida correctamente";
                                }
                                else
                                {
                                    description = "No se pueron guardas algunos datos de usuarios";
                                }
                            }
                        } else
                        {
                            //valor = 3;
                            description = "No existe información que actualizar de usuarios";
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
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, responseHeader.Result.ErrorException.Message);
                    } else
                    {
                        value = -500;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, responseHeader.Result.ErrorException.Message);
                    }
                }
                catch (Exception ex)
                {
                    SECUDOC.writeLog(ex.ToString());
                    value = -1;
                    description = ex.Message;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> getAllUsersLAN()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () => {
                int value = 0;
                String description = "";
                int lastId = 0;
                try
                {
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    List<ClsAgentesModel> usersList = ClsAgentesModel.getAllAgents(panelInstance, lastId, 0);
                    if (usersList != null)
                    {
                        int itemsToEvaluate = usersList.Count;
                        int count = 0;
                        if (lastId == 0)
                            UserModel.deleteAllUsers();
                        count = UserModel.saveAllUsersLAN(usersList);
                        if (count == itemsToEvaluate)
                        {
                            value = 1;
                            description = "Conexión establecida correctamente";
                        } else
                        {
                            description = "No se pudieron guardar algunos datos de usuarios";
                        }
                    } else
                    {
                        description = "No hay datos que actualizar de usuarios";
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

        public static async Task<ExpandoObject> userLoginServerAPI(String username, String password)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    String link = ConfiguracionModel.getLinkWs();
                    link = link.Replace(" ", "%20");
                    var client = new RestClient(link);
                    var request = new RestRequest("/loginUser", Method.Post);
                    request.AddJsonBody(new
                    {
                        username = username,
                        password = password,
                        authenticatioToken = ""
                    });
                    var responseHeader = await client.PostAsync(request);
                    if (responseHeader.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Content; // Raw content as string
                        var jsonResp = JsonConvert.DeserializeObject<ResponseLogin>(content);
                        ResponseLogin jsonUsers = (ResponseLogin)jsonResp;
                        if (jsonUsers != null)
                        {
                            clsAgentes am = jsonUsers.agent;
                            if (am != null)
                            {
                                if (UserModel.userExist(Convert.ToInt32(am.USUARIO_ID)))
                                {
                                    bool updated = UserModel.updateAgent(am);
                                    if (updated)
                                    {
                                        value = Convert.ToInt32(am.USUARIO_ID);
                                        description = "";
                                    }
                                    else description = "Algo falló al actualizar agente";
                                } else
                                {
                                    bool created = UserModel.createAgent(am);
                                    if (created)
                                    {
                                        value = Convert.ToInt32(am.USUARIO_ID);
                                        description = "";
                                    }
                                    else description = "Algo falló al actualizar agente";
                                }
                            }
                            else description = "Usuario no encontrado\r\nProbablemente el usuario o contraseña sean incorrectos!";
                        }
                        else description = "La respuesta fue nula";
                    }
                    else if (responseHeader.ResponseStatus == ResponseStatus.Error)
                    {
                        value = -400;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, responseHeader.ErrorException.Message.ToString());
                    } else if (responseHeader.ResponseStatus == ResponseStatus.TimedOut)
                    {
                        value = -404;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, responseHeader.ErrorException.Message.ToString());
                    } else
                    {
                        value = -500;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, responseHeader.ErrorException.Message.ToString());
                    }
                }
                catch (Exception ex)
                {
                    SECUDOC.writeLog("Exception: " + ex.ToString());
                    value = -1;
                    description = ""+ex.Message;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> userLoginServerLAN(String username, String password)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    int idAgente = ClsAgentesModel.getIdAgentExist(panelInstance, username, password);
                    if (idAgente != 0)
                    {
                        ClsAgentesModel.updateConnectedAndLastLoginAndSeenValues(panelInstance, idAgente, 1,
                                MetodosGenerales.getCurrentDateAndHour(), MetodosGenerales.getCurrentDateAndHour());
                        ClsAgentesModel am = ClsAgentesModel.getAnAgent(panelInstance, username, password);
                        if (am != null)
                        {
                            if (UserModel.userExist(Convert.ToInt32(am.USUARIO_ID)))
                            {
                                bool updated = UserModel.updateAgent(am);
                                if (updated)
                                {
                                    value = Convert.ToInt32(am.USUARIO_ID);
                                    description = "";
                                }
                                else description = "Algo falló al actualizar agente";
                            } else
                            {
                                bool created = UserModel.createAgent(am);
                                if (created)
                                {
                                    value = Convert.ToInt32(am.USUARIO_ID);
                                    description = "";
                                }
                                else description = "Algo falló al actualizar agente";
                            }
                        }
                        else description = "Usuario No encontrado";
                    }
                    else description = "Usuario No encontrado\r\nProbablemente la contraseña o el usuario sean incorrectos";
                } catch (Exception ex)
                {
                    SECUDOC.writeLog(ex.ToString());
                    value = -1;
                    description = "" + ex.Message;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> userLogoutServerAPI(int identificador)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    String link = ConfiguracionModel.getLinkWs();
                    link = link.Replace(" ", "%20");
                    var client = new RestClient(link);
                    //var request = new RestRequest("/ValidaUsuario", Method.Get);
                    var request = new RestRequest("/logoutUser", Method.Post);
                    //request.Timeout = 4000;
                    request.AddJsonBody(new
                    {
                        identificadorAgente = identificador
                    });
                    var responseHeader = await client.ExecuteAsync(request);
                    if (responseHeader.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Content; // Raw content as string
                        var jsonResp = JsonConvert.DeserializeObject<ResponseLogin>(responseHeader.Content);
                        ResponseLogin jsonUsers = (ResponseLogin)jsonResp;
                        if (jsonUsers != null)
                        {
                            clsAgentes am = jsonUsers.agent;
                            if (am != null)
                            {
                                bool updated = UserModel.updateAgent(am);
                                if (updated)
                                {
                                    value = Convert.ToInt32(am.USUARIO_ID);
                                    description = "";
                                }
                                else description = "Algo falló al actualizar agente";
                            }
                            else description = "Usuario no encontrado";
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
                catch (Exception ex)
                {
                    SECUDOC.writeLog("Exception: " + ex.ToString());
                    value = -1;
                    description = "" + ex.Message;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> userLogoutServerLAN(int identificador)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    int idAgente = ClsAgentesModel.getIdAgentByIdentificador(panelInstance, identificador);
                    ClsAgentesModel.updateConnectedAndLastLoginAndSeenValues(panelInstance, idAgente, 0,
                                MetodosGenerales.getCurrentDateAndHour(), MetodosGenerales.getCurrentDateAndHour());
                    ClsAgentesModel agent = ClsAgentesModel.getAnAgentByIdAgent(panelInstance, idAgente);
                    if (agent != null)
                    {
                        bool updated = UserModel.updateAgent(agent);
                        if (updated)
                        {
                            value = Convert.ToInt32(agent.USUARIO_ID);
                            description = "";
                        }
                        else description = "Algo falló al actualizar agente";
                    }
                    else description = "Usuario No encontrado";
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

        public static async Task<ExpandoObject> updateLastSeenAPI(int identificador)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    String link = ConfiguracionModel.getLinkWs();
                    link = link.Replace(" ", "%20");
                    var client = new RestClient(link);
                    //var request = new RestRequest("/ValidaUsuario", Method.Get);
                    var request = new RestRequest("/updateLastSeen", Method.Post);
                    request.Timeout = 4000;
                    request.AddJsonBody(new
                    {
                        identificadorAgente = identificador
                    });
                    var responseHeader = await client.ExecuteAsync(request);
                    if (responseHeader.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Content; // Raw content as string
                        var jsonResp = JsonConvert.DeserializeObject<ResponseLastSeen>(responseHeader.Content);
                        ResponseLastSeen respLastSeen = (ResponseLastSeen)jsonResp;
                        if (respLastSeen != null)
                        {
                            if (respLastSeen.updated)
                            {
                                value = 1;
                                description = "Información actualizada";
                            }
                            else description = "No se pudo actualizar la información";
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
                catch (Exception ex)
                {
                    SECUDOC.writeLog("Exception: " + ex.ToString());
                    value = -1;
                    description = "" + ex.Message;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> updateLastSeenLAN(int identificador)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    int idAgente = ClsAgentesModel.getIdAgentByIdentificador(panelInstance, identificador);
                    bool updated = ClsAgentesModel.updateLastSeen(panelInstance, idAgente, MetodosGenerales.getCurrentDateAndHour());
                    if (updated)
                    {
                        value = 1;
                        description = "Información actualizada correctamente";
                    }
                    else description = "No se pudo actualizar la información";
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

        private class ResponseLogin
        {
            public clsAgentes agent { get; set; }
        }

        private class ResponseLastSeen
        {
            public bool updated { get; set; }
        }

    }

}
