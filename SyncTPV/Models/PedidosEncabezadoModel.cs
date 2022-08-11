using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Dynamic;
using System.Threading.Tasks;

namespace SyncTPV.Models
{

    public class ResponseCotizacionesMostrador
    {
        public List<PedidosEncabezadoModel> cotizaciones { get; set; }
    }

    public class ResponseDeleteCM
    {
        public List<int> response { get; set; }
    }

    public class ResponsePrepedidos
    {
        public List<PedidosEncabezadoModel> prepedidos { get; set; }
    }

    public class PedidosEncabezadoModel
    {
        public static readonly int TYPE_CC = 1;
        public static readonly int TYPE_COMERCIAL = 2;
        public static readonly int TYPE_COTIZACIONMOSTRADOR = 3;
        public static readonly int TYPE_PREPEDIDOS = 4;
        public int id { get; set; }
        public int idDocumento { get; set; }
        public int clienteId { get; set; }
        public String nombreCliente { get; set; }
        public String telefonoCliente { get; set; }
        public int agenteId { get; set; }
        public String fechaHora { get; set; }
        public String folio { get; set; }
        public String direccion { get; set; }
        public double subtotal { get; set; }
        public double descuento { get; set; }
        public double total { get; set; }
        public double surtido { get; set; }
        public double listo { get; set; } // 2 = eliminado, 1 = enviado al servidor o terminado
        public double type { get; set; }
        public String observation { get; set; }
        public int facturar { get; set; }
        public List<PedidoDetalleModel> movements { get; set; }

        public static int createUpdateOrDeleteRecords(String query)
        {
            int response = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
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

        public static int createUpdateOrDeleteRecords(SQLiteConnection db, String query)
        {
            int response = 0;
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
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
                
            }
            return response;
        }

        public static async Task<int> saveAllCotizacionesMostrador(List<PedidosEncabezadoModel> cotizacionesMostList)
        {
            int lastId = 0;
            if (cotizacionesMostList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (var cotizacionMostrador in cotizacionesMostList)
                    {
                        if (!CustomerModel.checkIfTheClientExists(db, cotizacionMostrador.clienteId))
                        {
                            await CustomersController.getACustomer(db, cotizacionMostrador.clienteId);
                        }
                        if (!validateIfCotizacionMostradorExist(db, cotizacionMostrador.idDocumento))
                        {
                            int lastIdSaved = getLastId(db);
                            lastIdSaved++;
                            String query = "INSERT INTO " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " VALUES (@id, @idDocument, @clienteId, @nombreCliente," +
                                " @telefono, @nombreAgente, @fechaHora, @folio, @subtotal, @descuento, @total, @surtido, @listo, @type, @observation, "+
                                cotizacionMostrador.facturar + ")";
                            using (SQLiteCommand command = new SQLiteCommand(query, db))
                            {
                                command.Parameters.AddWithValue("@id", lastIdSaved);
                                command.Parameters.AddWithValue("@idDocument", cotizacionMostrador.idDocumento);
                                command.Parameters.AddWithValue("@clienteId", cotizacionMostrador.clienteId);
                                command.Parameters.AddWithValue("@nombreCliente", cotizacionMostrador.nombreCliente);
                                command.Parameters.AddWithValue("@telefono", cotizacionMostrador.telefonoCliente);
                                command.Parameters.AddWithValue("@nombreAgente", cotizacionMostrador.agenteId);
                                command.Parameters.AddWithValue("@fechaHora", cotizacionMostrador.fechaHora);
                                command.Parameters.AddWithValue("@folio", cotizacionMostrador.folio);
                                command.Parameters.AddWithValue("@subtotal", 0);
                                command.Parameters.AddWithValue("@descuento", 0);
                                command.Parameters.AddWithValue("@total", 0);
                                command.Parameters.AddWithValue("@surtido", 0);
                                command.Parameters.AddWithValue("@listo", 0);
                                command.Parameters.AddWithValue("@type", TYPE_COTIZACIONMOSTRADOR);
                                command.Parameters.AddWithValue("@observation", cotizacionMostrador.observation);
                                int recordInserted = command.ExecuteNonQuery();
                                if (recordInserted > 0)
                                    PedidoDetalleModel.saveAllMovimientosCotizacionesMostrador(db, cotizacionMostrador.idDocumento, cotizacionMostrador.movements);
                            }
                        } else
                        {
                            PedidoDetalleModel.saveAllMovimientosCotizacionesMostrador(db, cotizacionMostrador.idDocumento, cotizacionMostrador.movements);
                        }
                    }
                } catch (SQLiteException e) {
                    SECUDOC.writeLog(e.ToString());
                } finally {
                    if (db != null && db.State == ConnectionState.Open)
                        db.Close();
                }
            }
            return lastId;
        }

