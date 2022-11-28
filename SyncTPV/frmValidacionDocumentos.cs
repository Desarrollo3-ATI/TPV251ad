using Cripto;
using Newtonsoft.Json.Linq;
using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Threading;
using System.Windows.Forms;

namespace SyncTPV
{
    public partial class FrmValidacionDocumentos : Form
    {
        public bool Acredito = false;
        public FrmValidacionDocumentos()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
                Acredito = true;
                this.Close();
        }

    
        private void button2_Click(object sender, EventArgs e)
        {
            Acredito = false;
            this.Close();
        }


        private void FrmValidacionDocumentos_Load(object sender, EventArgs e)
        {
            Acredito = false;
            //string ultimaCargaInicial = TraerUltimaCargaInicial();
            //lblFechaHora.Text = ultimaCargaInicial;
            
        }

    }
}
