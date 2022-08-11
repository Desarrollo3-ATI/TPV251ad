using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using SyncTPV.Views;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;
using wsROMClase;
using wsROMClases.Models;
using static SyncTPV.Models.ConceptoSinRutaModel;
using System.Timers;
using System;
using System.Globalization;
using wsROMClases.Controllers;
using System.Dynamic;
using static SyncTPV.Models.BusquedasModel;

namespace SyncTPV
{
    public partial class FormArticulos : Form
    {
        public int call = 0;
        private int LIMIT = 17;
        private int progress = 0;
        private int lastId = 0;
        private int totalItems = 0, queryType = 0;
        private String query = "", queryTotals = "", itemCodeOrName = "";
        private DateTime lastLoading;
        private int firstVisibleRow;
        private ScrollBars gridScrollBars;
        private List<ClsItemModel> itemsList;
        private List<ClsItemModel> itemsListTemp;
        private FormWaiting formWaiting;
        private bool permissionPrepedido = false, webAcive = false;
        private bool serverModeLAN = false, conceptoXRutaCaja = false;
        private String comInstance = "", panelInstance = "";
        private String codigoCaja = "";
        private int idCaja = 0;
        private int matchPosition = 0, consideredFields = 0;
        private double descMaximo = 0;
        private int almacenId = 0;
        private bool cotmosActive = false;

        public FormArticulos(String codeOrName, int call, bool cotmosActive)
        {
            this.call = call;
            itemsList = new List<ClsItemModel>();
            InitializeComponent();
            showFormWaitingToGetAllInitalData();
            dataGridItems.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(GeneralTxt.RedSelectionRows, GeneralTxt.GreenSelectionRows, GeneralTxt.BlueSelectionRows);
            editBuscarArticulo.Text = codeOrName;
            dataGridItems.Scroll += new ScrollEventHandler(dataGridItems_Scroll);
            dataGridItems.CellDoubleClick += new DataGridViewCellEventHandler(dataGridItems_CellDoubleClick);
            timerBuscarItems.Stop();
            this.cotmosActive = cotmosActive;
        }

        private async void frmArticulos_LoadAsync(object sender, EventArgs e)
        {
            showFormWaitingToValidateUnitsMeasureandWeights();
            await getInstanceAndOhterInformations();
            await validatePermissionPrePedido();
            itemCodeOrName = editBuscarArticulo.Text.Trim();
            if (!itemCodeOrName.Equals(""))
            {
                queryType = 1;
                await fillDataGRidViewItems();
            }
            else
            {
                queryType = 0;
                await fillDataGRidViewItems();
            }
            editBuscarArticulo.Select();
            fillComboBoxMatchPositions();
            fillComboBoxConsideredFieds();
        }

        private void showFormWaitingToValidateUnitsMeasureandWeights()
        {
            formWaiting = new FormWaiting(this, 2, "Descargando unidades de medida y pesos");
            formWaiting.ShowDialog();
        }

