
namespace SyncTPV.Views
{
    partial class FrmPedidosCotizacionesSurtir
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPedidosCotizacionesSurtir));
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.btnPdfPrepedidos = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelDataGridView = new System.Windows.Forms.Panel();
            this.tabControlPedidosCotizaciones = new System.Windows.Forms.TabControl();
            this.tabPageCotizacionesMostrador = new System.Windows.Forms.TabPage();
            this.panelOpciones = new System.Windows.Forms.Panel();
            this.textBuscarPorFolioCotizacionesMostrador = new System.Windows.Forms.Label();
            this.btnBuscarFolioCotizacionesMostrador = new System.Windows.Forms.Button();
            this.editBusquedaFolioCotizacionesMostrador = new System.Windows.Forms.TextBox();
            this.textTotalRecords = new System.Windows.Forms.Label();
            this.dataGridViewCotizacionesM = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.agente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.folio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.opcion = new System.Windows.Forms.DataGridViewButtonColumn();
            this.cancelar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tabPagePrepedidos = new System.Windows.Forms.TabPage();
            this.panelDocumentsPrePedidos = new System.Windows.Forms.Panel();
            this.imgSinDatpsPrePedidos = new System.Windows.Forms.PictureBox();
            this.dataGridViewPrePedidos = new System.Windows.Forms.DataGridView();
            this.idPrePedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idDocumentoPrePedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientePrePedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.agentePrePedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaPrePedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.folioVentaPrePedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.observationPrePedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.facturarPrePedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusPrePedito = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.capturedNonConvertibleUnitsPrePEdido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unidadNoConvertiblePrePedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.surtirPrePedido = new System.Windows.Forms.DataGridViewButtonColumn();
            this.eliminarPrePedido = new System.Windows.Forms.DataGridViewButtonColumn();
            this.textTotalPrePedidos = new System.Windows.Forms.Label();
            this.btnSearchPrePedido = new System.Windows.Forms.Button();
            this.textFolioPrePedido = new System.Windows.Forms.Label();
            this.editFolioPrePedido = new System.Windows.Forms.TextBox();
            this.panelSelectRutaPrePedidos = new System.Windows.Forms.Panel();
            this.comboBoxRutaCajaPrepedido = new System.Windows.Forms.ComboBox();
            this.checkBoxTodosLosPrepedidos = new System.Windows.Forms.CheckBox();
            this.btnUpPrepedidos = new System.Windows.Forms.Button();
            this.btnDownPrepedidosList = new System.Windows.Forms.Button();
            this.btnUpdateRoutes = new System.Windows.Forms.Button();
            this.comboBoxSelectRutas = new System.Windows.Forms.ComboBox();
            this.textBuscarPrepedido = new System.Windows.Forms.Label();
            this.editBuscarPrepedido = new System.Windows.Forms.TextBox();
            this.tabPagePedidos = new System.Windows.Forms.TabPage();
            this.timerBuscarPrepedido = new System.Windows.Forms.Timer(this.components);
            this.panelToolbar.SuspendLayout();
            this.panelDataGridView.SuspendLayout();
            this.tabControlPedidosCotizaciones.SuspendLayout();
            this.tabPageCotizacionesMostrador.SuspendLayout();
            this.panelOpciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCotizacionesM)).BeginInit();
            this.tabPagePrepedidos.SuspendLayout();
            this.panelDocumentsPrePedidos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatpsPrePedidos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPrePedidos)).BeginInit();
            this.panelSelectRutaPrePedidos.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelToolbar
            // 
            this.panelToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelToolbar.BackColor = System.Drawing.Color.Coral;
            this.panelToolbar.Controls.Add(this.btnPdfPrepedidos);
            this.panelToolbar.Controls.Add(this.btnRefresh);
            this.panelToolbar.Controls.Add(this.btnClose);
            this.panelToolbar.Location = new System.Drawing.Point(0, 0);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(1012, 68);
            this.panelToolbar.TabIndex = 0;
            // 
            // btnPdfPrepedidos
            // 
            this.btnPdfPrepedidos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPdfPrepedidos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPdfPrepedidos.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnPdfPrepedidos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnPdfPrepedidos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPdfPrepedidos.Font = new System.Drawing.Font("Roboto Black", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPdfPrepedidos.ForeColor = System.Drawing.Color.White;
            this.btnPdfPrepedidos.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPdfPrepedidos.Location = new System.Drawing.Point(930, 2);
            this.btnPdfPrepedidos.Name = "btnPdfPrepedidos";
            this.btnPdfPrepedidos.Size = new System.Drawing.Size(75, 63);
            this.btnPdfPrepedidos.TabIndex = 2;
            this.btnPdfPrepedidos.Text = "PDF";
            this.btnPdfPrepedidos.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPdfPrepedidos.UseVisualStyleBackColor = true;
            this.btnPdfPrepedidos.Click += new System.EventHandler(this.btnPdfPrepedidos_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Roboto Black", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Image = global::SyncTPV.Properties.Resources.update;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRefresh.Location = new System.Drawing.Point(93, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 63);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Actualizar";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Image = global::SyncTPV.Properties.Resources.close;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.Location = new System.Drawing.Point(12, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 63);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Cerrar";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelDataGridView
            // 
            this.panelDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDataGridView.BackColor = System.Drawing.Color.AliceBlue;
            this.panelDataGridView.Controls.Add(this.tabControlPedidosCotizaciones);
            this.panelDataGridView.Location = new System.Drawing.Point(0, 72);
            this.panelDataGridView.Name = "panelDataGridView";
            this.panelDataGridView.Size = new System.Drawing.Size(1012, 485);
            this.panelDataGridView.TabIndex = 2;
            // 
            // tabControlPedidosCotizaciones
            // 
            this.tabControlPedidosCotizaciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlPedidosCotizaciones.Controls.Add(this.tabPageCotizacionesMostrador);
            this.tabControlPedidosCotizaciones.Controls.Add(this.tabPagePrepedidos);
            this.tabControlPedidosCotizaciones.Controls.Add(this.tabPagePedidos);
            this.tabControlPedidosCotizaciones.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlPedidosCotizaciones.Location = new System.Drawing.Point(3, 3);
            this.tabControlPedidosCotizaciones.Name = "tabControlPedidosCotizaciones";
            this.tabControlPedidosCotizaciones.SelectedIndex = 0;
            this.tabControlPedidosCotizaciones.Size = new System.Drawing.Size(1006, 479);
            this.tabControlPedidosCotizaciones.TabIndex = 0;
            this.tabControlPedidosCotizaciones.SelectedIndexChanged += new System.EventHandler(this.tabControlPedidosCotizaciones_SelectedIndexChanged);
            // 
            // tabPageCotizacionesMostrador
            // 
            this.tabPageCotizacionesMostrador.BackColor = System.Drawing.Color.FloralWhite;
            this.tabPageCotizacionesMostrador.Controls.Add(this.panelOpciones);
            this.tabPageCotizacionesMostrador.Controls.Add(this.textTotalRecords);
            this.tabPageCotizacionesMostrador.Controls.Add(this.dataGridViewCotizacionesM);
            this.tabPageCotizacionesMostrador.Location = new System.Drawing.Point(4, 24);
            this.tabPageCotizacionesMostrador.Name = "tabPageCotizacionesMostrador";
            this.tabPageCotizacionesMostrador.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCotizacionesMostrador.Size = new System.Drawing.Size(998, 451);
            this.tabPageCotizacionesMostrador.TabIndex = 0;
            this.tabPageCotizacionesMostrador.Text = "Mostrador";
            // 
            // panelOpciones
            // 
            this.panelOpciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelOpciones.Controls.Add(this.textBuscarPorFolioCotizacionesMostrador);
            this.panelOpciones.Controls.Add(this.btnBuscarFolioCotizacionesMostrador);
            this.panelOpciones.Controls.Add(this.editBusquedaFolioCotizacionesMostrador);
            this.panelOpciones.Location = new System.Drawing.Point(6, 6);
            this.panelOpciones.Name = "panelOpciones";
            this.panelOpciones.Size = new System.Drawing.Size(986, 31);
            this.panelOpciones.TabIndex = 4;
            // 
            // textBuscarPorFolioCotizacionesMostrador
            // 
            this.textBuscarPorFolioCotizacionesMostrador.AutoSize = true;
            this.textBuscarPorFolioCotizacionesMostrador.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBuscarPorFolioCotizacionesMostrador.Location = new System.Drawing.Point(244, 7);
            this.textBuscarPorFolioCotizacionesMostrador.Name = "textBuscarPorFolioCotizacionesMostrador";
            this.textBuscarPorFolioCotizacionesMostrador.Size = new System.Drawing.Size(38, 14);
            this.textBuscarPorFolioCotizacionesMostrador.TabIndex = 2;
            this.textBuscarPorFolioCotizacionesMostrador.Text = "Folio:";
            // 
            // btnBuscarFolioCotizacionesMostrador
            // 
            this.btnBuscarFolioCotizacionesMostrador.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscarFolioCotizacionesMostrador.BackColor = System.Drawing.Color.LightBlue;
            this.btnBuscarFolioCotizacionesMostrador.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnBuscarFolioCotizacionesMostrador.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarFolioCotizacionesMostrador.Image = global::SyncTPV.Properties.Resources.search;
            this.btnBuscarFolioCotizacionesMostrador.Location = new System.Drawing.Point(622, 3);
            this.btnBuscarFolioCotizacionesMostrador.Name = "btnBuscarFolioCotizacionesMostrador";
            this.btnBuscarFolioCotizacionesMostrador.Size = new System.Drawing.Size(54, 23);
            this.btnBuscarFolioCotizacionesMostrador.TabIndex = 1;
            this.btnBuscarFolioCotizacionesMostrador.UseVisualStyleBackColor = false;
            // 
            // editBusquedaFolioCotizacionesMostrador
            // 
            this.editBusquedaFolioCotizacionesMostrador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editBusquedaFolioCotizacionesMostrador.Location = new System.Drawing.Point(293, 5);
            this.editBusquedaFolioCotizacionesMostrador.Name = "editBusquedaFolioCotizacionesMostrador";
            this.editBusquedaFolioCotizacionesMostrador.Size = new System.Drawing.Size(323, 23);
            this.editBusquedaFolioCotizacionesMostrador.TabIndex = 0;
            // 
            // textTotalRecords
            // 
            this.textTotalRecords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textTotalRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTotalRecords.Location = new System.Drawing.Point(6, 40);
            this.textTotalRecords.Name = "textTotalRecords";
            this.textTotalRecords.Size = new System.Drawing.Size(986, 25);
            this.textTotalRecords.TabIndex = 3;
            this.textTotalRecords.Text = "Documentos";
            this.textTotalRecords.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridViewCotizacionesM
            // 
            this.dataGridViewCotizacionesM.AllowUserToAddRows = false;
            this.dataGridViewCotizacionesM.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewCotizacionesM.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewCotizacionesM.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewCotizacionesM.BackgroundColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(1, 2, 1, 2);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCotizacionesM.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewCotizacionesM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCotizacionesM.ColumnHeadersVisible = false;
            this.dataGridViewCotizacionesM.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.idDocumento,
            this.cliente,
            this.agente,
            this.fecha,
            this.folio,
            this.subtotal,
            this.descuento,
            this.total,
            this.tipo,
            this.status,
            this.opcion,
            this.cancelar});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCotizacionesM.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewCotizacionesM.Location = new System.Drawing.Point(5, 68);
            this.dataGridViewCotizacionesM.Name = "dataGridViewCotizacionesM";
            this.dataGridViewCotizacionesM.RowHeadersVisible = false;
            this.dataGridViewCotizacionesM.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewCotizacionesM.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewCotizacionesM.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.dataGridViewCotizacionesM.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCotizacionesM.Size = new System.Drawing.Size(987, 377);
            this.dataGridViewCotizacionesM.TabIndex = 2;
            this.dataGridViewCotizacionesM.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridItems_Scroll);
            // 
            // id
            // 
            this.id.HeaderText = "Id";
            this.id.Name = "id";
            // 
            // idDocumento
            // 
            this.idDocumento.HeaderText = "IdDocumento";
            this.idDocumento.Name = "idDocumento";
            // 
            // cliente
            // 
            this.cliente.HeaderText = "Cliente";
            this.cliente.Name = "cliente";
            // 
            // agente
            // 
            this.agente.HeaderText = "Agente";
            this.agente.Name = "agente";
            // 
            // fecha
            // 
            this.fecha.HeaderText = "Fecha";
            this.fecha.Name = "fecha";
            // 
            // folio
            // 
            this.folio.HeaderText = "Folio";
            this.folio.Name = "folio";
            // 
            // subtotal
            // 
            this.subtotal.HeaderText = "Subtotal";
            this.subtotal.Name = "subtotal";
            // 
            // descuento
            // 
            this.descuento.HeaderText = "Descuento";
            this.descuento.Name = "descuento";
            // 
            // total
            // 
            this.total.HeaderText = "Total";
            this.total.Name = "total";
            // 
            // tipo
            // 
            this.tipo.HeaderText = "Tipo";
            this.tipo.Name = "tipo";
            // 
            // status
            // 
            this.status.HeaderText = "Estatus";
            this.status.Name = "status";
            // 
            // opcion
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.PowderBlue;
            this.opcion.DefaultCellStyle = dataGridViewCellStyle2;
            this.opcion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.opcion.HeaderText = "Opción";
            this.opcion.Name = "opcion";
            // 
            // cancelar
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.MistyRose;
            this.cancelar.DefaultCellStyle = dataGridViewCellStyle3;
            this.cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelar.HeaderText = "Cancelar";
            this.cancelar.Name = "cancelar";
            this.cancelar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cancelar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // tabPagePrepedidos
            // 
            this.tabPagePrepedidos.BackColor = System.Drawing.Color.FloralWhite;
            this.tabPagePrepedidos.Controls.Add(this.panelDocumentsPrePedidos);
            this.tabPagePrepedidos.Controls.Add(this.panelSelectRutaPrePedidos);
            this.tabPagePrepedidos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPagePrepedidos.Location = new System.Drawing.Point(4, 24);
            this.tabPagePrepedidos.Name = "tabPagePrepedidos";
            this.tabPagePrepedidos.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePrepedidos.Size = new System.Drawing.Size(998, 451);
            this.tabPagePrepedidos.TabIndex = 1;
            this.tabPagePrepedidos.Text = "PrePedidos";
            // 
            // panelDocumentsPrePedidos
            // 
            this.panelDocumentsPrePedidos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDocumentsPrePedidos.Controls.Add(this.imgSinDatpsPrePedidos);
            this.panelDocumentsPrePedidos.Controls.Add(this.dataGridViewPrePedidos);
            this.panelDocumentsPrePedidos.Controls.Add(this.textTotalPrePedidos);
            this.panelDocumentsPrePedidos.Controls.Add(this.btnSearchPrePedido);
            this.panelDocumentsPrePedidos.Controls.Add(this.textFolioPrePedido);
            this.panelDocumentsPrePedidos.Controls.Add(this.editFolioPrePedido);
            this.panelDocumentsPrePedidos.Location = new System.Drawing.Point(8, 126);
            this.panelDocumentsPrePedidos.Name = "panelDocumentsPrePedidos";
            this.panelDocumentsPrePedidos.Size = new System.Drawing.Size(987, 319);
            this.panelDocumentsPrePedidos.TabIndex = 1;
            // 
            // imgSinDatpsPrePedidos
            // 
            this.imgSinDatpsPrePedidos.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.imgSinDatpsPrePedidos.Image = global::SyncTPV.Properties.Resources.sindatos;
            this.imgSinDatpsPrePedidos.Location = new System.Drawing.Point(388, 95);
            this.imgSinDatpsPrePedidos.Name = "imgSinDatpsPrePedidos";
            this.imgSinDatpsPrePedidos.Size = new System.Drawing.Size(225, 168);
            this.imgSinDatpsPrePedidos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgSinDatpsPrePedidos.TabIndex = 5;
            this.imgSinDatpsPrePedidos.TabStop = false;
            // 
            // dataGridViewPrePedidos
            // 
            this.dataGridViewPrePedidos.AllowUserToAddRows = false;
            this.dataGridViewPrePedidos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewPrePedidos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewPrePedidos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewPrePedidos.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.dataGridViewPrePedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPrePedidos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idPrePedido,
            this.idDocumentoPrePedido,
            this.clientePrePedido,
            this.agentePrePedido,
            this.fechaPrePedido,
            this.folioVentaPrePedido,
            this.observationPrePedido,
            this.facturarPrePedido,
            this.statusPrePedito,
            this.capturedNonConvertibleUnitsPrePEdido,
            this.unidadNoConvertiblePrePedido,
            this.surtirPrePedido,
            this.eliminarPrePedido});
            this.dataGridViewPrePedidos.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewPrePedidos.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewPrePedidos.Location = new System.Drawing.Point(3, 22);
            this.dataGridViewPrePedidos.MultiSelect = false;
            this.dataGridViewPrePedidos.Name = "dataGridViewPrePedidos";
            this.dataGridViewPrePedidos.ReadOnly = true;
            this.dataGridViewPrePedidos.RowHeadersVisible = false;
            this.dataGridViewPrePedidos.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewPrePedidos.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewPrePedidos.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.dataGridViewPrePedidos.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewPrePedidos.RowTemplate.Height = 100;
            this.dataGridViewPrePedidos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPrePedidos.Size = new System.Drawing.Size(981, 290);
            this.dataGridViewPrePedidos.TabIndex = 4;
            this.dataGridViewPrePedidos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPrePedidos_CellClick);
            this.dataGridViewPrePedidos.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridViewPrePedidos_Scroll);
            // 
            // idPrePedido
            // 
            this.idPrePedido.HeaderText = "Id";
            this.idPrePedido.Name = "idPrePedido";
            this.idPrePedido.ReadOnly = true;
            // 
            // idDocumentoPrePedido
            // 
            this.idDocumentoPrePedido.HeaderText = "IdDocumento";
            this.idDocumentoPrePedido.Name = "idDocumentoPrePedido";
            this.idDocumentoPrePedido.ReadOnly = true;
            // 
            // clientePrePedido
            // 
            this.clientePrePedido.HeaderText = "Cliente";
            this.clientePrePedido.Name = "clientePrePedido";
            this.clientePrePedido.ReadOnly = true;
            // 
            // agentePrePedido
            // 
            this.agentePrePedido.HeaderText = "Agente";
            this.agentePrePedido.Name = "agentePrePedido";
            this.agentePrePedido.ReadOnly = true;
            // 
            // fechaPrePedido
            // 
            this.fechaPrePedido.HeaderText = "Fecha";
            this.fechaPrePedido.Name = "fechaPrePedido";
            this.fechaPrePedido.ReadOnly = true;
            // 
            // folioVentaPrePedido
            // 
            this.folioVentaPrePedido.HeaderText = "Folio";
            this.folioVentaPrePedido.Name = "folioVentaPrePedido";
            this.folioVentaPrePedido.ReadOnly = true;
            // 
            // observationPrePedido
            // 
            this.observationPrePedido.HeaderText = "Observación";
            this.observationPrePedido.Name = "observationPrePedido";
            this.observationPrePedido.ReadOnly = true;
            // 
            // facturarPrePedido
            // 
            this.facturarPrePedido.HeaderText = "Facturar";
            this.facturarPrePedido.Name = "facturarPrePedido";
            this.facturarPrePedido.ReadOnly = true;
            // 
            // statusPrePedito
            // 
            this.statusPrePedito.HeaderText = "Status";
            this.statusPrePedito.Name = "statusPrePedito";
            this.statusPrePedito.ReadOnly = true;
            // 
            // capturedNonConvertibleUnitsPrePEdido
            // 
            this.capturedNonConvertibleUnitsPrePEdido.HeaderText = "Cantidad";
            this.capturedNonConvertibleUnitsPrePEdido.Name = "capturedNonConvertibleUnitsPrePEdido";
            this.capturedNonConvertibleUnitsPrePEdido.ReadOnly = true;
            // 
            // unidadNoConvertiblePrePedido
            // 
            this.unidadNoConvertiblePrePedido.HeaderText = "Unidad";
            this.unidadNoConvertiblePrePedido.Name = "unidadNoConvertiblePrePedido";
            this.unidadNoConvertiblePrePedido.ReadOnly = true;
            // 
            // surtirPrePedido
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.PowderBlue;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.surtirPrePedido.DefaultCellStyle = dataGridViewCellStyle5;
            this.surtirPrePedido.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.surtirPrePedido.HeaderText = "Opción";
            this.surtirPrePedido.Name = "surtirPrePedido";
            this.surtirPrePedido.ReadOnly = true;
            // 
            // eliminarPrePedido
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.MistyRose;
            this.eliminarPrePedido.DefaultCellStyle = dataGridViewCellStyle6;
            this.eliminarPrePedido.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.eliminarPrePedido.HeaderText = "Eliminar";
            this.eliminarPrePedido.Name = "eliminarPrePedido";
            this.eliminarPrePedido.ReadOnly = true;
            // 
            // textTotalPrePedidos
            // 
            this.textTotalPrePedidos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textTotalPrePedidos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTotalPrePedidos.Location = new System.Drawing.Point(5, 0);
            this.textTotalPrePedidos.Name = "textTotalPrePedidos";
            this.textTotalPrePedidos.Size = new System.Drawing.Size(979, 19);
            this.textTotalPrePedidos.TabIndex = 3;
            this.textTotalPrePedidos.Text = "Prepedidos";
            this.textTotalPrePedidos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSearchPrePedido
            // 
            this.btnSearchPrePedido.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchPrePedido.BackColor = System.Drawing.Color.Transparent;
            this.btnSearchPrePedido.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnSearchPrePedido.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchPrePedido.Location = new System.Drawing.Point(627, -73);
            this.btnSearchPrePedido.Name = "btnSearchPrePedido";
            this.btnSearchPrePedido.Size = new System.Drawing.Size(29, 23);
            this.btnSearchPrePedido.TabIndex = 2;
            this.btnSearchPrePedido.UseVisualStyleBackColor = false;
            // 
            // textFolioPrePedido
            // 
            this.textFolioPrePedido.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textFolioPrePedido.AutoSize = true;
            this.textFolioPrePedido.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textFolioPrePedido.Location = new System.Drawing.Point(244, -69);
            this.textFolioPrePedido.Name = "textFolioPrePedido";
            this.textFolioPrePedido.Size = new System.Drawing.Size(49, 14);
            this.textFolioPrePedido.TabIndex = 1;
            this.textFolioPrePedido.Text = "Buscar:";
            // 
            // editFolioPrePedido
            // 
            this.editFolioPrePedido.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editFolioPrePedido.Location = new System.Drawing.Point(301, -72);
            this.editFolioPrePedido.Name = "editFolioPrePedido";
            this.editFolioPrePedido.Size = new System.Drawing.Size(320, 22);
            this.editFolioPrePedido.TabIndex = 0;
            this.editFolioPrePedido.TextChanged += new System.EventHandler(this.editFolioPrePedido_TextChanged);
            this.editFolioPrePedido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editFolioPrePedido_KeyPress);
            // 
            // panelSelectRutaPrePedidos
            // 
            this.panelSelectRutaPrePedidos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSelectRutaPrePedidos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelSelectRutaPrePedidos.Controls.Add(this.comboBoxRutaCajaPrepedido);
            this.panelSelectRutaPrePedidos.Controls.Add(this.checkBoxTodosLosPrepedidos);
            this.panelSelectRutaPrePedidos.Controls.Add(this.btnUpPrepedidos);
            this.panelSelectRutaPrePedidos.Controls.Add(this.btnDownPrepedidosList);
            this.panelSelectRutaPrePedidos.Controls.Add(this.btnUpdateRoutes);
            this.panelSelectRutaPrePedidos.Controls.Add(this.comboBoxSelectRutas);
            this.panelSelectRutaPrePedidos.Controls.Add(this.textBuscarPrepedido);
            this.panelSelectRutaPrePedidos.Controls.Add(this.editBuscarPrepedido);
            this.panelSelectRutaPrePedidos.Location = new System.Drawing.Point(6, 6);
            this.panelSelectRutaPrePedidos.Name = "panelSelectRutaPrePedidos";
            this.panelSelectRutaPrePedidos.Size = new System.Drawing.Size(986, 114);
            this.panelSelectRutaPrePedidos.TabIndex = 0;
            // 
            // comboBoxRutaCajaPrepedido
            // 
            this.comboBoxRutaCajaPrepedido.BackColor = System.Drawing.Color.LightSkyBlue;
            this.comboBoxRutaCajaPrepedido.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxRutaCajaPrepedido.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRutaCajaPrepedido.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxRutaCajaPrepedido.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxRutaCajaPrepedido.FormattingEnabled = true;
            this.comboBoxRutaCajaPrepedido.Location = new System.Drawing.Point(463, 8);
            this.comboBoxRutaCajaPrepedido.Name = "comboBoxRutaCajaPrepedido";
            this.comboBoxRutaCajaPrepedido.Size = new System.Drawing.Size(220, 26);
            this.comboBoxRutaCajaPrepedido.TabIndex = 11;
            this.comboBoxRutaCajaPrepedido.SelectedIndexChanged += new System.EventHandler(this.comboBoxRutaCajaPrepedido_SelectedIndexChanged);
            // 
            // checkBoxTodosLosPrepedidos
            // 
            this.checkBoxTodosLosPrepedidos.AutoSize = true;
            this.checkBoxTodosLosPrepedidos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBoxTodosLosPrepedidos.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.checkBoxTodosLosPrepedidos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.checkBoxTodosLosPrepedidos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxTodosLosPrepedidos.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxTodosLosPrepedidos.Location = new System.Drawing.Point(311, 13);
            this.checkBoxTodosLosPrepedidos.Name = "checkBoxTodosLosPrepedidos";
            this.checkBoxTodosLosPrepedidos.Size = new System.Drawing.Size(143, 22);
            this.checkBoxTodosLosPrepedidos.TabIndex = 10;
            this.checkBoxTodosLosPrepedidos.Text = "Todos los pedidos";
            this.checkBoxTodosLosPrepedidos.UseVisualStyleBackColor = true;
            this.checkBoxTodosLosPrepedidos.CheckedChanged += new System.EventHandler(this.checkBoxTodosLosPrepedidos_CheckedChanged);
            // 
            // btnUpPrepedidos
            // 
            this.btnUpPrepedidos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpPrepedidos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpPrepedidos.FlatAppearance.BorderColor = System.Drawing.Color.Orange;
            this.btnUpPrepedidos.FlatAppearance.BorderSize = 2;
            this.btnUpPrepedidos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnUpPrepedidos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpPrepedidos.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpPrepedidos.Location = new System.Drawing.Point(791, 9);
            this.btnUpPrepedidos.Name = "btnUpPrepedidos";
            this.btnUpPrepedidos.Size = new System.Drawing.Size(75, 53);
            this.btnUpPrepedidos.TabIndex = 9;
            this.btnUpPrepedidos.Text = "Subir";
            this.btnUpPrepedidos.UseVisualStyleBackColor = true;
            this.btnUpPrepedidos.Click += new System.EventHandler(this.btnUpPrepedidos_Click);
            // 
            // btnDownPrepedidosList
            // 
            this.btnDownPrepedidosList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownPrepedidosList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDownPrepedidosList.FlatAppearance.BorderColor = System.Drawing.Color.Orange;
            this.btnDownPrepedidosList.FlatAppearance.BorderSize = 2;
            this.btnDownPrepedidosList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnDownPrepedidosList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownPrepedidosList.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownPrepedidosList.Location = new System.Drawing.Point(872, 9);
            this.btnDownPrepedidosList.Name = "btnDownPrepedidosList";
            this.btnDownPrepedidosList.Size = new System.Drawing.Size(75, 53);
            this.btnDownPrepedidosList.TabIndex = 8;
            this.btnDownPrepedidosList.Text = "Bajar";
            this.btnDownPrepedidosList.UseVisualStyleBackColor = true;
            this.btnDownPrepedidosList.Click += new System.EventHandler(this.btnDownPrepedidosList_Click);
            // 
            // btnUpdateRoutes
            // 
            this.btnUpdateRoutes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdateRoutes.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnUpdateRoutes.FlatAppearance.BorderSize = 2;
            this.btnUpdateRoutes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnUpdateRoutes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateRoutes.Location = new System.Drawing.Point(693, 61);
            this.btnUpdateRoutes.Name = "btnUpdateRoutes";
            this.btnUpdateRoutes.Size = new System.Drawing.Size(57, 46);
            this.btnUpdateRoutes.TabIndex = 7;
            this.btnUpdateRoutes.UseVisualStyleBackColor = true;
            this.btnUpdateRoutes.Click += new System.EventHandler(this.btnUpdateRoutes_Click);
            // 
            // comboBoxSelectRutas
            // 
            this.comboBoxSelectRutas.BackColor = System.Drawing.Color.LightSkyBlue;
            this.comboBoxSelectRutas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxSelectRutas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectRutas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxSelectRutas.Font = new System.Drawing.Font("Roboto Black", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxSelectRutas.FormattingEnabled = true;
            this.comboBoxSelectRutas.IntegralHeight = false;
            this.comboBoxSelectRutas.ItemHeight = 38;
            this.comboBoxSelectRutas.Location = new System.Drawing.Point(311, 61);
            this.comboBoxSelectRutas.Name = "comboBoxSelectRutas";
            this.comboBoxSelectRutas.Size = new System.Drawing.Size(357, 46);
            this.comboBoxSelectRutas.TabIndex = 6;
            this.comboBoxSelectRutas.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelectRutas_SelectedIndexChanged);
            // 
            // textBuscarPrepedido
            // 
            this.textBuscarPrepedido.AutoSize = true;
            this.textBuscarPrepedido.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBuscarPrepedido.Location = new System.Drawing.Point(5, 13);
            this.textBuscarPrepedido.Name = "textBuscarPrepedido";
            this.textBuscarPrepedido.Size = new System.Drawing.Size(221, 15);
            this.textBuscarPrepedido.TabIndex = 4;
            this.textBuscarPrepedido.Text = "Buscar por nombre del cliente o folio";
            // 
            // editBuscarPrepedido
            // 
            this.editBuscarPrepedido.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editBuscarPrepedido.Location = new System.Drawing.Point(8, 35);
            this.editBuscarPrepedido.Name = "editBuscarPrepedido";
            this.editBuscarPrepedido.Size = new System.Drawing.Size(232, 22);
            this.editBuscarPrepedido.TabIndex = 3;
            this.editBuscarPrepedido.TextChanged += new System.EventHandler(this.editBuscarPrepedido_TextChanged);
            // 
            // tabPagePedidos
            // 
            this.tabPagePedidos.BackColor = System.Drawing.Color.FloralWhite;
            this.tabPagePedidos.Location = new System.Drawing.Point(4, 24);
            this.tabPagePedidos.Name = "tabPagePedidos";
            this.tabPagePedidos.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePedidos.Size = new System.Drawing.Size(998, 451);
            this.tabPagePedidos.TabIndex = 2;
            this.tabPagePedidos.Text = "Pedidos";
            // 
            // timerBuscarPrepedido
            // 
            this.timerBuscarPrepedido.Interval = 300;
            this.timerBuscarPrepedido.Tick += new System.EventHandler(this.timerBuscarPrepedido_Tick);
            // 
            // FrmPedidosCotizacionesSurtir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(1011, 556);
            this.Controls.Add(this.panelDataGridView);
            this.Controls.Add(this.panelToolbar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmPedidosCotizacionesSurtir";
            this.Text = "Pedidos y Cotizaciones a Surtir";
            this.Load += new System.EventHandler(this.FrmPedidosCotizacionesSurtir_Load);
            this.Resize += new System.EventHandler(this.FrmPedidosCotizacionesSurtir_Resize);
            this.panelToolbar.ResumeLayout(false);
            this.panelDataGridView.ResumeLayout(false);
            this.tabControlPedidosCotizaciones.ResumeLayout(false);
            this.tabPageCotizacionesMostrador.ResumeLayout(false);
            this.panelOpciones.ResumeLayout(false);
            this.panelOpciones.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCotizacionesM)).EndInit();
            this.tabPagePrepedidos.ResumeLayout(false);
            this.panelDocumentsPrePedidos.ResumeLayout(false);
            this.panelDocumentsPrePedidos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatpsPrePedidos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPrePedidos)).EndInit();
            this.panelSelectRutaPrePedidos.ResumeLayout(false);
            this.panelSelectRutaPrePedidos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.Panel panelDataGridView;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TabControl tabControlPedidosCotizaciones;
        private System.Windows.Forms.TabPage tabPageCotizacionesMostrador;
        private System.Windows.Forms.Label textTotalRecords;
        private System.Windows.Forms.DataGridView dataGridViewCotizacionesM;
        private System.Windows.Forms.TabPage tabPagePrepedidos;
        private System.Windows.Forms.TabPage tabPagePedidos;
        private System.Windows.Forms.Panel panelDocumentsPrePedidos;
        private System.Windows.Forms.Panel panelSelectRutaPrePedidos;
        private System.Windows.Forms.Label textBuscarPrepedido;
        private System.Windows.Forms.TextBox editBuscarPrepedido;
        private System.Windows.Forms.Panel panelOpciones;
        private System.Windows.Forms.Label textBuscarPorFolioCotizacionesMostrador;
        private System.Windows.Forms.Button btnBuscarFolioCotizacionesMostrador;
        private System.Windows.Forms.TextBox editBusquedaFolioCotizacionesMostrador;
        private System.Windows.Forms.TextBox editFolioPrePedido;
        private System.Windows.Forms.Label textFolioPrePedido;
        private System.Windows.Forms.Button btnSearchPrePedido;
        private System.Windows.Forms.DataGridView dataGridViewPrePedidos;
        private System.Windows.Forms.Label textTotalPrePedidos;
        private System.Windows.Forms.PictureBox imgSinDatpsPrePedidos;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn agente;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn folio;
        private System.Windows.Forms.DataGridViewTextBoxColumn subtotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn descuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn total;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewButtonColumn opcion;
        private System.Windows.Forms.DataGridViewButtonColumn cancelar;
        private System.Windows.Forms.DataGridViewTextBoxColumn idPrePedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDocumentoPrePedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn clientePrePedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn agentePrePedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaPrePedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn folioVentaPrePedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn observationPrePedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn facturarPrePedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusPrePedito;
        private System.Windows.Forms.DataGridViewTextBoxColumn capturedNonConvertibleUnitsPrePEdido;
        private System.Windows.Forms.DataGridViewTextBoxColumn unidadNoConvertiblePrePedido;
        private System.Windows.Forms.DataGridViewButtonColumn surtirPrePedido;
        private System.Windows.Forms.DataGridViewButtonColumn eliminarPrePedido;
        private System.Windows.Forms.ComboBox comboBoxSelectRutas;
        private System.Windows.Forms.Button btnUpdateRoutes;
        private System.Windows.Forms.Button btnDownPrepedidosList;
        private System.Windows.Forms.Button btnUpPrepedidos;
        private System.Windows.Forms.Button btnPdfPrepedidos;
        private System.Windows.Forms.Timer timerBuscarPrepedido;
        private System.Windows.Forms.ComboBox comboBoxRutaCajaPrepedido;
        private System.Windows.Forms.CheckBox checkBoxTodosLosPrepedidos;
    }
}