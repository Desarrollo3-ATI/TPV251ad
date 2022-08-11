using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AdminDll
{
    public static class SECUDOC
    {
        public static void writeLog(string mensaje)
        {
            using (StreamWriter w = File.AppendText(General.RutaInicial + "\\SECUDOC.LOG"))
            {
                Log(mensaje, w);
            }
        }

        public static void writeFlag(string mensaje)
        {
            using (StreamWriter w = File.AppendText("C:\\utilizar.LOG"))
            {
                Log(mensaje, w);
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

        public static void Flag(string logMessage, TextWriter w)
        {
            w.Write("\rLog: ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("{0}", logMessage);
            w.WriteLine("-------------------------------");
        }
    }
}
