using SyncTPV.Controllers;
using SyncTPV.Controllers.Downloads;
using SyncTPV.Models;
using SyncTPV.Views.Customers;
using SyncTPV.Views.Extras;
using SyncTPV.Views.Mostrador;
using SyncTPV.Views.Reports;
using SyncTPV.Views.Rutas;
using SyncTPV.Views.Scale;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using wsROMClase;

namespace SyncTPV.Views
{
    public partial class FormWaiting : Form
    {
        public static readonly int CALL_REPORTES = 2;
        public static readonly int CALL_COTIZACIONESMOSTRADOR = 0;
        public static readonly int CALL_PREPEDIDOS = 1;
        public static readonly int CALL_RUTAS = 2;
        public static readonly int CALL_UPDATE_CXC_FROM_A_CUSTOMER = 4;
        public static readonly int CALL_DOWNLOAD_DIRECTORIOSERVER = 1;
        int call = 0;
        private FormGeneralsReports formGeneralsReports;
        int idRep = 0;
        private FrmCobranzaCxc frmCobranzaCxc;
        FrmDownloads frmDownloads;
        private int positionTableDownloads = 0;
        private FormArticulos formArticulos;
        private FormPayCart formPayCart;
        private FrmSendData frmSendData;
        private FrmSearchRoutesOrCheckouts frmSearchRoutesOrCheckouts;
        private FormCalculateWeight formCalculateWeight;
        private FormClosingEventArgs eventoCerrarVentana;
        private FormAddField formAddField;
        private FormFacturar formFacturar;
        private FormClientes frmClientes;
        private FormPrincipal formPrincipal;
        private FrmResumenDocuments frmResumenDocuments;
        private FormParametersReports formParametersReports;
        private FormConfiguracionGral formConfiguracionGral;
        private FormAddCustomer formAddCustomer;
        private FormVenta frmVentaNew;
        private FrmPedidosCotizacionesSurtir frmPedidosCotizacionesSurtir;
        private FrmActivacion frmActivacion;
        private FormConfiguracionWS formConfiguracionWS;
        private FormSeleccionarCaja formSeleccionarCaja;
        private FormCodigoAgente formCodigoAgente;
        private int idCotizacionPrepedido = 0;
        private String searchWordPrepedidos = "";
        private int dgvRowIndexMovement = 0;
        private bool getRoutes = false, updateCustomer = false, updateUsoCFDI = false, updateRegimeFiscal = false;
        private bool webActive = false;
        private String codigoCaja = "";

        public FormWaiting(FrmActivacion frmActivacion, int call)
        {
            InitializeComponent();
            this.call = call;
            this.frmActivacion = frmActivacion;
        }

        public FormWaiting(FormConfiguracionWS formConfiguracionWS, int call, String message)
        {
            InitializeComponent();
            this.call = call;
            this.formConfiguracionWS = formConfiguracionWS;
            textMessage.Text = message;
        }

        public FormWaiting(FrmCobranzaCxc frmCobranzaCxc, int call, int idRep)
        {
            InitializeComponent();
            this.call = call;
            this.frmCobranzaCxc = frmCobranzaCxc;
            this.idRep = idRep;
        }

        public FormWaiting(FormVenta frmVentaNew, int call, int dgvRowIndexMovement)
        {
            InitializeComponent();
            this.call = call;
            this.frmVentaNew = frmVentaNew;
            this.dgvRowIndexMovement = dgvRowIndexMovement;
        }

        public FormWaiting(FormGeneralsReports formGeneralsReports, int call, String searchWordPrepedidos)
        {
            InitializeComponent();
            this.call = call;
            this.formGeneralsReports = formGeneralsReports;
            this.searchWordPrepedidos = searchWordPrepedidos;
        }

        public FormWaiting(int call, int positionTableDownloads, FrmDownloads frmDownloads, String codigoCaja)
        {
            InitializeComponent();
            this.call = call;
            this.frmDownloads = frmDownloads;
            this.positionTableDownloads = positionTableDownloads;
            this.codigoCaja = codigoCaja;
        }

