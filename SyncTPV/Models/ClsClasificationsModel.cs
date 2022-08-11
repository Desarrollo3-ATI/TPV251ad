using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using wsROMClases;

namespace SyncTPV.Models
{
    public class ClsClasifications
    {
        public int id { get; set; }
        public String nombre { get; set; }
    }
    public class ClsClasificationsModel
    {

        public static int saveAllClasifications(List<ClsClasificacionesModel> clasificationsList)
        {
            int lastId = 0;
            if (clasificationsList != null)
            {
                var db = new SQLiteConnection();                
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (var clasification in clasificationsList)
                    {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_CLASIFICACIONES + " VALUES(@id, @name)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@id", clasification.id);
                            command.Parameters.AddWithValue("@name", clasification.nombre.Trim());
                            int recordSaved = command.ExecuteNonQuery();
                            if (recordSaved != 0)
                                lastId = Convert.ToInt32(clasification.id);
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
            return lastId;
        }

        public static int saveAllClasificationsLAN(List<ClsClasificacionesModel> clasificationsList)
        {
            int count = 0;
            if (clasificationsList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (var clasification in clasificationsList)
                    {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_CLASIFICACIONES + " VALUES(@id, @name)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@id", clasification.id);
                            command.Parameters.AddWithValue("@name", clasification.nombre.Trim());
                            int recordSaved = command.ExecuteNonQuery();
                            if (recordSaved != 0)
                                count++;
                        }
                    }
                }
                catch (SQLiteException e)
                {
                    SECUDOC.writeLog("Exception: " + e.ToString()); ;
                }
                finally
                {
                    if (db != null && db.State == ConnectionState.Open)
                        db.Close();
                }
            }
            return count;
        }

        public static Boolean deleteAllClasifications()
        {
            bool deleted = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "DELETE FROM " + LocalDatabase.TABLA_CLASIFICACIONES;
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
