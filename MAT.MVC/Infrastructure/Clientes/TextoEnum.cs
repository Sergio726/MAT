using System;
using MAT.Utilities;

namespace MAT.MVC.Infrastructure.Clientes
{
    /// <summary>
    /// Texto legible (Description) de un valor guardado como entero. Null o fuera del enum → null,
    /// para que la vista muestre "—" en vez de romperse (pasó con clientes viejos sin Sexo o Condición IVA).
    /// </summary>
    public static class TextoEnum
    {
        public static string De<TEnum>(int? valor) where TEnum : struct
        {
            if (!valor.HasValue || !Enum.IsDefined(typeof(TEnum), valor.Value)) return null;
            return ((Enum)Enum.ToObject(typeof(TEnum), valor.Value)).GetDescription();
        }
    }
}
