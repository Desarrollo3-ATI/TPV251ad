using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using SyncTPV.Views;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using wsROMClases;
using wsROMClases.Models.Panel;

namespace SyncTPV.Controllers
{
    public class SendSomeDocsJSONService
    {
        public int peticiones = 0;
        private FormPayCart frmPayCart;

        public async Task<ExpandoObject> startActionSendDocument(String idDocumento, int method, int envioDeDatos, int peticiones, 
            List<int> idsDocList, FormPayCart frmPayCart, bool permissionPrepedido)
        {
            this.peticiones = peticiones;
            this.frmPayCart = frmPayCart; //parar aqui 1
            dynamic response = null;
            await Task.Run(async () =>
            {
                int idLocal = 0;
                String[] parts = idDocumento.Split(new Char[] { '-' });
                if (parts != null && !parts.Equals(""))
                {
                    if (parts[0] != null && !parts[0].Equals(""))
                        idLocal = Convert.ToInt32(parts[0]);
                }
                if (method == 1)
                {
                    bool serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
                    if (serverModeLAN)
                        response = await handleActionSendDocumentsLAN(method, envioDeDatos, idLocal, idsDocList);
                    else response = await handleActionSendDocumentsAPI(method, envioDeDatos, idLocal, idsDocList, permissionPrepedido);
                }
                else if (method == 2)
                {
                    response = enviarPositions(idDocumento, envioDeDatos, idsDocList);
                }
            });
            return response;
        }

