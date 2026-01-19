using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using MAT.Utilities;
using MAT.MVC.Infrastructure;


namespace MAT.MVC.Integration
{

    public class ApiClient: IApiClient
    {
        private readonly HttpClient _httpClient;      

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;           
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            try
            {
                var response = await _httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                var jsonToken = JToken.Parse(content);
                if (jsonToken.Type == JTokenType.Object) {
                    var jsonResponse = JObject.Parse(content);
                    if (jsonResponse.ContainsKey("ok") && jsonResponse.ContainsKey("data"))
                    {
                        return jsonResponse["data"].ToObject<T>();
                    }
                }

                return JsonConvert.DeserializeObject<T>(content);

            }
            catch (TaskCanceledException ex)
            {
                // Timeout / cancelación (evitar HTML en excepción)
                var correlationId = RequestContext.GetOrCreateCorrelationId();
                MATLogger.Log($"[{correlationId}] BackendAPI timeout/cancel. Url: {uri}. {ex.Message}", 1);
                throw new Exception($"Error with url: {uri}. Timeout del servicio. ID: {correlationId}");
            }
            catch (HttpRequestException ex)
            {
                var correlationId = RequestContext.GetOrCreateCorrelationId();
                MATLogger.Log($"[{correlationId}] BackendAPI HTTP error. Url: {uri}. {ex.Message}", 1);
                throw new Exception($"Error with url: {uri}. No se pudo conectar al servicio. ID: {correlationId}");
            }
            catch (Exception ex)
            {
                var correlationId = RequestContext.GetOrCreateCorrelationId();
                MATLogger.Log($"[{correlationId}] BackendAPI error. Url: {uri}. {ex.Message}", 1);
                throw new Exception($"Error with url: {uri}. Ocurrió un error inesperado. ID: {correlationId}");
            }
            
        }

        public async Task<T> PostAsync<T>(string uri, object data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(uri, content);
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);
            }
            catch (TaskCanceledException ex)
            {
                var correlationId = RequestContext.GetOrCreateCorrelationId();
                MATLogger.Log($"[{correlationId}] BackendAPI timeout/cancel (POST). Url: {uri}. {ex.Message}", 1);
                throw new Exception($"Error with url: {uri}. Timeout del servicio. ID: {correlationId}");
            }
            catch (HttpRequestException ex)
            {
                var correlationId = RequestContext.GetOrCreateCorrelationId();
                MATLogger.Log($"[{correlationId}] BackendAPI HTTP error (POST). Url: {uri}. {ex.Message}", 1);
                throw new Exception($"Error with url: {uri}. No se pudo conectar al servicio. ID: {correlationId}");
            }
            catch (Exception ex)
            {
                var correlationId = RequestContext.GetOrCreateCorrelationId();
                MATLogger.Log($"[{correlationId}] BackendAPI error (POST). Url: {uri}. {ex.Message}", 1);
                throw new Exception($"Error with url: {uri}. Ocurrió un error inesperado. ID: {correlationId}");
            }
        }

        public async Task<T> PutAsync<T>(string uri, object data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync(uri, content);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);
            }
            catch (TaskCanceledException ex)
            {
                var correlationId = RequestContext.GetOrCreateCorrelationId();
                MATLogger.Log($"[{correlationId}] BackendAPI timeout/cancel (PUT). Url: {uri}. {ex.Message}", 1);
                throw new Exception($"Error with url: {uri}. Timeout del servicio. ID: {correlationId}");
            }
            catch (HttpRequestException ex)
            {
                var correlationId = RequestContext.GetOrCreateCorrelationId();
                MATLogger.Log($"[{correlationId}] BackendAPI HTTP error (PUT). Url: {uri}. {ex.Message}", 1);
                throw new Exception($"Error with url: {uri}. No se pudo conectar al servicio. ID: {correlationId}");
            }
            catch (Exception ex)
            {
                var correlationId = RequestContext.GetOrCreateCorrelationId();
                MATLogger.Log($"[{correlationId}] BackendAPI error (PUT). Url: {uri}. {ex.Message}", 1);
                throw new Exception($"Error with url: {uri}. Ocurrió un error inesperado. ID: {correlationId}");
            }
        }

        public async Task<bool> DeleteAsync(string uri)
        {
            var response = await _httpClient.DeleteAsync(uri);
            return response.IsSuccessStatusCode;
        }
    }
}