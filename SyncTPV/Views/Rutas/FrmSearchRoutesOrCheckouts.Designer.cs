
namespace SyncTPV.Views.Rutas
{
    partial class FrmSearchRoutesOrCheckouts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSearchRoutesOrCheckouts));
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.btnUpdateRoutesOrCheckouts = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.textTotalRoutesOrCheckouts = new System.Windows.Forms.Label();
            this.dataGridViewRoutesOrCheckouts = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textNombreRuta = new System.Windows.Forms.Label();
            this.editSearchRouteOrBox = new System.Windows.Forms.TextBox();
            this.panelToolbar.SuspendLayout();
            this.panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRoutesOrCheckouts)).BeginInit();
            this.SuspendLayout();
            // 
            // panelToolbar
            // 
            this.panelToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelToolbar.BackColor = System.Drawing.Color.Coral;
            this.panelToolbar.Controls.Add(this.btnUpdateRoutesOrCheckouts);
            this.panelToolbar.Controls.Add(this.btnClose);
            this.panelToolbar.Location = new System.Drawing.Point(-1, 0);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(411, 73);
            this.panelToolbar.TabIndex = 0;
            // 
            // btnUpdateRoutesOrCheckouts
            // 
            this.btnUpdateRoutesOrCheckouts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdateRoutesOrCheckouts.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnUpdateRoutesOrCheckouts.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnUpdateRoutesOrCheckouts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateRoutesOrCheckouts.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateRoutesOrCheckouts.ForeColor = System.Drawing.Color.White;
            this.btnUpdateRoutesOrCheckouts.Image = global::SyncTPV.Properties.Resources.update;
            this.btnUpdateRoutesOrCheckouts.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnUpdateRoutesOrCheckouts.Location = new System.Drawing.Point(84, 3);
            this.btnUpdateRoutesOrCheckouts.Name = "btnUpdateRoutesOrCheckouts";
            this.btnUpdateRoutesOrCheckouts.Size = new System.Drawing.Size(84, 67);
            this.btnUpdateRoutesOrCheckouts.TabIndex = 4;
            this.btnUpdateRoutesOrCheckouts.Text = "Actualizar";
            this.btnUpdateRoutesOrCheckouts.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnUpdateRoutesOrCheckouts.UseVisualStyleBackColor = true;
            this.btnUpdateRoutesOrCheckouts.Click += new System.EventHandler(this.btnUpdateRoutesOrCheckouts_Click);
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Image = global::SyncTPV.Properties.Resources.close;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.Location = new System.Drawing.Point(3, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 67);
            this.btnClose.TabIndex = 3;
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
            this.panelContent.Controls.Add(this.textTotalRoutesOrCheckouts);
            this.panelContent.Controls.Add(this.dataGridViewRoutesOrCheckouts);
            this.panelContent.Controls.Add(this.textNombreRuta);
            this.panelContent.Controls.Add(this.editSearchRouteOrBox);
            this.panelContent.Location = new System.Drawing.Point(-1, 79);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(411, 371);
            this.panelContent.TabIndex = 1;
            // 
            // textTotalRoutesOrCheckouts
            // 
            this.textTotalRoutesOrCheckouts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textTotalRoutesOrCheckouts.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTotalRoutesOrCheckouts.Location = new System.Drawing.Point(13, 39);
            this.textTotalRoutesOrCheckouts.Name = "textTotalRoutesOrCheckouts";
            this.textTotalRoutesOrCheckouts.Size = new System.Drawing.Size(386, 19);
            this.textTotalRoutesOrCheckouts.TabIndex = 3;
            this.textTotalRoutesOrCheckouts.Text = "Rutas";
            this.textTotalRoutesOrCheckouts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridViewRoutesOrCheckouts
            // 
            this.dataGridViewRoutesOrCheckouts.AllowUserToAddRows = false;
            this.dataGridViewRoutesOrCheckouts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewRoutesOrCheckouts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewRoutesOrCheckouts.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewRoutesOrCheckouts.BackgroundColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewRoutesOrCheckouts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewRoutesOrCheckouts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRoutesOrCheckouts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.code,
            this.name});
            this.dataGridViewRoutesOrCheckouts.Location = new System.Drawing.Point(13, 62);
            this.dataGridViewRoutesOrCheckouts.MultiSelect = false;
            this.dataGridViewRoutesOrCheckouts.Name = "dataGridViewRoutesOrCheckouts";
            this.dataGridViewRoutesOrCheckouts.ReadOnly = true;
            this.dataGridViewRoutesOrCheckouts.RowHeadersVisible = false;
            this.dataGridViewRoutesOrCheckouts.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewRoutesOrCheckouts.RowTemplate.ReadOnly = true;
            this.dataGridViewRoutesOrCheckouts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewRoutesOrCheckouts.Size = new System.Drawing.Size(386, 294);
            this.dataGridViewRoutesOrCheckouts.TabIndex = 2;
            this.dataGridViewRoutesOrCheckouts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewRoutesOrCheckouts_CellClick);
            this.dataGridViewRoutesOrCheckouts.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridViewRoutesOrCheckouts_Scroll);
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
            // textNombreRuta
            // 
            this.textNombreRuta.AutoSize = true;
            this.textNombreRuta.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textNombreRuta.Location = new System.Drawing.Point(44, 6);
            this.textNombreRuta.Name = "textNombreRuta";
            this.textNombreRuta.Size = new System.Drawing.Size(55, 14);
            this.textNombreRuta.TabIndex = 1;
            this.textNombreRuta.Text = "Nombre:";
            // 
            // editSearchRouteOrBox
            // 
            this.editSearchRouteOrBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editSearchRouteOrBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editSearchRouteOrBox.Location = new System.Drawing.Point(112, 3);
            this.editSearchRouteOrBox.Name = "editSearchRouteOrBox";
            this.editSearchRouteOrBox.Size = new System.Drawing.Size(269, 21);
            this.editSearchRouteOrBox.TabIndex = 1;
            this.editSearchRouteOrBox.TextChanged += new System.EventHandler(this.editSearchRouteOrBox_TextChanged);
            // 
            // FrmSearchRoutesOrCheckouts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 450);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelToolbar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSearchRoutesOrCheckouts";
            this.Text = "Buscar Rutas";
            this.Load += new System.EventHandler(this.FrmSearchRoutesOrCheckouts_Load);
            this.panelToolbar.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRoutesOrCheckouts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.TextBox editSearchRouteOrBox;
        private System.Windows.Forms.Label textNombreRuta;
        private System.Windows.Forms.DataGridView dataGridViewRoutesOrCheckouts;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn code;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.Button btnUpdateRoutesOrCheckouts;
        private System.Windows.Forms.Label textTotalRoutesOrCheckouts;
    }
}