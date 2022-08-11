using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using static SyncTPV.Controllers.ClsCuentasPorCobrarController;

namespace SyncTPV.Models
{
    public class CuentasXCobrarModel
    {
        public static readonly int CREDIT_DOCUMENT_TYPE = 1;
        public static readonly int REP_DOCUMENT_TYPE = 2;
        public int id { get; set; }
        public int customerId { get; set; }
        public int creditDocumentId { get; set; }
        public String creditDocumentFolio { get; set; }
        public String customerCode { get; set; }
        public String nameUser { get; set; }
        public int diasAtraso { get; set; }
        public double saldo_actual { get; set; }
        public String fechaVencimiento { get; set; }
        public String descripcion { get; set; }
        public String factura_mostrador { get; set; }
        public String reference { get; set; }
        public double amount { get; set; }
        public int enviado { get; set; }
        public int formOfPaymentIdCredit { get; set; }
        public int bankId { get; set; }
        public String date { get; set; }
        public int tipo { get; set; }
        public int status { get; set; }
        public String documentFolio { get; set; }
        public double SaldoPendinte { get; set; }
        public int userId { get; set; }

        public static int addNewAbono(int creditDocumentId, double importe, int formaCcId, String reference,
                                  String fecha)
        {
            int idAdded = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                CuentasXCobrarModel cxc = getDataForACxC(creditDocumentId);
                String query = "INSERT INTO " + LocalDatabase.TABLA_CXC + " (" + LocalDatabase.CAMPO_ID_CXC + ", " + LocalDatabase.CAMPO_CLIENTE_ID_CXC + ", " + LocalDatabase.CAMPO_DOCTO_CC_ID_CXC + ", " +
                    LocalDatabase.CAMPO_FOLIO_CXC + ", " + LocalDatabase.CAMPO_DIAS_ATRASO_CXC + ", " + LocalDatabase.CAMPO_SALDO_ACTUAL_CXC + ", " +
                    LocalDatabase.CAMPO_FECHA_VENCE_CXC + ", " + LocalDatabase.CAMPO_DESCRIPCION_CXC + ", " + LocalDatabase.CAMPO_FACTURA_MOSTRADOR_CXC + ", " +
                    LocalDatabase.CAMPO_ABONO_CXC + ", " + LocalDatabase.CAMPO_FORMA_COBRO_CC_ID_CXC + ", " + LocalDatabase.CAMPO_REFER_MOVTO_BANCARIO_CXC + ", " +
                    LocalDatabase.CAMPO_FECHA_CXC + ", " + LocalDatabase.CAMPO_BANCO_ID_CXC + ", " + LocalDatabase.CAMPO_TIPO_CXC + ") VALUES(@" + LocalDatabase.CAMPO_ID_CXC +
                    ", @" + LocalDatabase.CAMPO_CLIENTE_ID_CXC +
                    ", @" + LocalDatabase.CAMPO_DOCTO_CC_ID_CXC + ", @" + LocalDatabase.CAMPO_FOLIO_CXC + ", @" + LocalDatabase.CAMPO_DIAS_ATRASO_CXC +
                    ", @" + LocalDatabase.CAMPO_SALDO_ACTUAL_CXC + ", @" + LocalDatabase.CAMPO_FECHA_VENCE_CXC + ", @" + LocalDatabase.CAMPO_DESCRIPCION_CXC +
                    ", @" + LocalDatabase.CAMPO_FACTURA_MOSTRADOR_CXC + ", @" + LocalDatabase.CAMPO_ABONO_CXC + ", @" + LocalDatabase.CAMPO_FORMA_COBRO_CC_ID_CXC +
                    ", @" + LocalDatabase.CAMPO_REFER_MOVTO_BANCARIO_CXC + ", @" + LocalDatabase.CAMPO_FECHA_CXC + ", @" + LocalDatabase.CAMPO_BANCO_ID_CXC +
                    ", @" + LocalDatabase.CAMPO_TIPO_CXC + ")";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int idCxc = getLastId() + 1;
                    command.Parameters.AddWithValue(LocalDatabase.CAMPO_ID_CXC, idCxc);
                    command.Parameters.AddWithValue(LocalDatabase.CAMPO_CLIENTE_ID_CXC, cxc.customerId);
                    command.Parameters.AddWithValue(LocalDatabase.CAMPO_DOCTO_CC_ID_CXC, cxc.creditDocumentId);
                    command.Parameters.AddWithValue(LocalDatabase.CAMPO_FOLIO_CXC, cxc.creditDocumentFolio);
                    command.Parameters.AddWithValue(LocalDatabase.CAMPO_DIAS_ATRASO_CXC, cxc.diasAtraso);
                    command.Parameters.AddWithValue(LocalDatabase.CAMPO_SALDO_ACTUAL_CXC, cxc.saldo_actual);
                    command.Parameters.AddWithValue(LocalDatabase.CAMPO_FECHA_VENCE_CXC, cxc.fechaVencimiento);
                    command.Parameters.AddWithValue(LocalDatabase.CAMPO_DESCRIPCION_CXC, cxc.descripcion);
                    command.Parameters.AddWithValue(LocalDatabase.CAMPO_FACTURA_MOSTRADOR_CXC, cxc.factura_mostrador);
                    command.Parameters.AddWithValue(LocalDatabase.CAMPO_ABONO_CXC, importe);
                    command.Parameters.AddWithValue(LocalDatabase.CAMPO_FORMA_COBRO_CC_ID_CXC, formaCcId);
                    command.Parameters.AddWithValue(LocalDatabase.CAMPO_REFER_MOVTO_BANCARIO_CXC, reference);
                    command.Parameters.AddWithValue(LocalDatabase.CAMPO_FECHA_CXC, fecha);
                    command.Parameters.AddWithValue(LocalDatabase.CAMPO_BANCO_ID_CXC, 0);
                    command.Parameters.AddWithValue(LocalDatabase.CAMPO_TIPO_CXC, REP_DOCUMENT_TYPE);
                    int lastId = getLastId();
                    int numFolio = (lastId);
                    command.Parameters.AddWithValue(LocalDatabase.CAMPO_FOLIOI_CXC, numFolio + MetodosGenerales.getCurrentDateAndHourForFolioVenta());
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                    {
                        idAdded = idCxc;
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
            return idAdded;
        }

        public static double getAllTotalForAFormaDePagoAbono(int idFormaPago)
        {
            double suma = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT SUM(" + LocalDatabase.CAMPO_ABONO_CXC + ") FROM " + LocalDatabase.TABLA_CXC +
                        " WHERE " + LocalDatabase.CAMPO_FORMA_COBRO_CC_ID_CXC + " = " + idFormaPago + " AND " +
                        LocalDatabase.CAMPO_TIPO_CXC + " = " + 2 + " AND " + LocalDatabase.CAMPO_CANCELADO_CXC + " = " + 0;
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

        public static int getLastId()
        {
            int lastId = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_ID_CXC + " FROM " + LocalDatabase.TABLA_CXC + " ORDER BY " + LocalDatabase.CAMPO_ID_CXC + " DESC LIMIT 1";
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

        public static DataTable getAllAccountsReceivableFromACustomerDt(String query)
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
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return dt;
        }

        public static CuentasXCobrarModel getDataForACxC(int id)
        {
            CuentasXCobrarModel cxcm = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_CXC + " WHERE " + LocalDatabase.CAMPO_DOCTO_CC_ID_CXC + " = " + id + " AND " +
                        LocalDatabase.CAMPO_TIPO_CXC + " = " + 1;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cxcm = new CuentasXCobrarModel();
                                cxcm.id = (reader.GetInt32(0));
                                cxcm.customerId = (reader.GetInt32(1));
                                cxcm.creditDocumentId = Convert.ToInt32(reader.GetValue(2).ToString().Trim());
                                cxcm.creditDocumentFolio = (reader.GetString(3));
                                cxcm.diasAtraso = (reader.GetInt32(4));
                                cxcm.saldo_actual = Convert.ToDouble(reader[LocalDatabase.CAMPO_SALDO_ACTUAL_CXC].ToString().Trim());
                                cxcm.fechaVencimiento = (reader.GetString(6));
                                cxcm.descripcion = reader.GetValue(7).ToString().Trim();
                                cxcm.factura_mostrador = (reader.GetString(8));
                                double abonoTotal = obtainFullCreditOfADocument(db, cxcm.creditDocumentId);
                                cxcm.amount = (abonoTotal);
                                cxcm.enviado = (reader.GetInt32(11));
                                cxcm.formOfPaymentIdCredit = (reader.GetInt32(12));
                                cxcm.bankId = (reader.GetInt32(13));
                                cxcm.date = reader[LocalDatabase.CAMPO_FECHA_CXC].ToString().Trim();
                                cxcm.tipo = (reader.GetInt32(15));
                                cxcm.status = (reader.GetInt32(16));
                                cxcm.documentFolio = reader[LocalDatabase.CAMPO_FOLIOI_CXC].ToString().Trim();
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
            return cxcm;
        }

        public static CuentasXCobrarModel getPagoDelCliente(int idDocumento)
        {
            CuentasXCobrarModel cxcm = null;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_CXC + " WHERE " + LocalDatabase.CAMPO_DOCTO_CC_ID_CXC + " = " + idDocumento + " AND " +
                        LocalDatabase.CAMPO_TIPO_CXC + " = " + 2;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cxcm = new CuentasXCobrarModel();
                                cxcm.id = (reader.GetInt32(0));
                                cxcm.customerId = (reader.GetInt32(1));
                                cxcm.creditDocumentId = Convert.ToInt32(reader.GetValue(2).ToString().Trim());
                                cxcm.creditDocumentFolio = (reader.GetString(3));
                                cxcm.diasAtraso = (reader.GetInt32(4));
                                cxcm.saldo_actual = Convert.ToDouble(reader[LocalDatabase.CAMPO_SALDO_ACTUAL_CXC].ToString().Trim());
                                cxcm.fechaVencimiento = (reader.GetString(6));
                                cxcm.descripcion = (reader.GetString(7));
                                cxcm.factura_mostrador = (reader.GetString(8));
                                double abonoTotal = obtainFullCreditOfADocument(db, cxcm.creditDocumentId);
                                cxcm.amount = (abonoTotal);
                                cxcm.enviado = (reader.GetInt32(11));
                                cxcm.formOfPaymentIdCredit = (reader.GetInt32(12));
                                cxcm.bankId = (reader.GetInt32(13));
                                cxcm.date = reader[LocalDatabase.CAMPO_FECHA_CXC].ToString().Trim();
                                cxcm.tipo = (reader.GetInt32(15));
                                cxcm.status = (reader.GetInt32(16));
                                cxcm.documentFolio = reader[LocalDatabase.CAMPO_FOLIOI_CXC].ToString().Trim();
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
            return cxcm;
        }

        public static List<CuentasXCobrarModel> getOneNotSendCxc(int idDocument, int enviado)
        {
            List<CuentasXCobrarModel> listaRes = null;
            CuentasXCobrarModel cxc;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_CXC + " WHERE " + LocalDatabase.CAMPO_ID_CXC + " = " + idDocument +
                        " AND " + LocalDatabase.CAMPO_TIPO_CXC + " = " + REP_DOCUMENT_TYPE + " AND " + LocalDatabase.CAMPO_ENVIADO_CXC + " = " + enviado +
                        " AND " + LocalDatabase.CAMPO_CANCELADO_CXC + " = " + 0 + " ORDER BY " + LocalDatabase.CAMPO_ID_CXC + " ASC LIMIT " + 1;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            listaRes = new List<CuentasXCobrarModel>();
                            while (reader.Read())
                            {
                                int customerId = Convert.ToInt32(reader.GetValue(1).ToString().Trim());
                                String customerCode = CustomerModel.getClaveForAClient(customerId);
                                cxc = new CuentasXCobrarModel();
                                cxc.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_CXC].ToString().Trim());
                                cxc.customerId = customerId;
                                cxc.customerCode = customerCode;
                                cxc.creditDocumentId = Convert.ToInt32(reader.GetValue(2).ToString().Trim());
                                cxc.creditDocumentFolio = reader.GetString(3);
                                cxc.diasAtraso = reader.GetInt32(4);
                                cxc.saldo_actual = Convert.ToDouble(reader[LocalDatabase.CAMPO_SALDO_ACTUAL_CXC].ToString().Trim());
                                cxc.fechaVencimiento = reader.GetString(6);
                                cxc.factura_mostrador = reader.GetString(8);
                                cxc.reference = reader.GetValue(9).ToString().Trim();
                                cxc.amount = reader.GetDouble(10);
                                cxc.enviado = reader.GetInt32(11);
                                cxc.formOfPaymentIdCredit = reader.GetInt32(12);
                                cxc.bankId = reader.GetInt32(13);
                                cxc.date = reader.GetValue(14).ToString().Trim();
                                cxc.tipo = reader.GetInt32(15);
                                cxc.status = Convert.ToInt32(reader[LocalDatabase.CAMPO_CANCELADO_CXC].ToString().Trim());
                                cxc.documentFolio = reader[LocalDatabase.CAMPO_FOLIOI_CXC].ToString().Trim();
                                cxc.userId = ClsRegeditController.getIdUserInTurn();
                                cxc.nameUser = UserModel.getNameUser(cxc.userId);
                                listaRes.Add(cxc);
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
            return listaRes;
        }

        public static double getCurrentCustomerBalanceWithDb(SQLiteConnection db, int customerId)
        {
            double pendiente = 0;
            double saldo = 0;
            double abonoTotal = 0;
            if (db != null)
            {
                List<CuentasXCobrarModel> listCxcM = CuentasXCobrarModel.getBalanceAndTotalPaidForACustomer(customerId);
                if (listCxcM != null)
                {
                    for (int i = 0; i < listCxcM.Count; i++)
                    {
                        saldo += listCxcM[i].saldo_actual;
                        double totalPayments = obtainFullCreditOfADocument(db, listCxcM[i].creditDocumentId);
                        abonoTotal += totalPayments;
                    }
                }
            }
            else
            {
                var db1 = new SQLiteConnection();
                try
                {
                    db1.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db1.Open();
                    List<CuentasXCobrarModel> listCxcM = CuentasXCobrarModel.getBalanceAndTotalPaidForACustomer(customerId);
                    if (listCxcM != null)
                    {
                        for (int i = 0; i < listCxcM.Count; i++)
                        {
                            saldo += listCxcM[i].saldo_actual;
                            double totalPayments = obtainFullCreditOfADocument(db1, listCxcM[i].creditDocumentId);
                            abonoTotal += totalPayments;
                        }
                    }
                }
                catch (SQLiteException e)
                {
                    SECUDOC.writeLog(e.ToString());
                }
                finally
                {
                    if (db1 != null && db1.State == ConnectionState.Open)
                        db1.Close();
                }
            }
            pendiente = (saldo - abonoTotal);
            return pendiente;
        }

        public static double obtainFullCreditOfADocument(SQLiteConnection db, int id)
        {
            double totalPayments = 0;
            SQLiteConnection db1 = null;
            try
            {
                String query = "SELECT IFNULL(SUM(" + LocalDatabase.CAMPO_ABONO_CXC + "), 0) FROM " +
                            LocalDatabase.TABLA_CXC + " WHERE " + LocalDatabase.CAMPO_DOCTO_CC_ID_CXC + " = " + id + " AND " +
                            LocalDatabase.CAMPO_TIPO_CXC + " = " + 2 + " AND " + LocalDatabase.CAMPO_CANCELADO_CXC + " = " + 0;
                if (db == null)
                {
                    db1 = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
                    db1.Open();
                    using (SQLiteCommand command = new SQLiteCommand(query, db1))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    totalPayments = Convert.ToDouble(reader.GetValue(0).ToString().Trim());
                                }
                            }
                            if (reader != null && !reader.IsClosed)
                                reader.Close();
                        }
                    }
                }
                else
                {
                    using (SQLiteCommand command = new SQLiteCommand(query, db))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    totalPayments = Convert.ToDouble(reader.GetValue(0).ToString().Trim());
                                }
                            }
                            if (reader != null && !reader.IsClosed)
                                reader.Close();
                        }
                    }
                }

            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                if (db1 != null && db1.State == ConnectionState.Open)
                    db1.Close();
            }
            return totalPayments;
        }

        public static double getTotalAbonadoDeUnDocumento(int idDocumento)
        {
            double totalPayments = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT IFNULL(SUM(" + LocalDatabase.CAMPO_ABONO_CXC + "), 0) FROM " +
                            LocalDatabase.TABLA_CXC + " WHERE " + LocalDatabase.CAMPO_DOCTO_CC_ID_CXC + " = " + idDocumento + " AND " +
                            LocalDatabase.CAMPO_TIPO_CXC + " = " + 2 + " AND " + LocalDatabase.CAMPO_CANCELADO_CXC + " = " + 0;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    totalPayments = Convert.ToDouble(reader.GetValue(0).ToString().Trim());
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
            return totalPayments;
        }

        public static double getCurrentCustomerBalance(int customerId)
        {
            double pendiente = 0;
            double saldo = 0;
            double abonoTotal = 0;
            List<CuentasXCobrarModel> listCxcM = getBalanceAndTotalPaidForACustomer(customerId);
            if (listCxcM != null)
            {
                for (int i = 0; i < listCxcM.Count; i++)
                {
                    saldo += listCxcM[i].saldo_actual;
                    double totalPayments = obtainFullCreditOfADocument(null, listCxcM[i].creditDocumentId);
                    abonoTotal += totalPayments;
                }
            }
            pendiente = (saldo - abonoTotal);
            return pendiente;
        }

        public static List<CuentasXCobrarModel> getBalanceAndTotalPaidForACustomer(int customerId)
        {
            List<CuentasXCobrarModel> cobrarModels = null;
            CuentasXCobrarModel cxcm;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_DOCTO_CC_ID_CXC + ", " + LocalDatabase.CAMPO_SALDO_ACTUAL_CXC + " FROM " + LocalDatabase.TABLA_CXC + 
                    " WHERE " +LocalDatabase.CAMPO_CLIENTE_ID_CXC + " = " + customerId + " AND " +
                            LocalDatabase.CAMPO_TIPO_CXC + " = " + 1;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            cobrarModels = new List<CuentasXCobrarModel>();
                            while (reader.Read())
                            {
                                cxcm = new CuentasXCobrarModel();
                                int idDoctoCxc = Convert.ToInt32(reader[LocalDatabase.CAMPO_DOCTO_CC_ID_CXC].ToString().Trim());
                                cxcm.creditDocumentId = idDoctoCxc;
                                cxcm.saldo_actual = Convert.ToDouble(reader[LocalDatabase.CAMPO_SALDO_ACTUAL_CXC].ToString().Trim());
                                double abonoTotal = obtainFullCreditOfADocument(db, idDoctoCxc);
                                cxcm.amount = abonoTotal;
                                cobrarModels.Add(cxcm);
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
            return cobrarModels;
        }

        public static int getTheTotalNumberOfCreditsNotSent()
        {
            int total = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_CXC + " WHERE " + LocalDatabase.CAMPO_TIPO_CXC + " = " + 2 +
                        " AND " + LocalDatabase.CAMPO_ENVIADO_CXC + " = " + 0 + " AND " + LocalDatabase.CAMPO_CANCELADO_CXC + " = " + 0;
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

        public static List<CuentasXCobrarModel> getAllNotSendCxc(int enviado)
        {
            List<CuentasXCobrarModel> listaRes = null;
            CuentasXCobrarModel cxc;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_CXC + " WHERE " +
                        LocalDatabase.CAMPO_TIPO_CXC + " = " + REP_DOCUMENT_TYPE +
                        " AND " + LocalDatabase.CAMPO_ENVIADO_CXC + " = " + enviado +
                        " AND " + LocalDatabase.CAMPO_CANCELADO_CXC + " = " + 0 + 
                        " ORDER BY " + LocalDatabase.CAMPO_ID_CXC +
                        " ASC LIMIT " + 1;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            listaRes = new List<CuentasXCobrarModel>();
                            while (reader.Read())
                            {
                                int customerId = Convert.ToInt32(reader.GetValue(1).ToString().Trim());
                                String customerCode = CustomerModel.getClaveForAClient(customerId);
                                cxc = new CuentasXCobrarModel();
                                cxc.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_CXC].ToString().Trim());
                                cxc.customerId = customerId;
                                cxc.customerCode = customerCode;
                                cxc.creditDocumentId = Convert.ToInt32(reader.GetValue(2).ToString().Trim());
                                cxc.creditDocumentFolio = reader.GetValue(3).ToString().Trim();
                                cxc.diasAtraso = reader.GetInt32(4);
                                cxc.saldo_actual = Convert.ToDouble(reader[LocalDatabase.CAMPO_SALDO_ACTUAL_CXC].ToString().Trim());
                                cxc.fechaVencimiento = reader.GetString(6);
                                cxc.factura_mostrador = reader.GetString(8);
                                cxc.reference = reader.GetValue(9).ToString().Trim();
                                cxc.amount = Convert.ToDouble(reader.GetValue(10).ToString().Trim());
                                cxc.enviado = reader.GetInt32(11);
                                cxc.formOfPaymentIdCredit = Convert.ToInt32(reader.GetValue(12).ToString().Trim());
                                cxc.bankId = Convert.ToInt32(reader.GetValue(13).ToString().Trim());
                                cxc.date = reader.GetString(14);
                                cxc.tipo = reader.GetInt32(15);
                                cxc.status = Convert.ToInt32(reader[LocalDatabase.CAMPO_CANCELADO_CXC].ToString().Trim());
                                cxc.documentFolio = reader[LocalDatabase.CAMPO_FOLIOI_CXC].ToString().Trim();
                                cxc.userId = ClsRegeditController.getIdUserInTurn();
                                cxc.nameUser = UserModel.getNameUser(cxc.userId);
                                listaRes.Add(cxc);
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
            return listaRes;
        }

        public static List<CuentasXCobrarModel> getAllCxc(String query)
        {
            List<CuentasXCobrarModel> listaRes = null;
            CuentasXCobrarModel cxc;
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
                            listaRes = new List<CuentasXCobrarModel>();
                            while (reader.Read())
                            {
                                cxc = new CuentasXCobrarModel();
                                cxc.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_CXC].ToString().Trim());
                                cxc.customerId = Convert.ToInt32(reader.GetValue(1).ToString().Trim());
                                cxc.creditDocumentId = reader.GetInt32(2);
                                cxc.creditDocumentFolio = reader.GetString(3);
                                cxc.diasAtraso = reader.GetInt32(4);
                                cxc.saldo_actual = Convert.ToDouble(reader[LocalDatabase.CAMPO_SALDO_ACTUAL_CXC].ToString().Trim());
                                cxc.fechaVencimiento = reader.GetString(6);
                                cxc.factura_mostrador = reader.GetString(8);
                                cxc.reference = reader.GetValue(9).ToString().Trim();
                                cxc.amount = reader.GetDouble(10);
                                cxc.enviado = reader.GetInt32(11);
                                cxc.formOfPaymentIdCredit = reader.GetInt32(12);
                                cxc.bankId = reader.GetInt32(13);
                                cxc.date = reader.GetValue(14).ToString().Trim();
                                cxc.tipo = reader.GetInt32(15);
                                cxc.status = Convert.ToInt32(reader[LocalDatabase.CAMPO_CANCELADO_CXC].ToString().Trim());
                                cxc.documentFolio = reader[LocalDatabase.CAMPO_FOLIOI_CXC].ToString().Trim();
                                listaRes.Add(cxc);
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
            return listaRes;
        }

        public static List<CuentasXCobrarModel> getAllPagos()
        {
            List<CuentasXCobrarModel> pagosList = null;
            CuentasXCobrarModel cxc;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_CXC + " WHERE " +
                        LocalDatabase.CAMPO_TIPO_CXC + " = " + REP_DOCUMENT_TYPE +
                        " AND " + LocalDatabase.CAMPO_CANCELADO_CXC + " = " + 0;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            pagosList = new List<CuentasXCobrarModel>();
                            while (reader.Read())
                            {
                                cxc = new CuentasXCobrarModel();
                                cxc.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_CXC].ToString().Trim());
                                cxc.customerId = reader.GetInt32(1);
                                cxc.creditDocumentId = reader.GetInt32(2);
                                cxc.creditDocumentFolio = reader.GetString(3);
                                cxc.diasAtraso = reader.GetInt32(4);
                                cxc.saldo_actual = Convert.ToDouble(reader[LocalDatabase.CAMPO_SALDO_ACTUAL_CXC].ToString().Trim());
                                cxc.fechaVencimiento = reader.GetString(6);
                                cxc.factura_mostrador = reader.GetString(8);
                                cxc.reference = reader.GetValue(9).ToString().Trim();
                                cxc.amount = reader.GetDouble(10);
                                cxc.enviado = reader.GetInt32(11);
                                cxc.formOfPaymentIdCredit = reader.GetInt32(12);
                                cxc.bankId = reader.GetInt32(13);
                                cxc.date = reader.GetValue(14).ToString().Trim();
                                cxc.tipo = reader.GetInt32(15);
                                cxc.status = Convert.ToInt32(reader[LocalDatabase.CAMPO_CANCELADO_CXC].ToString().Trim());
                                cxc.documentFolio = reader[LocalDatabase.CAMPO_FOLIOI_CXC].ToString().Trim();
                                pagosList.Add(cxc);
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
            return pagosList;
        }

        public static Boolean updateAEnviadoUnaCxc(int repIdApp, int repIdServer)
        {
            Boolean updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_CXC + " SET " + 
                    LocalDatabase.CAMPO_ENVIADO_CXC + " = @repIdServer WHERE " +
                    LocalDatabase.CAMPO_ID_CXC + " = @repIdApp";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@repIdServer", repIdServer);
                    command.Parameters.AddWithValue("@repIdApp", repIdApp);
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

        public static int deleteAllCuentasPorCobrar()
        {
            int resp = 1;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand("DELETE FROM " + LocalDatabase.TABLA_CXC+" WHERE "+
                    LocalDatabase.CAMPO_TIPO_CXC+" = 1", db))
                {
                    int records = command.ExecuteNonQuery();
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

        public static bool deleteAllCuentasPorCobrarByCustomer(int idCustomer)
        {
            bool deleted = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "DELETE FROM " + LocalDatabase.TABLA_CXC + " WHERE " +
                    LocalDatabase.CAMPO_TIPO_CXC + " = 1 AND " + LocalDatabase.CAMPO_CLIENTE_ID_CXC + " = @idCustomer";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idCustomer", idCustomer);
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

        public static int deleteAllAbonosDeClientes()
        {
            int resp = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "DELETE FROM " + LocalDatabase.TABLA_CXC+" WHERE "+LocalDatabase.CAMPO_TIPO_CXC+" = 2 AND "+
                    LocalDatabase.CAMPO_ENVIADO_CXC+" > 0";
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

        /*public static bool createCxc(dynamic objectCredits)
        {
            bool updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_CXC + " SET " + LocalDatabase.CAMPO_CLIENTE_ID_CXC + " = " + arrayCredits[i].customerId + ", " +
                                LocalDatabase.CAMPO_FOLIO_CXC + " = '" + arrayCredits[i].creditDocumentFolio + "', " +
                                LocalDatabase.CAMPO_DIAS_ATRASO_CXC + " = " + arrayCredits[i].diasDeAtraso + ", " +
                                LocalDatabase.CAMPO_SALDO_ACTUAL_CXC + " = " + arrayCredits[i].saldoActual + ", " +
                                LocalDatabase.CAMPO_FECHA_VENCE_CXC + " = '" + arrayCredits[i].fechaDeVencimiento + "', " +
                                LocalDatabase.CAMPO_DESCRIPCION_CXC + " = '" + arrayCredits[i].observaciones + "', " +
                                LocalDatabase.CAMPO_FACTURA_MOSTRADOR_CXC + " = '" + arrayCredits[i].concepto + "' WHERE " +
                                LocalDatabase.CAMPO_DOCTO_CC_ID_CXC + " = " + arrayCredits[i].id;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idCliente", objectCredits.customerId);
                    command.Parameters.AddWithValue("@folioDocumento", objectCredits.creditDocumentFolio);
                    command.Parameters.AddWithValue("@diasAtraso", objectCredits.diasDeAtraso);
                    command.Parameters.AddWithValue("@saldoActual", objectCredits.saldoActual);
                    command.Parameters.AddWithValue("@fechaVencimiento", objectCredits.fechaDeVencimiento);
                    command.Parameters.AddWithValue("@observaciones", objectCredits.observaciones);
                    command.Parameters.AddWithValue("@concepto", objectCredits.concepto);
                    command.Parameters.AddWithValue("@idDocumento", objectCredits.id);
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
        }*/

        public static bool updateCxc(dynamic objectCredits)
        {
            bool updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_CXC + " SET " + LocalDatabase.CAMPO_CLIENTE_ID_CXC + " = @idCliente, " +
                                LocalDatabase.CAMPO_FOLIO_CXC + " = @folioDocumento, " +
                                LocalDatabase.CAMPO_DIAS_ATRASO_CXC + " = @diasAtraso, " +
                                LocalDatabase.CAMPO_SALDO_ACTUAL_CXC + " = @saldoActual, " +
                                LocalDatabase.CAMPO_FECHA_VENCE_CXC + " = @fechaVencimiento, " +
                                LocalDatabase.CAMPO_DESCRIPCION_CXC + " = @observaciones, " +
                                LocalDatabase.CAMPO_FACTURA_MOSTRADOR_CXC + " = @concepto WHERE " +
                                LocalDatabase.CAMPO_DOCTO_CC_ID_CXC + " = @idDocumento";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idCliente", objectCredits.customerId);
                    command.Parameters.AddWithValue("@folioDocumento", objectCredits.creditDocumentFolio);
                    command.Parameters.AddWithValue("@diasAtraso", objectCredits.diasDeAtraso);
                    command.Parameters.AddWithValue("@saldoActual", objectCredits.saldoActual);
                    command.Parameters.AddWithValue("@fechaVencimiento", objectCredits.fechaDeVencimiento);
                    command.Parameters.AddWithValue("@observaciones", objectCredits.observaciones);
                    command.Parameters.AddWithValue("@concepto", objectCredits.concepto);
                    command.Parameters.AddWithValue("@idDocumento", objectCredits.id);
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

        public static bool updateCxc(CreditResponse objectCredits)
        {
            bool updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_CXC + " SET " + LocalDatabase.CAMPO_CLIENTE_ID_CXC + " = @idCliente, " +
                                LocalDatabase.CAMPO_FOLIO_CXC + " = @folioDocumento, " +
                                LocalDatabase.CAMPO_DIAS_ATRASO_CXC + " = @diasAtraso, " +
                                LocalDatabase.CAMPO_SALDO_ACTUAL_CXC + " = @saldoActual, " +
                                LocalDatabase.CAMPO_FECHA_VENCE_CXC + " = @fechaVencimiento, " +
                                LocalDatabase.CAMPO_DESCRIPCION_CXC + " = @observaciones, " +
                                LocalDatabase.CAMPO_FACTURA_MOSTRADOR_CXC + " = @concepto WHERE " +
                                LocalDatabase.CAMPO_DOCTO_CC_ID_CXC + " = @idDocumento AND "+
                                LocalDatabase.CAMPO_TIPO_CXC+" = 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idCliente", objectCredits.customerId);
                    command.Parameters.AddWithValue("@folioDocumento", objectCredits.creditDocumentFolio);
                    command.Parameters.AddWithValue("@diasAtraso", objectCredits.diasDeAtraso);
                    command.Parameters.AddWithValue("@saldoActual", objectCredits.saldoActual);
                    command.Parameters.AddWithValue("@fechaVencimiento", objectCredits.fechaDeVencimiento);
                    command.Parameters.AddWithValue("@observaciones", objectCredits.observaciones);
                    command.Parameters.AddWithValue("@concepto", objectCredits.concepto);
                    command.Parameters.AddWithValue("@idDocumento", objectCredits.id);
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

    }
}
