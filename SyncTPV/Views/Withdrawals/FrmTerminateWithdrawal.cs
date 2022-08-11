using SyncTPV.Helpers.SqliteDatabaseHelper;
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

namespace SyncTPV.Views.Withdrawals
{
    public partial class FrmTerminateWithdrawal : Form
    {
        public static readonly int TERMINAR_RETIRO = 0;
        public static readonly int TERMINAR_INGRESO = 1;
        public static Boolean confirmation = false;
        private int idRetiroOIngreso = 0, call = 0;
        String message = "";

        public FrmTerminateWithdrawal(int idRetiroOIngreso, String message, int call)
        {
            this.idRetiroOIngreso = idRetiroOIngreso;
            InitializeComponent();
            this.message = message;
            this.call = call;
            editConcept.Focus();
            if (call == TERMINAR_RETIRO)
            {
                this.Text = "Terminar Retiro";
            } else if (call == TERMINAR_INGRESO)
            {
                this.Text = "Terminar Ingreso";
            }
        }

        private void FrmTerminateWithdrawal_Load(object sender, EventArgs e)
        {
            String information = "Confirmación de Importes\r\n";
            editTotales.Text = information + message;
            confirmation = false;
            editConcept.Select();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            confirmation = false;
            this.Close();
        }

        private void editConcept_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                saveRetiroOrIngreso();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            saveRetiroOrIngreso();
        }

        private void editDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                saveRetiroOrIngreso();
            }
        }

        private async Task saveRetiroOrIngreso()
        {
            String concept = editConcept.Text.Trim();
            if (concept.Equals(""))
            {
                if (call == TERMINAR_RETIRO)
                {
                    FormMessage fm = new FormMessage("Datos Requeridos", "Tienes que agregar al menos el concepto o motivo del Retiro", 3);
                    fm.ShowDialog();
                }
                else if (call == TERMINAR_INGRESO)
                {
                    FormMessage fm = new FormMessage("Datos Requeridos", "Tienes que agregar al menos el concepto o motivo del Ingreso", 3);
                    fm.ShowDialog();
                }
            }
            else
            {
                String description = editDescription.Text.Trim();
                if (call == TERMINAR_RETIRO)
                {
                    if (RetiroModel.updateConcetpAndDescription(idRetiroOIngreso, concept, description))
                    {
                        confirmation = true;
                        this.Close();
                    }
                    else
                    {
                        FormMessage fm = new FormMessage("Retiro no Encontrado", "No pudimos encontrar el retiro en turno", 2);
                        fm.ShowDialog();
                    }
                }
                else if (call == TERMINAR_INGRESO)
                {
                    if (IngresoModel.updateConcetpAndDescription(idRetiroOIngreso, concept, description))
                    {
                        confirmation = true;
                        this.Close();
                    }
                    else
                    {
                        FormMessage fm = new FormMessage("Ingreso no Encontrado", "No pudimos encontrar el Ingreso en turno", 2);
                        fm.ShowDialog();
                    }
                }
            }
        }
    }
}
