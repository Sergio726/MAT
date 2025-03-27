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
            catch (Exception ex)
            {
                string message = MATLogger.FormatExceptionToHtml(ex, $"Error with url: {uri}");
                throw new Exception(message);
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
            catch (Exception ex)
            {
                throw new Exception($"Error with url: {uri}.\nMessajeError: {ex.Message}.\nStackTrace: {ex.StackTrace}");
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
            catch (Exception ex)
            {
                throw new Exception($"Error with url: {uri}.\nMessajeError: {ex.Message}.\nStackTrace: {ex.StackTrace}");
            }
        }

        public async Task<bool> DeleteAsync(string uri)
        {
            var response = await _httpClient.DeleteAsync(uri);
            return response.IsSuccessStatusCode;
        }
    }
}