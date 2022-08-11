using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SyncTPV.Models.RegimenFiscalModel;

namespace SyncTPV.Controllers
{
    public class RegimenFiscalController
    {
        public static async Task<ExpandoObject> getAllRegimenFiscalAPI()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(() => {
                int value = 0;
                String description = "";
                List<RegimenFiscalModel> regimenFiscalList = null;
                try
                {
                    int itemsToEvaluate = 0;
                    string WSLic = "http://18.205.136.9:3000/wsRomLicense320/WsLicROM.asmx";
                    WSLic = WSLic.Replace(" ", "%20");
                    var client = new RestClient(WSLic);
                    var request = new RestRequest("/getAllRegimenFiscal", Method.Post);
                    request.AddJsonBody(new
                    {
                        lastId = 0,
                        limit = 1000,
                        parameterName = "paramterName",
                        parameterValue = ""
                    });
                    var responseHeader = client.ExecuteAsync(request);

                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        ResponseRegimenFiscal jsonResp = JsonConvert.DeserializeObject<ResponseRegimenFiscal>(responseHeader.Result.Content);
                        if (jsonResp.value == 1)
                        {
                            if (jsonResp.regimenFiscalList != null && jsonResp.regimenFiscalList.Count > 0)
                            {
                                regimenFiscalList = jsonResp.regimenFiscalList;
                                var db = new SQLiteConnection();
                                try
                                {
                                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                                    db.Open();
                                    for (int i = 0; i < jsonResp.regimenFiscalList.Count; i++)
                                    {
                                        String query = "INSERT INTO " + LocalDatabase.TABLA_REGIMEN_FISCAL + "(" +
                                            LocalDatabase.CAMPO_ID_REGIMEN_FISCAL + "," +
                                            LocalDatabase.CAMPO_VALOR_REGIMEN_FISCAL + "," +
                                            LocalDatabase.CAMPO_NOMBRE_REGIMEN_FISCAL + "," +
                                            LocalDatabase.CAMPO_DESCRIPCION_REGIMEN_FISCAL + "," +
                                            LocalDatabase.CAMPO_FECHA_ALTA_REGIMEN_FISCAL + "," +
                                            LocalDatabase.CAMPO_FECHA_REGIMEN_FISCAL + "," +
                                            LocalDatabase.CAMPO_MORAL_REGIMEN_FISCAL + "," +
                                            LocalDatabase.CAMPO_FISICA_REGIMEN_FISCAL + ")" +
                                            " VALUES (" +
                                              jsonResp.regimenFiscalList[i].id + ", '" +
                                              jsonResp.regimenFiscalList[i].valor + "', @nombre, @description, " +
                                              "'" +jsonResp.regimenFiscalList[i].fechaAlta.ToString() + "', '" +
                                              jsonResp.regimenFiscalList[i].fechaModificacion.ToString() + "', " +
                                              jsonResp.regimenFiscalList[i].moral + ", " +
                                              jsonResp.regimenFiscalList[i].fisica +
                                            ")";
                                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                                        {
                                            command.Parameters.AddWithValue("@nombre", jsonResp.regimenFiscalList[i].nombre);
                                            command.Parameters.AddWithValue("@description", jsonResp.regimenFiscalList[i].descripcion);
                                            int records = command.ExecuteNonQuery();
                                            if (records > 0)
                                                value = 1;
                                        }
                                    }
                                } catch (SQLiteException e)
                                {
                                    SECUDOC.writeLog(e.ToString());
                                    value = -1;
                                    description = e.Message;
                                } finally
                                {
                                    if (db != null && db.State == ConnectionState.Open)
                                        db.Close();
                                }
                            }
                        }
                        else
                        {
                            value = -2;
                            description = jsonResp.description;
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
                    response = -1;
                    description = e.Message;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                    response.regimenFiscalList = regimenFiscalList;
                }
            });
            return response;
        }

        public class ResponseRegimenFiscal
        {
            public int value { get; set; }
            public String description { get; set; }
            public int total { get; set; }
            public List<RegimenFiscalModel> regimenFiscalList { get; set; }
        }

    }
}
