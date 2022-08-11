using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace SyncTPV.Models
{
    public class ClsPositionsModel
    {
        public int id { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public int customerId { get; set; }
        public String date { get; set; }
        public int documentType { get; set; }
        public int agentId { get; set; }
        public String route { get; set; }
        public int idDoctoPanel { get; set; }
        public int enviado { get; set; }

        public static Boolean cancelPositionForADocument(int idDocumento)
        {
            Boolean cancel = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "UPDATE " + LocalDatabase.TABLA_POSICION + " SET " + LocalDatabase.CAMPO_ENVIADO_POSICION + " = @enviado WHERE " +
                    LocalDatabase.CAMPO_DOCTOID_POSICION + " = @idDocument";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@enviado", 1);
                    command.Parameters.AddWithValue("@idDocument", idDocumento);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        cancel = true;
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
            return cancel;
        }

        public static Boolean updateIdDoctoPanelPosition(int id, int idDoctoPanel)
        {
            Boolean updated = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "UPDATE " + LocalDatabase.TABLA_POSICION + " SET " + LocalDatabase.CAMPO_DOCTOID_PANEL_POSICION + " = " + idDoctoPanel + " WHERE " +
                    LocalDatabase.CAMPO_ID_POSICION + " = " + id;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
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

        public static ClsPositionsModel getAllDataForAPositionIdServer(int idDocument)
        {
            ClsPositionsModel pm = null;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_POSICION + " WHERE " +
                        LocalDatabase.CAMPO_DOCTOID_PANEL_POSICION + " = " + idDocument +
                        " AND " + LocalDatabase.CAMPO_ENVIADO_POSICION + " = " + 0;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                pm = new ClsPositionsModel();
                                pm.latitude = Convert.ToDouble(reader[LocalDatabase.CAMPO_LAT_POSICION].ToString().Trim());
                                pm.longitude = Convert.ToDouble(reader[LocalDatabase.CAMPO_LON_POSICION].ToString().Trim());
                                pm.customerId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLIENTE_ID_POSICION].ToString().Trim());
                                pm.date = reader[LocalDatabase.CAMPO_FECHA_POSICION].ToString().Trim();
                                pm.documentType = Convert.ToInt32(reader[LocalDatabase.CAMPO_TIPO_MOV_POSICION].ToString().Trim());
                                pm.agentId = Convert.ToInt32(reader[LocalDatabase.CAMPO_VENDEDOR_ID_POSICION].ToString().Trim());
                                pm.route = reader[LocalDatabase.CAMPO_RUTA_POSICION].ToString().Trim();
                                pm.idDoctoPanel = Convert.ToInt32(reader[LocalDatabase.CAMPO_DOCTOID_PANEL_POSICION].ToString().Trim());
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
            return pm;
        }

        public static int updatePosicionEnviadaEnviarDocumentosConPermiso(int idWs)
        {
            int resp = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "UPDATE " + LocalDatabase.TABLA_POSICION + " SET " + LocalDatabase.CAMPO_ENVIADO_POSICION + " = " + 1 + " WHERE " +
                    LocalDatabase.CAMPO_DOCTOID_PANEL_POSICION + "=" + idWs;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        resp = 1;
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

        public static int getTheTotalNumberOfUnsentLocations()
        {
            int total = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_POSICION + " WHERE " +
                        LocalDatabase.CAMPO_TIPO_MOV_POSICION + " != " + 9 + " AND " + LocalDatabase.CAMPO_ENVIADO_POSICION + " = " + 0;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                total = reader.GetInt32(0);
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

        public static int getIdPositionFromTheDocument(int idDocto)
        {
            int resp = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_ID_POSICION + " FROM " + LocalDatabase.TABLA_POSICION + " WHERE " + LocalDatabase.CAMPO_DOCTOID_POSICION +
                        " = " + idDocto;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    resp = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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
            return resp;
        }

        public static List<ClsPositionsModel> getAllNotSendPositions(int enviado)
        {
            List<ClsPositionsModel> todasLasPosiciones = null;
            ClsPositionsModel pm = null;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_POSICION + " WHERE " +
                        LocalDatabase.CAMPO_TIPO_MOV_POSICION + " != " + 9 +
                        " AND " + LocalDatabase.CAMPO_ENVIADO_POSICION + "=" + enviado;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            todasLasPosiciones = new List<ClsPositionsModel>();
                            while (reader.Read())
                            {
                                pm = new ClsPositionsModel();
                                pm.latitude = Convert.ToDouble(reader[LocalDatabase.CAMPO_LAT_POSICION].ToString().Trim());
                                pm.longitude = Convert.ToDouble(reader[LocalDatabase.CAMPO_LON_POSICION].ToString().Trim());
                                pm.customerId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLIENTE_ID_POSICION].ToString().Trim());
                                pm.date = reader[LocalDatabase.CAMPO_FECHA_POSICION].ToString().Trim();
                                pm.documentType = Convert.ToInt32(reader[LocalDatabase.CAMPO_TIPO_MOV_POSICION].ToString().Trim());
                                pm.agentId = Convert.ToInt32(reader[LocalDatabase.CAMPO_VENDEDOR_ID_POSICION].ToString().Trim());
                                pm.route = reader[LocalDatabase.CAMPO_RUTA_POSICION].ToString().Trim();
                                pm.idDoctoPanel = Convert.ToInt32(reader[LocalDatabase.CAMPO_DOCTOID_PANEL_POSICION].ToString().Trim());
                                todasLasPosiciones.Add(pm);
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
            return todasLasPosiciones;
        }

        public static ClsPositionsModel getAllDataForAPositionIdApp(int idDocument, int documentType)
        {
            ClsPositionsModel pm = null;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_POSICION + " WHERE " +
                        LocalDatabase.CAMPO_DOCTOID_POSICION + " = " + idDocument +
                        " AND " + LocalDatabase.CAMPO_TIPO_MOV_POSICION + " = " + documentType +
                        " AND " + LocalDatabase.CAMPO_ENVIADO_POSICION + " = " + 0;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                pm = new ClsPositionsModel();
                                pm.latitude = Convert.ToDouble(reader[LocalDatabase.CAMPO_LAT_POSICION].ToString().Trim());
                                pm.longitude = Convert.ToDouble(reader[LocalDatabase.CAMPO_LON_POSICION].ToString().Trim());
                                pm.customerId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLIENTE_ID_POSICION].ToString().Trim());
                                pm.date = reader[LocalDatabase.CAMPO_FECHA_POSICION].ToString().Trim();
                                pm.documentType = Convert.ToInt32(reader[LocalDatabase.CAMPO_TIPO_MOV_POSICION].ToString().Trim());
                                pm.agentId = Convert.ToInt32(reader[LocalDatabase.CAMPO_VENDEDOR_ID_POSICION].ToString().Trim());
                                pm.route = reader[LocalDatabase.CAMPO_RUTA_POSICION].ToString().Trim();
                                pm.idDoctoPanel = Convert.ToInt32(reader[LocalDatabase.CAMPO_DOCTOID_PANEL_POSICION].ToString().Trim());
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
            return pm;
        }

        public static int updatePosicionEnviada(int idLocationApp, int idDocumentServer)
        {
            int resp = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "UPDATE " + LocalDatabase.TABLA_POSICION + " SET " + LocalDatabase.CAMPO_DOCTOID_PANEL_POSICION + " = @idDocumentServer, " +
                    LocalDatabase.CAMPO_ENVIADO_POSICION + " = " + 1 + " WHERE " + LocalDatabase.CAMPO_ID_POSICION + " = " + idLocationApp;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (idDocumentServer > 0)
                            command.Parameters.AddWithValue("@idDocumentServer", idDocumentServer);
                        int records = command.ExecuteNonQuery();
                        if (records > 0)
                            resp = 1;
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
            return resp;
        }

    }
}
