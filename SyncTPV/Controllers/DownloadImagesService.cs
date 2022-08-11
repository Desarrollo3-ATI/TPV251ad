using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace SyncTPV.Controllers
{
    public class DownloadImagesService
    {
        public async Task<ExpandoObject> handleDownloadImages()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                DatosTicketModel dtm = DatosTicketModel.getDataForFTPServer();
                if (dtm != null)
                {
                    response = obtainAllAndSaveImagesForItems(dtm.EMPRESA, dtm.FTPSERVER, dtm.FTPPUERTO, dtm.FTPUSER, dtm.FTPPASSWORD);
                    if (response.error != 404)
                        response = obtainAllAndSaveImagesForCustomers(dtm.EMPRESA, dtm.FTPSERVER, dtm.FTPPUERTO, dtm.FTPUSER, dtm.FTPPASSWORD);
                }
                //GeneralMethods.Companion.deleteCache(this);
            });
            return response;
        }

        private ExpandoObject obtainAllAndSaveImagesForItems(String enterpriseName, String server, int portNumber,
                                                String user, String password)
        {
            dynamic response = new ExpandoObject();
            int error = 0;
            string ruta = MetodosGenerales.rootDirectory + "\\Imagenes\\articulos";
            // Especifica el directorio a manipular.
            try {
                if (Directory.Exists(ruta))                         // Revisar si existe el directorio
                {
                    //Directory.Delete(ruta, true);                               // borrarlo
                }
                else
                {
                    DirectoryInfo di = Directory.CreateDirectory(ruta);             //crearlo
                }
            }
            catch (Exception e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            int imagesCount = 0;
            List<ItemModel> itemsList = ItemModel.getallItems("SELECT * FROM " + LocalDatabase.TABLA_ITEM+" WHERE "+LocalDatabase.CAMPO_ID_ITEM+" > 0");
            if (itemsList != null)
            {
                foreach (ItemModel item in itemsList)
                {
                    try
                    {
                        String url = "ftp://" + server + "/" + enterpriseName + "/articulos/" + item.id + "-1.jpg";
                        long lenghtFile = fileSizeDownload(url, user, password);
                        if (lenghtFile != 0)
                        {
                            //url = url.Replace(" ", "%20");
                            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
                            request.Method = WebRequestMethods.Ftp.DownloadFile;
                            request.Credentials = new NetworkCredential(user, password);
                            request.UseBinary = true;
                            FtpWebResponse resp = (FtpWebResponse)request.GetResponse();
                            using (Stream responseStream = resp.GetResponseStream())
                            {
                                using (FileStream fileStream = new FileStream(ruta + "\\" + item.id + "-1.jpg", FileMode.Create))
                                {
                                    byte[] buffer = new byte[lenghtFile];
                                    int read = 0;
                                    while (true)
                                    {
                                        read = responseStream.Read(buffer, 0, buffer.Length);
                                        if (read == 0)
                                            break;
                                        fileStream.Write(buffer, 0, read);
                                        fileStream.Flush();
                                        Console.WriteLine("Downloaded {0} bytes", fileStream.Position);
                                    }
                                    imagesCount++;
                                    if (fileStream != null)
                                        fileStream.Close();
                                }
                            }
                        } else
                        {
                            imagesCount++;
                        }
                    }
                    catch (WebException e)
                    {
                        String status = ((FtpWebResponse)e.Response).StatusDescription;
                        SECUDOC.writeLog(e.ToString()+" - "+ status);
                        if (e.Message.Equals("The remote server returned an error: (550) File unavailable (e.g., file not found, no access).") ||
                            e.Message.Equals("Error en el servidor remoto: (550) Archivo no disponible (ej. no se encuentra el archivo o no se tiene acceso)."))
                        {
                            error = 500;
                        }
                        else
                        {
                            error = 404;
                        }
                        break;
                    }
                }
            }
            response.valor = imagesCount;
            response.error = error;
            return response;
        }

        //get file size for downloading
        private long fileSizeDownload(string ftpAddressFile, String user, String password)
        {
            long dataLength = 0;
            try
            {
                FtpWebRequest request = FtpWebRequest.Create(ftpAddressFile) as FtpWebRequest;
                request.Method = WebRequestMethods.Ftp.GetFileSize;
                request.Credentials = new NetworkCredential(user, password);
                dataLength = (long)request.GetResponse().ContentLength;
            } catch (WebException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            return dataLength;
        }

        private ExpandoObject obtainAllAndSaveImagesForCustomers(String enterpriseName, String server, int portNumber, String user,
                                                    String password)
        {
            dynamic response = new ExpandoObject();
            int error = 0;
            string ruta = MetodosGenerales.rootDirectory + "\\Imagenes\\customers";
            // Especifica el directorio a manipular.
            try
            {
                if (Directory.Exists(ruta))                         // Revisar si existe el directorio
                {
                    // borrarlo
                }
                else
                {
                    DirectoryInfo di = Directory.CreateDirectory(ruta);             //crearlo
                }
            }
            catch (Exception e)
            {
                SECUDOC.writeLog(e.Message);
            }
            int imagesCount = 0;
            String query = "SELECT * FROM " + LocalDatabase.TABLA_CLIENTES+" WHERE "+LocalDatabase.CAMPO_ID_CLIENTE+" > 0";
            List<ClsClienteModel> cmList = CustomerModel.getAllCustomers(query);
            if (cmList != null)
            {
                foreach (ClsClienteModel cm in cmList)
                {
                    try
                    {
                        int bytesRead = 0;
                        String url = "ftp://" + server + "/" + enterpriseName + "/customers/" + cm.CLIENTE_ID + "-c.jpg";
                        long lenghtFile = fileSizeDownload(url, user, password);
                        if (lenghtFile != 0)
                        {
                            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
                            request.Credentials = new NetworkCredential(user, password);
                            request.Method = WebRequestMethods.Ftp.DownloadFile;
                            request.UseBinary = true;
                            FtpWebResponse resp = (FtpWebResponse)request.GetResponse();
                            using (Stream reader = resp.GetResponseStream())
                            {
                                using (FileStream fileStream = new FileStream(ruta + "\\" + cm.CLIENTE_ID + "-c.jpg", FileMode.Create))
                                {
                                    byte[] buffer = new byte[fileStream.Length];
                                    while (true)
                                    {
                                        bytesRead = reader.Read(buffer, 0, buffer.Length);
                                        if (bytesRead == 0)
                                            break;
                                        fileStream.Write(buffer, 0, bytesRead);
                                    }
                                    imagesCount++;
                                    if (fileStream != null)
                                        fileStream.Close();
                                }
                            }
                        } else
                        {
                            imagesCount++;
                        }
                    }
                    catch (WebException e)
                    {
                        String status = ((FtpWebResponse)e.Response).StatusDescription;
                        SECUDOC.writeLog(e.ToString()+" - "+status);
                        if (e.Message.Equals("The remote server returned an error: (550) File unavailable (e.g., file not found, no access).") ||
                            e.Message.Equals("Error en el servidor remoto: (550) Archivo no disponible (ej. no se encuentra el archivo o no se tiene acceso)."))
                        {
                            error = 500;
                        }
                        else
                        {
                            error = 404;
                        }
                        break;
                    }
                }
            }
            response.valor = imagesCount;
            response.error = error;
            return response;
        }

    }
}
