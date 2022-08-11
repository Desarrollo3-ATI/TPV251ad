using System;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SyncTPV
{
    public class clsFTP
    {
        int Error = 0;
        public int Download(string url, string ftpUserName, string ftpPassWord, string file, ref string MensajeError)
        {
            try
            {
                string uri = url + "/" + file;
                Uri serverUri = new Uri(uri);
                if (serverUri.Scheme != Uri.UriSchemeFtp)
                {
                    return 1;
                }
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url + "/" + file);
                request.UseBinary = true;
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(ftpUserName, ftpPassWord);
                request.KeepAlive = false;
                request.UsePassive = false;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                FileStream writeStream = new FileStream(MetodosGenerales.rootDirectory + "\\Imagenes" + "\\" + "Articulos" + "\\" + file, FileMode.Create);
                int Length = 2048;
                Byte[] buffer = new Byte[Length];
                int bytesRead = responseStream.Read(buffer, 0, Length);
                while (bytesRead > 0)
                {
                    writeStream.Write(buffer, 0, bytesRead);
                    bytesRead = responseStream.Read(buffer, 0, Length);
                }
                writeStream.Close();
                response.Close();
            }
            catch (WebException wEx)
            {
                MensajeError = wEx.ToString();
                SECUDOC.writeLog(wEx.ToString());
                Error = 1;
            }
            return Error;
        }

        public int DownloadCliente(string url, string ftpUserName, string ftpPassWord, string file, ref string MensajeError)
        {
            try
            {
                string uri = url + "/" + file;
                Uri serverUri = new Uri(uri);
                if (serverUri.Scheme != Uri.UriSchemeFtp)
                {
                    return 1;
                }
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url + "/" + file);
                request.UseBinary = true;
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(ftpUserName, ftpPassWord);
                request.KeepAlive = false;
                request.UsePassive = false;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                FileStream writeStream = new FileStream(MetodosGenerales.rootDirectory + "\\Imagenes" + "\\" + "Clientes" + "\\" + file, FileMode.Create);
                int Length = 2048;
                Byte[] buffer = new Byte[Length];
                int bytesRead = responseStream.Read(buffer, 0, Length);
                while (bytesRead > 0)
                {
                    writeStream.Write(buffer, 0, bytesRead);
                    bytesRead = responseStream.Read(buffer, 0, Length);
                }
                writeStream.Close();
                response.Close();
            }
            catch (WebException wEx)
            {
                MensajeError = wEx.ToString();
                SECUDOC.writeLog(wEx.ToString());
                Error = 1;
            }
            return Error;
        }

        public string[] GetFileList(string url, string ftpUserName, string ftpPassWord, ref string MensajeError)
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            WebResponse response = null;
            StreamReader reader = null;

            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
                request.UseBinary = true;
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(ftpUserName, ftpPassWord);
                request.KeepAlive = false;
                request.UsePassive = false;
                response = request.GetResponse();
                reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                SECUDOC.writeLog(ex.ToString());
                if (ex.Message.Equals("Unable to connect to the remote server"))
                {

                }
                MensajeError = ex.ToString();
                if (reader != null)
                {
                    reader.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                downloadFiles = null;
                return downloadFiles;
            }
        }

        public ExpandoObject uploadFile(string url, string ftpUserName, string ftpPassword, string path)
        {
            dynamic response = new ExpandoObject();
            int error = 0;
            String errorMessage = "";
            try {
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(url);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = true;
                using (FileStream stream = File.Open(path, FileMode.Open))
                {
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    using (Stream reqStream = request.GetRequestStream())
                    {
                        reqStream.Write(buffer, 0, buffer.Length);
                        reqStream.Flush();
                        reqStream.Close();
                        error = 1;
                        errorMessage = "";
                    }
                    stream.Flush();
                    stream.Close();
                }
            } catch (Exception ex) {
                SECUDOC.writeLog(ex.ToString());
                error = -1;
                errorMessage = "" + ex.Message;
            } finally {
                response.valor = error;
                response.description = errorMessage;
            }
            return response;
        }

    }
}
