using MAT.MVC.Integration.BackendApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MAT.MVC.Integration.BackendApi
{
    public class ResponseReservaDto: ErrorDto
    {   
        public string ReservaId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ExpirationOn { get; set; }
        public int ExpirationOnMinutes { get; set; }
        public ResponseReservaFacturaDto Factura { get; set; }


    }

    [DataContract]
    public class ResponseReservaFacturaDto
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public int Estado { get; set; }

        [DataMember]
        public string EstadoDescripcion { get; set; }

        [DataMember]
        public decimal Total { get; set; }

        [DataMember]
        public decimal MontoPagado { get; set; }

        [DataMember]
        public decimal Saldo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime? UltimaFechaPago { get; set; }
    }

}