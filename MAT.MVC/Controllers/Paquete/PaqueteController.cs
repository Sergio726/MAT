using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.Utilities;
using MAT.Entities;
using MAT.Services;
using MAT.MVC.Models;
using System.IO;
using System.Data;
using Newtonsoft.Json;

namespace MAT.MVC.Controllers.Paquete
{
    public class PaqueteController : Controller
    {
        PaqueteService paqueteService;
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
                
                PaisService PaisService = new Services.PaisService();
                List<Pais> LPais = new List<Pais>();
                LPais = PaisService.GetAll().ToList();
                ViewBag.ListPais = LPais;


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
            catch {
                return RedirectToAction("Index", "Paquete");
            }
            
        }

        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection form)
        {
            paqueteService = new PaqueteService();
            Entities.Paquete paqueteEdit = paqueteService.GetByPaqueteId(id);
            Helper.FillEntity<Entities.Paquete>(ref paqueteEdit, form);
            paqueteService.Update(paqueteEdit);
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
                    PaqueteService paqServ = new PaqueteService();
                    Entities.Paquete paquete = paqServ.GetByPaqueteId(paqueteid);
                    paquete.Foto = relativepath;
                    paqServ.Update(paquete);
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
                sResult[2] = "Error: " + e.Message + "StackTrace: " + e.StackTrace;
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
                sResult[2] = "Error: " + e.Message + "StackTrace: " + e.StackTrace;
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
                if (new Services.ViajeService().GetByPaqueteId(id).Count < 1)
                {
                    PaqueteService spaquete = new PaqueteService();
                    MAT.Entities.Paquete EPaquete = spaquete.GetByPaqueteId(id);
                    Services.PaqueteServicioService paqueteservicioService = new PaqueteServicioService();
                    List<Entities.PaqueteServicio> servicios = paqueteservicioService.GetByPaqueteId(id).ToList();
                    for (int i = servicios.Count - 1; i > -1; i--)
                    {
                        var item = servicios[i];
                        paqueteservicioService.Delete(item.PaqueteServicioId);
                    }
                    Services.PaqueteExcursionService paqueteexcursionService = new PaqueteExcursionService();
                    List<Entities.PaqueteExcursion> excursiones = paqueteexcursionService.GetByPaqueteId(id).ToList();
                    for (int i = excursiones.Count - 1; i > -1; i--)
                    {
                        var item = excursiones[i];
                        paqueteexcursionService.Delete(item.PaqueteExcursionId);
                    }
                    Services.PaquetePrecioService paqueteprecioService = new PaquetePrecioService();
                    List<Entities.PaquetePrecio> precios = paqueteprecioService.GetByPaqueteId(id).ToList();
                    for (int i = precios.Count - 1; i > -1; i--)
                    {
                        var item = precios[i];
                        paqueteprecioService.Delete(item.PaquetePrecioId);
                    }
                    Services.PaqueteAdicionalService paqueteadicionalService = new PaqueteAdicionalService();
                    List<Entities.PaqueteAdicional> adicionales = paqueteadicionalService.GetByPaqueteId(id).ToList();
                    for (int i = adicionales.Count - 1; i > -1; i--)
                    {
                        var item = adicionales[i];
                        paqueteadicionalService.Delete(item.PaqueteAdicionalId);
                    }
                    spaquete.Delete(EPaquete);
                    return RedirectToAction("Index", "Paquete");
                }
                else
                {
                    string msj = "El paquete que desea eliminar posee viajes vinculados. Elimine primero los viajes en el apartado Gestión de Viajes.";
                    return RedirectToAction("Index", "Paquete", new { msgerror = msj });
                }
            }
            catch
            {
                string msj = "Error al eliminar paquete. Contacte con el Administrador de Sistema.";
                return RedirectToAction("Index", "Paquete", new { msgerror = msj });
            }
#pragma warning disable CS0162 // Unreachable code detected
            return RedirectToAction("Index", "Paquete");
