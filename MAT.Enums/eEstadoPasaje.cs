using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAT.Enums
{
    public enum eEstadoPasaje
	{
        [Description("Disponible")]
	    Disponible = 1,
        [Description("Reservado")]
        Reservado = 2,
        [Description("Señado")]
        Señado = 3,
        [Description("Pagado")]
        Pagado = 4,
        [Description("Pre-reserva")]
        Prereserva = 5,
        [Description("Reserva-Hotel")]
        ReservaHotel= 6,
        [Description("Anulado")]
        Anulado = 7,
        [Description("Pasaje y Hotel prereservados")]
        PasajeHotelPrereserva = 8
	}
}
