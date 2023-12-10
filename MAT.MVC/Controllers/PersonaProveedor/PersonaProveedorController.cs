using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.Entities;
using MAT.Services;
using MAT.Utilities;

namespace MAT.MVC.Controllers.PersonaProveedor
{
    public class PersonaProveedorController : Controller
    {
        //
        // GET: /PersonaProveedor/

        public ActionResult Index()
        {
            PersonaProveedorService SPersonaProv = new PersonaProveedorService();
            IList<MAT.Entities.PersonaProveedor> PersonaProv = SPersonaProv.GetAll();
            return View(PersonaProv);
            
        }

        public ActionResult Create()
        {

           return View(); 
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            #region Entidades y Servicios
            PersonaProveedorService SPersonaProv = new PersonaProveedorService();
            PersonaService SPersona = new PersonaService();
            ProveedorService SProv = new ProveedorService();

            MAT.Entities.PersonaProveedor PersonaProv = new MAT.Entities.PersonaProveedor();
            MAT.Entities.Persona Persona = new MAT.Entities.Persona();
            MAT.Entities.Proveedor Prov = new MAT.Entities.Proveedor();

            #endregion

            #region Agregar Persona
            Persona.Apellido = collection.Get("Apellido").ToString();
            Persona.Nombre = collection.Get("Nombre").ToString();
            Persona.TipoDocumento = Convert.ToInt32(collection.Get("TipoDocumento").ToString());
            Persona.NroDocumento = collection.Get("NroDocumento").ToString();
            Persona.LocalidadId =Convert.ToInt32(collection.Get("LocalidadId").ToString().Trim(','));
            Persona.Telefono = collection.Get("Telefono").ToString();
            Persona.Email = collection.Get("Email").ToString();
            string FecNac = collection.Get("FechaNacimiento").ToString();
            if (FecNac != "")
            {
                Persona.FechaNacimiento = Convert.ToDateTime(collection.Get("FechaNacimiento").ToString());
            }

            Persona.Sexo = Convert.ToInt32(collection.Get("Sexo").ToString());

            SPersona.Insert(Persona);
            #endregion

            #region Agregar Proveedor
            Prov.ProveedorId = Persona.PersonaId;
            if (!string.IsNullOrEmpty(collection.Get("RazonSocial"))) Prov.RazonSocial = collection.Get("RazonSocial").ToString();
            if (!string.IsNullOrEmpty(collection.Get("LocalidadEmpresa"))) Prov.LocalidadId = Convert.ToInt16(collection.Get("LocalidadEmpresa").ToString());
            if (!string.IsNullOrEmpty(collection.Get("ProveedorTelefono"))) Prov.Telefono = collection.Get("ProveedorTelefono").ToString();
            if (!string.IsNullOrEmpty(collection.Get("Fax"))) Prov.Fax = collection.Get("Fax").ToString();
            if (!string.IsNullOrEmpty(collection.Get("Web"))) Prov.Web = collection.Get("Web").ToString();
            if (!string.IsNullOrEmpty(collection.Get("ProveedorMail"))) Prov.Email = collection.Get("ProveedorMail");
            if (!string.IsNullOrEmpty(collection.Get("Idioma"))) Prov.Idioma = collection.Get("Idioma").ToString();
            if (!string.IsNullOrEmpty(collection.Get("CondicionIva"))) Prov.CondicionIva = Convert.ToInt16(collection.Get("CondicionIva"));
            if (!string.IsNullOrEmpty(collection.Get("Cuit"))) Prov.Cuit = collection.Get("Cuit").ToString();
            if (!string.IsNullOrEmpty(collection.Get("FormaPago"))) Prov.FormaPago = Convert.ToInt16(collection.Get("FormaPago").ToString());
            SProv.Save(Prov);
            #endregion

            return RedirectToAction("Index", "PersonaProveedor");
        }

        public ActionResult Edit(Guid Id)
        {
            #region Entidades y Servicios
            PersonaProveedorService SPersonaProv = new PersonaProveedorService();
            PersonaService SPersona = new PersonaService();
            ProveedorService SProv = new ProveedorService();

            MAT.Entities.PersonaProveedor PersonaProv = new MAT.Entities.PersonaProveedor();
            MAT.Entities.Persona Persona = SPersona.Get(new PersonaKey(Id));
            MAT.Entities.Proveedor Prov = SProv.Get(new ProveedorKey(Id));

            #endregion
            /* Debo actualizar la entidad PersonaProveedor ya que esta no pertenece a la 
            base de datos */
            #region Agregar PersonaProveedor
            PersonaProv.Apellido = Persona.Apellido;
            PersonaProv.Nombre = Persona.Nombre;
            PersonaProv.TipoDocumento = Persona.TipoDocumento;
            PersonaProv.NroDocumento = Persona.NroDocumento;
            PersonaProv.LocalidadId = Persona.LocalidadId;
            PersonaProv.Telefono = Persona.Telefono;
            PersonaProv.Email = Persona.Email;
            PersonaProv.FechaNacimiento = Persona.FechaNacimiento;
            PersonaProv.Sexo = Persona.Sexo;

            PersonaProv.RazonSocial = Prov.RazonSocial;
            PersonaProv.ProveedorLocalidadId = Prov.LocalidadId;
            PersonaProv.Telefono = Prov.Telefono;
            PersonaProv.Fax = Prov.Fax;
            PersonaProv.Web = Prov.Web;
            PersonaProv.ProveedorEmail = Prov.Email;
            PersonaProv.Idioma = Prov.Idioma;
            PersonaProv.CondicionIva = Prov.CondicionIva;
            PersonaProv.Cuit = Prov.Cuit;
            PersonaProv.FormaPago = Prov.FormaPago;
                 
            #endregion

            return View(PersonaProv);
        }