        public FormWaiting(FrmPedidosCotizacionesSurtir frmPedidosCotizacionesSurtir, int call, int idCotizacionPrepedido, bool getRoutes, bool webActive,
            String codigoCaja)
        {
            InitializeComponent();
            this.call = call;
            this.frmPedidosCotizacionesSurtir = frmPedidosCotizacionesSurtir;
            this.idCotizacionPrepedido = idCotizacionPrepedido;
            this.getRoutes = getRoutes;
            this.webActive = webActive;
            this.codigoCaja = codigoCaja;
        }

        public FormWaiting(FormArticulos formArticulos, int call, String message)
        {
            InitializeComponent();
            this.call = call;
            this.formArticulos = formArticulos;
            textMessage.Text = message;
        }

        public FormWaiting(FormPayCart formPayCart, int call)
        {
            InitializeComponent();
            this.formPayCart = formPayCart;
            this.call = call;
        }

        public FormWaiting(int call, FrmSendData frmSendData)
        {
            InitializeComponent();
            this.call = call;
            this.frmSendData = frmSendData;
        }

        public FormWaiting(int call, FrmSearchRoutesOrCheckouts frmSearchRoutesOrCheckouts)
        {
            InitializeComponent();
            this.call = call;
            this.frmSearchRoutesOrCheckouts = frmSearchRoutesOrCheckouts;
        }

        public FormWaiting(int call, FormCalculateWeight formCalculateWeight, FormClosingEventArgs e)
        {
            InitializeComponent();
            this.call = call;
            this.formCalculateWeight = formCalculateWeight;
            this.eventoCerrarVentana = e;
        }

        public FormWaiting(FormCalculateWeight formCalculateWeight, int call)
        {
            InitializeComponent();
            this.formCalculateWeight = formCalculateWeight;
            this.call = call;
        }

        public FormWaiting(FormAddField formAddField, int call)
        {
            InitializeComponent();
            this.formAddField = formAddField;
            this.call = call;
        }

        public FormWaiting(FormFacturar formFacturar, int call, int type)
        {
            InitializeComponent();
            this.formFacturar = formFacturar;
            this.call = call;
            if (call == CALL_DOWNLOAD_DIRECTORIOSERVER)
            {
                formFacturar.processToDownloadRouteInvoice();
            } else if (call == 2)
            {
                if (type == 0)
                {
                    formFacturar.getInvoiceFiles("pdf");
                } else
                {
                    formFacturar.getInvoiceFiles("xml");
                }
            } else if (call == 3)
            {
                formFacturar.processtoValidatePrintInvoice();
            }
        }

        public FormWaiting(FormClientes frmClientes, int call)
        {
            InitializeComponent();
            this.frmClientes = frmClientes;
            this.call = call;
        }

        public FormWaiting(FormPrincipal formPrincipal, int call, String msg)
        {
            InitializeComponent();
            this.formPrincipal = formPrincipal;
            this.call = call;
            this.textMessage.Text = msg;
        }

        public FormWaiting(FrmResumenDocuments frmResumenDocuments, int call)
        {
            InitializeComponent();
            this.frmResumenDocuments = frmResumenDocuments;
            this.call = call;
        }

        public FormWaiting(FormParametersReports formParametersReports, int call)
        {
            InitializeComponent();
            this.formParametersReports = formParametersReports;
            this.call = call;
        }

        public FormWaiting(FormAddCustomer formAddCustomer, int call, String title, bool updateCustomer)
        {
            InitializeComponent();
            this.formAddCustomer = formAddCustomer;
            this.call = call;
            textMessage.Text = "Espera un momento, por favor\r\n" + title;
            this.updateRegimeFiscal = updateCustomer;
        }

        public FormWaiting(FormConfiguracionGral formConfiguracionGral, int call, String message)
        {
            InitializeComponent();
            textMessage.Text = "Espera un momento, por favor...\r\n" + message;
            this.formConfiguracionGral = formConfiguracionGral;
            this.call = call;
            this.updateRegimeFiscal = updateCustomer;
        }

        public FormWaiting(FormSeleccionarCaja formSeleccionarCaja, int call, String message)
        {
            InitializeComponent();
            this.formSeleccionarCaja = formSeleccionarCaja;
            this.call = call;
            textMessage.Text = message;
        }

