
using SyncTPV.Helpers.Design;

namespace SyncTPV.Views
{
    partial class FrmAddObservation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddObservation));
            this.editObservationFrmAddObservation = new System.Windows.Forms.TextBox();
            this.btnCancelarFrmAddObservation = new SyncTPV.Helpers.Design.CancelRoundedButton();
            this.btnOkFrmAddObservation = new SyncTPV.RoundedButton();
            this.SuspendLayout();
            // 
            // editObservationFrmAddObservation
            // 
            this.editObservationFrmAddObservation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editObservationFrmAddObservation.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editObservationFrmAddObservation.Location = new System.Drawing.Point(12, 12);
            this.editObservationFrmAddObservation.MaxLength = 10000;
            this.editObservationFrmAddObservation.Multiline = true;
            this.editObservationFrmAddObservation.Name = "editObservationFrmAddObservation";
            this.editObservationFrmAddObservation.Size = new System.Drawing.Size(404, 107);
            this.editObservationFrmAddObservation.TabIndex = 0;
            // 
            // btnCancelarFrmAddObservation
            // 
            this.btnCancelarFrmAddObservation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelarFrmAddObservation.BackColor = System.Drawing.Color.Transparent;
            this.btnCancelarFrmAddObservation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelarFrmAddObservation.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnCancelarFrmAddObservation.FlatAppearance.BorderSize = 2;
            this.btnCancelarFrmAddObservation.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Snow;
            this.btnCancelarFrmAddObservation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarFrmAddObservation.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarFrmAddObservation.Location = new System.Drawing.Point(12, 129);
            this.btnCancelarFrmAddObservation.Name = "btnCancelarFrmAddObservation";
            this.btnCancelarFrmAddObservation.Size = new System.Drawing.Size(147, 37);
            this.btnCancelarFrmAddObservation.TabIndex = 2;
            this.btnCancelarFrmAddObservation.Text = "Cancelar";
            this.btnCancelarFrmAddObservation.UseVisualStyleBackColor = false;
            this.btnCancelarFrmAddObservation.Visible = false;
            this.btnCancelarFrmAddObservation.Click += new System.EventHandler(this.btnCancelarFrmAddObservation_Click);
            // 
            // btnOkFrmAddObservation
            // 
            this.btnOkFrmAddObservation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOkFrmAddObservation.BackColor = System.Drawing.Color.Transparent;
            this.btnOkFrmAddObservation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOkFrmAddObservation.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnOkFrmAddObservation.FlatAppearance.BorderSize = 2;
            this.btnOkFrmAddObservation.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnOkFrmAddObservation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOkFrmAddObservation.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOkFrmAddObservation.Location = new System.Drawing.Point(263, 129);
            this.btnOkFrmAddObservation.Name = "btnOkFrmAddObservation";
            this.btnOkFrmAddObservation.Size = new System.Drawing.Size(153, 37);
            this.btnOkFrmAddObservation.TabIndex = 1;
            this.btnOkFrmAddObservation.Text = "Aceptar";
            this.btnOkFrmAddObservation.UseVisualStyleBackColor = false;
            this.btnOkFrmAddObservation.Click += new System.EventHandler(this.btnOkFrmAddObservation_Click);
            // 
            // FrmAddObservation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(432, 177);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancelarFrmAddObservation);
            this.Controls.Add(this.btnOkFrmAddObservation);
            this.Controls.Add(this.editObservationFrmAddObservation);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(448, 216);
            this.MinimumSize = new System.Drawing.Size(448, 216);
            this.Name = "FrmAddObservation";
            this.Text = "Observación";
            this.Load += new System.EventHandler(this.FrmAddObservation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox editObservationFrmAddObservation;
        private RoundedButton btnOkFrmAddObservation;
        private CancelRoundedButton btnCancelarFrmAddObservation;
    }
}