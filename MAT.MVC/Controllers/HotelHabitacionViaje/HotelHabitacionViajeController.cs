using MAT.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using MAT.MVC.Infrastructure;

namespace MAT.MVC.Controllers.HotelHabitacionViaje
{
    public class HotelHabitacionViajeController : Controller
    {
        //
        // GET: /HotelHabitacionViaje/

        public ActionResult ABM(string ViajeHotelID)
        {
            HotelHabitacionViajeModel item = new HotelHabitacionViajeModel();
            DataSet ds = new DataSet();
            string json = "";
            try
            {
                  item = MVC.Models.HotelHabitacionViajMethod.GetEncabezado(new Guid(ViajeHotelID));
                ViewBag.HabitacionTipo = MAT.MVC.Models.HabitacionTipoMethod.GetAllHabitacionTipo();
                
                ds = MVC.Models.HotelHabitacionViajMethod.GetDistribucionHab(new Guid(item.ViajeID),new Guid(item.HotelID),Convert.ToString(item.Fecha));
                json = JsonConvert.SerializeObject(ds, Formatting.Indented);

            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "HotelHabitacionViajeController");
            }

            ViewBag.dtDistribucionHab = json;
            return PartialView(item);
        }

        [Authorize]
        public JsonResult AddDistribucion(string HotelID, string ViajeID , string Fecha, int Tipo, int Cantidad, decimal habPrecio, string habDescripcion)
        {
            string[] sResult = new string[2];

            try
            {
                int[] aTipo = new int[Cantidad];
                for (int i = 0; i < Cantidad; i++)
                {
                    aTipo[i] = Tipo;
                }
                string sTipo = string.Join(",", aTipo);

                MVC.Models.HotelHabitacionViajMethod.Insert(new Guid(HotelID), new Guid(ViajeID), Convert.ToDateTime(Fecha), sTipo, habPrecio, habDescripcion);
                sResult[0] = "Done.";
            }
            catch (Exception e)
            {
                sResult[0] = "";
                sResult[1] = "Error: " + ErrorUtil.LogAndGetPublicMessage(e, "HotelHabitacionViajeController.AddDistribucion");
            }


            return Json(new
            {
                Mensaje = sResult[0],
                Error = sResult[1]
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult dtDistribucionHab(string HotelID, string ViajeID, string Fecha)
        {
            string[] sResult = new string[3];
            DataSet ds = new DataSet();
            string json = "";
            try
            {
                ds = MVC.Models.HotelHabitacionViajMethod.GetDistribucionHab(new Guid(ViajeID), new Guid(HotelID), Fecha);
                json = JsonConvert.SerializeObject(ds, Formatting.Indented);
                
                sResult[0] = json;
                sResult[1] = "Done.";
            }
            catch (Exception e)
            {
                sResult[0] = "";
                sResult[1] = "Error.";
                sResult[2] = ErrorUtil.LogAndGetPublicMessage(e, "HotelHabitacionViajeController.dtDistribucionHab");
            }


            return Json(new
            {
                DataTable = sResult[0],
                Mensaje = sResult[1],
                Error = sResult[2]
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult deleteDistribucionRow(int TransHotelHabitacionViajeID)
        {
            string[] sResult = new string[3];
            
            try
            {
                int Status = MVC.Models.HotelHabitacionViajMethod.Delete(TransHotelHabitacionViajeID);
                
                sResult[0] = Status.ToString();
                sResult[1] = "Done.";
                
            }
            catch (Exception e)
            {
                sResult[0] = "";
                sResult[1] = "Error.";
                sResult[2] = ErrorUtil.LogAndGetPublicMessage(e, "HotelHabitacionViajeController.deleteDistribucionRow");
            }


            return Json(new
            {
                Estado = sResult[0],
                Mensaje = sResult[1],
                Error = sResult[2]
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult CreatePlantilla(string Nombre, string ViajeID, string HotelID)
        {
            string[] sResult = new string[2];

            try
            {
                int iStatus = MVC.Models.HotelHabitacionViajMethod.CreatePlantilla(Nombre, new Guid(ViajeID), new Guid(HotelID));
                if (iStatus == 0)
                {
                    sResult[0] = "Done.";
                }
                else {
                    sResult[0] = "Alert.";
                    sResult[1] = "El nombre de la plantilla ya existe, por favor ingrese otro diferente.";
                }

                

            }
            catch (Exception e)
            {
                sResult[0] = "Error.";
                sResult[1] = "Error: " + ErrorUtil.LogAndGetPublicMessage(e, "HotelHabitacionViajeController.CreatePlantilla");
            }


            return Json(new
            {
                Mensaje = sResult[0],
                Error = sResult[1]
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult GetPlantillasDistribucion() {
            string json = "";

            try
            {
                DataSet ds = MVC.Models.HotelHabitacionViajMethod.GetPlantillasDistribucion();
                json = JsonConvert.SerializeObject(ds, Formatting.Indented);

            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "HotelHabitacionViajeController");
            }

            ViewBag.dtPlantilla = json;
            return PartialView();
        }

        [Authorize]
        public JsonResult AddDistribucionByPlantilla(string HotelID, string ViajeID, string Fecha, int PlantillaId)
        {
            string[] sResult = new string[2];

            try
            {
                MVC.Models.HotelHabitacionViajMethod.InsertByPlantilla(new Guid(HotelID), new Guid(ViajeID), Convert.ToDateTime(Fecha), PlantillaId);
                sResult[0] = "Done.";
            }
            catch (Exception e)
            {
                sResult[0] = "";
                sResult[1] = "Error: " + ErrorUtil.LogAndGetPublicMessage(e, "HotelHabitacionViajeController.AddDistribucionByPlantilla");
            }


            return Json(new
            {
                Mensaje = sResult[0],
                Error = sResult[1]
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult deletePlantilla(int PlantillaId)
        {
            string[] sResult = new string[2];

            try
            {
                MVC.Models.HotelHabitacionViajMethod.deletePlantilla(PlantillaId);
                sResult[0] = "Done.";
            }
            catch (Exception e)
            {
                sResult[0] = "";
                sResult[1] = "Error: " + ErrorUtil.LogAndGetPublicMessage(e, "HotelHabitacionViajeController.deletePlantilla");
            }


            return Json(new
            {
                Mensaje = sResult[0],
                Error = sResult[1]
            }, JsonRequestBehavior.AllowGet);
        }

    }


}
