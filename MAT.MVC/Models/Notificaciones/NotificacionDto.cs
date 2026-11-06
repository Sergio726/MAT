using System;

namespace MAT.MVC.Models.Notificaciones
{
    /// <summary>Un aviso de la campanita (<c>dbo.Notificacion</c>) con la marca de leído del usuario actual.</summary>
    public sealed class NotificacionDto
    {
        public int Id { get; set; }
        /// <summary>ViajeProximo | UltimosLugares | ViajeCompleto | Cumpleanios</summary>
        public string Tipo { get; set; }
        public string Titulo { get; set; }
        public string Detalle { get; set; }
        public string Url { get; set; }
        public DateTime Fecha { get; set; }
        public bool Leida { get; set; }
    }
}
