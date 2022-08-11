using Cripto;
using SyncTPV.Models;
using System;
using System.Windows.Forms;

namespace SyncTPV
{
    public partial class FrmRuta : Form
    {
        //string ruta = "";
        FormMessage msj;
        public FrmRuta()
        {
            //ruta = rutaString;
            InitializeComponent();
        }

        private async void btnAceptarRuta_Click(object sender, EventArgs e)
        {
            bool licenseActive = await clsGeneral.isTheLicenseValid(AES.Desencriptar(LicenseModel.getEndDateEncryptFromTheLocalDb()), "");
            if (!licenseActive)
            {
                msj = new FormMessage("Licencia Expirada", "Su licencia ha expirado.", 2);
                msj.ShowDialog();
            }
            else
            {
                GeneralTxt.Ruta = txtRuta.Text.Trim();
                if (GeneralTxt.routeWithTpv != GeneralTxt.Ruta)
                {
                    //MessageBox.Show("Tienes que escribir una ruta valida", "Ruta Incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    msj = new FormMessage("Ruta Incorrecta", "Debes ingresar una ruta valida", 2);
                    msj.ShowDialog();
                    txtRuta.Text = "";
                    FormPrincipal.doInitialCharge = false;
                }
                else
                {
                    FormPrincipal.doInitialCharge = true;
                    this.Close();
                }
            }
        }

        private void picCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmRuta_Load(object sender, EventArgs e)
        {
            txtRuta.Text = GeneralTxt.routeWithTpv;
        }
    }
}
