using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace SyncTPV.Views.Scale
{
    public partial class FormCalculateWeight : Form
    {
        private readonly int TIPO_PESOBRUTO = 0, TIPO_PESOTARA = 1, TIPO_PESONETO = 2;
        private delegate void DelegadoAcceso(string accion);
        SerialPort serialPortScale = null;
        private int movementId = 0, idDocumento = 0;
        private String puertoCom = "";
        private int baudRate = 2400, dataBits = 8;
        private Parity parityValue = Parity.None;
        private StopBits stopBitsValue = StopBits.One;
        private FormVenta frmVentaNew;
        private bool ciclarLectura = true;
        private String kiloGramos = "";
        private String kilos = "", gramos = "";
        private FormWaiting formWaiting;
        private List<TaraModel> tarasList = null;
        private bool leerPesoDeBascula = true, reseteandoBascula = false;

        public FormCalculateWeight(FormVenta frmVentaNew, int idDocumento, int movementId)
        {
            this.idDocumento = idDocumento;
            this.movementId = movementId;
            this.frmVentaNew = frmVentaNew;
            InitializeComponent();
            btnClose.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.back_white, 35, 35);
            checkBoxPesarPolloVivo.Checked = true;
        }


        private async void FrmCalculateWeight_Load(object sender, EventArgs e)
        {
            await fillObservationDocument();
            ClsScaleModel sm = ClsScaleModel.getallDataFromAScale();
            if (sm != null) {
                puertoCom = sm.portname;
                baudRate = sm.baud_rate;
                if (sm.parity == 0)
                    parityValue = Parity.None;
                else if (sm.parity == 1)
                    parityValue = Parity.Odd;
                else if (sm.parity == 2)
                    parityValue = Parity.Even;
                else if (sm.parity == 3)
                    parityValue = Parity.Mark;
                else if (sm.parity == 4)
                    parityValue = Parity.Space;
                dataBits = sm.data_bits;
                if (sm.stop_bits == 0)
                    stopBitsValue = StopBits.None;
                else if (sm.stop_bits == 1)
                    stopBitsValue = StopBits.One;
                else if (sm.stop_bits == 1.5)
                    stopBitsValue = StopBits.OnePointFive;
                else if (sm.stop_bits == 2)
                    stopBitsValue = StopBits.Two;
            }
            formWaiting = new FormWaiting(this, 13);//await validateIfTemporalWeightExist();
            formWaiting.ShowDialog();
            fillComboBoxTipoPesoExcedente();
            fillComboBoxTipoCajas();
            validateCheckPolloVivo();
            validateIfCapturaPesoManualIsActivated();
        }

        public async Task validateIfTemporalWeightExist()
        {
            int value = 0, cajas = 0;
            String description = "";
            double pesoBruto = 0, pesoNeto = 0;
            double totalPesoTaras = 0;
            double pesoCaja = 0;
            bool enviadoAlCliente = false;
            await Task.Run(async () =>
            {
                WeightModel wm = WeightModel.getAWeightTemporal(movementId);
                if (wm != null)
                {
                    WeightModel wmReal = WeightModel.getWeightReal(movementId);
                    if (wmReal != null)
                    {
                        pesoBruto = wm.pesoBruto;
                        // Pollo Vivo
                        totalPesoTaras = (Math.Abs(wm.pesoCaja) + Math.Abs(wm.pesoPolloLesionado) + Math.Abs(wm.pesoPolloMuerto) +
                            Math.Abs(wm.pesoPolloBajoDePeso) + Math.Abs(wm.pesoPolloGolpeado));
                        // Pollo No Vivo
                        pesoCaja = wm.pesoCaja;
                        pesoNeto = wm.pesoNeto;
                        cajas = wm.cajas;
                        enviadoAlCliente = DocumentModel.isItDocumentPrepedidoSendedToTheCustomer(idDocumento);
                        value = 1;
                    } else
                    {
                        int lastId = WeightModel.getLastId();
                        lastId++;
                        String query = "INSERT INTO " + LocalDatabase.TABLA_PESO + " VALUES(" + lastId + ", 0, " + movementId + ", " +
                             wm.pesoBruto + ", " + wm.pesoCaja + ", " + wm.pesoNeto + ", '" +
                                DateTime.Now.ToString("yyyy-MM-dd") + "', " + wm.cajas + ", " + wm.pesoPolloLesionado + ", " +
                                wm.pesoPolloMuerto + ", " + wm.pesoPolloBajoDePeso + ", " + wm.pesoPolloGolpeado+", " + WeightModel.TIPO_REAL + ")";
                        if (WeightModel.createUpdateOrDelete(query) > 0)
                        {
                            pesoBruto = wm.pesoBruto;
                            // Pollo Vivo
                            totalPesoTaras = (Math.Abs(wm.pesoCaja) + Math.Abs(wm.pesoPolloLesionado) + Math.Abs(wm.pesoPolloMuerto) +
                                Math.Abs(wm.pesoPolloBajoDePeso) + Math.Abs(wm.pesoPolloGolpeado));
                            // Pollo No Vivo
                            pesoCaja = wm.pesoCaja;
                            pesoNeto = wm.pesoNeto;
                            cajas = wm.cajas;
                            enviadoAlCliente = DocumentModel.isItDocumentPrepedidoSendedToTheCustomer(idDocumento);
                            value = 1;
                        }
                        else description = "No pudimos crear el registro para los pesos reales!";
                    }
                } else
                {
                    wm = WeightModel.getWeightReal(movementId);
                    if (wm != null)
                    {
                        int lastId = WeightModel.getLastId();
                        lastId++;
                        String query = "INSERT INTO " + LocalDatabase.TABLA_PESO + " VALUES(" + lastId + ", 0, " + movementId + ", " +
                             wm.pesoBruto + ", " + wm.pesoCaja + ", " + wm.pesoNeto + ", '" +
                                DateTime.Now.ToString("yyyy-MM-dd") + "', " + wm.cajas + ", " + wm.pesoPolloLesionado + ", " +
                                wm.pesoPolloMuerto + ", " + wm.pesoPolloBajoDePeso + ", " + wm.pesoPolloGolpeado + ", " + WeightModel.TIPO_TEMPORAL + ")";
                        if (WeightModel.createUpdateOrDelete(query) > 0)
                        {
                            pesoBruto = wm.pesoBruto;
                            // Pollo Vivo
                            totalPesoTaras = (Math.Abs(wm.pesoCaja) + Math.Abs(wm.pesoPolloLesionado) + Math.Abs(wm.pesoPolloMuerto) +
                                Math.Abs(wm.pesoPolloBajoDePeso) + Math.Abs(wm.pesoPolloGolpeado));
                            // Pollo No Vivo
                            pesoCaja = wm.pesoCaja;
                            pesoNeto = wm.pesoNeto;
                            cajas = wm.cajas;
                            enviadoAlCliente = DocumentModel.isItDocumentPrepedidoSendedToTheCustomer(idDocumento);
                            value = 1;
                        } description = "No pudimos crear el registro temporal para los pesos!";
                    }
                    else
                    {
                        int lastId = WeightModel.getLastId();
                        lastId++;
                        String query = "INSERT INTO " + LocalDatabase.TABLA_PESO + " VALUES(" + lastId + ", 0, " + movementId + ", 0, 0, 0, '" +
                            DateTime.Now.ToString("yyyy-MM-dd") + "', 0, 0, 0, 0, 0, " + WeightModel.TIPO_REAL + ")";
                        if (WeightModel.createUpdateOrDelete(query) > 0)
                        {
                            lastId++;
                            query = "INSERT INTO " + LocalDatabase.TABLA_PESO + " VALUES(" + lastId + ", 0, " + movementId + ", 0, 0, 0, '" +
                                DateTime.Now.ToString("yyyy-MM-dd") + "', 0, 0, 0, 0, 0, " + WeightModel.TIPO_TEMPORAL + ")";
                            if (WeightModel.createUpdateOrDelete(query) > 0)
                            {
                                value = 2;
                            }
                            else description = "No pudimos crear el registro para los pesos temporales!";
                        }
                        else description = "No pudimos crear el registro para los pesos reales!";
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
                editOperacionesPesoBruto.Text = "" + pesoBruto;
                editOperacionesPesoTaras.Text = "-" + totalPesoTaras;
                textPesoTotalCajas.Text = "" +pesoCaja;
                editPesoNeto.Text = "" + pesoNeto;
                editCajas.Text = "" + cajas;
                if (enviadoAlCliente)
                {
                    panelPesoBruto.Enabled = false;
                    editPesoBruto.Enabled = false;
                    btnRestarPesoBruto.Enabled = false;
                    btnSumarPesoBruto.Enabled = false;
                    editPesoTaras.Select();
                }
                else editPesoBruto.Select();
            } else if (value == 2)
            {
                editPesoBruto.Text = "0";
                editPesoBruto.Select();
            } else
            {
                FormMessage formMessage = new FormMessage("Calculando pesos", description, 3);
                formMessage.ShowDialog();
            }
        }

        private async Task fillObservationDocument()
        {
            String observation = "";
            await Task.Run(async () =>
            {
                observation = DocumentModel.getDocumentObservation(idDocumento);
            });
            editObservationDoc.Text = observation;
        }

        private async Task validateIfCapturaPesoManualIsActivated()
        {
            if (ConfiguracionModel.isCapturaPesoManualPermissionActivated())
            {
                btnReconectar.Visible = false;
                editPesoBruto.ReadOnly = false;
                editPesoBruto.Select();
            }
            else
            {
                btnReconectar.Visible = false;
                editPesoBruto.ReadOnly = true;
                editPesoBruto.Select();
                initProcessReadScale();
                //timerResetBascula.Enabled = true;
            }
        }

        private async Task validateCheckPolloVivo()
        {
            int isPolloVivo = await isDocumentForPolloVivo();
            if (isPolloVivo == 1 || isPolloVivo == 2)
                checkBoxPesarPolloVivo.Checked = true;
            else checkBoxPesarPolloVivo.Checked = false;
            this.Size = new Size(593, 467);
            if (checkBoxPesarPolloVivo.Checked)
            {
                panelSeleccionarCajas.Visible = false;
                panelSeleccionarCajas.Height = 0;
                panelTotales.Location = new Point(10, 255);
                panelPesoTaras.Visible = true;
                panelPesoTaras.Size = new Size(559, 131);
            }
            else
            {
                panelSeleccionarCajas.Size = new Size(559, 109);
                panelSeleccionarCajas.Visible = true;
                panelPesoTaras.Visible = false;
                panelPesoTaras.Height = 0;
                panelSeleccionarCajas.Location = new Point(8, 117);
                panelTotales.Location = new Point(10, 255);
                editCajas.ReadOnly = true;
            }
            checkBoxPesarPolloVivo.Enabled = false;
        }

        private async Task<int> isDocumentForPolloVivo()
        {
            int response = 0;
            await Task.Run(() => {
                String query = "SELECT PE.facturar FROM Documentos D " +
                "INNER JOIN PedidoEncabezado PE ON D.CIDDOCTOPEDIDOCC = PE.CIDDOCTOPEDIDOCC WHERE PE.type = 4 AND D.id = "+idDocumento;
                response = PedidosEncabezadoModel.getIntValue(query);
            });
            return response;
        }

        private async Task fillComboBoxTipoCajas()
        {
            tarasList = await getAllTaras();
            if (tarasList != null)
            {
                comboBoxTiposCajas.Items.Clear();
                foreach(TaraModel tm in tarasList)
                {
                    comboBoxTiposCajas.Items.Add("Peso: "+tm.peso+ " kg, "+tm.name);
                }
                comboBoxTiposCajas.SelectedIndex = 0;
                textInfoCantidadDeCajasSeleccionadas.Text = "Cantidad de Cajas del Tipo: " + tarasList[0].tipo;
            }
        }

        private async Task<List<TaraModel>> getAllTaras()
        {
            List<TaraModel> tarasList = null;
            await Task.Run(() => {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_TARA;
                tarasList = TaraModel.getTaras(query);
            });
            return tarasList;
        }

        private async Task initProcessReadScale()
        {
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            dynamic response = await initializeSerialPortConnection();
            if (response.valor == -1) {
                FormMessage fm = new FormMessage("Exception", response.description, 3);
                fm.ShowDialog();
            } else
            {
                if (serialPortScale.IsOpen)
                    editScaleInformation.Text += "Conectado\r\n";
                else editScaleInformation.Text += "Desconectado\r\n";
                readPortScale();
            }
        }

        private void fillComboBoxTipoPesoExcedente()
        {
            comboBoxTipoPesoExcedente.Items.Clear();
            comboBoxTipoPesoExcedente.Items.Add("Taras");
            comboBoxTipoPesoExcedente.Items.Add("Pollo Lesionado");
            comboBoxTipoPesoExcedente.Items.Add("Pollo Muerto");
            comboBoxTipoPesoExcedente.Items.Add("Pollo Bajo de Peso");
            comboBoxTipoPesoExcedente.Items.Add("Pollo Golpeado");
            comboBoxTipoPesoExcedente.SelectedIndex = 0;
            bool enviado = DocumentModel.isItDocumentPrepedidoSendedToTheCustomer(idDocumento);
            if (enviado){
                bool enviadoPendienteDestarar = DocumentModel.isItDocumentPrepedidoSendedToTheCustomerAndPendienteDeDestarar(idDocumento);
                if (enviadoPendienteDestarar)
                {
                    btnResetPeso.Text = "Borrar Peso Tara/Pollo Mal";
                }
                else
                {
                    btnResetPeso.Text = "Borrar Peso Tara/Pollo Mal";
                }
            } else
            {
                btnResetPeso.Text = "Resetear Pesos";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public async Task<int> validarCierreDeVentana()
        {
            int respuesta = 0;
            await Task.Run(() => {
                bool enviadoAlCliente = DocumentModel.isItDocumentPrepedidoSendedToTheCustomer(idDocumento);
                if (!enviadoAlCliente)
                    respuesta = 2;
                else respuesta = 1;
            });
            return respuesta;
        }

        public async void refreshFrmVenta()
        {
            if (frmVentaNew != null)
            {
                frmVentaNew.editCapturedUnits.Text = "" + WeightModel.getPesoNetoTemporal(movementId);
                await frmVentaNew.logicToAddItemLocal(WeightModel.getPesoNetoTemporal(movementId));
            }
            this.Close();
        }

        public void confirmarEliminarPesos(FormClosingEventArgs e) {
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            FrmConfirmation frmConfirmation = new FrmConfirmation("Cancelar", "Pesos capturados, todos los pesos serán ELIMINADOS\r\n¿Deseas salir sin guardar?");
            frmConfirmation.StartPosition = FormStartPosition.CenterScreen;
            frmConfirmation.ShowDialog();
            if (FrmConfirmation.confirmation)
            {
                formWaiting = new FormWaiting(this, 12);
                formWaiting.ShowDialog();
            }
        }

        public async Task deleteAllBeforeCapturedWeights()
        {
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                String query = "DELETE FROM " + LocalDatabase.TABLA_PESO + " WHERE " + LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = " +
                    movementId;
                int response = WeightModel.createUpdateOrDelete(query);
                if (response == 1)
                {
                    MovimientosModel.updateCapturedUnits(movementId, 0);
                    value = 1;
                }
                else description = "No pudimos eliminarl los pesos correctamente!";
            });
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            if (value == 1)
            {
                if (frmVentaNew != null)
                {
                    frmVentaNew.resetearValores(0);
                    await frmVentaNew.fillDataGridMovements();
                }
                this.Close();
            } else {
                FormMessage formMessage = new FormMessage("Eliminando Pesos", description, 3);
                formMessage.ShowDialog();
                if (frmVentaNew != null)
                {
                    frmVentaNew.resetearValores(0);
                    await frmVentaNew.fillDataGridMovements();
                }
                this.Close();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            formWaiting = new FormWaiting(this, 10); //await proccesToSaveRealWeights(false);
            formWaiting.ShowDialog();
        }

        public async Task proccesToSaveRealWeights()
        {
            int value = 0;
            String description = "";
            String pesoBrutoText = editPesoBruto.Text.Trim();
            String pesoTarasText = editPesoTaras.Text.Trim();
            String pesoNetoText = editPesoNeto.Text.Trim();
            String numeroDeCajas = editCajas.Text.Trim();
            String textoCajas = editCajas.Text.Trim();
            bool polloVivoChecked = checkBoxPesarPolloVivo.Checked;
            String newObservation = editObservationDoc.Text.Trim();
            double pesoNetoReal = 0;
            await Task.Run(async () =>
            {
                await updateObservationDocument(newObservation);
                if (!numeroDeCajas.Equals("") && !numeroDeCajas.Equals("0"))
                {
                    if (!pesoNetoText.Equals("") && !pesoNetoText.Equals("0"))
                    {
                        int response = await savePesosInRealTypeField(polloVivoChecked, textoCajas);
                        if (response == 1)
                        {
                            pesoNetoReal = WeightModel.getPesoNetoReal(movementId);
                            value = 1;
                        }
                        else
                        {
                            pesoNetoReal = WeightModel.getPesoNetoReal(movementId);
                            description = "No pudimos actualizar correctamente los pesos, algo falló al guardarlos!";
                            value = 2;
                        }
                    }
                    else
                    {
                        pesoNetoReal = WeightModel.getPesoNetoTemporal(movementId);
                        value = 3;
                    }
                }
                else
                {
                    description = "Tienes que agregar el número de cajas antes de guardar!";
                }
            });
            if (value == 1)
            {
                if (frmVentaNew != null)
                    await frmVentaNew.logicToAddItemLocal(pesoNetoReal);
                if (formWaiting != null)
                {
                    formWaiting.Dispose();
                    formWaiting.Close();
                }
                this.Close();
            }
            else if (value == 2) {
                if (frmVentaNew != null)
                    await frmVentaNew.logicToAddItemLocal(pesoNetoReal);
                if (formWaiting != null)
                {
                    formWaiting.Dispose();
                    formWaiting.Close();
                }
                FormMessage formMessage = new FormMessage("Guardando Pesos", description, 3);
                formMessage.ShowDialog();
                this.Close();
            } else if (value == 3)
            {
                if (frmVentaNew != null)
                {
                    frmVentaNew.editCapturedUnits.Text = "" + pesoNetoReal;
                    await frmVentaNew.logicToAddItemLocal(pesoNetoReal);
                }
                if (formWaiting != null)
                {
                    formWaiting.Dispose();
                    formWaiting.Close();
                }
                this.Close();
            }
            else
            {
                if (formWaiting != null)
                {
                    formWaiting.Dispose();
                    formWaiting.Close();
                }
                FormMessage formMessage = new FormMessage("Guardando Pesos", description, 3);
                formMessage.ShowDialog();
            }
        }

        private async Task updateObservationDocument(String newObservation)
        {
            await Task.Run(async () =>
            {
                if (!newObservation.Equals(""))
                {
                    DocumentModel.updateTheDocumentObservation(idDocumento, newObservation);
                }
            });
        }

        private async Task<int> savePesosInRealTypeField(bool polloVivoChecked, String textoCajas)
        {
            int response = 0;
            await Task.Run(async () => {
                WeightModel wm = WeightModel.getAWeightTemporal(movementId);
                if (wm != null)
                {
                    int cajas = 0;
                    if (polloVivoChecked)
                    {
                        if (textoCajas.Equals(""))
                            cajas = 0;
                        else cajas = Convert.ToInt32(textoCajas);
                    }
                    else cajas = wm.cajas;
                    double pesoTaratotal = (Math.Abs(wm.pesoCaja) + Math.Abs(wm.pesoPolloLesionado) + Math.Abs(wm.pesoPolloMuerto) + 
                    Math.Abs(wm.pesoPolloGolpeado) + Math.Abs(wm.pesoPolloBajoDePeso));
                    double pesoNeto = (Math.Abs(wm.pesoBruto) - pesoTaratotal);
                    if (pesoNeto == wm.pesoNeto)
                        pesoNeto = wm.pesoNeto;
                    response = WeightModel.updateWeightReal(wm.pesoBruto, wm.pesoCaja, pesoNeto, cajas, wm.pesoPolloLesionado, 
                        wm.pesoPolloMuerto, wm.pesoPolloBajoDePeso, wm.pesoPolloGolpeado, movementId);
                }
            });
            return response;
        }

        private void btnRestar_Click(object sender, EventArgs e)
        {
            formWaiting = new FormWaiting(this, 6); //await restarPesoTara();
            formWaiting.ShowDialog();
        }

        private void editPesoTaras_KeyPress(object sender, KeyPressEventArgs e)
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
                formWaiting = new FormWaiting(this, 6); //restarPesoTara();
                formWaiting.ShowDialog();
            }
        }

        public async Task restarPesoTara()
        {
            int value = 0;
            String description = "";
            int pesoPolloMalExcedenteIndex = comboBoxTipoPesoExcedente.SelectedIndex;
            String kilosText = editPesoTaras.Text.Trim();
            double pesoNeto = 0, kilos = 0;
            await Task.Run(async () =>
            {
                if (!kilosText.Equals(""))
                {
                    kilosText = kilosText.Replace("kg", "").Trim();
                    kilos = Convert.ToDouble(kilosText);
                    if (kilos > 0)
                    {
                        int response = await processToAddOrSubstractWeight(TIPO_PESOTARA, 0, kilos, pesoPolloMalExcedenteIndex);
                        if (response != 0)
                        {
                            value = 1;
                        }
                        else description = "No pudimos realizar el proceso de resta de las taras!";
                    }
                    pesoNeto = WeightModel.getPesoNetoTemporal(movementId);
                }
                else description = "Los kilos no pueden ser menor a cero!";
            });
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            if (value == 1)
            {
                editPesoNeto.Text = "" + pesoNeto;
                if (editOperacionesPesoTaras.Text.Trim().Equals("Operaciones"))
                    editOperacionesPesoTaras.Text = "" + kilos;
                else editOperacionesPesoTaras.Text += "-" + kilos;
                editPesoTaras.Text = "0";
                editPesoTaras.Select();
            } else
            {
                FormMessage formMessage = new FormMessage("Restando Peso Tara", description, 3);
                formMessage.ShowDialog();
            }
        }

        private void btnSumar_Click(object sender, EventArgs e)
        {
            formWaiting = new FormWaiting(this, 9);//procesoSumarTaras();
            formWaiting.ShowDialog();
        }

        public async Task procesoSumarTaras()
        {
            int value = 0;
            String description = "";
            int pesoPolloMalExcedenteIndex = comboBoxTipoPesoExcedente.SelectedIndex;
            String kilosText = editPesoTaras.Text.Trim();
            double pesoNeto = 0, kilos = 0;
            await Task.Run(async () =>
            {
                if (!kilosText.Equals(""))
                {
                    kilosText = kilosText.Replace("kg", "").Trim();
                    kilos = Convert.ToDouble(kilosText);
                    if (kilos > 0)
                    {
                        int response = await processToAddOrSubstractWeight(TIPO_PESOTARA, 1, kilos, pesoPolloMalExcedenteIndex);
                        if (response != 0)
                        {
                            value = 1;
                        }
                        else description = "No pudimos completar el proceso de sumar las taras!";
                    } else description = "El peso bruto a restar tiene que ser mayor a cero!";
                    pesoNeto = WeightModel.getPesoNetoTemporal(movementId);
                }
                else description = "El peso bruto a restar tiene que ser mayor a cero!";
            });
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            if (value == 1)
            {
                if (editOperacionesPesoTaras.Text.Trim().Equals("Operaciones"))
                    editOperacionesPesoTaras.Text = "" + kilos;
                else editOperacionesPesoTaras.Text += "+" + kilos;
                editPesoNeto.Text = ""+pesoNeto;
                editPesoTaras.Text = "0";
                editPesoTaras.Select();
            } else
            {
                FormMessage formMessage = new FormMessage("Proceso Sumar Taras", description, 3);
                formMessage.ShowDialog();
            }
        }

        private void btnSumarPesoBruto_Click(object sender, EventArgs e)
        {
            formWaiting = new FormWaiting(this, 7); //sumarPesoBruto();
            formWaiting.ShowDialog();
            editPesoBruto.Focus();
        }

        private void editPesoActual_KeyPress(object sender, KeyPressEventArgs e)
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
                formWaiting = new FormWaiting(this, 7); //sumarPesoBruto();
                formWaiting.ShowDialog();
                editPesoBruto.Focus();
            }
        }

        public async Task sumarPesoBruto()
        {
            int value = 0;
            String description = "";
            String kilosText = editPesoBruto.Text.Trim();
            double pesoNeto = 0, kilos = 0;
            await Task.Run(async () =>
            {
                if (!kilosText.Equals(""))
                {
                    kilosText = kilosText.Replace("kg", "").Trim();
                    kilos = Convert.ToDouble(kilosText);
                    if (kilos > 0)
                    {
                        int response = await processToAddOrSubstractWeight(TIPO_PESOBRUTO, 1, kilos, 0);
                        if (response != 0)
                        {
                            value = 1;
                        }
                        else description = "No pudimos terminar correctamente el proceso de sumar el peso bruto!";
                    }
                    else description = "El peso bruto tiene que se mayor a cero!";
                    pesoNeto = WeightModel.getPesoNetoTemporal(movementId);
                }
                else description = "El peso bruto tiene que se mayor a cero!";
            });
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            if (value == 1)
            {
                this.Enabled = true;
                if (editOperacionesPesoBruto.Text.Trim().Equals("Operaciones"))
                    editOperacionesPesoBruto.Text = "" + kilos;
                else editOperacionesPesoBruto.Text += "+" + kilos;
                editPesoNeto.Text = "" + pesoNeto;
                editPesoBruto.Select();
            } else
            {
                FormMessage formMessage = new FormMessage("Proceso Sumar Bruto", description, 3);
                formMessage.ShowDialog();
                editPesoBruto.Select();
            }
        }

        private void btnRestarPesoBruto_Click(object sender, EventArgs e)
        {
            formWaiting = new FormWaiting(this, 8); //procesoRestarPesoBruto
            formWaiting.ShowDialog();
            editPesoBruto.Focus();
        }

        public async Task procesoRestarPesoBruto()
        {
            int value = 0;
            String description = "";
            String kilosText = editPesoBruto.Text.Trim();
            double pesoNeto = 0, kilos = 0;
            await Task.Run(async () =>
            {
                if (!kilosText.Equals(""))
                {
                    kilosText = kilosText.Replace("kg", "").Trim();
                    kilos = Convert.ToDouble(kilosText);
                    if (kilos > 0)
                    {
                        int response = await processToAddOrSubstractWeight(TIPO_PESOBRUTO, 0, kilos, 0);
                        if (response != 0)
                        {
                            value = 1;
                        }
                        else description = "No pudimos terminar el proceso de restar el peso bruto!";
                    } else description = "El peso bruto a restar debe ser meyor a cero!";
                     pesoNeto = WeightModel.getPesoNetoTemporal(movementId);
                }
                else description = "El peso bruto a restar debe ser meyor a cero!";
            });
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            if (value == 1)
            {
                this.Enabled = true;
                if (editOperacionesPesoBruto.Text.Trim().Equals("Operaciones"))
                    editOperacionesPesoBruto.Text = "" + kilos;
                else editOperacionesPesoBruto.Text += "-" + kilos;
                editPesoNeto.Text = ""+pesoNeto;
                //editPesoBruto.Text = "0";
                editPesoBruto.Select();
            } else
            {
                FormMessage formMessage = new FormMessage("Proceso Restar Peso Bruto", description, 3);
                formMessage.ShowDialog();
                editPesoBruto.Select();
            }
        }

        private async Task<int> processToAddOrSubstractWeight(int tipoPeso, int tipoOperacion, double kilo, int pesoPolloMalExcedenteIndex) {
            int response = 0;
            if (tipoPeso == TIPO_PESOBRUTO)
            {
                response = await updatePesos(tipoPeso, tipoOperacion, kilo, 0, 0, pesoPolloMalExcedenteIndex);
            } else if (tipoPeso == TIPO_PESOTARA)
            {
                response = await updatePesos(tipoPeso, tipoOperacion, 0, kilo, 0, pesoPolloMalExcedenteIndex);

            } else if (tipoPeso == TIPO_PESONETO)
            {
                response = await updatePesos(tipoPeso, tipoOperacion, 0, 0, kilo, pesoPolloMalExcedenteIndex);
            }
            if (response == 0)
            {
                FormMessage formMessage = new FormMessage("Exception", "Algo falló al actualizar el peso", 2);
                formMessage.ShowDialog();
            }
            return response;
        }

        private async Task<int> updatePesos(int tipoPeso, int tipoOperacion, double pesoBruto, double pesoTara, double pesoNeto, int polloMal)
        {
            int response = 0;
            await Task.Run(async () => {
                if (movementId != 0)
                {
                    WeightModel wm = WeightModel.getAWeightTemporal(movementId);
                    if (wm != null)
                    {
                        if (tipoPeso == TIPO_PESOBRUTO)
                        {
                            double nuevoPesoBruto = 0;
                            double nuevoPesoNeto = 0;
                            double pesoNetoAnterior = wm.pesoNeto;
                            double pesoBrutoAnterior = wm.pesoBruto;
                            if (tipoOperacion == 0)
                            {
                                nuevoPesoBruto = pesoBrutoAnterior - pesoBruto;
                                nuevoPesoNeto = pesoNetoAnterior - pesoBruto;
                            }
                            else
                            {
                                nuevoPesoBruto = pesoBrutoAnterior + pesoBruto;
                                nuevoPesoNeto = pesoNetoAnterior + pesoBruto;
                            }
                            response = WeightModel.updatePesoBrutoYNetoTemporal(movementId, nuevoPesoBruto, nuevoPesoNeto);
                        } else if (tipoPeso == TIPO_PESOTARA) {
                            if (polloMal == 0)
                            {
                                /* Tara */
                                double nuevoPesoTara = 0;
                                double nuevoPesoNeto = 0;
                                double pesoNetoAnterior = wm.pesoNeto;
                                double pesoTaraAnterior = wm.pesoCaja;
                                if (tipoOperacion == 0)
                                {
                                    nuevoPesoTara = pesoTaraAnterior - pesoTara;
                                    nuevoPesoNeto = pesoNetoAnterior - pesoTara;
                                } else
                                {
                                    nuevoPesoTara = pesoTaraAnterior + pesoTara;
                                    nuevoPesoNeto = pesoNetoAnterior + pesoTara;
                                }
                                if (WeightModel.updatePesoTaraYNetoTemporal(movementId, nuevoPesoTara, nuevoPesoNeto))
                                    response = 1;
                            } else if (polloMal == 1)
                            {
                                /* Lesionado */
                                double nuevoPesoPolloLesionado = 0;
                                double nuevoPesoNeto = 0;
                                double pesoNetoAnterior = wm.pesoNeto;
                                double pesoPolloLesionadoAnterior = wm.pesoPolloLesionado;
                                if (tipoOperacion == 0)
                                {
                                    nuevoPesoPolloLesionado = pesoPolloLesionadoAnterior - pesoTara;
                                    nuevoPesoNeto = pesoNetoAnterior - pesoTara;
                                }
                                else
                                {
                                    nuevoPesoPolloLesionado = pesoPolloLesionadoAnterior + pesoTara;
                                    nuevoPesoNeto = pesoNetoAnterior + pesoTara;
                                }
                                response = WeightModel.updatePesoPolloLesionadoYNetoTemporal(movementId, nuevoPesoPolloLesionado, nuevoPesoNeto);
                            } else if (polloMal == 2)
                            {
                                /* Muerto */
                                double nuevoPesoPolloMuerto = 0;
                                double nuevoPesoNeto = 0;
                                double pesoNetoAnterior = wm.pesoNeto;
                                double pesoPolloMuertoAnterior = wm.pesoPolloMuerto;
                                if (tipoOperacion == 0)
                                {
                                    nuevoPesoPolloMuerto = pesoPolloMuertoAnterior - pesoTara;
                                    nuevoPesoNeto = pesoNetoAnterior - pesoTara;
                                }
                                else
                                {
                                    nuevoPesoPolloMuerto = pesoPolloMuertoAnterior + pesoTara;
                                    nuevoPesoNeto = pesoNetoAnterior + pesoTara;
                                }
                                response = WeightModel.updatePesoPolloMuertoYNetoTemporal(movementId, nuevoPesoPolloMuerto, nuevoPesoNeto);
                            } else if (polloMal == 3)
                            {
                                /* Bajo de Peso */
                                double nuevoPesoPolloBajoDePeso = 0;
                                double nuevoPesoNeto = 0;
                                double pesoNetoAnterior = wm.pesoNeto;
                                double pesoPolloBajoPesoAnterior = wm.pesoPolloBajoDePeso;
                                if (tipoOperacion == 0)
                                {
                                    nuevoPesoPolloBajoDePeso = pesoPolloBajoPesoAnterior - pesoTara;
                                    nuevoPesoNeto = pesoNetoAnterior - pesoTara;
                                }
                                else
                                {
                                    nuevoPesoPolloBajoDePeso = pesoPolloBajoPesoAnterior + pesoTara;
                                    nuevoPesoNeto = pesoNetoAnterior + pesoTara;
                                }
                                response = WeightModel.updatePesoPolloBajoPesoYNetoTemporal(movementId, nuevoPesoPolloBajoDePeso,
                                    nuevoPesoNeto);
                            } else if (polloMal == 4)
                            {
                                /* Golpeado */
                                double newPesoPolloGolpeado = 0;
                                double nuevoPesoNeto = 0;
                                double pesoNetoAnterior = wm.pesoNeto;
                                double pesoPolloGolpeadoAnterior = wm.pesoPolloGolpeado;
                                if (tipoOperacion == 0)
                                {
                                    newPesoPolloGolpeado = pesoPolloGolpeadoAnterior - pesoTara;
                                    nuevoPesoNeto = pesoNetoAnterior - pesoTara;
                                }
                                else
                                {
                                    newPesoPolloGolpeado = pesoPolloGolpeadoAnterior + pesoTara;
                                    nuevoPesoNeto = pesoNetoAnterior + pesoTara;
                                }
                                response = WeightModel.updatePesoPolloGolpeadoYNetoTemporal(movementId, newPesoPolloGolpeado,
                                    nuevoPesoNeto);
                            }
                        } else if (tipoPeso == TIPO_PESONETO)
                        {
                            response = WeightModel.updatePesoNetoTemporal(movementId, pesoNeto);
                        }
                    }
                }
            });
            return response;
        }

        private void editCajas_KeyPress(object sender, KeyPressEventArgs e)
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
                //proccesToSaveWeights(false);
                formWaiting = new FormWaiting(this, 10);
                formWaiting.ShowDialog();
            }
        }

        private void FrmCalculateWeight_FormClosing(object sender, FormClosingEventArgs e)
        {
            //cerrando = true;
            //cerrandoVentanaPesos(e);
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
        }

        private void cerrandoVentanaPesos(FormClosingEventArgs e)
        {
            formWaiting = new FormWaiting(0, this, e);
            formWaiting.ShowDialog();
        }

        private void cerrarPuertoSerial()
        {
            ciclarLectura = false;
            Thread.Sleep(600);
        }

        private void comboBoxTipoPesoExcedente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTipoPesoExcedente.Focused)
            {
                if (comboBoxTipoPesoExcedente.SelectedIndex == 0)
                {

                    textPesoTaras.Text = "Peso Taras:";
                    editPesoTaras.Select();
                }
                else if (comboBoxTipoPesoExcedente.SelectedIndex == 1)
                {
                    textPesoTaras.Text = "Pollo Lesionado:";
                    editPesoTaras.Select();
                }
                else if (comboBoxTipoPesoExcedente.SelectedIndex == 2)
                {
                    textPesoTaras.Text = "Pollo Muerto:";
                    editPesoTaras.Select();
                }
                else if (comboBoxTipoPesoExcedente.SelectedIndex == 3)
                {
                    textPesoTaras.Text = "Pollo Bajo de Peso:";
                    editPesoTaras.Select();
                }
                else
                {
                    textPesoTaras.Text = "Pollo Golpeado:";
                    editPesoTaras.Select();
                }
            }
        }

        private void comboBoxTiposCajas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTiposCajas.Focused)
            {
                if (tarasList != null)
                {
                    if (tarasList.Count >= comboBoxTiposCajas.SelectedIndex || tarasList.Count <= comboBoxTiposCajas.SelectedIndex)
                    {
                        textInfoCantidadDeCajasSeleccionadas.Text = "Cantidad de Cajas Tipo: " + tarasList[comboBoxTiposCajas.SelectedIndex].tipo;
                    }
                }
                editCantidadDeCajasSeleccionada.Select();
            }
        }

        private void checkBoxPesarPolloVivo_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPesarPolloVivo.Focused)
            {
                if (checkBoxPesarPolloVivo.Checked)
                {
                    panelSeleccionarCajas.Visible = false;
                    panelSeleccionarCajas.Height = 0;
                    panelTotales.Location = new Point(13, 173);
                    panelPesoTaras.Visible = true;
                    panelPesoTaras.Size = new Size(417, 90);
                }
                else
                {
                    panelSeleccionarCajas.Size = new Size(417, 86);
                    panelSeleccionarCajas.Visible = true;
                    panelPesoTaras.Visible = false;
                    panelPesoTaras.Height = 0;
                    panelSeleccionarCajas.Location = new Point(13, 77);
                    panelTotales.Location = new Point(13, 173);
                }
            }
        }

        private void btnAgregarCajas_Click(object sender, EventArgs e)
        {
            formWaiting = new FormWaiting(this, 5);
            formWaiting.ShowDialog();
            //multiplicarCajasPorPesos();
        }

        private void editCantidadDeCajasSeleccionada_KeyPress(object sender, KeyPressEventArgs e)
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
                formWaiting = new FormWaiting(this, 5);
                formWaiting.ShowDialog();
                //multiplicarCajasPorPesos();
            }
        }

        public async Task multiplicarCajasPorPesos()
        {
            int value = 0;
            String description = "";
            String numeroDeCajasText = editCantidadDeCajasSeleccionada.Text.Trim();
            int idTara = tarasList[comboBoxTiposCajas.SelectedIndex].id;
            int pesoPolloMalExcedenteIndex = comboBoxTipoPesoExcedente.SelectedIndex;
            String textNumeroDeCajas = editCajas.Text.Trim();
            double pesoNeto = 0, numerOfCajas = 0, totalPesoCajas = 0;
            await Task.Run(async () =>
            {
                if (numeroDeCajasText.Equals("") || numeroDeCajasText.Equals("0"))
                {
                    description = "Tienes que ingresa un número valido!";
                }
                else
                {
                    double numeroCajas = Convert.ToDouble(numeroDeCajasText);
                    if (numeroCajas > 0)
                    {
                        String query = "SELECT " + LocalDatabase.CAMPO_PESO_TARA + " FROM " + LocalDatabase.TABLA_TARA + " WHERE " +
                                LocalDatabase.CAMPO_ID_TARA + " = " + idTara;
                        double pesoCaja = TaraModel.getDoubleValue(query);
                        double pesoADescontar = pesoCaja * numeroCajas;
                        int response = await processToAddOrSubstractWeight(TIPO_PESOTARA, 0, pesoADescontar, pesoPolloMalExcedenteIndex);
                        if (response != 0)
                        {
                            if (!textNumeroDeCajas.Equals(""))
                            {
                                double cajasAnteriores = Convert.ToDouble(textNumeroDeCajas);
                                double nuevasCajas = cajasAnteriores + numeroCajas;
                                numerOfCajas = nuevasCajas;
                                query = "UPDATE " + LocalDatabase.TABLA_PESO + " SET " + LocalDatabase.CAMPO_CAJAS_PESO + " = " + nuevasCajas +
                                    " WHERE " + LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = " + movementId +
                                    " AND " + LocalDatabase.CAMPO_TIPO_PESO + " = " + WeightModel.TIPO_TEMPORAL;
                                WeightModel.createUpdateOrDelete(query);
                            }
                            else
                            {
                                numerOfCajas = numeroCajas;
                                query = "UPDATE " + LocalDatabase.TABLA_PESO + " SET " + LocalDatabase.CAMPO_CAJAS_PESO + " = " + numeroCajas +
                                    " WHERE " + LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = " + movementId +
                                    " AND " + LocalDatabase.CAMPO_TIPO_PESO + " = " + WeightModel.TIPO_TEMPORAL;
                                WeightModel.createUpdateOrDelete(query);
                            }
                            pesoNeto = WeightModel.getPesoNetoTemporal(movementId);
                            query = "SELECT " + LocalDatabase.CAMPO_PESOCAJA_PESO + " FROM " + LocalDatabase.TABLA_PESO + " WHERE " +
                                LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = " + movementId;
                            totalPesoCajas = WeightModel.getDoubleValue(query);
                            value = 1;
                        }
                        else description = "No pudimos realizar el proceso de calcular pesos de taras/cajas";
                    }
                    else description = "La cantidad de cajas tiene que ser mayor a cero!";
                }
            });
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            if (value == 1)
            {
                editCajas.Text = ""+numerOfCajas;
                editPesoNeto.Text = ""+pesoNeto;
                textPesoTotalCajas.Text = "Peso Cajas: " + totalPesoCajas;
                editCantidadDeCajasSeleccionada.Text = "";
                editCantidadDeCajasSeleccionada.Select();
            } else
            {
                FormMessage formMessage = new FormMessage("Exception", description, 3);
                formMessage.ShowDialog();
            }
        }

        private void btnBorrarPesoBruto_Click(object sender, EventArgs e)
        {
            FrmConfirmation frmConfirmation = new FrmConfirmation("Eliminar Peso Bruto", "¿Estás seguro de que deseas eliminar el peso bruto?");
            frmConfirmation.StartPosition = FormStartPosition.CenterParent;
            frmConfirmation.ShowDialog();
            if (FrmConfirmation.confirmation)
            {
                formWaiting = new FormWaiting(this, 2);
                formWaiting.ShowDialog();
            }
        }

        public async Task processToDeletePesosBrutos()
        {
            int value = 0;
            String description = "";
            double nuevoPesoNeto = 0;
            await Task.Run(async () =>
            {
                bool enviadoAlCliente = DocumentModel.isItDocumentPrepedidoSendedToTheCustomer(idDocumento);
                if (enviadoAlCliente)
                {
                    description = "No podemos resetear el peso bruto entregado al cliente";
                }
                else
                {
                    String query = "UPDATE " + LocalDatabase.TABLA_PESO + " SET " +LocalDatabase.CAMPO_PESOBRUTO_PESO + " = 0" +
                        " WHERE " + LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = " + movementId + " AND " +
                        LocalDatabase.CAMPO_TIPO_PESO + " = " + WeightModel.TIPO_TEMPORAL;
                    int response = WeightModel.createUpdateOrDelete(query);
                    if (response == 1)
                    {
                        query = "SELECT * FROM " + LocalDatabase.TABLA_PESO + " WHERE " + LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = " +
                            movementId + " AND " + LocalDatabase.CAMPO_TIPO_PESO + " = " + WeightModel.TIPO_TEMPORAL;
                        WeightModel wm = WeightModel.getAWeight(query);
                        if (wm != null)
                        {
                            nuevoPesoNeto -= wm.pesoCaja;
                            nuevoPesoNeto -= wm.pesoPolloLesionado;
                            nuevoPesoNeto -= wm.pesoPolloMuerto;
                            nuevoPesoNeto -= wm.pesoPolloBajoDePeso;
                            nuevoPesoNeto -= wm.pesoPolloGolpeado;
                            query = "UPDATE " + LocalDatabase.TABLA_PESO + " SET " +
                        LocalDatabase.CAMPO_PESONETO_PESO + " = " + (-nuevoPesoNeto) +
                        " WHERE " + LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = " + movementId + " AND " +
                        LocalDatabase.CAMPO_TIPO_PESO + " = " + WeightModel.TIPO_TEMPORAL;
                            WeightModel.createUpdateOrDelete(query);
                            value = 1;
                        }
                        else description = "No pudimos encontrar los valores de las taras!";
                    }
                    else description = "No pudimos actualizar el peso bruto del movimiento "+ movementId;
                }
            });
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            if (value == 1)
            {
                editPesoNeto.Text = "-" + nuevoPesoNeto;
                editOperacionesPesoBruto.Text = "Operaciones";
                editPesoBruto.Text = "0";
                editPesoBruto.Select();
                PopupNotifier popup = new PopupNotifier();
                popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                popup.TitleColor = Color.FromArgb(43, 143, 192);
                popup.TitleText = "Peso Bruto";
                popup.TitlePadding = new Padding(5, 5, 5, 5);
                popup.ButtonBorderColor = Color.Red;
                popup.ContentText = "El peso bruto del pedido fue reseteado!";
                popup.ContentColor = Color.FromArgb(43, 143, 192);
                popup.HeaderHeight = 10;
                popup.AnimationDuration = 1000;
                popup.HeaderColor = Color.FromArgb(200, 244, 255);
                popup.Popup();
            } else
            {
                FormMessage formMessage = new FormMessage("Reseteando peso bruto", description, 2);
                formMessage.ShowDialog();
            }
        }

        private void btnBorrarPesoCajas_Click(object sender, EventArgs e)
        {
            FrmConfirmation frmConfirmation = new FrmConfirmation("Eliminar Peso de Cajas", "¿Estás seguro de que deseas eliminar el peso de las cajas?");
            frmConfirmation.StartPosition = FormStartPosition.CenterParent;
            frmConfirmation.ShowDialog();
            if (FrmConfirmation.confirmation)
            {
                formWaiting = new FormWaiting(this, 3);
                formWaiting.ShowDialog();
            }
        }

        public async Task processToDeletePesoDeCajas()
        {
            int value = 0;
            String description = "";
            double pesoCaja = 0, pesoBruto = 0;
            await Task.Run(async () =>
            {
                bool enviadoAlCliente = DocumentModel.isItDocumentPrepedidoSendedToTheCustomer(idDocumento);
                if (enviadoAlCliente)
                {
                    String query = "SELECT * FROM " + LocalDatabase.TABLA_PESO + " WHERE " + LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = " +
                        movementId + " AND " + LocalDatabase.CAMPO_TIPO_PESO + " = " + WeightModel.TIPO_REAL;
                    WeightModel wm = WeightModel.getAWeight(query);
                    if (wm != null)
                    {
                        query = "UPDATE " + LocalDatabase.TABLA_PESO + " SET " + LocalDatabase.CAMPO_PESOCAJA_PESO + " = 0, " +
                            LocalDatabase.CAMPO_PESOPOLLOLESIONADO_PESO + " = 0, " + LocalDatabase.CAMPO_PESOPOLLOMUERTO_PESO + " = 0, " +
                            LocalDatabase.CAMPO_PESOPOLLOBAJOPESO_PESO + " = 0, " + LocalDatabase.CAMPO_PESOPOLLOGOLPEADO_PESO + " = 0, " +
                            LocalDatabase.CAMPO_PESOBRUTO_PESO + " = " + wm.pesoBruto + ", " +
                            LocalDatabase.CAMPO_PESONETO_PESO + " = " + wm.pesoBruto;
                        if (wm.cajas != 0)
                            query += ", " + LocalDatabase.CAMPO_CAJAS_PESO + " = " + wm.cajas;
                        query += " WHERE " + LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = " + movementId + " AND " +
                        LocalDatabase.CAMPO_TIPO_PESO + " = " + WeightModel.TIPO_TEMPORAL;
                        int response = WeightModel.createUpdateOrDelete(query);
                        pesoCaja = wm.cajas;
                        pesoBruto = wm.pesoBruto;
                        value = 1;
                    }
                    else description = "No pudimos encontrar los datos de los pesos reales!";
                }
                else
                {
                    double pesoBrutoAnterior = WeightModel.getPesoBrutoTemporal(movementId);
                    String query = "UPDATE " + LocalDatabase.TABLA_PESO + " SET " + LocalDatabase.CAMPO_PESOCAJA_PESO + " = 0, " +
                            LocalDatabase.CAMPO_CAJAS_PESO + " = 0, " +
                        LocalDatabase.CAMPO_PESONETO_PESO + " = " + pesoBrutoAnterior + " WHERE " +
                        LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = " + movementId + " AND " +
                        LocalDatabase.CAMPO_TIPO_PESO + " = " + WeightModel.TIPO_TEMPORAL;
                    if (WeightModel.createUpdateOrDelete(query) > 0)
                    {
                        value = 2;
                    }
                    else description = "No pudimos actualizar el peso bruto!";
                }
            });
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            if (value == 1)
            {
                editOperacionesPesoTaras.Text = "Operaciones";
                editCajas.Text = "" + pesoCaja;
                editPesoNeto.Text = "" + pesoBruto;
                editCantidadDeCajasSeleccionada.Text = "" + 0;
                textPesoTotalCajas.Text = "Peso Cajas";
                editPesoBruto.Select();
            } else if (value == 2)
            {
                editPesoNeto.Text = "" + WeightModel.getPesoNetoTemporal(movementId);
                editCantidadDeCajasSeleccionada.Text = "";
                editCajas.Text = "0";
                textPesoTotalCajas.Text = "Peso Cajas: 0";
                editPesoBruto.Select();
            } 
            else
            {
                FormMessage formMessage = new FormMessage("Reseteando pesos de cajas", description, 3);
                formMessage.ShowDialog();
            }
        }

        private void btnBorrarPesoTarasPollosMalos_Click(object sender, EventArgs e)
        {
            FrmConfirmation frmConfirmation = new FrmConfirmation("Eliminar Peso de Taras o Pollos Mal", "¿Estás seguro de que deseas eliminar el peso de las taras o pollos con afectaciones?");
            frmConfirmation.StartPosition = FormStartPosition.CenterParent;
            frmConfirmation.ShowDialog();
            if (FrmConfirmation.confirmation)
            {
                formWaiting = new FormWaiting(this, 4);
                formWaiting.ShowDialog();
            }
        }

        public async Task processToDeletePesoTaras()
        {
            int value = 0;
            String description = "";
            double pesoTaras = 0, pesoBruto = 0;
            await Task.Run(async () =>
            {
                bool enviadoAlCliente = DocumentModel.isItDocumentPrepedidoSendedToTheCustomer(idDocumento);
                if (enviadoAlCliente)
                {
                    String query = "SELECT * FROM " + LocalDatabase.TABLA_PESO + " WHERE " + LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = " +
                        movementId + " AND " + LocalDatabase.CAMPO_TIPO_PESO + " = " + WeightModel.TIPO_REAL;
                    WeightModel wm = WeightModel.getAWeight(query);
                    if (wm != null)
                    {
                        query = "UPDATE " + LocalDatabase.TABLA_PESO + " SET " + LocalDatabase.CAMPO_PESOCAJA_PESO + " = 0, " +
                            LocalDatabase.CAMPO_PESOPOLLOLESIONADO_PESO + " = 0, " + LocalDatabase.CAMPO_PESOPOLLOMUERTO_PESO + " = 0, " +
                            LocalDatabase.CAMPO_PESOPOLLOBAJOPESO_PESO + " = 0, " + LocalDatabase.CAMPO_PESOPOLLOGOLPEADO_PESO + " = 0, " +
                            LocalDatabase.CAMPO_PESOBRUTO_PESO + " = " + wm.pesoBruto + ", " +
                            LocalDatabase.CAMPO_PESONETO_PESO + " = " + wm.pesoBruto;
                        if (wm.cajas != 0)
                            query += ", " + LocalDatabase.CAMPO_CAJAS_PESO + " = " + wm.cajas;
                        query += " WHERE " + LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = " + movementId + " AND " +
                        LocalDatabase.CAMPO_TIPO_PESO + " = " + WeightModel.TIPO_TEMPORAL;
                        if (WeightModel.createUpdateOrDelete(query) > 0)
                        {
                            pesoTaras = wm.cajas;
                            pesoBruto = wm.pesoBruto;
                            value = 1;
                        }
                        else description = "No pudimos actualizar los datos del peso de taras y pollo con defectos!";
                    }
                    else description = "No pudimos encontrar la información del peso real!";
                }
                else
                {
                    double pesoBrutoAnterior = WeightModel.getPesoBrutoTemporal(movementId);
                    String query = "UPDATE " + LocalDatabase.TABLA_PESO + " SET " + LocalDatabase.CAMPO_PESOCAJA_PESO + " = 0, " +
                                 LocalDatabase.CAMPO_CAJAS_PESO + " = 0, " + LocalDatabase.CAMPO_PESOPOLLOLESIONADO_PESO + " = 0, " +
                                 LocalDatabase.CAMPO_PESOPOLLOMUERTO_PESO + " = 0, " + LocalDatabase.CAMPO_PESOPOLLOBAJOPESO_PESO + " = 0, " +
                                 LocalDatabase.CAMPO_PESOPOLLOGOLPEADO_PESO + " = 0, " +
                             LocalDatabase.CAMPO_PESONETO_PESO + " = " + pesoBrutoAnterior +
                             " WHERE " +
                             LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = " + movementId + " AND " +
                             LocalDatabase.CAMPO_TIPO_PESO + " = " + WeightModel.TIPO_TEMPORAL;
                    if (WeightModel.createUpdateOrDelete(query) > 0)
                        value = 2;
                    else description = "No pudimos actualizar los datos del peso de las taras y pollos con defectos!";
                }
            });
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            if (value == 1)
            {
                editOperacionesPesoTaras.Text = "Operaciones";
                editCajas.Text = "" + pesoTaras;
                editPesoNeto.Text = "" + pesoBruto;
                editCantidadDeCajasSeleccionada.Text = "" + 0;
                textPesoTotalCajas.Text = "Peso Cajas";
            } else if (value == 2)
            {
                editOperacionesPesoTaras.Text = "Operaciones";
                editPesoNeto.Text = "" + WeightModel.getPesoNetoTemporal(movementId);
                editCajas.Text = 0 + "";
                editCantidadDeCajasSeleccionada.Text = "" + 0;
            }
            else
            {
                FormMessage formMessage = new FormMessage("Reseteando Peso Taras", description, 3);
                formMessage.ShowDialog();
            }
        }

        private void FrmCalculateWeight_FormClosed(object sender, FormClosedEventArgs e)
        {
            WeightModel.deleteWieghtTemporal(movementId);
            cerrarPuertoSerial();
        }

        private void editPesoBruto_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void editPesoBruto_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void btnResetPeso_Click(object sender, EventArgs e)
        {
            processToResetWeight();
        }

        private void textInfoKgTara_Click(object sender, EventArgs e)
        {

        }

        private void btnReconectar_Click(object sender, EventArgs e)
        {
            formWaiting = new FormWaiting(this, 1);
            formWaiting.ShowDialog();
        }

        public async Task resetScaleConnection()
        {
            cerrarPuertoSerial();
            if (serialPortScale != null && serialPortScale.IsOpen)
            {
                serialPortScale.DiscardInBuffer();
                serialPortScale.DiscardOutBuffer();
                serialPortScale.DataReceived -= new SerialDataReceivedEventHandler(scale_DataReceived);
                serialPortScale.Close();
                serialPortScale.Dispose();
                initProcessReadScale();
            }
        }

        private void processToResetWeight()
        {
            FrmConfirmation frmConfirmation = new FrmConfirmation("Eliminar Pesos", "Este proceso ELIMINARÁ todos los pesos capturados BRUTO y TARAS, por " +
                "lo que tendrás que iniciar de cero.\r\n¿Estás seguro de que deseas eliminar todos los pesos capturados?");
            frmConfirmation.StartPosition = FormStartPosition.CenterScreen;
            frmConfirmation.ShowDialog();
            if (FrmConfirmation.confirmation)
            {
                formWaiting = new FormWaiting(this, 11); //processToResetAllWeights()
                formWaiting.ShowDialog();
            }
        }

        public async Task processToResetAllWeights()
        {
            int value = 0;
            String description = "";
            bool polloVivoChecked = checkBoxPesarPolloVivo.Checked;
            String textoCajas = editCajas.Text.Trim();
            double cantidadCajas = 0, pesoBruto = 0;
            await Task.Run(async () =>
            {
                bool enviadoAlCliente = DocumentModel.isItDocumentPrepedidoSendedToTheCustomer(idDocumento);
                if (enviadoAlCliente)
                {
                    WeightModel wm = WeightModel.getWeightReal(movementId);
                    if (wm != null)
                    {
                        String query = "UPDATE " + LocalDatabase.TABLA_PESO + " SET " + LocalDatabase.CAMPO_PESOCAJA_PESO + " = 0, " +
                            LocalDatabase.CAMPO_PESOPOLLOLESIONADO_PESO + " = 0, " + LocalDatabase.CAMPO_PESOPOLLOMUERTO_PESO + " = 0, " +
                            LocalDatabase.CAMPO_PESOPOLLOBAJOPESO_PESO + " = 0, " + LocalDatabase.CAMPO_PESOPOLLOGOLPEADO_PESO + " = 0, " +
                            LocalDatabase.CAMPO_PESOBRUTO_PESO + " = " + wm.pesoBruto + ", " +
                            LocalDatabase.CAMPO_PESONETO_PESO + " = " + wm.pesoNeto;
                        if (wm.cajas != 0)
                            query += ", " + LocalDatabase.CAMPO_CAJAS_PESO + " = " + wm.cajas;
                        query += " WHERE " + LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = " + movementId + " AND " +
                        LocalDatabase.CAMPO_TIPO_PESO + " = " + WeightModel.TIPO_TEMPORAL;
                        int response = WeightModel.createUpdateOrDelete(query);
                        if (response == 1)
                        {
                            cantidadCajas = wm.cajas;
                            pesoBruto = wm.pesoBruto;
                            value = 1;
                        }
                        else description = "No pudimos actualizar los datos para los pesos temporales!";
                    }
                    else description = "No pudimos encontrar la información de los pesos reales!";
                }
                else
                {
                    int response = WeightModel.updateWeightTemporal(0, 0, 0, 0, 0, 0, 0, 0, movementId);
                    if (response == 1)
                    {
                        int responseSave = await savePesosInRealTypeField(polloVivoChecked, textoCajas);
                        if (responseSave == 1)
                        {
                            WeightModel wm = WeightModel.getWeightReal(movementId);
                            if (wm != null)
                            {
                                cantidadCajas = wm.cajas;
                                pesoBruto = wm.pesoBruto;
                            }
                            value = 2;
                        }
                        else description = "No pudimos actualizar correctamente los pesos reales a partir de los temporales!";
                    }
                    else description = "No pudimos actualizar correctamente los pesos temporales!";
                }
            });
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            if (value == 1)
            {
                if (frmVentaNew != null)
                    await frmVentaNew.logicToAddItemLocal(pesoBruto);
                editOperacionesPesoTaras.Text = "Operaciones";
                editCajas.Text = "" + cantidadCajas;
                editPesoNeto.Text = "" +pesoBruto;
                editCantidadDeCajasSeleccionada.Text = "" + 0;
                textPesoTotalCajas.Text = "Peso Cajas";
            } else if (value == 2)
            {
                if (frmVentaNew != null)
                    await frmVentaNew.logicToAddItemLocal(0);
                editOperacionesPesoBruto.Text = "Operaciones";
                editOperacionesPesoTaras.Text = "Operaciones";
                textPesoTotalCajas.Text = "Peso Cajas";
                editCajas.Text = "" + cantidadCajas;
                editPesoNeto.Text = "" + pesoBruto;
                editCantidadDeCajasSeleccionada.Text = "" + 0;
                textPesoTotalCajas.Text = "Peso Cajas";
            } 
            else
            {
                FormMessage formMessage = new FormMessage("Reseteando Pesos", description, 3);
                formMessage.ShowDialog();
            }
        }

        private async Task<ExpandoObject> initializeSerialPortConnection()
        {
            dynamic response = new ExpandoObject();
            int valor = 0;
            String description = "";
            await Task.Run(async() =>
            {
                try
                {
                    serialPortScale = new SerialPort(puertoCom, baudRate, parityValue, dataBits, stopBitsValue);
                    serialPortScale.Handshake = Handshake.None;
                    serialPortScale.ReadTimeout = 500;
                    serialPortScale.WriteTimeout = 500;
                    serialPortScale.DtrEnable = true;
                    serialPortScale.RtsEnable = true;
                    serialPortScale.DataReceived += new SerialDataReceivedEventHandler(scale_DataReceived);
                    serialPortScale.Open();
                    leerPesoDeBascula = true;
                    ciclarLectura = true;
                }
                catch (Exception ex)
                {
                    SECUDOC.writeLog(ex.ToString());
                    valor = -1;
                    if (ex.Message.Contains("Access denied") || ex.Message.Contains("Acceso denegado"))
                        description = "No se encontró información en el puerto de la báscula seleccionado! \r\n" + ex.Message;
                    else description = "Error con el puerto de la báscula seleccionado validar que existe! \r\n" + ex.Message;
                }
                finally
                {
                    if (valor == -1)
                    {
                        ciclarLectura = false;
                    }
                }
            });
            response.valor = valor;
            response.description = description;
            return response;
        }

        private async Task readPortScale()
        {
            int count = 0;
            await Task.Run(async () => {
                while (ciclarLectura)
                {
                    try
                    {
                        if (serialPortScale.IsOpen)
                        {
                            if (leerPesoDeBascula)
                            {
                                try
                                {
                                    serialPortScale.Write("P");
                                } catch (TimeoutException ex)
                                {
                                    SECUDOC.writeLog(ex.ToString());
                                } catch (InvalidOperationException ex)
                                {
                                    SECUDOC.writeLog(ex.ToString());
                                } catch (ArgumentNullException ex)
                                {
                                    SECUDOC.writeLog(ex.ToString());
                                }
                            }
                        } else
                        {
                            if (leerPesoDeBascula)
                            {
                                serialPortScale = new SerialPort(puertoCom, baudRate, parityValue, dataBits, stopBitsValue);
                                serialPortScale.Handshake = Handshake.None;
                                serialPortScale.ReadTimeout = 500;
                                serialPortScale.WriteTimeout = 500;
                                serialPortScale.DataReceived += new SerialDataReceivedEventHandler(scale_DataReceived);
                                serialPortScale.Open();
                                serialPortScale.Write("P");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        SECUDOC.writeLog(ex.ToString());
                        this.BeginInvoke((Action)(() =>
                        {
                            editScaleInformation.Text = ex.Message+"\r\n";
                        }));
                        ciclarLectura = false;
                    }
                    Thread.Sleep(600);
                    count++;
                }
                if (serialPortScale != null && serialPortScale.IsOpen)
                {
                    serialPortScale.DiscardInBuffer();
                    serialPortScale.DiscardOutBuffer();
                    serialPortScale.DataReceived -= new SerialDataReceivedEventHandler(scale_DataReceived);
                    serialPortScale.Close();
                }
            });
        }

        private void scale_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (this.Enabled)
                {
                    //SerialPort sp = (SerialPort) sender;
                    kiloGramos += serialPortScale.ReadExisting();//0101.75 kg
                    if (kiloGramos.EndsWith("g"))
                    {
                        kiloGramos = kiloGramos.Substring(0, (kiloGramos.Length - 3));
                        this.BeginInvoke(new DelegadoAcceso(si_DataReceived2), new object[] { kiloGramos });
                        leerPesoDeBascula = true;
                        kiloGramos = "";
                    }
                }
            } catch (InvalidOperationException ex)
            {
                SECUDOC.writeLog(ex.ToString());
                leerPesoDeBascula = true;
            }
        }

        private void si_DataReceived2(String kg)
        {
            if (this.Enabled)
            {
                if (editPesoBruto != null && editPesoBruto.Focused)
                {
                    editPesoBruto.Text = kg;
                    editScaleInformation.Text = "Leyendo peso bruto...";
                }
                else if (editPesoTaras != null && editPesoTaras.Focused)
                {
                    editPesoTaras.Text = kg;
                    editScaleInformation.Text = "Leyendo peso tara...";
                }
            }
        }

    }
}
