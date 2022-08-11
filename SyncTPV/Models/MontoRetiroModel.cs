using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using wsROMClases;
using wsROMClases.Models.Panel;

namespace SyncTPV.Models
{
    public class MontoRetiroModel
    {
        public int id { get; set; }
        public int formaCobroId { get; set; }
        public double importe { get; set; }
        public int retiroId { get; set; }
        public int enviado { get; set; }
        public String createdAt { get; set; }
        public String updatedAt { get; set; }

        public static int createNewWithdrawalAmount(int fcId, double importe, int retiroId, String createdAt, String updatedAt)
        {
            int montoId = 0;
            int lastId = getLastId() + 1;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "INSERT INTO " + LocalDatabase.TABLA_MONTORETIROS + " (" + LocalDatabase.CAMPO_ID_MONTORETIROS + ", " + LocalDatabase.CAMPO_FORMACOBROID_MONTORETIROS + ", " +
                    LocalDatabase.CAMPO_IMPORTE_MONTORETIROS + ", " + LocalDatabase.CAMPO_RETIROID_MONTORETIRO + ", createdAt, updatedAt) VALUES(@id, @fcId, @importe, @retiroId, " +
                    "@createdAt, @updatedAt)";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", lastId);
                    command.Parameters.AddWithValue("@fcId", fcId);
                    command.Parameters.AddWithValue("@importe", importe);
                    command.Parameters.AddWithValue("@retiroId", retiroId);
                    command.Parameters.AddWithValue("@createdAt", createdAt);
                    command.Parameters.AddWithValue("@updatedAt", updatedAt);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        montoId = lastId;
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
            return montoId;
        }

