using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncTPV.Controllers
{
    public class PrePedidosController
    {
        public static async Task<ExpandoObject> getTotalPrepedidosWs(String routeCode, String parameterName1, String parameterValue1, 
            String parameterName2, String parameterValue2)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                int lastIdServer = 0;
                try
                {
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/getTotalPrePedidos", Method.Post);
                    request.AddJsonBody(new
                    {
                        lastId = 0,
                        limit = 0,
                        routeCode = routeCode,
                        parameterName1 = parameterName1,
                        parameterValue1 = parameterValue1,
                        parameterName2 = parameterName2,
                        parameterValue2 = parameterValue2
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var jsonResp = JsonConvert.DeserializeObject<int>(responseHeader.Result.Content);
                        int total = (int)jsonResp;
                        if (total > 0)
                        {
                            value = total;
                        }
                        else
                        {
                            description = "No se encontró ningún pedido";
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        if (responseHeader.Result.ErrorMessage.Equals("Unable to connect to the remote server"))
                        {
                            value = -404;
                            description = "No pudimos establecer conexión con el servidor! " + responseHeader.Result.ErrorMessage;
                        }
                        else
                        {
                            value = -500;
                            description = "Algo falló al negociar con el servidor " + responseHeader.Result.ErrorMessage;
                        }
                    }
                } catch (Exception e)
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

        public static async Task<ExpandoObject> getAllPrePedidosAPI(String routeCode, int lastIdServer,
            int LIMIT, String parameterName1, String parameterValue1, String parameterName2, String parameterValue2, int totalItemsPrePedServer)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () => {
                int value = 0;
                String description = "";
                try {
                    int lastId = 0;
                    int itemsToEvaluate = 0;
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    // client.Authenticator = new HttpBasicAuthenticator(username, password);
                    var request = new RestRequest("/getAllPrePedidos", Method.Post);
                    request.AddJsonBody(new
                    {
                        lastId = lastIdServer,
                        limit = LIMIT,
                        routeCode = routeCode,
                        parameterName1 = parameterName1,
                        parameterValue1 = parameterValue1,
                        parameterName2 = parameterName2,
                        parameterValue2 = parameterValue2
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed) {
                        var jsonResp = JsonConvert.DeserializeObject<ResponsePrepedidos>(responseHeader.Result.Content);
                        ResponsePrepedidos prepedidos = (ResponsePrepedidos)jsonResp;
                        if (prepedidos.prepedidos != null && prepedidos.prepedidos.Count > 0) {
                            var db = new SQLiteConnection();
                            try {
                                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                                db.Open();
                                int count = 0;
                                foreach (PedidosEncabezadoModel pem in prepedidos.prepedidos) {
                                    lastIdServer = pem.id;
                                    String queryPedidoExist = "SELECT * FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " WHERE " +
                                        LocalDatabase.CAMPO_DOCUMENTOID_PE + " = " + pem.id;
                                    PedidosEncabezadoModel pedidosEncabezado = PedidosEncabezadoModel.getAPedCot(db, queryPedidoExist);
                                    if (pedidosEncabezado != null)
                                    {
                                        int enviado = PedidosEncabezadoModel.estaElPrepedidoSurtidoYEntregado(pedidosEncabezado.idDocumento);
                                        if (enviado == 1)
                                        {
                                            count++;
                                            totalItemsPrePedServer--;
                                        } else {
                                            String codigoCliente = pem.nombreCliente;
                                            if (codigoCliente == null || codigoCliente.Equals(""))
                                            {
                                                codigoCliente = CustomerModel.getClaveForAClient(pem.clienteId);
                                                if (codigoCliente.Equals(""))
                                                {
                                                    dynamic responseCustomer = await CustomersController.getACustomerAPI(pem.clienteId);
                                                    if (responseCustomer.value == 1)
                                                    {
                                                        ClsClienteModel customerModel = responseCustomer.customerModel;
                                                        if (customerModel != null)
                                                            codigoCliente = customerModel.CLAVE;
                                                    }
                                                }
                                            }
                                            String query = "UPDATE " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " SET " +
                                            LocalDatabase.CAMPO_CLIENTEID_PE + " = "+pem.clienteId + ", "+
                                            LocalDatabase.CAMPO_CNOMBRECLIENTE_PE + " = '" + codigoCliente + "', "+
                                            LocalDatabase.CAMPO_CNOMBREAGENTECC_PE + " = '" + pem.agenteId + "', "+
                                            LocalDatabase.CAMPO_CFECHA_PE + " = '" + pem.fechaHora + "', "+
                                            LocalDatabase.CAMPO_CFOLIO_PE + " = '" + pem.folio + "', "+
                                            LocalDatabase.CAMPO_CSUBTOTAL_PE + " = " +pem.subtotal + ", "+
                                            LocalDatabase.CAMPO_CDESCUENTO_PE + " = " + pem.descuento + ", "+
                                            LocalDatabase.CAMPO_CTOTAL_PE + " = " + pem.total + ", "+
                                            LocalDatabase.CAMPO_TYPE_PE + " = 4, "+
                                            LocalDatabase.CAMPO_OBSERVATION_PE + " = '" + pem.observation + "', "+
                                            LocalDatabase.CAMPO_FACTURAR_PE + " = " + pem.facturar + " WHERE "+
                                            LocalDatabase.CAMPO_DOCUMENTOID_PE+" = "+pedidosEncabezado.idDocumento;
                                            if (PedidosEncabezadoModel.createUpdateOrDeleteRecords(db, query) > 0)
                                            {
                                                foreach (PedidoDetalleModel pdm in pem.movements)
                                                {
                                                    String queryGetLastId = "SELECT " + LocalDatabase.CAMPO_ID_PD + " FROM " + 
                                                    LocalDatabase.TABLA_PEDIDODETALLE +" WHERE "+
                                                    LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_PD+" = "+pem.id+" AND "+
                                                    LocalDatabase.CAMPO_CNUMEROMOVIMIENTO_PD+" = "+ pdm.numero;
                                                    int getIDPedidoDetalle = PedidoDetalleModel.getIntValue(db, queryGetLastId);
                                                    query = "UPDATE " + LocalDatabase.TABLA_PEDIDODETALLE + " SET "+
                                                    LocalDatabase.CAMPO_CIDPRODUCTO_PD + " = "+pdm.itemId + ", "+
                                                    LocalDatabase.CAMPO_CCODIGOPRODUCTO_PD + " = '" + pdm.itemCode + "', "+
                                                    LocalDatabase.CAMPO_CNUMEROMOVIMIENTO_PD + " = " + pdm.numero + ", "+
                                                    LocalDatabase.CAMPO_CPRECIO_PD + " = " + pdm.precio + ", "+
                                                    LocalDatabase.CAMPO_CUNIDADES_PD + " = " +pdm.unidadesCapturadas + ", "+
                                                    LocalDatabase.CAMPO_CAPTUREDUNITID_PD + " = " + pdm.unidadCapturadaId + ", "+
                                                    LocalDatabase.CAMPO_CSUBTOTAL_PD + " = " + pdm.subtotal + ", "+
                                                    LocalDatabase.CAMPO_CDESCUENTO_PD + " = " + pdm.descuento + ", "+
                                                    LocalDatabase.CAMPO_CTOTAL_PD+" = " +pdm.total + ", "+
                                                    LocalDatabase.CAMPO_NONCONVERTIBLEUNITS_PD + " = " + pdm.unidadesNoConvertibles + ", "+
                                                    LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_PD + " = " + pdm.unidadNoConvertibleId + ", "+
                                                    LocalDatabase.CAMPO_OBSERVATION_PD+" = @observation"+
                                                    " WHERE " +LocalDatabase.CAMPO_ID_PD+" = "+ getIDPedidoDetalle+" AND "+
                                                    LocalDatabase.CAMPO_CNUMEROMOVIMIENTO_PD+" = "+ pdm.numero;
                                                    PedidoDetalleModel.createUpdateOrDeleteRecords(db, query, pdm.observation);
                                                }
                                                count++;
                                            }
                                        }
                                    } else {
                                        String queryGetLastId = "SELECT " + LocalDatabase.CAMPO_ID_PEDIDOENCABEZADO + " FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO +
                                    " ORDER BY " + LocalDatabase.CAMPO_ID_PEDIDOENCABEZADO + " DESC LIMIT 1";
                                        lastId = PedidosEncabezadoModel.getIntValue(db, queryGetLastId);
                                        lastId++;
                                        String codigoCliente = pem.nombreCliente;
                                        if (codigoCliente == null || codigoCliente.Equals(""))
                                        {
                                            codigoCliente = CustomerModel.getClaveForAClient(pem.clienteId);
                                            if (codigoCliente.Equals(""))
                                            {
                                                dynamic responseCustomer = await CustomersController.getACustomerAPI(pem.clienteId);
                                                if (responseCustomer.value == 1)
                                                {
                                                    ClsClienteModel customerModel = responseCustomer.customerModel;
                                                    if (customerModel != null)
                                                        codigoCliente = customerModel.CLAVE;
                                                }
                                            }
                                        }
                                        String query = "INSERT INTO " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " VALUES(" + lastId + ", " + pem.id + ", " +
                                        pem.clienteId + ", '" + codigoCliente + "', '', '" + pem.agenteId + "', '" + pem.fechaHora + "', '" + pem.folio + "', " +
                                        pem.subtotal + ", " + pem.descuento + ", " + pem.total + ", 0, 0, 4, '" + pem.observation + "', " + pem.facturar + ")";
                                        if (PedidosEncabezadoModel.createUpdateOrDeleteRecords(db, query) > 0)
                                        {
                                            foreach (PedidoDetalleModel pdm in pem.movements)
                                            {
                                                queryGetLastId = "SELECT " + LocalDatabase.CAMPO_ID_PD + " FROM " + LocalDatabase.TABLA_PEDIDODETALLE +
                                                " ORDER BY " + LocalDatabase.CAMPO_ID_PD + " DESC LIMIT 1";
                                                int lastIdMove = PedidoDetalleModel.getIntValue(db, queryGetLastId);
                                                lastIdMove++;
                                                query = "INSERT INTO " + LocalDatabase.TABLA_PEDIDODETALLE + " VALUES(" + lastIdMove + ", " + pem.id +
                                                ", " + pdm.itemId + ", '" + pdm.itemCode + "', " + pdm.numero + ", " + pdm.precio + ", " +
                                                pdm.unidadesCapturadas + ", " + pdm.unidadCapturadaId + ", " + pdm.subtotal + ", " + pdm.descuento + ", " +
                                                pdm.total + ", " + pdm.unidadesNoConvertibles + ", " + pdm.unidadNoConvertibleId + ", @observation)";
                                                PedidoDetalleModel.createUpdateOrDeleteRecords(db, query, pdm.observation);
                                            }
                                            count++;
                                        }
                                    }
                                }
                                itemsToEvaluate = prepedidos.prepedidos.Count;
                                if (itemsToEvaluate == count) {
                                    value = 1;
                                    description = "Datos Actualizados Correctamente";
                                }
                            } catch (SQLiteException e) {
                                SECUDOC.writeLog(e.ToString());
                                value = -1;
                                description = "Exception: " + e.ToString();
                            } finally {
                                if (db != null && db.State == ConnectionState.Open)
                                    db.Close();
                            }
                        } else {
                            description = "No se Encontró Ningún Documento para esta Ruta";
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        if (responseHeader.Result.ErrorMessage.Equals("Unable to connect to the remote server"))
                        {
                            value = -404;
                            description = "No pudimos establecer conexión con el servidor! " + responseHeader.Result.ErrorMessage;
                        }
                        else
                        {
                            value = -500;
                            description = "Algo falló al negociar con el servidor " + responseHeader.Result.ErrorMessage;
                        }
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
                    response.lastIdServer = lastIdServer;
                    response.totalItemsPrePedServer = totalItemsPrePedServer;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> getTotalPrepedidosLAN(String routeCode,
            String parameterName1, String parameterValue1, String parameterName2, String parameterValue2)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    String dbNamePanel = InstanceSQLSEModel.getDbName(InstanceSQLSEModel.ID_PANEL);
                    String dbNameComercial = InstanceSQLSEModel.getDbName(InstanceSQLSEModel.ID_COM);
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    int totalPrepedidos = ClsDocumentoModel.getTotalOfDocumentsPrePedido(panelInstance, routeCode, parameterName1,
                        parameterValue1, parameterName2, parameterValue2, dbNamePanel, dbNameComercial);
                    if (totalPrepedidos > 0)
                    {
                        value = totalPrepedidos;
                    } else
                    {
                        description = "No se encontró ningún pedido pendiente";
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

        public static async Task<ExpandoObject> getAllPrePedidosLAN(String panelInstance, int lastId, String routeCode,
            int limit, String parameterName1, String parameterValue1, String parameterName2, String parameterValue2, int totalItemsPrePedServer)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () => {
                int value = 0;
                String description = "";
                int lastIdServer = 0;
                String dbNamePanel = InstanceSQLSEModel.getDbName(InstanceSQLSEModel.ID_PANEL);
                String dbNameComercial = InstanceSQLSEModel.getDbName(InstanceSQLSEModel.ID_COM);
                var connCom = new SqlConnection();
                try
                {
                    connCom.ConnectionString = panelInstance;
                    connCom.Open();
                    String queryPrePedidos = "";
                    if (limit == 0)
                    {
                        queryPrePedidos = "SELECT D.CIDDOCUMENTO, D.CIDCLIENTEPROVEEDOR, D." + DbStructure.RomDb.CAMPO_CLIENTECODIGO_DOCUMENTO + ", " +
                        "D.CIDAGENTE, D.CFECHA, D.CREFERENCIA, D.CTOTAL, D.CDESCUENTO, D." + DbStructure.RomDb.CAMPO_OBSERVACION_DOCUMENTO + ", " +
                        "D." + DbStructure.RomDb.CAMPO_FACTURAR_DOCUMENTO + ", M.CIDPRODUCTO, M.CCODIGOPRODUCTO, M.CNUMEROMOVIMIENTO, M.CPRECIO, " +
                        "M.CUNIDADESCAPTURADAS, M.CUNIDADCAPTURADAID, M.CTOTAL AS TOTALM, M.CDESCUENTO1, M." + DbStructure.RomDb.CAMPO_UNIDADESNOCONVERTIBLES_MOVIMIENTO + ", " +
                        "M." + DbStructure.RomDb.CAMPO_UNIDADNOCONVERTIBLEID_MOVIMIENTO + ", " +
                        "(CASE WHEN M." + DbStructure.RomDb.CAMPO_OBSERVACION_MOVIMIENTO + " IS NULL THEN '' ELSE M." +
                        DbStructure.RomDb.CAMPO_OBSERVACION_MOVIMIENTO + " END) AS " + DbStructure.RomDb.CAMPO_OBSERVACION_MOVIMIENTO + " " +
                        "FROM " + dbNamePanel + ".dbo.Documentos D " +
                        "INNER JOIN " + dbNamePanel + ".dbo.Movimientos M ON D.CIDDOCUMENTO = M.CIDDOCUMENTO " +
                        "INNER JOIN " + dbNameComercial + ".dbo.admClientes C ON C.CCODIGOCLIENTE = D.CCODIGOCLIENTE " +
                        "WHERE D." + DbStructure.RomDb.CAMPO_TIPO_DOCUMENTO + " = 50 AND " +
                        "D.CREADO = 'R' ";
                        if (!routeCode.Equals(""))
                            queryPrePedidos += "AND D.CRUTA = @routeCode ";
                        queryPrePedidos += "AND (C.CRAZONSOCIAL LIKE @" + parameterName1+" "+
                "OR D.CREFERENCIA LIKE @"+ parameterName2 + ") AND D.CIDDOCUMENTO > @lastId ORDER BY D.CIDDOCUMENTO ASC";
                    } else
                    {
                        queryPrePedidos = "SELECT TOP " + limit + " D.CIDDOCUMENTO, D.CIDCLIENTEPROVEEDOR, D." + DbStructure.RomDb.CAMPO_CLIENTECODIGO_DOCUMENTO + ", " +
                        "D.CIDAGENTE, D.CFECHA, D.CREFERENCIA, D.CTOTAL, D.CDESCUENTO, D." + DbStructure.RomDb.CAMPO_OBSERVACION_DOCUMENTO + ", " +
                        "D." + DbStructure.RomDb.CAMPO_FACTURAR_DOCUMENTO + ", M.CIDPRODUCTO, M.CCODIGOPRODUCTO, M.CNUMEROMOVIMIENTO, M.CPRECIO, " +
                        "M.CUNIDADESCAPTURADAS, M.CUNIDADCAPTURADAID, M.CTOTAL AS TOTALM, M.CDESCUENTO1, M." + DbStructure.RomDb.CAMPO_UNIDADESNOCONVERTIBLES_MOVIMIENTO + ", " +
                        "M." + DbStructure.RomDb.CAMPO_UNIDADNOCONVERTIBLEID_MOVIMIENTO + ", " +
                        "(CASE WHEN M." + DbStructure.RomDb.CAMPO_OBSERVACION_MOVIMIENTO + " IS NULL THEN '' ELSE M." +
                DbStructure.RomDb.CAMPO_OBSERVACION_MOVIMIENTO + " END) AS " + DbStructure.RomDb.CAMPO_OBSERVACION_MOVIMIENTO + " " +
                "FROM " + dbNamePanel + ".dbo.Documentos D " +
                        "INNER JOIN " + dbNamePanel + ".dbo.Movimientos M ON D.CIDDOCUMENTO = M.CIDDOCUMENTO " +
                        "INNER JOIN " + dbNameComercial + ".dbo.admClientes C ON C.CCODIGOCLIENTE = D.CCODIGOCLIENTE " +
                        "WHERE D." + DbStructure.RomDb.CAMPO_TIPO_DOCUMENTO + " = 50 AND " +
                        "D.CREADO = 'R' ";
                        if (!routeCode.Equals(""))
                            queryPrePedidos += "AND D.CRUTA = @routeCode ";
                        queryPrePedidos += "AND (C.CRAZONSOCIAL LIKE @" + parameterName1+" "+
                "OR D.CREFERENCIA LIKE @"+ parameterName2 + ") AND D.CIDDOCUMENTO > @lastId ORDER BY D.CIDDOCUMENTO ASC";
                    }
                    using (SqlCommand command = new SqlCommand(queryPrePedidos, connCom))
                    {
                        if (!routeCode.Equals(""))
                            command.Parameters.AddWithValue("@routeCode", routeCode);
                        command.Parameters.AddWithValue("@lastId", lastId);
                        command.Parameters.AddWithValue("@"+ parameterName1, "%"+parameterValue1+"%");
                        command.Parameters.AddWithValue("@"+ parameterName2, "%"+parameterValue2+"%");
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                var db = new SQLiteConnection();
                                try
                                {
                                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                                    db.Open();
                                    int countMovements = 1;
                                    int lastDocumentId = 0;
                                    while (reader.Read())
                                    {
                                        int documentId = Convert.ToInt32(reader["CIDDOCUMENTO"].ToString().Trim());
                                        lastIdServer = documentId;
                                        String queryPedidoExist = "SELECT * FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " WHERE " +
                                        LocalDatabase.CAMPO_DOCUMENTOID_PE + " = " + documentId;
                                        PedidosEncabezadoModel pedidosEncabezado = PedidosEncabezadoModel.getAPedCot(db, queryPedidoExist);
                                        if (pedidosEncabezado != null)
                                        {
                                            int enviado = PedidosEncabezadoModel.estaElPrepedidoSurtidoYEntregado(pedidosEncabezado.idDocumento);
                                            if (enviado == 1)
                                            {
                                                totalItemsPrePedServer--;
                                            }
                                            else
                                            {
                                                int movementNumber = Convert.ToInt32(reader["CNUMEROMOVIMIENTO"].ToString().Trim());
                                                if (documentId != lastDocumentId)
                                                    countMovements = 1;
                                                if (countMovements == 1)
                                                {
                                                    int idCliente = Convert.ToInt32(reader["CIDCLIENTEPROVEEDOR"].ToString().Trim());
                                                    String codigoCliente = "";
                                                    if (codigoCliente.Equals(""))
                                                    {
                                                        codigoCliente = CustomerModel.getClaveForAClient(idCliente);
                                                        if (codigoCliente.Equals(""))
                                                        {
                                                            dynamic responseCustomer = await CustomersController.getACustomerLAN(db, idCliente);
                                                            if (responseCustomer.value == 1)
                                                            {
                                                                ClsClienteModel customerModel = responseCustomer.customerModel;
                                                                if (customerModel != null)
                                                                    codigoCliente = customerModel.CLAVE;
                                                            }

                                                        }
                                                    }
                                                    String query = "UPDATE " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " SET " +
                                                    LocalDatabase.CAMPO_CLIENTEID_PE + " = " + idCliente + ", " +
                                                    LocalDatabase.CAMPO_CNOMBRECLIENTE_PE + " = '" + codigoCliente + "', " +
                                                    LocalDatabase.CAMPO_CNOMBREAGENTECC_PE + " = '" + Convert.ToInt32(reader["CIDAGENTE"].ToString().Trim()) + "', " +
                                                    LocalDatabase.CAMPO_CFECHA_PE + " = '" + reader["CFECHA"].ToString().Trim() + "', " +
                                                    LocalDatabase.CAMPO_CFOLIO_PE + " = '" + reader["CREFERENCIA"].ToString().Trim() + "', " +
                                                    LocalDatabase.CAMPO_CSUBTOTAL_PE + " = " + Convert.ToDouble(reader["CTOTAL"].ToString().Trim()) + ", " +
                                                    LocalDatabase.CAMPO_CDESCUENTO_PE + " = " + Convert.ToDouble(reader["CDESCUENTO"].ToString().Trim()) + ", " +
                                                    LocalDatabase.CAMPO_CTOTAL_PE + " = " + (Convert.ToDouble(reader["CTOTAL"].ToString().Trim()) - Convert.ToDouble(reader["CDESCUENTO"].ToString().Trim())) + ", " +
                                                    LocalDatabase.CAMPO_TYPE_PE + " = 4, " +
                                                    LocalDatabase.CAMPO_OBSERVATION_PE + " = '" + reader[DbStructure.RomDb.CAMPO_OBSERVACION_DOCUMENTO].ToString().Trim() + "', " +
                                                    LocalDatabase.CAMPO_FACTURAR_PE + " = " + Convert.ToInt32(reader[DbStructure.RomDb.CAMPO_FACTURAR_DOCUMENTO].ToString().Trim()) + " WHERE " +
                                                    LocalDatabase.CAMPO_DOCUMENTOID_PE + " = " + pedidosEncabezado.idDocumento;
                                                    if (PedidosEncabezadoModel.createUpdateOrDeleteRecords(db, query) > 0)
                                                    {
                                                        String getIdPedidoDetalle = "SELECT " + LocalDatabase.CAMPO_ID_PD + " FROM " +
                                                        LocalDatabase.TABLA_PEDIDODETALLE + " WHERE " + LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_PD + " = " +
                                                        pedidosEncabezado.idDocumento + " AND " + LocalDatabase.CAMPO_CNUMEROMOVIMIENTO_PD + " = " + movementNumber;
                                                        int idMovement = PedidoDetalleModel.getIntValue(db, getIdPedidoDetalle);
                                                        query = "UPDATE " + LocalDatabase.TABLA_PEDIDODETALLE + " SET " +
                                                        LocalDatabase.CAMPO_CIDPRODUCTO_PD + " = " + Convert.ToInt32(reader["CIDPRODUCTO"].ToString().Trim()) + ", " +
                                                        LocalDatabase.CAMPO_CCODIGOPRODUCTO_PD + " = '" + reader["CCODIGOPRODUCTO"].ToString().Trim() + "', " +
                                                        LocalDatabase.CAMPO_CNUMEROMOVIMIENTO_PD + " = " + movementNumber + ", " +
                                                        LocalDatabase.CAMPO_CPRECIO_PD + " = " + Convert.ToDouble(reader["CPRECIO"].ToString().Trim()) + ", " +
                                                        LocalDatabase.CAMPO_CUNIDADES_PD + " = " + Convert.ToDouble(reader["CUNIDADESCAPTURADAS"].ToString().Trim()) + ", " +
                                                        LocalDatabase.CAMPO_CAPTUREDUNITID_PD + " = " + Convert.ToInt32(reader["CUNIDADCAPTURADAID"].ToString().Trim()) + ", " +
                                                        LocalDatabase.CAMPO_CSUBTOTAL_PD + " = " + Convert.ToDouble(reader["TOTALM"].ToString().Trim()) + ", " +
                                                        LocalDatabase.CAMPO_CDESCUENTO_PD + " = " + Convert.ToDouble(reader["CDESCUENTO1"].ToString().Trim()) + ", " +
                                                        LocalDatabase.CAMPO_CTOTAL_PD + " = " + (Convert.ToDouble(reader["TOTALM"].ToString().Trim()) - Convert.ToDouble(reader["CDESCUENTO1"].ToString().Trim())) + ", " +
                                                        LocalDatabase.CAMPO_NONCONVERTIBLEUNITS_PD + " = " + Convert.ToDouble(reader[DbStructure.RomDb.CAMPO_UNIDADESNOCONVERTIBLES_MOVIMIENTO].ToString().Trim()) + ", " +
                                                        LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_PD + " = " + Convert.ToInt32(reader[DbStructure.RomDb.CAMPO_UNIDADNOCONVERTIBLEID_MOVIMIENTO].ToString().Trim()) + ", " +
                                                        LocalDatabase.CAMPO_OBSERVATION_PD + " = @observation WHERE " +
                                                        LocalDatabase.CAMPO_ID_PD + " = " + idMovement;
                                                        PedidoDetalleModel.createUpdateOrDeleteRecords(db, query, reader[DbStructure.RomDb.CAMPO_OBSERVACION_MOVIMIENTO].ToString().Trim());
                                                        countMovements++;
                                                    }
                                                }
                                                else
                                                {
                                                    String getIdPedidoDetalle = "SELECT " + LocalDatabase.CAMPO_ID_PD + " FROM " +
                                                        LocalDatabase.TABLA_PEDIDODETALLE + " WHERE " + LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_PD + " = " +
                                                        pedidosEncabezado.idDocumento + " AND " + LocalDatabase.CAMPO_CNUMEROMOVIMIENTO_PD + " = " + movementNumber;
                                                    int idMovement = PedidoDetalleModel.getIntValue(db, getIdPedidoDetalle);
                                                    String query = "UPDATE " + LocalDatabase.TABLA_PEDIDODETALLE + " SET " +
                                                    LocalDatabase.CAMPO_CIDPRODUCTO_PD + " = " + Convert.ToInt32(reader["CIDPRODUCTO"].ToString().Trim()) + ", " +
                                                    LocalDatabase.CAMPO_CCODIGOPRODUCTO_PD + " = '" + reader["CCODIGOPRODUCTO"].ToString().Trim() + "', " +
                                                    LocalDatabase.CAMPO_CNUMEROMOVIMIENTO_PD + " = " + movementNumber + ", " +
                                                    LocalDatabase.CAMPO_CPRECIO_PD + " = " + Convert.ToDouble(reader["CPRECIO"].ToString().Trim()) + ", " +
                                                    LocalDatabase.CAMPO_CUNIDADES_PD + " = " + Convert.ToDouble(reader["CUNIDADESCAPTURADAS"].ToString().Trim()) + ", " +
                                                    LocalDatabase.CAMPO_CAPTUREDUNITID_PD + " = " + Convert.ToInt32(reader["CUNIDADCAPTURADAID"].ToString().Trim()) + ", " +
                                                    LocalDatabase.CAMPO_CSUBTOTAL_PD + " = " + Convert.ToDouble(reader["TOTALM"].ToString().Trim()) + ", " +
                                                    LocalDatabase.CAMPO_CDESCUENTO_PD + " = " + Convert.ToDouble(reader["CDESCUENTO1"].ToString().Trim()) + ", " +
                                                    LocalDatabase.CAMPO_CTOTAL_PD + " = " + (Convert.ToDouble(reader["TOTALM"].ToString().Trim()) - Convert.ToDouble(reader["CDESCUENTO1"].ToString().Trim())) + ", " +
                                                    LocalDatabase.CAMPO_NONCONVERTIBLEUNITS_PD + " = " + Convert.ToDouble(reader[DbStructure.RomDb.CAMPO_UNIDADESNOCONVERTIBLES_MOVIMIENTO].ToString().Trim()) + ", " +
                                                    LocalDatabase.CAMPO_NONCONVERTIBLEUNITID_PD + " = " + Convert.ToInt32(reader[DbStructure.RomDb.CAMPO_UNIDADNOCONVERTIBLEID_MOVIMIENTO].ToString().Trim()) + ", " +
                                                    LocalDatabase.CAMPO_OBSERVATION_PD + " = @observation WHERE " +
                                                    LocalDatabase.CAMPO_ID_PD + " = " + idMovement;
                                                    PedidoDetalleModel.createUpdateOrDeleteRecords(db, query, reader[DbStructure.RomDb.CAMPO_OBSERVACION_MOVIMIENTO].ToString().Trim());
                                                    countMovements++;
                                                }
                                                lastDocumentId = documentId;
                                            }
                                        }
                                        else
                                        {
                                            int idCliente = Convert.ToInt32(reader["CIDCLIENTEPROVEEDOR"].ToString().Trim());
                                            int movementNumber = Convert.ToInt32(reader["CNUMEROMOVIMIENTO"].ToString().Trim());
                                            if (documentId != lastDocumentId)
                                                countMovements = 1;
                                            if (countMovements == 1)
                                            {
                                                String queryGetLastId = "SELECT " + LocalDatabase.CAMPO_ID_PEDIDOENCABEZADO + " FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO +
                                        " ORDER BY " + LocalDatabase.CAMPO_ID_PEDIDOENCABEZADO + " DESC LIMIT 1";
                                                lastId = PedidosEncabezadoModel.getIntValue(db, queryGetLastId);
                                                lastId++;
                                                String codigoCliente = "";
                                                if (codigoCliente.Equals(""))
                                                {
                                                    codigoCliente = CustomerModel.getClaveForAClient(idCliente);
                                                    if (codigoCliente.Equals(""))
                                                    {
                                                        dynamic responseCustomer = await CustomersController.getACustomerLAN(idCliente);
                                                        if (responseCustomer.value == 1)
                                                        {
                                                            ClsClienteModel customerModel = responseCustomer.customerModel;
                                                            if (customerModel != null)
                                                                codigoCliente = customerModel.CLAVE;
                                                        }

                                                    }
                                                }
                                                String query = "INSERT INTO " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " VALUES(" + lastId + ", " + documentId + ", " +
                                                idCliente + ", '" + codigoCliente + "', '', '" +
                                                Convert.ToInt32(reader["CIDAGENTE"].ToString().Trim()) + "', '" + reader["CFECHA"].ToString().Trim() + "', '" +
                                                reader["CREFERENCIA"].ToString().Trim() + "', " + Convert.ToDouble(reader["CTOTAL"].ToString().Trim()) + ", " +
                                                Convert.ToDouble(reader["CDESCUENTO"].ToString().Trim()) + ", " +
                                                (Convert.ToDouble(reader["CTOTAL"].ToString().Trim()) - Convert.ToDouble(reader["CDESCUENTO"].ToString().Trim())) + ", 0, 0, 4, '" +
                                                reader[DbStructure.RomDb.CAMPO_OBSERVACION_DOCUMENTO].ToString().Trim() + "', " +
                                                Convert.ToInt32(reader[DbStructure.RomDb.CAMPO_FACTURAR_DOCUMENTO].ToString().Trim()) + ")";
                                                if (PedidosEncabezadoModel.createUpdateOrDeleteRecords(db, query) > 0)
                                                {
                                                    queryGetLastId = "SELECT " + LocalDatabase.CAMPO_ID_PD + " FROM " + LocalDatabase.TABLA_PEDIDODETALLE +
                                                " ORDER BY " + LocalDatabase.CAMPO_ID_PD + " DESC LIMIT 1";
                                                    int lastIdMove = PedidoDetalleModel.getIntValue(db, queryGetLastId);
                                                    lastIdMove++;
                                                    query = "INSERT INTO " + LocalDatabase.TABLA_PEDIDODETALLE + " VALUES(" + lastIdMove + ", " + documentId +
                                                    ", " + Convert.ToInt32(reader["CIDPRODUCTO"].ToString().Trim()) + ", '" +
                                                    reader["CCODIGOPRODUCTO"].ToString().Trim() + "', " + movementNumber + ", " +
                                                    Convert.ToDouble(reader["CPRECIO"].ToString().Trim()) + ", " +
                                                    Convert.ToDouble(reader["CUNIDADESCAPTURADAS"].ToString().Trim()) + ", " +
                                                    Convert.ToInt32(reader["CUNIDADCAPTURADAID"].ToString().Trim()) + ", " +
                                                    Convert.ToDouble(reader["TOTALM"].ToString().Trim()) + ", " +
                                                    Convert.ToDouble(reader["CDESCUENTO1"].ToString().Trim()) + ", " +
                                                    (Convert.ToDouble(reader["TOTALM"].ToString().Trim()) - Convert.ToDouble(reader["CDESCUENTO1"].ToString().Trim())) + ", " +
                                                    Convert.ToDouble(reader[DbStructure.RomDb.CAMPO_UNIDADESNOCONVERTIBLES_MOVIMIENTO].ToString().Trim()) + ", " +
                                                    Convert.ToInt32(reader[DbStructure.RomDb.CAMPO_UNIDADNOCONVERTIBLEID_MOVIMIENTO].ToString().Trim()) + ", " +
                                                    "@observation)";
                                                    PedidoDetalleModel.createUpdateOrDeleteRecords(db, query, reader[DbStructure.RomDb.CAMPO_OBSERVACION_MOVIMIENTO].ToString().Trim());
                                                    /* Actualizar información del cliente */
                                                    countMovements++;
                                                }
                                            }
                                            else
                                            {
                                                String queryGetLastId = "SELECT " + LocalDatabase.CAMPO_ID_PD + " FROM " + LocalDatabase.TABLA_PEDIDODETALLE +
                                                " ORDER BY " + LocalDatabase.CAMPO_ID_PD + " DESC LIMIT 1";
                                                int lastIdMove = PedidoDetalleModel.getIntValue(db, queryGetLastId);
                                                lastIdMove++;
                                                String query = "INSERT INTO " + LocalDatabase.TABLA_PEDIDODETALLE + " VALUES(" + lastIdMove + ", " + documentId +
                                                ", " + Convert.ToInt32(reader["CIDPRODUCTO"].ToString().Trim()) + ", '" +
                                                reader["CCODIGOPRODUCTO"].ToString().Trim() + "', " + movementNumber + ", " +
                                                Convert.ToDouble(reader["CPRECIO"].ToString().Trim()) + ", " +
                                                Convert.ToDouble(reader["CUNIDADESCAPTURADAS"].ToString().Trim()) + ", " +
                                                Convert.ToInt32(reader["CUNIDADCAPTURADAID"].ToString().Trim()) + ", " +
                                                Convert.ToDouble(reader["TOTALM"].ToString().Trim()) + ", " +
                                                Convert.ToDouble(reader["CDESCUENTO1"].ToString().Trim()) + ", " +
                                                (Convert.ToDouble(reader["TOTALM"].ToString().Trim()) - Convert.ToDouble(reader["CDESCUENTO1"].ToString().Trim())) + ", " +
                                                Convert.ToDouble(reader[DbStructure.RomDb.CAMPO_UNIDADESNOCONVERTIBLES_MOVIMIENTO].ToString().Trim()) + ", " +
                                                Convert.ToInt32(reader[DbStructure.RomDb.CAMPO_UNIDADNOCONVERTIBLEID_MOVIMIENTO].ToString().Trim()) + ", " +
                                                "@observation)";
                                                PedidoDetalleModel.createUpdateOrDeleteRecords(db, query, reader[DbStructure.RomDb.CAMPO_OBSERVACION_MOVIMIENTO].ToString().Trim());
                                                countMovements++;
                                            }
                                            lastDocumentId = documentId;
                                        }
                                    }
                                }
                                catch (SQLiteException e)
                                {
                                    SECUDOC.writeLog(e.ToString());
                                    value = -1;
                                    description = e.Message;
                                }
                                finally
                                {
                                    if (db != null && db.State == ConnectionState.Open)
                                        db.Close();
                                }
                                value = 1;
                                description = "PrePedidos Actualizados Correctamente";
                            }
                            else
                            {
                                description = "No se Encontraron PrePedidos Para esta Ruta";
                            }
                            if (reader != null && !reader.IsClosed)
                                reader.Close();
                        }
                    }
                }
                catch (SqlException e)
                {
                    SECUDOC.writeLog("Exception: " + e.ToString());
                    value = -1;
                    description = e.Message;
                }
                finally
                {
                    if (connCom != null && connCom.State == ConnectionState.Open)
                        connCom.Close();
                    response.value = value;
                    response.description = description;
                    response.lastIdServer = lastIdServer;
                    response.totalItemsPrePedServer = totalItemsPrePedServer;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> deletePrePedidos(List<int> idsToDelete)
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
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/deletePrePedido", Method.Post);
                    request.AddJsonBody(new {
                        ids = idsToDelete
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content; // Raw content as string
                        var jsonResp = JsonConvert.DeserializeObject<ResponseDeleteCM>(content);
                        ResponseDeleteCM idsDeleted = (ResponseDeleteCM)jsonResp;
                        int count = 0;
                        if (idsDeleted.response != null)
                        {
                            foreach (int id in idsDeleted.response)
                            {
                                String query = "DELETE FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " WHERE " + LocalDatabase.CAMPO_LISTO_PE + " = " + 2 + " AND " +
                                        LocalDatabase.CAMPO_DOCUMENTOID_PE + " = " + id;
                                if (PedidosEncabezadoModel.deleteARecord(query))
                                {
                                    String queryMoves = "DELETE FROM " + LocalDatabase.TABLA_PEDIDODETALLE + " WHERE " + LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_PD + " = " + id;
                                    if (PedidoDetalleModel.deleteARecord(queryMoves))
                                    {

                                    }
                                    else
                                    {

                                    }
                                    count++;
                                }
                            }
                            value = 1;
                        }
                        else description = "No se encontró nigún documento a eliminar";

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
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> deletePrePedidosLAN(List<int> idsToDelete)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    int lastId = 0;
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    List<int> idsDeleted = new List<int>(); ;
                    if (idsToDelete != null)
                    {
                        foreach (int id in idsToDelete)
                        {
                            String query = "DELETE FROM " + DbStructure.RomDb.TABLA_DOCUMENTO + " WHERE " + DbStructure.RomDb.CAMPO_TIPO_DOCUMENTO + " = " + 50 +
                                " AND " + DbStructure.RomDb.CAMPO_CREADO_DOCUMENTO + " = 'R' AND " + DbStructure.RomDb.CAMPO_ID_DOCUMENTO + " = " + id;
                            if (ClsDocumentoModel.deleteARecord(panelInstance, query))
                                idsDeleted.Add(id);
                        }
                    }
                    int count = 0;
                    if (idsDeleted != null)
                    {
                        foreach (int id in idsDeleted)
                        {
                            String query = "DELETE FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " WHERE " + LocalDatabase.CAMPO_LISTO_PE + " = " + 2 + " AND " +
                                    LocalDatabase.CAMPO_DOCUMENTOID_PE + " = " + id;
                            if (PedidosEncabezadoModel.deleteARecord(query))
                            {
                                String queryMoves = "DELETE FROM " + LocalDatabase.TABLA_PEDIDODETALLE + " WHERE " + LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_PD + " = " + id;
                                if (PedidoDetalleModel.deleteARecord(queryMoves))
                                {

                                }
                                else
                                {

                                }
                                count++;
                            }
                        }
                        value = 1;
                    }
                    else description = "No había nigún documento a eliminar";

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

        public static async Task<ExpandoObject> validarDocumentosEnPausaTipoPrepedido()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(() => {
                int saved = 0;
                String query = "SELECT * FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO+" WHERE "+LocalDatabase.CAMPO_TYPE_PE+" = "+
                PedidosEncabezadoModel.TYPE_PREPEDIDOS;
                List<PedidosEncabezadoModel> pedidosList = PedidosEncabezadoModel.getAllDocuments(query);
                if (pedidosList != null)
                {
                    foreach (PedidosEncabezadoModel pem in pedidosList)
                    {
                        query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " + 
                        LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_DOC + " = "+pem.idDocumento + " AND " + 
                        LocalDatabase.CAMPO_PAUSAR_DOC + " = 1 AND "+LocalDatabase.CAMPO_CANCELADO_DOC+" = 0";
                        if (DocumentModel.getIntValue(query) > 0)
                        {
                            query = "SELECT " + LocalDatabase.CAMPO_ID_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                            LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_DOC + " = " + pem.idDocumento;
                            int idDocumentoVenta = DocumentModel.getIntValue(query);
                            bool documentoEnviadoAlCliente = DocumentModel.isItDocumentPrepedidoSendedToTheCustomerAndPendienteDeDestarar(idDocumentoVenta);
                            if (documentoEnviadoAlCliente)
                            {
                                query = "UPDATE " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " SET " +
                            LocalDatabase.CAMPO_SURTIDO_PE + " = 1, "+LocalDatabase.CAMPO_LISTO_PE+" = 1 WHERE " +
                            LocalDatabase.CAMPO_DOCUMENTOID_PE + " = " + pem.idDocumento;
                                saved = PedidosEncabezadoModel.createUpdateOrDeleteRecords(query);
                            } else
                            {
                                query = "UPDATE " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " SET " +
                            LocalDatabase.CAMPO_SURTIDO_PE + " = 1 WHERE " +
                            LocalDatabase.CAMPO_DOCUMENTOID_PE + " = " + pem.idDocumento;
                                saved = PedidosEncabezadoModel.createUpdateOrDeleteRecords(query);
                            }
                        }
                        query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " + 
                        LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_DOC + " = " +pem.idDocumento + " AND " +
                        LocalDatabase.CAMPO_IDWEBSERVICE_DOC + " != 0 AND " + LocalDatabase.CAMPO_CANCELADO_DOC + " = 0";
                        if (DocumentModel.getIntValue(query) > 0)
                        {
                            query = "UPDATE " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " SET " + LocalDatabase.CAMPO_SURTIDO_PE + " = 1 AND "+
                            LocalDatabase.CAMPO_LISTO_PE+" = 1 WHERE " +
                            LocalDatabase.CAMPO_DOCUMENTOID_PE + " = " + pem.idDocumento;
                            saved = PedidosEncabezadoModel.createUpdateOrDeleteRecords(query);
                        }
                    }
                }
                response.valor = saved;
            });
            return response;
        }

    }
}
