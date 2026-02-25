using System.ComponentModel;
namespace MAT.Enums
{
    public enum eTipoComprobante
    {
        [Description("Factura A")]
        FacturaA = 1,
        [Description("Factura B")]
        FacturaB = 2,
        [Description("Factura C")]
        FacturaC = 3,
        [Description("Nota de Cr\u00e9dito A")]
        NotaCreditoA = 4,
        [Description("Nota de Cr\u00e9dito B")]
        NotaCreditoB = 5,
        [Description("Nota de Cr\u00e9dito C")]
        NotaCreditoC = 6
    }
}
