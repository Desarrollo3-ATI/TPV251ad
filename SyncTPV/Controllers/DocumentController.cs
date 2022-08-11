using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Models;
using SyncTPV.Views;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncTPV.Controllers
{
    public class DocumentController
    {

        public static async Task<ExpandoObject> agentDocumentSynchronizedToCommercialAPI()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(() =>
            {
                int value = 0;
                String description = "";
                try
                {
                    int lastId = 0;
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    // client.Authenticator = new HttpBasicAuthenticator(username, password);
                    var request = new RestRequest("/validarDocumentosNoEnviadoDeUnAgente", Method.Get);
                    request.AddParameter("claveAgente", UserModel.getCode(ClsRegeditController.getIdUserInTurn()));
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content;
                        var jsonResp = JsonConvert.DeserializeObject<ResponseDocumentosPendientes>(content);
                        ResponseDocumentosPendientes pendingDocuments = (ResponseDocumentosPendientes)jsonResp;
                        if (pendingDocuments.response != null)
                        {
                            int valor = 0;
                            for (int i = 0; i < pendingDocuments.response.Count; i++)
                            {
                                valor = pendingDocuments.response[i].valor;
                                description = pendingDocuments.response[i].descripcion;
                            }
                            value = valor;
                        } else
                        {
                            description = "Servidor encontrado, pero no pudimos obtener la respuesta (response null)";
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        value = -400;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value,
                            responseHeader.Result.ErrorException.Message);
                    } else if (responseHeader.Result.ResponseStatus == ResponseStatus.TimedOut)
                    {
                        value = -404;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value, 
                            responseHeader.Result.ErrorException.Message);
                    } else
                    {
                        value = -500;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value,
                            responseHeader.Result.ErrorException.Message);
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

        public static async Task<ExpandoObject> agentDocumentSynchronizedToCommercialLAN()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(() =>
            {
                int value = 0;
                String description = "";
                try
                {
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    int numdocsNoSends = ClsDocumentoModel.verificarDocsNoEnviadosDeUnAgente(panelInstance, UserModel.getCode(ClsRegeditController.getIdUserInTurn()));
                    if (numdocsNoSends > 0)
                    {
                        value = numdocsNoSends;
                        description = "Aún hay documentos pendientes por enviar a comercial";
                    }
                    else if (numdocsNoSends == -1)
                    {
                        value = numdocsNoSends;
                        description = "El Código del agente no existe";
                    }
                    else
                    {
                        value = numdocsNoSends;
                        description = "No hay documentos pendientes de enviar a comercial";
                    }
                } catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    value = -1;
                    description = e.Message;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                }
            });
            return response;
        }


        public static async Task<String> updateObervationInCurrentDocument(int idDocument, int call, String observation)
        {
            await Task.Run(async () =>
            {
                Boolean updatedObservation = DocumentModel.updateTheDocumentObservation(idDocument, observation);
                if (updatedObservation)
                    observation = DocumentModel.getDocumentObservation(idDocument);
                /*if (call == FormPayCart.CALL_UPDATE_OBSERVATION_DOCUMENT_DATA)
                {
                    return observation;
                }
                else
                {
                    return observation;
                }*/
            });
            return observation;
        }

    }

    public class ResponseDocumentosPendientes
    {
        public List<DocumentosPendientes> response { get; set; }
        public class DocumentosPendientes
        {
            public int valor { get; set; }
            public String descripcion { get; set; }
        }
    }

}
