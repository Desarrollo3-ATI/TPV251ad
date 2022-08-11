using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using wsROMClases.Models.Panel;

namespace SyncTPV
{

    public class PreciosempresaModel
    {
        public int PRECIO_EMPRESA_ID { set; get; }
        public String NOMBRE { set; get; }
        public double precioImporte { set; get; }

        public static int saveAllPreciosEmpresa(List<ClsPreciosEmpresaModel> preciosEmpresaList)
        {
            int count = 0;
            if (preciosEmpresaList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (var precioEmpresa in preciosEmpresaList)
                    {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_PRECIOSEMPRESA + " VALUES(@id, @nombre)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@id", precioEmpresa.PRECIO_EMPRESA_ID);
                            command.Parameters.AddWithValue("@nombre", precioEmpresa.NOMBRE);
                            int recordSaved = command.ExecuteNonQuery();
                            if (recordSaved != 0)
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

        public static int saveAllPreciosEmpresaLAN(List<ClsPreciosEmpresaModel> preciosEmpresaList)
        {
            int count = 0;
            if (preciosEmpresaList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (var precioEmpresa in preciosEmpresaList)
                    {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_PRECIOSEMPRESA + " VALUES(@id, @nombre)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@id", precioEmpresa.PRECIO_EMPRESA_ID);
                            command.Parameters.AddWithValue("@nombre", precioEmpresa.NOMBRE);
                            int recordSaved = command.ExecuteNonQuery();
                            if (recordSaved != 0)
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

        public static String getAPriceName(int idPrecioEmpresa)
        {
            String name = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_NOMBRE_PRECIOSEMPRESA + " FROM " + LocalDatabase.TABLA_PRECIOSEMPRESA +
                    " WHERE " + LocalDatabase.CAMPO_ID_PRECIOSEMPRESA + " = @id";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", idPrecioEmpresa);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    name = reader.GetValue(0).ToString().Trim();
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
            return name;
        }

        public static List<String> obtenerListaDeNombreDePrecios()
        {
            List<String> pricenameList = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_PRECIOSEMPRESA;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            pricenameList = new List<string>();
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    pricenameList.Add(reader.GetValue(1).ToString().Trim());
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
            return pricenameList;
        }

        public static Boolean deleteAllPreciosEmpresa()
        {
            bool deleted = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "DELETE FROM " + LocalDatabase.TABLA_PRECIOSEMPRESA;
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
