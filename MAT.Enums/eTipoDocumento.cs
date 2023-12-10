using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAT.Enums
{
    public enum eTipoDocumento
    {
        [Description("DNI")]
        DNI = 1,
        [Description("Pasaporte")]
        PASAPORTE = 2,
        [Description("Cédula")]
        CEDULA=3,
        [Description("Libreta Civil")]
        LIBRETA=4

    }

}
