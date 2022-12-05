using Cripto;
using Newtonsoft.Json.Linq;
using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Threading;
using System.Windows.Forms;

namespace SyncTPV
{
    public partial class frmValidacionCargaInicial : Form
    {

        public frmValidacionCargaInicial()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            int value = 0;
            String description = "";
            bool permissionGranted = false;
            string password = txtContraseña.Text;
            String panelInstance = "";
            bool serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
            if (serverModeLAN)
            {
                panelInstance = InstanceSQLSEModel.getStringPanelInstance();
            }
            if (password == "" || password == null)
            {
                FormMessage msj = new FormMessage("Datos Requeridos!", "Debes ingresar una contraseña válida", 2);
                msj.ShowDialog();
            }
            else
            {
                if (serverModeLAN)
                {
                    dynamic responsePass = ClsAgentesModel.isItSupervisorPassword(panelInstance, password);
                    if (responsePass.value == 1)
                    {
                        bool existe = responsePass.existe;
                        if (existe)
                        {
                            FormMessage formMessage = new FormMessage("Modo LAN Activado", "Cuando usas el modo LAN todas las conexiones se realizan directamente al servidor," +
                            " por lo que ya no es necesario realizar carga inicial", 1);
                            formMessage.ShowDialog();
                        }
                        else
                        {
                            FormMessage msj = new FormMessage("Sin supervisor", "No existe un agente supervisor.", 2);
                            msj.ShowDialog();
                        }
                    }
                    else
                    {
                        FormMessage msj = new FormMessage("Acción denegada", "Contraseña erronea.", 2);
                        msj.ShowDialog();
                    }
                }
                else
                {
                    dynamic responsePass = UserModel.isItSupervisorPassword(password);
                    if (responsePass.value == 1)
                    {
                        bool existe = responsePass.existe;
                        if (existe)
                        {
                            bool licenseActive = await clsGeneral.isTheLicenseValid(AES.Desencriptar(LicenseModel.getEndDateEncryptFromTheLocalDb()), "");
                            if (!licenseActive)
                            {
                                FormMessage msj = new FormMessage("Licencia Expirada", "Su licencia ha expirado.", 2);
                                msj.ShowDialog();
                            }
                            else
                            {
                                String pendingData = armarStringParaMostrarInformacionAEnviar();
                                //int pendingDocuments = ClsDocumentModel.getAllDocumentsCanceledOrNotSended();
                                if (pendingData.Equals(""))
                                {
                                    this.Close();
                                    FormPrincipal.doInitialCharge = true;
                                }
                                else
                                {
                                    FormPrincipal.doInitialCharge = false;
                                    FormMessage msj = new FormMessage("Validar Documentos Pendientes", "Hay documentos pendientes por Enviar o Pausados", 2);
                                    msj.ShowDialog();
                                }
                            }
                        }
                        else
                        {
                            FormMessage msj = new FormMessage("Sin supervisor", "No existe un agente supervisor.", 2);
                            msj.ShowDialog();
                        }
                    }
                    else
                    {
                        FormMessage msj = new FormMessage("Acción denegada", "Contraseña erronea.", 2);
                        msj.ShowDialog();
                    }
                }

            }
        }

        private String armarStringParaMostrarInformacionAEnviar()
        {
            String datosPendientesDeEnviar = "";
            int cadcNotSent = CustomerADCModel.getTheTotalNumberOfAdditionalCustomersNotSent();
            if (cadcNotSent > 0)
            {
                if (cadcNotSent == 1)
                {
                    datosPendientesDeEnviar += cadcNotSent + " Cliente Nuevo\n";
                }
                else
                {
                    datosPendientesDeEnviar += cadcNotSent + " Clientes Nuevos\n";
                }
            }
            int docsNotSent = DocumentModel.getTotalNumberOfDocuments("SELECT COUNT(*) FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                LocalDatabase.CAMPO_ENVIADOALWS_DOC + " = " + 0);
            if (docsNotSent > 0)
            {
                if (docsNotSent == 1)
                {
                    datosPendientesDeEnviar += docsNotSent + " Documento\n";
                }
                else
                {
                    datosPendientesDeEnviar += docsNotSent + " Documentos\n";
                }
            }
            int locationsNotSent = ClsPositionsModel.getTheTotalNumberOfUnsentLocations();
            if (locationsNotSent > 0)
            {
                if (locationsNotSent == 1)
                {
                    datosPendientesDeEnviar += locationsNotSent + " Ubicación\n";
                }
                else
                {
                    datosPendientesDeEnviar += locationsNotSent + " Ubicaciones\n";
                }
            }
            int cxcNotSent = CuentasXCobrarModel.getTheTotalNumberOfCreditsNotSent();
            if (cxcNotSent > 0)
            {
                if (cxcNotSent == 1)
                {
                    datosPendientesDeEnviar += cxcNotSent + " Pago";
                }
                else
                {
                    datosPendientesDeEnviar += cxcNotSent + " Pagos";
                }
            }
            int retirosNotSent = RetiroModel.getTheTotalNumberOfWithdrawalsNotSentToTheServer();
            if (retirosNotSent > 0)
            {
                if (retirosNotSent == 1)
                {
                    datosPendientesDeEnviar += retirosNotSent + " Corte de caja";
                }
                else
                {
                    datosPendientesDeEnviar += retirosNotSent + " Cortes de caja";
                }
            }
            int ticketsNotSent = DatosTicketModel.getTheTotalNumberOfTicketsNotSentToTheServer();
            if (ticketsNotSent > 0)
            {
                if (ticketsNotSent == 1)
                {
                    datosPendientesDeEnviar += ticketsNotSent + " tickets";
                }
                else
                {
                    datosPendientesDeEnviar += ticketsNotSent + " tickets";
                }
            }
            return datosPendientesDeEnviar;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormPrincipal.doInitialCharge = false;
            this.Close();
        }

        private void picCerrar_Click(object sender, EventArgs e)
        {
            FormPrincipal.doInitialCharge = false;
            this.Close();
        }

        private void FrmValidacionCargaInicial_Load(object sender, EventArgs e)
        {
            FormPrincipal.doInitialCharge = false;
            lblCaja.Text = UserModel.getAStringValueForAnyUser("SELECT " + LocalDatabase.CAMPO_CAJANOMBRE_USUARIO + " FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " +
                LocalDatabase.CAMPO_ID_USUARIO + " = " + ClsRegeditController.getIdUserInTurn());
            txtContraseña.Focus();
            //string ultimaCargaInicial = TraerUltimaCargaInicial();
            //lblFechaHora.Text = ultimaCargaInicial;
            String lastCarga = ClsBanderasCargaInicialModel.getTheLastCreatedAtOfARecord();
            if (lastCarga.Equals(""))
            {
                textLastInitialCharge.Text = "Útima Descarga: " + lastCarga;

            }
            else
            {
                textLastInitialCharge.Text = "Útima Descarga: " + lastCarga;
            }
        }

        private void txtContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                button1_Click(sender, e);
            }
        }

        private void txtContraseña_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
