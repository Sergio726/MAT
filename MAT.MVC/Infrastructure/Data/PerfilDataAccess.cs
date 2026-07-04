using MAT.Entities;
using MAT.Utilities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MAT.MVC.Infrastructure.Data
{
    /// <summary>
    /// Resultado del perfil de una persona: la vista vPersona + flags de rol.
    /// </summary>
    public class PerfilData
    {
        public VPersona Persona { get; set; }
        public bool EsCliente { get; set; }
        public bool EsPasajero { get; set; }
        public bool EsVendedor { get; set; }
        public bool EsProveedor { get; set; }
    }

    /// <summary>
    /// Acceso a datos del perfil de una persona en un solo roundtrip (NetTiers F7.1).
    /// Reemplaza los 5 roundtrips de PerfilModel (vPersona + 4 GetById != null).
    /// </summary>
    public static class PerfilDataAccess
    {
        public static PerfilData GetByPersonaId(Guid personaId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@PersonaID", SqlDbType.UniqueIdentifier, 0, personaId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Perfil_GetByPersonaId", parameters))
            {
                if (!reader.Read())
                {
                    return new PerfilData();
                }

                return new PerfilData
                {
                    Persona = new VPersona
                    {
                        PersonaId = reader.GetGuid("PersonaID"),
                        Apellido = reader.GetString("Apellido"),
                        Nombre = reader.GetString("Nombre"),
                        TipoDocumento = reader.GetNullableInt("TipoDocumento"),
                        NroDocumento = reader.GetString("NroDocumento"),
                        Telefono = reader.GetString("Telefono"),
                        Email = reader.GetString("Email"),
                        FechaNacimiento = reader.GetNullableDateTime("FechaNacimiento"),
                        LocalidadId = reader.GetNullableInt("LocalidadID"),
                        UserId = reader.GetNullableInt("UserId"),
                        Domicilio = reader.GetString("Domicilio"),
                        Ocupacion = reader.GetString("Ocupacion"),
                        Nacionalidad = reader.GetString("Nacionalidad"),
                        PaisResidencia = reader.GetString("PaisResidencia"),
                        Sexo = reader.GetNullableInt("Sexo")
                    },
                    EsCliente = reader.GetBool("EsCliente"),
                    EsPasajero = reader.GetBool("EsPasajero"),
                    EsVendedor = reader.GetBool("EsVendedor"),
                    EsProveedor = reader.GetBool("EsProveedor")
                };
            }
        }
    }
}
