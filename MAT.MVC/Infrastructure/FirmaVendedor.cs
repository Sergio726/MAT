using System;
using MAT.MVC.Common;
using MAT.MVC.Infrastructure.Data;

namespace MAT.MVC.Infrastructure
{
    /// <summary>
    /// Vendedor logueado y su nombre para firmar mensajes de WhatsApp.
    /// <c>Vendedor.Descripcion</c> está vacío en producción: el nombre vive en <c>Persona</c>.
    /// </summary>
    public static class FirmaVendedor
    {
        public static Guid? VendedorIdActual()
        {
            try
            {
                return MATContext.CurrentVendedor?.VendedorId;
            }
            catch
            {
                // Usuario sin vendedor asociado (p. ej. administrador).
                return null;
            }
        }

        /// <summary>
        /// "Nombre Apellido" del vendedor logueado, o null si no tiene vendedor asociado.
        /// </summary>
        public static string NombreActual()
        {
            var vendedorId = VendedorIdActual();
            if (!vendedorId.HasValue) return null;
            var persona = PersonaDataAccess.GetById(vendedorId.Value);
            return persona == null ? null : SeguimientoCatalog.NombreParaFirma(persona.Nombre, persona.Apellido);
        }
    }
}