#pragma warning restore CS0162 // Unreachable code detected
        }

        public ActionResult Vinculos(Guid id)
        {
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
            ServicioService service = new ServicioService();
            List<MAT.Entities.Servicio> list = service.GetAll().ToList();
            #region Except
            List<MAT.Entities.Servicio> vinculados = new List<Entities.Servicio>();
            List<Entities.PaqueteServicio> vinculos = new PaqueteServicioService().GetByPaqueteId(id).ToList();
            foreach (var item in vinculos)
            {
                vinculados.Add(service.GetByServicioId(item.ServicioId.Value));
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
                PaqueteServicioService psService = new PaqueteServicioService();
                Entities.PaqueteServicio paqueteservicio = new PaqueteServicio();
                paqueteservicio.PaqueteServicioId = Guid.NewGuid();
                paqueteservicio.PaqueteId = paqueteid;
                paqueteservicio.ServicioId = servicioid;
                psService.Insert(paqueteservicio);
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
                PaqueteServicioService psService = new PaqueteServicioService();
                Entities.PaqueteServicio paqueteservicio = psService.GetAll().Where(ps => ps.ServicioId.Value == servicioid && ps.PaqueteId.Value == paqueteid).FirstOrDefault();
                bool result = psService.Delete(paqueteservicio.PaqueteServicioId);
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
                ViewBag.Error = e.Message + e.StackTrace;
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
            PrecioService service = new PrecioService();
            List<MAT.Entities.Precio> list = service.GetAll().ToList();
            #region Except
            List<MAT.Entities.Precio> vinculados = new List<Entities.Precio>();
            List<Entities.PaquetePrecio> vinculos = new PaquetePrecioService().GetByPaqueteId(id).ToList();
            foreach (var item in vinculos)
            {
                vinculados.Add(service.GetByPrecioId(item.PrecioId.Value));
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
                PaquetePrecioService psService = new PaquetePrecioService();
                Entities.PaquetePrecio paqueteprecio = new Entities.PaquetePrecio();
                paqueteprecio.PaquetePrecioId = Guid.NewGuid();
                paqueteprecio.PaqueteId = paqueteid;
                paqueteprecio.PrecioId = precioid;
                psService.Insert(paqueteprecio);
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
                PaquetePrecioService psService = new PaquetePrecioService();
                Entities.PaquetePrecio paqueteprecio = psService.GetAll().Where(ps => ps.PrecioId.Value == precioid && ps.PaqueteId.Value == paqueteid).FirstOrDefault();
                bool result = psService.Delete(paqueteprecio.PaquetePrecioId);
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
            AdicionalService service = new AdicionalService();
            List<MAT.Entities.Adicional> list = service.GetAll().ToList();
            #region Except
            List<MAT.Entities.Adicional> vinculados = new List<Entities.Adicional>();
            List<Entities.PaqueteAdicional> vinculos = new PaqueteAdicionalService().GetByPaqueteId(id).ToList();
            foreach (var item in vinculos)
            {
                vinculados.Add(service.GetByAdicionalId(item.AdicionalId.Value));
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
                PaqueteAdicionalService psService = new PaqueteAdicionalService();
                Entities.PaqueteAdicional paqueteadicional = new PaqueteAdicional();
                paqueteadicional.PaqueteAdicionalId = Guid.NewGuid();
                paqueteadicional.PaqueteId = paqueteid;
                paqueteadicional.AdicionalId = adicionalid;
                psService.Insert(paqueteadicional);
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
                PaqueteAdicionalService psService = new PaqueteAdicionalService();
                Entities.PaqueteAdicional paqueteprecio = psService.GetAll().Where(ps => ps.AdicionalId.Value == adicionalid && ps.PaqueteId.Value == paqueteid).FirstOrDefault();
                bool result = psService.Delete(paqueteprecio.PaqueteAdicionalId);
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
