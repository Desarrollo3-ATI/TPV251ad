
namespace SyncTPV.Views
{
    partial class FrmAddAbono
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddAbono));
            this.panelRealizarAbono = new System.Windows.Forms.Panel();
            this.textNombreFormaDeCobro = new System.Windows.Forms.Label();
            this.btnAbonarFrmAddAbono = new SyncTPV.RoundedButton();
            this.textFcFrmAddAbono = new System.Windows.Forms.Label();
            this.editImporteFrmAddAbono = new System.Windows.Forms.TextBox();
            this.textCurrencyFrmAddAbono = new System.Windows.Forms.Label();
            this.textInfoAbonoFrmAddAbono = new System.Windows.Forms.Label();
            this.panelRealizarAbono.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelRealizarAbono
            // 
            this.panelRealizarAbono.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRealizarAbono.AutoScroll = true;
            this.panelRealizarAbono.Controls.Add(this.textNombreFormaDeCobro);
            this.panelRealizarAbono.Controls.Add(this.btnAbonarFrmAddAbono);
            this.panelRealizarAbono.Controls.Add(this.textFcFrmAddAbono);
            this.panelRealizarAbono.Controls.Add(this.editImporteFrmAddAbono);
            this.panelRealizarAbono.Controls.Add(this.textCurrencyFrmAddAbono);
            this.panelRealizarAbono.Controls.Add(this.textInfoAbonoFrmAddAbono);
            this.panelRealizarAbono.Location = new System.Drawing.Point(12, 12);
            this.panelRealizarAbono.Name = "panelRealizarAbono";
            this.panelRealizarAbono.Size = new System.Drawing.Size(419, 150);
            this.panelRealizarAbono.TabIndex = 0;
            // 
            // textNombreFormaDeCobro
            // 
            this.textNombreFormaDeCobro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textNombreFormaDeCobro.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.textNombreFormaDeCobro.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textNombreFormaDeCobro.ForeColor = System.Drawing.Color.DodgerBlue;
            this.textNombreFormaDeCobro.Location = new System.Drawing.Point(22, 9);
            this.textNombreFormaDeCobro.Name = "textNombreFormaDeCobro";
            this.textNombreFormaDeCobro.Size = new System.Drawing.Size(377, 40);
            this.textNombreFormaDeCobro.TabIndex = 11;
            this.textNombreFormaDeCobro.Text = "Forma de Cobro";
            this.textNombreFormaDeCobro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnAbonarFrmAddAbono
            // 
            this.btnAbonarFrmAddAbono.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbonarFrmAddAbono.BackColor = System.Drawing.Color.Transparent;
            this.btnAbonarFrmAddAbono.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAbonarFrmAddAbono.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnAbonarFrmAddAbono.FlatAppearance.BorderSize = 2;
            this.btnAbonarFrmAddAbono.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnAbonarFrmAddAbono.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbonarFrmAddAbono.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbonarFrmAddAbono.Location = new System.Drawing.Point(208, 107);
            this.btnAbonarFrmAddAbono.Name = "btnAbonarFrmAddAbono";
            this.btnAbonarFrmAddAbono.Size = new System.Drawing.Size(191, 40);
            this.btnAbonarFrmAddAbono.TabIndex = 10;
            this.btnAbonarFrmAddAbono.Text = "Abonar";
            this.btnAbonarFrmAddAbono.UseVisualStyleBackColor = false;
            this.btnAbonarFrmAddAbono.Click += new System.EventHandler(this.btnAbonarFrmAddAbono_Click_1);
            // 
            // textFcFrmAddAbono
            // 
            this.textFcFrmAddAbono.AutoSize = true;
            this.textFcFrmAddAbono.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textFcFrmAddAbono.Location = new System.Drawing.Point(266, 59);
            this.textFcFrmAddAbono.Name = "textFcFrmAddAbono";
            this.textFcFrmAddAbono.Size = new System.Drawing.Size(0, 17);
            this.textFcFrmAddAbono.TabIndex = 9;
            // 
            // editImporteFrmAddAbono
            // 
            this.editImporteFrmAddAbono.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editImporteFrmAddAbono.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editImporteFrmAddAbono.ForeColor = System.Drawing.Color.Green;
            this.editImporteFrmAddAbono.Location = new System.Drawing.Point(101, 62);
            this.editImporteFrmAddAbono.Name = "editImporteFrmAddAbono";
            this.editImporteFrmAddAbono.Size = new System.Drawing.Size(251, 22);
            this.editImporteFrmAddAbono.TabIndex = 8;
            this.editImporteFrmAddAbono.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.editImporteFrmAddAbono.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editImporteFrmAddAbono_KeyPress_1);
            // 
            // textCurrencyFrmAddAbono
            // 
            this.textCurrencyFrmAddAbono.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textCurrencyFrmAddAbono.AutoSize = true;
            this.textCurrencyFrmAddAbono.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textCurrencyFrmAddAbono.Location = new System.Drawing.Point(358, 65);
            this.textCurrencyFrmAddAbono.Name = "textCurrencyFrmAddAbono";
            this.textCurrencyFrmAddAbono.Size = new System.Drawing.Size(39, 16);
            this.textCurrencyFrmAddAbono.TabIndex = 7;
            this.textCurrencyFrmAddAbono.Text = "MXN";
            // 
            // textInfoAbonoFrmAddAbono
            // 
            this.textInfoAbonoFrmAddAbono.AutoSize = true;
            this.textInfoAbonoFrmAddAbono.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInfoAbonoFrmAddAbono.Location = new System.Drawing.Point(19, 65);
            this.textInfoAbonoFrmAddAbono.Name = "textInfoAbonoFrmAddAbono";
            this.textInfoAbonoFrmAddAbono.Size = new System.Drawing.Size(71, 16);
            this.textInfoAbonoFrmAddAbono.TabIndex = 6;
            this.textInfoAbonoFrmAddAbono.Text = "Importe $";
            // 
            // FrmAddAbono
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(443, 174);
            this.Controls.Add(this.panelRealizarAbono);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmAddAbono";
            this.Text = "Realizar Abono";
            this.Load += new System.EventHandler(this.FrmAddAbono_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmAddAbono_KeyPress);
            this.panelRealizarAbono.ResumeLayout(false);
            this.panelRealizarAbono.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelRealizarAbono;
        private System.Windows.Forms.Label textNombreFormaDeCobro;
        private System.Windows.Forms.Label textFcFrmAddAbono;
        private System.Windows.Forms.TextBox editImporteFrmAddAbono;
        private System.Windows.Forms.Label textCurrencyFrmAddAbono;
        private System.Windows.Forms.Label textInfoAbonoFrmAddAbono;
        private RoundedButton btnAbonarFrmAddAbono;
    }
}