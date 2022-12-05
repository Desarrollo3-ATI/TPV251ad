namespace SyncTPV
{
    partial class FormMovimientos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMovimientos));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.btnimprimirpuro = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnFactura = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelMovimientos = new System.Windows.Forms.Panel();
            this.panelFootbar = new System.Windows.Forms.Panel();
            this.textTotalDocumento = new System.Windows.Forms.Label();
            this.textDescuentoDocumento = new System.Windows.Forms.Label();
            this.textSubtotalDocumento = new System.Windows.Forms.Label();
            this.editDescuentoDocumento = new System.Windows.Forms.TextBox();
            this.editSubtotalDocumento = new System.Windows.Forms.TextBox();
            this.editTotalDocumento = new System.Windows.Forms.TextBox();
            this.textTotalMovmientos = new System.Windows.Forms.Label();
            this.imgSinDatos = new System.Windows.Forms.PictureBox();
            this.dataGridMovs = new System.Windows.Forms.DataGridView();
            this.idMovDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemIdMovDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.capturedUnitsMovDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.capturedUnitIdMovDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceMovDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subtotalMovDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.discountMovDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalMovDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.observationDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelDatosDocumento = new System.Windows.Forms.Panel();
            this.editFolioDocumento = new System.Windows.Forms.TextBox();
            this.textStatus = new System.Windows.Forms.Label();
            this.textObservationDoc = new System.Windows.Forms.Label();
            this.editObservationDoc = new System.Windows.Forms.TextBox();
            this.editCliente = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.textDatosDocumentoMovimientosDocs = new System.Windows.Forms.Label();
            this.pnlLeft.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelMovimientos.SuspendLayout();
            this.panelFootbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMovs)).BeginInit();
            this.panelDatosDocumento.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlRight
            // 
            this.pnlRight.Location = new System.Drawing.Point(668, 29);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(30, 280);
            this.pnlRight.TabIndex = 18;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.btnimprimirpuro);
            this.pnlLeft.Controls.Add(this.btnImprimir);
            this.pnlLeft.Controls.Add(this.btnEditar);
            this.pnlLeft.Controls.Add(this.btnCancelar);
            this.pnlLeft.Controls.Add(this.btnFactura);
            this.pnlLeft.Controls.Add(this.panelContent);
            this.pnlLeft.Controls.Add(this.btnClose);
            this.pnlLeft.Controls.Add(this.textDatosDocumentoMovimientosDocs);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(1064, 543);
            this.pnlLeft.TabIndex = 20;
            // 
            // btnimprimirpuro
            // 
            this.btnimprimirpuro.BackColor = System.Drawing.Color.Coral;
            this.btnimprimirpuro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnimprimirpuro.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnimprimirpuro.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnimprimirpuro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnimprimirpuro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnimprimirpuro.ForeColor = System.Drawing.Color.White;
            this.btnimprimirpuro.Location = new System.Drawing.Point(344, 3);
            this.btnimprimirpuro.Name = "btnimprimirpuro";
            this.btnimprimirpuro.Size = new System.Drawing.Size(83, 68);
            this.btnimprimirpuro.TabIndex = 9;
            this.btnimprimirpuro.Text = "Imprimir";
            this.btnimprimirpuro.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnimprimirpuro.UseVisualStyleBackColor = false;
            this.btnimprimirpuro.Click += new System.EventHandler(this.btnimprimirpuro_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.BackColor = System.Drawing.Color.Coral;
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnImprimir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.ForeColor = System.Drawing.Color.White;
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnImprimir.Location = new System.Drawing.Point(165, 3);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(87, 68);
            this.btnImprimir.TabIndex = 8;
            this.btnImprimir.Text = "Reimprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.Coral;
            this.btnEditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditar.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnEditar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.ForeColor = System.Drawing.Color.White;
            this.btnEditar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEditar.Location = new System.Drawing.Point(84, 3);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(75, 68);
            this.btnEditar.TabIndex = 7;
            this.btnEditar.Text = "Modificar";
            this.btnEditar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.BackColor = System.Drawing.Color.Coral;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCancelar.Location = new System.Drawing.Point(986, 3);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 68);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnFactura
            // 
            this.btnFactura.BackColor = System.Drawing.Color.Coral;
            this.btnFactura.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFactura.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnFactura.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnFactura.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFactura.ForeColor = System.Drawing.Color.White;
            this.btnFactura.Location = new System.Drawing.Point(256, 3);
            this.btnFactura.Name = "btnFactura";
            this.btnFactura.Size = new System.Drawing.Size(83, 68);
            this.btnFactura.TabIndex = 5;
            this.btnFactura.Text = "Factura";
            this.btnFactura.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnFactura.UseVisualStyleBackColor = false;
            this.btnFactura.Click += new System.EventHandler(this.btnFactura_Click);
            // 
            // panelContent
            // 
            this.panelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContent.Controls.Add(this.panelMovimientos);
            this.panelContent.Controls.Add(this.panelDatosDocumento);
            this.panelContent.Location = new System.Drawing.Point(0, 77);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1064, 466);
            this.panelContent.TabIndex = 4;
            // 
            // panelMovimientos
            // 
            this.panelMovimientos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMovimientos.Controls.Add(this.panelFootbar);
            this.panelMovimientos.Controls.Add(this.textTotalMovmientos);
            this.panelMovimientos.Controls.Add(this.imgSinDatos);
            this.panelMovimientos.Controls.Add(this.dataGridMovs);
            this.panelMovimientos.Location = new System.Drawing.Point(0, 129);
            this.panelMovimientos.Name = "panelMovimientos";
            this.panelMovimientos.Size = new System.Drawing.Size(1064, 337);
            this.panelMovimientos.TabIndex = 1;
            // 
            // panelFootbar
            // 
            this.panelFootbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFootbar.Controls.Add(this.textTotalDocumento);
            this.panelFootbar.Controls.Add(this.textDescuentoDocumento);
            this.panelFootbar.Controls.Add(this.textSubtotalDocumento);
            this.panelFootbar.Controls.Add(this.editDescuentoDocumento);
            this.panelFootbar.Controls.Add(this.editSubtotalDocumento);
            this.panelFootbar.Controls.Add(this.editTotalDocumento);
            this.panelFootbar.Location = new System.Drawing.Point(12, 272);
            this.panelFootbar.Name = "panelFootbar";
            this.panelFootbar.Size = new System.Drawing.Size(1040, 62);
            this.panelFootbar.TabIndex = 6;
            // 
            // textTotalDocumento
            // 
            this.textTotalDocumento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textTotalDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTotalDocumento.Location = new System.Drawing.Point(847, 8);
            this.textTotalDocumento.Name = "textTotalDocumento";
            this.textTotalDocumento.Size = new System.Drawing.Size(190, 22);
            this.textTotalDocumento.TabIndex = 7;
            this.textTotalDocumento.Text = "Total";
            this.textTotalDocumento.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textDescuentoDocumento
            // 
            this.textDescuentoDocumento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textDescuentoDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textDescuentoDocumento.Location = new System.Drawing.Point(648, 8);
            this.textDescuentoDocumento.Name = "textDescuentoDocumento";
            this.textDescuentoDocumento.Size = new System.Drawing.Size(193, 22);
            this.textDescuentoDocumento.TabIndex = 6;
            this.textDescuentoDocumento.Text = "Descuento";
            this.textDescuentoDocumento.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textSubtotalDocumento
            // 
            this.textSubtotalDocumento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textSubtotalDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textSubtotalDocumento.Location = new System.Drawing.Point(443, 8);
            this.textSubtotalDocumento.Name = "textSubtotalDocumento";
            this.textSubtotalDocumento.Size = new System.Drawing.Size(199, 22);
            this.textSubtotalDocumento.TabIndex = 5;
            this.textSubtotalDocumento.Text = "Subtotal";
            this.textSubtotalDocumento.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // editDescuentoDocumento
            // 
            this.editDescuentoDocumento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.editDescuentoDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editDescuentoDocumento.Location = new System.Drawing.Point(648, 33);
            this.editDescuentoDocumento.Name = "editDescuentoDocumento";
            this.editDescuentoDocumento.Size = new System.Drawing.Size(193, 21);
            this.editDescuentoDocumento.TabIndex = 2;
            this.editDescuentoDocumento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // editSubtotalDocumento
            // 
            this.editSubtotalDocumento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.editSubtotalDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editSubtotalDocumento.Location = new System.Drawing.Point(443, 33);
            this.editSubtotalDocumento.Name = "editSubtotalDocumento";
            this.editSubtotalDocumento.Size = new System.Drawing.Size(199, 21);
            this.editSubtotalDocumento.TabIndex = 1;
            this.editSubtotalDocumento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // editTotalDocumento
            // 
            this.editTotalDocumento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.editTotalDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editTotalDocumento.Location = new System.Drawing.Point(847, 33);
            this.editTotalDocumento.Name = "editTotalDocumento";
            this.editTotalDocumento.Size = new System.Drawing.Size(190, 21);
            this.editTotalDocumento.TabIndex = 0;
            this.editTotalDocumento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textTotalMovmientos
            // 
            this.textTotalMovmientos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textTotalMovmientos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTotalMovmientos.Location = new System.Drawing.Point(12, 0);
            this.textTotalMovmientos.Name = "textTotalMovmientos";
            this.textTotalMovmientos.Size = new System.Drawing.Size(1040, 23);
            this.textTotalMovmientos.TabIndex = 5;
            this.textTotalMovmientos.Text = "Movimientos";
            this.textTotalMovmientos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.textTotalMovmientos.Click += new System.EventHandler(this.textTotalMovmientos_Click);
            // 
            // imgSinDatos
            // 
            this.imgSinDatos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.imgSinDatos.Image = ((System.Drawing.Image)(resources.GetObject("imgSinDatos.Image")));
            this.imgSinDatos.Location = new System.Drawing.Point(388, 45);
            this.imgSinDatos.Name = "imgSinDatos";
            this.imgSinDatos.Size = new System.Drawing.Size(296, 182);
            this.imgSinDatos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgSinDatos.TabIndex = 4;
            this.imgSinDatos.TabStop = false;
            // 
            // dataGridMovs
            // 
            this.dataGridMovs.AllowUserToAddRows = false;
            this.dataGridMovs.AllowUserToDeleteRows = false;
            this.dataGridMovs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridMovs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridMovs.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridMovs.BackgroundColor = System.Drawing.Color.Azure;
            this.dataGridMovs.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Coral;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridMovs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridMovs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridMovs.ColumnHeadersVisible = false;
            this.dataGridMovs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idMovDoc,
            this.itemIdMovDoc,
            this.capturedUnitsMovDoc,
            this.capturedUnitIdMovDoc,
            this.priceMovDoc,
            this.subtotalMovDoc,
            this.discountMovDoc,
            this.totalMovDoc,
            this.observationDgv});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Calibri", 10F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridMovs.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridMovs.EnableHeadersVisualStyles = false;
            this.dataGridMovs.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.dataGridMovs.Location = new System.Drawing.Point(12, 26);
            this.dataGridMovs.MultiSelect = false;
            this.dataGridMovs.Name = "dataGridMovs";
            this.dataGridMovs.ReadOnly = true;
            this.dataGridMovs.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Calibri", 10F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Crimson;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridMovs.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridMovs.RowHeadersVisible = false;
            this.dataGridMovs.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridMovs.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridMovs.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(2);
            this.dataGridMovs.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridMovs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridMovs.Size = new System.Drawing.Size(1040, 240);
            this.dataGridMovs.TabIndex = 3;
            // 
            // idMovDoc
            // 
            this.idMovDoc.HeaderText = "Id";
            this.idMovDoc.Name = "idMovDoc";
            this.idMovDoc.ReadOnly = true;
            // 
            // itemIdMovDoc
            // 
            this.itemIdMovDoc.HeaderText = "Producto o Servicio";
            this.itemIdMovDoc.Name = "itemIdMovDoc";
            this.itemIdMovDoc.ReadOnly = true;
            // 
            // capturedUnitsMovDoc
            // 
            this.capturedUnitsMovDoc.HeaderText = "Cantidad";
            this.capturedUnitsMovDoc.Name = "capturedUnitsMovDoc";
            this.capturedUnitsMovDoc.ReadOnly = true;
            // 
            // capturedUnitIdMovDoc
            // 
            this.capturedUnitIdMovDoc.HeaderText = "Unidad";
            this.capturedUnitIdMovDoc.Name = "capturedUnitIdMovDoc";
            this.capturedUnitIdMovDoc.ReadOnly = true;
            // 
            // priceMovDoc
            // 
            this.priceMovDoc.HeaderText = "Precio";
            this.priceMovDoc.Name = "priceMovDoc";
            this.priceMovDoc.ReadOnly = true;
            // 
            // subtotalMovDoc
            // 
            this.subtotalMovDoc.HeaderText = "Subtotal";
            this.subtotalMovDoc.Name = "subtotalMovDoc";
            this.subtotalMovDoc.ReadOnly = true;
            // 
            // discountMovDoc
            // 
            this.discountMovDoc.HeaderText = "Descuento";
            this.discountMovDoc.Name = "discountMovDoc";
            this.discountMovDoc.ReadOnly = true;
            // 
            // totalMovDoc
            // 
            this.totalMovDoc.HeaderText = "Total";
            this.totalMovDoc.Name = "totalMovDoc";
            this.totalMovDoc.ReadOnly = true;
            // 
            // observationDgv
            // 
            this.observationDgv.HeaderText = "Observación";
            this.observationDgv.Name = "observationDgv";
            this.observationDgv.ReadOnly = true;
            // 
            // panelDatosDocumento
            // 
            this.panelDatosDocumento.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDatosDocumento.Controls.Add(this.editFolioDocumento);
            this.panelDatosDocumento.Controls.Add(this.textStatus);
            this.panelDatosDocumento.Controls.Add(this.textObservationDoc);
            this.panelDatosDocumento.Controls.Add(this.editObservationDoc);
            this.panelDatosDocumento.Controls.Add(this.editCliente);
            this.panelDatosDocumento.Location = new System.Drawing.Point(0, 3);
            this.panelDatosDocumento.Name = "panelDatosDocumento";
            this.panelDatosDocumento.Size = new System.Drawing.Size(1064, 120);
            this.panelDatosDocumento.TabIndex = 0;
            // 
            // editFolioDocumento
            // 
            this.editFolioDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editFolioDocumento.Location = new System.Drawing.Point(12, 62);
            this.editFolioDocumento.Name = "editFolioDocumento";
            this.editFolioDocumento.Size = new System.Drawing.Size(240, 22);
            this.editFolioDocumento.TabIndex = 4;
            this.editFolioDocumento.Text = "Folio";
            // 
            // textStatus
            // 
            this.textStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textStatus.Location = new System.Drawing.Point(845, 4);
            this.textStatus.Name = "textStatus";
            this.textStatus.Size = new System.Drawing.Size(216, 23);
            this.textStatus.TabIndex = 3;
            this.textStatus.Text = "Status";
            this.textStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textObservationDoc
            // 
            this.textObservationDoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textObservationDoc.AutoSize = true;
            this.textObservationDoc.Location = new System.Drawing.Point(765, 48);
            this.textObservationDoc.Name = "textObservationDoc";
            this.textObservationDoc.Size = new System.Drawing.Size(142, 13);
            this.textObservationDoc.TabIndex = 2;
            this.textObservationDoc.Text = "Observación del Documento";
            // 
            // editObservationDoc
            // 
            this.editObservationDoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.editObservationDoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editObservationDoc.Location = new System.Drawing.Point(768, 64);
            this.editObservationDoc.Multiline = true;
            this.editObservationDoc.Name = "editObservationDoc";
            this.editObservationDoc.Size = new System.Drawing.Size(293, 53);
            this.editObservationDoc.TabIndex = 1;
            // 
            // editCliente
            // 
            this.editCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editCliente.Location = new System.Drawing.Point(12, 14);
            this.editCliente.Name = "editCliente";
            this.editCliente.Size = new System.Drawing.Size(354, 22);
            this.editCliente.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Coral;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Image = global::SyncTPV.Properties.Resources.close;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.Location = new System.Drawing.Point(3, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 68);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Cerrar";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // textDatosDocumentoMovimientosDocs
            // 
            this.textDatosDocumentoMovimientosDocs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textDatosDocumentoMovimientosDocs.BackColor = System.Drawing.Color.Coral;
            this.textDatosDocumentoMovimientosDocs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textDatosDocumentoMovimientosDocs.ForeColor = System.Drawing.Color.White;
            this.textDatosDocumentoMovimientosDocs.Location = new System.Drawing.Point(0, 0);
            this.textDatosDocumentoMovimientosDocs.Name = "textDatosDocumentoMovimientosDocs";
            this.textDatosDocumentoMovimientosDocs.Size = new System.Drawing.Size(1064, 74);
            this.textDatosDocumentoMovimientosDocs.TabIndex = 1;
            this.textDatosDocumentoMovimientosDocs.Text = "Datos Del Documento";
            this.textDatosDocumentoMovimientosDocs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormMovimientos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(1064, 543);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlRight);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormMovimientos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Movimientos del documento";
            this.Load += new System.EventHandler(this.FrmEstadoDoc_Load);
            this.pnlLeft.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelMovimientos.ResumeLayout(false);
            this.panelFootbar.ResumeLayout(false);
            this.panelFootbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMovs)).EndInit();
            this.panelDatosDocumento.ResumeLayout(false);
            this.panelDatosDocumento.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Label textDatosDocumentoMovimientosDocs;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelMovimientos;
        private System.Windows.Forms.PictureBox imgSinDatos;
        private System.Windows.Forms.DataGridView dataGridMovs;
        private System.Windows.Forms.DataGridViewTextBoxColumn idMovDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemIdMovDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn capturedUnitsMovDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn capturedUnitIdMovDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceMovDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn subtotalMovDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn discountMovDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalMovDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn observationDgv;
        private System.Windows.Forms.Panel panelDatosDocumento;
        private System.Windows.Forms.Label textTotalMovmientos;
        private System.Windows.Forms.TextBox editCliente;
        private System.Windows.Forms.TextBox editObservationDoc;
        private System.Windows.Forms.Label textObservationDoc;
        private System.Windows.Forms.Button btnFactura;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Label textStatus;
        private System.Windows.Forms.TextBox editFolioDocumento;
        private System.Windows.Forms.Panel panelFootbar;
        private System.Windows.Forms.Label textTotalDocumento;
        private System.Windows.Forms.Label textDescuentoDocumento;
        private System.Windows.Forms.Label textSubtotalDocumento;
        private System.Windows.Forms.TextBox editDescuentoDocumento;
        private System.Windows.Forms.TextBox editSubtotalDocumento;
        private System.Windows.Forms.TextBox editTotalDocumento;
        private System.Windows.Forms.Button btnimprimirpuro;
    }
}