        public async Task validateDownloadUnitsOfMeasureAndWeightProcess()
        {
            int totalUnits = 0;
            await Task.Run(async () =>
            {
                totalUnits = UnitsOfMeasureAndWeightModel.getTotalUnits();
            });
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {

                if (totalUnits == 0)
                {
                    if (serverModeLAN)
                    {
                        dynamic responseUnit = await UnitsOfMeasureAndWeightController.downloadAllUnitsOfMeasureAndWeightLAN();
                        if (responseUnit.value == 1)
                        {
                            value = 1;
                            description = "Unidades de medida descargadas correctamente!";
                        }
                        else
                        {
                            description = responseUnit.description;
                        }
                    }
                    else
                    {
                        if (webAcive)
                        {
                            dynamic responseUnit = await UnitsOfMeasureAndWeightController.downloadAllUnitsOfMeasureAndWeightAPI();
                            if (responseUnit.value == 1)
                            {
                                value = 1;
                                description = "Unidades de medida descargadas correctamente!";
                            }
                            else
                            {
                                description = responseUnit.description;
                            }
                        } else value = 2;
                    }
                    if (serverModeLAN)
                    {
                        dynamic responseConversion = await ConversionsUnitsController.downloadAllConversionsUnitsLAN();
                        if (responseConversion.value == 1)
                        {
                            value = 1;
                            description = "Unidades de medida y conversiones descargadas correctamente!";
                        }
                        else description = responseConversion.description;
                    }
                    else
                    {
                        if (webAcive)
                        {
                            dynamic responseConversion = await ConversionsUnitsController.downloadAllConversionsUnitsAPI();
                            if (responseConversion.value == 1)
                            {
                                value = 1;
                                description = "Unidades de medida y conversiones descargadas correctamente!";
                            }
                            else description = responseConversion.description;
                        }
                        else
                        {
                            value = 2;
                        }
                    }
                }
                else
                {
                    value = 2;
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
                popup.TitleText = "Unidades de medida y peso";
                popup.TitlePadding = new Padding(5, 5, 5, 5);
                popup.ButtonBorderColor = Color.Red;
                popup.ContentText = description;
                popup.ContentColor = Color.FromArgb(43, 143, 192);
                popup.HeaderHeight = 10;
                popup.AnimationDuration = 1000;
                popup.HeaderColor = Color.FromArgb(200, 244, 255);
                popup.Popup();
            } else if (value == 2)
            {

            }
            else
            {
                FormMessage formMessage = new FormMessage("Unidades de medida y peso", description, 3);
                formMessage.ShowDialog();
            }
        }

        private void showFormWaitingToGetAllInitalData()
        {
            formWaiting = new FormWaiting(this, 1, "Cargando datos iniciales...");
            formWaiting.ShowDialog();
        }

        public async Task getAllInitialDataProcess()
        {
            await Task.Run(async () =>
            {
                serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
                if (!serverModeLAN)
                {
                    dynamic responseWeb = ConfiguracionModel.webActive();
                    if (responseWeb.value == 1)
                        webAcive = responseWeb.active;
                }
                
                permissionPrepedido = UserModel.doYouHavePermissionPrepedido();
                FormVenta.seleccionoUnItem = false;
            });
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
        }

        private async Task fillComboBoxMatchPositions()
        {
            var dataSource = new List<MatchsPositionsBusquedas>();
            dataSource.Add(new MatchsPositionsBusquedas() { value = 0, name = "Cualquier lado" });
            dataSource.Add(new MatchsPositionsBusquedas() { value = 1, name = "Al principio" });
            dataSource.Add(new MatchsPositionsBusquedas() { value = 2, name = "Al final" });
            comboBoxMatchPosition.DataSource = dataSource;
            comboBoxMatchPosition.ValueMember = "value";
            comboBoxMatchPosition.DisplayMember = "name";
            comboBoxMatchPosition.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxMatchPosition.SelectedIndexChanged += new EventHandler(this.comboBoxMatchPosition_SelectedIndexChanged);
            dynamic responseExist = BusquedasModel.busquedaModelExist(BusquedasModel.MODEL_ITEMS);
            if (responseExist.value > 0)
            {
                BusquedasModel bm = BusquedasModel.getBusqueda(BusquedasModel.MODEL_ITEMS);
                if (bm != null)
                {
                    matchPosition = bm.matchPosition;
                    consideredFields = bm.consideredFields;
                    comboBoxMatchPosition.SelectedIndex = bm.matchPosition;
                }
            }
        }

        private async Task fillComboBoxConsideredFieds()
        {
            var dataSource = new List<MatchsPositionsBusquedas>();
            dataSource.Add(new MatchsPositionsBusquedas() { value = 0, name = "Código y Nombre" });
            dataSource.Add(new MatchsPositionsBusquedas() { value = 1, name = "Código" });
            dataSource.Add(new MatchsPositionsBusquedas() { value = 2, name = "Nombre" });
            comboBoxCamposBusqueda.DataSource = dataSource;
            comboBoxCamposBusqueda.ValueMember = "value";
            comboBoxCamposBusqueda.DisplayMember = "name";
            comboBoxCamposBusqueda.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxCamposBusqueda.SelectedIndexChanged += new EventHandler(this.comboBoxCamposBusqueda_SelectedIndexChanged);
            dynamic responseExist = BusquedasModel.busquedaModelExist(BusquedasModel.MODEL_ITEMS);
            if (responseExist.value > 0)
            {
                BusquedasModel bm = BusquedasModel.getBusqueda(BusquedasModel.MODEL_ITEMS);
                if (bm != null)
                {
                    consideredFields = bm.consideredFields;
                    matchPosition = bm.matchPosition;
                    comboBoxCamposBusqueda.SelectedIndex = bm.consideredFields;
                }
            }
        }

        private async Task getInstanceAndOhterInformations()
        {
            int value = 0;
            String description = "";
            String versionText = "";
            await Task.Run(async() =>
            {
                if (serverModeLAN)
                {
                    comInstance = InstanceSQLSEModel.getStringComInstance();
                    panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    conceptoXRutaCaja = ClsConfiguracionModel.isTheConceptByRouteActive(panelInstance);
                    versionText = "Versión LAN " + MetodosGenerales.versionNumber;
                }
                else
                {
                    if (webAcive)
                        versionText = "Versión Web (Online) " + MetodosGenerales.versionNumber;
                    else versionText = "Versión Web (Offline) " + MetodosGenerales.versionNumber;
                }
                if (cotmosActive)
                {
                    dynamic responseConfig = ConfiguracionModel.getCodigoCajaPadre();
                    if (responseConfig.value == 1)
                    {
                        value = 1;
                        codigoCaja = responseConfig.code;
                        descMaximo = UserModel.getDescuentoMaximoByCheckoutCode(codigoCaja);
                        almacenId = CajaModel.getAlmacenIdByCheckoutCode(codigoCaja);
                        if (serverModeLAN)
                            idCaja = ClsCajaModel.getIdByCodeBox(panelInstance, codigoCaja);
                    }
                    else
                    {
                        value = responseConfig.value;
                        description = responseConfig.description;
                    }
                }
                else
                {
                    value = 1;
                    codigoCaja = UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn());
                    descMaximo = UserModel.getDescuentoMaximo(ClsRegeditController.getIdUserInTurn());
                    almacenId = UserModel.getAlmacenIdFromTheUser(ClsRegeditController.getIdUserInTurn());
                    if (serverModeLAN)
                        idCaja = ClsCajaModel.getIdByCodeBox(panelInstance, codigoCaja);
                }
            });
            textVersion.Text = versionText;
            if (value == 1)
            {

            }
            else
            {
                FormMessage formMessage = new FormMessage("Configuración", description, 3);
                formMessage.ShowDialog();
            }
        }

