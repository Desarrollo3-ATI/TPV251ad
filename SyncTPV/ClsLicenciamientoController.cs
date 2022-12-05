using AdminDll;
using Cripto;
using Microsoft.Win32;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Data;
using System.Data.SQLite;
using System.Dynamic;
using System.IO;
using System.Threading.Tasks;

namespace SyncTPV
{
    public static class ClsLicenciamientoController
    {
        public static string rutaRegistro { get; } = "Software\\Syncs\\SyncTPV\\SCS";
        public static string nombreSync { get; } = "SYNCTPV";


        public static async Task<ExpandoObject> llenarDatosEnRegedit(DateTime fechaActual, String codigoSitio, String fechaActivacion, String fechaFin,
            String synckey, int tipoLic)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                try
                {
                    string[] lic = File.ReadAllLines(MetodosGenerales.rootDirectory + "\\Licencia.lic");
                    lic = BajoNivel.Desencriptar(lic);
                    clsRegistro Reg = new clsRegistro();
                    Reg.EID = codigoSitio;//codigo sitio
                    Reg.FA = fechaActivacion;
                    Reg.FV = fechaFin;
                    Reg.SK = synckey;
                    Reg.TL = tipoLic+"";
                    Reg.TS = nombreSync;
                    try
                    {
                        Reg.IDE = codigoSitio;
                        if (Reg.IDE == "") Reg.IDE = "0";
                    }
                    catch { Reg.IDE = "0"; }
                    DateTime feechaFinLic = Convert.ToDateTime(fechaFin);
                    TimeSpan cont = feechaFinLic.Date - fechaActual.Date;
                    int diasRestantes = cont.Days;
                    Reg.DR = diasRestantes.ToString();
                    string valor = string.Empty;
                    RegistryKey subKey = Registry.LocalMachine.OpenSubKey(rutaRegistro, true);
                    if (subKey != null)
                    {
                        Reg.Encriptar();
                        subKey.SetValue("DR", Reg.DR);
                        subKey.SetValue("EID", Reg.EID);
                        subKey.SetValue("FA", Reg.FA);
                        subKey.SetValue("FV", Reg.FV);
                        subKey.SetValue("SK", Reg.SK);
                        subKey.SetValue("TL", Reg.TL);
                        subKey.SetValue("TS", Reg.TS);
                        subKey.SetValue("IDE", Reg.IDE);
                        value = 1;
                    }
                    else
                    {
                        RegistryKey regKey = Registry.LocalMachine.CreateSubKey(rutaRegistro);
                        subKey = regKey.OpenSubKey(rutaRegistro, true);
                        if (subKey != null)
                        {
                            Reg.Encriptar();
                            subKey.SetValue("DR", Reg.DR);
                            subKey.SetValue("EID", Reg.EID);
                            subKey.SetValue("FA", Reg.FA);
                            subKey.SetValue("FV", Reg.FV);
                            subKey.SetValue("SK", Reg.SK);
                            subKey.SetValue("TL", Reg.TL);
                            subKey.SetValue("TS", Reg.TS);
                            subKey.SetValue("IDE", Reg.IDE);
                            value = 1;
                        }
                        else description = "No pudimos aperturar la conexión con llaves del registro para la aplicación!";
                    }
                }
                catch (Exception ex)
                {
                    SECUDOC.writeLog(ex.ToString());
                    value = -1;
                    description = ex.Message;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                }
            });
            return response;
        }        

        public static async Task addDataInLicenseLic(String syncName, String tipoLicencia, String endDate, int Dias)
        {
            await Task.Run(async () =>
            {
                try
                {
                    string[] lic = File.ReadAllLines(General.RutaInicial + "\\Licencia.lic");
                    if (lic.Length > 0)
                    {
                        string FileName = General.RutaInicial + "\\Licencia.lic";
                        StreamWriter salida = new StreamWriter(FileName);
                        salida.WriteLine((lic[0]));             //--------- Synckey
                        salida.WriteLine(BajoNivel.Encrip(syncName));      //--------- Sync (Encriptado 2)
                        salida.WriteLine(BajoNivel.Encrip(tipoLicencia));  //--------- Licencia (Encriptado 2)
                        salida.WriteLine(BajoNivel.Encriptar(BajoNivel.getCodigoSitio()));
                        salida.WriteLine(BajoNivel.Encrip(endDate)); //---------(Encriptado 2)
                        salida.Close();
                    }
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                }
            });
        }

    }
}
