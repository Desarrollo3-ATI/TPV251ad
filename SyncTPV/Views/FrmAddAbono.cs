using SyncTPV.Controllers;
using SyncTPV.Models;
using System;
using System.Dynamic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncTPV.Views
{
    public partial class FrmAddAbono : Form
    {
        public int idDocument { get; set; }
        public int idFc { get; set; }
        private FormPayCart fpc;

        public FrmAddAbono(FormPayCart fpc, int idDocument, int idFc)
        {
            this.fpc = fpc;
            this.idDocument = idDocument;
            this.idFc = idFc;
            InitializeComponent();
        }

        private void addNewAbono()
        {
            String importeText = editImporteFrmAddAbono.Text.Trim();
            if (importeText.Equals(""))
            {
                FormMessage fm = new FormMessage("Importe No Encontrado", "Debes ingresar un importe válido! ", 2);
                fm.ShowDialog();
            }
            else
            {
                try
                {
                    double amount = Convert.ToDouble(importeText.Replace(",", ""));
                    if (amount >= 0)
                    {
                        dynamic map = new ExpandoObject();
                        map.fcId = Convert.ToInt32(idFc);
                        map.importe = amount;
                        map.idDocument = idDocument;
                        callAddWaysToCollectTask(map);
                    }
                    else
                    {
                        FormMessage fm = new FormMessage("Alerta", "El importe tiene que ser mayor o igual a cero!", 2);
                        fm.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    FormMessage fm = new FormMessage("Importe No Encontrado", "Debes ingresar solo números! ", 2);
                    fm.ShowDialog();
                }
            }
        }

        private async Task callAddWaysToCollectTask(dynamic map)
        {
            AddNewWaysToCollectController anwc = new AddNewWaysToCollectController();
            int response = await anwc.doInBackground(map);
            if (response == -1)
            {
                FormMessage fm = new FormMessage("Alerta", "Deberías agregar un importe mayor a cero", 2);
                fm.ShowDialog();
            }
            else if (response == 0)
            {
                FormMessage fm = new FormMessage("Alerta", "No se puede saldar un documento a crédito!", 2);
                fm.ShowDialog();
            }
            await fpc.fillDocumentInformation(FormPayCart.CALL_GET_DOCUMENT_DATA, 0);
            this.Close();
        }

        private void FrmAddAbono_Load(object sender, EventArgs e)
        {
            String nameFc = FormasDeCobroModel.getANameFrromAFomaDeCobroWithId(idFc);
            textNombreFormaDeCobro.Text = nameFc.Trim();
            double importeAbonado = FormasDeCobroDocumentoModel.getTheImportAbonadoForAFormaDeCobroOfADocument(idFc, idDocument);
            if (importeAbonado == 0)
            {
                double saldoPendiente = FormasDeCobroDocumentoModel.getSaldoPendienteOfTheLastFcDcoument(idDocument);
                String saldoPtexto = Convert.ToString(saldoPendiente);
                saldoPtexto = saldoPtexto.Replace(",", "");
                double saldoPendienteSinComas = Convert.ToDouble(saldoPtexto);
                editImporteFrmAddAbono.Text = ("" + saldoPendienteSinComas);
            }
            else
            {
                String impAbono = Convert.ToString(importeAbonado);
                impAbono = impAbono.Replace(",", "");
                double impAbonado = Convert.ToDouble(impAbono);
                editImporteFrmAddAbono.Text = ("" + impAbonado);
            }
        }

        private void btnAbonarFrmAddAbono_Click_1(object sender, EventArgs e)
        {
            addNewAbono();
        }

        private void FrmAddAbono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Escape))
                this.Close();
        }

        private void editImporteFrmAddAbono_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Escape))
                this.Close();
            else if (e.KeyChar == Convert.ToChar(Keys.Enter))
                addNewAbono();
        }
    }
}
