
namespace SyncTPV.Views.Withdrawals
{
    partial class FrmTerminateWithdrawal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTerminateWithdrawal));
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelInformation = new System.Windows.Forms.Panel();
            this.btnAceptar = new SyncTPV.RoundedButton();
            this.editDescription = new System.Windows.Forms.TextBox();
            this.textDescription = new System.Windows.Forms.Label();
            this.editConcept = new System.Windows.Forms.TextBox();
            this.textConcept = new System.Windows.Forms.Label();
            this.editTotales = new System.Windows.Forms.TextBox();
            this.panelToolbar.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelToolbar
            // 
            this.panelToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelToolbar.BackColor = System.Drawing.Color.Coral;
            this.panelToolbar.Controls.Add(this.btnClose);
            this.panelToolbar.Location = new System.Drawing.Point(0, -1);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(738, 72);
            this.panelToolbar.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Image = global::SyncTPV.Properties.Resources.close;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.Location = new System.Drawing.Point(12, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 66);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Cerrar";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelContent
            // 
            this.panelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContent.Controls.Add(this.panelInformation);
            this.panelContent.Controls.Add(this.btnAceptar);
            this.panelContent.Controls.Add(this.editDescription);
            this.panelContent.Controls.Add(this.textDescription);
            this.panelContent.Controls.Add(this.editConcept);
            this.panelContent.Controls.Add(this.textConcept);
            this.panelContent.Location = new System.Drawing.Point(12, 77);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(713, 320);
            this.panelContent.TabIndex = 1;
            // 
            // panelInformation
            // 
            this.panelInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelInformation.AutoScroll = true;
            this.panelInformation.Controls.Add(this.editTotales);
            this.panelInformation.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelInformation.Location = new System.Drawing.Point(42, 144);
            this.panelInformation.Name = "panelInformation";
            this.panelInformation.Size = new System.Drawing.Size(646, 110);
            this.panelInformation.TabIndex = 4;
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
            this.btnAceptar.Location = new System.Drawing.Point(509, 260);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(179, 44);
            this.btnAceptar.TabIndex = 3;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // editDescription
            // 
            this.editDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editDescription.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editDescription.Location = new System.Drawing.Point(42, 82);
            this.editDescription.Multiline = true;
            this.editDescription.Name = "editDescription";
            this.editDescription.Size = new System.Drawing.Size(646, 56);
            this.editDescription.TabIndex = 2;
            this.editDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editDescription_KeyPress);
            // 
            // textDescription
            // 
            this.textDescription.AutoSize = true;
            this.textDescription.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textDescription.Location = new System.Drawing.Point(39, 63);
            this.textDescription.Name = "textDescription";
            this.textDescription.Size = new System.Drawing.Size(77, 15);
            this.textDescription.TabIndex = 2;
            this.textDescription.Text = "Descripción";
            // 
            // editConcept
            // 
            this.editConcept.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editConcept.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editConcept.Location = new System.Drawing.Point(146, 21);
            this.editConcept.Name = "editConcept";
            this.editConcept.Size = new System.Drawing.Size(542, 22);
            this.editConcept.TabIndex = 1;
            this.editConcept.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editConcept_KeyPress);
            // 
            // textConcept
            // 
            this.textConcept.AutoSize = true;
            this.textConcept.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textConcept.Location = new System.Drawing.Point(39, 24);
            this.textConcept.Name = "textConcept";
            this.textConcept.Size = new System.Drawing.Size(62, 15);
            this.textConcept.TabIndex = 0;
            this.textConcept.Text = "Concepto";
            // 
            // editTotales
            // 
            this.editTotales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editTotales.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editTotales.Location = new System.Drawing.Point(3, 3);
            this.editTotales.Multiline = true;
            this.editTotales.Name = "editTotales";
            this.editTotales.Size = new System.Drawing.Size(640, 104);
            this.editTotales.TabIndex = 0;
            this.editTotales.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FrmTerminateWithdrawal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(737, 409);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelToolbar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmTerminateWithdrawal";
            this.Text = "Terminar Retiro";
            this.Load += new System.EventHandler(this.FrmTerminateWithdrawal_Load);
            this.panelToolbar.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            this.panelInformation.ResumeLayout(false);
            this.panelInformation.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox editConcept;
        private System.Windows.Forms.Label textConcept;
        private System.Windows.Forms.TextBox editDescription;
        private System.Windows.Forms.Label textDescription;
        private System.Windows.Forms.Panel panelInformation;
        private RoundedButton btnAceptar;
        private System.Windows.Forms.TextBox editTotales;
    }
}