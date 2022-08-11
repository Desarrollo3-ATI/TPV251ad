using Microsoft.Toolkit.Uwp.Notifications;
using SyncTPV.Models;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace SyncTPV
{
    public partial class FormPasswordConfirmation : Form
    {
        public static Boolean permissionGranted = false;
        String title = "";
        String message = "";
        private bool serverModeLAN = false;
        private String panelInstance = "";

        public FormPasswordConfirmation(String title, String msg)
        {
            this.title = title;
            this.message = msg;
            InitializeComponent();
            permissionGranted = false;
            serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
            if (serverModeLAN)
            {
                panelInstance = InstanceSQLSEModel.getStringPanelInstance();
            }
            imgCancelar.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.btn_cancelar_normal, 132, 40);
            imgAceptar.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.btn_aceptar_normal, 132, 40);
        }

        private void FrmValidacionDocumentos_Load(object sender, EventArgs e)
        {
            textMessageFrmPasswordConfirmation.Text = message;
            this.Text = title;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            permissionGranted = false;
            this.Close();
        }

        private void txtContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                imgAceptar_Click(sender, e);
            }
        }

        private void FrmPasswordConfirmation_FormClosed(object sender, FormClosedEventArgs e)
        {
            //permissionGranted = false;
        }

        private void editPasswordValidacionDocumentos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private async void imgAceptar_Click(object sender, EventArgs e)
        {
            imgAceptar.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.btn_aceptar_presionado, 132, 40);
            int value = 0;
            String description = "";
            String password = editPasswordValidacionDocumentos.Text.Trim();
            await Task.Run(async () =>
            {
                if (!password.Equals(""))
                {
                    if (serverModeLAN)
                    {
                        dynamic responsePass = ClsAgentesModel.isItSupervisorPassword(panelInstance, password);
                        if (responsePass.value == 1)
                        {
                            bool existe = responsePass.existe;
                            if (existe)
                            {
                                permissionGranted = true;
                                value = 1;
                            }
                            else
                            {
                                value = responsePass.value;
                                description = responsePass.description;
                                permissionGranted = false;
                                Thread.Sleep(150);
                            }
                        } else
                        {
                            value = responsePass.value;
                            description = responsePass.description;
                            permissionGranted = false;
                            Thread.Sleep(150);
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
                                value = 1;
                                permissionGranted = true;
                            }
                            else
                            {
                                value = responsePass.value;
                                description = responsePass.description;
                                permissionGranted = false;
                                Thread.Sleep(150);
                            }
                        } else
                        {
                            value = responsePass.value;
                            description = responsePass.description;
                            permissionGranted = false;
                            Thread.Sleep(150);
                        }
                    }
                }
                else
                {
                    description = "Tienes que agregar una contraseña válida";
                    permissionGranted = false;
                    Thread.Sleep(150);
                }
            });
            if (value == 1 && permissionGranted)
            {
                this.Close();
            } else
            {
                PopupNotifier popup = new PopupNotifier();
                popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.caution, 100, 100);
                popup.TitleColor = Color.Red;
                popup.TitleText = "Contraseña del Supervisor";
                popup.ContentText = description;
                popup.ContentColor = Color.Red;
                popup.Popup();
                imgAceptar.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.btn_aceptar_normal, 132, 40);
            }
        }

        private void imgCancelar_Click(object sender, EventArgs e)
        {
            permissionGranted = false;
            this.Close();
        }
    }
}
