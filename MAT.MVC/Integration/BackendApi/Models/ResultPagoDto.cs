using MAT.MVC.Integration.BackendApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAT.MVC.Integration.BackendApi
{
    public class ResultPagoDto: ErrorDto
    {
        public string Result { get; set; }
        public string FacturaId { get; set; }
        public string FacturaEstadoId { get; set; }
        public string FacturaEstado { get; set; }

        public PaymentDto Payment { get; set; }
    }

    public class PaymentDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("preferenceId")]
        public string PreferenceId { get; set; }

        [JsonProperty("init_point")]
        public string InitPoint { get; set; }

        [JsonProperty("cliente_id")]
        public string ClienteId { get; set; }

        [JsonProperty("collector_id")]
        public int CollectorId { get; set; }

        [JsonProperty("date_created")]
        public string DateCreated { get; set; }

        [JsonProperty("date_of_expiration")]
        public string DateOfExpiration { get; set; }

        [JsonProperty("expiration_date_from")]
        public string ExpirationDateFrom { get; set; }

        [JsonProperty("expiration_date_to")]
        public string ExpirationDateTo { get; set; }

        [JsonProperty("items")]
        public List<ItemPayment> Items { get; set; }

        [JsonProperty("facturaId")]
        public string FacturaId { get; set; }

        [JsonProperty("paymentId")]
        public string PaymentId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("status_detail")]
        public string StatusDetail { get; set; }

        [JsonProperty("date_approved")]
        public string DateApproved { get; set; }

        [JsonProperty("date_last_updated")]
        public string DateLastUpdated { get; set; }
    }

    public class ItemPayment
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("unit_price")]
        public decimal UnitPrice { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }


}