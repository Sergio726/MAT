using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.Entities;
using MAT.Enums;
using MAT.Utilities;
using MAT.MVC.Infrastructure.Data;
using MAT.MVC.Models;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using MAT.MVC.Common;
using System.Web.Helpers;
using MAT.MVC.Infrastructure;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using Newtonsoft.Json;

namespace MAT.MVC.Controllers.Viaje
{
    public class ViajeController : Controller
    {
        //
        // GET: /Viaje/

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListViajes(string sDateYear)
        {
            List<ViajeModel> ListViajes = ViajeMethod.ListViajeByYear(sDateYear);

            return PartialView(ListViajes);
        }


        public ActionResult PopPupViajes()
        {
            List<Entities.Viaje> viajes = ViajeDataAccess.GetAll();
            return PartialView(viajes);
        }

        public ActionResult Details(Guid id)
        {
            return View(ViajeDataAccess.GetById(id));
        }

        public ActionResult Create()
        {
            return View(new MAT.Entities.Viaje());
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            if (!string.IsNullOrEmpty(collection.Get("PaqueteId")))
            {
                Guid paqueteid = new Guid(collection.Get("PaqueteId").ToString());
                #region Entidades y Servicios
                MAT.Entities.Viaje EViaje = new MAT.Entities.Viaje();
                List<Entities.PaqueteServicio> servicios = PaqueteDataAccess.GetPaqueteServiciosByPaqueteId(paqueteid);
                #endregion
                if (ExisteServicioBus(servicios))
                {
                    #region datos de Viaje
                    EViaje.PaqueteId = new Guid(collection.Get("PaqueteId").ToString());
                    EViaje.Origen = collection.Get("Origen").ToString();
                    string FecSal = collection.Get("FechaSalida").ToString();
                    EViaje.FechaRegreso = !string.IsNullOrEmpty(collection.Get("FechaRegreso")) ? Convert.ToDateTime(collection.Get("FechaRegreso")) : DateTime.MinValue;
                    EViaje.HoraRegreso = !string.IsNullOrEmpty(collection.Get("HoraRegreso")) ? collection.Get("HoraRegreso").Substring(0, 5) : null;
                    if (!string.IsNullOrEmpty(collection.Get("Descripcion"))) EViaje.Descripcion = collection.Get("Descripcion");
                    if (FecSal != "")
                    {
                        EViaje.FechaSalida = Convert.ToDateTime(FecSal);
                    }

                    string HrSal = collection.Get("HoraSalida").ToString();
                    if (HrSal != "")
                    {
                        //int hr = Convert.ToInt16(HrSal.Substring(0, 2));
                        //int min = Convert.ToInt16(HrSal.Substring(3, 2));
                        //EViaje.HoraSalida = new TimeSpan(0,hr, min,0);
                        EViaje.HoraSalida = HrSal.Substring(0, 5);
                    }

                    EViaje.PaisOrigen = collection.Get("PaisOrigen").ToString();
                    EViaje.PaisDestino = collection.Get("PaisDestino").ToString();
                    EViaje.Paso = collection.Get("Paso").ToString();
                    EViaje.Medio = collection.Get("Medio").ToString();

                    ViajeDataAccess.Insert(EViaje);

                    #endregion

                    #region Crear Pasajes
                    PaqueteModel paqueteModel = new PaqueteModel(EViaje.ViajeId);
                    paqueteModel.GenerarPasajes();
                    #endregion

                    return RedirectToAction("Index", "Viaje");
                }
                else
                {
                    ViewData["error"] = "Debe asignar un servicio de transporte al paquete antes de crear un viaje.";
                    return View(new MAT.Entities.Viaje());
                }
            }
            else
            {
                ViewData["error"] = "Seleccione un paquete por favor.";
                return View(new MAT.Entities.Viaje());
            }


        }
        private bool ExisteServicioBus(List<Entities.PaqueteServicio> servicios)
        {
            foreach (var item in servicios)
            {
                Entities.Servicio servicio = Infrastructure.Data.MaestrosDataAccess.GetServicioById(item.ServicioId.Value);
                if (servicio.TransporteId.HasValue) return true;
            }
            return false;
        }

