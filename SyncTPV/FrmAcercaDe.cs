using Cripto;
using SyncTPV.Models;
using System;
using System.Windows.Forms;

namespace SyncTPV
{
    public partial class FrmAcercaDe : Form
    {
        string MensajeError = "";
        int diasRestantes = 0;
        public FrmAcercaDe()
        {
            InitializeComponent();
        }

        private void FrmAcercaDe_Load(object sender, EventArgs e)
        {
            editCodigoSitio.Text = AdminDll.BajoNivel.getCodigoSitio();
            String endDate = AES.Desencriptar(LicenseModel.getEndDateEncryptFromTheLocalDb()); //LlenarAcercaDe().Split('|');

            lblRespLicencia.Text = "SYNCTPV";
            lblRespVigencia.Text = endDate;

            TimeSpan cont = clsGeneral.changeLicenseEndDateFormat(endDate) - clsGeneral.changeLicenseEndDateFormat(DateTime.Now.ToString("dd/MM/yyyy"));
            diasRestantes = cont.Days;
            lblRespRestante.Text = diasRestantes + "";
        }

        private void picCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picCerrar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
