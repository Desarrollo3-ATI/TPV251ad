using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wsROMClases.Models;

namespace SyncTPV.Controllers
{
    public class AperturaTurnoController
    {
        public static async Task<AperturaTurnoModel> completeJSONValue(int userId)
        {
            AperturaTurnoModel atm = null;
            await Task.Run(() => {
                string dateNow = DateTime.Now.ToString("yyyyMMdd");
                String query = "SELECT * FROM " + LocalDatabase.TABLA_APERTURATURNO + " WHERE " +LocalDatabase.CAMPO_USERID_APERTURATURNO+" = "+userId+
                " AND "+LocalDatabase.CAMPO_STATUS_APERTURATURNO+" = "+0+" AND "+LocalDatabase.CAMPO_SERVERID_APERTURATURNO+" = "+0;
                atm = AperturaTurnoModel.getARecord(query);
                /*if (atm != null)
                {
                    value += "{\n" +
                        " \"id\":" + atm.id + ",\n" +
                        " \"userId\":" + atm.userId + ",\n" +
                        " \"importe\":" + atm.importe + ",\n" +
                        " \"fechaHora\":\"" + atm.fechaHora + "\",\n" +
                        " \"createdAt\":\"" + atm.createdAt + "\"\n" +
                        "}";
                }*/
            });
            return atm;
        }

        public static async Task<ExpandoObject> insertCheckoutOpeningToServer(AperturaTurnoModel apertura, String codigoCaja)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(() => {
                int value = 0;
                String description = "";
                try
                {
                    int itemsToEvaluate = 0;
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    // client.Authenticator = new HttpBasicAuthenticator(username, password);
                    var request = new RestRequest("/insertCheckoutOpening", Method.Post);
                    //request.AddParameter("application/json", dataJSON, ParameterType.RequestBody);
                    request.AddJsonBody(new
                    {
                        id = apertura.id,
                        userId = apertura.userId,
                        importe = apertura.importe,
                        fechaHora = apertura.fechaHora,
                        createdAt = apertura.createdAt,
                        codigoCaja = codigoCaja
                    });
                    //request.AddJsonBody(dataJSON);
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed) {
                        var jsonResp = JsonConvert.DeserializeObject<ResponseAperturaTurno>(responseHeader.Result.Content);
                        ResponseAperturaTurno respJson = (ResponseAperturaTurno)jsonResp;
                        if (respJson.response != null)
                        {
                            int records = AperturaTurnoModel.marcarComoEnviado(respJson.response.id, jsonResp.response.userId,
                                respJson.response.createdAt);
                            if (records > 0) {
                                value = 1;
                            }
                        } else {
                            value = -2;
                            description = "La respuesta del servidor a sido nula";
                        }
                    } else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error) {
                        if (responseHeader.Result.ErrorMessage.Equals("Unable to connect to the remote server")) {
                            value = -404;
                        } else {
                            value = -500;
                        }
                    }
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    response = -1;
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

        public static async Task<ExpandoObject> insertCheckoutOpeningToServerLAN(String panelInstance, int userId, double importe, 
            String fechaHora, String createdAt, String codigoCaja)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () => {
                int value = 0;
                String description = "";
                ClsAperturaCajaModel acm = null;
                try
                {
                    
                    int idCreated = ClsAperturaCajaModel.createUpdateOrDeleteARecord(panelInstance, userId, importe, fechaHora, createdAt, 
                        codigoCaja);
                    if (idCreated > 0)
                    {
                        if (AperturaTurnoModel.checkIfAperturaExist(userId, createdAt))
                        {
                            int records = AperturaTurnoModel.marcarComoEnviado(idCreated, userId, createdAt);
                            if (records > 0)
                            {
                                value = 1;
                            } else
                            {
                                description = "Algo falló al actualizar la apertura localmente!";
                            }
                        } else
                        {
                            int lastIdAperturaLocal = AperturaTurnoModel.getLastId();
                            lastIdAperturaLocal++;
                            int records = AperturaTurnoModel.saveNewApertura(lastIdAperturaLocal, userId, importe, fechaHora, createdAt,
                                1, idCreated);
                            if (records > 0)
                            {
                                value = 1;
                            }
                            else
                            {
                                description = "Algo falló al guardar la apertura localmente!";
                            }
                        }
                    } else
                    {
                        description = "Ocurrió un error al intentar agregar la apertura de caja en el servidor!";
                    }
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    value = -1;
                    description = e.Message;
                } finally
                {
                    response.value = value;
                    response.description = description;
                    response.acm = acm;
                }
            });
            return response;
        }

    }
}
