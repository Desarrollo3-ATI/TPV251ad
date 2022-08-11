using SyncTPV.Controllers;
using SyncTPV.Controllers.Downloads;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Threading.Tasks;
using wsROMClase;
using wsROMClases.Models.Commercial;
using wsROMClases.Models.Panel;

namespace SyncTPV
{
    public class ItemModel
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string codigo { get; set; }
        public int clasificacionId1 { get; set; }
        public int clasificacionId2 { get; set; }
        public int clasificacionId3 { get; set; }
        public int clasificacionId4 { get; set; }
        public int clasificacionId5 { get; set; }
        public int clasificacionId6 { get; set; }
        public double existencia { get; set; }
        public int ordenar { get; set; }
        public int consolidado { get; set; }
        public double descuentoMaximo { get; set; }
        public double precio1 { get; set; }
        public double precio2 { get; set; }
        public double precio3 { get; set; }
        public double precio4 { get; set; }
        public double precio5 { get; set; }
        public double precio6 { get; set; }
        public double precio7 { get; set; }
        public double precio8 { get; set; }
        public double precio9 { get; set; }
        public double precio10 { get; set; }
        public int baseUnitId { get; set; }
        public int nonConvertibleUnitId { get; set; }
        public int purchaseUnitId { get; set; }
        public int salesUnitId { get; set; }
        public String codigoAlterno { get; set; }
        public int fiscalProduct { get; set; }
        public double imp1 { get; set; }
        public double imp2 { get; set; }
        public double imp3 { get; set; }
        public double reten1 { get; set; }
        public double reten2 { get; set; }
        public int imp1Excento { get; set; } //0 = No Excento, 1 = Excento
        public int imp2CuotaFija { get; set; } //0 = considerar como porcentaje, 1 = cuota fija
        public int imp3CuotaFija { get; set; } //0 = considerar como porcentaje, 1 = cuota fija
        public String textExtra1 { get; set; }
        public String textExtra2 { get; set; }
        public String textExtra3 { get; set; }
        public double importeExtra1 { get; set; }
        public double importeExtra2 { get; set; }
        public double importeExtra3 { get; set; }
        public double importeExtra4 { get; set; }
        public double cantidadFiscal { get; set; }

