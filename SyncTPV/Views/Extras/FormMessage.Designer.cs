namespace SyncTPV
{
    partial class FormMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMessage));
            this.lbl1 = new System.Windows.Forms.TextBox();
            this.picCopy = new System.Windows.Forms.PictureBox();
            this.lbl2 = new System.Windows.Forms.Label();
            this.picCerrar = new System.Windows.Forms.PictureBox();
            this.pnlSup = new System.Windows.Forms.Panel();
            this.imgMessage = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnAceptar = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picCopy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCerrar)).BeginInit();
            this.pnlSup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAceptar)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl1
            // 
            this.lbl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl1.BackColor = System.Drawing.Color.White;
            this.lbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.Location = new System.Drawing.Point(79, 12);
            this.lbl1.Multiline = true;
            this.lbl1.Name = "lbl1";
            this.lbl1.ReadOnly = true;
            this.lbl1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.lbl1.Size = new System.Drawing.Size(534, 148);
            this.lbl1.TabIndex = 6;
            this.lbl1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lbl1_KeyUp);
            // 
            // picCopy
            // 
            this.picCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picCopy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picCopy.Image = ((System.Drawing.Image)(resources.GetObject("picCopy.Image")));
            this.picCopy.Location = new System.Drawing.Point(619, 12);
            this.picCopy.Name = "picCopy";
            this.picCopy.Size = new System.Drawing.Size(28, 28);
            this.picCopy.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCopy.TabIndex = 8;
            this.picCopy.TabStop = false;
            this.toolTip1.SetToolTip(this.picCopy, "Copia todo el cuerpo\r\ndel mensaje");
            this.picCopy.Click += new System.EventHandler(this.picCopy_Click);
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl2.Location = new System.Drawing.Point(510, 0);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(14, 16);
            this.lbl2.TabIndex = 0;
            this.lbl2.Text = "-";
            // 
            // picCerrar
            // 
            this.picCerrar.Dock = System.Windows.Forms.DockStyle.Left;
            this.picCerrar.Image = ((System.Drawing.Image)(resources.GetObject("picCerrar.Image")));
            this.picCerrar.Location = new System.Drawing.Point(0, 0);
            this.picCerrar.Name = "picCerrar";
            this.picCerrar.Size = new System.Drawing.Size(30, 19);
            this.picCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCerrar.TabIndex = 42;
            this.picCerrar.TabStop = false;
            this.picCerrar.Click += new System.EventHandler(this.picCerrar_Click);
            // 
            // pnlSup
            // 
            this.pnlSup.BackColor = System.Drawing.Color.SeaGreen;
            this.pnlSup.Controls.Add(this.picCerrar);
            this.pnlSup.Controls.Add(this.lbl2);
            this.pnlSup.ForeColor = System.Drawing.Color.White;
            this.pnlSup.Location = new System.Drawing.Point(3, 254);
            this.pnlSup.Name = "pnlSup";
            this.pnlSup.Size = new System.Drawing.Size(524, 19);
            this.pnlSup.TabIndex = 2;
            this.pnlSup.Visible = false;
            // 
            // imgMessage
            // 
            this.imgMessage.Image = ((System.Drawing.Image)(resources.GetObject("imgMessage.Image")));
            this.imgMessage.Location = new System.Drawing.Point(3, 12);
            this.imgMessage.Name = "imgMessage";
            this.imgMessage.Size = new System.Drawing.Size(70, 74);
            this.imgMessage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgMessage.TabIndex = 9;
            this.imgMessage.TabStop = false;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptar.Location = new System.Drawing.Point(516, 166);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(131, 40);
            this.btnAceptar.TabIndex = 10;
            this.btnAceptar.TabStop = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click_1);
            // 
            // FormMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(659, 218);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.imgMessage);
            this.Controls.Add(this.picCopy);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.pnlSup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMessage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ventana de mensaje";
            this.Load += new System.EventHandler(this.FormMessage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picCopy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCerrar)).EndInit();
            this.pnlSup.ResumeLayout(false);
            this.pnlSup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAceptar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox lbl1;
        private System.Windows.Forms.PictureBox picCopy;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.PictureBox picCerrar;
        private System.Windows.Forms.Panel pnlSup;
        private System.Windows.Forms.PictureBox imgMessage;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox btnAceptar;
    }
}