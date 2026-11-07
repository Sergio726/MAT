using System;
using System.Collections.Generic;

namespace MAT.MVC.Models.Home
{
    /// <summary>
    /// Respuesta del buscador global (bloque G del plan del panel principal): grupos por tipo de dato.
    /// El navegador los muestra debajo de Módulos y Acciones, con el mismo formato.
    /// </summary>
    public sealed class BuscadorGrupoDto
    {
        /// <summary>"cliente", "factura" o "viaje".</summary>
        public string Tipo { get; set; }
        public string Titulo { get; set; }
        public IList<BuscadorItemDto> Items { get; set; }

        public BuscadorGrupoDto()
        {
            Items = new List<BuscadorItemDto>();
        }
    }

    public sealed class BuscadorItemDto
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Url { get; set; }
        public string Icono { get; set; }
        /// <summary>true = enlace "Ver todos" al final del grupo.</summary>
        public bool VerTodos { get; set; }
    }

    public sealed class BuscadorViajeDto
    {
        public Guid ViajeId { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaSalida { get; set; }
    }
}
