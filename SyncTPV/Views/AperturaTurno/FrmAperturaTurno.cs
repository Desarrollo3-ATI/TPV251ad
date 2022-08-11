using Microsoft.Toolkit.Uwp.Notifications;
using SyncTPV.Controllers;
using SyncTPV.Controllers.Downloads;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace SyncTPV.Views.AperturaTurno
{
    public partial class FrmAperturaTurno : Form
    {
        FormPrincipal formPrincipal = null;
        private int idUser = 0;
        private bool serverModeLAN = false, cotmosActive = false;
        private String panelInstance = "", codigoCaja = "";

        public FrmAperturaTurno(FormPrincipal formPrincipal, bool cotmosActive)
        {
            InitializeComponent();
            this.formPrincipal = formPrincipal;
            this.cotmosActive = cotmosActive;
        }

        private async void FrmAperturaTurno_Load(object sender, EventArgs e)
        {
            editImporte.Text = 0 + "";
            editImporte.Select();
            await loadInitialData();
        }

        private async Task loadInitialData()
        {
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                idUser = ClsRegeditController.getIdUserInTurn();
                serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
                panelInstance = InstanceSQLSEModel.getStringPanelInstance();
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

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            doCheckoutOpening();
        }

        private async Task doCheckoutOpening()
        {
            dynamic response = await doCheckoutOpeningProcess();
            validateResponseDoCheckoutOpeningProcess(response);
        }

        private async Task<ExpandoObject> doCheckoutOpeningProcess()
        {
            dynamic response = new ExpandoObject(); ;
            int value = 0;
            String description = "";
            double amount = Convert.ToDouble(editImporte.Text.Trim());
            await Task.Run(async () => {
                string dateNow = DateTime.Now.ToString("yyyyMMdd");
                string dateTimeNow = DateTime.Now.ToString("dd-MM-yyyy H:mm:ss");
                if (serverModeLAN)
                {
                    dynamic responseServer = await AperturaTurnoController.insertCheckoutOpeningToServerLAN(panelInstance,
                        idUser, amount, dateTimeNow, dateNow, codigoCaja);
                    if (responseServer.value > 0)
                    {
                        value = responseServer.value;
                        description = responseServer.description;
                    } else
                    {
                        value = responseServer.value;
                        description = responseServer.description;
                    }
                } else
                {
                    int lastId = AperturaTurnoModel.getLastId();
                    lastId++;
                    String queryExist = "SELECT " + LocalDatabase.CAMPO_ID_APERTURATURNO + " FROM " + LocalDatabase.TABLA_APERTURATURNO + " WHERE " +
                    LocalDatabase.CAMPO_CREATEDAT_APERTURATURNO + " = '" + dateNow + "' AND " + LocalDatabase.CAMPO_USERID_APERTURATURNO + " = " +
                    ClsRegeditController.getIdUserInTurn();
                    int exist = AperturaTurnoModel.getIntValue(queryExist);
                    if (exist > 0)
                    {
                        value = 2;
                        description = "Apertura de Caja realizado para este agente, intenta con otro agente!";
                    }
                    else
                    {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_APERTURATURNO + " VALUES(" + lastId + ", " + idUser + ", " + amount + ", '" + dateTimeNow + "', '" + dateNow + "', 0, 0)";
                        int records = AperturaTurnoModel.createUpdateOrDelete(query);
                        if (records > 0)
                        {
                            value = 1;
                        }
                        else
                        {
                            value = 0;
                            description = "Algo falló al agregar la apertura de turno";
                        }
                    }
                }
                response.value = value;
                response.description = description;
            });
            return response;
        }

        private async Task validateResponseDoCheckoutOpeningProcess(dynamic response)
        {
            if (serverModeLAN)
            {
                if (response.value > 0)
                {
                    await ClsInitialChargeController.doIndividualDownloadProcess(null, formPrincipal, 2, 22, 0, serverModeLAN, codigoCaja);
                    // Requires Microsoft.Toolkit.Uwp.Notifications NuGet package version 7.0 or greater
                    new ToastContentBuilder()
                        .AddArgument("action", "viewConversation")
                        .AddArgument("conversationId", 9813)
                        // Inline image
                        //.AddInlineImage(new Uri("https://picsum.photos/360/202?image=883"))
                        // Profile (app logo override) image
                        .AddAppLogoOverride(new Uri("ms-appdata:" + Properties.Resources.synctpvlogo), ToastGenericAppLogoCrop.Circle)
                        .AddText("Apertura de Turno")
                        .AddText("La apertura de caja se realizó correctamente para el usuario en turno!")
                        .Show(); // Not seeing the Show() method? Make sure you have version 7.0, and if you're using .NET 5, your TFM must be net5.0-windows10.0.17763.0 or greater
                    this.Close();
                } else
                {
                    FormMessage formMessage = new FormMessage("Exception", response.description, 2);
                    formMessage.ShowDialog();
                    this.Close();
                }
            } else
            {
                if (response.value == 1)
                {
                    await ClsInitialChargeController.doIndividualDownloadProcess(null, formPrincipal, 2, 22, 0, serverModeLAN, codigoCaja);
                    AperturaTurnoModel apertura = await AperturaTurnoController.completeJSONValue(idUser);
                    if (apertura == null)
                    {
                        FormMessage formMessage = new FormMessage("Exception", "No pudimos encontrar la información de apertura de turno", 2);
                        formMessage.ShowDialog();
                    }
                    else
                    {
                        dynamic responseServer = await AperturaTurnoController.insertCheckoutOpeningToServer(apertura, codigoCaja);
                        if (responseServer.value == 1)
                        {
                            // Requires Microsoft.Toolkit.Uwp.Notifications NuGet package version 7.0 or greater
                            new ToastContentBuilder()
                                .AddArgument("action", "viewConversation")
                                .AddArgument("conversationId", 9813)
                                // Inline image
                                //.AddInlineImage(new Uri("https://picsum.photos/360/202?image=883"))
                                // Profile (app logo override) image
                                .AddAppLogoOverride(new Uri("ms-appdata:" + Properties.Resources.synctpvlogo), ToastGenericAppLogoCrop.Circle)
                                .AddText("Apertura de Turno")
                                .AddText("La apertura de caja se realizó correctamente para el usuario en turno!")
                                .Show(); // Not seeing the Show() method? Make sure you have version 7.0, and if you're using .NET 5, your TFM must be net5.0-windows10.0.17763.0 or greater
                        }
                        else if (responseServer.value == -2)
                        {
                            FormMessage frmMessage = new FormMessage("Exception", "Datos sincronizados al servidor pero la respuesta es incorrecta", 2);
                            frmMessage.ShowDialog();
                        }
                        else if (responseServer.value == -404)
                        {
                            FormMessage frmMessage = new FormMessage("Exception", "Tiempo Excedido no pudimos encontrar el servidor", 2);
                            frmMessage.ShowDialog();
                        }
                        else if (responseServer.value == -500)
                        {
                            FormMessage frmMessage = new FormMessage("Exception", "Algo falló en el servidor", 2);
                            frmMessage.ShowDialog();
                        }
                        else
                        {
                            FormMessage frmMessage = new FormMessage("Exception", "Algo falló al enviar y recibir datos al servidor. "+
                                responseServer.description, 2);
                            frmMessage.ShowDialog();
                        }
                        this.Close();
                    }
                }
                else if (response.value == 2)
                {
                    FormMessage fm = new FormMessage("Turno Vigente", ""+response.description, 1);
                    fm.ShowDialog();
                    this.Close();
                }
                else
                {
                    FormMessage fm = new FormMessage("Advertencia", ""+ response.description, 2);
                    fm.ShowDialog();
                }
            }
        }

        private void editImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            char signo_decimal = (char)46;
            //Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == signo_decimal)
            {
                e.Handled = false;
            }
            else
              if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan
                e.Handled = true;
            }
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                doCheckoutOpening();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static void closeThisWindow(FrmAperturaTurno fat)
        {
            fat.Close();
        }
    }
}
