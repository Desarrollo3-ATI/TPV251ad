using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Threading.Tasks;

namespace SyncTPV.Controllers
{
    public class ConfigurationWsController
    {

        public static async Task<ExpandoObject> doPingWs(string link)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(() =>
            {
                int value = 0;
                String description = "";
                try
                {
                    var client = new RestClient(link);
                    var request = new RestRequest("/pingResources", Method.Post);
                    request.Timeout = 5000;
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        ResponsePing responsePing = JsonConvert.DeserializeObject<ResponsePing>(content);
                        if (responsePing.value == 1)
                        {
                            value = 1;
                        }
                        else
                        {
                            value = responsePing.value;
                            description = responsePing.description;
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.TimedOut)
                    {
                        value = -404;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, 
                            responseHeader.Result.ErrorException.Message);
                    } else if (responseHeader.Result.ResponseStatus == ResponseStatus.Aborted)
                    {
                        value = -400;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, 
                            responseHeader.Result.ErrorException.Message);
                    } else
                    {
                        value = -500;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, 
                            responseHeader.Result.ErrorException.Message);
                    }
                }
                catch (Exception ex)
                {
                    SECUDOC.writeLog(ex.ToString());
                    value = -1;
                    description = "Verificar que la URL sea correcta!\r\n"+ex.Message;
                } finally
                {
                    response.value = value;
                    response.description = description;
                }
            });
            return response;
        }

        public class ResponsePing
        {
            public int value { set; get; }
            public String description { set; get; }
        }

        private class InstancesResponse
        {
            public String commercial { get; set; }
            public String panel { get; set; }
        }

        private static InstanceSQLSEModel validateIntance(String caracteres)
        {
            InstanceSQLSEModel im = null;
            if (!caracteres.Equals(""))
            {
                String[] parts = caracteres.Split('=');
                String part2 = parts[1];
                String[] parts1 = part2.Split(';');
                String instance = parts1[0];
                String part3 = parts[2];
                String[] parts2 = part3.Split(';');
                String dbName = parts2[0];
                String part4 = parts[3];
                String[] parts3 = part4.Split(';');
                String user = parts3[0];
                String pass = parts[4];
                if (!instance.Equals("") && !dbName.Equals("") && !user.Equals("") && !pass.Equals(""))
                {
                    im = new InstanceSQLSEModel();
                    im.instance = instance;
                    im.dbName = dbName;
                    im.user = user;
                    im.pass = pass;
                }
            }
            return im;
        }

        public static async Task<int> getInstancesSQLSE()
        {
            int response = 0;
            await Task.Run(() =>
            {
                try
                {
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/getInstanceSQLSEData", Method.Post);
                    request.AddJsonBody(new
                    {

                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        var jsonResp = JsonConvert.DeserializeObject<InstancesResponse>(content);
                        InstancesResponse instances = (InstancesResponse)jsonResp;
                        if (instances != null)
                        {
                            String query = "";
                            InstanceSQLSEModel saved = null;
                            String com = instances.commercial;
                            saved = validateIntance(com);
                            if (saved != null)
                            {
                                query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_INSTANCESQLSE + " WHERE " + LocalDatabase.CAMPO_ID_INSTANCESQLSE + " = " + 1;
                                int exist = InstanceSQLSEModel.getIntValue(query);
                                if (exist > 0)
                                {
                                    query = "UPDATE " + LocalDatabase.TABLA_INSTANCESQLSE + " SET " + LocalDatabase.CAMPO_INSTANCE_INSTANCESQLSE + " = '" + saved.instance + "', " +
                                    LocalDatabase.CAMPO_DBNAME_INSTANCESQLSE + " = '" + saved.dbName + "', " + LocalDatabase.CAMPO_USER_INSTANCESQLSE + " = '" + saved.user + "', " +
                                    LocalDatabase.CAMPO_PASS_INSTANCESQLSE + " = '" + saved.pass + "' WHERE " + LocalDatabase.CAMPO_ID_INSTANCESQLSE + " = " + 1;
                                    if (InstanceSQLSEModel.createOrUpdateInstanceSQLSE(query))
                                        response = 1;
                                } else
                                {
                                    if (InstanceSQLSEModel.insertARecord(1, saved.instance, saved.dbName, saved.user, saved.pass, saved.IPServer))
                                        response = 1;
                                }
                            }
                            String panel = instances.panel;
                            saved = validateIntance(panel);
                            if (saved != null)
                            {
                                query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_INSTANCESQLSE + " WHERE " + LocalDatabase.CAMPO_ID_INSTANCESQLSE + " = " + 2;
                                int exist = InstanceSQLSEModel.getIntValue(query);
                                if (exist > 0)
                                {
                                    query = "UPDATE " + LocalDatabase.TABLA_INSTANCESQLSE + " SET " + LocalDatabase.CAMPO_INSTANCE_INSTANCESQLSE + " = '" + saved.instance + "', " +
                                    LocalDatabase.CAMPO_DBNAME_INSTANCESQLSE + " = '" + saved.dbName + "', " + LocalDatabase.CAMPO_USER_INSTANCESQLSE + " = '" + saved.user + "', " +
                                    LocalDatabase.CAMPO_PASS_INSTANCESQLSE + " = '" + saved.pass + "' WHERE " + LocalDatabase.CAMPO_ID_INSTANCESQLSE + " = " + 2;
                                    if (InstanceSQLSEModel.createOrUpdateInstanceSQLSE(query))
                                        response = 1;
                                }
                                else
                                { 
                                    if (InstanceSQLSEModel.insertARecord(2, saved.instance, saved.dbName, saved.user, saved.pass, saved.IPServer))
                                        response = 1;
                                }
                            }
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        response = 400;
                    } else if (responseHeader.Result.ResponseStatus == ResponseStatus.TimedOut)
                    {
                        response = 404;
                    } else
                    {
                        response = 500;
                    }
                }
                catch (Exception ex)
                {
                    SECUDOC.writeLog(ex.ToString());
                    response = 2;
                }
            });
            return response;
        }

        public static async Task<int> validateInstances(int idInstance, String instance)
        {
            int response = 0;
            await Task.Run(() =>
            {
                var db = new SqlConnection();   
                try
                {
                    db.ConnectionString = instance;
                    db.Open();
                    String query = "";
                    if (idInstance == 1)
                        query = "SELECT * FROM "+DbStructure.ComDb.TABLA_PRODUCTOS;
                    else query = "SELECT * FROM " + DbStructure.RomDb.TABLA_ITEM;
                    using (SqlCommand command = new SqlCommand(query, db))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                response = 1;
                            else response = 1;
                            if (reader != null && !reader.IsClosed)
                                reader.Close();
                        }
                    }
                } catch (SqlException e)
                {
                    SECUDOC.writeLog(e.ToString());
                }
                finally
                {
                    if (db != null && db.State == ConnectionState.Open)
                        db.Close();
                }
            });
            return response;
        }

    }
}
