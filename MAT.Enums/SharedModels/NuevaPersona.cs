using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MAT.Enums.SharedModels
{
    public class NuevaPersona
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Nombre")]
        public string Nombre { get; set; }

        [JsonProperty("Apellido")]
        public string Apellido { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("NroDocumento")]
        public string NroDocumento { get; set; }

        
    }

    public class Pasajero: NuevaPersona
    {
        [JsonProperty("Edad")]
        public int Edad { get; set; }

        [JsonProperty("Habitacion")]
        public Habitacion Habitacion { get; set; }

        [JsonProperty("Butaca")]
        public Butaca Butaca { get; set; }

        [JsonProperty("Adicionales")]
        public List<NuevoAdicional> Adicionales { get; set; }

        [JsonProperty("PasajeroAdulto")]
        public PasajeroAdulto PasajeroAdulto { get; set; }
    }

    public class PasajeroAdulto
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("NombreCompleto")]
        public string NombreCompleto { get; set; }
    }
}
