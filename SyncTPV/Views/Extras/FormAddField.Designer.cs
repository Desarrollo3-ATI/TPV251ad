
namespace SyncTPV.Views.Extras
{
    partial class FormAddField
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddField));
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.checkBoxMismaInstancia = new System.Windows.Forms.CheckBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBoxPanelInstance = new System.Windows.Forms.GroupBox();
            this.btnSeePassPanel = new System.Windows.Forms.Button();
            this.editDbNamePanel = new System.Windows.Forms.TextBox();
            this.textInfoDbNamePanel = new System.Windows.Forms.Label();
            this.editPassInstanciaPanel = new System.Windows.Forms.TextBox();
            this.textInfoPassSql = new System.Windows.Forms.Label();
            this.editUserInstanciaPanel = new System.Windows.Forms.TextBox();
            this.textInfoUsuarioSql = new System.Windows.Forms.Label();
            this.editNombreInstanciaPanel = new System.Windows.Forms.TextBox();
            this.textInfoInstancia = new System.Windows.Forms.Label();
            this.textInfoIpServer = new System.Windows.Forms.Label();
            this.editIpServerPanel = new System.Windows.Forms.TextBox();
            this.groupBoxComInstance = new System.Windows.Forms.GroupBox();
            this.btnSeePassCom = new System.Windows.Forms.Button();
            this.editDbNameComercial = new System.Windows.Forms.TextBox();
            this.editPassInstanciaComercial = new System.Windows.Forms.TextBox();
            this.textInfoDbNameComercial = new System.Windows.Forms.Label();
            this.textInfoPassComercial = new System.Windows.Forms.Label();
            this.editUserInstanciaComercial = new System.Windows.Forms.TextBox();
            this.textInfoUserComercial = new System.Windows.Forms.Label();
            this.editNombreInstanciaComercial = new System.Windows.Forms.TextBox();
            this.textInfoInstanciaComercial = new System.Windows.Forms.Label();
            this.textInfoIpComercial = new System.Windows.Forms.Label();
            this.editIpServidorComercial = new System.Windows.Forms.TextBox();
            this.panelToolbar.SuspendLayout();
            this.groupBoxPanelInstance.SuspendLayout();
            this.groupBoxComInstance.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelToolbar
            // 
            this.panelToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelToolbar.BackColor = System.Drawing.Color.Coral;
            this.panelToolbar.Controls.Add(this.checkBoxMismaInstancia);
            this.panelToolbar.Controls.Add(this.btnCerrar);
            this.panelToolbar.Location = new System.Drawing.Point(-2, -3);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(591, 68);
            this.panelToolbar.TabIndex = 0;
            // 
            // checkBoxMismaInstancia
            // 
            this.checkBoxMismaInstancia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxMismaInstancia.AutoSize = true;
            this.checkBoxMismaInstancia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxMismaInstancia.ForeColor = System.Drawing.Color.White;
            this.checkBoxMismaInstancia.Location = new System.Drawing.Point(440, 15);
            this.checkBoxMismaInstancia.Name = "checkBoxMismaInstancia";
            this.checkBoxMismaInstancia.Size = new System.Drawing.Size(137, 20);
            this.checkBoxMismaInstancia.TabIndex = 301;
            this.checkBoxMismaInstancia.Text = "Misma Instancia";
            this.checkBoxMismaInstancia.UseVisualStyleBackColor = true;
            this.checkBoxMismaInstancia.CheckedChanged += new System.EventHandler(this.checkBoxMismaInstancia_CheckedChanged);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnCerrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCerrar.Location = new System.Drawing.Point(3, 3);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(75, 61);
            this.btnCerrar.TabIndex = 0;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(443, 357);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(146, 54);
            this.btnSave.TabIndex = 300;
            this.btnSave.Text = "Guardar";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBoxPanelInstance
            // 
            this.groupBoxPanelInstance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxPanelInstance.Controls.Add(this.btnSeePassPanel);
            this.groupBoxPanelInstance.Controls.Add(this.editDbNamePanel);
            this.groupBoxPanelInstance.Controls.Add(this.textInfoDbNamePanel);
            this.groupBoxPanelInstance.Controls.Add(this.editPassInstanciaPanel);
            this.groupBoxPanelInstance.Controls.Add(this.textInfoPassSql);
            this.groupBoxPanelInstance.Controls.Add(this.editUserInstanciaPanel);
            this.groupBoxPanelInstance.Controls.Add(this.textInfoUsuarioSql);
            this.groupBoxPanelInstance.Controls.Add(this.editNombreInstanciaPanel);
            this.groupBoxPanelInstance.Controls.Add(this.textInfoInstancia);
            this.groupBoxPanelInstance.Controls.Add(this.textInfoIpServer);
            this.groupBoxPanelInstance.Controls.Add(this.editIpServerPanel);
            this.groupBoxPanelInstance.Location = new System.Drawing.Point(12, 71);
            this.groupBoxPanelInstance.MinimumSize = new System.Drawing.Size(278, 239);
            this.groupBoxPanelInstance.Name = "groupBoxPanelInstance";
            this.groupBoxPanelInstance.Size = new System.Drawing.Size(278, 271);
            this.groupBoxPanelInstance.TabIndex = 100;
            this.groupBoxPanelInstance.TabStop = false;
            this.groupBoxPanelInstance.Text = "Instancia SQLServer de PanelROM";
            // 
            // btnSeePassPanel
            // 
            this.btnSeePassPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSeePassPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSeePassPanel.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnSeePassPanel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSeePassPanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeePassPanel.Location = new System.Drawing.Point(233, 227);
            this.btnSeePassPanel.Name = "btnSeePassPanel";
            this.btnSeePassPanel.Size = new System.Drawing.Size(24, 24);
            this.btnSeePassPanel.TabIndex = 106;
            this.btnSeePassPanel.UseVisualStyleBackColor = true;
            this.btnSeePassPanel.Click += new System.EventHandler(this.btnSeePassPanel_Click);
            // 
            // editDbNamePanel
            // 
            this.editDbNamePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editDbNamePanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editDbNamePanel.Location = new System.Drawing.Point(24, 138);
            this.editDbNamePanel.Name = "editDbNamePanel";
            this.editDbNamePanel.Size = new System.Drawing.Size(234, 22);
            this.editDbNamePanel.TabIndex = 103;
            this.editDbNamePanel.Text = "adPanelROM";
            // 
            // textInfoDbNamePanel
            // 
            this.textInfoDbNamePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textInfoDbNamePanel.AutoSize = true;
            this.textInfoDbNamePanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInfoDbNamePanel.Location = new System.Drawing.Point(21, 121);
            this.textInfoDbNamePanel.Name = "textInfoDbNamePanel";
            this.textInfoDbNamePanel.Size = new System.Drawing.Size(155, 15);
            this.textInfoDbNamePanel.TabIndex = 26;
            this.textInfoDbNamePanel.Text = "Nombre Base de Datos";
            // 
            // editPassInstanciaPanel
            // 
            this.editPassInstanciaPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editPassInstanciaPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editPassInstanciaPanel.Location = new System.Drawing.Point(23, 228);
            this.editPassInstanciaPanel.Name = "editPassInstanciaPanel";
            this.editPassInstanciaPanel.PasswordChar = '*';
            this.editPassInstanciaPanel.Size = new System.Drawing.Size(204, 22);
            this.editPassInstanciaPanel.TabIndex = 105;
            // 
            // textInfoPassSql
            // 
            this.textInfoPassSql.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textInfoPassSql.AutoSize = true;
            this.textInfoPassSql.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInfoPassSql.Location = new System.Drawing.Point(20, 211);
            this.textInfoPassSql.Name = "textInfoPassSql";
            this.textInfoPassSql.Size = new System.Drawing.Size(203, 15);
            this.textInfoPassSql.TabIndex = 24;
            this.textInfoPassSql.Text = "Contraseña de la Instancia Sql";
            // 
            // editUserInstanciaPanel
            // 
            this.editUserInstanciaPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editUserInstanciaPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editUserInstanciaPanel.Location = new System.Drawing.Point(23, 185);
            this.editUserInstanciaPanel.Name = "editUserInstanciaPanel";
            this.editUserInstanciaPanel.Size = new System.Drawing.Size(234, 22);
            this.editUserInstanciaPanel.TabIndex = 104;
            this.editUserInstanciaPanel.Text = "sa";
            // 
            // textInfoUsuarioSql
            // 
            this.textInfoUsuarioSql.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textInfoUsuarioSql.AutoSize = true;
            this.textInfoUsuarioSql.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInfoUsuarioSql.Location = new System.Drawing.Point(20, 168);
            this.textInfoUsuarioSql.Name = "textInfoUsuarioSql";
            this.textInfoUsuarioSql.Size = new System.Drawing.Size(180, 15);
            this.textInfoUsuarioSql.TabIndex = 22;
            this.textInfoUsuarioSql.Text = "Usuario de la Instancia Sql";
            // 
            // editNombreInstanciaPanel
            // 
            this.editNombreInstanciaPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editNombreInstanciaPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editNombreInstanciaPanel.Location = new System.Drawing.Point(23, 95);
            this.editNombreInstanciaPanel.Name = "editNombreInstanciaPanel";
            this.editNombreInstanciaPanel.Size = new System.Drawing.Size(234, 22);
            this.editNombreInstanciaPanel.TabIndex = 102;
            // 
            // textInfoInstancia
            // 
            this.textInfoInstancia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textInfoInstancia.AutoSize = true;
            this.textInfoInstancia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInfoInstancia.Location = new System.Drawing.Point(20, 78);
            this.textInfoInstancia.Name = "textInfoInstancia";
            this.textInfoInstancia.Size = new System.Drawing.Size(224, 15);
            this.textInfoInstancia.TabIndex = 20;
            this.textInfoInstancia.Text = "Ingresa el Nombre de la Instancia";
            // 
            // textInfoIpServer
            // 
            this.textInfoIpServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textInfoIpServer.AutoSize = true;
            this.textInfoIpServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInfoIpServer.Location = new System.Drawing.Point(20, 30);
            this.textInfoIpServer.Name = "textInfoIpServer";
            this.textInfoIpServer.Size = new System.Drawing.Size(174, 15);
            this.textInfoIpServer.TabIndex = 19;
            this.textInfoIpServer.Text = "Ingresar la IP del Servidor";
            // 
            // editIpServerPanel
            // 
            this.editIpServerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editIpServerPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editIpServerPanel.Location = new System.Drawing.Point(23, 48);
            this.editIpServerPanel.Name = "editIpServerPanel";
            this.editIpServerPanel.Size = new System.Drawing.Size(234, 22);
            this.editIpServerPanel.TabIndex = 101;
            this.editIpServerPanel.Text = "127.0.0.1";
            // 
            // groupBoxComInstance
            // 
            this.groupBoxComInstance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxComInstance.Controls.Add(this.btnSeePassCom);
            this.groupBoxComInstance.Controls.Add(this.editDbNameComercial);
            this.groupBoxComInstance.Controls.Add(this.editPassInstanciaComercial);
            this.groupBoxComInstance.Controls.Add(this.textInfoDbNameComercial);
            this.groupBoxComInstance.Controls.Add(this.textInfoPassComercial);
            this.groupBoxComInstance.Controls.Add(this.editUserInstanciaComercial);
            this.groupBoxComInstance.Controls.Add(this.textInfoUserComercial);
            this.groupBoxComInstance.Controls.Add(this.editNombreInstanciaComercial);
            this.groupBoxComInstance.Controls.Add(this.textInfoInstanciaComercial);
            this.groupBoxComInstance.Controls.Add(this.textInfoIpComercial);
            this.groupBoxComInstance.Controls.Add(this.editIpServidorComercial);
            this.groupBoxComInstance.Location = new System.Drawing.Point(296, 71);
            this.groupBoxComInstance.MinimumSize = new System.Drawing.Size(278, 239);
            this.groupBoxComInstance.Name = "groupBoxComInstance";
            this.groupBoxComInstance.Size = new System.Drawing.Size(278, 271);
            this.groupBoxComInstance.TabIndex = 200;
            this.groupBoxComInstance.TabStop = false;
            this.groupBoxComInstance.Text = "Instancia SQLServer de Comercial";
            // 
            // btnSeePassCom
            // 
            this.btnSeePassCom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSeePassCom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSeePassCom.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnSeePassCom.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSeePassCom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeePassCom.Location = new System.Drawing.Point(233, 228);
            this.btnSeePassCom.Name = "btnSeePassCom";
            this.btnSeePassCom.Size = new System.Drawing.Size(24, 24);
            this.btnSeePassCom.TabIndex = 206;
            this.btnSeePassCom.UseVisualStyleBackColor = true;
            this.btnSeePassCom.Click += new System.EventHandler(this.btnSeePassCom_Click);
            // 
            // editDbNameComercial
            // 
            this.editDbNameComercial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editDbNameComercial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editDbNameComercial.ForeColor = System.Drawing.Color.Silver;
            this.editDbNameComercial.Location = new System.Drawing.Point(23, 138);
            this.editDbNameComercial.Name = "editDbNameComercial";
            this.editDbNameComercial.Size = new System.Drawing.Size(234, 22);
            this.editDbNameComercial.TabIndex = 203;
            this.editDbNameComercial.Text = "Nombre_BDatos_Comercial_01";
            this.editDbNameComercial.Enter += new System.EventHandler(this.editDbNameComercial_Enter);
            this.editDbNameComercial.Leave += new System.EventHandler(this.editDbNameComercial_Leave);
            // 
            // editPassInstanciaComercial
            // 
            this.editPassInstanciaComercial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editPassInstanciaComercial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editPassInstanciaComercial.Location = new System.Drawing.Point(23, 228);
            this.editPassInstanciaComercial.Name = "editPassInstanciaComercial";
            this.editPassInstanciaComercial.PasswordChar = '*';
            this.editPassInstanciaComercial.Size = new System.Drawing.Size(204, 22);
            this.editPassInstanciaComercial.TabIndex = 205;
            // 
            // textInfoDbNameComercial
            // 
            this.textInfoDbNameComercial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textInfoDbNameComercial.AutoSize = true;
            this.textInfoDbNameComercial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInfoDbNameComercial.Location = new System.Drawing.Point(20, 121);
            this.textInfoDbNameComercial.Name = "textInfoDbNameComercial";
            this.textInfoDbNameComercial.Size = new System.Drawing.Size(155, 15);
            this.textInfoDbNameComercial.TabIndex = 28;
            this.textInfoDbNameComercial.Text = "Nombre Base de Datos";
            // 
            // textInfoPassComercial
            // 
            this.textInfoPassComercial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textInfoPassComercial.AutoSize = true;
            this.textInfoPassComercial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInfoPassComercial.Location = new System.Drawing.Point(20, 211);
            this.textInfoPassComercial.Name = "textInfoPassComercial";
            this.textInfoPassComercial.Size = new System.Drawing.Size(203, 15);
            this.textInfoPassComercial.TabIndex = 24;
            this.textInfoPassComercial.Text = "Contraseña de la Instancia Sql";
            // 
            // editUserInstanciaComercial
            // 
            this.editUserInstanciaComercial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editUserInstanciaComercial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editUserInstanciaComercial.Location = new System.Drawing.Point(23, 185);
            this.editUserInstanciaComercial.Name = "editUserInstanciaComercial";
            this.editUserInstanciaComercial.Size = new System.Drawing.Size(234, 22);
            this.editUserInstanciaComercial.TabIndex = 204;
            this.editUserInstanciaComercial.Text = "sa";
            // 
            // textInfoUserComercial
            // 
            this.textInfoUserComercial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textInfoUserComercial.AutoSize = true;
            this.textInfoUserComercial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInfoUserComercial.Location = new System.Drawing.Point(20, 168);
            this.textInfoUserComercial.Name = "textInfoUserComercial";
            this.textInfoUserComercial.Size = new System.Drawing.Size(180, 15);
            this.textInfoUserComercial.TabIndex = 22;
            this.textInfoUserComercial.Text = "Usuario de la Instancia Sql";
            // 
            // editNombreInstanciaComercial
            // 
            this.editNombreInstanciaComercial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editNombreInstanciaComercial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editNombreInstanciaComercial.Location = new System.Drawing.Point(23, 95);
            this.editNombreInstanciaComercial.Name = "editNombreInstanciaComercial";
            this.editNombreInstanciaComercial.Size = new System.Drawing.Size(234, 22);
            this.editNombreInstanciaComercial.TabIndex = 202;
            // 
            // textInfoInstanciaComercial
            // 
            this.textInfoInstanciaComercial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textInfoInstanciaComercial.AutoSize = true;
            this.textInfoInstanciaComercial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInfoInstanciaComercial.Location = new System.Drawing.Point(20, 78);
            this.textInfoInstanciaComercial.Name = "textInfoInstanciaComercial";
            this.textInfoInstanciaComercial.Size = new System.Drawing.Size(224, 15);
            this.textInfoInstanciaComercial.TabIndex = 20;
            this.textInfoInstanciaComercial.Text = "Ingresa el Nombre de la Instancia";
            // 
            // textInfoIpComercial
            // 
            this.textInfoIpComercial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textInfoIpComercial.AutoSize = true;
            this.textInfoIpComercial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInfoIpComercial.Location = new System.Drawing.Point(20, 30);
            this.textInfoIpComercial.Name = "textInfoIpComercial";
            this.textInfoIpComercial.Size = new System.Drawing.Size(174, 15);
            this.textInfoIpComercial.TabIndex = 19;
            this.textInfoIpComercial.Text = "Ingresar la IP del Servidor";
            // 
            // editIpServidorComercial
            // 
            this.editIpServidorComercial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editIpServidorComercial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editIpServidorComercial.Location = new System.Drawing.Point(23, 48);
            this.editIpServidorComercial.Name = "editIpServidorComercial";
            this.editIpServidorComercial.Size = new System.Drawing.Size(234, 22);
            this.editIpServidorComercial.TabIndex = 201;
            this.editIpServidorComercial.Text = "127.0.0.1";
            // 
            // FormAddField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(587, 408);
            this.Controls.Add(this.groupBoxComInstance);
            this.Controls.Add(this.groupBoxPanelInstance);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panelToolbar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(603, 415);
            this.Name = "FormAddField";
            this.Text = "Agregar Protocolo de Internet";
            this.Load += new System.EventHandler(this.FormAddField_Load);
            this.panelToolbar.ResumeLayout(false);
            this.panelToolbar.PerformLayout();
            this.groupBoxPanelInstance.ResumeLayout(false);
            this.groupBoxPanelInstance.PerformLayout();
            this.groupBoxComInstance.ResumeLayout(false);
            this.groupBoxComInstance.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBoxPanelInstance;
        private System.Windows.Forms.TextBox editPassInstanciaPanel;
        private System.Windows.Forms.Label textInfoPassSql;
        private System.Windows.Forms.TextBox editUserInstanciaPanel;
        private System.Windows.Forms.Label textInfoUsuarioSql;
        private System.Windows.Forms.TextBox editNombreInstanciaPanel;
        private System.Windows.Forms.Label textInfoInstancia;
        private System.Windows.Forms.Label textInfoIpServer;
        private System.Windows.Forms.TextBox editIpServerPanel;
        private System.Windows.Forms.GroupBox groupBoxComInstance;
        private System.Windows.Forms.TextBox editPassInstanciaComercial;
        private System.Windows.Forms.Label textInfoPassComercial;
        private System.Windows.Forms.TextBox editUserInstanciaComercial;
        private System.Windows.Forms.Label textInfoUserComercial;
        private System.Windows.Forms.TextBox editNombreInstanciaComercial;
        private System.Windows.Forms.Label textInfoInstanciaComercial;
        private System.Windows.Forms.Label textInfoIpComercial;
        private System.Windows.Forms.TextBox editIpServidorComercial;
        private System.Windows.Forms.CheckBox checkBoxMismaInstancia;
        private System.Windows.Forms.TextBox editDbNamePanel;
        private System.Windows.Forms.Label textInfoDbNamePanel;
        private System.Windows.Forms.TextBox editDbNameComercial;
        private System.Windows.Forms.Label textInfoDbNameComercial;
        private System.Windows.Forms.Button btnSeePassPanel;
        private System.Windows.Forms.Button btnSeePassCom;
    }
}