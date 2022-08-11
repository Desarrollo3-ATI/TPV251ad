namespace SyncTPV.Views.Mostrador
{
    partial class FormCodigoAgente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCodigoAgente));
            this.PanelToolbar = new System.Windows.Forms.Panel();
            this.editCodigoCajaPadre = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.editCodigoAgente = new System.Windows.Forms.TextBox();
            this.textInfoCodigoAgente = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.PanelToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelToolbar
            // 
            this.PanelToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelToolbar.BackColor = System.Drawing.Color.Coral;
            this.PanelToolbar.Controls.Add(this.editCodigoCajaPadre);
            this.PanelToolbar.Controls.Add(this.btnClose);
            this.PanelToolbar.Location = new System.Drawing.Point(-1, 0);
            this.PanelToolbar.Margin = new System.Windows.Forms.Padding(0);
            this.PanelToolbar.Name = "PanelToolbar";
            this.PanelToolbar.Size = new System.Drawing.Size(446, 75);
            this.PanelToolbar.TabIndex = 1;
            // 
            // editCodigoCajaPadre
            // 
            this.editCodigoCajaPadre.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.editCodigoCajaPadre.BackColor = System.Drawing.Color.FloralWhite;
            this.editCodigoCajaPadre.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editCodigoCajaPadre.Location = new System.Drawing.Point(111, 12);
            this.editCodigoCajaPadre.Name = "editCodigoCajaPadre";
            this.editCodigoCajaPadre.ReadOnly = true;
            this.editCodigoCajaPadre.Size = new System.Drawing.Size(296, 26);
            this.editCodigoCajaPadre.TabIndex = 3;
            this.editCodigoCajaPadre.Text = "Validación de agente";
            this.editCodigoCajaPadre.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.Location = new System.Drawing.Point(3, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 67);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Cerrar";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // editCodigoAgente
            // 
            this.editCodigoAgente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editCodigoAgente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.editCodigoAgente.Font = new System.Drawing.Font("Roboto Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editCodigoAgente.Location = new System.Drawing.Point(15, 119);
            this.editCodigoAgente.Name = "editCodigoAgente";
            this.editCodigoAgente.Size = new System.Drawing.Size(419, 27);
            this.editCodigoAgente.TabIndex = 0;
            this.editCodigoAgente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.editCodigoAgente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editCodigoAgente_KeyPress);
            // 
            // textInfoCodigoAgente
            // 
            this.textInfoCodigoAgente.AutoSize = true;
            this.textInfoCodigoAgente.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInfoCodigoAgente.Location = new System.Drawing.Point(12, 85);
            this.textInfoCodigoAgente.Name = "textInfoCodigoAgente";
            this.textInfoCodigoAgente.Size = new System.Drawing.Size(209, 18);
            this.textInfoCodigoAgente.TabIndex = 4;
            this.textInfoCodigoAgente.Text = "Ingresar el Código del Agente";
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptar.Location = new System.Drawing.Point(283, 183);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(148, 39);
            this.btnAceptar.TabIndex = 1;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // FormCodigoAgente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(443, 234);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.textInfoCodigoAgente);
            this.Controls.Add(this.editCodigoAgente);
            this.Controls.Add(this.PanelToolbar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(459, 273);
            this.MinimumSize = new System.Drawing.Size(459, 273);
            this.Name = "FormCodigoAgente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormCodigoAgente";
            this.Load += new System.EventHandler(this.FormCodigoAgente_Load);
            this.PanelToolbar.ResumeLayout(false);
            this.PanelToolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel PanelToolbar;
        private System.Windows.Forms.TextBox editCodigoCajaPadre;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox editCodigoAgente;
        private System.Windows.Forms.Label textInfoCodigoAgente;
        private System.Windows.Forms.Button btnAceptar;
    }
}