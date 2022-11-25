using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Dynamic;
using System.Threading.Tasks;
using wsROMClase;
using wsROMClases;

namespace SyncTPV
{
    public class MovimientosModel
    {
        public static readonly int CALL_LOCAL_CANCELED = 1;
        public static readonly int CALL_SERVER_CANCELED = 2;
        public int id { set; get; }
        public int documentId { set; get; }
        public string itemCode { set; get; }
        public int itemId { set; get; }
        public String itemName { get; set; }
        public double baseUnits { set; get; }
        public double nonConvertibleUnits { get; set; }
        public double capturedUnits { get; set; }
        public int nonConvertibleUnitId { get; set; }
        public int capturedUnitId { get; set; }
        public int capturesUnitsType { get; set; }
        public double price { set; get; }
        public double monto { set; get; }
        public double total { set; get; }
        public int position { set; get; }
        public int documentType { set; get; }
        public string nameUser { set; get; }
        public string invoice { set; get; }
        public double descuentoPorcentaje { set; get; }
        public double descuentoImporte { set; get; }
        public string observations { set; get; }
        public double discount { get; set; }
        public int idDev { set; get; }
        public string comments { set; get; }
        public int userId { set; get; }
        public int enviadoAlWs { set; get; }
        public double rateDiscountPromo { get; set; }
        public int cancel { get; set; }

        public bool useFiscalField = false;
        public static MovimientosModel armMovement(ClsItemModel itemModel, int idDocumento, double unidadesBase,
                                                        double unidadesNoConvertibles, double unidadesCapturadas,
                                                        int unidadNoConvertibleId, int unidadCapturadaId,
                                                        int capturedUnitType, double price, double monto, double total,
                                                        int tipoDeDocumentoId, String nombreUsuario,
                                                        double descuentoEnPorcentaje, double descuentoEnImporte,
                                                        String observaciones, int idUsuario, double rateDiscountPromo)
        {
            if (descuentoEnPorcentaje >= rateDiscountPromo)
            {
                descuentoEnPorcentaje -= rateDiscountPromo;
            }
            MovimientosModel mdm = new MovimientosModel();
            mdm.documentId = idDocumento;
            mdm.itemCode = itemModel.codigo;
            mdm.itemId = itemModel.id;
            mdm.baseUnits = unidadesBase;
            mdm.nonConvertibleUnits = unidadesNoConvertibles;
            mdm.capturedUnits = unidadesCapturadas;
            mdm.nonConvertibleUnitId = unidadNoConvertibleId;
            mdm.capturedUnitId = unidadCapturadaId;
            mdm.capturesUnitsType = capturedUnitType;
            mdm.price = price;
            mdm.monto = monto;
            mdm.total = (double)decimal.Round((decimal)total, 2);
            int numMovimiento = MovimientosModel.getLastNumeroDeMovimiento(idDocumento);
            if (numMovimiento == 0)
                mdm.position = 1;
            else
            {
                numMovimiento++;
                mdm.position = numMovimiento;
            }
            mdm.documentType = tipoDeDocumentoId;
            mdm.nameUser = nombreUsuario;
            mdm.invoice = "N";
            mdm.descuentoPorcentaje = descuentoEnPorcentaje;
            mdm.descuentoImporte = descuentoEnImporte;
            mdm.observations = observaciones;
            mdm.idDev = 0;
            mdm.comments = observaciones;
            mdm.userId = idUsuario;
            mdm.rateDiscountPromo = rateDiscountPromo;
            mdm.itemName = itemModel.nombre;
            return mdm;
        }

