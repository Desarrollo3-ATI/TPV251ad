using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using wsROMClases.Models.Panel;

namespace SyncTPV
{

    public class FormasDePagoModel
    {
        public int FORMA_COBRO_ID { get; set; }
        public string NOMBRE { get; set; }

        public decimal Total { get; set; }

        public decimal Cambio { get; set; }

        public string Fecha { get; set; }

        public decimal Arqueo { get; set; }

        public decimal CorteCaja { get; set; }

        public static int saveAllFormasDePago(List<FormasDePagoModel> fpList)
        {
            int count = 0;
            if (fpList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (var formaDePago in fpList)
                    {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_FORMASPAGOVENTAS + " VALUES(@id, @nombre)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@id", formaDePago.FORMA_COBRO_ID);
                            command.Parameters.AddWithValue("@nombre", formaDePago.NOMBRE);
                            int recordSaved = command.ExecuteNonQuery();
                            if (recordSaved != 0)
                                count++;
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
            }
            return count;
        }

        public static int saveAllFormasDePagoLAN(List<ClsFormasDePagoModel> fpList)
        {
            int count = 0;
            if (fpList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (var formaDePago in fpList)
                    {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_FORMASPAGOVENTAS + " VALUES(@id, @nombre)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@id", formaDePago.FORMA_COBRO_ID);
                            command.Parameters.AddWithValue("@nombre", formaDePago.NOMBRE);
                            int recordSaved = command.ExecuteNonQuery();
                            if (recordSaved != 0)
                                count++;
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
            }
            return count;
        }

        public static Boolean deleteAllFormasDePago()
        {
            bool deleted = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "DELETE FROM " + LocalDatabase.TABLA_FORMASPAGOVENTAS;
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
