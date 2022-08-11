using System;
using System.Windows.Forms;

namespace SyncTPV
{
    public partial class FrmImageCarga : Form
    {
        public FrmImageCarga()
        {
            InitializeComponent();
        }

        private void FrmImageCarga_Load(object sender, EventArgs e)
        {
            //picCargando.Image = Properties.Resources.iconfinder_correct_3855625;
            //TRYThread.Sleep(5000);
            //this.Close();
            timer1.Enabled = true;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Close();
        }

        private void FrmImageCarga_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
