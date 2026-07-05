using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MAT.MVC.Infrastructure
{
    /// <summary>
    /// Serializa respuestas JSON con propiedades en camelCase (Newtonsoft), para endpoints
    /// consumidos por grillas que esperan ese formato (mismo contrato que <c>ReportesController</c>).
    /// </summary>
    public static class JsonCamelCaseHelper
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Include
        };

        public static ContentResult Serialize(object payload)
        {
            var json = JsonConvert.SerializeObject(payload, Settings);
            return new ContentResult
            {
                Content = json,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8
            };
        }
    }
}
