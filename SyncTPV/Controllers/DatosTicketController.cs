using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Dynamic;
using System.Threading.Tasks;

namespace SyncTPV.Controllers
{
    public class DatosTicketController
    {
        public static async Task<ExpandoObject> downloadAllDatosTicketAPI()
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
                    // client.Authenticator = new HttpBasicAuthenticator(username, password);
                    var request = new RestRequest("/DatosTicket", Method.Get);
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        var jsonResp = JsonConvert.DeserializeObject<List<DatosTicketModel>>(content);
                        List<DatosTicketModel> dtList = (List<DatosTicketModel>)jsonResp;
                        if (dtList != null && dtList.Count > 0)
                        {
                            if (lastId == 0)
                                DatosTicketModel.deleteAllDatosTicket();
                            lastId = DatosTicketModel.saveAllDatosTicket(dtList);
                            value = 1;
                        } else
                        {
                            description = "No se encontró la información de la empresa y ticket";
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

        public static ExpandoObject getallDataTicketLAN()
        {
            dynamic pm = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_DATOSTICKET + " Where "+ LocalDatabase.CAMPO_TICCONFIGURA + " = 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                pm = new ExpandoObject();
                                pm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_TICCONFIGURA].ToString().Trim());
                                pm.nombre = reader[LocalDatabase.CAMPO_TICEMPRESA].ToString().Trim();
                                pm.direccion = reader[LocalDatabase.CAMPO_TICDIRECCION].ToString().Trim();
                                pm.rfc = reader[LocalDatabase.CAMPO_TICRFC].ToString().Trim();
                                pm.expedido = reader[LocalDatabase.CAMPO_TICEXPENDIDO].ToString().Trim();
                                //
                                pm.ventaEfectivo = reader[LocalDatabase.CAMPO_TICVENTA].ToString().Trim(); 
                                pm.ventaCredito = reader[LocalDatabase.CAMPO_TICCREDITO].ToString().Trim();
                                pm.pedidos = reader[LocalDatabase.CAMPO_TICPEDIDO].ToString().Trim();
                                pm.cotizacion = reader[LocalDatabase.CAMPO_TICCOTIZACION].ToString().Trim();
                                pm.cobranza = reader[LocalDatabase.CAMPO_TICCOBRANZA].ToString().Trim();
                                pm.devolucion = reader[LocalDatabase.CAMPO_TICDEVOLUCION].ToString().Trim();
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
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
            return pm;
        }

        public static ExpandoObject updateallDataTicketTPVLAN(
            String empresa, String direccion, String rfc, String expedido,
            String efectivo, String credito, String cotizacion, String cobranza, String pedido, String devolucion
            )
        {
            dynamic pm = new ExpandoObject();
            var db = new SQLiteConnection();

            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "update " + LocalDatabase.TABLA_DATOSTICKET + " set " +
                LocalDatabase.CAMPO_TICEMPRESA + " = '" + empresa +"',"+
                LocalDatabase.CAMPO_TICDIRECCION + " = '" + direccion + "'," +
                LocalDatabase.CAMPO_TICRFC + " = '" + rfc + "'," +
                LocalDatabase.CAMPO_TICEXPENDIDO + " = '" + expedido + "',"+
                LocalDatabase.CAMPO_TICVENTA + " = '" + efectivo + "'," +
                LocalDatabase.CAMPO_TICCREDITO + " = '" + credito + "'," +
                LocalDatabase.CAMPO_TICPEDIDO + " = '" + pedido + "'," +
                LocalDatabase.CAMPO_TICCOTIZACION + " = '" + cotizacion + "'," +
                LocalDatabase.CAMPO_TICCOBRANZA + " = '" + cobranza + "'," +
                LocalDatabase.CAMPO_TICDEVOLUCION + " = '" + devolucion + "'" +
                " Where " + LocalDatabase.CAMPO_TICCONFIGURA + " = 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {        
                            pm.value = 1;
                            pm.descripcion = "Información guardada correctamente";
                        }
                        else
                        {
                            pm.value = 0;
                            pm.descripcion = "No fue posible guardar la información";
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
                pm.descripcion = e.ToString();
                pm.value = 0;
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return pm;
        }

        public static async Task<ExpandoObject> downloadAllDatosTicketLAN()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    int lastId = 0;
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    ClsDatosTicketModel datosTicket = ClsDatosTicketModel.getDatosTicket(panelInstance);
                    if (datosTicket != null)
                    {
                        if (lastId == 0)
                            DatosTicketModel.deleteAllDatosTicket();
                        lastId = DatosTicketModel.saveAllDatosTicketLAN(datosTicket);
                        value = 1;
                    } else
                    {
                        description = "No se encontró la información de la empresa y ticket";
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

    }
}
