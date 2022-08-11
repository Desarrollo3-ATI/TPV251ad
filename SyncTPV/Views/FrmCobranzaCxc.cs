using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using SyncTPV.Views.Cxc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace SyncTPV.Views
{
    public partial class FrmCobranzaCxc : Form
    {
        private int LIMIT = 50;
        public int idCustomer = 0;
        private int idCxc = 0;
        private String queryCxc = "", queryMovesCxc = "", queryTotalCxc = "", queryTotalMovesCxc = "", referenceAbonoFiniquitar = "";
        private FormWaiting frmWaiting;
        private bool permissionPrepedido = false;
        private bool serverModeLAN = false;
        private List<CuentasXCobrarModel> cxcList;
        private List<CuentasXCobrarModel> cxcListTemp;
        private List<MovementsCxcModel> movesCxcList;
        private List<MovementsCxcModel> movesCxcListTemp;
        private ScrollBars gridScrollBarsCxc, gridScrollBarsMovesCxc;
        private DateTime lastLoadingCxc, lastLoadingMovesCxc;
        private int lastIdCxc = 0, lastIdMovesCxc = 0;
        private int progressCxc = 0, progressMovesCxc = 0;
        private int firstVisibleRowCxc, firstVisibleRowMovesCxc;
        private int totalCxc = 0, totalMovesCxc, queryTypeCxc = 0, queryTypeMovesCxc;
        private int repIdLocal = 0;

        public FrmCobranzaCxc(int idCustomer)
        {
            this.idCustomer = idCustomer;
            cxcList = new List<CuentasXCobrarModel>();
            movesCxcList = new List<MovementsCxcModel>();
            InitializeComponent();
            permissionPrepedido = UserModel.doYouHavePermissionPrepedido();
            serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
        }

        private void FrmCobranzaCxc_Load(object sender, EventArgs e)
        {
            String queryName = "SELECT " + LocalDatabase.CAMPO_NOMBRECLIENTE + " FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " +
                LocalDatabase.CAMPO_ID_CLIENTE + " = " + idCustomer;
            String customerName = CustomerModel.getAStringValueFromACustomer(queryName);
            textNombreClienteFrmCobranza.Text = "Créditos de " + customerName;
            dataGridViewCreditsDocuments.RowTemplate.DefaultCellStyle.Padding = new Padding(10,5,10,5);
            dataGridViewCreditsDocuments.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            fillDataGridViewCxc();
        }

        private void hideScrollBars()
        {
            imgSinDatos.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.logosynctpvmoving, 300, 300);
            imgSinDatos.Visible = true;
            gridScrollBarsCxc = dataGridViewCreditsDocuments.ScrollBars;
            //dataGridItems.ScrollBars = ScrollBars.None;
        }

        private async Task fillDataGridViewCxc()
        {
            hideScrollBars();
            lastLoadingCxc = DateTime.Now;
            cxcListTemp = await getAllCxcFromACustomer();
            if (cxcListTemp != null)
            {
                progressCxc += cxcListTemp.Count;
                cxcList.AddRange(cxcListTemp);
                if (cxcList.Count > 0 && dataGridViewCreditsDocuments.ColumnHeadersVisible == false)
                    dataGridViewCreditsDocuments.ColumnHeadersVisible = true;
                for (int i = 0; i < cxcListTemp.Count; i++)
                {
                    int n = dataGridViewCreditsDocuments.Rows.Add();
                    dataGridViewCreditsDocuments.Rows[n].Cells[0].Value = cxcListTemp[i].id + "";
                    dataGridViewCreditsDocuments.Columns["idDgvCxc"].Visible = false;
                    dataGridViewCreditsDocuments.Rows[n].Cells[1].Value = cxcListTemp[i].creditDocumentId;
                    dataGridViewCreditsDocuments.Columns["idCxcDgvCxc"].Visible = false;
                    dataGridViewCreditsDocuments.Rows[n].Cells[2].Value = cxcListTemp[i].creditDocumentFolio;
                    dataGridViewCreditsDocuments.Columns[2].Width = 80;
                    dataGridViewCreditsDocuments.Rows[n].Cells[3].Value = cxcListTemp[i].diasAtraso;
                    dataGridViewCreditsDocuments.Columns[3].Width = 80;
                    double abonos = CuentasXCobrarModel.getTotalAbonadoDeUnDocumento(cxcListTemp[i].creditDocumentId);
                    double saldoActual = (cxcListTemp[i].saldo_actual - abonos);
                    dataGridViewCreditsDocuments.Rows[n].Cells[4].Value = "Saldo anterior: " + 
                        cxcListTemp[i].saldo_actual.ToString("C", CultureInfo.CurrentCulture)+" MXN"+
                        "\r\nAbonos: " + abonos.ToString("C", CultureInfo.CurrentCulture)+" MXN" +
                        "\r\nNuevo saldo: "+saldoActual.ToString("C", CultureInfo.CurrentCulture)+ "MXN";
                    dataGridViewCreditsDocuments.Columns[4].Width = 150;
                    dataGridViewCreditsDocuments.Rows[n].Cells[5].Value = cxcListTemp[i].fechaVencimiento;
                    dataGridViewCreditsDocuments.Columns[5].Width = 200;
                    dataGridViewCreditsDocuments.Rows[n].Cells[6].Value = cxcListTemp[i].factura_mostrador;
                    dataGridViewCreditsDocuments.Columns[6].Width = 250;
                }
                dataGridViewCreditsDocuments.PerformLayout();
                cxcListTemp.Clear();
                if (cxcList.Count > 0)
                    lastIdCxc = Convert.ToInt32(cxcList[cxcList.Count - 1].id);
                imgSinDatos.Visible = false;
                //dataGridItems.Focus();
            }
            else
            {
                if (progressCxc == 0)
                    imgSinDatos.Visible = true;
            }
            textTotalCredits.Text = "Créditos: " + totalCxc.ToString().Trim();
            //reset displayed row
            if (firstVisibleRowCxc > -1)
            {
                showScrollBars();
                if (cxcList.Count > 0)
                {
                    dataGridViewCreditsDocuments.FirstDisplayedScrollingRowIndex = firstVisibleRowCxc;
                    imgSinDatos.Visible = false;
                }
            }
        }

        private void showScrollBars()
        {
            dataGridViewCreditsDocuments.ScrollBars = gridScrollBarsCxc;
        }

        private void dataGridViewCreditsDocuments_Scroll(object sender, ScrollEventArgs e)
        {
            if (cxcList.Count < totalCxc && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
                {
                    int cuntDisplayed = dataGridViewCreditsDocuments.Rows.Count - getDisplayedRowsCount();
                    if (e.NewValue >= cuntDisplayed)
                    {
                        //prevent loading from autoscroll.
                        TimeSpan ts = DateTime.Now - lastLoadingCxc;
                        if (ts.TotalMilliseconds > 100)
                        {
                            firstVisibleRowCxc = e.NewValue;
                            //dataGridItems.PerformLayout();
                            fillDataGridViewCxc();
                        }
                        else
                        {
                            dataGridViewCreditsDocuments.FirstDisplayedScrollingRowIndex = e.OldValue;
                        }
                    }
                }
            }
        }

        private int getDisplayedRowsCount()
        {
            int count = dataGridViewCreditsDocuments.Rows[dataGridViewCreditsDocuments.FirstDisplayedScrollingRowIndex].Height;
            count = dataGridViewCreditsDocuments.Height / count;
            return count;
        }

        private async Task<List<CuentasXCobrarModel>> getAllCxcFromACustomer()
        {
            List<CuentasXCobrarModel> cxcList = null;
            await Task.Run(async () =>
            {
                if (queryTypeCxc == 0)
                {
                    queryCxc = "SELECT * FROM " + LocalDatabase.TABLA_CXC + " WHERE " + LocalDatabase.CAMPO_CLIENTE_ID_CXC + " = " + idCustomer +
                        " AND " + LocalDatabase.CAMPO_TIPO_CXC + " = 1 ORDER BY " + LocalDatabase.CAMPO_DOCTO_CC_ID_CXC + " ASC LIMIT " + LIMIT;
                    queryTotalCxc = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_CXC + " WHERE " + LocalDatabase.CAMPO_CLIENTE_ID_CXC + " = " + idCustomer +
                        " AND " + LocalDatabase.CAMPO_TIPO_CXC + " = 1";
                    cxcList = CuentasXCobrarModel.getAllCxc(queryCxc);
                    totalCxc = CuentasXCobrarModel.getIntValue(queryTotalCxc);
                }
            });
            return cxcList;
        }

        private void dataGridViewCreditsDocuments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                object idCxc = dataGridViewCreditsDocuments.Rows[e.RowIndex].Cells[1].Value;
                this.idCxc = Convert.ToInt32(idCxc);
                CuentasXCobrarModel cxcm = CuentasXCobrarModel.getDataForACxC(this.idCxc);
                if (cxcm != null)
                {
                    textPendienteDocument.Text = "Pendiente: $ " + MetodosGenerales.obtieneDosDecimales((cxcm.saldo_actual - cxcm.amount)) + " MXN";
                    referenceAbonoFiniquitar = "" + idCustomer + MetodosGenerales.getCurrentDateAndHourForFolioVenta();
                    textReferenciaAbonoFiniquitar.Text = "Referencia: " + referenceAbonoFiniquitar;
                }
                fillComboBoxFormasDecobro();
                getDataSelectedCredit();
                resetearVariablesMovesCxc(0);
                fillDataGridMovementsCredits();
                editImporteAbono.Focus();
            }
        }

        private void resetearVariablesMovesCxc(int queryType)
        {
            this.queryTypeMovesCxc = queryType;
            queryMovesCxc = "";
            queryTotalMovesCxc = "";
            totalMovesCxc = 0;
            lastIdMovesCxc = 0;
            progressMovesCxc = 0;
            movesCxcList.Clear();
            dataGridViewMovimientosCxc.Rows.Clear();
        }

        private async Task fillComboBoxFormasDecobro()
        {
            List<FormasDeCobroModel> fcList = null;
            await Task.Run(async () =>
            {
                if (serverModeLAN)
                    await FormasDeCobroController.downloadAllFormasDeCobroLAN();
                else await FormasDeCobroController.downloadAllFormasDeCobroAPI(0);
                fcList = FormasDeCobroModel.getAllFormasDeCobro();
                if (fcList == null)
                {
                    if (serverModeLAN)
                        await FormasDeCobroController.downloadAllFormasDeCobroLAN();
                    else await FormasDeCobroController.downloadAllFormasDeCobroAPI(0);
                    fcList = FormasDeCobroModel.getAllFormasDeCobro();
                } 
            });
            if (fcList != null)
            {
                comboBoxFormasDeCobro.DataSource = fcList;
                comboBoxFormasDeCobro.ValueMember = "FORMA_COBRO_ID";
                comboBoxFormasDeCobro.DisplayMember = "NOMBRE";
            }
            else
            {
                comboBoxFormasDeCobro.DataSource = null;
            }
        }

        private async Task getDataSelectedCredit()
        {
            String data = await getAllDataCredit();
            textCreditoSeleccionado.Text = data;
        }

        private async Task<string> getAllDataCredit()
        {
            String data = "";
            await Task.Run(async () =>
            {
                CuentasXCobrarModel cxcm = CuentasXCobrarModel.getDataForACxC(idCxc);
                if (cxcm != null)
                {
                    String queryCustomer = "SELECT " + LocalDatabase.CAMPO_NOMBRECLIENTE + " FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " +
                    LocalDatabase.CAMPO_ID_CLIENTE + " = " + cxcm.customerId;
                    String tipoDocument = "Documento a Crédito";
                    data = "Cliente: " + CustomerModel.getAStringValueFromACustomer(queryCustomer) + " Tipo Documento: " + tipoDocument;
                }
            });
            return data;
        }

        private void dataGridViewCreditsDocuments_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void hideScrollBarsMovesCxc()
        {
            imgSinDatosMovimientos.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.logosynctpvmoving, 300, 300);
            imgSinDatosMovimientos.Visible = true;
            gridScrollBarsMovesCxc = dataGridViewMovimientosCxc.ScrollBars;
            //dataGridItems.ScrollBars = ScrollBars.None;
        }

        private async Task fillDataGridMovementsCredits()
        {
            hideScrollBarsMovesCxc();
            lastLoadingMovesCxc = DateTime.Now;
            movesCxcListTemp = await getAllMovementsFromACredit();
            if (movesCxcListTemp != null)
            {
                progressMovesCxc += movesCxcListTemp.Count;
                movesCxcList.AddRange(movesCxcListTemp);
                if (movesCxcList.Count > 0 && dataGridViewMovimientosCxc.ColumnHeadersVisible == false)
                    dataGridViewMovimientosCxc.ColumnHeadersVisible = true;
                for (int i = 0; i < movesCxcListTemp.Count; i++)
                {
                    int n = dataGridViewMovimientosCxc.Rows.Add();
                    dataGridViewMovimientosCxc.Rows[n].Cells[0].Value = movesCxcListTemp[i].id + "";
                    dataGridViewMovimientosCxc.Columns[0].Visible = false;
                    dataGridViewMovimientosCxc.Rows[n].Cells[1].Value = movesCxcListTemp[i].creditDocumentId;
                    dataGridViewMovimientosCxc.Columns[1].Visible = false;
                    dataGridViewMovimientosCxc.Rows[n].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dataGridViewMovimientosCxc.Rows[n].Cells[2].Value = movesCxcListTemp[i].itemCode;
                    dataGridViewMovimientosCxc.Rows[n].Cells[3].Value = movesCxcListTemp[i].number;
                    dataGridViewMovimientosCxc.Rows[n].Cells[4].Value = movesCxcListTemp[i].price;
                    dataGridViewMovimientosCxc.Rows[n].Cells[5].Value = movesCxcListTemp[i].capturedUnits;
                    dataGridViewMovimientosCxc.Rows[n].Cells[6].Value = movesCxcListTemp[i].capturedUnitId;
                    dataGridViewMovimientosCxc.Rows[n].Cells[7].Value = movesCxcListTemp[i].subtotal;
                    dataGridViewMovimientosCxc.Rows[n].Cells[8].Value = movesCxcListTemp[i].discount;
                    dataGridViewMovimientosCxc.Rows[n].Cells[9].Value = movesCxcListTemp[i].total;
                }
                dataGridViewMovimientosCxc.PerformLayout();
                movesCxcListTemp.Clear();
                if (movesCxcList.Count > 0)
                    lastIdMovesCxc = Convert.ToInt32(movesCxcList[movesCxcList.Count - 1].id);
                imgSinDatosMovimientos.Visible = false;
            }
            else
            {
                if (progressMovesCxc == 0)
                    imgSinDatosMovimientos.Visible = true;
            }
            textInfoMovimientos.Text = "Movimientos: " + totalMovesCxc.ToString().Trim();
            if (firstVisibleRowMovesCxc > -1)
            {
                showScrollBarsMovesCxc();
                if (movesCxcList.Count > 0)
                {
                    dataGridViewMovimientosCxc.FirstDisplayedScrollingRowIndex = firstVisibleRowMovesCxc;
                    imgSinDatos.Visible = false;
                }
            }
        }

        private void showScrollBarsMovesCxc()
        {
            dataGridViewMovimientosCxc.ScrollBars = gridScrollBarsMovesCxc;
        }

        private async Task<List<MovementsCxcModel>> getAllMovementsFromACredit()
        {
            List<MovementsCxcModel> movesList = null;
            await Task.Run(async () =>
            {
                if (queryTypeMovesCxc == 0)
                {
                    queryMovesCxc = "SELECT * FROM " + LocalDatabase.TABLA_MOVEMENTCXC + " WHERE " + LocalDatabase.CAMPO_CXCID_MOVEMENTCXC +
                    " = " + idCxc+" AND "+LocalDatabase.CAMPO_ID_MOVEMENTCXC+" > "+lastIdMovesCxc+" ORDER BY "+
                    LocalDatabase.CAMPO_ID_MOVEMENTCXC+" ASC LIMIT "+LIMIT;
                    queryTotalMovesCxc = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_MOVEMENTCXC + " WHERE " + LocalDatabase.CAMPO_CXCID_MOVEMENTCXC +
                    " = " + idCxc;
                    movesList = MovementsCxcModel.getAllMovementsOfACxc(queryMovesCxc);
                    totalMovesCxc = MovementsCxcModel.getIntValue(queryTotalMovesCxc);
                }
            });
            return movesList;
        }

        private void dataGridViewMovimientosCxc_Scroll(object sender, ScrollEventArgs e)
        {
            if (movesCxcList.Count < totalMovesCxc && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
                {
                    int cuntDisplayed = dataGridViewMovimientosCxc.Rows.Count - getDisplayedRowsCount();
                    if (e.NewValue >= cuntDisplayed)
                    {
                        //prevent loading from autoscroll.
                        TimeSpan ts = DateTime.Now - lastLoadingMovesCxc;
                        if (ts.TotalMilliseconds > 100)
                        {
                            firstVisibleRowMovesCxc = e.NewValue;
                            //dataGridItems.PerformLayout();
                            fillDataGridMovementsCredits();
                        }
                        else
                        {
                            dataGridViewMovimientosCxc.FirstDisplayedScrollingRowIndex = e.OldValue;
                        }
                    }
                }
            }
        }

        private void btnVerPagos_Click(object sender, EventArgs e)
        {
            if (idCxc != 0)
            {
                FormPagosCxc formPagosCxc = new FormPagosCxc(idCxc);
                formPagosCxc.ShowDialog();
            } else
            {
                FormMessage formMessage = new FormMessage("Seleccionar Crédito","Tienes que seleccionar un documento a crédito de la lista para ver los pagos " +
                    "realizados", 3);
                formMessage.ShowDialog();
            }
        }

        private void dataGridViewCreditsDocuments_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void dataGridViewMovimientosCxc_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void editImporteAbono_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void btnRealizarAbono_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void textCurrency_Click(object sender, EventArgs e)
        {

        }

        private void btnRealizarAbono_Click_1(object sender, EventArgs e)
        {
            logicToValidateAddAbono();
        }

        private async Task logicToValidateAddAbono()
        {
            if (idCxc != 0)
            {
                String abonoText = editImporteAbono.Text.ToString().Trim();
                if (!abonoText.Equals(""))
                {
                    CuentasXCobrarModel cxcm = CuentasXCobrarModel.getDataForACxC(idCxc);
                    if (cxcm != null)
                    {
                        double pending = cxcm.saldo_actual - cxcm.amount;
                        double amountToPay = Convert.ToDouble(abonoText);
                        if (amountToPay > 0 && amountToPay < pending)
                        {
                            int fcId = Convert.ToInt32(comboBoxFormasDeCobro.SelectedValue);
                            var repIdLocal = CuentasXCobrarModel.addNewAbono(idCxc, amountToPay, fcId, referenceAbonoFiniquitar, MetodosGenerales.getCurrentDateAndHour());
                            if (repIdLocal != 0)
                            {
                                if (UserModel.permisoParaEnviarDocsAutomaticamente() > 0)
                                {
                                    dynamic responseValidateLicense = await LicenseModel.validateLicense();
                                    if (responseValidateLicense.value > 0)
                                    {
                                        if (MetodosGenerales.verifyIfInternetIsAvailable())
                                        {
                                            //EnviarDocumentosService.startActionSendPayment(4, 0, 0, repIdLocal)
                                            frmWaiting = new FormWaiting(this, 1, repIdLocal);
                                            frmWaiting.StartPosition = FormStartPosition.CenterScreen;
                                            frmWaiting.ShowDialog();
                                        }
                                        else
                                        {
                                            FormMessage formMessage = new FormMessage("Error al Validar Datos", "Tu licencia no esta activa!", 2);
                                            formMessage.ShowDialog();
                                            editImporteAbono.Focus();
                                        }
                                        updateInformation();
                                    }
                                    else
                                    {
                                        //saveClientPositionCobranza(cxcm.clienteId, repIdLocal);
                                        PopupNotifier popup = new PopupNotifier();
                                        popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                                        popup.TitleColor = Color.Red;
                                        popup.TitleText = "Datos Correctos";
                                        popup.ContentText = "Abono agregado Satisfactoriamente!";
                                        popup.ContentColor = Color.Red;
                                        popup.Popup();
                                        updateInformation();
                                    }
                                }
                                else
                                {
                                    //saveClientPositionCobranza(cxcm.clienteId, repIdLocal);
                                    PopupNotifier popup = new PopupNotifier();
                                    popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                                    popup.TitleColor = Color.Red;
                                    popup.TitleText = "Datos Correctos";
                                    popup.ContentText = "Abono agregado correctamente!";
                                    popup.ContentColor = Color.Red;
                                    popup.Popup();
                                }
                            }
                            else
                            {
                                FormMessage formMessage = new FormMessage("Datos Faltantes", "Algo falló al agregar el abono!", 2);
                                formMessage.ShowDialog();
                                editImporteAbono.Focus();
                            }
                        }
                        else
                        {
                            FormMessage formMessage = new FormMessage("Datos Faltantes", "Tienes que agregar un importe mayor a cero y menor al total pendiente!", 2);
                            formMessage.ShowDialog();
                            editImporteAbono.Focus();
                        }
                    }
                    else
                    {
                        FormMessage formMessage = new FormMessage("Datos Faltantes", "Cuenta no encontrada!", 2);
                        formMessage.ShowDialog();
                        editImporteAbono.Focus();
                    }
                }
                else
                {
                    FormMessage formMessage = new FormMessage("Datos Faltantes", "Tienes que agregar un importe válido!", 2);
                    formMessage.ShowDialog();
                    editImporteAbono.Focus();
                }
            }
            else
            {
                FormMessage formMessage = new FormMessage("Datos Faltantes", "Tienes que seleccionar una cuenta por cobrar o documento a crédito!", 2);
                formMessage.ShowDialog();
                editImporteAbono.Focus();
            }
        }

        public void validateResponseSendReps(dynamic response, int repIdLocal)
        {
            if (frmWaiting != null)
                frmWaiting.Close();
            if (response.value == 100)
            {
                resetearValoresCxc(0);
                fillDataGridViewCxc();
                PopupNotifier popup = new PopupNotifier();
                popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                popup.TitleColor = Color.Blue;
                popup.TitleText = "Pago enviado";
                popup.ContentText = "Los datos del pago fueron enviados!";
                popup.ContentColor = Color.Blue;
                popup.Popup();
                clsTicket ticket = new clsTicket();
                ticket.imprimirTicketCobranza(repIdLocal, permissionPrepedido, serverModeLAN);
                updateInformation();
            }
        }

        private void updateInformation()
        {
            idCxc = 0;
            editImporteAbono.Text = "";
            textCreditoSeleccionado.Text = "Seleccionar Documento";
            textPendienteDocument.Text = "Pendiente";
            textReferenciaAbonoFiniquitar.Text = "Referencia";
            resetearValoresCxc(0);
            fillDataGridViewCxc();
            resetearVariablesMovesCxc(0);
            fillDataGridMovementsCredits();
        }

        private void btnLiquidarCredito_Click(object sender, EventArgs e)
        {
            if (idCxc != 0)
            {
                processToFiniquitarDeuda();
            }
            else
            {
                FormMessage formMessage = new FormMessage("Datos Faltantes", "Tienes que seleccionar una cuenta!", 2);
                formMessage.ShowDialog();
            }
        }

        private async Task processToFiniquitarDeuda()
        {
            CuentasXCobrarModel cxcm = CuentasXCobrarModel.getDataForACxC(this.idCxc);
            if (cxcm != null)
            {
                double pendientePorPagar = (cxcm.saldo_actual - cxcm.amount);
                FrmConfirmation fc = new FrmConfirmation("Finiquitar Crédito", "¿Estas seguro de que deseas finiquitar la deuda de " +
                    "$ " + MetodosGenerales.obtieneDosDecimales(pendientePorPagar) + " MXN?");
                fc.StartPosition = FormStartPosition.CenterScreen;
                fc.ShowDialog();
                if (FrmConfirmation.confirmation)
                {
                    int fcId = Convert.ToInt32(comboBoxFormasDeCobro.SelectedValue);
                    repIdLocal = CuentasXCobrarModel.addNewAbono(idCxc, pendientePorPagar, fcId, referenceAbonoFiniquitar, MetodosGenerales.getCurrentDateAndHour());
                    if (repIdLocal != 0)
                    {
                        if (UserModel.permisoParaEnviarDocsAutomaticamente() > 0)
                        {
                            dynamic responseValidateLicense = await LicenseModel.validateLicense();
                            if (responseValidateLicense.value > 0)
                            {
                                if (MetodosGenerales.verifyIfInternetIsAvailable())
                                {
                                    //EnviarDocumentosService.startActionSendPayment(4, 0, 0, repIdLocal)
                                    frmWaiting = new FormWaiting(this, 1, repIdLocal);
                                    frmWaiting.StartPosition = FormStartPosition.CenterScreen;
                                    frmWaiting.ShowDialog();
                                }
                                else
                                {
                                    FormMessage formMessage = new FormMessage("Error al Validar Datos", "Tu licencia no esta activa!", 2);
                                    formMessage.ShowDialog();
                                    editImporteAbono.Focus();
                                }
                                updateInformation();
                            }
                            else
                            {
                                //saveClientPositionCobranza(cxcm.clienteId, repIdLocal);
                                PopupNotifier popup = new PopupNotifier();
                                popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.caution, 100, 100);
                                popup.TitleColor = Color.Red;
                                popup.TitleText = "Datos Correctos";
                                popup.ContentText = "Abono agregado Satisfactoriamente!";
                                popup.ContentColor = Color.Red;
                                popup.Popup();
                                updateInformation();
                            }
                        }
                        else
                        {
                            //saveClientPositionCobranza(cxcm.clienteId, repIdLocal);
                            PopupNotifier popup = new PopupNotifier();
                            popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.caution, 100, 100);
                            popup.TitleColor = Color.Red;
                            popup.TitleText = "Datos Correctos";
                            popup.ContentText = "Abono agregado correctamente!";
                            popup.ContentColor = Color.Red;
                            popup.Popup();
                        }
                    }
                    else
                    {
                        FormMessage formMessage = new FormMessage("Exception", "Algo falló al agregar el abono!", 2);
                        formMessage.ShowDialog();
                        editImporteAbono.Focus();
                    }
                }
            }
        }

        public async Task validateSendResp(int idRep)
        {
            bool serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
            EnviarDocumentosService eds = new EnviarDocumentosService();
            dynamic response = await eds.startActionSendPayment(4, 0, 0, idRep, serverModeLAN);
            repIdLocal = idRep;
            validateResponseSendReps(response, repIdLocal);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdateCxc_Click(object sender, EventArgs e)
        {
            if (idCustomer != 0)
            {
                frmWaiting = new FormWaiting(this, 0, repIdLocal);
                frmWaiting.StartPosition = FormStartPosition.CenterScreen;
                frmWaiting.ShowDialog();
            } else
            {
                FormMessage formMessage = new FormMessage("Cliente No Seleccionado", "Probablemente no seleccionaste ningún cliente!",3);
                formMessage.ShowDialog();
            }
        }

        public async Task downloadCxcForThisCustomer()
        {
            dynamic response = null;
            if (serverModeLAN)
                response = await ClsCuentasPorCobrarController.downloadCreditForACustomerLAN(idCustomer);
            else response = await ClsCuentasPorCobrarController.downloadCreditForACustomerAPI(idCustomer);
            if (response.value == 1)
            {
                if (frmWaiting != null)
                    frmWaiting.Close();
                resetearValoresCxc(0);
                fillDataGridViewCxc();
            } else if (response.value == 0)
            {
                if (frmWaiting != null)
                    frmWaiting.Close();
                FormMessage fm = new FormMessage("Sin Datos por Descargar", response.description, 3);
                fm.ShowDialog();
                resetearValoresCxc(0);
                fillDataGridViewCxc();
            } 
            else
            {
                if (frmWaiting != null)
                    frmWaiting.Close();
                FormMessage fm = new FormMessage("Error", response.description, 2);
                fm.ShowDialog();
            }
        }

        private void resetearValoresCxc(int queryType)
        {
            this.queryTypeCxc = queryType;
            queryCxc = "";
            queryTotalCxc = "";
            totalCxc = 0;
            lastIdCxc = 0;
            progressCxc = 0;
            cxcList.Clear();
            dataGridViewCreditsDocuments.Rows.Clear();
        }

    }
}
