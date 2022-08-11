namespace SyncTPV
{
    partial class FrmRuta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRuta));
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.btnAceptarRuta = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtRuta
            // 
            this.txtRuta.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtRuta.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRuta.Location = new System.Drawing.Point(79, 54);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRuta.Size = new System.Drawing.Size(219, 22);
            this.txtRuta.TabIndex = 0;
            this.txtRuta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnAceptarRuta
            // 
            this.btnAceptarRuta.BackColor = System.Drawing.Color.Gainsboro;
            this.btnAceptarRuta.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(112)))), ((int)(((byte)(67)))));
            this.btnAceptarRuta.FlatAppearance.BorderSize = 2;
            this.btnAceptarRuta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptarRuta.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptarRuta.ForeColor = System.Drawing.Color.Black;
            this.btnAceptarRuta.Location = new System.Drawing.Point(113, 108);
            this.btnAceptarRuta.Name = "btnAceptarRuta";
            this.btnAceptarRuta.Size = new System.Drawing.Size(126, 31);
            this.btnAceptarRuta.TabIndex = 2;
            this.btnAceptarRuta.Text = "Descargar Datos";
            this.btnAceptarRuta.UseVisualStyleBackColor = false;
            this.btnAceptarRuta.Click += new System.EventHandler(this.btnAceptarRuta_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtRuta);
            this.groupBox1.Controls.Add(this.btnAceptarRuta);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(28, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(365, 156);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Descargar Ruta Comercial";
            // 
            // FrmRuta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(418, 206);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmRuta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ruta a Descargar";
            this.Load += new System.EventHandler(this.FrmRuta_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.Button btnAceptarRuta;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}