        private async Task<ExpandoObject> handleActionSendDocumentsAPI(int method, int envioDeDatos, int idDocumentoLocal, List<int> idsDocList,
            bool permissionPrepedido)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                String jsonDocument = "";
                String jsonMovement = "";
                String jsonFcDocument = "";
                int idPosition = ClsPositionsModel.getIdPositionFromTheDocument(idDocumentoLocal);
                DocumentModel dvm = DocumentModel.getAllDataDocumentNotSent(idDocumentoLocal);
                if (dvm != null)
                {
                    String observation = "";
                    if (dvm.observacion != null && !dvm.observacion.Equals(""))
                    {
                        observation = dvm.observacion.Replace("\"", "");
                        observation = observation.Replace("'", "");
                    }
                    jsonDocument =
                            "{\n" +
                                    " \"claveCliente\": \"" + dvm.clave_cliente + "\",\n" +
                                    " \"clienteId\": " + dvm.cliente_id + ",\n" +
                                    " \"descuento\": " + dvm.descuento + ",\n" +
                                    " \"total\": " + (dvm.total + dvm.descuento) + ",\n" +
                                    " \"nombreUsuario\": \"" + dvm.nombreu + "\",\n" +
                                    " \"almacenId\": " + dvm.almacen_id + ",\n" +
                                    " \"anticipo\": " + dvm.anticipo + ",\n" +
                                    " \"tipoDocumento\": " + dvm.tipo_documento + ",\n" +
                                    " \"formaCobroId\": " + dvm.forma_cobro_id + ",\n" +
                                    " \"factura\": " + dvm.factura + ",\n" +
                                    " \"observacion\": \"" + observation + "\",\n" +
                                    " \"dev\": " + dvm.dev + ",\n" +
                                    " \"folioVenta\": \"" + dvm.fventa + "\",\n" +
                                    " \"fechaHora\": \"" + dvm.fechahoramov + "\",\n" +
                                    " \"usuarioId\": " + dvm.usuario_id + ",\n" +
                                    " \"formaCobroIdAbono\": " + dvm.forma_corbo_id_abono + ",\n" +
                                    " \"cIddoctoPedidoCC\": " + dvm.ciddoctopedidocc + ",\n";
                    WeightModel wm = new WeightModel();
                    if (permissionPrepedido)
                    {
                        bool isPrepedido = DocumentModel.isItDocumentFromPrepedidoSurtido(idDocumentoLocal);
                        if (isPrepedido)
                        {
                            String query = "SELECT " + LocalDatabase.CAMPO_ID_MOV + " FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " +
                                LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " + idDocumentoLocal;
                            int idMovement = MovimientosModel.getIntValue(query);
                            wm = WeightModel.getAWeight(idMovement);
                            if (wm != null)
                            {
                                jsonDocument += " \"importeExtra1\": " + wm.pesoBruto + ",\n" +
                                " \"importeExtra2\": " + wm.pesoCaja + ",\n" +
                                " \"importeExtra3\": " + wm.pesoPolloLesionado + ",\n" +
                                " \"importeExtra4\": " + wm.pesoPolloMuerto + ",\n" +
                                " \"textoExtra2\": " + wm.pesoPolloBajoDePeso + ",\n" +
                                " \"textoExtra3\": " + wm.pesoPolloGolpeado + ",\n";
                            }
                            else
                            {
                                jsonDocument += " \"importeExtra1\": " + 0 + ",\n" +
                                " \"importeExtra2\": " + 0 + ",\n" +
                                " \"importeExtra3\": " + 0 + ",\n" +
                                " \"importeExtra4\": " + 0 + ",\n" +
                                " \"textoExtra2\": \"\",\n" +
                                " \"textoExtra3\": \"\",\n";
                            }
                        }
                        else
                        {
                            jsonDocument += " \"importeExtra1\": " + 0 + ",\n" +
                                " \"importeExtra2\": " + 0 + ",\n" +
                                " \"importeExtra3\": " + 0 + ",\n" +
                                " \"importeExtra4\": " + 0 + ",\n" +
                                " \"textoExtra2\": \"\",\n" +
                                " \"textoExtra3\": \"\",\n";
                        }
                    }
                    else
                    {
                        jsonDocument += " \"importeExtra1\": " + 0 + ",\n" +
                            " \"importeExtra2\": " + 0 + ",\n" +
                            " \"importeExtra3\": " + 0 + ",\n" +
                            " \"importeExtra4\": " + 0 + ",\n" +
                            " \"textoExtra2\": \"\",\n" +
                            " \"textoExtra3\": \"\",\n";
                    }
                    List<MovimientosModel> movementsList = MovimientosModel.getAllNotSendMovimientosFromADocumnt(idDocumentoLocal, 0);
                    if (movementsList != null)
                    {
                        jsonMovement = " \"movementList\": "+ JsonConvert.SerializeObject(movementsList);
                    }
                    List<FormasDeCobroDocumentoModel> fcDocumentoList = FormasDeCobroDocumentoModel.getAllTheWaysToCollectADocument(idDocumentoLocal);
                    if (fcDocumentoList != null)
                    {
                        jsonFcDocument = ", \"fcDocumentoList\": " + JsonConvert.SerializeObject(fcDocumentoList);
                        /*jsonFcDocument = " \"fcDocumentoList\": [\n";
                        for (int i = 0; i < fcDocumentoList.Count; i++)
                        {
                            jsonFcDocument +=
                                    " { \n" +
                                            "  \"id\":" + fcDocumentoList[i].id + ",\n" +
                                            "  \"formaCobroIdAbono\":" + fcDocumentoList[i].formaCobroIdAbono + ",\n" +
                                            "  \"importe\":" + fcDocumentoList[i].importe + ",\n" +
                                            "  \"totalDocumento\":" + fcDocumentoList[i].totalDocumento + ",\n" +
                                            "  \"cambio\":" + fcDocumentoList[i].cambio + ",\n" +
                                            "  \"saldoDocumento\":" + fcDocumentoList[i].saldoDocumento + ",\n" +
                                            "  \"documentoId\":" + fcDocumentoList[i].documentoId + "";
                            if (i == (fcDocumentoList.Count - 1))
                                jsonFcDocument += "\n}\n";
                            else jsonFcDocument += "\n},\n";*/
                    } else
                    {
                        jsonFcDocument = ", \"fcDocumentoList\": []";
                    }
                    //jsonFcDocument += "\n]";
                    jsonDocument += jsonMovement + "\n" + jsonFcDocument + "\n}";
                    if (jsonDocument.Equals(""))
                    {
                        //sendEndMessage(100, "Documentos enviados!", 3, envioDeDatos, peticiones, "", idsDocList);
                        response.error = 1;
                        response.value = 100;
                        response.description = "Documentos enviados!";
                        response.method = 3;
                        response.envioDedatos = envioDeDatos;
                        response.peticiones = peticiones;
                        response.idDocumento = "";
                        response.idsDocsList = idsDocList;
                    }
                    else
                    {
                        try
                        {
                            response = await sendDataToTheServer(method, idDocumentoLocal, jsonDocument, envioDeDatos, idDocumentoLocal, idsDocList, idPosition,
                                dvm, wm, movementsList, fcDocumentoList, jsonDocument);
                        }
                        catch (Exception e)
                        {
                            SECUDOC.writeLog(e.ToString());
                            if (!jsonDocument.Equals("") && jsonMovement.Equals(""))
                            {
                                if (MovimientosModel.updateAEnviadoMovimientosDeUnDocumento(dvm.id) > 0)
                                {
                                    /** Enviar todos los movimientos de este documento */
                                    if (DocumentModel.updateAEnviadoUnDocumento(1, dvm.id) > 0)
                                    {
                                        if (DocumentModel.getDocumentType(dvm.id) == DocumentModel.TIPO_REMISION)
                                        {
                                            //sendEndMessage(50, "Documentos en el Servidor!", method, envioDeDatos,peticiones += 1, "", idsDocList);
                                            response.error = 1;
                                            response.value = 50;
                                            response.description = "Documentos en el Servidor!";
                                            response.method = method;
                                            response.envioDedatos = envioDeDatos;
                                            response.peticiones = peticiones += 1;
                                            response.idDocumento = "";
                                            response.idsDocsList = idsDocList;
                                        }
                                        else
                                        {
                                            //sendEndMessage(50, "Enviando documento...", 2, envioDeDatos,peticiones, dvm.getId() + "-" + 0, idsDocList);
                                            response.error = 1;
                                            response.value = 50;
                                            response.description = "Enviando documento...";
                                            response.method = 2;
                                            response.envioDedatos = envioDeDatos;
                                            response.peticiones = peticiones;
                                            response.idDocumento = dvm.id + "-" + 0;
                                            response.idsDocsList = idsDocList;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    //sendEndMessage(100, "Documentos enviados!", 3, envioDeDatos, peticiones, "", idsDocList);
                    response.error = 1;
                    response.value = 100;
                    response.description = "Documentos enviados!";
                    response.method = 3;
                    response.envioDedatos = envioDeDatos;
                    response.peticiones = peticiones;
                    response.idDocumento = "";
                    response.idsDocsList = idsDocList;
                }
            });
            return response;
        }

        private async Task<ExpandoObject> handleActionSendDocumentsLAN(int method, int envioDeDatos, int idDocumentoLocal, List<int> idsDocList)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                ClsDocumentoModel dm = null;
                int idPosition = ClsPositionsModel.getIdPositionFromTheDocument(idDocumentoLocal);
                DocumentModel dvm = DocumentModel.getAllDataDocumentNotSent(idDocumentoLocal);
                if (dvm != null)
                {
                    String observation = "";
                    if (dvm.observacion != null && !dvm.observacion.Equals(""))
                    {
                        observation = dvm.observacion.Replace("\"", "");
                        observation = observation.Replace("'", "");
                    }
                    dm = new ClsDocumentoModel();
                    dm.claveCliente = dvm.clave_cliente;
                    dm.clienteId = dvm.cliente_id;
                    dm.descuento = dvm.descuento;
                    dm.total = (dvm.total + dvm.descuento);
                    dm.nombreUsuario = dvm.nombreu;
                    dm.almacenId = dvm.almacen_id;
                    dm.anticipo = dvm.anticipo;
                    dm.tipoDocumento = dvm.tipo_documento;
                    dm.formaCobroId = dvm.forma_cobro_id;
                    dm.factura = dvm.factura;
                    dm.observacion = observation;
                    dm.dev = dvm.dev;
                    dm.folioVenta = dvm.fventa;
                    dm.fechaHora = dvm.fechahoramov;
                    dm.usuarioId = dvm.usuario_id;
                    dm.formaCobroIdAbono = dvm.forma_corbo_id_abono;
                    dm.cIddoctoPedidoCC = dvm.ciddoctopedidocc;
                    if (UserModel.doYouHavePermissionPrepedido())
                    {
                        bool isPrepedido = DocumentModel.isItDocumentFromPrepedidoSurtido(idDocumentoLocal);
                        if (isPrepedido)
                        {
                            String query = "SELECT " + LocalDatabase.CAMPO_ID_MOV + " FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " +
                                LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " + idDocumentoLocal;
                            int idMovement = MovimientosModel.getIntValue(query);
                            WeightModel wm = WeightModel.getAWeight(idMovement);
                            if (wm != null)
                            {
                                dm.importeExtra1 = wm.pesoBruto;
                                dm.importeExtra2 = wm.pesoCaja;
                                dm.importeExtra3 = wm.pesoPolloLesionado;
                                dm.importeExtra4 = wm.pesoPolloMuerto;
                                dm.textoExtra2 = wm.pesoPolloBajoDePeso + "";
                                dm.textoExtra3 = wm.pesoPolloGolpeado + "";
                            }
                            else
                            {
                                dm.importeExtra1 = 0;
                                dm.importeExtra2 = 0;
                                dm.importeExtra3 = 0;
                                dm.importeExtra4 = 0;
                                dm.textoExtra2 = "";
                                dm.textoExtra3 = "";
                            }
                        }
                        else
                        {
                            dm.importeExtra1 = 0;
                            dm.importeExtra2 = 0;
                            dm.importeExtra3 = 0;
                            dm.importeExtra4 = 0;
                            dm.textoExtra2 = "";
                            dm.textoExtra3 = "";
                        }
                    }
                    else
                    {
                        dm.importeExtra1 = 0;
                        dm.importeExtra2 = 0;
                        dm.importeExtra3 = 0;
                        dm.importeExtra4 = 0;
                        dm.textoExtra2 = "";
                        dm.textoExtra3 = "";
                    }
                    List<ClsMovimientosModel> movementsToSend = new List<ClsMovimientosModel>();
                    ClsMovimientosModel movement = null;
                    List<MovimientosModel> movementsList = MovimientosModel.getAllNotSendMovimientosFromADocumnt(idDocumentoLocal, 0);
                    if (movementsList != null)
                    {
                        for (int i = 0; i < movementsList.Count; i++)
                        {
                            String observationMov = "";
                            if (!movementsList[i].observations.Equals(""))
                            {
                                observationMov = movementsList[i].observations.Replace("\"", "");
                                observationMov = observationMov.Replace("'", "");
                            }
                            movement = new ClsMovimientosModel();
                            movement.documentId = movementsList[i].documentId;
                            movement.itemCode = movementsList[i].itemCode;
                            movement.itemId = movementsList[i].itemId;
                            movement.baseUnits = movementsList[i].baseUnits;
                            movement.nonConvertibleUnits = movementsList[i].nonConvertibleUnits;
                            movement.capturedUnits = movementsList[i].capturedUnits;
                            movement.capturedUnitId = movementsList[i].capturedUnitId;
                            movement.nonConvertibleUnitId = movementsList[i].nonConvertibleUnitId;
                            movement.capturedUnitType = movementsList[i].capturesUnitsType;
                            movement.price = movementsList[i].price;
                            movement.total = movementsList[i].total;
                            movement.position = movementsList[i].position;
                            movement.documentType = movementsList[i].documentType;
                            movement.nameUser = movementsList[i].nameUser;
                            movement.invoice = movementsList[i].invoice;
                            movement.discount = (movementsList[i].descuentoPorcentaje + movementsList[i].rateDiscountPromo);
                            movement.observations = observationMov;
                            movement.iddev = movementsList[i].idDev;
                            movement.comments = "";
                            movement.userId = movementsList[i].userId;
                            movementsToSend.Add(movement);
                        }
                    }
                    dm.movementList = movementsToSend;
                    List<FormaCobroDocumentoModel> formasCobrosList = new List<FormaCobroDocumentoModel>();
                    FormaCobroDocumentoModel formaCobroDocumento = null;
                    List<FormasDeCobroDocumentoModel> fcDocumentoList = FormasDeCobroDocumentoModel.getAllTheWaysToCollectADocument(idDocumentoLocal);
                    if (fcDocumentoList != null)
                    {
                        formasCobrosList = new List<FormaCobroDocumentoModel>();
                        for (int i = 0; i < fcDocumentoList.Count; i++)
                        {
                            formaCobroDocumento = new FormaCobroDocumentoModel();
                            formaCobroDocumento.id = fcDocumentoList[i].id;
                            formaCobroDocumento.formaCobroIdAbono = fcDocumentoList[i].formaCobroIdAbono;
                            formaCobroDocumento.importe = fcDocumentoList[i].importe;
                            formaCobroDocumento.totalDocumento = fcDocumentoList[i].totalDocumento;
                            formaCobroDocumento.cambio = fcDocumentoList[i].cambio;
                            formaCobroDocumento.saldoDocumento = fcDocumentoList[i].saldoDocumento;
                            formaCobroDocumento.documentoId = fcDocumentoList[i].documentoId;
                            formasCobrosList.Add(formaCobroDocumento);
                        }
                    }
                    dm.fcDocumentoList = formasCobrosList;
                    if (dm == null)
                    {
                        response.error = 1;
                        response.value = 100;
                        response.description = "Documentos enviados!";
                        response.method = 3;
                        response.envioDedatos = envioDeDatos;
                        response.peticiones = peticiones;
                        response.idDocumento = "";
                        response.idsDocsList = idsDocList;
                    }
                    else
                    {
                        try
                        {
                            response = await sendDataToTheServerLAN(dm, idDocumentoLocal, idPosition, idsDocList, method, 
                                envioDeDatos);
                        }
                        catch (Exception e)
                        {
                            SECUDOC.writeLog(e.ToString());
                            if (dm != null && dm.movementList == null)
                            {
                                if (MovimientosModel.updateAEnviadoMovimientosDeUnDocumento(dvm.id) > 0)
                                {
                                    /** Enviar todos los movimientos de este documento */
                                    if (DocumentModel.updateAEnviadoUnDocumento(1, dvm.id) > 0)
                                    {
                                        if (DocumentModel.getDocumentType(dvm.id) == DocumentModel.TIPO_REMISION)
                                        {
                                            response.error = 1;
                                            response.value = 50;
                                            response.description = "Documentos en el Servidor!";
                                            response.method = method;
                                            response.envioDedatos = envioDeDatos;
                                            response.peticiones = peticiones += 1;
                                            response.idDocumento = "";
                                            response.idsDocsList = idsDocList;
                                        }
                                        else
                                        {
                                            response.error = 1;
                                            response.value = 50;
                                            response.description = "Enviando documento...";
                                            response.method = 2;
                                            response.envioDedatos = envioDeDatos;
                                            response.peticiones = peticiones;
                                            response.idDocumento = dvm.id + "-" + 0;
                                            response.idsDocsList = idsDocList;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    response.error = 1;
                    response.value = 100;
                    response.description = "Documentos enviados!";
                    response.method = 3;
                    response.envioDedatos = envioDeDatos;
                    response.peticiones = peticiones;
                    response.idDocumento = "";
                    response.idsDocsList = idsDocList;
                }
            });
            return response;
        }

        private class ResponseJSONDocs
        {
            public int value { get; set; }
            public String description { get; set; }
            public int idDocumento { get; set; }
            public int iddev { get; set; }
            public List<ResponseFcDocuments> formasDePagoList { get; set; }
        }

        private class ResponseFcDocuments
        {
            public int idFcDocApp { get; set; }
            public int idFcDocServer { get; set; }
        }

        private async Task<ExpandoObject> sendDataToTheServer(int method, int idDocLocal, String dataJSON, int envioDeDatos, int idDocumento, List<int> idsDocList, 
            int idPosition, DocumentModel dvm, WeightModel wm, List<MovimientosModel> movementsList, List<FormasDeCobroDocumentoModel> fcDocumentoList,
            String jsonToSend)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                try
                {
                    int itemsToEvaluate = 0;
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/insertarDocumentosJSON", Method.Post);
                    request.AddJsonBody(jsonToSend);
                    /*request.AddJsonBody(new
                    {
                        claveCliente = dvm.clave_cliente,
                        clienteId = dvm.cliente_id,
                        descuento = dvm.descuento,
                        total = (dvm.total + dvm.descuento),
                        nombreUsuario = dvm.nombreu,
                        almacenId = dvm.almacen_id,
                        anticipo = dvm.anticipo,
                        tipoDocumento = dvm.tipo_documento,
                        formaCobroId = dvm.forma_cobro_id,
                        factura = dvm.factura,
                        observacion = dvm.observacion,
                        dev = dvm.dev,
                        folioVenta = dvm.fventa,
                        fechaHora = dvm.fechahoramov,
                        usuarioId = dvm.usuario_id,
                        formaCobroIdAbono = dvm.forma_corbo_id_abono,
                        cIddoctoPedidoCC = dvm.ciddoctopedidocc,
                        importeExtra1 = wm.pesoBruto,
                        importeExtra2 = wm.pesoCaja,
                        importeExtra3 = wm.pesoPolloLesionado,
                        importeExtra4 = wm.pesoPolloMuerto,
                        textoExtra2 = wm.pesoPolloBajoDePeso,
                        textoExtra3 = wm.pesoPolloGolpeado,
                        movementList = movementsList,
                        fcDocumentoList = fcDocumentoList
                    });*/
                    var responseHeader = client.ExecutePostAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content; // Raw content as string
                        ResponseJSONDocs responseDocument = JsonConvert.DeserializeObject<ResponseJSONDocs>(content);
                        if (responseDocument.value == 1)
                        {
                            response = validateResponse(responseDocument.idDocumento, responseDocument.formasDePagoList, responseDocument.description,
                                idDocLocal, idPosition, idsDocList, method, envioDeDatos);
                        } else
                        {
                            response.error = responseDocument.value;
                            response.value = 100;
                            response.description = responseDocument.description;
                            response.method = 3;
                            response.envioDedatos = envioDeDatos;
                            response.peticiones = peticiones;
                            response.idDocumento = "";
                            response.idsDocsList = idsDocList;
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        response.error = -400;
                        response.value = 100;
                        response.description = MetodosGenerales.getErrorMessageFromNetworkCode(-400, responseHeader.Result.ErrorException.Message);
                        response.method = 3;
                        response.envioDedatos = envioDeDatos;
                        response.peticiones = peticiones;
                        response.idDocumento = "";
                        response.idsDocsList = idsDocList;
                    } else if (responseHeader.Result.ResponseStatus == ResponseStatus.TimedOut)
                    {
                        response.error = -404;
                        response.value = 100;
                        response.description = MetodosGenerales.getErrorMessageFromNetworkCode(-404, responseHeader.Result.ErrorException.Message);
                        response.method = 3;
                        response.envioDedatos = envioDeDatos;
                        response.peticiones = peticiones;
                        response.idDocumento = "";
                        response.idsDocsList = idsDocList;
                    } else
                    {
                        response.error = -500;
                        response.value = 100;
                        response.description = MetodosGenerales.getErrorMessageFromNetworkCode(-500, responseHeader.Result.ErrorException.Message);
                        response.method = 3;
                        response.envioDedatos = envioDeDatos;
                        response.peticiones = peticiones;
                        response.idDocumento = "";
                        response.idsDocsList = idsDocList;
                    }
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    response.error = -1;
                    response.value = 100;
                    response.description = e.Message;
                    response.method = 3;
                    response.envioDedatos = envioDeDatos;
                    response.peticiones = peticiones;
                    response.idDocumento = "";
                    response.idsDocsList = idsDocList;
                }
            });
            return response;
        }

        private async Task<ExpandoObject> sendDataToTheServerLAN(ClsDocumentoModel dm, int idLocal, int idPosition, List<int> idsDocList, int method, int envioDeDatos)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                try
                {
                    int itemsToEvaluate = 0;
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    dynamic respuesta = ClsDocumentoModel.insertNewDocumentLogic(panelInstance, dm);
                    response = await validateResponseLAN(respuesta.response, idLocal, idPosition, idsDocList, method, envioDeDatos);
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    response.error = 1;
                    response.value = 100;
                    response.description = e.ToString();
                    response.method = 3;
                    response.envioDedatos = envioDeDatos;
                    response.peticiones = peticiones;
                    response.idDocumento = "";
                    response.idsDocsList = idsDocList;
                }
            });
            return response;
        }

