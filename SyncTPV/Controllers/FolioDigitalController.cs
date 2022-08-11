using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wsROMClases.Models;
using wsROMClases.Models.Commercial;

namespace SyncTPV.Controllers
{
    public class FolioDigitalController
    {
        public static async Task<ExpandoObject> getFolioDigitalWs(int idDocumentoServer)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () => {
                int error = 0;
                String errorMessage = "";
                try
                {
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    // client.Authenticator = new HttpBasicAuthenticator(username, password);
                    var request = new RestRequest("/getUUIDOfAnInvoice", Method.Post);
                    request.AddJsonBody(new
                    {
                        idDocumento = idDocumentoServer
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var jsonResp = JsonConvert.DeserializeObject<ResponseFoliosDigitales>(responseHeader.Result.Content);
                        ResponseFoliosDigitales responseFd = (ResponseFoliosDigitales)jsonResp;
                        if (responseFd.invoices != null)
                        {
                            foreach (var invoice in responseFd.invoices)
                            {
                                if (FoliosDigitalesModel.validateIfFolioDigitalExistByIdDocumentoServer(idDocumentoServer))
                                {
                                    if (FoliosDigitalesModel.updateFolioDigital(invoice.id, invoice.tipoDocumento, invoice.idConcepto,
                                        idDocumentoServer, invoice.serieConcepto, invoice.folio, invoice.estado, invoice.entregado,
                                        invoice.fechaEmision, invoice.emailEntrega, invoice.rutaDiscoEntrega, invoice.fechaCancelacion,
                                        invoice.horaCancelacion, invoice.tradicionalDigital, invoice.tipoCFDI, invoice.rfcEmisor,
                                        invoice.razonSocialEmisor, invoice.codigoUSoCFDI, invoice.uuid, invoice.idADD, responseFd.idFdPanel))
                                    {
                                        error = invoice.id;
                                    }
                                }
                                else
                                {
                                    error = FoliosDigitalesModel.createFolioDigital(invoice.id, invoice.tipoDocumento, invoice.idConcepto,
                                        idDocumentoServer, invoice.serieConcepto, invoice.folio, invoice.estado, invoice.entregado,
                                        invoice.fechaEmision, invoice.emailEntrega, invoice.rutaDiscoEntrega, invoice.fechaCancelacion,
                                        invoice.horaCancelacion, invoice.tradicionalDigital, invoice.tipoCFDI, invoice.rfcEmisor,
                                        invoice.razonSocialEmisor, invoice.codigoUSoCFDI, invoice.uuid, invoice.idADD, responseFd.idFdPanel);
                                }
                            }
                        }
                        else
                        {
                            if (FoliosDigitalesModel.validateIfFolioDigitalExistByIdDocumentoServer(idDocumentoServer))
                            {
                                if (FoliosDigitalesModel.deleteFolioDigital(idDocumentoServer))
                                    error = 0;
                            }
                            errorMessage = "" + responseFd.description;
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        if (responseHeader.Result.ErrorMessage.Equals("Unable to connect to the remote server"))
                        {
                            error = -404;
                            errorMessage += "Tiempo Excedido, Servidor No Encontrado " + responseHeader.Result.ErrorMessage;
                        }
                        else
                        {
                            error = -500;
                            errorMessage += "Algo falló en el servidor " + responseHeader.Result.ErrorMessage;
                        }
                    }
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    error = -1;
                    errorMessage = "" + e.Message;
                }
                finally
                {
                    response.valor = error;
                    response.description = errorMessage;
                }
            });
            return response;
        }

