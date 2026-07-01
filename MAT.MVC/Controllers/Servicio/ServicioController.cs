using MAT.MVC.Infrastructure;
using MAT.MVC.Models;
using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace MAT.MVC.Controllers.Servicio
{
    public class ServicioController : Controller
    {
        public ActionResult Index()
        {
            List<ServicioModel> listSevicios = new List<ServicioModel>();
            try
            {
                DataSet ds = ServicioMethod.ServiciosGetAll();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ServicioModel item = new ServicioModel();
                    item.ServicioID = row["ServicioID"].ToString();
                    item.Descripcion = row["Descripcion"].ToString();
                    item.Precio = Convert.ToDecimal(row["Precio"].ToString());
                    item.Moneda = row["Moneda"].ToString();
                    listSevicios.Add(item);
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "ServicioController.Index");
            }
            return View(listSevicios);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection datos)
        {
            try
            {
                ServicioModel serv = new ServicioModel();

                serv.Descripcion = datos["Descripcion"];
                if (datos["Precio"] != "")
                {
                    serv.Precio = Convert.ToDecimal(datos["Precio"]);
                }
                else
                {
                    serv.Precio = 0;
                }
                serv.Moneda = datos["Moneda"];
                serv.TipoServicio = 2;
                serv.ProveedorID = datos["ProveedorId"];
                serv.TransporteID = datos["TransporteId"];

                ServicioMethod.ServicioInsert(serv);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "ServicioController.Create");
                return View();
            }
        }

        public ActionResult Edit(Guid id)
        {
            try
            {
                var servicio = ServicioMethod.GetEntityById(id);
                if (servicio == null)
                {
                    TempData["Error"] = "El servicio solicitado no existe.";
                    return RedirectToAction("Index");
                }

                return View(servicio);
            }
            catch (Exception e)
            {
                TempData["Error"] = ErrorUtil.LogAndGetPublicMessage(e, "ServicioController.Edit");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection datos)
        {
            MAT.Entities.Servicio servicio = null;
            try
            {
                servicio = ServicioMethod.GetEntityById(id);
                if (servicio == null)
                {
                    TempData["Error"] = "El servicio solicitado no existe.";
                    return RedirectToAction("Index");
                }

                Helper.FillEntity(ref servicio, datos);
                ServicioMethod.UpdateServicio(servicio);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "ServicioController.Edit");
                return View(servicio ?? new MAT.Entities.Servicio());
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                ServicioMethod.DeleteServicio(id);
                return true;
            }
            catch (Exception ex)
            {
                ErrorUtil.LogAndGetPublicMessage(ex, "ServicioController.Delete");
                return false;
            }
        }
    }
}