        private async Task validatePermissionPrePedido()
        {
            if (permissionPrepedido)
            {
                dataGridItems.RowTemplate.DefaultCellStyle.Padding = new Padding(15, 15, 15, 15);
                dataGridItems.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }
        }

        private void PictureCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void hideScrollBars()
        {
            //imgSinDatos.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.logosynctpvmoving, 300, 300);
            //imgSinDatos.Visible = true;
            gridScrollBars = dataGridItems.ScrollBars;
            //dataGridItems.ScrollBars = ScrollBars.None;
        }

        private async Task fillDataGRidViewItems()
        {
            //await processBeforeAndAfterToLoadItems(true);
            hideScrollBars();
            lastLoading = DateTime.Now;
            itemsListTemp = await getAllItems();
            if (itemsListTemp != null)
            {
                progress += itemsListTemp.Count;
                itemsList.AddRange(itemsListTemp);
                if (itemsList.Count > 0 && dataGridItems.ColumnHeadersVisible == false)
                    dataGridItems.ColumnHeadersVisible = true;
                for (int i = 0; i < itemsListTemp.Count; i++)
                {
                    int n = dataGridItems.Rows.Add();
                    dataGridItems.Rows[n].Cells[0].Value = itemsListTemp[i].id + "";
                    dataGridItems.Columns["id"].Visible = false;
                    dataGridItems.Rows[n].Cells[1].Value = itemsListTemp[i].codigo;
                    dataGridItems.Columns[1].Width = 100;
                    dataGridItems.Rows[n].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dataGridItems.Rows[n].Cells[2].Value = itemsListTemp[i].nombre;
                    dataGridItems.Columns[2].Width = 400;
                    dataGridItems.Rows[n].Cells[3].Value = MetodosGenerales.obtieneDosDecimales(itemsListTemp[i].existencia);
                    dataGridItems.Columns[3].Width = 90;
                    dataGridItems.Rows[n].Cells[4].Value = itemsListTemp[i].precio1.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                    dataGridItems.Columns[4].Width = 100;
                    dataGridItems.Rows[n].Cells[5].Value = "Ver Detalles";
                    dataGridItems.Columns[5].Width = 110;
                }
                dataGridItems.PerformLayout();
                itemsListTemp.Clear();
                if (itemsList.Count > 0)
                    lastId = Convert.ToInt32(itemsList[itemsList.Count - 1].id);
                imgSinDatos.Visible = false;
            }
            else
            {
                if (progress == 0)
                    imgSinDatos.Visible = true;
            }
            textTotalItems.Text = "Productos o Servicios: " + totalItems.ToString().Trim();
            //reset displayed row
            if (firstVisibleRow > -1)
            {
                showScrollBars();
                if (itemsList.Count > 0)
                {
                    dataGridItems.FirstDisplayedScrollingRowIndex = firstVisibleRow;
                    imgSinDatos.Visible = false;
                }
            }
        }

