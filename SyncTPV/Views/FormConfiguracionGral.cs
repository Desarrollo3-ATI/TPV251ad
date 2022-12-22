using Newtonsoft.Json.Linq;
using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using SyncTPV.Views;
using SyncTPV.Views.Extras;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;
using wsROMClases.Models;

namespace SyncTPV
{
    public partial class FormConfiguracionGral : Form
    {
        private bool usoBasculaActivated = false, webActive = false;
        private FormWaiting formWaiting;
        private bool serverModeLAN = false;

        public FormConfiguracionGral()
        {
            InitializeComponent();
            btnClose.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.back_white, 45, 45);
            serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
        }

        public async Task processToSendConfiguration()
        {
            dynamic response = null;
            await Task.Run(async () =>
            {
                if (ConfiguracionModel.isLANPermissionActivated())
                    response = await ConfigurationsTpvController.createOrUpdateConfigTPVAPI();
                else response = await ConfigurationsTpvController.createOrUpdateConfigTPVAPI();
            });
            if (response.value >= 1)
            {
                if (formWaiting != null)
                {
                    formWaiting.Dispose();
                    formWaiting.Close();
                }
            } else
            {
                if (formWaiting != null)
                {
                    formWaiting.Dispose();
                    formWaiting.Close();
                }
                FormMessage formMessage = new FormMessage("Exception", response.description, 2);
                formMessage.ShowDialog();
            }
        }

        private async void FrmConfiguracionGral_Load(object sender, EventArgs e)
        {
            showFormWaitingToGetPanelConfiguration();
            String Impresoras;
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                Impresoras = PrinterSettings.InstalledPrinters[i];
                cmbImpresoras.Items.Add(Impresoras);
                //comboInstalledPrinters.Items.Add(pkInstalledPrinters);
            }
            PrinterModel pm = PrinterModel.getallDataFromAPrinter();
            if (pm != null)
            {
                txtImpresora.Text = pm.nombre;
                if (pm.original.Equals(""))
                    editLeyendaOriginal.Text = "Original";
                else editLeyendaOriginal.Text = pm.original;
                if (pm.copia.Equals(""))
                    editLeyendaCopia.Text = "Copia";
                else editLeyendaCopia.Text = pm.copia;
            } else
            {
                editLeyendaOriginal.Text = "Original";
                editLeyendaCopia.Text = "Copia";
            }
            int numeroCopias = ConfiguracionModel.getNumCopias();
            editNumCopias.Text = "" + numeroCopias;
            // Nos traemos la configuración de la bascula 
            usoBasculaActivated = ConfiguracionModel.scalePermissionIsActivated();
            if (usoBasculaActivated)
            {
                checkBoxUsoBascula.Checked = true;
                cnombrebascula.Enabled = true;
                cPuerto.Enabled = true;
                cParidad.Enabled = true;
                cBitsdatos.Enabled = true;
                cBitsPadada.Enabled = true;
                cBitsxSeg.Enabled = true;
                // llena los campso de nuestra Form
                cnombrebascula.Text = ClsScaleModel.getallDataFromAScale().name;
                cPuerto.Text = ClsScaleModel.getallDataFromAScale().portname;
                cParidad.Text = Convert.ToString(ClsScaleModel.getallDataFromAScale().parity);
                cBitsdatos.Text = Convert.ToString(ClsScaleModel.getallDataFromAScale().data_bits);
                cBitsPadada.Text = Convert.ToString(ClsScaleModel.getallDataFromAScale().stop_bits);
                cBitsxSeg.Text = Convert.ToString(ClsScaleModel.getallDataFromAScale().baud_rate);
                checkBoxCapturaPesoManual.Enabled = true;
                bool capturaPesoManualActivated = ConfiguracionModel.isCapturaPesoManualPermissionActivated();
                if (capturaPesoManualActivated)
                    checkBoxCapturaPesoManual.Checked = true;
                else checkBoxCapturaPesoManual.Checked = false;
            }
            else
            {
                checkBoxUsoBascula.Checked = false;
                cnombrebascula.Enabled = false;
                cPuerto.Enabled = false;
                cParidad.SelectedIndex = 0;
                cParidad.Enabled = false;
                cParidad.SelectedIndex = 0;
                cBitsdatos.Enabled = false;
                cBitsdatos.SelectedIndex = 4;
                cBitsPadada.Enabled = false;
                cBitsPadada.SelectedIndex = 0;
                cBitsxSeg.Enabled = false;
                cBitsxSeg.SelectedIndex = 3;
                checkBoxCapturaPesoManual.Enabled = false;
                bool capturaPesoManualActivated = ConfiguracionModel.isCapturaPesoManualPermissionActivated();
                if (capturaPesoManualActivated)
                    checkBoxCapturaPesoManual.Checked = true;
                else checkBoxCapturaPesoManual.Checked = false;
            }
            validateUseFiscalProductField();
            validateCerrarCOM();
            if (PrinterModel.verifyIfAPrinterIdAdded())
                btnConfigTicket.Enabled = true;
            else btnConfigTicket.Enabled = false;
            fillNameForFiscalItemField();
            await getWebActiveValue();

