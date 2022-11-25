using AdminDll;
using Cripto;
using Microsoft.Win32;
using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Dynamic;
using System.Threading.Tasks;

namespace SyncTPV.Models
{
    public class ResponseLicense
    {
        //public List<LicenseModel> response { get; set; }
        public int value { get; set; }
        public String description { get; set; }
    }

    public class ResponseValidateLicense
    {
        public int value { get; set; }
        public String description { get; set; }
        public String codigoEquipo { get; set; }
        public String fechaActualServer { get; set; }
        public int idE { get; set; }

    }

    public class LicenseModel
    {
        public static string routeRegedit { get; } = "Software\\Syncs\\SyncTPV\\SCS";
        public int valor { get; set; }
        public String descripcion { get; set; }

        public static async Task<ExpandoObject> activateLicenseInTheServer(String siteCode, String synckey)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                int idE = 0;
                String description = "";
                try
                {
                    if (siteCode.Trim().Length > 20)
                    {
                        siteCode = AES.Desencriptar(siteCode);
                        synckey = AES.Desencriptar(synckey);
                    }
                    string WSLic = "http://18.205.136.9:3000/wsRomLicense321/WsLicROM.asmx";
                    var client = new RestClient(WSLic); 
                    var request = new RestRequest("/activateLicense", Method.Post);
                    //request.AddParameter("codigo", siteCode);
                    //request.AddParameter("synckey", synckey);
                    request.AddJsonBody(new { 
                        codigo = siteCode,
                        synckey = synckey
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content; // Raw content as string
                        ResponseLicense jsonResp = JsonConvert.DeserializeObject<ResponseLicense>(content);
                        if (jsonResp.value > 0)
                        {
                            int tipoLic = jsonResp.value;
                            String fechaFin = jsonResp.description;
                            if (tipoLic == 0)
                            {
                                //Licencia no encontrada o no activada
                                value = -2;
                                description = fechaFin;
                                deleteLocalLicenseDb();
                            }
                            else if (tipoLic == 2)
                            {
                                bool licenseActive = await clsGeneral.isTheLicenseValid(fechaFin, "");
                                if (!licenseActive)
                                {
                                    //Licencia no activada
                                    value = -2;
                                    description = "La licencia No está activa validar fecha de vencimiento";
                                    deleteLocalLicenseDb();
                                }
                                else
                                {
                                    if (saveLicenseDataInLocalDb(siteCode, synckey, fechaFin, tipoLic, idE))//ide en 0
                                    {
                                        DateTime fechaActual = DateTime.Now;
                                        DateTime fechaActivacion = DateTime.Now;
                                        await ClsLicenciamientoController.llenarDatosEnRegedit(fechaActual, siteCode, fechaActivacion.ToString(), 
                                            fechaFin, synckey, tipoLic);
                                        value = 1;
                                        description = "Licencia activada con exito!";
                                    } else
                                    {
                                        value = -2;
                                        description = "No pudimos activar la licencia validar información!";
                                    }
                                }
                            }
                            else
                            {
                                value = -3;
                                description = "Validar el tipo de licencia probablemente fue creada para otro tipo de software";
                                deleteLocalLicenseDb();
                            }
                        } else
                        {
                            description = "La información de la licencia no fue encontrada en el servidor (licencia null)";
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
                catch (Exception Ex)
                {
                    SECUDOC.writeLog(Ex.ToString());
                    value = -1;
                    description = Ex.Message;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                }
            });
            return response;
        }

        /*public static async Task<ExpandoObject> validateLicenseInTheServer()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    String siteCode = BajoNivel.getCodigoSitio();
                    updateSiteCode(1, siteCode);
                    String synckey = getSynckeyInLocalDb();
                    string WSLic = "http://18.205.136.9:3000/wsRomLicense/WsLicROM.asmx";
                    var client = new RestClient(WSLic);
                    var request = new RestRequest("/validarLicencia", Method.Get);
                    request.AddParameter("codigo", siteCode);
                    request.AddParameter("synckey", synckey);
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed) {
                        var content = responseHeader.Result.Content;
                        var jsonResp = JsonConvert.DeserializeObject<ResponseLicense>(content);
                        if (jsonResp != null && jsonResp.response != null) {
                            int tipoLic = 0;
                            String fechaFin = "";
                            foreach (var res in jsonResp.response)
                            {
                                tipoLic = res.valor;
                                fechaFin = res.descripcion;
                            }
                            if (tipoLic == 0)
                            {
                                value = -2;
                                description = "el tipo de la licencia no fue encontrada";
                            } else if (tipoLic == 2)
                            {
                                bool licenseActive = await clsGeneral.isTheLicenseValid(fechaFin, "");
                                if (!licenseActive)
                                {
                                    //La licencia está vencida
                                    saveLicenseDataInLocalDb(siteCode, synckey, fechaFin, tipoLic);
                                    value = -2;
                                    description = "Licencia vencida";
                                }
                                else
                                {
                                    if (saveLicenseDataInLocalDb(siteCode, synckey, fechaFin, tipoLic))
                                    {
                                        value = 1;
                                        description = "Licencia activada con exito!";
                                    }
                                    else
                                    {
                                        value = -2;
                                        description = "No pudimos activar la licencia validar información!";
                                    }
                                }
                            } else
                            {
                                value = -3;
                            }
                        } else
                        {
                            description = "Licencia no encontrada en el servidor (license null)";
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
        }*/

        public static async Task<ExpandoObject> validateLicense()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                int idE = 0;
                String description = "";
                try
                {
                    String siteCode = BajoNivel.getCodigoSitio();
                    updateSiteCode(1, siteCode);
                    String synckey = getSynckeyInLocalDb();
                    //String WSLic = "http://18.205.136.9:3000/wsRomLicense/WsLicROM.asmx";
                    String WSLic = "http://18.205.136.9:3000/wsRomLicense321/WsLicROM.asmx";
                    var client = new RestClient(WSLic);
                    var request = new RestRequest("/validateLicense", Method.Post);
                    request.AddJsonBody(new
                    {
                        codigo = siteCode,
                        synckey = synckey
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        ResponseValidateLicense responseValidateLic = JsonConvert.DeserializeObject<ResponseValidateLicense>(content);
                        if (responseValidateLic != null)
                        {
                            int tipoLic = responseValidateLic.value;
                            String codigoEquipo = responseValidateLic.codigoEquipo;
                            idE = responseValidateLic.idE;
                            String fechaFin = "";
                            if (tipoLic == 0)
                            {
                                value = -2;
                                description = "El tipo de la licencia no fue encontrada";
                                deleteLocalLicenseDb();
                            }
                            else if (tipoLic == 2)
                            {
                                fechaFin = responseValidateLic.description;
                                bool licenseActive = await clsGeneral.isTheLicenseValid(fechaFin, responseValidateLic.fechaActualServer);
                                if (!licenseActive)
                                {
                                    /** La licencia está vencida */
                                    saveLicenseDataInLocalDb(siteCode, synckey, fechaFin, tipoLic, idE);
                                    value = -2;
                                    description = "Licencia vencida";
                                    deleteLocalLicenseDb();
                                }
                                else
                                {
                                    if (siteCode.Equals(codigoEquipo))
                                    {
                                        if (saveLicenseDataInLocalDb(siteCode, synckey, fechaFin, tipoLic, idE))
                                        {
                                            value = 1;
                                            description = "Licencia activada con exito!";
                                        }
                                        else
                                        {
                                            value = -2;
                                            description = "No pudimos activar la licencia validar información!";
                                        }
                                    } else
                                    {
                                        value = -2;
                                        description = "La licencia fue activada para otro código de sitio!";
                                        deleteLocalLicenseDb();
                                    }
                                }
                            }
                            else
                            {
                                value = -3;
                                description = "Validar el tipo de licencia probablemente fue creada para otro tipo de software";
                                deleteLocalLicenseDb();
                            }
                        }
                        else
                        {
                            description = "Licencia no encontrada en el servidor (license null)";
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        value = -400;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, responseHeader.Result.ErrorException.Message);
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.TimedOut)
                    {
                        value = -404;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, responseHeader.Result.ErrorException.Message);
                    }
                    else
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

        public static async Task<ExpandoObject> validateLocalLicense()
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                try
                {
                    if (clsGeneral.authenticationLicense())
                    {
                        clsGeneral.TipoLicenciaSyncReg();
                        if (clsGeneral.validarRegistroSync())
                        {
                            String licenseEndDate = LicenseModel.getEndDateEncryptFromTheLocalDb();
                            if (licenseEndDate.Equals(""))
                            {
                                description = "La informaciónd de la licencia no fue encontrada!";
                            } else
                            {
                                String fechaFin = AES.Desencriptar(licenseEndDate);
                                DateTime endDateLic = Convert.ToDateTime(fechaFin);
                                DateTime todayDate = DateTime.Now;
                                if (endDateLic > todayDate)
                                {
                                    value = 1;
                                }
                                else description = "La licencia a caducado!";
                            }
                        }
                        else
                        {
                            String licenseEndDate = LicenseModel.getEndDateEncryptFromTheLocalDb();
                            if (licenseEndDate.Equals(""))
                            {
                                description = "La información de la licencia no fue encontrada!";
                            } else
                            {
                                String fechaFin = AES.Desencriptar(licenseEndDate);
                                DateTime endDateLic = Convert.ToDateTime(fechaFin);
                                DateTime todayDate = DateTime.Now;
                                if (endDateLic > todayDate)
                                {
                                    value = 1;
                                }
                                else description = "La licencia a caducado!";
                            }
                        }
                    }
                    else
                    {
                        clsGeneral.fillRegeditAndFiles();
                        clsRegistro regeditValues = await LicenseModel.getRegeditValues();
                        if (regeditValues != null)
                        {
                            String licenseEndDate = LicenseModel.getEndDateEncryptFromTheLocalDb();
                            if (licenseEndDate.Equals(""))
                            {
                                description = "La información de la licencia no fue encontrada!";
                            } else
                            {
                                await ClsLicenciamientoController.addDataInLicenseLic(clsGeneral.nombreSync, regeditValues.TS,
                                licenseEndDate, Convert.ToInt32(regeditValues.DR));
                                String fechaFin = AES.Desencriptar(licenseEndDate);
                                DateTime endDateLic = Convert.ToDateTime(fechaFin);
                                DateTime todayDate = DateTime.Now;
                                if (todayDate <= endDateLic)
                                {
                                    value = 1;
                                }
                                else description = "La licencia a caducado!";
                            }
                        }
                        else
                        {
                            String licenseEndDate = LicenseModel.getEndDateEncryptFromTheLocalDb();
                            if (licenseEndDate.Equals(""))
                            {
                                description = "La información de la licencia no fue encontrada!";
                            } else
                            {
                                String fechaFin = AES.Desencriptar(licenseEndDate);
                                DateTime endDateLic = Convert.ToDateTime(fechaFin);
                                DateTime todayDate = DateTime.Now;
                                if (todayDate <= endDateLic)
                                {
                                    value = 1;
                                }
                                else description = "La licencia a caducado!";
                            }
                        }
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

        public static async Task<ExpandoObject> validateIfLicenseServerIsAccesible()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    string WSLic = "http://18.205.136.9:3000/wsRomLicense/WsLicROM.asmx";
                    var client = new RestClient(WSLic);
                    var request = new RestRequest("/pingLicense", Method.Post);
                    request.Timeout = 5000;
                    request.AddJsonBody(new
                    {
                        appType = 2
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        dynamic jsonResp = JsonConvert.DeserializeObject<ExpandoObject>(content);
                        if (jsonResp != null)
                        {
                            value = Convert.ToInt32(jsonResp.value);
                            if (value == 2)
                                value = 1;
                            description = jsonResp.description;
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        value = -400;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, responseHeader.Result.ErrorException.Message);
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.TimedOut)
                    {
                        value = -404;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, responseHeader.Result.ErrorException.Message);
                    }
                    else
                    {
                        value = -500;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, responseHeader.Result.ErrorException.Message);
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

        public static bool updateSiteCode(int idLicencia, String siteCode)
        {
            bool updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite; ;
                db.Open();
                String query = "UPDATE "+LocalDatabase.TABLA_LICENCIA+" SET "+LocalDatabase.CAMPO_CODIGO_DE_SITIO_LICENCIA+" = @codigoSitio " +
                    "WHERE "+LocalDatabase.CAMPO_ID_LICENCIA+" = "+ idLicencia;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@codigoSitio", BajoNivel.Encriptar(siteCode));
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        updated = true;
                }
            } catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            } finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return updated;
        }

        public static Boolean doesTheLicenseExistLocally(String siteCode)
        {
            bool exists = false; 
            String csBDEncrypt = "";
            var conn = new SQLiteConnection();
            try
            {
                conn.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                conn.Open();
                String query = "SELECT "+ LocalDatabase.CAMPO_CODIGO_DE_SITIO_LICENCIA + " FROM " + LocalDatabase.TABLA_LICENCIA+" WHERE "+
                    LocalDatabase.CAMPO_ID_LICENCIA+" =  1";
                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                {
                                    csBDEncrypt = reader.GetValue(0).ToString().Trim();
                                    if (!csBDEncrypt.Equals(""))
                                    {
                                        String csBD = "";
                                        try
                                        {
                                            csBD = AES.Desencriptar(csBDEncrypt);
                                        } catch (Exception e)
                                        {
                                            SECUDOC.writeLog(e.ToString());
                                            csBD = csBDEncrypt;
                                        }
                                        if (csBD == siteCode)
                                            exists = true;
                                    }
                                }
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }

                }
            }
            catch (SQLiteException Ex)
            {
                SECUDOC.writeLog(Ex.ToString()+" CS: "+ csBDEncrypt);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return exists;
        }

