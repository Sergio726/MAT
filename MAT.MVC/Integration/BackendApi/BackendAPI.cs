using MAT.MVC.Integration.BackendApi.Models;
using MAT.MVC.Models;
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
        Task<List<ImageResponse>> GetAllImageAsync();
    }
    public class BackendAPI: IBackendAPI
    {
        
        private readonly IApiClient _apiClient;
        public BackendAPI() 
        {
            string backendApiURL = System.Configuration.ConfigurationManager.AppSettings["BackendAPI_URL"].ToString();
            HttpClient httpClient = HttpClientFactory.Create();
            httpClient.BaseAddress = new Uri(backendApiURL);
            
            _apiClient = new ApiClient(httpClient);
        }

        public async Task<List<ImageResponse>> GetAllImageAsync()
        {
            return await _apiClient.GetAsync<List<ImageResponse>>("api/image");
        }

        public async Task<List<ResultPasajeDto>> GetListOfPasajesByViajeID(string viajeId)
        {
            string queryParam = $"viajeId={viajeId}";
            string url = "api/pasajes/getPasajesByViajeID?" + queryParam;
            return await _apiClient.GetAsync<List<ResultPasajeDto>>(url);
        }
    }
}