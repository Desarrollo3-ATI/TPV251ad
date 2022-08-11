using Microsoft.Toolkit.Uwp.Notifications;
using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using SyncTPV.Views;
using SyncTPV.Views.Extras;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace SyncTPV
{
    public partial class FormConfiguracionWS : Form
    {
        string MensajeError = "", query = "";
        int Error = 0;
        int diasRestantes = 0;
        bool BanderaWS = false;
        private FormWaiting formWaiting;
        private bool cotmosActive = false;

        public FormConfiguracionWS()
        {
            InitializeComponent();
        }

        private void frmConfiguracionWS_Load(object sender, EventArgs e)
        {
            txtCodigoSitio.Text = AdminDll.BajoNivel.getCodigoSitio();
            lblVersion.Text += Application.ProductVersion;
            lblVersion.Text = "V"+MetodosGenerales.versionNumber;
            label4.Focus();
            MetodosGenerales.rootDirectory = Application.StartupPath;
            string[] acerca = LicenseModel.getLicenseTypeAndEndDateInLocalDb().Split('|');
            string[] fechaFin = acerca[1].Split(' ');
            if (acerca[0] == "2")
                lblLicencia.Text = "SYNCTPV";
            lblVigencia.Text = fechaFin[0];
            String endDate = fechaFin[0];
            TimeSpan cont;
            try
            {
                cont = clsGeneral.changeLicenseEndDateFormat(endDate) - clsGeneral.changeLicenseEndDateFormat(DateTime.Now.ToString("dd/MM/yyyy"));
            }
            catch
            {
                cont = clsGeneral.changeLicenseEndDateFormat(endDate) - clsGeneral.changeLicenseEndDateFormat(DateTime.Now.ToString("dd/MM/yyyy"));
            }
            diasRestantes = cont.Days;
            lblRestante.Text = diasRestantes + "";
            String url = ConfiguracionModel.getLinkWs();
            if (!url.Equals(""))
            {
                editLinkWsFrmConfigurationWs.Text = url;
            }
            else
            {
                editLinkWsFrmConfigurationWs.Text = "http://miservidor.com/wsRom/WSyncROM.asmx";
            }
            String query = "SELECT " + LocalDatabase.CAMPO_SERVERMODE_CONFIG + " FROM " + LocalDatabase.TABLA_CONFIGURACION;
            int serverMode = ConfiguracionModel.getIntValue(query);
            if (serverMode == 1)
            {
                checkBoxUseInServer.Checked = true;
                editLinkWsFrmConfigurationWs.ReadOnly = true;
            }
            else checkBoxUseInServer.Checked = false;
            validateIdCotMosIsActive();
        }

        private void validateIdCotMosIsActive()
        {
            formWaiting = new FormWaiting(this, 0, "Validanto cotización mostrador...");
            formWaiting.ShowDialog();
        }

        public async Task cotMosPermissionProcess()
        {
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                dynamic responseCotmos = ConfiguracionModel.cotmosActive();
                if (responseCotmos.value == 1)
                {
                    value = 1;
                    cotmosActive = responseCotmos.active;
                } else
                {
                    value = responseCotmos.value;
                    description = responseCotmos.description;
                }
            });
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            if (value == 1)
            {
                checkBoxCotMos.Checked = cotmosActive;
                btnSelectCaja.Enabled = cotmosActive;
            } else
            {
                FormMessage formMessage = new FormMessage("Cotización de Mostrador", description, 3);
                formMessage.ShowDialog();
            }
        }

        private void btnProbarConexion_Click(object sender, EventArgs e)
        {
            doPing();
        }

        public async Task doPing()
        {
            int value = 0;
            String description = "";
            this.Cursor = Cursors.WaitCursor;
            btnProbarConexion.Enabled = false;
            String linkWs = editLinkWsFrmConfigurationWs.Text.Trim();
            await Task.Run(async () =>
            {
                if (!linkWs.Equals(""))
                {
                    dynamic responsePing = await ConfigurationWsController.doPingWs(linkWs);
                    if (responsePing.value == 1)
                    {
                        dynamic responseConfig = ConfiguracionModel.saveLinkWs(linkWs);
                        if (responseConfig.value == 1)
                        {
                            String query = "SELECT " + LocalDatabase.CAMPO_SERVERMODE_CONFIG + " FROM " + LocalDatabase.TABLA_CONFIGURACION + " WHERE " +
                            LocalDatabase.CAMPO_ID_CONFIGURACION + " = " + 1;
                            if (ConfiguracionModel.getIntValue(query) == 1)
                                ConfigurationWsController.getInstancesSQLSE();
                            dynamic responseCajas = await CajaController.getAllCajasAPI();
                            if (responseCajas.value == 1)
                            {
                                dynamic responseUsers = await downloadAllUsersProcess();
                                if (responseUsers.value == 1)
                                {
                                    value = 1;
                                    description = "Conexión existosa!\r\nUsuarios actualizados correctamente!";
                                }
                                else
                                {
                                    value = responseUsers.value;
                                    description = responseUsers.description;
                                }
                            } else
                            {
                                value = responseCajas.value;
                                description = responseCajas.description;
                            }
                        }
                        else
                        {
                            value = responseConfig.value;
                            description = responseConfig.description;
                        }
                    }
                    else
                    {
                        value = responsePing.value;
                        description = responsePing.description;
                    }
                } else
                {
                    description = "Tienes que agregar una URL válida";
                }
            });
            btnProbarConexion.Enabled = true;
            this.Cursor = Cursors.Default;
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            if (value == 1)
            {
                FormMessage formMessage = new FormMessage("Proceso de Conexión", description, 1);
                formMessage.ShowDialog();
            } else
            {
                FormMessage formMessage = new FormMessage("Proceso de Conexión", description, 3);
                formMessage.ShowDialog();
            }
        }

        private void btnDescargarUsuario_Click(object sender, EventArgs e)
        {
            downloadAllUsersProcess();
        }

        private void editLinkWsFrmConfigurationWs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                doPing();
            }
        }

        private void checkBoxUseInServer_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUseInServer.Checked)
            {
                editLinkWsFrmConfigurationWs.Enabled = false;
                btnProbarConexion.Enabled = false;
                if (ConfiguracionModel.configurationExist(1))
                {
                    String query = "UPDATE " + LocalDatabase.TABLA_CONFIGURACION + " SET " + LocalDatabase.CAMPO_SERVERMODE_CONFIG + " = 1 WHERE " +
                    LocalDatabase.CAMPO_ID_CONFIGURACION + " = " + 1;
                    if (ConfiguracionModel.updateServerMode(query))
                    {
                        FormAddField formAddField = new FormAddField(this);
                        formAddField.StartPosition = FormStartPosition.CenterScreen;
                        formAddField.ShowDialog();
                        checkBoxUseInServer.Checked = true;
                        PopupNotifier popup = new PopupNotifier();
                        popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                        popup.TitleColor = Color.Blue;
                        popup.TitleText = "Uso en Red LAN Activado";
                        popup.ContentText = "Esta aplicación fue configurada para usar en red local";
                        popup.ContentColor = Color.Blue;
                        popup.Popup();
                    }
                } else
                {
                    dynamic responseConfig = ConfiguracionModel.saveLinkWs("");
                    if (responseConfig.value == 1)
                    {
                        String query = "UPDATE " + LocalDatabase.TABLA_CONFIGURACION + " SET " + LocalDatabase.CAMPO_SERVERMODE_CONFIG + " = 1 WHERE " +
                    LocalDatabase.CAMPO_ID_CONFIGURACION + " = " + 1;
                        if (ConfiguracionModel.updateServerMode(query))
                        {
                            FormAddField formAddField = new FormAddField(this);
                            formAddField.StartPosition = FormStartPosition.CenterScreen;
                            formAddField.ShowDialog();
                            checkBoxUseInServer.Checked = true;
                            PopupNotifier popup = new PopupNotifier();
                            popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                            popup.TitleColor = Color.Blue;
                            popup.TitleText = "Uso en Red LAN Activado";
                            popup.ContentText = "Esta aplicación fue configurada para usar en red local";
                            popup.ContentColor = Color.Blue;
                            popup.Popup();
                        }
                    } else
                    {
                        FormMessage formMessage = new FormMessage("Configuración", responseConfig.description, 3);
                        formMessage.ShowDialog();
                    }
                }
            }
            else
            {
                editLinkWsFrmConfigurationWs.Enabled = true;
                editLinkWsFrmConfigurationWs.ReadOnly = false;
                btnProbarConexion.Enabled = true;
                if (ConfiguracionModel.configurationExist(1))
                {
                    String query = "UPDATE " + LocalDatabase.TABLA_CONFIGURACION + " SET " + LocalDatabase.CAMPO_SERVERMODE_CONFIG + " = 0 WHERE " +
                    LocalDatabase.CAMPO_ID_CONFIGURACION + " = " + 1;
                    if (ConfiguracionModel.updateServerMode(query))
                    {
                        checkBoxUseInServer.Checked = false;
                        PopupNotifier popup = new PopupNotifier();
                        popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                        popup.TitleColor = Color.Blue;
                        popup.TitleText = "Uso en Red LAN Desactivado";
                        popup.ContentText = "Esta aplicación fue configurada para usar con o sin Internet";
                        popup.ContentColor = Color.Blue;
                        popup.Popup();
                    }
                } else
                {
                    String linkWs = editLinkWsFrmConfigurationWs.Text.Trim();
                    if (linkWs.Equals(""))
                    {
                        FormMessage formMessage = new FormMessage("Datos Faltantes", "Tienes que agregar una URL válida", 2);
                        formMessage.ShowDialog();
                    } else
                    {
                        dynamic responseConfig = ConfiguracionModel.saveLinkWs(linkWs);
                        if (responseConfig.value == 1)
                        {
                            String query = "UPDATE " + LocalDatabase.TABLA_CONFIGURACION + " SET " + LocalDatabase.CAMPO_SERVERMODE_CONFIG + " = 0 WHERE " +
                        LocalDatabase.CAMPO_ID_CONFIGURACION + " = " + 1;
                            if (ConfiguracionModel.updateServerMode(query))
                            {
                                checkBoxUseInServer.Checked = false;
                                PopupNotifier popup = new PopupNotifier();
                                popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                                popup.TitleColor = Color.Blue;
                                popup.TitleText = "Uso en Red LAN Desactivado";
                                popup.ContentText = "Esta aplicación fue configurada para usar con o sin Internet";
                                popup.ContentColor = Color.Blue;
                                popup.Popup();
                            }
                        } else
                        {
                            FormMessage formMessage = new FormMessage("Configuración", responseConfig.description, 3);
                            formMessage.ShowDialog();
                        }
                    }
                }
            }
        }

        private void checkBoxCotMos_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCotMos.Focused)
            {
                if (checkBoxCotMos.Checked)
                {
                    cotmosActive = true;
                    showFormWaitingToUpdateCotmosProcess();
                } else
                {
                    cotmosActive = false;
                    showFormWaitingToUpdateCotmosProcess();
                }
            }
        }

        private void showFormWaitingToUpdateCotmosProcess()
        {
            formWaiting = new FormWaiting(this, 1, "Actualizado permiso cotizaciones de mostrador...");
            formWaiting.ShowDialog();
        }

        public async Task updateCotMosValue()
        {
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                int cotmos = 0;
                if (cotmosActive)
                    cotmos = 1;
                dynamic responseCotmos = ConfiguracionModel.updateCotmos(cotmos);
                if (responseCotmos.value == 1)
                {
                    value = 1;
                } else
                {
                    value = responseCotmos.value;
                    description = responseCotmos.description;
                    if (cotmosActive)
                        cotmosActive = false;
                    else cotmosActive = true;
                }
            });
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            if (value == 1)
            {
                checkBoxCotMos.Checked = cotmosActive;
                btnSelectCaja.Enabled = cotmosActive;
                new ToastContentBuilder()
                        .AddArgument("action", "viewConversation")
                        .AddArgument("conversationId", 9814)
                        // Inline image
                        //.AddInlineImage(new Uri("https://picsum.photos/360/202?image=883"))
                        // Profile (app logo override) image
                        .AddAppLogoOverride(new Uri("ms-appdata:" + Properties.Resources.synctpvlogo), ToastGenericAppLogoCrop.Circle)
                        .AddText("Cotización Mostrador")
                        .AddText("Uso de cotizaciones de mostrador actualizada...")
                        .Show();
                if (cotmosActive)
                {
                    FormMessage formMessage = new FormMessage("Cotización Mostrador", "Por favor selecciona la Caja padre, encargada de cobrar las cotizaciones de " +
                    "mostrador\r\nClic en el botón Cajas, después selecciona la caja de la lista haciendo clic " +
                    "sobre la misma!", 1);
                    formMessage.ShowDialog();
                }
            } else
            {
                checkBoxCotMos.Checked = cotmosActive;
                btnSelectCaja.Enabled = cotmosActive;
                FormMessage formMessage = new FormMessage("Cotización Mostrador", description, 3);
                formMessage.ShowDialog();
            }
        }

        private void btnSelectCaja_Click(object sender, EventArgs e)
        {
            FormSeleccionarCaja formSeleccionarCaja = new FormSeleccionarCaja();
            formSeleccionarCaja.ShowDialog();
        }

        private async Task<ExpandoObject> downloadAllUsersProcess()
        {
            return await UsersController.getAllUsers();
        }


    }
}