        public static bool saveLicenseDataInLocalDb(String cs, String synckey, string fecha, int tipoLic, int idE)
        {
            bool saved = false;
            var conn = new SQLiteConnection();
            try
            {
                String siteCode = BajoNivel.Encriptar(cs);
                synckey = BajoNivel.Encriptar(synckey);
                fecha = BajoNivel.Encriptar(fecha);
                conn.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                conn.Open();
                String query = "INSERT OR REPLACE INTO " + LocalDatabase.TABLA_LICENCIA + " VALUES (1, @siteCode, @synckey, @fecha, @x, @idE, @tipoLic); " +
                    "SELECT last_insert_rowid();";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@siteCode", siteCode);
                    cmd.Parameters.AddWithValue("@synckey", synckey);
                    cmd.Parameters.AddWithValue("@fecha", fecha);
                    cmd.Parameters.AddWithValue("@x", "x");
                    cmd.Parameters.AddWithValue("@idE", idE);
                    cmd.Parameters.AddWithValue("@tipoLic", tipoLic);
                    int records = cmd.ExecuteNonQuery();
                    if (records > 0)
                        saved = true;
                }
            }
            catch (SQLiteException Ex)
            {
                SECUDOC.writeLog(Ex.ToString());
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return saved;
        }

        public static String getEndDateEncryptFromTheLocalDb()
        {
            String endDate = "";
            var conn = new SQLiteConnection();
            try
            {
                conn.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                conn.Open();
                String query = "SELECT "+ LocalDatabase.CAMPO_FECHA_FIN_LICENCIA + " FROM " + LocalDatabase.TABLA_LICENCIA;
                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    endDate = reader.GetValue(0).ToString().Trim();
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException Ex)
            {
                SECUDOC.writeLog(Ex.ToString());
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return endDate;
        }

        public static string getLicenseTypeAndEndDateInLocalDb()
        {
            String response = "|0";
            var conn = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            conn.Open();
            try
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_LICENCIA;
                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int tipoLic = Convert.ToInt32(reader[LocalDatabase.CAMPO_TIPOLIC_LICENCA].ToString().Trim());
                                String fechaFin = reader[LocalDatabase.CAMPO_FECHA_FIN_LICENCIA].ToString().Trim();
                                fechaFin = AES.Desencriptar(fechaFin);
                                response = tipoLic + "|" + fechaFin;
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                //MensajeError = Ex.Message;
                SECUDOC.writeLog(Ex.ToString());
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return response;
        }

        public static string getSiteCodeInLocalDb()
        {
            String siteCode = "";
            var conn = new SQLiteConnection();
            try
            {
                conn.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                conn.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_CODIGO_DE_SITIO_LICENCIA + " FROM " + LocalDatabase.TABLA_LICENCIA +
                    " WHERE " + LocalDatabase.CAMPO_ID_LICENCIA + " = 1";
                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    siteCode = AES.Desencriptar(reader.GetValue(0).ToString().Trim());
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException Ex)
            {
                SECUDOC.writeLog(Ex.ToString());
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return siteCode;
        }

        public static string getSynckeyInLocalDb()
        {
            String synckey = "";
            var conn = new SQLiteConnection();
            try
            {
                conn.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                conn.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_SYNCKEY_LICENCIA + " FROM " + LocalDatabase.TABLA_LICENCIA + " WHERE " +
                    LocalDatabase.CAMPO_ID_LICENCIA + " = 1";
                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader[LocalDatabase.CAMPO_SYNCKEY_LICENCIA] != DBNull.Value)
                                    synckey = AES.Desencriptar(reader[LocalDatabase.CAMPO_SYNCKEY_LICENCIA].ToString().Trim());
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException Ex)
            {
                SECUDOC.writeLog(Ex.ToString());
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return synckey;
        }

        public static int getLicenseTypeInLocalDb()
        {
            int type = 0;
            var conn = new SQLiteConnection();
            try
            {
                conn.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                conn.Open();
                String query = "SELECT "+LocalDatabase.CAMPO_TIPOLIC_LICENCA + " FROM " + LocalDatabase.TABLA_LICENCIA;
                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader[LocalDatabase.CAMPO_TIPOLIC_LICENCA] != DBNull.Value)
                                    type = Convert.ToInt32(reader[LocalDatabase.CAMPO_TIPOLIC_LICENCA].ToString().Trim());
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                SECUDOC.writeLog(Ex.ToString());
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return type;
        }

        public static int getIdEspeciualLocalDb()
        {
            int idE = 0;
            var conn = new SQLiteConnection();
            try
            {
                conn.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                conn.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_IDE_LICENCIA + " FROM " + LocalDatabase.TABLA_LICENCIA;
                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader[LocalDatabase.CAMPO_IDE_LICENCIA] != DBNull.Value)
                                    idE = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDE_LICENCIA].ToString().Trim());
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                SECUDOC.writeLog(Ex.ToString());
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return idE;
        }

