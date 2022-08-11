using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace SyncTPV.Controllers
{
    public class CotizacionesMostradorController
    {

        public static async Task<ExpandoObject> downloadAllCotizacionesMostrador()
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
                    var request = new RestRequest("/getAllCostizacionesMostrador", Method.Post);
                    request.AddJsonBody(new
                    {
                        lastId = lastId,
                        limit = 50
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var jsonResp = JsonConvert.DeserializeObject<ResponseCotizacionesMostrador>(responseHeader.Result.Content);
                        ResponseCotizacionesMostrador cotizacionesObject = (ResponseCotizacionesMostrador)jsonResp;
                        if (cotizacionesObject.cotizaciones != null && cotizacionesObject.cotizaciones.Count > 0)
                        {
                            if (lastId == 0)
                                PedidosEncabezadoModel.deleteAllCotizacionesMostrador();
                            lastId = await PedidosEncabezadoModel.saveAllCotizacionesMostrador(cotizacionesObject.cotizaciones);
                            itemsToEvaluate = cotizacionesObject.cotizaciones.Count;
                            value = 1;
                            description = "Datos descargados correctamente";
                        } else
                        {
                            description = "No se encontró ningún documento";
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

        public static async Task<ExpandoObject> downloadAllCotizacionesMostradorLAN()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                int lastId = 0;
                String description = "";
                try
                {
                    String comInstance = InstanceSQLSEModel.getStringComInstance();
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    List<ExpandoObject> cotizacionesList = ClsDocumentoModel.getAllCotizacionesMostrador(comInstance, panelInstance, lastId, 0);
                    if (cotizacionesList != null)
                    {
                        int itemsToEvaluate = cotizacionesList.Count;
                        if (lastId == 0)
                            PedidosEncabezadoModel.deleteAllCotizacionesMostrador();
                        lastId = await PedidosEncabezadoModel.saveAllCotizacionesMostradorLAN(cotizacionesList);
                        value = 1;
                        description = "Datos descargados correctamente";
                    }
                    else
                    {
                        description = "No hay cotizaciones";
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

        public static async Task<int> deleteCotizacionesMostrador(List<int> idsCotMos)
        {
            int response = 0;
            await Task.Run(async () =>
            {
                try
                {
                    int lastId = 0;
                    int itemsToEvaluate = 0;
                    int correctResponse = 0;
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    // client.Authenticator = new HttpBasicAuthenticator(username, password);
                    var request = new RestRequest("/deleteCotizacionMostrador", Method.Post);
                    request.AddJsonBody(new
                    {
                        ids = idsCotMos
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content; // Raw content as string
                        var jsonResp = JsonConvert.DeserializeObject<ResponseDeleteCM>(content);
                        ResponseDeleteCM idsDeleted = (ResponseDeleteCM)jsonResp;
                        int count = 0;
                        if (idsDeleted.response != null)
                        {
                            foreach (int id in idsDeleted.response)
                            {
                                String query = "DELETE FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " WHERE " + LocalDatabase.CAMPO_LISTO_PE + " = " + 2 + " AND " +
                                        LocalDatabase.CAMPO_DOCUMENTOID_PE + " = " + id;
                                if (PedidosEncabezadoModel.deleteARecord(query))
                                {
                                    String queryMoves = "DELETE FROM " + LocalDatabase.TABLA_PEDIDODETALLE + " WHERE " + LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_PD + " = " + id;
                                    if (PedidoDetalleModel.deleteARecord(queryMoves))
                                    {

                                    }
                                    else
                                    {

                                    }
                                    count++;
                                }
                            }
                        }
                        if (count == idsDeleted.response.Count)
                            correctResponse = 1;
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        if (responseHeader.Result.ErrorMessage.Equals("Unable to connect to the remote server"))
                        {
                            correctResponse = 404;
                        }
                        else
                        {
                            correctResponse = 404;
                        }
                    }
                    response = correctResponse;
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    response = -1;
                }
            });
            return response;
        }

        public static async Task<int> deleteCotizacionesMostradorLAN(List<int> idsCotMos)
        {
            int response = 0;
            await Task.Run(async () =>
            {
                try
                {
                    int lastId = 0;
                    int itemsToEvaluate = 0;
                    int correctResponse = 0;
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    List<int> idsDeleted = new List<int>(); ;
                    if (idsCotMos != null)
                    {
                        foreach (int id in idsCotMos)
                        {
                            String query = "DELETE FROM " + DbStructure.RomDb.TABLA_DOCUMENTO + " WHERE " + DbStructure.RomDb.CAMPO_TIPO_DOCUMENTO + " = " + 51 +
                                " AND " + DbStructure.RomDb.CAMPO_CREADO_DOCUMENTO + " = '5' AND " + DbStructure.RomDb.CAMPO_ID_DOCUMENTO + " = " + id;
                            if (ClsDocumentoModel.deleteARecord(panelInstance, query))
                                idsDeleted.Add(id);
                        }
                    }
                    int count = 0;
                    if (idsDeleted != null)
                    {
                        foreach (int id in idsDeleted)
                        {
                            String query = "DELETE FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " WHERE " + LocalDatabase.CAMPO_LISTO_PE + " = " + 2 + " AND " +
                                    LocalDatabase.CAMPO_DOCUMENTOID_PE + " = " + id;
                            if (PedidosEncabezadoModel.deleteARecord(query))
                            {
                                String queryMoves = "DELETE FROM " + LocalDatabase.TABLA_PEDIDODETALLE + " WHERE " + LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_PD + " = " + id;
                                if (PedidoDetalleModel.deleteARecord(queryMoves))
                                {

                                }
                                else
                                {

                                }
                                count++;
                            }
                        }
                    }
                    if (count == idsDeleted.Count)
                        correctResponse = 1;
                    response = correctResponse;
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    response = -1;
                }
            });
            return response;
        }

    }
}
