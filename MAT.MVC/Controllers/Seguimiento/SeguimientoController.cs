using System;
using System.Linq;
using System.Web.Mvc;
using MAT.Enums;
using MAT.MVC.Common;
using MAT.MVC.Infrastructure;
using MAT.MVC.Infrastructure.Data;

namespace MAT.MVC.Controllers.Seguimiento
{
    /// <summary>
    /// Seguimiento de clientes para vendedores: cumpleaños del día con saludo por WhatsApp, listas por
    /// categoría, ficha del cliente y registro de contactos. Todos los vendedores ven todos los clientes.
    /// </summary>
    [Authorize]
    public class SeguimientoController : Controller
    {
        private const int DiasCumpleaniosMaximo = 60;

        public ActionResult Index()
        {
            ViewBag.Categorias = SeguimientoCatalog.GetCategorias();
            ViewBag.Resultados = SeguimientoCatalog.GetResultados();
            return View();
        }

        /// <summary>
        /// Cumpleaños de hoy (<paramref name="dias"/> = 0) o de los próximos N días, con el saludo armado para WhatsApp.
        /// </summary>
        [HttpGet]
        public ActionResult Cumpleanios(int dias = 0)
        {
            try
            {
                dias = Math.Max(0, Math.Min(dias, DiasCumpleaniosMaximo));
                var firma = GetFirmaVendedor();
                var list = SeguimientoDataAccess.GetCumpleanios(dias);
                foreach (var cliente in list)
                {
                    SeguimientoCatalog.Completar(cliente, SeguimientoCatalog.ArmarSaludoCumpleanios(cliente, firma));
                }
                return JsonCamelCaseHelper.Serialize(new { success = true, data = list, total = list.Count });
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "SeguimientoController.Cumpleanios");
                return JsonCamelCaseHelper.Serialize(new { success = false, message = msg });
            }
        }

        [HttpGet]
        public ActionResult Resumen()
        {
            try
            {
                var cantidades = SeguimientoDataAccess.GetResumenCategorias();
                var categorias = SeguimientoCatalog.GetCategorias();
                foreach (var categoria in categorias)
                {
                    int cantidad;
                    categoria.Cantidad = cantidades.TryGetValue(categoria.Codigo, out cantidad) ? cantidad : 0;
                }
                return JsonCamelCaseHelper.Serialize(new { success = true, data = categorias });
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "SeguimientoController.Resumen");
                return JsonCamelCaseHelper.Serialize(new { success = false, message = msg });
            }
        }

        /// <summary>
        /// Clientes de una categoría y/o que coinciden con el texto (apellido, nombre o DNI).
        /// </summary>
        [HttpGet]
        public ActionResult Clientes(string categoria = null, string texto = null)
        {
            try
            {
                categoria = string.IsNullOrWhiteSpace(categoria) ? null : categoria.Trim();
                texto = string.IsNullOrWhiteSpace(texto) ? null : texto.Trim();

                if (categoria != null && !SeguimientoCatalog.EsCategoriaValida(categoria))
                    return JsonCamelCaseHelper.Serialize(new { success = false, message = "Categoría no válida." });
                if (categoria == null && (texto == null || texto.Length < 2))
                    return JsonCamelCaseHelper.Serialize(new { success = false, message = "Elegí una categoría o escribí al menos 2 letras para buscar." });

                var list = SeguimientoDataAccess.GetClientes(categoria, texto);
                foreach (var cliente in list)
                {
                    SeguimientoCatalog.Completar(cliente);
                }
                return JsonCamelCaseHelper.Serialize(new { success = true, data = list, total = list.Count });
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "SeguimientoController.Clientes");
                return JsonCamelCaseHelper.Serialize(new { success = false, message = msg });
            }
        }

        [HttpGet]
        public ActionResult Ficha(Guid id)
        {
            try
            {
                var ficha = SeguimientoDataAccess.GetFicha(id);
                if (ficha == null)
                    return JsonCamelCaseHelper.Serialize(new { success = false, message = "No se encontró el cliente." });

                SeguimientoCatalog.Completar(ficha.Cliente);
                foreach (var contacto in ficha.Contactos)
                {
                    contacto.ResultadoNombre = SeguimientoCatalog.NombreResultado(contacto.Resultado);
                }
                return JsonCamelCaseHelper.Serialize(new { success = true, data = ficha });
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "SeguimientoController.Ficha");
                return JsonCamelCaseHelper.Serialize(new { success = false, message = msg });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarContacto(Guid clienteId, int resultado, string observacion = null)
        {
            try
            {
                if (!Enum.IsDefined(typeof(eResultadoContacto), resultado))
                    return JsonCamelCaseHelper.Serialize(new { success = false, message = "Elegí el resultado del contacto." });

                observacion = string.IsNullOrWhiteSpace(observacion) ? null : observacion.Trim();
                if (observacion != null && observacion.Length > 500) observacion = observacion.Substring(0, 500);

                SeguimientoDataAccess.InsertContacto(clienteId, GetVendedorId(), User.Identity.Name, resultado, observacion);
                return JsonCamelCaseHelper.Serialize(new
                {
                    success = true,
                    message = "Contacto registrado: " + SeguimientoCatalog.NombreResultado(resultado) + "."
                });
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "SeguimientoController.RegistrarContacto");
                return JsonCamelCaseHelper.Serialize(new { success = false, message = msg });
            }
        }

        private static Guid? GetVendedorId()
        {
            try
            {
                return MATContext.CurrentVendedor?.VendedorId;
            }
            catch
            {
                // Usuario sin vendedor asociado (p. ej. administrador): el contacto queda a nombre del usuario.
                return null;
            }
        }

        /// <summary>
        /// Nombre del vendedor para firmar el saludo; <c>Vendedor.Descripcion</c> está vacío, el nombre vive en Persona.
        /// </summary>
        private static string GetFirmaVendedor()
        {
            var vendedorId = GetVendedorId();
            if (!vendedorId.HasValue) return null;
            var persona = PersonaDataAccess.GetById(vendedorId.Value);
            return persona == null ? null : SeguimientoCatalog.NombreParaFirma(persona.Nombre, persona.Apellido);
        }
    }
}
