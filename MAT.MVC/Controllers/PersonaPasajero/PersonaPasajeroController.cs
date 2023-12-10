using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.Entities;
using MAT.Services;
using MAT.Enums;
using MAT.MVC.Models;

namespace MAT.MVC.Controllers.PersonaPasajero
{
    public class PersonaPasajeroController : Controller
    {
        //
        // GET: /PersonaPasajero/

        public ActionResult Index()
        {
            PersonaPasajeroService srv = new PersonaPasajeroService();
            IList<MAT.Entities.PersonaPasajero> LPersonaPasajero = srv.GetAll();
            return View(LPersonaPasajero);
            
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            #region Servicios y Entidades
            PersonaPasajeroService SPersonaPasajero = new PersonaPasajeroService();
            PersonaService SPersona = new PersonaService();
            PasajeroService SPasajero = new PasajeroService();
            ClienteService SCliente = new ClienteService();

            MAT.Entities.PersonaPasajero PersonaPasajero =new MAT.Entities.PersonaPasajero();
            MAT.Entities.Persona Persona = new MAT.Entities.Persona();
            MAT.Entities.Pasajero Pasajero = new MAT.Entities.Pasajero();
            MAT.Entities.Cliente Cliente = new Entities.Cliente();
            #endregion

            #region Agregar Persona
            Persona.Apellido = collection.Get("Apellido").ToString();
            Persona.Nombre = collection.Get("Nombre").ToString();
            Persona.TipoDocumento = Convert.ToInt32(collection.Get("TipoDocumento").ToString());
            Persona.NroDocumento = collection.Get("NroDocumento").ToString();
            Persona.LocalidadId =Convert.ToInt16( collection.Get("LocalidadId").ToString());
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

            #region Agregar Pasajero
            Pasajero.PasajeroId = Persona.PersonaId;
            if(!String.IsNullOrEmpty(collection.Get("Pasaporte"))) Pasajero.Pasaporte = collection.Get("Pasaporte").ToString();
            if (!String.IsNullOrEmpty(collection.Get("VencimientoPasaporte"))) Pasajero.VencimientoPasaporte = Convert.ToDateTime( collection.Get("VencimientoPasaporte").ToString());
            if (!String.IsNullOrEmpty(collection.Get("EmisionPasaporte"))) Pasajero.EmisionPasaporte = Convert.ToDateTime( collection.Get("EmisionPasaporte").ToString());
            if(!String.IsNullOrEmpty(collection.Get("PaisOrigen"))) Pasajero.PaisOrigen = collection.Get("PaisOrigen").ToString();

            SPasajero.Save(Pasajero);
            #endregion

            #region Agregar Cliente
            bool escliente = collection.Get("escliente") == "on" ? true : false;
            if (escliente)
            {
                Cliente.RazonSocial = String.Format("{0} {1}", Persona.Nombre, Persona.Apellido);
                Cliente.ClienteId = Persona.PersonaId;
                Cliente.FormaPago = (int)eFormaPago.Contado;
                Cliente.TipoId = (int)eTipoCliente.Minorista;
                Cliente.CondicionIva = (int)eCondicionIVA.Consumidor_Final;
                SCliente.Insert(Cliente);
            }
            #endregion

            #region CuentaCorriente
            CuentaService SCuenta = new CuentaService();
            MAT.Entities.Cuenta ECuenta = new MAT.Entities.Cuenta();
            ECuenta.ClienteId = Persona.PersonaId;
           // ECuenta.Estado = true;
            SCuenta.Insert(ECuenta);
            #endregion

            return RedirectToAction("Index", "PersonaPasajero");

        }

        public ActionResult Edit(Guid Id)
        {
            #region Servicios y Entidades
            PersonaPasajeroService SPersonaPasajero = new PersonaPasajeroService();
            PersonaService SPersona = new PersonaService();
            PasajeroService SPasajero = new PasajeroService();

            MAT.Entities.PersonaPasajero PersonaPasajero = new MAT.Entities.PersonaPasajero();
            MAT.Entities.Persona Persona = SPersona.Get(new PersonaKey(Id));
            MAT.Entities.Pasajero Pasajero = SPasajero.Get(new PasajeroKey(Id));
            #endregion

            #region Actualizar PersonaPasajero
            PersonaPasajero.Apellido = Persona.Apellido;
            PersonaPasajero.Nombre = Persona.Nombre;
            PersonaPasajero.TipoDocumento = Persona.TipoDocumento;
            PersonaPasajero.NroDocumento = Persona.NroDocumento;
            PersonaPasajero.LocalidadId = Persona.LocalidadId;
            PersonaPasajero.Telefono = Persona.Telefono;
            PersonaPasajero.Email = Persona.Email;
            PersonaPasajero.FechaNacimiento = Persona.FechaNacimiento;
            PersonaPasajero.Sexo = Persona.Sexo;
            PersonaPasajero.Pasaporte = Pasajero.Pasaporte;
            PersonaPasajero.VencimientoPasaporte = Pasajero.VencimientoPasaporte;
            PersonaPasajero.EmisionPasaporte = Pasajero.EmisionPasaporte;
            PersonaPasajero.PaisOrigen = Pasajero.PaisOrigen;

            #endregion

            return View(PersonaPasajero);
        }

        [HttpPost]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            #region Servicios y Entidades
            PersonaPasajeroService SPersonaPasajero = new PersonaPasajeroService();
            PersonaService SPersona = new PersonaService();
            PasajeroService SPasajero = new PasajeroService();

            MAT.Entities.PersonaPasajero PersonaPasajero = new MAT.Entities.PersonaPasajero();
            MAT.Entities.Persona Persona = SPersona.Get(new PersonaKey(Id));
            MAT.Entities.Pasajero Pasajero = SPasajero.Get(new PasajeroKey(Id));
            #endregion

