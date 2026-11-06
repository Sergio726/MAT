using System.Collections.Generic;
using System.Data;
using MAT.MVC.Models.Home;
using MAT.Utilities;

namespace MAT.MVC.Infrastructure.Data
{
    /// <summary>Datos del inicio (panel principal).</summary>
    public static class DashboardDataAccess
    {
        /// <summary>Viajes que salen entre hoy (hora argentina) y <paramref name="dias"/> días, ordenados por salida.</summary>
        public static List<ProximaSalidaDto> GetProximasSalidas(int dias)
        {
            var parameters = new[] { DBHelper.MakeParam("@Dias", SqlDbType.Int, 0, dias) };
            var lista = new List<ProximaSalidaDto>();
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Dashboard_ProximasSalidas", parameters))
            {
                while (reader.Read())
                {
                    lista.Add(new ProximaSalidaDto
                    {
                        ViajeId = reader.GetGuid("ViajeID"),
                        Descripcion = reader.GetString("Descripcion"),
                        Paquete = reader.GetString("Paquete"),
                        FechaSalida = reader.GetNullableDateTime("FechaSalida").GetValueOrDefault(),
                        HoraSalida = reader.GetString("HoraSalida")?.Trim(),
                        DiasFaltan = reader.GetInt("DiasFaltan"),
                        Total = reader.GetInt("Total"),
                        Libres = reader.GetInt("Libres"),
                        Vendidos = reader.GetInt("Vendidos"),
                        PreReservas = reader.GetInt("PreReservas")
                    });
                }
            }
            return lista;
        }
    }
}
