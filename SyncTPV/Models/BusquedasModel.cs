using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncTPV.Models
{
    public class BusquedasModel
    {
        public static readonly int MODEL_ITEMS = 1;
        public static readonly int MODEL_CUSTOMERS = 2;
        public int id { get; set; }
        public int model { get; set; }
        public int matchPosition { get; set; } //0 = cualquier lado, 1 = al principio, 2 = al final
        public int consideredFields { get; set; } //0 = codigo y nombre, 1 = código, 2 = nombre

        public static ExpandoObject createBusqueda(int model, int matchPosition, int consideredFields)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "INSERT INTO "+LocalDatabase.TABLA_BUSQUEDAS+ " VALUES(@id, @model, @matchPosition, @consideredFields);";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int idCreated = getLastId() + 1;
                    command.Parameters.AddWithValue("@id", idCreated);
                    command.Parameters.AddWithValue("@model", model);
                    command.Parameters.AddWithValue("@matchPosition", matchPosition);
                    command.Parameters.AddWithValue("@consideredFields", consideredFields);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        value = idCreated;
                }
            } catch (SQLiteException e)
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

        public static ExpandoObject updateBusqueda(int model, int matchPosition, int consideredFields)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_BUSQUEDAS + " SET " +
                    LocalDatabase.CAMPO_POSICIONCOINCIDENCIA_BUSQUEDAS+" = @matchPosition, "+
                    LocalDatabase.CAMPO_CAMPOSCONCIDERADOS_BUSQUEDAS+" = @consideredFields WHERE "+
                    LocalDatabase.CAMPO_MODELO_BUSQUEDAS + " = @model";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@model", model);
                    command.Parameters.AddWithValue("@matchPosition", matchPosition);
                    command.Parameters.AddWithValue("@consideredFields", consideredFields);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        value = 1;
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

        public static int getLastId()
        {
            int lastId = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT MAX(id) AS id FROM "+LocalDatabase.TABLA_BUSQUEDAS+" LIMIT 1";
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
            } catch (SQLiteException e)
            {
                SECUDOC.writeWeight(e.ToString());
            } finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return lastId;
        }

        public static BusquedasModel getBusqueda(int model)
        {
            BusquedasModel bm = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_BUSQUEDAS + " WHERE "+
                    LocalDatabase.CAMPO_MODELO_BUSQUEDAS+" = @model";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@model", model);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                bm = new BusquedasModel();
                                bm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_BUSQUEDAS].ToString().Trim());
                                bm.model = Convert.ToInt32(reader[LocalDatabase.CAMPO_MODELO_BUSQUEDAS].ToString().Trim());
                                bm.matchPosition = Convert.ToInt32(reader[LocalDatabase.CAMPO_POSICIONCOINCIDENCIA_BUSQUEDAS].ToString().Trim());
                                bm.consideredFields = Convert.ToInt32(reader[LocalDatabase.CAMPO_CAMPOSCONCIDERADOS_BUSQUEDAS].ToString().Trim());
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeWeight(e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return bm;
        }

        public static ExpandoObject busquedaModelExist(int model)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_BUSQUEDAS + " WHERE "+
                    LocalDatabase.CAMPO_MODELO_BUSQUEDAS+" = @model";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@model", model);
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
                SECUDOC.writeWeight(e.ToString());
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


        public class MatchsPositionsBusquedas
        {
            public int value { get; set; }
            public string name { get; set; }
        }

    }
}
