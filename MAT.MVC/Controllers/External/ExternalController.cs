using MAT.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MAT.MVC.Controllers
{
    public class ExternalController : Controller
    {
        //
        // GET: /External/
        [AllowAnonymous]
        public JsonResult PackagesGetAll(string Date1, string Date2, double Price1, double Price2)
        {
            var json = "";
            try
            {
                List<MAT.MVC.Models.ExternalModel.PaqueteInfo> listPaquete = new List<ExternalModel.PaqueteInfo>();
                listPaquete = ExternalMethod.GetPackages(Date1, Date2, Price1, Price2);

                var jsonSerialiser = new JavaScriptSerializer();
                json = jsonSerialiser.Serialize(listPaquete);
            }
            catch (Exception e){
                json = "Error: " + e.Message.ToString();
            }
           

            return Json(new
            {
                json
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
