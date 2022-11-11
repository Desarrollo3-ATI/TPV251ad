using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Dynamic;
using wsROMClase;
using wsROMClases;

namespace SyncTPV.Models
{
    public class PromotionsModel
    {
        public int id { get; set; }
        public String codigo { get; set; }
        public String nombre { get; set; }
        public String fechaInicio { get; set; }
        public String fechaFin { get; set; }
        public double volumenMinimo { get; set; }
        public double volumenMaximo { get; set; }
        public double porcentajeDescuento { get; set; }
        public int clasiClienteIdUno { get; set; }
        public int clasiClienteIdDos { get; set; }
        public int clasiClienteIdTres { get; set; }
        public int clasiClienteIdCuatro { get; set; }
        public int clasiClienteIdCinco { get; set; }
        public int clasiClienteIdSeis { get; set; }
        public int clasiProductoIdUno { get; set; }
        public int clasiProductoIdDos { get; set; }
        public int clasiProductoIdTres { get; set; }
        public int clasiProductoIdCuatro { get; set; }
        public int clasiProductoIdCinco { get; set; }
        public int clasiProductoIdSeis { get; set; }
        public String creado { get; set; }
        public int tipoPromocion { get; set; }
        public int conceptoDocumentoId { get; set; }
        public int subtipoPromocion { get; set; }
        public String horaInicio { get; set; }
        public String horaFin { get; set; }
        public int tipoPro { get; set; }
        public int valA { get; set; }
        public int valB { get; set; }
        public int dias { get; set; }
        public String fechaAlta { get; set; }
        public int status { get; set; }

