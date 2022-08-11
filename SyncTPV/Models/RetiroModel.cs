using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using wsROMClases;

namespace SyncTPV
{
    public class clsRetiro
    {
        public int ID { get; set; }
        public string FECHA { get; set; }
        public string CLAVEUSUARIO { get; set; }
        public string MONTO { get; set; }
    }

    public class RetiroModel
    {
        public int id { get; set; }
        public int number { get; set; }
        public int idUsuario { get; set; }
        public String claveUsuario { get; set; }
        public String fechaHora { get; set; }
        public int enviado { get; set; }
        public int idServer { get; set; }
        public int downloaded { get; set; }
        public String concept { get; set; }
        public String description { get; set; }

        public List<MontoRetiroModel> montos { get; set; }

        public static int createNewWithdrawal(int numero, String concept, String description)
        {
            int retiroId = 0;
            int lastID = getLastId() + 1;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "INSERT INTO " + LocalDatabase.TABLA_RETIROS + " (" + LocalDatabase.CAMPO_ID_RETIRO + ", " + LocalDatabase.CAMPO_NUMERO_RETIRO + ", " +
                    LocalDatabase.CAMPO_IDUSUARIO_RETIRO + ", " + LocalDatabase.CAMPO_CLAVEUSUARIO_RETIRO + ", " +
                    LocalDatabase.CAMPO_FECHAHORA_RETIRO + ", "+LocalDatabase.CAMPO_CONCEPT_RETIRO+", "+LocalDatabase.CAMPO_DESCRIPTION_RETIRO+") VALUES(@id, @number, @userId, @userCode, @date, '"+concept+"', '"+description+"')";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", lastID);
                    command.Parameters.AddWithValue("@number", numero);
                    command.Parameters.AddWithValue("@userId", ClsRegeditController.getIdUserInTurn());
                    command.Parameters.AddWithValue("@userCode", UserModel.getAStringValueForAnyUser("SELECT " + LocalDatabase.CAMPO_ClAVE_USUARIO + " FROM " +
                        LocalDatabase.TABLA_USUARIO + " WHERE " + LocalDatabase.CAMPO_ID_USUARIO + " = " + ClsRegeditController.getIdUserInTurn()));
                    command.Parameters.AddWithValue("@date", MetodosGenerales.getCurrentDateAndHour());
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        retiroId = lastID;
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
            return retiroId;
        }

