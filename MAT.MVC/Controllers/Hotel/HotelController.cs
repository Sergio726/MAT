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
using MAT.MVC.Infrastructure;
using MAT.MVC.Common;

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
                ViewBag.MsgError = ErrorUtil.LogAndGetPublicMessage(e, "HotelController.Index");
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
                ViewBag.MsgError = ErrorUtil.LogAndGetPublicMessage(e, "HotelController.ABM");
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
                sResult[2] = ErrorUtil.LogAndGetPublicMessage(e, "HotelController.HotelCreate");
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
                sResult[2] = ErrorUtil.LogAndGetPublicMessage(e, "HotelController.HotelUpdate");
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
                sResult[2] = ErrorUtil.LogAndGetPublicMessage(e, "HotelController.HotelCheckNombre");
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
            
            return View();
        }

        public ActionResult EsquemaDistribucion(Guid? hotelid, Guid? viajeid, string fecha)
        {
            List<EsquemaDistribucion> ListDistribucion = new List<EsquemaDistribucion>();

            if (!hotelid.HasValue || !viajeid.HasValue)
            {
                return new HttpStatusCodeResult(400, "Faltan parámetros requeridos: hotelid y/o viajeid.");
            }

            try
            {
                ViewData["viajeid"] = viajeid.Value;

                ListDistribucion = MVC.Models.HotelMethod.GetEsquemaDistribucion(viajeid.Value, hotelid.Value, fecha);

                SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@ViajeId", SqlDbType.UniqueIdentifier, 0, viajeid.Value),
                    DBHelper.MakeParam("@HotelId", SqlDbType.UniqueIdentifier, 0, hotelid.Value),
                    DBHelper.MakeParam("@Fecha", SqlDbType.Date, 0, Convert.ToDateTime(fecha))
                };

                Int32 iCantPax = Convert.ToInt32(DBHelper.ExecuteScalar("dbo.usp_MAT_Hotel_EsquemaDistribucion_CantPax", dbParams));

                ViewBag.CantPax = iCantPax;
                return PartialView(ListDistribucion);
            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "HotelController.EsquemaDistribucion");
                return PartialView(ListDistribucion);
            }
        }

        /// <summary>
        /// Quita un pasajero de una habitación (elimina el registro en ReservaHabitacion).
        /// Si el pasajero queda sin ninguna habitación asignada en el viaje, actualiza el estado del pasaje.
        /// </summary>
        [Authorize]
        [HttpPost]
        public JsonResult QuitarPasajeroHabitacion(string pasajeroId, string habitacionId, string viajeId)
        {
            try
            {
                if (string.IsNullOrEmpty(pasajeroId) || string.IsNullOrEmpty(habitacionId) || string.IsNullOrEmpty(viajeId))
                {
                    return Json(new { ok = false, message = "Faltan parámetros: pasajeroId, habitacionId o viajeId." });
                }
                if (MATContext.CurrentVendedor == null)
                {
                    return Json(new { ok = false, message = "Usuario sin vendedor asociado. No se puede auditar la acción." });
                }

                var gPasajero = new Guid(pasajeroId);
                var gHabitacion = new Guid(habitacionId);
                var gViaje = new Guid(viajeId);

                SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@PasajeroID", SqlDbType.UniqueIdentifier, 0, gPasajero),
                    DBHelper.MakeParam("@HabitacionID", SqlDbType.UniqueIdentifier, 0, gHabitacion),
                    DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, gViaje),
                    DBHelper.MakeParam("@UserID", SqlDbType.UniqueIdentifier, 0, MATContext.CurrentVendedor.VendedorId)
                };

                using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_PersonaCliente_DeleteReservaHotel", dbParams))
                {
                    if (_reader.Read())
                    {
                        var id = _reader["ID"] != DBNull.Value ? Convert.ToInt32(_reader["ID"]) : 0;
                        var result = _reader["Result"]?.ToString() ?? "";
                        if (id == 1 && result == "Done.")
                        {
                            return Json(new { ok = true, message = "Pasajero quitado de la habitación correctamente." });
                        }
                        return Json(new { ok = false, message = result });
                    }
                }

                return Json(new { ok = false, message = "No se recibió respuesta del servidor." });
            }
            catch (Exception e)
            {
                return Json(new { ok = false, message = ErrorUtil.LogAndGetPublicMessage(e, "HotelController.QuitarPasajeroHabitacion") });
            }
        }
    }
}
