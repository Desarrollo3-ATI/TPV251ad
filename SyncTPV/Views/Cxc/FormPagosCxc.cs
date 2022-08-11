using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncTPV.Views.Cxc
{
    public partial class FormPagosCxc : Form
    {
        private int idCxc = 0;
        private int LIMIT = 17;
        private int progress = 0;
        private int lastId = 0;
        private int totalPagos = 0, queryType = 0;
        private String query = "", queryTotals = "";
        private DateTime lastLoading;
        private int firstVisibleRow;
        private ScrollBars gridScrollBars;
        private List<CuentasXCobrarModel> pagosList = null;
        private List<CuentasXCobrarModel> pagosListTemp = null;
        private bool serverModeLAN = false;

        public FormPagosCxc(int idCxc)
        {
            this.idCxc = idCxc;
            pagosList = new List<CuentasXCobrarModel>();
            serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
            InitializeComponent();
            btnClose.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.back_white, 45, 45);
        }

        private void hideScrollBars()
        {
            imgSinDatos.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.logosynctpvmoving, 300, 300);
            imgSinDatos.Visible = true;
            gridScrollBars = dataGridViewPagos.ScrollBars;
            //dataGridItems.ScrollBars = ScrollBars.None;
        }

        private void FormPagosCxc_Load(object sender, EventArgs e)
        {
            fillDataGridViewPagos();
        }

        private async Task fillDataGridViewPagos()
        {
            hideScrollBars();
            lastLoading = DateTime.Now;
            pagosListTemp = await getAllPagos();
            if (pagosListTemp != null)
            {
                progress += pagosListTemp.Count;
                pagosList.AddRange(pagosListTemp);
                if (pagosList.Count > 0 && dataGridViewPagos.ColumnHeadersVisible == false)
                    dataGridViewPagos.ColumnHeadersVisible = true;
                for (int i = 0; i < pagosListTemp.Count; i++)
                {
                    int n = dataGridViewPagos.Rows.Add();
                    dataGridViewPagos.Rows[n].Cells[0].Value = pagosListTemp[i].id + "";
                    dataGridViewPagos.Columns[0].Visible = false;
                    String fcName = "";
                    if (serverModeLAN)
                    {
                        if (FormasDeCobroModel.getTotalOfFc() == 0)
                            await FormasDeCobroController.downloadAllFormasDeCobroLAN();
                        fcName = FormasDeCobroModel.getANameFrromAFomaDeCobroWithId(pagosListTemp[i].formOfPaymentIdCredit);
                    }
                    else
                    {
                        if (FormasDeCobroModel.getTotalOfFc() == 0)
                            await FormasDeCobroController.downloadAllFormasDeCobroAPI(0);
                        fcName = FormasDeCobroModel.getANameFrromAFomaDeCobroWithId(pagosListTemp[i].formOfPaymentIdCredit);
                    }
                    dataGridViewPagos.Rows[n].Cells[1].Value = fcName;
                    dataGridViewPagos.Rows[n].Cells[2].Value = pagosListTemp[i].amount.ToString("C", CultureInfo.CurrentCulture)+" MXN";
                    dataGridViewPagos.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridViewPagos.Rows[n].Cells[3].Value = pagosListTemp[i].reference;
                    dataGridViewPagos.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridViewPagos.Rows[n].Cells[4].Value = pagosListTemp[i].date;
                    dataGridViewPagos.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    if (pagosListTemp[i].enviado != 0 && pagosListTemp[i].status == 0)
                        dataGridViewPagos.Rows[n].Cells[5].Value = "Enviado";
                    else if (pagosListTemp[i].enviado != 0 && pagosListTemp[i].status == 0)
                        dataGridViewPagos.Rows[n].Cells[5].Value = "Local";
                    else dataGridViewPagos.Rows[n].Cells[5].Value = "Cancelado";
                    dataGridViewPagos.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                dataGridViewPagos.PerformLayout();
                pagosListTemp.Clear();
                if (pagosList.Count > 0)
                    lastId = Convert.ToInt32(pagosList[pagosList.Count - 1].id);
                imgSinDatos.Visible = false;
            }
            else
            {
                if (progress == 0)
                    imgSinDatos.Visible = true;
            }
            textTotalPagos.Text = "Pagos: " + totalPagos.ToString().Trim();
            //reset displayed row
            if (firstVisibleRow > -1)
            {
                showScrollBars();
                if (pagosList.Count > 0)
                {
                    dataGridViewPagos.FirstDisplayedScrollingRowIndex = firstVisibleRow;
                    imgSinDatos.Visible = false;
                }
            }
        }

        private async Task<List<CuentasXCobrarModel>> getAllPagos()
        {
            List<CuentasXCobrarModel> pagosList = null;
            await Task.Run(async () =>
            {
                if (queryType == 0)
                {
                    query = "SELECT * FROM "+LocalDatabase.TABLA_CXC+" WHERE "+
                    LocalDatabase.CAMPO_TIPO_CXC+" = 2 AND "+LocalDatabase.CAMPO_DOCTO_CC_ID_CXC+" = "+idCxc+" AND "+
                    LocalDatabase.CAMPO_ID_CXC+" > "+lastId+" ORDER BY "+ LocalDatabase.CAMPO_ID_CXC+" ASC LIMIT "+LIMIT;
                    queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_CXC + " WHERE " +
                    LocalDatabase.CAMPO_TIPO_CXC + " = 2 AND " + LocalDatabase.CAMPO_DOCTO_CC_ID_CXC + " = " + idCxc;
                    pagosList = CuentasXCobrarModel.getAllCxc(query);
                    totalPagos = CuentasXCobrarModel.getIntValue(queryTotals);
                }
            });
            return pagosList;
        }

        private void dataGridViewPagos_Scroll(object sender, ScrollEventArgs e)
        {
            if (pagosList.Count < totalPagos && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
                {
                    int cuntDisplayed = dataGridViewPagos.Rows.Count - getDisplayedRowsCount();
                    if (e.NewValue >= cuntDisplayed)
                    {
                        //prevent loading from autoscroll.
                        TimeSpan ts = DateTime.Now - lastLoading;
                        if (ts.TotalMilliseconds > 100)
                        {
                            firstVisibleRow = e.NewValue;
                            //dataGridItems.PerformLayout();
                            fillDataGridViewPagos();
                        }
                        else
                        {
                            dataGridViewPagos.FirstDisplayedScrollingRowIndex = e.OldValue;
                        }
                    }
                }
            }
        }

        private int getDisplayedRowsCount()
        {
            int count = dataGridViewPagos.Rows[dataGridViewPagos.FirstDisplayedScrollingRowIndex].Height;
            count = dataGridViewPagos.Height / count;
            return count;
        }

        private void dataGridViewPagos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnClose_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void showScrollBars()
        {
            dataGridViewPagos.ScrollBars = gridScrollBars;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
