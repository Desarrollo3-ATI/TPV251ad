using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Data;
using System.Data.SQLite;
using System.Dynamic;

namespace SyncTPV.Models
{
    public class PrinterModel
    {
        public int id { get; set; }
        public String nombre { get; set; }
        public String mac { get; set; }
        public int tamanio { get; set; }
        public int tipo { get; set; }
        public String original { get; set; }
        public String copia { get; set; }
        public int showFolio { get; set; }
        public int showCodigoCaja { get; set; }
        public int showCodigoUsuario { get; set; }
        public int showNombreUsuario { get; set; }
        public int showFechaHora { get; set; }
        public int showPorcentajeDescuentoMovimiento { get; set; }
        

        public static ExpandoObject saveANewPrinter(String name, String direccion, int tipoTicket, int tipoPrinter, String leyendaOriginal, 
            String leyendaCopia, int showFolio, int showCodigoCaja, int showCodigoUsuario, int showNombreUsuario, int showFechaHora, int showPorcentajeDescuentoMovimiento)
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
                String query = "INSERT INTO " + LocalDatabase.TABLA_IMPRESORAS +
                    " VALUES(@id, @name, @direccion, @tipoTicket, @tipoPrinter, @leyendaOriginal, @leyendaCopia, @showFolio, @showCodigoCaja, " +
                    "@showCodigoUsuario, @showNombreUsuario, @showFechaHora, @showPorcentajeDescuentoMovimiento)";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", 1);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@direccion", direccion);
                    command.Parameters.AddWithValue("@tipoTicket", tipoTicket);
                    command.Parameters.AddWithValue("@tipoPrinter", tipoPrinter);
                    command.Parameters.AddWithValue("@leyendaOriginal", leyendaOriginal);
                    command.Parameters.AddWithValue("@leyendaCopia", leyendaCopia);
                    command.Parameters.AddWithValue("@showFolio", showFolio);
                    command.Parameters.AddWithValue("@showCodigoCaja", showCodigoCaja);
                    command.Parameters.AddWithValue("@showCodigoUsuario", showCodigoUsuario);
                    command.Parameters.AddWithValue("@showNombreUsuario", showNombreUsuario);
                    command.Parameters.AddWithValue("@showFechaHora", showFechaHora);
                    command.Parameters.AddWithValue("@showPorcentajeDescuentoMovimiento", showPorcentajeDescuentoMovimiento);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                    {
                        value = 1;
                        saved = true;
                    }
                    else description = "No pudimos agregar la configuración de la impresora!";
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

        public static ExpandoObject updatePrinter(String name, String direccion, int tipoTicket, int tipoPrinter, String original,
            String copia)
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
                String query = "UPDATE " + LocalDatabase.TABLA_IMPRESORAS + " SET " + LocalDatabase.CAMPO_NOMBRE_IMPRESORA + " = @name, " +
                    LocalDatabase.CAMPO_DIRECCION_MAC + " = @direccion, " + LocalDatabase.CAMPO_TIPO_TICKET + " = @tipoTicket, " +
                    LocalDatabase.CAMPO_TIPO_IMPRESORA + " = @tipoImpresora, "+
                    LocalDatabase.CAMPO_ORIGINAL_IMPRESORA+" = @original, " +
                    LocalDatabase.CAMPO_COPIA_IMPRESORA+" = @copia WHERE " + LocalDatabase.CAMPO_ID_IMPRESORA + " = " + 1;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@direccion", direccion);
                    command.Parameters.AddWithValue("@tipoTicket", tipoTicket);
                    command.Parameters.AddWithValue("@tipoImpresora", tipoPrinter);
                    command.Parameters.AddWithValue("@original", original);
                    command.Parameters.AddWithValue("@copia", copia);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                    {
                        value = 1;
                        updated = true;
                    }
                    else description = "No pudimos actualizar la configuración de la impresora";
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

