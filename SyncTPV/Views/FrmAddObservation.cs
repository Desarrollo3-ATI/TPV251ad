using System;
using System.Windows.Forms;

namespace SyncTPV.Views
{
    public partial class FrmAddObservation : Form
    {
        private FormPayCart fpc;

        public FrmAddObservation(FormPayCart fpc)
        {
            this.fpc = fpc;
            InitializeComponent();
        }

        private void FrmAddObservation_Load(object sender, EventArgs e)
        {
            if (!FormPayCart.documentObservation.Equals(""))
                editObservationFrmAddObservation.Text = FormPayCart.documentObservation;
        }

        private void btnCancelarFrmAddObservation_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOkFrmAddObservation_Click(object sender, EventArgs e)
        {
            String observation = editObservationFrmAddObservation.Text.ToString().Trim();
            FormPayCart.documentObservation = observation;
            fpc.fillDocumentInformation(FormPayCart.CALL_UPDATE_OBSERVATION_DOCUMENT_DATA, 0);
            this.Close();
        }
    }
}
