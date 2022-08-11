using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wsROMClases.Models;

namespace SyncTPV.Models
{
    public class CajaModel
    {
        public int id { get; set; }
        public String code { get; set; }
        public String name { get; set; }
        public int warehouseId { get; set; }
        public String createdAt { get; set; }

        public static bool createCaja(int id, String code, String name, int almacenId, String createdAt)
        {
            bool created = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "INSERT INTO "+LocalDatabase.TABLA_CAJA+" VALUES(@id, @code, @name, @almacenId, @createdAt)";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@code", code);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@almacenId", almacenId);
                    command.Parameters.AddWithValue("@createdAt", createdAt);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        created = true;
                }
            } catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            } finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return created;
        }

        public static bool updateCaja(int id, String code, String name, int almacenId, String createdAt)
        {
            bool updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE "+LocalDatabase.TABLA_CAJA+" SET "+LocalDatabase.CAMPO_CODIGO_CAJA+" = @code, "+
                    LocalDatabase.CAMPO_NOMBRE_CAJA+" = @name, "+LocalDatabase.CAMPO_ALMACENID_CAJA+" = @almacenId, "+
                    LocalDatabase.CAMPO_CREATEDAT_CAJA+" = @createdAt WHERE "+LocalDatabase.CAMPO_ID_CAJA+" = @id";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@code", code);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@almacenId", almacenId);
                    command.Parameters.AddWithValue("@createdAt", createdAt);
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

        public static int getIdByCodeBox(String panelInstance, String codeBox)
        {
            int idBox = 0;
            SqlConnection connPanel = new SqlConnection();
            try
            {
                connPanel.ConnectionString = panelInstance;
                connPanel.Open();
                String query = "SELECT IdCaja FROM Cajas WHERE Codigo = @codeBox";
                using (SqlCommand command = new SqlCommand(query, connPanel))
                {
                    command.Parameters.AddWithValue("@codeBox", codeBox);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                idBox = reader.GetInt32(0);
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                SECUDOC.writeLog("Exception getIdByCodeBox: " + e.Message);
            }
            finally
            {
                if (connPanel != null && connPanel.State == ConnectionState.Open)
                    connPanel.Close();
            }
            return idBox;
        }

        public static int getIntValue(String query)
        {
            int total = 0;
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
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    total = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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

        /*public static String getCodeBox()
        {
            String code = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT codigo FROM Caja WHERE codigo != '' ORDER BY id ASC LIMIT 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    code = reader.GetValue(0).ToString().Trim();
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
            return code;
        }*/

        public static int getIdByCodeBox(String codeBox)
        {
            int idCaja = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_ID_USUARIO + " FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " +
                    LocalDatabase.CAMPO_CAJACODIGO_USUARIO + " = @codigo";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@codigo", codeBox);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    idCaja = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return idCaja;
        }

        public static int getLastId()
        {
            int lastId = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT MAX("+LocalDatabase.CAMPO_ID_CAJA+") FROM "+LocalDatabase.TABLA_CAJA+" LIMIT 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                                if (reader.GetValue(0) != DBNull.Value)
                                    lastId = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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

        public static bool checkIfCajaExist(int idCaja)
        {
            bool exist = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_CAJA + " WHERE "+
                    LocalDatabase.CAMPO_ID_CAJA+" = "+idCaja;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                            exist = true;
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

        public static List<ClsCajaModel> getAllCajas(String query)
        {
            List<ClsCajaModel> cajasList = null;
            ClsCajaModel caja = null;
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
                            cajasList = new List<ClsCajaModel>();
                            while (reader.Read())
                            {
                                caja = new ClsCajaModel();
                                caja.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_CAJA].ToString().Trim());
                                caja.code = reader[LocalDatabase.CAMPO_CODIGO_CAJA].ToString().Trim();
                                caja.name = reader[LocalDatabase.CAMPO_NOMBRE_CAJA].ToString().Trim();
                                caja.warehouseId = Convert.ToInt32(reader[LocalDatabase.CAMPO_ALMACENID_CAJA].ToString().Trim());
                                caja.createdAt = reader[LocalDatabase.CAMPO_CREATEDAT_CAJA].ToString().Trim();
                                cajasList.Add(caja);
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
            return cajasList;
        }

        public static int getAlmacenIdByCheckoutCode(string codigoCaja)
        {
            int valor = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT almacenId FROM " + LocalDatabase.TABLA_CAJA + " WHERE " +
                    LocalDatabase.CAMPO_CODIGO_CAJA + " = '" + codigoCaja+ "'";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                                if (reader.GetValue(0) != DBNull.Value)
                                    valor = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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
            return valor;
        }

        public static bool deleteAllCajas()
        {
            bool deleted = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "DELETE FROM "+LocalDatabase.TABLA_CAJA;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        deleted = true;
                }
            }
            catch (SqlException e)
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
