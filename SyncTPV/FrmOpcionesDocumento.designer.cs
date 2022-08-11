namespace SyncTPV
{
    partial class FrmOpcionesDocumento
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
            this.btnVerMovimientos = new System.Windows.Forms.Button();
            this.btnModificarDocumento = new System.Windows.Forms.Button();
            this.btnPrintTicket = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancelarDocumentFrmOpcionesDoc = new System.Windows.Forms.Button();
            this.panelContent.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnVerMovimientos
            // 
            this.btnVerMovimientos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVerMovimientos.BackColor = System.Drawing.Color.White;
            this.btnVerMovimientos.FlatAppearance.BorderColor = System.Drawing.Color.SkyBlue;
            this.btnVerMovimientos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerMovimientos.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerMovimientos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVerMovimientos.Location = new System.Drawing.Point(24, 68);
            this.btnVerMovimientos.Name = "btnVerMovimientos";
            this.btnVerMovimientos.Size = new System.Drawing.Size(270, 36);
            this.btnVerMovimientos.TabIndex = 0;
            this.btnVerMovimientos.Text = "Ver Movimientos";
            this.btnVerMovimientos.UseVisualStyleBackColor = false;
            this.btnVerMovimientos.Click += new System.EventHandler(this.ver_Click);
            // 
            // btnModificarDocumento
            // 
            this.btnModificarDocumento.BackColor = System.Drawing.Color.White;
            this.btnModificarDocumento.FlatAppearance.BorderColor = System.Drawing.Color.Orange;
            this.btnModificarDocumento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificarDocumento.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificarDocumento.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModificarDocumento.Location = new System.Drawing.Point(24, 152);
            this.btnModificarDocumento.Name = "btnModificarDocumento";
            this.btnModificarDocumento.Size = new System.Drawing.Size(270, 36);
            this.btnModificarDocumento.TabIndex = 2;
            this.btnModificarDocumento.Text = "Modificar Documento";
            this.btnModificarDocumento.UseVisualStyleBackColor = false;
            this.btnModificarDocumento.Click += new System.EventHandler(this.modificar_Click);
            // 
            // btnPrintTicket
            // 
            this.btnPrintTicket.BackColor = System.Drawing.Color.White;
            this.btnPrintTicket.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btnPrintTicket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintTicket.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintTicket.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintTicket.Location = new System.Drawing.Point(24, 110);
            this.btnPrintTicket.Name = "btnPrintTicket";
            this.btnPrintTicket.Size = new System.Drawing.Size(270, 36);
            this.btnPrintTicket.TabIndex = 17;
            this.btnPrintTicket.Text = "Imprimir Ticket";
            this.btnPrintTicket.UseVisualStyleBackColor = false;
            this.btnPrintTicket.Click += new System.EventHandler(this.button1_Click);
            // 
            // panelContent
            // 
            this.panelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContent.BackColor = System.Drawing.Color.OldLace;
            this.panelContent.Controls.Add(this.groupBox1);
            this.panelContent.Location = new System.Drawing.Point(0, 0);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(347, 243);
            this.panelContent.TabIndex = 20;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCancelarDocumentFrmOpcionesDoc);
            this.groupBox1.Controls.Add(this.btnModificarDocumento);
            this.groupBox1.Controls.Add(this.btnPrintTicket);
            this.groupBox1.Controls.Add(this.btnVerMovimientos);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(322, 215);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            // 
            // btnCancelarDocumentFrmOpcionesDoc
            // 
            this.btnCancelarDocumentFrmOpcionesDoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelarDocumentFrmOpcionesDoc.BackColor = System.Drawing.Color.White;
            this.btnCancelarDocumentFrmOpcionesDoc.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnCancelarDocumentFrmOpcionesDoc.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnCancelarDocumentFrmOpcionesDoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarDocumentFrmOpcionesDoc.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarDocumentFrmOpcionesDoc.Image = global::SyncTPV.Properties.Resources.cancel_black;
            this.btnCancelarDocumentFrmOpcionesDoc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelarDocumentFrmOpcionesDoc.Location = new System.Drawing.Point(24, 26);
            this.btnCancelarDocumentFrmOpcionesDoc.Name = "btnCancelarDocumentFrmOpcionesDoc";
            this.btnCancelarDocumentFrmOpcionesDoc.Size = new System.Drawing.Size(270, 36);
            this.btnCancelarDocumentFrmOpcionesDoc.TabIndex = 1;
            this.btnCancelarDocumentFrmOpcionesDoc.Text = "Cancelar Documento";
            this.btnCancelarDocumentFrmOpcionesDoc.UseVisualStyleBackColor = false;
            this.btnCancelarDocumentFrmOpcionesDoc.Click += new System.EventHandler(this.btnCancelarDocument_Click);
            // 
            // FrmOpcionesDocumento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(347, 243);
            this.Controls.Add(this.panelContent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmOpcionesDocumento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Opciones del documento";
            this.Load += new System.EventHandler(this.FrmOpcionDoc_Load);
            this.panelContent.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnVerMovimientos;
        private System.Windows.Forms.Button btnCancelarDocumentFrmOpcionesDoc;
        private System.Windows.Forms.Button btnModificarDocumento;
        private System.Windows.Forms.Button btnPrintTicket;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}