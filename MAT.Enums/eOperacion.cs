using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAT.Enums
{
    public enum eOperacion
    {
        [Description("Alta")]
        Alta= 1,
        [Description("Baja")]
        Baja= 2,
        [Description("Modificación")]
        Modificacion = 2
    }
}
