using System.Net.Http;
using System.Threading.Tasks;

namespace MAT.MVC.Integration
{
    public interface IApiClient
    {
        Task<T> GetAsync<T>(string uri);
        Task<T> PostAsync<T>(string uri, object data);
        Task<T> PutAsync<T>(string uri, object data);
        Task<bool> DeleteAsync(string uri);

        //Task<T> PostAsync<T, R>(HttpRequestMessage requestMessage, R content);

        //Task<T> GetAsync<T>(HttpRequestMessage requestMessage, int id = 0, string name = null);
    }

    
}