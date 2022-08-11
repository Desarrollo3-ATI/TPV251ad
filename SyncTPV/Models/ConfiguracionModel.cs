using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Dynamic;

namespace SyncTPV.Models
{
    public class ConfiguracionModel
    {
        public static readonly int SERVER_MODE_ACTIVE = 1;
        public int id { get; set; }
        public String linkAPI { get; set; }
        public int debugMode { get; set; }
        public int smsPermission { get; set; }
        public int printPermission { get; set; }
        public int serverMode { get; set; }
        public int numCopias { get; set; }
        public int scalesPermission { get; set; }
        public int captureWeightsManually { get; set; }
        public int appType { get; set; }
        public int serverId { get; set; }
        public int enterpriseId { get; set; }
        public int useFiscalField { get; set; }
        public int cerrarCOM { get; set; }
        public int positionFiscalItemField { get; set; }
        public int webActivate { get; set; }
        public int cotmos { get; set; }
        public String codigoCajaPadre { get; set; }

        public static ExpandoObject saveLinkWs(String linkWs)
        {
            dynamic response = new ExpandoObject();
            int value = 1;
            String description = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                dynamic responseExiste = ConfiguracionModel.configurationExist(db);
                if (responseExiste.value == 1)
                {
                    String query = "UPDATE " + LocalDatabase.TABLA_CONFIGURACION + " SET " +
                    LocalDatabase.CAMPO_VALOR_CONFIGURACION + " = @linkWs " +
                    "WHERE " + LocalDatabase.CAMPO_ID_CONFIGURACION + " = 1;";
                    using (SQLiteCommand command = new SQLiteCommand(query, db))
                    {
                        command.Parameters.AddWithValue("@linkWs", linkWs);
                        int numberOfRecords = command.ExecuteNonQuery();
                        if (numberOfRecords > 0)
                        {
                            value = 1;
                        }
                        else description = "No pudimos actualizar el link del web service para la Configuración!";
                    }
                } else if (responseExiste.value == 0)
                {
                    String query = "INSERT INTO " + LocalDatabase.TABLA_CONFIGURACION + " VALUES (1, @url, @modoDebug, " +
                        "@smsPermission, @printPermission, @serverMode, @numberOfCopies, @scalesPermission, " +
                        "@capturaPesoManual, @appType, @serverId, @enterpriseId, @useFicalField, @cerrarCOM, " +
                        "@fiscalItemField, @webActive, @cotmos, @codigoCajaPadre);";
                    using (SQLiteCommand command = new SQLiteCommand(query, db))
                    {
                        command.Parameters.AddWithValue("@url", linkWs);
                        command.Parameters.AddWithValue("@modoDebug", 1);
                        command.Parameters.AddWithValue("@smsPermission", 0);
                        command.Parameters.AddWithValue("@printPermission", 1);
                        command.Parameters.AddWithValue("@serverMode", 0);
                        command.Parameters.AddWithValue("@numberOfCopies", 1);
                        command.Parameters.AddWithValue("@scalesPermission", 0);
                        command.Parameters.AddWithValue("@capturaPesoManual", 0);
                        command.Parameters.AddWithValue("@appType", 2);
                        command.Parameters.AddWithValue("@serverId", 0);
                        command.Parameters.AddWithValue("@enterpriseId", 0);
                        command.Parameters.AddWithValue("@useFicalField", 1);
                        command.Parameters.AddWithValue("@cerrarCOM", 0);
                        command.Parameters.AddWithValue("@fiscalItemField", 6);
                        command.Parameters.AddWithValue("@webActive", 1);
                        command.Parameters.AddWithValue("@cotmos", 0);
                        command.Parameters.AddWithValue("@codigoCajaPadre", "");
                        int numberOfRecords = command.ExecuteNonQuery();
                        if (numberOfRecords > 0)
                        {
                            value = 1;
                        }
                        else description = "No pudimos crear el registro de la Configuración!";
                    }
                }
                else
                {
                    value = responseExiste.value;
                    description = responseExiste.description;
                }
            }
            catch (SQLiteException ex)
            {
                SECUDOC.writeLog(ex.ToString());
                value = -1;
                description = ex.Message;
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
                response.value = value;
                response.description = description;
            }
            return response;
        }

