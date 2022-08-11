
namespace SyncTPV.Views.Scale
{
    partial class FormCalculateWeight
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCalculateWeight));
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.btnReconectar = new System.Windows.Forms.Button();
            this.checkBoxPesarPolloVivo = new System.Windows.Forms.CheckBox();
            this.btnResetPeso = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panelTotales = new System.Windows.Forms.Panel();
            this.editObservationDoc = new System.Windows.Forms.TextBox();
            this.editCajas = new System.Windows.Forms.TextBox();
            this.textInfoCajas = new System.Windows.Forms.Label();
            this.btnGuardar = new SyncTPV.RoundedButton();
            this.textPesoNeto = new System.Windows.Forms.Label();
            this.editPesoNeto = new System.Windows.Forms.TextBox();
            this.panelSeleccionarCajas = new System.Windows.Forms.Panel();
            this.textPesoTotalCajas = new System.Windows.Forms.Label();
            this.btnBorrarPesoCajas = new System.Windows.Forms.Button();
            this.btnAgregarCajas = new System.Windows.Forms.Button();
            this.textInfoCantidadDeCajasSeleccionadas = new System.Windows.Forms.Label();
            this.textInfoSeleccionarCajas = new System.Windows.Forms.Label();
            this.editCantidadDeCajasSeleccionada = new System.Windows.Forms.TextBox();
            this.comboBoxTiposCajas = new System.Windows.Forms.ComboBox();
            this.panelPesoTaras = new System.Windows.Forms.Panel();
            this.editOperacionesPesoTaras = new System.Windows.Forms.TextBox();
            this.btnBorrarPesoTarasPollosMalos = new System.Windows.Forms.Button();
            this.comboBoxTipoPesoExcedente = new System.Windows.Forms.ComboBox();
            this.textInfoKgTara = new System.Windows.Forms.Label();
            this.textPesoTaras = new System.Windows.Forms.Label();
            this.editPesoTaras = new System.Windows.Forms.TextBox();
            this.btnRestarPesoTara = new System.Windows.Forms.Button();
            this.btnSumarPesoTara = new System.Windows.Forms.Button();
            this.panelPesoBruto = new System.Windows.Forms.Panel();
            this.editScaleInformation = new System.Windows.Forms.TextBox();
            this.editOperacionesPesoBruto = new System.Windows.Forms.TextBox();
            this.btnBorrarPesoBruto = new System.Windows.Forms.Button();
            this.btnRestarPesoBruto = new System.Windows.Forms.Button();
            this.btnSumarPesoBruto = new System.Windows.Forms.Button();
            this.textKg = new System.Windows.Forms.Label();
            this.textPesoBruto = new System.Windows.Forms.Label();
            this.editPesoBruto = new System.Windows.Forms.TextBox();
            this.panelToolbar.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelTotales.SuspendLayout();
            this.panelSeleccionarCajas.SuspendLayout();
            this.panelPesoTaras.SuspendLayout();
            this.panelPesoBruto.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelToolbar
            // 
            this.panelToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelToolbar.BackColor = System.Drawing.Color.Coral;
            this.panelToolbar.Controls.Add(this.btnReconectar);
            this.panelToolbar.Controls.Add(this.checkBoxPesarPolloVivo);
            this.panelToolbar.Controls.Add(this.btnResetPeso);
            this.panelToolbar.Controls.Add(this.btnClose);
            this.panelToolbar.Controls.Add(this.panel2);
            this.panelToolbar.Location = new System.Drawing.Point(-1, -2);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(581, 69);
            this.panelToolbar.TabIndex = 0;
            // 
            // btnReconectar
            // 
            this.btnReconectar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReconectar.BackColor = System.Drawing.Color.Coral;
            this.btnReconectar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReconectar.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnReconectar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnReconectar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReconectar.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReconectar.ForeColor = System.Drawing.Color.White;
            this.btnReconectar.Location = new System.Drawing.Point(370, 3);
            this.btnReconectar.Name = "btnReconectar";
            this.btnReconectar.Size = new System.Drawing.Size(82, 62);
            this.btnReconectar.TabIndex = 5;
            this.btnReconectar.Text = "Reconectar Báscula";
            this.btnReconectar.UseVisualStyleBackColor = false;
            this.btnReconectar.Click += new System.EventHandler(this.btnReconectar_Click);
            // 
            // checkBoxPesarPolloVivo
            // 
            this.checkBoxPesarPolloVivo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxPesarPolloVivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBoxPesarPolloVivo.Font = new System.Drawing.Font("Roboto Black", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxPesarPolloVivo.ForeColor = System.Drawing.Color.White;
            this.checkBoxPesarPolloVivo.Location = new System.Drawing.Point(458, 4);
            this.checkBoxPesarPolloVivo.Name = "checkBoxPesarPolloVivo";
            this.checkBoxPesarPolloVivo.Size = new System.Drawing.Size(120, 27);
            this.checkBoxPesarPolloVivo.TabIndex = 4;
            this.checkBoxPesarPolloVivo.Text = "Pesar Pollo Vivo";
            this.checkBoxPesarPolloVivo.UseVisualStyleBackColor = true;
            this.checkBoxPesarPolloVivo.CheckedChanged += new System.EventHandler(this.checkBoxPesarPolloVivo_CheckedChanged);
            // 
            // btnResetPeso
            // 
            this.btnResetPeso.BackColor = System.Drawing.Color.Coral;
            this.btnResetPeso.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnResetPeso.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnResetPeso.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnResetPeso.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetPeso.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetPeso.ForeColor = System.Drawing.Color.White;
            this.btnResetPeso.Location = new System.Drawing.Point(84, 3);
            this.btnResetPeso.Name = "btnResetPeso";
            this.btnResetPeso.Size = new System.Drawing.Size(82, 62);
            this.btnResetPeso.TabIndex = 3;
            this.btnResetPeso.Text = "Borrar \r\nPesos";
            this.btnResetPeso.UseVisualStyleBackColor = false;
            this.btnResetPeso.Click += new System.EventHandler(this.btnResetPeso_Click);
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
            this.btnClose.Size = new System.Drawing.Size(75, 62);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Cerrar";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(0, 99);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(517, 188);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.panelTotales);
            this.panel3.Controls.Add(this.panelSeleccionarCajas);
            this.panel3.Controls.Add(this.panelPesoTaras);
            this.panel3.Controls.Add(this.panelPesoBruto);
            this.panel3.Location = new System.Drawing.Point(-1, 73);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(578, 465);
            this.panel3.TabIndex = 1;
            // 
            // panelTotales
            // 
            this.panelTotales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTotales.Controls.Add(this.editObservationDoc);
            this.panelTotales.Controls.Add(this.editCajas);
            this.panelTotales.Controls.Add(this.textInfoCajas);
            this.panelTotales.Controls.Add(this.btnGuardar);
            this.panelTotales.Controls.Add(this.textPesoNeto);
            this.panelTotales.Controls.Add(this.editPesoNeto);
            this.panelTotales.Location = new System.Drawing.Point(7, 369);
            this.panelTotales.Name = "panelTotales";
            this.panelTotales.Size = new System.Drawing.Size(559, 93);
            this.panelTotales.TabIndex = 31;
            // 
            // editObservationDoc
            // 
            this.editObservationDoc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editObservationDoc.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editObservationDoc.Location = new System.Drawing.Point(14, 8);
            this.editObservationDoc.Multiline = true;
            this.editObservationDoc.Name = "editObservationDoc";
            this.editObservationDoc.Size = new System.Drawing.Size(233, 75);
            this.editObservationDoc.TabIndex = 32;
            // 
            // editCajas
            // 
            this.editCajas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.editCajas.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editCajas.Location = new System.Drawing.Point(326, 53);
            this.editCajas.Name = "editCajas";
            this.editCajas.Size = new System.Drawing.Size(86, 33);
            this.editCajas.TabIndex = 31;
            this.editCajas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editCajas_KeyPress);
            // 
            // textInfoCajas
            // 
            this.textInfoCajas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textInfoCajas.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInfoCajas.Location = new System.Drawing.Point(253, 61);
            this.textInfoCajas.Name = "textInfoCajas";
            this.textInfoCajas.Size = new System.Drawing.Size(67, 22);
            this.textInfoCajas.TabIndex = 30;
            this.textInfoCajas.Text = "Cajas:";
            this.textInfoCajas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.BackColor = System.Drawing.Color.Transparent;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnGuardar.FlatAppearance.BorderSize = 2;
            this.btnGuardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Location = new System.Drawing.Point(418, 35);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(129, 51);
            this.btnGuardar.TabIndex = 29;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // textPesoNeto
            // 
            this.textPesoNeto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textPesoNeto.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textPesoNeto.Location = new System.Drawing.Point(342, 8);
            this.textPesoNeto.Name = "textPesoNeto";
            this.textPesoNeto.Size = new System.Drawing.Size(87, 19);
            this.textPesoNeto.TabIndex = 28;
            this.textPesoNeto.Text = "Peso Neto:";
            this.textPesoNeto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // editPesoNeto
            // 
            this.editPesoNeto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.editPesoNeto.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editPesoNeto.Location = new System.Drawing.Point(435, 3);
            this.editPesoNeto.Name = "editPesoNeto";
            this.editPesoNeto.ReadOnly = true;
            this.editPesoNeto.Size = new System.Drawing.Size(121, 26);
            this.editPesoNeto.TabIndex = 27;
            this.editPesoNeto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panelSeleccionarCajas
            // 
            this.panelSeleccionarCajas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSeleccionarCajas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelSeleccionarCajas.Controls.Add(this.textPesoTotalCajas);
            this.panelSeleccionarCajas.Controls.Add(this.btnBorrarPesoCajas);
            this.panelSeleccionarCajas.Controls.Add(this.btnAgregarCajas);
            this.panelSeleccionarCajas.Controls.Add(this.textInfoCantidadDeCajasSeleccionadas);
            this.panelSeleccionarCajas.Controls.Add(this.textInfoSeleccionarCajas);
            this.panelSeleccionarCajas.Controls.Add(this.editCantidadDeCajasSeleccionada);
            this.panelSeleccionarCajas.Controls.Add(this.comboBoxTiposCajas);
            this.panelSeleccionarCajas.Location = new System.Drawing.Point(7, 254);
            this.panelSeleccionarCajas.Name = "panelSeleccionarCajas";
            this.panelSeleccionarCajas.Size = new System.Drawing.Size(559, 109);
            this.panelSeleccionarCajas.TabIndex = 30;
            // 
            // textPesoTotalCajas
            // 
            this.textPesoTotalCajas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textPesoTotalCajas.AutoSize = true;
            this.textPesoTotalCajas.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textPesoTotalCajas.Location = new System.Drawing.Point(9, 88);
            this.textPesoTotalCajas.Name = "textPesoTotalCajas";
            this.textPesoTotalCajas.Size = new System.Drawing.Size(68, 14);
            this.textPesoTotalCajas.TabIndex = 36;
            this.textPesoTotalCajas.Text = "Peso Cajas";
            // 
            // btnBorrarPesoCajas
            // 
            this.btnBorrarPesoCajas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBorrarPesoCajas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBorrarPesoCajas.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnBorrarPesoCajas.FlatAppearance.BorderSize = 2;
            this.btnBorrarPesoCajas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBorrarPesoCajas.Font = new System.Drawing.Font("Roboto Black", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBorrarPesoCajas.ForeColor = System.Drawing.Color.Red;
            this.btnBorrarPesoCajas.Location = new System.Drawing.Point(519, 3);
            this.btnBorrarPesoCajas.Name = "btnBorrarPesoCajas";
            this.btnBorrarPesoCajas.Size = new System.Drawing.Size(33, 23);
            this.btnBorrarPesoCajas.TabIndex = 35;
            this.btnBorrarPesoCajas.Text = "X";
            this.btnBorrarPesoCajas.UseVisualStyleBackColor = true;
            this.btnBorrarPesoCajas.Click += new System.EventHandler(this.btnBorrarPesoCajas_Click);
            // 
            // btnAgregarCajas
            // 
            this.btnAgregarCajas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregarCajas.BackColor = System.Drawing.Color.Transparent;
            this.btnAgregarCajas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarCajas.FlatAppearance.BorderColor = System.Drawing.Color.LimeGreen;
            this.btnAgregarCajas.FlatAppearance.BorderSize = 2;
            this.btnAgregarCajas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarCajas.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarCajas.Location = new System.Drawing.Point(416, 56);
            this.btnAgregarCajas.Name = "btnAgregarCajas";
            this.btnAgregarCajas.Size = new System.Drawing.Size(127, 46);
            this.btnAgregarCajas.TabIndex = 4;
            this.btnAgregarCajas.Text = "Agregar";
            this.btnAgregarCajas.UseVisualStyleBackColor = false;
            this.btnAgregarCajas.Click += new System.EventHandler(this.btnAgregarCajas_Click);
            // 
            // textInfoCantidadDeCajasSeleccionadas
            // 
            this.textInfoCantidadDeCajasSeleccionadas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textInfoCantidadDeCajasSeleccionadas.AutoSize = true;
            this.textInfoCantidadDeCajasSeleccionadas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInfoCantidadDeCajasSeleccionadas.Location = new System.Drawing.Point(353, 0);
            this.textInfoCantidadDeCajasSeleccionadas.Name = "textInfoCantidadDeCajasSeleccionadas";
            this.textInfoCantidadDeCajasSeleccionadas.Size = new System.Drawing.Size(107, 15);
            this.textInfoCantidadDeCajasSeleccionadas.TabIndex = 3;
            this.textInfoCantidadDeCajasSeleccionadas.Text = "Cantidad de Cajas";
            // 
            // textInfoSeleccionarCajas
            // 
            this.textInfoSeleccionarCajas.AutoSize = true;
            this.textInfoSeleccionarCajas.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInfoSeleccionarCajas.Location = new System.Drawing.Point(9, 9);
            this.textInfoSeleccionarCajas.Name = "textInfoSeleccionarCajas";
            this.textInfoSeleccionarCajas.Size = new System.Drawing.Size(140, 14);
            this.textInfoSeleccionarCajas.TabIndex = 2;
            this.textInfoSeleccionarCajas.Text = "Seleccionar Tipo de Caja";
            // 
            // editCantidadDeCajasSeleccionada
            // 
            this.editCantidadDeCajasSeleccionada.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editCantidadDeCajasSeleccionada.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editCantidadDeCajasSeleccionada.Location = new System.Drawing.Point(356, 19);
            this.editCantidadDeCajasSeleccionada.Name = "editCantidadDeCajasSeleccionada";
            this.editCantidadDeCajasSeleccionada.Size = new System.Drawing.Size(124, 31);
            this.editCantidadDeCajasSeleccionada.TabIndex = 1;
            this.editCantidadDeCajasSeleccionada.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editCantidadDeCajasSeleccionada_KeyPress);
            // 
            // comboBoxTiposCajas
            // 
            this.comboBoxTiposCajas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxTiposCajas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTiposCajas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxTiposCajas.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxTiposCajas.FormattingEnabled = true;
            this.comboBoxTiposCajas.Location = new System.Drawing.Point(6, 41);
            this.comboBoxTiposCajas.Name = "comboBoxTiposCajas";
            this.comboBoxTiposCajas.Size = new System.Drawing.Size(303, 33);
            this.comboBoxTiposCajas.TabIndex = 0;
            this.comboBoxTiposCajas.SelectedIndexChanged += new System.EventHandler(this.comboBoxTiposCajas_SelectedIndexChanged);
            // 
            // panelPesoTaras
            // 
            this.panelPesoTaras.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPesoTaras.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelPesoTaras.Controls.Add(this.editOperacionesPesoTaras);
            this.panelPesoTaras.Controls.Add(this.btnBorrarPesoTarasPollosMalos);
            this.panelPesoTaras.Controls.Add(this.comboBoxTipoPesoExcedente);
            this.panelPesoTaras.Controls.Add(this.textInfoKgTara);
            this.panelPesoTaras.Controls.Add(this.textPesoTaras);
            this.panelPesoTaras.Controls.Add(this.editPesoTaras);
            this.panelPesoTaras.Controls.Add(this.btnRestarPesoTara);
            this.panelPesoTaras.Controls.Add(this.btnSumarPesoTara);
            this.panelPesoTaras.Location = new System.Drawing.Point(7, 117);
            this.panelPesoTaras.Name = "panelPesoTaras";
            this.panelPesoTaras.Size = new System.Drawing.Size(559, 131);
            this.panelPesoTaras.TabIndex = 29;
            // 
            // editOperacionesPesoTaras
            // 
            this.editOperacionesPesoTaras.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editOperacionesPesoTaras.Location = new System.Drawing.Point(3, 74);
            this.editOperacionesPesoTaras.Multiline = true;
            this.editOperacionesPesoTaras.Name = "editOperacionesPesoTaras";
            this.editOperacionesPesoTaras.ReadOnly = true;
            this.editOperacionesPesoTaras.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.editOperacionesPesoTaras.Size = new System.Drawing.Size(342, 49);
            this.editOperacionesPesoTaras.TabIndex = 35;
            // 
            // btnBorrarPesoTarasPollosMalos
            // 
            this.btnBorrarPesoTarasPollosMalos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBorrarPesoTarasPollosMalos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBorrarPesoTarasPollosMalos.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnBorrarPesoTarasPollosMalos.FlatAppearance.BorderSize = 2;
            this.btnBorrarPesoTarasPollosMalos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBorrarPesoTarasPollosMalos.Font = new System.Drawing.Font("Roboto Black", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBorrarPesoTarasPollosMalos.ForeColor = System.Drawing.Color.Red;
            this.btnBorrarPesoTarasPollosMalos.Location = new System.Drawing.Point(519, 3);
            this.btnBorrarPesoTarasPollosMalos.Name = "btnBorrarPesoTarasPollosMalos";
            this.btnBorrarPesoTarasPollosMalos.Size = new System.Drawing.Size(33, 23);
            this.btnBorrarPesoTarasPollosMalos.TabIndex = 32;
            this.btnBorrarPesoTarasPollosMalos.Text = "X";
            this.btnBorrarPesoTarasPollosMalos.UseVisualStyleBackColor = true;
            this.btnBorrarPesoTarasPollosMalos.Click += new System.EventHandler(this.btnBorrarPesoTarasPollosMalos_Click);
            // 
            // comboBoxTipoPesoExcedente
            // 
            this.comboBoxTipoPesoExcedente.BackColor = System.Drawing.Color.Azure;
            this.comboBoxTipoPesoExcedente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxTipoPesoExcedente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTipoPesoExcedente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxTipoPesoExcedente.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxTipoPesoExcedente.FormattingEnabled = true;
            this.comboBoxTipoPesoExcedente.Location = new System.Drawing.Point(3, 3);
            this.comboBoxTipoPesoExcedente.Name = "comboBoxTipoPesoExcedente";
            this.comboBoxTipoPesoExcedente.Size = new System.Drawing.Size(238, 33);
            this.comboBoxTipoPesoExcedente.TabIndex = 34;
            this.comboBoxTipoPesoExcedente.SelectedIndexChanged += new System.EventHandler(this.comboBoxTipoPesoExcedente_SelectedIndexChanged);
            // 
            // textInfoKgTara
            // 
            this.textInfoKgTara.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textInfoKgTara.AutoSize = true;
            this.textInfoKgTara.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInfoKgTara.Location = new System.Drawing.Point(503, 49);
            this.textInfoKgTara.Name = "textInfoKgTara";
            this.textInfoKgTara.Size = new System.Drawing.Size(22, 14);
            this.textInfoKgTara.TabIndex = 33;
            this.textInfoKgTara.Text = "Kg";
            this.textInfoKgTara.Click += new System.EventHandler(this.textInfoKgTara_Click);
            // 
            // textPesoTaras
            // 
            this.textPesoTaras.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textPesoTaras.Location = new System.Drawing.Point(45, 39);
            this.textPesoTaras.Name = "textPesoTaras";
            this.textPesoTaras.Size = new System.Drawing.Size(196, 24);
            this.textPesoTaras.TabIndex = 32;
            this.textPesoTaras.Text = "Peso Taras:";
            this.textPesoTaras.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // editPesoTaras
            // 
            this.editPesoTaras.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editPesoTaras.Font = new System.Drawing.Font("Roboto Black", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editPesoTaras.Location = new System.Drawing.Point(247, 35);
            this.editPesoTaras.Name = "editPesoTaras";
            this.editPesoTaras.Size = new System.Drawing.Size(250, 33);
            this.editPesoTaras.TabIndex = 31;
            this.editPesoTaras.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.editPesoTaras.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editPesoTaras_KeyPress);
            // 
            // btnRestarPesoTara
            // 
            this.btnRestarPesoTara.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestarPesoTara.BackColor = System.Drawing.Color.Transparent;
            this.btnRestarPesoTara.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestarPesoTara.FlatAppearance.BorderColor = System.Drawing.Color.LimeGreen;
            this.btnRestarPesoTara.FlatAppearance.BorderSize = 2;
            this.btnRestarPesoTara.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestarPesoTara.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestarPesoTara.ForeColor = System.Drawing.Color.Black;
            this.btnRestarPesoTara.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRestarPesoTara.Location = new System.Drawing.Point(449, 74);
            this.btnRestarPesoTara.Name = "btnRestarPesoTara";
            this.btnRestarPesoTara.Size = new System.Drawing.Size(102, 50);
            this.btnRestarPesoTara.TabIndex = 30;
            this.btnRestarPesoTara.Text = "Agregar (restar)";
            this.btnRestarPesoTara.UseVisualStyleBackColor = false;
            this.btnRestarPesoTara.Click += new System.EventHandler(this.btnRestar_Click);
            // 
            // btnSumarPesoTara
            // 
            this.btnSumarPesoTara.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSumarPesoTara.BackColor = System.Drawing.Color.Transparent;
            this.btnSumarPesoTara.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSumarPesoTara.FlatAppearance.BorderColor = System.Drawing.Color.Coral;
            this.btnSumarPesoTara.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSumarPesoTara.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSumarPesoTara.ForeColor = System.Drawing.Color.Black;
            this.btnSumarPesoTara.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSumarPesoTara.Location = new System.Drawing.Point(356, 74);
            this.btnSumarPesoTara.Name = "btnSumarPesoTara";
            this.btnSumarPesoTara.Size = new System.Drawing.Size(75, 49);
            this.btnSumarPesoTara.TabIndex = 29;
            this.btnSumarPesoTara.Text = "Quitar (sumar)";
            this.btnSumarPesoTara.UseVisualStyleBackColor = false;
            this.btnSumarPesoTara.Click += new System.EventHandler(this.btnSumar_Click);
            // 
            // panelPesoBruto
            // 
            this.panelPesoBruto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPesoBruto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelPesoBruto.Controls.Add(this.editScaleInformation);
            this.panelPesoBruto.Controls.Add(this.editOperacionesPesoBruto);
            this.panelPesoBruto.Controls.Add(this.btnBorrarPesoBruto);
            this.panelPesoBruto.Controls.Add(this.btnRestarPesoBruto);
            this.panelPesoBruto.Controls.Add(this.btnSumarPesoBruto);
            this.panelPesoBruto.Controls.Add(this.textKg);
            this.panelPesoBruto.Controls.Add(this.textPesoBruto);
            this.panelPesoBruto.Controls.Add(this.editPesoBruto);
            this.panelPesoBruto.Location = new System.Drawing.Point(7, 3);
            this.panelPesoBruto.Name = "panelPesoBruto";
            this.panelPesoBruto.Size = new System.Drawing.Size(559, 108);
            this.panelPesoBruto.TabIndex = 28;
            // 
            // editScaleInformation
            // 
            this.editScaleInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editScaleInformation.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editScaleInformation.Location = new System.Drawing.Point(4, 4);
            this.editScaleInformation.Multiline = true;
            this.editScaleInformation.Name = "editScaleInformation";
            this.editScaleInformation.Size = new System.Drawing.Size(143, 30);
            this.editScaleInformation.TabIndex = 33;
            // 
            // editOperacionesPesoBruto
            // 
            this.editOperacionesPesoBruto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editOperacionesPesoBruto.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editOperacionesPesoBruto.Location = new System.Drawing.Point(4, 40);
            this.editOperacionesPesoBruto.Multiline = true;
            this.editOperacionesPesoBruto.Name = "editOperacionesPesoBruto";
            this.editOperacionesPesoBruto.ReadOnly = true;
            this.editOperacionesPesoBruto.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.editOperacionesPesoBruto.Size = new System.Drawing.Size(341, 61);
            this.editOperacionesPesoBruto.TabIndex = 32;
            // 
            // btnBorrarPesoBruto
            // 
            this.btnBorrarPesoBruto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBorrarPesoBruto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBorrarPesoBruto.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnBorrarPesoBruto.FlatAppearance.BorderSize = 2;
            this.btnBorrarPesoBruto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBorrarPesoBruto.Font = new System.Drawing.Font("Roboto Black", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBorrarPesoBruto.ForeColor = System.Drawing.Color.Red;
            this.btnBorrarPesoBruto.Location = new System.Drawing.Point(514, 3);
            this.btnBorrarPesoBruto.Name = "btnBorrarPesoBruto";
            this.btnBorrarPesoBruto.Size = new System.Drawing.Size(38, 24);
            this.btnBorrarPesoBruto.TabIndex = 31;
            this.btnBorrarPesoBruto.Text = "X";
            this.btnBorrarPesoBruto.UseVisualStyleBackColor = true;
            this.btnBorrarPesoBruto.Click += new System.EventHandler(this.btnBorrarPesoBruto_Click);
            // 
            // btnRestarPesoBruto
            // 
            this.btnRestarPesoBruto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestarPesoBruto.BackColor = System.Drawing.Color.Transparent;
            this.btnRestarPesoBruto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestarPesoBruto.FlatAppearance.BorderColor = System.Drawing.Color.Coral;
            this.btnRestarPesoBruto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestarPesoBruto.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestarPesoBruto.ForeColor = System.Drawing.Color.Black;
            this.btnRestarPesoBruto.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRestarPesoBruto.Location = new System.Drawing.Point(351, 40);
            this.btnRestarPesoBruto.Name = "btnRestarPesoBruto";
            this.btnRestarPesoBruto.Size = new System.Drawing.Size(80, 61);
            this.btnRestarPesoBruto.TabIndex = 29;
            this.btnRestarPesoBruto.Text = "Quitar (restar)";
            this.btnRestarPesoBruto.UseVisualStyleBackColor = false;
            this.btnRestarPesoBruto.Click += new System.EventHandler(this.btnRestarPesoBruto_Click);
            // 
            // btnSumarPesoBruto
            // 
            this.btnSumarPesoBruto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSumarPesoBruto.BackColor = System.Drawing.Color.Transparent;
            this.btnSumarPesoBruto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSumarPesoBruto.FlatAppearance.BorderColor = System.Drawing.Color.LimeGreen;
            this.btnSumarPesoBruto.FlatAppearance.BorderSize = 2;
            this.btnSumarPesoBruto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSumarPesoBruto.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSumarPesoBruto.ForeColor = System.Drawing.Color.Black;
            this.btnSumarPesoBruto.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSumarPesoBruto.Location = new System.Drawing.Point(449, 40);
            this.btnSumarPesoBruto.Name = "btnSumarPesoBruto";
            this.btnSumarPesoBruto.Size = new System.Drawing.Size(102, 61);
            this.btnSumarPesoBruto.TabIndex = 28;
            this.btnSumarPesoBruto.Text = "Agregar (sumar)";
            this.btnSumarPesoBruto.UseVisualStyleBackColor = false;
            this.btnSumarPesoBruto.Click += new System.EventHandler(this.btnSumarPesoBruto_Click);
            // 
            // textKg
            // 
            this.textKg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textKg.AutoSize = true;
            this.textKg.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textKg.Location = new System.Drawing.Point(454, 11);
            this.textKg.Name = "textKg";
            this.textKg.Size = new System.Drawing.Size(22, 14);
            this.textKg.TabIndex = 27;
            this.textKg.Text = "Kg";
            this.textKg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textPesoBruto
            // 
            this.textPesoBruto.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textPesoBruto.Location = new System.Drawing.Point(153, 3);
            this.textPesoBruto.Name = "textPesoBruto";
            this.textPesoBruto.Size = new System.Drawing.Size(88, 24);
            this.textPesoBruto.TabIndex = 26;
            this.textPesoBruto.Text = "Peso Bruto:";
            this.textPesoBruto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // editPesoBruto
            // 
            this.editPesoBruto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editPesoBruto.Font = new System.Drawing.Font("Roboto Black", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editPesoBruto.Location = new System.Drawing.Point(247, 0);
            this.editPesoBruto.Name = "editPesoBruto";
            this.editPesoBruto.Size = new System.Drawing.Size(201, 33);
            this.editPesoBruto.TabIndex = 25;
            this.editPesoBruto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.editPesoBruto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.editPesoBruto_KeyDown);
            this.editPesoBruto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editPesoActual_KeyPress);
            this.editPesoBruto.KeyUp += new System.Windows.Forms.KeyEventHandler(this.editPesoBruto_KeyUp);
            // 
            // FormCalculateWeight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(577, 537);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panelToolbar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(593, 467);
            this.Name = "FormCalculateWeight";
            this.Text = "Calcular Peso";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCalculateWeight_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmCalculateWeight_FormClosed);
            this.Load += new System.EventHandler(this.FrmCalculateWeight_Load);
            this.panelToolbar.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panelTotales.ResumeLayout(false);
            this.panelTotales.PerformLayout();
            this.panelSeleccionarCajas.ResumeLayout(false);
            this.panelSeleccionarCajas.PerformLayout();
            this.panelPesoTaras.ResumeLayout(false);
            this.panelPesoTaras.PerformLayout();
            this.panelPesoBruto.ResumeLayout(false);
            this.panelPesoBruto.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnResetPeso;
        private System.Windows.Forms.Panel panelPesoTaras;
        private System.Windows.Forms.ComboBox comboBoxTipoPesoExcedente;
        private System.Windows.Forms.Label textInfoKgTara;
        private System.Windows.Forms.Label textPesoTaras;
        private System.Windows.Forms.TextBox editPesoTaras;
        private System.Windows.Forms.Button btnRestarPesoTara;
        private System.Windows.Forms.Button btnSumarPesoTara;
        private System.Windows.Forms.Panel panelPesoBruto;
        private System.Windows.Forms.Button btnRestarPesoBruto;
        private System.Windows.Forms.Button btnSumarPesoBruto;
        private System.Windows.Forms.Label textKg;
        private System.Windows.Forms.Label textPesoBruto;
        private System.Windows.Forms.TextBox editPesoBruto;
        private System.Windows.Forms.Panel panelSeleccionarCajas;
        private System.Windows.Forms.TextBox editCantidadDeCajasSeleccionada;
        private System.Windows.Forms.ComboBox comboBoxTiposCajas;
        private System.Windows.Forms.Label textInfoSeleccionarCajas;
        private System.Windows.Forms.Label textInfoCantidadDeCajasSeleccionadas;
        private System.Windows.Forms.Button btnAgregarCajas;
        private System.Windows.Forms.Panel panelTotales;
        private System.Windows.Forms.TextBox editCajas;
        private System.Windows.Forms.Label textInfoCajas;
        private System.Windows.Forms.Label textPesoNeto;
        private System.Windows.Forms.TextBox editPesoNeto;
        private System.Windows.Forms.CheckBox checkBoxPesarPolloVivo;
        private System.Windows.Forms.Button btnBorrarPesoBruto;
        private System.Windows.Forms.Button btnBorrarPesoCajas;
        private System.Windows.Forms.Button btnBorrarPesoTarasPollosMalos;
        private System.Windows.Forms.Label textPesoTotalCajas;
        private RoundedButton btnGuardar;
        private System.Windows.Forms.TextBox editOperacionesPesoBruto;
        private System.Windows.Forms.TextBox editOperacionesPesoTaras;
        private System.Windows.Forms.TextBox editObservationDoc;
        private System.Windows.Forms.Button btnReconectar;
        private System.Windows.Forms.TextBox editScaleInformation;
    }
}