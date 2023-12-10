using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAT.Enums
{
    public enum eEstadoFactura
    {
        [Description("Señado")]
        Señado = 3,
        [Description("Pagado")]
        Pagado = 4,
        [Description("Pre-reserva")]
        Prereserva=5,
        [Description("Nota de Crédito")]
        NotadeCredito=6,
        [Description("Anulado")]
        Anulado = 7
    }
}
