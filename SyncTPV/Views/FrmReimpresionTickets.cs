using SyncTPV.Controllers;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Xml.Linq;
using Tulpep.NotificationWindow;
using Windows.System;
using static SyncTPV.Views.FrmReimpresionTickets;

namespace SyncTPV.Views
{
    public partial class FrmReimpresionTickets : Form
    {
        public static Boolean confirmation = false;
        private static String fecha = "", fechamax = "";
        private static String referencia = "";
        private static int idAgente = 0, limite = 1000;
        private static String tipoDocumento = "Todos";
        private static bool usarfecha = false;
        public FrmReimpresionTickets()
        {
            InitializeComponent();
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

        private void dataGridItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                String ticket = gridTickets.CurrentRow.Cells["GridTicket"].Value.ToString();
                if (!ticket.Equals("") || ticket != null)
                {
                    string printer = "";
                    PrinterModel pm = PrinterModel.getallDataFromAPrinter();
                    if (pm != null)
                    {
                        printer = pm.nombre;
                        String auxLinea = ticket;
                        auxLinea = auxLinea.Replace(">.<", "\u001b").Replace(">u<", "\u0001").Replace(">o<", "\0");
                        if (printer == "Microsoft XPS Document Writer")
                        {
                            auxLinea = auxLinea.Replace("\u001bE\u0001", "").Replace("\u001bE\0", "").Replace("\u001bm", "").Replace("\u001bd\u0001", "").Replace("\u001bp0ᐔ", "");
                        }

                        RawPrinterHelper.SendStringToPrinter(printer, auxLinea,true);
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo imprimir el tickets, este no existe en el erespaldo de la empresa");
                }
            }
        }
        private async void FrmConfirmation_Load_1(object sender, EventArgs e)
        {
            confirmation = true;
            chckUsarFecha.Checked = true;
            FechaMaxima.Text = MetodosGenerales.getCurrentDateAndHour();
            FechaTickets.Text = MetodosGenerales.getCurrentDateAndHour();
            validarAgentes();
            validarTipos();
            toolTip1.SetToolTip(comboLimite, "Sin limite, hacer uso del: 0");
        }

        public void ActualizarTotal(int registros)
        {
            TextTotal.Text = "Total: "+registros.ToString();
        }

        private async Task validarAgentes()
        {
            List<UserModel> usersList = await getAllUsers();
            UserModel todos = new UserModel();
            todos.id = 0;
            todos.Nombre = "Todos";
            if (usersList != null)
            {
                usersList.Add(todos);
            }
            else
            {
                usersList = new List<UserModel>();
                usersList.Add(todos);
            }
            if (usersList != null)
            {
                //Setup data binding
                this.ComboAgentes.DataSource = usersList;
                this.ComboAgentes.ValueMember = "id";
                this.ComboAgentes.DisplayMember = "Nombre";
                // make it readonly
                //this.ComboAgentes.DropDownStyle = ComboBoxStyle.DropDownList;
                ComboAgentes.SelectedIndex = ComboAgentes.Items.Count - 1;
            }
        }
        private async Task<List<UserModel>> getAllUsers()
        {
            List<UserModel> usersList = null;
            await Task.Run(async () =>
            {
                if (LicenseModel.isItTPVLicense())
                    usersList = UserModel.getAllTellers();
                else usersList = UserModel.getAllAgents();
            });
            return usersList;
        }
        public class tipos
        {
            public tipos(){ }
                public tipos(String tipo)
            {
                this.Tipo = tipo;
            }
            public String Tipo { set; get; }
        }

        public void validarTipos()
        {
            /*if (dtm == null)
            {
                dynamic responseDtm = null;
                if (serverModeLAN)
                    responseDtm = await DatosTicketController.downloadAllDatosTicketLAN();
                else responseDtm = await DatosTicketController.downloadAllDatosTicketAPI();
                if (responseDtm.value == 1)
                {
                    dtm = DatosTicketModel.getAllData();
                }
            }*/
            List<tipos> tiposDoc = new List<tipos>();
            int documentType = 2;
            String nameDocumentType = "";
            String pieTicket = "";
            tipos tipox = null;

            tipox = new tipos("VENTA");
            tiposDoc.Add(tipox);
            tipox = new tipos("PRUEBA");
            tiposDoc.Add(tipox);
            tipox = new tipos("INGRESO");
            tiposDoc.Add(tipox);
            tipox = new tipos("CORTE");
            tiposDoc.Add(tipox);
            tipox = new tipos("CAJON-ABRIR");
            tiposDoc.Add(tipox);
            tipox = new tipos("Todos");
            tiposDoc.Add(tipox);

            //Setup data binding
            this.comboTiposDocumentos.DataSource = tiposDoc;
            this.comboTiposDocumentos.ValueMember = "Tipo";
            this.comboTiposDocumentos.DisplayMember = "Tipo";
            // make it readonly
            //this.comboTiposDocumentos.DropDownStyle = ComboBoxStyle.DropDownList;

            comboTiposDocumentos.SelectedIndex = comboTiposDocumentos.Items.Count - 1;
        }

