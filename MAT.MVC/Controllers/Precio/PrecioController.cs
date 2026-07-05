using MAT.MVC.Infrastructure;
using MAT.MVC.Infrastructure.Data;
using MAT.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAT.MVC.Controllers.Precio
{
    public class PrecioController : Controller
    {
        //
        // GET: /Precio/

        public ActionResult Index()
        {
            DataSet ds = new DataSet();
            string json = "";
            try
            {
                ds = MAT.MVC.Models.PrecioMethod.GetAll();
                json = JsonConvert.SerializeObject(ds, Formatting.Indented);
                ViewBag.jResult = json;

                return PartialView();
            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "PrecioController");
                return PartialView();
            }

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            Entities.Precio precio = new Entities.Precio();
            precio.PrecioId = Guid.NewGuid();
            MAT.Utilities.Helper.FillEntity<Entities.Precio>(ref precio, form);
            precio.Monto = Convert.ToDouble(form["Monto"]);
            PaqueteDataAccess.InsertPrecio(precio);
            return RedirectToAction("Index");
        }
        public bool Delete(Guid id)
        {
            bool result = false;
            try
            {
                // NetTiers F4: la cascada (vinculos PaquetePrecio, Pasaje.PrecioID = NULL,
                // borrado del precio) corre en la transaccion del SP.
                PaqueteDataAccess.DeletePrecioCascade(id);
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        //public ActionResult Edit(Guid id)
        //{
        //    return PartialView(new Services.PrecioService().GetByPrecioId(id));
        //}

        //[HttpPost]
        //public ActionResult Edit(Guid id, FormCollection form)
        //{
        //    Services.PrecioService precioService = new Services.PrecioService();
        //    Entities.Precio precio = precioService.GetByPrecioId(id);
        //    Helper.FillEntity<Entities.Precio>(ref precio, form);
        //    precioService.Update(precio);
        //    return RedirectToAction("Index");
        //}
        public ActionResult ABM(string PrecioID)
        {
            try
            {
                MAT.MVC.Models.PrecioModel oPrecio = new Models.PrecioModel();
                if (PrecioID != "0")
                {
                    DataSet ds = MAT.MVC.Models.PrecioMethod.GetById(new Guid(PrecioID));

                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        oPrecio.PrecioID = new Guid(item["PrecioID"].ToString());
                        oPrecio.Monto = Convert.ToInt32(item["Monto"]);

                        if (item["Vigencia"].ToString() != "")
                        {
                            oPrecio.Vigencia = Convert.ToDateTime(item["Vigencia"]);
                        }

                        oPrecio.Descripcion = item["Descripcion"].ToString();

                        if (item["Mes"].ToString() != "")
                        {
                            oPrecio.Mes = item["Mes"].ToString();
                        }

                        if (item["DescripcionVoucher"].ToString() != "")
                        {
                            oPrecio.DescripcionVoucher = item["DescripcionVoucher"].ToString();
                        }

                    }
                }

                return PartialView(oPrecio);
            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "PrecioController");
                return PartialView();
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult Insert(MVC.Models.PrecioModel Precio)
        {
            string[] sResult = new string[2];
            try
            {
                MVC.Models.PrecioMethod.Insert(Precio);
                sResult[0] = "Done.";
                sResult[1] = "";
            }
            catch (Exception e)
            {
                sResult[0] = "Error.";
                sResult[1] = ErrorUtil.LogAndGetPublicMessage(e, "PrecioController");
            }
            return Json(sResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult Update(MVC.Models.PrecioModel Precio)
        {
            string[] sResult = new string[2];
            try
            {
                MVC.Models.PrecioMethod.Update(Precio);
                sResult[0] = "Done.";
                sResult[1] = "";
            }
            catch (Exception e)
            {
                sResult[0] = "Error.";
                sResult[1] = ErrorUtil.LogAndGetPublicMessage(e, "PrecioController");
            }
            return Json(sResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult Delete(string PrecioID)
        {
            string[] sResult = new string[2];
            try
            {
                MVC.Models.PrecioMethod.Delete(new Guid(PrecioID));
                sResult[0] = "Done.";
                sResult[1] = "";
            }
            catch (Exception e)
            {
                sResult[0] = "Error.";
                sResult[1] = ErrorUtil.LogAndGetPublicMessage(e, "PrecioController");
            }
            return Json(sResult, JsonRequestBehavior.AllowGet);
        }
    }
}
