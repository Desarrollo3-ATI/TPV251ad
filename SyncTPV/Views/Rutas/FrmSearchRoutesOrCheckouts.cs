using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using SyncTPV.Models.Server.Panel;
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

namespace SyncTPV.Views.Rutas
{
    public partial class FrmSearchRoutesOrCheckouts : Form
    {
        public static readonly int CALL_SURTIRPEDCOT = 0;
        private int call = 0;
        private int LIMIT = 100;
        private int progress = 0;
        private int lastId = 0;
        private int totalItems = 0, queryType = 0;
        private String query = "", queryTotals = "", routeCodeOrName = "";
        private DateTime lastLoading;
        private int firstVisibleRow;
        private ScrollBars gridScrollBars;
        private List<RutaModel> routesList;
        private List<RutaModel> routesListTemp;
        FormWaiting frmWaiting;
        private int idRoute = 0;

        public FrmSearchRoutesOrCheckouts(int call)
        {
            InitializeComponent();
            routesList = new List<RutaModel>();
            this.call = call;
        }

        private void FrmSearchRoutesOrCheckouts_Load(object sender, EventArgs e) {
            fillDataGridViewRoutes();
        }

        private void hideScrollBars()
        {
            //imgSinDatos.Image = ClsMetodosGenerales.redimencionarBitmap(Properties.Resources.logosynctpvmoving, 300, 300);
            //imgSinDatos.Visible = true;
            gridScrollBars = dataGridViewRoutesOrCheckouts.ScrollBars;
            //dataGridItems.ScrollBars = ScrollBars.None;
        }

        private async Task fillDataGridViewRoutes()
        {
            hideScrollBars();
            lastLoading = DateTime.Now;
            routesListTemp = await getAllRoutes();
            if (routesListTemp != null)
            {
                progress += routesListTemp.Count;
                routesList.AddRange(routesListTemp);
                for (int i = 0; i < routesListTemp.Count; i++)
                {
                    int n = dataGridViewRoutesOrCheckouts.Rows.Add();
                    dataGridViewRoutesOrCheckouts.Rows[n].Cells[0].Value = routesListTemp[i].id + "";
                    dataGridViewRoutesOrCheckouts.Columns["id"].Visible = false;
                    dataGridViewRoutesOrCheckouts.Rows[n].Cells[1].Value = routesListTemp[i].code;
                    dataGridViewRoutesOrCheckouts.Rows[n].Cells[2].Value = routesListTemp[i].name;
                }
                dataGridViewRoutesOrCheckouts.PerformLayout();
                routesListTemp.Clear();
                lastId = Convert.ToInt32(routesList[routesList.Count - 1].id);
                //imgSinDatos.Visible = false;
                dataGridViewRoutesOrCheckouts.Select();
            }
            else
            {
                //if (progress == 0)
                    //imgSinDatos.Visible = true;
            }
            textTotalRoutesOrCheckouts.Text = "Rutas: " + totalItems.ToString().Trim();
            //reset displayed row
            if (firstVisibleRow > -1)
            {
                showScrollBars();
                if (routesList.Count > 0)
                    dataGridViewRoutesOrCheckouts.FirstDisplayedScrollingRowIndex = firstVisibleRow;
                //imgSinDatos.Visible = false;
            }
        }

        private void showScrollBars()
        {
            dataGridViewRoutesOrCheckouts.ScrollBars = gridScrollBars;
        }

        private async Task<List<RutaModel>> getAllRoutes()
        {
            List<RutaModel> routesList = null;
            await Task.Run(() => { 
                if (queryType == 0) {
                    if (LIMIT < 100)
                        LIMIT = 100;
                    query = "SELECT * FROM " + LocalDatabase.TABLA_RUTA + " WHERE " + LocalDatabase.CAMPO_ID_RUTA +" > " + lastId + 
                    " ORDER BY " + LocalDatabase.CAMPO_ID_RUTA + " LIMIT " + LIMIT;
                    queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_RUTA;
                } else if (queryType == 1) {
                    if (LIMIT == 100)
                        LIMIT = 50;
                    query = "SELECT * FROM " + LocalDatabase.TABLA_RUTA + " WHERE " +LocalDatabase.CAMPO_CODE_RUTA+" LIKE '%"+ routeCodeOrName + "%' " +
                    "OR "+LocalDatabase.CAMPO_NAME_RUTA+" LIKE '%"+ routeCodeOrName + "%' AND "+LocalDatabase.CAMPO_ID_RUTA + " > " + lastId +
                    " ORDER BY " + LocalDatabase.CAMPO_ID_RUTA + " LIMIT " + LIMIT;
                    queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_RUTA+ " WHERE " + LocalDatabase.CAMPO_CODE_RUTA + " LIKE '%" + routeCodeOrName + "%' " +
                    "OR " + LocalDatabase.CAMPO_NAME_RUTA + " LIKE '%" + routeCodeOrName + "%'";
                }
                totalItems = RutaModel.getIntValue(queryTotals);
                routesList = RutaModel.getAllRoutes(query);
            });
            return routesList;
        }

        private void editSearchRouteOrBox_TextChanged(object sender, EventArgs e)
        {
            routeCodeOrName = editSearchRouteOrBox.Text.Trim();
            if (!routeCodeOrName.Equals(""))
            {
                queryType = 1;
                resetearValores(queryType);
                fillDataGridViewRoutes();
            }
            else
            {
                queryType = 0;
                resetearValores(queryType);
                fillDataGridViewRoutes();
            }
        }