        public static async Task<int> saveAllCotizacionesMostradorLAN(List<ExpandoObject> cotizacionesMostList)
        {
            int lastId = 0;
            if (cotizacionesMostList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (dynamic cotizacionMostrador in cotizacionesMostList)
                    {
                        if (!CustomerModel.checkIfTheClientExists(db, cotizacionMostrador.clienteId))
                        {
                            await CustomersController.getACustomerLAN(db, cotizacionMostrador.clienteId);
                        }
                        if (!validateIfCotizacionMostradorExist(db, cotizacionMostrador.idDocumento))
                        {
                            int lastIdSaved = getLastId(db);
                            lastIdSaved++;
                            String query = "INSERT INTO " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " VALUES (@id, @idDocument, @clienteId, @nombreCliente," +
                                " @telefono, @nombreAgente, @fechaHora, @folio, @subtotal, @descuento, @total, @surtido, @listo, @type, @observation, " +
                                "@facturar)";
                            using (SQLiteCommand command = new SQLiteCommand(query, db))
                            {
                                command.Parameters.AddWithValue("@id", lastIdSaved);
                                command.Parameters.AddWithValue("@idDocument", cotizacionMostrador.idDocumento);
                                command.Parameters.AddWithValue("@clienteId", cotizacionMostrador.clienteId);
                                command.Parameters.AddWithValue("@nombreCliente", cotizacionMostrador.nombreCliente);
                                command.Parameters.AddWithValue("@telefono", cotizacionMostrador.telefonoCliente);
                                command.Parameters.AddWithValue("@nombreAgente", cotizacionMostrador.agenteId);
                                command.Parameters.AddWithValue("@fechaHora", cotizacionMostrador.fechaHora);
                                command.Parameters.AddWithValue("@folio", cotizacionMostrador.folio);
                                command.Parameters.AddWithValue("@subtotal", 0);
                                command.Parameters.AddWithValue("@descuento", 0);
                                command.Parameters.AddWithValue("@total", 0);
                                command.Parameters.AddWithValue("@surtido", 0);
                                command.Parameters.AddWithValue("@listo", 0);
                                command.Parameters.AddWithValue("@type", TYPE_COTIZACIONMOSTRADOR);
                                command.Parameters.AddWithValue("@observation", cotizacionMostrador.observation);
                                command.Parameters.AddWithValue("@facturar", cotizacionMostrador.facturar);
                                int recordInserted = command.ExecuteNonQuery();
                                if (recordInserted > 0)
                                    PedidoDetalleModel.saveAllMovimientosCotizacionesMostradorLAN(db, cotizacionMostrador.idDocumento, cotizacionMostrador.movements);
                            }
                        }
                        else
                        {
                            PedidoDetalleModel.saveAllMovimientosCotizacionesMostradorLAN(db, cotizacionMostrador.idDocumento, cotizacionMostrador.movements);
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

        public static List<PedidosEncabezadoModel> getAllDocuments(String query)
        {
            List<PedidosEncabezadoModel> documentsList = null;
            PedidosEncabezadoModel document = null;
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
                            documentsList = new List<PedidosEncabezadoModel>();
                            while (reader.Read())
                            {
                                document = new PedidosEncabezadoModel();
                                document.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_PEDIDOENCABEZADO].ToString().Trim());
                                document.idDocumento = Convert.ToInt32(reader[LocalDatabase.CAMPO_DOCUMENTOID_PE].ToString().Trim());
                                document.clienteId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLIENTEID_PE].ToString().Trim());
                                document.nombreCliente = reader[LocalDatabase.CAMPO_CNOMBRECLIENTE_PE].ToString();
                                document.telefonoCliente = reader[LocalDatabase.CAMPO_TELEFONO_PEDIDOSENCABEZADO].ToString().Trim();
                                document.agenteId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CNOMBREAGENTECC_PE].ToString().Trim());
                                document.fechaHora = reader[LocalDatabase.CAMPO_CFECHA_PE].ToString().Trim();
                                document.folio = reader[LocalDatabase.CAMPO_CFOLIO_PE].ToString().Trim();
                                document.subtotal = Convert.ToDouble(reader[LocalDatabase.CAMPO_CSUBTOTAL_PE].ToString().Trim());
                                document.descuento = Convert.ToDouble(reader[LocalDatabase.CAMPO_CDESCUENTO_PE].ToString().Trim());
                                document.total = Convert.ToDouble(reader[LocalDatabase.CAMPO_CTOTAL_PE].ToString().Trim());
                                document.surtido = Convert.ToInt32(reader[LocalDatabase.CAMPO_SURTIDO_PE].ToString().Trim());
                                document.listo = Convert.ToInt32(reader[LocalDatabase.CAMPO_LISTO_PE].ToString().Trim());
                                document.type = Convert.ToInt32(reader[LocalDatabase.CAMPO_TYPE_PE].ToString().Trim());
                                document.observation = reader[LocalDatabase.CAMPO_OBSERVATION_PE].ToString().Trim();
                                document.facturar = Convert.ToInt32(reader[LocalDatabase.CAMPO_FACTURAR_PE].ToString().Trim());
                                documentsList.Add(document);
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
            return documentsList;
        }

        public static List<PedidosEncabezadoModel> getAllDocuments(SQLiteConnection db, String query)
        {
            List<PedidosEncabezadoModel> documentsList = null;
            PedidosEncabezadoModel document = null;
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            documentsList = new List<PedidosEncabezadoModel>();
                            while (reader.Read())
                            {
                                document = new PedidosEncabezadoModel();
                                document.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_PEDIDOENCABEZADO].ToString().Trim());
                                document.idDocumento = Convert.ToInt32(reader[LocalDatabase.CAMPO_DOCUMENTOID_PE].ToString().Trim());
                                document.clienteId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLIENTEID_PE].ToString().Trim());
                                document.nombreCliente = reader[LocalDatabase.CAMPO_CNOMBRECLIENTE_PE].ToString();
                                document.telefonoCliente = reader[LocalDatabase.CAMPO_TELEFONO_PEDIDOSENCABEZADO].ToString().Trim();
                                document.agenteId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CNOMBREAGENTECC_PE].ToString().Trim());
                                document.fechaHora = reader[LocalDatabase.CAMPO_CFECHA_PE].ToString().Trim();
                                document.folio = reader[LocalDatabase.CAMPO_CFOLIO_PE].ToString().Trim();
                                document.subtotal = Convert.ToDouble(reader[LocalDatabase.CAMPO_CSUBTOTAL_PE].ToString().Trim());
                                document.descuento = Convert.ToDouble(reader[LocalDatabase.CAMPO_CDESCUENTO_PE].ToString().Trim());
                                document.total = Convert.ToDouble(reader[LocalDatabase.CAMPO_CTOTAL_PE].ToString().Trim());
                                document.surtido = Convert.ToInt32(reader[LocalDatabase.CAMPO_SURTIDO_PE].ToString().Trim());
                                document.listo = Convert.ToInt32(reader[LocalDatabase.CAMPO_LISTO_PE].ToString().Trim());
                                document.type = Convert.ToInt32(reader[LocalDatabase.CAMPO_TYPE_PE].ToString().Trim());
                                document.observation = reader[LocalDatabase.CAMPO_OBSERVATION_PE].ToString().Trim();
                                document.facturar = Convert.ToInt32(reader[LocalDatabase.CAMPO_FACTURAR_PE].ToString().Trim());
                                documentsList.Add(document);
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
                
            }
            return documentsList;
        }

        public static List<PedidosEncabezadoModel> getAllDocumentsWithParametersToSearch(String query, String parameterName1, String parameterValue1, 
            String parameterName2, String parameterValue2)
        {
            List<PedidosEncabezadoModel> documentsList = null;
            PedidosEncabezadoModel document = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@"+parameterName1, "%"+parameterValue1+"%");
                    command.Parameters.AddWithValue("@" + parameterName2, "%"+parameterValue2+"%");
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            documentsList = new List<PedidosEncabezadoModel>();
                            while (reader.Read())
                            {
                                document = new PedidosEncabezadoModel();
                                document.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_PEDIDOENCABEZADO].ToString().Trim());
                                document.idDocumento = Convert.ToInt32(reader[LocalDatabase.CAMPO_DOCUMENTOID_PE].ToString().Trim());
                                document.clienteId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLIENTEID_PE].ToString().Trim());
                                document.nombreCliente = reader[LocalDatabase.CAMPO_CNOMBRECLIENTE_PE].ToString();
                                document.telefonoCliente = reader[LocalDatabase.CAMPO_TELEFONO_PEDIDOSENCABEZADO].ToString().Trim();
                                document.agenteId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CNOMBREAGENTECC_PE].ToString().Trim());
                                document.fechaHora = reader[LocalDatabase.CAMPO_CFECHA_PE].ToString().Trim();
                                document.folio = reader[LocalDatabase.CAMPO_CFOLIO_PE].ToString().Trim();
                                document.subtotal = Convert.ToDouble(reader[LocalDatabase.CAMPO_CSUBTOTAL_PE].ToString().Trim());
                                document.descuento = Convert.ToDouble(reader[LocalDatabase.CAMPO_CDESCUENTO_PE].ToString().Trim());
                                document.total = Convert.ToDouble(reader[LocalDatabase.CAMPO_CTOTAL_PE].ToString().Trim());
                                document.surtido = Convert.ToInt32(reader[LocalDatabase.CAMPO_SURTIDO_PE].ToString().Trim());
                                document.listo = Convert.ToInt32(reader[LocalDatabase.CAMPO_LISTO_PE].ToString().Trim());
                                document.type = Convert.ToInt32(reader[LocalDatabase.CAMPO_TYPE_PE].ToString().Trim());
                                document.observation = reader[LocalDatabase.CAMPO_OBSERVATION_PE].ToString().Trim();
                                document.facturar = Convert.ToInt32(reader[LocalDatabase.CAMPO_FACTURAR_PE].ToString().Trim());
                                documentsList.Add(document);
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
            return documentsList;
        }

        public static PedidosEncabezadoModel getAPedCot(String query)
        {
            PedidosEncabezadoModel document = null;
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
                                document = new PedidosEncabezadoModel();
                                document.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_PEDIDOENCABEZADO].ToString().Trim());
                                document.idDocumento = Convert.ToInt32(reader[LocalDatabase.CAMPO_DOCUMENTOID_PE].ToString().Trim());
                                document.clienteId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLIENTEID_PE].ToString().Trim());
                                document.nombreCliente = reader[LocalDatabase.CAMPO_CNOMBRECLIENTE_PE].ToString();
                                document.telefonoCliente = reader[LocalDatabase.CAMPO_TELEFONO_PEDIDOSENCABEZADO].ToString().Trim();
                                document.agenteId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CNOMBREAGENTECC_PE].ToString().Trim());
                                document.fechaHora = reader[LocalDatabase.CAMPO_CFECHA_PE].ToString().Trim();
                                document.folio = reader[LocalDatabase.CAMPO_CFOLIO_PE].ToString().Trim();
                                document.subtotal = Convert.ToDouble(reader[LocalDatabase.CAMPO_CSUBTOTAL_PE].ToString().Trim());
                                document.descuento = Convert.ToDouble(reader[LocalDatabase.CAMPO_CDESCUENTO_PE].ToString().Trim());
                                document.total = Convert.ToDouble(reader[LocalDatabase.CAMPO_CTOTAL_PE].ToString().Trim());
                                document.surtido = Convert.ToInt32(reader[LocalDatabase.CAMPO_SURTIDO_PE].ToString().Trim());
                                document.listo = Convert.ToInt32(reader[LocalDatabase.CAMPO_LISTO_PE].ToString().Trim());
                                document.type = Convert.ToInt32(reader[LocalDatabase.CAMPO_TYPE_PE].ToString().Trim());
                                document.observation = reader[LocalDatabase.CAMPO_OBSERVATION_PE].ToString().Trim();
                                document.facturar = Convert.ToInt32(reader[LocalDatabase.CAMPO_FACTURAR_PE].ToString().Trim());
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
            return document;
        }

        public static PedidosEncabezadoModel getAPedCot(int idDocument)
        {
            PedidosEncabezadoModel document = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " WHERE " +
                    LocalDatabase.CAMPO_DOCUMENTOID_PE + " = " + idDocument;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                document = new PedidosEncabezadoModel();
                                document.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_PEDIDOENCABEZADO].ToString().Trim());
                                document.idDocumento = Convert.ToInt32(reader[LocalDatabase.CAMPO_DOCUMENTOID_PE].ToString().Trim());
                                document.clienteId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLIENTEID_PE].ToString().Trim());
                                document.nombreCliente = reader[LocalDatabase.CAMPO_CNOMBRECLIENTE_PE].ToString();
                                document.telefonoCliente = reader[LocalDatabase.CAMPO_TELEFONO_PEDIDOSENCABEZADO].ToString().Trim();
                                document.agenteId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CNOMBREAGENTECC_PE].ToString().Trim());
                                document.fechaHora = reader[LocalDatabase.CAMPO_CFECHA_PE].ToString().Trim();
                                document.folio = reader[LocalDatabase.CAMPO_CFOLIO_PE].ToString().Trim();
                                document.subtotal = Convert.ToDouble(reader[LocalDatabase.CAMPO_CSUBTOTAL_PE].ToString().Trim());
                                document.descuento = Convert.ToDouble(reader[LocalDatabase.CAMPO_CDESCUENTO_PE].ToString().Trim());
                                document.total = Convert.ToDouble(reader[LocalDatabase.CAMPO_CTOTAL_PE].ToString().Trim());
                                document.surtido = Convert.ToInt32(reader[LocalDatabase.CAMPO_SURTIDO_PE].ToString().Trim());
                                document.listo = Convert.ToInt32(reader[LocalDatabase.CAMPO_LISTO_PE].ToString().Trim());
                                document.type = Convert.ToInt32(reader[LocalDatabase.CAMPO_TYPE_PE].ToString().Trim());
                                document.observation = reader[LocalDatabase.CAMPO_OBSERVATION_PE].ToString().Trim();
                                document.facturar = Convert.ToInt32(reader[LocalDatabase.CAMPO_FACTURAR_PE].ToString().Trim());
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
            return document;
        }

        public static PedidosEncabezadoModel getAPedCot(SQLiteConnection db, String query)
        {
            PedidosEncabezadoModel document = null;
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader readerPedido = command.ExecuteReader())
                    {
                        if (readerPedido.HasRows)
                        {
                            while (readerPedido.Read())
                            {
                                document = new PedidosEncabezadoModel();
                                document.id = Convert.ToInt32(readerPedido[LocalDatabase.CAMPO_ID_PEDIDOENCABEZADO].ToString().Trim());
                                document.idDocumento = Convert.ToInt32(readerPedido[LocalDatabase.CAMPO_DOCUMENTOID_PE].ToString().Trim());
                                document.clienteId = Convert.ToInt32(readerPedido[LocalDatabase.CAMPO_CLIENTEID_PE].ToString().Trim());
                                document.nombreCliente = readerPedido[LocalDatabase.CAMPO_CNOMBRECLIENTE_PE].ToString();
                                document.telefonoCliente = readerPedido[LocalDatabase.CAMPO_TELEFONO_PEDIDOSENCABEZADO].ToString().Trim();
                                document.agenteId = Convert.ToInt32(readerPedido[LocalDatabase.CAMPO_CNOMBREAGENTECC_PE].ToString().Trim());
                                document.fechaHora = readerPedido[LocalDatabase.CAMPO_CFECHA_PE].ToString().Trim();
                                document.folio = readerPedido[LocalDatabase.CAMPO_CFOLIO_PE].ToString().Trim();
                                document.subtotal = Convert.ToDouble(readerPedido[LocalDatabase.CAMPO_CSUBTOTAL_PE].ToString().Trim());
                                document.descuento = Convert.ToDouble(readerPedido[LocalDatabase.CAMPO_CDESCUENTO_PE].ToString().Trim());
                                document.total = Convert.ToDouble(readerPedido[LocalDatabase.CAMPO_CTOTAL_PE].ToString().Trim());
                                document.surtido = Convert.ToInt32(readerPedido[LocalDatabase.CAMPO_SURTIDO_PE].ToString().Trim());
                                document.listo = Convert.ToInt32(readerPedido[LocalDatabase.CAMPO_LISTO_PE].ToString().Trim());
                                document.type = Convert.ToInt32(readerPedido[LocalDatabase.CAMPO_TYPE_PE].ToString().Trim());
                                document.observation = readerPedido[LocalDatabase.CAMPO_OBSERVATION_PE].ToString().Trim();
                                document.facturar = Convert.ToInt32(readerPedido[LocalDatabase.CAMPO_FACTURAR_PE].ToString().Trim());
                            }
                        }
                        if (readerPedido != null && !readerPedido.IsClosed)
                            readerPedido.Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                SECUDOC.writeLog(Ex.ToString());
            }
            finally
            {
                
            }
            return document;
        }

        public static List<int> getAllIdsCotizacionesMostrador()
        {
            List<int> idsList = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT "+LocalDatabase.CAMPO_DOCUMENTOID_PE + " FROM "+LocalDatabase.TABLA_PEDIDOENCABEZADO+" WHERE "+
                    LocalDatabase.CAMPO_TYPE_PE+" = "+3;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            idsList = new List<int>();
                            while (reader.Read())
                            {
                                idsList.Add(Convert.ToInt32(reader[LocalDatabase.CAMPO_DOCUMENTOID_PE].ToString().Trim()));
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
            return idsList;
        }

        public static int getLastId()
        {
            int lastId = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT MAX(" + LocalDatabase.CAMPO_ID_PEDIDOENCABEZADO + ") FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO+" ORDER BY "+
                    LocalDatabase.CAMPO_ID_PEDIDOENCABEZADO;
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
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return lastId;
        }

        public static int getLastIdPanel()
        {
            int lastId = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT MAX(" + LocalDatabase.CAMPO_DOCUMENTOID_PE + ") FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " ORDER BY " +
                    LocalDatabase.CAMPO_DOCUMENTOID_PE;
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
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return lastId;
        }

        public static int getLastId(SQLiteConnection db)
        {
            int lastId = 0;
            try
            {
                String query = "SELECT MAX(" + LocalDatabase.CAMPO_ID_PEDIDOENCABEZADO + ") FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " ORDER BY " +
                    LocalDatabase.CAMPO_ID_PEDIDOENCABEZADO;
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
                
            }
            return lastId;
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
            catch (Exception Ex)
            {
                SECUDOC.writeLog(Ex.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return value;
        }

        public static int getIntValueWithParameters(String query, String parameterName1, String parameterValue1, String parameterName2,
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
                    command.Parameters.AddWithValue("@" + parameterName2, "%" + parameterValue2 + "%");
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
            catch (Exception Ex)
            {
                SECUDOC.writeLog(Ex.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return value;
        }

        public static int estaElPrepedidoSurtidoYEntregado(int idPedido)
        {
            int value = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT COUNT(*) FROM "+LocalDatabase.TABLA_PEDIDOENCABEZADO+" WHERE "+
                    LocalDatabase.CAMPO_DOCUMENTOID_PE+" = "+idPedido+" AND "+LocalDatabase.CAMPO_SURTIDO_PE+" = 1 AND "+
                    LocalDatabase.CAMPO_LISTO_PE+" = 1 AND "+LocalDatabase.CAMPO_TYPE_PE+" = "+TYPE_PREPEDIDOS;
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
            catch (Exception Ex)
            {
                SECUDOC.writeLog(Ex.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return value;
        }

        public static int getOrderType(int idPedido)
        {
            int value = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT "+LocalDatabase.CAMPO_TYPE_PE+" FROM "+LocalDatabase.TABLA_PEDIDOENCABEZADO+" WHERE "+
                    LocalDatabase.CAMPO_ID_PEDIDOENCABEZADO+" = "+idPedido;
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
            catch (Exception Ex)
            {
                SECUDOC.writeLog(Ex.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return value;
        }

        public static int getIntValue(SQLiteConnection db, String query)
        {
            int value = 0;
            try {
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                value = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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
                
            }
            return value;
        }

        public static List<int> getIntListVlues(String query)
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
                            {
                                valuesList.Add(Convert.ToInt32(reader.GetValue(0).ToString().Trim()));
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
            return valuesList;
        }

        public static List<int> getIntListVlues(SQLiteConnection db, String query)
        {
            List<int> valuesList = null;
            try {
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            valuesList = new List<int>();
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    valuesList.Add(Convert.ToInt32(reader.GetValue(0).ToString().Trim()));
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

            }
            return valuesList;
        }

        public static Boolean validateIfCotizacionMostradorExist(int idCotizacionMostrador)
        {
            bool exist = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " WHERE " +
                    LocalDatabase.CAMPO_DOCUMENTOID_PE + " = "+ idCotizacionMostrador + " AND "+LocalDatabase.CAMPO_TYPE_PE+" = "+TYPE_COTIZACIONMOSTRADOR;
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
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return exist;
        }

        public static Boolean validateIfCotizacionMostradorExist(SQLiteConnection db, int idCotizacionMostrador)
        {
            bool exist = false;
            try
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " WHERE " +
                    LocalDatabase.CAMPO_DOCUMENTOID_PE + " = " + idCotizacionMostrador + " AND " + LocalDatabase.CAMPO_TYPE_PE + " = " + TYPE_COTIZACIONMOSTRADOR;
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
                
            }
            return exist;
        }

        public static Boolean documentExists(int idDocument)
        {
            bool exist = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " WHERE " +
                    LocalDatabase.CAMPO_DOCUMENTOID_PE + " = " + idDocument;
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
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return exist;
        }

        public static Boolean documentExists(SQLiteConnection db, int idDocument)
        {
            bool exist = false;
            try
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " WHERE " +
                    LocalDatabase.CAMPO_DOCUMENTOID_PE + " = " + idDocument;
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
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                
            }
            return exist;
        }

        public static int marcarPedidoComoSurtidoONoSurtido(int idPedido, int surtido)
        {
            int resp = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " SET " + 
                    LocalDatabase.CAMPO_SURTIDO_PE + " = " + surtido + 
                    " WHERE " +LocalDatabase.CAMPO_DOCUMENTOID_PE + " = " + idPedido;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        resp = 1;
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

        public static int marcarPedidoComoListoONo(int idPedido, int listo)
        {
            int resp = 0;
            var db = new SQLiteConnection();            
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " SET " + LocalDatabase.CAMPO_LISTO_PE + " = " + listo + 
                    " WHERE " +LocalDatabase.CAMPO_DOCUMENTOID_PE + " = " + idPedido;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        resp = 1;
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

        public static int updateSubtotalDescuentoYTotalCotizacionesMostrador(int idDocumento, double subtotal, double descuento, double total)
        {
            int resp = 0;
            var db = new SQLiteConnection();            
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " SET " + LocalDatabase.CAMPO_CSUBTOTAL_PE + " = " + subtotal + ", " +
                    LocalDatabase.CAMPO_CDESCUENTO_PE+" = "+descuento+", "+LocalDatabase.CAMPO_CTOTAL_PE+" = "+total+" WHERE " +
                    LocalDatabase.CAMPO_DOCUMENTOID_PE + " = " + idDocumento;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        resp = 1;
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

        public static bool updateSubtotalDescuentoYTotalCotizacionesMostrador(SQLiteConnection db, int idDocumento, double subtotal, double descuento, double total)
        {
            bool resp = false;
            try
            {
                String query = "UPDATE " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " SET " + LocalDatabase.CAMPO_CSUBTOTAL_PE + " = @subtotal, " +
                    LocalDatabase.CAMPO_CDESCUENTO_PE + " = @descuento, " + LocalDatabase.CAMPO_CTOTAL_PE + " = @total WHERE " +
                    LocalDatabase.CAMPO_DOCUMENTOID_PE + " = @idDocumento";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@subtotal", subtotal);
                    command.Parameters.AddWithValue("@descuento", descuento);
                    command.Parameters.AddWithValue("@total", total);
                    command.Parameters.AddWithValue("@idDocumento", idDocumento);
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

            }
            return resp;
        }

        public static bool updateARecord(String query)
        {
            bool updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        updated = true;
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
            return updated;
        }

        public static bool deletePrepedidosDeleted()
        {
            
            bool deleted = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "DELETE FROM pedidoEncabezado WHERE type = 4 AND listo = 2";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int deletedRegist = command.ExecuteNonQuery();
                    if (deletedRegist > 0)
                        deleted = true;
                }
            }
            catch (SQLiteException ex)
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

        public static bool deleteARecord(String query)
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

        public static bool deleteARecord(SQLiteConnection db, String query)
        {
            bool deleted = false;
            try {
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

            }
            return deleted;
        }

        public static Boolean deleteAllCotizacionesMostrador()
        {
            bool deleted = false;
            bool detallesDeleted = PedidoDetalleModel.deleteAllPedidosDetalleCotizacionesMostrador();
            if (detallesDeleted)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    String query = "DELETE FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " WHERE " + LocalDatabase.CAMPO_TYPE_PE + " = " + 3;
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
            }
            return deleted;
        }

    }

}
