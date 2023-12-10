using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAT.Enums
{
    public enum eTabla
    {
        [Description("Pago")]
        Pago=1,
        [Description("Factura")]
        Factura = 2,
        [Description("Nota de Credito")]
        Nota = 3
    }
}
