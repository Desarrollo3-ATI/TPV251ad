namespace SyncTPV.Views.Taras
{
    partial class FormAddNewTara
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddNewTara));
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.textAyudaTipo = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.textTipo = new System.Windows.Forms.Label();
            this.editTipo = new System.Windows.Forms.TextBox();
            this.textPeso = new System.Windows.Forms.Label();
            this.editPeso = new System.Windows.Forms.TextBox();
            this.textColor = new System.Windows.Forms.Label();
            this.editColor = new System.Windows.Forms.TextBox();
            this.textNombre = new System.Windows.Forms.Label();
            this.editNombre = new System.Windows.Forms.TextBox();
            this.textCodigo = new System.Windows.Forms.Label();
            this.editCodigo = new System.Windows.Forms.TextBox();
            this.panelToolbar.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelToolbar
            // 
            this.panelToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelToolbar.BackColor = System.Drawing.Color.Coral;
            this.panelToolbar.Controls.Add(this.btnClose);
            this.panelToolbar.Location = new System.Drawing.Point(-1, -1);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(549, 76);
            this.panelToolbar.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Roboto Black", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::SyncTPV.Properties.Resources.close;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.Location = new System.Drawing.Point(13, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 70);
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
            this.panelContent.BackColor = System.Drawing.Color.FloralWhite;
            this.panelContent.Controls.Add(this.textAyudaTipo);
            this.panelContent.Controls.Add(this.btnSave);
            this.panelContent.Controls.Add(this.textTipo);
            this.panelContent.Controls.Add(this.editTipo);
            this.panelContent.Controls.Add(this.textPeso);
            this.panelContent.Controls.Add(this.editPeso);
            this.panelContent.Controls.Add(this.textColor);
            this.panelContent.Controls.Add(this.editColor);
            this.panelContent.Controls.Add(this.textNombre);
            this.panelContent.Controls.Add(this.editNombre);
            this.panelContent.Controls.Add(this.textCodigo);
            this.panelContent.Controls.Add(this.editCodigo);
            this.panelContent.Location = new System.Drawing.Point(-1, 78);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(549, 328);
            this.panelContent.TabIndex = 1;
            // 
            // textAyudaTipo
            // 
            this.textAyudaTipo.AutoSize = true;
            this.textAyudaTipo.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textAyudaTipo.Location = new System.Drawing.Point(30, 265);
            this.textAyudaTipo.Name = "textAyudaTipo";
            this.textAyudaTipo.Size = new System.Drawing.Size(174, 13);
            this.textAyudaTipo.TabIndex = 11;
            this.textAyudaTipo.Text = "Debe ser una letra del abecedario";
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(377, 265);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(172, 63);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Crear";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // textTipo
            // 
            this.textTipo.AutoSize = true;
            this.textTipo.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTipo.Location = new System.Drawing.Point(27, 213);
            this.textTipo.Name = "textTipo";
            this.textTipo.Size = new System.Drawing.Size(32, 14);
            this.textTipo.TabIndex = 9;
            this.textTipo.Text = "Tipo";
            // 
            // editTipo
            // 
            this.editTipo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.editTipo.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editTipo.Location = new System.Drawing.Point(30, 240);
            this.editTipo.MaxLength = 1;
            this.editTipo.Name = "editTipo";
            this.editTipo.Size = new System.Drawing.Size(151, 23);
            this.editTipo.TabIndex = 8;
            this.editTipo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editTipo_KeyPress);
            // 
            // textPeso
            // 
            this.textPeso.AutoSize = true;
            this.textPeso.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textPeso.Location = new System.Drawing.Point(319, 128);
            this.textPeso.Name = "textPeso";
            this.textPeso.Size = new System.Drawing.Size(35, 14);
            this.textPeso.TabIndex = 7;
            this.textPeso.Text = "Peso";
            // 
            // editPeso
            // 
            this.editPeso.Font = new System.Drawing.Font("Roboto Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editPeso.Location = new System.Drawing.Point(322, 155);
            this.editPeso.Name = "editPeso";
            this.editPeso.Size = new System.Drawing.Size(151, 26);
            this.editPeso.TabIndex = 6;
            this.editPeso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editPeso_KeyPress);
            // 
            // textColor
            // 
            this.textColor.AutoSize = true;
            this.textColor.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textColor.Location = new System.Drawing.Point(27, 128);
            this.textColor.Name = "textColor";
            this.textColor.Size = new System.Drawing.Size(37, 14);
            this.textColor.TabIndex = 5;
            this.textColor.Text = "Color";
            // 
            // editColor
            // 
            this.editColor.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editColor.Location = new System.Drawing.Point(30, 155);
            this.editColor.MaxLength = 15;
            this.editColor.Name = "editColor";
            this.editColor.Size = new System.Drawing.Size(151, 23);
            this.editColor.TabIndex = 4;
            // 
            // textNombre
            // 
            this.textNombre.AutoSize = true;
            this.textNombre.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textNombre.Location = new System.Drawing.Point(219, 22);
            this.textNombre.Name = "textNombre";
            this.textNombre.Size = new System.Drawing.Size(51, 14);
            this.textNombre.TabIndex = 3;
            this.textNombre.Text = "Nombre";
            // 
            // editNombre
            // 
            this.editNombre.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editNombre.Location = new System.Drawing.Point(222, 49);
            this.editNombre.MaxLength = 20;
            this.editNombre.Name = "editNombre";
            this.editNombre.Size = new System.Drawing.Size(313, 23);
            this.editNombre.TabIndex = 2;
            // 
            // textCodigo
            // 
            this.textCodigo.AutoSize = true;
            this.textCodigo.Font = new System.Drawing.Font("Roboto Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textCodigo.Location = new System.Drawing.Point(27, 22);
            this.textCodigo.Name = "textCodigo";
            this.textCodigo.Size = new System.Drawing.Size(46, 14);
            this.textCodigo.TabIndex = 1;
            this.textCodigo.Text = "Código";
            // 
            // editCodigo
            // 
            this.editCodigo.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editCodigo.Location = new System.Drawing.Point(30, 49);
            this.editCodigo.Name = "editCodigo";
            this.editCodigo.Size = new System.Drawing.Size(151, 23);
            this.editCodigo.TabIndex = 0;
            // 
            // FormAddNewTara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(546, 404);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelToolbar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(562, 443);
            this.Name = "FormAddNewTara";
            this.Text = "Agregar Nueva Tara";
            this.Load += new System.EventHandler(this.FormAddNewTara_Load);
            this.panelToolbar.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label textCodigo;
        private System.Windows.Forms.TextBox editCodigo;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label textTipo;
        private System.Windows.Forms.TextBox editTipo;
        private System.Windows.Forms.Label textPeso;
        private System.Windows.Forms.TextBox editPeso;
        private System.Windows.Forms.Label textColor;
        private System.Windows.Forms.TextBox editColor;
        private System.Windows.Forms.Label textNombre;
        private System.Windows.Forms.TextBox editNombre;
        private System.Windows.Forms.Label textAyudaTipo;
    }
}