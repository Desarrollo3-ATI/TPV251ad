using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncTPV
{
    public partial class FrmRecuperarDocumento : Form
    {
        int idCustomer = 0;
        public int call = 0;
        private int LIMIT = 70;
        private int progress = 0;
        private int lastId = 0;
        private int totalItems = 0, queryType = 0;
        private String query = "", queryTotals = "", itemCodeOrName = "";
        private DateTime lastLoading;
        private int firstVisibleRow;
        private ScrollBars gridScrollBars;
        private List<DocumentModel> documentsList;
        private List<DocumentModel> documentsListTemp;
        private bool permissionPrepedido = false;

        public FrmRecuperarDocumento(int idCustomer)
        {
            this.idCustomer = idCustomer;
            InitializeComponent();
            //panel1.BackColor = Color.FromArgb(GeneralTxt.Red, GeneralTxt.Green, GeneralTxt.Blue);
            //dtGridDocumentos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(GeneralTxt.Red, GeneralTxt.Green, GeneralTxt.Blue);
            //dtGridDocumentos.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(GeneralTxt.RedSelectionRows, GeneralTxt.GreenSelectionRows, GeneralTxt.BlueSelectionRows);
            documentsList = new List<DocumentModel>();
            btnCerrar.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.close, 40, 40);
            permissionPrepedido = UserModel.doYouHavePermissionPrepedido();
        }

        private void frmAbrirDoc_Load(object sender, EventArgs e)
        {
            this.Text = "Busqueda por cliente seleccionado";
            validarPermisoPrepedido(0);
            fillDataGrid();
        }

        private void validarPermisoPrepedido(int todos)
        {
            if (permissionPrepedido) {
                String query = "SELECT " + LocalDatabase.CAMPO_NOMBRECLIENTE + " FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " = " +
                idCustomer;
                if (todos == 1)
                    textClienteFrmRecuperarDocumento.Text = "Todos los Documentos por Destarar o Pesar";
                else textClienteFrmRecuperarDocumento.Text = "Documentos por destarar o pesar del cliente: " + CustomerModel.getAStringValueFromACustomer(query);
                dtGridDocumentos.RowTemplate.DefaultCellStyle.Padding = new Padding(15, 15, 15, 15);
                dtGridDocumentos.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            } else {
                String query = "SELECT " + LocalDatabase.CAMPO_NOMBRECLIENTE + " FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " = " +
                idCustomer;
                if (todos == 1)
                    textClienteFrmRecuperarDocumento.Text = "Todos los Documentos Pendientes";
                else textClienteFrmRecuperarDocumento.Text = "Documentos pendientes del cliente: " + CustomerModel.getAStringValueFromACustomer(query);
            }
        }

        private void hideScrollBars()
        {
            imgSinDatosFrmRecuperarDocumentos.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.logosynctpvmoving, 300, 300);
            imgSinDatosFrmRecuperarDocumentos.Visible = true;
            gridScrollBars = dtGridDocumentos.ScrollBars;
            //dataGridItems.ScrollBars = ScrollBars.None;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async Task fillDataGrid()
        {
            hideScrollBars();
            lastLoading = DateTime.Now;
            documentsListTemp = await getAllDocuments();
            if (documentsListTemp != null) {
                progress += documentsListTemp.Count;
                documentsList.AddRange(documentsListTemp);
                if (documentsList.Count > 0 && dtGridDocumentos.ColumnHeadersVisible == false)
                    dtGridDocumentos.ColumnHeadersVisible = true;
                for (int i = 0; i < documentsListTemp.Count; i++)
                {
                    int n = dtGridDocumentos.Rows.Add();
                    dtGridDocumentos.Rows[n].Cells[0].Value = documentsListTemp[i].id;
                    if (documentsListTemp[i].tipo_documento == DocumentModel.TIPO_VENTA ||
                        documentsListTemp[i].tipo_documento == DocumentModel.TIPO_REMISION) {
                        dtGridDocumentos.Rows[n].Cells[1].Value = "Venta";
                    } else if (documentsListTemp[i].tipo_documento == DocumentModel.TIPO_COTIZACION) {
                        dtGridDocumentos.Rows[n].Cells[1].Value = "Cotización";
                    } else if (documentsListTemp[i].tipo_documento == DocumentModel.TIPO_PEDIDO) {
                        dtGridDocumentos.Rows[n].Cells[1].Value = "Pedido";
                    } else if (documentsListTemp[i].tipo_documento == DocumentModel.TIPO_DEVOLUCION)
                    {
                        dtGridDocumentos.Rows[n].Cells[1].Value = "Devolución";
                    } else if (documentsListTemp[i].tipo_documento == DocumentModel.TIPO_COTIZACION_MOSTRADOR)
                    {
                        dtGridDocumentos.Rows[n].Cells[1].Value = "Cotización de Mostrador";
                    }
                    dtGridDocumentos.Columns["TipoDocumento"].Width = 100;
                    if (documentsListTemp[i].cliente_id != 0)
                    {
                        String customerName = CustomerModel.getName(documentsListTemp[i].cliente_id);
                        dtGridDocumentos.Rows[n].Cells[2].Value = customerName;
                    } else
                    {
                        dtGridDocumentos.Rows[n].Cells[2].Value = documentsListTemp[i].cliente_id;
                    }
                    dtGridDocumentos.Columns["clienteDocument"].Width = 200;
                    dtGridDocumentos.Rows[n].Cells[3].Value = documentsListTemp[i].fventa;
                    dtGridDocumentos.Columns["Folio"].Width = 120;
                    dtGridDocumentos.Rows[n].Cells[4].Value = documentsListTemp[i].fechahoramov;
                    if (permissionPrepedido) {
                        dtGridDocumentos.Rows[n].Cells[1].Value = "Entrega";
                        dtGridDocumentos.Columns[5].HeaderText = "Total de Pollos";
                        if (documentsListTemp[i].ciddoctopedidocc != 0)
                        {
                            String queryPE = "SELECT * FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " WHERE " +
                                LocalDatabase.CAMPO_DOCUMENTOID_PE +" = " + documentsListTemp[i].ciddoctopedidocc + " AND " + 
                            LocalDatabase.CAMPO_TYPE_PE + " = " + PedidosEncabezadoModel.TYPE_PREPEDIDOS;
                            PedidosEncabezadoModel pem = PedidosEncabezadoModel.getAPedCot(queryPE);
                            if (pem != null)
                            {
                                queryPE = "SELECT * FROM " + LocalDatabase.TABLA_PEDIDODETALLE + " WHERE " + 
                                    LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_PD + " = " + pem.idDocumento;
                                List<PedidoDetalleModel> pdm = PedidoDetalleModel.getAllMovements(queryPE);
                                if (pdm != null)
                                {
                                    String unitName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(pdm[0].unidadNoConvertibleId);
                                    dtGridDocumentos.Rows[n].Cells[5].Value = pdm[0].unidadesNoConvertibles + " "+ unitName;
                                }
                                query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_PESO + " P " +
                                    "INNER JOIN " + LocalDatabase.TABLA_MOVIMIENTO + " M ON P." + LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = M." +
                                    LocalDatabase.CAMPO_ID_MOV + " INNER JOIN " + LocalDatabase.TABLA_DOCUMENTOVENTA + " D ON M." +
                                    LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = D." + LocalDatabase.CAMPO_ID_DOC + " WHERE " +
                                    "D." + LocalDatabase.CAMPO_ID_DOC + " = " +
                                    documentsListTemp[i].id + " AND (P." + LocalDatabase.CAMPO_PESOCAJA_PESO + " = 0)";
                                //WeightModel wm = WeightModel.getAWeight(query);
                                int pesoTaraNoCapturado = WeightModel.getIntValue(query);
                                if (pesoTaraNoCapturado == 1)
                                {
                                    dtGridDocumentos.Rows[n].Cells[6].Value = "Destarar";
                                } else
                                {
                                    dtGridDocumentos.Rows[n].Cells[6].Value = "Pendiente";
                                }
                            }
                        }
                    } else
                    {
                        dtGridDocumentos.Rows[n].Cells[5].Value = "$ " + MetodosGenerales.obtieneDosDecimales(documentsListTemp[i].total) + " MXN";
                        dtGridDocumentos.Rows[n].Cells[6].Value = "Pendiente";
                    }
                }
                dtGridDocumentos.PerformLayout();
                documentsListTemp.Clear();
                lastId = Convert.ToInt32(documentsList[documentsList.Count - 1].id);
                imgSinDatosFrmRecuperarDocumentos.Visible = false;
            }
            else
            {
                if (progress == 0)
                {
                    imgSinDatosFrmRecuperarDocumentos.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.sindatos, 300, 300);
                    imgSinDatosFrmRecuperarDocumentos.Visible = true;
                }
            }
            textTotalRecords.Text = "Documentos: " + totalItems.ToString().Trim();
            //reset displayed row
            if (firstVisibleRow > -1)
            {
                dtGridDocumentos.ScrollBars = gridScrollBars;
                if (documentsList.Count > 0) {
                    dtGridDocumentos.FirstDisplayedScrollingRowIndex = firstVisibleRow;
                    imgSinDatosFrmRecuperarDocumentos.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.sindatos, 300, 300);
                    imgSinDatosFrmRecuperarDocumentos.Visible = false;
                }
            }
        }

        private async Task<List<DocumentModel>> getAllDocuments()
        {
            List<DocumentModel> documentsList = null;
            await Task.Run(() => {
                if (queryType == 0 && idCustomer != 0)
                {
                    query = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                    LocalDatabase.CAMPO_PAUSAR_DOC + " = " + 1 + " AND " + LocalDatabase.CAMPO_CLIENTEID_DOC + " = " + idCustomer + " AND " +
                    LocalDatabase.CAMPO_CANCELADO_DOC + " = " + 0+" ORDER BY "+LocalDatabase.CAMPO_ID_DOC+" ASC LIMIT "+LIMIT;
                    queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                    LocalDatabase.CAMPO_PAUSAR_DOC + " = " + 1 + " AND " + LocalDatabase.CAMPO_CLIENTEID_DOC + " = " + idCustomer + " AND " +
                    LocalDatabase.CAMPO_CANCELADO_DOC + " = " + 0;
                } else {
                    query = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                    LocalDatabase.CAMPO_PAUSAR_DOC + " = " + 1 + " AND " + LocalDatabase.CAMPO_CANCELADO_DOC + " = " + 0+
                    " ORDER BY " + LocalDatabase.CAMPO_ID_DOC + " ASC LIMIT " + LIMIT;
                    queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                    LocalDatabase.CAMPO_PAUSAR_DOC + " = " + 1 + " AND " + LocalDatabase.CAMPO_CANCELADO_DOC + " = " + 0;
                }
                documentsList = DocumentModel.getAllDocuments(query);
                totalItems = DocumentModel.getIntValue(queryTotals);
            });
            return documentsList;
        }

        private void checkBoxMostrarTodos_CheckedChanged(object sender, EventArgs e) {
            if (checkBoxMostrarTodos.Checked) {
                queryType = 1;
                resetearValores(queryType);
                this.Text = "Busqueda por todos los documentos por destarar";
                validarPermisoPrepedido(queryType);
                fillDataGrid();
            } else
            {
                queryType = 0;
                resetearValores(queryType);
                this.Text = "Busqueda por cliente seleccionado";
                validarPermisoPrepedido(queryType);
                fillDataGrid();
            }
        }

        private void dtGridDocumentos_Scroll(object sender, ScrollEventArgs e)
        {
            if (documentsList.Count < totalItems)
            {
                if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
                {
                    if (e.NewValue > dtGridDocumentos.Rows.Count - getDisplayedRowsCount())
                    {
                        //prevent loading from autoscroll.
                        TimeSpan ts = DateTime.Now - lastLoading;
                        if (ts.TotalMilliseconds > 100)
                        {
                            firstVisibleRow = e.NewValue;
                            //dataGridItems.PerformLayout();
                            fillDataGrid();
                        }
                        else
                        {
                            dtGridDocumentos.FirstDisplayedScrollingRowIndex = e.OldValue;
                        }
                    }
                }
            }
        }

        private int getDisplayedRowsCount()
        {
            int count = dtGridDocumentos.Rows[dtGridDocumentos.FirstDisplayedScrollingRowIndex].Height;
            count = dtGridDocumentos.Height / count;
            return count;
        }

        private void dtGridDocumentos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int idDocument = Convert.ToInt32(dtGridDocumentos.CurrentRow.Cells[0].Value.ToString());
                String query = "SELECT " + LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                    LocalDatabase.CAMPO_ID_DOC + " = " + idDocument;
                int idPedido = DocumentModel.getIntValue(query);
                if (idPedido != 0)
                {
                    query = "SELECT " + LocalDatabase.CAMPO_TYPE_PE + " FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " WHERE " +
                        LocalDatabase.CAMPO_DOCUMENTOID_PE + " = " + idPedido;
                    int tipoPedido = PedidosEncabezadoModel.getIntValue(query);
                }
                FormVenta.idDocument = idDocument;
                this.Close();
            }
        }

        public void resetearValores(int queryType)
        {
            this.queryType = queryType;
            query = "";
            queryTotals = "";
            totalItems = 0;
            lastId = 0;
            progress = 0;
            documentsList.Clear();
            dtGridDocumentos.Rows.Clear();
        }

    }
}
