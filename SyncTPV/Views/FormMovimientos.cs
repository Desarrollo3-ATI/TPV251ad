using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using SyncTPV.Views;
using SyncTPV.Views.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using wsROMClases.Models.Commercial;

namespace SyncTPV
{
    public partial class FormMovimientos : Form
    {
        public static readonly int TIPO_MOVDOC = 1, TIPOMOVPED = 2;
        private int idDocument = 0;
        private DocumentModel dvm;
        private PedidosEncabezadoModel pem;
        private int movementsType = 0;
        private int LIMIT = 100;
        private int progress = 0;
        private int lastId = 0;
        private int totalMovimientos = 0, queryType = 0;
        private String query = "", queryTotals = "", itemCodeOrName = "";
        private DateTime lastLoading;
        private int firstVisibleRow;
        private ScrollBars gridScrollBars;
        private List<MovimientosModel> movementsList;
        private List<MovimientosModel> movementsListTemp;
        private List<PedidoDetalleModel> movementsCreditList;
        private List<PedidoDetalleModel> movementsCreditListTemp;
        private bool permissionPrepedido = false, serverModeLAN = false;
        private int dgvPosition = 0;
        private FrmResumenDocuments frmResumenDocuments;
        private bool cotmosActive = false;
        private String codigoCaja = "";