        private void dataGridViewRoutesOrCheckouts_Scroll(object sender, ScrollEventArgs e)
        {
            if (routesList.Count < totalItems)
            {
                if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
                {
                    if (e.NewValue > dataGridViewRoutesOrCheckouts.Rows.Count - getDisplayedRowsCount())
                    {
                        //prevent loading from autoscroll.
                        TimeSpan ts = DateTime.Now - lastLoading;
                        if (ts.TotalMilliseconds > 100)
                        {
                            firstVisibleRow = e.NewValue;
                            //dataGridItems.PerformLayout();
                            fillDataGridViewRoutes();
                        }
                        else
                        {
                            dataGridViewRoutesOrCheckouts.FirstDisplayedScrollingRowIndex = e.OldValue;
                        }
                    }
                }
            }
        }

        private int getDisplayedRowsCount()
        {
            int count = dataGridViewRoutesOrCheckouts.Rows[dataGridViewRoutesOrCheckouts.FirstDisplayedScrollingRowIndex].Height;
            count = dataGridViewRoutesOrCheckouts.Height / count;
            return count;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewRoutesOrCheckouts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (call == CALL_SURTIRPEDCOT)
                {
                    idRoute = Convert.ToInt32(dataGridViewRoutesOrCheckouts.Rows[e.RowIndex].Cells["id"].Value);
                    String codeRoute = dataGridViewRoutesOrCheckouts.Rows[e.RowIndex].Cells["code"].Value.ToString().Trim();
                    if (idRoute != 0)
                    {
                        FrmPedidosCotizacionesSurtir.routeCajaIdPrePedido = idRoute;
                        FrmPedidosCotizacionesSurtir.routeCajaCodePrePedido = codeRoute;
                    }
                    this.Close();
                }
            }
        }

        private void btnUpdateRoutesOrCheckouts_Click(object sender, EventArgs e)
        {
            frmWaiting = new FormWaiting(0, this);
            frmWaiting.StartPosition = FormStartPosition.CenterScreen;
            frmWaiting.ShowDialog();
        }

        public async Task doDownloadRoutesProcess() {
            String query = "SELECT " + LocalDatabase.CAMPO_SERVERMODE_CONFIG + " FROM " + LocalDatabase.TABLA_CONFIGURACION + " WHERE " +
                LocalDatabase.CAMPO_ID_CONFIGURACION + " = 1";
            int serverMode = ConfiguracionModel.getIntValue(query);
            if (serverMode == 1) {
                String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                dynamic response = await RutaController.getAllRoutesWithSQL(panelInstance, 0, "");
                if (response != null) {
                    if (response.valor == 1)
                    {
                        if (routeCodeOrName.Equals(""))
                        {
                            queryType = 0;
                            resetearValores(queryType);
                            fillDataGridViewRoutes();
                        }
                        else
                        {
                            queryType = 1;
                            resetearValores(queryType);
                            fillDataGridViewRoutes();
                        }
                        PopupNotifier popup = new PopupNotifier();
                        popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                        popup.TitleColor = Color.Blue;
                        popup.TitleText = "Datos Actualizados";
                        popup.ContentText = response.description;
                        popup.ContentColor = Color.Red;
                        popup.Popup();
                    } else
                    {
                        if (routeCodeOrName.Equals(""))
                        {
                            queryType = 0;
                            resetearValores(queryType);
                            fillDataGridViewRoutes();
                        }
                        else
                        {
                            queryType = 1;
                            resetearValores(queryType);
                            fillDataGridViewRoutes();
                        }
                        /*PopupNotifier popup = new PopupNotifier();
                        popup.Image = ClsMetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                        popup.TitleColor = Color.Blue;
                        popup.TitleText = "Validar Información";
                        popup.ContentText = response.description;
                        popup.ContentColor = Color.Red;
                        popup.Popup();*/
                        FormMessage formMessage = new FormMessage("Validar Información", "" + response.description, 2);
                        formMessage.ShowDialog();
                    }
                }
                if (frmWaiting != null)
                    frmWaiting.Close();
            } else {
                dynamic response = await RutaController.getAllRoutesWithWs(0, "");
                if (response != null) {
                    if (response.valor == 1) {
                        if (routeCodeOrName.Equals(""))
                        {
                            queryType = 0;
                            resetearValores(queryType);
                            fillDataGridViewRoutes();
                        }
                        else
                        {
                            queryType = 1;
                            resetearValores(queryType);
                            fillDataGridViewRoutes();
                        }
                        PopupNotifier popup = new PopupNotifier();
                        popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                        popup.TitleColor = Color.Blue;
                        popup.TitleText = "Datos Actualizados";
                        popup.ContentText = response.description;
                        popup.ContentColor = Color.Red;
                        popup.Popup();
                    } else {
                        if (routeCodeOrName.Equals(""))
                        {
                            queryType = 0;
                            resetearValores(queryType);
                            fillDataGridViewRoutes();
                        }
                        else
                        {
                            queryType = 1;
                            resetearValores(queryType);
                            fillDataGridViewRoutes();
                        }
                        FormMessage formMessage = new FormMessage("Validar Información", "" + response.description, 2);
                        formMessage.ShowDialog();
                        /*PopupNotifier popup = new PopupNotifier();
                        popup.Image = ClsMetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                        popup.TitleColor = Color.Blue;
                        popup.TitleText = "Validar Información";
                        popup.ContentText = response.description;
                        popup.ContentColor = Color.Red;
                        popup.Popup();*/
                    }
                    if (frmWaiting != null)
                        frmWaiting.Close();
                }
            }
        }

        private void resetearValores(int queryType)
        {
            this.queryType = queryType;
            query = "";
            queryTotals = "";
            totalItems = 0;
            lastId = 0;
            progress = 0;
            routesList.Clear();
            dataGridViewRoutesOrCheckouts.Rows.Clear();
        }

    }
}
