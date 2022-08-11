using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using wsROMClases.Models.Panel;

namespace SyncTPV.Controllers
{
    public class SendAdditionalCustomerController
    {

        public static async Task<ExpandoObject> enviarClientesAdicionales(int idClienteADC)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    int idCustomer = Math.Abs(idClienteADC);
                    CustomerADCModel cm = CustomerADCModel.getAllDataForACustomerADCNotSent(idCustomer);
                    if (cm != null)
                    {
                        String nameUser = UserModel.getNameUser(ClsRegeditController.getIdUserInTurn()); ;
                        if (ConfiguracionModel.isLANPermissionActivated())
                        {
                            dynamic responseCustomer = await enviarClienteAlLAN(cm.id, nameUser, cm.nombre, cm.zona, cm.ciudad, cm.estado,
                                cm.calle, cm.numero, cm.colonia, cm.poblacion, cm.referencia, cm.telefono, cm.cp, cm.email, 
                                cm.rfc, "", cm.tipoContribuyente, cm.codigoUsoCFDI, cm.codigoRegimenFiscal);
                            value = responseCustomer.value;
                            description = responseCustomer.description;
                        }
                        else
                        {
                            dynamic responseCustomer = await enviarClienteAlWs(cm.id, nameUser, cm.nombre, cm.zona, cm.ciudad,
                                cm.estado, cm.calle, cm.numero, cm.colonia, cm.poblacion, cm.referencia,
                                cm.telefono, cm.cp, cm.email, cm.rfc, "Cliente adicional agregado en SyncTPV",
                                cm.tipoContribuyente, cm.codigoRegimenFiscal, cm.codigoUsoCFDI);
                            value = responseCustomer.value;
                            description = responseCustomer.description;
                        }
                    }
                    else
                    {
                        description = "Este cliente ya fue enviado al servidor";
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

        private class ResponseClienteADC
        {
            public int value { get; set; }
            public String description { get; set; }
            public int idClienteAdc { get; set; }
        }

        private static async Task<ExpandoObject> enviarClienteAlWs(int idClienteLocal, String nombreUsuario, String nombreCliente, 
            int clasificationId1, int ciudadId, int estadoId, String calle, String numero, String colonia, String poblacion,
            String referencia, String telefono, String codigoPostal, String email, String rfc, String observations,
            int tipoContribuyente, String codigoRegimenFiscal, String codigoUsoCFDI)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                try
                {
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/insertCustomerAdc", Method.Post);
                    request.AddJsonBody(new
                    {
                        nombreUsuario = nombreUsuario,
                        nombreCliente = nombreCliente,
                        clasificationId1 = clasificationId1,
                        ciudadId = ciudadId,
                        estadoId = estadoId,
                        calle = calle,
                        numero = numero,
                        colonia = colonia,
                        poblacion = poblacion,
                        referencia = referencia,
                        telefono = telefono,
                        codigoPostal = codigoPostal,
                        email = email,
                        rfc = rfc,
                        observations = observations,
                        tipoContribuyente = tipoContribuyente,
                        codigoRegimenFiscal = codigoRegimenFiscal,
                        codigoUsoCFDI = codigoUsoCFDI
                    });
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        var content = responseHeader.Result.Content; // Raw content as string
                        ResponseClienteADC jsonResp = JsonConvert.DeserializeObject<ResponseClienteADC>(content);
                        if (jsonResp.value > 0)
                        {
                            int idClienteAdc = jsonResp.idClienteAdc;
                            if (CustomerADCModel.updateEnviadoYComercialId(idClienteAdc, idClienteLocal) > 0)
                            {
                                if (idClienteLocal > 0)
                                {
                                    String idLocalText = "-" + idClienteLocal;
                                    idClienteLocal = Convert.ToInt32(idLocalText);
                                    String idServerText = "-" + idClienteAdc;
                                    idClienteAdc = Convert.ToInt32(idServerText);
                                }
                                DocumentModel.updateCustomerId(idClienteLocal, idClienteAdc);
                            }
                            value = 1;
                        }
                        else
                        {
                            description = jsonResp.description;
                        }
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                    {
                        value = -400;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value,
                            responseHeader.Result.ErrorException.Message);
                    }
                    else if (responseHeader.Result.ResponseStatus == ResponseStatus.TimedOut)
                    {
                        value = -404;
                        description = MetodosGenerales.getErrorMessageFromNetworkCode(value,
                            responseHeader.Result.ErrorException.Message);
                    }
                    else
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

        private static async Task<ExpandoObject> enviarClienteAlLAN(int id, String nombreUsuario, String nombreCliente, 
            int clasificationId1, int ciudadId, int estadoId, String calle, String numero, String colonia, String poblacion,
            String referencia, String telefono, String codigoPostal, String email, String rfc, String observations,
            int tipoContribuyente, String codigoRegimenFiscal, String codigoUsoCFDI)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            await Task.Run(async () =>
            {
                try
                {
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    dynamic responseCustomer = ClsClienteADCModel.createClienteADC(panelInstance, nombreUsuario, nombreCliente,
                        clasificationId1, ciudadId, estadoId, calle, numero, colonia, poblacion, referencia, telefono,
                        codigoPostal, email, rfc, observations,
                        tipoContribuyente, codigoRegimenFiscal, codigoUsoCFDI);
                    if (responseCustomer.value > 0)
                    {
                        int idClienteAdc = responseCustomer.idClienteAdc;
                        if (CustomerADCModel.updateEnviadoYComercialId(idClienteAdc, id) > 0)
                        {
                            if (id > 0)
                            {
                                String idLocalText = "-" + id;
                                id = Convert.ToInt32(idLocalText);
                                String idServerText = "-" + idClienteAdc;
                                idClienteAdc = Convert.ToInt32(idServerText);
                            }
                            DocumentModel.updateCustomerId(id, idClienteAdc);
                            value = 1;
                        }
                        else description = "No se pudo actualizar el Id del cliente descargado del servidor!";
                    }
                    else
                    {
                        description = responseCustomer.description;
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

    }
}
