using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using SyncTPV.Views.Withdrawals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace SyncTPV.Views.Entries
{
    public partial class FormIngresos : Form
    {
        public int call = 0;
        private int LIMIT = 50;
        private int progressIngreso = 0, progressFc = 0;
        private int lastId = 0, lastIdFc = 0;
        private int totalRecords = 0, totalRecordsFc = 0, queryTypeIngreso = 0, queryTypeFc = 0;
        private String query = "", queryFc = "", queryTotals = "", queryTotalsFc = "", ingresoCodeOrName = "";
        private DateTime lastLoading, lastLoadingFc;
        private int firstVisibleRow, firstVisibleRowFc;
        private ScrollBars gridScrollBars, gridScrollBarsFc;
        private List<IngresoModel> ingresoList;
        private List<IngresoModel> ingresoListTemp;
        private List<FormasDeCobroModel> fcList;
        private List<FormasDeCobroModel> fcListTemp;
        private FormWaiting frmWaiting;
        private int userId = 0, idIngreso = 0, numeroIngreso = 0;
        private bool createMode = false;
        private bool serverModeLAN = false;

        public FormIngresos()
        {
            ingresoList = new List<IngresoModel>();
            fcList = new List<FormasDeCobroModel>();
            InitializeComponent();
            serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
            imgSinDatos.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.sindatos, 297, 205);
            btnNuevo.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.add_white, 35, 35);
            userId = ClsRegeditController.getIdUserInTurn();
        }

        private void FormIngresos_Load(object sender, EventArgs e)
        {
            loadInformation();
            sendAllIngresosNotSended();
        }

        private async Task sendAllIngresosNotSended()
        {
            await Task.Run(async () =>
            {
                if (serverModeLAN)
                {
                    IngresosController.uploadIngresosLAN();
                } else
                {
                    IngresosController.uploadIngresosAPI();
                }
            });
        }

        private async Task loadInformation()
        {
            tableLayoutPanelContent.ColumnStyles[1].SizeType = SizeType.Absolute;
            tableLayoutPanelContent.ColumnStyles[1].Width = 0;
            await fillDataGridViewIngresos();
        }
        private void hideScrollBars()
        {
            imgSinDatos.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.logosynctpvmoving, 297, 205);
            imgSinDatos.Visible = true;
            gridScrollBars = dataGridViewIngresos.ScrollBars;
            //dataGridItems.ScrollBars = ScrollBars.None;
        }

        private async Task fillDataGridViewIngresos()
        {
            hideScrollBars();
            lastLoading = DateTime.Now;
            ingresoListTemp = await getAllIngresos();
            if (ingresoListTemp != null)
            {
                progressIngreso += ingresoListTemp.Count;
                ingresoList.AddRange(ingresoListTemp);
                if (ingresoList.Count > 0 && dataGridViewIngresos.ColumnHeadersVisible == false)
                    dataGridViewIngresos.ColumnHeadersVisible = true;
                for (int i = 0; i < ingresoListTemp.Count; i++)
                {
                    int n = dataGridViewIngresos.Rows.Add();
                    dataGridViewIngresos.Rows[n].Cells[0].Value = ingresoListTemp[i].id + "";
                    dataGridViewIngresos.Columns["idIngresoDgv"].Visible = false;
                    dataGridViewIngresos.Rows[n].Cells[1].Value = ingresoListTemp[i].number;
                    dataGridViewIngresos.Rows[n].Cells[2].Value = ingresoListTemp[i].concept;
                    dataGridViewIngresos.Columns[2].Width = 200;
                    dataGridViewIngresos.Rows[n].Cells[3].Value = ingresoListTemp[i].description;
                    dataGridViewIngresos.Columns[3].Width = 250;
                    dataGridViewIngresos.Rows[n].Cells[4].Value = ingresoListTemp[i].dateTime;
                    double importe = MontoIngresoModel.getTotalOfAnEntry(ingresoListTemp[i].id);
                    dataGridViewIngresos.Rows[n].Cells[5].Value = importe.ToString("C", CultureInfo.CurrentCulture)+" MXN";
                    if (ingresoListTemp[i].sended == 0)
                        dataGridViewIngresos.Rows[n].Cells[6].Value = "No Enviado";
                    else dataGridViewIngresos.Rows[n].Cells[6].Value = "Enviado";
                }
                dataGridViewIngresos.PerformLayout();
                ingresoListTemp.Clear();
                lastId = Convert.ToInt32(ingresoList[ingresoList.Count - 1].id);
                imgSinDatos.Visible = false;
                //dataGridItems.Focus();
            }
            else
            {
                if (progressIngreso == 0)
                    imgSinDatos.Visible = true;
            }
            textTotalRecords.Text = "Ingresos: " + totalRecords.ToString().Trim();
            //reset displayed row
            if (firstVisibleRow > -1)
            {
                showScrollBars();
                if (ingresoList.Count > 0)
                {
                    dataGridViewIngresos.FirstDisplayedScrollingRowIndex = firstVisibleRow;
                    imgSinDatos.Visible = false;
                }
            }
        }

        private void showScrollBars()
        {
            dataGridViewIngresos.ScrollBars = gridScrollBars;
            imgSinDatos.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.sindatos, 297, 205);
        }

        public async Task<List<IngresoModel>> getAllIngresos()
        {
            List<IngresoModel> ingresos = null;
            await Task.Run(async () =>
            {
                if (queryTypeIngreso == 0)
                {
                    query = "SELECT * FROM "+LocalDatabase.TABLA_INGRESO+" WHERE "+LocalDatabase.CAMPO_ID_INGRESO+" > "+lastId+
                    " ORDER BY "+LocalDatabase.CAMPO_ID_INGRESO+" ASC LIMIT "+LIMIT;
                    queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_INGRESO+" WHERE "+LocalDatabase.CAMPO_ID_INGRESO+" > 0";
                    ingresos = IngresoModel.getListIngresos(query);
                    totalRecords = IngresoModel.getIntValue(queryTotals);
                }
            });
            return ingresos;
        }

        private void dataGridViewIngresos_Scroll(object sender, ScrollEventArgs e)
        {
            if (ingresoList.Count < totalRecords && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
                {
                    int cuntDisplayed = dataGridViewIngresos.Rows.Count - getDisplayedRowsCount();
                    if (e.NewValue >= cuntDisplayed)
                    {
                        //prevent loading from autoscroll.
                        TimeSpan ts = DateTime.Now - lastLoading;
                        if (ts.TotalMilliseconds > 100)
                        {
                            firstVisibleRow = e.NewValue;
                            //dataGridItems.PerformLayout();
                            fillDataGridViewIngresos();
                        }
                        else
                        {
                            dataGridViewIngresos.FirstDisplayedScrollingRowIndex = e.OldValue;
                        }
                    }
                }
            }
        }

        private void dataGridViewIngresos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {

            }
        }

        private void btnCreateOrUpdate_Click(object sender, EventArgs e)
        {
            processToSaveEntry();
        }

        private async Task processToSaveEntry()
        {
            if (idIngreso != 0)
            {
                if (createMode)
                {
                    double total = 0;
                    String msg = "";
                    String msg1 = "";
                    List<FormasDeCobroModel> fcList = FormasDeCobroModel.getAllFormasDeCobro();
                    if (fcList != null)
                    {
                        for (int i = 0; i < fcList.Count; i++)
                        {
                            double montoAretirar = MontoIngresoModel.getTotalEntryFromAPaymentMethod(fcList[i].FORMA_COBRO_ID, idIngreso);
                            if (montoAretirar > 0)
                            {
                                String montoRetirando = montoAretirar.ToString("C", CultureInfo.CurrentCulture);
                                msg += fcList[i].NOMBRE + " " + montoRetirando + " MXN\r\n";
                                total += montoAretirar;
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
                    FrmTerminateWithdrawal fcw = new FrmTerminateWithdrawal(idIngreso, msg + msg1, FrmTerminateWithdrawal.TERMINAR_INGRESO);
                    fcw.StartPosition = FormStartPosition.CenterScreen;
                    fcw.ShowDialog();
                    if (FrmTerminateWithdrawal.confirmation)
                    {
                        terminateEntry(fcList);
                    }
                }
                else
                {

                }
            }
            else
            {
                FormMessage formMessage = new FormMessage("Ingreso No Encontrado", "No puedes terminar un Ingreso vacio!", 2);
                formMessage.ShowDialog();
            }
        }

        private async Task terminateEntry(List<FormasDeCobroModel> mrmList)
        {
            if (IngresoModel.updateDateForTerminateIngresoOfMoney(idIngreso))
            {
                if (ConfiguracionModel.printPermissionIsActivated())
                {
                    clsTicket Ticket = new clsTicket();
                    await Ticket.IngresoACaja(idIngreso, serverModeLAN);
                }
                PopupNotifier popup = new PopupNotifier();
                popup.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.success_green, 100, 100);
                popup.TitleColor = Color.BlueViolet;
                popup.TitleText = "Genial";
                popup.ContentText = "Ingreso procesado correctamente";
                popup.ContentColor = Color.BlueViolet;
                popup.Popup();
                SendIngresoController sic = new SendIngresoController();
                dynamic response = new ExpandoObject();
                if (ConfiguracionModel.isLANPermissionActivated())
                    response = await sic.handleActionSendIngresoLAN(idIngreso);
                else response = await sic.handleActionSendIngresosAPI(idIngreso);
                idIngreso = 0;
                this.Close();
            }
        }

        private void dataGridViewFc_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (dataGridViewFc.Rows[e.RowIndex].Cells["amountFcIngresoDgv"].Selected)
                {
                    int idFc = Convert.ToInt32(dataGridViewFc.Rows[e.RowIndex].Cells[0].Value.ToString().Trim());
                    if (idFc != 0)
                    {
                        if (dataGridViewFc.Rows[e.RowIndex].Cells["amountFcIngresoDgv"].Value == null)
                            dataGridViewFc.Rows[e.RowIndex].Cells["amountFcIngresoDgv"].Value = 0;
                        String amountText = dataGridViewFc.Rows[e.RowIndex].Cells["amountFcIngresoDgv"].Value.ToString().Trim();
                        amountText = amountText.Replace("$","").Replace(",","");
                        double amount = 0;
                        bool result = Double.TryParse(amountText, out amount);
                        if (result)
                        {
                            if (amount > 0)
                            {
                                logicToAddEntryAmounts(idFc, amount);
                            }
                            else if (amount == 0)
                            {
                                if (eliminarIngresoDeUnaFormaDeCobro(idFc))
                                {
                                    //updateItemRecyclerAmount(fcId, recyclerPosition);
                                    //fieldsClean();
                                    //fillWithdrawalsGridView();
                                    resetearValoresFc(0);
                                    fillDataGridViewFc();
                                }
                            }
                            else
                            {
                                FormMessage fm = new FormMessage("Alerta", "Tienes que agregar un importe mayor o igual cero", 3);
                                fm.ShowDialog();
                            }
                        }
                        else
                        {
                            FormMessage formMessage = new FormMessage("Exception", "El importe tiene que ser unicamente caracteres numéricos", 3);
                            formMessage.ShowDialog();
                            resetearValoresFc(0);
                            fillDataGridViewFc();
                        }
                    } else
                    {
                        FormMessage formMessage = new FormMessage("Excepción", "No se pudo obtener la información de la Forma de Pago", 3);
                        formMessage.ShowDialog();
                    }
                }
            }
        }

        private async Task logicToAddEntryAmounts(int fcId, double amount)
        {
            agregarIngresoAUnaFormaDeCobro(fcId, amount);
        }

        public void agregarIngresoAUnaFormaDeCobro(int fcId, double importe)
        {
            numeroIngreso = IngresoModel.checkTheLastEntryNumber();
            if (numeroIngreso > 0 && idIngreso == 0)
            {
                /** Primer registro de un retiro que no es el primero del día */
                String userCode = UserModel.getCode(userId);                
                idIngreso = IngresoModel.createIngreso(numeroIngreso +1, userId, userCode, "", "");
                if (MontoIngresoModel.createEntryAmount(fcId, importe, idIngreso, MetodosGenerales.getCurrentDateAndHour(),
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
                    //fieldsClean();
                    resetearValoresFc(0);
                    fillDataGridViewFc();
                    resetearValoresIngresos(0);
                    fillDataGridViewIngresos();
                }
            }
            else if (numeroIngreso == 0 && idIngreso == 0)
            {
                /** Primer registro del primer retiro del día */
                String userCode = UserModel.getCode(userId);
                idIngreso = IngresoModel.createIngreso(1, userId, userCode, "", "");
                if (MontoIngresoModel.createEntryAmount(fcId, importe, idIngreso, MetodosGenerales.getCurrentDateAndHour(),
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
                    //fieldsClean();
                    resetearValoresFc(0);
                    fillDataGridViewFc();
                    resetearValoresIngresos(0);
                    fillDataGridViewIngresos();
                }
            }
            else if (numeroIngreso == 0 && idIngreso > 0)
            {
                /** Registro que No es el primero pero Sí es el primer retiro del día */
                double ingresado = MontoIngresoModel.getTotalEntryFromAPaymentMethod(fcId, idIngreso);
                if (ingresado > 0)
                {
                    if (MontoIngresoModel.updateEntryAmount(fcId, importe, idIngreso, MetodosGenerales.getCurrentDateAndHour()))
                    {
                        /*PopupNotifier popup = new PopupNotifier();
                        popup.Image = ClsMetodosGenerales.redimencionarBitmap(Properties.Resources.success_green, 100, 100);
                        popup.TitleColor = Color.Blue;
                        popup.TitleText = "Genial";
                        popup.ContentText = "Importe Actualizado";
                        popup.ContentColor = Color.Blue;
                        popup.Popup();*/
                        //updateItemRecyclerAmount(fcId, position);
                        //fieldsClean();
                        resetearValoresFc(0);
                        fillDataGridViewFc();
                        resetearValoresIngresos(0);
                        fillDataGridViewIngresos();
                    }
                }
                else
                {
                    if (MontoIngresoModel.createEntryAmount(fcId, importe, idIngreso, MetodosGenerales.getCurrentDateAndHour(),
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
                        //fieldsClean();
                        resetearValoresFc(0);
                        fillDataGridViewFc();
                        resetearValoresIngresos(0);
                        fillDataGridViewIngresos();
                    }
                }
            }
            else if (numeroIngreso > 0 && idIngreso > 0)
            {
                /** REgistro que no es el primero y que tampoco es el primer retiro del día */
                double ingresado = MontoIngresoModel.getTotalEntryFromAPaymentMethod(fcId, idIngreso);
                if (ingresado > 0)
                {
                    if (MontoIngresoModel.updateEntryAmount(fcId, importe, idIngreso, MetodosGenerales.getCurrentDateAndHour()))
                    {
                        /*PopupNotifier popup = new PopupNotifier();
                        popup.Image = ClsMetodosGenerales.redimencionarBitmap(Properties.Resources.success_green, 100, 100);
                        popup.TitleColor = Color.Blue;
                        popup.TitleText = "Genial";
                        popup.ContentText = "Importe Actualizado";
                        popup.ContentColor = Color.Blue;
                        popup.Popup();*/
                        //updateItemRecyclerAmount(fcId, position);
                        //fieldsClean();
                        resetearValoresFc(0);
                        fillDataGridViewFc();
                        resetearValoresIngresos(0);
                        fillDataGridViewIngresos();
                    }
                }
                else
                {
                    if (MontoIngresoModel.createEntryAmount(fcId, importe, idIngreso, MetodosGenerales.getCurrentDateAndHour(),
                        MetodosGenerales.getCurrentDateAndHour()) > 0)
                    {
                        /*PopupNotifier popup = new PopupNotifier();
                        popup.Image = ClsMetodosGenerales.redimencionarBitmap(Properties.Resources.success_green, 100, 100);
                        popup.TitleColor = Color.Blue;
                        popup.TitleText = "Genial";
                        popup.ContentText = "Importe agregado";
                        popup.ContentColor = Color.Blue;
                        popup.Popup();*/
                        //fieldsClean();
                        resetearValoresFc(0);
                        fillDataGridViewFc();
                        resetearValoresIngresos(0);
                        fillDataGridViewIngresos();
                    }
                }
            }
        }

        private void btnCancelOrDelete_Click(object sender, EventArgs e)
        {
            if (idIngreso > 0)
            {
                if (createMode)
                {
                    int response = validateDeleteCurrentEntry(0);
                    if (response == 1)
                    {
                        tableLayoutPanelContent.ColumnStyles[1].SizeType = SizeType.Absolute;
                        tableLayoutPanelContent.ColumnStyles[1].Width = 0;
                        resetearValoresFc(0);
                        resetearValoresIngresos(0);
                        fillDataGridViewIngresos();
                        btnNuevo.Visible = true;
                        idIngreso = 0;
                    }
                    else if (response == 2)
                    {

                    }
                } else
                {
                    idIngreso = 0;
                    tableLayoutPanelContent.ColumnStyles[1].SizeType = SizeType.Absolute;
                    tableLayoutPanelContent.ColumnStyles[1].Width = 0;
                    resetearValoresFc(0);
                    resetearValoresIngresos(0);
                    btnNuevo.Visible = true;
                }
            } else
            {
                tableLayoutPanelContent.ColumnStyles[1].SizeType = SizeType.Absolute;
                tableLayoutPanelContent.ColumnStyles[1].Width = 0;
                btnNuevo.Visible = true;
                idIngreso = 0;
            }
        }

        private Boolean eliminarIngresoDeUnaFormaDeCobro(int fcId)
        {
            Boolean removed = false;
            if (MontoIngresoModel.removeAnAmountFromAnEntry(fcId, idIngreso))
            {
                int montos = MontoIngresoModel.checkForEntryAmounts(idIngreso);
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

        private void dataGridViewFc_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                processToSaveEntry();
            } else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnCreateOrUpdate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.F10))
            {
                processToSaveEntry();
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Escape))
                this.Close();
        }

        private void btnCancelOrDelete_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.F10))
            {
                processToSaveEntry();
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Escape))
                this.Close();
        }

        private void btnClose_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Escape))
                this.Close();
        }

        private void dataGridViewIngresos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private int getDisplayedRowsCount()
        {
            int count = dataGridViewIngresos.Rows[dataGridViewIngresos.FirstDisplayedScrollingRowIndex].Height;
            count = dataGridViewIngresos.Height / count;
            return count;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (idIngreso != 0)
            {

            } else
            {
                createMode = true;
                resetearValoresFc(0);
                tableLayoutPanelContent.ColumnStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanelContent.ColumnStyles[1].Width = 50;
                fillDataGridViewFc();
                dataGridViewFc.Select();
                btnNuevo.Visible = false;
            }
        }

        private void hideScrollBarsFc()
        {
            imgSinDatosFc.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.logosynctpvmoving, 197, 122);
            imgSinDatosFc.Visible = true;
            gridScrollBars = dataGridViewFc.ScrollBars;
            //dataGridItems.ScrollBars = ScrollBars.None;
        }

        private async Task fillDataGridViewFc()
        {
            hideScrollBarsFc();
            lastLoading = DateTime.Now;
            fcListTemp = await getAllFc();
            if (fcListTemp != null)
            {
                progressFc += fcListTemp.Count;
                fcList.AddRange(fcListTemp);
                if (fcList.Count > 0 && dataGridViewFc.ColumnHeadersVisible == false)
                    dataGridViewFc.ColumnHeadersVisible = true;
                for (int i = 0; i < fcListTemp.Count; i++)
                {
                    int n = dataGridViewFc.Rows.Add();
                    dataGridViewFc.Rows[n].Cells[0].Value = fcListTemp[i].FORMA_COBRO_ID + "";
                    dataGridViewFc.Columns["idFcIngresoDgv"].Visible = false;
                    dataGridViewFc.Rows[n].Cells[1].Value = fcListTemp[i].NOMBRE;
                    dataGridViewFc.Columns[1].ReadOnly = true;
                    if (idIngreso != 0)
                        dataGridViewFc.Rows[n].Cells[2].Value = MontoIngresoModel.getTotalEntryFromAPaymentMethod(fcListTemp[i].FORMA_COBRO_ID, 
                            idIngreso).ToString("C", CultureInfo.CurrentCulture);
                    else dataGridViewFc.Rows[n].Cells[2].Value = 0.ToString("C", CultureInfo.CurrentCulture);
                }
                dataGridViewFc.PerformLayout();
                fcListTemp.Clear();
                lastIdFc = Convert.ToInt32(fcList[fcList.Count - 1].FORMA_COBRO_ID);
                imgSinDatosFc.Visible = false;
                //dataGridItems.Focus();
            }
            else
            {
                if (progressFc == 0)
                    imgSinDatosFc.Visible = true;
            }
            //textTotalRecords.Text = "Ingresos: " + totalRecords.ToString().Trim();
            //reset displayed row
            if (firstVisibleRowFc > -1)
            {
                showScrollBarsFc();
                if (ingresoList.Count > 0)
                {
                    dataGridViewFc.FirstDisplayedScrollingRowIndex = firstVisibleRowFc;
                    imgSinDatosFc.Visible = false;
                }
            }
        }

        private void showScrollBarsFc()
        {
            dataGridViewFc.ScrollBars = gridScrollBars;
            imgSinDatosFc.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.sindatos, 197, 122);
        }

        private async Task<List<FormasDeCobroModel>> getAllFc()
        {
            List<FormasDeCobroModel> fcList = null;
            await Task.Run(async () =>
            {
                if (queryTypeFc == 0)
                {
                    totalRecordsFc = FormasDeCobroModel.getTotalOfFc();
                    if (totalRecordsFc == 0)
                    {
                        if (serverModeLAN)
                            await FormasDeCobroController.downloadAllFormasDeCobroLAN();
                        else await FormasDeCobroController.downloadAllFormasDeCobroAPI(0);
                    }
                    queryFc = "SELECT * FROM " + LocalDatabase.TABLA_FORMASCOBRO + " WHERE " + LocalDatabase.CAMPO_ID_FORMASCOBRO +
                    " > " + lastIdFc + " ORDER BY " + LocalDatabase.CAMPO_ID_FORMASCOBRO + " ASC LIMIT " + LIMIT;
                    queryTotalsFc = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_FORMASCOBRO;
                    fcList = FormasDeCobroModel.getAllFormasDeCobro(queryFc);
                    totalRecordsFc = FormasDeCobroModel.getIntValue(queryTotalsFc);
                }
            });
            return fcList;
        }
        private void dataGridViewFc_Scroll(object sender, ScrollEventArgs e)
        {
            if (fcList.Count < totalRecordsFc && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
                {
                    int cuntDisplayed = dataGridViewFc.Rows.Count - getDisplayedRowsCountFc();
                    if (e.NewValue >= cuntDisplayed)
                    {
                        //prevent loading from autoscroll.
                        TimeSpan ts = DateTime.Now - lastLoadingFc;
                        if (ts.TotalMilliseconds > 100)
                        {
                            firstVisibleRowFc = e.NewValue;
                            //dataGridItems.PerformLayout();
                            fillDataGridViewFc();
                        }
                        else
                        {
                            dataGridViewFc.FirstDisplayedScrollingRowIndex = e.OldValue;
                        }
                    }
                }
            }
        }

        private int getDisplayedRowsCountFc()
        {
            int count = dataGridViewFc.Rows[dataGridViewFc.FirstDisplayedScrollingRowIndex].Height;
            count = dataGridViewFc.Height / count;
            return count;
        }

        private void dataGridViewFc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormIngresos_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (idIngreso != 0 && createMode)
            {
                int response = validateDeleteCurrentEntry(0);
                if (response == 1)
                    idIngreso = 0;
                else if (response == 2)
                    e.Cancel = true;
                else
                {
                    
                }
            }
        }

        private int validateDeleteCurrentEntry(int call)
        {
            int response = 0;
            if (idIngreso <= 0)
            {
                if (call == 0)
                    this.Close();
                else if (call == 1)
                {
                    idIngreso = 0;
                    //tabControlCortesDeCaja.SelectedIndex = 1;
                }
            }
            else
            {
                FrmConfirmation fc = new FrmConfirmation("Cancelación", "Desea cancelar el Ingreso actual?");
                fc.StartPosition = FormStartPosition.CenterScreen;
                fc.ShowDialog();
                if (FrmConfirmation.confirmation)
                {
                    int montos = MontoIngresoModel.checkForEntryAmounts(idIngreso);
                    if (montos > 0)
                    {
                        if (MontoIngresoModel.removeAllAmountsFromAnEntry(idIngreso))
                        {
                            if (IngresoModel.removeAnEntry(idIngreso))
                            {
                                response = 1;
                            }
                        }
                    }
                    else
                    {
                        if (IngresoModel.removeAnEntry(idIngreso))
                        {
                            response = 1;
                        }
                    }
                } else
                {
                    response = 2;
                }
            }
            return response;
        }

        private void resetearValoresIngresos(int queryType)
        {
            this.queryTypeIngreso = queryType;
            query = "";
            queryTotals = "";
            totalRecords = 0;
            lastId = 0;
            progressIngreso = 0;
            ingresoList.Clear();
            dataGridViewIngresos.Rows.Clear();
        }

        private void resetearValoresFc(int queryType)
        {
            this.queryTypeFc = queryType;
            queryFc = "";
            queryTotalsFc = "";
            totalRecordsFc = 0;
            lastIdFc = 0;
            progressFc = 0;
            fcList.Clear();
            dataGridViewFc.Rows.Clear();
        }

    }
}
