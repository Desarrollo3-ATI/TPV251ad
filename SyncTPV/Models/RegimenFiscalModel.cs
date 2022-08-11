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
    public class RegimenFiscalModel
    {
        public int id { get; set; }
        public String valor { get; set; }
        public String nombre { get; set; }
        public String descripcion { get; set; }
        public String fechaAlta { get; set; }
        public String fechaModificacion { get; set; }
        public int moral { get; set; }
        public int fisica { get; set; }
        public static List<RegimenFiscalModel> getLocalRegimenFiscal()
        {
            List<RegimenFiscalModel> respuesta = null;
            RegimenFiscalModel rm;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * from RegimenFiscal";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            respuesta =new List<RegimenFiscalModel>();
                            while (reader.Read())
                            {
                                rm = new RegimenFiscalModel();
                                rm.id = Convert.ToInt32(reader["id"].ToString().Trim());
                                rm.valor = reader["valor"].ToString().Trim();
                                rm.nombre = reader["nombre"].ToString().Trim();
                                rm.descripcion = reader["descripcion"].ToString().Trim();
                                rm.fechaAlta = reader["fechaAlta"].ToString().Trim();
                                rm.fechaModificacion = reader["fechaModificacion"].ToString().Trim();
                                rm.moral = Convert.ToInt32(reader["moral"].ToString().Trim());
                                rm.fisica = Convert.ToInt32(reader["fisica"].ToString().Trim());
                                respuesta.Add(rm);
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
        public static List<RegimenFiscalModel> getLocalFitroRegimenFiscal(String filtro)
        {
            List<RegimenFiscalModel> respuesta = null;
            RegimenFiscalModel rm;
            SQLiteConnection db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * from RegimenFiscal Where "+filtro+" = 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            respuesta = new List<RegimenFiscalModel>();
                            while (reader.Read())
                            {
                                rm = new RegimenFiscalModel();
                                rm.id = Convert.ToInt32(reader["id"].ToString().Trim());
                                rm.valor = reader["valor"].ToString().Trim();
                                rm.nombre = reader["nombre"].ToString().Trim();
                                rm.descripcion = reader["descripcion"].ToString().Trim();
                                rm.fechaAlta = reader["fechaAlta"].ToString().Trim();
                                rm.fechaModificacion = reader["fechaModificacion"].ToString().Trim();
                                rm.moral = Convert.ToInt32(reader["moral"].ToString().Trim());
                                rm.fisica = Convert.ToInt32(reader["fisica"].ToString().Trim());
                                respuesta.Add(rm);
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

        public static int getTotalRegimenFiscal()
        {
            int records = 0;
            List<RegimenFiscalModel> respuesta = null;
            SQLiteConnection db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT count(*) as total from RegimenFiscal";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                                records = Convert.ToInt32(reader["total"].ToString().Trim());

                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                SECUDOC.writeLog("Exception: " + e.ToString());
                records = 0;
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return records;
        }

    }
}
