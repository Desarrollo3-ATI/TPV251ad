using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using wsROMClase;

namespace SyncTPV.Controllers
{
    public class UnitsOfMeasureAndWeightController
    {

        public static async Task<ExpandoObject> downloadAllUnitsOfMeasureAndWeightAPI()
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
                    var request = new RestRequest("/getUnitsOfMeasureAndWeight", Method.Post);
                    request.AddJsonBody(new
                    {
                        lastId = lastId
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        ResponseUnitsOfMeasureAndWeight responseUnits = JsonConvert.DeserializeObject<ResponseUnitsOfMeasureAndWeight>(content);
                        if (responseUnits.response.unitMeasureWeight != null)
                        {
                            if (lastId == 0)
                                UnitsOfMeasureAndWeightModel.deleteAllUnitsOfMeasureAndWeight();
                            lastId = UnitsOfMeasureAndWeightModel.saveAllUnitsOfMeasureAndWeight(responseUnits.response.unitMeasureWeight);
                            itemsToEvaluate = responseUnits.response.unitMeasureWeightCount;
                            value = 1;
                            description = "Datos Actualizados Correctamente";
                        } else
                        {
                            value = 1;
                            description = "No se encontró ninguna unidad de medida y peso!";
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        value = -400;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, responseHeader.Result.ErrorException.Message);
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.TimedOut)
                    {
                        value = -404;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, responseHeader.Result.ErrorException.Message);
                    }
                    else
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

