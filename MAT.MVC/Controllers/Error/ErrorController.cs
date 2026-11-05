using System.Web.Mvc;

namespace MAT.MVC.Controllers.Error
{
    /// <summary>
    /// Página propia para direcciones que no existen (módulo Clientes F5). Hoy se ve una pantalla amarilla de ASP.NET
    /// en local y un error genérico de IIS en el servidor. Se activa con <c>httpErrors</c> en Web.config
    /// (ver docs/PENDIENTES.md, F5); mientras tanto responde en /Error/NoEncontrado.
    /// </summary>
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        public ActionResult NoEncontrado()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
    }
}
