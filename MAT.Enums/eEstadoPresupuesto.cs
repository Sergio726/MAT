using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAT.Enums
{
    public enum eEstadoPresupuesto
    {
        [Description("Pendiente")]
        Pendiente = 1,
        [Description("Expirado")]
        Expirado = 2,
        [Description("Cerrado")]
        Cerrado = 3
    }
}

