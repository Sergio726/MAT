using System;
using System.Collections.Generic;

namespace MAT.MVC.Models.Clientes
{
    /// <summary>Catálogos del formulario de cliente (<c>usp_MAT_Catalogo_Geografia</c>, módulo Clientes F4).</summary>
    public sealed class ClienteCatalogos
    {
        public IList<PaisCatalogo> Paises { get; set; }
        public IList<ProvinciaCatalogo> Provincias { get; set; }
        public IList<string> Ocupaciones { get; set; }

        public ClienteCatalogos()
        {
            Paises = new List<PaisCatalogo>();
            Provincias = new List<ProvinciaCatalogo>();
            Ocupaciones = new List<string>();
        }
    }

    public sealed class PaisCatalogo
    {
        public Guid PaisId { get; set; }
        /// <summary>ISO alfa-3: lo que se guarda en Nacionalidad y PaisResidencia.</summary>
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public bool TieneProvincias { get; set; }
    }

    public sealed class ProvinciaCatalogo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Guid PaisId { get; set; }
    }
}
