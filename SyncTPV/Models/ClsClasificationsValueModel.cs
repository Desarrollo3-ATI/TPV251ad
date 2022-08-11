using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using wsROMClases;

namespace SyncTPV.Models
{
    public class ClsClasificationsValue
    {
        public int id { get; set; }
        public String valor { get; set; }
        public int clasificationId { get; set; }
        public String codigo { get; set; }
        public String segmentoContableUno { get; set; }
        public String segmentoContableDos { get; set; }
        public String segmentoContableTres { get; set; }
    }

    public class ClasificationsValueModel
    {
        public static int saveAllClasificationsValue(List<ClsClasificationsValue> clasificationsValueList)
        {
            int count = 0;
            if (clasificationsValueList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (var clasificationsValue in clasificationsValueList)
                    {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_CLASIFICACION_VALOR + " VALUES(@id, @valor, @clasificationId, " +
                            "@code, @segmentoContableUno, @segmentoContableDos, @segmentoContableTres)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@id", clasificationsValue.id);
                            command.Parameters.AddWithValue("@valor", clasificationsValue.valor);
                            command.Parameters.AddWithValue("@clasificationId", clasificationsValue.clasificationId);
                            command.Parameters.AddWithValue("@code", clasificationsValue.codigo);
                            command.Parameters.AddWithValue("@segmentoContableUno", clasificationsValue.segmentoContableUno);
                            command.Parameters.AddWithValue("@segmentoContableDos", clasificationsValue.segmentoContableDos);
                            command.Parameters.AddWithValue("@segmentoContableTres", clasificationsValue.segmentoContableTres);
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

        public static int saveAllClasificationsValueLAN(List<ClsClasificacionesValoresModel> clasificationsValueList)
        {
            int lastId = 0;
            if (clasificationsValueList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (var clasificationsValue in clasificationsValueList)
                    {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_CLASIFICACION_VALOR + " VALUES(@id, @valor, @clasificationId, " +
                            "@code, @segmentoContableUno, @segmentoContableDos, @segmentoContableTres)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@id", clasificationsValue.id);
                            command.Parameters.AddWithValue("@valor", clasificationsValue.valor);
                            command.Parameters.AddWithValue("@clasificationId", clasificationsValue.clasificationId);
                            command.Parameters.AddWithValue("@code", clasificationsValue.codigo);
                            command.Parameters.AddWithValue("@segmentoContableUno", clasificationsValue.segmentoContableUno);
                            command.Parameters.AddWithValue("@segmentoContableDos", clasificationsValue.segmentoContableDos);
                            command.Parameters.AddWithValue("@segmentoContableTres", clasificationsValue.segmentoContableTres);
                            int recordSaved = command.ExecuteNonQuery();
                            if (recordSaved != 0)
                                lastId = Convert.ToInt32(clasificationsValue.id);
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

        public static Boolean deleteAllClasificationsValue()
        {
            bool deleted = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "DELETE FROM " + LocalDatabase.TABLA_CLASIFICACION_VALOR;
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

        public static String getCodeForAClasification(int idClasificationValue)
        {
            String value = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_CODIGO_CLASIVALOR + " FROM " +
                        LocalDatabase.TABLA_CLASIFICACION_VALOR +
                        " WHERE " + LocalDatabase.CAMPO_ID_CLASIVALOR + " = " + idClasificationValue;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                                if (reader.GetValue(0) != DBNull.Value)
                                    value = reader.GetValue(0).ToString().Trim();
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
            return value;
        }

    }
}
