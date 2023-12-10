using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MAT.MVC.Controllers.Herramientas
{
    public class HerramientasController : Controller
    {
        //
        // GET: /Herramientas/

        //[Authorize]
        //public ActionResult Cotizador(string sMode = "0")
        //{
        //    ViewBag.Mode = sMode;
        //    return PartialView();
        //}

        [Authorize]
        public ActionResult Cotizador(string sMode = "0")
        {
            ViewBag.Mode = sMode;
            return PartialView();
        }

    }
}
