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
    public class CajaController
    {

        public static async Task<ExpandoObject> cajasExistAPI()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {                    
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/cajaExist", Method.Get);
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        var jsonResp = JsonConvert.DeserializeObject<ResponseCajaExist>(content);
                        ResponseCajaExist cajas = (ResponseCajaExist)jsonResp;
                        if (cajas.cajasTotales > 0)
                        {
                            value = cajas.cajasTotales;
                            description = "Cajas encontradas";
                        }
                        else
                        {
                            description = "Tienes que crear al menos una Caja en el PanelROM > Módulo Rutas/Cajas";
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

        private class ResponseCajaExist
        {
            public int cajasTotales { get; set; }
        }

        public static async Task<ExpandoObject> cajasExistLAN()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    int cajasTotales = ClsCajaModel.getCajasTotales(panelInstance);
                    if (cajasTotales > 0)
                    {
                        value = cajasTotales;
                        description = "Cajas encontradas";
                    }
                    else
                    {
                        description = "Tienes que crear al menos una Caja en el PanelROM > Módulo Rutas/Cajas";
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

        public static async Task<ExpandoObject> getAllCajasAPI()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                List<ClsCajaModel> cajasList = null;
                try
                {
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/getAllCheckouts", Method.Post);
                    request.AddJsonBody(new
                    {
                        lastId = 0,
                        limit = 0
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        var jsonResp = JsonConvert.DeserializeObject<ResponseGetAllCajas>(content);
                        ResponseGetAllCajas responseCaja = (ResponseGetAllCajas)jsonResp;
                        if (responseCaja.value > 0)
                        {
                            value = responseCaja.value;
                            description = responseCaja.description;
                            cajasList = responseCaja.cajasList;
                            CajaModel.deleteAllCajas();
                            createOrUpdateCajas(cajasList);
                        } else
                        {
                            CajaModel.deleteAllCajas();
                            description = "No se encontró ninguna caja";
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        value = -400;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, responseHeader.Result.ErrorException.Message.ToString());
                    } else if (responseHeader.Result.ResponseStatus == ResponseStatus.TimedOut)
                    {
                        value = -404;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, responseHeader.Result.ErrorException.Message.ToString());
                    } else
                    {
                        value = -500;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, responseHeader.Result.ErrorException.Message.ToString());
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
                    response.cajasList = cajasList;
                }
            });
            return response;
        }

        private class ResponseGetAllCajas
        {
            public int value { get; set; }
            public String description { get; set; }
            public int totalCajas { get; set; }
            public List<ClsCajaModel> cajasList { get; set; }
        }

        public static async Task<ExpandoObject> getAllCajasLAN()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                List<ClsCajaModel> cajasList = null;
                try
                {
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    cajasList = ClsCajaModel.getAllCajas(panelInstance, 0,0);
                    CajaModel.deleteAllCajas();
                    createOrUpdateCajas(cajasList);
                    value = 1;
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
                    response.cajasList = cajasList;
                }
            });
            return response;
        }

        private static async Task createOrUpdateCajas(List<ClsCajaModel> cajasList)
        {
            await Task.Run(async () =>
            {
                if (cajasList != null)
                {
                    foreach (ClsCajaModel caja in cajasList)
                    {
                        if (CajaModel.checkIfCajaExist(caja.id))
                        {
                            CajaModel.updateCaja(caja.id, caja.code, caja.name, caja.warehouseId, caja.createdAt);
                        } else
                        {
                            CajaModel.createCaja(caja.id, caja.code, caja.name, caja.warehouseId, caja.createdAt);
                        }
                    }
                }
            });
        }

    }
}
