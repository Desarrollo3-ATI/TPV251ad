using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Data;
using System.Data.SQLite;

namespace SyncTPV.Models
{
    public class AperturaTurnoModel
    {
        public int id { get; set; }
        public int userId { get; set; }
        public double importe { get; set; }
        public String fechaHora { get; set; }
        public String createdAt { get; set; }
        public String codigoCaja { get; set; }
        public int status { get; set; }
        public int serverId { get; set; }

        public static int createUpdateOrDelete(String query) {
            int response = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        response = 1;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog("Exception createUpdateOrDelete: " + e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return response;
        }

        public static int saveNewApertura(int id, int userId, double amount, String dateTimeNow, String dateNow,
            int status, int idServer)
        {
            int response = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "INSERT INTO " + LocalDatabase.TABLA_APERTURATURNO + " VALUES(@id, @userId, @amount, @dateTimeNow, " +
                    "@dateNow, @status, @idServer)";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@amount", amount);
                    command.Parameters.AddWithValue("@dateTimeNow", dateTimeNow);
                    command.Parameters.AddWithValue("@dateNow", dateNow);
                    command.Parameters.AddWithValue("@status", status);
                    command.Parameters.AddWithValue("@idServer", idServer);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        response = 1;
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
            return response;
        }

        public static int marcarComoEnviado(int idServer, int userId, String createdAt)
        {
            int response = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_APERTURATURNO + " SET " + LocalDatabase.CAMPO_STATUS_APERTURATURNO + " = " + 1 + ", " +
                            LocalDatabase.CAMPO_SERVERID_APERTURATURNO + " = @idServer WHERE " +
                            LocalDatabase.CAMPO_USERID_APERTURATURNO + " = @userId " +
                            "AND " + LocalDatabase.CAMPO_CREATEDAT_APERTURATURNO + " = @createdAt";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idServer", idServer);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@createdAt", createdAt);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        response = 1;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog("Exception createUpdateOrDelete: " + e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return response;
        }

        public static AperturaTurnoModel getARecord(String query)
        {
            AperturaTurnoModel atm = null;
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
                                atm = new AperturaTurnoModel();
                                atm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_APERTURATURNO].ToString().Trim());
                                atm.userId = Convert.ToInt32(reader[LocalDatabase.CAMPO_USERID_APERTURATURNO].ToString().Trim());
                                atm.importe = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMPORTE_APERTURATURNO].ToString().Trim());
                                atm.fechaHora = reader[LocalDatabase.CAMPO_FECHAHORA_APERTURATURNO].ToString().Trim();
                                atm.createdAt = reader[LocalDatabase.CAMPO_CREATEDAT_APERTURATURNO].ToString().Trim();
                                atm.status = Convert.ToInt32(reader[LocalDatabase.CAMPO_STATUS_APERTURATURNO].ToString().Trim());
                                atm.serverId = Convert.ToInt32(reader[LocalDatabase.CAMPO_SERVERID_APERTURATURNO].ToString().Trim());
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog("Exception getARecord: " + e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return atm;
        }


        public static bool getALastRecord(String coneccion,String query)
        {
            bool ocupado = true;
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
                            
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog("Exception getARecord: " + e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return ocupado;
        }
        public static AperturaTurnoModel getARecordWithParameters(String dateTime, int idUser)
        {
            AperturaTurnoModel atm = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_APERTURATURNO + " WHERE " +
                    LocalDatabase.CAMPO_CREATEDAT_APERTURATURNO + " = @dateTime AND " +
                    LocalDatabase.CAMPO_USERID_APERTURATURNO + " = @idUser";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@dateTime", dateTime);
                    command.Parameters.AddWithValue("@idUser", idUser);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                atm = new AperturaTurnoModel();
                                atm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_APERTURATURNO].ToString().Trim());
                                atm.userId = Convert.ToInt32(reader[LocalDatabase.CAMPO_USERID_APERTURATURNO].ToString().Trim());
                                atm.importe = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMPORTE_APERTURATURNO].ToString().Trim());
                                atm.fechaHora = reader[LocalDatabase.CAMPO_FECHAHORA_APERTURATURNO].ToString().Trim();
                                atm.createdAt = reader[LocalDatabase.CAMPO_CREATEDAT_APERTURATURNO].ToString().Trim();
                                atm.status = Convert.ToInt32(reader[LocalDatabase.CAMPO_STATUS_APERTURATURNO].ToString().Trim());
                                atm.serverId = Convert.ToInt32(reader[LocalDatabase.CAMPO_SERVERID_APERTURATURNO].ToString().Trim());
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
            return atm;
        }

        public static int getIntValue(String query)
        {
            int response = 0;
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
                                    response = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog("Exception getARecord: " + e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return response;
        }

        public static int getLastId()
        {
            int lastId = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT "+LocalDatabase.CAMPO_ID_APERTURATURNO+" FROM "+LocalDatabase.TABLA_APERTURATURNO+" ORDER BY "+
                    LocalDatabase.CAMPO_ID_APERTURATURNO+" DESC LIMIT 1";
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
                SECUDOC.writeLog("Exception getARecord: " + e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return lastId;
        }

        public static bool checkIfAperturaExist(int userId, String createdAt)
        {
            bool exist = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT COUNT(*) FROM AperturaTurno WHERE user_id = @userId AND created_at = @createdAt";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@createdAt", createdAt);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) > 0)
                                        exist = true;
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
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
            return exist;
        }

        public static double getImporteDeAperturaActual()
        {
            double amount = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT "+LocalDatabase.CAMPO_IMPORTE_APERTURATURNO+" FROM "+LocalDatabase.TABLA_APERTURATURNO+" Order by "+
                    LocalDatabase.CAMPO_ID_APERTURATURNO + " DESC LIMIT 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("fechaActual",DateTime.Now.ToString("yyyyMMdd"));
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    amount = Convert.ToDouble(reader.GetValue(0).ToString().Trim());
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog("Exception getARecord: " + e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return amount;
        }

    }

    public class ResponseAperturaTurno
    {
        public AperturaTurnoModel response { get; set; }
    }
}