        private ExpandoObject validateResponse(int idDocumentoPanel, List<ResponseFcDocuments> jsonArrayFcDocs, 
            String description, int idDocLocal, int idPosition, List<int> idsDocList, int method, int envioDeDatos)
        {
            dynamic response = new ExpandoObject();
            try
            {
                for (int j = 0; j < jsonArrayFcDocs.Count; j++)
                {
                    int idFcDocApp = jsonArrayFcDocs[j].idFcDocApp;
                    int idFcDocServer = jsonArrayFcDocs[j].idFcDocServer;
                    FormasDeCobroDocumentoModel.updateIdServerInAFcDocument(idFcDocApp, idFcDocServer);
                }
                if (idDocumentoPanel > 0)
                {
                    ClsPositionsModel.updateIdDoctoPanelPosition(idPosition, idDocumentoPanel);
                    if (DocumentModel.addIdWebServiceToTheDocument(idDocLocal, idDocumentoPanel) > 0)
                    {
                        if (MovimientosModel.updateAEnviadoMovimientosDeUnDocumento(idDocLocal) > 0)
                        {
                            int documentType = DocumentModel.getDocumentType(idDocLocal);
                            /** Enviar todos los movimientos de este documento */
                            if (DocumentModel.updateAEnviadoUnDocumento(1, idDocLocal) > 0)
                            {
                                if (documentType == DocumentModel.TIPO_REMISION)
                                {
                                    //sendEndMessage(50, "Documentos en el Servidor!", method, envioDeDatos,peticiones += 1, "", idsDocList);
                                    response.error = 1;
                                    response.value = 50;
                                    response.description = "Documentos en el Servidor!";
                                    response.method = method;
                                    response.envioDedatos = envioDeDatos;
                                    response.peticiones = peticiones += 1;
                                    response.idDocumento = "";
                                    response.idsDocsList = idsDocList;
                                }
                                else
                                {
                                    //sendEndMessage(50, "Enviando documento...", 2, envioDeDatos,peticiones, idDocLocal + "-" + idDocPanel, idsDocList);
                                    response.error = 1;
                                    response.value = 50;
                                    response.description = "Enviando documento...";
                                    response.method = 2;
                                    response.envioDedatos = envioDeDatos;
                                    response.peticiones = peticiones;
                                    response.idDocumento = idDocLocal + "-" + idDocumentoPanel;
                                    response.idsDocsList = idsDocList;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (description.Equals("Documento guardado anteriormente"))
                    {
                        if (MovimientosModel.updateAEnviadoMovimientosDeUnDocumento(idDocLocal) > 0)
                        {
                            int documentType = DocumentModel.getDocumentType(idDocLocal);
                            if (documentType == DocumentModel.TIPO_COTIZACION_MOSTRADOR)
                            {

                            }
                            /** Enviar todos los movimientos de este documento */
                            if (DocumentModel.updateAEnviadoUnDocumento(1, idDocLocal) > 0)
                            {
                                if (documentType == DocumentModel.TIPO_REMISION)
                                {
                                    //sendEndMessage(50, "Documentos en el Servidor!", method, envioDeDatos,peticiones += 1, "", idsDocList);
                                    response.error = 1;
                                    response.value = 50;
                                    response.description = "Documentos en el Servidor!";
                                    response.method = method;
                                    response.envioDedatos = envioDeDatos;
                                    response.peticiones = peticiones += 1;
                                    response.idDocumento = "";
                                    response.idsDocsList = idsDocList;
                                }
                                else
                                {
                                    //sendEndMessage(50, "Enviando documento...", 2, envioDeDatos,peticiones, idDocLocal + "-" + idDocPanel, idsDocList);
                                    response.error = 1;
                                    response.value = 50;
                                    response.description = "Enviando documento...";
                                    response.method = 3;
                                    response.envioDedatos = envioDeDatos;
                                    response.peticiones = peticiones;
                                    response.idDocumento = idDocLocal + "-" + idDocumentoPanel;
                                    response.idsDocsList = idsDocList;
                                }
                            }
                        }
                    }
                    else
                    {
                        //GeneralMethods.Companion.deleteCache(context);
                        //sendEndMessage(100, "Respuesta Incorrecta!", 3, envioDeDatos, peticiones,"", idsDocList);
                        response.error = 1;
                        response.value = 100;
                        response.description = "Documento actualizado en el servidor!";
                        response.method = 3;
                        response.envioDedatos = envioDeDatos;
                        response.peticiones = peticiones;
                        response.idDocumento = "";
                        response.idsDocsList = idsDocList;
                    }
                }
            }
            catch (Exception e)
            {
                //sendEndMessage(100, "Respuesta Incorrecta!", 3, envioDeDatos, peticiones,"", idsDocList);
                response.error = -1;
                response.value = 100;
                response.description = e.Message;
                response.method = 3;
                response.envioDedatos = envioDeDatos;
                response.peticiones = peticiones;
                response.idDocumento = "";
                response.idsDocsList = idsDocList;
                SECUDOC.writeLog(e.ToString());
            }
            return response;
        }

        private async Task<ExpandoObject> validateResponseLAN(dynamic respuesta, int idDocLocal, int idPosition, List<int> idsDocList, int method, int envioDeDatos)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                try
                {
                    int idDocPanel = respuesta[0].valor;
                    List<ExpandoObject> jsonArrayFcDocs = respuesta[0].fcDocIds;
                    for (int j = 0; j < jsonArrayFcDocs.Count; j++)
                    {
                        dynamic formaDeCobroSaved = jsonArrayFcDocs[j];
                        int idFcDocApp = formaDeCobroSaved.idFcDocApp;
                        int idFcDocServer = formaDeCobroSaved.idFcDocServer;
                        FormasDeCobroDocumentoModel.updateIdServerInAFcDocument(idFcDocApp, idFcDocServer);
                    }
                    if (idDocPanel > 0)
                    {
                        ClsPositionsModel.updateIdDoctoPanelPosition(idPosition, idDocPanel);
                        if (DocumentModel.addIdWebServiceToTheDocument(idDocLocal, idDocPanel) > 0)
                        {
                            if (MovimientosModel.updateAEnviadoMovimientosDeUnDocumento(idDocLocal) > 0)
                            {
                                int documentType = DocumentModel.getDocumentType(idDocLocal);
                                /** Enviar todos los movimientos de este documento */
                                if (DocumentModel.updateAEnviadoUnDocumento(1, idDocLocal) > 0)
                                {
                                    if (documentType == DocumentModel.TIPO_REMISION)
                                    {
                                        response.error = 1;
                                        response.value = 50;
                                        response.description = "Documentos en el Servidor!";
                                        response.method = method;
                                        response.envioDedatos = envioDeDatos;
                                        response.peticiones = peticiones += 1;
                                        response.idDocumento = "";
                                        response.idsDocsList = idsDocList;
                                    }
                                    else
                                    {
                                        response.error = 1;
                                        response.value = 50;
                                        response.description = "Enviando documento...";
                                        response.method = 2;
                                        response.envioDedatos = envioDeDatos;
                                        response.peticiones = peticiones;
                                        response.idDocumento = idDocLocal + "-" + idDocPanel;
                                        response.idsDocsList = idsDocList;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (respuesta[0].descripcion.Equals("Documento guardado anteriormente"))
                        {
                            if (MovimientosModel.updateAEnviadoMovimientosDeUnDocumento(idDocLocal) > 0)
                            {
                                int documentType = DocumentModel.getDocumentType(idDocLocal);
                                if (documentType == DocumentModel.TIPO_COTIZACION_MOSTRADOR)
                                {

                                }
                                /** Enviar todos los movimientos de este documento */
                                if (DocumentModel.updateAEnviadoUnDocumento(1, idDocLocal) > 0)
                                {
                                    if (documentType == DocumentModel.TIPO_REMISION)
                                    {
                                        response.error = 1;
                                        response.value = 50;
                                        response.description = "Documentos en el Servidor!";
                                        response.method = method;
                                        response.envioDedatos = envioDeDatos;
                                        response.peticiones = peticiones += 1;
                                        response.idDocumento = "";
                                        response.idsDocsList = idsDocList;
                                    }
                                    else
                                    {
                                        response.error = 1;
                                        response.value = 50;
                                        response.description = "Enviando documento...";
                                        response.method = 3;
                                        response.envioDedatos = envioDeDatos;
                                        response.peticiones = peticiones;
                                        response.idDocumento = idDocLocal + "-" + idDocPanel;
                                        response.idsDocsList = idsDocList;
                                    }
                                }
                            }
                        }
                        else
                        {
                            response.error = 1;
                            response.value = 100;
                            response.description = "Documento actualizado en el servidor!";
                            response.method = 3;
                            response.envioDedatos = envioDeDatos;
                            response.peticiones = peticiones;
                            response.idDocumento = "";
                            response.idsDocsList = idsDocList;
                        }
                    }
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog("Error JSONException: " + e.ToString());
                    response.error = 1;
                    response.value = 100;
                    response.description = "Error al procesar la respuesta: " + e.ToString();
                    response.method = 3;
                    response.envioDedatos = envioDeDatos;
                    response.peticiones = peticiones;
                    response.idDocumento = "";
                    response.idsDocsList = idsDocList;
                }
            });
            return response;
        }

        private ExpandoObject enviarPositions(String idDocumento, int envioDeDatos, List<int> idsDocList)
        {
            dynamic response = new ExpandoObject();
            String[] parts = idDocumento.Split(new Char[] { '-' });
            int idWs = 0;
            if (parts != null && !parts.Equals(""))
            {
                if (parts[1] != null && !parts[1].Equals(""))
                    idWs = Convert.ToInt32(parts[1]);
            }
            ClsPositionsModel pm = ClsPositionsModel.getAllDataForAPositionIdServer(idWs);
            if (pm != null)
            {
                UserModel user = UserModel.getNameIdAndRutaOfTheUser();
                response = sendLocationToTheWsConPermiso(pm.latitude, pm.longitude, pm.customerId, pm.date, pm.documentType, user.id,
                        user.Ruta, pm.idDoctoPanel, envioDeDatos, idDocumento, idWs, idsDocList);
            }
            else
            {
                //sendEndMessage(20, "Datos enviados!", 1, envioDeDatos, peticiones += 1, "", idsDocList);
                response.error = 1;
                response.value = 20;
                response.description = "Datos enviados!";
                response.method = 1;
                response.envioDedatos = envioDeDatos;
                response.peticiones = peticiones += 1;
                response.idDocumento = "";
                response.idsDocsList = idsDocList;
            }
            return response;
        }

        public ExpandoObject sendLocationToTheWsConPermiso(double latitude, double longitude, int idCliente, String fh, int tipoDoc, int idAgente, String ruta, int idDocto,
                                              int envioDeDatos, String idDocumento, int idWs, List<int> idsDocList)
        {
            dynamic response = new ExpandoObject();
            try
            {
                String url = ConfiguracionModel.getLinkWs() + "/insertar_gps?LAT=" + latitude + "&LON=" + longitude + "&CLIENTE_ID=" + idCliente + "&FECHA=" + fh + "&TIPO_MOV=" + tipoDoc + "&VENDEDOR_ID=" + idAgente +
                    "&RUTA=" + ruta + "&idDocto=" + idDocto;
                url = url.Replace(" ", "%20");
                var client = new RestClient();
                // client.Authenticator = new HttpBasicAuthenticator(username, password);
                var request = new RestRequest();
                var responseHeader = client.GetAsync(request);
                if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                {
                    var content = responseHeader.Result.Content; // Raw content as string
                    //var responseHttp = client.Get<String>(request);
                    var jsonResp = JsonConvert.DeserializeObject<List<clsAgentes>>(content);
                    List<clsAgentes> jsonUsers = (List<clsAgentes>)jsonResp;
                    //GeneralMethods.Companion.deleteCache(context);
                    if (ClsPositionsModel.updatePosicionEnviadaEnviarDocumentosConPermiso(idWs) > 0)
                    {
                        //sendEndMessage(95, "Enviando ubicaciones...", 1, envioDeDatos, peticiones += 1, idDocumento, idsDocList);
                        response.error = 1;
                        response.value = 95;
                        response.description = "Enviando ubicaciones...";
                        response.method = 1;
                        response.envioDedatos = envioDeDatos;
                        response.peticiones = peticiones += 1;
                        response.idDocumento = idDocumento;
                        response.idsDocsList = idsDocList;
                    }
                }
                else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                {
                    if (responseHeader.Result.ErrorMessage.Equals("Unable to connect to the remote server"))
                    {
                        response.error = 404;
                    }
                    else
                    {
                        response.error = 404;
                    }
                }

            }
            catch (Exception ex)
            {
                SECUDOC.writeLog("Exception: " + ex.Message);
            }
            return response;
        }

    }
}
