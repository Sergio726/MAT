using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.Entities;
using MAT.Services;
using MAT.Enums;
using MAT.Utilities;
using MAT.MVC.Models;
using System.Data.SqlClient;
using System.Data;

namespace MAT.MVC.Controllers.PasajeroViaje
{
    public class PasajeroViajeController : Controller
    {


        public ActionResult Index(Guid Id)
        {
            //int totalCount;
            MAT.Services.PasajeroViajeService Servicio = new PasajeroViajeService();
            List<MAT.Entities.PasajeroViaje> EPasajeroViaje = Servicio.GetAll().Where(p => p.ViajeId == Id).ToList();
            return View(EPasajeroViaje);
        }

        public ActionResult Manifiesto(Guid Id)
        {
           
            //MAT.Services.PasajeroViajeService Servicio = new PasajeroViajeService();
            //IList<MAT.Entities.PasajeroViaje> EPasajeroViaje = Servicio.GetAll().Where(p => p.ViajeId == Id).OrderBy(ps => ps.Apellido).ToList();
            List<MAT.Entities.PasajeroViaje> EPasajeroViaje = GetListPasajeroViaje(Id);

            return PartialView(EPasajeroViaje);
        }
        public ActionResult CNRT(Guid Id)
        {

            List<MAT.Entities.PasajeroViaje> EPasajeroViaje = GetListPasajeroViaje(Id);
            return PartialView(EPasajeroViaje);
        }

        public ActionResult CNRTHojaUno()
        {
            return PartialView();
        }


        public ActionResult ListadoSimple(Guid Id)
        {

            //List<MAT.Entities.PasajeroViaje> EPasajeroViaje = new List<MAT.Entities.PasajeroViaje>();
            //try {
            //    EPasajeroViaje = GetListPasajeroViaje(Id);
            //    return PartialView(EPasajeroViaje);
            //}
            //catch {
            //    return PartialView(EPasajeroViaje);
            //}

            List<PasajeroViajeModel> LPasajeroViaje = new List<PasajeroViajeModel>();
            ViewBag.Paquete = new PaqueteStandard();
            try
            {
                LPasajeroViaje = MAT.MVC.Models.PasajeroViajeMethod.GetPasajeroViajeByViajeID(Id.ToString());

                ViewBag.Paquete = MAT.MVC.Models.PaqueteMethod.GetPaqueteByID(Id.ToString());

                return PartialView(LPasajeroViaje);
            }
            catch
            {
                return PartialView(LPasajeroViaje);
            }


        }

        public ActionResult ListadoSimpleToExport(Guid Id)
        {

            List<MAT.Entities.PasajeroViaje> EPasajeroViaje = new List<MAT.Entities.PasajeroViaje>();
            try
            {
                EPasajeroViaje = GetListPasajeroViaje(Id);
                return PartialView(EPasajeroViaje);
            }
            catch
            {
                return PartialView(EPasajeroViaje);
            }


        }

        public List<MAT.Entities.PasajeroViaje> GetListPasajeroViaje(Guid Id)
        {

            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@ViajeID", SqlDbType.VarChar, 0, Convert.ToString(Id)),
                    };
            SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_PasajeroViaje_GetByViajeID", dbParams);


            List<MAT.Entities.PasajeroViaje> EPasajeroViaje = new List<MAT.Entities.PasajeroViaje>();
            while (_reader.Read())
            {
                MAT.Entities.PasajeroViaje item = new MAT.Entities.PasajeroViaje();
                item.ViajeId = new Guid(_reader["ViajeID"].ToString());
                item.PersonaId = new Guid(_reader["PersonaID"].ToString());
                item.Apellido = _reader["Apellido"].ToString();
                item.Nombre = _reader["Nombre"].ToString();
                item.TipoDocumento = Convert.ToInt32(_reader["TipoDocumento"].ToString());
                item.NroDocumento = _reader["NroDocumento"].ToString();
                item.FechaNacimiento = Convert.ToDateTime(_reader["FechaNacimiento"].ToString());
                item.Telefono = _reader["Telefono"].ToString();
                item.Sexo = Convert.ToInt32(_reader["Sexo"]);
                item.Nacionalidad = _reader["Nacionalidad"].ToString();
                item.PaisResidencia = _reader["PaisResidencia"].ToString();
                item.Ocupacion = _reader["Ocupacion"].ToString();
                EPasajeroViaje.Add(item);

            }

            return EPasajeroViaje;
        }
        
    }
}
