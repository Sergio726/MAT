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
using Newtonsoft.Json;
using MAT.MVC.Infrastructure;
using MAT.MVC.Infrastructure.Data;

namespace MAT.MVC.Controllers.Paquete
{
    public class PaqueteController : Controller
    {
        //
        // GET: /Paquete/

        public ActionResult Index(string msgerror)
        {
            List<PaqueteStandard> LResult = new List<PaqueteStandard>();
            try
            {
                LResult = PaqueteVinculos.ListPaqueteByYear();
            }
            catch (Exception e)
            {
                ViewData["error"] = e.Message.ToString();
            }
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
                    case "detail": Paquete = PaqueteVinculos.GetPaqueteByID(sPaqueteID);
                        ViewBag.InfoDestino = PaqueteVinculos.GetPaqueteDestino(sPaqueteID);
                        break;
                    case "edit": Paquete = PaqueteVinculos.GetPaqueteByID(sPaqueteID);
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
        public ActionResult Edit(Guid id, FormCollection form)
        {
            Entities.Paquete paqueteEdit = PaqueteDataAccess.GetPaqueteById(id);
            Helper.FillEntity<Entities.Paquete>(ref paqueteEdit, form);
            PaqueteDataAccess.UpdatePaquete(paqueteEdit);
            return RedirectToAction("Details", "Paquete", new { id = id });
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file, FormCollection form)
        {
            string serverpath = "/Images/Paquetes";
            Guid paqueteid = new Guid(Session["paqueteid"].ToString());
            if (file != null && file.ContentLength > 0)
                try
                {
                    string relativepath = string.Format("{0}/{1}.{2}", serverpath, Session["destinoid"], file.FileName.Split('.')[1]);
                    string path = Path.Combine(Server.MapPath(serverpath), Session["destinoid"] + "." + file.FileName.Split('.')[1]);
                    file.SaveAs(path);
                    Entities.Paquete paquete = PaqueteDataAccess.GetPaqueteById(paqueteid);
                    paquete.Foto = relativepath;
                    PaqueteDataAccess.UpdatePaquete(paquete);
                    ViewBag.Message = "Archivo cargado correctamente";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "Archivo no especificado.";
            }
            //return RedirectToAction("Edit", "Paquete", new { id = paqueteid });
            return RedirectToAction("Edit", "Paquete", new { sAction = "edit", sPaqueteID = paqueteid });
        }

        public JsonResult InsertPaquete(PaqueteStandard Paquete)
        {
            string[] sResult = new string[2];
            try
            {
                sResult = PaqueteVinculos.PaqueteInsert(Paquete);
            }
            catch (Exception e)
            {
                sResult[0] = "";
                // Legacy: devuelve Mensaje, mantenemos formato sin StackTrace
                sResult[1] = "Error: " + ErrorUtil.LogAndGetPublicMessage(e, "PaqueteController.InsertPaquete");
            }

            return Json(new
            {
                ID = sResult[0],
                Mensaje = sResult[1]
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdatePaquete(PaqueteStandard Paquete)
        {
            string[] sResult = new string[2];
            try
            {
                sResult = PaqueteVinculos.PaqueteUpdate(Paquete);
            }
            catch (Exception e)
            {
                sResult[0] = "";
                // Legacy: devuelve Mensaje, mantenemos formato sin StackTrace
                sResult[1] = "Error: " + ErrorUtil.LogAndGetPublicMessage(e, "PaqueteController.UpdatePaquete");
            }

            return Json(new
            {
                ID = sResult[0],
                Mensaje = sResult[1]
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(Guid id)
        {
            try
            {
                if (PaqueteDataAccess.CountViajesByPaqueteId(id) < 1)
                {
                    PaqueteDataAccess.DeletePaqueteCascade(id);
                    return RedirectToAction("Index", "Paquete");
                }
                else
                {
                    string msj = "El paquete que desea eliminar posee viajes vinculados. Elimine primero los viajes en el apartado Gestión de Viajes.";
                    return RedirectToAction("Index", "Paquete", new { msgerror = msj });
                }
            }
            catch (Exception e)
            {
                ErrorUtil.LogAndGetPublicMessage(e, "PaqueteController.Delete");
                string msj = "Error al eliminar paquete. Contacte con el Administrador de Sistema.";
                return RedirectToAction("Index", "Paquete", new { msgerror = msj });
            }
        }

        public ActionResult Vinculos(Guid id)
        {
            ViewBag.PaqueteID = id;
            #region excursiones
            List<MAT.MVC.Models.PaqueteExcursionCustomModel> lPaqueteExcursion = new List<PaqueteExcursionCustomModel>();
            DataSet ds = PaqueteExcursionMethod.GetByPaqueteID(id);
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                MAT.MVC.Models.PaqueteExcursionCustomModel o = new PaqueteExcursionCustomModel();
                o.PaqueteExcursionID = new Guid(item["PaqueteExcursionID"].ToString());
                o.IsOpcional = Convert.ToBoolean(item["IsOpcional"]);
                o.Descripcion = item["Descripcion"].ToString();

                lPaqueteExcursion.Add(o);
            }

            ViewBag.Excursiones = lPaqueteExcursion;
            #endregion

            List<PaqueteViculosModel> vinculosModel = ClassPaqueteVinculos.GetVinculosByPaqueteID(id);
            return View(vinculosModel);
        }

        #region Servicios
        public ActionResult Servicios(Guid id)
        {
            return PartialView(id);
        }
        public ActionResult RenderGridServicios(string filter, Guid id)
        {
            List<MAT.Entities.Servicio> list = ServicioMethod.GetAllEntities();
            #region Except
            List<MAT.Entities.Servicio> vinculados = new List<Entities.Servicio>();
            List<Entities.PaqueteServicio> vinculos = PaqueteDataAccess.GetPaqueteServiciosByPaqueteId(id);
            foreach (var item in vinculos)
            {
                vinculados.Add(MaestrosDataAccess.GetServicioById(item.ServicioId.Value));
            }
            list = list.Except(vinculados).ToList();
            #endregion
            if (!string.IsNullOrEmpty(filter))
            {
                list = list.Where(p => p.Descripcion.ToUpper().Contains(filter.ToUpper())).ToList();
            }
            return PartialView(list);
        }
        public string VincularServicio(Guid servicioid, Guid paqueteid)
        {
            try
            {
                Entities.PaqueteServicio paqueteservicio = new PaqueteServicio();
                paqueteservicio.PaqueteServicioId = Guid.NewGuid();
                paqueteservicio.PaqueteId = paqueteid;
                paqueteservicio.ServicioId = servicioid;
                PaqueteDataAccess.InsertPaqueteServicio(paqueteservicio);
                return "True";
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {

            }
            return "False";
        }
        public string DesvincularServicio(Guid servicioid, Guid paqueteid)
        {
            try
            {
                bool result = PaqueteDataAccess.DeletePaqueteServicio(servicioid, paqueteid);
                if (result) return "True";
                else return "False";
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {

            }
            return "False";
        }
        #endregion

        #region Excursiones
        public ActionResult Excursiones(Guid id)
        {
            return PartialView(id);
        }
        public ActionResult RenderGridExcursiones(Guid id)
        {
            DataSet ds = new DataSet();
            string json = "";
            try 
            {
                ds = MAT.MVC.Models.ExcursionMethod.GetToAdd(id);
                json = JsonConvert.SerializeObject(ds, Formatting.Indented);
                ViewBag.jResult = json;
                ViewBag.PaqueteID = id;
            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "PaqueteController.RenderGridExcursiones");
            }
            return PartialView();
        }
        public string VincularExcursion(Guid excursionid, Guid paqueteid, bool IsOpcional)
        {
            try
            {
                Entities.PaqueteExcursion paqueteexcursion = new PaqueteExcursion();
                paqueteexcursion.PaqueteId = paqueteid;
                MAT.MVC.Models.PaqueteExcursionCustomModel o = new MAT.MVC.Models.PaqueteExcursionCustomModel();
                o.ExcursionID = excursionid;
                o.PaqueteID = paqueteid;
                o.IsOpcional = IsOpcional;

                MAT.MVC.Models.PaqueteExcursionMethod.InsertNew(o);

                return "True";
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {

            }
            return "False";
        }
        public string DesvincularExcursion(Guid PaqueteExcursionID)
        {
            try
            {
                MAT.MVC.Models.PaqueteExcursionMethod.Delete(PaqueteExcursionID);
                
               return "True";
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {

            }
            return "False";
        }
        #endregion

        #region Precios
        public ActionResult Precios(Guid id)
        {
            return PartialView(id);
        }
        public ActionResult RenderGridPrecios(string filter, Guid id)
        {
            List<MAT.Entities.Precio> list = PaqueteDataAccess.GetAllPrecios();
            #region Except
            List<MAT.Entities.Precio> vinculados = new List<Entities.Precio>();
            List<Entities.PaquetePrecio> vinculos = PaqueteDataAccess.GetPaquetePreciosByPaqueteId(id);
            foreach (var item in vinculos)
            {
                vinculados.Add(PaqueteDataAccess.GetPrecioById(item.PrecioId.Value));
            }
            list = list.Except(vinculados).ToList();
            #endregion
            if (!string.IsNullOrEmpty(filter))
            {
                list = list.Where(p => p.Descripcion.ToUpper().Contains(filter.ToUpper())).ToList();
            }
            return PartialView(list);
        }

        public string VincularPrecio(Guid precioid, Guid paqueteid)
        {
            try
            {
                Entities.PaquetePrecio paqueteprecio = new Entities.PaquetePrecio();
                paqueteprecio.PaquetePrecioId = Guid.NewGuid();
                paqueteprecio.PaqueteId = paqueteid;
                paqueteprecio.PrecioId = precioid;
                PaqueteDataAccess.InsertPaquetePrecio(paqueteprecio);
                return "True";
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {

            }
            return "False";
        }
        public string DesvincularPrecio(Guid precioid, Guid paqueteid)
        {
            try
            {
                bool result = PaqueteDataAccess.DeletePaquetePrecio(precioid, paqueteid);
                if (result) return "True";
                else return "False";
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {

            }
            return "False";
        }
        #endregion

        #region Adicionales
        public ActionResult Adicionales(Guid id)
        {
            return PartialView(id);
        }
        public ActionResult RenderGridAdicionales(string filter, Guid id)
        {
            List<MAT.Entities.Adicional> list = MaestrosDataAccess.GetAllAdicionales();
            #region Except
            List<MAT.Entities.Adicional> vinculados = new List<Entities.Adicional>();
            List<Entities.PaqueteAdicional> vinculos = PaqueteDataAccess.GetPaqueteAdicionalesByPaqueteId(id);
            foreach (var item in vinculos)
            {
                vinculados.Add(MaestrosDataAccess.GetAdicionalById(item.AdicionalId.Value));
            }
            list = list.Except(vinculados).ToList();
            #endregion
            if (!string.IsNullOrEmpty(filter))
            {
                list = list.Where(p => p.Descripcion.ToUpper().Contains(filter.ToUpper())).ToList();
            }
            return PartialView(list);
        }

        public string VincularAdicional(Guid adicionalid, Guid paqueteid)
        {
            try
            {
                Entities.PaqueteAdicional paqueteadicional = new PaqueteAdicional();
                paqueteadicional.PaqueteAdicionalId = Guid.NewGuid();
                paqueteadicional.PaqueteId = paqueteid;
                paqueteadicional.AdicionalId = adicionalid;
                PaqueteDataAccess.InsertPaqueteAdicional(paqueteadicional);
                return "True";
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {

            }
            return "False";
        }
        public string DesvincularAdicional(Guid adicionalid, Guid paqueteid)
        {
            try
            {
                bool result = PaqueteDataAccess.DeletePaqueteAdicional(adicionalid, paqueteid);
                if (result) return "True";
                else return "False";
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {

            }
            return "False";
        }
        #endregion


    }
}
