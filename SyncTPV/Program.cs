using System;
using System.Windows.Forms;

namespace SyncTPV
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                FormIniciarSesion frmIniciarSesion = new FormIniciarSesion();
                frmIniciarSesion.ShowDialog();
                Application.Run(frmIniciarSesion);
            }
            catch (ObjectDisposedException e)
            {
                SECUDOC.writeLog("Exception in ProgramClass: " + e.ToString());
            }
        }
    }
}
