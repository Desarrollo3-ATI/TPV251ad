namespace SyncTPV
{
    partial class FormPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.backgroundCargaInicial = new System.ComponentModel.BackgroundWorker();
            this.pbCargaInicialFrmPrincipal = new System.Windows.Forms.ProgressBar();
            this.menuStripFrmPrincipal = new System.Windows.Forms.MenuStrip();
            this.datosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aperturaDeCajaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cargaInicialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actualizarDatosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.descargasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opcionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuraciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.soporteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarSesiónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelContent = new System.Windows.Forms.Panel();
            this.textDownloadInfo = new System.Windows.Forms.Label();
            this.textVersionFrmPrincipal = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnTestLan = new System.Windows.Forms.Button();
            this.imgSinDatos = new System.Windows.Forms.PictureBox();
            this.textPromotionsFrmPrincipal = new System.Windows.Forms.Label();
            this.dataGridViewPromotionsFrmPincipal = new System.Windows.Forms.DataGridView();
            this.idDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codeDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vencimientoDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.minDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descuentoDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnIngresos = new System.Windows.Forms.Button();
            this.btnTaras = new System.Windows.Forms.Button();
            this.btnReportsFrmPrincipal = new System.Windows.Forms.Button();
            this.btnVenta = new System.Windows.Forms.Button();
            this.btnArticulos = new System.Windows.Forms.Button();
            this.btnHistorialDocumentosFrmPrincipal = new System.Windows.Forms.Button();
            this.btnCliente = new System.Windows.Forms.Button();
            this.btnCorteDeCaja = new System.Windows.Forms.Button();
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.pictureBoxLogoSuperior = new System.Windows.Forms.PictureBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.panelNombres = new System.Windows.Forms.Panel();
            this.panelNombresUno = new System.Windows.Forms.Panel();
            this.editNombreCajero = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelNombresDos = new System.Windows.Forms.Panel();
            this.editNombreCaja = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStripFrmPrincipal.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPromotionsFrmPincipal)).BeginInit();
            this.panelMenu.SuspendLayout();
            this.panelToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogoSuperior)).BeginInit();
            this.panelNombres.SuspendLayout();
            this.panelNombresUno.SuspendLayout();
            this.panelNombresDos.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pbCargaInicialFrmPrincipal
            // 
            this.pbCargaInicialFrmPrincipal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbCargaInicialFrmPrincipal.Location = new System.Drawing.Point(19, 625);
            this.pbCargaInicialFrmPrincipal.Name = "pbCargaInicialFrmPrincipal";
            this.pbCargaInicialFrmPrincipal.Size = new System.Drawing.Size(865, 16);
            this.pbCargaInicialFrmPrincipal.TabIndex = 5;
            this.pbCargaInicialFrmPrincipal.Visible = false;
            this.pbCargaInicialFrmPrincipal.Click += new System.EventHandler(this.ProgressBar1_Click);
            // 
            // menuStripFrmPrincipal
            // 
            this.menuStripFrmPrincipal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.menuStripFrmPrincipal.AutoSize = false;
            this.menuStripFrmPrincipal.BackColor = System.Drawing.Color.Coral;
            this.menuStripFrmPrincipal.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStripFrmPrincipal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStripFrmPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.datosToolStripMenuItem,
            this.opcionesToolStripMenuItem});
            this.menuStripFrmPrincipal.Location = new System.Drawing.Point(709, 4);
            this.menuStripFrmPrincipal.Name = "menuStripFrmPrincipal";
            this.menuStripFrmPrincipal.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuStripFrmPrincipal.Size = new System.Drawing.Size(324, 64);
            this.menuStripFrmPrincipal.TabIndex = 16;
            this.menuStripFrmPrincipal.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStripFrmPrincipal_ItemClicked);
            // 
            // datosToolStripMenuItem
            // 
            this.datosToolStripMenuItem.BackColor = System.Drawing.Color.Coral;
            this.datosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aperturaDeCajaToolStripMenuItem,
            this.cargaInicialToolStripMenuItem,
            this.actualizarDatosToolStripMenuItem,
            this.sendDataToolStripMenuItem,
            this.descargasToolStripMenuItem});
            this.datosToolStripMenuItem.Font = new System.Drawing.Font("Roboto Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datosToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.datosToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("datosToolStripMenuItem.Image")));
            this.datosToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.datosToolStripMenuItem.Name = "datosToolStripMenuItem";
            this.datosToolStripMenuItem.Size = new System.Drawing.Size(79, 60);
            this.datosToolStripMenuItem.Text = "&Datos";
            this.datosToolStripMenuItem.DropDownClosed += new System.EventHandler(this.datosToolStripMenuItem_DropDownClosed);
            this.datosToolStripMenuItem.DropDownOpened += new System.EventHandler(this.datosToolStripMenuItem_DropDownOpened);
            // 
            // aperturaDeCajaToolStripMenuItem
            // 
            this.aperturaDeCajaToolStripMenuItem.Margin = new System.Windows.Forms.Padding(1);
            this.aperturaDeCajaToolStripMenuItem.Name = "aperturaDeCajaToolStripMenuItem";
            this.aperturaDeCajaToolStripMenuItem.Size = new System.Drawing.Size(218, 24);
            this.aperturaDeCajaToolStripMenuItem.Text = "Apertura de Caja";
            this.aperturaDeCajaToolStripMenuItem.Click += new System.EventHandler(this.aperturaDeCajaToolStripMenuItem_Click);
            // 
            // cargaInicialToolStripMenuItem
            // 
            this.cargaInicialToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cargaInicialToolStripMenuItem.Image")));
            this.cargaInicialToolStripMenuItem.Margin = new System.Windows.Forms.Padding(1);
            this.cargaInicialToolStripMenuItem.Name = "cargaInicialToolStripMenuItem";
            this.cargaInicialToolStripMenuItem.Size = new System.Drawing.Size(218, 24);
            this.cargaInicialToolStripMenuItem.Text = "Carga Inicial";
            this.cargaInicialToolStripMenuItem.Click += new System.EventHandler(this.btnCargaInicial_Click);
            // 
            // actualizarDatosToolStripMenuItem
            // 
            this.actualizarDatosToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.actualizarDatosToolStripMenuItem.Margin = new System.Windows.Forms.Padding(1);
            this.actualizarDatosToolStripMenuItem.Name = "actualizarDatosToolStripMenuItem";
            this.actualizarDatosToolStripMenuItem.RightToLeftAutoMirrorImage = true;
            this.actualizarDatosToolStripMenuItem.Size = new System.Drawing.Size(218, 24);
            this.actualizarDatosToolStripMenuItem.Text = "Actualizar Datos";
            this.actualizarDatosToolStripMenuItem.Click += new System.EventHandler(this.actualizarDatosToolStripMenuItem_Click);
            // 
            // sendDataToolStripMenuItem
            // 
            this.sendDataToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sendDataToolStripMenuItem.Image")));
            this.sendDataToolStripMenuItem.Margin = new System.Windows.Forms.Padding(1);
            this.sendDataToolStripMenuItem.Name = "sendDataToolStripMenuItem";
            this.sendDataToolStripMenuItem.Size = new System.Drawing.Size(218, 24);
            this.sendDataToolStripMenuItem.Text = "Enviar Documentos";
            this.sendDataToolStripMenuItem.Click += new System.EventHandler(this.btnSendData_Click);
            // 
            // descargasToolStripMenuItem
            // 
            this.descargasToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.descargasToolStripMenuItem.Margin = new System.Windows.Forms.Padding(1);
            this.descargasToolStripMenuItem.Name = "descargasToolStripMenuItem";
            this.descargasToolStripMenuItem.Size = new System.Drawing.Size(218, 24);
            this.descargasToolStripMenuItem.Text = "Descargas";
            this.descargasToolStripMenuItem.Click += new System.EventHandler(this.descargasToolStripMenuItem_Click);
            // 
            // opcionesToolStripMenuItem
            // 
            this.opcionesToolStripMenuItem.BackColor = System.Drawing.Color.Coral;
            this.opcionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configuraciónToolStripMenuItem,
            this.soporteToolStripMenuItem,
            this.acercaDeToolStripMenuItem,
            this.cerrarSesiónToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.opcionesToolStripMenuItem.Font = new System.Drawing.Font("Roboto Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opcionesToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.opcionesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("opcionesToolStripMenuItem.Image")));
            this.opcionesToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.opcionesToolStripMenuItem.Name = "opcionesToolStripMenuItem";
            this.opcionesToolStripMenuItem.Size = new System.Drawing.Size(104, 60);
            this.opcionesToolStripMenuItem.Text = "Opciones";
            this.opcionesToolStripMenuItem.DropDownClosed += new System.EventHandler(this.opcionesToolStripMenuItem_DropDownClosed);
            this.opcionesToolStripMenuItem.DropDownOpened += new System.EventHandler(this.opcionesToolStripMenuItem_DropDownOpened);
            // 
            // configuraciónToolStripMenuItem
            // 
            this.configuraciónToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("configuraciónToolStripMenuItem.Image")));
            this.configuraciónToolStripMenuItem.Name = "configuraciónToolStripMenuItem";
            this.configuraciónToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.configuraciónToolStripMenuItem.Text = "Configuración";
            this.configuraciónToolStripMenuItem.Click += new System.EventHandler(this.BtnConfiguracion_Click);
            // 
            // soporteToolStripMenuItem
            // 
            this.soporteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("soporteToolStripMenuItem.Image")));
            this.soporteToolStripMenuItem.Name = "soporteToolStripMenuItem";
            this.soporteToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.soporteToolStripMenuItem.Text = "Soporte";
            this.soporteToolStripMenuItem.Click += new System.EventHandler(this.btnSoporte_Click);
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("acercaDeToolStripMenuItem.Image")));
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.acercaDeToolStripMenuItem.Text = "Acerca De";
            this.acercaDeToolStripMenuItem.Click += new System.EventHandler(this.btnAcercade_Click);
            // 
            // cerrarSesiónToolStripMenuItem
            // 
            this.cerrarSesiónToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cerrarSesiónToolStripMenuItem.Image")));
            this.cerrarSesiónToolStripMenuItem.Name = "cerrarSesiónToolStripMenuItem";
            this.cerrarSesiónToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.cerrarSesiónToolStripMenuItem.Text = "Cerrar Sesión";
            this.cerrarSesiónToolStripMenuItem.Click += new System.EventHandler(this.BtnCerrarSesion_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("salirToolStripMenuItem.Image")));
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // panelContent
            // 
            this.panelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContent.BackColor = System.Drawing.Color.FloralWhite;
            this.panelContent.Controls.Add(this.panelNombres);
            this.panelContent.Controls.Add(this.textDownloadInfo);
            this.panelContent.Controls.Add(this.textVersionFrmPrincipal);
            this.panelContent.Controls.Add(this.panel2);
            this.panelContent.Controls.Add(this.pbCargaInicialFrmPrincipal);
            this.panelContent.Location = new System.Drawing.Point(210, 66);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(903, 663);
            this.panelContent.TabIndex = 17;
            // 
            // textDownloadInfo
            // 
            this.textDownloadInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textDownloadInfo.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textDownloadInfo.Location = new System.Drawing.Point(16, 603);
            this.textDownloadInfo.Name = "textDownloadInfo";
            this.textDownloadInfo.Size = new System.Drawing.Size(875, 19);
            this.textDownloadInfo.TabIndex = 10;
            this.textDownloadInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.textDownloadInfo.Visible = false;
            // 
            // textVersionFrmPrincipal
            // 
            this.textVersionFrmPrincipal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textVersionFrmPrincipal.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textVersionFrmPrincipal.Location = new System.Drawing.Point(696, 644);
            this.textVersionFrmPrincipal.Name = "textVersionFrmPrincipal";
            this.textVersionFrmPrincipal.Size = new System.Drawing.Size(207, 19);
            this.textVersionFrmPrincipal.TabIndex = 9;
            this.textVersionFrmPrincipal.Text = "Versión ";
            this.textVersionFrmPrincipal.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.btnTestLan);
            this.panel2.Controls.Add(this.imgSinDatos);
            this.panel2.Controls.Add(this.textPromotionsFrmPrincipal);
            this.panel2.Controls.Add(this.dataGridViewPromotionsFrmPincipal);
            this.panel2.Location = new System.Drawing.Point(12, 79);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(879, 498);
            this.panel2.TabIndex = 8;
            // 
            // btnTestLan
            // 
            this.btnTestLan.Location = new System.Drawing.Point(23, 12);
            this.btnTestLan.Name = "btnTestLan";
            this.btnTestLan.Size = new System.Drawing.Size(75, 23);
            this.btnTestLan.TabIndex = 14;
            this.btnTestLan.Text = "Test LAN";
            this.btnTestLan.UseVisualStyleBackColor = true;
            this.btnTestLan.Click += new System.EventHandler(this.btnTestLan_Click);
            // 
            // imgSinDatos
            // 
            this.imgSinDatos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imgSinDatos.Location = new System.Drawing.Point(270, 113);
            this.imgSinDatos.Name = "imgSinDatos";
            this.imgSinDatos.Size = new System.Drawing.Size(353, 285);
            this.imgSinDatos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgSinDatos.TabIndex = 13;
            this.imgSinDatos.TabStop = false;
            this.imgSinDatos.Click += new System.EventHandler(this.imgSinDatos_Click);
            // 
            // textPromotionsFrmPrincipal
            // 
            this.textPromotionsFrmPrincipal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textPromotionsFrmPrincipal.Font = new System.Drawing.Font("Roboto Black", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textPromotionsFrmPrincipal.Location = new System.Drawing.Point(18, 12);
            this.textPromotionsFrmPrincipal.Name = "textPromotionsFrmPrincipal";
            this.textPromotionsFrmPrincipal.Size = new System.Drawing.Size(845, 25);
            this.textPromotionsFrmPrincipal.TabIndex = 12;
            this.textPromotionsFrmPrincipal.Text = "Promociones";
            this.textPromotionsFrmPrincipal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.textPromotionsFrmPrincipal.Click += new System.EventHandler(this.textPromotionsFrmPrincipal_Click);
            // 
            // dataGridViewPromotionsFrmPincipal
            // 
            this.dataGridViewPromotionsFrmPincipal.AllowUserToAddRows = false;
            this.dataGridViewPromotionsFrmPincipal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewPromotionsFrmPincipal.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewPromotionsFrmPincipal.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewPromotionsFrmPincipal.BackgroundColor = System.Drawing.Color.FloralWhite;
            this.dataGridViewPromotionsFrmPincipal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.Moccasin;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewPromotionsFrmPincipal.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridViewPromotionsFrmPincipal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPromotionsFrmPincipal.ColumnHeadersVisible = false;
            this.dataGridViewPromotionsFrmPincipal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDgv,
            this.codeDgv,
            this.nameDgv,
            this.vencimientoDgv,
            this.minDgv,
            this.maxDgv,
            this.descuentoDgv});
            this.dataGridViewPromotionsFrmPincipal.Location = new System.Drawing.Point(18, 50);
            this.dataGridViewPromotionsFrmPincipal.MultiSelect = false;
            this.dataGridViewPromotionsFrmPincipal.Name = "dataGridViewPromotionsFrmPincipal";
            this.dataGridViewPromotionsFrmPincipal.ReadOnly = true;
            this.dataGridViewPromotionsFrmPincipal.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FloralWhite;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.MediumBlue;
            dataGridViewCellStyle14.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewPromotionsFrmPincipal.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridViewPromotionsFrmPincipal.RowHeadersVisible = false;
            this.dataGridViewPromotionsFrmPincipal.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewPromotionsFrmPincipal.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FloralWhite;
            this.dataGridViewPromotionsFrmPincipal.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewPromotionsFrmPincipal.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.DodgerBlue;
            this.dataGridViewPromotionsFrmPincipal.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(1, 4, 1, 4);
            this.dataGridViewPromotionsFrmPincipal.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewPromotionsFrmPincipal.RowTemplate.Height = 30;
            this.dataGridViewPromotionsFrmPincipal.RowTemplate.ReadOnly = true;
            this.dataGridViewPromotionsFrmPincipal.Size = new System.Drawing.Size(845, 425);
            this.dataGridViewPromotionsFrmPincipal.TabIndex = 11;
            this.dataGridViewPromotionsFrmPincipal.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPromotionsFrmPincipal_CellContentClick);
            this.dataGridViewPromotionsFrmPincipal.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridViewPromotionsFrmPincipal_Scroll);
            // 
            // idDgv
            // 
            this.idDgv.HeaderText = "Id";
            this.idDgv.Name = "idDgv";
            this.idDgv.ReadOnly = true;
            // 
            // codeDgv
            // 
            this.codeDgv.HeaderText = "Código";
            this.codeDgv.Name = "codeDgv";
            this.codeDgv.ReadOnly = true;
            // 
            // nameDgv
            // 
            this.nameDgv.HeaderText = "Nombre";
            this.nameDgv.Name = "nameDgv";
            this.nameDgv.ReadOnly = true;
            // 
            // vencimientoDgv
            // 
            this.vencimientoDgv.HeaderText = "Vencimiento";
            this.vencimientoDgv.Name = "vencimientoDgv";
            this.vencimientoDgv.ReadOnly = true;
            // 
            // minDgv
            // 
            this.minDgv.HeaderText = "Mínimo";
            this.minDgv.Name = "minDgv";
            this.minDgv.ReadOnly = true;
            // 
            // maxDgv
            // 
            this.maxDgv.HeaderText = "Máximo";
            this.maxDgv.Name = "maxDgv";
            this.maxDgv.ReadOnly = true;
            // 
            // descuentoDgv
            // 
            this.descuentoDgv.HeaderText = "Descuento";
            this.descuentoDgv.Name = "descuentoDgv";
            this.descuentoDgv.ReadOnly = true;
            // 
            // panelMenu
            // 
            this.panelMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelMenu.BackColor = System.Drawing.Color.Black;
            this.panelMenu.Controls.Add(this.btnIngresos);
            this.panelMenu.Controls.Add(this.btnTaras);
            this.panelMenu.Controls.Add(this.btnReportsFrmPrincipal);
            this.panelMenu.Controls.Add(this.btnVenta);
            this.panelMenu.Controls.Add(this.btnArticulos);
            this.panelMenu.Controls.Add(this.btnHistorialDocumentosFrmPrincipal);
            this.panelMenu.Controls.Add(this.btnCliente);
            this.panelMenu.Controls.Add(this.btnCorteDeCaja);
            this.panelMenu.Location = new System.Drawing.Point(0, 66);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(210, 663);
            this.panelMenu.TabIndex = 18;
            // 
            // btnIngresos
            // 
            this.btnIngresos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIngresos.BackColor = System.Drawing.Color.Black;
            this.btnIngresos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIngresos.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnIngresos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnIngresos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIngresos.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIngresos.ForeColor = System.Drawing.Color.White;
            this.btnIngresos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIngresos.Location = new System.Drawing.Point(3, 465);
            this.btnIngresos.Name = "btnIngresos";
            this.btnIngresos.Size = new System.Drawing.Size(201, 60);
            this.btnIngresos.TabIndex = 8;
            this.btnIngresos.Text = "Ingresos";
            this.btnIngresos.UseVisualStyleBackColor = false;
            this.btnIngresos.Click += new System.EventHandler(this.btnIngresos_Click);
            // 
            // btnTaras
            // 
            this.btnTaras.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTaras.BackColor = System.Drawing.Color.Black;
            this.btnTaras.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTaras.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnTaras.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnTaras.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaras.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaras.ForeColor = System.Drawing.Color.White;
            this.btnTaras.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTaras.Location = new System.Drawing.Point(3, 399);
            this.btnTaras.Name = "btnTaras";
            this.btnTaras.Size = new System.Drawing.Size(201, 60);
            this.btnTaras.TabIndex = 7;
            this.btnTaras.Text = "Cajas";
            this.btnTaras.UseVisualStyleBackColor = false;
            this.btnTaras.Click += new System.EventHandler(this.btnTaras_Click);
            // 
            // btnReportsFrmPrincipal
            // 
            this.btnReportsFrmPrincipal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReportsFrmPrincipal.BackColor = System.Drawing.Color.Black;
            this.btnReportsFrmPrincipal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReportsFrmPrincipal.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnReportsFrmPrincipal.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnReportsFrmPrincipal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportsFrmPrincipal.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReportsFrmPrincipal.ForeColor = System.Drawing.Color.White;
            this.btnReportsFrmPrincipal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReportsFrmPrincipal.Location = new System.Drawing.Point(3, 333);
            this.btnReportsFrmPrincipal.Name = "btnReportsFrmPrincipal";
            this.btnReportsFrmPrincipal.Size = new System.Drawing.Size(201, 60);
            this.btnReportsFrmPrincipal.TabIndex = 6;
            this.btnReportsFrmPrincipal.Text = "Reporte    \r\nCorte (F6)";
            this.btnReportsFrmPrincipal.UseVisualStyleBackColor = false;
            // 
            // btnVenta
            // 
            this.btnVenta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVenta.BackColor = System.Drawing.Color.Black;
            this.btnVenta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVenta.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnVenta.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnVenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVenta.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVenta.ForeColor = System.Drawing.Color.White;
            this.btnVenta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVenta.Location = new System.Drawing.Point(3, 3);
            this.btnVenta.Name = "btnVenta";
            this.btnVenta.Size = new System.Drawing.Size(201, 60);
            this.btnVenta.TabIndex = 1;
            this.btnVenta.Text = "Ventas (F2)";
            this.btnVenta.UseVisualStyleBackColor = false;
            // 
            // btnArticulos
            // 
            this.btnArticulos.BackColor = System.Drawing.Color.Black;
            this.btnArticulos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnArticulos.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnArticulos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnArticulos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnArticulos.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnArticulos.ForeColor = System.Drawing.Color.White;
            this.btnArticulos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnArticulos.Location = new System.Drawing.Point(3, 135);
            this.btnArticulos.Name = "btnArticulos";
            this.btnArticulos.Size = new System.Drawing.Size(201, 60);
            this.btnArticulos.TabIndex = 3;
            this.btnArticulos.Text = "Artículos";
            this.btnArticulos.UseVisualStyleBackColor = false;
            // 
            // btnHistorialDocumentosFrmPrincipal
            // 
            this.btnHistorialDocumentosFrmPrincipal.BackColor = System.Drawing.Color.Black;
            this.btnHistorialDocumentosFrmPrincipal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHistorialDocumentosFrmPrincipal.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnHistorialDocumentosFrmPrincipal.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnHistorialDocumentosFrmPrincipal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistorialDocumentosFrmPrincipal.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistorialDocumentosFrmPrincipal.ForeColor = System.Drawing.Color.White;
            this.btnHistorialDocumentosFrmPrincipal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHistorialDocumentosFrmPrincipal.Location = new System.Drawing.Point(3, 201);
            this.btnHistorialDocumentosFrmPrincipal.Name = "btnHistorialDocumentosFrmPrincipal";
            this.btnHistorialDocumentosFrmPrincipal.Size = new System.Drawing.Size(201, 60);
            this.btnHistorialDocumentosFrmPrincipal.TabIndex = 4;
            this.btnHistorialDocumentosFrmPrincipal.Text = "Historial de \r\nVentas (F3)";
            this.btnHistorialDocumentosFrmPrincipal.UseVisualStyleBackColor = false;
            // 
            // btnCliente
            // 
            this.btnCliente.BackColor = System.Drawing.Color.Black;
            this.btnCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCliente.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnCliente.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCliente.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCliente.ForeColor = System.Drawing.Color.White;
            this.btnCliente.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCliente.Location = new System.Drawing.Point(3, 69);
            this.btnCliente.Name = "btnCliente";
            this.btnCliente.Size = new System.Drawing.Size(201, 60);
            this.btnCliente.TabIndex = 2;
            this.btnCliente.Text = "Clientes";
            this.btnCliente.UseVisualStyleBackColor = false;
            // 
            // btnCorteDeCaja
            // 
            this.btnCorteDeCaja.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCorteDeCaja.BackColor = System.Drawing.Color.Black;
            this.btnCorteDeCaja.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCorteDeCaja.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnCorteDeCaja.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnCorteDeCaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCorteDeCaja.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCorteDeCaja.ForeColor = System.Drawing.Color.White;
            this.btnCorteDeCaja.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCorteDeCaja.Location = new System.Drawing.Point(3, 267);
            this.btnCorteDeCaja.Name = "btnCorteDeCaja";
            this.btnCorteDeCaja.Size = new System.Drawing.Size(201, 60);
            this.btnCorteDeCaja.TabIndex = 5;
            this.btnCorteDeCaja.Text = "Corte\r\nde Caja (F5)";
            this.btnCorteDeCaja.UseVisualStyleBackColor = false;
            // 
            // panelToolbar
            // 
            this.panelToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelToolbar.BackColor = System.Drawing.Color.Coral;
            this.panelToolbar.Controls.Add(this.pictureBoxLogoSuperior);
            this.panelToolbar.Controls.Add(this.menuStripFrmPrincipal);
            this.panelToolbar.Controls.Add(this.btnCerrar);
            this.panelToolbar.Location = new System.Drawing.Point(0, -2);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(1113, 70);
            this.panelToolbar.TabIndex = 19;
            // 
            // pictureBoxLogoSuperior
            // 
            this.pictureBoxLogoSuperior.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxLogoSuperior.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBoxLogoSuperior.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBoxLogoSuperior.Location = new System.Drawing.Point(1033, 0);
            this.pictureBoxLogoSuperior.Name = "pictureBoxLogoSuperior";
            this.pictureBoxLogoSuperior.Size = new System.Drawing.Size(80, 70);
            this.pictureBoxLogoSuperior.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLogoSuperior.TabIndex = 19;
            this.pictureBoxLogoSuperior.TabStop = false;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnCerrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCerrar.Location = new System.Drawing.Point(3, 4);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnCerrar.Size = new System.Drawing.Size(75, 63);
            this.btnCerrar.TabIndex = 20;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // panelNombres
            // 
            this.panelNombres.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelNombres.Controls.Add(this.panelNombresDos);
            this.panelNombres.Controls.Add(this.panelNombresUno);
            this.panelNombres.Location = new System.Drawing.Point(12, 3);
            this.panelNombres.Name = "panelNombres";
            this.panelNombres.Size = new System.Drawing.Size(879, 70);
            this.panelNombres.TabIndex = 11;
            // 
            // panelNombresUno
            // 
            this.panelNombresUno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNombresUno.Controls.Add(this.editNombreCajero);
            this.panelNombresUno.Controls.Add(this.label1);
            this.panelNombresUno.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelNombresUno.Location = new System.Drawing.Point(0, 0);
            this.panelNombresUno.Name = "panelNombresUno";
            this.panelNombresUno.Size = new System.Drawing.Size(430, 70);
            this.panelNombresUno.TabIndex = 0;
            // 
            // editNombreCajero
            // 
            this.editNombreCajero.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editNombreCajero.BackColor = System.Drawing.Color.FloralWhite;
            this.editNombreCajero.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editNombreCajero.Location = new System.Drawing.Point(93, 12);
            this.editNombreCajero.Multiline = true;
            this.editNombreCajero.Name = "editNombreCajero";
            this.editNombreCajero.ReadOnly = true;
            this.editNombreCajero.Size = new System.Drawing.Size(332, 46);
            this.editNombreCajero.TabIndex = 61;
            this.editNombreCajero.Text = "Nombre Cajero";
            this.editNombreCajero.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Roboto Black", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 43);
            this.label1.TabIndex = 59;
            this.label1.Text = "Cajero:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelNombresDos
            // 
            this.panelNombresDos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNombresDos.Controls.Add(this.editNombreCaja);
            this.panelNombresDos.Controls.Add(this.label2);
            this.panelNombresDos.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelNombresDos.Location = new System.Drawing.Point(462, 0);
            this.panelNombresDos.Name = "panelNombresDos";
            this.panelNombresDos.Size = new System.Drawing.Size(417, 70);
            this.panelNombresDos.TabIndex = 62;
            // 
            // editNombreCaja
            // 
            this.editNombreCaja.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editNombreCaja.BackColor = System.Drawing.Color.FloralWhite;
            this.editNombreCaja.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editNombreCaja.Location = new System.Drawing.Point(72, 13);
            this.editNombreCaja.Multiline = true;
            this.editNombreCaja.Name = "editNombreCaja";
            this.editNombreCaja.ReadOnly = true;
            this.editNombreCaja.Size = new System.Drawing.Size(341, 45);
            this.editNombreCaja.TabIndex = 64;
            this.editNombreCaja.Text = "Nombre Caja";
            this.editNombreCaja.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Roboto Black", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 43);
            this.label2.TabIndex = 63;
            this.label2.Text = "Caja:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AntiqueWhite;
            this.ClientSize = new System.Drawing.Size(1113, 729);
            this.Controls.Add(this.panelToolbar);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.panelContent);
            this.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(700, 480);
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menú Principal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPrincipalPrueba_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPrincipalPrueba_FormClosed);
            this.Load += new System.EventHandler(this.frmPrincipalPrueba_Load);
            this.Shown += new System.EventHandler(this.FormPrincipal_Shown);
            this.SizeChanged += new System.EventHandler(this.frmPrincipalPrueba_SizeChanged);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmPrincipalPrueba_KeyUp);
            this.Leave += new System.EventHandler(this.frmPrincipalPrueba_Leave);
            this.menuStripFrmPrincipal.ResumeLayout(false);
            this.menuStripFrmPrincipal.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPromotionsFrmPincipal)).EndInit();
            this.panelMenu.ResumeLayout(false);
            this.panelToolbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogoSuperior)).EndInit();
            this.panelNombres.ResumeLayout(false);
            this.panelNombresUno.ResumeLayout(false);
            this.panelNombresUno.PerformLayout();
            this.panelNombresDos.ResumeLayout(false);
            this.panelNombresDos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private System.ComponentModel.BackgroundWorker backgroundCargaInicial;
        public System.Windows.Forms.ProgressBar pbCargaInicialFrmPrincipal;
        private System.Windows.Forms.MenuStrip menuStripFrmPrincipal;
        private System.Windows.Forms.ToolStripMenuItem datosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opcionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cargaInicialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configuraciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem soporteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarSesiónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox imgSinDatos;
        private System.Windows.Forms.Label textPromotionsFrmPrincipal;
        private System.Windows.Forms.DataGridView dataGridViewPromotionsFrmPincipal;
        private System.Windows.Forms.Label textVersionFrmPrincipal;
        private System.Windows.Forms.ToolStripMenuItem actualizarDatosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem descargasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aperturaDeCajaToolStripMenuItem;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button btnReportsFrmPrincipal;
        private System.Windows.Forms.Button btnVenta;
        private System.Windows.Forms.Button btnArticulos;
        private System.Windows.Forms.Button btnHistorialDocumentosFrmPrincipal;
        private System.Windows.Forms.Button btnCliente;
        private System.Windows.Forms.Button btnCorteDeCaja;
        private System.Windows.Forms.PictureBox pictureBoxLogoSuperior;
        private System.Windows.Forms.Button btnTaras;
        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnTestLan;
        private System.Windows.Forms.Button btnIngresos;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn codeDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn vencimientoDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn minDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn descuentoDgv;
        private System.Windows.Forms.Label textDownloadInfo;
        private System.Windows.Forms.Panel panelNombres;
        private System.Windows.Forms.Panel panelNombresDos;
        private System.Windows.Forms.TextBox editNombreCaja;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox editNombreCajero;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelNombresUno;
    }
}