using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncTPV.Views.Extras
{
    public partial class FormConfigTickets : Form
    {
        public FormConfigTickets()
        {
            InitializeComponent();
            btnClose.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.back_white, 35, 35);
        }

        private void FormConfigTickets_Load(object sender, EventArgs e)
        {
            validateParametersActivateds();
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
    }
}
