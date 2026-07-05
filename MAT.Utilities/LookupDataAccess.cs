using MAT.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace MAT.Utilities
{
    /// <summary>
    /// Lookups y dropdowns vía SP + DBHelper (NetTiers F9). Reemplaza *Service en Helper.cs.
    /// </summary>
    public static class LookupDataAccess
    {
        public sealed class LookupItem
        {
            public string Value { get; set; }
            public string Text { get; set; }
        }

        public static List<LookupItem> GetPaqueteSelectItems()
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@DateYear", SqlDbType.VarChar, 4, string.Empty),
                DBHelper.MakeParam("@Search", SqlDbType.NVarChar, 100, DBNull.Value),
                DBHelper.MakeParam("@Temporada", SqlDbType.Int, 0, DBNull.Value),
                DBHelper.MakeParam("@Moneda", SqlDbType.Int, 0, DBNull.Value)
            };
            return ReadLookupList("dbo.usp_MAT_Paquete_GetPaquetes", parameters,
                r => r.GetGuid(r.GetOrdinal("PaqueteID")).ToString(),
                r => GetString(r, "Descripcion") ?? string.Empty);
        }

        public static List<LookupItem> GetTransporteSelectItems()
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@TransporteID", SqlDbType.VarChar, 36, string.Empty)
            };
            return ReadLookupList("dbo.usp_MAT_Transporte_GetListTransporte", parameters,
                r => r.GetGuid(r.GetOrdinal("TransporteID")).ToString(),
                r => GetString(r, "NroCoche") ?? string.Empty);
        }

        public static List<LookupItem> GetHotelSelectItems()
        {
            return ReadLookupList("dbo.usp_MAT_Hotel_GetAll", null,
                r => r.GetGuid(r.GetOrdinal("HotelID")).ToString(),
                r => GetString(r, "Nombre") ?? string.Empty);
        }

        public static List<LookupItem> GetProveedorSelectItems()
        {
            return ReadLookupList("dbo.usp_MAT_Proveedor_GetSelectList", null,
                r => r.GetGuid(r.GetOrdinal("ProveedorID")).ToString(),
                r => GetString(r, "RazonSocial") ?? string.Empty);
        }

        public static List<LookupItem> GetViajeSelectItems()
        {
            var list = new List<LookupItem>();
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Viaje_GetSelectList", null))
            {
                while (reader.Read())
                {
                    var viajeId = reader.GetGuid(reader.GetOrdinal("ViajeID"));
                    var descripcion = GetString(reader, "PaqueteDescripcion") ?? "Sin paquete";
                    var fechaSalida = GetNullableDateTime(reader, "FechaSalida");
                    var fecha = fechaSalida.HasValue
                        ? fechaSalida.Value.ToShortDateString()
                        : string.Empty;
                    list.Add(new LookupItem
                    {
                        Value = viajeId.ToString(),
                        Text = string.Format(CultureInfo.CurrentCulture, "{0} - Salida {1}", descripcion, fecha)
                    });
                }
            }
            return list;
        }

        public static List<LookupItem> GetHotelSelectItemsByViajeId(Guid viajeId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, viajeId)
            };
            return ReadLookupList("dbo.usp_MAT_Hotel_GetByViajeID", parameters,
                r => r.GetGuid(r.GetOrdinal("HotelID")).ToString(),
                r => GetString(r, "Nombre") ?? string.Empty);
        }

        public static string GetTransporteDisplayText(Guid transporteId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@TransporteID", SqlDbType.UniqueIdentifier, 0, transporteId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Transporte_GetById", parameters))
            {
                return reader.Read() ? (GetString(reader, "NroCoche") ?? "Sin Definir") : "Sin Definir";
            }
        }

        public static string GetPaqueteDisplayText(Guid paqueteId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@PaqueteID", SqlDbType.UniqueIdentifier, 0, paqueteId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Paquete_GetById", parameters))
            {
                return reader.Read() ? (GetString(reader, "Descripcion") ?? "Sin Definir") : "Sin Definir";
            }
        }

        public static string GetHotelDisplayText(Guid hotelId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@HotelID", SqlDbType.UniqueIdentifier, 0, hotelId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Hotel_GetById", parameters))
            {
                return reader.Read() ? (GetString(reader, "Nombre") ?? "Sin Definir") : "Sin Definir";
            }
        }

        public static string GetVendedorDisplayName(Guid personaId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@PersonaID", SqlDbType.UniqueIdentifier, 0, personaId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_PersonaVendedor_GetEntities", parameters))
            {
                if (!reader.Read())
                {
                    return "Sin asignar";
                }

                var nombre = GetString(reader, "Nombre") ?? string.Empty;
                var apellido = GetString(reader, "Apellido") ?? string.Empty;
                var full = (nombre + " " + apellido).Trim();
                return string.IsNullOrEmpty(full) ? "Sin asignar" : full;
            }
        }

        public static void GenerarVoucher(ref Pasaje pasaje)
        {
            var voucherId = Guid.NewGuid();
            var voucherParams = new[]
            {
                DBHelper.MakeParam("@VoucherID", SqlDbType.UniqueIdentifier, 0, voucherId),
                DBHelper.MakeParam("@FechaEmision", SqlDbType.DateTime, 0, DateTime.Now),
                DBHelper.MakeParam("@VendedorID", SqlDbType.UniqueIdentifier, 0, DBNull.Value)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Voucher_Insert", voucherParams);

            pasaje.VoucherId = voucherId;

            var pasajeParams = new[]
            {
                DBHelper.MakeParam("@PasajeID", SqlDbType.UniqueIdentifier, 0, pasaje.PasajeId),
                DBHelper.MakeParam("@PasajeroID", SqlDbType.UniqueIdentifier, 0, (object)pasaje.PasajeroId ?? DBNull.Value),
                DBHelper.MakeParam("@ButacaID", SqlDbType.UniqueIdentifier, 0, (object)pasaje.ButacaId ?? DBNull.Value),
                DBHelper.MakeParam("@FechaReserva", SqlDbType.Date, 0, (object)pasaje.FechaReserva ?? DBNull.Value),
                DBHelper.MakeParam("@FechaCompra", SqlDbType.Date, 0, (object)pasaje.FechaCompra ?? DBNull.Value),
                DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, (object)pasaje.ViajeId ?? DBNull.Value),
                DBHelper.MakeParam("@FacturaID", SqlDbType.UniqueIdentifier, 0, (object)pasaje.FacturaId ?? DBNull.Value),
                DBHelper.MakeParam("@EstadoPasaje", SqlDbType.Int, 0, pasaje.EstadoPasaje),
                DBHelper.MakeParam("@VoucherID", SqlDbType.UniqueIdentifier, 0, voucherId),
                DBHelper.MakeParam("@PrecioID", SqlDbType.UniqueIdentifier, 0, (object)pasaje.PrecioId ?? DBNull.Value)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Pasaje_UpdateEntity", pasajeParams);
        }

        public static int CountReservasByHabitacionAndViaje(Guid habitacionId, Guid viajeId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@HabitacionID", SqlDbType.UniqueIdentifier, 0, habitacionId),
                DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, viajeId)
            };
            var result = DBHelper.ExecuteScalar("dbo.usp_MAT_ReservaHabitacion_CountByHabitacionAndViaje", parameters);
            if (result == null || result == DBNull.Value)
            {
                return 0;
            }
            return Convert.ToInt32(result);
        }

        public static int GetHabitacionCapacidad(Guid habitacionId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@HabitacionID", SqlDbType.UniqueIdentifier, 0, habitacionId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Habitacion_GetById", parameters))
            {
                if (!reader.Read())
                {
                    return 0;
                }

                var ordinal = reader.GetOrdinal("Capacidad");
                if (reader.IsDBNull(ordinal))
                {
                    return 0;
                }

                return Convert.ToInt32(reader.GetValue(ordinal));
            }
        }

        public static string GetDisponibilidad(Guid habitacionId, Guid viajeId)
        {
            var capacidad = GetHabitacionCapacidad(habitacionId);
            var reservas = CountReservasByHabitacionAndViaje(habitacionId, viajeId);
            return (capacidad - reservas).ToString(CultureInfo.InvariantCulture);
        }

        public static bool IsHabitacionDisponible(Guid habitacionId, Guid viajeId)
        {
            var capacidad = GetHabitacionCapacidad(habitacionId);
            var reservas = CountReservasByHabitacionAndViaje(habitacionId, viajeId);
            return capacidad - reservas > 0;
        }

        public static string GetServicioDescripcion(Guid servicioId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@ServicioID", SqlDbType.UniqueIdentifier, 0, servicioId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Servicio_GetById", parameters))
            {
                return reader.Read() ? (GetString(reader, "Descripcion") ?? string.Empty) : string.Empty;
            }
        }

        public static double GetPrecioServicio(Guid servicioId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@ServicioID", SqlDbType.UniqueIdentifier, 0, servicioId)
            };
            var result = DBHelper.ExecuteScalar("dbo.usp_MAT_PrecioServicio_GetActiveByServicioId", parameters);
            if (result == null || result == DBNull.Value)
            {
                return 0;
            }
            return Convert.ToDouble(result);
        }

        public static string GetViajeDescripcion(Guid viajeId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, viajeId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Viaje_GetEntityById", parameters))
            {
                if (!reader.Read())
                {
                    return string.Empty;
                }

                var paqueteId = GetNullableGuid(reader, "PaqueteID");
                var fechaSalida = GetNullableDateTime(reader, "FechaSalida");
                var descripcion = paqueteId.HasValue
                    ? GetPaqueteDisplayText(paqueteId.Value)
                    : "Sin paquete";
                var fecha = fechaSalida.HasValue
                    ? fechaSalida.Value.ToShortDateString()
                    : string.Empty;
                return string.Format(CultureInfo.CurrentCulture, "{0} - Salida {1}", descripcion, fecha);
            }
        }

        private static List<LookupItem> ReadLookupList(
            string spName,
            SqlParameter[] parameters,
            Func<SqlDataReader, string> valueSelector,
            Func<SqlDataReader, string> textSelector)
        {
            var list = new List<LookupItem>();
            using (var reader = DBHelper.ExecuteDataReader(spName, parameters))
            {
                while (reader.Read())
                {
                    list.Add(new LookupItem
                    {
                        Value = valueSelector(reader),
                        Text = textSelector(reader)
                    });
                }
            }
            return list;
        }

        private static string GetString(SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
        }

        private static Guid? GetNullableGuid(SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            return reader.IsDBNull(ordinal) ? (Guid?)null : reader.GetGuid(ordinal);
        }

        private static DateTime? GetNullableDateTime(SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            return reader.IsDBNull(ordinal) ? (DateTime?)null : reader.GetDateTime(ordinal);
        }
    }
}
