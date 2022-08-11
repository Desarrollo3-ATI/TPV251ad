namespace SyncTPV.Views.Cxc
{
    partial class FormPagosCxc
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPagosCxc));
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.imgSinDatos = new System.Windows.Forms.PictureBox();
            this.dataGridViewPagos = new System.Windows.Forms.DataGridView();
            this.idPagoDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fcPagoDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountPagoDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.referPagoDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaDgvPagosCxc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusPagoDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textTotalPagos = new System.Windows.Forms.Label();
            this.panelToolbar.SuspendLayout();
            this.panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPagos)).BeginInit();
            this.SuspendLayout();
            // 
            // panelToolbar
            // 
            this.panelToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelToolbar.BackColor = System.Drawing.Color.Coral;
            this.panelToolbar.Controls.Add(this.btnClose);
            this.panelToolbar.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelToolbar.Location = new System.Drawing.Point(-2, -2);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(804, 71);
            this.panelToolbar.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Roboto Black", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(3, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 65);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Cerrar";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnClose_KeyUp);
            // 
            // panelContent
            // 
            this.panelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContent.Controls.Add(this.imgSinDatos);
            this.panelContent.Controls.Add(this.dataGridViewPagos);
            this.panelContent.Controls.Add(this.textTotalPagos);
            this.panelContent.Location = new System.Drawing.Point(-2, 66);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(804, 385);
            this.panelContent.TabIndex = 1;
            // 
            // imgSinDatos
            // 
            this.imgSinDatos.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.imgSinDatos.Location = new System.Drawing.Point(269, 143);
            this.imgSinDatos.Name = "imgSinDatos";
            this.imgSinDatos.Size = new System.Drawing.Size(244, 181);
            this.imgSinDatos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgSinDatos.TabIndex = 2;
            this.imgSinDatos.TabStop = false;
            // 
            // dataGridViewPagos
            // 
            this.dataGridViewPagos.AllowUserToAddRows = false;
            this.dataGridViewPagos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewPagos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewPagos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewPagos.BackgroundColor = System.Drawing.Color.FloralWhite;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Coral;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(1);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewPagos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPagos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idPagoDgv,
            this.fcPagoDgv,
            this.amountPagoDgv,
            this.referPagoDgv,
            this.fechaDgvPagosCxc,
            this.statusPagoDgv});
            this.dataGridViewPagos.GridColor = System.Drawing.Color.FloralWhite;
            this.dataGridViewPagos.Location = new System.Drawing.Point(17, 33);
            this.dataGridViewPagos.Name = "dataGridViewPagos";
            this.dataGridViewPagos.RowHeadersVisible = false;
            this.dataGridViewPagos.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewPagos.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(1, 5, 1, 5);
            this.dataGridViewPagos.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewPagos.Size = new System.Drawing.Size(773, 339);
            this.dataGridViewPagos.TabIndex = 1;
            this.dataGridViewPagos.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridViewPagos_Scroll);
            this.dataGridViewPagos.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridViewPagos_KeyUp);
            // 
            // idPagoDgv
            // 
            this.idPagoDgv.HeaderText = "Id";
            this.idPagoDgv.Name = "idPagoDgv";
            // 
            // fcPagoDgv
            // 
            this.fcPagoDgv.HeaderText = "Forma de Pago";
            this.fcPagoDgv.Name = "fcPagoDgv";
            // 
            // amountPagoDgv
            // 
            this.amountPagoDgv.HeaderText = "Importe";
            this.amountPagoDgv.Name = "amountPagoDgv";
            // 
            // referPagoDgv
            // 
            this.referPagoDgv.HeaderText = "Referencia";
            this.referPagoDgv.Name = "referPagoDgv";
            // 
            // fechaDgvPagosCxc
            // 
            this.fechaDgvPagosCxc.HeaderText = "Fecha";
            this.fechaDgvPagosCxc.Name = "fechaDgvPagosCxc";
            // 
            // statusPagoDgv
            // 
            this.statusPagoDgv.HeaderText = "Estatus";
            this.statusPagoDgv.Name = "statusPagoDgv";
            // 
            // textTotalPagos
            // 
            this.textTotalPagos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textTotalPagos.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTotalPagos.Location = new System.Drawing.Point(14, 6);
            this.textTotalPagos.Name = "textTotalPagos";
            this.textTotalPagos.Size = new System.Drawing.Size(776, 24);
            this.textTotalPagos.TabIndex = 0;
            this.textTotalPagos.Text = "Pagos";
            this.textTotalPagos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormPagosCxc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelToolbar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormPagosCxc";
            this.Text = "Abonos del Documento a crédito";
            this.Load += new System.EventHandler(this.FormPagosCxc_Load);
            this.panelToolbar.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPagos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label textTotalPagos;
        private System.Windows.Forms.DataGridView dataGridViewPagos;
        private System.Windows.Forms.PictureBox imgSinDatos;
        private System.Windows.Forms.DataGridViewTextBoxColumn idPagoDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn fcPagoDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountPagoDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn referPagoDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaDgvPagosCxc;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusPagoDgv;
    }
}