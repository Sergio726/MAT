using MAT.Entities;

namespace MAT.MVC.Models
{
    /// <summary>
    /// Ítem de autocompletar de localidades (<c>/Localidad/Search</c>).
    /// </summary>
    public sealed class LocalidadLookupDto
    {
        public int LocalidadId { get; set; }
        public string Nombre { get; set; }

        public static LocalidadLookupDto From(VLocalidad item)
        {
            return new LocalidadLookupDto
            {
                LocalidadId = item.Id,
                Nombre = item.Nombre
            };
        }
    }
}
