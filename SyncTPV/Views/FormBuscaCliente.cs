using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using SyncTPV.Views.Customers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using wsROMClases.Models;
using wsROMClases.Models.Commercial;

namespace SyncTPV
{
    public partial class FormBuscaCliente : Form
    {
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
        private bool serverModeLAN = false, permissionPrepedido = false, webActive = false;
        private String comInstance = "", panelInstance = "", codigoCaja = "";

        public FormBuscaCliente(bool serverModeLAN, bool permissionPrepedido)
        {
            this.serverModeLAN = serverModeLAN;
            this.permissionPrepedido = permissionPrepedido;
            InitializeComponent();
            btnClose.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.back_white, 40, 40);
            btnAgregarCliente.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.add_customer_white, 40, 40);
            customersList = new List<ClsClienteModel>();
        }

        private async void frmBuscaCliente_Load(object sender, EventArgs e)
        {
            CustomerModel.validarCLientesADCyCustomers();
            if (serverModeLAN)
            {
                await Task.Run(async () =>
                {
                    comInstance = InstanceSQLSEModel.getStringComInstance();
                    panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    codigoCaja = ClsCajaModel.getBoxCodeByAgentId(panelInstance, ClsRegeditController.getIdUserInTurn());
                });
            } else
            {
                int lastIdNuevo = CustomerADCModel.getLastId();
                if (lastIdNuevo > 0)
                {
                    lastIdNuevo++;
                    lastId = -lastIdNuevo;
                }
                else lastId = 0;
                await Task.Run(async () =>
                {
                    dynamic responseWeb = ConfiguracionModel.webActive();
                    if (responseWeb.value == 1)
                        webActive = responseWeb.active;
                });
            }
            await deleteAllCustomersSendedAndUploadedToComercialPremium();
            if (permissionPrepedido)
            {
                btnAgregarCliente.Visible = false;
            } else btnAgregarCliente.Visible = true;
            editBuscarCliente.Select();
            await fillCustomersDataGrid();
        }

        private async Task deleteAllCustomersSendedAndUploadedToComercialPremium()
        {
            await Task.Run(async () =>
            {
                CustomerADCModel.deleteAllCustomersUploadedToComercial();
            });
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            } else if (e.KeyCode == Keys.Down)
            {
                dtGridBuscaClientes.Select();
            }
        }

        private void hideScrollBars()
        {
            //imgSinDatos.Image = ClsMetodosGenerales.redimencionarBitmap(Properties.Resources.logosynctpvmoving, 300, 300);
            //imgSinDatos.Visible = true;
            gridScrollBars = dtGridBuscaClientes.ScrollBars;
            //dataGridItems.ScrollBars = ScrollBars.None;
        }

        private async Task fillCustomersDataGrid()
        {
            CustomerModel.validarCLientesADCyCustomers();
            hideScrollBars();
            lastLoading = DateTime.Now;
            customersListTemp = await getAllCustomers();
            if (customersListTemp != null)
            {
                progress += customersListTemp.Count;
                customersList.AddRange(customersListTemp);
                if (customersList.Count > 0 && dtGridBuscaClientes.ColumnHeadersVisible == false)
                    dtGridBuscaClientes.ColumnHeadersVisible = true;
                for (int i = 0; i < customersListTemp.Count; i++)
                {
                    int n = dtGridBuscaClientes.Rows.Add();
                    dtGridBuscaClientes.Rows[n].Cells[0].Value = customersListTemp[i].CLIENTE_ID;
                    dtGridBuscaClientes.Rows[n].Cells[1].Value = customersListTemp[i].CLAVE;
                    dtGridBuscaClientes.Columns[1].Width = 100;
                    if (customersListTemp[i].CLIENTE_ID < 0)
                        dtGridBuscaClientes.Rows[n].Cells[2].Value = customersListTemp[i].NOMBRE + " (Nuevo)";
                    else dtGridBuscaClientes.Rows[n].Cells[2].Value = customersListTemp[i].NOMBRE;
                }
                
                dtGridBuscaClientes.PerformLayout();
                customersListTemp.Clear();
                if (customersList.Count > 0)
                    lastId = Convert.ToInt32(customersList[customersList.Count - 1].CLIENTE_ID);
            }
            else
            {
                //if (progress == 0)
                    //imgSinDatos.Visible = true;
            }
            textTotalClientes.Text = "Clientes: " + totalCustomers.ToString().Trim();
            if (firstVisibleRow > -1)
            {
                showScrollBars();
                if (customersList.Count > 0)
                {
                    dtGridBuscaClientes.FirstDisplayedScrollingRowIndex = firstVisibleRow;
                    //imgSinDatos.Visible = false;
                }
            }
        }

        private void showScrollBars()
        {
            dtGridBuscaClientes.ScrollBars = gridScrollBars;
        }

        private async Task<List<ClsClienteModel>> getAllCustomers()
        {
            List<ClsClienteModel> customersList = null;
            await Task.Run(async () =>
            {
                if (serverModeLAN)
                {
                    if (checkBoxMostrarNuevos.Checked)
                    {
                        if (queryType == 0)
                        {
                           
                                
                            query = "SELECT * FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " < " + lastId + " ORDER BY " +
                           LocalDatabase.CAMPO_ID_CLIENTE;
                            queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " < " + lastId + " ORDER BY " +
                           LocalDatabase.CAMPO_ID_CLIENTE;
                            totalCustomers = CustomerModel.getTotalRecordsWithParameters(queryTotals, "", "", 0);
                            customersList = CustomerModel.getAllCustomersWithParameters(query, "", "", 0);
                            
                        }
                        else if (queryType == 1)
                        {
                            query = "SELECT * FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE (" +
                        LocalDatabase.CAMPO_NOMBRECLIENTE + " LIKE @parameterName COLLATE Latin1_General_CI_AI OR " + 
                        LocalDatabase.CAMPO_CLAVECLIENTE + " LIKE @parameterName COLLATE Latin1_General_CI_AI)" +
                        " AND " + LocalDatabase.CAMPO_ID_CLIENTE + " < " + lastId + " ORDER BY " +
                            LocalDatabase.CAMPO_ID_CLIENTE + " LIMIT " + LIMIT;
                            queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE (" +
                        LocalDatabase.CAMPO_NOMBRECLIENTE + " LIKE @parameterName COLLATE Latin1_General_CI_AI OR " + 
                        LocalDatabase.CAMPO_CLAVECLIENTE + " LIKE @parameterName COLLATE Latin1_General_CI_AI)"+
                        " AND " + LocalDatabase.CAMPO_ID_CLIENTE + " < " + lastId + " ORDER BY ";
                            totalCustomers = CustomerModel.getTotalRecordsWithParameters(queryTotals, "parameterName", customerCodeOrName, 1);
                            customersList = CustomerModel.getAllCustomersWithParameters(query, "parameterName", customerCodeOrName, 1);
                        }
                    } else
                    {
                        if (queryType == 0)
                        {
                            totalCustomers = ClsCustomersModel.getTotalOfCustomersTPV(comInstance, panelInstance, codigoCaja);
                            dynamic responseCustomer = ClsCustomersModel.getAllCustomersFromAnUser(comInstance, panelInstance, 
                                codigoCaja, true, lastId, LIMIT);
                            
                            queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_CLIENTES;
                            totalCustomers = CustomerModel.getTotalRecordsWithParameters(queryTotals, "", "", 0);
                            customersList = responseCustomer.customersList;
                            
                            
                        }
                        else if (queryType == 1)
                        {
                            query = "SELECT * FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE (" +
                            LocalDatabase.CAMPO_NOMBRECLIENTE + " LIKE @parameterName COLLATE Latin1_General_CI_AI OR " +
                            LocalDatabase.CAMPO_CLAVECLIENTE + " LIKE @parameterName COLLATE Latin1_General_CI_AI)" +
                            " AND " + LocalDatabase.CAMPO_ID_CLIENTE + " > " + lastId + " ORDER BY " +
                            LocalDatabase.CAMPO_ID_CLIENTE + " LIMIT " + LIMIT;
                            queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE (" +
                            LocalDatabase.CAMPO_NOMBRECLIENTE + " LIKE @parameterName COLLATE Latin1_General_CI_AI OR " +
                            LocalDatabase.CAMPO_CLAVECLIENTE + " LIKE @parameterName COLLATE Latin1_General_CI_AI)" +
                            " AND " + LocalDatabase.CAMPO_ID_CLIENTE + " > " + lastId ;
                            totalCustomers = CustomerModel.getTotalRecordsWithParameters(queryTotals, "parameterName", customerCodeOrName, 1);
                            customersList = CustomerModel.getAllCustomersWithParameters(query, "parameterName", customerCodeOrName, 1);

                        }
                    }
                } else
                {
                    if (webActive)
                    {
                        if (queryType == 0)
                        {
                            if (lastId >= 0)
                            {
                                dynamic responseCustomerTotal = await CustomersController.getTotalOfCustomersAPI(lastId, LIMIT, 0, "", "");
                                if (responseCustomerTotal.value == 1)
                                {
                                    totalCustomers = responseCustomerTotal.total;
                                    dynamic responseCustomer = await CustomersController.getAllCustomersAPI(lastId, LIMIT, "", "");
                                    if (responseCustomer.value == 1)
                                        customersList = responseCustomer.customersList;
                                    else
                                    {
                                        query = "SELECT * FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " > " + lastId + " ORDER BY " +
                               LocalDatabase.CAMPO_ID_CLIENTE + " LIMIT " + LIMIT;
                                        queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_CLIENTES;
                                        totalCustomers = CustomerModel.getTotalRecordsWithParameters(queryTotals, "", "", 0);
                                        customersList = CustomerModel.getAllCustomersWithParameters(query, "", "", 0);
                                    }
                                }
                                else
                                {
                                    query = "SELECT * FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " > " + lastId + " ORDER BY " +
                               LocalDatabase.CAMPO_ID_CLIENTE + " LIMIT " + LIMIT;
                                    queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_CLIENTES;
                                    totalCustomers = CustomerModel.getTotalRecordsWithParameters(queryTotals, "", "", 0);
                                    customersList = CustomerModel.getAllCustomersWithParameters(query, "", "", 0);
                                }
                            }
                            else
                            {
                                query = "SELECT * FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " > " + lastId + " ORDER BY " +
                               LocalDatabase.CAMPO_ID_CLIENTE + " LIMIT " + LIMIT;
                                queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_CLIENTES;
                                totalCustomers = CustomerModel.getTotalRecordsWithParameters(queryTotals, "", "", 0);
                                customersList = CustomerModel.getAllCustomersWithParameters(query, "", "", 0);
                            }
                        }
                        else
                        {
                            if (lastId >= 0)
                            {
                                dynamic responseCustomerTotal = await CustomersController.getTotalOfCustomersAPI(lastId, LIMIT, 1, "parameterName", customerCodeOrName);
                                if (responseCustomerTotal.value == 1)
                                {
                                    totalCustomers = responseCustomerTotal.total;
                                    dynamic responseCustomer = await CustomersController.getAllCustomersAPI(lastId, LIMIT, "parameterName", customerCodeOrName);
                                    if (responseCustomer.value == 1)
                                        customersList = responseCustomer.customersList;
                                    else
                                    {
                                        query = "SELECT * FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE (" +
                           LocalDatabase.CAMPO_NOMBRECLIENTE + " LIKE @parameterName COLLATE Latin1_General_CI_AI OR " +
                           LocalDatabase.CAMPO_CLAVECLIENTE + " LIKE @parameterName COLLATE Latin1_General_CI_AI) " +
                           " AND " + LocalDatabase.CAMPO_ID_CLIENTE + " > " + lastId + " ORDER BY " +
                               LocalDatabase.CAMPO_ID_CLIENTE + " LIMIT " + LIMIT;
                                        queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE (" +
                                    LocalDatabase.CAMPO_NOMBRECLIENTE + " LIKE @parameterName COLLATE Latin1_General_CI_AI OR " +
                                    LocalDatabase.CAMPO_CLAVECLIENTE + " LIKE @parameterName COLLATE Latin1_General_CI_AI)";
                                        totalCustomers = CustomerModel.getTotalRecordsWithParameters(queryTotals, "parameterName", customerCodeOrName, 1);
                                        customersList = CustomerModel.getAllCustomersWithParameters(query, "parameterName", customerCodeOrName, 1);
                                    }
                                }
                                else
                                {
                                    query = "SELECT * FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE (" +
                           LocalDatabase.CAMPO_NOMBRECLIENTE + " LIKE @parameterName COLLATE Latin1_General_CI_AI OR " +
                           LocalDatabase.CAMPO_CLAVECLIENTE + " LIKE @parameterName COLLATE Latin1_General_CI_AI)" +
                           " AND " + LocalDatabase.CAMPO_ID_CLIENTE + " < " + lastId + " ORDER BY " +
                               LocalDatabase.CAMPO_ID_CLIENTE + " LIMIT " + LIMIT;
                                    queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE (" +
                                LocalDatabase.CAMPO_NOMBRECLIENTE + " LIKE @parameterName COLLATE Latin1_General_CI_AI OR " +
                                LocalDatabase.CAMPO_CLAVECLIENTE + " LIKE @parameterName COLLATE Latin1_General_CI_AI) "+
                                " AND " + LocalDatabase.CAMPO_ID_CLIENTE + " < " + lastId + " ORDER BY ";
                                    totalCustomers = CustomerModel.getTotalRecordsWithParameters(queryTotals, "parameterName", customerCodeOrName, 1);
                                    customersList = CustomerModel.getAllCustomersWithParameters(query, "parameterName", customerCodeOrName, 1);
                                }
                            }
                            else
                            {
                                query = "SELECT * FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE (" +
                           LocalDatabase.CAMPO_NOMBRECLIENTE + " LIKE @parameterName COLLATE Latin1_General_CI_AI OR " +
                           LocalDatabase.CAMPO_CLAVECLIENTE + " LIKE @parameterName COLLATE Latin1_General_CI_AI)" +
                           " AND " + LocalDatabase.CAMPO_ID_CLIENTE + " > " + lastId + " ORDER BY " +
                               LocalDatabase.CAMPO_ID_CLIENTE + " LIMIT " + LIMIT;
                                queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE (" +
                            LocalDatabase.CAMPO_NOMBRECLIENTE + " LIKE @parameterName COLLATE Latin1_General_CI_AI OR " +
                            LocalDatabase.CAMPO_CLAVECLIENTE + " LIKE @parameterName COLLATE Latin1_General_CI_AI)";
                                totalCustomers = CustomerModel.getTotalRecordsWithParameters(queryTotals, "parameterName", customerCodeOrName, 1);
                                customersList = CustomerModel.getAllCustomersWithParameters(query, "parameterName", customerCodeOrName, 1);
                            }
                        }
                    } else
                    {
                        if (queryType == 0)
                        {
                            if (lastId >= 0)
                            {
                                query = "SELECT * FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " > " + lastId + " ORDER BY " +
                               LocalDatabase.CAMPO_ID_CLIENTE + " LIMIT " + LIMIT;
                                queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_CLIENTES;
                                totalCustomers = CustomerModel.getTotalRecordsWithParameters(queryTotals, "", "", 0);
                                customersList = CustomerModel.getAllCustomersWithParameters(query, "", "", 0);
                            }
                            else
                            {
                                query = "SELECT * FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " > " + lastId + " ORDER BY " +
                               LocalDatabase.CAMPO_ID_CLIENTE + " LIMIT " + LIMIT;
                                queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_CLIENTES;
                                totalCustomers = CustomerModel.getTotalRecordsWithParameters(queryTotals, "", "", 0);
                                customersList = CustomerModel.getAllCustomersWithParameters(query, "", "", 0);
                            }
                        }
                        else
                        {
                            query = "SELECT * FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE (" +
                            LocalDatabase.CAMPO_NOMBRECLIENTE + " LIKE @parameterName COLLATE Latin1_General_CI_AI OR " +
                            LocalDatabase.CAMPO_CLAVECLIENTE + " LIKE @parameterName COLLATE Latin1_General_CI_AI)" +
                            " AND " + LocalDatabase.CAMPO_ID_CLIENTE + " < " + lastId + " ORDER BY " +
                            LocalDatabase.CAMPO_ID_CLIENTE + " LIMIT " + LIMIT;
                            queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE (" +
                            LocalDatabase.CAMPO_NOMBRECLIENTE + " LIKE @parameterName COLLATE Latin1_General_CI_AI OR " +
                            LocalDatabase.CAMPO_CLAVECLIENTE + " LIKE @parameterName COLLATE Latin1_General_CI_AI)" +
                            " AND " + LocalDatabase.CAMPO_ID_CLIENTE + " < " + lastId ;
                            totalCustomers = CustomerModel.getTotalRecordsWithParameters(queryTotals, "parameterName", customerCodeOrName, 1);
                            customersList = CustomerModel.getAllCustomersWithParameters(query, "parameterName", customerCodeOrName, 1);
                        }
                    }
                }
            });
            return customersList;
        }

        private void dtGridBuscaClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                FormVenta.idCustomer = Convert.ToInt32(dtGridBuscaClientes.CurrentRow.Cells[0].Value.ToString());
                this.Close();
            }
        }

        private void dtGridBuscaClientes_Scroll(object sender, ScrollEventArgs e)
        {
            if (customersList.Count < totalCustomers && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
                {
                    if (e.NewValue > dtGridBuscaClientes.Rows.Count - getDisplayedRowsCount())
                    {
                        //prevent loading from autoscroll.
                        TimeSpan ts = DateTime.Now - lastLoading;
                        if (ts.TotalMilliseconds > 100)
                        {
                            firstVisibleRow = e.NewValue;
                            fillCustomersDataGrid();
                        }
                        else
                        {
                            dtGridBuscaClientes.FirstDisplayedScrollingRowIndex = e.OldValue;
                        }
                    }
                }
            }
        }

        private int getDisplayedRowsCount()
        {
            int count = dtGridBuscaClientes.Rows[dtGridBuscaClientes.FirstDisplayedScrollingRowIndex].Height;
            count = dtGridBuscaClientes.Height / count;
            return count;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            timerBuscarCliente.Stop();
            timerBuscarCliente.Start();
        }

        private void checkBoxMostrarNuevos_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMostrarNuevos.Checked)
            {
                editBuscarCliente.Select();
                resetearValores(queryType);
                fillCustomersDataGrid();
                timerBuscarCliente.Stop();
            } else
            {
                editBuscarCliente.Select();
                resetearValores(queryType);
                fillCustomersDataGrid();
                timerBuscarCliente.Stop();
            }
        }

        private void dtGridBuscaClientes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                
            }
        }

        private void dtGridBuscaClientes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dtGridBuscaClientes.CurrentCell.ColumnIndex >= 0 && dtGridBuscaClientes.CurrentRow.Index >= 0)
                {
                    FormVenta.idCustomer = Convert.ToInt32(dtGridBuscaClientes.CurrentRow.Cells[0].Value.ToString());
                    this.Close();
                }
            }
        }

        private void editBuscarCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (dtGridBuscaClientes.CurrentCell.ColumnIndex >= 0 && dtGridBuscaClientes.CurrentRow.Index >= 0)
                {
                    FormVenta.idCustomer = Convert.ToInt32(dtGridBuscaClientes.CurrentRow.Cells[0].Value.ToString());
                    this.Close();
                }
            }
        }

        private void timerBuscarCliente_Tick(object sender, EventArgs e)
        {
            customerCodeOrName = editBuscarCliente.Text.ToString().Trim();
            if (!customerCodeOrName.Equals(""))
            {
                queryType = 1;
                resetearValores(queryType);
                fillCustomersDataGrid();
                timerBuscarCliente.Stop();
            }
            else
            {
                queryType = 0;
                resetearValores(queryType);
                fillCustomersDataGrid();
                timerBuscarCliente.Stop();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void FormBuscaCliente_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                LIMIT = 60;
                resetearValores(queryType);
                await fillCustomersDataGrid();
            }
            else
            {
                LIMIT = 30;
                resetearValores(queryType);
                await fillCustomersDataGrid();
            }
        }

        private void btnAgregarCliente_Click(object sender, EventArgs e)
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

        private void dtGridBuscaClientes_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmBuscaCliente_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void picCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtGridBuscaClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void resetearValores(int queryType)
        {
            this.queryType = queryType;
            query = "";
            queryTotals = "";
            totalCustomers = 0;
            if (serverModeLAN)
                lastId = 0;
            else
            {
                int lastIdNuevo = CustomerADCModel.getLastId();
                if (lastIdNuevo > 0)
                {
                    lastIdNuevo++;
                    lastId = -lastIdNuevo;
                }
                else lastId = 0;
            }
            firstVisibleRow = 0;
            progress = 0;
            customersList.Clear();
            dtGridBuscaClientes.Rows.Clear();
        }

    }
}
