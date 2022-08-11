using SyncTPV.Models;
using SyncTPV.Views;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace SyncTPV
{
    public partial class FrmActivacion : Form
    {
        private FormWaiting formWaiting;
        string MensajeError = "";
        bool activado = false;
        int Error = 0;
        public FrmActivacion()
        {
            InitializeComponent();
        }

        private void picCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmActivacion_Load(object sender, EventArgs e)
        {
            editSiteCodeFrmActivacion.Text = AdminDll.BajoNivel.getCodigoSitio();
            String siteCode = editSiteCodeFrmActivacion.Text;
            if (LicenseModel.doesTheLicenseExistLocally(siteCode))
            {
                txtActivar.Text = "LICENCIA ACTIVADA";
                txtActivar.ReadOnly = true;
                btnActivar.Enabled = false;
            }
        }

        private void btnActivar_Click(object sender, EventArgs e)
        {
            formWaiting = new FormWaiting(this, 0);//activateLicense();
            formWaiting.ShowDialog();
        }

        public async Task activateLicense()
        {
            int value = 0;
            String description = "";
            String synckey = txtActivar.Text.Trim();
            btnActivar.Text = "Activando...";
            btnActivar.Enabled = false;
            Cursor = Cursors.WaitCursor;
            String siteCode = editSiteCodeFrmActivacion.Text.Trim();
            await Task.Run(async () =>
            {
                if (!synckey.Equals(""))
                {
                    if (synckey.Length == 34)
                    {
                        DateTime fechaActual = DateTime.Now.Date;
                        dynamic responseLic = await LicenseModel.activateLicenseInTheServer(siteCode, synckey);
                        if (responseLic.value == 1)
                        {
                            clsRegistro Registro = await LicenseModel.getRegeditValues();
                            if (Registro != null)
                            {
                                await ClsLicenciamientoController.addDataInLicenseLic(ClsLicenciamientoController.nombreSync, Registro.TL, Registro.FV,
                                    Convert.ToInt32(Registro.DR));
                                activado = true;
                                value = 1;
                            }
                            else
                            {
                                value = -2;
                                description = responseLic.description;
                            }
                        }
                        else if (responseLic.value == -2)
                        {
                            value = -1;
                            description = responseLic.description;   
                        }
                        else if (responseLic.value == -3)
                        {
                            value = -1;
                            description = responseLic.description;
                        }
                        else
                        {
                            value = -1;
                            description = responseLic.description;
                        }
                    }
                    else if (synckey.Length > 34)
                    {
                        description = "Synkey demasiado larga!";
                    }
                    else if (synckey.Length < 34)
                    {
                        description = "Synkey demasiado corta, faltan caracteres!";
                    }
                }
                else
                {
                    description = "Ingresa una synckey válida!";
                }
            });
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            if (value == 1)
            {
                Cursor = Cursors.Default;
                btnActivar.Text = "Activado";
                btnActivar.Enabled = false;
                PopupNotifier popup = new PopupNotifier();
                popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                popup.TitleColor = Color.FromArgb(43, 143, 192);
                popup.TitleText = "Activando Licencia";
                popup.TitlePadding = new Padding(5, 5, 5, 5);
                popup.ButtonBorderColor = Color.Red;
                popup.ContentText = "Licencia activada correctamente!";
                popup.ContentColor = Color.FromArgb(43, 143, 192);
                popup.HeaderHeight = 10;
                popup.AnimationDuration = 1000;
                popup.HeaderColor = Color.FromArgb(200, 244, 255);
                popup.Popup();
                this.Close();
            } else if (value == -1)
            {
                Cursor = Cursors.Default;
                btnActivar.Text = "Activar";
                btnActivar.Enabled = true;
                FormMessage formMessage = new FormMessage("Activando licencia", description, 2);
                formMessage.ShowDialog();
            } else if (value == -2)
            {
                btnActivar.Enabled = true;
                Cursor = Cursors.Default;
                btnActivar.Text = "Activar";
                btnActivar.Enabled = true;
                FormMessage formMessage = new FormMessage("Activando licencia", description, 2);
                formMessage.ShowDialog();
            }
            else
            {
                FormMessage formMessage = new FormMessage("Activando licencia", description, 2);
                formMessage.ShowDialog();
            }
        }

        private void txtActivar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                activateLicense();
            }
        }
    }

}
