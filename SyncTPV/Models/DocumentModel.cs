using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Dynamic;
using System.Threading.Tasks;
using wsROMClase;

namespace SyncTPV
{
    public class clsDocumento
    {
        public int id { set; get; }
        public string clave_cliente { set; get; }
        public int cliente_id { set; get; }
        public int descuento { set; get; }
        public double total { set; get; }
        public string NOMBREU { set; get; }
        public int ALMACE_ID { set; get; }
        public decimal ANTICIPO { set; get; }
        public string TIPO_DOCUMENTO { set; get; }
        public int FORMA_COBRO_ID { set; get; }
        public int FACTURA { set; get; }
        public string observacion { set; get; }
        public int DEV { set; get; }
        public string FVENTA { set; get; }
        public string FECHAHORAMOV { set; get; }
        public int USUARIO_ID { set; get; }
        public int FORMA_COBRO_ID_ABONO { set; get; }
        public int CLIENTE_ID { set; get; }
        public int CIDDOCTOPEDIDOCC { set; get; }
        public int cancelado { set; get; }
        public int enviado_al_ws { set; get; }
        public int id_web_service { set; get; }
        public int archivado { set; get; }
        public int pausado { set; get; }
        public string descripcion { set; get; }
        public double cambio { set; get; }

    }

    public class DocumentModel
    {
        public static readonly int TIPO_COTIZACION = 1;
        public static readonly int TIPO_COTIZACION_MOSTRADOR = 51;
        public static readonly int TIPO_PREPEDIDO = 50; //Prepedido
        public static readonly int TIPO_VENTA = 2;
        public static readonly int TIPO_PEDIDO = 3;
        public static readonly int TIPO_REMISION = 4;
        public static readonly int TIPO_DEVOLUCION = 5;
        public static readonly int TIPO_ENTRADA_ALMACEN = 32;
        public static readonly int TIPO_TRASPASO_ALMACEN = 34;
        public static readonly int TIPO_SALIDA_ALMACEN = 33;
        public static readonly int FORMA_PAGO_CREDITO = 71;

        /* Opciones para Remisiones Fiscales */
        public static readonly int FISCAL_FACTURAR = 2;
        public static readonly int FISCAL_NO_FACTURAR = 3;
        public static readonly int NO_FISCAL_NO_FACTURAR = 4;

        public int id { get; set; }
        public String clave_cliente { get; set; }
        public int cliente_id { get; set; }
        public double descuento { get; set; }
        public double total { get; set; }
        public String nombreu { get; set; }
        public int almacen_id { get; set; }
        public double anticipo { get; set; }
        public int tipo_documento { get; set; }
        public int forma_cobro_id { get; set; }
        public int factura { get; set; }
        public String observacion { get; set; }
        public int dev { get; set; }
        public String fventa { get; set; }
        public String fechahoramov { get; set; }
        public int usuario_id { get; set; }
        public int forma_corbo_id_abono { get; set; }
        public int ciddoctopedidocc { get; set; }
        public int estado { get; set; }
        public int enviadoAlWs { get; set; }
        public int idWebService { get; set; }
        public int documentoArchivado { get; set; }
        public int pausado { set; get; }
        public int papeleraReciclaje { set; get; }
        public String claveUsuario { get; set; }
        public List<MovimientosModel> movimientos { get; set; }
        public List<FormasDeCobroDocumentoModel> formasCobroDocumento { get; set; }

