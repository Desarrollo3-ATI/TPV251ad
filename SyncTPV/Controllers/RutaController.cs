using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using SyncTPV.Models.Server.Panel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncTPV.Controllers
{
    public class RutaController
    {
        public static object RutaServexrModel { get; private set; }

        public static async Task<ExpandoObject> getAllRoutesWithWs(int lastId, String routeCode) {
            dynamic response = new ExpandoObject();
            await Task.Run(() => {
                try{
                    String errorMessage = "";
                    int itemsToEvaluate = 0;
                    int correctResponse = 0;
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    // client.Authenticator = new HttpBasicAuthenticator(username, password);
                    var request = new RestRequest("/getAllRoutes", Method.Post);
                    request.AddJsonBody(new
                    {
                        lastId = lastId,
                        limit = 1000,
                        routeCode = routeCode
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var jsonResp = JsonConvert.DeserializeObject<ResponseRutas>(responseHeader.Result.Content);
                        ResponseRutas routesResponse = (ResponseRutas)jsonResp;
                        if (routesResponse.routesList != null && routesResponse.routesCount > 0)
                        {
                            if (lastId == 0)
                            {
                                String query = "DELETE FROM " + LocalDatabase.TABLA_RUTA;
                                RutaModel.createUpdateOrDeleteRecords(query);
                            }
                            int count = 0;
                            foreach (RutaModel rm in routesResponse.routesList)
                            {
                                String query = "INSERT INTO " + LocalDatabase.TABLA_RUTA + " VALUES(" + rm.id + ", '" + rm.code + "', '" + rm.name + "', " +
                                "'" + rm.color + "', '" + rm.createdAt + "')";
                                if (RutaModel.createUpdateOrDeleteRecords(query) > 0)
                                    count++;
                            }
                            itemsToEvaluate = routesResponse.routesCount;
                            if (itemsToEvaluate == count)
                            {
                                correctResponse = 1;
                                errorMessage = "Datos Actualizados Correctamente";
                            }
                        }
                        else
                        {
                            String query = "DELETE FROM " + LocalDatabase.TABLA_RUTA;
                            RutaModel.createUpdateOrDeleteRecords(query);
                            errorMessage = "No se Encontró Ninguna Ruta";
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        if (responseHeader.Result.ErrorMessage.Equals("Unable to connect to the remote server"))
                        {
                            correctResponse = 404;
                            errorMessage = "Tiempo Excedido! " + responseHeader.Result.ErrorMessage;
                        }
                        else
                        {
                            correctResponse = 500;
                            errorMessage = "Tiempo Excedido! " + responseHeader.Result.ErrorMessage;
                        }
                    }
                    response.value = correctResponse;
                    response.description = errorMessage;
                } catch (Exception e) {
                    SECUDOC.writeLog(e.ToString());
                    response.value = -1;
                    response.description = "Exception: " + e.ToString();
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> getAllRoutesWithSQL(String panelInstance, int lastId, String routeCode)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(() => {
                int value = 0;
                String description = "";
                String queryTotal = "SELECT COUNT(*) FROM " + DbStructure.RomDb.TABLA_ROUTES;
                int routes = RutaServerModel.getIntValue(panelInstance, queryTotal);
                if (routes > 0) {
                    String queryDeleteRoutes = "DELETE FROM " + LocalDatabase.TABLA_RUTA;
                    RutaModel.createUpdateOrDeleteRecords(queryDeleteRoutes);
                    var db = new SqlConnection();
                    try
                    {
                        db.ConnectionString = panelInstance;
                        db.Open();
                        String query = "";
                        if (!routeCode.Equals(""))
                        {
                            query = "SELECT * FROM " + DbStructure.RomDb.TABLA_ROUTES + " WHERE " + DbStructure.RomDb.CAMPO_CODE_ROUTES + " = '" + routeCode + "'";
                        }
                        else
                        {
                            query = "SELECT * FROM " + DbStructure.RomDb.TABLA_ROUTES + " WHERE " + DbStructure.RomDb.CAMPO_ID_ROUTES + " > " +
                                lastId + " ORDER BY " + DbStructure.RomDb.CAMPO_ID_ROUTES;
                        }
                        using (SqlCommand command = new SqlCommand(query, db))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        String querySave = "INSERT INTO " + LocalDatabase.TABLA_RUTA + " VALUES(" +
                                        Convert.ToInt32(reader[DbStructure.RomDb.CAMPO_ID_ROUTES].ToString().Trim()) + ", '" +
                                        reader[DbStructure.RomDb.CAMPO_CODE_ROUTES].ToString().Trim() + "', '" +
                                        reader[DbStructure.RomDb.CAMPO_NAME_ROUTES].ToString().Trim()
                                        + "', " + "'" + reader[DbStructure.RomDb.CAMPO_COLOR_ROUTES].ToString().Trim() + "', '" +
                                        reader[DbStructure.RomDb.CAMPO_CREATEDAT_ROUTES].ToString().Trim() + "')";
                                        RutaModel.createUpdateOrDeleteRecords(querySave);
                                    }
                                }
                                value = 1;
                                description = "Descarga completada";
                                if (reader != null && !reader.IsClosed)
                                    reader.Close();
                            }
                        }
                    }
                    catch (SqlException e)
                    {
                        SECUDOC.writeLog(e.ToString());
                        value = -1;
                        description = "Exception: "+e.ToString();
                    }
                    finally
                    {
                        if (db != null && db.State == ConnectionState.Open)
                            db.Close();
                        response.value = value;
                        response.description = description;
                    }
                } else
                {
                    String queryDeleteRoutes = "DELETE FROM " + LocalDatabase.TABLA_RUTA;
                    RutaModel.createUpdateOrDeleteRecords(queryDeleteRoutes);
                    description = "No existen rutas que descargar";
                    response.value = value;
                    response.description = description;
                }
            });
            return response;
        }

    }
}
