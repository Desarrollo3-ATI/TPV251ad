namespace SyncTPV
{
    partial class FrmImageCarga
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImageCarga));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblTexto = new System.Windows.Forms.Label();
            this.picCopy = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.picCopy)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblTexto
            // 
            this.lblTexto.AutoSize = true;
            this.lblTexto.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTexto.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblTexto.Location = new System.Drawing.Point(44, 15);
            this.lblTexto.Name = "lblTexto";
            this.lblTexto.Size = new System.Drawing.Size(287, 33);
            this.lblTexto.TabIndex = 0;
            this.lblTexto.Text = "Copiado al portapapeles";
            // 
            // picCopy
            // 
            this.picCopy.Image = ((System.Drawing.Image)(resources.GetObject("picCopy.Image")));
            this.picCopy.Location = new System.Drawing.Point(13, 20);
            this.picCopy.Name = "picCopy";
            this.picCopy.Size = new System.Drawing.Size(28, 28);
            this.picCopy.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCopy.TabIndex = 9;
            this.picCopy.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.picCopy);
            this.groupBox1.Controls.Add(this.lblTexto);
            this.groupBox1.Location = new System.Drawing.Point(6, -2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(343, 58);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // FrmImageCarga
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(190)))), ((int)(((byte)(125)))));
            this.ClientSize = new System.Drawing.Size(356, 60);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(356, 60);
            this.MinimumSize = new System.Drawing.Size(356, 60);
            this.Name = "FrmImageCarga";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmImageCarga";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmImageCarga_FormClosed);
            this.Load += new System.EventHandler(this.FrmImageCarga_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picCopy)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblTexto;
        private System.Windows.Forms.PictureBox picCopy;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}