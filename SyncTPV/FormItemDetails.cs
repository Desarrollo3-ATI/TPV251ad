using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using SyncTPV.Views.Extras;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using Tulpep.NotificationWindow;
using wsROMClase;
using wsROMClases.Helpers;
using wsROMClases.Models.Commercial;
using wsROMClases.Models.Panel;

namespace SyncTPV
{
    public partial class FormItemDetails : Form
    {
        int idItem = 0;
        string MensajeError = "";
        int Error = 0;
        FormMessage msj;
        private String stock = "", comInstance = "";
        private bool serverModeLAN = false;
        private int positionFiscalItemFeld = 6;
        private bool cotmosActive = false;
        private String codigoCaja = "";

        public FormItemDetails(int itemId, bool cotmosActive)
        {
            InitializeComponent();
            idItem = itemId;
            this.cotmosActive = cotmosActive;
        }

        private async void frmArticuloInfo_Load(object sender, EventArgs e)
        {
            await loadInitialData();
            getCurrentInformationForThisItem();
        }

        private async Task loadInitialData()
        {
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
                comInstance = InstanceSQLSEModel.getStringComInstance();
                positionFiscalItemFeld = ConfiguracionModel.getPositionFiscalItemField();
                if (cotmosActive)
                {
                    dynamic responseConfig = ConfiguracionModel.getCodigoCajaPadre();
                    if (responseConfig.value == 1)
                    {
                        value = 1;
                        codigoCaja = responseConfig.code;
                    } else
                    {
                        value = responseConfig.value;
                        description = responseConfig.description;
                    }
                } else
                {
                    value = 1;
                    codigoCaja = UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn());
                }
            });
            if (value == 1)
            {

            } else
            {
                FormMessage formMessage = new FormMessage("Configuración", description, 3);
                formMessage.ShowDialog();
            }
        }

        private async Task getCurrentInformationForThisItem()
        {
            if (serverModeLAN)
            {
                dynamic response = await ItemsController.getAnItemFromTheServerLAN(idItem, this, codigoCaja);
                if (response.value == 1)
                {
                    fillServerData(response.item);
                    fillTextAllPrices(response.item);
                    downloadImageItem();
                } else
                {
                    FormMessage formMessage = new FormMessage("Exception", response.description, 2);
                    formMessage.ShowDialog();
                }
            }
            else
            {
                dynamic response = await ItemsController.getAnItemFromTheServerAPI(idItem, this, codigoCaja);
                if (response.value == 1)
                {
                    fillServerData(response.item);
                    fillTextAllPrices(response.item);
                    downloadImageItem();
                }
            }
        }

        public async Task updateUIProgressBar(int valuePb, bool showPb)
        {
            await Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => {
                progressBarLoadItemDetail.Value = valuePb;
                progressBarLoadItemDetail.Visible = showPb;
                if (!showPb && serverModeLAN == false)
                {
                    //fillLocalData();
                    //fillTextAllPrices(null);
                    
                }
            }), DispatcherPriority.Background, null);
        }

        private void picCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUploadImg_Click(object sender, EventArgs e)
        {
            FormPasswordConfirmation formPasswordConfirmation = new FormPasswordConfirmation("Acceso Supervisor", "Ingresa la contraseña del supervisor");
            formPasswordConfirmation.StartPosition = FormStartPosition.CenterScreen;
            formPasswordConfirmation.ShowDialog();
            if (FormPasswordConfirmation.permissionGranted)
            {
                updateImageProcess();
            }
        }

        private async Task updateImageProcess()
        {
            string rutaArchivoLocal = "";
            openFileDialogSearchItemImage.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *jpeg; *.jpe; *.jfif; *.png";
            openFileDialogSearchItemImage.Title = "Seleccionar Imágen ...";
            if (openFileDialogSearchItemImage.ShowDialog() == DialogResult.OK)
            {
                rutaArchivoLocal = openFileDialogSearchItemImage.FileName;
                String rutaImagenes = MetodosGenerales.rootDirectory + "\\Imagenes";
                if (!Directory.Exists(rutaImagenes))
                    Directory.CreateDirectory(rutaImagenes);
                String rutaCarpetaLocal = MetodosGenerales.rootDirectory + "\\Imagenes\\items";
                if (!Directory.Exists(rutaCarpetaLocal))
                    Directory.CreateDirectory(rutaCarpetaLocal);
                String destFile = Path.Combine(rutaCarpetaLocal, idItem + "-1.jpg");
                try
                {
                    FileStream fsSal = new FileStream(rutaArchivoLocal, FileMode.Open, FileAccess.Read);
                    FileStream fsDest = new FileStream(destFile, FileMode.Create, FileAccess.Write);
                    fsSal.CopyTo(fsDest);
                    fsSal.Close();
                    fsDest.Close();
                    //File.Copy(rutaArchivoLocal, destFile, true);
                    dynamic responseImage = null;
                    if (serverModeLAN)
                        responseImage = await SubirImagenesController.uploadImagenesViaLAN("items", rutaCarpetaLocal, idItem);
                    else responseImage = await SubirImagenesController.uploadImageAPI("items", rutaCarpetaLocal, idItem);       
                    if (responseImage.value == 1)
                    {
                        string rutaImagenLocal = MetodosGenerales.rootDirectory + "\\Imagenes\\items\\" + idItem + "-1.jpg";
                        try
                        {
                            FileStream fs = new FileStream(rutaImagenLocal, FileMode.Open, FileAccess.Read);
                            pictureArticulo.Image = MetodosGenerales.redimencionarImagenes(Image.FromStream(fs), 162, 137);
                            fs.Close();
                        }
                        catch (Exception ex)
                        {
                            SECUDOC.writeLog(ex.ToString());
                            rutaImagenLocal = MetodosGenerales.rootDirectory + "\\Imagenes\\Estaticas\\SyncTPV.png";
                            FileStream fs = new FileStream(rutaImagenLocal, FileMode.Open, FileAccess.Read);
                            pictureArticulo.Image = MetodosGenerales.redimencionarImagenes(Image.FromStream(fs), 162, 137);
                            fs.Close();
                        }
                        msj = new FormMessage("Carga de imagen exitosa", "Imágen actualizada correctamente", 1);
                        msj.ShowDialog();
                    }
                    else
                    {
                        msj = new FormMessage("Carga de imagen fallida", responseImage.description, 3);
                        msj.ShowDialog();
                    }
                } catch (IOException e)
                {
                    SECUDOC.writeLog(e.ToString());
                    msj = new FormMessage("Carga de imagen fallida", e.Message, 3);
                    msj.ShowDialog();
                }
            }
        }

        private async Task downloadImageItem()
        {
            await Task.Run(async () =>
            {
                if (serverModeLAN)
                {
                    String rutaLocal = MetodosGenerales.rootDirectory + "\\Imagenes\\items";
                    await SubirImagenesController.getImageLAN("items", rutaLocal, idItem);
                }
                else await SubirImagenesController.getImage("-1", idItem);
            });
            LlenarImagen(idItem);
        }

        private void pictureArticulo_Click(object sender, EventArgs e)
        {
            FormImagen formImagen = new FormImagen(0, idItem);
            formImagen.StartPosition = FormStartPosition.CenterScreen;
            formImagen.ShowDialog();
        }

        /*public async Task fillLocalData()
        {
            ClsItemModel item = await getAnItem();
            if (item != null)
            {
                editCodigoItem.Text = item.codigo;
                editNombreItem.Text = item.nombre;
                editExistenciaItem.Text = stock;
                editDescuentoItem.Text = item.descuentoMaximo + "%";
                if (item.baseUnitId != 0)
                {
                    string query = "SELECT " + LocalDatabase.CAMPO_NAME_UNITMEASUREWEIGHT + " FROM " + LocalDatabase.TABLA_UNITMEASUREWEIGHT +
                        " WHERE " + LocalDatabase.CAMPO_IDSERVER_UNITMEASUREWEIGHT + " = " + item.baseUnitId;
                    String baseUnitName = UnitsOfMeasureAndWeightModel.getStringValueFromUnitsMeasureAndWeight(query);
                    textBaseUnit.Text = "Unidad Base: " + baseUnitName;
                }
                else textBaseUnit.Text = "Sin Unidad Base";
                if (item.nonConvertibleUnitId != 0)
                {
                    string query = "SELECT " + LocalDatabase.CAMPO_NAME_UNITMEASUREWEIGHT + " FROM " + LocalDatabase.TABLA_UNITMEASUREWEIGHT +
                        " WHERE " + LocalDatabase.CAMPO_IDSERVER_UNITMEASUREWEIGHT + " = " + item.nonConvertibleUnitId;
                    String nonConvertibleUnitName = UnitsOfMeasureAndWeightModel.getStringValueFromUnitsMeasureAndWeight(query);
                    textNonConvertibleUnit.Text = "Unidad No Convertible: " + nonConvertibleUnitName;
                }
                else textNonConvertibleUnit.Text = "Sin Unidad No Convertible";
                if (item.purchaseUnitId != 0)
                {
                    string query = "SELECT " + LocalDatabase.CAMPO_NAME_UNITMEASUREWEIGHT + " FROM " + LocalDatabase.TABLA_UNITMEASUREWEIGHT +
                        " WHERE " + LocalDatabase.CAMPO_IDSERVER_UNITMEASUREWEIGHT + " = " + item.purchaseUnitId;
                    String purchaseUnitName = UnitsOfMeasureAndWeightModel.getStringValueFromUnitsMeasureAndWeight(query);
                    textPurchaseUnit.Text = "Unidad de Compra: " + purchaseUnitName;
                }
                else textPurchaseUnit.Text = "Sin Unidad de Compra";
                if (item.salesUnitId != 0)
                {
                    string query = "SELECT " + LocalDatabase.CAMPO_NAME_UNITMEASUREWEIGHT + " FROM " + LocalDatabase.TABLA_UNITMEASUREWEIGHT +
                        " WHERE " + LocalDatabase.CAMPO_IDSERVER_UNITMEASUREWEIGHT + " = " + item.salesUnitId;
                    String salesUnitName = UnitsOfMeasureAndWeightModel.getStringValueFromUnitsMeasureAndWeight(query);
                    textSalesUnit.Text = "Unidad de Venta: " + salesUnitName;
                }
                else textSalesUnit.Text = "Sin Unidad de Venta";
                if (item.fiscalProduct == 0)
                {
                    textProductFiscalNoFiscal.Text = "Producto No Fiscal";
                    textProductFiscalNoFiscal.ForeColor = Color.FromArgb(240, 127, 113);
                }
                else
                {
                    textProductFiscalNoFiscal.Text = "Producto Fiscal";
                    textProductFiscalNoFiscal.ForeColor = Color.FromArgb(113, 173, 240);
                }
            }
        }*/

        public async Task fillServerData(ClsItemModel im)
        {
            ClsItemModel item = null;
            if (im == null)
                item = await getAnItemServerLAN(im);
            else item = im;
            if (item != null)
            {
                int salesUnitIsMajor = -1;
                double conversionFactor = 0;
                editCodigoItem.Text = item.codigo;
                editNombreItem.Text = item.nombre;
                editDescuentoItem.Text = item.descuentoMaximo + "%";
                String baseUnitName = ClsUnitsOfMeasureAndWeightModel.getNameFromAnUnitsMeasureAndWeight(comInstance, item.baseUnitId);
                if (item.baseUnitId != 0)
                {
                    textBaseUnit.Text = "Unidad Base: " + baseUnitName;
                }
                else textBaseUnit.Text = "Sin Unidad Base";
                if (item.nonConvertibleUnitId != 0)
                {
                    String nonConvertibleUnitName = ClsUnitsOfMeasureAndWeightModel.getNameFromAnUnitsMeasureAndWeight(comInstance, 
                        item.nonConvertibleUnitId);
                    textNonConvertibleUnit.Text = "Unidad No Convertible: " + nonConvertibleUnitName;
                }
                else textNonConvertibleUnit.Text = "Sin Unidad No Convertible";
                if (item.purchaseUnitId != 0)
                {
                    String purchaseUnitName = ClsUnitsOfMeasureAndWeightModel.getNameFromAnUnitsMeasureAndWeight(comInstance, item.purchaseUnitId);
                    textPurchaseUnit.Text = "Unidad de Compra: " + purchaseUnitName;
                }
                else textPurchaseUnit.Text = "Sin Unidad de Compra";
                String unitSaleName = ClsUnitsOfMeasureAndWeightModel.getNameFromAnUnitsMeasureAndWeight(comInstance, item.salesUnitId);
                if (item.salesUnitId != 0)
                {
                    textSalesUnit.Text = "Unidad de Venta: " + unitSaleName;
                }
                else textSalesUnit.Text = "Sin Unidad de Venta";
                bool isFiscal = await ItemModel.getFiscalItemFieldValue(item, positionFiscalItemFeld);
                if (isFiscal)
                {
                    textProductFiscalNoFiscal.Text = "Producto comercial";
                    textProductFiscalNoFiscal.ForeColor = Color.FromArgb(113, 173, 240);
                } else
                {
                    textProductFiscalNoFiscal.Text = "Producto de control";
                    textProductFiscalNoFiscal.ForeColor = Color.FromArgb(240, 127, 113);
                }
                if (serverModeLAN)
                {
                    dynamic responseUnitHegher = await ConversionsUnitsController.checkIfTheCapturedUnitIsHigherLAN(im.baseUnitId, im.salesUnitId);
                    if (responseUnitHegher.value == 1)
                        salesUnitIsMajor = responseUnitHegher.salesUnitIsHigher;
                }
                else
                {
                    dynamic responseUnitHegher = await ConversionsUnitsController.checkIfTheCapturedUnitIsHigherAPI(im.baseUnitId, im.salesUnitId);
                    if (responseUnitHegher.value == 1)
                        salesUnitIsMajor = responseUnitHegher.salesUnitIsHigher;
                    else salesUnitIsMajor = ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(im.baseUnitId, im.salesUnitId);
                }
                if (item.existencia > 0)
                {
                    if (salesUnitIsMajor == 1)
                    {
                        if (serverModeLAN)
                        {
                            dynamic responseConversion = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(im.baseUnitId, 
                                im.salesUnitId, true);
                            if (responseConversion.value == 1)
                                conversionFactor = responseConversion.majorFactor;
                        }
                        else
                        {
                            dynamic responseConversion = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemAPI(im.baseUnitId,
                                im.salesUnitId, true);
                            if (responseConversion.value == 1)
                                conversionFactor = responseConversion.majorFactor;
                            else conversionFactor = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(im.baseUnitId, im.salesUnitId, true);
                        }
                        double stockUnidadVenta = item.existencia;
                        if (conversionFactor != 0)
                            stockUnidadVenta = (item.existencia / conversionFactor);
                        stock = "Comercial Premium: Base: " + MetodosGenerales.obtieneDosDecimales(item.existencia) + " " + baseUnitName +
                                "\r\nVenta: " + MetodosGenerales.obtieneDosDecimales(stockUnidadVenta) + " " +
                                unitSaleName;
                    }
                    else if (salesUnitIsMajor == 0)
                    {
                        double stockUnidadVenta = item.existencia;
                        if (conversionFactor != 0)
                            stockUnidadVenta = (item.existencia * conversionFactor);
                        stock = "Comercial Premium: Base: " + MetodosGenerales.obtieneDosDecimales(item.existencia) + " " + baseUnitName +
                                "\r\nVenta: " + MetodosGenerales.obtieneDosDecimales(stockUnidadVenta) + " " +
                                unitSaleName;
                    }
                    else if (salesUnitIsMajor == 2)
                    {
                        stock = "Comercial Premium: " + MetodosGenerales.obtieneDosDecimales(item.existencia) + " " + baseUnitName;
                    }
                    else
                    {
                        if (baseUnitName.Equals(""))
                        {
                            stock = "Comercial Premium: " + MetodosGenerales.obtieneDosDecimales(item.existencia) + " Unidades";
                        }
                        else
                        {
                            stock = "Comercial Premium: " + MetodosGenerales.obtieneDosDecimales(item.existencia) + " " + baseUnitName;
                        }
                    }
                }
                else
                {
                    //colorStock = ContextCompat.getColor(context, R.color.toolbarBackgoruund);
                    if (salesUnitIsMajor == 1)
                    {
                        if (serverModeLAN)
                        {
                            dynamic responseConversion = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(im.baseUnitId,
                                im.salesUnitId, true);
                            if (responseConversion.value == 1)
                                conversionFactor = responseConversion.majorFactor;
                        }
                        else
                        {
                            dynamic responseConversion = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemAPI(im.baseUnitId,
                                im.salesUnitId, true);
                            if (responseConversion.value == 1)
                                conversionFactor = responseConversion.majorFactor;
                            else conversionFactor = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(im.baseUnitId, im.salesUnitId, true);
                        }
                        double stockUnidadVenta = item.existencia;
                        if (conversionFactor != 0)
                            stockUnidadVenta = (item.existencia / conversionFactor);
                        stock = "Comercial Premium: Base: " + MetodosGenerales.obtieneDosDecimales(item.existencia) + " " + baseUnitName +
                                "\r\nVenta: " + MetodosGenerales.obtieneDosDecimales(stockUnidadVenta) + " " +
                                unitSaleName;
                    }
                    else if (salesUnitIsMajor == 0)
                    {
                        if (serverModeLAN)
                        {
                            dynamic responseConversion = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(im.baseUnitId,
                                im.salesUnitId, true);
                            if (responseConversion.value == 1)
                                conversionFactor = responseConversion.majorFactor;
                        }
                        else
                        {
                            dynamic responseConversion = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemAPI(im.baseUnitId,
                                im.salesUnitId, true);
                            if (responseConversion.value == 1)
                                conversionFactor = responseConversion.majorFactor;
                            else conversionFactor = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(im.baseUnitId, im.salesUnitId, true);
                        }
                        double stockUnidadVenta = item.existencia;
                        if (conversionFactor != 0)
                            stockUnidadVenta = (item.existencia * conversionFactor);
                        stock = "Comercial Premium: Base: " + MetodosGenerales.obtieneDosDecimales(item.existencia) + " " + baseUnitName + "" +
                            "\r\nExistencia Venta: " +
                                MetodosGenerales.obtieneDosDecimales(stockUnidadVenta) + " " +unitSaleName;
                    }
                    else if (salesUnitIsMajor == 2)
                    {
                        stock = "Comercial Premium: " + MetodosGenerales.obtieneDosDecimales(item.existencia) + " " + baseUnitName;
                    }
                    else
                    {
                        if (baseUnitName.Equals(""))
                        {
                            stock = "Comercial Premium: " + MetodosGenerales.obtieneDosDecimales(item.existencia) + " Unidades";
                        }
                        else
                        {
                            stock = "Comercial Premium: " + MetodosGenerales.obtieneDosDecimales(item.existencia) + " " + baseUnitName;
                        }
                    }
                }
                double unidadesPendientesServer = 0;
                double unidadesPendientesLocal = 0;
                double existenciaReal = 0;
                if (serverModeLAN)
                {
                    dynamic responseUnitsPendings = await ItemsController.getUnitsPendingsLAN(idItem);
                    if (responseUnitsPendings.value == 1)
                    {
                        unidadesPendientesServer = responseUnitsPendings.unidadesPendientes;
                    }
                } else
                {
                    dynamic responseUnitsPendings = await ItemsController.getUnitsPendingsAPI(idItem, codigoCaja);
                    if (responseUnitsPendings.value == 1)
                    {
                        unidadesPendientesServer = responseUnitsPendings.unidadesPendientes;
                    }
                }
                double unidadesPendientesLocales = MovimientosModel.getUnidadesBasePendientesLocales(item.id, 0, true);
                existenciaReal = (item.existencia - unidadesPendientesServer);
                existenciaReal = (existenciaReal - unidadesPendientesLocales);
                editExistenciaItem.Text = stock+"\r\nUnidades pendientes (Panel): "+ MetodosGenerales.obtieneDosDecimales(unidadesPendientesServer) +
                    "\r\nUnidades pendientes (local): "+ MetodosGenerales.obtieneDosDecimales(unidadesPendientesLocales) +
                    "\r\nExistencia Real: "+ MetodosGenerales.obtieneDosDecimales(existenciaReal);
            }
        }

        private async Task<ClsItemModel> getAnItem()
        {
            ClsItemModel item = null;
            await Task.Run(async () =>
            {
                item = ItemModel.getAllDataFromAnItem(idItem);
                double conversionFactor = 0;
            });
            return item;
        }

        private async Task<ClsItemModel> getAnItemServerLAN(ClsItemModel item)
        {
            await Task.Run(async () =>
            {
                double conversionFactor = 0;
                int salesUnitIsMajor = 0;
                int baseUnitId = item.baseUnitId;
                int salesUnitId = item.salesUnitId;
                String baseUnitName = ClsUnitsOfMeasureAndWeightModel.getNameFromAnUnitsMeasureAndWeight(comInstance, baseUnitId);
                String unitSaleName = ClsUnitsOfMeasureAndWeightModel.getNameFromAnUnitsMeasureAndWeight(comInstance, salesUnitId);
                salesUnitIsMajor = ClsConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(comInstance, baseUnitId, salesUnitId);
                if (salesUnitIsMajor == 1)
                {
                    conversionFactor = ClsConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(comInstance, baseUnitId, salesUnitId, true);
                }
                else if (salesUnitIsMajor == 0)
                {
                    conversionFactor = ClsConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(comInstance, baseUnitId, salesUnitId, false);
                }
                if (item.existencia > 0)
                {
                    if (salesUnitIsMajor == 1)
                    {
                        stock = "Base: " + MetodosGenerales.obtieneDosDecimales(item.existencia) + " " + baseUnitName +
                                " Venta: " + MetodosGenerales.obtieneDosDecimales((item.existencia / conversionFactor)) + " " +
                                unitSaleName;
                    }
                    else if (salesUnitIsMajor == 0)
                    {
                        stock = "Base: " + MetodosGenerales.obtieneDosDecimales(item.existencia) + " " + baseUnitName +
                                " Venta: " + MetodosGenerales.obtieneDosDecimales((item.existencia / conversionFactor)) + " " +
                                unitSaleName;
                    }
                    else if (salesUnitIsMajor == 2)
                    {
                        stock = "" + MetodosGenerales.obtieneDosDecimales(item.existencia) + " " + baseUnitName;
                    }
                    else
                    {
                        if (baseUnitName.Equals(""))
                        {
                            stock = "" + MetodosGenerales.obtieneDosDecimales(item.existencia) + " Unidades";
                        }
                        else
                        {
                            stock = "" + MetodosGenerales.obtieneDosDecimales(item.existencia) + " " + baseUnitName;
                        }
                    }
                }
                else
                {
                    //colorStock = ContextCompat.getColor(context, R.color.toolbarBackgoruund);
                    if (salesUnitIsMajor == 1)
                    {
                        stock = "Base: " + MetodosGenerales.obtieneDosDecimales(item.existencia) + " " + baseUnitName +
                                " Venta: " + MetodosGenerales.obtieneDosDecimales((item.existencia / conversionFactor)) + " " +
                                unitSaleName;
                    }
                    else if (salesUnitIsMajor == 0)
                    {
                        stock = "Base: " + MetodosGenerales.obtieneDosDecimales(item.existencia) + " " + baseUnitName + "\nExistencia Venta: " +
                                MetodosGenerales.obtieneDosDecimales((item.existencia / conversionFactor)) + " " +
                                unitSaleName;
                    }
                    else if (salesUnitIsMajor == 2)
                    {
                        stock = "" + MetodosGenerales.obtieneDosDecimales(item.existencia) + " " + baseUnitName;
                    }
                    else
                    {
                        if (baseUnitName.Equals(""))
                        {
                            stock = "" + MetodosGenerales.obtieneDosDecimales(item.existencia) + " Unidades";
                        }
                        else
                        {
                            stock = "" + MetodosGenerales.obtieneDosDecimales(item.existencia) + " " + baseUnitName;
                        }
                    }
                }
            });
            return item;
        }

        private void LlenarImagen(int idItem)
        {
            string path = MetodosGenerales.rootDirectory + "\\Imagenes\\items\\" + idItem + "-1.jpg";
            if (!File.Exists(path))
            {
                try
                {
                    String route = MetodosGenerales.rootDirectory + "\\Imagenes\\Estaticas\\SyncTPV.png";
                    FileStream fs = new FileStream(route, FileMode.Open, FileAccess.Read);
                    pictureArticulo.Image = MetodosGenerales.redimencionarImagenes(Image.FromStream(fs), 162, 137);
                    fs.Close();
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                }
            } else
            {
                try
                {
                    FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                    pictureArticulo.Image = MetodosGenerales.redimencionarImagenes(Image.FromStream(fs), 162, 137);
                    fs.Close();
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    path = MetodosGenerales.rootDirectory + "\\Imagenes\\Estaticas\\SyncTPV.png";
                    FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                    pictureArticulo.Image = MetodosGenerales.redimencionarImagenes(Image.FromStream(fs), 162, 137);
                    fs.Close();
                }
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private async Task fillTextAllPrices(ClsItemModel item)
        {
            String pricesText = "";
            if (serverModeLAN)
                pricesText = await getAllPricesForAnItemServer(item);
            else
            {
                pricesText = await getAllPricesForAnItemAPI(item);
            }
            textPreciosItem.Text = pricesText;
        }

        private async Task<string> getAllPricesForAnItemAPI(ClsItemModel itemModel)
        {
            String prices = "";
            await Task.Run(async () =>
            {
                double conversionFactor = 0;
                int salesUnitIsMajor = ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(itemModel.baseUnitId,
                    itemModel.salesUnitId);
                if (salesUnitIsMajor == 1)
                {
                    conversionFactor = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId,
                            itemModel.salesUnitId, true);
                }
                else if (salesUnitIsMajor == 0)
                {
                    conversionFactor = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId,
                            itemModel.salesUnitId, false);
                } else
                {

                }
                double precioCaptura = 0;
                List<ClsPreciosEmpresaModel> priceList = await ItemModel.getAllPricesForAnItemAPILAN(itemModel, serverModeLAN, codigoCaja);
                if (priceList != null)
                {
                    for (int i = 0; i < priceList.Count; i++)
                    {
                        if (salesUnitIsMajor == 1)
                        {
                            if (conversionFactor > 0)
                                precioCaptura = priceList[i].precioImporte * conversionFactor;
                            else precioCaptura = priceList[i].precioImporte;
                            if (i == 0)
                            {
                                prices += priceList[i].NOMBRE + ": " +
                                        priceList[i].precioImporte.ToString("C", CultureInfo.CurrentCulture) + " MXN - " + precioCaptura.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                            }
                            else
                                prices += "\n" + priceList[i].NOMBRE + ": " +priceList[i].precioImporte.ToString("C", CultureInfo.CurrentCulture) + 
                                " MXN - " + precioCaptura.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                        }
                        else if (salesUnitIsMajor == 0)
                        {
                            if (conversionFactor > 0)
                                precioCaptura = priceList[i].precioImporte * conversionFactor;
                            else precioCaptura = priceList[i].precioImporte;
                            if (i == 0)
                            {
                                prices += priceList[i].NOMBRE + ": " + priceList[i].precioImporte.ToString("C", CultureInfo.CurrentCulture) +
                                " MXN - " + precioCaptura.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                            }
                            else
                                prices += "\n" + priceList[i].NOMBRE + ": " + priceList[i].precioImporte.ToString("C", CultureInfo.CurrentCulture) +
                                " MXN - " + precioCaptura.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                        }
                        else
                        {
                            if (i == 0)
                            {
                                prices += priceList[i].NOMBRE + ": " + priceList[i].precioImporte.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                            }
                            else
                                prices += "\n" + priceList[i].NOMBRE + ": " + priceList[i].precioImporte.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                        }
                    }
                }
            });
            return prices;
        }

        private async Task<string> getAllPricesForAnItemServer(ClsItemModel item)
        {
            String prices = "";
            await Task.Run(async () =>
            {
                double conversionFactor = 0;
                int baseUnitId = item.baseUnitId;
                int salesUnitId = item.salesUnitId;
                var salesUnitIsMajor = ClsConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(comInstance, baseUnitId,
                    salesUnitId);
                if (salesUnitIsMajor == 1)
                {
                    conversionFactor = ClsConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(comInstance, baseUnitId,
                            salesUnitId, true);
                }
                else if (salesUnitIsMajor == 0)
                {
                    conversionFactor = ClsConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(comInstance, baseUnitId,
                            salesUnitId, false);
                }
                double precioCaptura = 0;
                double precioActual = 0;
                List<String> priceNameList = PreciosempresaModel.obtenerListaDeNombreDePrecios();
                if (priceNameList == null || (priceNameList != null && priceNameList.Count <= 0))
                {
                    if (serverModeLAN)
                        await PreciosEmpresaController.downloadAllPreciosEmpresaLAN();
                    else await PreciosEmpresaController.downloadAllPreciosEmpresaAPI();
                    priceNameList = PreciosempresaModel.obtenerListaDeNombreDePrecios();
                }
                if (priceNameList != null)
                {
                    for (int i = 0; i < priceNameList.Count; i++)
                    {
                        if (salesUnitIsMajor == 1)
                        {
                            if (i == 0)
                            {
                                precioActual = item.precio1;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }                                
                            else if (i == 1)
                            {
                                precioActual = item.precio2;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }
                            else if (i == 2)
                            {
                                precioActual = item.precio3;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }
                            else if (i == 3)
                            {
                                precioActual = item.precio4;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }                                
                            else if (i == 4)
                            {
                                precioActual = item.precio5;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }
                            else if (i == 5)
                            {
                                precioActual = item.precio6;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }
                            else if (i == 6)
                            {
                                precioActual = item.precio7;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }                                
                            else if (i == 7)
                            {
                                precioActual = item.precio8;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }
                            else if (i == 8)
                            {
                                precioActual = item.precio9;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }
                            else if (i == 9)
                            {
                                precioActual = item.precio10;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }
                        }
                        else if (salesUnitIsMajor == 0)
                        {
                            if (i == 0)
                            {
                                precioActual = item.precio1;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }
                            else if (i == 1)
                            {
                                precioActual = item.precio2;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }
                            else if (i == 2)
                            {
                                precioActual = item.precio3;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }
                            else if (i == 3)
                            {
                                precioActual = item.precio4;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }                                
                            else if (i == 4)
                            {
                                precioActual = item.precio5;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }
                            else if (i == 5)
                            {
                                precioActual = item.precio6;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }
                            else if (i == 6)
                            {
                                precioActual = item.precio7;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }
                            else if (i == 7)
                            {
                                precioActual = item.precio8;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }
                            else if (i == 8)
                            {
                                precioActual = item.precio9;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }
                            else if (i == 9)
                            {
                                precioActual = item.precio10;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }
                        }
                        else
                        {
                            if (i == 0)
                            {
                                precioActual = item.precio1;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }
                            else if (i == 1)
                            {
                                precioActual = item.precio2;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }
                            else if (i == 2)
                            {
                                precioActual = item.precio3;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }
                            else if (i == 3)
                            {
                                precioActual = item.precio4;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }
                            else if (i == 4)
                            {
                                precioActual = item.precio5;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }
                            else if (i == 5)
                            {
                                precioActual = item.precio6;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }
                            else if (i == 6)
                            {
                                precioActual = item.precio7;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }
                            else if (i == 7)
                            {
                                precioActual = item.precio8;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }
                            else if (i == 8)
                            {
                                precioActual = item.precio9;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }
                            else if (i == 9)
                            {
                                precioActual = item.precio10;
                                if (conversionFactor > 0)
                                    precioCaptura = precioActual * conversionFactor;
                            }
                        }
                        if (i == 0)
                        {
                            prices += priceNameList[i] + ": " + precioActual.ToString("C", CultureInfo.CurrentCulture) + " MXN - " + 
                            precioCaptura.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                        }
                        else
                            prices += "\n" + priceNameList[i] + ": " + precioActual.ToString("C", CultureInfo.CurrentCulture) + " MXN - " + 
                            precioCaptura.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                    }
                }

            });
            return prices;
        }

    }
}
