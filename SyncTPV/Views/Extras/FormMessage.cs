using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using wsROMClases.Models.Panel;

namespace SyncTPV
{
    public partial class FormMessage : Form
    {
        string message = "";
        string titulo = "", query = "";
        int tipo = 0, idDocumento = 0, Error = 0;
        List<ClsMovimientosModel> movs = new List<ClsMovimientosModel>();
        public List<int> listDocumentosSeleccionadosCancelar = new List<int>();

        public FormMessage(string tittle, string msj, int tipo)
        {
            InitializeComponent();
            message = msj;
            titulo = tittle;
            this.tipo = tipo;
            btnAceptar.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.btn_aceptar_normal, 120, 40);
        }

        private void FormMessage_Load(object sender, EventArgs e)
        {

            string[] valores = titulo.Split('|');

            if (valores.Length > 1)
                idDocumento = Convert.ToInt32(valores[1]);
            lbl1.Text = message;
            lbl2.Text = titulo;
            if (tipo == 2)
            {
                this.Text = titulo;
                pnlSup.BackColor = Color.Crimson;
                imgMessage.Visible = true;
                imgMessage.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.warning_red, 104, 104);
            }
            else if (tipo == 1)
            {
                this.Text = titulo;
                pnlSup.BackColor = Color.SeaGreen;
                imgMessage.Visible = true;
                imgMessage.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 104, 104);
            }
            else if (tipo == 3)
            {
                this.Text = titulo;
                imgMessage.Visible = true;
                imgMessage.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.warning_yellow, 104, 104);
            }
            else if (tipo == 4)
            {
                pnlSup.BackColor = Color.FromArgb(242, 235, 93);
                //btnAceptar.BackColor = Color.FromArgb(98, 98, 96);
                btnAceptar.Text = "Cancelar";
                lbl2.ForeColor = Color.Black;
                lbl2.Text = valores[0];
            }
            lbl1.Text = message;
            lbl2.Text = titulo;
            lbl1.Focus();
        }

        private void lbl1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            } else if (e.KeyCode == Keys.Enter)
            {
                this.Close();
            }
        }

        private void btnAceptar_Click_1(object sender, EventArgs e)
        {
            btnAceptar.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.btn_aceptar_presionado, 120, 40);
            this.Close();
        }

        private void picCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picCopy_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetDataObject(lbl1.Text, true);
                FrmImageCarga carga = new FrmImageCarga();
                carga.Show();
            }
            catch (Exception ex)
            {
                SECUDOC.writeLog(ex.ToString());
            }
        }

    }
}