        [HttpPost]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            #region Entidades y Servicios
            PersonaProveedorService SPersonaProv = new PersonaProveedorService();
            PersonaService SPersona = new PersonaService();
            ProveedorService SProv = new ProveedorService();

            MAT.Entities.PersonaProveedor PersonaProv = new MAT.Entities.PersonaProveedor();
            MAT.Entities.Persona Persona = SPersona.Get(new PersonaKey(Id));
            MAT.Entities.Proveedor Prov = SProv.Get(new ProveedorKey(Id));

            #endregion
            /* Debo actualizar la entidad PersonaProveedor ya que esta no pertenece a la 
            base de datos */

            #region Actualizar Persona
            Helper.FillEntity<MAT.Entities.Persona>(ref Persona, collection);
            SPersona.Update(Persona);
            #endregion

            #region Actualizar Proveedor
            if (!string.IsNullOrEmpty(collection.Get("RazonSocial"))) Prov.RazonSocial = collection.Get("RazonSocial").ToString();
            if (!string.IsNullOrEmpty(collection.Get("LocalidadEmpresa"))) Prov.LocalidadId = Convert.ToInt16(collection.Get("LocalidadEmpresa").ToString());
            if (!string.IsNullOrEmpty(collection.Get("ProveedorTelefono"))) Prov.Telefono = collection.Get("ProveedorTelefono").ToString();
            if (!string.IsNullOrEmpty(collection.Get("Fax"))) Prov.Fax = collection.Get("Fax").ToString();
            if (!string.IsNullOrEmpty(collection.Get("Web"))) Prov.Web = collection.Get("Web").ToString();
            if (!string.IsNullOrEmpty(collection.Get("ProveedorEmail"))) Prov.Email = collection.Get("ProveedorEmail").ToString();
            if (!string.IsNullOrEmpty(collection.Get("Idioma"))) Prov.Idioma = collection.Get("Idioma").ToString();
            if (!string.IsNullOrEmpty(collection.Get("CondicionIva"))) Prov.CondicionIva = Convert.ToInt16(collection.Get("CondicionIva"));
            if (!string.IsNullOrEmpty(collection.Get("Cuit"))) Prov.Cuit = collection.Get("Cuit").ToString();
            if (!string.IsNullOrEmpty(collection.Get("FormaPago"))) Prov.FormaPago = Convert.ToInt16(collection.Get("FormaPago"));

            SProv.Update(Prov);

            #endregion


            return RedirectToAction("Details/" + Persona.PersonaId + "/", "PersonaProveedor");
        }
        
        public ActionResult Details(Guid Id)
        {
            #region Entidades y Servicios
            PersonaProveedorService SPersonaProv = new PersonaProveedorService();
            PersonaService SPersona = new PersonaService();
            ProveedorService SProv = new ProveedorService();

            MAT.Entities.PersonaProveedor PersonaProv = new MAT.Entities.PersonaProveedor();
            MAT.Entities.Persona Persona = SPersona.Get(new PersonaKey(Id));
            MAT.Entities.Proveedor Prov = SProv.Get(new ProveedorKey(Id));

            #endregion
            
            #region Actualizar PersonaProveedor
            PersonaProv.PersonaId = Persona.PersonaId;
            PersonaProv.Apellido = Persona.Apellido;
            PersonaProv.Nombre = Persona.Nombre;
            PersonaProv.TipoDocumento = Persona.TipoDocumento;
            PersonaProv.NroDocumento = Persona.NroDocumento;
            PersonaProv.LocalidadId = Persona.LocalidadId;
            PersonaProv.Telefono = Persona.Telefono;
            PersonaProv.Email = Persona.Email;
            PersonaProv.FechaNacimiento = Persona.FechaNacimiento;
            PersonaProv.Sexo = Persona.Sexo;

            PersonaProv.RazonSocial =Prov.RazonSocial;
            PersonaProv.ProveedorLocalidadId = Prov.LocalidadId;
            PersonaProv.Telefono = Prov.Telefono;
            PersonaProv.Fax = Prov.Fax;
            PersonaProv.Web = Prov.Web;
            PersonaProv.ProveedorEmail = Prov.Email;
            PersonaProv.Idioma = Prov.Idioma;
            PersonaProv.CondicionIva = Prov.CondicionIva;
            PersonaProv.Cuit = Prov.Cuit;
            PersonaProv.FormaPago = Prov.FormaPago;

            #endregion

            return View(PersonaProv);
        }

        public ActionResult Delete(Guid Id)
        { 
            #region Servicios
            PersonaProveedorService SPersonaProv = new PersonaProveedorService();
            PersonaService SPersona = new PersonaService();
            ProveedorService SProv = new ProveedorService();
                        

            #endregion

            SProv.Delete(Id);
            SPersona.Delete(Id);

            return RedirectToAction("Index", "PersonaProveedor");
        }
    }
}
