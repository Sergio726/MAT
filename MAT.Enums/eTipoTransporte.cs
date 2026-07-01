using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MAT.Enums
{
    /// <summary>
    /// Tipos de unidad terrestre (Transporte.Tipo). Description = valor persistido en BD.
    /// </summary>
    public enum eTipoTransporte
    {
        [Description("MINIBUS")]
        [Display(Name = "Minibus")]
        Minibus = 1,

        [Description("CAMION 4X4")]
        [Display(Name = "Camión 4x4")]
        Camion4x4 = 2,

        [Description("PISOELEVADO")]
        [Display(Name = "Piso Elevado")]
        PisoElevado = 3,

        [Description("DOBLEPISO")]
        [Display(Name = "Doble Piso")]
        DoblePiso = 4,
    }
}
