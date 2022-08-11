using System;
using System.Windows.Forms;

namespace SyncTPV
{
    public static class Validaciones
    {
        //e.Handled = false -> permite escribir el caracter
        //e.Handled = true -> no permite escribir el caracter
        public static void SoloNumerosDecimales(KeyPressEventArgs e, string text)
        {
            try
            {
                if (Char.IsNumber(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (Char.IsSeparator(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    if (e.KeyChar == '.')
                    {
                        if (text.Contains(".")) e.Handled = true;
                        else e.Handled = false;
                    }
                    else e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                SECUDOC.writeLog(ex.ToString());
            }
        }

        public static void SoloNumeros(KeyPressEventArgs e)
        {
            try
            {
                if (Char.IsNumber(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (Char.IsSeparator(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                SECUDOC.writeLog(ex.ToString());
            }
        }
    }
}