        public static List<MovimientosModel> getAllMovementsFromADocument(int idDocument)
        {
            List<MovimientosModel> movesList = null;
            MovimientosModel movimientos = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " + LocalDatabase.CAMPO_DOCUMENTOID_MOV +
                    " = @idDocument";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idDocument", idDocument);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            movesList = new List<MovimientosModel>();
                            while (reader.Read())
                            {
                                movimientos = new MovimientosModel();
                                movimientos.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_MOV].ToString());
                                movimientos.itemCode = reader[LocalDatabase.CAMPO_CLAVEART_MOV].ToString();
                                movimientos.itemId = Convert.ToInt32(reader[LocalDatabase.CAMPO_ARTICULOID_MOV].ToString().Trim());
                                movimientos.nameUser = reader[LocalDatabase.CAMPO_NOMBREU_MOV].ToString();
                                movimientos.baseUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_BASEUNIT_MOV].ToString().Trim());
                                movimientos.nonConvertibleUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNIT_MOV].ToString().Trim());
                                movimientos.capturedUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_CAPTUREDUNIT_MOV].ToString());
                                movimientos.nonConvertibleUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_MOV].ToString().Trim());
                                movimientos.capturedUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CAPTUREDUNITID_MOV].ToString().Trim());
                                movimientos.descuentoPorcentaje = Convert.ToDouble(reader[LocalDatabase.CAMPO_DESCUENTOPOR_MOV].ToString().Trim());
                                movimientos.descuentoImporte = Convert.ToDouble(reader[LocalDatabase.CAMPO_DESCUENTOIMP_MOV].ToString().Trim());
                                if (reader[LocalDatabase.CAMPO_OBSERVACIONES_MOV] != DBNull.Value)
                                    movimientos.observations = reader[LocalDatabase.CAMPO_OBSERVACIONES_MOV].ToString().Trim();
                                else movimientos.observations = "";
                                movimientos.monto = Convert.ToDouble(reader[LocalDatabase.CAMPO_MONTO_MOV].ToString());
                                movimientos.price = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO_MOV].ToString());
                                movimientos.total = Convert.ToDouble(reader[LocalDatabase.CAMPO_TOTAL_MOV].ToString());
                                movesList.Add(movimientos);
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
            return movesList;
        }

        public static List<MovimientosModel> getAllMovements(String query)
        {
            List<MovimientosModel> movesList = null;
            MovimientosModel movimientos = null;
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
                            movesList = new List<MovimientosModel>();
                            while (reader.Read())
                            {
                                movimientos = new MovimientosModel();
                                movimientos.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_MOV].ToString());
                                movimientos.itemCode = reader[LocalDatabase.CAMPO_CLAVEART_MOV].ToString();
                                movimientos.itemId = Convert.ToInt32(reader[LocalDatabase.CAMPO_ARTICULOID_MOV].ToString().Trim());
                                movimientos.nameUser = reader[LocalDatabase.CAMPO_NOMBREU_MOV].ToString();
                                movimientos.baseUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_BASEUNIT_MOV].ToString().Trim());
                                movimientos.nonConvertibleUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNIT_MOV].ToString().Trim());
                                movimientos.capturedUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_CAPTUREDUNIT_MOV].ToString());
                                movimientos.nonConvertibleUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_MOV].ToString().Trim());
                                movimientos.capturedUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CAPTUREDUNITID_MOV].ToString().Trim());
                                movimientos.descuentoImporte = Convert.ToDouble(reader[LocalDatabase.CAMPO_DESCUENTOIMP_MOV].ToString().Trim());
                                if (reader[LocalDatabase.CAMPO_OBSERVACIONES_MOV] != DBNull.Value)
                                    movimientos.observations = reader[LocalDatabase.CAMPO_OBSERVACIONES_MOV].ToString().Trim();
                                else movimientos.observations = "";
                                movimientos.monto = Convert.ToDouble(reader[LocalDatabase.CAMPO_MONTO_MOV].ToString().Trim());
                                movimientos.price = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO_MOV].ToString().Trim());
                                movimientos.total = Convert.ToDouble(reader[LocalDatabase.CAMPO_TOTAL_MOV].ToString().Trim());
                                movesList.Add(movimientos);
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
            return movesList;
        }

        public static DataTable getAllMovementsFromADocumentDt(String query)
        {
            DataTable dt = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                    adapter.SelectCommand = command;
                    dt = new DataTable();
                    adapter.Fill(dt);
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
            return dt;
        }

        public static int addNewMovimiento(MovimientosModel mdm, double newAmountDiscount)
        {
            int resp = 0;
            var db = new SQLiteConnection();
            try {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "INSERT INTO " + LocalDatabase.TABLA_MOVIMIENTO + " VALUES (@id, @documentId, @itemCode, @itemId, @unidadesBase, @unidadesNoConvertibles, " +
                        "@unidadesCapturadas, @unidadesNoConvertiblesId, @unidadesCapturadasId, @capturesUnitsType, @precio, @monto, @total, @posicion, @tipoDocumentoId, " +
                        "@nombreUsuario, @factura, @descuentoPorcentaje, @descuentoImporte, @observaciones, @idDev, @comentario, @usuarioId, @enviadoAlWs, " +
                        "@rateDiscountPromo, @cancel)";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int lastId = getLastId() + 1;
                    command.Parameters.AddWithValue("@id", lastId);
                    command.Parameters.AddWithValue("@documentId", mdm.documentId);
                    command.Parameters.AddWithValue("@itemCode", mdm.itemCode);
                    command.Parameters.AddWithValue("@itemId", mdm.itemId);
                    command.Parameters.AddWithValue("@unidadesBase", mdm.baseUnits);
                    command.Parameters.AddWithValue("@unidadesNoConvertibles", mdm.nonConvertibleUnits);
                    command.Parameters.AddWithValue("@unidadesCapturadas", mdm.capturedUnits);
                    command.Parameters.AddWithValue("@unidadesNoConvertiblesId", mdm.nonConvertibleUnitId);
                    command.Parameters.AddWithValue("@unidadesCapturadasId", mdm.capturedUnitId);
                    command.Parameters.AddWithValue("@capturesUnitsType", mdm.capturesUnitsType);
                    command.Parameters.AddWithValue("@precio", mdm.price);
                    command.Parameters.AddWithValue("@monto", mdm.monto);
                    command.Parameters.AddWithValue("@total", mdm.total);
                    command.Parameters.AddWithValue("@posicion", mdm.position);
                    command.Parameters.AddWithValue("@tipoDocumentoId", mdm.documentType);
                    command.Parameters.AddWithValue("@nombreUsuario", mdm.nameUser);
                    command.Parameters.AddWithValue("@factura", mdm.invoice);
                    command.Parameters.AddWithValue("@descuentoPorcentaje", mdm.descuentoPorcentaje);
                    command.Parameters.AddWithValue("@descuentoImporte", newAmountDiscount);
                    command.Parameters.AddWithValue("@observaciones", mdm.observations);
                    command.Parameters.AddWithValue("@idDev", mdm.idDev);
                    command.Parameters.AddWithValue("@comentario", mdm.comments);
                    command.Parameters.AddWithValue("@usuarioId", mdm.userId);
                    command.Parameters.AddWithValue("@enviadoAlWs", 0);
                    command.Parameters.AddWithValue("@rateDiscountPromo", mdm.rateDiscountPromo);
                    command.Parameters.AddWithValue("@cancel", 0);
                    int recordSaved = command.ExecuteNonQuery();
                    if (recordSaved > 0)
                        resp = lastId;
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

        public static int getLastId()
        {
            int lastId = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_ID_MOV + " FROM " + LocalDatabase.TABLA_MOVIMIENTO + " ORDER BY " +
                    LocalDatabase.CAMPO_ID_MOV + " DESC LIMIT 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                lastId = reader.GetInt32(0);
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
            return lastId;
        }

        public static int existeUnMovimientoConElMismoPrecioYDescuento(SQLiteConnection db, int idDocument, int idItem,
                                                                   double price, double discount)
        {
            int records = 0;
            try
            {
                String query = "SELECT COUNT(" + LocalDatabase.CAMPO_ID_MOV + ") FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " +
                        LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " + idDocument +
                        " AND " + LocalDatabase.CAMPO_ARTICULOID_MOV + " = " + idItem +
                        " AND " + LocalDatabase.CAMPO_PRECIO_MOV + " = " + price +
                        " AND " + LocalDatabase.CAMPO_DESCUENTOIMP_MOV + " = " + discount;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    records = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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
            return records;
        }

        public static int getLastNumeroDeMovimiento(int id)
        {
            int movimiento = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_POSICION_MOV + " FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " +
                    LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " + id + " ORDER BY " + LocalDatabase.CAMPO_POSICION_MOV + " DESC LIMIT 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                movimiento = reader.GetInt32(0);
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
            return movimiento;
        }

        public static int getIntValue(String query)
        {
            int movimiento = 0;
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
                                    movimiento = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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
            return movimiento;
        }

        public static double getTotalCapturedUnitsFromADocument(int idDocumento)
        {
            double unidades = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT SUM(" + LocalDatabase.CAMPO_CAPTUREDUNIT_MOV + ") FROM " + LocalDatabase.TABLA_MOVIMIENTO +
                    " WHERE " + LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " + idDocumento;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    unidades = Convert.ToDouble(reader.GetValue(0).ToString().Trim());
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
            return unidades;
        }

        public static int getTotalNumberOfMovimientos(int idDocumento)
        {
            int value = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT COUNT(*) FROM "+LocalDatabase.TABLA_MOVIMIENTO+" WHERE "+
                    LocalDatabase.CAMPO_DOCUMENTOID_MOV+ " = @idDocumento";
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
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return value;
        }

        public static int getTotalNumberOfMovimientosFiscalesDiferentesAlQueIntentamosAgregar(int idDocumento, int fiscalONoFiscal, String fiscalField, bool serverLan)
        {
            if (serverLan)
        {
                int value = 0;
                var db = new SQLiteConnection();
                List<String> listaCod = null;
                String listain = "";
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    String query1 = "SELECT * FROM Movimientos where DOCTO_ID_PEDIDO = @idDoc";
                    using (SQLiteCommand command = new SQLiteCommand(query1, db))
                    {
                        command.Parameters.AddWithValue("@idDoc", idDocumento);
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                listaCod = new List<string>();
                                while (reader.Read())
                                {
                                    String codArticulo = reader["CLAVE_ART"].ToString().Trim();
                                    listaCod.Add(codArticulo);
                                }
                            }
                            if (reader != null && !reader.IsClosed)
                                reader.Close();
                        }
                    }
                    if(listaCod != null)
                    {
                        if (listaCod.Count > 1)
                        {
                            listain = "'" + listaCod[0] + "'";
                            for (int i = 1; i < listaCod.Count; i++)
                            {
                                listain = listain + ",'" + listaCod[0] + "'";
                            }
                        }
                        else if (listaCod.Count == 1)
                        {
                            listain = "'" + listaCod[0] + "'";
                        }
                        else
                        {

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
                String campoComercialFiscal = "";
                switch (fiscalField)
                {
                    case "textoExtra1":
                        campoComercialFiscal = "CTEXTOEXTRA1";
                        break;
                    case "textoExtra2":
                        campoComercialFiscal = "CTEXTOEXTRA2";
                        break;
                    case "textoExtra3":
                        campoComercialFiscal = "CTEXTOEXTRA3";
                        break;
                    case "importeExtra1":
                        campoComercialFiscal = "CIMPORTEEXTRA1";
                        break;
                    case "importeExtra2":
                        campoComercialFiscal = "CIMPORTEEXTRA2";
                        break;
                    case "importeExtra3":
                        campoComercialFiscal = "CIMPORTEEXTRA3";
                        break;
                    default:
                        campoComercialFiscal = "CIMPORTEEXTRA4";
                        break;
                }
                value = -1;
                //aqui me quede > select count(*) from admProductos p where p.CCODIGOPRODUCTO in ('TPV2566','PROD002','PRUEBA0003','TPV2566','TPV2566') and (CIMPORTEEXTRA4 != 0 and CIMPORTEEXTRA4 != '0')
                if (listain.Equals(""))
                {
                    value = 0;
                }
                else
                {
                    String query = "select count(*) from admProductos p where p.CCODIGOPRODUCTO in (" + listain + ") and (" + campoComercialFiscal + " != " + fiscalONoFiscal + ")";
                    String comInstance = InstanceSQLSEModel.getStringComInstance();

                    var dbCom = new System.Data.SqlClient.SqlConnection();
                    dbCom.ConnectionString = comInstance;
                    dbCom.Open();
                    try
                    {
                        using (SqlCommand commandItem = new SqlCommand(query, dbCom))
                        {
                            using (SqlDataReader reader = commandItem.ExecuteReader())
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
                }
                return value;
                
            }
            else
            {
                int value = 0;
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    String query = "SELECT COUNT(*) FROM Movimientos M " +
                        "INNER JOIN Item I ON M.CLAVE_ART = I.CODE " +
                        "INNER JOIN Documentos D ON M.DOCTO_ID_PEDIDO = D.id " +
                        "WHERE I." + fiscalField + " != " + fiscalONoFiscal + " AND D.id = @idDocumento";
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
                    if (db != null && db.State == ConnectionState.Open)
                        db.Close();
                }
                return value;
            }
           

        }
        

        public static bool getMovimientosFiscalesDiferentesLAN(int idDocumento, String fiscalField, bool serverLan)
        {
            bool value = true;
            if (serverLan)
            {
                var db = new SQLiteConnection();
                List<String> listaCod = null;
                String listain = "";
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    String query1 = "SELECT * FROM Movimientos where DOCTO_ID_PEDIDO = @idDoc";
                    using (SQLiteCommand command = new SQLiteCommand(query1, db))
                    {
                        command.Parameters.AddWithValue("@idDoc", idDocumento);
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                listaCod = new List<string>();
                                while (reader.Read())
                                {
                                    String codArticulo = reader["CLAVE_ART"].ToString().Trim();
                                    listaCod.Add(codArticulo);
                                }
                            }
                            if (reader != null && !reader.IsClosed)
                                reader.Close();
                        }
                    }
                    if (listaCod != null)
                    {
                        if (listaCod.Count > 1)
                        {
                            listain = "'" + listaCod[0] + "'";
                            for (int i = 1; i < listaCod.Count; i++)
                            {
                                listain = listain + ",'" + listaCod[i] + "'";
                            }
                        }
                        else if (listaCod.Count == 1)
                        {
                            listain = "'" + listaCod[0] + "'";
                        }
                        else
                        {

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
                String campoComercialFiscal = "";
                switch (fiscalField)
                {
                    case "textoExtra1":
                        campoComercialFiscal = "CTEXTOEXTRA1";
                        break;
                    case "textoExtra2":
                        campoComercialFiscal = "CTEXTOEXTRA2";
                        break;
                    case "textoExtra3":
                        campoComercialFiscal = "CTEXTOEXTRA3";
                        break;
                    case "importeExtra1":
                        campoComercialFiscal = "CIMPORTEEXTRA1";
                        break;
                    case "importeExtra2":
                        campoComercialFiscal = "CIMPORTEEXTRA2";
                        break;
                    case "importeExtra3":
                        campoComercialFiscal = "CIMPORTEEXTRA3";
                        break;
                    default:
                        campoComercialFiscal = "CIMPORTEEXTRA4";
                        break;
                }
                value = true;
                //aqui me quede > select count(*) from admProductos p where p.CCODIGOPRODUCTO in ('TPV2566','PROD002','PRUEBA0003','TPV2566','TPV2566') and (CIMPORTEEXTRA4 != 0 and CIMPORTEEXTRA4 != '0')
                if (listain.Equals(""))
                {
                    value = false;
                }
                else
                {
                    String query1 = "select count(*) from admProductos p where p.CCODIGOPRODUCTO in (" + listain + ") and (" + campoComercialFiscal + " = 0)";
                    String query2 = "select count(*) from admProductos p where p.CCODIGOPRODUCTO in (" + listain + ") and (" + campoComercialFiscal + " = 1)";
                    String comInstance = InstanceSQLSEModel.getStringComInstance();

                    var dbCom = new System.Data.SqlClient.SqlConnection();
                    dbCom.ConnectionString = comInstance;
                    dbCom.Open();
                    int fiscal = 0;
                    int Nfiscal = 0;
                    try
                    {
                        using (SqlCommand commandItem = new SqlCommand(query1, dbCom))
                        {
                            using (SqlDataReader reader = commandItem.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {

                                    while (reader.Read())
                                    {
                                        if (reader.GetValue(0) != DBNull.Value)
                                        {
                                            Nfiscal = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
                                            
                                        }
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
                    try
                    {
                        using (SqlCommand commandItem = new SqlCommand(query2, dbCom))
                        {
                            using (SqlDataReader reader = commandItem.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {

                                    while (reader.Read())
                                    {
                                        if (reader.GetValue(0) != DBNull.Value)
                                        {
                                            fiscal = Convert.ToInt32(reader.GetValue(0).ToString().Trim());

                                        }
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
                    if(fiscal==0 || Nfiscal == 0)
                    {
                        if(fiscal == 0 && Nfiscal == 0)
                        {
                            value = false;
                        }
                        else
                        {
                            value = false;
                        }
                    }
                    else
                    {
                        value = true;
                    }
                }
                

            }
            else
            {
                
            }
            return value;
        }

        public static int decreaseOrIncreaseExistenceOfAnItem(int idDoc, ClsItemModel itemModel, double capturedUnits,
                                                          int capturedUnitId, double precio, double descuentoI, int tipoDoc, int idMovimiento,
                                                          bool serverModeLAN)
        {
            int resp = 0;
            double nuevaExistencia = 0;
            MovimientosModel mdm = MovimientosModel.getAMovement("SELECT * FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " + 
                LocalDatabase.CAMPO_ID_MOV + " = " + idMovimiento);
            if (mdm != null)
            {
                if (tipoDoc == DocumentModel.TIPO_VENTA || tipoDoc == DocumentModel.TIPO_REMISION)
                {
                    /** Disminuir existencias documentos de Venta y Ventas TPV */
                    double existenciaActual = itemModel.existencia;//ItemModel.getExistenciaForAItemActualizarDatos(idItem);
                    //int baseUnitId = ItemModel.getBaseUnitId(idItem);
                    if (itemModel.baseUnitId != capturedUnitId)
                    {
                        int capturedUnitIsMajorThanTheBase = ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(itemModel.baseUnitId, capturedUnitId);
                        if (capturedUnitIsMajorThanTheBase == 0)
                        {
                            /** Unidad de venta es menor: multiplicamos la base por el numero de conversión mayor */
                            double majorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, true);
                            double stockInMinorUnits = existenciaActual * majorConversion;
                            double newStockInMinorUnits = stockInMinorUnits - capturedUnits;
                            nuevaExistencia = newStockInMinorUnits / majorConversion;
                        }
                        else if (capturedUnitIsMajorThanTheBase == 1)
                        {
                            /** Unidad de venta es mayor: multiplicamos la base por el numero de conversión mayor */
                            if (itemModel.salesUnitId != capturedUnitId)
                            {
                                double majorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, true);
                                double newCapturedUnits = capturedUnits * majorConversion;
                                nuevaExistencia = existenciaActual - newCapturedUnits;
                            }
                            else
                            {
                                double minorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, false);
                                double newCapturedUnits = capturedUnits / minorConversion;
                                nuevaExistencia = existenciaActual - newCapturedUnits;
                            }
                        }
                        else if (capturedUnitIsMajorThanTheBase == 2)
                        {
                            /** Unidad de venta es igual a la unidad base */
                            nuevaExistencia = (existenciaActual - capturedUnits);
                        }
                        else
                        {
                            nuevaExistencia = (existenciaActual - capturedUnits);
                        }
                    }
                    else
                    {
                        nuevaExistencia = (existenciaActual - capturedUnits);
                    }
                    if (serverModeLAN)
                    {
                        resp = 1;
                    }
                    else
                    {
                        if (ItemModel.changeExistenceToAnItem(itemModel.id, nuevaExistencia))
                        {
                            resp = 1;
                        }
                    }
                }
                else if (tipoDoc == DocumentModel.TIPO_DEVOLUCION)
                {
                    /** Aumentar exitencias documentos de devolución */
                    double currentExistance = itemModel.existencia;//ItemModel.getExistenciaForAItemActualizarDatos(idItem);
                    if (itemModel.baseUnitId != capturedUnitId)
                    {
                        int capturedUnitIsMajor = ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(itemModel.baseUnitId, capturedUnitId);
                        if (capturedUnitIsMajor == 0)
                        {
                            /** Unidad de venta es menor: multiplicamos la base por el numero de conversión mayor */
                            double majorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, true);
                            double stockInMinorUnits = currentExistance * majorConversion;
                            double newStockInMinorUnits = stockInMinorUnits + capturedUnits;
                            nuevaExistencia = newStockInMinorUnits / majorConversion;
                        }
                        else if (capturedUnitIsMajor == 1)
                        {
                            /** Unidad de venta es mayor: multiplicamos la base por el numero de conversión mayor */
                            double minorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, false);
                            double newCapturedUnits = capturedUnits / minorConversion;
                            nuevaExistencia = currentExistance + newCapturedUnits;
                        }
                        else if (capturedUnitIsMajor == 2)
                        {
                            /** Unidad de venta es igual a la unidad base */
                            nuevaExistencia = (currentExistance + capturedUnits);
                        }
                        else
                        {
                            nuevaExistencia = (currentExistance + capturedUnits);
                        }
                    }
                    else
                    {
                        nuevaExistencia = (currentExistance + capturedUnits);
                    }
                    if (serverModeLAN)
                    {
                        resp = 1;
                    }
                    else
                    {
                        if (ItemModel.changeExistenceToAnItem(itemModel.id, nuevaExistencia))
                        {
                            resp = 1;
                        }
                    }
                }
                else
                {
                    /** No realizar cambios en existencia a documentos Cotización y Pedido */
                    resp = -1;
                }
            }
            return resp;
        }

        public static Boolean checkIfThereAreStillMovementsForTheDocumentInShift(int idDoc)
        {
            Boolean thereAre = false;
            var db = new SQLiteConnection();            
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " +
                        LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " + idDoc;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            thereAre = true;
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
            return thereAre;
        }

        public static Boolean checkIfThereAreStillMovementsForTheDocumentInShift(SQLiteConnection db, int idDoc)
        {
            Boolean thereAre = false;
            try
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " +
                        LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " + idDoc;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            thereAre = true;
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
            return thereAre;
        }

        public static int cancelMovementsOfDocuments(int idDoc, int callMade, int documentType, bool serverModeLAN)
        {
            int todosEliminados = 0;
            int movsAEliminar = 0;
            int contador = 0;
            List<MovimientosModel> movementsList = null;
            if (documentType == DocumentModel.TIPO_VENTA || documentType == DocumentModel.TIPO_REMISION)
            {
                /** Documentos de tipo Venta ROM y TPV */
                movementsList = MovimientosModel.getMovimientosOfTheCurrentDocument(idDoc);
                if (movementsList != null && movementsList.Count > 0)
                {
                    movsAEliminar = movementsList.Count;
                    if (serverModeLAN)
                    {
                        contador = MovimientosModel.getTotalNumberOfMovimientos(idDoc);
                        for (int i = 0; i < movementsList.Count; i++)
                        {
                            string query = "DELETE FROM " + LocalDatabase.TABLA_PESO + " WHERE " + LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = " +
                                movementsList[i].id;
                            WeightModel.createUpdateOrDelete(query);
                        }
                        MovimientosModel.deleteAllMovementFromADocument(idDoc);
                    } else
                    {
                        for (int i = 0; i < movsAEliminar; i++)
                        {
                            string query = "DELETE FROM " + LocalDatabase.TABLA_PESO + " WHERE " + LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = " +
                                movementsList[i].id;
                            WeightModel.createUpdateOrDelete(query);
                            if (callMade == CALL_LOCAL_CANCELED)
                            {
                                double unidadesCapturadas = MovimientosModel.getCapturedUnitsOfAMove(movementsList[i].id, idDoc);
                                if (unidadesCapturadas > 0)
                                {
                                    double existenciaActual = ItemModel.getTheCurrentExistenceOfAnItem(movementsList[i].itemId);
                                    int baseUnitId = ItemModel.getBaseUnitId(movementsList[i].itemId);
                                    int capturedUnitId = MovimientosModel.getCapturedUnitId(movementsList[i].id);
                                    double nuevaExistencia = 0;
                                    if (baseUnitId != capturedUnitId)
                                    {
                                        int capturedUnitIsMajor = ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(baseUnitId, capturedUnitId);
                                        if (capturedUnitIsMajor == 0)
                                        {
                                            /** Unidad de venta es menor: multiplicamos la base por el numero de conversión mayor */
                                            double majorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(baseUnitId, capturedUnitId, true);
                                            double stockInMinorUnits = existenciaActual * majorConversion;
                                            double newStockInMinorUnits = stockInMinorUnits + unidadesCapturadas;
                                            nuevaExistencia = newStockInMinorUnits / majorConversion;
                                        }
                                        else if (capturedUnitIsMajor == 1)
                                        {
                                            /**Unidad de venta es mayor: multiplicamos la base por el numero de conversión mayor */
                                            double minorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(baseUnitId, capturedUnitId, false);
                                            double baseUnits = unidadesCapturadas / minorConversion;
                                            nuevaExistencia = existenciaActual + baseUnits;
                                        }
                                        else if (capturedUnitIsMajor == 2)
                                        {
                                            nuevaExistencia = existenciaActual + unidadesCapturadas;
                                        }
                                        else
                                        {
                                            nuevaExistencia = existenciaActual + unidadesCapturadas;
                                        }
                                    }
                                    else
                                    {
                                        nuevaExistencia = unidadesCapturadas + existenciaActual;
                                    }
                                    if (ItemModel.changeTheExistenceOfAnItem(movementsList[i].itemId, nuevaExistencia))
                                    {
                                        if (MovimientosModel.deleteAMoveByIdMovimiento(idDoc, movementsList[i].id))
                                        {
                                            contador += 1;
                                        }
                                        else
                                        {
                                            SECUDOC.writeLog("Movimiento número: " +
                                                    movementsList[i].position + ", No se pudo eliminar! ");
                                        }
                                    }
                                    else
                                    {
                                        if (MovimientosModel.deleteAMoveByIdMovimiento(idDoc, movementsList[i].id))
                                        {
                                            contador += 1;
                                        }
                                        else
                                        {
                                            SECUDOC.writeLog("Movimiento número: " +
                                                    movementsList[i].position + ", No se pudo eliminar! ");
                                        }
                                    }
                                }
                                else
                                {
                                    if (MovimientosModel.deleteAMoveByIdMovimiento(idDoc, movementsList[i].id))
                                    {
                                        contador += 1;
                                    }
                                }
                            }
                            else if (callMade == CALL_SERVER_CANCELED)
                            {
                                if (MovimientosModel.deleteAMoveByIdMovimiento(idDoc, movementsList[i].id))
                                {
                                    contador += 1;
                                }
                                else
                                {
                                    SECUDOC.writeLog("Movimiento número: " +
                                                    movementsList[i].position + ", No se pudo eliminar! ");
                                }
                            }
                        }
                    }
                }
            }
            else if (documentType == DocumentModel.TIPO_DEVOLUCION)
            {
                if (serverModeLAN)
                {
                    contador = MovimientosModel.getTotalNumberOfMovimientos(idDoc);
                    movsAEliminar = contador;
                    MovimientosModel.deleteAllMovementFromADocument(idDoc);
                } else
                {
                    /** Documentos de tipo Devolución */
                    movementsList = MovimientosModel.getMovimientosOfTheCurrentDocument(idDoc);
                    if (movementsList != null)
                    {
                        movsAEliminar = movementsList.Count;
                        for (int i = 0; i < movementsList.Count; i++)
                        {
                            if (callMade == CALL_LOCAL_CANCELED)
                            {
                                double unidadesCapturadas = MovimientosModel.getCapturedUnitsOfAMove(movementsList[i].id, idDoc);
                                if (unidadesCapturadas > 0)
                                {
                                    double existenciaActual = ItemModel.getTheCurrentExistenceOfAnItem(movementsList[i].itemId);
                                    int baseUnitId = ItemModel.getBaseUnitId(movementsList[i].itemId);
                                    int capturedUnitId = MovimientosModel.getCapturedUnitId(movementsList[i].id);
                                    double nuevaExistencia = 0;
                                    if (baseUnitId != capturedUnitId)
                                    {
                                        int capturedUnitIsMajor = ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(baseUnitId, capturedUnitId);
                                        if (capturedUnitIsMajor == 0)
                                        {
                                            /** Unidad de venta es menor: multiplicamos la base por el numero de conversión mayor */
                                            double majorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(baseUnitId, capturedUnitId, true);
                                            double stockInMinorUnits = existenciaActual * majorConversion;
                                            double newStockInMinorUnits = stockInMinorUnits - unidadesCapturadas;
                                            nuevaExistencia = newStockInMinorUnits / majorConversion;
                                        }
                                        else if (capturedUnitIsMajor == 1)
                                        {
                                            /**Unidad de venta es mayor: multiplicamos la base por el numero de conversión mayor */
                                            double minorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(baseUnitId, capturedUnitId, false);
                                            double baseUnits = unidadesCapturadas / minorConversion;
                                            nuevaExistencia = existenciaActual - baseUnits;
                                        }
                                        else if (capturedUnitIsMajor == 2)
                                        {
                                            nuevaExistencia = existenciaActual - unidadesCapturadas;
                                        }
                                        else
                                        {
                                            nuevaExistencia = existenciaActual - unidadesCapturadas;
                                        }
                                    }
                                    else
                                    {
                                        nuevaExistencia = existenciaActual - unidadesCapturadas;
                                    }
                                    if (ItemModel.changeTheExistenceOfAnItem(movementsList[i].itemId, nuevaExistencia))
                                    {
                                        if (MovimientosModel.deleteAMoveByIdMovimiento(idDoc, movementsList[i].id))
                                        {
                                            contador += 1;
                                        }
                                        else
                                        {
                                            SECUDOC.writeLog("Movimiento número: " +
                                                    movementsList[i].position + ", No se pudo eliminar! ");
                                        }
                                    }
                                    else
                                    {
                                        SECUDOC.writeLog("Fallo al sumar la existencia del Articulo: " +
                                                movementsList[i].itemId);
                                    }
                                }
                                else
                                {
                                    if (MovimientosModel.deleteAMoveByIdMovimiento(idDoc, movementsList[i].id))
                                    {
                                        contador += 1;
                                    }
                                }
                            }
                            else if (callMade == CALL_SERVER_CANCELED)
                            {
                                if (MovimientosModel.deleteAMoveByIdMovimiento(idDoc, movementsList[i].id))
                                {
                                    contador += 1;
                                }
                                else
                                {
                                    SECUDOC.writeLog("Movimiento número: " +
                                                    movementsList[i].position + ", No se pudo eliminar! ");
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (serverModeLAN)
                {
                    contador = MovimientosModel.getTotalNumberOfMovimientos(idDoc);
                    movsAEliminar = contador;
                    MovimientosModel.deleteAllMovementFromADocument(idDoc);
                } else
                {
                    contador = MovimientosModel.getTotalNumberOfMovimientos(idDoc);
                    movsAEliminar = contador;
                    MovimientosModel.deleteAllMovementFromADocument(idDoc);
                }
            }
            if (movsAEliminar == contador)
            {
                todosEliminados = 1;
            }
            return todosEliminados;
        }

        public static List<MovimientosModel> getMovimientosOfTheCurrentDocument(int id)
        {
            List<MovimientosModel> listaMovsOfThedocto = null;
            MovimientosModel mcm;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " +
                        LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " + id;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            listaMovsOfThedocto = new List<MovimientosModel>();
                            while (reader.Read())
                            {
                                mcm = new MovimientosModel();
                                mcm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_MOV].ToString().Trim());
                                mcm.documentId = Convert.ToInt32(reader[LocalDatabase.CAMPO_DOCUMENTOID_MOV].ToString().Trim());
                                mcm.itemCode = reader[LocalDatabase.CAMPO_CLAVEART_MOV].ToString().Trim();
                                mcm.itemId = Convert.ToInt32(reader[LocalDatabase.CAMPO_ARTICULOID_MOV].ToString().Trim());
                                mcm.baseUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_BASEUNIT_MOV].ToString().Trim());
                                mcm.nonConvertibleUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNIT_MOV].ToString().Trim());
                                mcm.capturedUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_CAPTUREDUNIT_MOV].ToString().Trim());
                                mcm.nonConvertibleUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_MOV].ToString().Trim());
                                mcm.capturedUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CAPTUREDUNITID_MOV].ToString().Trim());
                                mcm.capturesUnitsType = Convert.ToInt32(reader[LocalDatabase.CAMPO_CAPTUREDUNITTYPE_MOV].ToString().Trim());
                                mcm.price = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO_MOV].ToString().Trim());
                                mcm.monto = Convert.ToDouble(reader[LocalDatabase.CAMPO_MONTO_MOV].ToString().Trim());
                                mcm.total = Convert.ToDouble(reader[LocalDatabase.CAMPO_TOTAL_MOV].ToString().Trim());
                                mcm.position = Convert.ToInt32(reader[LocalDatabase.CAMPO_POSICION_MOV].ToString().Trim());
                                mcm.documentType = Convert.ToInt32(reader[LocalDatabase.CAMPO_TIPODOCUMENTO_MOV].ToString().Trim());
                                mcm.nameUser = reader[LocalDatabase.CAMPO_NOMBREU_MOV].ToString().Trim();
                                mcm.invoice = reader[LocalDatabase.CAMPO_FACTURA_MOV].ToString().Trim();
                                mcm.descuentoPorcentaje = Convert.ToDouble(reader[LocalDatabase.CAMPO_DESCUENTOPOR_MOV].ToString().Trim());
                                mcm.descuentoImporte = Convert.ToDouble(reader[LocalDatabase.CAMPO_DESCUENTOIMP_MOV].ToString().Trim());
                                mcm.observations = reader[LocalDatabase.CAMPO_OBSERVACIONES_MOV].ToString().Trim();
                                mcm.idDev = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDDEV_MOV].ToString().Trim());
                                mcm.comments = reader[LocalDatabase.CAMPO_COMENTARIO_MOV].ToString().Trim();
                                mcm.userId = Convert.ToInt32(reader[LocalDatabase.CAMPO_USUARIOID_MOV].ToString().Trim());
                                mcm.enviadoAlWs = Convert.ToInt32(reader[LocalDatabase.CAMPO_ENVIADOALWS_MOV].ToString().Trim());
                                mcm.rateDiscountPromo = Convert.ToDouble(reader[LocalDatabase.CAMPO_RATEDISCOUNTPROMO_MOV].ToString().Trim());
                                listaMovsOfThedocto.Add(mcm);
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
            return listaMovsOfThedocto;
        }

        public static double getCapturedUnitsOfAMove(int idMovement, int idDoc)
        {
            double capturedUnits = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_CAPTUREDUNIT_MOV + " FROM " +
                        LocalDatabase.TABLA_MOVIMIENTO + " WHERE " + LocalDatabase.CAMPO_ID_MOV+" = "+ idMovement +
                        " AND " + LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " + idDoc;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader[LocalDatabase.CAMPO_CAPTUREDUNIT_MOV] != DBNull.Value)
                                    capturedUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_CAPTUREDUNIT_MOV].ToString().Trim());
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
            return capturedUnits;
        }

        public static int getCapturedUnitId(int idMovement)
        {
            int capturedUnitId = 0;
            var db = new SQLiteConnection();            
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_CAPTUREDUNITID_MOV + " FROM " +
                        LocalDatabase.TABLA_MOVIMIENTO + " WHERE " + LocalDatabase.CAMPO_ID_MOV + " = " + idMovement;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                capturedUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CAPTUREDUNITID_MOV].ToString().Trim());
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
            return capturedUnitId;
        }

        public static Boolean deleteAMoveByIdMovimiento(int idDocumento, int idMovimiento)
        {
            Boolean deleted = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "DELETE FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " + LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = @idDocumento" +
                    " AND " + LocalDatabase.CAMPO_ID_MOV + " = @idMovimiento";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idDocumento", idDocumento);
                    command.Parameters.AddWithValue("@idMovimiento", idMovimiento);
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

        public static Boolean deleteAllMovementFromADocument(int idDocumento)
        {
            Boolean deleted = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "DELETE FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " + LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = @idDocumento";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idDocumento", idDocumento);
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

        public static Boolean createUpdateOrDeleteRecords(String query)
        {
            Boolean deleted = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
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

        public static int getIdMovement(int idItem, int position, int idDocument)
        {
            int id = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_ID_MOV + " FROM " +
                        LocalDatabase.TABLA_MOVIMIENTO + " WHERE " +
                        LocalDatabase.CAMPO_ARTICULOID_MOV + " = " + idItem + " AND " +
                        LocalDatabase.CAMPO_POSICION_MOV + " = " + position+ " AND "+
                        LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = "+idDocument;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_MOV].ToString().Trim());
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
            return id;
        }


        public static int getPositionFromTheFirstItem(int idMovement, int idDocument)
        {
            int position = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_POSICION_MOV + " FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " +
                    LocalDatabase.CAMPO_ID_MOV + " = @idMovement AND " + LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = @idDocument";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idMovement", idMovement);
                    command.Parameters.AddWithValue("@idDocument", idDocument);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader[LocalDatabase.CAMPO_POSICION_MOV] != DBNull.Value)
                                    position = Convert.ToInt32(reader[LocalDatabase.CAMPO_POSICION_MOV].ToString().Trim());
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
            return position;
        }

        public static async Task<Boolean> removeOrAddQuantityToAMovement(int idMovement, int position, ClsItemModel itemModel,
                                                         double capturedUnits, int idDocumento,
                                                         int addOrSubstrac, int actualizar, double newPrice,
                                                         double capturedNonConvertibleUnits, double discountRate, bool serverModeLAN,
                                                         int capturedUnitId, double baseUnits) {
            Boolean removed = false;
            double lastCapturedUnits = getCapturedUnitsOfAMove(idMovement, idDocumento);
            
            
            
            
            
            if (await subtractOrAddAndRecalculateSalesMovement(idMovement, itemModel, position, capturedUnits, 
                addOrSubstrac, actualizar, newPrice, capturedNonConvertibleUnits, discountRate, capturedUnitId, baseUnits))
            {
                int tipo = DocumentModel.getDocumentType(idDocumento);
                double newExistence = 0;
                if (tipo == DocumentModel.TIPO_VENTA || tipo == DocumentModel.TIPO_REMISION)
                {
                    if (serverModeLAN)
                    {
                        removed = true;
                    } else
                    {
                        double currentExistence = itemModel.existencia;// ItemModel.getTheCurrentExistenceOfAnItem(idArt);
                        if (itemModel.baseUnitId != capturedUnitId)
                        {
                            int capturedUnitIsMajor = ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(itemModel.baseUnitId, capturedUnitId);
                            if (capturedUnitIsMajor == 0)
                            {
                                /** Unidad de venta es menor: multiplicamos la base por el numero de conversión mayor */
                                double majorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, true);
                                double stockInMinorUnits = currentExistence * majorConversion;
                                double newStockInMinorUnits = 0;
                                if (addOrSubstrac == 0)
                                {
                                    stockInMinorUnits = stockInMinorUnits + lastCapturedUnits;
                                    newStockInMinorUnits = stockInMinorUnits + capturedUnits;
                                }
                                else
                                {
                                    stockInMinorUnits = stockInMinorUnits + lastCapturedUnits;
                                    newStockInMinorUnits = stockInMinorUnits - capturedUnits;
                                }
                                newExistence = newStockInMinorUnits / majorConversion;
                            }
                            else if (capturedUnitIsMajor == 1)
                            {
                                /** Unidad de venta es mayor: multiplicamos la base por el numero de conversión mayor */
                                double minorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, false);
                                double newCapturedUnits = capturedUnits / minorConversion;
                                if (addOrSubstrac == 0)
                                {
                                    currentExistence = currentExistence + lastCapturedUnits;
                                    newExistence = currentExistence + newCapturedUnits;
                                }
                                else
                                {
                                    currentExistence = currentExistence + lastCapturedUnits;
                                    newExistence = currentExistence - newCapturedUnits;
                                }
                            }
                            else if (capturedUnitIsMajor == 2)
                            {
                                /** Unidad de venta es igual a la unidad base */
                                if (addOrSubstrac == 0)
                                {
                                    currentExistence = (currentExistence + lastCapturedUnits);
                                    newExistence = (currentExistence + capturedUnits);
                                }
                                else
                                {
                                    currentExistence = (currentExistence + lastCapturedUnits);
                                    newExistence = (currentExistence - capturedUnits);
                                }
                            }
                            else
                            {
                                if (addOrSubstrac == 0)
                                {
                                    currentExistence = (currentExistence + lastCapturedUnits);
                                    newExistence = (currentExistence + capturedUnits);
                                }
                                else
                                {
                                    currentExistence = (currentExistence + lastCapturedUnits);
                                    newExistence = (currentExistence - capturedUnits);
                                }
                            }
                        }
                        else
                        {
                            if (addOrSubstrac == 0)
                            {
                                currentExistence = (currentExistence + lastCapturedUnits);
                                newExistence = (currentExistence + capturedUnits);
                            }
                            else
                            {
                                currentExistence = (currentExistence + lastCapturedUnits);
                                newExistence = (currentExistence - capturedUnits);
                            }
                        }
                        if (ItemModel.changeTheExistenceOfAnItem(itemModel.id, newExistence))
                        {
                            removed = true;
                        }
                    }
                }
                else if (tipo == DocumentModel.TIPO_DEVOLUCION)
                {
                    /** Documentos de Devolución */
                    if (serverModeLAN)
                    {
                        removed = true;
                    } else
                    {
                        double currentExistance = itemModel.existencia;// ItemModel.getTheCurrentExistenceOfAnItem(idArt);
                        if (itemModel.baseUnitId != capturedUnitId)
                        {
                            int capturedUnitIsMajor = ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(itemModel.baseUnitId, capturedUnitId);
                            if (capturedUnitIsMajor == 0)
                            {
                                /** Unidad de venta es menor: multiplicamos la base por el numero de conversión mayor */
                                double majorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, true);
                                double stockInMinorUnits = currentExistance * majorConversion;
                                double newStockInMinorUnits = 0;
                                if (addOrSubstrac == 0)
                                {
                                    stockInMinorUnits = (stockInMinorUnits - lastCapturedUnits);
                                    newStockInMinorUnits = stockInMinorUnits - capturedUnits;
                                }
                                else
                                {
                                    stockInMinorUnits = (stockInMinorUnits - lastCapturedUnits);
                                    newStockInMinorUnits = stockInMinorUnits + capturedUnits;
                                }
                                newExistence = newStockInMinorUnits / majorConversion;
                            }
                            else if (capturedUnitIsMajor == 1)
                            {
                                /** Unidad de venta es mayor: multiplicamos la base por el numero de conversión mayor */
                                double minorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, false);
                                double newCapturedUnits = capturedUnits / minorConversion;
                                if (addOrSubstrac == 0)
                                {
                                    currentExistance = (currentExistance - lastCapturedUnits);
                                    newExistence = currentExistance - newCapturedUnits;
                                }
                                else
                                {
                                    currentExistance = (currentExistance - lastCapturedUnits);
                                    newExistence = currentExistance + newCapturedUnits;
                                }
                            }
                            else if (capturedUnitIsMajor == 2)
                            {
                                /** Unidad de venta es igual a la unidad base */
                                if (addOrSubstrac == 0)
                                {
                                    currentExistance = (currentExistance - lastCapturedUnits);
                                    newExistence = (currentExistance - capturedUnits);
                                }
                                else
                                {
                                    currentExistance = (currentExistance - lastCapturedUnits);
                                    newExistence = (currentExistance + capturedUnits);
                                }
                            }
                            else
                            {
                                if (addOrSubstrac == 0)
                                {
                                    currentExistance = (currentExistance - lastCapturedUnits);
                                    newExistence = (currentExistance - capturedUnits);
                                }
                                else
                                {
                                    currentExistance = (currentExistance - lastCapturedUnits);
                                    newExistence = (currentExistance + capturedUnits);
                                }
                            }
                        }
                        else
                        {
                            if (addOrSubstrac == 0)
                            {
                                currentExistance = (currentExistance - lastCapturedUnits);
                                newExistence = (currentExistance - capturedUnits);
                            }
                            else
                            {
                                currentExistance = (currentExistance - lastCapturedUnits);
                                newExistence = (currentExistance + capturedUnits);
                            }
                        }
                        if (ItemModel.changeTheExistenceOfAnItem(itemModel.id, newExistence))
                        {
                            removed = true;
                        }
                    }
                }
                else
                {
                    removed = true;
                }
            }
            else
            {
                removed = false;
            }
            return removed;
        }

        public static async Task<Boolean> subtractOrAddAndRecalculateSalesMovement(int idMovement, ClsItemModel itemModel, int positionMove,
                                                              double capturedUnits, int addOrSubstract, int actualizar, double newPrice,
                                                              double capturedNonConvertibleUnits, double discountRate, int capturedUnitId,
                                                              double baseUnits)
        {
            bool subtracted = false;
            await Task.Run(async ()  =>
            {
                double previousCapturedUnits = 0,
                    nuevoMonto = 0, nuevoDescuento = 0, nuevoTotal = 0;
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    String query = "SELECT * FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " +
                            LocalDatabase.CAMPO_ID_MOV + " = " + idMovement;
                    using (SQLiteCommand command = new SQLiteCommand(query, db))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    previousCapturedUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_CAPTUREDUNIT_MOV].ToString().Trim());
                                }
                            }
                            if (reader != null && !reader.IsClosed)
                                reader.Close();
                        }
                    }
                    double newCapturedUnits = 0;
                    if (itemModel.baseUnitId != capturedUnitId)
                    {
                        int capturedUnitIsMajor = -1;
                        capturedUnitIsMajor = ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(itemModel.baseUnitId, capturedUnitId);
                        if (capturedUnitIsMajor == 0)
                        {
                            /** Unidad de venta es menor: multiplicamos la base por el numero de conversión mayor */
                            double majorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, true);
                            if (addOrSubstract == 0)
                            {
                                newCapturedUnits = previousCapturedUnits - capturedUnits;
                            }
                            else
                            {
                                newCapturedUnits = previousCapturedUnits + capturedUnits;
                            }
                        }
                        else if (capturedUnitIsMajor == 1)
                        {
                            /** Unidad de venta es mayor: multiplicamos la base por el numero de conversión mayor */
                            double minorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, false);
                            if (addOrSubstract == 0)
                            {
                                newCapturedUnits = previousCapturedUnits - capturedUnits;
                            }
                            else
                            {
                                newCapturedUnits = previousCapturedUnits + capturedUnits;
                            }
                        }
                        else if (capturedUnitIsMajor == 2)
                        {
                            /** Unidad de venta es igual a la unidad base */
                            if (addOrSubstract == 0)
                            {
                                newCapturedUnits = previousCapturedUnits - capturedUnits;
                            }
                            else
                            {
                                newCapturedUnits = previousCapturedUnits + capturedUnits;
                            }
                        }
                        else
                        {
                            if (addOrSubstract == 0)
                            {
                                newCapturedUnits = previousCapturedUnits - capturedUnits;
                            }
                            else
                            {
                                newCapturedUnits = previousCapturedUnits + capturedUnits;
                            }
                        }
                    }
                    else
                    {
                        if (addOrSubstract == 0)
                        {
                            newCapturedUnits = previousCapturedUnits - capturedUnits;
                        }
                        else
                        {
                            newCapturedUnits = previousCapturedUnits + capturedUnits;
                        }
                    }
                    if (actualizar == 1)
                    {
                        newCapturedUnits = capturedUnits;
                    }
                    nuevoMonto = (newCapturedUnits * newPrice);
                    nuevoDescuento = (nuevoMonto * discountRate) / 100;
                    nuevoTotal = (nuevoMonto - nuevoDescuento);
                    nuevoTotal = (double)decimal.Round((decimal)nuevoTotal, 2);
                    try
                    {
                        double newdisconunt = MovimientosModel.getPorcentajePromotionMoviments(idMovement);
                        if (newdisconunt <= discountRate)
                        {
                            discountRate = discountRate - newdisconunt;
                        }
                    }catch(Exception e)
                    {
                        SECUDOC.writeLog(e.ToString());
                    }
                    query = "UPDATE " + LocalDatabase.TABLA_MOVIMIENTO + " SET " + LocalDatabase.CAMPO_BASEUNIT_MOV + " = " + baseUnits + ", " +
                        LocalDatabase.CAMPO_NONCONVERTIBLEUNIT_MOV + " = " + capturedNonConvertibleUnits + ", " +
                        LocalDatabase.CAMPO_PRECIO_MOV + " = " + newPrice + ", " +
                        LocalDatabase.CAMPO_CAPTUREDUNIT_MOV + " = " + newCapturedUnits + ", " +
                        LocalDatabase.CAMPO_MONTO_MOV + " = " + nuevoMonto + ", " +
                        LocalDatabase.CAMPO_DESCUENTOPOR_MOV + " = @discountRate, " +
                        LocalDatabase.CAMPO_DESCUENTOIMP_MOV + " = " + nuevoDescuento + ", " +
                        LocalDatabase.CAMPO_TOTAL_MOV + " = " + nuevoTotal +
                        " WHERE " + LocalDatabase.CAMPO_ID_MOV + " = " + idMovement;
                    using (SQLiteCommand command1 = new SQLiteCommand(query, db))
                    {
                        command1.Parameters.AddWithValue("@discountRate", discountRate);
                        int records = command1.ExecuteNonQuery();
                        if (records > 0)
                            subtracted = true;
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
            });
            return subtracted;
        }

        public static Boolean validateWhetherToApplyPromotionToAMovement(int idMovement, double itemUnits, ClsItemModel itemModel, double descuento)
        {
            dynamic a = new ExpandoObject();
            a.valor = 0;
            a.categoria1 = 0;
            a.categoria2 = 0;
            a.categoria3 = 0;
            a.categoria4 = 0;
            a.categoria5 = 0;
            a.categoria6 = 0;
            int idCustomer = FormVenta.idCustomer;
            try
            {
                a = CustomerModel.getAllDataFromACustomerCategorias(idCustomer);
            }
            catch (Exception e)
            {
                SECUDOC.writeLog(e.ToString());
                a.valor = 0;
            }
            bool changed = false;
            double rateDiscountPromo = 0;
            /** Validamos si tiene promoción para aplicarlo */
            ClsPromocionesModel pm = PromotionsModel.getPromotionForAnItemCustomer(a, itemModel);
            if (pm != null)
            {
                int vigente = PromotionsModel.validateValidityEndDate(pm.fechaFin.ToString());
                if (vigente > 0)
                {
                    if (pm.volumenMinimo != 0 && itemUnits >= pm.volumenMinimo &&
                            pm.volumenMaximo != 0 && itemUnits <= pm.volumenMaximo)
                    {
                        rateDiscountPromo = pm.porcentajeDescuento;
                    }
                    else if (pm.volumenMinimo == 0 && itemUnits >= pm.volumenMinimo &&
                          pm.volumenMaximo != 0 && itemUnits <= pm.volumenMaximo)
                    {
                        rateDiscountPromo = pm.porcentajeDescuento;
                    }
                    else if (pm.volumenMinimo != 0 && itemUnits <= pm.volumenMinimo && pm.volumenMaximo == 0)
                    {
                        rateDiscountPromo = pm.porcentajeDescuento;
                    }
                    else if (pm.volumenMinimo == 0 && pm.volumenMaximo == 0)
                    {
                        rateDiscountPromo = pm.porcentajeDescuento;
                    }
                    else
                    {
                        rateDiscountPromo = 0;
                    }
                    if (rateDiscountPromo != 0)
                    {
                        if (changePromotionDiscountPercentageToAnItem(idMovement, rateDiscountPromo))
                        {
                            double amount = getAmountOfAMovement(idMovement);
                            double discountAmount = getDiscountOnTheAmountOfAMovement(idMovement);
                            discountAmount += (amount * discountAmount) / 100;
                            if (changeTheDiscountOnTheAmountOfAMove(idMovement, discountAmount))
                            {
                                double total = amount - discountAmount;
                                if (changeTheTotalOfAMove(idMovement, total))
                                {
                                    changed = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (changePromotionDiscountPercentageToAnItem(idMovement, 0))
                            changed = true;
                    }
                }
                else
                {
                    if (changePromotionDiscountPercentageToAnItem(idMovement, 0))
                        changed = true;
                }
            }
            else
            {
                if(changePromotionDiscountPercentageToAnItem(idMovement, 0))
                            changed = true;
            }
            return true;
        }

        public static Boolean changePromotionDiscountPercentageToAnItem(int idMovement,
                                                                    double rateDiscountPromo)
        {
            bool changed = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_MOVIMIENTO + " SET " +
                    LocalDatabase.CAMPO_RATEDISCOUNTPROMO_MOV + " = @rateDiscountPromo WHERE " +
                    LocalDatabase.CAMPO_ID_MOV + " = @idMovement";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idMovement", idMovement);
                    command.Parameters.AddWithValue("@rateDiscountPromo", rateDiscountPromo);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        changed = true;
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
            return changed;
        }

        public static double getAmountOfAMovement(int itemId, int positionMove)
        {
            double amount = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_MONTO_MOV + " FROM " + LocalDatabase.TABLA_MOVIMIENTO +
                        " WHERE " + LocalDatabase.CAMPO_ARTICULOID_MOV + " = " + itemId + " AND " + LocalDatabase.CAMPO_POSICION_MOV + " = " + positionMove;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                amount = Convert.ToDouble(reader[LocalDatabase.CAMPO_MONTO_MOV].ToString().Trim());
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
            return amount;
        }

        public static double getAmountOfAMovement(int idMovement)
        {
            double amount = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_MONTO_MOV + " FROM " + LocalDatabase.TABLA_MOVIMIENTO +
                        " WHERE " + LocalDatabase.CAMPO_ID_MOV + " = @idMovement";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idMovement", idMovement);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                amount = Convert.ToDouble(reader[LocalDatabase.CAMPO_MONTO_MOV].ToString().Trim());
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
            return amount;
        }

        public static double getDiscountOnTheAmountOfAMovement(int itemId, int positionMove)
        {
            double discountAmount = 0;
            var db = new SQLiteConnection();            
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_DESCUENTOIMP_MOV + " FROM " +
                        LocalDatabase.TABLA_MOVIMIENTO + " WHERE " + LocalDatabase.CAMPO_ARTICULOID_MOV + " = " + itemId +
                        " AND " + LocalDatabase.CAMPO_POSICION_MOV + " = " + positionMove;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                discountAmount = Convert.ToDouble(reader[LocalDatabase.CAMPO_DESCUENTOIMP_MOV].ToString().Trim());
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
            return discountAmount;
        }

        public static double getDiscountOnTheAmountOfAMovement(int idMovement)
        {
            double discountAmount = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_DESCUENTOIMP_MOV + " FROM " +
                        LocalDatabase.TABLA_MOVIMIENTO + " WHERE " + LocalDatabase.CAMPO_ID_MOV + " = @idMovement";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idMovement", idMovement);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader[LocalDatabase.CAMPO_DESCUENTOIMP_MOV] != DBNull.Value)
                                    discountAmount = Convert.ToDouble(reader[LocalDatabase.CAMPO_DESCUENTOPOR_MOV].ToString().Trim());
                                else
                                    discountAmount = 0;
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
            return discountAmount;
        }

        public static Boolean changeTheDiscountOnTheAmountOfAMove(int itemId, int positionMove,
                                                              double discount)
        {
            bool changed = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_MOVIMIENTO + " SET " + LocalDatabase.CAMPO_DESCUENTOIMP_MOV + " = " + discount + " WHERE " +
                    LocalDatabase.CAMPO_ARTICULOID_MOV + " = " + itemId + " AND " + LocalDatabase.CAMPO_POSICION_MOV + " = " + positionMove;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
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

        public static Boolean changeTheDiscountOnTheAmountOfAMove(int idMovement, double discount)
        {
            bool changed = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_MOVIMIENTO + " SET " + 
                    LocalDatabase.CAMPO_DESCUENTOIMP_MOV + " = @discount WHERE " +
                    LocalDatabase.CAMPO_ID_MOV + " = @idMovement";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@discount", discount);
                    command.Parameters.AddWithValue("@idMovement", idMovement);
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

        public static Boolean changeTheTotalOfAMove(int idMovement, double total)
        {
            bool changed = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_MOVIMIENTO + " SET " + 
                    LocalDatabase.CAMPO_TOTAL_MOV + " = @total WHERE " +
                    LocalDatabase.CAMPO_ID_MOV + " = @idMovement";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idMovement", idMovement);
                    command.Parameters.AddWithValue("@total", total);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        changed = true;
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
            return changed;
        }

        public static bool updateObervation(int idMovement, String observation)
        {
            bool changed = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_MOVIMIENTO + " SET " +
                    LocalDatabase.CAMPO_OBSERVACIONES_MOV + " = @observation WHERE " +
                    LocalDatabase.CAMPO_ID_MOV + " = @idMovement";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idMovement", idMovement);
                    command.Parameters.AddWithValue("@observation", observation);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        changed = true;
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
            return changed;
        }

        public static MovimientosModel getAMovement(String query)
        {
            MovimientosModel mcm = null;
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
                                mcm = new MovimientosModel();
                                mcm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_MOV].ToString().Trim());
                                mcm.documentId = Convert.ToInt32(reader[LocalDatabase.CAMPO_DOCUMENTOID_MOV].ToString().Trim());
                                mcm.itemCode = reader[LocalDatabase.CAMPO_CLAVEART_MOV].ToString().Trim();
                                mcm.itemId = Convert.ToInt32(reader[LocalDatabase.CAMPO_ARTICULOID_MOV].ToString().Trim());
                                mcm.baseUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_BASEUNIT_MOV].ToString().Trim());
                                mcm.nonConvertibleUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNIT_MOV].ToString().Trim());
                                mcm.capturedUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_CAPTUREDUNIT_MOV].ToString().Trim());
                                mcm.nonConvertibleUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_MOV].ToString().Trim());
                                mcm.capturedUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CAPTUREDUNITID_MOV].ToString().Trim());
                                mcm.capturesUnitsType = Convert.ToInt32(reader[LocalDatabase.CAMPO_CAPTUREDUNITTYPE_MOV].ToString().Trim());
                                mcm.price = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO_MOV].ToString().Trim());
                                mcm.monto = Convert.ToDouble(reader[LocalDatabase.CAMPO_MONTO_MOV].ToString().Trim());
                                mcm.total = Convert.ToDouble(reader[LocalDatabase.CAMPO_TOTAL_MOV].ToString().Trim());
                                mcm.position = Convert.ToInt32(reader[LocalDatabase.CAMPO_POSICION_MOV].ToString().Trim());
                                mcm.documentType = Convert.ToInt32(reader[LocalDatabase.CAMPO_TIPODOCUMENTO_MOV].ToString().Trim());
                                mcm.nameUser = reader[LocalDatabase.CAMPO_NOMBREU_MOV].ToString().Trim();
                                mcm.invoice = reader[LocalDatabase.CAMPO_FACTURA_MOV].ToString().Trim();
                                mcm.descuentoPorcentaje = Convert.ToDouble(reader[LocalDatabase.CAMPO_DESCUENTOPOR_MOV].ToString().Trim());
                                mcm.descuentoImporte = Convert.ToDouble(reader[LocalDatabase.CAMPO_DESCUENTOIMP_MOV].ToString().Trim());
                                mcm.observations = reader[LocalDatabase.CAMPO_OBSERVACIONES_MOV].ToString().Trim();
                                mcm.idDev = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDDEV_MOV].ToString().ToString());
                                mcm.comments = reader[LocalDatabase.CAMPO_COMENTARIO_MOV].ToString().Trim();
                                mcm.userId = Convert.ToInt32(reader[LocalDatabase.CAMPO_USUARIOID_MOV].ToString().Trim());
                                mcm.enviadoAlWs = Convert.ToInt32(reader[LocalDatabase.CAMPO_ENVIADOALWS_MOV].ToString().Trim());
                                mcm.rateDiscountPromo = Convert.ToDouble(reader[LocalDatabase.CAMPO_RATEDISCOUNTPROMO_MOV].ToString().Trim());
                                mcm.cancel = Convert.ToInt32(reader[LocalDatabase.CAMPO_CANCEL_MOV].ToString().Trim());
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
            return mcm;
        }

        public static MovimientosModel getAMovementSurtir(String query)
        {
            MovimientosModel mcm = null;
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
                                mcm = new MovimientosModel();
                                mcm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_MOV].ToString().Trim());
                                mcm.documentId = Convert.ToInt32(reader[LocalDatabase.CAMPO_DOCUMENTOID_MOV].ToString().Trim());
                                mcm.itemCode = reader[LocalDatabase.CAMPO_CLAVEART_MOV].ToString().Trim();
                                mcm.itemId = Convert.ToInt32(reader[LocalDatabase.CAMPO_ARTICULOID_MOV].ToString().Trim());
                                mcm.baseUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_BASEUNIT_MOV].ToString().Trim());
                                mcm.nonConvertibleUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNIT_MOV].ToString().Trim());
                                mcm.capturedUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_CAPTUREDUNIT_MOV].ToString().Trim());
                                mcm.nonConvertibleUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_MOV].ToString().Trim());
                                mcm.capturedUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CAPTUREDUNITID_MOV].ToString().Trim());
                                mcm.capturesUnitsType = Convert.ToInt32(reader[LocalDatabase.CAMPO_CAPTUREDUNITTYPE_MOV].ToString().Trim());
                                mcm.price = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO_MOV].ToString().Trim());
                                mcm.monto = Convert.ToDouble(reader[LocalDatabase.CAMPO_MONTO_MOV].ToString().Trim());
                                mcm.total = Convert.ToDouble(reader[LocalDatabase.CAMPO_TOTAL_MOV].ToString().Trim());
                                mcm.position = Convert.ToInt32(reader[LocalDatabase.CAMPO_POSICION_MOV].ToString().Trim());
                                mcm.documentType = Convert.ToInt32(reader[LocalDatabase.CAMPO_TIPODOCUMENTO_MOV].ToString().Trim());
                                mcm.nameUser = reader[LocalDatabase.CAMPO_NOMBREU_MOV].ToString().Trim();
                                mcm.invoice = reader[LocalDatabase.CAMPO_FACTURA_MOV].ToString().Trim();
                                mcm.descuentoPorcentaje = Convert.ToDouble(reader[LocalDatabase.CAMPO_DESCUENTOPOR_MOV].ToString().Trim());
                                mcm.descuentoImporte = Convert.ToDouble(reader[LocalDatabase.CAMPO_DESCUENTOIMP_MOV].ToString().Trim());
                                mcm.observations = reader[LocalDatabase.CAMPO_OBSERVACIONES_MOV].ToString().Trim();
                                mcm.idDev = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDDEV_MOV].ToString().ToString());
                                mcm.comments = reader[LocalDatabase.CAMPO_COMENTARIO_MOV].ToString().Trim();
                                mcm.userId = Convert.ToInt32(reader[LocalDatabase.CAMPO_USUARIOID_MOV].ToString().Trim());
                                mcm.enviadoAlWs = Convert.ToInt32(reader[LocalDatabase.CAMPO_ENVIADOALWS_MOV].ToString().Trim());
                                mcm.rateDiscountPromo = Convert.ToDouble(reader[LocalDatabase.CAMPO_RATEDISCOUNTPROMO_MOV].ToString().Trim());
                                mcm.cancel = Convert.ToInt32(reader[LocalDatabase.CAMPO_CANCEL_MOV].ToString().Trim());
                                mcm.descuentoPorcentaje = mcm.descuentoPorcentaje - mcm.rateDiscountPromo;
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
            return mcm;
        }

        public static MovimientosModel getMovement(String query)
        {
            MovimientosModel mcm = null;
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
                                mcm = new MovimientosModel();
                                mcm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_MOV].ToString().Trim());
                                mcm.documentId = Convert.ToInt32(reader[LocalDatabase.CAMPO_DOCUMENTOID_MOV].ToString().Trim());
                                mcm.itemCode = reader[LocalDatabase.CAMPO_CLAVEART_MOV].ToString().Trim();
                                mcm.itemId = Convert.ToInt32(reader[LocalDatabase.CAMPO_ARTICULOID_MOV].ToString().Trim());
                                mcm.baseUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_BASEUNIT_MOV].ToString().Trim());
                                mcm.nonConvertibleUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNIT_MOV].ToString().Trim());
                                mcm.capturedUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_CAPTUREDUNIT_MOV].ToString().Trim());
                                mcm.nonConvertibleUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_MOV].ToString().Trim());
                                mcm.capturedUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CAPTUREDUNITID_MOV].ToString().Trim());
                                mcm.capturesUnitsType = Convert.ToInt32(reader[LocalDatabase.CAMPO_CAPTUREDUNITTYPE_MOV].ToString().Trim());
                                mcm.price = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO_MOV].ToString().Trim());
                                mcm.monto = Convert.ToDouble(reader[LocalDatabase.CAMPO_MONTO_MOV].ToString().Trim());
                                mcm.total = Convert.ToDouble(reader[LocalDatabase.CAMPO_TOTAL_MOV].ToString().Trim());
                                mcm.position = Convert.ToInt32(reader[LocalDatabase.CAMPO_POSICION_MOV].ToString().Trim());
                                mcm.documentType = Convert.ToInt32(reader[LocalDatabase.CAMPO_TIPODOCUMENTO_MOV].ToString().Trim());
                                mcm.nameUser = reader[LocalDatabase.CAMPO_NOMBREU_MOV].ToString().Trim();
                                mcm.invoice = reader[LocalDatabase.CAMPO_FACTURA_MOV].ToString().Trim();
                                mcm.descuentoPorcentaje = Convert.ToDouble(reader[LocalDatabase.CAMPO_DESCUENTOPOR_MOV].ToString().Trim());
                                mcm.descuentoImporte = Convert.ToDouble(reader[LocalDatabase.CAMPO_DESCUENTOIMP_MOV].ToString().Trim());
                                mcm.observations = reader[LocalDatabase.CAMPO_OBSERVACIONES_MOV].ToString().Trim();
                                mcm.idDev = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDDEV_MOV].ToString().ToString());
                                mcm.comments = reader[LocalDatabase.CAMPO_COMENTARIO_MOV].ToString().Trim();
                                mcm.userId = Convert.ToInt32(reader[LocalDatabase.CAMPO_USUARIOID_MOV].ToString().Trim());
                                mcm.enviadoAlWs = Convert.ToInt32(reader[LocalDatabase.CAMPO_ENVIADOALWS_MOV].ToString().Trim());
                                mcm.rateDiscountPromo = Convert.ToDouble(reader[LocalDatabase.CAMPO_RATEDISCOUNTPROMO_MOV].ToString().Trim());
                                mcm.cancel = Convert.ToInt32(reader[LocalDatabase.CAMPO_CANCEL_MOV].ToString().Trim());
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
            return mcm;
        }

        public static double getPorcentajePromotionMoviments(int idMov)
        {
            double mcm = 0;
            var db = new SQLiteConnection();
            String query = "select "+LocalDatabase.CAMPO_RATEDISCOUNTPROMO_MOV + " from Movimientos where id = " + idMov;
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
                                mcm = Convert.ToDouble(reader[LocalDatabase.CAMPO_RATEDISCOUNTPROMO_MOV].ToString().Trim());
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
                mcm = 0;
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return mcm;
        }

        public static MovimientosModel getAMovementFromADocument(int idDocumento)
        {
            MovimientosModel mcm = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " + LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " +
                    idDocumento;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                mcm = new MovimientosModel();
                                mcm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_MOV].ToString().Trim());
                                mcm.documentId = Convert.ToInt32(reader[LocalDatabase.CAMPO_DOCUMENTOID_MOV].ToString().Trim());
                                mcm.itemCode = reader[LocalDatabase.CAMPO_CLAVEART_MOV].ToString().Trim();
                                mcm.itemId = Convert.ToInt32(reader[LocalDatabase.CAMPO_ARTICULOID_MOV].ToString().Trim());
                                mcm.baseUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_BASEUNIT_MOV].ToString().Trim());
                                mcm.nonConvertibleUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNIT_MOV].ToString().Trim());
                                mcm.capturedUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_CAPTUREDUNIT_MOV].ToString().Trim());
                                mcm.nonConvertibleUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_MOV].ToString().Trim());
                                mcm.capturedUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CAPTUREDUNITID_MOV].ToString().Trim());
                                mcm.capturesUnitsType = Convert.ToInt32(reader[LocalDatabase.CAMPO_CAPTUREDUNITTYPE_MOV].ToString().Trim());
                                mcm.price = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO_MOV].ToString().Trim());
                                mcm.monto = Convert.ToDouble(reader[LocalDatabase.CAMPO_MONTO_MOV].ToString().Trim());
                                mcm.total = Convert.ToDouble(reader[LocalDatabase.CAMPO_TOTAL_MOV].ToString().Trim());
                                mcm.position = Convert.ToInt32(reader[LocalDatabase.CAMPO_POSICION_MOV].ToString().Trim());
                                mcm.documentType = Convert.ToInt32(reader[LocalDatabase.CAMPO_TIPODOCUMENTO_MOV].ToString().Trim());
                                mcm.nameUser = reader[LocalDatabase.CAMPO_NOMBREU_MOV].ToString().Trim();
                                mcm.invoice = reader[LocalDatabase.CAMPO_FACTURA_MOV].ToString().Trim();
                                mcm.descuentoPorcentaje = Convert.ToDouble(reader[LocalDatabase.CAMPO_DESCUENTOPOR_MOV].ToString().Trim());
                                mcm.descuentoImporte = Convert.ToDouble(reader[LocalDatabase.CAMPO_DESCUENTOIMP_MOV].ToString().Trim());
                                mcm.observations = reader[LocalDatabase.CAMPO_OBSERVACIONES_MOV].ToString().Trim();
                                mcm.idDev = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDDEV_MOV].ToString().ToString());
                                mcm.comments = reader[LocalDatabase.CAMPO_COMENTARIO_MOV].ToString().Trim();
                                mcm.userId = Convert.ToInt32(reader[LocalDatabase.CAMPO_USUARIOID_MOV].ToString().Trim());
                                mcm.enviadoAlWs = Convert.ToInt32(reader[LocalDatabase.CAMPO_ENVIADOALWS_MOV].ToString().Trim());
                                mcm.rateDiscountPromo = Convert.ToDouble(reader[LocalDatabase.CAMPO_RATEDISCOUNTPROMO_MOV].ToString().Trim());
                                mcm.cancel = Convert.ToInt32(reader[LocalDatabase.CAMPO_CANCEL_MOV].ToString().Trim());
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
            return mcm;
        }

        public static MovimientosModel getAMovementFromADocument(int idDocumento, int idMovimiento)
        {
            MovimientosModel mcm = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " + LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " +
                    idDocumento+" AND "+LocalDatabase.CAMPO_ID_MOV+" = "+idMovimiento;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                mcm = new MovimientosModel();
                                mcm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_MOV].ToString().Trim());
                                mcm.documentId = Convert.ToInt32(reader[LocalDatabase.CAMPO_DOCUMENTOID_MOV].ToString().Trim());
                                mcm.itemCode = reader[LocalDatabase.CAMPO_CLAVEART_MOV].ToString().Trim();
                                mcm.itemId = Convert.ToInt32(reader[LocalDatabase.CAMPO_ARTICULOID_MOV].ToString().Trim());
                                mcm.baseUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_BASEUNIT_MOV].ToString().Trim());
                                mcm.nonConvertibleUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNIT_MOV].ToString().Trim());
                                mcm.capturedUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_CAPTUREDUNIT_MOV].ToString().Trim());
                                mcm.nonConvertibleUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_MOV].ToString().Trim());
                                mcm.capturedUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CAPTUREDUNITID_MOV].ToString().Trim());
                                mcm.capturesUnitsType = Convert.ToInt32(reader[LocalDatabase.CAMPO_CAPTUREDUNITTYPE_MOV].ToString().Trim());
                                mcm.price = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO_MOV].ToString().Trim());
                                mcm.monto = Convert.ToDouble(reader[LocalDatabase.CAMPO_MONTO_MOV].ToString().Trim());
                                mcm.total = Convert.ToDouble(reader[LocalDatabase.CAMPO_TOTAL_MOV].ToString().Trim());
                                mcm.position = Convert.ToInt32(reader[LocalDatabase.CAMPO_POSICION_MOV].ToString().Trim());
                                mcm.documentType = Convert.ToInt32(reader[LocalDatabase.CAMPO_TIPODOCUMENTO_MOV].ToString().Trim());
                                mcm.nameUser = reader[LocalDatabase.CAMPO_NOMBREU_MOV].ToString().Trim();
                                mcm.invoice = reader[LocalDatabase.CAMPO_FACTURA_MOV].ToString().Trim();
                                mcm.descuentoPorcentaje = Convert.ToDouble(reader[LocalDatabase.CAMPO_DESCUENTOPOR_MOV].ToString().Trim());
                                mcm.descuentoImporte = Convert.ToDouble(reader[LocalDatabase.CAMPO_DESCUENTOIMP_MOV].ToString().Trim());
                                mcm.observations = reader[LocalDatabase.CAMPO_OBSERVACIONES_MOV].ToString().Trim();
                                mcm.idDev = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDDEV_MOV].ToString().ToString());
                                mcm.comments = reader[LocalDatabase.CAMPO_COMENTARIO_MOV].ToString().Trim();
                                mcm.userId = Convert.ToInt32(reader[LocalDatabase.CAMPO_USUARIOID_MOV].ToString().Trim());
                                mcm.enviadoAlWs = Convert.ToInt32(reader[LocalDatabase.CAMPO_ENVIADOALWS_MOV].ToString().Trim());
                                mcm.rateDiscountPromo = Convert.ToDouble(reader[LocalDatabase.CAMPO_RATEDISCOUNTPROMO_MOV].ToString().Trim());
                                mcm.cancel = Convert.ToInt32(reader[LocalDatabase.CAMPO_CANCEL_MOV].ToString().Trim());
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
            return mcm;
        }

        public static String obtenerSumaDeSubDescYTotalDeMovimientosDeUndocumento(int idDocumento)
        {
            String sumas = "";
            double subtotal = 0;
            double desc = 0;
            double total = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " +
                        LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " + idDocumento;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                subtotal += Convert.ToDouble(reader[LocalDatabase.CAMPO_MONTO_MOV].ToString().Trim());
                                desc += Convert.ToDouble(reader[LocalDatabase.CAMPO_DESCUENTOIMP_MOV].ToString().Trim());
                                total += Convert.ToDouble(reader[LocalDatabase.CAMPO_TOTAL_MOV].ToString().Trim());
                            }
                            sumas = subtotal + "-" + desc + "-" + total;
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
            return sumas;
        }

        public static double getNewStockBySubtractingOrAddingUnits(int baseUnitId, int capturedUnitId, double capturedUnits, double stock, Boolean adding, 
            Boolean byTwo)
        {
            double newStock = 0;
            if (baseUnitId != capturedUnitId)
            {
                int capturedUnitIsMajor = ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(baseUnitId, capturedUnitId);
                if (capturedUnitIsMajor == 0)
                {
                    /** Unidad de venta es menor: multiplicamos la base por el numero de conversión mayor */
                    double majorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(baseUnitId, capturedUnitId, true);
                    double stockInMinorUnits = stock * majorConversion;
                    double newStockInMinorUnits = 0;
                    if (adding)
                    {
                        if (byTwo)
                        {
                            newStockInMinorUnits = stockInMinorUnits + (capturedUnits * 2);
                        }
                        else
                        {
                            newStockInMinorUnits = stockInMinorUnits + capturedUnits;
                        }
                    }
                    else
                    {
                        if (byTwo)
                        {
                            newStockInMinorUnits = stockInMinorUnits - (capturedUnits * 2);
                        }
                        else
                        {
                            newStockInMinorUnits = stockInMinorUnits - capturedUnits;
                        }
                    }
                    newStock = newStockInMinorUnits / majorConversion;
                }
                else if (capturedUnitIsMajor == 1)
                {
                    /** Unidad de venta es mayor: multiplicamos la base por el numero de conversión mayor */
                    double minorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(baseUnitId, capturedUnitId, false);
                    double newCapturedUnits = capturedUnits / minorConversion;
                    if (adding)
                    {
                        if (byTwo)
                        {
                            newStock = stock + (newCapturedUnits * 2);
                        }
                        else
                        {
                            newStock = stock + newCapturedUnits;
                        }
                    }
                    else
                    {
                        if (byTwo)
                        {
                            newStock = stock - (newCapturedUnits * 2);
                        }
                        else
                        {
                            newStock = stock - newCapturedUnits;
                        }
                    }
                }
                else if (capturedUnitIsMajor == 2)
                {
                    /** Unidad de venta es igual a la unidad base */
                    if (adding)
                    {
                        if (byTwo)
                        {
                            newStock = (stock + (capturedUnits * 2));
                        }
                        else
                        {
                            newStock = (stock + capturedUnits);
                        }
                    }
                    else
                    {
                        if (byTwo)
                        {
                            newStock = (stock - (capturedUnits * 2));
                        }
                        else
                        {
                            newStock = (stock - capturedUnits);
                        }
                    }
                }
                else
                {
                    if (adding)
                    {
                        if (byTwo)
                        {
                            newStock = (stock + (capturedUnits * 2));
                        }
                        else
                        {
                            newStock = (stock + capturedUnits);
                        }
                    }
                    else
                    {
                        if (byTwo)
                        {
                            newStock = (stock - (capturedUnits * 2));
                        }
                        else
                        {
                            newStock = (stock - capturedUnits);
                        }
                    }
                }
            }
            else
            {
                if (adding)
                {
                    if (byTwo)
                    {
                        newStock = (stock + (capturedUnits * 2));
                    }
                    else
                    {
                        newStock = (stock + capturedUnits);
                    }
                }
                else
                {
                    if (byTwo)
                    {
                        newStock = (stock - (capturedUnits * 2));
                    }
                    else
                    {
                        newStock = (stock - capturedUnits);
                    }
                }
            }
            return newStock;
        }

        public static Boolean updateCapturedUnits(int idMovement, double capturedUnits)
        {
            Boolean updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_MOVIMIENTO + " SET " + LocalDatabase.CAMPO_CAPTUREDUNIT_MOV + " = @capturedUnits WHERE " +
                    LocalDatabase.CAMPO_ID_MOV + " = @idMovement";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@capturedUnits", capturedUnits);
                    command.Parameters.AddWithValue("@idMovement", idMovement);
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

        public static bool updateCapturedUnitId(int idMovement, int capturedUnitId)
        {
            bool updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_MOVIMIENTO + " SET " + LocalDatabase.CAMPO_CAPTUREDUNITID_MOV + " = @capturedUnitId WHERE " +
                    LocalDatabase.CAMPO_ID_MOV + " = @idMovement";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@capturedUnitId", capturedUnitId);
                    command.Parameters.AddWithValue("@idMovement", idMovement);
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

        public static Boolean updateFacturaField(int idMovement, String generarFactura)
        {
            Boolean updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_MOVIMIENTO + " SET " + LocalDatabase.CAMPO_FACTURA_MOV + " = @generarFactura WHERE " +
                    LocalDatabase.CAMPO_ID_MOV + " = @idMovement";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@generarFactura", generarFactura);
                    command.Parameters.AddWithValue("@idMovement", idMovement);
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

        public static Boolean checkForMovementsOfAnItem(int idDocument, int idItem)
        {
            Boolean thereAre = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " +
                        LocalDatabase.CAMPO_ARTICULOID_MOV + " = @idItem AND " + LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = @idDocument";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idItem", idItem);
                    command.Parameters.AddWithValue("@idDocument", idDocument);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            thereAre = true;
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
            return thereAre;
        }

        public static Boolean reassignMovementPositions()
        {
            Boolean changed = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                int items = 0;
                int contador = 0;
                String query = "SELECT " + LocalDatabase.CAMPO_ID_MOV + ", " + LocalDatabase.CAMPO_POSICION_MOV + " FROM " +
                        LocalDatabase.TABLA_MOVIMIENTO + " WHERE " + LocalDatabase.CAMPO_POSICION_MOV + " > " + 0;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            int count = 1;
                            while (reader.Read())
                            {
                                String query1 = "UPDATE " + LocalDatabase.TABLA_MOVIMIENTO + " SET " + LocalDatabase.CAMPO_POSICION_MOV + " = " + count + " WHERE " +
                                    LocalDatabase.CAMPO_ID_MOV + " = " + reader.GetInt32(0);
                                using (SQLiteCommand command1 = new SQLiteCommand(query1, db))
                                {
                                    int records = command1.ExecuteNonQuery();
                                    if (records > 0)
                                        contador++; count++;
                                }
                            }
                            if (items == contador)
                                changed = true;
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
            return changed;
        }

        public static double obtenerElDescuentoTotalEnImporteDeUnDocumento(int idDoc)
        {
            double descuento = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT SUM(" + LocalDatabase.CAMPO_DESCUENTOIMP_MOV + ") FROM " + LocalDatabase.TABLA_MOVIMIENTO +
                        " WHERE " + LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " + idDoc;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                descuento = reader.GetDouble(0);
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
            return descuento;
        }

        public static double getUnidadesBasePendientesLocales(int idItem, int idMovimiento, bool incluyendoElActual)
        {
            double unidades = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT SUM(M." + LocalDatabase.CAMPO_BASEUNIT_MOV + ") AS unidades FROM " + LocalDatabase.TABLA_MOVIMIENTO + " M " +
                    "INNER JOIN " + LocalDatabase.TABLA_DOCUMENTOVENTA + " D ON M." + LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = D." + LocalDatabase.CAMPO_ID_DOC +
                    " WHERE (M." + LocalDatabase.CAMPO_ARTICULOID_MOV + " = " + idItem + " AND (D." + LocalDatabase.CAMPO_CANCELADO_DOC + " = 0 AND " +
                    "D." + LocalDatabase.CAMPO_PAUSAR_DOC + " = 1) OR (D." + LocalDatabase.CAMPO_PAUSAR_DOC + " = 0 AND D."+
                    LocalDatabase.CAMPO_ENVIADOALWS_DOC+" = 0";
                if (incluyendoElActual)
                    query += "))";
                else
                {
                    query += ")) AND M."+LocalDatabase.CAMPO_ID_MOV+" != "+idMovimiento+"";
                }
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    unidades = Convert.ToDouble(reader.GetValue(0).ToString().Trim());
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
            return unidades;
        }

        public static List<MovimientosModel> getAllNotSendMovimientosFromADocumnt(int idDoc, int enviado)
        {
            List<MovimientosModel> listaMvCarrito = null;
            MovimientosModel mcm = null;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " +
                        LocalDatabase.CAMPO_DOCUMENTOID_MOV + "=" + idDoc;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            listaMvCarrito = new List<MovimientosModel>();
                            while (reader.Read())
                            {
                                mcm = new MovimientosModel();
                                mcm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_MOV].ToString().Trim());
                                mcm.documentId = Convert.ToInt32(reader[LocalDatabase.CAMPO_DOCUMENTOID_MOV].ToString().Trim());
                                mcm.itemCode = reader[LocalDatabase.CAMPO_CLAVEART_MOV].ToString().Trim();
                                mcm.itemId = Convert.ToInt32(reader[LocalDatabase.CAMPO_ARTICULOID_MOV].ToString().Trim());
                                mcm.baseUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_BASEUNIT_MOV].ToString().Trim());
                                mcm.nonConvertibleUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNIT_MOV].ToString().Trim());
                                mcm.capturedUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_CAPTUREDUNIT_MOV].ToString().Trim());
                                mcm.nonConvertibleUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_MOV].ToString().Trim());
                                mcm.capturedUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CAPTUREDUNITID_MOV].ToString().Trim());
                                mcm.capturesUnitsType = Convert.ToInt32(reader[LocalDatabase.CAMPO_CAPTUREDUNITTYPE_MOV].ToString().Trim());
                                mcm.price = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO_MOV].ToString().Trim());
                                mcm.monto = Convert.ToDouble(reader[LocalDatabase.CAMPO_MONTO_MOV].ToString().Trim());
                                mcm.total = Convert.ToDouble(reader[LocalDatabase.CAMPO_TOTAL_MOV].ToString().Trim());
                                mcm.position = Convert.ToInt32(reader[LocalDatabase.CAMPO_POSICION_MOV].ToString().Trim());
                                mcm.documentType = Convert.ToInt32(reader[LocalDatabase.CAMPO_TIPODOCUMENTO_MOV].ToString().Trim());
                                mcm.nameUser = reader[LocalDatabase.CAMPO_NOMBREU_MOV].ToString().Trim();
                                mcm.invoice = reader[LocalDatabase.CAMPO_FACTURA_MOV].ToString().Trim();
                                mcm.descuentoPorcentaje = Convert.ToDouble(reader[LocalDatabase.CAMPO_DESCUENTOPOR_MOV].ToString().Trim());
                                mcm.descuentoImporte = Convert.ToDouble(reader[LocalDatabase.CAMPO_DESCUENTOIMP_MOV].ToString().Trim());
                                if (reader[LocalDatabase.CAMPO_OBSERVACIONES_MOV] != DBNull.Value)
                                    mcm.observations = reader[LocalDatabase.CAMPO_OBSERVACIONES_MOV].ToString().Trim();
                                else mcm.observations = "";
                                if (!mcm.observations.Equals(""))
                                {
                                    mcm.observations = mcm.observations.Replace("\"", "");
                                    mcm.observations = mcm.observations.Replace("'", "");
                                }
                                mcm.idDev = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDDEV_MOV].ToString().Trim());
                                if (reader[LocalDatabase.CAMPO_COMENTARIO_MOV] != DBNull.Value)
                                    mcm.comments = reader[LocalDatabase.CAMPO_COMENTARIO_MOV].ToString().Trim();
                                else mcm.comments = "";
                                mcm.userId = Convert.ToInt32(reader[LocalDatabase.CAMPO_USUARIOID_MOV].ToString().Trim());
                                mcm.rateDiscountPromo = Convert.ToDouble(reader[LocalDatabase.CAMPO_RATEDISCOUNTPROMO_MOV].ToString().Trim());
                                mcm.discount = (mcm.descuentoPorcentaje + mcm.rateDiscountPromo);
                                listaMvCarrito.Add(mcm);
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
            return listaMvCarrito;
        }

        public static int updateAEnviadoMovimientosDeUnDocumento(int idDocumento)
        {
            int resp = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "UPDATE " + LocalDatabase.TABLA_MOVIMIENTO + " SET " + LocalDatabase.CAMPO_ENVIADOALWS_MOV + " = " + 1 + " WHERE " +
                    LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " + idDocumento;
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

        public static Boolean deleteAllMovements()
        {
            bool deleted = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "DELETE FROM " + LocalDatabase.TABLA_MOVIMIENTO;
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
