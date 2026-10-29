using System.ComponentModel;

namespace MAT.Enums
{
    /// <summary>
    /// Resultado de un contacto con el cliente (tabla ClienteContacto, CK 1..8).
    /// </summary>
    public enum eResultadoContacto
    {
        [Description("Saludo de cumpleaños")]
        SaludoCumpleanios = 1,
        [Description("Ofrecido viaje")]
        OfrecidoViaje = 2,
        [Description("Interesado")]
        Interesado = 3,
        [Description("Compró")]
        Compro = 4,
        [Description("No le interesa")]
        NoLeInteresa = 5,
        [Description("No contesta")]
        NoContesta = 6,
        [Description("Número equivocado")]
        NumeroEquivocado = 7,
        [Description("No volver a contactar")]
        NoVolverAContactar = 8
    }
}
