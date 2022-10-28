using System;
using System.Diagnostics;
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
                string NombreProceso = Process.GetCurrentProcess().ProcessName;
                Process[] OProcesos = Process.GetProcessesByName(NombreProceso);
                if (OProcesos.Length == 1)
                {
                    FormIniciarSesion frmIniciarSesion = new FormIniciarSesion();
                    frmIniciarSesion.ShowDialog();
                    Application.Run(frmIniciarSesion);
                }
                else
                {
                    MessageBox.Show("¡SyncTPV ya esta en ejecucion!");
                }
            }
            catch (ObjectDisposedException e)
            {
                SECUDOC.writeLog("Exception in ProgramClass: " + e.ToString());
            }
        }
    }
}
