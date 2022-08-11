using SyncTPV.Helpers.Design;

namespace SyncTPV
{
    partial class FormPasswordConfirmation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPasswordConfirmation));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textMessageFrmPasswordConfirmation = new System.Windows.Forms.Label();
            this.btnCancelar = new SyncTPV.Helpers.Design.CancelRoundedButton();
            this.btnAceptar = new SyncTPV.RoundedButton();
            this.label3 = new System.Windows.Forms.Label();
            this.editPasswordValidacionDocumentos = new System.Windows.Forms.TextBox();
            this.imgAceptar = new System.Windows.Forms.PictureBox();
            this.imgCancelar = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgAceptar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCancelar)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.imgCancelar);
            this.groupBox1.Controls.Add(this.imgAceptar);
            this.groupBox1.Controls.Add(this.textMessageFrmPasswordConfirmation);
            this.groupBox1.Controls.Add(this.btnCancelar);
            this.groupBox1.Controls.Add(this.btnAceptar);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.editPasswordValidacionDocumentos);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(23, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(470, 154);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            // 
            // textMessageFrmPasswordConfirmation
            // 
            this.textMessageFrmPasswordConfirmation.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.textMessageFrmPasswordConfirmation.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textMessageFrmPasswordConfirmation.Location = new System.Drawing.Point(17, 20);
            this.textMessageFrmPasswordConfirmation.Name = "textMessageFrmPasswordConfirmation";
            this.textMessageFrmPasswordConfirmation.Size = new System.Drawing.Size(447, 50);
            this.textMessageFrmPasswordConfirmation.TabIndex = 51;
            this.textMessageFrmPasswordConfirmation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelar.BackColor = System.Drawing.Color.Transparent;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnCancelar.FlatAppearance.BorderSize = 2;
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Snow;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.Black;
            this.btnCancelar.Location = new System.Drawing.Point(6, 108);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(132, 40);
            this.btnCancelar.TabIndex = 50;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptar.BackColor = System.Drawing.Color.Transparent;
            this.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptar.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnAceptar.FlatAppearance.BorderSize = 2;
            this.btnAceptar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAceptar.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.Color.Black;
            this.btnAceptar.Location = new System.Drawing.Point(318, 108);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(132, 40);
            this.btnAceptar.TabIndex = 49;
            this.btnAceptar.Text = "Confirmar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Visible = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Roboto Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "Contraseña:";
            // 
            // editPasswordValidacionDocumentos
            // 
            this.editPasswordValidacionDocumentos.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editPasswordValidacionDocumentos.Location = new System.Drawing.Point(156, 74);
            this.editPasswordValidacionDocumentos.Name = "editPasswordValidacionDocumentos";
            this.editPasswordValidacionDocumentos.Size = new System.Drawing.Size(282, 22);
            this.editPasswordValidacionDocumentos.TabIndex = 1;
            this.editPasswordValidacionDocumentos.UseSystemPasswordChar = true;
            this.editPasswordValidacionDocumentos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtContraseña_KeyPress);
            this.editPasswordValidacionDocumentos.KeyUp += new System.Windows.Forms.KeyEventHandler(this.editPasswordValidacionDocumentos_KeyUp);
            // 
            // imgAceptar
            // 
            this.imgAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.imgAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgAceptar.Location = new System.Drawing.Point(318, 103);
            this.imgAceptar.Name = "imgAceptar";
            this.imgAceptar.Size = new System.Drawing.Size(146, 45);
            this.imgAceptar.TabIndex = 52;
            this.imgAceptar.TabStop = false;
            this.imgAceptar.Click += new System.EventHandler(this.imgAceptar_Click);
            // 
            // imgCancelar
            // 
            this.imgCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.imgCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgCancelar.Location = new System.Drawing.Point(6, 103);
            this.imgCancelar.Name = "imgCancelar";
            this.imgCancelar.Size = new System.Drawing.Size(146, 45);
            this.imgCancelar.TabIndex = 53;
            this.imgCancelar.TabStop = false;
            this.imgCancelar.Click += new System.EventHandler(this.imgCancelar_Click);
            // 
            // FormPasswordConfirmation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(505, 185);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormPasswordConfirmation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ingrese contraseña";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmPasswordConfirmation_FormClosed);
            this.Load += new System.EventHandler(this.FrmValidacionDocumentos_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgAceptar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCancelar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox editPasswordValidacionDocumentos;
        private System.Windows.Forms.Label textMessageFrmPasswordConfirmation;
        private RoundedButton btnAceptar;
        private CancelRoundedButton btnCancelar;
        private System.Windows.Forms.PictureBox imgAceptar;
        private System.Windows.Forms.PictureBox imgCancelar;
    }
}