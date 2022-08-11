using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using SyncTPV.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncTPV
{
    public partial class FrmOpcionesDocumento : Form
    {
        int idDocument = 0;
        public static Boolean cencelDocument = false;
        private bool permissionPrepedido = false;

        public FrmOpcionesDocumento(int idDocument, FrmResumenDocuments frmResumenDocuments, int queryType)
        {
            this.idDocument = idDocument;
            InitializeComponent();
            btnCancelarDocumentFrmOpcionesDoc.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.cancel_black, 30, 30);
            btnVerMovimientos.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.lista_cuadrada_black, 30, 30);
            btnPrintTicket.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.printer_black, 30, 30);
            btnModificarDocumento.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.edit_black, 30, 30);
        }

        private void picCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ver_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void modificar_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void btnCancelarDocument_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void FrmOpcionDoc_Load(object sender, EventArgs e)
        {
            
        }

      
    }
}