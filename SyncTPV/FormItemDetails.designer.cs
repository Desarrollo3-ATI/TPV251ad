namespace SyncTPV
{
    partial class FormItemDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormItemDetails));
            this.pictureArticulo = new System.Windows.Forms.PictureBox();
            this.tabControlItemDetail = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textProductFiscalNoFiscal = new System.Windows.Forms.Label();
            this.progressBarLoadItemDetail = new System.Windows.Forms.ProgressBar();
            this.textSalesUnit = new System.Windows.Forms.Label();
            this.textPurchaseUnit = new System.Windows.Forms.Label();
            this.textNonConvertibleUnit = new System.Windows.Forms.Label();
            this.textBaseUnit = new System.Windows.Forms.Label();
            this.textPreciosItem = new System.Windows.Forms.Label();
            this.btnUploadImg = new System.Windows.Forms.Button();
            this.editDescuentoItem = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.editExistenciaItem = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.editNombreItem = new System.Windows.Forms.TextBox();
            this.textNombreItem = new System.Windows.Forms.Label();
            this.editCodigoItem = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.openFileDialogSearchItemImage = new System.Windows.Forms.OpenFileDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureArticulo)).BeginInit();
            this.tabControlItemDetail.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureArticulo
            // 
            this.pictureArticulo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureArticulo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureArticulo.Image = ((System.Drawing.Image)(resources.GetObject("pictureArticulo.Image")));
            this.pictureArticulo.Location = new System.Drawing.Point(21, 17);
            this.pictureArticulo.Name = "pictureArticulo";
            this.pictureArticulo.Size = new System.Drawing.Size(162, 147);
            this.pictureArticulo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureArticulo.TabIndex = 16;
            this.pictureArticulo.TabStop = false;
            this.pictureArticulo.Click += new System.EventHandler(this.pictureArticulo_Click);
            // 
            // tabControlItemDetail
            // 
            this.tabControlItemDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlItemDetail.Controls.Add(this.tabPage2);
            this.tabControlItemDetail.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlItemDetail.Location = new System.Drawing.Point(12, 12);
            this.tabControlItemDetail.Name = "tabControlItemDetail";
            this.tabControlItemDetail.SelectedIndex = 0;
            this.tabControlItemDetail.Size = new System.Drawing.Size(848, 560);
            this.tabControlItemDetail.TabIndex = 37;
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.textProductFiscalNoFiscal);
            this.tabPage2.Controls.Add(this.progressBarLoadItemDetail);
            this.tabPage2.Controls.Add(this.textSalesUnit);
            this.tabPage2.Controls.Add(this.textPurchaseUnit);
            this.tabPage2.Controls.Add(this.textNonConvertibleUnit);
            this.tabPage2.Controls.Add(this.textBaseUnit);
            this.tabPage2.Controls.Add(this.textPreciosItem);
            this.tabPage2.Controls.Add(this.btnUploadImg);
            this.tabPage2.Controls.Add(this.editDescuentoItem);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.editExistenciaItem);
            this.tabPage2.Controls.Add(this.pictureArticulo);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.editNombreItem);
            this.tabPage2.Controls.Add(this.textNombreItem);
            this.tabPage2.Controls.Add(this.editCodigoItem);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(840, 533);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Datos";
            // 
            // textProductFiscalNoFiscal
            // 
            this.textProductFiscalNoFiscal.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textProductFiscalNoFiscal.Location = new System.Drawing.Point(657, 140);
            this.textProductFiscalNoFiscal.Name = "textProductFiscalNoFiscal";
            this.textProductFiscalNoFiscal.Size = new System.Drawing.Size(136, 35);
            this.textProductFiscalNoFiscal.TabIndex = 26;
            this.textProductFiscalNoFiscal.Text = "Fiscal";
            // 
            // progressBarLoadItemDetail
            // 
            this.progressBarLoadItemDetail.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.progressBarLoadItemDetail.Location = new System.Drawing.Point(3, 518);
            this.progressBarLoadItemDetail.Name = "progressBarLoadItemDetail";
            this.progressBarLoadItemDetail.Size = new System.Drawing.Size(172, 10);
            this.progressBarLoadItemDetail.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBarLoadItemDetail.TabIndex = 24;
            this.progressBarLoadItemDetail.Visible = false;
            // 
            // textSalesUnit
            // 
            this.textSalesUnit.AutoSize = true;
            this.textSalesUnit.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textSalesUnit.Location = new System.Drawing.Point(18, 374);
            this.textSalesUnit.Name = "textSalesUnit";
            this.textSalesUnit.Size = new System.Drawing.Size(103, 15);
            this.textSalesUnit.TabIndex = 23;
            this.textSalesUnit.Text = "Unidad De Venta";
            // 
            // textPurchaseUnit
            // 
            this.textPurchaseUnit.AutoSize = true;
            this.textPurchaseUnit.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textPurchaseUnit.Location = new System.Drawing.Point(18, 325);
            this.textPurchaseUnit.Name = "textPurchaseUnit";
            this.textPurchaseUnit.Size = new System.Drawing.Size(115, 15);
            this.textPurchaseUnit.TabIndex = 22;
            this.textPurchaseUnit.Text = "Unidad De Compra";
            // 
            // textNonConvertibleUnit
            // 
            this.textNonConvertibleUnit.AutoSize = true;
            this.textNonConvertibleUnit.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textNonConvertibleUnit.Location = new System.Drawing.Point(18, 280);
            this.textNonConvertibleUnit.Name = "textNonConvertibleUnit";
            this.textNonConvertibleUnit.Size = new System.Drawing.Size(138, 15);
            this.textNonConvertibleUnit.TabIndex = 21;
            this.textNonConvertibleUnit.Text = "Unidad No Convertible";
            // 
            // textBaseUnit
            // 
            this.textBaseUnit.AutoSize = true;
            this.textBaseUnit.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBaseUnit.Location = new System.Drawing.Point(18, 239);
            this.textBaseUnit.Name = "textBaseUnit";
            this.textBaseUnit.Size = new System.Drawing.Size(80, 15);
            this.textBaseUnit.TabIndex = 20;
            this.textBaseUnit.Text = "Unidad Base";
            // 
            // textPreciosItem
            // 
            this.textPreciosItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textPreciosItem.AutoSize = true;
            this.textPreciosItem.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textPreciosItem.Location = new System.Drawing.Point(519, 239);
            this.textPreciosItem.Name = "textPreciosItem";
            this.textPreciosItem.Size = new System.Drawing.Size(52, 15);
            this.textPreciosItem.TabIndex = 18;
            this.textPreciosItem.Text = "Precios";
            this.textPreciosItem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnUploadImg
            // 
            this.btnUploadImg.BackColor = System.Drawing.Color.Transparent;
            this.btnUploadImg.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnUploadImg.FlatAppearance.BorderSize = 2;
            this.btnUploadImg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUploadImg.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUploadImg.Image = ((System.Drawing.Image)(resources.GetObject("btnUploadImg.Image")));
            this.btnUploadImg.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUploadImg.Location = new System.Drawing.Point(43, 170);
            this.btnUploadImg.Name = "btnUploadImg";
            this.btnUploadImg.Size = new System.Drawing.Size(113, 26);
            this.btnUploadImg.TabIndex = 17;
            this.btnUploadImg.Text = "Subir Imagen";
            this.btnUploadImg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUploadImg.UseVisualStyleBackColor = false;
            this.btnUploadImg.Click += new System.EventHandler(this.btnUploadImg_Click);
            // 
            // editDescuentoItem
            // 
            this.editDescuentoItem.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.editDescuentoItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editDescuentoItem.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editDescuentoItem.Location = new System.Drawing.Point(657, 109);
            this.editDescuentoItem.Multiline = true;
            this.editDescuentoItem.Name = "editDescuentoItem";
            this.editDescuentoItem.ReadOnly = true;
            this.editDescuentoItem.Size = new System.Drawing.Size(136, 24);
            this.editDescuentoItem.TabIndex = 12;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(654, 89);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(118, 15);
            this.label14.TabIndex = 11;
            this.label14.Text = "Descuento Maximo";
            // 
            // editExistenciaItem
            // 
            this.editExistenciaItem.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.editExistenciaItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editExistenciaItem.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editExistenciaItem.Location = new System.Drawing.Point(216, 109);
            this.editExistenciaItem.Multiline = true;
            this.editExistenciaItem.Name = "editExistenciaItem";
            this.editExistenciaItem.ReadOnly = true;
            this.editExistenciaItem.Size = new System.Drawing.Size(367, 66);
            this.editExistenciaItem.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(339, 89);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(75, 15);
            this.label12.TabIndex = 7;
            this.label12.Text = "Existencias";
            // 
            // editNombreItem
            // 
            this.editNombreItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editNombreItem.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.editNombreItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editNombreItem.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editNombreItem.Location = new System.Drawing.Point(367, 38);
            this.editNombreItem.Multiline = true;
            this.editNombreItem.Name = "editNombreItem";
            this.editNombreItem.ReadOnly = true;
            this.editNombreItem.Size = new System.Drawing.Size(467, 24);
            this.editNombreItem.TabIndex = 4;
            // 
            // textNombreItem
            // 
            this.textNombreItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textNombreItem.AutoSize = true;
            this.textNombreItem.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textNombreItem.Location = new System.Drawing.Point(519, 18);
            this.textNombreItem.Name = "textNombreItem";
            this.textNombreItem.Size = new System.Drawing.Size(143, 18);
            this.textNombreItem.TabIndex = 3;
            this.textNombreItem.Text = "Nombre del Articulo";
            // 
            // editCodigoItem
            // 
            this.editCodigoItem.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.editCodigoItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editCodigoItem.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editCodigoItem.Location = new System.Drawing.Point(193, 38);
            this.editCodigoItem.Multiline = true;
            this.editCodigoItem.Name = "editCodigoItem";
            this.editCodigoItem.ReadOnly = true;
            this.editCodigoItem.Size = new System.Drawing.Size(168, 24);
            this.editCodigoItem.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(203, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(127, 18);
            this.label9.TabIndex = 1;
            this.label9.Text = "Clave del Articulo";
            // 
            // openFileDialogSearchItemImage
            // 
            this.openFileDialogSearchItemImage.FileName = "openFileDialogSearchItemImage";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FrmItemDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(872, 584);
            this.Controls.Add(this.tabControlItemDetail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmItemDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Información del Articulo";
            this.Load += new System.EventHandler(this.frmArticuloInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureArticulo)).EndInit();
            this.tabControlItemDetail.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureArticulo;
        private System.Windows.Forms.TabControl tabControlItemDetail;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox editDescuentoItem;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox editExistenciaItem;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox editNombreItem;
        private System.Windows.Forms.Label textNombreItem;
        private System.Windows.Forms.TextBox editCodigoItem;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnUploadImg;
        private System.Windows.Forms.OpenFileDialog openFileDialogSearchItemImage;
        private System.Windows.Forms.Label textPreciosItem;
        private System.Windows.Forms.Label textSalesUnit;
        private System.Windows.Forms.Label textPurchaseUnit;
        private System.Windows.Forms.Label textNonConvertibleUnit;
        private System.Windows.Forms.Label textBaseUnit;
        private System.Windows.Forms.ProgressBar progressBarLoadItemDetail;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label textProductFiscalNoFiscal;
    }
}