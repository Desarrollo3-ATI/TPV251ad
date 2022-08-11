namespace SyncTPV
{
    partial class FormConfiguracionGral
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfiguracionGral));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnConfigTicket = new System.Windows.Forms.Button();
            this.panelLeyendas = new System.Windows.Forms.Panel();
            this.editLeyendaCopia = new System.Windows.Forms.TextBox();
            this.editLeyendaOriginal = new System.Windows.Forms.TextBox();
            this.textLeyendaCopia = new System.Windows.Forms.Label();
            this.textLeyendaOriginal = new System.Windows.Forms.Label();
            this.textInfoLeyendas = new System.Windows.Forms.Label();
            this.editNumCopias = new System.Windows.Forms.TextBox();
            this.textNumCopias = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnImprimir = new System.Windows.Forms.PictureBox();
            this.txtImpresora = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbImpresoras = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBoxScalePermission = new System.Windows.Forms.GroupBox();
            this.checkBoxCerrarCOM = new System.Windows.Forms.CheckBox();
            this.checkBoxCapturaPesoManual = new System.Windows.Forms.CheckBox();
            this.cnombrebascula = new System.Windows.Forms.TextBox();
            this.checkBoxUsoBascula = new System.Windows.Forms.CheckBox();
            this.cBitsPadada = new System.Windows.Forms.ComboBox();
            this.cBitsdatos = new System.Windows.Forms.ComboBox();
            this.cParidad = new System.Windows.Forms.ComboBox();
            this.cBitsxSeg = new System.Windows.Forms.ComboBox();
            this.cPuerto = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxFiscales = new System.Windows.Forms.CheckBox();
            this.editFiscalItemField = new System.Windows.Forms.TextBox();
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.checkBoxUsoWeb = new System.Windows.Forms.CheckBox();
            this.btnGuardar = new SyncTPV.RoundedButton();
            this.groupBox1.SuspendLayout();
            this.panelLeyendas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnImprimir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBoxScalePermission.SuspendLayout();
            this.panelToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.btnConfigTicket);
            this.groupBox1.Controls.Add(this.panelLeyendas);
            this.groupBox1.Controls.Add(this.editNumCopias);
            this.groupBox1.Controls.Add(this.textNumCopias);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnImprimir);
            this.groupBox1.Controls.Add(this.txtImpresora);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbImpresoras);
            this.groupBox1.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(28, 99);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(623, 159);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuración Impresora ";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btnConfigTicket
            // 
            this.btnConfigTicket.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfigTicket.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnConfigTicket.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnConfigTicket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfigTicket.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfigTicket.Location = new System.Drawing.Point(10, 120);
            this.btnConfigTicket.Name = "btnConfigTicket";
            this.btnConfigTicket.Size = new System.Drawing.Size(116, 33);
            this.btnConfigTicket.TabIndex = 52;
            this.btnConfigTicket.Text = "Encabezado";
            this.btnConfigTicket.UseVisualStyleBackColor = true;
            this.btnConfigTicket.Click += new System.EventHandler(this.btnConfigTicket_Click);
            this.btnConfigTicket.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnConfigTicket_KeyUp);
            // 
            // panelLeyendas
            // 
            this.panelLeyendas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLeyendas.Controls.Add(this.editLeyendaCopia);
            this.panelLeyendas.Controls.Add(this.editLeyendaOriginal);
            this.panelLeyendas.Controls.Add(this.textLeyendaCopia);
            this.panelLeyendas.Controls.Add(this.textLeyendaOriginal);
            this.panelLeyendas.Controls.Add(this.textInfoLeyendas);
            this.panelLeyendas.Location = new System.Drawing.Point(177, 71);
            this.panelLeyendas.Name = "panelLeyendas";
            this.panelLeyendas.Size = new System.Drawing.Size(440, 82);
            this.panelLeyendas.TabIndex = 51;
            // 
            // editLeyendaCopia
            // 
            this.editLeyendaCopia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editLeyendaCopia.Location = new System.Drawing.Point(62, 49);
            this.editLeyendaCopia.MaxLength = 30;
            this.editLeyendaCopia.Name = "editLeyendaCopia";
            this.editLeyendaCopia.Size = new System.Drawing.Size(368, 22);
            this.editLeyendaCopia.TabIndex = 4;
            this.editLeyendaCopia.KeyUp += new System.Windows.Forms.KeyEventHandler(this.editLeyendaCopia_KeyUp);
            // 
            // editLeyendaOriginal
            // 
            this.editLeyendaOriginal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editLeyendaOriginal.Location = new System.Drawing.Point(62, 20);
            this.editLeyendaOriginal.MaxLength = 30;
            this.editLeyendaOriginal.Name = "editLeyendaOriginal";
            this.editLeyendaOriginal.Size = new System.Drawing.Size(368, 22);
            this.editLeyendaOriginal.TabIndex = 3;
            this.editLeyendaOriginal.KeyUp += new System.Windows.Forms.KeyEventHandler(this.editLeyendaOriginal_KeyUp);
            // 
            // textLeyendaCopia
            // 
            this.textLeyendaCopia.AutoSize = true;
            this.textLeyendaCopia.Location = new System.Drawing.Point(7, 52);
            this.textLeyendaCopia.Name = "textLeyendaCopia";
            this.textLeyendaCopia.Size = new System.Drawing.Size(39, 14);
            this.textLeyendaCopia.TabIndex = 2;
            this.textLeyendaCopia.Text = "Copia";
            this.textLeyendaCopia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textLeyendaOriginal
            // 
            this.textLeyendaOriginal.AutoSize = true;
            this.textLeyendaOriginal.Location = new System.Drawing.Point(7, 23);
            this.textLeyendaOriginal.Name = "textLeyendaOriginal";
            this.textLeyendaOriginal.Size = new System.Drawing.Size(49, 14);
            this.textLeyendaOriginal.TabIndex = 1;
            this.textLeyendaOriginal.Text = "Original";
            this.textLeyendaOriginal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textInfoLeyendas
            // 
            this.textInfoLeyendas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textInfoLeyendas.AutoSize = true;
            this.textInfoLeyendas.Location = new System.Drawing.Point(128, 3);
            this.textInfoLeyendas.Name = "textInfoLeyendas";
            this.textInfoLeyendas.Size = new System.Drawing.Size(115, 14);
            this.textInfoLeyendas.TabIndex = 0;
            this.textInfoLeyendas.Text = "Leyendas en Tickets";
            this.textInfoLeyendas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // editNumCopias
            // 
            this.editNumCopias.Location = new System.Drawing.Point(126, 84);
            this.editNumCopias.MaxLength = 1;
            this.editNumCopias.Name = "editNumCopias";
            this.editNumCopias.Size = new System.Drawing.Size(45, 22);
            this.editNumCopias.TabIndex = 2;
            this.editNumCopias.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.editNumCopias.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editNumCopias_KeyPress);
            this.editNumCopias.KeyUp += new System.Windows.Forms.KeyEventHandler(this.editNumCopias_KeyUp);
            // 
            // textNumCopias
            // 
            this.textNumCopias.AutoSize = true;
            this.textNumCopias.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textNumCopias.Location = new System.Drawing.Point(10, 87);
            this.textNumCopias.Name = "textNumCopias";
            this.textNumCopias.Size = new System.Drawing.Size(108, 14);
            this.textNumCopias.TabIndex = 49;
            this.textNumCopias.Text = "Número de Copias";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(4, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 17);
            this.label6.TabIndex = 48;
            this.label6.Visible = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.Location = new System.Drawing.Point(445, 35);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(24, 24);
            this.btnImprimir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnImprimir.TabIndex = 3;
            this.btnImprimir.TabStop = false;
            this.toolTip1.SetToolTip(this.btnImprimir, "Imprimir pagina\r\nde prueba para la \r\nimpresora seleccionada\r\n");
            this.btnImprimir.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // txtImpresora
            // 
            this.txtImpresora.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImpresora.Location = new System.Drawing.Point(126, 35);
            this.txtImpresora.Name = "txtImpresora";
            this.txtImpresora.Size = new System.Drawing.Size(294, 27);
            this.txtImpresora.TabIndex = 1;
            this.txtImpresora.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtImpresora_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre Impresora:";
            // 
            // cmbImpresoras
            // 
            this.cmbImpresoras.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbImpresoras.FormattingEnabled = true;
            this.cmbImpresoras.Location = new System.Drawing.Point(126, 35);
            this.cmbImpresoras.Name = "cmbImpresoras";
            this.cmbImpresoras.Size = new System.Drawing.Size(315, 27);
            this.cmbImpresoras.TabIndex = 1;
            this.cmbImpresoras.SelectedIndexChanged += new System.EventHandler(this.cmbImpresoras_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Image = global::SyncTPV.Properties.Resources.scale;
            this.pictureBox1.Location = new System.Drawing.Point(442, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 28);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox1, "Imprimir pagina\r\nde prueba para la \r\nimpresora seleccionada\r\n");
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // groupBoxScalePermission
            // 
            this.groupBoxScalePermission.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxScalePermission.BackColor = System.Drawing.Color.White;
            this.groupBoxScalePermission.Controls.Add(this.checkBoxCerrarCOM);
            this.groupBoxScalePermission.Controls.Add(this.checkBoxCapturaPesoManual);
            this.groupBoxScalePermission.Controls.Add(this.cnombrebascula);
            this.groupBoxScalePermission.Controls.Add(this.checkBoxUsoBascula);
            this.groupBoxScalePermission.Controls.Add(this.cBitsPadada);
            this.groupBoxScalePermission.Controls.Add(this.cBitsdatos);
            this.groupBoxScalePermission.Controls.Add(this.cParidad);
            this.groupBoxScalePermission.Controls.Add(this.cBitsxSeg);
            this.groupBoxScalePermission.Controls.Add(this.cPuerto);
            this.groupBoxScalePermission.Controls.Add(this.label7);
            this.groupBoxScalePermission.Controls.Add(this.label9);
            this.groupBoxScalePermission.Controls.Add(this.label8);
            this.groupBoxScalePermission.Controls.Add(this.label5);
            this.groupBoxScalePermission.Controls.Add(this.label4);
            this.groupBoxScalePermission.Controls.Add(this.label2);
            this.groupBoxScalePermission.Controls.Add(this.pictureBox1);
            this.groupBoxScalePermission.Controls.Add(this.label3);
            this.groupBoxScalePermission.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxScalePermission.Location = new System.Drawing.Point(28, 264);
            this.groupBoxScalePermission.Name = "groupBoxScalePermission";
            this.groupBoxScalePermission.Size = new System.Drawing.Size(623, 183);
            this.groupBoxScalePermission.TabIndex = 47;
            this.groupBoxScalePermission.TabStop = false;
            this.groupBoxScalePermission.Text = "Configuración Báscula ";
            // 
            // checkBoxCerrarCOM
            // 
            this.checkBoxCerrarCOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxCerrarCOM.AutoSize = true;
            this.checkBoxCerrarCOM.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxCerrarCOM.Location = new System.Drawing.Point(6, 154);
            this.checkBoxCerrarCOM.Name = "checkBoxCerrarCOM";
            this.checkBoxCerrarCOM.Size = new System.Drawing.Size(169, 19);
            this.checkBoxCerrarCOM.TabIndex = 55;
            this.checkBoxCerrarCOM.Text = "Cerrar COM a cada ciclo";
            this.checkBoxCerrarCOM.UseVisualStyleBackColor = true;
            this.checkBoxCerrarCOM.Visible = false;
            this.checkBoxCerrarCOM.CheckedChanged += new System.EventHandler(this.checkBoxCerrarCOM_CheckedChanged);
            // 
            // checkBoxCapturaPesoManual
            // 
            this.checkBoxCapturaPesoManual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxCapturaPesoManual.AutoSize = true;
            this.checkBoxCapturaPesoManual.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxCapturaPesoManual.Location = new System.Drawing.Point(437, 154);
            this.checkBoxCapturaPesoManual.Name = "checkBoxCapturaPesoManual";
            this.checkBoxCapturaPesoManual.Size = new System.Drawing.Size(167, 19);
            this.checkBoxCapturaPesoManual.TabIndex = 12;
            this.checkBoxCapturaPesoManual.Text = "Captura de Peso Manual";
            this.checkBoxCapturaPesoManual.UseVisualStyleBackColor = true;
            this.checkBoxCapturaPesoManual.CheckedChanged += new System.EventHandler(this.checkBoxCapturaPesoManual_CheckedChanged);
            this.checkBoxCapturaPesoManual.KeyUp += new System.Windows.Forms.KeyEventHandler(this.checkBoxCapturaPesoManual_KeyUp);
            // 
            // cnombrebascula
            // 
            this.cnombrebascula.Location = new System.Drawing.Point(145, 32);
            this.cnombrebascula.Name = "cnombrebascula";
            this.cnombrebascula.Size = new System.Drawing.Size(291, 22);
            this.cnombrebascula.TabIndex = 6;
            this.cnombrebascula.Text = "BASCULA TECNOCOR";
            this.cnombrebascula.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cnombrebascula_KeyUp);
            // 
            // checkBoxUsoBascula
            // 
            this.checkBoxUsoBascula.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxUsoBascula.AutoSize = true;
            this.checkBoxUsoBascula.Location = new System.Drawing.Point(504, 0);
            this.checkBoxUsoBascula.Name = "checkBoxUsoBascula";
            this.checkBoxUsoBascula.Size = new System.Drawing.Size(109, 18);
            this.checkBoxUsoBascula.TabIndex = 5;
            this.checkBoxUsoBascula.Text = "Uso de báscula";
            this.checkBoxUsoBascula.UseVisualStyleBackColor = true;
            this.checkBoxUsoBascula.CheckedChanged += new System.EventHandler(this.checkBoxUsoBascula_CheckedChanged);
            this.checkBoxUsoBascula.KeyUp += new System.Windows.Forms.KeyEventHandler(this.checkBoxUsoBascula_KeyUp);
            // 
            // cBitsPadada
            // 
            this.cBitsPadada.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBitsPadada.FormattingEnabled = true;
            this.cBitsPadada.Items.AddRange(new object[] {
            "1",
            "1.5",
            "2"});
            this.cBitsPadada.Location = new System.Drawing.Point(351, 95);
            this.cBitsPadada.Name = "cBitsPadada";
            this.cBitsPadada.Size = new System.Drawing.Size(121, 22);
            this.cBitsPadada.TabIndex = 10;
            this.cBitsPadada.SelectedIndexChanged += new System.EventHandler(this.comboBox5_SelectedIndexChanged);
            // 
            // cBitsdatos
            // 
            this.cBitsdatos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBitsdatos.FormattingEnabled = true;
            this.cBitsdatos.Items.AddRange(new object[] {
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.cBitsdatos.Location = new System.Drawing.Point(351, 69);
            this.cBitsdatos.Name = "cBitsdatos";
            this.cBitsdatos.Size = new System.Drawing.Size(121, 22);
            this.cBitsdatos.TabIndex = 8;
            // 
            // cParidad
            // 
            this.cParidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cParidad.FormattingEnabled = true;
            this.cParidad.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4"});
            this.cParidad.Location = new System.Drawing.Point(99, 122);
            this.cParidad.Name = "cParidad";
            this.cParidad.Size = new System.Drawing.Size(121, 22);
            this.cParidad.TabIndex = 11;
            // 
            // cBitsxSeg
            // 
            this.cBitsxSeg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBitsxSeg.FormattingEnabled = true;
            this.cBitsxSeg.Items.AddRange(new object[] {
            "2400",
            "4800",
            "7200",
            "9600",
            "14400"});
            this.cBitsxSeg.Location = new System.Drawing.Point(99, 95);
            this.cBitsxSeg.Name = "cBitsxSeg";
            this.cBitsxSeg.Size = new System.Drawing.Size(121, 22);
            this.cBitsxSeg.TabIndex = 9;
            this.cBitsxSeg.SelectedIndexChanged += new System.EventHandler(this.cBitsxSeg_SelectedIndexChanged);
            // 
            // cPuerto
            // 
            this.cPuerto.Location = new System.Drawing.Point(99, 68);
            this.cPuerto.Name = "cPuerto";
            this.cPuerto.Size = new System.Drawing.Size(100, 22);
            this.cPuerto.TabIndex = 7;
            this.cPuerto.Text = "COM4";
            this.cPuerto.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(252, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 17);
            this.label7.TabIndex = 54;
            this.label7.Text = "Bits de parada:";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(261, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 17);
            this.label9.TabIndex = 53;
            this.label9.Text = "Bits de datos:";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(43, 125);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 17);
            this.label8.TabIndex = 52;
            this.label8.Text = "Paridad:";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 17);
            this.label5.TabIndex = 50;
            this.label5.Text = "Bits x segundos:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(43, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 17);
            this.label4.TabIndex = 49;
            this.label4.Text = "Puerto:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 17);
            this.label2.TabIndex = 48;
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Nombre de la báscula:";
            // 
            // checkBoxFiscales
            // 
            this.checkBoxFiscales.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxFiscales.AutoSize = true;
            this.checkBoxFiscales.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxFiscales.Location = new System.Drawing.Point(12, 479);
            this.checkBoxFiscales.Name = "checkBoxFiscales";
            this.checkBoxFiscales.Size = new System.Drawing.Size(233, 18);
            this.checkBoxFiscales.TabIndex = 48;
            this.checkBoxFiscales.Text = "Validar productos ficales y no fiscales";
            this.checkBoxFiscales.UseVisualStyleBackColor = true;
            this.checkBoxFiscales.CheckedChanged += new System.EventHandler(this.checkBoxFiscales_CheckedChanged);
            this.checkBoxFiscales.KeyUp += new System.Windows.Forms.KeyEventHandler(this.checkBoxFiscales_KeyUp);
            // 
            // editFiscalItemField
            // 
            this.editFiscalItemField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.editFiscalItemField.BackColor = System.Drawing.Color.LightBlue;
            this.editFiscalItemField.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.editFiscalItemField.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editFiscalItemField.Location = new System.Drawing.Point(12, 453);
            this.editFiscalItemField.Name = "editFiscalItemField";
            this.editFiscalItemField.ReadOnly = true;
            this.editFiscalItemField.Size = new System.Drawing.Size(233, 15);
            this.editFiscalItemField.TabIndex = 49;
            this.editFiscalItemField.Text = "Campo Fiscal";
            this.editFiscalItemField.KeyUp += new System.Windows.Forms.KeyEventHandler(this.editFiscalItemField_KeyUp);
            // 
            // panelToolbar
            // 
            this.panelToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelToolbar.BackColor = System.Drawing.Color.Coral;
            this.panelToolbar.Controls.Add(this.checkBoxUsoWeb);
            this.panelToolbar.Controls.Add(this.btnClose);
            this.panelToolbar.Location = new System.Drawing.Point(0, 0);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(678, 75);
            this.panelToolbar.TabIndex = 50;
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.Location = new System.Drawing.Point(3, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(81, 66);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Cerrar (Esc)";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnClose_KeyUp);
            // 
            // checkBoxUsoWeb
            // 
            this.checkBoxUsoWeb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxUsoWeb.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxUsoWeb.Checked = true;
            this.checkBoxUsoWeb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUsoWeb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBoxUsoWeb.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.checkBoxUsoWeb.FlatAppearance.CheckedBackColor = System.Drawing.Color.Green;
            this.checkBoxUsoWeb.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.checkBoxUsoWeb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxUsoWeb.Font = new System.Drawing.Font("Roboto Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxUsoWeb.ForeColor = System.Drawing.Color.White;
            this.checkBoxUsoWeb.Location = new System.Drawing.Point(532, 12);
            this.checkBoxUsoWeb.Name = "checkBoxUsoWeb";
            this.checkBoxUsoWeb.Size = new System.Drawing.Size(134, 31);
            this.checkBoxUsoWeb.TabIndex = 1;
            this.checkBoxUsoWeb.Text = "Uso Web (Sí)";
            this.checkBoxUsoWeb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxUsoWeb.UseVisualStyleBackColor = true;
            this.checkBoxUsoWeb.CheckedChanged += new System.EventHandler(this.checkBoxUsoWeb_CheckedChanged);
            this.checkBoxUsoWeb.KeyUp += new System.Windows.Forms.KeyEventHandler(this.checkBoxUsoWeb_KeyUp);
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
            this.btnGuardar.Location = new System.Drawing.Point(517, 456);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(144, 41);
            this.btnGuardar.TabIndex = 13;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.BtnGuardar_Click);
            this.btnGuardar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnGuardar_KeyUp);
            // 
            // FormConfiguracionGral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(678, 509);
            this.Controls.Add(this.panelToolbar);
            this.Controls.Add(this.editFiscalItemField);
            this.Controls.Add(this.checkBoxFiscales);
            this.Controls.Add(this.groupBoxScalePermission);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormConfiguracionGral";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuración General";
            this.Load += new System.EventHandler(this.FrmConfiguracionGral_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelLeyendas.ResumeLayout(false);
            this.panelLeyendas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnImprimir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBoxScalePermission.ResumeLayout(false);
            this.groupBoxScalePermission.PerformLayout();
            this.panelToolbar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtImpresora;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox btnImprimir;
        private System.Windows.Forms.ComboBox cmbImpresoras;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBoxScalePermission;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxUsoBascula;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox cPuerto;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cBitsxSeg;
        private System.Windows.Forms.ComboBox cParidad;
        private System.Windows.Forms.ComboBox cBitsdatos;
        private System.Windows.Forms.ComboBox cBitsPadada;
        private System.Windows.Forms.TextBox cnombrebascula;
        private RoundedButton btnGuardar;
        private System.Windows.Forms.CheckBox checkBoxCapturaPesoManual;
        private System.Windows.Forms.Panel panelLeyendas;
        private System.Windows.Forms.TextBox editNumCopias;
        private System.Windows.Forms.Label textNumCopias;
        private System.Windows.Forms.TextBox editLeyendaCopia;
        private System.Windows.Forms.TextBox editLeyendaOriginal;
        private System.Windows.Forms.Label textLeyendaCopia;
        private System.Windows.Forms.Label textLeyendaOriginal;
        private System.Windows.Forms.Label textInfoLeyendas;
        private System.Windows.Forms.CheckBox checkBoxFiscales;
        private System.Windows.Forms.CheckBox checkBoxCerrarCOM;
        private System.Windows.Forms.Button btnConfigTicket;
        private System.Windows.Forms.TextBox editFiscalItemField;
        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox checkBoxUsoWeb;
    }
}