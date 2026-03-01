using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.Entities;
using MAT.Services;
using MAT.Utilities;
using System.Web.Services;
using MAT.MVC.Models;
using MAT.MVC.Common;
using PagedList;
using MAT.Enums;
using System.Data.SqlClient;
using System.Data;
using MAT.MVC.Filters;
using System.Text;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using MAT.MVC.Infrastructure;
using System.Configuration;

namespace MAT.MVC.Controllers.PersonaCliente
{
    public class PersonaClienteController : Controller
    {
       
        public ActionResult RenderGridClientes()
        {
            List<PersonaClienteModel> LPersonaCliente = new List<PersonaClienteModel>();
            try
            {
                LPersonaCliente = PersonaClienteMethod.PersonaClienteGetAll();
               
                var jsonPatientList = JsonConvert.SerializeObject(LPersonaCliente);
                ViewBag.sbDataSetJson = jsonPatientList.ToString();

                return PartialView();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return PartialView();
            }
            
        }


        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Obtiene los TOP 10 clientes con búsqueda optimizada para mostrar en tarjetas
        /// </summary>
        /// <param name="searchTerm">Término de búsqueda (opcional)</param>
        /// <returns>JSON con lista de clientes</returns>
        [HttpPost]
        [Authorize]
        public JsonResult GetClientesTop(string searchTerm = "")
        {
            try
            {
                // Limpiar término de búsqueda
                if (string.IsNullOrWhiteSpace(searchTerm))
                    searchTerm = "";

                // Obtener top 10 clientes
                List<PersonaClienteModel> clientes = PersonaClienteMethod.PersonaClienteGetTop(searchTerm, 10);

                return Json(new
                {
                    success = true,
                    data = clientes,
                    count = clientes.Count
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message,
                    data = new List<PersonaClienteModel>(),
                    count = 0
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult Create(string msj)
        {
            if (!string.IsNullOrEmpty(msj)) ViewData["error"] = msj;
            ViewBag.ListOcupacion = GetAllOcupacion();
            ViewBag.ListProvincia = GetAllProvincia();
            return View();
        }

        /// <summary>
        /// Crea un nuevo cliente con datos pre-cargados desde un presupuesto (prospecto que decide comprar)
        /// </summary>
        [Authorize]
        public ActionResult CreateFromPresupuesto(Guid? presupuestoId, string returnUrl = null)
        {
            ViewBag.ListOcupacion = GetAllOcupacion();
            ViewBag.ListProvincia = GetAllProvincia();
            ViewBag.ReturnUrl = returnUrl;

            if (!presupuestoId.HasValue)
            {
                ViewData["error"] = "No se especificó el presupuesto.";
                return View("Create");
            }

            var presupuesto = MAT.MVC.Models.PresupuestoMethod.GetById(presupuestoId.Value);
            if (presupuesto == null)
            {
                ViewData["error"] = "Presupuesto no encontrado.";
                return View("Create");
            }

            var model = new MAT.Entities.PersonaCliente();
            model.Nacionalidad = "ARG";
            model.PaisResidencia = "ARG";
            model.Domicilio = "S/D";

            if (!string.IsNullOrWhiteSpace(presupuesto.NombreCliente))
            {
                var nombreCompleto = presupuesto.NombreCliente.Trim();
                if (nombreCompleto.Contains(","))
                {
                    var partes = nombreCompleto.Split(new[] { ',' }, 2);
                    model.Apellido = partes[0].Trim();
                    model.Nombre = partes.Length > 1 ? partes[1].Trim() : string.Empty;
                }
                else
                {
                    var palabras = nombreCompleto.Split(new[] { ' ' }, 2);
                    model.Apellido = palabras[0].Trim();
                    model.Nombre = palabras.Length > 1 ? palabras[1].Trim() : string.Empty;
                }
            }

            if (!string.IsNullOrWhiteSpace(presupuesto.TelefonoCliente))
                model.Celular = presupuesto.TelefonoCliente.Trim();
            if (!string.IsNullOrWhiteSpace(presupuesto.EmailCliente))
                model.Email = presupuesto.EmailCliente.Trim();
            if (!string.IsNullOrWhiteSpace(presupuesto.DniCliente))
                model.NroDocumento = presupuesto.DniCliente.Trim();

            return View("Create", model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(FormCollection collection)
        {
            PersonaClienteModel PersonaCliente = new PersonaClienteModel();
            try
            {

                #region Persona
                PersonaCliente.Apellido = collection.Get("Apellido").ToString().ToUpper();
                PersonaCliente.Nombre = collection.Get("Nombre").ToString();
                PersonaCliente.TipoDocumento = Convert.ToInt32(collection.Get("TipoDocumento").ToString());
                PersonaCliente.NroDocumento = collection.Get("NroDocumento").ToString();
                if (!string.IsNullOrEmpty(collection.Get("LocalidadId"))) PersonaCliente.LocalidadID = Convert.ToInt32(collection.Get("LocalidadId").ToString());
                if (!string.IsNullOrEmpty(collection.Get("Provincia"))) PersonaCliente.Provincia = Convert.ToInt32(collection.Get("Provincia"));
                if (!string.IsNullOrEmpty(collection.Get("Telefono"))) PersonaCliente.Telefono = collection.Get("Telefono").ToString();
                if (!string.IsNullOrEmpty(collection.Get("Celular"))) PersonaCliente.Celular = collection.Get("Celular").ToString();
                if (!string.IsNullOrEmpty(collection.Get("Email"))) PersonaCliente.Email = collection.Get("Email").ToString();
                if (!string.IsNullOrEmpty(collection.Get("Domicilio"))) PersonaCliente.Domicilio = collection.Get("Domicilio");
                if (!string.IsNullOrEmpty(collection.Get("Ocupacion"))) PersonaCliente.Ocupacion = collection.Get("Ocupacion").ToString();
                if (!string.IsNullOrEmpty(collection.Get("FechaNacimiento"))) PersonaCliente.FechaNacimiento = Convert.ToDateTime(collection.Get("FechaNacimiento"));
                if (!string.IsNullOrEmpty(collection.Get("Sexo"))) PersonaCliente.Sexo = Convert.ToInt32(collection.Get("Sexo").ToString());
                if (!string.IsNullOrEmpty(collection.Get("Nacionalidad"))) PersonaCliente.Nacionalidad = collection.Get("Nacionalidad").ToString();
                if (!string.IsNullOrEmpty(collection.Get("PaisResidencia"))) PersonaCliente.PaisResidencia = collection.Get("PaisResidencia").ToString();
                #endregion

                #region Cliente
                if (!string.IsNullOrEmpty(collection.Get("RazonSocial"))) PersonaCliente.RazonSocial = collection.Get("RazonSocial").ToString().ToUpper();
                if (!string.IsNullOrEmpty(collection.Get("Cuit"))) PersonaCliente.Cuit = collection.Get("Cuit").ToString();
                if (!string.IsNullOrEmpty(collection.Get("Empresa"))) PersonaCliente.Empresa = collection.Get("Empresa").ToString();
                if (!string.IsNullOrEmpty(collection.Get("FormaPago"))) PersonaCliente.FormaPago = Convert.ToInt32(collection.Get("FormaPago"));
                if (!string.IsNullOrEmpty(collection.Get("CondicionIva"))) PersonaCliente.CondicionIva = Convert.ToInt32(collection.Get("CondicionIva"));
                if (!string.IsNullOrEmpty(collection.Get("Fax"))) PersonaCliente.Fax = collection.Get("Fax").ToString();
                if (!string.IsNullOrEmpty(collection.Get("Web"))) PersonaCliente.Web = collection.Get("Web").ToString();
                if (!string.IsNullOrEmpty(collection.Get("Idioma"))) PersonaCliente.Idioma = collection.Get("Idioma").ToString();
                if (!string.IsNullOrEmpty(collection.Get("Promotor"))) PersonaCliente.Promotor = collection.Get("Promotor").ToString();
                if (!string.IsNullOrEmpty(collection.Get("Observacion"))) PersonaCliente.Observacion = collection.Get("Observacion").ToString();
                if (!string.IsNullOrEmpty(collection.Get("TipoId"))) PersonaCliente.TipoID = Convert.ToInt32(collection.Get("TipoId").ToString());
                if (MATContext.CurrentVendedor != null) PersonaCliente.VendedorID = MATContext.CurrentVendedor.VendedorId.ToString();
                #endregion

                #region create
                PersonaClienteMethod.CreatePersonaCliente(PersonaCliente);
                #endregion

                var returnUrl = collection.Get("returnUrl");
                if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index", "PersonaCliente");
            }
            catch (Exception e)
            {
                return RedirectToAction("Create", "PersonaCliente", new { msj = "No se pudo ingresar el pasajero al sistema. Disculpe las molestias." + e.Message });
            }



        }

        [Authorize]
        public ActionResult Edit(Guid Id)
        {
            try
            {
                PersonaClienteService SPersonaCliente = new PersonaClienteService();
                PersonaService SPersona = new PersonaService();
                ClienteService SCliente = new ClienteService();

                MAT.Entities.PersonaCliente PersonaCliente = new MAT.Entities.PersonaCliente();

                // Intentamos obtener los datos de Persona y Cliente
                MAT.Entities.Persona Persona = SPersona.Get(new PersonaKey(Id));
                if (Persona == null)
                {
                    throw new Exception($"No se encontró la persona con ID: {Id}");
                }

                MAT.Entities.Cliente Cliente = SCliente.Get(new ClienteKey(Id));
                if (Cliente == null)
                {
                    throw new Exception($"No se encontró el cliente con ID: {Id}");
                }

                try
                {
                    /* Debo actualizar la entidad PersonaCliente ya que esta no pertenece a la 
                    base de datos */
                    #region Actualizar PersonaCliente
                    PersonaCliente.PersonaId = Persona.PersonaId;
                    PersonaCliente.Apellido = Persona.Apellido;
                    PersonaCliente.Nombre = Persona.Nombre;
                    PersonaCliente.TipoDocumento = Persona.TipoDocumento;
                    PersonaCliente.NroDocumento = Persona.NroDocumento;
                    PersonaCliente.LocalidadId = Persona.LocalidadId;
                    PersonaCliente.Provincia = Persona.Provincia;
                    PersonaCliente.Domicilio = Persona.Domicilio;
                    PersonaCliente.Telefono = Persona.Telefono;
                    PersonaCliente.Celular = Persona.Celular;
                    PersonaCliente.Email = Persona.Email;
                    PersonaCliente.FechaNacimiento = Persona.FechaNacimiento;
                    PersonaCliente.Sexo = Persona.Sexo;
                    PersonaCliente.RazonSocial = Cliente.RazonSocial;
                    PersonaCliente.Cuit = Cliente.Cuit;
                    PersonaCliente.Moneda = Cliente.Moneda;
                    PersonaCliente.Empresa = Cliente.Empresa;
                    PersonaCliente.FormaPago = Cliente.FormaPago;
                    PersonaCliente.CondicionIva = Cliente.CondicionIva;
                    PersonaCliente.VendedorId = Cliente.VendedorId;
                    PersonaCliente.Fax = Cliente.Fax;
                    PersonaCliente.Web = Cliente.Web;
                    PersonaCliente.Idioma = Cliente.Idioma;
                    PersonaCliente.Promotor = Cliente.Promotor;
                    PersonaCliente.Observacion = Cliente.Observacion;
                    PersonaCliente.TipoId = Cliente.TipoId;
                    PersonaCliente.Nacionalidad = Persona.Nacionalidad;
                    PersonaCliente.PaisResidencia = Persona.PaisResidencia;
                    PersonaCliente.Ocupacion = Persona.Ocupacion;
                    #endregion

                    ViewBag.ListOcupacion = GetAllOcupacion();
                    ViewBag.ListProvincia = GetAllProvincia();
                    return View(PersonaCliente);
                }
                catch (Exception ex)
                {
                    // Log del error específico durante la actualización de PersonaCliente
                    // Aquí podrías usar tu sistema de logging preferido
                    throw new Exception("Error al actualizar los datos de PersonaCliente", ex);
                }
            }
            catch (Exception e)
            {
                // Log del error general
                // Aquí podrías usar tu sistema de logging preferido

                // Retornamos a la vista de error compartida
                return View("Error", e);
                
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            PersonaClienteService SPersonaCliente = new PersonaClienteService();
            PersonaService SPersona = new PersonaService();
            ClienteService SCliente = new ClienteService();

            MAT.Entities.PersonaCliente PersonaCliente = new MAT.Entities.PersonaCliente();
            MAT.Entities.Persona Persona = SPersona.Get(new PersonaKey(Id));
            MAT.Entities.Cliente Cliente = SCliente.Get(new ClienteKey(Id));

            try
            {
                if (!string.IsNullOrEmpty(collection.Get("Apellido"))) Persona.Apellido = collection.Get("Apellido").ToString();
                if (!string.IsNullOrEmpty(collection.Get("Nombre"))) Persona.Nombre = collection.Get("Nombre").ToString();
                if (!string.IsNullOrEmpty(collection.Get("TipoDocumento"))) Persona.TipoDocumento = Convert.ToInt32(collection.Get("TipoDocumento").ToString());
                if (!string.IsNullOrEmpty(collection.Get("NroDocumento"))) Persona.NroDocumento = collection.Get("NroDocumento").ToString();
                if (!string.IsNullOrEmpty(collection.Get("LocalidadId"))) Persona.LocalidadId = Convert.ToInt16(collection.Get("LocalidadId").ToString());
                if (!string.IsNullOrEmpty(collection.Get("Provincia"))) Persona.Provincia = Convert.ToInt32(collection.Get("Provincia"));
                if (!string.IsNullOrEmpty(collection.Get("Domicilio"))) Persona.Domicilio = collection.Get("Domicilio").ToString();
                if (!string.IsNullOrEmpty(collection.Get("Telefono"))) Persona.Telefono = collection.Get("Telefono").ToString();
                if (!string.IsNullOrEmpty(collection.Get("Celular"))) Persona.Celular = collection.Get("Celular").ToString();
                if (!string.IsNullOrEmpty(collection.Get("Email"))) Persona.Email = collection.Get("Email").ToString();
                if (!string.IsNullOrEmpty(collection.Get("Ocupacion"))) Persona.Ocupacion = collection.Get("Ocupacion").ToString();
                if (!string.IsNullOrEmpty(collection.Get("FechaNacimiento"))) Persona.FechaNacimiento = Convert.ToDateTime(collection.Get("FechaNacimiento"));
                if (!string.IsNullOrEmpty(collection.Get("Sexo"))) Persona.Sexo = Convert.ToInt32(collection.Get("Sexo").ToString());
                if (!string.IsNullOrEmpty(collection.Get("Nacionalidad"))) Persona.Nacionalidad = collection.Get("Nacionalidad").ToString();
                if (!string.IsNullOrEmpty(collection.Get("PaisResidencia"))) Persona.PaisResidencia = collection.Get("PaisResidencia").ToString();
                SPersona.Update(Persona);

                if (!string.IsNullOrEmpty(collection.Get("RazonSocial"))) Cliente.RazonSocial = collection.Get("RazonSocial").ToString();
                if (!string.IsNullOrEmpty(collection.Get("Cuit"))) Cliente.Cuit = collection.Get("Cuit").ToString();
                if (!string.IsNullOrEmpty(collection.Get("Moneda"))) Cliente.Moneda = collection.Get("Moneda").ToString();
                if (!string.IsNullOrEmpty(collection.Get("Empresa"))) Cliente.Empresa = collection.Get("Empresa").ToString();
                if (!string.IsNullOrEmpty(collection.Get("FormaPago"))) Cliente.FormaPago = Convert.ToInt32(collection.Get("FormaPago"));
                if (!string.IsNullOrEmpty(collection.Get("CondicionIva"))) Cliente.CondicionIva = Convert.ToInt32(collection.Get("CondicionIva"));
                if (!string.IsNullOrEmpty(collection.Get("Fax"))) Cliente.Fax = collection.Get("Fax").ToString();
                if (!string.IsNullOrEmpty(collection.Get("Web"))) Cliente.Web = collection.Get("Web").ToString();
                if (!string.IsNullOrEmpty(collection.Get("Idioma"))) Cliente.Idioma = collection.Get("Idioma").ToString();
                if (!string.IsNullOrEmpty(collection.Get("Promotor"))) Cliente.Promotor = collection.Get("Promotor").ToString();
                if (!string.IsNullOrEmpty(collection.Get("Observacion"))) Cliente.Observacion = collection.Get("Observacion").ToString();
                SCliente.Update(Cliente);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }

            return RedirectToAction("Details", "PersonaCliente", new { Id = Persona.PersonaId });
        }

        [Authorize]
        public ActionResult Details(Guid Id)
        {
            PersonaClienteService SPersonaCliente = new PersonaClienteService();
            PersonaService SPersona = new PersonaService();
            ClienteService SCliente = new ClienteService();

            MAT.Entities.PersonaCliente PersonaCliente = new MAT.Entities.PersonaCliente();
            MAT.Entities.Persona Persona = SPersona.Get(new PersonaKey(Id));
            MAT.Entities.Cliente Cliente = SCliente.Get(new ClienteKey(Id));

            #region Actualizar PersonaCliente
            PersonaCliente.PersonaId = Persona.PersonaId;
            PersonaCliente.Apellido = Persona.Apellido;
            PersonaCliente.Nombre = Persona.Nombre;
            PersonaCliente.TipoDocumento = Persona.TipoDocumento;
            PersonaCliente.NroDocumento = Persona.NroDocumento;
            PersonaCliente.Domicilio = Persona.Domicilio;
            PersonaCliente.LocalidadId = Persona.LocalidadId;
            PersonaCliente.Provincia = Persona.Provincia;
            PersonaCliente.Celular = Persona.Celular;
            PersonaCliente.Telefono = Persona.Telefono;
            PersonaCliente.Email = Persona.Email;
            PersonaCliente.FechaNacimiento = Persona.FechaNacimiento;
            PersonaCliente.Sexo = Persona.Sexo;
            PersonaCliente.RazonSocial = Cliente.RazonSocial;
            PersonaCliente.Cuit = Cliente.Cuit;
            PersonaCliente.Moneda = Cliente.Moneda;
            PersonaCliente.Empresa = Cliente.Empresa;
            PersonaCliente.FormaPago = Cliente.FormaPago;
            PersonaCliente.CondicionIva = Cliente.CondicionIva;
            PersonaCliente.VendedorId = Cliente.VendedorId;
            PersonaCliente.Fax = Cliente.Fax;
            PersonaCliente.Web = Cliente.Web;
            PersonaCliente.Idioma = Cliente.Idioma;
            PersonaCliente.Promotor = Cliente.Promotor;
            PersonaCliente.Observacion = Cliente.Observacion;
            PersonaCliente.TipoId = Cliente.TipoId;
            PersonaCliente.Nacionalidad = Persona.Nacionalidad;
            PersonaCliente.PaisResidencia = Persona.PaisResidencia;
            PersonaCliente.Ocupacion = Persona.Ocupacion;
            #endregion

            return View(PersonaCliente);
        }

        [Authorize]
        public ActionResult Delete(Guid Id)
        {
            PersonaService SPersona = new PersonaService();
            ClienteService SCliente = new ClienteService();

            SCliente.Delete(Id);
            SPersona.Delete(Id);

            return RedirectToAction("Index", "PersonaCliente");
        }

        public ActionResult HistorialdePagos(Guid ClienteID)
        {
            try
            {
                PersonaClienteModel oPersona = MAT.MVC.Models.PersonaClienteMethod.PersonaClienteGetByPersonaID(ClienteID);

                ViewBag.oPesona = oPersona;
                ViewBag.ClienteID = ClienteID;
            }
            catch (Exception e)
            {
                ViewBag.Error ="Error: " + e.Message;
            }
            return View();
        }

        public ActionResult partialHistorialdePagos(Guid ClienteID)
        {
            
            try
            {
                List<FacturaViaje> model = new List<FacturaViaje>();
                model = FacturaMetod.GetListFacturaByViajeByClienteID(ClienteID);
                return PartialView(model);
            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController.partialHistorialdePagos");
                return PartialView();
                
            }
            
        }


        public ActionResult partialHistorialdePagosByFactura(Guid FacturaID)
        {
            try
            {
                List<PagoModel> model = new List<PagoModel>();
                model = PagoMethod.GetPagosByFacturaID(FacturaID);
                ViewBag.FacturaID = FacturaID;
                return PartialView(model);
            }
            catch (Exception e)
            {
                ViewBag.Error = "Error: " + e.Message;
                return PartialView();

            }

        }



        public ActionResult CuentaCorriente(Guid clienteid)
        {
            ViewBag.ClienteID = clienteid;
            return View();
        }

        [Authorize]   
        public ActionResult Facturas(Guid clienteid)
        {
            ViewBag.ClienteID = clienteid;
            List<FacturaStandard> facturasModel = new List<FacturaStandard>();
            try
            {
                facturasModel = MAT.MVC.Models.FacturaMetod.ListFacturaByClienteID(clienteid);
                return View(facturasModel);
            }
            catch (Exception e) {
                ViewBag.Error = e.Message;
                return View(facturasModel);
            }

         
        }

        public ActionResult PartialFacturas(Guid clienteid)
        {
            ViewBag.ClienteID = clienteid;
            List<FacturaStandard> facturasModel = new List<FacturaStandard>();
            try
            {
                facturasModel = MAT.MVC.Models.FacturaMetod.ListFacturaByClienteID(clienteid);
                return PartialView(facturasModel);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return PartialView(facturasModel);
            }
        }

        public ActionResult PopupDetalleFactura(Guid facturaid)
        {
            ViewBag.FacturaID = facturaid;
            return PartialView();
        }

        public ActionResult DetalleFactura(Guid facturaid)
        {
            try
            {
                FacturaStandard factura = new FacturaStandard();
                List<FacturaDetalle> facturaDetalle = new List<FacturaDetalle>();
                ViewBag.Error = "";

                factura = FacturaMetod.FacturaStandardByID(facturaid);
                facturaDetalle = FacturaMetod.FacturaDetalleByID(facturaid);
                ViewBag.FacturaDetalle = facturaDetalle;
                ViewBag.ListMenores = GetPasajeroMenorByFacturaID(facturaid);
                ViewBag.ExtendFacturaDetalle = FacturaMetod.GetDetalleByFacturaID(facturaid);
                return PartialView(factura);
            }
            catch (Exception e)
            {

                ViewBag.Error = e.Message;
                return View("Error", e);
            }
            
        }

        /// <summary>
        /// Endpoint optimizado para el popin DetalleFactura: 1 solo SP / 1 roundtrip.
        /// Requiere el SP dbo.usp_MAT_Factura_GetDetallePopupByFacturaID (ver /database).
        /// </summary>
        public ActionResult DetalleFacturaFast(Guid facturaid)
        {
            try
            {
                ViewBag.Error = "";

                var data = FacturaMetod.GetDetallePopupByFacturaID(facturaid);
                ViewBag.FacturaDetalle = data.FacturaDetalle ?? new List<FacturaDetalle>();
                ViewBag.ListMenores = data.Menores ?? new List<PasajeroMenorModel>();
                ViewBag.ExtendFacturaDetalle = data.Items ?? new List<DBOFacturaDetalle>();

                // Reutilizamos la misma vista parcial para minimizar cambios UI.
                return PartialView("DetalleFactura", data.Factura ?? new FacturaStandard());
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error", e);
            }
        }

        public static List<PasajeroMenorModel> GetPasajeroMenorByFacturaID(Guid facturaid)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@FacturaID", SqlDbType.VarChar, 0, Convert.ToString(facturaid)),
                    };
            List<PasajeroMenorModel> model = new List<PasajeroMenorModel>();

            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_PersonaCliente_GetPasajeroMenorByFactura", dbParams))
            {
                while (_reader.Read())
                {
                    PasajeroMenorModel item = new PasajeroMenorModel();
                    item.PasajeroMenorID = Convert.ToInt32(_reader["id"]);
                    item.ApellidoMayor = _reader["ApellidoMayor"].ToString();
                    item.NombreMayor = _reader["NombreMayor"].ToString();
                    item.DocMayor = _reader["DocMayor"].ToString();
                    item.ApellidoMenor = _reader["ApellidoMenor"].ToString();
                    item.NomreMenor = _reader["NomreMenor"].ToString();
                    item.DocMenor = _reader["DocMenor"].ToString();
                    model.Add(item);
                }
            }

            return model;
        }

        public static int EliminarVinculoPasajeroMenor(Guid facturaid)
        { 
            Int16 iResult = 0;
            try
            {
                SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@FacturaID", SqlDbType.VarChar, 0, Convert.ToString(facturaid)),
                    };

                using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_PersonaCliente_DesvincularMenor", dbParams))
                {
                    if (_reader.Read())
                    {
                        iResult = Convert.ToInt16(_reader["Id"]);
                    }
                }

            }
            catch { 
            
            }
          
            return iResult;
        }

        public static int LiberarHabitaciones(Guid facturaid)
        {
            Int16 iResult = 0;
            try
            {
                SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@FacturaID", SqlDbType.VarChar, 0, Convert.ToString(facturaid)),
                    };

                using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_PersonaCliente_EliminarVenta_LiberarHabitaciones", dbParams))
                {
                    if (_reader.Read())
                    {
                        iResult = Convert.ToInt16(_reader["ID"]);
                    }
                }

            }
            catch
            {

            }

            return iResult;
        }

        public static string EliminarFactura(Guid facturaid)
        {
            string sResult = "";
            try
            {
                SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@FacturaID", SqlDbType.VarChar, 0, Convert.ToString(facturaid)),
                        DBHelper.MakeParam("@VendedorID", SqlDbType.VarChar, 0, MATContext.CurrentVendedor.VendedorId.ToString()),
                    };

                using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Factura_DeleteFactura", dbParams))
                {
                    if (_reader.Read())
                    {
                        sResult = _reader["Result"].ToString(); ;
                    }
                }

            }
            catch(Exception e)
            {
                sResult = ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController.EliminarFactura");
            }

            return sResult;
        }

        [Authorize]
        public ActionResult RegistrarPago(Guid facturaid, Guid clienteid)
        {
            ViewBag.FacturaID = facturaid;
            ViewBag.ClienteID = clienteid;
            try
            {
                ViewBag.Saldo = MVC.Models.ReservaMethod.GetSaldoFactura(facturaid);
            }
            catch (Exception ex)
            {
                MATLogger.Log(string.Format("RegistrarPago GetSaldoFactura: {0} {1}", ex.Message, ex.StackTrace), 1);
                ViewBag.Saldo = 0m;
                ViewBag.SaldoError = "No se pudo obtener el saldo. Verifique la factura.";
            }
            return PartialView();
        }

        [Authorize]
        public ActionResult RegistrarPagoTotal(Guid facturaid, Guid clienteid)
        {
            ViewBag.FacturaID = facturaid;
            ViewBag.ClienteID = clienteid;
            try
            {
                ViewBag.Saldo = MVC.Models.ReservaMethod.GetSaldoFactura(facturaid);
            }
            catch (Exception ex)
            {
                MATLogger.Log(string.Format("RegistrarPagoTotal GetSaldoFactura: {0} {1}", ex.Message, ex.StackTrace), 1);
                ViewBag.Saldo = 0m;
                ViewBag.SaldoError = "No se pudo obtener el saldo. Verifique la factura.";
            }
            return PartialView();
        }

        [HttpPost]
        [Authorize]
        public JsonResult RegistrarPago(string monto, string tipopago, string recibo, string TransaccionID, string FacturaID, string ClienteID,
                                  string MontoRecibido = "", string MontoRecibidoMonedaTipo = "1", string MontoEquivalente = "", string MontoEquivalenteMonedaTipo = "", string MontoEquivalenteCotizacion = "")
        {
            try
            {
                decimal? dMontoRecibido = null;
                decimal? dMontoEquivalenteCotizacion = null;
                decimal? dMontoEquivalente = null;
                int? iMontoEquivalenteMonedaTipo = null;

                if (MontoRecibido != "")
                {
                    dMontoRecibido = Convert.ToDecimal(MontoRecibido);
                }
                if (MontoEquivalenteCotizacion != "")
                {
                    dMontoEquivalenteCotizacion = Convert.ToDecimal(MontoEquivalenteCotizacion);
                }
                if (MontoEquivalente != "")
                {
                    dMontoEquivalente = Convert.ToDecimal(MontoEquivalente);
                }
                if (MontoEquivalenteMonedaTipo != "")
                {
                    iMontoEquivalenteMonedaTipo = Convert.ToInt32(MontoEquivalenteMonedaTipo);
                }

                DataSet ds = MVC.Models.PagoMethod.NuevoPago(Convert.ToDecimal(monto), ClienteID, recibo, TransaccionID, Convert.ToInt32(tipopago), FacturaID, "",dMontoRecibido,  Convert.ToInt32(MontoRecibidoMonedaTipo), dMontoEquivalente, iMontoEquivalenteMonedaTipo, dMontoEquivalenteCotizacion);


                bool bReturn = false;
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    if (item["Result"].ToString() == "Done.")
                    {
                        bReturn = true;
                        break;
                    }
                }

                if (bReturn)
                {
                    var pagos = MVC.Models.PagoMethod.GetPagosByFacturaID(Guid.Parse(FacturaID));
                    var ultimoPago = pagos.OrderByDescending(p => p.FechaPago).FirstOrDefault();
                    return Json(new { Success = true, PagoID = ultimoPago != null ? ultimoPago.PagoID : "", FacturaID = FacturaID });
                }
                return Json(new { Success = false, PagoID = "", FacturaID = FacturaID });
            }
            catch (Exception ex)
            {
                MATLogger.Log(String.Format("{0} {1}", ex.Message, ex.StackTrace), 1);
                return Json(new { Success = false, PagoID = "", FacturaID = FacturaID });
            }
        }

       
        [HttpPost]
        [Authorize]
        public JsonResult RegistrarPagoTotal(string monto, string tipopago, string factura, string recibo, string TransaccionID, string FacturaID, string ClienteID,
                                       string MontoRecibido = "", string MontoRecibidoMonedaTipo = "1", string MontoEquivalente = "", string MontoEquivalenteMonedaTipo = "", string MontoEquivalenteCotizacion = "")
        {
            try
            {
                decimal? dMontoRecibido = null;
                decimal? dMontoEquivalenteCotizacion = null;
                decimal? dMontoEquivalente = null;
                int? iMontoEquivalenteMonedaTipo = null;

                if (MontoRecibido != "")
                {
                    dMontoRecibido = Convert.ToDecimal(MontoRecibido);
                }
                if (MontoEquivalenteCotizacion != "")
                {
                    dMontoEquivalenteCotizacion = Convert.ToDecimal(MontoEquivalenteCotizacion);
                }
                if (MontoEquivalente != "")
                {
                    dMontoEquivalente = Convert.ToDecimal(MontoEquivalente);
                }
                if (MontoEquivalenteMonedaTipo != "")
                {
                    iMontoEquivalenteMonedaTipo = Convert.ToInt32(MontoEquivalenteMonedaTipo);
                }

                DataSet ds = MVC.Models.PagoMethod.NuevoPago(Convert.ToDecimal(monto), ClienteID, recibo, TransaccionID, Convert.ToInt32(tipopago), FacturaID, factura, dMontoRecibido, Convert.ToInt32(MontoRecibidoMonedaTipo), dMontoEquivalente, iMontoEquivalenteMonedaTipo, dMontoEquivalenteCotizacion);


                bool bReturn = false;
                foreach (DataRow item in ds.Tables[0].Rows)
	            {
                    if (item["Result"].ToString() == "Done.")
                    {
                        bReturn = true;
                        break;
                    }
	            }

                if (bReturn)
                {
                    var pagos = MVC.Models.PagoMethod.GetPagosByFacturaID(Guid.Parse(FacturaID));
                    var ultimoPago = pagos.OrderByDescending(p => p.FechaPago).FirstOrDefault();
                    return Json(new { Success = true, PagoID = ultimoPago != null ? ultimoPago.PagoID : "", FacturaID = FacturaID });
                }
                return Json(new { Success = false, PagoID = "", FacturaID = FacturaID });
            }
            catch (Exception ex)
            {
                MATLogger.Log(String.Format("{0} {1}", ex.Message, ex.StackTrace), 1);
                return Json(new { Success = false, PagoID = "", FacturaID = FacturaID });
            }
        }

        [Authorize]
        public ActionResult RegistrarNotaCredito(Guid facturaid, Guid clienteid, string sMonto, string sTotalPagos)
        {
            try
            {
                var factura = FacturaMetod.FacturaStandardByID(facturaid);
                if (factura == null || factura.Estado != (int)eEstadoFactura.Pagado || factura.Saldo > 0)
                {
                    ViewBag.Error = "La nota de crédito solo está disponible para facturas pagadas en su totalidad.";
                    return PartialView("RegistrarNotaCredito", new NotaCreditoModel());
                }

                var (dFecha, viajeNombre) = MVC.Models.ViajeMethod.GetDatosViajeByFacturaID(facturaid);
                NotaCreditoModel _model = new NotaCreditoModel();
                ViewBag.FacturaID = facturaid;
                _model = NotaCreditoMethod.CalcularNota(Convert.ToDecimal(sMonto), dFecha);
                _model.ClienteID = clienteid;
                _model.NroNota = NotaCreditoMethod.GetNextNroNota();
                _model.ViajeFecha = dFecha != default(DateTime) ? (DateTime?)dFecha : null;
                _model.ViajeNombre = viajeNombre;
                ViewBag.MontoFactura = Convert.ToDecimal(sMonto);
                ViewBag.TotalPagos = MAT.MVC.Models.PagoMethod.Pago_TotalPagosByFacturaID(facturaid);
                ViewBag.PorcentajeRetencionSugerido = _model.PorcentajeRetencion;
                ViewBag.PorcentajeCreditoSugerido = 1M - _model.PorcentajeRetencion;
                return PartialView(_model);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return PartialView("RegistrarNotaCredito", new NotaCreditoModel());
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult RegistrarNotaCredito(NotaCreditoModel NotaCredito, Guid FacturaID)
        {
            string[] sResult = new string[2];
            try
            {
                EliminarVinculoPasajeroMenor(FacturaID);
                LiberarHabitaciones(FacturaID);
                MAT.MVC.Models.NotaCreditoMethod.InsertNewNota(NotaCredito, FacturaID);
                
                sResult[0] = "Done.";
                sResult[1] = "";
            }
            catch (Exception e)
            {
                sResult[0] = "Error.";
                sResult[1] = e.Message;
            }
            return Json(sResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public bool validarCTA(string ClienteId, string Estado)
        {
            //string msj = "";
            bool estado = false;
            Guid Id = new Guid(ClienteId);

            switch (Estado)
            {

                case "Activar":
                    CuentaService SCuenta = new CuentaService();
                    MAT.Entities.Cuenta ECuenta = SCuenta.GetByClienteId(Id).FirstOrDefault();
                    ECuenta.Estado = true;
                    SCuenta.Update(ECuenta);
                    //msj = "Se activo la Cuenta Corriente para el cliente seleccionado";
                    estado = true;
                    break;
                case "Desactivar":

                    CuentaService SCuentaC = new CuentaService();
                    MAT.Entities.Cuenta ECuentaC = SCuentaC.GetByClienteId(Id).FirstOrDefault();
                    ECuentaC.Estado = false;
                    SCuentaC.Update(ECuentaC);
                    //msj = "Se descactivo la Cuenta Corriente para el cliente selccionado";
                    estado = false;
                    break;
                case "Verificar":

                    CuentaService SCuentaCTA = new CuentaService();
                    MAT.Entities.Cuenta ECuentaV = SCuentaCTA.GetByClienteId(Id).FirstOrDefault();

                    if (ECuentaV == null)
                    {
                        ECuentaV = new MAT.Entities.Cuenta();
                        ECuentaV.ClienteId = Id;
                        ECuentaV.Estado = false;
                        SCuentaCTA.Insert(ECuentaV);
                        estado = false;
                    }
                    else
                    {
                        estado = ECuentaV.Estado;
                    }

                    break;
            }
            return estado;

        }

        public JsonResult ExistDni(string dni = "")
        {
            string[] sResult = new string[2];
            try
            {
                int ExistDNI = MAT.MVC.Models.PersonaClienteMethod.IfExistDNI(dni);
                if (ExistDNI == 0)
                    sResult[0] = "False";
                else
                    sResult[0] = "True";
                sResult[1] = "Done.";
            }
            catch (Exception e)
            {
                sResult[0] = "";
                sResult[1] = "Error: " + ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController.ExistDni");
            }

            return Json(new
            {
                Data = sResult[0],
                Result = sResult[1]
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult Voucher(Guid facturaid, string sTipoVoucher = "Individual", bool bInfoAdicional = false)
        {
            List<Models.VoucherStandard> ListVoucher = new List<Models.VoucherStandard>();
            try
            {
                ListVoucher = Models.VoucherMethod.GetVoucherByFacturaID(facturaid.ToString());
                string sView = "";
                ViewBag.InfoAdicional = bInfoAdicional;

                switch (sTipoVoucher)
                {
                    case "Individual" :
                        sView = "Voucher";
                        break;
                    case "Grupal":
                        sView = "VoucherGrupal";
                        break;
                     
                }

                return PartialView(sView, ListVoucher);
                
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return PartialView(ListVoucher);
            }
            
        }

        [Authorize]
        public ActionResult ReciboPago(Guid pagoId, Guid facturaId)
        {
            try
            {
                var pagos = Models.PagoMethod.GetPagosByFacturaID(facturaId);
                var pago = pagos.FirstOrDefault(p => p.PagoID == pagoId.ToString());
                if (pago == null)
                {
                    ViewBag.Error = "No se encontró el pago. Es posible que haya sido eliminado.";
                    return View("ReciboPago", new Models.ReciboPagoViewModel());
                }

                var detalle = Models.PagoMethod.GetPagoDetalleByPagoID(pagoId);
                var factura = FacturaMetod.FacturaStandardByID(facturaId);

                var model = new Models.ReciboPagoViewModel
                {
                    PagoID = pago.PagoID,
                    FechaPago = pago.FechaPago,
                    Monto = pago.Monto,
                    NroRecibo = pago.NroRecibo,
                    TipoPagoDescripcion = pago.TipoPagoDescripcion,
                    TransaccionID = pago.TransaccionID,
                    Vendedor = pago.Vendedor,
                    Moneda = pago.Moneda,
                    ClienteNombre = factura != null ? (factura.ClienteNombre + " " + factura.ClienteApellido).Trim() : "",
                    PaqueteDescripcion = factura != null ? (factura.PaqueteDescripcion ?? "") : "",
                    ViajeDescripcion = factura != null ? (factura.ViajeDescripcion ?? "") : "",
                    NroFactura = factura != null ? (factura.NroFactura ?? "") : "",
                    Detalle = detalle
                };

                return View("ReciboPago", model);
            }
            catch (Exception e)
            {
                MAT.MVC.Infrastructure.ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController.ReciboPago");
                ViewBag.Error = "Ocurrió un error al cargar el recibo. Intente nuevamente.";
                return View("ReciboPago", new Models.ReciboPagoViewModel());
            }
        }

        public JsonResult Voucher_GetNrPrintByFacturaID(Guid facturaId)
        {
            string[] sResult = new string[2];
            
            try
            {
                sResult[0] = "Done.";
                sResult[1] = Models.VoucherMethod.GetNrPrintByFacturaID(facturaId.ToString()).ToString();
            }
            catch (Exception e){
                sResult[0] = ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController.Voucher_GetNrPrintByFacturaID");
                sResult[1] = "";
            }
            return Json(sResult , JsonRequestBehavior.AllowGet);  
        }

        public JsonResult CancelarPago(Guid PagoID)
        {
            string[] sResult = new string[2];

            try
            {
                sResult[0] = "Done.";
                sResult[1] = "";
                Models.PagoMethod.DeletePago(PagoID);    
            }
            catch (Exception e)
            {
                sResult[0] = "Error.";
                sResult[1] = e.Message;
            }
            return Json(sResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EliminarVenta(string facturaid, string clienteid)
        {
            EliminarVentaModel ventamodel = new EliminarVentaModel();
            ventamodel.ClienteId = clienteid;
            ventamodel.FacturaId = facturaid;
            return PartialView(ventamodel);
        }

        [HttpPost]
        public bool EliminarVenta(Guid facturaid, Guid clienteid, string code)
        {
            string Code1 = System.Configuration.ConfigurationManager.AppSettings["PersonaClienteCode"];
            string Code2 = System.Configuration.ConfigurationManager.AppSettings["PersonaClienteCode2"];

            if (!(code.Equals(Code1) || code.Equals(Code2))) return false;
                    
            bool result = false;
            try
            {
                EliminarVinculoPasajeroMenor(facturaid);
                LiberarHabitaciones(facturaid);
               
                string sResultDelete = EliminarFactura(facturaid);
                if (sResultDelete == "Done.")
                {
                    result = true;
                }
                else
                {
                    result = false;
                    ViewBag.MsgError = sResultDelete;
                }
                
            }
            catch (Exception e)
            {
                ViewBag.MsgError = e.Message;
                result = false;
            }
            return result;
        }


        [HttpPost]
        [Authorize]
        public JsonResult EliminarPasajeroDeFactura(Guid pasajeID, Guid facturaID, string code)
        {
            string[] sResult = new string[2];
            try
            {
                // Validar código de seguridad contra valores configurados en Web.config
                var code1 = ConfigurationManager.AppSettings["PersonaClienteCode"];
                var code2 = ConfigurationManager.AppSettings["PersonaClienteCode2"];
                
                if (string.IsNullOrWhiteSpace(code) || !(code.Equals(code1) || code.Equals(code2)))
                {
                    sResult[0] = "Error.";
                    sResult[1] = "Código de seguridad incorrecto. Verifique el código ingresado.";
                    return Json(sResult, JsonRequestBehavior.AllowGet);
                }

                // Desvincular menores de la factura antes de eliminar el pasajero
                EliminarVinculoPasajeroMenor(facturaID);
                
                SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@PasajeID", SqlDbType.UniqueIdentifier, 0, pasajeID),
                    DBHelper.MakeParam("@FacturaID", SqlDbType.UniqueIdentifier, 0, facturaID),
                    DBHelper.MakeParam("@Result", SqlDbType.VarChar, 100, "")
                };
                
                // Configurar parámetro de salida
                dbParams[2].Direction = ParameterDirection.Output;
                
                DBHelper.ExecuteNonQuery("dbo.usp_MAT_Factura_EliminarPasajero", dbParams);
                
                string result = dbParams[2].Value != null ? dbParams[2].Value.ToString() : "Error: No se recibió respuesta del procedimiento.";
                
                if (result == "Done.")
                {
                    sResult[0] = "Done.";
                    sResult[1] = "Pasajero eliminado correctamente. La factura ha sido actualizada.";
                }
                else
                {
                    sResult[0] = "Error.";
                    sResult[1] = result;
                }
            }
            catch (Exception e)
            {
                sResult[0] = "Error.";
                sResult[1] = "Error al eliminar el pasajero: " + e.Message;
            }
            
            return Json(sResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult CambiarPasajeroDePasaje(Guid pasajeID, Guid nuevoPasajeroID)
        {
            string[] sResult = new string[2];
            try
            {
                SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@PasajeID", SqlDbType.UniqueIdentifier, 0, pasajeID),
                    DBHelper.MakeParam("@NuevoPasajeroID", SqlDbType.UniqueIdentifier, 0, nuevoPasajeroID),
                    DBHelper.MakeParam("@Result", SqlDbType.VarChar, 100, "")
                };
                
                // Configurar parámetro de salida
                dbParams[2].Direction = ParameterDirection.Output;
                
                DBHelper.ExecuteNonQuery("dbo.usp_MAT_Pasaje_CambiarPasajero", dbParams);
                
                string result = dbParams[2].Value != null ? dbParams[2].Value.ToString() : "Error: No se recibió respuesta del procedimiento.";
                
                if (result == "Done.")
                {
                    sResult[0] = "Done.";
                    sResult[1] = "Pasajero cambiado correctamente. La factura ha sido actualizada.";
                }
                else
                {
                    sResult[0] = "Error.";
                    sResult[1] = result;
                }
            }
            catch (Exception e)
            {
                sResult[0] = "Error.";
                sResult[1] = "Error al cambiar el pasajero: " + e.Message;
            }
            
            return Json(sResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ElegirNuevaButaca(Guid anteriorid, Guid pasajeid)
        {
            ViewData["anteriorid"] = anteriorid;
            ViewData["pasajeid"] = pasajeid;
            List<PasajeModel> pasajes = new List<PasajeModel>();
            PasajeService pServ = new PasajeService();
            Guid viajeid = pServ.GetByPasajeId(pasajeid).ViajeId.Value;
            
            List<ReservaStandard> Model = new List<ReservaStandard>();

            try
            {
                Model = ReservaMethod.GetListOfPasajesByViajeID(viajeid.ToString());
                ViewBag.PreReservas = ReservaMethod.GetPreReservaVencidas(viajeid.ToString());
            }
            catch (Exception e)
            {
                ViewBag.MsgError = e.Message;
            }

            return PartialView(Model);
        }

        public ActionResult SeleccionarImportes(Guid paqueteid, string piso, Guid pasajeid, Guid anteriorid, Guid nuevopasaje)
        {
            if (!string.IsNullOrEmpty(piso)) ViewData["piso"] = piso;
            ViewData["pasajeid"] = pasajeid;
            ViewData["anteriorid"] = anteriorid;
            ViewData["nuevopasaje"] = nuevopasaje;
            SeleccionarPasajeroModel seleccionarpasajeromodel = new SeleccionarPasajeroModel(paqueteid);
            return PartialView(seleccionarpasajeromodel);
        }
        public bool ConfirmarCambioButaca(Guid pasajeid, string adicional, Guid nuevopasaje)
        {
            bool result = false;
            try
            {
                MVC.Models.ReservaMethod.CambioButacas(adicional, pasajeid, nuevopasaje);
                
                result = true;
            }
            catch 
            {
                result = false;
            }
            return result;
        }

        public ActionResult PrincipalHabitaciones(string pasajeid, string viajeid, string facturaid)
        {
            List<HotelDropDown> ListHotel = new List<HotelDropDown>();
            ListHotel = HotelMethod.GetHotelByViaje(viajeid);

            ViewData["pasajeid"] = pasajeid;
            ViewData["viajeid"] = viajeid;
            ViewData["facturaid"] = facturaid;
            ViewBag.ListHotel = ListHotel;
            return PartialView();
        }

        public ActionResult GridHabitaciones(string HotelID, string viajeid = "", string PasajeroID = "", string Fecha = "")
        {
            ViewData["viajeid"] = viajeid;
            ViewBag.PasajeroId = PasajeroID;
            List<HabitacionDisponibilidad> model = new List<HabitacionDisponibilidad>();
            try
            {
                model = HabitacionMethod.GetHabitacionDisponibilidad(viajeid, HotelID, Fecha);
                if (PasajeroID != "")
                {
                    string[] sHabReserva = MAT.MVC.Models.HabitacionMethod.HotelHabitacionReserva(PasajeroID, viajeid, Fecha);
                    ViewBag.HabitacionSelected = sHabReserva[0];

                }
                return PartialView(model);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return PartialView(model);
            }
        }

        /// <summary>
        /// Confirma el cambio de habitación para un pasaje en un viaje. El JS envía pasajeid, viajeid, nuevahabitacion.
        /// </summary>
        [HttpPost]
        public ContentResult ConfirmarCambioHabitacion(string pasajeid, string viajeid, string nuevahabitacion)
        {
            bool result = false;
            try
            {
                if (string.IsNullOrEmpty(pasajeid) || string.IsNullOrEmpty(viajeid) || string.IsNullOrEmpty(nuevahabitacion))
                    return Content("False");

                Guid _pasajeid = new Guid(pasajeid);
                Guid _viajeid = new Guid(viajeid);
                Guid _nuevahabitacion = new Guid(nuevahabitacion);

                var reservaService = new ReservaHabitacionService();
                var reserva = reservaService.GetByPasajeId(_pasajeid)
                    .Where(re => re.ViajeId.HasValue && re.ViajeId.Value == _viajeid)
                    .FirstOrDefault();
                if (reserva != null)
                {
                    reserva.HabitacionId = _nuevahabitacion;
                    reservaService.Update(reserva);
                    result = true;
                }
            }
            catch
            {
                result = false;
            }
            return Content(result ? "True" : "False");
        }

        public static List<string> GetAllOcupacion()
        {
            List<string> List = new List<string>();
            
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_GetAllOcupacion", null))
            {
                while (_reader.Read())
                {
                    string item;
                    item = _reader["Ocupacion"].ToString();
                    List.Add(item);
                }
            }

            return List;
        }

        public static List<SelectListItem> GetAllProvincia()
        {
            var list = new List<SelectListItem>();
            using (SqlDataReader reader = DBHelper.ExecuteDataReader("usp_GetAllProvincia", null))
            {
                while (reader.Read())
                {
                    list.Add(new SelectListItem
                    {
                        Value = reader["ID"].ToString(),
                        Text  = reader["Nombre"].ToString()
                    });
                }
            }
            return list;
        }

       
        public JsonResult EliminarReservaHotel(string sPasajeroID = "", string sHabitacionID = "", string sViajeID = "")
        {
            string sResult = "";

            try
            {
                if (sPasajeroID != "" && sHabitacionID != "" && sViajeID != "")
                {
                    SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@PasajeroID", SqlDbType.VarChar, 0, sPasajeroID),
                        DBHelper.MakeParam("@HabitacionID", SqlDbType.VarChar, 0, sHabitacionID),
                        DBHelper.MakeParam("@ViajeID", SqlDbType.VarChar, 0, sViajeID),
                        DBHelper.MakeParam("@UserID", SqlDbType.UniqueIdentifier, 0, MATContext.CurrentVendedor.VendedorId),
                    };

                    using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_PersonaCliente_DeleteReservaHotel", dbParams))
                    {
                        if (_reader.Read())
                        {
                            if (_reader["Result"].ToString() == "Done.")
                            {
                                sResult = "Done.";
                            }
                        }
                    }

                   
                }
            }
            catch (Exception e)
            {
                sResult = "Error: " + ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController.EliminarVenta");
            }


            return Json(new
            {
                Estado = sResult
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddDescuentoRecargo(string sFacturaID, string sMonto, string sDetalle, string sDescuentoIncremento)
        {
            string sResult = "";

            try
            {
                if (sFacturaID != "" && sMonto != "" && sDetalle != "" && sDescuentoIncremento != "")
                {
                    bool bIsDescuento = false;
                    if (sDescuentoIncremento == "Descuento")
                    {
                        bIsDescuento = true;
                    }
                    SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@FacturaID", SqlDbType.VarChar, 0, sFacturaID),
                        DBHelper.MakeParam("@Detalle", SqlDbType.VarChar, 0, sDetalle),
                        DBHelper.MakeParam("@Monto", SqlDbType.Money, 0, Convert.ToDouble(sMonto)),
                        DBHelper.MakeParam("@IsDescuento", SqlDbType.Bit, 0, bIsDescuento),
                    };

                    DBHelper.ExecuteNonQuery("usp_MAT_DetalleFactura_AgregarDescuentoRecargo", dbParams);

                    sResult = "Done.";


                    }
            }
            catch (Exception e)
            {
                sResult = "Error: " + ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController.EliminarReservaHotel");
            }


            return Json(new
            {
                Estado = sResult
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult NotaCreditoList()
        {
            try
            {
                List<MAT.MVC.Models.PersonaCliente_CreditoClienteModel> CreditoClienteList = new List<PersonaCliente_CreditoClienteModel>();
                CreditoClienteList = MAT.MVC.Models.PersonaClienteMethod.PersonaClienteCreditoClienteGetAll();

                var jsonCreditoClienteList = JsonConvert.SerializeObject(CreditoClienteList);
                ViewBag.sbDataSetJson = jsonCreditoClienteList.ToString();

            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            
            return View();
        }

        [Authorize]
        public ActionResult CambiarPrecioList(string DetalleFacturaId, string FacturaId)
        {
            List<MAT.MVC.Models.PaquetePrecio> lPaquetePrecio = new List<MAT.MVC.Models.PaquetePrecio>();
            try
            {
                lPaquetePrecio = MAT.MVC.Models.PaqueteVinculos.GetPaquetePrecioByFacturaId(FacturaId);
                ViewBag.DetalleFacturaId = DetalleFacturaId;
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }

            return PartialView(lPaquetePrecio);
        }

        public JsonResult DetalleFacturaSetPrecio(string DetalleFacturaId, string PrecioId)
        {
            string[] sResult = new string[2];

            try
            {
                sResult[0] = "Done.";
                sResult[1] = "";
                Models.PaqueteVinculos.DetalleFacturaSetPrecio(Convert.ToInt32(DetalleFacturaId),PrecioId);
            }
            catch (Exception e)
            {
                sResult[0] = "Error.";
                sResult[1] = e.Message;
            }
            return Json(sResult, JsonRequestBehavior.AllowGet);
        } 
    }
}
