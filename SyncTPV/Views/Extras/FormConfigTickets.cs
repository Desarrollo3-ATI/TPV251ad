using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncTPV.Views.Extras
{
    public partial class FormConfigTickets : Form
    {
        bool serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
        public bool reporteError = true;
        public FormConfigTickets()
        {
            InitializeComponent();
            btnClose.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.back_white, 35, 35);
            btnGuardar.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.btn_aceptar_normal, 200, 75);
            btnLimpiar.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.update_data_black, 50, 50);
        }

        private void FormConfigTickets_Load(object sender, EventArgs e)
        {
            validateParametersActivateds();
            validateDatosDeEmpresa();
            checkVistaErrores.Checked = Convert.ToBoolean(validateRE());
            reporteError = false;
        }

        public int validateRE()
        {
            //checkBoxVentaRapida
            int ventarapida = 1;
                ventarapida = ConfiguracionModel.validateREActivated();
            return ventarapida;
        }
        private async Task validateDatosDeEmpresa()
        {
            dynamic tk = null; 
            await Task.Run(async () =>
            {
                tk = DatosTicketController.getallDataTicketLAN();
            });
            if(tk != null)
            {
                editEmpresa.Text   = tk.nombre;
                editDireccion.Text = tk.direccion;
                editExpedido.Text = tk.expedido;
                editrfc.Text = tk.rfc;
                editVentaEfectivo.Text = tk.ventaEfectivo;
                editVentaCredito.Text = tk.ventaCredito;
                editPedido.Text = tk.pedidos ;
                editCotizacion.Text = tk.cotizacion;
                editCobranza.Text = tk.cobranza;
                editDevolucion.Text = tk.devolucion;
            }
            else
            {
                Console.WriteLine("nada");
            }
        }
        
        private async Task validateParametersActivateds()
        {
            PrinterModel pm = null;
            await Task.Run(async () =>
            {
                pm = PrinterModel.getallDataFromAPrinter();
            });
            if (pm != null)
            {
                if (pm.showFolio == 1)
                    checkBoxFolio.Checked = true;
                else checkBoxFolio.Checked = false;
                if (pm.showCodigoCaja == 1)
                    checkBoxCodigoCaja.Checked = true;
                else checkBoxCodigoCaja.Checked = false;
                if (pm.showCodigoUsuario == 1)
                    checkBoxCodigoUsuario.Checked = true;
                else checkBoxCodigoUsuario.Checked = false;
                if (pm.showNombreUsuario == 1)
                    checkBoxNombreUsuario.Checked = true;
                else checkBoxNombreUsuario.Checked = false;
                if (pm.showFechaHora == 1)
                    checkBoxFechaHora.Checked = true;
                else checkBoxFechaHora.Checked = false;
                if (pm.showPorcentajeDescuentoMovimiento == 1)
                    checkBoxDescuentoventa.Checked = true;
                else checkBoxDescuentoventa.Checked = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBoxFolio_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFolio.Focused)
            {
                if (checkBoxFolio.Checked)
                {
                    PrinterModel.updateShowEncabezadoField(1, 0, 1);
                    MetodosGenerales.showToastSuccess("Activado", "El Folio se mostrará en el encabezado del ticket");
                } else
                {
                    PrinterModel.updateShowEncabezadoField(1, 0, 0);
                    MetodosGenerales.showToastSuccess("Desactivado", "El Folio No se mostrará en el encabezado del ticket");
                }
            }
        }

        private void checkBoxFolio_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void checkBoxCodigoCaja_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCodigoCaja.Focused)
            {
                if (checkBoxCodigoCaja.Checked)
                {
                    PrinterModel.updateShowEncabezadoField(1, 1, 1);
                    MetodosGenerales.showToastSuccess("Activado", "El Código de la caja se mostrará en el encabezado del ticket");
                } else
                {
                    PrinterModel.updateShowEncabezadoField(1, 1, 0);
                    MetodosGenerales.showToastSuccess("Desactivado", "El Código de la caja No se mostrará en el encabezado del ticket");
                }
            }
        }

        private void checkBoxCodigoUsuario_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCodigoUsuario.Focused)
            {
                if (checkBoxCodigoUsuario.Checked)
                {
                    PrinterModel.updateShowEncabezadoField(1, 2, 1);
                    MetodosGenerales.showToastSuccess("Activado", "El Código del usuario se mostrará en el encabezado del ticket");
                } else
                {
                    PrinterModel.updateShowEncabezadoField(1, 2, 0);
                    MetodosGenerales.showToastSuccess("Desactivado", "El Código del usuario No se mostrará en el encabezado del ticket");
                }
            }
        }

        private void btnClose_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void checkBoxNombreUsuario_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxNombreUsuario.Focused)
            {
                if (checkBoxNombreUsuario.Checked)
                {
                    PrinterModel.updateShowEncabezadoField(1, 3, 1);
                    MetodosGenerales.showToastSuccess("Activado", "El nombre del usuario se mostrará en el encabezado del ticket");
                } else
                {
                    PrinterModel.updateShowEncabezadoField(1, 3, 0);
                    MetodosGenerales.showToastSuccess("Desactivado", "El nombre del usuario No se mostrará en el encabezado del ticket");
                }
            }
        }

        private void checkBoxFechaHora_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFechaHora.Focused)
            {
                if (checkBoxFechaHora.Checked)
                {
                    PrinterModel.updateShowEncabezadoField(1, 4, 1);
                    MetodosGenerales.showToastSuccess("Desactivado", "La fecha y hora se mostrará en el encabezado del ticket");
                } else
                {
                    PrinterModel.updateShowEncabezadoField(1, 4, 0);
                    MetodosGenerales.showToastSuccess("Desactivado", "La fecha y hora No se mostrará en el encabezado del ticket");
                }
            }
        }

        private void checkBoxCodigoUsuario_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void checkBoxCodigoCaja_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void checkBoxNombreUsuario_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void checkBoxFechaHora_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            btnGuardar.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.btn_aceptar_presionado, 200, 75);
            FormPasswordConfirmation formPasswordConfirmation = new FormPasswordConfirmation("Acceso Supervisor", "Ingresa la contraseña del supervisor");
            formPasswordConfirmation.StartPosition = FormStartPosition.CenterScreen;
            formPasswordConfirmation.ShowDialog();
            if (FormPasswordConfirmation.permissionGranted)
            {
            
                try
                {
                   
                    bool bandera = false;
                    dynamic respuesta = null;
                    String empresa = editEmpresa.Text;
                    String direccion = editDireccion.Text;
                    String rfc = editrfc.Text;
                    String expend = editExpedido.Text;

                    String efectivo = editVentaEfectivo.Text;
                    String credito = editVentaCredito.Text;
                    String cotizacion = editCotizacion.Text;
                    String cobranza = editCobranza.Text;
                    String pedido = editPedido.Text;
                    String devolucion = editDevolucion.Text;

                    await Task.Run(async () =>
                    {
                        respuesta = DatosTicketController.updateallDataTicketTPVLAN(
                            empresa,direccion,rfc,expend,
                            efectivo,credito,cotizacion,cobranza,pedido,devolucion);
                    });
                    if (respuesta != null)
                    {
                        bandera = (bool)respuesta.value;
                        if (bandera)
                        {
                            MetodosGenerales.showToastSuccess("Guardado", respuesta.descripcion);
                        }
                        else
                        {
                            MetodosGenerales.showToastSuccess("Error al guardar", respuesta.descripcion);
                        }
                    }
                }
                catch(Exception error)
                {
                    SECUDOC.writeLog(error.ToString());
                }
            }
            btnGuardar.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.btn_aceptar_normal, 200, 75);
        }

        private async void btnLimpiar_Click(object sender, EventArgs e)
        {
            btnLimpiar.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.update_data_black, 60, 60);

            FormPasswordConfirmation formPasswordConfirmation = new FormPasswordConfirmation("Acceso Supervisor", "Ingresa la contraseña del supervisor");
            formPasswordConfirmation.StartPosition = FormStartPosition.CenterScreen;
            formPasswordConfirmation.ShowDialog();
            if (FormPasswordConfirmation.permissionGranted)
            {
               
                if (serverModeLAN)
                    await DatosTicketController.downloadAllDatosTicketLAN();
                else
                {
                    await DatosTicketController.downloadAllDatosTicketAPI();
                }
     
                validateDatosDeEmpresa();
            }
            btnLimpiar.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.update_data_black, 50, 50);
        }

        private void checkBoxDescuentoventa_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDescuentoventa.Focused)
            {
                if (checkBoxDescuentoventa.Checked)
                {
                    PrinterModel.updateShowEncabezadoField(1, 5, 1);
                    MetodosGenerales.showToastSuccess("Desactivado", "El porcentaje de los descuentos se mostrará en el encabezado del ticket");
                }
                else
                {
                    PrinterModel.updateShowEncabezadoField(1, 5, 0);
                    MetodosGenerales.showToastSuccess("Desactivado", "El porcentaje de los descuentos no se mostrará en el encabezado del ticket");
                }
            }
        }

        private void checkVistaErrores_CheckedChanged(object sender, EventArgs e)
        {
            //bool cambio = checkBoxVentaRapida.Checked;
            if (!reporteError)
            {
                FormPasswordConfirmation fpc = new FormPasswordConfirmation("Autorización del Supervisor", "Ingresar Contraseña");
                fpc.StartPosition = FormStartPosition.CenterScreen;
                fpc.ShowDialog();
                if (FormPasswordConfirmation.permissionGranted)
                {
                    if (checkVistaErrores.Checked)
                    {
                        String query = "UPDATE " + LocalDatabase.TABLA_CONFIGURACION + " SET " + LocalDatabase.CAMPO_REPORTEERROR_CONFIG
                        + " = " + 1 + " WHERE id = 1";
                        ConfiguracionModel.updateServerMode(query);
                    }
                    else
                    {
                        String query = "UPDATE " + LocalDatabase.TABLA_CONFIGURACION + " SET " + LocalDatabase.CAMPO_REPORTEERROR_CONFIG
                        + " = " + 0 + " WHERE id = 1";
                        ConfiguracionModel.updateServerMode(query);

                    }
                }
                else
                {
                    reporteError = true;
                    checkVistaErrores.Checked = !checkVistaErrores.Checked;
                    reporteError = false;
                }
            }
        }
    }
}
