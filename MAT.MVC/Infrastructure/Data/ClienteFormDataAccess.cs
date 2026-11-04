using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MAT.MVC.Infrastructure.Clientes;
using MAT.MVC.Models.Clientes;
using MAT.Utilities;

namespace MAT.MVC.Infrastructure.Data
{
    /// <summary>
    /// Persona con el mismo documento que otra (para el aviso de documento repetido).
    /// </summary>
    public sealed class DocumentoExistente
    {
        public Guid PersonaId { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string NroDocumento { get; set; }
        public bool EsCliente { get; set; }

        public string NombreCompleto
        {
            get { return string.Join(", ", new[] { Apellido?.Trim(), Nombre?.Trim() }.Where(s => !string.IsNullOrEmpty(s))); }
        }
    }

    /// <summary>
    /// Formulario de cliente: carga para editar, alta/edición transaccional y documento repetido
    /// (SPs <c>usp_MAT_PersonaCliente_Guardar</c> y <c>usp_MAT_Persona_BuscarPorDocumento</c>).
    /// </summary>
    public static class ClienteFormDataAccess
    {
        public static ClienteFormModel Obtener(Guid personaId)
        {
            var persona = PersonaDataAccess.GetById(personaId);
            var cliente = persona == null ? null : ClienteDataAccess.GetById(personaId);
            if (persona == null || cliente == null) return null;

            var cuenta = CuentaDataAccess.GetByClienteId(personaId).FirstOrDefault();
            return ClienteFormMapper.DesdeEntidades(persona, cliente, cuenta == null ? (bool?)null : cuenta.Estado);
        }

        /// <summary>
        /// Guarda (alta si <see cref="ClienteFormModel.PersonaId"/> es null) y devuelve el id del cliente.
        /// El modelo tiene que venir validado y normalizado.
        /// </summary>
        public static Guid Guardar(ClienteFormModel m, Guid? vendedorId)
        {
            DateTime fechaNacimiento;
            if (!ClienteValidacion.TryParseFecha(m.FechaNacimiento, out fechaNacimiento))
                throw new ArgumentException("Fecha de nacimiento inválida.", "m");

            var parameters = new List<System.Data.SqlClient.SqlParameter>
            {
                DBHelper.MakeParam("@PersonaID", SqlDbType.UniqueIdentifier, 0, (object)m.PersonaId ?? DBNull.Value),
                DBHelper.MakeParam("@Apellido", SqlDbType.VarChar, 100, m.Apellido),
                DBHelper.MakeParam("@Nombre", SqlDbType.VarChar, 100, m.Nombre),
                DBHelper.MakeParam("@TipoDocumento", SqlDbType.Int, 0, m.TipoDocumento),
                DBHelper.MakeParam("@NroDocumento", SqlDbType.VarChar, 50, m.NroDocumento),
                DBHelper.MakeParam("@Sexo", SqlDbType.Int, 0, (object)m.Sexo ?? DBNull.Value),
                DBHelper.MakeParam("@FechaNacimiento", SqlDbType.Date, 0, fechaNacimiento),
                DBHelper.MakeParam("@Nacionalidad", SqlDbType.VarChar, 50, m.Nacionalidad),
                DBHelper.MakeParam("@PaisResidencia", SqlDbType.VarChar, 50, m.PaisResidencia),
                DBHelper.MakeParam("@Telefono", SqlDbType.VarChar, 50, (object)m.Telefono ?? DBNull.Value),
                DBHelper.MakeParam("@Celular", SqlDbType.VarChar, 50, (object)m.Celular ?? DBNull.Value),
                DBHelper.MakeParam("@Email", SqlDbType.VarChar, 50, (object)m.Email ?? DBNull.Value),
                DBHelper.MakeParam("@Domicilio", SqlDbType.VarChar, 100, m.Domicilio),
                DBHelper.MakeParam("@Provincia", SqlDbType.Int, 0, m.Provincia),
                DBHelper.MakeParam("@LocalidadID", SqlDbType.Int, 0, m.LocalidadId),
                DBHelper.MakeParam("@Ocupacion", SqlDbType.VarChar, 50, (object)m.Ocupacion ?? DBNull.Value),
                DBHelper.MakeParam("@RazonSocial", SqlDbType.VarChar, 50, (object)m.RazonSocial ?? DBNull.Value),
                DBHelper.MakeParam("@Cuit", SqlDbType.VarChar, 50, (object)m.Cuit ?? DBNull.Value),
                DBHelper.MakeParam("@Empresa", SqlDbType.VarChar, 50, (object)m.Empresa ?? DBNull.Value),
                DBHelper.MakeParam("@CondicionIva", SqlDbType.Int, 0, (object)m.CondicionIva ?? DBNull.Value),
                DBHelper.MakeParam("@FormaPago", SqlDbType.Int, 0, (object)m.FormaPago ?? DBNull.Value),
                DBHelper.MakeParam("@TipoID", SqlDbType.Int, 0, m.TipoId),
                DBHelper.MakeParam("@Fax", SqlDbType.VarChar, 50, (object)m.Fax ?? DBNull.Value),
                DBHelper.MakeParam("@Web", SqlDbType.VarChar, 50, (object)m.Web ?? DBNull.Value),
                DBHelper.MakeParam("@Idioma", SqlDbType.VarChar, 50, (object)m.Idioma ?? DBNull.Value),
                DBHelper.MakeParam("@Promotor", SqlDbType.VarChar, 50, (object)m.Promotor ?? DBNull.Value),
                DBHelper.MakeParam("@Observacion", SqlDbType.VarChar, 250, (object)m.Observacion ?? DBNull.Value),
                DBHelper.MakeParam("@CuentaCorrienteHabilitada", SqlDbType.Bit, 0, m.CuentaCorrienteHabilitada),
                DBHelper.MakeParam("@VendedorID", SqlDbType.UniqueIdentifier, 0, (object)vendedorId ?? DBNull.Value)
            };
            return (Guid)DBHelper.ExecuteScalar("dbo.usp_MAT_PersonaCliente_Guardar", parameters.ToArray());
        }

        /// <summary>
        /// Otra persona con el mismo número de documento (sin puntos ni ceros a la izquierda), o null.
        /// </summary>
        public static DocumentoExistente BuscarPorDocumento(string documento, Guid? excluirPersonaId)
        {
            if (string.IsNullOrWhiteSpace(documento)) return null;
            var parameters = new[]
            {
                DBHelper.MakeParam("@Documento", SqlDbType.VarChar, 50, documento.Trim()),
                DBHelper.MakeParam("@ExcluirPersonaID", SqlDbType.UniqueIdentifier, 0, (object)excluirPersonaId ?? DBNull.Value)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Persona_BuscarPorDocumento", parameters))
            {
                if (!reader.Read()) return null;
                return new DocumentoExistente
                {
                    PersonaId = reader.GetGuid("PersonaID"),
                    Apellido = reader.GetString("Apellido"),
                    Nombre = reader.GetString("Nombre"),
                    NroDocumento = reader.GetString("NroDocumento"),
                    EsCliente = reader.GetBool("EsCliente")
                };
            }
        }
    }
}
