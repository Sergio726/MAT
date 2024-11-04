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
                throw new Exception($"Error with url: {uri}. MessajeError: {ex.Message}. StackTrace: {ex.StackTrace}");
            }
            
        }

        public async Task<T> PostAsync<T>(string uri, object data)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, content);            
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result);
        }

        public async Task<T> PutAsync<T>(string uri, object data)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(uri, content);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result);
        }

        public async Task<bool> DeleteAsync(string uri)
        {
            var response = await _httpClient.DeleteAsync(uri);
            return response.IsSuccessStatusCode;
        }


        //public async Task<T> GetAsync<T>(HttpRequestMessage requestMessage, int id = 0, string name = null)
        //{
        //    try
        //    {
        //        using (var client = _httpClientFactory.CreateClient())
        //        {
        //            var response = await client.SendAsync(requestMessage);
        //            response.EnsureSuccessStatusCode();

        //            _logger.LogInformation($"Request {requestMessage.Method} to {requestMessage.RequestUri} with was successful");

        //            var contentString = await response.Content.ReadAsStringAsync();
        //            return JsonConvert.DeserializeObject<T>(contentString);


        //        }
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        _logger.LogError(ex, $"Request {requestMessage.Method} to {requestMessage.RequestUri} failed");
        //        throw;
        //    }
        //}

        //public async Task<T> PostAsync<T, R>(HttpRequestMessage requestMessage, R content)
        //{

        //    try
        //    {
        //        using (var client = _httpClientFactory.CreateClient())
        //        {
        //            if (content != null)
        //            {
        //                var jsonContent = JsonConvert.SerializeObject(content);
        //                requestMessage.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        //                var response = await client.SendAsync(requestMessage);
        //                response.EnsureSuccessStatusCode();

        //                _logger.LogInformation($"Request {requestMessage.Method} to {requestMessage.RequestUri} with was successful");

        //                var contentString = await response.Content.ReadAsStringAsync();
        //                return JsonConvert.DeserializeObject<T>(contentString);
        //            }
        //            else
        //            {
        //                _logger.LogError("Content cannot be null");
        //                throw new ArgumentNullException("Content cannot be null");
        //            }
        //        }
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        _logger.LogError(ex, $"Request {requestMessage.Method} to {requestMessage.RequestUri} failed");
        //        throw;
        //    }
        //}

    }

    
}