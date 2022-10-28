namespace SyncTPV
{
    partial class FormBuscaCliente
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBuscaCliente));
            this.editBuscarCliente = new System.Windows.Forms.TextBox();
            this.dtGridBuscaClientes = new System.Windows.Forms.DataGridView();
            this.idCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.textTotalClientes = new System.Windows.Forms.Label();
            this.checkBoxMostrarNuevos = new System.Windows.Forms.CheckBox();
            this.timerBuscarCliente = new System.Windows.Forms.Timer(this.components);
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.btnAgregarCliente = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridBuscaClientes)).BeginInit();
            this.panelToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // editBuscarCliente
            // 
            this.editBuscarCliente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editBuscarCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editBuscarCliente.Location = new System.Drawing.Point(72, 72);
            this.editBuscarCliente.Name = "editBuscarCliente";
            this.editBuscarCliente.Size = new System.Drawing.Size(310, 21);
            this.editBuscarCliente.TabIndex = 0;
            this.editBuscarCliente.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            this.editBuscarCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editBuscarCliente_KeyPress);
            this.editBuscarCliente.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            // 
            // dtGridBuscaClientes
            // 
            this.dtGridBuscaClientes.AllowUserToAddRows = false;
            this.dtGridBuscaClientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtGridBuscaClientes.BackgroundColor = System.Drawing.Color.FloralWhite;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtGridBuscaClientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtGridBuscaClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridBuscaClientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idCliente,
            this.codigo,
            this.nombre});
            this.dtGridBuscaClientes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtGridBuscaClientes.GridColor = System.Drawing.Color.FloralWhite;
            this.dtGridBuscaClientes.Location = new System.Drawing.Point(10, 126);
            this.dtGridBuscaClientes.Name = "dtGridBuscaClientes";
            this.dtGridBuscaClientes.ReadOnly = true;
            this.dtGridBuscaClientes.RowHeadersVisible = false;
            this.dtGridBuscaClientes.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FloralWhite;
            this.dtGridBuscaClientes.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtGridBuscaClientes.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dtGridBuscaClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtGridBuscaClientes.Size = new System.Drawing.Size(499, 202);
            this.dtGridBuscaClientes.TabIndex = 1;
            this.dtGridBuscaClientes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtGridBuscaClientes_CellClick);
            this.dtGridBuscaClientes.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dtGridBuscaClientes_Scroll);
            this.dtGridBuscaClientes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtGridBuscaClientes_KeyDown);
            this.dtGridBuscaClientes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtGridBuscaClientes_KeyPress);
            this.dtGridBuscaClientes.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dtGridBuscaClientes_KeyUp);
            // 
            // idCliente
            // 
            this.idCliente.HeaderText = "Id";
            this.idCliente.Name = "idCliente";
            this.idCliente.ReadOnly = true;
            this.idCliente.Visible = false;
            // 
            // codigo
            // 
            this.codigo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.codigo.HeaderText = "Clave";
            this.codigo.Name = "codigo";
            this.codigo.ReadOnly = true;
            // 
            // nombre
            // 
            this.nombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nombre.HeaderText = "Nombre";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(16, 72);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 24);
            this.button1.TabIndex = 51;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // textTotalClientes
            // 
            this.textTotalClientes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textTotalClientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTotalClientes.Location = new System.Drawing.Point(10, 99);
            this.textTotalClientes.Name = "textTotalClientes";
            this.textTotalClientes.Size = new System.Drawing.Size(499, 24);
            this.textTotalClientes.TabIndex = 52;
            this.textTotalClientes.Text = "Clientes";
            this.textTotalClientes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBoxMostrarNuevos
            // 
            this.checkBoxMostrarNuevos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxMostrarNuevos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxMostrarNuevos.Location = new System.Drawing.Point(388, 74);
            this.checkBoxMostrarNuevos.Name = "checkBoxMostrarNuevos";
            this.checkBoxMostrarNuevos.Size = new System.Drawing.Size(121, 18);
            this.checkBoxMostrarNuevos.TabIndex = 53;
            this.checkBoxMostrarNuevos.Text = "Mostrar Nuevos";
            this.checkBoxMostrarNuevos.UseVisualStyleBackColor = true;
            this.checkBoxMostrarNuevos.CheckedChanged += new System.EventHandler(this.checkBoxMostrarNuevos_CheckedChanged);
            // 
            // timerBuscarCliente
            // 
            this.timerBuscarCliente.Interval = 300;
            this.timerBuscarCliente.Tick += new System.EventHandler(this.timerBuscarCliente_Tick);
            // 
            // panelToolbar
            // 
            this.panelToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelToolbar.BackColor = System.Drawing.Color.Coral;
            this.panelToolbar.Controls.Add(this.btnAgregarCliente);
            this.panelToolbar.Controls.Add(this.btnClose);
            this.panelToolbar.Location = new System.Drawing.Point(0, -1);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(523, 67);
            this.panelToolbar.TabIndex = 54;
            // 
            // btnAgregarCliente
            // 
            this.btnAgregarCliente.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnAgregarCliente.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnAgregarCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarCliente.ForeColor = System.Drawing.Color.White;
            this.btnAgregarCliente.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAgregarCliente.Location = new System.Drawing.Point(84, 3);
            this.btnAgregarCliente.Name = "btnAgregarCliente";
            this.btnAgregarCliente.Size = new System.Drawing.Size(75, 60);
            this.btnAgregarCliente.TabIndex = 1;
            this.btnAgregarCliente.Text = "Agregar";
            this.btnAgregarCliente.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAgregarCliente.UseVisualStyleBackColor = true;
            this.btnAgregarCliente.Click += new System.EventHandler(this.btnAgregarCliente_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.Location = new System.Drawing.Point(3, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 60);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Cerrar";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FormBuscaCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(521, 340);
            this.Controls.Add(this.panelToolbar);
            this.Controls.Add(this.checkBoxMostrarNuevos);
            this.Controls.Add(this.textTotalClientes);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dtGridBuscaClientes);
            this.Controls.Add(this.editBuscarCliente);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormBuscaCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Buscar Cliente";
            this.Load += new System.EventHandler(this.frmBuscaCliente_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmBuscaCliente_KeyUp);
            this.Resize += new System.EventHandler(this.FormBuscaCliente_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dtGridBuscaClientes)).EndInit();
            this.panelToolbar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox editBuscarCliente;
        private System.Windows.Forms.DataGridView dtGridBuscaClientes;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label textTotalClientes;
        private System.Windows.Forms.CheckBox checkBoxMostrarNuevos;
        private System.Windows.Forms.Timer timerBuscarCliente;
        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAgregarCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn idCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
    }
}