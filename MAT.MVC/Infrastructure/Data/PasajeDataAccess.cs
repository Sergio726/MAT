using MAT.Utilities;
using System;
using System.Data;

namespace MAT.MVC.Infrastructure.Data
{
    /// <summary>
    /// Acceso a datos de Pasaje vía SP + DBHelper (NetTiers F4; se extiende en F6).
    /// </summary>
    public static class PasajeDataAccess
    {
        /// <summary>
        /// Genera un pasaje por cada butaca del transporte para el viaje, en una
        /// transacción del SP (set-based). Devuelve la cantidad de pasajes generados.
        /// </summary>
        public static int GenerarPasajesByViaje(Guid viajeId, Guid transporteId, int estadoPasaje)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, viajeId),
                DBHelper.MakeParam("@TransporteID", SqlDbType.UniqueIdentifier, 0, transporteId),
                DBHelper.MakeParam("@EstadoPasaje", SqlDbType.Int, 0, estadoPasaje)
            };
            return DBHelper.ExecuteNonQueryOutput("dbo.usp_MAT_Pasaje_GenerarByViaje", parameters, "@Generados", SqlDbType.Int, 0);
        }
    }
}
