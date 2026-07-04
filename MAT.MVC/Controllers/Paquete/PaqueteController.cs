using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.Utilities;
using MAT.Entities;
using MAT.MVC.Models;
using System.IO;
using System.Data;
using MAT.MVC.Infrastructure;
using MAT.MVC.Infrastructure.Data;

namespace MAT.MVC.Controllers.Paquete
{
    public class PaqueteController : Controller
    {
        //
        // GET: /Paquete/

        public ActionResult Index(string year, string search, int? temporada, int? moneda, string msgerror)
        {
            // Por defecto se muestran los paquetes del anio actual; "0" = Todos.
            string selectedYear = string.IsNullOrWhiteSpace(year) ? DateTime.Now.Year.ToString() : year;
            string yearParam = selectedYear == "0" ? "" : selectedYear;

            List<PaqueteStandard> LResult = new List<PaqueteStandard>();
            try
            {
                LResult = PaqueteVinculos.ListPaqueteByYear(yearParam, search, temporada, moneda);
            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "PaqueteController.Index");
            }

            if (!string.IsNullOrEmpty(msgerror))
            {
                ViewBag.Error = msgerror;
            }

            var currentYear = DateTime.Now.Year;
            ViewBag.Years = Enumerable.Range(0, 7).Select(i => currentYear - i).ToList();
            ViewBag.SelectedYear = selectedYear;
            ViewBag.Search = search;
            ViewBag.SelectedTemporada = temporada;
            ViewBag.SelectedMoneda = moneda;
            ViewBag.Monedas = MonedaMethod.GetMonedaTipoAll();

            return View(LResult);
        }

