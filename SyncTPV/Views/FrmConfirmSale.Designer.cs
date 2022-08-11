
namespace SyncTPV.Views
{
    partial class FrmConfirmSale
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfirmSale));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOkFrmConfirmSale = new SyncTPV.RoundedButton();
            this.textCambioFrmConfirmSale = new System.Windows.Forms.Label();
            this.textTotalFrmConfirmSale = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timerTickCloseFrmConfirmSale = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btnOkFrmConfirmSale);
            this.panel1.Controls.Add(this.textCambioFrmConfirmSale);
            this.panel1.Controls.Add(this.textTotalFrmConfirmSale);
            this.panel1.Location = new System.Drawing.Point(12, 180);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(349, 186);
            this.panel1.TabIndex = 1;
            // 
            // btnOkFrmConfirmSale
            // 
            this.btnOkFrmConfirmSale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOkFrmConfirmSale.BackColor = System.Drawing.Color.Transparent;
            this.btnOkFrmConfirmSale.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOkFrmConfirmSale.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnOkFrmConfirmSale.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnOkFrmConfirmSale.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOkFrmConfirmSale.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOkFrmConfirmSale.Location = new System.Drawing.Point(156, 145);
            this.btnOkFrmConfirmSale.Name = "btnOkFrmConfirmSale";
            this.btnOkFrmConfirmSale.Size = new System.Drawing.Size(190, 38);
            this.btnOkFrmConfirmSale.TabIndex = 4;
            this.btnOkFrmConfirmSale.Text = "Aceptar";
            this.btnOkFrmConfirmSale.UseVisualStyleBackColor = false;
            this.btnOkFrmConfirmSale.Click += new System.EventHandler(this.btnOkFrmConfirmSale_Click);
            // 
            // textCambioFrmConfirmSale
            // 
            this.textCambioFrmConfirmSale.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textCambioFrmConfirmSale.Font = new System.Drawing.Font("Roboto Black", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textCambioFrmConfirmSale.Location = new System.Drawing.Point(3, 76);
            this.textCambioFrmConfirmSale.Name = "textCambioFrmConfirmSale";
            this.textCambioFrmConfirmSale.Size = new System.Drawing.Size(343, 38);
            this.textCambioFrmConfirmSale.TabIndex = 3;
            this.textCambioFrmConfirmSale.Text = "$ 0 MXN";
            this.textCambioFrmConfirmSale.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textTotalFrmConfirmSale
            // 
            this.textTotalFrmConfirmSale.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textTotalFrmConfirmSale.Font = new System.Drawing.Font("Roboto Black", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTotalFrmConfirmSale.Location = new System.Drawing.Point(3, 18);
            this.textTotalFrmConfirmSale.Name = "textTotalFrmConfirmSale";
            this.textTotalFrmConfirmSale.Size = new System.Drawing.Size(343, 35);
            this.textTotalFrmConfirmSale.TabIndex = 1;
            this.textTotalFrmConfirmSale.Text = "$ 0 MXN";
            this.textTotalFrmConfirmSale.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(349, 162);
            this.panel2.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.pictureBox1.Location = new System.Drawing.Point(43, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(259, 156);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // timerTickCloseFrmConfirmSale
            // 
            this.timerTickCloseFrmConfirmSale.Tick += new System.EventHandler(this.timerTickCloseFrmConfirmSale_Tick);
            // 
            // FrmConfirmSale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(373, 378);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmConfirmSale";
            this.Text = "Documento Terminado";
            this.Load += new System.EventHandler(this.FrmConfirmSale_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label textCambioFrmConfirmSale;
        private System.Windows.Forms.Label textTotalFrmConfirmSale;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timerTickCloseFrmConfirmSale;
        private RoundedButton btnOkFrmConfirmSale;
    }
}