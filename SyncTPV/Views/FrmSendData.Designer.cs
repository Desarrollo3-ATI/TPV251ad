
namespace SyncTPV.Views
{
    partial class FrmSendData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSendData));
            this.btnAceptar = new SyncTPV.RoundedButton();
            this.textDescription = new System.Windows.Forms.Label();
            this.progressBarSendData = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptar.BackColor = System.Drawing.Color.Transparent;
            this.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptar.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnAceptar.FlatAppearance.BorderSize = 2;
            this.btnAceptar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Location = new System.Drawing.Point(297, 261);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(228, 43);
            this.btnAceptar.TabIndex = 0;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // textDescription
            // 
            this.textDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textDescription.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textDescription.Location = new System.Drawing.Point(12, 13);
            this.textDescription.Name = "textDescription";
            this.textDescription.Size = new System.Drawing.Size(513, 139);
            this.textDescription.TabIndex = 1;
            this.textDescription.Text = "Description";
            this.textDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBarSendData
            // 
            this.progressBarSendData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarSendData.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.progressBarSendData.Location = new System.Drawing.Point(15, 199);
            this.progressBarSendData.Name = "progressBarSendData";
            this.progressBarSendData.Size = new System.Drawing.Size(510, 23);
            this.progressBarSendData.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarSendData.TabIndex = 2;
            // 
            // FrmSendData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(537, 316);
            this.Controls.Add(this.progressBarSendData);
            this.Controls.Add(this.textDescription);
            this.Controls.Add(this.btnAceptar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSendData";
            this.Text = "FrmSendData";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmSendData_FormClosed);
            this.Load += new System.EventHandler(this.FrmSendData_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label textDescription;
        private System.Windows.Forms.ProgressBar progressBarSendData;
        private RoundedButton btnAceptar;
    }
}