using MAT.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MAT.Utilities
{
    /// <summary>
    /// Catálogo geográfico vía SP + DBHelper (NetTiers F2).
    /// Errores SQL se propagan al caller MVC para normalización con ErrorUtil.
    /// </summary>
    public static class GeoDataAccess
    {
        public static List<Pais> GetAllPaises()
        {
            var list = new List<Pais>();
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Pais_GetAll", null))
            {
                while (reader.Read())
                {
                    list.Add(MapPais(reader));
                }
            }
            return list;
        }

        public static List<Provincia> GetAllProvincias()
        {
            var list = new List<Provincia>();
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_GetAllProvincia", null))
            {
                while (reader.Read())
                {
                    list.Add(new Provincia
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("ID")),
                        Nombre = GetString(reader, "Nombre")
                    });
                }
            }
            return list;
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

            var list = new List<Departamento>();
            var parameters = new[]
            {
                DBHelper.MakeParam("@IdProvincia", SqlDbType.Int, 0, idProvincia)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Departamento_GetByProvinciaId", parameters))
            {
                while (reader.Read())
                {
                    list.Add(MapDepartamento(reader));
                }
            }
            return list;
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

        public static List<Localidad> GetAllLocalidades()
        {
            var list = new List<Localidad>();
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Localidad_GetAll", null))
            {
                while (reader.Read())
                {
                    list.Add(new Localidad
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("ID")),
                        Nombre = GetString(reader, "Nombre")
                    });
                }
            }
            return list;
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

        public static List<VLocalidad> SearchVLocalidad(string query)
        {
            var trimmed = (query ?? string.Empty).Trim();
            if (trimmed.Length < 3)
            {
                return new List<VLocalidad>();
            }

            var parameters = new[]
            {
                DBHelper.MakeParam("@Query", SqlDbType.NVarChar, 250, trimmed)
            };
            var list = new List<VLocalidad>();
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_VLocalidad_Search", parameters))
            {
                while (reader.Read())
                {
                    list.Add(new VLocalidad
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("ID")),
                        Nombre = GetString(reader, "Nombre")
                    });
                }
            }
            return list;
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

        private static string GetString(SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
        }
    }
}