        public static int getLastId()
        {
            int lastId = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT MAX(" + LocalDatabase.CAMPO_ID_RETIRO + ") FROM " + LocalDatabase.TABLA_RETIROS + " ORDER BY " +
                    LocalDatabase.CAMPO_ID_RETIRO + " DESC LIMIT 1";
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
            catch (Exception Ex)
            {
                SECUDOC.writeLog(Ex.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return lastId;
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
            catch (Exception Ex)
            {
                SECUDOC.writeLog(Ex.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return value;
        }

        public static int getWithdrawalNumber(int idRetiro)
        {
            int number = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_NUMERO_RETIRO + " FROM " + LocalDatabase.TABLA_RETIROS + " WHERE " +
                    LocalDatabase.CAMPO_ID_RETIRO + " = " + idRetiro;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                number = reader.GetInt32(0);
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

        public static String getDateFromAWithdrawal(int idRetiro)
        {
            String date = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_FECHAHORA_RETIRO + " FROM " + LocalDatabase.TABLA_RETIROS + " WHERE " +
                    LocalDatabase.CAMPO_ID_RETIRO + " = " + idRetiro;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                date = reader.GetString(0);
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

        public static RetiroModel getAWithdrawalsNotSentToTheServer(int idRetiroApp)
        {
            RetiroModel rm = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_RETIROS + " WHERE " + LocalDatabase.CAMPO_ENVIADO_RETIRO + " = " +
                        0 + " AND " + LocalDatabase.CAMPO_IDSERVER_RETIRO + " = " + 0 + " AND " + LocalDatabase.CAMPO_ID_RETIRO + " = " + idRetiroApp;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                rm = new RetiroModel();
                                rm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_RETIRO].ToString().Trim());
                                rm.number = Convert.ToInt32(reader[LocalDatabase.CAMPO_NUMERO_RETIRO].ToString().Trim());
                                rm.idUsuario = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDUSUARIO_RETIRO].ToString().Trim());
                                rm.claveUsuario = reader[LocalDatabase.CAMPO_CLAVEUSUARIO_RETIRO].ToString().Trim();
                                rm.fechaHora = reader[LocalDatabase.CAMPO_FECHAHORA_RETIRO].ToString().Trim();
                                rm.concept = reader[LocalDatabase.CAMPO_CONCEPT_RETIRO].ToString().Trim();
                                rm.description = reader[LocalDatabase.CAMPO_DESCRIPTION_RETIRO].ToString().Trim();
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
            return rm;
        }

        public static RetiroModel getARecord(String query)
        {
            RetiroModel rm = null;
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
                                rm = new RetiroModel();
                                rm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_RETIRO].ToString().Trim());
                                rm.number = Convert.ToInt32(reader[LocalDatabase.CAMPO_NUMERO_RETIRO].ToString().Trim());
                                rm.idUsuario = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDUSUARIO_RETIRO].ToString().Trim());
                                rm.claveUsuario = reader[LocalDatabase.CAMPO_CLAVEUSUARIO_RETIRO].ToString().Trim();
                                rm.fechaHora = reader[LocalDatabase.CAMPO_FECHAHORA_RETIRO].ToString().Trim();
                                if (reader[LocalDatabase.CAMPO_CONCEPT_RETIRO] == DBNull.Value)
                                    rm.concept = "";
                                else rm.concept = reader[LocalDatabase.CAMPO_CONCEPT_RETIRO].ToString().Trim();
                                if (reader[LocalDatabase.CAMPO_DESCRIPTION_RETIRO] == DBNull.Value)
                                    rm.description = "";
                                else rm.description = reader[LocalDatabase.CAMPO_DESCRIPTION_RETIRO].ToString().Trim();
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
            return rm;
        }

        public static int getTheTotalNumberOfWithdrawalsNotSentToTheServer()
        {
            int total = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT COUNT(R." + LocalDatabase.CAMPO_ID_RETIRO + ") FROM " + LocalDatabase.TABLA_RETIROS + " R " +
                        "INNER JOIN " + LocalDatabase.TABLA_MONTORETIROS + " M ON R." + LocalDatabase.CAMPO_ID_RETIRO + " = " +
                        "M." + LocalDatabase.CAMPO_RETIROID_MONTORETIRO +
                        " WHERE (R." + LocalDatabase.CAMPO_ENVIADO_RETIRO + " = " + 0 + " OR R." + LocalDatabase.CAMPO_IDSERVER_RETIRO + " = " + 0 +")"+
                        " AND M." + LocalDatabase.CAMPO_IMPORTE_MONTORETIROS + " > " + 0 + " AND M." + LocalDatabase.CAMPO_ENVIADO_MONTORETIRO + " = " + 0;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                total = reader.GetInt32(0);
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
            return total;
        }

        public static int checkTheLastWithdrawalNumber()
        {
            int resp = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String[] fechaHoy = MetodosGenerales.getCurrentDateAndHour().Split(new Char[] { ' ' });
                String query = "SELECT " + LocalDatabase.CAMPO_FECHAHORA_RETIRO + " FROM " + LocalDatabase.TABLA_RETIROS;
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
                                    String query1 = "SELECT " + LocalDatabase.CAMPO_NUMERO_RETIRO + " FROM " + LocalDatabase.TABLA_RETIROS + " WHERE " +
                                            LocalDatabase.CAMPO_FECHAHORA_RETIRO + " = @fechaHora";
                                    using (SQLiteCommand command1 = new SQLiteCommand(query1, db))
                                    {
                                        command1.Parameters.AddWithValue("@fechaHora", reader.GetString(0));
                                        using (SQLiteDataReader reader1 = command1.ExecuteReader())
                                        {
                                            if (reader1.HasRows)
                                            {
                                                while (reader1.Read())
                                                {
                                                    resp = reader1.GetInt32(0);
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

        public static Boolean updateDateForTerminateWithdrawalOfMoney(int id)
        {
            Boolean terminated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_RETIROS + " SET " + LocalDatabase.CAMPO_FECHAHORA_RETIRO + " = @fechaHora" +
                    " WHERE " + LocalDatabase.CAMPO_ID_RETIRO + " = @idRetiro";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@fechaHora", MetodosGenerales.getCurrentDateAndHour());
                    command.Parameters.AddWithValue("@idRetiro", id);
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

        public static Boolean updateARecord(String query)
        {
            Boolean terminated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
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

        public static Boolean updateConcetpAndDescription(int idRetiro, String concept, String description)
        {
            Boolean terminated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_RETIROS + " SET " + LocalDatabase.CAMPO_CONCEPT_RETIRO + " = @concept, " +
                    LocalDatabase.CAMPO_DESCRIPTION_RETIRO + " = @description WHERE " + LocalDatabase.CAMPO_ID_RETIRO + " = @idRetiro";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@concept", concept);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@idRetiro", idRetiro);
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

        public static Boolean removeAWithdrawal(int id)
        {
            Boolean removed = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "DELETE FROM " + LocalDatabase.TABLA_RETIROS + " WHERE " + LocalDatabase.CAMPO_ID_RETIRO + " = " + id;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
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

        public static Boolean updateServerIdInAWithdrawal(int idRetiroApp, int idRetiroServer)
        {
            Boolean updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_RETIROS + " SET " + LocalDatabase.CAMPO_ENVIADO_RETIRO + " = " + 1 + ", " + 
                    LocalDatabase.CAMPO_IDSERVER_RETIRO +" = " + idRetiroServer + " WHERE " + 
                    LocalDatabase.CAMPO_ID_RETIRO + " = " + idRetiroApp;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
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

        public static DataTable getAllWithdrawalsDt(String query)
        {
            DataTable dt = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                    adapter.SelectCommand = command;
                    dt = new DataTable();
                    adapter.Fill(dt);
                }
            }
            catch (SQLiteException ex)
            {
                SECUDOC.writeLog(ex.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return dt;
        }

        public static Boolean deleteAWithdrawal(SQLiteConnection db, int id)
        {
            Boolean deleted = false;
            try
            {
                String query = "DELETE FROM " + LocalDatabase.TABLA_RETIROS + " WHERE " + LocalDatabase.CAMPO_ID_RETIRO + " = " + id;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        deleted = true;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                
            }
            return deleted;
        }

        public static List<RetiroModel> getAllWithdrawals(String query)
        {
            List<RetiroModel> retirosList = null;
            RetiroModel rm;
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
                            retirosList = new List<RetiroModel>();
                            while (reader.Read())
                            {
                                rm = new RetiroModel();
                                rm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_RETIRO].ToString().Trim());
                                rm.number = Convert.ToInt32(reader[LocalDatabase.CAMPO_NUMERO_RETIRO].ToString().Trim());
                                rm.idUsuario = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDUSUARIO_RETIRO].ToString().Trim());
                                rm.claveUsuario = reader[LocalDatabase.CAMPO_CLAVEUSUARIO_RETIRO].ToString().Trim();
                                rm.fechaHora = reader[LocalDatabase.CAMPO_FECHAHORA_RETIRO].ToString().Trim();
                                if (reader[LocalDatabase.CAMPO_CONCEPT_RETIRO] == DBNull.Value)
                                    rm.concept = "Sin Concepto";
                                else rm.concept = reader[LocalDatabase.CAMPO_CONCEPT_RETIRO].ToString().Trim();
                                if (reader[LocalDatabase.CAMPO_DESCRIPTION_RETIRO] == DBNull.Value)
                                    rm.description = "";
                                else rm.description = reader[LocalDatabase.CAMPO_DESCRIPTION_RETIRO].ToString().Trim();
                                retirosList.Add(rm);
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
            return retirosList;
        }

        public static List<RetiroModel> getAllWithdrawalsNotSentToTheServer()
        {
            List<RetiroModel> retirosList = null;
            RetiroModel rm;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_RETIROS + " WHERE " + LocalDatabase.CAMPO_IDSERVER_RETIRO + " = " + 0;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            retirosList = new List<RetiroModel>();
                            while (reader.Read())
                            {
                                rm = new RetiroModel();
                                rm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_RETIRO].ToString().Trim());
                                rm.number = Convert.ToInt32(reader[LocalDatabase.CAMPO_NUMERO_RETIRO].ToString().Trim());
                                rm.idUsuario = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDUSUARIO_RETIRO].ToString().Trim());
                                rm.claveUsuario = reader[LocalDatabase.CAMPO_CLAVEUSUARIO_RETIRO].ToString().Trim();
                                rm.fechaHora = reader[LocalDatabase.CAMPO_FECHAHORA_RETIRO].ToString().Trim();
                                rm.concept = reader[LocalDatabase.CAMPO_CONCEPT_RETIRO].ToString().Trim();
                                rm.description = reader[LocalDatabase.CAMPO_DESCRIPTION_RETIRO].ToString().Trim();
                                retirosList.Add(rm);
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
            return retirosList;
        }

        public static List<ClsRetirosModel> getAllWithdrawalsNotSendedToTheServer()
        {
            List<ClsRetirosModel> retirosList = null;
            ClsRetirosModel rm;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_RETIROS + " WHERE " + LocalDatabase.CAMPO_IDSERVER_RETIRO + " = " + 0;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            retirosList = new List<ClsRetirosModel>();
                            while (reader.Read())
                            {
                                rm = new ClsRetirosModel();
                                rm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_RETIRO].ToString().Trim());
                                rm.number = Convert.ToInt32(reader[LocalDatabase.CAMPO_NUMERO_RETIRO].ToString().Trim());
                                rm.idUsuario = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDUSUARIO_RETIRO].ToString().Trim());
                                rm.claveUsuario = reader[LocalDatabase.CAMPO_CLAVEUSUARIO_RETIRO].ToString().Trim();
                                rm.fechaHora = reader[LocalDatabase.CAMPO_FECHAHORA_RETIRO].ToString().Trim();
                                rm.concept = reader[LocalDatabase.CAMPO_CONCEPT_RETIRO].ToString().Trim();
                                rm.description = reader[LocalDatabase.CAMPO_DESCRIPTION_RETIRO].ToString().Trim();
                                rm.montos = MontoRetiroModel.getAllAmountsFromAWithdrawals(rm.id);
                                retirosList.Add(rm);
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
            return retirosList;
        }

        public static Boolean deleteAllRetiros()
        {
            bool deleted = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "DELETE FROM " + LocalDatabase.TABLA_RETIROS;
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
