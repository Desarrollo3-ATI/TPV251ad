using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;
using wsROMClases.Models;

namespace SyncTPV.Views.Extras
{
    public partial class FormSeleccionarCaja : Form
    {
        private int LIMIT = 30;
        private int progress = 0;
        private int lastId = 0;
        private int totalCajas = 0, queryType = 0;
        private String query = "", queryTotals = "";
        private DateTime lastLoading;
        private int firstVisibleRow;
        private ScrollBars gridScrollBars;
        private List<ClsCajaModel> cajasList;
        private List<ClsCajaModel> cajasListTemp;
        private String code = "";
        private FormWaiting formWaiting;

        public FormSeleccionarCaja()
        {
            cajasList = new List<ClsCajaModel>();
            InitializeComponent();
            btnClose.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.back_white, 40, 40);
            imgSinDatos.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.sindatos, 208, 156);
        }

        private async void FormSeleccionarCaja_Load(object sender, EventArgs e)
        {
            await loadInitialData();
            await fillDataGridViewCajas();
        }

        private async Task loadInitialData()
        {
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                dynamic responseConfig = ConfiguracionModel.getCodigoCajaPadre();
                if (responseConfig.value == 1)
                {
                    value = 1;
                    code = responseConfig.code;
                } else
                {
                    value = responseConfig.value;
                    description = responseConfig.description;
                }
            });
            if (value == 1)
            {
                editCodigoCajaPadre.Text = "Código Caja Padre: "+ code;
            } else
            {
                editCodigoCajaPadre.Text = "Caja Padre No Seleccionada!";
                FormMessage formMessage = new FormMessage("Caja Padre", description, 3);
                formMessage.ShowDialog();
            }
        }

        private void hideScrollBars()
        {
            //imgSinDatos.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.logosynctpvmoving, 300, 300);
            //imgSinDatos.Visible = true;
            gridScrollBars = dataGridViewCajas.ScrollBars;
            //dataGridItems.ScrollBars = ScrollBars.None;
        }

        private async Task fillDataGridViewCajas()
        {
            hideScrollBars();
            lastLoading = DateTime.Now;
            cajasListTemp = await getAllCajas();
            if (cajasListTemp != null)
            {
                progress += cajasListTemp.Count;
                cajasList.AddRange(cajasListTemp);
                if (cajasList.Count > 0 && dataGridViewCajas.ColumnHeadersVisible == false)
                    dataGridViewCajas.ColumnHeadersVisible = true;
                for (int i = 0; i < cajasListTemp.Count; i++)
                {
                    int n = dataGridViewCajas.Rows.Add();
                    dataGridViewCajas.Rows[n].Cells[0].Value = cajasListTemp[i].id;
                    dataGridViewCajas.Columns["idDgv"].Visible = false;
                    dataGridViewCajas.Rows[n].Cells[1].Value = cajasListTemp[i].code;
                    dataGridViewCajas.Rows[n].Cells[2].Value = cajasListTemp[i].name;
                    dataGridViewCajas.Rows[n].Cells[3].Value = cajasListTemp[i].warehouseId;
                    dataGridViewCajas.Rows[n].Cells[4].Value = cajasListTemp[i].createdAt;
                }
                dataGridViewCajas.PerformLayout();
                cajasListTemp.Clear();
                if (cajasList.Count > 0)
                    lastId = Convert.ToInt32(cajasList[cajasList.Count - 1].id);
                imgSinDatos.Visible = false;
            }
            else
            {
                if (progress == 0)
                    imgSinDatos.Visible = true;
            }
            editTotalCajas.Text = "Cajas: " + totalCajas.ToString().Trim();
            //reset displayed row
            if (firstVisibleRow > -1)
            {
                showScrollBars();
                if (cajasList.Count > 0)
                {
                    dataGridViewCajas.FirstDisplayedScrollingRowIndex = firstVisibleRow;
                    imgSinDatos.Visible = false;
                }
            }
        }

        private async Task<List<ClsCajaModel>> getAllCajas()
        {
            List<ClsCajaModel> cajasList = null;
            await Task.Run(async () =>
            {
                if (queryType == 0)
                {
                    query = "SELECT * FROM Caja " +
                    "WHERE id > "+lastId+" ORDER BY id ASC LIMIT "+LIMIT;
                    queryTotals = "SELECT COUNT(*) FROM Caja";
                    cajasList = CajaModel.getAllCajas(query);
                    totalCajas = CajaModel.getIntValue(queryTotals);
                }
            });
            return cajasList;
        }

        private void showScrollBars()
        {
            dataGridViewCajas.ScrollBars = gridScrollBars;
        }

        private void dataGridViewCajas_Scroll(object sender, ScrollEventArgs e)
        {
            if (cajasList.Count < totalCajas && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
                {
                    int cuntDisplayed = dataGridViewCajas.Rows.Count - getDisplayedRowsCount();
                    if (e.NewValue >= cuntDisplayed)
                    {
                        //prevent loading from autoscroll.
                        TimeSpan ts = DateTime.Now - lastLoading;
                        if (ts.TotalMilliseconds > 100)
                        {
                            firstVisibleRow = e.NewValue;
                            //dataGridItems.PerformLayout();
                            fillDataGridViewCajas();
                        }
                        else
                        {
                            dataGridViewCajas.FirstDisplayedScrollingRowIndex = e.OldValue;
                        }
                    }
                }
            }
        }

        private int getDisplayedRowsCount()
        {
            int count = dataGridViewCajas.Rows[dataGridViewCajas.FirstDisplayedScrollingRowIndex].Height;
            count = dataGridViewCajas.Height / count;
            return count;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewCajas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                code = cajasList[e.RowIndex].code;
                formWaiting = new FormWaiting(this, 0, "Actualizando Caja Padre...");
                formWaiting.ShowDialog();
            }
        }

        public async Task addOrUpdateCajaPadreProcess()
        {
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                dynamic responseConfig = ConfiguracionModel.updateCodigoCajaPadre(code);
                if (responseConfig.value == 1)
                {
                    value = 1;
                } else
                {
                    value = responseConfig.value;
                    description = responseConfig.description;
                }
            });
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            if (value == 1)
            {
                editCodigoCajaPadre.Text = "Código Caja Padre: " + code;
                PopupNotifier popup = new PopupNotifier();
                popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                popup.TitleColor = Color.FromArgb(43, 143, 192);
                popup.TitleText = "Caja Padre";
                popup.TitlePadding = new Padding(5, 5, 5, 5);
                popup.ButtonBorderColor = Color.Red;
                popup.ContentText = "El código para la caja padre fue actualizada!";
                popup.ContentColor = Color.FromArgb(43, 143, 192);
                popup.HeaderHeight = 10;
                popup.AnimationDuration = 1000;
                popup.HeaderColor = Color.FromArgb(200, 244, 255);
                popup.Popup();
            } else
            {
                if (code.Equals(""))
                    editCodigoCajaPadre.Text = "Caja Padre No Seleccionada!";
                FormMessage formMessage = new FormMessage("Actualizando Caja Padre", description, 3);
                formMessage.ShowDialog();
            }
        }

    }
}
