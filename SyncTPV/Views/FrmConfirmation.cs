using System;
using System.Windows.Forms;

namespace SyncTPV.Views
{
    public partial class FrmConfirmation : Form
    {
        public static Boolean confirmation = false;
        private String title = "";
        private String message = "";
        public FrmConfirmation(String title, String message)
        {
            InitializeComponent();
            this.title = title;
            this.message = message;
        }

        private void FrmConfirmation_Load(object sender, EventArgs e)
        {
            this.Text = title;
            textMsgFrmConfirmation.Text = message;
            confirmation = false;
        }

        private void btnCancelFrmConfirmation_Click(object sender, EventArgs e)
        {
            confirmation = false;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            confirmation = true;
            this.Close();
        }

        private void FrmConfirmation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                confirmation = true;
                this.Close();
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Escape))
            {
                confirmation = false;
                this.Close();
            }
        }

        private void btnOkFrmConfirmation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                confirmation = true;
                this.Close();
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Escape))
            {
                confirmation = false;
                this.Close();
            }
        }

        private void btnCancelFrmConfirmation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                confirmation = true;
                this.Close();
            } else if (e.KeyChar == Convert.ToChar(Keys.Escape))
            {
                confirmation = false;
                this.Close();
            }
        }
    }
}
