using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Dynamic;
using System.Windows.Documents;

namespace SyncTPV.Models
{
    public class FormasDeCobroDocumentoModel
    {
        public int id { get; set; }
        public int formaCobroIdAbono { get; set; }
        public double importe { get; set; }
        public double totalDocumento { get; set; }
        public double cambio { get; set; }
        public double saldoDocumento { get; set; }
        public int documentoId { get; set; }
        public int idServer { get; set; }

        public static Boolean removePendingBalanceToTheLastFormOfCollectionOfTheDocument(int idDocumento)
        {
            Boolean changed = false;
            if (checkIfThereIsAlreadyASubscriptionToAPaymentMethod(idDocumento))
            {
                var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
                db.Open();
                try
                {
                    String query = "DELETE FROM " + LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO + " WHERE " + 
                        LocalDatabase.CAMPO_DOCID_FORMACOBRODOC + " = " + idDocumento;
                    using (SQLiteCommand command = new SQLiteCommand(query, db))
                    {
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
            else
            {
                return true;
            }
        }

        public static double getAllTotalForAFormaDePagoInReporteDeCorte(int idFormaPago)
        {
            double suma = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT SUM(FCD." + LocalDatabase.CAMPO_IMPORTE_FORMACOBRODOC + ") " +
                        "FROM " + LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO + " FCD " +
                        " INNER JOIN " + LocalDatabase.TABLA_DOCUMENTOVENTA + " D ON D." + LocalDatabase.CAMPO_ID_DOC + " = " +
                        "FCD." + LocalDatabase.CAMPO_DOCID_FORMACOBRODOC +
                        " WHERE FCD." + LocalDatabase.CAMPO_FORMACOBROIDABONO_FORMACOBRODOC + " = " + idFormaPago +
                                " AND D." + LocalDatabase.CAMPO_CANCELADO_DOC + " = " + 0 +
                        " AND D." + LocalDatabase.CAMPO_PAUSAR_DOC + " = " + 0 + " AND (" +
                        LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 4 + " OR D."+ LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = 2)";
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

        public static double getSumCambioForAFormaDePagoInReporteDeCorte(int idFormaPago)
        {
            double suma = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT SUM(FCD." + LocalDatabase.CAMPO_CAMBIO_FORMACOBRODOC + ") " +
                        "FROM " + LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO + " FCD " +
                        " INNER JOIN " + LocalDatabase.TABLA_DOCUMENTOVENTA + " D ON D." + LocalDatabase.CAMPO_ID_DOC + " = " +
                        "FCD." + LocalDatabase.CAMPO_DOCID_FORMACOBRODOC +
                        " WHERE FCD." + LocalDatabase.CAMPO_FORMACOBROIDABONO_FORMACOBRODOC + " = " + idFormaPago +
                        " AND D." + LocalDatabase.CAMPO_CANCELADO_DOC + " = " + 0 +
                        " AND D." + LocalDatabase.CAMPO_PAUSAR_DOC + " = " + 0 + " AND (D." + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 2+" OR "+
                        LocalDatabase.CAMPO_TIPODOCUMENTO_DOC+" = "+4+")";
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

        public static Boolean checkIfThereIsAlreadyASubscriptionToAPaymentMethod(int idDocumento)
        {
            Boolean exists = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO + " WHERE " +
                        LocalDatabase.CAMPO_DOCID_FORMACOBRODOC + " = " + idDocumento;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                            exists = true;
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

        public static int deleteAllFcOfADocument(int documentoId)
        {
            int resp = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "DELETE FROM " + LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO + " WHERE " + LocalDatabase.CAMPO_DOCID_FORMACOBRODOC +
                        " = " + documentoId;
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

        public static double getCalculoFinalDocumentOFormasCobro(int documentoId)
        {
            double resp = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO + " WHERE " + LocalDatabase.CAMPO_DOCID_FORMACOBRODOC +
                        " = " + documentoId;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                resp = resp+Convert.ToDouble(reader[LocalDatabase.CAMPO_IMPORTE_FORMACOBRODOC].ToString().Trim());
                            }
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
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return resp;
        }

        public static bool recalculoFormasCobroDocumento(int documentoId)
        {
            bool recalculado = true;
            double saldo = 0;
            Boolean withoutFcDocuments = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            //traer doumento
            List<dynamic> FormasCobroDoc = new List<dynamic>();
            double totalAPagar = 0;
            db.Open();
            try
            {
                String query = "SELECT * FROM " +
                        LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO + " WHERE " + LocalDatabase.CAMPO_DOCID_FORMACOBRODOC + " = " + documentoId;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            withoutFcDocuments = true;
                            while (reader.Read())
                            {
                                dynamic forma = new ExpandoObject();
                                forma.id = Convert.ToUInt32(reader[LocalDatabase.CAMPO_ID_FORMACOBRODOC].ToString().Trim());
                                forma.idTipoPago = Convert.ToUInt32(reader[LocalDatabase.CAMPO_FORMACOBROIDABONO_FORMACOBRODOC].ToString().Trim());
                                forma.importe = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMPORTE_FORMACOBRODOC].ToString().Trim());
                                forma.totalDoc = Convert.ToDouble(reader[LocalDatabase.CAMPO_TOTALDOC_FORMACOBRODOC].ToString().Trim());
                                forma.pendienteDoc = Convert.ToDouble(reader[LocalDatabase.CAMPO_SALDODOC_FORMACOBRODOC].ToString().Trim());
                                forma.cambio = Convert.ToDouble(reader[LocalDatabase.CAMPO_CAMBIO_FORMACOBRODOC].ToString().Trim());
                                forma.idDoc = Convert.ToDouble(reader[LocalDatabase.CAMPO_DOCID_FORMACOBRODOC].ToString().Trim());
                                forma.idDocServer = Convert.ToDouble(reader[LocalDatabase.CAMPO_IDSERVER_FORMACOBRODOC].ToString().Trim());
                                FormasCobroDoc.Add(forma);
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
            //traer formas de cobro
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_TOTAL_DOC + " FROM " +
                        LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " + LocalDatabase.CAMPO_ID_DOC + " = " + documentoId +
                        " ORDER BY " + LocalDatabase.CAMPO_ID_DOC + " DESC LIMIT 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            withoutFcDocuments = true;
                            while (reader.Read())
                            {
                                totalAPagar = Convert.ToDouble(reader[LocalDatabase.CAMPO_TOTAL_DOC].ToString().Trim());
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
            double porpagar = totalAPagar;
            if(totalAPagar < 0)
            {
                recalculado = false;
            }
            else
            {
                bool correcto = false;
                if(FormasCobroDoc != null)
                {
                    for(int x = 0; x< FormasCobroDoc.Count; x++)
                    {
                        double aporpagar = porpagar - FormasCobroDoc[x].importe;
                        decimal eporpagar =  decimal.Round((decimal) aporpagar, 2);
                        porpagar = Convert.ToDouble(eporpagar);
                        if (porpagar < 0 && (x+1)  == FormasCobroDoc.Count)
                        {
                            FormasCobroDoc[x].cambio = MetodosGenerales.obtieneDosDecimales(Math.Abs(porpagar));
                            FormasCobroDoc[x].pendienteDoc = 0;
                        }
                        else
                        {
                            if(porpagar < 0)
                            {
                                
                                FormasCobroDoc[x].cambio = MetodosGenerales.obtieneDosDecimales(Math.Abs(porpagar));
                                FormasCobroDoc[x].pendienteDoc = 0;
                                porpagar = 0;
                            }
                            else
                            {
                                FormasCobroDoc[x].cambio = 0;
                                FormasCobroDoc[x].pendienteDoc = MetodosGenerales.obtieneDosDecimales(porpagar);
                            }
                        }
                        FormasCobroDoc[x].totalDoc = totalAPagar;
                    }
                    //insert
                    for (int x = 0; x < FormasCobroDoc.Count; x++)
                    {
                        correcto = FormasDeCobroDocumentoModel.updateRecalculoBD(
                             (int) FormasCobroDoc[x].idTipoPago, (int) documentoId, (double) FormasCobroDoc[x].importe, (double) FormasCobroDoc[x].cambio, (double) FormasCobroDoc[x].pendienteDoc
                            );
                        if (correcto == false)
                        {
                            recalculado = false;
                        }
                    }
                }
            }
            return recalculado;
        }

        public static double getSaldoPendienteOfTheLastFcDcoument(int documentoId)
        {
            double saldo = 0;
            Boolean withoutFcDocuments = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_SALDODOC_FORMACOBRODOC + " FROM " +
                        LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO + " WHERE " + LocalDatabase.CAMPO_DOCID_FORMACOBRODOC + " = " + documentoId +
                        " ORDER BY " + LocalDatabase.CAMPO_ID_FORMACOBRODOC + " DESC LIMIT 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            withoutFcDocuments = true;
                            while (reader.Read())
                            {
                                saldo = Convert.ToDouble(reader[LocalDatabase.CAMPO_SALDODOC_FORMACOBRODOC].ToString().Trim());
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
                if (saldo == 0 && !withoutFcDocuments)
                {
                    saldo = DocumentModel.getTotalForADocument(documentoId);
                }
            }
            return saldo;
        }

        public static double getCambioOfTheDcoument(int documentoId)
        {
            double cambio = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_CAMBIO_FORMACOBRODOC + " FROM " +
                        LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO + " WHERE " + LocalDatabase.CAMPO_DOCID_FORMACOBRODOC + " = " + documentoId +
                        " ORDER BY " + LocalDatabase.CAMPO_ID_FORMACOBRODOC + " ASC";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cambio += Convert.ToDouble(reader[LocalDatabase.CAMPO_CAMBIO_FORMACOBRODOC].ToString().Trim());
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
            return cambio;
        }

        public static List<FormasDeCobroDocumentoModel> getAllTheWaysToCollectADocument(int documentoId)
        {
            List<FormasDeCobroDocumentoModel> fcDocumentsList = new List<FormasDeCobroDocumentoModel>();
            FormasDeCobroDocumentoModel fcdm;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO + " WHERE " +
                        LocalDatabase.CAMPO_DOCID_FORMACOBRODOC + " = " + documentoId;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                fcdm = new FormasDeCobroDocumentoModel();
                                fcdm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_FORMACOBRODOC].ToString().Trim());
                                fcdm.formaCobroIdAbono = Convert.ToInt32(reader[LocalDatabase.CAMPO_FORMACOBROIDABONO_FORMACOBRODOC].ToString().Trim());
                                fcdm.importe = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMPORTE_FORMACOBRODOC].ToString().Trim());
                                fcdm.totalDocumento = Convert.ToDouble(reader[LocalDatabase.CAMPO_TOTALDOC_FORMACOBRODOC].ToString().Trim());
                                fcdm.cambio = Convert.ToDouble(reader[LocalDatabase.CAMPO_CAMBIO_FORMACOBRODOC].ToString().Trim());
                                fcdm.saldoDocumento = Convert.ToDouble(reader[LocalDatabase.CAMPO_SALDODOC_FORMACOBRODOC].ToString().Trim());
                                fcdm.documentoId = documentoId;
                                fcDocumentsList.Add(fcdm);
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
            return fcDocumentsList;
        }

        public static List<FormasDeCobroDocumentoModel> getAllTheWaysToCollect(String query)
        {
            List<FormasDeCobroDocumentoModel> fcDocumentsList = null;
            FormasDeCobroDocumentoModel fcdm;
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
                            fcDocumentsList = new List<FormasDeCobroDocumentoModel>();
                            while (reader.Read())
                            {
                                fcdm = new FormasDeCobroDocumentoModel();
                                fcdm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_FORMACOBRODOC].ToString().Trim());
                                fcdm.formaCobroIdAbono = Convert.ToInt32(reader[LocalDatabase.CAMPO_FORMACOBROIDABONO_FORMACOBRODOC].ToString().Trim());
                                fcdm.importe = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMPORTE_FORMACOBRODOC].ToString().Trim());
                                fcdm.totalDocumento = Convert.ToDouble(reader[LocalDatabase.CAMPO_TOTALDOC_FORMACOBRODOC].ToString().Trim());
                                fcdm.cambio = Convert.ToDouble(reader[LocalDatabase.CAMPO_CAMBIO_FORMACOBRODOC].ToString().Trim());
                                fcdm.saldoDocumento = Convert.ToDouble(reader[LocalDatabase.CAMPO_SALDODOC_FORMACOBRODOC].ToString().Trim());
                                fcdm.documentoId = Convert.ToInt32(reader[LocalDatabase.CAMPO_DOCID_FORMACOBRODOC].ToString().Trim());
                                fcdm.idServer = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDSERVER_FORMACOBRODOC].ToString().Trim());
                                fcDocumentsList.Add(fcdm);
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
            return fcDocumentsList;
        }

        public static Boolean updateIdServerInAFcDocument(int idApp, int idServer)
        {
            Boolean updated = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "UPDATE " + LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO + " SET " + LocalDatabase.CAMPO_IDSERVER_FORMACOBRODOC + " = " + idServer + " WHERE " +
                    LocalDatabase.CAMPO_ID_FORMACOBRODOC + " = " + idApp;
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

        public static double getTheImportAbonadoForAFormaDeCobroOfADocument(int fcId, int documentoId)
        {
            double importeAbonado = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_IMPORTE_FORMACOBRODOC + " FROM " + LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO +
                        " WHERE " + LocalDatabase.CAMPO_FORMACOBROIDABONO_FORMACOBRODOC + " = " + fcId + " AND " + LocalDatabase.CAMPO_DOCID_FORMACOBRODOC +
                        " = " + documentoId;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                importeAbonado = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMPORTE_FORMACOBRODOC].ToString().Trim());
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
            return importeAbonado;
        }

        public static int createUpdateOrDeleteFcDocument(int idDocument, int fcId, double amount)
        {
            int response = 0;
            if (checkIfThereIsAPaymentFormIdAndDocumentId(fcId, idDocument))
            {
                if (amount > 0)
                {
                    if (checkIfItIsTheLastRecordWithFcId(fcId, idDocument))
                    {
                        if (deleteAFcDocument(idDocument, fcId))
                        {
                            dynamic map = getTotalAndBalanceFromTheLastFcIdDoc(idDocument);
                            if (map != null)
                            {
                                double lastBalance = map.get("balance");
                                double balance = lastBalance - amount;
                                if (balance < 0)
                                {
                                    if (updateBalanceAndChange(fcId, idDocument, Math.Abs(balance), 0))
                                    {
                                        response = 1;
                                    }
                                }
                                else
                                {
                                    if (updateBalanceAndChange(fcId, idDocument, 0, balance))
                                    {
                                        response = 1;
                                    }
                                }
                            }
                            else
                            {
                                double totalDocument = DocumentModel.getTotalForADocumentWithContext(idDocument);
                                double balance = totalDocument - amount;
                                if (balance < 0)
                                {
                                    if (addNewFcDocument(idDocument, fcId, totalDocument, amount, Math.Abs(balance), 0))
                                    {
                                        response = 1;
                                    }
                                }
                                else
                                {
                                    if (addNewFcDocument(idDocument, fcId, totalDocument, amount, 0, balance))
                                    {
                                        response = 1;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //aquimodificar
                        if (updateRecalculoBD(idDocument, fcId,amount,0,0))
                        {
                            recalculoFormasCobroDocumento(idDocument);
                            /*
                            List<FormasDeCobroDocumentoModel> fcdList = getAllDataFromTheFirstFcIdDoc(idDocument);
                            if (fcdList != null)
                            {
                                for (int i = 0; i < fcdList.Count; i++)
                                {
                                    if (i == 0)
                                    {
                                        double balance = fcdList[i].totalDocumento - fcdList[i].importe;
                                        if (balance < 0)
                                        {
                                            if (updateBalanceAndChange(fcdList[i].formaCobroIdAbono, idDocument, Math.Abs(balance), 0))
                                            {
                                                response = 1;
                                            }
                                        }
                                        else
                                        {
                                            if (updateBalanceAndChange(fcdList[i].formaCobroIdAbono, idDocument, 0, balance))
                                            {
                                                response = 1;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        double balance = fcdList[i - 1].saldoDocumento - fcdList[i].importe;
                                        if (balance < 0)
                                        {
                                            if (updateBalanceAndChange(fcdList[i].formaCobroIdAbono, idDocument, Math.Abs(balance), 0))
                                            {
                                                response = 1;
                                            }
                                        }
                                        else
                                        {
                                            if (updateBalanceAndChange(fcdList[i].formaCobroIdAbono, idDocument, 0, balance))
                                            {
                                                response = 1;
                                            }
                                        }
                                    }
                                }
                            }
                            dynamic map = getTotalAndBalanceFromTheLastFcIdDoc(idDocument);
                            if (map != null)
                            {
                                double lastTotal = map.get("total");
                                double total = map.get("balance");
                                double balance = total - amount;
                                if (balance < 0)
                                {
                                    if (addNewFcDocument(idDocument, fcId, lastTotal, amount, Math.Abs(balance), 0))
                                    {
                                        response = 1;
                                    }
                                }
                                else
                                {
                                    if (addNewFcDocument(idDocument, fcId, lastTotal, amount, 0, balance))
                                    {
                                        response = 1;
                                    }
                                }
                            }*/
                        }
                    }
                }
                else
                {
                    if (checkIfItIsTheLastRecordWithFcId(fcId, idDocument))
                    {
                        if (deleteAFcDocument(idDocument, fcId))
                        {
                            response = 1;
                        }
                    }
                    else
                    {
                        if (deleteAFcDocument(idDocument, fcId))
                        {
                            List<FormasDeCobroDocumentoModel> fcdList = getAllDataFromTheFirstFcIdDoc(idDocument);
                            if (fcdList != null)
                            {
                                for (int i = 0; i < fcdList.Count; i++)
                                {
                                    if (i == 0)
                                    {
                                        double balance = fcdList[i].totalDocumento - fcdList[i].importe;
                                        if (balance < 0)
                                        {
                                            if (updateBalanceAndChange(fcdList[i].formaCobroIdAbono, idDocument, Math.Abs(balance), 0))
                                            {
                                                response = 1;
                                            }
                                        }
                                        else
                                        {
                                            if (updateBalanceAndChange(fcdList[i].formaCobroIdAbono, idDocument, 0, balance))
                                            {
                                                response = 1;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        double balance = fcdList[i - 1].saldoDocumento - fcdList[i].importe;
                                        if (balance < 0)
                                        {
                                            if (updateBalanceAndChange(fcdList[i].formaCobroIdAbono, idDocument, Math.Abs(balance), 0))
                                            {
                                                response = 1;
                                            }
                                        }
                                        else
                                        {
                                            if (updateBalanceAndChange(fcdList[i].formaCobroIdAbono, idDocument, 0, balance))
                                            {
                                                response = 1;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (checkIfThereIsAlreadyASubscriptionToAPaymentMethod(idDocument))
                {
                    if (amount > 0)
                    {
                        dynamic map = getTotalAndBalanceFromTheLastFcIdDoc(idDocument);
                        if (map != null)
                        {
                            double lastTotal = map.total;
                            double total = map.balance;
                            double balance = 0;
                            if (total == 0)
                            {
                                balance = Convert.ToDouble("-" + amount);
                            }
                            else
                            {
                                balance = total - amount;
                            }
                            if (balance < 0)
                            {
                                if (addNewFcDocument(idDocument, fcId, lastTotal, amount, Math.Abs(balance), 0))
                                {
                                    response = 1;
                                }
                            }
                            else
                            {
                                if (addNewFcDocument(idDocument, fcId, lastTotal, amount, 0, balance))
                                {
                                    response = 1;
                                }
                            }
                        }
                    }
                    else
                    {
                        response = -1;
                    }
                }
                else
                {
                    if (amount <= 0)
                    {
                        response = -1;
                    }
                    else
                    {
                        double totalDocument = DocumentModel.getTotalForADocumentWithContext(idDocument);
                        double balance = totalDocument - amount;
                        if (balance < 0)
                        {
                            if (addNewFcDocument(idDocument, fcId, totalDocument, amount, Math.Abs(balance), 0))
                            {
                                response = 1;
                            }
                        }
                        else
                        {
                            if (addNewFcDocument(idDocument, fcId, totalDocument, amount, 0, balance))
                            {
                                response = 1;
                            }
                        }
                    }
                }
            }
            return response;
        }

        private static bool checkIfThereIsAPaymentFormIdAndDocumentId(int fcId, int idDocument)
        {
            Boolean exists = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO + " WHERE " +
                        LocalDatabase.CAMPO_FORMACOBROIDABONO_FORMACOBRODOC + " = " + fcId + " AND " +
                        LocalDatabase.CAMPO_DOCID_FORMACOBRODOC + " = " + idDocument;
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
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return exists;
        }

        public static Boolean checkIfExistWithFcId(int fcId, int idDocument)
        {
            Boolean isIt = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                 String query = "SELECT " + LocalDatabase.CAMPO_FORMACOBROIDABONO_FORMACOBRODOC + " FROM " +
                        LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO + " WHERE " + LocalDatabase.CAMPO_DOCID_FORMACOBRODOC + " = " + idDocument + 
                        " AND "+LocalDatabase.CAMPO_FORMACOBROIDABONO_FORMACOBRODOC+" = "+fcId;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (Convert.ToInt32(reader[LocalDatabase.CAMPO_FORMACOBROIDABONO_FORMACOBRODOC].ToString().Trim()) == fcId)
                                {
                                    isIt = true;
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
            return isIt;
        }

        public static Boolean checkIfItIsTheLastRecordWithFcId(int fcId, int idDocument)
        {
            Boolean isIt = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_FORMACOBROIDABONO_FORMACOBRODOC + " FROM " +
                        LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO + " WHERE " + LocalDatabase.CAMPO_DOCID_FORMACOBRODOC + " = " + idDocument + " ORDER BY " +
                        LocalDatabase.CAMPO_ID_FORMACOBRODOC + " DESC LIMIT 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (Convert.ToInt32(reader[LocalDatabase.CAMPO_FORMACOBROIDABONO_FORMACOBRODOC].ToString().Trim()) == fcId)
                                {
                                    isIt = true;
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
            return isIt;
        }

        public static Boolean addNewFcDocument(int idDocument, int fcId, double totalDocument,
                                            double amount, double change, double balance)
        {
            Boolean created = false;
            change = MetodosGenerales.obtieneDosDecimales(change);
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                int id = getTheLastId() + 1;
                String query = "INSERT INTO " + LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO + " (" + LocalDatabase.CAMPO_ID_FORMACOBRODOC + ", " +
                    LocalDatabase.CAMPO_FORMACOBROIDABONO_FORMACOBRODOC + ", " + LocalDatabase.CAMPO_IMPORTE_FORMACOBRODOC + ", " +
                    LocalDatabase.CAMPO_TOTALDOC_FORMACOBRODOC + ", " + LocalDatabase.CAMPO_CAMBIO_FORMACOBRODOC + ", " +
                    LocalDatabase.CAMPO_SALDODOC_FORMACOBRODOC + ", " + LocalDatabase.CAMPO_DOCID_FORMACOBRODOC + ") " +
                    "VALUES(" + id + ", " + fcId + ", " + amount + ", " + totalDocument + ", " + change + ", " + balance + ", " + idDocument + ")";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        created = true;
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
            return created;
        }

        public static int getTheLastId()
        {
            int lastId = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_ID_FORMACOBRODOC + " FROM " + LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO +
                    " ORDER BY " + LocalDatabase.CAMPO_ID_FORMACOBRODOC + " DESC LIMIT 1";
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

        public static Boolean deleteAFcDocument(int idDocument, int fcId)
        {
            Boolean deleted = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "DELETE FROM " + LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO + " WHERE " + LocalDatabase.CAMPO_FORMACOBROIDABONO_FORMACOBRODOC +
                        " = " + fcId + " AND " + LocalDatabase.CAMPO_DOCID_FORMACOBRODOC + " = " + idDocument;
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

        public static ExpandoObject getTotalAndBalanceFromTheLastFcIdDoc(int idDocument)
        {
            dynamic map = null;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_TOTALDOC_FORMACOBRODOC + ", " +
                        LocalDatabase.CAMPO_SALDODOC_FORMACOBRODOC + " FROM " + LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO + " WHERE " +
                        LocalDatabase.CAMPO_DOCID_FORMACOBRODOC + " = " + idDocument + " ORDER BY " + LocalDatabase.CAMPO_ID_FORMACOBRODOC +
                        " DESC LIMIT 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                map = new ExpandoObject();
                                map.total = Convert.ToDouble(reader[LocalDatabase.CAMPO_TOTALDOC_FORMACOBRODOC].ToString().Trim());
                                map.balance = Convert.ToDouble(reader[LocalDatabase.CAMPO_SALDODOC_FORMACOBRODOC].ToString().Trim());
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
            return map;
        }

        public static Boolean updateBalanceAndChange(int fcId, int idDocument, double change, double balance)
        {
            Boolean updated = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            change = MetodosGenerales.obtieneDosDecimales(change);
            try
            {
                String query = "UPDATE " + LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO + " SET " + LocalDatabase.CAMPO_CAMBIO_FORMACOBRODOC + " = " + change + ", " +
                    LocalDatabase.CAMPO_SALDODOC_FORMACOBRODOC + " = " + balance + " WHERE " + LocalDatabase.CAMPO_DOCID_FORMACOBRODOC +
                        " = " + idDocument + " AND " + LocalDatabase.CAMPO_FORMACOBROIDABONO_FORMACOBRODOC + " = " + fcId;
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

        public static Boolean updateRecalculoBD(int fcId, int idDocument, double importe, double change, double balance)
        {
            Boolean updated = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "UPDATE " + LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO + " SET " + LocalDatabase.CAMPO_CAMBIO_FORMACOBRODOC + " = " + change + ", " +
                    LocalDatabase.CAMPO_IMPORTE_FORMACOBRODOC + " = " + importe + ", " +
                    LocalDatabase.CAMPO_SALDODOC_FORMACOBRODOC + " = " + balance + " WHERE " + LocalDatabase.CAMPO_DOCID_FORMACOBRODOC +
                        " = " + idDocument + " AND " + LocalDatabase.CAMPO_FORMACOBROIDABONO_FORMACOBRODOC + " = " + fcId;
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

        public static void deleteAllFcOfADocumentDb(SQLiteConnection db, int documentoId)
        {
            int response = 0;
            try
            {
                String query = "DELETE FROM " + LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO + " WHERE " + LocalDatabase.CAMPO_DOCID_FORMACOBRODOC +
                        " = " + documentoId;
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
        }

        public static double getTotalForAFormaDeCobroDoc(int fcId)
        {
            double totalPagado = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT SUM(" + LocalDatabase.CAMPO_TOTALDOC_FORMACOBRODOC + ") FROM " + LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO +
                        " WHERE " + LocalDatabase.CAMPO_FORMACOBROIDABONO_FORMACOBRODOC + " = " + fcId;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                totalPagado = Convert.ToDouble(reader.GetValue(0).ToString().Trim());
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
            return totalPagado;
        }

        public static double getTotalFormaDeCobroDocForADocument(int idDocucument, int fcId)
        {
            double totalPagado = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT SUM(" + LocalDatabase.CAMPO_TOTALDOC_FORMACOBRODOC + ") FROM " + LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO +
                        " WHERE " + LocalDatabase.CAMPO_FORMACOBROIDABONO_FORMACOBRODOC + " = " + fcId+" AND "+LocalDatabase.CAMPO_DOCID_FORMACOBRODOC+" = "+idDocucument;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    totalPagado = Convert.ToDouble(reader.GetValue(0).ToString().Trim());
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
            return totalPagado;
        }

        public static List<FormasDeCobroDocumentoModel> getAllDataFromTheFirstFcIdDoc(int idDocument)
        {
            List<FormasDeCobroDocumentoModel> fcdList = null;
            FormasDeCobroDocumentoModel fcd = null;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO + " WHERE " +
                        LocalDatabase.CAMPO_DOCID_FORMACOBRODOC + " = " + idDocument + " ORDER BY " +
                        LocalDatabase.CAMPO_ID_FORMACOBRODOC + " ASC";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            fcdList = new List<FormasDeCobroDocumentoModel>();
                            while (reader.Read())
                            {
                                fcd = new FormasDeCobroDocumentoModel();
                                fcd.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_FORMACOBRODOC].ToString().Trim());
                                fcd.formaCobroIdAbono = Convert.ToInt32(reader[LocalDatabase.CAMPO_FORMACOBROIDABONO_FORMACOBRODOC].ToString().Trim());
                                fcd.importe = Convert.ToDouble(reader[LocalDatabase.CAMPO_IMPORTE_FORMACOBRODOC].ToString().Trim());
                                fcd.totalDocumento = Convert.ToDouble(reader[LocalDatabase.CAMPO_TOTALDOC_FORMACOBRODOC].ToString().Trim());
                                fcd.cambio = Convert.ToDouble(reader[LocalDatabase.CAMPO_CAMBIO_FORMACOBRODOC].ToString().Trim());
                                fcd.saldoDocumento = Convert.ToDouble(reader[LocalDatabase.CAMPO_SALDODOC_FORMACOBRODOC].ToString().Trim());
                                fcdList.Add(fcd);
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
            return fcdList;
        }

        public static int getFcWithHigherAmount(int documentoId)
        {
            int fc = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_FORMACOBROIDABONO_FORMACOBRODOC +
                        " FROM " + LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO + " WHERE " +
                        LocalDatabase.CAMPO_DOCID_FORMACOBRODOC + " = " + documentoId +
                        " AND " + LocalDatabase.CAMPO_IMPORTE_FORMACOBRODOC + " = (SELECT MAX(" + LocalDatabase.CAMPO_IMPORTE_FORMACOBRODOC + ")" +
                        " FROM " + LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO + " WHERE " +
                        LocalDatabase.CAMPO_DOCID_FORMACOBRODOC + " = " + documentoId + ") LIMIT 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                fc = Convert.ToInt32(reader[LocalDatabase.CAMPO_FORMACOBROIDABONO_FORMACOBRODOC].ToString().Trim());
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
            return fc;
        }

        public static Boolean deleteAllFormasDeCobroDocumento()
        {
            bool deleted = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "DELETE FROM " + LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO;
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
