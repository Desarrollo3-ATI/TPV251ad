using SyncTPV.Models;
using System;
using System.Dynamic;
using System.Threading.Tasks;
using wsROMClase;

namespace SyncTPV.Controllers
{
    public class ClsChangeDocumentTypeController
    {
        private Boolean documentTypeUpdated = false;

        public async Task<ExpandoObject> doInBackground(int idDocumento, int tipoDeDocumento, bool serverModeLAN, bool webActive, String codigoCaja)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                int documentType = 0;
                String btnText = "";
                String description = "";
                bool btnTerminateVisible = false;
                int positionSpinner = 0;
                try
                {
                    if (idDocumento > 0)
                    {
                        if (await DocumentModel.updateTypeFromADocument(idDocumento, DocumentModel.getDocumentType(idDocumento), tipoDeDocumento, 
                            serverModeLAN, webActive, codigoCaja))
                        {
                            if (DocumentModel.updatePaymentFormIdAndCreditId(idDocumento, 0, 0, 0))
                            {
                                FormasDeCobroDocumentoModel.removePendingBalanceToTheLastFormOfCollectionOfTheDocument(idDocumento);
                            }
                            documentTypeUpdated = true;
                        }
                        else
                        {
                            documentTypeUpdated = false;
                        }
                        if (documentTypeUpdated)
                        {
                            value = 1;
                            if (tipoDeDocumento == DocumentModel.TIPO_COTIZACION)
                            {
                                documentType = 1;
                                btnText = "Terminar";
                                btnTerminateVisible = false;
                                positionSpinner = 1;
                            }
                            else if (tipoDeDocumento == DocumentModel.TIPO_VENTA)
                            {
                                documentType = 2;
                                btnText = "Cobrar";
                                btnTerminateVisible = true;
                                positionSpinner = 0;
                            }
                            else if (tipoDeDocumento == DocumentModel.TIPO_REMISION)
                            {
                                documentType = 4;
                                btnText = "Cobrar";
                                btnTerminateVisible = true;
                                positionSpinner = 0;
                            }
                            else if (tipoDeDocumento == DocumentModel.TIPO_PEDIDO)
                            {
                                documentType = 3;
                                btnText = "Terminar";
                                btnTerminateVisible = false;
                                positionSpinner = 2;
                            }
                            else if (tipoDeDocumento == DocumentModel.TIPO_DEVOLUCION)
                            {
                                documentType = 5;
                                btnText = "Terminar";
                                btnTerminateVisible = true;
                                positionSpinner = 3;
                            }
                            description = "Cambio exitoso";
                        }
                        else
                        {
                            value = -2;
                            description = "No se pudo realizar la conversión validar existencias de los productos!";
                            if (DocumentModel.getDocumentType(idDocumento) == DocumentModel.TIPO_COTIZACION)
                            {
                                documentType = 1;
                                btnText = "Terminar";
                                btnTerminateVisible = false;
                                positionSpinner = 1;
                            }
                            else if (DocumentModel.getDocumentType(idDocumento) == DocumentModel.TIPO_VENTA)
                            {
                                documentType = 2;
                                btnText = "Cobrar";
                                btnTerminateVisible = true;
                                positionSpinner = 0;
                            }
                            else if (DocumentModel.getDocumentType(idDocumento) == DocumentModel.TIPO_REMISION)
                            {
                                documentType = 4;
                                btnText = "Cobrar";
                                btnTerminateVisible = true;
                                positionSpinner = 0;
                            }
                            else if (DocumentModel.getDocumentType(idDocumento) == DocumentModel.TIPO_PEDIDO)
                            {
                                documentType = 3;
                                btnText = "Terminar";
                                btnTerminateVisible = false;
                                positionSpinner = 2;
                            }
                            else if (DocumentModel.getDocumentType(idDocumento) == DocumentModel.TIPO_DEVOLUCION)
                            {
                                documentType = 5;
                                btnText = "Terminar";
                                btnTerminateVisible = true;
                                positionSpinner = 3;
                            }
                        }
                    }
                    else
                    {
                        if (tipoDeDocumento == DocumentModel.TIPO_COTIZACION)
                        {
                            documentType = 1;
                            btnText = "Terminar";
                            btnTerminateVisible = false;
                            positionSpinner = 1;
                        }
                        else if (tipoDeDocumento == DocumentModel.TIPO_VENTA)
                        {
                            documentType = 2;
                            btnText = "Cobrar";
                            btnTerminateVisible = true;
                            positionSpinner = 0;
                        }
                        else if (tipoDeDocumento == DocumentModel.TIPO_REMISION)
                        {
                            documentType = 4;
                            btnText = "Cobrar";
                            btnTerminateVisible = true;
                            positionSpinner = 0;
                        }
                        else if (tipoDeDocumento == DocumentModel.TIPO_PEDIDO)
                        {
                            documentType = 3;
                            btnText = "Terminar";
                            btnTerminateVisible = false;
                            positionSpinner = 2;
                        }
                        else if (tipoDeDocumento == DocumentModel.TIPO_DEVOLUCION)
                        {
                            documentType = 5;
                            btnText = "Terminar";
                            btnTerminateVisible = true;
                            positionSpinner = 3;
                        }
                        description = "Documento no encontrado";
                    }
                } catch (Exception ex)
                {
                    SECUDOC.writeLog(ex.ToString());
                    value = -1;
                    description = ex.Message;
                    btnTerminateVisible = true;
                    positionSpinner = 0;
                }
                finally
                {
                    response.value = value;
                    response.typeDocument = documentType;
                    response.btnTerminateText = btnText;
                    response.description = description;
                    response.btnTerminateVisible = btnTerminateVisible;
                    response.positionSpinner = positionSpinner;
                }
            });
            return response;
        }
    }
}
