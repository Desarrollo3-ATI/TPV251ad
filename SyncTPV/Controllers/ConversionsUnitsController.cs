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
    public class ConversionsUnitsController
    {
        public static async Task<ExpandoObject> downloadAllConversionsUnitsAPI()
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
                    var request = new RestRequest("/getUnitConversions", Method.Post);
                    request.AddJsonBody(new
                    {
                        lastId = lastId
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        ResponseConversionsUnits conversionsUnits = JsonConvert.DeserializeObject<ResponseConversionsUnits>(content);
                        if (conversionsUnits.response.unitConversions != null && conversionsUnits.response.unitConversions.Count > 0)
                        {
                            if (lastId == 0)
                                ConversionsUnitsModel.deleteAllConversionsUnits();
                            lastId = ConversionsUnitsModel.saveAllConversionsUnits(conversionsUnits.response.unitConversions);
                            itemsToEvaluate = conversionsUnits.response.unitConversionsCount;
                            value = 1;
                            description = "Datos descargador y procesados correctamente";
                        }
                        else
                        {
                            value = 1;
                            description = "No encontramos ninguna conversión de unidades!";
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

        public static async Task<ExpandoObject> downloadAllConversionsUnitsLAN()
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
                    List<ClsUnitConversionsModel> cucmList = ClsUnitConversionsModel.getAllUnitConversions(comInstance, lastId);
                    if (cucmList != null && cucmList.Count > 0)
                    {
                        int itemsToEvaluate = cucmList.Count;
                        if (lastId == 0)
                            ConversionsUnitsModel.deleteAllConversionsUnits();
                        lastId = ConversionsUnitsModel.saveAllConversionsUnitsLAN(cucmList);
                        value = 1;
                        description = "Datos descargador y procesados correctamente";
                    }
                    else
                    {
                        value = 1;
                        description = "No encontramos niguna conversión de unidades!";
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

        public static async Task<ExpandoObject> checkIfTheCapturedUnitIsHigherAPI(int baseUnitId, int capturedUnitId)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                int salesUnitIsHigher = 0;
                try
                {
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    // client.Authenticator = new HttpBasicAuthenticator(username, password);
                    var request = new RestRequest("/checkIfTheCapturedUnitIsHigher", Method.Post);
                    request.AddJsonBody(new
                    {
                        baseUnitId = baseUnitId,
                        capturedUnitId = capturedUnitId
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        var jsonResp = JsonConvert.DeserializeObject<int>(content);
                        salesUnitIsHigher = (int)jsonResp;
                        value = 1;
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
                            description = "Algo falló al establecer conexión con el servidor! " + responseHeader.Result.ErrorMessage;
                        }
                    }
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    value = -1;
                    description = "Oops ocurrió una excepción al procesar la respuesta! " + e.ToString();
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                    response.salesUnitIsHigher = salesUnitIsHigher;
                }
            });
            return response;
        }
        public static async Task<ExpandoObject> checkIfTheCapturedUnitIsHigherLAN(int baseUnitId, int capturedUnitId)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                int salesUnitIsHigher = 0;
                try
                {
                    String comInstance = InstanceSQLSEModel.getStringComInstance();
                    salesUnitIsHigher = ClsUnitConversionsModel.checkIfTheCapturedUnitIsHigher(comInstance, baseUnitId, capturedUnitId);
                    if (salesUnitIsHigher >= 0)
                    {
                        value = 1;
                        description = "Datos descargador y procesados correctamente";
                    }
                    else
                    {
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
                    response.salesUnitIsHigher = salesUnitIsHigher;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> getMajorOrMinorConversionFactorFromAnItemAPI(int unitOne, int unitTwo, bool getMajor)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                double majorFactor = 0;
                try
                {
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    // client.Authenticator = new HttpBasicAuthenticator(username, password);
                    var request = new RestRequest("/getMajorOrMinorConversionFactorFromAnItem", Method.Post);
                    request.AddJsonBody(new
                    {
                        unitOne = unitOne,
                        unitTwo = unitTwo,
                        getMajor = getMajor
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        var jsonResp = JsonConvert.DeserializeObject<double>(content);
                        majorFactor = (double)jsonResp;
                        value = 1;
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
                            description = "Algo falló al establecer conexión con el servidor! " + responseHeader.Result.ErrorMessage;
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
                    response.majorFactor = majorFactor;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> getMajorOrMinorConversionFactorFromAnItemLAN(int unitOne, int unitTwo, bool getMajor)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                double majorFactor = 0;
                try
                {
                    String comInstance = InstanceSQLSEModel.getStringComInstance();
                    majorFactor = ClsUnitConversionsModel.getMajorOrMinorConversionFactorFromAnItem(comInstance, unitOne, unitTwo, getMajor);
                    value = 1;
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
                    response.majorFactor = majorFactor;
                }
            });
            return response;
        }

    }
}
