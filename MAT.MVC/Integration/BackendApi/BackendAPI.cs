using MAT.MVC.Integration.BackendApi.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace MAT.MVC.Integration
{
    public interface IBackendAPI
    {
        //Task<ShopwareGetAllSKUsResponse> GetSKUAsync(string sku);
        Task<ImageResponse> GetAllImageAsync();
    }
    public class BackendAPI: BaseHttpClient, IBackendAPI
    {
        public BackendAPI(IHttpClientFactory httpClientFactory, ILogger<BackendAPI> looger) : base(httpClientFactory, looger)
        {
            
        }

        public async Task<ImageResponse> GetAllImageAsync()
        {
            using(HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, "api/image"))
            {
                return await GetAsync<ImageResponse>(requestMessage);
            }
            
            
        }
    }
}