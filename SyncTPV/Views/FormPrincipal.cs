using AdminDll;
using DbStructure;
using iTextSharp.text;
using Newtonsoft.Json.Linq;
using SyncTPV.Controllers;
using SyncTPV.Controllers.Downloads;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using SyncTPV.Views;
using SyncTPV.Views.AperturaTurno;
using SyncTPV.Views.Entries;
using SyncTPV.Views.Reports;
using SyncTPV.Views.Taras;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using Tulpep.NotificationWindow;
using wsROMClases;
using wsROMClases.Models;

namespace SyncTPV
{
    public partial class FormPrincipal : Form
    {
        public static Boolean doInitialCharge = false;
        string MensajeError = "", WS = "";
        string rutaString = "";
        string FechaHora = "";
        int Error = 0, TotalDocEnv = 0, TotalDoc = 0, Ultimoid = 0;
        bool banderaDescargandoDatos = false;
        string[] ArregloImagenes, ArregloImagenesClientes;
        public int idespecial { get; set; }
        public String tipoSync { get; set; } = "NORMAL";
        private bool applicationExist = false, loggingOut = false;

        FormMessage msj;
        /* Variables datagrid promocionnes */
        private int LIMIT = 50;
        private int progress = 0;
        private int lastId = 0;
        private int totalItems = 0, queryType = 0;
        private String query = "", queryTotals = "", itemCodeOrName = "";
        private DateTime lastLoading;
        private int firstVisibleRow;
        private ScrollBars gridScrollBars;
        private List<ClsPromocionesModel> promosList;
        private List<ClsPromocionesModel> promosListTemp;
        FormPrincipal pp;
        /** Varibales para realizar el envio de datos */
        public static int methodSend = 1;
        public static int envioDeDatos = 0;
        private FrmAperturaTurno frmAperturaTurno;
        private FormWaiting formWaiting;
        private String comInstance = "", panelInstance = "";
        private bool serverModeLAN = false, webActive = false;
        private bool cotmosActive = false;
        private String codigoCaja = "";
        private AperturaTurnoModel atm = null;
        public FormPrincipal(bool cotmosActive)
        {
            promosList = new List<ClsPromocionesModel>();
            if (getPrevInstance())
            {
                this.Close();
                Environment.Exit(0);
            }
            InitializeComponent();
            banderaDescargandoDatos = false;
            buttonsEvents();
            pictureBoxLogoSuperior.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.synctpvlogo, 67, 70);
            btnCerrar.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.close, 35, 35);
            btnTestLan.Visible = false;
            this.cotmosActive = cotmosActive;
        }

        private void buttonsEvents()
        {
            btnVenta.Click += new EventHandler(btnVenta_Click);
            btnCliente.Click += new EventHandler(btnCliente_Click);
            btnArticulos.Click += new EventHandler(btnArticulos_Click);
            btnHistorialDocumentosFrmPrincipal.Click += new EventHandler(btnHistorialDocumentosFrmPrincipal_Click);
            btnCorteDeCaja.Click += new EventHandler(btnCorteDeCaja_Click);
            btnReportsFrmPrincipal.Click += new EventHandler(btnReportsFrmPrincipal_Click);
            aperturaDeCajaToolStripMenuItem.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.store_black, 40, 40);
        }

        private static bool getPrevInstance()
        {
            //get the name of current process, i,e the process 
            //name of this current application
            string currPrsName = Process.GetCurrentProcess().ProcessName;
            //Get the name of all processes having the 
            //same name as this process name 
            Process[] allProcessWithThisName
                         = Process.GetProcessesByName(currPrsName);
            //if more than one process is running return true.
            //which means already previous instance of the application 
            //is running
            if (allProcessWithThisName.Length > 1)
            {
                MessageBox.Show("Already Running");
                return true; // Yes Previous Instance Exist
            }
            else
            {
                return false; //No Prev Instance Running
            }
        }


        private async void frmPrincipalPrueba_Load(object sender, EventArgs e)
        {
            await validarUltimaApertura();
            await validarPermisoPrepedido();
            await validateOptionsToServerModeLANOrWeb();
            pp = this;
            GeneralTxt.LOGINSUPERVISOR = false;
            await fillInformationForUserAndCheckout();
            try
            {
                verifyServerOrLocalLicense();
            }
            catch (Exception ex)
            {
                SECUDOC.writeLog(ex.ToString());
            }
            this.dataGridViewPromotionsFrmPincipal.CellBorderStyle = DataGridViewCellBorderStyle.None;
            fillDataGridPromotions();
        }
        private async Task validarUltimaApertura()
        {
            String queryApertura = "select * from AperturaTurno ORDER by id desc LIMIT 1";
            atm = AperturaTurnoModel.getARecord(queryApertura);
            if (atm != null)
            {
                editBoxUltimaApertura.Text = atm.fechaHora.ToString();
            }
            else
            {
                editBoxUltimaApertura.Text = "No se encontraron aperturas de turno";
            }
        }
        private async Task validarPermisoPrepedido()
        {
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
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
            if (UserModel.doYouHavePermissionPrepedido())
            {
                btnVenta.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.entregar_blanco, 35, 35);
                btnVenta.Text = "Entregas";
                btnVenta.Height = 100;
                btnVenta.FlatAppearance.BorderSize = 1;
                btnHistorialDocumentosFrmPrincipal.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.historial_documentos, 35, 35);
                btnHistorialDocumentosFrmPrincipal.Text = "Historial de \nEntregas (F3)";
                btnHistorialDocumentosFrmPrincipal.Height = 100;
                btnHistorialDocumentosFrmPrincipal.FlatAppearance.BorderSize = 1;
                btnHistorialDocumentosFrmPrincipal.Location = new Point(3, btnVenta.Location.Y + 105);
                btnCliente.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.cliente_blanco, 35, 35);
                btnCliente.Height = 100;
                btnCliente.Location = new Point(3, btnHistorialDocumentosFrmPrincipal.Location.Y + 105);
                btnCliente.FlatAppearance.BorderSize = 1;
                btnArticulos.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.articulos, 35, 35);
                btnArticulos.Location = new Point(3, btnCliente.Location.Y + 105);
                btnArticulos.Height = 100;
                btnArticulos.FlatAppearance.BorderSize = 1;
                btnCorteDeCaja.Visible = false;
                btnIngresos.Visible = false;
               
                btnReportsFrmPrincipal.Text = "Reporte de \nEntregas (F6)";
                btnReportsFrmPrincipal.Location = new Point(3, btnArticulos.Location.Y + 105);
                btnReportsFrmPrincipal.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.reportes_blanco, 35, 35);
                btnReportsFrmPrincipal.Height = 100;
                btnReportsFrmPrincipal.FlatAppearance.BorderSize = 1;
                btnReportsFrmPrincipal.Visible = false;
                textPromotionsFrmPrincipal.Visible = false;
                dataGridViewPromotionsFrmPincipal.Visible = false;
                imgSinDatos.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.tpv_marcaagua, 300, 300);
                imgSinDatos.BringToFront();
                imgSinDatos.Visible = true;
                aperturaDeCajaToolStripMenuItem.Visible = false;
                cargaInicialToolStripMenuItem.Visible = false;
                actualizarDatosToolStripMenuItem.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.update_data_black, 20, 20);
                actualizarDatosToolStripMenuItem.Padding = new Padding(10, 10, 10, 10);
                actualizarDatosToolStripMenuItem.TextAlign = ContentAlignment.MiddleCenter;
                sendDataToolStripMenuItem.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.upload, 20, 20);
                sendDataToolStripMenuItem.Padding = new Padding(10, 10, 10, 10);
                descargasToolStripMenuItem.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.download, 20, 20);
                descargasToolStripMenuItem.Padding = new Padding(10, 10, 10, 10);
                btnTaras.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.tara_white, 35, 35);
                btnTaras.Visible = true;
                //btnTaras.Location = new Point(3, btnReportsFrmPrincipal.Location.Y +105);
                btnTaras.Location = new Point(3, btnArticulos.Location.Y + 105);
                btnTaras.Text = "Taras/\nCajas";
                btnTaras.Height = 100;
                btnTaras.FlatAppearance.BorderSize = 1;
                configuraciónToolStripMenuItem.Padding = new Padding(10, 10, 10, 10);
                soporteToolStripMenuItem.Padding = new Padding(10, 10, 10, 10);
                acercaDeToolStripMenuItem.Padding = new Padding(10, 10, 10, 10);
                cerrarSesiónToolStripMenuItem.Padding = new Padding(10, 10, 10, 10);
                salirToolStripMenuItem.Padding = new Padding(10, 10, 10, 10);
            }
            else
            {
                btnVenta.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.carrito, 35, 35);
                btnVenta.Height = 60;
                btnVenta.Text = "Ventas (F2)";
                btnCliente.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.cliente_blanco, 35, 35);
                btnCliente.Height = 60;
                btnCliente.Text = "Clientes";
                btnCliente.Location = new Point(3, 69);
                btnArticulos.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.articulos, 35, 35);
                btnArticulos.Height = 60;
                btnArticulos.Location = new Point(3, 135);
                btnHistorialDocumentosFrmPrincipal.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.historial_documentos, 35, 35);
                btnHistorialDocumentosFrmPrincipal.Height = 60;
                btnHistorialDocumentosFrmPrincipal.Location = new Point(3, 201);
                btnHistorialDocumentosFrmPrincipal.Text = "Historial de \nVentas (F3)";
                btnCorteDeCaja.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.corte_caja_blanco, 35, 35);
                btnCorteDeCaja.Height = 60;
                btnCorteDeCaja.Location = new Point(3, 267);
                btnCorteDeCaja.Text = "Retiros/Corte\n de Caja(F5)";
                btnCorteDeCaja.Visible = true;
                btnIngresos.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.ingreso_white, 35, 35);
                btnIngresos.Height = 60;
                btnIngresos.Visible = true;
                btnIngresos.Location = new Point(3, 333);
                btnReportsFrmPrincipal.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.reportes_blanco, 35, 35);
                btnReportsFrmPrincipal.Height = 60;
                btnReportsFrmPrincipal.Location = new Point(3, 399);
                btnReportsFrmPrincipal.Visible = true;
                btnReportsFrmPrincipal.Text = "Reporte de \nCaja (F6)";
                btnReimpresionTickets.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.printer_white, 35, 35);
                btnReimpresionTickets.Height = 60;
                btnReimpresionTickets.Location = new Point(3, 465);
                
                btnReimpresionTickets.Text = "Resumen de \nTickets";
                actualizarDatosToolStripMenuItem.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.update_data_black, 20, 20);
                sendDataToolStripMenuItem.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.upload, 20, 20);
                descargasToolStripMenuItem.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.download, 20, 20);
                btnTaras.Visible = false;
            }
        }

        private async Task fillInformationForUserAndCheckout()
        {
            String userName = "";
            String checkoutName = "";
            await Task.Run(async () =>
            {
                if (cotmosActive)
                {
                    checkoutName = "Cotización de Mostrador";
                } else
                {
                    checkoutName = UserModel.getCurrentCheckoutName();
                }
                userName = UserModel.getAStringValueForAnyUser("SELECT " + LocalDatabase.CAMPO_NOMBRE_USER + " FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " +
                        LocalDatabase.CAMPO_ID_USUARIO + " = " + ClsRegeditController.getIdUserInTurn());
            });
            editNombreCaja.Text = checkoutName;
            editNombreCajero.Text = userName;
        }

        private async Task validateOptionsToServerModeLANOrWeb()
        {
            String description = "";
            await Task.Run(async () =>
            {
                if (serverModeLAN)
                {
                    comInstance = InstanceSQLSEModel.getStringComInstance();
                    panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    description = "Versión LAN " + MetodosGenerales.versionNumber;
                } else
                {
                    dynamic responseWeb = ConfiguracionModel.webActive();
                    if (responseWeb.value == 1)
                        webActive = responseWeb.active;
                    if (webActive)
                        description = "Versión Web (Online) " + MetodosGenerales.versionNumber;
                    else description = "Versión Web (Offline) " + MetodosGenerales.versionNumber;
                }
            });
            textVersionFrmPrincipal.Text = description;
        }

        private void FormPrincipal_Shown(object sender, EventArgs e)
        {
            opcionesToolStripMenuItem.Visible = true;
        }

        public async Task sendAllData(int methodSend, int sendData, int requests, String idDocumento)
        {
            bool serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
            SendDataService sds = new SendDataService();
            dynamic response = await sds.sendDataByMethods(methodSend, sendData, requests, idDocumento, serverModeLAN);
            await validateResponseSendData(response);
        }

        private async Task validateResponseSendData(dynamic response)
        {
            if (response != null)
            {
                String descripcion = response.description;
                int valor = response.value;
                methodSend = response.method;
                envioDeDatos = response.envioDeDatos;
                int peticiones = response.peticiones;
                String idDocumento = response.idDocumento;
                if (envioDeDatos == 1)
                {
                    //if (pbEnvDeDocs != null)
                    //pbEnvDeDocs.setProgress(valor);
                    if (descripcion.Equals("Tiempo Excedido"))
                    {
                        FormMessage formMessage = new FormMessage("Servisor No Encontrado", "Validar que el servidor se encuentre accesible", 2);
                        formMessage.ShowDialog();
                    }
                    else if (descripcion.Equals("Algo falló al intentar conectar con la IP del servidor"))
                    {
                        FormMessage formMessage = new FormMessage("Servisor No Encontrado", "Validar que el servidor se encuentre accesible", 2);
                        formMessage.ShowDialog();
                        /*PopupNotifier popup = new PopupNotifier();
                        popup.Image = ClsMetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                        popup.TitleColor = Color.Blue;
                        popup.TitleText = "Servisor No Encontrado";
                        popup.ContentText = "Validar que el servidor se encuentre accesible";
                        popup.ContentColor = Color.Red;
                        popup.Popup();*/
                    }
                    else
                    {
                        if (valor < 100 && methodSend < 7)
                        {
                            await sendAllData(methodSend, envioDeDatos, peticiones, idDocumento);
                        }/* else
                        {
                            methodSend = 1;
                        }*/
                    }
                }
                else
                {
                    if (descripcion.Equals("Tiempo Excedido"))
                    {

                    }
                    else if (descripcion.Equals("Algo falló al intentar conectar con la IP del servidor"))
                    {

                    }
                    else
                    {

                    }
                }
            }
        }

        private async Task<List<ClsPromocionesModel>> getAllPromotions()
        {
            List<ClsPromocionesModel> promosList = null;
            await Task.Run(async () =>
            {
                if (serverModeLAN)
                {
                    if (queryType == 0)
                    {
                        promosList = ClsPromocionesModel.getAllPromos(comInstance, lastId, LIMIT);
                        totalItems = ClsPromocionesModel.getTotalPromos(comInstance);
                    }
                } else
                {
                    if (queryType == 0)
                    {
                        query = "SELECT * FROM " + LocalDatabase.TABLA_PROMOCIONES +
                        " WHERE " + LocalDatabase.CAMPO_ID_PROMOTION + " > " + lastId +
                        " ORDER BY " + LocalDatabase.CAMPO_ID_PROMOTION + " LIMIT " + LIMIT;
                        queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_PROMOCIONES;
                        promosList = PromotionsModel.getAllPromotions(query);
                        totalItems = PromotionsModel.getTheNumberOfPromotions(queryTotals);
                    }
                }
            });
            return promosList;
        }

        private void hideScrollBars()
        {
            imgSinDatos.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.tpv_marcaagua, 260, 260);
            imgSinDatos.Visible = true;
            gridScrollBars = dataGridViewPromotionsFrmPincipal.ScrollBars;
            //dataGridItems.ScrollBars = ScrollBars.None;
        }

        private async Task fillDataGridPromotions()
        {
            hideScrollBars();
            lastLoading = DateTime.Now;
            promosListTemp = await getAllPromotions();
            if (promosListTemp != null)
            {
                progress += promosListTemp.Count;
                promosList.AddRange(promosListTemp);
                if (promosList.Count > 0 && dataGridViewPromotionsFrmPincipal.ColumnHeadersVisible == false)
                    dataGridViewPromotionsFrmPincipal.ColumnHeadersVisible = true;
                for (int i = 0; i < promosListTemp.Count; i++)
                {
                    int n = dataGridViewPromotionsFrmPincipal.Rows.Add();
                    dataGridViewPromotionsFrmPincipal.Rows[n].Cells[0].Value = promosListTemp[i].id + "";
                    dataGridViewPromotionsFrmPincipal.Columns[0].Visible = false;
                    dataGridViewPromotionsFrmPincipal.Rows[n].Cells[1].Value = promosListTemp[i].code;
                    dataGridViewPromotionsFrmPincipal.Columns[1].Visible = false;
                    dataGridViewPromotionsFrmPincipal.Rows[n].Cells[2].Value = promosListTemp[i].nombre;
                    dataGridViewPromotionsFrmPincipal.Columns[2].Width = 400;
                    dataGridViewPromotionsFrmPincipal.Rows[n].Cells[3].Value = promosListTemp[i].fechaFin;
                    dataGridViewPromotionsFrmPincipal.Columns[3].Width = 180;
                    dataGridViewPromotionsFrmPincipal.Rows[n].Cells[4].Value = promosListTemp[i].volumenMinimo;
                    dataGridViewPromotionsFrmPincipal.Rows[n].Cells[5].Value = promosListTemp[i].volumenMaximo;
                    dataGridViewPromotionsFrmPincipal.Rows[n].Cells[6].Value = promosListTemp[i].porcentajeDescuento+" %";
                    dataGridViewPromotionsFrmPincipal.Columns[6].Width = 120;
                }
                dataGridViewPromotionsFrmPincipal.PerformLayout();
                promosListTemp.Clear();
                lastId = Convert.ToInt32(promosList[promosList.Count - 1].id);
                imgSinDatos.Visible = false;
            }
            else
            {
                if (progress == 0)
                    imgSinDatos.Visible = true;
            }
            textPromotionsFrmPrincipal.Text = "Promociones: " + totalItems.ToString().Trim();
            //reset displayed row
            if (firstVisibleRow > -1)
            {
                showScrollBars();
                if (promosList.Count > 0)
                {
                    dataGridViewPromotionsFrmPincipal.FirstDisplayedScrollingRowIndex = firstVisibleRow;
                    imgSinDatos.Visible = false;
                }
            }
        }

        private void showScrollBars()
        {
            dataGridViewPromotionsFrmPincipal.ScrollBars = gridScrollBars;
        }

        private void dataGridViewPromotionsFrmPincipal_Scroll(object sender, ScrollEventArgs e)
        {
            if (promosList.Count < totalItems && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
                {
                    int cuntDisplayed = dataGridViewPromotionsFrmPincipal.Rows.Count - getDisplayedRowsCount();
                    if (e.NewValue >= cuntDisplayed)
                    {
                        //prevent loading from autoscroll.
                        TimeSpan ts = DateTime.Now - lastLoading;
                        if (ts.TotalMilliseconds > 100)
                        {
                            firstVisibleRow = e.NewValue;
                            //dataGridItems.PerformLayout();
                            fillDataGridPromotions();
                        }
                        else
                        {
                            dataGridViewPromotionsFrmPincipal.FirstDisplayedScrollingRowIndex = e.OldValue;
                        }
                    }
                }
            }
        }

        private int getDisplayedRowsCount()
        {
            int count = dataGridViewPromotionsFrmPincipal.Rows[dataGridViewPromotionsFrmPincipal.FirstDisplayedScrollingRowIndex].Height;
            count = dataGridViewPromotionsFrmPincipal.Height / count;
            return count;
        }

        public void resetearVariablesBusqueda(int queryType)
        {
            this.queryType = queryType;
            query = "";
            queryTotals = "";
            totalItems = 0;
            lastId = 0;
            progress = 0;
            if (promosList != null)
                promosList.Clear();
            dataGridViewPromotionsFrmPincipal.Rows.Clear();
        }

        private async Task verifyServerOrLocalLicense()
        {
            bool response = await validateLicense();
            if (response)
            {
                int licenseType = LicenseModel.getLicenseTypeInLocalDb();
                if (licenseType != 1 && licenseType != 2)
                {
                    bloquearEventos(false);
                    btnVenta.Visible = true;
                }
                if (licenseType == 2)
                    btnVenta.Visible = true;
                else if (licenseType == 1)
                    btnVenta.Visible = false;
            }
            else
            {
                bloquearEventos(false);
            }
        }

        private async Task<bool> validateLicense()
        {
            bool response = false;
            await Task.Run(async () =>
            {
                DateTime fechaActual = DateTime.Now;
                dynamic responseValicateLicense = await LicenseModel.validateLicense();
                if (responseValicateLicense.value > 0)
                {
                    idespecial = clsGeneral.idDesarrolloEspecial;
                    clsGeneral.fillRegeditAndFiles();
                    clsRegistro regeditValues = await LicenseModel.getRegeditValues();
                    if (regeditValues != null) {
                        String licenseEndDate = LicenseModel.getEndDateEncryptFromTheLocalDb();
                        await ClsLicenciamientoController.addDataInLicenseLic(clsGeneral.nombreSync, tipoSync,
                            licenseEndDate, Convert.ToInt32(regeditValues.DR));
                        response = true;
                    }
                } else
                {
                    dynamic responseLocalLic = await LicenseModel.validateLocalLicense();
                    if (responseLocalLic.value == 1)
                    {
                        idespecial = clsGeneral.idDesarrolloEspecial;
                        tipoSync = tipoSync;
                        response = true;
                    } else
                    {
                        FormMessage formMessage = new FormMessage("Licencia", responseLocalLic.description, 2);
                        formMessage.ShowDialog();
                        Environment.Exit(0);
                    }
                }
            });
            return response;
        }

        private void btnCargaInicial_Click(object sender, EventArgs e)
        {
            if (serverModeLAN)
            {
                FormMessage formMessage = new FormMessage("Modo LAN Activado", "Cuando usas el modo LAN todas las conexiones se realizan directamente al servidor," +
                    " por lo que ya no es necesario realizar carga inicial", 1);
                formMessage.ShowDialog();
            } else
            {
                if (webActive)
                {
                    frmValidacionCargaInicial Validacion = new frmValidacionCargaInicial();
                    Validacion.ShowDialog();
                    if (doInitialCharge)
                    {
                        banderaDescargandoDatos = true;
                        pbCargaInicialFrmPrincipal.Visible = true;
                        bloquearEventos(false);
                        downloadDataFromTheServer(1);
                    }
                } else
                {
                    FormMessage formMessage = new FormMessage("Carga Inicial", "No es posible realizar este proceso si " +
                        "la conexión Web está desactivada.\r\n Ir a Configuración y activar conexión Web!", 3);
                    formMessage.ShowDialog();
                }
            }
        }
        private async Task downloadDataFromTheServer(int downloadType)
        {
            textDownloadInfo.Text = "";
            textDownloadInfo.Visible = true;
            dynamic response = await ClsInitialChargeController.doDownloadProcess(pp, downloadType, codigoCaja);
            if (response.value < 0)
            {
                msj = new FormMessage("Conexión Incorrecta!", response.description, 3);
                msj.ShowDialog();
            }
            else
            {
                if (response.documentosPendientes > 0)
                {
                    msj = new FormMessage("Sincronizar Datos en el Servidor", "Hay documentos en el PanelROM pendientes de sincronizar al sistema COMERCIAL PREMIUM!", 2);
                    msj.ShowDialog();
                }
                else if (response.documentosPendientes < 0)
                {
                    msj = new FormMessage("Exception", response.description, 3);
                    msj.ShowDialog();
                }
            }
            bloquearEventos(true);
            banderaDescargandoDatos = false;
            pbCargaInicialFrmPrincipal.Value = 0;
            pbCargaInicialFrmPrincipal.Visible = false;
        }

        private static async Task<String> processResponse(int responseType, String methodName)
        {
            String response = "";
            if (responseType == 1)
            {
                response = "Genial datos descargados";
            }
            else if (responseType == -1)
            {
                response = "Oops la respuesta " + methodName + " no pudo ser procesada correctamente ";
            }
            else if (responseType == 404)
            {
                response = "Tiempo excedido no pudimos encontrar el servidor, verificar URL de recursos para actualizar " + methodName;
            }
            else if (responseType == 500)
            {
                response = "Algo falló en el servidor, validar recursos de " + methodName;
            }
            return response;
        }

        public async Task updateUI(int value, bool showMessage, String description, int messageType)
        {
            BeginInvoke((Action)(() =>
            {
                pbCargaInicialFrmPrincipal.Value = value;
                textDownloadInfo.Text = description;
                if (showMessage)
                {
                    if (value == 100)
                    {
                        textDownloadInfo.Text = "";
                        textDownloadInfo.Visible = false;
                    }
                    FormMessage formMessage = new FormMessage("Descargando Datos", description, messageType);
                    formMessage.ShowDialog();
                }
            }));
        }

        public async Task updateUICloseFat(int value, bool showMessage, String description)
        {
            await Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => {
                pbCargaInicialFrmPrincipal.Value = value;
                if (showMessage)
                {
                    PopupNotifier popup = new PopupNotifier();
                    popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                    popup.TitleColor = Color.Blue;
                    popup.TitleText = "Importante";
                    popup.ContentText = description;
                    popup.ContentColor = Color.Red;
                    popup.Popup();
                }
                if (frmAperturaTurno != null)
                {
                    frmAperturaTurno.Close();
                }
            }), DispatcherPriority.Background, null);
            //Thread.Sleep(200);
        }

        void bloquearEventos(bool bandera)
        {
            aperturaDeCajaToolStripMenuItem.Enabled = bandera;
            cargaInicialToolStripMenuItem.Enabled = bandera;
            actualizarDatosToolStripMenuItem.Enabled = bandera;
            sendDataToolStripMenuItem.Enabled = bandera;
            opcionesToolStripMenuItem.Enabled = bandera;
            descargasToolStripMenuItem.Enabled = bandera;
            //envia.Enabled = bandera;
            btnCliente.Enabled = bandera;
            btnArticulos.Enabled = bandera;
            //btnPedidos.Enabled = bandera;
            btnCorteDeCaja.Enabled = bandera;
            btnHistorialDocumentosFrmPrincipal.Enabled = bandera;
            btnReportsFrmPrincipal.Enabled = bandera;
            btnVenta.Enabled = bandera;
            btnTaras.Enabled = bandera;
            btnIngresos.Enabled = bandera;
            btnReimpresionTickets.Enabled = bandera;
        }

        private void btnArticulos_Click(object sender, EventArgs e)
        {
            //AbrirForm(new frmArticulos());
            FormArticulos articulos = new FormArticulos("", 0, cotmosActive);
            articulos.ShowDialog();
        }

        private void btnPedidos_Click(object sender, EventArgs e)
        {

        }

        private void btnVenta_Click(object sender, EventArgs e)
        {
            //if (Eselmismoagente())
            if(true)
            {
                formWaiting = new FormWaiting(this, 3, "Validando información...");
                formWaiting.ShowDialog();
            }
            else
            {
                FormMessage fm = new FormMessage("Advertencia", "El agente de venta cambio, realice la apertura de caja para poder vender.\n\rlas ventas seran eliminadas al realizar la apertrua de caja.", 3);
                fm.ShowDialog();
            }
        }

        public bool Eselmismoagente()
        {
            bool value=false;
            var db = new SQLiteConnection();
            try
            {
                String query = "SELECT * from Documentos where Documentos.USUARIO_ID != '" + ClsRegeditController.getIdUserInTurn() + "'";
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            value = false;
                        }
                        else
                        {
                            value = true;
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                SECUDOC.writeLog(Ex.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return value;
        }

        public async Task validateCotmosActivatedToVentaProcesssin()
        {
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                if (UserModel.doYouHavePermissionPrepedido())
                {
                    PrinterModel pm = PrinterModel.getallDataFromAPrinter();
                    if (pm != null)
                    {
                        value = 1;
                    }
                    else
                    {
                        description = "Asegurate de actualizar la información de la impresora en la " +
                            "Configuración";
                    }
                }
                else
                {
                    if (cotmosActive)
                    {
                        PrinterModel pm = PrinterModel.getallDataFromAPrinter();
                        if (pm != null)
                        {
                            value = 1;
                        }
                        else
                        {
                            description = "Asegurate de actualizar la información de la impresora en la " +
                        "Configuración";
                        }
                    } else
                    {
                        String rutaConec = "";
                        String query = "";
                        if (serverModeLAN)
                        {
                            rutaConec=ClsSQLiteDbHelper.instanceSQLite;
                            query = "SELECT * FROM " + RomDb.TABLA_APERTURATURNO + " WHERE " + RomDb.CAMPO_USERID_APERTURATURNO + " = @idUser";
                        }
                        else
                        {
                            rutaConec = panelInstance;
                            query = "SELECT * FROM " + LocalDatabase.TABLA_APERTURATURNO +
                            " WHERE " + LocalDatabase.CAMPO_USERID_APERTURATURNO + " = " + ClsRegeditController.getIdUserInTurn();
                        }
                        value = 1;
                        
                        
                        //bool ocupado = AperturaTurnoModel.getALastRecord(rutaConec, query);<- comparar el ulti
                        /*
                         string dateNow = DateTime.Now.ToString("yyyyMMdd");
                        if (serverModeLAN)
                        {
                            ClsAperturaCajaModel atm = ClsAperturaCajaModel.getARecordWithParameters(panelInstance, dateNow,
                                ClsRegeditController.getIdUserInTurn());
                            if (atm != null)
                            {
                                PrinterModel pm = PrinterModel.getallDataFromAPrinter();
                                if (pm != null)
                                {
                                    value = 1;
                                    //this.Visible = false;
                                    //FormVenta Venta = new FormVenta(0);
                                    //Venta.ShowDialog();
                                    //this.Visible = true;
                                }
                                            else
                                {
                                    description = "Asegurate de actualizar la información de la impresora en la " +
                                "Configuración";
                                }
                            }
                                        else
                            {
                                description = "Antes de iniciar el proceso de Ventas tienes que realizar Apertura de Caja!";
                            }
                        }
                                    else
                        {
                            String query = "SELECT * FROM " + LocalDatabase.TABLA_APERTURATURNO +
                            " WHERE " + LocalDatabase.CAMPO_CREATEDAT_APERTURATURNO + " = '" + dateNow + "' AND " +
                            LocalDatabase.CAMPO_USERID_APERTURATURNO + " = " + ClsRegeditController.getIdUserInTurn();
                            AperturaTurnoModel atm = AperturaTurnoModel.getARecord(query);
                            if (atm != null)
                            {
                                PrinterModel pm = PrinterModel.getallDataFromAPrinter();
                                if (pm != null)
                                {
                                    value = 1;
                                    //this.Visible = false;
                                    //FormVenta Venta = new FormVenta(0);
                                    //Venta.ShowDialog();
                                    //this.Visible = true;
                                }
                                else
                                {
                                    description = "Asegurate de actualizar la información de la impresora en la " +
                                "Configuración";
                                }
                            }
                            else
                            {
                                description = "Antes de iniciar el proceso de Ventas tienes que realizar Apertura de Caja!";
                            }
                        }
            */
                    }
                }
            });
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            if (value == 1)
            {
                this.Visible = false;
                FormVenta Venta = new FormVenta(0, cotmosActive);
                Venta.ShowDialog();
                this.Visible = true;
            } else
            {
                FormMessage formMessage = new FormMessage("Ventas", description, 3);
                formMessage.ShowDialog();
            }
        }

        public async Task validateCotmosActivatedToVentaProcess()
        {
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                if (UserModel.doYouHavePermissionPrepedido())
                {
                    PrinterModel pm = PrinterModel.getallDataFromAPrinter();
                    if (pm != null)
                    {
                        value = 1;
                    }
                    else
                    {
                        description = "Asegurate de actualizar la información de la impresora en la " +
                            "Configuración";
                    }
                }
                else
                {
                    if (cotmosActive)
                    {
                        PrinterModel pm = PrinterModel.getallDataFromAPrinter();
                        if (pm != null)
                        {
                            value = 1;
                        }
                        else
                        {
                            description = "Asegurate de actualizar la información de la impresora en la " +
                        "Configuración";
                        }
                    }
                    else
                    {
                        int idEspecial = LicenseModel.getIdEspeciualLocalDb();
                        if (1 == idEspecial)
                        {
                            if (Eselmismoagente())
                            {
                                value = 1;
                            }
                            else
                            {
                                description = "El agente de venta cambio, realice la apertura de caja para poder vender.\n\rlas ventas seran eliminadas al realizar la apertrua de caja.";

                            }
                        }
                        else if (2 == idEspecial)
                        {
                            value = 1;
                            msj = new FormMessage("Recordatorio", "Recuerde que es primordial el uso de la apertura de caja cada inicio de turno de venta para su optimo funcionamiento.", 1);
                            msj.ShowDialog();
                        }
                        else { 
                            string dateNow = DateTime.Now.ToString("yyyyMMdd");
                            if (serverModeLAN)
                            {
                                ClsAperturaCajaModel atm = ClsAperturaCajaModel.getARecordWithParameters(panelInstance, dateNow,
                                    ClsRegeditController.getIdUserInTurn());
                                if (atm != null)
                                {
                                    PrinterModel pm = PrinterModel.getallDataFromAPrinter();
                                    if (pm != null)
                                    {
                                        value = 1;
                                        /*this.Visible = false;
                                        FormVenta Venta = new FormVenta(0);
                                        Venta.ShowDialog();
                                        this.Visible = true;*/
                                    }
                                    else
                                    {
                                        description = "Asegurate de actualizar la información de la impresora en la " +
                                    "Configuración";
                                    }
                                }
                                else
                                {
                                    description = "Antes de iniciar el proceso de Ventas tienes que realizar Apertura de Caja!";
                                }
                            }
                            else
                            {
                                String query = "SELECT * FROM " + LocalDatabase.TABLA_APERTURATURNO +
                                " WHERE " + LocalDatabase.CAMPO_CREATEDAT_APERTURATURNO + " = '" + dateNow + "' AND " +
                                LocalDatabase.CAMPO_USERID_APERTURATURNO + " = " + ClsRegeditController.getIdUserInTurn();
                                AperturaTurnoModel atm = AperturaTurnoModel.getARecord(query);
                                if (atm != null)
                                {
                                    PrinterModel pm = PrinterModel.getallDataFromAPrinter();
                                    if (pm != null)
                                    {
                                        value = 1;
                                        /*this.Visible = false;
                                        FormVenta Venta = new FormVenta(0);
                                        Venta.ShowDialog();
                                        this.Visible = true;*/
                                    }
                                    else
                                    {
                                        description = "Asegurate de actualizar la información de la impresora en la " +
                                    "Configuración";
                                    }
                                }
                                else
                                {
                                    description = "Antes de iniciar el proceso de Ventas tienes que realizar Apertura de Caja!";
                                }
                            }
                        }
                    }
                }
            });
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            if (value == 1)
            {
                this.Visible = false;
                FormVenta Venta = new FormVenta(0, cotmosActive);
                Venta.ShowDialog();
                this.Visible = true;
            }
            else
            {
                FormMessage formMessage = new FormMessage("Ventas", description, 3);
                formMessage.ShowDialog();
            }
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            FormClientes clientes = new FormClientes();
            clientes.ShowDialog();
        }

        private void frmPrincipalPrueba_KeyUp(object sender, KeyEventArgs e)
        {
            if (!banderaDescargandoDatos)
            {
                if (e.KeyCode == Keys.F2)
                {
                    if (banderaDescargandoDatos == false)
                    {
                        formWaiting = new FormWaiting(this, 3, "Validando información...");
                        formWaiting.ShowDialog();
                    }
                } else if (e.KeyCode == Keys.F3)
                {
                    if (UserModel.doYouHavePermissionPrepedido())
                    {
                        FrmResumenDocuments frmResumenDoc = new FrmResumenDocuments();
                        frmResumenDoc.ShowDialog();
                    } else
                    {
                        FormPasswordConfirmation frmValidacionDocumentos = new FormPasswordConfirmation("Autorización del Supervisor", "Ingresar Contraseña del Supervisor");
                        frmValidacionDocumentos.ShowDialog();
                        if (FormPasswordConfirmation.permissionGranted)
                        {
                            FrmResumenDocuments frmResumenDoc = new FrmResumenDocuments();
                            frmResumenDoc.ShowDialog();
                        }
                    }
                } else if (e.KeyCode == Keys.F5)
                {
                    formWaiting = new FormWaiting(this, 4, "Validando información...");
                    formWaiting.ShowDialog();
                }
                else if (e.KeyCode == Keys.F6)
                {
                    if (UserModel.doYouHavePermissionPrepedido())
                    {
                        PrinterModel pm = PrinterModel.getallDataFromAPrinter();
                        if (pm != null)
                        {
                            FormGeneralsReports formGeneralsReports = new FormGeneralsReports("");
                            formGeneralsReports.StartPosition = FormStartPosition.CenterScreen;
                            formGeneralsReports.ShowDialog();
                        } else
                        {
                            FormMessage formMessage = new FormMessage("Impresora Faltante", "Asegurate de actualizar la información de la impresora en la " +
                        "Configuración", 3);
                            formMessage.ShowDialog();
                        }
                    }
                    else
                    {
                        formWaiting = new FormWaiting(this, 0, "Validando información, espera un momento...");
                        formWaiting.ShowDialog();
                    }
                }
            }
        }

        private void datosToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            datosToolStripMenuItem.ForeColor = Color.Black;
        }

        private void datosToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            datosToolStripMenuItem.ForeColor = Color.White;
        }

        private void opcionesToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            opcionesToolStripMenuItem.ForeColor = Color.White;
        }

        private void opcionesToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            opcionesToolStripMenuItem.ForeColor = Color.Black;
        }

        private void btnPromotionsPrincipal_Click(object sender, EventArgs e)
        {
            FrmPromotions fp = new FrmPromotions();
            fp.ShowDialog();
        }

        private void BtnLinks_Click(object sender, EventArgs e)
        {

        }

        private void ProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void frmPrincipalPrueba_SizeChanged(object sender, EventArgs e)
        {
            
        }

        private void btnCorte_Click(object sender, EventArgs e)
        {
            FormPasswordConfirmation fpc = new FormPasswordConfirmation("Autoriación del Supervisor", "Ingresar Contraseña del Supervisor");
            fpc.StartPosition = FormStartPosition.CenterScreen;
            fpc.ShowDialog();
            if (FormPasswordConfirmation.permissionGranted)
            {
                PrinterModel pm = PrinterModel.getallDataFromAPrinter();
                if (pm != null)
                {
                    FormGeneralsReports formGeneralsReports = new FormGeneralsReports("");
                    formGeneralsReports.StartPosition = FormStartPosition.CenterScreen;
                    formGeneralsReports.ShowDialog();
                } else
                {
                    FormMessage formMessage = new FormMessage("Impresora Faltante", "Asegurate de actualizar la información de la impresora en la " +
                        "Configuración", 3);
                    formMessage.ShowDialog();
                }
            }
        }

        private void btnHistorialDocumentosFrmPrincipal_Click(object sender, EventArgs e)
        {
            if (UserModel.doYouHavePermissionPrepedido())
            {
                FrmResumenDocuments frd = new FrmResumenDocuments();
                frd.ShowDialog();
            } else
            {
                FormPasswordConfirmation fpc = new FormPasswordConfirmation("Autorización del Supervisor", "Ingresar contraseña");
                fpc.StartPosition = FormStartPosition.CenterScreen;
                fpc.ShowDialog();
                if (FormPasswordConfirmation.permissionGranted)
                {
                    FrmResumenDocuments frd = new FrmResumenDocuments();
                    frd.ShowDialog();
                }
            }
        }

        private void textPromotionsFrmPrincipal_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewPromotionsFrmPincipal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void imgSinDatos_Click(object sender, EventArgs e)
        {

        }

        private void btnReportsFrmPrincipal_Click(object sender, EventArgs e)
        {
            if (UserModel.doYouHavePermissionPrepedido())
            {
                PrinterModel pm = PrinterModel.getallDataFromAPrinter();
                if (pm != null)
                {
                    FormGeneralsReports formGeneralsReports = new FormGeneralsReports("");
                    formGeneralsReports.StartPosition = FormStartPosition.CenterScreen;
                    formGeneralsReports.ShowDialog();
                } else
                {
                    FormMessage formMessage = new FormMessage("Impresora Faltante", "Asegurate de actualizar la información de la impresora en la " +
                        "Configuración", 3);
                    formMessage.ShowDialog();
                }
            } else
            {
                formWaiting = new FormWaiting(this, 0, "Validando información, espera un momento...");
                formWaiting.ShowDialog();
            }
        }

        public async Task validateProcessToDoReporteCorte()
        {
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                if (cotmosActive)
                {
                    value = 1;
                }
                else
                {
                    String pendiente = await armarStringParaMostrarInformacionAEnviar();
                    if (pendiente.Equals("")) { 
                        dynamic responseDocuments = null;
                        if (ConfiguracionModel.isLANPermissionActivated())
                            responseDocuments = await DocumentController.agentDocumentSynchronizedToCommercialLAN();
                        else responseDocuments = await DocumentController.agentDocumentSynchronizedToCommercialAPI();
                        if (responseDocuments.value == 0)
                        {
                            value = 1;
                        }
                        else if (responseDocuments.value > 0)
                        {
                            if (responseDocuments.value == 1)
                            {
                                description = "Tienes que sincronizar los documentos al sistema de Comercial en el Servidor!\n" +
                                " Se encontró " + responseDocuments.value + " Documento pendiente en el Panel";
                            }
                            else if (responseDocuments.value > 1)
                            {
                                description = "Tienes que sincronizar los documentos al sistema de Comercial en el Servidor!\n" +
                                " Se encontraron " + responseDocuments.value + " Documentos pendientes en el Panel";
                            }
                        }
                        else
                        {
                            value = responseDocuments.value;
                            description = responseDocuments.description;
                        }
                    }
                    else
                    {
                        value = 0;
                        description = "Tienes documentos sin enviar al servidor!.\n" +
                                ""+ pendiente;
                    }
                }
            });
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            if (value == 1)
            {
                FormPasswordConfirmation fpc = new FormPasswordConfirmation("Autorización del Supervisor", "Ingresar Contraseña");
                fpc.StartPosition = FormStartPosition.CenterScreen;
                fpc.ShowDialog();
                if (FormPasswordConfirmation.permissionGranted)
                {
                    PrinterModel pm = PrinterModel.getallDataFromAPrinter();
                    if (pm != null)
                    {
                        FormGeneralsReports formGeneralsReports = new FormGeneralsReports("");
                        formGeneralsReports.StartPosition = FormStartPosition.CenterScreen;
                        formGeneralsReports.ShowDialog();
                    }
                    else
                    {
                        FormMessage formMessage = new FormMessage("Impresora Faltante", "Asegurate de actualizar la información de la impresora en la " +
                        "Configuración", 3);
                        formMessage.ShowDialog();
                    }
                }
            } else
            {
                FormMessage fm = new FormMessage("Reportes", description, 3);
                fm.ShowDialog();
            }
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            if (MetodosGenerales.verifyIfInternetIsAvailable())
            {
                FrmSendData fc = new FrmSendData("Enviar Datos", "Este proceso enviará todos los datos almacenados localmente", 1);
                fc.StartPosition = FormStartPosition.CenterScreen;
                fc.ShowDialog();
            }
            else
            {
                FormMessage message = new FormMessage("Datos Incorrectos", "Oops! Tienes que conectarte a Internet", 2);
                message.ShowDialog();
            }
        }

        private void btnAcercade_Click(object sender, EventArgs e)
        {
            FrmAcercaDe acerca = new FrmAcercaDe();
            acerca.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            applicationExist = true;
            Environment.Exit(0);
        }

        private async void BtnConfiguracion_Click(object sender, EventArgs e)
        {
            FormConfiguracionGral Configuracion = new FormConfiguracionGral();
            Configuracion.ShowDialog();
            await validateOptionsToServerModeLANOrWeb();
        }

        private void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            applicationExist = false;
            formWaiting = new FormWaiting(this, 1, "Cerrando sesión, espera un momento por favor...");
            formWaiting.ShowDialog();
        }

        public async Task processToDoLogout()
        {
            //this.Visible = false;
            loggingOut = true;
            dynamic response = null;
            bool serverLAN = ConfiguracionModel.isLANPermissionActivated();
            if (serverLAN)
                response = await UsersController.userLogoutServerLAN(ClsRegeditController.getIdUserInTurn());
            else response = await UsersController.userLogoutServerAPI(ClsRegeditController.getIdUserInTurn());
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            ClsRegeditController.saveIdUserInTurn(0);
            ClsRegeditController.saveLoginStatus(false);
            ClsRegeditController.saveRememberLogin(false);
            ClsRegeditController.saveCurrentIdEnterprise(0);
            this.Close();
        }

        private void btnCorteDeCaja_Click(object sender, EventArgs e)
        {
            formWaiting = new FormWaiting(this, 4, "Validando información...");
            formWaiting.ShowDialog();
        }

        public async Task validateCotmosActivatedForWithdrawalsProcess()
        {
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                if (cotmosActive)
                {
                    value = 1;
                } else
                {
                    String rutaConec = "";
                    String query = "";
                    if (serverModeLAN)
                    {
                        rutaConec = ClsSQLiteDbHelper.instanceSQLite;
                        query = "SELECT * FROM " + RomDb.TABLA_APERTURATURNO + " WHERE " + RomDb.CAMPO_USERID_APERTURATURNO + " = @idUser";
                    }
                    else
                    {
                        rutaConec = panelInstance;
                        query = "SELECT * FROM " + LocalDatabase.TABLA_APERTURATURNO +
                        " WHERE " + LocalDatabase.CAMPO_USERID_APERTURATURNO + " = " + ClsRegeditController.getIdUserInTurn();
                    }
                    value = 1;
                    /*string dateNow = DateTime.Now.ToString("yyyyMMdd");
                    if (serverModeLAN)
                    {
                        ClsAperturaCajaModel atm = ClsAperturaCajaModel.getARecordWithParameters(panelInstance, dateNow,
                            ClsRegeditController.getIdUserInTurn());
                        if (atm != null)
                        {
                            value = 1;
                        }
                        else
                        {
                            description = "Antes de iniciar el proceso de Ventas tienes que realizar Apertura de Caja!";
                        }
                    }
                    else
                    {
                        String query = "SELECT * FROM " + LocalDatabase.TABLA_APERTURATURNO +
                        " WHERE " + LocalDatabase.CAMPO_CREATEDAT_APERTURATURNO + " = '" + dateNow + "' AND " + LocalDatabase.CAMPO_USERID_APERTURATURNO + " = " +
                        ClsRegeditController.getIdUserInTurn();
                        AperturaTurnoModel atm = AperturaTurnoModel.getARecord(query);
                        if (atm != null)
                        {
                            value = 1;
                        }
                        else
                        {
                            description = "Antes de iniciar el proceso de Ventas tienes que realizar Apertura de Caja!";
                        }
                    }*/
                }
            });
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            if (value == 1)
            {
                FormPasswordConfirmation fpc = new FormPasswordConfirmation("Autorización del Supervisor", "");
                fpc.StartPosition = FormStartPosition.CenterScreen;
                fpc.ShowDialog();
                if (FormPasswordConfirmation.permissionGranted)
                {
                    PrinterModel pm = PrinterModel.getallDataFromAPrinter();
                    if (pm != null)
                    {
                        FormCorteCaja frc = new FormCorteCaja();
                        frc.ShowDialog();
                    }
                    else
                    {
                        FormMessage formMessage = new FormMessage("Impresora Faltante", "Asegurate de actualizar la información de la impresora en la " +
                    "Configuración", 3);
                        formMessage.ShowDialog();
                    }
                }
            } else
            {
                FormMessage formMessage = new FormMessage("Retiros y Cortes de Caja", description, 3);
                formMessage.ShowDialog();
            }
        }

        private void frmPrincipalPrueba_Leave(object sender, EventArgs e)
        {

        }

        private void actualizarDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (serverModeLAN)
            {
                FormMessage formMessage = new FormMessage("Modo LAN Activado", "Cuando utilizas el modo LAN todas las conexiones se realizan directamente al " +
                    "servidor, por lo que la actualización de datos no es necesaria",1);
                formMessage.ShowDialog();
            } else
            {
                if (webActive)
                {
                    pbCargaInicialFrmPrincipal.Visible = true;
                    bloquearEventos(false);
                    //progressBar1.Value = 0;
                    //Bandera = 1;
                    //backgroundCargaInicial.RunWorkerAsync();
                    downloadDataFromTheServer(2);
                } else
                {
                    FormMessage formMessage = new FormMessage("Actualización de Datos", "No es posible realizar este proceso si " +
                        "la conexión Web está desactivada.\r\n Ir a Configuración y activar conexión Web!", 3);
                    formMessage.ShowDialog();
                }
            }
        }

        private async void descargasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDownloads fd = new FrmDownloads(cotmosActive);
            fd.StartPosition = FormStartPosition.CenterScreen;
            fd.ShowDialog();
            await validarPermisoPrepedido();
        }

        private void aperturaDeCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //aperturaDeCaja();
            if (!serverModeLAN && !webActive)
            {
                FormMessage formMessage = new FormMessage("Web Desactivado", "Para realizar la apertura de caja tienes que " +
                    "tener la conexión web activa para poder validar la información del servidor!", 3);
                formMessage.ShowDialog();
            }
            formWaiting = new FormWaiting(this, 2, "Validando apertura de turno...");
            formWaiting.ShowDialog();
            validarUltimaApertura();
        }

        public async Task aperturaDeCajaProcess()
        {
            int value = 0;
            String description = "";
            await Task.Run(async () => {
                String pendiente = await armarStringParaMostrarInformacionAEnviar();
                if (pendiente.Equals(""))
                {
                    string dateNow = DateTime.Now.ToString("yyyyMMdd");
                    if (serverModeLAN)
                    {
                        ClsAperturaCajaModel atm = ClsAperturaCajaModel.getARecordWithParameters(panelInstance, dateNow, ClsRegeditController.getIdUserInTurn());
                        if (atm != null)
                        {
                            description = "La apertura de turno para este usuario ya fue realizada anteriormente!";
                        }
                        else
                        {
                            dynamic responseDocuments = await DocumentController.agentDocumentSynchronizedToCommercialLAN();
                            if (responseDocuments.value == 0)
                            {
                                value = 1;
                                /*frmAperturaTurno = new FrmAperturaTurno(this);
                                frmAperturaTurno.StartPosition = FormStartPosition.CenterScreen;
                                frmAperturaTurno.ShowDialog();*/
                            }
                            else if (responseDocuments.value > 0)
                            {
                                if (responseDocuments.value == 1)
                                {
                                    description = "Tienes que sincronizar los documentos " +
                                        "al sistema de Comercial en el Servidor! " +
                                        "Sen encontro 1 Documento pendiente en el Panel";
                                }
                                else
                                {
                                    description = "Tienes que sincronizar los " +
                                        "documentos al sistema de Comercial en el Servidor! " +
                                        "Se encontraron " + responseDocuments.value + " Documentos pendientes en el Panel";
                                }
                            }
                            else
                            {
                                description = responseDocuments.description;
                            }
                        }
                    }
                    else
                    {
                        if (webActive)
                        {
                            AperturaTurnoModel atm = AperturaTurnoModel.getARecordWithParameters(dateNow,
                            ClsRegeditController.getIdUserInTurn());
                            if (atm != null)
                            {
                                description = "La apertura de turno para este usuario ya fue realizada anteriormente!";
                            }
                            else
                            {
                                dynamic responseDocuments = await DocumentController.agentDocumentSynchronizedToCommercialAPI();
                                if (responseDocuments.value == 0)
                                {
                                    value = 1;
                                }
                                else if (responseDocuments.value > 0)
                                {
                                    if (responseDocuments.value == 1)
                                    {
                                        description = "Tienes que sincronizar los documentos al sistema de Comercial en " +
                                        "el Servidor! " +
                                            "Sen encontro 1 Documento pendiente en el Panel";
                                    }
                                    else
                                    {
                                        description = "Tienes que sincronizar los documentos al sistema de Comercial " +
                                        "en el Servidor! " +
                                            "Se encontraron " + responseDocuments.value + " Documentos pendientes en el Panel";
                                    }
                                }
                                else
                                {
                                    value = responseDocuments.value;
                                    description = responseDocuments.description;
                                }
                            }
                        } else
                        {
                            AperturaTurnoModel atm = AperturaTurnoModel.getARecordWithParameters(dateNow,
                            ClsRegeditController.getIdUserInTurn());
                            if (atm != null)
                            {
                                description = "La apertura de turno para este usuario ya fue realizada anteriormente!";
                            }
                            else
                            {
                                dynamic responseDocuments = await DocumentController.agentDocumentSynchronizedToCommercialAPI();
                                if (responseDocuments.value == 0)
                                {
                                    value = 1;
                                }
                                else if (responseDocuments.value > 0)
                                {
                                    if (responseDocuments.value == 1)
                                    {
                                        description = "Tienes que sincronizar los documentos al sistema de Comercial en " +
                                        "el Servidor! " +
                                            "Sen encontro 1 Documento pendiente en el Panel";
                                    }
                                    else
                                    {
                                        description = "Tienes que sincronizar los documentos al sistema de Comercial " +
                                        "en el Servidor! " +
                                            "Se encontraron " + responseDocuments.value + " Documentos pendientes en el Panel";
                                    }
                                }
                                else
                                {
                                    value = responseDocuments.value;
                                    description = responseDocuments.description;
                                }
                            }
                        }
                    }
                }
                else
                {
                    description = "Hay datos pendientes de enviar o pausados!";
                }
            });
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            if (value == 1)
            {
                frmAperturaTurno = new FrmAperturaTurno(this, cotmosActive);
                frmAperturaTurno.StartPosition = FormStartPosition.CenterScreen;
                frmAperturaTurno.ShowDialog();
            } else
            {
                FormMessage fm = new FormMessage("Validación", description, 2);
                fm.ShowDialog();
            }
        }

        private async Task<String> armarStringParaMostrarInformacionAEnviar()
        {
            String reponse = "";
            await Task.Run(async () => {
                int cadcNotSent = CustomerADCModel.getTheTotalNumberOfAdditionalCustomersNotSent();
                if (cadcNotSent > 0)
                {
                    if (cadcNotSent == 1)
                    {
                        reponse += cadcNotSent + " Cliente Nuevo\n";
                    }
                    else
                    {
                        reponse += cadcNotSent + " Clientes Nuevos\n";
                    }
                }
                int docsNotSent = DocumentModel.getTotalNumberOfDocuments("SELECT COUNT(*) FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                    LocalDatabase.CAMPO_ENVIADOALWS_DOC + " = " + 0);
                if (docsNotSent > 0)
                {
                    if (docsNotSent == 1)
                    {
                        reponse += docsNotSent + " Documento\n";
                    }
                    else
                    {
                        reponse += docsNotSent + " Documentos\n";
                    }
                }
                int locationsNotSent = ClsPositionsModel.getTheTotalNumberOfUnsentLocations();
                if (locationsNotSent > 0)
                {
                    if (locationsNotSent == 1)
                    {
                        reponse += locationsNotSent + " Ubicación\n";
                    }
                    else
                    {
                        reponse += locationsNotSent + " Ubicaciones\n";
                    }
                }
                int cxcNotSent = CuentasXCobrarModel.getTheTotalNumberOfCreditsNotSent();
                if (cxcNotSent > 0)
                {
                    if (cxcNotSent == 1)
                    {
                        reponse += cxcNotSent + " Pago";
                    }
                    else
                    {
                        reponse += cxcNotSent + " Pagos";
                    }
                }
                int retirosNotSent = RetiroModel.getTheTotalNumberOfWithdrawalsNotSentToTheServer();
                if (retirosNotSent > 0)
                {
                    if (retirosNotSent == 1)
                    {
                        reponse += retirosNotSent + " Corte de caja";
                    }
                    else
                    {
                        reponse += retirosNotSent + " Cortes de caja";
                    }
                }
                int ticketsNotSent = DatosTicketModel.getTheTotalNumberOfTicketsNotSentToTheServer();
                if (ticketsNotSent > 0)
                {
                    if (ticketsNotSent == 1)
                    {
                        reponse += ticketsNotSent + " tickets";
                    }
                    else
                    {
                        reponse += ticketsNotSent + " tickets";
                    }
                }
            });
            return reponse;
        }

        private void btnTaras_Click(object sender, EventArgs e)
        {
            FrmTaras frmTaras = new FrmTaras();
            frmTaras.StartPosition = FormStartPosition.CenterScreen;
            frmTaras.ShowDialog();
        }

        private void btnResumen_Click(object sender, EventArgs e)
        {
            FormPasswordConfirmation frmValidacionDocumentos = new FormPasswordConfirmation("Autorización del Supervisor", "");
            frmValidacionDocumentos.ShowDialog();
            if (FormPasswordConfirmation.permissionGranted)
            {
                FrmResumenDocuments Resumen = new FrmResumenDocuments();
                Resumen.ShowDialog();
            }

        }

        private void textBox1_MouseHover(object sender, EventArgs e)
        {
            if (atm != null)
            {
                toolTip1.SetToolTip(editBoxUltimaApertura, "Agente:"+atm.userId.ToString() + " inicio con $" + atm.importe.ToString()+" en caja");
            }
            else
            {
                toolTip1.SetToolTip(editBoxUltimaApertura, "No aplica");
            }
        }

        private void btnReimpresionTickets_Click(object sender, EventArgs e)
        {
            FormPasswordConfirmation fpc = new FormPasswordConfirmation("Autorización del Supervisor", "Ingresar Contraseña");
            fpc.StartPosition = FormStartPosition.CenterScreen;
            fpc.ShowDialog();
            if (FormPasswordConfirmation.permissionGranted)
            {
                PrinterModel pm = PrinterModel.getallDataFromAPrinter();
                if (pm != null)
                {

                    FrmReimpresionTickets frmTickets = new FrmReimpresionTickets();
                    frmTickets.ShowDialog();
                }
                else
                {
                    String description = "Asegurate de actualizar la información de la impresora en la " +
                        "Configuración";
                    MessageBox.Show(description);

                }
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            applicationExist = true;
            this.Close();
        }

        private void btnTestLan_Click(object sender, EventArgs e)
        {
            getConnectionLAN();
        }

        private void menuStripFrmPrincipal_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void frmPrincipalPrueba_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (applicationExist)
                Environment.Exit(0);
            else
            {
                if (loggingOut)
                {
                    this.Dispose();
                    FormIniciarSesion Login = new FormIniciarSesion();
                    Login.ShowDialog();
                    Login.Focus();
                }
                else
                {
                    Environment.Exit(0);
                }
            }
        }

        private async Task getConnectionLAN()
        {
            dynamic response = await UsersController.userLoginServerLAN("192.168.50.136", "1433");
            if (response.error >= 1)
            {
                FormMessage fm = new FormMessage("Test", "Local: " + response.errorMessage + " - " + response.error, 1);
                fm.ShowDialog();
            } else
            {
                FormMessage fm = new FormMessage("Test", "Local: " + response.errorMessage, 2);
                fm.ShowDialog();
            }
        }

        private void btnIngresos_Click(object sender, EventArgs e)
        {
            formWaiting = new FormWaiting(this, 5, "Validando Información...");
            formWaiting.ShowDialog();
        }

        public async Task validateCotmosActivatedToIngresosProcess()
        {
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                if (cotmosActive)
                {
                    value = 1;
                } else
                {
                    String rutaConec = "";
                    String query = "";
                    if (serverModeLAN)
                    {
                        rutaConec = ClsSQLiteDbHelper.instanceSQLite;
                        query = "SELECT * FROM " + RomDb.TABLA_APERTURATURNO + " WHERE " + RomDb.CAMPO_USERID_APERTURATURNO + " = @idUser";
                    }
                    else
                    {
                        rutaConec = panelInstance;
                        query = "SELECT * FROM " + LocalDatabase.TABLA_APERTURATURNO +
                        " WHERE " + LocalDatabase.CAMPO_USERID_APERTURATURNO + " = " + ClsRegeditController.getIdUserInTurn();
                    }
                    value = 1;
                    /*string dateNow = DateTime.Now.ToString("yyyyMMdd");
                    if (serverModeLAN)
                    {
                        ClsAperturaCajaModel atm = ClsAperturaCajaModel.getARecordWithParameters(panelInstance, dateNow,
                            ClsRegeditController.getIdUserInTurn());
                        if (atm != null)
                        {
                            value = 1;
                        }
                        else
                        {
                            description = "Antes de iniciar el proceso de Ventas tienes que realizar Apertura de Caja!";
                        }
                    }
                    else
                    {
                        String query = "SELECT * FROM " + LocalDatabase.TABLA_APERTURATURNO +
                        " WHERE " + LocalDatabase.CAMPO_CREATEDAT_APERTURATURNO + " = '" + dateNow + "' AND " +
                        LocalDatabase.CAMPO_USERID_APERTURATURNO + " = " + ClsRegeditController.getIdUserInTurn();
                        AperturaTurnoModel atm = AperturaTurnoModel.getARecord(query);
                        if (atm != null)
                        {
                            value = 1;
                        }
                        else
                        {
                            description = "Antes de iniciar el proceso de Ventas tienes que realizar Apertura de Caja!";
                        }
                    }*/
                }
            });
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            if (value == 1)
            {
                PrinterModel pm = PrinterModel.getallDataFromAPrinter();
                if (pm != null)
                {
                    FormIngresos formIngresos = new FormIngresos();
                    formIngresos.StartPosition = FormStartPosition.CenterScreen;
                    formIngresos.ShowDialog();
                }
                else
                {
                    FormMessage formMessage = new FormMessage("Impresora Faltante", "Asegurate de actualizar la información de la impresora en la " +
                    "Configuración", 3);
                    formMessage.ShowDialog();
                }
            } else
            {
                FormMessage formMessage = new FormMessage("Ingresos", description, 3);
                formMessage.ShowDialog();
            }
        }

        private void frmPrincipalPrueba_FormClosing(object sender, FormClosingEventArgs e)
        {
            validateIfUserRemeberLogin();
        }

        private async Task validateIfUserRemeberLogin()
        {
            if (!ClsRegeditController.getRememberLogin())
            {
                dynamic response = null;
                bool serverLAN = ConfiguracionModel.isLANPermissionActivated();
                if (serverLAN)
                    response = await UsersController.userLogoutServerLAN(ClsRegeditController.getIdUserInTurn());
                else response = await UsersController.userLogoutServerAPI(ClsRegeditController.getIdUserInTurn());
            }
            else
            {
                dynamic response = null;
                bool serverLAN = ConfiguracionModel.isLANPermissionActivated();
                if (serverLAN)
                    response = await UsersController.updateLastSeenLAN(ClsRegeditController.getIdUserInTurn());
                else response = await UsersController.updateLastSeenAPI(ClsRegeditController.getIdUserInTurn());
            }
        }

        private void btnSoporte_Click(object sender, EventArgs e)
        {
            //AbrirForm(new FrmSoporte());
            FrmSoporte soporte = new FrmSoporte();
            soporte.ShowDialog();
        }

        public void AbrirForm(object frmHijo)
        {
            //if (this.pnlContent.Controls.Count > 0)
            //this.pnlContent.Controls.RemoveAt(0);


            Form fd = frmHijo as Form;
            fd.TopLevel = false;
            fd.Dock = DockStyle.Fill;
            //this.pnlContent.Controls.Add(fd);
            //this.pnlContent.Tag = fd;
            fd.Show();
        }
    }
    public static class ModifyProgressBarColor
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        public static void SetState(this ProgressBar pBar, int state)
        {
            SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
        }
    }
    public class RoundButton : Button
    {
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            GraphicsPath graphs = new GraphicsPath();
            graphs.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            this.Region = new System.Drawing.Region(graphs);
            base.OnPaint(e);
        }
    }
}

