
namespace SyncTPV.Views.AperturaTurno
{
    partial class FrmAperturaTurno
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAperturaTurno));
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.btnIniciar = new SyncTPV.RoundedButton();
            this.textImporte = new System.Windows.Forms.Label();
            this.editImporte = new System.Windows.Forms.TextBox();
            this.textInformation = new System.Windows.Forms.Label();
            this.panelToolbar.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelToolbar
            // 
            this.panelToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelToolbar.BackColor = System.Drawing.Color.Coral;
            this.panelToolbar.Controls.Add(this.btnCerrar);
            this.panelToolbar.Location = new System.Drawing.Point(-1, 0);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(519, 74);
            this.panelToolbar.TabIndex = 0;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.FlatAppearance.BorderColor = System.Drawing.Color.OrangeRed;
            this.btnCerrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Image = global::SyncTPV.Properties.Resources.close;
            this.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCerrar.Location = new System.Drawing.Point(13, 3);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(75, 68);
            this.btnCerrar.TabIndex = 0;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // panelContent
            // 
            this.panelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContent.BackColor = System.Drawing.Color.Transparent;
            this.panelContent.Controls.Add(this.btnIniciar);
            this.panelContent.Controls.Add(this.textImporte);
            this.panelContent.Controls.Add(this.editImporte);
            this.panelContent.Controls.Add(this.textInformation);
            this.panelContent.Location = new System.Drawing.Point(12, 80);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(492, 270);
            this.panelContent.TabIndex = 1;
            // 
            // btnIniciar
            // 
            this.btnIniciar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIniciar.BackColor = System.Drawing.Color.Transparent;
            this.btnIniciar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIniciar.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnIniciar.FlatAppearance.BorderSize = 2;
            this.btnIniciar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnIniciar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIniciar.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIniciar.Location = new System.Drawing.Point(319, 214);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(154, 42);
            this.btnIniciar.TabIndex = 3;
            this.btnIniciar.Text = "Iniciar";
            this.btnIniciar.UseVisualStyleBackColor = false;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // textImporte
            // 
            this.textImporte.AutoSize = true;
            this.textImporte.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textImporte.Location = new System.Drawing.Point(55, 166);
            this.textImporte.Name = "textImporte";
            this.textImporte.Size = new System.Drawing.Size(92, 15);
            this.textImporte.TabIndex = 2;
            this.textImporte.Text = "Importe Inicial";
            // 
            // editImporte
            // 
            this.editImporte.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editImporte.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editImporte.Location = new System.Drawing.Point(169, 163);
            this.editImporte.Name = "editImporte";
            this.editImporte.Size = new System.Drawing.Size(249, 23);
            this.editImporte.TabIndex = 1;
            this.editImporte.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.editImporte.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editImporte_KeyPress);
            // 
            // textInformation
            // 
            this.textInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textInformation.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInformation.Location = new System.Drawing.Point(3, 0);
            this.textInformation.Name = "textInformation";
            this.textInformation.Size = new System.Drawing.Size(486, 87);
            this.textInformation.TabIndex = 0;
            this.textInformation.Text = "Este proceso eliminará todos los datos registrados en un turno anterior para inci" +
    "ar con saldo cero\r\n";
            this.textInformation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmAperturaTurno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(516, 362);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelToolbar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmAperturaTurno";
            this.Text = "Apertura de Turno";
            this.Load += new System.EventHandler(this.FrmAperturaTurno_Load);
            this.panelToolbar.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label textInformation;
        private System.Windows.Forms.Label textImporte;
        private System.Windows.Forms.TextBox editImporte;
        private RoundedButton btnIniciar;
    }
}