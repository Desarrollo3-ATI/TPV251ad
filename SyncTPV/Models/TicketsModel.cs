using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wsROMClases.Models;

namespace SyncTPV.Models
{
    public class TicketsModel
    {
        public int id { get; set; }
        public String referencia { get; set; }
        public String datos { get; set; }
        public int idAgente { get; set; }
        public String tipoDocumento { get; set; }
        public String fecha { get; set; }
        public int idPanel { get; set; }
        public int estatus { get; set; }

        public static List<int> getAllIdsTicketsNotSends()
        {
            List<int> ingresosList = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_ID_RESPALDOTICKETS + " FROM " + LocalDatabase.TABLA_RESPALDOTICKETS + " WHERE " +
                    LocalDatabase.CAMPO_IDWS_RESPALDOTICKETS + " = 0 ";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            ingresosList = new List<int>();
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    ingresosList.Add(Convert.ToInt32(reader.GetValue(0).ToString().Trim()));
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
            return ingresosList;
        }

        public static ExpandoObject obtenerListaDeTickets(string StringPanel, bool banFecha, string fecha, int idAgente, string referencia, string tipoDocumento, string fechamax, int limite)
        {
            int num = 0;
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = StringPanel;
            sqlConnection.Open();
            dynamic response = new ExpandoObject();
            List<ExpandoObject> respuesta = new List<ExpandoObject>();
            try
            {
                string text = "SELECT ";
                if (limite > 0)
                {
                    text = text + " TOP " + limite;
                }

                text = text + " * FROM Tickets WHERE estatus = " + 0 + " ";
                if (banFecha)
                {
                    text = text + " AND (fecha between '" + fecha + " 00:00:00' AND '" + fechamax + " 23:59:59')";
                }

                if (idAgente != 0)
                {
                    text = text + " AND idAgente = " + idAgente;
                }

                if (!referencia.Equals(""))
                {
                    text = text + " AND referencia = '" + referencia + "'";
                }

                if (!tipoDocumento.Equals("Todos"))
                {
                    text = text + " AND tipoDocumento = '" + tipoDocumento + "'";
                }

                SqlCommand sqlCommand = new SqlCommand(text, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        dynamic val = new ExpandoObject();
                        val.id = Convert.ToInt32(sqlDataReader["id"].ToString().Trim());
                        val.referencia = sqlDataReader["referencia"].ToString().Trim();
                        val.datos = sqlDataReader["datos"].ToString().Trim();
                        val.idAgente = Convert.ToInt32(sqlDataReader["idAgente"].ToString().Trim());
                        val.tipoDocumento = sqlDataReader["tipoDocumento"].ToString().Trim();
                        val.fecha = sqlDataReader["fecha"].ToString().Trim();
                        val.estatus = Convert.ToInt32(sqlDataReader["estatus"].ToString().Trim());
                        respuesta.Add(val);
                    }
                }

                if (sqlDataReader != null && !sqlDataReader.IsClosed)
                {
                    sqlDataReader.Close();
                }
            }
            catch (SqlException ex)
            {
                SECUDOC.writeLog(ex.ToString());
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
                response.respuesta = respuesta;
            }

            return response;
        }

        public static int getIdTicketsByReference(String referencia)
        {
            int ingresosList = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_ID_RESPALDOTICKETS + " FROM " + LocalDatabase.TABLA_RESPALDOTICKETS + " WHERE " +
                    LocalDatabase.CAMPO_REFERENCIA_RESPALDOTICKETS + " = '"+referencia+"' ";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    ingresosList = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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
            return ingresosList;
        }

        public static Boolean deleteAllTickets()
        {
            bool deleted = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "DELETE FROM " + LocalDatabase.TABLA_RESPALDOTICKETS;
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

        public static int getIncomeIdIfItExists(SqlConnection db, string referencia)
        {
            int result = 0;
            try
            {
                string cmdText = "SELECT id FROM Tickets WHERE referencia =  '" + referencia + "'";
                SqlCommand sqlCommand = new SqlCommand(cmdText, db);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        if (sqlDataReader.GetValue(0) != DBNull.Value)
                        {
                            result = Convert.ToInt32(sqlDataReader.GetValue(0).ToString().Trim());
                        }
                    }
                }

                if (sqlDataReader != null && !sqlDataReader.IsClosed)
                {
                    sqlDataReader.Close();
                }
            }
            catch (SqlException ex)
            {
                SECUDOC.writeLog(ex.ToString());
            }
            finally
            {
            }