        public FormWaiting(FormCodigoAgente formCodigoAgente, int call, String message)
        {
            InitializeComponent();
            this.formCodigoAgente = formCodigoAgente;
            this.call = call;
            textMessage.Text = message;
        }

        private async void FrmWaiting_Load(object sender, EventArgs e)
        {
            progressBarFrmWaiting.Value = 50;
            if (frmCobranzaCxc != null) {
                if (call == 1)
                {
                    frmCobranzaCxc.validateSendResp(idRep);
                } else if (call == 0)
                {
                    await frmCobranzaCxc.downloadCxcForThisCustomer();
                }
            } 
            else if (frmVentaNew != null)
            {
                if (call == 0)
                {
                    frmVentaNew.changeDocumentType();
                    //validateDocumentTypeChange(frmVentaNew.idDocument, frmVentaNew.documentType, frmVentaNew.itemModel);
                } else if (call == 1)
                {
                    await frmVentaNew.processToDeleteMovement(dgvRowIndexMovement);
                }
            } 
            else if (formGeneralsReports != null)
            {
                validateProcessReports();
            }
            else if (frmDownloads != null)
            {
                if (call == 0)
                {
                    processDownloadData(positionTableDownloads, codigoCaja);
                }
            } else if (frmPedidosCotizacionesSurtir != null)
            {
                if (call == CALL_COTIZACIONESMOSTRADOR)
                {
                    await frmPedidosCotizacionesSurtir.processToUpdateCotMos();
                } else if (call == CALL_PREPEDIDOS)
                {
                    await frmPedidosCotizacionesSurtir.processToUpdatePrePedidos();
                } else if (call == CALL_RUTAS)
                {
                    await frmPedidosCotizacionesSurtir.doDownloadRoutesOrCheckoutsProcess(getRoutes);
                } else if (call == 3)
                {
                    await frmPedidosCotizacionesSurtir.saveDocument(idCotizacionPrepedido, webActive, codigoCaja);
                } else if (call == 4)
                {
                    await frmPedidosCotizacionesSurtir.saveDocumentPrePedido(idCotizacionPrepedido, webActive, codigoCaja);
                }
            } else if (formArticulos != null)
            {
                if (call == 0)
                    formArticulos.proccesToGetNewItems();
                else if (call == 1)
                    await formArticulos.getAllInitialDataProcess();
                else if (call == 2)
                    await formArticulos.validateDownloadUnitsOfMeasureAndWeightProcess();
            } else if (formPayCart != null)
            {
                if (call == 0)
                    await formPayCart.callTerminateDocumentTask();
            } else if (frmSendData != null)
            {
                await frmSendData.sendData();
            } else if (frmSearchRoutesOrCheckouts != null)
            {
                frmSearchRoutesOrCheckouts.doDownloadRoutesProcess();
            } else if (formCalculateWeight != null)
            {
                if (call == 0)
                    validateMethodCalculate();
                else if (call == 1)
                    await formCalculateWeight.resetScaleConnection();
                else if (call == 2)
                    await formCalculateWeight.processToDeletePesosBrutos();
                else if (call == 3)
                    await formCalculateWeight.processToDeletePesoDeCajas();
                else if (call == 4)
                    await formCalculateWeight.processToDeletePesoTaras();
                else if (call == 5)
                    await formCalculateWeight.multiplicarCajasPorPesos();
                else if (call == 6)
                    await formCalculateWeight.restarPesoTara();
                else if (call == 7)
                    await formCalculateWeight.sumarPesoBruto();
                else if (call == 8)
                    await formCalculateWeight.procesoRestarPesoBruto();
                else if (call == 9)
                    await formCalculateWeight.procesoSumarTaras();
                else if (call == 10)
                    await formCalculateWeight.proccesToSaveRealWeights();
                else if (call == 11)
                    await formCalculateWeight.processToResetAllWeights();
                else if (call == 12)
                    await formCalculateWeight.deleteAllBeforeCapturedWeights();
                else if (call == 13)
                    await formCalculateWeight.validateIfTemporalWeightExist();
            } 
            else if (formAddField != null)
            {
                validateConnectionToTheServerLAN();
            } 
            else if (frmResumenDocuments != null)
            {
                processToCallMethodsInResumenDocumentos();
            } else if (formParametersReports != null)
            {
                processToGeneratePdfs(searchWordPrepedidos);
            } else if (formAddCustomer != null)
            {
                if (call == 0)
                    await formAddCustomer.downloadStatesAndCities();
                else if (call == 1)
                    await formAddCustomer.processToAddOrUpdateNewcustomer(updateCustomer);
                else if (call == 2)
                    await formAddCustomer.getAllUsosCFDIProcess();
                else if (call == 3)
                    await formAddCustomer.getAllRegimienesFiscalesProcess();
            }
            else if (frmClientes != null)
            {
                if (call == 0)
                {
                    this.frmClientes.updateNewCustomers();
                }
            } 
            else if (formPrincipal != null)
            {
                if (call == 0)
                    await formPrincipal.validateProcessToDoReporteCorte();
                else if (call == 1)
                    await formPrincipal.processToDoLogout();
                else if (call == 2)
                    await formPrincipal.aperturaDeCajaProcess();
                else if (call == 3)
                    await formPrincipal.validateCotmosActivatedToVentaProcess();
                else if (call == 4)
                    await formPrincipal.validateCotmosActivatedForWithdrawalsProcess();
                else if (call == 5)
                    await formPrincipal.validateCotmosActivatedToIngresosProcess();
            } else if (frmActivacion != null)
            {
                if (call == 0)
                    await frmActivacion.activateLicense();
            } else if (formConfiguracionGral != null)
            {
                if (call == 0)
                    await formConfiguracionGral.getPanelConfigurationProcess();
                else if (call == 1)
                    await formConfiguracionGral.processToSendConfiguration();
                else if (call == 2)
                    await formConfiguracionGral.saveConfigurations();
                else if (call == 3)
                    await formConfiguracionGral.changeUseScaleProcess();
                else if (call == 4)
                    await formConfiguracionGral.updateWebActiveProcess();
            } else if (formConfiguracionWS != null)
            {
                if (call == 0)
                    await formConfiguracionWS.cotMosPermissionProcess();
                else if (call == 1)
                    await formConfiguracionWS.updateCotMosValue();
            } else if (formSeleccionarCaja != null)
            {
                if (call == 0)
                    await formSeleccionarCaja.addOrUpdateCajaPadreProcess();
            } else if (formCodigoAgente != null)
            {
                if (call == 0)
                    await formCodigoAgente.addOrUpdateAgentInCurrentDocument();
            }
        }