        private async Task processBeforeAndAfterToLoadItems(bool visible)
        {
            progressBarLoadItems.Visible = visible;
            editBuscarArticulo.ReadOnly = visible;
            if (!visible)
                editBuscarArticulo.Focus();
        }

        private void showScrollBars()
        {
            dataGridItems.ScrollBars = gridScrollBars;
        }

        private void dataGridItems_Scroll(object sender, ScrollEventArgs e)
        {
            if (itemsList.Count < totalItems && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
                {
                    int cuntDisplayed = dataGridItems.Rows.Count - getDisplayedRowsCount();
                    if (e.NewValue >= cuntDisplayed)
                    {
                        //prevent loading from autoscroll.
                        TimeSpan ts = DateTime.Now - lastLoading;
                        if (ts.TotalMilliseconds > 100)
                        {
                            firstVisibleRow = e.NewValue;
                            //dataGridItems.PerformLayout();
                            fillDataGRidViewItems();
                        }
                        else
                        {
                            dataGridItems.FirstDisplayedScrollingRowIndex = e.OldValue;
                        }
                    }
                }
            }
        }

        private int getDisplayedRowsCount()
        {
            int count = dataGridItems.Rows[dataGridItems.FirstDisplayedScrollingRowIndex].Height;
            count = dataGridItems.Height / count;
            return count;
        }

