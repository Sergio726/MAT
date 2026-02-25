using System.ComponentModel;
namespace MAT.Enums
{
    public enum eAlicuotaIva
    {
        [Description("0%")]
        Cero = 0,
        [Description("2.5%")]
        DosYMedio = 1,
        [Description("5%")]
        Cinco = 2,
        [Description("10.5%")]
        DiezYMedio = 3,
        [Description("21%")]
        Veintiuno = 4,
        [Description("27%")]
        Veintisiete = 5
    }
}
