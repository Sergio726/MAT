using Newtonsoft.Json;

namespace MAT.Enums.SharedModels
{
    public class Habitacion
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Capacidad")]
        public int Capacidad { get; set; }

        [JsonProperty("NroHabitacion")]
        public string NroHabitacion { get; set; }

        [JsonProperty("Precio")]
        public int Precio { get; set; }

        [JsonProperty("Descripcion")]
        public string Descripcion { get; set; }
    }
}
