namespace SyncTPV.Views.Customers
{
    partial class FormAddCustomer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddCustomer));
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.btnDeleteCustomer = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelRegUso = new System.Windows.Forms.Panel();
            this.textUsoCfdi = new System.Windows.Forms.Label();
            this.textRegimenFiscal = new System.Windows.Forms.Label();
            this.textTipoContribuyente = new System.Windows.Forms.Label();
            this.comboBoxUsoCFDI = new System.Windows.Forms.ComboBox();
            this.comboBoxTipoContribuyente = new System.Windows.Forms.ComboBox();
            this.comboBoxRegimenFiscal = new System.Windows.Forms.ComboBox();
            this.textRFC = new System.Windows.Forms.Label();
            this.editRfc = new System.Windows.Forms.TextBox();
            this.comboBoxCiudades = new System.Windows.Forms.ComboBox();
            this.textCiudad = new System.Windows.Forms.Label();
            this.comboBoxEstados = new System.Windows.Forms.ComboBox();
            this.textEstado = new System.Windows.Forms.Label();
            this.textEmail = new System.Windows.Forms.Label();
            this.editEmail = new System.Windows.Forms.TextBox();
            this.textCodigoPostal = new System.Windows.Forms.Label();
            this.editCodigoPostal = new System.Windows.Forms.TextBox();
            this.textTelefono = new System.Windows.Forms.Label();
            this.editTelefono = new System.Windows.Forms.TextBox();
            this.textReferencia = new System.Windows.Forms.Label();
            this.editReferencia = new System.Windows.Forms.TextBox();
            this.textPoblacion = new System.Windows.Forms.Label();
            this.editPoblacion = new System.Windows.Forms.TextBox();
            this.textColonia = new System.Windows.Forms.Label();
            this.editColonia = new System.Windows.Forms.TextBox();
            this.textNumeroExt = new System.Windows.Forms.Label();
            this.editNumeroExterior = new System.Windows.Forms.TextBox();
            this.textNombreCalle = new System.Windows.Forms.Label();
            this.editNombreCalle = new System.Windows.Forms.TextBox();
            this.btnSaveCustomer = new System.Windows.Forms.Button();
            this.textNombre = new System.Windows.Forms.Label();
            this.editNombre = new System.Windows.Forms.TextBox();
            this.panelToolbar.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelRegUso.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelToolbar
            // 
            this.panelToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelToolbar.BackColor = System.Drawing.Color.Coral;
            this.panelToolbar.Controls.Add(this.btnDeleteCustomer);
            this.panelToolbar.Controls.Add(this.btnClose);
            this.panelToolbar.Location = new System.Drawing.Point(-1, -2);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(683, 75);
            this.panelToolbar.TabIndex = 0;
            // 
            // btnDeleteCustomer
            // 
            this.btnDeleteCustomer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteCustomer.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnDeleteCustomer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnDeleteCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteCustomer.ForeColor = System.Drawing.Color.White;
            this.btnDeleteCustomer.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDeleteCustomer.Location = new System.Drawing.Point(84, 3);
            this.btnDeleteCustomer.Name = "btnDeleteCustomer";
            this.btnDeleteCustomer.Size = new System.Drawing.Size(75, 69);
            this.btnDeleteCustomer.TabIndex = 16;
            this.btnDeleteCustomer.Text = "Borrar";
            this.btnDeleteCustomer.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDeleteCustomer.UseVisualStyleBackColor = true;
            this.btnDeleteCustomer.Click += new System.EventHandler(this.btnDeleteCustomer_Click);
            // 
            // btnClose
            // 
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
            this.btnClose.Size = new System.Drawing.Size(75, 69);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "Cerrar";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelContent
            // 
            this.panelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContent.Controls.Add(this.panelRegUso);
            this.panelContent.Controls.Add(this.textRFC);
            this.panelContent.Controls.Add(this.editRfc);
            this.panelContent.Controls.Add(this.comboBoxCiudades);
            this.panelContent.Controls.Add(this.textCiudad);
            this.panelContent.Controls.Add(this.comboBoxEstados);
            this.panelContent.Controls.Add(this.textEstado);
            this.panelContent.Controls.Add(this.textEmail);
            this.panelContent.Controls.Add(this.editEmail);
            this.panelContent.Controls.Add(this.textCodigoPostal);
            this.panelContent.Controls.Add(this.editCodigoPostal);
            this.panelContent.Controls.Add(this.textTelefono);
            this.panelContent.Controls.Add(this.editTelefono);
            this.panelContent.Controls.Add(this.textReferencia);
            this.panelContent.Controls.Add(this.editReferencia);
            this.panelContent.Controls.Add(this.textPoblacion);
            this.panelContent.Controls.Add(this.editPoblacion);
            this.panelContent.Controls.Add(this.textColonia);
            this.panelContent.Controls.Add(this.editColonia);
            this.panelContent.Controls.Add(this.textNumeroExt);
            this.panelContent.Controls.Add(this.editNumeroExterior);
            this.panelContent.Controls.Add(this.textNombreCalle);
            this.panelContent.Controls.Add(this.editNombreCalle);
            this.panelContent.Controls.Add(this.btnSaveCustomer);
            this.panelContent.Controls.Add(this.textNombre);
            this.panelContent.Controls.Add(this.editNombre);
            this.panelContent.Location = new System.Drawing.Point(-1, 76);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(683, 430);
            this.panelContent.TabIndex = 1;
            // 
            // panelRegUso
            // 
            this.panelRegUso.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRegUso.Controls.Add(this.textUsoCfdi);
            this.panelRegUso.Controls.Add(this.textRegimenFiscal);
            this.panelRegUso.Controls.Add(this.textTipoContribuyente);
            this.panelRegUso.Controls.Add(this.comboBoxUsoCFDI);
            this.panelRegUso.Controls.Add(this.comboBoxTipoContribuyente);
            this.panelRegUso.Controls.Add(this.comboBoxRegimenFiscal);
            this.panelRegUso.Location = new System.Drawing.Point(3, 335);
            this.panelRegUso.Name = "panelRegUso";
            this.panelRegUso.Size = new System.Drawing.Size(506, 92);
            this.panelRegUso.TabIndex = 27;
            // 
            // textUsoCfdi
            // 
            this.textUsoCfdi.AutoSize = true;
            this.textUsoCfdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textUsoCfdi.Location = new System.Drawing.Point(283, 44);
            this.textUsoCfdi.Name = "textUsoCfdi";
            this.textUsoCfdi.Size = new System.Drawing.Size(56, 15);
            this.textUsoCfdi.TabIndex = 19;
            this.textUsoCfdi.Text = "UsoCFDI";
            this.textUsoCfdi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.textUsoCfdi.Click += new System.EventHandler(this.label2_Click);
            // 
            // textRegimenFiscal
            // 
            this.textRegimenFiscal.AutoSize = true;
            this.textRegimenFiscal.Cursor = System.Windows.Forms.Cursors.Default;
            this.textRegimenFiscal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textRegimenFiscal.Location = new System.Drawing.Point(3, 44);
            this.textRegimenFiscal.Name = "textRegimenFiscal";
            this.textRegimenFiscal.Size = new System.Drawing.Size(93, 15);
            this.textRegimenFiscal.TabIndex = 18;
            this.textRegimenFiscal.Text = "Regimen Fiscal";
            this.textRegimenFiscal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textTipoContribuyente
            // 
            this.textTipoContribuyente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textTipoContribuyente.AutoSize = true;
            this.textTipoContribuyente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTipoContribuyente.Location = new System.Drawing.Point(66, 9);
            this.textTipoContribuyente.Name = "textTipoContribuyente";
            this.textTipoContribuyente.Size = new System.Drawing.Size(129, 15);
            this.textTipoContribuyente.TabIndex = 3;
            this.textTipoContribuyente.Text = "Tipo de Contribuyente:";
            this.textTipoContribuyente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxUsoCFDI
            // 
            this.comboBoxUsoCFDI.BackColor = System.Drawing.Color.LightYellow;
            this.comboBoxUsoCFDI.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxUsoCFDI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUsoCFDI.FormattingEnabled = true;
            this.comboBoxUsoCFDI.Location = new System.Drawing.Point(237, 61);
            this.comboBoxUsoCFDI.Name = "comboBoxUsoCFDI";
            this.comboBoxUsoCFDI.Size = new System.Drawing.Size(266, 21);
            this.comboBoxUsoCFDI.TabIndex = 19;
            this.comboBoxUsoCFDI.SelectedIndexChanged += new System.EventHandler(this.comboBoxUsoCFDI_SelectedIndexChanged);
            // 
            // comboBoxTipoContribuyente
            // 
            this.comboBoxTipoContribuyente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxTipoContribuyente.BackColor = System.Drawing.Color.LightBlue;
            this.comboBoxTipoContribuyente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxTipoContribuyente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTipoContribuyente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxTipoContribuyente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxTipoContribuyente.FormattingEnabled = true;
            this.comboBoxTipoContribuyente.Items.AddRange(new object[] {
            "Persona Física",
            "Persona Moral"});
            this.comboBoxTipoContribuyente.Location = new System.Drawing.Point(201, 6);
            this.comboBoxTipoContribuyente.MaxDropDownItems = 2;
            this.comboBoxTipoContribuyente.Name = "comboBoxTipoContribuyente";
            this.comboBoxTipoContribuyente.Size = new System.Drawing.Size(203, 23);
            this.comboBoxTipoContribuyente.TabIndex = 17;
            this.comboBoxTipoContribuyente.SelectedIndexChanged += new System.EventHandler(this.comboBoxTipoContribuyente_SelectedIndexChanged);
            // 
            // comboBoxRegimenFiscal
            // 
            this.comboBoxRegimenFiscal.BackColor = System.Drawing.Color.LightYellow;
            this.comboBoxRegimenFiscal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxRegimenFiscal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRegimenFiscal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxRegimenFiscal.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBoxRegimenFiscal.FormattingEnabled = true;
            this.comboBoxRegimenFiscal.Location = new System.Drawing.Point(3, 61);
            this.comboBoxRegimenFiscal.Name = "comboBoxRegimenFiscal";
            this.comboBoxRegimenFiscal.Size = new System.Drawing.Size(228, 23);
            this.comboBoxRegimenFiscal.TabIndex = 18;
            this.comboBoxRegimenFiscal.SelectedIndexChanged += new System.EventHandler(this.comboBoxRegimenFiscal_SelectedIndexChanged);
            // 
            // textRFC
            // 
            this.textRFC.AutoSize = true;
            this.textRFC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textRFC.Location = new System.Drawing.Point(344, 288);
            this.textRFC.Name = "textRFC";
            this.textRFC.Size = new System.Drawing.Size(36, 15);
            this.textRFC.TabIndex = 26;
            this.textRFC.Text = "*RFC";
            // 
            // editRfc
            // 
            this.editRfc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.editRfc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editRfc.Location = new System.Drawing.Point(347, 307);
            this.editRfc.MaxLength = 20;
            this.editRfc.Name = "editRfc";
            this.editRfc.Size = new System.Drawing.Size(323, 21);
            this.editRfc.TabIndex = 12;
            // 
            // comboBoxCiudades
            // 
            this.comboBoxCiudades.FormattingEnabled = true;
            this.comboBoxCiudades.Location = new System.Drawing.Point(347, 146);
            this.comboBoxCiudades.Name = "comboBoxCiudades";
            this.comboBoxCiudades.Size = new System.Drawing.Size(323, 21);
            this.comboBoxCiudades.TabIndex = 6;
            this.comboBoxCiudades.SelectedIndexChanged += new System.EventHandler(this.comboBoxCiudades_SelectedIndexChanged);
            // 
            // textCiudad
            // 
            this.textCiudad.AutoSize = true;
            this.textCiudad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textCiudad.Location = new System.Drawing.Point(344, 128);
            this.textCiudad.Name = "textCiudad";
            this.textCiudad.Size = new System.Drawing.Size(119, 15);
            this.textCiudad.TabIndex = 23;
            this.textCiudad.Text = "*Seleccionar Ciudad";
            // 
            // comboBoxEstados
            // 
            this.comboBoxEstados.FormattingEnabled = true;
            this.comboBoxEstados.Location = new System.Drawing.Point(13, 146);
            this.comboBoxEstados.Name = "comboBoxEstados";
            this.comboBoxEstados.Size = new System.Drawing.Size(284, 21);
            this.comboBoxEstados.TabIndex = 5;
            this.comboBoxEstados.SelectedIndexChanged += new System.EventHandler(this.comboBoxEstados_SelectedIndexChanged);
            // 
            // textEstado
            // 
            this.textEstado.AutoSize = true;
            this.textEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textEstado.Location = new System.Drawing.Point(14, 128);
            this.textEstado.Name = "textEstado";
            this.textEstado.Size = new System.Drawing.Size(118, 15);
            this.textEstado.TabIndex = 21;
            this.textEstado.Text = "*Seleccionar Estado";
            // 
            // textEmail
            // 
            this.textEmail.AutoSize = true;
            this.textEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textEmail.Location = new System.Drawing.Point(13, 288);
            this.textEmail.Name = "textEmail";
            this.textEmail.Size = new System.Drawing.Size(39, 15);
            this.textEmail.TabIndex = 20;
            this.textEmail.Text = "Email";
            // 
            // editEmail
            // 
            this.editEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editEmail.Location = new System.Drawing.Point(13, 307);
            this.editEmail.MaxLength = 60;
            this.editEmail.Name = "editEmail";
            this.editEmail.Size = new System.Drawing.Size(284, 21);
            this.editEmail.TabIndex = 11;
            // 
            // textCodigoPostal
            // 
            this.textCodigoPostal.AutoSize = true;
            this.textCodigoPostal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textCodigoPostal.Location = new System.Drawing.Point(339, 234);
            this.textCodigoPostal.Name = "textCodigoPostal";
            this.textCodigoPostal.Size = new System.Drawing.Size(88, 15);
            this.textCodigoPostal.TabIndex = 18;
            this.textCodigoPostal.Text = "*Código Postal";
            // 
            // editCodigoPostal
            // 
            this.editCodigoPostal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editCodigoPostal.Location = new System.Drawing.Point(347, 253);
            this.editCodigoPostal.MaxLength = 6;
            this.editCodigoPostal.Name = "editCodigoPostal";
            this.editCodigoPostal.Size = new System.Drawing.Size(323, 21);
            this.editCodigoPostal.TabIndex = 10;
            this.editCodigoPostal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editCodigoPostal_KeyPress);
            // 
            // textTelefono
            // 
            this.textTelefono.AutoSize = true;
            this.textTelefono.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTelefono.Location = new System.Drawing.Point(13, 234);
            this.textTelefono.Name = "textTelefono";
            this.textTelefono.Size = new System.Drawing.Size(60, 15);
            this.textTelefono.TabIndex = 16;
            this.textTelefono.Text = "*Telefono";
            // 
            // editTelefono
            // 
            this.editTelefono.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editTelefono.Location = new System.Drawing.Point(13, 253);
            this.editTelefono.MaxLength = 15;
            this.editTelefono.Name = "editTelefono";
            this.editTelefono.Size = new System.Drawing.Size(284, 21);
            this.editTelefono.TabIndex = 9;
            this.editTelefono.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editTelefono_KeyPress);
            // 
            // textReferencia
            // 
            this.textReferencia.AutoSize = true;
            this.textReferencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textReferencia.Location = new System.Drawing.Point(344, 180);
            this.textReferencia.Name = "textReferencia";
            this.textReferencia.Size = new System.Drawing.Size(67, 15);
            this.textReferencia.TabIndex = 14;
            this.textReferencia.Text = "Referencia";
            // 
            // editReferencia
            // 
            this.editReferencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editReferencia.Location = new System.Drawing.Point(347, 199);
            this.editReferencia.MaxLength = 60;
            this.editReferencia.Name = "editReferencia";
            this.editReferencia.Size = new System.Drawing.Size(323, 21);
            this.editReferencia.TabIndex = 8;
            // 
            // textPoblacion
            // 
            this.textPoblacion.AutoSize = true;
            this.textPoblacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textPoblacion.Location = new System.Drawing.Point(13, 180);
            this.textPoblacion.Name = "textPoblacion";
            this.textPoblacion.Size = new System.Drawing.Size(62, 15);
            this.textPoblacion.TabIndex = 12;
            this.textPoblacion.Text = "Población";
            // 
            // editPoblacion
            // 
            this.editPoblacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editPoblacion.Location = new System.Drawing.Point(13, 199);
            this.editPoblacion.MaxLength = 60;
            this.editPoblacion.Name = "editPoblacion";
            this.editPoblacion.Size = new System.Drawing.Size(284, 21);
            this.editPoblacion.TabIndex = 7;
            // 
            // textColonia
            // 
            this.textColonia.AutoSize = true;
            this.textColonia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textColonia.Location = new System.Drawing.Point(344, 74);
            this.textColonia.Name = "textColonia";
            this.textColonia.Size = new System.Drawing.Size(54, 15);
            this.textColonia.TabIndex = 10;
            this.textColonia.Text = "*Colonia";
            // 
            // editColonia
            // 
            this.editColonia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editColonia.Location = new System.Drawing.Point(347, 93);
            this.editColonia.MaxLength = 60;
            this.editColonia.Name = "editColonia";
            this.editColonia.Size = new System.Drawing.Size(323, 21);
            this.editColonia.TabIndex = 4;
            // 
            // textNumeroExt
            // 
            this.textNumeroExt.AutoSize = true;
            this.textNumeroExt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textNumeroExt.Location = new System.Drawing.Point(14, 74);
            this.textNumeroExt.Name = "textNumeroExt";
            this.textNumeroExt.Size = new System.Drawing.Size(102, 15);
            this.textNumeroExt.TabIndex = 8;
            this.textNumeroExt.Text = "*Numero Exterior";
            // 
            // editNumeroExterior
            // 
            this.editNumeroExterior.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editNumeroExterior.Location = new System.Drawing.Point(13, 93);
            this.editNumeroExterior.MaxLength = 30;
            this.editNumeroExterior.Name = "editNumeroExterior";
            this.editNumeroExterior.Size = new System.Drawing.Size(285, 21);
            this.editNumeroExterior.TabIndex = 3;
            // 
            // textNombreCalle
            // 
            this.textNombreCalle.AutoSize = true;
            this.textNombreCalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textNombreCalle.Location = new System.Drawing.Point(344, 20);
            this.textNombreCalle.Name = "textNombreCalle";
            this.textNombreCalle.Size = new System.Drawing.Size(118, 15);
            this.textNombreCalle.TabIndex = 6;
            this.textNombreCalle.Text = "*Nombre de la Calle";
            // 
            // editNombreCalle
            // 
            this.editNombreCalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editNombreCalle.Location = new System.Drawing.Point(347, 39);
            this.editNombreCalle.MaxLength = 60;
            this.editNombreCalle.Name = "editNombreCalle";
            this.editNombreCalle.Size = new System.Drawing.Size(323, 21);
            this.editNombreCalle.TabIndex = 2;
            // 
            // btnSaveCustomer
            // 
            this.btnSaveCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveCustomer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveCustomer.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnSaveCustomer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSaveCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveCustomer.Location = new System.Drawing.Point(515, 383);
            this.btnSaveCustomer.Name = "btnSaveCustomer";
            this.btnSaveCustomer.Size = new System.Drawing.Size(168, 47);
            this.btnSaveCustomer.TabIndex = 13;
            this.btnSaveCustomer.Text = "Guardar";
            this.btnSaveCustomer.UseVisualStyleBackColor = true;
            this.btnSaveCustomer.Click += new System.EventHandler(this.btnSaveCustomer_Click);
            // 
            // textNombre
            // 
            this.textNombre.AutoSize = true;
            this.textNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textNombre.Location = new System.Drawing.Point(14, 20);
            this.textNombre.Name = "textNombre";
            this.textNombre.Size = new System.Drawing.Size(174, 15);
            this.textNombre.TabIndex = 3;
            this.textNombre.Text = "*Nombre Completo del Cliente";
            // 
            // editNombre
            // 
            this.editNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editNombre.Location = new System.Drawing.Point(13, 39);
            this.editNombre.MaxLength = 60;
            this.editNombre.Name = "editNombre";
            this.editNombre.Size = new System.Drawing.Size(285, 21);
            this.editNombre.TabIndex = 1;
            // 
            // FormAddCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(681, 505);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelToolbar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(697, 512);
            this.Name = "FormAddCustomer";
            this.Text = "Agregar Cliente Nuevo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAddCustomer_FormClosing);
            this.Load += new System.EventHandler(this.FormAddCustomer_Load);
            this.panelToolbar.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            this.panelRegUso.ResumeLayout(false);
            this.panelRegUso.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.TextBox editNombre;
        private System.Windows.Forms.Label textNombre;
        private System.Windows.Forms.Button btnSaveCustomer;
        private System.Windows.Forms.Label textNombreCalle;
        private System.Windows.Forms.TextBox editNombreCalle;
        private System.Windows.Forms.Label textNumeroExt;
        private System.Windows.Forms.TextBox editNumeroExterior;
        private System.Windows.Forms.Label textRFC;
        private System.Windows.Forms.TextBox editRfc;
        private System.Windows.Forms.ComboBox comboBoxCiudades;
        private System.Windows.Forms.Label textCiudad;
        private System.Windows.Forms.ComboBox comboBoxEstados;
        private System.Windows.Forms.Label textEstado;
        private System.Windows.Forms.Label textEmail;
        private System.Windows.Forms.TextBox editEmail;
        private System.Windows.Forms.Label textCodigoPostal;
        private System.Windows.Forms.TextBox editCodigoPostal;
        private System.Windows.Forms.Label textTelefono;
        private System.Windows.Forms.TextBox editTelefono;
        private System.Windows.Forms.Label textReferencia;
        private System.Windows.Forms.TextBox editReferencia;
        private System.Windows.Forms.Label textPoblacion;
        private System.Windows.Forms.TextBox editPoblacion;
        private System.Windows.Forms.Label textColonia;
        private System.Windows.Forms.TextBox editColonia;
        private System.Windows.Forms.Button btnDeleteCustomer;
        private System.Windows.Forms.Panel panelRegUso;
        private System.Windows.Forms.ComboBox comboBoxUsoCFDI;
        private System.Windows.Forms.ComboBox comboBoxTipoContribuyente;
        private System.Windows.Forms.ComboBox comboBoxRegimenFiscal;
        private System.Windows.Forms.Label textTipoContribuyente;
        private System.Windows.Forms.Label textUsoCfdi;
        private System.Windows.Forms.Label textRegimenFiscal;
    }
}