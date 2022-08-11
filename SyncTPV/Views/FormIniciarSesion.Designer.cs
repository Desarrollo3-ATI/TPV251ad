namespace SyncTPV
{
    partial class FormIniciarSesion
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormIniciarSesion));
            this.editUsernameFrmIniciarSesion = new System.Windows.Forms.TextBox();
            this.editPassFrmIniciarSesion = new System.Windows.Forms.TextBox();
            this.btnConfiguracionInicial = new System.Windows.Forms.PictureBox();
            this.PictureIconPass = new System.Windows.Forms.PictureBox();
            this.PictureIconUser = new System.Windows.Forms.PictureBox();
            this.PictureLogo = new System.Windows.Forms.PictureBox();
            this.linkLabelActivateLicenseFrmIniciarSesion = new System.Windows.Forms.LinkLabel();
            this.textVersionFrmLogin = new System.Windows.Forms.Label();
            this.checkBoxRecordarLogin = new System.Windows.Forms.CheckBox();
            this.btnLogin = new SyncTPV.RoundedButton();
            ((System.ComponentModel.ISupportInitialize)(this.btnConfiguracionInicial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureIconPass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureIconUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // editUsernameFrmIniciarSesion
            // 
            this.editUsernameFrmIniciarSesion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.editUsernameFrmIniciarSesion.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editUsernameFrmIniciarSesion.Location = new System.Drawing.Point(58, 173);
            this.editUsernameFrmIniciarSesion.Name = "editUsernameFrmIniciarSesion";
            this.editUsernameFrmIniciarSesion.Size = new System.Drawing.Size(232, 22);
            this.editUsernameFrmIniciarSesion.TabIndex = 0;
            this.editUsernameFrmIniciarSesion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editUsernameFrmIniciarSesion_KeyPress);
            // 
            // editPassFrmIniciarSesion
            // 
            this.editPassFrmIniciarSesion.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editPassFrmIniciarSesion.Location = new System.Drawing.Point(58, 213);
            this.editPassFrmIniciarSesion.Name = "editPassFrmIniciarSesion";
            this.editPassFrmIniciarSesion.Size = new System.Drawing.Size(232, 22);
            this.editPassFrmIniciarSesion.TabIndex = 1;
            this.editPassFrmIniciarSesion.UseSystemPasswordChar = true;
            this.editPassFrmIniciarSesion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPass_KeyPress);
            // 
            // btnConfiguracionInicial
            // 
            this.btnConfiguracionInicial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfiguracionInicial.BackColor = System.Drawing.Color.Linen;
            this.btnConfiguracionInicial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnConfiguracionInicial.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfiguracionInicial.Location = new System.Drawing.Point(286, 12);
            this.btnConfiguracionInicial.Name = "btnConfiguracionInicial";
            this.btnConfiguracionInicial.Size = new System.Drawing.Size(36, 32);
            this.btnConfiguracionInicial.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnConfiguracionInicial.TabIndex = 6;
            this.btnConfiguracionInicial.TabStop = false;
            this.btnConfiguracionInicial.Click += new System.EventHandler(this.btnConfiguracion_Click);
            // 
            // PictureIconPass
            // 
            this.PictureIconPass.Image = ((System.Drawing.Image)(resources.GetObject("PictureIconPass.Image")));
            this.PictureIconPass.Location = new System.Drawing.Point(32, 213);
            this.PictureIconPass.Name = "PictureIconPass";
            this.PictureIconPass.Size = new System.Drawing.Size(20, 20);
            this.PictureIconPass.TabIndex = 4;
            this.PictureIconPass.TabStop = false;
            // 
            // PictureIconUser
            // 
            this.PictureIconUser.Image = ((System.Drawing.Image)(resources.GetObject("PictureIconUser.Image")));
            this.PictureIconUser.Location = new System.Drawing.Point(32, 173);
            this.PictureIconUser.Name = "PictureIconUser";
            this.PictureIconUser.Size = new System.Drawing.Size(20, 20);
            this.PictureIconUser.TabIndex = 3;
            this.PictureIconUser.TabStop = false;
            // 
            // PictureLogo
            // 
            this.PictureLogo.Image = ((System.Drawing.Image)(resources.GetObject("PictureLogo.Image")));
            this.PictureLogo.Location = new System.Drawing.Point(117, 36);
            this.PictureLogo.Name = "PictureLogo";
            this.PictureLogo.Size = new System.Drawing.Size(113, 113);
            this.PictureLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureLogo.TabIndex = 0;
            this.PictureLogo.TabStop = false;
            // 
            // linkLabelActivateLicenseFrmIniciarSesion
            // 
            this.linkLabelActivateLicenseFrmIniciarSesion.ActiveLinkColor = System.Drawing.Color.Orange;
            this.linkLabelActivateLicenseFrmIniciarSesion.AutoSize = true;
            this.linkLabelActivateLicenseFrmIniciarSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabelActivateLicenseFrmIniciarSesion.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelActivateLicenseFrmIniciarSesion.Location = new System.Drawing.Point(89, 325);
            this.linkLabelActivateLicenseFrmIniciarSesion.Name = "linkLabelActivateLicenseFrmIniciarSesion";
            this.linkLabelActivateLicenseFrmIniciarSesion.Size = new System.Drawing.Size(135, 28);
            this.linkLabelActivateLicenseFrmIniciarSesion.TabIndex = 4;
            this.linkLabelActivateLicenseFrmIniciarSesion.TabStop = true;
            this.linkLabelActivateLicenseFrmIniciarSesion.Text = "  Tienes una Synckey? \r\nClick aquí para activarla";
            this.linkLabelActivateLicenseFrmIniciarSesion.Visible = false;
            this.linkLabelActivateLicenseFrmIniciarSesion.Click += new System.EventHandler(this.linkLabelActivateLicenseFrmIniciarSesion_Click);
            // 
            // textVersionFrmLogin
            // 
            this.textVersionFrmLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textVersionFrmLogin.Font = new System.Drawing.Font("Roboto Black", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textVersionFrmLogin.Location = new System.Drawing.Point(176, 369);
            this.textVersionFrmLogin.Name = "textVersionFrmLogin";
            this.textVersionFrmLogin.Size = new System.Drawing.Size(158, 23);
            this.textVersionFrmLogin.TabIndex = 10;
            this.textVersionFrmLogin.Text = "Validando...";
            this.textVersionFrmLogin.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // checkBoxRecordarLogin
            // 
            this.checkBoxRecordarLogin.AutoSize = true;
            this.checkBoxRecordarLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBoxRecordarLogin.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxRecordarLogin.Location = new System.Drawing.Point(92, 243);
            this.checkBoxRecordarLogin.Name = "checkBoxRecordarLogin";
            this.checkBoxRecordarLogin.Size = new System.Drawing.Size(153, 17);
            this.checkBoxRecordarLogin.TabIndex = 2;
            this.checkBoxRecordarLogin.Text = "Recordar Inicio de Sesión";
            this.checkBoxRecordarLogin.UseVisualStyleBackColor = true;
            this.checkBoxRecordarLogin.CheckedChanged += new System.EventHandler(this.checkBoxRecordarLogin_CheckedChanged);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnLogin.FlatAppearance.BorderSize = 2;
            this.btnLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.Color.Black;
            this.btnLogin.Location = new System.Drawing.Point(72, 276);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(205, 46);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Iniciar Sesión";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // FormIniciarSesion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(334, 391);
            this.Controls.Add(this.checkBoxRecordarLogin);
            this.Controls.Add(this.textVersionFrmLogin);
            this.Controls.Add(this.linkLabelActivateLicenseFrmIniciarSesion);
            this.Controls.Add(this.btnConfiguracionInicial);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.PictureIconPass);
            this.Controls.Add(this.PictureIconUser);
            this.Controls.Add(this.editPassFrmIniciarSesion);
            this.Controls.Add(this.editUsernameFrmIniciarSesion);
            this.Controls.Add(this.PictureLogo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(350, 430);
            this.Name = "FormIniciarSesion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inicio de Sesion";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmIniciarSesion_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnConfiguracionInicial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureIconPass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureIconUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureLogo;
        private System.Windows.Forms.TextBox editUsernameFrmIniciarSesion;
        private System.Windows.Forms.TextBox editPassFrmIniciarSesion;
        private System.Windows.Forms.PictureBox PictureIconUser;
        private System.Windows.Forms.PictureBox PictureIconPass;
        private System.Windows.Forms.PictureBox btnConfiguracionInicial;
        private System.Windows.Forms.LinkLabel linkLabelActivateLicenseFrmIniciarSesion;
        private System.Windows.Forms.Label textVersionFrmLogin;
        private System.Windows.Forms.CheckBox checkBoxRecordarLogin;
        private RoundedButton btnLogin;
    }
}

