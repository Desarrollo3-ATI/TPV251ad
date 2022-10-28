using SyncTPV.Models;
using System;
using System.Dynamic;
using System.Threading.Tasks;
using wsROMClase;
using wsROMClases.Models.Commercial;

namespace SyncTPV.Controllers
{

    public class AddMovementController
    {
        private ClsItemModel itemModel;
        private int idDocument, documentType;
        private double salesUnits, nonConvertibleUnits, capturedUnits, price, discountRate, descMax;
        private String discountText = "", observation = "";
        private double monto;
        private bool serverModeLAN = false;

        public AddMovementController(int idDocument, double capturedUnits, double price, ClsItemModel itemModel, String discountText, 
            double descMax, int documentType, String observation, double nonConvertibleUnits, bool severModeLAN)
        {
            this.capturedUnits = capturedUnits;
            this.price = price;
            this.idDocument = idDocument;
            this.discountText = discountText;
            this.descMax = descMax;
            this.documentType = documentType;
            this.observation = observation;
            this.nonConvertibleUnits = nonConvertibleUnits;
            this.itemModel = itemModel;
            this.serverModeLAN = severModeLAN;
        }

        public async Task<ExpandoObject> doInBackgroundLocal(int capturedUnitId, bool serverModeLAN, bool permissionPrepedido, bool webActive)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                dynamic responseMovimiento = new ExpandoObject();
                bool insufficientStock = false;
                int nonConvertibleUnitId = itemModel.nonConvertibleUnitId;
                double newAmountDiscount = 0;
                if (documentType == DocumentModel.TIPO_VENTA || documentType == DocumentModel.TIPO_REMISION)
                {
                    /** Logic for types Ventas and Ventas TPV */
                    double existencias = itemModel.existencia;
                    if (itemModel.salesUnitId != capturedUnitId)
                    {
                        int capturedUnitsIsMajorThanSalesUnits = -1;// ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(itemModel.salesUnitId, capturedUnitId);
                        if (serverModeLAN)
                        {
                            dynamic responseMajorConversion = await ConversionsUnitsController.checkIfTheCapturedUnitIsHigherLAN(itemModel.salesUnitId, capturedUnitId);
                            if (responseMajorConversion.value == 1)
                                capturedUnitsIsMajorThanSalesUnits = responseMajorConversion.salesUnitIsHigher;
                        }
                        else
                        {
                            if (webActive)
                            {
                                dynamic responseMajorConversion = await ConversionsUnitsController.checkIfTheCapturedUnitIsHigherAPI(itemModel.salesUnitId, capturedUnitId);
                                if (responseMajorConversion.value == 1)
                                    capturedUnitsIsMajorThanSalesUnits = responseMajorConversion.salesUnitIsHigher;
                                else capturedUnitsIsMajorThanSalesUnits = ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(itemModel.salesUnitId, capturedUnitId);
                            } else
                            {
                                capturedUnitsIsMajorThanSalesUnits = ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(itemModel.salesUnitId, capturedUnitId);
                            }
                        }
                        if (capturedUnitsIsMajorThanSalesUnits == 0)
                        {
                            /** Unidad de venta es menor: multiplicamos la base por el numero de conversión mayor */
                            double majorConversion = 0;// ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.salesUnitId, capturedUnitId, true);
                            if (serverModeLAN)
                            {
                                dynamic responseFactor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.salesUnitId, capturedUnitId, true);
                                if (responseFactor.value == 1)
                                    majorConversion = responseFactor.majorFactor;
                            }
                            else
                            {
                                if (webActive)
                                {
                                    dynamic responseFactor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemAPI(itemModel.salesUnitId, capturedUnitId, true);
                                    if (responseFactor.value == 1)
                                        majorConversion = responseFactor.majorFactor;
                                    else majorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.salesUnitId, capturedUnitId, true);
                                } else
                                {
                                    majorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.salesUnitId, capturedUnitId, true);
                                }
                            }
                            if (majorConversion != 0)
                            {
                                double unidadesLocales = MovimientosModel.getUnidadesBasePendientesLocales(itemModel.id, 0, true);
                                existencias = (existencias - unidadesLocales);
                                if (capturedUnits > existencias)
                                    insufficientStock = true;
                                else
                                {
                                    salesUnits = capturedUnits;
                                }
                            } else
                            {
                                double unidadesLocales = MovimientosModel.getUnidadesBasePendientesLocales(itemModel.id, 0, true);
                                existencias = (existencias - unidadesLocales);
                                if (capturedUnits > existencias)
                                    insufficientStock = true;
                                else
                                {
                                    salesUnits = capturedUnits;
                                }
                            }
                        }
                        else if (capturedUnitsIsMajorThanSalesUnits == 1)
                        {
                            /** Unidad capturada es mayor a la de venta */
                            if (capturedUnitId == itemModel.baseUnitId)
                            {
                                double unidadesLocales = MovimientosModel.getUnidadesBasePendientesLocales(itemModel.id, 0, true);
                                existencias = (existencias - unidadesLocales);
                                if (capturedUnits > existencias)
                                    insufficientStock = true;
                                else
                                {
                                    double minorConversion = 0;// ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.salesUnitId, capturedUnitId, false);
                                    if (serverModeLAN)
                                    {
                                        dynamic responseFactor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.salesUnitId, capturedUnitId, false);
                                        if (responseFactor.value == 1)
                                            minorConversion = responseFactor.majorFactor;
                                    }
                                    else
                                    {
                                        if (webActive)
                                        {
                                            dynamic responseFactor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemAPI(itemModel.salesUnitId, capturedUnitId, false);
                                            if (responseFactor.value == 1)
                                                minorConversion = responseFactor.majorFactor;
                                            else minorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.salesUnitId, capturedUnitId, false);
                                        } else
                                        {
                                            minorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.salesUnitId, capturedUnitId, false);
                                        }
                                    }
                                    salesUnits = capturedUnits;
                                }
                            }
                            else
                            {
                                double minorConversion = 0;// ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.salesUnitId, capturedUnitId, false);
                                if (serverModeLAN)
                                {
                                    dynamic responseFactor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.salesUnitId, capturedUnitId, false);
                                    if (responseFactor.value == 1)
                                        minorConversion = responseFactor.majorFactor;
                                }
                                else
                                {
                                    if (webActive)
                                    {
                                        dynamic responseFactor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemAPI(itemModel.salesUnitId, capturedUnitId, false);
                                        if (responseFactor.value == 1)
                                            minorConversion = responseFactor.majorFactor;
                                        else minorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.salesUnitId, capturedUnitId, false);
                                    } else
                                    {
                                        minorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.salesUnitId, capturedUnitId, false);
                                    }
                                }
                                if (minorConversion != 0)
                                    salesUnits = (capturedUnits / minorConversion);
                                else salesUnits = (capturedUnits);
                                double majorFactor = 0;// ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.salesUnitId, itemModel.baseUnitId, true);
                                if (serverModeLAN)
                                {
                                    dynamic responseFactor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.salesUnitId, itemModel.baseUnitId, true);
                                    if (responseFactor.value == 1)
                                        majorFactor = responseFactor.majorFactor;
                                }
                                else
                                {
                                    if (webActive)
                                    {
                                        dynamic responseFactor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemAPI(itemModel.salesUnitId, itemModel.baseUnitId, true);
                                        if (responseFactor.value == 1)
                                            majorFactor = responseFactor.majorFactor;
                                        else majorFactor = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.salesUnitId, itemModel.baseUnitId, true);
                                    } else
                                    {
                                        majorFactor = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.salesUnitId, itemModel.baseUnitId, true);
                                    }
                                }
                                if (majorFactor != 0)
                                {
                                    double unidadesLocales = MovimientosModel.getUnidadesBasePendientesLocales(itemModel.id, 0, true);
                                    existencias = (existencias - unidadesLocales);
                                    double stockInSalesUnits = existencias * majorFactor;
                                    if (salesUnits > stockInSalesUnits)
                                        insufficientStock = true;
                                }
                                else
                                {
                                    double stockInSalesUnits = existencias;
                                    double unidadesLocales = MovimientosModel.getUnidadesBasePendientesLocales(itemModel.id, 0, true);
                                    stockInSalesUnits = (stockInSalesUnits - unidadesLocales);
                                    if (salesUnits > stockInSalesUnits)
                                        insufficientStock = true;
                                }
                            }
                        }
                        else if (capturedUnitsIsMajorThanSalesUnits == 2)
                        {
                            double unidadesLocales = MovimientosModel.getUnidadesBasePendientesLocales(itemModel.id, 0, true);
                            existencias = (existencias - unidadesLocales);
                            if (capturedUnits > existencias)
                                insufficientStock = true;
                            else salesUnits = capturedUnits;
                        }
                        else
                        {
                            double unidadesLocales = MovimientosModel.getUnidadesBasePendientesLocales(itemModel.id, 0, true);
                            existencias = (existencias - unidadesLocales);
                            if (capturedUnits > existencias)
                                insufficientStock = true;
                            else salesUnits = capturedUnits;
                        }
                    }
                    else
                    {
                        /** */
                        if (itemModel.baseUnitId != capturedUnitId)
                        {
                            /** Unidades capturadas son iguales a la de venta */
                            int capturedUnitIsMajorThanTheBase = -1;
                            if (serverModeLAN)
                            {
                                dynamic responseMajorConversion = await ConversionsUnitsController.checkIfTheCapturedUnitIsHigherLAN(itemModel.baseUnitId, capturedUnitId);
                                if (responseMajorConversion.value == 1)
                                    capturedUnitIsMajorThanTheBase = responseMajorConversion.salesUnitIsHigher;
                            }
                            else
                            {
                                if (webActive)
                                {
                                    dynamic responseMajorConversion = await ConversionsUnitsController.checkIfTheCapturedUnitIsHigherAPI(itemModel.baseUnitId, capturedUnitId);
                                    if (responseMajorConversion.value == 1)
                                        capturedUnitIsMajorThanTheBase = responseMajorConversion.salesUnitIsHigher;
                                    else capturedUnitIsMajorThanTheBase = ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(itemModel.baseUnitId, capturedUnitId);
                                } else
                                {
                                    capturedUnitIsMajorThanTheBase = ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(itemModel.baseUnitId, capturedUnitId);
                                }
                            }
                            if (capturedUnitIsMajorThanTheBase == 0)
                            {
                                /** Unidades capturadas son mayores en equivalencia a las de base e igual a las de venta*/
                                double majorFactor = 0;// ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, true);
                                if (serverModeLAN)
                                {
                                    dynamic responseFactor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.baseUnitId, capturedUnitId, true);
                                    if (responseFactor.value == 1)
                                        majorFactor = responseFactor.majorFactor;
                                }
                                else
                                {
                                    if (webActive)
                                    {
                                        dynamic responseFactor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemAPI(itemModel.baseUnitId, capturedUnitId, true);
                                        if (responseFactor.value == 1)
                                            majorFactor = responseFactor.majorFactor;
                                        else majorFactor = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, true);
                                    } else
                                    {
                                        majorFactor = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, true);
                                    }
                                }
                                double unidadesLocales = MovimientosModel.getUnidadesBasePendientesLocales(itemModel.id, 0, true);
                                existencias = (existencias - unidadesLocales);
                                double majorStock = (existencias * majorFactor);
                                if (capturedUnits > majorStock)
                                    insufficientStock = true;
                                else
                                {
                                    salesUnits = (capturedUnits / majorFactor);
                                }
                            }
                            else if (capturedUnitIsMajorThanTheBase == 1)
                            {
                                /** Unidades capturadas son menores a las de base e iguales a las de venta */
                                double majorFactor = 0;
                                if (serverModeLAN)
                                {
                                    dynamic responseFactor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.baseUnitId, capturedUnitId, true);
                                    if (responseFactor.value == 1)
                                        majorFactor = responseFactor.majorFactor;
                                }
                                else
                                {
                                    if (webActive)
                                    {
                                        dynamic responseFactor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemAPI(itemModel.baseUnitId, capturedUnitId, true);
                                        if (responseFactor.value == 1)
                                            majorFactor = responseFactor.majorFactor;
                                        else majorFactor = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, true);
                                    } else
                                    {
                                        majorFactor = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, true);
                                    }
                                }
                                if (majorFactor != 0)
                                {
                                    double unidadesLocales = MovimientosModel.getUnidadesBasePendientesLocales(itemModel.id, 0, true);
                                    existencias = (existencias - unidadesLocales);
                                    double minorStock = (existencias / majorFactor);
                                    if (capturedUnits > minorStock)
                                        insufficientStock = true;
                                    else
                                    {
                                        salesUnits = (capturedUnits * majorFactor);
                                    }
                                } else
                                {
                                    double minorStock = (existencias);
                                    double unidadesLocales = MovimientosModel.getUnidadesBasePendientesLocales(itemModel.id, 0, true);
                                    minorStock = (minorStock - unidadesLocales);
                                    if (capturedUnits > minorStock)
                                        insufficientStock = true;
                                    else
                                    {
                                        salesUnits = capturedUnits;
                                    }
                                }
                            }
                            else
                            {
                                //Unidades capturadas son iguales que las de base
                                double unidadesLocales = MovimientosModel.getUnidadesBasePendientesLocales(itemModel.id, 0, true);
                                existencias = (existencias - unidadesLocales);
                                if (existencias >= capturedUnits)
                                {
                                    salesUnits = capturedUnits;
                                }
                                else
                                {
                                    insufficientStock = true;
                                }
                            }
                        }
                        else
                        {
                            double unidadesLocales = MovimientosModel.getUnidadesBasePendientesLocales(itemModel.id, 0, true);
                            existencias = (existencias - unidadesLocales);
                            if (capturedUnits > existencias)
                                insufficientStock = true;
                            else salesUnits = capturedUnits;
                        }
                    }
                    if (DatosTicketModel.sellOnlyWithStock())
                    {
                        if (!insufficientStock)
                        {
                            if (!discountText.Equals(""))
                            {
                                discountText = discountText.Replace(",", "");
                                double descuentoIngresado = Convert.ToDouble(discountText);
                                if (descuentoIngresado > descMax)
                                {
                                    responseMovimiento.valor = -5;
                                    responseMovimiento.idMovimiento = 0;
                                    responseMovimiento.position = 0;
                                    //Toast.makeText(context, "El descuento maximo es de "+descMax+" %", Toast.LENGTH_SHORT).show();
                                }
                                else
                                {
                                    if (descuentoIngresado > 100.0)
                                    {
                                        responseMovimiento.valor = -6;
                                        responseMovimiento.idMovimiento = 0;
                                        responseMovimiento.position = 0;
                                        //Toast.makeText(context, "Oops! el anexo 20 no permite descuentos mayores al 100% en comprobantes fiscales", Toast.LENGTH_SHORT).show();
                                    }
                                    else
                                    {
                                        discountRate = descuentoIngresado;
                                        monto = (capturedUnits * price);
                                        newAmountDiscount = (monto * discountRate) / 100;
                                        dynamic myMap = applyDiscountPromotionsLocal(documentType, itemModel,
                                                capturedUnits, monto);
                                        newAmountDiscount += myMap.newAmountDiscount;
                                        double totalItem = (monto - newAmountDiscount);
                                        responseMovimiento = addMovement(salesUnits, nonConvertibleUnits, capturedUnits, nonConvertibleUnitId,
                                                capturedUnitId, ItemModel.getCapturedUnitType(itemModel.id, capturedUnitId),
                                                monto, totalItem, myMap.rateDiscountPromo, newAmountDiscount, observation, permissionPrepedido);
                                    }
                                }
                            }
                            else
                            {
                                discountRate = 0;
                                monto = (capturedUnits * price);
                                newAmountDiscount = (monto * discountRate) / 100;
                                dynamic myMap = applyDiscountPromotionsLocal(documentType, itemModel,
                                        capturedUnits, monto);
                                newAmountDiscount += myMap.newAmountDiscount;
                                double totalItem = (monto - newAmountDiscount);
                                responseMovimiento = addMovement(salesUnits, nonConvertibleUnits, capturedUnits, nonConvertibleUnitId,
                                        capturedUnitId, ItemModel.getCapturedUnitType(itemModel.id, capturedUnitId),
                                        monto, totalItem, myMap.rateDiscountPromo, newAmountDiscount, observation, permissionPrepedido);
                            }
                        }
                        else
                        {
                            responseMovimiento.valor = 1;
                            responseMovimiento.idMovimiento = 0;
                            responseMovimiento.position = 0;
                        }
                    } else
                    {
                        if (!discountText.Equals(""))
                        {
                            discountText = discountText.Replace(",", "");
                            double descuentoIngresado = Convert.ToDouble(discountText);
                            if (descuentoIngresado > descMax)
                            {
                                responseMovimiento.valor = -5;
                                responseMovimiento.position = 0;
                                responseMovimiento.idMovimiento = 0;
                            }
                            else
                            {
                                if (descuentoIngresado > 100.0)
                                {
                                    responseMovimiento.valor = -6;
                                    responseMovimiento.position = 0;
                                    responseMovimiento.idMovimiento = 0;
                                }
                                else
                                {
                                    discountRate = descuentoIngresado;
                                    monto = (capturedUnits * price);
                                    newAmountDiscount = (monto * discountRate) / 100;
                                    dynamic myMap = applyDiscountPromotionsLocal(documentType, itemModel,
                                            capturedUnits, monto);
                                    newAmountDiscount += myMap.newAmountDiscount;
                                    double totalItem = (monto - newAmountDiscount);
                                    responseMovimiento = addMovement(salesUnits, nonConvertibleUnits, capturedUnits, nonConvertibleUnitId,
                                            capturedUnitId, ItemModel.getCapturedUnitType(itemModel.id, capturedUnitId),
                                            monto, totalItem, myMap.rateDiscountPromo, newAmountDiscount, observation, permissionPrepedido);
                                }
                            }
                        }
                        else
                        {
                            discountRate = 0;
                            monto = (capturedUnits * price);
                            newAmountDiscount = (monto * discountRate) / 100;
                            dynamic myMap = applyDiscountPromotionsLocal(documentType, itemModel,
                                    capturedUnits, monto);
                            newAmountDiscount += myMap.newAmountDiscount;
                            double totalItem = (monto - newAmountDiscount);
                            responseMovimiento = addMovement(salesUnits, nonConvertibleUnits, capturedUnits, nonConvertibleUnitId,
                                    capturedUnitId, ItemModel.getCapturedUnitType(itemModel.id, capturedUnitId),
                                    monto, totalItem, myMap.rateDiscountPromo, newAmountDiscount, observation, permissionPrepedido);
                        }
                    }
                }
                else if (documentType == DocumentModel.TIPO_DEVOLUCION)
                {
                    /** Logic for types Devoluciones */
                    if (itemModel.salesUnitId != capturedUnitId)
                    {
                        int capturedUnitIsMajor = -1;// ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(itemModel.salesUnitId, capturedUnitId);
                        if (serverModeLAN)
                        {
                            dynamic responseMajorConversion = await ConversionsUnitsController.checkIfTheCapturedUnitIsHigherLAN(itemModel.salesUnitId, capturedUnitId);
                            if (responseMajorConversion.value == 1)
                                capturedUnitIsMajor = responseMajorConversion.salesUnitIsHigher;
                        }
                        else capturedUnitIsMajor = ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(itemModel.salesUnitId, capturedUnitId);
                        if (capturedUnitIsMajor == 0)
                        {
                            /** Unidad de venta es menor: multiplicamos la base por el numero de conversión mayor */
                            double currentExistance = itemModel.existencia;//ItemModel.getTheCurrentExistenceOfAnItem(itemModel.id);
                            double majorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.salesUnitId, capturedUnitId, true);
                            double stockInMinorUnits = currentExistance * majorConversion;
                            double newStockInMinorUnits = stockInMinorUnits + capturedUnits;
                            double newStockMajorUnit = newStockInMinorUnits / majorConversion;
                            salesUnits = currentExistance - newStockMajorUnit;
                        }
                        else if (capturedUnitIsMajor == 1)
                        {
                            /** Unidad de venta es mayor: multiplicamos la base por el numero de conversión mayor */
                            double minorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.salesUnitId, capturedUnitId, false);
                            salesUnits = capturedUnits / minorConversion;
                        }
                        else if (capturedUnitIsMajor == 2)
                        {
                            salesUnits = capturedUnits;
                        }
                        else
                        {
                            salesUnits = capturedUnits;
                        }
                    }
                    else
                    {
                        salesUnits = capturedUnits;
                    }
                    if (!discountText.Equals(""))
                    {
                        discountText = discountText.Replace(",", "");
                        double descuentoIngresado = Convert.ToDouble(discountText);
                        if (descuentoIngresado > descMax)
                        {
                            responseMovimiento.valor = -5;
                            responseMovimiento.position = 0;
                            responseMovimiento.idMovimiento = 0;
                        }
                        else
                        {
                            if (descuentoIngresado > 100.0)
                            {
                                responseMovimiento.valor = -6;
                                responseMovimiento.position = 0;
                                responseMovimiento.idMovimiento = 0;
                            }
                            else
                            {
                                discountRate = descuentoIngresado;
                                monto = (capturedUnits * price);
                                newAmountDiscount = (monto * discountRate) / 100;
                                dynamic myMap = applyDiscountPromotionsLocal(documentType, itemModel,
                                        capturedUnits, monto);
                                newAmountDiscount += myMap.newAmountDiscount;
                                double totalItem = (monto - newAmountDiscount);
                                responseMovimiento = addMovement(salesUnits, nonConvertibleUnits, capturedUnits, nonConvertibleUnitId,
                                        capturedUnitId, ItemModel.getCapturedUnitType(itemModel.id, capturedUnitId),
                                        monto, totalItem, myMap.rateDiscountPromo, newAmountDiscount, observation, permissionPrepedido);
                            }
                        }
                    }
                    else
                    {
                        discountRate = 0;
                        monto = (capturedUnits * price);
                        newAmountDiscount = (monto * discountRate) / 100;
                        dynamic myMap = applyDiscountPromotionsLocal(documentType, itemModel,
                                capturedUnits, monto);
                        newAmountDiscount += myMap.newAmountDiscount;
                        double totalItem = (monto - newAmountDiscount);
                        responseMovimiento = addMovement(salesUnits, nonConvertibleUnits, capturedUnits, nonConvertibleUnitId,
                                capturedUnitId, ItemModel.getCapturedUnitType(itemModel.id, capturedUnitId),
                                monto, totalItem, myMap.rateDiscountPromo, newAmountDiscount, observation, permissionPrepedido);
                    }
                }
                else
                {
                    /** Logic for types Ventas and Ventas TPV */
                    double existencias = itemModel.existencia;
                    if (itemModel.salesUnitId != capturedUnitId)
                    {
                        int capturedUnitIsMajor = -1;
                        if (serverModeLAN)
                        {
                            dynamic responseMajorConversion = await ConversionsUnitsController.checkIfTheCapturedUnitIsHigherLAN(itemModel.salesUnitId, capturedUnitId);
                            if (responseMajorConversion.value == 1)
                                capturedUnitIsMajor = responseMajorConversion.salesUnitIsHigher;
                        } else capturedUnitIsMajor = ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(itemModel.salesUnitId, capturedUnitId);
                        if (capturedUnitIsMajor == 0)
                        {
                            /** Unidad de venta es menor: multiplicamos la base por el numero de conversión mayor */
                            double currentExistance = itemModel.existencia;//ItemModel.getTheCurrentExistenceOfAnItem(itemModel.id);
                            double majorConversion = 0;
                            if (serverModeLAN)
                            {
                                dynamic responseFactor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.salesUnitId, capturedUnitId, true);
                                if (responseFactor.value == 1)
                                    majorConversion = responseFactor.majorFactor;
                            } else majorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.salesUnitId, capturedUnitId, true);
                            double stockInMinorUnits = currentExistance * majorConversion;
                            double newStockInMinorUnits = stockInMinorUnits - capturedUnits;
                            double newStockMajorUnit = newStockInMinorUnits / majorConversion;
                            salesUnits = currentExistance - newStockMajorUnit;
                            if (capturedUnits > stockInMinorUnits)
                                insufficientStock = true;
                        }
                        else if (capturedUnitIsMajor == 1)
                        {
                            /** Unidad de venta es mayor: multiplicamos la base por el numero de conversión mayor */
                            double minorConversion = 0;
                            if (serverModeLAN)
                            {
                                dynamic responseFactor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.salesUnitId, capturedUnitId, false);
                                if (responseFactor.value == 1)
                                    minorConversion = responseFactor.majorFactor;
                            } else minorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.salesUnitId, capturedUnitId, false);
                            salesUnits = capturedUnits / minorConversion;
                            if (capturedUnits > existencias)
                            {
                                insufficientStock = true;
                            }
                        }
                        else if (capturedUnitIsMajor == 2)
                        {
                            salesUnits = capturedUnits;
                            if (capturedUnits > existencias)
                            {
                                insufficientStock = true;
                            }
                        }
                        else
                        {
                            salesUnits = capturedUnits;
                            if (capturedUnits > existencias)
                            {
                                insufficientStock = true;
                            }
                        }
                    }
                    else
                    {
                        if (itemModel.baseUnitId != capturedUnitId)
                        {
                            /** Unidades capturadas son iguales a la de venta */
                            int capturedUnitIsMajorThanTheBase = -1;// ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(itemModel.baseUnitId, capturedUnitId);
                            if (serverModeLAN)
                            {
                                dynamic responseMajorConversion = await ConversionsUnitsController.checkIfTheCapturedUnitIsHigherLAN(itemModel.baseUnitId, capturedUnitId);
                                if (responseMajorConversion.value == 1)
                                    capturedUnitIsMajorThanTheBase = responseMajorConversion.salesUnitIsHigher;
                            } else capturedUnitIsMajorThanTheBase = ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(itemModel.baseUnitId, capturedUnitId);
                            if (capturedUnitIsMajorThanTheBase == 0)
                            {
                                /** Unidades capturadas son mayores en equivalencia a las de base e igual a las de venta*/
                                double majorFactor = 0;// ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, true);
                                if (serverModeLAN)
                                {
                                    dynamic responseFactor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.baseUnitId, capturedUnitId, true);
                                    if (responseFactor.value == 1)
                                        majorFactor = responseFactor.majorFactor;
                                } else majorFactor = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, true);
                                double majorStock = (existencias * majorFactor);
                                if (capturedUnits > majorStock)
                                {
                                    insufficientStock = true;
                                }
                                else
                                {
                                    salesUnits = capturedUnits;
                                }
                            }
                            else if (capturedUnitIsMajorThanTheBase == 1)
                            {
                                /** Unidades capturadas son menores a las de base e iguales a las de venta */
                                double majorFactor = 0;// ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, true);
                                if (serverModeLAN)
                                {
                                    dynamic responseFactor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.baseUnitId, capturedUnitId, true);
                                    if (responseFactor.value == 1)
                                        majorFactor = responseFactor.majorFactor;
                                } else majorFactor = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, true);
                                double minorStock = (existencias * majorFactor);
                                if (capturedUnits > minorStock)
                                {
                                    insufficientStock = true;
                                }
                                else
                                {
                                    salesUnits = capturedUnits;
                                }
                            }
                            else
                            {
                                //Unidades capturadas son iguales que las de base
                                if (existencias >= capturedUnits)
                                {
                                    salesUnits = capturedUnits;
                                }
                                else
                                {
                                    insufficientStock = true;
                                }
                            }
                        }
                        else
                        {
                            if (capturedUnits > existencias)
                                insufficientStock = true;
                            else salesUnits = capturedUnits;
                        }
                    }
                    if (!discountText.Equals(""))
                    {
                        discountText = discountText.Replace(",", "");
                        double descuentoIngresado = Convert.ToDouble(discountText);
                        if (descuentoIngresado > descMax)
                        {
                            responseMovimiento.valor = -5;
                            responseMovimiento.position = 0;
                            responseMovimiento.idMovimiento = 0;
                        }
                        else
                        {
                            discountRate = descuentoIngresado;
                            monto = (capturedUnits * price);
                            newAmountDiscount = (monto * discountRate) / 100;
                            dynamic myMap = applyDiscountPromotionsLocal(documentType, itemModel,
                                    capturedUnits, monto);
                            newAmountDiscount += myMap.newAmountDiscount;
                            double totalItem = (monto - newAmountDiscount);
                            responseMovimiento = addMovement(salesUnits, nonConvertibleUnits, capturedUnits, nonConvertibleUnitId,
                                    capturedUnitId, ItemModel.getCapturedUnitType(itemModel.id, capturedUnitId),
                                    monto, totalItem, myMap.rateDiscountPromo, newAmountDiscount, observation, permissionPrepedido);
                        }
                    }
                    else
                    {
                        discountRate = 0;
                        monto = (capturedUnits * price);
                        newAmountDiscount = (monto * discountRate) / 100;
                        dynamic myMap = applyDiscountPromotionsLocal(documentType, itemModel,
                                capturedUnits, monto);
                        newAmountDiscount += myMap.newAmountDiscount;
                        double totalItem = (monto - newAmountDiscount);
                        responseMovimiento = addMovement(salesUnits, nonConvertibleUnits, capturedUnits, nonConvertibleUnitId,
                                capturedUnitId, ItemModel.getCapturedUnitType(itemModel.id, capturedUnitId), monto, totalItem,
                                myMap.rateDiscountPromo, newAmountDiscount, observation, permissionPrepedido);
                    }
                }
                if (responseMovimiento.valor > 0)
                {
                    response.valor = responseMovimiento.valor;
                    response.idDocument = idDocument;
                    response.descMax = descMax;
                    response.position = responseMovimiento.position;
                    if (insufficientStock && (documentType == DocumentModel.TIPO_VENTA || documentType == DocumentModel.TIPO_REMISION))
                        response.noStock = 1;
                    else
                        response.noStock = 0;
                    response.idMovimiento = responseMovimiento.idMovimiento;
                }
                else
                {
                    response.valor = responseMovimiento.valor;
                    response.idDocument = idDocument;
                    response.descMax = descMax;
                    response.position = 0;
                    if (insufficientStock)
                        response.noStock = 0;
                    else
                        response.noStock = 0;
                    response.idMovimiento = responseMovimiento.idMovimiento;
                }
            });
            return response;
        }

        private ExpandoObject addMovement(double baseUnits, double nonConvertibleUnits, double capturedUnits, int nonConvertibleUnitId,
                            int capturedUnitId, int capturedUnitType, double monto, double total, double rateDiscountPromo, double newAmountDiscount,
                            String observation, bool permissionPrepedido)
        {
            dynamic response = new ExpandoObject();
            if (LicenseModel.isItTPVLicense() && idDocument == 0)
            {
                if (permissionPrepedido)
                {
                    documentType = DocumentModel.TIPO_PREPEDIDO;
                    FormVenta.documentType = documentType;
                }
                createDocument();
                FormVenta.idDocument = idDocument;
            }
            DocumentModel dvm = DocumentModel.getAllDataDocumento(idDocument);
            newAmountDiscount = Convert.ToDouble(String.Format("{0:0.00}", newAmountDiscount));
            if (dvm != null)
            {
                if (permissionPrepedido)
                {
                    if (!DocumentModel.isItDocumentFromAPrepedido(idDocument))
                    {
                        capturedUnits = 0;
                    }
                }
                MovimientosModel mdm = MovimientosModel.armMovement(itemModel, idDocument,
                    baseUnits, nonConvertibleUnits, capturedUnits, nonConvertibleUnitId, capturedUnitId, capturedUnitType,
                    price, monto, total, documentType, dvm.nombreu, discountRate,
                    newAmountDiscount, observation, dvm.usuario_id, rateDiscountPromo);
                if (mdm != null)
                {
                    dynamic responseMovimiento = agregarArticuloAlCarrito(mdm, newAmountDiscount, itemModel.id, capturedUnitId);
                    response.valor = responseMovimiento.value;
                    response.idMovimiento = responseMovimiento.idMovimiento;
                    response.position = mdm.position;
                }
                else
                {
                    response.valor = 0;
                    response.idMovimiento = 0;
                    response.position = 0;
                }
            } else
            {
                response.valor = 0;
                response.idMovimiento = 0;
                response.position = 0;
            }
            return response;
        }

        private void createDocument()
        {
            UserModel usuarios = UserModel.datosUsuarioParaElDocumento();
            if (usuarios != null)
            {
                String clave = CustomerModel.getClaveForAClient(FormVenta.idCustomer);
                DocumentModel dvm = new DocumentModel();
                dvm.clave_cliente = clave;
                if (FormVenta.idCustomer < 0)
                {
                    int idCustomerPanel = CustomerADCModel.getNewClientIdPanel(FormVenta.idCustomer);
                    if (idCustomerPanel > 0)
                    {
                        String newCustomerIdPanel = "-" + idCustomerPanel;
                        dvm.cliente_id = Convert.ToInt32(newCustomerIdPanel);
                    } else
                    {
                        dvm.cliente_id = FormVenta.idCustomer;
                    }
                } else
                {
                    dvm.cliente_id = FormVenta.idCustomer;
                }
                dvm.nombreu = usuarios.Nombre;
                dvm.almacen_id = usuarios.almacen_id;
                dvm.tipo_documento = documentType;
                dvm.factura = 0;
                dvm.fventa = "A"+ClsRegeditController.getIdUserInTurn()+"C"+FormVenta.idCustomer + MetodosGenerales.getCurrentDateAndHourForFolioVenta();
                dvm.usuario_id = usuarios.id;
                dvm.ciddoctopedidocc = 0;
                dvm.pausado = 1;
                if (idDocument == 0)
                    idDocument = 1;
                idDocument = DocumentModel.addNewDocument(dvm, idDocument);
            }
        }

        private ExpandoObject agregarArticuloAlCarrito(MovimientosModel mdm, double newAmountDiscount, int idArt, int capturedUnitId)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            int idMovimientoAgregado = 0;
            int idCreated = MovimientosModel.addNewMovimiento(mdm, newAmountDiscount);
            if (idCreated >= 1)
            {
                idMovimientoAgregado = idCreated;
                int rDisminuyoLaExistencia = MovimientosModel.decreaseOrIncreaseExistenceOfAnItem(idDocument, itemModel, capturedUnits, capturedUnitId,
                    mdm.price, newAmountDiscount, documentType, idCreated, serverModeLAN);
                if (rDisminuyoLaExistencia > 0)
                {
                    if (serverModeLAN)
                    {
                        validarSiEsUnPrepedidoDePolloVivoONo(itemModel);
                        FormasDeCobroDocumentoModel.removePendingBalanceToTheLastFormOfCollectionOfTheDocument(idDocument);
                        value = 1;
                    } else
                    {
                        if (ItemModel.addAnItemToCart(idArt))
                        {
                            validarSiEsUnPrepedidoDePolloVivoONo(itemModel);
                            FormasDeCobroDocumentoModel.removePendingBalanceToTheLastFormOfCollectionOfTheDocument(idDocument);
                            value = 1;
                        }
                        else
                        {
                            value = -3;
                            //Toast.makeText(context, "Oops, currio un error al agregar el articulo!", Toast.LENGTH_SHORT).show();
                        }
                    }
                }
                else if (rDisminuyoLaExistencia == -1)
                {
                    if (serverModeLAN)
                    {
                        validarSiEsUnPrepedidoDePolloVivoONo(itemModel);
                        FormasDeCobroDocumentoModel.removePendingBalanceToTheLastFormOfCollectionOfTheDocument(idDocument);
                        value = 1;
                    } else
                    {
                        if (ItemModel.addAnItemToCart(idArt))
                        {
                            validarSiEsUnPrepedidoDePolloVivoONo(itemModel);
                            FormasDeCobroDocumentoModel.removePendingBalanceToTheLastFormOfCollectionOfTheDocument(idDocument);
                            value = 1;
                        }
                        else
                        {
                            value = -3;
                            //Toast.makeText(context, "Oops, ocurrió un error al agregar el articulo!", Toast.LENGTH_SHORT).show();
                        }
                    }
                }
                else
                {
                    value = -2;
                    //Toast.makeText(context, "Oops, ocurrió un error al actualizar las existencias!", Toast.LENGTH_SHORT).show();
                }
                //actualizarArticuloAgregado(context, position, idArt);
            } else if (idCreated == -1) {
                value = 2;
                //Existe un movimiento con el mismo descuento
            } else {
                value = -1;
                //Toast.makeText(context, "Error al agregar el movimiento a la base de datos!", Toast.LENGTH_SHORT).show();
            }
            response.value = value;
            response.idMovimiento = idMovimientoAgregado;
            return response;
        }

        private void validarSiEsUnPrepedidoDePolloVivoONo(ClsItemModel itemModel)
        {
            bool isPolloVivo = false;
            if (documentType == DocumentModel.TIPO_PREPEDIDO)
            {
                if (itemModel != null)
                {
                    if (itemModel.clasificacionId1 != 0)
                    {
                        String codeClasification = ClasificationsValueModel.getCodeForAClasification(itemModel.clasificacionId1);
                        if (codeClasification.Equals("PV1"))
                            isPolloVivo = true;
                    }
                    else if (itemModel.clasificacionId2 != 0)
                    {
                        String codeClasification = ClasificationsValueModel.getCodeForAClasification(itemModel.clasificacionId2);
                        if (codeClasification.Equals("PV1"))
                            isPolloVivo = true;
                    }
                    else if (itemModel.clasificacionId3 != 0)
                    {
                        String codeClasification = ClasificationsValueModel.getCodeForAClasification(itemModel.clasificacionId3);
                        if (codeClasification.Equals("PV1"))
                            isPolloVivo = true;
                    }
                    else if (itemModel.clasificacionId4 != 0)
                    {
                        String codeClasification = ClasificationsValueModel.getCodeForAClasification(itemModel.clasificacionId4);
                        if (codeClasification.Equals("PV1"))
                            isPolloVivo = true;
                    }
                    else if (itemModel.clasificacionId5 != 0)
                    {
                        String codeClasification = ClasificationsValueModel.getCodeForAClasification(itemModel.clasificacionId5);
                        if (codeClasification.Equals("PV1"))
                            isPolloVivo = true;
                    }
                    else if (itemModel.clasificacionId6 != 0)
                    {
                        String codeClasification = ClasificationsValueModel.getCodeForAClasification(itemModel.clasificacionId6);
                        if (codeClasification.Equals("PV1"))
                            isPolloVivo = true;
                    }
                }
                if (isPolloVivo)
                {
                    DocumentModel.updateInvoiceField(idDocument, 1);
                }
                else
                {
                    DocumentModel.updateInvoiceField(idDocument, 0);
                }
            }
        }


        private ExpandoObject applyDiscountPromotionsLocal(int documentType, ClsItemModel itemModel, double cantidadArticulo, double monto)
        {
            dynamic myMap = new ExpandoObject();
            double rateDiscountPromo = 0;
            double newAmountDiscount = 0;
            if (documentType != 5)
            {
                dynamic rateAndDiscountList = PromotionsModel.logicForAplyPromotions(itemModel, cantidadArticulo, monto, serverModeLAN);
                if (rateAndDiscountList != null)
                {
                    if (rateAndDiscountList.aplica == "1")
                    {
                        rateDiscountPromo = Convert.ToDouble(rateAndDiscountList.porcentaje);
                        double promotionDiscount = Convert.ToDouble(rateAndDiscountList.importe);
                        newAmountDiscount = promotionDiscount;
                    }
                    else if (rateAndDiscountList.aplica == "-1")
                    {
                        rateDiscountPromo = 0;
                    }
                    else if (rateAndDiscountList.aplica == "-2")
                    {
                        rateDiscountPromo = 0;
                    }
                    else
                    {
                        rateDiscountPromo = 0;
                    }
                }
            }
            myMap.rateDiscountPromo = rateDiscountPromo;
            myMap.newAmountDiscount = newAmountDiscount;
            return myMap;
        }

    }
}