        public ActionResult Edit(string sAction , string sViajeID)
        {
            ViajeModel Model = new ViajeModel();
            try
            {
                ViewBag.Action = sAction;
                PaqueteStandard Paquete = new PaqueteStandard();
                List<PaqueteStandard> ListPaquete = new List<PaqueteStandard>();
                ListPaquete = PaqueteVinculos.ListPaqueteByYear();
                
                var json = "";
                var jsonSerialiser = new JavaScriptSerializer();
                json = jsonSerialiser.Serialize(ListPaquete);
                ViewBag.jListPaquete = json;

                switch (sAction)
                {
                    case "detail": Model = MAT.MVC.Models.ViajeMethod.ViajeByViajeID(sViajeID);
                        break;
                    case "edit": Model = MAT.MVC.Models.ViajeMethod.ViajeByViajeID(sViajeID);
                        break;
                    case "new": Model = new ViajeModel();
                        Model.Origen = "SALTA";
                        Model.PaisOrigen = "ARGENTINA";
                        Model.Medio = "TERRESTRE";
                        Model.TiempoConsentracion = 30;
                        break;
                }
               
                return View(Model);
            }
            catch 
            {
                return View(Model);
            }
            
        }

        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
           
            #region Entidades y Servicios
            MAT.Entities.Viaje EViaje = ViajeDataAccess.GetById(id);
            #endregion

            #region datos de Viaje
            EViaje.PaqueteId = new Guid(collection.Get("PaqueteId").ToString());
            EViaje.Origen = collection.Get("Origen").ToString();
            string FecSal = collection.Get("FechaSalida").ToString();
            EViaje.FechaRegreso = !string.IsNullOrEmpty(collection.Get("FechaRegreso")) ? Convert.ToDateTime(collection.Get("FechaRegreso")) : DateTime.MinValue;
            EViaje.HoraRegreso = !string.IsNullOrEmpty(collection.Get("HoraRegreso")) ? collection.Get("HoraRegreso").Substring(0, 5) : null;
            if (FecSal != "")
            {
                EViaje.FechaSalida = Convert.ToDateTime(FecSal);
            }

            string HrSal = collection.Get("HoraSalida").ToString();
            if (HrSal != "")
            {
                //int hr = Convert.ToInt16(HrSal.Substring(0, 2));
                //int min = Convert.ToInt16(HrSal.Substring(3, 2));
                //EViaje.HoraSalida = new TimeSpan(0, hr, min, 0);
                EViaje.HoraSalida = HrSal.Substring(0, 5);
            }

            EViaje.PaisOrigen = collection.Get("PaisOrigen").ToString();
            EViaje.PaisDestino = collection.Get("PaisDestino").ToString();
            EViaje.Paso = collection.Get("Paso").ToString();
            EViaje.Medio = collection.Get("Medio").ToString();

            ViajeDataAccess.Update(EViaje);

            #endregion

            return RedirectToAction("Details/" + id.ToString(), "Viaje");
        }



