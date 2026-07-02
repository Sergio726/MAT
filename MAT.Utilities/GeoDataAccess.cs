using MAT.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MAT.Utilities
{
    /// <summary>
    /// Jerarquía geo de una localidad (país → provincia → departamento → localidad).
    /// </summary>
    public sealed class LocalidadGeoInfo
    {
        public int IdLocalidad { get; set; }
        public int IdDepartamento { get; set; }
        public int IdProvincia { get; set; }
        public Guid IdPais { get; set; }
    }

    /// <summary>
    /// Catálogo geográfico vía SP + DBHelper (NetTiers F2).
    /// Errores SQL se propagan al caller MVC para normalización con ErrorUtil.
    /// </summary>
    public static class GeoDataAccess
    {
        private const int MinSearchTermLength = 3;

        public static List<Pais> GetAllPaises()
        {
            return ReadList("dbo.usp_MAT_Pais_GetAll", null, MapPais);
        }

        public static List<Provincia> GetAllProvincias()
        {
            return ReadList("dbo.usp_GetAllProvincia", null, reader => new Provincia
            {
                Id = reader.GetInt32(reader.GetOrdinal("ID")),
                Nombre = GetString(reader, "Nombre")
            });
        }

        public static Provincia GetProvinciaById(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var parameters = new[]
            {
                DBHelper.MakeParam("@ID", SqlDbType.Int, 0, id)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Provincia_GetById", parameters))
            {
                return reader.Read() ? MapProvincia(reader) : null;
            }
        }

        public static List<Departamento> GetDepartamentosByProvinciaId(int idProvincia)
        {
            if (idProvincia <= 0)
            {
                return new List<Departamento>();
            }

            var parameters = new[]
            {
                DBHelper.MakeParam("@IdProvincia", SqlDbType.Int, 0, idProvincia)
            };
            return ReadList("dbo.usp_MAT_Departamento_GetByProvinciaId", parameters, MapDepartamento);
        }

        public static Localidad GetLocalidadById(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var parameters = new[]
            {
                DBHelper.MakeParam("@ID", SqlDbType.Int, 0, id)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Localidad_GetById", parameters))
            {
                return reader.Read() ? MapLocalidad(reader) : null;
            }
        }

        /// <summary>
        /// Catálogo completo de localidades. No usar para dropdowns UI; preferir
        /// <see cref="SearchLocalidades"/> o cascada por departamento.
        /// </summary>
        [Obsolete("No usar para dropdowns UI. Usar SearchLocalidades o matGeo.")]
        public static List<Localidad> GetAllLocalidades()
        {
            return ReadList("dbo.usp_MAT_Localidad_GetAll", null, reader => new Localidad
            {
                Id = reader.GetInt32(reader.GetOrdinal("ID")),
                Nombre = GetString(reader, "Nombre")
            });
        }

        public static int InsertLocalidad(int idDepartamento, string nombre)
        {
            if (idDepartamento <= 0)
            {
                throw new ArgumentException("Departamento inválido.", nameof(idDepartamento));
            }

            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre de la localidad es obligatorio.", nameof(nombre));
            }

            var parameters = new[]
            {
                DBHelper.MakeParam("@IdDepartamento", SqlDbType.Int, 0, idDepartamento),
                DBHelper.MakeParam("@Nombre", SqlDbType.NVarChar, 250, nombre.Trim())
            };
            return DBHelper.ExecuteNonQueryOutput("dbo.usp_MAT_Localidad_Insert", parameters, "@Id", SqlDbType.Int, 0);
        }

        public static List<Provincia> GetProvinciasByPaisId(string paisId)
        {
            if (string.IsNullOrWhiteSpace(paisId))
            {
                return new List<Provincia>();
            }

            Guid paisGuid;
            if (!Guid.TryParse(paisId.Trim(), out paisGuid))
            {
                return new List<Provincia>();
            }

            var parameters = new[]
            {
                DBHelper.MakeParam("@PaisID", SqlDbType.VarChar, 36, paisGuid.ToString())
            };
            return ReadList("dbo.usp_Provincia_GetAllByPaisID", parameters, MapProvincia);
        }

        public static List<VLocalidad> GetLocalidadesByDepartamentoId(int idDepartamento)
        {
            if (idDepartamento <= 0)
            {
                return new List<VLocalidad>();
            }

            var parameters = new[]
            {
                DBHelper.MakeParam("@IdDepartamento", SqlDbType.Int, 0, idDepartamento)
            };
            return ReadVLocalidadList("dbo.usp_Localidad_GetByIdDepartamento", parameters);
        }

        public static LocalidadGeoInfo GetLocalidadGeoInfo(int idLocalidad)
        {
            if (idLocalidad <= 0)
            {
                return null;
            }

            var parameters = new[]
            {
                DBHelper.MakeParam("@idLocalidad", SqlDbType.Int, 0, idLocalidad)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_GetInfoByLocalidadId", parameters))
            {
                if (!reader.Read())
                {
                    return null;
                }

                return new LocalidadGeoInfo
                {
                    IdLocalidad = reader.GetInt32(reader.GetOrdinal("IdLocalidad")),
                    IdDepartamento = reader.GetInt32(reader.GetOrdinal("IdDepartamento")),
                    IdProvincia = reader.GetInt32(reader.GetOrdinal("IdProvincia")),
                    IdPais = reader.GetGuid(reader.GetOrdinal("IdPais"))
                };
            }
        }

        public static List<VLocalidad> SearchLocalidades(string term, int? idProvincia, int? idDepartamento)
        {
            string trimmed;
            if (!TryNormalizeSearchTerm(term, out trimmed))
            {
                return new List<VLocalidad>();
            }

            var parameters = new[]
            {
                DBHelper.MakeParam("@Term", SqlDbType.NVarChar, 250, trimmed),
                DBHelper.MakeParam("@IdProvincia", SqlDbType.Int, 0, ToOptionalInt(idProvincia)),
                DBHelper.MakeParam("@IdDepartamento", SqlDbType.Int, 0, ToOptionalInt(idDepartamento))
            };
            return ReadVLocalidadList("dbo.usp_MAT_Localidad_Search", parameters);
        }

        private static List<T> ReadList<T>(string storedProcedure, SqlParameter[] parameters, Func<SqlDataReader, T> map)
        {
            var list = new List<T>();
            using (var reader = DBHelper.ExecuteDataReader(storedProcedure, parameters))
            {
                while (reader.Read())
                {
                    list.Add(map(reader));
                }
            }
            return list;
        }

        private static List<VLocalidad> ReadVLocalidadList(string storedProcedure, SqlParameter[] parameters)
        {
            return ReadList(storedProcedure, parameters, MapVLocalidad);
        }

        private static bool TryNormalizeSearchTerm(string term, out string trimmed)
        {
            trimmed = (term ?? string.Empty).Trim();
            return trimmed.Length >= MinSearchTermLength;
        }

        private static object ToOptionalInt(int? value)
        {
            return value.HasValue && value.Value > 0 ? (object)value.Value : DBNull.Value;
        }

        private static Pais MapPais(SqlDataReader reader)
        {
            return new Pais
            {
                PaisId = reader.GetGuid(reader.GetOrdinal("PaisID")),
                Descripcion = GetString(reader, "Descripcion")
            };
        }

        private static Provincia MapProvincia(SqlDataReader reader)
        {
            return new Provincia
            {
                Id = reader.GetInt32(reader.GetOrdinal("ID")),
                Nombre = GetString(reader, "Nombre"),
                IdPais = reader.GetGuid(reader.GetOrdinal("IdPais"))
            };
        }

        private static Departamento MapDepartamento(SqlDataReader reader)
        {
            return new Departamento
            {
                Id = reader.GetInt32(reader.GetOrdinal("ID")),
                IdProvincia = reader.GetInt32(reader.GetOrdinal("IdProvincia")),
                Nombre = GetString(reader, "Nombre")
            };
        }

        private static Localidad MapLocalidad(SqlDataReader reader)
        {
            return new Localidad
            {
                Id = reader.GetInt32(reader.GetOrdinal("ID")),
                IdDepartamento = reader.GetInt32(reader.GetOrdinal("IdDepartamento")),
                Nombre = GetString(reader, "Nombre")
            };
        }

        private static VLocalidad MapVLocalidad(SqlDataReader reader)
        {
            return new VLocalidad
            {
                Id = reader.GetInt32(reader.GetOrdinal("ID")),
                Nombre = GetString(reader, "Nombre")
            };
        }

        private static string GetString(SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
        }
    }
}
