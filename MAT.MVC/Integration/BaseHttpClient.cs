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

    public class BaseHttpClient: IBaseHttpClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<BaseHttpClient> _logger;

        public BaseHttpClient(IHttpClientFactory httpClientFactory, ILogger<BaseHttpClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<T> GetAsync<T>(HttpRequestMessage requestMessage, int id = 0, string name = null)
        {
            try
            {
                using (var client = _httpClientFactory.CreateClient())
                {
                    var response = await client.SendAsync(requestMessage);
                    response.EnsureSuccessStatusCode();

                    _logger.LogInformation($"Request {requestMessage.Method} to {requestMessage.RequestUri} with was successful");

                    var contentString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(contentString);

                    
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Request {requestMessage.Method} to {requestMessage.RequestUri} failed");
                throw;
            }
        }

        public async Task<T> PostAsync<T, R>(HttpRequestMessage requestMessage, R content)
        {

            try
            {
                using (var client = _httpClientFactory.CreateClient())
                {
                    if (content != null)
                    {
                        var jsonContent = JsonConvert.SerializeObject(content);
                        requestMessage.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                        var response = await client.SendAsync(requestMessage);
                        response.EnsureSuccessStatusCode();

                        _logger.LogInformation($"Request {requestMessage.Method} to {requestMessage.RequestUri} with was successful");

                        var contentString = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(contentString);
                    }
                    else
                    {
                        _logger.LogError("Content cannot be null");
                        throw new ArgumentNullException("Content cannot be null");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Request {requestMessage.Method} to {requestMessage.RequestUri} failed");
                throw;
            }
        }
                
    }

    
}