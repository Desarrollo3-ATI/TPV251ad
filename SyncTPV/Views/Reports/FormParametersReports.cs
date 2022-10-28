using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace SyncTPV.Views.Reports
{
    public partial class FormParametersReports : Form
    {
        String queryPrepedidoReport = "", parameterName1 = "", parameterName2 = "", parameterValue1 = "", parameterValue2 = "",
            routeCodePrePedido = "";
        int queryType = 0, routeIdPrePedido = 0, idAgentByRoute = 0;
        private bool reporteTodasLasRutas = false, pausados = false;
        private FormWaiting frmWaiting;
        private int call = 0;
        private FormGeneralsReports formGeneralsReports;
        private bool serverModeLAN = false;
        private bool showDocs = false, showCredits = false, showPagos = false, showIngresos = false, showRetiros = false;

        public FormParametersReports()
        {
            InitializeComponent();
        }

        public FormParametersReports(FormGeneralsReports formGeneralsReports, int call, bool serverModeLAN)
        {
            InitializeComponent();
            this.formGeneralsReports = formGeneralsReports;
            this.call = call;
            this.serverModeLAN = serverModeLAN;
        }

        private void checkBoxDocs_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDocs.Checked)
                showDocs = true;
            else showDocs = false;
        }

        private void checkBoxCreditos_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCreditos.Checked)
                showCredits = true;
            else showCredits = false;
        }

        private void checkBoxIngresos_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxIngresos.Checked)
                showIngresos = true;
            else showIngresos = false;
        }

        private void checkBoxRetiros_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRetiros.Checked)
                showRetiros = true;
            else showRetiros = false;
        }

        private void checkBoxPagos_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPagos.Checked)
                showPagos = true;
            else showPagos = false;
        }

        public FormParametersReports(String queryPrepedido,
            String parameterName1, String parameterName2, String parameterValue1, String parameterValue2, int queryType, String routeCodePrePedido,
            int routeIdPrePedido, int idAgentByRoute)
        {
            this.queryPrepedidoReport = queryPrepedido;
            this.parameterName1 = parameterName1;
            this.parameterName2 = parameterName2;
            this.parameterValue1 = parameterValue1;
            this.parameterValue2 = parameterValue2;
            this.queryType = queryType;
            this.routeCodePrePedido = routeCodePrePedido;
            this.routeIdPrePedido = routeIdPrePedido;
            this.idAgentByRoute = idAgentByRoute;
            InitializeComponent();            
        }

        private void FormParametersReports_Load(object sender, EventArgs e)
        {
            btnClose.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.back_white, 35, 35);
            if (formGeneralsReports != null)
            {
                checkBoxDocs.Checked = false;
                checkBoxCreditos.Checked = false;
                checkBoxIngresos.Checked = false;
                checkBoxRetiros.Checked = false;
                tableLayoutPanelParametrosReportes.RowStyles[0].SizeType = SizeType.Absolute;
                tableLayoutPanelParametrosReportes.RowStyles[0].Height = 0;
            } else
            {
                tableLayoutPanelParametrosReportes.RowStyles[1].SizeType = SizeType.Absolute;
                tableLayoutPanelParametrosReportes.RowStyles[1].Height = 0;
            }
        }

        private void checkBoxTodasLasRutas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTodasLasRutas.Checked)
                reporteTodasLasRutas = true;
            else reporteTodasLasRutas = false;
        }

        private void checkBoxIncluirPendientes_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxIncluirPendientes.Checked)
                pausados = true;
            else pausados = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGeneratePdf_Click(object sender, EventArgs e)
        {
            SECUDOC.corteLog("**Imprimiendo Reporte - "+ DateTime.Now+ " - IdUsuario: "+ClsRegeditController.getIdUserInTurn());
            try
            {
                String RutaTPV = (AdminDll.General.RutaInicial + "\\Data\\").Replace("\\", "/");
                String Ruta = (AdminDll.General.RutaInicial + "\\SQLiteDatabaseBrowserPortable\\Other\\").Replace("\\", "/");
                String newfile = "CorteTPV-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".db";
                string[] Archivos = Directory.GetFiles(Ruta, "*.db").OrderBy(d => new FileInfo(d).CreationTime).ToArray();
                SECUDOC.corteLog("Corte de caja Realizada: " + DateTime.Now.ToString() + " (" + Archivos.Length+")");
               
                    File.Copy(RutaTPV + "SyncTPV.db", (Ruta + newfile), true);
                    if (Archivos.Length > 99)
                    {
                        int elementosAEliminar = Archivos.Length - 99;
                        for (int i = 0; i < elementosAEliminar; i++)
                        {
                                File.Delete(Archivos[i]);
                            
                        }
                    }

                
            }
            catch (Exception error)
            {
                SECUDOC.writeLog("*Error al respaldar la base de datos*\n" + error.ToString());
            }
            if (formGeneralsReports != null)
            {
                processtoPrintTicketReport();
            } else
            {
                frmWaiting = new FormWaiting(this, 0);
                frmWaiting.StartPosition = FormStartPosition.CenterScreen;
                frmWaiting.ShowDialog();
            }
        }

        private async Task processtoPrintTicketReport()
        {
            clsTicket Ticket = new clsTicket();
            await Ticket.createAndPrintReporteCajaTicket(serverModeLAN, showDocs, showCredits, showPagos, showIngresos, showRetiros);
            PopupNotifier popup = new PopupNotifier();
            popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
            popup.TitleColor = Color.Blue;
            popup.TitleText = "Imprimiendo";
            popup.ContentText = "Procesando información...";
            popup.ContentColor = Color.Blue;
            popup.Popup();
        }

        public async Task processtoDownloadAllPrepedidosFromAllRoutes(String searchWordPrepedidos)
        {
            int respuesta = 0;
            if (reporteTodasLasRutas)
            {
                List<RutaModel> routesList = await getAllRoutes();
                if (routesList != null && routesList.Count > 0)
                {
                    foreach (var route in routesList)
                    {
                        routeIdPrePedido = route.id;
                        routeCodePrePedido = route.code;
                        respuesta = await processToUpdatePrePedidos("parameterName1", searchWordPrepedidos, "parameterName2", searchWordPrepedidos);
                        if (respuesta != 1)
                            break;
                    }
                }
            } else
            {
                respuesta = await processToUpdatePrePedidos("parameterName1", searchWordPrepedidos, "parameterName2", searchWordPrepedidos);
            }
            if (respuesta == 1)
            {
                await processToGeneratePdfPrepedidos();
            } else
            {
                await processToGeneratePdfPrepedidos();
            }
        }

        private async Task processToGeneratePdfPrepedidos()
        {
            String nameReport = "Pedidos";
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
            bool created = false;
            if (reporteTodasLasRutas)
            {
                if (pausados)
                {
                    if (queryType == 0)
                    {
                        String query = "SELECT * FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " WHERE (" +
                            LocalDatabase.CAMPO_SURTIDO_PE + " = " + 0 + " || "+ LocalDatabase.CAMPO_SURTIDO_PE + " = 1) AND (" +
                        LocalDatabase.CAMPO_LISTO_PE + " = 0) AND " + LocalDatabase.CAMPO_TYPE_PE + " = " + 4 + " AND " +
                        LocalDatabase.CAMPO_ID_PEDIDOENCABEZADO + " > 0 ORDER BY " +
                        LocalDatabase.CAMPO_ID_PEDIDOENCABEZADO;
                        created = await ClsPdfMethods.crearPdfPrepedidos(enterpriseName, filePath, query, parameterName1, parameterName2,
                   parameterValue1, parameterValue2, queryType);
                    }
                }
                else
                {
                    if (queryType == 0)
                    {
                        String query = "SELECT * FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " WHERE (" +
                            LocalDatabase.CAMPO_SURTIDO_PE + " = " + 0 + ") AND " +
                        LocalDatabase.CAMPO_LISTO_PE + " != " + 2 + " AND " + LocalDatabase.CAMPO_TYPE_PE + " = " + 4 + " AND " +
                        LocalDatabase.CAMPO_ID_PEDIDOENCABEZADO + " > 0 ORDER BY " +
                        LocalDatabase.CAMPO_ID_PEDIDOENCABEZADO;
                        created = await ClsPdfMethods.crearPdfPrepedidos(enterpriseName, filePath, query, parameterName1, parameterName2,
                   parameterValue1, parameterValue2, queryType);
                    }
                }
               
            }
            else
            {
                if (pausados)
                {
                    if (queryType == 0)
                    {
                        String query = "SELECT * FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " WHERE (" + 
                            LocalDatabase.CAMPO_SURTIDO_PE + " = " + 0 + " || "+ LocalDatabase.CAMPO_SURTIDO_PE + " = 1) AND " +
                    LocalDatabase.CAMPO_LISTO_PE + " = 0 AND " + LocalDatabase.CAMPO_TYPE_PE + " = " + 4 + " AND " +
                    LocalDatabase.CAMPO_CNOMBREAGENTECC_PE + " = " + idAgentByRoute + " AND " +
                    LocalDatabase.CAMPO_ID_PEDIDOENCABEZADO + " > 0 ORDER BY " +
                    LocalDatabase.CAMPO_ID_PEDIDOENCABEZADO;
                        created = await ClsPdfMethods.crearPdfPrepedidos(enterpriseName, filePath, query, parameterName1, parameterName2,
                   parameterValue1, parameterValue2, queryType);
                    }
                } else
                {
                    created = await ClsPdfMethods.crearPdfPrepedidos(enterpriseName, filePath, queryPrepedidoReport, parameterName1, parameterName2,
                   parameterValue1, parameterValue2, queryType);
                }
            }
            if (frmWaiting != null)
                frmWaiting.Close();
            if (created)
            {
                FormMessage formMessage = new FormMessage("Genial", "Pdf Generado correctamente", 1);
                formMessage.ShowDialog();
            }
            if (frmWaiting != null)
                frmWaiting.Close();
        }

        private async Task<List<RutaModel>> getAllRoutes()
        {
            List<RutaModel> routesList = null;
            await Task.Run(() => {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_RUTA + " WHERE " + LocalDatabase.CAMPO_ID_RUTA + " > " + 0 +
                    " ORDER BY " + LocalDatabase.CAMPO_ID_RUTA;
                routesList = RutaModel.getAllRoutes(query);
            });
            return routesList;
        }

        public async Task<int> processToUpdatePrePedidos(String parameterName1, String parameterValue1, String parameterName2, String parameterValue2)
        {
            int respuesta = 0;
            // Pendientes de eliminar Listo = 2;
            if (routeIdPrePedido != 0 && !routeCodePrePedido.Equals(""))
            {
                dynamic response = new ExpandoObject();
                if (ConfiguracionModel.isLANPermissionActivated())
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
                            int lastIdServer = PedidosEncabezadoModel.getLastIdPanel();
                            response = await PrePedidosController.getAllPrePedidosLAN(panelInstance, lastIdServer, routeCodePrePedido,
                                1000, parameterName1, parameterValue1, parameterName2, parameterValue2, 0);
                            if (response.value == 1)
                            {
                                FrmPedidosCotizacionesSurtir.removeAllRepeatedMovesFromPreOrders(lastIdServer);
                                //dynamic response1 = await PrePedidosController.validarDocumentosEnPausaTipoPrepedido();
                                /*PopupNotifier popup = new PopupNotifier();
                                popup.Image = ClsMetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                                popup.TitleColor = Color.Blue;
                                popup.TitleText = "Datos Actualizados";
                                popup.ContentText = response.description;
                                popup.ContentColor = Color.Red;
                                popup.Popup();*/
                                respuesta = 1;
                            }
                            else
                            {
                                /*FormMessage fm = new FormMessage("Validar", "" + response.description, 2);
                                fm.ShowDialog();*/
                                FrmPedidosCotizacionesSurtir.removeAllRepeatedMovesFromPreOrders(lastIdServer);
                            }
                        }
                        else
                        {
                            int lastIdServer = PedidosEncabezadoModel.getLastIdPanel();
                            response = await PrePedidosController.getAllPrePedidosLAN(panelInstance, lastIdServer, routeCodePrePedido,
                                1000, parameterName1, parameterValue1, parameterName2, parameterValue2, 0);
                            if (response.value == 1)
                            {
                                FrmPedidosCotizacionesSurtir.removeAllRepeatedMovesFromPreOrders(lastIdServer);
                                //dynamic response1 = await PrePedidosController.validarDocumentosEnPausaTipoPrepedido();
                                /*PopupNotifier popup = new PopupNotifier();
                                popup.Image = ClsMetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                                popup.TitleColor = Color.Blue;
                                popup.TitleText = "Datos Actualizados";
                                popup.ContentText = response.description;
                                popup.ContentColor = Color.Red;
                                popup.Popup();*/
                                respuesta = 1;
                            }
                            else
                            {
                                /*FormMessage fm = new FormMessage("Validar", "" + response.description, 2);
                                fm.ShowDialog();*/
                                FrmPedidosCotizacionesSurtir.removeAllRepeatedMovesFromPreOrders(lastIdServer);
                            }
                        }
                    }
                    else
                    {
                        int lastIdServer = PedidosEncabezadoModel.getLastIdPanel();
                        response = await PrePedidosController.getAllPrePedidosLAN(panelInstance, lastIdServer, routeCodePrePedido,
                            1000, parameterName1, parameterValue1, parameterName2, parameterValue2,0);
                        if (response.value == 1)
                        {
                            //dynamic response1 = await PrePedidosController.validarDocumentosEnPausaTipoPrepedido();
                            /*PopupNotifier popup = new PopupNotifier();
                            popup.Image = ClsMetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                            popup.TitleColor = Color.Blue;
                            popup.TitleText = "Datos Actualizados";
                            popup.ContentText = response.description;
                            popup.ContentColor = Color.Red;
                            popup.Popup();*/
                            FrmPedidosCotizacionesSurtir.removeAllRepeatedMovesFromPreOrders(lastIdServer);
                            respuesta = 1;
                        }
                        else
                        {
                            /*FormMessage fm = new FormMessage("Validar", "" + response.description, 2);
                            fm.ShowDialog();*/
                            FrmPedidosCotizacionesSurtir.removeAllRepeatedMovesFromPreOrders(lastIdServer);
                        }
                    }
                    /*if (frmWaiting != null)
                        frmWaiting.Close();
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
                    btnRefresh.Enabled = true;*/
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
                            int lastIdServer = PedidosEncabezadoModel.getLastIdPanel();
                            response = await PrePedidosController.getAllPrePedidosAPI(routeCodePrePedido, lastIdServer,
                                0, parameterName1, parameterValue1, parameterName2, parameterValue2,0);
                            if (response.value == 1)
                            {
                                //dynamic response1 = await PrePedidosController.validarDocumentosEnPausaTipoPrepedido();
                                /*PopupNotifier popup = new PopupNotifier();
                                popup.Image = ClsMetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                                popup.TitleColor = Color.Blue;
                                popup.TitleText = "Datos Actualizados";
                                popup.ContentText = response.description;
                                popup.ContentColor = Color.Red;
                                popup.Popup();*/
                                FrmPedidosCotizacionesSurtir.removeAllRepeatedMovesFromPreOrders(lastIdServer);
                                respuesta = 1;
                            }
                            else
                            {
                                /*FormMessage fm = new FormMessage("Validar", "" + response.description, 2);
                                fm.ShowDialog();*/
                                FrmPedidosCotizacionesSurtir.removeAllRepeatedMovesFromPreOrders(lastIdServer);
                            }
                        }
                        else
                        {
                            int lastIdServer = PedidosEncabezadoModel.getLastIdPanel();
                            response = await PrePedidosController.getAllPrePedidosAPI(routeCodePrePedido, lastIdServer,
                                0, parameterName1, parameterValue1, parameterName2, parameterValue2,0);
                            if (response.value == 1)
                            {
                                //dynamic response1 = await PrePedidosController.validarDocumentosEnPausaTipoPrepedido();
                                /*PopupNotifier popup = new PopupNotifier();
                                popup.Image = ClsMetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                                popup.TitleColor = Color.Blue;
                                popup.TitleText = "Datos Actualizados";
                                popup.ContentText = response.description;
                                popup.ContentColor = Color.Red;
                                popup.Popup();*/
                                FrmPedidosCotizacionesSurtir.removeAllRepeatedMovesFromPreOrders(lastIdServer);
                                respuesta = 1;
                            }
                            else
                            {
                                /*FormMessage fm = new FormMessage("Validar", "" + response.description, 2);
                                fm.ShowDialog();*/
                                FrmPedidosCotizacionesSurtir.removeAllRepeatedMovesFromPreOrders(lastIdServer);
                            }
                        }
                    }
                    else
                    {
                        int lastIdServer = PedidosEncabezadoModel.getLastIdPanel();
                        response = await PrePedidosController.getAllPrePedidosAPI(routeCodePrePedido, lastIdServer,
                            0, parameterName1, parameterValue1, parameterName2, parameterValue2,0);
                        if (response.value == 1)
                        {
                            //dynamic response1 = await PrePedidosController.validarDocumentosEnPausaTipoPrepedido();
                            /*PopupNotifier popup = new PopupNotifier();
                            popup.Image = ClsMetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                            popup.TitleColor = Color.Blue;
                            popup.TitleText = "Datos Actualizados";
                            popup.ContentText = response.description;
                            popup.ContentColor = Color.Red;
                            popup.Popup();*/
                            FrmPedidosCotizacionesSurtir.removeAllRepeatedMovesFromPreOrders(lastIdServer);
                            respuesta = 1;
                        }
                        else
                        {
                            /*FormMessage fm = new FormMessage("Validar", "" + response.description, 2);
                            fm.ShowDialog();*/
                            FrmPedidosCotizacionesSurtir.removeAllRepeatedMovesFromPreOrders(lastIdServer);
                        }
                    }
                    /*if (frmWaiting != null)
                        frmWaiting.Close();
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
                    btnRefresh.Enabled = true;*/
                }
            }
            else
            {
                /*if (frmWaiting != null)
                    frmWaiting.Close();
                FormMessage fm = new FormMessage("Validar Datos", "Tienes que seleccionar una ruta", 2);
                fm.ShowDialog();
                btnRefresh.Enabled = true;*/
            }
            return respuesta;
        }

    }
}
