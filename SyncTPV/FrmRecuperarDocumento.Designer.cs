namespace SyncTPV
{
    partial class FrmRecuperarDocumento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRecuperarDocumento));
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxMostrarTodos = new System.Windows.Forms.CheckBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.textClienteFrmRecuperarDocumento = new System.Windows.Forms.Label();
            this.dtGridDocumentos = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clienteDocument = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Folio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalGeneral = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imgSinDatosFrmRecuperarDocumentos = new System.Windows.Forms.PictureBox();
            this.textTotalRecords = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridDocumentos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatosFrmRecuperarDocumentos)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(112)))), ((int)(((byte)(67)))));
            this.panel1.Controls.Add(this.checkBoxMostrarTodos);
            this.panel1.Controls.Add(this.btnCerrar);
            this.panel1.Controls.Add(this.textClienteFrmRecuperarDocumento);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(873, 70);
            this.panel1.TabIndex = 1;
            // 
            // checkBoxMostrarTodos
            // 
            this.checkBoxMostrarTodos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxMostrarTodos.AutoSize = true;
            this.checkBoxMostrarTodos.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxMostrarTodos.ForeColor = System.Drawing.Color.White;
            this.checkBoxMostrarTodos.Location = new System.Drawing.Point(676, 47);
            this.checkBoxMostrarTodos.Name = "checkBoxMostrarTodos";
            this.checkBoxMostrarTodos.Size = new System.Drawing.Size(184, 19);
            this.checkBoxMostrarTodos.TabIndex = 2;
            this.checkBoxMostrarTodos.Text = "Ver Todos Los Documentos";
            this.checkBoxMostrarTodos.UseVisualStyleBackColor = true;
            this.checkBoxMostrarTodos.CheckedChanged += new System.EventHandler(this.checkBoxMostrarTodos_CheckedChanged);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCerrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Roboto Black", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Image = global::SyncTPV.Properties.Resources.close;
            this.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCerrar.Location = new System.Drawing.Point(15, 3);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(75, 64);
            this.btnCerrar.TabIndex = 1;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // textClienteFrmRecuperarDocumento
            // 
            this.textClienteFrmRecuperarDocumento.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textClienteFrmRecuperarDocumento.ForeColor = System.Drawing.Color.White;
            this.textClienteFrmRecuperarDocumento.Location = new System.Drawing.Point(96, 19);
            this.textClienteFrmRecuperarDocumento.Name = "textClienteFrmRecuperarDocumento";
            this.textClienteFrmRecuperarDocumento.Size = new System.Drawing.Size(691, 24);
            this.textClienteFrmRecuperarDocumento.TabIndex = 0;
            this.textClienteFrmRecuperarDocumento.Text = "Cliente";
            // 
            // dtGridDocumentos
            // 
            this.dtGridDocumentos.AllowUserToAddRows = false;
            this.dtGridDocumentos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtGridDocumentos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtGridDocumentos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtGridDocumentos.BackgroundColor = System.Drawing.Color.Azure;
            this.dtGridDocumentos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Coral;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtGridDocumentos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtGridDocumentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridDocumentos.ColumnHeadersVisible = false;
            this.dtGridDocumentos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.TipoDocumento,
            this.clienteDocument,
            this.Folio,
            this.Fecha,
            this.TotalGeneral,
            this.statusDgv});
            this.dtGridDocumentos.Location = new System.Drawing.Point(12, 105);
            this.dtGridDocumentos.Name = "dtGridDocumentos";
            this.dtGridDocumentos.RowHeadersVisible = false;
            this.dtGridDocumentos.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dtGridDocumentos.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtGridDocumentos.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dtGridDocumentos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtGridDocumentos.Size = new System.Drawing.Size(848, 366);
            this.dtGridDocumentos.TabIndex = 2;
            this.dtGridDocumentos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtGridDocumentos_CellClick);
            this.dtGridDocumentos.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dtGridDocumentos_Scroll);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // TipoDocumento
            // 
            this.TipoDocumento.HeaderText = "Tipo de Documento";
            this.TipoDocumento.Name = "TipoDocumento";
            // 
            // clienteDocument
            // 
            this.clienteDocument.HeaderText = "Cliente";
            this.clienteDocument.Name = "clienteDocument";
            // 
            // Folio
            // 
            this.Folio.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Folio.HeaderText = "Folio";
            this.Folio.Name = "Folio";
            // 
            // Fecha
            // 
            this.Fecha.FillWeight = 130F;
            this.Fecha.HeaderText = "Fecha y Hora";
            this.Fecha.Name = "Fecha";
            // 
            // TotalGeneral
            // 
            this.TotalGeneral.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TotalGeneral.FillWeight = 10.30928F;
            this.TotalGeneral.HeaderText = "Total";
            this.TotalGeneral.Name = "TotalGeneral";
            this.TotalGeneral.Width = 200;
            // 
            // statusDgv
            // 
            this.statusDgv.HeaderText = "Estatus";
            this.statusDgv.Name = "statusDgv";
            // 
            // imgSinDatosFrmRecuperarDocumentos
            // 
            this.imgSinDatosFrmRecuperarDocumentos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imgSinDatosFrmRecuperarDocumentos.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("imgSinDatosFrmRecuperarDocumentos.BackgroundImage")));
            this.imgSinDatosFrmRecuperarDocumentos.Location = new System.Drawing.Point(244, 145);
            this.imgSinDatosFrmRecuperarDocumentos.Name = "imgSinDatosFrmRecuperarDocumentos";
            this.imgSinDatosFrmRecuperarDocumentos.Size = new System.Drawing.Size(393, 281);
            this.imgSinDatosFrmRecuperarDocumentos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imgSinDatosFrmRecuperarDocumentos.TabIndex = 3;
            this.imgSinDatosFrmRecuperarDocumentos.TabStop = false;
            this.imgSinDatosFrmRecuperarDocumentos.Visible = false;
            // 
            // textTotalRecords
            // 
            this.textTotalRecords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textTotalRecords.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTotalRecords.Location = new System.Drawing.Point(12, 77);
            this.textTotalRecords.Name = "textTotalRecords";
            this.textTotalRecords.Size = new System.Drawing.Size(849, 25);
            this.textTotalRecords.TabIndex = 4;
            this.textTotalRecords.Text = "Total de Documentos";
            this.textTotalRecords.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmRecuperarDocumento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(873, 483);
            this.Controls.Add(this.textTotalRecords);
            this.Controls.Add(this.imgSinDatosFrmRecuperarDocumentos);
            this.Controls.Add(this.dtGridDocumentos);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmRecuperarDocumento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Abrir Documento";
            this.Load += new System.EventHandler(this.frmAbrirDoc_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridDocumentos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatosFrmRecuperarDocumentos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dtGridDocumentos;
        private System.Windows.Forms.PictureBox imgSinDatosFrmRecuperarDocumentos;
        private System.Windows.Forms.Label textClienteFrmRecuperarDocumento;
        private System.Windows.Forms.Label textTotalRecords;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.CheckBox checkBoxMostrarTodos;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn clienteDocument;
        private System.Windows.Forms.DataGridViewTextBoxColumn Folio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalGeneral;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusDgv;
    }
}