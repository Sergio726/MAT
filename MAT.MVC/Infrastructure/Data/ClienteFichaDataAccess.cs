using System;
using System.Data;
using MAT.Enums;
using MAT.MVC.Infrastructure.Clientes;
using MAT.MVC.Models.Clientes;
using MAT.Utilities;

namespace MAT.MVC.Infrastructure.Data
{
    /// <summary>Datos de la ficha del cliente (<c>usp_MAT_PersonaCliente_Ficha</c>).</summary>
    public static class ClienteFichaDataAccess
    {
        /// <returns>Null si no existe un cliente con ese id.</returns>
        public static ClienteFichaModel Get(Guid personaId)
        {
            var parameters = new[] { DBHelper.MakeParam("@PersonaID", SqlDbType.UniqueIdentifier, 0, personaId) };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_PersonaCliente_Ficha", parameters))
            {
                if (!reader.Read()) return null;

                var fechaNacimiento = reader.GetNullableDateTime("FechaNacimiento");
                return new ClienteFichaModel
                {
                    PersonaId = reader.GetGuid("PersonaID"),
                    Apellido = Limpio(reader.GetString("Apellido")),
                    Nombre = Limpio(reader.GetString("Nombre")),
                    TipoDocumentoTexto = TextoEnum.De<eTipoDocumento>(reader.GetNullableInt("TipoDocumento")),
                    NroDocumento = Limpio(reader.GetString("NroDocumento")),
                    FechaNacimiento = fechaNacimiento,
                    Edad = Edad(fechaNacimiento),
                    SexoTexto = TextoEnum.De<eSexo>(reader.GetNullableInt("Sexo")),
                    Nacionalidad = Limpio(reader.GetString("Nacionalidad")),
                    PaisResidencia = Limpio(reader.GetString("PaisResidencia")),
                    Ocupacion = Limpio(reader.GetString("Ocupacion")),
                    Domicilio = Limpio(reader.GetString("Domicilio")),
                    Telefono = Limpio(reader.GetString("Telefono")),
                    Celular = Limpio(reader.GetString("Celular")),
                    Email = Limpio(reader.GetString("Email")),
                    LocalidadNombre = Limpio(reader.GetString("LocalidadNombre")),
                    DepartamentoNombre = Limpio(reader.GetString("DepartamentoNombre")),
                    ProvinciaNombre = Limpio(reader.GetString("ProvinciaNombre")),
                    RazonSocial = Limpio(reader.GetString("RazonSocial")),
                    Cuit = Limpio(reader.GetString("Cuit")),
                    CondicionIvaTexto = TextoEnum.De<eCondicionIVA>(reader.GetNullableInt("CondicionIva")),
                    FormaPagoTexto = TextoEnum.De<eFormaPago>(reader.GetNullableInt("FormaPago")),
                    Empresa = Limpio(reader.GetString("Empresa")),
                    Fax = Limpio(reader.GetString("Fax")),
                    Web = Limpio(reader.GetString("Web")),
                    Idioma = Limpio(reader.GetString("Idioma")),
                    Promotor = Limpio(reader.GetString("Promotor")),
                    Observacion = Limpio(reader.GetString("Observacion")),
                    TipoClienteTexto = TextoEnum.De<eTipoCliente>(reader.GetNullableInt("TipoID")),
                    FechaAlta = reader.GetNullableDateTime("FechaAlta"),
                    VendedorNombre = Limpio(reader.GetString("VendedorNombre")?.Trim(' ', ',')),
                    CuentaHabilitada = reader["CuentaHabilitada"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["CuentaHabilitada"]),
                    CantidadFacturas = reader.GetInt("CantidadFacturas")
                };
            }
        }

        private static string Limpio(string valor)
        {
            return string.IsNullOrWhiteSpace(valor) ? null : valor.Trim();
        }

        private static int? Edad(DateTime? nacimiento)
        {
            if (!nacimiento.HasValue) return null;
            var hoy = ClienteValidacion.HoyArgentina();
            var edad = hoy.Year - nacimiento.Value.Year;
            if (nacimiento.Value.Date > hoy.AddYears(-edad)) edad--;
            return edad >= 0 && edad < 120 ? edad : (int?)null;
        }
    }
}
