namespace SyncTPV.Views.Reports
{
    partial class FormParametersReports
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormParametersReports));
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.tableLayoutPanelParametrosReportes = new System.Windows.Forms.TableLayoutPanel();
            this.panelTable1 = new System.Windows.Forms.Panel();
            this.checkBoxIncluirPendientes = new System.Windows.Forms.CheckBox();
            this.checkBoxTodasLasRutas = new System.Windows.Forms.CheckBox();
            this.panelTable2 = new System.Windows.Forms.Panel();
            this.checkBoxDetallesDevoluciones = new System.Windows.Forms.CheckBox();
            this.checkBoxFormasCobroDevoluciones = new System.Windows.Forms.CheckBox();
            this.checkBoxDevoluciones = new System.Windows.Forms.CheckBox();
            this.checkBoxPagos = new System.Windows.Forms.CheckBox();
            this.checkBoxRetiros = new System.Windows.Forms.CheckBox();
            this.checkBoxIngresos = new System.Windows.Forms.CheckBox();
            this.checkBoxCreditos = new System.Windows.Forms.CheckBox();
            this.checkBoxDocs = new System.Windows.Forms.CheckBox();
            this.btnGeneratePdf = new System.Windows.Forms.Button();
            this.panelToolbar.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.tableLayoutPanelParametrosReportes.SuspendLayout();
            this.panelTable1.SuspendLayout();
            this.panelTable2.SuspendLayout();
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
            this.panelToolbar.Size = new System.Drawing.Size(371, 68);
            this.panelToolbar.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.Location = new System.Drawing.Point(3, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 62);
            this.btnClose.TabIndex = 1;
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
            this.panelContent.Controls.Add(this.tableLayoutPanelParametrosReportes);
            this.panelContent.Controls.Add(this.btnGeneratePdf);
            this.panelContent.Location = new System.Drawing.Point(0, 71);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(371, 296);
            this.panelContent.TabIndex = 1;
            // 
            // tableLayoutPanelParametrosReportes
            // 
            this.tableLayoutPanelParametrosReportes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelParametrosReportes.ColumnCount = 1;
            this.tableLayoutPanelParametrosReportes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelParametrosReportes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelParametrosReportes.Controls.Add(this.panelTable1, 0, 0);
            this.tableLayoutPanelParametrosReportes.Controls.Add(this.panelTable2, 0, 1);
            this.tableLayoutPanelParametrosReportes.Location = new System.Drawing.Point(12, 3);
            this.tableLayoutPanelParametrosReportes.Name = "tableLayoutPanelParametrosReportes";
            this.tableLayoutPanelParametrosReportes.RowCount = 2;
            this.tableLayoutPanelParametrosReportes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelParametrosReportes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelParametrosReportes.Size = new System.Drawing.Size(346, 236);
            this.tableLayoutPanelParametrosReportes.TabIndex = 5;
            // 
            // panelTable1
            // 
            this.panelTable1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTable1.Controls.Add(this.checkBoxIncluirPendientes);
            this.panelTable1.Controls.Add(this.checkBoxTodasLasRutas);
            this.panelTable1.Location = new System.Drawing.Point(3, 3);
            this.panelTable1.Name = "panelTable1";
            this.panelTable1.Size = new System.Drawing.Size(340, 112);
            this.panelTable1.TabIndex = 0;
            // 
            // checkBoxIncluirPendientes
            // 
            this.checkBoxIncluirPendientes.AutoSize = true;
            this.checkBoxIncluirPendientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxIncluirPendientes.Location = new System.Drawing.Point(3, 16);
            this.checkBoxIncluirPendientes.Name = "checkBoxIncluirPendientes";
            this.checkBoxIncluirPendientes.Size = new System.Drawing.Size(271, 24);
            this.checkBoxIncluirPendientes.TabIndex = 6;
            this.checkBoxIncluirPendientes.Text = "Incluir Pendientes o Pausados";
            this.checkBoxIncluirPendientes.UseVisualStyleBackColor = true;
            this.checkBoxIncluirPendientes.CheckedChanged += new System.EventHandler(this.checkBoxIncluirPendientes_CheckedChanged);
            // 
            // checkBoxTodasLasRutas
            // 
            this.checkBoxTodasLasRutas.AutoSize = true;
            this.checkBoxTodasLasRutas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxTodasLasRutas.Location = new System.Drawing.Point(3, 45);
            this.checkBoxTodasLasRutas.Name = "checkBoxTodasLasRutas";
            this.checkBoxTodasLasRutas.Size = new System.Drawing.Size(158, 24);
            this.checkBoxTodasLasRutas.TabIndex = 5;
            this.checkBoxTodasLasRutas.Text = "Todas las Rutas";
            this.checkBoxTodasLasRutas.UseVisualStyleBackColor = true;
            this.checkBoxTodasLasRutas.CheckedChanged += new System.EventHandler(this.checkBoxTodasLasRutas_CheckedChanged);
            // 
            // panelTable2
            // 
            this.panelTable2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTable2.Controls.Add(this.checkBoxDetallesDevoluciones);
            this.panelTable2.Controls.Add(this.checkBoxFormasCobroDevoluciones);
            this.panelTable2.Controls.Add(this.checkBoxDevoluciones);
            this.panelTable2.Controls.Add(this.checkBoxPagos);
            this.panelTable2.Controls.Add(this.checkBoxRetiros);
            this.panelTable2.Controls.Add(this.checkBoxIngresos);
            this.panelTable2.Controls.Add(this.checkBoxCreditos);
            this.panelTable2.Controls.Add(this.checkBoxDocs);
            this.panelTable2.Location = new System.Drawing.Point(3, 121);
            this.panelTable2.Name = "panelTable2";
            this.panelTable2.Size = new System.Drawing.Size(340, 112);
            this.panelTable2.TabIndex = 1;
            // 
            // checkBoxDetallesDevoluciones
            // 
            this.checkBoxDetallesDevoluciones.AutoSize = true;
            this.checkBoxDetallesDevoluciones.Location = new System.Drawing.Point(182, 67);
            this.checkBoxDetallesDevoluciones.Name = "checkBoxDetallesDevoluciones";
            this.checkBoxDetallesDevoluciones.Size = new System.Drawing.Size(130, 17);
            this.checkBoxDetallesDevoluciones.TabIndex = 7;
            this.checkBoxDetallesDevoluciones.Text = "Detalles devoluciones";
            this.checkBoxDetallesDevoluciones.UseVisualStyleBackColor = true;
            this.checkBoxDetallesDevoluciones.Visible = false;
            this.checkBoxDetallesDevoluciones.CheckedChanged += new System.EventHandler(this.checkBoxDetallesDevoluciones_CheckedChanged);
            // 
            // checkBoxFormasCobroDevoluciones
            // 
            this.checkBoxFormasCobroDevoluciones.AutoSize = true;
            this.checkBoxFormasCobroDevoluciones.Location = new System.Drawing.Point(182, 90);
            this.checkBoxFormasCobroDevoluciones.Name = "checkBoxFormasCobroDevoluciones";
            this.checkBoxFormasCobroDevoluciones.Size = new System.Drawing.Size(156, 17);
            this.checkBoxFormasCobroDevoluciones.TabIndex = 6;
            this.checkBoxFormasCobroDevoluciones.Text = "Formas cobro devoluciones";
            this.checkBoxFormasCobroDevoluciones.UseVisualStyleBackColor = true;
            this.checkBoxFormasCobroDevoluciones.Visible = false;
            this.checkBoxFormasCobroDevoluciones.CheckedChanged += new System.EventHandler(this.checkBoxFormasCobroDevoluciones_CheckedChanged);
            // 
            // checkBoxDevoluciones
            // 
            this.checkBoxDevoluciones.AutoSize = true;
            this.checkBoxDevoluciones.Location = new System.Drawing.Point(182, 44);
            this.checkBoxDevoluciones.Name = "checkBoxDevoluciones";
            this.checkBoxDevoluciones.Size = new System.Drawing.Size(120, 17);
            this.checkBoxDevoluciones.TabIndex = 5;
            this.checkBoxDevoluciones.Text = "Incluir devoluciones";
            this.checkBoxDevoluciones.UseVisualStyleBackColor = true;
            this.checkBoxDevoluciones.CheckedChanged += new System.EventHandler(this.checkBoxDevoluciones_CheckedChanged);
            // 
            // checkBoxPagos
            // 
            this.checkBoxPagos.AutoSize = true;
            this.checkBoxPagos.Location = new System.Drawing.Point(19, 67);
            this.checkBoxPagos.Name = "checkBoxPagos";
            this.checkBoxPagos.Size = new System.Drawing.Size(97, 17);
            this.checkBoxPagos.TabIndex = 4;
            this.checkBoxPagos.Text = "Detalles Pagos";
            this.checkBoxPagos.UseVisualStyleBackColor = true;
            this.checkBoxPagos.CheckedChanged += new System.EventHandler(this.checkBoxPagos_CheckedChanged);
            // 
            // checkBoxRetiros
            // 
            this.checkBoxRetiros.AutoSize = true;
            this.checkBoxRetiros.Location = new System.Drawing.Point(19, 90);
            this.checkBoxRetiros.Name = "checkBoxRetiros";
            this.checkBoxRetiros.Size = new System.Drawing.Size(100, 17);
            this.checkBoxRetiros.TabIndex = 3;
            this.checkBoxRetiros.Text = "Detalles Retiros";
            this.checkBoxRetiros.UseVisualStyleBackColor = true;
            this.checkBoxRetiros.CheckedChanged += new System.EventHandler(this.checkBoxRetiros_CheckedChanged);
            // 
            // checkBoxIngresos
            // 
            this.checkBoxIngresos.AutoSize = true;
            this.checkBoxIngresos.Location = new System.Drawing.Point(182, 21);
            this.checkBoxIngresos.Name = "checkBoxIngresos";
            this.checkBoxIngresos.Size = new System.Drawing.Size(107, 17);
            this.checkBoxIngresos.TabIndex = 2;
            this.checkBoxIngresos.Text = "Detalles Ingresos";
            this.checkBoxIngresos.UseVisualStyleBackColor = true;
            this.checkBoxIngresos.CheckedChanged += new System.EventHandler(this.checkBoxIngresos_CheckedChanged);
            // 
            // checkBoxCreditos
            // 
            this.checkBoxCreditos.AutoSize = true;
            this.checkBoxCreditos.Location = new System.Drawing.Point(19, 44);
            this.checkBoxCreditos.Name = "checkBoxCreditos";
            this.checkBoxCreditos.Size = new System.Drawing.Size(105, 17);
            this.checkBoxCreditos.TabIndex = 1;
            this.checkBoxCreditos.Text = "Detalles Créditos";
            this.checkBoxCreditos.UseVisualStyleBackColor = true;
            this.checkBoxCreditos.CheckedChanged += new System.EventHandler(this.checkBoxCreditos_CheckedChanged);
            // 
            // checkBoxDocs
            // 
            this.checkBoxDocs.AutoSize = true;
            this.checkBoxDocs.Location = new System.Drawing.Point(19, 21);
            this.checkBoxDocs.Name = "checkBoxDocs";
            this.checkBoxDocs.Size = new System.Drawing.Size(127, 17);
            this.checkBoxDocs.TabIndex = 0;
            this.checkBoxDocs.Text = "Detalles Documentos";
            this.checkBoxDocs.UseVisualStyleBackColor = true;
            this.checkBoxDocs.CheckedChanged += new System.EventHandler(this.checkBoxDocs_CheckedChanged);
            // 
            // btnGeneratePdf
            // 
            this.btnGeneratePdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGeneratePdf.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnGeneratePdf.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnGeneratePdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGeneratePdf.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGeneratePdf.Location = new System.Drawing.Point(219, 245);
            this.btnGeneratePdf.Name = "btnGeneratePdf";
            this.btnGeneratePdf.Size = new System.Drawing.Size(149, 48);
            this.btnGeneratePdf.TabIndex = 2;
            this.btnGeneratePdf.Text = "Generar Reporte";
            this.btnGeneratePdf.UseVisualStyleBackColor = true;
            this.btnGeneratePdf.Click += new System.EventHandler(this.btnGeneratePdf_Click);
            // 
            // FormParametersReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(370, 367);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelToolbar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(386, 406);
            this.Name = "FormParametersReports";
            this.Text = "Reportes";
            this.Load += new System.EventHandler(this.FormParametersReports_Load);
            this.panelToolbar.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.tableLayoutPanelParametrosReportes.ResumeLayout(false);
            this.panelTable1.ResumeLayout(false);
            this.panelTable1.PerformLayout();
            this.panelTable2.ResumeLayout(false);
            this.panelTable2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Button btnGeneratePdf;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelParametrosReportes;
        private System.Windows.Forms.Panel panelTable1;
        private System.Windows.Forms.CheckBox checkBoxIncluirPendientes;
        private System.Windows.Forms.CheckBox checkBoxTodasLasRutas;
        private System.Windows.Forms.Panel panelTable2;
        private System.Windows.Forms.CheckBox checkBoxRetiros;
        private System.Windows.Forms.CheckBox checkBoxIngresos;
        private System.Windows.Forms.CheckBox checkBoxCreditos;
        private System.Windows.Forms.CheckBox checkBoxDocs;
        private System.Windows.Forms.CheckBox checkBoxPagos;
        private System.Windows.Forms.CheckBox checkBoxDevoluciones;
        private System.Windows.Forms.CheckBox checkBoxDetallesDevoluciones;
        private System.Windows.Forms.CheckBox checkBoxFormasCobroDevoluciones;
    }
}