        public static bool updateShowEncabezadoField(int idPrinter, int type, int status)
        {
            bool updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_IMPRESORAS + " SET ";
                if (type == 0)
                    query += LocalDatabase.CAMPO_SHOWFOLIO_IMPRESORA + " = @status ";
                else if (type == 1)
                    query += LocalDatabase.CAMPO_SHOWCODIGOCAJA_IMPRESORA + " = @status ";
                else if (type == 2)
                    query += LocalDatabase.CAMPO_SHOWCODIGOUSUARIO_IMPRESORA + " = @status ";
                else if (type == 3)
                    query += LocalDatabase.CAMPO_SHOWNOMBREUSUARIO_IMPRESORA + " = @status ";
                else if (type == 4)
                    query += LocalDatabase.CAMPO_SHOWFECHAHORA_IMPRESORA + " = @status ";
                else if (type == 5)
                    query += LocalDatabase.CAMPO_SHOWPORCENTAJEDESCUENTOMOVIMIENTO_IMPRESORA + " = @status ";
                query += " WHERE " + LocalDatabase.CAMPO_ID_IMPRESORA + " = " + 1;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@status", status);
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

        public static Boolean verifyIfAPrinterIdAdded()
        {
            Boolean exist = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_IMPRESORAS + " WHERE " + LocalDatabase.CAMPO_ID_IMPRESORA + " = " + 1;
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

        public static PrinterModel getallDataFromAPrinter()
        {
            PrinterModel pm = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_IMPRESORAS + " WHERE " + LocalDatabase.CAMPO_ID_IMPRESORA + " = " + 1;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                pm = new PrinterModel();
                                pm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_IMPRESORA].ToString().Trim());
                                pm.nombre = reader[LocalDatabase.CAMPO_NOMBRE_IMPRESORA].ToString().Trim();
                                pm.mac = reader[LocalDatabase.CAMPO_DIRECCION_MAC].ToString().Trim();
                                pm.tamanio = Convert.ToInt32(reader[LocalDatabase.CAMPO_TIPO_TICKET].ToString().Trim());
                                pm.tipo = Convert.ToInt32(reader[LocalDatabase.CAMPO_TIPO_IMPRESORA].ToString().Trim());
                                pm.original = reader[LocalDatabase.CAMPO_ORIGINAL_IMPRESORA].ToString().Trim();
                                pm.copia = reader[LocalDatabase.CAMPO_COPIA_IMPRESORA].ToString().Trim();
                                pm.showFolio = Convert.ToInt32(reader[LocalDatabase.CAMPO_SHOWFOLIO_IMPRESORA].ToString().Trim());
                                pm.showCodigoCaja = Convert.ToInt32(reader[LocalDatabase.CAMPO_SHOWCODIGOCAJA_IMPRESORA].ToString().Trim());
                                pm.showCodigoUsuario = Convert.ToInt32(reader[LocalDatabase.CAMPO_SHOWCODIGOUSUARIO_IMPRESORA].ToString().Trim());
                                pm.showNombreUsuario = Convert.ToInt32(reader[LocalDatabase.CAMPO_SHOWNOMBREUSUARIO_IMPRESORA].ToString().Trim());
                                pm.showFechaHora = Convert.ToInt32(reader[LocalDatabase.CAMPO_SHOWFECHAHORA_IMPRESORA].ToString().Trim());
                                pm.showPorcentajeDescuentoMovimiento = Convert.ToInt32(reader[LocalDatabase.CAMPO_SHOWPORCENTAJEDESCUENTOMOVIMIENTO_IMPRESORA].ToString().Trim());
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
            return pm;
        }

        public static String getTextoOriginal()
        {
            String original = "Original";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT "+LocalDatabase.CAMPO_ORIGINAL_IMPRESORA+" FROM " + LocalDatabase.TABLA_IMPRESORAS + " WHERE " + 
                    LocalDatabase.CAMPO_ID_IMPRESORA + " = " + 1;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader[LocalDatabase.CAMPO_ORIGINAL_IMPRESORA] != DBNull.Value)
                                    original = reader[LocalDatabase.CAMPO_ORIGINAL_IMPRESORA].ToString().Trim();
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
            return original;
        }

        public static String getTextoCopia()
        {
            String original = "Copia";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_COPIA_IMPRESORA + " FROM " + LocalDatabase.TABLA_IMPRESORAS + " WHERE " + 
                    LocalDatabase.CAMPO_ID_IMPRESORA + " = " + 1;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader[LocalDatabase.CAMPO_COPIA_IMPRESORA] != DBNull.Value)
                                    original = reader[LocalDatabase.CAMPO_COPIA_IMPRESORA].ToString().Trim();
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
            return original;
        }

    }
}
