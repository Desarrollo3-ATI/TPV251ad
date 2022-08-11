
using SyncTPV.Helpers.Design;

namespace SyncTPV.Views
{
    partial class FormPayCart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPayCart));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelFirstSectionFrmPayCart = new System.Windows.Forms.Panel();
            this.panelVentaACredito = new System.Windows.Forms.Panel();
            this.checkBoxCreditoFrmPayCart = new System.Windows.Forms.CheckBox();
            this.textCreditoFrmPayCart = new System.Windows.Forms.Label();
            this.textInfoFcFrmPayCart = new System.Windows.Forms.Label();
            this.panelDvFcFrmPayCart = new System.Windows.Forms.Panel();
            this.imgSinDatosFrmPayCart = new System.Windows.Forms.PictureBox();
            this.dataGridViewFcFrmPayCArt = new System.Windows.Forms.DataGridView();
            this.idDgvFc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDgvFc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountDgvFc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelTerminarDocumento = new System.Windows.Forms.Panel();
            this.txtNombreAgente = new System.Windows.Forms.Label();
            this.textNumeroCopias = new System.Windows.Forms.Label();
            this.editNumeroCopias = new System.Windows.Forms.TextBox();
            this.btnObservacionesFrmPayCart = new System.Windows.Forms.Button();
            this.checkBoxCotizacionMostrador = new System.Windows.Forms.CheckBox();
            this.textCambioFrmPayCart = new System.Windows.Forms.Label();
            this.textPendienteFrmPayCart = new System.Windows.Forms.Label();
            this.textTotalFrmPayCart = new System.Windows.Forms.Label();
            this.textDescuentoFrmPayCart = new System.Windows.Forms.Label();
            this.textSubtotalFrmPayCart = new System.Windows.Forms.Label();
            this.textInfoCambioFrmPayCart = new System.Windows.Forms.Label();
            this.textInfoPendienteFrmPayCart = new System.Windows.Forms.Label();
            this.textInfoTotalFrmPayCart = new System.Windows.Forms.Label();
            this.textInfoDescuentoFrmPayCart = new System.Windows.Forms.Label();
            this.textInfoSubtotalFrmPayCart = new System.Windows.Forms.Label();
            this.btnAceptarFrmPayCart = new SyncTPV.RoundedButton();
            this.btnCancelFrmPayCart = new SyncTPV.Helpers.Design.CancelRoundedButton();
            this.panelGenerarFactura = new System.Windows.Forms.Panel();
            this.checkBoxInvoiceFrmPayCart = new System.Windows.Forms.CheckBox();
            this.textFacturaFrmPayCart = new System.Windows.Forms.Label();
            this.panelFirstSectionFrmPayCart.SuspendLayout();
            this.panelVentaACredito.SuspendLayout();
            this.panelDvFcFrmPayCart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatosFrmPayCart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFcFrmPayCArt)).BeginInit();
            this.panelTerminarDocumento.SuspendLayout();
            this.panelGenerarFactura.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelFirstSectionFrmPayCart
            // 
            this.panelFirstSectionFrmPayCart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFirstSectionFrmPayCart.Controls.Add(this.panelVentaACredito);
            this.panelFirstSectionFrmPayCart.Controls.Add(this.textInfoFcFrmPayCart);
            this.panelFirstSectionFrmPayCart.Controls.Add(this.panelDvFcFrmPayCart);
            this.panelFirstSectionFrmPayCart.Location = new System.Drawing.Point(42, -1);
            this.panelFirstSectionFrmPayCart.Name = "panelFirstSectionFrmPayCart";
            this.panelFirstSectionFrmPayCart.Size = new System.Drawing.Size(547, 219);
            this.panelFirstSectionFrmPayCart.TabIndex = 0;
            // 
            // panelVentaACredito
            // 
            this.panelVentaACredito.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelVentaACredito.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelVentaACredito.Controls.Add(this.checkBoxCreditoFrmPayCart);
            this.panelVentaACredito.Controls.Add(this.textCreditoFrmPayCart);
            this.panelVentaACredito.Location = new System.Drawing.Point(3, 3);
            this.panelVentaACredito.Name = "panelVentaACredito";
            this.panelVentaACredito.Size = new System.Drawing.Size(541, 41);
            this.panelVentaACredito.TabIndex = 9;
            // 
            // checkBoxCreditoFrmPayCart
            // 
            this.checkBoxCreditoFrmPayCart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxCreditoFrmPayCart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBoxCreditoFrmPayCart.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.checkBoxCreditoFrmPayCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxCreditoFrmPayCart.Location = new System.Drawing.Point(382, 8);
            this.checkBoxCreditoFrmPayCart.Name = "checkBoxCreditoFrmPayCart";
            this.checkBoxCreditoFrmPayCart.Size = new System.Drawing.Size(152, 23);
            this.checkBoxCreditoFrmPayCart.TabIndex = 6;
            this.checkBoxCreditoFrmPayCart.UseVisualStyleBackColor = true;
            this.checkBoxCreditoFrmPayCart.CheckedChanged += new System.EventHandler(this.checkBoxCreditoFrmPayCart_CheckedChanged);
            this.checkBoxCreditoFrmPayCart.KeyUp += new System.Windows.Forms.KeyEventHandler(this.checkBoxCreditoFrmPayCart_KeyUp);
            // 
            // textCreditoFrmPayCart
            // 
            this.textCreditoFrmPayCart.AutoSize = true;
            this.textCreditoFrmPayCart.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textCreditoFrmPayCart.Location = new System.Drawing.Point(86, 8);
            this.textCreditoFrmPayCart.Name = "textCreditoFrmPayCart";
            this.textCreditoFrmPayCart.Size = new System.Drawing.Size(176, 25);
            this.textCreditoFrmPayCart.TabIndex = 5;
            this.textCreditoFrmPayCart.Text = "Venta a Crédito";
            // 
            // textInfoFcFrmPayCart
            // 
            this.textInfoFcFrmPayCart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textInfoFcFrmPayCart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInfoFcFrmPayCart.Location = new System.Drawing.Point(6, 47);
            this.textInfoFcFrmPayCart.Name = "textInfoFcFrmPayCart";
            this.textInfoFcFrmPayCart.Size = new System.Drawing.Size(535, 23);
            this.textInfoFcFrmPayCart.TabIndex = 8;
            this.textInfoFcFrmPayCart.Text = "Seleccionar Forma de Cobro";
            this.textInfoFcFrmPayCart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelDvFcFrmPayCart
            // 
            this.panelDvFcFrmPayCart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDvFcFrmPayCart.Controls.Add(this.imgSinDatosFrmPayCart);
            this.panelDvFcFrmPayCart.Controls.Add(this.dataGridViewFcFrmPayCArt);
            this.panelDvFcFrmPayCart.Location = new System.Drawing.Point(3, 73);
            this.panelDvFcFrmPayCart.Name = "panelDvFcFrmPayCart";
            this.panelDvFcFrmPayCart.Size = new System.Drawing.Size(541, 142);
            this.panelDvFcFrmPayCart.TabIndex = 6;
            // 
            // imgSinDatosFrmPayCart
            // 
            this.imgSinDatosFrmPayCart.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.imgSinDatosFrmPayCart.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("imgSinDatosFrmPayCart.BackgroundImage")));
            this.imgSinDatosFrmPayCart.Location = new System.Drawing.Point(177, 33);
            this.imgSinDatosFrmPayCart.Name = "imgSinDatosFrmPayCart";
            this.imgSinDatosFrmPayCart.Size = new System.Drawing.Size(163, 99);
            this.imgSinDatosFrmPayCart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgSinDatosFrmPayCart.TabIndex = 1;
            this.imgSinDatosFrmPayCart.TabStop = false;
            // 
            // dataGridViewFcFrmPayCArt
            // 
            this.dataGridViewFcFrmPayCArt.AllowUserToAddRows = false;
            this.dataGridViewFcFrmPayCArt.AllowUserToDeleteRows = false;
            this.dataGridViewFcFrmPayCArt.AllowUserToResizeColumns = false;
            this.dataGridViewFcFrmPayCArt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewFcFrmPayCArt.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewFcFrmPayCArt.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewFcFrmPayCArt.BackgroundColor = System.Drawing.Color.Azure;
            this.dataGridViewFcFrmPayCArt.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.MintCream;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFcFrmPayCArt.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewFcFrmPayCArt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFcFrmPayCArt.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDgvFc,
            this.nameDgvFc,
            this.amountDgvFc});
            this.dataGridViewFcFrmPayCArt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dataGridViewFcFrmPayCArt.GridColor = System.Drawing.Color.LightSteelBlue;
            this.dataGridViewFcFrmPayCArt.Location = new System.Drawing.Point(3, 0);
            this.dataGridViewFcFrmPayCArt.MultiSelect = false;
            this.dataGridViewFcFrmPayCArt.Name = "dataGridViewFcFrmPayCArt";
            this.dataGridViewFcFrmPayCArt.ReadOnly = true;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.Honeydew;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFcFrmPayCArt.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewFcFrmPayCArt.RowHeadersVisible = false;
            this.dataGridViewFcFrmPayCArt.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewFcFrmPayCArt.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.dataGridViewFcFrmPayCArt.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFcFrmPayCArt.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFcFrmPayCArt.Size = new System.Drawing.Size(535, 139);
            this.dataGridViewFcFrmPayCArt.TabIndex = 0;
            this.dataGridViewFcFrmPayCArt.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFcFrmPayCArt_CellClick);
            this.dataGridViewFcFrmPayCArt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewFcFrmPayCArt_KeyDown);
            // 
            // idDgvFc
            // 
            this.idDgvFc.HeaderText = "Id";
            this.idDgvFc.Name = "idDgvFc";
            this.idDgvFc.ReadOnly = true;
            // 
            // nameDgvFc
            // 
            this.nameDgvFc.HeaderText = "Nombre";
            this.nameDgvFc.Name = "nameDgvFc";
            this.nameDgvFc.ReadOnly = true;
            // 
            // amountDgvFc
            // 
            this.amountDgvFc.HeaderText = "Importe";
            this.amountDgvFc.Name = "amountDgvFc";
            this.amountDgvFc.ReadOnly = true;
            // 
            // panelTerminarDocumento
            // 
            this.panelTerminarDocumento.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTerminarDocumento.Controls.Add(this.txtNombreAgente);
            this.panelTerminarDocumento.Controls.Add(this.textNumeroCopias);
            this.panelTerminarDocumento.Controls.Add(this.editNumeroCopias);
            this.panelTerminarDocumento.Controls.Add(this.btnObservacionesFrmPayCart);
            this.panelTerminarDocumento.Controls.Add(this.checkBoxCotizacionMostrador);
            this.panelTerminarDocumento.Controls.Add(this.textCambioFrmPayCart);
            this.panelTerminarDocumento.Controls.Add(this.textPendienteFrmPayCart);
            this.panelTerminarDocumento.Controls.Add(this.textTotalFrmPayCart);
            this.panelTerminarDocumento.Controls.Add(this.textDescuentoFrmPayCart);
            this.panelTerminarDocumento.Controls.Add(this.textSubtotalFrmPayCart);
            this.panelTerminarDocumento.Controls.Add(this.textInfoCambioFrmPayCart);
            this.panelTerminarDocumento.Controls.Add(this.textInfoPendienteFrmPayCart);
            this.panelTerminarDocumento.Controls.Add(this.textInfoTotalFrmPayCart);
            this.panelTerminarDocumento.Controls.Add(this.textInfoDescuentoFrmPayCart);
            this.panelTerminarDocumento.Controls.Add(this.textInfoSubtotalFrmPayCart);
            this.panelTerminarDocumento.Controls.Add(this.btnAceptarFrmPayCart);
            this.panelTerminarDocumento.Controls.Add(this.btnCancelFrmPayCart);
            this.panelTerminarDocumento.Location = new System.Drawing.Point(39, 266);
            this.panelTerminarDocumento.Name = "panelTerminarDocumento";
            this.panelTerminarDocumento.Size = new System.Drawing.Size(547, 234);
            this.panelTerminarDocumento.TabIndex = 2;
            // 
            // txtNombreAgente
            // 
            this.txtNombreAgente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNombreAgente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtNombreAgente.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreAgente.ForeColor = System.Drawing.Color.MediumBlue;
            this.txtNombreAgente.Location = new System.Drawing.Point(180, 185);
            this.txtNombreAgente.Name = "txtNombreAgente";
            this.txtNombreAgente.Size = new System.Drawing.Size(181, 17);
            this.txtNombreAgente.TabIndex = 24;
            this.txtNombreAgente.Text = "Agente: ";
            this.txtNombreAgente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textNumeroCopias
            // 
            this.textNumeroCopias.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textNumeroCopias.AutoSize = true;
            this.textNumeroCopias.Location = new System.Drawing.Point(207, 208);
            this.textNumeroCopias.Name = "textNumeroCopias";
            this.textNumeroCopias.Size = new System.Drawing.Size(72, 13);
            this.textNumeroCopias.TabIndex = 23;
            this.textNumeroCopias.Text = "Copias Ticket";
            // 
            // editNumeroCopias
            // 
            this.editNumeroCopias.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editNumeroCopias.Location = new System.Drawing.Point(285, 205);
            this.editNumeroCopias.Name = "editNumeroCopias";
            this.editNumeroCopias.Size = new System.Drawing.Size(52, 20);
            this.editNumeroCopias.TabIndex = 22;
            this.editNumeroCopias.Text = "1";
            this.editNumeroCopias.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.editNumeroCopias.TextChanged += new System.EventHandler(this.editNumeroCopias_TextChanged);
            this.editNumeroCopias.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editNumeroCopias_KeyPress);
            this.editNumeroCopias.KeyUp += new System.Windows.Forms.KeyEventHandler(this.editNumeroCopias_KeyUp);
            // 
            // btnObservacionesFrmPayCart
            // 
            this.btnObservacionesFrmPayCart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnObservacionesFrmPayCart.BackColor = System.Drawing.Color.White;
            this.btnObservacionesFrmPayCart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnObservacionesFrmPayCart.Location = new System.Drawing.Point(6, 3);
            this.btnObservacionesFrmPayCart.Name = "btnObservacionesFrmPayCart";
            this.btnObservacionesFrmPayCart.Size = new System.Drawing.Size(535, 36);
            this.btnObservacionesFrmPayCart.TabIndex = 21;
            this.btnObservacionesFrmPayCart.Text = "Observación o Comentario";
            this.btnObservacionesFrmPayCart.UseVisualStyleBackColor = false;
            this.btnObservacionesFrmPayCart.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnObservacionesFrmPayCart_KeyUp);
            // 
            // checkBoxCotizacionMostrador
            // 
            this.checkBoxCotizacionMostrador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxCotizacionMostrador.AutoSize = true;
            this.checkBoxCotizacionMostrador.Checked = true;
            this.checkBoxCotizacionMostrador.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCotizacionMostrador.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBoxCotizacionMostrador.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxCotizacionMostrador.ForeColor = System.Drawing.Color.SaddleBrown;
            this.checkBoxCotizacionMostrador.Location = new System.Drawing.Point(171, 160);
            this.checkBoxCotizacionMostrador.Name = "checkBoxCotizacionMostrador";
            this.checkBoxCotizacionMostrador.Size = new System.Drawing.Size(191, 22);
            this.checkBoxCotizacionMostrador.TabIndex = 20;
            this.checkBoxCotizacionMostrador.Text = "Cotización Mostrador";
            this.checkBoxCotizacionMostrador.UseVisualStyleBackColor = true;
            this.checkBoxCotizacionMostrador.KeyUp += new System.Windows.Forms.KeyEventHandler(this.checkBoxCotizacionMostrador_KeyUp);
            // 
            // textCambioFrmPayCart
            // 
            this.textCambioFrmPayCart.AutoSize = true;
            this.textCambioFrmPayCart.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textCambioFrmPayCart.Location = new System.Drawing.Point(367, 139);
            this.textCambioFrmPayCart.Name = "textCambioFrmPayCart";
            this.textCambioFrmPayCart.Size = new System.Drawing.Size(73, 18);
            this.textCambioFrmPayCart.TabIndex = 19;
            this.textCambioFrmPayCart.Text = "$ 0 MXN";
            // 
            // textPendienteFrmPayCart
            // 
            this.textPendienteFrmPayCart.AutoSize = true;
            this.textPendienteFrmPayCart.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textPendienteFrmPayCart.Location = new System.Drawing.Point(367, 113);
            this.textPendienteFrmPayCart.Name = "textPendienteFrmPayCart";
            this.textPendienteFrmPayCart.Size = new System.Drawing.Size(73, 18);
            this.textPendienteFrmPayCart.TabIndex = 18;
            this.textPendienteFrmPayCart.Text = "$ 0 MXN";
            // 
            // textTotalFrmPayCart
            // 
            this.textTotalFrmPayCart.AutoSize = true;
            this.textTotalFrmPayCart.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTotalFrmPayCart.Location = new System.Drawing.Point(367, 87);
            this.textTotalFrmPayCart.Name = "textTotalFrmPayCart";
            this.textTotalFrmPayCart.Size = new System.Drawing.Size(73, 18);
            this.textTotalFrmPayCart.TabIndex = 17;
            this.textTotalFrmPayCart.Text = "$ 0 MXN";
            // 
            // textDescuentoFrmPayCart
            // 
            this.textDescuentoFrmPayCart.AutoSize = true;
            this.textDescuentoFrmPayCart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textDescuentoFrmPayCart.Location = new System.Drawing.Point(387, 65);
            this.textDescuentoFrmPayCart.Name = "textDescuentoFrmPayCart";
            this.textDescuentoFrmPayCart.Size = new System.Drawing.Size(49, 13);
            this.textDescuentoFrmPayCart.TabIndex = 16;
            this.textDescuentoFrmPayCart.Text = "$ 0 MXN";
            // 
            // textSubtotalFrmPayCart
            // 
            this.textSubtotalFrmPayCart.AutoSize = true;
            this.textSubtotalFrmPayCart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textSubtotalFrmPayCart.Location = new System.Drawing.Point(387, 42);
            this.textSubtotalFrmPayCart.Name = "textSubtotalFrmPayCart";
            this.textSubtotalFrmPayCart.Size = new System.Drawing.Size(49, 13);
            this.textSubtotalFrmPayCart.TabIndex = 15;
            this.textSubtotalFrmPayCart.Text = "$ 0 MXN";
            // 
            // textInfoCambioFrmPayCart
            // 
            this.textInfoCambioFrmPayCart.AutoSize = true;
            this.textInfoCambioFrmPayCart.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInfoCambioFrmPayCart.Location = new System.Drawing.Point(96, 139);
            this.textInfoCambioFrmPayCart.Name = "textInfoCambioFrmPayCart";
            this.textInfoCambioFrmPayCart.Size = new System.Drawing.Size(66, 18);
            this.textInfoCambioFrmPayCart.TabIndex = 14;
            this.textInfoCambioFrmPayCart.Text = "Cambio";
            // 
            // textInfoPendienteFrmPayCart
            // 
            this.textInfoPendienteFrmPayCart.AutoSize = true;
            this.textInfoPendienteFrmPayCart.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInfoPendienteFrmPayCart.Location = new System.Drawing.Point(96, 113);
            this.textInfoPendienteFrmPayCart.Name = "textInfoPendienteFrmPayCart";
            this.textInfoPendienteFrmPayCart.Size = new System.Drawing.Size(82, 18);
            this.textInfoPendienteFrmPayCart.TabIndex = 13;
            this.textInfoPendienteFrmPayCart.Text = "Pendiente";
            // 
            // textInfoTotalFrmPayCart
            // 
            this.textInfoTotalFrmPayCart.AutoSize = true;
            this.textInfoTotalFrmPayCart.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInfoTotalFrmPayCart.Location = new System.Drawing.Point(97, 87);
            this.textInfoTotalFrmPayCart.Name = "textInfoTotalFrmPayCart";
            this.textInfoTotalFrmPayCart.Size = new System.Drawing.Size(46, 18);
            this.textInfoTotalFrmPayCart.TabIndex = 12;
            this.textInfoTotalFrmPayCart.Text = "Total";
            // 
            // textInfoDescuentoFrmPayCart
            // 
            this.textInfoDescuentoFrmPayCart.AutoSize = true;
            this.textInfoDescuentoFrmPayCart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInfoDescuentoFrmPayCart.Location = new System.Drawing.Point(96, 65);
            this.textInfoDescuentoFrmPayCart.Name = "textInfoDescuentoFrmPayCart";
            this.textInfoDescuentoFrmPayCart.Size = new System.Drawing.Size(59, 13);
            this.textInfoDescuentoFrmPayCart.TabIndex = 11;
            this.textInfoDescuentoFrmPayCart.Text = "Descuento";
            // 
            // textInfoSubtotalFrmPayCart
            // 
            this.textInfoSubtotalFrmPayCart.AutoSize = true;
            this.textInfoSubtotalFrmPayCart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInfoSubtotalFrmPayCart.Location = new System.Drawing.Point(96, 42);
            this.textInfoSubtotalFrmPayCart.Name = "textInfoSubtotalFrmPayCart";
            this.textInfoSubtotalFrmPayCart.Size = new System.Drawing.Size(46, 13);
            this.textInfoSubtotalFrmPayCart.TabIndex = 10;
            this.textInfoSubtotalFrmPayCart.Text = "Subtotal";
            // 
            // btnAceptarFrmPayCart
            // 
            this.btnAceptarFrmPayCart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptarFrmPayCart.BackColor = System.Drawing.Color.Transparent;
            this.btnAceptarFrmPayCart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptarFrmPayCart.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnAceptarFrmPayCart.FlatAppearance.BorderSize = 2;
            this.btnAceptarFrmPayCart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnAceptarFrmPayCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptarFrmPayCart.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptarFrmPayCart.Location = new System.Drawing.Point(367, 188);
            this.btnAceptarFrmPayCart.Name = "btnAceptarFrmPayCart";
            this.btnAceptarFrmPayCart.Size = new System.Drawing.Size(177, 43);
            this.btnAceptarFrmPayCart.TabIndex = 1;
            this.btnAceptarFrmPayCart.Text = "Terminar (F10)";
            this.btnAceptarFrmPayCart.UseVisualStyleBackColor = false;
            this.btnAceptarFrmPayCart.Click += new System.EventHandler(this.btnAceptarFrmPayCart_Click);
            this.btnAceptarFrmPayCart.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnAceptarFrmPayCart_KeyUp);
            // 
            // btnCancelFrmPayCart
            // 
            this.btnCancelFrmPayCart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelFrmPayCart.BackColor = System.Drawing.Color.Transparent;
            this.btnCancelFrmPayCart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelFrmPayCart.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnCancelFrmPayCart.FlatAppearance.BorderSize = 2;
            this.btnCancelFrmPayCart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MistyRose;
            this.btnCancelFrmPayCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelFrmPayCart.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelFrmPayCart.Location = new System.Drawing.Point(3, 188);
            this.btnCancelFrmPayCart.Name = "btnCancelFrmPayCart";
            this.btnCancelFrmPayCart.Size = new System.Drawing.Size(171, 43);
            this.btnCancelFrmPayCart.TabIndex = 0;
            this.btnCancelFrmPayCart.Text = "Cancelar";
            this.btnCancelFrmPayCart.UseVisualStyleBackColor = false;
            this.btnCancelFrmPayCart.Click += new System.EventHandler(this.btnCancelFrmPayCart_Click);
            this.btnCancelFrmPayCart.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnCancelFrmPayCart_KeyUp);
            // 
            // panelGenerarFactura
            // 
            this.panelGenerarFactura.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelGenerarFactura.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelGenerarFactura.Controls.Add(this.checkBoxInvoiceFrmPayCart);
            this.panelGenerarFactura.Controls.Add(this.textFacturaFrmPayCart);
            this.panelGenerarFactura.Location = new System.Drawing.Point(39, 222);
            this.panelGenerarFactura.Name = "panelGenerarFactura";
            this.panelGenerarFactura.Size = new System.Drawing.Size(547, 41);
            this.panelGenerarFactura.TabIndex = 11;
            // 
            // checkBoxInvoiceFrmPayCart
            // 
            this.checkBoxInvoiceFrmPayCart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxInvoiceFrmPayCart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBoxInvoiceFrmPayCart.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.checkBoxInvoiceFrmPayCart.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxInvoiceFrmPayCart.Location = new System.Drawing.Point(349, 9);
            this.checkBoxInvoiceFrmPayCart.Name = "checkBoxInvoiceFrmPayCart";
            this.checkBoxInvoiceFrmPayCart.Size = new System.Drawing.Size(188, 25);
            this.checkBoxInvoiceFrmPayCart.TabIndex = 7;
            this.checkBoxInvoiceFrmPayCart.Text = "No";
            this.checkBoxInvoiceFrmPayCart.UseVisualStyleBackColor = true;
            this.checkBoxInvoiceFrmPayCart.KeyUp += new System.Windows.Forms.KeyEventHandler(this.checkBoxInvoiceFrmPayCart_KeyUp);
            // 
            // textFacturaFrmPayCart
            // 
            this.textFacturaFrmPayCart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.textFacturaFrmPayCart.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textFacturaFrmPayCart.Location = new System.Drawing.Point(62, 9);
            this.textFacturaFrmPayCart.Name = "textFacturaFrmPayCart";
            this.textFacturaFrmPayCart.Size = new System.Drawing.Size(251, 25);
            this.textFacturaFrmPayCart.TabIndex = 6;
            this.textFacturaFrmPayCart.Text = "Generar Factura";
            // 
            // FormPayCart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(631, 502);
            this.Controls.Add(this.panelGenerarFactura);
            this.Controls.Add(this.panelTerminarDocumento);
            this.Controls.Add(this.panelFirstSectionFrmPayCart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormPayCart";
            this.Text = "FrmPayCart";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPayCart_FormClosing);
            this.Load += new System.EventHandler(this.FrmPayCart_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmPayCart_KeyUp);
            this.panelFirstSectionFrmPayCart.ResumeLayout(false);
            this.panelVentaACredito.ResumeLayout(false);
            this.panelVentaACredito.PerformLayout();
            this.panelDvFcFrmPayCart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgSinDatosFrmPayCart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFcFrmPayCArt)).EndInit();
            this.panelTerminarDocumento.ResumeLayout(false);
            this.panelTerminarDocumento.PerformLayout();
            this.panelGenerarFactura.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelFirstSectionFrmPayCart;
        private System.Windows.Forms.Panel panelTerminarDocumento;
        private System.Windows.Forms.Panel panelDvFcFrmPayCart;
        private System.Windows.Forms.DataGridView dataGridViewFcFrmPayCArt;
        private System.Windows.Forms.PictureBox imgSinDatosFrmPayCart;
        private System.Windows.Forms.Label textCambioFrmPayCart;
        private System.Windows.Forms.Label textPendienteFrmPayCart;
        private System.Windows.Forms.Label textTotalFrmPayCart;
        private System.Windows.Forms.Label textDescuentoFrmPayCart;
        private System.Windows.Forms.Label textSubtotalFrmPayCart;
        private System.Windows.Forms.Label textInfoCambioFrmPayCart;
        private System.Windows.Forms.Label textInfoPendienteFrmPayCart;
        private System.Windows.Forms.Label textInfoTotalFrmPayCart;
        private System.Windows.Forms.Label textInfoDescuentoFrmPayCart;
        private System.Windows.Forms.Label textInfoSubtotalFrmPayCart;
        private System.Windows.Forms.Label textInfoFcFrmPayCart;
        private System.Windows.Forms.Panel panelVentaACredito;
        private System.Windows.Forms.CheckBox checkBoxCreditoFrmPayCart;
        private System.Windows.Forms.Label textCreditoFrmPayCart;
        private System.Windows.Forms.CheckBox checkBoxCotizacionMostrador;
        private System.Windows.Forms.Button btnObservacionesFrmPayCart;
        private System.Windows.Forms.Panel panelGenerarFactura;
        private System.Windows.Forms.CheckBox checkBoxInvoiceFrmPayCart;
        private RoundedButton btnAceptarFrmPayCart;
        private CancelRoundedButton btnCancelFrmPayCart;
        private System.Windows.Forms.Label textFacturaFrmPayCart;
        private System.Windows.Forms.Label textNumeroCopias;
        private System.Windows.Forms.TextBox editNumeroCopias;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDgvFc;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDgvFc;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountDgvFc;
        public System.Windows.Forms.Label txtNombreAgente;
    }
}