using AdminDll;
using Cripto;
using Microsoft.Win32;
using SyncTPV.Models;
using System;
using System.Data.SQLite;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace SyncTPV
{
    public class MetodosGenerales
    {
        public static string rootDirectory = Application.StartupPath;
        public static string versionNumber = "2.5.11";
        public static string supportedResourceVersion = "5.6.0";

        public static Bitmap redimencionarImagenes(Image originalImage, int width, int height)
        {
            //var radio = Math.Max((double)width / originalImage.Width, (double)height / originalImage.Height);
            //var newWidth = (int)(originalImage.Width * radio);
            //var newHeight = (int)(originalImage.Height * radio);
            var newImage = new Bitmap(width, height);
            Graphics.FromImage(newImage).DrawImage(originalImage, 0, 0, width, height);
            Bitmap finalImage = new Bitmap(newImage);
            return finalImage;
        }

        public static Bitmap redimencionarBitmap(Bitmap originalImage, int width, int height) {
            //var radio = Math.Max((double)width / originalImage.Width, (double)height / originalImage.Height);
            //var newWidth = (int)(width * radio);
            //var newHeight = (int)(height * radio);
            var newImage = new Bitmap(width, height);
            Graphics.FromImage(newImage).DrawImage(originalImage, 0, 0, width, height);
            Bitmap finalImage = new Bitmap(newImage);
            return finalImage;
        }

        public static void showToastSuccess(String tittle, String message)
        {
            PopupNotifier popup = new PopupNotifier();
            popup.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.success_green, 100, 100);
            popup.TitleColor = Color.FromArgb(43, 143, 192);
            popup.TitleText = tittle;
            popup.TitlePadding = new Padding(5, 5, 5, 5);
            popup.ButtonBorderColor = Color.Red;
            popup.ContentText = message;
            popup.ContentColor = Color.FromArgb(43, 143, 192);
            popup.HeaderHeight = 10;
            popup.AnimationDuration = 1000;
            popup.HeaderColor = Color.FromArgb(200, 244, 255);
            popup.Popup();
        }

        public static bool verifyIfInternetIsAvailable()
        {
            bool state = false;
            bool RedActiva = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            if (RedActiva)
                state = true;
            return state;
        }

        public static double obtieneDosDecimales(double precio)
        {
            double precioNew = 0;
            string[] precios = precio.ToString().Split('.');
            if (precios.Length > 1)
            {
                if (precios[1].Length > 2)
                {
                    precios[1] = precios[1].Substring(0, 2);
                    precio = Convert.ToDouble(precios[0] + "." + precios[1]);
                }
                else if (precios[1].Length < 2)
                {
                    precios[1] = precios[1] + 0;
                    precio = Convert.ToDouble(precios[0] + "." + precios[1]);
                }
            }
            else
            {
                precio = Convert.ToDouble(precios[0] + ".00");
            }
            return precioNew = Convert.ToDouble(precio);
        }

        public static String quitarAsentos(String textoConAsentos)
        {
            string textoNormalizado = textoConAsentos.Normalize(NormalizationForm.FormD);
            //coincide todo lo que no sean letras y números ascii o espacio
            //y lo reemplazamos por una cadena vacía.
            Regex reg = new Regex("[^a-zA-Z0-9 ]");
            string textoSinAcentos = reg.Replace(textoNormalizado, "");
            return textoSinAcentos;
        }

        public static String getCurrentDateAndHourForFolioVenta()
        {
            String currentDate = "";
            DateTime fh = DateTime.Now;
            currentDate = fh.ToString("yyMMddHHmmss");
            return currentDate;
        }

        public static String getCurrentDateAndHour()
        {
            String currentDate = "";
            DateTime fh = DateTime.Now;
            currentDate = fh.ToString("yyyy-MM-dd HH:mm:ss");
            return currentDate;
        }

        public static String getErrorMessageFromNetworkCode(int errorCode, String errorException)
        {
            String msg = "";
            if (errorCode == -400)
            {
                msg = "Algo falló al negociar con el servidor (Error de conexión), validar que el servidor esté activo!";
                if (errorException != null && !errorException.Equals(""))
                    msg += "\r\n" + errorException;
            } else if (errorCode == -404)
            {
                msg = "No pudimos establecer conexión con el servidor (Tiempo Excedido)";
                if (errorException != null && !errorException.Equals(""))
                    msg += "\r\n" + errorException;
            } else
            {
                msg = "Algo falló en el servidor mientras se establecia la conexión, Internal Server Error: " + Math.Abs(errorCode);
                if (errorException != null && !errorException.Equals(""))
                    msg += "\r\n" + errorException;
            }
            return msg;
        }

    }

    public static class clsGeneral
    {
        public static string rutaRegistro { get; } = "Software\\Syncs\\SyncTPV\\SCS";
        public static string nombreSync { get; } = "SyncTPV";

        public static string MessageError = "";

        //public static clsClienteSync datoscte { set; get; } = new clsClienteSync();

        public static int idDesarrolloEspecial { get; set; } = 0;

        public static async Task<bool> isTheLicenseValid(String fechaFin, String fechaHoy)
        {
            bool valid = false;
            await Task.Run(async () =>
            {
                try
                {
                    SECUDOC.writeLog(fechaFin+" - "+fechaHoy);
                    if (!fechaHoy.Equals(""))
                    {
                        DateTime fechaActual = Convert.ToDateTime(fechaHoy);
                        DateTime fechaVencimiento = Convert.ToDateTime(fechaFin);
                        if (fechaVencimiento >= fechaActual)
                            valid = true;
                        else valid = false;
                    }
                    else
                    {
                        String today = DateTime.Now.ToString("dd/MM/yyyy");
                        DateTime todayDate = DateTime.ParseExact(today, "dd/MM/yyyy", null);
                        if (changeLicenseEndDateFormat(fechaFin) >= todayDate)
                            valid = true;
                        else valid = false;
                    }
                }
                catch (Exception ex)
                {
                    SECUDOC.writeLog(ex.ToString());
                    if (ex.Message.Equals("String was not recognized as a valid DateTime."))
                    {
                        String today = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss tt");
                        if (Convert.ToDateTime(fechaFin) >= Convert.ToDateTime(today))
                        {
                            valid = true;
                        }
                    }
                    else
                    {
                        string[] FechaNew = fechaFin.Split('.');
                        string FechaNuevo = FechaNew[0] + ".m.";
                        if (Convert.ToDateTime(FechaNuevo) >= DateTime.Now)
                        {
                            valid = true;
                        }
                        else
                            valid = false;
                    }
                }
            });
            return valid;
        }

        public static DateTime changeLicenseEndDateFormat(String endDate)
        {
            DateTime newEndDate = new DateTime();
            try
            {
                String outputDate = Convert.ToDateTime(endDate).ToString("dd/MM/yyyy");
                newEndDate = Convert.ToDateTime(outputDate);
            }
            catch (Exception ex)
            {
                SECUDOC.writeLog(ex.ToString());
            }
            return newEndDate;
        }

        public static bool fillRegeditAndFiles()
        {
            try
            {
                string[] lic = File.ReadAllLines(MetodosGenerales.rootDirectory + "\\Licencia.lic");
                lic = BajoNivel.Desencriptar(lic);
                String synckey = LicenseModel.getSynckeyInLocalDb();
                clsRegistro Reg = new clsRegistro();
                String endDateText = LicenseModel.getEndDateEncryptFromTheLocalDb();
                if (endDateText.Equals(""))
                {
                    endDateText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    endDateText = AdminDll.BajoNivel.Encrip(endDateText);
                    Reg.FV = endDateText;
                }
                else Reg.FV = endDateText;
                Reg.SK = synckey;
                Reg.TL = "2";
                Reg.TS = "SyncTPV";
                try
                {
                    Reg.IDE = idDesarrolloEspecial.ToString();
                    Reg.EID = BajoNivel.getCodigoSitio();
                    if (Reg.IDE == "") Reg.IDE = "0";
                }
                catch { Reg.IDE = "0"; }
                DateTime fechaFin = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                if (!endDateText.Equals(""))
                {
                    //endDateText = AdminDll.BajoNivel.Desencrip(endDateText);
                    endDateText = AES.Desencriptar(endDateText);

                    fechaFin = Convert.ToDateTime(endDateText);
                }
                    
                TimeSpan cont = fechaFin.Date - DateTime.Now.Date;
                int diasRestantes = cont.Days;
                Reg.DR = diasRestantes.ToString();
                Reg.FA = "1";
                Reg.FV = endDateText;
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
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                SECUDOC.writeLog(ex.ToString());
                return false;
            }
        }


        public static bool authenticationLicense()
        {
            //---------- Verificamos que aún tenga días
            int DiasRestantes = getRemainingDays();
            if (DiasRestantes == 0)
                return false;
            else return true;
        }

        public static int TipoLicenciaSyncReg()
        {
            clsRegistro registro = getRegeditValues();
            if (registro.TS.ToString() == nombreSync)
            {
                if (registro.FA.ToString() != "")
                {
                    if (registro.EID.ToString() == "0")
                        return -1;
                    if (registro.EID.ToString() != BajoNivel.getCodigoSitio())
                        return -1;
                    idDesarrolloEspecial = Convert.ToInt32(registro.IDE.ToString());
                    return 0;
                }
                else
                    return -1;
            }
            else
                return -1;
        }

        public static bool validarRegistroSync()
        {
            string aes = "", calculado = "";
            DateTime fechaFin = new DateTime();
            clsRegistro regeditValues = getRegeditValues();
            //Obtener datos de registro
            if (regeditValues != null)
            {
                if (regeditValues.TS == nombreSync)
                {
                    if (regeditValues.FA != "")
                    {
                        if (regeditValues.EID != "0")
                        {
                            aes = regeditValues.EID;
                            fechaFin = Convert.ToDateTime(regeditValues.FV);
                            calculado = BajoNivel.getCodigoSitio();
                        }
                        else
                        {
                            MessageError = "La licencia ha sido revocada";
                            return false;
                        }
                    }
                    else
                    {
                        MessageError = "La licencia no ha sido activada";
                        return false;
                    }
                }
                else
                {
                    MessageError = "Software incorrecto. Licencia para: " + regeditValues.TS;
                    return false;
                }
            }
            //Validacion
            if (string.Compare(aes, calculado) == 0)
            {
                DateTime currentDate = DateTime.Now;
                if (currentDate > fechaFin)
                {
                    MessageError = "La licencia ha expirado";
                    return false;
                }
                else
                {
                    TimeSpan cont = fechaFin.Date - currentDate.Date;
                }
            }
            else
            {
                MessageError = "La licencia usada no se activo para este equipo";
                return false;
            }
            return true;
        }

        public static int getRemainingDays()
        {
            int days = 0;
            String endDateText = LicenseModel.getEndDateEncryptFromTheLocalDb();
            if (endDateText.Equals(""))
            {
                RegistryKey subKey = Registry.LocalMachine.OpenSubKey(rutaRegistro, true);
                if (subKey != null)
                    subKey.SetValue("DR", BajoNivel.Encriptar(0+""));
            } else
            {
                DateTime endDate = Convert.ToDateTime(AES.Desencriptar(endDateText));
                if (endDate.Date > DateTime.Now.Date)
                {
                    TimeSpan Days = DateTime.Now.Date - endDate.Date;
                    days -= Days.Days;
                    if (days < 0) days = 0;
                    RegistryKey subKey = Registry.LocalMachine.OpenSubKey(rutaRegistro, true);
                    if (subKey != null)
                        subKey.SetValue("DR", BajoNivel.Encriptar(days.ToString()));
                }
                else
                {
                    RegistryKey subKey = Registry.LocalMachine.OpenSubKey(rutaRegistro, true);
                    if (subKey != null)
                        subKey.SetValue("DR", BajoNivel.Encriptar(days.ToString()));
                }
            }
            return days;
        }

        public static clsRegistro getRegeditValues()
        {
            clsRegistro regeditValues = new clsRegistro();
            RegistryKey subKey = default(RegistryKey);
            string valor = string.Empty, MessageError = "";
            subKey = Registry.LocalMachine.OpenSubKey(rutaRegistro);
            if (subKey == null)
            {
                MessageError = "Problemas con la autentificación de licencia temporal.";
            }
            else
            {
                regeditValues.DR = subKey.GetValue("DR").ToString();
                regeditValues.EID = subKey.GetValue("EID").ToString();
                regeditValues.FA = subKey.GetValue("FA").ToString();
                regeditValues.FV = subKey.GetValue("FV").ToString();
                regeditValues.SK = subKey.GetValue("SK").ToString();
                regeditValues.TL = subKey.GetValue("TL").ToString();
                regeditValues.TS = subKey.GetValue("TS").ToString();
                regeditValues.IDE = subKey.GetValue("IDE").ToString();
                regeditValues.Desencriptar();
            }
            return regeditValues;
        }

        public static clsRegistro getRegisterFromLicenseLic()
        {
            clsRegistro dataLic = null;
            try
            {
                string[] lic = File.ReadAllLines(General.RutaInicial + "\\Licencia.lic");
                if (lic != null && lic.Length > 0)
                {
                    dataLic = new clsRegistro();
                    dataLic.SK = AES.Desencriptar(lic[0].ToString());
                    dataLic.TS = BajoNivel.Desencrip(lic[1].ToString());
                    dataLic.TL = BajoNivel.Desencrip(lic[2].ToString());
                    dataLic.EID = AES.Desencriptar(lic[3].ToString());
                    dataLic.FV = BajoNivel.Desencrip(lic[4].ToString());
                }
            } catch (Exception e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            return dataLic;
        }

        public static bool LicenciaVacia()
        {
            string[] lic = File.ReadAllLines(MetodosGenerales.rootDirectory + "\\Licencia.lic");
            lic = AdminDll.BajoNivel.Desencriptar(lic);
            if (lic.Length == 0)
                return true;
            else return false;
        }
        private static string ConvertirEnteroTexto(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;

            try
            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
            if (decimales > 0)
            {
                dec = " CON " + decimales.ToString() + "/100";
            }

            res = toText(Convert.ToDouble(entero)) + dec;
            return res;
        }
        private static string toText(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);
            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + toText(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";
            else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);
            }

            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);
            }

            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            else
            {
                Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }
            return Num2Text;
        }

        public static async Task<ExpandoObject> SubirImagenFTP(string carpeta, string file, string nombreArchivo, string extension, string carpetaLocal)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(() => {
                int error = 0;
                String errorMessage = "";
                String rutaCarpeta = MetodosGenerales.rootDirectory + "\\Imagenes\\"+ carpetaLocal;
                if (!Directory.Exists(rutaCarpeta))
                {
                    Directory.CreateDirectory(rutaCarpeta);
                }
                DatosTicketModel dtm = DatosTicketModel.getDataForFTPServer();
                if (dtm.EMPRESA != null && !dtm.EMPRESA.Equals("") && dtm.FTPSERVER != null && !dtm.FTPSERVER.Equals("") && 
                dtm.FTPUSER != null && !dtm.FTPUSER.Equals("") && dtm.FTPPASSWORD != null && !dtm.FTPPASSWORD.Equals("")) {
                    string nomenclatura = "";
                    if (carpeta == "customers")
                        nomenclatura = "-c";
                    else if (carpeta == "articulos")
                        nomenclatura = "-1";
                    String urlFtp = "ftp://" + dtm.FTPSERVER + "/" + dtm.EMPRESA + "/" + carpeta + "/" + nombreArchivo + nomenclatura + ".jpg";
                    clsFTP FTP = new clsFTP();
                    dynamic respuesta = FTP.uploadFile(urlFtp, dtm.FTPUSER, dtm.FTPPASSWORD, file);
                    if (respuesta.valor != -1) {
                        try {
                            string ruta = MetodosGenerales.rootDirectory + "\\Imagenes" + "\\" + carpetaLocal+"\\"+nombreArchivo + nomenclatura + ".jpg";
                            if (!File.Exists(ruta)) {
                                File.Copy(file, ruta);
                            } else {
                                File.Delete(ruta);
                                File.Copy(file, ruta);
                            }
                            error = respuesta.valor;
                            errorMessage = respuesta.description;
                        } catch (Exception ex) {
                            SECUDOC.writeLog(ex.ToString());
                            errorMessage = "Exception: " + ex.Message;
                            error = -2;
                        }
                    } else {
                        error = respuesta.valor;
                        errorMessage = respuesta.description;
                    }
                } else {
                    errorMessage = "Datos del servidor FTP no encontrador validar descarga DatosTicket o en SyncROM Panel";
                    error = 2;
                }
                response.valor = error;
                response.description = errorMessage;
            });
            return response;
        }

    }
}
