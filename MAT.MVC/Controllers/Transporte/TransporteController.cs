using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.Services;
using MAT.Entities;

namespace MAT.MVC.Controllers.Transporte
{
    public class TransporteController : Controller
    {
        //
        // GET: /Transporte/

        public ActionResult Index()
        {
            //TransporteService STransporte = new TransporteService();
            //IList<Entities.Transporte> LTransporte = STransporte.GetAll().OrderBy(tr => tr.NroCoche).ToList();
            List<MAT.MVC.Models.TransporteModel> LTransporte = new List<Models.TransporteModel>();
            LTransporte = MAT.MVC.Models.TransporteMethod.GetListTransporte();
            return View(LTransporte);
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(Entities.Transporte Transporte)
        {
            TransporteService Stransporte = new TransporteService();
            Stransporte.Insert(Transporte);
            return RedirectToAction("Index", "Transporte");
        }

        public ActionResult Edit(Guid Id)
        {
            TransporteService Stransporte = new TransporteService();
            MAT.Entities.Transporte Transporte = Stransporte.Get(new TransporteKey( Id));
            return View(Transporte);
        }


        
        [HttpPost]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            /*Para editar los regiostros es necesario usar la siguiente esctructra, que es la que reconoce el
            el comando Update*/

            TransporteService Stransporte = new TransporteService();
            MAT.Entities.Transporte Transporte = Stransporte.Get(new TransporteKey(Id));
            try
            {
                if (!string.IsNullOrEmpty(collection.Get("NroCoche"))) Transporte.NroCoche = collection.Get("NroCoche");
                if (!string.IsNullOrEmpty(collection.Get("MaxPasajeros"))) Transporte.MaxPasajeros = Convert.ToInt16(collection.Get("MaxPasajeros"));
                if (!string.IsNullOrEmpty(collection.Get("KmRecorridos"))) Transporte.KmRecorridos = Convert.ToInt32(collection.Get("KmRecorridos"));
                if (!string.IsNullOrEmpty(collection.Get("UltimoService"))) Transporte.UltimoService = Convert.ToDateTime(collection.Get("UltimoService"));
                if (!string.IsNullOrEmpty(collection.Get("Matricula"))) Transporte.Matricula = collection.Get("Matricula");

                Stransporte.Update(Transporte);

               
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
            { 
            
            }

            return RedirectToAction("Index");

        }

        public ActionResult Delete(Guid Id)
        {
            try
            {
                TransporteService STransporte = new TransporteService();
                MAT.Entities.Transporte Transporte = STransporte.Get(new TransporteKey(Id));
                STransporte.Delete(Transporte);
            }
            catch 
            {
                string ex = "No se puede eliminar el transporte seleccionado, verifique que ninguna butaca este vinculada con este transporte";
                return PartialView("Error", ex);
            }
            return RedirectToAction("Index", "Transporte");
        }
    }
}
