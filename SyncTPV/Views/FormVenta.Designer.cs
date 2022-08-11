using SyncTPV.Helpers;

namespace SyncTPV
{
    partial class FormVenta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVenta));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.btnSurtirPedidos = new System.Windows.Forms.Button();
            this.btnClearOptions = new System.Windows.Forms.Button();
            this.panelClienteToolbar = new System.Windows.Forms.Panel();
            this.panelCliente = new System.Windows.Forms.Panel();
            this.btnBuscarClientesNew = new System.Windows.Forms.Button();
            this.editNombreCliente = new System.Windows.Forms.TextBox();
            this.btnRecuperar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnPayWithCashFrmVenta = new System.Windows.Forms.Button();
            this.btnPausarDocumentoFrmVenta = new System.Windows.Forms.Button();
            this.imgCliente = new System.Windows.Forms.PictureBox();
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.panelImgItem = new System.Windows.Forms.Panel();
            this.imgItem = new System.Windows.Forms.PictureBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.textExistenciaReal = new System.Windows.Forms.Label();
            this.panelObservationMovement = new System.Windows.Forms.Panel();
            this.textObservationMovement = new System.Windows.Forms.Label();
            this.editObservationMovement = new System.Windows.Forms.TextBox();
            this.textFolioFrmVenta = new System.Windows.Forms.Label();
            this.textInfoDocumentTypeFrmVenta = new System.Windows.Forms.Label();
            this.comboBoxDocumentTypeFrmVenta = new System.Windows.Forms.ComboBox();
            this.panelFieldsVenta = new System.Windows.Forms.Panel();
            this.panel32 = new System.Windows.Forms.Panel();
            this.editUnidadNoConvertible = new System.Windows.Forms.TextBox();
            this.textUnidadDeMedida = new System.Windows.Forms.Label();
            this.btnOpenScale = new System.Windows.Forms.Button();
            this.editCapturedUnits = new System.Windows.Forms.TextBox();
            this.textUnidadNoConvertible = new System.Windows.Forms.Label();
            this.btnAgregar = new SyncTPV.RoundedButton();
            this.comboBoxUnitMWITemVenta = new System.Windows.Forms.ComboBox();
            this.textInfoCantidad = new System.Windows.Forms.Label();
            this.panelFieldsCodItemsVenta = new System.Windows.Forms.Panel();
            this.progressBarLoadPrices = new System.Windows.Forms.ProgressBar();
            this.pictureBoxInfoDescuento = new System.Windows.Forms.PictureBox();
            this.textDiscountRateInfoVenta = new System.Windows.Forms.Label();
            this.editPriceItemVenta = new System.Windows.Forms.TextBox();
            this.editDiscountItemVenta = new System.Windows.Forms.TextBox();
            this.comboCodigoItemVenta = new System.Windows.Forms.ComboBox();
            this.btnBuscarArticuloTeclado = new System.Windows.Forms.Button();
            this.editNombreItemVenta = new System.Windows.Forms.TextBox();
            this.comboPreciosItemVenta = new System.Windows.Forms.ComboBox();
            this.panel24 = new System.Windows.Forms.Panel();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBoxInfoSeleccionarPrecio = new System.Windows.Forms.PictureBox();
            this.btnBuscarClientes = new System.Windows.Forms.Button();
            this.txtClientes = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelTotales = new System.Windows.Forms.Panel();
            this.panelSubtotales = new System.Windows.Forms.Panel();
            this.editChangeFrmVenta = new System.Windows.Forms.Label();
            this.editPendingFrmVenta = new System.Windows.Forms.Label();
            this.textChangeFrmVenta = new System.Windows.Forms.Label();
            this.textPendingFrmVenta = new System.Windows.Forms.Label();
            this.textDiscountFrmVenta = new System.Windows.Forms.Label();
            this.textSubtotalFrmVenta = new System.Windows.Forms.Label();
            this.textInfoTotal = new System.Windows.Forms.Label();
            this.textTotalFrmVenta = new System.Windows.Forms.Label();
            this.dataGridMovements = new System.Windows.Forms.DataGridView();
            this.idMovimentDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Clave = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.piezasMovements = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalGeneral = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Eliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.itemIdDgvItems = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textTotalMovements = new System.Windows.Forms.Label();
            this.panelGridMovementsVenta = new System.Windows.Forms.Panel();
            this.imgSinDatosFrmVenta = new System.Windows.Forms.PictureBox();
            this.panelBotonCobrar = new System.Windows.Forms.Panel();
            this.textVersion = new System.Windows.Forms.Label();
            this.btnCobrarFrmVenta = new SyncTPV.RoundedButton();
            this.panelToolbar.SuspendLayout();
            this.panelClienteToolbar.SuspendLayout();
            this.panelCliente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCliente)).BeginInit();
            this.panelSuperior.SuspendLayout();
            this.panelImgItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgItem)).BeginInit();
            this.panel6.SuspendLayout();
            this.panelObservationMovement.SuspendLayout();
            this.panelFieldsVenta.SuspendLayout();
            this.panel32.SuspendLayout();
            this.panelFieldsCodItemsVenta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfoDescuento)).BeginInit();
            this.panel24.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfoSeleccionarPrecio)).BeginInit();
            this.panelTotales.SuspendLayout();
            this.panelSubtotales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMovements)).BeginInit();
            this.panelGridMovementsVenta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatosFrmVenta)).BeginInit();
            this.panelBotonCobrar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelToolbar
            // 
            this.panelToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelToolbar.BackColor = System.Drawing.Color.Coral;
            this.panelToolbar.Controls.Add(this.btnSurtirPedidos);
            this.panelToolbar.Controls.Add(this.btnClearOptions);
            this.panelToolbar.Controls.Add(this.panelClienteToolbar);
            this.panelToolbar.Controls.Add(this.btnRecuperar);
            this.panelToolbar.Controls.Add(this.btnCerrar);
            this.panelToolbar.Controls.Add(this.btnPayWithCashFrmVenta);
            this.panelToolbar.Controls.Add(this.btnPausarDocumentoFrmVenta);
            this.panelToolbar.Location = new System.Drawing.Point(0, 0);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(912, 70);
            this.panelToolbar.TabIndex = 0;
            // 
            // btnSurtirPedidos
            // 
            this.btnSurtirPedidos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSurtirPedidos.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSurtirPedidos.FlatAppearance.BorderSize = 0;
            this.btnSurtirPedidos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(20)))), ((int)(((byte)(224)))));
            this.btnSurtirPedidos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.btnSurtirPedidos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSurtirPedidos.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSurtirPedidos.ForeColor = System.Drawing.Color.White;
            this.btnSurtirPedidos.Image = ((System.Drawing.Image)(resources.GetObject("btnSurtirPedidos.Image")));
            this.btnSurtirPedidos.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSurtirPedidos.Location = new System.Drawing.Point(276, 5);
            this.btnSurtirPedidos.Name = "btnSurtirPedidos";
            this.btnSurtirPedidos.Size = new System.Drawing.Size(75, 62);
            this.btnSurtirPedidos.TabIndex = 20;
            this.btnSurtirPedidos.Text = "Surtir";
            this.btnSurtirPedidos.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSurtirPedidos.UseVisualStyleBackColor = true;
            this.btnSurtirPedidos.Click += new System.EventHandler(this.btnSurtirPedidosCotizaciones_Click);
            this.btnSurtirPedidos.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnSurtirPedidos_KeyUp);
            // 
            // btnClearOptions
            // 
            this.btnClearOptions.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearOptions.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnClearOptions.FlatAppearance.BorderSize = 0;
            this.btnClearOptions.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(20)))), ((int)(((byte)(224)))));
            this.btnClearOptions.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.btnClearOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearOptions.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearOptions.ForeColor = System.Drawing.Color.White;
            this.btnClearOptions.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClearOptions.Location = new System.Drawing.Point(12, 5);
            this.btnClearOptions.Name = "btnClearOptions";
            this.btnClearOptions.Size = new System.Drawing.Size(75, 62);
            this.btnClearOptions.TabIndex = 19;
            this.btnClearOptions.Text = "Limpiar";
            this.btnClearOptions.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClearOptions.UseVisualStyleBackColor = true;
            this.btnClearOptions.Click += new System.EventHandler(this.btnClearOptions_Click);
            this.btnClearOptions.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnClearOptions_KeyUp);
            // 
            // panelClienteToolbar
            // 
            this.panelClienteToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelClienteToolbar.Controls.Add(this.panelCliente);
            this.panelClienteToolbar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelClienteToolbar.Location = new System.Drawing.Point(526, 0);
            this.panelClienteToolbar.Name = "panelClienteToolbar";
            this.panelClienteToolbar.Size = new System.Drawing.Size(386, 70);
            this.panelClienteToolbar.TabIndex = 18;
            // 
            // panelCliente
            // 
            this.panelCliente.BackColor = System.Drawing.Color.Coral;
            this.panelCliente.Controls.Add(this.btnBuscarClientesNew);
            this.panelCliente.Controls.Add(this.editNombreCliente);
            this.panelCliente.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCliente.Location = new System.Drawing.Point(0, 0);
            this.panelCliente.Name = "panelCliente";
            this.panelCliente.Size = new System.Drawing.Size(386, 70);
            this.panelCliente.TabIndex = 4;
            // 
            // btnBuscarClientesNew
            // 
            this.btnBuscarClientesNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscarClientesNew.BackColor = System.Drawing.Color.MintCream;
            this.btnBuscarClientesNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarClientesNew.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarClientesNew.Image")));
            this.btnBuscarClientesNew.Location = new System.Drawing.Point(351, 23);
            this.btnBuscarClientesNew.Name = "btnBuscarClientesNew";
            this.btnBuscarClientesNew.Size = new System.Drawing.Size(29, 31);
            this.btnBuscarClientesNew.TabIndex = 76;
            this.btnBuscarClientesNew.UseVisualStyleBackColor = false;
            this.btnBuscarClientesNew.Click += new System.EventHandler(this.btnBuscarClientesNew_Click_1);
            this.btnBuscarClientesNew.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnBuscarClientesNew_KeyUp);
            // 
            // editNombreCliente
            // 
            this.editNombreCliente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editNombreCliente.BackColor = System.Drawing.Color.Azure;
            this.editNombreCliente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editNombreCliente.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editNombreCliente.Location = new System.Drawing.Point(61, 6);
            this.editNombreCliente.Multiline = true;
            this.editNombreCliente.Name = "editNombreCliente";
            this.editNombreCliente.ReadOnly = true;
            this.editNombreCliente.Size = new System.Drawing.Size(284, 61);
            this.editNombreCliente.TabIndex = 72;
            this.editNombreCliente.Text = "Cliente No Encontrado";
            this.editNombreCliente.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtClientesNew_KeyUp);
            // 
            // btnRecuperar
            // 
            this.btnRecuperar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRecuperar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnRecuperar.FlatAppearance.BorderSize = 0;
            this.btnRecuperar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(20)))), ((int)(((byte)(224)))));
            this.btnRecuperar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.btnRecuperar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecuperar.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecuperar.ForeColor = System.Drawing.Color.White;
            this.btnRecuperar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRecuperar.Location = new System.Drawing.Point(174, 5);
            this.btnRecuperar.Name = "btnRecuperar";
            this.btnRecuperar.Size = new System.Drawing.Size(92, 62);
            this.btnRecuperar.TabIndex = 17;
            this.btnRecuperar.Text = "Recuperar";
            this.btnRecuperar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRecuperar.UseVisualStyleBackColor = true;
            this.btnRecuperar.Click += new System.EventHandler(this.btnAbrir_Click);
            this.btnRecuperar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnAbrir_KeyUp);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(20)))), ((int)(((byte)(224)))));
            this.btnCerrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCerrar.Location = new System.Drawing.Point(445, 5);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(75, 62);
            this.btnCerrar.TabIndex = 16;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.PictureBox4_Click);
            this.btnCerrar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnCerrar_KeyUp);
            // 
            // btnPayWithCashFrmVenta
            // 
            this.btnPayWithCashFrmVenta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPayWithCashFrmVenta.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnPayWithCashFrmVenta.FlatAppearance.BorderSize = 0;
            this.btnPayWithCashFrmVenta.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(20)))), ((int)(((byte)(224)))));
            this.btnPayWithCashFrmVenta.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.btnPayWithCashFrmVenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayWithCashFrmVenta.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPayWithCashFrmVenta.ForeColor = System.Drawing.Color.White;
            this.btnPayWithCashFrmVenta.Image = ((System.Drawing.Image)(resources.GetObject("btnPayWithCashFrmVenta.Image")));
            this.btnPayWithCashFrmVenta.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPayWithCashFrmVenta.Location = new System.Drawing.Point(357, 5);
            this.btnPayWithCashFrmVenta.Name = "btnPayWithCashFrmVenta";
            this.btnPayWithCashFrmVenta.Size = new System.Drawing.Size(82, 62);
            this.btnPayWithCashFrmVenta.TabIndex = 15;
            this.btnPayWithCashFrmVenta.Text = "Efectivo";
            this.btnPayWithCashFrmVenta.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPayWithCashFrmVenta.UseVisualStyleBackColor = true;
            this.btnPayWithCashFrmVenta.Click += new System.EventHandler(this.PictureBox3_Click);
            // 
            // btnPausarDocumentoFrmVenta
            // 
            this.btnPausarDocumentoFrmVenta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPausarDocumentoFrmVenta.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnPausarDocumentoFrmVenta.FlatAppearance.BorderSize = 0;
            this.btnPausarDocumentoFrmVenta.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(20)))), ((int)(((byte)(224)))));
            this.btnPausarDocumentoFrmVenta.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.btnPausarDocumentoFrmVenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPausarDocumentoFrmVenta.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPausarDocumentoFrmVenta.ForeColor = System.Drawing.Color.White;
            this.btnPausarDocumentoFrmVenta.Image = ((System.Drawing.Image)(resources.GetObject("btnPausarDocumentoFrmVenta.Image")));
            this.btnPausarDocumentoFrmVenta.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPausarDocumentoFrmVenta.Location = new System.Drawing.Point(93, 5);
            this.btnPausarDocumentoFrmVenta.Name = "btnPausarDocumentoFrmVenta";
            this.btnPausarDocumentoFrmVenta.Size = new System.Drawing.Size(75, 62);
            this.btnPausarDocumentoFrmVenta.TabIndex = 13;
            this.btnPausarDocumentoFrmVenta.Text = "Pausar";
            this.btnPausarDocumentoFrmVenta.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPausarDocumentoFrmVenta.UseVisualStyleBackColor = true;
            this.btnPausarDocumentoFrmVenta.Click += new System.EventHandler(this.btnPausar_Click);
            this.btnPausarDocumentoFrmVenta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnPausarDocumentoFrmVenta_KeyUp);
            // 
            // imgCliente
            // 
            this.imgCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgCliente.Image = ((System.Drawing.Image)(resources.GetObject("imgCliente.Image")));
            this.imgCliente.Location = new System.Drawing.Point(529, 6);
            this.imgCliente.Name = "imgCliente";
            this.imgCliente.Size = new System.Drawing.Size(55, 61);
            this.imgCliente.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgCliente.TabIndex = 77;
            this.imgCliente.TabStop = false;
            this.toolTip1.SetToolTip(this.imgCliente, "Selecciona el cliente a \r\nrealizar la venta. ");
            this.imgCliente.Click += new System.EventHandler(this.imgCliente_Click);
            // 
            // panelSuperior
            // 
            this.panelSuperior.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSuperior.AutoScroll = true;
            this.panelSuperior.BackColor = System.Drawing.Color.FloralWhite;
            this.panelSuperior.Controls.Add(this.panelImgItem);
            this.panelSuperior.Controls.Add(this.panel6);
            this.panelSuperior.Controls.Add(this.panelFieldsVenta);
            this.panelSuperior.Location = new System.Drawing.Point(0, 70);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(912, 266);
            this.panelSuperior.TabIndex = 1;
            // 
            // panelImgItem
            // 
            this.panelImgItem.Controls.Add(this.imgItem);
            this.panelImgItem.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelImgItem.Location = new System.Drawing.Point(0, 0);
            this.panelImgItem.Name = "panelImgItem";
            this.panelImgItem.Size = new System.Drawing.Size(172, 266);
            this.panelImgItem.TabIndex = 81;
            // 
            // imgItem
            // 
            this.imgItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgItem.Image = ((System.Drawing.Image)(resources.GetObject("imgItem.Image")));
            this.imgItem.Location = new System.Drawing.Point(7, 55);
            this.imgItem.Margin = new System.Windows.Forms.Padding(10);
            this.imgItem.Name = "imgItem";
            this.imgItem.Size = new System.Drawing.Size(155, 131);
            this.imgItem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgItem.TabIndex = 82;
            this.imgItem.TabStop = false;
            this.imgItem.Click += new System.EventHandler(this.imgItem_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.textExistenciaReal);
            this.panel6.Controls.Add(this.panelObservationMovement);
            this.panel6.Controls.Add(this.textFolioFrmVenta);
            this.panel6.Controls.Add(this.textInfoDocumentTypeFrmVenta);
            this.panel6.Controls.Add(this.comboBoxDocumentTypeFrmVenta);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(619, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(293, 266);
            this.panel6.TabIndex = 80;
            // 
            // textExistenciaReal
            // 
            this.textExistenciaReal.AutoSize = true;
            this.textExistenciaReal.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textExistenciaReal.Location = new System.Drawing.Point(8, 240);
            this.textExistenciaReal.Name = "textExistenciaReal";
            this.textExistenciaReal.Size = new System.Drawing.Size(62, 14);
            this.textExistenciaReal.TabIndex = 86;
            this.textExistenciaReal.Text = "Existencia";
            // 
            // panelObservationMovement
            // 
            this.panelObservationMovement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelObservationMovement.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelObservationMovement.Controls.Add(this.textObservationMovement);
            this.panelObservationMovement.Controls.Add(this.editObservationMovement);
            this.panelObservationMovement.Location = new System.Drawing.Point(11, 121);
            this.panelObservationMovement.Name = "panelObservationMovement";
            this.panelObservationMovement.Size = new System.Drawing.Size(264, 97);
            this.panelObservationMovement.TabIndex = 85;
            this.panelObservationMovement.Visible = false;
            // 
            // textObservationMovement
            // 
            this.textObservationMovement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textObservationMovement.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textObservationMovement.Location = new System.Drawing.Point(10, 6);
            this.textObservationMovement.Name = "textObservationMovement";
            this.textObservationMovement.Size = new System.Drawing.Size(237, 23);
            this.textObservationMovement.TabIndex = 88;
            this.textObservationMovement.Text = "Observacion del Movimiento";
            // 
            // editObservationMovement
            // 
            this.editObservationMovement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editObservationMovement.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editObservationMovement.Location = new System.Drawing.Point(10, 32);
            this.editObservationMovement.Multiline = true;
            this.editObservationMovement.Name = "editObservationMovement";
            this.editObservationMovement.Size = new System.Drawing.Size(237, 58);
            this.editObservationMovement.TabIndex = 87;
            this.editObservationMovement.KeyUp += new System.Windows.Forms.KeyEventHandler(this.editObservationMovement_KeyUp);
            // 
            // textFolioFrmVenta
            // 
            this.textFolioFrmVenta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textFolioFrmVenta.Font = new System.Drawing.Font("Roboto Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textFolioFrmVenta.Location = new System.Drawing.Point(13, 85);
            this.textFolioFrmVenta.Name = "textFolioFrmVenta";
            this.textFolioFrmVenta.Size = new System.Drawing.Size(266, 23);
            this.textFolioFrmVenta.TabIndex = 84;
            this.textFolioFrmVenta.Text = "Folio #";
            this.textFolioFrmVenta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textInfoDocumentTypeFrmVenta
            // 
            this.textInfoDocumentTypeFrmVenta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textInfoDocumentTypeFrmVenta.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.textInfoDocumentTypeFrmVenta.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInfoDocumentTypeFrmVenta.Location = new System.Drawing.Point(48, 5);
            this.textInfoDocumentTypeFrmVenta.Name = "textInfoDocumentTypeFrmVenta";
            this.textInfoDocumentTypeFrmVenta.Size = new System.Drawing.Size(192, 23);
            this.textInfoDocumentTypeFrmVenta.TabIndex = 83;
            this.textInfoDocumentTypeFrmVenta.Text = "Tipo de Documento";
            this.textInfoDocumentTypeFrmVenta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxDocumentTypeFrmVenta
            // 
            this.comboBoxDocumentTypeFrmVenta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDocumentTypeFrmVenta.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.comboBoxDocumentTypeFrmVenta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxDocumentTypeFrmVenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDocumentTypeFrmVenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxDocumentTypeFrmVenta.Font = new System.Drawing.Font("Roboto Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxDocumentTypeFrmVenta.FormattingEnabled = true;
            this.comboBoxDocumentTypeFrmVenta.Location = new System.Drawing.Point(11, 42);
            this.comboBoxDocumentTypeFrmVenta.Name = "comboBoxDocumentTypeFrmVenta";
            this.comboBoxDocumentTypeFrmVenta.Size = new System.Drawing.Size(268, 27);
            this.comboBoxDocumentTypeFrmVenta.TabIndex = 82;
            this.comboBoxDocumentTypeFrmVenta.SelectedIndexChanged += new System.EventHandler(this.comboBoxDocumentTypeFrmVenta_SelectedIndexChanged);
            this.comboBoxDocumentTypeFrmVenta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.comboBoxDocumentTypeFrmVenta_KeyUp);
            // 
            // panelFieldsVenta
            // 
            this.panelFieldsVenta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFieldsVenta.BackColor = System.Drawing.Color.FloralWhite;
            this.panelFieldsVenta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelFieldsVenta.Controls.Add(this.panel32);
            this.panelFieldsVenta.Controls.Add(this.panelFieldsCodItemsVenta);
            this.panelFieldsVenta.Controls.Add(this.panel24);
            this.panelFieldsVenta.Location = new System.Drawing.Point(180, 2);
            this.panelFieldsVenta.Margin = new System.Windows.Forms.Padding(5);
            this.panelFieldsVenta.MaximumSize = new System.Drawing.Size(700, 500);
            this.panelFieldsVenta.Name = "panelFieldsVenta";
            this.panelFieldsVenta.Size = new System.Drawing.Size(431, 259);
            this.panelFieldsVenta.TabIndex = 1;
            // 
            // panel32
            // 
            this.panel32.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel32.Controls.Add(this.editUnidadNoConvertible);
            this.panel32.Controls.Add(this.textUnidadDeMedida);
            this.panel32.Controls.Add(this.btnOpenScale);
            this.panel32.Controls.Add(this.editCapturedUnits);
            this.panel32.Controls.Add(this.textUnidadNoConvertible);
            this.panel32.Controls.Add(this.btnAgregar);
            this.panel32.Controls.Add(this.comboBoxUnitMWITemVenta);
            this.panel32.Controls.Add(this.textInfoCantidad);
            this.panel32.Location = new System.Drawing.Point(3, 146);
            this.panel32.Name = "panel32";
            this.panel32.Size = new System.Drawing.Size(419, 106);
            this.panel32.TabIndex = 84;
            // 
            // editUnidadNoConvertible
            // 
            this.editUnidadNoConvertible.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editUnidadNoConvertible.Location = new System.Drawing.Point(12, 18);
            this.editUnidadNoConvertible.Name = "editUnidadNoConvertible";
            this.editUnidadNoConvertible.Size = new System.Drawing.Size(125, 21);
            this.editUnidadNoConvertible.TabIndex = 73;
            this.editUnidadNoConvertible.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.editUnidadNoConvertible.Visible = false;
            this.editUnidadNoConvertible.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editUnidadNoConvertible_KeyPress);
            this.editUnidadNoConvertible.KeyUp += new System.Windows.Forms.KeyEventHandler(this.editUnidadNoConvertible_KeyUp);
            // 
            // textUnidadDeMedida
            // 
            this.textUnidadDeMedida.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textUnidadDeMedida.AutoSize = true;
            this.textUnidadDeMedida.Font = new System.Drawing.Font("Roboto Black", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textUnidadDeMedida.Location = new System.Drawing.Point(267, 4);
            this.textUnidadDeMedida.Name = "textUnidadDeMedida";
            this.textUnidadDeMedida.Size = new System.Drawing.Size(125, 13);
            this.textUnidadDeMedida.TabIndex = 74;
            this.textUnidadDeMedida.Text = "Unidad de Medida/Peso";
            // 
            // btnOpenScale
            // 
            this.btnOpenScale.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnOpenScale.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenScale.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.btnOpenScale.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenScale.Image = global::SyncTPV.Properties.Resources.scale;
            this.btnOpenScale.Location = new System.Drawing.Point(153, 66);
            this.btnOpenScale.Name = "btnOpenScale";
            this.btnOpenScale.Size = new System.Drawing.Size(38, 25);
            this.btnOpenScale.TabIndex = 84;
            this.btnOpenScale.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOpenScale.UseVisualStyleBackColor = false;
            this.btnOpenScale.Click += new System.EventHandler(this.btnOpenScale_Click);
            // 
            // editCapturedUnits
            // 
            this.editCapturedUnits.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editCapturedUnits.Location = new System.Drawing.Point(9, 68);
            this.editCapturedUnits.Name = "editCapturedUnits";
            this.editCapturedUnits.Size = new System.Drawing.Size(128, 22);
            this.editCapturedUnits.TabIndex = 83;
            this.editCapturedUnits.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.editCapturedUnits.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editCapturedUnits_KeyPress);
            this.editCapturedUnits.KeyUp += new System.Windows.Forms.KeyEventHandler(this.editCapturedUnits_KeyUp);
            // 
            // textUnidadNoConvertible
            // 
            this.textUnidadNoConvertible.AutoSize = true;
            this.textUnidadNoConvertible.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textUnidadNoConvertible.Location = new System.Drawing.Point(9, 2);
            this.textUnidadNoConvertible.Name = "textUnidadNoConvertible";
            this.textUnidadNoConvertible.Size = new System.Drawing.Size(96, 14);
            this.textUnidadNoConvertible.TabIndex = 72;
            this.textUnidadNoConvertible.Text = "Unidad no Conv.";
            this.textUnidadNoConvertible.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.textUnidadNoConvertible.Visible = false;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregar.BackColor = System.Drawing.Color.Transparent;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnAgregar.FlatAppearance.BorderSize = 2;
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.Font = new System.Drawing.Font("Roboto Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Location = new System.Drawing.Point(206, 56);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(210, 45);
            this.btnAgregar.TabIndex = 8;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnAgregar_KeyUp);
            // 
            // comboBoxUnitMWITemVenta
            // 
            this.comboBoxUnitMWITemVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxUnitMWITemVenta.BackColor = System.Drawing.Color.LightBlue;
            this.comboBoxUnitMWITemVenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUnitMWITemVenta.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBoxUnitMWITemVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxUnitMWITemVenta.FormattingEnabled = true;
            this.comboBoxUnitMWITemVenta.Location = new System.Drawing.Point(233, 20);
            this.comboBoxUnitMWITemVenta.Name = "comboBoxUnitMWITemVenta";
            this.comboBoxUnitMWITemVenta.Size = new System.Drawing.Size(178, 23);
            this.comboBoxUnitMWITemVenta.TabIndex = 5;
            this.comboBoxUnitMWITemVenta.SelectedIndexChanged += new System.EventHandler(this.comboBoxUnitMWITemVenta_SelectedIndexChanged);
            this.comboBoxUnitMWITemVenta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.comboBoxUnitMWITemVenta_KeyUp);
            // 
            // textInfoCantidad
            // 
            this.textInfoCantidad.AutoSize = true;
            this.textInfoCantidad.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInfoCantidad.Location = new System.Drawing.Point(8, 42);
            this.textInfoCantidad.Name = "textInfoCantidad";
            this.textInfoCantidad.Size = new System.Drawing.Size(67, 18);
            this.textInfoCantidad.TabIndex = 82;
            this.textInfoCantidad.Text = "Cantidad";
            this.textInfoCantidad.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // panelFieldsCodItemsVenta
            // 
            this.panelFieldsCodItemsVenta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFieldsCodItemsVenta.BackColor = System.Drawing.Color.FloralWhite;
            this.panelFieldsCodItemsVenta.Controls.Add(this.progressBarLoadPrices);
            this.panelFieldsCodItemsVenta.Controls.Add(this.pictureBoxInfoDescuento);
            this.panelFieldsCodItemsVenta.Controls.Add(this.textDiscountRateInfoVenta);
            this.panelFieldsCodItemsVenta.Controls.Add(this.editPriceItemVenta);
            this.panelFieldsCodItemsVenta.Controls.Add(this.editDiscountItemVenta);
            this.panelFieldsCodItemsVenta.Controls.Add(this.comboCodigoItemVenta);
            this.panelFieldsCodItemsVenta.Controls.Add(this.btnBuscarArticuloTeclado);
            this.panelFieldsCodItemsVenta.Controls.Add(this.editNombreItemVenta);
            this.panelFieldsCodItemsVenta.Controls.Add(this.comboPreciosItemVenta);
            this.panelFieldsCodItemsVenta.Location = new System.Drawing.Point(48, 6);
            this.panelFieldsCodItemsVenta.Name = "panelFieldsCodItemsVenta";
            this.panelFieldsCodItemsVenta.Size = new System.Drawing.Size(374, 134);
            this.panelFieldsCodItemsVenta.TabIndex = 79;
            // 
            // progressBarLoadPrices
            // 
            this.progressBarLoadPrices.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.progressBarLoadPrices.Location = new System.Drawing.Point(174, 111);
            this.progressBarLoadPrices.Name = "progressBarLoadPrices";
            this.progressBarLoadPrices.Size = new System.Drawing.Size(35, 18);
            this.progressBarLoadPrices.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBarLoadPrices.TabIndex = 89;
            this.progressBarLoadPrices.Visible = false;
            // 
            // pictureBoxInfoDescuento
            // 
            this.pictureBoxInfoDescuento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxInfoDescuento.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxInfoDescuento.Image")));
            this.pictureBoxInfoDescuento.Location = new System.Drawing.Point(222, 75);
            this.pictureBoxInfoDescuento.Name = "pictureBoxInfoDescuento";
            this.pictureBoxInfoDescuento.Size = new System.Drawing.Size(29, 24);
            this.pictureBoxInfoDescuento.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxInfoDescuento.TabIndex = 79;
            this.pictureBoxInfoDescuento.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBoxInfoDescuento, "Lista de precios que tiene \r\nel articulo. \r\nNOTA: Si el usuario tiene permisos \r\n" +
        "para cambiar el precio  tendrá \r\nactivado esta casilla. ");
            // 
            // textDiscountRateInfoVenta
            // 
            this.textDiscountRateInfoVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textDiscountRateInfoVenta.AutoSize = true;
            this.textDiscountRateInfoVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textDiscountRateInfoVenta.Location = new System.Drawing.Point(341, 80);
            this.textDiscountRateInfoVenta.Name = "textDiscountRateInfoVenta";
            this.textDiscountRateInfoVenta.Size = new System.Drawing.Size(21, 17);
            this.textDiscountRateInfoVenta.TabIndex = 88;
            this.textDiscountRateInfoVenta.Text = "%";
            // 
            // editPriceItemVenta
            // 
            this.editPriceItemVenta.BackColor = System.Drawing.Color.LightGreen;
            this.editPriceItemVenta.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.editPriceItemVenta.Font = new System.Drawing.Font("Roboto Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editPriceItemVenta.Location = new System.Drawing.Point(3, 104);
            this.editPriceItemVenta.Name = "editPriceItemVenta";
            this.editPriceItemVenta.Size = new System.Drawing.Size(165, 27);
            this.editPriceItemVenta.TabIndex = 86;
            this.editPriceItemVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.editPriceItemVenta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editPriceItemVenta_KeyPress);
            // 
            // editDiscountItemVenta
            // 
            this.editDiscountItemVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editDiscountItemVenta.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.editDiscountItemVenta.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editDiscountItemVenta.Location = new System.Drawing.Point(257, 77);
            this.editDiscountItemVenta.Name = "editDiscountItemVenta";
            this.editDiscountItemVenta.Size = new System.Drawing.Size(78, 23);
            this.editDiscountItemVenta.TabIndex = 87;
            this.editDiscountItemVenta.Text = "0";
            this.editDiscountItemVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.editDiscountItemVenta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editDiscountItemVenta_KeyPress);
            this.editDiscountItemVenta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.editDiscountItemVenta_KeyUp);
            // 
            // comboCodigoItemVenta
            // 
            this.comboCodigoItemVenta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboCodigoItemVenta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.comboCodigoItemVenta.BackColor = System.Drawing.Color.Khaki;
            this.comboCodigoItemVenta.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboCodigoItemVenta.FormattingEnabled = true;
            this.comboCodigoItemVenta.IntegralHeight = false;
            this.comboCodigoItemVenta.Location = new System.Drawing.Point(3, 3);
            this.comboCodigoItemVenta.Name = "comboCodigoItemVenta";
            this.comboCodigoItemVenta.Size = new System.Drawing.Size(363, 27);
            this.comboCodigoItemVenta.TabIndex = 71;
            this.comboCodigoItemVenta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboCodigoItemVenta_KeyPress);
            this.comboCodigoItemVenta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.comboCodigoItemVenta_KeyUp);
            // 
            // btnBuscarArticuloTeclado
            // 
            this.btnBuscarArticuloTeclado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscarArticuloTeclado.BackColor = System.Drawing.Color.White;
            this.btnBuscarArticuloTeclado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarArticuloTeclado.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnBuscarArticuloTeclado.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnBuscarArticuloTeclado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarArticuloTeclado.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarArticuloTeclado.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarArticuloTeclado.Image")));
            this.btnBuscarArticuloTeclado.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscarArticuloTeclado.Location = new System.Drawing.Point(324, 37);
            this.btnBuscarArticuloTeclado.Name = "btnBuscarArticuloTeclado";
            this.btnBuscarArticuloTeclado.Size = new System.Drawing.Size(46, 29);
            this.btnBuscarArticuloTeclado.TabIndex = 3;
            this.btnBuscarArticuloTeclado.Text = "F3";
            this.btnBuscarArticuloTeclado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscarArticuloTeclado.UseVisualStyleBackColor = false;
            this.btnBuscarArticuloTeclado.Click += new System.EventHandler(this.BtnBuscarArticuloTeclado_Click);
            this.btnBuscarArticuloTeclado.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnBuscarArticuloTeclado_KeyUp);
            // 
            // editNombreItemVenta
            // 
            this.editNombreItemVenta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editNombreItemVenta.BackColor = System.Drawing.Color.Khaki;
            this.editNombreItemVenta.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.editNombreItemVenta.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editNombreItemVenta.Location = new System.Drawing.Point(3, 37);
            this.editNombreItemVenta.Name = "editNombreItemVenta";
            this.editNombreItemVenta.Size = new System.Drawing.Size(315, 27);
            this.editNombreItemVenta.TabIndex = 2;
            this.editNombreItemVenta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarProductoTeclado_KeyPress);
            this.editNombreItemVenta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.editNombreItemVenta_KeyUp);
            // 
            // comboPreciosItemVenta
            // 
            this.comboPreciosItemVenta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboPreciosItemVenta.BackColor = System.Drawing.Color.LightGreen;
            this.comboPreciosItemVenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPreciosItemVenta.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboPreciosItemVenta.Font = new System.Drawing.Font("Roboto Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboPreciosItemVenta.FormattingEnabled = true;
            this.comboPreciosItemVenta.Location = new System.Drawing.Point(3, 72);
            this.comboPreciosItemVenta.Name = "comboPreciosItemVenta";
            this.comboPreciosItemVenta.Size = new System.Drawing.Size(185, 27);
            this.comboPreciosItemVenta.TabIndex = 4;
            this.comboPreciosItemVenta.SelectedIndexChanged += new System.EventHandler(this.CmbPreciosNew_SelectedIndexChanged);
            this.comboPreciosItemVenta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.comboPreciosItemVenta_KeyUp);
            // 
            // panel24
            // 
            this.panel24.BackColor = System.Drawing.Color.FloralWhite;
            this.panel24.Controls.Add(this.pictureBox10);
            this.panel24.Controls.Add(this.pictureBox6);
            this.panel24.Controls.Add(this.pictureBoxInfoSeleccionarPrecio);
            this.panel24.Location = new System.Drawing.Point(6, 6);
            this.panel24.Name = "panel24";
            this.panel24.Size = new System.Drawing.Size(36, 99);
            this.panel24.TabIndex = 1;
            // 
            // pictureBox10
            // 
            this.pictureBox10.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox10.Image")));
            this.pictureBox10.Location = new System.Drawing.Point(4, 37);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(28, 27);
            this.pictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox10.TabIndex = 78;
            this.pictureBox10.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox10, "Permite ingresar la cantidad\r\ny el articulo al mismo tiempo\r\nEjemplo 5 * M002\r\n(C" +
        "antidad * Articulo).\r\n");
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.Location = new System.Drawing.Point(4, 5);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(28, 26);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 73;
            this.pictureBox6.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox6, "Ingrese el código del\r\narticulo a vender. ");
            // 
            // pictureBoxInfoSeleccionarPrecio
            // 
            this.pictureBoxInfoSeleccionarPrecio.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxInfoSeleccionarPrecio.Image")));
            this.pictureBoxInfoSeleccionarPrecio.Location = new System.Drawing.Point(3, 70);
            this.pictureBoxInfoSeleccionarPrecio.Name = "pictureBoxInfoSeleccionarPrecio";
            this.pictureBoxInfoSeleccionarPrecio.Size = new System.Drawing.Size(29, 24);
            this.pictureBoxInfoSeleccionarPrecio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxInfoSeleccionarPrecio.TabIndex = 74;
            this.pictureBoxInfoSeleccionarPrecio.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBoxInfoSeleccionarPrecio, "Lista de precios que tiene \r\nel articulo. \r\nNOTA: Si el usuario tiene permisos \r\n" +
        "para cambiar el precio  tendrá \r\nactivado esta casilla. ");
            // 
            // btnBuscarClientes
            // 
            this.btnBuscarClientes.BackColor = System.Drawing.Color.Black;
            this.btnBuscarClientes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBuscarClientes.Font = new System.Drawing.Font("Calibri", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarClientes.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnBuscarClientes.Location = new System.Drawing.Point(1449, 213);
            this.btnBuscarClientes.Name = "btnBuscarClientes";
            this.btnBuscarClientes.Size = new System.Drawing.Size(10, 33);
            this.btnBuscarClientes.TabIndex = 63;
            this.btnBuscarClientes.Text = "Buscar   F8";
            this.btnBuscarClientes.UseVisualStyleBackColor = false;
            // 
            // txtClientes
            // 
            this.txtClientes.Font = new System.Drawing.Font("Calibri", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClientes.Location = new System.Drawing.Point(1141, 213);
            this.txtClientes.Name = "txtClientes";
            this.txtClientes.Size = new System.Drawing.Size(83, 29);
            this.txtClientes.TabIndex = 62;
            this.txtClientes.Text = "PUBLICO GENERAL";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1047, 248);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 23);
            this.label4.TabIndex = 65;
            this.label4.Text = "Articulos:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1054, 213);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 23);
            this.label5.TabIndex = 64;
            this.label5.Text = "Clientes:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // panelTotales
            // 
            this.panelTotales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTotales.AutoScroll = true;
            this.panelTotales.BackColor = System.Drawing.Color.Honeydew;
            this.panelTotales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTotales.Controls.Add(this.panelSubtotales);
            this.panelTotales.Controls.Add(this.textInfoTotal);
            this.panelTotales.Controls.Add(this.textTotalFrmVenta);
            this.panelTotales.Location = new System.Drawing.Point(610, 344);
            this.panelTotales.Margin = new System.Windows.Forms.Padding(5);
            this.panelTotales.Name = "panelTotales";
            this.panelTotales.Size = new System.Drawing.Size(298, 172);
            this.panelTotales.TabIndex = 79;
            // 
            // panelSubtotales
            // 
            this.panelSubtotales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSubtotales.BackColor = System.Drawing.Color.Honeydew;
            this.panelSubtotales.Controls.Add(this.editChangeFrmVenta);
            this.panelSubtotales.Controls.Add(this.editPendingFrmVenta);
            this.panelSubtotales.Controls.Add(this.textChangeFrmVenta);
            this.panelSubtotales.Controls.Add(this.textPendingFrmVenta);
            this.panelSubtotales.Controls.Add(this.textDiscountFrmVenta);
            this.panelSubtotales.Controls.Add(this.textSubtotalFrmVenta);
            this.panelSubtotales.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelSubtotales.Location = new System.Drawing.Point(10, 3);
            this.panelSubtotales.Name = "panelSubtotales";
            this.panelSubtotales.Size = new System.Drawing.Size(256, 97);
            this.panelSubtotales.TabIndex = 82;
            // 
            // editChangeFrmVenta
            // 
            this.editChangeFrmVenta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editChangeFrmVenta.BackColor = System.Drawing.Color.LightGreen;
            this.editChangeFrmVenta.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editChangeFrmVenta.Location = new System.Drawing.Point(126, 151);
            this.editChangeFrmVenta.Name = "editChangeFrmVenta";
            this.editChangeFrmVenta.Size = new System.Drawing.Size(124, 31);
            this.editChangeFrmVenta.TabIndex = 37;
            this.editChangeFrmVenta.Text = "$0.00";
            this.editChangeFrmVenta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.editChangeFrmVenta.Visible = false;
            // 
            // editPendingFrmVenta
            // 
            this.editPendingFrmVenta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editPendingFrmVenta.BackColor = System.Drawing.Color.LightGreen;
            this.editPendingFrmVenta.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editPendingFrmVenta.Location = new System.Drawing.Point(126, 108);
            this.editPendingFrmVenta.Name = "editPendingFrmVenta";
            this.editPendingFrmVenta.Size = new System.Drawing.Size(124, 31);
            this.editPendingFrmVenta.TabIndex = 36;
            this.editPendingFrmVenta.Text = "$0.00";
            this.editPendingFrmVenta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.editPendingFrmVenta.Visible = false;
            // 
            // textChangeFrmVenta
            // 
            this.textChangeFrmVenta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textChangeFrmVenta.BackColor = System.Drawing.Color.LightGreen;
            this.textChangeFrmVenta.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textChangeFrmVenta.Location = new System.Drawing.Point(11, 151);
            this.textChangeFrmVenta.Name = "textChangeFrmVenta";
            this.textChangeFrmVenta.Size = new System.Drawing.Size(0, 31);
            this.textChangeFrmVenta.TabIndex = 35;
            this.textChangeFrmVenta.Text = "Cambio:";
            this.textChangeFrmVenta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.textChangeFrmVenta.Visible = false;
            // 
            // textPendingFrmVenta
            // 
            this.textPendingFrmVenta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textPendingFrmVenta.BackColor = System.Drawing.Color.LightGreen;
            this.textPendingFrmVenta.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textPendingFrmVenta.Location = new System.Drawing.Point(11, 108);
            this.textPendingFrmVenta.Name = "textPendingFrmVenta";
            this.textPendingFrmVenta.Size = new System.Drawing.Size(0, 31);
            this.textPendingFrmVenta.TabIndex = 34;
            this.textPendingFrmVenta.Text = "Pendiente:";
            this.textPendingFrmVenta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.textPendingFrmVenta.Visible = false;
            // 
            // textDiscountFrmVenta
            // 
            this.textDiscountFrmVenta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textDiscountFrmVenta.BackColor = System.Drawing.Color.Honeydew;
            this.textDiscountFrmVenta.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textDiscountFrmVenta.Location = new System.Drawing.Point(11, 53);
            this.textDiscountFrmVenta.Name = "textDiscountFrmVenta";
            this.textDiscountFrmVenta.Size = new System.Drawing.Size(235, 31);
            this.textDiscountFrmVenta.TabIndex = 31;
            this.textDiscountFrmVenta.Text = "0.00";
            this.textDiscountFrmVenta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textSubtotalFrmVenta
            // 
            this.textSubtotalFrmVenta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textSubtotalFrmVenta.BackColor = System.Drawing.Color.Honeydew;
            this.textSubtotalFrmVenta.Font = new System.Drawing.Font("Roboto Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textSubtotalFrmVenta.Location = new System.Drawing.Point(11, 9);
            this.textSubtotalFrmVenta.Name = "textSubtotalFrmVenta";
            this.textSubtotalFrmVenta.Size = new System.Drawing.Size(239, 33);
            this.textSubtotalFrmVenta.TabIndex = 30;
            this.textSubtotalFrmVenta.Text = "0.00";
            this.textSubtotalFrmVenta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textInfoTotal
            // 
            this.textInfoTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textInfoTotal.BackColor = System.Drawing.Color.Honeydew;
            this.textInfoTotal.Font = new System.Drawing.Font("Roboto Black", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInfoTotal.Location = new System.Drawing.Point(12, 106);
            this.textInfoTotal.Name = "textInfoTotal";
            this.textInfoTotal.Size = new System.Drawing.Size(256, 38);
            this.textInfoTotal.TabIndex = 80;
            this.textInfoTotal.Text = "Total";
            this.textInfoTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textTotalFrmVenta
            // 
            this.textTotalFrmVenta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textTotalFrmVenta.BackColor = System.Drawing.Color.Honeydew;
            this.textTotalFrmVenta.Font = new System.Drawing.Font("Roboto Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTotalFrmVenta.Location = new System.Drawing.Point(19, 154);
            this.textTotalFrmVenta.Name = "textTotalFrmVenta";
            this.textTotalFrmVenta.Size = new System.Drawing.Size(249, 67);
            this.textTotalFrmVenta.TabIndex = 79;
            this.textTotalFrmVenta.Text = "0.00";
            this.textTotalFrmVenta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridMovements
            // 
            this.dataGridMovements.AllowUserToAddRows = false;
            this.dataGridMovements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridMovements.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridMovements.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridMovements.BackgroundColor = System.Drawing.Color.Azure;
            this.dataGridMovements.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridMovements.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridMovements.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridMovements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridMovements.ColumnHeadersVisible = false;
            this.dataGridMovements.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idMovimentDgv,
            this.Clave,
            this.Nombre,
            this.Cantidad,
            this.Unidad,
            this.piezasMovements,
            this.Precio,
            this.Subtotal,
            this.Descuento,
            this.TotalGeneral,
            this.Eliminar,
            this.itemIdDgvItems});
            this.dataGridMovements.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dataGridMovements.GridColor = System.Drawing.Color.SteelBlue;
            this.dataGridMovements.Location = new System.Drawing.Point(0, 32);
            this.dataGridMovements.Margin = new System.Windows.Forms.Padding(10);
            this.dataGridMovements.Name = "dataGridMovements";
            this.dataGridMovements.ReadOnly = true;
            this.dataGridMovements.RowHeadersVisible = false;
            this.dataGridMovements.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridMovements.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dataGridMovements.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.dataGridMovements.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridMovements.Size = new System.Drawing.Size(598, 140);
            this.dataGridMovements.TabIndex = 70;
            this.dataGridMovements.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridArticulos_CellClick);
            this.dataGridMovements.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridMovements_CellMouseClick);
            this.dataGridMovements.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridArticulos_Scroll);
            this.dataGridMovements.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridMovements_KeyUp);
            // 
            // idMovimentDgv
            // 
            this.idMovimentDgv.HeaderText = "Id";
            this.idMovimentDgv.Name = "idMovimentDgv";
            this.idMovimentDgv.ReadOnly = true;
            // 
            // Clave
            // 
            this.Clave.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Clave.FillWeight = 50F;
            this.Clave.HeaderText = "Clave";
            this.Clave.Name = "Clave";
            this.Clave.ReadOnly = true;
            // 
            // Nombre
            // 
            this.Nombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Nombre.FillWeight = 145F;
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            // 
            // Cantidad
            // 
            this.Cantidad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Cantidad.FillWeight = 40F;
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.ReadOnly = true;
            // 
            // Unidad
            // 
            this.Unidad.HeaderText = "Unidad";
            this.Unidad.Name = "Unidad";
            this.Unidad.ReadOnly = true;
            // 
            // piezasMovements
            // 
            this.piezasMovements.HeaderText = "Piezas Pollo";
            this.piezasMovements.Name = "piezasMovements";
            this.piezasMovements.ReadOnly = true;
            // 
            // Precio
            // 
            this.Precio.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Precio.FillWeight = 70F;
            this.Precio.HeaderText = "Precio";
            this.Precio.Name = "Precio";
            this.Precio.ReadOnly = true;
            // 
            // Subtotal
            // 
            this.Subtotal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Subtotal.FillWeight = 50F;
            this.Subtotal.HeaderText = "Subtotal";
            this.Subtotal.Name = "Subtotal";
            this.Subtotal.ReadOnly = true;
            // 
            // Descuento
            // 
            this.Descuento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Descuento.DefaultCellStyle = dataGridViewCellStyle5;
            this.Descuento.FillWeight = 83.56756F;
            this.Descuento.HeaderText = "Descuento";
            this.Descuento.Name = "Descuento";
            this.Descuento.ReadOnly = true;
            // 
            // TotalGeneral
            // 
            this.TotalGeneral.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TotalGeneral.FillWeight = 83.56756F;
            this.TotalGeneral.HeaderText = "Total";
            this.TotalGeneral.Name = "TotalGeneral";
            this.TotalGeneral.ReadOnly = true;
            // 
            // Eliminar
            // 
            this.Eliminar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.LightCoral;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            this.Eliminar.DefaultCellStyle = dataGridViewCellStyle6;
            this.Eliminar.FillWeight = 70F;
            this.Eliminar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Eliminar.HeaderText = "Eliminar";
            this.Eliminar.Name = "Eliminar";
            this.Eliminar.ReadOnly = true;
            this.Eliminar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Eliminar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // itemIdDgvItems
            // 
            this.itemIdDgvItems.HeaderText = "ItemId";
            this.itemIdDgvItems.Name = "itemIdDgvItems";
            this.itemIdDgvItems.ReadOnly = true;
            // 
            // textTotalMovements
            // 
            this.textTotalMovements.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textTotalMovements.Font = new System.Drawing.Font("Roboto Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTotalMovements.Location = new System.Drawing.Point(18, 0);
            this.textTotalMovements.Name = "textTotalMovements";
            this.textTotalMovements.Size = new System.Drawing.Size(564, 22);
            this.textTotalMovements.TabIndex = 72;
            this.textTotalMovements.Text = "Total";
            this.textTotalMovements.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelGridMovementsVenta
            // 
            this.panelGridMovementsVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelGridMovementsVenta.Controls.Add(this.textTotalMovements);
            this.panelGridMovementsVenta.Controls.Add(this.imgSinDatosFrmVenta);
            this.panelGridMovementsVenta.Controls.Add(this.dataGridMovements);
            this.panelGridMovementsVenta.Location = new System.Drawing.Point(0, 344);
            this.panelGridMovementsVenta.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.panelGridMovementsVenta.Name = "panelGridMovementsVenta";
            this.panelGridMovementsVenta.Size = new System.Drawing.Size(602, 172);
            this.panelGridMovementsVenta.TabIndex = 71;
            // 
            // imgSinDatosFrmVenta
            // 
            this.imgSinDatosFrmVenta.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.imgSinDatosFrmVenta.BackColor = System.Drawing.Color.White;
            this.imgSinDatosFrmVenta.Image = global::SyncTPV.Properties.Resources.sindatos;
            this.imgSinDatosFrmVenta.Location = new System.Drawing.Point(167, 57);
            this.imgSinDatosFrmVenta.Name = "imgSinDatosFrmVenta";
            this.imgSinDatosFrmVenta.Size = new System.Drawing.Size(251, 108);
            this.imgSinDatosFrmVenta.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgSinDatosFrmVenta.TabIndex = 71;
            this.imgSinDatosFrmVenta.TabStop = false;
            // 
            // panelBotonCobrar
            // 
            this.panelBotonCobrar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBotonCobrar.Controls.Add(this.textVersion);
            this.panelBotonCobrar.Controls.Add(this.btnCobrarFrmVenta);
            this.panelBotonCobrar.Location = new System.Drawing.Point(0, 524);
            this.panelBotonCobrar.Name = "panelBotonCobrar";
            this.panelBotonCobrar.Size = new System.Drawing.Size(908, 64);
            this.panelBotonCobrar.TabIndex = 80;
            // 
            // textVersion
            // 
            this.textVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textVersion.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textVersion.Location = new System.Drawing.Point(724, 33);
            this.textVersion.Name = "textVersion";
            this.textVersion.Size = new System.Drawing.Size(175, 23);
            this.textVersion.TabIndex = 74;
            this.textVersion.Text = "Version";
            this.textVersion.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // btnCobrarFrmVenta
            // 
            this.btnCobrarFrmVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnCobrarFrmVenta.AutoSize = true;
            this.btnCobrarFrmVenta.BackColor = System.Drawing.Color.Transparent;
            this.btnCobrarFrmVenta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCobrarFrmVenta.FlatAppearance.BorderColor = System.Drawing.Color.MediumSeaGreen;
            this.btnCobrarFrmVenta.FlatAppearance.BorderSize = 3;
            this.btnCobrarFrmVenta.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGreen;
            this.btnCobrarFrmVenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCobrarFrmVenta.Font = new System.Drawing.Font("Roboto Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCobrarFrmVenta.Image = ((System.Drawing.Image)(resources.GetObject("btnCobrarFrmVenta.Image")));
            this.btnCobrarFrmVenta.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCobrarFrmVenta.Location = new System.Drawing.Point(335, 8);
            this.btnCobrarFrmVenta.Margin = new System.Windows.Forms.Padding(3, 20, 3, 3);
            this.btnCobrarFrmVenta.Name = "btnCobrarFrmVenta";
            this.btnCobrarFrmVenta.Size = new System.Drawing.Size(312, 48);
            this.btnCobrarFrmVenta.TabIndex = 73;
            this.btnCobrarFrmVenta.Text = "Cobrar   (F10)";
            this.btnCobrarFrmVenta.UseVisualStyleBackColor = false;
            this.btnCobrarFrmVenta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnCobrarFrmVenta_KeyUp);
            // 
            // FormVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(911, 586);
            this.Controls.Add(this.imgCliente);
            this.Controls.Add(this.panelBotonCobrar);
            this.Controls.Add(this.panelTotales);
            this.Controls.Add(this.panelGridMovementsVenta);
            this.Controls.Add(this.panelSuperior);
            this.Controls.Add(this.panelToolbar);
            this.Controls.Add(this.btnBuscarClientes);
            this.Controls.Add(this.txtClientes);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Name = "FormVenta";
            this.Text = "Venta";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmVentaNew_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmVentaNew_FormClosed);
            this.Load += new System.EventHandler(this.FrmVentaNew_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmVentaNew_KeyUp);
            this.panelToolbar.ResumeLayout(false);
            this.panelClienteToolbar.ResumeLayout(false);
            this.panelCliente.ResumeLayout(false);
            this.panelCliente.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCliente)).EndInit();
            this.panelSuperior.ResumeLayout(false);
            this.panelImgItem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgItem)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panelObservationMovement.ResumeLayout(false);
            this.panelObservationMovement.PerformLayout();
            this.panelFieldsVenta.ResumeLayout(false);
            this.panel32.ResumeLayout(false);
            this.panel32.PerformLayout();
            this.panelFieldsCodItemsVenta.ResumeLayout(false);
            this.panelFieldsCodItemsVenta.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfoDescuento)).EndInit();
            this.panel24.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfoSeleccionarPrecio)).EndInit();
            this.panelTotales.ResumeLayout(false);
            this.panelSubtotales.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMovements)).EndInit();
            this.panelGridMovementsVenta.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatosFrmVenta)).EndInit();
            this.panelBotonCobrar.ResumeLayout(false);
            this.panelBotonCobrar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Button btnBuscarClientes;
        private System.Windows.Forms.TextBox txtClientes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnPausarDocumentoFrmVenta;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnRecuperar;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label textFolioFrmVenta;
        private System.Windows.Forms.Label textInfoDocumentTypeFrmVenta;
        private System.Windows.Forms.ComboBox comboBoxDocumentTypeFrmVenta;
        private System.Windows.Forms.Panel panelTotales;
        public System.Windows.Forms.Button btnPayWithCashFrmVenta;
        private System.Windows.Forms.Panel panelFieldsVenta;
        private System.Windows.Forms.Panel panel32;
        private System.Windows.Forms.Label textInfoCantidad;
        private System.Windows.Forms.Panel panelFieldsCodItemsVenta;
        private System.Windows.Forms.ComboBox comboBoxUnitMWITemVenta;
        private System.Windows.Forms.Button btnBuscarArticuloTeclado;
        private System.Windows.Forms.TextBox editNombreItemVenta;
        private System.Windows.Forms.ComboBox comboPreciosItemVenta;
        private System.Windows.Forms.Panel panel24;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBoxInfoSeleccionarPrecio;
        private System.Windows.Forms.Panel panelObservationMovement;
        private System.Windows.Forms.Label textObservationMovement;
        private System.Windows.Forms.TextBox editObservationMovement;
        private System.Windows.Forms.Panel panelClienteToolbar;
        private System.Windows.Forms.Panel panelCliente;
        private System.Windows.Forms.PictureBox imgCliente;
        private System.Windows.Forms.Button btnBuscarClientesNew;
        private System.Windows.Forms.TextBox editNombreCliente;
        private System.Windows.Forms.ComboBox comboCodigoItemVenta;
        private System.Windows.Forms.Label textUnidadDeMedida;
        private System.Windows.Forms.TextBox editUnidadNoConvertible;
        private System.Windows.Forms.Label textUnidadNoConvertible;
        public System.Windows.Forms.DataGridView dataGridMovements;
        private System.Windows.Forms.PictureBox imgSinDatosFrmVenta;
        private System.Windows.Forms.Label textTotalMovements;
        private System.Windows.Forms.Panel panelGridMovementsVenta;
        private System.Windows.Forms.Button btnClearOptions;
        private System.Windows.Forms.Button btnSurtirPedidos;
        private System.Windows.Forms.Button btnOpenScale;
        public System.Windows.Forms.TextBox editCapturedUnits;
        private System.Windows.Forms.Label textInfoTotal;
        private System.Windows.Forms.Label textTotalFrmVenta;
        private System.Windows.Forms.Label textDiscountRateInfoVenta;
        public System.Windows.Forms.TextBox editDiscountItemVenta;
        public System.Windows.Forms.TextBox editPriceItemVenta;
        private System.Windows.Forms.PictureBox pictureBoxInfoDescuento;
        private System.Windows.Forms.Panel panelSubtotales;
        private System.Windows.Forms.Label editChangeFrmVenta;
        private System.Windows.Forms.Label editPendingFrmVenta;
        private System.Windows.Forms.Label textChangeFrmVenta;
        private System.Windows.Forms.Label textPendingFrmVenta;
        private System.Windows.Forms.Label textDiscountFrmVenta;
        private System.Windows.Forms.Label textSubtotalFrmVenta;
        private System.Windows.Forms.Panel panelBotonCobrar;
        private RoundedButton btnAgregar;
        private RoundedButton btnCobrarFrmVenta;
        private System.Windows.Forms.Label textExistenciaReal;
        private System.Windows.Forms.DataGridViewTextBoxColumn idMovimentDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn Clave;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn piezasMovements;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subtotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalGeneral;
        private System.Windows.Forms.DataGridViewButtonColumn Eliminar;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemIdDgvItems;
        private System.Windows.Forms.Panel panelImgItem;
        private System.Windows.Forms.PictureBox imgItem;
        private System.Windows.Forms.ProgressBar progressBarLoadPrices;
        private System.Windows.Forms.Label textVersion;
    }
}