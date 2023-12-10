using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.Entities;
using MAT.Services;
using MAT.Utilities;
using MAT.MVC.Models;
using WebMatrix.WebData;
using System.Data;
using System.Data.SqlClient;

namespace MAT.MVC.Controllers.Hotel
{
    public class HotelController : Controller
    {
        //
        // GET: /Hotel/

        public ActionResult Index()
        {
            List<MAT.MVC.Models.HotelStandard> LHotel = new List<HotelStandard>();

            try
            {
                LHotel = MAT.MVC.Models.HotelMethod.GetHotelAll();
            }
            catch (Exception e)
            {
                ViewBag.MsgError = e.Message + " " + e.StackTrace;
            }
            return View(LHotel);
        }

        //nuevo modulo de carga Hoteles/Habitaciones/Viajes
        [Authorize]
        public ActionResult ABM(string Id = "")
        {
            MAT.MVC.Models.tblHotel oHotel = new tblHotel();
            try
            {
                if (Id != "")
                {
                    oHotel = MVC.Models.HotelMethod.GetHotelByID(new Guid(Id));
                }
                else
                {
                    oHotel.HotelID = "";
                }
            }
            catch (Exception e)
            {
                ViewBag.MsgError = e.Message + " " + e.StackTrace;
            }
            return View(oHotel);
        }

        [Authorize]
        public JsonResult HotelCreate(MVC.Models.tblHotel oHotel)
        {
            string[] sResult = new string[3];

            try
            {
                if (!bHotelCheckNombre(oHotel.Nombre))
                {
                    sResult[0] = MVC.Models.HotelMethod.CreateHotel(oHotel);
                    sResult[1] = "Done.";
                }
                else
                {
                    sResult[1] = "Existe.";
                }
                    
            }
            catch (Exception e)
            {
                sResult[0] = "";
                sResult[1] = "Error.";
                sResult[2] = "Error: " + e.Message + "StackTrace: " + e.StackTrace;
            }


            return Json(new
            {
                HotelID = sResult[0],
                Mensaje = sResult[1],
                Error = sResult[2]
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult HotelUpdate(MVC.Models.tblHotel oHotel)
        {
            string[] sResult = new string[3];

            try
            {
                MVC.Models.HotelMethod.UpdateHotel(oHotel);
                sResult[1] = "Done.";
            }
            catch (Exception e)
            {
                sResult[0] = "";
                sResult[1] = "Error.";
                sResult[2] = "Error: " + e.Message + "StackTrace: " + e.StackTrace;
            }


            return Json(new
            {
                HotelID = oHotel.HotelID,
                Mensaje = sResult[1],
                Error = sResult[2]
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult HotelCheckNombre(string Nombre)
        {
            string[] sResult = new string[3];

            try
            {
                if (MVC.Models.HotelMethod.CheckNombreHotel(Nombre) == 1)
                {
                    sResult[0] = "Yes";
                }
                else
                {
                    sResult[0] = "No";
                }
                
                sResult[1] = "Done.";
            }
            catch (Exception e)
            {
                sResult[0] = "";
                sResult[1] = "Error.";
                sResult[2] = "Error: " + e.Message + "StackTrace: " + e.StackTrace;
            }


            return Json(new
            {
                Existe = sResult[0],
                Mensaje = sResult[1],
                Error = sResult[2]
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public bool bHotelCheckNombre(string Nombre)
        {
               if (MVC.Models.HotelMethod.CheckNombreHotel(Nombre) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

        }
                
        
        [Authorize]
        public ActionResult Distribucion(string viajeid)
        {
            try
            {
               ViewData["viajeid"] = viajeid;
               ViewBag.ListHotel =  HotelMethod.GetHotelByViaje(viajeid);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            
            return PartialView();
        }

        public ActionResult EsquemaDistribucion(Guid hotelid, Guid viajeid, string fecha)
        {
            List<EsquemaDistribucion> ListDistribucion = new List<EsquemaDistribucion>();
            try
            {
                ViewData["viajeid"] = viajeid;
                ListDistribucion = MVC.Models.HotelMethod.GetEsquemaDistribucion(viajeid, hotelid, fecha);

                SqlParameter[] dbParams = new SqlParameter[]
                    { 
                          DBHelper.MakeParam("@ViajeId", SqlDbType.UniqueIdentifier, 0, viajeid),
                          DBHelper.MakeParam("@HotelId", SqlDbType.UniqueIdentifier, 0, hotelid),
                          DBHelper.MakeParam("@Fecha", SqlDbType.Date, 0, Convert.ToDateTime(fecha))
                    };
                Int32 iCantPax = Convert.ToInt32(DBHelper.ExecuteScalar("dbo.usp_MAT_Hotel_EsquemaDistribucion_CantPax", dbParams));

                ViewBag.CantPax = iCantPax;
                return PartialView(ListDistribucion);
            }
            catch (Exception e){

                ViewBag.Error = e.Message + e.StackTrace;
                return PartialView(ListDistribucion);
            }
           
        }
    }
}