        public string CrearPasajes(string id)
        {
            Guid viajeid = new Guid(id);
            PaqueteModel paqueteModel = new PaqueteModel(viajeid);
            string messageResult = "";
            if (paqueteModel.GenerarPasajes())
            {
                messageResult = "Pasajes generados correctamente";
            }
            else
            {
                messageResult = "Ha ocurrido un error al generar los pasajes. Consulte con el Administrador.";
            }
            return messageResult;
        }

        
        public ActionResult Hoteles(Guid id)
        {
            ViewData["viajeid"] = id;
            
            List<MAT.MVC.Models.ViajeHotel> ViajeH = new List<MAT.MVC.Models.ViajeHotel>();

            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@ViajeID", SqlDbType.VarChar, 0, Convert.ToString(id)),
                    };
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Viaje_ViajeHotel_GetByViajeID", dbParams))
            {
                while (_reader.Read())
                {
                    MAT.MVC.Models.ViajeHotel item = new MAT.MVC.Models.ViajeHotel();
                    item.ViajeHotelID = _reader["ViajeHotelID"].ToString();
                    item.ViajeID = _reader["ViajeID"].ToString();
                    item.HotelID = _reader["HotelID"].ToString();
                    if (_reader["Desde"].ToString() != "")
                    {
                        item.Desde = _reader["Desde"].ToString();    
                    }
                    if (_reader["Hasta"].ToString() != "")
                    {
                        item.Hasta = _reader["Hasta"].ToString();    
                    }
                    
                    item.HoraIngreso = _reader["HoraIngreso"].ToString();
                    item.HoraSalida = _reader["HoraSalida"].ToString();
                    item.Nombre = _reader["Nombre"].ToString();
                    item.ViajeNombre = _reader["ViajeNombre"].ToString();
                    item.Comentario = _reader["Comentario"].ToString();
                    ViajeH.Add(item);
                }
            }
            return View(ViajeH);
        }
        public ActionResult HotelesDisponibles(Guid id)
        {

            DataSet ds = new DataSet();
            string json = "";
            try
            {
                ds = MAT.MVC.Models.ViajeMethod.GetAvailableHoteles(id);
                json = JsonConvert.SerializeObject(ds, Formatting.Indented);
                ViewBag.jResult = json;
                ViewBag.ViajeID = id;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
            { 
            
            }
            return PartialView(id);
        }
                
        public bool VincularHotel(Guid hotelid, Guid viajeid)
        {
            bool result = false;
            try
            {
                SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@ViajeID", SqlDbType.VarChar, 0, viajeid.ToString()),
                    DBHelper.MakeParam("@HotelID", SqlDbType.VarChar, 0, hotelid.ToString())
                };
                using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Viaje_VincularHotelViaje", dbParams))
                {
                    while (_reader.Read())
                    {
                        if (_reader["Result"].ToString() == "Done.")
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                    }
                }
               
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                result = false;
            }
            return result;
        }
        public bool DesvincularHotel(Guid hotelid, Guid viajeid)
        {
            bool result = false;
            try
            {
                MAT.MVC.Models.ViajeMethod.DeleteHotel(viajeid, hotelid);
                result = true;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                result = false;
            }
            return result;
        }

        public ActionResult HotelSetIngresoEgreso(string ViajeHotelID, string viajeid, string Fecha)
        {
            
            MAT.MVC.Models.ViajeHotel ViajeH = new MAT.MVC.Models.ViajeHotel();
            ViewBag.ViajeHotelID = ViajeHotelID;
            ViewBag.ViajeID = viajeid;

            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@ViajeID", SqlDbType.VarChar, 0, viajeid),
                        DBHelper.MakeParam("@ViajeHotelID", SqlDbType.VarChar, 0, ViajeHotelID),
                        DBHelper.MakeParam("@Fecha", SqlDbType.VarChar, 10, Fecha),
                    };
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Viaje_ViajeHotel_GetAll", dbParams))
            {
                while (_reader.Read())
                {
                    ViajeH.ViajeHotelID = _reader["ViajeHotelID"].ToString();
                    ViajeH.ViajeID = _reader["ViajeID"].ToString();
                    ViajeH.HotelID = _reader["HotelID"].ToString();
                    if (_reader["Desde"].ToString() != "")
                    {
                        ViajeH.Desde = _reader["Desde"].ToString();
                    }
                    if (_reader["Hasta"].ToString() != "")
                    {
                        ViajeH.Hasta = _reader["Hasta"].ToString();
                    }

                    ViajeH.HoraIngreso = _reader["HoraIngreso"].ToString();
                    ViajeH.HoraSalida = _reader["HoraSalida"].ToString();
                    ViajeH.Nombre = _reader["Nombre"].ToString();
                    ViajeH.Comentario = _reader["Comentario"].ToString();
                }
            }


            return PartialView(ViajeH);
        }

        public JsonResult HotelIngresoEgreso_Set(string ViajeHotelID = "", string Desde = "", string Hasta = "", string HoraIngreso = "", string HoraSalida = "", string Comentario = "")
        {
            string[] sResult = new string[3];

            try
            {
                
                    SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@ViajeHotelID", SqlDbType.VarChar, 0, ViajeHotelID),
                        DBHelper.MakeParam("@Desde", SqlDbType.VarChar, 10, Desde),
                        DBHelper.MakeParam("@Hasta", SqlDbType.VarChar, 10, Hasta),
                        DBHelper.MakeParam("@HoraIngreso", SqlDbType.VarChar, 0, HoraIngreso),
                        DBHelper.MakeParam("@HoraSalida", SqlDbType.VarChar, 0, HoraSalida),
                        DBHelper.MakeParam("@Comentario", SqlDbType.VarChar, 0, Comentario)
                    };
                    SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_Viaje_ViajeHotel_IngresoEgreso_Set", dbParams);

                    while (_reader.Read())
                    {

                        sResult[0] = _reader["Id"].ToString();
                        sResult[1] = _reader["ErrorMsg"].ToString();

                    }
                    sResult[2] = "Done.";

                
            }
            catch (Exception e)
            {
                sResult[0] = "";
                sResult[1] = "Error: " + ErrorUtil.LogAndGetPublicMessage(e, "ViajeController.HotelIngresoEgreso_Set");
                sResult[2] = "Error.";
            }


            return Json(new
            {
                ID = sResult[0],
                Mensaje = sResult[1],
                Estado = sResult[2]
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateViaje(ViajeModel Viaje)
        {
            string[] sResult = new string[2];
            try
            {
                sResult = ViajeMethod.UpdateViaje(Viaje);
            }
            catch (Exception e)
            {
                sResult[0] = "";
                sResult[1] = "Error: " + ErrorUtil.LogAndGetPublicMessage(e, "ViajeController.UpdateViaje");
            }

            return Json(new
            {
                ID = sResult[0],
                Mensaje = sResult[1]
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertViaje(ViajeModel Viaje)
        {
            string[] sResult = new string[2];
            try
            {
                sResult = ViajeMethod.InsertViaje(Viaje);
              
            }
            catch (Exception e)
            {
                sResult[0] = "";
                sResult[1] = "Error: " + ErrorUtil.LogAndGetPublicMessage(e, "ViajeController.InsertViaje");
            }

            return Json(new
            {
                ID = sResult[0],
                Mensaje = sResult[1]
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteViaje(Guid ViajeId, string DeleteDetalle)
        {
            string[] sResult = new string[2];
            try
            {
                ViajeMethod.DeleteViaje(ViajeId, MATContext.CurrentVendedor.VendedorId, DeleteDetalle);
                sResult[0] = "ViajeId";
                sResult[1] = "Done";
            }
            catch (Exception e)
            {
                sResult[0] = "";
                sResult[1] = "Error: " + ErrorUtil.LogAndGetPublicMessage(e, "ViajeController.DeleteViaje");
            }

            return Json(new
            {
                ID = sResult[0],
                Mensaje = sResult[1]
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetViajesPorVencer()
        {
            string sMensaje = "";
            List<MAT.MVC.Models.ViajePorVencerDto> items = new List<MAT.MVC.Models.ViajePorVencerDto>();

            try
            {
                const string cacheKey = "MAT.ViajesPorVencer.items.v1";

                // Cache server-side sin depender de System.Runtime.Caching (evita problemas de referencia)
                var cached = HttpRuntime.Cache[cacheKey] as List<MAT.MVC.Models.ViajePorVencerDto>;
                if (cached != null)
                {
                    items = cached;
                }
                else
                {
                    items = ViajeMethod.GetViajesPorVencerDto(7);
                    HttpRuntime.Cache.Insert(
                        cacheKey,
                        items,
                        dependencies: null,
                        absoluteExpiration: DateTime.UtcNow.AddMinutes(5),
                        slidingExpiration: Cache.NoSlidingExpiration
                    );
                }
            }
            catch (Exception e)
            {
                sMensaje = "Error: " + ErrorUtil.LogAndGetPublicMessage(e, "ViajeController.GetViajesPorVencer");
            }

            // Fallback legacy (para no romper consumidores viejos): { Table: [...] }
            // Nota: NO incluye la metadata del DataSet, pero conserva el shape usado por el Home viejo.
            var sJsonResult = "";
            try
            {
                sJsonResult = JsonConvert.SerializeObject(new { Table = items });
            }
            catch { }

            return Json(new
            {
                items,
                sJsonResult,
                sMensaje
            }, JsonRequestBehavior.AllowGet);
        }


    }
}
