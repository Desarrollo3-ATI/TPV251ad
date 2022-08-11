using SyncTPV.Controllers.Downloads;
using SyncTPV.Models;
using SyncTPV.Views;
using System;
using System.Dynamic;
using System.Threading.Tasks;

namespace SyncTPV.Controllers
{
    public class CarritoRomTaskController
    {
        private DocumentModel dvm = null;
        private double pending, discountDocument, change;
        private Boolean billPermission = false;
        private int printTicket = 0, sendPermission = 0, idCustomer = 0, idDocument = 0;
        private int license = 0;
        private int response = 0;

        public CarritoRomTaskController(int license)
        {
            this.license = license;
        }

        public async Task<ExpandoObject> doInBackground(int idDocument, int call, int printTicket, bool serverModeLAN)
        {
            dynamic resp = new ExpandoObject();
            await Task.Run(async () =>
            {
                if (call == FormPayCart.CALL_GET_DOCUMENT_DATA)
                {
                    dynamic sumsMap = getCurrentSumsFromDocument(idDocument);
                    DocumentModel.updateInformationForAPausedDocuments(idDocument, sumsMap.descuento, sumsMap.total);
                    resp.dvm = DocumentModel.getAllDataDocumento(idDocument);
                    resp.discountDocument = MovimientosModel.obtenerElDescuentoTotalEnImporteDeUnDocumento(idDocument);
                    resp.pending = FormasDeCobroDocumentoModel.getSaldoPendienteOfTheLastFcDcoument(idDocument);
                    resp.change = FormasDeCobroDocumentoModel.getCambioOfTheDcoument(idDocument);
                    if (serverModeLAN)
                        await FormasDeCobroController.downloadAllFormasDeCobroLAN();
                    else await FormasDeCobroController.downloadAllFormasDeCobroAPI(ClsInitialChargeController.UPDATE_DATA);
                    resp.fcList = FormasDeCobroModel.getAllFormasDeCobro();
                    resp.billPermission = UserModel.doYouHavePermissionToBill();
                }
                else if (call == FormPayCart.CALL_END_DOCUMENT)
                {
                    resp.idDocument = idDocument;
                    resp.printTicket = printTicket;
                    idCustomer = DocumentModel.getCustomerIdOfADocument(idDocument);
                    if (ItemModel.quitarTodosLosArticulosDelCarrito() > 0)
                    {
                        if (UserModel.permisoParaEnviarDocsAutomaticamente() == 1)
                        {
                            resp.sendPermission = 1;
                            if (MetodosGenerales.verifyIfInternetIsAvailable())
                            {
                                if (license == 1)
                                {
                                    if (idCustomer < 0)
                                    {
                                        if (printTicket == 1)
                                        {
                                            
                                        }
                                        else
                                        {
                                            resp.response = 1;
                                        }
                                        resp.response = 1;
                                        //SendAdditionalCustomerService.startActionSendAdditionalCustomer(context, idCustomer);
                                    }
                                    else
                                    {
                                        if (printTicket == 1)
                                        {
                                            
                                        }
                                        else
                                        {
                                            resp.response = 2;
                                        }
                                        resp.response = 2;
                                        //enviarDocDirectamenteAlWs(idDocumento);
                                    }
                                }
                            }
                            else
                            {
                                resp.response = 3;
                                //cerrarActivitiesFinalizarEsteYVerificarADondeIr(idPosition);
                            }
                        }
                        else
                        {
                            resp.sendPermission = 0;
                            resp.response = 3;
                        }
                    }
                }
            });
            return resp;
        }

        public ExpandoObject getCurrentSumsFromDocument(int idDocumento)
        {
            dynamic sumsMap = new ExpandoObject();
            String sumas = MovimientosModel.obtenerSumaDeSubDescYTotalDeMovimientosDeUndocumento(idDocumento);
            if (!sumas.Equals(""))
            {
                String[] parts = sumas.Split(new Char[] { '-' });
                String subtotal = parts[0];
                subtotal = subtotal.Replace(",", "");
                sumsMap.subtotal = Convert.ToDouble(subtotal);
                String descuento = parts[1];
                descuento = descuento.Replace(",", "");
                sumsMap.descuento = Convert.ToDouble(descuento);
                String total = parts[2];
                total = total.Replace(",", "");
                sumsMap.total = Convert.ToDouble(total);
            }
            else
            {
                sumsMap.subtotal = 0;
                sumsMap.descuento = 0;
                sumsMap.total = 0;
            }
            return sumsMap;
        }
    }
}
