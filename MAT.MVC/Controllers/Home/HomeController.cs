using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.Entities;
using MAT.Services;
using System.Text;
using MAT.MVC.Models;
using WebMatrix.WebData;
using PagedList;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;
using MAT.Utilities;
using System.Configuration;

namespace MAT.MVC.Controllers.Home
{
    public class HomeController : Controller
    {
        //private readonly PaqueteDetalleService _paqueteDetalleService;

        //public HomeController()
        //{
        //    _paqueteDetalleService = new PaqueteDetalleService(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        //}

        //
        // GET: /Home/

        [Authorize]
        public ActionResult Index()
        {
            if (!WebSecurity.Initialized)
            {                
                return RedirectToAction("Login", "Account");
            }
            if (Roles.IsUserInRole(User.Identity.Name, "Administrador")) return RedirectToAction("Index", "Admin");
            return View();
        }

        [Authorize]
        public ActionResult ViajesPorFecha()
        {
            return View();
        }

        public string GetFechasDeViajes()
        {
            ViajeService vServ = new ViajeService();
            List<Entities.Viaje> viajesdefecha = vServ.GetAll().OrderByDescending(v => v.FechaSalida).ToList();
            StringBuilder arrayfechas = new StringBuilder();
            foreach (var item in viajesdefecha)
            {
                arrayfechas.Append(item.FechaSalida.Value.ToString("yyyy-MM-dd"));
                arrayfechas.Append(",");
            }
            arrayfechas.Remove(arrayfechas.Length - 1, 1);
            return arrayfechas.ToString();
        }

        [Authorize]
        public ActionResult TodosLosViajesIndex()
        {
            ViajeService vServ = new ViajeService();
            int yearinicio = vServ.GetAll().OrderBy(v => v.FechaSalida).FirstOrDefault().FechaSalida.Value.Year;
            int yearfinal = vServ.GetAll().OrderByDescending(v => v.FechaSalida).FirstOrDefault().FechaSalida.Value.Year;
            List<SelectListItem> yearlistitem = new List<SelectListItem>();
            for (int i = yearinicio; i < yearfinal +1 ; i++)
            {
                SelectListItem item = new SelectListItem();
                item.Text = i.ToString();
                item.Value = i.ToString();
                yearlistitem.Add(item);
            }
            return View(yearlistitem);
        }

        [Authorize]
        public ActionResult TodosLosViajes(string yearfilter)
        {
            //ViajeService vServ = new ViajeService();
            //int year = Convert.ToInt32(yearfilter);
            //List<Entities.Viaje> viajesdefecha = vServ.GetAll().Where(vf => vf.FechaSalida.Value.Year==year).OrderByDescending(v => v.FechaSalida).ToList();
            //List<PaqueteModel> paquetes = new List<PaqueteModel>();
            //foreach (Entities.Viaje viaje in viajesdefecha)
            //{
            //    paquetes.Add(new PaqueteModel(viaje.ViajeId));
            //}

            List<ViajeModel> Viajes = new List<ViajeModel>();

            Viajes = MVC.Models.ViajeMethod.ViajeByDate(null, Convert.ToInt32(yearfilter));


            //return PartialView(paquetes);
            return PartialView(Viajes);
        }

        public ActionResult GenerarPasajes()
        {

            return null;
        }

        /// <summary>
        /// Metodo que devuelve una lista html de los viajes-paquetes
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public string GetViajes(string fecha)
        {
            try
            {
                
                DateTime datefecha = Convert.ToDateTime(fecha);

                // Creamos una lista simulada de viajes
                List<ViajeModel> Viajes = new List<ViajeModel>();

                Viajes = MVC.Models.ViajeMethod.ViajeByDate(fecha);
                                 
                // Generamos el HTML
                StringBuilder htmlstring = new StringBuilder();
                htmlstring.Append("<ul class='viajes-lista'>");
                foreach (var item in Viajes)
                {
                    htmlstring.Append("<li data-image='" + "fotoviaje" + "' style='background-image:url(" + "foto" + ");'>");
                    htmlstring.Append("<a href='#' class='btn_viaje' data-id=" + item.ViajeID + ">");
                    htmlstring.Append("<div class='viaje-container'>");
                    htmlstring.Append("<h3>"); htmlstring.Append(item.Descripcion); htmlstring.Append("</h3>");
                    if (!String.IsNullOrEmpty(item.Descripcion)) htmlstring.Append("<h5>" + item.Descripcion + "</h5>");

                    htmlstring.Append("<h5>"); htmlstring.Append(String.Format("Destino: {0}", item.PaqueteNombre)); htmlstring.Append("</h5>");
                    htmlstring.Append("<h6>"); htmlstring.Append(String.Format("Salida: {0}", item.FechaSalida.ToString())); htmlstring.Append("</h6>");
                    htmlstring.Append("</div></a></li>");
                }

                htmlstring.Append("</ul>");
                return htmlstring.ToString();
            }
            
            catch (Exception ex)
            {
                StringBuilder excepcion = new StringBuilder();
                excepcion.AppendLine(ex.Message);
                excepcion.AppendLine(ex.Source);
                excepcion.AppendLine(ex.StackTrace);
                return excepcion.ToString();
            }
           
        }

        public JsonResult QuickLocalidadSearch(string query, string sIdProvincia = "")
        {
            VLocalidadService localidadService = new VLocalidadService();
            List<Entities.VLocalidad> _localidades = localidadService.GetAll().Where(loc => loc.Nombre.ToUpper().Contains(query.ToUpper())).ToList();
            
            return Json(_localidades, JsonRequestBehavior.AllowGet);
        }

        public ActionResult HistorialPagos()
        {
            return View();
        }

        public ActionResult RenderGridHistorialPagos(int? page, string filter)
        {
            List<Entities.Historial> historial = new Services.HistorialService().GetAll().ToList();
            IList<HistorialModel> historialModel = new List<HistorialModel>();
            foreach (var item in historial)
            {
                historialModel.Add(new HistorialModel(item.HistorialId));
            }
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            if (!string.IsNullOrEmpty(filter))
            {
                historialModel = historialModel.Where(hi => hi.Cliente.Nombre.ToUpper().Contains(filter.ToUpper()) || hi.Cliente.Apellido.ToUpper().Contains(filter.ToUpper()) || hi.Vendedor.Nombre.Contains(filter) || hi.Vendedor.Apellido.Contains(filter)).ToList();
            }
            return PartialView(historialModel.ToPagedList(pageNumber,pageSize));
        }

       
    }
}
