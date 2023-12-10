using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.Entities;
using MAT.Services;
using PagedList;

namespace MAT.MVC.Controllers.Butaca
{
    public class ButacaController : Controller
    {
        //
        // GET: /Butaca/

        public ActionResult Index(int? page,string searchString)
        {            
            ButacaService SButaca = new ButacaService();
            IList<MAT.Entities.Butaca> Butaca = new List<MAT.Entities.Butaca>();

            if (!String.IsNullOrEmpty(searchString))
            {
                Butaca = SButaca.GetByTransporteId(new Guid(searchString)).OrderByDescending(b => b.NroButaca).ToList();
            }
            else
            {

                Butaca = SButaca.GetAll().OrderBy(b => b.TransporteId).OrderByDescending(b => b.NroButaca).ToList();
            }
           


            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1);

            return View(Butaca.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create(string TransporteId)
        {
            
            MAT.Entities.Butaca EButaca =new  MAT.Entities.Butaca();
            if (TransporteId == null)
            {
                EButaca = new MAT.Entities.Butaca();
            }

            else
            {
                if (TransporteId != null)
                {
                    Guid TrId = new Guid(TransporteId);
                    TransporteService STransporte = new TransporteService();
                    MAT.Entities.Transporte ETransporte = STransporte.GetByTransporteId(TrId);
                    EButaca.TransporteId = TrId;
                }

            }
            return View(EButaca);
        }

        [HttpPost]
        public ActionResult Create(Entities.Butaca EButaca)
        {
            ButacaService SButaca = new ButacaService();
            SButaca.Insert(EButaca);

            //Vuelve a la lista de butacas, filtrando el transporte seleccionado
            return RedirectToAction("Index", "Butaca", new { SearchString = EButaca.TransporteId.ToString() });
        }

        public ActionResult Edit(Guid Id)
        {
            ButacaService SButaca = new ButacaService();
            MAT.Entities.Butaca EButaca = SButaca.GetByButacaId(Id);
            return View(EButaca);
        }

        [HttpPost]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            ButacaService SButaca = new ButacaService();
            MAT.Entities.Butaca EButaca = SButaca.GetByButacaId(Id);

            EButaca.NroButaca = Convert.ToInt32( collection.Get("NroButaca"));
            EButaca.Piso = Convert.ToInt32(collection.Get("Piso"));
            EButaca.Ubicacion = Convert.ToInt32(collection.Get("Ubicacion"));
            EButaca.Tipo = Convert.ToInt32(collection.Get("Tipo"));
            EButaca.TransporteId = new Guid(collection.Get("TransporteId"));
            EButaca.Fila = collection.Get("Fila");
            EButaca.Posicion = collection.Get("Posicion");
            EButaca.CodigoButaca = collection.Get("CodigoButaca");

            SButaca.Update(EButaca);
            return RedirectToAction("Details/" + EButaca.ButacaId.ToString(), "Butaca");
        }

        public ActionResult Details(Guid Id)
        {
            MAT.Services.ButacaService SButaca = new ButacaService();
            MAT.Entities.Butaca EButaca = SButaca.GetByButacaId(Id);
            return View(EButaca);
        }

        public ActionResult Delete(Guid Id)
        {
            try
            {
                MAT.Services.ButacaService SButaca = new ButacaService();
                SButaca.Delete(Id);
               
            }
            catch (Exception e)
            {
                return PartialView("Error", e.Message);
            }
            return RedirectToAction("Index", "Butaca");
        }
    
    }
}
