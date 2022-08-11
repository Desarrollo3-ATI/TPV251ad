using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;
using wsROMClases.Models;
using wsROMClases.Models.Panel;
using static SyncTPV.Models.RegimenFiscalModel;
using static SyncTPV.Models.UsoCFDIModel;

namespace SyncTPV.Views.Customers
{
    public partial class FormAddCustomer : Form
    {
        private int idCustomer = 0;
        List<ClsEstadosModel> estadosList = null;
        List<ClsCiudadesModel> ciudadesList = null;
        List<UsoCFDIModel> usosCFDIList = null;
        List<RegimenFiscalModel> regimenFiscalList = null;
        private int idEstado = 0, idCiudad = 0;
        private bool serverModeLAN = false;
        private FormWaiting formWaiting;
        private String codigoEstado = "";

        public FormAddCustomer()
        {
            serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
            InitializeComponent();
        }

        private void FormAddCustomer_Load(object sender, EventArgs e)
        {
            validateIfStatesAndCitiesExist();
            fillComboBoxEstados();
            validateIfRegimenFiscalExist();
            validateIfUsoCFDIExist();
            if (idCustomer == 0)
            {
                btnDeleteCustomer.Visible = false;
            }
            comboBoxTipoContribuyente.SelectedIndex = 0;
        }

        private async Task validateIfStatesAndCitiesExist()
        {
            int estados = EstadosModel.getTotalStates();
            if (estados == 0)
            {
                formWaiting = new FormWaiting(this, 0, "Descargando Estados y Ciudades", false);
                formWaiting.StartPosition = FormStartPosition.CenterScreen;
                formWaiting.ShowDialog();
            }
            else
            {

            }
        }

        private void validateIfUsoCFDIExist()
        {
            int estados = UsoCFDIModel.getTotalUsoCFDI();
            if (estados == 0)
            {
                formWaiting = new FormWaiting(this, 2, "Datos uso CFDI", false);
                formWaiting.StartPosition = FormStartPosition.CenterScreen;
                formWaiting.ShowDialog();
            }
        }

        public async Task getAllUsosCFDIProcess()
        {
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                dynamic responseUsos = await UsoCFDIController.getAllUsosCFDIAPI();
                if (responseUsos.value == 1)
                {
                    value = 1;
                } else
                {
                    value = responseUsos.value;
                    description = responseUsos.description;
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
                popup.TitleText = "Usos de CFDI";
                popup.TitlePadding = new Padding(5, 5, 5, 5);
                popup.ButtonBorderColor = Color.Red;
                popup.ContentText = "Usos de CFDI actualizados correctamente";
                popup.ContentColor = Color.FromArgb(43, 143, 192);
                popup.HeaderHeight = 10;
                popup.AnimationDuration = 1000;
                popup.HeaderColor = Color.FromArgb(200, 244, 255);
                popup.Popup();
            } else
            {
                FormMessage formMessage = new FormMessage("Usos CFDI", description, 3);
                formMessage.ShowDialog();
            }
        }

        private void validateIfRegimenFiscalExist()
        {
            int estados = RegimenFiscalModel.getTotalRegimenFiscal();
            if (estados == 0)
            {
                formWaiting = new FormWaiting(this, 3, "Datos regimen fiscal", false);
                formWaiting.StartPosition = FormStartPosition.CenterScreen;
                formWaiting.ShowDialog();
            }
        }

        public async Task getAllRegimienesFiscalesProcess()
        {
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                dynamic responseRegimen = await RegimenFiscalController.getAllRegimenFiscalAPI();
                if (responseRegimen.value == 1)
                {
                    value = 1;
                }
                else
                {
                    value = responseRegimen.value;
                    description = responseRegimen.description;
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
                popup.TitleText = "Régimen Fiscal";
                popup.TitlePadding = new Padding(5, 5, 5, 5);
                popup.ButtonBorderColor = Color.Red;
                popup.ContentText = "Regímenes fiscales actualizados correctamente";
                popup.ContentColor = Color.FromArgb(43, 143, 192);
                popup.HeaderHeight = 10;
                popup.AnimationDuration = 1000;
                popup.HeaderColor = Color.FromArgb(200, 244, 255);
                popup.Popup();
            }
            else
            {
                FormMessage formMessage = new FormMessage("Régimen Fiscal", description, 3);
                formMessage.ShowDialog();
            }
        }

