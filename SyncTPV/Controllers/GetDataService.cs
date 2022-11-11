using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Dynamic;
using System.Threading.Tasks;
using wsROMClases;
using static ClsDocumentoModel;

namespace SyncTPV.Controllers
{
    public class GetDataService
    {
        public static readonly int GET_DOCUMENT = 0;
        public static readonly int GET_WITHDRAWAL = 1;

        public async Task<ExpandoObject> downloadAllDocuments(int userId, String startDate, String endDate, int lastId, int limit)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                response = handleGetTotalDocuments(userId, startDate, endDate, lastId);
                if (response.value > 0)
                {
                    response = handleActionDownloadDocuments(userId, startDate, endDate, lastId, limit, response.value);
                    response.information = GET_DOCUMENT;
                } else
                {
                    response.information = GET_DOCUMENT;
                }
            });
            return response;
        }

        public async Task<ExpandoObject> downloadAllDocumentsLAN(int userId, String startDate, String endDate, int lastId, int limit)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                response = handleGetTotalDocumentsLAN(userId, startDate, endDate, lastId);
                if (response.value > 0)
                {
                    response = handleActionDownloadDocumentsLAN(userId, startDate, endDate, lastId, limit, response.value);
                    response.information = GET_DOCUMENT;
                } else
                {
                    response.information = GET_DOCUMENT;
                }
            });
            return response;
        }

        public async Task<ExpandoObject> downloadAllWithdrawals(int userId, String startDate, String endDate, int lastId, int limit)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                response = handleGetTotalWithdrawals(userId, startDate, endDate);
                if (response.value > 0)
                {
                    response = handleActionDownloadWithdrawals(userId, startDate, endDate, lastId, limit, response.value);
                    response.information = GET_WITHDRAWAL;
                }
                else
                {
                    response.information = GET_WITHDRAWAL;
                }
            });
            return response;
        }


        public async Task<ExpandoObject> downloadAllWithdrawalsLAN(int userId, String startDate, String endDate, int lastId, int limit)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                response = handleGetTotalWithdrawalsLAN(userId, startDate, endDate);
                if (response.value > 0)
                {
                    response = handleActionDownloadWithdrawalsLAN(userId, startDate, endDate, lastId, limit, response.value);
                    response.information = GET_WITHDRAWAL;
                } else
                {
                    response.information = GET_WITHDRAWAL;
                }
            });
            return response;
        }

        private ExpandoObject handleGetTotalDocuments(int userId, string startDate, string endDate, int lastId)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            try
            {
                int itemsToEvaluate = 0;
                String url = ConfiguracionModel.getLinkWs();
                url = url.Replace(" ", "%20");
                var client = new RestClient(url);
                // client.Authenticator = new HttpBasicAuthenticator(username, password);
                var request = new RestRequest("/getTotalDocumentsByDateAndUser", Method.Post);
                request.AddJsonBody(new
                {
                    idUsuario = userId,
                    fechaInicio = startDate,
                    fechaFin = endDate
                });
                var responseHeader = client.ExecuteAsync(request);
                if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                {
                    var content = responseHeader.Result.Content; // Raw content as string
                    dynamic respServer = new ExpandoObject();
                    var jsonResp = JsonConvert.DeserializeObject<ExpandoObject>(content);
                    dynamic respJson = (ExpandoObject)jsonResp;
                    if (respJson != null && respJson.total > 0)
                    {
                        value = Convert.ToInt32(respJson.total);
                    }
                    else
                    {
                        description = "No se encontró ningún Documento";
                    }
                }
                else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                {
                    if (responseHeader.Result.ErrorMessage.Equals("Unable to connect to the remote server"))
                    {
                        value = -404;
                        description = "No se pudo establecer conexión con el servidor";
                    }
                    else
                    {
                        value = -500;
                        description = "Algo falló al intentar negociar con el servidor";
                    }
                }
            }
            catch (Exception e)
            {
                SECUDOC.writeLog(e.ToString());
                value = -1;
                description = e.Message;
            }
            finally
            {
                response.value = value;
                response.description = description;
            }
            return response;
        }

        private ExpandoObject handleGetTotalDocumentsLAN(int userId, string startDate, string endDate, int lastId)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            try
            {
                String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                value = ClsDocumentoModel.getTotalDocumentsByDateAndUser(panelInstance,
                    userId, startDate, endDate);
                if (value == 0)
                {
                    description = "No se encontró nigún documento por descargar";
                }
            }
            catch (Exception e)
            {
                SECUDOC.writeLog(e.ToString());
                value = -1;
                description = e.Message;
            }
            finally
            {
                response.value = value;
                response.description = description;
            }
            return response;
        }

        private ExpandoObject handleActionDownloadDocuments(int userId, String startDate, String endDate, int lastId, int limit, int totalDocuments)
        {
            dynamic response = new ExpandoObject();
            /*String dataJson = "{\n" +
                    " \"idDocumento\":" + lastId + ",\n" +
                    " \"idUsuario\":" + userId + ",\n" +
                    " \"fechaInicio\":\"" + startDate + "\",\n" +
                    " \"fechaFin\":\"" + endDate + "\"\n" +
                    "}";*/
            int value = 0;
            String description = "";
            List<DocumentoModel> documentos = null;
            try
            {
                int itemsToEvaluate = 0;
                String url = ConfiguracionModel.getLinkWs();
                url = url.Replace(" ", "%20");
                var client = new RestClient(url);
                // client.Authenticator = new HttpBasicAuthenticator(username, password);
                var request = new RestRequest("/obtainDocumentsWithTheirPaymentFormsByDateOrUserTPV", Method.Post);
                request.AddJsonBody(new
                {
                    lastId = lastId,
                    idUsuario = userId,
                    fechaInicio = startDate,
                    fechaFin = endDate,
                    limit = limit
                });
                var responseHeader = client.ExecuteAsync(request);
                if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                {
                    var content = responseHeader.Result.Content; // Raw content as string
                    var jsonResp = JsonConvert.DeserializeObject<List<DocumentoModel>>(content);
                    List<DocumentoModel> respJson = (List<DocumentoModel>)jsonResp;
                    if (respJson != null && respJson.Count > 0)
                    {
                        value = totalDocuments;
                        documentos = respJson;
                    }
                    else
                    {
                        description = "No se encontraron Documentos que mostrar";
                    }
                }
                else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                {
                    if (responseHeader.Result.ErrorMessage.Equals("Unable to connect to the remote server"))
                    {
                        value = -404;
                        description = "No pudimos establecer conexión con el servidor";
                    }
                    else
                    {
                        value = -500;
                        description = "Algo falló al negociar información con el servidor";
                    }
                }
            }
            catch (Exception e)
            {
                SECUDOC.writeLog(e.ToString());
                value = -1;
                description = e.Message;
            }
            finally
            {
                response.value = value;
                response.description = description;
                response.documentos = documentos;
            }
            return response;
        }

        private ExpandoObject handleActionDownloadDocumentsLAN(int userId, String startDate, String endDate, int lastId, int limit, int totalDocuments)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            List<DocumentoModel> documentos = null;
            try
            {
                String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                List<DocumentoModel> obtainedDocs = ClsDocumentoModel.getDocumentsWithTheirPaymentFormsByDateAndUserTPV(panelInstance,
                        lastId, userId, startDate, endDate, limit);
                if (obtainedDocs != null && obtainedDocs.Count > 0)
                {
                    value = totalDocuments;
                    documentos = obtainedDocs;
                }
                else
                {
                    description = "No se encontró ningún Documento";
                }
            }
            catch (Exception e)
            {
                SECUDOC.writeLog(e.ToString());
                value = -1;
                description = e.Message;
            } finally
            {
                response.value = value;
                response.description = description;
                response.documentos = documentos;
            }
            return response;
        }

        private class ResponseDocumento
        {
            public DocumentResponse response { get; set; }
        }

        private class DocumentResponse
        {
            public int documentsCount { get; set; }
            public List<DocumentModel> documents { get; set; }
        }

        private class FcDResponse
        {
            public int id { get; set; }
            public int formaCobroIdAbono { get; set; }
            public double importe { get; set; }
            public double totalDocumento { get; set; }
            public double cambio { get; set; }
            public double saldoDocumento { get; set; }
            public int documentoId { get; set; }
            public int idServer { get; set; }
        }

        /*private ExpandoObject analizeResponseDocuments(ResponseDocument dr, int lastId)
        {
            dynamic response = new ExpandoObject();
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            int documentsCount = 0;
            int idDocument = 0;
            try
            {
                documentsCount = dr.response.documentsCount;
                List<DocumentFields> docsList = dr.response.documents;
                for (int j = 0; j < docsList.Count; j++)
                {
                    DocumentFields df = docsList[j];
                    lastId = df.idServer;
                    String folio = df.folioVenta;
                    bool exists = false;
                    String query = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " + LocalDatabase.CAMPO_IDWEBSERVICE_DOC +
                            " = " + lastId + " OR "+LocalDatabase.CAMPO_FVENTA_DOC+" = '"+ folio + "'";
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
                    if (!exists)
                    {
                        try
                        {
                            String queryInsert = "INSERT INTO " + LocalDatabase.TABLA_DOCUMENTOVENTA + " VALUES(@" + LocalDatabase.CAMPO_ID_DOC + ", @" + LocalDatabase.CAMPO_CLAVECLIENTE_DOC + ", " +
                            "@" + LocalDatabase.CAMPO_CLIENTEID_DOC + ", @" + LocalDatabase.CAMPO_DESCUENTO_DOC + ", @" + LocalDatabase.CAMPO_TOTAL_DOC +
                            ", @" + LocalDatabase.CAMPO_NOMBREU_DOC + ", @" + LocalDatabase.CAMPO_ALMACENID_DOC + ", " + " @" + LocalDatabase.CAMPO_ANTICIPO_DOC + ", " +
                            "@" + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + ", @" + LocalDatabase.CAMPO_FORMACOBROID_DOC + ", @" + LocalDatabase.CAMPO_FACTURA_DOC + ", " +
                            "@" + LocalDatabase.CAMPO_OBSERVACION_DOC + ", @" + LocalDatabase.CAMPO_DEV_DOC + ", @" + LocalDatabase.CAMPO_FVENTA_DOC + ", " +
                            "@" + LocalDatabase.CAMPO_FECHAHORAMOV_DOC + ", " +
                            "@" + LocalDatabase.CAMPO_USUARIOID_DOC + ", @" + LocalDatabase.CAMPO_FORMACOBROIDABONO_DOC + ", @" + LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_DOC + ", " +
                            "@" + LocalDatabase.CAMPO_CANCELADO_DOC + ", @" + LocalDatabase.CAMPO_ENVIADOALWS_DOC + ", @" + LocalDatabase.CAMPO_IDWEBSERVICE_DOC + ", " +
                            "@" + LocalDatabase.CAMPO_ARCHIVADO_DOC + ", @" + LocalDatabase.CAMPO_PAUSAR_DOC + ", @" + LocalDatabase.CAMPO_PAPELERARECICLAJE_DOC + "); " +
                            "SELECT last_insert_rowid();";
                            using (SQLiteCommand commandDocs = new SQLiteCommand(queryInsert, db))
                            {
                                if (DocumentModel.verifyIfADocumentExists(idDocument))
                                {
                                    idDocument = DocumentModel.getLastId() + 1;
                                }
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ID_DOC, idDocument);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CLAVECLIENTE_DOC, df.claveCliente);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CLIENTEID_DOC, df.clienteId);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_DESCUENTO_DOC, df.descuento);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_TOTAL_DOC, df.total);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_NOMBREU_DOC, df.nombreUsuario);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ALMACENID_DOC, df.almacenId);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ANTICIPO_DOC, df.anticipo);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC, df.tipoDocumento);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FORMACOBROID_DOC, df.formaCobroId);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FACTURA_DOC, df.factura);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_OBSERVACION_DOC, df.observacion);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_DEV_DOC, df.dev);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FVENTA_DOC, df.folioVenta);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FECHAHORAMOV_DOC, df.fechaHora);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_USUARIOID_DOC, df.usuarioId);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FORMACOBROIDABONO_DOC, df.formaCobroIdAbono);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_DOC, df.pedidoId);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CANCELADO_DOC, 0);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ENVIADOALWS_DOC, df.enviadoServer);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_IDWEBSERVICE_DOC, lastId);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ARCHIVADO_DOC, 0);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_PAUSAR_DOC, 0);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_PAPELERARECICLAJE_DOC, 0);
                                int idApp = commandDocs.ExecuteNonQuery();
                                if (idApp > 0)
                                {
                                    List<FcDResponse> fcdList = df.formasCobroDocumento;
                                    if (fcdList != null && fcdList.Count > 0)
                                    {
                                        for (int k = 0; k < fcdList.Count; k++)
                                        {
                                            FcDResponse fcd = fcdList[k];
                                            int id = ClsFormasDeCobroDocumentoModel.getTheLastId() + 1;
                                            String queryFcd = "INSERT INTO " + LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO + " (" + LocalDatabase.CAMPO_ID_FORMACOBRODOC + ", " +
                                                LocalDatabase.CAMPO_FORMACOBROIDABONO_FORMACOBRODOC + ", " + LocalDatabase.CAMPO_IMPORTE_FORMACOBRODOC + ", " +
                                                LocalDatabase.CAMPO_TOTALDOC_FORMACOBRODOC + ", " + LocalDatabase.CAMPO_CAMBIO_FORMACOBRODOC + ", " +
                                                LocalDatabase.CAMPO_SALDODOC_FORMACOBRODOC + ", " + LocalDatabase.CAMPO_DOCID_FORMACOBRODOC + ") " +
                                                "VALUES(" + id + ", " + fcd.formaCobroIdAbono + ", " + fcd.importe + ", " + fcd.totalDocumento + ", " + fcd.cambio + ", " +
                                                fcd.saldoDocumento + ", " + idDocument + ")";
                                            using (SQLiteCommand commandFcd = new SQLiteCommand(queryFcd, db))
                                            {
                                                int records = commandFcd.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (SQLiteException e)
                        {
                            SECUDOC.writeLog(e.ToString());
                        }
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
                response.valor = 100;
                response.descripcion = "Respuesta Incorrecta!";
                response.idDocumento = 0;
                response.idRetiro = 0;
                response.totalRecords = 0;
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
                response.valor = 100;
                response.descripcion = "Proceso Finalizado Documents";
                response.idDocumento = lastId;
                response.idRetiro = 0;
                response.totalRecords = documentsCount;
            }
            return response;
        }*/

        /*private ExpandoObject analizeResponseDocumentsLAN(List<ExpandoObject> dr, int lastId)
        {
            dynamic response = new ExpandoObject();
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            int documentsCount = 0;
            int idDocument = 0;
            try
            {
                documentsCount = dr.Count;
                for (int j = 0; j < dr.Count; j++)
                {
                    dynamic df = dr[j];
                    lastId = df.idServer;
                    String folio = df.folioVenta;
                    bool exists = false;
                    String query = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " + LocalDatabase.CAMPO_IDWEBSERVICE_DOC +
                            " = " + lastId + " OR " + LocalDatabase.CAMPO_FVENTA_DOC + " = '" + folio + "'";
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
                    if (!exists)
                    {
                        try
                        {
                            String queryInsert = "INSERT INTO " + LocalDatabase.TABLA_DOCUMENTOVENTA + " VALUES(@" + LocalDatabase.CAMPO_ID_DOC + ", @" + LocalDatabase.CAMPO_CLAVECLIENTE_DOC + ", " +
                            "@" + LocalDatabase.CAMPO_CLIENTEID_DOC + ", @" + LocalDatabase.CAMPO_DESCUENTO_DOC + ", @" + LocalDatabase.CAMPO_TOTAL_DOC +
                            ", @" + LocalDatabase.CAMPO_NOMBREU_DOC + ", @" + LocalDatabase.CAMPO_ALMACENID_DOC + ", " + " @" + LocalDatabase.CAMPO_ANTICIPO_DOC + ", " +
                            "@" + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + ", @" + LocalDatabase.CAMPO_FORMACOBROID_DOC + ", @" + LocalDatabase.CAMPO_FACTURA_DOC + ", " +
                            "@" + LocalDatabase.CAMPO_OBSERVACION_DOC + ", @" + LocalDatabase.CAMPO_DEV_DOC + ", @" + LocalDatabase.CAMPO_FVENTA_DOC + ", " +
                            "@" + LocalDatabase.CAMPO_FECHAHORAMOV_DOC + ", " +
                            "@" + LocalDatabase.CAMPO_USUARIOID_DOC + ", @" + LocalDatabase.CAMPO_FORMACOBROIDABONO_DOC + ", @" + LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_DOC + ", " +
                            "@" + LocalDatabase.CAMPO_CANCELADO_DOC + ", @" + LocalDatabase.CAMPO_ENVIADOALWS_DOC + ", @" + LocalDatabase.CAMPO_IDWEBSERVICE_DOC + ", " +
                            "@" + LocalDatabase.CAMPO_ARCHIVADO_DOC + ", @" + LocalDatabase.CAMPO_PAUSAR_DOC + ", @" + LocalDatabase.CAMPO_PAPELERARECICLAJE_DOC + "); " +
                            "SELECT last_insert_rowid();";
                            using (SQLiteCommand commandDocs = new SQLiteCommand(queryInsert, db))
                            {
                                if (DocumentModel.verifyIfADocumentExists(idDocument))
                                {
                                    idDocument = DocumentModel.getLastId() + 1;
                                }
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ID_DOC, idDocument);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CLAVECLIENTE_DOC, df.claveCliente);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CLIENTEID_DOC, df.clienteId);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_DESCUENTO_DOC, df.descuento);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_TOTAL_DOC, df.total);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_NOMBREU_DOC, df.nombreUsuario);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ALMACENID_DOC, df.almacenId);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ANTICIPO_DOC, df.anticipo);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC, df.tipoDocumento);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FORMACOBROID_DOC, df.formaCobroId);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FACTURA_DOC, df.factura);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_OBSERVACION_DOC, df.observacion);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_DEV_DOC, df.dev);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FVENTA_DOC, df.folioVenta);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FECHAHORAMOV_DOC, df.fechaHora);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_USUARIOID_DOC, df.usuarioId);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FORMACOBROIDABONO_DOC, df.formaCobroIdAbono);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_DOC, df.pedidoId);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CANCELADO_DOC, 0);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ENVIADOALWS_DOC, df.enviadoServer);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_IDWEBSERVICE_DOC, lastId);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ARCHIVADO_DOC, 0);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_PAUSAR_DOC, 0);
                                commandDocs.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_PAPELERARECICLAJE_DOC, 0);
                                int idApp = commandDocs.ExecuteNonQuery();
                                if (idApp > 0)
                                {
                                    List<ExpandoObject> fcdList = df.formasCobroDocumento;
                                    if (fcdList != null && fcdList.Count > 0)
                                    {
                                        for (int k = 0; k < fcdList.Count; k++)
                                        {
                                            dynamic fcd = fcdList[k];
                                            int id = ClsFormasDeCobroDocumentoModel.getTheLastId() + 1;
                                            String queryFcd = "INSERT INTO " + LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO + " (" + LocalDatabase.CAMPO_ID_FORMACOBRODOC + ", " +
                                                LocalDatabase.CAMPO_FORMACOBROIDABONO_FORMACOBRODOC + ", " + LocalDatabase.CAMPO_IMPORTE_FORMACOBRODOC + ", " +
                                                LocalDatabase.CAMPO_TOTALDOC_FORMACOBRODOC + ", " + LocalDatabase.CAMPO_CAMBIO_FORMACOBRODOC + ", " +
                                                LocalDatabase.CAMPO_SALDODOC_FORMACOBRODOC + ", " + LocalDatabase.CAMPO_DOCID_FORMACOBRODOC + ") " +
                                                "VALUES(" + id + ", " + fcd.formaCobroIdAbono + ", " + fcd.importe + ", " + fcd.totalDocumento + ", " + fcd.cambio + ", " +
                                                fcd.saldoDocumento + ", " + idDocument + ")";
                                            using (SQLiteCommand commandFcd = new SQLiteCommand(queryFcd, db))
                                            {
                                                int records = commandFcd.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (SQLiteException e)
                        {
                            SECUDOC.writeLog(e.ToString());
                        }
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
                response.valor = 100;
                response.descripcion = "Respuesta Incorrecta!";
                response.idDocumento = 0;
                response.idRetiro = 0;
                response.totalRecords = 0;
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
                response.valor = 100;
                response.descripcion = "Proceso Finalizado Documents";
                response.idDocumento = lastId;
                response.idRetiro = 0;
                response.totalRecords = documentsCount;
            }
            return response;
        }*/

        private ExpandoObject handleActionDownloadWithdrawals(int userId, String startDate, String endDate, int lastId, int limit, int totalRetiros)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            List<ClsRetirosModel> retiros = null;
            try
            {
                int itemsToEvaluate = 0;
                String url = ConfiguracionModel.getLinkWs();
                url = url.Replace(" ", "%20");
                var client = new RestClient(url);
                // client.Authenticator = new HttpBasicAuthenticator(username, password);
                var request = new RestRequest("/getAllWithdrawalsWithTheirAmountsByDateAndUser", Method.Post);
                request.AddJsonBody(new
                {
                    lastId = lastId,
                    idUsuario = userId,
                    fechaInicio = startDate,
                    fechaFin = endDate,
                    limit = limit
                });
                var responseHeader = client.ExecuteAsync(request);
                if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                {
                    var content = responseHeader.Result.Content; // Raw content as string
                    var jsonResp = JsonConvert.DeserializeObject<List<ClsRetirosModel>>(content);
                    List<ClsRetirosModel> respJson = (List<ClsRetirosModel>)jsonResp;
                    if (respJson != null && respJson.Count > 0)
                    {
                        value = totalRetiros;
                        retiros = respJson;
                    } else
                    {
                        description = "No se encontró ningún retiro";
                    }
                }
                else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                {
                    if (responseHeader.Result.ErrorMessage.Equals("Unable to connect to the remote server"))
                    {
                        value = -404;
                        description = "No pudimos conectarnos al servidor";
                    }
                    else
                    {
                        value = -500;
                        description = "Algo falló al negociar con el servidor";
                    }
                }
            }
            catch (Exception e)
            {
                SECUDOC.writeLog(e.ToString());
                value = -1;
                description = e.Message;
            }
            finally
            {
                response.value = value;
                response.description = description;
                response.retiros = retiros;
            }
            return response;
        }

        private ExpandoObject handleGetTotalWithdrawals(int userId, string startDate, string endDate)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            try
            {
                int itemsToEvaluate = 0;
                String url = ConfiguracionModel.getLinkWs();
                url = url.Replace(" ", "%20");
                var client = new RestClient(url);
                // client.Authenticator = new HttpBasicAuthenticator(username, password);
                var request = new RestRequest("/getTotalWithdrawalsWithTheirAmountsByDateAndUser", Method.Post);
                request.AddJsonBody(new
                {
                    idUsuario = userId,
                    fechaInicio = startDate,
                    fechaFin = endDate
                });
                var responseHeader = client.ExecuteAsync(request);
                if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                {
                    var content = responseHeader.Result.Content; // Raw content as string
                    dynamic respServer = new ExpandoObject();
                    var jsonResp = JsonConvert.DeserializeObject<ExpandoObject>(content);
                    dynamic respJson = (ExpandoObject)jsonResp;
                    if (respJson != null && respJson.total > 0)
                    {
                        value = Convert.ToInt32(respJson.total);
                    } else
                    {
                        description = "No se encontró ningún Retiro";
                    }
                }
                else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                {
                    if (responseHeader.Result.ErrorMessage.Equals("Unable to connect to the remote server"))
                    {
                        value = -404;
                        description = "No se pudo establecer conexión con el servidor";
                    }
                    else
                    {
                        value = -500;
                        description = "Algo falló al intentar negociar con el servidor";
                    }
                }
            }
            catch (Exception e)
            {
                SECUDOC.writeLog(e.ToString());
                value = -1;
                description = e.Message;
            }
            finally
            {
                response.value = value;
                response.description = description;
            }
            return response;
        }

        private ExpandoObject handleGetTotalWithdrawalsLAN(int userId, string startDate, string endDate)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            try
            {
                String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                value = ClsRetirosModel.getTotalWithdrawalsByDateAndUser(panelInstance,
                    userId, startDate, endDate);
                if (value == 0)
                {
                    description = "No se encontró ningún Retiro por descargar";
                }
            }
            catch (Exception e)
            {
                SECUDOC.writeLog(e.ToString());
                value = -1;
                description = e.Message;
            }
            finally
            {
                response.value = value;
                response.description = description;
            }
            return response;
        }

        private ExpandoObject handleActionDownloadWithdrawalsLAN(int userId, String startDate, String endDate, int lastId, int limit, int totalRetiros)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            List<ClsRetirosModel> retiros = null;
            try
            {
                String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                List<ClsRetirosModel> obtainedWithdrawals = ClsRetirosModel.getAllWithdrawalsWithTheirAmountsByDateAndUser(panelInstance,
                        lastId, userId, startDate, endDate, limit);
                if (obtainedWithdrawals != null && obtainedWithdrawals.Count > 0)
                {
                    value = totalRetiros;
                    retiros = obtainedWithdrawals;
                    //response = analizeResponseWithdrawalsLAN(obtainedWithdrawals, lastId);
                } else
                {
                    description = "No se encontraron Retiros por descargar";
                }
            }
            catch (Exception e)
            {
                SECUDOC.writeLog(e.ToString());
                value = -1;
                description = e.Message;
            }
            finally
            {
                response.value = value;
                response.description = description;
                response.retiros = retiros;
            }
            return response;
        }

        private class ResponseWithdrawal
        {
            public WithdrawalResponse response { get; set; }
        }

        private class WithdrawalResponse
        {
            public int withdrawalsCount { get; set; }
            public List<RetiroModel> withdrawals { get; set; }
        }

        /*private List<RetiroModel> analizeResponseWithdrawals(ResponseWithdrawal rw)
        {
             = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                retiros = rw.response.withdrawals;
                /*for (int i = 0; i < jsonArrayWithdrawals.Count; i++)
                {
                    WithdrawalFields objectWithdrawal = jsonArrayWithdrawals[i];
                    lastIdRetiro = objectWithdrawal.idServer;
                    bool exist = false;
                    try
                    {
                        String query = "SELECT * FROM " + LocalDatabase.TABLA_RETIROS + " WHERE " + LocalDatabase.CAMPO_IDSERVER_RETIRO +
                            " = " + lastIdRetiro;
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            using (SQLiteDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                    exist = true;
                                if (reader != null && !reader.IsClosed)
                                    reader.Close();
                            }
                        }
                    }
                    catch (SQLiteException e)
                    {
                        SECUDOC.writeLog(e.ToString());
                    }/*
                    /*if (!exist)
                    {
                        try
                        {
                            String queryWithdrawal = "INSERT INTO " + LocalDatabase.TABLA_RETIROS + " (" + LocalDatabase.CAMPO_ID_RETIRO + ", " + LocalDatabase.CAMPO_NUMERO_RETIRO + ", " +
                            LocalDatabase.CAMPO_IDUSUARIO_RETIRO + ", " + LocalDatabase.CAMPO_CLAVEUSUARIO_RETIRO + ", " +
                            LocalDatabase.CAMPO_FECHAHORA_RETIRO + ", " + LocalDatabase.CAMPO_ENVIADO_RETIRO + ", " +
                            LocalDatabase.CAMPO_IDSERVER_RETIRO + ", " + LocalDatabase.CAMPO_DOWNLOADED_RETIRO + ", "+LocalDatabase.CAMPO_CONCEPT_RETIRO+", " +
                            LocalDatabase.CAMPO_DESCRIPTION_RETIRO+") VALUES(@id, @number, @userId, @userCode, @date, " +
                            "@enviado, @idServer, @downloaded, '"+ objectWithdrawal.concept + "', '"+ objectWithdrawal.description + "')";
                            using (SQLiteCommand commandWithdrawal = new SQLiteCommand(queryWithdrawal, db))
                            {
                                int lastID = RetiroModel.getLastId() + 1;
                                commandWithdrawal.Parameters.AddWithValue("@id", lastID);
                                commandWithdrawal.Parameters.AddWithValue("@number", objectWithdrawal.numero);
                                commandWithdrawal.Parameters.AddWithValue("@userId", objectWithdrawal.idUsuario);
                                commandWithdrawal.Parameters.AddWithValue("@userCode", objectWithdrawal.claveUsuario);
                                commandWithdrawal.Parameters.AddWithValue("@date", objectWithdrawal.fechaHora);
                                commandWithdrawal.Parameters.AddWithValue("@enviado", 1);
                                commandWithdrawal.Parameters.AddWithValue("@idServer", lastIdRetiro);
                                commandWithdrawal.Parameters.AddWithValue("@downloaded", 1);
                                int records = commandWithdrawal.ExecuteNonQuery();
                                if (records > 0)
                                {
                                    List<AmountsWithdrawalsFields> jsonArrayAmounts = objectWithdrawal.montos;
                                    for (int j = 0; j < jsonArrayAmounts.Count; j++)
                                    {
                                        AmountsWithdrawalsFields objectAmount = jsonArrayAmounts[j];
                                        try
                                        {
                                            String queryMontos = "INSERT INTO " + LocalDatabase.TABLA_MONTORETIROS + " (" + LocalDatabase.CAMPO_ID_MONTORETIROS + ", " + LocalDatabase.CAMPO_FORMACOBROID_MONTORETIROS + ", " +
                                            LocalDatabase.CAMPO_IMPORTE_MONTORETIROS + ", " + LocalDatabase.CAMPO_RETIROID_MONTORETIRO + ", " +
                                            LocalDatabase.CAMPO_ENVIADO_MONTORETIRO + ") VALUES(@id, @fcId, @importe, @retiroId, @enviado)";
                                            using (SQLiteCommand commandMontos = new SQLiteCommand(queryMontos, db))
                                            {
                                                int lastIdMonto = MontoRetiroModel.getLastId() + 1;
                                                commandMontos.Parameters.AddWithValue("@id", lastIdMonto);
                                                commandMontos.Parameters.AddWithValue("@fcId", objectAmount.formaCobroIdAbono);
                                                commandMontos.Parameters.AddWithValue("@importe", objectAmount.importe);
                                                commandMontos.Parameters.AddWithValue("@retiroId", lastID);
                                                commandMontos.Parameters.AddWithValue("@enviado", 1);
                                                int recordsMontos = commandMontos.ExecuteNonQuery();
                                            }
                                        }
                                        catch (SQLiteException e)
                                        {
                                            SECUDOC.writeLog(e.ToString());
                                        }
                                    }
                                }
                            }
                        }
                        catch (SQLiteException e)
                        {
                            SECUDOC.writeLog(e.ToString());
                            response.valor = 100;
                            response.descripcion = "Respuesta Incorrecta! "+e.Message;
                            response.idDocumento = 0;
                            response.idRetiro = 0;
                            response.totalRecords = 0;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return retiros;
        }*/

        /*private ExpandoObject analizeResponseWithdrawalsLAN(List<ExpandoObject> rw, int lastId)
        {
            dynamic response = new ExpandoObject();
            var db = new SQLiteConnection();
            int withdrawalsCount = 0;
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                withdrawalsCount = rw.Count;
                for (int i = 0; i < rw.Count; i++)
                {
                    dynamic objectWithdrawal = rw[i];
                    lastId = objectWithdrawal.idServer;
                    bool exist = false;
                    try
                    {
                        String query = "SELECT * FROM " + LocalDatabase.TABLA_RETIROS + " WHERE " + LocalDatabase.CAMPO_IDSERVER_RETIRO +
                            " = " + lastId;
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            using (SQLiteDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                    exist = true;
                                if (reader != null && !reader.IsClosed)
                                    reader.Close();
                            }
                        }
                    }
                    catch (SQLiteException e)
                    {
                        SECUDOC.writeLog(e.ToString());
                    }
                    if (!exist)
                    {
                        try
                        {
                            String queryWithdrawal = "INSERT INTO " + LocalDatabase.TABLA_RETIROS + " (" + LocalDatabase.CAMPO_ID_RETIRO + ", " + LocalDatabase.CAMPO_NUMERO_RETIRO + ", " +
                            LocalDatabase.CAMPO_IDUSUARIO_RETIRO + ", " + LocalDatabase.CAMPO_CLAVEUSUARIO_RETIRO + ", " +
                            LocalDatabase.CAMPO_FECHAHORA_RETIRO + ", " + LocalDatabase.CAMPO_ENVIADO_RETIRO + ", " +
                            LocalDatabase.CAMPO_IDSERVER_RETIRO + ", " + LocalDatabase.CAMPO_DOWNLOADED_RETIRO + ", " + LocalDatabase.CAMPO_CONCEPT_RETIRO + ", " +
                            LocalDatabase.CAMPO_DESCRIPTION_RETIRO + ") VALUES(@id, @number, @userId, @userCode, @date, " +
                            "@enviado, @idServer, @downloaded, '" + objectWithdrawal.concept + "', '" + objectWithdrawal.description + "')";
                            using (SQLiteCommand commandWithdrawal = new SQLiteCommand(queryWithdrawal, db))
                            {
                                int lastID = RetiroModel.getLastId() + 1;
                                commandWithdrawal.Parameters.AddWithValue("@id", lastID);
                                commandWithdrawal.Parameters.AddWithValue("@number", objectWithdrawal.numero);
                                commandWithdrawal.Parameters.AddWithValue("@userId", objectWithdrawal.idUsuario);
                                commandWithdrawal.Parameters.AddWithValue("@userCode", objectWithdrawal.claveUsuario);
                                commandWithdrawal.Parameters.AddWithValue("@date", objectWithdrawal.fechaHora);
                                commandWithdrawal.Parameters.AddWithValue("@enviado", 1);
                                commandWithdrawal.Parameters.AddWithValue("@idServer", lastId);
                                commandWithdrawal.Parameters.AddWithValue("@downloaded", 1);
                                int records = commandWithdrawal.ExecuteNonQuery();
                                if (records > 0)
                                {
                                    List<ExpandoObject> jsonArrayAmounts = objectWithdrawal.montos;
                                    if (jsonArrayAmounts != null && jsonArrayAmounts.Count > 0)
                                    {
                                        for (int j = 0; j < jsonArrayAmounts.Count; j++)
                                        {
                                            dynamic objectAmount = jsonArrayAmounts[j];
                                            try
                                            {
                                                String queryMontos = "INSERT INTO " + LocalDatabase.TABLA_MONTORETIROS + " (" + LocalDatabase.CAMPO_ID_MONTORETIROS + ", " + LocalDatabase.CAMPO_FORMACOBROID_MONTORETIROS + ", " +
                                                LocalDatabase.CAMPO_IMPORTE_MONTORETIROS + ", " + LocalDatabase.CAMPO_RETIROID_MONTORETIRO + ", " +
                                                LocalDatabase.CAMPO_ENVIADO_MONTORETIRO + ") VALUES(@id, @fcId, @importe, @retiroId, @enviado)";
                                                using (SQLiteCommand commandMontos = new SQLiteCommand(queryMontos, db))
                                                {
                                                    int lastIdMonto = MontoRetiroModel.getLastId() + 1;
                                                    commandMontos.Parameters.AddWithValue("@id", lastIdMonto);
                                                    commandMontos.Parameters.AddWithValue("@fcId", objectAmount.formaCobroIdAbono);
                                                    commandMontos.Parameters.AddWithValue("@importe", objectAmount.importe);
                                                    commandMontos.Parameters.AddWithValue("@retiroId", lastID);
                                                    commandMontos.Parameters.AddWithValue("@enviado", 1);
                                                    int recordsMontos = commandMontos.ExecuteNonQuery();
                                                }
                                            }
                                            catch (SQLiteException e)
                                            {
                                                SECUDOC.writeLog(e.ToString());
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (SQLiteException e)
                        {
                            SECUDOC.writeLog(e.ToString());
                            response.valor = 100;
                            response.descripcion = "Respuesta Incorrecta!";
                            response.idDocumento = 0;
                            response.idRetiro = 0;
                            response.totalRecords = 0;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SECUDOC.writeLog(e.ToString());
                response.valor = 100;
                response.descripcion = "Respuesta Incorrecta!";
                response.idDocumento = 0;
                response.idRetiro = 0;
                response.totalRecords = 0;
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
                response.valor = 100;
                response.descripcion = "Proceso Finalizado Withdrawals";
                response.idDocumento = lastId;
                response.idRetiro = 0;
                response.totalRecords = withdrawalsCount;
            }
            return response;
        }*/

    }
}
