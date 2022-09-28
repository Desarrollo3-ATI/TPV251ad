namespace SyncTPV.Views.Extras
{
    partial class FormConfigTickets
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfigTickets));
            this.panelTollbar = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.groupBoxEncabezadoTicket = new System.Windows.Forms.GroupBox();
            this.checkBoxFolio = new System.Windows.Forms.CheckBox();
            this.checkBoxCodigoUsuario = new System.Windows.Forms.CheckBox();
            this.checkBoxCodigoCaja = new System.Windows.Forms.CheckBox();
            this.checkBoxNombreUsuario = new System.Windows.Forms.CheckBox();
            this.checkBoxFechaHora = new System.Windows.Forms.CheckBox();
            this.panelTollbar.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.groupBoxEncabezadoTicket.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTollbar
            // 
            this.panelTollbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTollbar.BackColor = System.Drawing.Color.Coral;
            this.panelTollbar.Controls.Add(this.btnClose);
            this.panelTollbar.Location = new System.Drawing.Point(-2, 0);
            this.panelTollbar.Name = "panelTollbar";
            this.panelTollbar.Size = new System.Drawing.Size(804, 70);
            this.panelTollbar.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.Location = new System.Drawing.Point(3, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 64);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Cerrar";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnClose_KeyUp);
            // 
            // panelContent
            // 
            this.panelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContent.BackColor = System.Drawing.Color.FloralWhite;
            this.panelContent.Controls.Add(this.groupBoxEncabezadoTicket);
            this.panelContent.Location = new System.Drawing.Point(-2, 69);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(804, 383);
            this.panelContent.TabIndex = 1;
            // 
            // groupBoxEncabezadoTicket
            // 
            this.groupBoxEncabezadoTicket.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxEncabezadoTicket.Controls.Add(this.checkBoxFechaHora);
            this.groupBoxEncabezadoTicket.Controls.Add(this.checkBoxNombreUsuario);
            this.groupBoxEncabezadoTicket.Controls.Add(this.checkBoxCodigoCaja);
            this.groupBoxEncabezadoTicket.Controls.Add(this.checkBoxCodigoUsuario);
            this.groupBoxEncabezadoTicket.Controls.Add(this.checkBoxFolio);
            this.groupBoxEncabezadoTicket.Location = new System.Drawing.Point(14, 10);
            this.groupBoxEncabezadoTicket.Name = "groupBoxEncabezadoTicket";
            this.groupBoxEncabezadoTicket.Size = new System.Drawing.Size(776, 138);
            this.groupBoxEncabezadoTicket.TabIndex = 0;
            this.groupBoxEncabezadoTicket.TabStop = false;
            this.groupBoxEncabezadoTicket.Text = "Encabezado del Ticket";
            // 
            // checkBoxFolio
            // 
            this.checkBoxFolio.AutoSize = true;
            this.checkBoxFolio.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxFolio.Location = new System.Drawing.Point(6, 34);
            this.checkBoxFolio.Name = "checkBoxFolio";
            this.checkBoxFolio.Size = new System.Drawing.Size(100, 19);
            this.checkBoxFolio.TabIndex = 0;
            this.checkBoxFolio.Text = "Mostrar Folio";
            this.checkBoxFolio.UseVisualStyleBackColor = true;
            this.checkBoxFolio.CheckedChanged += new System.EventHandler(this.checkBoxFolio_CheckedChanged);
            this.checkBoxFolio.KeyUp += new System.Windows.Forms.KeyEventHandler(this.checkBoxFolio_KeyUp);
            // 
            // checkBoxCodigoUsuario
            // 
            this.checkBoxCodigoUsuario.AutoSize = true;
            this.checkBoxCodigoUsuario.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxCodigoUsuario.Location = new System.Drawing.Point(6, 72);
            this.checkBoxCodigoUsuario.Name = "checkBoxCodigoUsuario";
            this.checkBoxCodigoUsuario.Size = new System.Drawing.Size(178, 19);
            this.checkBoxCodigoUsuario.TabIndex = 1;
            this.checkBoxCodigoUsuario.Text = "Mostrar Código del Usuario";
            this.checkBoxCodigoUsuario.UseVisualStyleBackColor = true;
            this.checkBoxCodigoUsuario.CheckedChanged += new System.EventHandler(this.checkBoxCodigoUsuario_CheckedChanged);
            this.checkBoxCodigoUsuario.KeyUp += new System.Windows.Forms.KeyEventHandler(this.checkBoxCodigoUsuario_KeyUp);
            // 
            // checkBoxCodigoCaja
            // 
            this.checkBoxCodigoCaja.AutoSize = true;
            this.checkBoxCodigoCaja.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxCodigoCaja.Location = new System.Drawing.Point(249, 34);
            this.checkBoxCodigoCaja.Name = "checkBoxCodigoCaja";
            this.checkBoxCodigoCaja.Size = new System.Drawing.Size(170, 19);
            this.checkBoxCodigoCaja.TabIndex = 2;
            this.checkBoxCodigoCaja.Text = "Mostrar Código de la Caja";
            this.checkBoxCodigoCaja.UseVisualStyleBackColor = true;
            this.checkBoxCodigoCaja.CheckedChanged += new System.EventHandler(this.checkBoxCodigoCaja_CheckedChanged);
            this.checkBoxCodigoCaja.KeyUp += new System.Windows.Forms.KeyEventHandler(this.checkBoxCodigoCaja_KeyUp);
            // 
            // checkBoxNombreUsuario
            // 
            this.checkBoxNombreUsuario.AutoSize = true;
            this.checkBoxNombreUsuario.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxNombreUsuario.Location = new System.Drawing.Point(249, 72);
            this.checkBoxNombreUsuario.Name = "checkBoxNombreUsuario";
            this.checkBoxNombreUsuario.Size = new System.Drawing.Size(164, 19);
            this.checkBoxNombreUsuario.TabIndex = 3;
            this.checkBoxNombreUsuario.Text = "Mostrar Nombre Usuario";
            this.checkBoxNombreUsuario.UseVisualStyleBackColor = true;
            this.checkBoxNombreUsuario.CheckedChanged += new System.EventHandler(this.checkBoxNombreUsuario_CheckedChanged);
            this.checkBoxNombreUsuario.KeyUp += new System.Windows.Forms.KeyEventHandler(this.checkBoxNombreUsuario_KeyUp);
            // 
            // checkBoxFechaHora
            // 
            this.checkBoxFechaHora.AutoSize = true;
            this.checkBoxFechaHora.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxFechaHora.Location = new System.Drawing.Point(516, 34);
            this.checkBoxFechaHora.Name = "checkBoxFechaHora";
            this.checkBoxFechaHora.Size = new System.Drawing.Size(138, 19);
            this.checkBoxFechaHora.TabIndex = 4;
            this.checkBoxFechaHora.Text = "Mostrar Fecha Hora";
            this.checkBoxFechaHora.UseVisualStyleBackColor = true;
            this.checkBoxFechaHora.CheckedChanged += new System.EventHandler(this.checkBoxFechaHora_CheckedChanged);
            this.checkBoxFechaHora.KeyUp += new System.Windows.Forms.KeyEventHandler(this.checkBoxFechaHora_KeyUp);
            // 
            // FormConfigTickets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelTollbar);
            this.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(816, 489);
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "FormConfigTickets";
            this.Text = "Configuración de ticketstiros";
            this.Load += new System.EventHandler(this.FormConfigTickets_Load);
            this.panelTollbar.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.groupBoxEncabezadoTicket.ResumeLayout(false);
            this.groupBoxEncabezadoTicket.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTollbar;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.GroupBox groupBoxEncabezadoTicket;
        private System.Windows.Forms.CheckBox checkBoxNombreUsuario;
        private System.Windows.Forms.CheckBox checkBoxCodigoCaja;
        private System.Windows.Forms.CheckBox checkBoxCodigoUsuario;
        private System.Windows.Forms.CheckBox checkBoxFolio;
        private System.Windows.Forms.CheckBox checkBoxFechaHora;
    }
}