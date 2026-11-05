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
            return new ContentResult
            {
                Content = ToJson(payload),
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8
            };
        }

        /// <summary>
        /// Mismo JSON, para incrustar en la página dentro de <c>&lt;script type="application/json"&gt;</c>
        /// (se escapa <c>&lt;/</c> para que un texto con "&lt;/script&gt;" no corte el bloque).
        /// </summary>
        public static string ToJson(object payload)
        {
            return JsonConvert.SerializeObject(payload, Settings).Replace("</", "<\\/");
        }
    }
}
