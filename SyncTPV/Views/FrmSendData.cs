using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Drawing;
using System.Dynamic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace SyncTPV.Views
{
    public partial class FrmSendData : Form
    {
        private String title, msg;
        private int btnSendOrTerminate = 1;
        private FormWaiting formWaiting;

        public FrmSendData(String title, String msg, int btnSendOrTerminate)
        {
            this.title = title;
            this.msg = msg;
            this.btnSendOrTerminate = btnSendOrTerminate;
            InitializeComponent();
        }

        private void FrmSendData_Load(object sender, EventArgs e)
        {
            this.Text = title;
            textDescription.Text = msg;
            btnAceptar.Text = "Enviar";
            progressBarSendData.Value = 0;
            String datosPendientesDeEnviar = armarStringParaMostrarInformacionAEnviar();
            if (datosPendientesDeEnviar.Equals(""))
            {
                btnSendOrTerminate = 1;
                msg = "Sin datos para enviar";
                btnAceptar.Text = "Enviar";
            }
            else
            {
                msg = datosPendientesDeEnviar;
            }
            PopupNotifier popup = new PopupNotifier();
            popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.caution, 100, 100);
            popup.TitleColor = Color.Red;
            popup.TitleText = "Datos en Cola";
            popup.ContentText = msg;
            popup.ContentColor = Color.Red;
            popup.Popup();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            formWaiting = new FormWaiting(0, this);
            formWaiting.ShowDialog();
        }

        public async Task sendData()
        {
            if (btnSendOrTerminate == 1)
            {
                btnAceptar.Enabled = false;
                await sendAllData(FormPrincipal.methodSend, 1, 0, "");
                btnSendOrTerminate = 2;
                btnAceptar.Text = "Cerrar";
                btnAceptar.Enabled = true;
                
            }
            else if (btnSendOrTerminate == 2)
            {
                if (formWaiting != null)
                {
                    formWaiting.Dispose();
                    formWaiting.Close();
                }
                this.Close();
            }
        }

        public async Task sendAllData(int methodSend, int sendData, int requests, String idDocumento)
        {
            try
            {
                bool serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
                SendDataService sds = new SendDataService();
                var response = await sds.sendDataByMethods(methodSend, sendData, requests, idDocumento, serverModeLAN);
                await validateResponseSendData(response);
            } catch (Exception e)
            {
                SECUDOC.writeLog(e.ToString());
                if (formWaiting != null)
                {
                    formWaiting.Dispose();
                    formWaiting.Close();
                }
                FormMessage formMessage = new FormMessage("Envío de Datos", e.Message+
                    " Método: "+methodSend, 3);
                formMessage.ShowDialog();
            }
        }

        /*public async Task sendAndGetTicket(bool banderaFecha, String Fecha, int idAgente, String referencia, String tipo)
        {
            try
            {
                bool serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
                SendDataService sds = new SendDataService();
                var response = await sds.obtenerTicketsDelWs(
                    banderaFecha, Fecha, idAgente, referencia, tipo
                );
            }
            catch (Exception e)
            {
                SECUDOC.writeLog(e.ToString());
                if (formWaiting != null)
                {
                    formWaiting.Dispose();
                    formWaiting.Close();
                }
                FormMessage formMessage = new FormMessage("Envío de Datos", e.Message +
                    " Método: " + "", 3);
                formMessage.ShowDialog();
            }
        }*/
        private async Task validateResponseSendData(dynamic response)
        {
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            int valor = response.value;
            String description = response.description;
            FormPrincipal.methodSend = response.method;
            FormPrincipal.envioDeDatos = response.envioDeDatos;
            int peticiones = response.peticiones;
            String idDocumento = response.idDocumento;
            if (valor >= 0)
            {
                if (FormPrincipal.envioDeDatos == 1)
                {
                    //if (pbEnvDeDocs != null)
                    //pbEnvDeDocs.setProgress(valor);
                    if (description.Equals("Tiempo Excedido"))
                    {
                        if (textDescription != null)
                        {
                            textDescription.Text = ("" + description + ". Reintentar");
                            textDescription.ForeColor = Color.Red;
                        }
                        if (btnAceptar != null)
                        {
                            btnSendOrTerminate = 1;
                            btnAceptar.Visible = true;
                            btnAceptar.Text = "Reintentar";
                            textDescription.ForeColor = Color.Red;
                        }
                    }
                    else if (description.Equals("Algo falló al intentar conectar con la IP del servidor"))
                    {
                        if (textDescription != null)
                        {
                            textDescription.Text = "" + description + ". Reintentar";
                            textDescription.ForeColor = Color.Red;
                        }
                        if (btnAceptar != null)
                        {
                            btnSendOrTerminate = 1;
                            btnAceptar.Visible = true;
                            btnAceptar.Text = "Reintentar";
                            textDescription.ForeColor = Color.Red;
                        }
                    }
                    else if (description.Equals("Respuesta Incorrecta!"))
                    {
                        if (textDescription != null)
                        {
                            textDescription.Text = "" + description + ". Reintentar";
                            textDescription.ForeColor = Color.Red;
                        }
                        if (btnAceptar != null)
                        {
                            btnSendOrTerminate = 1;
                            btnAceptar.Visible = true;
                            btnAceptar.Text = "Reintentar";
                            textDescription.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        if (textDescription != null)
                        {
                            textDescription.Text = "" + description;
                            textDescription.ForeColor = Color.Blue;
                        }
                        if (valor < 100 && FormPrincipal.methodSend < 8)
                        {
                            await sendAllData(FormPrincipal.methodSend, FormPrincipal.envioDeDatos, peticiones, idDocumento);
                            if (formWaiting != null)
                            {
                                formWaiting.Dispose();
                                formWaiting.Close();
                            }
                        }
                        else
                        {
                            if (formWaiting != null)
                            {
                                formWaiting.Dispose();
                                formWaiting.Close();
                            }
                            //MediaPlayer ring = MediaPlayer.create(MenuActivity.this, R.raw.eventually);
                            //ring.start();
                            btnSendOrTerminate = 2;
                            btnAceptar.Visible = true;
                            btnAceptar.Text = "Terminar";
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
                    if (formWaiting != null)
                    {
                        formWaiting.Dispose();
                        formWaiting.Close();
                    }
                    if (description.Equals("Tiempo Excedido"))
                    {
                        if (textDescription != null)
                        {
                            textDescription.Text = "" + description + ". Reintentar";
                            textDescription.ForeColor = Color.Red;
                        }
                        if (btnAceptar != null)
                        {
                            btnSendOrTerminate = 1;
                            btnAceptar.Text = "Reintentar";
                            btnAceptar.Visible = true;
                        }
                    }
                    else if (description.Equals("Algo falló al intentar conectar con la IP del servidor"))
                    {
                        if (textDescription != null)
                        {
                            textDescription.Text = "" + description + ". Reintentar";
                            textDescription.ForeColor = Color.Red;
                        }
                        if (btnAceptar != null)
                        {
                            btnSendOrTerminate = 1;
                            btnAceptar.Visible = true;
                            btnAceptar.Text = ("Reintentar");
                        }
                    }
                    else
                    {
                        if (progressBarSendData != null)
                            progressBarSendData.Value = (100);
                        if (textDescription != null)
                        {
                            textDescription.Text = "" + description;
                            textDescription.ForeColor = Color.Blue;
                        }
                        //MediaPlayer ring = MediaPlayer.create(MenuActivity.this, R.raw.eventually);
                        //ring.start();
                        btnSendOrTerminate = 2;
                        btnAceptar.Visible = true;
                        btnAceptar.Text = "Terminar";
                    }
                    if (formWaiting != null)
                    {
                        formWaiting.Dispose();
                        formWaiting.Close();
                    }
                }
            } else
            {
                if (progressBarSendData != null)
                    progressBarSendData.Value = (100);
                FormMessage formMessage = new FormMessage("Enviar Datos", description, 3);
                formMessage.ShowDialog();
                if (textDescription != null)
                {
                    textDescription.Text = "" + description + ". Reintentar";
                    textDescription.ForeColor = Color.Red;
                }
                if (btnAceptar != null)
                {
                    btnSendOrTerminate = 1;
                    btnAceptar.Visible = true;
                    btnAceptar.Text = "Reintentar";
                    textDescription.ForeColor = Color.Red;
                }
            }
        }

        private void FrmSendData_FormClosed(object sender, FormClosedEventArgs e)
        {
            btnSendOrTerminate = 1;
            FormPrincipal.methodSend = 1;
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
                LocalDatabase.CAMPO_ENVIADOALWS_DOC + " = " + 0+" OR "+LocalDatabase.CAMPO_IDWEBSERVICE_DOC + " = "+0+" AND "+
                LocalDatabase.CAMPO_PAUSAR_DOC+" = 0");
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
            int ticketsNotSent =  DatosTicketModel.getTheTotalNumberOfTicketsNotSentToTheServer();
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

    }
}
