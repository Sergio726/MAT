using System;

namespace MAT.MVC.Models.Home
{
    /// <summary>Un viaje del panel "Próximas salidas" del inicio (<c>usp_MAT_Dashboard_ProximasSalidas</c>).</summary>
    public sealed class ProximaSalidaDto
    {
        public const int UmbralUltimosLugares = 5;

        public Guid ViajeId { get; set; }
        public string Descripcion { get; set; }
        /// <summary>Distingue viajes con el mismo nombre (por ejemplo, con noche y sin noche).</summary>
        public string Paquete { get; set; }
        public DateTime FechaSalida { get; set; }
        public string HoraSalida { get; set; }
        public int DiasFaltan { get; set; }
        public int Total { get; set; }
        public int Libres { get; set; }
        public int Vendidos { get; set; }
        public int PreReservas { get; set; }

        /// <summary>completo | ultimos | disponible | sinPasajes</summary>
        public string Estado
        {
            get
            {
                if (Total == 0) return "sinPasajes";
                if (Libres == 0) return "completo";
                return Libres <= UmbralUltimosLugares ? "ultimos" : "disponible";
            }
        }
    }
}