        public static int getLastId()
        {
            int lastId = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT MAX(" + LocalDatabase.CAMPO_ID_MONTORETIROS + ") FROM " + LocalDatabase.TABLA_MONTORETIROS + " ORDER BY " +
                    LocalDatabase.CAMPO_ID_MONTORETIROS + " DESC LIMIT 1";
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

        public static Boolean updateWithdrawalAmount(int fcId, double importe, int retiroId, String updatedAt)
        {
            Boolean resp = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "UPDATE " + LocalDatabase.TABLA_MONTORETIROS + " SET " + 
                    LocalDatabase.CAMPO_IMPORTE_MONTORETIROS + " = " + importe +", "+
                    LocalDatabase.CAMPO_UPDATEDAT_MONTORETIRO+" = @updatedAt "+
                    " WHERE " + LocalDatabase.CAMPO_FORMACOBROID_MONTORETIROS + " = " + fcId + " AND " +
                        LocalDatabase.CAMPO_RETIROID_MONTORETIRO + " = " + retiroId;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@updatedAt", updatedAt);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        resp = true;
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

        public static double getTotalWithdrawnFromAPaymentMethod(int fcId, int retiroId)
        {
            double total = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_IMPORTE_MONTORETIROS + " FROM " + LocalDatabase.TABLA_MONTORETIROS +
                        " WHERE " + LocalDatabase.CAMPO_FORMACOBROID_MONTORETIROS + " = " + fcId + " AND " + LocalDatabase.CAMPO_RETIROID_MONTORETIRO + " = " + retiroId;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                total = reader.GetDouble(0);
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

        public static Boolean removeAnAmountFromAWithdrawal(int fcId, int retiroId)
        {
            Boolean removed = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "DELETE FROM " + LocalDatabase.TABLA_MONTORETIROS + " WHERE " + LocalDatabase.CAMPO_FORMACOBROID_MONTORETIROS + " = " + fcId +
                        " AND " + LocalDatabase.CAMPO_RETIROID_MONTORETIRO + " = " + retiroId;
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

        public static List<MontoRetiroModel> getAllAmountsFromAWithdrawal(int idRetiroApp)
        {
            List<MontoRetiroModel> montosList = null;
            MontoRetiroModel mrm;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_MONTORETIROS + " WHERE " +
                        LocalDatabase.CAMPO_RETIROID_MONTORETIRO + " = " + idRetiroApp;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            montosList = new List<MontoRetiroModel>();
                            while (reader.Read())
                            {
                                mrm = new MontoRetiroModel();
                                mrm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_MONTORETIROS].ToString().Trim());
                                mrm.formaCobroId = Convert.ToInt32(reader[LocalDatabase.CAMPO_FORMACOBROID_MONTORETIROS].ToString().Trim());
                                mrm.importe = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMPORTE_MONTORETIROS].ToString().Trim());
                                if (reader[LocalDatabase.CAMPO_CREATEDAT_MONTORETIRO] != DBNull.Value)
                                    mrm.createdAt = reader[LocalDatabase.CAMPO_CREATEDAT_MONTORETIRO].ToString().Trim();
                                else mrm.createdAt = MetodosGenerales.getCurrentDateAndHour();
                                if (reader[LocalDatabase.CAMPO_UPDATEDAT_MONTORETIRO] != DBNull.Value)
                                    mrm.updatedAt = reader[LocalDatabase.CAMPO_UPDATEDAT_MONTORETIRO].ToString().Trim();
                                else mrm.updatedAt = MetodosGenerales.getCurrentDateAndHour();
                                montosList.Add(mrm);
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
            return montosList;
        }

        public static List<ClsMontosRetirosModel> getAllAmountsFromAWithdrawals(int idRetiroApp)
        {
            List<ClsMontosRetirosModel> montosList = null;
            ClsMontosRetirosModel mrm;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_MONTORETIROS + " WHERE " +
                        LocalDatabase.CAMPO_RETIROID_MONTORETIRO + " = " + idRetiroApp;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            montosList = new List<ClsMontosRetirosModel>();
                            while (reader.Read())
                            {
                                mrm = new ClsMontosRetirosModel();
                                mrm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_MONTORETIROS].ToString().Trim());
                                mrm.formaCobroId = Convert.ToInt32(reader[LocalDatabase.CAMPO_FORMACOBROID_MONTORETIROS].ToString().Trim());
                                mrm.importe = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMPORTE_MONTORETIROS].ToString().Trim());
                                if (reader[LocalDatabase.CAMPO_CREATEDAT_MONTORETIRO] != DBNull.Value)
                                    mrm.createdAt = Convert.ToDateTime(reader[LocalDatabase.CAMPO_CREATEDAT_MONTORETIRO].ToString().Trim());
                                if (reader[LocalDatabase.CAMPO_UPDATEDAT_MONTORETIRO] != DBNull.Value)
                                    mrm.updatedAt = Convert.ToDateTime(reader[LocalDatabase.CAMPO_UPDATEDAT_MONTORETIRO].ToString().Trim());
                                montosList.Add(mrm);
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
            return montosList;
        }

        public static Boolean removeAllAmountsFromAWithdrawal(int retiroId)
        {
            Boolean removed = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "DELETE FROM " + LocalDatabase.TABLA_MONTORETIROS + " WHERE " + LocalDatabase.CAMPO_RETIROID_MONTORETIRO + " = " + retiroId;
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

        public static int checkForWithdrawalAmounts(int retiroId)
        {
            int quantity = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_MONTORETIROS + " WHERE " +
                        LocalDatabase.CAMPO_RETIROID_MONTORETIRO + " = " + retiroId;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    quantity = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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
            return quantity;
        }

        public static Boolean updateSentFieldOfWithdrawalAmounts(int idRetiroApp)
        {
            Boolean updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_MONTORETIROS + " SET " + LocalDatabase.CAMPO_ENVIADO_RETIRO + " = @sended WHERE " +
                    LocalDatabase.CAMPO_RETIROID_MONTORETIRO + " = @idRetiroApp";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@sended",1);
                    command.Parameters.AddWithValue("@idRetiroApp", idRetiroApp);
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

        public static double getAllWithdrawalAmountsFromACollectionForm(int fcId)
        {
            double totalRetirado = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_IMPORTE_MONTORETIROS + " FROM " + LocalDatabase.TABLA_MONTORETIROS +
                        " WHERE " + LocalDatabase.CAMPO_FORMACOBROID_MONTORETIROS + " = " + fcId;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    totalRetirado += Convert.ToDouble(reader.GetValue(0).ToString().Trim());
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
            return totalRetirado;
        }

        public static double getTotalOfAWithdrawal(int retiroId)
        {
            double total = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT SUM(" + LocalDatabase.CAMPO_IMPORTE_MONTORETIROS + ") FROM " + LocalDatabase.TABLA_MONTORETIROS + " WHERE " +
                        LocalDatabase.CAMPO_RETIROID_MONTORETIRO + " = " + retiroId;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    total = Convert.ToDouble(reader.GetValue(0).ToString().Trim());
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

        public static DataTable getAllAmountsFromAWithdrawalDt(String query)
        {
            DataTable dt = null;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                    adapter.SelectCommand = command;
                    dt = new DataTable();
                    adapter.Fill(dt);
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
            return dt;
        }

        public static Boolean deleteAllAmountsFromAWithdrawal(SQLiteConnection db, int idWithdrawalApp)
        {
            Boolean deleted = false;
            try
            {
                String query = "DELETE FROM " + LocalDatabase.TABLA_MONTORETIROS + " WHERE " + LocalDatabase.CAMPO_RETIROID_MONTORETIRO + 
                    " = " + idWithdrawalApp;
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

        public static Boolean deleteAllMontoRetiros()
        {
            bool deleted = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "DELETE FROM " + LocalDatabase.TABLA_MONTORETIROS;
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
