using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Dynamic;
using System.Threading.Tasks;

namespace SyncTPV.Controllers
{
    public class GetFTPCredentialsService
    {
        public async Task<ExpandoObject> handleActionFTPCredentials()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                try
                {
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    // client.Authenticator = new HttpBasicAuthenticator(username, password);
                    var request = new RestRequest("/DatosTicket");
                    var responseHeader = client.GetAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content; // Raw content as string
                        //var responseHttp = client.Get<String>(request);
                        var jsonResp = JsonConvert.DeserializeObject<List<ResponseTicket>>(content);
                        List<ResponseTicket> jsonTicket = (List<ResponseTicket>)jsonResp;
                        var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
                        db.Open();
                        try
                        {
                            int itemsAEvaluar = jsonTicket.Count;
                            int contador = 0;
                            DatosTicketModel.deleteAllDatosTicket(db);
                            for (int i = 0; i < itemsAEvaluar; i++)
                            {
                                ResponseTicket rt = jsonTicket[i];
                                String query = "INSERT INTO " + LocalDatabase.TABLA_DATOSTICKET + " VALUES(@id, @empresa, @direccion, @rfc, " +
                                "@expedido, @pieVentas, @pieCredito, @piePedido, @pieCotizacion, @pieCobranza, @pieDevolucion, " +
                                "@ftpServer, @ftpUser, @ftpPassword, @ftpPuerto, @claveSup, @pDevolucion, @valVC)";
                                using (SQLiteCommand command = new SQLiteCommand(query, db))
                                {
                                    command.Parameters.AddWithValue("@id", rt.CONFIGURA_ID);
                                    command.Parameters.AddWithValue("@empresa", rt.EMPRESA);
                                    command.Parameters.AddWithValue("@direccion", rt.DIRECCION);
                                    command.Parameters.AddWithValue("@rfc", rt.RFC);
                                    command.Parameters.AddWithValue("@expedido", rt.EXPEDIDO);
                                    command.Parameters.AddWithValue("@pieVentas", rt.PIE_TICKETVENTAS);
                                    command.Parameters.AddWithValue("@pieCredito", rt.PIE_TICKETCREDITO);
                                    command.Parameters.AddWithValue("@piePedido", rt.PIE_TICKETPEDIDO);
                                    command.Parameters.AddWithValue("@pieCotizacion", rt.PIE_TICKETCOTIZACION);
                                    command.Parameters.AddWithValue("@pieCobranza", rt.PIE_TICKETCOBRANZA);
                                    command.Parameters.AddWithValue("@pieDevolucion", rt.PIE_TICKETDEVOLUCION);
                                    command.Parameters.AddWithValue("@ftpServer", rt.FTPSERVER);
                                    command.Parameters.AddWithValue("@ftpUser", rt.FTPUSER);
                                    command.Parameters.AddWithValue("@ftpPassword", rt.FTPPASSWORD);
                                    command.Parameters.AddWithValue("@ftpPuerto", rt.FTPPUERTO);
                                    command.Parameters.AddWithValue("@claveSup", rt.CLAVESUP);
                                    command.Parameters.AddWithValue("@pDevolucion", rt.PDEVOLUCION);
                                    command.Parameters.AddWithValue("@valVC", rt.VALVC);
                                    int recordSaved = command.ExecuteNonQuery();
                                    if (recordSaved != 0)
                                        contador++;
                                }
                            }
                            if (contador == itemsAEvaluar)
                            {
                                response.valor = 100;
                                response.descripcion = "";
                            }
                            else
                            {
                                response.valor = 0;
                                response.descripcion = "Faltaron datos";
                            }
                        }
                        catch (SQLiteException e)
                        {
                            SECUDOC.writeLog("Exception: " + e.ToString());
                        }
                        finally
                        {
                            if (db != null && db.State == ConnectionState.Open)
                                db.Close();
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        if (responseHeader.Result.ErrorMessage.Equals("Unable to connect to the remote server"))
                        {
                            response.valor = 404;
                            response.descripcion = "Respuesta Incorrecta";
                        }
                        else
                        {
                            response.valor = 404;
                            response.descripcion = "Respuesta Incorrecta";
                        }
                    }

                }
                catch (Exception ex)
                {
                    SECUDOC.writeLog("Exception: " + ex.ToString());
                    response.valor = 404;
                    response.descripcion = "Respuesta Incorrecta: "+ex.Message;
                }
            });
            return response;
        }

        private class ResponseTicket
        {
            public int CONFIGURA_ID { get; set; }
            public string EMPRESA { get; set; }
            public string DIRECCION { get; set; }
            public string RFC { get; set; }
            public string EXPEDIDO { get; set; }
            public string PIE_TICKETVENTAS { get; set; }
            public string PIE_TICKETCREDITO { get; set; }
            public string PIE_TICKETPEDIDO { get; set; }
            public string PIE_TICKETCOTIZACION { get; set; }
            public string PIE_TICKETCOBRANZA { get; set; }
            public string PIE_TICKETDEVOLUCION { get; set; }
            public string FTPSERVER { get; set; }
            public string FTPUSER { get; set; }
            public string FTPPASSWORD { get; set; }
            public string FTPPUERTO { get; set; }
            public string CLAVESUP { get; set; }
            public string PDEVOLUCION { get; set; }
            public string VALVC { get; set; }
        }
    }
}
