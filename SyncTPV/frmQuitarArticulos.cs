using System;
using System.Windows.Forms;

namespace SyncTPV
{
    public partial class frmQuitarArticulos : Form
    {
        int cant = 0;
        int total = 0;
        public frmQuitarArticulos(int cantidad)
        {
            cant = cantidad;
            InitializeComponent();
            NumEliminar.Value = cant;
        }

        private void frmQuitarArticulos_Load(object sender, EventArgs e)
        {
            lblTotal.Text = cant + "";
        }

        private void txtTotal_KeyUp(object sender, KeyEventArgs e)
        {
            string rest = NumEliminar.Value.ToString().Trim();
            if (NumEliminar.Value.ToString().Trim() == "")
                rest = 0 + "";
            total = cant - Convert.ToInt32(rest);

            lblTotal.Text = total + "";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Quitar " + NumEliminar.Value.ToString() + " producto(s)?", "Confirmar acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                //GeneralTxt.cantidadArticulos = total;
                this.Close();
            }
        }

        private void picCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
