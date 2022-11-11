using SyncTPV.Controllers;
using SyncTPV.Controllers.Downloads;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using SyncTPV.Views;
using SyncTPV.Views.Extras;
using SyncTPV.Views.Scale;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;
using wsROMClase;
using wsROMClases.Models.Commercial;
using wsROMClases.Models.Panel;

namespace SyncTPV
{
    public partial class FormVenta : Form
    {
        private readonly int LIMIT = 20;
        private int progress = 0;
        public static int licenciaActivadaVigente = 0;
        List<MovimientosModel> movesList = null;
        List<MovimientosModel> movesListTemp = null;
        bool editMove = false;
        public static ClsItemModel itemModel = null;
        public static ClsClienteModel customerModel = null;
        public static int idDocument = 0, idCustomer = 0;
        public static int documentType = 0;
        private bool modifyPricePermission = false, selectPricesPermission = false;
        private double conversionFactor = 0;
        private double finalRealPrice = 0;
        int capturedUnitId = 0;
        List<ClsUnitsMeasureWeightModel> umwList = null;
        public static int pausedFragment = 0;
        private int queryType = 0, lastId = 0, totalMovements = 0, firstVisibleRow;
        private double totalArticulos = 0;
        private String query = "", queryRecords = "";
        private DateTime lastLoading;
        private ScrollBars gridScrollBars;
        bool procesoPrincipal = false, procesoLlenadoInfoMovimiento = false;
        public static int activateOpcionFacturar = 1;
        private int idMovement = 0;
        private bool permissionPrepedido = false, serverModeLAN = false;
        private String comInstance = "", panelInstance = "";
        private FormWaiting formWaiting;
        public static bool seleccionoUnItem = false, obtenerPrecios = false;
        private bool webActive = false;
        private int positionFiscalItemField = 6;
        private bool cotmosActive = false;
        private String codigoCaja = "";

