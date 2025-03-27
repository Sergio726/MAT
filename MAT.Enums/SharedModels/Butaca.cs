using Newtonsoft.Json;

namespace MAT.Enums.SharedModels
{
    public class Butaca
    {
        [JsonProperty("ButacaId")]
        public string ButacaId { get; set; }

        [JsonProperty("PasajeId")]
        public string PasajeId { get; set; }

        [JsonProperty("Codigo")]
        public string Codigo { get; set; }

        [JsonProperty("Precio")]
        public string Precio { get; set; }
    }
}
