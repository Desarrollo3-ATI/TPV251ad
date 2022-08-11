using System;
using System.Windows.Forms;

namespace SyncTPV.Views
{
    public partial class FrmConfirmSale : Form
    {
        public Timer timer = new Timer();
        public double total = 0;
        public double cambio = 0;

        public FrmConfirmSale(double total, double cambio)
        {
            this.total = total;
            this.cambio = cambio;
            InitializeComponent();
        }

        private void FrmConfirmSale_Load(object sender, EventArgs e)
        {
            if (!UserModel.doYouHavePermissionPrepedido())
            {
                textTotalFrmConfirmSale.Text = "Total $ " + MetodosGenerales.obtieneDosDecimales(total) + " MXN";
                textCambioFrmConfirmSale.Text = "Cambio $ " + MetodosGenerales.obtieneDosDecimales(cambio) + " MXN";
            } else
            {
                textTotalFrmConfirmSale.Text = "Documento Guardado Correctamente";
                textCambioFrmConfirmSale.Visible = false;
            }
            timer.Interval = 15000;
            timer.Tick += new EventHandler(timerTickCloseFrmConfirmSale_Tick);
            timer.Start();
            pictureBox1.Image = Properties.Resources.success_green;//ClsMetodosGenerales.redimencionarBitmap(, 276, 186);
        }

        private void timerTickCloseFrmConfirmSale_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOkFrmConfirmSale_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
