using Newtonsoft.Json;

namespace MAT.Enums.SharedModels
{
    public class DatosPago
    {
        [JsonProperty("Condition")]
        public string Condition { get; set; }

        [JsonProperty("Monto")]
        public int Monto { get; set; }

        [JsonProperty("MontoFactura")]
        public int MontoFactura { get; set; }

        [JsonProperty("TipoPago")]
        public int TipoPago { get; set; }

        [JsonProperty("Descuento")]
        public int Descuento { get; set; }

        [JsonProperty("Detalledescuento")]
        public string Detalledescuento { get; set; }

        [JsonProperty("Recibo")]
        public string Recibo { get; set; }

        [JsonProperty("TransaccionId")]
        public string TransaccionId { get; set; }

        [JsonProperty("NroFactura")]
        public string NroFactura { get; set; }

        [JsonProperty("Observaciones")]
        public string Observaciones { get; set; }

        [JsonProperty("MontoRecibidoMonedaTipo")]
        public string MontoRecibidoMonedaTipo { get; set; }

        [JsonProperty("MontoEquivalente")]
        public int? MontoEquivalente { get; set; }

        [JsonProperty("MontoEquivalenteMonedaTipo")]
        public string MontoEquivalenteMonedaTipo { get; set; }

        [JsonProperty("MontoEquivalenteCotizacion")]
        public string MontoEquivalenteCotizacion { get; set; }

        [JsonProperty("ViajeMonedaTipo")]
        public string ViajeMonedaTipo { get; set; }

        [JsonProperty("MontoRecibido")]
        public int? MontoRecibido { get; set; }
    }
}
