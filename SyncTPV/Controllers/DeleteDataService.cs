using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Dynamic;
using System.Threading.Tasks;

namespace SyncTPV.Controllers
{
    public class DeleteDataService
    {
        private Boolean newQuery = false;

        public async Task<ExpandoObject> deleteDownloadedDocuments(Boolean newQuery)
        {
            dynamic response = null;
            this.newQuery = newQuery;
            await Task.Run(async () =>
            {
                response = await handleActionDeleteDownloadDocuments();
            });
            return response;
        }

        public async Task<ExpandoObject> deleteDownloadedDocumentsLAN(Boolean newQuery)
        {
            dynamic response = null;
            this.newQuery = newQuery;
            await Task.Run(async () =>
            {
                response = await handleActionDeleteDownloadDocuments();
            });
            return response;
        }

        private async Task<ExpandoObject> handleActionDeleteDownloadDocuments()
        {
            dynamic response = null;
            int totalDocuments = 0;
            int countDeleted = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_ID_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA +
                        " WHERE " + LocalDatabase.CAMPO_ENVIADOALWS_DOC + " = " + 1 + " AND " + LocalDatabase.CAMPO_CANCELADO_DOC + " = " + 0;
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
                                    totalDocuments++;
                                    if (MovimientosModel.checkIfThereAreStillMovementsForTheDocumentInShift(db, 
                                        Convert.ToInt32(reader.GetValue(0).ToString().Trim()))) {
                                        countDeleted++;
                                    } else {
                                        if (DocumentModel.deleteDocumentsDownloadedFromOtherDates(db, Convert.ToInt32(reader.GetValue(0).ToString().Trim())))
                                            countDeleted++;
                                    }
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
                if (totalDocuments == countDeleted)
                {
                    //Toast.makeText(context, "Documentos Actualizados!", Toast.LENGTH_SHORT).show();
                    if (newQuery)
                    {
                        response = new ExpandoObject();
                        response.valor = 100;
                        response.descripcion = "Documentos Actualizados";
                    }
                    //onDestroy();
                }
            }
            return response;
        }

        public async Task<ExpandoObject> deleteDownloadedWithdrawals(Boolean newQuery)
        {
            dynamic response = null;
            await Task.Run(async () =>
            {
                this.newQuery = newQuery;
                response = await handleActionDeleteDownloadWithdrawals();
            });
            return response;
        }

        private async Task<ExpandoObject> handleActionDeleteDownloadWithdrawals()
        {
            dynamic response = null;
            int totalWithdrawals = 0;
            int count = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_ID_RETIRO + " FROM " + LocalDatabase.TABLA_RETIROS +
                        " WHERE " + LocalDatabase.CAMPO_DOWNLOADED_RETIRO + " = " + 1;
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
                                    totalWithdrawals++;
                                    MontoRetiroModel.deleteAllAmountsFromAWithdrawal(db, Convert.ToInt32(reader.GetValue(0).ToString().Trim()));
                                    RetiroModel.deleteAWithdrawal(db, Convert.ToInt32(reader.GetValue(0).ToString().Trim()));
                                    count++;
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
                if (totalWithdrawals == count)
                {
                    //Toast.makeText(context, "Retiros Actualizados!", Toast.LENGTH_SHORT).show();
                    if (newQuery)
                    {
                        response = new ExpandoObject();
                        response.valor = 100;
                        response.descripcion = "Retiros Actualizados";
                    }
                    //onDestroy();
                }
            }
            return response;
        }
    }
}
