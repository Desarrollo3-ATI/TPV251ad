namespace SyncTPV.Views.Extras
{
    partial class FormSeleccionarCaja
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSeleccionarCaja));
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.imgSinDatos = new System.Windows.Forms.PictureBox();
            this.dataGridViewCajas = new System.Windows.Forms.DataGridView();
            this.idDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codeDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.warehouseIdDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdAtDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.editTotalCajas = new System.Windows.Forms.TextBox();
            this.editCodigoCajaPadre = new System.Windows.Forms.TextBox();
            this.panelToolbar.SuspendLayout();
            this.panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCajas)).BeginInit();
            this.SuspendLayout();
            // 
            // panelToolbar
            // 
            this.panelToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelToolbar.BackColor = System.Drawing.Color.Coral;
            this.panelToolbar.Controls.Add(this.editCodigoCajaPadre);
            this.panelToolbar.Controls.Add(this.btnClose);
            this.panelToolbar.Location = new System.Drawing.Point(0, 0);
            this.panelToolbar.Margin = new System.Windows.Forms.Padding(0);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(627, 73);
            this.panelToolbar.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.Location = new System.Drawing.Point(3, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 67);
            this.btnClose.TabIndex = 0;
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
            this.panelContent.Controls.Add(this.imgSinDatos);
            this.panelContent.Controls.Add(this.dataGridViewCajas);
            this.panelContent.Controls.Add(this.editTotalCajas);
            this.panelContent.Location = new System.Drawing.Point(0, 76);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(627, 286);
            this.panelContent.TabIndex = 1;
            // 
            // imgSinDatos
            // 
            this.imgSinDatos.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.imgSinDatos.Location = new System.Drawing.Point(202, 68);
            this.imgSinDatos.Name = "imgSinDatos";
            this.imgSinDatos.Size = new System.Drawing.Size(208, 156);
            this.imgSinDatos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgSinDatos.TabIndex = 2;
            this.imgSinDatos.TabStop = false;
            // 
            // dataGridViewCajas
            // 
            this.dataGridViewCajas.AllowUserToAddRows = false;
            this.dataGridViewCajas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewCajas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewCajas.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewCajas.BackgroundColor = System.Drawing.Color.FloralWhite;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCajas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewCajas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCajas.ColumnHeadersVisible = false;
            this.dataGridViewCajas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDgv,
            this.codeDgv,
            this.nameDgv,
            this.warehouseIdDgv,
            this.createdAtDgv});
            this.dataGridViewCajas.GridColor = System.Drawing.Color.FloralWhite;
            this.dataGridViewCajas.Location = new System.Drawing.Point(12, 25);
            this.dataGridViewCajas.MultiSelect = false;
            this.dataGridViewCajas.Name = "dataGridViewCajas";
            this.dataGridViewCajas.ReadOnly = true;
            this.dataGridViewCajas.RowHeadersVisible = false;
            this.dataGridViewCajas.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewCajas.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewCajas.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCajas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCajas.Size = new System.Drawing.Size(602, 249);
            this.dataGridViewCajas.TabIndex = 1;
            this.dataGridViewCajas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCajas_CellClick);
            this.dataGridViewCajas.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridViewCajas_Scroll);
            // 
            // idDgv
            // 
            this.idDgv.HeaderText = "Id";
            this.idDgv.Name = "idDgv";
            this.idDgv.ReadOnly = true;
            // 
            // codeDgv
            // 
            this.codeDgv.HeaderText = "Código";
            this.codeDgv.Name = "codeDgv";
            this.codeDgv.ReadOnly = true;
            // 
            // nameDgv
            // 
            this.nameDgv.HeaderText = "Nombre";
            this.nameDgv.Name = "nameDgv";
            this.nameDgv.ReadOnly = true;
            // 
            // warehouseIdDgv
            // 
            this.warehouseIdDgv.HeaderText = "Almacen";
            this.warehouseIdDgv.Name = "warehouseIdDgv";
            this.warehouseIdDgv.ReadOnly = true;
            // 
            // createdAtDgv
            // 
            this.createdAtDgv.HeaderText = "Creación";
            this.createdAtDgv.Name = "createdAtDgv";
            this.createdAtDgv.ReadOnly = true;
            // 
            // editTotalCajas
            // 
            this.editTotalCajas.BackColor = System.Drawing.Color.FloralWhite;
            this.editTotalCajas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.editTotalCajas.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editTotalCajas.Location = new System.Drawing.Point(12, 3);
            this.editTotalCajas.Name = "editTotalCajas";
            this.editTotalCajas.ReadOnly = true;
            this.editTotalCajas.Size = new System.Drawing.Size(602, 16);
            this.editTotalCajas.TabIndex = 0;
            this.editTotalCajas.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // editCodigoCajaPadre
            // 
            this.editCodigoCajaPadre.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.editCodigoCajaPadre.BackColor = System.Drawing.Color.FloralWhite;
            this.editCodigoCajaPadre.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editCodigoCajaPadre.Location = new System.Drawing.Point(195, 12);
            this.editCodigoCajaPadre.Name = "editCodigoCajaPadre";
            this.editCodigoCajaPadre.ReadOnly = true;
            this.editCodigoCajaPadre.Size = new System.Drawing.Size(255, 26);
            this.editCodigoCajaPadre.TabIndex = 1;
            this.editCodigoCajaPadre.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FormSeleccionarCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(626, 362);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelToolbar);
            this.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSeleccionarCaja";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleccionar Caja";
            this.Load += new System.EventHandler(this.FormSeleccionarCaja_Load);
            this.panelToolbar.ResumeLayout(false);
            this.panelToolbar.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCajas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.TextBox editTotalCajas;
        private System.Windows.Forms.DataGridView dataGridViewCajas;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn codeDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn warehouseIdDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdAtDgv;
        private System.Windows.Forms.PictureBox imgSinDatos;
        private System.Windows.Forms.TextBox editCodigoCajaPadre;
    }
}