using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.Entities;
using MAT.MVC.Infrastructure.Data;
using PagedList;
using MAT.Utilities;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using MAT.MVC.Models;
using MAT.MVC.Common;
using System.Diagnostics;

namespace MAT.MVC.Controllers.RevervaHabitacion
{
    public class ReservaHabitacionController : Controller
    {
        //
        // GET: /ReservaHabitacion/
              

        public ActionResult Index(Guid id, string PasajeID, string PasajeroID)
        {
            try
            {
                ViewData["viajeid"] = id;
                ViewBag.PasajeID = PasajeID;
                ViewBag.PasajeroID = PasajeroID;
                ViewBag.ListaHoteles = MAT.MVC.Models.HotelMethod.GetHotelByViaje(id.ToString());
                return PartialView();
            }
            catch (Exception e)
            {

                return PartialView("Error", e);
            }
           
        }

        public ActionResult GridHotelHabitacion(string HotelID, string viajeid = "", string PasajeroID = "", string Fecha = "")
        {
            ViewData["viajeid"] = viajeid;
            ViewBag.Fecha = Fecha;
            
            List<HabitacionDisponibilidad> model = new List<HabitacionDisponibilidad>();
            try
            {
                var swTotal = Stopwatch.StartNew();
                var swDb = Stopwatch.StartNew();
                model = HabitacionMethod.GetHabitacionDisponibilidad(viajeid, HotelID, Fecha);
                swDb.Stop();

                long reservaMs = 0;
                if (PasajeroID != "")
                {
                    var swReserva = Stopwatch.StartNew();
                    string[] sHabReserva = MAT.MVC.Models.HabitacionMethod.HotelHabitacionReserva(PasajeroID, viajeid, Fecha);
                    ViewBag.HabitacionSelected = sHabReserva[0];
                    ViewBag.PasajeroID = PasajeroID;
                    swReserva.Stop();
                    reservaMs = swReserva.ElapsedMilliseconds;
                    
                }

                swTotal.Stop();
                ViewBag.PerfTotalMs = swTotal.ElapsedMilliseconds;
                ViewBag.PerfDbMs = swDb.ElapsedMilliseconds;
                ViewBag.PerfReservaMs = reservaMs;
                ViewBag.PerfCount = (model == null ? 0 : model.Count);

                return PartialView(model);
            }
            catch (Exception e){
                ViewBag.Error = e.Message;
                return PartialView(model);
            }
            
        }
        
        //public void SetPasaje(string PasajeId, string PasajeroId)
        //{
        //    if (PasajeId != "")
        //    {
        //        MAT.Utilities.HelperBinding.Pasaje.IdSelect = PasajeId;
        //        MAT.Utilities.HelperBinding.Pasajero.IdSelect = PasajeroId;
        //    }
        //}


        public void Actualizar(Guid HotelId)
        {
            IList<MAT.Entities.Habitacion> EHabitacion = ReservaHabitacionDataAccess.GetHabitacionesByHotelId(HotelId);

            //Actualizo estado de habitacion
            foreach (var item in EHabitacion)
            {
                IList<MAT.Entities.VConsultaReservaHabitacion> EConsulta = ReservaHabitacionDataAccess.GetConsultaByHabitacionId(item.HabitacionId, expiro: false);

                //Recorro las habitaciones del hotel seleccionado
                foreach (var item1 in EConsulta)
                {
                    DateTime FechaHoy = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    DateTime FechaHasta = Convert.ToDateTime(item1.Hasta.Value.ToShortDateString());

                    if (FechaHoy > FechaHasta)
                    {
                        //Actualizar Estado de Habitacion
                        MAT.Entities.Habitacion EHabitacionA = MaestrosDataAccess.GetHabitacionById(item1.HabitacionId.Value);
                        EHabitacionA.Ocupacion = EHabitacionA.Ocupacion - 1;
                        ReservaHabitacionDataAccess.UpdateHabitacion(EHabitacionA);

                        if (EHabitacionA.Capacidad > EHabitacionA.Ocupacion)
                        {
                            EHabitacionA.Estado = 0;//habitacion sin completar
                            ReservaHabitacionDataAccess.UpdateHabitacion(EHabitacionA);
                        }

                        MAT.Entities.ReservaHabitacion EReserva = ReservaHabitacionDataAccess.GetById(item1.ReservaHabitacionId);
                        EReserva.Expiro = true;//cambia el estado de la reserva
                        ReservaHabitacionDataAccess.Update(EReserva);
                    }
                }
            }
        }

