using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using wsROMClases.Models.Panel;

namespace SyncTPV
{
    public class ZonasDeClientesModel
    {
        public int ZONA_CLIENTE_ID { get; set; }
        public string NOMBRE { get; set; }

        public static int saveAllZones(List<ZonasDeClientesModel> zonesList)
        {
            int count = 0;
            if (zonesList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (var ZonaCliente in zonesList)
                    {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_ZONACLIENTES + " VALUES (@id, @nombre)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@id", ZonaCliente.ZONA_CLIENTE_ID);
                            command.Parameters.AddWithValue("@nombre", ZonaCliente.NOMBRE);
                            int recordInserted = command.ExecuteNonQuery();
                            if (recordInserted != 0)
                                count++;
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
            }
            return count;
        }

        public static int saveAllZonesLAN(List<ClsZonasDeClientesModel> zonesList)
        {
            int count = 0;
            if (zonesList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (var ZonaCliente in zonesList)
                    {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_ZONACLIENTES + " VALUES (@id, @nombre)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@id", ZonaCliente.ZONA_CLIENTE_ID);
                            command.Parameters.AddWithValue("@nombre", ZonaCliente.NOMBRE);
                            int recordInserted = command.ExecuteNonQuery();
                            if (recordInserted != 0)
                                count++;
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
            }
            return count;
        }

        public static List<ZonasDeClientesModel> getAllCustomersZones()
        {
            List<ZonasDeClientesModel> zonesList = null;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_ZONACLIENTES;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            zonesList = new List<ZonasDeClientesModel>();
                            while (reader.Read())
                            {
                                ZonasDeClientesModel Zonaclientes = new ZonasDeClientesModel();
                                Zonaclientes.ZONA_CLIENTE_ID = Convert.ToInt32(reader[LocalDatabase.CAMPO_ZONA_CLIENTE]);
                                Zonaclientes.NOMBRE = reader[LocalDatabase.CAMPO_ZONA_NOMBRE].ToString().Trim();
                                zonesList.Add(Zonaclientes);
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
            return zonesList;
        }

        public static String getNameFromAZone(int idZone)
        {
            String zoneName = "";
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_ZONA_NOMBRE + " FROM " + LocalDatabase.TABLA_ZONACLIENTES + " WHERE " + LocalDatabase.CAMPO_ZONA_CLIENTE +
                    " = " + idZone;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                zoneName = reader[LocalDatabase.CAMPO_ZONA_NOMBRE].ToString().Trim();
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
            return zoneName;
        }

        public static Boolean deleteAllCustomersZones()
        {
            bool deleted = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "DELETE FROM " + LocalDatabase.TABLA_ZONACLIENTES;
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
