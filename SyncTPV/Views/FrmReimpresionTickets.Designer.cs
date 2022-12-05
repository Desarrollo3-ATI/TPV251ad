
using SyncTPV.Helpers.Design;

namespace SyncTPV.Views
{
    partial class FrmReimpresionTickets
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReimpresionTickets));
            this.FechaMaxima = new System.Windows.Forms.DateTimePicker();
            this.gridTickets = new System.Windows.Forms.DataGridView();
            this.GridId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridReferencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridAgente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridEstatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridImprimir = new System.Windows.Forms.DataGridViewButtonColumn();
            this.GridTicket = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textUser = new System.Windows.Forms.Label();
            this.ComboAgentes = new System.Windows.Forms.ComboBox();
            this.comboReferencia = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboTiposDocumentos = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chckUsarFecha = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TextTotal = new System.Windows.Forms.Label();
            this.FechaTickets = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.comboLimite = new System.Windows.Forms.TextBox();
            this.btnBuscarReportes = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridTickets)).BeginInit();
            this.SuspendLayout();
            // 
            // FechaMaxima
            // 
            this.FechaMaxima.Location = new System.Drawing.Point(98, 41);
            this.FechaMaxima.Name = "FechaMaxima";
            this.FechaMaxima.Size = new System.Drawing.Size(169, 20);
            this.FechaMaxima.TabIndex = 68;
            this.FechaMaxima.Value = new System.DateTime(2022, 10, 31, 14, 17, 49, 0);
            this.FechaMaxima.ValueChanged += new System.EventHandler(this.FechaTickets_ValueChanged);
            // 
            // gridTickets
            // 
            this.gridTickets.AllowUserToAddRows = false;
            this.gridTickets.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridTickets.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridTickets.BackgroundColor = System.Drawing.Color.FloralWhite;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(1);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridTickets.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridTickets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTickets.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GridId,
            this.GridReferencia,
            this.GridAgente,
            this.GridTipo,
            this.GridFecha,
            this.GridEstatus,
            this.GridImprimir,
            this.GridTicket});
            this.gridTickets.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gridTickets.GridColor = System.Drawing.Color.Azure;
            this.gridTickets.Location = new System.Drawing.Point(0, 109);
            this.gridTickets.Name = "gridTickets";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridTickets.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridTickets.RowHeadersVisible = false;
            this.gridTickets.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.gridTickets.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.Azure;
            this.gridTickets.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridTickets.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(2);
            this.gridTickets.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridTickets.Size = new System.Drawing.Size(684, 337);
            this.gridTickets.TabIndex = 69;
            this.gridTickets.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridItems_CellClick);
            // 
            // GridId
            // 
            this.GridId.HeaderText = "Id";
            this.GridId.Name = "GridId";
            this.GridId.Width = 47;
            // 
            // GridReferencia
            // 
            this.GridReferencia.HeaderText = "Referencia";
            this.GridReferencia.Name = "GridReferencia";
            this.GridReferencia.Width = 110;
            // 
            // GridAgente
            // 
            this.GridAgente.HeaderText = "Agente";
            this.GridAgente.Name = "GridAgente";
            this.GridAgente.Width = 83;
            // 
            // GridTipo
            // 
            this.GridTipo.HeaderText = "Tipo";
            this.GridTipo.Name = "GridTipo";
            this.GridTipo.Width = 66;
            // 
            // GridFecha
            // 
            this.GridFecha.HeaderText = "Fecha";
            this.GridFecha.Name = "GridFecha";
            this.GridFecha.Width = 77;
            // 
            // GridEstatus
            // 
            this.GridEstatus.HeaderText = "Estatus";
            this.GridEstatus.Name = "GridEstatus";
            this.GridEstatus.Width = 85;
            // 
            // GridImprimir
            // 
            this.GridImprimir.HeaderText = "Imprimir";
            this.GridImprimir.Name = "GridImprimir";
            this.GridImprimir.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.GridImprimir.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.GridImprimir.Width = 89;
            // 
            // GridTicket
            // 
            this.GridTicket.HeaderText = "Ticket";
            this.GridTicket.Name = "GridTicket";
            this.GridTicket.Visible = false;
            this.GridTicket.Width = 77;
            // 
            // textUser
            // 
            this.textUser.AutoSize = true;
            this.textUser.Location = new System.Drawing.Point(291, 41);
            this.textUser.Name = "textUser";
            this.textUser.Size = new System.Drawing.Size(41, 13);
            this.textUser.TabIndex = 73;
            this.textUser.Text = "Agente";
            // 
            // ComboAgentes
            // 
            this.ComboAgentes.FormattingEnabled = true;
            this.ComboAgentes.Location = new System.Drawing.Point(338, 38);
            this.ComboAgentes.Name = "ComboAgentes";
            this.ComboAgentes.Size = new System.Drawing.Size(198, 21);
            this.ComboAgentes.TabIndex = 72;
            this.ComboAgentes.SelectedIndexChanged += new System.EventHandler(this.ComboAgentes_SelectedIndexChanged);
            // 
            // comboReferencia
            // 
            this.comboReferencia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboReferencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboReferencia.Location = new System.Drawing.Point(338, 11);
            this.comboReferencia.Name = "comboReferencia";
            this.comboReferencia.Size = new System.Drawing.Size(198, 21);
            this.comboReferencia.TabIndex = 74;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(273, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 75;
            this.label1.Text = "Referencia";
            // 
            // comboTiposDocumentos
            // 
            this.comboTiposDocumentos.FormattingEnabled = true;
            this.comboTiposDocumentos.Location = new System.Drawing.Point(336, 69);
            this.comboTiposDocumentos.Name = "comboTiposDocumentos";
            this.comboTiposDocumentos.Size = new System.Drawing.Size(200, 21);
            this.comboTiposDocumentos.TabIndex = 76;
            this.comboTiposDocumentos.SelectedIndexChanged += new System.EventHandler(this.comboTiposDocumentos_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(302, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 79;
            this.label3.Text = "Tipo";
            // 
            // chckUsarFecha
            // 
            this.chckUsarFecha.AutoSize = true;
            this.chckUsarFecha.Location = new System.Drawing.Point(4, 29);
            this.chckUsarFecha.Name = "chckUsarFecha";
            this.chckUsarFecha.Size = new System.Drawing.Size(61, 17);
            this.chckUsarFecha.TabIndex = 80;
            this.chckUsarFecha.Text = "Fechas";
            this.chckUsarFecha.UseVisualStyleBackColor = true;
            this.chckUsarFecha.CheckedChanged += new System.EventHandler(this.chckBDlocal_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(3, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 81;
            this.label2.Text = "Limite de consulta: ";
            // 
            // TextTotal
            // 
            this.TextTotal.AutoSize = true;
            this.TextTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextTotal.Location = new System.Drawing.Point(549, 69);
            this.TextTotal.Name = "TextTotal";
            this.TextTotal.Size = new System.Drawing.Size(79, 24);
            this.TextTotal.TabIndex = 82;
            this.TextTotal.Text = "Total: 0";
            // 
            // FechaTickets
            // 
            this.FechaTickets.Location = new System.Drawing.Point(98, 10);
            this.FechaTickets.Name = "FechaTickets";
            this.FechaTickets.Size = new System.Drawing.Size(169, 20);
            this.FechaTickets.TabIndex = 83;
            this.FechaTickets.Value = new System.DateTime(2022, 10, 31, 14, 17, 49, 0);
            this.FechaTickets.ValueChanged += new System.EventHandler(this.FechaMaxima_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(64, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 84;
            this.label4.Text = "Inicio";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(71, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 85;
            this.label5.Text = "Fin";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(186, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 87;
            this.label6.Text = "registros";
            // 
            // toolTip1
            // 
            this.toolTip1.Tag = "Sin limite: usar 0";
            // 
            // comboLimite
            // 
            this.comboLimite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboLimite.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboLimite.Location = new System.Drawing.Point(98, 72);
            this.comboLimite.Name = "comboLimite";
            this.comboLimite.Size = new System.Drawing.Size(82, 22);
            this.comboLimite.TabIndex = 88;
            this.comboLimite.Text = "1000";
            this.comboLimite.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnBuscarReportes
            // 
            this.btnBuscarReportes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscarReportes.BackColor = System.Drawing.Color.LightGray;
            this.btnBuscarReportes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarReportes.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnBuscarReportes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnBuscarReportes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarReportes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarReportes.Location = new System.Drawing.Point(553, 13);
            this.btnBuscarReportes.Name = "btnBuscarReportes";
            this.btnBuscarReportes.Size = new System.Drawing.Size(119, 46);
            this.btnBuscarReportes.TabIndex = 70;
            this.btnBuscarReportes.Text = "Buscar tikets servidor";
            this.btnBuscarReportes.UseVisualStyleBackColor = false;
            this.btnBuscarReportes.Click += new System.EventHandler(this.btnBuscarReportes_Click);
            // 
            // FrmReimpresionTickets
            // 
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Coral;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(684, 446);
            this.Controls.Add(this.comboLimite);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.FechaTickets);
            this.Controls.Add(this.TextTotal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chckUsarFecha);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboTiposDocumentos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboReferencia);
            this.Controls.Add(this.textUser);
            this.Controls.Add(this.ComboAgentes);
            this.Controls.Add(this.btnBuscarReportes);
            this.Controls.Add(this.gridTickets);
            this.Controls.Add(this.FechaMaxima);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmReimpresionTickets";
            this.Text = "Historial de Tickets (Servidor)";
            this.Load += new System.EventHandler(this.FrmConfirmation_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.gridTickets)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label textMsgFrmConfirmation;
        private System.Windows.Forms.ProgressBar progressBarFrmConfirmation;
        private RoundedButton btnOkFrmConfirmation;
        private CancelRoundedButton btnCancelFrmConfirmation;
        private System.Windows.Forms.DateTimePicker FechaMaxima;
        private System.Windows.Forms.DataGridView gridTickets;
        private System.Windows.Forms.Button btnBuscarReportes;
        private System.Windows.Forms.Label textUser;
        private System.Windows.Forms.ComboBox ComboAgentes;
        private System.Windows.Forms.TextBox comboReferencia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboTiposDocumentos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chckUsarFecha;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label TextTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridId;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridReferencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridAgente;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridEstatus;
        private System.Windows.Forms.DataGridViewButtonColumn GridImprimir;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridTicket;
        private System.Windows.Forms.DateTimePicker FechaTickets;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox comboLimite;
    }
}