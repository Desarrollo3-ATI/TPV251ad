using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace SyncTPV.Models
{
    public class ClsBanderasCargaInicialModel
    {
        public int id { get; set; }
        public String name { get; set; }
        public int registrosObtenidos { get; set; }
        public int registrosGuardados { get; set; }
        public String errorObtenido { get; set; }
        public String createdAt { get; set; }
        public String updatedAt { get; set; }

        public static List<ClsBanderasCargaInicialModel> getAllDownloadsMethods(String query)
        {
            List<ClsBanderasCargaInicialModel> downloadsList = null;
            ClsBanderasCargaInicialModel download = null;
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
                            downloadsList = new List<ClsBanderasCargaInicialModel>();
                            while (reader.Read())
                            {
                                download = new ClsBanderasCargaInicialModel();
                                download.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_BANDERACI].ToString().Trim());
                                download.name = reader[LocalDatabase.CAMPO_NOMBRE_METODO_BANDERACI].ToString().Trim();
                                download.registrosObtenidos = Convert.ToInt32(reader[LocalDatabase.CAMPO_OBTENIDO_METODOBANDERACI].ToString().Trim());
                                download.registrosGuardados = Convert.ToInt32(reader[LocalDatabase.CAMPO_GUARDADO_METODO_BANDERACI].ToString().Trim());
                                download.errorObtenido = reader[LocalDatabase.CAMPO_ERROR_OBTENIDO_METODOBANDERACI].ToString().Trim();
                                download.createdAt = reader[LocalDatabase.CAMPO_CREATEDAT_METODOBANDERACI].ToString().Trim();
                                download.updatedAt = reader[LocalDatabase.CAMPO_UPDATEDAT_METODOBANDERACI].ToString().Trim();
                                downloadsList.Add(download);
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return downloadsList;
        }

        public static int createRecordsForCI(List<String> ciMethodsName)
        {
            int resp = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                int hasFlag = 0;
                String query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_BANDERASCI;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                hasFlag = reader.GetInt32(0);
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
                if (hasFlag <= 0)
                {
                    int count = 0;
                    if (ciMethodsName != null)
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            String queryBanderas = "INSERT INTO " + LocalDatabase.TABLA_BANDERASCI + " (" + LocalDatabase.CAMPO_ID_BANDERACI + ", " +
                                LocalDatabase.CAMPO_NOMBRE_METODO_BANDERACI + ", " + LocalDatabase.CAMPO_OBTENIDO_METODOBANDERACI + ", " +
                                LocalDatabase.CAMPO_GUARDADO_METODO_BANDERACI + ", " + LocalDatabase.CAMPO_ERROR_OBTENIDO_METODOBANDERACI + ", " +
                                LocalDatabase.CAMPO_CREATEDAT_METODOBANDERACI + ", " + LocalDatabase.CAMPO_UPDATEDAT_METODOBANDERACI + ") VALUES(@" +
                                LocalDatabase.CAMPO_ID_BANDERACI + ", @" + LocalDatabase.CAMPO_NOMBRE_METODO_BANDERACI + ", @" +
                                LocalDatabase.CAMPO_OBTENIDO_METODOBANDERACI + ", @" + LocalDatabase.CAMPO_GUARDADO_METODO_BANDERACI + ", @" +
                                LocalDatabase.CAMPO_ERROR_OBTENIDO_METODOBANDERACI + ", @" + LocalDatabase.CAMPO_CREATEDAT_METODOBANDERACI + ", @" +
                                LocalDatabase.CAMPO_UPDATEDAT_METODOBANDERACI + ")";
                            using (SQLiteCommand command = new SQLiteCommand(queryBanderas, db))
                            {
                                int lastId = getLastId() + 1;
                                command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ID_BANDERACI, lastId);
                                command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_NOMBRE_METODO_BANDERACI, ciMethodsName[i]);
                                command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_OBTENIDO_METODOBANDERACI, 0);
                                command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_GUARDADO_METODO_BANDERACI, 0);
                                command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ERROR_OBTENIDO_METODOBANDERACI, "");
                                command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CREATEDAT_METODOBANDERACI, MetodosGenerales.getCurrentDateAndHour());
                                command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_UPDATEDAT_METODOBANDERACI, "");
                                int records = command.ExecuteNonQuery();
                                if (records > 0)
                                    count++;
                            }
                        }
                        if (count == ciMethodsName.Count)
                            resp = 1;
                    }
                }
                else
                {
                    int count = hasFlag;
                    if (ciMethodsName != null)
                    {
                        for (int i = count; i < 20; i++)
                        {
                            String queryBanderas = "INSERT INTO " + LocalDatabase.TABLA_BANDERASCI + " (" + LocalDatabase.CAMPO_ID_BANDERACI + ", " +
                                LocalDatabase.CAMPO_NOMBRE_METODO_BANDERACI + ", " + LocalDatabase.CAMPO_OBTENIDO_METODOBANDERACI + ", " +
                                LocalDatabase.CAMPO_GUARDADO_METODO_BANDERACI + ", " + LocalDatabase.CAMPO_ERROR_OBTENIDO_METODOBANDERACI + ", " +
                                LocalDatabase.CAMPO_CREATEDAT_METODOBANDERACI + ", " + LocalDatabase.CAMPO_UPDATEDAT_METODOBANDERACI + ") VALUES(@" +
                                LocalDatabase.CAMPO_ID_BANDERACI + ", @" + LocalDatabase.CAMPO_NOMBRE_METODO_BANDERACI + ", @" +
                                LocalDatabase.CAMPO_OBTENIDO_METODOBANDERACI + ", @" + LocalDatabase.CAMPO_GUARDADO_METODO_BANDERACI + ", @" +
                                LocalDatabase.CAMPO_ERROR_OBTENIDO_METODOBANDERACI + ", @" + LocalDatabase.CAMPO_CREATEDAT_METODOBANDERACI + ", @" +
                                LocalDatabase.CAMPO_UPDATEDAT_METODOBANDERACI + ")";
                            using (SQLiteCommand command = new SQLiteCommand(queryBanderas, db))
                            {
                                int lastId = getLastId() + 1;
                                command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ID_BANDERACI, lastId);
                                command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_NOMBRE_METODO_BANDERACI, ciMethodsName[count]);
                                command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_OBTENIDO_METODOBANDERACI, 0);
                                command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_GUARDADO_METODO_BANDERACI, 0);
                                command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ERROR_OBTENIDO_METODOBANDERACI, "");
                                command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CREATEDAT_METODOBANDERACI, MetodosGenerales.getCurrentDateAndHour());
                                command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_UPDATEDAT_METODOBANDERACI, "");
                                int records = command.ExecuteNonQuery();
                                if (records > 0)
                                    count++;
                            }
                        }
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return resp;
        }

        public static int getLastId()
        {
            int lastId = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_ID_BANDERACI + " FROM " + LocalDatabase.TABLA_BANDERASCI + " ORDER BY " +
                    LocalDatabase.CAMPO_ID_BANDERACI + " DESC LIMIT 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                lastId = reader.GetInt32(0);
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

        public static String getTheLastCreatedAtOfARecord()
        {
            String createdAt = "";
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_UPDATEDAT_METODOBANDERACI + " FROM " + LocalDatabase.TABLA_BANDERASCI +
                        " WHERE " + LocalDatabase.CAMPO_ID_BANDERACI + " = " + 1 + " OR " + LocalDatabase.CAMPO_ID_BANDERACI + " = " + 2;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                createdAt = reader[LocalDatabase.CAMPO_UPDATEDAT_METODOBANDERACI].ToString().Trim();
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return createdAt;
        }

        public static int updateValuesLocal(SQLiteConnection db, int id, int datos)
        {
            int resp = 0;
            try
            {
                String query = "UPDATE " + LocalDatabase.TABLA_BANDERASCI + " SET " + LocalDatabase.CAMPO_GUARDADO_METODO_BANDERACI + " = @datos, " +
                    LocalDatabase.CAMPO_UPDATEDAT_METODOBANDERACI + " = @fechaHora WHERE " + LocalDatabase.CAMPO_ID_BANDERACI + " = " + id;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@datos", datos);
                    command.Parameters.AddWithValue("@fechaHora", MetodosGenerales.getCurrentDateAndHour());
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        resp = 1;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
            }
            return resp;
        }

        public static int updateValuesServer(SQLiteConnection db, int id, int datos)
        {
            int resp = 0;
            try
            {
                String query = "UPDATE " + LocalDatabase.TABLA_BANDERASCI + " SET " + LocalDatabase.CAMPO_OBTENIDO_METODOBANDERACI + " = @datos, " +
                    LocalDatabase.CAMPO_UPDATEDAT_METODOBANDERACI + " = @fechaHora WHERE " + LocalDatabase.CAMPO_ID_BANDERACI + " = " + id;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@datos", datos);
                    command.Parameters.AddWithValue("@fechaHora", MetodosGenerales.getCurrentDateAndHour());
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        resp = 1;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
            }
            return resp;
        }

    }
}