        [Authorize]
        public ActionResult Edit(string sAction, string sPaqueteID)
        {
            Session["paqueteid"] = sPaqueteID;

            try
            {
                ViewBag.Action = sAction;
                ViewBag.ListPais = GeoDataAccess.GetAllPaises();

                PaqueteStandard Paquete = new PaqueteStandard();
                switch (sAction)
                {
                    case "detail":
                        Paquete = PaqueteVinculos.GetPaqueteByID(sPaqueteID);
                        ViewBag.InfoDestino = PaqueteVinculos.GetPaqueteDestino(sPaqueteID);
                        break;
                    case "edit":
                        Paquete = PaqueteVinculos.GetPaqueteByID(sPaqueteID);
                        ViewBag.InfoDestino = PaqueteVinculos.GetPaqueteDestino(sPaqueteID);
                        break;
                    case "new":
                        Paquete = new PaqueteStandard();
                        Paquete.Moneda = 1;
                        Paquete.Temporada = "1";
                        break;
                }
                return View(Paquete);
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "PaqueteController.Edit");
                return RedirectToAction("Index", "Paquete", new { msgerror = msg });
            }
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file, FormCollection form)
        {
            string serverpath = "/Images/Paquetes";
            Guid paqueteid = new Guid(Session["paqueteid"].ToString());
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    string relativepath = string.Format("{0}/{1}.{2}", serverpath, Session["destinoid"], file.FileName.Split('.')[1]);
                    string path = Path.Combine(Server.MapPath(serverpath), Session["destinoid"] + "." + file.FileName.Split('.')[1]);
                    file.SaveAs(path);
                    Entities.Paquete paquete = PaqueteDataAccess.GetPaqueteById(paqueteid);
                    paquete.Foto = relativepath;
                    PaqueteDataAccess.UpdatePaquete(paquete);
                    TempData["Success"] = "Imagen cargada correctamente.";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ErrorUtil.LogAndGetPublicMessage(ex, "PaqueteController.UploadFile");
                }
            }
            else
            {
                TempData["Error"] = "Archivo no especificado.";
            }
            return RedirectToAction("Edit", "Paquete", new { sAction = "edit", sPaqueteID = paqueteid });
        }

        public JsonResult InsertPaquete(PaqueteStandard Paquete)
        {
            try
            {
                string[] r = PaqueteVinculos.PaqueteInsert(Paquete);
                bool ok = r[1] == "Done.";
                return Json(new
                {
                    success = ok,
                    id = r[0],
                    message = ok ? "Paquete creado correctamente." : "No se pudo guardar el paquete."
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "PaqueteController.InsertPaquete");
                return Json(new { success = false, id = "", message = msg }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdatePaquete(PaqueteStandard Paquete)
        {
            try
            {
                string[] r = PaqueteVinculos.PaqueteUpdate(Paquete);
                bool ok = r[1] == "Done.";
                return Json(new
                {
                    success = ok,
                    id = r[0],
                    message = ok ? "Paquete actualizado correctamente." : "No se pudo guardar el paquete."
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "PaqueteController.UpdatePaquete");
                return Json(new { success = false, id = "", message = msg }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Delete(Guid id)
        {
            try
            {
                if (PaqueteDataAccess.CountViajesByPaqueteId(id) >= 1)
                {
                    return Json(new
                    {
                        success = false,
                        message = "El paquete posee viajes vinculados. Elimine primero los viajes en Gestion de Viajes."
                    });
                }

                PaqueteDataAccess.DeletePaqueteCascade(id);
                return Json(new { success = true, message = "Paquete eliminado correctamente." });
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "PaqueteController.Delete");
                return Json(new { success = false, message = msg });
            }
        }

        public ActionResult Vinculos(Guid id)
        {
            try
            {
                var paquete = PaqueteVinculos.GetPaqueteByID(id.ToString());
                ViewBag.PaqueteID = id;
                ViewBag.PaqueteDescripcion = paquete != null ? paquete.Descripcion : "";
                List<PaqueteViculosModel> vinculosModel = ClassPaqueteVinculos.GetVinculosByPaqueteID(id);
                return View(vinculosModel);
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "PaqueteController.Vinculos");
                return RedirectToAction("Index", "Paquete", new { msgerror = msg });
            }
        }

        // Contenido de las secciones de vinculos para refresco parcial (AJAX) sin recargar la pagina.
        public ActionResult VinculosContent(Guid id)
        {
            ViewBag.PaqueteID = id;
            List<PaqueteViculosModel> vinculosModel = ClassPaqueteVinculos.GetVinculosByPaqueteID(id);
            return PartialView("_PaqueteVinculosSections", vinculosModel);
        }

        #region Servicios
        public ActionResult RenderGridServicios(string filter, Guid id)
        {
            try
            {
                var list = PaqueteDataAccess.GetAvailableServicios(id, filter)
                    .Select(s => new PaqueteVincularItem { Id = s.ServicioId.ToString(), Descripcion = s.Descripcion })
                    .ToList();
                ViewBag.Tipo = "servicio";
                return PartialView("_GridVincular", list);
            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "PaqueteController.RenderGridServicios");
                ViewBag.Tipo = "servicio";
                return PartialView("_GridVincular", new List<PaqueteVincularItem>());
            }
        }

        [HttpPost]
        public JsonResult VincularServicio(Guid servicioid, Guid paqueteid)
        {
            try
            {
                Entities.PaqueteServicio paqueteservicio = new PaqueteServicio
                {
                    PaqueteServicioId = Guid.NewGuid(),
                    PaqueteId = paqueteid,
                    ServicioId = servicioid
                };
                PaqueteDataAccess.InsertPaqueteServicio(paqueteservicio);
                return Json(new { success = true, message = "Servicio vinculado." });
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "PaqueteController.VincularServicio");
                return Json(new { success = false, message = msg });
            }
        }

        [HttpPost]
        public JsonResult DesvincularServicio(Guid servicioid, Guid paqueteid)
        {
            try
            {
                bool result = PaqueteDataAccess.DeletePaqueteServicio(servicioid, paqueteid);
                return Json(new { success = result, message = result ? "Servicio desvinculado." : "No se pudo desvincular el servicio." });
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "PaqueteController.DesvincularServicio");
                return Json(new { success = false, message = msg });
            }
        }
        #endregion

        #region Excursiones
        public ActionResult RenderGridExcursiones(string filter, Guid id)
        {
            try
            {
                DataSet ds = MAT.MVC.Models.ExcursionMethod.GetToAdd(id);
                var list = new List<PaqueteVincularItem>();
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var descripcion = row["Descripcion"].ToString();
                        if (!string.IsNullOrEmpty(filter) && descripcion.IndexOf(filter, StringComparison.OrdinalIgnoreCase) < 0)
                        {
                            continue;
                        }
                        list.Add(new PaqueteVincularItem
                        {
                            Id = row["ExcursionID"].ToString(),
                            Descripcion = descripcion
                        });
                    }
                }
                ViewBag.Tipo = "excursion";
                return PartialView("_GridVincular", list);
            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "PaqueteController.RenderGridExcursiones");
                ViewBag.Tipo = "excursion";
                return PartialView("_GridVincular", new List<PaqueteVincularItem>());
            }
        }

        [HttpPost]
        public JsonResult VincularExcursion(Guid excursionid, Guid paqueteid, bool IsOpcional)
        {
            try
            {
                var o = new MAT.MVC.Models.PaqueteExcursionCustomModel
                {
                    ExcursionID = excursionid,
                    PaqueteID = paqueteid,
                    IsOpcional = IsOpcional
                };
                MAT.MVC.Models.PaqueteExcursionMethod.InsertNew(o);
                return Json(new { success = true, message = "Excursion vinculada." });
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "PaqueteController.VincularExcursion");
                return Json(new { success = false, message = msg });
            }
        }

        [HttpPost]
        public JsonResult DesvincularExcursion(Guid PaqueteExcursionID)
        {
            try
            {
                MAT.MVC.Models.PaqueteExcursionMethod.Delete(PaqueteExcursionID);
                return Json(new { success = true, message = "Excursion desvinculada." });
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "PaqueteController.DesvincularExcursion");
                return Json(new { success = false, message = msg });
            }
        }
        #endregion

        #region Precios
        public ActionResult RenderGridPrecios(string filter, Guid id)
        {
            try
            {
                var list = PaqueteDataAccess.GetAvailablePrecios(id, filter)
                    .Select(p => new PaqueteVincularItem { Id = p.PrecioId.ToString(), Descripcion = p.Descripcion })
                    .ToList();
                ViewBag.Tipo = "precio";
                return PartialView("_GridVincular", list);
            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "PaqueteController.RenderGridPrecios");
                ViewBag.Tipo = "precio";
                return PartialView("_GridVincular", new List<PaqueteVincularItem>());
            }
        }

        [HttpPost]
        public JsonResult VincularPrecio(Guid precioid, Guid paqueteid)
        {
            try
            {
                Entities.PaquetePrecio paqueteprecio = new Entities.PaquetePrecio
                {
                    PaquetePrecioId = Guid.NewGuid(),
                    PaqueteId = paqueteid,
                    PrecioId = precioid
                };
                PaqueteDataAccess.InsertPaquetePrecio(paqueteprecio);
                return Json(new { success = true, message = "Precio vinculado." });
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "PaqueteController.VincularPrecio");
                return Json(new { success = false, message = msg });
            }
        }

        [HttpPost]
        public JsonResult DesvincularPrecio(Guid precioid, Guid paqueteid)
        {
            try
            {
                bool result = PaqueteDataAccess.DeletePaquetePrecio(precioid, paqueteid);
                return Json(new { success = result, message = result ? "Precio desvinculado." : "No se pudo desvincular el precio." });
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "PaqueteController.DesvincularPrecio");
                return Json(new { success = false, message = msg });
            }
        }
        #endregion

        #region Adicionales
        public ActionResult RenderGridAdicionales(string filter, Guid id)
        {
            try
            {
                var list = PaqueteDataAccess.GetAvailableAdicionales(id, filter)
                    .Select(a => new PaqueteVincularItem { Id = a.AdicionalId.ToString(), Descripcion = a.Descripcion })
                    .ToList();
                ViewBag.Tipo = "adicional";
                return PartialView("_GridVincular", list);
            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "PaqueteController.RenderGridAdicionales");
                ViewBag.Tipo = "adicional";
                return PartialView("_GridVincular", new List<PaqueteVincularItem>());
            }
        }

        [HttpPost]
        public JsonResult VincularAdicional(Guid adicionalid, Guid paqueteid)
        {
            try
            {
                Entities.PaqueteAdicional paqueteadicional = new PaqueteAdicional
                {
                    PaqueteAdicionalId = Guid.NewGuid(),
                    PaqueteId = paqueteid,
                    AdicionalId = adicionalid
                };
                PaqueteDataAccess.InsertPaqueteAdicional(paqueteadicional);
                return Json(new { success = true, message = "Adicional vinculado." });
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "PaqueteController.VincularAdicional");
                return Json(new { success = false, message = msg });
            }
        }

        [HttpPost]
        public JsonResult DesvincularAdicional(Guid adicionalid, Guid paqueteid)
        {
            try
            {
                bool result = PaqueteDataAccess.DeletePaqueteAdicional(adicionalid, paqueteid);
                return Json(new { success = result, message = result ? "Adicional desvinculado." : "No se pudo desvincular el adicional." });
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "PaqueteController.DesvincularAdicional");
                return Json(new { success = false, message = msg });
            }
        }
        #endregion
    }
}