        public async Task downloadStatesAndCities()
        {
            if (serverModeLAN)
            {
                if (formWaiting != null)
                {
                    formWaiting.Dispose();
                    formWaiting.Close();
                }
            }
            else
            {
                dynamic response = await EstadosController.downloadAllEstados();
                if (response.value >= 1)
                {
                    response = await CiudadesController.downloadAllCities();
                    if (response.value >= 1)
                    {
                        if (formWaiting != null)
                        {
                            formWaiting.Dispose();
                            formWaiting.Close();
                        }
                    } else if (!response.value == 0)
                    {
                        if (formWaiting != null)
                        {
                            formWaiting.Dispose();
                            formWaiting.Close();
                        }
                        FormMessage formMessage = new FormMessage("Datos Faltantes", response.description, 3);
                        formMessage.ShowDialog();
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
                } else if (response.value == 0)
                {
                    if (formWaiting != null)
                    {
                        formWaiting.Dispose();
                        formWaiting.Close();
                    }
                    FormMessage formMessage = new FormMessage("Datos Faltantes", "Tienes que validar que la base de datos CATSAT se encuentre creada y " +
                        "en la instancia del PanelROM", 3);
                    formMessage.ShowDialog();
                }
                else
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
        }

        private async Task fillComboBoxEstados()
        {
            estadosList = await getAllEstados();
            if (estadosList != null)
            {
                //Setup data binding
                this.comboBoxEstados.DataSource = estadosList;
                this.comboBoxEstados.ValueMember = "id";
                this.comboBoxEstados.DisplayMember = "Nombre";
                // make it readonly
                this.comboBoxEstados.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBoxEstados.SelectedIndex = -1;
            }
        }

        private async Task fillComboBoxUsoCFDI()
        {
            usosCFDIList = UsoCFDIModel.getLocalUsoCFDI();
            if (usosCFDIList != null)
            {
                //Setup data binding
                this.comboBoxUsoCFDI.DataSource = usosCFDIList;
                this.comboBoxUsoCFDI.ValueMember = "valor";
                this.comboBoxUsoCFDI.DisplayMember = "Nombre";
                // make it readonly
                this.comboBoxUsoCFDI.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBoxUsoCFDI.SelectedIndex = 0;
            }
        }

        private async Task fillComboBoxRegimenFiscal()
        {
            regimenFiscalList = RegimenFiscalModel.getLocalRegimenFiscal();
            if (regimenFiscalList != null)
            {
                this.comboBoxRegimenFiscal.DataSource = regimenFiscalList;
                this.comboBoxRegimenFiscal.ValueMember = "valor";
                this.comboBoxRegimenFiscal.DisplayMember = "Nombre";
                // make it readonly
                this.comboBoxRegimenFiscal.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBoxRegimenFiscal.SelectedIndex = 0;
            }

        }

        private async Task<List<ClsEstadosModel>> getAllEstados()
        {
            List<ClsEstadosModel> estadosList = null;
            await Task.Run(async () =>
            {
                if (serverModeLAN)
                {
                    dynamic response = await EstadosController.getAllEstadosLAN();
                    if (response.value >= 1)
                    {
                        estadosList = response.estadosList;
                    }
                } else
                {
                    String query = "SELECT * FROM " + LocalDatabase.TABLA_ESTADOS;
                    estadosList = EstadosModel.getAllEstados(query);
                }
            });
            return estadosList;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormAddCustomer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (idCustomer != 0)
            {

            }
            else
            {
                String customerName = editNombre.Text.Trim();
                if (!customerName.Equals(""))
                {
                    if (idCustomer == 0)
                    {
                        showDialogConfirmation();
                        if (!FrmConfirmation.confirmation)
                        {
                            e.Cancel = true;
                        }
                    }
                    else
                    {

                    }
                }
            }
        }

        private void showDialogConfirmation()
        {
            FrmConfirmation formMessage = new FrmConfirmation("Cancelar", "Está seguro de salir sin guardar la información");
            formMessage.ShowDialog();
        }

        private void btnSaveCustomer_Click(object sender, EventArgs e)
        {
            if (idCustomer != 0)
            {
                if (CustomerADCModel.isTheCustomerSendedByIdPanel(idCustomer))
                {
                    FormMessage formMessage = new FormMessage("Cliente Enviado", "No puedes actualizar al cliente en turno ya que está enviado al servidor", 3);
                    formMessage.ShowDialog();
                } else
                {
                    formWaiting = new FormWaiting(this, 1, "Guardando Cliente", true);
                    formWaiting.ShowDialog();
                    //processToAddOrUpdateNewcustomer(true);
                }
            } else
            {
                formWaiting = new FormWaiting(this, 1, "Guardando Cliente", false);
                formWaiting.ShowDialog();
                //processToAddOrUpdateNewcustomer(false);
            }
        }

        public async Task processToAddOrUpdateNewcustomer(bool editing)
        {
            int value = 0;
            String description = "";
            String nombre = editNombre.Text.Trim();
            String calle = editNombreCalle.Text.Trim();
            String numeroExt = editNumeroExterior.Text.Trim();
            String colonia = editColonia.Text.Trim();
            String poblacion = editPoblacion.Text.Trim();
            String referencia = editReferencia.Text.Trim();
            String telefono = editTelefono.Text.Trim();
            String cp = editCodigoPostal.Text.Trim();
            String email = editEmail.Text.Trim();
            String rfc = editRfc.Text.Trim();
            int tipoContribuyente = comboBoxTipoContribuyente.SelectedIndex;
            String codigoRegimenFiscal = comboBoxRegimenFiscal.SelectedValue.ToString();
            String codigoUsoCFDI = comboBoxUsoCFDI.SelectedValue.ToString();
            await Task.Run(async () =>
            {
                if (nombre.Equals(""))
                    description = "Tienes que agregar el nombre del cliente";
                else
                {
                    if (calle.Equals(""))
                        description = "Tienes que agregar el nombre de la calle";
                    else
                    {
                        if (numeroExt.Equals(""))
                            description = "Tienes que agregar el número exterior";
                        else
                        {
                            if (colonia.Equals(""))
                                description = "Tienes que agregar la colonia, barrio, fraccionamiento, etc.";
                            else
                            {
                                if (telefono.Equals(""))
                                    description = "Tienes que agregar algún teléfono del cliente";
                                else
                                {
                                    if (cp.Equals(""))
                                        description = "Tienes que agregar el código postal";
                                    else
                                    {
                                        if (idEstado == 0 || idCiudad == 0)
                                            description = "Tienes que seleccionar un estado y una ciudad válida";
                                        else
                                        {
                                            if (rfc.Equals(""))
                                                description = "Tienes que agregar el RFC del cliente";
                                            else
                                            {
                                                if (tipoContribuyente == -1)
                                                {
                                                    description = "Tipo contribuyente es obligatorio";
                                                }
                                                else
                                                {
                                                    if (codigoRegimenFiscal.Equals(""))
                                                    {
                                                        description = "El regimen fiscal debe ser seleccionado";
                                                    }
                                                    else
                                                    {
                                                        if (codigoUsoCFDI.Equals(""))
                                                        {
                                                            description = "El uso CFDI debe ser seleccionado";
                                                        }
                                                        else
                                                        {
                                                            value = 1;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            });
            if (value == 1)
            {
                if (editing)
                {
                    int response = await logicToUpdateNewCustomer(nombre, calle, numeroExt, colonia, poblacion, referencia,
                    telefono, cp, email, rfc, tipoContribuyente, codigoRegimenFiscal, codigoUsoCFDI);
                    if (response != 0)
                    {
                        if (formWaiting != null)
                        {
                            formWaiting.Dispose();
                            formWaiting.Close();
                        }
                        idCustomer = response;
                        FormMessage formMessage = new FormMessage("Cliente Actualizado Correctamente", "Para que el cliente creado se enlace correctamente al sistema de comercial necesitas crearle un documento.", 1);
                        formMessage.ShowDialog();
                        if (idCustomer != 0)
                        {
                            btnSaveCustomer.Text = "Actualizar";
                            btnDeleteCustomer.Visible = true;
                            btnDeleteCustomer.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.delete_white, 40, 40);
                        }
                    } else
                    {
                        if (formWaiting != null)
                        {
                            formWaiting.Dispose();
                            formWaiting.Close();
                        }
                        FormMessage formMessage = new FormMessage("Cliente No Creado Correctamente", "Algo falló al crear al nuevo cliente", 3);
                        formMessage.ShowDialog();
                    }
                }
                else
                {
                    int response = await logicToAddNewCustomer(nombre, calle, numeroExt, colonia, poblacion, referencia,
                    telefono, cp, email, rfc, tipoContribuyente, codigoRegimenFiscal, codigoUsoCFDI);
                    if (response != 0)
                    {
                        if (formWaiting != null)
                        {
                            formWaiting.Dispose();
                            formWaiting.Close();
                        }
                        idCustomer = response;
                        FormMessage formMessage = new FormMessage("Cliente Creado Correctamente", "Para que el cliente creado se enlace correctamente al sistema de comercial necesitas crearle un documento.", 1);
                        formMessage.ShowDialog();
                        if (idCustomer != 0)
                        {
                            btnSaveCustomer.Text = "Actualizar";
                            btnDeleteCustomer.Visible = true;
                            btnDeleteCustomer.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.delete_white, 40, 40);
                        }
                    } else
                    {
                        if (formWaiting != null)
                        {
                            formWaiting.Dispose();
                            formWaiting.Close();
                        }
                        FormMessage formMessage = new FormMessage("Cliente No Creado Correctamente", "Algo falló al crear al nuevo cliente", 3);
                        formMessage.ShowDialog();
                    }
                }
            } else
            {
                if (formWaiting != null)
                {
                    formWaiting.Dispose();
                    formWaiting.Close();
                }
                FormMessage formMessage = new FormMessage("Datos Faltantes", description, 3);
                formMessage.ShowDialog();
            }
        }

        private async Task<int> logicToAddNewCustomer(String nombre, String calle, String numeroExt, String colonia, String poblacion, 
            String referencia, String telefono, String cp, String email, String rfc, int tipoContribuyente,
            String codigoRegimenFiscal, String codigoUsoCFDI)
        {
            int response = 0;
            int idCreated = CustomerADCModel.createANewCustomerADC(nombre, calle, numeroExt, colonia, idEstado.ToString(), idCiudad.ToString(),
                                            poblacion, referencia, telefono, cp, email, rfc, tipoContribuyente,
                                            codigoRegimenFiscal, codigoUsoCFDI);
            if (idCreated > 0)
            {
                String idClienteNegativo = "-" + idCreated;
                CustomerModel.createNewCustomer(Convert.ToInt32(idClienteNegativo), nombre, "", 0, "", "1", "",
                    telefono, 1, 0, "", referencia, "", 0, "Bueno", "", 0, 0, 0, "", rfc, "", "",0,"","");
                if (CustomerModel.updateClaveClienteAdditionalAgregado(Convert.ToInt32(idClienteNegativo)) > 0)
                {
                    response = Convert.ToInt32(idClienteNegativo);
                }
            }
            return response;
        }

        private async Task<int> logicToUpdateNewCustomer(String nombre, String calle, String numeroExt, String colonia, String poblacion,
            String referencia, String telefono, String cp, String email, String rfc, int tipoContribuyente,
            String codigoRegimenFiscal, String codigoUsoCFDI)
        {
            int idUpdated = CustomerADCModel.updateANewCustomerADC(Math.Abs(idCustomer), nombre, calle, numeroExt, colonia, idEstado.ToString(), idCiudad.ToString(),
                                            poblacion, referencia, telefono, cp, email, rfc, tipoContribuyente,
                                            codigoRegimenFiscal, codigoUsoCFDI);
            if (idUpdated > 0)
            {
                String idClienteNegativo = "-" + idUpdated;
                CustomerModel.updateNewCustomer(Convert.ToInt32(idClienteNegativo), nombre, "", 0, "", "1", "",
                    telefono, 1, 0, "", referencia, "", 0, "Bueno", "", 0, 0, 0, "", rfc, "", "",
                    tipoContribuyente, codigoRegimenFiscal, codigoUsoCFDI);
                if (CustomerModel.updateClaveClienteAdditionalAgregado(Convert.ToInt32(idClienteNegativo)) > 0)
                {
                    idUpdated = Convert.ToInt32(idClienteNegativo);
                }
            }
            return idUpdated;
        }

        private void comboBoxEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEstados.Focused)
            {
                if (comboBoxEstados.SelectedIndex >= 0)
                {
                    idEstado = Convert.ToInt32(comboBoxEstados.SelectedValue.ToString().Trim());
                    fillComboBoxCiudades(idEstado);
                }
            }
        }

        private async Task fillComboBoxCiudades(int estadoId)
        {
            dynamic responseCities = await getAllCitiesByEstado(estadoId);
            if (responseCities.value == 1)
            {
                ciudadesList = responseCities.ciudadesList;
                if (ciudadesList != null)
                {
                    //Setup data binding
                    this.comboBoxCiudades.DataSource = ciudadesList;
                    this.comboBoxCiudades.ValueMember = "CIUDAD_ID";
                    this.comboBoxCiudades.DisplayMember = "NOMBRE";
                    // make it readonly
                    this.comboBoxCiudades.DropDownStyle = ComboBoxStyle.DropDownList;
                    comboBoxCiudades.SelectedIndex = -1;
                    comboBoxCiudades.Enabled = true;
                }
                else
                {
                    comboBoxCiudades.Enabled = false;
                }
            } else
            {
                FormMessage formMessage = new FormMessage("Ciudades", responseCities.description, 3);
                formMessage.ShowDialog();
            }
        }

        private void comboBoxCiudades_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCiudades.Focused)
            {
                if (comboBoxCiudades.SelectedIndex >= 0)
                {
                    idCiudad = Convert.ToInt32(comboBoxCiudades.SelectedValue.ToString().Trim());
                }
            }
        }

        private void editTelefono_KeyPress(object sender, KeyPressEventArgs e)
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

        private void editCodigoPostal_KeyPress(object sender, KeyPressEventArgs e)
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
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (idCustomer != 0)
                {
                    if (CustomerADCModel.isTheCustomerSendedByIdPanel(idCustomer))
                    {
                        FormMessage formMessage = new FormMessage("Cliente Enviado", "No puedes actualizar al cliente en turno ya que está enviado al servidor", 3);
                        formMessage.ShowDialog();
                    }
                    else
                    {
                        processToAddOrUpdateNewcustomer(true);
                    }
                }
                else
                {
                    processToAddOrUpdateNewcustomer(false);
                }
            }
        }

        private async Task<ExpandoObject> getAllCitiesByEstado(int estadoId)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            List<ClsCiudadesModel> ciudadesList = null;
            await Task.Run(async () =>
            {
                try
                {
                    if (serverModeLAN)
                    {
                        String codigoEstado = "";
                        if (estadoId < 10)
                            codigoEstado = "0" + estadoId;
                        else codigoEstado = estadoId.ToString();
                        dynamic responseCities = await CiudadesController.getAllCitiesByEstadoLAN(codigoEstado);
                        if (responseCities.value == 1)
                        {
                            value = 1;
                            ciudadesList = responseCities.citiesList;
                        } else
                        {
                            value = responseCities.value;
                            description = responseCities.description;
                        }
                    }
                    else
                    {
                        String query = "SELECT * FROM " + LocalDatabase.TABLA_CIUDADES + " WHERE " +
                    LocalDatabase.CAMPO_ESTADOID_CIUDAD + " = " + estadoId;
                        ciudadesList = CiudadesModel.getAllCiudades(query);
                        if (ciudadesList != null && ciudadesList.Count > 0)
                            value = 1;
                        else description = "No se encontró ninguna Ciudad en la base de datos local, Actualizar Datos!";
                    }
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    value = -1;
                    description = e.Message;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                    response.ciudadesList = ciudadesList;
                }
            });
            return response;
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (CustomerADCModel.isTheCustomerSendedByIdPanel(Math.Abs(idCustomer)))
            {
                FormMessage formMessage = new FormMessage("Acción No Permitida", "El cliente ya fue sincronizado al servidor, no podemos eliminarlo", 3);
                formMessage.ShowDialog();
            }
            else
            {
                FrmConfirmation frmConfirmation = new FrmConfirmation("Eliminar Cliente", "¿Deseas eliminar el cliente nuevo y sus documentos?");
                frmConfirmation.StartPosition = FormStartPosition.CenterScreen;
                frmConfirmation.ShowDialog();
                if (FrmConfirmation.confirmation)
                {
                    CustomerADCModel.deleteClienteNuevoConSusDocumentos(idCustomer);
                    idCustomer = 0;
                    resetearVentana();
                }
            }
        }

        private void resetearVentana()
        {
            editNombre.Text = "";
            editNombreCalle.Text = "";
            editNumeroExterior.Text = "";
            editColonia.Text = "";
            comboBoxEstados.SelectedIndex = -1;
            comboBoxCiudades.SelectedIndex = -1;
            editPoblacion.Text = "";
            editReferencia.Text = "";
            editTelefono.Text = "";
            editCodigoPostal.Text = "";
            editEmail.Text = "";
            editRfc.Text = "";
            btnDeleteCustomer.Visible = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxTipoContribuyente_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxUsoCFDI.DataSource = null;
            comboBoxRegimenFiscal.DataSource = null;
            comboBoxRegimenFiscal.Items.Clear();
            comboBoxUsoCFDI.Items.Clear();
            String tipoContribuyenteSelected = comboBoxTipoContribuyente.SelectedItem.ToString();
            if (tipoContribuyenteSelected.Equals("Persona Física"))
            {
                tipoContribuyenteSelected = "fisica";
            }
            else
            {
                tipoContribuyenteSelected = "moral";
            }
            regimenFiscalList = RegimenFiscalModel.getLocalFitroRegimenFiscal(tipoContribuyenteSelected);
            if (regimenFiscalList != null)
            {
                this.comboBoxRegimenFiscal.DataSource = regimenFiscalList;
                this.comboBoxRegimenFiscal.ValueMember = "valor";
                this.comboBoxRegimenFiscal.DisplayMember = "Nombre";
                // make it readonly
                this.comboBoxRegimenFiscal.DropDownStyle = ComboBoxStyle.DropDownList;
            }

            usosCFDIList = UsoCFDIModel.getLocalFitroUsoCFDI(tipoContribuyenteSelected);
            if (usosCFDIList != null)
            {
                this.comboBoxUsoCFDI.DataSource = usosCFDIList;
                this.comboBoxUsoCFDI.ValueMember = "valor";
                this.comboBoxUsoCFDI.DisplayMember = "Nombre";
                // make it readonly
                this.comboBoxUsoCFDI.DropDownStyle = ComboBoxStyle.DropDownList;
            }

        }

        private void comboBoxRegimenFiscal_SelectedIndexChanged(object sender, EventArgs e)
        {
             
        }

        private void comboBoxUsoCFDI_SelectedIndexChanged(object sender, EventArgs e)
        {
            //idUsoCFDI = comboBoxUsoCFDI.SelectedValue.ToString().Trim();
        }

        private bool rfcValida(string curp)
        {
            var re = @"/^([A-ZÑ&]{3,4}) ?(?:- ?)?(\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])) ?(?:- ?)?([A-Z\d]{2})([A\d])$/";
            Regex rx = new Regex(re, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var validado = rx.IsMatch(curp);

            if (!validado)  //Coincide con el formato general?
                return false;

            //Validar que coincida el dígito verificador
            //if (!curp.EndsWith(DigitoVerificador(curp.ToUpper())))
                //return false;

            return true; //Validado
        }

    }
}
