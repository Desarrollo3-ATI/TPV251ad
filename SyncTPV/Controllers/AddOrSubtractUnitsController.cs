using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Dynamic;
using System.Threading.Tasks;
using wsROMClase;
using wsROMClases.Models.Commercial;
using wsROMClases.Models.Panel;

namespace SyncTPV.Controllers
{
    public class AddOrSubtractUnitsController
    {
        private int idDocument, idMovement, positionMovement, moreOrLess, call, documentType;
        private double capturedUnits;

        public AddOrSubtractUnitsController(int idDocument, int idMovement, int positionMovement, int moreOrLess,
                                         double capturedUnits, int call, int documentType)
        {
            this.idDocument = idDocument;
            this.idMovement = idMovement;
            this.positionMovement = positionMovement;
            this.moreOrLess = moreOrLess;
            this.capturedUnits = capturedUnits;
            this.call = call;
            this.documentType = documentType;
        }

        public async Task<ExpandoObject> doInBackgroundLocal(ClsItemModel itemModel, int addOrSubstract, int actualizar, 
            double capturedNonConvertibleUnits, double newPrice, bool serverModeLAN, bool permissionPrepedido, String discountText, 
            String observation, int capturedUnitId)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                double baseUnits = 0;
                MovimientosModel.updateObervation(idMovement, observation);
                try
                {
                    double discountRate = 0;
                    if (call == 0)
                    {
                        if (await MovimientosModel.removeOrAddQuantityToAMovement(idMovement, positionMovement, itemModel, capturedUnits, idDocument,
                            addOrSubstract, actualizar, newPrice, capturedNonConvertibleUnits, discountRate, serverModeLAN, capturedUnitId, baseUnits))
                        {
                            if (DocumentModel.getDocumentType(idDocument) != 5)
                            {
                                double newUnitOfMove = MovimientosModel.getCapturedUnitsOfAMove(idMovement, idDocument);
                                if (MovimientosModel.validateWhetherToApplyPromotionToAMovement(idMovement, newUnitOfMove, itemModel))
                                {
                                    value = 1;
                                }
                            }
                            else
                            {
                                value = 1;
                                //Toast.makeText(context, "Cantidad restada correctamente!", Toast.LENGTH_SHORT).show();
                                //updateItemInformation(context, idItem, positionMovement, positionRecycler);
                                //imm.toggleSoftInput(InputMethodManager.HIDE_IMPLICIT_ONLY, 0);
                                //dialogCambioCant.dismiss();
                            }
                        }
                        else
                        {
                            value = 0;
                            //Toast.makeText(context, "Oops, algo falló al cambiar la existencia!", Toast.LENGTH_SHORT).show();
                            //updateItemInformation(context, idItem, positionMovement, positionRecycler);
                            //imm.toggleSoftInput(InputMethodManager.HIDE_IMPLICIT_ONLY, 0);
                            //dialogCambioCant.dismiss();
                        }
                    }
                    else
                    {
                        bool add = false;
                        if (addOrSubstract == 1)
                        {
                            if (documentType == DocumentModel.TIPO_VENTA || documentType == DocumentModel.TIPO_REMISION)
                            {
                                double currentExistence = itemModel.existencia;
                                if (itemModel.baseUnitId != capturedUnitId)
                                {
                                    int capturedUnitIsMajor = -1;
                                    if (serverModeLAN)
                                    {
                                        dynamic responseUnits = await ConversionsUnitsController.checkIfTheCapturedUnitIsHigherLAN(itemModel.baseUnitId, capturedUnitId);
                                        if (responseUnits.value == 1)
                                            capturedUnitIsMajor = responseUnits.salesUnitIsHigher;
                                    } else
                                    {
                                        dynamic responseUnits = await ConversionsUnitsController.checkIfTheCapturedUnitIsHigherAPI(itemModel.baseUnitId, capturedUnitId);
                                        if (responseUnits.value == 1)
                                            capturedUnitIsMajor = responseUnits.salesUnitIsHigher;
                                        else capturedUnitIsMajor = ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(itemModel.baseUnitId, capturedUnitId);
                                    }
                                    if (capturedUnitIsMajor == 0)
                                    {
                                        /** Unidad de venta es menor: multiplicamos la base por el numero de conversión mayor */
                                        double majorConversion = 0;
                                        if (serverModeLAN)
                                        {
                                            dynamic responseConversion = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.baseUnitId, capturedUnitId, true);
                                            if (responseConversion.value == 1)
                                                majorConversion = responseConversion.majorFactor;
                                        } else
                                        {
                                            dynamic responseConversion = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemAPI(itemModel.baseUnitId, capturedUnitId, true);
                                            if (responseConversion.value == 1)
                                                majorConversion = responseConversion.majorFactor;
                                            else majorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, true);
                                        }
                                        if (majorConversion != 0)
                                        {
                                            double unidadesLocales = MovimientosModel.getUnidadesBasePendientesLocales(itemModel.id, idMovement, false);
                                            currentExistence = (currentExistence - unidadesLocales);
                                            double stockInMinorUnits = currentExistence * majorConversion;
                                            if (stockInMinorUnits >= capturedUnits)
                                            {
                                                add = true;
                                                baseUnits = (capturedUnits / majorConversion);
                                            }
                                        } else
                                        {
                                            double unidadesLocales = MovimientosModel.getUnidadesBasePendientesLocales(itemModel.id, idMovement, false);
                                            currentExistence = (currentExistence - unidadesLocales);
                                            double stockInMinorUnits = currentExistence;
                                            if (stockInMinorUnits >= capturedUnits)
                                            {
                                                add = true;
                                                baseUnits = capturedUnits;
                                            }
                                        }
                                    }
                                    else if (capturedUnitIsMajor == 1)
                                    {
                                        /** Unidad de venta es mayor: multiplicamos la base por el numero de conversión mayor */
                                        double minorConversion = 0;
                                        if (serverModeLAN)
                                        {
                                            dynamic responseConversion = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.baseUnitId, capturedUnitId, false);
                                            if (responseConversion.value == 1)
                                                minorConversion = responseConversion.majorFactor;
                                        } else
                                        {
                                            dynamic responseConversion = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemAPI(itemModel.baseUnitId, capturedUnitId, false);
                                            if (responseConversion.value == 1)
                                                minorConversion = responseConversion.majorFactor;
                                            else minorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, false);
                                        }
                                        if (minorConversion != 0)
                                        {
                                            double unidadesLocales = MovimientosModel.getUnidadesBasePendientesLocales(itemModel.id, idMovement, false);
                                            currentExistence = (currentExistence - unidadesLocales);
                                            double newCapturedUnits = capturedUnits / minorConversion;
                                            if (currentExistence >= newCapturedUnits)
                                            {
                                                add = true;
                                                baseUnits = newCapturedUnits;
                                            }
                                        } else
                                        {
                                            double newCapturedUnits = capturedUnits;
                                            double unidadesLocales = MovimientosModel.getUnidadesBasePendientesLocales(itemModel.id, idMovement, false);
                                            currentExistence = (currentExistence - unidadesLocales);
                                            if (currentExistence >= newCapturedUnits)
                                            {
                                                add = true;
                                                baseUnits = newCapturedUnits;
                                            }
                                        }
                                    }
                                    else if (capturedUnitIsMajor == 2)
                                    {
                                        /** Unidad de venta es igual a la unidad base */
                                        double unidadesLocales = MovimientosModel.getUnidadesBasePendientesLocales(itemModel.id, idMovement, false);
                                        currentExistence = (currentExistence - unidadesLocales);
                                        if (currentExistence >= capturedUnits)
                                        {
                                            add = true;
                                            baseUnits = capturedUnits;
                                        }
                                    }
                                    else
                                    {
                                        double unidadesLocales = MovimientosModel.getUnidadesBasePendientesLocales(itemModel.id, idMovement, false);
                                        currentExistence = (currentExistence - unidadesLocales);
                                        if (currentExistence >= capturedUnits)
                                        { 
                                            add = true;
                                            baseUnits = capturedUnits;
                                        }
                                    }
                                }
                                else
                                {
                                    double unidadesLocales = MovimientosModel.getUnidadesBasePendientesLocales(itemModel.id, idMovement, false);
                                    currentExistence = (currentExistence - unidadesLocales);
                                    if (currentExistence >= capturedUnits)
                                    {
                                        add = true;
                                        baseUnits = capturedUnits;
                                    }
                                }
                            }
                            else
                            {
                                add = true;
                            }
                        }
                        else
                        {
                            add = true;
                        }
                        int mayorAExistencia = 0;
                        if (!discountText.Equals(""))
                        {
                            discountText = discountText.Replace("$", "").Replace(",", "");
                            double descuentoIngresado = Convert.ToDouble(discountText);
                            if (descuentoIngresado > itemModel.descuentoMaximo)
                            {
                                value = -5;
                                description = "El descuento máximo es de " + itemModel.descuentoMaximo + " %";
                            }
                            else
                            {
                                if (descuentoIngresado > 100.0)
                                {
                                    value = -6;
                                    description = "El anexo 20 no permite descuentos mayores al 100%";
                                }
                                else
                                {
                                    discountRate = descuentoIngresado;
                                }
                            }
                        }
                        else
                        {
                            discountRate = 0;
                        }
                        if (serverModeLAN)
                            await DatosTicketController.downloadAllDatosTicketLAN();
                        else await DatosTicketController.downloadAllDatosTicketAPI();
                        if (permissionPrepedido)
                        {
                            if (add || !add)
                            {
                                if (await MovimientosModel.removeOrAddQuantityToAMovement(idMovement, positionMovement, itemModel, capturedUnits, idDocument,
                                    addOrSubstract, actualizar, newPrice, capturedNonConvertibleUnits, discountRate, serverModeLAN, capturedUnitId,
                                    baseUnits))
                                {
                                    if (DocumentModel.getDocumentType(idDocument) != 5)
                                    {
                                        double newUnitOfMove = MovimientosModel.getCapturedUnitsOfAMove(idMovement, idDocument);
                                        if (MovimientosModel.validateWhetherToApplyPromotionToAMovement(idMovement, newUnitOfMove, itemModel))
                                        {
                                            value = 1;
                                            //Toast.makeText(context, "Cantidad restada correctamente!", Toast.LENGTH_SHORT).show();
                                            //updateItemInformation(context, idItem, positionMovement, positionRecycler);
                                            //imm.toggleSoftInput(InputMethodManager.HIDE_IMPLICIT_ONLY, 0);
                                            //dialogCambioCant.dismiss();
                                        }
                                        else
                                        {
                                            value = 1;
                                        }
                                    }
                                    else
                                    {
                                        value = 1;
                                        //Toast.makeText(context, "Cantidad restada correctamente!", Toast.LENGTH_SHORT).show();
                                        //updateItemInformation(context, idItem, positionMovement, positionRecycler);
                                        //imm.toggleSoftInput(InputMethodManager.HIDE_IMPLICIT_ONLY, 0);
                                        //dialogCambioCant.dismiss();
                                    }
                                }
                                else
                                {
                                    value = 0;
                                    //Toast.makeText(context, "Oops, algo falló al cambiar la existencia!", Toast.LENGTH_SHORT).show();
                                    //updateItemInformation(context, idItem, positionMovement, positionRecycler);
                                    //imm.toggleSoftInput(InputMethodManager.HIDE_IMPLICIT_ONLY, 0);
                                    //dialogCambioCant.dismiss();
                                }
                                MovimientosModel.updateCapturedUnitId(idMovement, capturedUnitId);
                            }
                            else
                            {
                                value = -1;
                            }
                        }
                        else
                        {
                            if (value != -5 && value != -6)
                            {
                                if (DatosTicketModel.sellOnlyWithStock())
                                {
                                    if (add)
                                    {
                                        if (await MovimientosModel.removeOrAddQuantityToAMovement(idMovement, positionMovement, itemModel, capturedUnits, idDocument,
                                                addOrSubstract, actualizar, newPrice, capturedNonConvertibleUnits, discountRate, serverModeLAN, capturedUnitId,
                                                baseUnits))
                                        {
                                            if (DocumentModel.getDocumentType(idDocument) != 5)
                                            {
                                                double newUnitOfMove = MovimientosModel.getCapturedUnitsOfAMove(idMovement, idDocument);
                                                if (MovimientosModel.validateWhetherToApplyPromotionToAMovement(idMovement, newUnitOfMove, itemModel))
                                                {
                                                    value = 1;
                                                    //Toast.makeText(context, "Cantidad restada correctamente!", Toast.LENGTH_SHORT).show();
                                                    //updateItemInformation(context, idItem, positionMovement, positionRecycler);
                                                    //imm.toggleSoftInput(InputMethodManager.HIDE_IMPLICIT_ONLY, 0);
                                                    //dialogCambioCant.dismiss();
                                                }
                                                else
                                                {
                                                    value = 1;
                                                }
                                            }
                                            else
                                            {
                                                value = 1;
                                                //Toast.makeText(context, "Cantidad restada correctamente!", Toast.LENGTH_SHORT).show();
                                                //updateItemInformation(context, idItem, positionMovement, positionRecycler);
                                                //imm.toggleSoftInput(InputMethodManager.HIDE_IMPLICIT_ONLY, 0);
                                                //dialogCambioCant.dismiss();
                                            }
                                        }
                                        else
                                        {
                                            value = 0;
                                            //Toast.makeText(context, "Oops, algo falló al cambiar la existencia!", Toast.LENGTH_SHORT).show();
                                            //updateItemInformation(context, idItem, positionMovement, positionRecycler);
                                            //imm.toggleSoftInput(InputMethodManager.HIDE_IMPLICIT_ONLY, 0);
                                            //dialogCambioCant.dismiss();
                                        }
                                        MovimientosModel.updateCapturedUnitId(idMovement, capturedUnitId);
                                    }
                                    else
                                    {
                                        value = -1;
                                    }
                                }
                                else
                                {
                                    if (add || !add)
                                    {
                                        if (await MovimientosModel.removeOrAddQuantityToAMovement(idMovement, positionMovement, itemModel, capturedUnits, idDocument,
                                            addOrSubstract, actualizar, newPrice, capturedNonConvertibleUnits, discountRate, serverModeLAN, capturedUnitId,
                                            baseUnits))
                                        {
                                            if (DocumentModel.getDocumentType(idDocument) != 5)
                                            {
                                                double newUnitOfMove = MovimientosModel.getCapturedUnitsOfAMove(idMovement, idDocument);
                                                if (MovimientosModel.validateWhetherToApplyPromotionToAMovement(idMovement, newUnitOfMove, itemModel))
                                                {
                                                    value = 1;
                                                    //Toast.makeText(context, "Cantidad restada correctamente!", Toast.LENGTH_SHORT).show();
                                                    //updateItemInformation(context, idItem, positionMovement, positionRecycler);
                                                    //imm.toggleSoftInput(InputMethodManager.HIDE_IMPLICIT_ONLY, 0);
                                                    //dialogCambioCant.dismiss();
                                                }
                                                else
                                                {
                                                    value = 1;
                                                }
                                            }
                                            else
                                            {
                                                value = 1;
                                                //Toast.makeText(context, "Cantidad restada correctamente!", Toast.LENGTH_SHORT).show();
                                                //updateItemInformation(context, idItem, positionMovement, positionRecycler);
                                                //imm.toggleSoftInput(InputMethodManager.HIDE_IMPLICIT_ONLY, 0);
                                                //dialogCambioCant.dismiss();
                                            }
                                        }
                                        else
                                        {
                                            value = 0;
                                            //Toast.makeText(context, "Oops, algo falló al cambiar la existencia!", Toast.LENGTH_SHORT).show();
                                            //updateItemInformation(context, idItem, positionMovement, positionRecycler);
                                            //imm.toggleSoftInput(InputMethodManager.HIDE_IMPLICIT_ONLY, 0);
                                            //dialogCambioCant.dismiss();
                                        }
                                        MovimientosModel.updateCapturedUnitId(idMovement, capturedUnitId);
                                    }
                                    else
                                    {
                                        value = -1;
                                    }
                                }
                            }
                        }
                    }
                } catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    description = e.Message;
                } finally
                {
                    response.call = call;
                    response.value = value;
                    response.description = description;
                }
            });
            return response;
        }

    }
}
