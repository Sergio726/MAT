using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAT.Enums
{
    public enum eTipoCliente
    {
        [Description("Mayorista")]
        Mayorista=1,
        [Description("Minorista")]
        Minorista=2,
        [Description("Tercero")]
        Tercero=3
    }
}
