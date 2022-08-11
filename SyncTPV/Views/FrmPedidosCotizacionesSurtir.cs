using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using SyncTPV.Views.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Dynamic;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using Tulpep.NotificationWindow;
using wsROMClase;
using wsROMClases.Models;

namespace SyncTPV.Views
{
    public partial class FrmPedidosCotizacionesSurtir : Form
    {
        private int responseExistencias = 0;
        private int tabPosition = 0;
        private int LIMIT = 30;
        private int progressCotMos = 0, progressPrePedido = 0;
        private int lastIdCotMos = 0, lastIdPrePedido = 0;
        private int totalItemsCotMos = 0, totalItemsPrePedServer = 0, queryTypeCotMos = 0, queryTypePrePedido = 0;
        private static String queryCotMos = "", queryTotalsCotMos = "", folioCotMos = "";
        private static String queryPrePedidoTemp = "", queryPrePedidoReport = "", queryTotalsPrePedido = "", searchWordPrepedido = "";
        private DateTime lastLoadingCotMos, lastLoadingPrePedido;
        private int firstVisibleRowCotMos, firstVisibleRowPrePedido;
        private ScrollBars gridScrollBarsCotMos, gridScrollBarsPrePedido;
        private List<PedidosEncabezadoModel> cotMostradorList;
        private List<PedidosEncabezadoModel> cotMostradorListTemp;
        private List<PedidosEncabezadoModel> prePedidosList;
        private List<PedidosEncabezadoModel> prePedidosListTemp;
        private FormWaiting formWaiting;
        private int fiscalDocument = 0;
        public static int routeCajaIdPrePedido = 0;
        public static String routeCajaCodePrePedido = "";
        private List<RutaModel> routesList;
        private List<ClsCajaModel> cajasList;
        private bool reporteTodasLasRutas = false;
        private int idAgentByRoute = 0;
        private bool permissionPrepedido = false, serverModeLAN = false;
        private String panelInstance = "";
        private int positionFiscalItemField = 6;
        private bool webActive = false, cotmosActive = false;
        private String codigoCaja = "";

