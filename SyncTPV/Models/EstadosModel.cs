using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using wsROMClases.Models;

namespace SyncTPV
{

    public class EstadosModel
    {

        public static int saveAllEstados(List<ClsEstadosModel> estadosList)
        {
            int lastId = 0;
            if (estadosList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (var state in estadosList)
                    {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_ESTADOS + " VALUES (@id, @nombre)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@id", state.id);
                            command.Parameters.AddWithValue("@nombre", state.nombre);
                            int recordInserted = command.ExecuteNonQuery();
                            if (recordInserted != 0)
                                lastId = state.id;
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

        public static int saveAllEstadosLAN(List<ClsEstadosModel> estadosList)
        {
            int count = 0;
            if (estadosList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (var state in estadosList)
                    {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_ESTADOS + " VALUES (@id, @nombre)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@id", state.id);
                            command.Parameters.AddWithValue("@nombre", state.nombre);
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

        public static Boolean deleteAlEstados()
        {
            bool deleted = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "DELETE FROM " + LocalDatabase.TABLA_ESTADOS;
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

        public static List<ClsEstadosModel> getAllEstados(String query)
        {
            List<ClsEstadosModel> estadosList = null;
            ClsEstadosModel estado = null;
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
                            estadosList = new List<ClsEstadosModel>();
                            while (reader.Read())
                            {
                                estado = new ClsEstadosModel();
                                estado.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_ESTADO].ToString().Trim());
                                estado.nombre = reader[LocalDatabase.CAMPO_NOMBRE_ESTADO].ToString().Trim();
                                estadosList.Add(estado);
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            } catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            } finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return estadosList;
        }

        public static int getTotalStates()
        {
            int records = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT COUNT(*) FROM "+LocalDatabase.TABLA_ESTADOS;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    records = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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
            return records;
        }
    }
}
