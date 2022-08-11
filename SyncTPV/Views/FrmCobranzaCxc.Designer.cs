
namespace SyncTPV.Views
{
    partial class FrmCobranzaCxc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCobranzaCxc));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelTituloFrmCobranza = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnUpdateCxc = new System.Windows.Forms.Button();
            this.textNombreClienteFrmCobranza = new System.Windows.Forms.Label();
            this.panelDataGridFrmCobranza = new System.Windows.Forms.Panel();
            this.btnVerPagos = new System.Windows.Forms.Button();
            this.textTotalCredits = new System.Windows.Forms.Label();
            this.imgSinDatos = new System.Windows.Forms.PictureBox();
            this.dataGridViewCreditsDocuments = new System.Windows.Forms.DataGridView();
            this.idDgvCxc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idCxcDgvCxc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.folioDgvCxc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diasAtrasadosDgvCxc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saldoActualDgvCxc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaVencimientoDgvCxc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.conceptoDgvCxc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelAbonarLiquidar = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.imgSinDatosMovimientos = new System.Windows.Forms.PictureBox();
            this.textInfoMovimientos = new System.Windows.Forms.Label();
            this.dataGridViewMovimientosCxc = new System.Windows.Forms.DataGridView();
            this.idMoveCxcDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idDocumentoMoveCxcDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemMoveCxcDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numberMoveCxcDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceMoveCxcDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.capturedUnitsMoveCxcDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.capturedUnitIdMoveCxcDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subtotalMoveCxcDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.discountMoveCxcDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalMoveCxcDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelAbonar = new System.Windows.Forms.Panel();
            this.textReferenciaAbonoFiniquitar = new System.Windows.Forms.Label();
            this.textInfoSeleccionarFormaDeCobro = new System.Windows.Forms.Label();
            this.comboBoxFormasDeCobro = new System.Windows.Forms.ComboBox();
            this.textPendienteDocument = new System.Windows.Forms.Label();
            this.textCurrency = new System.Windows.Forms.Label();
            this.textImporteAbono = new System.Windows.Forms.Label();
            this.textRealizarAbono = new System.Windows.Forms.Label();
            this.editImporteAbono = new System.Windows.Forms.TextBox();
            this.textCreditoSeleccionado = new System.Windows.Forms.Label();
            this.btnLiquidarCredito = new SyncTPV.RoundedButton();
            this.btnRealizarAbono = new SyncTPV.RoundedButton();
            this.panelTituloFrmCobranza.SuspendLayout();
            this.panelDataGridFrmCobranza.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCreditsDocuments)).BeginInit();
            this.panelAbonarLiquidar.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatosMovimientos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMovimientosCxc)).BeginInit();
            this.panelAbonar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTituloFrmCobranza
            // 
            this.panelTituloFrmCobranza.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTituloFrmCobranza.BackColor = System.Drawing.Color.Coral;
            this.panelTituloFrmCobranza.Controls.Add(this.btnClose);
            this.panelTituloFrmCobranza.Controls.Add(this.btnUpdateCxc);
            this.panelTituloFrmCobranza.Controls.Add(this.textNombreClienteFrmCobranza);
            this.panelTituloFrmCobranza.Location = new System.Drawing.Point(-2, -1);
            this.panelTituloFrmCobranza.Name = "panelTituloFrmCobranza";
            this.panelTituloFrmCobranza.Size = new System.Drawing.Size(1114, 76);
            this.panelTituloFrmCobranza.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Image = global::SyncTPV.Properties.Resources.close;
            this.btnClose.Location = new System.Drawing.Point(3, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 66);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Cerrar";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnUpdateCxc
            // 
            this.btnUpdateCxc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateCxc.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnUpdateCxc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateCxc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateCxc.ForeColor = System.Drawing.Color.White;
            this.btnUpdateCxc.Image = global::SyncTPV.Properties.Resources.update;
            this.btnUpdateCxc.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnUpdateCxc.Location = new System.Drawing.Point(1011, 3);
            this.btnUpdateCxc.Name = "btnUpdateCxc";
            this.btnUpdateCxc.Size = new System.Drawing.Size(88, 70);
            this.btnUpdateCxc.TabIndex = 1;
            this.btnUpdateCxc.Text = "Actualizar";
            this.btnUpdateCxc.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnUpdateCxc.UseVisualStyleBackColor = true;
            this.btnUpdateCxc.Click += new System.EventHandler(this.btnUpdateCxc_Click);
            // 
            // textNombreClienteFrmCobranza
            // 
            this.textNombreClienteFrmCobranza.AutoSize = true;
            this.textNombreClienteFrmCobranza.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textNombreClienteFrmCobranza.ForeColor = System.Drawing.Color.White;
            this.textNombreClienteFrmCobranza.Location = new System.Drawing.Point(154, 26);
            this.textNombreClienteFrmCobranza.Name = "textNombreClienteFrmCobranza";
            this.textNombreClienteFrmCobranza.Size = new System.Drawing.Size(65, 20);
            this.textNombreClienteFrmCobranza.TabIndex = 0;
            this.textNombreClienteFrmCobranza.Text = "Cliente";
            // 
            // panelDataGridFrmCobranza
            // 
            this.panelDataGridFrmCobranza.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDataGridFrmCobranza.Controls.Add(this.btnVerPagos);
            this.panelDataGridFrmCobranza.Controls.Add(this.textTotalCredits);
            this.panelDataGridFrmCobranza.Controls.Add(this.imgSinDatos);
            this.panelDataGridFrmCobranza.Controls.Add(this.dataGridViewCreditsDocuments);
            this.panelDataGridFrmCobranza.Location = new System.Drawing.Point(12, 81);
            this.panelDataGridFrmCobranza.Name = "panelDataGridFrmCobranza";
            this.panelDataGridFrmCobranza.Size = new System.Drawing.Size(477, 546);
            this.panelDataGridFrmCobranza.TabIndex = 1;
            // 
            // btnVerPagos
            // 
            this.btnVerPagos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnVerPagos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerPagos.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnVerPagos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnVerPagos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerPagos.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerPagos.Location = new System.Drawing.Point(3, 496);
            this.btnVerPagos.Name = "btnVerPagos";
            this.btnVerPagos.Size = new System.Drawing.Size(121, 45);
            this.btnVerPagos.TabIndex = 3;
            this.btnVerPagos.Text = "Pagos";
            this.btnVerPagos.UseVisualStyleBackColor = true;
            this.btnVerPagos.Click += new System.EventHandler(this.btnVerPagos_Click);
            // 
            // textTotalCredits
            // 
            this.textTotalCredits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textTotalCredits.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTotalCredits.Location = new System.Drawing.Point(16, 13);
            this.textTotalCredits.Name = "textTotalCredits";
            this.textTotalCredits.Size = new System.Drawing.Size(439, 25);
            this.textTotalCredits.TabIndex = 2;
            this.textTotalCredits.Text = "Créditos";
            this.textTotalCredits.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imgSinDatos
            // 
            this.imgSinDatos.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.imgSinDatos.Image = ((System.Drawing.Image)(resources.GetObject("imgSinDatos.Image")));
            this.imgSinDatos.Location = new System.Drawing.Point(105, 154);
            this.imgSinDatos.Name = "imgSinDatos";
            this.imgSinDatos.Size = new System.Drawing.Size(247, 198);
            this.imgSinDatos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgSinDatos.TabIndex = 1;
            this.imgSinDatos.TabStop = false;
            // 
            // dataGridViewCreditsDocuments
            // 
            this.dataGridViewCreditsDocuments.AllowUserToAddRows = false;
            this.dataGridViewCreditsDocuments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewCreditsDocuments.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewCreditsDocuments.BackgroundColor = System.Drawing.Color.FloralWhite;
            this.dataGridViewCreditsDocuments.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCreditsDocuments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewCreditsDocuments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCreditsDocuments.ColumnHeadersVisible = false;
            this.dataGridViewCreditsDocuments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDgvCxc,
            this.idCxcDgvCxc,
            this.folioDgvCxc,
            this.diasAtrasadosDgvCxc,
            this.saldoActualDgvCxc,
            this.fechaVencimientoDgvCxc,
            this.conceptoDgvCxc});
            this.dataGridViewCreditsDocuments.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dataGridViewCreditsDocuments.GridColor = System.Drawing.Color.FloralWhite;
            this.dataGridViewCreditsDocuments.Location = new System.Drawing.Point(3, 57);
            this.dataGridViewCreditsDocuments.MultiSelect = false;
            this.dataGridViewCreditsDocuments.Name = "dataGridViewCreditsDocuments";
            this.dataGridViewCreditsDocuments.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.LightSkyBlue;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCreditsDocuments.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewCreditsDocuments.RowHeadersVisible = false;
            this.dataGridViewCreditsDocuments.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewCreditsDocuments.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewCreditsDocuments.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(1, 5, 1, 5);
            this.dataGridViewCreditsDocuments.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCreditsDocuments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCreditsDocuments.Size = new System.Drawing.Size(469, 433);
            this.dataGridViewCreditsDocuments.TabIndex = 0;
            this.dataGridViewCreditsDocuments.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCreditsDocuments_CellClick);
            this.dataGridViewCreditsDocuments.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewCreditsDocuments_CellMouseDoubleClick);
            this.dataGridViewCreditsDocuments.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridViewCreditsDocuments_Scroll);
            this.dataGridViewCreditsDocuments.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridViewCreditsDocuments_KeyUp);
            // 
            // idDgvCxc
            // 
            this.idDgvCxc.HeaderText = "Id";
            this.idDgvCxc.Name = "idDgvCxc";
            this.idDgvCxc.ReadOnly = true;
            this.idDgvCxc.Width = 62;
            // 
            // idCxcDgvCxc
            // 
            this.idCxcDgvCxc.HeaderText = "Id Credito";
            this.idCxcDgvCxc.Name = "idCxcDgvCxc";
            this.idCxcDgvCxc.ReadOnly = true;
            this.idCxcDgvCxc.Width = 63;
            // 
            // folioDgvCxc
            // 
            this.folioDgvCxc.HeaderText = "Folio";
            this.folioDgvCxc.Name = "folioDgvCxc";
            this.folioDgvCxc.ReadOnly = true;
            this.folioDgvCxc.Width = 62;
            // 
            // diasAtrasadosDgvCxc
            // 
            this.diasAtrasadosDgvCxc.HeaderText = "Días Atrasados";
            this.diasAtrasadosDgvCxc.Name = "diasAtrasadosDgvCxc";
            this.diasAtrasadosDgvCxc.ReadOnly = true;
            this.diasAtrasadosDgvCxc.Width = 62;
            // 
            // saldoActualDgvCxc
            // 
            this.saldoActualDgvCxc.HeaderText = "Saldo Actual";
            this.saldoActualDgvCxc.Name = "saldoActualDgvCxc";
            this.saldoActualDgvCxc.ReadOnly = true;
            this.saldoActualDgvCxc.Width = 62;
            // 
            // fechaVencimientoDgvCxc
            // 
            this.fechaVencimientoDgvCxc.HeaderText = "Fecha Vencimiento";
            this.fechaVencimientoDgvCxc.Name = "fechaVencimientoDgvCxc";
            this.fechaVencimientoDgvCxc.ReadOnly = true;
            this.fechaVencimientoDgvCxc.Width = 63;
            // 
            // conceptoDgvCxc
            // 
            this.conceptoDgvCxc.HeaderText = "Concepto";
            this.conceptoDgvCxc.Name = "conceptoDgvCxc";
            this.conceptoDgvCxc.ReadOnly = true;
            this.conceptoDgvCxc.Width = 62;
            // 
            // panelAbonarLiquidar
            // 
            this.panelAbonarLiquidar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAbonarLiquidar.AutoScroll = true;
            this.panelAbonarLiquidar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelAbonarLiquidar.Controls.Add(this.panel2);
            this.panelAbonarLiquidar.Controls.Add(this.panelAbonar);
            this.panelAbonarLiquidar.Controls.Add(this.textCreditoSeleccionado);
            this.panelAbonarLiquidar.Location = new System.Drawing.Point(490, 81);
            this.panelAbonarLiquidar.Name = "panelAbonarLiquidar";
            this.panelAbonarLiquidar.Size = new System.Drawing.Size(607, 546);
            this.panelAbonarLiquidar.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.imgSinDatosMovimientos);
            this.panel2.Controls.Add(this.textInfoMovimientos);
            this.panel2.Controls.Add(this.dataGridViewMovimientosCxc);
            this.panel2.Location = new System.Drawing.Point(6, 39);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(594, 257);
            this.panel2.TabIndex = 2;
            // 
            // imgSinDatosMovimientos
            // 
            this.imgSinDatosMovimientos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imgSinDatosMovimientos.Image = ((System.Drawing.Image)(resources.GetObject("imgSinDatosMovimientos.Image")));
            this.imgSinDatosMovimientos.Location = new System.Drawing.Point(156, 77);
            this.imgSinDatosMovimientos.Name = "imgSinDatosMovimientos";
            this.imgSinDatosMovimientos.Size = new System.Drawing.Size(271, 143);
            this.imgSinDatosMovimientos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgSinDatosMovimientos.TabIndex = 2;
            this.imgSinDatosMovimientos.TabStop = false;
            // 
            // textInfoMovimientos
            // 
            this.textInfoMovimientos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textInfoMovimientos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInfoMovimientos.Location = new System.Drawing.Point(3, 0);
            this.textInfoMovimientos.Name = "textInfoMovimientos";
            this.textInfoMovimientos.Size = new System.Drawing.Size(585, 40);
            this.textInfoMovimientos.TabIndex = 1;
            this.textInfoMovimientos.Text = "Movimientos Del Documento";
            this.textInfoMovimientos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridViewMovimientosCxc
            // 
            this.dataGridViewMovimientosCxc.AllowUserToAddRows = false;
            this.dataGridViewMovimientosCxc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewMovimientosCxc.BackgroundColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewMovimientosCxc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewMovimientosCxc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMovimientosCxc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idMoveCxcDgv,
            this.idDocumentoMoveCxcDgv,
            this.itemMoveCxcDgv,
            this.numberMoveCxcDgv,
            this.priceMoveCxcDgv,
            this.capturedUnitsMoveCxcDgv,
            this.capturedUnitIdMoveCxcDgv,
            this.subtotalMoveCxcDgv,
            this.discountMoveCxcDgv,
            this.totalMoveCxcDgv});
            this.dataGridViewMovimientosCxc.Location = new System.Drawing.Point(3, 43);
            this.dataGridViewMovimientosCxc.Name = "dataGridViewMovimientosCxc";
            this.dataGridViewMovimientosCxc.RowHeadersVisible = false;
            this.dataGridViewMovimientosCxc.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewMovimientosCxc.Size = new System.Drawing.Size(585, 205);
            this.dataGridViewMovimientosCxc.TabIndex = 0;
            this.dataGridViewMovimientosCxc.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridViewMovimientosCxc_Scroll);
            this.dataGridViewMovimientosCxc.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridViewMovimientosCxc_KeyUp);
            // 
            // idMoveCxcDgv
            // 
            this.idMoveCxcDgv.HeaderText = "Id";
            this.idMoveCxcDgv.Name = "idMoveCxcDgv";
            // 
            // idDocumentoMoveCxcDgv
            // 
            this.idDocumentoMoveCxcDgv.HeaderText = "IdDocumento";
            this.idDocumentoMoveCxcDgv.Name = "idDocumentoMoveCxcDgv";
            // 
            // itemMoveCxcDgv
            // 
            this.itemMoveCxcDgv.HeaderText = "Producto";
            this.itemMoveCxcDgv.Name = "itemMoveCxcDgv";
            // 
            // numberMoveCxcDgv
            // 
            this.numberMoveCxcDgv.HeaderText = "Número";
            this.numberMoveCxcDgv.Name = "numberMoveCxcDgv";
            // 
            // priceMoveCxcDgv
            // 
            this.priceMoveCxcDgv.HeaderText = "Precio";
            this.priceMoveCxcDgv.Name = "priceMoveCxcDgv";
            // 
            // capturedUnitsMoveCxcDgv
            // 
            this.capturedUnitsMoveCxcDgv.HeaderText = "Unidades";
            this.capturedUnitsMoveCxcDgv.Name = "capturedUnitsMoveCxcDgv";
            // 
            // capturedUnitIdMoveCxcDgv
            // 
            this.capturedUnitIdMoveCxcDgv.HeaderText = "Unidad";
            this.capturedUnitIdMoveCxcDgv.Name = "capturedUnitIdMoveCxcDgv";
            // 
            // subtotalMoveCxcDgv
            // 
            this.subtotalMoveCxcDgv.HeaderText = "Subtotal";
            this.subtotalMoveCxcDgv.Name = "subtotalMoveCxcDgv";
            // 
            // discountMoveCxcDgv
            // 
            this.discountMoveCxcDgv.HeaderText = "Descuento";
            this.discountMoveCxcDgv.Name = "discountMoveCxcDgv";
            // 
            // totalMoveCxcDgv
            // 
            this.totalMoveCxcDgv.HeaderText = "Total";
            this.totalMoveCxcDgv.Name = "totalMoveCxcDgv";
            // 
            // panelAbonar
            // 
            this.panelAbonar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAbonar.AutoScroll = true;
            this.panelAbonar.BackColor = System.Drawing.Color.MintCream;
            this.panelAbonar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAbonar.Controls.Add(this.textReferenciaAbonoFiniquitar);
            this.panelAbonar.Controls.Add(this.textInfoSeleccionarFormaDeCobro);
            this.panelAbonar.Controls.Add(this.comboBoxFormasDeCobro);
            this.panelAbonar.Controls.Add(this.textPendienteDocument);
            this.panelAbonar.Controls.Add(this.btnLiquidarCredito);
            this.panelAbonar.Controls.Add(this.btnRealizarAbono);
            this.panelAbonar.Controls.Add(this.textCurrency);
            this.panelAbonar.Controls.Add(this.textImporteAbono);
            this.panelAbonar.Controls.Add(this.textRealizarAbono);
            this.panelAbonar.Controls.Add(this.editImporteAbono);
            this.panelAbonar.Location = new System.Drawing.Point(6, 302);
            this.panelAbonar.Name = "panelAbonar";
            this.panelAbonar.Size = new System.Drawing.Size(594, 237);
            this.panelAbonar.TabIndex = 1;
            // 
            // textReferenciaAbonoFiniquitar
            // 
            this.textReferenciaAbonoFiniquitar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textReferenciaAbonoFiniquitar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textReferenciaAbonoFiniquitar.Location = new System.Drawing.Point(9, 116);
            this.textReferenciaAbonoFiniquitar.Name = "textReferenciaAbonoFiniquitar";
            this.textReferenciaAbonoFiniquitar.Size = new System.Drawing.Size(580, 23);
            this.textReferenciaAbonoFiniquitar.TabIndex = 16;
            this.textReferenciaAbonoFiniquitar.Text = "Referencia";
            this.textReferenciaAbonoFiniquitar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textInfoSeleccionarFormaDeCobro
            // 
            this.textInfoSeleccionarFormaDeCobro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textInfoSeleccionarFormaDeCobro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInfoSeleccionarFormaDeCobro.Location = new System.Drawing.Point(8, 90);
            this.textInfoSeleccionarFormaDeCobro.Name = "textInfoSeleccionarFormaDeCobro";
            this.textInfoSeleccionarFormaDeCobro.Size = new System.Drawing.Size(233, 21);
            this.textInfoSeleccionarFormaDeCobro.TabIndex = 15;
            this.textInfoSeleccionarFormaDeCobro.Text = "Forma de Cobro:";
            this.textInfoSeleccionarFormaDeCobro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxFormasDeCobro
            // 
            this.comboBoxFormasDeCobro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxFormasDeCobro.BackColor = System.Drawing.Color.Azure;
            this.comboBoxFormasDeCobro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFormasDeCobro.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBoxFormasDeCobro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxFormasDeCobro.FormattingEnabled = true;
            this.comboBoxFormasDeCobro.Location = new System.Drawing.Point(247, 89);
            this.comboBoxFormasDeCobro.Name = "comboBoxFormasDeCobro";
            this.comboBoxFormasDeCobro.Size = new System.Drawing.Size(342, 24);
            this.comboBoxFormasDeCobro.TabIndex = 14;
            // 
            // textPendienteDocument
            // 
            this.textPendienteDocument.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textPendienteDocument.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textPendienteDocument.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textPendienteDocument.ForeColor = System.Drawing.Color.Red;
            this.textPendienteDocument.Location = new System.Drawing.Point(3, 0);
            this.textPendienteDocument.Name = "textPendienteDocument";
            this.textPendienteDocument.Size = new System.Drawing.Size(586, 33);
            this.textPendienteDocument.TabIndex = 13;
            this.textPendienteDocument.Text = "Pendiente";
            this.textPendienteDocument.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textCurrency
            // 
            this.textCurrency.AutoSize = true;
            this.textCurrency.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textCurrency.Location = new System.Drawing.Point(432, 63);
            this.textCurrency.Name = "textCurrency";
            this.textCurrency.Size = new System.Drawing.Size(39, 16);
            this.textCurrency.TabIndex = 10;
            this.textCurrency.Text = "MXN";
            this.textCurrency.Click += new System.EventHandler(this.textCurrency_Click);
            // 
            // textImporteAbono
            // 
            this.textImporteAbono.AutoSize = true;
            this.textImporteAbono.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textImporteAbono.Location = new System.Drawing.Point(93, 63);
            this.textImporteAbono.Name = "textImporteAbono";
            this.textImporteAbono.Size = new System.Drawing.Size(71, 16);
            this.textImporteAbono.TabIndex = 9;
            this.textImporteAbono.Text = "Importe $";
            // 
            // textRealizarAbono
            // 
            this.textRealizarAbono.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textRealizarAbono.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textRealizarAbono.Location = new System.Drawing.Point(3, 33);
            this.textRealizarAbono.Name = "textRealizarAbono";
            this.textRealizarAbono.Size = new System.Drawing.Size(586, 24);
            this.textRealizarAbono.TabIndex = 8;
            this.textRealizarAbono.Text = "Realizar Abono";
            this.textRealizarAbono.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // editImporteAbono
            // 
            this.editImporteAbono.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editImporteAbono.Location = new System.Drawing.Point(179, 60);
            this.editImporteAbono.Name = "editImporteAbono";
            this.editImporteAbono.Size = new System.Drawing.Size(247, 22);
            this.editImporteAbono.TabIndex = 7;
            this.editImporteAbono.KeyUp += new System.Windows.Forms.KeyEventHandler(this.editImporteAbono_KeyUp);
            // 
            // textCreditoSeleccionado
            // 
            this.textCreditoSeleccionado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textCreditoSeleccionado.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.textCreditoSeleccionado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textCreditoSeleccionado.ForeColor = System.Drawing.Color.MidnightBlue;
            this.textCreditoSeleccionado.Location = new System.Drawing.Point(6, 0);
            this.textCreditoSeleccionado.Name = "textCreditoSeleccionado";
            this.textCreditoSeleccionado.Size = new System.Drawing.Size(591, 36);
            this.textCreditoSeleccionado.TabIndex = 0;
            this.textCreditoSeleccionado.Text = "Seleccionar Documento";
            this.textCreditoSeleccionado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLiquidarCredito
            // 
            this.btnLiquidarCredito.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLiquidarCredito.BackColor = System.Drawing.Color.Transparent;
            this.btnLiquidarCredito.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLiquidarCredito.FlatAppearance.BorderColor = System.Drawing.Color.MediumSeaGreen;
            this.btnLiquidarCredito.FlatAppearance.BorderSize = 2;
            this.btnLiquidarCredito.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Honeydew;
            this.btnLiquidarCredito.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLiquidarCredito.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLiquidarCredito.Location = new System.Drawing.Point(293, 142);
            this.btnLiquidarCredito.Name = "btnLiquidarCredito";
            this.btnLiquidarCredito.Size = new System.Drawing.Size(296, 46);
            this.btnLiquidarCredito.TabIndex = 12;
            this.btnLiquidarCredito.Text = "Liquidar Deuda";
            this.btnLiquidarCredito.UseVisualStyleBackColor = false;
            this.btnLiquidarCredito.Click += new System.EventHandler(this.btnLiquidarCredito_Click);
            // 
            // btnRealizarAbono
            // 
            this.btnRealizarAbono.BackColor = System.Drawing.Color.Transparent;
            this.btnRealizarAbono.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRealizarAbono.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnRealizarAbono.FlatAppearance.BorderSize = 2;
            this.btnRealizarAbono.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnRealizarAbono.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRealizarAbono.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRealizarAbono.Location = new System.Drawing.Point(5, 142);
            this.btnRealizarAbono.Name = "btnRealizarAbono";
            this.btnRealizarAbono.Size = new System.Drawing.Size(272, 46);
            this.btnRealizarAbono.TabIndex = 11;
            this.btnRealizarAbono.Text = "Abonar";
            this.btnRealizarAbono.UseVisualStyleBackColor = false;
            this.btnRealizarAbono.Click += new System.EventHandler(this.btnRealizarAbono_Click_1);
            this.btnRealizarAbono.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnRealizarAbono_KeyUp);
            // 
            // FrmCobranzaCxc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(1109, 639);
            this.Controls.Add(this.panelAbonarLiquidar);
            this.Controls.Add(this.panelDataGridFrmCobranza);
            this.Controls.Add(this.panelTituloFrmCobranza);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmCobranzaCxc";
            this.Text = "Cobranza";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmCobranzaCxc_Load);
            this.panelTituloFrmCobranza.ResumeLayout(false);
            this.panelTituloFrmCobranza.PerformLayout();
            this.panelDataGridFrmCobranza.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCreditsDocuments)).EndInit();
            this.panelAbonarLiquidar.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatosMovimientos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMovimientosCxc)).EndInit();
            this.panelAbonar.ResumeLayout(false);
            this.panelAbonar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTituloFrmCobranza;
        private System.Windows.Forms.Label textNombreClienteFrmCobranza;
        private System.Windows.Forms.Panel panelDataGridFrmCobranza;
        private System.Windows.Forms.DataGridView dataGridViewCreditsDocuments;
        private System.Windows.Forms.PictureBox imgSinDatos;
        private System.Windows.Forms.Panel panelAbonarLiquidar;
        private System.Windows.Forms.Label textCreditoSeleccionado;
        private System.Windows.Forms.Panel panelAbonar;
        private System.Windows.Forms.Label textCurrency;
        private System.Windows.Forms.Label textImporteAbono;
        private System.Windows.Forms.Label textRealizarAbono;
        private System.Windows.Forms.TextBox editImporteAbono;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label textInfoMovimientos;
        private System.Windows.Forms.DataGridView dataGridViewMovimientosCxc;
        private System.Windows.Forms.PictureBox imgSinDatosMovimientos;
        private System.Windows.Forms.Label textPendienteDocument;
        private System.Windows.Forms.Label textInfoSeleccionarFormaDeCobro;
        private System.Windows.Forms.ComboBox comboBoxFormasDeCobro;
        private System.Windows.Forms.Label textReferenciaAbonoFiniquitar;
        private System.Windows.Forms.Button btnUpdateCxc;
        private System.Windows.Forms.Button btnClose;
        private RoundedButton btnRealizarAbono;
        private RoundedButton btnLiquidarCredito;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDgvCxc;
        private System.Windows.Forms.DataGridViewTextBoxColumn idCxcDgvCxc;
        private System.Windows.Forms.DataGridViewTextBoxColumn folioDgvCxc;
        private System.Windows.Forms.DataGridViewTextBoxColumn diasAtrasadosDgvCxc;
        private System.Windows.Forms.DataGridViewTextBoxColumn saldoActualDgvCxc;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaVencimientoDgvCxc;
        private System.Windows.Forms.DataGridViewTextBoxColumn conceptoDgvCxc;
        private System.Windows.Forms.Label textTotalCredits;
        private System.Windows.Forms.DataGridViewTextBoxColumn idMoveCxcDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDocumentoMoveCxcDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemMoveCxcDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn numberMoveCxcDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceMoveCxcDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn capturedUnitsMoveCxcDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn capturedUnitIdMoveCxcDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn subtotalMoveCxcDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn discountMoveCxcDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalMoveCxcDgv;
        private System.Windows.Forms.Button btnVerPagos;
    }
}