        public static Boolean isItTPVLicense()
        {
            Boolean itIs = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_TIPOLIC_LICENCA + " FROM " + LocalDatabase.TABLA_LICENCIA + " WHERE " +
                        LocalDatabase.CAMPO_ID_LICENCIA + " = " + 1;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetInt32(0) == 2)
                                    itIs = true;
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
            return itIs;
        }

        public static Boolean isItROMLicense()
        {
            Boolean itIs = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_TIPOLIC_LICENCA + " FROM " + LocalDatabase.TABLA_LICENCIA + " WHERE " +
                        LocalDatabase.CAMPO_ID_LICENCIA + " = " + 1;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetInt32(0) == 1)
                                    itIs = true;
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
            return itIs;
        }

        public static async Task<clsRegistro> getRegeditValues()
        {
            clsRegistro regeditValues = null;
            try
            {
                RegistryKey subKey = Registry.LocalMachine.OpenSubKey(routeRegedit);
                if (subKey != null)
                {
                    regeditValues = new clsRegistro();
                    regeditValues.DR = subKey.GetValue("DR").ToString();
                    regeditValues.EID = subKey.GetValue("EID").ToString();
                    regeditValues.FA = subKey.GetValue("FA").ToString();
                    regeditValues.FV = subKey.GetValue("FV").ToString();
                    regeditValues.SK = subKey.GetValue("SK").ToString();
                    regeditValues.TL = subKey.GetValue("TL").ToString();
                    regeditValues.TS = subKey.GetValue("TS").ToString();
                    regeditValues.IDE = subKey.GetValue("IDE").ToString();
                    regeditValues.Desencriptar();
                }
            } catch (UnauthorizedAccessException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            return regeditValues;
        }

        public static bool deleteLocalLicenseDb()
        {
            bool deleted = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "DELETE FROM "+LocalDatabase.TABLA_LICENCIA;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        deleted = true;
                }
            } catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            } finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return deleted;
        }

    }
}