        public FrmPedidosCotizacionesSurtir(bool cotmosActive)
        {
            lastIdCotMos = PedidosEncabezadoModel.getLastId();
            permissionPrepedido = UserModel.doYouHavePermissionPrepedido();
            serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
            InitializeComponent();
            btnClose.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.back_white, 35, 35);
            cotMostradorList = new List<PedidosEncabezadoModel>();
            prePedidosList = new List<PedidosEncabezadoModel>();
            this.editBusquedaFolioCotizacionesMostrador.KeyPress += new KeyPressEventHandler(editBusqueda_KeyPress);
            this.btnBuscarFolioCotizacionesMostrador.Click += new EventHandler(btnBuscar_Click);
            this.dataGridViewCotizacionesM.CellClick += new DataGridViewCellEventHandler(dataGridViewPedidosCotizaciones_CellClick);
            btnSearchPrePedido.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.search_black, 20, 20);
            btnPdfPrepedidos.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.pdf_white, 35, 35);
            positionFiscalItemField = ConfiguracionModel.getPositionFiscalItemField();
            if (permissionPrepedido)
                LIMIT = 10;
            this.cotmosActive = cotmosActive;
        }

        private void FrmPedidosCotizacionesSurtir_Load(object sender, EventArgs e) {
            panelInstance = InstanceSQLSEModel.getStringPanelInstance();
            deleteAllPedidosEncabezadoSurtidos();
            validarPermisoPrepedido();
            loadInitialData();
        }

        private async Task loadInitialData()
        {
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                dynamic responseWeb = ConfiguracionModel.webActive();
                if (responseWeb.value == 1)
                {
                    value = 1;
                    webActive = responseWeb.active;
                }
                else
                {
                    value = responseWeb.value;
                    description = responseWeb.description;
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
        private void tabControlPedidosCotizaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            validateTabPositionIsVisible();
        }

        private async Task validateTabPositionIsVisible()
        {
            tabPosition = tabControlPedidosCotizaciones.SelectedIndex;
            if (permissionPrepedido)
                tabPosition = 1;
            switch (tabPosition)
            {
                case 0:
                    {
                        await fillDataGridViewCotMostrador();
                        break;
                    }
                case 1:
                    {
                        tabControlPedidosCotizaciones.SelectedIndex = 0;
                        await enableAllPreorders();
                        break;
                    }
                case 2:
                    {
                        PopupNotifier popup = new PopupNotifier();
                        popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                        popup.TitleColor = Color.Blue;
                        popup.TitleText = "Modulo en Desarrollo";
                        popup.ContentText = "Oops esta opción aún se encuentra en desarrollo!";
                        popup.ContentColor = Color.Red;
                        popup.Popup();
                        break;
                    }
            }
        }

        private async Task enableAllPreorders()
        {
            checkBoxTodosLosPrepedidos.Checked = true;
            comboBoxRutaCajaPrepedido.Enabled = false;
            comboBoxSelectRutas.Enabled = false;
            btnUpdateRoutes.Enabled = false;
            queryTypePrePedido = 2;
            resetearValoresPrePedidos(queryTypePrePedido);
            await fillDataGridViewPrePedidos();
        }

        private void validarPermisoPrepedido()
        {
            if (permissionPrepedido)
            {
                //tabPageCotizacionesMostrador
                btnUpdateRoutes.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.update_data_black, 35, 35);
                tabControlPedidosCotizaciones.TabPages.Remove(tabControlPedidosCotizaciones.TabPages[0]);
                tabControlPedidosCotizaciones.TabPages.Remove(tabControlPedidosCotizaciones.TabPages[1]);
                btnPdfPrepedidos.Visible = true;
                tabPosition = 1;
            } else
            {
                tabControlPedidosCotizaciones.TabPages.Remove(tabControlPedidosCotizaciones.TabPages[2]);
                tabControlPedidosCotizaciones.TabPages.Remove(tabControlPedidosCotizaciones.TabPages[1]);
                btnPdfPrepedidos.Visible = false;
                tabPosition = 0;
                validateTabPositionIsVisible();
            }
            //loadData();
        }

        private async Task deleteAllPedidosEncabezadoSurtidos()
        {
            await Task.Run(async () => {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    String query = "SELECT " + LocalDatabase.CAMPO_ID_PEDIDOENCABEZADO + " FROM " +
                    LocalDatabase.TABLA_PEDIDOENCABEZADO + " WHERE " +LocalDatabase.CAMPO_SURTIDO_PE + " = " + 1 +
                    " AND " + LocalDatabase.CAMPO_LISTO_PE + " = " + 1+" AND "+LocalDatabase.CAMPO_TYPE_PE+" = "+
                    PedidosEncabezadoModel.TYPE_COTIZACIONMOSTRADOR;
                    List<int> idsToDelete = PedidosEncabezadoModel.getIntListVlues(db, query);
                    if (idsToDelete != null)
                    {
                        foreach (int id in idsToDelete)
                        {
                            query = "DELETE FROM " + LocalDatabase.TABLA_PEDIDODETALLE + " WHERE " + LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_PD + " = " + id;
                            PedidoDetalleModel.deleteARecord(db, query);
                            query = "DELETE FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " WHERE " +LocalDatabase.CAMPO_ID_PEDIDOENCABEZADO+" = "+
                            id;
                            PedidosEncabezadoModel.deleteARecord(db, query);
                        }
                    }
                } catch (SQLiteException e) {
                    SECUDOC.writeLog(e.ToString());
                } finally {
                    if (db != null && db.State == ConnectionState.Open)
                        db.Close();
                }
            });
            int lastIdServer = 0;
            removeAllRepeatedMovesFromPreOrders(lastIdServer);
        }

        public static async Task removeAllRepeatedMovesFromPreOrders(int lastIdServer)
        {
            await Task.Run(async () =>
            {
                String query = "SELECT " + LocalDatabase.CAMPO_DOCUMENTOID_PE + " FROM " +
                    LocalDatabase.TABLA_PEDIDOENCABEZADO + " WHERE " + LocalDatabase.CAMPO_SURTIDO_PE + " = 0 " +
                    "AND " + LocalDatabase.CAMPO_LISTO_PE + " = 0 AND " + LocalDatabase.CAMPO_TYPE_PE + " = " +
                    PedidosEncabezadoModel.TYPE_PREPEDIDOS+" AND "+LocalDatabase.CAMPO_DOCUMENTOID_PE+" >= "+lastIdServer;                
                List<int> idsToDelete = PedidosEncabezadoModel.getIntListVlues(query);
                if (idsToDelete != null)
                {
                    foreach (int idPedido in idsToDelete)
                    {
                        query = "DELETE FROM "+LocalDatabase.TABLA_PEDIDODETALLE+" WHERE "+LocalDatabase.CAMPO_ID_PD+" IN (" +
                        "SELECT "+ LocalDatabase.CAMPO_ID_PD + " FROM "+ LocalDatabase.TABLA_PEDIDODETALLE + " WHERE "+
                        LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_PD + " = "+ idPedido + " AND "+ LocalDatabase.CAMPO_ID_PD + " != (" +
                        "SELECT "+ LocalDatabase.CAMPO_ID_PD + " FROM "+ LocalDatabase.TABLA_PEDIDODETALLE + " WHERE "+ 
                        LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_PD + " = "+ idPedido + " ORDER BY "+ LocalDatabase.CAMPO_ID_PD + " ASC LIMIT 1" +
                        "))";
                        PedidoDetalleModel.deleteRecordsWithoutParameters(query);
                    }
                }
            });
        }

        int positionComboRutas = 0;

        private async Task fillComboBoxRutas(bool fillRoutes)
        {
            if (fillRoutes)
            {
                routesList = await getAllRoutes();
                if (routesList != null)
                {
                    comboBoxSelectRutas.DataSource = routesList;
                    comboBoxSelectRutas.DisplayMember = "name";
                    comboBoxSelectRutas.ValueMember = "id";
                    comboBoxSelectRutas.Select();
                    comboBoxSelectRutas.SelectedIndex = positionComboRutas;
                }
                else
                {
                    formWaiting = new FormWaiting(this, FormWaiting.CALL_RUTAS, 0, true, webActive, codigoCaja);
                    formWaiting.StartPosition = FormStartPosition.CenterScreen;
                    formWaiting.ShowDialog();
                }
            } else
            {
                cajasList = await getAllCheckouts();
                if (cajasList != null)
                {
                    comboBoxSelectRutas.DataSource = cajasList;
                    comboBoxSelectRutas.DisplayMember = "name";
                    comboBoxSelectRutas.ValueMember = "id";
                    comboBoxSelectRutas.Select();
                    comboBoxSelectRutas.SelectedIndex = positionComboRutas;
                }
                else
                {
                    formWaiting = new FormWaiting(this, FormWaiting.CALL_RUTAS, 0, false, webActive, codigoCaja);
                    formWaiting.StartPosition = FormStartPosition.CenterScreen;
                    formWaiting.ShowDialog();
                }
            }
        }



        private void btnUpdateRoutes_Click(object sender, EventArgs e)
        {
            if (comboBoxRutaCajaPrepedido.Enabled)
            {
                if (comboBoxRutaCajaPrepedido.SelectedIndex == 0)
                {
                    formWaiting = new FormWaiting(this, FormWaiting.CALL_RUTAS, 0, true, webActive, codigoCaja);
                    formWaiting.StartPosition = FormStartPosition.CenterScreen;
                    formWaiting.ShowDialog();
                } else
                {
                    formWaiting = new FormWaiting(this, FormWaiting.CALL_RUTAS, 0, false, webActive, codigoCaja);
                    formWaiting.StartPosition = FormStartPosition.CenterScreen;
                    formWaiting.ShowDialog();
                }
            }
        }

        public async Task doDownloadRoutesOrCheckoutsProcess(bool getRoutes)
        {
            if (serverModeLAN)
            {
                if (getRoutes)
                {
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    dynamic response = await RutaController.getAllRoutesWithSQL(panelInstance, 0, "");
                    if (response != null)
                    {
                        if (response.value == 1)
                        {
                            await fillComboBoxRutas(true);
                        }
                        else
                        {
                            FormMessage formMessage = new FormMessage("Validar Información", "" + response.description, 2);
                            formMessage.ShowDialog();
                        }
                    }
                } else
                {
                    dynamic response = await CajaController.getAllCajasLAN();
                    if (response.value == 1)
                    {
                        await fillComboBoxRutas(false);
                    }
                    else
                    {
                        FormMessage formMessage = new FormMessage("Validar Información", "" + response.description, 2);
                        formMessage.ShowDialog();
                    }
                }
                if (formWaiting != null)
                {
                    formWaiting.Dispose();
                    formWaiting.Close();
                }
            }
            else
            {
                if (getRoutes)
                {
                    dynamic response = await RutaController.getAllRoutesWithWs(0, "");
                    if (response != null)
                    {
                        if (response.value == 1)
                        {
                            await fillComboBoxRutas(true);
                        }
                        else
                        {
                            FormMessage formMessage = new FormMessage("Validar Información", "" + response.description, 2);
                            formMessage.ShowDialog();
                        }
                    }
                } else
                {
                    dynamic response = await CajaController.getAllCajasAPI();
                    if (response.value == 1)
                    {
                        await fillComboBoxRutas(false);
                    }
                    else
                    {
                        FormMessage formMessage = new FormMessage("Validar Información", "" + response.description, 2);
                        formMessage.ShowDialog();
                    }
                }
                if (formWaiting != null)
                {
                    formWaiting.Dispose();
                    formWaiting.Close();
                }
            }
        }

        private async Task<List<RutaModel>> getAllRoutes()
        {
            List < RutaModel > routesList = null;
            await Task.Run(() => {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_RUTA + " WHERE " + LocalDatabase.CAMPO_ID_RUTA + " > " + 0 +
                    " ORDER BY " + LocalDatabase.CAMPO_ID_RUTA;
                routesList = RutaModel.getAllRoutes(query);
            });
            return routesList;
        }

        private async Task<List<ClsCajaModel>> getAllCheckouts()
        {
            List<ClsCajaModel> checkoutsList = null;
            await Task.Run(() => {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_CAJA+" WHERE "+LocalDatabase.CAMPO_ID_CAJA+" > 0 "+
                "ORDER BY "+LocalDatabase.CAMPO_ID_CAJA;
                checkoutsList = CajaModel.getAllCajas(query);
            });
            return checkoutsList;
        }

        bool lecturaDelComboRutas = false;

        private async void comboBoxSelectRutas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!lecturaDelComboRutas)
            {
                lecturaDelComboRutas = true;
                if (comboBoxRutaCajaPrepedido.Enabled)
                {
                    if (comboBoxRutaCajaPrepedido.SelectedIndex == 0)
                    {
                        await validateRouteSelected(true);
                    }
                    else
                    {
                        await validateRouteSelected(false);
                    }
                } else lecturaDelComboRutas = false;
            }
        }

        private async Task validateRouteSelected(bool route)
        {
            if (route)
            {
                if (routesList != null && routesList.Count > 0)
                {
                    if (comboBoxSelectRutas.Items != null && comboBoxSelectRutas.Items.Count > 0)
                    {
                        routeCajaIdPrePedido = routesList[comboBoxSelectRutas.SelectedIndex].id;
                        routeCajaCodePrePedido = routesList[comboBoxSelectRutas.SelectedIndex].code;
                        if (!routeCajaCodePrePedido.Equals(""))
                        {
                            await loadPrepedidosInitialData();
                        }
                        else
                        {
                            FormMessage formMessage = new FormMessage("Importante", "No pudimos encontrar la ruta seleccionada", 2);
                            formMessage.ShowDialog();
                        }
                    }
                }
                else
                {
                    FormMessage formMessage = new FormMessage("Importante", "Actualizar rutas para validar si existen", 2);
                    formMessage.ShowDialog();
                }
            } else
            {
                if (cajasList != null && cajasList.Count > 0)
                {
                    if (comboBoxSelectRutas.Items != null && comboBoxSelectRutas.Items.Count > 0)
                    {
                        routeCajaIdPrePedido = cajasList[comboBoxSelectRutas.SelectedIndex].id;
                        routeCajaCodePrePedido = cajasList[comboBoxSelectRutas.SelectedIndex].code;
                        if (!routeCajaCodePrePedido.Equals(""))
                        {
                            await loadPrepedidosInitialData();
                        }
                        else
                        {
                            FormMessage formMessage = new FormMessage("Importante", "No pudimos encontrar la ruta seleccionada", 2);
                            formMessage.ShowDialog();
                        }
                    }
                }
                else
                {
                    FormMessage formMessage = new FormMessage("Importante", "Actualizar rutas para validar si existen", 2);
                    formMessage.ShowDialog();
                }
            }
            lecturaDelComboRutas = false;
        }

        private async Task loadPrepedidosInitialData()
        {
            searchWordPrepedido = editBuscarPrepedido.Text.ToString().Trim();
            if (searchWordPrepedido.Equals(""))
            {
                queryTypePrePedido = 0;
                resetearValoresPrePedidos(queryTypePrePedido);
                await fillDataGridViewPrePedidos();
            } else
            {
                queryTypePrePedido = 1;
                resetearValoresPrePedidos(queryTypePrePedido);
                await fillDataGridViewPrePedidos();
            }
        }

        private void hideScrollBars()
        {
            //imgSinDatos.Image = ClsMetodosGenerales.redimencionarBitmap(Properties.Resources.logosynctpvmoving, 300, 300);
            //imgSinDatos.Visible = true;
            gridScrollBarsCotMos = dataGridViewCotizacionesM.ScrollBars;
            //dataGridItems.ScrollBars = ScrollBars.None;
        }

        private async Task fillDataGridViewCotMostrador()
        {
            hideScrollBars();
            lastLoadingCotMos = DateTime.Now;
            cotMostradorListTemp = await getAllCotizacionesMostrador();
            if (cotMostradorListTemp != null)
            {
                progressCotMos += cotMostradorListTemp.Count;
                cotMostradorList.AddRange(cotMostradorListTemp);
                if (cotMostradorList.Count > 0 && dataGridViewCotizacionesM.ColumnHeadersVisible == false)
                    dataGridViewCotizacionesM.ColumnHeadersVisible = true;
                for (int i = 0; i < cotMostradorListTemp.Count; i++)
                {
                    int n = dataGridViewCotizacionesM.Rows.Add();
                    dataGridViewCotizacionesM.Rows[n].Cells[0].Value = cotMostradorListTemp[i].id + "";
                    dataGridViewCotizacionesM.Columns["id"].Visible = false;
                    dataGridViewCotizacionesM.Rows[n].Cells[1].Value = cotMostradorListTemp[i].idDocumento;
                    dataGridViewCotizacionesM.Columns["idDocumento"].Visible = false;
                    if (cotMostradorListTemp[i].nombreCliente.Equals(""))
                    {
                        if (cotMostradorListTemp[i].clienteId < 0)
                            dataGridViewCotizacionesM.Rows[n].Cells[2].Value = "Cliente Nuevo";
                        else dataGridViewCotizacionesM.Rows[n].Cells[2].Value = cotMostradorListTemp[i].nombreCliente;
                    }
                    else dataGridViewCotizacionesM.Rows[n].Cells[2].Value = cotMostradorListTemp[i].nombreCliente;
                    if (cotMostradorListTemp[i].agenteId > 0)
                    {
                        String agentName = UserModel.getNameUser(cotMostradorListTemp[i].agenteId);
                        dataGridViewCotizacionesM.Rows[n].Cells[3].Value = agentName;
                    } else dataGridViewCotizacionesM.Rows[n].Cells[3].Value = "No Encontrado "+ cotMostradorListTemp[i].agenteId;
                    dataGridViewCotizacionesM.Rows[n].Cells[4].Value = cotMostradorListTemp[i].fechaHora;
                    dataGridViewCotizacionesM.Rows[n].Cells[5].Value = cotMostradorListTemp[i].folio;
                    dataGridViewCotizacionesM.Columns["folio"].Width = 120;
                    dataGridViewCotizacionesM.Rows[n].Cells[6].Value = cotMostradorListTemp[i].subtotal.ToString("C", CultureInfo.CurrentCulture);
                    dataGridViewCotizacionesM.Rows[n].Cells[7].Value = cotMostradorListTemp[i].descuento.ToString("C", CultureInfo.CurrentCulture);
                    dataGridViewCotizacionesM.Rows[n].Cells[8].Value = cotMostradorListTemp[i].total.ToString("C", CultureInfo.CurrentCulture)+" MXN";
                    dataGridViewCotizacionesM.Rows[n].Cells[9].Value = cotMostradorListTemp[i].type;
                    dataGridViewCotizacionesM.Columns["tipo"].Visible = false;
                    dataGridViewCotizacionesM.Rows[n].Cells[10].Value = cotMostradorListTemp[i].surtido;
                    dataGridViewCotizacionesM.Columns["status"].Visible = false;
                    dataGridViewCotizacionesM.Rows[n].Cells[11].Value = "Surtir";
                    dataGridViewCotizacionesM.Columns[11].Width = 90;
                    dataGridViewCotizacionesM.Rows[n].Cells[12].Value = "Eliminar";
                    dataGridViewCotizacionesM.Columns[12].Width = 95;
                }
                dataGridViewCotizacionesM.PerformLayout();
                cotMostradorListTemp.Clear();
                if (cotMostradorList.Count > 0)
                    lastIdCotMos = Convert.ToInt32(cotMostradorList[cotMostradorList.Count - 1].id);
                //imgSinDatos.Visible = false;
            }
            else
            {
                //if (progress == 0)
                    //imgSinDatos.Visible = true;
            }
            textTotalRecords.Text = "Cotizaciones: " + totalItemsCotMos.ToString().Trim();
            //reset displayed row
            if (firstVisibleRowCotMos > -1)
            {
                showScrollBars();
                if (cotMostradorList.Count > 0)
                    dataGridViewCotizacionesM.FirstDisplayedScrollingRowIndex = firstVisibleRowCotMos;
                //imgSinDatos.Visible = false;
            }
        }

        private void showScrollBars()
        {
            dataGridViewCotizacionesM.ScrollBars = gridScrollBarsCotMos;
        }

        private void dataGridItems_Scroll(object sender, ScrollEventArgs e)
        {
            if (cotMostradorList.Count < totalItemsCotMos && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
                {
                    if (e.NewValue > dataGridViewCotizacionesM.Rows.Count - getDisplayedRowsCountCotizaciones())
                    {
                        //prevent loading from autoscroll.
                        TimeSpan ts = DateTime.Now - lastLoadingCotMos;
                        if (ts.TotalMilliseconds > 100)
                        {
                            firstVisibleRowCotMos = e.NewValue;
                            //dataGridItems.PerformLayout();
                            fillDataGridViewCotMostrador();
                        }
                        else
                        {
                            dataGridViewCotizacionesM.FirstDisplayedScrollingRowIndex = e.OldValue;
                        }
                    }
                }
            }
        }

        private int getDisplayedRowsCountCotizaciones()
        {
            int count = dataGridViewCotizacionesM.Rows[dataGridViewCotizacionesM.FirstDisplayedScrollingRowIndex].Height;
            count = dataGridViewCotizacionesM.Height / count;
            return count;
        }

        private async Task<List<PedidosEncabezadoModel>> getAllCotizacionesMostrador()
        {
            List<PedidosEncabezadoModel> documentsList = null;
            await Task.Run(() => {
                if (queryTypeCotMos == 0) {
                    queryCotMos = "SELECT * FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " WHERE " + 
                    LocalDatabase.CAMPO_SURTIDO_PE + " = 0 AND " +
                    LocalDatabase.CAMPO_LISTO_PE + " != 2 AND " +
                    LocalDatabase.CAMPO_TYPE_PE+" = 3 AND "+
                    LocalDatabase.CAMPO_ID_PEDIDOENCABEZADO + " != 0 AND "+
                    LocalDatabase.CAMPO_ID_PEDIDOENCABEZADO + " <= " + lastIdCotMos + 
                    " ORDER BY " + LocalDatabase.CAMPO_ID_PEDIDOENCABEZADO + " DESC LIMIT " + LIMIT;
                    queryTotalsCotMos = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " WHERE " + 
                    LocalDatabase.CAMPO_SURTIDO_PE + " = 0 AND " +
                    LocalDatabase.CAMPO_LISTO_PE + " != 2 AND "+
                    LocalDatabase.CAMPO_TYPE_PE+" = 3";
                    totalItemsCotMos = PedidosEncabezadoModel.getIntValue(queryTotalsCotMos);
                    documentsList = PedidosEncabezadoModel.getAllDocuments(queryCotMos);
                } else if (queryTypeCotMos == 1)
                {
                    queryCotMos = "SELECT * FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " WHERE " + 
                    LocalDatabase.CAMPO_SURTIDO_PE + " = 0 AND " +
                    LocalDatabase.CAMPO_LISTO_PE + " != 2 AND " +
                    LocalDatabase.CAMPO_TYPE_PE+" = 3 AND ("+
                    LocalDatabase.CAMPO_CNOMBRECLIENTE_PE + " LIKE @nombreCliente OR " + LocalDatabase.CAMPO_CFOLIO_PE + " LIKE @folio) AND " +
                    LocalDatabase.CAMPO_ID_PEDIDOENCABEZADO+" != 0 AND "+
                    LocalDatabase.CAMPO_ID_PEDIDOENCABEZADO +" <= " + lastIdCotMos + 
                    " ORDER BY " + LocalDatabase.CAMPO_ID_PEDIDOENCABEZADO + " DESC LIMIT " + LIMIT;
                    queryTotalsCotMos = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " WHERE " + " WHERE " + 
                    LocalDatabase.CAMPO_SURTIDO_PE + " = 0 AND " +
                    LocalDatabase.CAMPO_LISTO_PE + " != 2 AND " +
                    LocalDatabase.CAMPO_TYPE_PE+" = 3 AND ("+
                    LocalDatabase.CAMPO_CNOMBRECLIENTE_PE + " LIKE @nombreCliente OR " + 
                    LocalDatabase.CAMPO_CFOLIO_PE + " LIKE @folio)";
                    totalItemsCotMos = PedidosEncabezadoModel.getIntValueWithParameters(queryTotalsCotMos, "nombreCliente", folioCotMos,
                        "folio", folioCotMos);
                    documentsList = PedidosEncabezadoModel.getAllDocumentsWithParametersToSearch(queryCotMos, "nombreCliente", folioCotMos,
                         "folio", folioCotMos);
                }
            });
            return documentsList;
        }

        private void editBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                folioCotMos = editBusquedaFolioCotizacionesMostrador.Text.Trim();
                if (!folioCotMos.Equals(""))
                {
                    queryTypeCotMos = 1;
                    resetearValoresCotMostrador(queryTypeCotMos);
                    fillDataGridViewCotMostrador();
                }
                else
                {
                    queryTypeCotMos = 0;
                    resetearValoresCotMostrador(queryTypeCotMos);
                    fillDataGridViewCotMostrador();
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            folioCotMos = editBusquedaFolioCotizacionesMostrador.Text.Trim();
            if (!folioCotMos.Equals(""))
            {
                queryTypeCotMos = 1;
                resetearValoresCotMostrador(queryTypeCotMos);
                fillDataGridViewCotMostrador();
            }
            else
            {
                queryTypeCotMos = 0;
                resetearValoresCotMostrador(queryTypeCotMos);
                fillDataGridViewCotMostrador();
            }
        }

        private void dataGridViewPedidosCotizaciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.ColumnIndex < 11) {
                int idPedCot = Convert.ToInt32(dataGridViewCotizacionesM.Rows[dataGridViewCotizacionesM.CurrentRow.Index].Cells[1].Value.ToString().Trim());
                FormMovimientos frmMovimientos = new FormMovimientos(idPedCot, FormMovimientos.TIPOMOVPED, e.RowIndex, cotmosActive);
                frmMovimientos.ShowDialog();
            } else if (e.ColumnIndex == 11) {
                String idText = dataGridViewCotizacionesM.Rows[dataGridViewCotizacionesM.CurrentRow.Index].Cells[1].Value.ToString();
                int idCotizacion = Convert.ToInt32(idText);
                if (serverModeLAN)
                {
                    callProcessSaveDocument(idCotizacion);
                } else
                {
                    int itemDoesNotExist = validateIdAllItemsExist(idCotizacion);
                    if (itemDoesNotExist == 0)
                        callProcessSaveDocument(idCotizacion);
                    else
                    {
                        FormMessage fm = new FormMessage("Productos o Servicios No Encontrados", "Tienes que actualizar los productos para poder surtir el Documento", 2);
                        fm.ShowDialog();
                    }
                }
            } else if (e.ColumnIndex == 12)
            {
                processToDeleteCotizacionMostrador(e.RowIndex);
            }
        }

        private async Task callProcessSaveDocument(int idCotizacion)
        {
            formWaiting = new FormWaiting(this, 3, idCotizacion, false, webActive, codigoCaja);
            formWaiting.ShowDialog();
        }

        private async Task processToDeleteCotizacionMostrador(int positionAEliminar)
        {
            FrmConfirmation fm = new FrmConfirmation("Cancelar Cotización", "Al cancelar la Cotización de Mostrador será eliminada permanentemente!\n ¿Desea Continuar?");
            fm.StartPosition = FormStartPosition.CenterScreen;
            fm.ShowDialog();
            if (FrmConfirmation.confirmation)
            {
                String idText = dataGridViewCotizacionesM.Rows[dataGridViewCotizacionesM.CurrentRow.Index].Cells[1].Value.ToString().Trim();
                int idPedCot = Convert.ToInt32(idText);
                String query = "UPDATE " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " SET " + LocalDatabase.CAMPO_LISTO_PE + " = 2 " +
                    "WHERE " + LocalDatabase.CAMPO_DOCUMENTOID_PE + " = " + idPedCot;
                if (PedidosEncabezadoModel.updateARecord(query))
                {
                    List<int> idsToDelete = new List<int>();
                    idsToDelete.Add(idPedCot);
                    int response = 0;
                    if (ConfiguracionModel.isLANPermissionActivated())
                        response = await CotizacionesMostradorController.deleteCotizacionesMostradorLAN(idsToDelete);
                    else response = await CotizacionesMostradorController.deleteCotizacionesMostrador(idsToDelete);
                    if (response == 1)
                    {
                        /*PopupNotifier popup = new PopupNotifier();
                        popup.Image = ClsMetodosGenerales.redimencionarImagenes(Properties.Resources.caution, 100, 100);
                        popup.TitleColor = Color.Red;
                        popup.TitleText = "Cotización Eliminada";
                        popup.ContentText = "La cotización de mostrador fue eliminada correctamente";
                        popup.ContentColor = Color.Red;
                        popup.Popup();*/
                    } else if (response == 404)
                    {
                        /*PopupNotifier popup = new PopupNotifier();
                        popup.Image = ClsMetodosGenerales.redimencionarImagenes(Properties.Resources.caution, 100, 100);
                        popup.TitleColor = Color.Red;
                        popup.TitleText = "Cotización Eliminada";
                        popup.ContentText = "La cotización de mostrador fue eliminada";
                        popup.ContentColor = Color.Red;
                        popup.Popup();*/
                    } else
                    {
                        /*PopupNotifier popup = new PopupNotifier();
                        popup.Image = ClsMetodosGenerales.redimencionarImagenes(Properties.Resources.caution, 100, 100);
                        popup.TitleColor = Color.Red;
                        popup.TitleText = "Cotización Eliminada";
                        popup.ContentText = "La cotización de mostrador fue eliminada en el servidor";
                        popup.ContentColor = Color.Red;
                        popup.Popup();*/
                    }
                    dataGridViewCotizacionesM.Rows.Remove(dataGridViewCotizacionesM.Rows[positionAEliminar]);
                }
            }
        }

        private async Task<int> getIdAgenteByCodigoRutaOCaja(bool comboRutaOCajaActivated, int comboRutaOCajaId)
        {
            int idAgente = 0;
            bool rutaOCajaNoAsignada = false;
            await Task.Run(async () =>
            {
                if (queryTypePrePedido != 2)
                {
                    if (comboRutaOCajaActivated)
                    {
                        if (comboRutaOCajaId == 0)
                        {
                            String queryGetAgentByRoute = "SELECT " + LocalDatabase.CAMPO_ID_USUARIO + " FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " +
                    LocalDatabase.CAMPO_RUTA_USER + " = '" + routeCajaCodePrePedido + "'";
                            idAgente = RutaModel.getIntValue(queryGetAgentByRoute);
                            if (idAgente != 0)
                                rutaOCajaNoAsignada = true;
                        }
                        else
                        {
                            idAgente = CajaModel.getIdByCodeBox(routeCajaCodePrePedido);
                            if (idAgente != 0)
                                rutaOCajaNoAsignada = true;
                        }
                    }
                    else
                    {
                        queryTypePrePedido = 2;
                        idAgente = 0;
                        rutaOCajaNoAsignada = true;
                    }
                }
                else rutaOCajaNoAsignada = true;
            });
            if (!rutaOCajaNoAsignada)
            {
                FormMessage formMessage = new FormMessage("Ruta o Caja No Asignada", "Tienes que asignar la Ruta o la Caja en el SyncPANEL para poder buscar sus " +
                    "Prepedidos", 3);
                formMessage.ShowDialog();
            }
            return idAgente;
        }

        private async Task fillDataGridViewPrePedidos() {
            try
            {
                gridScrollBarsPrePedido = dataGridViewPrePedidos.ScrollBars;
                lastLoadingPrePedido = DateTime.Now;
                prePedidosListTemp = await getAllPrePedidos(idAgentByRoute);
                if (prePedidosListTemp != null)
                {
                    progressPrePedido += prePedidosListTemp.Count;
                    prePedidosList.AddRange(prePedidosListTemp);
                    for (int i = 0; i < prePedidosListTemp.Count; i++)
                    {
                        int n = dataGridViewPrePedidos.Rows.Add();
                        dataGridViewPrePedidos.Rows[n].Height = 70;
                        dataGridViewPrePedidos.Rows[n].Cells[0].Value = prePedidosListTemp[i].id + "";
                        dataGridViewPrePedidos.Columns["idPrePedido"].Visible = false;
                        dataGridViewPrePedidos.Rows[n].Cells[1].Value = prePedidosListTemp[i].idDocumento;
                        dataGridViewPrePedidos.Columns["idDocumentoPrePedido"].Visible = false;
                        if (prePedidosListTemp[i].nombreCliente.Equals(""))
                        {
                            String codigoCliente = "";
                            String customerName = CustomerModel.getName(prePedidosListTemp[i].clienteId);
                            dataGridViewPrePedidos.Rows[n].Cells[2].Value = codigoCliente + "\r\n" + customerName;
                            dataGridViewPrePedidos.Columns["clientePrePedido"].Width = 125;
                        }
                        else
                        {
                            String customerName = CustomerModel.getName(prePedidosListTemp[i].clienteId);
                            dataGridViewPrePedidos.Rows[n].Cells[2].Value = prePedidosListTemp[i].nombreCliente + "\r\n" + customerName;
                            dataGridViewPrePedidos.Columns["clientePrePedido"].Width = 125;
                        }
                        if (prePedidosListTemp[i].agenteId != 0)
                        {
                            String query = "SELECT " + LocalDatabase.CAMPO_NOMBRE_USER + " FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " +
                                LocalDatabase.CAMPO_ID_USUARIO + " = " + prePedidosListTemp[i].agenteId;
                            String agentName = UserModel.getAStringValueForAnyUser(query);
                            dataGridViewPrePedidos.Rows[n].Cells[3].Value = agentName;
                            dataGridViewPrePedidos.Columns["agentePrePedido"].Width = 120;
                        }
                        else
                        {
                            dataGridViewPrePedidos.Rows[n].Cells[3].Value = prePedidosListTemp[i].agenteId;
                            dataGridViewPrePedidos.Columns["agentePrePedido"].Width = 120;
                        }
                        dataGridViewPrePedidos.Rows[n].Cells[4].Value = prePedidosListTemp[i].fechaHora;
                        dataGridViewPrePedidos.Rows[n].Cells[5].Value = prePedidosListTemp[i].folio;
                        dataGridViewPrePedidos.Columns["folioVentaPrePedido"].Width = 120;
                        dataGridViewPrePedidos.Rows[n].Cells[6].Value = prePedidosListTemp[i].observation;
                        dataGridViewPrePedidos.Columns["observationPrePedido"].Visible = true;
                        if (prePedidosListTemp[i].facturar == 1)
                            dataGridViewPrePedidos.Rows[n].Cells[7].Value = "Sí";
                        else dataGridViewPrePedidos.Rows[n].Cells[7].Value = "No";
                        dataGridViewPrePedidos.Columns["facturarPrePedido"].Visible = false;
                        if (prePedidosListTemp[i].listo == 0)
                            dataGridViewPrePedidos.Rows[n].Cells[8].Value = "Sin Surtir";
                        else dataGridViewPrePedidos.Rows[n].Cells[8].Value = "Surtido";
                        String query1 = "SELECT * FROM " + LocalDatabase.TABLA_PEDIDODETALLE + " WHERE " + LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_PD +
                            " = " + prePedidosListTemp[i].idDocumento;
                        List<PedidoDetalleModel> movesList = PedidoDetalleModel.getAllMovements(query1);
                        if (movesList != null)
                        {
                            dataGridViewPrePedidos.Rows[n].Cells[9].Value = movesList[0].unidadesNoConvertibles;
                            String nonConvertibleUnitName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(movesList[0].unidadNoConvertibleId);
                            dataGridViewPrePedidos.Rows[n].Cells[10].Value = nonConvertibleUnitName;
                        }
                        else
                        {
                            dataGridViewPrePedidos.Rows[n].Cells[9].Value = 0;
                            dataGridViewPrePedidos.Rows[n].Cells[10].Value = "Unidades";
                        }
                        dataGridViewPrePedidos.Rows[n].Cells[11].Value = "Surtir";
                        dataGridViewPrePedidos.Columns["statusPrePedito"].Visible = false;
                        dataGridViewPrePedidos.Rows[n].Cells[11].Style.Font = new Font("Roboto", 15.0F, GraphicsUnit.Pixel);
                        dataGridViewPrePedidos.Rows[n].Cells[11].Style.Font = new Font(dataGridViewPrePedidos.Font, FontStyle.Bold);
                        dataGridViewPrePedidos.Rows[n].Cells[12].Value = "Eliminar";
                    }
                    dataGridViewCotizacionesM.PerformLayout();
                    prePedidosListTemp.Clear();
                    if (prePedidosList.Count > 0)
                        lastIdPrePedido = Convert.ToInt32(prePedidosList[prePedidosList.Count - 1].idDocumento);
                    imgSinDatpsPrePedidos.Visible = false;
                }
                else
                {
                    if (progressPrePedido == 0)
                        imgSinDatpsPrePedidos.Visible = true;
                }
                textTotalPrePedidos.Text = "Prepedidos: " + totalItemsPrePedServer.ToString().Trim();
                //reset displayed row
                if (firstVisibleRowPrePedido > -1)
                {
                    dataGridViewPrePedidos.ScrollBars = gridScrollBarsPrePedido;
                    if (prePedidosList.Count > 0)
                    {
                        dataGridViewPrePedidos.FirstDisplayedScrollingRowIndex = firstVisibleRowPrePedido;
                        imgSinDatpsPrePedidos.Visible = false;
                    }
                }
            } catch (Exception e)
            {
                SECUDOC.writeLog(e.ToString());
            }
        }

        private async Task<List<PedidosEncabezadoModel>> getAllPrePedidos(int idAgentByRoute)
        {
            List<PedidosEncabezadoModel> documentsList = null;
            idAgentByRoute = await getIdAgenteByCodigoRutaOCaja(comboBoxRutaCajaPrepedido.Enabled, comboBoxRutaCajaPrepedido.SelectedIndex);
            await Task.Run(async () => {
                if (queryTypePrePedido == 0)
                {
                    if (serverModeLAN)
                    {
                        dynamic responseTotalP = await PrePedidosController.getTotalPrepedidosLAN(routeCajaCodePrePedido,
                            "parameterName1", searchWordPrepedido, "parameterName2", searchWordPrepedido);
                        if (responseTotalP.value >= 0)
                        {
                            totalItemsPrePedServer = responseTotalP.value;
                        }
                        dynamic responsePrepedidos = await PrePedidosController.getAllPrePedidosLAN(panelInstance, lastIdPrePedido, routeCajaCodePrePedido, LIMIT, 
                            "parameterName1", searchWordPrepedido, "parameterName2", searchWordPrepedido, totalItemsPrePedServer);
                        if (responsePrepedidos.value >= 0)
                        {
                            queryPrePedidoTemp = "SELECT * FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO +
                    " WHERE " + LocalDatabase.CAMPO_SURTIDO_PE + " = " + 0 + " AND " +
                    LocalDatabase.CAMPO_LISTO_PE + " != 2 AND " + LocalDatabase.CAMPO_TYPE_PE + " = 4 ";
                            if (idAgentByRoute != 0)
                                queryPrePedidoTemp += "AND " + LocalDatabase.CAMPO_CNOMBREAGENTECC_PE + " = " + idAgentByRoute;
                            queryPrePedidoTemp += " AND " + LocalDatabase.CAMPO_DOCUMENTOID_PE + " > " + lastIdPrePedido + " ORDER BY " +
                            LocalDatabase.CAMPO_DOCUMENTOID_PE + " LIMIT " + LIMIT;
                            documentsList = PedidosEncabezadoModel.getAllDocuments(queryPrePedidoTemp);
                            lastIdPrePedido = responsePrepedidos.lastIdServer;
                            totalItemsPrePedServer = responsePrepedidos.totalItemsPrePedServer;
                        }
                    }
                    else
                    {
                        dynamic responseTotalP = await PrePedidosController.getTotalPrepedidosWs(routeCajaCodePrePedido,
                            "parameterName1", searchWordPrepedido, "parameterName2", searchWordPrepedido);
                        if (responseTotalP.value >= 0)
                        {
                            totalItemsPrePedServer = responseTotalP.value;
                        }
                        dynamic responsePrepedidos = await PrePedidosController.getAllPrePedidosAPI(routeCajaCodePrePedido, lastIdPrePedido, LIMIT, "", "",
                            "","", totalItemsPrePedServer);
                        if (responsePrepedidos.value >= 0)
                        {
                            queryPrePedidoTemp = "SELECT * FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO +
                    " WHERE " + LocalDatabase.CAMPO_SURTIDO_PE + " = " + 0 + " AND " +
                    LocalDatabase.CAMPO_LISTO_PE + " != 2 AND " + LocalDatabase.CAMPO_TYPE_PE + " = 4 ";
                            if (idAgentByRoute != 0)
                                queryPrePedidoTemp += "AND " + LocalDatabase.CAMPO_CNOMBREAGENTECC_PE + " = " + idAgentByRoute;
                            queryPrePedidoTemp += " AND " + LocalDatabase.CAMPO_DOCUMENTOID_PE + " > " + lastIdPrePedido + " ORDER BY " +
                            LocalDatabase.CAMPO_DOCUMENTOID_PE + " LIMIT " + LIMIT;
                            documentsList = PedidosEncabezadoModel.getAllDocuments(queryPrePedidoTemp);
                            lastIdPrePedido = responsePrepedidos.lastIdServer;
                            totalItemsPrePedServer = responsePrepedidos.totalItemsPrePedServer;
                        }
                    }
                    /*queryTotalsPrePedido = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO +
                    " WHERE " + LocalDatabase.CAMPO_SURTIDO_PE + " = " + 0 + " AND " +
                    LocalDatabase.CAMPO_LISTO_PE + " != " + 2 + " AND " + LocalDatabase.CAMPO_TYPE_PE + " = " + 4;
                    if (idAgentByRoute != 0)
                        queryTotalsPrePedido += " AND " + LocalDatabase.CAMPO_CNOMBREAGENTECC_PE + " = " + idAgentByRoute;
                    totalItemsPrePedServer = PedidosEncabezadoModel.getIntValue(queryTotalsPrePedido);*/
                    queryPrePedidoReport = "SELECT * FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO +
                    " WHERE " + LocalDatabase.CAMPO_SURTIDO_PE + " = " + 0 + " AND " +
                    LocalDatabase.CAMPO_LISTO_PE + " != " + 2 + " AND " + LocalDatabase.CAMPO_TYPE_PE + " = " + 4;
                    if (idAgentByRoute != 0)
                        queryPrePedidoReport += " AND " + LocalDatabase.CAMPO_CNOMBREAGENTECC_PE + " = " + idAgentByRoute;
                    queryPrePedidoReport += " AND " +LocalDatabase.CAMPO_DOCUMENTOID_PE + " > 0 ORDER BY " +
                    LocalDatabase.CAMPO_DOCUMENTOID_PE;
                }
                else if (queryTypePrePedido == 1)
                {
                    if (serverModeLAN)
                    {
                        dynamic responseTotalP = await PrePedidosController.getTotalPrepedidosLAN(routeCajaCodePrePedido,
                            "parameterName1", searchWordPrepedido, "parameterName2", searchWordPrepedido);
                        if (responseTotalP.value >= 0)
                        {
                            totalItemsPrePedServer = responseTotalP.value;
                        }
                        dynamic responsePrepedidos = await PrePedidosController.getAllPrePedidosLAN(panelInstance, lastIdPrePedido, routeCajaCodePrePedido, LIMIT, "parameterName1", 
                            searchWordPrepedido, "parameterName2", searchWordPrepedido, totalItemsPrePedServer);
                        if (responsePrepedidos.value >= 0)
                        {
                            queryPrePedidoTemp = "SELECT * FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " PE " +
                    "INNER JOIN " + LocalDatabase.TABLA_CLIENTES + " C ON PE." + LocalDatabase.CAMPO_CLIENTEID_PE + " = C." + LocalDatabase.CAMPO_ID_CLIENTE + " " +
                    "WHERE PE." + LocalDatabase.CAMPO_SURTIDO_PE + " = " + 0 +
                    " AND PE." + LocalDatabase.CAMPO_LISTO_PE + " != " + 2 + " AND PE." + LocalDatabase.CAMPO_TYPE_PE + " = 4 ";
                            if (idAgentByRoute != 0)
                                queryPrePedidoTemp += "AND PE." + LocalDatabase.CAMPO_CNOMBREAGENTECC_PE + " = " + idAgentByRoute + " ";
                            queryPrePedidoTemp += "AND (C." + LocalDatabase.CAMPO_NOMBRECLIENTE + " LIKE @parameterName1 OR PE." + LocalDatabase.CAMPO_CFOLIO_PE + " LIKE @parameterName2) " +
                            "AND PE." + LocalDatabase.CAMPO_DOCUMENTOID_PE + " > " + lastIdPrePedido + " ORDER BY " +
                            "PE." + LocalDatabase.CAMPO_DOCUMENTOID_PE + " LIMIT " + LIMIT;
                            documentsList = PedidosEncabezadoModel.getAllDocumentsWithParametersToSearch(queryPrePedidoTemp, "parameterName1",
                        searchWordPrepedido, "parameterName2", searchWordPrepedido);
                            lastIdPrePedido = responsePrepedidos.lastIdServer;
                            totalItemsPrePedServer = responsePrepedidos.totalItemsPrePedServer;
                        }                            
                    }
                    else
                    {
                        dynamic responseTotalP = await PrePedidosController.getTotalPrepedidosWs(routeCajaCodePrePedido,
                            "parameterName1", searchWordPrepedido,
                            "parameterName2", searchWordPrepedido);
                        if (responseTotalP.value >= 0)
                        {
                            totalItemsPrePedServer = responseTotalP.value;
                        }
                        dynamic responsePrepedidos = await PrePedidosController.getAllPrePedidosAPI(routeCajaCodePrePedido, lastIdPrePedido, LIMIT, "parameterName1", searchWordPrepedido, 
                            "parameterName2", searchWordPrepedido, totalItemsPrePedServer);
                        if (responsePrepedidos.value >= 0)
                        {
                            queryPrePedidoTemp = "SELECT * FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " PE " +
                    "INNER JOIN " + LocalDatabase.TABLA_CLIENTES + " C ON PE." + LocalDatabase.CAMPO_CLIENTEID_PE + " = C." + LocalDatabase.CAMPO_ID_CLIENTE + " " +
                    "WHERE PE." + LocalDatabase.CAMPO_SURTIDO_PE + " = " + 0 +
                    " AND PE." + LocalDatabase.CAMPO_LISTO_PE + " != " + 2 + " AND PE." + LocalDatabase.CAMPO_TYPE_PE + " = 4 ";
                            if (idAgentByRoute != 0)
                                queryPrePedidoTemp += "AND PE." + LocalDatabase.CAMPO_CNOMBREAGENTECC_PE + " = " + idAgentByRoute + " ";
                            queryPrePedidoTemp += "AND (C." + LocalDatabase.CAMPO_NOMBRECLIENTE + " LIKE @parameterName1 OR PE." + LocalDatabase.CAMPO_CFOLIO_PE + " LIKE @parameterName2) " +
                            "AND PE." + LocalDatabase.CAMPO_DOCUMENTOID_PE + " > " + lastIdPrePedido + " ORDER BY " +
                            "PE." + LocalDatabase.CAMPO_DOCUMENTOID_PE + " LIMIT " + LIMIT;
                            documentsList = PedidosEncabezadoModel.getAllDocumentsWithParametersToSearch(queryPrePedidoTemp, "parameterName1",
                        searchWordPrepedido, "parameterName2", searchWordPrepedido);
                            lastIdPrePedido = responsePrepedidos.lastIdServer;
                            totalItemsPrePedServer = responsePrepedidos.totalItemsPrePedServer;
                        }
                    }
                    /*queryTotalsPrePedido = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " PE " +
                    "INNER JOIN " + LocalDatabase.TABLA_CLIENTES + " C ON PE." + LocalDatabase.CAMPO_CLIENTEID_PE + " = C." + LocalDatabase.CAMPO_ID_CLIENTE + " " +
                    "WHERE PE." + LocalDatabase.CAMPO_SURTIDO_PE + " = " + 0 + " AND PE." + LocalDatabase.CAMPO_LISTO_PE + " != 2 " +
                    "AND PE." + LocalDatabase.CAMPO_TYPE_PE + " = " + 4 + " ";
                    if (idAgentByRoute != 0)
                        queryTotalsPrePedido += "AND PE." + LocalDatabase.CAMPO_CNOMBREAGENTECC_PE + " = " + idAgentByRoute + " ";
                    queryTotalsPrePedido += "AND (C." + LocalDatabase.CAMPO_NOMBRECLIENTE + " LIKE @parameterName1 OR PE." + LocalDatabase.CAMPO_CFOLIO_PE + " LIKE @parameterName2)";
                    totalItemsPrePedLocal = PedidosEncabezadoModel.getIntValueWithParameters(queryTotalsPrePedido, "parameterName1",
                        searchWordPrepedido, "parameterName2", searchWordPrepedido);*/
                    queryPrePedidoReport = "SELECT * FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " PE " +
                    "INNER JOIN " + LocalDatabase.TABLA_CLIENTES + " C ON PE." + LocalDatabase.CAMPO_CLIENTEID_PE + " = C." + LocalDatabase.CAMPO_ID_CLIENTE +
                    " WHERE PE." + LocalDatabase.CAMPO_SURTIDO_PE + " = " + 0 +
                    " AND PE." + LocalDatabase.CAMPO_LISTO_PE + " != " + 2 + " AND PE." + LocalDatabase.CAMPO_TYPE_PE + " = 4 ";
                    if (idAgentByRoute != 0)
                        queryPrePedidoReport += "AND PE." + LocalDatabase.CAMPO_CNOMBREAGENTECC_PE + " = " + idAgentByRoute + " ";
                    queryPrePedidoReport += "AND (C." + LocalDatabase.CAMPO_NOMBRECLIENTE + " LIKE @parameterName1 OR PE." +LocalDatabase.CAMPO_CFOLIO_PE + " LIKE @parameterName2) " +
                    "AND PE." +LocalDatabase.CAMPO_DOCUMENTOID_PE + " > 0 ORDER BY PE." + LocalDatabase.CAMPO_DOCUMENTOID_PE;
                }
                else if (queryTypePrePedido == 2)
                {
                    routeCajaCodePrePedido = "";
                    if (serverModeLAN)
                    {
                        dynamic responseTotalP = await PrePedidosController.getTotalPrepedidosLAN(routeCajaCodePrePedido,
                            "parameterName1", searchWordPrepedido, "parameterName2", searchWordPrepedido);
                        if (responseTotalP.value >= 0)
                        {
                            totalItemsPrePedServer = responseTotalP.value;
                        }
                        /*else
                        {
                            queryTotalsPrePedido = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " PE " +
                    "INNER JOIN " + LocalDatabase.TABLA_CLIENTES + " C ON PE." + LocalDatabase.CAMPO_CLIENTEID_PE + " = C." + LocalDatabase.CAMPO_ID_CLIENTE + " " +
                    "WHERE PE." + LocalDatabase.CAMPO_SURTIDO_PE + " = " + 0 + " AND PE." + LocalDatabase.CAMPO_LISTO_PE + " != " + 2 +
                    " AND PE." + LocalDatabase.CAMPO_TYPE_PE + " = 4 " +
                    "AND (C." + LocalDatabase.CAMPO_NOMBRECLIENTE + " LIKE @parameterName1 OR PE." + LocalDatabase.CAMPO_CFOLIO_PE + " LIKE @parameterName2)";
                            totalItemsPrePedLocal = PedidosEncabezadoModel.getIntValueWithParameters(queryTotalsPrePedido, "parameterName1",
                                searchWordPrepedido, "parameterName2", searchWordPrepedido);
                        }*/
                        dynamic responsePrepedidos = await PrePedidosController.getAllPrePedidosLAN(panelInstance, lastIdPrePedido, routeCajaCodePrePedido, LIMIT, "parameterName1",
                            searchWordPrepedido, "parameterName2", searchWordPrepedido, totalItemsPrePedServer);
                        if (responsePrepedidos.value >= 0)
                        {
                            queryPrePedidoTemp = "SELECT * FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " PE " +
                    "INNER JOIN " + LocalDatabase.TABLA_CLIENTES + " C ON PE." + LocalDatabase.CAMPO_CLIENTEID_PE + " = C." + LocalDatabase.CAMPO_ID_CLIENTE + " " +
                    "WHERE PE." + LocalDatabase.CAMPO_SURTIDO_PE + " = 0 " +
                    "AND PE." + LocalDatabase.CAMPO_LISTO_PE + " != 2 AND PE." + LocalDatabase.CAMPO_TYPE_PE + " = 4 AND " +
                    "(C." + LocalDatabase.CAMPO_NOMBRECLIENTE + " LIKE @parameterName1 OR PE." + LocalDatabase.CAMPO_CFOLIO_PE + " LIKE @parameterName2) " +
                    "AND PE." + LocalDatabase.CAMPO_DOCUMENTOID_PE + " > " + lastIdPrePedido + " ORDER BY " +
                    "PE." + LocalDatabase.CAMPO_DOCUMENTOID_PE + " LIMIT " + LIMIT;
                            documentsList = PedidosEncabezadoModel.getAllDocumentsWithParametersToSearch(queryPrePedidoTemp, "parameterName1",
                        searchWordPrepedido, "parameterName2", searchWordPrepedido);
                            lastIdPrePedido = responsePrepedidos.lastIdServer;
                            totalItemsPrePedServer = responsePrepedidos.totalItemsPrePedServer;
                        }
                    }
                    else
                    {
                        dynamic responseTotalP = await PrePedidosController.getTotalPrepedidosWs(routeCajaCodePrePedido,
                            "parameterName1", searchWordPrepedido,"parameterName2", searchWordPrepedido);
                        if (responseTotalP.value >= 0)
                        {
                            totalItemsPrePedServer = responseTotalP.value;
                        }
                        dynamic responsePrepedidos = await PrePedidosController.getAllPrePedidosAPI(routeCajaCodePrePedido, lastIdPrePedido, LIMIT, "parameterName1", searchWordPrepedido,
                            "parameterName2", searchWordPrepedido, totalItemsPrePedServer);
                        if (responsePrepedidos.value >= 0)
                        {
                            queryPrePedidoTemp = "SELECT * FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " PE " +
                    "INNER JOIN " + LocalDatabase.TABLA_CLIENTES + " C ON PE." + LocalDatabase.CAMPO_CLIENTEID_PE + " = C." + LocalDatabase.CAMPO_ID_CLIENTE + " " +
                    "WHERE PE." + LocalDatabase.CAMPO_SURTIDO_PE + " = 0 " +
                    "AND PE." + LocalDatabase.CAMPO_LISTO_PE + " != 2 AND PE." + LocalDatabase.CAMPO_TYPE_PE + " = 4 AND " +
                    "(C." + LocalDatabase.CAMPO_NOMBRECLIENTE + " LIKE @parameterName1 OR PE." + LocalDatabase.CAMPO_CFOLIO_PE + " LIKE @parameterName2) " +
                    "AND PE." + LocalDatabase.CAMPO_DOCUMENTOID_PE + " > " + lastIdPrePedido + " ORDER BY " +
                    "PE." + LocalDatabase.CAMPO_DOCUMENTOID_PE + " LIMIT " + LIMIT;
                            documentsList = PedidosEncabezadoModel.getAllDocumentsWithParametersToSearch(queryPrePedidoTemp, "parameterName1",
                        searchWordPrepedido, "parameterName2", searchWordPrepedido);
                            lastIdPrePedido = responsePrepedidos.lastIdServer;
                            totalItemsPrePedServer = responsePrepedidos.totalItemsPrePedServer;
                        }
                    }
                    /*queryTotalsPrePedido = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " PE " +
                    "INNER JOIN " + LocalDatabase.TABLA_CLIENTES + " C ON PE." + LocalDatabase.CAMPO_CLIENTEID_PE + " = C." + LocalDatabase.CAMPO_ID_CLIENTE + " " +
                    "WHERE PE." + LocalDatabase.CAMPO_SURTIDO_PE + " = " + 0 + " AND PE." + LocalDatabase.CAMPO_LISTO_PE + " != " + 2 +
                    " AND PE." + LocalDatabase.CAMPO_TYPE_PE + " = 4 " +
                    "AND (C." + LocalDatabase.CAMPO_NOMBRECLIENTE + " LIKE @parameterName1 OR PE." + LocalDatabase.CAMPO_CFOLIO_PE + " LIKE @parameterName2)";
                    totalItemsPrePedLocal = PedidosEncabezadoModel.getIntValueWithParameters(queryTotalsPrePedido, "parameterName1",
                        searchWordPrepedido, "parameterName2", searchWordPrepedido);*/
                    queryPrePedidoReport = "SELECT * FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " PE " +
                    "INNER JOIN " + LocalDatabase.TABLA_CLIENTES + " C ON PE." + LocalDatabase.CAMPO_CLIENTEID_PE + " = C." + LocalDatabase.CAMPO_ID_CLIENTE +
                    " WHERE PE." + LocalDatabase.CAMPO_SURTIDO_PE + " = " + 0 +
                    " AND PE." + LocalDatabase.CAMPO_LISTO_PE + " != 2 AND PE." + LocalDatabase.CAMPO_TYPE_PE + " = 4 AND " +
                    "(C." + LocalDatabase.CAMPO_NOMBRECLIENTE + " LIKE @parameterName1 OR PE." + LocalDatabase.CAMPO_CFOLIO_PE + " LIKE @parameterName2) " +
                    "AND PE." + LocalDatabase.CAMPO_DOCUMENTOID_PE + " > 0 ORDER BY PE." + LocalDatabase.CAMPO_DOCUMENTOID_PE;
                }
            });
            return documentsList;
        }

        private async void dataGridViewPrePedidos_Scroll(object sender, ScrollEventArgs e)
        {
            if (prePedidosList.Count < totalItemsPrePedServer && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
                {
                    int cuntDisplayed = dataGridViewPrePedidos.Rows.Count - getDisplayedRowsCountPrepedidos();
                    if (e.NewValue >= cuntDisplayed)
                    {
                        //prevent loading from autoscroll.
                        TimeSpan ts = DateTime.Now - lastLoadingPrePedido;
                        if (ts.TotalMilliseconds > 100)
                        {
                            firstVisibleRowPrePedido = e.NewValue;
                            fillDataGridViewPrePedidos();
                        }
                        else
                        {
                            dataGridViewPrePedidos.FirstDisplayedScrollingRowIndex = e.OldValue;
                        }
                    }
                }
            }
        }

        private int getDisplayedRowsCountPrepedidos()
        {
            int count = dataGridViewPrePedidos.Rows[dataGridViewPrePedidos.FirstDisplayedScrollingRowIndex].Height;
            count = dataGridViewPrePedidos.Height / count;
            return count;
        }

        public async Task saveDocument(int idPedCot, bool webActive, String codigoCaja)
        {
            responseExistencias = await saveDocumentAndGoToSales(idPedCot, webActive, codigoCaja);
            if (responseExistencias == -2) {
                FormMessage formMessage = new FormMessage("Existencias Insuficientes", "No puedes vender productos sin existencia!", 3);
                formMessage.ShowDialog();
            }
            if (fiscalDocument == DocumentModel.FISCAL_FACTURAR)
                FormVenta.activateOpcionFacturar = 1;
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            this.Close();
        }

        private async Task<int> saveDocumentAndGoToSales(int idPedido, bool webActive, String codigoCaja)
        {
            int responseExistencia = 0;
            await Task.Run(async () => {
                String comInstance = InstanceSQLSEModel.getStringComInstance();
                PedidosEncabezadoModel pm = PedidosEncabezadoModel.getAPedCot(idPedido);
                if (pm != null)
                {
                    int documentType = DocumentModel.TIPO_REMISION;
                    if (permissionPrepedido)
                        documentType = DocumentModel.TIPO_VENTA;
                    if (tabPosition == 0)
                    {
                        if (LicenseModel.isItTPVLicense())
                        {
                            int idDocumentoCreado = createDocument(documentType, idPedido, fiscalDocument, pm.observation, pm.agenteId, codigoCaja);
                            FormVenta.idCustomer = pm.clienteId;
                            FormVenta.idDocument = idDocumentoCreado;
                        }
                    }
                    else if (tabPosition == 1)
                    {
                        if (LicenseModel.isItTPVLicense())
                        {
                            int idDocumentoCreado = createDocument(documentType, idPedido, fiscalDocument, pm.observation, pm.agenteId, codigoCaja);
                            FormVenta.idCustomer = pm.clienteId;
                            FormVenta.idDocument = idDocumentoCreado;
                        }
                    }
                    List<PedidoDetalleModel> movementsList = PedidoDetalleModel.getAllMovementsFromAnOrder(idPedido);
                    if (movementsList != null) {
                        foreach (PedidoDetalleModel pdm in movementsList)
                        {
                            ClsItemModel im = null;
                            if (pm.facturar != DocumentModel.FISCAL_FACTURAR)
                            {
                                if (serverModeLAN)
                                {
                                    dynamic responseGetItem = await ItemsController.getAnItemFromTheServerLAN(pdm.itemId, null, codigoCaja);
                                    if (responseGetItem.value == 1)
                                        im = responseGetItem.item;
                                    dynamic responseUnitsPendings = await ItemsController.getUnitsPendingsLAN(im.id);
                                    if (responseUnitsPendings.value == 1)
                                        im.existencia = (im.existencia - responseUnitsPendings.unidadesPendientes);
                                } else
                                {
                                    if (webActive)
                                    {
                                        dynamic responseGetItem = await ItemsController.getAnItemFromTheServerAPI(pdm.itemId, null, codigoCaja);
                                        if (responseGetItem.value == 1)
                                        {
                                            if (responseGetItem.item != null)
                                            {
                                                im = responseGetItem.item;
                                                dynamic responseUnitsPendings = await ItemsController.getUnitsPendingsAPI(im.id, codigoCaja);
                                                if (responseUnitsPendings.value == 1)
                                                    im.existencia = (im.existencia - responseUnitsPendings.unidadesPendientes);
                                            }
                                            else
                                            {
                                                im = ItemModel.getAllDataFromAnItem(pdm.itemId);
                                                if (im != null)
                                                {
                                                    double pendingLocalUnits = MovimientosModel.getUnidadesBasePendientesLocales(pdm.itemId, 0, true);
                                                    im.existencia = (im.existencia - pendingLocalUnits);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            im = ItemModel.getAllDataFromAnItem(pdm.itemId);
                                            if (im != null)
                                            {
                                                double pendingLocalUnits = MovimientosModel.getUnidadesBasePendientesLocales(pdm.itemId, 0, true);
                                                im.existencia = (im.existencia - pendingLocalUnits);
                                            }
                                        }
                                    } else
                                    {
                                        im = ItemModel.getAllDataFromAnItem(pdm.itemId);
                                        if (im != null)
                                        {
                                            double pendingLocalUnits = MovimientosModel.getUnidadesBasePendientesLocales(pdm.itemId, 0, true);
                                            im.existencia = (im.existencia - pendingLocalUnits);
                                        }
                                    }
                                }
                                int fiscalProduct = 1;
                                bool useFiscalField = await ConfigurationsTpvController.checkIfUseFiscalValueActivated();
                                if (useFiscalField)
                                {
                                    bool isFiscal = await ItemModel.getFiscalItemFieldValue(im, positionFiscalItemField);
                                    if (isFiscal)
                                        fiscalProduct = 1;
                                    else fiscalProduct = 0;
                                }
                                else fiscalProduct = 1;
                                if (fiscalProduct == 1)
                                {
                                    documentType = DocumentModel.TIPO_REMISION;
                                    fiscalDocument = DocumentModel.FISCAL_FACTURAR;
                                }
                                else if (fiscalProduct == 0)
                                {
                                    documentType = DocumentModel.TIPO_REMISION;
                                    fiscalDocument = DocumentModel.NO_FISCAL_NO_FACTURAR;
                                }
                            } else
                            {
                                documentType = DocumentModel.TIPO_VENTA;
                                fiscalDocument = pm.facturar;
                            }
                            responseExistencia = await validateStock(idPedido, documentType, im, pdm.unidadesCapturadas, pdm.unidadCapturadaId, pdm.precio, pdm.descuento,
                                pdm.unidadesNoConvertibles, pdm.unidadNoConvertibleId, fiscalDocument, pm.observation, pdm.observation,
                                comInstance);
                        }
                    }
                }
            });
            return responseExistencia;
        }

        private async Task<int> validateStock(int idPedido, int documentType, ClsItemModel itemModel, double capturedUnits, int capturedUnitId, double precio, double descuento,
            double nonConvertibleUnits, int nonConvertibleUnitId, int fiscalDocument, String observationDocumento, String observacionMovimiento,
            String comInstance)
        {
            int responseExistencia = 0;
            bool insufficientStock = false;
            double salesUnits = 0;
            try
            {
                if (documentType == DocumentModel.TIPO_VENTA || documentType == DocumentModel.TIPO_REMISION)
                {
                    /** Logic for types Ventas and Ventas TPV */
                    double existencias = itemModel.existencia;
                    if (itemModel.salesUnitId != capturedUnitId)
                    {
                        int capturedUnitsIsMajorThanSalesUnits = -1;
                        if (serverModeLAN)
                        {
                            dynamic responseCaptureUnit = await ConversionsUnitsController.checkIfTheCapturedUnitIsHigherLAN(itemModel.salesUnitId, capturedUnitId);
                            if (responseCaptureUnit.value == 1)
                                capturedUnitsIsMajorThanSalesUnits = responseCaptureUnit.salesUnitIsHigher;
                        }
                        else capturedUnitsIsMajorThanSalesUnits = ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(itemModel.salesUnitId, capturedUnitId);
                        if (capturedUnitsIsMajorThanSalesUnits == 0)
                        {
                            /** Unidad de venta es menor: multiplicamos la base por el numero de conversión mayor */
                            double majorConversion = 0;
                            if (serverModeLAN)
                            {
                                dynamic responseMajor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.salesUnitId, capturedUnitId, true);
                                if (responseMajor.value == 1)
                                    majorConversion = responseMajor.majorFactor;
                            }
                            else majorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.salesUnitId, capturedUnitId, true);
                            double stockInMinorUnits = existencias;
                            if (majorConversion != 0)
                                stockInMinorUnits = existencias * majorConversion;
                            if (capturedUnits > stockInMinorUnits)
                                insufficientStock = true;
                            else
                            {
                                double newStockInMinorUnits = stockInMinorUnits - capturedUnits;
                                double newStockMajorUnit = newStockInMinorUnits;
                                if (majorConversion != 0)
                                    newStockMajorUnit = newStockInMinorUnits / majorConversion;
                                salesUnits = existencias - newStockMajorUnit;
                            }
                        }
                        else if (capturedUnitsIsMajorThanSalesUnits == 1)
                        {
                            /** Unidad capturada es mayor a la de venta */
                            if (capturedUnitId == itemModel.baseUnitId)
                            {
                                if (capturedUnits > existencias)
                                {
                                    insufficientStock = true;
                                }
                                else
                                {
                                    double minorConversion = 0;
                                    if (serverModeLAN)
                                    {
                                        dynamic responseMajor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.salesUnitId, capturedUnitId, false);
                                        if (responseMajor.value == 1)
                                            minorConversion = responseMajor.majorFactor;
                                    }
                                    else minorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.salesUnitId, capturedUnitId, false);
                                    if (minorConversion != 0)
                                        salesUnits = capturedUnits / minorConversion;
                                    else salesUnits = capturedUnits;
                                }
                            }
                            else
                            {
                                double minorConversion = 0;
                                if (serverModeLAN)
                                {
                                    dynamic responseMajor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.salesUnitId, capturedUnitId, false);
                                    if (responseMajor.value == 1)
                                        minorConversion = responseMajor.majorFactor;
                                }
                                else minorConversion = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.salesUnitId, capturedUnitId, false);
                                if (minorConversion != 0)
                                    salesUnits = capturedUnits / minorConversion;
                                else salesUnits = capturedUnits;
                                double majorFactor = 0;
                                if (serverModeLAN)
                                {
                                    dynamic responseMajor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.salesUnitId, itemModel.baseUnitId, true);
                                    if (responseMajor.value == 1)
                                        majorFactor = responseMajor.majorFactor;
                                }
                                else majorFactor = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.salesUnitId, itemModel.baseUnitId, true);
                                double stockInSalesUnits = existencias;
                                if (majorFactor != 0)
                                    stockInSalesUnits = existencias * majorFactor;
                                if (salesUnits > stockInSalesUnits)
                                {
                                    insufficientStock = true;
                                }
                            }
                        }
                        else if (capturedUnitsIsMajorThanSalesUnits == 2)
                        {
                            if (capturedUnits > existencias)
                                insufficientStock = true;
                            else salesUnits = capturedUnits;
                        }
                        else
                        {
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
                                dynamic responseCaptureUnit = await ConversionsUnitsController.checkIfTheCapturedUnitIsHigherLAN(itemModel.baseUnitId, capturedUnitId);
                                if (responseCaptureUnit.value == 1)
                                    capturedUnitIsMajorThanTheBase = responseCaptureUnit.salesUnitIsHigher;
                            }
                            else capturedUnitIsMajorThanTheBase = ConversionsUnitsModel.checkIfTheCapturedUnitIsHigher(itemModel.baseUnitId, capturedUnitId);
                            if (capturedUnitIsMajorThanTheBase == 0)
                            {
                                /** Unidades capturadas son mayores en equivalencia a las de base e igual a las de venta*/
                                double majorFactor = 0;
                                if (serverModeLAN)
                                {
                                    dynamic responseMajor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.baseUnitId, capturedUnitId, true);
                                    if (responseMajor.value == 1)
                                        majorFactor = responseMajor.majorFactor;
                                }
                                else majorFactor = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, true);
                                double majorStock = (existencias);
                                if (majorFactor != 0)
                                    majorStock = (existencias * majorFactor);
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
                                double majorFactor = 0;
                                if (serverModeLAN)
                                {
                                    dynamic responseMajor = await ConversionsUnitsController.getMajorOrMinorConversionFactorFromAnItemLAN(itemModel.baseUnitId, capturedUnitId, true);
                                    if (responseMajor.value == 1)
                                        majorFactor = responseMajor.majorFactor;
                                }
                                else majorFactor = ConversionsUnitsModel.getMajorOrMinorConversionFactorFromAnItem(itemModel.baseUnitId, capturedUnitId, true);
                                double minorStock = (existencias);
                                if (majorFactor != 0)
                                    minorStock = (existencias * majorFactor);
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
                    if (!insufficientStock)
                    {
                        if (serverModeLAN)
                            await DatosTicketController.downloadAllDatosTicketLAN();
                        else
                        {
                            if (webActive)
                                await DatosTicketController.downloadAllDatosTicketAPI();
                        }
                        double monto = (capturedUnits * precio);
                        double newAmountDiscount = (monto * descuento) / 100;
                        dynamic myMap = applyDiscountPromotions(documentType, itemModel, capturedUnits, monto);
                        double totalItem = (monto - newAmountDiscount);
                        int capturedUnitType = 0;
                        if (serverModeLAN)
                            capturedUnitType = ClsItemModel.getCapturedUnitType(comInstance, itemModel.id, capturedUnitId);
                        else capturedUnitType = ItemModel.getCapturedUnitType(itemModel.id, capturedUnitId);
                        dynamic respuesta = addMovement(idPedido, documentType, salesUnits, nonConvertibleUnits, capturedUnits, nonConvertibleUnitId,
                                capturedUnitId, capturedUnitType,
                                monto, totalItem, myMap.rateDiscountPromo, newAmountDiscount, observationDocumento, observacionMovimiento, itemModel, descuento, precio, fiscalDocument);
                    }
                    else
                    {
                        if (serverModeLAN)
                            await DatosTicketController.downloadAllDatosTicketLAN();
                        else
                        {
                            if (webActive)
                                await DatosTicketController.downloadAllDatosTicketAPI();
                        }
                        if (DatosTicketModel.sellOnlyWithStock())
                        {
                            responseExistencia = -2;
                        }
                        else
                        {
                            double monto = (capturedUnits * precio);
                            double newAmountDiscount = (monto * descuento) / 100;
                            dynamic myMap = applyDiscountPromotions(documentType, itemModel, capturedUnits, monto);
                            //newAmountDiscount += myMap.newAmountDiscount;
                            double totalItem = (monto - newAmountDiscount);
                            int capturedUnitType = 0;
                            if (serverModeLAN)
                                capturedUnitType = ClsItemModel.getCapturedUnitType(comInstance, itemModel.id, capturedUnitId);
                            else capturedUnitType = ItemModel.getCapturedUnitType(itemModel.id, capturedUnitId);
                            dynamic respuesta = addMovement(idPedido, documentType, salesUnits, nonConvertibleUnits, capturedUnits, nonConvertibleUnitId,
                                    capturedUnitId, capturedUnitType,
                                    monto, totalItem, myMap.rateDiscountPromo, newAmountDiscount, observationDocumento, observacionMovimiento, itemModel, descuento, precio, fiscalDocument);
                        }
                    }
                }
            } catch (Exception e)
            {
                SECUDOC.writeLog(e.ToString());
                responseExistencia = -1;
            }
            return responseExistencia;
        }

        private ExpandoObject applyDiscountPromotions(int documentType, ClsItemModel itemModel, double cantidadArticulo, double monto)
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

        private void dataGridViewPrePedidos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.ColumnIndex <= 10)
            {
                int idDocumentPrePedido = Convert.ToInt32(dataGridViewPrePedidos.Rows[e.RowIndex].Cells["idDocumentoPrePedido"].Value.ToString().Trim());
                FormMovimientos frmMovimientos = new FormMovimientos(idDocumentPrePedido, FormMovimientos.TIPOMOVPED, e.RowIndex, cotmosActive);
                frmMovimientos.ShowDialog();
            } else if (e.RowIndex >= 0 && e.ColumnIndex == 11)
            {
                int idDocumentPrePedido = Convert.ToInt32(dataGridViewPrePedidos.Rows[e.RowIndex].Cells["idDocumentoPrePedido"].Value.ToString().Trim());
                if (serverModeLAN)
                {
                    //saveDocumentPrePedido(idDocumentPrePedido);
                    callProcessCreateDocumentPrepedido(idDocumentPrePedido);
                } else
                {
                    int itemDoesNotExist = validateIdAllItemsExist(idDocumentPrePedido);
                    if (itemDoesNotExist == 0)
                        callProcessCreateDocumentPrepedido(idDocumentPrePedido);
                    else
                    {
                        processToDownloadItemsInformationForPrepedido(idDocumentPrePedido, codigoCaja);
                        itemDoesNotExist = validateIdAllItemsExist(idDocumentPrePedido);
                        if (itemDoesNotExist == 0)
                            callProcessCreateDocumentPrepedido(idDocumentPrePedido);
                        else
                        {
                            FormMessage fm = new FormMessage("Productos o Servicios No Encontrados", "Tienes que actualizar los productos para poder surtir el Documento", 2);
                            fm.ShowDialog();
                        }
                    }
                }
            } else if (e.RowIndex >= 0 && e.ColumnIndex == 12)
            {
                int idDocumentPrePedido = Convert.ToInt32(dataGridViewPrePedidos.Rows[e.RowIndex].Cells["idDocumentoPrePedido"].Value.ToString().Trim());
                processToDeletePrePedido(idDocumentPrePedido, e.RowIndex);
            }
        }

        private async Task processToDownloadItemsInformationForPrepedido(int idPrepedido, String codigoCaja)
        {
            List<PedidoDetalleModel> movementsList = PedidoDetalleModel.getAllMovementsFromAnOrder(idPrepedido);
            if (movementsList != null)
            {
                foreach (var movement in movementsList)
                {
                    await ItemsController.getAnItemFromTheServerAPI(movement.itemId, null, codigoCaja);
                }
            }
        }

        private async Task callProcessCreateDocumentPrepedido(int idDocumentPrePedido)
        {
            formWaiting = new FormWaiting(this, 4, idDocumentPrePedido, false, webActive, codigoCaja);
            formWaiting.ShowDialog();
        }

        private int validateIdAllItemsExist(int idDocumentPrePedido)
        {
            int response = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_ITEM + " I " +
                "INNER JOIN " + LocalDatabase.TABLA_PEDIDODETALLE + " PD ON I." + LocalDatabase.CAMPO_CODIGO_ITEM + " = PD." +
                LocalDatabase.CAMPO_CCODIGOPRODUCTO_PD + " WHERE PD." + LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_PD + " = " + idDocumentPrePedido;
                if (!ItemModel.checkIfRecordExist(db, query))
                    response = 1;
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return response;
        }

        private void editFolioPrePedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private async void editFolioPrePedido_TextChanged(object sender, EventArgs e)
        {
            /*searchWordPrepedido = editFolioPrePedido.Text.Trim();
            if (searchWordPrepedido.Equals(""))
            {
                queryTypePrePedido = 0;
                resetearValoresPrePedidos(queryTypePrePedido);
                await fillDataGridViewPrePedidos();
            } else
            {
                queryTypePrePedido = 1;
                if (checkBoxTodosLosPrepedidos.Checked && !comboBoxRutaCajaPrepedido.Enabled)
                {
                    routeCajaIdPrePedido = 0;
                    routeCajaCodePrePedido = "";
                }
                resetearValoresPrePedidos(queryTypePrePedido);
                await fillDataGridViewPrePedidos();
            }*/
        }

        private void btnDownPrepedidosList_Click(object sender, EventArgs e)
        {
            try
            {
                if (prePedidosList != null && prePedidosList.Count > 0)
                {
                    dataGridViewPrePedidos.FirstDisplayedScrollingRowIndex = dataGridViewPrePedidos.FirstDisplayedScrollingRowIndex + 1;
                }
            } catch (Exception ex) {
                //SECUDOC.writeLog(ex.ToString());
            }
        }

        private void btnUpPrepedidos_Click(object sender, EventArgs e)
        {
            try
            {
                if (prePedidosList != null && prePedidosList.Count > 0)
                {
                    dataGridViewPrePedidos.FirstDisplayedScrollingRowIndex = dataGridViewPrePedidos.FirstDisplayedScrollingRowIndex - 1;
                }
            }
            catch (Exception ex)
            {
                //SECUDOC.writeLog(ex.ToString());
            }
        }

        private void editBuscarPrepedido_TextChanged(object sender, EventArgs e)
        {
            timerBuscarPrepedido.Stop();
            timerBuscarPrepedido.Start();
        }

        private async void timerBuscarPrepedido_Tick(object sender, EventArgs e)
        {
            searchWordPrepedido = editBuscarPrepedido.Text.Trim();
            if (!searchWordPrepedido.Equals(""))
            {
                queryTypePrePedido = 1;
                if (checkBoxTodosLosPrepedidos.Checked && !comboBoxRutaCajaPrepedido.Enabled)
                {
                    routeCajaIdPrePedido = 0;
                    routeCajaCodePrePedido = "";
                }
                timerBuscarPrepedido.Stop();
                resetearValoresPrePedidos(queryTypePrePedido);
                await fillDataGridViewPrePedidos();
            }
            else
            {
                queryTypePrePedido = 0;
                timerBuscarPrepedido.Stop();
                resetearValoresPrePedidos(queryTypePrePedido);
                await fillDataGridViewPrePedidos();
            }
        }

        private async void FrmPedidosCotizacionesSurtir_Resize(object sender, EventArgs e)
        {
            if (permissionPrepedido)
            {
                if (this.WindowState == FormWindowState.Maximized)
                {
                    LIMIT = 15;
                    resetearValoresPrePedidos(queryTypePrePedido);
                    await fillDataGridViewPrePedidos();
                }
                else
                {
                    LIMIT = 10;
                    resetearValoresPrePedidos(queryTypePrePedido);
                    await fillDataGridViewPrePedidos();
                }
            }
        }

        private void btnPdfPrepedidos_Click(object sender, EventArgs e)
        {
            FormParametersReports formParametersReports = new FormParametersReports(queryPrePedidoReport, "nombreCliente", "folio",
                    searchWordPrepedido, searchWordPrepedido, queryTypePrePedido, routeCajaCodePrePedido, routeCajaIdPrePedido, idAgentByRoute);
            formParametersReports.StartPosition = FormStartPosition.CenterScreen;
            formParametersReports.ShowDialog();
            //processToGeneratePdfPrepedidos();
        }

        public async Task saveDocumentPrePedido(int idDocumentoPrepedido, bool webActive, String codigoCaja)
        {
            responseExistencias = await saveDocumentAndGoToSales(idDocumentoPrepedido, webActive, codigoCaja);
            if (responseExistencias == -2)
            {
                FormMessage formMessage = new FormMessage("Existencia Insuficiente", "El producto no cuenta con la existencia suficiente", 3);
                formMessage.ShowDialog();
            }
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            if (fiscalDocument == DocumentModel.FISCAL_FACTURAR)
                FormVenta.activateOpcionFacturar = 1;
            this.Close();
        }

        private void comboBoxRutaCajaPrepedido_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxRutaCajaPrepedido.Enabled)
            {
                if (comboBoxRutaCajaPrepedido.SelectedIndex == 0)
                {
                    formWaiting = new FormWaiting(this, FormWaiting.CALL_RUTAS, 0, true, webActive, codigoCaja);
                    formWaiting.ShowDialog();
                } else if (comboBoxRutaCajaPrepedido.SelectedIndex == 1)
                {
                    formWaiting = new FormWaiting(this, FormWaiting.CALL_RUTAS, 0, false, webActive, codigoCaja);
                    formWaiting.ShowDialog();
                }
            }
        }

        private async Task processToDeletePrePedido(int idDocumentoPrePedido, int positionAEliminar)
        {
            FrmConfirmation fm = new FrmConfirmation("Cancelar PrePedido", "Al cancelar el Prepedido será eliminado permanentemente!\n ¿Desea Continuar?");
            fm.StartPosition = FormStartPosition.CenterScreen;
            fm.ShowDialog();
            if (FrmConfirmation.confirmation)
            {
                String query = "UPDATE " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " SET " + LocalDatabase.CAMPO_LISTO_PE + " = 2 " +
                    "WHERE " + LocalDatabase.CAMPO_DOCUMENTOID_PE + " = " + idDocumentoPrePedido;
                if (PedidosEncabezadoModel.updateARecord(query))
                {
                    List<int> idsToDelete = new List<int>();
                    idsToDelete.Add(idDocumentoPrePedido);
                    if (ConfiguracionModel.isLANPermissionActivated())
                        await PrePedidosController.deletePrePedidosLAN(idsToDelete);
                    else await PrePedidosController.deletePrePedidos(idsToDelete);
                    totalItemsPrePedServer -= 1;
                    textTotalPrePedidos.Text = "Prepedidos: " + totalItemsPrePedServer;
                    if (prePedidosList.Count > 0)
                        prePedidosList.RemoveAt(positionAEliminar);
                    dataGridViewPrePedidos.Rows.Remove(dataGridViewPrePedidos.Rows[positionAEliminar]);
                }
            }
        }

        private async void checkBoxTodosLosPrepedidos_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTodosLosPrepedidos.Focused)
            {
                if (checkBoxTodosLosPrepedidos.Checked)
                {
                    searchWordPrepedido = editBuscarPrepedido.Text.Trim();
                    comboBoxRutaCajaPrepedido.Enabled = false;
                    comboBoxSelectRutas.Enabled = false;
                    btnUpdateRoutes.Enabled = false;
                    queryTypePrePedido = 2;
                    resetearValoresPrePedidos(queryTypePrePedido);
                    await fillDataGridViewPrePedidos();
                } else
                {
                    comboBoxRutaCajaPrepedido.Enabled = true;
                    comboBoxSelectRutas.Enabled = true;
                    btnUpdateRoutes.Enabled = true;
                    fillComboBoxTipoRutaCajaPrepedido();
                    await fillComboBoxRutas(true);
                }
            }
        }

        private void fillComboBoxTipoRutaCajaPrepedido()
        {
            comboBoxRutaCajaPrepedido.Items.Clear();
            comboBoxRutaCajaPrepedido.Items.Add("Buscar por Ruta");
            comboBoxRutaCajaPrepedido.Items.Add("Buscar por Caja");
            comboBoxRutaCajaPrepedido.SelectedIndex = 0;
        }

        private ExpandoObject addMovement(int idPedido, int documentType, double baseUnits, double nonConvertibleUnits, double capturedUnits, 
            int nonConvertibleUnitId,int capturedUnitId, int capturedUnitType, double monto, double total, double rateDiscountPromo, 
            double newAmountDiscount, String observationDocumento, String observacionMovimiento, ClsItemModel itemModel, double discountRate, double price, int fiscalDocument) {
            dynamic response = new ExpandoObject();
            DocumentModel dvm = null;
            if (tabPosition == 0)
                dvm = DocumentModel.getAllDataDocumento(FormVenta.idDocument);
            else if (tabPosition == 1)
                dvm = DocumentModel.getAllDataDocumento(FormVenta.idDocument);
            newAmountDiscount = Convert.ToDouble(String.Format("{0:0.00}", newAmountDiscount));
            if (dvm != null) {
                MovimientosModel mdm = null;
                if (tabPosition == 0)
                {
                    mdm = MovimientosModel.armMovement(itemModel, FormVenta.idDocument,
                    baseUnits, nonConvertibleUnits, capturedUnits, nonConvertibleUnitId, capturedUnitId, capturedUnitType,
                    price, monto, total, documentType, dvm.nombreu, discountRate,
                    newAmountDiscount, observacionMovimiento, dvm.usuario_id, rateDiscountPromo);
                } else if (tabPosition == 1)
                {
                    mdm = MovimientosModel.armMovement(itemModel, FormVenta.idDocument,
                    baseUnits, nonConvertibleUnits, 0, nonConvertibleUnitId, capturedUnitId, capturedUnitType,
                    price, monto, total, documentType, dvm.nombreu, discountRate,
                    newAmountDiscount, observacionMovimiento, dvm.usuario_id, rateDiscountPromo);
                }
                if (mdm != null)
                {
                    response.position = mdm.position;
                    if (tabPosition == 0)
                        response.valor = agregarArticuloAlCarrito(FormVenta.idDocument, mdm, newAmountDiscount, itemModel.id, documentType, capturedUnits, capturedUnitId);
                    else if (tabPosition == 1)
                        response.valor = agregarArticuloAlCarrito(FormVenta.idDocument, mdm, newAmountDiscount, itemModel.id, documentType, capturedUnits, capturedUnitId);
                }
                else
                {
                    response.position = 0;
                    response.valor = 0;
                }
            }
            else
            {
                response.position = 0;
                response.valor = 0;
            }
            return response;
        }


        private int createDocument(int documentType, int idPedido, int fiscalDocument, String observation, int agenteId, string codigoCaja)
        {
            int idDocumentoCreated = 0;
            UserModel um = null;
            if (cotmosActive)
                um = UserModel.datosUsuarioParaElDocumento();
            else
            {
                dynamic responseUser = UserModel.getAgentById(agenteId);
                if (responseUser.value == 1)
                {
                    um = responseUser.um;
                    
                } else
                {
                    
                }
            }
            if (um != null)
            {
                String clave = CustomerModel.getClaveForAClient(FormVenta.idCustomer);
                DocumentModel dvm = new DocumentModel();
                if (clave.Equals("") && FormVenta.idCustomer < 0)
                {
                    clave = "CADCL" + FormVenta.idCustomer;
                    dvm.clave_cliente = clave;
                } else dvm.clave_cliente = clave;
                dvm.cliente_id = FormVenta.idCustomer;
                dvm.nombreu = um.Nombre;
                dvm.almacen_id = CajaModel.getAlmacenIdByCheckoutCode(codigoCaja);
                dvm.tipo_documento = documentType;
                dvm.factura = fiscalDocument;
                dvm.observacion = observation;
                dvm.fventa = FormVenta.idCustomer + MetodosGenerales.getCurrentDateAndHourForFolioVenta();
                dvm.usuario_id = um.id;
                dvm.ciddoctopedidocc = idPedido;
                dvm.pausado = 1;
                if (tabPosition == 0)
                {
                    idDocumentoCreated = DocumentModel.addNewDocument(dvm);
                    if (idDocumentoCreated > 0)
                    {
                        FormVenta.idDocument = idDocumentoCreated;
                        PedidosEncabezadoModel.marcarPedidoComoSurtidoONoSurtido(idPedido, 1);
                    }
                } else if (tabPosition == 1)
                {
                    idDocumentoCreated = DocumentModel.addNewDocument(dvm);
                    if (idDocumentoCreated > 0)
                    {
                        FormVenta.idDocument = idDocumentoCreated;
                        PedidosEncabezadoModel.marcarPedidoComoSurtidoONoSurtido(idPedido, 1);
                    }
                }
            }
            return idDocumentoCreated;
        }

        private int agregarArticuloAlCarrito(int idDocument, MovimientosModel mdm, double newAmountDiscount, int idItem, int documentType, double capturedUnits, 
            int capturedUnitId)
        {
            int response = 0;
            int respuesta = MovimientosModel.addNewMovimiento(mdm, newAmountDiscount);
            if (respuesta >= 1)
            {
                if (serverModeLAN)
                {
                    FormasDeCobroDocumentoModel.removePendingBalanceToTheLastFormOfCollectionOfTheDocument(idDocument);
                    response = 1;
                } else
                {
                    if (ItemModel.addAnItemToCart(idItem))
                    {
                        //Toast.makeText(getContext(), "Articulo agregado satisfactoriamente!", Toast.LENGTH_SHORT).show();
                        FormasDeCobroDocumentoModel.removePendingBalanceToTheLastFormOfCollectionOfTheDocument(idDocument);
                        response = 1;
                    }
                    else
                    {
                        response = -3;
                        //Toast.makeText(context, "Oops, currio un error al agregar el articulo!", Toast.LENGTH_SHORT).show();
                    }
                    //actualizarArticuloAgregado(context, position, idArt);
                }
            }
            else if (respuesta == -1)
            {
                response = -4;
                //Existe un movimiento con el mismo descuento
            }
            else
            {
                response = -1;
                //Toast.makeText(context, "Error al agregar el movimiento a la base de datos!", Toast.LENGTH_SHORT).show();
            }
            return response;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            btnRefresh.Enabled = false;
            int call = 0;
            if (tabPosition == 0)
                call = FormWaiting.CALL_COTIZACIONESMOSTRADOR;
            else if (tabPosition == 1)
                call = FormWaiting.CALL_PREPEDIDOS;
            formWaiting = new FormWaiting(this, call, 0, true, webActive, codigoCaja);
            formWaiting.ShowDialog();
        }

        public async Task processToUpdateCotMos()
        {
            // Pendientes de eliminar Listo = 2;
            String query = "SELECT " + LocalDatabase.CAMPO_DOCUMENTOID_PE + " FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " WHERE " +
                LocalDatabase.CAMPO_LISTO_PE + " = " + 2+" AND "+LocalDatabase.CAMPO_TYPE_PE+" = "+3;
            List<int> idsToDelete = PedidosEncabezadoModel.getIntListVlues(query);
            if (idsToDelete != null)
            {
                int response = 0;
                if (serverModeLAN)
                    response = await CotizacionesMostradorController.deleteCotizacionesMostradorLAN(idsToDelete);
                else response = await CotizacionesMostradorController.deleteCotizacionesMostrador(idsToDelete);
                if (response != 0)
                {
                    await doDownloadProcessPedidosCotizaciones(this);
                } else
                {
                    await doDownloadProcessPedidosCotizaciones(this);
                }
            }
            else
            {
                await doDownloadProcessPedidosCotizaciones(this);
            }
        }

        public static async Task doDownloadProcessPedidosCotizaciones(FrmPedidosCotizacionesSurtir frmPedidosCotizacionesSurtir)
        {
            int value = 0;
            dynamic response = new ExpandoObject();
            bool serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
            if (serverModeLAN)
                response = await CotizacionesMostradorController.downloadAllCotizacionesMostradorLAN();
            else response = await CotizacionesMostradorController.downloadAllCotizacionesMostrador();
            String message = response.description;
            if (response.value == 1)
            {
                value = 1;
            }
            else if (response.value == -404)
            {
                value = 2;
            }
            else if (response.value == -500)
            {
                value = 2;
            }
            else
            {
                value = 2;
            }
            if (frmPedidosCotizacionesSurtir != null)
                await frmPedidosCotizacionesSurtir.updateUIAfterDownload(value, true, message);
        }

        public async Task processToUpdatePrePedidos()
        {
            // Pendientes de eliminar Listo = 2;
            /*if (routeCajaIdPrePedido != 0 && !routeCajaCodePrePedido.Equals(""))
            {
                
            } else {
                if (frmWaiting != null)
                {
                    frmWaiting.Close();
                    frmWaiting = null;
                }
                FormMessage fm = new FormMessage("Validar Datos", "Tienes que seleccionar una ruta", 2);
                fm.ShowDialog();
                btnRefresh.Enabled = true;
            }*/
            dynamic response = new ExpandoObject();
            if (serverModeLAN)
            {
                String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                String query = "SELECT " + LocalDatabase.CAMPO_DOCUMENTOID_PE + " FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " WHERE " +
                LocalDatabase.CAMPO_LISTO_PE + " = " + 2 + " AND " + LocalDatabase.CAMPO_TYPE_PE + " = 4";
                List<int> idsToDelete = PedidosEncabezadoModel.getIntListVlues(query);
                if (idsToDelete != null)
                {
                    response = await PrePedidosController.deletePrePedidosLAN(idsToDelete);
                    if (response.value != 0)
                    {
                        int lastIdServer = PedidosEncabezadoModel.getLastIdPanel() - 1;
                        response = await PrePedidosController.getAllPrePedidosLAN(panelInstance, lastIdServer, routeCajaCodePrePedido,
                            LIMIT, "parameterName1", searchWordPrepedido, "parameterName2", searchWordPrepedido, totalItemsPrePedServer);
                        if (response.value == 1)
                        {
                            removeAllRepeatedMovesFromPreOrders(lastIdServer);
                        }
                        else
                        {
                            FormMessage fm = new FormMessage("Validar", "" + response.description, 2);
                            fm.ShowDialog();
                            removeAllRepeatedMovesFromPreOrders(lastIdServer);
                        }
                    }
                    else
                    {
                        int lastIdServer = PedidosEncabezadoModel.getLastIdPanel() - 1;
                        response = await PrePedidosController.getAllPrePedidosLAN(panelInstance, lastIdServer, routeCajaCodePrePedido,
                            LIMIT, "parameterName1", searchWordPrepedido, "parameterName2", searchWordPrepedido, totalItemsPrePedServer);
                        if (response.value == 1)
                        {
                            removeAllRepeatedMovesFromPreOrders(lastIdServer);
                            //dynamic response1 = await PrePedidosController.validarDocumentosEnPausaTipoPrepedido();
                            /*PopupNotifier popup = new PopupNotifier();
                            popup.Image = ClsMetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                            popup.TitleColor = Color.Blue;
                            popup.TitleText = "Datos Actualizados";
                            popup.ContentText = response.description;
                            popup.ContentColor = Color.Red;
                            popup.Popup();*/
                        }
                        else
                        {
                            FormMessage fm = new FormMessage("Validar", "" + response.description, 2);
                            fm.ShowDialog();
                            removeAllRepeatedMovesFromPreOrders(lastIdServer);
                        }
                    }
                }
                else
                {
                    int lastIdServer = PedidosEncabezadoModel.getLastIdPanel() - 1;
                    response = await PrePedidosController.getAllPrePedidosLAN(panelInstance, lastIdServer, routeCajaCodePrePedido,
                        LIMIT, "parameterName1", searchWordPrepedido, "parameterName2", searchWordPrepedido, totalItemsPrePedServer);
                    if (response.value == 1)
                    {
                        removeAllRepeatedMovesFromPreOrders(lastIdServer);
                    }
                    else
                    {
                        FormMessage fm = new FormMessage("Validar", "" + response.description, 2);
                        fm.ShowDialog();
                        removeAllRepeatedMovesFromPreOrders(lastIdServer);
                    }
                }
                if (formWaiting != null)
                {
                    formWaiting.Dispose();
                    formWaiting.Close();
                }
                if (editFolioPrePedido.Text.ToString().Trim().Equals(""))
                {
                    queryTypePrePedido = 0;
                    resetearValoresPrePedidos(queryTypePrePedido);
                    await fillDataGridViewPrePedidos();
                }
                else
                {
                    queryTypePrePedido = 1;
                    resetearValoresPrePedidos(queryTypePrePedido);
                    await fillDataGridViewPrePedidos();
                }
                btnRefresh.Enabled = true;
            }
            else
            {
                String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                String query = "SELECT " + LocalDatabase.CAMPO_DOCUMENTOID_PE + " FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " WHERE " +
                LocalDatabase.CAMPO_LISTO_PE + " = " + 2 + " AND " + LocalDatabase.CAMPO_TYPE_PE + " = 4";
                List<int> idsToDelete = PedidosEncabezadoModel.getIntListVlues(query);
                if (idsToDelete != null)
                {
                    response = await PrePedidosController.deletePrePedidos(idsToDelete);
                    if (response.value != 0)
                    {
                        int lastIdServer = PedidosEncabezadoModel.getLastIdPanel() - 1;
                        response = await PrePedidosController.getAllPrePedidosAPI(routeCajaCodePrePedido, lastIdServer,
                            LIMIT, "parameterName1", searchWordPrepedido, "parameterName2", searchWordPrepedido, totalItemsPrePedServer);
                        if (response.value == 1)
                        {
                            removeAllRepeatedMovesFromPreOrders(lastIdServer);
                        }
                        else
                        {
                            FormMessage fm = new FormMessage("Validar", "" + response.description, 2);
                            fm.ShowDialog();
                            removeAllRepeatedMovesFromPreOrders(lastIdServer);
                        }
                    }
                    else
                    {
                        int lastIdServer = PedidosEncabezadoModel.getLastIdPanel() - 1;
                        response = await PrePedidosController.getAllPrePedidosAPI(routeCajaCodePrePedido, lastIdServer,
                            LIMIT, "parameterName1", searchWordPrepedido, "parameterName2", searchWordPrepedido, totalItemsPrePedServer);
                        if (response.value == 1)
                        {
                            removeAllRepeatedMovesFromPreOrders(lastIdServer);
                        }
                        else
                        {
                            FormMessage fm = new FormMessage("Validar", "" + response.description, 2);
                            fm.ShowDialog();
                            removeAllRepeatedMovesFromPreOrders(lastIdServer);
                        }
                    }
                }
                else
                {
                    int lastIdServer = PedidosEncabezadoModel.getLastIdPanel() - 1;
                    response = await PrePedidosController.getAllPrePedidosAPI(routeCajaCodePrePedido, lastIdServer,
                        LIMIT, "parameterName1", searchWordPrepedido, "parameterName2", searchWordPrepedido, totalItemsPrePedServer);
                    if (response.value == 1)
                    {
                        removeAllRepeatedMovesFromPreOrders(lastIdServer);
                        //dynamic response1 = await PrePedidosController.validarDocumentosEnPausaTipoPrepedido();
                        /*PopupNotifier popup = new PopupNotifier();
                        popup.Image = ClsMetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                        popup.TitleColor = Color.Blue;
                        popup.TitleText = "Datos Actualizados";
                        popup.ContentText = response.description;
                        popup.ContentColor = Color.Red;
                        popup.Popup();*/
                    }
                    else
                    {
                        FormMessage fm = new FormMessage("Validar", "" + response.description, 2);
                        fm.ShowDialog();
                        removeAllRepeatedMovesFromPreOrders(lastIdServer);
                    }
                }
                if (formWaiting != null)
                {
                    formWaiting.Dispose();
                    formWaiting.Close();
                }
                if (editFolioPrePedido.Text.ToString().Trim().Equals(""))
                {
                    queryTypePrePedido = 0;
                    resetearValoresPrePedidos(queryTypePrePedido);
                    await fillDataGridViewPrePedidos();
                }
                else
                {
                    queryTypePrePedido = 1;
                    resetearValoresPrePedidos(queryTypePrePedido);
                    await fillDataGridViewPrePedidos();
                }
                btnRefresh.Enabled = true;
            }
        }

        public async Task updateUIAfterDownload(int value, bool showMessage, String description)
        {
            await Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => {
                if (formWaiting != null)
                {
                    formWaiting.Dispose();
                    formWaiting.Close();
                }
                folioCotMos = editBusquedaFolioCotizacionesMostrador.Text.Trim();
                if (!folioCotMos.Equals(""))
                {
                    queryTypeCotMos = 1;
                    resetearValoresCotMostrador(queryTypeCotMos);
                    fillDataGridViewCotMostrador();
                }
                else
                {
                    queryTypeCotMos = 0;
                    resetearValoresCotMostrador(queryTypeCotMos);
                    fillDataGridViewCotMostrador();
                }
                if (showMessage)
                {
                    /*PopupNotifier popup = new PopupNotifier();
                    popup.Image = ClsMetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                    popup.TitleColor = Color.Blue;
                    popup.TitleText = "Datos Actualizados";
                    popup.ContentText = description;
                    popup.ContentColor = Color.Red;
                    popup.Popup();*/
                }
                btnRefresh.Enabled = true;
            }), DispatcherPriority.Background, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void resetearValoresCotMostrador(int queryType)
        {
            this.queryTypeCotMos = queryType;
            queryCotMos = "";
            queryTotalsCotMos = "";
            totalItemsCotMos = 0;
            lastIdCotMos = PedidosEncabezadoModel.getLastId();
            progressCotMos = 0;
            cotMostradorList.Clear();
            dataGridViewCotizacionesM.Rows.Clear();
        }

        private void resetearValoresPrePedidos(int queryType)
        {
            this.queryTypePrePedido = queryType;
            queryPrePedidoTemp = "";
            queryPrePedidoReport = "";
            queryTotalsPrePedido = "";
            totalItemsPrePedServer = 0;
            lastIdPrePedido = 0;
            progressPrePedido = 0;
            prePedidosList.Clear();
            firstVisibleRowPrePedido = 0;
            dataGridViewPrePedidos.Rows.Clear();
        }

    }
}