        public FormMovimientos(int idDocument, int movementsType, int dgvPosition, bool cotmosActive)
        {
            this.idDocument = idDocument;
            this.movementsType = movementsType;
            this.dgvPosition = dgvPosition;
            InitializeComponent();
            dataGridMovs.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(GeneralTxt.RedSelectionRows, GeneralTxt.GreenSelectionRows, GeneralTxt.BlueSelectionRows);
            btnClose.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.back_white, 40, 40);
            btnCancelar.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.delete_white, 40, 40);
            btnImprimir.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.printer_black,40,40);
            btnEditar.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.edit_black, 40,40);
            btnFactura.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.pdf_white, 40, 40);
            btnimprimirpuro.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.printer_white, 40, 40);
            movementsList = new List<MovimientosModel>();
            movementsCreditList = new List<PedidoDetalleModel>();
            this.cotmosActive = cotmosActive;
        }

        public FormMovimientos(FrmResumenDocuments frmResumenDocuments, int idDocument, int movementsType, int dgvPosition)
        {
            this.frmResumenDocuments = frmResumenDocuments;
            this.idDocument = idDocument;
            this.movementsType = movementsType;
            this.dgvPosition = dgvPosition;
            InitializeComponent();
            dataGridMovs.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(GeneralTxt.RedSelectionRows, GeneralTxt.GreenSelectionRows, GeneralTxt.BlueSelectionRows);
            btnClose.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.back_white, 40, 40);
            btnCancelar.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.delete_white, 40, 40);
            btnImprimir.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.printer_black, 40, 40);
            btnEditar.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.edit_black, 40, 40);
            btnFactura.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.pdf_white, 40, 40);
            btnimprimirpuro.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.printer_white, 40, 40);
            movementsList = new List<MovimientosModel>();
            movementsCreditList = new List<PedidoDetalleModel>();
        }

        private async void FrmEstadoDoc_Load(object sender, EventArgs e)
        {
            permissionPrepedido = UserModel.doYouHavePermissionPrepedido();
            serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
            await loadInitialData();
            if (permissionPrepedido)
            {
                String queryMov = "SELECT * FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " + LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " +
                                idDocument;
                MovimientosModel mm = MovimientosModel.getAMovement(queryMov);
                if (mm != null)
                {
                    String queryPeso = "SELECT * FROM " + LocalDatabase.TABLA_PESO + " WHERE " + LocalDatabase.CAMPO_MOVIMIENTOID_PESO +
                        " = " + mm.id;
                    WeightModel wm = WeightModel.getAWeight(queryPeso);
                    if (wm != null)
                    {
                        if (wm.pesoCaja == 0)
                        {
                            btnEditar.Text = "Destarar";
                        }
                        else
                        {
                            btnEditar.Text = "Destarado";
                        }
                    }
                    else
                    {
                        btnEditar.Text = "Pesar";
                    }
                }
                else
                {
                    btnEditar.Text = "Documento Incompleto";
                }
                btnFactura.Visible = false;
            }
            if (movementsType == TIPO_MOVDOC)
            {
                verifyStatusDocument();
                fillDataTextInformacionDocument();
                fillDataGridMovements();
            }
            else if (movementsType == TIPOMOVPED)
            {
                btnEditar.Enabled = false;
                btnImprimir.Enabled = false;
                btnFactura.Enabled = false;
                btnCancelar.Enabled = false;                
                fillDataTextInformacionPedidos();
                fillDataGridPedidos();
            } else if (movementsType == 3)
            {

            }
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
                        value = 1;
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
                    value = 1;
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

        private async Task verifyStatusDocument()
        {
            int status = await getStatusDocument();
            if (status == 1)
            {
                btnCancelar.Enabled = true;
                btnEditar.Enabled = false;
                btnImprimir.Enabled = true;
                btnFactura.Enabled = true;
                textStatus.Text = "Enviado al Servidor";
                textStatus.ForeColor = Color.Blue;
            }
            else if (status == 2)
            {
                btnCancelar.Enabled = true;
                if (permissionPrepedido)
                {
                    btnImprimir.Enabled = true;
                    btnEditar.Enabled = true;
                }
                else
                {
                    btnImprimir.Enabled = false;
                    btnEditar.Enabled = true;
                    btnEditar.Text = "Modificar";
                }
                btnFactura.Enabled = false;
                textStatus.Text = "Pausado o Pendiente";
                textStatus.ForeColor = Color.GreenYellow;
            }
            else if (status == 3)
            {
                btnCancelar.Enabled = true;
                btnFactura.Enabled = false;
                if (permissionPrepedido)
                {
                    btnEditar.Enabled = false;
                }
                else
                {
                    btnEditar.Enabled = true;
                    btnEditar.Text = "Editar";
                }
                textStatus.Text = "No enviado";
                textStatus.ForeColor = Color.Red;
            }
        }

        private async Task<int> getStatusDocument()
        {
            int status = 0;
            await Task.Run(async () =>
            {
                String query = "SELECT " + LocalDatabase.CAMPO_ENVIADOALWS_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA +
                " WHERE " + LocalDatabase.CAMPO_ID_DOC + " = " + idDocument;
                int sended = DocumentModel.getIntValue(query);
                query = "SELECT " + LocalDatabase.CAMPO_PAUSAR_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA +
                " WHERE " + LocalDatabase.CAMPO_ID_DOC + " = " + idDocument;
                int paused = DocumentModel.getIntValue(query);
                if (sended == 1)
                    status = 1;
                else if (paused == 1 && sended == 0)
                    status = 2;
                else if (sended == 0 && paused == 0)
                    status = 3;
            });
            return status;
        }

        private async Task fillDataTextInformacionDocument()
        {
            String documentType = await fillDocumentTypeData();
            if (!documentType.Equals(""))
                textDatosDocumentoMovimientosDocs.Text = documentType;
            if (dvm != null)
            {
                String customerNme = "";
                if (dvm.cliente_id < 0)
                {
                    dynamic responseNewCustomer = await downloadNewCustomerAndUpdateDocument(dvm.cliente_id);
                    if (responseNewCustomer.value == 1)
                    {
                        ClsClienteModel customerModel = responseNewCustomer.customerModel;
                        if (customerModel != null)
                            customerNme = customerModel.NOMBRE;
                    } else
                    {
                        FormMessage formMessage = new FormMessage("Cliente Nuevo", responseNewCustomer.description, 3);
                        formMessage.ShowDialog();
                    }
                } else
                {
                    customerNme = CustomerModel.getName(dvm.cliente_id);
                    if (customerNme.Equals(""))
                    {
                        if (serverModeLAN)
                        {
                            dynamic responseCustomer = await CustomersController.getACustomerLAN(dvm.cliente_id);
                            if (responseCustomer.value == 1)
                            {
                                ClsClienteModel cm = responseCustomer.customerModel;
                                if (cm != null)
                                    customerNme = cm.NOMBRE;
                            } else
                            {
                                FormMessage formMessage = new FormMessage("Buscando Cliente", responseCustomer.description, 3);
                                formMessage.ShowDialog();
                            }
                        } else
                        {
                            dynamic responseCustomer = await CustomersController.getACustomerAPI(dvm.cliente_id);
                            if (responseCustomer.value == 1)
                            {
                                ClsClienteModel cm = responseCustomer.customerModel;
                                if (cm != null)
                                    customerNme = cm.NOMBRE;
                            } else
                            {
                                FormMessage formMessage = new FormMessage("Buscando Cliente", responseCustomer.description, 3);
                                formMessage.ShowDialog();
                            }
                        }
                    }
                }
                editCliente.Text = customerNme;
                editCliente.ReadOnly = true;
                editObservationDoc.Text = dvm.observacion;
                editObservationDoc.ReadOnly = true;
                editFolioDocumento.Text = dvm.fventa;
                editFolioDocumento.ReadOnly = true;
                double subtotal = dvm.total + dvm.descuento;
                editSubtotalDocumento.Text = subtotal.ToString("C2") + " MXN";
                editSubtotalDocumento.ReadOnly = true;
                editDescuentoDocumento.Text = dvm.descuento.ToString("C2") + " MXN";
                editDescuentoDocumento.ReadOnly = true;
                editTotalDocumento.Text = dvm.total.ToString("C2") + " MXN";
                editTotalDocumento.ReadOnly = true;
            }
        }

        private async Task<ExpandoObject> downloadNewCustomerAndUpdateDocument(int idCliente)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                ClsClienteModel customerModel = null;
                try
                {
                    if (serverModeLAN)
                    {
                        dynamic responseNewCustomer = await CustomersController.getANewCustomerLAN(idCliente);
                        if (responseNewCustomer.value == 1)
                        {
                            value = 1;
                            customerModel = responseNewCustomer.customerModel;
                        }
                        else
                        {
                            description = responseNewCustomer.description;
                        }
                    }
                    else
                    {
                        dynamic responseNewCustomer = await CustomersController.getANewCustomerAPI(idCliente);
                        if (responseNewCustomer.value == 1)
                        {
                            value = 1;
                            customerModel = responseNewCustomer.customerModel;
                        }
                        else
                        {
                            description = responseNewCustomer.description;
                        }
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
                    response.customerModel = customerModel;
                }
            });
            return response;
        }


        private async Task fillDataTextInformacionPedidos()
        {
            String textoInformacion = await getAllDataPedidos();
            if (!textoInformacion.Equals(""))
                textDatosDocumentoMovimientosDocs.Text = textoInformacion;
            if (pem != null)
            {
                String cutomerName = CustomerModel.getName(pem.clienteId);
                if (cutomerName.Equals(""))
                {
                    if (pem.clienteId < 0)
                        editCliente.Text = "Cliente Nuevo";
                    else editCliente.Text = cutomerName;
                }
                else editCliente.Text = cutomerName;
                editCliente.ReadOnly = true;
                editFolioDocumento.Text = pem.folio;
                editFolioDocumento.ReadOnly = true;
                editObservationDoc.Text = pem.observation;
                editObservationDoc.ReadOnly = true;
                double subtotal = pem.total - pem.descuento;
                editSubtotalDocumento.Text = subtotal.ToString("C2") + " MXN";
                editSubtotalDocumento.ReadOnly = true;
                editDescuentoDocumento.Text = pem.descuento.ToString("C2") + " MXN";
                editDescuentoDocumento.ReadOnly = true;
                editTotalDocumento.Text = pem.total.ToString("C2") + " MXN";
                editTotalDocumento.ReadOnly = true;
            }
            if (permissionPrepedido)
            {

                textSubtotalDocumento.Visible = false;
                editSubtotalDocumento.Visible = false;
                textDescuentoDocumento.Visible = false;
                editDescuentoDocumento.Visible = false;
                textTotalDocumento.Visible = false;
                editTotalDocumento.Visible = false;
            }
        }

        private async Task<string> fillDocumentTypeData()
        {
            String tipoDocument = "";
            await Task.Run(async () =>
            {
                dvm = DocumentModel.getAllDataDocumento(idDocument);
                if (dvm != null)
                {
                    if (dvm.tipo_documento == 1)
                    {
                        tipoDocument = "Documento de Cotización";
                    }
                    else if (dvm.tipo_documento == 2)
                    {
                        if (permissionPrepedido)
                            tipoDocument = "Documento De Entrega";
                        else tipoDocument = "Documento De Venta";
                    }
                    else if (dvm.tipo_documento == 3)
                    {
                        tipoDocument = "Documento De Pedido";
                    }
                    else if (dvm.tipo_documento == 4)
                    {
                        if (permissionPrepedido)
                            tipoDocument = "Documento De Entrega";
                        else tipoDocument = "Documento De Remisión";
                    }
                    else if (dvm.tipo_documento == 5)
                    {
                        tipoDocument = "Documento Devolución";
                    } else if (dvm.tipo_documento == DocumentModel.TIPO_PREPEDIDO)
                    {
                        tipoDocument = "Prepedido";
                    } 
                    else if (dvm.tipo_documento == 51)
                    {
                        tipoDocument = "Documento Cotización de Mostrador";
                    }
                }
            });
            return tipoDocument;
        }

        private async Task<string> getAllDataPedidos()
        {
            String data = "";
            await Task.Run(async () =>
            {
                pem = PedidosEncabezadoModel.getAPedCot(idDocument);
                if (pem != null)
                {
                    String queryCustomer = "SELECT " + LocalDatabase.CAMPO_NOMBRECLIENTE + " FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " +
                    LocalDatabase.CAMPO_ID_CLIENTE + " = " + pem.clienteId;
                    String tipoDocument = "";
                    if (pem.type == PedidosEncabezadoModel.TYPE_CC)
                        tipoDocument = "Documento de Pedido CallCenter";
                    else if (pem.type == PedidosEncabezadoModel.TYPE_COMERCIAL)
                        tipoDocument = "Documento de Pedido";
                    else if (pem.type == PedidosEncabezadoModel.TYPE_COTIZACIONMOSTRADOR)
                        tipoDocument = "Documento de Cotización Mostrador";
                    else if (pem.type == PedidosEncabezadoModel.TYPE_PREPEDIDOS)
                        tipoDocument = "Documento de Pedido";
                    data = "" + tipoDocument;
                }
            });
            return data;
        }

        private void hideScrollBars()
        {
            imgSinDatos.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.logosynctpvmoving, 300, 300);
            imgSinDatos.Visible = true;
            gridScrollBars = dataGridMovs.ScrollBars;
            //dataGridItems.ScrollBars = ScrollBars.None;
        }

        private async Task fillDataGridMovements()
        {
            hideScrollBars();
            lastLoading = DateTime.Now;
            movementsListTemp = await getAllMovementsFromADocument();
            if (movementsListTemp != null)
            {
                progress += movementsListTemp.Count;
                movementsList.AddRange(movementsListTemp);
                if (movementsList.Count > 0 && dataGridMovs.ColumnHeadersVisible == false)
                    dataGridMovs.ColumnHeadersVisible = true;
                for (int i = 0; i < movementsListTemp.Count; i++)
                {
                    int n = dataGridMovs.Rows.Add();
                    dataGridMovs.Rows[n].Cells[0].Value = movementsListTemp[i].id + "";
                    dataGridMovs.Columns["idMovDoc"].Visible = false;
                    if (movementsListTemp[i].itemId != 0)
                    {
                        String itemName = ItemModel.getTheNameOfAnItem(movementsListTemp[i].itemId);
                        if (itemName.Equals(""))
                        {
                            dynamic response = null;
                            if (serverModeLAN)
                                response = await ItemsController.getItemNameLAN(movementsListTemp[i].itemId);
                            else response = await ItemsController.getItemNameAPI(movementsListTemp[i].itemId, codigoCaja);
                            if (response != null)
                            {
                                if (response.value == 1)
                                    itemName = response.name;
                            }
                        }
                        dataGridMovs.Rows[n].Cells[1].Value = itemName;
                    } else dataGridMovs.Rows[n].Cells[1].Value = "Producto No Encontrado";
                    dataGridMovs.Columns[1].Width = 230;
                    dataGridMovs.Rows[n].Cells[2].Value = movementsListTemp[i].capturedUnits;
                    if (movementsListTemp[i].capturedUnitId != 0)
                    {
                        String unidadName = "(Ninguna)";// UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(movementsListTemp[i].capturedUnitId);
                        if (serverModeLAN)
                        {
                            dynamic responseUnitName = await UnitsOfMeasureAndWeightController.getNameOfAndUnitMeasureWeightLAN(movementsListTemp[i].capturedUnitId);
                            if (responseUnitName.value == 1)
                                unidadName = responseUnitName.name;
                        }
                        else unidadName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(movementsListTemp[i].capturedUnitId);
                        dataGridMovs.Rows[n].Cells[3].Value = unidadName;
                    } else
                    {
                        dataGridMovs.Rows[n].Cells[3].Value = "(Ninguna)";
                    }
                    if (UserModel.doYouHavePermissionPrepedido())
                    {
                        dataGridMovs.Columns[4].HeaderText = "Total de Pollos";
                        String unitName = "(Ninguna)";//UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(movementsListTemp[i].nonConvertibleUnitId);
                        if (serverModeLAN)
                        {
                            dynamic responseUnitName = await UnitsOfMeasureAndWeightController.getNameOfAndUnitMeasureWeightLAN(movementsListTemp[i].nonConvertibleUnitId);
                            if (responseUnitName.value == 1)
                                unitName = responseUnitName.name;
                        }
                        else unitName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(movementsListTemp[i].nonConvertibleUnitId);
                        dataGridMovs.Rows[n].Cells[4].Value = movementsListTemp[i].nonConvertibleUnits+" "+unitName;
                        dataGridMovs.Columns[5].Visible = false;
                        dataGridMovs.Columns[6].Visible = false;
                        dataGridMovs.Columns[7].Visible = false;
                    } else
                    {
                        double subtotal = (movementsListTemp[i].total + movementsListTemp[i].descuentoImporte);
                        dataGridMovs.Rows[n].Cells[4].Value = movementsListTemp[i].price.ToString("C", CultureInfo.CurrentCulture)+" MXN";
                        dataGridMovs.Rows[n].Cells[5].Value = subtotal.ToString("C", CultureInfo.CurrentCulture)+" MXN";
                        dataGridMovs.Rows[n].Cells[6].Value = movementsListTemp[i].descuentoImporte.ToString("C", CultureInfo.CurrentCulture)+" MXN";
                        dataGridMovs.Rows[n].Cells[7].Value = movementsListTemp[i].total.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                    }
                    dataGridMovs.Rows[n].Cells[8].Value = movementsListTemp[i].observations;
                }
                dataGridMovs.PerformLayout();
                movementsListTemp.Clear();
                if (movementsList.Count > 0)
                    lastId = Convert.ToInt32(movementsList[movementsList.Count - 1].id);
                imgSinDatos.Visible = false;
                //dataGridItems.Focus();
            }
            else
            {
                if (progress == 0)
                    imgSinDatos.Visible = true;
            }
            textTotalMovmientos.Text = "Movimientos: " + totalMovimientos.ToString().Trim();
            //reset displayed row
            if (firstVisibleRow > -1)
            {
                showScrollBars();
                if (movementsList.Count > 0)
                {
                    dataGridMovs.FirstDisplayedScrollingRowIndex = firstVisibleRow;
                    imgSinDatos.Visible = false;
                }
            }
        }

        private void showScrollBars()
        {
            dataGridMovs.ScrollBars = gridScrollBars;
        }

        private void dataGridMovs_Scroll(object sender, ScrollEventArgs e)
        {
            if (movementsList.Count < totalMovimientos && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
                {
                    if (e.NewValue > dataGridMovs.Rows.Count - getDisplayedRowsCount())
                    {
                        //prevent loading from autoscroll.
                        TimeSpan ts = DateTime.Now - lastLoading;
                        if (ts.TotalMilliseconds > 100)
                        {
                            firstVisibleRow = e.NewValue;
                            //dataGridItems.PerformLayout();
                            if (movementsType == TIPO_MOVDOC)
                                fillDataGridMovements();
                            else if (movementsType == TIPOMOVPED)
                                fillDataGridPedidos();
                        }
                        else
                        {
                            dataGridMovs.FirstDisplayedScrollingRowIndex = e.OldValue;
                        }
                    }
                }
            }
        }

        private int getDisplayedRowsCount()
        {
            int count = dataGridMovs.Rows[dataGridMovs.FirstDisplayedScrollingRowIndex].Height;
            count = dataGridMovs.Height / count;
            return count;
        }

        private async Task<List<MovimientosModel>> getAllMovementsFromADocument()
        {
            List<MovimientosModel> movementsList = null;
            await Task.Run(async () =>
            {
                if (queryType == 0)
                {
                    query = "SELECT * FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " + LocalDatabase.CAMPO_DOCUMENTOID_MOV +
                    " = " + idDocument+" AND "+LocalDatabase.CAMPO_ID_MOV+" > "+lastId+" ORDER BY "+LocalDatabase.CAMPO_ID_MOV+" ASC LIMIT "+LIMIT;
                    queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " + LocalDatabase.CAMPO_DOCUMENTOID_MOV +
                    " = " + idDocument;
                    movementsList = MovimientosModel.getAllMovements(query);
                    totalMovimientos = MovimientosModel.getIntValue(queryTotals);
                }                
            });
            return movementsList;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textTotalMovmientos_Click(object sender, EventArgs e)
        {

        }

        private void btnFactura_Click(object sender, EventArgs e)
        {
            validateDownloadInvoice();
        }

        private async Task validateDownloadInvoice()
        {
            String query = "SELECT " + LocalDatabase.CAMPO_IDWEBSERVICE_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                LocalDatabase.CAMPO_ID_DOC + " = " + idDocument;
            int idDocumentoServer = DocumentModel.getIntValue(query);
            if (idDocumentoServer != 0)
            {
                /* Obtener datos de la ruta del directorio */
                FormFacturar frmFacturar = new FormFacturar(idDocument, idDocumentoServer);
                frmFacturar.StartPosition = FormStartPosition.CenterScreen;
                frmFacturar.ShowDialog();
                //await UploadAndDownloadFilesController.getInvoiceFile("XAXX010101000FFCRTF020000000001");
            }
            else
            {
                FormMessage formMessage = new FormMessage("Documento No Enviado","No pudimos obtener la información del documento en el servidor\r\n" +
                    "Asegurate de que este documento se encuentre sincronizado",3);
                formMessage.ShowDialog();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (permissionPrepedido)
            {
                bool documentoPendienteDeDescatarar = DocumentModel.isItDocumentPrepedidoSendedToTheCustomerAndPendienteDeDestarar(idDocument);
                if (documentoPendienteDeDescatarar)
                {
                    FrmConfirmation fc = new FrmConfirmation("Cancelar Documento Entregado", "¿Estás seguro de que deseas eliminar el documento entregado al cliente?");
                    fc.StartPosition = FormStartPosition.CenterScreen;
                    fc.ShowDialog();
                    if (FrmConfirmation.confirmation)
                    {
                        if (ClsPositionsModel.cancelPositionForADocument(idDocument))
                            cancelarUnDocumento(idDocument, 1);
                        else cancelarUnDocumento(idDocument, 1);
                    }
                }
                else
                {
                    String query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                        LocalDatabase.CAMPO_ID_DOC + " = " + idDocument + " AND " + LocalDatabase.CAMPO_ENVIADOALWS_DOC + " = 1 AND " +
                        LocalDatabase.CAMPO_IDWEBSERVICE_DOC + " != 0";
                    int enviadoAlPanel = DocumentModel.getIntValue(query);
                    if (enviadoAlPanel > 0)
                    {
                        FrmConfirmation fc = new FrmConfirmation("Cancelar Documento", "Al eliminar un documento enviado al servidor tendrás que eliminarlo manualmente en el mismo y en comercial\n¿Estás seguro de eliminar un documento sincronizado al servidor?");
                        fc.StartPosition = FormStartPosition.CenterScreen;
                        fc.ShowDialog();
                        if (FrmConfirmation.confirmation)
                        {
                            if (ClsPositionsModel.cancelPositionForADocument(idDocument))
                                cancelarUnDocumento(idDocument, 1);
                            else cancelarUnDocumento(idDocument, 1);
                        }
                    }
                    else
                    {
                        FrmConfirmation fc = new FrmConfirmation("Cancelar Documento", "Desea cancelar el documento seleccionado?");
                        fc.StartPosition = FormStartPosition.CenterScreen;
                        fc.ShowDialog();
                        if (FrmConfirmation.confirmation)
                        {
                            if (ClsPositionsModel.cancelPositionForADocument(idDocument))
                                cancelarUnDocumento(idDocument, 1);
                            else cancelarUnDocumento(idDocument, 1);
                        }
                    }
                }
            }
            else
            {
                FrmConfirmation fc = new FrmConfirmation("Cancelar Documento", "Desea cancelar el documento seleccionado?");
                fc.StartPosition = FormStartPosition.CenterScreen;
                fc.ShowDialog();
                if (FrmConfirmation.confirmation)
                {
                    if (ClsPositionsModel.cancelPositionForADocument(idDocument))
                        cancelarUnDocumento(idDocument, 1);
                    else cancelarUnDocumento(idDocument, 1);
                }
            }
        }

        private void cancelarUnDocumento(int idDocumento, int llamada)
        {
            if (MovimientosModel.checkIfThereAreStillMovementsForTheDocumentInShift(idDocumento))
            {
                int tipoDoc = DocumentModel.getDocumentType(idDocumento);
                if (MovimientosModel.cancelMovementsOfDocuments(idDocumento, MovimientosModel.CALL_LOCAL_CANCELED, tipoDoc, serverModeLAN) > 0)
                {
                    int idpedido = DocumentModel.getCiddoctopedidoFromADocument(idDocumento);
                    if (idpedido > 0)
                    {
                        PedidosEncabezadoModel.marcarPedidoComoSurtidoONoSurtido(idpedido, 0);
                        PedidosEncabezadoModel.marcarPedidoComoListoONo(idpedido, 0);
                    }
                    if (DocumentModel.cancelADocument(idDocumento) > 0)
                    {
                        if (serverModeLAN)
                        {
                            this.Close();
                            if (frmResumenDocuments != null)
                            {
                                dvm = DocumentModel.getAllDataDocumento(idDocument);
                                frmResumenDocuments.updateRowDataGridView(dgvPosition, dgvPosition, dvm);
                            }
                        } else
                        {
                            if (ItemModel.quitarTodosLosArticulosDelCarrito() > 0)
                            {
                                this.Close();
                                if (frmResumenDocuments != null)
                                {
                                    dvm = DocumentModel.getAllDataDocumento(idDocument);
                                    frmResumenDocuments.updateRowDataGridView(dgvPosition, dgvPosition, dvm);
                                }
                            }
                            else
                            {
                                FormMessage fm = new FormMessage("Excepción", "Ocurrio un error al quitar los artículos del carrito!", 2);
                                fm.ShowDialog();
                            }
                        }
                    }
                    else
                    {
                        FormMessage fm = new FormMessage("Excepción", "Ocurrio un error al cancelar el documento Num: " + idDocumento, 2);
                        fm.ShowDialog();
                    }
                }
                else
                {
                    FormMessage fm = new FormMessage("Excepción", "Ocurrio un error al eliminar todos los movimientos!", 2);
                    fm.ShowDialog();
                }
            }
            else
            {
                int idpedido = DocumentModel.getCiddoctopedidoFromADocument(idDocumento);
                if (idpedido > 0)
                {
                    PedidosEncabezadoModel.marcarPedidoComoSurtidoONoSurtido(idpedido, 0);
                    PedidosEncabezadoModel.marcarPedidoComoListoONo(idpedido, 0);
                }
                if (DocumentModel.cancelADocument(idDocumento) > 0)
                {
                    if (serverModeLAN)
                    {
                        this.Close();
                        if (frmResumenDocuments != null)
                        {
                            dvm = DocumentModel.getAllDataDocumento(idDocument);
                            frmResumenDocuments.updateRowDataGridView(dgvPosition, dgvPosition, dvm);
                        }
                    } else
                    {
                        if (ItemModel.quitarTodosLosArticulosDelCarrito() > 0)
                        {
                            this.Close();
                            if (frmResumenDocuments != null)
                            {
                                dvm = DocumentModel.getAllDataDocumento(idDocument);
                                frmResumenDocuments.updateRowDataGridView(dgvPosition, dgvPosition, dvm);
                            }
                        }
                        else
                        {
                            FormMessage fm = new FormMessage("Excepción", "Ocurrio un error al quitar los artículos del carrito!", 2);
                            fm.ShowDialog();
                        }
                    }
                }
                else
                {
                    FormMessage fm = new FormMessage("Excepción", "Ocurrio un error al cancelar el documento Num: " + idDocumento, 2);
                    fm.ShowDialog();
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            FormVenta Venta = new FormVenta(idDocument, cotmosActive);
            Venta.ShowDialog();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            int documentType = DocumentModel.getDocumentType(idDocument);
            String textCopia = PrinterModel.getTextoCopia();
            clsTicket ticket = new clsTicket();
            ticket.CrearTicket(idDocument, false, permissionPrepedido, documentType, textCopia, serverModeLAN);
        }

        private void btnimprimirpuro_Click(object sender, EventArgs e)
        {
            string printer = "";
            PrinterModel pm = PrinterModel.getallDataFromAPrinter();
            if (pm != null)
            {
                printer = pm.nombre;
                String referencia = editFolioDocumento.Text;
                String auxLinea = DatosTicketModel.getticketrespaldo(referencia);
                auxLinea = auxLinea.Replace(">.<", "\u001b").Replace(">u<", "\u0001").Replace(">o<", "\0");
                RawPrinterHelper.SendStringToPrinter(printer, auxLinea, true);
            }
        }

        private async Task fillDataGridPedidos()
        {
            hideScrollBars();
            lastLoading = DateTime.Now;
            movementsCreditListTemp = await getAllMovementsFromAPedido();
            if (movementsCreditListTemp != null)
            {
                progress += movementsCreditListTemp.Count;
                movementsCreditList.AddRange(movementsCreditListTemp);
                if (movementsCreditList.Count > 0 && dataGridMovs.ColumnHeadersVisible == false)
                    dataGridMovs.ColumnHeadersVisible = true;
                for (int i = 0; i < movementsCreditListTemp.Count; i++)
                {
                    int n = dataGridMovs.Rows.Add();
                    dataGridMovs.Rows[n].Cells[0].Value = movementsCreditListTemp[i].id + "";
                    dataGridMovs.Columns["idMovDoc"].Visible = false;
                    if (movementsCreditListTemp[i].itemId != 0)
                    {
                        String itemName = ItemModel.getTheNameOfAnItem(movementsCreditListTemp[i].itemId);
                        if (itemName.Equals(""))
                        {
                            dynamic response = null;
                            if (serverModeLAN)
                            {
                                response = await ItemsController.getItemNameLAN(movementsCreditListTemp[i].itemId);
                            }
                            else
                            {
                                response = await ItemsController.getItemNameAPI(movementsCreditListTemp[i].itemId, codigoCaja);
                            }
                            if (response != null)
                            {
                                if (response.value == 1)
                                    itemName = response.name;
                            }
                        }
                        dataGridMovs.Rows[n].Cells[1].Value = itemName;
                    }
                    else dataGridMovs.Rows[n].Cells[1].Value = "Producto No Encontrado";
                    dataGridMovs.Columns[1].Width = 230;
                    if (movementsCreditListTemp[i].unidadCapturadaId != 0)
                    {
                        String unidadName = "";
                        if (serverModeLAN)
                        {
                            dynamic responseUnitName = await UnitsOfMeasureAndWeightController.getNameOfAndUnitMeasureWeightLAN(movementsCreditListTemp[i].unidadCapturadaId);
                            if (responseUnitName.value == 1)
                                unidadName = responseUnitName.name;
                        }
                        else unidadName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(movementsCreditListTemp[i].unidadCapturadaId);
                        dataGridMovs.Rows[n].Cells[3].Value = unidadName;
                    }
                    else
                    {
                        dataGridMovs.Rows[n].Cells[3].Value = "(Ninguna)";
                    }
                    if (UserModel.doYouHavePermissionPrepedido())
                    {
                        dataGridMovs.Rows[n].Cells[2].Value = "0";
                        dataGridMovs.Columns["capturedUnitsMovDoc"].HeaderText = "Peso";
                        String unidadNoConvertibleName = "(Ninguna)";
                        if (movementsCreditListTemp[i].unidadNoConvertibleId != 0)
                        {
                            if (serverModeLAN)
                            {
                                dynamic responseUnitName = await UnitsOfMeasureAndWeightController.getNameOfAndUnitMeasureWeightLAN(movementsCreditListTemp[i].unidadNoConvertibleId);
                                if (responseUnitName.value == 1)
                                    unidadNoConvertibleName = responseUnitName.name;
                            }
                            else unidadNoConvertibleName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(movementsCreditListTemp[i].unidadNoConvertibleId);
                        }
                        dataGridMovs.Rows[n].Cells[4].Value = movementsCreditListTemp[i].unidadesNoConvertibles + " " + unidadNoConvertibleName;
                        dataGridMovs.Columns["subtotalMovDoc"].Visible = false;
                        dataGridMovs.Columns["discountMovDoc"].Visible = false;
                        dataGridMovs.Columns["totalMovDoc"].Visible = false;
                        dataGridMovs.Rows[n].Cells[7].Value = "$" + movementsCreditListTemp[i].total + " MXN";
                    } else
                    {
                        dataGridMovs.Rows[n].Cells[2].Value = movementsCreditListTemp[i].unidadesCapturadas;
                        dataGridMovs.Rows[n].Cells[4].Value = movementsCreditListTemp[i].precio;
                        dataGridMovs.Rows[n].Cells[5].Value = movementsCreditListTemp[i].subtotal;
                        dataGridMovs.Rows[n].Cells[6].Value = movementsCreditListTemp[i].descuento;
                        dataGridMovs.Rows[n].Cells[7].Value = "$" + movementsCreditListTemp[i].total + " MXN";
                    }
                    dataGridMovs.Rows[n].Cells[8].Value = movementsCreditListTemp[i].observation;
                }
                dataGridMovs.PerformLayout();
                movementsCreditListTemp.Clear();
                lastId = Convert.ToInt32(movementsCreditList[movementsCreditList.Count - 1].id);
                imgSinDatos.Visible = false;
                //dataGridItems.Focus();
            }
            else
            {
                if (progress == 0)
                    imgSinDatos.Visible = true;
            }
            textTotalMovmientos.Text = "Movimientos: " + totalMovimientos.ToString().Trim();
            //reset displayed row
            if (firstVisibleRow > -1)
            {
                showScrollBars();
                if (movementsCreditList.Count > 0)
                {
                    dataGridMovs.FirstDisplayedScrollingRowIndex = firstVisibleRow;
                    imgSinDatos.Visible = false;
                }
            }
        }

        private async Task<List<PedidoDetalleModel>> getAllMovementsFromAPedido()
        {
            List<PedidoDetalleModel> movementsList = null;
            await Task.Run(async () =>
            {
                query = "SELECT * FROM "+LocalDatabase.TABLA_PEDIDODETALLE+" WHERE " + LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_PD +
                    " = " + idDocument+" AND "+LocalDatabase.CAMPO_ID_PD+" > "+lastId+" ORDER BY "+LocalDatabase.CAMPO_ID_PD+" ASC LIMIT "+LIMIT;
                queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_PEDIDODETALLE + " WHERE " + LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_PD +
                    " = " + idDocument;
                movementsList = PedidoDetalleModel.getAllMovements(query);
                totalMovimientos = PedidoDetalleModel.getIntValue(queryTotals);
            });
            return movementsList;
        }

        private void picCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridMovs_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void resetearValores()
        {

        }
    }
}
