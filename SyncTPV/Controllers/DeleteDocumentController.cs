using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SyncTPV.Controllers
{
    public class DeleteDocumentController
    {
        public DeleteDocumentController()
        {

        }

        public async Task<Boolean> doInBackground(int idDocument, int documentType, bool serverModeLAN, bool permissionPrepedido)
        {
            Boolean canceled = false;
            await Task.Run(async () =>
            {
                if (permissionPrepedido)
                {
                    List<MovimientosModel> mmList = MovimientosModel.getAllMovementsFromADocument(idDocument);
                    if (mmList != null && mmList.Count > 0)
                    {
                        foreach (var mm in mmList)
                        {
                            WeightModel.deleteWieghtTemporal(mm.id);
                            WeightModel.deleteWieghtReal(mm.id);
                        }
                    }
                }
                if (MovimientosModel.checkIfThereAreStillMovementsForTheDocumentInShift(idDocument))
                {
                    if (MovimientosModel.cancelMovementsOfDocuments(idDocument, MovimientosModel.CALL_LOCAL_CANCELED, documentType, serverModeLAN) > 0)
                    {
                        int idpedido = DocumentModel.getCiddoctopedidoFromADocument(idDocument);
                        if (idpedido > 0)
                            PedidosEncabezadoModel.marcarPedidoComoSurtidoONoSurtido(idpedido, 0);
                        FormasDeCobroDocumentoModel.deleteAllFcOfADocument(idDocument);
                        if (DocumentModel.eliminarUnDocumento(idDocument) > 0)
                        {
                            if (ItemModel.quitarTodosLosArticulosDelCarrito() > 0)
                            {
                                canceled = true;
                            }
                        }
                    }
                }
                else
                {
                    int idpedido = DocumentModel.getCiddoctopedidoFromADocument(idDocument);
                    if (idpedido > 0)
                        PedidosEncabezadoModel.marcarPedidoComoSurtidoONoSurtido(idpedido, 0);
                    FormasDeCobroDocumentoModel.deleteAllFcOfADocument(idDocument);
                    if (DocumentModel.eliminarUnDocumento(idDocument) > 0)
                    {
                        if (ItemModel.quitarTodosLosArticulosDelCarrito() > 0)
                        {
                            canceled = true;
                        }
                    }
                }
            });
            return canceled;
        }
    }
}
