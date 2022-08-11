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
    public class CiudadesController
    {
        public static async Task<ExpandoObject> downloadAllCities()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    int lastId = 0;
                    int itemsToEvaluate = 0;
                    String url = ConfiguracionModel.getLinkWs();
                    url = url.Replace(" ", "%20");
                    var client = new RestClient(url);
                    var request = new RestRequest("/getAllCities", Method.Post);
                    request.AddJsonBody(new {});
                    var responseHeader = client.ExecuteAsync(request);
                    if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                    {
                        ResponseCities responseCities = JsonConvert.DeserializeObject<ResponseCities>(responseHeader.Result.Content);
                        if (responseCities.value == 1)
                        {
                            if (responseCities.ciudadesList != null)
                            {
                                int citiesSaved = 0;
                                if (lastId == 0)
                                {
                                    citiesSaved = CiudadesModel.getNumberOfCitiesSaved();
                                    if (citiesSaved != responseCities.ciudadesList.Count)
                                        CiudadesModel.deleteAllCities();
                                }
                                citiesSaved = CiudadesModel.getNumberOfCitiesSaved();
                                if (citiesSaved != responseCities.ciudadesList.Count)
                                    lastId = CiudadesModel.saveAllCities(responseCities.ciudadesList);
                                itemsToEvaluate = responseCities.ciudadesList.Count;
                                value = 1;
                            }
                            else
                            {
                                description = "No se encontró ninguna Ciudad por descargar, validar base de " +
                                "datos CATSAT en la instancia del PanelROM";
                            }
                        } else
                        {
                            value = responseCities.value;
                            description = responseCities.description;
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
                catch (Exception e)
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

        private class ResponseCities
        {
            public int value { get; set; }
            public String description { get; set; }
            public List<ClsCiudadesModel> ciudadesList { get; set; }
        }

        public static async Task<ExpandoObject> downloadAllCitiesLAN()
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                try
                {
                    int lastId = 0;
                    String catsatInstance = InstanceSQLSEModel.getStringCATSATInstance();
                    String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                    dynamic responseCities = ClsCiudadesModel.getAllCities(catsatInstance, panelInstance);
                    if (responseCities.value == 1)
                    {
                        List<ClsCiudadesModel> citiesList = responseCities.ciudadesList;
                        if (citiesList != null)
                        {
                            int itemsToEvaluate = citiesList.Count;
                            int citiesSaved = 0;
                            if (lastId == 0)
                            {
                                citiesSaved = CiudadesModel.getNumberOfCitiesSaved();
                                if (citiesSaved != citiesList.Count)
                                    CiudadesModel.deleteAllCities();
                            }
                            int count = 0;
                            citiesSaved = CiudadesModel.getNumberOfCitiesSaved();
                            if (citiesSaved != citiesList.Count)
                                count = CiudadesModel.saveAllCitiesLAN(citiesList);
                            if (count == itemsToEvaluate)
                                value = 1;
                        }
                        else
                        {
                            description = "No se encontró ninguna Ciudad por descargar, validar base de datos CATSAT " +
                            "en la instancia del PanelROM";
                        }
                    } else
                    {
                        value = responseCities.value;
                        description = responseCities.description;
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

        public static async Task<ExpandoObject> getAllCitiesByEstadoLAN(String codigoEstado)
        {
            dynamic response = new ExpandoObject();
            await Task.Run(async () =>
            {
                int value = 0;
                String description = "";
                List<ClsCiudadesModel> citiesList = null;
                try
                {
                    int lastId = 0;
                    String catsatInstance = InstanceSQLSEModel.getStringCATSATInstance();
                    citiesList = ClsCiudadesModel.getAllCitiesFromAnCountry(catsatInstance, codigoEstado);
                    if (citiesList != null)
                    {
                        value = 1;
                    }
                    else
                    {
                        description = "No se encontró ninguna Ciudad por descargar, validar base de datos CATSAT en la instancia del PanelROM";
                    }
                }
                catch (Exception e)
                {
                    SECUDOC.writeLog(e.ToString());
                    value = -1;
                    description = e.Message;
                }
                finally
                {
                    response.value = value;
                    response.description = description;
                    response.citiesList = citiesList;
                }
            });
            return response;
        }

    }
}
