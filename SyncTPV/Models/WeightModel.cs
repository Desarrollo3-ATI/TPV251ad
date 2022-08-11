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
    public class WeightModel
    {
        public static readonly int TIPO_REAL = 1;
        public static readonly int TIPO_TEMPORAL = 2;
        public int id { get; set; }
        public int idServer { get; set; }
        public int movementId { get; set; }
        public double pesoBruto { get; set; }
        public double pesoCaja { get; set; }
        public double pesoNeto { get; set; }
        public String createdAt { get; set; }
        public int cajas { get; set; }
        public double pesoPolloLesionado { get; set; }
        public double pesoPolloMuerto { get; set; }
        public double pesoPolloBajoDePeso { get; set; }
        public double pesoPolloGolpeado { get; set; }
        public int tipo { get; set; }

        public static int createUpdateOrDelete(String query)
        {
            int response = 0;
            var db = new SQLiteConnection();
            try {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db)) {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        response = 1;
                }
            } catch (SQLiteException e) {
                SECUDOC.writeLog(e.ToString());
            } finally {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return response;
        }

        public static int updateWeightReal(double pesoBruto, double pesoTara, double pesoNeto, double cajas, double pesoPolloLesionado,
            double pesoPolloMuerto, double pesoPolloBajoDePeso, double pesoPolloGolpeado, int movementId)
        {
            int response = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_PESO + " SET " + LocalDatabase.CAMPO_PESOBRUTO_PESO + " = @pesoBruto, " +
                    LocalDatabase.CAMPO_PESOCAJA_PESO + " = @pesoTara, " + LocalDatabase.CAMPO_PESONETO_PESO + " = @pesoNeto, " +
                    LocalDatabase.CAMPO_CAJAS_PESO + " = @cajas, " +
                    LocalDatabase.CAMPO_PESOPOLLOLESIONADO_PESO + " = @pesoPolloLesionado, " +
                    LocalDatabase.CAMPO_PESOPOLLOMUERTO_PESO + " = @pesoPolloMuerto, " +
                    LocalDatabase.CAMPO_PESOPOLLOBAJOPESO_PESO + " = @pesoPolloBajoDePeso, " +
                    LocalDatabase.CAMPO_PESOPOLLOGOLPEADO_PESO + " = @pesoPolloGolpeado WHERE " +
                    LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = @movementId AND " + LocalDatabase.CAMPO_TIPO_PESO + " = " + TIPO_REAL;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@pesoBruto", pesoBruto);
                    command.Parameters.AddWithValue("@pesoTara", pesoTara);
                    command.Parameters.AddWithValue("@pesoNeto", pesoNeto);
                    command.Parameters.AddWithValue("@cajas", cajas);
                    command.Parameters.AddWithValue("@pesoPolloLesionado", pesoPolloLesionado);
                    command.Parameters.AddWithValue("@pesoPolloMuerto", pesoPolloMuerto);
                    command.Parameters.AddWithValue("@pesoPolloBajoDePeso", pesoPolloBajoDePeso);
                    command.Parameters.AddWithValue("@pesoPolloGolpeado", pesoPolloGolpeado);
                    command.Parameters.AddWithValue("@movementId", movementId);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        response = 1;
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

        public static int updateWeightTemporal(double pesoBruto, double pesoTara, double pesoNeto, double cajas, double pesoPolloLesionado,
            double pesoPolloMuerto, double pesoPolloBajoDePeso, double pesoPolloGolpeado, int movementId)
        {
            int response = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_PESO + " SET " + LocalDatabase.CAMPO_PESOCAJA_PESO + " = @pesoTara, " +
                            LocalDatabase.CAMPO_PESOPOLLOLESIONADO_PESO + " = @pesoPolloLesionado, " + 
                            LocalDatabase.CAMPO_PESOPOLLOMUERTO_PESO + " = @pesoPolloMuerto, " +
                            LocalDatabase.CAMPO_PESOPOLLOBAJOPESO_PESO + " = @pesoPolloBajoDePeso, " + 
                            LocalDatabase.CAMPO_PESOPOLLOGOLPEADO_PESO + " = @pesoPolloGolpeado, " +
                        LocalDatabase.CAMPO_PESOBRUTO_PESO + " = @pesoBruto, " + LocalDatabase.CAMPO_PESONETO_PESO + " = @pesoNeto, " +
                        LocalDatabase.CAMPO_CAJAS_PESO + " = @cajas " +
                        "WHERE " + LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = @movementId AND " +
                        LocalDatabase.CAMPO_TIPO_PESO + " = " + WeightModel.TIPO_TEMPORAL;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@pesoBruto", pesoBruto);
                    command.Parameters.AddWithValue("@pesoTara", pesoTara);
                    command.Parameters.AddWithValue("@pesoNeto", pesoNeto);
                    command.Parameters.AddWithValue("@cajas", cajas);
                    command.Parameters.AddWithValue("@pesoPolloLesionado", pesoPolloLesionado);
                    command.Parameters.AddWithValue("@pesoPolloMuerto", pesoPolloMuerto);
                    command.Parameters.AddWithValue("@pesoPolloBajoDePeso", pesoPolloBajoDePeso);
                    command.Parameters.AddWithValue("@pesoPolloGolpeado", pesoPolloGolpeado);
                    command.Parameters.AddWithValue("@movementId", movementId);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        response = 1;
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

        public static int updatePesoNetoTemporal(int movimientoId, double pesoNeto)
        {
            int response = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_PESO + " SET " + LocalDatabase.CAMPO_PESONETO_PESO + " = @pesoNeto " +
                                "WHERE " + LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = @movimientoId " +
                                "AND " + LocalDatabase.CAMPO_TIPO_PESO + " = @tipo";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@movimientoId", movimientoId);
                    command.Parameters.AddWithValue("@pesoNeto", pesoNeto);
                    command.Parameters.AddWithValue("@tipo", WeightModel.TIPO_TEMPORAL);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        response = 1;
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

        public static int updatePesoPolloLesionadoYNetoTemporal(int movimientoId, double pesoPolloLesionado, double pesoNeto)
        {
            int response = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_PESO + " SET " + LocalDatabase.CAMPO_PESOPOLLOLESIONADO_PESO +
                                " = @pesoPolloLesionado, " + LocalDatabase.CAMPO_PESONETO_PESO + " = @pesoNeto " +
                                "WHERE " + LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = @movimientoId " +
                                "AND " + LocalDatabase.CAMPO_TIPO_PESO + " = @tipo";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@movimientoId", movimientoId);
                    command.Parameters.AddWithValue("@pesoPolloLesionado", pesoPolloLesionado);
                    command.Parameters.AddWithValue("@pesoNeto", pesoNeto);
                    command.Parameters.AddWithValue("@tipo", WeightModel.TIPO_TEMPORAL);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        response = 1;
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

        public static int updatePesoBrutoYNetoTemporal(int movimientoId, double pesoBruto, double pesoNeto)
        {
            int response = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_PESO + " SET " +
                                LocalDatabase.CAMPO_PESOBRUTO_PESO + " = @pesoBruto, " +
                                LocalDatabase.CAMPO_PESONETO_PESO + " = @pesoNeto WHERE " +
                                LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = @movimientoId " +
                                "AND " + LocalDatabase.CAMPO_TIPO_PESO + " = @tipo";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@movimientoId", movimientoId);
                    command.Parameters.AddWithValue("@pesoBruto", pesoBruto);
                    command.Parameters.AddWithValue("@pesoNeto", pesoNeto);
                    command.Parameters.AddWithValue("@tipo", TIPO_TEMPORAL);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        response = 1;
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

        public static int updatePesoPolloBajoPesoYNetoTemporal(int movimientoId, double nuevoPesoPolloBajoDePeso, double pesoNeto)
        {
            int response = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_PESO + " SET " + LocalDatabase.CAMPO_PESOPOLLOBAJOPESO_PESO +
                                " = @nuevoPesoPolloBajoDePeso, " + LocalDatabase.CAMPO_PESONETO_PESO + " = @pesoNeto " +
                                "WHERE " + LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = @movimientoId " +
                                "AND " + LocalDatabase.CAMPO_TIPO_PESO + " = @tipo";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@movimientoId", movimientoId);
                    command.Parameters.AddWithValue("@nuevoPesoPolloBajoDePeso", nuevoPesoPolloBajoDePeso);
                    command.Parameters.AddWithValue("@pesoNeto", pesoNeto);
                    command.Parameters.AddWithValue("@tipo", WeightModel.TIPO_TEMPORAL);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        response = 1;
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

        public static int updatePesoPolloGolpeadoYNetoTemporal(int movimientoId, double newPesoPolloGolpeado, double pesoNeto)
        {
            int response = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_PESO + " SET " + LocalDatabase.CAMPO_PESOPOLLOGOLPEADO_PESO +
                                " = @newPesoPolloGolpeado, " + LocalDatabase.CAMPO_PESONETO_PESO + " = @pesoNeto " +
                                "WHERE " + LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = @movimientoId " +
                                "AND " + LocalDatabase.CAMPO_TIPO_PESO + " = @tipo";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@movimientoId", movimientoId);
                    command.Parameters.AddWithValue("@newPesoPolloGolpeado", newPesoPolloGolpeado);
                    command.Parameters.AddWithValue("@pesoNeto", pesoNeto);
                    command.Parameters.AddWithValue("@tipo", WeightModel.TIPO_TEMPORAL);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        response = 1;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                if (db != null && db.State == System.Data.ConnectionState.Open)
                    db.Close();
            }
            return response;
        }

        public static int updatePesoPolloMuertoYNetoTemporal(int movimientoId, double nuevoPesoPolloMuerto, double pesoNeto)
        {
            int response = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_PESO + " SET " +
                                LocalDatabase.CAMPO_PESOPOLLOMUERTO_PESO + " = @nuevoPesoPolloMuerto, " +
                                LocalDatabase.CAMPO_PESONETO_PESO + " = @pesoNeto WHERE " +
                                LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = @movimientoId " +
                                "AND " + LocalDatabase.CAMPO_TIPO_PESO + " = @tipo";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@movimientoId", movimientoId);
                    command.Parameters.AddWithValue("@nuevoPesoPolloMuerto", nuevoPesoPolloMuerto);
                    command.Parameters.AddWithValue("@pesoNeto", pesoNeto);
                    command.Parameters.AddWithValue("@tipo", WeightModel.TIPO_TEMPORAL);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        response = 1;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                if (db != null && db.State == System.Data.ConnectionState.Open)
                    db.Close();
            }
            return response;
        }

        public static bool updatePesoTaraYNetoTemporal(int movimientoId, double pesoTara, double pesoNeto)
        {
            bool updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_PESO + " SET " +
                                LocalDatabase.CAMPO_PESOCAJA_PESO + " = @pesoTara, " +
                                LocalDatabase.CAMPO_PESONETO_PESO + " = @pesoNeto WHERE " +
                                LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = @movimientoId " +
                                "AND " + LocalDatabase.CAMPO_TIPO_PESO + " = @tipo";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@movimientoId", movimientoId);
                    command.Parameters.AddWithValue("@pesoTara", pesoTara);
                    command.Parameters.AddWithValue("@pesoNeto", pesoNeto);
                    command.Parameters.AddWithValue("@tipo", WeightModel.TIPO_TEMPORAL);
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

        public static WeightModel getAWeight(String query)
        {
            WeightModel wm = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) {
                            while (reader.Read())
                            {
                                wm = new WeightModel();
                                wm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_PESO].ToString().Trim());
                                wm.idServer = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDSERVER_PESO].ToString().Trim());
                                wm.movementId = Convert.ToInt32(reader[LocalDatabase.CAMPO_MOVIMIENTOID_PESO].ToString().Trim());
                                wm.pesoBruto = Convert.ToDouble(reader[LocalDatabase.CAMPO_PESOBRUTO_PESO].ToString().Trim());
                                wm.pesoCaja = Convert.ToDouble(reader[LocalDatabase.CAMPO_PESOCAJA_PESO].ToString().Trim());
                                wm.pesoNeto = Convert.ToDouble(reader[LocalDatabase.CAMPO_PESONETO_PESO].ToString().Trim());
                                wm.createdAt = reader[LocalDatabase.CAMPO_CREATEDAT_PESO].ToString().Trim();
                                wm.cajas = Convert.ToInt32(reader[LocalDatabase.CAMPO_CAJAS_PESO].ToString().Trim());
                                wm.pesoPolloLesionado = Convert.ToDouble(reader[LocalDatabase.CAMPO_PESOPOLLOLESIONADO_PESO].ToString().Trim());
                                wm.pesoPolloMuerto = Convert.ToDouble(reader[LocalDatabase.CAMPO_PESOPOLLOMUERTO_PESO].ToString().Trim());
                                wm.pesoPolloBajoDePeso = Convert.ToDouble(reader[LocalDatabase.CAMPO_PESOPOLLOBAJOPESO_PESO].ToString().Trim());
                                wm.pesoPolloGolpeado = Convert.ToDouble(reader[LocalDatabase.CAMPO_PESOPOLLOGOLPEADO_PESO].ToString().Trim());
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
                if (db != null && db.State == System.Data.ConnectionState.Open)
                    db.Close();
            }
            return wm;
        }

        public static WeightModel getAWeightTemporal(int movementId)
        {
            WeightModel wm = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_PESO + " WHERE " +
                    LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = @movementId AND " + LocalDatabase.CAMPO_TIPO_PESO + " = " +
                    WeightModel.TIPO_TEMPORAL;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@movementId", movementId);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                wm = new WeightModel();
                                wm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_PESO].ToString().Trim());
                                wm.idServer = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDSERVER_PESO].ToString().Trim());
                                wm.movementId = Convert.ToInt32(reader[LocalDatabase.CAMPO_MOVIMIENTOID_PESO].ToString().Trim());
                                wm.pesoBruto = Convert.ToDouble(reader[LocalDatabase.CAMPO_PESOBRUTO_PESO].ToString().Trim());
                                wm.pesoCaja = Convert.ToDouble(reader[LocalDatabase.CAMPO_PESOCAJA_PESO].ToString().Trim());
                                wm.pesoNeto = Convert.ToDouble(reader[LocalDatabase.CAMPO_PESONETO_PESO].ToString().Trim());
                                wm.createdAt = reader[LocalDatabase.CAMPO_CREATEDAT_PESO].ToString().Trim();
                                wm.cajas = Convert.ToInt32(reader[LocalDatabase.CAMPO_CAJAS_PESO].ToString().Trim());
                                wm.pesoPolloLesionado = Convert.ToDouble(reader[LocalDatabase.CAMPO_PESOPOLLOLESIONADO_PESO].ToString().Trim());
                                wm.pesoPolloMuerto = Convert.ToDouble(reader[LocalDatabase.CAMPO_PESOPOLLOMUERTO_PESO].ToString().Trim());
                                wm.pesoPolloBajoDePeso = Convert.ToDouble(reader[LocalDatabase.CAMPO_PESOPOLLOBAJOPESO_PESO].ToString().Trim());
                                wm.pesoPolloGolpeado = Convert.ToDouble(reader[LocalDatabase.CAMPO_PESOPOLLOGOLPEADO_PESO].ToString().Trim());
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
            return wm;
        }

        public static WeightModel getWeightReal(int movimientoId)
        {
            WeightModel wm = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_PESO + " WHERE " + LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = @movimientoId " +
                    "AND " + LocalDatabase.CAMPO_TIPO_PESO + " = "+TIPO_REAL;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@movimientoId", movimientoId);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                wm = new WeightModel();
                                wm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_PESO].ToString().Trim());
                                wm.idServer = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDSERVER_PESO].ToString().Trim());
                                wm.movementId = Convert.ToInt32(reader[LocalDatabase.CAMPO_MOVIMIENTOID_PESO].ToString().Trim());
                                wm.pesoBruto = Convert.ToDouble(reader[LocalDatabase.CAMPO_PESOBRUTO_PESO].ToString().Trim());
                                wm.pesoCaja = Convert.ToDouble(reader[LocalDatabase.CAMPO_PESOCAJA_PESO].ToString().Trim());
                                wm.pesoNeto = Convert.ToDouble(reader[LocalDatabase.CAMPO_PESONETO_PESO].ToString().Trim());
                                wm.createdAt = reader[LocalDatabase.CAMPO_CREATEDAT_PESO].ToString().Trim();
                                wm.cajas = Convert.ToInt32(reader[LocalDatabase.CAMPO_CAJAS_PESO].ToString().Trim());
                                wm.pesoPolloLesionado = Convert.ToDouble(reader[LocalDatabase.CAMPO_PESOPOLLOLESIONADO_PESO].ToString().Trim());
                                wm.pesoPolloMuerto = Convert.ToDouble(reader[LocalDatabase.CAMPO_PESOPOLLOMUERTO_PESO].ToString().Trim());
                                wm.pesoPolloBajoDePeso = Convert.ToDouble(reader[LocalDatabase.CAMPO_PESOPOLLOBAJOPESO_PESO].ToString().Trim());
                                wm.pesoPolloGolpeado = Convert.ToDouble(reader[LocalDatabase.CAMPO_PESOPOLLOGOLPEADO_PESO].ToString().Trim());
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
            return wm;
        }

        public static WeightModel getAWeight(int movementId)
        {
            WeightModel wm = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_PESO + " WHERE " + 
                    LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = " +movementId+" AND "+
                    LocalDatabase.CAMPO_TIPO_PESO+" = "+WeightModel.TIPO_REAL;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                wm = new WeightModel();
                                wm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_PESO].ToString().Trim());
                                wm.idServer = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDSERVER_PESO].ToString().Trim());
                                wm.movementId = Convert.ToInt32(reader[LocalDatabase.CAMPO_MOVIMIENTOID_PESO].ToString().Trim());
                                wm.pesoBruto = Convert.ToDouble(reader[LocalDatabase.CAMPO_PESOBRUTO_PESO].ToString().Trim());
                                wm.pesoCaja = Convert.ToDouble(reader[LocalDatabase.CAMPO_PESOCAJA_PESO].ToString().Trim());
                                wm.pesoNeto = Convert.ToDouble(reader[LocalDatabase.CAMPO_PESONETO_PESO].ToString().Trim());
                                wm.createdAt = reader[LocalDatabase.CAMPO_CREATEDAT_PESO].ToString().Trim();
                                wm.cajas = Convert.ToInt32(reader[LocalDatabase.CAMPO_CAJAS_PESO].ToString().Trim());
                                wm.pesoPolloLesionado = Convert.ToDouble(reader[LocalDatabase.CAMPO_PESOPOLLOLESIONADO_PESO].ToString().Trim());
                                wm.pesoPolloMuerto = Convert.ToDouble(reader[LocalDatabase.CAMPO_PESOPOLLOMUERTO_PESO].ToString().Trim());
                                wm.pesoPolloBajoDePeso = Convert.ToDouble(reader[LocalDatabase.CAMPO_PESOPOLLOBAJOPESO_PESO].ToString().Trim());
                                wm.pesoPolloGolpeado = Convert.ToDouble(reader[LocalDatabase.CAMPO_PESOPOLLOGOLPEADO_PESO].ToString().Trim());
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
                if (db != null && db.State == System.Data.ConnectionState.Open)
                    db.Close();
            }
            return wm;
        }

        public static bool validateIfARecordExist(String query)
        {
            bool exist = false;
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
                            exist = true;
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
                if (db != null && db.State == System.Data.ConnectionState.Open)
                    db.Close();
            }
            return exist;
        }

        public static int getIntValue(String query)
        {
            int value = 0;
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
                                    value = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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
                if (db != null && db.State == System.Data.ConnectionState.Open)
                    db.Close();
            }
            return value;
        }

        public static int getLastId()
        {
            int lastId = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT "+LocalDatabase.CAMPO_ID_PESO+" FROM "+LocalDatabase.TABLA_PESO+" ORDER BY "+
                    LocalDatabase.CAMPO_ID_PESO+" DESC LIMIT 1";
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
                if (db != null && db.State == System.Data.ConnectionState.Open)
                    db.Close();
            }
            return lastId;
        }

        public static double getDoubleValue(String query)
        {
            double value = 0;
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
                                    value = Convert.ToDouble(reader.GetValue(0).ToString().Trim());
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
                if (db != null && db.State == System.Data.ConnectionState.Open)
                    db.Close();
            }
            return value;
        }

        public static double getPesoNetoReal(int movementId)
        {
            double value = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_PESONETO_PESO + " FROM " + LocalDatabase.TABLA_PESO + " WHERE " +
                        LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = " + movementId+" AND "+
                        LocalDatabase.CAMPO_TIPO_PESO+" = "+TIPO_REAL;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    value = Convert.ToDouble(reader.GetValue(0).ToString().Trim());
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
            return value;
        }

        public static double getPesoBrutoReal(int movementId)
        {
            double value = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_PESOBRUTO_PESO + " FROM " + LocalDatabase.TABLA_PESO + " WHERE " +
                        LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = " + movementId + " AND " +
                        LocalDatabase.CAMPO_TIPO_PESO + " = " + TIPO_REAL;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    value = Convert.ToDouble(reader.GetValue(0).ToString().Trim());
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
                if (db != null && db.State == System.Data.ConnectionState.Open)
                    db.Close();
            }
            return value;
        }

        public static double getPesoNetoTemporal(int movementId)
        {
            double value = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_PESONETO_PESO + " FROM " + LocalDatabase.TABLA_PESO + " WHERE " +
                        LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = @movementId AND "+LocalDatabase.CAMPO_TIPO_PESO+ " = @temporal";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@movementId", movementId);
                    command.Parameters.AddWithValue("@temporal", TIPO_TEMPORAL);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    value = Convert.ToDouble(reader.GetValue(0).ToString().Trim());
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
            return value;
        }

        public static double getPesoTarasTemporal(int movementId)
        {
            double value = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_PESOCAJA_PESO + " FROM " + LocalDatabase.TABLA_PESO + " WHERE " +
                        LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = " + movementId +
                        " AND " + LocalDatabase.CAMPO_TIPO_PESO + " = " + TIPO_TEMPORAL;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    value = Convert.ToDouble(reader.GetValue(0).ToString().Trim());
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
                if (db != null && db.State == System.Data.ConnectionState.Open)
                    db.Close();
            }
            return value;
        }

        public static double getPesoBrutoTemporal(int movementId)
        {
            double value = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_PESOBRUTO_PESO + " FROM " + LocalDatabase.TABLA_PESO + " WHERE " +
                        LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = " + movementId + " AND " +
                        LocalDatabase.CAMPO_TIPO_PESO + " = " + TIPO_TEMPORAL;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    value = Convert.ToDouble(reader.GetValue(0).ToString().Trim());
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
                if (db != null && db.State == System.Data.ConnectionState.Open)
                    db.Close();
            }
            return value;
        }

        public static bool deleteWieghtTemporal(int movementId)
        {
            bool deleted = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "DELETE FROM " + LocalDatabase.TABLA_PESO+" WHERE "+LocalDatabase.CAMPO_MOVIMIENTOID_PESO+" = "+
                    movementId+" AND "+LocalDatabase.CAMPO_TIPO_PESO+" = "+TIPO_TEMPORAL;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
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
                if (db != null && db.State == System.Data.ConnectionState.Open)
                    db.Close();
            }
            return deleted;
        }

        public static bool deleteWieghtReal(int movementId)
        {
            bool deleted = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "DELETE FROM " + LocalDatabase.TABLA_PESO + " WHERE " + LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = " +
                    movementId + " AND " + LocalDatabase.CAMPO_TIPO_PESO + " = " + TIPO_REAL;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
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
                if (db != null && db.State == System.Data.ConnectionState.Open)
                    db.Close();
            }
            return deleted;
        }

    }
}
