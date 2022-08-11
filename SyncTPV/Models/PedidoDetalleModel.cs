using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncTPV.Models
{
    public class PedidoDetalleModel
    {
        public int id { get; set; }
        public int documentoId { get; set; }
        public int itemId { get; set; }
        public String itemCode { get; set; }
        public int numero { get; set; }
        public double precio { get; set; }
        public double unidadesCapturadas { get; set; }
        public int unidadCapturadaId { get; set; }
        public double subtotal { get; set; }
        public double descuento { get; set; }
        public double total { get; set; }
        public double unidadesNoConvertibles { get; set; }
        public int unidadNoConvertibleId { get; set; }
        public String observation { get; set; }

        public static bool createUpdateOrDeleteRecords(SQLiteConnection db, String query, String observation)
        {
            bool deleted = false;
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@observation", observation);
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

        public static int saveAllMovimientosCotizacionesMostrador(int idDocumento, List<PedidoDetalleModel> movimientosList)
        {
            int lastId = 0;
            if (movimientosList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    double descuento = 0, total = 0;
                    foreach (var movimiento in movimientosList)
                    {
                        String queryVerifyMove = "SELECT "+LocalDatabase.CAMPO_ID_PD + " FROM "+LocalDatabase.TABLA_PEDIDODETALLE + " WHERE "+
                            LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_PD+" = "+ idDocumento+" AND "+LocalDatabase.CAMPO_CNUMEROMOVIMIENTO_PD+" = "+ movimiento.numero+
                            " AND "+LocalDatabase.CAMPO_CIDPRODUCTO_PD+" = "+movimiento.itemId;
                        int idMoveAnterior = validateIfMovementExist(db, queryVerifyMove);
                        if (idMoveAnterior == 0)
                        {
                            int lastIdSaved = getLastId(db);
                            lastIdSaved++;
                            String query = "INSERT INTO " + LocalDatabase.TABLA_PEDIDODETALLE + " VALUES (@id, @documentoId, @itemId, @itemCode," +
                                " @numero, @precio, @unidadesCapturadas, @unidadCapturadaId, @subtotal, @descuento, @total, @unidadesNoConvertibles, " +
                                "@unidadNoConvertibleId, @observation)";
                            using (SQLiteCommand command = new SQLiteCommand(query, db))
                            {
                                double descuentoDecimal = (movimiento.descuento / 100);
                                descuento += (movimiento.subtotal * descuentoDecimal);
                                total += movimiento.subtotal;
                                command.Parameters.AddWithValue("@id", lastIdSaved);
                                command.Parameters.AddWithValue("@documentoId", idDocumento);
                                command.Parameters.AddWithValue("@itemId", movimiento.itemId);
                                command.Parameters.AddWithValue("@itemCode", movimiento.itemCode);
                                command.Parameters.AddWithValue("@numero", movimiento.numero);
                                command.Parameters.AddWithValue("@precio", movimiento.precio);
                                command.Parameters.AddWithValue("@unidadesCapturadas", movimiento.unidadesCapturadas);
                                command.Parameters.AddWithValue("@unidadCapturadaId", movimiento.unidadCapturadaId);
                                command.Parameters.AddWithValue("@subtotal", movimiento.subtotal);
                                command.Parameters.AddWithValue("@descuento", movimiento.descuento);
                                command.Parameters.AddWithValue("@total", movimiento.total);
                                command.Parameters.AddWithValue("@unidadesNoConvertibles", movimiento.unidadesNoConvertibles);
                                command.Parameters.AddWithValue("@unidadNoConvertibleId", movimiento.unidadNoConvertibleId);
                                command.Parameters.AddWithValue("@observation", movimiento.observation);
                                int recordInserted = command.ExecuteNonQuery();
                                if (recordInserted > 0)
                                    lastId = recordInserted;
                            }
                        }
                    }
                    if (total > 0)
                        PedidosEncabezadoModel.updateSubtotalDescuentoYTotalCotizacionesMostrador(db, idDocumento, total, descuento, (total - descuento));
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

        public static int saveAllMovimientosCotizacionesMostrador(SQLiteConnection db, int idDocumento, List<PedidoDetalleModel> movimientosList)
        {
            int lastId = 0;
            if (movimientosList != null)
            {
                try
                {
                    double descuento = 0, total = 0;
                    foreach (var movimiento in movimientosList)
                    {
                        String queryVerifyMove = "SELECT " + LocalDatabase.CAMPO_ID_PD + " FROM " + LocalDatabase.TABLA_PEDIDODETALLE + " WHERE " +
                            LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_PD + " = " + idDocumento + " AND " + LocalDatabase.CAMPO_CNUMEROMOVIMIENTO_PD + " = " + movimiento.numero +
                            " AND " + LocalDatabase.CAMPO_CIDPRODUCTO_PD + " = " + movimiento.itemId;
                        int idMoveAnterior = validateIfMovementExist(db, queryVerifyMove);
                        if (idMoveAnterior == 0)
                        {
                            int lastIdSaved = getLastId(db);
                            lastIdSaved++;
                            String query = "INSERT INTO " + LocalDatabase.TABLA_PEDIDODETALLE + " VALUES (@id, @documentoId, @itemId, @itemCode," +
                                " @numero, @precio, @unidadesCapturadas, @unidadCapturadaId, @subtotal, @descuento, @total, @unidadesNoConvertibles, " +
                                "@unidadNoConvertibleId, @observation)";
                            using (SQLiteCommand command = new SQLiteCommand(query, db))
                            {
                                double descuentoDecimal = (movimiento.descuento / 100);
                                descuento += (movimiento.subtotal * descuentoDecimal);
                                total += movimiento.subtotal;
                                command.Parameters.AddWithValue("@id", lastIdSaved);
                                command.Parameters.AddWithValue("@documentoId", idDocumento);
                                command.Parameters.AddWithValue("@itemId", movimiento.itemId);
                                command.Parameters.AddWithValue("@itemCode", movimiento.itemCode);
                                command.Parameters.AddWithValue("@numero", movimiento.numero);
                                command.Parameters.AddWithValue("@precio", movimiento.precio);
                                command.Parameters.AddWithValue("@unidadesCapturadas", movimiento.unidadesCapturadas);
                                command.Parameters.AddWithValue("@unidadCapturadaId", movimiento.unidadCapturadaId);
                                command.Parameters.AddWithValue("@subtotal", movimiento.subtotal);
                                command.Parameters.AddWithValue("@descuento", movimiento.descuento);
                                command.Parameters.AddWithValue("@total", movimiento.total);
                                command.Parameters.AddWithValue("@unidadesNoConvertibles", movimiento.unidadesNoConvertibles);
                                command.Parameters.AddWithValue("@unidadNoConvertibleId", movimiento.unidadNoConvertibleId);
                                command.Parameters.AddWithValue("@observation", movimiento.observation);
                                int recordInserted = command.ExecuteNonQuery();
                                if (recordInserted > 0)
                                    lastId = recordInserted;
                            }
                        }
                    }
                    if (total > 0)
                        PedidosEncabezadoModel.updateSubtotalDescuentoYTotalCotizacionesMostrador(db, idDocumento, total, descuento, (total - descuento));
                }
                catch (SQLiteException e)
                {
                    SECUDOC.writeLog(e.ToString());
                }
                finally
                {
                    
                }
            }
            return lastId;
        }

        public static int saveAllMovimientosCotizacionesMostradorLAN(int idDocumento, List<ExpandoObject> movimientosList)
        {
            int lastId = 0;
            if (movimientosList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    double descuento = 0, total = 0;
                    foreach (dynamic movimiento in movimientosList)
                    {
                        String queryVerifyMove = "SELECT " + LocalDatabase.CAMPO_ID_PD + " FROM " + LocalDatabase.TABLA_PEDIDODETALLE + " WHERE " +
                            LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_PD + " = " + idDocumento + " AND " + LocalDatabase.CAMPO_CNUMEROMOVIMIENTO_PD + " = " + movimiento.numero +
                            " AND " + LocalDatabase.CAMPO_CIDPRODUCTO_PD + " = " + movimiento.itemId;
                        int idMoveAnterior = validateIfMovementExist(queryVerifyMove);
                        if (idMoveAnterior == 0)
                        {
                            int lastIdSaved = getLastId(db);
                            lastIdSaved++;
                            String query = "INSERT INTO " + LocalDatabase.TABLA_PEDIDODETALLE + " VALUES (@id, @documentoId, @itemId, @itemCode," +
                                " @numero, @precio, @unidadesCapturadas, @unidadCapturadaId, @subtotal, @descuento, @total, @unidadesNoConvertibles, " +
                                "@unidadNoConvertibleId, @observation)";
                            using (SQLiteCommand command = new SQLiteCommand(query, db))
                            {
                                double descuentoDecimal = (movimiento.descuento / 100);
                                descuento += (movimiento.subtotal * descuentoDecimal);
                                total += movimiento.subtotal;
                                command.Parameters.AddWithValue("@id", lastIdSaved);
                                command.Parameters.AddWithValue("@documentoId", idDocumento);
                                command.Parameters.AddWithValue("@itemId", movimiento.itemId);
                                command.Parameters.AddWithValue("@itemCode", movimiento.itemCode);
                                command.Parameters.AddWithValue("@numero", movimiento.numero);
                                command.Parameters.AddWithValue("@precio", movimiento.precio);
                                command.Parameters.AddWithValue("@unidadesCapturadas", movimiento.unidadesCapturadas);
                                command.Parameters.AddWithValue("@unidadCapturadaId", movimiento.unidadCapturadaId);
                                command.Parameters.AddWithValue("@subtotal", movimiento.subtotal);
                                command.Parameters.AddWithValue("@descuento", movimiento.descuento);
                                command.Parameters.AddWithValue("@total", movimiento.total);
                                command.Parameters.AddWithValue("@unidadesNoConvertibles", movimiento.unidadesNoConvertibles);
                                command.Parameters.AddWithValue("@unidadNoConvertibleId", movimiento.unidadNoConvertibleId);
                                command.Parameters.AddWithValue("@observation", movimiento.observation);
                                int recordInserted = command.ExecuteNonQuery();
                                if (recordInserted > 0)
                                    lastId = recordInserted;
                            }
                        }
                    }
                    if (total > 0)
                        PedidosEncabezadoModel.updateSubtotalDescuentoYTotalCotizacionesMostrador(db, idDocumento, total, descuento, (total - descuento));
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

        public static int saveAllMovimientosCotizacionesMostradorLAN(SQLiteConnection db, int idDocumento, List<ExpandoObject> movimientosList)
        {
            int lastId = 0;
            if (movimientosList != null)
            {
                try
                {
                    double descuento = 0, total = 0;
                    foreach (dynamic movimiento in movimientosList)
                    {
                        String queryVerifyMove = "SELECT " + LocalDatabase.CAMPO_ID_PD + " FROM " + LocalDatabase.TABLA_PEDIDODETALLE + " WHERE " +
                            LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_PD + " = " + idDocumento + " AND " + 
                            LocalDatabase.CAMPO_CNUMEROMOVIMIENTO_PD + " = " + movimiento.numero +
                            " AND " + LocalDatabase.CAMPO_CIDPRODUCTO_PD + " = " + movimiento.itemId;
                        int idMoveAnterior = validateIfMovementExist(db, queryVerifyMove);
                        if (idMoveAnterior == 0)
                        {
                            int lastIdSaved = getLastId(db);
                            lastIdSaved++;
                            String query = "INSERT INTO " + LocalDatabase.TABLA_PEDIDODETALLE + " VALUES (@id, @documentoId, @itemId, @itemCode," +
                                " @numero, @precio, @unidadesCapturadas, @unidadCapturadaId, @subtotal, @descuento, @total, @unidadesNoConvertibles, " +
                                "@unidadNoConvertibleId, @observation)";
                            using (SQLiteCommand command = new SQLiteCommand(query, db))
                            {
                                double descuentoDecimal = (movimiento.descuento / 100);
                                descuento += (movimiento.subtotal * descuentoDecimal);
                                total += movimiento.subtotal;
                                command.Parameters.AddWithValue("@id", lastIdSaved);
                                command.Parameters.AddWithValue("@documentoId", idDocumento);
                                command.Parameters.AddWithValue("@itemId", movimiento.itemId);
                                command.Parameters.AddWithValue("@itemCode", movimiento.itemCode);
                                command.Parameters.AddWithValue("@numero", movimiento.numero);
                                command.Parameters.AddWithValue("@precio", movimiento.precio);
                                command.Parameters.AddWithValue("@unidadesCapturadas", movimiento.unidadesCapturadas);
                                command.Parameters.AddWithValue("@unidadCapturadaId", movimiento.unidadCapturadaId);
                                command.Parameters.AddWithValue("@subtotal", movimiento.subtotal);
                                command.Parameters.AddWithValue("@descuento", movimiento.descuento);
                                command.Parameters.AddWithValue("@total", movimiento.total);
                                command.Parameters.AddWithValue("@unidadesNoConvertibles", movimiento.unidadesNoConvertibles);
                                command.Parameters.AddWithValue("@unidadNoConvertibleId", movimiento.unidadNoConvertibleId);
                                command.Parameters.AddWithValue("@observation", movimiento.observation);
                                int recordInserted = command.ExecuteNonQuery();
                                if (recordInserted > 0)
                                    lastId = recordInserted;
                            }
                        }
                    }
                    if (total > 0)
                        PedidosEncabezadoModel.updateSubtotalDescuentoYTotalCotizacionesMostrador(db, idDocumento, total, descuento, (total - descuento));
                }
                catch (SQLiteException e)
                {
                    SECUDOC.writeLog(e.ToString());
                }
                finally
                {
                    
                }
            }
            return lastId;
        }

        public static List<PedidoDetalleModel> getAllMovements(String query)
        {
            List<PedidoDetalleModel> movementsList = null;
            PedidoDetalleModel movement = null;
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
                            movementsList = new List<PedidoDetalleModel>();
                            while (reader.Read())
                            {
                                movement = new PedidoDetalleModel();
                                movement.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_PD].ToString().Trim());
                                movement.documentoId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_PD].ToString().Trim());
                                movement.itemId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CIDPRODUCTO_PD].ToString().Trim());
                                movement.itemCode = reader[LocalDatabase.CAMPO_CCODIGOPRODUCTO_PD].ToString();
                                movement.numero = Convert.ToInt32(reader[LocalDatabase.CAMPO_CNUMEROMOVIMIENTO_PD].ToString().Trim());
                                movement.precio = Convert.ToDouble(reader[LocalDatabase.CAMPO_CPRECIO_PD].ToString().Trim());
                                movement.unidadesCapturadas = Convert.ToDouble(reader[LocalDatabase.CAMPO_CUNIDADES_PD].ToString().Trim());
                                movement.unidadCapturadaId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CAPTUREDUNITID_PD].ToString().Trim());
                                movement.subtotal = Convert.ToDouble(reader[LocalDatabase.CAMPO_CSUBTOTAL_PD].ToString().Trim());
                                movement.descuento = Convert.ToDouble(reader[LocalDatabase.CAMPO_CDESCUENTO_PD].ToString().Trim());
                                movement.total = Convert.ToDouble(reader[LocalDatabase.CAMPO_CTOTAL_PD].ToString().Trim());
                                movement.unidadesNoConvertibles = Convert.ToDouble(reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNITS_PD].ToString().Trim());
                                movement.unidadNoConvertibleId = Convert.ToInt32(reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_PD].ToString().Trim());
                                movement.observation = reader[LocalDatabase.CAMPO_OBSERVATION_PD].ToString().Trim();
                                movementsList.Add(movement);
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
            return movementsList;
        }

        public static List<PedidoDetalleModel> getAllMovementsFromAnOrder(int idPedido)
        {
            List<PedidoDetalleModel> movementsList = null;
            PedidoDetalleModel movement = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_PEDIDODETALLE + " WHERE " +
                    LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_PD + " = @idPedido";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idPedido", idPedido);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            movementsList = new List<PedidoDetalleModel>();
                            while (reader.Read())
                            {
                                movement = new PedidoDetalleModel();
                                movement.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_PD].ToString().Trim());
                                movement.documentoId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_PD].ToString().Trim());
                                movement.itemId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CIDPRODUCTO_PD].ToString().Trim());
                                movement.itemCode = reader[LocalDatabase.CAMPO_CCODIGOPRODUCTO_PD].ToString();
                                movement.numero = Convert.ToInt32(reader[LocalDatabase.CAMPO_CNUMEROMOVIMIENTO_PD].ToString().Trim());
                                movement.precio = Convert.ToDouble(reader[LocalDatabase.CAMPO_CPRECIO_PD].ToString().Trim());
                                movement.unidadesCapturadas = Convert.ToDouble(reader[LocalDatabase.CAMPO_CUNIDADES_PD].ToString().Trim());
                                movement.unidadCapturadaId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CAPTUREDUNITID_PD].ToString().Trim());
                                movement.subtotal = Convert.ToDouble(reader[LocalDatabase.CAMPO_CSUBTOTAL_PD].ToString().Trim());
                                movement.descuento = Convert.ToDouble(reader[LocalDatabase.CAMPO_CDESCUENTO_PD].ToString().Trim());
                                movement.total = Convert.ToDouble(reader[LocalDatabase.CAMPO_CTOTAL_PD].ToString().Trim());
                                movement.unidadesNoConvertibles = Convert.ToDouble(reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNITS_PD].ToString().Trim());
                                movement.unidadNoConvertibleId = Convert.ToInt32(reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_PD].ToString().Trim());
                                movement.observation = reader[LocalDatabase.CAMPO_OBSERVATION_PD].ToString().Trim();
                                movementsList.Add(movement);
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
            return movementsList;
        }

        public static int getLastId()
        {
            int lastId = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT MAX(" + LocalDatabase.CAMPO_ID_PD + ") FROM " + LocalDatabase.TABLA_PEDIDODETALLE + " ORDER BY " +
                    LocalDatabase.CAMPO_ID_PD;
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
                String query = "SELECT MAX(" + LocalDatabase.CAMPO_ID_PD + ") FROM " + LocalDatabase.TABLA_PEDIDODETALLE + " ORDER BY " +
                    LocalDatabase.CAMPO_ID_PD;
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

        public static int getIntValue(SQLiteConnection db, String query)
        {
            int value = 0;
            try
            {
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

            }
            return value;
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

        public static List<int> getIntListValue(SQLiteConnection db, String query)
        {
            List<int> listIds = null;
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            listIds = new List<int>();
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    listIds.Add(Convert.ToInt32(reader.GetValue(0).ToString().Trim()));
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
            return listIds;
        }

        public static int validateIfMovementExist(String query)
        {
            int id = 0;
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
                                    id = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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
            return id;
        }

        public static int validateIfMovementExist(SQLiteConnection db, String query)
        {
            int id = 0;
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    id = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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
            return id;
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
            try
            {
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

        public static Boolean deleteAllPedidosDetalleCotizacionesMostrador()
        {
            bool deleted = false;
            List<int> idsCotizacionesMostrador = PedidosEncabezadoModel.getAllIdsCotizacionesMostrador();
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                if (idsCotizacionesMostrador != null)
                {
                    int count = 0;
                    foreach (int idCotMos in idsCotizacionesMostrador)
                    {
                        String query = "DELETE FROM " + LocalDatabase.TABLA_PEDIDODETALLE + " WHERE " + LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_PD + " = " + idCotMos;
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            int deletedRecords = command.ExecuteNonQuery();
                            if (deletedRecords > 0)
                                count++;
                        }
                    }
                    if (idsCotizacionesMostrador.Count == count)
                        deleted = true;
                } else
                {
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


        public static bool deleteAllPDWhenTheirEncabezadosDoesNotExist()
        {
            bool deleted = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_PEDIDODETALLE;
                using (SQLiteCommand command = new SQLiteCommand(query, db)) {
                    using (SQLiteDataReader reader = command.ExecuteReader()) {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (!PedidosEncabezadoModel.documentExists(db, Convert.ToInt32(reader[LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_PD].ToString().Trim())))
                                {
                                    String queryDelete = "DELETE FROM " + LocalDatabase.TABLA_PEDIDODETALLE + " WHERE " +
                                        LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_PD + " = " + 
                                        Convert.ToInt32(reader[LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_PD].ToString().Trim());
                                    deleteARecord(db, queryDelete);
                                }
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            } catch (Exception ex) {
                SECUDOC.writeLog(ex.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return deleted;
        }

        public static bool deleteRecordsWithoutParameters(String query)
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
