using MAT.Entities;
using MAT.MVC.Models;
using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MAT.MVC.Infrastructure.Data
{
    /// <summary>
    /// Lecturas de maestros operativos vía SP + DBHelper (NetTiers F3).
    /// </summary>
    public static class MaestrosDataAccess
    {
        public static Butaca GetButacaById(Guid butacaId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@ButacaID", SqlDbType.UniqueIdentifier, 0, butacaId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Butaca_GetById", parameters))
            {
                return reader.Read() ? MapButaca(reader) : null;
            }
        }

        public static List<Butaca> GetButacasByTransporteId(Guid transporteId)
        {
            var list = new List<Butaca>();
            var parameters = new[]
            {
                DBHelper.MakeParam("@TransporteID", SqlDbType.UniqueIdentifier, 0, transporteId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Butaca_GetByTransporteId", parameters))
            {
                while (reader.Read())
                {
                    list.Add(MapButaca(reader));
                }
            }
            return list;
        }

        public static Transporte GetTransporteById(Guid transporteId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@TransporteID", SqlDbType.UniqueIdentifier, 0, transporteId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Transporte_GetById", parameters))
            {
                return reader.Read() ? MapTransporte(reader) : null;
            }
        }

        public static TransporteFormViewModel GetTransporteFormById(Guid transporteId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@TransporteID", SqlDbType.UniqueIdentifier, 0, transporteId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Transporte_GetById", parameters))
            {
                if (!reader.Read())
                {
                    return null;
                }

                return TransporteFormViewModel.FromReader(
                    reader.GetGuid(reader.GetOrdinal("TransporteID")),
                    GetString(reader, "NroCoche"),
                    GetNullableInt(reader, "MaxPasajeros"),
                    GetString(reader, "Matricula"),
                    GetOptionalString(reader, "Tipo"));
            }
        }

        public static Servicio GetServicioById(Guid servicioId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@ServicioID", SqlDbType.UniqueIdentifier, 0, servicioId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Servicio_GetById", parameters))
            {
                return reader.Read() ? MapServicio(reader) : null;
            }
        }

        public static Habitacion GetHabitacionById(Guid habitacionId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@HabitacionID", SqlDbType.UniqueIdentifier, 0, habitacionId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Habitacion_GetById", parameters))
            {
                return reader.Read() ? MapHabitacion(reader) : null;
            }
        }

        public static Hotel GetHotelById(Guid hotelId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@HotelID", SqlDbType.UniqueIdentifier, 0, hotelId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Hotel_GetById", parameters))
            {
                return reader.Read() ? MapHotel(reader) : null;
            }
        }

        public static Excursion GetExcursionById(Guid excursionId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@ExcursionID", SqlDbType.UniqueIdentifier, 0, excursionId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Excursion_GetById", parameters))
            {
                return reader.Read() ? MapExcursion(reader) : null;
            }
        }

        private static Butaca MapButaca(SqlDataReader reader)
        {
            var entity = new Butaca
            {
                ButacaId = reader.GetGuid(reader.GetOrdinal("ButacaID")),
                NroButaca = GetNullableInt(reader, "NroButaca"),
                Piso = GetNullableInt(reader, "Piso"),
                Ubicacion = GetNullableInt(reader, "Ubicacion"),
                Tipo = GetNullableInt(reader, "Tipo"),
                TransporteId = GetNullableGuid(reader, "TransporteID"),
                Fila = GetString(reader, "Fila"),
                Posicion = GetString(reader, "Posicion"),
                CodigoButaca = GetString(reader, "CodigoButaca")
            };
            return entity;
        }

        private static Transporte MapTransporte(SqlDataReader reader)
        {
            return new Transporte
            {
                TransporteId = reader.GetGuid(reader.GetOrdinal("TransporteID")),
                NroCoche = GetString(reader, "NroCoche"),
                MaxPasajeros = GetNullableInt(reader, "MaxPasajeros"),
                KmRecorridos = GetNullableInt(reader, "KmRecorridos"),
                UltimoService = GetNullableDateTime(reader, "UltimoService"),
                Matricula = GetString(reader, "Matricula")
            };
        }

        private static Servicio MapServicio(SqlDataReader reader)
        {
            return new Servicio
            {
                ServicioId = reader.GetGuid(reader.GetOrdinal("ServicioID")),
                Descripcion = GetString(reader, "Descripcion"),
                Precio = GetNullableDouble(reader, "Precio"),
                Moneda = GetString(reader, "Moneda"),
                Iva = GetString(reader, "Iva"),
                Alicuota = GetNullableDouble(reader, "Alicuota"),
                Validez = GetNullableDateTime(reader, "Validez"),
                VisibilidadTarifa = GetNullableInt(reader, "VisibilidadTarifa"),
                ProveedorId = GetNullableGuid(reader, "ProveedorID"),
                TransporteId = GetNullableGuid(reader, "TransporteID"),
                HotelId = GetNullableGuid(reader, "HotelID"),
                TipoServicio = GetNullableInt(reader, "TipoServicio")
            };
        }

        private static Habitacion MapHabitacion(SqlDataReader reader)
        {
            var entity = new Habitacion
            {
                HabitacionId = reader.GetGuid(reader.GetOrdinal("habitacionid")),
                NroHabitacion = GetNullableInt(reader, "nrohabitacion"),
                Tipo = reader.GetInt32(reader.GetOrdinal("tipo")),
                Estado = reader.GetInt32(reader.GetOrdinal("Estado")),
                Capacidad = reader.GetInt32(reader.GetOrdinal("Capacidad")),
                Ocupacion = reader.GetInt32(reader.GetOrdinal("Ocupacion")),
                Nombre = GetString(reader, "nombre"),
                HotelId = GetNullableGuid(reader, "HotelID")
            };
            return entity;
        }

        private static Hotel MapHotel(SqlDataReader reader)
        {
            return new Hotel
            {
                HotelId = reader.GetGuid(reader.GetOrdinal("HotelID")),
                Nombre = GetString(reader, "Nombre"),
                Direccion = GetString(reader, "Direccion"),
                Cp = GetString(reader, "CP"),
                Telefono = GetString(reader, "Telefono"),
                Email = GetString(reader, "Email"),
                Contacto = GetString(reader, "Contacto"),
                CantidadHabitaciones = GetNullableInt(reader, "CantidadHabitaciones"),
                Categoria = GetNullableInt(reader, "Categoria"),
                CheckIn = GetString(reader, "CheckIn"),
                CheckOut = GetString(reader, "CheckOut"),
                GoogleMapHtml = GetString(reader, "GoogleMapHtml")
            };
        }

        private static Excursion MapExcursion(SqlDataReader reader)
        {
            return new Excursion
            {
                ExcursionId = reader.GetGuid(reader.GetOrdinal("ExcursionID")),
                Descripcion = GetString(reader, "Descripcion"),
                Costo = GetNullableDouble(reader, "Costo"),
                Observaciones = GetString(reader, "Observaciones"),
                ProveedorId = GetNullableGuid(reader, "ProveedorID")
            };
        }

        private static string GetString(SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
        }

        private static string GetOptionalString(SqlDataReader reader, string column)
        {
            return HasColumn(reader, column) ? GetString(reader, column) : null;
        }

        private static bool HasColumn(IDataRecord reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (string.Equals(reader.GetName(i), columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private static int? GetNullableInt(SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            if (reader.IsDBNull(ordinal))
            {
                return null;
            }

            return Convert.ToInt32(reader.GetValue(ordinal));
        }

        private static double? GetNullableDouble(SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            if (reader.IsDBNull(ordinal))
            {
                return null;
            }

            return Convert.ToDouble(reader.GetValue(ordinal));
        }

        private static DateTime? GetNullableDateTime(SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            if (reader.IsDBNull(ordinal))
            {
                return null;
            }

            return reader.GetDateTime(ordinal);
        }

        private static Guid? GetNullableGuid(SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            if (reader.IsDBNull(ordinal))
            {
                return null;
            }

            return reader.GetGuid(ordinal);
        }
    }
}
