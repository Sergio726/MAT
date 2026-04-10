namespace MAT.MVC.Models.Reportes
{
    /// <summary>
    /// Ítem de autocompletar de viajes para reportes Admin (<c>usp_MAT_Reportes_BuscarViajes</c>).
    /// </summary>
    public sealed class ReporteViajeLookupDto
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }
        /// <summary>Fecha de salida del viaje (solo fecha, ISO <c>yyyy-MM-dd</c> en JSON).</summary>
        public string FechaSalida { get; set; }
    }
}
