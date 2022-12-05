using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace SyncTPV
{
    public class DatosTicketModel
    {
        public int CONFIGURA_ID { get; set; }
        public string EMPRESA { get; set; }
        public string DIRECCION { get; set; }
        public string RFC { get; set; }
        public string EXPEDIDO { get; set; }
        public string PIE_TICKETVENTAS { get; set; }
        public string PIE_TICKETCREDITO { get; set; }
        public string PIE_TICKETPEDIDO { get; set; }
        public string PIE_TICKETCOTIZACION { get; set; }
        public string PIE_TICKETCOBRANZA { get; set; }
        public string PIE_TICKETDEVOLUCION { get; set; }
        public string FTPSERVER { get; set; }
        public string FTPUSER { get; set; }
        public string FTPPASSWORD { get; set; }
        public int FTPPUERTO { get; set; }
        public string CLAVESUP { get; set; }
        public string PDEVOLUCION { get; set; }
        public string VALVC { get; set; }
        public int modificarInfoSucursal { get; set; }
        public int venderConExistencia { get; set; }
        public String nameImp1 { get; set; }
        public String nameImp2 { get; set; }
        public String nameImp3 { get; set; }
        public String nameReten1 { get; set; }
        public String nameReten2 { get; set; }

        public static int saveAllDatosTicket(List<DatosTicketModel> dtList)
        {
            int lastId = 0;
            if (dtList != null)
            {
                var db = new SQLiteConnection();                
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (var dt in dtList)
                    {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_DATOSTICKET + " VALUES(@id, @empresa, @direccion, @rfc, " +
                            "@expedido, @pieVentas, @pieCredito, @piePedido, @pieCotizacion, @pieCobranza, @pieDevolucion, " +
                            "@ftpServer, @ftpUser, @ftpPassword, @ftpPuerto, @claveSup, @pDevolucion, @valVC, @modificarInfoSucursal," +
                            "@venderConExistencia, @nameImp1, @nameImp2, @nameImp3, @nameReten1, @nameReten2,@banauto)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@id", dt.CONFIGURA_ID);
                            command.Parameters.AddWithValue("@empresa", dt.EMPRESA);
                            command.Parameters.AddWithValue("@direccion", dt.DIRECCION);
                            command.Parameters.AddWithValue("@rfc", dt.RFC);
                            command.Parameters.AddWithValue("@expedido", dt.EXPEDIDO);
                            command.Parameters.AddWithValue("@pieVentas", dt.PIE_TICKETVENTAS);
                            command.Parameters.AddWithValue("@pieCredito", dt.PIE_TICKETCREDITO);
                            command.Parameters.AddWithValue("@piePedido", dt.PIE_TICKETPEDIDO);
                            command.Parameters.AddWithValue("@pieCotizacion", dt.PIE_TICKETCOTIZACION);
                            command.Parameters.AddWithValue("@pieCobranza", dt.PIE_TICKETCOBRANZA);
                            command.Parameters.AddWithValue("@pieDevolucion", dt.PIE_TICKETDEVOLUCION);
                            command.Parameters.AddWithValue("@ftpServer", dt.FTPSERVER);
                            command.Parameters.AddWithValue("@ftpUser", dt.FTPUSER);
                            command.Parameters.AddWithValue("@ftpPassword", dt.FTPPASSWORD);
                            command.Parameters.AddWithValue("@ftpPuerto", dt.FTPPUERTO);
                            command.Parameters.AddWithValue("@claveSup", dt.CLAVESUP);
                            command.Parameters.AddWithValue("@pDevolucion", dt.PDEVOLUCION);
                            command.Parameters.AddWithValue("@valVC", dt.VALVC);
                            command.Parameters.AddWithValue("@modificarInfoSucursal", dt.modificarInfoSucursal);
                            command.Parameters.AddWithValue("@venderConExistencia", dt.venderConExistencia);
                            command.Parameters.AddWithValue("@nameImp1", dt.nameImp1);
                            command.Parameters.AddWithValue("@nameImp2", dt.nameImp2);
                            command.Parameters.AddWithValue("@nameImp3", dt.nameImp3);
                            command.Parameters.AddWithValue("@nameReten1", dt.nameReten1);
                            command.Parameters.AddWithValue("@nameReten2", dt.nameReten2); 
                            command.Parameters.AddWithValue("@banauto", 1); 
                            int recordSaved = command.ExecuteNonQuery();
                            if (recordSaved != 0)
                                lastId = Convert.ToInt32(dt.CONFIGURA_ID);
                        }
                    }
                }
                catch (SQLiteException e)
                {
                    SECUDOC.writeLog("Exception: " + e.ToString());
                }
                finally
                {
                    if (db != null && db.State == ConnectionState.Open)
                        db.Close();
                }
            }
            return lastId;
        }

        public static int saveAllDatosTicketLAN(ClsDatosTicketModel dt)
        {
            int lastId = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "INSERT INTO " + LocalDatabase.TABLA_DATOSTICKET + " VALUES(@id, @empresa, @direccion, @rfc, " +
                        "@expedido, @pieVentas, @pieCredito, @piePedido, @pieCotizacion, @pieCobranza, @pieDevolucion, " +
                        "@ftpServer, @ftpUser, @ftpPassword, @ftpPuerto, @claveSup, @pDevolucion, @valVC, @modificarInfoSucursal, " +
                        "@venderConExistencia, @nameImp1, @nameImp2, @nameImp3, @nameReten1, @nameReten2,@banactualizar)";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", dt.CONFIGURA_ID);
                    command.Parameters.AddWithValue("@empresa", dt.EMPRESA);
                    command.Parameters.AddWithValue("@direccion", dt.DIRECCION);
                    command.Parameters.AddWithValue("@rfc", dt.RFC);
                    command.Parameters.AddWithValue("@expedido", dt.EXPEDIDO);
                    command.Parameters.AddWithValue("@pieVentas", dt.PIE_TICKETVENTAS);
                    command.Parameters.AddWithValue("@pieCredito", dt.PIE_TICKETCREDITO);
                    command.Parameters.AddWithValue("@piePedido", dt.PIE_TICKETPEDIDO);
                    command.Parameters.AddWithValue("@pieCotizacion", dt.PIE_TICKETCOTIZACION);
                    command.Parameters.AddWithValue("@pieCobranza", dt.PIE_TICKETCOBRANZA);
                    command.Parameters.AddWithValue("@pieDevolucion", dt.PIE_TICKETDEVOLUCION);
                    command.Parameters.AddWithValue("@ftpServer", dt.FTPSERVER);
                    command.Parameters.AddWithValue("@ftpUser", dt.FTPUSER);
                    command.Parameters.AddWithValue("@ftpPassword", dt.FTPPASSWORD);
                    command.Parameters.AddWithValue("@ftpPuerto", dt.FTPPUERTO);
                    command.Parameters.AddWithValue("@claveSup", dt.CLAVESUP);
                    command.Parameters.AddWithValue("@pDevolucion", dt.PDEVOLUCION);
                    command.Parameters.AddWithValue("@valVC", dt.VALVC);
                    command.Parameters.AddWithValue("@modificarInfoSucursal", dt.modificarInfoSucursal);
                    command.Parameters.AddWithValue("@venderConExistencia", dt.venderConExistencia);
                    command.Parameters.AddWithValue("@nameImp1", dt.nameImp1);
                    command.Parameters.AddWithValue("@nameImp2", dt.nameImp2);
                    command.Parameters.AddWithValue("@nameImp3", dt.nameImp3);
                    command.Parameters.AddWithValue("@nameReten1", dt.@nameReten1);
                    command.Parameters.AddWithValue("@nameReten2", dt.@nameReten2);
                    command.Parameters.AddWithValue("@banactualizar", 1);
                    int recordSaved = command.ExecuteNonQuery();
                    if (recordSaved != 0)
                        lastId = Convert.ToInt32(dt.CONFIGURA_ID);
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog("Exception: " + e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return lastId;
        }

        public static int getTheTotalNumberOfTicketsNotSentToTheServer()
        {
            int total = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT COUNT(" + LocalDatabase.CAMPO_ID_RESPALDOTICKETS + ") FROM " + LocalDatabase.TABLA_RESPALDOTICKETS + " " +
                        " WHERE " + LocalDatabase.CAMPO_IDWS_RESPALDOTICKETS + " = " + 0;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                total = reader.GetInt32(0);
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

        public static DatosTicketModel getAllData()
        {
            DatosTicketModel dtm = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_DATOSTICKET;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dtm = new DatosTicketModel();
                                dtm.EMPRESA = reader[LocalDatabase.CAMPO_TICEMPRESA].ToString().Trim();
                                dtm.DIRECCION = reader[LocalDatabase.CAMPO_TICDIRECCION].ToString().Trim();
                                dtm.RFC = reader[LocalDatabase.CAMPO_TICRFC].ToString().Trim();
                                dtm.EXPEDIDO = reader[LocalDatabase.CAMPO_TICEXPENDIDO].ToString().Trim();
                                dtm.PIE_TICKETVENTAS = reader[LocalDatabase.CAMPO_TICVENTA].ToString().Trim();
                                dtm.PIE_TICKETCREDITO = reader[LocalDatabase.CAMPO_TICCREDITO].ToString().Trim();
                                dtm.PIE_TICKETPEDIDO = reader[LocalDatabase.CAMPO_TICPEDIDO].ToString().Trim();
                                dtm.PIE_TICKETCOTIZACION = reader[LocalDatabase.CAMPO_TICCOTIZACION].ToString().Trim();
                                dtm.PIE_TICKETCOBRANZA = reader[LocalDatabase.CAMPO_TICCOBRANZA].ToString().Trim();
                                dtm.PIE_TICKETDEVOLUCION = reader[LocalDatabase.CAMPO_TICDEVOLUCION].ToString().Trim();
                                dtm.FTPSERVER = reader[LocalDatabase.CAMPO_TICFTPSERVER].ToString().Trim();
                                dtm.FTPUSER = reader[LocalDatabase.CAMPO_TICFTPUSER].ToString().Trim();
                                dtm.FTPPASSWORD = reader[LocalDatabase.CAMPO_TICFTPPASS].ToString().Trim();
                                if (reader[LocalDatabase.CAMPO_TICFTPPUETO] == null || reader[LocalDatabase.CAMPO_TICFTPPUETO].ToString().Trim().Equals(""))
                                {
                                    dtm.FTPPUERTO = 21;
                                }
                                else
                                {
                                    dtm.FTPPUERTO = Convert.ToInt32(reader[LocalDatabase.CAMPO_TICFTPPUETO].ToString().Trim());
                                }
                                dtm.CLAVESUP = reader[LocalDatabase.CAMPO_TICCLAVESUP].ToString().Trim();
                                dtm.PDEVOLUCION = reader[LocalDatabase.CAMPO_TICPDEVOLUCION].ToString().Trim();
                                dtm.VALVC = reader[LocalDatabase.CAMPO_TICVALVC].ToString().Trim();
                                dtm.modificarInfoSucursal = Convert.ToInt32(reader["modify_branch_information"].ToString().Trim());
                                dtm.venderConExistencia = Convert.ToInt32(reader["venderConExistencia"].ToString().Trim());
                                if (reader[LocalDatabase.CAMPO_NAMEIMP1_DATOSTICKET] != DBNull.Value)
                                    dtm.nameImp1 = reader[LocalDatabase.CAMPO_NAMEIMP1_DATOSTICKET].ToString().Trim();
                                else dtm.nameImp1 = "";
                                if (reader[LocalDatabase.CAMPO_NAMEIMP2_DATOSTICKET] != DBNull.Value)
                                    dtm.nameImp2 = reader[LocalDatabase.CAMPO_NAMEIMP2_DATOSTICKET].ToString().Trim();
                                else dtm.nameImp2 = "";
                                if (reader[LocalDatabase.CAMPO_NAMEIMP3_DATOSTICKET] != DBNull.Value)
                                    dtm.nameImp3 = reader[LocalDatabase.CAMPO_NAMEIMP3_DATOSTICKET].ToString().Trim();
                                else dtm.nameImp3 = "";
                                if (reader[LocalDatabase.CAMPO_NAMERETEN1_DATOSTICKET] != DBNull.Value)
                                    dtm.nameReten1 = reader[LocalDatabase.CAMPO_NAMERETEN1_DATOSTICKET].ToString().Trim();
                                else dtm.nameReten1 = "";
                                if (reader[LocalDatabase.CAMPO_NAMERETEN2_DATOSTICKET] != DBNull.Value)
                                    dtm.nameReten2 = reader[LocalDatabase.CAMPO_NAMERETEN2_DATOSTICKET].ToString().Trim();
                                else dtm.nameReten2 = "";
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
            return dtm;
        }

        public static DatosTicketModel getDataForFTPServer()
        {
            DatosTicketModel dtm = null;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_DATOSTICKET;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dtm = new DatosTicketModel();
                                dtm.EMPRESA = reader[LocalDatabase.CAMPO_TICEMPRESA].ToString().Trim();
                                dtm.FTPSERVER = reader[LocalDatabase.CAMPO_TICFTPSERVER].ToString().Trim();
                                dtm.FTPUSER = reader[LocalDatabase.CAMPO_TICFTPUSER].ToString().Trim();
                                dtm.FTPPASSWORD = reader[LocalDatabase.CAMPO_TICFTPPASS].ToString().Trim();
                                if (reader[LocalDatabase.CAMPO_TICFTPPUETO] == DBNull.Value)
                                    dtm.FTPPUERTO = 0;
                                else
                                {
                                    if (reader[LocalDatabase.CAMPO_TICFTPPUETO].ToString().Trim().Equals(""))
                                        dtm.FTPPUERTO = 0;
                                    else dtm.FTPPUERTO = Convert.ToInt32(reader[LocalDatabase.CAMPO_TICFTPPUETO].ToString().Trim());
                                }
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
            return dtm;
        }

        public static String getNameImp(int impuesto)
        {
            String name = "";
            var db = new SQLiteConnection();            
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT nameImpuesto"+impuesto+" FROM " + LocalDatabase.TABLA_DATOSTICKET+" WHERE "+
                    LocalDatabase.CAMPO_TICCONFIGURA+" = 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    name = reader.GetValue(0).ToString().Trim();
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
            return name;
        }

        public static String getticketrespaldo(String referencia)
        {
            String name = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT "+LocalDatabase.CAMPO_DATOS_RESPALDOTICKETS+" FROM "+LocalDatabase.TABLA_RESPALDOTICKETS+
                    " WHERE " + LocalDatabase.CAMPO_REFERENCIA_RESPALDOTICKETS + " = '" + referencia + "'";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    name = reader.GetValue(0).ToString().Trim();
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
            return name;
        }

        public static int putticketrespaldo(String referencia,String datos, String tipoDocumento)
        {
            int valores = 0;

            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "INSERT INTO "+LocalDatabase.TABLA_RESPALDOTICKETS+" ("+LocalDatabase.CAMPO_REFERENCIA_RESPALDOTICKETS
                    +", "+LocalDatabase.CAMPO_DATOS_RESPALDOTICKETS+","+ LocalDatabase.CAMPO_IDAGENTE_RESPALDOTICKETS+","+
                    LocalDatabase.CAMPO_TIPODOCUMENTO_RESPALDOTICKETS+ ", "+ LocalDatabase.CAMPO_FECHA_RESPALDOTICKETS + ") values ('" + referencia 
                    + "', '"+ datos + "',"+ ClsRegeditController.getIdUserInTurn() +", '"+ tipoDocumento +"', '"+
                    MetodosGenerales.getCurrentDateAndHour() + "')";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int val = command.ExecuteNonQuery();
                    valores = val;
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
            return valores;
        }
        public static bool sellOnlyWithStock()
        {
            bool yes = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT "+LocalDatabase.CAMPO_VENDERCONEXISTENCIA_DATOSTICKET+" FROM "+
                    LocalDatabase.TABLA_DATOSTICKET+" WHERE "+LocalDatabase.CAMPO_TICCONFIGURA+" = 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) > 0)
                                        yes = true;
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
            return yes;
        }

        public static Boolean deleteAllDatosTicket()
        {
            bool deleted = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "DELETE FROM " + LocalDatabase.TABLA_DATOSTICKET;
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

        public static void deleteAllDatosTicket(SQLiteConnection db)
        {
            String query = "DELETE FROM " + LocalDatabase.TABLA_DATOSTICKET;
            using (SQLiteCommand command = new SQLiteCommand(query, db))
            {
                int records = command.ExecuteNonQuery();
            }
        }

    }
}
