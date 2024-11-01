using Newtonsoft.Json;

namespace MAT.Enums.SharedModels
{
    public class NuevoAdicional
    {
        [JsonProperty("AdicionalId")]
        public string AdicionalId { get; set; }

        [JsonProperty("Descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty("Precio")]
        public int Precio { get; set; }

        [JsonProperty("IsMenor")]
        public bool IsMenor { get; set; }
    }
}
