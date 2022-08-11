﻿using System;
using System.IO;
using System.Threading.Tasks;

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

    }
}
