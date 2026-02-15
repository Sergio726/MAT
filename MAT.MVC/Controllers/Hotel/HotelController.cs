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

                Guid gPasajero, gHabitacion, gViaje;
                if (!Guid.TryParse(pasajeroId, out gPasajero) || !Guid.TryParse(habitacionId, out gHabitacion) || !Guid.TryParse(viajeId, out gViaje))
                {
                    return Json(new { ok = false, message = "Los identificadores proporcionados no son válidos." });
                }

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
                        int id = 0;
                        string result = "";

                        // Leer ID de forma segura (el SP puede devolver -1 en caso de error)
                        try
                        {
                            var rawId = _reader["ID"];
                            if (rawId != null && rawId != DBNull.Value)
                                id = Convert.ToInt32(rawId);
                        }
                        catch
                        {
                            id = -1;
                        }

                        // Leer Result de forma segura
                        try
                        {
                            var rawResult = _reader["Result"];
                            if (rawResult != null && rawResult != DBNull.Value)
                                result = rawResult.ToString();
                        }
                        catch
                        {
                            result = "Error al leer la respuesta del procedimiento.";
                        }

                        if (id == 1 && result == "Done.")
                        {
                            return Json(new { ok = true, message = "Pasajero quitado de la habitación correctamente." });
                        }

                        // El SP devolvió un error explícito
                        string errorMsg = !string.IsNullOrEmpty(result) ? result : "Error desconocido al quitar el pasajero.";
                        ErrorUtil.LogAndGetPublicMessage(new Exception("SP DeleteReservaHotel error: " + errorMsg), "HotelController.QuitarPasajeroHabitacion");
                        return Json(new { ok = false, message = errorMsg });
                    }
                }

                return Json(new { ok = false, message = "No se recibió respuesta del procedimiento. Contacte al administrador." });
            }
            catch (SqlException sqlEx)
            {
                // Error SQL (conexión, timeout, error de SP no controlado)
                string msg = "Error de base de datos al quitar el pasajero de la habitación.";
                ErrorUtil.LogAndGetPublicMessage(sqlEx, "HotelController.QuitarPasajeroHabitacion");
                return Json(new { ok = false, message = msg + " Detalle: " + sqlEx.Message });
            }
            catch (Exception e)
            {
                string msg = ErrorUtil.LogAndGetPublicMessage(e, "HotelController.QuitarPasajeroHabitacion");
                return Json(new { ok = false, message = !string.IsNullOrEmpty(msg) ? msg : "Error inesperado al quitar el pasajero. Contacte al administrador." });
            }
        }
    }
}