            try
            {
                #region Agregar Persona
                Persona.Apellido = collection.Get("Apellido").ToString();
                Persona.Nombre = collection.Get("Nombre").ToString();
                Persona.TipoDocumento = Convert.ToInt32( collection.Get("TipoDocumento").ToString());
                Persona.NroDocumento = collection.Get("NroDocumento").ToString();
                Persona.LocalidadId = (Convert.ToInt32( collection.Get("LocalidadId").ToString()));
                Persona.Telefono = collection.Get("Telefono").ToString();
                Persona.Email = collection.Get("Email").ToString();

                string fecNac = collection.Get("FechaNacimiento").ToString();
                if (fecNac != "")
                {
                    Persona.FechaNacimiento = Convert.ToDateTime(collection.Get("FechaNacimiento").ToString());
                }

                Persona.Sexo = Convert.ToInt32(collection.Get("Sexo").ToString());
                SPersona.Update(Persona);
                #endregion

                #region Agregar Pasajero

                Pasajero.Pasaporte = collection.Get("Pasaporte").ToString();
                if (collection.Get("VencimientoPasaporte").ToString() != "")
                {
                    try {
                        DateTime FechaV = new DateTime();
                        FechaV = Convert.ToDateTime(collection.Get("VencimientoPasaporte").ToString());
                        Pasajero.VencimientoPasaporte = FechaV; }
                    catch { }
                }

                if (collection.Get("EmisionPasaporte").ToString() != "")
                {
                    try {Pasajero.EmisionPasaporte = Convert.ToDateTime(collection.Get("EmisionPasaporte").ToString()); }
                    catch { }
                }
                
                Pasajero.PaisOrigen = collection.Get("PaisOrigen").ToString();

                SPasajero.Update(Pasajero);
                #endregion

                #region Actualizar PersonaPasajero
                PersonaPasajero.Apellido = Persona.Apellido;
                PersonaPasajero.Nombre = Persona.Nombre;
                PersonaPasajero.TipoDocumento = Persona.TipoDocumento;
                PersonaPasajero.NroDocumento = Persona.NroDocumento;
                PersonaPasajero.LocalidadId = Persona.LocalidadId;
                PersonaPasajero.Telefono = Persona.Telefono;
                PersonaPasajero.Email = Persona.Email;
                PersonaPasajero.FechaNacimiento = Persona.FechaNacimiento;
                PersonaPasajero.Sexo = Persona.Sexo;
                PersonaPasajero.Pasaporte = Pasajero.Pasaporte;
                PersonaPasajero.VencimientoPasaporte = Pasajero.VencimientoPasaporte;
                PersonaPasajero.EmisionPasaporte = Pasajero.EmisionPasaporte;
                PersonaPasajero.PaisOrigen = Pasajero.PaisOrigen;

                #endregion
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
            { 
            
            }
            return RedirectToAction("Details/" + (Persona.PersonaId) + "/", "PersonaPasajero");
        }

        public ActionResult Details(Guid Id)
        {
            #region Servicios y Entidades
            PersonaPasajeroService SPersonaPasajero = new PersonaPasajeroService();
            PersonaService SPersona = new PersonaService();
            PasajeroService SPasajero = new PasajeroService();

            MAT.Entities.PersonaPasajero PersonaPasajero = new MAT.Entities.PersonaPasajero();
            MAT.Entities.Persona Persona = SPersona.Get(new PersonaKey(Id));
            MAT.Entities.Pasajero Pasajero = SPasajero.Get(new PasajeroKey(Id));
            #endregion

            #region Actualizar PersonaPasajero
            PersonaPasajero.PersonaId = Persona.PersonaId;
            PersonaPasajero.Apellido = Persona.Apellido;
            PersonaPasajero.Nombre = Persona.Nombre;
            PersonaPasajero.TipoDocumento = Persona.TipoDocumento;
            PersonaPasajero.NroDocumento = Persona.NroDocumento;
            PersonaPasajero.LocalidadId = Persona.LocalidadId;
            PersonaPasajero.Telefono = Persona.Telefono;
            PersonaPasajero.Email = Persona.Email;
            PersonaPasajero.FechaNacimiento = Persona.FechaNacimiento;
            PersonaPasajero.Sexo = Persona.Sexo;
            PersonaPasajero.PasajeroId = Pasajero.PasajeroId;
            PersonaPasajero.Pasaporte = Pasajero.Pasaporte;
            PersonaPasajero.VencimientoPasaporte = Pasajero.VencimientoPasaporte;
            PersonaPasajero.EmisionPasaporte = Pasajero.EmisionPasaporte;
            PersonaPasajero.PaisOrigen = Pasajero.PaisOrigen;

            #endregion

            return View(PersonaPasajero);
        }

        public ActionResult Delete(Guid Id)
        {
            #region Servicios y Entidades
            
            PersonaService SPersona = new PersonaService();
            PasajeroService SPasajero = new PasajeroService();
            MAT.Entities.Persona Persona = SPersona.Get(new PersonaKey(Id));
            MAT.Entities.Pasajero Pasajero = SPasajero.Get(new PasajeroKey(Id));
            #endregion

            SPasajero.Delete(Pasajero);
            SPersona.Delete(Persona);
            
            return RedirectToAction("Index", "PersonaPasajero");
        }

        public ActionResult PartialPasajerosHistorial(Guid pasajeroid)
        {
            PasajeroHistorialModel historial = new PasajeroHistorialModel(pasajeroid);
            return PartialView(historial);
        }
    }

   
}
