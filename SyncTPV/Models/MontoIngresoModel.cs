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
    public class MontoIngresoModel
    {
        public int id { get; set; }
        public int formaCobroId { get; set; }
        public double importe { get; set; }
        public int retiroId { get; set; }
        public int enviado { get; set; }
        public String createdAt { get; set; }
        public String updatedAt { get; set; }

        public static int createEntryAmount(int fcId, double importe, int retiroId, String createdAt, String updatedAt)
        {
            int montoId = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                int lastId = getLastId() + 1;
                String query = "INSERT INTO " + LocalDatabase.TABLA_MONTOINGRESO + " VALUES(@id, @fcId, @importe, " +
                    "@retiroId, @sended, @createdAt, @updatedAt)";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", lastId);
                    command.Parameters.AddWithValue("@fcId", fcId);
                    command.Parameters.AddWithValue("@importe", importe);
                    command.Parameters.AddWithValue("@retiroId", retiroId);
                    command.Parameters.AddWithValue("@sended", 0);
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

        public static Boolean updateEntryAmount(int fcId, double importe, int ingresoId, String updatedAt)
        {
            Boolean resp = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_MONTOINGRESO + " SET " + 
                    LocalDatabase.CAMPO_IMPORTE_MONTOINGRESO + " = @amount, " +
                    LocalDatabase.CAMPO_UPDATEDAT_MONTOINGRESO+ " = @updatedAt "+
                    " WHERE " + LocalDatabase.CAMPO_FORMACOBROID_MONTOINGRESO + " = @fcEntry AND " +
                        LocalDatabase.CAMPO_INGRESOID_MONTOINGRESO + " = @ingresoId";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@amount", importe);
                    command.Parameters.AddWithValue("@fcEntry", fcId);
                    command.Parameters.AddWithValue("@ingresoId", ingresoId);
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

        public static Boolean updateSentFieldOfEntryAmounts(int idIngresoApp)
        {
            Boolean updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_MONTOINGRESO + " SET " + LocalDatabase.CAMPO_ENVIADO_INGRESO + " = @sended WHERE " +
                    LocalDatabase.CAMPO_INGRESOID_MONTOINGRESO + " = @idIngresoApp";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@sended", 1);
                    command.Parameters.AddWithValue("@idIngresoApp", idIngresoApp);
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

        public static List<MontoIngresoModel> getAllAmountsFromAnEntry(int idIngresoApp)
        {
            List<MontoIngresoModel> montosList = null;
            MontoIngresoModel mrm;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_MONTOINGRESO + " WHERE " +
                        LocalDatabase.CAMPO_INGRESOID_MONTOINGRESO + " = @idIngresoApp";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idIngresoApp", idIngresoApp);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            montosList = new List<MontoIngresoModel>();
                            while (reader.Read())
                            {
                                mrm = new MontoIngresoModel();
                                mrm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_MONTOINGRESO].ToString().Trim());
                                mrm.formaCobroId = Convert.ToInt32(reader[LocalDatabase.CAMPO_FORMACOBROID_MONTOINGRESO].ToString().Trim());
                                mrm.importe = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMPORTE_MONTOINGRESO].ToString().Trim());
                                if (reader[LocalDatabase.CAMPO_CREATEDAT_MONTOINGRESO] != DBNull.Value)
                                    mrm.createdAt = reader[LocalDatabase.CAMPO_CREATEDAT_MONTOINGRESO].ToString().Trim();
                                else mrm.createdAt = MetodosGenerales.getCurrentDateAndHour();
                                if (reader[LocalDatabase.CAMPO_UPDATEDAT_MONTOINGRESO] != DBNull.Value)
                                    mrm.updatedAt = reader[LocalDatabase.CAMPO_UPDATEDAT_MONTOINGRESO].ToString().Trim();
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

        public static List<ClsMontoIngresoModel> getAllMontosDeUnIngreso(int idIngresoApp)
        {
            List<ClsMontoIngresoModel> montosList = null;
            ClsMontoIngresoModel mrm;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_MONTOINGRESO + " WHERE " +
                        LocalDatabase.CAMPO_INGRESOID_MONTOINGRESO + " = @idIngresoApp";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idIngresoApp", idIngresoApp);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            montosList = new List<ClsMontoIngresoModel>();
                            while (reader.Read())
                            {
                                mrm = new ClsMontoIngresoModel();
                                mrm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_MONTOINGRESO].ToString().Trim());
                                mrm.formaCobroId = Convert.ToInt32(reader[LocalDatabase.CAMPO_FORMACOBROID_MONTOINGRESO].ToString().Trim());
                                mrm.importe = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMPORTE_MONTOINGRESO].ToString().Trim());
                                if (reader[LocalDatabase.CAMPO_CREATEDAT_MONTOINGRESO] != DBNull.Value)
                                    mrm.createdAt = Convert.ToDateTime(reader[LocalDatabase.CAMPO_CREATEDAT_MONTOINGRESO].ToString().Trim());
                                else mrm.createdAt = Convert.ToDateTime(MetodosGenerales.getCurrentDateAndHour());
                                if (reader[LocalDatabase.CAMPO_UPDATEDAT_MONTOINGRESO] != DBNull.Value)
                                    mrm.updatedAt = Convert.ToDateTime(reader[LocalDatabase.CAMPO_UPDATEDAT_MONTOINGRESO].ToString().Trim());
                                else mrm.updatedAt = Convert.ToDateTime(MetodosGenerales.getCurrentDateAndHour());
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

        public static int getLastId()
        {
            int lastId = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT MAX(" + LocalDatabase.CAMPO_ID_MONTOINGRESO + ") FROM " + LocalDatabase.TABLA_MONTOINGRESO + " ORDER BY " +
                    LocalDatabase.CAMPO_ID_MONTOINGRESO + " DESC LIMIT 1";
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
            catch (SQLiteException Ex)
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

        public static double getAllEntryAmountsFromACollectionForm(int fcId)
        {
            double totalRetirado = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_IMPORTE_MONTOINGRESO + " FROM " + LocalDatabase.TABLA_MONTOINGRESO +
                        " WHERE " + LocalDatabase.CAMPO_FORMACOBROID_MONTOINGRESO + " = " + fcId;
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

        public static int getLastId(SQLiteConnection db)
        {
            int lastId = 0;
            try
            {
                String query = "SELECT MAX(" + LocalDatabase.CAMPO_ID_MONTOINGRESO + ") FROM " + LocalDatabase.TABLA_MONTOINGRESO + " ORDER BY " +
                    LocalDatabase.CAMPO_ID_MONTOINGRESO + " DESC LIMIT 1";
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
                
            }
            return lastId;
        }

        public static double getTotalEntryFromAPaymentMethod(int fcId, int ingresoId)
        {
            double total = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_IMPORTE_MONTOINGRESO + " FROM " + LocalDatabase.TABLA_MONTOINGRESO +
                        " WHERE " + LocalDatabase.CAMPO_FORMACOBROID_MONTOINGRESO + " = @fcId AND " + 
                        LocalDatabase.CAMPO_INGRESOID_MONTOINGRESO + " = @ingresoId";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@fcId", fcId);
                    command.Parameters.AddWithValue("@ingresoId", ingresoId);
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

        public static double getTotalOfAnEntry(int idIngreso)
        {
            double total = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT SUM(" + LocalDatabase.CAMPO_IMPORTE_MONTOINGRESO + ") FROM " + LocalDatabase.TABLA_MONTOINGRESO + " WHERE " +
                        LocalDatabase.CAMPO_INGRESOID_MONTOINGRESO + " = " + idIngreso;
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

        public static Boolean removeAnAmountFromAnEntry(int fcId, int idIngreso)
        {
            Boolean removed = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "DELETE FROM " + LocalDatabase.TABLA_MONTOINGRESO + " WHERE " +
                    LocalDatabase.CAMPO_FORMACOBROID_MONTOINGRESO + " = @fcId AND " + 
                    LocalDatabase.CAMPO_INGRESOID_MONTOINGRESO + " = @idIngreso";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@fcId", fcId);
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

        public static int checkForEntryAmounts(int ingresoId)
        {
            int quantity = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_MONTOINGRESO + " WHERE " +
                        LocalDatabase.CAMPO_INGRESOID_MONTOINGRESO + " = @ingresoId";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@ingresoId", ingresoId);
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

        public static Boolean removeAllAmountsFromAnEntry(int ingresoId)
        {
            Boolean removed = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "DELETE FROM " + LocalDatabase.TABLA_MONTOINGRESO + " WHERE " +
                    LocalDatabase.CAMPO_INGRESOID_MONTOINGRESO + " = @ingresoId";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@ingresoId", ingresoId);
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

        public static bool deleteAllMontoIngreso()
        {
            bool deleted = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "DELETE FROM " + LocalDatabase.TABLA_MONTOINGRESO;
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
