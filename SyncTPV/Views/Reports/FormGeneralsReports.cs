using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;
using wsROMClases;
using static ClsDocumentoModel;

namespace SyncTPV.Views.Reports
{
    public partial class FormGeneralsReports : Form
    {
        private int reporteCajaActivated = 0;
        double totalVendidoYCobrado = 0;
        public static int userId = 0;
        int Error = 0;
        string FechaInicial = "", FechaFinal = "", MensajeError = "";
        int contadorBanderaFecha = 0;
        private static int lastIdCreditsTurno = 0, lastIdVentasTurno = 0, lastIdEITurno = 0,
            lastIdDocumentosAnteriores = 0, lastIdEIAnteriores = 0;
        FormWaiting frmWaiting;
        /* Todo lo relacionado a la Carga de Créditos*/
        public int LIMIT = 30;
        private int progressCreditosDelTurno = 0, progressVentasDelTurno = 0, progressEITurno = 0,
            progressDocumentosAnteriores = 0, progressEIAnteriores = 0;
        private DateTime lastLoadingCreditosDelTurno, lastLoadingVentasDelTurno, lastLoadingEITurno,
            lastLoadingDocumentosAnteriores, lastLoadingEIAnteriores;
        private int firstVisibleRowCreditosDelTurno, firstVisibleRowVentasDelTurno,
            firstVisibleRowEITurno, firstVisibleRowDocumentosAnteriores,
            firstVisibleRowEIAnteriores;
        private ScrollBars gridScrollBarsCreditosDelTurno, gridScrollBarsVentasDelTurno,
            gridScrollBarsEITurno, gridScrollBarsDocumentosAnteriores, gridScrollBarsEIAnteriores;
        private int totalCreditosDelTurno = 0, totalVentasDelTurno = 0, totalEITurno = 0,
            totalDocumentosAnteriores = 0, totalEIAnteriores = 0,
            queryTypeCreditosDelTurno = 0, queryTypeVentasDelTurno = 0, queryTypeEITurno = 0,
            queryTypeDocumentosAnteriores = 0, queryTypeEIAnteriores = 0;
        private String queryCreditosDelTurno = "", queryTotalesCreditosDelTurno = "",
            queryVentasDelTurno = "", queryTotalesVentasDelTurno = "",
            queryEITurno, queryTotalEITurno;
        private List<DocumentModel> creditosDelTurnoList;
        private List<DocumentModel> creditosDelTurnoListTemp;
        private List<DocumentModel> ventasDelTurnoList;
        private List<DocumentModel> ventasDelTurnoListTemp;
        private List<RetiroModel> egresosTurnoList;
        private List<RetiroModel> egresosTurnoListTemp;
        private List<ClsRetirosModel> egresosList;
        private List<ClsRetirosModel> egresosListTemp;
        private List<IngresoModel> ingresoTurnoList;
        private List<IngresoModel> ingresoTurnoListTemp;
        private String queryDocumentosAnterioresTemp = "", queryDocumentosAnterioresReport = "", queryTotalesDocumentosAnteriores = "",
            queryEIAnterioresTemp = "", queryEIAnterioresReport = "", queryTotalesEIAnteriores = "";
        private List<DocumentoModel> documentosAnterioresList;
        private List<DocumentoModel> documentosAnterioresListTemp;
        private bool permissionPrepedido = false, egresosTurnoListActivated = true;
        private double totalDeApertura = 0;
        private bool serverModeLAN = false;
        private String searchWordPrepedidos = "";
        private DateTime fechainicial = DateTime.Now;
        private DateTime fechafinal = DateTime.Now;
        private bool BDlocal = false;
        public FormGeneralsReports(String searchWordPrepedidos)
        {
            InitializeComponent();
            documentosAnterioresList = new List<DocumentoModel>();
            creditosDelTurnoList = new List<DocumentModel>();
            ventasDelTurnoList = new List<DocumentModel>();
            egresosTurnoList = new List<RetiroModel>();
            egresosList = new List<ClsRetirosModel>();
            ingresoTurnoList = new List<IngresoModel>();
            btnClose.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.back_white, 40, 40);
            permissionPrepedido = UserModel.doYouHavePermissionPrepedido();
            serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
            this.searchWordPrepedidos = searchWordPrepedidos;
        }

        private void FormGeneralsReports_Load(object sender, EventArgs e)
        {
            toolTipLimite.SetToolTip(ComboBoxLimite, "Sin limite: 0");
            fillComboUsers();
            ComboBoxLimite.Text = LIMIT.ToString();
            comboBoxSelectEI.Items.Add("Retiros");
            comboBoxSelectEI.Items.Add("Ingresos");
            totalDeApertura = AperturaTurnoModel.getImporteDeAperturaActual();
            editImporteApertura.Text = "Importe de Apertura: "+totalDeApertura.ToString("C") + " MXN";
            //checkBoxTipoDeBusqueda.Visible = false;
            string cargaInicial = "";
            if (cargaInicial == "" || cargaInicial == null) { }
            else { 
                dateTimePickerStart.Value = Convert.ToDateTime(cargaInicial);
            }
            validarPermisoPrepedido();
        }

        private async Task fillComboUsers()
        {
            List<UserModel> usersList = await getAllUsers();
            if (usersList != null)
            {
                //Setup data binding
                this.comboBoxSelectUser.DataSource = usersList;
                this.comboBoxSelectUser.ValueMember = "id";
                this.comboBoxSelectUser.DisplayMember = "Nombre";
                // make it readonly
                this.comboBoxSelectUser.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        private async Task<List<UserModel>> getAllUsers()
        {
            List<UserModel> usersList = null;
            await Task.Run(async () =>
            {
                if (LicenseModel.isItTPVLicense())
                    usersList = UserModel.getAllTellers();
                else usersList = UserModel.getAllAgents();
            });
            return usersList;
        }

        private void comboBoxSelectEI_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSelectEI.SelectedIndex == 0)
            {
                egresosTurnoListActivated = true;
                resetVariablesEIDelTurno(0);
                fillDataGridRetiradoReporteCaja();
                fillTotaltesRetiros();
            }
            else { 
                egresosTurnoListActivated = false;
                resetVariablesEIDelTurno(0);
                fillDataGridIngresosReporteCaja();
                fillTotaltesRetiros();
            }
        }

        private async Task validarPermisoPrepedido()
        {
            if (UserModel.doYouHavePermissionPrepedido())
            {
                tabControlReportes.SelectedIndex = 0;
                tabControlReportes.TabPages.Remove(tabControlReportes.TabPages[2]);
                tabControlReportes.TabPages.Remove(tabControlReportes.TabPages[1]);
                comboBoxSelectUser.Font = new Font(comboBoxSelectUser.Font.FontFamily, 20);
                comboBoxSelectUser.Location = new Point(comboBoxSelectUser.Location.X, comboBoxSelectUser.Location.Y - 12);
                dateTimePickerStart.Height = 35;
                dateTimePickerStart.Font = new Font(dateTimePickerStart.Font.FontFamily, 11);
                dateTimePickerEnd.Height = 35;
                dateTimePickerEnd.Font = new Font(dateTimePickerStart.Font.FontFamily, 11);
            }
            else
            {
                tabControlReportes.SelectedIndex = 2;
                tableLayoutPanelReportes.ColumnStyles[3].SizeType = SizeType.Percent;
                tableLayoutPanelReportes.ColumnStyles[3].Width = 17;
                //imgSinDatosCreditosDelTurno.Image = ClsMetodosGenerales.redimencionarBitmap(Properties.Resources.sindatos, 144, 133);
                dataGridViewVentasTurno.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGridViewEITurno.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }
        }

