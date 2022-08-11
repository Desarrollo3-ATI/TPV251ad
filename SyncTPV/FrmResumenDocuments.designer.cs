using SyncTPV.Helpers.Design;

namespace SyncTPV
{
    partial class FrmResumenDocuments
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmResumenDocuments));
            this.dataGridDoc = new System.Windows.Forms.DataGridView();
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaHora = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewImageColumn();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.panelTabResumenDocuments = new System.Windows.Forms.Panel();
            this.panelFiltros = new System.Windows.Forms.Panel();
            this.btnSubir = new System.Windows.Forms.Button();
            this.btnBajar = new System.Windows.Forms.Button();
            this.btnReportePdf = new System.Windows.Forms.Button();
            this.textFechaFin = new System.Windows.Forms.Label();
            this.textFechaInicio = new System.Windows.Forms.Label();
            this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.tabControlResumenDocumentos = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textTotalDocumentos = new System.Windows.Forms.Label();
            this.imgSinDatosFrmResumenDocumentos = new System.Windows.Forms.PictureBox();
            this.cbxDocumentosSeleccionarTodos = new System.Windows.Forms.CheckBox();
            this.pnlDocumentosTop = new System.Windows.Forms.Panel();
            this.textBuscar = new System.Windows.Forms.Label();
            this.editBuscarDocumentos = new System.Windows.Forms.TextBox();
            this.cmbDocumentosMostrar = new System.Windows.Forms.ComboBox();
            this.lblDocumentosMostrar = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancelDocuments = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.timerBuscarDocumentos = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDoc)).BeginInit();
            this.pnlContent.SuspendLayout();
            this.panelTabResumenDocuments.SuspendLayout();
            this.panelFiltros.SuspendLayout();
            this.tabControlResumenDocumentos.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatosFrmResumenDocumentos)).BeginInit();
            this.pnlDocumentosTop.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridDoc
            // 
            this.dataGridDoc.AllowUserToAddRows = false;
            this.dataGridDoc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridDoc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridDoc.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridDoc.BackgroundColor = System.Drawing.Color.Azure;
            this.dataGridDoc.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Coral;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridDoc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridDoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridDoc.ColumnHeadersVisible = false;
            this.dataGridDoc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.id,
            this.Tipo,
            this.Cliente,
            this.nombre,
            this.fventa,
            this.fechaHora,
            this.total,
            this.status});
            this.dataGridDoc.EnableHeadersVisualStyles = false;
            this.dataGridDoc.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.dataGridDoc.Location = new System.Drawing.Point(3, 85);
            this.dataGridDoc.Name = "dataGridDoc";
            this.dataGridDoc.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DeepSkyBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridDoc.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridDoc.RowHeadersVisible = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(145)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridDoc.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridDoc.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridDoc.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dataGridDoc.RowTemplate.Height = 25;
            this.dataGridDoc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridDoc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridDoc.Size = new System.Drawing.Size(951, 327);
            this.dataGridDoc.TabIndex = 0;
            this.dataGridDoc.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridDoc_CellDoubleClick);
            this.dataGridDoc.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridDoc_Scroll);
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "-";
            this.colCheck.Name = "colCheck";
            this.colCheck.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCheck.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // id
            // 
            this.id.HeaderText = "Id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            // 
            // Tipo
            // 
            this.Tipo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Tipo.HeaderText = "Tipo";
            this.Tipo.Name = "Tipo";
            this.Tipo.ReadOnly = true;
            // 
            // Cliente
            // 
            this.Cliente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Cliente.HeaderText = "Cliente";
            this.Cliente.Name = "Cliente";
            this.Cliente.ReadOnly = true;
            this.Cliente.Width = 65;
            // 
            // nombre
            // 
            this.nombre.HeaderText = "Nombre";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            // 
            // fventa
            // 
            this.fventa.HeaderText = "Folio Venta";
            this.fventa.Name = "fventa";
            this.fventa.ReadOnly = true;
            // 
            // fechaHora
            // 
            this.fechaHora.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.fechaHora.HeaderText = "Fecha";
            this.fechaHora.Name = "fechaHora";
            this.fechaHora.ReadOnly = true;
            // 
            // total
            // 
            this.total.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.total.HeaderText = "Total";
            this.total.Name = "total";
            this.total.ReadOnly = true;
            // 
            // status
            // 
            this.status.HeaderText = "Estatus";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.NavajoWhite;
            this.pnlContent.Controls.Add(this.panelTabResumenDocuments);
            this.pnlContent.Controls.Add(this.panel1);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(974, 611);
            this.pnlContent.TabIndex = 16;
            // 
            // panelTabResumenDocuments
            // 
            this.panelTabResumenDocuments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTabResumenDocuments.Controls.Add(this.panelFiltros);
            this.panelTabResumenDocuments.Controls.Add(this.tabControlResumenDocumentos);
            this.panelTabResumenDocuments.Location = new System.Drawing.Point(0, 76);
            this.panelTabResumenDocuments.Name = "panelTabResumenDocuments";
            this.panelTabResumenDocuments.Size = new System.Drawing.Size(971, 535);
            this.panelTabResumenDocuments.TabIndex = 5;
            // 
            // panelFiltros
            // 
            this.panelFiltros.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFiltros.Controls.Add(this.btnSubir);
            this.panelFiltros.Controls.Add(this.btnBajar);
            this.panelFiltros.Controls.Add(this.btnReportePdf);
            this.panelFiltros.Controls.Add(this.textFechaFin);
            this.panelFiltros.Controls.Add(this.textFechaInicio);
            this.panelFiltros.Controls.Add(this.dateTimePickerEnd);
            this.panelFiltros.Controls.Add(this.dateTimePickerStart);
            this.panelFiltros.Location = new System.Drawing.Point(3, 3);
            this.panelFiltros.Name = "panelFiltros";
            this.panelFiltros.Size = new System.Drawing.Size(965, 76);
            this.panelFiltros.TabIndex = 2;
            // 
            // btnSubir
            // 
            this.btnSubir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSubir.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnSubir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSubir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubir.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubir.Location = new System.Drawing.Point(733, 10);
            this.btnSubir.Name = "btnSubir";
            this.btnSubir.Size = new System.Drawing.Size(98, 57);
            this.btnSubir.TabIndex = 6;
            this.btnSubir.Text = "Subir";
            this.btnSubir.UseVisualStyleBackColor = true;
            this.btnSubir.Click += new System.EventHandler(this.btnSubir_Click);
            // 
            // btnBajar
            // 
            this.btnBajar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBajar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBajar.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnBajar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnBajar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBajar.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBajar.Location = new System.Drawing.Point(863, 10);
            this.btnBajar.Name = "btnBajar";
            this.btnBajar.Size = new System.Drawing.Size(98, 57);
            this.btnBajar.TabIndex = 5;
            this.btnBajar.Text = "Bajar";
            this.btnBajar.UseVisualStyleBackColor = true;
            this.btnBajar.Click += new System.EventHandler(this.btnBajar_Click);
            // 
            // btnReportePdf
            // 
            this.btnReportePdf.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReportePdf.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnReportePdf.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnReportePdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportePdf.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReportePdf.Location = new System.Drawing.Point(330, 10);
            this.btnReportePdf.Name = "btnReportePdf";
            this.btnReportePdf.Size = new System.Drawing.Size(209, 57);
            this.btnReportePdf.TabIndex = 4;
            this.btnReportePdf.Text = "Generar Reporte PDF";
            this.btnReportePdf.UseVisualStyleBackColor = true;
            this.btnReportePdf.Click += new System.EventHandler(this.btnReportePdf_Click);
            // 
            // textFechaFin
            // 
            this.textFechaFin.AutoSize = true;
            this.textFechaFin.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textFechaFin.Location = new System.Drawing.Point(222, 10);
            this.textFechaFin.Name = "textFechaFin";
            this.textFechaFin.Size = new System.Drawing.Size(25, 15);
            this.textFechaFin.TabIndex = 3;
            this.textFechaFin.Text = "Fin";
            // 
            // textFechaInicio
            // 
            this.textFechaInicio.AutoSize = true;
            this.textFechaInicio.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textFechaInicio.Location = new System.Drawing.Point(58, 10);
            this.textFechaInicio.Name = "textFechaInicio";
            this.textFechaInicio.Size = new System.Drawing.Size(40, 15);
            this.textFechaInicio.TabIndex = 2;
            this.textFechaInicio.Text = "Inicio";
            // 
            // dateTimePickerEnd
            // 
            this.dateTimePickerEnd.CalendarFont = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerEnd.CalendarTitleBackColor = System.Drawing.Color.DodgerBlue;
            this.dateTimePickerEnd.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerEnd.Location = new System.Drawing.Point(172, 41);
            this.dateTimePickerEnd.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            this.dateTimePickerEnd.Size = new System.Drawing.Size(135, 26);
            this.dateTimePickerEnd.TabIndex = 1;
            this.dateTimePickerEnd.ValueChanged += new System.EventHandler(this.dateTimePickerEnd_ValueChanged);
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.CalendarFont = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerStart.CalendarTitleBackColor = System.Drawing.Color.DodgerBlue;
            this.dateTimePickerStart.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerStart.Location = new System.Drawing.Point(18, 41);
            this.dateTimePickerStart.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.Size = new System.Drawing.Size(137, 26);
            this.dateTimePickerStart.TabIndex = 0;
            this.dateTimePickerStart.ValueChanged += new System.EventHandler(this.dateTimePickerStart_ValueChanged);
            // 
            // tabControlResumenDocumentos
            // 
            this.tabControlResumenDocumentos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlResumenDocumentos.Controls.Add(this.tabPage1);
            this.tabControlResumenDocumentos.Font = new System.Drawing.Font("Roboto Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlResumenDocumentos.Location = new System.Drawing.Point(3, 85);
            this.tabControlResumenDocumentos.Name = "tabControlResumenDocumentos";
            this.tabControlResumenDocumentos.SelectedIndex = 0;
            this.tabControlResumenDocumentos.Size = new System.Drawing.Size(965, 447);
            this.tabControlResumenDocumentos.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textTotalDocumentos);
            this.tabPage1.Controls.Add(this.imgSinDatosFrmResumenDocumentos);
            this.tabPage1.Controls.Add(this.cbxDocumentosSeleccionarTodos);
            this.tabPage1.Controls.Add(this.pnlDocumentosTop);
            this.tabPage1.Controls.Add(this.dataGridDoc);
            this.tabPage1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(957, 415);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Resumen Documentos";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textTotalDocumentos
            // 
            this.textTotalDocumentos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textTotalDocumentos.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTotalDocumentos.Location = new System.Drawing.Point(7, 57);
            this.textTotalDocumentos.Name = "textTotalDocumentos";
            this.textTotalDocumentos.Size = new System.Drawing.Size(947, 23);
            this.textTotalDocumentos.TabIndex = 4;
            this.textTotalDocumentos.Text = "Documentos";
            this.textTotalDocumentos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imgSinDatosFrmResumenDocumentos
            // 
            this.imgSinDatosFrmResumenDocumentos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imgSinDatosFrmResumenDocumentos.Location = new System.Drawing.Point(431, 146);
            this.imgSinDatosFrmResumenDocumentos.Name = "imgSinDatosFrmResumenDocumentos";
            this.imgSinDatosFrmResumenDocumentos.Size = new System.Drawing.Size(99, 180);
            this.imgSinDatosFrmResumenDocumentos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgSinDatosFrmResumenDocumentos.TabIndex = 3;
            this.imgSinDatosFrmResumenDocumentos.TabStop = false;
            // 
            // cbxDocumentosSeleccionarTodos
            // 
            this.cbxDocumentosSeleccionarTodos.AutoSize = true;
            this.cbxDocumentosSeleccionarTodos.Location = new System.Drawing.Point(7, 94);
            this.cbxDocumentosSeleccionarTodos.Name = "cbxDocumentosSeleccionarTodos";
            this.cbxDocumentosSeleccionarTodos.Size = new System.Drawing.Size(15, 14);
            this.cbxDocumentosSeleccionarTodos.TabIndex = 2;
            this.cbxDocumentosSeleccionarTodos.UseVisualStyleBackColor = true;
            this.cbxDocumentosSeleccionarTodos.CheckedChanged += new System.EventHandler(this.cbxDocumentosSeleccionarTodos_CheckedChanged);
            // 
            // pnlDocumentosTop
            // 
            this.pnlDocumentosTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDocumentosTop.BackColor = System.Drawing.Color.NavajoWhite;
            this.pnlDocumentosTop.Controls.Add(this.textBuscar);
            this.pnlDocumentosTop.Controls.Add(this.editBuscarDocumentos);
            this.pnlDocumentosTop.Controls.Add(this.cmbDocumentosMostrar);
            this.pnlDocumentosTop.Controls.Add(this.lblDocumentosMostrar);
            this.pnlDocumentosTop.Location = new System.Drawing.Point(7, 6);
            this.pnlDocumentosTop.Name = "pnlDocumentosTop";
            this.pnlDocumentosTop.Size = new System.Drawing.Size(944, 48);
            this.pnlDocumentosTop.TabIndex = 1;
            // 
            // textBuscar
            // 
            this.textBuscar.AutoSize = true;
            this.textBuscar.Font = new System.Drawing.Font("Roboto Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBuscar.Location = new System.Drawing.Point(3, 18);
            this.textBuscar.Name = "textBuscar";
            this.textBuscar.Size = new System.Drawing.Size(70, 23);
            this.textBuscar.TabIndex = 3;
            this.textBuscar.Text = "Buscar";
            // 
            // editBuscarDocumentos
            // 
            this.editBuscarDocumentos.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editBuscarDocumentos.Location = new System.Drawing.Point(79, 18);
            this.editBuscarDocumentos.Name = "editBuscarDocumentos";
            this.editBuscarDocumentos.Size = new System.Drawing.Size(299, 23);
            this.editBuscarDocumentos.TabIndex = 2;
            this.editBuscarDocumentos.TextChanged += new System.EventHandler(this.editBuscarDocumentos_TextChanged);
            // 
            // cmbDocumentosMostrar
            // 
            this.cmbDocumentosMostrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDocumentosMostrar.BackColor = System.Drawing.Color.FloralWhite;
            this.cmbDocumentosMostrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbDocumentosMostrar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDocumentosMostrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbDocumentosMostrar.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDocumentosMostrar.FormattingEnabled = true;
            this.cmbDocumentosMostrar.Location = new System.Drawing.Point(668, 11);
            this.cmbDocumentosMostrar.Name = "cmbDocumentosMostrar";
            this.cmbDocumentosMostrar.Size = new System.Drawing.Size(273, 27);
            this.cmbDocumentosMostrar.TabIndex = 1;
            this.cmbDocumentosMostrar.SelectedIndexChanged += new System.EventHandler(this.cmbDocumentosMostrar_SelectedIndexChanged);
            // 
            // lblDocumentosMostrar
            // 
            this.lblDocumentosMostrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDocumentosMostrar.AutoSize = true;
            this.lblDocumentosMostrar.Font = new System.Drawing.Font("Roboto Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocumentosMostrar.Location = new System.Drawing.Point(537, 15);
            this.lblDocumentosMostrar.Name = "lblDocumentosMostrar";
            this.lblDocumentosMostrar.Size = new System.Drawing.Size(125, 23);
            this.lblDocumentosMostrar.TabIndex = 0;
            this.lblDocumentosMostrar.Text = "Documentos:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Coral;
            this.panel1.Controls.Add(this.btnCancelDocuments);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(974, 70);
            this.panel1.TabIndex = 1;
            // 
            // btnCancelDocuments
            // 
            this.btnCancelDocuments.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelDocuments.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnCancelDocuments.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(20)))), ((int)(((byte)(224)))));
            this.btnCancelDocuments.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnCancelDocuments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelDocuments.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelDocuments.ForeColor = System.Drawing.Color.White;
            this.btnCancelDocuments.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCancelDocuments.Location = new System.Drawing.Point(175, 3);
            this.btnCancelDocuments.Name = "btnCancelDocuments";
            this.btnCancelDocuments.Size = new System.Drawing.Size(75, 61);
            this.btnCancelDocuments.TabIndex = 8;
            this.btnCancelDocuments.Text = "Cancelar";
            this.btnCancelDocuments.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelDocuments.UseVisualStyleBackColor = true;
            this.btnCancelDocuments.Click += new System.EventHandler(this.btnCancelDocuments_Click);
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(20)))), ((int)(((byte)(224)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(84, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 61);
            this.button1.TabIndex = 7;
            this.button1.Text = "Actualizar";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.PictureUpdate_Click);
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(20)))), ((int)(((byte)(224)))));
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Image = global::SyncTPV.Properties.Resources.close;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.Location = new System.Drawing.Point(3, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 61);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Cerrar";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.pictureCerrar_Click);
            // 
            // timerBuscarDocumentos
            // 
            this.timerBuscarDocumentos.Interval = 300;
            this.timerBuscarDocumentos.Tick += new System.EventHandler(this.timerBuscarDocumentos_Tick);
            // 
            // FrmResumenDocuments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(974, 611);
            this.Controls.Add(this.pnlContent);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmResumenDocuments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Resumen Documentos";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmResumenDocuments_FormClosed);
            this.Load += new System.EventHandler(this.FrmResumenDoc_Load);
            this.Resize += new System.EventHandler(this.FrmResumenDocuments_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDoc)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.panelTabResumenDocuments.ResumeLayout(false);
            this.panelFiltros.ResumeLayout(false);
            this.panelFiltros.PerformLayout();
            this.tabControlResumenDocumentos.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatosFrmResumenDocumentos)).EndInit();
            this.pnlDocumentosTop.ResumeLayout(false);
            this.pnlDocumentosTop.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridDoc;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelTabResumenDocuments;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TabControl tabControlResumenDocumentos;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel pnlDocumentosTop;
        private System.Windows.Forms.ComboBox cmbDocumentosMostrar;
        private System.Windows.Forms.Label lblDocumentosMostrar;
        private System.Windows.Forms.CheckBox cbxDocumentosSeleccionarTodos;
        private System.Windows.Forms.PictureBox imgSinDatosFrmResumenDocumentos;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn fventa;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaHora;
        private System.Windows.Forms.DataGridViewTextBoxColumn total;
        private System.Windows.Forms.DataGridViewImageColumn status;
        private System.Windows.Forms.Label textTotalDocumentos;
        private System.Windows.Forms.Button btnCancelDocuments;
        private System.Windows.Forms.Label textBuscar;
        private System.Windows.Forms.TextBox editBuscarDocumentos;
        private System.Windows.Forms.Panel panelFiltros;
        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.Label textFechaFin;
        private System.Windows.Forms.Label textFechaInicio;
        private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
        private System.Windows.Forms.Button btnReportePdf;
        private System.Windows.Forms.Button btnBajar;
        private System.Windows.Forms.Button btnSubir;
        private System.Windows.Forms.Timer timerBuscarDocumentos;
    }
}