        public static int saveAllPromotions(List<ClsPromocionesModel> promosList)
        {
            int count = 0;
            if (promosList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (var promotion in promosList)
                    {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_PROMOCIONES + " VALUES(@id, @codigo, @nombre, @fechaInicio, " +
                            "@fechaFin, @volumenMinimo, @volumenMaximo, @porcentajeDescuento, @clasiClienteIdUno, @clasiClienteIdDos, " +
                            "@clasiClienteIdTres, @clasiClienteIdCuatro, @clasiClienteIdCinco, @clasiClienteIdSeis, @clasiProductoIdUno, " +
                            "@clasiProductoIdDos, @clasiProductoIdTres, @clasiProductoIdCuatro, @clasiProductoIdCinco, @clasiProductoIdSeis, " +
                            "@creado, @tipoPromocion, @conceptoDocumentoId, @subtipoPromocion, @horaInicio, @horaFin, @tipoPro, " +
                            "@valA, @valB, @dias, @fechaAlta, @status)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@id", promotion.id);
                            command.Parameters.AddWithValue("@codigo", promotion.code);
                            command.Parameters.AddWithValue("@nombre", promotion.nombre);
                            command.Parameters.AddWithValue("@fechaInicio", promotion.fechaInicio);
                            command.Parameters.AddWithValue("@fechaFin", promotion.fechaFin);
                            command.Parameters.AddWithValue("@volumenMinimo", promotion.volumenMinimo);
                            command.Parameters.AddWithValue("@volumenMaximo", promotion.volumenMaximo);
                            command.Parameters.AddWithValue("@porcentajeDescuento", promotion.porcentajeDescuento);
                            command.Parameters.AddWithValue("@clasiClienteIdUno", promotion.clasiClienteIdUno);
                            command.Parameters.AddWithValue("@clasiClienteIdDos", promotion.clasiClienteIdDos);
                            command.Parameters.AddWithValue("@clasiClienteIdTres", promotion.clasiClienteIdTres);
                            command.Parameters.AddWithValue("@clasiClienteIdCuatro", promotion.clasiClienteIdCuatro);
                            command.Parameters.AddWithValue("@clasiClienteIdCinco", promotion.clasiClienteIdCinco);
                            command.Parameters.AddWithValue("@clasiClienteIdSeis", promotion.clasiClienteIdSeis);
                            command.Parameters.AddWithValue("@clasiProductoIdUno", promotion.clasiProductoIdUno);
                            command.Parameters.AddWithValue("@clasiProductoIdDos", promotion.clasiProductoIdDos);
                            command.Parameters.AddWithValue("@clasiProductoIdTres", promotion.clasiProductoIdTres);
                            command.Parameters.AddWithValue("@clasiProductoIdCuatro", promotion.clasiProductoIdCuatro);
                            command.Parameters.AddWithValue("@clasiProductoIdCinco", promotion.clasiProductoIdCinco);
                            command.Parameters.AddWithValue("@clasiProductoIdSeis", promotion.clasiProductoIdSeis);
                            command.Parameters.AddWithValue("@creado", promotion.creado);
                            command.Parameters.AddWithValue("@tipoPromocion", promotion.tipoPromocion);
                            command.Parameters.AddWithValue("@conceptoDocumentoId", promotion.conceptoDocumentoId);
                            command.Parameters.AddWithValue("@subtipoPromocion", promotion.subtipoPromocion);
                            command.Parameters.AddWithValue("@horaInicio", promotion.horaInicio);
                            command.Parameters.AddWithValue("@horaFin", promotion.horaFin);
                            command.Parameters.AddWithValue("@tipoPro", promotion.tipoPro);
                            command.Parameters.AddWithValue("@valA", promotion.valA);
                            command.Parameters.AddWithValue("@valB", promotion.valB);
                            command.Parameters.AddWithValue("@dias", promotion.dias);
                            command.Parameters.AddWithValue("@fechaAlta", promotion.fechaAlta);
                            command.Parameters.AddWithValue("@status", promotion.status);
                            int recordSaved = command.ExecuteNonQuery();
                            if (recordSaved != 0)
                                count++;
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
            return count;
        }

        public static int saveAllPromotionsLAN(List<ClsPromocionesModel> promosList)
        {
            int count = 0;
            if (promosList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (var promotion in promosList)
                    {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_PROMOCIONES + " VALUES(@id, @codigo, @nombre, @fechaInicio, " +
                            "@fechaFin, @volumenMinimo, @volumenMaximo, @porcentajeDescuento, @clasiClienteIdUno, @clasiClienteIdDos, " +
                            "@clasiClienteIdTres, @clasiClienteIdCuatro, @clasiClienteIdCinco, @clasiClienteIdSeis, @clasiProductoIdUno, " +
                            "@clasiProductoIdDos, @clasiProductoIdTres, @clasiProductoIdCuatro, @clasiProductoIdCinco, @clasiProductoIdSeis, " +
                            "@creado, @tipoPromocion, @conceptoDocumentoId, @subtipoPromocion, @horaInicio, @horaFin, @tipoPro, " +
                            "@valA, @valB, @dias, @fechaAlta, @status)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@id", promotion.id);
                            command.Parameters.AddWithValue("@codigo", promotion.code);
                            command.Parameters.AddWithValue("@nombre", promotion.nombre);
                            command.Parameters.AddWithValue("@fechaInicio", promotion.fechaInicio);
                            command.Parameters.AddWithValue("@fechaFin", promotion.fechaFin);
                            command.Parameters.AddWithValue("@volumenMinimo", promotion.volumenMinimo);
                            command.Parameters.AddWithValue("@volumenMaximo", promotion.volumenMaximo);
                            command.Parameters.AddWithValue("@porcentajeDescuento", promotion.porcentajeDescuento);
                            command.Parameters.AddWithValue("@clasiClienteIdUno", promotion.clasiClienteIdUno);
                            command.Parameters.AddWithValue("@clasiClienteIdDos", promotion.clasiClienteIdDos);
                            command.Parameters.AddWithValue("@clasiClienteIdTres", promotion.clasiClienteIdTres);
                            command.Parameters.AddWithValue("@clasiClienteIdCuatro", promotion.clasiClienteIdCuatro);
                            command.Parameters.AddWithValue("@clasiClienteIdCinco", promotion.clasiClienteIdCinco);
                            command.Parameters.AddWithValue("@clasiClienteIdSeis", promotion.clasiClienteIdSeis);
                            command.Parameters.AddWithValue("@clasiProductoIdUno", promotion.clasiProductoIdUno);
                            command.Parameters.AddWithValue("@clasiProductoIdDos", promotion.clasiProductoIdDos);
                            command.Parameters.AddWithValue("@clasiProductoIdTres", promotion.clasiProductoIdTres);
                            command.Parameters.AddWithValue("@clasiProductoIdCuatro", promotion.clasiProductoIdCuatro);
                            command.Parameters.AddWithValue("@clasiProductoIdCinco", promotion.clasiProductoIdCinco);
                            command.Parameters.AddWithValue("@clasiProductoIdSeis", promotion.clasiProductoIdSeis);
                            command.Parameters.AddWithValue("@creado", promotion.creado);
                            command.Parameters.AddWithValue("@tipoPromocion", promotion.tipoPromocion);
                            command.Parameters.AddWithValue("@conceptoDocumentoId", promotion.conceptoDocumentoId);
                            command.Parameters.AddWithValue("@subtipoPromocion", promotion.subtipoPromocion);
                            command.Parameters.AddWithValue("@horaInicio", promotion.horaInicio);
                            command.Parameters.AddWithValue("@horaFin", promotion.horaFin);
                            command.Parameters.AddWithValue("@tipoPro", promotion.tipoPro);
                            command.Parameters.AddWithValue("@valA", promotion.valA);
                            command.Parameters.AddWithValue("@valB", promotion.valB);
                            command.Parameters.AddWithValue("@dias", promotion.dias);
                            command.Parameters.AddWithValue("@fechaAlta", promotion.fechaAlta);
                            command.Parameters.AddWithValue("@status", promotion.status);
                            int recordSaved = command.ExecuteNonQuery();
                            if (recordSaved != 0)
                                count++;
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
            return count;
        }

        public static ExpandoObject logicForAplyPromotionsCustomersItems(ClsItemModel itemModel, double cantidadArticulo, double monto, bool serverModeLAN, int idDocumento)
        {
            int idCustomer = idDocumento;
            dynamic a = new ExpandoObject();
            a.valor = 0;
            a.categoria1 = 0;
            a.categoria2 = 0;
            a.categoria3 = 0;
            a.categoria4 = 0;
            a.categoria5 = 0;
            a.categoria6 = 0;
            try
            {
                a = CustomerModel.getAllDataFromACustomerCategorias(idCustomer);
            }catch(Exception e)
            {
                SECUDOC.writeLog(e.ToString());
                a.valor = 0;
            }

            dynamic rateAndAmountDiscountList = new ExpandoObject();
            String promotionName = "";
            double promotionDiscount = 0;
            double rateDiscountPromo = 0;
            /** Validamos si tiene promoción para aplicarlo */
            ClsPromocionesModel pm = null;
            /*if (serverModeLAN)
            {
                if (itemModel.clasificacionId1 != 0 || itemModel.clasificacionId2 != 0 || itemModel.clasificacionId3 != 0 ||
                    itemModel.clasificacionId4 != 0 || itemModel.clasificacionId5 != 0 || itemModel.clasificacionId6 != 0)
                {
                    String comInstance = InstanceSQLSEModel.getStringComInstance();
                    pm = ClsPromocionesModel.getPromotionForAnItem(comInstance, itemModel);
                }
            }
            else
            {
                if (itemModel.clasificacionId1 != 0 || itemModel.clasificacionId2 != 0 || itemModel.clasificacionId3 != 0 ||
                    itemModel.clasificacionId4 != 0 || itemModel.clasificacionId5 != 0 || itemModel.clasificacionId6 != 0)
                {
                    pm = getPromotionForAnItem(itemModel); 
                }
            }*/
            pm = getPromotionForAnItemCustomer(a, itemModel);
            if (pm != null)
            {
                int vigente = validateValidityEndDate(pm.fechaFin.ToString());
                if (vigente > 0)
                {
                    if (pm.volumenMinimo != 0 && cantidadArticulo >= pm.volumenMinimo &&
                            pm.volumenMaximo != 0 && cantidadArticulo <= pm.volumenMaximo)
                    {
                        promotionName = pm.code;
                        rateDiscountPromo = pm.porcentajeDescuento;
                        promotionDiscount = (monto * rateDiscountPromo) / 100;
                        rateAndAmountDiscountList.aplica = "1";
                        rateAndAmountDiscountList.code = promotionName;
                        rateAndAmountDiscountList.porcentaje = rateDiscountPromo + "";
                        rateAndAmountDiscountList.importe = promotionDiscount + "";
                    }
                    else if (pm.volumenMinimo == 0 && cantidadArticulo >= pm.volumenMinimo &&
                          pm.volumenMaximo != 0 && cantidadArticulo <= pm.volumenMaximo)
                    {
                        promotionName = pm.code;
                        rateDiscountPromo = pm.porcentajeDescuento;
                        promotionDiscount = (monto * rateDiscountPromo) / 100;
                        rateAndAmountDiscountList.aplica = "1";
                        rateAndAmountDiscountList.code = promotionName;
                        rateAndAmountDiscountList.porcentaje = rateDiscountPromo + "";
                        rateAndAmountDiscountList.importe = promotionDiscount + "";
                    }
                    else if (pm.volumenMinimo != 0 && cantidadArticulo <= pm.volumenMinimo && pm.volumenMaximo == 0)
                    {
                        promotionName = pm.code;
                        rateDiscountPromo = pm.porcentajeDescuento;
                        promotionDiscount = (monto * rateDiscountPromo) / 100;
                        rateAndAmountDiscountList.aplica = "1";
                        rateAndAmountDiscountList.code = promotionName;
                        rateAndAmountDiscountList.porcentaje = rateDiscountPromo + "";
                        rateAndAmountDiscountList.importe = promotionDiscount + "";
                    }
                    else if (pm.volumenMinimo == 0 && pm.volumenMaximo == 0)
                    {
                        promotionName = pm.code;
                        rateDiscountPromo = pm.porcentajeDescuento;
                        promotionDiscount = (monto * rateDiscountPromo) / 100;
                        rateAndAmountDiscountList.aplica = "1";
                        rateAndAmountDiscountList.code = promotionName;
                        rateAndAmountDiscountList.porcentaje = rateDiscountPromo + "";
                        rateAndAmountDiscountList.importe = promotionDiscount + "";
                    }
                    else
                    {
                        promotionName = pm.code;
                        rateDiscountPromo = 0;
                        rateAndAmountDiscountList.aplica = "-2";
                        rateAndAmountDiscountList.code = promotionName;
                        rateAndAmountDiscountList.porcentaje = rateDiscountPromo + "";
                        rateAndAmountDiscountList.importe = promotionDiscount + "";
                    }
                }
                else
                {
                    rateDiscountPromo = 0;
                    rateAndAmountDiscountList.aplica = "-1";
                    rateAndAmountDiscountList.code = "Sin promoción";
                    rateAndAmountDiscountList.porcentaje = rateDiscountPromo + "";
                    rateAndAmountDiscountList.importe = promotionDiscount + "";
                }
            }
            else
            {
                rateDiscountPromo = 0;
                rateAndAmountDiscountList.aplica = "-1";
                rateAndAmountDiscountList.code = "Sin promoción";
                rateAndAmountDiscountList.porcentaje = rateDiscountPromo + "";
                rateAndAmountDiscountList.importe = promotionDiscount + "";
            }
            return rateAndAmountDiscountList;
        }

        public static ExpandoObject logicForAplyPromotions(ClsItemModel itemModel, double cantidadArticulo, double monto, bool serverModeLAN)
        {
            int idCustomer = FormVenta.idCustomer;
            dynamic a = new ExpandoObject();
            a.valor = 0;
            a.categoria1 = 0;
            a.categoria2 = 0;
            a.categoria3 = 0;
            a.categoria4 = 0;
            a.categoria5 = 0;
            a.categoria6 = 0;
            try
            {
                a = CustomerModel.getAllDataFromACustomerCategorias(idCustomer);
            }
            catch (Exception e)
            {
                SECUDOC.writeLog(e.ToString());
                a.valor = 0;
            }
            dynamic rateAndAmountDiscountList = new ExpandoObject();
            String promotionName = "";
            double promotionDiscount = 0;
            double rateDiscountPromo = 0;
            /** Validamos si tiene promoción para aplicarlo */
            ClsPromocionesModel pm = null;
            if (serverModeLAN)
            {
                if (itemModel.clasificacionId1 != 0 || itemModel.clasificacionId2 != 0 || itemModel.clasificacionId3 != 0 ||
                    itemModel.clasificacionId4 != 0 || itemModel.clasificacionId5 != 0 || itemModel.clasificacionId6 != 0){
                    String comInstance = InstanceSQLSEModel.getStringComInstance();
                    pm = ClsPromocionesModel.getPromotionForAnItem(comInstance, itemModel);
                }
            } else
            {
                if (itemModel.clasificacionId1 != 0 || itemModel.clasificacionId2 != 0 || itemModel.clasificacionId3 != 0 ||
                    itemModel.clasificacionId4 != 0 || itemModel.clasificacionId5 != 0 || itemModel.clasificacionId6 != 0)
                {
                    pm = getPromotionForAnItemCustomer(a, itemModel); 
                }
            }
            if (pm != null)
            {
                int vigente = validateValidityEndDate(pm.fechaFin.ToString());
                if (vigente > 0)
                {
                    if (pm.volumenMinimo != 0 && cantidadArticulo >= pm.volumenMinimo &&
                            pm.volumenMaximo != 0 && cantidadArticulo <= pm.volumenMaximo)
                    {
                        promotionName = pm.code;
                        rateDiscountPromo = pm.porcentajeDescuento;
                        promotionDiscount = (monto * rateDiscountPromo) / 100;
                        rateAndAmountDiscountList.aplica = "1";
                        rateAndAmountDiscountList.code = promotionName;
                        rateAndAmountDiscountList.porcentaje = rateDiscountPromo + "";
                        rateAndAmountDiscountList.importe = promotionDiscount + "";
                    }
                    else if (pm.volumenMinimo == 0 && cantidadArticulo >= pm.volumenMinimo &&
                          pm.volumenMaximo != 0 && cantidadArticulo <= pm.volumenMaximo)
                    {
                        promotionName = pm.code;
                        rateDiscountPromo = pm.porcentajeDescuento;
                        promotionDiscount = (monto * rateDiscountPromo) / 100;
                        rateAndAmountDiscountList.aplica = "1";
                        rateAndAmountDiscountList.code = promotionName;
                        rateAndAmountDiscountList.porcentaje = rateDiscountPromo + "";
                        rateAndAmountDiscountList.importe = promotionDiscount + "";
                    }
                    else if (pm.volumenMinimo != 0 && cantidadArticulo <= pm.volumenMinimo && pm.volumenMaximo == 0)
                    {
                        promotionName = pm.code;
                        rateDiscountPromo = pm.porcentajeDescuento;
                        promotionDiscount = (monto * rateDiscountPromo) / 100;
                        rateAndAmountDiscountList.aplica = "1";
                        rateAndAmountDiscountList.code = promotionName;
                        rateAndAmountDiscountList.porcentaje = rateDiscountPromo + "";
                        rateAndAmountDiscountList.importe = promotionDiscount + "";
                    }
                    else if (pm.volumenMinimo == 0 && pm.volumenMaximo == 0)
                    {
                        promotionName = pm.code;
                        rateDiscountPromo = pm.porcentajeDescuento;
                        promotionDiscount = (monto * rateDiscountPromo) / 100;
                        rateAndAmountDiscountList.aplica = "1";
                        rateAndAmountDiscountList.code = promotionName;
                        rateAndAmountDiscountList.porcentaje = rateDiscountPromo + "";
                        rateAndAmountDiscountList.importe = promotionDiscount + "";
                    }
                    else
                    {
                        promotionName = pm.code;
                        rateDiscountPromo = 0;
                        rateAndAmountDiscountList.aplica = "-2";
                        rateAndAmountDiscountList.code = promotionName;
                        rateAndAmountDiscountList.porcentaje = rateDiscountPromo + "";
                        rateAndAmountDiscountList.importe = promotionDiscount + "";
                    }
                }
                else
                {
                    rateDiscountPromo = 0;
                    rateAndAmountDiscountList.aplica = "-1";
                    rateAndAmountDiscountList.code = "Sin promoción";
                    rateAndAmountDiscountList.porcentaje = rateDiscountPromo + "";
                    rateAndAmountDiscountList.importe = promotionDiscount + "";
                }
            } else
            {
                rateDiscountPromo = 0;
                rateAndAmountDiscountList.aplica = "-1";
                rateAndAmountDiscountList.code = "Sin promoción";
                rateAndAmountDiscountList.porcentaje = rateDiscountPromo + "";
                rateAndAmountDiscountList.importe = promotionDiscount + "";
            }
            return rateAndAmountDiscountList;
        }

        public static List<ClsPromocionesModel> getAllPromotions(String query)
        {
            List<ClsPromocionesModel> promosList = null;
            ClsPromocionesModel pm = null;
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
                            promosList = new List<ClsPromocionesModel>();
                            while (reader.Read())
                            {
                                pm = new ClsPromocionesModel();
                                pm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_PROMOTION].ToString().Trim());
                                pm.code = reader[LocalDatabase.CAMPO_CODIGO_PROMOTION].ToString().Trim();
                                pm.nombre = reader[LocalDatabase.CAMPO_NOMBRE_PROMOTION].ToString().Trim();
                                pm.fechaInicio = Convert.ToDateTime(reader[LocalDatabase.CAMPO_FECHAINICIO_PROMOTION].ToString().Trim());
                                pm.fechaFin = Convert.ToDateTime(reader[LocalDatabase.CAMPO_FECHAFIN_PROMOTION].ToString().Trim());
                                pm.volumenMinimo = Convert.ToDouble(reader[LocalDatabase.CAMPO_VOLUMENMINIMO_PROMOTION].ToString().Trim());
                                pm.volumenMaximo = Convert.ToDouble(reader[LocalDatabase.CAMPO_VOLUMENMAXIMO_PROMOTION].ToString().Trim());
                                pm.porcentajeDescuento = Convert.ToDouble(reader[LocalDatabase.CAMPO_PORCENTAJEDESCUENTO_PROMOTION].ToString().Trim());
                                pm.clasiProductoIdUno = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIPRODUCTOID1_PROMOTION].ToString().Trim());
                                pm.clasiProductoIdDos = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIPRODUCTOID2_PROMOTION].ToString().Trim());
                                pm.clasiProductoIdTres = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIPRODUCTOID3_PROMOTION].ToString().Trim());
                                pm.clasiProductoIdCuatro = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIPRODUCTOID4_PROMOTION].ToString().Trim());
                                pm.clasiProductoIdCinco = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIPRODUCTOID5_PROMOTION].ToString().Trim());
                                pm.clasiProductoIdSeis = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIPRODUCTOID6_PROMOTION].ToString().Trim());
                                pm.creado = reader[LocalDatabase.CAMPO_CREADO_PROMOTION].ToString().Trim();
                                promosList.Add(pm);
                            }
                        }
                        if (reader != null && reader.IsClosed)
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
            return promosList;
        }

        public static int getTheNumberOfPromotions(String query)
        {
            int number = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
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
                                number = reader.GetInt32(0);
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
            return number;
        }

        public static ClsPromocionesModel getAllDataForAPromotion(int id)
        {
            ClsPromocionesModel pm = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_PROMOCIONES + " WHERE " + LocalDatabase.CAMPO_ID_PROMOTION + " = " + id;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                pm = new ClsPromocionesModel();
                                pm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_PROMOTION].ToString().Trim());
                                pm.code = reader[LocalDatabase.CAMPO_CODIGO_PROMOTION].ToString().Trim();
                                pm.nombre = reader[LocalDatabase.CAMPO_NOMBRE_PROMOTION].ToString().Trim();
                                pm.fechaInicio = Convert.ToDateTime(reader[LocalDatabase.CAMPO_FECHAINICIO_PROMOTION].ToString().Trim());
                                pm.fechaFin = Convert.ToDateTime(reader[LocalDatabase.CAMPO_FECHAFIN_PROMOTION].ToString().Trim());
                                pm.volumenMinimo = Convert.ToDouble(reader[LocalDatabase.CAMPO_VOLUMENMINIMO_PROMOTION].ToString().Trim());
                                pm.volumenMaximo = Convert.ToDouble(reader[LocalDatabase.CAMPO_VOLUMENMAXIMO_PROMOTION].ToString().Trim());
                                pm.porcentajeDescuento = Convert.ToDouble(reader[LocalDatabase.CAMPO_PORCENTAJEDESCUENTO_PROMOTION].ToString().Trim());
                                pm.clasiProductoIdUno = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIPRODUCTOID1_PROMOTION].ToString().Trim());
                                pm.clasiProductoIdDos = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIPRODUCTOID2_PROMOTION].ToString().Trim());
                                pm.clasiProductoIdTres = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIPRODUCTOID3_PROMOTION].ToString().Trim());
                                pm.clasiProductoIdCuatro = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIPRODUCTOID4_PROMOTION].ToString().Trim());
                                pm.clasiProductoIdCinco = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIPRODUCTOID5_PROMOTION].ToString().Trim());
                                pm.clasiProductoIdSeis = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIPRODUCTOID6_PROMOTION].ToString().Trim());
                                pm.creado = reader[LocalDatabase.CAMPO_CREADO_PROMOTION].ToString().Trim();
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

        public static ClsPromocionesModel getPromotionForAnItemCustomer(dynamic a, ClsItemModel im)
        {
            //falta los 2 de estos de abajo
            ClsPromocionesModel pm = null;
            if (a.valor == 1)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    String query = "SELECT * FROM " + LocalDatabase.TABLA_PROMOCIONES + " WHERE " +
                        LocalDatabase.CAMPO_CLASIPRODUCTOID1_PROMOTION + " = " + im.clasificacionId1 + " AND " +
                        LocalDatabase.CAMPO_CLASIPRODUCTOID2_PROMOTION + " = " + im.clasificacionId2 + " AND " +
                        LocalDatabase.CAMPO_CLASIPRODUCTOID3_PROMOTION + " = " + im.clasificacionId3 + " AND " +
                        LocalDatabase.CAMPO_CLASIPRODUCTOID4_PROMOTION + " = " + im.clasificacionId4 + " AND " +
                        LocalDatabase.CAMPO_CLASIPRODUCTOID5_PROMOTION + " = " + im.clasificacionId5 + " AND " +
                        LocalDatabase.CAMPO_CLASIPRODUCTOID6_PROMOTION + " = " + im.clasificacionId6 + " AND " +
                        LocalDatabase.CAMPO_CLASICLIENTEID1_PROMOTION + " = " + a.categoria1 + " AND " +
                        LocalDatabase.CAMPO_CLASICLIENTEID2_PROMOTION + " = " + a.categoria2 + " AND " +
                        LocalDatabase.CAMPO_CLASICLIENTEID3_PROMOTION + " = " + a.categoria3 + " AND " +
                        LocalDatabase.CAMPO_CLASICLIENTEID4_PROMOTION + " = " + a.categoria4 + " AND " +
                        LocalDatabase.CAMPO_CLASICLIENTEID5_PROMOTION + " = " + a.categoria5 + " AND " +
                        LocalDatabase.CAMPO_CLASICLIENTEID6_PROMOTION + " = " + a.categoria6;
                    using (SQLiteCommand command = new SQLiteCommand(query, db))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    pm = new ClsPromocionesModel();
                                    pm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_PROMOTION].ToString().Trim());
                                    pm.code = reader[LocalDatabase.CAMPO_CODIGO_PROMOTION].ToString().Trim();
                                    pm.nombre = reader[LocalDatabase.CAMPO_NOMBRE_PROMOTION].ToString().Trim();
                                    pm.fechaInicio = Convert.ToDateTime(reader[LocalDatabase.CAMPO_FECHAINICIO_PROMOTION].ToString().Trim());
                                    pm.fechaFin = Convert.ToDateTime(reader[LocalDatabase.CAMPO_FECHAFIN_PROMOTION].ToString().Trim());
                                    pm.volumenMinimo = Convert.ToDouble(reader[LocalDatabase.CAMPO_VOLUMENMINIMO_PROMOTION].ToString().Trim());
                                    pm.volumenMaximo = Convert.ToDouble(reader[LocalDatabase.CAMPO_VOLUMENMAXIMO_PROMOTION].ToString().Trim());
                                    pm.porcentajeDescuento = Convert.ToDouble(reader[LocalDatabase.CAMPO_PORCENTAJEDESCUENTO_PROMOTION].ToString().Trim());
                                    pm.clasiProductoIdUno = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIPRODUCTOID1_PROMOTION].ToString().Trim());
                                    pm.clasiProductoIdDos = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIPRODUCTOID2_PROMOTION].ToString().Trim());
                                    pm.clasiProductoIdTres = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIPRODUCTOID3_PROMOTION].ToString().Trim());
                                    pm.clasiProductoIdCuatro = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIPRODUCTOID4_PROMOTION].ToString().Trim());
                                    pm.clasiProductoIdCinco = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIPRODUCTOID5_PROMOTION].ToString().Trim());
                                    pm.clasiProductoIdSeis = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIPRODUCTOID6_PROMOTION].ToString().Trim());
                                    pm.creado = reader[LocalDatabase.CAMPO_CREADO_PROMOTION].ToString().Trim();
                                    pm.clasiClienteIdUno = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASICLIENTEID1_PROMOTION].ToString().Trim());
                                    pm.clasiClienteIdDos = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASICLIENTEID2_PROMOTION].ToString().Trim());
                                    pm.clasiClienteIdTres = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASICLIENTEID3_PROMOTION].ToString().Trim());
                                    pm.clasiClienteIdCuatro = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASICLIENTEID4_PROMOTION].ToString().Trim());
                                    pm.clasiClienteIdCinco = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASICLIENTEID5_PROMOTION].ToString().Trim());
                                    pm.clasiClienteIdSeis = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASICLIENTEID6_PROMOTION].ToString().Trim());
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
            return pm;
        }

        public static ClsPromocionesModel getPromotionForAnItem(ClsItemModel im)
        {
            ClsPromocionesModel pm = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_PROMOCIONES + " WHERE " +
                    LocalDatabase.CAMPO_CLASIPRODUCTOID1_PROMOTION + " = " + im.clasificacionId1+" AND "+
                    LocalDatabase.CAMPO_CLASIPRODUCTOID2_PROMOTION + " = " + im.clasificacionId2 + " AND " +
                    LocalDatabase.CAMPO_CLASIPRODUCTOID3_PROMOTION + " = " + im.clasificacionId3 + " AND " +
                    LocalDatabase.CAMPO_CLASIPRODUCTOID4_PROMOTION + " = " + im.clasificacionId4 + " AND " +
                    LocalDatabase.CAMPO_CLASIPRODUCTOID5_PROMOTION + " = " + im.clasificacionId5 + " AND " +
                    LocalDatabase.CAMPO_CLASIPRODUCTOID6_PROMOTION + " = " + im.clasificacionId6;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                pm = new ClsPromocionesModel();
                                pm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_PROMOTION].ToString().Trim());
                                pm.code = reader[LocalDatabase.CAMPO_CODIGO_PROMOTION].ToString().Trim();
                                pm.nombre = reader[LocalDatabase.CAMPO_NOMBRE_PROMOTION].ToString().Trim();
                                pm.fechaInicio = Convert.ToDateTime(reader[LocalDatabase.CAMPO_FECHAINICIO_PROMOTION].ToString().Trim());
                                pm.fechaFin = Convert.ToDateTime(reader[LocalDatabase.CAMPO_FECHAFIN_PROMOTION].ToString().Trim());
                                pm.volumenMinimo = Convert.ToDouble(reader[LocalDatabase.CAMPO_VOLUMENMINIMO_PROMOTION].ToString().Trim());
                                pm.volumenMaximo = Convert.ToDouble(reader[LocalDatabase.CAMPO_VOLUMENMAXIMO_PROMOTION].ToString().Trim());
                                pm.porcentajeDescuento = Convert.ToDouble(reader[LocalDatabase.CAMPO_PORCENTAJEDESCUENTO_PROMOTION].ToString().Trim());
                                pm.clasiProductoIdUno = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIPRODUCTOID1_PROMOTION].ToString().Trim());
                                pm.clasiProductoIdDos = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIPRODUCTOID2_PROMOTION].ToString().Trim());
                                pm.clasiProductoIdTres = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIPRODUCTOID3_PROMOTION].ToString().Trim());
                                pm.clasiProductoIdCuatro = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIPRODUCTOID4_PROMOTION].ToString().Trim());
                                pm.clasiProductoIdCinco = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIPRODUCTOID5_PROMOTION].ToString().Trim());
                                pm.clasiProductoIdSeis = Convert.ToInt32(reader[LocalDatabase.CAMPO_CLASIPRODUCTOID6_PROMOTION].ToString().Trim());
                                pm.creado = reader[LocalDatabase.CAMPO_CREADO_PROMOTION].ToString().Trim();
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

        public static int validateValidityEndDate(String fechaFin)
        {
            int resultado = 0;
            try
            {
                /**Obtenemos las fechas enviadas en el formato a comparar*/
                DateTime fechaDateFin = DateTime.Parse(fechaFin);
                fechaDateFin.ToString("yyyy-MM-dd");
                DateTime fechaDateHoy = DateTime.Now;
                fechaDateHoy.ToString("yyyy-MM-dd");
                if (DateTime.Compare(fechaDateFin, fechaDateHoy) < 0)
                {
                    resultado = -1;
                }
                else
                {
                    if (fechaDateHoy.CompareTo(fechaDateFin) == 0)
                    {
                        resultado = 1;
                    }
                    else if (fechaDateFin.CompareTo(fechaDateHoy) > 0)
                    {
                        resultado = 1;
                    }
                    else
                    {
                        resultado = -1;
                    }
                }
            }
            catch (Exception e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            return resultado;
        }

        public static Boolean deleteAllPromotions()
        {
            bool deleted = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "DELETE FROM " + LocalDatabase.TABLA_PROMOCIONES;
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
