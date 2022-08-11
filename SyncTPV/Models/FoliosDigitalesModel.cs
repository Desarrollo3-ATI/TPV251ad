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
    public class FoliosDigitalesModel
    {
        public int id { get; set; }
        public int tipoDocumento { get; set; }
        public int idConcepto { get; set; }
        public int idDocumento { get; set; }
        public String serieConcepto { get; set; }
        public String folio { get; set; }
        public int estado { get; set; }
        public int entregado { get; set; }
        public DateTime fechaEmision { get; set; }
        public String emailEntrega { get; set; }
        public String rutaDiscoEntrega { get; set; }
        public DateTime fechaCancelacion { get; set; }
        public String horaCancelacion { get; set; }
        public int tradicionalDigital { get; set; }
        public String tipoCFDI { get; set; }
        public String rfcEmisor { get; set; }
        public String razonSocialEmisor { get; set; }
        public String codigoUSoCFDI { get; set; }
        public String uuid { get; set; }
        public String idADD { get; set; }
        public int idServer { get; set; }

        public static int createFolioDigital(int id, int tipoDocumento, int idConcepto, int idDocumento, String serieConcepto, String folio,
            int estado, int entregado, DateTime fechaEmision, String emailEntrega, String rutaDiscoEntrega, DateTime fechaCancelacion, 
            String horaCancelacion, int tradicionalDigital, String tipoCFDI, String rfcEmisor, String razonSocialEmisor, String codigoUsoCFDI, 
            String uuid,String idAdd, int idServer)
        {
            int idCreated = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "INSERT INTO " + LocalDatabase.TABLA_FOLIOSDIGITALES + " VALUES(@id, @tipoDocumento, @idConcepto, @idDocumento, " +
                    "@serieConcepto, @folio, @estado, @entregado, @fechaEmision, @emailEntrega, @rutaDiscoEntrega, " +
                    "@fechaCancelacion, @horaCancelacion, @tradicionalDigital, @tipoCFDI, @rfcEmisor, @razonSocialEmisor, @codigoUsoCFDI, @uuid, " +
                    "@idADD, @idServer)";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@tipoDocumento", tipoDocumento);
                    command.Parameters.AddWithValue("@idConcepto", idConcepto);
                    command.Parameters.AddWithValue("@idDocumento", idDocumento);
                    command.Parameters.AddWithValue("@serieConcepto", serieConcepto);
                    command.Parameters.AddWithValue("@folio", folio);
                    command.Parameters.AddWithValue("@estado", estado);
                    command.Parameters.AddWithValue("@entregado", entregado);
                    command.Parameters.AddWithValue("@fechaEmision", fechaEmision);
                    command.Parameters.AddWithValue("@emailEntrega", emailEntrega);
                    command.Parameters.AddWithValue("@rutaDiscoEntrega", rutaDiscoEntrega);
                    command.Parameters.AddWithValue("@fechaCancelacion", fechaCancelacion);
                    command.Parameters.AddWithValue("@horaCancelacion", horaCancelacion);
                    command.Parameters.AddWithValue("@tradicionalDigital", tradicionalDigital);
                    command.Parameters.AddWithValue("@tipoCFDI", tipoCFDI);
                    command.Parameters.AddWithValue("@rfcEmisor", rfcEmisor);
                    command.Parameters.AddWithValue("@razonSocialEmisor", razonSocialEmisor);
                    command.Parameters.AddWithValue("@codigoUsoCFDI", codigoUsoCFDI);
                    command.Parameters.AddWithValue("@uuid", uuid);
                    command.Parameters.AddWithValue("@idADD", idAdd);
                    command.Parameters.AddWithValue("@idServer", idServer);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        idCreated = id;
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
            return idCreated;
        }

        public static bool updateFolioDigital(int id, int tipoDocumento, int idConcepto, int idDocumento, String serieConcepto, String folio,
            int estado, int entregado, DateTime fechaEmision, String emailEntrega, String rutaDiscoEntrega, DateTime fechaCancelacion,
            String horaCancelacion, int tradicionalDigital, String tipoCFDI, String rfcEmisor, String razonSocialEmisor, String codigoUsoCFDI,
            String uuid,String idAdd, int idServer)
        {
            bool updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_FOLIOSDIGITALES + " SET " + 
                    LocalDatabase.CAMPO_TIPODOC_FOLIODIGITAL + " = @tipoDocumento, " +
                    LocalDatabase.CAMPO_IDCONCEPTO_FOLIODIGITAL + " = @idConcepto, " + 
                    LocalDatabase.CAMPO_IDDOCUMENTO_FOLIODIGITAL + " = @idDocumento, " +
                    LocalDatabase.CAMPO_SERIECONCEPTO_FOLIODIGITAL + " = @serieConcepto, " +
                    LocalDatabase.CAMPO_FOLIO_FOLIODIGITAL+" = @folio," +
                    LocalDatabase.CAMPO_ESTADO_FOLIODIGITAL+ " = @estado, " +
                    LocalDatabase.CAMPO_ENTREGADO_FOLIODIGITAL+ " = @entregado, " +
                    LocalDatabase.CAMPO_FECHAEMISION_FOLIODIGITAL+ " = @fechaEmision, " +
                    LocalDatabase.CAMPO_EMAILENTREGA_FOLIODIGITAL+ " = @emailEntrega," +
                    LocalDatabase.CAMPO_RUTADISCOENTREGA_FOLIODIGITAL+ " = @rutaDiscoEntrega, " +
                    LocalDatabase.CAMPO_FECHACANCELACION_FOLIODIGITAL+ " = @fechaCancelacion, " +
                    LocalDatabase.CAMPO_HORACANCELACION_FOLIODIGITAL+ " = @horaCancelacion, " +
                    LocalDatabase.CAMPO_TRADICIONALDIGITAL_FOLIODIGITAL+ " = @tradicionalDigital, " +
                    LocalDatabase.CAMPO_TIPOCFDI_FOLIODIGITAL+ " = @tipoCFDI, " +
                    LocalDatabase.CAMPO_RFCEMISOR_FOLIODIGITAL+ " = @rfcEmisor, " +
                    LocalDatabase.CAMPO_RAZONSOCIALEMISOR_FOLIODIGITAL+ " = @razonSocialEmisor, " +
                    LocalDatabase.CAMPO_CODIGOUSOCFDI_FOLIODIGITAL + " = @codigoUsoCFDI, " +
                    LocalDatabase.CAMPO_UUID_FOLIODIGITAL+ " = @uuid, " +
                    LocalDatabase.CAMPO_IDADD_FOLIODIGITAL+ " = @idAdd, " +
                    LocalDatabase.CAMPO_IDSERVER_FOLIODIGITAL+ " = @idServer WHERE " +
                    LocalDatabase.CAMPO_ID_FOLIODIGITAL + " = @id";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@tipoDocumento", tipoDocumento);
                    command.Parameters.AddWithValue("@idConcepto", idConcepto);
                    command.Parameters.AddWithValue("@idDocumento", idDocumento);
                    command.Parameters.AddWithValue("@serieConcepto", serieConcepto);
                    command.Parameters.AddWithValue("@folio", folio);
                    command.Parameters.AddWithValue("@estado", estado);
                    command.Parameters.AddWithValue("@entregado", entregado);
                    command.Parameters.AddWithValue("@fechaEmision", fechaEmision);
                    command.Parameters.AddWithValue("@emailEntrega", emailEntrega);
                    command.Parameters.AddWithValue("@rutaDiscoEntrega", rutaDiscoEntrega);
                    command.Parameters.AddWithValue("@fechaCancelacion", fechaCancelacion);
                    command.Parameters.AddWithValue("@horaCancelacion", horaCancelacion);
                    command.Parameters.AddWithValue("@tradicionalDigital", tradicionalDigital);
                    command.Parameters.AddWithValue("@tipoCFDI", tipoCFDI);
                    command.Parameters.AddWithValue("@rfcEmisor", rfcEmisor);
                    command.Parameters.AddWithValue("@razonSocialEmisor", razonSocialEmisor);
                    command.Parameters.AddWithValue("@codigoUsoCFDI", codigoUsoCFDI);
                    command.Parameters.AddWithValue("@uuid", uuid);
                    command.Parameters.AddWithValue("@idAdd", idAdd);
                    command.Parameters.AddWithValue("@idServer", idServer);
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

        public static bool validateIfFolioDigitalExistByIdDocumentoServer(int idDocumentoServer)
        {
            bool exist = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_FOLIOSDIGITALES + " WHERE " +
                    LocalDatabase.CAMPO_IDDOCUMENTO_FOLIODIGITAL + " = @idDocumentoServer";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idDocumentoServer", idDocumentoServer);
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

        public static FoliosDigitalesModel getFolioDigital(int idDocumentoServer)
        {
            FoliosDigitalesModel fdm = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_FOLIOSDIGITALES + " WHERE " +
                    LocalDatabase.CAMPO_IDDOCUMENTO_FOLIODIGITAL + " = @idDocumentoServer";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idDocumentoServer", idDocumentoServer);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                fdm = new FoliosDigitalesModel();
                                fdm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_FOLIODIGITAL].ToString().Trim());
                                fdm.tipoDocumento = Convert.ToInt32(reader[LocalDatabase.CAMPO_TIPODOC_FOLIODIGITAL].ToString().Trim());
                                fdm.serieConcepto = reader[LocalDatabase.CAMPO_SERIECONCEPTO_FOLIODIGITAL].ToString().Trim();
                                fdm.folio = reader[LocalDatabase.CAMPO_FOLIO_FOLIODIGITAL].ToString().Trim();
                                fdm.rfcEmisor = reader[LocalDatabase.CAMPO_RFCEMISOR_FOLIODIGITAL].ToString().Trim();
                                fdm.uuid = reader[LocalDatabase.CAMPO_UUID_FOLIODIGITAL].ToString().Trim();
                                fdm.idADD = reader[LocalDatabase.CAMPO_IDADD_FOLIODIGITAL].ToString().Trim();
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
            return fdm;
        }

        public static bool deleteFolioDigital(int idDocumentoServer)
        {
            bool deleted = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "DELETE FROM " + LocalDatabase.TABLA_FOLIOSDIGITALES + " WHERE " +
                    LocalDatabase.CAMPO_IDDOCUMENTO_FOLIODIGITAL + " = @idDocumentoServer";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idDocumentoServer", idDocumentoServer);
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
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return deleted;
        }

        public static bool deleteAllFoliosDigitales()
        {
            bool deleted = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "DELETE FROM " + LocalDatabase.TABLA_FOLIOSDIGITALES;
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
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return deleted;
        }

    }
}
