using System.Net;
using System.Web.Mvc;

namespace MAT.MVC.Infrastructure
{
    /// <summary>
    /// Solo acepta pedidos AJAX del propio sitio (cabecera X-Requested-With, que jQuery agrega en $.ajax).
    /// Se usa junto con [HttpPost] en las acciones que escriben datos y se llaman por AJAX (2026-11-07): antes
    /// respondían a un GET, así que un enlace o una imagen en otra página podían dispararlas con la sesión del
    /// usuario (CSRF). Un formulario o un enlace de otro sitio no pueden poner esa cabecera sin que el navegador
    /// lo bloquee, así que esto cumple el papel del token antifalsificación para estas llamadas.
    /// </summary>
    public sealed class SoloAjaxAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Solo se acepta desde la aplicación.");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
