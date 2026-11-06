using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Caching;
using MAT.MVC.Models.Notificaciones;
using MAT.Utilities;

namespace MAT.MVC.Infrastructure.Data
{
    /// <summary>Avisos de la campanita (<c>usp_MAT_Notificacion_*</c>).</summary>
    public static class NotificacionDataAccess
    {
        private const string ClaveGeneracion = "MAT.Notificaciones.UltimaGeneracion";
        private static readonly object Candado = new object();

        /// <summary>
        /// Crea los avisos nuevos como mucho cada 5 minutos (la campanita se consulta en cada página).
        /// El SP no repite avisos, así que una corrida de más no hace daño.
        /// </summary>
        public static void GenerarSiCorresponde()
        {
            if (HttpRuntime.Cache[ClaveGeneracion] != null) return;
            lock (Candado)
            {
                if (HttpRuntime.Cache[ClaveGeneracion] != null) return;
                DBHelper.ExecuteNonQuery("dbo.usp_MAT_Notificacion_Generar", null);
                HttpRuntime.Cache.Insert(ClaveGeneracion, DateTime.UtcNow, null, DateTime.UtcNow.AddMinutes(5), Cache.NoSlidingExpiration);
            }
        }

        public static IList<NotificacionDto> ParaUsuario(string usuario, int cantidad, out int noLeidas)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@Usuario", SqlDbType.VarChar, 100, usuario),
                DBHelper.MakeParam("@Cantidad", SqlDbType.Int, 0, cantidad)
            };
            noLeidas = 0;
            var lista = new List<NotificacionDto>();
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Notificacion_GetParaUsuario", parameters))
            {
                if (reader.Read()) noLeidas = reader.GetInt("NoLeidas");
                reader.NextResult();
                while (reader.Read())
                {
                    lista.Add(new NotificacionDto
                    {
                        Id = reader.GetInt("ID"),
                        Tipo = reader.GetString("Tipo"),
                        Titulo = reader.GetString("Titulo"),
                        Detalle = reader.GetString("Detalle"),
                        Url = reader.GetString("Url"),
                        Fecha = reader.GetNullableDateTime("Fecha").GetValueOrDefault(),
                        Leida = reader.GetBool("Leida")
                    });
                }
            }
            return lista;
        }

        /// <param name="ids">Null o vacío = todas las vigentes.</param>
        public static void MarcarLeidas(string usuario, IEnumerable<int> ids)
        {
            var lista = ids == null ? null : string.Join(",", ids.Where(i => i > 0).Distinct());
            var parameters = new[]
            {
                DBHelper.MakeParam("@Usuario", SqlDbType.VarChar, 100, usuario),
                DBHelper.MakeParam("@IDs", SqlDbType.VarChar, -1, string.IsNullOrEmpty(lista) ? (object)DBNull.Value : lista)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Notificacion_MarcarLeidas", parameters);
        }
    }
}
