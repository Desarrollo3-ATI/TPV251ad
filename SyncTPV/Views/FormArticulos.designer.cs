namespace SyncTPV
{
    partial class FormArticulos
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormArticulos));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlBusArti = new System.Windows.Forms.Panel();
            this.btnDescargarNuevos = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.editBuscarArticulo = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelBusquedaDerecha = new System.Windows.Forms.Panel();
            this.panelParametrosBusqueda = new System.Windows.Forms.Panel();
            this.comboBoxCamposBusqueda = new System.Windows.Forms.ComboBox();
            this.textCamposBusqueda = new System.Windows.Forms.Label();
            this.textMatchPosition = new System.Windows.Forms.Label();
            this.comboBoxMatchPosition = new System.Windows.Forms.ComboBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.textTotalItems = new System.Windows.Forms.Label();
            this.panelDataGridViewArticulos = new System.Windows.Forms.Panel();
            this.progressBarLoadItems = new System.Windows.Forms.ProgressBar();
            this.textVersion = new System.Windows.Forms.Label();
            this.imgSinDatos = new System.Windows.Forms.PictureBox();
            this.dataGridItems = new System.Windows.Forms.DataGridView();
            this.timerBuscarItems = new System.Windows.Forms.Timer(this.components);
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stockDgvItems = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precio1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detalles = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pnlBusArti.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelParametrosBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.panelDataGridViewArticulos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridItems)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBusArti
            // 
            this.pnlBusArti.BackColor = System.Drawing.Color.Coral;
            this.pnlBusArti.Controls.Add(this.btnDescargarNuevos);
            this.pnlBusArti.Controls.Add(this.button2);
            this.pnlBusArti.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBusArti.Location = new System.Drawing.Point(0, 0);
            this.pnlBusArti.Name = "pnlBusArti";
            this.pnlBusArti.Size = new System.Drawing.Size(869, 64);
            this.pnlBusArti.TabIndex = 12;
            // 
            // btnDescargarNuevos
            // 
            this.btnDescargarNuevos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDescargarNuevos.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnDescargarNuevos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDescargarNuevos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDescargarNuevos.ForeColor = System.Drawing.Color.White;
            this.btnDescargarNuevos.Image = global::SyncTPV.Properties.Resources.update;
            this.btnDescargarNuevos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDescargarNuevos.Location = new System.Drawing.Point(104, 3);
            this.btnDescargarNuevos.Name = "btnDescargarNuevos";
            this.btnDescargarNuevos.Size = new System.Drawing.Size(123, 58);
            this.btnDescargarNuevos.TabIndex = 4;
            this.btnDescargarNuevos.Text = "Descargar \r\nNuevos";
            this.btnDescargarNuevos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDescargarNuevos.UseVisualStyleBackColor = true;
            this.btnDescargarNuevos.Click += new System.EventHandler(this.btnDescargarNuevos_Click);
            // 
            // button2
            // 
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(20)))), ((int)(((byte)(224)))));
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.Location = new System.Drawing.Point(3, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 58);
            this.button2.TabIndex = 3;
            this.button2.Text = "Cerrar (ESC)";
            this.button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.PictureCerrar_Click);
            this.button2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.button2_KeyUp);
            // 
            // editBuscarArticulo
            // 
            this.editBuscarArticulo.BackColor = System.Drawing.Color.White;
            this.editBuscarArticulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editBuscarArticulo.Location = new System.Drawing.Point(293, 13);
            this.editBuscarArticulo.Name = "editBuscarArticulo";
            this.editBuscarArticulo.Size = new System.Drawing.Size(310, 22);
            this.editBuscarArticulo.TabIndex = 1;
            this.editBuscarArticulo.TextChanged += new System.EventHandler(this.editBuscarArticulo_TextChanged);
            this.editBuscarArticulo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editBuscarArticulo_KeyPress);
            this.editBuscarArticulo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.editBuscarArticulo_KeyUp);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panelBusquedaDerecha);
            this.panel2.Controls.Add(this.panelParametrosBusqueda);
            this.panel2.Controls.Add(this.pictureBox5);
            this.panel2.Controls.Add(this.editBuscarArticulo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 64);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(869, 110);
            this.panel2.TabIndex = 15;
            // 
            // panelBusquedaDerecha
            // 
            this.panelBusquedaDerecha.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelBusquedaDerecha.Location = new System.Drawing.Point(644, 0);
            this.panelBusquedaDerecha.Name = "panelBusquedaDerecha";
            this.panelBusquedaDerecha.Size = new System.Drawing.Size(225, 110);
            this.panelBusquedaDerecha.TabIndex = 75;
            // 
            // panelParametrosBusqueda
            // 
            this.panelParametrosBusqueda.Controls.Add(this.comboBoxCamposBusqueda);
            this.panelParametrosBusqueda.Controls.Add(this.textCamposBusqueda);
            this.panelParametrosBusqueda.Controls.Add(this.textMatchPosition);
            this.panelParametrosBusqueda.Controls.Add(this.comboBoxMatchPosition);
            this.panelParametrosBusqueda.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelParametrosBusqueda.Location = new System.Drawing.Point(0, 0);
            this.panelParametrosBusqueda.Name = "panelParametrosBusqueda";
            this.panelParametrosBusqueda.Size = new System.Drawing.Size(287, 110);
            this.panelParametrosBusqueda.TabIndex = 74;
            // 
            // comboBoxCamposBusqueda
            // 
            this.comboBoxCamposBusqueda.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxCamposBusqueda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCamposBusqueda.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBoxCamposBusqueda.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxCamposBusqueda.FormattingEnabled = true;
            this.comboBoxCamposBusqueda.Location = new System.Drawing.Point(12, 78);
            this.comboBoxCamposBusqueda.Name = "comboBoxCamposBusqueda";
            this.comboBoxCamposBusqueda.Size = new System.Drawing.Size(150, 21);
            this.comboBoxCamposBusqueda.TabIndex = 82;
            // 
            // textCamposBusqueda
            // 
            this.textCamposBusqueda.AutoSize = true;
            this.textCamposBusqueda.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textCamposBusqueda.Location = new System.Drawing.Point(9, 62);
            this.textCamposBusqueda.Name = "textCamposBusqueda";
            this.textCamposBusqueda.Size = new System.Drawing.Size(115, 13);
            this.textCamposBusqueda.TabIndex = 81;
            this.textCamposBusqueda.Text = "Campos de Busqueda";
            // 
            // textMatchPosition
            // 
            this.textMatchPosition.AutoSize = true;
            this.textMatchPosition.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textMatchPosition.Location = new System.Drawing.Point(12, 6);
            this.textMatchPosition.Name = "textMatchPosition";
            this.textMatchPosition.Size = new System.Drawing.Size(138, 13);
            this.textMatchPosition.TabIndex = 80;
            this.textMatchPosition.Text = "Posición de Coincidencias";
            // 
            // comboBoxMatchPosition
            // 
            this.comboBoxMatchPosition.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxMatchPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMatchPosition.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBoxMatchPosition.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxMatchPosition.FormattingEnabled = true;
            this.comboBoxMatchPosition.Location = new System.Drawing.Point(12, 22);
            this.comboBoxMatchPosition.Name = "comboBoxMatchPosition";
            this.comboBoxMatchPosition.Size = new System.Drawing.Size(150, 21);
            this.comboBoxMatchPosition.TabIndex = 79;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(609, 6);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(29, 29);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 73;
            this.pictureBox5.TabStop = false;
            // 
            // textTotalItems
            // 
            this.textTotalItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textTotalItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTotalItems.Location = new System.Drawing.Point(15, 0);
            this.textTotalItems.Name = "textTotalItems";
            this.textTotalItems.Size = new System.Drawing.Size(842, 22);
            this.textTotalItems.TabIndex = 74;
            this.textTotalItems.Text = "Total";
            this.textTotalItems.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelDataGridViewArticulos
            // 
            this.panelDataGridViewArticulos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDataGridViewArticulos.Controls.Add(this.progressBarLoadItems);
            this.panelDataGridViewArticulos.Controls.Add(this.textVersion);
            this.panelDataGridViewArticulos.Controls.Add(this.textTotalItems);
            this.panelDataGridViewArticulos.Controls.Add(this.imgSinDatos);
            this.panelDataGridViewArticulos.Controls.Add(this.dataGridItems);
            this.panelDataGridViewArticulos.Location = new System.Drawing.Point(0, 180);
            this.panelDataGridViewArticulos.Name = "panelDataGridViewArticulos";
            this.panelDataGridViewArticulos.Size = new System.Drawing.Size(869, 351);
            this.panelDataGridViewArticulos.TabIndex = 16;
            // 
            // progressBarLoadItems
            // 
            this.progressBarLoadItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarLoadItems.BackColor = System.Drawing.Color.FloralWhite;
            this.progressBarLoadItems.ForeColor = System.Drawing.Color.FloralWhite;
            this.progressBarLoadItems.Location = new System.Drawing.Point(757, 337);
            this.progressBarLoadItems.Name = "progressBarLoadItems";
            this.progressBarLoadItems.Size = new System.Drawing.Size(112, 11);
            this.progressBarLoadItems.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBarLoadItems.TabIndex = 12;
            this.progressBarLoadItems.Visible = false;
            // 
            // textVersion
            // 
            this.textVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textVersion.AutoSize = true;
            this.textVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textVersion.Location = new System.Drawing.Point(3, 338);
            this.textVersion.Name = "textVersion";
            this.textVersion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textVersion.Size = new System.Drawing.Size(49, 13);
            this.textVersion.TabIndex = 11;
            this.textVersion.Text = "Version";
            this.textVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imgSinDatos
            // 
            this.imgSinDatos.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.imgSinDatos.Image = ((System.Drawing.Image)(resources.GetObject("imgSinDatos.Image")));
            this.imgSinDatos.Location = new System.Drawing.Point(328, 128);
            this.imgSinDatos.Name = "imgSinDatos";
            this.imgSinDatos.Size = new System.Drawing.Size(215, 159);
            this.imgSinDatos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgSinDatos.TabIndex = 10;
            this.imgSinDatos.TabStop = false;
            // 
            // dataGridItems
            // 
            this.dataGridItems.AllowUserToAddRows = false;
            this.dataGridItems.AllowUserToDeleteRows = false;
            this.dataGridItems.AllowUserToResizeRows = false;
            this.dataGridItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridItems.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridItems.BackgroundColor = System.Drawing.Color.FloralWhite;
            this.dataGridItems.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridItems.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.SkyBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.code,
            this.name,
            this.stockDgvItems,
            this.precio1,
            this.detalles});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(222)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridItems.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridItems.EnableHeadersVisualStyles = false;
            this.dataGridItems.GridColor = System.Drawing.Color.Linen;
            this.dataGridItems.Location = new System.Drawing.Point(28, 25);
            this.dataGridItems.Name = "dataGridItems";
            this.dataGridItems.ReadOnly = true;
            this.dataGridItems.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.InfoText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Linen;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridItems.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridItems.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(145)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridItems.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridItems.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridItems.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FloralWhite;
            this.dataGridItems.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridItems.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.dataGridItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridItems.Size = new System.Drawing.Size(814, 294);
            this.dataGridItems.TabIndex = 2;
            this.dataGridItems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridItems_CellClick);
            this.dataGridItems.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridItems_CellContentClick);
            this.dataGridItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridItems_KeyDown);
            this.dataGridItems.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridItems_KeyUp);
            // 
            // timerBuscarItems
            // 
            this.timerBuscarItems.Interval = 400;
            this.timerBuscarItems.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // id
            // 
            this.id.HeaderText = "Id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            // 
            // code
            // 
            this.code.HeaderText = "Código";
            this.code.Name = "code";
            this.code.ReadOnly = true;
            // 
            // name
            // 
            this.name.HeaderText = "Nombre";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // stockDgvItems
            // 
            this.stockDgvItems.HeaderText = "Existencia";
            this.stockDgvItems.Name = "stockDgvItems";
            this.stockDgvItems.ReadOnly = true;
            // 
            // precio1
            // 
            this.precio1.HeaderText = "Precio ";
            this.precio1.Name = "precio1";
            this.precio1.ReadOnly = true;
            // 
            // detalles
            // 
            this.detalles.HeaderText = "Detalles";
            this.detalles.Name = "detalles";
            this.detalles.ReadOnly = true;
            this.detalles.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.detalles.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // FormArticulos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(245)))), ((int)(((byte)(231)))));
            this.ClientSize = new System.Drawing.Size(869, 529);
            this.Controls.Add(this.panelDataGridViewArticulos);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlBusArti);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(885, 521);
            this.Name = "FormArticulos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Artículos";
            this.Load += new System.EventHandler(this.frmArticulos_LoadAsync);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmArticulos_KeyUp);
            this.Resize += new System.EventHandler(this.FrmArticulos_Resize);
            this.pnlBusArti.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelParametrosBusqueda.ResumeLayout(false);
            this.panelParametrosBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.panelDataGridViewArticulos.ResumeLayout(false);
            this.panelDataGridViewArticulos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridItems)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlBusArti;
        private System.Windows.Forms.TextBox editBuscarArticulo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label textTotalItems;
        private System.Windows.Forms.Panel panelDataGridViewArticulos;
        private System.Windows.Forms.PictureBox imgSinDatos;
        private System.Windows.Forms.DataGridView dataGridItems;
        private System.Windows.Forms.Button btnDescargarNuevos;
        private System.Windows.Forms.Label textVersion;
        private System.Windows.Forms.Timer timerBuscarItems;
        private System.Windows.Forms.ProgressBar progressBarLoadItems;
        private System.Windows.Forms.Panel panelParametrosBusqueda;
        private System.Windows.Forms.ComboBox comboBoxCamposBusqueda;
        private System.Windows.Forms.Label textCamposBusqueda;
        private System.Windows.Forms.Label textMatchPosition;
        private System.Windows.Forms.ComboBox comboBoxMatchPosition;
        private System.Windows.Forms.Panel panelBusquedaDerecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn code;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn stockDgvItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn precio1;
        private System.Windows.Forms.DataGridViewButtonColumn detalles;
    }
}