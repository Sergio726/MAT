using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAT.Enums
{
    public enum eCondicionIVA
    {
        [Description("IVA Responsable Inscripto")]
        IVA_Responsable_Inscripto = 1,
        [Description("IVA Responsable No Inscripto")]
        IVA_Responsable_no_Inscripto = 2,
        [Description("IVA No Responsable")]
        IVA_no_Responsable = 3,
        [Description("IVA Sujeto Exento")]
        IVA_Sujeto_Exento = 4,
        [Description("Consumidor Final")]
        Consumidor_Final = 5,
        [Description("Responsable Monotributo")]
        Responsable_Monotributo = 6,
        [Description("Sujeto no Categorizado")]
        Sujeto_no_Categorizado = 7,
        [Description("Proveedor del Exterior")]
        Proveedor_del_Exterior = 8,
        [Description("Cliente del Exterior")]
        Cliente_del_Exterior = 9,
        [Description("IVA Liberado Ley N° 19.640")]
        IVA_Liberado_Ley_Nº_19640 = 10,
        [Description("IVA Responsable Inscripto - Agente de Percepción")]
        IVA_Responsable_Inscripto_Agente_de_Percepción = 11,
        [Description("Pequeño Contribuyente Eventual")]
        Pequeño_Contribuyente_Eventual = 12,
        [Description("Monotributista Social")]
        Monotributista_Social = 13,
        [Description("Pequeño Contribuyente Eventual Social")]
        Pequeño_Contribuyente_Eventual_Social = 14,

    }
}