        public static int saveItems(List<ClsItemModel> itemsList)
        {
            int lastId = 0;
            if (itemsList != null)
            {
                var db = new SQLiteConnection();                
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    ClsBanderasCargaInicialModel.updateValuesServer(db, 2, itemsList.Count);
                    foreach (var item in itemsList)
                    {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_ITEM + " VALUES(@id, @name, @code, @clasificationId1, " +
                            "@clasificationId2, @clasificationId3, @clasificationId4, @clasificationId5, @clasificationId6, @stock, " +
                            "@ordenar, @consolidado, @descuentoMaximo, @precio1, @precio2, @precio3, @precio4, @precio5, @precio6, " +
                            "@precio7, @precio8, @precio9, @precio10, @agregado, @baseUnitId, @nonConvertibleUnitId, @purchaseUnitId," +
                            " @salesUnitId, @codigoAlterno, @fiscalProduct, @imp1, @imp2, @imp3, @reten1, @reten2)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@id", item.id);
                            command.Parameters.AddWithValue("@name", item.nombre);
                            command.Parameters.AddWithValue("@code", item.codigo);
                            command.Parameters.AddWithValue("@clasificationId1", item.clasificacionId1);
                            command.Parameters.AddWithValue("@clasificationId2", item.clasificacionId2);
                            command.Parameters.AddWithValue("@clasificationId3", item.clasificacionId3);
                            command.Parameters.AddWithValue("@clasificationId4", item.clasificacionId4);
                            command.Parameters.AddWithValue("@clasificationId5", item.clasificacionId5);
                            command.Parameters.AddWithValue("@clasificationId6", item.clasificacionId6);
                            command.Parameters.AddWithValue("@stock", item.existencia);
                            command.Parameters.AddWithValue("@ordenar", item.ordenar);
                            command.Parameters.AddWithValue("@consolidado", item.consolidado);
                            command.Parameters.AddWithValue("@descuentoMaximo", item.descuentoMaximo);
                            command.Parameters.AddWithValue("@precio1", item.precio1);
                            command.Parameters.AddWithValue("@precio2", item.precio2);
                            command.Parameters.AddWithValue("@precio3", item.precio3);
                            command.Parameters.AddWithValue("@precio4", item.precio4);
                            command.Parameters.AddWithValue("@precio5", item.precio5);
                            command.Parameters.AddWithValue("@precio6", item.precio6);
                            command.Parameters.AddWithValue("@precio7", item.precio7);
                            command.Parameters.AddWithValue("@precio8", item.precio8);
                            command.Parameters.AddWithValue("@precio9", item.precio9);
                            command.Parameters.AddWithValue("@precio10", item.precio10);
                            command.Parameters.AddWithValue("@agregado", 0);
                            command.Parameters.AddWithValue("@baseUnitId", item.baseUnitId);
                            command.Parameters.AddWithValue("@nonConvertibleUnitId", item.nonConvertibleUnitId);
                            command.Parameters.AddWithValue("@purchaseUnitId", item.purchaseUnitId);
                            command.Parameters.AddWithValue("@salesUnitId", item.salesUnitId);
                            command.Parameters.AddWithValue("@codigoAlterno", item.codigoAlterno);
                            command.Parameters.AddWithValue("@fiscalProduct", 1);
                            command.Parameters.AddWithValue("@imp1", item.imp1);
                            command.Parameters.AddWithValue("@imp2", item.imp2);
                            command.Parameters.AddWithValue("@imp3", item.imp3);
                            command.Parameters.AddWithValue("@reten1", item.reten1);
                            command.Parameters.AddWithValue("@reten2", item.reten2);
                            int recordSaved = command.ExecuteNonQuery();
                            if (recordSaved != 0)
                                lastId = Convert.ToInt32(item.id);
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

        public static int saveItemsLAN(List<ClsItemModel> itemsList)
        {
            int lastId = 0;
            if (itemsList != null)
            {
                var db = new SQLiteConnection();                
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    ClsBanderasCargaInicialModel.updateValuesServer(db, 2, itemsList.Count);
                    foreach (var item in itemsList)
                    {
                        string itemName = "";
                        itemName = item.nombre.Replace("'", "").Trim();
                        String query = "INSERT INTO " + LocalDatabase.TABLA_ITEM + " VALUES(@id, @name, @code, @clasificationId1, " +
                            "@clasificationId2, @clasificationId3, @clasificationId4, @clasificationId5, @clasificationId6, @stock, " +
                            "@ordenar, @consolidado, @descuentoMaximo, @precio1, @precio2, @precio3, @precio4, @precio5, @precio6, " +
                            "@precio7, @precio8, @precio9, @precio10, @agregado, @baseUnitId, @nonConvertibleUnitId, @purchaseUnitId," +
                            " @salesUnitId, @codigoAlterno, @fiscalProduct, @imp1, @imp2, @imp3, @reten1, @reten2, " +
                            "@imp1Excento, @imp2CuotaFija, @imp3CuotaFija, @textExtra1, @textExtra2, @textExtra3, " +
                            "@importeExtra1, @importeExtra2, @importeExtra3, @importeExtra4, @cantidadFiscal)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@id", item.id);
                            command.Parameters.AddWithValue("@name", itemName.Trim());
                            command.Parameters.AddWithValue("@code", item.codigo);
                            command.Parameters.AddWithValue("@clasificationId1", item.clasificacionId1);
                            command.Parameters.AddWithValue("@clasificationId2", item.clasificacionId2);
                            command.Parameters.AddWithValue("@clasificationId3", item.clasificacionId3);
                            command.Parameters.AddWithValue("@clasificationId4", item.clasificacionId4);
                            command.Parameters.AddWithValue("@clasificationId5", item.clasificacionId5);
                            command.Parameters.AddWithValue("@clasificationId6", item.clasificacionId6);
                            command.Parameters.AddWithValue("@stock", item.existencia);
                            command.Parameters.AddWithValue("@ordenar", item.ordenar);
                            command.Parameters.AddWithValue("@consolidado", item.consolidado);
                            command.Parameters.AddWithValue("@descuentoMaximo", item.descuentoMaximo);
                            command.Parameters.AddWithValue("@precio1", item.precio1);
                            command.Parameters.AddWithValue("@precio2", item.precio2);
                            command.Parameters.AddWithValue("@precio3", item.precio3);
                            command.Parameters.AddWithValue("@precio4", item.precio4);
                            command.Parameters.AddWithValue("@precio5", item.precio5);
                            command.Parameters.AddWithValue("@precio6", item.precio6);
                            command.Parameters.AddWithValue("@precio7", item.precio7);
                            command.Parameters.AddWithValue("@precio8", item.precio8);
                            command.Parameters.AddWithValue("@precio9", item.precio9);
                            command.Parameters.AddWithValue("@precio10", item.precio10);
                            command.Parameters.AddWithValue("@agregado", 0);
                            command.Parameters.AddWithValue("@baseUnitId", item.baseUnitId);
                            command.Parameters.AddWithValue("@nonConvertibleUnitId", item.nonConvertibleUnitId);
                            command.Parameters.AddWithValue("@purchaseUnitId", item.purchaseUnitId);
                            command.Parameters.AddWithValue("@salesUnitId", item.salesUnitId);
                            command.Parameters.AddWithValue("@codigoAlterno", item.codigoAlterno);
                            command.Parameters.AddWithValue("@fiscalProduct", 1);
                            command.Parameters.AddWithValue("@imp1", item.imp1);
                            command.Parameters.AddWithValue("@imp2", item.imp2);
                            command.Parameters.AddWithValue("@imp3", item.imp3);
                            command.Parameters.AddWithValue("@reten1", item.reten1);
                            command.Parameters.AddWithValue("@reten2", item.reten2);
                            command.Parameters.AddWithValue("@imp1Excento", item.imp1Excento);
                            command.Parameters.AddWithValue("@imp2CuotaFija", item.imp2CuotaFija);
                            command.Parameters.AddWithValue("@imp3CuotaFija", item.imp3CuotaFija);
                            command.Parameters.AddWithValue("@textExtra1", item.textExtra1);
                            command.Parameters.AddWithValue("@textExtra2", item.textExtra2);
                            command.Parameters.AddWithValue("@textExtra3", item.textExtra3);
                            command.Parameters.AddWithValue("@importeExtra1", item.importeExtra1);
                            command.Parameters.AddWithValue("@importeExtra2", item.importeExtra2);
                            command.Parameters.AddWithValue("@importeExtra3", item.importeExtra3);
                            command.Parameters.AddWithValue("@importeExtra4", item.importeExtra4);
                            command.Parameters.AddWithValue("@cantidadFiscal", item.cantidadFiscal);
                            int recordSaved = command.ExecuteNonQuery();
                            if (recordSaved != 0)
                                lastId = Convert.ToInt32(item.id);
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

        public static int saveAItem(ClsItemModel im, SQLiteConnection db)
        {
            int lastId = 0;
            if (im != null)
            {
                try
                {
                    String query = "INSERT INTO " + LocalDatabase.TABLA_ITEM + " VALUES(@id, @name, @code, @clasificationId1, " +
                        "@clasificationId2, @clasificationId3, @clasificationId4, @clasificationId5, @clasificationId6, @stock, " +
                        "@ordenar, @consolidado, @descuentoMaximo, @precio1, @precio2, @precio3, @precio4, @precio5, @precio6, " +
                        "@precio7, @precio8, @precio9, @precio10, @agregado, @baseUnitId, @nonConvertibleUnitId, @purchaseUnitId," +
                        " @salesUnitId, @codigoAlterno, @fiscalProduct, @imp1, @imp2, @imp3, @reten1, @reten2, @imp1Excento, " +
                        "@imp2CuotaFija, @imp3CuotaFija, @textExtra1, @textExtra2, @textExtra3, @importeExtra1, " +
                        "@importeExtra2, @importeExtra3, @importeExtra4, @cantidadFiscal)";
                    using (SQLiteCommand command = new SQLiteCommand(query, db))
                    {
                        command.Parameters.AddWithValue("@id", im.id);
                        command.Parameters.AddWithValue("@name", im.nombre);
                        command.Parameters.AddWithValue("@code", im.codigo);
                        command.Parameters.AddWithValue("@clasificationId1", im.clasificacionId1);
                        command.Parameters.AddWithValue("@clasificationId2", im.clasificacionId2);
                        command.Parameters.AddWithValue("@clasificationId3", im.clasificacionId3);
                        command.Parameters.AddWithValue("@clasificationId4", im.clasificacionId4);
                        command.Parameters.AddWithValue("@clasificationId5", im.clasificacionId5);
                        command.Parameters.AddWithValue("@clasificationId6", im.clasificacionId6);
                        command.Parameters.AddWithValue("@stock", im.existencia);
                        command.Parameters.AddWithValue("@ordenar", im.ordenar);
                        command.Parameters.AddWithValue("@consolidado", im.consolidado);
                        command.Parameters.AddWithValue("@descuentoMaximo", im.descuentoMaximo);
                        command.Parameters.AddWithValue("@precio1", im.precio1);
                        command.Parameters.AddWithValue("@precio2", im.precio2);
                        command.Parameters.AddWithValue("@precio3", im.precio3);
                        command.Parameters.AddWithValue("@precio4", im.precio4);
                        command.Parameters.AddWithValue("@precio5", im.precio5);
                        command.Parameters.AddWithValue("@precio6", im.precio6);
                        command.Parameters.AddWithValue("@precio7", im.precio7);
                        command.Parameters.AddWithValue("@precio8", im.precio8);
                        command.Parameters.AddWithValue("@precio9", im.precio9);
                        command.Parameters.AddWithValue("@precio10", im.precio10);
                        command.Parameters.AddWithValue("@agregado", 0);
                        command.Parameters.AddWithValue("@baseUnitId", im.baseUnitId);
                        command.Parameters.AddWithValue("@nonConvertibleUnitId", im.nonConvertibleUnitId);
                        command.Parameters.AddWithValue("@purchaseUnitId", im.purchaseUnitId);
                        command.Parameters.AddWithValue("@salesUnitId", im.salesUnitId);
                        command.Parameters.AddWithValue("@codigoAlterno", im.codigoAlterno);
                        command.Parameters.AddWithValue("@fiscalProduct", 1);
                        command.Parameters.AddWithValue("@imp1", im.imp1);
                        command.Parameters.AddWithValue("@imp2", im.imp2);
                        command.Parameters.AddWithValue("@imp3", im.imp3);
                        command.Parameters.AddWithValue("@reten1", im.reten1);
                        command.Parameters.AddWithValue("@reten2", im.reten2);
                        command.Parameters.AddWithValue("@imp1Excento", im.imp1Excento);
                        command.Parameters.AddWithValue("@imp2CuotaFija", im.imp2CuotaFija);
                        command.Parameters.AddWithValue("@imp3CuotaFija", im.imp3CuotaFija);
                        command.Parameters.AddWithValue("@textExtra1", im.textExtra1);
                        command.Parameters.AddWithValue("@textExtra2", im.textExtra2);
                        command.Parameters.AddWithValue("@textExtra3", im.textExtra3);
                        command.Parameters.AddWithValue("@importeExtra1", im.importeExtra1);
                        command.Parameters.AddWithValue("@importeExtra2", im.importeExtra2);
                        command.Parameters.AddWithValue("@importeExtra3", im.importeExtra3);
                        command.Parameters.AddWithValue("@importeExtra4", im.importeExtra4);
                        command.Parameters.AddWithValue("@cantidadFiscal", im.cantidadFiscal);
                        int recordSaved = command.ExecuteNonQuery();
                        if (recordSaved != 0)
                            lastId = Convert.ToInt32(im.id);
                    }
                }
                catch (SQLiteException e)
                {
                    SECUDOC.writeLog("Exception: " + e.ToString());
                }
                finally
                {

                }
            }
            return lastId;
        }

        public static int saveAItemLAN(ClsItemModel im, SQLiteConnection db)
        {
            int lastId = 0;
            if (im != null)
            {
                try
                {
                    string itemName = "";
                    itemName = im.nombre.Replace("'", "").Trim();
                    String query = "INSERT INTO " + LocalDatabase.TABLA_ITEM + " VALUES(@id, @name, @code, @clasificationId1, " +
                        "@clasificationId2, @clasificationId3, @clasificationId4, @clasificationId5, @clasificationId6, @stock, " +
                        "@ordenar, @consolidado, @descuentoMaximo, @precio1, @precio2, @precio3, @precio4, @precio5, @precio6, " +
                        "@precio7, @precio8, @precio9, @precio10, @agregado, @baseUnitId, @nonConvertibleUnitId, @purchaseUnitId," +
                        " @salesUnitId, @codigoAlterno, @fiscalProduct, @imp1, @imp2, @imp3, @reten1, @reten2, @imp1Excento, " +
                        "@imp2CuotaFija, @imp3CuotaFija, @textExtra1, @textExtra2, @textExtra3, @importeExtra1, " +
                        "@importeExtra2, @importeExtra3, @importeExtra4, @cantidadFiscal)";
                    using (SQLiteCommand command = new SQLiteCommand(query, db))
                    {
                        command.Parameters.AddWithValue("@id", im.id);
                        command.Parameters.AddWithValue("@name", itemName.Trim());
                        command.Parameters.AddWithValue("@code", im.codigo);
                        command.Parameters.AddWithValue("@clasificationId1", im.clasificacionId1);
                        command.Parameters.AddWithValue("@clasificationId2", im.clasificacionId2);
                        command.Parameters.AddWithValue("@clasificationId3", im.clasificacionId3);
                        command.Parameters.AddWithValue("@clasificationId4", im.clasificacionId4);
                        command.Parameters.AddWithValue("@clasificationId5", im.clasificacionId5);
                        command.Parameters.AddWithValue("@clasificationId6", im.clasificacionId6);
                        command.Parameters.AddWithValue("@stock", im.existencia);
                        command.Parameters.AddWithValue("@ordenar", im.ordenar);
                        command.Parameters.AddWithValue("@consolidado", im.consolidado);
                        command.Parameters.AddWithValue("@descuentoMaximo", im.descuentoMaximo);
                        command.Parameters.AddWithValue("@precio1", im.precio1);
                        command.Parameters.AddWithValue("@precio2", im.precio2);
                        command.Parameters.AddWithValue("@precio3", im.precio3);
                        command.Parameters.AddWithValue("@precio4", im.precio4);
                        command.Parameters.AddWithValue("@precio5", im.precio5);
                        command.Parameters.AddWithValue("@precio6", im.precio6);
                        command.Parameters.AddWithValue("@precio7", im.precio7);
                        command.Parameters.AddWithValue("@precio8", im.precio8);
                        command.Parameters.AddWithValue("@precio9", im.precio9);
                        command.Parameters.AddWithValue("@precio10", im.precio10);
                        command.Parameters.AddWithValue("@agregado", 0);
                        command.Parameters.AddWithValue("@baseUnitId", im.baseUnitId);
                        command.Parameters.AddWithValue("@nonConvertibleUnitId", im.nonConvertibleUnitId);
                        command.Parameters.AddWithValue("@purchaseUnitId", im.purchaseUnitId);
                        command.Parameters.AddWithValue("@salesUnitId", im.salesUnitId);
                        command.Parameters.AddWithValue("@codigoAlterno", im.codigoAlterno);
                        command.Parameters.AddWithValue("@fiscalProduct", 1);
                        command.Parameters.AddWithValue("@imp1", im.imp1);
                        command.Parameters.AddWithValue("@imp2", im.imp2);
                        command.Parameters.AddWithValue("@imp3", im.imp3);
                        command.Parameters.AddWithValue("@reten1", im.reten1);
                        command.Parameters.AddWithValue("@reten2", im.reten2);
                        command.Parameters.AddWithValue("@imp1Excento", im.imp1Excento);
                        command.Parameters.AddWithValue("@imp2CuotaFija", im.imp2CuotaFija);
                        command.Parameters.AddWithValue("@imp3CuotaFija", im.imp3CuotaFija);
                        command.Parameters.AddWithValue("@textExtra1", im.textExtra1);
                        command.Parameters.AddWithValue("@textExtra2", im.textExtra2);
                        command.Parameters.AddWithValue("@textExtra3", im.textExtra3);
                        command.Parameters.AddWithValue("@importeExtra1", im.importeExtra1);
                        command.Parameters.AddWithValue("@importeExtra2", im.importeExtra2);
                        command.Parameters.AddWithValue("@importeExtra3", im.importeExtra3);
                        command.Parameters.AddWithValue("@importeExtra4", im.importeExtra4);
                        command.Parameters.AddWithValue("@cantidadFiscal", im.cantidadFiscal);
                        int recordSaved = command.ExecuteNonQuery();
                        if (recordSaved != 0)
                            lastId = Convert.ToInt32(im.id);
                    }
                }
                catch (SQLiteException e)
                {
                    SECUDOC.writeLog("Exception: " + e.ToString());
                }
                finally
                {

                }
            }
            return lastId;
        }

        public static bool updateAItem(ClsItemModel item, SQLiteConnection db)
        {
            bool updated = false;
            int lastId = 0;
            try
            {
                if (checkIfItemExist(db, item.id))
                {
                    String query = "UPDATE " + LocalDatabase.TABLA_ITEM + " SET " + LocalDatabase.CAMPO_NOMBRE_ITEM + " = @name, " +
                        LocalDatabase.CAMPO_CODIGO_ITEM + " = @code, " + LocalDatabase.CAMPO_CLASIFICATIONID1_ITEM + " = @clasificationId1, " +
                        LocalDatabase.CAMPO_CLASIFICATIONID2_ITEM + " = @clasificationId2, " + LocalDatabase.CAMPO_CLASIFICATIONID3_ITEM + " = @clasificationId3, " +
                        LocalDatabase.CAMPO_CLASIFICATIONID4_ITEM + " = @clasificationId4, " + LocalDatabase.CAMPO_CLASIFICATIONID5_ITEM + " = @clasificationId5, " +
                        LocalDatabase.CAMPO_CLASIFICATIONID6_ITEM + " = @clasificationId6, " + LocalDatabase.CAMPO_STOCK_ITEM + " = @stock, " +
                        LocalDatabase.CAMPO_ORDENAR_ITEM + " = @ordenar, " + LocalDatabase.CAMPO_CONSOLIDADO_ITEM + " = @consolidado, " +
                        LocalDatabase.CAMPO_DESCMAX_ITEM + " = @descuentoMaximo, " + LocalDatabase.CAMPO_PRECIO1_ITEM + " = @precio1, " +
                        LocalDatabase.CAMPO_PRECIO2_ITEM + " = @precio2, " + LocalDatabase.CAMPO_PRECIO3_ITEM + " = @precio3, " +
                        LocalDatabase.CAMPO_PRECIO4_ITEM + " = @precio4, " + LocalDatabase.CAMPO_PRECIO5_ITEM + " = @precio5, " +
                        LocalDatabase.CAMPO_PRECIO6_ITEM + " = @precio6, " + LocalDatabase.CAMPO_PRECIO7_ITEM + " = @precio7, " +
                        LocalDatabase.CAMPO_PRECIO8_ITEM + " = @precio8, " + LocalDatabase.CAMPO_PRECIO9_ITEM + " = @precio9, " +
                        LocalDatabase.CAMPO_PRECIO10_ITEM + " = @precio10, " + LocalDatabase.CAMPO_AGREGADO_ITEM + " = @agregado, " +
                        LocalDatabase.CAMPO_BASEUNITID_ITEM + " = @baseUnitId, " + LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_ITEM + " = @nonConvertibleUnitId, " +
                        LocalDatabase.CAMPO_PURCHASEUNITID_ITEM + " = @purchaseUnitId, " + LocalDatabase.CAMPO_SALESUNITID_ITEM + " = @salesUnitId, " +
                        LocalDatabase.CAMPO_CODIGOALTERNO_ITEM + " = @codigoAlterno, " + 
                        LocalDatabase.CAMPO_FISCALPRODUCT_ITEM + " = @fiscalProduct, " +
                        LocalDatabase.CAMPO_IMP1_ITEM+" = @imp1, "+
                        LocalDatabase.CAMPO_IMP2_ITEM + " = @imp2, " +
                        LocalDatabase.CAMPO_IMP3_ITEM + " = @imp3, " +
                        LocalDatabase.CAMPO_RETEN1_ITEM + " = @reten1, " +
                        LocalDatabase.CAMPO_RETEN2_ITEM + " = @reten2, " +
                        LocalDatabase.CAMPO_IMP1EXCENTO_ITEM + " = @imp1Excento, " +
                        LocalDatabase.CAMPO_IMP2CUOTA_ITEM + " = @imp2CuotaFija, " +
                        LocalDatabase.CAMPO_IMP3CUOTA_ITEM + " = @imp3CuotaFija, " +
                        LocalDatabase.CAMPO_TEXTEXTRA1_ITEM + " = @textExtra1, " +
                        LocalDatabase.CAMPO_TEXTEXTRA2_ITEM + " = @textExtra2, " +
                        LocalDatabase.CAMPO_TEXTEXTRA3_ITEM + " = @textExtra3, " +
                        LocalDatabase.CAMPO_IMPORTEEXTRA1_ITEM + " = @importeExtra1, " +
                        LocalDatabase.CAMPO_IMPORTEEXTRA2_ITEM + " = @importeExtra2, " +
                        LocalDatabase.CAMPO_IMPORTEEXTRA3_ITEM + " = @importeExtra3, " +
                        LocalDatabase.CAMPO_IMPORTEEXTRA4_ITEM + " = @importeExtra4, " +
                        LocalDatabase.CAMPO_CANTIDADFISCAL_ITEM + " = @cantidadFiscal " +
                        "WHERE " + LocalDatabase.CAMPO_ID_ITEM + " = " + item.id;
                    using (SQLiteCommand command = new SQLiteCommand(query, db))
                    {
                        command.Parameters.AddWithValue("@name", item.nombre);
                        command.Parameters.AddWithValue("@code", item.codigo);
                        command.Parameters.AddWithValue("@clasificationId1", item.clasificacionId1);
                        command.Parameters.AddWithValue("@clasificationId2", item.clasificacionId2);
                        command.Parameters.AddWithValue("@clasificationId3", item.clasificacionId3);
                        command.Parameters.AddWithValue("@clasificationId4", item.clasificacionId4);
                        command.Parameters.AddWithValue("@clasificationId5", item.clasificacionId5);
                        command.Parameters.AddWithValue("@clasificationId6", item.clasificacionId6);
                        command.Parameters.AddWithValue("@stock", item.existencia);
                        command.Parameters.AddWithValue("@ordenar", item.ordenar);
                        command.Parameters.AddWithValue("@consolidado", item.consolidado);
                        command.Parameters.AddWithValue("@descuentoMaximo", item.descuentoMaximo);
                        command.Parameters.AddWithValue("@precio1", item.precio1);
                        command.Parameters.AddWithValue("@precio2", item.precio2);
                        command.Parameters.AddWithValue("@precio3", item.precio3);
                        command.Parameters.AddWithValue("@precio4", item.precio4);
                        command.Parameters.AddWithValue("@precio5", item.precio5);
                        command.Parameters.AddWithValue("@precio6", item.precio6);
                        command.Parameters.AddWithValue("@precio7", item.precio7);
                        command.Parameters.AddWithValue("@precio8", item.precio8);
                        command.Parameters.AddWithValue("@precio9", item.precio9);
                        command.Parameters.AddWithValue("@precio10", item.precio10);
                        command.Parameters.AddWithValue("@agregado", 0);
                        command.Parameters.AddWithValue("@baseUnitId", item.baseUnitId);
                        command.Parameters.AddWithValue("@nonConvertibleUnitId", item.nonConvertibleUnitId);
                        command.Parameters.AddWithValue("@purchaseUnitId", item.purchaseUnitId);
                        command.Parameters.AddWithValue("@salesUnitId", item.salesUnitId);
                        command.Parameters.AddWithValue("@codigoAlterno", item.codigoAlterno);
                        command.Parameters.AddWithValue("@fiscalProduct", 1);
                        command.Parameters.AddWithValue("@imp1", item.imp1);
                        command.Parameters.AddWithValue("@imp2", item.imp2);
                        command.Parameters.AddWithValue("@imp3", item.imp3);
                        command.Parameters.AddWithValue("@reten1", item.reten1);
                        command.Parameters.AddWithValue("@reten2", item.reten2);
                        command.Parameters.AddWithValue("@imp1Excento", item.imp1Excento);
                        command.Parameters.AddWithValue("@imp2CuotaFija", item.imp2CuotaFija);
                        command.Parameters.AddWithValue("@imp3CuotaFija", item.imp3CuotaFija);
                        command.Parameters.AddWithValue("@textExtra1", item.textExtra1);
                        command.Parameters.AddWithValue("@textExtra2", item.textExtra2);
                        command.Parameters.AddWithValue("@textExtra3", item.textExtra3);
                        command.Parameters.AddWithValue("@importeExtra1", item.importeExtra1);
                        command.Parameters.AddWithValue("@importeExtra2", item.importeExtra2);
                        command.Parameters.AddWithValue("@importeExtra3", item.importeExtra3);
                        command.Parameters.AddWithValue("@importeExtra4", item.importeExtra4);
                        command.Parameters.AddWithValue("@cantidadFiscal", item.cantidadFiscal);
                        int recordUpdated = command.ExecuteNonQuery();
                        if (recordUpdated != 0)
                            updated = true;
                    }
                }
                else
                {
                    saveAItem(item, db);
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog("Exception: " + e.ToString());
            }
            finally
            {
            }
            return updated;
        }

        public static bool updateFiscalValue(int itemId, int fiscalValue)
        {
            bool updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_ITEM + " SET " +
                        LocalDatabase.CAMPO_FISCALPRODUCT_ITEM + " = @fiscalValue WHERE " + LocalDatabase.CAMPO_ID_ITEM + " = @itemId";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@fiscalValue", fiscalValue);
                    command.Parameters.AddWithValue("@itemId", itemId);
                    int recordUpdated = command.ExecuteNonQuery();
                    if (recordUpdated != 0)
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

        public static bool updateName(int itemId, String name)
        {
            bool updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_ITEM + " SET " +
                        LocalDatabase.CAMPO_NOMBRE_ITEM + " = @name WHERE " + LocalDatabase.CAMPO_ID_ITEM + " = @itemId";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@itemId", itemId);
                    int recordUpdated = command.ExecuteNonQuery();
                    if (recordUpdated != 0)
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

        public static bool updateAItemLAN(ClsItemModel item, SQLiteConnection db)
        {
            bool updated = false;
            int lastId = 0;
            try
            {
                if (checkIfItemExist(item.id))
                {
                    string itemName = "";
                    itemName = item.nombre.Replace("'", "").Trim();
                    String query = "UPDATE " + LocalDatabase.TABLA_ITEM + " SET " + LocalDatabase.CAMPO_NOMBRE_ITEM + " = @name, " +
                        LocalDatabase.CAMPO_CODIGO_ITEM + " = @code, " + LocalDatabase.CAMPO_CLASIFICATIONID1_ITEM + " = @clasificationId1, " +
                        LocalDatabase.CAMPO_CLASIFICATIONID2_ITEM + " = @clasificationId2, " + LocalDatabase.CAMPO_CLASIFICATIONID3_ITEM + " = @clasificationId3, " +
                        LocalDatabase.CAMPO_CLASIFICATIONID4_ITEM + " = @clasificationId4, " + LocalDatabase.CAMPO_CLASIFICATIONID5_ITEM + " = @clasificationId5, " +
                        LocalDatabase.CAMPO_CLASIFICATIONID6_ITEM + " = @clasificationId6, " + LocalDatabase.CAMPO_STOCK_ITEM + " = @stock, " +
                        LocalDatabase.CAMPO_ORDENAR_ITEM + " = @ordenar, " + LocalDatabase.CAMPO_CONSOLIDADO_ITEM + " = @consolidado, " +
                        LocalDatabase.CAMPO_DESCMAX_ITEM + " = @descuentoMaximo, " + LocalDatabase.CAMPO_PRECIO1_ITEM + " = @precio1, " +
                        LocalDatabase.CAMPO_PRECIO2_ITEM + " = @precio2, " + LocalDatabase.CAMPO_PRECIO3_ITEM + " = @precio3, " +
                        LocalDatabase.CAMPO_PRECIO4_ITEM + " = @precio4, " + LocalDatabase.CAMPO_PRECIO5_ITEM + " = @precio5, " +
                        LocalDatabase.CAMPO_PRECIO6_ITEM + " = @precio6, " + LocalDatabase.CAMPO_PRECIO7_ITEM + " = @precio7, " +
                        LocalDatabase.CAMPO_PRECIO8_ITEM + " = @precio8, " + LocalDatabase.CAMPO_PRECIO9_ITEM + " = @precio9, " +
                        LocalDatabase.CAMPO_PRECIO10_ITEM + " = @precio10, " + LocalDatabase.CAMPO_AGREGADO_ITEM + " = @agregado, " +
                        LocalDatabase.CAMPO_BASEUNITID_ITEM + " = @baseUnitId, " + LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_ITEM + " = @nonConvertibleUnitId, " +
                        LocalDatabase.CAMPO_PURCHASEUNITID_ITEM + " = @purchaseUnitId, " + LocalDatabase.CAMPO_SALESUNITID_ITEM + " = @salesUnitId, " +
                        LocalDatabase.CAMPO_CODIGOALTERNO_ITEM + " = @codigoAlterno, " + 
                        LocalDatabase.CAMPO_FISCALPRODUCT_ITEM + " = @fiscalProduct, " +
                        LocalDatabase.CAMPO_IMP1_ITEM+" = @imp1, "+
                        LocalDatabase.CAMPO_IMP2_ITEM + " = @imp2, " +
                        LocalDatabase.CAMPO_IMP3_ITEM + " = @imp3, " +
                        LocalDatabase.CAMPO_RETEN1_ITEM + " = @reten1, " +
                        LocalDatabase.CAMPO_RETEN2_ITEM + " = @reten2, " +
                        LocalDatabase.CAMPO_IMP1EXCENTO_ITEM + " = @imp1Excento, " +
                        LocalDatabase.CAMPO_IMP2CUOTA_ITEM + " = @imp2CuotaFija, " +
                        LocalDatabase.CAMPO_IMP3CUOTA_ITEM + " = @imp3CuotaFija, " +
                        LocalDatabase.CAMPO_TEXTEXTRA1_ITEM + " = @textExtra1, " +
                        LocalDatabase.CAMPO_TEXTEXTRA2_ITEM + " = @textExtra2, " +
                        LocalDatabase.CAMPO_TEXTEXTRA3_ITEM + " = @textExtra3, " +
                        LocalDatabase.CAMPO_IMPORTEEXTRA1_ITEM + " = @importeExtra1, " +
                        LocalDatabase.CAMPO_IMPORTEEXTRA2_ITEM + " = @importeExtra2, " +
                        LocalDatabase.CAMPO_IMPORTEEXTRA3_ITEM + " = @importeExtra3, " +
                        LocalDatabase.CAMPO_IMPORTEEXTRA4_ITEM + " = @importeExtra4, " +
                        LocalDatabase.CAMPO_CANTIDADFISCAL_ITEM + " = @cantidadFiscal " +
                        "WHERE " + LocalDatabase.CAMPO_ID_ITEM + " = " + item.id;
                    using (SQLiteCommand command = new SQLiteCommand(query, db))
                    {
                        command.Parameters.AddWithValue("@name", itemName.Trim());
                        command.Parameters.AddWithValue("@code", item.codigo);
                        command.Parameters.AddWithValue("@clasificationId1", item.clasificacionId1);
                        command.Parameters.AddWithValue("@clasificationId2", item.clasificacionId2);
                        command.Parameters.AddWithValue("@clasificationId3", item.clasificacionId3);
                        command.Parameters.AddWithValue("@clasificationId4", item.clasificacionId4);
                        command.Parameters.AddWithValue("@clasificationId5", item.clasificacionId5);
                        command.Parameters.AddWithValue("@clasificationId6", item.clasificacionId6);
                        command.Parameters.AddWithValue("@stock", item.existencia);
                        command.Parameters.AddWithValue("@ordenar", item.ordenar);
                        command.Parameters.AddWithValue("@consolidado", item.consolidado);
                        command.Parameters.AddWithValue("@descuentoMaximo", item.descuentoMaximo);
                        command.Parameters.AddWithValue("@precio1", item.precio1);
                        command.Parameters.AddWithValue("@precio2", item.precio2);
                        command.Parameters.AddWithValue("@precio3", item.precio3);
                        command.Parameters.AddWithValue("@precio4", item.precio4);
                        command.Parameters.AddWithValue("@precio5", item.precio5);
                        command.Parameters.AddWithValue("@precio6", item.precio6);
                        command.Parameters.AddWithValue("@precio7", item.precio7);
                        command.Parameters.AddWithValue("@precio8", item.precio8);
                        command.Parameters.AddWithValue("@precio9", item.precio9);
                        command.Parameters.AddWithValue("@precio10", item.precio10);
                        command.Parameters.AddWithValue("@agregado", 0);
                        command.Parameters.AddWithValue("@baseUnitId", item.baseUnitId);
                        command.Parameters.AddWithValue("@nonConvertibleUnitId", item.nonConvertibleUnitId);
                        command.Parameters.AddWithValue("@purchaseUnitId", item.purchaseUnitId);
                        command.Parameters.AddWithValue("@salesUnitId", item.salesUnitId);
                        command.Parameters.AddWithValue("@codigoAlterno", item.codigoAlterno);
                        command.Parameters.AddWithValue("@fiscalProduct", 1);
                        command.Parameters.AddWithValue("@imp1", item.imp1);
                        command.Parameters.AddWithValue("@imp2", item.imp2);
                        command.Parameters.AddWithValue("@imp3", item.imp3);
                        command.Parameters.AddWithValue("@reten1", item.reten1);
                        command.Parameters.AddWithValue("@reten2", item.reten2);
                        command.Parameters.AddWithValue("@imp1Excento", item.imp1Excento);
                        command.Parameters.AddWithValue("@imp2CuotaFija", item.imp2CuotaFija);
                        command.Parameters.AddWithValue("@imp3CuotaFija", item.imp3CuotaFija);
                        command.Parameters.AddWithValue("@textExtra1", item.textExtra1);
                        command.Parameters.AddWithValue("@textExtra2", item.textExtra2);
                        command.Parameters.AddWithValue("@textExtra3", item.textExtra3);
                        command.Parameters.AddWithValue("@importeExtra1", item.importeExtra1);
                        command.Parameters.AddWithValue("@importeExtra2", item.importeExtra2);
                        command.Parameters.AddWithValue("@importeExtra3", item.importeExtra3);
                        command.Parameters.AddWithValue("@importeExtra4", item.importeExtra4);
                        command.Parameters.AddWithValue("@cantidadFiscal", item.cantidadFiscal);
                        int recordUpdated = command.ExecuteNonQuery();
                        if (recordUpdated != 0)
                            updated = true;
                    }
                }
                else
                {
                    saveAItemLAN(item, db);
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog("Exception: " + e.ToString());
            }
            finally
            {
            }
            return updated;
        }

        public static Boolean checkIfItemExist(int idItem)
        {
            bool exist = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_ITEM + " WHERE " + LocalDatabase.CAMPO_ID_ITEM + " = " + idItem;
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
            catch (Exception Ex)
            {
                SECUDOC.writeLog(Ex.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return exist;
        }

        public static Boolean checkIfItemExist(SQLiteConnection dbLite, int idItem)
        {
            bool exist = false;
            try
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_ITEM + " WHERE " + LocalDatabase.CAMPO_ID_ITEM + " = " + idItem;
                using (SQLiteCommand command = new SQLiteCommand(query, dbLite))
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
            catch (Exception Ex)
            {
                SECUDOC.writeLog(Ex.ToString());
            }
            finally
            {
                
            }
            return exist;
        }

        public static Boolean checkIfRecordExist(SQLiteConnection db, String query)
        {
            bool exist = false;
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) {
                            exist = true;
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
            }
            return exist;
        }

        public static List<ItemModel> getallItems(String query)
        {
            List<ItemModel> itemsList = null;
            ItemModel item = null;
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
                            itemsList = new List<ItemModel>();
                            while (reader.Read())
                            {
                                item = new ItemModel();
                                item.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_ITEM].ToString().Trim());
                                item.nombre = reader[LocalDatabase.CAMPO_NOMBRE_ITEM].ToString().Trim();
                                item.codigo = reader[LocalDatabase.CAMPO_CODIGO_ITEM].ToString().Trim();
                                item.clasificacionId1 = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIFICATIONID1_ITEM].ToString().Trim());
                                item.clasificacionId2 = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIFICATIONID2_ITEM].ToString().Trim());
                                item.existencia = Convert.ToDouble(reader[LocalDatabase.CAMPO_STOCK_ITEM].ToString());
                                item.descuentoMaximo = Convert.ToInt32(reader[LocalDatabase.CAMPO_DESCMAX_ITEM].ToString().Trim());
                                item.baseUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_BASEUNITID_ITEM].ToString().Trim());
                                item.purchaseUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_PURCHASEUNITID_ITEM].ToString().Trim());
                                item.salesUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_SALESUNITID_ITEM].ToString().Trim());
                                item.precio1 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO1_ITEM].ToString().Trim());
                                item.precio2 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO2_ITEM].ToString().Trim());
                                item.precio3 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO3_ITEM].ToString().Trim());
                                item.precio4 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO4_ITEM].ToString().Trim());
                                item.precio5 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO5_ITEM].ToString().Trim());
                                item.precio6 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO6_ITEM].ToString().Trim());
                                item.precio7 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO7_ITEM].ToString().Trim());
                                item.precio8 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO8_ITEM].ToString().Trim());
                                item.precio9 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO9_ITEM].ToString().Trim());
                                item.precio10 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO10_ITEM].ToString().Trim());
                                item.codigoAlterno = reader[LocalDatabase.CAMPO_CODIGOALTERNO_ITEM].ToString().Trim();
                                if (reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_ITEM] == DBNull.Value)
                                    item.nonConvertibleUnitId = 0;
                                else item.nonConvertibleUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_ITEM].ToString().Trim());
                                item.fiscalProduct = Convert.ToInt32(reader[LocalDatabase.CAMPO_FISCALPRODUCT_ITEM].ToString().Trim());
                                item.imp1 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMP1_ITEM].ToString().Trim());
                                item.imp2 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMP2_ITEM].ToString().Trim());
                                item.imp3 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMP3_ITEM].ToString().Trim());
                                item.reten1 = Convert.ToDouble(reader[LocalDatabase.CAMPO_RETEN1_ITEM].ToString().Trim());
                                item.reten2 = Convert.ToDouble(reader[LocalDatabase.CAMPO_RETEN2_ITEM].ToString().Trim());
                                itemsList.Add(item);
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
            return itemsList;
        }

        public static List<ClsItemModel> getAllItemsWithParams(String query, String parameterName, String searchWord, int withParams, int matchPosition)
        {
            List<ClsItemModel> itemsList = null;
            ClsItemModel item = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    if (withParams == 1)
                    {
                        if (matchPosition == 0)
                            command.Parameters.AddWithValue("@" + parameterName, "%" + searchWord + "%");
                        else if (matchPosition == 1)
                            command.Parameters.AddWithValue("@" + parameterName, searchWord + "%");
                        else if (matchPosition == 2)
                            command.Parameters.AddWithValue("@" + parameterName, "%" + searchWord);
                    }
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            itemsList = new List<ClsItemModel>();
                            while (reader.Read())
                            {
                                item = new ClsItemModel();
                                item.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_ITEM].ToString().Trim());
                                item.nombre = reader[LocalDatabase.CAMPO_NOMBRE_ITEM].ToString().Trim();
                                item.codigo = reader[LocalDatabase.CAMPO_CODIGO_ITEM].ToString().Trim();
                                item.clasificacionId1 = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIFICATIONID1_ITEM].ToString().Trim());
                                item.clasificacionId2 = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIFICATIONID2_ITEM].ToString().Trim());
                                item.existencia = Convert.ToDouble(reader[LocalDatabase.CAMPO_STOCK_ITEM].ToString());
                                item.descuentoMaximo = Convert.ToInt32(reader[LocalDatabase.CAMPO_DESCMAX_ITEM].ToString().Trim());
                                item.baseUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_BASEUNITID_ITEM].ToString().Trim());
                                item.purchaseUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_PURCHASEUNITID_ITEM].ToString().Trim());
                                item.salesUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_SALESUNITID_ITEM].ToString().Trim());
                                item.precio1 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO1_ITEM].ToString().Trim());
                                item.precio2 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO2_ITEM].ToString().Trim());
                                item.precio3 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO3_ITEM].ToString().Trim());
                                item.precio4 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO4_ITEM].ToString().Trim());
                                item.precio5 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO5_ITEM].ToString().Trim());
                                item.precio6 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO6_ITEM].ToString().Trim());
                                item.precio7 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO7_ITEM].ToString().Trim());
                                item.precio8 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO8_ITEM].ToString().Trim());
                                item.precio9 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO9_ITEM].ToString().Trim());
                                item.precio10 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO10_ITEM].ToString().Trim());
                                item.codigoAlterno = reader[LocalDatabase.CAMPO_CODIGOALTERNO_ITEM].ToString().Trim();
                                if (reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_ITEM] == DBNull.Value)
                                    item.nonConvertibleUnitId = 0;
                                else item.nonConvertibleUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_ITEM].ToString().Trim());
                                item.imp1 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMP1_ITEM].ToString().Trim());
                                item.imp2 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMP2_ITEM].ToString().Trim());
                                item.imp3 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMP3_ITEM].ToString().Trim());
                                item.reten1 = Convert.ToDouble(reader[LocalDatabase.CAMPO_RETEN1_ITEM].ToString().Trim());
                                item.reten2 = Convert.ToDouble(reader[LocalDatabase.CAMPO_RETEN2_ITEM].ToString().Trim());
                                itemsList.Add(item);
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
            return itemsList;
        }

        public static int getIntValueForItems(String query)
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

        public static double getImpuesto(int idItem, int impuesto)
        {
            double imp = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT impuesto"+impuesto+" FROM "+LocalDatabase.TABLA_ITEM+" WHERE "+
                    LocalDatabase.CAMPO_ID_ITEM+" = @id";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", idItem);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    imp = Convert.ToDouble(reader.GetValue(0).ToString().Trim());
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
            return imp;
        }

        public static int getTotalNumberFromItems(String query, String parameterValue, String parameters, int withParameters, int matchPosition)
        {
            int value = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    if (withParameters == 1)
                    {
                        if (matchPosition == 0)
                            command.Parameters.AddWithValue("@" + parameterValue, "%" + parameters + "%");
                        else if (matchPosition == 1)
                            command.Parameters.AddWithValue("@" + parameterValue, parameters + "%");
                        else if (matchPosition == 2)
                            command.Parameters.AddWithValue("@" + parameterValue, "%" + parameters);
                    }
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

        public static ClsItemModel getAllDataFromAnItem(int idItem)
        {
            ClsItemModel item = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_ITEM + " WHERE " + LocalDatabase.CAMPO_ID_ITEM + " = @idItem";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idItem", idItem);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                item = new ClsItemModel();
                                item.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_ITEM].ToString().Trim());
                                item.nombre = reader[LocalDatabase.CAMPO_NOMBRE_ITEM].ToString().Trim();
                                item.codigo = reader[LocalDatabase.CAMPO_CODIGO_ITEM].ToString().Trim();
                                item.clasificacionId1 = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIFICATIONID1_ITEM].ToString().Trim());
                                item.clasificacionId2 = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIFICATIONID2_ITEM].ToString().Trim());
                                item.existencia = Convert.ToDouble(reader[LocalDatabase.CAMPO_STOCK_ITEM].ToString());
                                item.descuentoMaximo = Convert.ToInt32(reader[LocalDatabase.CAMPO_DESCMAX_ITEM].ToString().Trim());
                                item.baseUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_BASEUNITID_ITEM].ToString().Trim());
                                item.purchaseUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_PURCHASEUNITID_ITEM].ToString().Trim());
                                item.salesUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_SALESUNITID_ITEM].ToString().Trim());
                                item.precio1 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO1_ITEM].ToString().Trim());
                                item.precio2 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO2_ITEM].ToString().Trim());
                                item.precio3 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO3_ITEM].ToString().Trim());
                                item.precio4 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO4_ITEM].ToString().Trim());
                                item.precio5 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO5_ITEM].ToString().Trim());
                                item.precio6 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO6_ITEM].ToString().Trim());
                                item.precio7 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO7_ITEM].ToString().Trim());
                                item.precio8 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO8_ITEM].ToString().Trim());
                                item.precio9 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO9_ITEM].ToString().Trim());
                                item.precio10 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO10_ITEM].ToString().Trim());
                                item.codigoAlterno = reader[LocalDatabase.CAMPO_CODIGOALTERNO_ITEM].ToString().Trim();
                                if (reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_ITEM] == DBNull.Value)
                                    item.nonConvertibleUnitId = 0;
                                else item.nonConvertibleUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_ITEM].ToString().Trim());
                                item.imp1 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMP1_ITEM].ToString().Trim());
                                item.imp2 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMP2_ITEM].ToString().Trim());
                                item.imp3 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMP3_ITEM].ToString().Trim());
                                item.reten1 = Convert.ToDouble(reader[LocalDatabase.CAMPO_RETEN1_ITEM].ToString().Trim());
                                item.reten2 = Convert.ToDouble(reader[LocalDatabase.CAMPO_RETEN2_ITEM].ToString().Trim());
                                item.imp1Excento = Convert.ToInt32(reader[LocalDatabase.CAMPO_IMP1EXCENTO_ITEM].ToString().Trim());
                                item.imp2CuotaFija = Convert.ToInt32(reader[LocalDatabase.CAMPO_IMP2CUOTA_ITEM].ToString().Trim());
                                item.imp3CuotaFija = Convert.ToInt32(reader[LocalDatabase.CAMPO_IMP3CUOTA_ITEM].ToString().Trim());
                                item.textExtra1 = reader[LocalDatabase.CAMPO_TEXTEXTRA1_ITEM].ToString().Trim();
                                item.textExtra2 = reader[LocalDatabase.CAMPO_TEXTEXTRA2_ITEM].ToString().Trim();
                                item.textExtra3 = reader[LocalDatabase.CAMPO_TEXTEXTRA3_ITEM].ToString().Trim();
                                item.importeExtra1 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMPORTEEXTRA1_ITEM].ToString().Trim());
                                item.importeExtra2 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMPORTEEXTRA2_ITEM].ToString().Trim());
                                item.importeExtra3 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMPORTEEXTRA3_ITEM].ToString().Trim());
                                item.importeExtra4 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMPORTEEXTRA4_ITEM].ToString().Trim());
                                item.cantidadFiscal = Convert.ToDouble(reader[LocalDatabase.CAMPO_CANTIDADFISCAL_ITEM].ToString().Trim());
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
            return item;
        }

        public static ClsItemModel getAllDataFromAnItemLAN(String comInstance,String codeProduct)
        {
            String queryItem = "SELECT * FROM admProductos WHERE CCODIGOPRODUCTO = '" + codeProduct + "'";
            var dbCom = new SqlConnection();
            ClsItemModel item = null;
            var db = new SQLiteConnection();
            dbCom.ConnectionString = comInstance;
            dbCom.Open();
            try
            {
                
                bool preciosConIVA = ClsParametrosModel.isPreciosConIvaActivated(dbCom);
                using (SqlCommand commandItem = new SqlCommand(queryItem, dbCom))
                {
                    using (SqlDataReader drItem = commandItem.ExecuteReader())
                    {
                        if (drItem.HasRows)
                        {
                            
                            while (drItem.Read())
                            {
                                item = new ClsItemModel();
                                item.id = Convert.ToInt32(drItem["CIDPRODUCTO"].ToString().Trim());
                                item.nombre = drItem["CNOMBREPRODUCTO"].ToString().Trim();
                                item.codigo = drItem["CCODIGOPRODUCTO"].ToString().Trim();
                                item.clasificacionId1 = Convert.ToInt32(drItem["CIDVALORCLASIFICACION1"].ToString().Trim());
                                item.clasificacionId2 = Convert.ToInt32(drItem["CIDVALORCLASIFICACION2"].ToString().Trim());
                                //item.existencia = Convert.ToDouble(drItem[LocalDatabase.CAMPO_STOCK_ITEM].ToString());
                                //item.descuentoMaximo = Convert.ToInt32(drItem[LocalDatabase.CAMPO_DESCMAX_ITEM].ToString().Trim());
                                item.baseUnitId = Convert.ToInt32(drItem["CIDUNIDADBASE"].ToString().Trim());
                                item.purchaseUnitId = Convert.ToInt32(drItem["CIDUNIDADCOMPRA"].ToString().Trim());
                                item.salesUnitId = Convert.ToInt32(drItem["CIDUNIDADVENTA"].ToString().Trim());
                                item.precio1 = Convert.ToDouble(drItem["CPRECIO1"].ToString().Trim());
                                item.precio2 = Convert.ToDouble(drItem["CPRECIO2"].ToString().Trim());
                                item.precio3 = Convert.ToDouble(drItem["CPRECIO3"].ToString().Trim());
                                item.precio4 = Convert.ToDouble(drItem["CPRECIO4"].ToString().Trim());
                                item.precio5 = Convert.ToDouble(drItem["CPRECIO5"].ToString().Trim());
                                item.precio6 = Convert.ToDouble(drItem["CPRECIO6"].ToString().Trim());
                                item.precio7 = Convert.ToDouble(drItem["CPRECIO7"].ToString().Trim());
                                item.precio8 = Convert.ToDouble(drItem["CPRECIO8"].ToString().Trim());
                                item.precio9 = Convert.ToDouble(drItem["CPRECIO9"].ToString().Trim());
                                item.precio10 = Convert.ToDouble(drItem["CPRECIO10"].ToString().Trim());
                                item.codigoAlterno = drItem["CCODALTERN"].ToString().Trim();
                                if (drItem["CIDUNIDADNOCONVERTIBLE"] == DBNull.Value)
                                    item.nonConvertibleUnitId = 0;
                                else item.nonConvertibleUnitId = Convert.ToInt32(drItem["CIDUNIDADNOCONVERTIBLE"].ToString().Trim());
                                item.imp1 = Convert.ToDouble(drItem["CIMPUESTO1"].ToString().Trim());
                                item.imp2 = Convert.ToDouble(drItem["CIMPUESTO2"].ToString().Trim());
                                item.imp3 = Convert.ToDouble(drItem["CIMPUESTO3"].ToString().Trim());
                                item.reten1 = Convert.ToDouble(drItem["CRETENCION1"].ToString().Trim());
                                item.reten2 = Convert.ToDouble(drItem["CRETENCION2"].ToString().Trim());
                                item.imp1Excento = Convert.ToInt32(drItem["CESEXENTO"].ToString().Trim());
                                item.imp2CuotaFija = Convert.ToInt32(drItem["CESCUOTAI2"].ToString().Trim());
                                item.imp3CuotaFija = Convert.ToInt32(drItem["CESCUOTAI3"].ToString().Trim());
                                item.textExtra1 = drItem["CTEXTOEXTRA1"].ToString().Trim();
                                item.textExtra2 = drItem["CTEXTOEXTRA2"].ToString().Trim();
                                item.textExtra3 = drItem["CTEXTOEXTRA3"].ToString().Trim();
                                item.importeExtra1 = Convert.ToDouble(drItem["CIMPORTEEXTRA1"].ToString().Trim());
                                item.importeExtra2 = Convert.ToDouble(drItem["CIMPORTEEXTRA2"].ToString().Trim());
                                item.importeExtra3 = Convert.ToDouble(drItem["CIMPORTEEXTRA3"].ToString().Trim());
                                item.importeExtra4 = Convert.ToDouble(drItem["CIMPORTEEXTRA4"].ToString().Trim());
                                item.cantidadFiscal = Convert.ToDouble(drItem["CCANTIDADFISCAL"].ToString().Trim());

                            }
                        }
                        
                        if (drItem != null && !drItem.IsClosed)
                            drItem.Close();
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
            return item;
        }

        public static ClsItemModel getAllDataFromAnItemWithCode(String itemCode)
        {
            ClsItemModel item = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_ITEM + " WHERE " + LocalDatabase.CAMPO_CODIGO_ITEM + " = @itemCode";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@itemCode", itemCode);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                item = new ClsItemModel();
                                item.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_ITEM].ToString().Trim());
                                item.nombre = reader[LocalDatabase.CAMPO_NOMBRE_ITEM].ToString().Trim();
                                item.codigo = reader[LocalDatabase.CAMPO_CODIGO_ITEM].ToString().Trim();
                                item.clasificacionId1 = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIFICATIONID1_ITEM].ToString().Trim());
                                item.clasificacionId2 = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIFICATIONID2_ITEM].ToString().Trim());
                                item.existencia = Convert.ToDouble(reader[LocalDatabase.CAMPO_STOCK_ITEM].ToString());
                                item.descuentoMaximo = Convert.ToInt32(reader[LocalDatabase.CAMPO_DESCMAX_ITEM].ToString().Trim());
                                item.baseUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_BASEUNITID_ITEM].ToString().Trim());
                                item.purchaseUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_PURCHASEUNITID_ITEM].ToString().Trim());
                                item.salesUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_SALESUNITID_ITEM].ToString().Trim());
                                item.precio1 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO1_ITEM].ToString().Trim());
                                item.precio2 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO2_ITEM].ToString().Trim());
                                item.precio3 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO3_ITEM].ToString().Trim());
                                item.precio4 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO4_ITEM].ToString().Trim());
                                item.precio5 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO5_ITEM].ToString().Trim());
                                item.precio6 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO6_ITEM].ToString().Trim());
                                item.precio7 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO7_ITEM].ToString().Trim());
                                item.precio8 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO8_ITEM].ToString().Trim());
                                item.precio9 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO9_ITEM].ToString().Trim());
                                item.precio10 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO10_ITEM].ToString().Trim());
                                item.codigoAlterno = reader[LocalDatabase.CAMPO_CODIGOALTERNO_ITEM].ToString().Trim();
                                if (reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_ITEM] == DBNull.Value)
                                    item.nonConvertibleUnitId = 0;
                                else item.nonConvertibleUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_ITEM].ToString().Trim());
                                item.imp1 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMP1_ITEM].ToString().Trim());
                                item.imp2 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMP2_ITEM].ToString().Trim());
                                item.imp3 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMP3_ITEM].ToString().Trim());
                                item.reten1 = Convert.ToDouble(reader[LocalDatabase.CAMPO_RETEN1_ITEM].ToString().Trim());
                                item.reten2 = Convert.ToDouble(reader[LocalDatabase.CAMPO_RETEN2_ITEM].ToString().Trim());
                                item.imp1Excento = Convert.ToInt32(reader[LocalDatabase.CAMPO_IMP1EXCENTO_ITEM].ToString().Trim());
                                item.imp2CuotaFija = Convert.ToInt32(reader[LocalDatabase.CAMPO_IMP2CUOTA_ITEM].ToString().Trim());
                                item.imp3CuotaFija = Convert.ToInt32(reader[LocalDatabase.CAMPO_IMP3CUOTA_ITEM].ToString().Trim());
                                item.textExtra1 = reader[LocalDatabase.CAMPO_TEXTEXTRA1_ITEM].ToString().Trim();
                                item.textExtra2 = reader[LocalDatabase.CAMPO_TEXTEXTRA2_ITEM].ToString().Trim();
                                item.textExtra3 = reader[LocalDatabase.CAMPO_TEXTEXTRA3_ITEM].ToString().Trim();
                                item.importeExtra1 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMPORTEEXTRA1_ITEM].ToString().Trim());
                                item.importeExtra2 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMPORTEEXTRA2_ITEM].ToString().Trim());
                                item.importeExtra3 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMPORTEEXTRA3_ITEM].ToString().Trim());
                                item.importeExtra4 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMPORTEEXTRA4_ITEM].ToString().Trim());
                                item.cantidadFiscal = Convert.ToDouble(reader[LocalDatabase.CAMPO_CANTIDADFISCAL_ITEM].ToString().Trim());
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
            return item;
        }

        public static ClsItemModel getAllDataFromAnItemWithBarCode(String itemCode)
        {
            ClsItemModel item = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_ITEM + " WHERE " + LocalDatabase.CAMPO_CODIGOALTERNO_ITEM + " = @itemCode";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@itemCode", itemCode);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                item = new ClsItemModel();
                                item.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_ITEM].ToString().Trim());
                                item.nombre = reader[LocalDatabase.CAMPO_NOMBRE_ITEM].ToString().Trim();
                                item.codigo = reader[LocalDatabase.CAMPO_CODIGO_ITEM].ToString().Trim();
                                item.clasificacionId1 = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIFICATIONID1_ITEM].ToString().Trim());
                                item.clasificacionId2 = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIFICATIONID2_ITEM].ToString().Trim());
                                item.existencia = Convert.ToDouble(reader[LocalDatabase.CAMPO_STOCK_ITEM].ToString());
                                item.descuentoMaximo = Convert.ToInt32(reader[LocalDatabase.CAMPO_DESCMAX_ITEM].ToString().Trim());
                                item.baseUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_BASEUNITID_ITEM].ToString().Trim());
                                item.purchaseUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_PURCHASEUNITID_ITEM].ToString().Trim());
                                item.salesUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_SALESUNITID_ITEM].ToString().Trim());
                                item.precio1 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO1_ITEM].ToString().Trim());
                                item.precio2 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO2_ITEM].ToString().Trim());
                                item.precio3 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO3_ITEM].ToString().Trim());
                                item.precio4 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO4_ITEM].ToString().Trim());
                                item.precio5 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO5_ITEM].ToString().Trim());
                                item.precio6 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO6_ITEM].ToString().Trim());
                                item.precio7 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO7_ITEM].ToString().Trim());
                                item.precio8 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO8_ITEM].ToString().Trim());
                                item.precio9 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO9_ITEM].ToString().Trim());
                                item.precio10 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO10_ITEM].ToString().Trim());
                                item.codigoAlterno = reader[LocalDatabase.CAMPO_CODIGOALTERNO_ITEM].ToString().Trim();
                                if (reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_ITEM] == DBNull.Value)
                                    item.nonConvertibleUnitId = 0;
                                else item.nonConvertibleUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_ITEM].ToString().Trim());
                                item.imp1 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMP1_ITEM].ToString().Trim());
                                item.imp2 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMP2_ITEM].ToString().Trim());
                                item.imp3 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMP3_ITEM].ToString().Trim());
                                item.reten1 = Convert.ToDouble(reader[LocalDatabase.CAMPO_RETEN1_ITEM].ToString().Trim());
                                item.reten2 = Convert.ToDouble(reader[LocalDatabase.CAMPO_RETEN2_ITEM].ToString().Trim());
                                item.imp1Excento = Convert.ToInt32(reader[LocalDatabase.CAMPO_IMP1EXCENTO_ITEM].ToString().Trim());
                                item.imp2CuotaFija = Convert.ToInt32(reader[LocalDatabase.CAMPO_IMP2CUOTA_ITEM].ToString().Trim());
                                item.imp3CuotaFija = Convert.ToInt32(reader[LocalDatabase.CAMPO_IMP3CUOTA_ITEM].ToString().Trim());
                                item.textExtra1 = reader[LocalDatabase.CAMPO_TEXTEXTRA1_ITEM].ToString().Trim();
                                item.textExtra2 = reader[LocalDatabase.CAMPO_TEXTEXTRA2_ITEM].ToString().Trim();
                                item.textExtra3 = reader[LocalDatabase.CAMPO_TEXTEXTRA3_ITEM].ToString().Trim();
                                item.importeExtra1 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMPORTEEXTRA1_ITEM].ToString().Trim());
                                item.importeExtra2 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMPORTEEXTRA2_ITEM].ToString().Trim());
                                item.importeExtra3 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMPORTEEXTRA3_ITEM].ToString().Trim());
                                item.importeExtra4 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMPORTEEXTRA4_ITEM].ToString().Trim());
                                item.cantidadFiscal = Convert.ToDouble(reader[LocalDatabase.CAMPO_CANTIDADFISCAL_ITEM].ToString().Trim());
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
            return item;
        }

        public static int getDefaultPriceFromACustomer(int clienteId)
        {
            int listaDePrecio = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_PRECIOEMPRESA_ID_CLIENTE + " FROM " +
                                LocalDatabase.TABLA_CLIENTES + " WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " = @clienteId";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@clienteId", clienteId);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    listaDePrecio = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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
            return listaDePrecio;
        }

        public static int getDefaultPriceFromAnAgent(int agentId)
        {
            int listaDePrecio = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_PRECIO_EMPRESA_ID_USUARIO + " FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " +
                                LocalDatabase.CAMPO_ID_USUARIO + " = @agentId";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@agentId", agentId);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    listaDePrecio = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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
            return listaDePrecio;
        }

        public static double getAmountForAPrice(int itemId, int numberPrice)
        {
            double price = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query1 = "SELECT precio_" + numberPrice + " FROM " + LocalDatabase.TABLA_ITEM +
                                                " WHERE " + LocalDatabase.CAMPO_ID_ITEM + " = " + itemId;
                using (SQLiteCommand command = new SQLiteCommand(query1, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    price = Convert.ToDouble(reader.GetValue(0).ToString().Trim());
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
            return price;
        }

        public static List<ClsItemModel> getAllItemsWithKeyOrNameSimilars(String claveNom, int documentType)
        {
            List<ClsItemModel> itemList = null;
            ClsItemModel item;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_ITEM + " WHERE " + LocalDatabase.CAMPO_CODIGO_ITEM
                        + " LIKE '%" + claveNom + "%' OR " + LocalDatabase.CAMPO_NOMBRE_ITEM + " LIKE '%" + claveNom + "%' LIMIT " + 100;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    //command.Parameters.AddWithValue("@code", );
                    //command.Parameters.AddWithValue("@name", claveNom);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            itemList = new List<ClsItemModel>();
                            while (reader.Read())
                            {
                                item = new ClsItemModel();
                                item.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_ITEM].ToString().Trim());
                                item.nombre = reader[LocalDatabase.CAMPO_NOMBRE_ITEM].ToString().Trim();
                                item.codigo = reader[LocalDatabase.CAMPO_CODIGO_ITEM].ToString().Trim();
                                item.clasificacionId1 = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIFICATIONID1_ITEM].ToString().Trim());
                                item.clasificacionId2 = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIFICATIONID2_ITEM].ToString().Trim());
                                item.existencia = Convert.ToDouble(reader[LocalDatabase.CAMPO_STOCK_ITEM].ToString());
                                item.descuentoMaximo = Convert.ToInt32(reader[LocalDatabase.CAMPO_DESCMAX_ITEM].ToString().Trim());
                                item.baseUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_BASEUNITID_ITEM].ToString().Trim());
                                item.purchaseUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_PURCHASEUNITID_ITEM].ToString().Trim());
                                item.salesUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_SALESUNITID_ITEM].ToString().Trim());
                                item.precio1 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO1_ITEM].ToString().Trim());
                                item.precio2 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO2_ITEM].ToString().Trim());
                                item.precio3 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO3_ITEM].ToString().Trim());
                                item.precio4 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO4_ITEM].ToString().Trim());
                                item.precio5 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO5_ITEM].ToString().Trim());
                                item.precio6 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO6_ITEM].ToString().Trim());
                                item.precio7 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO7_ITEM].ToString().Trim());
                                item.precio8 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO8_ITEM].ToString().Trim());
                                item.precio9 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO9_ITEM].ToString().Trim());
                                item.precio10 = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRECIO10_ITEM].ToString().Trim());
                                item.codigoAlterno = reader[LocalDatabase.CAMPO_CODIGOALTERNO_ITEM].ToString().Trim();
                                if (reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_ITEM] == DBNull.Value)
                                    item.nonConvertibleUnitId = 0;
                                else item.nonConvertibleUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_ITEM].ToString().Trim());
                                item.imp1 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMP1_ITEM].ToString().Trim());
                                item.imp2 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMP2_ITEM].ToString().Trim());
                                item.imp3 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMP3_ITEM].ToString().Trim());
                                item.reten1 = Convert.ToDouble(reader[LocalDatabase.CAMPO_RETEN1_ITEM].ToString().Trim());
                                item.reten2 = Convert.ToDouble(reader[LocalDatabase.CAMPO_RETEN2_ITEM].ToString().Trim());
                                item.imp1Excento = Convert.ToInt32(reader[LocalDatabase.CAMPO_IMP1EXCENTO_ITEM].ToString().Trim());
                                item.imp2CuotaFija = Convert.ToInt32(reader[LocalDatabase.CAMPO_IMP2CUOTA_ITEM].ToString().Trim());
                                item.imp3CuotaFija = Convert.ToInt32(reader[LocalDatabase.CAMPO_IMP3CUOTA_ITEM].ToString().Trim());
                                item.textExtra1 = reader[LocalDatabase.CAMPO_TEXTEXTRA1_ITEM].ToString().Trim();
                                item.textExtra2 = reader[LocalDatabase.CAMPO_TEXTEXTRA2_ITEM].ToString().Trim();
                                item.textExtra3 = reader[LocalDatabase.CAMPO_TEXTEXTRA3_ITEM].ToString().Trim();
                                item.importeExtra1 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMPORTEEXTRA1_ITEM].ToString().Trim());
                                item.importeExtra2 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMPORTEEXTRA2_ITEM].ToString().Trim());
                                item.importeExtra3 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMPORTEEXTRA3_ITEM].ToString().Trim());
                                item.importeExtra4 = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMPORTEEXTRA4_ITEM].ToString().Trim());
                                item.cantidadFiscal = Convert.ToDouble(reader[LocalDatabase.CAMPO_CANTIDADFISCAL_ITEM].ToString().Trim());
                                itemList.Add(item);
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
            return itemList;
        }

        public static int getBaseUnitId(int idItem)
        {
            int baseUnitId = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_BASEUNITID_ITEM + " FROM " +
                        LocalDatabase.TABLA_ITEM + " WHERE " + LocalDatabase.CAMPO_ID_ITEM + " = " + idItem;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                baseUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_BASEUNITID_ITEM].ToString().Trim());
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
            return baseUnitId;
        }

        public static int getPurchaseUnitId(int idItem)
        {
            int purchaseUnitId = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_PURCHASEUNITID_ITEM + " FROM " +
                        LocalDatabase.TABLA_ITEM + " WHERE " + LocalDatabase.CAMPO_ID_ITEM + " = " + idItem;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                purchaseUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_PURCHASEUNITID_ITEM].ToString().Trim());
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
            return purchaseUnitId;
        }

        public static int getSalesUnitId(int idItem)
        {
            int salesUnitId = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_SALESUNITID_ITEM + " FROM " +
                        LocalDatabase.TABLA_ITEM + " WHERE " + LocalDatabase.CAMPO_ID_ITEM + " = " + idItem;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader[LocalDatabase.CAMPO_SALESUNITID_ITEM] != DBNull.Value)
                                    salesUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_SALESUNITID_ITEM].ToString().Trim());
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
            return salesUnitId;
        }

        public static int getNonConvertibleUnitId(int idItem)
        {
            int noConvertibleUnitId = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_ITEM + " FROM " +
                        LocalDatabase.TABLA_ITEM + " WHERE " + LocalDatabase.CAMPO_ID_ITEM + " = @idItem";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idItem", idItem);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                noConvertibleUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_ITEM].ToString().Trim());
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
            return noConvertibleUnitId;
        }

        public static async Task<List<ClsPreciosEmpresaModel>> getAllPricesForAnItemAPILAN(ClsItemModel im, bool serverModeLAN, String codigoCaja)
        {
            List<ClsPreciosEmpresaModel> priceList = null;
            await Task.Run(async () =>
            {
                if (serverModeLAN)
                {
                    dynamic responsePrices = await ItemsController.getAllPricesForAnItemLAN(im.id, im.imp1, im.imp2, im.imp3,
                        im.imp1Excento, im.imp2CuotaFija, im.imp3CuotaFija, im.cantidadFiscal, im.reten1, im.reten2, codigoCaja);
                    if (responsePrices.value == 1)
                        priceList = responsePrices.pricesList;
                } else
                {
                    List<String> priceNameList = PreciosempresaModel.obtenerListaDeNombreDePrecios();
                    if (priceNameList == null || (priceNameList != null && priceNameList.Count <= 0))
                    {
                        await PreciosEmpresaController.downloadAllPreciosEmpresaAPI();
                    }
                    priceNameList = PreciosempresaModel.obtenerListaDeNombreDePrecios();
                    ClsPreciosEmpresaModel pem;
                    var db = new SQLiteConnection();
                    try
                    {
                        db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                        db.Open();
                        int precios = 0;
                        if (priceNameList != null)
                            precios = priceNameList.Count;
                        String query = "SELECT " + LocalDatabase.CAMPO_PRECIO1_ITEM + ", " + LocalDatabase.CAMPO_PRECIO2_ITEM + ", " +
                                LocalDatabase.CAMPO_PRECIO3_ITEM + ", " + LocalDatabase.CAMPO_PRECIO4_ITEM + ", " + LocalDatabase.CAMPO_PRECIO5_ITEM + ", " +
                                LocalDatabase.CAMPO_PRECIO6_ITEM + ", " + LocalDatabase.CAMPO_PRECIO7_ITEM + ", " + LocalDatabase.CAMPO_PRECIO8_ITEM + ", " +
                                LocalDatabase.CAMPO_PRECIO9_ITEM + ", " + LocalDatabase.CAMPO_PRECIO10_ITEM + " FROM " + LocalDatabase.TABLA_ITEM + " WHERE " +
                                LocalDatabase.CAMPO_ID_ITEM + " = " + im.id;
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            using (SQLiteDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    priceList = new List<ClsPreciosEmpresaModel>();
                                    while (reader.Read())
                                    {
                                        for (int i = 0; i < precios; i++)
                                        {
                                            pem = new ClsPreciosEmpresaModel();
                                            pem.NOMBRE = priceNameList[i];
                                            pem.precioImporte = (Convert.ToDouble(reader.GetValue(-1 + (i + 1)).ToString().Trim()));
                                            priceList.Add(pem);
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
                }
            });
            return priceList;
        }

        public static double getTheCurrentExistenceOfAnItem(int idItem)
        {
            double existencia = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_STOCK_ITEM + " FROM " +
                        LocalDatabase.TABLA_ITEM + " WHERE " + LocalDatabase.CAMPO_ID_ITEM + " = @id";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", idItem);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    existencia = Convert.ToDouble(reader[LocalDatabase.CAMPO_STOCK_ITEM].ToString().Trim());
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
            return existencia;
        }

        public static int getCapturedUnitType(int idItem, int capturedUnitId)
        {
            int type = 4;
            int baseUnitId = getBaseUnitId(idItem);
            int purchaseUnitId = getPurchaseUnitId(idItem);
            int salesUnitId = getSalesUnitId(idItem);
            if (baseUnitId == capturedUnitId)
            {
                type = 1;
            }
            else if (purchaseUnitId == capturedUnitId)
            {
                type = 2;
            }
            else if (salesUnitId == capturedUnitId)
            {
                type = 3;
            }
            else
            {
                type = 4;
            }
            return type;
        }

        public static String getCodeForAnItem(int id)
        {
            String code = "";
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_CODIGO_ITEM + " FROM " + LocalDatabase.TABLA_ITEM + " WHERE " +
                        LocalDatabase.CAMPO_ID_ITEM + " = " + id;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                code = reader[LocalDatabase.CAMPO_CODIGO_ITEM].ToString().Trim();
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
            return code;
        }

        public static String getTheNameOfAnItem(int idItem)
        {
            String nombre = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_NOMBRE_ITEM + " FROM " + LocalDatabase.TABLA_ITEM +
                        " WHERE " + LocalDatabase.CAMPO_ID_ITEM + " = @idItem";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idItem", idItem);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    nombre = reader[LocalDatabase.CAMPO_NOMBRE_ITEM].ToString().Trim();
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
            return nombre;
        }

        public static double getExistenciaForAItemActualizarDatos(int id)
        {
            double existencia = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_STOCK_ITEM + " FROM " + LocalDatabase.TABLA_ITEM +
                        " WHERE " + LocalDatabase.CAMPO_ID_ITEM + " = " + id;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                existencia = Convert.ToDouble(reader[LocalDatabase.CAMPO_STOCK_ITEM].ToString().Trim());
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
            return existencia;
        }

        public static Boolean changeExistenceToAnItem(int idItem, double existencia)
        {
            Boolean changed = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_ITEM + " SET " + LocalDatabase.CAMPO_STOCK_ITEM + " = @stock WHERE " +
                    LocalDatabase.CAMPO_ID_ITEM + " = @idItem";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@stock", existencia);
                    command.Parameters.AddWithValue("@idItem", idItem);
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

        public static Boolean addAnItemToCart(int idArt)
        {
            Boolean added = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "UPDATE " + LocalDatabase.TABLA_ITEM + " SET " + LocalDatabase.CAMPO_AGREGADO_ITEM + " = " + 1 + " WHERE " + LocalDatabase.CAMPO_ID_ITEM + " = " + idArt;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        added = true;
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
            return added;
        }

        public static double getDescuentoMaximoOfTheAnItem(int idItem)
        {
            double descMex = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_DESCMAX_ITEM + " FROM " + LocalDatabase.TABLA_ITEM + " WHERE " +
                        LocalDatabase.CAMPO_ID_ITEM + " = " + idItem;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                descMex = Convert.ToDouble(reader[LocalDatabase.CAMPO_DESCMAX_ITEM].ToString().Trim());
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
            return descMex;
        }

        public static Boolean changeTheExistenceOfAnItem(int idItem, double existencia)
        {
            Boolean changed = false;
            var db = new SQLiteConnection();
            try {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_ITEM + " SET " + LocalDatabase.CAMPO_STOCK_ITEM + " = @stock WHERE " +
                    LocalDatabase.CAMPO_ID_ITEM + " = @idItem";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@stock", existencia);
                    command.Parameters.AddWithValue("@idItem", idItem);
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

        public static int quitarTodosLosArticulosDelCarrito()
        {
            int quitado = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_ITEM + " SET " + LocalDatabase.CAMPO_AGREGADO_ITEM + " = @added";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@added", 0);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        quitado = records;
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
            return quitado;
        }

        public static Boolean removeAnItemFromTheCart(int idArt)
        {
            Boolean removed = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "UPDATE " + LocalDatabase.TABLA_ITEM + " SET " + LocalDatabase.CAMPO_AGREGADO_ITEM + " = " + 0 + " WHERE " +
                    LocalDatabase.CAMPO_ID_ITEM + " = " + idArt;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        removed = true;
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
            return removed;
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

        public static Boolean updateStockFromTheServer(int idItem, double serverStock)
        {
            Boolean removed = false;
            double localStock = getDoubleValue("SELECT "+LocalDatabase.CAMPO_STOCK_ITEM+" FROM "+LocalDatabase.TABLA_ITEM+" WHERE "+
                LocalDatabase.CAMPO_ID_ITEM+" = "+
                idItem);
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_ITEM + " SET " + LocalDatabase.CAMPO_STOCK_ITEM + " = " + serverStock + " WHERE " +
                    LocalDatabase.CAMPO_ID_ITEM + " = " + idItem;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        removed = true;
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
            return removed;
        }

        public static bool updateStock(int idItem, double newStock)
        {
            bool updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_ITEM + " SET " + LocalDatabase.CAMPO_STOCK_ITEM + " = @newStock WHERE " +
                    LocalDatabase.CAMPO_ID_ITEM + " = @idItem";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@newStock", newStock);
                    command.Parameters.AddWithValue("@idItem", idItem);
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

        public static async Task<bool> getFiscalItemFieldValue(ClsItemModel im, int positionFiscalItemField)
        {
            bool fiscal = false;
            await Task.Run(async () =>
            {
                if (positionFiscalItemField == 0)
                {
                    if (im.textExtra1.Equals("") || im.textExtra1.Equals("0"))
                        fiscal = true;
                }
                else if (positionFiscalItemField == 1)
                {
                    if (im.textExtra2.Equals("") || im.textExtra2.Equals("0"))
                        fiscal = true;
                } else if (positionFiscalItemField == 2)
                {
                    if (im.textExtra3.Equals("") || im.textExtra3.Equals("0"))
                        fiscal = true;
                } else if (positionFiscalItemField == 3)
                {
                    if (im.importeExtra1 == 0)
                        fiscal = true;
                } else if (positionFiscalItemField == 4)
                {
                    if (im.importeExtra2 == 0)
                        fiscal = true;
                } else if (positionFiscalItemField == 5)
                {
                    if (im.importeExtra3 == 0)
                        fiscal = true;
                } else
                {
                    if (im.importeExtra4 == 0)
                        fiscal = true;
                }
            });
            return fiscal;
        }

        public static async Task<String> getNameForFiscalItemField(int positionFiscalItemField)
        {
            String name = "";
            await Task.Run(async () =>
            {
                if (positionFiscalItemField == 0)
                    name = "Texto Extra 1";
                else if (positionFiscalItemField == 1)
                    name = "Texto Extra 2";
                else if (positionFiscalItemField == 2)
                    name = "Texto Extra 3";
                else if (positionFiscalItemField == 3)
                    name = "Importe Extra 1";
                else if (positionFiscalItemField == 4)
                    name = "Importe Extra 2";
                else if (positionFiscalItemField == 5)
                    name = "Importe Extra 3";
                else name = "Importe Extra 4";
            });
            return name;
        }

        public static async Task<String> getTableNameForFiscalItemField(int positionFiscalItemField)
        {
            String name = "";
            await Task.Run(async () =>
            {
                if (positionFiscalItemField == 0)
                    name = "textoExtra1";
                else if (positionFiscalItemField == 1)
                    name = "textoExtra2";
                else if (positionFiscalItemField == 2)
                    name = "textoExtra3";
                else if (positionFiscalItemField == 3)
                    name = "importeExtra1";
                else if (positionFiscalItemField == 4)
                    name = "importeExtra2";
                else if (positionFiscalItemField == 5)
                    name = "importeExtra3";
                else name = "importeExtra4";
            });
            return name;
        }

        public static Boolean deleteAllItemsInLocalDb()
        {
            bool deleted = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "DELETE FROM " + LocalDatabase.TABLA_ITEM;
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



    public class clsImpuesto
    {
        public string CUSAIMPUESTO { set; get; }
        public string CUSAPORCENTAJEIMPUESTO { set; get; }
        public string CIDFORMULAPORCIMPUESTO { set; get; }
        public string CIDFORMULAIMPUESTO { set; get; }
    }

    public class clsDatosProducto
    {
        public double Impuesto1 { set; get; }
        public string Imp1Exento { set; get; }
        public double Impuesto2 { set; get; }
        public string Imp2CuotaFija { set; get; }
        public double Impuesto3 { set; get; }
        public string Imp3CuotaFija { set; get; }
        public double CantidadFiscal { set; get; }
        public double Retencion1 { set; get; }
        public double Retencion2 { set; get; }
    }

    public class UpdateStocksResponse
    {
        public int stocks { get; set; }
    }

    public class ItemsStockResponse
    {
        public List<Stocks> stocks { get; set; }
    }

    public class Stocks
    {
        public int id { get; set; }
        public double stock { get; set; }
        public int baseUnitId { get; set; }
        public int itemId { get; set; }
        public int almacenId { get; set; }
        public int empresaId { get; set; }
    }
}

