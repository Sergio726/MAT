using System.Net.Http;
using System.Threading.Tasks;

namespace MAT.MVC.Integration
{
    public interface IBaseHttpClient
    {
        Task<T> PostAsync<T, R>(HttpRequestMessage requestMessage, R content);

        Task<T> GetAsync<T>(HttpRequestMessage requestMessage, int id = 0, string name = null);
    }

    
}