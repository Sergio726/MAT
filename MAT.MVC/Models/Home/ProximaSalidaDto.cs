using System;

namespace MAT.MVC.Models.Home
{
    /// <summary>Un viaje del panel "Próximas salidas" del inicio (<c>usp_MAT_Dashboard_ProximasSalidas</c>).</summary>
    public sealed class ProximaSalidaDto
    {
        /// <summary>"Últimos lugares" con este porcentaje libre o menos (misma regla que el aviso de la campanita).</summary>
        public const int PorcentajeUltimosLugares = 15;

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

        /// <summary>completo | ultimos | disponible | sinPasajes. La pantalla solo marca los excepcionales.</summary>
        public string Estado
        {
            get
            {
                if (Total == 0) return "sinPasajes";
                if (Libres == 0) return "completo";
                return Libres * 100 <= Total * PorcentajeUltimosLugares ? "ultimos" : "disponible";
            }
        }

        /// <summary>Vendidos + pre-reservas: lo que llena la barra de ocupación.</summary>
        public int Ocupados { get { return Math.Max(0, Total - Libres); } }
    }
}