            checkBoxVentaRapida.Checked = !Convert.ToBoolean(await validateVentarapida());
            pordefecto = true;
        }
        bool pordefecto = false;
        public async Task<int> validateVentarapida()
        {
            //checkBoxVentaRapida
            int ventarapida = 1;
            await Task.Run(async () =>
            {
                ventarapida = ConfiguracionModel.validateVentarapidaActivated();   
            });
            return ventarapida;
        }

        private async Task getWebActiveValue()
        {
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                dynamic responseConfig = ConfiguracionModel.webActive();
                if (responseConfig.value == 1)
                {
                    value = 1;
                    webActive = responseConfig.active;
                    if (webActive)
                        description = "Uso Web (Sí)";
                    else description = "Uso Web (No)";
                }
                else
                {
                    value = responseConfig.value;
                    description = responseConfig.description;
                }
            });
            if (value == 1)
            {
                checkBoxUsoWeb.Text = description;
                checkBoxUsoWeb.Checked = webActive;
            } else
            {
                FormMessage formMessage = new FormMessage("Web Activo", description, 3);
                formMessage.ShowDialog();
            }
        }

        private void showFormWaitingToGetPanelConfiguration()
        {
            formWaiting = new FormWaiting(this, 0, "Actualizando configuración del servidor...");
            formWaiting.ShowDialog();
        }

        public async Task getPanelConfigurationProcess()
        {
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                if (serverModeLAN)
                {
                    dynamic responseConfig = await PanelConfigurationController.getPanelConfigurationLAN();
                    if (responseConfig.value == 1)
                    {
                        ClsConfiguracionModel cm = responseConfig.cm;
                        dynamic responseUpdate = ConfiguracionModel.updatePositionFiscalItemField(cm.fiscalItemField);
                        if (responseUpdate.value == 1)
                        {
                            value = 1;
                        }
                        else
                        {
                            value = responseUpdate.value;
                            description = responseUpdate.description;
                        }
                    } else
                    {
                        value = responseConfig.value;
                        description = responseConfig.description;
                    }
                } else
                {
                    dynamic responseConfig = await PanelConfigurationController.getPanelConfigurationAPI();
                    if (responseConfig.value == 1)
                    {
                        ClsConfiguracionModel cm = responseConfig.cm;
                        dynamic responseUpdate = ConfiguracionModel.updatePositionFiscalItemField(cm.fiscalItemField);
                        if (responseUpdate.value == 1)
                        {
                            value = 1;
                        } else
                        {
                            value = responseUpdate.value;
                            description = responseUpdate.description;
                        }
                    }
                    else
                    {
                        value = responseConfig.value;
                        description = responseConfig.description;
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
                PopupNotifier popup = new PopupNotifier();
                popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                popup.TitleColor = Color.FromArgb(43, 143, 192);
                popup.TitleText = "Configuración";
                popup.TitlePadding = new Padding(5, 5, 5, 5);
                popup.ButtonBorderColor = Color.Red;
                popup.ContentText = "Configuración actualizada!";
                popup.ContentColor = Color.FromArgb(43, 143, 192);
                popup.HeaderHeight = 10;
                popup.AnimationDuration = 1000;
                popup.HeaderColor = Color.FromArgb(200, 244, 255);
                popup.Popup();
            } else
            {
                FormMessage formMessage = new FormMessage("Configuración", description, 3);
                formMessage.ShowDialog();
            }
        }

        private async Task fillNameForFiscalItemField()
        {
            int positionFiscalItemField = 0;
            await Task.Run(async () => {
                positionFiscalItemField = ConfiguracionModel.getPositionFiscalItemField();
            });
            String name = await ItemModel.getNameForFiscalItemField(positionFiscalItemField);
            editFiscalItemField.Text = name;
        }

        private async Task validateUseFiscalProductField()
        {
            bool useFical = false;
            await Task.Run(async () =>
            {
                useFical = ConfiguracionModel.useFiscalFieldValueActivated();
            });
            if (useFical)
                checkBoxFiscales.Checked = true;
            else checkBoxFiscales.Checked = false;
        }

        private async Task validateCerrarCOM()
        {
            bool close = false;
            await Task.Run(async () =>
            {
                close = ConfiguracionModel.isCerrarCOMActivated();
            });
            if (close)
                checkBoxCerrarCOM.Checked = true;
            else checkBoxCerrarCOM.Checked = false;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            formWaiting = new FormWaiting(this, 2, "Guardando Nueva configuración...");
            formWaiting.ShowDialog();
        }

        public async Task saveConfigurations()
        {
            await savePrinter();
        }

        private async Task savePrinter()
        {
            String printerName = txtImpresora.Text.Trim();
            String textoOriginal = editLeyendaOriginal.Text.Trim();
            String textoCopia = editLeyendaCopia.Text.Trim();
            String numeroCopiasText = editNumCopias.Text.Trim();
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                if (!printerName.Equals("") && !textoOriginal.Equals("") && !textoCopia.Equals(""))
                {
                    int numCopies = 1;
                    if (!numeroCopiasText.Equals(""))
                        numCopies = Convert.ToInt32(numeroCopiasText);
                    if (!PrinterModel.verifyIfAPrinterIdAdded())
                    {
                        dynamic responsePrinter = PrinterModel.saveANewPrinter(printerName, "", 1, 1, textoOriginal, textoCopia, 1, 1, 1, 1, 1, 1);
                        if (responsePrinter.value == 1)
                        {
                            dynamic responseCopias = ConfiguracionModel.updateNumeroCopias(numCopies);
                            if (responseCopias.value == 1)
                            {
                                clsTicket ticket = new clsTicket();
                                await ticket.PaginaPrueba(serverModeLAN);
                                value = 1;
                            } else
                            {
                                value = responseCopias.value;
                                description = responseCopias.description;
                            }
                        }
                        else
                        {
                            value = responsePrinter.value;
                            description = responsePrinter.description;
                        }
                    }
                    else
                    {
                        dynamic responsePrinter = PrinterModel.updatePrinter(printerName, "", 1, 1, textoOriginal, textoCopia);
                        if (responsePrinter.value == 1)
                        {
                            dynamic responseCopias = ConfiguracionModel.updateNumeroCopias(numCopies);
                            if (responseCopias.value == 1)
                            {
                                clsTicket ticket = new clsTicket();
                                await ticket.PaginaPrueba(serverModeLAN);
                                value = 1;
                            } else
                            {
                                value = responseCopias.value;
                                description = responseCopias.description;
                            }
                        }
                        else
                        {
                            value = responsePrinter.value;
                            description = responsePrinter.description;
                        }
                    }
                }
                else
                {
                    description = "Seleccione el nombre de la impresora, también agregar leyendar de original y copia";
                }
            });
            if (value == 1)
            {
                PopupNotifier popup = new PopupNotifier();
                popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                popup.TitleColor = Color.FromArgb(43, 143, 192);
                popup.TitleText = "Configuración";
                popup.TitlePadding = new Padding(5, 5, 5, 5);
                popup.ButtonBorderColor = Color.Red;
                popup.ContentText = "Información de la Impresora y número de copias actualizada correctamente!";
                popup.ContentColor = Color.FromArgb(43, 143, 192);
                popup.HeaderHeight = 10;
                popup.AnimationDuration = 1000;
                popup.HeaderColor = Color.FromArgb(200, 244, 255);
                popup.Popup();
                btnConfigTicket.Enabled = true;
                await saveBascula();
            } else
            {
                FormMessage formMessage = new FormMessage("Impresora No Encontrada", description, 2);
                formMessage.ShowDialog();
            }
        }

        private async Task saveBascula()
        {
            int value = 0;
            String description = "";
            String scaleName = cnombrebascula.Text.Trim();
            String puerto = cPuerto.Text.Trim();
            String bitsPorSeg = cBitsxSeg.Text.ToString().Trim();
            String paridad = cParidad.Text.ToString().Trim();
            String bitsDeDatos = cBitsdatos.Text.ToString().Trim();
            String bitsDeParada = cBitsPadada.Text.ToString().Trim();
            await Task.Run(async () =>
            {
                if (usoBasculaActivated)
                {
                    if (!scaleName.Equals(""))
                    {
                        dynamic responseScale = ClsScaleModel.verifyIfAScaleIdAdded();
                        if (responseScale.value == 1)
                        {
                            dynamic responseSaveScale = ClsScaleModel.updateScale(scaleName, puerto, Convert.ToInt32(bitsPorSeg),
                                Convert.ToInt32(paridad), Convert.ToInt32(bitsDeDatos),
                                Convert.ToDouble(bitsDeParada));
                            if (responseSaveScale.value == 1)
                            {
                                value = 1;
                                description = "Información de la báscula actualizada correctamente!";
                            }
                            else
                            {
                                value = responseSaveScale.value;
                                description = responseSaveScale.description;
                            }

                        }
                        else if (responseScale.value == 0)
                        {
                            dynamic responseSaveScale = ClsScaleModel.saveANewScale(scaleName, puerto, Convert.ToInt32(bitsPorSeg),
                                Convert.ToInt32(paridad), Convert.ToInt32(bitsDeDatos),
                                Convert.ToDouble(bitsDeParada));
                            if (responseSaveScale.value == 1)
                            {
                                value = 1;
                                description = "Información de la báscula actualizada correctamente!";
                            }
                            else
                            {
                                value = responseSaveScale.value;
                                description = responseSaveScale.description;
                            }
                        }
                        else
                        {
                            value = responseScale.value;
                            description = responseScale.description;
                        }
                    }
                    else
                    {
                        description = "Agregar el nombre de una báscula";
                    }
                }
                else
                {
                    value = 1;
                    description = "Configuración guardada correctamente!";
                }
            });
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            if (value == 1)
            {
                PopupNotifier popup = new PopupNotifier();
                popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                popup.TitleColor = Color.FromArgb(43, 143, 192);
                popup.TitleText = "Configuración";
                popup.TitlePadding = new Padding(5, 5, 5, 5);
                popup.ButtonBorderColor = Color.Red;
                popup.ContentText = description;
                popup.ContentColor = Color.FromArgb(43, 143, 192);
                popup.HeaderHeight = 10;
                popup.AnimationDuration = 1000;
                popup.HeaderColor = Color.FromArgb(200, 244, 255);
                popup.Popup();
            } else
            {
                FormMessage formMessage = new FormMessage("Báscula No Encontrada", description, 2);
                formMessage.ShowDialog();
            }
        }

        private async void pictureBox2_Click(object sender, EventArgs e)
        {
            GeneralTxt.Impresora = txtImpresora.Text;
            if (txtImpresora.Text != "")
            {
                clsTicket Ticket = new clsTicket();
                await Ticket.PaginaPrueba(serverModeLAN);
            }
        }

        private void cmbImpresoras_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtImpresora.Text = cmbImpresoras.SelectedItem.ToString();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxUsoBascula_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUsoBascula.Focused == true)
            {
                if (checkBoxUsoBascula.Checked == true)
                {
                    usoBasculaActivated = true;
                    formWaiting = new FormWaiting(this, 3, "Actualizando uso de báscula...");
                    formWaiting.ShowDialog();
                }
                else
                {
                    usoBasculaActivated = false;
                    formWaiting = new FormWaiting(this, 3, "Actualizando uso de báscula...");
                    formWaiting.ShowDialog();
                }
            }

        }

        public async Task changeUseScaleProcess()
        {
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                int activated = 0;
                if (usoBasculaActivated)
                    activated = 1;
                dynamic responseScale = ConfiguracionModel.updateUsoBascula(activated);
                if (responseScale.value == 1)
                {
                    value = 1;
                    if (activated == 1)
                        description = "Uso de báscula activado correctamente!";
                    else description = "Uso de báscula desactivado correctamente!";
                } else
                {
                    value = responseScale.value;
                    description = responseScale.description;
                    if (usoBasculaActivated) usoBasculaActivated = false;
                    else usoBasculaActivated = true;
                }
            });
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            if (value == 1)
            {
                PopupNotifier popup = new PopupNotifier();
                popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                popup.TitleColor = Color.FromArgb(43, 143, 192);
                popup.TitleText = "Uso de Báscula";
                popup.TitlePadding = new Padding(5, 5, 5, 5);
                popup.ButtonBorderColor = Color.Red;
                popup.ContentText = description;
                popup.ContentColor = Color.FromArgb(43, 143, 192);
                popup.HeaderHeight = 10;
                popup.AnimationDuration = 1000;
                popup.HeaderColor = Color.FromArgb(200, 244, 255);
                popup.Popup();
            } else
            {
                FormMessage formMessage = new FormMessage("Uso de Báscula", description, 3);
                formMessage.ShowDialog();
            }
            checkBoxUsoBascula.Checked = usoBasculaActivated;
            cnombrebascula.Enabled = usoBasculaActivated;
            cPuerto.Enabled = usoBasculaActivated;
            cParidad.Enabled = usoBasculaActivated;
            cBitsdatos.Enabled = usoBasculaActivated;
            cBitsPadada.Enabled = usoBasculaActivated;
            cBitsxSeg.Enabled = usoBasculaActivated;
        }

        private void cBitsxSeg_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxCapturaPesoManual_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCapturaPesoManual.Focused)
            {
                if (checkBoxCapturaPesoManual.Checked)
                {
                    String query = "UPDATE "+LocalDatabase.TABLA_CONFIGURACION+" SET "+LocalDatabase.CAMPO_CAPTURAPESOMANUAL_CONFIG+" = 1 WHERE "+
                        LocalDatabase.CAMPO_ID_CONFIGURACION+" = 1";
                    ConfiguracionModel.createUpdateOrDelete(query);
                } else
                {
                    String query = "UPDATE " + LocalDatabase.TABLA_CONFIGURACION + " SET " + LocalDatabase.CAMPO_CAPTURAPESOMANUAL_CONFIG + " = 0 WHERE " +
                        LocalDatabase.CAMPO_ID_CONFIGURACION + " = 1";
                    ConfiguracionModel.createUpdateOrDelete(query);
                }
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void editNumCopias_KeyPress(object sender, KeyPressEventArgs e)
        {
            char signo_decimal = (char)46;
            //Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == signo_decimal)
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan
                e.Handled = true;
            }
        }

        private void checkBoxFiscales_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFiscales.Focused)
            {
                if (checkBoxFiscales.Checked)
                {
                    updateFiscalesField(1);
                    PopupNotifier popup = new PopupNotifier();
                    popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                    popup.TitleColor = Color.FromArgb(43, 143, 192);
                    popup.TitleText = "Uso de productos de control Activado";
                    popup.TitlePadding = new Padding(5, 5, 5, 5);
                    popup.ButtonBorderColor = Color.Red;
                    popup.ContentText = "Se activó el manejo de productos comercial y de control";
                    popup.ContentColor = Color.FromArgb(43, 143, 192);
                    popup.HeaderHeight = 10;
                    popup.AnimationDuration = 1000;
                    popup.HeaderColor = Color.FromArgb(200, 244, 255);
                    popup.Popup();
                }
                else
                {
                    updateFiscalesField(0);
                    PopupNotifier popup = new PopupNotifier();
                    popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                    popup.TitleColor = Color.FromArgb(175, 82, 82);
                    popup.TitleText = "Uso de productos de control Desactivado";
                    popup.TitlePadding = new Padding(5, 5, 5, 5);
                    popup.ButtonBorderColor = Color.Red;
                    popup.ContentText = "Se desactivó el manejo de productos comerciales y de control";
                    popup.ContentColor = Color.FromArgb(175, 82, 82);
                    popup.HeaderHeight = 10;
                    popup.AnimationDuration = 1000;
                    popup.HeaderColor = Color.FromArgb(255, 200, 200);
                    popup.Popup();
                }
            }
        }

        private async Task updateFiscalesField(int value)
        {
            await Task.Run(async () =>
            {
                ConfiguracionModel.updateFiscalesField(value);
            });
        }

        private void checkBoxCerrarCOM_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCerrarCOM.Focused)
            {
                if (checkBoxCerrarCOM.Checked)
                {
                    ConfiguracionModel.updateCerrarCOM(1);
                    PopupNotifier popup = new PopupNotifier();
                    popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                    popup.TitleColor = Color.FromArgb(43, 143, 192);
                    popup.TitleText = "Cerrar COM Activado";
                    popup.TitlePadding = new Padding(5, 5, 5, 5);
                    popup.ButtonBorderColor = Color.Red;
                    popup.ContentText = "La conexióncon el puerto COM se realizará a cada ciclo de lectura";
                    popup.ContentColor = Color.FromArgb(43, 143, 192);
                    popup.HeaderHeight = 10;
                    popup.AnimationDuration = 1000;
                    popup.HeaderColor = Color.FromArgb(200, 244, 255);
                    popup.Popup();
                }
                else
                {
                    ConfiguracionModel.updateCerrarCOM(0);
                    PopupNotifier popup = new PopupNotifier();
                    popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                    popup.TitleColor = Color.FromArgb(175, 82, 82);
                    popup.TitleText = "Cerrar COM Desactivado";
                    popup.TitlePadding = new Padding(5, 5, 5, 5);
                    popup.ButtonBorderColor = Color.Red;
                    popup.ContentText = "El puerto COM solo se reconectará con el botón y al reiniciar la ventana de pesos";
                    popup.ContentColor = Color.FromArgb(175, 82, 82);
                    popup.HeaderHeight = 10;
                    popup.AnimationDuration = 1000;
                    popup.HeaderColor = Color.FromArgb(255, 200, 200);
                    popup.Popup();
                }
            }
        }

        private void btnConfigTicket_Click(object sender, EventArgs e)
        {
            FormConfigTickets formConfigTickets = new FormConfigTickets();
            formConfigTickets.StartPosition = FormStartPosition.CenterScreen;
            formConfigTickets.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtImpresora_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnGuardar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void checkBoxFiscales_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnConfigTicket_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void editNumCopias_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void editLeyendaOriginal_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void editLeyendaCopia_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void checkBoxUsoBascula_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void cnombrebascula_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void checkBoxCapturaPesoManual_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void editFiscalItemField_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void checkBoxUsoWeb_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUsoWeb.Focused)
            {
                if (checkBoxUsoWeb.Checked)
                {
                    webActive = true;
                    formWaiting = new FormWaiting(this, 4, "Activando conexión web...");
                    formWaiting.ShowDialog();
                } else
                {
                    webActive = false;
                    formWaiting = new FormWaiting(this, 4, "Desactivando conexión web...");
                    formWaiting.ShowDialog();
                }
            }
        }

        private void checkBoxUsoWeb_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void checkBoxVentaRapida_CheckedChanged(object sender, EventArgs e)
        {
            //bool cambio = checkBoxVentaRapida.Checked;
            if (pordefecto)
            {
                FormPasswordConfirmation fpc = new FormPasswordConfirmation("Autorización del Supervisor", "Ingresar Contraseña");
                fpc.StartPosition = FormStartPosition.CenterScreen;
                fpc.ShowDialog();
                if (FormPasswordConfirmation.permissionGranted)
                {
                    if (checkBoxVentaRapida.Checked)
                    {
                        String query = "UPDATE " + LocalDatabase.TABLA_CONFIGURACION + " SET " + LocalDatabase.CAMPO_VENTARAPIDA_CONFIG
                        + " = " + 0 + " WHERE id = 1";
                        ConfiguracionModel.updateServerMode(query);
                    }
                    else
                    {
                        String query = "UPDATE " + LocalDatabase.TABLA_CONFIGURACION + " SET " + LocalDatabase.CAMPO_VENTARAPIDA_CONFIG
                        + " = " + 1 + " WHERE id = 1";
                        ConfiguracionModel.updateServerMode(query);

                    }
                }
                else
                {
                    pordefecto= false;
                    checkBoxVentaRapida.Checked = !checkBoxVentaRapida.Checked;
                    pordefecto = true;
                }
            }
            
        }
        
        public async Task updateWebActiveProcess()
        {
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                int active = 0;
                if (webActive)
                    active = 1;
                dynamic responseWeb = ConfiguracionModel.updateWebActive(active);
                if (responseWeb.value == 1)
                {
                    value = 1;
                    if (webActive)
                        description = "Uso Web (Sí)";
                    else description = "Uso Web (No)";
                } else
                {
                    value = responseWeb.value;
                    description = responseWeb.description;
                    if (webActive) webActive = false;
                    else webActive = true;
                }
            });
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            if (value == 1)
            {
                checkBoxUsoWeb.Text = description;
                checkBoxUsoWeb.Checked = webActive;
                PopupNotifier popup = new PopupNotifier();
                popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                popup.TitleColor = Color.FromArgb(43, 143, 192);
                popup.TitleText = "Conexión Web";
                popup.TitlePadding = new Padding(5, 5, 5, 5);
                popup.ButtonBorderColor = Color.Red;
                popup.ContentText = description;
                popup.ContentColor = Color.FromArgb(43, 143, 192);
                popup.HeaderHeight = 10;
                popup.AnimationDuration = 1000;
                popup.HeaderColor = Color.FromArgb(200, 244, 255);
                popup.Popup();
                if (serverModeLAN)
                {
                    FormMessage formMessage = new FormMessage("Web Activo", "Activar o desactivar el modo de conexión vía " +
                        "Web (Internet) no aplica para las terminales configuradas en LAN (Area de Red Local).", 1);
                    formMessage.ShowDialog();
                }
            } else
            {
                FormMessage formMessage = new FormMessage("Conexión Web", description, 3);
                formMessage.ShowDialog();
            }
        }
    }
}
