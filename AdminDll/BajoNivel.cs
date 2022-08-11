using System;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AdminDll
{
    public static class BajoNivel
    {
        public static string key { get; } = "syncatichiapas";
        public static Boolean registrar = false;
        public static string Encriptar(string texto)
        {
            string resultado;
            byte[] encriptado;
            resultado = Cripto.AES.Encriptar(texto, out encriptado);
            return resultado;
        }

        public static string[] Desencriptar(string[] lines)
        {
            try
            {
                lines[0] = Cripto.AES.Desencriptar(lines[0]);
                lines[1] = Cripto.AES.Desencriptar(lines[1]);
                lines[2] = Cripto.AES.Desencriptar(lines[2]);
                lines[3] = Cripto.AES.Desencriptar(lines[3]);
                lines[4] = Cripto.AES.Desencriptar(lines[4]);
                lines[5] = Cripto.AES.Desencriptar(lines[5]);
                lines[6] = Cripto.AES.Desencriptar(lines[6]);
                lines[7] = Cripto.AES.Desencriptar(lines[7]);
                lines[8] = Cripto.AES.Desencriptar(lines[8]);
                lines[9] = Cripto.AES.Desencriptar(lines[9]);
                lines[10] = Cripto.AES.Desencriptar(lines[10]);
                lines[11] = Cripto.AES.Desencriptar(lines[11]);
                lines[12] = Cripto.AES.Desencriptar(lines[12]);
                lines[13] = Cripto.AES.Desencriptar(lines[13]);
                lines[14] = Cripto.AES.Desencriptar(lines[14]);
                lines[15] = Cripto.AES.Desencriptar(lines[15]);
            }
            catch { }
            return lines;
        }

        public static string Encrip(string texto)
        {
            try
            {

                byte[] keyArray;
                byte[] Arreglo_a_Cifrar = Encoding.UTF8.GetBytes(texto);

                //Se utilizan las clases de encriptación MD5
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();

                //Algoritmo TripleDES
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);
                tdes.Clear();

                //se regresa el resultado en forma de una cadena
                texto = Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);
            }
            catch{ }
            return texto;
        }

        public static string Desencrip(string textoEncriptado)
        {
            try
            {
                byte[] keyArray;
                byte[] Array_a_Descifrar = Convert.FromBase64String(textoEncriptado);

                //algoritmo MD5
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();

                byte[] resultArray = cTransform.TransformFinalBlock(Array_a_Descifrar, 0, Array_a_Descifrar.Length);

                tdes.Clear();
                textoEncriptado = Encoding.UTF8.GetString(resultArray);

            }
            catch{ }
            return textoEncriptado;
        }

        public static void SaveKey(string key)
        {
            if (key != "")
            {
                key = Encriptar(key);
            }
            string fileName = General.RutaInicial + @"\Licencia.lic";
            using (StreamWriter outputFile = new StreamWriter(fileName))
            {
                outputFile.Write(key);
            }
        }

        public static void SaveKey(string key, string fechaFin, string equipo)
        {
            if (key != "" && fechaFin != "" && equipo != "")
            {
                key = Encriptar(key);
                fechaFin = Encriptar(fechaFin);
                equipo = Encriptar(equipo);
            }
            string fileName = General.RutaInicial + @"\local.dll";
            using (StreamWriter salida = new StreamWriter(fileName))
            {
                salida.WriteLine(key);
                salida.WriteLine(fechaFin);
                salida.WriteLine(equipo);
                salida.Close();
            }
        }

        public static string getCodigoSitioV2(string sitio)
        {
            string sCSP;
            sCSP = GetCSP();         
            string[] codigo;
            string CS;
            codigo = sCSP.Split('-');
            CS = codigo[3] + "-" + sitio + "-" + codigo[2] + "-" + codigo[1];
            if (validar())
            {
                SECUDOC.writeLog("Retorna V2");
            }
            return CS;
        }

        public static string getCodigoSitioV3(string sitio)
        {
            string sCSP,sitioDD;
            sCSP = GetCSP();
            string[] codigo;
            string CS;
            codigo = sCSP.Split('-');
            sitioDD = sitio.Substring(0, 4);
            CS = codigo[3] + "-" + sitioDD + "-" + codigo[2] + "-" + codigo[1];
            if (validar())
            {
                SECUDOC.writeLog("Retorna V3");
            }
            return CS;
        }

        public static string getCodigoSitioV4()
        {
            string sCSP, codigoSitio;
            sCSP = GetCSP();
            string[] codigo;
            string CS;
            codigo = sCSP.Split('-');
            codigoSitio = codigo[4].Substring(0, 4);
            CS = codigo[3] + "-" + codigoSitio + "-" + codigo[2] + "-" + codigo[1];
            if (validar())
            {
                SECUDOC.writeLog("Retorna V4");
            }
            return CS;
        }

        public static string GetCSP()
        {
            string sCSP = "";
            string sQuery = "SELECT * FROM Win32_ComputerSystemProduct";
            ManagementObjectSearcher oManagementObjectSearcher = new ManagementObjectSearcher(sQuery);
            ManagementObjectCollection oCollection = oManagementObjectSearcher.Get();
            foreach (ManagementObject oManagementObject in oCollection)
            {
                sCSP = oManagementObject["UUID"].ToString();
            }
            return sCSP;
        }

        public static string generaBloque(string bInicial, string bFinal)
        {
            string resultado = "";
            string[] letras = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "X", "Y", "Z", "W" };
            if (validar())
            {
                SECUDOC.writeLog("Ingresa correctamente y obtiene las letras");
            }
            for (int index =0; index <= bInicial.Length-1; index++)
            {
                string n;
                n = bInicial.Substring(index,1);
                if(validar())
                    SECUDOC.writeLog("Todo bien");
                int num = 0;
                if(Int32.TryParse(n.ToString(), out num))
                {
                    if (validar())
                    {
                        SECUDOC.writeLog("Valor de  " + num);
                    }
                    num = num + Convert.ToInt32(bFinal.Substring(index,1).ToString());
                    if (num < 10)
                    {
                        if (validar())
                        {
                            SECUDOC.writeLog("Si es mayor num que 10  " + num);
                        }
                        resultado += num.ToString();
                    }                     
                    else
                    {
                        if (validar())
                        {
                            SECUDOC.writeLog("No es mayor num que 10  " + num);
                            SECUDOC.writeLog(" LETRAS  " + letras.Length);
                        }
                        resultado += letras[num - 10];
                    }                       
                }
                else
                {
                    int pos = 0;
                    if (validar())
                    {
                        SECUDOC.writeLog("Ingreso en el else  ");
                    }
                    for (int i=0; i<= letras.Length-1; i++)
                    {
                        if (letras[i] == n.ToString())
                            pos = i;
                    }
                    int temp;
                    temp = Convert.ToInt32(bFinal.Substring(index,1).ToString());
                    pos = pos + temp;
                    if (validar())
                    {
                        SECUDOC.writeLog("Valor de pos  " + pos);
                    }
                    if (pos >= letras.Length)
                    {
                        pos = pos - letras.Length;
                        if (validar())
                        {
                            SECUDOC.writeLog("El valor de resultado " + pos + "" + letras.Length);
                        }
                    }
                    resultado += letras[pos];
                    if (validar())
                    {
                        SECUDOC.writeLog("El valor de resultado " + resultado);
                    }
                }
            }
            return resultado;
        }
        public static bool validar()
        {
            string file = General.RutaInicial + "\\utilizar.LOG";
            if (File.Exists(file))
                registrar = true;
            else
                registrar = false;

            return registrar;
    
        }
        public static string getCodigoSitio()
        {
            if (validar())
            {
                SECUDOC.writeFlag("Inicio");
            }
            string CS = "";
            string sProcessorID = "";
            string[] dd = new string[2];
            string enc1 = "6541";
            string enc2 = "8537";
            string[] b4 = { "", ""};
            string msj;
            validar();
            Boolean disco = false;
            Boolean procesador = false; 
            
            try
            {
                dd = DiskDrive();
                if (validar()) SECUDOC.writeLog("DD0: " + dd[0]);
                if (validar()) SECUDOC.writeLog("DD1: " + dd[1]);
                dd[0] = dd[0].Trim().ToUpper().Replace(" ", "");
                dd[1] = dd[1].Trim().ToUpper().Replace(" ", "");
                dd[0] = EliminarEspeciales(dd[0]);
                dd[1] = EliminarEspeciales(dd[1]);
                if (dd[0] == "" || dd[1] == "") 
                    disco = false;
                else
                    disco = true;
            }
            catch (Exception ex)
            {
                disco = false;
                msj = ex.ToString();
                if (validar())
                {
                    SECUDOC.writeLog("Error en el disco");
                    SECUDOC.writeLog(ex.ToString());
                }
            }
            try
            {
                sProcessorID = GetProcessorID();             
                b4 = sProcessorID.Split('-');
                procesador = true;                
            }
            catch (Exception ex)
            {
                procesador = false;
                if (validar())
                {
                    SECUDOC.writeLog("Error en el procesador");
                    SECUDOC.writeLog(ex.ToString());
                }
            }
            if (procesador == true && disco == true)
            {
                if (validar())
                {
                    SECUDOC.writeLog("------");
                    SECUDOC.writeLog("P0: " + dd[0]);
                    SECUDOC.writeLog("P1: " + dd[1]);
                    //SECUDOC.writeFlag("D: " + disco);
                }
                string b1, b2, b3;
                b1 = dd[0].Substring(0, 1) + dd[0].Substring(dd[0].Length - 1, 1);
                b1 += dd[1].Substring(0, 1) + dd[1].Substring(dd[1].Length - 1, 1);
                b2 = generaBloque(b1, enc1);
                b3 = generaBloque(b4[3], enc2);
                CS = b1 + "-" + b2 + "-" + b3 + "-" + b4[3];
                if(validar())
                {
                    SECUDOC.writeLog("Si obtuvo información de procesador y el disco");
                    //SECUDOC.writeFlag("");
                }
            }
            else if( procesador == true && disco == false)
            {
                if (validar())
                {
                    SECUDOC.writeLog("Si obtuvo información de procesador pero el disco falló");
                }
                CS = getCodigoSitioV2(b4[3]);
            }
            else if (procesador == false && disco == true)
            {
                if (validar())
                {
                    SECUDOC.writeLog("Falló al obtener información del procesador, pero el disco si obtuvo");
                }
                CS = getCodigoSitioV3(dd[1]);
            }
            else if (procesador == false && disco == false)
            {
                if (validar())
                {
                    SECUDOC.writeLog("Ninguno de los métodos de obtención de información funcionaron");
                }
                CS = getCodigoSitioV4();
            }
            return CS;
        }

        public static string EliminarEspeciales(string s)
        {
            return Regex.Replace(s, "[{}!'#$%&/()=?¡¿|*+¨´:.;,<>-]", "",RegexOptions.None);
        }
        
        public static string[] DiskDrive()
        {
            string[] sCSP = new string[3];
            string sQuery = "SELECT * FROM Win32_DiskDrive";
            ManagementObjectSearcher oManagementObjectSearcher = new ManagementObjectSearcher(sQuery);
            ManagementObjectCollection oCollection = oManagementObjectSearcher.Get();
            foreach (ManagementObject oManagementObject in oCollection)
            {
                if(oManagementObject["Name"].ToString().Contains("PHYSICALDRIVE0"))
                {
                    sCSP[0] = oManagementObject["SerialNumber"].ToString();
                    sCSP[1] = oManagementObject["Model"].ToString();
                    if (validar())
                    {
                        SECUDOC.writeLog("Obtención del disco exitoso");
                    }
                    return sCSP;
                }               
            }
            return sCSP;
        }

        public static string GetProcessorID()
        {
            string cpuInfo = String.Empty;
            ManagementClass managementClass = new ManagementClass("Win32_Processor");
            ManagementObjectCollection managementObjCol = managementClass.GetInstances();

            foreach (ManagementObject managementObj in managementObjCol)
            {
                if (cpuInfo == String.Empty)
                {
                    cpuInfo = managementObj.Properties["ProcessorId"].Value.ToString();
                    if (validar())
                    {
                        SECUDOC.writeLog("Obtención del procesador exitoso");
                    }
                    break;
                }
            }
            cpuInfo = cpuInfo.Insert(4, "-");
            cpuInfo = cpuInfo.Insert(9, "-");
            cpuInfo = cpuInfo.Insert(14, "-");
            return cpuInfo;
        }
    }
}
