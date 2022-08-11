namespace SyncTPV.Views.Entries
{
    partial class FormIngresos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormIngresos));
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.tableLayoutPanelContent = new System.Windows.Forms.TableLayoutPanel();
            this.panelIngreso = new System.Windows.Forms.Panel();
            this.imgSinDatosFc = new System.Windows.Forms.PictureBox();
            this.btnCancelOrDelete = new System.Windows.Forms.Button();
            this.btnCreateOrUpdate = new System.Windows.Forms.Button();
            this.dataGridViewFc = new System.Windows.Forms.DataGridView();
            this.idFcIngresoDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameFcIngresoDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountFcIngresoDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelTablaIngresos = new System.Windows.Forms.Panel();
            this.imgSinDatos = new System.Windows.Forms.PictureBox();
            this.dataGridViewIngresos = new System.Windows.Forms.DataGridView();
            this.textTotalRecords = new System.Windows.Forms.Label();
            this.idIngresoDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numberIngresoDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.conceptIngresoDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionIngresoDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaHoraIngresoDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalDgvIngresos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusIngresoDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelToolbar.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.tableLayoutPanelContent.SuspendLayout();
            this.panelIngreso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatosFc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFc)).BeginInit();
            this.panelTablaIngresos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIngresos)).BeginInit();
            this.SuspendLayout();
            // 
            // panelToolbar
            // 
            this.panelToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelToolbar.BackColor = System.Drawing.Color.Coral;
            this.panelToolbar.Controls.Add(this.btnNuevo);
            this.panelToolbar.Controls.Add(this.btnClose);
            this.panelToolbar.Location = new System.Drawing.Point(0, -1);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(802, 69);
            this.panelToolbar.TabIndex = 0;
            // 
            // btnNuevo
            // 
            this.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevo.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnNuevo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Image = global::SyncTPV.Properties.Resources.add_white;
            this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNuevo.Location = new System.Drawing.Point(84, 3);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(75, 63);
            this.btnNuevo.TabIndex = 1;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Image = global::SyncTPV.Properties.Resources.close;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.Location = new System.Drawing.Point(3, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 63);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Cerrar";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnClose_KeyPress);
            // 
            // panelContent
            // 
            this.panelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContent.Controls.Add(this.tableLayoutPanelContent);
            this.panelContent.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelContent.Location = new System.Drawing.Point(0, 66);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(802, 387);
            this.panelContent.TabIndex = 1;
            // 
            // tableLayoutPanelContent
            // 
            this.tableLayoutPanelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelContent.AutoScroll = true;
            this.tableLayoutPanelContent.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetPartial;
            this.tableLayoutPanelContent.ColumnCount = 2;
            this.tableLayoutPanelContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelContent.Controls.Add(this.panelIngreso, 1, 0);
            this.tableLayoutPanelContent.Controls.Add(this.panelTablaIngresos, 0, 0);
            this.tableLayoutPanelContent.Location = new System.Drawing.Point(12, 8);
            this.tableLayoutPanelContent.Name = "tableLayoutPanelContent";
            this.tableLayoutPanelContent.RowCount = 1;
            this.tableLayoutPanelContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelContent.Size = new System.Drawing.Size(776, 364);
            this.tableLayoutPanelContent.TabIndex = 0;
            // 
            // panelIngreso
            // 
            this.panelIngreso.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelIngreso.Controls.Add(this.imgSinDatosFc);
            this.panelIngreso.Controls.Add(this.btnCancelOrDelete);
            this.panelIngreso.Controls.Add(this.btnCreateOrUpdate);
            this.panelIngreso.Controls.Add(this.dataGridViewFc);
            this.panelIngreso.Location = new System.Drawing.Point(392, 6);
            this.panelIngreso.Name = "panelIngreso";
            this.panelIngreso.Size = new System.Drawing.Size(378, 352);
            this.panelIngreso.TabIndex = 1;
            // 
            // imgSinDatosFc
            // 
            this.imgSinDatosFc.BackColor = System.Drawing.Color.Azure;
            this.imgSinDatosFc.Location = new System.Drawing.Point(98, 88);
            this.imgSinDatosFc.Name = "imgSinDatosFc";
            this.imgSinDatosFc.Size = new System.Drawing.Size(197, 122);
            this.imgSinDatosFc.TabIndex = 6;
            this.imgSinDatosFc.TabStop = false;
            // 
            // btnCancelOrDelete
            // 
            this.btnCancelOrDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelOrDelete.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnCancelOrDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnCancelOrDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelOrDelete.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelOrDelete.Location = new System.Drawing.Point(3, 313);
            this.btnCancelOrDelete.Name = "btnCancelOrDelete";
            this.btnCancelOrDelete.Size = new System.Drawing.Size(155, 36);
            this.btnCancelOrDelete.TabIndex = 5;
            this.btnCancelOrDelete.Text = "Cancelar";
            this.btnCancelOrDelete.UseVisualStyleBackColor = true;
            this.btnCancelOrDelete.Click += new System.EventHandler(this.btnCancelOrDelete_Click);
            this.btnCancelOrDelete.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnCancelOrDelete_KeyPress);
            // 
            // btnCreateOrUpdate
            // 
            this.btnCreateOrUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateOrUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreateOrUpdate.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnCreateOrUpdate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnCreateOrUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateOrUpdate.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateOrUpdate.Location = new System.Drawing.Point(220, 313);
            this.btnCreateOrUpdate.Name = "btnCreateOrUpdate";
            this.btnCreateOrUpdate.Size = new System.Drawing.Size(155, 36);
            this.btnCreateOrUpdate.TabIndex = 4;
            this.btnCreateOrUpdate.Text = "Guardar (F10)";
            this.btnCreateOrUpdate.UseVisualStyleBackColor = true;
            this.btnCreateOrUpdate.Click += new System.EventHandler(this.btnCreateOrUpdate_Click);
            this.btnCreateOrUpdate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnCreateOrUpdate_KeyPress);
            // 
            // dataGridViewFc
            // 
            this.dataGridViewFc.AllowUserToAddRows = false;
            this.dataGridViewFc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewFc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewFc.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewFc.BackgroundColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(1, 3, 1, 3);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewFc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idFcIngresoDgv,
            this.nameFcIngresoDgv,
            this.amountFcIngresoDgv});
            this.dataGridViewFc.GridColor = System.Drawing.Color.Azure;
            this.dataGridViewFc.Location = new System.Drawing.Point(3, 4);
            this.dataGridViewFc.MultiSelect = false;
            this.dataGridViewFc.Name = "dataGridViewFc";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFc.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewFc.RowHeadersVisible = false;
            this.dataGridViewFc.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewFc.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.Azure;
            this.dataGridViewFc.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewFc.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.dataGridViewFc.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFc.Size = new System.Drawing.Size(372, 303);
            this.dataGridViewFc.TabIndex = 3;
            this.dataGridViewFc.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFc_CellContentClick);
            this.dataGridViewFc.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFc_CellValueChanged);
            this.dataGridViewFc.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridViewFc_Scroll);
            this.dataGridViewFc.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridViewFc_KeyUp);
            // 
            // idFcIngresoDgv
            // 
            this.idFcIngresoDgv.HeaderText = "Id";
            this.idFcIngresoDgv.Name = "idFcIngresoDgv";
            // 
            // nameFcIngresoDgv
            // 
            this.nameFcIngresoDgv.HeaderText = "Nombre";
            this.nameFcIngresoDgv.Name = "nameFcIngresoDgv";
            // 
            // amountFcIngresoDgv
            // 
            this.amountFcIngresoDgv.HeaderText = "Importe";
            this.amountFcIngresoDgv.Name = "amountFcIngresoDgv";
            // 
            // panelTablaIngresos
            // 
            this.panelTablaIngresos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTablaIngresos.Controls.Add(this.imgSinDatos);
            this.panelTablaIngresos.Controls.Add(this.dataGridViewIngresos);
            this.panelTablaIngresos.Controls.Add(this.textTotalRecords);
            this.panelTablaIngresos.Location = new System.Drawing.Point(6, 6);
            this.panelTablaIngresos.Name = "panelTablaIngresos";
            this.panelTablaIngresos.Size = new System.Drawing.Size(377, 352);
            this.panelTablaIngresos.TabIndex = 0;
            // 
            // imgSinDatos
            // 
            this.imgSinDatos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.imgSinDatos.BackColor = System.Drawing.Color.Azure;
            this.imgSinDatos.Location = new System.Drawing.Point(34, 88);
            this.imgSinDatos.Name = "imgSinDatos";
            this.imgSinDatos.Size = new System.Drawing.Size(297, 205);
            this.imgSinDatos.TabIndex = 2;
            this.imgSinDatos.TabStop = false;
            // 
            // dataGridViewIngresos
            // 
            this.dataGridViewIngresos.AllowUserToAddRows = false;
            this.dataGridViewIngresos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewIngresos.BackgroundColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(1, 3, 1, 3);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewIngresos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewIngresos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewIngresos.ColumnHeadersVisible = false;
            this.dataGridViewIngresos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idIngresoDgv,
            this.numberIngresoDgv,
            this.conceptIngresoDgv,
            this.descriptionIngresoDgv,
            this.fechaHoraIngresoDgv,
            this.totalDgvIngresos,
            this.statusIngresoDgv});
            this.dataGridViewIngresos.GridColor = System.Drawing.Color.Azure;
            this.dataGridViewIngresos.Location = new System.Drawing.Point(3, 26);
            this.dataGridViewIngresos.Name = "dataGridViewIngresos";
            this.dataGridViewIngresos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewIngresos.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewIngresos.RowHeadersVisible = false;
            this.dataGridViewIngresos.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewIngresos.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.Azure;
            this.dataGridViewIngresos.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewIngresos.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.dataGridViewIngresos.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewIngresos.Size = new System.Drawing.Size(371, 323);
            this.dataGridViewIngresos.TabIndex = 1;
            this.dataGridViewIngresos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewIngresos_CellContentClick);
            this.dataGridViewIngresos.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridViewIngresos_Scroll);
            this.dataGridViewIngresos.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridViewIngresos_KeyUp);
            // 
            // textTotalRecords
            // 
            this.textTotalRecords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textTotalRecords.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTotalRecords.Location = new System.Drawing.Point(3, 4);
            this.textTotalRecords.Name = "textTotalRecords";
            this.textTotalRecords.Size = new System.Drawing.Size(371, 19);
            this.textTotalRecords.TabIndex = 0;
            this.textTotalRecords.Text = "Ingresos";
            this.textTotalRecords.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // idIngresoDgv
            // 
            this.idIngresoDgv.HeaderText = "Id";
            this.idIngresoDgv.Name = "idIngresoDgv";
            // 
            // numberIngresoDgv
            // 
            this.numberIngresoDgv.HeaderText = "Número";
            this.numberIngresoDgv.Name = "numberIngresoDgv";
            // 
            // conceptIngresoDgv
            // 
            this.conceptIngresoDgv.HeaderText = "Concepto";
            this.conceptIngresoDgv.Name = "conceptIngresoDgv";
            // 
            // descriptionIngresoDgv
            // 
            this.descriptionIngresoDgv.HeaderText = "Descripción";
            this.descriptionIngresoDgv.Name = "descriptionIngresoDgv";
            // 
            // fechaHoraIngresoDgv
            // 
            this.fechaHoraIngresoDgv.HeaderText = "Fecha";
            this.fechaHoraIngresoDgv.Name = "fechaHoraIngresoDgv";
            // 
            // totalDgvIngresos
            // 
            this.totalDgvIngresos.HeaderText = "Total";
            this.totalDgvIngresos.Name = "totalDgvIngresos";
            // 
            // statusIngresoDgv
            // 
            this.statusIngresoDgv.HeaderText = "Estatus";
            this.statusIngresoDgv.Name = "statusIngresoDgv";
            // 
            // FormIngresos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelToolbar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "FormIngresos";
            this.Text = "Ingresos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormIngresos_FormClosing);
            this.Load += new System.EventHandler(this.FormIngresos_Load);
            this.panelToolbar.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.tableLayoutPanelContent.ResumeLayout(false);
            this.panelIngreso.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatosFc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFc)).EndInit();
            this.panelTablaIngresos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIngresos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelContent;
        private System.Windows.Forms.Panel panelIngreso;
        private System.Windows.Forms.Panel panelTablaIngresos;
        private System.Windows.Forms.Label textTotalRecords;
        private System.Windows.Forms.DataGridView dataGridViewIngresos;
        private System.Windows.Forms.PictureBox imgSinDatos;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.DataGridView dataGridViewFc;
        private System.Windows.Forms.Button btnCancelOrDelete;
        private System.Windows.Forms.Button btnCreateOrUpdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn idFcIngresoDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameFcIngresoDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountFcIngresoDgv;
        private System.Windows.Forms.PictureBox imgSinDatosFc;
        private System.Windows.Forms.DataGridViewTextBoxColumn idIngresoDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn numberIngresoDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn conceptIngresoDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionIngresoDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaHoraIngresoDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalDgvIngresos;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusIngresoDgv;
    }
}