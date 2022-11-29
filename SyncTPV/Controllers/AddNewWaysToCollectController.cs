using SyncTPV.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace SyncTPV.Controllers
{
    public class AddNewWaysToCollectController
    {
        public async Task<int> doInBackground(dynamic maps)
        {
            int response = 0;
            await Task.Run(async () =>
            {
                int fcId = maps.fcId;
                double importe = maps.importe;
                int idDocument = maps.idDocument;
                int fcIdDocument = DocumentModel.getPaymentMethodForADocument(idDocument);
                int cobrado = 0;
                
                if (fcIdDocument == DocumentModel.FORMA_PAGO_CREDITO)
                {
                    double beforeAdvance = DocumentModel.getDocumentAdvance(idDocument);
                    double beforeTotal = DocumentModel.getTotalForADocumentWithContext(idDocument);
                    double newPending = beforeTotal - beforeAdvance;
                    if (importe >= newPending)
                    {
                        response = 0;
                    }
                    else
                    {
                        String totalDocTwoDecText = Convert.ToString(beforeTotal);
                        if (importe == Convert.ToDouble(totalDocTwoDecText))
                        {
                            cobrado = FormasDeCobroDocumentoModel.createUpdateOrDeleteFcDocument(idDocument, fcId, beforeTotal);
                        }
                        else
                        {
                            cobrado = FormasDeCobroDocumentoModel.createUpdateOrDeleteFcDocument(idDocument, fcId, importe);
                        }
                        int fcIdHigher = FormasDeCobroDocumentoModel.getFcWithHigherAmount(idDocument);
                        if (DocumentModel.updateTheIdOfTheSubscriptionPaymentMethod(idDocument, fcIdHigher))
                        {
                            if (fcIdHigher > 0)
                            {
                                double pending = FormasDeCobroDocumentoModel.getSaldoPendienteOfTheLastFcDcoument(idDocument);
                                double totalDocument1 = DocumentModel.getTotalForADocumentWithContext(idDocument);
                                if (pending > 0)
                                {
                                    double advance = totalDocument1 - pending;
                                    if (DocumentModel.updateDocumentAdvance(idDocument, advance))
                                    {
                                        response = validateResponse(cobrado);
                                    }
                                }
                                else
                                {
                                    if (DocumentModel.updateDocumentAdvance(idDocument, totalDocument1))
                                    {
                                        response = validateResponse(cobrado);
                                    }
                                }
                            }
                            else
                            {
                                if (DocumentModel.updateDocumentAdvance(idDocument, 0))
                                {
                                    response = validateResponse(cobrado);
                                }
                            }
                        }
                    }
                }
                else
                {
                    bool existe= FormasDeCobroDocumentoModel.checkIfExistWithFcId(fcId, idDocument);
                    double v1 = 0.0;
                    double v2 = 0.0;
                    double totalv1 = DocumentModel.getTotalForADocumentWithContext(idDocument);
                    if (existe)
                    {
                        FormasDeCobroDocumentoModel.updateRecalculoBD(
                             fcId, idDocument, importe, v1, v2
                            );
                        
                    }
                    else
                    {
                        FormasDeCobroDocumentoModel.addNewFcDocument(idDocument, fcId, totalv1, importe, v1, v2);
                    }
                    bool a = false;
                    try
                    {
                       a = FormasDeCobroDocumentoModel.recalculoFormasCobroDocumento(idDocument);
                    }
                    catch(Exception e)
                    {
                        SECUDOC.writeLog(e.ToString());
                    }
                    /*double beforeTotal = DocumentModel.getTotalForADocumentWithContext(idDocument);
                    String totalDocTwoDecText = beforeTotal + "";
                    if (importe == Convert.ToDouble(totalDocTwoDecText))
                    {
                        cobrado = FormasDeCobroDocumentoModel.createUpdateOrDeleteFcDocument(idDocument, fcId, beforeTotal);
                    }
                    else
                    {
                        cobrado = FormasDeCobroDocumentoModel.createUpdateOrDeleteFcDocument(idDocument, fcId, importe);
                    }*/
                    int fcIdHigher = FormasDeCobroDocumentoModel.getFcWithHigherAmount(idDocument);
                    if (DocumentModel.updatePaymentFormIdWithHigherAmount(idDocument, fcIdHigher))
                    {
                        if (fcIdHigher > 0)
                        {
                            double pending = FormasDeCobroDocumentoModel.getSaldoPendienteOfTheLastFcDcoument(idDocument);
                            double totalDocument1 = DocumentModel.getTotalForADocumentWithContext(idDocument);
                            if (pending > 0)
                            {
                                double advance = totalDocument1 - pending;
                                advance = MetodosGenerales.obtieneDosDecimales(advance);
                                if (DocumentModel.updateDocumentAdvance(idDocument, advance))
                                {
                                    response = validateResponse(cobrado);
                                }
                            }
                            else
                            {
                                totalDocument1 = MetodosGenerales.obtieneDosDecimales(totalDocument1);
                                if (DocumentModel.updateDocumentAdvance(idDocument, totalDocument1))
                                {
                                    response = validateResponse(cobrado);
                                }
                            }
                        }
                        else
                        {
                            if (DocumentModel.updateDocumentAdvance(idDocument, 0))
                            {
                                response = validateResponse(cobrado);
                            }
                        }
                    }
                    
                }

            });
            return response;
        }

        private int validateResponse(int cobrado)
        {
            int response = 0;
            if (cobrado == -1)
            {
                response = -1;
            }
            else if (cobrado >= 0)
            {
                response = 1;
            }
            return response;
        }
    }
}
