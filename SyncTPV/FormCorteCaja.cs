using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using SyncTPV.Views;
using SyncTPV.Views.Withdrawals;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace SyncTPV
{
    public partial class FormCorteCaja : Form
    {
        public static int retiroId = 0, fcId = 0, idRetiroRealizado = 0;
        private int numeroRetiro = 0;
        string MensajeError = "", FechaInicial = "", FechaFinal = "";
        decimal Total = 0M;
        List<clsRetiro> lRetiros = new List<clsRetiro>();
        List<clsRetiro> lRetirosID = new List<clsRetiro>();
        private bool serverModeLAN = false;
        private int LIMIT = 30;
        private int progressFc = 0;
        private int lastIdFc = 0;
        private int totalFc = 0, queryTypeFc = 0;
        private String queryFc = "", queryTotalsFc = "";
        private DateTime lastLoading;
        private int firstVisibleRow;
        private ScrollBars gridScrollBars;
        private List<FormasDeCobroModel> fcList;
        private List<FormasDeCobroModel> fcListTemp;

        public FormCorteCaja()
        {
            fcList = new List<FormasDeCobroModel>();
            InitializeComponent();
            serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
            btnRetirar.Click += new EventHandler(this.btnRetirar_Click);

        }

        private void FrmCorteCaja_Load(object sender, EventArgs e)
        {
            resetearDatosFc(0);
            fillDataGridFormasDeCobro(0);
            dataGridFormaCobros.Focus();
            sendAllRetirosNotSended();
        }

        private async Task sendAllRetirosNotSended()
        {
            await Task.Run(async () =>
            {
                if (serverModeLAN)
                {
                    RetirosController.uploadRetirosLAN();
                } else
                {
                    RetirosController.uploadRetirosAPI();
                }
            });
        }

        private void hideScrollBars()
        {
            imgSinDatos.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.logosynctpvmoving, 300, 300);
            imgSinDatos.Visible = true;
            gridScrollBars = dataGridFormaCobros.ScrollBars;
            //dataGridItems.ScrollBars = ScrollBars.None;
        }

        private async void fillDataGridFormasDeCobro(int call)
        {
            hideScrollBars();
            lastLoading = DateTime.Now;
            fcListTemp = await getAllFormasDeCobro();
            if (fcListTemp != null)
            {
                progressFc += fcListTemp.Count;
                fcList.AddRange(fcListTemp);
                if (fcList.Count > 0 && dataGridFormaCobros.ColumnHeadersVisible == false)
                    dataGridFormaCobros.ColumnHeadersVisible = true;
                for (int i = 0; i < fcListTemp.Count; i++)
                {
                    int n = dataGridFormaCobros.Rows.Add();
                    dataGridFormaCobros.Rows[n].Cells[0].Value = fcListTemp[i].FORMA_COBRO_ID + "";
                    dataGridFormaCobros.Columns["idFcDgv"].Visible = false;
                    dataGridFormaCobros.Rows[n].Cells[1].Value = fcListTemp[i].NOMBRE;
                    double importe = MontoRetiroModel.getTotalWithdrawnFromAPaymentMethod(fcListTemp[i].FORMA_COBRO_ID, retiroId);
                    dataGridFormaCobros.Rows[n].Cells[2].Value = importe;
                }
                dataGridFormaCobros.PerformLayout();
                fcListTemp.Clear();
                if (fcList.Count > 0)
                    lastIdFc = Convert.ToInt32(fcList[fcList.Count - 1].FORMA_COBRO_ID);
                imgSinDatos.Visible = false;
                if (call == 0)
                {
                    clsTicket ticket = new clsTicket();
                    ticket.abrirCajonDinero();
                    ticket.printTicket();
                }
            }
            else
            {
                if (progressFc == 0)
                    imgSinDatos.Visible = true;
            }
            DataGridViewColumn column = dataGridFormaCobros.Columns[0];
            column.Width = 60;
        }

        private async Task<List<FormasDeCobroModel>> getAllFormasDeCobro()
        {
            List<FormasDeCobroModel> fcList = null;
            await Task.Run(async () =>
            {
                if (queryTypeFc == 0)
                {
                    totalFc = FormasDeCobroModel.getTotalOfFc();
                    if (totalFc == 0)
                    {
                        if (serverModeLAN)
                            await FormasDeCobroController.downloadAllFormasDeCobroLAN();
                        else await FormasDeCobroController.downloadAllFormasDeCobroAPI(0);
                    }
                    fcList = FormasDeCobroModel.getAllFormasDeCobro();
                    if (fcList != null && fcList.Count > 0)
                        totalFc = fcList.Count;
                }
            });
            return fcList;
        }

        private async Task fillDataGridRetirosRealizados(int idRetiro)
        {
            DataTable dt = await getAllWithdrawalsAmounts(idRetiro);
            if (dt != null)
            {
                dataGridViewRetirosRealizados.Refresh();
                if (dt.Rows.Count > 0)
                {
                    int count = dt.Rows.Count;
                    if (count > 0)
                    {
                        DataTable dtCloned = dt.Clone();
                        dtCloned.Columns[1].DataType = typeof(String);
                        dtCloned.Columns[3].DataType = typeof(String);
                        foreach (DataRow row in dt.Rows) { dtCloned.ImportRow(row); }
                        dtCloned.AsEnumerable().ToList<DataRow>().ForEach(r =>
                        {
                            r["Forma de Pago"] = FormasDeCobroModel.getANameFrromAFomaDeCobroWithId(Convert.ToInt32(r["Forma de Pago"].ToString()));
                            if (Convert.ToInt32(r["Estatus"].ToString()) == 1)
                            {
                                r["Estatus"] = "Enviado al Servidor";
                            }
                            else
                            {
                                r["Estatus"] = "Pendiente de Enviar";
                            }
                        }
                         );
                        dataGridViewRetirosRealizados.DataSource = dtCloned;
                        dataGridViewRetirosRealizados.Columns["id"].Visible = false;
                        imgSinDatosRetirosRealizados.Visible = false;
                    }
                    else
                    {
                        imgSinDatosRetirosRealizados.Visible = true;
                    }
                }
                else
                {
                    imgSinDatosRetirosRealizados.Visible = true;
                }
            }
        }

        private async Task<DataTable> getAllWithdrawalsAmounts(int idRetiro)
        {
            DataTable dt = null;
            await Task.Run(async () =>
            {
                String query = "SELECT " + LocalDatabase.CAMPO_ID_MONTORETIROS + ", " + LocalDatabase.CAMPO_FORMACOBROID_MONTORETIROS + " AS 'Forma de Pago', " +
                LocalDatabase.CAMPO_IMPORTE_MONTORETIROS + " AS 'Importe', " + LocalDatabase.CAMPO_ENVIADO_MONTORETIRO + " AS 'Estatus' FROM " +
                LocalDatabase.TABLA_MONTORETIROS + " WHERE " + LocalDatabase.CAMPO_RETIROID_MONTORETIRO + " = " + idRetiro;
                dt = MontoRetiroModel.getAllAmountsFromAWithdrawalDt(query);
            });
            return dt;
        }

        private void txtMonto_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) if (editAmount.Text.Trim() != "") validateAmount();
            Validaciones.SoloNumerosDecimales(e, editAmount.Text);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRetirar_Click(object sender, EventArgs e)
        {
            validateAmount();
        }

        private async Task validateAmount()
        {
            String amountText = editAmount.Text.Trim();
            if (amountText.Equals("") || fcId == 0)
            {
                FormMessage fm = new FormMessage("Alerta", "Oops a la forma de cobro seleccionada se tiene que agregar un importe válido!", 2);
                fm.ShowDialog();
            }
            else
            {
                double amount = Convert.ToDouble(amountText.Replace(",", ""));
                if (amount > 0 && fcId > 0)
                {
                    agregarAbonoAUnaFormaDeCobro(fcId, amount);
                }
                else if (amount == 0)
                {
                    if (eliminarAbonoDeUnaFormaDeCobro(fcId))
                    {
                        //updateItemRecyclerAmount(fcId, recyclerPosition);
                        fieldsClean();
                        resetearDatosFc(0);
                        fillDataGridFormasDeCobro(1);
                    }
                }
                else
                {
                    FormMessage fm = new FormMessage("Alerta", "Tienes que agregar un importe mayor o igual cero", 2);
                    fm.ShowDialog();
                }
            }
        }

        public void agregarAbonoAUnaFormaDeCobro(int fcId, double importe)
        {
            numeroRetiro = RetiroModel.checkTheLastWithdrawalNumber();
            if (numeroRetiro > 0 && retiroId == 0)
            {
                /** Primer registro de un retiro que no es el primero del día */
                retiroId = RetiroModel.createNewWithdrawal(numeroRetiro + 1, "", "");
                if (MontoRetiroModel.createNewWithdrawalAmount(fcId, importe, retiroId, MetodosGenerales.getCurrentDateAndHour(),
                    MetodosGenerales.getCurrentDateAndHour()) > 0)
                {
                    /*PopupNotifier popup = new PopupNotifier();
                    popup.Image = ClsMetodosGenerales.redimencionarBitmap(Properties.Resources.success_green, 100, 100);
                    popup.TitleColor = Color.Blue;
                    popup.TitleText = "Genial";
                    popup.ContentText = "Importe agregado";
                    popup.ContentColor = Color.Blue;
                    popup.Popup();*/
                    //updateItemRecyclerAmount(context, fcId, position);
                    resetearDatosFc(0);
                    fillDataGridFormasDeCobro(1);
                }
            }
            else if (numeroRetiro == 0 && retiroId == 0)
            {
                /** Primer registro del primer retiro del día */
                retiroId = RetiroModel.createNewWithdrawal(1, "", "");
                if (MontoRetiroModel.createNewWithdrawalAmount(fcId, importe, retiroId, MetodosGenerales.getCurrentDateAndHour(),
                    MetodosGenerales.getCurrentDateAndHour()) > 0)
                {
                    /*PopupNotifier popup = new PopupNotifier();
                    popup.Image = ClsMetodosGenerales.redimencionarBitmap(Properties.Resources.success_green, 100, 100);
                    popup.TitleColor = Color.Blue;
                    popup.TitleText = "Genial";
                    popup.ContentText = "Importe agregado";
                    popup.ContentColor = Color.Blue;
                    popup.Popup();*/
                    //updateItemRecyclerAmount(context, fcId, position);
                    resetearDatosFc(0);
                    fillDataGridFormasDeCobro(1);
                }
            }
            else if (numeroRetiro == 0 && retiroId > 0)
            {
                /** Registro que No es el primero pero Sí es el primer retiro del día */
                double retirado = MontoRetiroModel.getTotalWithdrawnFromAPaymentMethod(fcId, retiroId);
                if (retirado > 0)
                {
                    if (MontoRetiroModel.updateWithdrawalAmount(fcId, importe, retiroId, MetodosGenerales.getCurrentDateAndHour()))
                    {
                        /*PopupNotifier popup = new PopupNotifier();
                        popup.Image = ClsMetodosGenerales.redimencionarBitmap(Properties.Resources.success_green, 100, 100);
                        popup.TitleColor = Color.Blue;
                        popup.TitleText = "Genial";
                        popup.ContentText = "Importe Actualizado";
                        popup.ContentColor = Color.Blue;
                        popup.Popup();*/
                        //updateItemRecyclerAmount(fcId, position);
                        resetearDatosFc(0);
                        fillDataGridFormasDeCobro(1);
                    }
                }
                else
                {
                    if (MontoRetiroModel.createNewWithdrawalAmount(fcId, importe, retiroId, MetodosGenerales.getCurrentDateAndHour(),
                        MetodosGenerales.getCurrentDateAndHour()) > 0)
                    {
                        /*PopupNotifier popup = new PopupNotifier();
                        popup.Image = ClsMetodosGenerales.redimencionarBitmap(Properties.Resources.success_green, 100, 100);
                        popup.TitleColor = Color.Blue;
                        popup.TitleText = "Genial";
                        popup.ContentText = "Importe agregado";
                        popup.ContentColor = Color.Blue;
                        popup.Popup();*/
                        //updateItemRecyclerAmount(fcId, position);
                        resetearDatosFc(0);
                        fillDataGridFormasDeCobro(1);
                    }
                }
            }
            else if (numeroRetiro > 0 && retiroId > 0)
            {
                /** REgistro que no es el primero y que tampoco es el primer retiro del día */
                double retirado = MontoRetiroModel.getTotalWithdrawnFromAPaymentMethod(fcId, retiroId);
                if (retirado > 0)
                {
                    if (MontoRetiroModel.updateWithdrawalAmount(fcId, importe, retiroId, MetodosGenerales.getCurrentDateAndHour()))
                    {
                        /*PopupNotifier popup = new PopupNotifier();
                        popup.Image = ClsMetodosGenerales.redimencionarBitmap(Properties.Resources.success_green, 100, 100);
                        popup.TitleColor = Color.Blue;
                        popup.TitleText = "Genial";
                        popup.ContentText = "Importe Actualizado";
                        popup.ContentColor = Color.Blue;
                        popup.Popup();*/
                        //updateItemRecyclerAmount(fcId, position);
                        resetearDatosFc(0);
                        fillDataGridFormasDeCobro(1);
                    }
                }
                else
                {
                    if (MontoRetiroModel.createNewWithdrawalAmount(fcId, importe, retiroId, MetodosGenerales.getCurrentDateAndHour(),
                        MetodosGenerales.getCurrentDateAndHour()) > 0)
                    {
                        /*PopupNotifier popup = new PopupNotifier();
                        popup.Image = ClsMetodosGenerales.redimencionarBitmap(Properties.Resources.success_green, 100, 100);
                        popup.TitleColor = Color.Blue;
                        popup.TitleText = "Genial";
                        popup.ContentText = "Importe agregado";
                        popup.ContentColor = Color.Blue;
                        popup.Popup();*/
                        resetearDatosFc(0);
                        fillDataGridFormasDeCobro(1);
                    }
                }
            }
        }

        private Boolean eliminarAbonoDeUnaFormaDeCobro(int fcId)
        {
            Boolean removed = false;
            if (MontoRetiroModel.removeAnAmountFromAWithdrawal(fcId, retiroId))
            {
                int montos = MontoRetiroModel.checkForWithdrawalAmounts(retiroId);
                /*if (montos == 0)
                {
                    RetiroModel.removeAWithdrawal(retiroId);
                    retiroId = 0;
                    PopupNotifier popup = new PopupNotifier();
                    popup.Image = ClsMetodosGenerales.redimencionarBitmap(Properties.Resources.iconfinder_alert_1282954, 100,100);
                    popup.TitleColor = Color.Blue;
                    popup.TitleText = "Genial";
                    popup.ContentText = "Retiro eliminado!";
                    popup.ContentColor = Color.Blue;
                    popup.Popup();
                }
                else
                {
                    PopupNotifier popup = new PopupNotifier();
                    popup.Image = ClsMetodosGenerales.redimencionarBitmap(Properties.Resources.iconfinder_alert_1282954, 100, 100);
                    popup.TitleColor = Color.Blue;
                    popup.TitleText = "Genial";
                    popup.ContentText = "Monto del retiro eliminado!";
                    popup.ContentColor = Color.Blue;
                    popup.Popup();
                }*/
                removed = true;
            }
            return removed;
        }

        private void dtGridFormasDeCobro_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indexRow = e.RowIndex;
            if (indexRow >= 0)
            {
                var Nombre = dataGridFormaCobros.Rows[e.RowIndex].Cells[1].Value;
                var id = dataGridFormaCobros.Rows[e.RowIndex].Cells[0].Value;
                //var Id = dtGridFormaPago.Rows[e.RowIndex].Cells[0].Value;
                string NombreFormaPago = (string)Nombre;
                fcId = Convert.ToInt32(id);
                //GeneralTxt.IDFORMAPAGO = idFormaPago;
                editFormaDeCobro.Text = NombreFormaPago;
                editAmount.Focus();
                if (retiroId > 0)
                {
                    double retirado = MontoRetiroModel.getTotalWithdrawnFromAPaymentMethod(fcId, retiroId);
                    if (retirado == 0)
                    {
                        editAmount.Text = "";
                    }
                    else
                    {
                        editAmount.Text = "" + retirado;
                    }
                }
                else
                {
                    editAmount.Text = "";
                }
            }
        }

        private void frmCorteCaja_FormClosed(object sender, FormClosedEventArgs e)
        {
            validateCurrentWithdrawal(0);
        }

        private void validateCurrentWithdrawal(int call)
        {
            if (retiroId <= 0)
            {
                if (call == 0)
                    this.Close();
                else if (call == 1)
                {
                    retiroId = 0;
                    tabControlCortesDeCaja.SelectedIndex = 1;
                }
            }
            else
            {
                FrmConfirmation fc = new FrmConfirmation("Cancelación", "Desea cancelar el retiro actual?");
                fc.StartPosition = FormStartPosition.CenterScreen;
                fc.ShowDialog();
                if (FrmConfirmation.confirmation)
                {
                    int montos = MontoRetiroModel.checkForWithdrawalAmounts(retiroId);
                    if (montos > 0)
                    {
                        if (MontoRetiroModel.removeAllAmountsFromAWithdrawal(retiroId))
                        {
                            if (RetiroModel.removeAWithdrawal(retiroId))
                            {
                                retiroId = 0;
                                if (call == 0)
                                    this.Close();
                                else if (call == 1)
                                {
                                    retiroId = 0;
                                    tabControlCortesDeCaja.SelectedIndex = 1;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (RetiroModel.removeAWithdrawal(retiroId))
                        {
                            retiroId = 0;
                            if (call == 0)
                                this.Close();
                            else if (call == 1)
                            {
                                retiroId = 0;
                                tabControlCortesDeCaja.SelectedIndex = 1;
                            }
                        }
                    }
                }
            }
        }

        private void btnTerminarRetiro_Click(object sender, EventArgs e)
        {
            if (retiroId == 0)
            {
                FormMessage formMessage = new FormMessage("Retiro No Encontrado", "No puedes terminar un Retiro vacio!", 2);
                formMessage.ShowDialog();
            } else
            {
                double total = 0;
                String msg = "";
                String msg1 = "";
                List<FormasDeCobroModel> fcList = FormasDeCobroModel.getAllFormasDeCobro();
                if (fcList != null)
                {
                    for (int i = 0; i < fcList.Count; i++)
                    {
                        if (fcList[i].FORMA_COBRO_ID != 0)
                        {
                            double montoAretirar = MontoRetiroModel.getTotalWithdrawnFromAPaymentMethod(fcList[i].FORMA_COBRO_ID, retiroId);
                            if (montoAretirar > 0)
                            {
                                String montoRetirando = montoAretirar.ToString("C", CultureInfo.CurrentCulture);
                                msg += fcList[i].NOMBRE + " " + montoRetirando + " MXN \r\n";
                                total += montoAretirar;
                            }
                            else
                            {
                                fcList.RemoveAt(i);
                                i = i - 1;
                            }
                        }
                        else
                        {
                            fcList.RemoveAt(i);
                            i = i - 1;
                        }
                    }
                    String montoTotal = total.ToString("C", CultureInfo.CurrentCulture);
                    msg1 = "Total = " + montoTotal + " MXN";
                }
                FrmTerminateWithdrawal fcw = new FrmTerminateWithdrawal(retiroId, msg + msg1, FrmTerminateWithdrawal.TERMINAR_RETIRO);
                fcw.StartPosition = FormStartPosition.CenterScreen;
                fcw.ShowDialog();
                if (FrmTerminateWithdrawal.confirmation) {
                    terminateWithdrawal(fcList);
                }
            }
        }

        private void tabControlCortesDeCaja_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlCortesDeCaja.SelectedIndex == 0)
            {
                idRetiroRealizado = 0;
            }
            else if (tabControlCortesDeCaja.SelectedIndex == 1)
            {
                tabControlCortesDeCaja.SelectedIndex = 0;
                validateCurrentWithdrawal(1);
                fillComboWithdrawals();
            }
        }

        private async Task fillComboWithdrawals()
        {
            List<RetiroModel> retirosList = await getAllWithdrawals();
            if (retirosList != null)
            {
                //Setup data binding
                this.comboBoxSeleccionarRetiro.DataSource = retirosList;
                this.comboBoxSeleccionarRetiro.ValueMember = "id";
                this.comboBoxSeleccionarRetiro.DisplayMember = "numero";
                // make it readonly
                this.comboBoxSeleccionarRetiro.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            comboBoxSeleccionarRetiro.Focus();
        }

        private async Task<List<RetiroModel>> getAllWithdrawals()
        {
            List<RetiroModel> retirosList = null;
            await Task.Run(async () =>
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_RETIROS;
                retirosList = RetiroModel.getAllWithdrawals(query);
            });
            return retirosList;
        }

        private void editAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void dataGridFormaCobros_Scroll(object sender, ScrollEventArgs e)
        {
            if (fcList.Count < totalFc && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
                {
                    int cuntDisplayed = dataGridFormaCobros.Rows.Count - getDisplayedRowsCount();
                    if (e.NewValue >= cuntDisplayed)
                    {
                        //prevent loading from autoscroll.
                        TimeSpan ts = DateTime.Now - lastLoading;
                        if (ts.TotalMilliseconds > 100)
                        {
                            firstVisibleRow = e.NewValue;
                            //dataGridItems.PerformLayout();
                            fillDataGridFormasDeCobro(1);
                        }
                        else
                        {
                            dataGridFormaCobros.FirstDisplayedScrollingRowIndex = e.OldValue;
                        }
                    }
                }
            }
        }

        private int getDisplayedRowsCount()
        {
            int count = dataGridFormaCobros.Rows[dataGridFormaCobros.FirstDisplayedScrollingRowIndex].Height;
            count = dataGridFormaCobros.Height / count;
            return count;
        }

        private void editAmount_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                validateAmount();
            }
        }

        private void dtGridFormaCobros_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var Nombre = dataGridFormaCobros.CurrentRow.Cells["Nombre"].Value.ToString();
                var id = Convert.ToInt32(dataGridFormaCobros.CurrentRow.Cells["Id"].Value.ToString());
                //var Id = dtGridFormaPago.Rows[e.RowIndex].Cells[0].Value;
                string NombreFormaPago = (string)Nombre;
                fcId = Convert.ToInt32(id);
                //GeneralTxt.IDFORMAPAGO = idFormaPago;
                editFormaDeCobro.Text = NombreFormaPago;
                editAmount.Focus();
                if (retiroId > 0)
                {
                    double retirado = MontoRetiroModel.getTotalWithdrawnFromAPaymentMethod(fcId, retiroId);
                    if (retirado == 0)
                    {
                        editAmount.Text = "";
                    }
                    else
                    {
                        editAmount.Text = "" + retirado;
                    }
                }
                else
                {
                    editAmount.Text = "";
                }
            }
        }

        private void comboBoxSeleccionarRetiro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSeleccionarRetiro.Focused)
            {
                String idRetiro = comboBoxSeleccionarRetiro.SelectedValue.ToString();
                if (idRetiro.ToString() != null && !idRetiro.ToString().Equals(""))
                {
                    idRetiroRealizado = Convert.ToInt32(idRetiro.ToString());
                    String query = "SELECT * FROM " + LocalDatabase.TABLA_RETIROS + " WHERE "+ LocalDatabase.CAMPO_ID_RETIRO + " = " + idRetiroRealizado;
                    RetiroModel rm = RetiroModel.getARecord(query);
                    if (rm != null)
                    {
                        textConcept.Text = ""+rm.concept;
                        textDescription.Text = ""+rm.description;
                    }
                    fillDataGridRetirosRealizados(idRetiroRealizado);
                }
            }
        }

        private void btnPrintRetiro_Click(object sender, EventArgs e)
        {
            PopupNotifier popup = new PopupNotifier();
            popup.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.success_green, 100, 100);
            popup.TitleColor = Color.Blue;
            popup.TitleText = "Conectando";
            popup.ContentText = "Impresión en proceso";
            popup.ContentColor = Color.Blue;
            popup.Popup();
            clsTicket Ticket = new clsTicket();
            Ticket.CorteCaja(idRetiroRealizado, serverModeLAN);
        }

        private async Task terminateWithdrawal(List<FormasDeCobroModel> mrmList)
        {
            if (RetiroModel.updateDateForTerminateWithdrawalOfMoney(retiroId))
            {
                if (ConfiguracionModel.printPermissionIsActivated())
                {
                    clsTicket Ticket = new clsTicket();
                    Ticket.CorteCaja(retiroId, serverModeLAN);
                }
                PopupNotifier popup = new PopupNotifier();
                popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                popup.TitleColor = Color.Blue;
                popup.TitleText = "Genial";
                popup.ContentText = "Retiro procesado correctamente";
                popup.ContentColor = Color.Blue;
                popup.Popup();
                SendWithdrawalsService sw = new SendWithdrawalsService();
                dynamic response = new ExpandoObject();
                if (ConfiguracionModel.isLANPermissionActivated())
                    response = await sw.handleActionSendRetirosLAN(retiroId);
                else response = await sw.handleActionSendRetiros(retiroId);
                retiroId = 0;
                this.Close();
            }
        }

        private void fieldsClean()
        {
            editFormaDeCobro.Text = "";
            editAmount.Text = "";
            Total = 0;
            fcId = 0;
        }

        private void resetearDatosFc(int queryType)
        {
            this.queryTypeFc = queryType;
            queryFc = "";
            queryTotalsFc = "";
            totalFc = 0;
            lastIdFc = 0;
            progressFc = 0;
            fcList.Clear();
            dataGridFormaCobros.Rows.Clear();
        }

    }
}

