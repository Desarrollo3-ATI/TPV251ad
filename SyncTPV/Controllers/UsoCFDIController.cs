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

namespace SyncTPV.Controllers
{
    public class UsoCFDIController
    {

        public static async Task<ExpandoObject> getAllUsosCFDIAPI()
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            List<UsoCFDIModel> usosCfdiList = null;
            await Task.Run(async () =>
            {
                try
                {
                    int itemsToEvaluate = 0;
                    string WSLic = "http://18.205.136.9:3000/wsRomLicense320/WsLicROM.asmx";
                    WSLic = WSLic.Replace(" ", "%20");
                    var client = new RestClient(WSLic);
                    var request = new RestRequest("/getAllUsosCfdi", Method.Post);
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
                        ResponseUSOCFDI jsonResp = JsonConvert.DeserializeObject<ResponseUSOCFDI>(responseHeader.Result.Content);
                        if (jsonResp.value == 1)
                        {
                            if (jsonResp.usoCfdiList != null && jsonResp.usoCfdiList.Count > 0)
                            {
                                usosCfdiList = jsonResp.usoCfdiList;
                                var db = new SQLiteConnection();
                                try {
                                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                                    db.Open();
                                    for (int i = 0; i < jsonResp.usoCfdiList.Count; i++)
                                    {
                                        String query = "INSERT INTO " + LocalDatabase.TABLA_USO_CFDI + "(" +
                                        LocalDatabase.CAMPO_ID_USO_CFDI + "," +
                                        LocalDatabase.CAMPO_VALOR_USO_CFDI + "," +
                                        LocalDatabase.CAMPO_NOMBRE_USO_CFDI + "," +
                                        LocalDatabase.CAMPO_DESCRIPCION_USO_CFDI + "," +
                                        LocalDatabase.CAMPO_FECHA_ALTA_USO_CFDI + "," +
                                        LocalDatabase.CAMPO_FECHA_USO_CFDI + "," +
                                        LocalDatabase.CAMPO_MORAL_USO_CFDI + "," +
                                        LocalDatabase.CAMPO_FISICA_USO_CFDI + ")" +
                                        " VALUES (" +
                                            jsonResp.usoCfdiList[i].id + ", '" +
                                            jsonResp.usoCfdiList[i].valor.ToString() + "', @nombre, @description, '" +
                                            jsonResp.usoCfdiList[i].fechaAlta.ToString() + "', '" +
                                            jsonResp.usoCfdiList[i].fechaModificacion.ToString() + "', " +
                                            jsonResp.usoCfdiList[i].moral + ", " +
                                            jsonResp.usoCfdiList[i].fisica +
                                        ")";
                                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                                        {
                                            command.Parameters.AddWithValue("@nombre", jsonResp.usoCfdiList[i].nombre);
                                            command.Parameters.AddWithValue("@description", jsonResp.usoCfdiList[i].descripcion);
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
                    response = -1;
                    description = e.Message;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                    response.usosCfdiList = usosCfdiList;
                }
            });
            return response;
        }

        private class ResponseUSOCFDI
        {
            public int value { get; set; }
            public String description { get; set; }
            public int total { get; set; }
            public List<UsoCFDIModel> usoCfdiList { get; set; }
        }

    }
}
