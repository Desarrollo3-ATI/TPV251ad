using AdminDll;
using Cripto;
using Microsoft.Win32;
using System;

namespace SyncTPV.Controllers
{
    public class ClsRegeditController
    {

        public static void saveIdDefaultCustomer(int idCustomer)
        {
            try
            {
                String rutaRegistro = "Software\\Syncs\\SyncTPV\\Data";
                String[] namesExists = Registry.LocalMachine.GetSubKeyNames();
                Registry.LocalMachine.CreateSubKey(rutaRegistro);
                var subKey = Registry.LocalMachine.OpenSubKey(rutaRegistro, true);
                if (subKey != null)
                {
                    String idDefaultCustomer = BajoNivel.Encriptar(idCustomer.ToString());
                    subKey.SetValue("IDDCPG", idDefaultCustomer);
                }
            } catch (UnauthorizedAccessException e)
            {
                SECUDOC.writeLog(e.ToString());
            }            
        }

        public static void saveCurrentIdEnterprise(int idEnterprise)
        {
            try
            {
                String rutaRegistro = "Software\\Syncs\\SyncTPV\\Data";
                String[] namesExists = Registry.LocalMachine.GetSubKeyNames();
                Registry.LocalMachine.CreateSubKey(rutaRegistro);
                var subKey = Registry.LocalMachine.OpenSubKey(rutaRegistro, true);
                if (subKey != null)
                {
                    String idDefaultEnterprise = BajoNivel.Encriptar(idEnterprise.ToString());
                    subKey.SetValue("IDEMPROM", idDefaultEnterprise);
                }
            } catch (UnauthorizedAccessException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
        }

        public static int getCurrentIdEnterprise()
        {
            int id = 0;
            try
            {
                String rutaRegistro = "Software\\Syncs\\SyncTPV\\Data";
                String[] namesExists = Registry.LocalMachine.GetSubKeyNames();
                Registry.LocalMachine.CreateSubKey(rutaRegistro);
                var subKey = Registry.LocalMachine.OpenSubKey(rutaRegistro, true);
                if (subKey != null)
                {
                    var idEnterprise = subKey.GetValue("IDEMPROM");
                    if (idEnterprise != null)
                        id = Convert.ToInt32(AES.Desencriptar(idEnterprise.ToString()));
                }
            } catch (UnauthorizedAccessException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            return id;
        }

        public static int getIdDefaultCustomer()
        {
            int id = 0;
            try
            {
                String rutaRegistro = "Software\\Syncs\\SyncTPV\\Data";
                String[] namesExists = Registry.LocalMachine.GetSubKeyNames();
                Registry.LocalMachine.CreateSubKey(rutaRegistro);
                var subKey = Registry.LocalMachine.OpenSubKey(rutaRegistro, true);
                if (subKey != null)
                {
                    var idCustomer = subKey.GetValue("IDDCPG");
                    if (idCustomer != null)
                        id = Convert.ToInt32(AES.Desencriptar(idCustomer.ToString()));
                }
            } catch (UnauthorizedAccessException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            return id;
        }

        public static void saveIdUserInTurn(int idCustomer)
        {
            try
            {
                String rutaRegistro = "Software\\Syncs\\SyncTPV\\Data";
                String[] namesExists = Registry.LocalMachine.GetSubKeyNames();
                Registry.LocalMachine.CreateSubKey(rutaRegistro);
                var subKey = Registry.LocalMachine.OpenSubKey(rutaRegistro, true);
                if (subKey != null)
                {
                    String idDefaultCustomer = BajoNivel.Encriptar(idCustomer.ToString());
                    subKey.SetValue("IDUIT", idDefaultCustomer);
                }
            } catch (UnauthorizedAccessException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
        }

        public static int getIdUserInTurn()
        {
            int id = 0;
            String rutaRegistro = "Software\\Syncs\\SyncTPV\\Data";
            String[] namesExists = Registry.LocalMachine.GetSubKeyNames();
            Registry.LocalMachine.CreateSubKey(rutaRegistro);
            var subKey = Registry.LocalMachine.OpenSubKey(rutaRegistro, true);
            if (subKey != null)
            {
                var idCustomer = subKey.GetValue("IDUIT");
                id = Convert.ToInt32(AES.Desencriptar(idCustomer.ToString()));
            }
            return id;
        }

        public static void saveLoginStatus(Boolean status)
        {
            try
            {
                String rutaRegistro = "Software\\Syncs\\SyncTPV\\Data";
                String[] namesExists = Registry.LocalMachine.GetSubKeyNames();
                Registry.LocalMachine.CreateSubKey(rutaRegistro);
                var subKey = Registry.LocalMachine.OpenSubKey(rutaRegistro, true);
                if (subKey != null)
                {
                    subKey.SetValue("LOGINUIT", status);
                }
            } catch (UnauthorizedAccessException e)
            {
                SECUDOC.writeLog(e.ToString());
            }            
        }

        public static void saveRememberLogin(Boolean status)
        {
            try
            {
                String rutaRegistro = "Software\\Syncs\\SyncTPV\\Data";
                String[] namesExists = Registry.LocalMachine.GetSubKeyNames();
                Registry.LocalMachine.CreateSubKey(rutaRegistro);
                var subKey = Registry.LocalMachine.OpenSubKey(rutaRegistro, true);
                if (subKey != null)
                {
                    subKey.SetValue("REMEMBERLOGIN", status);
                }
            } catch (UnauthorizedAccessException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
        }

        public static Boolean getStatusLogin()
        {
            bool status = false;
            try
            {
                String rutaRegistro = "Software\\Syncs\\SyncTPV\\Data";
                String[] namesExists = Registry.LocalMachine.GetSubKeyNames();
                Registry.LocalMachine.CreateSubKey(rutaRegistro);
                var subKey = Registry.LocalMachine.OpenSubKey(rutaRegistro, true);
                if (subKey != null)
                {
                    status = Convert.ToBoolean(subKey.GetValue("LOGINUIT"));
                }
            } catch (UnauthorizedAccessException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            return status;
        }

        public static Boolean getRememberLogin()
        {
            bool status = false;
            try
            {
                String rutaRegistro = "Software\\Syncs\\SyncTPV\\Data";
                String[] namesExists = Registry.LocalMachine.GetSubKeyNames();
                Registry.LocalMachine.CreateSubKey(rutaRegistro);
                var subKey = Registry.LocalMachine.OpenSubKey(rutaRegistro, true);
                if (subKey != null)
                {
                    status = Convert.ToBoolean(subKey.GetValue("REMEMBERLOGIN"));
                }
            } catch (UnauthorizedAccessException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            return status;
        }
    }
}
