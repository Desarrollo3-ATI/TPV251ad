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

namespace SyncTPV.Views.Taras
{
    public partial class FormAddNewTara : Form
    {
        private FrmTaras frmTaras;
        public FormAddNewTara(FrmTaras frmTaras)
        {
            InitializeComponent();
            this.frmTaras = frmTaras;
        }

        private void FormAddNewTara_Load(object sender, EventArgs e)
        {
            int lastId = TaraModel.getLastId();
            lastId++;
            editCodigo.Text = "TARA" + lastId;
            editCodigo.ReadOnly = true;
            editNombre.Text = "Tara Número "+lastId;
            editColor.Text = "Sin Color";
            editPeso.Select();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            String tipo = editTipo.Text.Trim();
            if (tipo.Equals(""))
            {
                FormMessage formMessage = new FormMessage("Tipo de Tara Faltante","Tienes que agregar una letra diferente a las agregadas anteriormente en el Tipo de Tara",2);
                formMessage.ShowDialog();
                editTipo.Select();
            } else
            {
                String textoPeso = editPeso.Text.Trim();
                if (textoPeso.Equals(""))
                {
                    FormMessage formMessage = new FormMessage("Peso Faltante", "Tienes que agregar un peso para la tara", 2);
                    formMessage.ShowDialog();
                    editPeso.Select();
                } else
                {
                    String code = editCodigo.Text.Trim();
                    String name = editNombre.Text.Trim();
                    String color = editColor.Text.Trim();
                    double weight = Convert.ToDouble(textoPeso);
                    if (name.Equals(""))
                    {
                        FormMessage formMessage = new FormMessage("Nombre No Encontrado", "Tienes que agregar un nombre para la tara", 2);
                        formMessage.ShowDialog();
                        editNombre.Select();
                    } else
                    {
                        validateAddTaraLogic(code, name, color, weight, tipo);
                    }
                }
            }
        }

        private async Task validateAddTaraLogic(String code, String name, String color, double weight, String type)
        {
            if (TaraModel.typeExist(type))
            {
                FormMessage formMessage = new FormMessage("Tipo de Tara Repetido", "Tienes que agregar una letra diferente a las agregadas anteriormente en el Tipo de Tara", 2);
                formMessage.ShowDialog();
                editTipo.Select();
            } else
            {
                if (color.Equals(""))
                    color = "Sin Color";
                bool created = TaraModel.createANewRecord(code, name, color, weight, type);
                if (created)
                {
                    if (frmTaras != null)
                    {
                        frmTaras.resetearVariables(0);
                        await frmTaras.fillDataGridViewTaras();
                        this.Close();
                    } else
                    {
                        this.Close();
                    }
                } else
                {
                    FormMessage formMessage = new FormMessage("Exception", "Algo falló al agregar la nueva Tara", 2);
                    formMessage.ShowDialog();
                }
            }
        }

        private void editTipo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 255))
            {
                e.Handled = true;                
            }
        }

        private void editPeso_KeyPress(object sender, KeyPressEventArgs e)
        {
            char signo_decimal = (char)46;
            //Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == signo_decimal)
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan
                e.Handled = true;
            }
        }
    }
}
