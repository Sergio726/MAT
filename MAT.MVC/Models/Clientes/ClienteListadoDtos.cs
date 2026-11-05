using System;
using System.Collections.Generic;

namespace MAT.MVC.Models.Clientes
{
    /// <summary>Fila del listado de clientes (<c>usp_MAT_PersonaCliente_Buscar</c>) más lo que arma el servidor.</summary>
    public sealed class ClienteListadoItemDto
    {
        public Guid PersonaId { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string NroDocumento { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string LocalidadNombre { get; set; }
        public int TipoId { get; set; }
        public string TipoNombre { get; set; }
        public DateTime? FechaAlta { get; set; }
        public string Categoria { get; set; }
        public string CategoriaNombre { get; set; }
        public bool EsMenor { get; set; }
        public bool EsTitularFacturas { get; set; }
        /// <summary>false = dado de baja (solo aparece con "Mostrar dados de baja").</summary>
        public bool Activo { get; set; }
        public bool TieneWhatsApp { get; set; }
        public string WhatsAppUrl { get; set; }
    }

    /// <summary>Una página de resultados con el total (para "Mostrando 1–25 de 342").</summary>
    public sealed class PaginaDto<T>
    {
        public int Total { get; set; }
        public int Pagina { get; set; }
        public int Tamanio { get; set; }
        public IList<T> Items { get; set; }
    }

    /// <summary>Fila de "Altas por período" (<c>usp_MAT_Reportes_ClientesNuevosPagina</c>).</summary>
    public sealed class ClienteAltaDto
    {
        public Guid PersonaId { get; set; }
        public string ClienteNombre { get; set; }
        public string NroDocumento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public DateTime? FechaAlta { get; set; }
        public DateTime? PrimeraFactura { get; set; }
        public string VendedorNombre { get; set; }
    }
}