        public FormVenta(int idDocument, bool cotmosActive)
        {
            FormVenta.idDocument = idDocument;
            serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
            InitializeComponent();
            eventElements();
            movesList = new List<MovimientosModel>();
            btnClearOptions.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.clean_white, 38, 38);
            btnSurtirPedidos.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.pedidos, 40, 40);
            pictureBoxInfoDescuento.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.discount_black, 29, 24);
            imgCliente.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.cliente_black, 55, 61);
            imgSinDatosFrmVenta.Image = Properties.Resources.sindatos;
            positionFiscalItemField = ConfiguracionModel.getPositionFiscalItemField();
            this.cotmosActive = cotmosActive;
        }

        private void eventElements()
        {
            dataGridMovements.Scroll += new ScrollEventHandler(dataGridArticulos_Scroll);
            editCapturedUnits.KeyPress += new KeyPressEventHandler(editCapturedUnits_KeyPress);
            editUnidadNoConvertible.KeyPress += new KeyPressEventHandler(editUnidadNoConvertible_KeyPress);
            btnAgregar.Click += new EventHandler(BtnAgregar_Click);
            btnCobrarFrmVenta.Click += new EventHandler(BtnCobrar_Click);
        }

        private async void FrmVentaNew_Load(object sender, EventArgs e)
        {
            textporcentajepromocion.Text = "0";
            textporcentajepromocion.Visible = false;
            textPromocionesMovimiento.Visible = false;
            await loadInitialData();
            await validateIfWebIsActive();
            validateInitialData();
        }

        private async Task loadInitialData()
        {
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                if (cotmosActive)
                {
                    dynamic responseConfig = ConfiguracionModel.getCodigoCajaPadre();
                    if (responseConfig.value == 1)
                    {
                        value = 1;
                        codigoCaja = responseConfig.code;
                    }
                    else
                    {
                        value = responseConfig.value;
                        description = responseConfig.description;
                    }
                }
                else
                {
                    value = 1;
                    codigoCaja = UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn());
                }
            });
            if (value == 1)
            {

            }
            else
            {
                FormMessage formMessage = new FormMessage("Configuración", description, 3);
                formMessage.ShowDialog();
            }
        }

        private async Task validateIfWebIsActive()
        {
            String description = "";
            await Task.Run(async () =>
            {
                if (serverModeLAN)
                {
                    comInstance = InstanceSQLSEModel.getStringComInstance();
                    panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    permissionPrepedido = ClsAgentesModel.doYouHavePermissionPrepedido(panelInstance, ClsRegeditController.getIdUserInTurn());
                    description = "Versión LAN " + MetodosGenerales.versionNumber;
                }
                else
                {
                    dynamic responseWeb = ConfiguracionModel.webActive();
                    if (responseWeb.value == 1)
                        webActive = responseWeb.active;
                    if (webActive)
                        description = "Versión Web (Online) " + MetodosGenerales.versionNumber;
                    else description = "Versión Web (Offline) " + MetodosGenerales.versionNumber;
                    deleteAllMovementsWithoutDocuments();
                    permissionPrepedido = UserModel.doYouHavePermissionPrepedido();
                }
            });
            textVersion.Text = description;
        }

        private async Task validateInitialData()
        {
            editObservationMovement.MaxLength = 250;
            await validateIfUserHavePricesPermissions();
            if (idDocument != 0)
            {
                int clienteAVender = DocumentModel.getCustomerIdOfADocument(idDocument);
                idCustomer = clienteAVender;
                await callGetADataCustomer(idCustomer);
                if (customerModel != null)
                {
                    editNombreCliente.Text = customerModel.NOMBRE;
                    editDiscountItemVenta.Text = customerModel.descuentoMovimiento + "";
                    comboCodigoItemVenta.Focus();
                    downloadImageCustomer(idCustomer);
                }
                await fillDataGridMovements();
                btnCerrar.Text = "Cancelar";
                btnCerrar.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.close, 45, 45);
            }
            else
            {
                await downloadIdClientePublicoEnGeneral();
                editCapturedUnits.Text = Convert.ToString(1);
                if (idCustomer == 0)
                    idCustomer = ClsRegeditController.getIdDefaultCustomer();
                await callGetADataCustomer(idCustomer);
                if (customerModel != null)
                {
                    editNombreCliente.Text = customerModel.NOMBRE;
                    editDiscountItemVenta.Text = customerModel.descuentoMovimiento + "";
                    comboCodigoItemVenta.Focus();
                    downloadImageCustomer(idCustomer);
                }
                btnRecuperar.Visible = true;
                btnCerrar.Text = "Cerrar";
                btnCerrar.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.back_white, 45, 45);
            }
            btnOpenScale.Visible = false;
            validarPermisoPrepedido();
        }

        private async Task downloadImageCustomer(int idCustomer)
        {
            await Task.Run(async () =>
            {
                if (serverModeLAN)
                {
                    String rutaLocal = MetodosGenerales.rootDirectory + "\\Imagenes\\customers";
                    await SubirImagenesController.getImageLAN("customers", rutaLocal, idCustomer);
                }
                else await SubirImagenesController.getImage("-c", idCustomer);
            });
            fillImagenCliente(idCustomer);
        }

        private async Task validateIfUserHavePricesPermissions()
        {
            await Task.Run(async () =>
            {
                selectPricesPermission = UserModel.doYouHavePermissionToSelectPrices(ClsRegeditController.getIdUserInTurn());
                modifyPricePermission = UserModel.doYouHavePermissionToModifyPrices(ClsRegeditController.getIdUserInTurn());
            });
        }

        private void fillImagenCliente(int idCustomer)
        {
            string rutaImg = MetodosGenerales.rootDirectory + "\\Imagenes\\Customers\\" + idCustomer + "-c.jpg";
            if (File.Exists(rutaImg))
            {
                try
                {
                    FileStream fs = new FileStream(rutaImg, FileMode.Open, FileAccess.Read);
                    imgCliente.Image = MetodosGenerales.redimencionarImagenes(Image.FromStream(fs), 55, 61);
                    fs.Close();
                }
                catch (Exception ex)
                {
                    SECUDOC.writeLog(ex.ToString());
                    imgCliente.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.cliente_black, 55, 61);
                }
            }
            else
            {
                try
                {
                    rutaImg = MetodosGenerales.rootDirectory + "\\Imagenes\\Estaticas\\SyncTPV.png";
                    imgCliente.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.cliente_black, 55, 61);
                }
                catch (Exception ex)
                {
                    SECUDOC.writeLog(ex.ToString());
                }
            }
        }

        private async Task callGetADataCustomer(int idCustomer)
        {
            await Task.Run(async () =>
            {
                if (serverModeLAN)
                {
                    dynamic responseCustomer = await CustomersController.getACustomerLAN(idCustomer);
                    if (responseCustomer.value == 1)
                        customerModel = responseCustomer.customerModel;
                }
                else
                {
                    customerModel = CustomerModel.getAllDataFromACustomer(idCustomer);
                    if (customerModel == null)
                    {
                        if (MetodosGenerales.verifyIfInternetIsAvailable())
                        {
                            dynamic responseCustomer = await CustomersController.getACustomerAPI(idCustomer);
                            if (responseCustomer.value == 1)
                                customerModel = responseCustomer.customerModel;
                        }
                        else customerModel = CustomerModel.getAllDataFromACustomer(idCustomer);
                    }
                }
            });
        }

        private async Task downloadIdClientePublicoEnGeneral()
        {
            if (serverModeLAN)
            {
                dynamic responseCP = await ClsInitialChargeController.downloadCustomerIdPublicGeneralTpvLAN();
                if (responseCP.value < 0)
                {
                    FormMessage formMessage = new FormMessage("Exception", responseCP.description, 3);
                    formMessage.ShowDialog();
                }
            }
            else
            {
                if (MetodosGenerales.verifyIfInternetIsAvailable())
                {
                    dynamic responseCP = await ClsInitialChargeController.downloadCustomerIdPublicGeneralTpv();
                    if (responseCP.value < 0)
                    {
                        FormMessage formMessage = new FormMessage("Exception", responseCP.description, 3);
                        formMessage.ShowDialog();
                    }
                }
                else idCustomer = ClsRegeditController.getIdDefaultCustomer();
            }
        }

        private void validarPermisoPrepedido()
        {
            if (permissionPrepedido)
            {
                panelToolbar.Height = 80;
                panelSuperior.Location = new Point(panelSuperior.Location.X, panelToolbar.Location.Y + 81);
                btnPayWithCashFrmVenta.Visible = false;
                btnPayWithCashFrmVenta.Width = 0;
                comboBoxDocumentTypeFrmVenta.Items.Clear();
                comboBoxDocumentTypeFrmVenta.Items.Add("Prepedido");
                comboBoxDocumentTypeFrmVenta.SelectedIndex = 0;
                comboPreciosItemVenta.Visible = false;
                textDiscountFrmVenta.Visible = false;
                textInfoTotal.Text = "Pollos a Entregar";
                textInfoTotal.Location = new Point(textInfoTotal.Location.X, textInfoTotal.Location.Y - 67);
                textTotalFrmVenta.Location = new Point(textTotalFrmVenta.Location.X, textTotalFrmVenta.Location.Y - 67);
                btnRecuperar.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.pedientes_white, 37, 37);
                btnRecuperar.Text = "Pendientes";
                btnRecuperar.Height = 75;
                btnRecuperar.FlatAppearance.BorderSize = 1;
                btnClearOptions.Height = 75;
                btnClearOptions.FlatAppearance.BorderSize = 1;
                btnPausarDocumentoFrmVenta.Height = 75;
                btnPausarDocumentoFrmVenta.FlatAppearance.BorderSize = 1;
                btnSurtirPedidos.Height = 75;
                btnSurtirPedidos.FlatAppearance.BorderSize = 1;
                btnCerrar.Height = 75;
                btnCerrar.FlatAppearance.BorderSize = 1;
                if (idDocument != 0) {
                    btnBuscarClientesNew.Enabled = false;
                    bool enviado = DocumentModel.isItDocumentPrepedidoSendedToTheCustomer(idDocument);
                    if (enviado)
                    {
                        btnCobrarFrmVenta.Text = "Guardar (F10)";
                        btnCobrarFrmVenta.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.send, 35, 35);
                    }
                    else
                    {
                        btnCobrarFrmVenta.Text = "Entregar al Cliente (F10)";
                        btnCobrarFrmVenta.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.entregar_back, 35, 35);
                    }
                    btnPausarDocumentoFrmVenta.Visible = true;
                } else
                {
                    btnCobrarFrmVenta.Text = "Entregar al Cliente (F10)";
                    btnCobrarFrmVenta.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.entregar_back, 35, 35);
                    btnPausarDocumentoFrmVenta.Visible = false;
                    btnSurtirPedidos.Visible = true;
                }
                dataGridMovements.RowTemplate.DefaultCellStyle.Padding = new Padding(10, 15, 10, 15);
                dataGridMovements.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                editCapturedUnits.ReadOnly = true;
            }
            else
            {
                editCapturedUnits.ReadOnly = false;
                btnRecuperar.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.recuperardocumento_white, 35, 35);
                fillComboDocumentType();
                if (idDocument == 0)
                    btnSurtirPedidos.Visible = true;
            }
        }

        public async Task fillDataGridMovements()
        {
            hideScrollBars();
            lastLoading = DateTime.Now;
            movesListTemp = await getAllMovements();
            if (movesListTemp != null)
            {
                progress += movesListTemp.Count;
                movesList.AddRange(movesListTemp);
                if (movesList.Count > 0 && dataGridMovements.ColumnHeadersVisible == false)
                    dataGridMovements.ColumnHeadersVisible = true;
                for (int i = 0; i < movesListTemp.Count; i++)
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    style.Font = new Font(this.dataGridMovements.Font, FontStyle.Bold);
                    int n = dataGridMovements.Rows.Add();
                    if (permissionPrepedido)
                        dataGridMovements.Rows[n].Height = 70;
                    dataGridMovements.Rows[n].Cells[0].Value = movesListTemp[i].id;
                    dataGridMovements.Columns["idMovimentDgv"].Visible = false;
                    dataGridMovements.Rows[n].Cells[1].Value = movesListTemp[i].itemCode;
                    this.dataGridMovements.Columns["Clave"].Visible = false;
                    String itemName = ItemModel.getTheNameOfAnItem(movesListTemp[i].itemId);
                    if (itemName.Equals(""))
                    {
                        dynamic response = null;
                        if (serverModeLAN)
                        {
                            response = await ItemsController.getItemNameLAN(movesListTemp[i].itemId);
                            if (response.value == 1)
                                itemName = response.name;
                        }
                        else
                        {
                            if (webActive)
                            {
                                response = await ItemsController.getItemNameAPI(movesListTemp[i].itemId, codigoCaja);
                                if (response.value == 1)
                                    itemName = response.name;
                            }
                        }
                    }
                    dataGridMovements.Rows[n].Cells[2].Value = itemName;
                    dataGridMovements.Rows[n].Cells[3].Value = movesListTemp[i].capturedUnits;
                    this.dataGridMovements.Columns["Cantidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.dataGridMovements.Columns["Cantidad"].Width = 70;
                    String unitName = "";
                    if (movesListTemp[i].capturedUnitId != 0)
                    {
                        if (serverModeLAN)
                        {
                            dynamic responseUnitName = await UnitsOfMeasureAndWeightController.getNameOfAndUnitMeasureWeightLAN(movesListTemp[i].capturedUnitId);
                            if (responseUnitName.value == 1)
                                unitName = responseUnitName.name;
                        }
                        else
                        {
                            unitName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(movesListTemp[i].capturedUnitId);
                            if (unitName.Equals(""))
                            {
                                if (webActive)
                                {
                                    dynamic responseUnitName = await UnitsOfMeasureAndWeightController.getNameOfAndUnitMeasureWeightAPI(movesListTemp[i].capturedUnitId);
                                    if (responseUnitName.value == 1)
                                        unitName = responseUnitName.name;
                                }
                            }
                        }
                    }
                    else unitName = "Unidades";
                    dataGridMovements.Rows[n].Cells[4].Value = unitName;
                    this.dataGridMovements.Columns["Unidad"].Width = 70;
                    if (!permissionPrepedido)
                    {
                        dataGridMovements.Rows[n].Cells[5].Value = "" + movesListTemp[i].nonConvertibleUnits;
                        dataGridMovements.Columns["piezasMovements"].Visible = false;
                        dataGridMovements.Rows[n].Cells[6].Value = movesListTemp[i].price.ToString("C", CultureInfo.CurrentCulture) + "";
                        this.dataGridMovements.Columns["precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        this.dataGridMovements.Columns["precio"].Width = 75;
                        dataGridMovements.Rows[n].Cells[7].Value = movesListTemp[i].monto.ToString("C", CultureInfo.CurrentCulture);
                        this.dataGridMovements.Columns["Subtotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        this.dataGridMovements.Columns["Subtotal"].Width = 80;
                        dataGridMovements.Rows[n].Cells[8].Value = movesListTemp[i].descuentoPorcentaje + "% = " +
                            movesListTemp[i].descuentoImporte.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                        this.dataGridMovements.Columns["Descuento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dataGridMovements.Rows[n].Cells[9].Value = movesListTemp[i].total.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                        style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        this.dataGridMovements.Columns["TotalGeneral"].DefaultCellStyle = style;
                        dataGridMovements.Rows[n].Cells[10].Value = "Borrar";
                    }
                    else if (permissionPrepedido)
                    {
                        if (movesListTemp[i].nonConvertibleUnitId != 0)
                        {
                            if (serverModeLAN)
                            {
                                dynamic responseUnitName = await UnitsOfMeasureAndWeightController.getNameOfAndUnitMeasureWeightLAN(movesListTemp[i].nonConvertibleUnitId);
                                if (responseUnitName.value == 1)
                                    unitName = responseUnitName.name;
                            }
                            else
                            {
                                unitName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(movesListTemp[i].nonConvertibleUnitId);
                                if (unitName.Equals(""))
                                {
                                    if (webActive)
                                    {
                                        dynamic responseUnitName = await UnitsOfMeasureAndWeightController.getNameOfAndUnitMeasureWeightAPI(movesListTemp[i].nonConvertibleUnitId);
                                        if (responseUnitName.value == 1)
                                            unitName = responseUnitName.name;
                                    }
                                }
                            }
                            dataGridMovements.Rows[n].Cells[5].Value = "" + movesListTemp[i].nonConvertibleUnits + " " + unitName;
                            dataGridMovements.Columns["piezasMovements"].Visible = true;
                        } else
                        {
                            dataGridMovements.Rows[n].Cells[5].Value = "" + movesListTemp[i].nonConvertibleUnits + " Unidades";
                            dataGridMovements.Columns["piezasMovements"].Visible = true;
                        }
                        dataGridMovements.Rows[n].Cells[6].Value = movesListTemp[i].price.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                        dataGridMovements.Columns["precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        if (documentType == DocumentModel.TIPO_PREPEDIDO)
                        {
                            dataGridMovements.Rows[n].Cells[7].Value = "0.00";
                            dataGridMovements.Columns["Subtotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        }
                        else
                        {
                            dataGridMovements.Rows[n].Cells[7].Value = movesListTemp[i].monto.ToString("C", CultureInfo.CurrentCulture);
                            dataGridMovements.Columns["Subtotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        }
                        dataGridMovements.Rows[n].Cells[8].Value = movesListTemp[i].descuentoPorcentaje + "% = " +
                            movesListTemp[i].descuentoImporte.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                        this.dataGridMovements.Columns["Descuento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dataGridMovements.Rows[n].Cells[9].Value = movesListTemp[i].total.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                        style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        this.dataGridMovements.Columns["TotalGeneral"].DefaultCellStyle = style;
                        this.dataGridMovements.Columns["TotalGeneral"].Visible = false;
                        dataGridMovements.Rows[n].Cells[10].Value = "Borrar";
                        this.dataGridMovements.Columns[10].Width = 100;
                    }
                    dataGridMovements.Rows[n].Cells[11].Value = movesListTemp[i].itemId;
                    dataGridMovements.Columns[11].Visible = false;
                }
                dataGridMovements.PerformLayout();
                movesListTemp.Clear();
                if (movesList.Count > 0)
                    lastId = Convert.ToInt32(movesList[movesList.Count - 1].id);
                imgSinDatosFrmVenta.Visible = false;
                actualizarSubDescYTotalVenta(idDocument);
            }
            else
            {
                if (progress == 0)
                {
                    imgSinDatosFrmVenta.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.sindatos, 200, 200);
                    imgSinDatosFrmVenta.Visible = true;
                }
                actualizarSubDescYTotalVenta(idDocument);
            }
            await updateTotalNumberOfMovements();
            if (firstVisibleRow > -1)
            {
                showScrollBars();
                if (movesList.Count > 0)
                {
                    int displayed = dataGridMovements.DisplayedRowCount(true);
                    int lastVisible = (firstVisibleRow + displayed) - 1;
                    int lastIndex = dataGridMovements.RowCount - 1;
                    if (lastVisible < lastIndex)
                    {
                        dataGridMovements.FirstDisplayedScrollingRowIndex = lastIndex;
                    } else dataGridMovements.FirstDisplayedScrollingRowIndex = firstVisibleRow;
                    imgSinDatosFrmVenta.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.sindatos, 200, 200);
                    imgSinDatosFrmVenta.Visible = false;
                    if (idDocument != 0)
                        btnCerrar.Text = "Cencelar";
                    else btnCerrar.Text = "Cerrar";
                }
                else btnCerrar.Text = "Cerrar";
            }
        }

        private async Task addNewColumnInDataGridMovements(MovimientosModel movimiento)
        {
            if (dataGridMovements.ColumnHeadersVisible == false)
                dataGridMovements.ColumnHeadersVisible = true;
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style.Font = new Font(this.dataGridMovements.Font, FontStyle.Bold);
            int n = dataGridMovements.Rows.Add();
            if (permissionPrepedido)
                dataGridMovements.Rows[n].Height = 70;
            dataGridMovements.Rows[n].Cells[0].Value = movimiento.id;
            dataGridMovements.Columns["idMovimentDgv"].Visible = false;
            dataGridMovements.Rows[n].Cells[1].Value = movimiento.itemCode;
            this.dataGridMovements.Columns["Clave"].Visible = false;
            String itemName = ItemModel.getTheNameOfAnItem(movimiento.itemId);
            if (itemName.Equals(""))
            {
                dynamic response = null;
                if (serverModeLAN)
                {
                    response = await ItemsController.getItemNameLAN(movimiento.itemId);
                    if (response.value == 1)
                        itemName = response.name;
                }
                else
                {
                    if (webActive)
                    {
                        response = await ItemsController.getItemNameAPI(movimiento.itemId, codigoCaja);
                        if (response.value == 1)
                            itemName = response.name;
                    }
                }
            }
            dataGridMovements.Rows[n].Cells[2].Value = itemName;
            dataGridMovements.Rows[n].Cells[3].Value = movimiento.capturedUnits;
            this.dataGridMovements.Columns["Cantidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridMovements.Columns["Cantidad"].Width = 70;
            String unitName = "";
            if (movimiento.capturedUnitId != 0)
            {
                if (serverModeLAN)
                {
                    dynamic responseUnitName = await UnitsOfMeasureAndWeightController.getNameOfAndUnitMeasureWeightLAN(movimiento.capturedUnitId);
                    if (responseUnitName.value == 1)
                        unitName = responseUnitName.name;
                }
                else
                {
                    unitName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(movimiento.capturedUnitId);
                    if (unitName.Equals(""))
                    {
                        if (webActive)
                        {
                            dynamic responseUnitName = await UnitsOfMeasureAndWeightController.getNameOfAndUnitMeasureWeightAPI(movimiento.capturedUnitId);
                            if (responseUnitName.value == 1)
                                unitName = responseUnitName.name;
                        }
                    }
                }
            }
            else unitName = "Unidades";
            dataGridMovements.Rows[n].Cells[4].Value = unitName;
            this.dataGridMovements.Columns["Unidad"].Width = 70;
            if (!permissionPrepedido)
            {
                dataGridMovements.Rows[n].Cells[5].Value = "" + movimiento.nonConvertibleUnits;
                dataGridMovements.Columns["piezasMovements"].Visible = false;
                dataGridMovements.Rows[n].Cells[6].Value = movimiento.price.ToString("C", CultureInfo.CurrentCulture) + "";
                this.dataGridMovements.Columns["precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridMovements.Columns["precio"].Width = 75;
                dataGridMovements.Rows[n].Cells[7].Value = movimiento.monto.ToString("C", CultureInfo.CurrentCulture);
                this.dataGridMovements.Columns["Subtotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridMovements.Columns["Subtotal"].Width = 80;
                dataGridMovements.Rows[n].Cells[8].Value = movimiento.descuentoPorcentaje + "% = " +
                    movimiento.descuentoImporte.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                this.dataGridMovements.Columns["Descuento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridMovements.Rows[n].Cells[9].Value = movimiento.total.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridMovements.Columns["TotalGeneral"].DefaultCellStyle = style;
                dataGridMovements.Rows[n].Cells[10].Value = "Borrar";
            }
            else if (permissionPrepedido)
            {
                if (movimiento.nonConvertibleUnitId != 0)
                {
                    if (serverModeLAN)
                    {
                        dynamic responseUnitName = await UnitsOfMeasureAndWeightController.getNameOfAndUnitMeasureWeightLAN(movimiento.nonConvertibleUnitId);
                        if (responseUnitName.value == 1)
                            unitName = responseUnitName.name;
                    }
                    else
                    {
                        unitName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(movimiento.nonConvertibleUnitId);
                        if (unitName.Equals(""))
                        {
                            if (webActive)
                            {
                                dynamic responseUnitName = await UnitsOfMeasureAndWeightController.getNameOfAndUnitMeasureWeightAPI(movimiento.nonConvertibleUnitId);
                                if (responseUnitName.value == 1)
                                    unitName = responseUnitName.name;
                            }
                        }
                    }
                    dataGridMovements.Rows[n].Cells[5].Value = "" + movimiento.nonConvertibleUnits + " " + unitName;
                    dataGridMovements.Columns["piezasMovements"].Visible = true;
                }
                else
                {
                    dataGridMovements.Rows[n].Cells[5].Value = "" + movimiento.nonConvertibleUnits + " Unidades";
                    dataGridMovements.Columns["piezasMovements"].Visible = true;
                }
                dataGridMovements.Rows[n].Cells[6].Value = movimiento.price.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                this.dataGridMovements.Columns["precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                if (documentType == DocumentModel.TIPO_PREPEDIDO)
                {
                    dataGridMovements.Rows[n].Cells[7].Value = "0.00";
                    dataGridMovements.Columns["Subtotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                } else
                {
                    dataGridMovements.Rows[n].Cells[7].Value = movimiento.monto.ToString("C", CultureInfo.CurrentCulture);
                    dataGridMovements.Columns["Subtotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                dataGridMovements.Rows[n].Cells[8].Value = movimiento.descuentoPorcentaje + "% = " +
                    movimiento.descuentoImporte.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                this.dataGridMovements.Columns["Descuento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridMovements.Rows[n].Cells[9].Value = movimiento.total.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridMovements.Columns["TotalGeneral"].DefaultCellStyle = style;
                this.dataGridMovements.Columns["TotalGeneral"].Visible = false;
                dataGridMovements.Rows[n].Cells[10].Value = "Borrar";
                this.dataGridMovements.Columns[10].Width = 100;
            }
            dataGridMovements.Rows[n].Cells[11].Value = movimiento.itemId;
            dataGridMovements.Columns[11].Visible = false;
            try
            {
                dataGridMovements.FirstDisplayedScrollingRowIndex = dataGridMovements.FirstDisplayedScrollingRowIndex + 1;
            }
            catch (Exception ex)
            {
                //SECUDOC.writeLog(ex.ToString());
            }
            imgSinDatosFrmVenta.Visible = false;
            movesList.Add(movimiento);
            if (movesList.Count > 0)
                lastId = movesList[movesList.Count - 1].id;
            await updateTotalNumberOfMovements();
            actualizarSubDescYTotalVenta(idDocument);
        }

        private async Task editNewColumnInDataGridMovements(MovimientosModel movimiento, int dgvPosition)
        {
            if (dataGridMovements.ColumnHeadersVisible == false)
                dataGridMovements.ColumnHeadersVisible = true;
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style.Font = new Font(this.dataGridMovements.Font, FontStyle.Bold);
            int n = dgvPosition;
            if (permissionPrepedido)
                dataGridMovements.Rows[n].Height = 70;
            dataGridMovements.Rows[n].Cells[0].Value = movimiento.id;
            dataGridMovements.Columns["idMovimentDgv"].Visible = false;
            dataGridMovements.Rows[n].Cells[1].Value = movimiento.itemCode;
            this.dataGridMovements.Columns["Clave"].Visible = false;
            String itemName = ItemModel.getTheNameOfAnItem(movimiento.itemId);
            if (itemName.Equals(""))
            {
                dynamic response = null;
                if (serverModeLAN)
                {
                    response = await ItemsController.getItemNameLAN(movimiento.itemId);
                    if (response.value == 1)
                        itemName = response.name;
                }
                else
                {
                    if (webActive)
                    {
                        response = await ItemsController.getItemNameAPI(movimiento.itemId, codigoCaja);
                        if (response.value == 1)
                            itemName = response.name;
                    }
                }
            }
            dataGridMovements.Rows[n].Cells[2].Value = itemName;
            dataGridMovements.Rows[n].Cells[3].Value = movimiento.capturedUnits;
            this.dataGridMovements.Columns["Cantidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridMovements.Columns["Cantidad"].Width = 70;
            String unitName = "";
            if (movimiento.capturedUnitId != 0)
            {
                if (serverModeLAN)
                {
                    dynamic responseUnitName = await UnitsOfMeasureAndWeightController.getNameOfAndUnitMeasureWeightLAN(movimiento.capturedUnitId);
                    if (responseUnitName.value == 1)
                        unitName = responseUnitName.name;
                }
                else
                {
                    unitName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(movimiento.capturedUnitId);
                    if (unitName.Equals(""))
                    {
                        if (webActive)
                        {
                            dynamic responseUnitName = await UnitsOfMeasureAndWeightController.getNameOfAndUnitMeasureWeightAPI(movimiento.capturedUnitId);
                            if (responseUnitName.value == 1)
                                unitName = responseUnitName.name;
                        }
                    }
                }
            } else unitName = "Unidades";
            dataGridMovements.Rows[n].Cells[4].Value = unitName;
            this.dataGridMovements.Columns["Unidad"].Width = 70;
            if (!permissionPrepedido)
            {
                dataGridMovements.Rows[n].Cells[5].Value = "" + movimiento.nonConvertibleUnits;
                dataGridMovements.Columns["piezasMovements"].Visible = false;
                dataGridMovements.Rows[n].Cells[6].Value = movimiento.price.ToString("C", CultureInfo.CurrentCulture) + "";
                this.dataGridMovements.Columns["precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridMovements.Columns["precio"].Width = 75;
                dataGridMovements.Rows[n].Cells[7].Value = movimiento.monto.ToString("C", CultureInfo.CurrentCulture);
                this.dataGridMovements.Columns["Subtotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridMovements.Columns["Subtotal"].Width = 80;
                if(movimiento.descuentoPorcentaje >= movimiento.rateDiscountPromo)
                {
                    movimiento.descuentoPorcentaje = movimiento.descuentoPorcentaje - movimiento.discount;
                }
                dataGridMovements.Rows[n].Cells[8].Value = movimiento.descuentoPorcentaje + "% = " +
                    movimiento.descuentoImporte.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                this.dataGridMovements.Columns["Descuento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridMovements.Rows[n].Cells[9].Value = movimiento.total.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridMovements.Columns["TotalGeneral"].DefaultCellStyle = style;
                dataGridMovements.Rows[n].Cells[10].Value = "Borrar";
            }
            else if (permissionPrepedido)
            {
                if (movimiento.nonConvertibleUnitId != 0)
                {
                    if (serverModeLAN)
                    {
                        dynamic responseUnitName = await UnitsOfMeasureAndWeightController.getNameOfAndUnitMeasureWeightLAN(movimiento.nonConvertibleUnitId);
                        if (responseUnitName.value == 1)
                            unitName = responseUnitName.name;
                    }
                    else
                    {
                        unitName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(movimiento.nonConvertibleUnitId);
                        if (unitName.Equals(""))
                        {
                            dynamic responseUnitName = await UnitsOfMeasureAndWeightController.getNameOfAndUnitMeasureWeightAPI(movimiento.nonConvertibleUnitId);
                            if (responseUnitName.value == 1)
                                unitName = responseUnitName.name;
                        }
                    }
                    dataGridMovements.Rows[n].Cells[5].Value = "" + movimiento.nonConvertibleUnits + " " + unitName;
                    dataGridMovements.Columns["piezasMovements"].Visible = true;
                }
                else
                {
                    dataGridMovements.Rows[n].Cells[5].Value = "" + movimiento.nonConvertibleUnits + " Unidades";
                    dataGridMovements.Columns["piezasMovements"].Visible = true;
                }
                dataGridMovements.Rows[n].Cells[6].Value = movimiento.price.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                this.dataGridMovements.Columns["precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                if (documentType == DocumentModel.TIPO_PREPEDIDO)
                {
                    dataGridMovements.Rows[n].Cells[7].Value = "0.00";
                    dataGridMovements.Columns["Subtotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                } else
                {
                    dataGridMovements.Rows[n].Cells[7].Value = movimiento.monto.ToString("C", CultureInfo.CurrentCulture);
                    dataGridMovements.Columns["Subtotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                dataGridMovements.Rows[n].Cells[8].Value = movimiento.descuentoPorcentaje + "% = " +
                    movimiento.descuentoImporte.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                this.dataGridMovements.Columns["Descuento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridMovements.Rows[n].Cells[9].Value = movimiento.total.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridMovements.Columns["TotalGeneral"].DefaultCellStyle = style;
                this.dataGridMovements.Columns["TotalGeneral"].Visible = false;
                dataGridMovements.Rows[n].Cells[10].Value = "Borrar";
                this.dataGridMovements.Columns[10].Width = 100;
            }
            dataGridMovements.Rows[n].Cells[11].Value = movimiento.itemId;
            dataGridMovements.Columns[11].Visible = false;
            imgSinDatosFrmVenta.Visible = false;
            if (movesList.Count > 0)
            {
                movesList[dgvPosition] = movimiento;
                lastId = movesList[movesList.Count - 1].id;
            }
            await updateTotalNumberOfMovements();
            actualizarSubDescYTotalVenta(idDocument);
        }

        private async Task updateTotalNumberOfMovements()
        {
            totalMovements = await getTotalNumberOfMovements();
            totalArticulos = await getTotalNumberOfItems();
            textTotalMovements.Text = "Movimientos: " + totalMovements.ToString().Trim() + " - Artículos: " + totalArticulos.ToString().Trim();
        }

        private void showScrollBars()
        {
            dataGridMovements.ScrollBars = gridScrollBars;
        }

        private async Task<List<MovimientosModel>> getAllMovements()
        {
            List<MovimientosModel> movesList = null;
            await Task.Run(async () =>
            {
                if (queryType == 0)
                {
                    query = "SELECT * FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " + LocalDatabase.CAMPO_DOCUMENTOID_MOV +
                    " = " + idDocument + " AND " + LocalDatabase.CAMPO_ID_MOV + " > " + lastId + " ORDER BY " + LocalDatabase.CAMPO_ID_MOV +
                    " LIMIT " + LIMIT;
                }
                movesList = MovimientosModel.getAllMovementsFromADocument(idDocument);
            });
            return movesList;
        }

        public async Task<int> getTotalNumberOfMovements()
        {
            await Task.Run(async () => {
                if (queryType == 0)
                {
                    queryRecords = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " + LocalDatabase.CAMPO_DOCUMENTOID_MOV +
                    " = " + idDocument;
                }
                totalMovements = MovimientosModel.getIntValue(queryRecords);
            });
            return totalMovements;
        }

        public async Task<double> getTotalNumberOfItems()
        {
            await Task.Run(async () => {
                totalArticulos = MovimientosModel.getTotalCapturedUnitsFromADocument(idDocument);
            });
            return totalArticulos;
        }

        private async Task deleteAllMovementsWithoutDocuments()
        {
            await Task.Run(async () => {
                String query = "DELETE FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " + LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = 0";
                MovimientosModel.createUpdateOrDeleteRecords(query);
                query = "DELETE FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " + LocalDatabase.CAMPO_ID_DOC + " = 0";
                DocumentModel.createUpdateOrDeleteRecords(query);
            });
        }

        private void fillComboDocumentType()
        {
            comboBoxDocumentTypeFrmVenta.Items.Clear();
            if (!permissionPrepedido)
            {
                if (cotmosActive)
                {
                    comboBoxDocumentTypeFrmVenta.Items.Add("Cotización");
                    if (documentType == 0)
                        documentType = DocumentModel.TIPO_COTIZACION;
                } else
                {
                    if (UserModel.doYouHavePermissionToSell())
                    {
                        comboBoxDocumentTypeFrmVenta.Items.Add("Venta");
                        if (documentType == 0)
                            documentType = DocumentModel.TIPO_REMISION;
                    }
                    if (UserModel.doYouHavePermissionToMakeQuotes())
                    {
                        comboBoxDocumentTypeFrmVenta.Items.Add("Cotización");
                        if (documentType == 0)
                            documentType = DocumentModel.TIPO_COTIZACION;
                    }
                    if (UserModel.doYouHavePermissionToPlaceOrders())
                    {
                        comboBoxDocumentTypeFrmVenta.Items.Add("Pedido");
                        if (documentType == 0)
                            documentType = DocumentModel.TIPO_PEDIDO;
                    }
                    if (UserModel.doYouHavePermissionToMakeReturns())
                    {
                        comboBoxDocumentTypeFrmVenta.Items.Add("Devolución");
                        if (documentType == 0)
                            documentType = DocumentModel.TIPO_DEVOLUCION;
                    }
                }
            }
            int count = comboBoxDocumentTypeFrmVenta.Items.Count;
            if (count == 0)
            {
                FormMessage fm = new FormMessage("Tipos De Documentos No Encontrados", "Necesitas tener permisos para realizar diferentes tipos de Documento", 2);
                fm.ShowDialog();
            } else
            {
                if (documentType == DocumentModel.TIPO_VENTA || documentType == DocumentModel.TIPO_REMISION)
                {
                    comboBoxDocumentTypeFrmVenta.SelectedIndex = 0;
                }
                else if (documentType == DocumentModel.TIPO_COTIZACION)
                {
                    if (count == 1)
                    {
                        if (comboBoxDocumentTypeFrmVenta.Items[0].ToString().Trim().Equals("Venta"))
                        {
                            comboBoxDocumentTypeFrmVenta.SelectedIndex = 0;
                            documentType = DocumentModel.TIPO_REMISION;
                        }
                        else comboBoxDocumentTypeFrmVenta.SelectedIndex = 0;
                    }
                    else if (count == 2)
                    {
                        if (comboBoxDocumentTypeFrmVenta.Items[0].ToString().Trim().Equals("Venta"))
                            comboBoxDocumentTypeFrmVenta.SelectedIndex = 1;
                        else if (comboBoxDocumentTypeFrmVenta.Items[0].ToString().Trim().Equals("Cotización"))
                            comboBoxDocumentTypeFrmVenta.SelectedIndex = 0;
                    }
                    else if (count == 3)
                    {
                        if (comboBoxDocumentTypeFrmVenta.Items[0].ToString().Trim().Equals("Venta"))
                            comboBoxDocumentTypeFrmVenta.SelectedIndex = 1;
                        else if (comboBoxDocumentTypeFrmVenta.Items[0].ToString().Trim().Equals("Cotización"))
                            comboBoxDocumentTypeFrmVenta.SelectedIndex = 0;
                    }
                    else
                    {
                        comboBoxDocumentTypeFrmVenta.SelectedIndex = 1;
                    }
                }
                else if (documentType == DocumentModel.TIPO_PEDIDO)
                {
                    if (count == 1)
                    {
                        if (comboBoxDocumentTypeFrmVenta.Items[0].ToString().Trim().Equals("Venta"))
                        {
                            comboBoxDocumentTypeFrmVenta.SelectedIndex = 0;
                            documentType = DocumentModel.TIPO_REMISION;
                        }
                        else if (comboBoxDocumentTypeFrmVenta.Items[0].ToString().Trim().Equals("Cotización"))
                        {
                            comboBoxDocumentTypeFrmVenta.SelectedIndex = 0;
                            documentType = DocumentModel.TIPO_COTIZACION;
                        }
                        else
                        {
                            comboBoxDocumentTypeFrmVenta.SelectedIndex = 0;
                        }
                    }
                    else if (count == 2)
                    {
                        if (comboBoxDocumentTypeFrmVenta.Items[0].ToString().Trim().Equals("Pedido"))
                            comboBoxDocumentTypeFrmVenta.SelectedIndex = 0;
                        else comboBoxDocumentTypeFrmVenta.SelectedIndex = 1;
                    }
                    else if (count == 3)
                    {
                        if (comboBoxDocumentTypeFrmVenta.Items[0].ToString().Trim().Equals("Venta"))
                        {
                            if (comboBoxDocumentTypeFrmVenta.Items[1].ToString().Trim().Equals("Cotización"))
                                comboBoxDocumentTypeFrmVenta.SelectedIndex = 2;
                            else comboBoxDocumentTypeFrmVenta.SelectedIndex = 1;
                        }
                        else
                        {
                            comboBoxDocumentTypeFrmVenta.SelectedIndex = 2;
                        }
                    }
                    else
                    {
                        comboBoxDocumentTypeFrmVenta.SelectedIndex = 2;
                    }

                }
                else if (documentType == DocumentModel.TIPO_DEVOLUCION)
                {
                    if (count == 1)
                    {
                        if (comboBoxDocumentTypeFrmVenta.Items[0].ToString().Trim().Equals("Venta"))
                        {
                            comboBoxDocumentTypeFrmVenta.SelectedIndex = 0;
                            documentType = DocumentModel.TIPO_REMISION;
                        }
                        else if (comboBoxDocumentTypeFrmVenta.Items[0].ToString().Trim().Equals("Cotización"))
                        {
                            comboBoxDocumentTypeFrmVenta.SelectedIndex = 0;
                            documentType = DocumentModel.TIPO_COTIZACION;
                        }
                        else if (comboBoxDocumentTypeFrmVenta.Items[0].ToString().Trim().Equals("Pedido"))
                        {
                            comboBoxDocumentTypeFrmVenta.SelectedIndex = 0;
                            documentType = DocumentModel.TIPO_PEDIDO;
                        }
                        else
                        {
                            comboBoxDocumentTypeFrmVenta.SelectedIndex = 0;
                        }
                    }
                    else if (count == 2)
                    {
                        if (comboBoxDocumentTypeFrmVenta.Items[0].ToString().Trim().Equals("Devolución"))
                            comboBoxDocumentTypeFrmVenta.SelectedIndex = 0;
                        else comboBoxDocumentTypeFrmVenta.SelectedIndex = 1;
                    }
                    else if (count == 3)
                    {
                        if (comboBoxDocumentTypeFrmVenta.Items[0].ToString().Trim().Equals("Cotización"))
                            comboBoxDocumentTypeFrmVenta.SelectedIndex = 2;
                        else
                        {
                            comboBoxDocumentTypeFrmVenta.SelectedIndex = 2;
                        }
                    }
                    else if (count == 4)
                        comboBoxDocumentTypeFrmVenta.SelectedIndex = 3;
                }
            }
        }

        private void CmbPreciosNew_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboPreciosItemVenta.Focused)
            {
                string[] valor = comboPreciosItemVenta.Text.Split('$');
                double priceSeleccionated = Convert.ToDouble(valor[1].Replace("$", "").Replace(",", "").Replace("MXN", ""));
                editPriceItemVenta.Text = priceSeleccionated.ToString("C", CultureInfo.CurrentCulture);
            } else if (!comboPreciosItemVenta.Focused && !editMove)
            {
                string[] valor = comboPreciosItemVenta.Text.Split('$');
                double priceSeleccionated = Convert.ToDouble(valor[1].Replace("$", "").Replace(",", "").Replace("MXN", ""));
                editPriceItemVenta.Text = priceSeleccionated.ToString("C", CultureInfo.CurrentCulture);
            }
        }

        private async void DataGridArticulos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textporcentajepromocion.Text = "0";
            textporcentajepromocion.Visible = false;
            textPromocionesMovimiento.Visible = false;
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.ColumnIndex <= 9) {
                editMove = true;
                if (movesList.Count > 0)
                {
                    MovimientosModel mm = movesList[e.RowIndex];
                    if (mm != null)
                    {
                        seleccionoUnItem = true;
                        procesoPrincipal = false;
                        procesoLlenadoInfoMovimiento = true;
                        await fillAllFieldsFromAMovement(mm);
                    }
                } else
                {
                    FormMessage formMessage = new FormMessage("Movmieno No Encontrado", "La lista de movimientos está vacia pausar o eliminar el documento", 3);
                    formMessage.ShowDialog();
                }
            } else if (e.RowIndex >= 0 && e.ColumnIndex == 10) {
                formWaiting = new FormWaiting(this, 1, e.RowIndex);
                formWaiting.ShowDialog();
            }
        }

        public async Task processToDeleteMovement(int rowIndex)
        {
            idMovement = Convert.ToInt32(dataGridMovements.Rows[rowIndex].Cells[0].Value.ToString().Trim());
            String itemCode = dataGridMovements.Rows[rowIndex].Cells[1].Value.ToString().Trim();
            int itemId = Convert.ToInt32(dataGridMovements.Rows[rowIndex].Cells[11].Value.ToString().Trim());
            ClsItemModel item = ItemModel.getAllDataFromAnItemWithCode(itemCode);
            if (permissionPrepedido)
            {
                if (item != null)
                {
                    String query = "SELECT * FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " +
                        LocalDatabase.CAMPO_ARTICULOID_MOV + " = " + itemId + " AND " +
                        LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " + idDocument;
                    MovimientosModel mm = MovimientosModel.getAMovement(query);
                    if (mm != null)
                    {
                        if (formWaiting != null)
                            formWaiting.Close();
                        FrmConfirmation frmConfirmation = new FrmConfirmation("Eliminar Movimiento", "¿Estás seguro de que deseas eliminar el movimiento?");
                        frmConfirmation.StartPosition = FormStartPosition.CenterScreen;
                        frmConfirmation.ShowDialog();
                        if (FrmConfirmation.confirmation)
                            await eliminarElMovimientoDelCarrito(dataGridMovements.Rows[rowIndex], rowIndex, item, idDocument);
                    } else
                    {
                        if (formWaiting != null)
                        {
                            formWaiting.Dispose();
                            formWaiting.Close();
                        }
                    }
                }
                else
                {
                    if (formWaiting != null)
                    {
                        formWaiting.Dispose();
                        formWaiting.Close();
                    }
                    FormMessage formMessage = new FormMessage("Item No Encontrado", "Probablemente el artículo no existe en la base de datos local", 3);
                    formMessage.ShowDialog();
                }
            }
            else
            {
                if (serverModeLAN)
                {
                    dynamic response = await ItemsController.getAnItemFromTheServerLAN(itemId, null, codigoCaja);
                    if (response.value == 1)
                        item = response.item;
                    if (item != null)
                    {
                        String query = "SELECT * FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " +
                            LocalDatabase.CAMPO_ARTICULOID_MOV + " = " + item.id + " AND " + LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " + idDocument;
                        MovimientosModel mm = MovimientosModel.getAMovement(query);
                        if (mm != null)
                        {
                            await eliminarElMovimientoDelCarrito(dataGridMovements.Rows[rowIndex], rowIndex, item, idDocument);
                        } else
                        {
                            if (formWaiting != null)
                            {
                                formWaiting.Dispose();
                                formWaiting.Close();
                            }
                        }
                    }
                    else
                    {
                        if (formWaiting != null)
                        {
                            formWaiting.Dispose();
                            formWaiting.Close();
                        }
                        FormMessage formMessage = new FormMessage("Item No Encontrado", "Probablemente el artículo no existe en la base de datos local", 3);
                        formMessage.ShowDialog();
                    }
                }
                else
                {
                    if (item != null)
                    {
                        String query = "SELECT * FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " +
                            LocalDatabase.CAMPO_ARTICULOID_MOV + " = " + item.id + " AND " + LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " + idDocument;
                        MovimientosModel mm = MovimientosModel.getAMovement(query);
                        if (mm != null)
                        {
                            await eliminarElMovimientoDelCarrito(dataGridMovements.Rows[rowIndex], rowIndex, item, idDocument);
                        }
                    }
                    else
                    {
                        if (formWaiting != null)
                        {
                            formWaiting.Dispose();
                            formWaiting.Close();
                        }
                        FormMessage formMessage = new FormMessage("Item No Encontrado", "Probablemente el artículo no existe en la base de datos local", 3);
                        formMessage.ShowDialog();
                    }
                }
            }
        }

        private async Task eliminarElMovimientoDelCarrito(DataGridViewRow currentRow, int rowIndex, ClsItemModel itemModel, int idDoc)
        {
            DeleteMovementController dmc = new DeleteMovementController(idMovement, idDoc);
            int response = await dmc.doInBackground(itemModel, serverModeLAN, webActive, codigoCaja);
            if (response == -1)
            {
                if (formWaiting != null)
                {
                    formWaiting.Dispose();
                    formWaiting.Close();
                }
                FormMessage message = new FormMessage("Excepción Encontrada", "Ocurrio un error al quitar el articulo del carrito!", 2);
                message.ShowDialog();
            }
            else if (response == -2)
            {
                if (formWaiting != null)
                {
                    formWaiting.Dispose();
                    formWaiting.Close();
                }
                FormMessage message = new FormMessage("Excepción Encontrada", "No se pudo, sumar la cantidad a la existencia!", 2);
                message.ShowDialog();
            }
            if (movesList.Count > 0)
            {
                movesList.RemoveAt(rowIndex);
                if (movesList.Count > 0)
                    lastId = movesList[movesList.Count - 1].id;
                else lastId = 0;
            }
            dataGridMovements.Rows.Remove(currentRow);
            actualizarSubDescYTotalVenta(idDocument);
            clearMovementValues();
            clearMovementsAddedData();
            await updateTotalNumberOfMovements();
            if (formWaiting != null)
                formWaiting.Close();
        }

        private async Task fillAllFieldsFromAMovement(MovimientosModel mm)
        {
            textporcentajepromocion.Visible = true;
            textPromocionesMovimiento.Visible = true;
            if (procesoLlenadoInfoMovimiento)
            {
                procesoLlenadoInfoMovimiento = false;
                if (comboPreciosItemVenta != null)
                    comboPreciosItemVenta.Items.Clear();
                if (mm != null)
                {
                    btnAgregar.Text = "Actualizar";
                    idMovement = mm.id;
                    int idItem = mm.itemId;
                    if (!serverModeLAN)
                    {
                        if (webActive)
                        {
                            dynamic responseItem = await ItemsController.getAnItemFromTheServerAPI(idItem, null, codigoCaja);
                            if (responseItem.value == 1)
                                itemModel = responseItem.item;
                            else itemModel = ItemModel.getAllDataFromAnItem(idItem);
                        } else
                        {
                            itemModel = ItemModel.getAllDataFromAnItem(idItem);
                        }
                    }
                    else itemModel = await getItemInformationViaLAN(idItem);
                    if (seleccionoUnItem)
                    {
                        if (serverModeLAN)
                        {
                            dynamic responseUnitsPendings = await ItemsController.getUnitsPendingsLAN(idItem);
                            if (responseUnitsPendings.value == 1)
                            {
                                itemModel.existencia = (itemModel.existencia - responseUnitsPendings.unidadesPendientes);
                                double unidadesPendientesLocales = MovimientosModel.getUnidadesBasePendientesLocales(itemModel.id, 0, true);
                                double existenciaLocal = (itemModel.existencia - unidadesPendientesLocales);
                                if (itemModel.existencia > 0)
                                {
                                    textExistenciaReal.Text = "Existencia Real: " + existenciaLocal;
                                    textExistenciaReal.ForeColor = Color.Blue;
                                    textExistenciaReal.Visible = true;
                                }
                                else
                                {
                                    textExistenciaReal.Text = "Existencia Real: " + existenciaLocal;
                                    textExistenciaReal.ForeColor = Color.Red;
                                    textExistenciaReal.Visible = true;
                                }
                            }
                        }
                        else
                        {
                            if (webActive)
                            {
                                dynamic responseUnitsPendings = await ItemsController.getUnitsPendingsAPI(idItem, codigoCaja);
                                if (responseUnitsPendings.value == 1)
                                {
                                    itemModel.existencia = (itemModel.existencia - responseUnitsPendings.unidadesPendientes);
                                    double unidadesPendientesLocales = MovimientosModel.getUnidadesBasePendientesLocales(itemModel.id, 0, true);
                                    double existenciaLocal = (itemModel.existencia - unidadesPendientesLocales);
                                    if (itemModel.existencia > 0)
                                    {
                                        textExistenciaReal.Text = "Existencia Real: " + existenciaLocal;
                                        textExistenciaReal.ForeColor = Color.Blue;
                                        textExistenciaReal.Visible = true;
                                    }
                                    else
                                    {
                                        textExistenciaReal.Text = "Existencia Real: " + existenciaLocal;
                                        textExistenciaReal.ForeColor = Color.Red;
                                        textExistenciaReal.Visible = true;
                                    }
                                }
                            } else
                            {
                                double unidadesPendientesLocales = MovimientosModel.getUnidadesBasePendientesLocales(itemModel.id, 0, true);
                                double existenciaLocal = (itemModel.existencia - unidadesPendientesLocales);
                                if (itemModel.existencia > 0)
                                {
                                    textExistenciaReal.Text = "Existencia Temporal: " + existenciaLocal;
                                    textExistenciaReal.ForeColor = Color.Blue;
                                    textExistenciaReal.Visible = true;
                                }
                                else
                                {
                                    textExistenciaReal.Text = "Existencia Temporal: " + existenciaLocal;
                                    textExistenciaReal.ForeColor = Color.Red;
                                    textExistenciaReal.Visible = true;
                                }
                            }
                        }
                    }
                    downloadImageItem(idItem);
                    double porcentajepromocion = MovimientosModel.getPorcentajePromotionMoviments(mm.id);
                    if (porcentajepromocion > 0)
                    {
                        textporcentajepromocion.Visible = true;
                        textporcentajepromocion.Text = porcentajepromocion + "%";
                        textPromocionesMovimiento.Visible = true;
                    }
                    else
                    {
                        textporcentajepromocion.Visible = false;
                        textporcentajepromocion.Text = "0";
                        textPromocionesMovimiento.Visible = false;
                    }
                    comboCodigoItemVenta.Text = mm.itemCode;
                    editNombreItemVenta.Text = itemModel.nombre;
                    await loadInitialData(0, mm, false);
                    editCapturedUnits.Text = mm.capturedUnits.ToString();
                    double realdescuento = mm.descuentoPorcentaje;

                    editDiscountItemVenta.Text = (realdescuento).ToString();
                    editPriceItemVenta.Text = mm.price.ToString("C", CultureInfo.CurrentCulture);
                    panelObservationMovement.Visible = true;

                    if (mm.nonConvertibleUnits != 0)
                    {
                        String unidadNoConvertibleName = "";
                        if (serverModeLAN)
                        {
                            dynamic responseUnit = await UnitsOfMeasureAndWeightController.getNameOfAndUnitMeasureWeightLAN(mm.nonConvertibleUnitId);
                            if (responseUnit.value == 1)
                                unidadNoConvertibleName = responseUnit.name;
                        } else
                        {
                            if (webActive)
                            {
                                dynamic responseUnit = await UnitsOfMeasureAndWeightController.getNameOfAndUnitMeasureWeightAPI(mm.nonConvertibleUnitId);
                                if (responseUnit.value == 1)
                                    unidadNoConvertibleName = responseUnit.name;
                                else unidadNoConvertibleName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(mm.nonConvertibleUnitId);
                            } else
                            {
                                unidadNoConvertibleName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(mm.nonConvertibleUnitId);
                            }
                        }
                        textUnidadNoConvertible.Text = unidadNoConvertibleName;
                        textUnidadNoConvertible.Visible = true;
                        editUnidadNoConvertible.Visible = true;
                        editUnidadNoConvertible.Text = mm.nonConvertibleUnits + "";
                    }
                    else
                    {
                        textUnidadNoConvertible.Visible = false;
                        editUnidadNoConvertible.Visible = false;
                    }
                    if (ConfiguracionModel.scalePermissionIsActivated())
                    {
                        btnOpenScale.Visible = true;
                        if (permissionPrepedido)
                        {
                            btnOpenScale.Location = new Point(btnOpenScale.Location.X, btnOpenScale.Location.Y - (btnOpenScale.Location.Y - 5));
                            btnOpenScale.Height = 45;
                            btnOpenScale.Width = 60;
                        }
                    }
                    else btnOpenScale.Visible = false;
                    editObservationMovement.Text = mm.observations;
                    if (permissionPrepedido)
                    {
                        if (!DocumentModel.isItDocumentFromAPrepedido(idDocument))
                        {
                            textInfoCantidad.Visible = false;
                            editCapturedUnits.Visible = false;
                            btnOpenScale.Visible = false;
                            editUnidadNoConvertible.Select();
                            pictureBoxInfoSeleccionarPrecio.Visible = false;
                            comboPreciosItemVenta.Visible = true;
                            editPriceItemVenta.Visible = true;
                            pictureBoxInfoDescuento.Visible = false;
                            editDiscountItemVenta.Visible = false;
                            textDiscountRateInfoVenta.Visible = false;
                        } else
                        {
                            textInfoCantidad.Visible = true;
                            editCapturedUnits.Visible = true;
                            btnOpenScale.Visible = true;
                            pictureBoxInfoSeleccionarPrecio.Visible = true;
                            comboPreciosItemVenta.Visible = true;
                            editPriceItemVenta.Visible = true;
                            pictureBoxInfoDescuento.Visible = true;
                            editDiscountItemVenta.Visible = true;
                            textDiscountRateInfoVenta.Visible = true;
                        }
                    }
                }
                else
                {
                    FormMessage msj = new FormMessage("Busqueda Finalizada!", "El Movimiento no existe, verificar código o productos!", 2);
                    msj.ShowDialog();
                }
                procesoLlenadoInfoMovimiento = true;

            }
        }
        //D3sde4Pps$22_E
        private async Task downloadImageItem(int idItem)
        {
            if (idItem != 0)
            {
                await Task.Run(async () =>
                {
                    if (serverModeLAN)
                    {
                        String rutaLocal = MetodosGenerales.rootDirectory + "\\Imagenes\\items";
                        await SubirImagenesController.getImageLAN("items", rutaLocal, idItem);
                    } else
                    {
                        if (webActive)
                            await SubirImagenesController.getImage("-1", idItem);
                    }
                });
            }
            showItemImage(idItem);
        }

        private async Task<ClsItemModel> getItemInformationViaLAN(int idItem)
        {
            ClsItemModel itemModel = null;
            dynamic response = await ItemsController.getAnItemFromTheServerLAN(idItem, null, codigoCaja);
            if (response.value == 1)
                itemModel = response.item;
            else
            {
                FormMessage formMessage = new FormMessage("Exception", response.description, 2);
                formMessage.ShowDialog();
            }
            return itemModel;
        }

        private void cobrarCarritoTpv(int idDocumento)
        {
            if (completarDocumentoConTotales(idDocumento))
            {
                if (MovimientosModel.checkIfThereAreStillMovementsForTheDocumentInShift(idDocumento))
                {
                    documentType = DocumentModel.getDocumentType(idDocumento);
                    FormPayCart fpc = new FormPayCart(this, idDocument, documentType, activateOpcionFacturar,
                        webActive, cotmosActive);
                    fpc.StartPosition = FormStartPosition.CenterScreen;
                    fpc.ShowDialog();
                    if (idDocument != 0)
                    {
                        btnRecuperar.Visible = false;
                        btnSurtirPedidos.Visible = false;
                        idCustomer = DocumentModel.getCustomerIdOfADocument(idDocument);
                        getAndFillCustomerInformation(idCustomer);
                    } else
                    {
                        btnRecuperar.Visible = true;
                        btnSurtirPedidos.Visible = true;
                        if (editMove)
                        {
                            clearMovementsAddedData();
                            clearMovementValues();
                            editMove = false;
                        }
                        getAndFillCustomerInformation(0);
                    }
                    if (permissionPrepedido) {
                        if (DocumentModel.isItDocumentFromAPrepedido(idDocumento))
                        {
                            textInfoDocumentTypeFrmVenta.Text = "Surtiendo PrePedido";
                            comboBoxDocumentTypeFrmVenta.Visible = false;
                        } else
                        {
                            textInfoDocumentTypeFrmVenta.Text = "Tipo de Documento";
                            comboBoxDocumentTypeFrmVenta.Visible = true;
                        }
                    } else {
                        textInfoDocumentTypeFrmVenta.Text = "Tipo de Documento";
                        comboBoxDocumentTypeFrmVenta.Visible = true;
                    }
                    comboCodigoItemVenta.Select();
                }
                else
                {
                    FormMessage fm = new FormMessage("Documento No Encontrado", "No hay artículos que cobrar", 2);
                    fm.ShowDialog();
                }
            }
            else
            {
                FormMessage fm = new FormMessage("Documento No Encontrado", "No existe ningún documento en turno!", 3);
                fm.ShowDialog();
            }
        }

        private Boolean completarDocumentoConTotales(int idDocumento)
        {
            dynamic sumsMap = getCurrentSumsFromDocument(idDocumento);
            return DocumentModel.updateInformationToCobrarDocumentTpv(sumsMap.descuento, sumsMap.total, 0, idDocumento);
        }

        public async Task resetearTodosLosValores(bool resetCustomer)
        {
            idDocument = 0;
            activateOpcionFacturar = 1;
            textFolioFrmVenta.Text = "Folio #";
            textSubtotalFrmVenta.Text = "Subtotal $ 0  MXN";
            textDiscountFrmVenta.Text = "Descuento $ 0 MXN";
            textTotalFrmVenta.Text = "$ 0 MXN";
            editCapturedUnits.Text = Convert.ToString(1);
            if (permissionPrepedido)
            {
                btnRecuperar.Visible = true;
                btnSurtirPedidos.Visible = true;
                btnBuscarClientesNew.Enabled = true;
            }
            else
            {
                editDiscountItemVenta.Text = "";
                btnRecuperar.Visible = true;
            }
            editObservationMovement.MaxLength = 250;
            editObservationMovement.Text = "";
            dataGridMovements.Rows.Clear();
            movesList.Clear();
            comboCodigoItemVenta.Focus();
            await updateTotalNumberOfMovements();
            textExistenciaReal.Visible = false;
            if (resetCustomer)
            {
                getAndFillCustomerInformation(0);
            }
        }

        private async Task getAndFillCustomerInformation(int idCustomer)
        {
            await downloadIdClientePublicoEnGeneral();
            editCapturedUnits.Text = Convert.ToString(1);
            if (idCustomer == 0)
                idCustomer = ClsRegeditController.getIdDefaultCustomer();
            FormVenta.idCustomer = idCustomer;
            await callGetADataCustomer(idCustomer);
            if (customerModel != null)
            {
                editNombreCliente.Text = customerModel.NOMBRE;
                editDiscountItemVenta.Text = customerModel.descuentoMovimiento + "";
                comboCodigoItemVenta.Focus();
                downloadImageCustomer(idCustomer);
            }
        }

        public async Task clearMovementValues()
        {
            itemModel = null;
            editCapturedUnits.Text = 1 + "";
            comboCodigoItemVenta.Text = "";
            editNombreItemVenta.Text = "";
            editPriceItemVenta.Text = 0 + "";
            if (idCustomer != 0)
            {
                double descuentoCliente = 0;
                if (serverModeLAN)
                {
                    dynamic responseDesc = await CustomersController.getDescuentoClienteLAN(idCustomer);
                    if (responseDesc.value == 1)
                        descuentoCliente = responseDesc.discount;
                } else
                {
                    descuentoCliente = CustomerModel.getDescuentoDelCliente(idCustomer);
                }
                editDiscountItemVenta.Text = descuentoCliente + "";
            } else editDiscountItemVenta.Text = 0 + "";
            if (comboPreciosItemVenta != null)
                comboPreciosItemVenta.Items.Clear();
            if (comboBoxUnitMWITemVenta != null)
                comboBoxUnitMWITemVenta.Items.Clear();
            editObservationMovement.MaxLength = 250;
            editObservationMovement.Text = "";
            panelObservationMovement.Visible = false;
            editUnidadNoConvertible.Text = "0";
            editUnidadNoConvertible.Visible = false;
            textUnidadNoConvertible.Visible = false;
            idMovement = 0;
            editMove = false;
            btnAgregar.Text = "Agregar";
            showItemImage(0);
            textExistenciaReal.Visible = false;
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            panelSubtotales.BackColor = Color.FromArgb(255, 87, 34);
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            //panel4.BackColor = System.Drawing.Color.FromArgb(255, 87, 34);
            cobrarCarritoTpv(idDocument);
            //panel4.BackColor = System.Drawing.Color.Transparent;
        }

        private void BtnCobrar_Click(object sender, EventArgs e)
        {
            //panel4.BackColor = Color.FromArgb(255, 87, 34);
            cobrarCarritoTpv(idDocument);
            //panel4.BackColor = Color.Transparent;
        }

        private async Task fillAllFieldsFromAMovement(ClsItemModel item, double captedUnits, bool barCode)
        {
            if (comboPreciosItemVenta != null)
                comboPreciosItemVenta.Items.Clear();
            if (item != null)
            {
                if (seleccionoUnItem)
                {
                    if (serverModeLAN)
                    {
                        dynamic responseUnitsPendings = await ItemsController.getUnitsPendingsLAN(item.id);
                        if (responseUnitsPendings.value == 1)
                        {
                            item.existencia = (item.existencia - responseUnitsPendings.unidadesPendientes);
                            if (!barCode)
                            {
                                double unidadesPendientesLocales = MovimientosModel.getUnidadesBasePendientesLocales(item.id, 0, true);
                                double existenciaLocal = (item.existencia - unidadesPendientesLocales);
                                if (item.existencia > 0)
                                {
                                    textExistenciaReal.Text = "Existencia Real: " + existenciaLocal;
                                    textExistenciaReal.ForeColor = Color.Blue;
                                    textExistenciaReal.Visible = true;
                                }
                                else
                                {
                                    textExistenciaReal.Text = "Existencia Real: " + existenciaLocal;
                                    textExistenciaReal.ForeColor = Color.Red;
                                    textExistenciaReal.Visible = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (webActive)
                        {
                            dynamic responseUnitsPendings = await ItemsController.getUnitsPendingsAPI(item.id, codigoCaja);
                            if (responseUnitsPendings.value == 1)
                            {
                                item.existencia = (item.existencia - responseUnitsPendings.unidadesPendientes);
                                if (!barCode)
                                {
                                    double unidadesPendientesLocales = MovimientosModel.getUnidadesBasePendientesLocales(item.id, 0, true);
                                    double existenciaLocal = (item.existencia - unidadesPendientesLocales);
                                    if (item.existencia > 0)
                                    {
                                        textExistenciaReal.Text = "Existencia Real: " + existenciaLocal;
                                        textExistenciaReal.ForeColor = Color.Blue;
                                        textExistenciaReal.Visible = true;
                                    }
                                    else
                                    {
                                        textExistenciaReal.Text = "Existencia Real: " + existenciaLocal;
                                        textExistenciaReal.ForeColor = Color.Red;
                                        textExistenciaReal.Visible = true;
                                    }
                                }
                            }
                        } else
                        {
                            if (!barCode)
                            {
                                double unidadesPendientesLocales = MovimientosModel.getUnidadesBasePendientesLocales(item.id, 0, true);
                                double existenciaLocal = (item.existencia - unidadesPendientesLocales);
                                if (item.existencia > 0)
                                {
                                    textExistenciaReal.Text = "Existencia Temporal: " + existenciaLocal;
                                    textExistenciaReal.ForeColor = Color.Blue;
                                    textExistenciaReal.Visible = true;
                                }
                                else
                                {
                                    textExistenciaReal.Text = "Existencia Temporal: " + existenciaLocal;
                                    textExistenciaReal.ForeColor = Color.Red;
                                    textExistenciaReal.Visible = true;
                                }
                            }
                        }
                    }
                }
                editCapturedUnits.Text = captedUnits.ToString();
                if (barCode)
                {
                    await getCapturedUnitIdFromAnItemWithBarCodeProcess(item);
                    finalRealPrice = await getPriceOfAnItemForCurrentCustomer(item, capturedUnitId, true, barCode);
                    editPriceItemVenta.Text = finalRealPrice + "";
                } else
                {
                    downloadImageItem(item.id);
                    if (item.codigoAlterno != null && item.codigoAlterno.Equals(""))
                        comboCodigoItemVenta.Text = item.codigo;
                    else comboCodigoItemVenta.Text = item.codigoAlterno;
                    editNombreItemVenta.Text = item.nombre;
                    await loadInitialData(0, null, false);
                    panelObservationMovement.Visible = true;
                    if (item.nonConvertibleUnitId != 0)
                    {
                        textUnidadNoConvertible.Text = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(item.nonConvertibleUnitId);
                        textUnidadNoConvertible.Visible = true;
                        editUnidadNoConvertible.Visible = true;
                        editUnidadNoConvertible.Text = "0";
                    }
                    else
                    {
                        textUnidadNoConvertible.Visible = false;
                        editUnidadNoConvertible.Visible = false;
                    }
                    if (ConfiguracionModel.scalePermissionIsActivated())
                    {
                        btnOpenScale.Visible = true;
                        if (permissionPrepedido)
                        {
                            btnOpenScale.Location = new Point(btnOpenScale.Location.X, btnOpenScale.Location.Y - 5);
                            btnOpenScale.Height = 45;
                            btnOpenScale.Width = 60;
                        }
                    }
                    else btnOpenScale.Visible = false;
                    if (permissionPrepedido)
                    {
                        if (!DocumentModel.isItDocumentFromAPrepedido(idDocument))
                        {
                            textInfoCantidad.Visible = false;
                            editCapturedUnits.Visible = false;
                            btnOpenScale.Visible = false;
                            editUnidadNoConvertible.Select();
                            pictureBoxInfoSeleccionarPrecio.Visible = false;
                            comboPreciosItemVenta.Visible = true;
                            editPriceItemVenta.Visible = true;
                            pictureBoxInfoDescuento.Visible = false;
                            editDiscountItemVenta.Visible = false;
                            textDiscountRateInfoVenta.Visible = false;
                        } else
                        {
                            textInfoCantidad.Visible = true;
                            editCapturedUnits.Visible = true;
                            btnOpenScale.Visible = true;
                            pictureBoxInfoSeleccionarPrecio.Visible = true;
                            comboPreciosItemVenta.Visible = true;
                            editPriceItemVenta.Visible = true;
                            pictureBoxInfoDescuento.Visible = true;
                            editDiscountItemVenta.Visible = true;
                            textDiscountRateInfoVenta.Visible = true;
                        }
                    }
                }
            } else {
                FormMessage msj = new FormMessage("Busqueda Finalizada!", "El artículo no existe, verificar código o productos!", 2);
                msj.ShowDialog();
            }
        }

        private async Task getCapturedUnitIdFromAnItemWithBarCodeProcess(ClsItemModel item)
        {
            await Task.Run(async () =>
            {
                if (item.salesUnitId == 0)
                    item.salesUnitId = item.baseUnitId;
                if (item.salesUnitId != 0)
                {
                    if (serverModeLAN)
                    {
                        dynamic responseUnidad = await UnitsOfMeasureAndWeightController.getAUnidadLAN(item.salesUnitId);
                        if (responseUnidad.value == 1)
                        {
                            ClsUnitsMeasureWeightModel umw = responseUnidad.umw;
                            if (umw != null)
                                capturedUnitId = umw.idServer;
                            else capturedUnitId = 0;
                        }
                    }
                    else
                    {
                        if (webActive)
                        {
                            dynamic responseUnidad = await UnitsOfMeasureAndWeightController.getAUnidadAPI(item.salesUnitId);
                            if (responseUnidad.value == 1)
                            {
                                ClsUnitsMeasureWeightModel umw = responseUnidad.umw;
                                if (umw != null)
                                    capturedUnitId = umw.idServer;
                                else capturedUnitId = 0;
                            }
                            else
                            {
                                ClsUnitsMeasureWeightModel umw = UnitsOfMeasureAndWeightModel.getAUnidad(item.salesUnitId);
                                if (umw != null)
                                    capturedUnitId = umw.idServer;
                                else capturedUnitId = 0;
                            }
                        } else
                        {
                            ClsUnitsMeasureWeightModel umw = UnitsOfMeasureAndWeightModel.getAUnidad(item.salesUnitId);
                            if (umw != null)
                                capturedUnitId = umw.idServer;
                            else capturedUnitId = 0;
                        }
                    }
                }
                else
                {
                    capturedUnitId = 0;
                }
            });
        }

        private async Task loadInitialData(int call, MovimientosModel mm, bool barCode)
        {
            showAndHideProgressLoadPrices(true);
            bool validateUnits = false;
            if (itemModel.salesUnitId == 0)
                itemModel.salesUnitId = itemModel.baseUnitId;
            if (call == 0)
            {
                if (umwList != null)
                    umwList.Clear();
                if (serverModeLAN)
                {
                    dynamic responseEquivalent = await UnitsOfMeasureAndWeightController.obtainUnitsEquivalentToBaseUnitLAN(itemModel.baseUnitId);
                    if (responseEquivalent.value == 1)
                        umwList = responseEquivalent.cumwmList;
                }
                else
                {
                    umwList = UnitsOfMeasureAndWeightModel.obtainUnitsEquivalentToBaseUnit(itemModel.baseUnitId);
                }
                if (!barCode)
                {
                    if (umwList != null && umwList.Count > 0)
                    {
                        for (int i = 0; i < umwList.Count; i++)
                        {
                            if (i == (umwList.Count - 1))
                                capturedUnitId = umwList[i].idServer;
                        }
                        textUnidadDeMedida.Visible = true;
                        comboBoxUnitMWITemVenta.Visible = true;
                        validateUnits = true;
                        if (mm != null)
                            fillComboUnitsMeasureAndWeight(umwList, mm.capturedUnitId);
                        else fillComboUnitsMeasureAndWeight(umwList, itemModel.salesUnitId);
                    }
                    else
                    {
                        if (itemModel.salesUnitId != 0)
                        {
                            ClsUnitsMeasureWeightModel umw = UnitsOfMeasureAndWeightModel.getAUnidad(itemModel.salesUnitId);
                            if (umw == null)
                            {
                                if (serverModeLAN)
                                {
                                    dynamic responseUnidad = await UnitsOfMeasureAndWeightController.getAUnidadLAN(itemModel.salesUnitId);
                                    if (responseUnidad.value == 1)
                                        umw = responseUnidad.umw;
                                }
                                else
                                {
                                    umw = UnitsOfMeasureAndWeightModel.getAUnidad(itemModel.salesUnitId);
                                }
                            }
                            else
                            {
                                if (serverModeLAN)
                                {
                                    dynamic responseUnit = await UnitsOfMeasureAndWeightController.getAUnidadLAN(itemModel.salesUnitId);
                                    if (responseUnit.value == 1)
                                        umw = responseUnit.umw;
                                }
                                else
                                {
                                    umw = UnitsOfMeasureAndWeightModel.getAUnidad(itemModel.salesUnitId);
                                }
                            }
                            if (umw != null)
                            {
                                umwList = new List<ClsUnitsMeasureWeightModel>();
                                umwList.Add(umw);
                                if (mm != null)
                                    fillComboUnitsMeasureAndWeight(umwList, mm.capturedUnitId);
                                else fillComboUnitsMeasureAndWeight(umwList, itemModel.salesUnitId);
                            }
                            else
                            {
                                comboBoxUnitMWITemVenta.Visible = false;
                                comboBoxUnitMWITemVenta.Enabled = true;
                                capturedUnitId = 0;
                            }
                        }
                        else
                        {
                            comboBoxUnitMWITemVenta.Visible = false;
                            capturedUnitId = 0;
                        }
                    }
                }
                if (!validateUnits)
                {
                    if (selectPricesPermission)
                    {
                        pictureBoxInfoSeleccionarPrecio.Visible = true;
                        comboPreciosItemVenta.Visible = true;
                        comboPreciosItemVenta.Enabled = true;
                    } else
                    {
                        pictureBoxInfoSeleccionarPrecio.Visible = false;
                        comboPreciosItemVenta.Visible = false;
                        comboPreciosItemVenta.Enabled = false;
                    }
                    if (modifyPricePermission)
                    {
                        editPriceItemVenta.Enabled = true;
                        editPriceItemVenta.ReadOnly = false;
                    }
                    else
                    {
                        editPriceItemVenta.ReadOnly = true;
                    }
                    if (!barCode)
                    {
                        if (selectPricesPermission)
                        {
                            List<ClsPreciosEmpresaModel> pricesList = await ItemsController.getPricesOfAnItem(itemModel, capturedUnitId,
                                customerModel.PRECIO_EMPRESA_ID, serverModeLAN, codigoCaja);
                            if (pricesList != null)
                            {
                                fillComboPricesFromTheItem(pricesList);
                            }
                        }
                    }
                    finalRealPrice = await getPriceOfAnItemForCurrentCustomer(itemModel, capturedUnitId, validateUnits, barCode);
                    if (!editMove)
                        editPriceItemVenta.Text = finalRealPrice.ToString("C", CultureInfo.CurrentCulture);
                }
            }
            else if (call == 1)
            {
                if (umwList != null)
                {
                    comboBoxUnitMWITemVenta.Visible = true;
                    validateUnits = true;
                }
                if (validateUnits)
                {
                    if (selectPricesPermission)
                    {
                        pictureBoxInfoSeleccionarPrecio.Visible = true;
                        comboPreciosItemVenta.Visible = true;
                        comboPreciosItemVenta.Enabled = true;
                    }
                    else
                    {
                        pictureBoxInfoSeleccionarPrecio.Visible = false;
                        comboPreciosItemVenta.Visible = false;
                        comboPreciosItemVenta.Enabled = false;
                    }
                    if (modifyPricePermission)
                    {
                        editPriceItemVenta.Enabled = true;
                        editPriceItemVenta.ReadOnly = false;
                    }
                    else
                    {
                        editPriceItemVenta.ReadOnly = true;
                    }
                    if (!barCode)
                    {
                        if (selectPricesPermission)
                        {
                            List<ClsPreciosEmpresaModel> pricesList = await ItemsController.getPricesOfAnItem(itemModel, capturedUnitId,
                                customerModel.PRECIO_EMPRESA_ID, serverModeLAN, codigoCaja);
                            if (pricesList != null)
                            {
                                fillComboPricesFromTheItem(pricesList);
                            }
                        }
                    }
                    finalRealPrice = await getPriceOfAnItemForCurrentCustomer(itemModel, capturedUnitId, validateUnits, barCode);
                    if (!editMove)
                        editPriceItemVenta.Text = finalRealPrice.ToString("C", CultureInfo.CurrentCulture);
                }
            }
            if (UserModel.doYouHavePermissionToDoDiscounts(ClsRegeditController.getIdUserInTurn()))
                editDiscountItemVenta.Enabled = true;
            else editDiscountItemVenta.Enabled = false;
            showAndHideProgressLoadPrices(false);
        }

        private void showAndHideProgressLoadPrices(bool visible)
        {
            progressBarLoadPrices.Value = 1;
            progressBarLoadPrices.Visible = visible;
        }

        private async Task<double> getPriceOfAnItemForCurrentCustomer(ClsItemModel itemModel, int capturedUnitId, bool validateUnits, bool barcode)
        {
            double price = 0;
            await Task.Run(async () =>
            {
                int capturedUnitIsMajor = -1;
                if (validateUnits)
                {
                    if (serverModeLAN)
                    {
                        dynamic responseUnitHegher = await ConversionsUnitsController.checkIfTheCapturedUnitIsHigherLAN(itemModel.baseUnitId, capturedUnitId);
                        if (responseUnitHegher.value == 1)
                            capturedUnitIsMajor = responseUnitHegher.salesUnitIsHigher;
                    }
                    else
                    {
                        capturedUnitIsMajor = ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(itemModel.baseUnitId, capturedUnitId);
                    }
                }
                if (modifyPricePermission)
                {
                    if (serverModeLAN)
                    {
                        dynamic responsePrice = await ItemsController.getAmountForAPriceLAN(itemModel.id,
                            customerModel.PRECIO_EMPRESA_ID, itemModel.imp1, itemModel.imp2, itemModel.imp3,
                            itemModel.imp1Excento, itemModel.imp2CuotaFija, itemModel.imp3CuotaFija, itemModel.cantidadFiscal,
                            itemModel.reten1, itemModel.reten2, codigoCaja);
                        if (responsePrice.value == 1)
                            price = responsePrice.price;
                    }
                    else
                    {
                        int listaDePrecio = ItemModel.getDefaultPriceFromACustomer(idCustomer);
                        price = ItemModel.getAmountForAPrice(itemModel.id, listaDePrecio);
                    }
                    if (capturedUnitIsMajor == 0)
                    {
                        if (serverModeLAN)
                        {
                            dynamic responseConversion = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.baseUnitId, capturedUnitId, true);
                            if (responseConversion.value == 1)
                                conversionFactor = responseConversion.majorFactor;
                        }
                        else
                        {
                            conversionFactor = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, true);
                        }
                        if (conversionFactor > 0)
                            price = price / conversionFactor;
                    }
                    else if (capturedUnitIsMajor == 1)
                    {
                        if (serverModeLAN)
                        {
                            dynamic responseConversion = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.baseUnitId, capturedUnitId, true);
                            if (responseConversion.value == 1)
                                conversionFactor = responseConversion.majorFactor;
                        }
                        else
                        {
                            conversionFactor = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, true);
                        }
                        if (conversionFactor > 0)
                            price = price * conversionFactor;
                    }
                }
                else
                {
                    if (serverModeLAN)
                    {
                        int listaDePrecio = 1;
                        if (customerModel != null)
                            listaDePrecio = customerModel.PRECIO_EMPRESA_ID;
                        else listaDePrecio = ItemModel.getDefaultPriceFromAnAgent(ClsRegeditController.getIdUserInTurn());
                        dynamic responsePrice = await ItemsController.getAmountForAPriceLAN(itemModel.id, listaDePrecio,
                            itemModel.imp1, itemModel.imp2, itemModel.imp3, itemModel.imp1Excento, itemModel.imp2CuotaFija,
                            itemModel.imp3CuotaFija, itemModel.cantidadFiscal, itemModel.reten1, itemModel.reten2, codigoCaja);
                        if (responsePrice.value == 1)
                            price = responsePrice.price;
                    }
                    else
                    {
                        int listaDePrecio = 1;
                        if (customerModel != null)
                            listaDePrecio = customerModel.PRECIO_EMPRESA_ID;
                        else listaDePrecio = ItemModel.getDefaultPriceFromAnAgent(ClsRegeditController.getIdUserInTurn());
                        price = ItemModel.getAmountForAPrice(itemModel.id, listaDePrecio);
                    }
                    if (capturedUnitIsMajor == 0)
                    {
                        if (serverModeLAN)
                        {
                            dynamic responseConversion = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.baseUnitId, capturedUnitId, true);
                            if (responseConversion.value == 1)
                                conversionFactor = responseConversion.majorFactor;
                        }
                        else
                        {
                            conversionFactor = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, true);
                        }
                        if (conversionFactor != 0)
                            price = price / conversionFactor;
                    }
                    else if (capturedUnitIsMajor == 1)
                    {
                        if (serverModeLAN)
                        {
                            dynamic responseConversion = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.baseUnitId, capturedUnitId, true);
                            if (responseConversion.value == 1)
                                conversionFactor = responseConversion.majorFactor;
                        }
                        else
                        {
                            conversionFactor = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, true);
                        }
                        if (conversionFactor != 0)
                            price = price * conversionFactor;
                    }
                }
            });
            return price;
        }

        private void fillComboUnitsMeasureAndWeight(List<ClsUnitsMeasureWeightModel> unitsList, int salesUnitId)
        {
            if (unitsList.Count <= 0)
            {
                comboBoxUnitMWITemVenta.Items.Clear();
                comboBoxUnitMWITemVenta.Items.Add("");
            }
            else
            {
                comboBoxUnitMWITemVenta.Items.Clear();
                int position = 0;
                int count = 0;
                foreach (ClsUnitsMeasureWeightModel unit in unitsList)
                {
                    comboBoxUnitMWITemVenta.Items.Add(" " + unit.name);
                    if (unit.idServer == salesUnitId)
                        position = count;
                    count++;
                }
                comboBoxUnitMWITemVenta.SelectedIndex = position;
            }
        }

        private void fillComboPricesFromTheItem(List<ClsPreciosEmpresaModel> pricesList)
        {
            comboPreciosItemVenta.Items.Clear();
            if (pricesList.Count <= 0)
            {
                comboPreciosItemVenta.Items.Add("");
            }
            else
            {
                foreach (ClsPreciosEmpresaModel price in pricesList)
                {
                    comboPreciosItemVenta.Items.Add(price.NOMBRE + " " + price.precioImporte.ToString("C", CultureInfo.CurrentCulture));
                }
                //validar la lista de precios
                int listaActual = 0;
                if (customerModel != null)
                {
                    var db = new SQLiteConnection();
                    try
                    {
                        ///aqui me quede 

                        String query = "SELECT " + LocalDatabase.CAMPO_LISTAPRECIO + " FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " +
                        LocalDatabase.CAMPO_ID_CLIENTE + " = " + customerModel.CLIENTE_ID;
                        db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                        db.Open();
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            using (SQLiteDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        if (reader.GetValue(0) != DBNull.Value)
                                            listaActual = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
                                    }
                                }
                                if (reader != null && !reader.IsClosed)
                                    reader.Close();
                            }
                        }
                    }
                    catch (SQLiteException Ex)
                    {
                        SECUDOC.writeLog(Ex.ToString());
                        listaActual = 0;
                    }
                    finally
                    {
                        if (db != null && db.State == ConnectionState.Open)
                            db.Close();
                    }
                }

                if (1 > pricesList.Count)
                {
                    FormMessage formMessage = new FormMessage("Lista de precio no encontrada", "El agente no tiene permitido la acción de la lista de precios", 3);
                    formMessage.ShowDialog();
                }
                else
                {
                    comboPreciosItemVenta.SelectedIndex = listaActual - 1;
                }
            }
        }

        private void showItemImage(int itemId)
        {
            String rutaImg = MetodosGenerales.rootDirectory + "\\Imagenes\\items\\" + itemId + "-1.jpg";
            if (File.Exists(rutaImg))
            {
                try
                {
                    FileStream fs = new FileStream(rutaImg, FileMode.Open, FileAccess.Read);
                    imgItem.Image = MetodosGenerales.redimencionarImagenes(Image.FromStream(fs), 162, 137);
                    fs.Close();
                }
                catch (Exception ex)
                {
                    SECUDOC.writeLog(ex.ToString());
                    rutaImg = MetodosGenerales.rootDirectory + "\\Imagenes\\Estaticas\\SyncTPV.png";
                    FileStream fs = new FileStream(rutaImg, FileMode.Open, FileAccess.Read);
                    imgItem.Image = MetodosGenerales.redimencionarImagenes(Image.FromStream(fs), 162, 137);
                    fs.Close();
                }
            } else
            {
                try
                {
                    rutaImg = MetodosGenerales.rootDirectory + "\\Imagenes\\Estaticas\\SyncTPV.png";
                    FileStream fs = new FileStream(rutaImg, FileMode.Open, FileAccess.Read);
                    imgItem.Image = MetodosGenerales.redimencionarImagenes(Image.FromStream(fs), 162, 137);
                    fs.Close();
                }
                catch (Exception ex)
                {
                    SECUDOC.writeLog(ex.ToString());
                }
            }
        }

        public async Task addOrEditMovementLocal(double capturedUnits, double finalPrice, String discountText,
            String observation, double nonConvertibleUnits)
        {
            if (editMove)
            {
                int dgvPosition = 0;
                if (permissionPrepedido)
                    dgvPosition = 0;
                else dgvPosition = dataGridMovements.CurrentRow.Index;
                if (!procesoPrincipal)
                {
                    procesoPrincipal = true;
                    int actualizar = 0;
                    if (btnAgregar.Text.Trim().Equals("Actualizar") && idMovement != 0)
                        actualizar = 1;
                    int positionMove = MovimientosModel.getPositionFromTheFirstItem(idMovement, idDocument);
                    if (documentType == 0)
                        documentType = DocumentModel.getDocumentType(idDocument);
                    AddOrSubtractUnitsController asuc = new AddOrSubtractUnitsController(idDocument, idMovement, positionMove, 1,
                        capturedUnits, 2, documentType);
                    //error cuando no tienen unidades de medida y peso validar
                    int capturedUnitId = 0;
                    if (umwList != null && umwList.Count > 0)
                        capturedUnitId = umwList[comboBoxUnitMWITemVenta.SelectedIndex].idServer;
                    dynamic respuesta = await asuc.doInBackgroundLocal(itemModel, 1, actualizar, nonConvertibleUnits, finalPrice, serverModeLAN,
                        permissionPrepedido, discountText, observation, capturedUnitId);
                    
                    if (respuesta.call == 0)
                    {
                        if (respuesta.value == 1)
                        {
                            MovimientosModel mm = MovimientosModel.getAMovementFromADocument(idDocument, idMovement);
                            if (mm != null)
                            {
                                await editNewColumnInDataGridMovements(mm, dgvPosition);
                            }
                            clearMovementsAddedData();
                            await clearMovementValues();
                            procesoPrincipal = false;
                        } else if (respuesta.value == -5)
                        {
                            FormMessage formMessage = new FormMessage("Descuento No Permitido", respuesta.description, 3);
                            formMessage.ShowDialog();
                            //resetearValores(0);
                            //fillDataGridMovements();
                            MovimientosModel mm = MovimientosModel.getAMovementFromADocument(idDocument, idMovement);
                            if (mm != null)
                            {
                                await editNewColumnInDataGridMovements(mm, dgvPosition);
                            }
                            procesoPrincipal = false;
                        } else if (respuesta.value == -6)
                        {
                            FormMessage formMessage = new FormMessage("Descuento No Permitido", respuesta.description, 3);
                            formMessage.ShowDialog();
                            //resetearValores(0);
                            //fillDataGridMovements();
                            MovimientosModel mm = MovimientosModel.getAMovementFromADocument(idDocument, idMovement);
                            if (mm != null)
                            {
                                await editNewColumnInDataGridMovements(mm, dgvPosition);
                            }
                            procesoPrincipal = false;
                        }
                        else
                        {
                            MovimientosModel mm = MovimientosModel.getAMovementFromADocument(idDocument, idMovement);
                            if (mm != null)
                            {
                                editNewColumnInDataGridMovements(mm, dgvPosition);
                            }
                            procesoPrincipal = false;
                        }
                    }
                    else
                    {
                        if (respuesta.value == 1)
                        {
                            //Toast.makeText(context, "Cantidad modificada correctamente!", Toast.LENGTH_SHORT).show();
                            //updateItemInformation(context, idItem, positionMove, positionRecycler);
                            //resetearValores(0);
                            //fillDataGridMovements();
                            MovimientosModel mm = MovimientosModel.getAMovementFromADocument(idDocument, idMovement);
                            if (mm != null)
                            {
                                await editNewColumnInDataGridMovements(mm, dgvPosition);
                            }
                            clearMovementsAddedData();
                            await clearMovementValues();
                            procesoPrincipal = false;
                        }
                        else if (respuesta.value == -1)
                        {
                            if (documentType == DocumentModel.TIPO_VENTA || documentType == DocumentModel.TIPO_REMISION)
                            {
                                FormMessage formMessage = new FormMessage("Existencia Insifuciente", "El producto no cuenta con la existencia suficiente!", 3);
                                formMessage.ShowDialog();
                            }
                            MovimientosModel mm = MovimientosModel.getAMovementFromADocument(idDocument, idMovement);
                            if (mm != null)
                            {
                                await editNewColumnInDataGridMovements(mm, dgvPosition);
                            }
                            clearMovementsAddedData();
                            await clearMovementValues();
                            procesoPrincipal = false;
                        }
                        else if (respuesta.value == -5)
                        {
                            FormMessage formMessage = new FormMessage("Descuento No Permitido", respuesta.description, 3);
                            formMessage.ShowDialog();
                            //resetearValores(0);
                            //fillDataGridMovements();
                            MovimientosModel mm = MovimientosModel.getAMovementFromADocument(idDocument, idMovement);
                            if (mm != null)
                            {
                                editNewColumnInDataGridMovements(mm, dgvPosition);
                            }
                            clearMovementsAddedData();
                            await clearMovementValues();
                            procesoPrincipal = false;
                        }
                        else if (respuesta.value == -6)
                        {
                            FormMessage formMessage = new FormMessage("Descuento No Permitido", respuesta.description, 3);
                            formMessage.ShowDialog();
                            //resetearValores(0);
                            //fillDataGridMovements();
                            MovimientosModel mm = MovimientosModel.getAMovementFromADocument(idDocument, idMovement);
                            if (mm != null)
                            {
                                editNewColumnInDataGridMovements(mm, dgvPosition);
                            }
                            clearMovementsAddedData();
                            await clearMovementValues();
                            procesoPrincipal = false;
                        }
                        else
                        {
                            MovimientosModel mm = MovimientosModel.getAMovementFromADocument(idDocument, idMovement);
                            if (mm != null)
                            {
                                await editNewColumnInDataGridMovements(mm, dgvPosition);
                            }
                            clearMovementsAddedData();
                            await clearMovementValues();
                            procesoPrincipal = false;
                        }
                    }
                    btnPausarDocumentoFrmVenta.Visible = true;
                    btnRecuperar.Visible = false;
                    editMove = false;

                }
            }
            else
            {
                int value = 0;
                String description = "";
                int idMovimientoAgregado = 0;
                int showWarningStock = 0;
                Cursor.Current = Cursors.WaitCursor;
                await Task.Run(async () =>
                {
                    if (!procesoPrincipal)
                    {
                        procesoPrincipal = true;
                        bool addItem = true;
                        int movimientosFiscalesYNoFiscales = 0;
                        if (documentType != DocumentModel.TIPO_COTIZACION_MOSTRADOR && documentType != DocumentModel.TIPO_COTIZACION &&
                            documentType != DocumentModel.TIPO_PEDIDO && documentType != DocumentModel.TIPO_DEVOLUCION)
                        {
                            bool useFiscalField = await ConfigurationsTpvController.checkIfUseFiscalValueActivated();
                            if (useFiscalField)
                            {
                                int addFiscalOrNotMovementCurrent = 1;
                                if (await ItemModel.getFiscalItemFieldValue(itemModel, positionFiscalItemField))
                                    addFiscalOrNotMovementCurrent = 0;
                                String fieldName = await ItemModel.getTableNameForFiscalItemField(positionFiscalItemField);
                                /*
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
                                 */
                                int movimientosFiscalesDiferente = 0;
                                if (idDocument > 0)
                                {
                                    movimientosFiscalesDiferente = MovimientosModel.getTotalNumberOfMovimientosFiscalesDiferentesAlQueIntentamosAgregar(
                                    idDocument, addFiscalOrNotMovementCurrent, fieldName, serverModeLAN);
                                }
                                if (movimientosFiscalesDiferente > 0)
                                    addItem = false;
                                else addItem = true;
                            }
                            else
                            {
                                addItem = true;
                            }
                        }
                        if (addItem)
                        {
                            if (permissionPrepedido)
                            {
                                List<MovimientosModel> mmList = MovimientosModel.getAllMovementsFromADocument(idDocument);
                                if (mmList != null && mmList.Count > 0)
                                {
                                    value = 3;
                                }
                                else
                                {
                                    AddMovementController amc = new AddMovementController(idDocument, capturedUnits, finalPrice,
                                        itemModel, discountText, itemModel.descuentoMaximo, documentType, observation,
                                    nonConvertibleUnits, serverModeLAN);
                                    dynamic response = await amc.doInBackgroundLocal(capturedUnitId, serverModeLAN, permissionPrepedido, webActive);
                                    try
                                    {
                                        discountText = discountText + MovimientosModel.getPorcentajePromotionMoviments(itemModel.id);
                                    }
                                    catch (Exception e)
                                    {
                                        SECUDOC.writeLog(e.ToString());
                                    }
                                    if (response.valor == 1)
                                    {
                                        value = 1;
                                        idDocument = response.idDocument;
                                        idMovimientoAgregado = response.idMovimiento;
                                        showWarningStock = response.noStock;
                                    }
                                    else if (response.valor == 2)
                                    {
                                        value = 2;
                                        idDocument = response.idDocument;
                                    }
                                    else if (response.valor == -1)
                                    {
                                        value = response.valor;
                                        description = "Error al agregar el movimiento a la base de datos";
                                        procesoPrincipal = false;
                                    }
                                    else if (response.valor == -2)
                                    {
                                        value = response.valor;
                                        description = "Ocurrió un error al actualizar las existencias";
                                        procesoPrincipal = false;
                                    }
                                    else if (response.valor == -3)
                                    {
                                        value = response.valor;
                                        description = "Ocurrio un error al agregar el articulo";
                                        procesoPrincipal = false;
                                    }
                                    else if (response.valor == -4)
                                    {
                                        value = response.valor;
                                        description = "Las existencias no son suficientes";
                                        procesoPrincipal = false;
                                    }
                                    else if (response.valor == -5)
                                    {
                                        value = response.valor;
                                        description = "El descuento maximo es de " + response.descMax + " %";
                                        procesoPrincipal = false;
                                    }
                                    else if (response.valor == -6)
                                    {
                                        value = response.valor;
                                        description = "El anexo 20 no permite descuentos mayores al 100% en comprobantes fiscales";
                                        procesoPrincipal = false;
                                    }
                                }
                            }
                            else
                            {
                                AddMovementController amc = new AddMovementController(idDocument, capturedUnits, finalPrice, itemModel, discountText,
                                itemModel.descuentoMaximo, documentType, observation,
                                nonConvertibleUnits, serverModeLAN);
                                dynamic response = await amc.doInBackgroundLocal(capturedUnitId, serverModeLAN, permissionPrepedido, webActive);
                                
                                if (response.valor == 1)
                                {
                                    value = 1;
                                    idDocument = response.idDocument;
                                    idMovimientoAgregado = response.idMovimiento;
                                    showWarningStock = response.noStock;
                                }
                                else if (response.valor == 2)
                                {
                                    value = 2;
                                    idDocument = response.idDocument;
                                }
                                else if (response.valor == -1)
                                {
                                    value = response.valor;
                                    description = "Error al agregar el movimiento a la base de datos";
                                    procesoPrincipal = false;
                                }
                                else if (response.valor == -2)
                                {
                                    value = response.valor;
                                    description = "Ocurrió un error al actualizar las existencias";
                                    procesoPrincipal = false;
                                }
                                else if (response.valor == -3)
                                {
                                    value = response.valor;
                                    description = "Ocurrio un error al agregar el articulo";
                                    procesoPrincipal = false;
                                }
                                else if (response.valor == -4)
                                {
                                    value = response.valor;
                                    description = "Las existencias no son suficientes";
                                    procesoPrincipal = false;
                                }
                                else if (response.valor == -5)
                                {
                                    value = response.valor;
                                    description = "El descuento maximo es de " + response.descMax + " %";
                                    procesoPrincipal = false;
                                }
                                else if (response.valor == -6)
                                {
                                    value = response.valor;
                                    description = "El anexo 20 no permite descuentos mayores al 100% en comprobantes fiscales";
                                    procesoPrincipal = false;
                                }
                            }
                        }
                        else
                        {
                            if (movimientosFiscalesYNoFiscales == 0)
                            {
                                description = "Solo puedes agregar productos o servicios Comerciales";
                                procesoPrincipal = false;
                            }
                            else
                            {
                                description = "Solo puedes agregar productos o servicios de Control";
                                procesoPrincipal = false;
                            }
                        }
                    }
                });
                Cursor.Current = Cursors.Default;
                editMove = false;
                if (value == 1) {
                    if (permissionPrepedido)
                    {
                        if (showWarningStock == 1)
                        {
                            FormMessage fm = new FormMessage("Existencia Insuficiente", "Excediste la existencia del producto!", 2);
                            fm.ShowDialog();
                            if (idMovimientoAgregado > 0)
                            {
                                MovimientosModel mm = MovimientosModel.getMovement("SELECT * FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " +
                                LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " + idDocument + " AND " +
                                LocalDatabase.CAMPO_ID_MOV + " = " + idMovimientoAgregado);
                                if (mm != null)
                                {
                                    await addNewColumnInDataGridMovements(mm);
                                }
                            }
                        }
                        else
                        {
                            MovimientosModel mm = MovimientosModel.getMovement("SELECT * FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " +
                                LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " + idDocument + " AND " +
                                LocalDatabase.CAMPO_ID_MOV + " = " + idMovimientoAgregado);
                            if (mm != null)
                            {
                                await addNewColumnInDataGridMovements(mm);
                            }
                        }
                        await clearMovementsAddedData();
                        btnPausarDocumentoFrmVenta.Visible = true;
                        btnRecuperar.Visible = false;
                        procesoPrincipal = false;
                    } else
                    {
                        if (showWarningStock == 1)
                        {
                            FormMessage fm = new FormMessage("Existencia Insuficiente", "Excediste la existencia del producto!", 2);
                            fm.ShowDialog();
                            if (idMovimientoAgregado > 0)
                            {
                                MovimientosModel mm = MovimientosModel.getMovement("SELECT * FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " +
                                LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " + idDocument + " AND " +
                                LocalDatabase.CAMPO_ID_MOV + " = " + idMovimientoAgregado);
                                if (mm != null)
                                {
                                    await addNewColumnInDataGridMovements(mm);
                                }
                            }
                        }
                        else
                        {
                            MovimientosModel mm = MovimientosModel.getMovement("SELECT * FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " +
                                LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " + idDocument + " AND " +
                                LocalDatabase.CAMPO_ID_MOV + " = " + idMovimientoAgregado);
                            if (mm != null)
                            {

                                await addNewColumnInDataGridMovements(mm);
                            }
                        }
                        //resetearValores(0);
                        //fillDataGridMovements();
                        await clearMovementsAddedData();
                        btnPausarDocumentoFrmVenta.Visible = true;
                        btnRecuperar.Visible = false;
                        procesoPrincipal = false;
                    }
                } else if (value == 2)
                {
                    if (permissionPrepedido)
                    {
                        int actualizar = 0;
                        if (btnAgregar.Text.Trim().Equals("Actualizar") && idMovement != 0)
                            actualizar = 1;
                        int positionMove = MovimientosModel.getPositionFromTheFirstItem(idMovement, idDocument);
                        AddOrSubtractUnitsController asuc = new AddOrSubtractUnitsController(idDocument, idMovement, positionMove, 1,
                            capturedUnits, 2, documentType);
                        int capturedUnitId = 0;
                        if (umwList != null && umwList.Count > 0)
                            capturedUnitId = umwList[comboBoxUnitMWITemVenta.SelectedIndex].idServer;
                        dynamic respuesta = await asuc.doInBackgroundLocal(itemModel, 1, actualizar, nonConvertibleUnits, finalPrice,
                            serverModeLAN, permissionPrepedido, discountText, observation, capturedUnitId);
                        try
                        {
                            discountText = discountText + respuesta.porcentajepromocion;
                        }
                        catch (Exception e)
                        {
                            SECUDOC.writeLog(e.ToString());
                        }
                        if (respuesta.call == 0)
                        {
                            if (respuesta.value == 1)
                            {
                                int dgvPosition = dataGridMovements.CurrentRow.Index;
                                MovimientosModel mm = MovimientosModel.getAMovementFromADocument(idDocument, idMovement);
                                if (mm != null)
                                {
                                    await editNewColumnInDataGridMovements(mm, dgvPosition);
                                }
                                await clearMovementsAddedData();
                            }
                            else
                            {
                                int dgvPosition = dataGridMovements.CurrentRow.Index;
                                MovimientosModel mm = MovimientosModel.getAMovementFromADocument(idDocument, idMovement);
                                if (mm != null)
                                {
                                    await editNewColumnInDataGridMovements(mm, dgvPosition);
                                }
                            }
                        }
                        else
                        {
                            //idDocument = response.idDocument;
                            if (respuesta.value == 1)
                            {
                                int dgvPosition = dataGridMovements.CurrentRow.Index;
                                MovimientosModel mm = MovimientosModel.getAMovementFromADocument(idDocument, idMovement);
                                if (mm != null)
                                {
                                    await editNewColumnInDataGridMovements(mm, dgvPosition);
                                }
                                await clearMovementsAddedData();
                            }
                            else if (respuesta.value == -1)
                            {
                                //Toast.makeText(context, "Oops existencia insufiente!", Toast.LENGTH_SHORT).show();
                                //updateItemInformation(context, idItem, positionMove, positionRecycler);
                                //resetearValores(0);
                                //fillDataGridMovements();
                                int dgvPosition = dataGridMovements.CurrentRow.Index;
                                MovimientosModel mm = MovimientosModel.getAMovementFromADocument(idDocument, idMovement);
                                if (mm != null)
                                {
                                    await editNewColumnInDataGridMovements(mm, dgvPosition);
                                }
                            }
                            else
                            {
                                //Toast.makeText(context, "Oops, algo falló al cambiar la existencia!", Toast.LENGTH_SHORT).show();
                                //updateItemInformation(context, idItem, positionMove, positionRecycler);
                                //resetearValores(0);
                                //fillDataGridMovements();
                                int dgvPosition = dataGridMovements.CurrentRow.Index;
                                MovimientosModel mm = MovimientosModel.getAMovementFromADocument(idDocument, idMovement);
                                if (mm != null)
                                {
                                    await editNewColumnInDataGridMovements(mm, dgvPosition);
                                }
                            }
                        }
                        btnPausarDocumentoFrmVenta.Visible = true;
                        btnRecuperar.Visible = false;
                    } else
                    {
                        int actualizar = 0;
                        if (btnAgregar.Text.Trim().Equals("Actualizar") && idMovement != 0)
                            actualizar = 1;
                        int positionMove = MovimientosModel.getPositionFromTheFirstItem(idMovement, idDocument);
                        AddOrSubtractUnitsController asuc = new AddOrSubtractUnitsController(idDocument, idMovement, positionMove, 1,
                            capturedUnits, 2, documentType);
                        int capturedUnitId = 0;
                        if (umwList != null && umwList.Count > 0)
                            capturedUnitId = umwList[comboBoxUnitMWITemVenta.SelectedIndex].idServer;
                        dynamic respuesta = await asuc.doInBackgroundLocal(itemModel, 1, actualizar, nonConvertibleUnits, finalPrice,
                            serverModeLAN, permissionPrepedido, discountText, observation, capturedUnitId);
                        try
                        {
                            discountText = discountText + MovimientosModel.getPorcentajePromotionMoviments(itemModel.id);
                        }
                        catch (Exception e)
                        {
                            SECUDOC.writeLog(e.ToString());
                        }
                        if (respuesta.call == 0)
                        {
                            if (respuesta.value == 1)
                            {
                                int dgvPosition = dataGridMovements.CurrentRow.Index;
                                MovimientosModel mm = MovimientosModel.getAMovementFromADocument(idDocument, idMovement);
                                if (mm != null)
                                {
                                    await editNewColumnInDataGridMovements(mm, dgvPosition);
                                }
                                await clearMovementsAddedData();
                            }
                            else
                            {
                                int dgvPosition = dataGridMovements.CurrentRow.Index;
                                MovimientosModel mm = MovimientosModel.getAMovementFromADocument(idDocument, idMovement);
                                if (mm != null)
                                {
                                    await editNewColumnInDataGridMovements(mm, dgvPosition);
                                }
                            }
                        }
                        else
                        {
                            //idDocument = response.idDocument;
                            if (respuesta.value == 1)
                            {
                                //resetearValores(0);
                                //fillDataGridMovements();
                                int dgvPosition = dataGridMovements.CurrentRow.Index;
                                MovimientosModel mm = MovimientosModel.getAMovementFromADocument(idDocument, idMovement);
                                if (mm != null)
                                {
                                    await editNewColumnInDataGridMovements(mm, dgvPosition);
                                }
                                await clearMovementsAddedData();
                            }
                            else if (respuesta.value == -1)
                            {
                                //Toast.makeText(context, "Oops existencia insufiente!", Toast.LENGTH_SHORT).show();
                                //updateItemInformation(context, idItem, positionMove, positionRecycler);
                                //resetearValores(0);
                                //fillDataGridMovements();
                                int dgvPosition = dataGridMovements.CurrentRow.Index;
                                MovimientosModel mm = MovimientosModel.getAMovementFromADocument(idDocument, idMovement);
                                if (mm != null)
                                {
                                    await editNewColumnInDataGridMovements(mm, dgvPosition);
                                }
                            }
                            else
                            {
                                //Toast.makeText(context, "Oops, algo falló al cambiar la existencia!", Toast.LENGTH_SHORT).show();
                                //updateItemInformation(context, idItem, positionMove, positionRecycler);
                                //resetearValores(0);
                                //fillDataGridMovements();
                                int dgvPosition = dataGridMovements.CurrentRow.Index;
                                MovimientosModel mm = MovimientosModel.getAMovementFromADocument(idDocument, idMovement);
                                if (mm != null)
                                {
                                    await editNewColumnInDataGridMovements(mm, dgvPosition);
                                }
                            }
                        }
                        btnPausarDocumentoFrmVenta.Visible = true;
                        btnRecuperar.Visible = false;
                    }
                } else if (value == 3)
                {
                    if (permissionPrepedido)
                    {
                        FormMessage formMessage = new FormMessage("Movimientos Unicos",
                                        "Solo puedes agregar un movimiento a los documentos de tipo " +
                                        "Prepedido", 3);
                        formMessage.ShowDialog();
                        clearMovementsAddedData();
                        btnPausarDocumentoFrmVenta.Visible = true;
                        btnRecuperar.Visible = false;
                        procesoPrincipal = false;
                    } else
                    {

                    }
                }
                else
                {
                    FormMessage formMessage = new FormMessage("Agregando Movimiento", description, 3);
                    formMessage.ShowDialog();
                }
                if (idDocument != 0)
                {
                    btnCerrar.Text = "Cancelar";
                    btnCerrar.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.close, 45, 45);
                } else
                {
                    btnCerrar.Text = "Cerrar";
                    btnCerrar.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.back_white, 45, 45);
                }
            }
        }

        private async Task clearMovementsAddedData()
        {
            try
            {
                String rutaImg = MetodosGenerales.rootDirectory + "\\Imagenes\\Estaticas\\SyncTPV.png";
                FileStream fs = new FileStream(rutaImg, FileMode.Open, FileAccess.Read);
                imgItem.Image = MetodosGenerales.redimencionarImagenes(Image.FromStream(fs), 162, 137);
                fs.Close();
            } catch (Exception e)
            {

            } finally
            {
                if (idCustomer != 0)
                {
                    double descuentoCliente = 0;
                    if (serverModeLAN)
                    {
                        dynamic responseDesc = await CustomersController.getDescuentoClienteLAN(idCustomer);
                        if (responseDesc.value == 1)
                            descuentoCliente = responseDesc.discount;
                    }
                    else
                    {
                        descuentoCliente = CustomerModel.getDescuentoDelCliente(idCustomer);
                    }
                    editDiscountItemVenta.Text = descuentoCliente + "";
                }
                else editDiscountItemVenta.Text = 0 + "";
                editPriceItemVenta.Text = "";
                editNombreItemVenta.Text = "";
                comboPreciosItemVenta.Items.Clear();
                comboBoxUnitMWITemVenta.Items.Clear();
                comboCodigoItemVenta.Text = "";
                itemModel = null;
                editUnidadNoConvertible.Text = "0";
                editUnidadNoConvertible.Visible = false;
                textUnidadNoConvertible.Visible = false;
                textUnidadDeMedida.Visible = false;
                comboBoxUnitMWITemVenta.Visible = false;
                panelObservationMovement.Visible = false;
                editUnidadNoConvertible.Text = "0";
                editUnidadNoConvertible.Visible = false;
                textUnidadNoConvertible.Visible = false;
                textExistenciaReal.Text = "";
                btnOpenScale.Visible = false;
            }
        }

        private void FrmVentaNew_FormClosed(object sender, FormClosedEventArgs e)
        {
            idDocument = 0;
            idCustomer = 0;
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            //validateCancelDocument();
            this.Close();
        }

        private async Task processToDeleteDocumentDirectly()
        {
            DeleteDocumentController ddc = new DeleteDocumentController();
            bool deleted = await ddc.doInBackground(idDocument, documentType, serverModeLAN, permissionPrepedido);
            resetearTodosLosValores(true);
            this.Close();
        }

        private async Task validateCancelDocument(FormClosingEventArgs e)
        {
            if (idDocument != 0) {
                if (permissionPrepedido) {
                    String query = "SELECT COUNT(*) FROM Documentos D " +
                        "INNER JOIN PedidoEncabezado P ON D.CIDDOCTOPEDIDOCC = P.CIDDOCTOPEDIDOCC " +
                        "INNER JOIN Movimientos M ON D.id = M.DOCTO_ID_PEDIDO " +
                        "INNER JOIN Weight W ON M.id = W.movementId " +
                        "WHERE D." + LocalDatabase.CAMPO_ID_DOC + " = " + idDocument + " AND D.pausa = 1 AND P.type = " +
                        PedidosEncabezadoModel.TYPE_PREPEDIDOS + " AND P.surtido = 1 AND P.listo = 1 AND " +
                        "W." + LocalDatabase.CAMPO_PESOCAJA_PESO + " = " + 0 + " AND W." + LocalDatabase.CAMPO_PESONETO_PESO + " != 0";
                    int documentoPendienteDeDescatarar = DocumentModel.getIntValue(query);
                    if (documentoPendienteDeDescatarar > 0)
                    {
                        FormMessage fm = new FormMessage("Documento Pendite de Destarar", "El documento no puede ser eliminado por que ya se envió al cliente", 2);
                        fm.ShowDialog();
                    } else {
                        FormPasswordConfirmation fpc = new FormPasswordConfirmation("Autorización del Supervisor", "Desea eliminar los pesos ya agregados de la entrega actual?");
                        fpc.ShowDialog();
                        if (FormPasswordConfirmation.permissionGranted)
                        {
                            DeleteDocumentController ddc = new DeleteDocumentController();
                            bool deleted = await ddc.doInBackground(idDocument, documentType, serverModeLAN, permissionPrepedido);
                            idDocument = 0;
                            activateOpcionFacturar = 1;
                            queryType = 0;
                            resetearTodosLosValores(true);
                            resetearValores(0);
                            clearMovementsAddedData();
                            imgSinDatosFrmVenta.Visible = true;
                            imgSinDatosFrmVenta.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.sindatos, 200, 200);
                            await updateTotalNumberOfMovements();
                            actualizarSubDescYTotalVenta(idDocument);
                            activarODesactivarElementosAlSurtirPrepedidos(true);
                            btnPausarDocumentoFrmVenta.Visible = false;
                            btnRecuperar.Visible = true;
                            btnSurtirPedidos.Visible = true;
                        } else
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                }
                else
                {
                    FormPasswordConfirmation fpc = new FormPasswordConfirmation("Autorización del Supervisor", "Desea eliminar el documento actual?");
                    fpc.ShowDialog();
                    if (FormPasswordConfirmation.permissionGranted)
                    {
                        DeleteDocumentController ddc = new DeleteDocumentController();
                        bool deleted = await ddc.doInBackground(idDocument, documentType, serverModeLAN, permissionPrepedido);
                        idDocument = 0;
                        activateOpcionFacturar = 1;
                        queryType = 0;
                        imgSinDatosFrmVenta.Visible = true;
                        imgSinDatosFrmVenta.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.sindatos, 200, 200);
                        await updateTotalNumberOfMovements();
                        actualizarSubDescYTotalVenta(idDocument);
                        activarODesactivarElementosAlSurtirPrepedidos(true);
                        btnPausarDocumentoFrmVenta.Visible = false;
                        btnRecuperar.Visible = true;
                        btnSurtirPedidos.Visible = true;
                        e.Cancel = true;
                        clearMovementsAddedData();
                        resetearTodosLosValores(true);
                        resetearValores(0);
                    } else
                    {
                        e.Cancel = true;
                    }
                    if (idDocument != 0)
                    {
                        btnCerrar.Text = "Cancelar";
                        btnCerrar.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.close, 45, 45);
                    } else
                    {
                        btnCerrar.Text = "Cerrar";
                        btnCerrar.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.back_white, 45, 45);
                    }
                }
            } else {
                resetearTodosLosValores(true);
                this.Close();
            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void BtnBuscarArticuloTeclado_Click(object sender, EventArgs e)
        {
            double capturedUnits = 1;
            String itemName = editNombreItemVenta.Text.ToString().Trim();
            if (!itemName.Equals(""))
            {
                String[] parts = itemName.Split('*');
                if (parts != null && parts.Length > 1)
                {
                    itemName = parts[1].Trim();
                    double value = 0;
                    bool result = double.TryParse(parts[0].Trim(), out value);
                    if (result)
                        capturedUnits = value;
                }
            }
            if (editMove)
            {
                clearMovementsAddedData();
                clearMovementValues();
            }
            FormArticulos fa = new FormArticulos("", 1, cotmosActive);
            fa.ShowDialog();
            if (itemModel != null)
            {
                editCapturedUnits.Select();
                await fillAllFieldsFromAMovement(itemModel, capturedUnits, false);
            }
        }

        private void btnBuscarClientesNew_Click_1(object sender, EventArgs e)
        {
            FormBuscaCliente buscaCliente = new FormBuscaCliente(serverModeLAN, permissionPrepedido);
            buscaCliente.ShowDialog();
            fillCustomerInformation(idCustomer);
            downloadImageCustomer(idCustomer);
        }

        private async Task fillCustomerInformation(int idCustomer)
        {
            if (idCustomer != 0)
            {
                if (serverModeLAN)
                {
                    if (idCustomer > 0)
                    {
                        dynamic responseCustomer = await CustomersController.getACustomerLAN(idCustomer);
                        if (responseCustomer.value == 1)
                            customerModel = responseCustomer.customerModel;
                        if (customerModel != null)
                        {
                            if (idDocument != 0)
                            {
                                if (DocumentModel.updateCustomer(idDocument, customerModel.CLIENTE_ID, customerModel.CLAVE))
                                    editNombreCliente.Text = customerModel.NOMBRE;
                                editDiscountItemVenta.Text = customerModel.descuentoMovimiento + "";
                            }
                            else
                            {
                                editNombreCliente.Text = customerModel.NOMBRE;
                                editDiscountItemVenta.Text = customerModel.descuentoMovimiento + "";
                            }
                            await getAndFillCustomerInformation(customerModel.CLIENTE_ID);
                        }
                        else
                        {
                            if (idCustomer < 0)
                            {
                                CustomerADCModel customerADC = CustomerADCModel.getAllDataForACustomerADCNotSent(Math.Abs(idCustomer));
                                customerModel = new ClsClienteModel();
                                customerModel.CLIENTE_ID = customerADC.id;
                                await getAndFillCustomerInformation(customerModel.CLIENTE_ID);
                                customerModel.NOMBRE = customerADC.nombre;
                                customerModel.PRECIO_EMPRESA_ID = 1;
                                if (customerModel.NOMBRE.Equals(""))
                                    editNombreCliente.Text = "Cliente Nuevo";
                                else editNombreCliente.Text = customerModel.NOMBRE;
                                editDiscountItemVenta.Text = customerModel.descuentoMovimiento + "";
                            }
                            else editNombreCliente.Text = "Cliente No Encontrado";
                        }
                    } else
                    {
                        CustomerADCModel customerADC = CustomerADCModel.getAllDataForACustomerADCNotSent(Math.Abs(idCustomer));
                        if (customerADC != null)
                        {
                            customerModel = new ClsClienteModel();
                            customerModel.CLIENTE_ID = Convert.ToInt32("-" + customerADC.id);
                            await getAndFillCustomerInformation(customerModel.CLIENTE_ID);
                            customerModel.NOMBRE = customerADC.nombre;
                            customerModel.PRECIO_EMPRESA_ID = 1;
                            if (customerModel.NOMBRE.Equals(""))
                                editNombreCliente.Text = "Cliente Nuevo";
                            else editNombreCliente.Text = customerModel.NOMBRE;
                            editDiscountItemVenta.Text = customerModel.descuentoMovimiento + "";
                            if (idDocument != 0)
                            {
                                if (DocumentModel.updateCustomer(idDocument, customerModel.CLIENTE_ID, customerModel.CLAVE))
                                    editNombreCliente.Text = customerModel.NOMBRE;
                                editDiscountItemVenta.Text = customerModel.descuentoMovimiento + "";
                            }
                            else
                            {
                                editNombreCliente.Text = customerModel.NOMBRE;
                                editDiscountItemVenta.Text = customerModel.descuentoMovimiento + "";
                            }
                        } else
                        {
                            CustomerADCModel customerSended = CustomerADCModel.getAdditionalCustomerById(Math.Abs(idCustomer));
                            if (customerSended != null)
                            {
                                int idPanel = CustomerADCModel.getNewClientIdPanel(Math.Abs(idCustomer));
                                String customerCode = ClsClienteModel.getCodeForAditionalCustomer(panelInstance, idPanel);
                                int idClienteSubido = ClsCustomersModel.getIdByCode(comInstance, customerCode);
                                customerModel = new ClsClienteModel();
                                customerModel.CLIENTE_ID = idClienteSubido;
                                await getAndFillCustomerInformation(customerModel.CLIENTE_ID);
                                customerModel.NOMBRE = customerSended.nombre;
                                customerModel.PRECIO_EMPRESA_ID = 1;
                                if (customerModel.NOMBRE.Equals(""))
                                    editNombreCliente.Text = "Cliente Nuevo";
                                else editNombreCliente.Text = customerModel.NOMBRE;
                                editDiscountItemVenta.Text = customerModel.descuentoMovimiento + "";
                                if (idDocument != 0)
                                {
                                    if (DocumentModel.updateCustomer(idDocument, customerModel.CLIENTE_ID, customerModel.CLAVE))
                                        editNombreCliente.Text = customerModel.NOMBRE;
                                    editDiscountItemVenta.Text = customerModel.descuentoMovimiento + "";
                                }
                                else
                                {
                                    editNombreCliente.Text = customerModel.NOMBRE;
                                    editDiscountItemVenta.Text = customerModel.descuentoMovimiento + "";
                                }
                            } else
                            {
                                FormMessage formMessage = new FormMessage("Cliente No Encontrado", "El cliente seleccionado ya fue subido al sistema de comercial, " +
                               "necesitas ir a Clientes y ver su perfil o detalles para que la información se actualice y ya puedas venderle", 3);
                                formMessage.ShowDialog();
                            }
                        }
                    }
                } else
                {
                    if (idCustomer > 0)
                    {
                        if (webActive)
                        {
                            dynamic responseCustomer = await CustomersController.getACustomerAPI(idCustomer);
                            if (responseCustomer.value == 1)
                            {
                                customerModel = responseCustomer.customerModel;
                                if (customerModel != null)
                                {
                                    if (idDocument != 0)
                                    {
                                        if (DocumentModel.updateCustomer(idDocument, customerModel.CLIENTE_ID, customerModel.CLAVE))
                                            editNombreCliente.Text = customerModel.NOMBRE;
                                        editDiscountItemVenta.Text = customerModel.descuentoMovimiento + "";
                                    }
                                    else
                                    {
                                        editNombreCliente.Text = customerModel.NOMBRE;
                                        editDiscountItemVenta.Text = customerModel.descuentoMovimiento + "";
                                    }
                                    await getAndFillCustomerInformation(customerModel.CLIENTE_ID);
                                }
                            }
                            else
                            {
                                FormMessage formMessage = new FormMessage("Cliente", responseCustomer.description, 3);
                                formMessage.ShowDialog();
                            }
                        } else
                        {
                            customerModel = CustomerModel.getAllDataFromACustomer(idCustomer);
                            if (customerModel != null)
                            {
                                if (idDocument != 0)
                                {
                                    if (DocumentModel.updateCustomer(idDocument, customerModel.CLIENTE_ID, customerModel.CLAVE))
                                        editNombreCliente.Text = customerModel.NOMBRE;
                                    editDiscountItemVenta.Text = customerModel.descuentoMovimiento + "";
                                }
                                else
                                {
                                    editNombreCliente.Text = customerModel.NOMBRE;
                                    editDiscountItemVenta.Text = customerModel.descuentoMovimiento + "";
                                }
                                await getAndFillCustomerInformation(customerModel.CLIENTE_ID);
                            } else
                            {
                                FormMessage formMessage = new FormMessage("Cliente", "Cliente no encontrado, tal vez " +
                                    "tengas que actualizar la información de los mismos", 3);
                                formMessage.ShowDialog();
                            }
                        }
                    } else
                    {
                        CustomerADCModel customerADC = CustomerADCModel.getAllDataForACustomerADCNotSent(Math.Abs(idCustomer));
                        if (customerADC != null)
                        {
                            customerModel = new ClsClienteModel();
                            customerModel.CLIENTE_ID = Convert.ToInt32("-" + customerADC.id);
                            await getAndFillCustomerInformation(customerModel.CLIENTE_ID);
                            customerModel.NOMBRE = customerADC.nombre;
                            customerModel.PRECIO_EMPRESA_ID = 1;
                            if (customerModel.NOMBRE.Equals(""))
                                editNombreCliente.Text = "Cliente Nuevo";
                            else editNombreCliente.Text = customerModel.NOMBRE;
                            editDiscountItemVenta.Text = customerModel.descuentoMovimiento + "";
                            if (idDocument != 0)
                            {
                                if (DocumentModel.updateCustomer(idDocument, customerModel.CLIENTE_ID, customerModel.CLAVE))
                                    editNombreCliente.Text = customerModel.NOMBRE;
                                editDiscountItemVenta.Text = customerModel.descuentoMovimiento + "";
                            }
                            else
                            {
                                editNombreCliente.Text = customerModel.NOMBRE;
                                editDiscountItemVenta.Text = customerModel.descuentoMovimiento + "";
                            }
                        }
                        else
                        {
                            CustomerADCModel customerSended = CustomerADCModel.getAdditionalCustomerById(Math.Abs(idCustomer));
                            if (customerSended != null)
                            {
                                int idPanel = CustomerADCModel.getNewClientIdPanel(Math.Abs(idCustomer));
                                String customerCode = ClsClienteModel.getCodeForAditionalCustomer(panelInstance, idPanel);
                                int idClienteSubido = ClsCustomersModel.getIdByCode(comInstance, customerCode);
                                customerModel = new ClsClienteModel();
                                customerModel.CLIENTE_ID = idClienteSubido;
                                await getAndFillCustomerInformation(customerModel.CLIENTE_ID);
                                customerModel.NOMBRE = customerSended.nombre;
                                customerModel.PRECIO_EMPRESA_ID = 1;
                                if (customerModel.NOMBRE.Equals(""))
                                    editNombreCliente.Text = "Cliente Nuevo";
                                else editNombreCliente.Text = customerModel.NOMBRE;
                                editDiscountItemVenta.Text = customerModel.descuentoMovimiento + "";
                                if (idDocument != 0)
                                {
                                    if (DocumentModel.updateCustomer(idDocument, customerModel.CLIENTE_ID, customerModel.CLAVE))
                                        editNombreCliente.Text = customerModel.NOMBRE;
                                    editDiscountItemVenta.Text = customerModel.descuentoMovimiento + "";
                                }
                                else
                                {
                                    editNombreCliente.Text = customerModel.NOMBRE;
                                    editDiscountItemVenta.Text = customerModel.descuentoMovimiento + "";
                                }
                            }
                            else
                            {
                                FormMessage formMessage = new FormMessage("Cliente No Encontrado", "El cliente seleccionado ya fue subido al sistema de comercial, " +
                               "necesitas ir a Clientes y ver su perfil o detalles para que la información se actualice y ya puedas venderle", 3);
                                formMessage.ShowDialog();
                            }
                        }
                    }
                }
            }
        }

        private async void btnAbrir_Click(object sender, EventArgs e)
        {
            if (idDocument != 0)
            {
                FormMessage fm = new FormMessage("Documento En Turno", "Existe un documento en la pantalla", 2);
                fm.ShowDialog();
            }
            else
            {
                FrmRecuperarDocumento frd = new FrmRecuperarDocumento(idCustomer);
                frd.ShowDialog();
                renderizarDatosRecuperadorDelDocumentoProcess();
            }
        }

        public async Task renderizarDatosRecuperadorDelDocumentoProcess()
        {
            Cursor.Current = Cursors.WaitCursor;
            resetearValores(0);
            fillDataGridMovements();
            documentType = DocumentModel.getDocumentType(idDocument);
            if (!permissionPrepedido)
                fillComboDocumentType();
            if (idDocument != 0)
            {
                int idCustomer = DocumentModel.getCustomerIdOfADocument(idDocument);
                await getAndFillCustomerInformation(idCustomer);
                btnRecuperar.Visible = false;
                btnPausarDocumentoFrmVenta.Visible = true;
                btnSurtirPedidos.Visible = false;
                if (permissionPrepedido)
                {
                    if (DocumentModel.isItDocumentFromAPrepedido(idDocument))
                    {
                        String query = "SELECT * FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " + LocalDatabase.CAMPO_DOCUMENTOID_MOV +
                        " = " + idDocument;
                        List<MovimientosModel> movesList = MovimientosModel.getAllMovements(query);
                        if (movesList != null)
                        {
                            query = "SELECT " + LocalDatabase.CAMPO_PESOCAJA_PESO + " FROM " + LocalDatabase.TABLA_PESO + " WHERE " +
                                LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = " + movesList[0].id;
                            double destarado = WeightModel.getDoubleValue(query);
                            if (destarado == 0)
                            {
                                btnCobrarFrmVenta.Text = "Entregar a Cliente (F10)";
                                btnCobrarFrmVenta.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.entregar_back, 35, 35);
                            }
                            else
                            {
                                btnCobrarFrmVenta.Text = "Guardar (F10)";
                                btnCobrarFrmVenta.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.send, 35, 35);
                            }
                        }
                        btnBuscarClientesNew.Enabled = false;
                    }
                    else
                    {
                        btnCobrarFrmVenta.Text = "Generar Prepedido (F10)";
                        btnCobrarFrmVenta.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.send, 35, 35);
                    }
                }
                btnCerrar.Text = "Cancelar";
                btnCerrar.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.close, 45, 45);
                comboCodigoItemVenta.Select();
            }
            else
            {
                btnCerrar.Text = "Cerrar";
                btnCerrar.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.back_white, 45, 45);
            }
            Cursor.Current = Cursors.Default;
        }

        private void comboBoxUnitMWITemVenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxSelectedIndexChangedProcess();
        }

        private async Task comboBoxSelectedIndexChangedProcess()
        {
            if (!obtenerPrecios)
            {
                obtenerPrecios = true;
                if (umwList != null && umwList.Count > 0)
                    capturedUnitId = umwList[comboBoxUnitMWITemVenta.SelectedIndex].idServer;
                await loadInitialData(1, null, false);
                obtenerPrecios = false;
            }
            if (comboBoxUnitMWITemVenta.Focused)
            {
                PopupNotifier popup = new PopupNotifier();
                popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                popup.TitleColor = Color.FromArgb(43, 143, 192);
                popup.TitleText = "Precio Actualizado";
                popup.TitlePadding = new Padding(5, 5, 5, 5);
                popup.ButtonBorderColor = Color.Red;
                popup.ContentText = "El precio asignado al cliente fue actualizado!";
                popup.ContentColor = Color.FromArgb(43, 143, 192);
                popup.HeaderHeight = 10;
                popup.AnimationDuration = 1000;
                popup.HeaderColor = Color.FromArgb(200, 244, 255);
                popup.Popup();
            }
        }

        private void txtBuscarProductoTeclado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                BtnBuscarArticuloTeclado_Click(sender, e);
            }
        }

        private async void editUnidadNoConvertible_KeyPress(object sender, KeyPressEventArgs e)
        {
            char signo_decimal = (char)46;
            //Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == signo_decimal)
            {
                e.Handled = false;
            } else if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
            {
                e.Handled = false;
            } else
            {
                //el resto de teclas pulsadas se desactivan
                e.Handled = true;
            }
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                double capturedUnits = 1;
                double value = 0;
                bool result = double.TryParse(editCapturedUnits.Text.Trim(), out value); //i now = 108
                if (result)
                    capturedUnits = value;
                await logicToAddItemLocal(capturedUnits);
            }
        }

        private async void editCapturedUnits_KeyPress(object sender, KeyPressEventArgs e)
        {
            char signo_decimal = (char)46;
            //Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == signo_decimal)
            {
                e.Handled = false;
            } else if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan
                e.Handled = true;
            }
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                double capturedUnits = 1;
                double value = 0;
                bool result = double.TryParse(editCapturedUnits.Text.Trim(), out value); //i now = 108
                if (result)
                    capturedUnits = value;
                await logicToAddItemLocal(capturedUnits);
            }
        }

        private async void comboCodigoItemVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (comboCodigoItemVenta.Text.Length == 0)
                e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
            else if (comboCodigoItemVenta.Text.Length > 0)
                e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                double capturedUnits = 1;
                String barcode = comboCodigoItemVenta.Text;
                if (!barcode.Equals(""))
                {
                    String[] parts = barcode.Split('*');
                    if (parts != null && parts.Length > 1)
                    {
                        barcode = parts[1].Trim();
                        double value = 0;
                        bool result = double.TryParse(parts[0].Trim(), out value); //i now = 108  
                        if (result)
                            capturedUnits = value;
                    }
                }
                if (barcode.Equals(""))
                {
                    FormMessage formMessage = new FormMessage("Código Inrrecto", "Ingresa el código del producto", 3);
                    formMessage.ShowDialog();
                } else
                {
                    await validateProcesGetItemByBarcode(barcode, capturedUnits);
                }
            }
        }

        private async Task validateProcesGetItemByBarcode(String barcode, double capturedUnits)
        {
            if (serverModeLAN)
            {
                int value = 0;
                String description = "";
                await Task.Run(async () =>
                {
                    dynamic response = await ItemsController.getAnItemFromTheServerByCodeLAN(barcode, codigoCaja);
                    if (response.value == 1)
                    {
                        itemModel = response.item;
                        if (itemModel == null)
                        {
                            description = "No pudimos encontrar el artículo por código y código alterno!";
                        } else
                        {
                            value = 1;
                        }
                    } else
                    {
                        value = response.value;
                        description = response.description;
                    }
                });
                if (value == 1)
                {
                    if (itemModel != null)
                    {
                        seleccionoUnItem = true;
                        await fillAllFieldsFromAMovement(itemModel, capturedUnits, true);
                        editCapturedUnits.Text = capturedUnits.ToString();
                        double unidadesCapturadas = 0;
                        bool result = double.TryParse(editCapturedUnits.Text.Trim(), out unidadesCapturadas); //i now = 108
                        if (result)
                            capturedUnits = unidadesCapturadas;
                        await logicToAddItemLocal(capturedUnits);
                    }
                    else
                    {
                        FormMessage formMessage = new FormMessage("Código Inrrecto", "El Producto o servicio No Existe!", 3);
                        formMessage.ShowDialog();
                    }
                } else
                {
                    FormMessage formMessage = new FormMessage("Buscando Artículo", description, 3);
                    formMessage.ShowDialog();
                }
            }
            else
            {
                int value = 0;
                String description = "";
                await Task.Run(async () =>
                {
                    if (webActive)
                    {
                        dynamic responseItem = await ItemsController.getAnItemFromTheServerByCodeAPI(barcode, codigoCaja);
                        if (responseItem.value == 1)
                        {
                            itemModel = responseItem.item;
                            if (itemModel == null)
                            {
                                description = "No pudimos encontrar el artículo por código y código alterno!";
                            } else
                            {
                                value = 1;
                            }
                        }
                        else
                        {
                            itemModel = ItemModel.getAllDataFromAnItemWithBarCode(barcode);
                            if (itemModel == null)
                            {
                                barcode.ToUpper();
                                itemModel = ItemModel.getAllDataFromAnItemWithCode(barcode);
                                if (itemModel == null)
                                {
                                    description = "No pudimos encontrar el artículo por código y código alterno!";
                                } else
                                {
                                    value = 1;
                                }
                            } else
                            {
                                value = 1;
                            }
                        }
                    }
                    else
                    {
                        itemModel = ItemModel.getAllDataFromAnItemWithBarCode(barcode);
                        if (itemModel == null)
                        {
                            barcode.ToUpper();
                            itemModel = ItemModel.getAllDataFromAnItemWithCode(barcode);
                            if (itemModel == null) {
                                description = "No pudimos encontrar el artículo por código y código alterno!";
                            }
                            else
                            {
                                value = 1;
                            }
                        } else
                        {
                            value = 1;
                        }
                    }
                });
                if (value == 1)
                {
                    if (itemModel != null)
                    {
                        seleccionoUnItem = true;
                        await fillAllFieldsFromAMovement(itemModel, capturedUnits, true);
                        editCapturedUnits.Text = capturedUnits.ToString();
                        double unidadesCapturadas = 0;
                        bool result = double.TryParse(editCapturedUnits.Text.Trim(), out unidadesCapturadas); //i now = 108
                        if (result)
                            capturedUnits = unidadesCapturadas;
                        await logicToAddItemLocal(capturedUnits);
                    }
                    else
                    {
                        FormMessage formMessage = new FormMessage("Buscando Artículo", "El Producto o servicio No Existe!", 3);
                        formMessage.ShowDialog();
                    }
                } else
                {
                    FormMessage formMessage = new FormMessage("Buscando Artículo", description, 3);
                    formMessage.ShowDialog();
                }
            }
        }

        private void frmVentaNew_FormClosing(object sender, FormClosingEventArgs e) {
            if (idDocument != 0)
            {
                if (permissionPrepedido)
                {
                    bool documentoEnviadoAlCliente = DocumentModel.isItDocumentPrepedidoSendedToTheCustomer(idDocument);
                    if (documentoEnviadoAlCliente)
                    {
                        resetearTodosLosValores(true);
                    } else {
                        bool yaPesaronPollos = DocumentModel.pesoBrutoGuardadoAntesDeEnviarPrepedido(idDocument);
                        if (yaPesaronPollos) {
                            validateCancelDocument(e);
                        } else
                        {
                            bool yaPesaronTarasOPollos = DocumentModel.pesoTarasOPolloMalGuardadoAntesDeEnviarPrepedido(idDocument);
                            if (yaPesaronTarasOPollos)
                            {
                                validateCancelDocument(e);
                            }
                            else
                            {
                                processToDeleteDocumentDirectly();
                            }
                        }
                    }
                }
                else
                {
                    validateCancelDocument(e);
                    e.Cancel = true;
                }
            }
            else
            {
                resetearTodosLosValores(true);
            }
        }

        private async void editPriceItemVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            char signo_decimal = (char)46;
            //Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == signo_decimal)
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan
                e.Handled = true;
            }
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                double capturedUnits = 1;
                double value = 0;
                bool result = double.TryParse(editCapturedUnits.Text.Trim(), out value); //i now = 108
                if (result)
                    capturedUnits = value;
                await logicToAddItemLocal(capturedUnits);
            }
        }

        private void comboCodigoItemVenta_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void dataGridMovements_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void editCapturedUnits_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void comboBoxDocumentTypeFrmVenta_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnAgregar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void comboBoxUnitMWITemVenta_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnClearOptions_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void editDiscountItemVenta_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void editObservationMovement_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnCobrarFrmVenta_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void comboPreciosItemVenta_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void editNombreItemVenta_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnAbrir_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnSurtirPedidos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnBuscarArticuloTeclado_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnBuscarClientesNew_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void editUnidadNoConvertible_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnPausarDocumentoFrmVenta_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void imgItem_Click(object sender, EventArgs e)
        {
            if (itemModel != null)
            {
                FormImagen formImagen = new FormImagen(0, itemModel.id);
                formImagen.StartPosition = FormStartPosition.CenterScreen;
                formImagen.ShowDialog();
            } else
            {
                FormMessage formMessage = new FormMessage("Artículo faltante", "Tienes que seleccionar un artículo o movimiento", 3);
                formMessage.ShowDialog();
            }
        }

        private void imgCliente_Click(object sender, EventArgs e)
        {
            if (idCustomer != 0)
            {
                FormImagen formImagen = new FormImagen(1, idCustomer);
                formImagen.StartPosition = FormStartPosition.CenterScreen;
                formImagen.ShowDialog();
            }
            else
            {
                FormMessage formMessage = new FormMessage("Cliente faltante", "Tienes que seleccionar un cliente", 3);
                formMessage.ShowDialog();
            }
        }

        private void btnCerrar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private async void editDiscountItemVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            char signo_decimal = (char)46;
            //Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == signo_decimal)
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan
                e.Handled = true;
            }
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                double capturedUnits = 1;
                double value = 0;
                bool result = double.TryParse(editCapturedUnits.Text.Trim(), out value); //i now = 108
                if (result)
                    capturedUnits = value;
                await logicToAddItemLocal(capturedUnits);
            }
        }

        private void txtClientesNew_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnClearOptions_Click(object sender, EventArgs e)
        {
            clearMovementValues();
        }

        private void btnSurtirPedidosCotizaciones_Click(object sender, EventArgs e)
        {
            if (idDocument == 0)
            {
                deleteAllPedidosDetallesHuerfanos();
                FrmPedidosCotizacionesSurtir fspc = new FrmPedidosCotizacionesSurtir(cotmosActive);
                fspc.StartPosition = FormStartPosition.CenterScreen;
                fspc.ShowDialog();
                fillCustomerInformation(idCustomer);
                resetearValores(0);
                fillDataGridMovements();
                clearMovementsAddedData();
                if (idDocument != 0)
                {
                    btnRecuperar.Visible = false;
                    btnSurtirPedidos.Visible = false;
                    btnPausarDocumentoFrmVenta.Visible = true;
                    if (permissionPrepedido) {
                        activarODesactivarElementosAlSurtirPrepedidos(false);
                        btnBuscarClientesNew.Enabled = false;
                        String query = "SELECT * FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " +
                            LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " + idDocument;
                        MovimientosModel mm = MovimientosModel.getAMovement(query);
                        procesoLlenadoInfoMovimiento = true;
                        fillAllFieldsFromAMovement(mm);
                    }
                    validateComboDocumentType();
                    btnCerrar.Text = "Cancelar";
                    btnCerrar.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.close, 45, 45);
                } else
                {
                    btnCerrar.Text = "Cerrar";
                    btnCerrar.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.back_white, 45, 45);
                }
            } else
            {
                FormMessage fm = new FormMessage("Documento en Turno", "Tienes un documento vigente, cancelar o terminar antes de surtir", 2);
                fm.ShowDialog();
            }
        }

        private void activarODesactivarElementosAlSurtirPrepedidos(bool show)
        {
            //pictureBoxInfoSeleccionarPrecio.Visible = show;
            //pictureBoxInfoPrecioSeleccionado.Visible = show;
            //pictureBoxInfoDescuento.Visible = show;
            //editDiscountItemVenta.Visible = show;
            //textDiscountRateInfoVenta.Visible = show;
            //comboPreciosItemVenta.Visible = show;
        }

        private async Task deleteAllPedidosDetallesHuerfanos()
        {
            await Task.Run(() => {
                PedidoDetalleModel.deleteAllPDWhenTheirEncabezadosDoesNotExist();
            });
        }

        private void btnOpenScale_Click(object sender, EventArgs e)
        {
            if (idMovement != 0)
            {
                FormCalculateWeight fcw = new FormCalculateWeight(this, idDocument, idMovement);
                fcw.StartPosition = FormStartPosition.CenterScreen;
                fcw.ShowDialog();
            } else
            {
                FormMessage formMessage = new FormMessage("Calcular Pesos", "El movimiento no a sido seleccionado, para calcular los pesos tienes que seleccionar" +
                    " el movimiento!", 3);
                formMessage.ShowDialog();
            }
        }

        private void dataGridMovements_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //e.RowIndex;
        }

        private void btnPausar_Click(object sender, EventArgs e)
        {
            if (idDocument != 0)
            {
                String title = "";
                String message = "";
                if (documentType == DocumentModel.TIPO_VENTA || documentType == DocumentModel.TIPO_REMISION)
                {
                    title = "Pausar Venta";
                    message = "Desea pausar la venta actual?";
                }
                else if (documentType == DocumentModel.TIPO_COTIZACION)
                {
                    title = "Pausar Cotización";
                    message = "Desea pausar la cotización actual?";
                }
                else if (documentType == DocumentModel.TIPO_PEDIDO)
                {
                    title = "Pausar Pedido";
                    message = "Desea pausar el pedido actual?";
                }
                else if (documentType == DocumentModel.TIPO_DEVOLUCION)
                {
                    title = "Pausar Devolución";
                    message = "Desea pausar la devolución actual?";
                }
                if (permissionPrepedido)
                {
                    if (DocumentModel.isItDocumentFromAPrepedido(idDocument))
                    {
                        title = "Pausar Entrega";
                        message = "Desea pausar la entrega actual?";
                    } else
                    {
                        title = "Pausar Prepedido";
                        message = "Desea pausar el Prepedido actual?";
                    }
                }
                FrmConfirmation fc = new FrmConfirmation(title, message);
                fc.StartPosition = FormStartPosition.CenterScreen;
                fc.ShowDialog();
                if (FrmConfirmation.confirmation)
                {
                    dynamic sumsMap = getCurrentSumsFromDocument(idDocument);
                    if (DocumentModel.updateInformationForAPausedDocuments(idDocument, sumsMap.descuento, sumsMap.total))
                    {
                        if (DocumentModel.updateToPauseTheDocument(idDocument, 1))
                        {
                            resetearTodosLosValores(true);
                            clearMovementValues();
                            if (pausedFragment == 1)
                            {
                                this.Close();
                            } else
                            {
                                btnPausarDocumentoFrmVenta.Visible = false;
                            }
                        }
                    }
                }
                if (idDocument != 0)
                {
                    btnCerrar.Text = "Cancelar";
                    btnCerrar.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.close, 45, 45);
                }
                else
                {
                    btnCerrar.Text = "Cerrar";
                    btnCerrar.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.back_white, 45, 45);
                }
            }
            else
            {
                FormMessage fm = new FormMessage("Sin Documento", "No puedes pausar un documento vacio", 2);
                fm.ShowDialog();
                btnCerrar.Text = "Cerrar";
                btnCerrar.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.back_white, 45, 45);
            }
        }

        private void comboBoxDocumentTypeFrmVenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxDocumentTypeFrmVenta.Focused)
            {
                validateComboDocumentType();
            }
        }
        private async Task<bool> validateUseFiscalProductField()
        {
            bool useFical = false;
            await Task.Run(async () =>
            {
                useFical = ConfiguracionModel.useFiscalFieldValueActivated();
            });
            return useFical;
        }
        private async Task validateComboDocumentType()
        {
            if (!permissionPrepedido)
            {
                if (comboBoxDocumentTypeFrmVenta.SelectedIndex == 0)
                {
                    if (comboBoxDocumentTypeFrmVenta.Items[0].ToString().Trim().Equals("Venta"))
                    {
                        List<int> movimientosFiscales = new List<int>();
                        List<MovimientosModel> mmList = MovimientosModel.getAllMovementsFromADocument(idDocument);
                        bool errorcambio = false;
                        if (mmList != null && mmList.Count > 0)
                        {
                            bool validarfiscal = await validateUseFiscalProductField();
                            if (validarfiscal)
                            {
                                if (serverModeLAN)
                                {
                                    try
                                    {
                                        String fiscalField = await ItemModel.getTableNameForFiscalItemField(positionFiscalItemField);
                                        errorcambio = MovimientosModel.getMovimientosFiscalesDiferentesLAN(idDocument, fiscalField, serverModeLAN);
                                    }
                                    catch (Exception e)
                                    {
                                        SECUDOC.writeLog(e.ToString());
                                    }
                                }
                                else
                                {
                                    bool actual = await ItemModel.getFiscalItemFieldValue(ItemModel.getAllDataFromAnItem(mmList[0].itemId), positionFiscalItemField);
                                    for (int i = 0; i < mmList.Count; i++)
                                    {
                                        ClsItemModel im = ItemModel.getAllDataFromAnItem(mmList[i].itemId);
                                        bool isFiscal = await ItemModel.getFiscalItemFieldValue(im, positionFiscalItemField);

                                        // traer datos
                                        var db = new SQLiteConnection();
                                        try
                                        {
                                            ///aqui me quede 

                                            String query = "SELECT * FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " +
                                            LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " + idDocument;
                                            db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                                            db.Open();
                                            using (SQLiteCommand command = new SQLiteCommand(query, db))
                                            {
                                                using (SQLiteDataReader reader = command.ExecuteReader())
                                                {
                                                    if (reader.HasRows)
                                                    {

                                                    }
                                                    if (reader != null && !reader.IsClosed)
                                                        reader.Close();
                                                }
                                            }
                                        }
                                        catch (SQLiteException Ex)
                                        {
                                            SECUDOC.writeLog(Ex.ToString());
                                        }
                                        finally
                                        {
                                            if (db != null && db.State == ConnectionState.Open)
                                                db.Close();
                                        }
                                        // buscar datos
                                        if (actual != isFiscal)
                                        {
                                            errorcambio = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                errorcambio = false;
                            }


                            if (errorcambio)
                            {
                                FormMessage msj = new FormMessage("Cambio de concepto invalido", "No puedes tener movimientos fiscales y no fiscales en el mismo documento.\r\nBorra los productos inconsistentes o elimina el documento.", 2);
                                msj.ShowDialog();
                                procesoPrincipal = false;
                            }
                            else
                            {
                                documentType = DocumentModel.TIPO_REMISION;
                            }
                        }
                        else
                        {
                            documentType = DocumentModel.TIPO_REMISION;
                        }
                    }
                    else if (comboBoxDocumentTypeFrmVenta.Items[0].ToString().Trim().Equals("Cotización"))
                        documentType = DocumentModel.TIPO_COTIZACION;
                    else if (comboBoxDocumentTypeFrmVenta.Items[0].ToString().Trim().Equals("Pedido"))
                        documentType = DocumentModel.TIPO_PEDIDO;
                    else if (comboBoxDocumentTypeFrmVenta.Items[0].ToString().Trim().Equals("Devolución"))
                        documentType = DocumentModel.TIPO_DEVOLUCION;
                }
                else if (comboBoxDocumentTypeFrmVenta.SelectedIndex == 1)
                {
                    if (comboBoxDocumentTypeFrmVenta.Items[1].ToString().Trim().Equals("Cotización"))
                        documentType = DocumentModel.TIPO_COTIZACION;
                    else if (comboBoxDocumentTypeFrmVenta.Items[1].ToString().Trim().Equals("Pedido"))
                        documentType = DocumentModel.TIPO_PEDIDO;
                    else if (comboBoxDocumentTypeFrmVenta.Items[1].ToString().Trim().Equals("Devolución"))
                        documentType = DocumentModel.TIPO_DEVOLUCION;
                }
                else if (comboBoxDocumentTypeFrmVenta.SelectedIndex == 2)
                {
                    if (comboBoxDocumentTypeFrmVenta.Items[2].ToString().Trim().Equals("Pedido"))
                        documentType = DocumentModel.TIPO_PEDIDO;
                    else if (comboBoxDocumentTypeFrmVenta.Items[2].ToString().Trim().Equals("Devolución"))
                        documentType = DocumentModel.TIPO_DEVOLUCION;
                }
                else if (comboBoxDocumentTypeFrmVenta.SelectedIndex == 3)
                {
                    if (comboBoxDocumentTypeFrmVenta.Items[3].ToString().Trim().Equals("Devolución"))
                        documentType = DocumentModel.TIPO_DEVOLUCION;
                }
            }
            else
            {
                List<int> movimientosFiscales = new List<int>();
                List<MovimientosModel> mmList = MovimientosModel.getAllMovementsFromADocument(idDocument);
                bool errorcambio = false;
                if (mmList != null && mmList.Count > 0)
                {
                    bool actual = await ItemModel.getFiscalItemFieldValue(ItemModel.getAllDataFromAnItem(mmList[0].itemId), positionFiscalItemField);
                    int addFiscalOrNotMovement = 0;
                    for (int i = 0; i < mmList.Count; i++)
                    {
                        ClsItemModel im = ItemModel.getAllDataFromAnItem(mmList[i].itemId);

                        bool isFiscal = await ItemModel.getFiscalItemFieldValue(im, positionFiscalItemField);

                        if (actual != isFiscal)
                        {
                            errorcambio = true;
                            break;
                        }
                    }
                    if (errorcambio)
                    {
                        FormMessage msj = new FormMessage("Cambio de concepto invalido", "No puedes tener movimientos fiscales y no fiscales en el mismo documento", 2);
                        msj.ShowDialog();
                        procesoPrincipal = false;
                    }
                    else
                    {
                        documentType = DocumentModel.TIPO_VENTA;
                    }
                }
                else
                {
                    documentType = DocumentModel.TIPO_VENTA;
                }
            }
            formWaiting = new FormWaiting(this, 0, 0);
            formWaiting.ShowDialog();
        }

        public async Task changeDocumentType()
        {
            ClsChangeDocumentTypeController cdtc = new ClsChangeDocumentTypeController();
            dynamic response = await cdtc.doInBackground(idDocument, documentType, serverModeLAN, webActive, codigoCaja);
            if (response != null)
            {
                if (formWaiting != null)
                    formWaiting.Close();
                if (response.value == 1 || response.value == 0)
                {
                    documentType = response.typeDocument;
                    if (response.btnTerminateVisible)
                    {
                        btnPayWithCashFrmVenta.Visible = true;
                        btnSurtirPedidos.Visible = true;
                    }
                    else
                    {
                        btnPayWithCashFrmVenta.Visible = false;
                        btnSurtirPedidos.Visible = false;
                    }
                    btnCobrarFrmVenta.Text = response.btnTerminateText + " (F10)";
                    if (!permissionPrepedido)
                        fillComboDocumentType();
                    comboCodigoItemVenta.Focus();
                } else
                {
                    FormMessage formMessage = new FormMessage("Exception", response.description, 3);
                    formMessage.ShowDialog();
                    documentType = response.typeDocument;
                    if (response.btnTerminateVisible)
                    {
                        btnPayWithCashFrmVenta.Visible = true;
                        btnSurtirPedidos.Visible = true;
                    }
                    else
                    {
                        btnPayWithCashFrmVenta.Visible = false;
                        btnSurtirPedidos.Visible = false;
                    }
                    btnCobrarFrmVenta.Text = response.btnTerminateText + " (F10)";
                    if (!permissionPrepedido)
                        fillComboDocumentType();
                    comboCodigoItemVenta.Focus();
                }
            } else
            {
                if (formWaiting != null)
                    formWaiting.Close();
            }
        }

        private async void FrmVentaNew_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                cobrarCarritoTpv(idDocument);
            } else if (e.KeyCode == Keys.F3)
            {
                FormArticulos fa = new FormArticulos("", 1, cotmosActive);
                fa.ShowDialog();
                if (itemModel != null)
                {
                    editCapturedUnits.Select();
                    seleccionoUnItem = false;
                    await fillAllFieldsFromAMovement(itemModel, 0, false);
                }
            }
        }

        private async void BtnAgregar_Click(object sender, EventArgs e)
        {
            double capturedUnits = 1;
            double value = 0;
            bool result = double.TryParse(editCapturedUnits.Text.Trim(), out value); //i now = 108
            if (result)
                capturedUnits = value;
            if (serverModeLAN)
            {
                await logicToAddItemLocal(capturedUnits);
            } else
            {
                await logicToAddItemLocal(capturedUnits);
            }
            textporcentajepromocion.Visible = false;
            textPromocionesMovimiento.Visible = false;
        }

        public async Task logicToAddItemLocal(double capturedUnits)
        {
            int value = 0;
            String description = "";
            String precioVenta = editPriceItemVenta.Text.Trim();
            await Task.Run(async () =>
            {
                if (!procesoPrincipal)
                {
                    if (itemModel == null)
                    {
                        description = "Tienes que elegir un artículo!";
                    }
                    else
                    {
                        bool documentoDePrepedido = DocumentModel.isItDocumentFromAPrepedido(idDocument);
                        if (capturedUnits <= 0 && !documentoDePrepedido && !permissionPrepedido)
                        {
                            description = "Tienes que agregar una cantidad (unidades) mayor a cero!";
                        }
                        else
                        {
                            String priceText = precioVenta.Replace(",", "").Replace("$", "").Replace("MXN", "");
                            if (priceText.Equals(""))
                            {
                                description = "Tienes que agregar un precio válido al artículo!";
                            }
                            else
                            {
                                priceText = priceText.Replace(",", "").Replace("$", "").Replace("MXN", "");
                                if (!permissionPrepedido)
                                {
                                    finalRealPrice = Convert.ToDouble(priceText);
                                    if (finalRealPrice <= 0)
                                    {
                                        description = "Tienes que agregar un precio mayor a cero!";
                                    }
                                    else
                                    {
                                        value = 1;
                                    }
                                }
                                else
                                {
                                    finalRealPrice = Convert.ToDouble(priceText);
                                    if (finalRealPrice <= 0)
                                    {
                                        description = "Tienes que agregar un precio mayor a cero";
                                    }
                                    else
                                    {
                                        if (idMovement != 0)
                                            editMove = true;
                                        value = 1;
                                    }
                                }
                            }
                        }
                    }
                } else
                {
                    description = "La variable del proceso principal es verdadera!\r\nSolución: Pausar documento y recuperarlo de nuevo!";
                }
            });
            if (value == 1)
            {
                double nonConvertibleUnits = 0;
                double valueNonConvertibleUnits = 0;
                bool resultNonConvertibleUnits = double.TryParse(editUnidadNoConvertible.Text.Trim(), out valueNonConvertibleUnits);
                if (resultNonConvertibleUnits)
                    nonConvertibleUnits = valueNonConvertibleUnits;
                String discountText = editDiscountItemVenta.Text.Trim();
                String observationMovement = editObservationMovement.Text.Trim();
                await addOrEditMovementLocal(capturedUnits, finalRealPrice, discountText, observationMovement, nonConvertibleUnits);
                comboCodigoItemVenta.DroppedDown = false;
                comboCodigoItemVenta.Focus();
                editCapturedUnits.Text = Convert.ToString(1);

            }
            else
            {

                FormMessage formMessage = new FormMessage("Agregando Movimiento", description, 3);
                formMessage.ShowDialog();
            }
        }

        private void dataGridArticulos_Scroll(object sender, ScrollEventArgs e)
        {
            if (movesList.Count < totalMovements && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
                {
                    if (e.NewValue > dataGridMovements.Rows.Count - getDisplayedRowsCount())
                    {
                        //prevent loading from autoscroll.
                        TimeSpan ts = DateTime.Now - lastLoading;
                        if (ts.TotalMilliseconds > 100)
                        {
                            firstVisibleRow = e.NewValue;
                            //dataGridItems.PerformLayout();
                            fillDataGridMovements();
                        }
                        else
                        {
                            dataGridMovements.FirstDisplayedScrollingRowIndex = e.OldValue;
                        }
                    }
                }
            }
        }

        private int getDisplayedRowsCount()
        {
            int count = dataGridMovements.Rows[dataGridMovements.FirstDisplayedScrollingRowIndex].Height;
            count = dataGridMovements.Height / count;
            return count;
        }

        private void hideScrollBars()
        {
            imgSinDatosFrmVenta.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.logosynctpvmoving, 300, 300);
            imgSinDatosFrmVenta.Visible = true;
            gridScrollBars = dataGridMovements.ScrollBars;
            //dataGridItems.ScrollBars = ScrollBars.None;
        }

        public void resetearValores(int queryType)
        {
            editObservationMovement.MaxLength = 250;
            editObservationMovement.Text = "";
            procesoPrincipal = false;
            this.queryType = queryType;
            query = "";
            queryRecords = "";
            totalMovements = 0;
            lastId = 0;
            progress = 0;
            movesList.Clear();
            dataGridMovements.Rows.Clear();
            textExistenciaReal.Visible = false;
        }

        public List<MovimientosModel> listaarecalcular()
        {
            List<MovimientosModel> movimientosModels = new List<MovimientosModel>();
            try
            {
                movimientosModels = MovimientosModel.getAllMovements("");//aqui
            }
            catch(Exception e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            return movimientosModels;
        }

        public void actualizarSubDescYTotalVenta(int idDocumento)
        {
            //List<MovimientosModel> listaarecalcular = listaarecalcular();
            String folioDoc = DocumentModel.getFolioVentaForADocument(idDocumento);
            if (folioDoc.Trim().Equals(""))
                textFolioFrmVenta.Text = "Documento No Creado";
            else textFolioFrmVenta.Text = "Folio # " + folioDoc;
            dynamic sumsMap = getCurrentSumsFromDocument(idDocumento);
            if (permissionPrepedido) {
                if (DocumentModel.isItDocumentFromAPrepedido(idDocumento))
                {
                    textInfoDocumentTypeFrmVenta.Text = "Surtiendo PrePedido";
                    comboBoxDocumentTypeFrmVenta.Visible = false;
                    textDiscountFrmVenta.Visible = false;
                    textInfoTotal.Text = "Pollos a Entregar";
                    String query = "SELECT * FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " + LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " +
                        idDocument;
                    List<MovimientosModel> movesList = MovimientosModel.getAllMovements(query);
                    if (movesList != null)
                    {
                        query = "SELECT * FROM " + LocalDatabase.TABLA_PESO + " WHERE " +
                            LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = " + movesList[0].id + " AND " +
                            LocalDatabase.CAMPO_TIPO_PESO + " = " + WeightModel.TIPO_REAL;
                        WeightModel wm = WeightModel.getAWeight(query);
                        if (wm != null)
                        {
                            String unitName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(movesList[0].nonConvertibleUnitId);
                            textTotalFrmVenta.Text = (" " + MetodosGenerales.obtieneDosDecimales(movesList[0].nonConvertibleUnits) + " " + unitName +
                                "\n en " + wm.cajas + " Cajas");
                            textSubtotalFrmVenta.Text = "Total: " + sumsMap.total.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                        }
                        else
                        {
                            String unitName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(movesList[0].nonConvertibleUnitId);
                            textTotalFrmVenta.Text = (" " + MetodosGenerales.obtieneDosDecimales(movesList[0].nonConvertibleUnits) + " " + unitName +
                                "\n en " + 0 + " Cajas");
                        }
                        bool enviadoAlClienteDestarado = DocumentModel.isItDocumentPrepedidoSendedToTheCustomerAndDestarado(idDocument);
                        if (!enviadoAlClienteDestarado)
                        {
                            btnCobrarFrmVenta.Text = "Entregar a Cliente (F10)";
                            btnCobrarFrmVenta.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.entregar_back, 35, 35);
                        }
                        else
                        {
                            btnCobrarFrmVenta.Text = "Guardar (F10)";
                            btnCobrarFrmVenta.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.send, 35, 35);
                        }
                    }
                }
                else
                {
                    textInfoDocumentTypeFrmVenta.Text = "Tipo de Documento";
                    comboBoxDocumentTypeFrmVenta.Visible = true;
                    btnCobrarFrmVenta.Text = "Generar Prepedido (F10)";
                    btnCobrarFrmVenta.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.send, 35, 35);
                    List<MovimientosModel> movesList = MovimientosModel.getAllMovementsFromADocument(idDocument);
                    if (movesList != null)
                    {
                        String unitName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(movesList[0].nonConvertibleUnitId);
                        textTotalFrmVenta.Text = (" " + MetodosGenerales.obtieneDosDecimales(movesList[0].nonConvertibleUnits) + " " + unitName);
                    }
                }
            }
            else
            {
                textInfoDocumentTypeFrmVenta.Text = "Tipo de Documento";
                comboBoxDocumentTypeFrmVenta.Visible = true;
                textSubtotalFrmVenta.Text = ("Subtotal " + sumsMap.subtotal.ToString("C", CultureInfo.CurrentCulture) + " MXN");
                textDiscountFrmVenta.Text = ("Descuento " + sumsMap.descuento.ToString("C", CultureInfo.CurrentCulture) + " MXN");
                pictureBoxInfoDescuento.Visible = true;
                pictureBoxInfoDescuento.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.discount_black, 15,15);
                textInfoTotal.Text = "Total";
                textTotalFrmVenta.Text = sumsMap.total.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                btnCobrarFrmVenta.Text = "Cobrar (F10)";
                btnCobrarFrmVenta.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.checkout, 35, 35);
            }
        }

        public static ExpandoObject getCurrentSumsFromDocument(int idDocumento)
        {
            dynamic sumsMap = new ExpandoObject();
            String sumas = MovimientosModel.obtenerSumaDeSubDescYTotalDeMovimientosDeUndocumento(idDocumento);
            if (!sumas.Equals(""))
            {
                String[] parts = sumas.Split(new Char[] { '-' });
                String subtotal = parts[0];
                subtotal = subtotal.Replace(",", "");
                if (subtotal.Equals(""))
                    subtotal = "0";
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
                sumsMap.subtotal = 0.0;
                sumsMap.descuento = 0.0;
                sumsMap.total = 0.0;
            }
            return sumsMap;
        }

    }
}
