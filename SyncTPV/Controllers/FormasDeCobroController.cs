using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Controllers.Downloads;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Dynamic;
using System.Threading.Tasks;
using wsROMClases.Models.Panel;

namespace SyncTPV.Controllers
{
    public class FormasDeCobroController
    {
        public static async Task<ExpandoObject> downloadAllFormasDeCobroAPI(int downloadType)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    int lastId = 0;
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/DTFORMASCOBRO", Method.Get);
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        var jsonResp = JsonConvert.DeserializeObject<List<FormasDeCobroModel>>(content);
                        List<FormasDeCobroModel> fcList = (List<FormasDeCobroModel>)jsonResp;
                        if (fcList != null && fcList.Count > 0)
                        {
                            if (downloadType == ClsInitialChargeController.INITIAL_CHARGE)
                            {
                                FormasDeCobroModel.deleteAllFormasDeCobro();
                                lastId = FormasDeCobroModel.saveAllFormasDeCobro(fcList);
                                value = 1;
                            }
                            else if (downloadType == ClsInitialChargeController.UPDATE_DATA)
                            {
                                int totalFc = FormasDeCobroModel.getTotalOfFc();
                                if (totalFc == fcList.Count)
                                {
                                    var db = new SQLiteConnection();
                                    try
                                    {
                                        db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                                        db.Open();
                                        foreach (var fc in fcList)
                                        {
                                            if (FormasDeCobroModel.checkIfFcExist(db, fc.FORMA_COBRO_ID))
                                                FormasDeCobroModel.updateFormasDeCobro(db, fc);
                                            else FormasDeCobroModel.saveFormasDeCobro(db, fc);
                                        }
                                        value = 1;
                                    }
                                    catch (SQLiteException e)
                                    {
                                        SECUDOC.writeLog(e.ToString());
                                    }
                                    finally
                                    {
                                        if (db != null && db.State == System.Data.ConnectionState.Open)
                                            db.Close();
                                    }
                                } else
                                {
                                    FormasDeCobroModel.deleteAllFormasDeCobro();
                                    lastId = FormasDeCobroModel.saveAllFormasDeCobro(fcList);
                                    value = 1;
                                }
                            }
                        }
                        else {
                            FormasDeCobroModel.deleteAllFormasDeCobro();
                            description = "No se encontró ninguna forma de cobro en el servidor";
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

        public static async Task<ExpandoObject> downloadAllFormasDeCobroLAN()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    List<ClsFormasDePagoModel> fcList = ClsFormasDePagoModel.getAllFormasDeCobro(panelInstance);
                    if (fcList != null && fcList.Count > 0)
                    {
                        FormasDeCobroModel.deleteAllFormasDeCobro();
                        FormasDeCobroModel.saveAllFormasDeCobroLAN(fcList);
                    } else
                    {
                        FormasDeCobroModel.deleteAllFormasDeCobro();
                        description = "No se encontró ninguna forma de cobro en el servidor";
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

    }
}