        public ActionResult ReservaHabitacion(Guid HabitacionId, Guid? viajeid, string fecha)
        {
            if (viajeid.HasValue) ViewData["viajeid"] = viajeid.Value;
            //if (HabitacionId != null) MAT.Utilities.HelperBinding.Habitacion.IdSelect = HabitacionId.ToString();
            ViewBag.HabitacionId = HabitacionId;

            MAT.MVC.Models.ViajeHotel ViajeH = new MAT.MVC.Models.ViajeHotel();

            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@ViajeID", SqlDbType.VarChar, 0, Convert.ToString(viajeid)),
                        DBHelper.MakeParam("@HabitacionID", SqlDbType.VarChar, 0, Convert.ToString(HabitacionId)),
                        DBHelper.MakeParam("@Fecha", SqlDbType.VarChar, 0, fecha)
                    };
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Viaje_ViajeHotel_GetAll", dbParams))
            {
                while (_reader.Read())
                {
                    ViajeH.ViajeHotelID = _reader["ViajeHotelID"].ToString() ?? "";
                    ViajeH.ViajeID = _reader["ViajeID"].ToString() ?? "";
                    ViajeH.HotelID = _reader["HotelID"].ToString() ?? "";
                    ViajeH.Desde = _reader["Desde"].ToString() ?? "";  
                    ViajeH.Hasta = _reader["Hasta"].ToString() ?? "";
                    ViajeH.HoraIngreso = _reader["HoraIngreso"].ToString() ?? "";
                    ViajeH.HoraSalida = _reader["HoraSalida"].ToString() ?? "";
                    ViajeH.Nombre = _reader["Nombre"].ToString() ?? "";
                }
            }


            return PartialView(ViajeH);
        }

        public string SetReserva(string Desde, string Hasta, string horadesde, string horahasta, Guid viajeid, string HabitacionID = "", string PasajeroID = "", string PasajeID = "")
        {
            string sResult = "false,";
            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@HabitacionID", SqlDbType.VarChar, 0, HabitacionID),
                    DBHelper.MakeParam("@PasajeID", SqlDbType.VarChar, 0, PasajeID),
                    DBHelper.MakeParam("@PasajeroID", SqlDbType.VarChar, 0, PasajeroID),
                    DBHelper.MakeParam("@Desde", SqlDbType.DateTime, 0, Convert.ToDateTime(Desde)),
                    DBHelper.MakeParam("@Hasta", SqlDbType.DateTime, 0, Convert.ToDateTime(Hasta)),
                    DBHelper.MakeParam("@HoraIngreso", SqlDbType.VarChar, 0, horadesde),
                    DBHelper.MakeParam("@HoraSalida", SqlDbType.VarChar, 0, horahasta),
                    DBHelper.MakeParam("@Expiro", SqlDbType.Bit, 0, 0),
                    DBHelper.MakeParam("@ViajeID", SqlDbType.VarChar, 0, viajeid.ToString()),
                    DBHelper.MakeParam("@UserID", SqlDbType.UniqueIdentifier, 0, MATContext.CurrentVendedor.VendedorId )
                };
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_ReservaHabitacion_NuevaReserva", dbParams))
            {
                while (_reader.Read())
                {
                    if (_reader["Result"].ToString() == "Done.")
                    {
                        sResult = "true, ";
                    }
                }
            }

              return sResult + PasajeID.ToString();
        }
        public string GetFechasReservadas(Guid id)
        {
            List<Entities.ReservaHabitacion> reservas = ReservaHabitacionDataAccess.GetByHabitacionId(id);
            StringBuilder arrayfechas = new StringBuilder();
            foreach (var item in reservas)
            {
                DateTime fecha_desde = item.Desde.Value;
                DateTime fecha_hasta = item.Hasta.Value;
                arrayfechas.Append(item.Desde.Value.ToString("yyyy-MM-dd"));
                arrayfechas.Append(",");
                int rango = Convert.ToInt32((fecha_hasta - fecha_desde).TotalDays);
                for (int i = 0; i < rango; i++)
                {
                    fecha_desde = fecha_desde.AddDays(1);
                    arrayfechas.Append(fecha_desde.ToString("yyyy-MM-dd"));
                    arrayfechas.Append(",");
                }
            }
            arrayfechas.Remove(arrayfechas.Length - 1, 1);
            return arrayfechas.ToString();
        }
    }
}
