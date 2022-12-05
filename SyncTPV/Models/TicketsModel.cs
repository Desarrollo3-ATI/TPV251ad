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
