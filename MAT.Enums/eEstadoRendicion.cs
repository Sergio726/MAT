using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAT.Enums
{
    public enum eEstadoRendicion
    {
        [Description("Pendiente")]
        Pendiente=0,
        [Description("Rendido")]
        Rendido=1
    }
}
