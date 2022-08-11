using System;
using System.Windows.Forms;

namespace SyncTPV
{
    public partial class frmCatalogoDoc : Form
    {
        public frmCatalogoDoc()
        {
            InitializeComponent();
        }

        private void frmCatalogoDoc_Load(object sender, EventArgs e)
        {
            label1.Text = "Elegir el Tipo de \n Documento a crear";
        }

        private void picCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
