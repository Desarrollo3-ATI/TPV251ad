namespace SyncTPV
{
    partial class FrmActivacion
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
            this.txtActivar = new System.Windows.Forms.TextBox();
            this.btnActivar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.editSiteCodeFrmActivacion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtActivar
            // 
            this.txtActivar.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtActivar.Location = new System.Drawing.Point(131, 80);
            this.txtActivar.Name = "txtActivar";
            this.txtActivar.Size = new System.Drawing.Size(286, 26);
            this.txtActivar.TabIndex = 18;
            this.txtActivar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtActivar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtActivar_KeyPress);
            // 
            // btnActivar
            // 
            this.btnActivar.BackColor = System.Drawing.Color.Transparent;
            this.btnActivar.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnActivar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActivar.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActivar.ForeColor = System.Drawing.Color.Black;
            this.btnActivar.Location = new System.Drawing.Point(150, 132);
            this.btnActivar.Name = "btnActivar";
            this.btnActivar.Size = new System.Drawing.Size(132, 31);
            this.btnActivar.TabIndex = 19;
            this.btnActivar.Text = "Activar";
            this.btnActivar.UseVisualStyleBackColor = false;
            this.btnActivar.Click += new System.EventHandler(this.btnActivar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 19);
            this.label2.TabIndex = 20;
            this.label2.Text = "Código de Sitio";
            // 
            // editSiteCodeFrmActivacion
            // 
            this.editSiteCodeFrmActivacion.BackColor = System.Drawing.Color.White;
            this.editSiteCodeFrmActivacion.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editSiteCodeFrmActivacion.Location = new System.Drawing.Point(131, 34);
            this.editSiteCodeFrmActivacion.Name = "editSiteCodeFrmActivacion";
            this.editSiteCodeFrmActivacion.ReadOnly = true;
            this.editSiteCodeFrmActivacion.Size = new System.Drawing.Size(286, 27);
            this.editSiteCodeFrmActivacion.TabIndex = 21;
            this.editSiteCodeFrmActivacion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(38, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 19);
            this.label3.TabIndex = 22;
            this.label3.Text = "Synckey";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnActivar);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtActivar);
            this.groupBox1.Controls.Add(this.editSiteCodeFrmActivacion);
            this.groupBox1.Font = new System.Drawing.Font("Roboto Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox1.Size = new System.Drawing.Size(434, 185);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "               Activación de Licencia";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(137, 218);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(301, 14);
            this.label1.TabIndex = 24;
            this.label1.Text = "Es importante conservar tu licencia en un lugar seguro";
            // 
            // FrmActivacion
            // 
            this.AcceptButton = this.btnActivar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(460, 246);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmActivacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Activación de licencia";
            this.Load += new System.EventHandler(this.FrmActivacion_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtActivar;
        private System.Windows.Forms.Button btnActivar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox editSiteCodeFrmActivacion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
    }
}