        private void dataGridItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int idItem = Convert.ToInt32(dataGridItems.CurrentRow.Cells["id"].Value.ToString());
                if (call == 0)
                {
                    FormItemDetails articuloInfo = new FormItemDetails(idItem, cotmosActive);
                    articuloInfo.ShowDialog();
                }
                else
                {
                    if (serverModeLAN)
                    {
                        if (conceptoXRutaCaja)
                        {
                            ClsItemModel itemModel = itemsList[dataGridItems.CurrentRow.Index];
                            FormVenta.seleccionoUnItem = true;
                            FormVenta.itemModel = itemModel;
                        } else
                        {
                            ClsItemModel itemModel = itemsList[dataGridItems.CurrentRow.Index];
                            FormVenta.seleccionoUnItem = true;
                            FormVenta.itemModel = itemModel;
                        }
                    } else
                    {
                        ClsItemModel itemModel = itemsList[dataGridItems.CurrentRow.Index];
                        FormVenta.seleccionoUnItem = true;
                        FormVenta.itemModel = itemModel;
                    }
                    this.Close();
                }
            }
        }

        private void dataGridItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int idItem = Convert.ToInt32(dataGridItems.CurrentRow.Cells["id"].Value.ToString());
                if (call == 0)
                {
                    FormItemDetails articuloInfo = new FormItemDetails(idItem, cotmosActive);
                    articuloInfo.ShowDialog();
                } else if (call == 1)
                {
                    if (serverModeLAN)
                    {
                        if (conceptoXRutaCaja)
                        {
                            ClsItemModel itemModel = itemsList[dataGridItems.CurrentRow.Index];
                            FormVenta.seleccionoUnItem = true;
                            FormVenta.itemModel = itemModel;
                        }
                        else
                        {
                            ClsItemModel itemModel = itemsList[dataGridItems.CurrentRow.Index];
                            FormVenta.seleccionoUnItem = true;
                            FormVenta.itemModel = itemModel;
                        }
                    } else
                    {
                        ClsItemModel itemModel = itemsList[dataGridItems.CurrentRow.Index];
                        FormVenta.seleccionoUnItem = true;
                        FormVenta.itemModel = itemModel;
                    }
                    this.Close();
                }
            }
        }

        private void dataGridItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                int idItem = Convert.ToInt32(dataGridItems.CurrentRow.Cells["id"].Value.ToString());
                if (call == 1)
                {
                    FormItemDetails articuloInfo = new FormItemDetails(idItem, cotmosActive);
                    articuloInfo.ShowDialog();
                }
                else
                {
                    FormItemDetails articuloInfo = new FormItemDetails(idItem, cotmosActive);
                    articuloInfo.ShowDialog();
                }
            }
        }

        private void editBuscarArticulo_TextChanged(object sender, EventArgs e)
        {
            timerBuscarItems.Stop();
            timerBuscarItems.Start();
        }

        private void editBuscarArticulo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            } 
            else if (e.KeyCode == Keys.Down)
            {
                dataGridItems.Select();
            }
        }

        private void btnDescargarNuevos_Click(object sender, EventArgs e)
        {
            formWaiting = new FormWaiting(this, 0, "Descargando nuevos artículos...");
            formWaiting.ShowDialog();
        }

        private void FrmArticulos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            itemCodeOrName = editBuscarArticulo.Text.Trim();
            if (!itemCodeOrName.Equals(""))
            {
                queryType = 1;
                resetearValores(queryType);
                fillDataGRidViewItems();
                timerBuscarItems.Stop();
            }
            else
            {
                queryType = 0;
                resetearValores(queryType);
                fillDataGRidViewItems();
                timerBuscarItems.Stop();
            }
        }

        private void FrmArticulos_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                LIMIT = 30;
                resetearValores(queryType);
                fillDataGRidViewItems();
            } else
            {
                LIMIT = 17;
                resetearValores(queryType);
                fillDataGRidViewItems();
            }
        }

        

        private async Task<ExpandoObject> createOrUpdateBusquedaItems(int matchPosition, int consideredFields)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    dynamic responseExist = BusquedasModel.busquedaModelExist(BusquedasModel.MODEL_ITEMS);
                    if (responseExist.value > 0)
                    {
                        dynamic responseUpdate = BusquedasModel.updateBusqueda(BusquedasModel.MODEL_ITEMS, matchPosition, consideredFields);
                        value = responseUpdate.value;
                        description = responseUpdate.description;
                    }
                    else if (responseExist.value == 0)
                    {
                        dynamic responseCreate = BusquedasModel.createBusqueda(BusquedasModel.MODEL_ITEMS, matchPosition, consideredFields);
                        value = responseCreate.value;
                        description = responseCreate.description;
                    }
                    else
                    {
                        value = responseExist.value;
                        description = responseExist.description;
                    }
                } catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    value = -1;
                    description = e.Message;
                } finally
                {
                    response.value = value;
                    response.description = description;
                }
            });
            return response;
        }

        private async void comboBoxMatchPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            MatchsPositionsBusquedas valueCombo = (MatchsPositionsBusquedas)comboBoxMatchPosition.SelectedItem;
            if (valueCombo != null)
            {
                int matchPosition = Convert.ToInt32(valueCombo.value);
                dynamic response = await createOrUpdateBusquedaItems(matchPosition, consideredFields);
                if (response.value > 0)
                {
                    comboBoxMatchPosition.SelectedIndex = matchPosition;
                    this.matchPosition = matchPosition;
                    if (comboBoxMatchPosition.Focused)
                    {
                        resetearValores(queryType);
                        fillDataGRidViewItems();
                    }
                }
                else
                {
                    FormMessage formMessage = new FormMessage("Exception", response.description, 3);
                    formMessage.ShowDialog();
                }
            }
        }

        private async void comboBoxCamposBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            MatchsPositionsBusquedas valueCombo = (MatchsPositionsBusquedas)comboBoxCamposBusqueda.SelectedItem;
            if (valueCombo != null)
            {
                int consideredFields = Convert.ToInt32(valueCombo.value);
                dynamic response = await createOrUpdateBusquedaItems(matchPosition, consideredFields);
                if (response.value > 0)
                {
                    comboBoxCamposBusqueda.SelectedIndex = consideredFields;
                    this.consideredFields = consideredFields;
                    if (comboBoxCamposBusqueda.Focused)
                    {
                        resetearValores(queryType);
                        fillDataGRidViewItems();
                    }
                }
                else
                {
                    FormMessage formMessage = new FormMessage("Exception", response.description, 3);
                    formMessage.ShowDialog();
                }
            }
        }

        private void dataGridItems_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void button2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private async Task<List<ClsItemModel>> getAllItems()
        {
            List<ClsItemModel> itemsList = null;
            String description = "";
            await Task.Run(async () =>
            {
                if (serverModeLAN)
                {
                    dynamic responseConceptos = ClsConceptosController.getConceptCodeForCalculatePrices(panelInstance, codigoCaja);
                    if (conceptoXRutaCaja)
                    {
                        if (queryType == 0)
                        {
                            if (responseConceptos.value == 1)
                            {
                                dynamic responseItems = ClsItemModel.getAllItemsTPV(comInstance, codigoCaja, lastId, responseConceptos.concepto,
                                    LIMIT, descMaximo, almacenId);
                                if (responseItems.value == 1)
                                {
                                    itemsList = responseItems.itemsList;
                                } else
                                {
                                    description = responseItems.description;
                                }
                                totalItems = ClsItemModel.getTotalItemsTPV(comInstance);
                            }
                        } else if (queryType == 1)
                        {
                            if (responseConceptos.value == 1)
                            {
                                dynamic responseItems = ClsItemModel.getAllItemsTPVWithParameters(comInstance, codigoCaja, lastId, responseConceptos.concepto,
                                    LIMIT, "parameterName", itemCodeOrName, matchPosition, consideredFields, descMaximo, almacenId);
                                if (responseItems.value == 1)
                                {
                                    itemsList = responseItems.itemsList;
                                } else
                                {
                                    description = responseItems.description;
                                }
                                totalItems = ClsItemModel.getTotalItemsTPVWithParameters(comInstance, "parameterName", itemCodeOrName, matchPosition,
                                    consideredFields);
                            }
                        }
                    } else
                    {
                        if (queryType == 0)
                        {
                            if (responseConceptos.value == 1)
                            {
                                dynamic responseItems = ClsItemModel.getAllItemsTPV(comInstance, codigoCaja, lastId, responseConceptos.concepto,
                                   LIMIT, descMaximo, almacenId);
                                if (responseItems.value == 1)
                                {
                                    itemsList = responseItems.itemsList;
                                } else
                                {
                                    description = responseItems.description;
                                }
                                totalItems = ClsItemModel.getTotalItemsTPV(comInstance);
                            }
                        } else if (queryType == 1)
                        {
                            if (responseConceptos.value == 1)
                            {
                                dynamic responseItems = ClsItemModel.getAllItemsTPVWithParameters(comInstance, codigoCaja, lastId, responseConceptos.concepto,
                                    LIMIT, "parameterName", itemCodeOrName, matchPosition, consideredFields, descMaximo, almacenId);
                                if (responseItems.value == 1)
                                {
                                    itemsList = responseItems.itemsList;
                                } else
                                {
                                    description = responseItems.description;
                                }
                                totalItems = ClsItemModel.getTotalItemsTPVWithParameters(comInstance, "parameterName", itemCodeOrName, matchPosition,
                                    consideredFields);
                            }
                        }
                    }
                } 
                else
                {
                    if (webAcive)
                    {
                        if (queryType == 0)
                        {
                            dynamic responseTotalItemsServer = await ItemsController.getTotalItemsAPI(0, "parameterValue", itemCodeOrName,
                                matchPosition, consideredFields);
                            if (responseTotalItemsServer.value == 1)
                            {
                                totalItems = responseTotalItemsServer.total;
                                dynamic responseItems = await ItemsController.getItemsFromTheServerAPI(1, lastId, 1, LIMIT,
                                    descMaximo, almacenId, codigoCaja);
                                if (responseItems.value == 1)
                                {
                                    itemsList = responseItems.itemsList;
                                }
                                else
                                {
                                    description = responseItems.description;
                                    query = "SELECT * FROM " + LocalDatabase.TABLA_ITEM + " WHERE " + LocalDatabase.CAMPO_ID_ITEM +
                        " > " + lastId + " ORDER BY " + LocalDatabase.CAMPO_ID_ITEM + " LIMIT " + LIMIT;
                                    itemsList = ItemModel.getAllItemsWithParams(query, "parameterValue", itemCodeOrName, 0, matchPosition);
                                }
                            }
                            else
                            {
                                query = "SELECT * FROM " + LocalDatabase.TABLA_ITEM + " WHERE " + LocalDatabase.CAMPO_ID_ITEM +
                        " > " + lastId + " ORDER BY " + LocalDatabase.CAMPO_ID_ITEM + " LIMIT " + LIMIT;
                                queryTotals = "SELECT COUNT(id) FROM " + LocalDatabase.TABLA_ITEM;
                                totalItems = ItemModel.getTotalNumberFromItems(queryTotals, "parameterValue", itemCodeOrName, 0, matchPosition);
                                itemsList = ItemModel.getAllItemsWithParams(query, "parameterValue", itemCodeOrName, 0, matchPosition);
                            }
                        }
                        else if (queryType == 1)
                        {
                            dynamic responseTotalItemsServer = await ItemsController.getTotalItemsAPI(1, "parameterValue", itemCodeOrName,
                                matchPosition, consideredFields);
                            if (responseTotalItemsServer.value == 1)
                            {
                                totalItems = responseTotalItemsServer.total;
                                dynamic responseItems = await ItemsController.getItemsFromTheServerWSWithParameters(1, lastId, 1, LIMIT, "parameterValue",
                                    itemCodeOrName, matchPosition, consideredFields, codigoCaja);
                                if (responseItems.value == 1)
                                {
                                    itemsList = responseItems.itemsList;
                                }
                                else
                                {
                                    description = responseItems.description;
                                    query = "SELECT * FROM " + LocalDatabase.TABLA_ITEM + " WHERE ";
                                    if (consideredFields == 0)
                                    {
                                        query += LocalDatabase.CAMPO_CODIGO_ITEM + " LIKE @parameterValue OR " +
                                        LocalDatabase.CAMPO_NOMBRE_ITEM + " LIKE @parameterValue";
                                    }
                                    else if (consideredFields == 1)
                                        query += LocalDatabase.CAMPO_CODIGO_ITEM + " LIKE @parameterValue";
                                    else if (consideredFields == 2)
                                        query += LocalDatabase.CAMPO_NOMBRE_ITEM + " LIKE @parameterValue";
                                    query += " AND " + LocalDatabase.CAMPO_ID_ITEM + " > " + lastId + " ORDER BY " + LocalDatabase.CAMPO_ID_ITEM + " LIMIT " + LIMIT;
                                    itemsList = ItemModel.getAllItemsWithParams(query, "parameterValue", itemCodeOrName, 1, matchPosition);
                                }
                            }
                            else
                            {
                                query = "SELECT * FROM " + LocalDatabase.TABLA_ITEM + " WHERE ";
                                if (consideredFields == 0)
                                {
                                    query += LocalDatabase.CAMPO_CODIGO_ITEM + " LIKE " +
                               "@parameterValue OR " + LocalDatabase.CAMPO_NOMBRE_ITEM + " LIKE @parameterValue ";
                                }
                                else if (consideredFields == 1)
                                    query += LocalDatabase.CAMPO_CODIGO_ITEM + " LIKE @parameterValue";
                                else if (consideredFields == 2)
                                    query += LocalDatabase.CAMPO_NOMBRE_ITEM + " LIKE @parameterValue";
                                query += " AND " + LocalDatabase.CAMPO_ID_ITEM +
                           " > " + lastId + " ORDER BY " + LocalDatabase.CAMPO_ID_ITEM + " LIMIT " + LIMIT;

                                queryTotals = "SELECT COUNT(id) FROM " + LocalDatabase.TABLA_ITEM + " WHERE " + LocalDatabase.CAMPO_CODIGO_ITEM + " LIKE " +
                                "@parameterValue OR " + LocalDatabase.CAMPO_NOMBRE_ITEM + " LIKE @parameterValue";
                                totalItems = ItemModel.getTotalNumberFromItems(queryTotals, "parameterValue", itemCodeOrName, 1, matchPosition);
                                itemsList = ItemModel.getAllItemsWithParams(query, "parameterValue", itemCodeOrName, 1, matchPosition);
                            }
                        }
                    } else
                    {
                        if (queryType == 0)
                        {
                            query = "SELECT * FROM " + LocalDatabase.TABLA_ITEM + " WHERE " + LocalDatabase.CAMPO_ID_ITEM +
                        " > " + lastId + " ORDER BY " + LocalDatabase.CAMPO_ID_ITEM + " LIMIT " + LIMIT;
                            queryTotals = "SELECT COUNT(id) FROM " + LocalDatabase.TABLA_ITEM;
                            totalItems = ItemModel.getTotalNumberFromItems(queryTotals, "parameterValue", itemCodeOrName, 0, matchPosition);
                            itemsList = ItemModel.getAllItemsWithParams(query, "parameterValue", itemCodeOrName, 0, matchPosition);
                        }
                        else if (queryType == 1)
                        {
                            query = "SELECT * FROM " + LocalDatabase.TABLA_ITEM + " WHERE ";
                            if (consideredFields == 0)
                            {
                                query += LocalDatabase.CAMPO_CODIGO_ITEM + " LIKE " +
                           "@parameterValue OR " + LocalDatabase.CAMPO_NOMBRE_ITEM + " LIKE @parameterValue ";
                            }
                            else if (consideredFields == 1)
                                query += LocalDatabase.CAMPO_CODIGO_ITEM + " LIKE @parameterValue";
                            else if (consideredFields == 2)
                                query += LocalDatabase.CAMPO_NOMBRE_ITEM + " LIKE @parameterValue";
                            query += " AND " + LocalDatabase.CAMPO_ID_ITEM +
                       " > " + lastId + " ORDER BY " + LocalDatabase.CAMPO_ID_ITEM + " LIMIT " + LIMIT;

                            queryTotals = "SELECT COUNT(id) FROM " + LocalDatabase.TABLA_ITEM + " WHERE " + LocalDatabase.CAMPO_CODIGO_ITEM + " LIKE " +
                            "@parameterValue OR " + LocalDatabase.CAMPO_NOMBRE_ITEM + " LIKE @parameterValue";
                            totalItems = ItemModel.getTotalNumberFromItems(queryTotals, "parameterValue", itemCodeOrName, 1, matchPosition);
                            itemsList = ItemModel.getAllItemsWithParams(query, "parameterValue", itemCodeOrName, 1, matchPosition);
                        }
                    }
                }
            });
            if (!description.Equals(""))
            {
                FormMessage formMessage = new FormMessage("Artículos", description, 3);
                formMessage.ShowDialog();
            }
            return itemsList;
        }

        private void editBuscarArticulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                try
                {
                    if (call == 0)
                    {
                        int idItem = 0;
                        if (dataGridItems.CurrentRow != null)
                            idItem = Convert.ToInt32(dataGridItems.CurrentRow.Cells["id"].Value.ToString().Trim());
                        FormItemDetails articuloInfo = new FormItemDetails(idItem, cotmosActive);
                        articuloInfo.ShowDialog();
                    }
                    else if (call == 1)
                    {
                        if (dataGridItems.CurrentRow != null)
                        {
                            ClsItemModel itemModel = itemsList[dataGridItems.CurrentRow.Index];
                            FormVenta.seleccionoUnItem = true;
                            FormVenta.itemModel = itemModel;
                            this.Close();
                        } else
                        {

                        }
                    }
                } catch (Exception ex)
                {
                    SECUDOC.writeLog(ex.ToString());
                    MessageBox.Show(ex.Message, "Exception");
                }
            }
        }

        private void resetearValores(int queryType) {
            this.queryType = queryType;
            query = "";
            queryTotals = "";
            totalItems = 0;
            lastId = 0;
            progress = 0;
            firstVisibleRow = 0;
            itemsList.Clear();
            dataGridItems.Rows.Clear();
        }

        public void proccesToGetNewItems()
        {
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            if (webAcive)
            {
                FormMessage formMessage = new FormMessage("Artículos", "En esta versión, todos los datos de los artículos " +
                "son actualizados en automático por lo que si agregaste o actualizaste un artículo en Comercial, solo " +
                "tienes que buscarlo y los datos ya deberían estar actualizados.", 1);
                formMessage.ShowDialog();
            } else
            {
                FormMessage formMessage = new FormMessage("Artículos", "la conexión vía Web está desactivada por lo que " +
                    "toda la información no se está actualizando!\r\nIr a Configuración y activar Conexión Web", 3);
                formMessage.ShowDialog();
            }
        }
    }
}
