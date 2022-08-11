using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using SyncTPV.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncTPV
{
    public partial class FrmResumenDocuments : Form
    {
        public int call = 0;
        private int LIMIT = 20;
        private int progress = 0;
        private int lastId = 0;
        private int totalItems = 0, queryType = 0;
        private String queryTemp = "", queryReporte = "", queryTotals = "", searchWord = "";
        private DateTime lastLoading;
        private int firstVisibleRow;
        private ScrollBars gridScrollBars;
        private List<DocumentModel> documentsList;
        private List<DocumentModel> documentsListTemp;
        private bool permissionPrepedido = false;
        private String startDate = "", endDate = "";
        private FormWaiting frmWaiting;
        private bool serverModeLAN = false;

        public FrmResumenDocuments()
        {
            InitializeComponent();
            lastId = DocumentModel.getLastId();
            serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
            documentsList = new List<DocumentModel>();
            permissionPrepedido = UserModel.doYouHavePermissionPrepedido();
            if (permissionPrepedido)
                LIMIT = 15;
            btnClose.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.back_white, 40, 40);
            btnCancelDocuments.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.delete_white, 40, 35);
            imgSinDatosFrmResumenDocumentos.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.sindatos, 254, 254);
        }

        private void picCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmResumenDoc_Load(object sender, EventArgs e)
        {
            fillComboStatusDocuments();
            //getDocumentsByFilters();
            fillDataGrid();
        }

        private async Task fillComboStatusDocuments()
        {
            if (permissionPrepedido)
            {
                cmbDocumentosMostrar.Items.Add("Cancelados");
                cmbDocumentosMostrar.Items.Add("Entregados Por Destarar");
                cmbDocumentosMostrar.Items.Add("Entregados Destarados");
                cmbDocumentosMostrar.Items.Add("Todos");
                dataGridDoc.RowTemplate.DefaultCellStyle.Padding = new Padding(2, 15, 2, 15);
                dataGridDoc.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                cmbDocumentosMostrar.Font = new Font(cmbDocumentosMostrar.Font.FontFamily, 20);
                cmbDocumentosMostrar.Location = new Point(cmbDocumentosMostrar.Location.X, cmbDocumentosMostrar.Location.Y - 5);
                cmbDocumentosMostrar.Width = 300;
                startDate = (dateTimePickerStart.Value).ToString("yyyy-MM-dd");
                endDate = (dateTimePickerEnd.Value).ToString("yyyy-MM-dd");
                startDate += " 00:00:00";
                endDate += " 23:59:59";
                lblDocumentosMostrar.Location = new Point(lblDocumentosMostrar.Location.X -30, lblDocumentosMostrar.Location.Y);
                cmbDocumentosMostrar.Location = new Point(cmbDocumentosMostrar.Location.X - 30, cmbDocumentosMostrar.Location.Y);
            } else
            {
                panelFiltros.Height = 0;
                tabControlResumenDocumentos.Location = new Point(tabControlResumenDocumentos.Location.X, panelFiltros.Location.Y);
                tabControlResumenDocumentos.Height = tabControlResumenDocumentos.Height + 76;
                cmbDocumentosMostrar.Items.Add("Cancelados");
                cmbDocumentosMostrar.Items.Add("Enviados");
                cmbDocumentosMostrar.Items.Add("Pausados");
                cmbDocumentosMostrar.Items.Add("Todos");
                //dataGridDoc.RowTemplate.DefaultCellStyle.Padding = new Padding(5, 5, 5, 5);
                dataGridDoc.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }
            cmbDocumentosMostrar.SelectedIndex = cmbDocumentosMostrar.Items.Count - 1;
        }

        private void hideScrollBars()
        {
            imgSinDatosFrmResumenDocumentos.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.logosynctpvmoving, 254, 254);
            imgSinDatosFrmResumenDocumentos.Visible = true;
            gridScrollBars = dataGridDoc.ScrollBars;
            //dataGridItems.ScrollBars = ScrollBars.None;
        }

        public async Task fillDataGrid()
        {
            hideScrollBars();
            lastLoading = DateTime.Now;
            documentsListTemp = await getAllDocuments();
            if (documentsListTemp != null)
            {
                progress += documentsListTemp.Count;
                documentsList.AddRange(documentsListTemp);
                if (documentsList.Count > 0 && dataGridDoc.ColumnHeadersVisible == false)
                    dataGridDoc.ColumnHeadersVisible = true;
                for (int i = 0; i < documentsListTemp.Count; i++)
                {
                    int n = dataGridDoc.Rows.Add();
                    if (permissionPrepedido)
                        dataGridDoc.Rows[n].Height = 70;
                    dataGridDoc.Rows[n].Cells[0].Value = "false";
                    if (permissionPrepedido)
                        dataGridDoc.Columns["colCheck"].Width = 50;
                    else dataGridDoc.Columns["colCheck"].Width = 20;
                    dataGridDoc.Rows[n].Cells[1].Value = documentsListTemp[i].id;
                    dataGridDoc.Columns["id"].Visible = false;
                    String documentTypeText = await getDocumentTypeText(documentsListTemp[i].tipo_documento);
                    dataGridDoc.Rows[n].Cells[2].Value = documentTypeText;
                    if (permissionPrepedido)
                        dataGridDoc.Columns["Tipo"].Width = 80;
                    if (documentsListTemp[i].cliente_id < 0)
                    {
                        String customerName = "";
                        if (CustomerADCModel.isTheCustomerSendedByIdPanel(Math.Abs(documentsListTemp[i].cliente_id)))
                        {
                            customerName = CustomerADCModel.getNombreWithPanelId(Math.Abs(documentsListTemp[i].cliente_id));
                        } else
                        {
                            customerName = CustomerADCModel.getNombreWithLocalId(Math.Abs(documentsListTemp[i].cliente_id));
                        }
                        if (customerName.Equals(""))
                            dataGridDoc.Rows[n].Cells[3].Value = "Cliente Nuevo (Ver Detalles doble clic)";
                        else dataGridDoc.Rows[n].Cells[3].Value = customerName;
                        dataGridDoc.Columns["Cliente"].Width = 300;
                    } else
                    {
                        String customerName = CustomerModel.getName(documentsListTemp[i].cliente_id);
                        if (customerName.Equals(""))
                            dataGridDoc.Rows[n].Cells[3].Value = documentsListTemp[i].clave_cliente;
                        else dataGridDoc.Rows[n].Cells[3].Value = customerName;
                        dataGridDoc.Columns["Cliente"].Width = 300;
                    }
                    dataGridDoc.Rows[n].Cells[4].Value = documentsListTemp[i].nombreu;
                    dataGridDoc.Columns["nombre"].Width = 200;
                    dataGridDoc.Rows[n].Cells[5].Value = documentsListTemp[i].fventa;
                    dataGridDoc.Columns["fventa"].Width = 120;
                    if (permissionPrepedido)
                    {
                        if (documentsListTemp[i].fechahoramov.Equals(""))
                        {
                            String queryMov = "SELECT * FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " + LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " +
                                documentsListTemp[i].id;
                            MovimientosModel mm = MovimientosModel.getAMovement(queryMov);
                            if (mm != null)
                            {
                                String queryPeso = "SELECT * FROM " + LocalDatabase.TABLA_PESO + " WHERE " + LocalDatabase.CAMPO_MOVIMIENTOID_PESO +
                                    " = " + mm.id;
                                WeightModel wm = WeightModel.getAWeight(queryPeso);
                                if (wm != null)
                                {
                                    if (wm.pesoCaja == 0)
                                        dataGridDoc.Rows[n].Cells[6].Value = "Documento Por Destarar";
                                    else dataGridDoc.Rows[n].Cells[6].Value = "Documento Destarado";
                                }
                                else dataGridDoc.Rows[n].Cells[6].Value = "Documento Sin Peso Bruto";
                            }
                            else dataGridDoc.Rows[n].Cells[6].Value = "Documento Incompleto";
                        }
                        else dataGridDoc.Rows[n].Cells[6].Value = documentsListTemp[i].fechahoramov;
                    } else {
                        if (documentsListTemp[i].fechahoramov.Equals(""))
                            dataGridDoc.Rows[n].Cells[6].Value = "Documento Incompleto";
                        else dataGridDoc.Rows[n].Cells[6].Value = documentsListTemp[i].fechahoramov;
                    }
                    dataGridDoc.Columns["fechaHora"].Width = 100;
                    if (permissionPrepedido)
                    {
                        dataGridDoc.Rows[n].Cells[7].Value = documentsListTemp[i].total.ToString("C",CultureInfo.CurrentCulture) + " MXN";
                        dataGridDoc.Columns[7].Visible = false;
                    }
                    else
                    {
                        dataGridDoc.Rows[n].Cells[7].Value = documentsListTemp[i].total.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                    }
                    if (documentsListTemp[i].enviadoAlWs == 1 && documentsListTemp[i].idWebService != 0 && documentsListTemp[i].estado == 0) {
                        dataGridDoc.Rows[n].Cells[8].Value = MetodosGenerales.redimencionarBitmap(Properties.Resources.cloud_online_black, 25, 25);
                        dataGridDoc[0, i].ReadOnly = true; //no permitir la selección
                    } else {
                        if (documentsListTemp[i].enviadoAlWs == 0 && documentsListTemp[i].estado == 0 && documentsListTemp[i].pausado == 0)
                        {
                            dataGridDoc.Rows[n].Cells[8].Value = MetodosGenerales.redimencionarBitmap(Properties.Resources.cloud_offline_black, 25, 25);
                        }
                        else if ((documentsListTemp[i].pausado == 1 && documentsListTemp[i].estado == 1) || 
                            (documentsListTemp[i].pausado == 0 && documentsListTemp[i].estado == 1)) {
                            dataGridDoc.Rows[n].Cells[8].Value = MetodosGenerales.redimencionarBitmap(Properties.Resources.cancel_black, 25, 25);
                            dataGridDoc[0, i].ReadOnly = true; //no permitir la selección
                            dataGridDoc.Rows[n].DefaultCellStyle.BackColor = Color.FromArgb(235, 89, 89);
                        }
                        else if (documentsListTemp[i].pausado == 1 && documentsListTemp[i].estado == 0)
                        {
                            if (permissionPrepedido)
                            {
                                bool enviado = DocumentModel.isItDocumentPrepedidoSendedToTheCustomer(documentsListTemp[i].id);
                                bool pesoBrutoCapturado = DocumentModel.pesoBrutoCapturado(documentsListTemp[i].id);
                                if (enviado && pesoBrutoCapturado)
                                {
                                    dataGridDoc.Rows[n].Cells[8].Value = MetodosGenerales.redimencionarBitmap(Properties.Resources.entregar_back, 25, 25);
                                } else
                                {
                                    dataGridDoc.Rows[n].Cells[8].Value = MetodosGenerales.redimencionarBitmap(Properties.Resources.pausa_black, 25, 25);
                                    dataGridDoc.Rows[n].DefaultCellStyle.BackColor = Color.FromArgb(249, 168, 37);
                                }
                            } else
                            {
                                dataGridDoc.Rows[n].Cells[8].Value = MetodosGenerales.redimencionarBitmap(Properties.Resources.pausa_black, 25, 25);
                                dataGridDoc.Rows[n].DefaultCellStyle.BackColor = Color.FromArgb(249, 168, 37);
                            }
                        }
                    }
                    dataGridDoc.Columns["status"].Width = 55;
                }
                dataGridDoc.PerformLayout();
                documentsListTemp.Clear();
                if (documentsList.Count > 0)
                    lastId = Convert.ToInt32(documentsList[documentsList.Count - 1].id);
                imgSinDatosFrmResumenDocumentos.Visible = false;
            }
            else
            {
                if (progress == 0)
                {
                    imgSinDatosFrmResumenDocumentos.Visible = true;
                    imgSinDatosFrmResumenDocumentos.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.sindatos, 254, 254);
                }
            }
            textTotalDocumentos.Text = "Documentos: " + totalItems.ToString().Trim();
            if (firstVisibleRow > -1)
            {
                dataGridDoc.ScrollBars = gridScrollBars;
                if (documentsList.Count > 0)
                {
                    dataGridDoc.FirstDisplayedScrollingRowIndex = firstVisibleRow;
                    imgSinDatosFrmResumenDocumentos.Visible = false;
                }
            }
        }

        private async Task<String> getDocumentTypeText(int documentType)
        {
            String typeName = "";
            await Task.Run(async () =>
            {
                if (documentType == 1)
                    typeName = "Cotización";
                else if (documentType == 2)
                    typeName = "Venta";
                else if (documentType == 3)
                    typeName = "Pedido";
                else if (documentType == 4)
                    typeName = "Venta";
                else if (documentType == 5)
                    typeName = "Devolución";
                else if (documentType == 51)
                    typeName = "Cotización de Mostrador";
                if (permissionPrepedido)
                {
                    if (documentType == DocumentModel.TIPO_PREPEDIDO)
                        typeName = "Prepedido";
                    else typeName = "Entregado";
                }
                else
                {
                    if (documentType == DocumentModel.TIPO_PREPEDIDO)
                        typeName = "Prepedido";
                }
            });
            return typeName;
        }

        private async Task<List<DocumentModel>> getAllDocuments()
        {
            List<DocumentModel> documentList = null;
            await Task.Run(async () =>
            {
                if (queryType == 0)
                {
                    if (permissionPrepedido)
                    {
                        queryTemp = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " + LocalDatabase.CAMPO_ID_DOC + " != 0 AND " +
                    LocalDatabase.CAMPO_ID_DOC + " <= " + lastId + " AND "+LocalDatabase.CAMPO_FECHAHORAMOV_DOC+" BETWEEN @startDate AND @endDate " +
                    "ORDER BY " + LocalDatabase.CAMPO_ID_DOC + " DESC LIMIT " + LIMIT;
                        queryReporte = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " + LocalDatabase.CAMPO_ID_DOC + " != 0" +
                        " AND " + LocalDatabase.CAMPO_FECHAHORAMOV_DOC + " BETWEEN @startDate AND @endDate";
                        queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA+ " WHERE "+ 
                        LocalDatabase.CAMPO_FECHAHORAMOV_DOC + " BETWEEN @startDate AND @endDate";
                        documentList = DocumentModel.getAllDocumentsWithParamtersDates(queryTemp, "startDate", startDate, "endDate", endDate);
                        totalItems = DocumentModel.getIntValueWithParametersDates(queryTotals, "startDate", startDate, "endDate", endDate);
                    } else
                    {
                        queryTemp = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " + LocalDatabase.CAMPO_ID_DOC + " != 0 AND " +
                    LocalDatabase.CAMPO_ID_DOC + " <= " + lastId + " ORDER BY " + LocalDatabase.CAMPO_ID_DOC + " DESC LIMIT " + LIMIT;
                        queryReporte = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " + LocalDatabase.CAMPO_ID_DOC + " != 0" +
                        " ORDER BY " + LocalDatabase.CAMPO_ID_DOC + " DESC";
                        queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA;
                        documentList = DocumentModel.getAllDocuments(queryTemp);
                        totalItems = DocumentModel.getIntValue(queryTotals);
                    }
                } else if (queryType == 1)
                {
                    if (permissionPrepedido)
                    {
                        queryTemp = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                        LocalDatabase.CAMPO_CANCELADO_DOC + " = 1 AND " + LocalDatabase.CAMPO_ID_DOC + " != 0 AND " +
                        LocalDatabase.CAMPO_ID_DOC + " <= " + lastId + " AND "+LocalDatabase.CAMPO_FECHAHORAMOV_DOC+" BETWEEN @startDate AND " +
                        "@endDate ORDER BY " + LocalDatabase.CAMPO_ID_DOC + " DESC LIMIT " + LIMIT;
                        queryReporte = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                        LocalDatabase.CAMPO_CANCELADO_DOC + " = 1 AND " + LocalDatabase.CAMPO_ID_DOC + " != 0" +
                        " AND "+LocalDatabase.CAMPO_FECHAHORAMOV_DOC+" BETWEEN @startDate AND @endDate";
                        queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                            LocalDatabase.CAMPO_CANCELADO_DOC + " = 1 AND "+LocalDatabase.CAMPO_FECHAHORAMOV_DOC+" BETWEEN @startDate AND @endDate";
                        documentList = DocumentModel.getAllDocumentsWithParamtersDates(queryTemp, "startDate", startDate, "endDate", endDate);
                        totalItems = DocumentModel.getIntValueWithParametersDates(queryTotals, "startDate", startDate, "endDate", endDate);
                    } else
                    {
                        queryTemp = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                        LocalDatabase.CAMPO_CANCELADO_DOC + " = 1 AND " + LocalDatabase.CAMPO_ID_DOC + " != 0 AND " +
                        LocalDatabase.CAMPO_ID_DOC + " <= " + lastId + " ORDER BY " + LocalDatabase.CAMPO_ID_DOC + " DESC LIMIT " + LIMIT;
                        queryReporte = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                        LocalDatabase.CAMPO_CANCELADO_DOC + " = 1 AND " + LocalDatabase.CAMPO_ID_DOC + " != 0" +
                        " ORDER BY " + LocalDatabase.CAMPO_ID_DOC + " DESC";
                        queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                            LocalDatabase.CAMPO_CANCELADO_DOC + " = 1";
                        documentList = DocumentModel.getAllDocuments(queryTemp);
                        totalItems = DocumentModel.getIntValue(queryTotals);
                    }
                } else if (queryType == 2)
                {
                    /* Por Destarar */
                    if (permissionPrepedido)
                    {
                        queryTemp = "SELECT D.id, D.clave_cliente, D.cliente_id, D.descuento, D.total, D.NOMBREU, D.ALMACEN_ID, D.ANTICIPO, " +
                        "D.TIPO_DOCUMENTO, D." +LocalDatabase.CAMPO_FORMACOBROID_DOC+", D."+LocalDatabase.CAMPO_FACTURA_DOC+", "+
                        "D.FVENTA, D.FECHAHORAMOV, D."+LocalDatabase.CAMPO_FORMACOBROIDABONO_DOC+", " +
                        "D."+LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_DOC+", D.cancelado, D.enviado_al_ws, D.id_web_service, D.archivado, D.pausa, " +
                        "D."+LocalDatabase.CAMPO_PAPELERARECICLAJE_DOC+" FROM Documentos D " +
                        "INNER JOIN PedidoEncabezado P ON D.CIDDOCTOPEDIDOCC = P.CIDDOCTOPEDIDOCC " +
                        "INNER JOIN Movimientos M ON D.id = M.DOCTO_ID_PEDIDO " +
                        "INNER JOIN Weight W ON M.id = W.movementId " +
                        "WHERE D.pausa = 1 AND P.type = 4 AND P.surtido = 1 AND P.listo = 1 " +
                        "AND W.box_weight = 0 AND W.net_weight != 0 AND D."+LocalDatabase.CAMPO_ID_DOC+" <= "+lastId+
                        " AND D."+LocalDatabase.CAMPO_FECHAHORAMOV_DOC+" BETWEEN @startDate AND @endDate " +
                        "ORDER BY D."+LocalDatabase.CAMPO_ID_DOC+" DESC LIMIT "+LIMIT;
                        queryReporte = "SELECT D.id, D.clave_cliente, D.cliente_id, D.descuento, D.total, D.NOMBREU, D.ALMACEN_ID, D.ANTICIPO, " +
                        "D.TIPO_DOCUMENTO, D." + LocalDatabase.CAMPO_FORMACOBROID_DOC + ", D." + LocalDatabase.CAMPO_FACTURA_DOC + ", " +
                        "D.FVENTA, D.FECHAHORAMOV, D." + LocalDatabase.CAMPO_FORMACOBROIDABONO_DOC + ", " +
                        "D." + LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_DOC + ", D.cancelado, D.enviado_al_ws, D.id_web_service, D.archivado, D.pausa, " +
                        "D." + LocalDatabase.CAMPO_PAPELERARECICLAJE_DOC + " FROM Documentos D " +
                        "INNER JOIN PedidoEncabezado P ON D.CIDDOCTOPEDIDOCC = P.CIDDOCTOPEDIDOCC " +
                        "INNER JOIN Movimientos M ON D.id = M.DOCTO_ID_PEDIDO " +
                        "INNER JOIN Weight W ON M.id = W.movementId " +
                        "WHERE D.pausa = 1 AND P.type = 4 AND P.surtido = 1 AND P.listo = 1 " +
                        "AND W.box_weight = 0 AND W.net_weight != 0 AND " +
                        "D." + LocalDatabase.CAMPO_FECHAHORAMOV_DOC + " BETWEEN @startDate AND @endDate";
                        queryTotals = "SELECT COUNT(*) FROM Documentos D " +
                        "INNER JOIN PedidoEncabezado P ON D.CIDDOCTOPEDIDOCC = P.CIDDOCTOPEDIDOCC " +
                        "INNER JOIN Movimientos M ON D.id = M.DOCTO_ID_PEDIDO " +
                        "INNER JOIN Weight W ON M.id = W.movementId " +
                        "WHERE D.pausa = 1 AND P.type = 4 AND P.surtido = 1 AND P.listo = 1 " +
                        "AND W.box_weight = 0 AND W.net_weight != 0 AND D."+LocalDatabase.CAMPO_FECHAHORAMOV_DOC+" BETWEEN @startDate AND @endDate";
                        documentList = DocumentModel.getAllDocumentsWithParamtersDates(queryTemp, "startDate", startDate, "endDate", endDate);
                        totalItems = DocumentModel.getIntValueWithParametersDates(queryTotals, "startDate", startDate, "endDate", endDate);
                    } else
                    {
                        queryTemp = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " + LocalDatabase.CAMPO_ENVIADOALWS_DOC + " = 1 " +
                    "AND " + LocalDatabase.CAMPO_CANCELADO_DOC + " = 0 AND " + LocalDatabase.CAMPO_ID_DOC + " != 0 AND " +
                        LocalDatabase.CAMPO_ID_DOC + " <= " + lastId + " ORDER BY " + LocalDatabase.CAMPO_ID_DOC + " DESC LIMIT " + LIMIT;
                        queryReporte = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " + LocalDatabase.CAMPO_ENVIADOALWS_DOC + " = 1 " +
                    "AND " + LocalDatabase.CAMPO_CANCELADO_DOC + " = 0 AND " + LocalDatabase.CAMPO_ID_DOC + " != 0" +
                    " ORDER BY " + LocalDatabase.CAMPO_ID_DOC + " DESC";
                        queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " + LocalDatabase.CAMPO_ENVIADOALWS_DOC + " = 1 AND " +
                            LocalDatabase.CAMPO_CANCELADO_DOC + " = 0";
                        documentList = DocumentModel.getAllDocuments(queryTemp);
                        totalItems = DocumentModel.getIntValue(queryTotals);
                    }
                } else if (queryType == 3)
                {
                    /* Destarados */
                    if (permissionPrepedido)
                    {
                        queryTemp = "SELECT D.id, D.clave_cliente, D.cliente_id, D.descuento, D.total, D.NOMBREU, D.ALMACEN_ID, D.ANTICIPO, " +
                        "D.TIPO_DOCUMENTO, D." +LocalDatabase.CAMPO_FORMACOBROID_DOC+", D."+LocalDatabase.CAMPO_FACTURA_DOC+", "+
                        "D.FVENTA, D.FECHAHORAMOV, D."+LocalDatabase.CAMPO_FORMACOBROIDABONO_DOC+", " +
                        "D."+LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_DOC+", D.cancelado, D.enviado_al_ws, D.id_web_service, D.archivado, D.pausa, " +
                        "D."+LocalDatabase.CAMPO_PAPELERARECICLAJE_DOC+" FROM Documentos D " +
                        "INNER JOIN PedidoEncabezado P ON D.CIDDOCTOPEDIDOCC = P.CIDDOCTOPEDIDOCC " +
                        "INNER JOIN Movimientos M ON D.id = M.DOCTO_ID_PEDIDO " +
                        "INNER JOIN Weight W ON M.id = W.movementId " +
                        "WHERE D.id_web_service != 0 AND P.type = 4 AND P.surtido = 1 AND P.listo = 1 " +
                        "AND W.box_weight != 0 AND W.net_weight != 0 AND D." + LocalDatabase.CAMPO_ID_DOC + " <= " + lastId +
                        " AND D."+LocalDatabase.CAMPO_FECHAHORAMOV_DOC+" BETWEEN @startDate AND @endDate " +
                        "ORDER BY D." + LocalDatabase.CAMPO_ID_DOC + " DESC LIMIT " + LIMIT;
                        queryReporte = "SELECT D.id, D.clave_cliente, D.cliente_id, D.descuento, D.total, D.NOMBREU, D.ALMACEN_ID, D.ANTICIPO, " +
                        "D.TIPO_DOCUMENTO, D." + LocalDatabase.CAMPO_FORMACOBROID_DOC + ", D." + LocalDatabase.CAMPO_FACTURA_DOC + ", " +
                        "D.FVENTA, D.FECHAHORAMOV, D." + LocalDatabase.CAMPO_FORMACOBROIDABONO_DOC + ", " +
                        "D." + LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_DOC + ", D.cancelado, D.enviado_al_ws, D.id_web_service, D.archivado, D.pausa, " +
                        "D." + LocalDatabase.CAMPO_PAPELERARECICLAJE_DOC + " FROM Documentos D " +
                        "INNER JOIN PedidoEncabezado P ON D.CIDDOCTOPEDIDOCC = P.CIDDOCTOPEDIDOCC " +
                        "INNER JOIN Movimientos M ON D.id = M.DOCTO_ID_PEDIDO " +
                        "INNER JOIN Weight W ON M.id = W.movementId " +
                        "WHERE D.id_web_service != 0 AND P.type = 4 AND P.surtido = 1 AND P.listo = 1 " +
                        "AND W.box_weight != 0 AND W.net_weight != 0 " +
                        "AND D." + LocalDatabase.CAMPO_FECHAHORAMOV_DOC + " BETWEEN @startDate AND @endDate";
                        queryTotals = "SELECT COUNT(*) FROM Documentos D " +
                        "INNER JOIN PedidoEncabezado P ON D.CIDDOCTOPEDIDOCC = P.CIDDOCTOPEDIDOCC " +
                        "INNER JOIN Movimientos M ON D.id = M.DOCTO_ID_PEDIDO " +
                        "INNER JOIN Weight W ON M.id = W.movementId " +
                        "WHERE D.id_web_service != 0 AND P.type = 4 AND P.surtido = 1 AND P.listo = 1 " +
                        "AND W.box_weight != 0 AND W.net_weight != 0 AND D."+LocalDatabase.CAMPO_FECHAHORAMOV_DOC+" BETWEEN @startDate AND @endDate";
                        documentList = DocumentModel.getAllDocumentsWithParamtersDates(queryTemp, "startDate", startDate, "endDate", endDate);
                        totalItems = DocumentModel.getIntValueWithParametersDates(queryTotals, "startDate", startDate, "endDate", endDate);
                    } else
                    {
                        queryTemp = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                    LocalDatabase.CAMPO_PAUSAR_DOC + " = 1 AND " + LocalDatabase.CAMPO_CANCELADO_DOC + " = 0 AND " +
                    LocalDatabase.CAMPO_ID_DOC + " != 0 AND " + LocalDatabase.CAMPO_ID_DOC + " <= " + lastId +
                    " ORDER BY " + LocalDatabase.CAMPO_ID_DOC + " DESC LIMIT " + LIMIT;
                        queryReporte = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                    LocalDatabase.CAMPO_PAUSAR_DOC + " = 1 AND " + LocalDatabase.CAMPO_CANCELADO_DOC + " = 0 AND " +
                    LocalDatabase.CAMPO_ID_DOC + " != 0 ORDER BY " + LocalDatabase.CAMPO_ID_DOC + " DESC";
                        queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                        LocalDatabase.CAMPO_PAUSAR_DOC + " = 1 AND " + LocalDatabase.CAMPO_CANCELADO_DOC + " = 0";
                        documentList = DocumentModel.getAllDocuments(queryTemp);
                        totalItems = DocumentModel.getIntValue(queryTotals);
                    }
                } else if (queryType == 4)
                {
                    queryTemp = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " D INNER JOIN " + LocalDatabase.TABLA_CLIENTES + " C ON D." +
                   LocalDatabase.CAMPO_CLIENTEID_DOC + " = C." + LocalDatabase.CAMPO_ID_CLIENTE + " WHERE D." + LocalDatabase.CAMPO_ID_DOC + " != 0 AND D." +
                   LocalDatabase.CAMPO_ID_DOC + " <= " + lastId + " AND D." + LocalDatabase.CAMPO_FVENTA_DOC + " LIKE @searchWord1 OR C." +
                   LocalDatabase.CAMPO_NOMBRECLIENTE + " LIKE @searchWord2 ORDER BY D." + LocalDatabase.CAMPO_ID_DOC + " DESC LIMIT " + LIMIT;
                    queryReporte = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " D INNER JOIN " + LocalDatabase.TABLA_CLIENTES + " C ON D." +
                   LocalDatabase.CAMPO_CLIENTEID_DOC + " = C." + LocalDatabase.CAMPO_ID_CLIENTE + " WHERE D." + LocalDatabase.CAMPO_ID_DOC + " != 0" +
                   " AND D." + LocalDatabase.CAMPO_FVENTA_DOC + " LIKE @searchWord1 OR C." +LocalDatabase.CAMPO_NOMBRECLIENTE + " LIKE @searchWord2";
                    queryTotals = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " D " +
                    "INNER JOIN " + LocalDatabase.TABLA_CLIENTES + " C ON D." + LocalDatabase.CAMPO_CLIENTEID_DOC + " = C." +
                    LocalDatabase.CAMPO_ID_CLIENTE + " WHERE D." +
                    LocalDatabase.CAMPO_FVENTA_DOC + " LIKE @searchWord1 OR C." + LocalDatabase.CAMPO_NOMBRECLIENTE + " LIKE @searchWord2";
                    documentList = DocumentModel.getAllDocumentsWithParamtersToSearch(queryTemp, "searchWord1", searchWord, "searchWord2", searchWord);
                    totalItems = DocumentModel.getIntValueWithParametersToSearch(queryTotals, "searchWord1", searchWord, "searchWord2", searchWord);
                }

            });
            return documentList;
        }

        private void btnfrmArticulos_Click(object sender, EventArgs e)
        {

        }

        private void pictureCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PictureUpdate_Click(object sender, EventArgs e)
        {
            //Error = LlenaGridDoc("SELECT * FROM Documentos");
            //if (Error != 0)
            //{
            //    msj = new FormMessage("Ha habido un error al mostrar los documentos", "Error de Documentos", 2);
            //    msj.ShowDialog();
            //}
            resetearValores(queryType);
            fillDataGrid();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Error = LlenaGridDoc("SELECT * FROM Documentos");
            //if (Error != 0)
            //{
            //    msj = new FormMessage("Ha habido un error al mostrar los documentos", "Error de Documentos", 2);
            //    msj.ShowDialog();
            //}
            cmbDocumentosMostrar_SelectedIndexChanged(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //ClsMovimientosModel movesList = ClsMovimientosModel.getAllMovementsFromADocument(idDocument);
            //clsTicket Ticket = new clsTicket();
            //Ticket.CrearTicketResumenArticulo(movesList, ref MensajeError);
        }

        private void cmbDocumentosMostrar_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            getDocumentsByFilters();
            Cursor = Cursors.Default;
        }

        private void getDocumentsByFilters()
        {
            if (cmbDocumentosMostrar.Focused)
            {
                switch (cmbDocumentosMostrar.SelectedIndex)
                {
                    case 0:
                        queryType = 1;
                        break;
                    case 1:
                        queryType = 2;
                        break;
                    case 2:
                        queryType = 3;
                        break;
                    default: //TODOS
                        queryType = 0;
                        break;
                }
                resetearValores(queryType);
                fillDataGrid();
            }
        }

        private void dataGridDoc_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 1 && e.RowIndex >= 0) {
                try {
                    int idDocument = Convert.ToInt32(dataGridDoc.CurrentRow.Cells[1].Value.ToString());
                    if (!DocumentModel.verifyIfADocumentIsCanceled(idDocument))
                    {
                        FormMovimientos frmMovimientos = new FormMovimientos(this, idDocument, FormMovimientos.TIPO_MOVDOC, e.RowIndex);
                        frmMovimientos.ShowDialog();
                    }
                } catch (NullReferenceException ex) {
                    SECUDOC.writeLog("Exception: " + ex.ToString());
                }
            }
        }

        public void updateRowDataGridView(int positionList, int positionDgv, DocumentModel dm)
        {
            if (documentsList != null && documentsList.Count > 0)
            {
                documentsList[positionList] = dm;
                if (permissionPrepedido)
                    dataGridDoc.Rows[positionDgv].Height = 70;
                //dtGridClientes.Rows[i].Cells["images"].Value = Image.FromFile(@"C:\cliente_grid.png");
                dataGridDoc.Rows[positionDgv].Cells[0].Value = "false";
                if (permissionPrepedido)
                    dataGridDoc.Columns["colCheck"].Width = 50;
                else dataGridDoc.Columns["colCheck"].Width = 20;
                //dataGridDoc.Rows[n].Cells[0].ReadOnly = false;
                dataGridDoc.Rows[positionDgv].Cells[1].Value = dm.id;
                dataGridDoc.Columns["id"].Visible = false;
                if (dm.tipo_documento == 1)
                    dataGridDoc.Rows[positionDgv].Cells[2].Value = "Cotización";
                else if (dm.tipo_documento == 2)
                    dataGridDoc.Rows[positionDgv].Cells[2].Value = "Venta";
                else if (dm.tipo_documento == 3)
                    dataGridDoc.Rows[positionDgv].Cells[2].Value = "Pedido";
                else if (dm.tipo_documento == 4)
                    dataGridDoc.Rows[positionDgv].Cells[2].Value = "Venta";
                else if (dm.tipo_documento == 5)
                    dataGridDoc.Rows[positionDgv].Cells[2].Value = "Devolución";
                else if (dm.tipo_documento == 51)
                    dataGridDoc.Rows[positionDgv].Cells[2].Value = "Cotización de Mostrador";
                if (permissionPrepedido)
                {
                    dataGridDoc.Rows[positionDgv].Cells[2].Value = "Entregado";
                    dataGridDoc.Columns["Tipo"].Width = 80;
                }
                if (dm.cliente_id < 0)
                {
                    String customerName = "";
                    if (CustomerADCModel.isTheCustomerSendedByIdPanel(Math.Abs(dm.cliente_id)))
                    {
                        customerName = CustomerADCModel.getNombreWithPanelId(Math.Abs(dm.cliente_id));
                    }
                    else
                    {
                        customerName = CustomerADCModel.getNombreWithLocalId(Math.Abs(dm.cliente_id));
                    }
                    dataGridDoc.Rows[positionDgv].Cells[3].Value = customerName;
                    dataGridDoc.Columns["Cliente"].Width = 300;
                }
                else
                {
                    dataGridDoc.Rows[positionDgv].Cells[3].Value = CustomerModel.getAStringValueFromACustomer("SELECT " + LocalDatabase.CAMPO_NOMBRECLIENTE + " FROM " + LocalDatabase.TABLA_CLIENTES +
                    " WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " = " + dm.cliente_id);
                    dataGridDoc.Columns["Cliente"].Width = 300;
                }
                dataGridDoc.Rows[positionDgv].Cells[4].Value = dm.nombreu;
                dataGridDoc.Columns["nombre"].Width = 200;
                dataGridDoc.Rows[positionDgv].Cells[5].Value = dm.fventa;
                dataGridDoc.Columns["fventa"].Width = 120;
                if (permissionPrepedido)
                {
                    if (dm.fechahoramov.Equals(""))
                    {
                        String queryMov = "SELECT * FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " + LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " +
                            dm.id;
                        MovimientosModel mm = MovimientosModel.getAMovement(queryMov);
                        if (mm != null)
                        {
                            String queryPeso = "SELECT * FROM " + LocalDatabase.TABLA_PESO + " WHERE " + LocalDatabase.CAMPO_MOVIMIENTOID_PESO +
                                " = " + mm.id;
                            WeightModel wm = WeightModel.getAWeight(queryPeso);
                            if (wm != null)
                            {
                                if (wm.pesoCaja == 0)
                                    dataGridDoc.Rows[positionDgv].Cells[6].Value = "Documento Por Destarar";
                                else dataGridDoc.Rows[positionDgv].Cells[6].Value = "Documento Destarado";
                            }
                            else dataGridDoc.Rows[positionDgv].Cells[6].Value = "Documento Sin Peso Bruto";
                        }
                        else dataGridDoc.Rows[positionDgv].Cells[6].Value = "Documento Incompleto";
                    }
                    else dataGridDoc.Rows[positionDgv].Cells[6].Value = dm.fechahoramov;
                }
                else
                {
                    if (dm.fechahoramov.Equals(""))
                        dataGridDoc.Rows[positionDgv].Cells[6].Value = "Documento Incompleto";
                    else dataGridDoc.Rows[positionDgv].Cells[6].Value = dm.fechahoramov;
                }
                dataGridDoc.Columns["fechaHora"].Width = 100;
                if (permissionPrepedido)
                {
                    dataGridDoc.Rows[positionDgv].Cells[7].Value = "$ " + MetodosGenerales.obtieneDosDecimales(dm.total) + " MXN";
                    dataGridDoc.Columns[7].Visible = false;
                }
                else
                {
                    dataGridDoc.Rows[positionDgv].Cells[7].Value = dm.total.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                }
                if (dm.enviadoAlWs == 1 && dm.idWebService != 0 && dm.estado == 0)
                {
                    dataGridDoc.Rows[positionDgv].Cells[8].Value = MetodosGenerales.redimencionarBitmap(Properties.Resources.cloud_online_black, 25, 25);//Image.FromFile(ClsMetodosGenerales.rootDirectory + @"\Imagenes\Estaticas\check.png");
                    dataGridDoc[0, positionList].ReadOnly = true; //no permitir la selección
                }
                else
                {
                    if (dm.enviadoAlWs == 0 && dm.estado == 0 && dm.pausado == 0)
                    {
                        dataGridDoc.Rows[positionList].Cells[8].Value = MetodosGenerales.redimencionarBitmap(Properties.Resources.cloud_offline_black, 25, 25);
                    }
                    else if ((dm.pausado == 1 && dm.estado == 1) ||
                        (dm.pausado == 0 && dm.estado == 1))
                    {
                        dataGridDoc.Rows[positionDgv].Cells[8].Value = MetodosGenerales.redimencionarBitmap(Properties.Resources.cancel_black, 25, 25);
                        dataGridDoc[0, positionList].ReadOnly = true; //no permitir la selección
                    }
                    else if (dm.pausado == 1 && dm.estado == 0)
                    {
                        if (permissionPrepedido)
                        {
                            bool enviado = DocumentModel.isItDocumentPrepedidoSendedToTheCustomer(dm.id);
                            bool pesoBrutoCapturado = DocumentModel.pesoBrutoCapturado(dm.id);
                            if (enviado && pesoBrutoCapturado)
                            {
                                dataGridDoc.Rows[positionList].Cells[8].Value = MetodosGenerales.redimencionarBitmap(Properties.Resources.entregar_back, 25, 25);
                            }
                            else
                            {
                                dataGridDoc.Rows[positionList].Cells[8].Value = MetodosGenerales.redimencionarBitmap(Properties.Resources.pausa_black, 25, 25);
                            }
                        }
                        else
                        {
                            dataGridDoc.Rows[positionList].Cells[8].Value = MetodosGenerales.redimencionarBitmap(Properties.Resources.pausa_black, 25, 25);
                        }
                    }
                }
                dataGridDoc.Columns["status"].Width = 55;
            }
        }

        private void dataGridDoc_Scroll(object sender, ScrollEventArgs e)
        {
            if (documentsList.Count < totalItems && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
                {
                    if (e.NewValue > dataGridDoc.Rows.Count - getDisplayedRowsCount())
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
                            dataGridDoc.FirstDisplayedScrollingRowIndex = e.OldValue;
                        }
                    }
                }
            }
        }

        private int getDisplayedRowsCount()
        {
            int count = dataGridDoc.Rows[dataGridDoc.FirstDisplayedScrollingRowIndex].Height;
            count = dataGridDoc.Height / count;
            return count;
        }

        private void cbxDocumentosSeleccionarTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (dataGridDoc.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridDoc.Rows.Count; i++)
                {
                    if (!dataGridDoc[0, i].ReadOnly)
                        dataGridDoc[0, i].Value = cbxDocumentosSeleccionarTodos.Checked;
                }
            }
        }

        private void FrmResumenDocuments_FormClosed(object sender, FormClosedEventArgs e)
        {
            resetearValores(0);
        }

        private void editBuscarDocumentos_TextChanged(object sender, EventArgs e)
        {
            timerBuscarDocumentos.Stop();
            timerBuscarDocumentos.Start();
        }

        private void timerBuscarDocumentos_Tick(object sender, EventArgs e)
        {
            searchWord = editBuscarDocumentos.Text.Trim();
            if (searchWord.Equals(""))
            {
                resetearValores(0);
                fillDataGrid();
                timerBuscarDocumentos.Stop();
            }
            else
            {
                resetearValores(4);
                fillDataGrid();
                timerBuscarDocumentos.Stop();
            }
        }

        private void dateTimePickerStart_ValueChanged(object sender, EventArgs e)
        {
            startDate = (dateTimePickerStart.Value).ToString("yyyy-MM-dd");
            endDate = (dateTimePickerEnd.Value).ToString("yyyy-MM-dd");
            startDate += " 00:00:00";
            endDate += " 23:59:59";
            resetearValores(0);
            fillDataGrid();
        }

        private void dateTimePickerEnd_ValueChanged(object sender, EventArgs e)
        {
            startDate = (dateTimePickerStart.Value).ToString("yyyy-MM-dd");
            endDate = (dateTimePickerEnd.Value).ToString("yyyy-MM-dd");
            startDate += " 00:00:00";
            endDate += " 23:59:59";
            //FormMessage formMessage = new FormMessage("Valor", "Inicio: " + startDate + " Final: " + endDate, 1);
            //formMessage.ShowDialog();
            resetearValores(0);
            fillDataGrid();
        }

        private void btnBajar_Click(object sender, EventArgs e)
        {
            try
            {
                if (documentsList != null && documentsList.Count > 0)
                {
                    dataGridDoc.FirstDisplayedScrollingRowIndex = dataGridDoc.FirstDisplayedScrollingRowIndex + 1;
                }
            }
            catch (Exception ex)
            {
                //SECUDOC.writeLog(ex.ToString());
            }
        }

        private void btnSubir_Click(object sender, EventArgs e)
        {
            try
            {
                if (documentsList != null && documentsList.Count > 0)
                {
                    dataGridDoc.FirstDisplayedScrollingRowIndex = dataGridDoc.FirstDisplayedScrollingRowIndex - 1;
                }
            }
            catch (Exception ex)
            {
                //SECUDOC.writeLog(ex.ToString());
            }
        }

        private void FrmResumenDocuments_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                if (permissionPrepedido)
                    LIMIT = 30;
                else LIMIT = 40;
                resetearValores(queryType);
                fillDataGrid();
            }
            else
            {
                if (permissionPrepedido)
                    LIMIT = 15;
                else LIMIT = 20;
                resetearValores(queryType);
                fillDataGrid();
            }
        }

        private void btnReportePdf_Click(object sender, EventArgs e)
        {
            //FormMessage formMessage = new FormMessage("Procesando", "Opción en desarrollo!", 3);
            //formMessage.ShowDialog();
            frmWaiting = new FormWaiting(this, 0);
            frmWaiting.StartPosition = FormStartPosition.CenterScreen;
            frmWaiting.ShowDialog();
        }

        public async Task processToGenerateReportPdf()
        {
            String nameReport = "Documentos";
            String enterpriseName = "";
            DatosTicketModel dtm = DatosTicketModel.getAllData();
            if (dtm != null)
            {
                enterpriseName = dtm.EMPRESA;
            }
            String folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PDFsTPV";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            String filePath = @"" + folderPath + "\\" + "Reporte-" + nameReport + "_" + MetodosGenerales.getCurrentDateAndHourForFolioVenta() + ".pdf";
            bool created = false;
            if (queryType != 4)
            {
                created = await ClsPdfMethods.crearPdfDocumentosPrepedidos(enterpriseName, filePath, queryReporte, "startDate", startDate,
                "endDate", endDate, queryType);
            } else
            {
                created = await ClsPdfMethods.crearPdfDocumentosPrepedidos(enterpriseName, filePath, queryReporte, "searchWord1",
                "searchWord2", searchWord, searchWord, queryType);
            }
            if (created)
            {
                if (frmWaiting != null)
                    frmWaiting.Close();
                FormMessage formMessage = new FormMessage("Documento Generado", "El documento PDF fue generado correctamente!", 1);
                formMessage.ShowDialog();
            } else
            {
                if (frmWaiting != null)
                    frmWaiting.Close();
                FormMessage formMessage = new FormMessage("Documento No Generado", "Ocurrió un error al generar el documento PDF!", 3);
                formMessage.ShowDialog();
            }
            if (frmWaiting != null)
                frmWaiting.Close();
        }

        private void btnCancelDocuments_Click(object sender, EventArgs e)
        {
            validateDeleteMultiplesDocuments();
        }

        private async Task validateDeleteMultiplesDocuments()
        {
            //obtenemos ID de documentos seleccionados por el usuario
            List<int> listDocumentosSeleccionados = new List<int>();
            if (dataGridDoc.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridDoc.Rows.Count; i++)
                {
                    if (dataGridDoc[0, i].Value.ToString().ToUpper() == "TRUE")
                    {
                        int idDocumento = Convert.ToInt32(dataGridDoc[1, i].Value.ToString());
                        listDocumentosSeleccionados.Add(idDocumento);
                    }

                }
            }
            if (listDocumentosSeleccionados.Count == 0)
            {
                FormMessage message = new FormMessage("Selección No Encontrada", "Debe seleccionar al menos 1 documento para cancelar", 2);
                message.ShowDialog();
            }
            else
            {
                FrmConfirmation fc = new FrmConfirmation("Cancelar Documentos", "Estás seguro de que deseas cancelar los documentos seleccionados?");
                fc.ShowDialog();
                if (FrmConfirmation.confirmation)
                {
                    DeleteMultiplesDocsController dmdc = new DeleteMultiplesDocsController();
                    await dmdc.doInBackground(listDocumentosSeleccionados, serverModeLAN);
                    resetearValores(queryType);
                    fillDataGrid();
                }
            }
        }

        public void resetearValores(int queryType)
        {
            this.queryType = queryType;
            queryTemp = "";
            queryReporte = "";
            queryTotals = "";
            totalItems = 0;
            lastId = DocumentModel.getLastId();
            firstVisibleRow = 0;
            progress = 0;
            documentsList.Clear();
            dataGridDoc.Rows.Clear();
        }

    }
}
