using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.Entities;
using PagedList;
using Model = MAT.MVC.Models;
using MAT.MVC.Models;
using Newtonsoft.Json;
using System.Data;
using MAT.MVC.Infrastructure;
namespace MAT.MVC.Controllers.Habitacion
{
    public class HabitacionController : Controller
    {
       
        public ActionResult Index(string searchString, string MsgError = null)
        {
            //List<Model.HabitacionStandard> ListHabitacion = new List<Model.HabitacionStandard>();
            try
            {
                ViewBag.Error = MsgError;
                ViewBag.HotelID = searchString;
                
                //lista hoteles
                ViewBag.lHoteles = MAT.MVC.Models.HotelMethod.GetHotelDropDown();

                return View();
            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "HabitacionController");
                return View();
            }
        }

        public ActionResult PartialListHabitacion(string HotelID)
        {
            DataSet ds = new DataSet();
            string json = "";
            try
            {
                ds = Model.HabitacionMethod.GetHabitacionByHotelId(HotelID); ;
                json = JsonConvert.SerializeObject(ds, Formatting.Indented);
                ViewBag.jResult = json;

                return PartialView();
            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "HabitacionController");
                return PartialView();
            }
        }
       
        //public ActionResult Create(Guid? id)
        //{
        //    Model.HabitacionStandard Hab = new Model.HabitacionStandard();
        //    ViewBag.HabitacionTipo = MAT.MVC.Models.HabitacionTipoMethod.GetAllHabitacionTipo();
        //    ViewBag.DDHotel = MAT.MVC.Models.HotelMethod.GetHotelDropDown();
        //    Hab.HotelID = id.ToString();
        //    return View(Hab);
        //}

        [Authorize]
        public ActionResult ABM(string Id = "")
        {
            HabitacionStandard Hab = new HabitacionStandard();
            
            try
            {
                ViewBag.Error = "";
                ViewBag.HabitacionTipo = MAT.MVC.Models.HabitacionTipoMethod.GetAllHabitacionTipo();
                
                if (Id != "")
	            {
	                Hab = Model.HabitacionMethod.GetHabitacionById(Id);	 
	            }

                return PartialView(Hab);
            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "HabitacionController");
                ViewBag.HabitacionTipo = "";
                return PartialView(Hab);
            }


        }

        [Authorize]
        public JsonResult updateHabitacion(HabitacionStandard oHabitacion)
        {
            string[] sResult = new string[2];

            try
            {
                sResult[0] = MVC.Models.HabitacionMethod.EditHabitacion(oHabitacion)[0]; 

            }
            catch (Exception e)
            {
                sResult[0] = "Error.";
                sResult[1] = "Error: " + ErrorUtil.LogAndGetPublicMessage(e, "HabitacionController.updateHabitacion");
            }


            return Json(new
            {
                Mensaje = sResult[0],
                Error = sResult[1]
            }, JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //public ActionResult Create(Model.HabitacionStandard Hab)
        //{
        //    try
        //    {
        //        ViewBag.Error = "";
        //        Model.HabitacionMethod.NewHabitacion(Hab);
        //        return RedirectToAction("Index", "Habitacion", new { SearchString = Hab.HotelID });
        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "HabitacionController");
        //        ViewBag.HabitacionTipo = MAT.MVC.Models.HabitacionTipoMethod.GetAllHabitacionTipo();
        //        return RedirectToAction("Create", "Habitacion", new { SearchString = Hab.HotelID });
        //    }

            
        //}

        //public ActionResult Edit(Guid Id)
        //{
        //    Model.HabitacionStandard Hab = new Model.HabitacionStandard();
        //    try
        //    {
        //        Hab = Model.HabitacionMethod.GetHabitacionById(Id.ToString());
        //        return View(Hab);
        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "HabitacionController");
        //        return View(Hab);
        //    }
           
        //}

        //[HttpPost]
        //public ActionResult Edit(Model.HabitacionStandard Hab)
        //{
        //    try
        //    {
        //        Model.HabitacionMethod.EditHabitacion(Hab);
        //        return RedirectToAction("Details", "Habitacion", new { Id = Hab.HabitacionID });
        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "HabitacionController");
        //        return View(Hab);
        //    }
            
        //}

        //public ActionResult Details(Guid Id)
        //{
        //    Model.HabitacionStandard Hab = new Model.HabitacionStandard();
        //    try
        //    {
        //        Hab = Model.HabitacionMethod.GetHabitacionById(Id.ToString());
        //        return View(Hab);
        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "HabitacionController");
        //        return View(Hab);
        //    }
            
        //}

        public ActionResult Delete(string HotelID, string HabitacionID)
        {
            try
            {
                Model.HabitacionMethod.DeleteHabitacion(HabitacionID);
                return RedirectToAction("Index", "Habitacion", new { SearchString = HotelID });
            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "HabitacionController");
                return RedirectToAction("Index", "Habitacion", new { SearchString = HotelID, MsgError = e.Message });
            }
        }

    }
}