        private void ComboAgentes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var um = (UserModel)ComboAgentes.SelectedItem;
            if (um != null)
            {
                idAgente = um.id;
                PopupNotifier popup = new PopupNotifier();
                popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.caution, 100, 100);
                popup.TitleColor = Color.Red;
                popup.TitleText = "Datos";
                popup.ContentText = "Documentos de " + um.Nombre + " por usuario";
                popup.ContentColor = Color.Red;
                popup.Popup();
            }
        }

        private async void btnBuscarReportes_Click(object sender, EventArgs e)
        {
            fecha = (FechaTickets.Value).ToString("yyyy-MM-dd");
            fechamax = (FechaMaxima.Value).ToString("yyyy-MM-dd");
            referencia = comboReferencia.Text;
            limite = int.Parse(comboLimite.Text.ToString());
            SendDataService sds = new SendDataService();
            dynamic response = await sds.obtenerTicketsDelWs(
                usarfecha, fecha,fechamax, idAgente, referencia, tipoDocumento, limite
            );
            //gridTickets.DataSource = response;
            try
            {
                gridTickets.Rows.Clear();
                if (response.respuesta != null)
                {
                    dynamic respuesta = response.respuesta;
                    ActualizarTotal(respuesta.Count);
                    for (int i = 0; i < respuesta.Count; i++)
                    {
                        gridTickets.Rows.Add();
                        gridTickets.Rows[i].Cells["GridId"].Value = (i+1).ToString();
                        gridTickets.Rows[i].Cells["GridReferencia"].Value = respuesta[i].referencia;
                        String nombreAgente = UserModel.getNameUser((int) respuesta[i].idAgente);
                        gridTickets.Rows[i].Cells["GridAgente"].Value = nombreAgente;
                        gridTickets.Rows[i].Cells["GridTipo"].Value = respuesta[i].tipoDocumento;
                        gridTickets.Rows[i].Cells["GridFecha"].Value = respuesta[i].fecha;
                        if(((int)respuesta[i].estatus) == 0)
                        {
                            gridTickets.Rows[i].Cells["GridEstatus"].Value = "Activo";
                        }
                        else
                        {
                            gridTickets.Rows[i].Cells["GridEstatus"].Value = "No activo";
                        }
                        gridTickets.Rows[i].Cells["GridImprimir"].Value = "Imprimir".ToString();
                        gridTickets.Rows[i].Cells["GridTicket"].Value = respuesta[i].datos;
                    }
                    gridTickets.PerformLayout();
                }
                else
                {
                    ActualizarTotal(0);
                    MessageBox.Show("No hay datos cargados");
                }
            }
            catch(Exception es)
            {
                SECUDOC.writeLog(es.ToString());
            }
            //MessageBox.Show("id "+idAgente+" fecha "+fecha+" tipo "+tipoDocumento+" referencia "+referencia+".");
        }


        private void comboTiposDocumentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipoDocumento = comboTiposDocumentos.Text.ToString();
        }

        private void chckBDlocal_CheckedChanged(object sender, EventArgs e)
        {
            if (chckUsarFecha.Checked)
            {
                usarfecha = true;
            }
            else
            {
                usarfecha = false;
            }
        }

        private void FechaTickets_ValueChanged(object sender, EventArgs e)
        {
            fecha = (FechaTickets.Value).ToString("yyyy-MM-dd");
        }
        private void FechaMaxima_ValueChanged(object sender, EventArgs e)
        {
            fechamax = (FechaMaxima.Value).ToString("yyyy-MM-dd");
        }
    }
}
