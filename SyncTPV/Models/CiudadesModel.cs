using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using wsROMClases.Models.Panel;

namespace SyncTPV
{
    public class CiudadesModel
    {
        public int CIUDAD_ID { set; get; }
        public String valor { get; set; }
        public string NOMBRE { set; get; }
        public int ESTADO_ID { set; get; }

        public static int saveAllCities(List<ClsCiudadesModel> citiesList)
        {
            int lastId = 0;
            if (citiesList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (var city in citiesList)
                    {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_CIUDADES + " VALUES (@id, @nombre, @estadoId)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@id", city.CIUDAD_ID);
                            command.Parameters.AddWithValue("@nombre", city.NOMBRE);
                            command.Parameters.AddWithValue("@estadoId", city.ESTADO_ID);
                            int recordInserted = command.ExecuteNonQuery();
                            if (recordInserted != 0)
                                lastId = city.CIUDAD_ID;
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
            return lastId;
        }

        public static int saveAllCitiesLAN(List<ClsCiudadesModel> citiesList)
        {
            int count = 0;
            if (citiesList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (var city in citiesList)
                    {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_CIUDADES + " VALUES (@id, @nombre, @estadoId)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@id", city.CIUDAD_ID);
                            command.Parameters.AddWithValue("@nombre", city.NOMBRE);
                            command.Parameters.AddWithValue("@estadoId", city.ESTADO_ID);
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

        public static int getNumberOfCitiesSaved()
        {
            int number = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_CIUDADES;
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
            catch (Exception ex)
            {
                SECUDOC.writeLog(ex.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return number;
        }

        public static List<ClsCiudadesModel> getAllCiudades(String query)
        {
            List<ClsCiudadesModel> ciudadesList = null;
            ClsCiudadesModel ciudad = null;
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
                            ciudadesList = new List<ClsCiudadesModel>();
                            while (reader.Read())
                            {
                                ciudad = new ClsCiudadesModel();
                                ciudad.CIUDAD_ID = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_CIUDAD].ToString().Trim());
                                ciudad.NOMBRE = reader[LocalDatabase.CAMPO_NOMBRE_CIUDAD].ToString().Trim();
                                ciudad.ESTADO_ID = Convert.ToInt32(reader[LocalDatabase.CAMPO_ESTADOID_CIUDAD].ToString().Trim());
                                ciudadesList.Add(ciudad);
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
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
            return ciudadesList;
        }

        public static Boolean deleteAllCities()
        {
            bool deleted = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "DELETE FROM " + LocalDatabase.TABLA_CIUDADES;
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