        public static DocumentModel getAllDataDocumento(int idDocument)
        {
            DocumentModel documentoVentaModel = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                        LocalDatabase.CAMPO_ID_DOC + " = " + idDocument;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                documentoVentaModel = new DocumentModel();
                                documentoVentaModel.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_DOC].ToString().Trim());
                                documentoVentaModel.cliente_id = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLIENTEID_DOC].ToString().Trim());
                                documentoVentaModel.clave_cliente = reader[LocalDatabase.CAMPO_CLAVECLIENTE_DOC].ToString().Trim();
                                documentoVentaModel.descuento = Convert.ToDouble(reader[LocalDatabase.CAMPO_DESCUENTO_DOC].ToString().Trim());
                                documentoVentaModel.total = Convert.ToDouble(reader[LocalDatabase.CAMPO_TOTAL_DOC].ToString().Trim());
                                documentoVentaModel.nombreu = reader[LocalDatabase.CAMPO_NOMBREU_DOC].ToString().Trim();
                                documentoVentaModel.almacen_id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ALMACENID_DOC].ToString().Trim());
                                documentoVentaModel.anticipo = Convert.ToDouble(reader[LocalDatabase.CAMPO_ANTICIPO_DOC].ToString().Trim());
                                documentoVentaModel.tipo_documento = Convert.ToInt32(reader[LocalDatabase.CAMPO_TIPODOCUMENTO_DOC].ToString().Trim());
                                documentoVentaModel.forma_cobro_id = Convert.ToInt32(reader[LocalDatabase.CAMPO_FORMACOBROID_DOC].ToString().Trim());
                                documentoVentaModel.factura = Convert.ToInt32(reader[LocalDatabase.CAMPO_FACTURA_DOC].ToString().Trim());
                                documentoVentaModel.observacion = reader[LocalDatabase.CAMPO_OBSERVACION_DOC].ToString().Trim();
                                documentoVentaModel.dev = Convert.ToInt32(reader[LocalDatabase.CAMPO_DEV_DOC].ToString().Trim());
                                documentoVentaModel.fventa = reader[LocalDatabase.CAMPO_FVENTA_DOC].ToString().Trim();
                                documentoVentaModel.fechahoramov = reader[LocalDatabase.CAMPO_FECHAHORAMOV_DOC].ToString().Trim();
                                documentoVentaModel.usuario_id = Convert.ToInt32(reader[LocalDatabase.CAMPO_USUARIOID_DOC].ToString().Trim());
                                documentoVentaModel.forma_corbo_id_abono = Convert.ToInt32(reader[LocalDatabase.CAMPO_FORMACOBROIDABONO_DOC].ToString().Trim());
                                documentoVentaModel.ciddoctopedidocc = Convert.ToInt32(reader[LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_DOC].ToString().Trim());
                                documentoVentaModel.estado = Convert.ToInt32(reader[LocalDatabase.CAMPO_CANCELADO_DOC].ToString().Trim());
                                documentoVentaModel.enviadoAlWs = Convert.ToInt32(reader[LocalDatabase.CAMPO_ENVIADOALWS_DOC].ToString().Trim());
                                documentoVentaModel.documentoArchivado = Convert.ToInt32(reader[LocalDatabase.CAMPO_ARCHIVADO_DOC].ToString().Trim());
                                documentoVentaModel.pausado = Convert.ToInt32(reader[LocalDatabase.CAMPO_PAUSAR_DOC].ToString().Trim());
                                documentoVentaModel.papeleraReciclaje = Convert.ToInt32(reader[LocalDatabase.CAMPO_PAPELERARECICLAJE_DOC].ToString().Trim());
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
            return documentoVentaModel;
        }

        public static int addNewDocument(DocumentModel dvm, int idDocument)
        {
            int idDocumentoCreated = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "INSERT INTO " + LocalDatabase.TABLA_DOCUMENTOVENTA + " VALUES(@" + LocalDatabase.CAMPO_ID_DOC + ", @" + LocalDatabase.CAMPO_CLAVECLIENTE_DOC + ", " +
                    "@" + LocalDatabase.CAMPO_CLIENTEID_DOC + ", @" + LocalDatabase.CAMPO_DESCUENTO_DOC + ", @" + LocalDatabase.CAMPO_TOTAL_DOC +
                    ", @" + LocalDatabase.CAMPO_NOMBREU_DOC + ", @" + LocalDatabase.CAMPO_ALMACENID_DOC + ", " + " @" + LocalDatabase.CAMPO_ANTICIPO_DOC + ", " +
                    "@" + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + ", @" + LocalDatabase.CAMPO_FORMACOBROID_DOC + ", @" + LocalDatabase.CAMPO_FACTURA_DOC + ", " +
                    "@" + LocalDatabase.CAMPO_OBSERVACION_DOC + ", @" + LocalDatabase.CAMPO_DEV_DOC + ", @" + LocalDatabase.CAMPO_FVENTA_DOC + ", " +
                    "@" + LocalDatabase.CAMPO_FECHAHORAMOV_DOC + ", " +
                    "@" + LocalDatabase.CAMPO_USUARIOID_DOC + ", @" + LocalDatabase.CAMPO_FORMACOBROIDABONO_DOC + ", @" + LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_DOC + ", " +
                    "@" + LocalDatabase.CAMPO_CANCELADO_DOC + ", @" + LocalDatabase.CAMPO_ENVIADOALWS_DOC + ", @" + LocalDatabase.CAMPO_IDWEBSERVICE_DOC + ", " +
                    "@" + LocalDatabase.CAMPO_ARCHIVADO_DOC + ", @" + LocalDatabase.CAMPO_PAUSAR_DOC + ", @" + 
                    LocalDatabase.CAMPO_PAPELERARECICLAJE_DOC + ")";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    if (verifyIfADocumentExists(idDocument))
                        idDocument = getLastId() + 1;
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ID_DOC, idDocument);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CLAVECLIENTE_DOC, dvm.clave_cliente);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CLIENTEID_DOC, dvm.cliente_id);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_DESCUENTO_DOC, dvm.descuento);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_TOTAL_DOC, dvm.total);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_NOMBREU_DOC, dvm.nombreu);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ALMACENID_DOC, dvm.almacen_id);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ANTICIPO_DOC, dvm.anticipo);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC, dvm.tipo_documento);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FORMACOBROID_DOC, dvm.forma_cobro_id);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FACTURA_DOC, dvm.factura);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_OBSERVACION_DOC, dvm.observacion);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_DEV_DOC, dvm.dev);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FVENTA_DOC, dvm.fventa);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FECHAHORAMOV_DOC, dvm.fechahoramov);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_USUARIOID_DOC, dvm.usuario_id);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FORMACOBROIDABONO_DOC, dvm.forma_corbo_id_abono);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_DOC, dvm.ciddoctopedidocc);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CANCELADO_DOC, dvm.estado);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ENVIADOALWS_DOC, dvm.enviadoAlWs);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_IDWEBSERVICE_DOC, dvm.idWebService);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ARCHIVADO_DOC, dvm.documentoArchivado);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_PAUSAR_DOC, dvm.pausado);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_PAPELERARECICLAJE_DOC, dvm.papeleraReciclaje);
                    int idUltimoDocumentoCreado = command.ExecuteNonQuery();
                    if (idUltimoDocumentoCreado > 0)
                    {
                        idDocumentoCreated = idDocument;
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
            return idDocumentoCreated;
        }

        public static int addNewDocument(DocumentModel dvm)
        {
            int idDocumentoCreated = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "INSERT INTO " + LocalDatabase.TABLA_DOCUMENTOVENTA + " VALUES(@" + LocalDatabase.CAMPO_ID_DOC + ", @" + LocalDatabase.CAMPO_CLAVECLIENTE_DOC + ", " +
                    "@" + LocalDatabase.CAMPO_CLIENTEID_DOC + ", @" + LocalDatabase.CAMPO_DESCUENTO_DOC + ", @" + LocalDatabase.CAMPO_TOTAL_DOC +
                    ", @" + LocalDatabase.CAMPO_NOMBREU_DOC + ", @" + LocalDatabase.CAMPO_ALMACENID_DOC + ", " + " @" + LocalDatabase.CAMPO_ANTICIPO_DOC + ", " +
                    "@" + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + ", @" + LocalDatabase.CAMPO_FORMACOBROID_DOC + ", @" + LocalDatabase.CAMPO_FACTURA_DOC + ", " +
                    "@" + LocalDatabase.CAMPO_OBSERVACION_DOC + ", @" + LocalDatabase.CAMPO_DEV_DOC + ", @" + LocalDatabase.CAMPO_FVENTA_DOC + ", " +
                    "@" + LocalDatabase.CAMPO_FECHAHORAMOV_DOC + ", " +
                    "@" + LocalDatabase.CAMPO_USUARIOID_DOC + ", @" + LocalDatabase.CAMPO_FORMACOBROIDABONO_DOC + ", @" + LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_DOC + ", " +
                    "@" + LocalDatabase.CAMPO_CANCELADO_DOC + ", @" + LocalDatabase.CAMPO_ENVIADOALWS_DOC + ", @" + LocalDatabase.CAMPO_IDWEBSERVICE_DOC + ", " +
                    "@" + LocalDatabase.CAMPO_ARCHIVADO_DOC + ", @" + LocalDatabase.CAMPO_PAUSAR_DOC + ", @" +
                    LocalDatabase.CAMPO_PAPELERARECICLAJE_DOC + ")";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int lastId = getLastId() + 1;
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ID_DOC, lastId);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CLAVECLIENTE_DOC, dvm.clave_cliente);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CLIENTEID_DOC, dvm.cliente_id);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_DESCUENTO_DOC, dvm.descuento);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_TOTAL_DOC, dvm.total);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_NOMBREU_DOC, dvm.nombreu);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ALMACENID_DOC, dvm.almacen_id);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ANTICIPO_DOC, dvm.anticipo);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC, dvm.tipo_documento);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FORMACOBROID_DOC, dvm.forma_cobro_id);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FACTURA_DOC, dvm.factura);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_OBSERVACION_DOC, dvm.observacion);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_DEV_DOC, dvm.dev);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FVENTA_DOC, dvm.fventa);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FECHAHORAMOV_DOC, dvm.fechahoramov);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_USUARIOID_DOC, dvm.usuario_id);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FORMACOBROIDABONO_DOC, dvm.forma_corbo_id_abono);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_DOC, dvm.ciddoctopedidocc);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CANCELADO_DOC, dvm.estado);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ENVIADOALWS_DOC, dvm.enviadoAlWs);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_IDWEBSERVICE_DOC, dvm.idWebService);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ARCHIVADO_DOC, dvm.documentoArchivado);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_PAUSAR_DOC, dvm.pausado);
                    command.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_PAPELERARECICLAJE_DOC, dvm.papeleraReciclaje);
                    int idUltimoDocumentoCreado = command.ExecuteNonQuery();
                    if (idUltimoDocumentoCreado > 0)
                        idDocumentoCreated = lastId;
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
            return idDocumentoCreated;
        }

        public static int getLastId()
        {
            int lastId = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT MAX(" + LocalDatabase.CAMPO_ID_DOC + ") FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " ORDER BY " +
                    LocalDatabase.CAMPO_ID_DOC + " DESC LIMIT 1";
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
            catch (SQLiteException Ex)
            {
                SECUDOC.writeLog(Ex.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return lastId;
        }

        public static double getAllTotalForAFormaDePagoAbono(int idFormaPago)
        {
            double suma = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT sum(" + LocalDatabase.CAMPO_ANTICIPO_DOC + ") FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                        LocalDatabase.CAMPO_FORMACOBROID_DOC + " = " + 71 + " AND " + LocalDatabase.CAMPO_FORMACOBROIDABONO_DOC + " = " + idFormaPago +
                        " AND " + LocalDatabase.CAMPO_CANCELADO_DOC + " = " + 0 + " AND " + LocalDatabase.CAMPO_PAUSAR_DOC + " = " + 0;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    suma = Convert.ToDouble(reader.GetValue(0).ToString().Trim());
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
            return suma;
        }

        public static int getAllDocumentsCanceledOrNotSended()
        {
            int documents = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE (" +
                    LocalDatabase.CAMPO_CANCELADO_DOC + " = @canceled AND " + LocalDatabase.CAMPO_ENVIADOALWS_DOC + " = @sended) OR (" +
                    LocalDatabase.CAMPO_PAUSAR_DOC + " = @paused AND " + LocalDatabase.CAMPO_CANCELADO_DOC + " = @canceled)";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@canceled", 0);
                    command.Parameters.AddWithValue("@sended", 0);
                    command.Parameters.AddWithValue("@paused", 1);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                documents = reader.GetInt32(0);
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                SECUDOC.writeLog(Ex.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return documents;
        }

        public static int getDocumentType(int id)
        {
            int type = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA +
                        " WHERE " + LocalDatabase.CAMPO_ID_DOC + " = " + id;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader[LocalDatabase.CAMPO_TIPODOCUMENTO_DOC] != DBNull.Value)
                                    type = Convert.ToInt32(reader[LocalDatabase.CAMPO_TIPODOCUMENTO_DOC].ToString().Trim());
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
            return type;
        }

        public static async Task<String> getDocumentTypeName(int type, bool permissionPrepedido)
        {
            String typeName = "";
            await Task.Run(async () =>
            {
                if (type == 1)
                    typeName = "Cotización";
                else if (type == 2)
                {
                    if (permissionPrepedido)
                        typeName = "Entrega";
                    else typeName = "Venta";
                }
                else if (type == 3)
                    typeName = "Pedido";
                else if (type == 4)
                {
                    if (permissionPrepedido)
                        typeName = "Entrega";
                    else typeName = "Venta";
                }
                else if (type == 5)
                    typeName = "Devolución";
                else if (type == 50)
                    typeName = "Prepedido";
                else if (type == 51)
                    typeName = "Cotización de Mostrador";
            });
            return typeName;
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
                                if (reader.GetValue(0) != DBNull.Value)
                                    value = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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


        public static int getIntValueWithParametersToSearch(String query, String parameterName1, String parameterValue1, String parameterName2, 
            String parameterValue2)
        {
            int value = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@"+parameterName1, "%"+parameterValue1+"%");
                    command.Parameters.AddWithValue("@"+parameterName2, "%"+parameterValue2+"%");
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                                if (reader.GetValue(0) != DBNull.Value)
                                    value = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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

        public static int getIntValueWithParametersDates(String query, String parameterName1, String parameterValue1, String parameterName2,
            String parameterValue2)
        {
            int value = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@" + parameterName1, parameterValue1);
                    command.Parameters.AddWithValue("@" + parameterName2, parameterValue2);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                                if (reader.GetValue(0) != DBNull.Value)
                                    value = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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


        public static List<int> getListIntValue(String query)
        {
            List<int> valuesList = null;
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
                            valuesList = new List<int>();
                            while (reader.Read())
                                valuesList.Add(Convert.ToInt32(reader.GetValue(0).ToString().Trim()));
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
            return valuesList;
        }

        public static List<dynamic> getReporteDeDocumentos(String query)
        {
            List<dynamic> listaResDocument = null;
            dynamic docVenta;
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
                            listaResDocument = new List<dynamic>();
                            while (reader.Read())
                            {
                                docVenta = new ExpandoObject();
                                docVenta.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_DOC].ToString().Trim());
                                docVenta.clave_cliente = reader[LocalDatabase.CAMPO_CLAVECLIENTE_DOC].ToString().Trim();
                                docVenta.cliente_id = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLIENTEID_DOC].ToString().Trim());
                                docVenta.descuento = Convert.ToDouble(reader[LocalDatabase.CAMPO_DESCUENTO_DOC].ToString().Trim());
                                docVenta.total = Convert.ToDouble(reader[LocalDatabase.CAMPO_TOTAL_DOC].ToString().Trim());
                                docVenta.nombreu = reader[LocalDatabase.CAMPO_NOMBREU_DOC].ToString().Trim();
                                docVenta.almacen_id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ALMACENID_DOC].ToString().Trim());
                                docVenta.anticipo = Convert.ToDouble(reader[LocalDatabase.CAMPO_ANTICIPO_DOC].ToString().Trim());
                                docVenta.tipo_documento = Convert.ToInt32(reader[LocalDatabase.CAMPO_TIPODOCUMENTO_DOC].ToString().Trim());
                                docVenta.forma_cobro_id = Convert.ToInt32(reader[LocalDatabase.CAMPO_FORMACOBROID_DOC].ToString().Trim());
                                docVenta.factura = Convert.ToInt32(reader[LocalDatabase.CAMPO_FACTURA_DOC].ToString().Trim());
                                docVenta.nombreformacobro = reader[LocalDatabase.CAMPO_NOMBRE_FORMASCOBRO].ToString().Trim();
                                if (reader[LocalDatabase.CAMPO_OBSERVACION_DOC] != DBNull.Value)
                                    docVenta.observacion = reader[LocalDatabase.CAMPO_OBSERVACION_DOC].ToString().Trim();
                                else docVenta.observacion = "";
                                docVenta.fventa = reader[LocalDatabase.CAMPO_FVENTA_DOC].ToString().Trim();
                                docVenta.fechahoramov = reader[LocalDatabase.CAMPO_FECHAHORAMOV_DOC].ToString().Trim();
                                docVenta.forma_corbo_id_abono = Convert.ToInt32(reader[LocalDatabase.CAMPO_FORMACOBROIDABONO_DOC].ToString().Trim());
                                docVenta.ciddoctopedidocc = Convert.ToInt32(reader[LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_DOC].ToString().Trim());
                                docVenta.estado = Convert.ToInt32(reader[LocalDatabase.CAMPO_CANCELADO_DOC].ToString().Trim());
                                docVenta.enviadoAlWs = Convert.ToInt32(reader[LocalDatabase.CAMPO_ENVIADOALWS_DOC].ToString().Trim());
                                docVenta.idWebService = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDWEBSERVICE_DOC].ToString().Trim());
                                docVenta.documentoArchivado = Convert.ToInt32(reader[LocalDatabase.CAMPO_ARCHIVADO_DOC].ToString().Trim());
                                docVenta.pausado = Convert.ToInt32(reader[LocalDatabase.CAMPO_PAUSAR_DOC].ToString().Trim());
                                docVenta.papeleraReciclaje = Convert.ToInt32(reader[LocalDatabase.CAMPO_PAPELERARECICLAJE_DOC].ToString().Trim());
                                docVenta.TotalMovimientos = 0;
                                //comentar esto
                                List<dynamic> movimientos = new List<dynamic>();
                                int TotalMov = 0;
                                try
                                {
                                    String query2 = "SELECT * from Movimientos inner join Item on " +
                                        "Item.code = Movimientos.CLAVE_ART where DOCTO_ID_PEDIDO =" + docVenta.id;
                                    
                                    using (SQLiteCommand command2 = new SQLiteCommand(query2, db))
                                    {
                                        using (SQLiteDataReader reader2 = command2.ExecuteReader())
                                        {
                                            if (reader2.HasRows)
                                            {

                                                while (reader2.Read())
                                                {
                                                    dynamic movimiento = new ExpandoObject();
                                                    TotalMov += 1;
                                                    movimiento.DOCTO_ID_PEDIDO = Convert.ToInt32(reader2["DOCTO_ID_PEDIDO"].ToString().Trim());
                                                    movimiento.CLAVE_ART = reader2["CLAVE_ART"].ToString().Trim();
                                                    movimiento.unidad_base = Convert.ToInt32(reader2["unidad_base"].ToString().Trim());
                                                    movimiento.unidad_no_convertible = Convert.ToInt32(reader2["unidad_no_convertible"].ToString().Trim());
                                                    movimiento.unidades_capturadas = Convert.ToInt32(reader2["unidades_capturadas"].ToString().Trim());
                                                    movimiento.captured_unit_type = Convert.ToInt32(reader2["captured_unit_type"].ToString().Trim());
                                                    movimiento.precio = float.Parse(reader2["precio"].ToString().Trim());
                                                    movimiento.MONTO = float.Parse(reader2["MONTO"].ToString().Trim());
                                                    movimiento.TOTAL = float.Parse(reader2["TOTAL"].ToString().Trim());
                                                    movimiento.TIPO_DOCUMENTO = Convert.ToInt32(reader2["TIPO_DOCUMENTO"].ToString().Trim());
                                                    movimiento.FACTURA = reader2["FACTURA"].ToString().Trim();
                                                    movimiento.DESCUENTOPOR = Convert.ToInt32(reader2["DESCUENTOPOR"].ToString().Trim());
                                                    movimiento.DESCUENTO = float.Parse(reader2["DESCUENTO"].ToString().Trim());
                                                    movimiento.OBSERVACIONES =reader2["OBSERVACIONES"].ToString().Trim();
                                                    movimiento.COMENTARIO = reader2["COMENTARIO"].ToString().Trim();
                                                    movimiento.enviado_al_ws = Convert.ToInt32(reader2["enviado_al_ws"].ToString().Trim());
                                                    movimiento.rate_discount_promo = float.Parse(reader2["rate_discount_promo"].ToString().Trim());
                                                    movimiento.POSICION = Convert.ToInt32(reader2["POSICION"].ToString().Trim());
                                                    movimiento.name = reader2["name"].ToString().Trim();
                                                    movimiento.clasification_1 = Convert.ToInt32(reader2["clasification_1"].ToString().Trim());
                                                    movimiento.clasification_2 = Convert.ToInt32(reader2["clasification_2"].ToString().Trim());
                                                    movimiento.clasification_3 = Convert.ToInt32(reader2["clasification_3"].ToString().Trim());
                                                    movimiento.clasification_4 = Convert.ToInt32(reader2["clasification_4"].ToString().Trim());
                                                    movimiento.clasification_5 = Convert.ToInt32(reader2["clasification_5"].ToString().Trim());
                                                    movimiento.clasification_6 = Convert.ToInt32(reader2["clasification_6"].ToString().Trim());
                                                    movimiento.stock = float.Parse(reader2["stock"].ToString().Trim());
                                                    movimiento.ordenar = float.Parse(reader2["ordenar"].ToString().Trim());
                                                    movimiento.precio_1 = float.Parse(reader2["precio_1"].ToString().Trim());
                                                    movimiento.precio_2 = float.Parse(reader2["precio_2"].ToString().Trim());
                                                    movimiento.precio_3 = float.Parse(reader2["precio_3"].ToString().Trim());
                                                    movimiento.precio_4 = float.Parse(reader2["precio_4"].ToString().Trim());
                                                    movimiento.precio_5 = float.Parse(reader2["precio_5"].ToString().Trim());
                                                    movimiento.precio_6 = float.Parse(reader2["precio_6"].ToString().Trim());
                                                    movimiento.precio_7 = float.Parse(reader2["precio_7"].ToString().Trim());
                                                    movimiento.precio_8 = float.Parse(reader2["precio_8"].ToString().Trim());
                                                    movimiento.precio_9 = float.Parse(reader2["precio_9"].ToString().Trim());
                                                    movimiento.precio_10 = float.Parse(reader2["precio_10"].ToString().Trim());
                                                    movimiento.fiscal_product = Convert.ToInt32(reader2["fiscal_product"].ToString().Trim());
                                                    movimientos.Add(movimiento);
                                                }
                                            }
                                            else
                                            {
                                                TotalMov = 0;
                                            }
                                            if (reader2 != null && !reader2.IsClosed)
                                                reader2.Close();
                                        }
                                    }
                                }
                                catch(Exception e)
                                {
                                    docVenta.Totalmovimientos = 0;
                                    SECUDOC.writeLog(e.ToString());
                                }
                                //movimientos  -  formas de cobro
                                List<dynamic> formasCobro = new List<dynamic>();
                                int TotalFC = 0;
                                try
                                {
                                    String query2 = "SELECT * from FormaCobroDocumento where documento_id =" + docVenta.id;

                                    using (SQLiteCommand command2 = new SQLiteCommand(query2, db))
                                    {
                                        using (SQLiteDataReader reader2 = command2.ExecuteReader())
                                        {
                                            if (reader2.HasRows)
                                            {

                                                while (reader2.Read())
                                                {
                                                    dynamic formasC = new ExpandoObject();
                                                    TotalFC += 1;
                                                    formasC.forma_cobro_id_abono = Convert.ToInt32(reader2["forma_cobro_id_abono"].ToString().Trim());
                                                    formasC.importe = float.Parse(reader2["importe"].ToString().Trim());
                                                    formasC.total_doc = float.Parse(reader2["total_doc"].ToString().Trim());
                                                    formasC.cambio = float.Parse(reader2["cambio"].ToString().Trim());
                                                    formasC.saldo_doc = float.Parse(reader2["saldo_doc"].ToString().Trim());
                                                    formasC.documento_id = Convert.ToInt32(reader2["documento_id"].ToString().Trim());
                                                    formasC.id_server = Convert.ToInt32(reader2["id_server"].ToString().Trim());
                                                    formasCobro.Add(formasC);
                                                }
                                            }
                                            else
                                            {
                                                TotalFC = 0;
                                            }
                                            if (reader2 != null && !reader2.IsClosed)
                                                reader2.Close();
                                        }
                                    }
                                }
                                catch (Exception e)
                                {
                                    docVenta.Totalmovimientos = 0;
                                    SECUDOC.writeLog(e.ToString());
                                }
                                docVenta.formasCobro = formasCobro;
                                docVenta.totalFormasC = TotalFC;
                                docVenta.movimientos = movimientos;
                                docVenta.totalMovimientos = TotalMov;
                                //comentar esto 
                                /*try
                                {
                                    String query2 = "SELECT count(*) totalMovimientos from Movimientos where DOCTO_ID_PEDIDO ="+ docVenta.id;
                                    using (SQLiteCommand command2 = new SQLiteCommand(query2, db))
                                    {
                                        using (SQLiteDataReader reader2 = command2.ExecuteReader())
                                        {
                                            if (reader2.HasRows)
                                            {
                                                while (reader2.Read())
                                                {
                                                    docVenta.totalMovimientos = Convert.ToInt32(reader2["totalMovimientos"].ToString().Trim()); 
                                                }
                                            }
                                            else
                                            {
                                                docVenta.Totalmovimientos = 0;
                                            }
                                            if (reader2 != null && !reader2.IsClosed)
                                                reader2.Close();
                                        }
                                    }
                                }catch(Exception error)
                                {
                                    docVenta.Totalmovimientos = 0;
                                    SECUDOC.writeLog(error.ToString());
                                }*/
                                listaResDocument.Add(docVenta);
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
            return listaResDocument;
        }

        public static int getCiddoctopedidoFromADocument(int idDocument)
        {
            int idDocumentoPedido = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA +
                    " WHERE " +LocalDatabase.CAMPO_ID_DOC + " = " + idDocument;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    idDocumentoPedido = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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
            return idDocumentoPedido;
        }

        public static int eliminarUnDocumento(int idDoc)
        {
            int eliminado = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "DELETE FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " + LocalDatabase.CAMPO_ID_DOC + "=" + idDoc;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        eliminado = 1;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return eliminado;
        }

        public static String getFolioVentaForADocument(int id)
        {
            String fventa = "";
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_FVENTA_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                        LocalDatabase.CAMPO_ID_DOC + " = " + id;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                fventa = reader[LocalDatabase.CAMPO_FVENTA_DOC].ToString().Trim();
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return fventa;
        }

        public static String getFechaHora(int idDocumento)
        {
            String fechaHora = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_FECHAHORAMOV_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                        LocalDatabase.CAMPO_ID_DOC + " = @idDocumento";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idDocumento", idDocumento);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    fechaHora = reader.GetValue(0).ToString().Trim();
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
            return fechaHora;
        }

        public static List<DocumentModel> getAllDocuments(String query)
        {
            List<DocumentModel> listaResDocument = null;
            DocumentModel docVenta;
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
                            listaResDocument = new List<DocumentModel>();
                            while (reader.Read())
                            {
                                docVenta = new DocumentModel();
                                docVenta.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_DOC].ToString().Trim());
                                docVenta.clave_cliente = reader[LocalDatabase.CAMPO_CLAVECLIENTE_DOC].ToString().Trim();
                                docVenta.cliente_id = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLIENTEID_DOC].ToString().Trim());
                                docVenta.descuento = Convert.ToDouble(reader[LocalDatabase.CAMPO_DESCUENTO_DOC].ToString().Trim());
                                docVenta.total = Convert.ToDouble(reader[LocalDatabase.CAMPO_TOTAL_DOC].ToString().Trim());
                                docVenta.nombreu = reader[LocalDatabase.CAMPO_NOMBREU_DOC].ToString().Trim();
                                docVenta.almacen_id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ALMACENID_DOC].ToString().Trim());
                                docVenta.anticipo = Convert.ToDouble(reader[LocalDatabase.CAMPO_ANTICIPO_DOC].ToString().Trim());
                                docVenta.tipo_documento = Convert.ToInt32(reader[LocalDatabase.CAMPO_TIPODOCUMENTO_DOC].ToString().Trim());
                                docVenta.forma_cobro_id = Convert.ToInt32(reader[LocalDatabase.CAMPO_FORMACOBROID_DOC].ToString().Trim());
                                docVenta.factura = Convert.ToInt32(reader[LocalDatabase.CAMPO_FACTURA_DOC].ToString().Trim());
                                if (reader[LocalDatabase.CAMPO_OBSERVACION_DOC] != DBNull.Value)
                                    docVenta.observacion = reader[LocalDatabase.CAMPO_OBSERVACION_DOC].ToString().Trim();
                                else docVenta.observacion = "";
                                docVenta.fventa = reader[LocalDatabase.CAMPO_FVENTA_DOC].ToString().Trim();
                                docVenta.fechahoramov = reader[LocalDatabase.CAMPO_FECHAHORAMOV_DOC].ToString().Trim();
                                docVenta.forma_corbo_id_abono = Convert.ToInt32(reader[LocalDatabase.CAMPO_FORMACOBROIDABONO_DOC].ToString().Trim());
                                docVenta.ciddoctopedidocc = Convert.ToInt32(reader[LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_DOC].ToString().Trim());
                                docVenta.estado = Convert.ToInt32(reader[LocalDatabase.CAMPO_CANCELADO_DOC].ToString().Trim());
                                docVenta.enviadoAlWs = Convert.ToInt32(reader[LocalDatabase.CAMPO_ENVIADOALWS_DOC].ToString().Trim());
                                docVenta.idWebService = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDWEBSERVICE_DOC].ToString().Trim());
                                docVenta.documentoArchivado = Convert.ToInt32(reader[LocalDatabase.CAMPO_ARCHIVADO_DOC].ToString().Trim());
                                docVenta.pausado = Convert.ToInt32(reader[LocalDatabase.CAMPO_PAUSAR_DOC].ToString().Trim());
                                docVenta.papeleraReciclaje = Convert.ToInt32(reader[LocalDatabase.CAMPO_PAPELERARECICLAJE_DOC].ToString().Trim());
                                listaResDocument.Add(docVenta);
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            } catch (SQLiteException e) {
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return listaResDocument;
        }

        public static List<DocumentModel> getAllDocumentsWithParamtersToSearch(String query, String parameterName1, String parameterValue1,
            String parameterName2, String parameterValue2)
        {
            List<DocumentModel> listaResDocument = null;
            DocumentModel docVenta;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@"+parameterName1, "%"+parameterValue1+"%");
                    command.Parameters.AddWithValue("@"+parameterName2, "%"+parameterValue2+"%");
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            listaResDocument = new List<DocumentModel>();
                            while (reader.Read())
                            {
                                docVenta = new DocumentModel();
                                docVenta.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_DOC].ToString().Trim());
                                docVenta.clave_cliente = reader[LocalDatabase.CAMPO_CLAVECLIENTE_DOC].ToString().Trim();
                                docVenta.cliente_id = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLIENTEID_DOC].ToString().Trim());
                                docVenta.descuento = Convert.ToDouble(reader[LocalDatabase.CAMPO_DESCUENTO_DOC].ToString().Trim());
                                docVenta.total = Convert.ToDouble(reader[LocalDatabase.CAMPO_TOTAL_DOC].ToString().Trim());
                                docVenta.nombreu = reader[LocalDatabase.CAMPO_NOMBREU_DOC].ToString().Trim();
                                docVenta.almacen_id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ALMACENID_DOC].ToString().Trim());
                                docVenta.anticipo = Convert.ToDouble(reader[LocalDatabase.CAMPO_ANTICIPO_DOC].ToString().Trim());
                                docVenta.tipo_documento = Convert.ToInt32(reader[LocalDatabase.CAMPO_TIPODOCUMENTO_DOC].ToString().Trim());
                                docVenta.forma_cobro_id = Convert.ToInt32(reader[LocalDatabase.CAMPO_FORMACOBROID_DOC].ToString().Trim());
                                docVenta.factura = Convert.ToInt32(reader[LocalDatabase.CAMPO_FACTURA_DOC].ToString().Trim());
                                if (reader[LocalDatabase.CAMPO_OBSERVACION_DOC] != DBNull.Value)
                                    docVenta.observacion = reader[LocalDatabase.CAMPO_OBSERVACION_DOC].ToString().Trim();
                                else docVenta.observacion = "";
                                docVenta.fventa = reader[LocalDatabase.CAMPO_FVENTA_DOC].ToString().Trim();
                                docVenta.fechahoramov = reader[LocalDatabase.CAMPO_FECHAHORAMOV_DOC].ToString().Trim();
                                docVenta.forma_corbo_id_abono = Convert.ToInt32(reader[LocalDatabase.CAMPO_FORMACOBROIDABONO_DOC].ToString().Trim());
                                docVenta.ciddoctopedidocc = Convert.ToInt32(reader[LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_DOC].ToString().Trim());
                                docVenta.estado = Convert.ToInt32(reader[LocalDatabase.CAMPO_CANCELADO_DOC].ToString().Trim());
                                docVenta.enviadoAlWs = Convert.ToInt32(reader[LocalDatabase.CAMPO_ENVIADOALWS_DOC].ToString().Trim());
                                docVenta.idWebService = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDWEBSERVICE_DOC].ToString().Trim());
                                docVenta.documentoArchivado = Convert.ToInt32(reader[LocalDatabase.CAMPO_ARCHIVADO_DOC].ToString().Trim());
                                docVenta.pausado = Convert.ToInt32(reader[LocalDatabase.CAMPO_PAUSAR_DOC].ToString().Trim());
                                docVenta.papeleraReciclaje = Convert.ToInt32(reader[LocalDatabase.CAMPO_PAPELERARECICLAJE_DOC].ToString().Trim());
                                listaResDocument.Add(docVenta);
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
            return listaResDocument;
        }

        public static List<DocumentModel> getAllDocumentsWithParamtersDates(String query, String parameterName1, String parameterValue1,
            String parameterName2, String parameterValue2)
        {
            List<DocumentModel> listaResDocument = null;
            DocumentModel docVenta;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@" + parameterName1, parameterValue1);
                    command.Parameters.AddWithValue("@" + parameterName2, parameterValue2);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            listaResDocument = new List<DocumentModel>();
                            while (reader.Read())
                            {
                                docVenta = new DocumentModel();
                                docVenta.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_DOC].ToString().Trim());
                                docVenta.clave_cliente = reader[LocalDatabase.CAMPO_CLAVECLIENTE_DOC].ToString().Trim();
                                docVenta.cliente_id = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLIENTEID_DOC].ToString().Trim());
                                docVenta.descuento = Convert.ToDouble(reader[LocalDatabase.CAMPO_DESCUENTO_DOC].ToString().Trim());
                                docVenta.total = Convert.ToDouble(reader[LocalDatabase.CAMPO_TOTAL_DOC].ToString().Trim());
                                docVenta.nombreu = reader[LocalDatabase.CAMPO_NOMBREU_DOC].ToString().Trim();
                                docVenta.almacen_id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ALMACENID_DOC].ToString().Trim());
                                docVenta.anticipo = Convert.ToDouble(reader[LocalDatabase.CAMPO_ANTICIPO_DOC].ToString().Trim());
                                docVenta.tipo_documento = Convert.ToInt32(reader[LocalDatabase.CAMPO_TIPODOCUMENTO_DOC].ToString().Trim());
                                docVenta.forma_cobro_id = Convert.ToInt32(reader[LocalDatabase.CAMPO_FORMACOBROID_DOC].ToString().Trim());
                                docVenta.factura = Convert.ToInt32(reader[LocalDatabase.CAMPO_FACTURA_DOC].ToString().Trim());
                                if (reader[LocalDatabase.CAMPO_OBSERVACION_DOC] != DBNull.Value)
                                    docVenta.observacion = reader[LocalDatabase.CAMPO_OBSERVACION_DOC].ToString().Trim();
                                else docVenta.observacion = "";
                                docVenta.fventa = reader[LocalDatabase.CAMPO_FVENTA_DOC].ToString().Trim();
                                docVenta.fechahoramov = reader[LocalDatabase.CAMPO_FECHAHORAMOV_DOC].ToString().Trim();
                                docVenta.forma_corbo_id_abono = Convert.ToInt32(reader[LocalDatabase.CAMPO_FORMACOBROIDABONO_DOC].ToString().Trim());
                                docVenta.ciddoctopedidocc = Convert.ToInt32(reader[LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_DOC].ToString().Trim());
                                docVenta.estado = Convert.ToInt32(reader[LocalDatabase.CAMPO_CANCELADO_DOC].ToString().Trim());
                                docVenta.enviadoAlWs = Convert.ToInt32(reader[LocalDatabase.CAMPO_ENVIADOALWS_DOC].ToString().Trim());
                                docVenta.idWebService = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDWEBSERVICE_DOC].ToString().Trim());
                                docVenta.documentoArchivado = Convert.ToInt32(reader[LocalDatabase.CAMPO_ARCHIVADO_DOC].ToString().Trim());
                                docVenta.pausado = Convert.ToInt32(reader[LocalDatabase.CAMPO_PAUSAR_DOC].ToString().Trim());
                                docVenta.papeleraReciclaje = Convert.ToInt32(reader[LocalDatabase.CAMPO_PAPELERARECICLAJE_DOC].ToString().Trim());
                                listaResDocument.Add(docVenta);
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
            return listaResDocument;
        }

        public static DataTable getAllDocumentsDt(String query)
        {
            DataTable dt = null;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                    adapter.SelectCommand = command;
                    dt = new DataTable();
                    adapter.Fill(dt);
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return dt;
        }

        public static int updateCreditFormCobroDocuments(int id, int FC, int typeDocumento)
        {
            int resp = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "UPDATE " + LocalDatabase.TABLA_DOCUMENTOVENTA + " SET " + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = @tipoDocumento, " +
                    LocalDatabase.CAMPO_FORMACOBROID_DOC + " = @FormaCobro, " + LocalDatabase.CAMPO_ANTICIPO_DOC + " = 0 "
                    + " WHERE " + LocalDatabase.CAMPO_ID_DOC + " = @id";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@FormaCobro", FC);
                    command.Parameters.AddWithValue("@tipoDocumento", typeDocumento);
                    command.Parameters.AddWithValue("@id", id);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        resp = records;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return resp;
        }

        public static Boolean updateInformationForAPausedDocuments(int id, double descuento, double total)
        {
            Boolean resp = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "UPDATE " + LocalDatabase.TABLA_DOCUMENTOVENTA + " SET " + LocalDatabase.CAMPO_DESCUENTO_DOC + " = @descuento, " +
                    LocalDatabase.CAMPO_TOTAL_DOC + " = @total, " + LocalDatabase.CAMPO_FECHAHORAMOV_DOC + " = @fechaHora WHERE " + LocalDatabase.CAMPO_ID_DOC + " = @id";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@descuento", descuento);
                    command.Parameters.AddWithValue("@total", total);
                    command.Parameters.AddWithValue("@fechaHora", MetodosGenerales.getCurrentDateAndHour());
                    command.Parameters.AddWithValue("@id", id);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        resp = true;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return resp;
        }

        public static bool verifyIfADocumentExists(int idDocument)
        {
            bool exists = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                        LocalDatabase.CAMPO_ID_DOC + " = " + idDocument;
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
                                        exists = true;
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
            return exists;
        }

        public static bool verifyIfADocumentIsCanceled(int idDocument)
        {
            bool exists = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            try
            {
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                        LocalDatabase.CAMPO_ID_DOC + " = " + idDocument + " AND " + LocalDatabase.CAMPO_CANCELADO_DOC + " = 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            exists = true;
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return exists;
        }

        public static Boolean updateToPauseTheDocument(int idDocument, int pausa)
        {
            Boolean resp = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_DOCUMENTOVENTA + " SET " + LocalDatabase.CAMPO_PAUSAR_DOC + " = @pausar " +
                    "WHERE " + LocalDatabase.CAMPO_ID_DOC +" = @idDocument";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@pausar", pausa);
                    command.Parameters.AddWithValue("@idDocument", idDocument);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        resp = true;
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
            return resp;
        }

        public static Boolean updateARecord(String query)
        {
            Boolean resp = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        resp = true;
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
            return resp;
        }

        public static Boolean updateCustomer(int idDocumento, int clienteId, String codigoCliente)
        {
            Boolean resp = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_DOCUMENTOVENTA + " SET " + 
                    LocalDatabase.CAMPO_CLAVECLIENTE_DOC + " = @codigo, " +
                            LocalDatabase.CAMPO_CLIENTEID_DOC + " = @clienteId WHERE " + LocalDatabase.CAMPO_ID_DOC + " = @idDocumento";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idDocumento", idDocumento);
                    command.Parameters.AddWithValue("@clienteId", clienteId);
                    command.Parameters.AddWithValue("@codigo", codigoCliente);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        resp = true;
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
            return resp;
        }

        public static ExpandoObject updateAgent(int idDocumento, int agenteId, String nombreAgente)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_DOCUMENTOVENTA + " SET " +
                    LocalDatabase.CAMPO_NOMBREU_DOC + " = @nombreAgente, " +
                            LocalDatabase.CAMPO_USUARIOID_DOC + " = @agenteId " +
                            "WHERE " + LocalDatabase.CAMPO_ID_DOC + " = @idDocumento";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idDocumento", idDocumento);
                    command.Parameters.AddWithValue("@agenteId", agenteId);
                    command.Parameters.AddWithValue("@nombreAgente", nombreAgente);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                    {
                        value = 1;
                    } else description = "El agente "+ nombreAgente + " no se pudo actualizar en el Documento con ID: "+idDocumento;
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
            }
            return response;
        }

        public static int cancelADocument(int idDoc)
        {
            int resp = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "UPDATE " + LocalDatabase.TABLA_DOCUMENTOVENTA + " SET " + LocalDatabase.CAMPO_CANCELADO_DOC + " = @cancelado, " + LocalDatabase.CAMPO_ENVIADOALWS_DOC +
                    " = @enviado WHERE " + LocalDatabase.CAMPO_ID_DOC + " = @idDocument";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@cancelado", 1);
                    command.Parameters.AddWithValue("@enviado", 1);
                    command.Parameters.AddWithValue("@idDocument", idDoc);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        resp = 1;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return resp;
        }

        public static async Task<bool> updateTypeFromADocument(int idDocument, int currentDocumentType, int newDocumentType, 
            bool serverModeLAN, bool webActive, String codigoCaja)
        {
            Boolean resp = false;
            int count = 0;
            int items = 0;
            bool continuar = false;
            await Task.Run(async () =>
            {
                List<MovimientosModel> movesList = MovimientosModel.getAllMovementsFromADocument(idDocument);
                if (movesList != null)
                {
                    items = movesList.Count;
                    for (int i = 0; i < items; i++)
                    {
                        double capturedUnits = movesList[i].capturedUnits;
                        ClsItemModel itemModel = null;
                        if (serverModeLAN)
                        {
                            dynamic responseItem = await ItemsController.getAnItemFromTheServerLAN(movesList[i].itemId, null, codigoCaja);
                            if (responseItem.value == 1)
                                itemModel = responseItem.item;
                        }
                        else
                        {
                            itemModel = ItemModel.getAllDataFromAnItem(movesList[i].itemId);
                        }
                        double stock = itemModel.existencia;
                        int capturedUnitId = movesList[i].capturedUnitId;
                        if (currentDocumentType == TIPO_VENTA || currentDocumentType == TIPO_REMISION)
                        {
                            if (newDocumentType == TIPO_COTIZACION || newDocumentType == TIPO_PEDIDO)
                            {
                                if (!serverModeLAN)
                                    stock = MovimientosModel.getNewStockBySubtractingOrAddingUnits(itemModel.baseUnitId, capturedUnitId, capturedUnits, stock, true, false);
                                //stock = stock + units;
                            }
                            else if (newDocumentType == TIPO_DEVOLUCION)
                            {
                                if (!serverModeLAN)
                                    stock = MovimientosModel.getNewStockBySubtractingOrAddingUnits(itemModel.baseUnitId, capturedUnitId, capturedUnits, stock, true, true);
                                //stock = stock + (units * 2);
                            } else
                            {
                                if (stock > 0)
                                {
                                    if (capturedUnits > stock)
                                    {
                                        /*if (serverModeLAN)
                                            await DatosTicketController.downloadAllDatosTicketLAN();
                                        else
                                        {
                                            if (webActive)
                                            {
                                                await DatosTicketController.downloadAllDatosTicketAPI();
                                            }
                                        }*/
                                        if (DatosTicketModel.sellOnlyWithStock())
                                            count--;
                                    }
                                } else
                                {
                                    /*if (serverModeLAN)
                                        await DatosTicketController.downloadAllDatosTicketLAN();
                                    else
                                    {
                                        if (webActive)
                                        {
                                            await DatosTicketController.downloadAllDatosTicketAPI();
                                        }
                                    }*/
                                    if (DatosTicketModel.sellOnlyWithStock())
                                        count--;
                                }
                            }
                        }
                        else if (currentDocumentType == TIPO_DEVOLUCION)
                        {
                            if (newDocumentType == TIPO_COTIZACION || newDocumentType == TIPO_PEDIDO)
                            {
                                if (!serverModeLAN)
                                    stock = MovimientosModel.getNewStockBySubtractingOrAddingUnits(itemModel.baseUnitId, capturedUnitId, capturedUnits, stock, false, false);
                                //stock = stock - units;
                            }
                            else if (newDocumentType == TIPO_VENTA || newDocumentType == TIPO_REMISION)
                            {
                                if (!serverModeLAN)
                                {
                                    double originalStock = MovimientosModel.getNewStockBySubtractingOrAddingUnits(itemModel.baseUnitId, capturedUnitId, capturedUnits, stock, false, false);//stock - units;
                                    if (originalStock > 0)
                                    {
                                        if (itemModel.baseUnitId != capturedUnitId)
                                        {
                                            int capturedUnitIsMajor = -1;
                                            if (serverModeLAN)
                                            {
                                                dynamic responsecapturedUnits = await ConversionsUnitsController.checkIfTheCapturedUnitIsHigherLAN(itemModel.baseUnitId, capturedUnitId);
                                                if (responsecapturedUnits.value == 1)
                                                    capturedUnitIsMajor = responsecapturedUnits.salesUnitIsHigher;
                                            } else capturedUnitIsMajor = ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(itemModel.baseUnitId, capturedUnitId);
                                            if (capturedUnitIsMajor == 0)
                                            {
                                                /** Unidad de venta es menor: multiplicamos la base por el numero de conversión mayor */
                                                double majorConversion = 0;
                                                if (serverModeLAN)
                                                {
                                                    dynamic responseMajor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.baseUnitId, capturedUnitId, true);
                                                    if (responseMajor.value == 1)
                                                        majorConversion = responseMajor.majorFactor;
                                                } else majorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, true);
                                                if (majorConversion != 0)
                                                    originalStock = stock * majorConversion;
                                                else originalStock = stock;
                                            }
                                            else if (capturedUnitIsMajor == 1)
                                            {
                                                /** Unidad de venta es mayor: multiplicamos la base por el numero de conversión mayor */
                                                double minorConversion = 0;
                                                if (serverModeLAN)
                                                {
                                                    dynamic responseMajor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.baseUnitId, capturedUnitId, false);
                                                    if (responseMajor.value == 1)
                                                        minorConversion = responseMajor.majorFactor;
                                                } else minorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, false);
                                                if (minorConversion != 0)
                                                    originalStock = stock / minorConversion;
                                                else originalStock = stock;
                                            }
                                        }
                                        if (originalStock >= capturedUnits)
                                        {
                                            stock = MovimientosModel.getNewStockBySubtractingOrAddingUnits(itemModel.baseUnitId,
                                                    capturedUnitId, capturedUnits, stock, false, true);
                                            //stock = stock - (units * 2);
                                        }
                                        else
                                        {
                                            if (itemModel.baseUnitId != capturedUnitId)
                                            {
                                                int capturedUnitIsMajor = -1;
                                                if (serverModeLAN)
                                                {
                                                    dynamic responsecapturedUnits = await ConversionsUnitsController.checkIfTheCapturedUnitIsHigherLAN(itemModel.baseUnitId, capturedUnitId);
                                                    if (responsecapturedUnits.value == 1)
                                                        capturedUnitIsMajor = responsecapturedUnits.salesUnitIsHigher;
                                                } else capturedUnitIsMajor = ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(itemModel.baseUnitId, capturedUnitId);
                                                if (capturedUnitIsMajor == 0)
                                                {
                                                    /** Unidad de venta es menor: multiplicamos la base por el numero de conversión mayor */
                                                    double majorConversion = 0;
                                                    if (serverModeLAN)
                                                    {
                                                        dynamic responseMajor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.baseUnitId, capturedUnitId, true);
                                                        if (responseMajor.value == 1)
                                                            majorConversion = responseMajor.majorFactor;
                                                    } else majorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, true);
                                                    if (majorConversion != 0)
                                                        originalStock = stock / majorConversion;
                                                    else originalStock = stock;
                                                }
                                                else if (capturedUnitIsMajor == 1)
                                                {
                                                    /** Unidad de venta es mayor: multiplicamos la base por el numero de conversión mayor */
                                                    double minorConversion = 0;
                                                    if (serverModeLAN)
                                                    {
                                                        dynamic responseMajor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.baseUnitId, capturedUnitId, false);
                                                        if (responseMajor.value == 1)
                                                            minorConversion = responseMajor.majorFactor;
                                                    } else minorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, false);
                                                    originalStock = stock * minorConversion;
                                                }
                                            }
                                            if (MovimientosModel.updateCapturedUnits(movesList[i].id, originalStock))
                                            {
                                                
                                                MovimientosModel.validateWhetherToApplyPromotionToAMovement(movesList[i].id, movesList[i].capturedUnits, itemModel, Math.Abs(movesList[i].rateDiscountPromo - movesList[i].descuentoPorcentaje));
                                                /*double newTotal = getTheRecalculatedTotal(context, movesList.get(i).getId(), originalStock);
                                                MovimientosDocumentoModel.changeTheTotalOfAMove(context, movesList.get(i).getArticulo_id(),
                                                        movesList.get(i).getPosicion(), newTotal);*/
                                                stock -= stock;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        /*if (serverModeLAN)
                                            await DatosTicketController.downloadAllDatosTicketLAN();
                                        else
                                        {
                                            if (webActive)
                                            {
                                                await DatosTicketController.downloadAllDatosTicketAPI();
                                            }
                                        }*/
                                        if (DatosTicketModel.sellOnlyWithStock())
                                            count--;
                                    }
                                }
                            }
                        }
                        else if (currentDocumentType == TIPO_COTIZACION || currentDocumentType == TIPO_PEDIDO)
                        {
                            if (newDocumentType == TIPO_VENTA || newDocumentType == TIPO_REMISION)
                            {
                                if (stock > 0)
                                {
                                    double conversionStock = stock;
                                    if (itemModel.baseUnitId != capturedUnitId)
                                    {
                                        int capturedUnitIsMajor = -1;
                                        if (serverModeLAN)
                                        {
                                            dynamic responsecapturedUnits = await ConversionsUnitsController.checkIfTheCapturedUnitIsHigherLAN(itemModel.baseUnitId, capturedUnitId);
                                            if (responsecapturedUnits.value == 1)
                                                capturedUnitIsMajor = responsecapturedUnits.salesUnitIsHigher;
                                        } else capturedUnitIsMajor = ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(itemModel.baseUnitId, capturedUnitId);
                                        if (capturedUnitIsMajor == 0)
                                        {
                                            /** Unidad de venta es menor: multiplicamos la base por el numero de conversión mayor */
                                            double majorConversion = 0;
                                            if (serverModeLAN)
                                            {
                                                dynamic responseMajor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.baseUnitId, capturedUnitId, true);
                                                if (responseMajor.value == 1)
                                                    majorConversion = responseMajor.majorFactor;
                                            } else majorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, true);
                                            if (majorConversion != 0)
                                                conversionStock = stock * majorConversion;
                                            else conversionStock = stock;
                                        }
                                        else if (capturedUnitIsMajor == 1)
                                        {
                                            /** Unidad de venta es mayor: multiplicamos la base por el numero de conversión mayor */
                                            double minorConversion = 0;
                                            if (serverModeLAN)
                                            {
                                                dynamic responseMajor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.baseUnitId, capturedUnitId, false);
                                                if (responseMajor.value == 1)
                                                    minorConversion = responseMajor.majorFactor;
                                            } else minorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, false);
                                            if (minorConversion != 0)
                                                conversionStock = stock / minorConversion;
                                            else conversionStock = stock;
                                        }
                                    }
                                    if (capturedUnits > conversionStock)
                                    {
                                        if (MovimientosModel.updateCapturedUnits(movesList[i].id, stock))
                                        {
                                            MovimientosModel.validateWhetherToApplyPromotionToAMovement(movesList[i].id, movesList[i].capturedUnits,
                                                itemModel, Math.Abs(movesList[i].rateDiscountPromo - movesList[i].descuentoPorcentaje));
                                            /*double newTotal = getTheRecalculatedTotal(context, movesList.get(i).getId(), stock);
                                            MovimientosDocumentoModel.changeTheTotalOfAMove(context, movesList.get(i).getArticulo_id(),
                                                    movesList.get(i).getPosicion(), newTotal);*/
                                            stock -= stock;
                                        }
                                    }
                                    else
                                    {
                                        if (!serverModeLAN)
                                            stock = MovimientosModel.getNewStockBySubtractingOrAddingUnits(itemModel.baseUnitId, capturedUnitId, capturedUnits, stock, false, false);
                                        //stock = stock - units;
                                    }
                                }
                                else
                                {
                                    /*if (serverModeLAN)
                                        await DatosTicketController.downloadAllDatosTicketLAN();
                                    else
                                    {
                                        if (webActive)
                                        {
                                            await DatosTicketController.downloadAllDatosTicketAPI();
                                        }
                                    }*/
                                    if (DatosTicketModel.sellOnlyWithStock())
                                        count--;
                                }
                            }
                            else if (newDocumentType == TIPO_DEVOLUCION)
                            {
                                if (!serverModeLAN)
                                    stock = MovimientosModel.getNewStockBySubtractingOrAddingUnits(itemModel.baseUnitId, capturedUnitId, capturedUnits, stock, true, false);
                                //stock = stock + units;
                            }
                        }
                        if (serverModeLAN)
                        {
                            count++;
                        }
                        else
                        {
                            if (ItemModel.changeTheExistenceOfAnItem(movesList[i].itemId, stock))
                                count++;
                        }
                    }
                    if (items == count)
                        continuar = true;
                }
                else
                {
                    continuar = true;
                }
                if (continuar)
                {
                    var db = new SQLiteConnection();
                    try
                    {
                        db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                        db.Open();
                        String query = "UPDATE " + LocalDatabase.TABLA_DOCUMENTOVENTA + " SET " + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = @documentType WHERE " +
                            LocalDatabase.CAMPO_ID_DOC + " = @idDocument";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@documentType", newDocumentType);
                            command.Parameters.AddWithValue("@idDocument", idDocument);
                            int records = command.ExecuteNonQuery();
                            if (records > 0)
                                resp = true;
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
            });
            return resp;
        }

        public static bool updateQuoteDocumentType(int idDocument, int documentType)
        {
            bool updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_DOCUMENTOVENTA + " SET " + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = @documentType " +
                    "WHERE " +LocalDatabase.CAMPO_ID_DOC + " = @idDocument";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@documentType", documentType);
                    command.Parameters.AddWithValue("@idDocument", idDocument);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        updated = true;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return updated;
        }

        public static bool updateGenerarFactura(int idDocument, int generarFactura)
        {
            bool updated = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "UPDATE " + LocalDatabase.TABLA_DOCUMENTOVENTA + " SET " + LocalDatabase.CAMPO_FACTURA_DOC + " = @generarFactura " +
                    "WHERE " + LocalDatabase.CAMPO_ID_DOC + " = @idDocument";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@generarFactura", generarFactura);
                    command.Parameters.AddWithValue("@idDocument", idDocument);
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

        public static bool seGeneraraFacturaSinFiscal(int idDocumento)
        {
            bool generate = false;
            var db = new SQLiteConnection();            
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT "+LocalDatabase.CAMPO_FACTURA_DOC+" FROM "+LocalDatabase.TABLA_DOCUMENTOVENTA+" WHERE "+
                    LocalDatabase.CAMPO_ID_DOC+ " = @idDocumento";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idDocumento", idDocumento);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                                if (reader.GetValue(0) != DBNull.Value)
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) == 1)
                                        generate = true;
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
            return generate;
        }

        public static bool seGeneraraFacturaConFiscal(int idDocumento)
        {
            bool generate = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_FACTURA_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                    LocalDatabase.CAMPO_ID_DOC + " = @idDocumento";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idDocumento", idDocumento);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                                if (reader.GetValue(0) != DBNull.Value)
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) == DocumentModel.FISCAL_FACTURAR)
                                        generate = true;
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
            return generate;
        }

        public static Boolean updatePaymentFormIdAndCreditId(int idDocument, int fcId, int fcIdAbono, double anticipo)
        {
            Boolean updated = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "UPDATE " + LocalDatabase.TABLA_DOCUMENTOVENTA + " SET " + LocalDatabase.CAMPO_FORMACOBROID_DOC + " = @fcId, " +
                    LocalDatabase.CAMPO_FORMACOBROIDABONO_DOC + " = @fcIdAbono, " + LocalDatabase.CAMPO_ANTICIPO_DOC + " = @anticipo WHERE " +
                    LocalDatabase.CAMPO_ID_DOC + " = @idDocument";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@fcId", fcId);
                    command.Parameters.AddWithValue("@fcIdAbono", fcIdAbono);
                    command.Parameters.AddWithValue("@anticipo", anticipo);
                    command.Parameters.AddWithValue("@idDocument", idDocument);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        updated = true;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return updated;
        }

        public static Boolean updateInformationToCobrarDocumentTpv(double descuento, double total,
                                                               int dev, int documentoId)
        {
            Boolean resp = false;
            if (verifyIfADocumentExists(documentoId))
            {
                
                var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
                db.Open();
                try
                {
                    String query = "UPDATE " + LocalDatabase.TABLA_DOCUMENTOVENTA + " SET " + LocalDatabase.CAMPO_DESCUENTO_DOC + " = @descuento, " +
                        LocalDatabase.CAMPO_TOTAL_DOC + " = @total, "+ LocalDatabase.CAMPO_DEV_DOC + " = @dev, " +
                        LocalDatabase.CAMPO_FECHAHORAMOV_DOC + " = @fechaHora WHERE " + LocalDatabase.CAMPO_ID_DOC + " = @idDocument"; ;
                    using (SQLiteCommand command = new SQLiteCommand(query, db))
                    {
                        command.Parameters.AddWithValue("@descuento", descuento);
                        command.Parameters.AddWithValue("@total", total);
                        command.Parameters.AddWithValue("@dev", dev);
                        command.Parameters.AddWithValue("@fechaHora", MetodosGenerales.getCurrentDateAndHour());
                        command.Parameters.AddWithValue("@idDocument", documentoId);
                        int records = command.ExecuteNonQuery();
                        if (records > 0)
                            resp = true;
                    }
                }
                catch (SQLiteException e)
                {
                    SECUDOC.writeLog(e.Message);
                }
                finally
                {
                    if (db != null && db.State == ConnectionState.Open)
                        db.Close();
                }
            }
            return resp;
        }

        public static double optenerImporteDocumento (int idDocument)
        {
            double isIt = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_ANTICIPO_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA +
                        " WHERE " + LocalDatabase.CAMPO_ID_DOC + " = " + idDocument;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                isIt = Convert.ToInt32(reader[LocalDatabase.CAMPO_FORMACOBROID_DOC].ToString().Trim());
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return isIt;
        }

        public static Boolean isItACreditSaleDocument(int idDocument)
        {
            Boolean isIt = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_FORMACOBROID_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA +
                        " WHERE " + LocalDatabase.CAMPO_ID_DOC + " = " + idDocument;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (Convert.ToInt32(reader[LocalDatabase.CAMPO_FORMACOBROID_DOC].ToString().Trim()) == 71)
                                    isIt = true;
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return isIt;
        }

        public static int getCustomerIdOfADocument(int idDocument)
        {
            int customerId = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_CLIENTEID_DOC + " FROM " +
                        LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " + LocalDatabase.CAMPO_ID_DOC + " = " + idDocument;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader[LocalDatabase.CAMPO_CLIENTEID_DOC] != DBNull.Value)
                                    customerId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLIENTEID_DOC].ToString().Trim());
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
            return customerId;
        }

        public static int getAgentIdOfADocument(int idDocument)
        {
            int agentId = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_USUARIOID_DOC + " FROM " +
                        LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " + LocalDatabase.CAMPO_ID_DOC + " = " + idDocument;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    agentId = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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
            return agentId;
        }

        public static double getTotalForADocumentWithContext(int documentoId)
        {
            double total = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_TOTAL_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                        LocalDatabase.CAMPO_ID_DOC + "=" + documentoId;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                double lastTotal = Convert.ToDouble(reader[LocalDatabase.CAMPO_TOTAL_DOC].ToString().Trim());
                                total = Convert.ToDouble(MetodosGenerales.obtieneDosDecimales(lastTotal));
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return total;
        }

        public static Boolean verifyThatTheAdvanceIsLessThanTheTotal(int idDocument)
        {
            Boolean resp = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_DESCUENTO_DOC + ", " + LocalDatabase.CAMPO_TOTAL_DOC + ", " +
                        LocalDatabase.CAMPO_ANTICIPO_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                        LocalDatabase.CAMPO_ID_DOC + " = " + idDocument;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                double total = Convert.ToDouble(reader[LocalDatabase.CAMPO_TOTAL_DOC].ToString().Trim());
                                double newTotal = Convert.ToDouble(MetodosGenerales.obtieneDosDecimales(total));
                                double advance = Convert.ToDouble(reader[LocalDatabase.CAMPO_ANTICIPO_DOC].ToString().Trim());
                                if (advance < newTotal)
                                    resp = true;
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return resp;
        }

        public static Boolean changeDocumentFromCashToCreditOrViceVersa(int idDocument, int documentType,
                                                                    int fcId, int fcIdAbono)
        {
            Boolean changed = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "UPDATE " + LocalDatabase.TABLA_DOCUMENTOVENTA + " SET " + LocalDatabase.CAMPO_ANTICIPO_DOC + " = @anticipo, " +
                    LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = @documentType, " + LocalDatabase.CAMPO_FORMACOBROID_DOC + " = @fcId, " +
                    LocalDatabase.CAMPO_FORMACOBROIDABONO_DOC + " = @fcIdAbono WHERE " + LocalDatabase.CAMPO_ID_DOC + " = @idDocument";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@anticipo", 0);
                    if (documentType != 0)
                        command.Parameters.AddWithValue("@documentType", documentType);
                    command.Parameters.AddWithValue("@fcId", fcId);
                    command.Parameters.AddWithValue("@fcIdAbono", fcIdAbono);
                    command.Parameters.AddWithValue("@idDocument", idDocument);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        changed = true;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return changed;
        }

        public static Boolean updateDocumentAdvance(int idDocument, double advance)
        {
            Boolean updated = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "UPDATE " + LocalDatabase.TABLA_DOCUMENTOVENTA + " SET " + 
                    LocalDatabase.CAMPO_ANTICIPO_DOC + " = @advance WHERE " +
                    LocalDatabase.CAMPO_ID_DOC + " = @idDocument";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@advance", advance);
                    command.Parameters.AddWithValue("@idDocument", idDocument);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        updated = true;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return updated;
        }

        public static double getTotalForADocument(int documentoId)
        {
            double total = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_TOTAL_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                        LocalDatabase.CAMPO_ID_DOC + "=" + documentoId;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                double lastTotal = Convert.ToDouble(reader[LocalDatabase.CAMPO_TOTAL_DOC].ToString().Trim());
                                total = Convert.ToDouble(MetodosGenerales.obtieneDosDecimales(lastTotal));
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return total;
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
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return value;
        }

        public static Boolean updateTheDocumentObservation(int idDocument, String observation)
        {
            Boolean updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_DOCUMENTOVENTA + " SET " + LocalDatabase.CAMPO_OBSERVACION_DOC + " = @observation WHERE " +
                     LocalDatabase.CAMPO_ID_DOC + " = " + idDocument;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@observation", observation);
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

        public static bool updateCustomerId(int lastCustomerId, int newCustomerId)
        {
            bool updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_DOCUMENTOVENTA + " SET " + LocalDatabase.CAMPO_CLIENTEID_DOC + " = @newCustomerId WHERE " +
                     LocalDatabase.CAMPO_CLIENTEID_DOC + " = " + lastCustomerId;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@newCustomerId", newCustomerId);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        updated = true;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return updated;
        }

        public static DocumentModel getAllDataDocumentNotSent(int idDocto)
        {
            DocumentModel documentoVentaModel = null;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                        LocalDatabase.CAMPO_ID_DOC + " = " + idDocto;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                documentoVentaModel = new DocumentModel();
                                documentoVentaModel.id = reader.GetInt32(0);
                                if (reader.GetValue(0) != DBNull.Value)
                                    documentoVentaModel.clave_cliente = reader.GetValue(1).ToString().Trim();
                                else documentoVentaModel.clave_cliente = "";
                                documentoVentaModel.cliente_id = reader.GetInt32(2);
                                documentoVentaModel.descuento = reader.GetDouble(3);
                                documentoVentaModel.total = reader.GetDouble(4);
                                documentoVentaModel.nombreu = reader.GetString(5);
                                documentoVentaModel.almacen_id = reader.GetInt32(6);
                                documentoVentaModel.anticipo = reader.GetDouble(7);
                                documentoVentaModel.tipo_documento = Convert.ToInt32(reader[LocalDatabase.CAMPO_TIPODOCUMENTO_DOC].ToString().Trim());
                                documentoVentaModel.forma_cobro_id = reader.GetInt32(9);
                                documentoVentaModel.factura = Convert.ToInt32(reader.GetValue(10).ToString().Trim());
                                if (reader[LocalDatabase.CAMPO_OBSERVACION_DOC] != DBNull.Value)
                                    documentoVentaModel.observacion = reader[LocalDatabase.CAMPO_OBSERVACION_DOC].ToString().Trim();
                                else documentoVentaModel.observacion = "";
                                documentoVentaModel.dev = reader.GetInt32(12);
                                documentoVentaModel.fventa = reader.GetString(13);
                                if (reader[LocalDatabase.CAMPO_FECHAHORAMOV_DOC] != DBNull.Value)
                                    documentoVentaModel.fechahoramov = reader[LocalDatabase.CAMPO_FECHAHORAMOV_DOC].ToString().Trim();
                                else documentoVentaModel.fechahoramov = "";
                                if (reader[LocalDatabase.CAMPO_USUARIOID_DOC] != DBNull.Value)
                                    documentoVentaModel.usuario_id = Convert.ToInt32(reader[LocalDatabase.CAMPO_USUARIOID_DOC].ToString().Trim());
                                else documentoVentaModel.usuario_id = 0;
                                if (reader[LocalDatabase.CAMPO_FORMACOBROIDABONO_DOC] != DBNull.Value)
                                    documentoVentaModel.forma_corbo_id_abono = Convert.ToInt32(reader[LocalDatabase.CAMPO_FORMACOBROIDABONO_DOC].ToString().Trim());
                                if (reader[LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_DOC] != DBNull.Value)
                                    documentoVentaModel.ciddoctopedidocc = Convert.ToInt32(reader[LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_DOC].ToString().Trim());
                                else documentoVentaModel.ciddoctopedidocc = 0;
                                documentoVentaModel.estado = reader.GetInt32(18);
                                documentoVentaModel.enviadoAlWs = reader.GetInt32(19);
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return documentoVentaModel;
        }

        public static DataTable getAllVentasForADayAndUser(String query)
        {
            DataTable dt = null;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                    adapter.SelectCommand = command;
                    dt = new DataTable();
                    adapter.Fill(dt);
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return dt;
        }

        public static String getDocumentObservation(int idDocument)
        {
            String observation = "";
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_OBSERVACION_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA +
                        " WHERE " + LocalDatabase.CAMPO_ID_DOC + " = " + idDocument;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    observation = reader[LocalDatabase.CAMPO_OBSERVACION_DOC].ToString().Trim();
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return observation;
        }

        public static int addIdWebServiceToTheDocument(int id, int idWs)
        {
            int resp = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "UPDATE " + LocalDatabase.TABLA_DOCUMENTOVENTA + " SET " + LocalDatabase.CAMPO_IDWEBSERVICE_DOC + " = " + idWs + " WHERE " +
                    LocalDatabase.CAMPO_ID_DOC + " = " + id;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        resp = 1;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return resp;
        }

        public static int updateAEnviadoUnDocumento(int aplico, int idDocLocal)
        {
            int resp = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "UPDATE " + LocalDatabase.TABLA_DOCUMENTOVENTA + " SET " + LocalDatabase.CAMPO_ENVIADOALWS_DOC + " = " + aplico + " WHERE " +
                    LocalDatabase.CAMPO_ID_DOC + "=" + idDocLocal;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        resp = 1;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return resp;
        }

        public static int getPaymentMethodForADocument(int idDocument)
        {
            int fpId = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_FORMACOBROID_DOC + " FROM " +
                        LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " + LocalDatabase.CAMPO_ID_DOC + " = " + idDocument;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                fpId = Convert.ToInt32(reader[LocalDatabase.CAMPO_FORMACOBROID_DOC].ToString().Trim());
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return fpId;
        }

        public static Boolean verifyIfACustomerHaveAnyDocument(SQLiteConnection db, String claveCliente)
        {
            Boolean resp = false;
            try
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                        LocalDatabase.CAMPO_CLAVECLIENTE_DOC + " = '" + claveCliente + "' AND " + LocalDatabase.CAMPO_CANCELADO_DOC + " = " + 0;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            resp = true;
                        }
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
            }
            return resp;
        }

        public static int getTotalNumberOfDocuments(String query)
        {
            int resp = 0;
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
                                resp = reader.GetInt32(0);
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return resp;
        }

        public static Boolean verifyIfACustomerHaveAnyDocumentWithContext(String claveCliente)
        {
            Boolean resp = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                        LocalDatabase.CAMPO_CLAVECLIENTE_DOC + " = '" + claveCliente + "' AND " + LocalDatabase.CAMPO_CANCELADO_DOC + " = " + 0;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            resp = true;
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return resp;
        }

        public static double getDocumentAdvance(int idDocument)
        {
            double advance = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_ANTICIPO_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA +
                        " WHERE " + LocalDatabase.CAMPO_ID_DOC + " = " + idDocument;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                advance = Convert.ToDouble(reader[LocalDatabase.CAMPO_ANTICIPO_DOC].ToString().Trim());
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return advance;
        }

        public static Boolean updatePaymentFormIdWithHigherAmount(int idDocument, int fcId)
        {
            Boolean updated = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "UPDATE " + LocalDatabase.TABLA_DOCUMENTOVENTA + " SET " + LocalDatabase.CAMPO_FORMACOBROID_DOC + " = " + fcId +
                    " WHERE " + LocalDatabase.CAMPO_ID_DOC + " = " + idDocument;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        updated = true;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return updated;
        }

        public static Boolean deleteDocumentsDownloadedFromOtherDates(SQLiteConnection db, int idDocumentApp)
        {
            Boolean deleted = false;
            try
            {
                FormasDeCobroDocumentoModel.deleteAllFcOfADocumentDb(db, idDocumentApp);
                String query = "DELETE FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " + LocalDatabase.CAMPO_ID_DOC + " = " + idDocumentApp + " AND " +
                    LocalDatabase.CAMPO_ENVIADOALWS_DOC + " = " + 1 + " AND " + LocalDatabase.CAMPO_IDWEBSERVICE_DOC + " > " + 0 + " AND " +
                    LocalDatabase.CAMPO_CANCELADO_DOC + " = " + 0;
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
            }
            return deleted;
        }

        public static List<DocumentModel> obtenerTodosLosDocumentosNoEnviadosAlWs(int enviado)
        {
            List<DocumentModel> todosLosDocs = null;
            DocumentModel documentoVentaModel = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE "+
                    LocalDatabase.CAMPO_PAUSAR_DOC + " = " + 0+
                    " AND "+LocalDatabase.CAMPO_IDWEBSERVICE_DOC+" = "+ enviado + " AND "+
                    LocalDatabase.CAMPO_CANCELADO_DOC+" = 0 ORDER BY " + LocalDatabase.CAMPO_ID_DOC + " ASC LIMIT " + 1;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            todosLosDocs = new List<DocumentModel>();
                            while (reader.Read())
                            {
                                documentoVentaModel = new DocumentModel();
                                documentoVentaModel.id = reader.GetInt32(0);
                                documentoVentaModel.clave_cliente = reader.GetString(1);
                                documentoVentaModel.cliente_id = reader.GetInt32(2);
                                documentoVentaModel.descuento = reader.GetDouble(3);
                                documentoVentaModel.total = reader.GetDouble(4);
                                documentoVentaModel.nombreu = reader.GetString(5);
                                documentoVentaModel.almacen_id = reader.GetInt32(6);
                                documentoVentaModel.anticipo = reader.GetDouble(7);
                                documentoVentaModel.tipo_documento = Convert.ToInt32(reader[LocalDatabase.CAMPO_TIPODOCUMENTO_DOC].ToString().Trim());
                                documentoVentaModel.forma_cobro_id = reader.GetInt32(9);
                                documentoVentaModel.factura = reader.GetInt32(10);
                                String observation = "";
                                if (reader.IsDBNull(reader.GetOrdinal(DbStructure.RomDb.CAMPO_OBSERVACION_DOCUMENTO)))
                                    documentoVentaModel.observacion = observation;
                                else documentoVentaModel.observacion = reader[DbStructure.RomDb.CAMPO_OBSERVACION_DOCUMENTO].ToString().Trim();
                                documentoVentaModel.dev = reader.GetInt32(12);
                                documentoVentaModel.fventa = reader.GetString(13);
                                documentoVentaModel.fechahoramov = reader.GetString(14);
                                documentoVentaModel.usuario_id = reader.GetInt32(15);
                                documentoVentaModel.forma_corbo_id_abono = reader.GetInt32(16);
                                documentoVentaModel.ciddoctopedidocc = reader.GetInt32(17);
                                documentoVentaModel.estado = reader.GetInt32(18);
                                documentoVentaModel.enviadoAlWs = reader.GetInt32(19);
                                documentoVentaModel.idWebService = reader.GetInt32(20);
                                todosLosDocs.Add(documentoVentaModel);
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
            return todosLosDocs;
        }

        public static Boolean updateTheIdOfTheSubscriptionPaymentMethod(int idDocument, int fcIdAbono)
        {
            Boolean updated = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "UPDATE " + LocalDatabase.TABLA_DOCUMENTOVENTA + " SET " + LocalDatabase.CAMPO_FORMACOBROIDABONO_DOC +
                    " = " + fcIdAbono + " WHERE " + LocalDatabase.CAMPO_ID_DOC + " = " + idDocument;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
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

        public static Boolean deleteAllDocuments()
        {
            bool deleted = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "DELETE FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA;
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

        public static Boolean esUnDocumentoDeEntrega(int idDocumento)
        {
            bool response = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT COUNT(*) FROM Documentos D " +
                        "INNER JOIN PedidoEncabezado PE ON D.CIDDOCTOPEDIDOCC = PE.CIDDOCTOPEDIDOCC " +
                        "WHERE D."+LocalDatabase.CAMPO_ID_DISTRIBUIDOR+" = "+idDocumento+" AND D.CIDDOCTOPEDIDOCC != 0 AND PE.type = 4";
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
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) == 1)
                                        response = true;
                                }
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            } catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            } finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return response;
        }

        public static Boolean isItDocumentPrepedidoSendedToTheCustomer(int idDocumento)
        {
            bool sended = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT COUNT(*) FROM Documentos D " +
                    "INNER JOIN PedidoEncabezado P ON D.CIDDOCTOPEDIDOCC = P.CIDDOCTOPEDIDOCC " +
                    "INNER JOIN Movimientos M ON D.id = M.DOCTO_ID_PEDIDO " +
                    "INNER JOIN Weight W ON M.id = W.movementId " +
                    "WHERE D."+LocalDatabase.CAMPO_ID_DOC+" = "+idDocumento+" AND D.pausa = 1 AND P.type = 4 " +
                    "AND P.surtido = 1 AND P.listo = 1 AND W."+LocalDatabase.CAMPO_TIPO_PESO+" = "+WeightModel.TIPO_REAL;
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
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) == 1)
                                        sended = true;
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
            return sended;
        }

        public static bool isItDocumentFromAPrepedido(int idDocumento)
        {
            bool sended = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT COUNT(*) FROM Documentos D " +
                    "INNER JOIN PedidoEncabezado P ON D.CIDDOCTOPEDIDOCC = P.CIDDOCTOPEDIDOCC " +
                    "WHERE D." + LocalDatabase.CAMPO_ID_DOC + " = " + idDocumento + " AND P.type = 4";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value) {
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) == 1)
                                        sended = true;
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
            return sended;
        }

        public static Boolean isItDocumentFromPrepedidoSurtido(int idDocumento)
        {
            bool sended = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT COUNT(*) FROM Documentos D " +
                    "INNER JOIN PedidoEncabezado P ON D.CIDDOCTOPEDIDOCC = P.CIDDOCTOPEDIDOCC " +
                    "WHERE D." + LocalDatabase.CAMPO_ID_DOC + " = " + idDocumento + " AND P.type = 4 AND P.surtido = 1";
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
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) == 1)
                                        sended = true;
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
            return sended;
        }

        public static Boolean isItDocumentPrepedidoSendedToTheCustomerAndPendienteDeDestarar(int idDocumento)
        {
            bool sended = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT COUNT(*) FROM Documentos D " +
                        "INNER JOIN PedidoEncabezado P ON D.CIDDOCTOPEDIDOCC = P.CIDDOCTOPEDIDOCC " +
                        "INNER JOIN Movimientos M ON D.id = M.DOCTO_ID_PEDIDO " +
                        "INNER JOIN Weight W ON M.id = W.movementId " +
                        "WHERE D." + LocalDatabase.CAMPO_ID_DOC + " = " + idDocumento + " AND D.pausa = 1 AND P.type = " +
                        PedidosEncabezadoModel.TYPE_PREPEDIDOS + " AND P.surtido = 1 AND P.listo = 1 AND " +
                        "W." + LocalDatabase.CAMPO_PESOCAJA_PESO + " = " + 0 + " AND W." + LocalDatabase.CAMPO_PESONETO_PESO + " != 0 " +
                        "AND W."+LocalDatabase.CAMPO_TIPO_PESO+" = "+WeightModel.TIPO_REAL;
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
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) == 1)
                                        sended = true;
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
            return sended;
        }

        public static Boolean isItDocumentPrepedidoSendedToTheCustomerAndDestarado(int idDocumento)
        {
            bool sended = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT COUNT(*) FROM Documentos D " +
                        "INNER JOIN PedidoEncabezado P ON D.CIDDOCTOPEDIDOCC = P.CIDDOCTOPEDIDOCC " +
                        "INNER JOIN Movimientos M ON D.id = M.DOCTO_ID_PEDIDO " +
                        "INNER JOIN Weight W ON M.id = W.movementId " +
                        "WHERE D." + LocalDatabase.CAMPO_ID_DOC + " = " + idDocumento + " AND D.pausa = 1 AND P.type = " +
                        PedidosEncabezadoModel.TYPE_PREPEDIDOS + " AND P.surtido = 1 AND P.listo = 1 AND " +
                        "W." + LocalDatabase.CAMPO_PESOCAJA_PESO + " != " + 0 + " AND W." + LocalDatabase.CAMPO_PESONETO_PESO + " != 0 " +
                        "AND W."+LocalDatabase.CAMPO_TIPO_PESO+" = "+WeightModel.TIPO_REAL;
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
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) == 1)
                                        sended = true;
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
            return sended;
        }

        public static Boolean pesoBrutoGuardadoAntesDeEnviarPrepedido(int idDocumento)
        {
            bool sended = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT COUNT(*) FROM "+LocalDatabase.TABLA_DOCUMENTOVENTA+" D " +
                            "INNER JOIN PedidoEncabezado P ON D.CIDDOCTOPEDIDOCC = P.CIDDOCTOPEDIDOCC " +
                            "INNER JOIN Movimientos M ON D.id = M.DOCTO_ID_PEDIDO " +
                            "INNER JOIN Weight W ON M.id = W.movementId " +
                            "WHERE D." + LocalDatabase.CAMPO_ID_DOC + " = " + idDocumento + " AND D.pausa = 1 AND " +
                            "P.type = " +PedidosEncabezadoModel.TYPE_PREPEDIDOS + " AND P.surtido = 1 AND P.listo = 0 AND " +
                            "W." + LocalDatabase.CAMPO_PESOBRUTO_PESO + " != " + 0+" AND "+
                            "W."+LocalDatabase.CAMPO_TIPO_PESO+" = "+WeightModel.TIPO_REAL;
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
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) == 1)
                                        sended = true;
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
            return sended;
        }

        public static Boolean pesoTarasOPolloMalGuardadoAntesDeEnviarPrepedido(int idDocumento)
        {
            bool sended = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " D " +
                            "INNER JOIN PedidoEncabezado P ON D.CIDDOCTOPEDIDOCC = P.CIDDOCTOPEDIDOCC " +
                            "INNER JOIN Movimientos M ON D.id = M.DOCTO_ID_PEDIDO " +
                            "INNER JOIN Weight W ON M.id = W.movementId " +
                            "WHERE D." + LocalDatabase.CAMPO_ID_DOC + " = " + idDocumento + " AND D.pausa = 1 AND " +
                            "P.type = " + PedidosEncabezadoModel.TYPE_PREPEDIDOS + " AND P.surtido = 1 AND P.listo = 0 AND " +
                            "(W." + LocalDatabase.CAMPO_PESOCAJA_PESO + " != " + 0+" OR " +
                            "W."+LocalDatabase.CAMPO_PESOPOLLOLESIONADO_PESO+" != 0 OR " +
                            "W."+LocalDatabase.CAMPO_PESOPOLLOMUERTO_PESO+" != 0 OR " +
                            "W."+LocalDatabase.CAMPO_PESOPOLLOBAJOPESO_PESO+" != 0 OR " +
                            "W."+LocalDatabase.CAMPO_PESOPOLLOGOLPEADO_PESO+") AND "+
                            "W."+LocalDatabase.CAMPO_TIPO_PESO+" = "+WeightModel.TIPO_REAL;
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
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) == 1)
                                        sended = true;
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
            return sended;
        }

        public static Boolean pesoTaraCapturado(int idDocument)
        {
            bool captured = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_PESO + " P " +
                        "INNER JOIN " + LocalDatabase.TABLA_MOVIMIENTO + " M ON P." + LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = M." +
                        LocalDatabase.CAMPO_ID_MOV + " INNER JOIN " + LocalDatabase.TABLA_DOCUMENTOVENTA + " D ON M." +
                        LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = D." + LocalDatabase.CAMPO_ID_DOC + " WHERE D." + LocalDatabase.CAMPO_ID_DOC + " = " +
                        idDocument + " AND P." + LocalDatabase.CAMPO_PESOCAJA_PESO + " != 0 AND P."+
                        LocalDatabase.CAMPO_TIPO_PESO+" = "+WeightModel.TIPO_REAL;
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
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) == 1)
                                        captured = true;
                                }
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
            return captured;
        }

        public static bool updateInvoiceField(int idDocument, int invoice)
        {
            bool updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE "+ LocalDatabase.TABLA_DOCUMENTOVENTA+" SET "+ LocalDatabase.CAMPO_FACTURA_DOC+" = "+ invoice+" WHERE "+
                    LocalDatabase.CAMPO_ID_DOC + " = " + idDocument;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
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

        public static Boolean pesoBrutoCapturado(int idDocument)
        {
            bool captured = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_PESO + " P " +
                        "INNER JOIN " + LocalDatabase.TABLA_MOVIMIENTO + " M ON P." + LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = M." +
                        LocalDatabase.CAMPO_ID_MOV + " INNER JOIN " + LocalDatabase.TABLA_DOCUMENTOVENTA + " D ON M." +
                        LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = D." + LocalDatabase.CAMPO_ID_DOC + " WHERE D." + LocalDatabase.CAMPO_ID_DOC + " = " +
                        idDocument + " AND P." + LocalDatabase.CAMPO_PESOBRUTO_PESO + " != 0 AND P."+
                        LocalDatabase.CAMPO_TIPO_PESO+" = "+WeightModel.TIPO_REAL;
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
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) == 1)
                                        captured = true;
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
            return captured;
        }

        public static Boolean createUpdateOrDeleteRecords(String query)
        {
            bool deleted = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int deletedRecords = command.ExecuteNonQuery();
                    if (deletedRecords > 0)
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
