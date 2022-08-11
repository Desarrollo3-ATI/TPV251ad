namespace SyncTPV.Views.Reports
{
    partial class FormFacturar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFacturar));
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSeleccionarRutaTerminal = new System.Windows.Forms.Button();
            this.editRutaTerminal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textRutaPdf = new System.Windows.Forms.Label();
            this.btnDownloadXml = new System.Windows.Forms.Button();
            this.btnDownloadPfd = new System.Windows.Forms.Button();
            this.btnVerXml = new System.Windows.Forms.Button();
            this.btnVerPdf = new System.Windows.Forms.Button();
            this.btnImprimirTicket = new System.Windows.Forms.Button();
            this.btnUbicacionArchivos = new System.Windows.Forms.Button();
            this.editXml = new System.Windows.Forms.TextBox();
            this.editPdf = new System.Windows.Forms.TextBox();
            this.panelToolbar.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelToolbar
            // 
            this.panelToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelToolbar.BackColor = System.Drawing.Color.Coral;
            this.panelToolbar.Controls.Add(this.btnClose);
            this.panelToolbar.Location = new System.Drawing.Point(0, 0);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(613, 70);
            this.panelToolbar.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(3, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 63);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Cerrar";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.label2);
            this.panelContent.Controls.Add(this.btnSeleccionarRutaTerminal);
            this.panelContent.Controls.Add(this.editRutaTerminal);
            this.panelContent.Controls.Add(this.label1);
            this.panelContent.Controls.Add(this.textRutaPdf);
            this.panelContent.Controls.Add(this.btnDownloadXml);
            this.panelContent.Controls.Add(this.btnDownloadPfd);
            this.panelContent.Controls.Add(this.btnVerXml);
            this.panelContent.Controls.Add(this.btnVerPdf);
            this.panelContent.Controls.Add(this.btnImprimirTicket);
            this.panelContent.Controls.Add(this.btnUbicacionArchivos);
            this.panelContent.Controls.Add(this.editXml);
            this.panelContent.Controls.Add(this.editPdf);
            this.panelContent.Location = new System.Drawing.Point(0, 72);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(613, 329);
            this.panelContent.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(63, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 14);
            this.label2.TabIndex = 12;
            this.label2.Text = "Ruta de almacenamiento Local";
            // 
            // btnSeleccionarRutaTerminal
            // 
            this.btnSeleccionarRutaTerminal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSeleccionarRutaTerminal.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnSeleccionarRutaTerminal.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSeleccionarRutaTerminal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeleccionarRutaTerminal.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeleccionarRutaTerminal.Location = new System.Drawing.Point(430, 34);
            this.btnSeleccionarRutaTerminal.Name = "btnSeleccionarRutaTerminal";
            this.btnSeleccionarRutaTerminal.Size = new System.Drawing.Size(102, 26);
            this.btnSeleccionarRutaTerminal.TabIndex = 11;
            this.btnSeleccionarRutaTerminal.Text = "Seleccionar";
            this.btnSeleccionarRutaTerminal.UseVisualStyleBackColor = true;
            this.btnSeleccionarRutaTerminal.Click += new System.EventHandler(this.btnSeleccionarRutaTerminal_Click);
            // 
            // editRutaTerminal
            // 
            this.editRutaTerminal.Location = new System.Drawing.Point(63, 34);
            this.editRutaTerminal.Name = "editRutaTerminal";
            this.editRutaTerminal.ReadOnly = true;
            this.editRutaTerminal.Size = new System.Drawing.Size(361, 20);
            this.editRutaTerminal.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(63, 180);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 14);
            this.label1.TabIndex = 9;
            this.label1.Text = "Archivo XML de la Factura";
            // 
            // textRutaPdf
            // 
            this.textRutaPdf.AutoSize = true;
            this.textRutaPdf.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textRutaPdf.Location = new System.Drawing.Point(63, 109);
            this.textRutaPdf.Name = "textRutaPdf";
            this.textRutaPdf.Size = new System.Drawing.Size(148, 14);
            this.textRutaPdf.TabIndex = 8;
            this.textRutaPdf.Text = "Archivo PDF de la Factura";
            // 
            // btnDownloadXml
            // 
            this.btnDownloadXml.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDownloadXml.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnDownloadXml.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnDownloadXml.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownloadXml.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownloadXml.Location = new System.Drawing.Point(420, 191);
            this.btnDownloadXml.Name = "btnDownloadXml";
            this.btnDownloadXml.Size = new System.Drawing.Size(102, 32);
            this.btnDownloadXml.TabIndex = 7;
            this.btnDownloadXml.Text = "Descargar";
            this.btnDownloadXml.UseVisualStyleBackColor = true;
            this.btnDownloadXml.Click += new System.EventHandler(this.btnDownloadXml_Click);
            // 
            // btnDownloadPfd
            // 
            this.btnDownloadPfd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDownloadPfd.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnDownloadPfd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnDownloadPfd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownloadPfd.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownloadPfd.Location = new System.Drawing.Point(420, 122);
            this.btnDownloadPfd.Name = "btnDownloadPfd";
            this.btnDownloadPfd.Size = new System.Drawing.Size(102, 32);
            this.btnDownloadPfd.TabIndex = 6;
            this.btnDownloadPfd.Text = "Descargar";
            this.btnDownloadPfd.UseVisualStyleBackColor = true;
            this.btnDownloadPfd.Click += new System.EventHandler(this.btnDownloadPfd_Click);
            // 
            // btnVerXml
            // 
            this.btnVerXml.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerXml.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnVerXml.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnVerXml.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerXml.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerXml.Location = new System.Drawing.Point(528, 191);
            this.btnVerXml.Name = "btnVerXml";
            this.btnVerXml.Size = new System.Drawing.Size(73, 32);
            this.btnVerXml.TabIndex = 5;
            this.btnVerXml.Text = "Ver";
            this.btnVerXml.UseVisualStyleBackColor = true;
            this.btnVerXml.Click += new System.EventHandler(this.btnVerXml_Click);
            // 
            // btnVerPdf
            // 
            this.btnVerPdf.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerPdf.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnVerPdf.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnVerPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerPdf.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerPdf.Location = new System.Drawing.Point(528, 122);
            this.btnVerPdf.Name = "btnVerPdf";
            this.btnVerPdf.Size = new System.Drawing.Size(73, 32);
            this.btnVerPdf.TabIndex = 4;
            this.btnVerPdf.Text = "Ver";
            this.btnVerPdf.UseVisualStyleBackColor = true;
            this.btnVerPdf.Click += new System.EventHandler(this.btnVerPdf_Click);
            // 
            // btnImprimirTicket
            // 
            this.btnImprimirTicket.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimirTicket.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnImprimirTicket.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnImprimirTicket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimirTicket.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimirTicket.Location = new System.Drawing.Point(438, 274);
            this.btnImprimirTicket.Name = "btnImprimirTicket";
            this.btnImprimirTicket.Size = new System.Drawing.Size(163, 42);
            this.btnImprimirTicket.TabIndex = 3;
            this.btnImprimirTicket.Text = "Imprimir Ticket de Factura";
            this.btnImprimirTicket.UseVisualStyleBackColor = true;
            this.btnImprimirTicket.Click += new System.EventHandler(this.btnImprimirTicket_Click);
            // 
            // btnUbicacionArchivos
            // 
            this.btnUbicacionArchivos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUbicacionArchivos.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnUbicacionArchivos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnUbicacionArchivos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUbicacionArchivos.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUbicacionArchivos.Location = new System.Drawing.Point(12, 274);
            this.btnUbicacionArchivos.Name = "btnUbicacionArchivos";
            this.btnUbicacionArchivos.Size = new System.Drawing.Size(165, 42);
            this.btnUbicacionArchivos.TabIndex = 2;
            this.btnUbicacionArchivos.Text = "Enviar";
            this.btnUbicacionArchivos.UseVisualStyleBackColor = true;
            this.btnUbicacionArchivos.Visible = false;
            // 
            // editXml
            // 
            this.editXml.Location = new System.Drawing.Point(63, 197);
            this.editXml.Name = "editXml";
            this.editXml.Size = new System.Drawing.Size(339, 20);
            this.editXml.TabIndex = 1;
            // 
            // editPdf
            // 
            this.editPdf.Location = new System.Drawing.Point(63, 128);
            this.editPdf.Name = "editPdf";
            this.editPdf.Size = new System.Drawing.Size(339, 20);
            this.editPdf.TabIndex = 0;
            // 
            // FormFacturar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(613, 400);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelToolbar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormFacturar";
            this.Text = "Facturación";
            this.Load += new System.EventHandler(this.FormFacturar_Load);
            this.panelToolbar.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Button btnImprimirTicket;
        private System.Windows.Forms.Button btnUbicacionArchivos;
        private System.Windows.Forms.TextBox editXml;
        private System.Windows.Forms.TextBox editPdf;
        private System.Windows.Forms.Button btnVerXml;
        private System.Windows.Forms.Button btnVerPdf;
        private System.Windows.Forms.Button btnDownloadXml;
        private System.Windows.Forms.Button btnDownloadPfd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label textRutaPdf;
        private System.Windows.Forms.TextBox editRutaTerminal;
        private System.Windows.Forms.Button btnSeleccionarRutaTerminal;
        private System.Windows.Forms.Label label2;
    }
}