        private async Task processToGeneratePdfs(String searchWordPrepedidos)
        {
            if (call == 0)
                await formParametersReports.processtoDownloadAllPrepedidosFromAllRoutes(searchWordPrepedidos);
        }

        private async Task processToCallMethodsInResumenDocumentos()
        {
            if (call == 0)
            {
                await frmResumenDocuments.processToGenerateReportPdf();
            }
        }

        private async Task validateConnectionToTheServerLAN()
        {
            await formAddField.processToSaveIP();
        }

        private async Task validateMethodCalculate()
        {
            int response = await formCalculateWeight.validarCierreDeVentana();
            if (response == 1)
            {
                formCalculateWeight.refreshFrmVenta();
            } else if (response == 2)
            {
                formCalculateWeight.confirmarEliminarPesos(eventoCerrarVentana);
            } else
            {
                this.Close();
            }
        }

        private async Task processDownloadData(int positionTableDownloads, String codigoCaja)
        {
            int method = positionTableDownloads + 1;
            bool serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
            await ClsInitialChargeController.doIndividualDownloadProcess(frmDownloads, null, 3, method, positionTableDownloads, serverModeLAN, codigoCaja);
        }

        private async Task validateProcessReports()
        {
            if (call == 0 || call == 1)
                formGeneralsReports.callDownloadDocumentsFromServer(call);
            else if (call == 2)
            {
                formGeneralsReports.generatePDfDocuments();
            } else if (call == 3)
            {
                formGeneralsReports.processToGeneratePdfRetiros(false);
            }
        }

        private void FrmWaiting_FormClosed(object sender, FormClosedEventArgs e)
        {
            formGeneralsReports = null;
            progressBarFrmWaiting.Value = 0;
            frmCobranzaCxc = null;
            idRep = 0;
        }

        private void FrmWaiting_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
