using RestSharp;
using RestSharp.Extensions;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncTPV.Controllers
{
    public class UploadAndDownloadFilesController
    {

        public static async Task<ExpandoObject> getInvoiceFileWs(String serieDocumento, int folioDocumento, String rfcCliente, String extension)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(() =>
            {
                int valor = 0;
                String description = "";
                try
                {
                    DirectoriosModel dm = DirectoriosModel.getDirectorioByType(DirectoriosModel.TIPO_FACTURAS_TERMINAL);
                    if (dm != null)
                    {
                        String nombreArchivo = serieDocumento + "" + folioDocumento;
                        String path = dm.ruta + "\\" + nombreArchivo + "." + extension;
                        String url = ConfiguracionModel.getLinkWs();
                        url = url.Replace(" ", "%20");
                        var client = new RestClient(url);
                        var request = new RestRequest("/getInvoiceFiles", Method.Post);
                        request.AddJsonBody(new
                        {
                            rfc = rfcCliente,
                            serie = serieDocumento,
                            folio = folioDocumento,
                            extensionArchivo = extension
                        });
                        var responseHeader = client.DownloadDataAsync(request);
                        if (responseHeader != null && responseHeader.Result.Length > 0)
                        {
                            //responseHeader.SaveAs(path);
                            //responseHeader.Result.IsSynchronized(path);
                            File.WriteAllBytes(path, responseHeader.Result);
                            valor = 1;
                            description = path;
                        } else
                        {
                            description = "No pudimos obtener el archivo en el servidor\r\n" +
                            "Validar que exista en la ruta correcta con el nombre de "+nombreArchivo+"."+extension;
                        }
                    } else
                    {
                        description = "Tienes que agregar la ruta del directorio local para guardar las facturas";
                    }
                }
                catch (Exception ex)
                {
                    SECUDOC.writeLog(ex.ToString());
                    valor = -1;
                    description = ex.Message;
                }
                finally
                {
                    response.valor = valor;
                    response.description = description;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> getInvoiceFileLAN(String serieDocumento, int folioDocumento, String rfcCliente, String extension)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(() =>
            {
                int valor = 0;
                String description = "";
                try
                {
                    DirectoriosModel dm = DirectoriosModel.getDirectorioByType(DirectoriosModel.TIPO_FACTURAS_TERMINAL);
                    if (dm != null)
                    {
                        DirectoriosModel dmServer = DirectoriosModel.getDirectorioByType(DirectoriosModel.TIPO_FACTURAS_SERVER);
                        if (dmServer != null)
                        {
                            String nombreArchivo = "";
                            String sourcePath = "";
                            if (dmServer.ruta.Contains("XML_SDK"))
                            {
                                nombreArchivo = serieDocumento + "" + folioDocumento;
                                //var path = dm.ruta + "\\" + nombreArchivo + "." + extension;
                                sourcePath = @"" + dmServer.ruta + "\\" + nombreArchivo + "." + extension;
                            } else
                            {
                                String folio = folioDocumento.ToString("D10");
                                nombreArchivo = rfcCliente + "F" + serieDocumento + "" + folio;
                                //var path = dm.ruta + "\\" + nombreArchivo + "." + extension;
                                sourcePath = @"" + dmServer.ruta + "\\" + nombreArchivo + "." + extension;
                            }
                            string[] stringSeparators = new string[] { ":", "\\" };
                            String[] parts = sourcePath.Split(stringSeparators, StringSplitOptions.None);
                            if (parts[1] != null && parts[1].Equals(""))
                            {
                                String serverIP = InstanceSQLSEModel.getIPServerValue(2);
                                sourcePath = @"\\"+serverIP;
                                for (int i = 0; i < parts.Length; i++)
                                {
                                    if (i > 0)
                                        if (i > 1)
                                        {
                                            if (i < parts.Length - 1)
                                                sourcePath += "\\" + parts[i];
                                        }
                                }
                            }
                            sourcePath = sourcePath.Replace("C:","");
                            string targetPath = @"" + dm.ruta;

                            // Use Path class to manipulate file and directory paths.
                            string sourceFile = Path.Combine(sourcePath, nombreArchivo + "." + extension);
                            string destFile = Path.Combine(targetPath, nombreArchivo + "." + extension);
                            //if (System.IO.Directory.Exists(serverPath))
                            if (!File.Exists(sourcePath))
                            {
                                // To copy a file to another location and
                                // overwrite the destination file if it already exists.
                                File.Copy(sourceFile, destFile, true);
                                valor = 1;
                                description = destFile;
                                /*string[] files = System.IO.Directory.GetFiles(serverPath);

                                // Copy the files and overwrite destination files if they already exist.
                                foreach (string s in files)
                                {
                                    // Use static Path methods to extract only the file name from the path.
                                    fileName = System.IO.Path.GetFileName(s);
                                    destFile = System.IO.Path.Combine(targetPath, fileName);
                                    System.IO.File.Copy(s, destFile, true);
                                }*/
                            }
                            else
                            {
                                description = "No fue posible obtener el archivo en el servidor";
                            }
                        } else
                        {
                            description = "Tienes que agregar la ruta del directorio en el servidor para guardar las facturas";
                        }
                        

                        /*String url = ClsConfiguracionModel.getLinkWs();
                        url = url.Replace(" ", "%20");
                        var client = new RestClient(url + "/getInvoiceFiles");
                        var request = new RestRequest(Method.POST);
                        String savedData = "{\"nombreArchivo\":\"" + nombreArchivo + "\", \"extensionArchivo\":\"" + extension + "\"}";
                        request.AddParameter("application/json", savedData, ParameterType.RequestBody);
                        var responseHeader = client.DownloadData(request);
                        if (responseHeader.Length > 0)
                        {
                            responseHeader.SaveAs(path);
                        } else
                        {
                            
                        }*/
                    } else
                    {
                        description = "Tienes que agregar la ruta del directorio local para guardar las facturas";
                    }
                }
                catch (Exception ex)
                {
                    SECUDOC.writeLog(ex.ToString());
                    valor = -1;
                    description = ex.Message;
                }
                finally
                {
                    response.valor = valor;
                    response.description = description;
                }
            });
            return response;
        }

    }
}
