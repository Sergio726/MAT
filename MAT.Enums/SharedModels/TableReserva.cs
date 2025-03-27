using System;
using System.Collections.Generic;

namespace MAT.Enums.SharedModels
{
    public class TablePasaje
    {
        public Guid PasajeId { get; set; }
        public Guid PasajeroId { get; set; }
        public Guid ButacaId { get; set; }
        public string ButacaCodigo { get; set; }
        public decimal ButacaPrecio { get; set; }
        public List<Guid> AdicionalesIds { get; set; }
        public Guid HabitacionId { get; set; }
    } 
}
