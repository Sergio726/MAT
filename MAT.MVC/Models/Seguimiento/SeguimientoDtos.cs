using System;
using System.Collections.Generic;

namespace MAT.MVC.Models.Seguimiento
{
    /// <summary>
    /// Fila de <c>fn_MAT_Seguimiento_Clientes</c> más los campos que arma el servidor (etiquetas y WhatsApp).
    /// </summary>
    public sealed class SeguimientoClienteDto
    {
        public Guid ClienteId { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string NroDocumento { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public bool FechaNacimientoDudosa { get; set; }
        public bool EsMenor { get; set; }
        /// <summary>Cantidad de otros clientes con el mismo celular (familias que comparten número).</summary>
        public int TelefonoCompartido { get; set; }
        /// <summary>Viajes propios o pagados para otros.</summary>
        public int ViajesTotales { get; set; }
        public int ViajesComoPasajero { get; set; }
        public int Viajes12Meses { get; set; }
        public DateTime? PrimerViaje { get; set; }
        public DateTime? UltimoViaje { get; set; }
        public DateTime? ProximoViaje { get; set; }
        public double? GastoArs { get; set; }
        public double? GastoUsd { get; set; }
        public string Categoria { get; set; }
        public string CategoriaNombre { get; set; }
        public string AccionSugerida { get; set; }
        public DateTime? UltimoContactoFecha { get; set; }
        public int? UltimoContactoResultado { get; set; }
        public string UltimoContactoResultadoNombre { get; set; }
        public string UltimoContactoPor { get; set; }
        public bool NoContactar { get; set; }
        public bool TieneWhatsApp { get; set; }
        public string WhatsAppUrl { get; set; }

        // Solo en el listado de cumpleaños
        public DateTime? ProximoCumple { get; set; }
        public int? DiasFaltan { get; set; }
        public int? EdadQueCumple { get; set; }
        public DateTime? UltimoSaludoFecha { get; set; }
        public string UltimoSaludoPor { get; set; }
    }

    public sealed class SeguimientoViajeDto
    {
        public DateTime? FechaSalida { get; set; }
        public DateTime? FechaRegreso { get; set; }
        public string Viaje { get; set; }
        public string EstadoPasaje { get; set; }
        /// <summary>"Viajó" o "Pagó para otros".</summary>
        public string Rol { get; set; }
        public int Pasajes { get; set; }
    }

    public sealed class SeguimientoContactoDto
    {
        public DateTime Fecha { get; set; }
        public int Resultado { get; set; }
        public string ResultadoNombre { get; set; }
        public string Observacion { get; set; }
        public string Quien { get; set; }
    }

    public sealed class SeguimientoFichaDto
    {
        public SeguimientoClienteDto Cliente { get; set; }
        public List<SeguimientoViajeDto> Viajes { get; set; }
        public List<SeguimientoContactoDto> Contactos { get; set; }
    }

    public sealed class SeguimientoCategoriaDto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string AccionSugerida { get; set; }
        public string Icono { get; set; }
        public int Cantidad { get; set; }
    }
}
