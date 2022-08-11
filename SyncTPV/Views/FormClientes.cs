using SyncTPV.Controllers;
using SyncTPV.Controllers.Downloads;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using SyncTPV.Views;
using SyncTPV.Views.Customers;
using SyncTPV.Views.Extras;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using Tulpep.NotificationWindow;
using wsROMClases.Models;
using wsROMClases.Models.Commercial;

namespace SyncTPV
{
    public partial class FormClientes : Form
    {
        int idCustomer = 0;
        private int LIMIT = 30;
        private int progress = 0;
        private int lastId = 0;
        private int totalCustomers = 0, queryType = 0;
        private String query = "", queryTotals = "", customerCodeOrName = "";
        private DateTime lastLoading;
        private int firstVisibleRow;
        private ScrollBars gridScrollBars;
        private List<ClsClienteModel> customersList;
        private List<ClsClienteModel> customersListTemp;
        private bool permissionPrepedido = false, serverModeLAN = false, webActive = false;
        public static ProgressBar progressCargaCliente;
        private FormWaiting frmWaiting;
        private String panelInstance = "", comInstance = "", codigoCaja = "";

        public FormClientes()
        {
            InitializeComponent();
            serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
            permissionPrepedido = UserModel.doYouHavePermissionPrepedido();
            btnAddCustomer.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.add_customer_white, 35, 35);
            btnCerrar.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.back_white, 35, 35);
            btnUpdate.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.download_local_white, 35, 35);
            dataGridClientes.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(GeneralTxt.RedSelectionRows, GeneralTxt.GreenSelectionRows, GeneralTxt.BlueSelectionRows);
            dataGridClientes.CellClick += new DataGridViewCellEventHandler(dataGridClientes_CellClick);
            dataGridClientes.Scroll += new ScrollEventHandler(dataGridClientes_Scroll);
            customersList = new List<ClsClienteModel>();
            btnCobranzaDetalleCliente.Click += new EventHandler(btnCobranzaDetalleCliente_Click);
            progressCargaCliente = progressBarGetCustomer;
        }

        private async void frmClientes_Load(object sender, EventArgs e)
        {
            await validateIfServerModeLANIsActive();
            editPendienteDetalleCliente.ReadOnly = true;
            if (UserModel.doYouHaveACollectionPermit() == 0)
            {
                btnCobranzaDetalleCliente.Visible = false;
            }
            if (UserModel.doYouHaveAAddNewCustomersPermission())
                btnAddCustomer.Visible = true;
            else btnAddCustomer.Visible = false;
            fillCustomersDataGrid();
            btnDelete.Visible = false;
            validatePermissionPrepedido();
        }

        private async Task validateIfServerModeLANIsActive()
        {
            if (serverModeLAN)
            {
                textVersionLAN.Text = "Versión LAN";
                await Task.Run(async () =>
                {
                    panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    comInstance = InstanceSQLSEModel.getStringComInstance();
                    codigoCaja = ClsCajaModel.getBoxCodeByAgentId(panelInstance, ClsRegeditController.getIdUserInTurn());
                });
            } else
            {
                int value = 0;
                String description = "";
                await Task.Run(async () =>
                {
                    dynamic responseWeb = ConfiguracionModel.webActive();
                    if (responseWeb.value == 1)
                    {
                        value = 1;
                        webActive = responseWeb.active;
                        if (webActive)
                            description = "Versión Web (Online)";
                        else description = "Versión Web (Offline)";
                    } else
                    {
                        if (webActive)
                            description = "Versión Web (Online)";
                        else description = "Versión Web (Offline)";
                    }
                });
                textVersionLAN.Text = description;
            }
        }

        private async Task validatePermissionPrepedido()
        {
            if (permissionPrepedido)
            {
                dataGridClientes.RowTemplate.DefaultCellStyle.Padding = new Padding(15, 15, 15, 15);
                dataGridClientes.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                btnAddCustomer.Visible = false;
                btnCobranzaDetalleCliente.Visible = false;
            } else
            {
                dataGridClientes.RowTemplate.DefaultCellStyle.Padding = new Padding(5,2,5,2);
                dataGridClientes.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }
        }

        private void hideScrollBars()
        {
            imgSinDatos.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.logosynctpvmoving, 300, 300);
            imgSinDatos.Visible = true;
            gridScrollBars = dataGridClientes.ScrollBars;
            //dataGridItems.ScrollBars = ScrollBars.None;
        }

        private async Task fillCustomersDataGrid()
        {
            hideScrollBars();
            lastLoading = DateTime.Now;
            customersListTemp = await getAllCustomers();
            if (customersListTemp != null)
            {
                progress += customersListTemp.Count;
                customersList.AddRange(customersListTemp);
                for (int i = 0; i < customersListTemp.Count; i++)
                {
                    int n = dataGridClientes.Rows.Add();
                    dataGridClientes.Rows[n].Cells[0].Value = customersListTemp[i].CLIENTE_ID + "";
                    dataGridClientes.Columns["id"].Visible = false;
                    dataGridClientes.Rows[n].Cells[1].Value = customersListTemp[i].NOMBRE;
                    dataGridClientes.Columns[1].Width = 200;
                    dataGridClientes.Rows[n].Cells[2].Value = customersListTemp[i].rfc;
                    dataGridClientes.Columns[2].Width = 150;
                    int precioId = customersListTemp[i].PRECIO_EMPRESA_ID;
                    if (precioId != 0)
                    {
                        String priceName = PreciosempresaModel.getAPriceName(precioId);
                        if (priceName.Equals(""))
                        {
                            if (serverModeLAN)
                                await PreciosEmpresaController.downloadAllPreciosEmpresaLAN();
                            else await PreciosEmpresaController.downloadAllPreciosEmpresaAPI();
                            priceName = PreciosempresaModel.getAPriceName(precioId);
                        }
                        dataGridClientes.Rows[n].Cells[3].Value = priceName;
                    }
                    else dataGridClientes.Rows[n].Cells[3].Value = "Sin precio asignado";
                    dataGridClientes.Columns[3].Width = 150;
                    dataGridClientes.Rows[n].Cells[4].Value = customersListTemp[i].denominacionComercial;
                    dataGridClientes.Columns[4].Width = 250;
                    dataGridClientes.Rows[n].Cells[5].Value = customersListTemp[i].codigoUsoCFDI;
                    dataGridClientes.Columns[5].Width = 250;
                    dataGridClientes.Rows[n].Cells[6].Value = customersListTemp[i].codigoRegimenFiscal;
                    dataGridClientes.Columns[6].Width = 250;
                }
                dataGridClientes.PerformLayout();
                customersListTemp.Clear();
                if (customersList.Count > 0)
                    lastId = Convert.ToInt32(customersList[customersList.Count - 1].CLIENTE_ID);
                imgSinDatos.Visible = false;
            }
            else
            {
                if (progress == 0)
                    imgSinDatos.Visible = true;
            }
            textTotalCustomers.Text = "Clientes: " + totalCustomers.ToString().Trim();
            if (firstVisibleRow > -1)
            {
                showScrollBars();
                if (customersList.Count > 0)
                {
                    dataGridClientes.FirstDisplayedScrollingRowIndex = firstVisibleRow;
                    imgSinDatos.Visible = false;
                }
            }
        }

        private void showScrollBars()
        {
            dataGridClientes.ScrollBars = gridScrollBars;
        }

        private async Task<List<ClsClienteModel>> getAllCustomers()
        {
            List<ClsClienteModel> customersList = null;
            await Task.Run(async () =>
            {
                if (serverModeLAN)
                {
                    if (queryType == 0)
                    {
                        totalCustomers = ClsCustomersModel.getTotalOfCustomersTPV(comInstance, panelInstance, codigoCaja);
                        dynamic responseCustomer = ClsCustomersModel.getAllCustomersFromAnUser(comInstance, panelInstance, codigoCaja, true,
                            lastId, LIMIT);
                        if (responseCustomer.value == 1)
                        {
                            customersList = responseCustomer.customersList;
                        } else
                        {

                        }
                    } else
                    {
                        totalCustomers = ClsCustomersModel.getTotalOfCustomersTPVWithPrameters(comInstance, panelInstance, codigoCaja,
                            "parameterName", customerCodeOrName);
                        customersList = ClsCustomersModel.getAllCustomersTPVWithParameters(comInstance,
                            lastId, LIMIT, "parameterName", customerCodeOrName);
                    }
                } else
                {
                    if (queryType == 0)
                    {
                        dynamic responseCustomerTotal = await CustomersController.getTotalOfCustomersAPI(lastId, LIMIT, 0, "", "");
                        if (responseCustomerTotal.value == 1)
                        {
                            totalCustomers = responseCustomerTotal.total;
                            dynamic responseCustomer = await CustomersController.getAllCustomersAPI(lastId, LIMIT, "", "");
                            if (responseCustomer.value == 1)
                            {
                                customersList = responseCustomer.customersList;
                            }
                            else
                            {
                                query = "SELECT * FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " > " + lastId +
                                " ORDER BY " +LocalDatabase.CAMPO_ID_CLIENTE + " LIMIT " + LIMIT;
                                queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_CLIENTES;
                                totalCustomers = CustomerModel.getTotalRecordsWithParameters(queryTotals, "", "", 0);
                                customersList = CustomerModel.getAllCustomersWithParameters(query, "", "", 0);
                            }
                        }
                        else
                        {
                            query = "SELECT * FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " > " + lastId + 
                            " ORDER BY " +LocalDatabase.CAMPO_ID_CLIENTE + " LIMIT " + LIMIT;
                            queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_CLIENTES;
                            totalCustomers = CustomerModel.getTotalRecordsWithParameters(queryTotals, "", "", 0);
                            customersList = CustomerModel.getAllCustomersWithParameters(query, "", "", 0);
                        }
                    }
                    else
                    {
                        dynamic responseCustomerTotal = await CustomersController.getTotalOfCustomersAPI(lastId, LIMIT, 1, "parameterName", customerCodeOrName);
                        if (responseCustomerTotal.value == 1)
                        {
                            totalCustomers = responseCustomerTotal.total;
                            dynamic responseCustomer = await CustomersController.getAllCustomersAPI(lastId, LIMIT, "parameterName", customerCodeOrName);
                            if (responseCustomer.value == 1)
                            {
                                customersList = responseCustomer.customersList;
                            }else
                            {
                                query = "SELECT * FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " +
                    LocalDatabase.CAMPO_NOMBRECLIENTE + " LIKE @parameterName OR " + LocalDatabase.CAMPO_CLAVECLIENTE + " LIKE @parameterName" +
                    " AND " + LocalDatabase.CAMPO_ID_CLIENTE + " > " + lastId + " ORDER BY " +
                        LocalDatabase.CAMPO_ID_CLIENTE + " LIMIT " + LIMIT;
                                queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " +
                            LocalDatabase.CAMPO_NOMBRECLIENTE + " LIKE @parameterName OR " + LocalDatabase.CAMPO_CLAVECLIENTE + " LIKE @parameterName";
                                totalCustomers = CustomerModel.getTotalRecordsWithParameters(queryTotals, "parameterName", customerCodeOrName, 1);
                                customersList = CustomerModel.getAllCustomersWithParameters(query, "parameterName", customerCodeOrName, 1);
                            }
                        } else
                        {
                            query = "SELECT * FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " +
                    LocalDatabase.CAMPO_NOMBRECLIENTE + " LIKE @parameterName OR " + LocalDatabase.CAMPO_CLAVECLIENTE + " LIKE @parameterName" +
                    " AND " + LocalDatabase.CAMPO_ID_CLIENTE + " > " + lastId + " ORDER BY " +
                        LocalDatabase.CAMPO_ID_CLIENTE + " LIMIT " + LIMIT;
                            queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " +
                        LocalDatabase.CAMPO_NOMBRECLIENTE + " LIKE @parameterName OR " + LocalDatabase.CAMPO_CLAVECLIENTE + " LIKE @parameterName";
                            totalCustomers = CustomerModel.getTotalRecordsWithParameters(queryTotals, "parameterName", customerCodeOrName, 1);
                            customersList = CustomerModel.getAllCustomersWithParameters(query, "parameterName", customerCodeOrName, 1);
                        }
                    }
                }
            });
            return customersList;
        }


        private void dataGridClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                idCustomer = customersList[e.RowIndex].CLIENTE_ID;
                logicToSeeMoreInformationAboutCustomer(e.RowIndex);
            }
        }

        private async Task logicToSeeMoreInformationAboutCustomer(int dgvRowPosition)
        {
            if (idCustomer < 0)
            {
                btnDelete.Visible = true;
                btnDelete.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.delete_white, 40, 40);
            }
            else
            {
                btnDelete.Visible = false;
            }
            if (!serverModeLAN)
            {
                progressCargaCliente.Visible = true;
                progressCargaCliente.Value = 20;
                dynamic response = null;
                if (idCustomer < 0)
                {
                    int idClientePanel = CustomerADCModel.getNewClientIdPanel(idCustomer);
                    if (idClientePanel != 0)
                    {
                        response = await CustomersController.getACustomerAPI(Convert.ToInt32("-" + idClientePanel));
                        if (response.value == 1)
                        {
                            ClsClienteModel customerModel = response.customerModel;
                            idCustomer = customerModel.CLIENTE_ID;
                            fillAllDataCustomer(idCustomer, customerModel);
                            downloadImageCustomer();
                            updateItemDgvClientes(dgvRowPosition, idCustomer);
                        }
                    }
                }
                else
                {
                    response = await CustomersController.getACustomerAPI(idCustomer);
                    if (response.value == 1)
                    {
                        ClsClienteModel customerModel = response.customerModel;
                        idCustomer = customerModel.CLIENTE_ID;
                        fillAllDataCustomer(idCustomer, customerModel);
                        downloadImageCustomer();
                        updateItemDgvClientes(dgvRowPosition, idCustomer);
                    }
                }
                progressCargaCliente.Visible = false;
            } else
            {
                progressCargaCliente.Visible = true;
                progressCargaCliente.Value = 50;
                dynamic response = null;
                if (idCustomer < 0)
                {
                    int idClientePanel = CustomerADCModel.getNewClientIdPanel(idCustomer);
                    if (idClientePanel != 0)
                    {
                        response = await CustomersController.getACustomerLAN(Convert.ToInt32("-" + idClientePanel));
                        if (response.value == 1)
                        {
                            ClsClienteModel customerModel = response.customerModel;
                            idCustomer = customerModel.CLIENTE_ID;
                            fillAllDataCustomer(idCustomer, customerModel);
                            LlenarImagen(idCustomer);
                            updateItemDgvClientes(dgvRowPosition, idCustomer);
                        }
                    }
                }
                else
                {
                    response = await CustomersController.getACustomerLAN(idCustomer);
                    if (response.value >= 1)
                    {
                        ClsClienteModel customerModel = response.customerModel;
                        fillAllDataCustomer(idCustomer, customerModel);
                        downloadImageCustomer();
                        updateItemDgvClientes(dgvRowPosition, idCustomer);
                    }
                }
                progressCargaCliente.Visible = false;
            }
        }

        private void updateItemDgvClientes(int dgvRowPosition, int idCliente)
        {
            if (customersList != null && customersList.Count > 0)
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " = " + idCliente;
                List<ClsClienteModel> cm = CustomerModel.getAllCustomers(query);
                if (cm != null && cm.Count > 0)
                {
                    customersList[dgvRowPosition] = cm[0];
                    dataGridClientes.Rows[dgvRowPosition].SetValues(customersList[dgvRowPosition]);
                }
            }
        }

        /*public static async Task updateUIGetCustomer(int value)
        {
            await Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => {
                if (progressCargaCliente != null)
                {
                    progressCargaCliente.Visible = false;
                    progressCargaCliente.Value = value;
                }
            }), DispatcherPriority.Background, null);
            //Thread.Sleep(200);
        }*/

        public void fillAllDataCustomer(int idCustomer, ClsClienteModel cm)
        {
            ClsClienteModel cliente = CustomerModel.getAllDataFromACustomer(idCustomer);
            if (cliente != null)
            {
                textClaveDetalleCliente.Text = cliente.CLAVE;
                textNombreClienteDetallescliente.Text = cliente.NOMBRE;
                textDenomComercialDetalleCliente.Text = cliente.denominacionComercial;
                txtDireccion.Text = cliente.DIRECCION;
                txtDireccion.ReadOnly = true;
                txtLimCredito.Text = "" + cliente.LIMITE_CREDITO.ToString("C2", CultureInfo.CurrentCulture)+" MXN";
                txtLimCredito.ReadOnly = true;
                txtListaPrecio.Text = PreciosempresaModel.getAPriceName(cliente.LISTAPRECIO);
                txtListaPrecio.ReadOnly = true;
                textZonaDetalleCliente.Text = ZonasDeClientesModel.getNameFromAZone(Convert.ToInt32(cliente.ZONA_CLIENTE_ID));
                double pendiente = CuentasXCobrarModel.getCurrentCustomerBalanceWithDb(null, idCustomer);
                editPendienteDetalleCliente.Text = "$ "+MetodosGenerales.obtieneDosDecimales(pendiente) +" MXN";
                txtTelefono.Text = cliente.TELEFONO;
                txtTelefono.ReadOnly = true;
                //imgSinDatosDetalleCliente.Visible = false;
            }
            else
            {
                if (cm != null)
                {
                    textClaveDetalleCliente.Text = cm.CLAVE;
                    textNombreClienteDetallescliente.Text = cm.NOMBRE;
                    textDenomComercialDetalleCliente.Text = cm.denominacionComercial;
                    txtDireccion.Text = cm.DIRECCION;
                    txtDireccion.ReadOnly = true;
                    txtLimCredito.Text = "" + cm.LIMITE_CREDITO.ToString("C2", CultureInfo.CurrentCulture) + " MXN";
                    txtLimCredito.ReadOnly = true;
                    txtListaPrecio.Text = PreciosempresaModel.getAPriceName(cm.LISTAPRECIO);
                    txtListaPrecio.ReadOnly = true;
                    textZonaDetalleCliente.Text = ZonasDeClientesModel.getNameFromAZone(Convert.ToInt32(cm.ZONA_CLIENTE_ID));
                    double pendiente = CuentasXCobrarModel.getCurrentCustomerBalanceWithDb(null, idCustomer);
                    editPendienteDetalleCliente.Text = "$ " + MetodosGenerales.obtieneDosDecimales(pendiente) + " MXN";
                    txtTelefono.Text = cm.TELEFONO;
                    txtTelefono.ReadOnly = true;
                } else
                {
                    //imgSinDatosDetalleCliente.Visible = true;
                    textClaveDetalleCliente.Text = "";
                    textNombreClienteDetallescliente.Text = "Seleccionar Cliente";
                    textDenomComercialDetalleCliente.Text = "";
                    txtDireccion.Text = "";
                    txtDireccion.ReadOnly = true;
                    txtLimCredito.Text = "";
                    txtLimCredito.ReadOnly = true;
                    txtListaPrecio.Text = "";
                    txtListaPrecio.ReadOnly = true;
                    textZonaDetalleCliente.Text = "";
                    editPendienteDetalleCliente.Text = "$ 0 MXN";
                    txtTelefono.Text = "";
                    txtTelefono.ReadOnly = true;
                }
            }
        }

        private void LlenarImagen(int idCustomer)
        {

            string rutaImg = MetodosGenerales.rootDirectory + "\\Imagenes\\Customers\\" + idCustomer + "-c.jpg";
            if (File.Exists(rutaImg))
            {
                try
                {
                    FileStream fs = new FileStream(rutaImg, FileMode.Open, FileAccess.Read);
                    pctBoxCliente.Image = MetodosGenerales.redimencionarImagenes(Image.FromStream(fs), 191, 174);
                    fs.Close();
                }
                catch (Exception ex)
                {
                    SECUDOC.writeLog(ex.Message);
                    rutaImg = MetodosGenerales.rootDirectory + "\\Imagenes\\Estaticas\\SyncTPV.png";
                    FileStream fs = new FileStream(rutaImg, FileMode.Open, FileAccess.Read);
                    pctBoxCliente.Image = MetodosGenerales.redimencionarImagenes(Image.FromStream(fs), 191, 174);
                    fs.Close();
                }
            } else
            {
                try
                {
                    rutaImg = MetodosGenerales.rootDirectory + "\\Imagenes\\Estaticas\\SyncTPV.png";
                    FileStream fs = new FileStream(rutaImg, FileMode.Open, FileAccess.Read);
                    pctBoxCliente.Image = MetodosGenerales.redimencionarImagenes(Image.FromStream(fs), 191, 174);
                    fs.Close();
                }
                catch (Exception ex)
                {
                    SECUDOC.writeLog(ex.ToString());
                }
            }
        }

        private void picCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBuscarCliente_TextChanged(object sender, EventArgs e)
        {
            timerBusquedaClientes.Stop();
            timerBusquedaClientes.Start();
        }

        private void textNombreClienteDetallescliente_Click(object sender, EventArgs e)
        {

        }

        private void btnCobranzaDetalleCliente_Click(object sender, EventArgs e)
        {
            string createdAt = DateTime.Now.ToString("yyyyMMdd");
            if (UserModel.doYouHavePermissionPrepedido())
            {
                FrmCobranzaCxc fcxc = new FrmCobranzaCxc(idCustomer);
                fcxc.ShowDialog();
            } else
            {
                if (serverModeLAN)
                {
                    ClsAperturaCajaModel atm = ClsAperturaCajaModel.getARecordWithParameters(panelInstance, createdAt,
                                    ClsRegeditController.getIdUserInTurn());
                    if (atm != null)
                    {
                        PrinterModel pm = PrinterModel.getallDataFromAPrinter();
                        if (pm != null)
                        {
                            FrmCobranzaCxc fcxc = new FrmCobranzaCxc(idCustomer);
                            fcxc.ShowDialog();
                        }
                        else
                        {
                            FormMessage formMessage = new FormMessage("Impresora Faltante", "Asegurate de actualizar la información de la impresora en la " +
                                "Configuración", 3);
                            formMessage.ShowDialog();
                        }
                    }
                    else
                    {
                        FormMessage fm = new FormMessage("Apertura de Caja Faltante", "Antes de iniciar el proceso de venta tienes que realizar la apertura de caja!", 2);
                        fm.ShowDialog();
                    }
                }
                else
                {
                    String query = "SELECT * FROM " + LocalDatabase.TABLA_APERTURATURNO +
                                " WHERE " + LocalDatabase.CAMPO_CREATEDAT_APERTURATURNO + " = '" + createdAt + "' AND " + LocalDatabase.CAMPO_USERID_APERTURATURNO + " = " +
                                ClsRegeditController.getIdUserInTurn();
                    AperturaTurnoModel atm = AperturaTurnoModel.getARecord(query);
                    if (atm != null)
                    {
                        PrinterModel pm = PrinterModel.getallDataFromAPrinter();
                        if (pm != null)
                        {
                            FrmCobranzaCxc fcxc = new FrmCobranzaCxc(idCustomer);
                            fcxc.ShowDialog();
                        }
                        else
                        {
                            FormMessage formMessage = new FormMessage("Impresora Faltante", "Asegurate de actualizar la información de la impresora en la " +
                                "Configuración", 3);
                            formMessage.ShowDialog();
                        }
                    }
                    else
                    {
                        FormMessage fm = new FormMessage("Apertura de Caja Faltante", "Antes de iniciar el proceso de venta tienes que realizar la apertura de caja!", 2);
                        fm.ShowDialog();
                    }
                }
            }
        }

        private void imgSearchCostomers_Click(object sender, EventArgs e)
        {
            customerCodeOrName = textBuscarCliente.Text.ToString().Trim();
            if (!customerCodeOrName.Equals(""))
            {
                queryType = 1;
                resetearValores(queryType);
                fillCustomersDataGrid();
            }
            else
            {
                queryType = 0;
                resetearValores(queryType);
                fillCustomersDataGrid();
            }
        }

        private void PictureCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBuscarCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                customerCodeOrName = textBuscarCliente.Text.ToString().Trim();
                if (!customerCodeOrName.Equals(""))
                {
                    queryType = 1;
                    resetearValores(queryType);
                    fillCustomersDataGrid();
                }
                else
                {
                    queryType = 0;
                    resetearValores(queryType);
                    fillCustomersDataGrid();
                }
            }
        }

        private void textBuscarCliente_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            } else
            {
                
            }
        }

        private void frmClientes_SizeChanged(object sender, EventArgs e)
        {
            Size currentSize = panelContent.Size;
            panelDataGridClientes.Width = (currentSize.Width / 2) - 10;
            panelPerfilCliente.Width = (currentSize.Width / 2) - 10;
            panelPerfilCliente.Margin = new Padding(5);
            this.panelPerfilCliente.Location = new Point(
                 this.panelDataGridClientes.Location.X + (currentSize.Width / 2),
                 this.panelDataGridClientes.Location.Y
             );
        }

        private void btnUploadImg_Click(object sender, EventArgs e)
        {
            FormPasswordConfirmation formPasswordConfirmation = new FormPasswordConfirmation("Acceso Supervisor", "Ingresa la contraseña del supervisor");
            formPasswordConfirmation.StartPosition = FormStartPosition.CenterScreen;
            formPasswordConfirmation.ShowDialog();
            if (FormPasswordConfirmation.permissionGranted)
            {
                if (idCustomer != 0)
                    updateImageProcess();
                else
                {
                    FormMessage formMessage = new FormMessage("Cliente no seleccionado", "Tienes que elegir un cliente para poder subir una imágen", 3);
                    formMessage.ShowDialog();
                }
            }
        }

        private async Task updateImageProcess()
        {
            string nameFile = "";
            string rutaArchivoLocal = "";
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *jpeg; *.jpe; *.jfif; *.png";
            openFileDialog1.Title = "Escoga la imagen a subir";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                rutaArchivoLocal = openFileDialog1.FileName;
                String rutaImagenes = MetodosGenerales.rootDirectory + "\\Imagenes";
                if (!Directory.Exists(rutaImagenes))
                    Directory.CreateDirectory(rutaImagenes);
                String rutaCarpetaLocal = MetodosGenerales.rootDirectory + "\\Imagenes\\customers";
                if (!Directory.Exists(rutaCarpetaLocal))
                    Directory.CreateDirectory(rutaCarpetaLocal);
                String destFile = Path.Combine(rutaCarpetaLocal, idCustomer + "-c.jpg");
                try
                {
                    /*FileStream fsSal = new FileStream(rutaArchivoLocal, FileMode.Open, FileAccess.Read);
                    FileStream fsDest = new FileStream(destFile, FileMode.Create, FileAccess.Write);
                    fsSal.CopyTo(fsDest);
                    fsSal.Close();
                    fsDest.Close();*/
                    File.Copy(rutaArchivoLocal, destFile, true);
                    dynamic responseImage = null;
                    if (serverModeLAN)
                        responseImage = await SubirImagenesController.uploadImagenesViaLAN("customers", rutaCarpetaLocal, idCustomer);
                    else responseImage = await SubirImagenesController.uploadImageAPI("customers", rutaCarpetaLocal, idCustomer);
                    if (responseImage.value == 1)
                    {
                        string rutaImagenLocal = MetodosGenerales.rootDirectory + "\\Imagenes\\customers\\" + idCustomer + "-c.jpg";
                        try
                        {
                            FileStream fs = new FileStream(rutaImagenLocal, FileMode.Open, FileAccess.Read);
                            pctBoxCliente.Image = MetodosGenerales.redimencionarImagenes(Image.FromStream(fs), 162, 137);
                            fs.Close();
                        }
                        catch (Exception ex)
                        {
                            SECUDOC.writeLog(ex.ToString());
                            rutaImagenLocal = MetodosGenerales.rootDirectory + "\\Imagenes\\Estaticas\\SyncTPV.png";
                            FileStream fs = new FileStream(rutaImagenLocal, FileMode.Open, FileAccess.Read);
                            pctBoxCliente.Image = MetodosGenerales.redimencionarImagenes(Image.FromStream(fs), 191, 174);
                            fs.Close();
                        }
                        FormMessage msj = new FormMessage("Carga de imagen exitosa", "Imágen actualizada correctamente", 1);
                        msj.ShowDialog();
                    }
                    else
                    {
                        FormMessage msj = new FormMessage("Carga de imagen fallida", responseImage.description, 3);
                        msj.ShowDialog();
                    }
                } catch (IOException e)
                {
                    SECUDOC.writeLog(e.ToString());
                    FormMessage msj = new FormMessage("Carga de imagen fallida", e.Message, 3);
                    msj.ShowDialog();
                }
            }
        }

        private async Task downloadImageCustomer()
        {
            await Task.Run(async () =>
            {
                if (serverModeLAN)
                {
                    String rutaLocal = MetodosGenerales.rootDirectory+"\\Imagenes\\customers";
                    await SubirImagenesController.getImageLAN("customers", rutaLocal, idCustomer);
                }
                else await SubirImagenesController.getImage("-c", idCustomer);
            });
            LlenarImagen(idCustomer);
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            string createdAt = DateTime.Now.ToString("yyyyMMdd");
            if (serverModeLAN)
            {
                ClsAperturaCajaModel atm = ClsAperturaCajaModel.getARecordWithParameters(panelInstance, createdAt,
                                    ClsRegeditController.getIdUserInTurn());
                if (atm != null)
                {
                    PrinterModel pm = PrinterModel.getallDataFromAPrinter();
                    if (pm != null)
                    {
                        FormAddCustomer formAddCustomer = new FormAddCustomer();
                        formAddCustomer.StartPosition = FormStartPosition.CenterScreen;
                        formAddCustomer.ShowDialog();
                    }
                    else
                    {
                        FormMessage formMessage = new FormMessage("Impresora Faltante", "Asegurate de actualizar la información de la impresora en la " +
                            "Configuración", 3);
                        formMessage.ShowDialog();
                    }
                }
                else
                {
                    FormMessage fm = new FormMessage("Apertura de Caja Faltante", "Antes de iniciar el proceso de venta tienes que realizar la apertura de caja!", 2);
                    fm.ShowDialog();
                }
            } else
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_APERTURATURNO +
                                " WHERE " + LocalDatabase.CAMPO_CREATEDAT_APERTURATURNO + " = '" + createdAt + "' AND " + LocalDatabase.CAMPO_USERID_APERTURATURNO + " = " +
                                ClsRegeditController.getIdUserInTurn();
                AperturaTurnoModel atm = AperturaTurnoModel.getARecord(query);
                if (atm != null)
                {
                    PrinterModel pm = PrinterModel.getallDataFromAPrinter();
                    if (pm != null)
                    {
                        FormAddCustomer formAddCustomer = new FormAddCustomer();
                        formAddCustomer.StartPosition = FormStartPosition.CenterScreen;
                        formAddCustomer.ShowDialog();
                    }
                    else
                    {
                        FormMessage formMessage = new FormMessage("Impresora Faltante", "Asegurate de actualizar la información de la impresora en la " +
                            "Configuración", 3);
                        formMessage.ShowDialog();
                    }
                }
                else
                {
                    FormMessage fm = new FormMessage("Apertura de Caja Faltante", "Antes de iniciar el proceso de venta tienes que realizar la apertura de caja!", 2);
                    fm.ShowDialog();
                }
            }
        }

        private void btnCerrar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void dataGridClientes_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnCobranzaDetalleCliente_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void panelPerfilCliente_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timerBusquedaClientes_Tick(object sender, EventArgs e)
        {
            customerCodeOrName = textBuscarCliente.Text.ToString().Trim();
            if (!customerCodeOrName.Equals(""))
            {
                queryType = 1;
                resetearValores(queryType);
                fillCustomersDataGrid();
                timerBusquedaClientes.Stop();
            }
            else
            {
                queryType = 0;
                resetearValores(queryType);
                fillCustomersDataGrid();
                timerBusquedaClientes.Stop();
            }
        }

        private void pctBoxCliente_Click(object sender, EventArgs e)
        {
            FormImagen formImagen = new FormImagen(1, idCustomer);
            formImagen.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (CustomerADCModel.isTheCustomerSendedByIdPanel(Math.Abs(idCustomer)))
            {
                /*FormMessage formMessage = new FormMessage("Acción No Permitida", "El cliente ya fue sincronizado al servidor, no podemos eliminarlo", 3);
                formMessage.ShowDialog();*/
                FrmConfirmation frmConfirmation = new FrmConfirmation("Eliminar Cliente", "¿Deseas eliminar el cliente nuevo y sus documentos?");
                frmConfirmation.StartPosition = FormStartPosition.CenterScreen;
                frmConfirmation.ShowDialog();
                if (FrmConfirmation.confirmation)
                {
                    CustomerADCModel.deleteClienteNuevoConSusDocumentos(idCustomer);
                    idCustomer = 0;
                    fillAllDataCustomer(idCustomer, null);
                    btnDelete.Visible = false;
                    resetearValores(0);
                    fillCustomersDataGrid();
                }
            } else
            {
                FrmConfirmation frmConfirmation = new FrmConfirmation("Eliminar Cliente","¿Deseas eliminar el cliente nuevo y sus documentos?");
                frmConfirmation.StartPosition = FormStartPosition.CenterScreen;
                frmConfirmation.ShowDialog();
                if (FrmConfirmation.confirmation)
                {
                    CustomerADCModel.deleteClienteNuevoConSusDocumentos(idCustomer);
                    idCustomer = 0;
                    fillAllDataCustomer(idCustomer, null);
                    btnDelete.Visible = false;
                    resetearValores(0);
                    fillCustomersDataGrid();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            frmWaiting = new FormWaiting(this, 0);
            frmWaiting.ShowDialog();
        }

        public async Task updateNewCustomers()
        {
            int response = 0;
            if (serverModeLAN)
            {
                //response = await ClsCustomersController.downloadAllCustomersLAN(ClsInitialChargeController.UPDATE_DATA, lastIdCustomer);
                response = 1;
            }
            else
            {
                int lastIdCustomer = CustomerModel.getLastId();
                dynamic responseCliente = await CustomersController.downloadAllCustomersAPI(ClsInitialChargeController.UPDATE_DATA, lastIdCustomer);
                if (responseCliente.value == 1)
                    response = 1;
            }
            if (frmWaiting != null)
                frmWaiting.Close();
            if (response == 1)
            {
                resetearValores(0);
                fillCustomersDataGrid();
            } else if (response == 0)
            {
                FormMessage formMessage = new FormMessage("Sin Datos","No encontramos datos que actualizar",2);
                formMessage.ShowDialog();
            } else if (response == -1)
            {
                FormMessage formMessage = new FormMessage("Exception", "Ocurrió un problema al intentar procesar la respuesta", 2);
                formMessage.ShowDialog();
            } else
            {
                FormMessage formMessage = new FormMessage("Error de Conexión", "Ocurrió un error durante la conexión con el servidor", 2);
                formMessage.ShowDialog();
            }
            if (frmWaiting != null)
                frmWaiting.Close();
        }

        private void dataGridClientes_Scroll(object sender, ScrollEventArgs e)
        {
            if (customersList.Count < totalCustomers && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
                {
                    int contadorDisplayed = dataGridClientes.Rows.Count - getDisplayedRowsCount();
                    if (e.NewValue >= contadorDisplayed)
                    {
                        //prevent loading from autoscroll.
                        TimeSpan ts = DateTime.Now - lastLoading;
                        if (ts.TotalMilliseconds > 100)
                        {
                            firstVisibleRow = e.NewValue;
                            //dataGridItems.PerformLayout();
                            fillCustomersDataGrid();
                        }
                        else
                        {
                            dataGridClientes.FirstDisplayedScrollingRowIndex = e.OldValue;
                        }
                    }
                }
            }
        }

        private int getDisplayedRowsCount()
        {
            int count = dataGridClientes.Rows[dataGridClientes.FirstDisplayedScrollingRowIndex].Height;
            count = dataGridClientes.Height / count;
            return count;
        }

        private void resetearValores(int queryType)
        {
            this.queryType = queryType;
            query = "";
            queryTotals = "";
            totalCustomers = 0;
            lastId = 0;
            progress = 0;
            firstVisibleRow = 0;
            customersList.Clear();
            dataGridClientes.Rows.Clear();
        }

    }
}