        public static ConfiguracionModel getConfiguration()
        {
            ConfiguracionModel cm = null;
            var conn = new SQLiteConnection();
            try
            {
                conn.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                conn.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_CONFIGURACION + " WHERE " + LocalDatabase.CAMPO_ID_CONFIGURACION + " = 1";
                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cm = new ConfiguracionModel();
                                cm.linkAPI = reader[LocalDatabase.CAMPO_VALOR_CONFIGURACION].ToString().Trim();
                                cm.debugMode = Convert.ToInt32(reader[LocalDatabase.CAMPO_MODODEBUG_CONFIGURACION].ToString().Trim());
                                cm.smsPermission = Convert.ToInt32(reader[LocalDatabase.CAMPO_SMS_CONFIG].ToString().Trim());
                                cm.printPermission = Convert.ToInt32(reader[LocalDatabase.CAMPO_PRINTPERMISSION_CONFIG].ToString().Trim());
                                cm.serverMode = Convert.ToInt32(reader[LocalDatabase.CAMPO_SERVERMODE_CONFIG].ToString().Trim());
                                cm.numCopias = Convert.ToInt32(reader[LocalDatabase.CAMPO_NUMERODECOPIAS_CONFIG].ToString().Trim());
                                cm.scalesPermission = Convert.ToInt32(reader[LocalDatabase.CAMPO_PERMISOBASCULA_CONFIG].ToString().Trim());
                                cm.captureWeightsManually = Convert.ToInt32(reader[LocalDatabase.CAMPO_CAPTURAPESOMANUAL_CONFIG].ToString().Trim());
                                cm.appType = Convert.ToInt32(reader[LocalDatabase.CAMPO_APPTYPE_CONFIG].ToString().Trim());
                                cm.serverId = Convert.ToInt32(reader[LocalDatabase.CAMPO_SERVERID_CONFIG].ToString().Trim());
                                cm.enterpriseId = Convert.ToInt32(reader[LocalDatabase.CAMPO_ENTERPRISEID_CONFIG].ToString().Trim());
                                cm.useFiscalField = Convert.ToInt32(reader[LocalDatabase.CAMPO_USEFICALFIELD_CONFIG].ToString().Trim());
                                cm.cerrarCOM = Convert.ToInt32(reader[LocalDatabase.CAMPO_CERRARCOM_CONFIG].ToString().Trim());
                                cm.positionFiscalItemField = Convert.ToInt32(reader[LocalDatabase.CAMPO_FISCALITEMFIELD_CONFIG].ToString().Trim());
                                cm.webActivate = Convert.ToInt32(reader[LocalDatabase.CAMPO_WEBACTIVE_CONFIG].ToString().Trim());
                                cm.cotmos = Convert.ToInt32(reader[LocalDatabase.CAMPO_COTMOS_CONFIG].ToString().Trim());
                                cm.codigoCajaPadre = reader[LocalDatabase.CAMPO_CAJAPADRE_CONFIG].ToString().Trim();
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
            return cm;
        }

        public static bool useFiscalFieldValueActivated()
        {
            bool useFiscal = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT "+LocalDatabase.CAMPO_USEFICALFIELD_CONFIG+" FROM "+LocalDatabase.TABLA_CONFIGURACION+" WHERE "+
                    LocalDatabase.CAMPO_ID_CONFIGURACION+" = 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                                if (reader.GetValue(0) != DBNull.Value)
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) == 1)
                                        useFiscal = true;
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            } catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return useFiscal;
        }

        public static int getPositionFiscalItemField()
        {
            int position = 6;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_FISCALITEMFIELD_CONFIG + " FROM " +
                    LocalDatabase.TABLA_CONFIGURACION + " WHERE " +LocalDatabase.CAMPO_ID_CONFIGURACION + " = 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                                if (reader.GetValue(0) != DBNull.Value)
                                    position = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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
            return position;
        }

        public static bool isCerrarCOMActivated()
        {
            bool close = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_CERRARCOM_CONFIG + " FROM " + LocalDatabase.TABLA_CONFIGURACION + " WHERE " +
                    LocalDatabase.CAMPO_ID_CONFIGURACION + " = 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                                if (reader.GetValue(0) != DBNull.Value)
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) == 1)
                                        close = true;
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
            return close;
        }

        public static String getLinkWs()
        {
            String url = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_CONFIGURACION + " WHERE " + LocalDatabase.CAMPO_ID_CONFIGURACION + " = 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                url = reader[LocalDatabase.CAMPO_VALOR_CONFIGURACION].ToString().Trim();
                                
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
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return url;
        }

        public static int getIntValue(String query)
        {
            int value = 0;
            var conn = new SQLiteConnection();
            try
            {
                conn.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    value = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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
            return value;
        }

        public static bool configurationExist(int idConfig)
        {
            bool exist = false;
            var conn = new SQLiteConnection();
            try
            {
                conn.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                conn.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_CONFIGURACION + " WHERE " +
                        LocalDatabase.CAMPO_ID_CONFIGURACION + " = @idConfig";
                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@idConfig", idConfig);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            exist = true;
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
            return exist;
        }

        public static int getNumCopias()
        {
            int value = 1;
            var conn = new SQLiteConnection();
            try
            {
                conn.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                conn.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_NUMERODECOPIAS_CONFIG + " FROM " + LocalDatabase.TABLA_CONFIGURACION +
                    " WHERE " + LocalDatabase.CAMPO_ID_CONFIGURACION + " = @idConfig";
                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@idConfig", 1);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    value = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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
            return value;
        }

        public static ExpandoObject configurationExist(SQLiteConnection db)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            try
            {
                String query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_CONFIGURACION;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) > 0)
                                        value = 1;
                                    else description = "La configuración del SyncTPV aún no existe!";
                            }
                        }
                        else description = "La configuración del SyncTPV aún no existe!";
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException e)
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
            return response;
        }

        public static bool updateServerMode(String query)
        {
            bool updated = true;
            var conn = new SQLiteConnection();
            try
            {
                conn.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        updated = true;
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
            return updated;
        }

        public static bool updateIdServer(int idServer)
        {
            bool updated = true;
            var conn = new SQLiteConnection();
            try
            {
                conn.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                conn.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_CONFIGURACION + " SET " + LocalDatabase.CAMPO_SERVERID_CONFIG + " = @serverId " +
                    "WHERE " + LocalDatabase.CAMPO_ID_CONFIGURACION + " = 1 AND " + LocalDatabase.CAMPO_ENTERPRISEID_CONFIG + " = @enterpriseId";
                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@serverId", idServer);
                    command.Parameters.AddWithValue("@enterpriseId", ClsRegeditController.getCurrentIdEnterprise());
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        updated = true;
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
            return updated;
        }

        public static ExpandoObject updateNumeroCopias(int numCopias)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            bool updated = true;
            var conn = new SQLiteConnection();
            try
            {
                conn.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                conn.Open();
                String query = "UPDATE "+LocalDatabase.TABLA_CONFIGURACION+" SET "+
                    LocalDatabase.CAMPO_NUMERODECOPIAS_CONFIG+ " = @numCopias WHERE "+
                    LocalDatabase.CAMPO_ID_CONFIGURACION+" = @idConfig";
                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@numCopias", numCopias);
                    command.Parameters.AddWithValue("@idConfig", 1);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                    {
                        value = 1;
                        updated = true;
                    }
                    else description = "No pudimos actualizar el número de copias en la tabla de Configuración";
                }
            }
            catch (SQLiteException Ex)
            {
                SECUDOC.writeLog(Ex.ToString());
                value = -1;
                description = Ex.Message;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
                response.value = value;
                response.description = description;
                response.updated = updated;
            }
            return response;
        }

        public static bool createUpdateOrDelete(String query)
        {
            bool updated = true;
            var conn = new SQLiteConnection();
            try
            {
                conn.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        updated = true;
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
            return updated;
        }

        public static Boolean printPermissionIsActivated()
        {
            Boolean activated = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_PRINTPERMISSION_CONFIG + " FROM " + LocalDatabase.TABLA_CONFIGURACION +
                        " WHERE " + LocalDatabase.CAMPO_ID_CONFIGURACION + " = " + 1;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetInt32(0) == 1)
                                    activated = true;
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
            return activated;
        }

        public static Boolean isTheConceptByRouteActive(string panelInstance)
        {
            bool isActive = false;
            SqlConnection connPanel = new SqlConnection();
            try
            {
                connPanel.ConnectionString = panelInstance;
                connPanel.Open();
                String query = "SELECT conceptoPorRuta FROM configuracion WHERE idConfiguracion = @id";
                using (SqlCommand command = new SqlCommand(query, connPanel))
                {
                    command.Parameters.AddWithValue("@id", 1);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetInt32(0) == 1)
                                    isActive = true;
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                SECUDOC.writeLog("Exception: " + e.ToString());
            }
            finally
            {
                if (connPanel != null && connPanel.State == ConnectionState.Open)
                    connPanel.Close();
            }
            return isActive;
        }

        public static Boolean isTheConceptByRouteActive(SqlConnection dbPanel)
        {
            bool isActive = false;
            try
            {
                String query = "SELECT conceptoPorRuta FROM configuracion WHERE idConfiguracion = @id";
                using (SqlCommand command = new SqlCommand(query, dbPanel))
                {
                    command.Parameters.AddWithValue("@id", 1);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetInt32(0) == 1)
                                    isActive = true;
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                SECUDOC.writeLog("Exception: " + e.ToString());
            }
            finally
            {
                
            }
            return isActive;
        }

        public static Boolean scalePermissionIsActivated()
        {
            Boolean activated = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_PERMISOBASCULA_CONFIG + " FROM " + LocalDatabase.TABLA_CONFIGURACION +
                        " WHERE " + LocalDatabase.CAMPO_ID_CONFIGURACION + " = " + 1;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) == 1)
                                    activated = true;
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
            return activated;
        }

        public static Boolean isLANPermissionActivated()
        {
            Boolean activated = false;
            var db = new SQLiteConnection();            
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_SERVERMODE_CONFIG + " FROM " + LocalDatabase.TABLA_CONFIGURACION +
                        " WHERE " + LocalDatabase.CAMPO_ID_CONFIGURACION + " = " + 1;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) == 1)
                                        activated = true;
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
            return activated;
        }

        public static Boolean isCapturaPesoManualPermissionActivated()
        {
            Boolean activated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_CAPTURAPESOMANUAL_CONFIG + " FROM " + LocalDatabase.TABLA_CONFIGURACION +
                        " WHERE " + LocalDatabase.CAMPO_ID_CONFIGURACION + " = " + 1;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) == 1)
                                        activated = true;
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
            return activated;
        }

        public static ExpandoObject updateUsoBascula(int activated)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            bool updated = true;
            var conn = new SQLiteConnection();
            try
            {
                conn.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                conn.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_CONFIGURACION + " SET " +
                LocalDatabase.CAMPO_PERMISOBASCULA_CONFIG + " = "+ activated + " WHERE " +
                        LocalDatabase.CAMPO_ID_CONFIGURACION + " = " + 1;
                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                    {
                        value = 1;
                        updated = true;
                    }
                    else description = "No pudimos actualizar el uso de la báscula!";
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
                value = -1;
                description = e.Message;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
                response.value = value;
                response.description = description;
                response.updated = updated;
            }
            return response;
        }

        public static bool updateFiscalesField(int useFiscalField)
        {
            bool updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE "+LocalDatabase.TABLA_CONFIGURACION+" SET "+LocalDatabase.CAMPO_USEFICALFIELD_CONFIG+" = @useFiscalField WHERE "+
                    LocalDatabase.CAMPO_ID_CONFIGURACION+" = 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@useFiscalField", useFiscalField);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        updated = true;
                }
            } catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return updated;
        }

        public static ExpandoObject updatePositionFiscalItemField(int positionFiscalItemField)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_CONFIGURACION + " SET " +
                    LocalDatabase.CAMPO_FISCALITEMFIELD_CONFIG + " = @positionFiscalItemField WHERE " +
                    LocalDatabase.CAMPO_ID_CONFIGURACION + " = 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@positionFiscalItemField", positionFiscalItemField);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        value = 1;
                    else description = "No pudimos actualizar la posición para el valor del campo fiscal!";
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
                value = -1;
                description = e.Message;
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
                response.value = value;
                response.description = description;
            }
            return response;
        }

        public static bool updateCerrarCOM(int cerrarCOM)
        {
            bool updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_CONFIGURACION + " SET " + LocalDatabase.CAMPO_CERRARCOM_CONFIG + " = @cerrarCOM WHERE " +
                    LocalDatabase.CAMPO_ID_CONFIGURACION + " = 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@cerrarCOM", cerrarCOM);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        updated = true;
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
            return updated;
        }

        public static ExpandoObject webActive()
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            bool active = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_WEBACTIVE_CONFIG + " FROM " + 
                    LocalDatabase.TABLA_CONFIGURACION +" WHERE " + LocalDatabase.CAMPO_ID_CONFIGURACION + " = " + 1;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) == 1)
                                    {
                                        value = 1;
                                        active = true;
                                    } else value = 1;
                            }
                        } else description = "No pudimos obtener el valor de Web Active en la Configuración!";
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
                value = -1;
                description = e.Message;
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
                response.value = value;
                response.description = description;
                response.active = active;
            }
            return response;
        }

        public static ExpandoObject updateWebActive(int webActive)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            bool updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_CONFIGURACION + " SET " +
                    LocalDatabase.CAMPO_WEBACTIVE_CONFIG + " = @webActive WHERE " +
                    LocalDatabase.CAMPO_ID_CONFIGURACION + " = 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@webActive", webActive);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                    {
                        value = 1;
                        updated = true;
                    }
                    else description = "No pudimos actualizar el campo web active en la Configuración!";
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
                value = -1;
                description = e.Message;
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
                response.value = value;
                response.description = description;
                response.updated = updated;
            }
            return response;
        }

        public static ExpandoObject updateCotmos(int cotmos)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            bool updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_CONFIGURACION + " SET " + 
                    LocalDatabase.CAMPO_COTMOS_CONFIG + " = @cotmos WHERE " +
                    LocalDatabase.CAMPO_ID_CONFIGURACION + " = 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@cotmos", cotmos);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                    {
                        value = 1;
                        updated = true;
                    }
                    else description = "No pudimos actualizar el uso cotización de mostrador en la Configuración!\r\n" +
                            "Validar si la conexión ya fue realizada!";
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
                value = -1;
                description = e.Message;
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
                response.value = value;
                response.description = description;
                response.updated = updated;
            }
            return response;
        }

        public static ExpandoObject cotmosActive()
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            bool active = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_COTMOS_CONFIG + " FROM " +
                    LocalDatabase.TABLA_CONFIGURACION + " WHERE " + LocalDatabase.CAMPO_ID_CONFIGURACION + " = " + 1;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) == 1)
                                    {
                                        value = 1;
                                        active = true;
                                    }
                                    else value = 1;
                            }
                        } else description = "No pudimos obtener el valor de Cotización de mostrador en la Configuración!\r\n" +
                            "Validar si la conexión ya fue realizada!";
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
                value = -1;
                description = e.Message;
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
                response.value = value;
                response.description = description;
                response.active = active;
            }
            return response;
        }

        public static ExpandoObject updateCodigoCajaPadre(String cajaPadre)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            bool updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_CONFIGURACION + " SET " +
                    LocalDatabase.CAMPO_CAJAPADRE_CONFIG + " = @cajaPadre WHERE " +
                    LocalDatabase.CAMPO_ID_CONFIGURACION + " = 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@cajaPadre", cajaPadre);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                    {
                        value = 1;
                        updated = true;
                    }
                    else description = "No pudimos actualizar el código de la Caja padre en la Configuración!\r\n" +
                            "Validar si la conexión ya fue realizada!";
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
                value = -1;
                description = e.Message;
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
                response.value = value;
                response.description = description;
                response.updated = updated;
            }
            return response;
        }

        public static ExpandoObject getCodigoCajaPadre()
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            String code = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_CAJAPADRE_CONFIG + " FROM " +
                    LocalDatabase.TABLA_CONFIGURACION + " WHERE " + LocalDatabase.CAMPO_ID_CONFIGURACION + " = " + 1;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                {
                                    value = 1;
                                    code = reader.GetValue(0).ToString().Trim();
                                }
                                else description = "El valor del código de la caja padre es Nulo!";
                            }
                        }
                        else description = "No pudimos obtener el valor del código de la Caja padre en la Configuración!\r\n" +
                            "Validar si la conexión ya fue realizada!";
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
                value = -1;
                description = e.Message;
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
                response.value = value;
                response.description = description;
                response.code = code;
            }
            return response;
        }

    }
}
