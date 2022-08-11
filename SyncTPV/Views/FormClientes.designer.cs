using System;
using System.Windows.Forms;

namespace SyncTPV
{
    partial class FormClientes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClientes));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.textBuscarCliente = new System.Windows.Forms.TextBox();
            this.panelEncabezadoCleintes = new System.Windows.Forms.Panel();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAddCustomer = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.panelBusquedaCliente = new System.Windows.Forms.Panel();
            this.progressBarGetCustomer = new System.Windows.Forms.ProgressBar();
            this.textVersionLAN = new System.Windows.Forms.Label();
            this.imgSearchCostomers = new System.Windows.Forms.PictureBox();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelPerfilCliente = new System.Windows.Forms.Panel();
            this.textZonaDetalleCliente = new System.Windows.Forms.Label();
            this.textDenomComercialDetalleCliente = new System.Windows.Forms.Label();
            this.editPendienteDetalleCliente = new System.Windows.Forms.TextBox();
            this.textInfoPendienteDetalleCliente = new System.Windows.Forms.Label();
            this.btnUploadImg = new System.Windows.Forms.Button();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtListaPrecio = new System.Windows.Forms.TextBox();
            this.txtLimCredito = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textNombreClienteDetallescliente = new System.Windows.Forms.Label();
            this.textClaveDetalleCliente = new System.Windows.Forms.Label();
            this.pctBoxCliente = new System.Windows.Forms.PictureBox();
            this.panelDataGridClientes = new System.Windows.Forms.Panel();
            this.progressBarLoadCustomers = new System.Windows.Forms.ProgressBar();
            this.textTotalCustomers = new System.Windows.Forms.Label();
            this.imgSinDatos = new System.Windows.Forms.PictureBox();
            this.dataGridClientes = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timerBusquedaClientes = new System.Windows.Forms.Timer(this.components);
            this.btnCobranzaDetalleCliente = new SyncTPV.RoundedButton();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rfc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.denominacion_comercial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usocfdi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.regimefiscal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEncabezadoCleintes.SuspendLayout();
            this.panelBusquedaCliente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSearchCostomers)).BeginInit();
            this.panelContent.SuspendLayout();
            this.panelPerfilCliente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctBoxCliente)).BeginInit();
            this.panelDataGridClientes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridClientes)).BeginInit();
            this.SuspendLayout();
            // 
            // textBuscarCliente
            // 
            this.textBuscarCliente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBuscarCliente.BackColor = System.Drawing.Color.White;
            this.textBuscarCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBuscarCliente.Location = new System.Drawing.Point(304, 24);
            this.textBuscarCliente.Name = "textBuscarCliente";
            this.textBuscarCliente.Size = new System.Drawing.Size(206, 26);
            this.textBuscarCliente.TabIndex = 7;
            this.textBuscarCliente.TextChanged += new System.EventHandler(this.txtBuscarCliente_TextChanged);
            this.textBuscarCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBuscarCliente_KeyPress);
            this.textBuscarCliente.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBuscarCliente_KeyUp);
            // 
            // panelEncabezadoCleintes
            // 
            this.panelEncabezadoCleintes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEncabezadoCleintes.BackColor = System.Drawing.Color.Coral;
            this.panelEncabezadoCleintes.Controls.Add(this.btnUpdate);
            this.panelEncabezadoCleintes.Controls.Add(this.btnDelete);
            this.panelEncabezadoCleintes.Controls.Add(this.btnAddCustomer);
            this.panelEncabezadoCleintes.Controls.Add(this.btnCerrar);
            this.panelEncabezadoCleintes.Location = new System.Drawing.Point(0, -1);
            this.panelEncabezadoCleintes.Name = "panelEncabezadoCleintes";
            this.panelEncabezadoCleintes.Size = new System.Drawing.Size(901, 72);
            this.panelEncabezadoCleintes.TabIndex = 11;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnUpdate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnUpdate.Location = new System.Drawing.Point(257, 5);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 64);
            this.btnUpdate.TabIndex = 14;
            this.btnUpdate.Text = "Actualizar";
            this.btnUpdate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Image = global::SyncTPV.Properties.Resources.add_customer_white;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDelete.Location = new System.Drawing.Point(176, 5);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 64);
            this.btnDelete.TabIndex = 13;
            this.btnDelete.Text = "Borrar";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAddCustomer
            // 
            this.btnAddCustomer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddCustomer.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnAddCustomer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnAddCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddCustomer.ForeColor = System.Drawing.Color.White;
            this.btnAddCustomer.Image = global::SyncTPV.Properties.Resources.add_customer_white;
            this.btnAddCustomer.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAddCustomer.Location = new System.Drawing.Point(95, 5);
            this.btnAddCustomer.Name = "btnAddCustomer";
            this.btnAddCustomer.Size = new System.Drawing.Size(75, 64);
            this.btnAddCustomer.TabIndex = 12;
            this.btnAddCustomer.Text = "Agregar";
            this.btnAddCustomer.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAddCustomer.UseVisualStyleBackColor = true;
            this.btnAddCustomer.Click += new System.EventHandler(this.btnAddCustomer_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnCerrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(20)))), ((int)(((byte)(224)))));
            this.btnCerrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Image = global::SyncTPV.Properties.Resources.close;
            this.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCerrar.Location = new System.Drawing.Point(3, 5);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(86, 64);
            this.btnCerrar.TabIndex = 11;
            this.btnCerrar.Text = "Cerrar (Esc)";
            this.btnCerrar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.PictureCerrar_Click);
            this.btnCerrar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnCerrar_KeyUp);
            // 
            // panelBusquedaCliente
            // 
            this.panelBusquedaCliente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBusquedaCliente.Controls.Add(this.progressBarGetCustomer);
            this.panelBusquedaCliente.Controls.Add(this.textVersionLAN);
            this.panelBusquedaCliente.Controls.Add(this.imgSearchCostomers);
            this.panelBusquedaCliente.Controls.Add(this.textBuscarCliente);
            this.panelBusquedaCliente.Location = new System.Drawing.Point(0, 77);
            this.panelBusquedaCliente.Name = "panelBusquedaCliente";
            this.panelBusquedaCliente.Size = new System.Drawing.Size(901, 67);
            this.panelBusquedaCliente.TabIndex = 14;
            // 
            // progressBarGetCustomer
            // 
            this.progressBarGetCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarGetCustomer.Location = new System.Drawing.Point(798, 3);
            this.progressBarGetCustomer.Name = "progressBarGetCustomer";
            this.progressBarGetCustomer.Size = new System.Drawing.Size(100, 15);
            this.progressBarGetCustomer.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBarGetCustomer.TabIndex = 58;
            this.progressBarGetCustomer.Visible = false;
            // 
            // textVersionLAN
            // 
            this.textVersionLAN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textVersionLAN.AutoSize = true;
            this.textVersionLAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textVersionLAN.Location = new System.Drawing.Point(3, 54);
            this.textVersionLAN.Name = "textVersionLAN";
            this.textVersionLAN.Size = new System.Drawing.Size(0, 13);
            this.textVersionLAN.TabIndex = 75;
            this.textVersionLAN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imgSearchCostomers
            // 
            this.imgSearchCostomers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imgSearchCostomers.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.imgSearchCostomers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgSearchCostomers.Image = ((System.Drawing.Image)(resources.GetObject("imgSearchCostomers.Image")));
            this.imgSearchCostomers.Location = new System.Drawing.Point(516, 24);
            this.imgSearchCostomers.Name = "imgSearchCostomers";
            this.imgSearchCostomers.Size = new System.Drawing.Size(29, 27);
            this.imgSearchCostomers.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgSearchCostomers.TabIndex = 74;
            this.imgSearchCostomers.TabStop = false;
            this.imgSearchCostomers.Click += new System.EventHandler(this.imgSearchCostomers_Click);
            // 
            // panelContent
            // 
            this.panelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContent.Controls.Add(this.panelPerfilCliente);
            this.panelContent.Controls.Add(this.panelDataGridClientes);
            this.panelContent.Location = new System.Drawing.Point(0, 150);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(901, 422);
            this.panelContent.TabIndex = 15;
            // 
            // panelPerfilCliente
            // 
            this.panelPerfilCliente.AutoScroll = true;
            this.panelPerfilCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelPerfilCliente.Controls.Add(this.textZonaDetalleCliente);
            this.panelPerfilCliente.Controls.Add(this.textDenomComercialDetalleCliente);
            this.panelPerfilCliente.Controls.Add(this.editPendienteDetalleCliente);
            this.panelPerfilCliente.Controls.Add(this.textInfoPendienteDetalleCliente);
            this.panelPerfilCliente.Controls.Add(this.btnCobranzaDetalleCliente);
            this.panelPerfilCliente.Controls.Add(this.btnUploadImg);
            this.panelPerfilCliente.Controls.Add(this.txtTelefono);
            this.panelPerfilCliente.Controls.Add(this.label14);
            this.panelPerfilCliente.Controls.Add(this.txtListaPrecio);
            this.panelPerfilCliente.Controls.Add(this.txtLimCredito);
            this.panelPerfilCliente.Controls.Add(this.label13);
            this.panelPerfilCliente.Controls.Add(this.label12);
            this.panelPerfilCliente.Controls.Add(this.txtDireccion);
            this.panelPerfilCliente.Controls.Add(this.label11);
            this.panelPerfilCliente.Controls.Add(this.textNombreClienteDetallescliente);
            this.panelPerfilCliente.Controls.Add(this.textClaveDetalleCliente);
            this.panelPerfilCliente.Controls.Add(this.pctBoxCliente);
            this.panelPerfilCliente.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelPerfilCliente.Location = new System.Drawing.Point(469, 0);
            this.panelPerfilCliente.Name = "panelPerfilCliente";
            this.panelPerfilCliente.Size = new System.Drawing.Size(432, 422);
            this.panelPerfilCliente.TabIndex = 19;
            this.panelPerfilCliente.Paint += new System.Windows.Forms.PaintEventHandler(this.panelPerfilCliente_Paint);
            // 
            // textZonaDetalleCliente
            // 
            this.textZonaDetalleCliente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textZonaDetalleCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textZonaDetalleCliente.Location = new System.Drawing.Point(204, 213);
            this.textZonaDetalleCliente.Name = "textZonaDetalleCliente";
            this.textZonaDetalleCliente.Size = new System.Drawing.Size(202, 33);
            this.textZonaDetalleCliente.TabIndex = 57;
            this.textZonaDetalleCliente.Text = "Zona del Cliente";
            this.textZonaDetalleCliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textDenomComercialDetalleCliente
            // 
            this.textDenomComercialDetalleCliente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textDenomComercialDetalleCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textDenomComercialDetalleCliente.Location = new System.Drawing.Point(155, 121);
            this.textDenomComercialDetalleCliente.Name = "textDenomComercialDetalleCliente";
            this.textDenomComercialDetalleCliente.Size = new System.Drawing.Size(257, 34);
            this.textDenomComercialDetalleCliente.TabIndex = 56;
            this.textDenomComercialDetalleCliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // editPendienteDetalleCliente
            // 
            this.editPendienteDetalleCliente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editPendienteDetalleCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editPendienteDetalleCliente.Location = new System.Drawing.Point(204, 283);
            this.editPendienteDetalleCliente.Multiline = true;
            this.editPendienteDetalleCliente.Name = "editPendienteDetalleCliente";
            this.editPendienteDetalleCliente.ReadOnly = true;
            this.editPendienteDetalleCliente.Size = new System.Drawing.Size(196, 24);
            this.editPendienteDetalleCliente.TabIndex = 55;
            // 
            // textInfoPendienteDetalleCliente
            // 
            this.textInfoPendienteDetalleCliente.AutoSize = true;
            this.textInfoPendienteDetalleCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInfoPendienteDetalleCliente.Location = new System.Drawing.Point(201, 263);
            this.textInfoPendienteDetalleCliente.Name = "textInfoPendienteDetalleCliente";
            this.textInfoPendienteDetalleCliente.Size = new System.Drawing.Size(104, 13);
            this.textInfoPendienteDetalleCliente.TabIndex = 54;
            this.textInfoPendienteDetalleCliente.Text = "Pendiente por Pagar";
            // 
            // btnUploadImg
            // 
            this.btnUploadImg.BackColor = System.Drawing.Color.White;
            this.btnUploadImg.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnUploadImg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUploadImg.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUploadImg.Image = ((System.Drawing.Image)(resources.GetObject("btnUploadImg.Image")));
            this.btnUploadImg.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUploadImg.Location = new System.Drawing.Point(6, 151);
            this.btnUploadImg.Name = "btnUploadImg";
            this.btnUploadImg.Size = new System.Drawing.Size(124, 26);
            this.btnUploadImg.TabIndex = 52;
            this.btnUploadImg.Text = "Subir Imagen";
            this.btnUploadImg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUploadImg.UseVisualStyleBackColor = false;
            this.btnUploadImg.Click += new System.EventHandler(this.btnUploadImg_Click);
            // 
            // txtTelefono
            // 
            this.txtTelefono.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTelefono.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelefono.Location = new System.Drawing.Point(10, 332);
            this.txtTelefono.Multiline = true;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(223, 24);
            this.txtTelefono.TabIndex = 51;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(13, 316);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(49, 13);
            this.label14.TabIndex = 50;
            this.label14.Text = "Teléfono";
            // 
            // txtListaPrecio
            // 
            this.txtListaPrecio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtListaPrecio.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtListaPrecio.Location = new System.Drawing.Point(10, 220);
            this.txtListaPrecio.Multiline = true;
            this.txtListaPrecio.Name = "txtListaPrecio";
            this.txtListaPrecio.Size = new System.Drawing.Size(188, 24);
            this.txtListaPrecio.TabIndex = 49;
            // 
            // txtLimCredito
            // 
            this.txtLimCredito.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLimCredito.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLimCredito.Location = new System.Drawing.Point(10, 283);
            this.txtLimCredito.Multiline = true;
            this.txtLimCredito.Name = "txtLimCredito";
            this.txtLimCredito.Size = new System.Drawing.Size(188, 24);
            this.txtLimCredito.TabIndex = 48;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(7, 196);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(76, 13);
            this.label13.TabIndex = 47;
            this.label13.Text = "Lista de precio";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(13, 263);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 13);
            this.label12.TabIndex = 46;
            this.label12.Text = "Limite Credito";
            // 
            // txtDireccion
            // 
            this.txtDireccion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDireccion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDireccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDireccion.Location = new System.Drawing.Point(8, 374);
            this.txtDireccion.Multiline = true;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.ReadOnly = true;
            this.txtDireccion.Size = new System.Drawing.Size(404, 34);
            this.txtDireccion.TabIndex = 45;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(155, 359);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(52, 13);
            this.label11.TabIndex = 44;
            this.label11.Text = "Dirección";
            // 
            // textNombreClienteDetallescliente
            // 
            this.textNombreClienteDetallescliente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textNombreClienteDetallescliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.textNombreClienteDetallescliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textNombreClienteDetallescliente.Location = new System.Drawing.Point(155, 29);
            this.textNombreClienteDetallescliente.Name = "textNombreClienteDetallescliente";
            this.textNombreClienteDetallescliente.Size = new System.Drawing.Size(257, 78);
            this.textNombreClienteDetallescliente.TabIndex = 43;
            this.textNombreClienteDetallescliente.Text = "Seleccionar Cliente";
            this.textNombreClienteDetallescliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textClaveDetalleCliente
            // 
            this.textClaveDetalleCliente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textClaveDetalleCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textClaveDetalleCliente.Location = new System.Drawing.Point(155, 3);
            this.textClaveDetalleCliente.Name = "textClaveDetalleCliente";
            this.textClaveDetalleCliente.Size = new System.Drawing.Size(225, 22);
            this.textClaveDetalleCliente.TabIndex = 42;
            this.textClaveDetalleCliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pctBoxCliente
            // 
            this.pctBoxCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pctBoxCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctBoxCliente.Image = ((System.Drawing.Image)(resources.GetObject("pctBoxCliente.Image")));
            this.pctBoxCliente.Location = new System.Drawing.Point(3, 3);
            this.pctBoxCliente.Name = "pctBoxCliente";
            this.pctBoxCliente.Size = new System.Drawing.Size(146, 142);
            this.pctBoxCliente.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pctBoxCliente.TabIndex = 41;
            this.pctBoxCliente.TabStop = false;
            this.pctBoxCliente.Click += new System.EventHandler(this.pctBoxCliente_Click);
            // 
            // panelDataGridClientes
            // 
            this.panelDataGridClientes.AutoScroll = true;
            this.panelDataGridClientes.Controls.Add(this.progressBarLoadCustomers);
            this.panelDataGridClientes.Controls.Add(this.textTotalCustomers);
            this.panelDataGridClientes.Controls.Add(this.imgSinDatos);
            this.panelDataGridClientes.Controls.Add(this.dataGridClientes);
            this.panelDataGridClientes.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelDataGridClientes.Location = new System.Drawing.Point(0, 0);
            this.panelDataGridClientes.Name = "panelDataGridClientes";
            this.panelDataGridClientes.Padding = new System.Windows.Forms.Padding(5);
            this.panelDataGridClientes.Size = new System.Drawing.Size(471, 422);
            this.panelDataGridClientes.TabIndex = 18;
            // 
            // progressBarLoadCustomers
            // 
            this.progressBarLoadCustomers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarLoadCustomers.ForeColor = System.Drawing.Color.FloralWhite;
            this.progressBarLoadCustomers.Location = new System.Drawing.Point(363, 400);
            this.progressBarLoadCustomers.Name = "progressBarLoadCustomers";
            this.progressBarLoadCustomers.Size = new System.Drawing.Size(100, 14);
            this.progressBarLoadCustomers.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBarLoadCustomers.TabIndex = 10;
            this.progressBarLoadCustomers.Visible = false;
            // 
            // textTotalCustomers
            // 
            this.textTotalCustomers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textTotalCustomers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTotalCustomers.Location = new System.Drawing.Point(11, 24);
            this.textTotalCustomers.Name = "textTotalCustomers";
            this.textTotalCustomers.Size = new System.Drawing.Size(452, 20);
            this.textTotalCustomers.TabIndex = 9;
            this.textTotalCustomers.Text = "Total";
            this.textTotalCustomers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imgSinDatos
            // 
            this.imgSinDatos.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.imgSinDatos.Image = ((System.Drawing.Image)(resources.GetObject("imgSinDatos.Image")));
            this.imgSinDatos.Location = new System.Drawing.Point(104, 135);
            this.imgSinDatos.Name = "imgSinDatos";
            this.imgSinDatos.Size = new System.Drawing.Size(242, 196);
            this.imgSinDatos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgSinDatos.TabIndex = 8;
            this.imgSinDatos.TabStop = false;
            // 
            // dataGridClientes
            // 
            this.dataGridClientes.AllowUserToAddRows = false;
            this.dataGridClientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridClientes.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridClientes.BackgroundColor = System.Drawing.Color.Azure;
            this.dataGridClientes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridClientes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Coral;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Menu;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridClientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridClientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.nombre,
            this.rfc,
            this.precio,
            this.denominacion_comercial,
            this.usocfdi,
            this.regimefiscal});
            this.dataGridClientes.EnableHeadersVisualStyles = false;
            this.dataGridClientes.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.dataGridClientes.Location = new System.Drawing.Point(11, 55);
            this.dataGridClientes.MultiSelect = false;
            this.dataGridClientes.Name = "dataGridClientes";
            this.dataGridClientes.ReadOnly = true;
            this.dataGridClientes.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(145)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridClientes.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridClientes.RowHeadersVisible = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(145)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridClientes.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridClientes.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridClientes.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.Azure;
            this.dataGridClientes.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridClientes.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridClientes.Size = new System.Drawing.Size(452, 339);
            this.dataGridClientes.TabIndex = 7;
            this.dataGridClientes.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridClientes_KeyUp);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // timerBusquedaClientes
            // 
            this.timerBusquedaClientes.Interval = 300;
            this.timerBusquedaClientes.Tick += new System.EventHandler(this.timerBusquedaClientes_Tick);
            // 
            // btnCobranzaDetalleCliente
            // 
            this.btnCobranzaDetalleCliente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCobranzaDetalleCliente.BackColor = System.Drawing.Color.Transparent;
            this.btnCobranzaDetalleCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCobranzaDetalleCliente.FlatAppearance.BorderColor = System.Drawing.Color.ForestGreen;
            this.btnCobranzaDetalleCliente.FlatAppearance.BorderSize = 2;
            this.btnCobranzaDetalleCliente.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Honeydew;
            this.btnCobranzaDetalleCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCobranzaDetalleCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCobranzaDetalleCliente.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCobranzaDetalleCliente.Location = new System.Drawing.Point(239, 316);
            this.btnCobranzaDetalleCliente.Name = "btnCobranzaDetalleCliente";
            this.btnCobranzaDetalleCliente.Size = new System.Drawing.Size(161, 40);
            this.btnCobranzaDetalleCliente.TabIndex = 53;
            this.btnCobranzaDetalleCliente.Text = "Cuentas por Cobrar";
            this.btnCobranzaDetalleCliente.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCobranzaDetalleCliente.UseVisualStyleBackColor = false;
            this.btnCobranzaDetalleCliente.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnCobranzaDetalleCliente_KeyUp);
            // 
            // id
            // 
            this.id.HeaderText = "Id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 90;
            // 
            // nombre
            // 
            this.nombre.HeaderText = "Nombre";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            this.nombre.Width = 90;
            // 
            // rfc
            // 
            this.rfc.HeaderText = "RFC";
            this.rfc.Name = "rfc";
            this.rfc.ReadOnly = true;
            this.rfc.Width = 89;
            // 
            // precio
            // 
            this.precio.HeaderText = "Precio Asignado";
            this.precio.Name = "precio";
            this.precio.ReadOnly = true;
            this.precio.Width = 90;
            // 
            // denominacion_comercial
            // 
            this.denominacion_comercial.HeaderText = "Denominación Comercial";
            this.denominacion_comercial.Name = "denominacion_comercial";
            this.denominacion_comercial.ReadOnly = true;
            this.denominacion_comercial.Width = 90;
            // 
            // usocfdi
            // 
            this.usocfdi.HeaderText = "UsoCFDI";
            this.usocfdi.Name = "usocfdi";
            this.usocfdi.ReadOnly = true;
            // 
            // regimefiscal
            // 
            this.regimefiscal.HeaderText = "RegimenFiscal";
            this.regimefiscal.Name = "regimefiscal";
            this.regimefiscal.ReadOnly = true;
            // 
            // FormClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(901, 572);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelBusquedaCliente);
            this.Controls.Add(this.panelEncabezadoCleintes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormClientes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clientes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmClientes_Load);
            this.SizeChanged += new System.EventHandler(this.frmClientes_SizeChanged);
            this.panelEncabezadoCleintes.ResumeLayout(false);
            this.panelBusquedaCliente.ResumeLayout(false);
            this.panelBusquedaCliente.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSearchCostomers)).EndInit();
            this.panelContent.ResumeLayout(false);
            this.panelPerfilCliente.ResumeLayout(false);
            this.panelPerfilCliente.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctBoxCliente)).EndInit();
            this.panelDataGridClientes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridClientes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox textBuscarCliente;
        private System.Windows.Forms.Panel panelEncabezadoCleintes;
        private System.Windows.Forms.Panel panelBusquedaCliente;
        private System.Windows.Forms.PictureBox imgSearchCostomers;
        private System.Windows.Forms.Button btnCerrar;
        private Panel panelContent;
        private Panel panelPerfilCliente;
        private Label textZonaDetalleCliente;
        private Label textDenomComercialDetalleCliente;
        private TextBox editPendienteDetalleCliente;
        private Label textInfoPendienteDetalleCliente;
        private Button btnUploadImg;
        private TextBox txtTelefono;
        private Label label14;
        private TextBox txtListaPrecio;
        private TextBox txtLimCredito;
        private Label label13;
        private Label label12;
        private TextBox txtDireccion;
        private Label label11;
        private Label textNombreClienteDetallescliente;
        private Label textClaveDetalleCliente;
        private PictureBox pctBoxCliente;
        private Panel panelDataGridClientes;
        private Label textTotalCustomers;
        private PictureBox imgSinDatos;
        private DataGridView dataGridClientes;
        private OpenFileDialog openFileDialog1;
        private RoundedButton btnCobranzaDetalleCliente;
        private Button btnAddCustomer;
        private Button btnDelete;
        private ProgressBar progressBarGetCustomer;
        private Button btnUpdate;
        private Label textVersionLAN;
        private Timer timerBusquedaClientes;
        private ProgressBar progressBarLoadCustomers;
        private DataGridViewTextBoxColumn id;
        private DataGridViewTextBoxColumn nombre;
        private DataGridViewTextBoxColumn rfc;
        private DataGridViewTextBoxColumn precio;
        private DataGridViewTextBoxColumn denominacion_comercial;
        private DataGridViewTextBoxColumn usocfdi;
        private DataGridViewTextBoxColumn regimefiscal;
    }
}