using System;
using System.Globalization;
using System.Web;
using System.Web.Caching;
using MAT.MVC.Models.Clientes;
using MAT.Utilities;

namespace MAT.MVC.Infrastructure.Data
{
    /// <summary>
    /// Países, provincias y ocupaciones para el formulario de cliente. Cambian muy poco: se guardan 10 minutos en memoria.
    /// </summary>
    public static class ClienteCatalogosDataAccess
    {
        private const string ClaveCache = "MAT.ClienteCatalogos";
        private static readonly CultureInfo Argentina = new CultureInfo("es-AR");
        private static readonly string[] Menores = { "de", "del", "la", "las", "los", "y", "do", "da" };

        public static ClienteCatalogos Get()
        {
            var catalogos = HttpRuntime.Cache.Get(ClaveCache) as ClienteCatalogos;
            if (catalogos != null) return catalogos;

            catalogos = Leer();
            HttpRuntime.Cache.Insert(ClaveCache, catalogos, null, DateTime.UtcNow.AddMinutes(10), Cache.NoSlidingExpiration);
            return catalogos;
        }

        /// <summary>"SANTIAGO DEL ESTERO" → "Santiago del Estero" (la mayoría de los nombres están en mayúsculas en la base).</summary>
        public static string NombreLegible(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return nombre;
            var palabras = Argentina.TextInfo.ToTitleCase(nombre.Trim().ToLower(Argentina)).Split(' ');
            for (var i = 1; i < palabras.Length; i++)
            {
                if (Array.IndexOf(Menores, palabras[i].ToLowerInvariant()) >= 0) palabras[i] = palabras[i].ToLowerInvariant();
            }
            return string.Join(" ", palabras);
        }

        private static ClienteCatalogos Leer()
        {
            var catalogos = new ClienteCatalogos();
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Catalogo_Geografia", null))
            {
                while (reader.Read())
                {
                    catalogos.Paises.Add(new PaisCatalogo
                    {
                        PaisId = reader.GetGuid("PaisID"),
                        Codigo = reader.GetString("Codigo")?.Trim(),
                        Nombre = NombreLegible(reader.GetString("Nombre")),
                        TieneProvincias = reader.GetBool("TieneProvincias")
                    });
                }
                reader.NextResult();
                while (reader.Read())
                {
                    catalogos.Provincias.Add(new ProvinciaCatalogo
                    {
                        Id = reader.GetInt("ID"),
                        Nombre = NombreLegible(reader.GetString("Nombre")),
                        PaisId = reader.GetGuid("IdPais")
                    });
                }
                reader.NextResult();
                while (reader.Read())
                {
                    catalogos.Ocupaciones.Add(reader.GetString("Nombre"));
                }
            }
            return catalogos;
        }
    }
}
