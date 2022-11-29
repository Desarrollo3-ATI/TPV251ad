using SyncTPV.Controllers;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace SyncTPV
{
    public static class SECUDOC
    {
        public static void writeLog(string mensaje)
        {
            try
            {
                using (StreamWriter w = File.AppendText(MetodosGenerales.rootDirectory + "\\SECUDOC.LOG"))
                {
                    Log(mensaje, w);
                }
            } catch (Exception e) { }
        }

        public static void corteLog(string mensaje)
        {
            try
            {
                if (!File.Exists(MetodosGenerales.rootDirectory + "\\CORTE.LOG"))
                {
                    File.Create(MetodosGenerales.rootDirectory + "\\CORTE.LOG");
                }
                using (StreamWriter w = File.AppendText(MetodosGenerales.rootDirectory + "\\CORTE.LOG"))
                {
                    Log(mensaje, w);
                }
            }
            catch (Exception e)
            {
                writeLog(e.ToString());
            }
        }

        public static void TicketTPV(string mensaje, string clave)
        {
            //estaría bien que por cada apertura de caja me cree una carpeta donde almacenará una copia de todos los tickets que se impriman en ese turno.
            string rutaRespaldoTicket = MetodosGenerales.rootDirectory + "\\TicketsTPV";
            string nombreRespaldoTicket = "\\TPV-" + clave;
            string tempRutaRespaldoTicket = rutaRespaldoTicket + nombreRespaldoTicket;
            int contador = 0;
            if (!Directory.Exists(rutaRespaldoTicket))
            {
                Directory.CreateDirectory(rutaRespaldoTicket);
            }
            //validar que el archivo no exista, si existe, renombra archivo(n).txt
            string auxNombre = nombreRespaldoTicket;
            while (File.Exists(rutaRespaldoTicket + nombreRespaldoTicket + ".txt"))
            {
                contador++;
                string aux = "(" + contador + ")";
                nombreRespaldoTicket = auxNombre + aux;
            }
            nombreRespaldoTicket = rutaRespaldoTicket + nombreRespaldoTicket;
            try
            {
                mensaje = mensaje.Replace("\u001bE\u0001", "").Replace("\u001bE\0", "").Replace("\u001bm", "").Replace("\u001bd\u0001", "").Replace("\u001bp0ᐔ", "");
                //crea archivo de Ticket PDF
                ClsPdfMethods.CreatePdfTicketTPV(nombreRespaldoTicket + ".pdf", mensaje);
                //crea archivo de Ticket
                using (StreamWriter w = File.AppendText(nombreRespaldoTicket + ".txt"))
                {
                    LogTicketTPV(mensaje, w);
                }
                //else
                //{
                //    SECUDOC.writeLog("No se encontraron respaldos " + DateTime.Now.ToString());
                //}
            }
            catch (Exception e)
            {
                writeLog("Error al guardar respaldo de Ticket: " + e.ToString());
            }
        }

        public static void writeWeight(String weight)
        {
            try
            {
                if (!File.Exists(MetodosGenerales.rootDirectory + "\\Weights.LOG"))
                {
                    File.Create(MetodosGenerales.rootDirectory + "\\Weights.LOG");
                }
                using (StreamWriter w = File.AppendText(MetodosGenerales.rootDirectory + "\\Weights.LOG"))
                {
                    Log(weight, w);
                }
            } catch (Exception e)
            {
                writeLog(e.ToString());
            }
            finally
            {
                writeLog(weight);
            }
        }

        public static void Log(string logMessage, TextWriter w)
        {
            w.Write("\rLog: ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("{0}", logMessage);
            w.WriteLine("-------------------------------");
        }

        public static void LogTicketTPV(string logMessage, TextWriter w)
        {
            //w.Write("Creado: ");
            //w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
            w.WriteLine("{0}", logMessage);
            w.WriteLine("----------------------------------------");
        }
    }
}
