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

namespace SyncTPV.Models
{
    public class UsoCFDIModel
    {
        public int id { get; set; }
        public String valor { get; set; }
        public String nombre { get; set; }
        public String descripcion { get; set; }
        public String fechaAlta { get; set; }
        public String fechaModificacion { get; set; }
        public int moral { get; set; }
        public int fisica { get; set; }

        public static List<UsoCFDIModel> getLocalUsoCFDI()
        {
            List<UsoCFDIModel> respuesta = null;
            UsoCFDIModel um;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * from UsoCFDI";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            respuesta = new List<UsoCFDIModel>();
                            while (reader.Read())
                            {
                                um = new UsoCFDIModel();
                                um.id = Convert.ToInt32(reader["id"].ToString().Trim());
                                um.valor = reader["valor"].ToString().Trim();
                                um.nombre = reader["nombre"].ToString().Trim();
                                um.descripcion = reader["descripcion"].ToString().Trim();
                                um.fechaAlta = reader["fechaAlta"].ToString().Trim();
                                um.fechaModificacion = reader["fechaModificacion"].ToString().Trim();
                                um.moral = Convert.ToInt32(reader["moral"].ToString().Trim());
                                um.fisica = Convert.ToInt32(reader["fisica"].ToString().Trim());
                                respuesta.Add(um);
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                SECUDOC.writeLog("Exception: " + e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return respuesta;
        }

        public static int getTotalUsoCFDI()
        {
            int records = 0;
            SQLiteConnection db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT count(*) as total from UsoCFDI";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                                if(reader.Read())
                                    records = Convert.ToInt32(reader["total"].ToString().Trim());
                            
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
            return records;
        }

        internal static List<UsoCFDIModel> getLocalFitroUsoCFDI(string filtro)
        {
            List<UsoCFDIModel> respuesta = null;
            UsoCFDIModel um;
            SQLiteConnection db = new SQLiteConnection();

            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * from UsoCFDI Where " + filtro + " = 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            respuesta = new List<UsoCFDIModel>();
                            while (reader.Read())
                            {
                                um = new UsoCFDIModel();
                                um.id = Convert.ToInt32(reader["id"].ToString().Trim());
                                um.valor = reader["valor"].ToString().Trim();
                                um.nombre = reader["nombre"].ToString().Trim();
                                um.descripcion = reader["descripcion"].ToString().Trim();
                                um.fechaAlta = reader["fechaAlta"].ToString().Trim();
                                um.fechaModificacion = reader["fechaModificacion"].ToString().Trim();
                                um.moral = Convert.ToInt32(reader["moral"].ToString().Trim());
                                um.fisica = Convert.ToInt32(reader["fisica"].ToString().Trim());
                                respuesta.Add(um);
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                SECUDOC.writeLog("Exception: " + e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return respuesta;
        }
    }
}
