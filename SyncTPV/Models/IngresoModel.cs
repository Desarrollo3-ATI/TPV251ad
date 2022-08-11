using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wsROMClases.Models;

namespace SyncTPV.Models
{
    public class IngresoModel
    {
        public int id { get; set; }
        public int number { get; set; }
        public int userId { get; set; }
        public String userCode { get; set; }
        public String dateTime { get; set; }
        public int sended { get; set; }
        public int serverId { get; set; }
        public int downloaded { get; set; }
        public String concept { get; set; }
        public String description { get; set; }
        public List<MontoIngresoModel> montos { get; set; }

        public static int createIngreso(int numero, int userId, String userCode, 
            String concept, String description)
        {
            int idCreated = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                int id = getLastId(db);
                id++;
                String query = "INSERT INTO "+LocalDatabase.TABLA_INGRESO+" VALUES(@id, @numero, @userId, @userCode, @fechaHora, @sended, " +
                    "@idServer, @downloaded, @concept, @description)";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@numero", numero);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@userCode", userCode);
                    command.Parameters.AddWithValue("@fechaHora", MetodosGenerales.getCurrentDateAndHour());
                    command.Parameters.AddWithValue("@sended", 0);
                    command.Parameters.AddWithValue("@idServer", 0);
                    command.Parameters.AddWithValue("@downloaded", 0);
                    command.Parameters.AddWithValue("@concept", concept);
                    command.Parameters.AddWithValue("@description", description);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        idCreated = id;
                }
            } catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            } finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return idCreated;
        }

        public static Boolean updateConcetpAndDescription(int idIngreso, String concept, String description)
        {
            Boolean terminated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_INGRESO + " SET " + LocalDatabase.CAMPO_CONCEPT_INGRESO + " = @concept, " +
                    LocalDatabase.CAMPO_DESCRIPTION_INGRESO + " = @description WHERE " + LocalDatabase.CAMPO_ID_INGRESO + " = @idIngreso";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@concept", concept);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@idIngreso", idIngreso);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        terminated = true;
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
            return terminated;
        }

        public static Boolean updateDateForTerminateIngresoOfMoney(int idIngreso)
        {
            Boolean terminated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_INGRESO + " SET " + LocalDatabase.CAMPO_FECHAHORA_INGRESO + " = @fechaHora" +
                    " WHERE " + LocalDatabase.CAMPO_ID_INGRESO + " = @idIngreso";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@fechaHora", MetodosGenerales.getCurrentDateAndHour());
                    command.Parameters.AddWithValue("@idIngreso", idIngreso);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        terminated = true;
                }
            } catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            } finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return terminated;
        }

        public static bool updateServerIdInAnEntry(int idIngresoApp, int idIngresoServer)
        {
            bool updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_INGRESO + " SET " + LocalDatabase.CAMPO_ENVIADO_INGRESO + " = @sended, " +
                    LocalDatabase.CAMPO_IDSERVER_INGRESO + " = @idIngresoServer WHERE " +
                    LocalDatabase.CAMPO_ID_INGRESO + " = @idIngresoApp";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@sended", 1);
                    command.Parameters.AddWithValue("@idIngresoApp", idIngresoApp);
                    command.Parameters.AddWithValue("@idIngresoServer", idIngresoServer);
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

        public static IngresoModel getARecord(String query)
        {
            IngresoModel im = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                im = new IngresoModel();
                                im.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_INGRESO].ToString().Trim());
                                im.number = Convert.ToInt32(reader[LocalDatabase.CAMPO_NUMERO_INGRESO].ToString().Trim());
                                im.userId = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDUSUARIO_INGRESO].ToString().Trim());
                                im.userCode = reader[LocalDatabase.CAMPO_CLAVEUSUARIO_INGRESO].ToString().Trim();
                                im.dateTime = reader[LocalDatabase.CAMPO_FECHAHORA_INGRESO].ToString().Trim();
                                if (reader[LocalDatabase.CAMPO_CONCEPT_INGRESO] == DBNull.Value)
                                    im.concept = "";
                                else im.concept = reader[LocalDatabase.CAMPO_CONCEPT_INGRESO].ToString().Trim();
                                if (reader[LocalDatabase.CAMPO_DESCRIPTION_INGRESO] == DBNull.Value)
                                    im.description = "";
                                else im.description = reader[LocalDatabase.CAMPO_DESCRIPTION_INGRESO].ToString().Trim();
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
            return im;
        }

        public static List<IngresoModel> getAllEntries(String query)
        {
            List<IngresoModel> ingresosList = null;
            IngresoModel im = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            ingresosList = new List<IngresoModel>();
                            while (reader.Read())
                            {
                                im = new IngresoModel();
                                im.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_INGRESO].ToString().Trim());
                                im.number = Convert.ToInt32(reader[LocalDatabase.CAMPO_NUMERO_INGRESO].ToString().Trim());
                                im.userId = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDUSUARIO_INGRESO].ToString().Trim());
                                im.userCode = reader[LocalDatabase.CAMPO_CLAVEUSUARIO_INGRESO].ToString().Trim();
                                im.dateTime = reader[LocalDatabase.CAMPO_FECHAHORA_INGRESO].ToString().Trim();
                                if (reader[LocalDatabase.CAMPO_CONCEPT_INGRESO] == DBNull.Value)
                                    im.concept = "";
                                else im.concept = reader[LocalDatabase.CAMPO_CONCEPT_INGRESO].ToString().Trim();
                                if (reader[LocalDatabase.CAMPO_DESCRIPTION_INGRESO] == DBNull.Value)
                                    im.description = "";
                                else im.description = reader[LocalDatabase.CAMPO_DESCRIPTION_INGRESO].ToString().Trim();
                                ingresosList.Add(im);
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
            return ingresosList;
        }

        public static List<int> getAllIdsEntriesNotSends()
        {
            List<int> ingresosList = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT "+LocalDatabase.CAMPO_ID_INGRESO+" FROM "+LocalDatabase.TABLA_INGRESO+" WHERE "+
                    LocalDatabase.CAMPO_ENVIADO_INGRESO+" = @enviado AND "+LocalDatabase.CAMPO_IDSERVER_INGRESO+" = @idServer";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@enviado", 0);
                    command.Parameters.AddWithValue("@idServer", 0);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            ingresosList = new List<int>();
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    ingresosList.Add(Convert.ToInt32(reader.GetValue(0).ToString().Trim()));
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
            return ingresosList;
        }

        public static List<ClsIngresosModel> getAllNotSendedToTheServer()
        {
            List<ClsIngresosModel> ingresosList = null;
            ClsIngresosModel im = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_INGRESO + " WHERE " + LocalDatabase.CAMPO_ENVIADO_INGRESO + " = " +
                    "@sended AND " + LocalDatabase.CAMPO_IDSERVER_INGRESO + " = @idServer";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@sended", 0);
                    command.Parameters.AddWithValue("@idServer", 0);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            ingresosList = new List<ClsIngresosModel>();
                            while (reader.Read())
                            {
                                im = new ClsIngresosModel();
                                im.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_INGRESO].ToString().Trim());
                                im.number = Convert.ToInt32(reader[LocalDatabase.CAMPO_NUMERO_INGRESO].ToString().Trim());
                                im.userId = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDUSUARIO_INGRESO].ToString().Trim());
                                im.userCode = reader[LocalDatabase.CAMPO_CLAVEUSUARIO_INGRESO].ToString().Trim();
                                im.dateTime = reader[LocalDatabase.CAMPO_FECHAHORA_INGRESO].ToString().Trim();
                                im.concept = reader[LocalDatabase.CAMPO_CONCEPT_INGRESO].ToString().Trim();
                                im.description = reader[LocalDatabase.CAMPO_DESCRIPTION_INGRESO].ToString().Trim();
                                im.montos = MontoIngresoModel.getAllMontosDeUnIngreso(im.id);
                                ingresosList.Add(im);
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
            return ingresosList;
        }

        public static IngresoModel getAnEntryNotSendedToTheServer(int idIngresoApp)
        {
            IngresoModel im = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_INGRESO + " WHERE " + LocalDatabase.CAMPO_ENVIADO_INGRESO + " = " +
                    "@sended AND " + LocalDatabase.CAMPO_IDSERVER_INGRESO + " = @idServer AND " + LocalDatabase.CAMPO_ID_INGRESO + " = @idIngresoApp";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@sended",0);
                    command.Parameters.AddWithValue("@idServer", 0);
                    command.Parameters.AddWithValue("@idIngresoApp", idIngresoApp);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                im = new IngresoModel();
                                im.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_INGRESO].ToString().Trim());
                                im.number = Convert.ToInt32(reader[LocalDatabase.CAMPO_NUMERO_INGRESO].ToString().Trim());
                                im.userId = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDUSUARIO_INGRESO].ToString().Trim());
                                im.userCode = reader[LocalDatabase.CAMPO_CLAVEUSUARIO_INGRESO].ToString().Trim();
                                im.dateTime = reader[LocalDatabase.CAMPO_FECHAHORA_INGRESO].ToString().Trim();
                                im.concept = reader[LocalDatabase.CAMPO_CONCEPT_INGRESO].ToString().Trim();
                                im.description = reader[LocalDatabase.CAMPO_DESCRIPTION_INGRESO].ToString().Trim();
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
            return im;
        }

        public static List<IngresoModel> getListIngresos(String query)
        {
            List<IngresoModel> ingresosList = null;
            IngresoModel im = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            ingresosList = new List<IngresoModel>();
                            while (reader.Read())
                            {
                                im = new IngresoModel();
                                im.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_INGRESO].ToString().Trim());
                                im.number = Convert.ToInt32(reader[LocalDatabase.CAMPO_NUMERO_INGRESO].ToString().Trim());
                                im.userId = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDUSUARIO_INGRESO].ToString().Trim());
                                im.userCode = reader[LocalDatabase.CAMPO_CLAVEUSUARIO_INGRESO].ToString().Trim();
                                im.dateTime = reader[LocalDatabase.CAMPO_FECHAHORA_INGRESO].ToString().Trim();
                                im.sended = Convert.ToInt32(reader[LocalDatabase.CAMPO_ENVIADO_INGRESO].ToString().Trim());
                                im.serverId = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDSERVER_INGRESO].ToString().Trim());
                                im.downloaded = Convert.ToInt32(reader[LocalDatabase.CAMPO_DOWNLOADED_INGRESO].ToString().Trim());
                                im.concept = reader[LocalDatabase.CAMPO_CONCEPT_INGRESO].ToString().Trim();
                                im.description = reader[LocalDatabase.CAMPO_DESCRIPTION_INGRESO].ToString().Trim();
                                ingresosList.Add(im);
                            }
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
            return ingresosList;
        }

        public static int getIntValue(String query)
        {
            int value = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
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
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return value;
        }

        public static String getDateFromAnEntry(int idIngreso)
        {
            String date = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_FECHAHORA_INGRESO + " FROM " + LocalDatabase.TABLA_INGRESO + " WHERE " +
                    LocalDatabase.CAMPO_ID_INGRESO + " = @idIngreso";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idIngreso", idIngreso);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    date = reader.GetValue(0).ToString().Trim();
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
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return date;
        }

        public static int getEntryNumber(int idIngreso)
        {
            int number = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_NUMERO_INGRESO + " FROM " + LocalDatabase.TABLA_INGRESO + " WHERE " +
                    LocalDatabase.CAMPO_ID_INGRESO + " = @idIngreso";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idIngreso", idIngreso);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    number = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return number;
        }

        public static int getLastentryNumber()
        {
            int number = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT MAX(" + LocalDatabase.CAMPO_NUMERO_INGRESO + ") FROM " + LocalDatabase.TABLA_INGRESO + " LIMIT 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    number = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return number;
        }

        public static int getLastId()
        {
            int lastId = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT MAX(" + LocalDatabase.CAMPO_ID_INGRESO + ") FROM " + LocalDatabase.TABLA_INGRESO + " LIMIT 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    lastId = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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
            return lastId;
        }

        public static int getLastId(SQLiteConnection db)
        {
            int lastId = 0;
            try
            {
                String query = "SELECT MAX(" + LocalDatabase.CAMPO_ID_INGRESO + ") FROM " + LocalDatabase.TABLA_INGRESO + " LIMIT 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    lastId = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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
                
            }
            return lastId;
        }

        public static int checkTheLastEntryNumber()
        {
            int resp = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String[] fechaHoy = MetodosGenerales.getCurrentDateAndHour().Split(new Char[] { ' ' });
                String query = "SELECT " + LocalDatabase.CAMPO_FECHAHORA_INGRESO + " FROM " + LocalDatabase.TABLA_INGRESO;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String[] fechaRet = reader.GetString(0).Split(new Char[] { ' ' });
                                if (fechaHoy[0] != null && fechaRet[0] != null && fechaHoy[0].Equals(fechaRet[0]))
                                {
                                    String query1 = "SELECT " + LocalDatabase.CAMPO_NUMERO_INGRESO + " FROM " + LocalDatabase.TABLA_INGRESO + 
                                        " WHERE " +LocalDatabase.CAMPO_FECHAHORA_INGRESO + " = @fechaHora";
                                    using (SQLiteCommand command1 = new SQLiteCommand(query1, db))
                                    {
                                        command1.Parameters.AddWithValue("@fechaHora", reader.GetValue(0).ToString().Trim());
                                        using (SQLiteDataReader reader1 = command1.ExecuteReader())
                                        {
                                            if (reader1.HasRows)
                                            {
                                                while (reader1.Read())
                                                {
                                                    if (reader1.GetValue(0) != DBNull.Value)
                                                        resp = Convert.ToInt32(reader1.GetValue(0).ToString().Trim());
                                                }
                                            }
                                            if (reader1 != null && !reader1.IsClosed)
                                                reader1.Close();
                                        }
                                    }
                                }
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
            return resp;
        }

        public static Boolean removeAnEntry(int idIngreso)
        {
            Boolean removed = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "DELETE FROM " + LocalDatabase.TABLA_INGRESO + " WHERE " + LocalDatabase.CAMPO_ID_INGRESO + " = @idIngreso";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idIngreso", idIngreso);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        removed = true;
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
            return removed;
        }

        public static bool deleteAllIngresos()
        {
            bool deleted = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "DELETE FROM " + LocalDatabase.TABLA_INGRESO;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int deletedRecords = command.ExecuteNonQuery();
                    deleted = true;
                }
            }
            catch (Exception ex)
            {
                SECUDOC.writeLog(ex.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return deleted;
        }

    }
}
