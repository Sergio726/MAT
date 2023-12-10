using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAT.Enums
{
    public enum eTipoButaca
    {
        [Description("Cama")]
        Cama = 1,
        [Description("Semi Cama")]
        SemiCama = 2,
        [Description("Ejecutivo")]
        Ejecutivo = 3,
        [Description("Suite Preferencial")]
        Suite = 4
    }
}