            return result;
        }

        public static List<ExpandoObject> savePanelDataTickets(string panelInstance, int id, string referencia, string datos, int idAgente, string tipoDocumento, string fecha, int idPanel, int estatus)
        {
            dynamic obj = null;
            List<ExpandoObject> list = null;
            SqlConnection sqlConnection = new SqlConnection();
            try
            {
                list = new List<ExpandoObject>();
                if (referencia != null)
                {
                    sqlConnection.ConnectionString = panelInstance;
                    sqlConnection.Open();
                    int incomeIdIfItExists = getIncomeIdIfItExists(sqlConnection, referencia);
                    if (incomeIdIfItExists > 0)
                    {
                        obj = new ExpandoObject();
                        obj.idIngresoApp = id;
                        obj.idIngresoServer = incomeIdIfItExists;
                        list.Add(obj);
                    }
                    else
                    {
                        string cmdText = "INSERT INTO Tickets (referencia,datos,idAgente,tipoDocumento,fecha,estatus) VALUES('" + referencia + "', '" + datos + "', " + idAgente + ", '" + tipoDocumento + "', '" + fecha + "', " + estatus + "); SELECT SCOPE_IDENTITY();";
                        SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
                        int num = sqlCommand.ExecuteNonQuery();
                        if (num > 0)
                        {
                            int incomeIdIfItExists2 = getIncomeIdIfItExists(sqlConnection, referencia);
                            if (incomeIdIfItExists2 > 0)
                            {
                                obj = new ExpandoObject();
                                obj.idIngresoApp = id;
                                obj.idIngresoServer = incomeIdIfItExists2;
                                list.Add(obj);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                SECUDOC.writeLog(ex.ToString());
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }

            return list;
        }

        public static bool actualizarIdServer(int id, int idServer)
        {
            bool t = false;
            String query = "UPDATE " + LocalDatabase.TABLA_RESPALDOTICKETS + 
                " SET " +LocalDatabase.CAMPO_IDWS_RESPALDOTICKETS + " = "+ idServer +
                " WHERE " + LocalDatabase.CAMPO_ID_RESPALDOTICKETS + " = " + id;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    
                        int actualizados = command.ExecuteNonQuery();
                        if(actualizados > 0)
                        {
                            t = true;
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
            return t;
        }
        public static TicketsModel getTickets(int idT)
        {
            TicketsModel t = null;
            String query = "SELECT * FROM "+LocalDatabase.TABLA_RESPALDOTICKETS+" WHERE "+LocalDatabase.CAMPO_ID_RESPALDOTICKETS+" = "+idT;
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
                                t = new TicketsModel();
                                t.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_RESPALDOTICKETS].ToString().Trim());
                                t.referencia = reader[LocalDatabase.CAMPO_REFERENCIA_RESPALDOTICKETS].ToString().Trim();
                                t.datos = reader[LocalDatabase.CAMPO_DATOS_RESPALDOTICKETS].ToString().Trim();
                                t.idAgente = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDAGENTE_RESPALDOTICKETS].ToString().Trim());
                                t.tipoDocumento = reader[LocalDatabase.CAMPO_TIPODOCUMENTO_RESPALDOTICKETS].ToString().Trim();
                                t.fecha = reader[LocalDatabase.CAMPO_FECHA_RESPALDOTICKETS].ToString().Trim();
                                t.idPanel = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDWS_RESPALDOTICKETS].ToString().Trim());
                                t.estatus = Convert.ToInt32(reader[LocalDatabase.CAMPO_ESTATUS_RESPALDOTICKETS].ToString().Trim());
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
            return t;
        }
       
    }
}
