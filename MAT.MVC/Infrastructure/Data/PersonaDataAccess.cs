using MAT.Entities;
using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MAT.MVC.Infrastructure.Data
{
    /// <summary>
    /// Acceso a datos de la entidad Persona y la vista vPersona vía SP + DBHelper (NetTiers F7).
    /// Reemplaza PersonaService y VPersonaService.
    /// </summary>
    public static class PersonaDataAccess
    {
        public static Persona GetById(Guid personaId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@PersonaID", SqlDbType.UniqueIdentifier, 0, personaId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Persona_GetEntityById", parameters))
            {
                return reader.Read() ? Map(reader) : null;
            }
        }

        public static Persona GetByUserId(int userId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@UserId", SqlDbType.Int, 0, userId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Persona_GetEntityByUserId", parameters))
            {
                return reader.Read() ? Map(reader) : null;
            }
        }

        public static List<Persona> GetAll()
        {
            var list = new List<Persona>();
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Persona_GetAllEntities", null))
            {
                while (reader.Read())
                {
                    list.Add(Map(reader));
                }
            }
            return list;
        }

        /// <summary>
        /// Inserta la Persona. Si llega con PersonaId vacío se genera uno nuevo y se asigna a la entidad
        /// (paridad con NetTiers, que generaba el Guid antes del insert).
        /// </summary>
        public static void Insert(Persona persona)
        {
            if (persona.PersonaId == Guid.Empty)
            {
                persona.PersonaId = Guid.NewGuid();
            }
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Persona_InsertEntity", BuildParams(persona));
        }

        public static void Update(Persona persona)
        {
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Persona_UpdateEntity", BuildParams(persona));
        }

        public static void Delete(Guid personaId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@PersonaID", SqlDbType.UniqueIdentifier, 0, personaId)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Persona_DeleteEntity", parameters);
        }

        private static SqlParameter[] BuildParams(Persona p)
        {
            return new[]
            {
                DBHelper.MakeParam("@PersonaID", SqlDbType.UniqueIdentifier, 0, p.PersonaId),
                DBHelper.MakeParam("@Apellido", SqlDbType.VarChar, 100, (object)p.Apellido ?? DBNull.Value),
                DBHelper.MakeParam("@Nombre", SqlDbType.VarChar, 100, (object)p.Nombre ?? DBNull.Value),
                DBHelper.MakeParam("@TipoDocumento", SqlDbType.Int, 0, (object)p.TipoDocumento ?? DBNull.Value),
                DBHelper.MakeParam("@NroDocumento", SqlDbType.VarChar, 50, (object)p.NroDocumento ?? DBNull.Value),
                DBHelper.MakeParam("@Celular", SqlDbType.VarChar, 50, (object)p.Celular ?? DBNull.Value),
                DBHelper.MakeParam("@Telefono", SqlDbType.VarChar, 50, (object)p.Telefono ?? DBNull.Value),
                DBHelper.MakeParam("@Email", SqlDbType.VarChar, 50, (object)p.Email ?? DBNull.Value),
                DBHelper.MakeParam("@FechaNacimiento", SqlDbType.Date, 0, (object)p.FechaNacimiento ?? DBNull.Value),
                DBHelper.MakeParam("@LocalidadID", SqlDbType.Int, 0, (object)p.LocalidadId ?? DBNull.Value),
                DBHelper.MakeParam("@UserId", SqlDbType.Int, 0, (object)p.UserId ?? DBNull.Value),
                DBHelper.MakeParam("@Domicilio", SqlDbType.VarChar, 100, (object)p.Domicilio ?? DBNull.Value),
                DBHelper.MakeParam("@Sexo", SqlDbType.Int, 0, (object)p.Sexo ?? DBNull.Value),
                DBHelper.MakeParam("@Ocupacion", SqlDbType.VarChar, 50, (object)p.Ocupacion ?? DBNull.Value),
                DBHelper.MakeParam("@Nacionalidad", SqlDbType.VarChar, 50, (object)p.Nacionalidad ?? DBNull.Value),
                DBHelper.MakeParam("@PaisResidencia", SqlDbType.VarChar, 50, (object)p.PaisResidencia ?? DBNull.Value),
                DBHelper.MakeParam("@Provincia", SqlDbType.Int, 0, (object)p.Provincia ?? DBNull.Value)
            };
        }

        private static Persona Map(SqlDataReader reader)
        {
            return new Persona
            {
                PersonaId = reader.GetGuid("PersonaID"),
                Apellido = reader.GetString("Apellido"),
                Nombre = reader.GetString("Nombre"),
                TipoDocumento = reader.GetNullableInt("TipoDocumento"),
                NroDocumento = reader.GetString("NroDocumento"),
                Celular = reader.GetString("Celular"),
                Telefono = reader.GetString("Telefono"),
                Email = reader.GetString("Email"),
                FechaNacimiento = reader.GetNullableDateTime("FechaNacimiento"),
                LocalidadId = reader.GetNullableInt("LocalidadID"),
                UserId = reader.GetNullableInt("UserId"),
                Domicilio = reader.GetString("Domicilio"),
                Sexo = reader.GetNullableInt("Sexo"),
                Ocupacion = reader.GetString("Ocupacion"),
                Nacionalidad = reader.GetString("Nacionalidad"),
                PaisResidencia = reader.GetString("PaisResidencia"),
                Provincia = reader.GetNullableInt("Provincia")
            };
        }
    }
}
