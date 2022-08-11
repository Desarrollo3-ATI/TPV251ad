using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System.Threading.Tasks;
using wsROMClase;

namespace SyncTPV.Controllers
{
    public class DeleteMovementController
    {
        private int idMovement, idDocument;

        public DeleteMovementController(int idMovement, int idDocument)
        {
            this.idMovement = idMovement;
            this.idDocument = idDocument;
        }

        public async Task<int> doInBackground(ClsItemModel itemModel, bool serverModeLAN, bool webActive, string codigoCaja)
        {
            return await logicDeleteMovement(idMovement, itemModel, idDocument, serverModeLAN, webActive, codigoCaja);
        }

        private async Task<int> logicDeleteMovement(int idMovement, ClsItemModel itemModel, int idDocument, bool serverModeLAN, bool webActive,
            string codigoCaja)
        {
            int response = 0;
            await Task.Run(async () =>
            {
                string query = "DELETE FROM " + LocalDatabase.TABLA_PESO + " WHERE " + LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = " + idMovement;
                WeightModel.createUpdateOrDelete(query);
                double nuevaExistencia = 0;
                double capturedUnits = MovimientosModel.getCapturedUnitsOfAMove(idMovement, idDocument);
                if (capturedUnits > 0)
                {
                    if (serverModeLAN)
                    {
                        dynamic responseUnitsPendings = await ItemsController.getUnitsPendingsLAN(itemModel.id);
                        if (responseUnitsPendings.value == 1)
                            itemModel.existencia = (itemModel.existencia - responseUnitsPendings.unidadesPendientes);
                        double unidadesPendientesLocales = MovimientosModel.getUnidadesBasePendientesLocales(itemModel.id, 0, true);
                        itemModel.existencia = (itemModel.existencia - unidadesPendientesLocales);
                    } else
                    {
                        if (webActive)
                        {
                            dynamic responseUnitsPendings = await ItemsController.getUnitsPendingsAPI(itemModel.id, codigoCaja);
                            if (responseUnitsPendings.value == 1)
                                itemModel.existencia = (itemModel.existencia - responseUnitsPendings.unidadesPendientes);
                        } else
                        {

                        }
                        double unidadesPendientesLocales = MovimientosModel.getUnidadesBasePendientesLocales(itemModel.id, 0, true);
                        itemModel.existencia = (itemModel.existencia - unidadesPendientesLocales);
                    }
                    double currentExistence = itemModel.existencia; 
                    int tipoDoc = DocumentModel.getDocumentType(idDocument);
                    if (tipoDoc == DocumentModel.TIPO_VENTA || tipoDoc == DocumentModel.TIPO_REMISION)
                    {
                        int capturedUnitId = MovimientosModel.getCapturedUnitId(idMovement);
                        if (itemModel.baseUnitId != capturedUnitId)
                        {
                            int capturedUnitIsMajor = ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(itemModel.baseUnitId, capturedUnitId);
                            if (capturedUnitIsMajor == 0)
                            {
                                /** Unidad de venta es menor: multiplicamos la base por el numero de conversión mayor */
                                double majorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, true);
                                double stockInMinorUnits = currentExistence * majorConversion;
                                double newStockInMinorUnits = stockInMinorUnits + capturedUnits;
                                nuevaExistencia = newStockInMinorUnits / majorConversion;
                            }
                            else if (capturedUnitIsMajor == 1)
                            {
                                /**Unidad de venta es mayor: multiplicamos la base por el numero de conversión mayor */
                                double minorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, false);
                                double baseUnits = capturedUnits / minorConversion;
                                nuevaExistencia = currentExistence + baseUnits;
                            }
                            else if (capturedUnitIsMajor == 2)
                            {
                                nuevaExistencia = currentExistence + capturedUnits;
                            }
                            else
                            {
                                nuevaExistencia = currentExistence + capturedUnits;
                            }
                        }
                        else
                        {
                            nuevaExistencia = currentExistence + capturedUnits;
                        }
                        if (serverModeLAN) {
                            if (MovimientosModel.deleteAMoveByIdMovimiento(idDocument, idMovement))
                            {
                                if (MovimientosModel.checkForMovementsOfAnItem(idDocument, itemModel.id))
                                {
                                    MovimientosModel.reassignMovementPositions();
                                    response = 1;
                                }
                                else
                                {
                                    if (MovimientosModel.checkIfThereAreStillMovementsForTheDocumentInShift(idDocument))
                                    {
                                        MovimientosModel.reassignMovementPositions();
                                        response = 1;
                                    }
                                    else
                                    {
                                        response = 2;
                                    }
                                }
                            }
                        } else
                        {
                            if (ItemModel.changeTheExistenceOfAnItem(itemModel.id, nuevaExistencia))
                            {
                                if (MovimientosModel.deleteAMoveByIdMovimiento(idDocument, idMovement))
                                {
                                    if (MovimientosModel.checkForMovementsOfAnItem(idDocument, itemModel.id))
                                    {
                                        MovimientosModel.reassignMovementPositions();
                                        response = 1;
                                    }
                                    else
                                    {
                                        if (ItemModel.removeAnItemFromTheCart(itemModel.id))
                                        {
                                            if (MovimientosModel.checkIfThereAreStillMovementsForTheDocumentInShift(idDocument))
                                            {
                                                MovimientosModel.reassignMovementPositions();
                                                response = 1;
                                            }
                                            else
                                            {
                                                response = 2;
                                            }
                                        }
                                        else
                                        {
                                            response = -1;
                                            //Toast.makeText(context, "Oops, ocurrio un error al quitar el articulo del carrito!", Toast.LENGTH_SHORT).show();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                response = -2;
                                //Toast.makeText(context, "Oops, no se pudo, sumar la cantidad a la existencia", Toast.LENGTH_SHORT).show();
                            }
                        }
                    }
                    else if (tipoDoc == DocumentModel.TIPO_DEVOLUCION)
                    {
                        int capturedUnitId = MovimientosModel.getCapturedUnitId(idMovement);
                        if (itemModel.baseUnitId != capturedUnitId)
                        {
                            int capturedUnitIsMajor = ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(itemModel.baseUnitId, capturedUnitId);
                            if (capturedUnitIsMajor == 0)
                            {
                                /** Unidad de venta es menor: multiplicamos la base por el numero de conversión mayor */
                                double majorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, true);
                                double stockInMinorUnits = currentExistence * majorConversion;
                                double newStockInMinorUnits = stockInMinorUnits - capturedUnits;
                                nuevaExistencia = newStockInMinorUnits / majorConversion;
                            }
                            else if (capturedUnitIsMajor == 1)
                            {
                                /**Unidad de venta es mayor: multiplicamos la base por el numero de conversión mayor */
                                double minorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, false);
                                double baseUnits = capturedUnits / minorConversion;
                                nuevaExistencia = currentExistence - baseUnits;
                            }
                            else if (capturedUnitIsMajor == 2)
                            {
                                nuevaExistencia = currentExistence - capturedUnits;
                            }
                            else
                            {
                                nuevaExistencia = currentExistence - capturedUnits;
                            }
                        }
                        else
                        {
                            nuevaExistencia = currentExistence - capturedUnits;
                        }
                        if (serverModeLAN)
                        {
                            if (MovimientosModel.deleteAMoveByIdMovimiento(idDocument, idMovement))
                            {
                                if (MovimientosModel.checkForMovementsOfAnItem(idDocument, itemModel.id))
                                {
                                    MovimientosModel.reassignMovementPositions();
                                    response = 1;
                                }
                                else
                                {
                                    if (MovimientosModel.checkIfThereAreStillMovementsForTheDocumentInShift(idDocument))
                                    {
                                        MovimientosModel.reassignMovementPositions();
                                        response = 1;
                                    }
                                    else
                                    {
                                        response = 2;
                                    }
                                }
                            }
                        } 
                        else
                        {
                            if (ItemModel.changeTheExistenceOfAnItem(itemModel.id, nuevaExistencia))
                            {
                                if (MovimientosModel.deleteAMoveByIdMovimiento(idDocument, idMovement))
                                {
                                    if (MovimientosModel.checkForMovementsOfAnItem(idDocument, itemModel.id))
                                    {
                                        MovimientosModel.reassignMovementPositions();
                                        response = 1;
                                    }
                                    else
                                    {
                                        if (ItemModel.removeAnItemFromTheCart(itemModel.id))
                                        {
                                            if (MovimientosModel.checkIfThereAreStillMovementsForTheDocumentInShift(idDocument))
                                            {
                                                MovimientosModel.reassignMovementPositions();
                                                response = 1;
                                                //Toast.makeText(context, "Artículo borrado del carrito exitosamente!", Toast.LENGTH_SHORT).show();
                                                //updateItemDeleted(positionRecycler);
                                            }
                                            else
                                            {
                                                response = 2;
                                                //Toast.makeText(context, "Artículo borrado del carrito exitosamente!", Toast.LENGTH_SHORT).show();
                                                //updateItemDeleted(positionRecycler);
                                                //onBackPressed();
                                            }
                                        }
                                        else
                                        {
                                            response = -1;
                                            //Toast.makeText(context, "Oops, ocurrio un error al quitar el articulo del carrito!", Toast.LENGTH_SHORT).show();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                response = -2;
                                //Toast.makeText(context, "Oops, no se pudo, sumar la cantidad a la existencia", Toast.LENGTH_SHORT).show();
                            }
                        }
                    }
                    else
                    {
                        if (serverModeLAN)
                        {
                            if (MovimientosModel.deleteAMoveByIdMovimiento(idDocument, idMovement))
                            {
                                if (MovimientosModel.checkForMovementsOfAnItem(idDocument, itemModel.id))
                                {
                                    MovimientosModel.reassignMovementPositions();
                                    response = 1;
                                    //Toast.makeText(context, "Artículo borrado del carrito exitosamente!", Toast.LENGTH_SHORT).show();
                                    //updateItemDeleted(positionRecycler);
                                }
                                else
                                {
                                    if (MovimientosModel.checkIfThereAreStillMovementsForTheDocumentInShift(idDocument))
                                    {
                                        MovimientosModel.reassignMovementPositions();
                                        response = 1;
                                        //Toast.makeText(context, "Artículo borrado del carrito exitosamente!", Toast.LENGTH_SHORT).show();
                                        //updateItemDeleted(positionRecycler);
                                    }
                                    else
                                    {
                                        response = 2;
                                        //Toast.makeText(context, "Artículo borrado del carrito exitosamente!", Toast.LENGTH_SHORT).show();
                                        //updateItemDeleted(positionRecycler);
                                        //onBackPressed();
                                    }
                                }
                            }
                            else
                            {
                                response = -2;
                                //Toast.makeText(context, "Oops, ocurrio un error al eliminar movimiento!", Toast.LENGTH_SHORT).show();
                            }
                        } else
                        {
                            if (MovimientosModel.deleteAMoveByIdMovimiento(idDocument, idMovement))
                            {
                                if (MovimientosModel.checkForMovementsOfAnItem(idDocument, itemModel.id))
                                {
                                    MovimientosModel.reassignMovementPositions();
                                    response = 1;
                                    //Toast.makeText(context, "Artículo borrado del carrito exitosamente!", Toast.LENGTH_SHORT).show();
                                    //updateItemDeleted(positionRecycler);
                                }
                                else
                                {
                                    if (ItemModel.removeAnItemFromTheCart(itemModel.id))
                                    {
                                        if (MovimientosModel.checkIfThereAreStillMovementsForTheDocumentInShift(idDocument))
                                        {
                                            MovimientosModel.reassignMovementPositions();
                                            response = 1;
                                            //Toast.makeText(context, "Artículo borrado del carrito exitosamente!", Toast.LENGTH_SHORT).show();
                                            //updateItemDeleted(positionRecycler);
                                        }
                                        else
                                        {
                                            response = 2;
                                            //Toast.makeText(context, "Artículo borrado del carrito exitosamente!", Toast.LENGTH_SHORT).show();
                                            //updateItemDeleted(positionRecycler);
                                            //onBackPressed();
                                        }
                                    }
                                    else
                                    {
                                        response = -1;
                                        //Toast.makeText(context, "Oops, ocurrio un error al quitar el articulo del carrito!", Toast.LENGTH_SHORT).show();
                                    }
                                }
                            }
                            else
                            {
                                response = -2;
                                //Toast.makeText(context, "Oops, ocurrio un error al eliminar movimiento!", Toast.LENGTH_SHORT).show();
                            }
                        }
                    }
                } else
                {
                    if (serverModeLAN)
                    {
                        if (MovimientosModel.deleteAMoveByIdMovimiento(idDocument, idMovement))
                        {
                            if (MovimientosModel.checkForMovementsOfAnItem(idDocument, itemModel.id))
                            {
                                MovimientosModel.reassignMovementPositions();
                                response = 1;
                            }
                            else
                            {
                                if (MovimientosModel.checkIfThereAreStillMovementsForTheDocumentInShift(idDocument))
                                {
                                    MovimientosModel.reassignMovementPositions();
                                    response = 1;
                                }
                                else
                                {
                                    response = 2;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (MovimientosModel.deleteAMoveByIdMovimiento(idDocument, idMovement))
                        {
                            if (MovimientosModel.checkForMovementsOfAnItem(idDocument, itemModel.id))
                            {
                                MovimientosModel.reassignMovementPositions();
                                response = 1;
                            }
                            else
                            {
                                if (ItemModel.removeAnItemFromTheCart(itemModel.id))
                                {
                                    if (MovimientosModel.checkIfThereAreStillMovementsForTheDocumentInShift(idDocument))
                                    {
                                        MovimientosModel.reassignMovementPositions();
                                        response = 1;
                                    }
                                    else
                                    {
                                        response = 2;
                                    }
                                }
                                else
                                {
                                    response = -1;
                                }
                            }
                        }
                    }
                }
            });
            return response;
        }
    }
}
