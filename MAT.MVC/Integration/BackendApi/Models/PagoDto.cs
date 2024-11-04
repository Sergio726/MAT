using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAT.MVC.Integration.BackendApi
{
    public class PagoDto
    {
        [JsonProperty("viajeId")]
        public Guid ViajeId { get; set; }

        [JsonProperty("vendedorId")]
        public Guid VendedorId { get; set; }

        [JsonProperty("clienteId")]
        public Guid ClienteId { get; set; }

        [JsonProperty("observaciones")]
        public string Observaciones { get; set; }

        [JsonProperty("condicion")]
        public string Condicion { get; set; }

        [JsonProperty("monedaTipo")]
        public string MonedaTipo { get; set; }

        [JsonProperty("descuentoDetalle")]
        public string DescuentoDetalle { get; set; }

        [JsonProperty("descuentoMonto")]
        public decimal DescuentoMonto { get; set; }

        [JsonProperty("monto")]
        public decimal Monto { get; set; }

        [JsonProperty("nroRecibo")]
        public string NroRecibo { get; set; }

        [JsonProperty("transaccionId")]
        public string TransaccionId { get; set; }

        [JsonProperty("tipoPago")]
        public int TipoPago { get; set; }

        [JsonProperty("nroFactura")]
        public string NroFactura { get; set; }

        [JsonProperty("montoRecibidoMonedaTipo")]
        public string MontoRecibidoMonedaTipo { get; set; }

        [JsonProperty("montoEquivalente")]
        public decimal? MontoEquivalente { get; set; }

        [JsonProperty("montoEquivalenteMonedaTipo")]
        public string MontoEquivalenteMonedaTipo { get; set; }

        [JsonProperty("montoEquivalenteCotizacion")]
        public decimal MontoEquivalenteCotizacion { get; set; }

        [JsonProperty("pasajes")]
        public List<PasajeDto> Pasajes { get; set; }
    }

    public class PasajeDto
    {
        [JsonProperty("pasajeId")]
        public Guid PasajeId { get; set; }

        [JsonProperty("pasajeroId")]
        public Guid PasajeroId { get; set; }

        [JsonProperty("butacaId")]
        public Guid ButacaId { get; set; }

        [JsonProperty("butacaCodigo")]
        public string ButacaCodigo { get; set; }

        [JsonProperty("butacaPrecio")]
        public decimal ButacaPrecio { get; set; }

        [JsonProperty("adicionalesIds")]
        public List<Guid> AdicionalesIds { get; set; }

        [JsonProperty("habitacionId")]
        public Guid HabitacionId { get; set; }
    }
}