using SyncTPV.Helpers.SqliteDatabaseHelper;
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

namespace SyncTPV.Views.Taras
{
    public partial class FrmTaras : Form
    {
        private int LIMIT = 70;
        private int progress = 0;
        private int lastId = 0;
        private int totalTaras = 0, queryType = 0;
        private String query = "", queryTotals = "", tarasCodeOrName = "";
        private DateTime lastLoading;
        private int firstVisibleRow;
        private ScrollBars gridScrollBars;
        private List<TaraModel> tarasList;
        private List<TaraModel> tarasListTemp;

        public FrmTaras()
        {
            InitializeComponent();
            tarasList = new List<TaraModel>();
            btnClose.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.close, 35, 35);
            btnAdd.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.add_white, 35, 35);
        }

        private void FrmTaras_Load(object sender, EventArgs e)
        {
            fillDataGridViewTaras();
        }

        private void hideScrollBars()
        {
            //imgSinDatos.Image = ClsMetodosGenerales.redimencionarBitmap(Properties.Resources.logosynctpvmoving, 300, 300);
            //imgSinDatos.Visible = true;
            gridScrollBars = dataGridViewTaras.ScrollBars;
            //dataGridItems.ScrollBars = ScrollBars.None;
        }

        private void dataGridViewTaras_Scroll(object sender, ScrollEventArgs e)
        {
            if (tarasList.Count < totalTaras)
            {
                if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
                {
                    if (e.NewValue > dataGridViewTaras.Rows.Count - getDisplayedRowsCount())
                    {
                        //prevent loading from autoscroll.
                        TimeSpan ts = DateTime.Now - lastLoading;
                        if (ts.TotalMilliseconds > 100)
                        {
                            firstVisibleRow = e.NewValue;
                            //dataGridItems.PerformLayout();
                            fillDataGridViewTaras();
                        }
                        else
                        {
                            dataGridViewTaras.FirstDisplayedScrollingRowIndex = e.OldValue;
                        }
                    }
                }
            }
        }

        private int getDisplayedRowsCount()
        {
            int count = dataGridViewTaras.Rows[dataGridViewTaras.FirstDisplayedScrollingRowIndex].Height;
            count = dataGridViewTaras.Height / count;
            return count;
        }

        private void dataGridViewTaras_SelectionChanged(object sender, EventArgs e)
        {
            /*dataGridViewTaras.CurrentCell = dataGridViewTaras.CurrentRow.Cells["pesoDgv"];
            dataGridViewTaras.BeginEdit(true);
            PopupNotifier popup = new PopupNotifier();
            popup.Image = ClsMetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
            popup.TitleColor = Color.Blue;
            popup.TitleText = "Selección Modificada";
            popup.ContentText = "Modificando: "+dataGridViewTaras.Rows.Count;
            popup.ContentColor = Color.Red;
            popup.Popup();*/
        }

        private void dataGridViewTaras_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridViewTaras.Rows[e.RowIndex].Cells["pesoDgv"].Selected)
                {
                    int idTara = Convert.ToInt32(dataGridViewTaras.Rows[e.RowIndex].Cells[0].Value.ToString().Trim());
                    if (idTara != 0)
                    {
                        String color = dataGridViewTaras.Rows[e.RowIndex].Cells[3].Value.ToString().Trim();
                        String pesoText = dataGridViewTaras.Rows[e.RowIndex].Cells[4].Value.ToString().Trim();
                        String tipo = dataGridViewTaras.Rows[e.RowIndex].Cells[5].Value.ToString().Trim();
                        double peso = 0;
                        bool result = Double.TryParse(pesoText, out peso);
                        if (result)
                        {
                            TaraModel.updateColorPesoTipo(idTara, color, peso, tipo);
                        }
                        else
                        {
                            FormMessage formMessage = new FormMessage("Exception", "El peso tiene que ser caracteres numericos", 2);
                            formMessage.ShowDialog();
                        }
                    }
                }
            }
        }

        private void dataGridViewTaras_CancelRowEdit(object sender, QuestionEventArgs e)
        {
            PopupNotifier popup = new PopupNotifier();
            popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
            popup.TitleColor = Color.Blue;
            popup.TitleText = "Edición Cancelada";
            popup.ContentText = "Cancelando: " + e.Response;
            popup.ContentColor = Color.Red;
            popup.Popup();
        }

        private void dataGridViewTaras_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormAddNewTara formAddNewTara = new FormAddNewTara(this);
            formAddNewTara.StartPosition = FormStartPosition.CenterScreen;
            formAddNewTara.ShowDialog();
        }

        public async Task fillDataGridViewTaras()
        {
            hideScrollBars();
            lastLoading = DateTime.Now;
            tarasListTemp = await getAllTaras();
            if (tarasListTemp != null)
            {
                progress += tarasListTemp.Count;
                tarasList.AddRange(tarasListTemp);
                if (tarasList.Count > 0 && dataGridViewTaras.ColumnHeadersVisible == false)
                    dataGridViewTaras.ColumnHeadersVisible = true;
                for (int i = 0; i < tarasListTemp.Count; i++)
                {
                    int n = dataGridViewTaras.Rows.Add();
                    dataGridViewTaras.Rows[n].Height = 80;
                    dataGridViewTaras.Rows[n].Cells[0].Value = tarasListTemp[i].id + "";
                    dataGridViewTaras.Columns["idDgv"].Visible = false;
                    dataGridViewTaras.Rows[n].Cells[1].Value = tarasListTemp[i].code;
                    dataGridViewTaras.Rows[n].Cells[1].ReadOnly = true;
                    dataGridViewTaras.Rows[n].Cells[2].Value = tarasListTemp[i].name;
                    dataGridViewTaras.Rows[n].Cells[2].ReadOnly = true;
                    dataGridViewTaras.Rows[n].Cells[3].Value = tarasListTemp[i].color;
                    dataGridViewTaras.Rows[n].Cells[3].ReadOnly = false;
                    dataGridViewTaras.Rows[n].Cells[4].Value = tarasListTemp[i].peso;
                    dataGridViewTaras.Rows[n].Cells[4].ReadOnly = false;
                    dataGridViewTaras.Rows[n].Cells[5].Value = tarasListTemp[i].tipo;
                    dataGridViewTaras.Rows[n].Cells[5].ReadOnly = false;
                    dataGridViewTaras.Rows[n].Cells[6].Value = tarasListTemp[i].createdAt;
                    dataGridViewTaras.Rows[n].Cells[6].ReadOnly = true;
                }
                //dataGridViewTaras.PerformLayout();
                tarasListTemp.Clear();
                lastId = Convert.ToInt32(tarasList[tarasList.Count - 1].id);
                //imgSinDatos.Visible = false;
                //dataGridItems.Focus();
            }
            else
            {
                //if (progress == 0)
                    //imgSinDatos.Visible = true;
            }
            textTotalRecords.Text = "Cajas: " + totalTaras.ToString().Trim();
            //reset displayed row
            if (firstVisibleRow > -1)
            {
                dataGridViewTaras.ScrollBars = gridScrollBars;
                if (tarasList.Count > 0)
                {
                    dataGridViewTaras.FirstDisplayedScrollingRowIndex = firstVisibleRow;
                    //imgSinDatos.Visible = false;
                }
            }
        }

        private async Task<List<TaraModel>> getAllTaras()
        {
            List<TaraModel> tarasList = null;
            await Task.Run(() => {
                query = "SELECT * FROM " + LocalDatabase.TABLA_TARA;
                queryTotals = "SELECT * FROM " + LocalDatabase.TABLA_TARA;
                tarasList = TaraModel.getTaras(query);
                totalTaras = TaraModel.getIntValue(query);
            });
            return tarasList;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void resetearVariables(int queryType)
        {
            this.queryType = queryType;
            dataGridViewTaras.Rows.Clear();
            tarasList.Clear();
        }
    }
}
