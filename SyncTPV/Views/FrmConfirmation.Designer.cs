
using SyncTPV.Helpers.Design;

namespace SyncTPV.Views
{
    partial class FrmConfirmation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfirmation));
            this.textMsgFrmConfirmation = new System.Windows.Forms.Label();
            this.btnCancelFrmConfirmation = new SyncTPV.Helpers.Design.CancelRoundedButton();
            this.btnOkFrmConfirmation = new SyncTPV.RoundedButton();
            this.progressBarFrmConfirmation = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // textMsgFrmConfirmation
            // 
            this.textMsgFrmConfirmation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textMsgFrmConfirmation.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textMsgFrmConfirmation.Location = new System.Drawing.Point(12, 9);
            this.textMsgFrmConfirmation.Name = "textMsgFrmConfirmation";
            this.textMsgFrmConfirmation.Size = new System.Drawing.Size(472, 105);
            this.textMsgFrmConfirmation.TabIndex = 3;
            this.textMsgFrmConfirmation.Text = "Mensaje";
            this.textMsgFrmConfirmation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancelFrmConfirmation
            // 
            this.btnCancelFrmConfirmation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelFrmConfirmation.BackColor = System.Drawing.Color.Transparent;
            this.btnCancelFrmConfirmation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelFrmConfirmation.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnCancelFrmConfirmation.FlatAppearance.BorderSize = 2;
            this.btnCancelFrmConfirmation.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Snow;
            this.btnCancelFrmConfirmation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelFrmConfirmation.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelFrmConfirmation.ForeColor = System.Drawing.Color.Black;
            this.btnCancelFrmConfirmation.Location = new System.Drawing.Point(12, 146);
            this.btnCancelFrmConfirmation.Name = "btnCancelFrmConfirmation";
            this.btnCancelFrmConfirmation.Size = new System.Drawing.Size(166, 43);
            this.btnCancelFrmConfirmation.TabIndex = 2;
            this.btnCancelFrmConfirmation.Text = "Cancelar";
            this.btnCancelFrmConfirmation.UseVisualStyleBackColor = false;
            this.btnCancelFrmConfirmation.Click += new System.EventHandler(this.btnCancelFrmConfirmation_Click);
            this.btnCancelFrmConfirmation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnCancelFrmConfirmation_KeyPress);
            // 
            // btnOkFrmConfirmation
            // 
            this.btnOkFrmConfirmation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOkFrmConfirmation.BackColor = System.Drawing.Color.Transparent;
            this.btnOkFrmConfirmation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOkFrmConfirmation.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnOkFrmConfirmation.FlatAppearance.BorderSize = 2;
            this.btnOkFrmConfirmation.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnOkFrmConfirmation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOkFrmConfirmation.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOkFrmConfirmation.ForeColor = System.Drawing.Color.Black;
            this.btnOkFrmConfirmation.Location = new System.Drawing.Point(314, 146);
            this.btnOkFrmConfirmation.Name = "btnOkFrmConfirmation";
            this.btnOkFrmConfirmation.Size = new System.Drawing.Size(170, 43);
            this.btnOkFrmConfirmation.TabIndex = 1;
            this.btnOkFrmConfirmation.Text = "Aceptar";
            this.btnOkFrmConfirmation.UseVisualStyleBackColor = false;
            this.btnOkFrmConfirmation.Click += new System.EventHandler(this.btnOk_Click);
            this.btnOkFrmConfirmation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnOkFrmConfirmation_KeyPress);
            // 
            // progressBarFrmConfirmation
            // 
            this.progressBarFrmConfirmation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarFrmConfirmation.Location = new System.Drawing.Point(71, 117);
            this.progressBarFrmConfirmation.Name = "progressBarFrmConfirmation";
            this.progressBarFrmConfirmation.Size = new System.Drawing.Size(353, 23);
            this.progressBarFrmConfirmation.TabIndex = 4;
            this.progressBarFrmConfirmation.Visible = false;
            // 
            // FrmConfirmation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(496, 201);
            this.Controls.Add(this.progressBarFrmConfirmation);
            this.Controls.Add(this.btnOkFrmConfirmation);
            this.Controls.Add(this.btnCancelFrmConfirmation);
            this.Controls.Add(this.textMsgFrmConfirmation);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmConfirmation";
            this.Text = "Confirmar";
            this.Load += new System.EventHandler(this.FrmConfirmation_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmConfirmation_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label textMsgFrmConfirmation;
        private System.Windows.Forms.ProgressBar progressBarFrmConfirmation;
        private RoundedButton btnOkFrmConfirmation;
        private CancelRoundedButton btnCancelFrmConfirmation;
    }
}