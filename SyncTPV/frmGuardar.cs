using System;
using System.Drawing;
using System.Windows.Forms;

namespace SyncTPV
{
    public partial class frmGuardar : Form
    {
        string Folio = "", descripcion = "";

        public frmGuardar(string _folio)
        {
            InitializeComponent();
            Folio = _folio;
            panel1.BackColor = Color.FromArgb(GeneralTxt.Red, GeneralTxt.Green, GeneralTxt.Blue);
        }

        private void frmGuardar_Load(object sender, EventArgs e)
        {
            txtFolio.Text = Folio;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtDescripcion.Text = "";
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtDescripcion.Text == "")
            {
                MessageBox.Show("Es necesario agregar una descripción al documento a guardar");
            }
            else
            {
                this.Close();
            }
        }
    }
}
