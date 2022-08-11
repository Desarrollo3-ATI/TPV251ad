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
    public class TaraModel
    {
        public int id { get; set; }
        public String code { get; set; }
        public String name { get; set; }
        public String color { get; set; }
        public double peso { get; set; }
        public String tipo { get; set; }
        public String createdAt { get; set; }

        public static bool createUpdateOrDelete(SQLiteConnection db, String query)
        {
            bool response = false;
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        response = true;
                }
            } catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {

            }
            return response;
        }

        public static bool createANewRecord(String code, String name, String color, double peso, String tipo)
        {
            bool response = false;
            int lastId = TaraModel.getLastId();
            lastId++;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "INSERT INTO "+LocalDatabase.TABLA_TARA+" VALUES(@id, @code, @name, @color, @peso, @tipo, @createdAt)";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", lastId);
                    command.Parameters.AddWithValue("@code", code);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@color", color);
                    command.Parameters.AddWithValue("@peso", peso);
                    command.Parameters.AddWithValue("@tipo", tipo);
                    command.Parameters.AddWithValue("@createdAt", DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss"));
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        response = true;
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
            return response;
        }

        public static List<TaraModel> getTaras(String query)
        {
            List<TaraModel> tarasList = null;
            TaraModel tara = null;
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
                            tarasList = new List<TaraModel>();
                            while (reader.Read())
                            {
                                tara = new TaraModel();
                                tara.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_TARA].ToString().Trim());
                                tara.code = reader[LocalDatabase.CAMPO_CODE_TARA].ToString().Trim();
                                tara.name = reader[LocalDatabase.CAMPO_NAME_TARA].ToString().Trim();
                                tara.color = reader[LocalDatabase.CAMPO_COLOR_TARA].ToString().Trim();
                                tara.peso = Convert.ToDouble(reader[LocalDatabase.CAMPO_PESO_TARA].ToString().Trim());
                                tara.tipo = reader[LocalDatabase.CAMPO_TIPO_TARA].ToString().Trim();
                                tara.createdAt = reader[LocalDatabase.CAMPO_CREATEDAT_TARA].ToString().Trim();
                                tarasList.Add(tara);
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
            return tarasList;
        }

        public static int getIntValue(String query)
        {
            int response = 0;
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
                                    response = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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
            return response;
        }

        public static bool typeExist(String type)
        {
            bool exist = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT COUNT(*) FROM "+LocalDatabase.TABLA_TARA+" WHERE "+LocalDatabase.CAMPO_TIPO_TARA+" = @type";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@type", type);
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

        public static int getLastId()
        {
            int response = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT "+LocalDatabase.CAMPO_ID_TARA+" FROM "+LocalDatabase.TABLA_TARA+" ORDER BY "+
                    LocalDatabase.CAMPO_ID_TARA+" DESC LIMIT 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    response = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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
            return response;
        }

        public static double getDoubleValue(String query)
        {
            double response = 0;
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
                                    response = Convert.ToDouble(reader.GetValue(0).ToString().Trim());
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
            return response;
        }

        public static bool updateColorPesoTipo(int idTara, String color, double peso, String tipo)
        {
            bool response = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_TARA + " SET " + LocalDatabase.CAMPO_COLOR_TARA + " = @color, " +
                    LocalDatabase.CAMPO_PESO_TARA + " = @peso, " + LocalDatabase.CAMPO_TIPO_TARA + " = @tipo " +
                    "WHERE " + LocalDatabase.CAMPO_ID_TARA + " = @idTara";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@color", color);
                    command.Parameters.AddWithValue("@peso", peso);
                    command.Parameters.AddWithValue("@tipo", tipo);
                    command.Parameters.AddWithValue("@idTara", idTara);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        response = true;
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
            return response;
        }

    }
}
