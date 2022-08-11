namespace SyncTPV
{
    partial class frmPedidos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPedidos));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnfrmArticulos = new System.Windows.Forms.Button();
            this.pnlCerrar = new System.Windows.Forms.Panel();
            this.picCerrar = new System.Windows.Forms.PictureBox();
            this.pnlIzq = new System.Windows.Forms.Panel();
            this.btnClienteLabel = new System.Windows.Forms.Button();
            this.txtBuscarPedido = new System.Windows.Forms.TextBox();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.dtGridPedidos = new System.Windows.Forms.DataGridView();
            this.Pedidos = new System.Windows.Forms.DataGridViewImageColumn();
            this.Cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Agente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Folio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Direccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlCerrar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCerrar)).BeginInit();
            this.pnlIzq.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridPedidos)).BeginInit();
            this.SuspendLayout();
            // 
            // btnfrmArticulos
            // 
            this.btnfrmArticulos.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnfrmArticulos.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnfrmArticulos.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnfrmArticulos.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnfrmArticulos.ForeColor = System.Drawing.Color.White;
            this.btnfrmArticulos.Location = new System.Drawing.Point(0, 0);
            this.btnfrmArticulos.Name = "btnfrmArticulos";
            this.btnfrmArticulos.Size = new System.Drawing.Size(721, 36);
            this.btnfrmArticulos.TabIndex = 13;
            this.btnfrmArticulos.Text = "P  E  D  I  D  O  S";
            this.btnfrmArticulos.UseVisualStyleBackColor = false;
            // 
            // pnlCerrar
            // 
            this.pnlCerrar.Controls.Add(this.picCerrar);
            this.pnlCerrar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCerrar.Location = new System.Drawing.Point(0, 0);
            this.pnlCerrar.Name = "pnlCerrar";
            this.pnlCerrar.Size = new System.Drawing.Size(721, 37);
            this.pnlCerrar.TabIndex = 41;
            // 
            // picCerrar
            // 
            this.picCerrar.Dock = System.Windows.Forms.DockStyle.Right;
            this.picCerrar.Image = ((System.Drawing.Image)(resources.GetObject("picCerrar.Image")));
            this.picCerrar.Location = new System.Drawing.Point(697, 0);
            this.picCerrar.Name = "picCerrar";
            this.picCerrar.Size = new System.Drawing.Size(24, 37);
            this.picCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCerrar.TabIndex = 39;
            this.picCerrar.TabStop = false;
            this.picCerrar.Click += new System.EventHandler(this.picCerrar_Click);
            // 
            // pnlIzq
            // 
            this.pnlIzq.Controls.Add(this.btnClienteLabel);
            this.pnlIzq.Controls.Add(this.pnlCerrar);
            this.pnlIzq.Controls.Add(this.txtBuscarPedido);
            this.pnlIzq.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlIzq.Location = new System.Drawing.Point(0, 36);
            this.pnlIzq.Name = "pnlIzq";
            this.pnlIzq.Size = new System.Drawing.Size(721, 44);
            this.pnlIzq.TabIndex = 42;
            // 
            // btnClienteLabel
            // 
            this.btnClienteLabel.BackColor = System.Drawing.Color.Teal;
            this.btnClienteLabel.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClienteLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnClienteLabel.Image = ((System.Drawing.Image)(resources.GetObject("btnClienteLabel.Image")));
            this.btnClienteLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClienteLabel.Location = new System.Drawing.Point(36, 1);
            this.btnClienteLabel.Name = "btnClienteLabel";
            this.btnClienteLabel.Size = new System.Drawing.Size(137, 32);
            this.btnClienteLabel.TabIndex = 17;
            this.btnClienteLabel.Text = "Buscar Cliente";
            this.btnClienteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClienteLabel.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnClienteLabel.UseVisualStyleBackColor = false;
            // 
            // txtBuscarPedido
            // 
            this.txtBuscarPedido.BackColor = System.Drawing.Color.Silver;
            this.txtBuscarPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarPedido.Location = new System.Drawing.Point(179, 14);
            this.txtBuscarPedido.Name = "txtBuscarPedido";
            this.txtBuscarPedido.Size = new System.Drawing.Size(135, 22);
            this.txtBuscarPedido.TabIndex = 16;
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.dtGridPedidos);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 80);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(721, 349);
            this.pnlContent.TabIndex = 43;
            // 
            // dtGridPedidos
            // 
            this.dtGridPedidos.AllowUserToAddRows = false;
            this.dtGridPedidos.BackgroundColor = System.Drawing.Color.White;
            this.dtGridPedidos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtGridPedidos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtGridPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridPedidos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Pedidos,
            this.Cliente,
            this.Agente,
            this.Folio,
            this.Direccion,
            this.Total,
            this.IdDocumento});
            this.dtGridPedidos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtGridPedidos.EnableHeadersVisualStyles = false;
            this.dtGridPedidos.GridColor = System.Drawing.Color.IndianRed;
            this.dtGridPedidos.Location = new System.Drawing.Point(0, 0);
            this.dtGridPedidos.Name = "dtGridPedidos";
            this.dtGridPedidos.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(199)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtGridPedidos.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dtGridPedidos.RowHeadersVisible = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(199)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Maiandra GD", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            this.dtGridPedidos.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dtGridPedidos.Size = new System.Drawing.Size(721, 349);
            this.dtGridPedidos.TabIndex = 1;
            // 
            // Pedidos
            // 
            this.Pedidos.FillWeight = 200F;
            this.Pedidos.HeaderText = "Pedidos";
            this.Pedidos.Name = "Pedidos";
            this.Pedidos.ReadOnly = true;
            this.Pedidos.Width = 80;
            // 
            // Cliente
            // 
            this.Cliente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Cliente.HeaderText = "Cliente";
            this.Cliente.Name = "Cliente";
            this.Cliente.ReadOnly = true;
            // 
            // Agente
            // 
            this.Agente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Agente.HeaderText = "Agente";
            this.Agente.Name = "Agente";
            this.Agente.ReadOnly = true;
            // 
            // Folio
            // 
            this.Folio.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Folio.HeaderText = "Folio";
            this.Folio.Name = "Folio";
            this.Folio.ReadOnly = true;
            // 
            // Direccion
            // 
            this.Direccion.HeaderText = "Dirección";
            this.Direccion.Name = "Direccion";
            this.Direccion.ReadOnly = true;
            this.Direccion.Width = 180;
            // 
            // Total
            // 
            this.Total.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            // 
            // IdDocumento
            // 
            this.IdDocumento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IdDocumento.HeaderText = "IdDocumento";
            this.IdDocumento.Name = "IdDocumento";
            this.IdDocumento.ReadOnly = true;
            this.IdDocumento.Visible = false;
            // 
            // frmPedidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(721, 429);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlIzq);
            this.Controls.Add(this.btnfrmArticulos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPedidos";
            this.Text = "P  E  D  I  D  O  S";
            this.Load += new System.EventHandler(this.frmPedidos_Load);
            this.pnlCerrar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCerrar)).EndInit();
            this.pnlIzq.ResumeLayout(false);
            this.pnlIzq.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtGridPedidos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnfrmArticulos;
        private System.Windows.Forms.Panel pnlCerrar;
        private System.Windows.Forms.PictureBox picCerrar;
        private System.Windows.Forms.Panel pnlIzq;
        private System.Windows.Forms.Button btnClienteLabel;
        private System.Windows.Forms.TextBox txtBuscarPedido;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.DataGridView dtGridPedidos;
        private System.Windows.Forms.DataGridViewImageColumn Pedidos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Agente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Folio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Direccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdDocumento;
    }
}