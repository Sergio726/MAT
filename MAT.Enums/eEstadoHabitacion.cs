using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAT.Enums
{
    public enum eEstadoHabitacion
    {
        [Description("LIBRE")]
        Libre = 0,
        [Description("OCUPADO")]
        Ocupado = 1,
    }
}
