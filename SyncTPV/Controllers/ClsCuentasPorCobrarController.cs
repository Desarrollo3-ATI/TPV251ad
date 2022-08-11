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
using wsROMClases.Controllers;

namespace SyncTPV.Controllers
{

    public class ClsCuentasPorCobrarController
    {

        public static async Task<ExpandoObject> downloadCreditsAPI()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    int lastId = 0;
                    int itemsToEvaluate = 0;
                    String ruta = "";
                    if (LicenseModel.isItTPVLicense())
                    {
                        ruta = UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn());
                    }
                    else
                    {
                        String query = "SELECT " + LocalDatabase.CAMPO_RUTA_USER + " FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " +
                        LocalDatabase.CAMPO_ID_USUARIO + " = " + ClsRegeditController.getIdUserInTurn();
                        ruta = UserModel.getAStringValueForAnyUser(query);
                    }
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/getAllCreditsDocuments", Method.Post);
                    request.AddJsonBody(new
                    {
                        idUser = ClsRegeditController.getIdUserInTurn(),
                        routeOrBoxCode = ruta,
                        lastId = lastId
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        ResponseCredits responseCredits = JsonConvert.DeserializeObject<ResponseCredits>(content);
                        if (responseCredits.value == 1)
                        {
                            if (lastId == 0)
                                MovementsCxcModel.deleteAllMovementsCxc();
                            value = saveAllCuentasPorCobrarWithMovements(responseCredits.documentsList, 3, lastId);
                            value = 1;
                        } else
                        {
                            value = responseCredits.value;
                            description = responseCredits.description;
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        value = -400;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, responseHeader.Result.ErrorException.Message);
                    } else if (responseHeader.Result.ResponseStatus == ResponseStatus.TimedOut)
                    {
                        value = -404;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, responseHeader.Result.ErrorException.Message);
                    } else
                    {
                        value = -500;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, responseHeader.Result.ErrorException.Message);
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
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> downloadCreditsLAN()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    int lastId = 0;
                    String comInstance = InstanceSQLSEModel.getStringComInstance();
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    String boxCode = UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn());
                    dynamic responseConceptos = ClsConceptosController.getConceptCodeForCalculatePrices(panelInstance,
                    boxCode);
                    if (responseConceptos.value == 1)
                    {
                        dynamic responseCredits = ClsDocumentoModel.getAllCreditDocumentsAssignedToAnAgent(comInstance,
                        panelInstance, responseConceptos.concepto, 0, ClsRegeditController.getIdUserInTurn(), true, 0);
                        if (responseCredits.value == 1)
                        {
                            if (lastId == 0)
                                MovementsCxcModel.deleteAllMovementsCxc();
                            value = saveAllCuentasPorCobrarWithMovementsLAN(responseCredits.documentsList, 3, lastId);
                        } else
                        {
                            value = responseCredits.value;
                            description = responseCredits.description;
                        }
                    } else
                    {
                        value = responseConceptos.value;
                        description = responseConceptos.description;
                    }
                } catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    value = -1;
                    description = e.Message;
                } finally
                {
                    response.value = value;
                    response.description = description;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> downloadCreditForACustomerAPI(int idCustomer)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    int lastId = 0;
                    int itemsToEvaluate = 0;
                    String routeCode = UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn());
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/getAllCreditDocumentForACustomer", Method.Post);
                    request.AddJsonBody(new
                    {
                        routeCode = routeCode,
                        idUser = ClsRegeditController.getIdUserInTurn(),
                        idCustomer = idCustomer,
                        lastId = lastId,
                        limit = 500
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content; // Raw content as string
                        ResponseCredits responseCredits = JsonConvert.DeserializeObject<ResponseCredits>(content);
                        if (responseCredits.value == 1)
                        {
                            CuentasXCobrarModel.deleteAllCuentasPorCobrarByCustomer(idCustomer);
                            for (int i = 0; i < responseCredits.documentsList.Count; i++)
                            {
                                MovementsCxcModel.deleteAllMovementsCxcByDocument(responseCredits.documentsList[i].id);
                            }
                            value = saveAllCuentasPorCobrarWithMovementsForACustomer(responseCredits.documentsList, 3);
                        } else
                        {
                            CuentasXCobrarModel.deleteAllCuentasPorCobrarByCustomer(idCustomer);
                            value = responseCredits.value;
                            description = responseCredits.description;
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        value = -400;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value,
                            responseHeader.Result.ErrorException.Message);
                    } else if (responseHeader.Result.ResponseStatus == ResponseStatus.TimedOut)
                    {
                        value = -404;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value,
                            responseHeader.Result.ErrorException.Message);
                    } else
                    {
                        value = -500;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value,
                            responseHeader.Result.ErrorException.Message);
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
            });
            return response;
        }

        public static async Task<ExpandoObject> downloadCreditForACustomerLAN(int idCustomer)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    String comInstance = InstanceSQLSEModel.getStringComInstance();
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    String boxCode = UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn());
                    dynamic responseConceptos = ClsConceptosController.getConceptCodeForCalculatePrices(panelInstance,
                    boxCode);
                    if (responseConceptos.value == 1)
                    {
                        dynamic responseCredits = ClsDocumentoModel.getAllCreditDocumentsForACustomer(comInstance,
                            responseConceptos.concepto, 0, idCustomer, 500);
                        if (responseCredits.value == 1)
                        {
                            CuentasXCobrarModel.deleteAllCuentasPorCobrarByCustomer(idCustomer);
                            for (int i = 0; i < responseCredits.documentsList.Count; i++)
                            {
                                dynamic credito = responseCredits.documentsList[i];
                                MovementsCxcModel.deleteAllMovementsCxcByDocument(credito.id);
                            }
                            value = saveAllCuentasPorCobrarWithMovementsForACustomerLAN(responseCredits.documentsList, 3);
                        }
                        else
                        {
                            CuentasXCobrarModel.deleteAllCuentasPorCobrarByCustomer(idCustomer);
                            value = responseConceptos.value;
                            description = responseConceptos.description;
                        }
                    } else
                    {
                        value = responseConceptos.value;
                        description = responseConceptos.description;
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
            });
            return response;
        }

        public class ResponseCredits
        {
            public int value { get; set; }
            public String description { get; set; }
            public List<CreditResponse> documentsList { get; set; }
        }

        public class CreditResponse
        {
            public int id { get; set; }
            public int customerId { get; set; }
            public String customerName { get; set; }
            public String creditDocumentFolio { get; set; }
            public int diasDeAtraso { get; set; }
            public double saldoActual { get; set; }
            public String fechaDeVencimiento { get; set; }
            public String observaciones { get; set; }
            public String concepto { get; set; }
            public List<MovementsResponse> movements { get; set; }
        }

        public class MovementsResponse
        {
            public int idCreditDocument { get; set; }
            public int productoId { get; set; }
            public String codigoProducto { get; set; }
            public int numeroMovimiento { get; set; }
            public double precio { get; set; }
            public double unidades { get; set; }
            public int unidadCapturadaId { get; set; }
            public double subtotal { get; set; }
            public double descuento { get; set; }
            public double total { get; set; }
        }

        public static int saveAllCuentasPorCobrarWithMovements(List<CreditResponse> documentsList, int method, int ultimoId)
        {
            int lastCallActivated = 0;
            int cantidadVAloresGuardados = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                int contador = 0;
                if (documentsList != null)
                {
                    int itemsAEvaluar = documentsList.Count;
                    ClsBanderasCargaInicialModel.updateValuesServer(db, method, itemsAEvaluar);
                    if (ultimoId == 0)
                    {
                        CuentasXCobrarModel.deleteAllCuentasPorCobrar();
                        MovementsCxcModel.deleteAllMovementsCxc();
                    }
                    for (int i = 0; i < documentsList.Count; i++)
                    {
                        CreditResponse objectCredits = documentsList[i];
                        String queryCredits = "INSERT INTO " + LocalDatabase.TABLA_CXC + " (" + LocalDatabase.CAMPO_CLIENTE_ID_CXC + ", " +
                            LocalDatabase.CAMPO_DOCTO_CC_ID_CXC + ", " + LocalDatabase.CAMPO_FOLIO_CXC + ", " + LocalDatabase.CAMPO_DIAS_ATRASO_CXC + ", " +
                            LocalDatabase.CAMPO_SALDO_ACTUAL_CXC + ", " + LocalDatabase.CAMPO_FECHA_VENCE_CXC + ", " + LocalDatabase.CAMPO_DESCRIPCION_CXC +
                            ", " + LocalDatabase.CAMPO_FACTURA_MOSTRADOR_CXC + ") VALUES(@" + LocalDatabase.CAMPO_CLIENTE_ID_CXC + ", @" +
                            LocalDatabase.CAMPO_DOCTO_CC_ID_CXC + ", @" + LocalDatabase.CAMPO_FOLIO_CXC + ", @" + LocalDatabase.CAMPO_DIAS_ATRASO_CXC + ", @" +
                            LocalDatabase.CAMPO_SALDO_ACTUAL_CXC + ", @" + LocalDatabase.CAMPO_FECHA_VENCE_CXC + ", @" + LocalDatabase.CAMPO_DESCRIPCION_CXC +
                            ", @" + LocalDatabase.CAMPO_FACTURA_MOSTRADOR_CXC + ")";
                        using (SQLiteCommand commandCredits = new SQLiteCommand(queryCredits, db))
                        {
                            commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CLIENTE_ID_CXC, objectCredits.customerId);
                            commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_DOCTO_CC_ID_CXC, objectCredits.id);
                            commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FOLIO_CXC, objectCredits.creditDocumentFolio);
                            commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_DIAS_ATRASO_CXC, objectCredits.diasDeAtraso);
                            commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_SALDO_ACTUAL_CXC, objectCredits.saldoActual);
                            commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FECHA_VENCE_CXC, objectCredits.fechaDeVencimiento);
                            commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_DESCRIPCION_CXC, objectCredits.observaciones);
                            commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FACTURA_MOSTRADOR_CXC, objectCredits.concepto);
                            int recordsCredits = commandCredits.ExecuteNonQuery();
                            if (recordsCredits > 0)
                            {
                                ultimoId = objectCredits.id;
                                List<MovementsResponse> arrayMovements = objectCredits.movements;
                                for (int j = 0; j < arrayMovements.Count; j++)
                                {
                                    MovementsResponse objectMovement = arrayMovements[j];
                                    String queryMovements = "INSERT INTO " + LocalDatabase.TABLA_MOVEMENTCXC + " (" + LocalDatabase.CAMPO_CXCID_MOVEMENTCXC + "," +
                                        " " + LocalDatabase.CAMPO_ITEMID_MOVEMENTCXC + ", " + LocalDatabase.CAMPO_ITEMCODE_MOVEMENTCXC + ", " +
                                        LocalDatabase.CAMPO_NUMBER_MOVEMENTCXC + ", " + LocalDatabase.CAMPO_PRICE_MOVEMENTCXC + ", " +
                                        LocalDatabase.CAMPO_CAPTUREDUNITS_MOVEMENTCXC + ", " + LocalDatabase.CAMPO_CAPTUREDUNITSID_MOVEMENTCXC + ", " +
                                        LocalDatabase.CAMPO_SUBTOTAL_MOVEMENTCXC + ", " + LocalDatabase.CAMPO_DESCUENTO_MOVEMENTCXC + ", " +
                                        LocalDatabase.CAMPO_TOTAL_MOVEMENTCXC + ") VALUES(@" + LocalDatabase.CAMPO_CXCID_MOVEMENTCXC + "," +
                                        " @" + LocalDatabase.CAMPO_ITEMID_MOVEMENTCXC + ", @" + LocalDatabase.CAMPO_ITEMCODE_MOVEMENTCXC + ", @" +
                                        LocalDatabase.CAMPO_NUMBER_MOVEMENTCXC + ", @" + LocalDatabase.CAMPO_PRICE_MOVEMENTCXC + ", @" +
                                        LocalDatabase.CAMPO_CAPTUREDUNITS_MOVEMENTCXC + ", @" + LocalDatabase.CAMPO_CAPTUREDUNITSID_MOVEMENTCXC + ", @" +
                                        LocalDatabase.CAMPO_SUBTOTAL_MOVEMENTCXC + ", @" + LocalDatabase.CAMPO_DESCUENTO_MOVEMENTCXC + ", @" +
                                        LocalDatabase.CAMPO_TOTAL_MOVEMENTCXC + ")";
                                    using (SQLiteCommand commandMoves = new SQLiteCommand(queryMovements, db))
                                    {
                                        commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CXCID_MOVEMENTCXC, objectMovement.idCreditDocument);
                                        commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ITEMID_MOVEMENTCXC, objectMovement.productoId);
                                        commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ITEMCODE_MOVEMENTCXC, objectMovement.codigoProducto);
                                        commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_NUMBER_MOVEMENTCXC, objectMovement.numeroMovimiento);
                                        commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_PRICE_MOVEMENTCXC, objectMovement.precio);
                                        commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CAPTUREDUNITS_MOVEMENTCXC, objectMovement.unidades);
                                        commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CAPTUREDUNITSID_MOVEMENTCXC, objectMovement.unidadCapturadaId);
                                        commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_SUBTOTAL_MOVEMENTCXC, objectMovement.subtotal);
                                        commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_DESCUENTO_MOVEMENTCXC, objectMovement.descuento);
                                        commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_TOTAL_MOVEMENTCXC, objectMovement.total);
                                        int recordsMoves = commandMoves.ExecuteNonQuery();
                                    }
                                }
                                contador++;
                            }
                        }
                        cantidadVAloresGuardados += contador;
                        ClsBanderasCargaInicialModel.updateValuesLocal(db, method, cantidadVAloresGuardados);
                        if (contador == itemsAEvaluar)
                        {
                            lastCallActivated = 1;
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
            return lastCallActivated;
        }

        public static int saveAllCuentasPorCobrarWithMovementsLAN(List<CreditResponse> documentsList, int method, int ultimoId)
        {
            int lastCallActivated = 0;
            int cantidadVAloresGuardados = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                int contador = 0;
                if (documentsList != null)
                {
                    int itemsAEvaluar = documentsList.Count;
                    ClsBanderasCargaInicialModel.updateValuesServer(db, method, itemsAEvaluar);
                    if (ultimoId == 0)
                    {
                        CuentasXCobrarModel.deleteAllCuentasPorCobrar();
                        MovementsCxcModel.deleteAllMovementsCxc();
                    }
                    for (int i = 0; i < documentsList.Count; i++)
                    {
                        dynamic objectCredits = documentsList[i];
                        String queryCredits = "INSERT INTO " + LocalDatabase.TABLA_CXC + " (" + LocalDatabase.CAMPO_CLIENTE_ID_CXC + ", " +
                            LocalDatabase.CAMPO_DOCTO_CC_ID_CXC + ", " + LocalDatabase.CAMPO_FOLIO_CXC + ", " + LocalDatabase.CAMPO_DIAS_ATRASO_CXC + ", " +
                            LocalDatabase.CAMPO_SALDO_ACTUAL_CXC + ", " + LocalDatabase.CAMPO_FECHA_VENCE_CXC + ", " + LocalDatabase.CAMPO_DESCRIPCION_CXC +
                            ", " + LocalDatabase.CAMPO_FACTURA_MOSTRADOR_CXC + ") VALUES(@" + LocalDatabase.CAMPO_CLIENTE_ID_CXC + ", @" +
                            LocalDatabase.CAMPO_DOCTO_CC_ID_CXC + ", @" + LocalDatabase.CAMPO_FOLIO_CXC + ", @" + LocalDatabase.CAMPO_DIAS_ATRASO_CXC + ", @" +
                            LocalDatabase.CAMPO_SALDO_ACTUAL_CXC + ", @" + LocalDatabase.CAMPO_FECHA_VENCE_CXC + ", @" + LocalDatabase.CAMPO_DESCRIPCION_CXC +
                            ", @" + LocalDatabase.CAMPO_FACTURA_MOSTRADOR_CXC + ")";
                        using (SQLiteCommand commandCredits = new SQLiteCommand(queryCredits, db))
                        {
                            commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CLIENTE_ID_CXC, objectCredits.clienteId);
                            commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_DOCTO_CC_ID_CXC, objectCredits.id);
                            commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FOLIO_CXC, objectCredits.folio);
                            commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_DIAS_ATRASO_CXC, objectCredits.diasDeAtraso);
                            commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_SALDO_ACTUAL_CXC, objectCredits.saldoActual);
                            commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FECHA_VENCE_CXC, objectCredits.fechaDeVencimiento);
                            commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_DESCRIPCION_CXC, objectCredits.observaciones);
                            commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FACTURA_MOSTRADOR_CXC, objectCredits.concepto);
                            int recordsCredits = commandCredits.ExecuteNonQuery();
                            if (recordsCredits > 0)
                            {
                                ultimoId = objectCredits.id;
                                List<ExpandoObject> arrayMovements = objectCredits.movements;
                                for (int j = 0; j < arrayMovements.Count; j++)
                                {
                                    dynamic objectMovement = arrayMovements[j];
                                    String queryMovements = "INSERT INTO " + LocalDatabase.TABLA_MOVEMENTCXC + " (" + LocalDatabase.CAMPO_CXCID_MOVEMENTCXC + "," +
                                        " " + LocalDatabase.CAMPO_ITEMID_MOVEMENTCXC + ", " + LocalDatabase.CAMPO_ITEMCODE_MOVEMENTCXC + ", " +
                                        LocalDatabase.CAMPO_NUMBER_MOVEMENTCXC + ", " + LocalDatabase.CAMPO_PRICE_MOVEMENTCXC + ", " +
                                        LocalDatabase.CAMPO_CAPTUREDUNITS_MOVEMENTCXC + ", " + LocalDatabase.CAMPO_CAPTUREDUNITSID_MOVEMENTCXC + ", " +
                                        LocalDatabase.CAMPO_SUBTOTAL_MOVEMENTCXC + ", " + LocalDatabase.CAMPO_DESCUENTO_MOVEMENTCXC + ", " +
                                        LocalDatabase.CAMPO_TOTAL_MOVEMENTCXC + ") VALUES(@" + LocalDatabase.CAMPO_CXCID_MOVEMENTCXC + "," +
                                        " @" + LocalDatabase.CAMPO_ITEMID_MOVEMENTCXC + ", @" + LocalDatabase.CAMPO_ITEMCODE_MOVEMENTCXC + ", @" +
                                        LocalDatabase.CAMPO_NUMBER_MOVEMENTCXC + ", @" + LocalDatabase.CAMPO_PRICE_MOVEMENTCXC + ", @" +
                                        LocalDatabase.CAMPO_CAPTUREDUNITS_MOVEMENTCXC + ", @" + LocalDatabase.CAMPO_CAPTUREDUNITSID_MOVEMENTCXC + ", @" +
                                        LocalDatabase.CAMPO_SUBTOTAL_MOVEMENTCXC + ", @" + LocalDatabase.CAMPO_DESCUENTO_MOVEMENTCXC + ", @" +
                                        LocalDatabase.CAMPO_TOTAL_MOVEMENTCXC + ")";
                                    using (SQLiteCommand commandMoves = new SQLiteCommand(queryMovements, db))
                                    {
                                        commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CXCID_MOVEMENTCXC, objectMovement.idCreditDocument);
                                        commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ITEMID_MOVEMENTCXC, objectMovement.productoId);
                                        commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ITEMCODE_MOVEMENTCXC, objectMovement.codigoProducto);
                                        commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_NUMBER_MOVEMENTCXC, objectMovement.numeroMovimiento);
                                        commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_PRICE_MOVEMENTCXC, objectMovement.precio);
                                        commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CAPTUREDUNITS_MOVEMENTCXC, objectMovement.unidades);
                                        commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CAPTUREDUNITSID_MOVEMENTCXC, objectMovement.unidadCapturadaId);
                                        commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_SUBTOTAL_MOVEMENTCXC, objectMovement.subtotal);
                                        commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_DESCUENTO_MOVEMENTCXC, objectMovement.descuento);
                                        commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_TOTAL_MOVEMENTCXC, objectMovement.total);
                                        int recordsMoves = commandMoves.ExecuteNonQuery();
                                    }
                                }
                                contador++;
                            }
                        }
                        cantidadVAloresGuardados += contador;
                        ClsBanderasCargaInicialModel.updateValuesLocal(db, method, cantidadVAloresGuardados);
                        if (contador == itemsAEvaluar)
                        {
                            lastCallActivated = 1;
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
            return lastCallActivated;
        }

        public static int saveAllCuentasPorCobrarWithMovementsForACustomer(List<CreditResponse> documentsList, int method)
        {
            int lastCallActivated = 0;
            int cantidadVAloresGuardados = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                int contador = 0;
                if (documentsList != null)
                {
                    int itemsAEvaluar = documentsList.Count;
                    ClsBanderasCargaInicialModel.updateValuesServer(db, method, itemsAEvaluar);
                    for (int i = 0; i < documentsList.Count; i++)
                    {
                        String query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_CXC + " WHERE " +
                            LocalDatabase.CAMPO_DOCTO_CC_ID_CXC + " = " + documentsList[i].id + " AND " + LocalDatabase.CAMPO_TIPO_CXC + " = 1";
                        int exist = CuentasXCobrarModel.getIntValue(query);
                        if (exist == 0)
                        {
                            CreditResponse objectCredits = documentsList[i];
                            String queryCredits = "INSERT INTO " + LocalDatabase.TABLA_CXC + " (" + LocalDatabase.CAMPO_CLIENTE_ID_CXC + ", " +
                                LocalDatabase.CAMPO_DOCTO_CC_ID_CXC + ", " + LocalDatabase.CAMPO_FOLIO_CXC + ", " + LocalDatabase.CAMPO_DIAS_ATRASO_CXC + ", " +
                                LocalDatabase.CAMPO_SALDO_ACTUAL_CXC + ", " + LocalDatabase.CAMPO_FECHA_VENCE_CXC + ", " + LocalDatabase.CAMPO_DESCRIPCION_CXC +
                                ", " + LocalDatabase.CAMPO_FACTURA_MOSTRADOR_CXC + ") VALUES(@" + LocalDatabase.CAMPO_CLIENTE_ID_CXC + ", @" +
                                LocalDatabase.CAMPO_DOCTO_CC_ID_CXC + ", @" + LocalDatabase.CAMPO_FOLIO_CXC + ", @" + LocalDatabase.CAMPO_DIAS_ATRASO_CXC + ", @" +
                                LocalDatabase.CAMPO_SALDO_ACTUAL_CXC + ", @" + LocalDatabase.CAMPO_FECHA_VENCE_CXC + ", @" + LocalDatabase.CAMPO_DESCRIPCION_CXC +
                                ", @" + LocalDatabase.CAMPO_FACTURA_MOSTRADOR_CXC + ")";
                            using (SQLiteCommand commandCredits = new SQLiteCommand(queryCredits, db))
                            {
                                commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CLIENTE_ID_CXC, objectCredits.customerId);
                                commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_DOCTO_CC_ID_CXC, objectCredits.id);
                                commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FOLIO_CXC, objectCredits.creditDocumentFolio);
                                commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_DIAS_ATRASO_CXC, objectCredits.diasDeAtraso);
                                commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_SALDO_ACTUAL_CXC, objectCredits.saldoActual);
                                commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FECHA_VENCE_CXC, objectCredits.fechaDeVencimiento);
                                commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_DESCRIPCION_CXC, objectCredits.observaciones);
                                commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FACTURA_MOSTRADOR_CXC, objectCredits.concepto);
                                int recordsCredits = commandCredits.ExecuteNonQuery();
                                if (recordsCredits > 0)
                                {
                                    List<MovementsResponse> arrayMovements = objectCredits.movements;
                                    for (int j = 0; j < arrayMovements.Count; j++)
                                    {
                                        MovementsResponse objectMovement = arrayMovements[j];
                                        String queryMovements = "INSERT INTO " + LocalDatabase.TABLA_MOVEMENTCXC + " (" + LocalDatabase.CAMPO_CXCID_MOVEMENTCXC + "," +
                                            " " + LocalDatabase.CAMPO_ITEMID_MOVEMENTCXC + ", " + LocalDatabase.CAMPO_ITEMCODE_MOVEMENTCXC + ", " +
                                            LocalDatabase.CAMPO_NUMBER_MOVEMENTCXC + ", " + LocalDatabase.CAMPO_PRICE_MOVEMENTCXC + ", " +
                                            LocalDatabase.CAMPO_CAPTUREDUNITS_MOVEMENTCXC + ", " + LocalDatabase.CAMPO_CAPTUREDUNITSID_MOVEMENTCXC + ", " +
                                            LocalDatabase.CAMPO_SUBTOTAL_MOVEMENTCXC + ", " + LocalDatabase.CAMPO_DESCUENTO_MOVEMENTCXC + ", " +
                                            LocalDatabase.CAMPO_TOTAL_MOVEMENTCXC + ") VALUES(@" + LocalDatabase.CAMPO_CXCID_MOVEMENTCXC + "," +
                                            " @" + LocalDatabase.CAMPO_ITEMID_MOVEMENTCXC + ", @" + LocalDatabase.CAMPO_ITEMCODE_MOVEMENTCXC + ", @" +
                                            LocalDatabase.CAMPO_NUMBER_MOVEMENTCXC + ", @" + LocalDatabase.CAMPO_PRICE_MOVEMENTCXC + ", @" +
                                            LocalDatabase.CAMPO_CAPTUREDUNITS_MOVEMENTCXC + ", @" + LocalDatabase.CAMPO_CAPTUREDUNITSID_MOVEMENTCXC + ", @" +
                                            LocalDatabase.CAMPO_SUBTOTAL_MOVEMENTCXC + ", @" + LocalDatabase.CAMPO_DESCUENTO_MOVEMENTCXC + ", @" +
                                            LocalDatabase.CAMPO_TOTAL_MOVEMENTCXC + ")";
                                        using (SQLiteCommand commandMoves = new SQLiteCommand(queryMovements, db))
                                        {
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CXCID_MOVEMENTCXC, objectMovement.idCreditDocument);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ITEMID_MOVEMENTCXC, objectMovement.productoId);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ITEMCODE_MOVEMENTCXC, objectMovement.codigoProducto);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_NUMBER_MOVEMENTCXC, objectMovement.numeroMovimiento);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_PRICE_MOVEMENTCXC, objectMovement.precio);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CAPTUREDUNITS_MOVEMENTCXC, objectMovement.unidades);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CAPTUREDUNITSID_MOVEMENTCXC, objectMovement.unidadCapturadaId);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_SUBTOTAL_MOVEMENTCXC, objectMovement.subtotal);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_DESCUENTO_MOVEMENTCXC, objectMovement.descuento);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_TOTAL_MOVEMENTCXC, objectMovement.total);
                                            int recordsMoves = commandMoves.ExecuteNonQuery();
                                        }
                                    }
                                    contador++;
                                }
                            }
                        } else
                        {
                            CreditResponse objectCredits = documentsList[i];
                            if (CuentasXCobrarModel.updateCxc(objectCredits))
                            {
                                for (int j = 0; j < documentsList[i].movements.Count; j++)
                                {
                                    String queryMovement = "SELECT COUNT(*) FROM "+LocalDatabase.TABLA_MOVEMENTCXC+" WHERE "+ 
                                        LocalDatabase.CAMPO_CXCID_MOVEMENTCXC + " = " + documentsList[i].movements[j].idCreditDocument + " AND " +
                                        LocalDatabase.CAMPO_ITEMID_MOVEMENTCXC + " = " + documentsList[i].movements[j].productoId + " AND " +
                                        LocalDatabase.CAMPO_NUMBER_MOVEMENTCXC + " = " + documentsList[i].movements[j].numeroMovimiento;
                                    int existMovement = MovementsCxcModel.getIntValue(queryMovement);
                                    if (existMovement == 0)
                                    {
                                        String queryMovements = "INSERT INTO " + LocalDatabase.TABLA_MOVEMENTCXC + " (" + LocalDatabase.CAMPO_CXCID_MOVEMENTCXC + "," +
                                        " " + LocalDatabase.CAMPO_ITEMID_MOVEMENTCXC + ", " + LocalDatabase.CAMPO_ITEMCODE_MOVEMENTCXC + ", " +
                                        LocalDatabase.CAMPO_NUMBER_MOVEMENTCXC + ", " + LocalDatabase.CAMPO_PRICE_MOVEMENTCXC + ", " +
                                        LocalDatabase.CAMPO_CAPTUREDUNITS_MOVEMENTCXC + ", " + LocalDatabase.CAMPO_CAPTUREDUNITSID_MOVEMENTCXC + ", " +
                                        LocalDatabase.CAMPO_SUBTOTAL_MOVEMENTCXC + ", " + LocalDatabase.CAMPO_DESCUENTO_MOVEMENTCXC + ", " +
                                        LocalDatabase.CAMPO_TOTAL_MOVEMENTCXC + ") VALUES(@" + LocalDatabase.CAMPO_CXCID_MOVEMENTCXC + "," +
                                        " @" + LocalDatabase.CAMPO_ITEMID_MOVEMENTCXC + ", @" + LocalDatabase.CAMPO_ITEMCODE_MOVEMENTCXC + ", @" +
                                        LocalDatabase.CAMPO_NUMBER_MOVEMENTCXC + ", @" + LocalDatabase.CAMPO_PRICE_MOVEMENTCXC + ", @" +
                                        LocalDatabase.CAMPO_CAPTUREDUNITS_MOVEMENTCXC + ", @" + LocalDatabase.CAMPO_CAPTUREDUNITSID_MOVEMENTCXC + ", @" +
                                        LocalDatabase.CAMPO_SUBTOTAL_MOVEMENTCXC + ", @" + LocalDatabase.CAMPO_DESCUENTO_MOVEMENTCXC + ", @" +
                                        LocalDatabase.CAMPO_TOTAL_MOVEMENTCXC + ")";
                                        using (SQLiteCommand commandMoves = new SQLiteCommand(queryMovements, db))
                                        {
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CXCID_MOVEMENTCXC, documentsList[i].movements[j].idCreditDocument);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ITEMID_MOVEMENTCXC, documentsList[i].movements[j].productoId);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ITEMCODE_MOVEMENTCXC, documentsList[i].movements[j].codigoProducto);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_NUMBER_MOVEMENTCXC, documentsList[i].movements[j].numeroMovimiento);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_PRICE_MOVEMENTCXC, documentsList[i].movements[j].precio);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CAPTUREDUNITS_MOVEMENTCXC, documentsList[i].movements[j].unidades);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CAPTUREDUNITSID_MOVEMENTCXC, documentsList[i].movements[j].unidadCapturadaId);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_SUBTOTAL_MOVEMENTCXC, documentsList[i].movements[j].subtotal);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_DESCUENTO_MOVEMENTCXC, documentsList[i].movements[j].descuento);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_TOTAL_MOVEMENTCXC, documentsList[i].movements[j].total);
                                            int recordsMoves = commandMoves.ExecuteNonQuery();
                                        }
                                    } else
                                    {
                                        String queryMovements = "UPDATE " + LocalDatabase.TABLA_MOVEMENTCXC + " SET " +
                                        LocalDatabase.CAMPO_ITEMCODE_MOVEMENTCXC + " = '"+ documentsList[i].movements[j].codigoProducto + "', " + 
                                        LocalDatabase.CAMPO_PRICE_MOVEMENTCXC + " = "+ documentsList[i].movements[j].precio+ ", " +
                                        LocalDatabase.CAMPO_CAPTUREDUNITS_MOVEMENTCXC + " = "+ documentsList[i].movements[j].unidades+ ", " + 
                                        LocalDatabase.CAMPO_CAPTUREDUNITSID_MOVEMENTCXC + " = "+ documentsList[i].movements[j].unidadCapturadaId + ", " +
                                        LocalDatabase.CAMPO_SUBTOTAL_MOVEMENTCXC + " = "+ documentsList[i].movements[j] .subtotal+", " + 
                                        LocalDatabase.CAMPO_DESCUENTO_MOVEMENTCXC + " = "+ documentsList[i].movements[j].descuento + ", " +
                                        LocalDatabase.CAMPO_TOTAL_MOVEMENTCXC + " = "+ documentsList[i].movements[j].total + " WHERE " +
                                        LocalDatabase.CAMPO_CXCID_MOVEMENTCXC + " = " + documentsList[i].movements[j].idCreditDocument + " AND " +
                                        LocalDatabase.CAMPO_ITEMID_MOVEMENTCXC + " = " + documentsList[i].movements[j].productoId + " AND " +
                                        LocalDatabase.CAMPO_NUMBER_MOVEMENTCXC + " = " + documentsList[i].movements[j].numeroMovimiento;
                                        MovementsCxcModel.createUpdateOrDeleteRecords(queryMovements);
                                    }
                                }
                                contador++;
                            }
                        }
                        cantidadVAloresGuardados = contador;
                        ClsBanderasCargaInicialModel.updateValuesLocal(db, method, cantidadVAloresGuardados);
                        if (contador == itemsAEvaluar)
                        {
                            lastCallActivated = 1;
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
            return lastCallActivated;
        }

        public static int saveAllCuentasPorCobrarWithMovementsForACustomerLAN(List<ExpandoObject> documentsList, int method)
        {
            int lastCallActivated = 0;
            int cantidadVAloresGuardados = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                int contador = 0;
                if (documentsList != null)
                {
                    int itemsAEvaluar = documentsList.Count;
                    ClsBanderasCargaInicialModel.updateValuesServer(db, method, itemsAEvaluar);
                    for (int i = 0; i < documentsList.Count; i++)
                    {
                        dynamic objectCredits = documentsList[i];
                        String query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_CXC + " WHERE " + 
                            LocalDatabase.CAMPO_DOCTO_CC_ID_CXC + " = " + objectCredits.id+" AND "+
                            LocalDatabase.CAMPO_TIPO_CXC+" = 1";
                        int exist = CuentasXCobrarModel.getIntValue(query);
                        if (exist == 0)
                        {
                            String queryCredits = "INSERT INTO " + LocalDatabase.TABLA_CXC + " (" + LocalDatabase.CAMPO_CLIENTE_ID_CXC + ", " +
                                LocalDatabase.CAMPO_DOCTO_CC_ID_CXC + ", " + LocalDatabase.CAMPO_FOLIO_CXC + ", " + LocalDatabase.CAMPO_DIAS_ATRASO_CXC + ", " +
                                LocalDatabase.CAMPO_SALDO_ACTUAL_CXC + ", " + LocalDatabase.CAMPO_FECHA_VENCE_CXC + ", " + LocalDatabase.CAMPO_DESCRIPCION_CXC +
                                ", " + LocalDatabase.CAMPO_FACTURA_MOSTRADOR_CXC + ") VALUES(@" + LocalDatabase.CAMPO_CLIENTE_ID_CXC + ", @" +
                                LocalDatabase.CAMPO_DOCTO_CC_ID_CXC + ", @" + LocalDatabase.CAMPO_FOLIO_CXC + ", @" + LocalDatabase.CAMPO_DIAS_ATRASO_CXC + ", @" +
                                LocalDatabase.CAMPO_SALDO_ACTUAL_CXC + ", @" + LocalDatabase.CAMPO_FECHA_VENCE_CXC + ", @" + LocalDatabase.CAMPO_DESCRIPCION_CXC +
                                ", @" + LocalDatabase.CAMPO_FACTURA_MOSTRADOR_CXC + ")";
                            using (SQLiteCommand commandCredits = new SQLiteCommand(queryCredits, db))
                            {
                                commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CLIENTE_ID_CXC, objectCredits.customerId);
                                commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_DOCTO_CC_ID_CXC, objectCredits.id);
                                commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FOLIO_CXC, objectCredits.creditDocumentFolio);
                                commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_DIAS_ATRASO_CXC, objectCredits.diasDeAtraso);
                                commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_SALDO_ACTUAL_CXC, objectCredits.saldoActual);
                                commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FECHA_VENCE_CXC, objectCredits.fechaDeVencimiento);
                                commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_DESCRIPCION_CXC, objectCredits.observaciones);
                                commandCredits.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_FACTURA_MOSTRADOR_CXC, objectCredits.concepto);
                                int recordsCredits = commandCredits.ExecuteNonQuery();
                                if (recordsCredits > 0)
                                {
                                    for (int j = 0; j < objectCredits.movements.Count; j++)
                                    {
                                        String queryMovements = "INSERT INTO " + LocalDatabase.TABLA_MOVEMENTCXC + " (" + LocalDatabase.CAMPO_CXCID_MOVEMENTCXC + "," +
                                            " " + LocalDatabase.CAMPO_ITEMID_MOVEMENTCXC + ", " + LocalDatabase.CAMPO_ITEMCODE_MOVEMENTCXC + ", " +
                                            LocalDatabase.CAMPO_NUMBER_MOVEMENTCXC + ", " + LocalDatabase.CAMPO_PRICE_MOVEMENTCXC + ", " +
                                            LocalDatabase.CAMPO_CAPTUREDUNITS_MOVEMENTCXC + ", " + LocalDatabase.CAMPO_CAPTUREDUNITSID_MOVEMENTCXC + ", " +
                                            LocalDatabase.CAMPO_SUBTOTAL_MOVEMENTCXC + ", " + LocalDatabase.CAMPO_DESCUENTO_MOVEMENTCXC + ", " +
                                            LocalDatabase.CAMPO_TOTAL_MOVEMENTCXC + ") VALUES(@" + LocalDatabase.CAMPO_CXCID_MOVEMENTCXC + "," +
                                            " @" + LocalDatabase.CAMPO_ITEMID_MOVEMENTCXC + ", @" + LocalDatabase.CAMPO_ITEMCODE_MOVEMENTCXC + ", @" +
                                            LocalDatabase.CAMPO_NUMBER_MOVEMENTCXC + ", @" + LocalDatabase.CAMPO_PRICE_MOVEMENTCXC + ", @" +
                                            LocalDatabase.CAMPO_CAPTUREDUNITS_MOVEMENTCXC + ", @" + LocalDatabase.CAMPO_CAPTUREDUNITSID_MOVEMENTCXC + ", @" +
                                            LocalDatabase.CAMPO_SUBTOTAL_MOVEMENTCXC + ", @" + LocalDatabase.CAMPO_DESCUENTO_MOVEMENTCXC + ", @" +
                                            LocalDatabase.CAMPO_TOTAL_MOVEMENTCXC + ")";
                                        using (SQLiteCommand commandMoves = new SQLiteCommand(queryMovements, db))
                                        {
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CXCID_MOVEMENTCXC, objectCredits.movements[j].idCreditDocument);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ITEMID_MOVEMENTCXC, objectCredits.movements[j].productoId);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ITEMCODE_MOVEMENTCXC, objectCredits.movements[j].codigoProducto);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_NUMBER_MOVEMENTCXC, objectCredits.movements[j].numeroMovimiento);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_PRICE_MOVEMENTCXC, objectCredits.movements[j].precio);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CAPTUREDUNITS_MOVEMENTCXC, objectCredits.movements[j].unidades);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CAPTUREDUNITSID_MOVEMENTCXC, objectCredits.movements[j].unidadCapturadaId);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_SUBTOTAL_MOVEMENTCXC, objectCredits.movements[j].subtotal);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_DESCUENTO_MOVEMENTCXC, objectCredits.movements[j].descuento);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_TOTAL_MOVEMENTCXC, objectCredits.movements[j].total);
                                            int recordsMoves = commandMoves.ExecuteNonQuery();
                                        }
                                    }
                                    contador++;
                                }
                            }
                        }
                        else
                        {
                            if (CuentasXCobrarModel.updateCxc(objectCredits))
                            {
                                for (int j = 0; j < objectCredits.movements.Count; j++)
                                {
                                    String queryMovement = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_MOVEMENTCXC + " WHERE " +
                                        LocalDatabase.CAMPO_CXCID_MOVEMENTCXC + " = " + objectCredits.movements[j].idCreditDocument + " AND " +
                                        LocalDatabase.CAMPO_ITEMID_MOVEMENTCXC + " = " + objectCredits.movements[j].productoId + " AND " +
                                        LocalDatabase.CAMPO_NUMBER_MOVEMENTCXC + " = " + objectCredits.movements[j].numeroMovimiento;
                                    int existMovement = MovementsCxcModel.getIntValue(queryMovement);
                                    if (existMovement == 0)
                                    {
                                        String queryMovements = "INSERT INTO " + LocalDatabase.TABLA_MOVEMENTCXC + " (" + LocalDatabase.CAMPO_CXCID_MOVEMENTCXC + "," +
                                        " " + LocalDatabase.CAMPO_ITEMID_MOVEMENTCXC + ", " + LocalDatabase.CAMPO_ITEMCODE_MOVEMENTCXC + ", " +
                                        LocalDatabase.CAMPO_NUMBER_MOVEMENTCXC + ", " + LocalDatabase.CAMPO_PRICE_MOVEMENTCXC + ", " +
                                        LocalDatabase.CAMPO_CAPTUREDUNITS_MOVEMENTCXC + ", " + LocalDatabase.CAMPO_CAPTUREDUNITSID_MOVEMENTCXC + ", " +
                                        LocalDatabase.CAMPO_SUBTOTAL_MOVEMENTCXC + ", " + LocalDatabase.CAMPO_DESCUENTO_MOVEMENTCXC + ", " +
                                        LocalDatabase.CAMPO_TOTAL_MOVEMENTCXC + ") VALUES(@" + LocalDatabase.CAMPO_CXCID_MOVEMENTCXC + "," +
                                        " @" + LocalDatabase.CAMPO_ITEMID_MOVEMENTCXC + ", @" + LocalDatabase.CAMPO_ITEMCODE_MOVEMENTCXC + ", @" +
                                        LocalDatabase.CAMPO_NUMBER_MOVEMENTCXC + ", @" + LocalDatabase.CAMPO_PRICE_MOVEMENTCXC + ", @" +
                                        LocalDatabase.CAMPO_CAPTUREDUNITS_MOVEMENTCXC + ", @" + LocalDatabase.CAMPO_CAPTUREDUNITSID_MOVEMENTCXC + ", @" +
                                        LocalDatabase.CAMPO_SUBTOTAL_MOVEMENTCXC + ", @" + LocalDatabase.CAMPO_DESCUENTO_MOVEMENTCXC + ", @" +
                                        LocalDatabase.CAMPO_TOTAL_MOVEMENTCXC + ")";
                                        using (SQLiteCommand commandMoves = new SQLiteCommand(queryMovements, db))
                                        {
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CXCID_MOVEMENTCXC, objectCredits.movements[j].idCreditDocument);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ITEMID_MOVEMENTCXC, objectCredits.movements[j].productoId);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_ITEMCODE_MOVEMENTCXC, objectCredits.movements[j].codigoProducto);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_NUMBER_MOVEMENTCXC, objectCredits.movements[j].numeroMovimiento);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_PRICE_MOVEMENTCXC, objectCredits.movements[j].precio);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CAPTUREDUNITS_MOVEMENTCXC, objectCredits.movements[j].unidades);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_CAPTUREDUNITSID_MOVEMENTCXC, objectCredits.movements[j].unidadCapturadaId);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_SUBTOTAL_MOVEMENTCXC, objectCredits.movements[j].subtotal);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_DESCUENTO_MOVEMENTCXC, objectCredits.movements[j].descuento);
                                            commandMoves.Parameters.AddWithValue("@" + LocalDatabase.CAMPO_TOTAL_MOVEMENTCXC, objectCredits.movements[j].total);
                                            int recordsMoves = commandMoves.ExecuteNonQuery();
                                        }
                                    }
                                    else
                                    {
                                        String queryMovements = "UPDATE " + LocalDatabase.TABLA_MOVEMENTCXC + " SET " +
                                        LocalDatabase.CAMPO_ITEMCODE_MOVEMENTCXC + " = '" + objectCredits.movements[j].codigoProducto + "', " +
                                        LocalDatabase.CAMPO_PRICE_MOVEMENTCXC + " = " + objectCredits.movements[j].precio + ", " +
                                        LocalDatabase.CAMPO_CAPTUREDUNITS_MOVEMENTCXC + " = " + objectCredits.movements[j].unidades + ", " +
                                        LocalDatabase.CAMPO_CAPTUREDUNITSID_MOVEMENTCXC + " = " + objectCredits.movements[j].unidadCapturadaId + ", " +
                                        LocalDatabase.CAMPO_SUBTOTAL_MOVEMENTCXC + " = " + objectCredits.movements[j].subtotal + ", " +
                                        LocalDatabase.CAMPO_DESCUENTO_MOVEMENTCXC + " = " + objectCredits.movements[j].descuento + ", " +
                                        LocalDatabase.CAMPO_TOTAL_MOVEMENTCXC + " = " + objectCredits.movements[j].total + " WHERE " +
                                        LocalDatabase.CAMPO_CXCID_MOVEMENTCXC + " = " + objectCredits.movements[j].idCreditDocument + " AND " +
                                        LocalDatabase.CAMPO_ITEMID_MOVEMENTCXC + " = " + objectCredits.movements[j].productoId + " AND " +
                                        LocalDatabase.CAMPO_NUMBER_MOVEMENTCXC + " = " + objectCredits.movements[j].numeroMovimiento;
                                        MovementsCxcModel.createUpdateOrDeleteRecords(queryMovements);
                                    }
                                }
                                contador++;
                            }
                        }
                        cantidadVAloresGuardados = contador;
                        ClsBanderasCargaInicialModel.updateValuesLocal(db, method, cantidadVAloresGuardados);
                        if (contador == itemsAEvaluar)
                        {
                            lastCallActivated = 1;
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
            return lastCallActivated;
        }

    }
}