        public static async Task<ExpandoObject> downloadAllUnitsOfMeasureAndWeightLAN()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    int lastId = 0;
                    String comInstance = InstanceSQLSEModel.getStringComInstance();
                    List<ClsUnitsMeasureWeightModel> cumwmList = ClsUnitsMeasureWeightModel.getAllUnitsOfMeasureAndWeight(comInstance, lastId);
                    if (cumwmList != null)
                    {
                        int itemsToEvaluate = cumwmList.Count;
                        if (lastId == 0)
                            UnitsOfMeasureAndWeightModel.deleteAllUnitsOfMeasureAndWeight();
                        lastId = UnitsOfMeasureAndWeightModel.saveAllUnitsOfMeasureAndWeightLAN(cumwmList);
                        value = 1;
                        description = "Datos Actualizados Correctamente";
                    }
                    else
                    {
                        value = 1;
                        description = "No hay datos que descargar";
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

        public static async Task<ExpandoObject> obtainUnitsEquivalentToBaseUnitAPI(int baseUnitId)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                List<ClsUnitsMeasureWeightModel> cumwmList = null;
                try
                {
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/obtainUnitsEquivalentToBaseUnit", Method.Post);
                    request.AddJsonBody(new
                    {
                        baseUnitId = baseUnitId,
                        capturedUnitId = value
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        var jsonResp = JsonConvert.DeserializeObject<List<ClsUnitsMeasureWeightModel>>(content);
                        cumwmList = (List<ClsUnitsMeasureWeightModel>)jsonResp;
                        if (cumwmList != null)
                        {
                            createOrUpdateMultiplesUnit(cumwmList);
                            value = 1;
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        if (responseHeader.Result.ErrorMessage.Equals("Unable to connect to the remote server"))
                        {
                            value = -404;
                            description = "Tiempo Excedido! Servidor No Encontrado" + responseHeader.Result.ErrorMessage;
                        }
                        else
                        {
                            value = -500;
                            description = "Algo falló! " + responseHeader.Result.ErrorMessage;
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
                    response.cumwmList = cumwmList;
                }
            });
            return response;
        }

        private static async Task createOrUpdateMultiplesUnit(List<ClsUnitsMeasureWeightModel> cumwmList)
        {
            await Task.Run(async () =>
            {
                if (cumwmList != null && cumwmList.Count > 0)
                {
                    foreach (var cumwm in cumwmList)
                    {
                        if (UnitsOfMeasureAndWeightModel.unitExist(cumwm.idServer))
                        {
                            UnitsOfMeasureAndWeightModel.updateUnitOfMeasureAndWeight(cumwm.idServer, cumwm.name, cumwm.abbreviation, cumwm.deployment,
                                cumwm.claveSAT, cumwm.claveComercioExterior);
                        }
                        else
                        {
                            int lastId = UnitsOfMeasureAndWeightModel.getLastId() + 1;
                            UnitsOfMeasureAndWeightModel.createUnitOfMeasureAndWeight(lastId, cumwm.idServer, cumwm.name, cumwm.abbreviation, cumwm.deployment,
                                cumwm.claveSAT, cumwm.claveComercioExterior);
                        }
                    }
                }
            });
        }

        public static async Task<ExpandoObject> obtainUnitsEquivalentToBaseUnitLAN(int baseUnitId)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                List<ClsUnitsMeasureWeightModel> cumwmList = null;
                try
                {
                    String comInstance = InstanceSQLSEModel.getStringComInstance();
                    cumwmList = ClsUnitsMeasureWeightModel.obtainUnitsEquivalentToBaseUnit(comInstance, baseUnitId);
                    if (cumwmList != null)
                    {
                        createOrUpdateMultiplesUnit(cumwmList);
                        value = 1;
                        description = "Datos Actualizados Correctamente";
                    }
                    else
                    {
                        value = 0;
                        description = "No hay datos que descargar";
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
                    response.cumwmList = cumwmList;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> getAUnidadLAN(int idUnidad)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                ClsUnitsMeasureWeightModel umw = null;
                try
                {
                    String comInstance = InstanceSQLSEModel.getStringComInstance();
                    umw = ClsUnitsMeasureWeightModel.getAUnidad(comInstance, idUnidad);
                    if (umw != null)
                    {
                        createOrUpdateUnit(umw.idServer, umw.name, umw.abbreviation, umw.deployment, umw.claveSAT, umw.claveComercioExterior);
                        value = 1;
                    }
                    else
                    {
                        value = 0;
                        description = "No hay datos que descargar";
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
                    response.umw = umw;
                }
            });
            return response;
        }

        private static async Task createOrUpdateUnit(int idServer, String name, String abbreviation, String deployment, String satKey, String foreignTradeKey)
        {
            await Task.Run(async () =>
            {
                if (UnitsOfMeasureAndWeightModel.unitExist(idServer))
                {
                    UnitsOfMeasureAndWeightModel.updateUnitOfMeasureAndWeight(idServer, name, abbreviation, deployment, satKey, foreignTradeKey);
                } else
                {
                    int lastId = UnitsOfMeasureAndWeightModel.getLastId() + 1;
                    UnitsOfMeasureAndWeightModel.createUnitOfMeasureAndWeight(lastId, idServer, name, abbreviation, deployment, satKey, foreignTradeKey);
                }
            });
        }

        public static async Task<ExpandoObject> getAUnidadAPI(int idUnidad)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                ClsUnitsMeasureWeightModel umw = null;
                try
                {
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/getAUnidad", Method.Post);
                    request.AddJsonBody(new
                    {
                        baseUnitId = idUnidad,
                        capturedUnitId = value
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        var jsonResp = JsonConvert.DeserializeObject<ClsUnitsMeasureWeightModel>(content);
                        umw = (ClsUnitsMeasureWeightModel)jsonResp;
                        if (umw != null)
                        {
                            createOrUpdateUnit(umw.idServer, umw.name, umw.abbreviation, umw.deployment, umw.claveSAT, umw.claveComercioExterior);
                            value = 1;
                        }
                        else
                        {
                            value = 0;
                            description = "No hay datos que descargar";
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        if (responseHeader.Result.ErrorMessage.Equals("Unable to connect to the remote server"))
                        {
                            value = -404;
                            description = "Tiempo Excedido! Servidor No Encontrado" + responseHeader.Result.ErrorMessage;
                        }
                        else
                        {
                            value = -500;
                            description = "Algo falló! " + responseHeader.Result.ErrorMessage;
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
                    response.umw = umw;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> getNameOfAndUnitMeasureWeightAPI(int idUnidad)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                String name = "";
                try
                {
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/getNameOfAndUnitMeasureWeight", Method.Post);
                    request.AddJsonBody(new
                    {
                        baseUnitId = idUnidad,
                        capturedUnitId = value
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        var jsonResp = JsonConvert.DeserializeObject<String>(content);
                        name = (String)jsonResp;
                        if (name != null && !name.Equals(""))
                            value = 1;
                        else
                        {
                            value = 0;
                            description = "No hay datos que descargar";
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        if (responseHeader.Result.ErrorMessage.Equals("Unable to connect to the remote server"))
                        {
                            value = -404;
                            description = "Tiempo Excedido! Servidor No Encontrado" + responseHeader.Result.ErrorMessage;
                        }
                        else
                        {
                            value = -500;
                            description = "Algo falló! " + responseHeader.Result.ErrorMessage;
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
                    response.name = name;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> getNameOfAndUnitMeasureWeightLAN(int idUnidad)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                String name = "";
                try
                {
                    String comInstance = InstanceSQLSEModel.getStringComInstance();
                    name = ClsUnitsMeasureWeightModel.getNameOfAndUnitMeasureWeight(comInstance, idUnidad);
                    if (name != null && !name.Equals(""))
                        value = 1;
                    else
                    {
                        value = 0;
                        description = "No hay datos que descargar";
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
                    response.name = name;
                }
            });
            return response;
        }

    }
}