        public static async Task<ExpandoObject> getFolioDigitalLAN(int idDocumentoServer)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int error = 0;
                String errorMessage = "";
                try
                {
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    String comInstance = InstanceSQLSEModel.getStringComInstance();
                    ClsDocumentoModel documento = ClsDocumentoModel.getADocumentById(panelInstance,
                            idDocumentoServer);
                    if (documento != null)
                    {
                        if ((documento.serieConceptoCom == null || documento.serieConceptoCom.Equals(""))
                            || (documento.folioCom == null || documento.folioCom.Equals("")))
                        {
                            errorMessage = "Código Concepto o Folio no se encontró para el Documento\r\n" +
                                "Validar si el documento está sincronizado a Comercial Premium";
                        }
                        else
                        {
                            int idDocumentoCom = ClsDocumentoComModel.getIdDocumentoByCodigoConceptoAndFolio(
                            comInstance, documento.serieConceptoCom, documento.folioCom);
                            if (idDocumentoCom != 0)
                            {
                                if (!ClsFoliosDigitalesComModel.checkIfFolioDigitalExist(comInstance, idDocumentoCom))
                                {
                                    idDocumentoCom = ClsDocumentoComModel.getIdDocumentoDestinoByIdDocumentoOrigen(comInstance, idDocumentoCom);
                                }
                                ClsFoliosDigitalesComModel ic = ClsFoliosDigitalesComModel.getFolioDigitalFromADocumento(
                                    comInstance, idDocumentoCom);
                                if (ic != null)
                                {
                                    if (ClsFoliosDigitalesModel.validateIfFolioDigitalExist(panelInstance, idDocumentoServer))
                                    {
                                        int idFdPanel = ClsFoliosDigitalesModel.getPanelIdByComId(panelInstance, ic.id);
                                        dynamic fdResponse = ClsFoliosDigitalesModel.updateFolioDigitalDeUnDocumento(panelInstance, ic.tipoDocumento,
                                            ic.idConcepto, idDocumentoServer, ic.serieConcepto, ic.folio, ic.estado, ic.entregado, ic.fechaEmision,
                                            ic.emailEntrega, ic.rutaDiscoEntrega, ic.fechaCancelacion, ic.horaCancelacion, ic.tradicionalDigital,
                                            ic.tipoCFDI, ic.rfcEmisor, ic.razonSocialEmisor, ic.codigoUSoCFDI, ic.uuid, ic.idADD, ic.id, idFdPanel);
                                        if (fdResponse.idFdPanel > 0)
                                        {
                                            if (FoliosDigitalesModel.validateIfFolioDigitalExistByIdDocumentoServer(idDocumentoServer))
                                            {
                                                if (FoliosDigitalesModel.updateFolioDigital(ic.id, ic.tipoDocumento, ic.idConcepto,
                                        idDocumentoServer, ic.serieConcepto, ic.folio, ic.estado, ic.entregado,
                                        ic.fechaEmision, ic.emailEntrega, ic.rutaDiscoEntrega, ic.fechaCancelacion,
                                        ic.horaCancelacion, ic.tradicionalDigital, ic.tipoCFDI, ic.rfcEmisor,
                                        ic.razonSocialEmisor, ic.codigoUSoCFDI, ic.uuid, ic.idADD, fdResponse.idFdPanel))
                                                {
                                                    error = ic.id;
                                                }
                                            }
                                            else
                                            {
                                                error = FoliosDigitalesModel.createFolioDigital(ic.id, ic.tipoDocumento, ic.idConcepto,
                                        idDocumentoServer, ic.serieConcepto, ic.folio, ic.estado, ic.entregado,
                                        ic.fechaEmision, ic.emailEntrega, ic.rutaDiscoEntrega, ic.fechaCancelacion,
                                        ic.horaCancelacion, ic.tradicionalDigital, ic.tipoCFDI, ic.rfcEmisor,
                                        ic.razonSocialEmisor, ic.codigoUSoCFDI, ic.uuid, ic.idADD, fdResponse.idFdPanel);
                                            }
                                        }
                                        else
                                        {
                                            errorMessage = "Algo falló al agregar información de la factura a la base de datos del PanelROM\r\n" +
                                                fdResponse.description;
                                        }
                                    }
                                    else
                                    {
                                        dynamic fdResponse = ClsFoliosDigitalesModel.createFolioDigitalDeUnDocumento(panelInstance, ic.tipoDocumento,
                                            ic.idConcepto, idDocumentoServer, ic.serieConcepto, ic.folio, ic.estado, ic.entregado, ic.fechaEmision,
                                            ic.emailEntrega, ic.rutaDiscoEntrega, ic.fechaCancelacion, ic.horaCancelacion, ic.tradicionalDigital,
                                            ic.tipoCFDI, ic.rfcEmisor, ic.razonSocialEmisor, ic.codigoUSoCFDI, ic.uuid, ic.idADD, ic.id);
                                        if (fdResponse.idFdPanel > 0)
                                        {
                                            if (FoliosDigitalesModel.validateIfFolioDigitalExistByIdDocumentoServer(idDocumentoServer))
                                            {
                                                if (FoliosDigitalesModel.updateFolioDigital(ic.id, ic.tipoDocumento, ic.idConcepto,
                                        idDocumentoServer, ic.serieConcepto, ic.folio, ic.estado, ic.entregado,
                                        ic.fechaEmision, ic.emailEntrega, ic.rutaDiscoEntrega, ic.fechaCancelacion,
                                        ic.horaCancelacion, ic.tradicionalDigital, ic.tipoCFDI, ic.rfcEmisor,
                                        ic.razonSocialEmisor, ic.codigoUSoCFDI, ic.uuid, ic.idADD, fdResponse.idFdPanel))
                                                {
                                                    error = ic.id;
                                                }
                                            }
                                            else
                                            {
                                                error = FoliosDigitalesModel.createFolioDigital(ic.id, ic.tipoDocumento, ic.idConcepto,
                                        idDocumentoServer, ic.serieConcepto, ic.folio, ic.estado, ic.entregado,
                                        ic.fechaEmision, ic.emailEntrega, ic.rutaDiscoEntrega, ic.fechaCancelacion,
                                        ic.horaCancelacion, ic.tradicionalDigital, ic.tipoCFDI, ic.rfcEmisor,
                                        ic.razonSocialEmisor, ic.codigoUSoCFDI, ic.uuid, ic.idADD, fdResponse.idFdPanel);
                                            }
                                        }
                                        else
                                        {
                                            errorMessage = "Algo falló al agregar información de la factura a la base de datos del PanelROM\r\n" +
                                                fdResponse.description;
                                        }
                                    }
                                }
                                else
                                {
                                    if (FoliosDigitalesModel.validateIfFolioDigitalExistByIdDocumentoServer(idDocumentoServer))
                                    {
                                        if (FoliosDigitalesModel.deleteFolioDigital(idDocumentoServer))
                                            error = 0;
                                    }
                                    errorMessage = "La factura que buscas aún no está timbrada!";
                                }
                            }
                            else
                            {
                                errorMessage = "No pudimos encontrar el Documento en Comercial Premium";
                            }
                        }
                    }
                    else
                    {
                        errorMessage = "Documento de SyncROM Panel No Encontrado validar si existe";
                    }
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    error = -1;
                    errorMessage = "Exception: " + e.Message;
                }
                finally
                {
                    response.valor = error;
                    response.description = errorMessage;
                }
            });
            return response;
        }

        public class ResponseFoliosDigitales
        {
            public int idFdPanel { get; set; }
            public int invoicesCount { get; set; }
            public List<FoliosDigitalesModel> invoices { get; set; }
            public String description { get; set; }
        }
    }
}
