using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using wsROMClases;

namespace SyncTPV.Models
{
    public class ResponseDatosDistribuidor
    {
        public List<ClsDatosDistribuidorResponse> response { get; set; }
    }
    public class ClsDatosDistribuidorResponse
    {
        public int id { get; set; }
        public String nombre { get; set; }
        public String calle { get; set; }
        public String municipio { get; set; }
        public String numero { get; set; }
        public String codigo_postal { get; set; }
        public String colonia { get; set; }
        public String estado { get; set; }
        public String correo1 { get; set; }
        public String correo2 { get; set; }
        public String pagina_web { get; set; }
        public String telefono1 { get; set; }
        public String telefono2 { get; set; }
        public String otros { get; set; }
        public String foto { get; set; }
    }
    public class DatosDistribuidorModel
    {
        public static int saveAllDistribuidores(List<ClsDatosDistribuidorResponse> distribuidorList)
        {
            int lastId = 0;
            if (distribuidorList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (var distribuidor in distribuidorList)
                    {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_DATOSDISTRIBUIDOR + " VALUES (@id, @nombre, @calle, @municipio," +
                            " @numero, @codigo_postal, @colonia, @estado, @correo1, @correo2, @pagina_web, @telefono1, @telefono2, @otros," +
                            " @foto)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@id", distribuidor.id);
                            command.Parameters.AddWithValue("@nombre", distribuidor.nombre);
                            command.Parameters.AddWithValue("@calle", distribuidor.calle);
                            command.Parameters.AddWithValue("@municipio", distribuidor.municipio);
                            command.Parameters.AddWithValue("@numero", distribuidor.numero);
                            command.Parameters.AddWithValue("@codigo_postal", distribuidor.codigo_postal);
                            command.Parameters.AddWithValue("@colonia", distribuidor.colonia);
                            command.Parameters.AddWithValue("@estado", distribuidor.estado);
                            command.Parameters.AddWithValue("@correo1", distribuidor.correo1);
                            command.Parameters.AddWithValue("@correo2", distribuidor.correo2);
                            command.Parameters.AddWithValue("@pagina_web", distribuidor.pagina_web);
                            command.Parameters.AddWithValue("@telefono1", distribuidor.telefono1);
                            command.Parameters.AddWithValue("@telefono2", distribuidor.telefono2);
                            command.Parameters.AddWithValue("@otros", distribuidor.otros);
                            command.Parameters.AddWithValue("@foto", distribuidor.foto);
                            int recordInserted = command.ExecuteNonQuery();
                            if (recordInserted != 0)
                                lastId = distribuidor.id;
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
            return lastId;
        }

        public static int saveAllDistribuidoresLAN(ClsDatosDistribuidor distribuidor)
        {
            int lastId = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "INSERT INTO " + LocalDatabase.TABLA_DATOSDISTRIBUIDOR + " VALUES (@id, @nombre, @calle, @municipio," +
                       " @numero, @codigo_postal, @colonia, @estado, @correo1, @correo2, @pagina_web, @telefono1, @telefono2, @otros," +
                       " @foto)";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", distribuidor.id);
                    command.Parameters.AddWithValue("@nombre", distribuidor.nombre);
                    command.Parameters.AddWithValue("@calle", distribuidor.calle);
                    command.Parameters.AddWithValue("@municipio", distribuidor.municipio);
                    command.Parameters.AddWithValue("@numero", distribuidor.numero);
                    command.Parameters.AddWithValue("@codigo_postal", distribuidor.codigoPostal);
                    command.Parameters.AddWithValue("@colonia", distribuidor.colonia);
                    command.Parameters.AddWithValue("@estado", distribuidor.estado);
                    command.Parameters.AddWithValue("@correo1", distribuidor.correo1);
                    command.Parameters.AddWithValue("@correo2", distribuidor.correo2);
                    command.Parameters.AddWithValue("@pagina_web", distribuidor.paginaWeb);
                    command.Parameters.AddWithValue("@telefono1", distribuidor.telefono1);
                    command.Parameters.AddWithValue("@telefono2", distribuidor.telefono2);
                    command.Parameters.AddWithValue("@otros", distribuidor.otros);
                    command.Parameters.AddWithValue("@foto", distribuidor.foto);
                    int recordInserted = command.ExecuteNonQuery();
                    if (recordInserted != 0)
                        lastId = distribuidor.id;
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

        public static Boolean deleteAllDistribuidores()
        {
            bool deleted = true;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "DELETE FROM " + LocalDatabase.TABLA_DATOSDISTRIBUIDOR;
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
