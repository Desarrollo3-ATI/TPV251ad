using Newtonsoft.Json;
using RestSharp;
using RestSharp.Extensions;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncTPV.Controllers
{
    public class SubirImagenesController
    {
        public static async Task<ExpandoObject> uploadImagenesViaLAN(String carpetaDest, String rutaLocal, int idModel)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(() => {
                int value = 0;
                String description = "";
                try
                {
                    String ipServer = InstanceSQLSEModel.getIPServerValue(2);
                    String rutaCarpetaDest = @"\\" + ipServer + "\\images\\" + carpetaDest;
                    if (!Directory.Exists(rutaCarpetaDest))
                        Directory.CreateDirectory(rutaCarpetaDest);
                    string nomenclatura = "";
                    if (carpetaDest.Equals("customers"))
                        nomenclatura = "-c";
                    else if (carpetaDest.Equals("items"))
                        nomenclatura = "-1";
                    //String rutaServerImage = @"\"+ipServer+ "\\Syncs\\SyncROM\\images\\" + carpetaDest + "\\"+ idMovel + nomenclatura+".jpg";
                    string sourceFile = Path.Combine(rutaLocal, idModel + nomenclatura + ".jpg");
                    string destFile = Path.Combine(rutaCarpetaDest, idModel + nomenclatura + ".jpg");
                    try
                    {
                        if (File.Exists(destFile))
                            File.Copy(sourceFile, destFile, true);
                        else File.Copy(sourceFile, destFile);
                        value = 1;
                    }
                    catch (IOException iox)
                    {
                        SECUDOC.writeLog(iox.ToString());
                        value = -1;
                        description = iox.Message;
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

        public static async Task<ExpandoObject> uploadImageAPI(String carpetaDest, String rutaLocal, int idModel)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () => {
                int value = 0;
                String description = "";
                try
                {
                    String carpetaLocal = "";
                    string nomenclatura = "";
                    if (carpetaDest == "customers")
                    {
                        nomenclatura = "-c";
                        carpetaLocal = "customers";
                    }
                    else if (carpetaDest == "items")
                    {
                        nomenclatura = "-1";
                        carpetaLocal = "items";
                    }
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    RestClient restClient = new RestClient(url);
                    RestRequest restRequest = new RestRequest("/insertImage");
                    restRequest.RequestFormat = DataFormat.Json;
                    restRequest.Method = Method.Post;
                    restRequest.AddHeader("Authorization", "Authorization");
                    restRequest.AddHeader("Content-Type", "multipart/form-data");
                    restRequest.AddFile("content", rutaLocal + "\\" + idModel +nomenclatura+".jpg");
                    var responseHeader = restClient.ExecuteAsync(restRequest);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var jsonResp = JsonConvert.DeserializeObject<dynamic>(responseHeader.Result.Content);
                        dynamic respJson = (dynamic)jsonResp;
                        if (respJson.value == 1)
                        {
                            value = 1;
                            description = respJson.description;
                        } else
                        {
                            description = respJson.description;
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        value = -404;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value,
                            responseHeader.Result.ErrorException.Message);
                    } else
                    {
                        value = -500;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value,
                            responseHeader.Result.ErrorException.Message);
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

        public static async Task<ExpandoObject> getImage(String nomenclatura, int idModel)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(() => {
                int value = 0;
                String description = "";
                try
                {
                    String nombreCarpeta = "";
                    if (nomenclatura.Equals("-1"))
                        nombreCarpeta = "items";
                    else if (nomenclatura.Equals("-c")) nombreCarpeta = "customers";
                    var path = MetodosGenerales.rootDirectory+"\\Imagenes\\"+nombreCarpeta + "\\" + idModel + nomenclatura + ".jpg";
                    if (!Directory.Exists(MetodosGenerales.rootDirectory + "\\Imagenes\\" + nombreCarpeta))
                        Directory.CreateDirectory(MetodosGenerales.rootDirectory + "\\Imagenes\\" + nombreCarpeta);
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/getImage", Method.Post);
                    request.AddBody(new
                    {
                        nomenclatura = nomenclatura,
                        idModel = idModel
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        ResponseGetImage responseImage = JsonConvert.DeserializeObject<ResponseGetImage>(content);
                        if (responseImage.value == 1)
                        {
                            File.WriteAllBytes(path, responseImage.image);
                            value = 1;
                        } else
                        {
                            value = responseImage.value;
                            description = responseImage.description;
                        }
                    } else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        value = -404;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value,
                            responseHeader.Result.ErrorException.Message);
                    } else
                    {
                        value = -500;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value,
                            responseHeader.Result.ErrorException.Message);
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

        private class ResponseGetImage
        {
            public int value { get; set; }
            public String description { get; set; }
            public byte[] image { get; set; }
        }

        public static async Task<ExpandoObject> getImageLAN(String carpetaDest, String rutaLocal, int idModel)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(() => {
                int value = 0;
                String description = "";
                try
                {
                    String ipServer = InstanceSQLSEModel.getIPServerValue(2);
                    String rutaCarpetaDest = @"\\" + ipServer + "\\images\\" + carpetaDest;
                    if (!Directory.Exists(rutaCarpetaDest))
                        Directory.CreateDirectory(rutaCarpetaDest);
                    string nomenclatura = "";
                    if (carpetaDest.Equals("customers"))
                        nomenclatura = "-c";
                    else if (carpetaDest.Equals("items"))
                        nomenclatura = "-1";
                    if (!Directory.Exists(rutaLocal))
                        Directory.CreateDirectory(rutaLocal);
                    string destFile = Path.Combine(rutaLocal, idModel + nomenclatura + ".jpg");
                    string sourceFile = Path.Combine(rutaCarpetaDest, idModel + nomenclatura + ".jpg");
                    try
                    {
                        if (File.Exists(sourceFile))
                        {
                            if (File.Exists(destFile))
                                File.Copy(sourceFile, destFile, true);
                            else File.Copy(sourceFile, destFile);
                        }
                        value = 1;
                    }
                    catch (IOException iox)
                    {
                        SECUDOC.writeLog(iox.ToString());
                        value = -1;
                        description = iox.Message;
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

    }
}
