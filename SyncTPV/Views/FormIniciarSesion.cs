using AdminDll;
using Cripto;
using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using SyncTPV.Views;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace SyncTPV
{
    public partial class FormIniciarSesion : Form
    {
        private bool rememberLogin = false;
        private bool doLogin = false;
        private bool cotmosAcitve = false;

        public FormIniciarSesion()
        {
            InitializeComponent();
            btnConfiguracionInicial.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.setting_black, 31, 31);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            General.RutaInicial = MetodosGenerales.rootDirectory;
            btnLogin.Enabled = false;
            checkBoxRecordarLogin.Enabled = false;
            btnConfiguracionInicial.Visible = false;
            if (FrmSplash.firtsTime)
            {
                this.Hide();
                FrmSplash splash = new FrmSplash();
                splash.StartPosition = FormStartPosition.CenterScreen;
                splash.ShowDialog();
            }
            else
            {
                this.Show();
            }
            await loadInitialData();
            await validateIfCheckoutIsCotMos();
        }

        private async Task loadInitialData()
        {
            int value = 0;
            String description = "";
            String versionNumber = "";
            String email = "";
            String pass = "";
            await Task.Run(async () => {
                rememberLogin = ClsRegeditController.getRememberLogin();
                if (ConfiguracionModel.isLANPermissionActivated())
                    versionNumber = "LAN V " + MetodosGenerales.versionNumber;
                else versionNumber = "Web V " + MetodosGenerales.versionNumber;
                string siteCode = BajoNivel.getCodigoSitio();
                if (LicenseModel.doesTheLicenseExistLocally(siteCode))
                {
                    dynamic responsePing = await LicenseModel.validateIfLicenseServerIsAccesible();
                    if (responsePing.value == 1)
                    {
                        dynamic responseLic = await LicenseModel.validateLicense();
                        if (responseLic.value == 1)
                        {
                            if (ClsRegeditController.getStatusLogin() && ClsRegeditController.getRememberLogin())
                            {
                                email = UserModel.getAStringValueForAnyUser("SELECT " + LocalDatabase.CAMPO_ClAVE_USUARIO + " FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " +
                                    LocalDatabase.CAMPO_ID_USUARIO + " = " + ClsRegeditController.getIdUserInTurn());
                                pass = UserModel.getAStringValueForAnyUser("SELECT " + LocalDatabase.CAMPO_PASSWORD_USUARIO + " FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " +
                                    LocalDatabase.CAMPO_ID_USUARIO + " = " + ClsRegeditController.getIdUserInTurn());
                                if (email.Trim().Equals("") && pass.Trim().Equals(""))
                                {
                                    ClsRegeditController.saveIdUserInTurn(0);
                                    ClsRegeditController.saveLoginStatus(false);
                                    ClsRegeditController.saveCurrentIdEnterprise(0);
                                    value = 1;
                                }
                                else
                                {
                                    value = 2;
                                }
                            }
                            else
                            {
                                ClsRegeditController.saveIdUserInTurn(0);
                                ClsRegeditController.saveLoginStatus(false);
                                ClsRegeditController.saveCurrentIdEnterprise(0);
                                value = 1;
                            }
                        }
                        else
                        {
                            dynamic responseLocalLic = await LicenseModel.validateLocalLicense();
                            if (responseLocalLic.value == 1)
                            {
                                if (ClsRegeditController.getStatusLogin() && ClsRegeditController.getRememberLogin())
                                {
                                    email = UserModel.getAStringValueForAnyUser("SELECT " + LocalDatabase.CAMPO_ClAVE_USUARIO + " FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " +
                                        LocalDatabase.CAMPO_ID_USUARIO + " = " + ClsRegeditController.getIdUserInTurn());
                                    pass = UserModel.getAStringValueForAnyUser("SELECT " + LocalDatabase.CAMPO_PASSWORD_USUARIO + " FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " +
                                        LocalDatabase.CAMPO_ID_USUARIO + " = " + ClsRegeditController.getIdUserInTurn());
                                    if (email.Trim().Equals("") && pass.Trim().Equals(""))
                                    {
                                        ClsRegeditController.saveIdUserInTurn(0);
                                        ClsRegeditController.saveLoginStatus(false);
                                        ClsRegeditController.saveCurrentIdEnterprise(0);
                                        value = 1;
                                    }
                                    else
                                    {
                                        value = 2;
                                    }
                                }
                                else
                                {
                                    ClsRegeditController.saveIdUserInTurn(0);
                                    ClsRegeditController.saveLoginStatus(false);
                                    ClsRegeditController.saveCurrentIdEnterprise(0);
                                    value = 1;
                                }
                            }
                            else
                            {
                                value = responseLocalLic.value;
                                description = responseLocalLic.description;
                            }
                        }
                    }
                    else
                    {
                        dynamic responseLocalLic = await LicenseModel.validateLocalLicense();
                        if (responseLocalLic.value == 1)
                        {
                            if (ClsRegeditController.getStatusLogin() && ClsRegeditController.getRememberLogin())
                            {
                                email = UserModel.getAStringValueForAnyUser("SELECT " + LocalDatabase.CAMPO_ClAVE_USUARIO + " FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " +
                                    LocalDatabase.CAMPO_ID_USUARIO + " = " + ClsRegeditController.getIdUserInTurn());
                                pass = UserModel.getAStringValueForAnyUser("SELECT " + LocalDatabase.CAMPO_PASSWORD_USUARIO + " FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " +
                                    LocalDatabase.CAMPO_ID_USUARIO + " = " + ClsRegeditController.getIdUserInTurn());
                                if (email.Trim().Equals("") && pass.Trim().Equals(""))
                                {
                                    ClsRegeditController.saveIdUserInTurn(0);
                                    ClsRegeditController.saveLoginStatus(false);
                                    ClsRegeditController.saveCurrentIdEnterprise(0);
                                    value = 1;
                                }
                                else
                                {
                                    value = 2;
                                }
                            }
                            else
                            {
                                ClsRegeditController.saveIdUserInTurn(0);
                                ClsRegeditController.saveLoginStatus(false);
                                ClsRegeditController.saveCurrentIdEnterprise(0);
                                value = 1;
                            }
                        }
                        else
                        {
                            value = responseLocalLic.value;
                            description = responseLocalLic.description;
                        }
                    }
                }
                else
                {
                    description = "SyncTPV No activado, tienes que activar la aplicación con la Synckey asignada!";
                    /*btnLogin.Enabled = false;
                    checkBoxRecordarLogin.Enabled = false;
                    btnConfiguracionInicial.Visible = false;
                    linkLabelActivateLicenseFrmIniciarSesion.Visible = true;
                    PopupNotifier popup = new PopupNotifier();
                    popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.warning_red, 100, 100);
                    popup.TitleColor = Color.Red;
                    popup.TitleText = "Licencia No Activada";
                    popup.ContentText = "Debes activar tu licencia ingresando la Synckey que se te proporcionó";
                    popup.ContentColor = Color.RosyBrown;
                    popup.Popup();*/
                }
            });
            textVersionFrmLogin.Text = versionNumber;
            if (value == 1)
            {
                btnLogin.Enabled = true;
                btnLogin.Visible = true;
                checkBoxRecordarLogin.Enabled = true;
                btnConfiguracionInicial.Visible = true;
                linkLabelActivateLicenseFrmIniciarSesion.Visible = false;
                this.Select();
                editUsernameFrmIniciarSesion.Text = "";
                editUsernameFrmIniciarSesion.Select();
            } else if (value == 2)
            {
                await processToValidateLoginAutomatically(email, pass);
            } else
            {
                FormMessage formMessage = new FormMessage("Licencia", description, 2);
                formMessage.ShowDialog();
                btnLogin.Enabled = false;
                checkBoxRecordarLogin.Enabled = false;
                btnConfiguracionInicial.Visible = false;
                linkLabelActivateLicenseFrmIniciarSesion.Visible = true;
                PopupNotifier popup = new PopupNotifier();
                popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.warning_red, 100, 100);
                popup.TitleColor = Color.Red;
                popup.TitleText = "Licencia No Activada";
                popup.ContentText = "Debes activar tu licencia ingresando la Synckey que se te proporcionó";
                popup.ContentColor = Color.RosyBrown;
                popup.Popup();
            }
        }

        private async Task processToValidateLoginAutomatically(String email, String pass)
        {
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                dynamic responseLogin = null;
                if (ConfiguracionModel.isLANPermissionActivated())
                    responseLogin = await UsersController.userLoginServerLAN(email, pass);
                else responseLogin = await UsersController.userLoginServerAPI(email, pass);
                if (responseLogin.value == 1)
                {
                    dynamic responsePingLicense = await LicenseModel.validateIfLicenseServerIsAccesible();
                    if (responsePingLicense.value > 0)
                    {
                        dynamic responseValidateLicense = await LicenseModel.validateLicense();
                        if (responseValidateLicense.value > 0)
                        {
                            value = 1;
                        }
                        else
                        {
                            bool licenseActive = await clsGeneral.isTheLicenseValid(AES.Desencriptar(LicenseModel.getEndDateEncryptFromTheLocalDb()), "");
                            if (!licenseActive)
                            {
                                ClsRegeditController.saveIdUserInTurn(0);
                                ClsRegeditController.saveLoginStatus(false);
                                ClsRegeditController.saveCurrentIdEnterprise(0);
                                description = "Su licencia ha expirado!";
                            }
                            else
                            {
                                value = 1;
                            }
                        }
                    }
                    else
                    {
                        bool licenseActivate = await clsGeneral.isTheLicenseValid(AES.Desencriptar(LicenseModel.getEndDateEncryptFromTheLocalDb()), "");
                        if (!licenseActivate)
                        {
                            ClsRegeditController.saveIdUserInTurn(0);
                            ClsRegeditController.saveLoginStatus(false);
                            ClsRegeditController.saveCurrentIdEnterprise(0);
                            description = "Su licencia ha expirado!";
                        }
                        else
                        {
                            value = 1;
                        }
                    }
                }
                else
                {
                    bool licenseActive = await clsGeneral.isTheLicenseValid(AES.Desencriptar(LicenseModel.getEndDateEncryptFromTheLocalDb()), "");
                    if (!licenseActive)
                    {
                        ClsRegeditController.saveIdUserInTurn(0);
                        ClsRegeditController.saveLoginStatus(false);
                        ClsRegeditController.saveCurrentIdEnterprise(0);
                        description = "Su licencia ha expirado!";
                    }
                    else
                    {
                        value = 1;
                    }
                }
            });
            if (value == 1)
            {
                await verifyUserLoggedIn(email, pass);
            }
            else
            {
                FormMessage msj = new FormMessage("Licencia", description, 2);
                msj.ShowDialog();
                btnLogin.Enabled = true;
                btnLogin.Visible = true;
                checkBoxRecordarLogin.Enabled = true;
                btnConfiguracionInicial.Visible = true;
                linkLabelActivateLicenseFrmIniciarSesion.Visible = false;
                editUsernameFrmIniciarSesion.Select();
            }
        }

        private async Task validateIfCheckoutIsCotMos()
        {
            await Task.Run(async () =>
            {
                dynamic responseCotmos = ConfiguracionModel.cotmosActive();
                if (responseCotmos.value == 1)
                {
                    cotmosAcitve = responseCotmos.active;
                } else
                {

                }
            });
            if (cotmosAcitve)
            {
                editUsernameFrmIniciarSesion.Text = "COTMOS";
                editUsernameFrmIniciarSesion.ReadOnly = true;
                editUsernameFrmIniciarSesion.Focus();
                editPassFrmIniciarSesion.Text = "**********";
                editPassFrmIniciarSesion.ReadOnly = true;
                checkBoxRecordarLogin.Enabled = false;
            }
            else
            {
                editUsernameFrmIniciarSesion.Text = "";
                editUsernameFrmIniciarSesion.ReadOnly = false;
                editUsernameFrmIniciarSesion.Focus();
                editPassFrmIniciarSesion.Text = "";
                editPassFrmIniciarSesion.ReadOnly = false;
                checkBoxRecordarLogin.Enabled = true;
            }
        }

        private async void btnConfiguracion_Click(object sender, EventArgs e)
        {
            FormConfiguracionWS frmConfiguracion = new FormConfiguracionWS();
            frmConfiguracion.ShowDialog();
            await validateIfCheckoutIsCotMos();
            String versionNumber = "";
            if (ConfiguracionModel.isLANPermissionActivated())
                versionNumber = "LAN V " + MetodosGenerales.versionNumber;
            else versionNumber = "Web V " + MetodosGenerales.versionNumber;
            textVersionFrmLogin.Text = versionNumber;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            editUsernameFrmIniciarSesion.Enabled = false;
            editPassFrmIniciarSesion.Enabled = false;
            btnConfiguracionInicial.Enabled = false;
            checkBoxRecordarLogin.Enabled = false;
            btnLogin.Enabled = false;
            validateCredentials();
            Cursor = Cursors.Default;
        }

        public async Task validateCredentials()
        {
            String email = editUsernameFrmIniciarSesion.Text.Trim();
            String pass = editPassFrmIniciarSesion.Text.Trim();
            if (email.Equals(""))
            {
                FormMessage msj = new FormMessage("Datos Faltantes", "La clave de usuario no puede estar vacia!", 2);
                msj.ShowDialog();
                editUsernameFrmIniciarSesion.Enabled = true;
                editPassFrmIniciarSesion.Enabled = true;
                btnConfiguracionInicial.Enabled = true;
                checkBoxRecordarLogin.Enabled = true;
                btnLogin.Text = "Iniciar Sesión";
                btnLogin.Enabled = true;
            }
            else
            {
                if (pass.Equals(""))
                {
                    FormMessage msj = new FormMessage("Datos Faltantes", "La contraseña de usuario no puede estar vacia!", 2);
                    msj.ShowDialog();
                    editUsernameFrmIniciarSesion.Enabled = true;
                    editPassFrmIniciarSesion.Enabled = true;
                    btnConfiguracionInicial.Enabled = true;
                    checkBoxRecordarLogin.Enabled = true;
                    btnLogin.Text = "Iniciar Sesión";
                    btnLogin.Enabled = true;
                }
                else
                {
                    btnLogin.Text = "Verificando...";
                    await doLoginProcess(email, pass);
                }
            }
        }

        private async Task doLoginProcess(String email, String pass)
        {
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                if (ConfiguracionModel.isLANPermissionActivated())
                {
                    dynamic responseCajas = await CajaController.cajasExistLAN();
                    if (responseCajas.value >= 1)
                    {
                        if (cotmosAcitve)
                        {
                            value = 1;
                        } else
                        {
                            dynamic responseLogin = await UsersController.userLoginServerLAN(editUsernameFrmIniciarSesion.Text.Trim(), editPassFrmIniciarSesion.Text.Trim());
                            if (responseLogin.value >= 1)
                            {
                                dynamic responsePingLicense = await LicenseModel.validateIfLicenseServerIsAccesible();
                                if (responsePingLicense.value > 0)
                                {
                                    dynamic responseValidateLicense = await LicenseModel.validateLicense();
                                    if (responseValidateLicense.value > 0)
                                    {
                                        value = 1;
                                    }
                                    else
                                    {
                                        bool licenseActive = await clsGeneral.isTheLicenseValid(AES.Desencriptar(LicenseModel.getEndDateEncryptFromTheLocalDb()), "");
                                        if (!licenseActive)
                                        {
                                            description = "Su licencia ha expirado!";
                                        }
                                        else
                                        {
                                            value = 1;
                                        }
                                    }
                                }
                                else
                                {
                                    bool licenseActive = await clsGeneral.isTheLicenseValid(AES.Desencriptar(LicenseModel.getEndDateEncryptFromTheLocalDb()), "");
                                    if (!licenseActive)
                                    {
                                        description = "Su licencia ha expirado!";
                                    }
                                    else
                                    {
                                        value = 1;
                                    }
                                }
                            }
                            else
                            {
                                value = responseLogin.value;
                                description = responseLogin.description;
                            }
                        }
                    }
                    else
                    {
                        value = responseCajas.value;
                        description = responseCajas.description;
                    }
                }
                else
                {
                    dynamic responseCajas = await CajaController.cajasExistAPI();
                    if (responseCajas.value >= 1)
                    {
                        if (cotmosAcitve)
                        {
                            value = 1;
                        } else
                        {
                            dynamic responseLogin = await UsersController.userLoginServerAPI(editUsernameFrmIniciarSesion.Text.Trim(), editPassFrmIniciarSesion.Text.Trim());
                            if (responseLogin.value >= 1)
                            {
                                dynamic responsePingLicense = await LicenseModel.validateIfLicenseServerIsAccesible();
                                if (responsePingLicense.value > 0)
                                {
                                    dynamic responseValidateLicense = await LicenseModel.validateLicense();
                                    if (responseValidateLicense.value > 0)
                                    {
                                        value = 1;
                                    }
                                    else
                                    {
                                        bool licenseActive = await clsGeneral.isTheLicenseValid(AES.Desencriptar(LicenseModel.getEndDateEncryptFromTheLocalDb()), "");
                                        if (!licenseActive)
                                        {
                                            description = "Su licencia ha expirado!";
                                        }
                                        else
                                        {
                                            value = 1;
                                        }
                                    }
                                }
                                else
                                {
                                    bool licenseActive = await clsGeneral.isTheLicenseValid(AES.Desencriptar(LicenseModel.getEndDateEncryptFromTheLocalDb()), "");
                                    if (!licenseActive)
                                    {
                                        description = "Su licencia ha expirado!";
                                    }
                                    else
                                    {
                                        value = 1;
                                    }
                                }
                            }
                            else
                            {
                                value = responseLogin.value;
                                description = responseLogin.description;
                            }
                        }
                    }
                    else
                    {
                        value = responseCajas.value;
                        description = responseCajas.description;
                        if (cotmosAcitve)
                        {
                            value = 1;
                        } else
                        {
                            dynamic responseLogin = await UsersController.userLoginServerAPI(editUsernameFrmIniciarSesion.Text.Trim(), editPassFrmIniciarSesion.Text.Trim());
                            if (responseLogin.value >= 1)
                            {
                                dynamic responsePingLicense = await LicenseModel.validateIfLicenseServerIsAccesible();
                                if (responsePingLicense.value > 0)
                                {
                                    dynamic responseValidateLicense = await LicenseModel.validateLicense();
                                    if (responseValidateLicense.value > 0)
                                    {
                                        value = 1;
                                    }
                                    else
                                    {
                                        bool licenseActive = await clsGeneral.isTheLicenseValid(AES.Desencriptar(LicenseModel.getEndDateEncryptFromTheLocalDb()), "");
                                        if (!licenseActive)
                                        {
                                            description = "Su licencia ha expirado!";
                                        }
                                        else
                                        {
                                            value = 1;
                                        }
                                    }
                                }
                                else
                                {
                                    bool licenseActive = await clsGeneral.isTheLicenseValid(AES.Desencriptar(LicenseModel.getEndDateEncryptFromTheLocalDb()), "");
                                    if (!licenseActive)
                                    {
                                        description = "Su licencia ha expirado!";
                                    }
                                    else
                                    {
                                        value = 1;
                                    }
                                }
                            }
                            else
                            {
                                bool licenseActive = await clsGeneral.isTheLicenseValid(AES.Desencriptar(LicenseModel.getEndDateEncryptFromTheLocalDb()), "");
                                if (!licenseActive)
                                {
                                    description = "Su licencia ha expirado!";
                                }
                                else
                                {
                                    value = 1;
                                }
                            }
                        }
                    }
                }
            });
            if (value == 1)
            {
                await verifyUserLoggedIn(email, pass);
            } else
            {
                FormMessage formMessage = new FormMessage("Iniciando Sesión", description, 2);
                formMessage.ShowDialog();
                editUsernameFrmIniciarSesion.Enabled = true;
                editPassFrmIniciarSesion.Enabled = true;
                btnConfiguracionInicial.Enabled = true;
                checkBoxRecordarLogin.Enabled = true;
                btnLogin.Text = "Iniciar Sesión";
                btnLogin.Enabled = true;
            }
        }

        private async Task verifyUserLoggedIn(String email, String pass)
        {
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                if (cotmosAcitve)
                {
                    value = 1;
                } else
                {
                    UserModel.loginUserInLocalDb(email, pass, rememberLogin);
                    if (ClsRegeditController.getStatusLogin())
                    {
                        value = 1;
                    }
                    else
                    {
                        description = "Credenciales incorrectas, verifica tus datos, que el agente tenga asignada una caja o vuelve a " +
                        "validar conexión con el servidor!";
                    }
                }
            });
            if (value == 1)
            {
                doLogin = true;
                this.Visible = false;
                this.Close();
            } else
            {
                FormMessage msj = new FormMessage("Usuario No Encontrado", description, 2);
                msj.ShowDialog();
                editPassFrmIniciarSesion.Select();
            }
        }

        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnLogin_Click(sender, e);
            }
        }

        private void linkLabelActivateLicenseFrmIniciarSesion_Click(object sender, EventArgs e)
        {
            FrmActivacion activacion = new FrmActivacion();
            activacion.ShowDialog();
            Form1_Load(sender, e);
        }

        private void editUsernameFrmIniciarSesion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnLogin_Click(sender, e);
            }
        }

        private void checkBoxRecordarLogin_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRecordarLogin.Checked)
                rememberLogin = true;
            else rememberLogin = false;
        }

        private void FrmIniciarSesion_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (doLogin)
                {
                    this.Dispose();
                    this.Close();
                    FormPrincipal prueba = new FormPrincipal(cotmosAcitve);
                    prueba.ShowDialog();
                }
                else
                {
                    this.Dispose();
                    this.Close();
                }
            } catch (Exception ex)
            {
                SECUDOC.writeLog(ex.ToString());
            }
        }
    }
}


