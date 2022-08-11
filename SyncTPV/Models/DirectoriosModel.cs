using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncTPV.Models
{
    public class DirectoriosModel
    {
        public static readonly int TIPO_FACTURAS_SERVER = 1;
        public static readonly int TIPO_FACTURAS_TERMINAL = 2;
        public int id { get; set; }
        public int tipo { get; set; }
        public String nombre { get; set; }
        public String ruta { get; set; }
        public int empresaId { get; set; }

        public static int createDirectory(int id, int type, String nombre, String ruta, int empresaId)
        {
            int idCreated = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "INSERT INTO " + LocalDatabase.TABLA_DIRECTORIOS + " VALUES(@id, @type, @nombre, @ruta, @empresaId)";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@ruta", ruta);
                    command.Parameters.AddWithValue("@empresaId", empresaId);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        idCreated = id;
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
            return idCreated;
        }

        public static bool updateDirectory(int id, int type, String nombre, String ruta, int empresaId)
        {
            bool updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_DIRECTORIOS + " SET "+LocalDatabase.CAMPO_TIPO_DIRECTORIO+" = @type, " +
                    LocalDatabase.CAMPO_NOMBRE_DIRECTORIO+" = @nombre, "+LocalDatabase.CAMPO_RUTA_DIRECTORIO+" = @ruta, " +
                    LocalDatabase.CAMPO_EMPRESAID_DIRECTORIO+" = @empresaId WHERE "+
                    LocalDatabase.CAMPO_ID_DIRECTORIO+" = @id";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@ruta", ruta);
                    command.Parameters.AddWithValue("@empresaId", empresaId);
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

        public static int validateIfDirectoryTypeExist(int type)
        {
            int records = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT COUNT(*) FROM "+LocalDatabase.TABLA_DIRECTORIOS+" WHERE "+
                    LocalDatabase.CAMPO_TIPO_DIRECTORIO+" = @tipo";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@tipo", type);
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

        public static bool validateIfDirectoryIdExist(int id)
        {
            bool exist = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_DIRECTORIOS + " WHERE " +
                    LocalDatabase.CAMPO_ID_DIRECTORIO + " = @id";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) == 1)
                                        exist = true;
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
            return exist;
        }

        public static DirectoriosModel getDirectorioByType(int tipo)
        {
            DirectoriosModel dm = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_DIRECTORIOS + " WHERE "+
                    LocalDatabase.CAMPO_TIPO_DIRECTORIO+" = @tipo";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@tipo", tipo);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dm = new DirectoriosModel();
                                dm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_DIRECTORIO].ToString().Trim());
                                dm.tipo = Convert.ToInt32(reader[LocalDatabase.CAMPO_TIPO_DIRECTORIO].ToString().Trim());
                                dm.nombre = reader[LocalDatabase.CAMPO_NOMBRE_DIRECTORIO].ToString().Trim();
                                dm.ruta = reader[LocalDatabase.CAMPO_RUTA_DIRECTORIO].ToString().Trim();
                                dm.empresaId = Convert.ToInt32(reader[LocalDatabase.CAMPO_EMPRESAID_DIRECTORIO].ToString().Trim());
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
            return dm;
        }

        public static int getLastId()
        {
            int lastId = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT MAX("+LocalDatabase.CAMPO_ID_DIRECTORIO+") FROM " + LocalDatabase.TABLA_DIRECTORIOS +" LIMIT 1";
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
            return lastId;
        }

        public static int getLastId(SQLiteConnection db)
        {
            int lastId = 0;
            try
            {
                String query = "SELECT MAX(" + LocalDatabase.CAMPO_ID_DIRECTORIO + ") FROM " + LocalDatabase.TABLA_DIRECTORIOS + " LIMIT 1";
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
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                
            }
            return lastId;
        }

        public static int geIdByType(int type)
        {
            int lastId = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_ID_DIRECTORIO + " FROM " + LocalDatabase.TABLA_DIRECTORIOS + " WHERE "+
                    LocalDatabase.CAMPO_TIPO_DIRECTORIO+" = @tipo LIMIT 1";
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
            return lastId;
        }

        public static String getRutaByType(int type)
        {
            String ruta = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_RUTA_DIRECTORIO + " FROM " + LocalDatabase.TABLA_DIRECTORIOS + " WHERE " +
                    LocalDatabase.CAMPO_TIPO_DIRECTORIO + " = @tipo LIMIT 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@tipo", type);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    ruta = reader.GetValue(0).ToString().Trim();
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
            return ruta;
        }

        public static bool deleteDirectoryByType(int type)
        {
            bool deleted = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "DELETE FROM " + LocalDatabase.TABLA_DIRECTORIOS + " WHERE "+
                    LocalDatabase.CAMPO_TIPO_DIRECTORIO+" = @type";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@type", type);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        deleted = true;
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
            return deleted;
        }

    }
}
