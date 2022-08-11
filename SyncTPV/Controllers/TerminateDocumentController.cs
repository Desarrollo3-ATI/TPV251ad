using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using wsROMClase;

namespace SyncTPV.Controllers
{
    public class TerminateDocumentController
    {
        public static readonly int TERMINATE_DOCUMENT_ROM = 0;
        private int idCustomer = 0;

        public async Task<ExpandoObject> doInBackground(int call, int idDocument, bool cotizacionMostrador, 
            bool serverModeLAN, int positionFiscalItemField)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                if (call == TERMINATE_DOCUMENT_ROM)
                {
                    idCustomer = DocumentModel.getCustomerIdOfADocument(idDocument);
                    int documentType = DocumentModel.getDocumentType(idDocument);
                    if (documentType == DocumentModel.TIPO_VENTA || documentType == DocumentModel.TIPO_REMISION || 
                    documentType == DocumentModel.TIPO_DEVOLUCION)
                    {
                        int fpId = DocumentModel.getPaymentMethodForADocument(idDocument);
                        if (fpId == DocumentModel.FORMA_PAGO_CREDITO)
                        {
                            if (DocumentModel.verifyThatTheAdvanceIsLessThanTheTotal(idDocument))
                            {
                                response.valor = 1;
                            }
                            else
                            {
                                response.valor = 0;
                            }
                        } else if (fpId != 0 && fpId != DocumentModel.FORMA_PAGO_CREDITO)
                        {
                            if (DocumentModel.verifyThatTheAdvanceIsLessThanTheTotal(idDocument))
                            {
                                response.valor = 2;
                            }
                            else
                            {
                                response.valor = 1;
                            }
                        } else
                        {
                            if (DocumentModel.verifyThatTheAdvanceIsLessThanTheTotal(idDocument))
                            {
                                response.valor = 2;
                            }
                            else
                            {
                                response.valor = 1;
                            }
                        }
                    }
                    else if (documentType == DocumentModel.TIPO_COTIZACION)
                    {
                        if (cotizacionMostrador)
                        {
                            DocumentModel.updateQuoteDocumentType(idDocument, 51);
                            int fiscalProduct = 0;
                            List<MovimientosModel> movementsList = MovimientosModel.getAllMovementsFromADocument(idDocument);
                            for (int i = 0; i < movementsList.Count; i++)
                            {
                                if (serverModeLAN)
                                {
                                    bool useFiscalField = await ConfigurationsTpvController.checkIfUseFiscalValueActivated();
                                    if (useFiscalField)
                                    {//use fiscal o no por compac
                                        String comInstance = InstanceSQLSEModel.getStringComInstance();
                                        ClsItemModel im = ItemModel.getAllDataFromAnItemLAN(comInstance, movementsList[i].itemCode);
                                        bool isFiscal = await ItemModel.getFiscalItemFieldValue(im, positionFiscalItemField);
                                        if (isFiscal)
                                        {
                                            MovimientosModel.updateFacturaField(movementsList[i].id, "S");
                                            fiscalProduct++;
                                        } else
                                        {
                                            MovimientosModel.updateFacturaField(movementsList[i].id, "N");
                                        }
                                    }
                                    else
                                    {
                                        MovimientosModel.updateFacturaField(movementsList[i].id, "S");
                                        fiscalProduct++;
                                    }
                                } else {
                                    bool useFiscalField = await ConfigurationsTpvController.checkIfUseFiscalValueActivated();
                                    if (useFiscalField)
                                    {
                                        ClsItemModel im = ItemModel.getAllDataFromAnItem(movementsList[i].itemId);
                                        bool isFiscal = await ItemModel.getFiscalItemFieldValue(im, positionFiscalItemField);
                                        if (isFiscal)
                                        {
                                            MovimientosModel.updateFacturaField(movementsList[i].id, "S");
                                            fiscalProduct++;
                                        }
                                        else
                                        {
                                            MovimientosModel.updateFacturaField(movementsList[i].id, "N");
                                        }
                                    } else
                                    {
                                        MovimientosModel.updateFacturaField(movementsList[i].id, "S");
                                        fiscalProduct++;
                                    }
                                }
                            }
                            String query = "SELECT "+LocalDatabase.CAMPO_FACTURA_DOC+" FROM "+LocalDatabase.TABLA_DOCUMENTOVENTA+" WHERE "+
                            LocalDatabase.CAMPO_ID_DOC+" = "+idDocument;
                            int facturarCotizacion = DocumentModel.getIntValue(query);
                            if (fiscalProduct > 0 && facturarCotizacion != DocumentModel.FISCAL_FACTURAR)
                                DocumentModel.updateGenerarFactura(idDocument, 1);
                        }
                        double totalDocument = DocumentModel.getTotalForADocumentWithContext(idDocument);
                        if (DocumentModel.updateDocumentAdvance(idDocument, totalDocument))
                        {
                            if (DocumentModel.verifyThatTheAdvanceIsLessThanTheTotal(idDocument))
                            {
                                response.valor = 2;
                            }
                            else
                            {
                                response.valor = 1;
                            }
                        }
                    } else if (documentType == DocumentModel.TIPO_PEDIDO)
                    {
                        double totalDocument = DocumentModel.getTotalForADocumentWithContext(idDocument);
                        if (DocumentModel.updateDocumentAdvance(idDocument, totalDocument))
                        {
                            if (DocumentModel.verifyThatTheAdvanceIsLessThanTheTotal(idDocument))
                            {
                                response.valor = 2;
                            }
                            else
                            {
                                response.valor = 1;
                            }
                        }
                    } else if (documentType == DocumentModel.TIPO_PREPEDIDO)
                    {
                        double totalDocument = DocumentModel.getTotalForADocumentWithContext(idDocument);
                        if (DocumentModel.updateDocumentAdvance(idDocument, totalDocument))
                        {
                            if (DocumentModel.verifyThatTheAdvanceIsLessThanTheTotal(idDocument))
                            {
                                response.valor = 2;
                            }
                            else
                            {
                                response.valor = 1;
                            }
                        }
                    }
                }
                response.idCustomer = idCustomer;
            });
            return response;
        }

    }
}
