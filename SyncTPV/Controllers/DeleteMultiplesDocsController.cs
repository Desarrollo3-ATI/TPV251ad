using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SyncTPV.Controllers
{
    public class DeleteMultiplesDocsController
    {
        private Boolean docsCancels = false;

        public async Task<Boolean> doInBackground(List<int> idsDocumentsList, bool serverModeLAN)
        {
            Boolean deleted = false;
            await Task.Run(async () =>
            {
                if (idsDocumentsList != null)
                {
                    for (int i = 0; i < idsDocumentsList.Count; i++)
                    {
                        int documentType = DocumentModel.getDocumentType(idsDocumentsList[i]);
                        if (ClsPositionsModel.cancelPositionForADocument(idsDocumentsList[i]))
                            docsCancels = cancelarVariosDocumentos(idsDocumentsList[i], documentType, serverModeLAN);
                        else docsCancels = cancelarVariosDocumentos(idsDocumentsList[i], documentType, serverModeLAN);
                    }
                }
            });
            return docsCancels;
        }

        private Boolean cancelarVariosDocumentos(int idDocumento, int documentType, bool serverModeLAN)
        {
            Boolean removed = false;
            if (MovimientosModel.checkIfThereAreStillMovementsForTheDocumentInShift(idDocumento))
            {
                if (MovimientosModel.cancelMovementsOfDocuments(idDocumento,
                        MovimientosModel.CALL_LOCAL_CANCELED, documentType, serverModeLAN) > 0)
                {
                    int idpedido = DocumentModel.getCiddoctopedidoFromADocument(idDocumento);
                    if (idpedido > 0)
                        PedidosEncabezadoModel.marcarPedidoComoSurtidoONoSurtido(idpedido, 0);
                    if (DocumentModel.cancelADocument(idDocumento) > 0)
                    {
                        if (ItemModel.quitarTodosLosArticulosDelCarrito() > 0)
                        {
                            removed = true;
                        }
                        else
                        {
                            removed = true;
                        }
                    }
                    else
                    {
                        removed = false;
                    }
                }
                else
                {
                    removed = false;
                }
            }
            else
            {
                int idpedido = DocumentModel.getCiddoctopedidoFromADocument(idDocumento);
                if (idpedido > 0)
                    PedidosEncabezadoModel.marcarPedidoComoSurtidoONoSurtido(idpedido, 0);
                if (DocumentModel.cancelADocument(idDocumento) > 0)
                {
                    if (ItemModel.quitarTodosLosArticulosDelCarrito() > 0)
                    {
                        removed = true;
                    }
                    else
                    {
                        removed = true;
                    }
                }
                else
                {
                    removed = false;
                }
            }
            return removed;
        }

    }
}
