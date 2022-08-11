using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wsROMClases.Models.Panel;

namespace SyncTPV.Controllers
{
    public class DirectoriosController
    {

        public static async Task<ExpandoObject> getDirectoriesRoutesWs(int tipo)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () => {
                int error = 0;
                String errorMessage = "";
                try
                {
                    int lastId = 0;
                    int itemsToEvaluate = 0;
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    // client.Authenticator = new HttpBasicAuthenticator(username, password);
                    var request = new RestRequest("/getDirectoryByType", Method.Post);
                    request.AddJsonBody(new
                    {
                        tipo = tipo
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content; // Raw content as string
                        //var responseHttp = client.Post<String>(request);
                        var jsonResp = JsonConvert.DeserializeObject<ResponseDirectory> (content);
                        ResponseDirectory responseDirectorio = (ResponseDirectory)jsonResp;
                        if (responseDirectorio.directorio != null)
                        {
                            DirectoriosModel dm = responseDirectorio.directorio;
                            int records = DirectoriosModel.validateIfDirectoryTypeExist(tipo);
                            if (records == 1)
                            {
                                if (DirectoriosModel.updateDirectory(dm.id, dm.tipo, dm.nombre, dm.ruta, dm.empresaId))
                                    error = dm.id;
                            }  else if (records > 1)
                            {
                                if (DirectoriosModel.deleteDirectoryByType(tipo))
                                {
                                    if (DirectoriosModel.validateIfDirectoryIdExist(dm.id))
                                        dm.id = DirectoriosModel.getLastId() + 1;
                                    error = DirectoriosModel.createDirectory(dm.id, dm.tipo, dm.nombre, dm.ruta, dm.empresaId);
                                }
                            }
                            else {
                                if (DirectoriosModel.validateIfDirectoryIdExist(dm.id))
                                    dm.id = DirectoriosModel.getLastId() + 1;
                                error = DirectoriosModel.createDirectory(dm.id, dm.tipo, dm.nombre, dm.ruta, dm.empresaId);
                            }
                        } else
                        {
                            if (DirectoriosModel.validateIfDirectoryTypeExist(tipo) > 0)
                            {
                                if (DirectoriosModel.deleteDirectoryByType(tipo))
                                    error = 0;
                            }
                            errorMessage = ""+responseDirectorio.description;
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        if (responseHeader.Result.ErrorMessage.Equals("Unable to connect to the remote server"))
                        {
                            error = -404;
                            errorMessage += "Tiempo Excedido, Servidor No Encontrado " + responseHeader.Result.ErrorMessage;
                        }
                        else
                        {
                            error = -500;
                            errorMessage += "Algo falló en el servidor " + responseHeader.Result.ErrorMessage;
                        }
                    }
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    error = -1;
                    errorMessage = "" + e.Message; ;
                }
                finally
                {
                    response.valor = error;
                    response.description = errorMessage;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> getDirectoriesRoutesLAN(int tipo)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async() =>
            {
                int error = 0;
                String errorMessage = "";
                try
                {
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    ClsDirectoriosModel dm = ClsDirectoriosModel.getDirectoryBytType(panelInstance, tipo);
                    if (dm != null)
                    {
                        int records = DirectoriosModel.validateIfDirectoryTypeExist(tipo);
                        if (records == 1)
                        {
                            if (DirectoriosModel.updateDirectory(dm.id, dm.tipo, dm.nombre, dm.ruta, dm.empresaId))
                                error = dm.id;
                        } else if (records > 1)
                        {
                            if (DirectoriosModel.deleteDirectoryByType(tipo))
                            {
                                if (DirectoriosModel.validateIfDirectoryIdExist(dm.id))
                                    dm.id = DirectoriosModel.getLastId() + 1;
                                error = DirectoriosModel.createDirectory(dm.id, dm.tipo, dm.nombre, dm.ruta, dm.empresaId);
                            }
                        }
                        else
                        {
                            if (DirectoriosModel.validateIfDirectoryIdExist(dm.id))
                                dm.id = DirectoriosModel.getLastId() + 1;
                            error = DirectoriosModel.createDirectory(dm.id, dm.tipo, dm.nombre, dm.ruta, dm.empresaId);
                        }
                        errorMessage = "Ruta del directorio encontrado correctamente";
                    }
                    else {
                        int records = DirectoriosModel.validateIfDirectoryTypeExist(tipo);
                        if (records > 0)
                        {
                            if (DirectoriosModel.deleteDirectoryByType(tipo))
                                error = 0;
                        }
                        errorMessage = "Datos de la ruta del directorio No encontrados\r\n";
                    }
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    error = -1;
                    errorMessage = "Exception: " + e.Message;
                }
                finally
                {
                    response.valor = error;
                    response.description = errorMessage;
                }
            });
            return response;
        }

        public class ResponseDirectory
        {
            public DirectoriosModel directorio { get; set; }
            public String description { get; set; }
        }
    }
}
