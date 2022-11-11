using SyncTPV.Controllers;
using SyncTPV.Controllers.Downloads;
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
using System.Windows.Threading;
using Tulpep.NotificationWindow;

namespace SyncTPV.Views
{
    public partial class FrmDownloads : Form
    {
        FormWaiting frmWaiting;
        private bool serverModeLAN = false;
        private bool cotmosActive = false;
        private String codigoCaja = "";

        public FrmDownloads(bool cotmosActive)
        {
            serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
            InitializeComponent();
            this.cotmosActive = cotmosActive;
        }

        private void FrmDownloads_Load(object sender, EventArgs e)
        {
            validatePermissionPrepedido();
            fillDataGridViewDownloads();
        }

        private async Task loadInitialData()
        {
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                if (cotmosActive)
                {
                    dynamic responseConfig = ConfiguracionModel.getCodigoCajaPadre();
                    if (responseConfig.value == 1)
                    {
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

        private async Task validatePermissionPrepedido()
        {
            if (UserModel.doYouHavePermissionPrepedido())
            {
                dataGridViewDownloads.RowTemplate.DefaultCellStyle.Padding = new Padding(15, 15, 15, 15);
                dataGridViewDownloads.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            } else
            {

            }
        }

        private async Task fillDataGridViewDownloads()
        {
            List<ClsBanderasCargaInicialModel> downloadsList = await getAllDownloads();
            if (downloadsList != null)
            {
                ClsBanderasCargaInicialModel bci = new ClsBanderasCargaInicialModel();
                bci.id = 21;
                bci.name = "Usuarios, Distribuidor y otros datos";
                bci.registrosObtenidos = 0;
                downloadsList.Add(bci);
                foreach (ClsBanderasCargaInicialModel download in downloadsList)
                {
                    int n = dataGridViewDownloads.Rows.Add();
                    dataGridViewDownloads.Rows[n].Cells[0].Value = download.id;
                    dataGridViewDownloads.Columns["Id"].Visible = false;
                    if (download.name.Trim().Equals("UnitMeasureWeight"))
                        dataGridViewDownloads.Rows[n].Cells[1].Value = "Unidades de Medida y Peso";
                    else if (download.name.Trim().Equals("ConversionsUnit"))
                        dataGridViewDownloads.Rows[n].Cells[1].Value = "Conversiones de Unidades";
                    else dataGridViewDownloads.Rows[n].Cells[1].Value = download.name;
                    dataGridViewDownloads.Rows[n].Cells[2].Value = download.registrosObtenidos;
                    dataGridViewDownloads.Columns["registrosObtenidos"].Visible = false;
                    dataGridViewDownloads.Rows[n].Cells[3].Value = "Actualizar";
                    dataGridViewDownloads.Rows[n].Cells[3].Style.BackColor = Color.FromArgb(113, 173, 240);
                    dataGridViewDownloads.Columns["download"].Width = 100;
                }
                dataGridViewDownloads.PerformLayout();
            }
        }

        private async Task<List<ClsBanderasCargaInicialModel>> getAllDownloads()
        {
            List<ClsBanderasCargaInicialModel> downloadsList = null;
            await Task.Run(() => {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_BANDERASCI;
                downloadsList = ClsBanderasCargaInicialModel.getAllDownloadsMethods(query);
            });
            return downloadsList;
        }

        private void btnCloseWindow_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewDownloads_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 3)
            {
                processToDownloadData(e.RowIndex);
            }
        }

        private async Task processToDownloadData(int index)
        {
            dataGridViewDownloads.Rows[index].Cells[3].Value = "Descargando...";
            PopupNotifier popup = new PopupNotifier();
            popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
            popup.TitleColor = Color.Blue;
            popup.TitleText = "Descargando Datos";
            popup.ContentText = "Iniciando descarga de información";
            popup.ContentColor = Color.Red;
            popup.Popup();
            
            frmWaiting = new FormWaiting(0, index, this, codigoCaja);
            frmWaiting.StartPosition = FormStartPosition.CenterScreen;
            frmWaiting.ShowDialog();
        }

        public async Task updateUIAftherDownload(int value, bool showMessage, String description, int positionTable)
        {
            await Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => {
                if (frmWaiting != null)
                    frmWaiting.Close();
                if (dataGridViewDownloads != null)
                    dataGridViewDownloads.Rows[positionTable].Cells[3].Value = "Actualizar";
                if (value == 1)
                {
                    if (showMessage)
                    {
                        /*PopupNotifier popup = new PopupNotifier();
                        popup.Image = ClsMetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                        popup.TitleColor = Color.Blue;
                        popup.TitleText = "Datos Actualizados";
                        popup.ContentText = description;
                        popup.ContentColor = Color.Red;
                        popup.Popup();*/
                    }
                } else if (value == 2)
                {
                    if (showMessage)
                    {
                        FormMessage formMessage = new FormMessage("Exception", description, value);
                        formMessage.ShowDialog();
                    }
                }
            }), DispatcherPriority.Background, null);
        }
    }
}
