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
    public class ClsScaleModel
    {
        public int id { get; set; }
        public String name { get; set; }
        public String portname { get; set; }
        public int baud_rate { get; set; }
        public int parity { get; set; }
        public int data_bits { get; set; }
        public double stop_bits { get; set; }

        public static ExpandoObject saveANewScale(String name, String portname, int baud_rate, int parity, int data_bits, double stop_bits)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            bool saved = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "INSERT INTO " + LocalDatabase.TABLA_BASCULA + " (" + LocalDatabase.CAMPO_ID_BASCULA + ", " +
                    LocalDatabase.CAMPO_NOMBRE_BASCULA + ", " + LocalDatabase.CAMPO_NOMBREPUERTO_BASCULA + ", " +
                    LocalDatabase.CAMPO_RANGODEBAUDIOS_BASCULA + ", " + LocalDatabase.CAMPO_PARIDAD_BASCULA + ", " +
                    LocalDatabase.CAMPO_BITSDEDATOS_BASCULA + ", " + LocalDatabase.CAMPO_BITSDEPARADA_BASCULA + ") VALUES(@id, @name, @portname, " +
                    "@baud_rate, @parity, @data_bits, @stop_bits)";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", 1);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@portname", portname);
                    command.Parameters.AddWithValue("@baud_rate", baud_rate);
                    command.Parameters.AddWithValue("@parity", parity);
                    command.Parameters.AddWithValue("@data_bits", data_bits);
                    command.Parameters.AddWithValue("@stop_bits", stop_bits);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                    {
                        value = 1;
                        saved = true;
                    }
                    else description = "No pudimos crear el registro de los datos de la báscula!";
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
                response.saved = saved;
            }
            return response;
        }

        public static ExpandoObject updateScale(String name, String portname, int baud_rate, int parity, int data_bits, 
            double stop_bits)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            bool updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_BASCULA + " SET " + LocalDatabase.CAMPO_NOMBRE_BASCULA + " = @name, " +
                    LocalDatabase.CAMPO_NOMBREPUERTO_BASCULA + " = @portname, " + LocalDatabase.CAMPO_RANGODEBAUDIOS_BASCULA + " = @baud_rate, " +
                    LocalDatabase.CAMPO_PARIDAD_BASCULA + " = @parity, " + LocalDatabase.CAMPO_BITSDEDATOS_BASCULA + " = @data_bits, " +
                    LocalDatabase.CAMPO_BITSDEPARADA_BASCULA + " = @stop_bits WHERE " + LocalDatabase.CAMPO_ID_BASCULA + " = " + 1;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", 1);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@portname", portname);
                    command.Parameters.AddWithValue("@baud_rate", baud_rate);
                    command.Parameters.AddWithValue("@parity", parity);
                    command.Parameters.AddWithValue("@data_bits", data_bits);
                    command.Parameters.AddWithValue("@stop_bits", stop_bits);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                    {
                        value = 1;
                        updated = true;
                    }
                    else description = "No pudimos actualizar el registro de la báscula!";
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
                response.updated = updated;
            }
            return response;
        }

        public static ExpandoObject verifyIfAScaleIdAdded()
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            bool exist = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_BASCULA + 
                    " WHERE " + LocalDatabase.CAMPO_ID_BASCULA + " = " + 1;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                {
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) > 0)
                                    {
                                        value = 1;
                                        exist = true;
                                    }
                                    else description = "No encontramos el registro de la báscula en la base de datos!";
                                } else description = "No encontramos el registro de la báscula en la base de datos " +
                                        "(COUNT NULL)!";
                            }
                        } else description = "No se encontró ningún registro en la tabla Báscula!";
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
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
                response.exist = exist;
            }
            return response;
        }

        public static ClsScaleModel getallDataFromAScale()
        {
            ClsScaleModel bm = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_BASCULA + " WHERE " + LocalDatabase.CAMPO_ID_BASCULA + " = " + 1;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                bm = new ClsScaleModel();
                                bm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_BASCULA].ToString().Trim());
                                bm.name = reader[LocalDatabase.CAMPO_NOMBRE_BASCULA].ToString().Trim();
                                bm.portname = reader[LocalDatabase.CAMPO_NOMBREPUERTO_BASCULA].ToString().Trim();
                                bm.parity = Convert.ToInt32(reader[LocalDatabase.CAMPO_PARIDAD_BASCULA].ToString().Trim());
                                bm.baud_rate = Convert.ToInt32(reader[LocalDatabase.CAMPO_RANGODEBAUDIOS_BASCULA].ToString().Trim());
                                bm.data_bits = Convert.ToInt32(reader[LocalDatabase.CAMPO_BITSDEDATOS_BASCULA].ToString().Trim());
                                bm.stop_bits = Convert.ToDouble(reader[LocalDatabase.CAMPO_BITSDEPARADA_BASCULA].ToString().Trim());
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
            return bm;
        }
    }
}
