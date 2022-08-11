namespace SyncTPV
{
    partial class FormCorteCaja
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCorteCaja));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBoxMontosARetirar = new System.Windows.Forms.GroupBox();
            this.panelAgregarMontos = new System.Windows.Forms.Panel();
            this.btnRetirar = new SyncTPV.RoundedButton();
            this.editAmount = new System.Windows.Forms.TextBox();
            this.textIngresarMonto = new System.Windows.Forms.Label();
            this.textFormaDeCobro = new System.Windows.Forms.Label();
            this.editFormaDeCobro = new System.Windows.Forms.TextBox();
            this.imgSinDatos = new System.Windows.Forms.PictureBox();
            this.dataGridFormaCobros = new System.Windows.Forms.DataGridView();
            this.idFcDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameFcDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountFcDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnTerminarRetiro = new SyncTPV.RoundedButton();
            this.pnlHacerCorte = new System.Windows.Forms.Panel();
            this.pnlCorteCaja = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.imgSinDatosRetirosRealizados = new System.Windows.Forms.PictureBox();
            this.btnPrintRetiro = new SyncTPV.RoundedButton();
            this.dataGridViewRetirosRealizados = new System.Windows.Forms.DataGridView();
            this.panelRetirosRealizados = new System.Windows.Forms.Panel();
            this.textDescription = new System.Windows.Forms.Label();
            this.textConcept = new System.Windows.Forms.Label();
            this.textInfoSeleccionarRetiro = new System.Windows.Forms.Label();
            this.comboBoxSeleccionarRetiro = new System.Windows.Forms.ComboBox();
            this.tabControlCortesDeCaja = new System.Windows.Forms.TabControl();
            this.tabPageRealizarRetiro = new System.Windows.Forms.TabPage();
            this.tabPageRetirosRealizados = new System.Windows.Forms.TabPage();
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.panelRetirosDeCaja = new System.Windows.Forms.Panel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBoxMontosARetirar.SuspendLayout();
            this.panelAgregarMontos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridFormaCobros)).BeginInit();
            this.pnlHacerCorte.SuspendLayout();
            this.pnlCorteCaja.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatosRetirosRealizados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRetirosRealizados)).BeginInit();
            this.panelRetirosRealizados.SuspendLayout();
            this.tabControlCortesDeCaja.SuspendLayout();
            this.tabPageRealizarRetiro.SuspendLayout();
            this.tabPageRetirosRealizados.SuspendLayout();
            this.panelToolbar.SuspendLayout();
            this.panelRetirosDeCaja.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxMontosARetirar
            // 
            this.groupBoxMontosARetirar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxMontosARetirar.Controls.Add(this.panelAgregarMontos);
            this.groupBoxMontosARetirar.Controls.Add(this.imgSinDatos);
            this.groupBoxMontosARetirar.Controls.Add(this.dataGridFormaCobros);
            this.groupBoxMontosARetirar.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxMontosARetirar.Location = new System.Drawing.Point(13, 20);
            this.groupBoxMontosARetirar.Name = "groupBoxMontosARetirar";
            this.groupBoxMontosARetirar.Size = new System.Drawing.Size(850, 353);
            this.groupBoxMontosARetirar.TabIndex = 3;
            this.groupBoxMontosARetirar.TabStop = false;
            this.groupBoxMontosARetirar.Text = "Monto a retirar";
            // 
            // panelAgregarMontos
            // 
            this.panelAgregarMontos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAgregarMontos.Controls.Add(this.btnRetirar);
            this.panelAgregarMontos.Controls.Add(this.editAmount);
            this.panelAgregarMontos.Controls.Add(this.textIngresarMonto);
            this.panelAgregarMontos.Controls.Add(this.textFormaDeCobro);
            this.panelAgregarMontos.Controls.Add(this.editFormaDeCobro);
            this.panelAgregarMontos.Location = new System.Drawing.Point(527, 24);
            this.panelAgregarMontos.Name = "panelAgregarMontos";
            this.panelAgregarMontos.Size = new System.Drawing.Size(317, 323);
            this.panelAgregarMontos.TabIndex = 15;
            // 
            // btnRetirar
            // 
            this.btnRetirar.BackColor = System.Drawing.Color.Transparent;
            this.btnRetirar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRetirar.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnRetirar.FlatAppearance.BorderSize = 2;
            this.btnRetirar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnRetirar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRetirar.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRetirar.Location = new System.Drawing.Point(62, 274);
            this.btnRetirar.Name = "btnRetirar";
            this.btnRetirar.Size = new System.Drawing.Size(203, 39);
            this.btnRetirar.TabIndex = 18;
            this.btnRetirar.Text = "Guardar";
            this.btnRetirar.UseVisualStyleBackColor = false;
            // 
            // editAmount
            // 
            this.editAmount.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editAmount.Location = new System.Drawing.Point(11, 202);
            this.editAmount.Name = "editAmount";
            this.editAmount.Size = new System.Drawing.Size(295, 27);
            this.editAmount.TabIndex = 14;
            this.editAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.editAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editAmount_KeyPress);
            this.editAmount.KeyUp += new System.Windows.Forms.KeyEventHandler(this.editAmount_KeyUp);
            // 
            // textIngresarMonto
            // 
            this.textIngresarMonto.AutoSize = true;
            this.textIngresarMonto.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textIngresarMonto.Location = new System.Drawing.Point(7, 175);
            this.textIngresarMonto.Name = "textIngresarMonto";
            this.textIngresarMonto.Size = new System.Drawing.Size(133, 19);
            this.textIngresarMonto.TabIndex = 17;
            this.textIngresarMonto.Text = "Ingresar Monto: $";
            // 
            // textFormaDeCobro
            // 
            this.textFormaDeCobro.AutoSize = true;
            this.textFormaDeCobro.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textFormaDeCobro.Location = new System.Drawing.Point(7, 93);
            this.textFormaDeCobro.Name = "textFormaDeCobro";
            this.textFormaDeCobro.Size = new System.Drawing.Size(126, 19);
            this.textFormaDeCobro.TabIndex = 15;
            this.textFormaDeCobro.Text = "Forma de Cobro:";
            // 
            // editFormaDeCobro
            // 
            this.editFormaDeCobro.BackColor = System.Drawing.Color.Azure;
            this.editFormaDeCobro.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editFormaDeCobro.Location = new System.Drawing.Point(9, 131);
            this.editFormaDeCobro.Name = "editFormaDeCobro";
            this.editFormaDeCobro.ReadOnly = true;
            this.editFormaDeCobro.Size = new System.Drawing.Size(297, 30);
            this.editFormaDeCobro.TabIndex = 16;
            // 
            // imgSinDatos
            // 
            this.imgSinDatos.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.imgSinDatos.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("imgSinDatos.BackgroundImage")));
            this.imgSinDatos.Location = new System.Drawing.Point(138, 117);
            this.imgSinDatos.Name = "imgSinDatos";
            this.imgSinDatos.Size = new System.Drawing.Size(269, 177);
            this.imgSinDatos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgSinDatos.TabIndex = 14;
            this.imgSinDatos.TabStop = false;
            // 
            // dataGridFormaCobros
            // 
            this.dataGridFormaCobros.AllowUserToAddRows = false;
            this.dataGridFormaCobros.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridFormaCobros.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridFormaCobros.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridFormaCobros.BackgroundColor = System.Drawing.Color.FloralWhite;
            this.dataGridFormaCobros.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Coral;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridFormaCobros.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridFormaCobros.ColumnHeadersHeight = 40;
            this.dataGridFormaCobros.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idFcDgv,
            this.nameFcDgv,
            this.amountFcDgv});
            this.dataGridFormaCobros.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dataGridFormaCobros.EnableHeadersVisualStyles = false;
            this.dataGridFormaCobros.GridColor = System.Drawing.Color.FloralWhite;
            this.dataGridFormaCobros.Location = new System.Drawing.Point(32, 24);
            this.dataGridFormaCobros.MultiSelect = false;
            this.dataGridFormaCobros.Name = "dataGridFormaCobros";
            this.dataGridFormaCobros.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridFormaCobros.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridFormaCobros.RowHeadersVisible = false;
            this.dataGridFormaCobros.RowHeadersWidth = 50;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.dataGridFormaCobros.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridFormaCobros.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridFormaCobros.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FloralWhite;
            this.dataGridFormaCobros.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridFormaCobros.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dataGridFormaCobros.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.dataGridFormaCobros.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridFormaCobros.Size = new System.Drawing.Size(489, 323);
            this.dataGridFormaCobros.TabIndex = 0;
            this.dataGridFormaCobros.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtGridFormasDeCobro_CellClick);
            this.dataGridFormaCobros.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridFormaCobros_Scroll);
            this.dataGridFormaCobros.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtGridFormaCobros_KeyDown);
            // 
            // idFcDgv
            // 
            this.idFcDgv.HeaderText = "Id";
            this.idFcDgv.Name = "idFcDgv";
            this.idFcDgv.ReadOnly = true;
            // 
            // nameFcDgv
            // 
            this.nameFcDgv.HeaderText = "Nombre";
            this.nameFcDgv.Name = "nameFcDgv";
            this.nameFcDgv.ReadOnly = true;
            // 
            // amountFcDgv
            // 
            this.amountFcDgv.HeaderText = "Importe";
            this.amountFcDgv.Name = "amountFcDgv";
            this.amountFcDgv.ReadOnly = true;
            // 
            // btnTerminarRetiro
            // 
            this.btnTerminarRetiro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTerminarRetiro.BackColor = System.Drawing.Color.Transparent;
            this.btnTerminarRetiro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTerminarRetiro.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnTerminarRetiro.FlatAppearance.BorderSize = 2;
            this.btnTerminarRetiro.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnTerminarRetiro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTerminarRetiro.Font = new System.Drawing.Font("Roboto Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTerminarRetiro.Location = new System.Drawing.Point(379, 379);
            this.btnTerminarRetiro.Name = "btnTerminarRetiro";
            this.btnTerminarRetiro.Size = new System.Drawing.Size(155, 41);
            this.btnTerminarRetiro.TabIndex = 10;
            this.btnTerminarRetiro.Text = "Terminar Retiro";
            this.btnTerminarRetiro.UseVisualStyleBackColor = false;
            this.btnTerminarRetiro.Click += new System.EventHandler(this.btnTerminarRetiro_Click);
            // 
            // pnlHacerCorte
            // 
            this.pnlHacerCorte.BackColor = System.Drawing.Color.White;
            this.pnlHacerCorte.Controls.Add(this.btnTerminarRetiro);
            this.pnlHacerCorte.Controls.Add(this.groupBoxMontosARetirar);
            this.pnlHacerCorte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHacerCorte.Location = new System.Drawing.Point(3, 3);
            this.pnlHacerCorte.Name = "pnlHacerCorte";
            this.pnlHacerCorte.Size = new System.Drawing.Size(880, 437);
            this.pnlHacerCorte.TabIndex = 9;
            // 
            // pnlCorteCaja
            // 
            this.pnlCorteCaja.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCorteCaja.Controls.Add(this.panel5);
            this.pnlCorteCaja.Controls.Add(this.panelRetirosRealizados);
            this.pnlCorteCaja.Location = new System.Drawing.Point(6, 6);
            this.pnlCorteCaja.Name = "pnlCorteCaja";
            this.pnlCorteCaja.Size = new System.Drawing.Size(938, 513);
            this.pnlCorteCaja.TabIndex = 10;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.imgSinDatosRetirosRealizados);
            this.panel5.Controls.Add(this.btnPrintRetiro);
            this.panel5.Controls.Add(this.dataGridViewRetirosRealizados);
            this.panel5.Location = new System.Drawing.Point(4, 87);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(928, 426);
            this.panel5.TabIndex = 12;
            // 
            // imgSinDatosRetirosRealizados
            // 
            this.imgSinDatosRetirosRealizados.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.imgSinDatosRetirosRealizados.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("imgSinDatosRetirosRealizados.BackgroundImage")));
            this.imgSinDatosRetirosRealizados.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.imgSinDatosRetirosRealizados.Location = new System.Drawing.Point(255, 49);
            this.imgSinDatosRetirosRealizados.Name = "imgSinDatosRetirosRealizados";
            this.imgSinDatosRetirosRealizados.Size = new System.Drawing.Size(406, 265);
            this.imgSinDatosRetirosRealizados.TabIndex = 13;
            this.imgSinDatosRetirosRealizados.TabStop = false;
            // 
            // btnPrintRetiro
            // 
            this.btnPrintRetiro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintRetiro.BackColor = System.Drawing.Color.Transparent;
            this.btnPrintRetiro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrintRetiro.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnPrintRetiro.FlatAppearance.BorderSize = 2;
            this.btnPrintRetiro.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnPrintRetiro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintRetiro.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintRetiro.Location = new System.Drawing.Point(728, 371);
            this.btnPrintRetiro.Name = "btnPrintRetiro";
            this.btnPrintRetiro.Size = new System.Drawing.Size(176, 43);
            this.btnPrintRetiro.TabIndex = 12;
            this.btnPrintRetiro.Text = "Imprimir Retiro";
            this.btnPrintRetiro.UseVisualStyleBackColor = false;
            this.btnPrintRetiro.Click += new System.EventHandler(this.btnPrintRetiro_Click);
            // 
            // dataGridViewRetirosRealizados
            // 
            this.dataGridViewRetirosRealizados.AllowUserToAddRows = false;
            this.dataGridViewRetirosRealizados.AllowUserToDeleteRows = false;
            this.dataGridViewRetirosRealizados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewRetirosRealizados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewRetirosRealizados.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewRetirosRealizados.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewRetirosRealizados.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Coral;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewRetirosRealizados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewRetirosRealizados.ColumnHeadersHeight = 40;
            this.dataGridViewRetirosRealizados.EnableHeadersVisualStyles = false;
            this.dataGridViewRetirosRealizados.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.dataGridViewRetirosRealizados.Location = new System.Drawing.Point(10, 3);
            this.dataGridViewRetirosRealizados.Name = "dataGridViewRetirosRealizados";
            this.dataGridViewRetirosRealizados.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(145)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewRetirosRealizados.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewRetirosRealizados.RowHeadersVisible = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(145)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridViewRetirosRealizados.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewRetirosRealizados.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewRetirosRealizados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewRetirosRealizados.Size = new System.Drawing.Size(915, 362);
            this.dataGridViewRetirosRealizados.TabIndex = 11;
            // 
            // panelRetirosRealizados
            // 
            this.panelRetirosRealizados.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRetirosRealizados.Controls.Add(this.textDescription);
            this.panelRetirosRealizados.Controls.Add(this.textConcept);
            this.panelRetirosRealizados.Controls.Add(this.textInfoSeleccionarRetiro);
            this.panelRetirosRealizados.Controls.Add(this.comboBoxSeleccionarRetiro);
            this.panelRetirosRealizados.Location = new System.Drawing.Point(3, 3);
            this.panelRetirosRealizados.Name = "panelRetirosRealizados";
            this.panelRetirosRealizados.Size = new System.Drawing.Size(932, 81);
            this.panelRetirosRealizados.TabIndex = 11;
            // 
            // textDescription
            // 
            this.textDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textDescription.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textDescription.Location = new System.Drawing.Point(458, 35);
            this.textDescription.Name = "textDescription";
            this.textDescription.Size = new System.Drawing.Size(447, 43);
            this.textDescription.TabIndex = 3;
            this.textDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textConcept
            // 
            this.textConcept.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textConcept.Location = new System.Drawing.Point(7, 35);
            this.textConcept.Name = "textConcept";
            this.textConcept.Size = new System.Drawing.Size(445, 43);
            this.textConcept.TabIndex = 2;
            this.textConcept.Text = "Concepto";
            this.textConcept.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textInfoSeleccionarRetiro
            // 
            this.textInfoSeleccionarRetiro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.textInfoSeleccionarRetiro.AutoSize = true;
            this.textInfoSeleccionarRetiro.Font = new System.Drawing.Font("Roboto Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInfoSeleccionarRetiro.Location = new System.Drawing.Point(191, 4);
            this.textInfoSeleccionarRetiro.Name = "textInfoSeleccionarRetiro";
            this.textInfoSeleccionarRetiro.Size = new System.Drawing.Size(142, 19);
            this.textInfoSeleccionarRetiro.TabIndex = 1;
            this.textInfoSeleccionarRetiro.Text = "Seleccionar Retiro";
            // 
            // comboBoxSeleccionarRetiro
            // 
            this.comboBoxSeleccionarRetiro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxSeleccionarRetiro.Font = new System.Drawing.Font("Roboto Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxSeleccionarRetiro.FormattingEnabled = true;
            this.comboBoxSeleccionarRetiro.Location = new System.Drawing.Point(361, 3);
            this.comboBoxSeleccionarRetiro.Name = "comboBoxSeleccionarRetiro";
            this.comboBoxSeleccionarRetiro.Size = new System.Drawing.Size(425, 31);
            this.comboBoxSeleccionarRetiro.TabIndex = 0;
            this.comboBoxSeleccionarRetiro.SelectedIndexChanged += new System.EventHandler(this.comboBoxSeleccionarRetiro_SelectedIndexChanged);
            // 
            // tabControlCortesDeCaja
            // 
            this.tabControlCortesDeCaja.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlCortesDeCaja.Controls.Add(this.tabPageRealizarRetiro);
            this.tabControlCortesDeCaja.Controls.Add(this.tabPageRetirosRealizados);
            this.tabControlCortesDeCaja.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlCortesDeCaja.Location = new System.Drawing.Point(18, 6);
            this.tabControlCortesDeCaja.Name = "tabControlCortesDeCaja";
            this.tabControlCortesDeCaja.SelectedIndex = 0;
            this.tabControlCortesDeCaja.Size = new System.Drawing.Size(894, 474);
            this.tabControlCortesDeCaja.TabIndex = 11;
            this.tabControlCortesDeCaja.SelectedIndexChanged += new System.EventHandler(this.tabControlCortesDeCaja_SelectedIndexChanged);
            // 
            // tabPageRealizarRetiro
            // 
            this.tabPageRealizarRetiro.Controls.Add(this.pnlHacerCorte);
            this.tabPageRealizarRetiro.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageRealizarRetiro.Location = new System.Drawing.Point(4, 27);
            this.tabPageRealizarRetiro.Name = "tabPageRealizarRetiro";
            this.tabPageRealizarRetiro.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRealizarRetiro.Size = new System.Drawing.Size(886, 443);
            this.tabPageRealizarRetiro.TabIndex = 1;
            this.tabPageRealizarRetiro.Text = "Realizar Retiro";
            this.tabPageRealizarRetiro.UseVisualStyleBackColor = true;
            // 
            // tabPageRetirosRealizados
            // 
            this.tabPageRetirosRealizados.BackColor = System.Drawing.Color.White;
            this.tabPageRetirosRealizados.Controls.Add(this.pnlCorteCaja);
            this.tabPageRetirosRealizados.Location = new System.Drawing.Point(4, 27);
            this.tabPageRetirosRealizados.Name = "tabPageRetirosRealizados";
            this.tabPageRetirosRealizados.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRetirosRealizados.Size = new System.Drawing.Size(886, 443);
            this.tabPageRetirosRealizados.TabIndex = 0;
            this.tabPageRetirosRealizados.Text = "Cortes de Caja Realizados";
            // 
            // panelToolbar
            // 
            this.panelToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelToolbar.BackColor = System.Drawing.Color.Coral;
            this.panelToolbar.Controls.Add(this.button2);
            this.panelToolbar.Location = new System.Drawing.Point(0, 0);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(964, 70);
            this.panelToolbar.TabIndex = 12;
            // 
            // button2
            // 
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(20)))), ((int)(((byte)(224)))));
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Image = global::SyncTPV.Properties.Resources.close;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.Location = new System.Drawing.Point(3, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 64);
            this.button2.TabIndex = 3;
            this.button2.Text = "Cerrar";
            this.button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // panelRetirosDeCaja
            // 
            this.panelRetirosDeCaja.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRetirosDeCaja.Controls.Add(this.tabControlCortesDeCaja);
            this.panelRetirosDeCaja.Location = new System.Drawing.Point(12, 76);
            this.panelRetirosDeCaja.Name = "panelRetirosDeCaja";
            this.panelRetirosDeCaja.Size = new System.Drawing.Size(940, 492);
            this.panelRetirosDeCaja.TabIndex = 15;
            // 
            // FormCorteCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(964, 580);
            this.Controls.Add(this.panelRetirosDeCaja);
            this.Controls.Add(this.panelToolbar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormCorteCaja";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Corte de Caja";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCorteCaja_FormClosed);
            this.Load += new System.EventHandler(this.FrmCorteCaja_Load);
            this.groupBoxMontosARetirar.ResumeLayout(false);
            this.panelAgregarMontos.ResumeLayout(false);
            this.panelAgregarMontos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridFormaCobros)).EndInit();
            this.pnlHacerCorte.ResumeLayout(false);
            this.pnlCorteCaja.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatosRetirosRealizados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRetirosRealizados)).EndInit();
            this.panelRetirosRealizados.ResumeLayout(false);
            this.panelRetirosRealizados.PerformLayout();
            this.tabControlCortesDeCaja.ResumeLayout(false);
            this.tabPageRealizarRetiro.ResumeLayout(false);
            this.tabPageRetirosRealizados.ResumeLayout(false);
            this.panelToolbar.ResumeLayout(false);
            this.panelRetirosDeCaja.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxMontosARetirar;
        private System.Windows.Forms.Panel pnlHacerCorte;
        private System.Windows.Forms.Panel pnlCorteCaja;
        private System.Windows.Forms.TabControl tabControlCortesDeCaja;
        private System.Windows.Forms.TabPage tabPageRetirosRealizados;
        private System.Windows.Forms.TabPage tabPageRealizarRetiro;
        private System.Windows.Forms.Panel panelRetirosRealizados;
        private System.Windows.Forms.DataGridView dataGridFormaCobros;
        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.Panel panelRetirosDeCaja;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox imgSinDatos;
        private System.Windows.Forms.Label textInfoSeleccionarRetiro;
        private System.Windows.Forms.ComboBox comboBoxSeleccionarRetiro;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.DataGridView dataGridViewRetirosRealizados;
        private System.Windows.Forms.PictureBox imgSinDatosRetirosRealizados;
        private System.Windows.Forms.Panel panelAgregarMontos;
        private System.Windows.Forms.TextBox editAmount;
        private System.Windows.Forms.Label textIngresarMonto;
        private System.Windows.Forms.Label textFormaDeCobro;
        private System.Windows.Forms.TextBox editFormaDeCobro;
        private System.Windows.Forms.Label textConcept;
        private System.Windows.Forms.Label textDescription;
        private RoundedButton btnTerminarRetiro;
        private RoundedButton btnRetirar;
        private RoundedButton btnPrintRetiro;
        private System.Windows.Forms.DataGridViewTextBoxColumn idFcDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameFcDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountFcDgv;
    }
}