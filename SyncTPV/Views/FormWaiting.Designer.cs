
namespace SyncTPV.Views
{
    partial class FormWaiting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWaiting));
            this.textMessage = new System.Windows.Forms.Label();
            this.imgWaiting = new System.Windows.Forms.PictureBox();
            this.progressBarFrmWaiting = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.imgWaiting)).BeginInit();
            this.SuspendLayout();
            // 
            // textMessage
            // 
            this.textMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textMessage.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textMessage.Location = new System.Drawing.Point(12, 9);
            this.textMessage.Name = "textMessage";
            this.textMessage.Size = new System.Drawing.Size(344, 98);
            this.textMessage.TabIndex = 1;
            this.textMessage.Text = "Espera un momento, por favor";
            this.textMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imgWaiting
            // 
            this.imgWaiting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imgWaiting.Image = ((System.Drawing.Image)(resources.GetObject("imgWaiting.Image")));
            this.imgWaiting.Location = new System.Drawing.Point(118, 110);
            this.imgWaiting.Name = "imgWaiting";
            this.imgWaiting.Size = new System.Drawing.Size(117, 113);
            this.imgWaiting.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgWaiting.TabIndex = 2;
            this.imgWaiting.TabStop = false;
            // 
            // progressBarFrmWaiting
            // 
            this.progressBarFrmWaiting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarFrmWaiting.BackColor = System.Drawing.Color.White;
            this.progressBarFrmWaiting.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.progressBarFrmWaiting.Location = new System.Drawing.Point(47, 244);
            this.progressBarFrmWaiting.Name = "progressBarFrmWaiting";
            this.progressBarFrmWaiting.Size = new System.Drawing.Size(270, 14);
            this.progressBarFrmWaiting.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBarFrmWaiting.TabIndex = 3;
            // 
            // FormWaiting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(368, 280);
            this.ControlBox = false;
            this.Controls.Add(this.progressBarFrmWaiting);
            this.Controls.Add(this.imgWaiting);
            this.Controls.Add(this.textMessage);
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormWaiting";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cargando";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmWaiting_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmWaiting_FormClosed);
            this.Load += new System.EventHandler(this.FrmWaiting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgWaiting)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label textMessage;
        private System.Windows.Forms.PictureBox imgWaiting;
        private System.Windows.Forms.ProgressBar progressBarFrmWaiting;
    }
}