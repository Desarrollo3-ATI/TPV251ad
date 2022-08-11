using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using SyncTPV.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace SyncTPV
{
    public partial class frmCorteReporte : Form
    {
        private int reporteCajaActivated = 0;
        double totalVendidoYCobrado = 0;
        public static int userId = 0;
        int Error = 0;
        string FechaInicial = "", FechaFinal = "", MensajeError = "";
        //int contadorBanderaFecha = 0;
        private static int lastId = 0;
        FormWaiting fw;
        /* Todo lo relacionado a la Carga de Créditos*/
        private readonly int LIMIT = 100;
        private int progressCreditosDelTurno = 0, progressVentasDelTurno = 0;
        private DateTime lastLoadingCreditosDelTurno, lastLoadingVentasDelTurno;
        private int firstVisibleRowCreditosDelTurno, firstVisibleRowVentasDelTurno;
        private ScrollBars gridScrollBarsCreditosDelTurno, gridScrollBarsVentasDelTurno;
        private int totalCreditosDelTurno = 0, totalVentasDelTurno = 0, queryTypeCreditosDelTurno = 0, queryTypeVentasDelTurno = 0, queryTypeVentasAnteriores = 0, queryTypeWithdrawals = 0;
        private String queryCreditosDelTurno = "", queryTotalesCreditosDelTurno = "", queryVentasDelTurno = "", queryTotalesVentasDelTurno = "";
        private static int lastIdVentasDelTurno = 0, lastIdCreditosDelTurno;
        private List<DocumentModel> creditosDelTurnoList;
        private List<DocumentModel> creditosDelTurnoListTemp;
        private List<DocumentModel> ventasDelTurnoList;
        private List<DocumentModel> ventasDelTurnoListTemp;
        private String queryDocumentosAnteriores = "", queryTotalesDocumentosAnteriores = "";
        private static int lastIdDocumentosAnteriores = 0;
        private List<DocumentModel> documentosAnterioresList;
        private List<DocumentModel> documentosAnterioresListTemp;
        private bool permissionPrepedido = false, serverModeLAN = false;

        public frmCorteReporte()
        {
            InitializeComponent();
            serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
            btnImprimirCorteDeCaja.Click += new EventHandler(this.btnImprimirCorteDeCaja_Click);
            btnGenerarPdfReporteDocumentos.Click += new EventHandler(this.btnGenerarPdfReporteDocumentos_Click);
            //btnGenerarPdfReporteRetiros.Click += new EventHandler(this.btnGenerarPdfReporteRetiros_Click);
            //btnGenerarPdfReporteRetiros.Location = new Point(100, 100);
            btnGenerarPdfRetiros.Click += new EventHandler(this.btnGenerarPdfReporteRetiros_Click);
            //panelToolbar.BackColor = Color.FromArgb(GeneralTxt.Red, GeneralTxt.Green, GeneralTxt.Blue);
            dataGridViewDocumentos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(GeneralTxt.Red, GeneralTxt.Green, GeneralTxt.Blue);
            //dataGridViewVentasReporteCaja.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(GeneralTxt.Red, GeneralTxt.Green, GeneralTxt.Blue);
            //dataGirdViewRetiradoReporteCaja.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(GeneralTxt.Red, GeneralTxt.Green, GeneralTxt.Blue);
            dataGridViewCortesDeCaja.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(GeneralTxt.Red, GeneralTxt.Green, GeneralTxt.Blue);
            dataGridViewDocumentos.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(GeneralTxt.RedSelectionRows, GeneralTxt.GreenSelectionRows, GeneralTxt.BlueSelectionRows);
            dataGridViewVentasReporteCaja.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(GeneralTxt.RedSelectionRows, GeneralTxt.GreenSelectionRows, GeneralTxt.BlueSelectionRows);
            dataGirdViewRetiradoReporteCaja.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(GeneralTxt.RedSelectionRows, GeneralTxt.GreenSelectionRows, GeneralTxt.BlueSelectionRows);
            dataGridViewCortesDeCaja.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(GeneralTxt.RedSelectionRows, GeneralTxt.GreenSelectionRows, GeneralTxt.BlueSelectionRows);
            documentosAnterioresList = new List<DocumentModel>();
            creditosDelTurnoList = new List<DocumentModel>();
            ventasDelTurnoList = new List<DocumentModel>();
            permissionPrepedido = UserModel.doYouHavePermissionPrepedido();
        }


        private void FrmCorteReporte_Load(object sender, EventArgs e)
        {
            //fillComboUsers();
            //checkBoxTipoDeBusqueda.Visible = false;
            /*string cargaInicial = "";
            if (cargaInicial == "" || cargaInicial == null) { }
            else { dateTimeStartDate.Value = Convert.ToDateTime(cargaInicial); }*/
            //validarPermisoPrepedido();
        }

        /*private async Task validarPermisoPrepedido()
        {
            if (ClsUsersModel.doYouHavePermissionPrepedido())
            {
                tabControlReportes.SelectedIndex = 0;
                tabControlReportes.TabPages.Remove(tabControlReportes.TabPages[2]);
                tabControlReportes.TabPages.Remove(tabControlReportes.TabPages[1]);
                comboBoxSeleccionarUsuario.Font = new Font(comboBoxSeleccionarUsuario.Font.FontFamily, 20);
                comboBoxSeleccionarUsuario.Location = new Point(comboBoxSeleccionarUsuario.Location.X, comboBoxSeleccionarUsuario.Location.Y - 12);
                dateTimeStartDate.Height = 35;
                dateTimeStartDate.Font = new Font(dateTimeStartDate.Font.FontFamily, 11);
                dateTimeEndDate.Height = 35;
                dateTimeEndDate.Font = new Font(dateTimeStartDate.Font.FontFamily, 11);
            } else
            {
                tabControlReportes.SelectedIndex = 2;
                imgSinDatosCreditosDelTurno.Image = ClsMetodosGenerales.redimencionarBitmap(Properties.Resources.sindatos, 144, 133);
            }
        }*/

        /*private async Task fillComboUsers()
        {
            List<ClsUsersModel> usersList = await getAllUsers();
            if (usersList != null)
            {
                //Setup data binding
                this.comboBoxSeleccionarUsuario.DataSource = usersList;
                this.comboBoxSeleccionarUsuario.ValueMember = "id";
                this.comboBoxSeleccionarUsuario.DisplayMember = "Nombre";

                // make it readonly
                this.comboBoxSeleccionarUsuario.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }*/

        /*private async Task<List<ClsUsersModel>> getAllUsers()
        {
            List<ClsUsersModel> usersList = null;
            await Task.Run(async () =>
            {
                if (ClsLicenseModel.isItTPVLicense())
                    usersList = ClsUsersModel.getAllTellers();
                else usersList = ClsUsersModel.getAllAgents();
            });
            return usersList;
        }*/

        /*private int progressDocumentosAnteriores = 0;
        private int totalDocumentosAnteriores = 0;
        private DateTime lastLoadingDocumentosAnteriores;
        private int firstVisibleRowDocumentosAnteriores;
        private ScrollBars gridScrollBarsDocumentosAnteriores;*/

        /*private async Task fillDataGridDocuments()
        {
            gridScrollBarsDocumentosAnteriores = dataGridViewDocumentos.ScrollBars;
            lastLoadingDocumentosAnteriores = DateTime.Now;
            documentosAnterioresListTemp = await getAllDocuments();
            if (documentosAnterioresListTemp != null)
            {
                progressDocumentosAnteriores += documentosAnterioresListTemp.Count;
                documentosAnterioresList.AddRange(documentosAnterioresListTemp);
                if (documentosAnterioresList.Count > 0 && dataGridViewDocumentos.ColumnHeadersVisible == false)
                    dataGridViewDocumentos.ColumnHeadersVisible = true;
                for (int i = 0; i < documentosAnterioresListTemp.Count; i++)
                {
                    int n = dataGridViewDocumentos.Rows.Add();
                    dataGridViewDocumentos.Rows[n].Cells[0].Value = documentosAnterioresListTemp[i].id + "";
                    dataGridViewDocumentos.Columns["idDocumento"].Visible = false;
                    String customerName = ClsCustomerModel.getAStringValueFromACustomer("SELECT " + LocalDatabase.CAMPO_NOMBRECLIENTE + " FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " +
                            LocalDatabase.CAMPO_ID_CLIENTE + " = " + documentosAnterioresListTemp[i].cliente_id);
                    dataGridViewDocumentos.Rows[n].Cells[1].Value = customerName;
                    if (documentosAnterioresListTemp[i].tipo_documento == 1)
                    {
                        dataGridViewDocumentos.Rows[n].Cells[2].Value = "Cotización";
                    }
                    else if (documentosAnterioresListTemp[i].tipo_documento == 2 || documentosAnterioresListTemp[i].tipo_documento == 4)
                    {
                        dataGridViewDocumentos.Rows[n].Cells[2].Value = "Venta";
                    }
                    else if (documentosAnterioresListTemp[i].tipo_documento == 3)
                    {
                        dataGridViewDocumentos.Rows[n].Cells[2].Value = "Pedido";
                    }
                    else if (documentosAnterioresListTemp[i].tipo_documento == 5)
                    {
                        dataGridViewDocumentos.Rows[n].Cells[2].Value = "Devolución";
                    } else if (documentosAnterioresListTemp[i].tipo_documento == ClsDocumentModel.TIPO_COTIZACION_MOSTRADOR)
                    {
                        dataGridViewDocumentos.Rows[n].Cells[2].Value = "Cotización de Mostrador";
                    }
                    if (permissionPrepedido)
                        dataGridViewDocumentos.Rows[n].Cells[2].Value = "Entrega";
                    dataGridViewDocumentos.Rows[n].Cells[3].Value = documentosAnterioresListTemp[i].fechahoramov;
                    dataGridViewDocumentos.Rows[n].Cells[4].Value = documentosAnterioresListTemp[i].fventa;
                    if (permissionPrepedido)
                    {
                        dataGridViewDocumentos.Columns["descuentoDocumento"].HeaderText = "Producto";
                        dataGridViewDocumentos.Columns["formaDecobroAbonoDocumento"].HeaderText = "Cantidad";
                        dataGridViewDocumentos.Columns["anticipoDocumento"].HeaderText = "Unidad";
                        dataGridViewDocumentos.Columns["totalDocumento"].HeaderText = "Piezas Pollos";
                        ClsMovimientosModel mm = ClsMovimientosModel.getAMovementFromADocument(documentosAnterioresListTemp[i].id);
                        if (mm != null)
                        {
                            String productName = ItemModel.getTheNameOfAnItem(mm.itemId);
                            dataGridViewDocumentos.Rows[n].Cells[5].Value = productName;
                            dataGridViewDocumentos.Rows[n].Cells[6].Value = "" + mm.unidadesCapturadas;
                            String capturedUnitsName = ClsUnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(mm.unidadesCapturadasId);
                            dataGridViewDocumentos.Rows[n].Cells[7].Value = "" + capturedUnitsName;
                            String capturedNonConvertibleUnitsName = ClsUnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(mm.unidadesNoConvertiblesId);
                            dataGridViewDocumentos.Rows[n].Cells[8].Value = ""+mm.unidadesNoConvertibles+" "+ capturedNonConvertibleUnitsName;
                        }
                    } else {
                        dataGridViewDocumentos.Rows[n].Cells[5].Value = documentosAnterioresListTemp[i].descuento;
                        String fcDocs = "";
                        String cambioInfo = "";
                        List<ClsFormasDeCobroDocumentoModel> fcdList = ClsFormasDeCobroDocumentoModel.getAllTheWaysToCollectADocument(documentosAnterioresListTemp[i].id);
                        double cambio = 0;
                        if (fcdList != null && fcdList.Count > 0)
                        {
                            for (int j = 0; j < fcdList.Count; j++)
                            {
                                String fcName = ClsFormasDeCobroModel.getANameFrromAFomaDeCobroWithId(fcdList[j].formaCobroIdAbono);
                                cambio += fcdList[j].cambio;
                                if (j == (fcdList.Count - 1))
                                    fcDocs += fcName + " $ " + fcdList[j].importe + " MXN";
                                else fcDocs += fcName + " $ " + fcdList[j].importe + " MXN\r\n";
                            }
                        }
                        else
                        {
                            if (documentosAnterioresListTemp[i].forma_corbo_id_abono > 0)
                            {
                                String fcName = ClsFormasDeCobroModel.getANameFrromAFomaDeCobroWithId(documentosAnterioresListTemp[i].forma_corbo_id_abono);
                                fcDocs += fcName + " $ " + ClsMetodosGenerales.obtieneDosDecimales(documentosAnterioresListTemp[i].anticipo) + " MXN";
                            }
                            else
                            {
                                String fcName = ClsFormasDeCobroModel.getANameFrromAFomaDeCobroWithId(documentosAnterioresListTemp[i].forma_corbo_id_abono);
                                fcDocs += fcName + " $ " + ClsMetodosGenerales.obtieneDosDecimales(documentosAnterioresListTemp[i].total) + " MXN";
                            }
                        }
                        cambioInfo += "\r\nCambio $ " + ClsMetodosGenerales.obtieneDosDecimales(cambio) + " MXN";
                        dataGridViewDocumentos.Rows[n].Cells[6].Value = fcDocs + cambioInfo;
                        dataGridViewDocumentos.Rows[n].Cells[7].Value = "$ " + ClsMetodosGenerales.obtieneDosDecimales(documentosAnterioresListTemp[i].anticipo) + " MXN";
                        dataGridViewDocumentos.Rows[n].Cells[8].Value = "Total $ " + ClsMetodosGenerales.obtieneDosDecimales(documentosAnterioresListTemp[i].total) + " MXN";
                    }
                }
                dataGridViewDocumentos.PerformLayout();
                documentosAnterioresListTemp.Clear();
                lastIdDocumentosAnteriores = Convert.ToInt32(documentosAnterioresList[documentosAnterioresList.Count - 1].id);
                imgSinDatosDocuments.Visible = false;
            }
            else
            {
                if (progressDocumentosAnteriores > 0)
                {
                    imgSinDatosDocuments.Visible = true;
                    btnGenerarPdfReporteDocumentos.Enabled = true;
                }
            }
            textTotalDocumentosAnteriores.Text = "Documentos: " + totalDocumentosAnteriores.ToString().Trim();
            //reset displayed row
            if (firstVisibleRowDocumentosAnteriores > -1)
            {
                dataGridViewDocumentos.ScrollBars = gridScrollBarsDocumentosAnteriores;
                if (dataGridViewDocumentos.Rows.Count > 0)
                {
                    dataGridViewDocumentos.FirstDisplayedScrollingRowIndex = firstVisibleRowDocumentosAnteriores;
                    imgSinDatosDocuments.Visible = false;
                }
            }
        }*/

        /*private async Task<List<ClsDocumentModel>> getAllDocuments()
        {
            List<ClsDocumentModel> documentsList = null;
            await Task.Run(async () =>
            {
                if (queryTypeVentasAnteriores == 0)
                {
                    queryDocumentosAnteriores = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                    LocalDatabase.CAMPO_FECHAHORAMOV_DOC + " BETWEEN '" + FechaInicial + " 00:00:00' AND '" +
                    FechaFinal + " 23:59:59' AND " + LocalDatabase.CAMPO_USUARIOID_DOC + " = " + userId + " AND " + LocalDatabase.CAMPO_ID_DOC + " > " +
                    lastIdDocumentosAnteriores + " ORDER BY " + LocalDatabase.CAMPO_ID_DOC + " LIMIT " + LIMIT;
                    queryTotalesDocumentosAnteriores = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                    LocalDatabase.CAMPO_FECHAHORAMOV_DOC + " BETWEEN '" + FechaInicial + " 00:00:00' AND '" +
                    FechaFinal + " 23:59:59' AND " + LocalDatabase.CAMPO_USUARIOID_DOC + " = " + userId;
                }
                totalDocumentosAnteriores = ClsDocumentModel.getIntValue(queryTotalesDocumentosAnteriores);
                documentsList = ClsDocumentModel.getAllDocuments(queryDocumentosAnteriores);
            });
            return documentsList;
        }*/

        /*private void dataGridViewDocumentos_Scroll(object sender, ScrollEventArgs e)
        {
            if (documentosAnterioresList.Count < totalDocumentosAnteriores)
            {
                if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
                {
                    if (e.NewValue > dataGridViewDocumentos.Rows.Count - getDisplayedRowsCountDocumentosAnteriores())
                    {
                        //prevent loading from autoscroll.
                        TimeSpan ts = DateTime.Now - lastLoadingDocumentosAnteriores;
                        if (ts.TotalMilliseconds > 100)
                        {
                            firstVisibleRowDocumentosAnteriores = e.NewValue;
                            //dataGridItems.PerformLayout();
                            fillDataGridDocuments();
                        }
                        else
                        {
                            dataGridViewDocumentos.FirstDisplayedScrollingRowIndex = e.OldValue;
                        }
                    }
                }
            }
        }*/

        /*private int getDisplayedRowsCountDocumentosAnteriores()
        {
            int count = dataGridViewDocumentos.Rows[dataGridViewDocumentos.FirstDisplayedScrollingRowIndex].Height;
            count = dataGridViewDocumentos.Height / count;
            return count;
        }*/

        private void button1_Click(object sender, EventArgs e)
        {
            //clsTicket Ticket = new clsTicket();
            //Ticket.CrearCorteCaja(FechaInicial, FechaFinal);

        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            //FechaInicial = dtHoraInicio.Value.ToString("HH:MM:ss");
            /*if (checkBoxTipoDeBusqueda.Checked)
            {
                string cargaInicial = "";
                if (cargaInicial == "" || cargaInicial == null) { }
                else { dateTimeStartDate.Value = Convert.ToDateTime(cargaInicial); }

                FechaInicial = (dateTimeStartDate.Value).ToString("yyyy-MM-dd HH:mm:ss");
                FechaFinal = (dateTimeEndDate.Value).ToString("yyyy-MM-dd");
                FechaFinal += " 23:59:59";
            }
            else
            {
                FechaInicial = (dateTimeStartDate.Value).ToString("yyyy-MM-dd HH:mm:ss");
                if (contadorBanderaFecha > 1)
                {
                    FechaInicial = (dateTimeStartDate.Value).ToString("yyyy-MM-dd");
                    FechaInicial += " 00:00:00";  //00:00:00 a. m
                }
                FechaFinal = (dateTimeEndDate.Value).ToString("yyyy-MM-dd");
                FechaFinal += " 23:59:59";
            }*/
        }

        private void picCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BusquedaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            /*if (checkBoxTipoDeBusqueda.Checked)
            {
                dateTimeStartDate.Enabled = false;
                dateTimeEndDate.Enabled = false;
            }
            else
            {
                dateTimeStartDate.Enabled = true;
                dateTimeEndDate.Enabled = true;
            }*/
        }

        private void dateTimeInicio_MouseCaptureChanged(object sender, EventArgs e)
        {
            //contadorBanderaFecha = contadorBanderaFecha + 1;
        }

        private void comboBoxSeleccionarUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSeleccionarUsuario.Focused == true)
            {
                var um = (UserModel)comboBoxSeleccionarUsuario.SelectedItem;
                if (um != null)
                {
                    //FechaInicial = (dateTimeStartDate.Value).ToString("yyyy-MM-dd HH:mm:ss");
                    FechaInicial = (dateTimeStartDate.Value).ToString("yyyy-MM-dd");
                    FechaFinal = (dateTimeEndDate.Value).ToString("yyyy-MM-dd");
                    //FechaFinal += " 23:59:59";
                    userId = um.id;
                    tabControl1_SelectedIndexChanged(sender, e);
                    PopupNotifier popup = new PopupNotifier();
                    popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.caution, 100, 100);
                    popup.TitleColor = Color.Red;
                    popup.TitleText = "Datos";
                    popup.ContentText = "Documentos de " + um.Nombre + " por usuario";
                    popup.ContentColor = Color.Red;
                    popup.Popup();
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControlReportes.SelectedIndex)
            {
                case 0:
                    {
                        if (reporteCajaActivated == 1)
                        {
                            panelSelectUserAndDate.Height = panelSelectUserAndDate.Height + 70;
                            panelTabControl.Location = new Point(0, 152);
                            panelTabControl.Width = this.Width - 10;
                            panelTabControl.Height = (this.Height - panelToolbar.Height -
                                panelSelectUserAndDate.Height - 50);
                            reporteCajaActivated = 0;
                        }
                        //logicToGetAndChargeDocuments();
                        break;
                    }
                case 1:
                    {
                        if (reporteCajaActivated == 1)
                        {
                            panelSelectUserAndDate.Height = panelSelectUserAndDate.Height + 70;
                            panelTabControl.Location = new Point(0, 152);
                            panelTabControl.Width = this.Width - 10;
                            panelTabControl.Height = (this.Height - panelToolbar.Height -
                                panelSelectUserAndDate.Height - 50);
                            reporteCajaActivated = 0;
                        }
                        //logicToGetAnbdChargeWithdrawals();
                        break;
                    }
                case 2:
                    {
                        /*if (!this.MaximizeBox)
                            textTotalVendido.MaximumSize = new Size(350,2000);
                        else textTotalVendido.MaximumSize = new Size(205, 2000);
                        reporteCajaActivated = 1;
                        panelSelectUserAndDate.Height = panelSelectUserAndDate.Height - 70;
                        panelTabControl.Location = new Point(0, 82);
                        panelTabControl.Width = this.Width - 10;
                        panelTabControl.Height = (this.Height - panelToolbar.Height - 50);
                        totalVendidoYCobrado = 0;
                        logicToGetAnbdChargeCash();*/
                        break;
                    }
            }
        }

        /*private async Task logicToGetAnbdChargeCash()
        {
            Cursor.Current = Cursors.WaitCursor;
            showOrHideDateAndUsers(false);
            fw = new FrmWaiting(FrmWaiting.CALL_REPORTES, this, 2, null, 0);
            fw.StartPosition = FormStartPosition.CenterScreen;
            fw.ShowDialog();
            await fillDataGridCreditosReporteCaja();
            await fillDataGridVentasReporteCaja();
            await fillDataGridRetiradoReporteCaja();
            await fillTotalesVentasDelTurno();
            await fillTotalesCreditosDelTurno();
            await fillTotalesPorFormasDeCobro();
            await fillTotaltesRetiros();
        }*/

        /*private async Task logicToGetAnbdChargeWithdrawals()
        {
            Cursor.Current = Cursors.WaitCursor;
            showOrHideDateAndUsers(true);
            fw = new FrmWaiting(FrmWaiting.CALL_REPORTES, this, 1, null, 0);
            fw.StartPosition = FormStartPosition.CenterScreen;
            fw.ShowDialog();
            //await fillDataGridRetiros();
            Cursor.Current = Cursors.Default;
        }*/

        /*private async Task logicToGetAndChargeDocuments()
        {
            Cursor.Current = Cursors.WaitCursor;
            showOrHideDateAndUsers(true);
            fw = new FrmWaiting(FrmWaiting.CALL_REPORTES, this, 0, null, 0);
            fw.StartPosition = FormStartPosition.CenterScreen;
            fw.ShowDialog();
            //await fillDataGridDocuments();
            Cursor.Current = Cursors.Default;
        }*/

        /*public void resetearValores(int queryTypeRef)
        {
            //queryType = queryTypeRef;
            //progress = ITEMS_TO_CALL;
            dataGridViewDocumentos.ClearSelection();
            lastId = 0;
        }*/

        /*public async Task callDeleteDocumentsProcess(Boolean newQuery)
        {
            bool serverModeLAN = ClsConfiguracionModel.isLANPermissionActivated();
            DeleteDataService dds = new DeleteDataService();
            dynamic response = new ExpandoObject();
            if (serverModeLAN)
                response = await dds.deleteDownloadedDocumentsLAN(newQuery);
            else response = await dds.deleteDownloadedDocuments(newQuery);
            validateDeleteDocumentsAndWithdrawalsResponse(response, 1);
        }*/

        /*public async Task callDeleteWithdrawalsProcess(Boolean newQuery)
        {
            DeleteDataService dds = new DeleteDataService();
            dynamic response = await dds.deleteDownloadedWithdrawals(newQuery);
            if (response != null)
            {
                validateDeleteDocumentsAndWithdrawalsResponse(response, 2);
            }
            else
            {
                if (fw != null && fw.Visible)
                    fw.Close();
            }
        }*/

        /*private void validateDeleteDocumentsAndWithdrawalsResponse(dynamic response, int dw)
        {
            if (response != null)
            {
                int valor = response.valor;
                if (valor == 100) {
                    if (dw == 1)
                        callDownloadDocumentsProcess(userId, FechaInicial + " 00:00:00", FechaFinal + " 23:59:59", 0, 0);
                    else if (dw == 2)
                        callDownloadWithdrawalsProcess(userId, FechaInicial + " 00:00:00", FechaFinal + " 23:59:59", 0, 0);
                }
            }
        }*/

        /*private async Task callDownloadDocumentsProcess(int idUser, String startDate, String endDate, int lastId, int times)
        {
            bool serverModeLAN = ClsConfiguracionModel.isLANPermissionActivated();
            GetDataService gds = new GetDataService();
            dynamic respuesta = new ExpandoObject();
            if (serverModeLAN)
                respuesta = await gds.downloadAllDocumentsLAN(idUser, startDate, endDate, lastId, times);
            else respuesta = await gds.downloadAllDocuments(idUser, startDate, endDate, lastId, times);
            validateDownloadDocumentsResponse(respuesta);
        }*/

        /*private async Task callDownloadWithdrawalsProcess(int idUser, String startDate, String endDate, int lastId, int times)
        {
            GetDataService gds = new GetDataService();
            dynamic respuesta = new ExpandoObject();
            if (ClsConfiguracionModel.isLANPermissionActivated())
                respuesta = await gds.downloadAllWithdrawalsLAN(idUser, startDate, endDate, lastId, times);
            else respuesta = await gds.downloadAllWithdrawals(idUser, startDate, endDate, lastId, times);
            validateDownloadDocumentsResponse(respuesta);
        }*/

        /*private async Task validateDownloadDocumentsResponse(dynamic respuesta)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (respuesta != null)
            {
                int value = respuesta.valor;
                String descripcion = respuesta.descripcion;
                int lastIdDocuments = respuesta.idDocumento;
                int lastIdWithdrawals = respuesta.idRetiro;
                int totalRecords = respuesta.totalRecords;
                int times = respuesta.times;
                //SalesReportFragment.dialogEsperaSalesReport.dismiss()
                if (value != 100)
                {
                    if (SalesReportFragment.dialogEsperaSalesReport != null && SalesReportFragment.dialogEsperaSalesReport.isShowing)
                        SalesReportFragment.dialogEsperaSalesReport.dismiss()
            if (WithdrawalReportFragment.dialogEsperaWithdrawalReport != null && WithdrawalReportFragment.dialogEsperaWithdrawalReport.isShowing)
                        WithdrawalReportFragment.dialogEsperaWithdrawalReport.dismiss()
                    PopupNotifier popup = new PopupNotifier();
                    popup.Image = ClsMetodosGenerales.redimencionarImagenes(Properties.Resources.caution, 100, 100);
                    popup.TitleColor = Color.Red;
                    popup.TitleText = "Alerta";
                    popup.ContentText = descripcion;
                    popup.ContentColor = Color.Red;
                    popup.Popup();
                    FormMessage fm = new FormMessage("Alerta", "Validar conexión a Internet y al servidor", 2);
                    fm.ShowDialog();
                    if (fw != null && fw.Visible)
                        fw.Close();
                }
                else
                {
                    if (totalRecords >= 1000)
                    {
                        if (times <= 1)
                        {
                            if (descripcion.Equals("Proceso Finalizado Documents"))
                            {
                                resetearValores(0);
                                await fillDataGridDocuments();
                                await callDownloadDocumentsProcess(userId, FechaInicial + " 00:00:00", FechaFinal + " 23:59:59", lastIdDocuments, times);
                            }
                            else if (descripcion.Equals("Proceso Finalizado Withdrawals"))
                            {
                                resetearValores(0);
                                await fillDataGridRetiros();
                                await callDownloadWithdrawalsProcess(userId, FechaInicial + " 00:00:00", FechaFinal + " 23:59:59", lastIdDocuments, times);
                            }
                        }
                        else
                        {
                            await callDeleteDocumentsProcess(false);
                            await callDeleteWithdrawalsProcess(false);
                            if (descripcion.Equals("Proceso Finalizado Documents"))
                            {
                                await callDownloadDocumentsProcess(userId, FechaInicial + " 00:00:00", FechaFinal + " 23:59:00", lastIdDocuments, times);
                            }
                            else if (descripcion.Equals("Proceso Finalizado Withdrawals"))
                            {
                                await callDownloadWithdrawalsProcess(userId, FechaInicial + " 00:00:00", FechaFinal + " 23:59:59", lastIdDocuments, times);
                            }
                        }
                    }
                    else
                    {
                        if (descripcion.Equals("Proceso Finalizado Documents"))
                        {
                            resetearValores(0);
                            resetVariablesDocumentosAnteriores();
                            await fillDataGridDocuments();
                            if (fw != null && fw.Visible)
                                fw.Close();
                        }
                        else if (descripcion.Equals("Proceso Finalizado Withdrawals"))
                        {
                            resetearValores(0);
                            await fillDataGridRetiros();
                            if (fw != null && fw.Visible)
                                fw.Close();
                        }
                    }
                }
            }
            Cursor.Current = Cursors.Default;
        }*/

        /*private async Task fillTotalesCreditosDelTurno()
        {
            Cursor.Current = Cursors.Default;
            String formatototales = "";
            List<ClsFormasDeCobroModel> formasList = null;
            await Task.Run(() => {
                formasList = ClsFormasDeCobroModel.getAllFormasDeCobro();
                double sumaCambio = 0;
                double sumaAnticiposVentasACredito = 0;
                double sumasDeImporte = 0.0;
                if (formasList != null)
                {
                    for (int i = 0; i < formasList.Count; i++)
                    {
                        if (formasList[i].FORMA_COBRO_CC_ID != 71)
                        {
                            sumaAnticiposVentasACredito = ClsDocumentModel.getAllTotalForAFormaDePagoAbono(formasList[i].FORMA_COBRO_CC_ID);
                            //sumaCambio = ClsFormasDeCobroDocumentoModel.getSumCambioForAFormaDePagoInReporteDeCorte(formasList[i].FORMA_COBRO_CC_ID);
                            sumasDeImporte = sumaAnticiposVentasACredito;
                            totalVendidoYCobrado += sumasDeImporte;
                            //sumasDeImporte -= sumaCambio;
                            if (formasList[i].FORMA_COBRO_CC_ID <= 9)
                            {
                                formatototales += "> 0" + formasList[i].FORMA_COBRO_CC_ID + " " + formasList[i].NOMBRE + " = $" +
                                        ClsMetodosGenerales.obtieneDosDecimales(sumasDeImporte) + " MXN\n";
                            }
                            else
                            {
                                formatototales += "> " + formasList[i].FORMA_COBRO_CC_ID + " " + formasList[i].NOMBRE + " = $" +
                                        ClsMetodosGenerales.obtieneDosDecimales(sumasDeImporte) + " MXN\n";
                            }
                            sumaCambio = 0;
                            sumaAnticiposVentasACredito = 0;
                            sumasDeImporte = 0;
                        }
                    }
                }
            });
            if (creditosDelTurnoList != null && creditosDelTurnoList.Count > 0)
            {
                String query = "SELECT sum(d." + LocalDatabase.CAMPO_TOTAL_DOC + ") FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " d " +
                    " INNER JOIN " + LocalDatabase.TABLA_CLIENTES + " c ON d." + LocalDatabase.CAMPO_CLIENTEID_DOC + " = c." +
                    LocalDatabase.CAMPO_ID_CLIENTE + " WHERE d." + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 2 + " AND d." +
                    LocalDatabase.CAMPO_CANCELADO_DOC + " = " + 0 + " AND d." + LocalDatabase.CAMPO_PAUSAR_DOC + " = " + 0 +
                    " AND d." + LocalDatabase.CAMPO_FORMACOBROID_DOC + " = " + 71;
                double totalACredito = ClsDocumentModel.getDoubleValue(query);
                textTotalAbonadoCredito.Text = "Total a Crédito: $" + ClsMetodosGenerales.obtieneDosDecimales(totalACredito) + " MXN \n" +
                    "Abonos a Créditos \n"+ formatototales;
            }
            Cursor.Current = Cursors.Default;
        }*/

        /*private async Task fillTotalesVentasDelTurno()
        {
            Cursor.Current = Cursors.Default;
            double contadoTotal = 0;
            await Task.Run(() => {
                String query = "SELECT SUM(D." + LocalDatabase.CAMPO_TOTAL_DOC + ") FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " D " +
                    " INNER JOIN " + LocalDatabase.TABLA_CLIENTES + " C ON D." + LocalDatabase.CAMPO_CLIENTEID_DOC + " = C." +
                    LocalDatabase.CAMPO_ID_CLIENTE +
                    " WHERE (D." + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 2 + " OR D."+ LocalDatabase.CAMPO_TIPODOCUMENTO_DOC +" = "+4+ ") AND D." +
                    LocalDatabase.CAMPO_CANCELADO_DOC + " = " + 0 + " AND D." + LocalDatabase.CAMPO_PAUSAR_DOC + " = " + 0 +
                    " AND (D." + LocalDatabase.CAMPO_FORMACOBROID_DOC + " != " + 71 + " AND D." + LocalDatabase.CAMPO_FORMACOBROID_DOC + " != " + 0 + ")";
                contadoTotal = ClsDocumentModel.getDoubleValue(query);
                totalVendidoYCobrado += contadoTotal;
            });
            if (textTotalVendido.Equals(""))
                textTotalVendido.Text = ("Vendido $ 0 MXN");
            else textTotalVendido.Text = ("Total al Contado: $" + ClsMetodosGenerales.obtieneDosDecimales(contadoTotal) + " MXN");
            Cursor.Current = Cursors.Default;
        }*/

        private async Task fillAbonosCxc() {
            double totalAbono = 0;
            String query = "SELECT SUM(" + LocalDatabase.CAMPO_ANTICIPO_DOC + ") FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                        LocalDatabase.CAMPO_FORMACOBROID_DOC + " = " + 71 + " AND " + LocalDatabase.CAMPO_ANTICIPO_DOC + " > " + 0 +
                        " AND " + LocalDatabase.CAMPO_CANCELADO_DOC + " = " + 0 + " AND " + LocalDatabase.CAMPO_PAUSAR_DOC + " = " + 0;
            double abonoDocs = 0;
            abonoDocs = DocumentModel.getDoubleValue(query);
            String query1 = "SELECT SUM(" + LocalDatabase.CAMPO_ABONO_CXC + ") FROM " + LocalDatabase.TABLA_CXC + " WHERE " +
                    LocalDatabase.CAMPO_TIPO_CXC + " = " + 2;
            double abonoCxc = 0;
            abonoCxc = DocumentModel.getDoubleValue(query1);
            totalAbono = abonoDocs + abonoCxc;
        }

        /*private async Task fillTotalesPorFormasDeCobro()
        {
            String formatototales = "";
            List<ClsFormasDeCobroModel> formasList = null;
            await Task.Run(() => {
                formasList = ClsFormasDeCobroModel.getAllFormasDeCobro();
                double sumaTXT = 0;
                double sumaCambio = 0;
                double sumaDos = 0;
                double sumasDeImporte = 0.0;
                if (formasList != null)
                {
                    for (int i = 0; i < formasList.Count; i++)
                    {
                        if (formasList[i].FORMA_COBRO_CC_ID != 71)
                        {
                            sumaTXT = ClsFormasDeCobroDocumentoModel.getAllTotalForAFormaDePagoInReporteDeCorte(formasList[i].FORMA_COBRO_CC_ID);
                            sumaCambio = ClsFormasDeCobroDocumentoModel.getSumCambioForAFormaDePagoInReporteDeCorte(formasList[i].FORMA_COBRO_CC_ID);
                            sumaDos = ClsCuentasXCobrarModel.getAllTotalForAFormaDePagoAbono(formasList[i].FORMA_COBRO_CC_ID);
                            sumasDeImporte = sumaTXT + sumaDos;
                            sumasDeImporte -= sumaCambio;
                            if (formasList[i].FORMA_COBRO_CC_ID <= 9)
                            {
                                formatototales += "> 0" + formasList[i].FORMA_COBRO_CC_ID + " " + formasList[i].NOMBRE + " = $" +
                                        ClsMetodosGenerales.obtieneDosDecimales(sumasDeImporte) + " MXN\n";
                            }
                            else
                            {
                                formatototales += "> " + formasList[i].FORMA_COBRO_CC_ID + " " + formasList[i].NOMBRE + " = $" +
                                        ClsMetodosGenerales.obtieneDosDecimales(sumasDeImporte) + " MXN\n";
                            }
                            sumaTXT = 0;
                            sumaCambio = 0;
                            sumaDos = 0;
                            sumasDeImporte = 0;
                        }
                    }
                }
            });
            //textInfoTotaltesPorFP.setText("Totales Por Formas de Cobro");
            //contadoT.setText(formatototales);
            textTotalVendido.Text += "\n" + "Totales Por Formas de Cobro" + "\n" + formatototales;
        }*/

        /*private async Task fillTotaltesRetiros()
        {
            Cursor.Current = Cursors.Default;
            String textTotalRetiradoDesc = "";
            double sobrante = 0;
            double totalRetirado = 0;
            List<ClsFormasDeCobroModel> fcList = null;
            await Task.Run(() => {
                fcList = ClsFormasDeCobroModel.getAllFormasDeCobro();
                String textFaltanteDesc = "";
                double totalFCRetiro = 0;
                if (fcList != null)
                {
                    for (int i = 0; i < fcList.Count; i++)
                    {
                        totalFCRetiro = ClsMontoRetiroModel.getAllWithdrawalAmountsFromACollectionForm(fcList[i].FORMA_COBRO_CC_ID);
                        if (totalFCRetiro > 0)
                        {
                            textTotalRetiradoDesc += ">" + fcList[i].NOMBRE + " $" + ClsMetodosGenerales.obtieneDosDecimales(totalFCRetiro) + "MXN\n";
                            totalRetirado += totalFCRetiro;
                        }
                    }
                    sobrante = totalVendidoYCobrado - totalRetirado;
                }
            });
            double faltante = sobrante;
            if (faltante < 0)
            {
                textFaltante.Text = ("Faltante $ 0 MXN");
                textSobrante.Text = ("Sobrante $ " + ClsMetodosGenerales.obtieneDosDecimales(Math.Abs(faltante)) + " MXN");
            }
            else if (faltante == 0)
            {
                textFaltante.Text = ("Faltante $ 0 MXN");
                textSobrante.Text = ("Sobrante $ 0 MXN");
            }
            else
            {
                textFaltante.Text = ("Faltante $ " + ClsMetodosGenerales.obtieneDosDecimales(Math.Abs(faltante)) + " MXN");
                textSobrante.Text = ("Sobrante $ 0 MXN");
            }
            if (textTotalRetirado.Equals(""))
                textTotalRetirado.Text = ("Retirado $ 0 MXN");
            else textTotalRetirado.Text = (textTotalRetiradoDesc + "\nTotal Retirado: $" + ClsMetodosGenerales.obtieneDosDecimales(totalRetirado) + " MXN");
            Cursor.Current = Cursors.Default;
        }*/

        /*private void showOrHideDateAndUsers(bool state)
        {
            Cursor.Current = Cursors.WaitCursor;
            textInfoSeleccionarUser.Visible = state;
            comboBoxSeleccionarUsuario.Visible = state;
            textInfoStartDate.Visible = state;
            imgStartDate.Visible = state;
            dateTimeStartDate.Visible = state;
            textInfoEndDate.Visible = state;
            imgEndDate.Visible = state;
            dateTimeEndDate.Visible = state;
            Cursor.Current = Cursors.Default;
        }*/

        /*private async Task fillDataGridRetiros()
        {
            DataTable dt = await getAllWithdrawals();
            if (dt != null)
            {
                dataGridViewCortesDeCaja.DataSource = null;
                if (dt.Rows.Count > 0)
                {
                    DataTable dtCloned = dt.Clone();
                    //dtCloned.Columns[1].DataType = typeof(Image);
                    dtCloned.Columns[0].DataType = typeof(String);
                    dtCloned.Columns[3].DataType = typeof(String);
                    //dtCloned.Columns[5].ColumnName = "Estatus";
                    dtCloned.Columns.Add("Importes", typeof(String));
                    dtCloned.Columns.Add("Total", typeof(String));
                    foreach (DataRow row in dt.Rows) { dtCloned.ImportRow(row); }
                    dtCloned.AsEnumerable().ToList<DataRow>().ForEach(r =>
                    {
                        String amounts = "";
                        List<ClsMontoRetiroModel> amountsList = ClsMontoRetiroModel.getAllAmountsFromAWithdrawal(Convert.ToInt32(r["id"].ToString()));
                        if (amountsList != null)
                        {
                            for (int i = 0; i < amountsList.Count; i++)
                            {
                                String fcName = ClsFormasDeCobroModel.getANameFrromAFomaDeCobroWithId(amountsList[i].formaCobroId);
                                if (i == (amountsList.Count - 1))
                                    amounts += fcName + " $ " + ClsMetodosGenerales.obtieneDosDecimales(amountsList[i].importe) + " MXN";
                                else amounts += fcName + " $ " + ClsMetodosGenerales.obtieneDosDecimales(amountsList[i].importe) + " MXN\r\n";
                            }
                        }
                        else
                        {
                            amounts = "Sin montos retirados";
                        }
                        r["Importes"] = amounts;
                        if (r["Estatus"].ToString().Equals("1"))
                        {
                            r["Estatus"] = "Enviado";
                        }
                        else
                        {
                            r["Estatus"] = "Local";
                        }
                        double totalAmounts = ClsMontoRetiroModel.getTotalOfAWithdrawal(Convert.ToInt32(r["id"].ToString()));
                        r["Total"] = " $ " + ClsMetodosGenerales.obtieneDosDecimales(totalAmounts) + " MXN";
                    }
                    );
                    dataGridViewCortesDeCaja.DataSource = dtCloned;
                    dataGridViewCortesDeCaja.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    dataGridViewCortesDeCaja.Columns["id"].Visible = false;
                    imgSinDatosRetiros.Visible = false;
                    btnGenerarPdfReporteRetiros.Enabled = true;
                }
                else
                {
                    imgSinDatosRetiros.Visible = true;
                    dataGridViewCortesDeCaja.DataSource = null;
                }
            }
            else
            {
                imgSinDatosRetiros.Visible = true;
                dataGridViewCortesDeCaja.DataSource = null;
            }
        }*/

        /*private async Task<DataTable> getAllWithdrawals()
        {
            DataTable dt = null;
            await Task.Run(() =>
            {
                String query = "SELECT " + LocalDatabase.CAMPO_ID_RETIRO + ", " + LocalDatabase.CAMPO_NUMERO_RETIRO + " AS 'Número De Retiro', " + LocalDatabase.CAMPO_FECHAHORA_RETIRO + " AS Fecha, " +
                LocalDatabase.CAMPO_ENVIADO_RETIRO + " AS Estatus FROM " + LocalDatabase.TABLA_RETIROS + " WHERE " +
                    LocalDatabase.CAMPO_FECHAHORA_RETIRO + " BETWEEN '" + FechaInicial + " 00:00:00' AND '" +
                    FechaFinal + " 23:59:59' AND " + LocalDatabase.CAMPO_IDUSUARIO_RETIRO + " = " + userId;
                dt = RetiroModel.getAllWithdrawalsDt(query);
            });
            return dt;
        }*/

        /*private async Task fillDataGridCreditosReporteCaja()
        {
            gridScrollBarsCreditosDelTurno = dataGridViewCreditosReporteCaja.ScrollBars;
            lastLoadingCreditosDelTurno = DateTime.Now;
            creditosDelTurnoListTemp = await getAllCreditsTurn();
            if (creditosDelTurnoListTemp != null && creditosDelTurnoListTemp.Count > 0)
            {
                progressCreditosDelTurno += creditosDelTurnoListTemp.Count;
                creditosDelTurnoList.AddRange(creditosDelTurnoListTemp);
                for (int i = 0; i < creditosDelTurnoListTemp.Count; i++)
                {
                    int n = dataGridViewCreditosReporteCaja.Rows.Add();
                    if (creditosDelTurnoListTemp[i].estado == 1)
                        dataGridViewCreditosReporteCaja.Rows[n].DefaultCellStyle.BackColor = Color.Red;
                    else dataGridViewCreditosReporteCaja.Rows[n].DefaultCellStyle.BackColor = Color.White;
                    dataGridViewCreditosReporteCaja.Rows[n].Cells[0].Value = creditosDelTurnoListTemp[i].id + "";
                    dataGridViewCreditosReporteCaja.Columns["idCredito"].Visible = false;
                    String query = "SELECT " + LocalDatabase.CAMPO_NOMBRECLIENTE + " FROM " + LocalDatabase.TABLA_CLIENTES +
                        " WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " = " + creditosDelTurnoListTemp[i].cliente_id;
                    String customerName = ClsCustomerModel.getAStringValueFromACustomer(query);
                    dataGridViewCreditosReporteCaja.Rows[n].Cells[1].Value = customerName;
                    dataGridViewCreditosReporteCaja.Rows[n].Cells[2].Value = creditosDelTurnoListTemp[i].fventa;
                    dataGridViewCreditosReporteCaja.Rows[n].Cells[3].Value = "$ " + ClsMetodosGenerales.obtieneDosDecimales(creditosDelTurnoListTemp[i].descuento) + " MXN";
                    dataGridViewCreditosReporteCaja.Rows[n].Cells[4].Value = "$ " + ClsMetodosGenerales.obtieneDosDecimales(creditosDelTurnoListTemp[i].total) + " MXN";
                }
                dataGridViewCreditosReporteCaja.PerformLayout();
                creditosDelTurnoListTemp.Clear();
                lastIdCreditosDelTurno = Convert.ToInt32(creditosDelTurnoList[creditosDelTurnoList.Count - 1].id);
                imgSinDatosCreditosDelTurno.Visible = false;
            }
            else
            {
                if (progressCreditosDelTurno == 0)
                    imgSinDatosCreditosDelTurno.Visible = true;
            }
            textCreditosDelTurno.Text = "Créditos del Turno: " + totalCreditosDelTurno.ToString().Trim();
            //reset displayed row
            if (firstVisibleRowCreditosDelTurno > -1)
            {
                dataGridViewCreditosReporteCaja.ScrollBars = gridScrollBarsCreditosDelTurno;
                if (creditosDelTurnoList.Count > 0)
                    dataGridViewCreditosReporteCaja.FirstDisplayedScrollingRowIndex = firstVisibleRowCreditosDelTurno;
                imgSinDatosCreditosDelTurno.Visible = false;
            }
        }*/

        /*private async Task<List<ClsDocumentModel>> getAllCreditsTurn()
        {
            List<ClsDocumentModel> documentsList = null;
            await Task.Run(async () =>
            {
                if (queryTypeVentasDelTurno == 0)
                {
                    queryCreditosDelTurno = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE (" + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 2 + " OR " +
                        LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 4 + ") AND " +
                        LocalDatabase.CAMPO_PAUSAR_DOC + " = " + 0 +" AND "+LocalDatabase.CAMPO_FORMACOBROID_DOC+" = "+71+
                        " AND " + LocalDatabase.CAMPO_ID_DOC + " > " + lastIdVentasDelTurno + " ORDER BY " +
                        LocalDatabase.CAMPO_ID_DOC + " LIMIT " + LIMIT;
                    queryTotalesCreditosDelTurno = "SELECT COUNT(id) FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA +
                        " WHERE (" + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 2 + " OR " +
                        LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 4 + ") AND " +
                        LocalDatabase.CAMPO_PAUSAR_DOC + " = " + 0+" AND " + LocalDatabase.CAMPO_FORMACOBROID_DOC + " = " + 71;
                }
                totalCreditosDelTurno = ClsDocumentModel.getIntValue(queryTotalesCreditosDelTurno);
                documentsList = ClsDocumentModel.getAllDocuments(queryCreditosDelTurno);
            });
            return documentsList;
        }*/

        /*private void dataGridViewCreditosReporteCaja_Scroll(object sender, ScrollEventArgs e)
        {
            if (creditosDelTurnoList.Count < totalCreditosDelTurno && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
                {
                    if (e.NewValue > dataGridViewCreditosReporteCaja.Rows.Count - getDisplayedRowsCountCreditosDelTurno())
                    {
                        //prevent loading from autoscroll.
                        TimeSpan ts = DateTime.Now - lastLoadingCreditosDelTurno;
                        if (ts.TotalMilliseconds > 100)
                        {
                            firstVisibleRowCreditosDelTurno = e.NewValue;
                            //dataGridItems.PerformLayout();
                            fillDataGridCreditosReporteCaja();
                        }
                        else
                        {
                            dataGridViewCreditosReporteCaja.FirstDisplayedScrollingRowIndex = e.OldValue;
                        }
                    }
                }
            }
        }*/

        /*private int getDisplayedRowsCountCreditosDelTurno()
        {
            int count = dataGridViewCreditosReporteCaja.Rows[dataGridViewCreditosReporteCaja.FirstDisplayedScrollingRowIndex].Height;
            count = dataGridViewCreditosReporteCaja.Height / count;
            return count;
        }*/

        /*private async Task fillDataGridVentasReporteCaja()
        {
            gridScrollBarsVentasDelTurno = dataGridViewVentasReporteCaja.ScrollBars;
            lastLoadingVentasDelTurno = DateTime.Now;
            ventasDelTurnoListTemp = await getAllSales();
            if (ventasDelTurnoListTemp != null && ventasDelTurnoListTemp.Count > 0)
            {
                progressVentasDelTurno += ventasDelTurnoListTemp.Count;
                ventasDelTurnoList.AddRange(ventasDelTurnoListTemp);
                for (int i = 0; i < ventasDelTurnoListTemp.Count; i++)
                {
                    int n = dataGridViewVentasReporteCaja.Rows.Add();
                    if (ventasDelTurnoListTemp[i].estado == 1)
                        dataGridViewVentasReporteCaja.Rows[n].DefaultCellStyle.BackColor = Color.Red;
                    else dataGridViewVentasReporteCaja.Rows[n].DefaultCellStyle.BackColor = Color.White;
                    dataGridViewVentasReporteCaja.Rows[n].Cells[0].Value = ventasDelTurnoListTemp[i].id + "";
                    dataGridViewVentasReporteCaja.Columns["id"].Visible = false;
                    String query = "SELECT " + LocalDatabase.CAMPO_NOMBRECLIENTE + " FROM " + LocalDatabase.TABLA_CLIENTES +
                        " WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " = " + ventasDelTurnoListTemp[i].cliente_id;
                    String customerName = ClsCustomerModel.getAStringValueFromACustomer(query);
                    dataGridViewVentasReporteCaja.Rows[n].Cells[1].Value = customerName;
                    dataGridViewVentasReporteCaja.Rows[n].Cells[2].Value = "$ " + ClsMetodosGenerales.obtieneDosDecimales(ventasDelTurnoListTemp[i].descuento) + " MXN";
                    dataGridViewVentasReporteCaja.Rows[n].Cells[3].Value = "$ " + ClsMetodosGenerales.obtieneDosDecimales(ventasDelTurnoListTemp[i].total) + " MXN";
                }
                dataGridViewVentasReporteCaja.PerformLayout();
                ventasDelTurnoListTemp.Clear();
                lastIdVentasDelTurno = Convert.ToInt32(ventasDelTurnoList[ventasDelTurnoList.Count - 1].id);
                imgSinDatosSalesRepCaja.Visible = false;
            }
            else
            {
                if (progressVentasDelTurno == 0)
                    imgSinDatosSalesRepCaja.Visible = true;
            }
            textVentasDelTurno.Text = "Ventas del Turno: " + totalVentasDelTurno.ToString().Trim();
            //reset displayed row
            if (firstVisibleRowVentasDelTurno > -1)
            {
                dataGridViewVentasReporteCaja.ScrollBars = gridScrollBarsVentasDelTurno;
                if (ventasDelTurnoList.Count > 0)
                    dataGridViewVentasReporteCaja.FirstDisplayedScrollingRowIndex = firstVisibleRowVentasDelTurno;
                imgSinDatosSalesRepCaja.Visible = false;
            }
        }*/

        /*private async Task<List<ClsDocumentModel>> getAllSales()
        {
            List<ClsDocumentModel> documentsList = null;
            await Task.Run(async () =>
            {
                if (queryTypeVentasDelTurno == 0)
                {
                    queryVentasDelTurno = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE (" + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 2 + " OR " +
                        LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 4 + ") AND " +
                        LocalDatabase.CAMPO_PAUSAR_DOC + " = " + 0 + " AND "+LocalDatabase.CAMPO_FORMACOBROID_DOC+" != "+71+
                        " AND " + LocalDatabase.CAMPO_ID_DOC + " > " + lastIdVentasDelTurno + " ORDER BY " +
                        LocalDatabase.CAMPO_ID_DOC + " LIMIT " + LIMIT;
                    queryTotalesVentasDelTurno = "SELECT COUNT(id) FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA +
                        " WHERE (" + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 2 + " OR " +
                        LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 4 + ") AND " +
                        LocalDatabase.CAMPO_PAUSAR_DOC + " = " + 0+ " AND " + LocalDatabase.CAMPO_FORMACOBROID_DOC + " != " + 71;
                }
                totalVentasDelTurno = ClsDocumentModel.getIntValue(queryTotalesVentasDelTurno);
                documentsList = ClsDocumentModel.getAllDocuments(queryVentasDelTurno);
            });
            return documentsList;
        }*/

        private void btnGenerarPdfRetiros_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridViewDocumentos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                //int idDocument = Convert.ToInt32(dataGridViewDocumentos.Rows[e.RowIndex].Cells["idDocumento"].Value.ToString().Trim());
                //FrmMovimientos frmMovimientos = new FrmMovimientos(idDocument, FrmMovimientos.TIPO_MOVDOC);
                //frmMovimientos.ShowDialog();
            }
        }

        /*private void dataGridViewVentasReporteCaja_Scroll(object sender, ScrollEventArgs e)
        {
            if (ventasDelTurnoList.Count < totalVentasDelTurno)
            {
                if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
                {
                    if (e.NewValue > dataGridViewVentasReporteCaja.Rows.Count - getDisplayedRowsCountVentasDelTurno())
                    {
                        //prevent loading from autoscroll.
                        TimeSpan ts = DateTime.Now - lastLoadingVentasDelTurno;
                        if (ts.TotalMilliseconds > 100)
                        {
                            firstVisibleRowVentasDelTurno = e.NewValue;
                            //dataGridItems.PerformLayout();
                            fillDataGridVentasReporteCaja();
                        }
                        else
                        {
                            dataGridViewVentasReporteCaja.FirstDisplayedScrollingRowIndex = e.OldValue;
                        }
                    }
                }
            }
        }*/

        /*private int getDisplayedRowsCountVentasDelTurno()
        {
            int count = dataGridViewVentasReporteCaja.Rows[dataGridViewVentasReporteCaja.FirstDisplayedScrollingRowIndex].Height;
            count = dataGridViewVentasReporteCaja.Height / count;
            return count;
        }*/

        /*private async Task fillDataGridRetiradoReporteCaja()
        {
            DataTable dt = await getAllWithdrawalsRepCaja();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    DataTable dtCloned = dt.Clone();
                    //dtCloned.Columns[1].SetOrdinal(3);
                    //dtCloned.Columns[0].DataType = typeof(String);
                    dtCloned.Columns.Add("Usuario", typeof(String));
                    dtCloned.Columns.Add("Total", typeof(String));
                    foreach (DataRow row in dt.Rows) { dtCloned.ImportRow(row); }
                    dtCloned.AsEnumerable().ToList<DataRow>().ForEach(r =>
                    {
                        r["Usuario"] = ClsUsersModel.getAStringValueForAnyUser("SELECT " + LocalDatabase.CAMPO_NOMBRE_USER + " FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " +
                            LocalDatabase.CAMPO_ID_USUARIO + " = " + ClsRegeditController.getIdUserInTurn());
                        double total = ClsMontoRetiroModel.getTotalOfAWithdrawal(Convert.ToInt32(r["id"].ToString()));
                        r["Total"] = "$ " + ClsMetodosGenerales.obtieneDosDecimales(total) + " MXN";
                    }
                    );
                    dataGirdViewRetiradoReporteCaja.DataSource = dtCloned;
                    dataGirdViewRetiradoReporteCaja.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    dataGirdViewRetiradoReporteCaja.Columns["id"].Visible = false;
                    imgSinDatosRetiradoRepCaja.Visible = false;
                }
                else
                {
                    imgSinDatosRetiradoRepCaja.Visible = true;
                }
            }
            else
            {
                imgSinDatosRetiradoRepCaja.Visible = true;
            }
        }*/

        private void dateTimeStartDate_ValueChanged(object sender, EventArgs e)
        {
            var um = (UserModel)comboBoxSeleccionarUsuario.SelectedItem;
            if (um != null)
            {
                /*FechaInicial = (dateTimeStartDate.Value).ToString("yyyy-MM-dd HH:mm:ss");
                if (contadorBanderaFecha > 1)
                {
                    FechaInicial = (dateTimeStartDate.Value).ToString("yyyy-MM-dd");
                    //FechaInicial += " 00:00:00";  //00:00:00 a. m
                }*/
                FechaInicial = (dateTimeStartDate.Value).ToString("yyyy-MM-dd");
                FechaFinal = (dateTimeEndDate.Value).ToString("yyyy-MM-dd");
                //FechaFinal += " 23:59:59";
                userId = um.id;
                tabControl1_SelectedIndexChanged(sender, e);
                PopupNotifier popup = new PopupNotifier();
                popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.caution, 100, 100);
                popup.TitleColor = Color.Blue;
                popup.TitleText = "Datos";
                popup.ContentText = "Documentos de " + um.Nombre + " por fecha";
                popup.ContentColor = Color.Red;
                popup.Popup();
            }
        }

        private void dateTimeEndDate_ValueChanged(object sender, EventArgs e)
        {
            var um = (UserModel)comboBoxSeleccionarUsuario.SelectedItem;
            if (um != null)
            {
                FechaInicial = (dateTimeStartDate.Value).ToString("yyyy-MM-dd");
                FechaFinal = (dateTimeEndDate.Value).ToString("yyyy-MM-dd");
                //FechaFinal += " 23:59:59";
                userId = um.id;
                tabControl1_SelectedIndexChanged(sender, e);
                PopupNotifier popup = new PopupNotifier();
                popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.caution, 100, 100);
                popup.TitleColor = Color.Red;
                popup.TitleText = "Datos";
                popup.ContentText = "Documentos de " + um.Nombre + " por fecha";
                popup.ContentColor = Color.Red;
                popup.Popup();
            }
        }

        private void frmCorteReporte_FormClosed(object sender, FormClosedEventArgs e)
        {
            /*resetVariablesDocumentosAnteriores();
            resetVariablesVentasDelTurno();
            callDeleteDocumentsProcess(false);
            callDeleteWithdrawalsProcess(false);*/
        }

        /*private void resetVariablesCreditosDelTurno()
        {
            queryTypeCreditosDelTurno = 0;
            queryCreditosDelTurno = "";
            queryTotalesCreditosDelTurno = "";
            totalCreditosDelTurno = 0;
            lastIdCreditosDelTurno = 0;
            progressCreditosDelTurno = 0;
            creditosDelTurnoList.Clear();
            dataGridViewCreditosReporteCaja.Rows.Clear();
        }*/

        /*private void resetVariablesVentasDelTurno()
        {
            queryTypeVentasDelTurno = 0;
            queryVentasDelTurno = "";
            queryTotalesVentasDelTurno = "";
            totalVentasDelTurno = 0;
            lastIdVentasDelTurno = 0;
            progressVentasDelTurno = 0;
            ventasDelTurnoList.Clear();
            dataGridViewVentasReporteCaja.Rows.Clear();
        }*/

        /*private void resetVariablesDocumentosAnteriores()
        {
            queryTypeVentasAnteriores = 0;
            queryDocumentosAnteriores = "";
            queryTotalesDocumentosAnteriores = "";
            totalDocumentosAnteriores = 0;
            lastIdDocumentosAnteriores = 0;
            progressDocumentosAnteriores = 0;
            documentosAnterioresList.Clear();
            dataGridViewDocumentos.Rows.Clear();
        }*/

        private void btnGenerarPdfReporteDocumentos_Click(object sender, EventArgs e)
        {
            PopupNotifier popup = new PopupNotifier();
            popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.caution, 100, 100);
            popup.TitleColor = Color.Red;
            popup.TitleText = "Proceso Iniciado";
            popup.ContentText = "Generando PDF";
            popup.ContentColor = Color.Red;
            popup.Popup();
            //generatePDfDocuments(dataGridViewDocumentos, "Documentos");
        }

        /*private async Task generatePDfDocuments(DataGridView dt, String nameReport)
        {
            SECUDOC.writeLog("Generando documento inicio evento");
            Cursor.Current = Cursors.WaitCursor;
            ClsPdfMethods cpdfm = new ClsPdfMethods();
            int response = await cpdfm.createPdf(dt, nameReport, userId, permissionPrepedido);
            SECUDOC.writeLog("Generando documento después de llamar al método");
            String folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (response == 1)
            {
                PopupNotifier popup = new PopupNotifier();
                popup.Image = ClsMetodosGenerales.redimencionarImagenes(Properties.Resources.caution, 100, 100);
                popup.TitleColor = Color.Red;
                popup.TitleText = "Proceso Finalizado";
                popup.ContentText = "Se generó un reporte a formato PDF en "+folderPath + "\\PDFsTPV\\Reportes";
                popup.ContentColor = Color.Red;
                popup.Popup();
            }
            else
            {
                PopupNotifier popup = new PopupNotifier();
                popup.Image = ClsMetodosGenerales.redimencionarImagenes(Properties.Resources.caution, 100, 100);
                popup.TitleColor = Color.Red;
                popup.TitleText = "Proceso Finalizado";
                popup.ContentText = "Validar si se generó el documento PDF en " + folderPath + "\\PDFsTPV\\Reportes";
                popup.ContentColor = Color.Red;
                popup.Popup();
            }
            btnGenerarPdfReporteDocumentos.Enabled = true;
            Cursor.Current = Cursors.Default;
        }*/

        private void btnGenerarPdfReporteRetiros_Click(object sender, EventArgs e)
        {
            //generatePDfDocuments(dataGridViewCortesDeCaja, "Retiros");
        }

        private void btnImprimirCorteDeCaja_Click(object sender, EventArgs e)
        {
            clsTicket Ticket = new clsTicket();
            //Ticket.createAndPrintReporteCajaTicket(serverModeLAN);
            PopupNotifier popup = new PopupNotifier();
            popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.caution, 100, 100);
            popup.TitleColor = Color.Red;
            popup.TitleText = "Imprimiendo";
            popup.ContentText = "Procesando información...";
            popup.ContentColor = Color.Blue;
            popup.Popup();
        }

    }
}
