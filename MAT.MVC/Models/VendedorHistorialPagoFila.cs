using System;
using MAT.Entities;

namespace MAT.MVC.Models
{
    /// <summary>Fila del historial de pagos por vendedor: pago + factura asociada (movimiento cuenta).</summary>
    public class VendedorHistorialPagoFila
    {
        public Pago Pago { get; set; }
        public Guid? FacturaId { get; set; }
    }
}
