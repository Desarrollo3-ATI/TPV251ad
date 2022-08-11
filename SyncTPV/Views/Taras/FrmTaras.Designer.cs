
namespace SyncTPV.Views.Taras
{
    partial class FrmTaras
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTaras));
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.textTotalRecords = new System.Windows.Forms.Label();
            this.dataGridViewTaras = new System.Windows.Forms.DataGridView();
            this.idDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codeDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colorDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pesoDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipoDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdAtDgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panelToolbar.SuspendLayout();
            this.panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTaras)).BeginInit();
            this.SuspendLayout();
            // 
            // panelToolbar
            // 
            this.panelToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelToolbar.BackColor = System.Drawing.Color.Coral;
            this.panelToolbar.Controls.Add(this.btnAdd);
            this.panelToolbar.Controls.Add(this.btnClose);
            this.panelToolbar.Location = new System.Drawing.Point(-1, 0);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(732, 61);
            this.panelToolbar.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Roboto Black", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(3, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(62, 56);
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
            this.panelContent.Controls.Add(this.textTotalRecords);
            this.panelContent.Controls.Add(this.dataGridViewTaras);
            this.panelContent.Location = new System.Drawing.Point(12, 67);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(705, 358);
            this.panelContent.TabIndex = 1;
            // 
            // textTotalRecords
            // 
            this.textTotalRecords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textTotalRecords.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTotalRecords.Location = new System.Drawing.Point(3, 0);
            this.textTotalRecords.Name = "textTotalRecords";
            this.textTotalRecords.Size = new System.Drawing.Size(699, 19);
            this.textTotalRecords.TabIndex = 1;
            this.textTotalRecords.Text = "Cajas";
            this.textTotalRecords.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridViewTaras
            // 
            this.dataGridViewTaras.AllowUserToAddRows = false;
            this.dataGridViewTaras.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewTaras.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTaras.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewTaras.BackgroundColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTaras.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTaras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTaras.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDgv,
            this.codeDgv,
            this.nameDgv,
            this.colorDgv,
            this.pesoDgv,
            this.tipoDgv,
            this.createdAtDgv});
            this.dataGridViewTaras.Location = new System.Drawing.Point(6, 22);
            this.dataGridViewTaras.MultiSelect = false;
            this.dataGridViewTaras.Name = "dataGridViewTaras";
            this.dataGridViewTaras.RowHeadersVisible = false;
            this.dataGridViewTaras.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTaras.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTaras.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(10);
            this.dataGridViewTaras.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTaras.RowTemplate.Height = 50;
            this.dataGridViewTaras.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTaras.Size = new System.Drawing.Size(696, 333);
            this.dataGridViewTaras.TabIndex = 0;
            this.dataGridViewTaras.CancelRowEdit += new System.Windows.Forms.QuestionEventHandler(this.dataGridViewTaras_CancelRowEdit);
            this.dataGridViewTaras.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewTaras_CellValidating);
            this.dataGridViewTaras.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTaras_CellValueChanged);
            this.dataGridViewTaras.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridViewTaras_Scroll);
            this.dataGridViewTaras.SelectionChanged += new System.EventHandler(this.dataGridViewTaras_SelectionChanged);
            // 
            // idDgv
            // 
            this.idDgv.HeaderText = "Id";
            this.idDgv.Name = "idDgv";
            // 
            // codeDgv
            // 
            this.codeDgv.HeaderText = "Código";
            this.codeDgv.Name = "codeDgv";
            // 
            // nameDgv
            // 
            this.nameDgv.HeaderText = "Nombre";
            this.nameDgv.Name = "nameDgv";
            // 
            // colorDgv
            // 
            this.colorDgv.HeaderText = "Color";
            this.colorDgv.Name = "colorDgv";
            // 
            // pesoDgv
            // 
            this.pesoDgv.HeaderText = "Peso";
            this.pesoDgv.Name = "pesoDgv";
            // 
            // tipoDgv
            // 
            this.tipoDgv.HeaderText = "Tipo";
            this.tipoDgv.Name = "tipoDgv";
            // 
            // createdAtDgv
            // 
            this.createdAtDgv.HeaderText = "Creado";
            this.createdAtDgv.Name = "createdAtDgv";
            // 
            // btnAdd
            // 
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Roboto Black", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Image = global::SyncTPV.Properties.Resources.add_white;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAdd.Location = new System.Drawing.Point(71, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 56);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Nuevo";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // FrmTaras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(729, 437);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelToolbar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmTaras";
            this.Text = "Cajas";
            this.Load += new System.EventHandler(this.FrmTaras_Load);
            this.panelToolbar.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTaras)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label textTotalRecords;
        private System.Windows.Forms.DataGridView dataGridViewTaras;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn codeDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn colorDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn pesoDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipoDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdAtDgv;
        private System.Windows.Forms.Button btnAdd;
    }
}