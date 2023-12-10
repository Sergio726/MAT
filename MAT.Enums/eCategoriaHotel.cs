using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAT.Enums
{
    public enum eCategoriaHotel
    {
        [Description("1 Estrella")]
        Una_Estrella = 1,
        [Description("2 Estrellas")]
        Dos_Estrellas = 2,
        [Description("3 Estrellas")]
        Tres_Estrellas = 3,
        [Description("4 Estrellas")]
        Cuatro_Estrellas = 4,
        [Description("5 Estrellas")]
        Cinco_Estrellas = 5
    }
}

