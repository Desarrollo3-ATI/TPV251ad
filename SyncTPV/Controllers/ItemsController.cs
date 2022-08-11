using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Controllers.Downloads;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Dynamic;
using System.Threading.Tasks;
using wsROMClase;
using wsROMClases.Controllers;
using wsROMClases.Models;
using wsROMClases.Models.Panel;

namespace SyncTPV.Controllers
{
    public class ItemsController
    {

        public static async Task<ExpandoObject> getAllItemsFromTheServerAPI(int downloadType, int lastId,
            int updateTheNewRecords, int limit, double descuentoMaximo, int almacenId, String codigoCaja)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    /*String codigoCaja = UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn());
                    if (codigoCaja.Equals(""))
                    {
                        codigoCaja = CajaModel.getCodeBox();
                    }*/
                    int itemsToEvaluate = 0;
                    do
                    {
                        String url = ConfiguracionModel.getLinkWs();
                        url = url.Replace(" ", "%20");
                        var client = new RestClient(url);
                        var request = new RestRequest("/getAllItems", Method.Post);
                        request.AddJsonBody(new
                        {
                            routeCode = codigoCaja,
                            lastId = lastId,
                            limit = limit,
                            descMaximo = descuentoMaximo,
                            almacenId = almacenId
                        });
                        var responseHeader = client.ExecuteAsync(request);
                        if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                        {
                            var content = responseHeader.Result.Content;
                            ResponseGetAllItems responseItems = JsonConvert.DeserializeObject<ResponseGetAllItems>(content);
                            if (responseItems.value == 1)
                            {
                                if (lastId == 0 && updateTheNewRecords == 0)
                                    ItemModel.deleteAllItemsInLocalDb();
                                if (downloadType == ClsInitialChargeController.INITIAL_CHARGE)
                                {
                                    lastId = ItemModel.saveItems(responseItems.itemsList);
                                    value = 1;
                                }
                                else
                                {
                                    var db = new SQLiteConnection();
                                    try
                                    {
                                        db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                                        db.Open();
                                        foreach (ClsItemModel item in responseItems.itemsList)
                                        {
                                            ItemModel.updateAItem(item, db);
                                            lastId = item.id;
                                        }
                                        value = 1;
                                    }
                                    catch (SQLiteException e)
                                    {
                                        SECUDOC.writeLog(e.ToString());
                                    }
                                    finally
                                    {
                                        if (db != null && db.State == ConnectionState.Open)
                                            db.Close();
                                    }
                                }
                                itemsToEvaluate = responseItems.itemsList.Count;
                            }
                            else
                            {
                                value = responseItems.value;
                                description = responseItems.description;
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
                        } else
                        {
                            value = -500;
                            description = MetodosGenerales.getErrorMessageFromNetworkCode(value,
                                responseHeader.Result.ErrorException.Message);
                        }
                    } while (itemsToEvaluate > (limit - 1) && value == 1);
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

        private class ResponseGetAllItems
        {
            public int value { get; set; }
            public String description { get; set; }
            public List<ClsItemModel> itemsList { get; set; }
        }

        public static async Task<ExpandoObject> getItemsFromTheServerAPI(int downloadType, int lastId,
            int updateTheNewRecords, int limit, double descuentoMaximo, int almacenId, String codigoCaja)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                List<ClsItemModel> itemsList = null;
                try
                {
                    /*String codigoCaja = UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn());
                    if (codigoCaja.Equals(""))
                    {
                        codigoCaja = CajaModel.getCodeBox();
                    }*/
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/getAllItems", Method.Post);
                    request.AddJsonBody(new
                    {
                        routeCode = codigoCaja,
                        lastId = lastId,
                        limit = limit,
                        descMaximo = descuentoMaximo,
                        almacenId = almacenId
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        ResponseGetAllItems responseItems = JsonConvert.DeserializeObject<ResponseGetAllItems>(content);
                        if (responseItems.value == 1)
                        {
                            itemsList = responseItems.itemsList;
                            saveItemsFromTheServer(itemsList, lastId, updateTheNewRecords, downloadType);
                            value = 1;
                        }
                        else
                        {
                            value = responseItems.value;
                            description = responseItems.description;
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
                    response.itemsList = itemsList;
                }
            });
            return response;
        }

        public static async Task saveItemsFromTheServer(List<ClsItemModel> itemsList, int lastId, int updateTheNewRecords, int downloadType)
        {
            await Task.Run(async () =>
            {
                if (lastId == 0 && updateTheNewRecords == 0)
                    ItemModel.deleteAllItemsInLocalDb();
                if (downloadType == ClsInitialChargeController.INITIAL_CHARGE)
                {
                    lastId = ItemModel.saveItems(itemsList);
                }
                else
                {
                    var db = new SQLiteConnection();
                    try
                    {
                        db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                        db.Open();
                        foreach (ClsItemModel item in itemsList)
                        {
                            ItemModel.updateAItem(item, db);
                            lastId = item.id;
                        }

                    }
                    catch (SQLiteException e)
                    {
                        SECUDOC.writeLog(e.ToString());
                    }
                    finally
                    {
                        if (db != null && db.State == ConnectionState.Open)
                            db.Close();
                    }
                }
            });
        }

        public static async Task<ExpandoObject> getTotalItemsAPI(int parameters, String parameterName, String parameterValue, int matchPosition,
            int consideredFields)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                int total = 0;
                try
                {
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/getTotalOfItems", Method.Post);
                    request.AddJsonBody(new
                    {
                        parameters = parameters,
                        parameterName = parameterName,
                        parameterValue = parameterValue,
                        matchPosition = matchPosition,
                        consideredFields = consideredFields
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content; // Raw content as string
                        var jsonResp = JsonConvert.DeserializeObject<int>(content);
                        total = (int)jsonResp;
                        value = 1;
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
                    response.total = total;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> getItemsFromTheServerWSWithParameters(int downloadType, int lastId, int updateTheNewRecords, int limit,
            String parameterName, String parameterValue, int matchPosition, int consideredFields, String codigoCaja)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                List<ClsItemModel> itemsList = null;
                try
                {
                    double descuentoMaximo = UserModel.getDescuentoMaximo(ClsRegeditController.getIdUserInTurn());
                    int almacenId = UserModel.getAlmacenIdFromTheUser(ClsRegeditController.getIdUserInTurn());
                    /*String codigoCaja = UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn());
                    if (codigoCaja.Equals(""))
                    {
                        codigoCaja = CajaModel.getCodeBox();
                    }*/
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/getItemsWithParameters", Method.Post);
                    request.AddJsonBody(new
                    {
                        routeCode = codigoCaja,
                        lastId = lastId,
                        limit = limit,
                        parameterName = parameterName,
                        parameterValue = parameterValue,
                        matchPosition = matchPosition,
                        consideredFields = consideredFields,
                        descMaximo = descuentoMaximo,
                        almacenId = almacenId
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        ResponseGetAllItems responseItems = JsonConvert.DeserializeObject<ResponseGetAllItems>(content);
                        if (responseItems.value == 1)
                        {
                            itemsList = responseItems.itemsList;
                            saveItemsFromTheServer(itemsList, lastId, updateTheNewRecords, downloadType);
                            value = 1;
                        }
                        else
                        {
                            value = responseItems.value;
                            description = responseItems.description;
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
                    response = -1;
                    description = e.Message;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                    response.itemsList = itemsList;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> getAllItemsFromTheServerLAN(int downloadType, int lastId, String codigoCaja, int idCaja)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                List<ClsItemModel> itemsList = null;
                try
                {
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    String comInstance = InstanceSQLSEModel.getStringComInstance();
                    /*String boxCode = UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn());
                    if (boxCode.Equals(""))
                    {
                        boxCode = CajaModel.getCodeBox();
                    }*/
                    double descuentoMaximo = UserModel.getDescuentoMaximo(ClsRegeditController.getIdUserInTurn());
                    int almacenId = UserModel.getAlmacenIdFromTheUser(ClsRegeditController.getIdUserInTurn());
                    dynamic responseConceptos = ClsConceptosController.getConceptCodeForCalculatePrices(panelInstance, codigoCaja);
                    if (responseConceptos.value == 1)
                    {
                        dynamic responseItems = ClsItemModel.getAllItemsTPV(comInstance, codigoCaja, lastId,
                            responseConceptos.concepto, 0, descuentoMaximo, almacenId);
                        if (responseItems.value == 1)
                        {
                            itemsList = responseItems.itemsList;
                            int itemsToEvaluate = itemsList.Count;
                            if (lastId == 0 && downloadType == ClsInitialChargeController.INITIAL_CHARGE)
                                ItemModel.deleteAllItemsInLocalDb();
                            if (downloadType == ClsInitialChargeController.INITIAL_CHARGE)
                                lastId = ItemModel.saveItemsLAN(itemsList);
                            else
                            {
                                var db = new SQLiteConnection();
                                try
                                {
                                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                                    db.Open();
                                    foreach (ClsItemModel item in itemsList)
                                    {
                                        ItemModel.updateAItemLAN(item, db);
                                    }
                                    value = 1;
                                }
                                catch (SQLiteException e)
                                {
                                    SECUDOC.writeLog(e.ToString());
                                }
                                finally
                                {
                                    if (db != null && db.State == ConnectionState.Open)
                                        db.Close();
                                }
                            }
                        } else
                        {
                            value = responseItems.value;
                            description = responseItems.description;
                        }
                    }
                    else
                    {
                        value = responseConceptos.value;
                        description = responseConceptos.description;
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

        public static async Task<ExpandoObject> getAnItemFromTheServerAPI(int itemId, FormItemDetails frmItemDetails, String codigoCaja)
        {
            dynamic response = new ExpandoObject();
            if (frmItemDetails != null)
                frmItemDetails.updateUIProgressBar(40, true);
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                ClsItemModel item = null;
                try
                {
                    int lastId = 0;
                    double descuentoMaximo = UserModel.getDescuentoMaximo(ClsRegeditController.getIdUserInTurn());
                    int almacenId = UserModel.getAlmacenIdFromTheUser(ClsRegeditController.getIdUserInTurn());
                    String url = ConfiguracionModel.getLinkWs();
                    /*String codigoCaja = UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn());
                    if (codigoCaja.Equals(""))
                    {
                        codigoCaja = CajaModel.getCodeBox();
                    }*/
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/getAnItem", Method.Post);
                    request.AddJsonBody(new
                    {
                        itemId = itemId,
                        routeCode = codigoCaja,
                        warehouseId = almacenId,
                        descMaximo = descuentoMaximo
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        ResponseGetAnItem responseItem = JsonConvert.DeserializeObject<ResponseGetAnItem>(content);
                        if (responseItem.value == 1)
                        {
                            item = responseItem.im;
                            saveOrUpdateAnItemData(item);
                            value = 1;
                        }
                        else
                        {
                            value = responseItem.value;
                            description = responseItem.description;
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
                    value = -1;
                    description = e.Message;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                    response.item = item;
                }
            });
            if (frmItemDetails != null)
                frmItemDetails.updateUIProgressBar(0, false);
            return response;
        }

        private class ResponseGetAnItem
        {
            public int value { get; set; }
            public String description { get; set; }
            public ClsItemModel im { get; set; }
        }

        private static async Task saveOrUpdateAnItemData(ClsItemModel itemModel)
        {
            await Task.Run(async () =>
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    if (ItemModel.checkIfRecordExist(db, "SELECT * FROM " + LocalDatabase.TABLA_ITEM + " WHERE " + LocalDatabase.CAMPO_ID_ITEM + " = " + itemModel.id))
                    {
                        ItemModel.updateAItem(itemModel, db);
                    }
                    else
                    {
                        ItemModel.saveAItem(itemModel, db);
                    }
                }
                catch (SQLiteException e)
                {
                    SECUDOC.writeLog(e.ToString());
                }
                finally
                {
                    if (db != null && db.State == ConnectionState.Open)
                        db.Close();
                }
            });
        }

        public static async Task<ExpandoObject> getAnItemFromTheServerLAN(int itemId, FormItemDetails frmItemDetails, String codigoCaja)
        {
            dynamic response = new ExpandoObject();
            if (frmItemDetails != null)
                await frmItemDetails.updateUIProgressBar(50, true);
            await Task.Run(() =>
            {
                int value = 0;
                String description = "";
                ClsItemModel item = null;
                try
                {
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    String comInstance = InstanceSQLSEModel.getStringComInstance();
                    double descuentoMaximo = UserModel.getDescuentoMaximo(ClsRegeditController.getIdUserInTurn());
                    int almacenId = UserModel.getAlmacenIdFromTheUser(ClsRegeditController.getIdUserInTurn());
                    /*String codigoCaja = UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn());
                    if (codigoCaja.Equals(""))
                    {
                        codigoCaja = CajaModel.getCodeBox();
                    }*/
                    int idCaja = ClsCajaModel.getIdByCodeBox(panelInstance, codigoCaja);
                    dynamic responseConceptos = ClsConceptosController.getConceptCodeForCalculatePrices(panelInstance, codigoCaja);
                    if (responseConceptos.value == 1)
                    {
                        dynamic responseItem = ClsItemModel.getAnItemTPV(comInstance, codigoCaja, itemId, responseConceptos.concepto,
                            descuentoMaximo, almacenId);
                        if (responseItem.value == 1)
                        {
                            value = 1;
                            item = responseItem.im;
                        }
                        else
                        {
                            value = responseItem.value;
                            description = responseItem.description;
                        }
                    }
                    else
                    {
                        value = responseConceptos.value;
                        description = responseConceptos.description;
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
                    response.item = item;
                }
            });
            if (frmItemDetails != null)
                frmItemDetails.updateUIProgressBar(0, false);
            return response;
        }

        public static async Task<ExpandoObject> getAnItemFromTheServerByCodeLAN(String itemCode, String codigoCaja)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                ClsItemModel item = null;
                try
                {
                    int lastId = 0;
                    int correctResponse = 0;
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    String comInstance = InstanceSQLSEModel.getStringComInstance();
                    /*String codigoCaja = UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn());
                    if (codigoCaja.Equals(""))
                    {
                        codigoCaja = CajaModel.getCodeBox();
                    }*/
                    int idCaja = ClsCajaModel.getIdByCodeBox(panelInstance, codigoCaja);
                    double descuentoMaximo = UserModel.getDescuentoMaximo(ClsRegeditController.getIdUserInTurn());
                    int almacenId = UserModel.getAlmacenIdFromTheUser(ClsRegeditController.getIdUserInTurn());
                    dynamic responseConceptos = ClsConceptosController.getConceptCodeForCalculatePrices(panelInstance, codigoCaja);
                    if (responseConceptos.value == 1)
                    {
                        dynamic responseItem = ClsItemModel.getAnItemByCodeTPV(comInstance, codigoCaja, itemCode, responseConceptos.concepto,
                            descuentoMaximo, almacenId);
                        if (responseItem.value == 1)
                        {
                            value = 1;
                            item = responseItem.im;
                        } else
                        {
                            value = responseItem.value;
                            description = responseItem.description;
                        }
                    }
                    else
                    {
                        value = responseConceptos.value;
                        description = responseConceptos.description;
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
                    response.item = item;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> getAnItemFromTheServerByCodeAPI(String itemCode, String codigoCaja)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                ClsItemModel item = null;
                try
                {
                    double descuentoMaximo = UserModel.getDescuentoMaximo(ClsRegeditController.getIdUserInTurn());
                    int almacenId = UserModel.getAlmacenIdFromTheUser(ClsRegeditController.getIdUserInTurn());
                    /*String codigoCaja = UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn());
                    if (codigoCaja.Equals(""))
                    {
                        codigoCaja = CajaModel.getCodeBox();
                    }*/
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/getAnItemByCode", Method.Post);
                    request.AddJsonBody(new
                    {
                        itemCode = itemCode,
                        warehouseId = almacenId,
                        routeOrCheckoutCode = codigoCaja,
                        descMaximo = descuentoMaximo
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        ResponseGetItemByCode responseItem = JsonConvert.DeserializeObject<ResponseGetItemByCode>(content);
                        if (responseItem.value == 1)
                        {
                            value = 1;
                            description = responseItem.description;
                            item = responseItem.im;
                        } else
                        {
                            value = responseItem.value;
                            description = responseItem.description;
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
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                    response.item = item;
                }
            });
            return response;
        }

        private class ResponseGetItemByCode
        {
            public int value { get; set; }
            public String description { get; set; }
            public ClsItemModel im { get; set; }
        }

        public static async Task<List<ClsPreciosEmpresaModel>> getPricesOfAnItem(ClsItemModel itemModel, int capturedUnitId, int listaDePrecio,
            bool serverModeLAN, String codigoCaja)
        {
            List<ClsPreciosEmpresaModel> pricesList = null;
            await Task.Run(async () =>
            {
                int capturedUnitIsMajor = 0;
                double conversionFactor = 0;
                if (serverModeLAN)
                {
                    dynamic responseCaptureUnit = await ConversionsUnitsController.checkIfTheCapturedUnitIsHigherLAN(itemModel.baseUnitId, capturedUnitId);
                    if (responseCaptureUnit.value == 1)
                        capturedUnitIsMajor = responseCaptureUnit.salesUnitIsHigher;
                }
                else
                {
                    capturedUnitIsMajor = ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(itemModel.baseUnitId, capturedUnitId);
                }
                double price = 0;
                List<ClsPreciosEmpresaModel> previousPriceList = await ItemModel.getAllPricesForAnItemAPILAN(itemModel, serverModeLAN, codigoCaja);
                if (previousPriceList != null)
                {
                    pricesList = new List<ClsPreciosEmpresaModel>();
                    for (int i = 0; i < previousPriceList.Count; i++)
                    {
                        ClsPreciosEmpresaModel pem = new ClsPreciosEmpresaModel();
                        pem.PRECIO_EMPRESA_ID = previousPriceList[i].PRECIO_EMPRESA_ID;
                        pem.NOMBRE = previousPriceList[i].NOMBRE;
                        if (capturedUnitIsMajor == 0)
                        {
                            price = previousPriceList[i].precioImporte;
                            if (serverModeLAN)
                            {
                                dynamic responseMajor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.baseUnitId, capturedUnitId, true);
                                if (responseMajor.value == 1)
                                    conversionFactor = responseMajor.majorFactor;
                            }
                            else
                            {
                                conversionFactor = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, true);
                            }
                            if (conversionFactor != 0)
                                price = price / conversionFactor;
                        }
                        else if (capturedUnitIsMajor == 1)
                        {
                            price = previousPriceList[i].precioImporte;
                            if (serverModeLAN)
                            {
                                dynamic responseMajor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.baseUnitId, capturedUnitId, true);
                                if (responseMajor.value == 1)
                                    conversionFactor = responseMajor.majorFactor;
                            }
                            else
                            {
                                conversionFactor = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, true);
                            }
                            if (conversionFactor != 0)
                                price = price * conversionFactor;
                        }
                        else if (capturedUnitIsMajor == 2)
                        {
                            price = previousPriceList[i].precioImporte;
                        }
                        else
                        {
                            price = previousPriceList[i].precioImporte;
                        }
                        pem.precioImporte = price;
                        pricesList.Add(pem);
                    }
                }
            });
            return pricesList;
        }

        public static async Task<ExpandoObject> getUnitsPendingsAPI(int itemId, String codigoCaja)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                double unidadesPendientes = 0;
                try
                {
                    /*String codigoCaja = UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn());
                    if (codigoCaja.Equals(""))
                    {
                        codigoCaja = CajaModel.getCodeBox();
                    }*/
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/getPendingUnitsForAnItem", Method.Post);
                    request.AddJsonBody(new
                    {
                        itemId = itemId,
                        warehouseId = UserModel.getAlmacenIdFromTheUser(ClsRegeditController.getIdUserInTurn()),
                        routeCode = codigoCaja
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content; // Raw content as string
                        ResponseUnidadesPendientes responseUnits = JsonConvert.DeserializeObject<ResponseUnidadesPendientes>(content);
                        if (responseUnits.value == 1)
                        {
                            value = 1;
                            unidadesPendientes = responseUnits.unidadesPendientes;
                        } else
                        {
                            value = responseUnits.value;
                            description = responseUnits.description;
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
                    response = -1;
                    description = e.Message;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                    response.unidadesPendientes = unidadesPendientes;
                }
            });
            return response;
        }

        private class ResponseUnidadesPendientes
        {
            public int value { get; set; }
            public String description { get; set; }
            public double unidadesPendientes { get; set; }
        }

        public static async Task<ExpandoObject> getUnitsPendingsLAN(int itemId)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                double unidadesPendientes = 0;
                try
                {
                    int warehouseId = UserModel.getAlmacenIdFromTheUser(ClsRegeditController.getIdUserInTurn());
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    dynamic responseUnits = ClsItemModel.getCapturedUnitsPending(panelInstance, itemId, warehouseId);
                    if (responseUnits.value == 1)
                    {
                        value = 1;
                        unidadesPendientes = responseUnits.pendingStock;
                    } else
                    {
                        value = responseUnits.value;
                        description = responseUnits.description;
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
                    response.unidadesPendientes = unidadesPendientes;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> getItemNameAPI(int itemId, String codigoCaja)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                String name = "";
                try
                {
                    /*String codigoCaja = UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn());
                    if (codigoCaja.Equals(""))
                    {
                        codigoCaja = CajaModel.getCodeBox();
                    }*/
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/getItemName", Method.Post);
                    request.AddJsonBody(new
                    {
                        itemId = itemId,
                        warehouseId = 0,
                        routeCode = codigoCaja
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        ResponseNameItem responseItem = JsonConvert.DeserializeObject<ResponseNameItem>(content);
                        if (responseItem.value == 1)
                        {
                            name = responseItem.name;
                            if (ItemModel.updateName(itemId, name))
                                value = 1;
                            else description = "No pudimos actualizar el nombre del producto en la base de datos local";
                        }
                        else {
                            value = responseItem.value;
                            description = responseItem.description;
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
                    response = -1;
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

        private class ResponseNameItem
        {
            public int value { get; set; }
            public String description { get; set; }
            public String name { get; set; }
        }

        public static async Task<ExpandoObject> getItemNameLAN(int itemId)
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
                    dynamic responseItem = ClsItemModel.getItemName(comInstance, itemId);
                    if (responseItem.value == 1)
                    {
                        value = 1;
                        name = responseItem.name;
                    } else
                    {
                        value = responseItem.value;
                        description = responseItem.description;
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
                    response.name = name;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> getAmountForAPriceAPI(int itemId, int listaDePrecio, String codigoCaja)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                double price = 0;
                try
                {
                    /*String codigoCaja = UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn());
                    if (codigoCaja.Equals(""))
                    {
                        codigoCaja = CajaModel.getCodeBox();
                    }*/
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/getAmountForAnItemPrice", Method.Post);
                    request.AddJsonBody(new
                    {
                        itemId = itemId,
                        warehouseId = listaDePrecio,
                        routeCode = codigoCaja
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        ResponseItemPrice responsePrice = JsonConvert.DeserializeObject<ResponseItemPrice>(content);
                        if (responsePrice.value == 1)
                        {
                            value = 1;
                            price = responsePrice.price;
                        }
                        else
                        {
                            value = responsePrice.value;
                            description = responsePrice.description;
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
                    response = -1;
                    description = e.Message;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                    response.price = price;
                }
            });
            return response;
        }

        private class ResponseItemPrice
        {
            public int value { get; set; }
            public String description { get; set; }
            public double price { get; set; }
        }

        public static async Task<ExpandoObject> getAmountForAPriceLAN(int itemId, int listaDePrecio, double imp1,
            double imp2, double imp3, int imp1Excento, int imp2Cuota, int imp3Cuota, double cantidadFiscal, 
            double reten1, double reten2, String codigoCaja)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                double price = 0;
                try
                {
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    String comInstance = InstanceSQLSEModel.getStringComInstance();
                    /*String codigoCaja = UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn());
                    if (codigoCaja.Equals(""))
                    {
                        codigoCaja = CajaModel.getCodeBox();
                    }*/
                    int idBox = ClsCajaModel.getIdByCodeBox(panelInstance, codigoCaja);
                    dynamic responseConceptos = ClsConceptosController.getConceptCodeForCalculatePrices(panelInstance, codigoCaja);
                    if (responseConceptos.value == 1)
                    {
                        dynamic responsePrice = ClsItemModel.getAmountForAPrice(comInstance, responseConceptos.concepto,
                            itemId, listaDePrecio, imp1, imp2, imp3, imp1Excento, imp2Cuota, imp3Cuota, cantidadFiscal,
                            reten1, reten2);
                        if (responsePrice.value == 1)
                        {
                            value = 1;
                            price = responsePrice.price;
                        } else
                        {
                            value = responsePrice.value;
                            description = responsePrice.description;
                        }
                    } else
                    {
                        value = responseConceptos.value;
                        description = responseConceptos.description;
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
                    response.price = price;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> getAllPricesForAnItemLAN(int itemId, double imp1, double imp2, double imp3,
            int imp1Excento, int imp2Cuota, int imp3Cuota, double cantidadFiscal, double reten1, double reten2, String codigoCaja)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                List<ClsPreciosEmpresaModel> pricesList = null;
                try
                {
                    String comInstance = InstanceSQLSEModel.getStringComInstance();
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    /*String codigoCaja = UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn());
                    if (codigoCaja.Equals(""))
                    {
                        codigoCaja = CajaModel.getCodeBox();
                    }*/
                    int idBox = ClsCajaModel.getIdByCodeBox(panelInstance, codigoCaja);
                    dynamic responseConceptos = ClsConceptosController.getConceptCodeForCalculatePrices(panelInstance, codigoCaja);
                    if (responseConceptos.value == 1)
                    {
                        dynamic responsePrices = ClsItemModel.getAllPricesEmpresaForAnItem(panelInstance, comInstance, 
                            responseConceptos.concepto, itemId, imp1, imp2, imp3, imp1Excento, imp2Cuota, imp3Cuota, 
                            cantidadFiscal, reten1, reten2);
                        if (responsePrices.value == 1)
                        {
                            value = 1;
                            pricesList = responsePrices.pricesList;
                        } else
                        {
                            value = responsePrices.value;
                            description = responsePrices.description;
                        }
                    } else
                    {
                        value = responseConceptos.value;
                        description = responseConceptos.description;
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
                    response.pricesList = pricesList;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> getImpuestoLAN(int itemId, int numImpuesto)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                double impuesto = 0;
                try
                {
                    String comInstance = InstanceSQLSEModel.getStringComInstance();
                    impuesto = ClsItemModel.getImpuesto(comInstance, itemId, numImpuesto);
                    value = 1;
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
                    response.impuesto = impuesto;
                }
            });
            return response;
        }

    }
}
