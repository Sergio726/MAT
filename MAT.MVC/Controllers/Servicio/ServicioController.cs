using MAT.Services;
using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.MVC.Models;
using System.Data;


namespace MAT.MVC.Controllers.Servicio
{
    public class ServicioController : Controller
    {
        //
        // GET: /Servicio/

        public ActionResult Index()
        {
            List<ServicioModel> ListSevicios = new List<ServicioModel>();
            //ServiciosGetAll
            try
            {
                DataSet ds = MVC.Models.ServicioMethod.ServiciosGetAll();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ServicioModel item = new ServicioModel();
                    item.ServicioID = row["ServicioID"].ToString();
                    item.Descripcion = row["Descripcion"].ToString();
                    item.Precio = Convert.ToDecimal(row["Precio"].ToString());
                    item.Moneda = row["Moneda"].ToString();
                    ListSevicios.Add(item);
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message + e.StackTrace;
            }
            return View(ListSevicios);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection datos)
        {
            try
            {
                MAT.MVC.Models.ServicioModel Serv = new Models.ServicioModel();

                Serv.Descripcion = datos["Descripcion"];
                if (datos["Precio"] != "")
                {
                    Serv.Precio = Convert.ToDecimal(datos["Precio"]);
                }
                else
                {
                    Serv.Precio = 0;
                }
                Serv.Moneda = datos["Moneda"];
                Serv.TipoServicio = 2;
                Serv.ProveedorID = datos["ProveedorId"];
                Serv.TransporteID = datos["TransporteId"];

                MAT.MVC.Models.ServicioMethod.ServicioInsert(Serv);
                return RedirectToAction("Index");
            }
            catch (Exception e){
                ViewBag.Error = e.Message;
                return RedirectToAction("Create");
            }
            
        }

        public ActionResult Edit(Guid id)
        {

            return View(new Services.ServicioService().GetByServicioId(id));
        }

        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection datos)
        {
            Services.ServicioService service = new ServicioService();
            Entities.Servicio servicio = service.GetByServicioId(id);
            Helper.FillEntity<Entities.Servicio>(ref servicio, datos);
            service.Update(servicio);
            return RedirectToAction("Index");
        }

        public bool Delete(Guid id)
        {
            bool result = false;
            try
            {
                PaqueteServicioService paqservicioService = new PaqueteServicioService();
                List<Entities.PaqueteServicio> serviciosvinculados = paqservicioService.GetByServicioId(id).ToList();
                for (int i = serviciosvinculados.Count; i > 0; i--)
                {
                    var item = serviciosvinculados[i - 1];
                    paqservicioService.Delete(item.PaqueteServicioId);
                }
                ServicioService servicioService = new ServicioService();
                servicioService.Delete(id);
                result = true;
            }
            catch 
            {
                result = false;
            }
            return result;
        }
    }
}
