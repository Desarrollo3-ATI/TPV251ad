using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace SyncTPV.Views.Mostrador
{
    public partial class FormCodigoAgente : Form
    {
        private FormPayCart formPaycart;
        private FormWaiting formWaiting;
        private int idDocumento = 0;
        private String nombreAgente = "";
        public FormCodigoAgente(FormPayCart formPaycart, int idDocumento,String nombreAgente)
        {
            this.formPaycart = formPaycart;
            this.idDocumento = idDocumento;
            this.nombreAgente = nombreAgente;
            InitializeComponent();
            btnClose.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.back_white, 40, 40);
            btnAceptar.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.btn_aceptar_normal, 148, 39);
        }

        private void FormCodigoAgente_Load(object sender, EventArgs e)
        {
            editCodigoAgente.Focus();
            if (nombreAgente.Equals(""))
                editCodigoCajaPadre.Text = "Agente No Asignado";
            else editCodigoCajaPadre.Text = nombreAgente;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            btnAceptar.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.btn_aceptar_presionado, 148, 39);
            formWaiting = new FormWaiting(this, 0, "Validando Información del Agente...");
            formWaiting.ShowDialog();
        }

        public async Task addOrUpdateAgentInCurrentDocument()
        {
            int value = 0;
            String description = "";
            String codigo = editCodigoAgente.Text.Trim();
            String nombreAgente = "";
            await Task.Run(async () =>
            {
                if (codigo.Equals(""))
                {
                    description = "El código del Agente no puede estar vacio!\r\nIngresar un código válido!";
                }
                else
                {
                    //Validar que el agente existe en la tabla Users obteniendo el id y nombre
                    dynamic responseAgent = UserModel.getAgentByCode(codigo);
                    if (responseAgent.value == 1)
                    {
                        //Llamar método del documento que actualiza el nombre y id del usuario
                        UserModel um = responseAgent.um;
                        if (um != null)
                        {
                            dynamic responseDocumento = DocumentModel.updateAgent(idDocumento, um.id, um.Nombre);
                            if (responseDocumento.value == 1)
                            {
                                value = 1;
                                nombreAgente = um.Nombre;
                            } else
                            {
                                value = responseDocumento.value;
                                description = responseDocumento.description;

                            }
                        } else
                        {
                            description = "El agente con código "+codigo+" es Nulo!";
                        }
                    }
                    else
                    {
                        value = responseAgent.value;
                        description = responseAgent.description;
                    }
                }
            });
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            if (value == 1)
            {
                formPaycart.txtNombreAgente.Text = "Agente: "+nombreAgente;
                PopupNotifier popup = new PopupNotifier();
                popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
                popup.TitleColor = Color.FromArgb(43, 143, 192);
                popup.TitleText = "Agente";
                popup.TitlePadding = new Padding(5, 5, 5, 5);
                popup.ButtonBorderColor = Color.Red;
                popup.ContentText = "Agente Actualizado correctamente!";
                popup.ContentColor = Color.FromArgb(43, 143, 192);
                popup.HeaderHeight = 10;
                popup.AnimationDuration = 1000;
                popup.HeaderColor = Color.FromArgb(200, 244, 255);
                popup.Popup();
                this.Close();
            } else
            {
                btnAceptar.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.btn_aceptar_normal, 148, 39);
                FormMessage formMessage = new FormMessage("Código Agente", description, 3);
                formMessage.ShowDialog();
                editCodigoAgente.Text = "";
                editCodigoAgente.Select();
            }
        }

        private void editCodigoAgente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                formWaiting = new FormWaiting(this, 0, "Validando Información del Agente...");
                formWaiting.ShowDialog();
            }
        }
    }
}