        private void comboBoxSelectUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSelectUser.Enabled)
            {
                if (permissionPrepedido)
                {

                } else
                {
                    if (tabControlReportes.SelectedIndex == 0)
                        resetearValoresDocumentos(0);
                    else if (tabControlReportes.SelectedIndex == 1)
                        resetearValoresEIAnteriores(0);
                }
                var um = (UserModel)comboBoxSelectUser.SelectedItem;
                if (um != null)
                {
                    FechaInicial = (dateTimePickerStart.Value).ToString("yyyy-MM-dd");
                    FechaFinal = (dateTimePickerEnd.Value).ToString("yyyy-MM-dd");
                    userId = um.id;
                    tabControlReportes_SelectedIndexChanged(sender, e);
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

        private void tabControlReportes_SelectedIndexChanged(object sender, EventArgs e)
        {
            chckBDlocal.Visible = false;
            String newLimite = "";
            newLimite = ComboBoxLimite.Text;
            int reallimit = 30;
            if (!newLimite.Equals(""))
            {
                try
                {
                    reallimit = int.Parse(newLimite);
                    if (reallimit < 0)
                    {
                        reallimit = 30;
                    }
                }catch(Exception ee)
                {
                    reallimit = 30;
                }
            }
            LIMIT = reallimit;
            switch (tabControlReportes.SelectedIndex)
            {
                case 0:
                    {
                        ComboBoxLimite.Visible = false;
                        txtlimite.Visible = false;
                        LIMIT = 0;
                        ComboBoxLimite.Text = "0";
                        chckBDlocal.Visible = true;
                        comboBoxSelectUser.Enabled = true;
                        dateTimePickerStart.Enabled = true;
                        dateTimePickerEnd.Enabled = true;
                        if (reporteCajaActivated == 1)
                        {
                            panelFilters.Height = panelFilters.Height + 58;
                            tabControlReportes.Location = new Point(0, 68);
                            tabControlReportes.Height = (this.Height - panelToolbar.Height -
                                panelFilters.Height - 45);
                            reporteCajaActivated = 0;
                        }
                        if (BDlocal)
                        {
                            logicToGetAndChargeDocumentsBDLocal(searchWordPrepedidos);
                        }
                        else
                        {
                            logicToGetAndChargeDocuments(searchWordPrepedidos);
                        }
                        break;
                    }
                case 1:
                    {
                        ComboBoxLimite.Visible = true;
                        txtlimite.Visible = true;
                        comboBoxSelectUser.Enabled = true;
                        dateTimePickerStart.Enabled = true;
                        dateTimePickerEnd.Enabled = true;
                        if (reporteCajaActivated == 1)
                        {
                            panelFilters.Height = panelFilters.Height + 58;
                            tabControlReportes.Location = new Point(0, 68);
                            tabControlReportes.Height = (this.Height - panelToolbar.Height -
                                panelFilters.Height - 45);
                            reporteCajaActivated = 0;
                        }
                        logicToGetAnbdChargeWithdrawals(searchWordPrepedidos);
                        
                        break;
                    }
                case 2:
                    {
                        resetVariablesEIDelTurno(0);
                        resetVariablesCreditosDelTurno(0);
                        resetearValoresDocumentos(0);
                        resetearValoresEIAnteriores(0);
                        comboBoxSelectUser.Enabled = false;
                        dateTimePickerStart.Enabled = false;
                        dateTimePickerEnd.Enabled = false;
                        reporteCajaActivated = 1;
                        panelFilters.Height = panelFilters.Height - 58;
                        tabControlReportes.Location = new Point(0, 4);
                        tabControlReportes.Height = (tabControlReportes.Height + 45);
                        totalVendidoYCobrado = 0;
                        logicToGetAnbdChargeCash();
                        break;
                    }
            }
        }

        private async Task logicToGetAnbdChargeWithdrawals(String searchWordPrepedidos)
        {
            Cursor.Current = Cursors.WaitCursor;
            showOrHideDateAndUsers(true);
            frmWaiting = new FormWaiting(this, GetDataService.GET_WITHDRAWAL, searchWordPrepedidos);
            frmWaiting.StartPosition = FormStartPosition.CenterScreen;
            frmWaiting.ShowDialog();
            //await fillDataGridRetiros();
            Cursor.Current = Cursors.Default;
        }

        /*----------------------- Tab 1 Documentos Anteriores ---------------*/
        private async Task logicToGetAndChargeDocuments(String searchWordPrepedidos)
        {
            Cursor.Current = Cursors.WaitCursor;
            showOrHideDateAndUsers(true);
            frmWaiting = new FormWaiting(this, GetDataService.GET_DOCUMENT, searchWordPrepedidos);
            frmWaiting.StartPosition = FormStartPosition.CenterScreen;
            frmWaiting.ShowDialog();
            await fillDataGridDocuments();
            Cursor.Current = Cursors.Default;
        }
        private async Task logicToGetAndChargeDocumentsBDLocal(String searchWordPrepedidos)
        {
            Cursor.Current = Cursors.WaitCursor;
            showOrHideDateAndUsers(true);
            dynamic documentosLocal = new ExpandoObject();
            documentosLocal = fillDataGridDocumentsDBLocal(FechaInicial,FechaFinal);
            dataGridViewDocumentosAnteriores.Rows.Clear();
            if (documentosLocal.value == -1)
            {
                SECUDOC.writeLog(documentosLocal.descripcion);
            }
            else
            {
                if(documentosLocal.value == 0)
                {
                    imgSinDatosDocumentosAnteriores.Visible = true;
                }
                else
                {
                    //MessageBox.Show(FechaInicial+" "+" "+FechaFinal+" "+userId);
                    dynamic Documentos = new ExpandoObject();
                    Documentos = documentosLocal.documentos;
                    for (int n = 0; n < Documentos.Count; n++)
                    {
                        String documentType = "";
                        if (Documentos[n].tipo_documento == 1)
                            documentType = "Cotización";
                        else if (Documentos[n].tipo_documento == 2)
                            documentType = "Venta";
                        else if (Documentos[n].tipo_documento == 3)
                            documentType = "Pedido";
                        else if (Documentos[n].tipo_documento == 4)
                            documentType = "Remisión";
                        else if (Documentos[n].tipo_documento == 5)
                            documentType = "Devolución";
                        else if (Documentos[n].tipo_documento == 9)
                            documentType = "Pago del Cliente";
                        else if (Documentos[n].tipo_documento == 50)
                            documentType = "Prepedido";
                        else if (Documentos[n].tipo_documento == 51)
                            documentType = "Cotización de Mostrador";
                        if (documentType.Equals(""))
                            documentType = Documentos[n].tipo_documento.ToString();
                        dynamic[] row = {
                            Documentos[n].idWebService,
                            Documentos[n].clave_cliente,
                            documentType,
                            Documentos[n].fechahoramov,
                            Documentos[n].fventa,
                            Documentos[n].descuento+"%",
                            Documentos[n].nombreformacobro,
                            Documentos[n].anticipo,
                            Documentos[n].total};
                        dataGridViewDocumentosAnteriores.Rows.Add(row);
                    }
                    dataGridViewDocumentosAnteriores.PerformLayout();
                    dataGridViewDocumentosAnteriores.Columns[5].Visible = true;
                    dataGridViewDocumentosAnteriores.Columns[6].Visible = true;
                    Cursor.Current = Cursors.Default;

                    imgSinDatosDocumentosAnteriores.Visible = false;
                }
            }
        }

        public async Task callDownloadDocumentsFromServer(int information)
        {
            if (information == GetDataService.GET_DOCUMENT)
            {
                if (lastIdDocumentosAnteriores == 0)
                    resetearValoresDocumentos(0);
                await callDownloadDocumentsProcess(userId, FechaInicial + " 00:00:00", FechaFinal + " 23:59:59", lastIdDocumentosAnteriores);
            } else if (information == GetDataService.GET_WITHDRAWAL)
            {
                if (lastIdEIAnteriores == 0)
                {
                    resetearValoresEIAnteriores(0);
                    resetVariablesEIDelTurno(0);
                }
                await callDownloadWithdrawalsProcess(userId, FechaInicial + " 00:00:00", FechaFinal + " 23:59:59", lastIdEIAnteriores);
                
            }
        }

        private async Task logicToGetAnbdChargeCash()
        {
            Cursor.Current = Cursors.WaitCursor;
            showOrHideDateAndUsers(false);
            /*fw = new FrmWaiting(FrmWaiting.CALL_REPORTES, this, 2, null, 0);
            fw.StartPosition = FormStartPosition.CenterScreen;
            fw.ShowDialog();*/

            
            await fillDataGridCreditosReporteCaja();
            await fillDataGridVentasReporteCaja();
            await fillDataGridRetiradoReporteCaja();
            await fillTotalesVentasDelTurno();
            await fillTotalesCreditosDelTurno();
            await getAllAbonos();
            await fillTotalesPorFormasDeCobro();
            await fillTotaltesRetiros();
        }

        /*private void dateTimePickerStart_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                //if (dateTimePickerStart.Enabled){
                    /*if (permissionPrepedido)
                    {

                    }
                    else
                    {
                        if (tabControlReportes.SelectedIndex == 0)
                            resetearValoresDocumentos(0);
                        else if (tabControlReportes.SelectedIndex == 1)
                            resetearValoresEIAnteriores(0);
                    }
                    UserModel um = (UserModel)comboBoxSelectUser.SelectedItem;
                    if (um != null)
                    {
                        /*FechaInicial = (dateTimePickerStart.Value).ToString("yyyy-MM-dd HH:mm:ss");
                        if (contadorBanderaFecha > 1)
                        {
                            FechaInicial = (dateTimePickerStart.Value).ToString("yyyy-MM-dd");
                            //FechaInicial += " 00:00:00";  //00:00:00 a. m
                        }
                        FechaInicial = (dateTimePickerStart.Value).ToString("yyyy-MM-dd");
                        FechaFinal = (dateTimePickerEnd.Value).ToString("yyyy-MM-dd");
                        //FechaFinal += " 23:59:59";
                        userId = um.id;
                        tabControlReportes_SelectedIndexChanged(sender, e);
                        PopupNotifier popup = new PopupNotifier();
                        popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.caution, 100, 100);
                        popup.TitleColor = Color.Blue;
                        popup.TitleText = "Datos";
                        popup.ContentText = "Documentos de " + um.Nombre + " por fecha";
                        popup.ContentColor = Color.Red;
                        popup.Popup();
                    }
                //}
            }
            catch(Exception error)
            {
                SECUDOC.writeLog(error.ToString());
            }
        }*/

        private void dateTimePickerEnd_ValueChanged(object sender, EventArgs e)
        {
            MessageBox.Show((dateTimePickerEnd.Value).ToString("yyyy-MM-dd"));
            FechaFinal = (dateTimePickerEnd.Value).ToString("yyyy-MM-dd");
            /*if (dateTimePickerEnd.Enabled)
            {
                if (permissionPrepedido)
                {

                }
                else
                {
                    if (tabControlReportes.SelectedIndex == 0)
                        resetearValoresDocumentos(0);
                    else if (tabControlReportes.SelectedIndex == 1)
                        resetearValoresEIAnteriores(0);
                }
                UserModel um = (UserModel)comboBoxSelectUser.SelectedItem;
                if (um != null)
                {
                    /*FechaInicial = (dateTimePickerStart.Value).ToString("yyyy-MM-dd HH:mm:ss");
                    if (contadorBanderaFecha > 1)
                    {
                        FechaInicial = (dateTimePickerStart.Value).ToString("yyyy-MM-dd");
                        //FechaInicial += " 00:00:00";  //00:00:00 a. m
                    }
                    FechaInicial = (dateTimePickerStart.Value).ToString("yyyy-MM-dd");
                    FechaFinal = (dateTimePickerEnd.Value).ToString("yyyy-MM-dd");
                    //FechaFinal += " 23:59:59";
                    userId = um.id;
                    tabControlReportes_SelectedIndexChanged(sender, e);
                    PopupNotifier popup = new PopupNotifier();
                    popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.caution, 100, 100);
                    popup.TitleColor = Color.Red;
                    popup.TitleText = "Datos";
                    popup.ContentText = "Documentos de " + um.Nombre + " por fecha";
                    popup.ContentColor = Color.Red;
                    popup.Popup();
                }
            }*/
        }

        private async Task callDownloadDocumentsProcess(int idUser, String startDate, String endDate, int lastId)
        {
            GetDataService gds = new GetDataService();
            dynamic respuesta = new ExpandoObject();
            bool serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
            if (serverModeLAN)
                respuesta = await gds.downloadAllDocumentsLAN(idUser, startDate, endDate, lastId, LIMIT);
            else respuesta = await gds.downloadAllDocuments(idUser, startDate, endDate, lastId, LIMIT);
            validateDownloadDocumentsResponse(respuesta);
        }

        private async Task callDownloadWithdrawalsProcess(int idUser, String startDate, String endDate, int lastId)
        {
            dynamic respuesta = new ExpandoObject();
            
                GetDataService gds = new GetDataService();
                if (ConfiguracionModel.isLANPermissionActivated())
                    respuesta = await gds.downloadAllWithdrawalsLAN(idUser, startDate, endDate, lastId, LIMIT);
                else respuesta = await gds.downloadAllWithdrawals(idUser, startDate, endDate, lastId, LIMIT);
            validateDownloadDocumentsResponse(respuesta);
            
        }

        
        private async Task validateDownloadDocumentsResponse(dynamic respuesta)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (respuesta != null)
            {
                int value = respuesta.value;
                String description = respuesta.description;
                int information = respuesta.information;
                if (value < 0)
                {
                    FormMessage fm = new FormMessage("Exception", description, 2);
                    fm.ShowDialog();
                    if (frmWaiting != null)
                    {
                        frmWaiting.Close();
                        frmWaiting.Dispose();
                    }
                }
                else
                {
                    if (information == GetDataService.GET_DOCUMENT)
                    {
                        if (value > 0)
                        {
                            if (frmWaiting != null && frmWaiting.Visible)
                                frmWaiting.Close();
                            totalDocumentosAnteriores = value;
                            documentosAnterioresListTemp = respuesta.documentos;
                            await fillDataGridDocuments();
                        } else if (value == 0)
                        {
                            if (frmWaiting != null && frmWaiting.Visible)
                                frmWaiting.Close();
                            FormMessage formMessage = new FormMessage("No se encontro información", description, 3);
                            formMessage.ShowDialog();
                        } else
                        {
                            if (frmWaiting != null && frmWaiting.Visible)
                                frmWaiting.Close();
                            FormMessage formMessage = new FormMessage("Exception", description, 2);
                            formMessage.ShowDialog();
                        }
                    }
                    else if (information == GetDataService.GET_WITHDRAWAL)
                    {
                        if (value > 0)
                        {
                            if (frmWaiting != null)
                                frmWaiting.Close();
                            totalEIAnteriores = value;
                            egresosListTemp = respuesta.retiros;
                            await fillDataGridRetiros();
                        }
                        else if (value == 0)
                        {
                            if (frmWaiting != null)
                            {
                                frmWaiting.Close();
                                frmWaiting.Dispose();
                            }
                            FormMessage formMessage = new FormMessage("No se encontro información", description, 3);
                            formMessage.ShowDialog();
                        }
                        else
                        {
                            if (frmWaiting != null)
                            {
                                frmWaiting.Close();
                                frmWaiting.Dispose();
                            }
                            FormMessage formMessage = new FormMessage("Exception", description, 2);
                            formMessage.ShowDialog();
                        }
                    } 
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void dateTimePickerStart_ValueChanged(object sender, EventArgs e)
        {
            MessageBox.Show((dateTimePickerStart.Value).ToString("yyyy-MM-dd"));

            FechaInicial = (dateTimePickerStart.Value).ToString("yyyy-MM-dd");
        }

        private void btnBuscarReportes_Click(object sender, EventArgs e)
        {
            if (dateTimePickerEnd.Enabled)
            {
                if (permissionPrepedido)
                {

                }
                else
                {
                    if (tabControlReportes.SelectedIndex == 0)
                        resetearValoresDocumentos(0);
                    else if (tabControlReportes.SelectedIndex == 1)
                        resetearValoresEIAnteriores(0);
                }
                UserModel um = (UserModel)comboBoxSelectUser.SelectedItem;
                if (um != null)
                {
                    FechaInicial = (dateTimePickerStart.Value).ToString("yyyy-MM-dd HH:mm:ss");
                    if (contadorBanderaFecha > 1)
                    {
                        FechaInicial = (dateTimePickerStart.Value).ToString("yyyy-MM-dd");
                        //FechaInicial += " 00:00:00";  //00:00:00 a. m
                    }
                    FechaInicial = (dateTimePickerStart.Value).ToString("yyyy-MM-dd");
                    FechaFinal = (dateTimePickerEnd.Value).ToString("yyyy-MM-dd");
                    //FechaFinal += " 23:59:59";
                    userId = um.id;
                    tabControlReportes_SelectedIndexChanged(sender, e);
                    PopupNotifier popup = new PopupNotifier();
                    popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.caution, 100, 100);
                    popup.TitleColor = Color.Red;
                    popup.TitleText = "Datos";
                    popup.ContentText = "Documentos de " + um.Nombre + " por fecha";
                    popup.ContentColor = Color.Red;
                    popup.Popup();
                }
            }
        }

        private void chckBDlocal_CheckedChanged(object sender, EventArgs e)
        {
            if (chckBDlocal.Checked)
                BDlocal = true;
            else BDlocal = false;
        }

        public static dynamic fillDataGridDocumentsDBLocal(String FechaInicial,String FechaFinal)
        {
            
            dynamic respuesta = new ExpandoObject();
            List<dynamic> Documentos = null;
            try
            {
                String query = "select * from Documentos inner join FormasDeCobro on Documentos.FORMA_COBRO_ID = FormasDeCobro.FORMA_COBRO_CC_ID Where " + LocalDatabase.CAMPO_FECHAHORAMOV_DOC+ " BETWEEN '"+
                    FechaInicial+" 00:00:00' AND '"+FechaFinal+" 23:59:59' AND "+LocalDatabase.CAMPO_USUARIOID_DOC + " = "+userId+" " +
                    "And "+LocalDatabase.CAMPO_PAUSAR_DOC+"= 0 AND "+
                    LocalDatabase.CAMPO_CANCELADO_DOC+"=0 order by id desc";
                
                Documentos = DocumentModel.getReporteDeDocumentos(query);
                if (Documentos != null)
                {
                    respuesta.value = Documentos.Count;
                    respuesta.description = "";
                }
                else
                {
                    respuesta.value = 0;
                    respuesta.description = "No hay documentos encontrados";
                }
                respuesta.documentos = Documentos;
            }
            catch(Exception e){
                respuesta.value = -1;
                respuesta.description = e.ToString();
                respuesta.documentos = null;
            }
            return respuesta;
        }

        private async Task fillDataGridDocuments()
        {
            gridScrollBarsDocumentosAnteriores = dataGridViewDocumentosAnteriores.ScrollBars;
            lastLoadingDocumentosAnteriores = DateTime.Now;
            if (documentosAnterioresListTemp != null && documentosAnterioresListTemp.Count > 0)
            {
                progressDocumentosAnteriores += documentosAnterioresListTemp.Count;
                documentosAnterioresList.AddRange(documentosAnterioresListTemp);
                if (documentosAnterioresList.Count > 0 && dataGridViewDocumentosAnteriores.ColumnHeadersVisible == false)
                    dataGridViewDocumentosAnteriores.ColumnHeadersVisible = true;
                for (int i = 0; i < documentosAnterioresListTemp.Count; i++)
                {
                    int n = dataGridViewDocumentosAnteriores.Rows.Add();
                    dataGridViewDocumentosAnteriores.Rows[n].Cells[0].Value = documentosAnterioresListTemp[i].id + "";
                    dataGridViewDocumentosAnteriores.Columns["idDgvDocuments"].Visible = false;
                    String customerName = CustomerModel.getAStringValueFromACustomer("SELECT " + LocalDatabase.CAMPO_NOMBRECLIENTE + " FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " +
                            LocalDatabase.CAMPO_ID_CLIENTE + " = " + documentosAnterioresListTemp[i].cliente_id);
                    if (customerName.Equals(""))
                        customerName = documentosAnterioresListTemp[i].clave_cliente;
                    dataGridViewDocumentosAnteriores.Rows[n].Cells[1].Value = customerName;
                    dataGridViewDocumentosAnteriores.Columns[1].Width = 150;
                    if (documentosAnterioresListTemp[i].tipo_documento == 1)
                        dataGridViewDocumentosAnteriores.Rows[n].Cells[2].Value = "Cotización";
                    else if (documentosAnterioresListTemp[i].tipo_documento == 2 || documentosAnterioresListTemp[i].tipo_documento == 4)
                        dataGridViewDocumentosAnteriores.Rows[n].Cells[2].Value = "Venta";
                    else if (documentosAnterioresListTemp[i].tipo_documento == 3)
                    {
                        dataGridViewDocumentosAnteriores.Rows[n].Cells[2].Value = "Pedido";
                    }
                    else if (documentosAnterioresListTemp[i].tipo_documento == 5)
                    {
                        dataGridViewDocumentosAnteriores.Rows[n].Cells[2].Value = "Devolución";
                    } else if (documentosAnterioresListTemp[i].tipo_documento == 9)
                        dataGridViewDocumentosAnteriores.Rows[n].Cells[2].Value = "Pago del Cliente";
                    else if (documentosAnterioresListTemp[i].tipo_documento == DocumentModel.TIPO_COTIZACION_MOSTRADOR)
                    {
                        dataGridViewDocumentosAnteriores.Rows[n].Cells[2].Value = "Cotización de Mostrador";
                    }
                    if (permissionPrepedido)
                        dataGridViewDocumentosAnteriores.Rows[n].Cells[2].Value = "Entrega";
                    dataGridViewDocumentosAnteriores.Rows[n].Cells[3].Value = documentosAnterioresListTemp[i].fechahoramov;
                    dataGridViewDocumentosAnteriores.Rows[n].Cells[4].Value = documentosAnterioresListTemp[i].fventa;
                    if (permissionPrepedido)
                    {
                        dataGridViewDocumentosAnteriores.Columns["discountDgvDocuments"].HeaderText = "Producto";
                        dataGridViewDocumentosAnteriores.Columns["fcDgvDocuments"].HeaderText = "Cantidad";
                        dataGridViewDocumentosAnteriores.Columns["anticipoDgvDocuments"].HeaderText = "Unidad";
                        dataGridViewDocumentosAnteriores.Columns["totalDgvDocuments"].HeaderText = "Piezas Pollos";
                        MovimientosModel mm = MovimientosModel.getAMovementFromADocument(documentosAnterioresListTemp[i].id);
                        if (mm != null)
                        {
                            String productName = ItemModel.getTheNameOfAnItem(mm.itemId);
                            dataGridViewDocumentosAnteriores.Rows[n].Cells[5].Value = productName;
                            dataGridViewDocumentosAnteriores.Rows[n].Cells[6].Value = "" + mm.capturedUnits;
                            String capturedUnitsName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(mm.capturedUnitId);
                            dataGridViewDocumentosAnteriores.Rows[n].Cells[7].Value = "" + capturedUnitsName;
                            String capturedNonConvertibleUnitsName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(mm.nonConvertibleUnitId);
                            dataGridViewDocumentosAnteriores.Rows[n].Cells[8].Value = "" + mm.nonConvertibleUnits + " " + capturedNonConvertibleUnitsName;
                        }
                    }
                    else
                    {
                        dataGridViewDocumentosAnteriores.Rows[n].Cells[5].Value = documentosAnterioresListTemp[i].descuento;
                        String fcDocs = "";
                        String cambioInfo = "";
                        List<FormasDeCobroDocumentoModel> fcdList = FormasDeCobroDocumentoModel.getAllTheWaysToCollectADocument(documentosAnterioresListTemp[i].id);
                        double cambio = 0;
                        if (fcdList != null && fcdList.Count > 0)
                        {
                            for (int j = 0; j < fcdList.Count; j++)
                            {
                                String fcName = FormasDeCobroModel.getANameFrromAFomaDeCobroWithId(fcdList[j].formaCobroIdAbono);
                                cambio += fcdList[j].cambio;
                                if (j == (fcdList.Count - 1))
                                    fcDocs += fcName + " " + fcdList[j].importe.ToString("C") + " MXN\r\n";
                                else fcDocs += fcName + " " + fcdList[j].importe.ToString("C") + " MXN\r\n";
                            }
                        }
                        else
                        {
                            if (documentosAnterioresListTemp[i].forma_corbo_id_abono > 0)
                            {
                                String fcName = FormasDeCobroModel.getANameFrromAFomaDeCobroWithId(documentosAnterioresListTemp[i].forma_corbo_id_abono);
                                fcDocs += fcName + " " + documentosAnterioresListTemp[i].anticipo.ToString("C") + " MXN\r\n";
                            }
                            else
                            {
                                String fcName = FormasDeCobroModel.getANameFrromAFomaDeCobroWithId(documentosAnterioresListTemp[i].forma_corbo_id_abono);
                                fcDocs += fcName + " " + documentosAnterioresListTemp[i].total.ToString("C") + " MXN\r\n";
                            }
                        }
                        cambioInfo += "Cambio " + cambio.ToString("C") + " MXN";
                        dataGridViewDocumentosAnteriores.Rows[n].Cells[6].Value = fcDocs +"\r\n"+ cambioInfo;
                        dataGridViewDocumentosAnteriores.Rows[n].Cells[7].Value = "" + documentosAnterioresListTemp[i].anticipo.ToString("C") + " MXN";
                        dataGridViewDocumentosAnteriores.Rows[n].Cells[8].Value = "" + documentosAnterioresListTemp[i].total.ToString("C") + " MXN";
                    }
                }
                dataGridViewDocumentosAnteriores.PerformLayout();
                documentosAnterioresListTemp.Clear();
                lastIdDocumentosAnteriores = Convert.ToInt32(documentosAnterioresList[documentosAnterioresList.Count - 1].id);
                imgSinDatosDocumentosAnteriores.Visible = false;
            }
            else
            {
                if (progressDocumentosAnteriores > 0)
                {
                    imgSinDatosDocumentosAnteriores.Visible = true;
                    btnReportePdfDocumentosAnteriores.Enabled = true;
                }
            }
            textTotalDocumentsAnteriores.Text = "Documentos: " + totalDocumentosAnteriores.ToString().Trim();
            //reset displayed row
            if (firstVisibleRowDocumentosAnteriores > -1)
            {
                dataGridViewDocumentosAnteriores.ScrollBars = gridScrollBarsDocumentosAnteriores;
                if (dataGridViewDocumentosAnteriores.Rows.Count > 0)
                {
                    dataGridViewDocumentosAnteriores.FirstDisplayedScrollingRowIndex = firstVisibleRowDocumentosAnteriores;
                    imgSinDatosDocumentosAnteriores.Visible = false;
                }
            }
        }

        private async Task<List<DocumentModel>> getAllDocuments()
        {
            List<DocumentModel> documentsList = null;
            await Task.Run(async () =>
            {
                if (queryTypeDocumentosAnteriores == 0)
                {
                    queryDocumentosAnterioresTemp = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                    LocalDatabase.CAMPO_FECHAHORAMOV_DOC + " BETWEEN '" + FechaInicial + " 00:00:00' AND '" +
                    FechaFinal + " 23:59:59' AND " + LocalDatabase.CAMPO_USUARIOID_DOC + " = " + userId + " AND " + LocalDatabase.CAMPO_ID_DOC + " > " +
                    lastIdDocumentosAnteriores + " ORDER BY " + LocalDatabase.CAMPO_ID_DOC + " LIMIT " + LIMIT;
                    queryTotalesDocumentosAnteriores = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                    LocalDatabase.CAMPO_FECHAHORAMOV_DOC + " BETWEEN '" + FechaInicial + " 00:00:00' AND '" +
                    FechaFinal + " 23:59:59' AND " + LocalDatabase.CAMPO_USUARIOID_DOC + " = " + userId;
                    queryDocumentosAnterioresReport = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                    LocalDatabase.CAMPO_FECHAHORAMOV_DOC + " BETWEEN '" + FechaInicial + " 00:00:00' AND '" +
                    FechaFinal + " 23:59:59' AND " + LocalDatabase.CAMPO_USUARIOID_DOC + " = " + userId;
                }
                totalDocumentosAnteriores = DocumentModel.getIntValue(queryTotalesDocumentosAnteriores);
                documentsList = DocumentModel.getAllDocuments(queryDocumentosAnterioresTemp);
            });
            return documentsList;
        }

        private void dataGridViewDocumentosAnteriores_Scroll(object sender, ScrollEventArgs e)
        {
            if (documentosAnterioresList.Count < totalDocumentosAnteriores && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
                {
                    if (e.NewValue > dataGridViewDocumentosAnteriores.Rows.Count - getDisplayedRowsCountDocumentosAnteriores())
                    {
                        //prevent loading from autoscroll.
                        TimeSpan ts = DateTime.Now - lastLoadingDocumentosAnteriores;
                        if (ts.TotalMilliseconds > 100)
                        {
                            firstVisibleRowDocumentosAnteriores = e.NewValue;
                            //dataGridItems.PerformLayout();
                            //fillDataGridDocuments();
                            logicToGetAndChargeDocuments(searchWordPrepedidos);
                        }
                        else
                        {
                            dataGridViewDocumentosAnteriores.FirstDisplayedScrollingRowIndex = e.OldValue;
                        }
                    }
                }
            }
        }

        private int getDisplayedRowsCountDocumentosAnteriores()
        {
            int count = dataGridViewDocumentosAnteriores.Rows[dataGridViewDocumentosAnteriores.FirstDisplayedScrollingRowIndex].Height;
            count = dataGridViewDocumentosAnteriores.Height / count;
            return count;
        }

        private void btnReportePdfDocumentosAnteriores_Click(object sender, EventArgs e)
        {
            PopupNotifier popup = new PopupNotifier();
            popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.caution, 100, 100);
            popup.TitleColor = Color.Red;
            popup.TitleText = "Proceso Iniciado";
            popup.ContentText = "Generando PDF";
            popup.ContentColor = Color.Red;
            popup.Popup();
            frmWaiting = new FormWaiting(this, 2, searchWordPrepedidos);
            frmWaiting.StartPosition = FormStartPosition.CenterScreen;
            frmWaiting.ShowDialog();
        }

        private async Task fillDataGridRetiros()
        {
            gridScrollBarsEIAnteriores = dataGridViewEIAnteriores.ScrollBars;
            lastLoadingEIAnteriores = DateTime.Now;
            //if (call == 0)
                //egresosTurnoListTemp = await getAllWithdrawals();
            if (egresosListTemp != null && egresosListTemp.Count > 0)
            {
                progressEIAnteriores += egresosListTemp.Count;
                egresosList.AddRange(egresosListTemp);
                if(LIMIT == 0)
                {
                    for (int i = 0; i < egresosListTemp.Count; i++)
                    {
                            int n = dataGridViewEIAnteriores.Rows.Add();
                            dataGridViewEIAnteriores.Rows[n].Cells[0].Value = egresosListTemp[i].id + "";
                            dataGridViewEIAnteriores.Rows[n].Cells[1].Value = egresosListTemp[i].number + "";
                            dataGridViewEIAnteriores.Rows[n].Cells[2].Value = egresosListTemp[i].concept + "";
                            dataGridViewEIAnteriores.Rows[n].Cells[3].Value = egresosListTemp[i].description + "";
                            dataGridViewEIAnteriores.Rows[n].Cells[4].Value = egresosListTemp[i].fechaHora + "";
                            dataGridViewEIAnteriores.Rows[n].Cells[5].Value = "0";
                            dataGridViewEIAnteriores.Columns[5].Visible = false;
                            dataGridViewEIAnteriores.Rows[n].Cells[6].Value = "0";
                            dataGridViewEIAnteriores.Columns[6].Visible = false;
                        
                    }
                }
                else
                {
                    for (int i = 0; i < egresosListTemp.Count; i++)
                    {
                        if (LIMIT > i)
                        {
                            int n = dataGridViewEIAnteriores.Rows.Add();
                            dataGridViewEIAnteriores.Rows[n].Cells[0].Value = egresosListTemp[i].id + "";
                            dataGridViewEIAnteriores.Rows[n].Cells[1].Value = egresosListTemp[i].number + "";
                            dataGridViewEIAnteriores.Rows[n].Cells[2].Value = egresosListTemp[i].concept + "";
                            dataGridViewEIAnteriores.Rows[n].Cells[3].Value = egresosListTemp[i].description + "";
                            dataGridViewEIAnteriores.Rows[n].Cells[4].Value = egresosListTemp[i].fechaHora + "";
                            dataGridViewEIAnteriores.Rows[n].Cells[5].Value = "0";
                            dataGridViewEIAnteriores.Columns[5].Visible = false;
                            dataGridViewEIAnteriores.Rows[n].Cells[6].Value = "0";
                            dataGridViewEIAnteriores.Columns[6].Visible = false;
                        }
                    }
                }
                
                dataGridViewEIAnteriores.PerformLayout();
                egresosListTemp.Clear();
                lastIdEIAnteriores = Convert.ToInt32(egresosList[egresosList.Count - 1].id);
                btnReportePdfEI.Enabled = true;
                imgSinDatosEIAnteriores.Visible = false;
            } else{
                if (progressEIAnteriores == 0)
                    imgSinDatosEIAnteriores.Visible = true;
            }
            textTotalEIAnteriores.Text = "Egresos: " + totalEIAnteriores.ToString().Trim();
            //reset displayed row
            if (firstVisibleRowEIAnteriores > -1)
            {
                dataGridViewEIAnteriores.ScrollBars = gridScrollBarsEIAnteriores;
                if (egresosList.Count > 0)
                    dataGridViewEIAnteriores.FirstDisplayedScrollingRowIndex = firstVisibleRowEIAnteriores;
                //imgSinDatosSalesRepCaja.Visible = false;
            }

            dataGridViewCreditsTurno.ClearSelection();
        }

        private async Task<List<RetiroModel>> getAllWithdrawals()
        {
            List<RetiroModel> retirosList = null;
            await Task.Run(() =>
            {
                if (queryTypeEIAnteriores == 0)
                {
                    queryEIAnterioresTemp = "SELECT * FROM " + LocalDatabase.TABLA_RETIROS + " WHERE " +
                    LocalDatabase.CAMPO_FECHAHORA_RETIRO + " BETWEEN '" + FechaInicial + " 00:00:00' AND '" +
                    FechaFinal + " 23:59:59' AND " + LocalDatabase.CAMPO_IDUSUARIO_RETIRO + " = " + userId +
                    " AND "+LocalDatabase.CAMPO_ID_RETIRO+" > "+lastIdEIAnteriores+" ORDER BY "+
                    LocalDatabase.CAMPO_ID_RETIRO+" ASC LIMIT "+LIMIT;
                    queryTotalesEIAnteriores = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_RETIROS + " WHERE " +
                    LocalDatabase.CAMPO_FECHAHORA_RETIRO + " BETWEEN '" + FechaInicial + " 00:00:00' AND '" +
                    FechaFinal + " 23:59:59' AND " + LocalDatabase.CAMPO_IDUSUARIO_RETIRO + " = " + userId;
                    queryEIAnterioresReport = "SELECT * FROM " + LocalDatabase.TABLA_RETIROS + " WHERE " +
                    LocalDatabase.CAMPO_FECHAHORA_RETIRO + " BETWEEN '" + FechaInicial + " 00:00:00' AND '" +
                    FechaFinal + " 23:59:59' AND " + LocalDatabase.CAMPO_IDUSUARIO_RETIRO + " = " + userId;
                }
                retirosList = RetiroModel.getAllWithdrawals(queryEIAnterioresTemp);
                totalEIAnteriores = RetiroModel.getIntValue(queryTotalesEIAnteriores);
            });
            return retirosList;
        }

        private void dataGridViewEIAnteriores_Scroll(object sender, ScrollEventArgs e)
        {
            if (egresosTurnoListActivated)
            {
                if (egresosList.Count < totalEIAnteriores && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
                {
                    if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
                    {
                        if (e.NewValue > dataGridViewEIAnteriores.Rows.Count - getDisplayedRowsCountEIAnteriores())
                        {
                            //prevent loading from autoscroll.
                            TimeSpan ts = DateTime.Now - lastLoadingEIAnteriores;
                            if (ts.TotalMilliseconds > 100)
                            {
                                firstVisibleRowEIAnteriores = e.NewValue;
                                //dataGridItems.PerformLayout();
                                //fillDataGridRetiros();
                                logicToGetAnbdChargeWithdrawals(searchWordPrepedidos);
                            }
                            else
                            {
                                dataGridViewEIAnteriores.FirstDisplayedScrollingRowIndex = e.OldValue;
                            }
                        }
                    }
                }
            }
            else
            {

            }
        }

        private int getDisplayedRowsCountEIAnteriores()
        {
            int count = dataGridViewEIAnteriores.Rows[dataGridViewEIAnteriores.FirstDisplayedScrollingRowIndex].Height;
            count = dataGridViewEIAnteriores.Height / count;
            return count;
        }

        private void btnReportePdfEI_Click(object sender, EventArgs e)
        {
            //generatePDfDocuments(dataGridViewEIAnteriores, "Retiros");
            frmWaiting = new FormWaiting(this, 3, searchWordPrepedidos);
            frmWaiting.StartPosition = FormStartPosition.CenterScreen;
            frmWaiting.ShowDialog();
        }

        public async Task processToGeneratePdfRetiros(bool entry)
        {
            String nameReport = "Retiros";
            String enterpriseName = "";
            DatosTicketModel dtm = DatosTicketModel.getAllData();
            if (dtm != null)
            {
                enterpriseName = dtm.EMPRESA;
            }
            String folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PDFsTPV";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            String filePath = @"" + folderPath + "\\" + "Reporte-" + nameReport + "_" + MetodosGenerales.getCurrentDateAndHourForFolioVenta() + ".pdf";
            bool created = await ClsPdfMethods.crearPdfEgresosIngresos(enterpriseName, filePath, entry, userId,
                FechaInicial, FechaFinal, 0, totalEIAnteriores);
            if (frmWaiting != null)
            {
                frmWaiting.Close();
                frmWaiting.Dispose();
            }
            FormMessage formMessage = new FormMessage("Documento Generado", "El documento PDF fue generado correctamente!\r\n"+
                filePath, 1);
            formMessage.ShowDialog();
        }

        public void resetearValoresDocumentos(int queryType)
        {
            this.queryTypeDocumentosAnteriores = queryType;
            queryDocumentosAnterioresTemp = "";
            queryDocumentosAnterioresReport = "";
            queryTotalesDocumentosAnteriores = "";
            totalDocumentosAnteriores = 0;
            lastIdDocumentosAnteriores = 0;
            progressDocumentosAnteriores = 0;
            documentosAnterioresList.Clear();
            dataGridViewDocumentosAnteriores.Rows.Clear();
        }

        public void resetearValoresEIAnteriores(int queryType)
        {
            this.queryTypeEIAnteriores = queryType;
            queryEIAnterioresTemp = "";
            queryEIAnterioresReport = "";
            queryTotalesEIAnteriores = "";
            totalEIAnteriores = 0;
            lastIdEIAnteriores = 0;
            progressEITurno = 0;
            if (egresosTurnoListActivated)
                egresosTurnoList.Clear();
            else ingresoTurnoList.Clear();
            dataGridViewEIAnteriores.Rows.Clear();
        }


        public async Task generatePDfDocuments()
        {
            Cursor.Current = Cursors.WaitCursor;
            String nameReport = "Documentos";
            String enterpriseName = "";
            DatosTicketModel dtm = DatosTicketModel.getAllData();
            if (dtm != null)
            {
                enterpriseName = dtm.EMPRESA;
            }
            String folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PDFsTPV";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            String filePath = @"" + folderPath + "\\" + "Reporte-" + nameReport + "_" + MetodosGenerales.getCurrentDateAndHourForFolioVenta() + ".pdf";
            ClsPdfMethods cpdfm = new ClsPdfMethods();
            if (BDlocal)
            {
                await cpdfm.createPdfDocumentsBDLOCAL(enterpriseName, filePath, nameReport, userId, permissionPrepedido,
                FechaInicial, FechaFinal, 0, totalDocumentosAnteriores);
            }
            else
            {
                await cpdfm.createPdfDocuments(enterpriseName, filePath, nameReport, userId, permissionPrepedido,
                FechaInicial, FechaFinal, 0, totalDocumentosAnteriores);
            }
            
            if (frmWaiting != null)
            {
                frmWaiting.Close();
                frmWaiting.Dispose();
            }
            FormMessage formMessage = new FormMessage("Documento Generado", "El documento PDF fue generado correctamente!\r\n"+
                filePath, 1);
            formMessage.ShowDialog();
            Cursor.Current = Cursors.Default;
        }

        /* -------------------- Tab 3 Documentos EI del Turno -----------------*/

        private void showOrHideDateAndUsers(bool state)
        {
            Cursor.Current = Cursors.WaitCursor;
            textUser.Visible = state;
            comboBoxSelectUser.Visible = state;
            textStartDate.Visible = state;
            //imgStartDate.Visible = state;
            dateTimePickerStart.Visible = state;
            textEndDate.Visible = state;
            //imgEndDate.Visible = state;
            dateTimePickerEnd.Visible = state;
            Cursor.Current = Cursors.Default;
        }

        private async Task fillDataGridCreditosReporteCaja()
        {
            gridScrollBarsCreditosDelTurno = dataGridViewCreditsTurno.ScrollBars;
            lastLoadingCreditosDelTurno = DateTime.Now;
            creditosDelTurnoListTemp = await getAllCreditsTurn();
            if (creditosDelTurnoListTemp != null && creditosDelTurnoListTemp.Count > 0)
            {
                progressCreditosDelTurno += creditosDelTurnoListTemp.Count;
                creditosDelTurnoList.AddRange(creditosDelTurnoListTemp);
                for (int i = 0; i < creditosDelTurnoListTemp.Count; i++)
                {
                    int n = dataGridViewCreditsTurno.Rows.Add();
                    if (creditosDelTurnoListTemp[i].estado == 1)
                        dataGridViewCreditsTurno.Rows[n].DefaultCellStyle.BackColor = Color.Red;
                    else dataGridViewCreditsTurno.Rows[n].DefaultCellStyle.BackColor = Color.White;
                    dataGridViewCreditsTurno.Rows[n].Cells[0].Value = creditosDelTurnoListTemp[i].id + "";
                    dataGridViewCreditsTurno.Columns["idDgvCreditsTurn"].Visible = false;
                    String query = "SELECT " + LocalDatabase.CAMPO_NOMBRECLIENTE + " FROM " + LocalDatabase.TABLA_CLIENTES +
                        " WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " = " + creditosDelTurnoListTemp[i].cliente_id;
                    String customerName = CustomerModel.getAStringValueFromACustomer(query);
                    dataGridViewCreditsTurno.Rows[n].Cells[1].Value = customerName;
                    dataGridViewCreditsTurno.Rows[n].Cells[2].Value = creditosDelTurnoListTemp[i].fventa;
                    dataGridViewCreditsTurno.Rows[n].Cells[3].Value = "$ " + MetodosGenerales.obtieneDosDecimales(creditosDelTurnoListTemp[i].descuento) + " MXN";
                    dataGridViewCreditsTurno.Rows[n].Cells[4].Value = "$ " + MetodosGenerales.obtieneDosDecimales(creditosDelTurnoListTemp[i].total) + " MXN";
                }
                dataGridViewCreditsTurno.PerformLayout();
                creditosDelTurnoListTemp.Clear();
                lastIdCreditsTurno = Convert.ToInt32(creditosDelTurnoList[creditosDelTurnoList.Count - 1].id);
                //imgSinDatosCreditosDelTurno.Visible = false;
            }
            else
            {
                /*if (progressCreditosDelTurno == 0)
                    imgSinDatosCreditosDelTurno.Visible = true;*/
            }
            textCreditsTurno.Text = "Créditos del Turno: " + totalCreditosDelTurno.ToString().Trim();
            //reset displayed row
            if (firstVisibleRowCreditosDelTurno > -1)
            {
                dataGridViewCreditsTurno.ScrollBars = gridScrollBarsCreditosDelTurno;
                if (creditosDelTurnoList.Count > 0)
                    dataGridViewCreditsTurno.FirstDisplayedScrollingRowIndex = firstVisibleRowCreditosDelTurno;
                //imgSinDatosCreditosDelTurno.Visible = false;
            }
        }

        private async Task<List<DocumentModel>> getAllCreditsTurn()
        {
            List<DocumentModel> documentsList = null;
            await Task.Run(async () =>
            {
                if (queryTypeVentasDelTurno == 0)
                {
                    queryCreditosDelTurno = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE (" + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 2 + " OR " +
                        LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 4 + ") AND " +
                        LocalDatabase.CAMPO_PAUSAR_DOC + " = " + 0 + " AND " + LocalDatabase.CAMPO_FORMACOBROID_DOC + " = " + 71 +
                        " AND " + LocalDatabase.CAMPO_ID_DOC + " > " + lastIdVentasTurno + " ORDER BY " +
                        LocalDatabase.CAMPO_ID_DOC + " LIMIT " + LIMIT;
                    queryTotalesCreditosDelTurno = "SELECT COUNT(id) FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA +
                        " WHERE (" + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 2 + " OR " +
                        LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 4 + ") AND " +
                        LocalDatabase.CAMPO_PAUSAR_DOC + " = " + 0 + " AND " + LocalDatabase.CAMPO_FORMACOBROID_DOC + " = " + 71;
                }
                totalCreditosDelTurno = DocumentModel.getIntValue(queryTotalesCreditosDelTurno);
                documentsList = DocumentModel.getAllDocuments(queryCreditosDelTurno);
            });
            return documentsList;
        }

        private void dataGridViewCreditsTurno_Scroll(object sender, ScrollEventArgs e)
        {
            if (creditosDelTurnoList.Count < totalCreditosDelTurno && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
                {
                    if (e.NewValue > dataGridViewCreditsTurno.Rows.Count - getDisplayedRowsCountCreditosDelTurno())
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
                            dataGridViewCreditsTurno.FirstDisplayedScrollingRowIndex = e.OldValue;
                        }
                    }
                }
            }
        }

        private int getDisplayedRowsCountCreditosDelTurno()
        {
            int count = dataGridViewCreditsTurno.Rows[dataGridViewCreditsTurno.FirstDisplayedScrollingRowIndex].Height;
            count = dataGridViewCreditsTurno.Height / count;
            return count;
        }

        private void resetVariablesCreditosDelTurno(int queryType)
        {
            queryTypeCreditosDelTurno = queryType;
            queryCreditosDelTurno = "";
            queryTotalesCreditosDelTurno = "";
            totalCreditosDelTurno = 0;
            lastIdCreditsTurno = 0;
            progressCreditosDelTurno = 0;
            creditosDelTurnoList.Clear();
            dataGridViewCreditsTurno.Rows.Clear();
        }

        private async Task fillDataGridVentasReporteCaja()
        {
            gridScrollBarsVentasDelTurno = dataGridViewVentasTurno.ScrollBars;
            lastLoadingVentasDelTurno = DateTime.Now;
            ventasDelTurnoListTemp = await getAllSales();
            if (ventasDelTurnoListTemp != null && ventasDelTurnoListTemp.Count > 0)
            {
                progressVentasDelTurno += ventasDelTurnoListTemp.Count;
                ventasDelTurnoList.AddRange(ventasDelTurnoListTemp);
                if (ventasDelTurnoList.Count > 0 && dataGridViewVentasTurno.ColumnHeadersVisible == false)
                    dataGridViewVentasTurno.ColumnHeadersVisible = true;
                for (int i = 0; i < ventasDelTurnoListTemp.Count; i++)
                {
                    int n = dataGridViewVentasTurno.Rows.Add();
                    if (ventasDelTurnoListTemp[i].estado == 1)
                        dataGridViewVentasTurno.Rows[n].DefaultCellStyle.BackColor = Color.Red;
                    else dataGridViewVentasTurno.Rows[n].DefaultCellStyle.BackColor = Color.White;
                    dataGridViewVentasTurno.Rows[n].Cells[0].Value = ventasDelTurnoListTemp[i].id + "";
                    dataGridViewVentasTurno.Columns["idDgvVentasTurno"].Visible = false;
                    String query = "SELECT " + LocalDatabase.CAMPO_NOMBRECLIENTE + " FROM " + LocalDatabase.TABLA_CLIENTES +
                        " WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " = " + ventasDelTurnoListTemp[i].cliente_id;
                    String customerName = CustomerModel.getAStringValueFromACustomer(query);
                    dataGridViewVentasTurno.Rows[n].Cells[1].Value = customerName;
                    dataGridViewVentasTurno.Rows[n].Cells[2].Value = ventasDelTurnoListTemp[i].fventa;
                    dataGridViewVentasTurno.Rows[n].Cells[3].Value = ventasDelTurnoListTemp[i].descuento.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                    dataGridViewVentasTurno.Rows[n].Cells[4].Value = ventasDelTurnoListTemp[i].total.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                }
                dataGridViewVentasTurno.PerformLayout();
                ventasDelTurnoListTemp.Clear();
                lastIdVentasTurno = Convert.ToInt32(ventasDelTurnoList[ventasDelTurnoList.Count - 1].id);
                //imgSinDatosSalesRepCaja.Visible = false;
            }
            else
            {
                /*if (progressVentasDelTurno == 0)
                    imgSinDatosSalesRepCaja.Visible = true;*/
            }
            textVentasTurno.Text = "Ventas del Turno: " + totalVentasDelTurno.ToString().Trim();
            //reset displayed row
            if (firstVisibleRowVentasDelTurno > -1)
            {
                dataGridViewVentasTurno.ScrollBars = gridScrollBarsVentasDelTurno;
                if (ventasDelTurnoList.Count > 0)
                    dataGridViewVentasTurno.FirstDisplayedScrollingRowIndex = firstVisibleRowVentasDelTurno;
                //imgSinDatosSalesRepCaja.Visible = false;
            }
        }

        private async Task<List<DocumentModel>> getAllSales()
        {
            List<DocumentModel> documentsList = null;
            await Task.Run(async () =>
            {
                if (queryTypeVentasDelTurno == 0)
                {
                    queryVentasDelTurno = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE (" + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 2 + " OR " +
                        LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 4 + ") AND " +
                        LocalDatabase.CAMPO_PAUSAR_DOC + " = " + 0 + " AND " + LocalDatabase.CAMPO_FORMACOBROID_DOC + " != " + 71 +
                        " AND " + LocalDatabase.CAMPO_ID_DOC + " > " + lastIdVentasTurno + " ORDER BY " +
                        LocalDatabase.CAMPO_ID_DOC + " LIMIT " + LIMIT;
                    queryTotalesVentasDelTurno = "SELECT COUNT(id) FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA +
                        " WHERE (" + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 2 + " OR " +
                        LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 4 + ") AND " +
                        LocalDatabase.CAMPO_PAUSAR_DOC + " = " + 0 + " AND " + LocalDatabase.CAMPO_FORMACOBROID_DOC + " != " + 71;
                }
                totalVentasDelTurno = DocumentModel.getIntValue(queryTotalesVentasDelTurno);
                documentsList = DocumentModel.getAllDocuments(queryVentasDelTurno);
            });
            return documentsList;
        }

        private void dataGridViewVentasTurno_Scroll(object sender, ScrollEventArgs e)
        {
            if (ventasDelTurnoList.Count < totalVentasDelTurno && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
                {
                    if (e.NewValue > dataGridViewVentasTurno.Rows.Count - getDisplayedRowsCountVentasDelTurno())
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
                            dataGridViewVentasTurno.FirstDisplayedScrollingRowIndex = e.OldValue;
                        }
                    }
                }
            }
        }

        private int getDisplayedRowsCountVentasDelTurno()
        {
            int count = dataGridViewVentasTurno.Rows[dataGridViewVentasTurno.FirstDisplayedScrollingRowIndex].Height;
            count = dataGridViewVentasTurno.Height / count;
            return count;
        }

        private void resetVariablesVentasDelTurno(int queryType)
        {
            queryTypeVentasDelTurno = queryType;
            queryVentasDelTurno = "";
            queryTotalesVentasDelTurno = "";
            totalVentasDelTurno = 0;
            lastIdVentasTurno = 0;
            progressVentasDelTurno = 0;
            ventasDelTurnoList.Clear();
            dataGridViewVentasTurno.Rows.Clear();
        }

        private async Task fillDataGridRetiradoReporteCaja()
        {
            gridScrollBarsEITurno = dataGridViewEITurno.ScrollBars;
            lastLoadingEITurno = DateTime.Now;
            egresosTurnoListTemp = await getAllWithdrawalsRepCaja();
            if (egresosTurnoListTemp != null && egresosTurnoListTemp.Count > 0)
            {
                progressEITurno += egresosTurnoListTemp.Count;
                egresosTurnoList.AddRange(egresosTurnoListTemp);
                for (int i = 0; i < egresosTurnoListTemp.Count; i++)
                {
                    int n = dataGridViewEITurno.Rows.Add();
                    dataGridViewEITurno.Rows[n].Cells[0].Value = egresosTurnoListTemp[i].id + "";
                    dataGridViewEITurno.Rows[n].Cells[1].Value = egresosTurnoListTemp[i].concept + "";
                    dataGridViewEITurno.Rows[n].Cells[2].Value = egresosTurnoListTemp[i].description + "";
                    dataGridViewEITurno.Rows[n].Cells[3].Value = egresosTurnoListTemp[i].number + "";
                    dataGridViewEITurno.Rows[n].Cells[4].Value = egresosTurnoListTemp[i].fechaHora + "";
                }
                dataGridViewEITurno.PerformLayout();
                egresosTurnoListTemp.Clear();
                lastIdEITurno = Convert.ToInt32(egresosTurnoList[egresosTurnoList.Count - 1].id);
                //imgSinDatosSalesRepCaja.Visible = false;
            }
            else
            {
                /*if (progressVentasDelTurno == 0)
                    imgSinDatosSalesRepCaja.Visible = true;*/
            }
            textEITurno.Text = "Retiros del Turno: " + totalEITurno.ToString().Trim();
            //reset displayed row
            if (firstVisibleRowEITurno > -1)
            {
                dataGridViewEITurno.ScrollBars = gridScrollBarsEITurno;
                if (egresosTurnoList.Count > 0)
                    dataGridViewEITurno.FirstDisplayedScrollingRowIndex = firstVisibleRowEITurno;
                //imgSinDatosSalesRepCaja.Visible = false;
            }
        }

        private async Task<List<RetiroModel>> getAllWithdrawalsRepCaja()
        {
            List<RetiroModel> retirosList = null;
            await Task.Run(async () =>
            {
                
                    queryEITurno = "SELECT * FROM " + LocalDatabase.TABLA_RETIROS +
                    //+ " WHERE " +
                    //LocalDatabase.CAMPO_IDUSUARIO_RETIRO + " = " + ClsRegeditController.getIdUserInTurn()+
                    //" AND " + LocalDatabase.CAMPO_ID_RETIRO+" > "+lastIdEITurno+
                    " ORDER BY " + LocalDatabase.CAMPO_ID_RETIRO+" ASC ";
                    queryTotalEITurno = "SELECT * FROM " + LocalDatabase.TABLA_RETIROS;
                    //+ " WHERE " +LocalDatabase.CAMPO_IDUSUARIO_RETIRO + " = " + ClsRegeditController.getIdUserInTurn();
                
                retirosList = RetiroModel.getAllWithdrawals(queryEITurno);
                totalEITurno = RetiroModel.getIntValue(queryTotalEITurno);
            });
            return retirosList;
        }

        private async Task fillDataGridIngresosReporteCaja()
        {
            gridScrollBarsEITurno = dataGridViewEITurno.ScrollBars;
            lastLoadingEITurno = DateTime.Now;
            ingresoTurnoListTemp = await getAllIngresosRepCaja();
            if (ingresoTurnoListTemp != null && ingresoTurnoListTemp.Count > 0)
            {
                progressEITurno += ingresoTurnoListTemp.Count;
                ingresoTurnoList.AddRange(ingresoTurnoListTemp);
                for (int i = 0; i < ingresoTurnoListTemp.Count; i++)
                {
                    int n = dataGridViewEITurno.Rows.Add();
                    dataGridViewEITurno.Rows[n].Cells[0].Value = ingresoTurnoListTemp[i].id + "";
                    dataGridViewEITurno.Rows[n].Cells[1].Value = ingresoTurnoListTemp[i].concept + "";
                    dataGridViewEITurno.Rows[n].Cells[2].Value = ingresoTurnoListTemp[i].description + "";
                    dataGridViewEITurno.Rows[n].Cells[3].Value = ingresoTurnoListTemp[i].number + "";
                    dataGridViewEITurno.Rows[n].Cells[4].Value = ingresoTurnoListTemp[i].dateTime + "";
                }
                dataGridViewEITurno.PerformLayout();
                ingresoTurnoListTemp.Clear();
                lastIdEITurno = Convert.ToInt32(ingresoTurnoList[ingresoTurnoList.Count - 1].id);
                //imgSinDatosSalesRepCaja.Visible = false;
            }
            else
            {
                /*if (progressVentasDelTurno == 0)
                    imgSinDatosSalesRepCaja.Visible = true;*/
            }
            textEITurno.Text = "Ingresos del Turno: " + totalEITurno.ToString().Trim();
            //reset displayed row
            if (firstVisibleRowEITurno > -1)
            {
                dataGridViewEITurno.ScrollBars = gridScrollBarsEITurno;
                if (ingresoTurnoList.Count > 0)
                    dataGridViewEITurno.FirstDisplayedScrollingRowIndex = firstVisibleRowEITurno;
                //imgSinDatosSalesRepCaja.Visible = false;
            }
        }

        private async Task<List<IngresoModel>> getAllIngresosRepCaja()
        {
            List<IngresoModel> ingresosList = null;
            await Task.Run(async () =>
            {
                if (queryTypeEITurno == 0)
                {
                    queryEITurno = "SELECT * FROM " + LocalDatabase.TABLA_INGRESO + " WHERE " +
                    LocalDatabase.CAMPO_IDUSUARIO_INGRESO + " = " + ClsRegeditController.getIdUserInTurn() +
                    " AND " + LocalDatabase.CAMPO_ID_INGRESO + " > " + lastIdEITurno +
                    " ORDER BY " + LocalDatabase.CAMPO_ID_INGRESO + " ASC ";
                    queryTotalEITurno = "SELECT * FROM " + LocalDatabase.TABLA_INGRESO + " WHERE " +
                    LocalDatabase.CAMPO_IDUSUARIO_INGRESO + " = " + ClsRegeditController.getIdUserInTurn();
                }
                ingresosList = IngresoModel.getAllEntries(queryEITurno);
                totalEITurno = IngresoModel.getIntValue(queryTotalEITurno);
            });
            return ingresosList;
        }

        private void dataGridViewEITurno_Scroll(object sender, ScrollEventArgs e)
        {
            if (egresosTurnoListActivated)
            {
                egresosDgvScroll(e);
            } else
            {
                ingresosDgvScroll(e);
            }
        }

        private void egresosDgvScroll(ScrollEventArgs e)
        {
            if (egresosTurnoList.Count < totalEITurno && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
                {
                    if (e.NewValue > dataGridViewEITurno.Rows.Count - getDisplayedRowsCountEITurno())
                    {
                        //prevent loading from autoscroll.
                        TimeSpan ts = DateTime.Now - lastLoadingEITurno;
                        if (ts.TotalMilliseconds > 100)
                        {
                            firstVisibleRowEITurno = e.NewValue;
                            //dataGridItems.PerformLayout();
                            fillDataGridRetiradoReporteCaja();
                        }
                        else
                        {
                            dataGridViewEITurno.FirstDisplayedScrollingRowIndex = e.OldValue;
                        }
                    }
                }
            }
        }

        private void ingresosDgvScroll(ScrollEventArgs e)
        {
            if (ingresoTurnoList.Count < totalEITurno && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
                {
                    if (e.NewValue > dataGridViewEITurno.Rows.Count - getDisplayedRowsCountEITurno())
                    {
                        //prevent loading from autoscroll.
                        TimeSpan ts = DateTime.Now - lastLoadingEITurno;
                        if (ts.TotalMilliseconds > 100)
                        {
                            firstVisibleRowEITurno = e.NewValue;
                            //dataGridItems.PerformLayout();
                            fillDataGridIngresosReporteCaja();
                        }
                        else
                        {
                            dataGridViewEITurno.FirstDisplayedScrollingRowIndex = e.OldValue;
                        }
                    }
                }
            }
        }

        private int getDisplayedRowsCountEITurno()
        {
            int count = dataGridViewEITurno.Rows[dataGridViewEITurno.FirstDisplayedScrollingRowIndex].Height;
            count = dataGridViewEITurno.Height / count;
            return count;
        }

        private void btnImprimirReporteCaja_Click(object sender, EventArgs e)
        {
            FormParametersReports formParametersReports = new FormParametersReports(this, 0, serverModeLAN);
            formParametersReports.StartPosition = FormStartPosition.CenterScreen;
            formParametersReports.ShowDialog();
        }

        private void resetVariablesEIDelTurno(int queryType)
        {
            queryTypeEITurno = queryType;
            queryEITurno = "";
            queryTotalEITurno = "";
            totalVentasDelTurno = 0;
            lastIdEITurno = 0;
            progressEITurno = 0;
            egresosTurnoList.Clear();
            dataGridViewEITurno.Rows.Clear();
        }

        private async Task fillTotalesVentasDelTurno()
        {
            Cursor.Current = Cursors.Default;
            double contadoTotal = 0;
            await Task.Run(() => {
                String query = "SELECT SUM(D." + LocalDatabase.CAMPO_ANTICIPO_DOC + ") FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " D " +
                    //" INNER JOIN " + LocalDatabase.TABLA_CLIENTES + " C ON D." + LocalDatabase.CAMPO_CLIENTEID_DOC + " = C." +LocalDatabase.CAMPO_ID_CLIENTE +
                    " WHERE (D." + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 2 + " OR D." + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 4 + ") AND D." +
                    LocalDatabase.CAMPO_CANCELADO_DOC + " = " + 0 + " AND D." + LocalDatabase.CAMPO_PAUSAR_DOC + " = " + 0 +
                    " AND (D." + LocalDatabase.CAMPO_FORMACOBROID_DOC + " != " + 71 + " AND D." + LocalDatabase.CAMPO_FORMACOBROID_DOC + " != " + 0 + ")";
                contadoTotal = DocumentModel.getDoubleValue(query);
                totalVendidoYCobrado += contadoTotal;
            });
            if (editTotalesVentasTurno.Equals(""))
                editTotalesVentasTurno.Text = ("Vendido $ 0 MXN");
            else editTotalesVentasTurno.Text = ("Total al Contado: " + contadoTotal.ToString("C", CultureInfo.CurrentCulture) + " MXN\r\n");
            Cursor.Current = Cursors.Default;
        }

        private async Task fillTotalesCreditosDelTurno()
        {
            Cursor.Current = Cursors.Default;
            String formatototales = "";
            List<FormasDeCobroModel> formasList = null;
            await Task.Run(() => {
                formasList = FormasDeCobroModel.getAllFormasDeCobro();
                double sumaCambio = 0;
                double sumaAnticiposVentasACredito = 0;
                double sumasDeImporte = 0.0;
                if (formasList != null)
                {
                    for (int i = 0; i < formasList.Count; i++)
                    {
                        if (formasList[i].FORMA_COBRO_ID != 71)
                        {
                            sumaAnticiposVentasACredito = DocumentModel.getAllTotalForAFormaDePagoAbono(formasList[i].FORMA_COBRO_ID);
                            //sumaCambio = ClsFormasDeCobroDocumentoModel.getSumCambioForAFormaDePagoInReporteDeCorte(formasList[i].FORMA_COBRO_CC_ID);
                            sumasDeImporte = sumaAnticiposVentasACredito;
                            totalVendidoYCobrado += sumasDeImporte;
                            //sumasDeImporte -= sumaCambio;
                            if (formasList[i].FORMA_COBRO_ID <= 9)
                            {
                                formatototales += "> 0" + formasList[i].FORMA_COBRO_ID + " " + formasList[i].NOMBRE + " = " +
                                        sumasDeImporte.ToString("C", CultureInfo.CurrentCulture) + " MXN\r\n";
                            }
                            else
                            {
                                formatototales += "> " + formasList[i].FORMA_COBRO_ID + " " + formasList[i].NOMBRE + " = " +
                                        sumasDeImporte.ToString("C", CultureInfo.CurrentCulture) + " MXN\r\n";
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
                String query = "SELECT SUM(d." + LocalDatabase.CAMPO_TOTAL_DOC + ") FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " d " +
                    " INNER JOIN " + LocalDatabase.TABLA_CLIENTES + " c ON d." + LocalDatabase.CAMPO_CLIENTEID_DOC + " = c." +
                    LocalDatabase.CAMPO_ID_CLIENTE + " WHERE d." + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 2 + " AND d." +
                    LocalDatabase.CAMPO_CANCELADO_DOC + " = " + 0 + " AND d." + LocalDatabase.CAMPO_PAUSAR_DOC + " = " + 0 +
                    " AND d." + LocalDatabase.CAMPO_FORMACOBROID_DOC + " = " + 71;
                double totalACredito = DocumentModel.getDoubleValue(query);
                editTotalesCreditsAbonosTurno.Text = "Total a Crédito: " + totalACredito.ToString("C", CultureInfo.CurrentCulture) + " MXN \r\n\r\n" +
                    "Abonos a Créditos \r\n" + formatototales+ "\r\n\r\n";
            }
            totalVendidoYCobrado += totalDeApertura;
            Cursor.Current = Cursors.Default;
        }

        private async Task fillTotalesPorFormasDeCobro()
        {
            String formatototales = "";
            List<FormasDeCobroModel> formasList = null;
            await Task.Run(() => {
                formasList = FormasDeCobroModel.getAllFormasDeCobro();
                double sumaTXT = 0;
                double sumaCambio = 0;
                double sumaDos = 0;
                double sumasDeImporte = 0.0;
                if (formasList != null)
                {
                    for (int i = 0; i < formasList.Count; i++)
                    {
                        if (formasList[i].FORMA_COBRO_ID != 71)
                        {
                            sumaTXT = FormasDeCobroDocumentoModel.getAllTotalForAFormaDePagoInReporteDeCorte(formasList[i].FORMA_COBRO_ID);
                            sumaCambio = FormasDeCobroDocumentoModel.getSumCambioForAFormaDePagoInReporteDeCorte(formasList[i].FORMA_COBRO_ID);
                            sumaDos = CuentasXCobrarModel.getAllTotalForAFormaDePagoAbono(formasList[i].FORMA_COBRO_ID);
                            sumasDeImporte = sumaTXT + sumaDos;
                            sumasDeImporte -= sumaCambio;
                            if (formasList[i].FORMA_COBRO_ID <= 9)
                            {
                                formatototales += "> 0" + formasList[i].FORMA_COBRO_ID + " " + formasList[i].NOMBRE + " = $" +
                                        MetodosGenerales.obtieneDosDecimales(sumasDeImporte) + " MXN\r\n";
                            }
                            else
                            {
                                formatototales += "> " + formasList[i].FORMA_COBRO_ID + " " + formasList[i].NOMBRE + " = $" +
                                        MetodosGenerales.obtieneDosDecimales(sumasDeImporte) + " MXN\r\n";
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
            editTotalesVentasTurno.Text += "\r\n" + "Totales Por Formas de Cobro" + "\r\n" + formatototales;
        }

        private async Task<double> getAllAbonos()
        {
            double total = 0;
            String formatoPagos = "";
            await Task.Run(() =>
            {
                List<CuentasXCobrarModel> pagosList = CuentasXCobrarModel.getAllPagos();
                if (pagosList != null)
                {
                    foreach (CuentasXCobrarModel pago in pagosList)
                    {
                        formatoPagos += "Folio: " + pago.creditDocumentFolio + " Total Cobrado: " + pago.amount.ToString("C", CultureInfo.CurrentCulture) + " MXN\r\n";
                        total += pago.amount;
                    }
                }
            });
            editTotalesCreditsAbonosTurno.Text += "Total De Pagos: "+total.ToString("C", CultureInfo.CurrentCulture)+ " MXN\r\n " +formatoPagos;
            totalVendidoYCobrado += total;
            return total;
        }

        private async Task fillTotaltesRetiros()
        {
            Cursor.Current = Cursors.Default;
            String textTotalRetiradoDesc = "";
            String textTotalIngresadoDesc = "";
            double sobrante = 0;
            double totalRetirado = 0;
            double totalIngresado = 0;
            List<FormasDeCobroModel> fcList = null;
            await Task.Run(() => {
                fcList = FormasDeCobroModel.getAllFormasDeCobro();
                String textFaltanteDesc = "";
                double totalFCRetiro = 0;
                double totalFCIngreso = 0;
                if (fcList != null)
                {
                    for (int i = 0; i < fcList.Count; i++)
                    {
                        totalFCRetiro = MontoRetiroModel.getAllWithdrawalAmountsFromACollectionForm(fcList[i].FORMA_COBRO_ID);
                        if (totalFCRetiro > 0)
                        {
                            textTotalRetiradoDesc += ">" + fcList[i].NOMBRE + " " + totalFCRetiro.ToString("C", CultureInfo.CurrentCulture) + "MXN\r\n";
                            totalRetirado += totalFCRetiro;
                        }
                        totalFCIngreso = MontoIngresoModel.getAllEntryAmountsFromACollectionForm(fcList[i].FORMA_COBRO_ID);
                        if (totalFCIngreso > 0)
                        {
                            textTotalIngresadoDesc += ">" + fcList[i].NOMBRE + " " + totalFCIngreso.ToString("C", CultureInfo.CurrentCulture) + "MXN\r\n";
                            totalIngresado += totalFCIngreso;
                        }
                    }
                }
            });
            sobrante = totalVendidoYCobrado + totalIngresado;
            editTotalEnCaja.Text = "Total en Caja: " + sobrante.ToString("C", CultureInfo.CurrentCulture) + " MXN";
            sobrante = sobrante - totalRetirado;
            double faltante = sobrante;
            if (faltante < 0)
            {
                editFaltante.Text = ("Faltante $ 0 MXN");
                editFaltante.ForeColor = Color.Blue;
                editSobrante.Text = ("Sobrante " + Math.Abs(faltante).ToString("C", CultureInfo.CurrentCulture) + " MXN");
                editSobrante.ForeColor = Color.Red;
            }
            else if (faltante == 0)
            {
                editFaltante.Text = ("Faltante $ 0 MXN");
                editFaltante.ForeColor = Color.Blue;
                editSobrante.Text = ("Sobrante $ 0 MXN");
                editSobrante.ForeColor = Color.Blue;
            }
            else
            {
                editFaltante.Text = ("Faltante " + Math.Abs(faltante).ToString("C", CultureInfo.CurrentCulture)+ " MXN");
                editFaltante.ForeColor = Color.Red;
                editSobrante.Text = ("Sobrante $ 0 MXN");
                editSobrante.ForeColor = Color.Blue;
            }
            editTotaltesEITurno.Text = (textTotalRetiradoDesc + "\r\nTotal Retirado: " + totalRetirado.ToString("C", CultureInfo.CurrentCulture) + " MXN \r\n\r\n");
            editTotaltesEITurno.Text += (textTotalIngresadoDesc + "\r\nTotal Ingresado: " + totalIngresado.ToString("C", CultureInfo.CurrentCulture) + " MXN");
            Cursor.Current = Cursors.Default;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormGeneralsReports_FormClosed(object sender, FormClosedEventArgs e)
        {
            resetVariablesCreditosDelTurno(0);
            resetVariablesVentasDelTurno(0);
            resetVariablesEIDelTurno(0);
            resetearValoresDocumentos(0);
            resetearValoresEIAnteriores(0);
            //callDeleteDocumentsProcess(false);
            //callDeleteWithdrawalsProcess(false);
        }

    }
}
