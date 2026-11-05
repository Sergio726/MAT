using System;
using System.Collections.Generic;
using MAT.MVC.Models.Seguimiento;

namespace MAT.MVC.Models.Clientes
{
    /// <summary>Un evento del historial del cliente (<c>ClienteAudit</c>).</summary>
    public sealed class ClienteAuditItemDto
    {
        public DateTime Fecha { get; set; }
        public string Usuario { get; set; }
        /// <summary>Alta | Modificacion | Baja | Reactivacion</summary>
        public string Accion { get; set; }
        /// <summary>Una línea por campo cambiado; en la baja, el motivo.</summary>
        public string Detalle { get; set; }
    }

    /// <summary>
    /// Ficha del cliente (<c>PersonaCliente/Details</c>): datos con los textos ya resueltos
    /// (<c>usp_MAT_PersonaCliente_Ficha</c>) más el resumen de Seguimiento (<c>usp_MAT_Seguimiento_Ficha</c>).
    /// </summary>
    public sealed class ClienteFichaModel
    {
        public Guid PersonaId { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string TipoDocumentoTexto { get; set; }
        public string NroDocumento { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? Edad { get; set; }
        public string SexoTexto { get; set; }
        public string Nacionalidad { get; set; }
        public string PaisResidencia { get; set; }
        public string Ocupacion { get; set; }

        public string Domicilio { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string LocalidadNombre { get; set; }
        public string DepartamentoNombre { get; set; }
        public string ProvinciaNombre { get; set; }

        public string RazonSocial { get; set; }
        public string Cuit { get; set; }
        public string CondicionIvaTexto { get; set; }
        public string FormaPagoTexto { get; set; }
        public string Empresa { get; set; }
        public string Fax { get; set; }
        public string Web { get; set; }
        public string Idioma { get; set; }
        public string Promotor { get; set; }
        public string Observacion { get; set; }
        public string TipoClienteTexto { get; set; }
        public DateTime? FechaAlta { get; set; }
        public string VendedorNombre { get; set; }
        public bool? CuentaHabilitada { get; set; }
        public int CantidadFacturas { get; set; }

        // Baja lógica (F6)
        public bool Activo { get; set; }
        public DateTime? FechaBaja { get; set; }
        public string MotivoBaja { get; set; }
        public string BajaUsuario { get; set; }

        /// <summary>Categoría, métricas, viajes y contactos. Null si Seguimiento no pudo calcularse.</summary>
        public SeguimientoFichaDto Seguimiento { get; set; }

        /// <summary>Resultados posibles al registrar un contacto (los mismos de Seguimiento).</summary>
        public IList<KeyValuePair<int, string>> ResultadosContacto { get; set; }

        public string NombreCompleto
        {
            get
            {
                var partes = new List<string>();
                if (!string.IsNullOrWhiteSpace(Apellido)) partes.Add(Apellido);
                if (!string.IsNullOrWhiteSpace(Nombre)) partes.Add(Nombre);
                return partes.Count == 0 ? "Cliente sin nombre" : string.Join(", ", partes);
            }
        }

        public string Iniciales
        {
            get
            {
                var a = string.IsNullOrWhiteSpace(Apellido) ? "" : Apellido.Trim().Substring(0, 1);
                var n = string.IsNullOrWhiteSpace(Nombre) ? "" : Nombre.Trim().Substring(0, 1);
                var iniciales = (a + n).ToUpperInvariant();
                return iniciales.Length == 0 ? "?" : iniciales;
            }
        }
    }
}
