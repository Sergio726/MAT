using MAT.Entities;
using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MAT.MVC.Infrastructure.Data
{
    /// <summary>
    /// Acceso a datos de la vista PersonaCliente vía SP + DBHelper (NetTiers F7).
    /// Reemplaza PersonaClienteService.
    /// </summary>
    public static class PersonaClienteDataAccess
    {
        public static List<PersonaCliente> GetAll()
        {
            return Query(null);
        }

        public static PersonaCliente GetByPersonaId(Guid personaId)
        {
            var list = Query(personaId);
            return list.Count > 0 ? list[0] : null;
        }

        private static List<PersonaCliente> Query(Guid? personaId)
        {
            var list = new List<PersonaCliente>();
            var parameters = new[]
            {
                DBHelper.MakeParam("@PersonaID", SqlDbType.UniqueIdentifier, 0, personaId.HasValue ? (object)personaId.Value : DBNull.Value)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_PersonaCliente_GetEntities", parameters))
            {
                while (reader.Read())
                {
                    list.Add(new PersonaCliente
                    {
                        PersonaId = reader.GetGuid("PersonaID"),
                        Apellido = reader.GetString("Apellido"),
                        Nombre = reader.GetString("Nombre"),
                        NroDocumento = reader.GetString("NroDocumento"),
                        Telefono = reader.GetString("Telefono"),
                        Email = reader.GetString("Email"),
                        FechaNacimiento = reader.GetNullableDateTime("FechaNacimiento"),
                        Domicilio = reader.GetString("Domicilio"),
                        Sexo = reader.GetNullableInt("Sexo"),
                        LocalidadId = reader.GetNullableInt("LocalidadID"),
                        ClienteId = reader.GetGuid("ClienteID"),
                        RazonSocial = reader.GetString("RazonSocial"),
                        Cuit = reader.GetString("Cuit"),
                        Moneda = reader.GetString("Moneda"),
                        Empresa = reader.GetString("Empresa"),
                        Ocupacion = reader.GetString("Ocupacion"),
                        FormaPago = reader.GetNullableInt("FormaPago"),
                        CondicionIva = reader.GetNullableInt("CondicionIva"),
                        VendedorId = reader.GetNullableGuid("VendedorID"),
                        Fax = reader.GetString("Fax"),
                        Web = reader.GetString("Web"),
                        Idioma = reader.GetString("Idioma"),
                        Promotor = reader.GetString("Promotor"),
                        Observacion = reader.GetString("Observacion"),
                        TipoId = reader.GetInt("TipoID"),
                        TipoDocumento = reader.GetNullableInt("TipoDocumento"),
                        Celular = reader.GetString("Celular"),
                        Nacionalidad = reader.GetString("Nacionalidad"),
                        PaisResidencia = reader.GetString("PaisResidencia"),
                        Provincia = reader.GetNullableInt("Provincia")
                    });
                }
            }
            return list;
        }
    }

    /// <summary>
    /// Acceso a datos de la vista PersonaPasajero vía SP + DBHelper (NetTiers F7).
    /// Reemplaza PersonaPasajeroService.
    /// </summary>
    public static class PersonaPasajeroDataAccess
    {
        public static List<PersonaPasajero> GetAll()
        {
            return Query(null, null, null);
        }

        /// <summary>
        /// Devuelve como máximo <paramref name="top"/> filas (F7.1: evita cargar toda la vista
        /// en la búsqueda rápida por GridView).
        /// </summary>
        public static List<PersonaPasajero> GetTop(int top)
        {
            return Query(null, null, top);
        }

        public static PersonaPasajero GetByPersonaId(Guid personaId)
        {
            var list = Query(personaId, null, null);
            return list.Count > 0 ? list[0] : null;
        }

        public static List<PersonaPasajero> Search(string term)
        {
            return Query(null, term, null);
        }

        private static List<PersonaPasajero> Query(Guid? personaId, string term, int? top)
        {
            var list = new List<PersonaPasajero>();
            var parameters = new[]
            {
                DBHelper.MakeParam("@PersonaID", SqlDbType.UniqueIdentifier, 0, personaId.HasValue ? (object)personaId.Value : DBNull.Value),
                DBHelper.MakeParam("@Term", SqlDbType.VarChar, 100, string.IsNullOrEmpty(term) ? (object)DBNull.Value : term),
                DBHelper.MakeParam("@Top", SqlDbType.Int, 0, top.HasValue ? (object)top.Value : DBNull.Value)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_PersonaPasajero_GetEntities", parameters))
            {
                while (reader.Read())
                {
                    list.Add(new PersonaPasajero
                    {
                        PersonaId = reader.GetGuid("PersonaID"),
                        Apellido = reader.GetString("Apellido"),
                        Nombre = reader.GetString("Nombre"),
                        NroDocumento = reader.GetString("NroDocumento"),
                        Telefono = reader.GetString("Telefono"),
                        Domicilio = reader.GetString("Domicilio"),
                        Email = reader.GetString("Email"),
                        FechaNacimiento = reader.GetNullableDateTime("FechaNacimiento"),
                        Sexo = reader.GetNullableInt("Sexo"),
                        PasajeroId = reader.GetGuid("PasajeroID"),
                        Pasaporte = reader.GetString("Pasaporte"),
                        VencimientoPasaporte = reader.GetNullableDateTime("VencimientoPasaporte"),
                        EmisionPasaporte = reader.GetNullableDateTime("EmisionPasaporte"),
                        PaisOrigen = reader.GetString("PaisOrigen"),
                        LocalidadId = reader.GetNullableInt("LocalidadID"),
                        TipoDocumento = reader.GetNullableInt("TipoDocumento")
                    });
                }
            }
            return list;
        }
    }

    /// <summary>
    /// Acceso a datos de la vista PersonaProveedor vía SP + DBHelper (NetTiers F7).
    /// Reemplaza PersonaProveedorService.
    /// </summary>
    public static class PersonaProveedorDataAccess
    {
        public static List<PersonaProveedor> GetAll()
        {
            var list = new List<PersonaProveedor>();
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_PersonaProveedor_GetEntities", null))
            {
                while (reader.Read())
                {
                    list.Add(new PersonaProveedor
                    {
                        PersonaId = reader.GetGuid("PersonaID"),
                        Apellido = reader.GetString("Apellido"),
                        Nombre = reader.GetString("Nombre"),
                        NroDocumento = reader.GetString("NroDocumento"),
                        LocalidadId = reader.GetNullableInt("LocalidadID"),
                        Telefono = reader.GetString("Telefono"),
                        Email = reader.GetString("Email"),
                        FechaNacimiento = reader.GetNullableDateTime("FechaNacimiento"),
                        Sexo = reader.GetNullableInt("Sexo"),
                        Domicilio = reader.GetString("Domicilio"),
                        ProveedorId = reader.GetGuid("ProveedorID"),
                        RazonSocial = reader.GetString("RazonSocial"),
                        ProveedorLocalidadId = reader.GetNullableInt("ProveedorLocalidadID"),
                        ProveedorTelefono = reader.GetString("ProveedorTelefono"),
                        Fax = reader.GetString("Fax"),
                        Web = reader.GetString("Web"),
                        ProveedorEmail = reader.GetString("ProveedorEmail"),
                        Idioma = reader.GetString("Idioma"),
                        CondicionIva = reader.GetNullableInt("CondicionIva"),
                        Cuit = reader.GetString("Cuit"),
                        FormaPago = reader.GetNullableInt("FormaPago"),
                        TipoDocumento = reader.GetNullableInt("TipoDocumento")
                    });
                }
            }
            return list;
        }
    }

    /// <summary>
    /// Acceso a datos de la vista PersonaVendedor vía SP + DBHelper (NetTiers F7).
    /// Reemplaza PersonaVendedorService.
    /// </summary>
    public static class PersonaVendedorDataAccess
    {
        public static PersonaVendedor GetByPersonaId(Guid personaId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@PersonaID", SqlDbType.UniqueIdentifier, 0, personaId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_PersonaVendedor_GetEntities", parameters))
            {
                return reader.Read() ? Map(reader) : null;
            }
        }

        private static PersonaVendedor Map(SqlDataReader reader)
        {
            return new PersonaVendedor
            {
                PersonaId = reader.GetGuid("PersonaID"),
                Apellido = reader.GetString("Apellido"),
                Nombre = reader.GetString("Nombre"),
                NroDocumento = reader.GetString("NroDocumento"),
                Domicilio = reader.GetString("Domicilio"),
                Telefono = reader.GetString("Telefono"),
                Email = reader.GetString("Email"),
                FechaNacimiento = reader.GetNullableDateTime("FechaNacimiento"),
                Sexo = reader.GetNullableInt("Sexo"),
                LocalidadId = reader.GetNullableInt("LocalidadID"),
                Descripcion = reader.GetString("Descripcion"),
                VendedorId = reader.GetGuid("VendedorID"),
                TipoDocumento = reader.GetNullableInt("TipoDocumento")
            };
        }
    }

    /// <summary>
    /// Acceso a datos de la vista vPersona vía SP + DBHelper (NetTiers F7). Reemplaza VPersonaService.
    /// </summary>
    public static class VPersonaDataAccess
    {
        public static VPersona GetByPersonaId(Guid personaId)
        {
            var list = Query(personaId, null);
            return list.Count > 0 ? list[0] : null;
        }

        public static List<VPersona> Search(string term)
        {
            return Query(null, term);
        }

        private static List<VPersona> Query(Guid? personaId, string term)
        {
            var list = new List<VPersona>();
            var parameters = new[]
            {
                DBHelper.MakeParam("@PersonaID", SqlDbType.UniqueIdentifier, 0, personaId.HasValue ? (object)personaId.Value : DBNull.Value),
                DBHelper.MakeParam("@Term", SqlDbType.VarChar, 100, string.IsNullOrEmpty(term) ? (object)DBNull.Value : term)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_VPersona_GetEntities", parameters))
            {
                while (reader.Read())
                {
                    list.Add(new VPersona
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
                    });
                }
            }
            return list;
        }
    }
}
