using System;
using System.Linq;
using System.Web.Mvc;
using MAT.MVC.Infrastructure;
using MAT.MVC.Infrastructure.Data;

namespace MAT.MVC.Controllers.Notificaciones
{
    /// <summary>
    /// Campanita de avisos de la barra superior (viajes próximos, últimos lugares, viajes completos y cumpleaños).
    /// Cada usuario marca los suyos como leídos; queda guardado en la base, no en el navegador.
    /// </summary>
    [Authorize]
    public class NotificacionesController : Controller
    {
        private const int Cantidad = 30;

        [HttpGet]
        public ActionResult Lista()
        {
            try
            {
                NotificacionDataAccess.GenerarSiCorresponde();
                int noLeidas;
                var avisos = NotificacionDataAccess.ParaUsuario(User.Identity.Name, Cantidad, out noLeidas);
                return JsonCamelCaseHelper.Serialize(new { success = true, noLeidas, data = avisos });
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "NotificacionesController.Lista");
                return JsonCamelCaseHelper.Serialize(new { success = false, message = msg });
            }
        }

        /// <param name="ids">IDs separados por coma; vacío = marcar todas.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MarcarLeidas(string ids = null)
        {
            try
            {
                var lista = string.IsNullOrWhiteSpace(ids) ? null : ids.Split(',')
                    .Select(s => { int n; return int.TryParse(s.Trim(), out n) ? n : 0; })
                    .Where(n => n > 0).ToList();
                NotificacionDataAccess.MarcarLeidas(User.Identity.Name, lista);
                return JsonCamelCaseHelper.Serialize(new { success = true });
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "NotificacionesController.MarcarLeidas");
                return JsonCamelCaseHelper.Serialize(new { success = false, message = msg });
            }
        }
    }
}
