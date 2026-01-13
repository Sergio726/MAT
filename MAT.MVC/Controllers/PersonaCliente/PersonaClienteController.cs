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
            return View();
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


            #region comentada
            try
            {

                #region Actualizar Persona
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
                #endregion

                #region Actualizar Cliente
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

                #endregion

                #region Actualizar PersonaCliente
                PersonaCliente.Apellido = Persona.Apellido;
                PersonaCliente.Nombre = Persona.Nombre;
                PersonaCliente.TipoDocumento = Persona.TipoDocumento;
                PersonaCliente.NroDocumento = Persona.NroDocumento;
                PersonaCliente.LocalidadId = Persona.LocalidadId;
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


                #endregion
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            #endregion

            return RedirectToAction("Details/" + (Persona.PersonaId) + "/", "PersonaCliente");
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
                ViewBag.Error = "Error: " + e.Message + " " + e.StackTrace;
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

        public ActionResult PartialCuentaCorriente(Guid clienteid)
        {
            
            return PartialView();
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

        public static void LiberarHabitacionesByPasajeID(Guid PasajeID)
        {
           
            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@PasajeID", SqlDbType.VarChar, 0, Convert.ToString(PasajeID)),
                };

            DBHelper.ExecuteNonQuery("dbo.usp_MAT_PersonaCliente_EliminarVenta_LiberarHabitacionesByPasajeID", dbParams);
          
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
                sResult = e.Message + " StackTrace: " + e.StackTrace.ToString();
            }

            return sResult;
        }

        [Authorize]
        public ActionResult RegistrarPago(Guid facturaid, Guid clienteid)
        {
            ViewBag.FacturaID = facturaid;
            ViewBag.ClienteID = clienteid;
            return PartialView();
        }

        [Authorize]
        public ActionResult RegistrarPagoTotal(Guid facturaid, Guid clienteid)
        {
            ViewBag.FacturaID = facturaid;
            ViewBag.ClienteID = clienteid;
            ViewBag.Saldo = MVC.Models.ReservaMethod.GetSaldoFactura(facturaid);
            return PartialView();
        }

        [HttpPost]
        [Authorize]
        public bool RegistrarPago(string monto, string tipopago, string recibo, string TransaccionID, string FacturaID, string ClienteID,
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

                return bReturn;
            }
            catch (Exception ex)
            {
                MATLogger.Log(String.Format("{0} {1}", ex.Message, ex.StackTrace), 1);
                return false;
            }
        }

       
        [HttpPost]
        [Authorize]
        public bool RegistrarPagoTotal(string monto, string tipopago, string factura, string recibo, string TransaccionID, string FacturaID, string ClienteID,
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

                return bReturn;
            }
            catch (Exception ex)
            {
                MATLogger.Log(String.Format("{0} {1}", ex.Message, ex.StackTrace), 1);
                return false;
            }
        }

        [Authorize]
        public ActionResult RegistrarNotaCredito(Guid facturaid, Guid clienteid, string sMonto, string sTotalPagos)
        {
            try
            {
                DateTime dFecha = MVC.Models.ViajeMethod.GetFechaViajeByFacturaID(facturaid);
                NotaCreditoModel _model = new NotaCreditoModel();
                ViewBag.FacturaID = facturaid;
                _model = NotaCreditoMethod.CalcularNota(Convert.ToDecimal(sMonto), dFecha);
                _model.ClienteID = clienteid;
                ViewBag.MontoFactura = Convert.ToDecimal(sMonto);
                ViewBag.TotalPagos = MAT.MVC.Models.PagoMethod.Pago_TotalPagosByFacturaID(facturaid);
                return PartialView(_model);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return PartialView();
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
                //PersonaService personaService = new PersonaService();
                //Entities.Persona _persona = personaService.GetAll().Where(per => per.NroDocumento.Equals(dni) || per.NroDocumento.Equals(string.Format("{0:99.999.999}", dni)) || per.NroDocumento.Equals(dni.Replace(".", ""))).FirstOrDefault();

                if (ExistDNI == 0)
                {
                    sResult[0] = "False";
                }
                else
                {
                    sResult[0] = "True";
                }

                //if (_persona == null)
                //{
                //    sResult[0] = "False";
                //}
                //else
                //{
                //    sResult[0] = "True";
                //}
               
                sResult[1] = "Done.";
            }
            catch (Exception e)
            {
                sResult[0] = "";
                sResult[1] = "Error: " + e.Message + "StackTrace: " + e.StackTrace;
            }

            return Json(new
            {
                Data = sResult[0],
                Result = sResult[1]
            }, JsonRequestBehavior.AllowGet);
        }

        //public string ExistDni(string dni)
        //{
        //    PersonaService personaService = new PersonaService();
        //    Entities.Persona _persona = personaService.GetAll().Where(per => per.NroDocumento.Equals(dni) || per.NroDocumento.Equals(string.Format("{0:99.999.999}", dni)) || per.NroDocumento.Equals(dni.Replace(".", ""))).FirstOrDefault();
        //    return _persona == null ? "False" : "True";
        //}

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

        public JsonResult Voucher_GetNrPrintByFacturaID(Guid facturaId)
        {
            string[] sResult = new string[2];
            
            try
            {
                sResult[0] = "Done.";
                sResult[1] = Models.VoucherMethod.GetNrPrintByFacturaID(facturaId.ToString()).ToString();
            }
            catch (Exception e){
                sResult[0] = e.Message + e.StackTrace;
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

        // [InitializeSimpleMembership]
        //public bool CancelarPago(Guid movimientoid)
        //{
        //    Guid VendedorID = MATContext.CurrentVendedor.VendedorId;
        //    if (MAT.MVC.Models.FacturaMetod.DeletePago(movimientoid, VendedorID))
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //public bool CancelarNota(Guid movimientoid)
        //{
        //    HistorialService historialService = new HistorialService();
        //    MovimientoCuentaService movimientoService = new MovimientoCuentaService();
        //    CuentaCorrienteService ccService = new CuentaCorrienteService();
        //    Entities.CuentaCorriente cc = new Entities.CuentaCorriente();
        //    Entities.MovimientoCuenta movimiento = movimientoService.GetByMovimientoId(movimientoid);
        //    NotaService notaService = new NotaService();
        //    if (movimiento.NotaId.HasValue)
        //    {
        //        Entities.Nota nota = notaService.GetByNotaId(movimiento.NotaId.Value);
        //        cc = ccService.GetByCuentaCorrienteId(movimiento.CuentaCorrienteId.Value);

        //        Entities.Historial nuevahistoria = new Historial();
        //        nuevahistoria.Tabla = (int)eTabla.Nota;
        //        nuevahistoria.Operacion = (int)eOperacion.Baja;
        //        nuevahistoria.Monto = nota.MontoNota.Value;
        //        nuevahistoria.FechaHoraRegistro = DateTime.Now;
        //        nuevahistoria.Cliente = cc.ClienteId;
        //        nuevahistoria.HistorialId = Guid.NewGuid();
        //        if (MATContext.CurrentVendedor != null) nuevahistoria.Vendedor = MATContext.CurrentVendedor.VendedorId;
        //        historialService.Insert(nuevahistoria);

        //        movimientoService.Delete(movimientoid);
        //        notaService.Delete(movimiento.NotaId.Value);
        //        ccService.Delete(cc);
        //        return true;
        //    }
        //    return false;
        //}

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
        public JsonResult EliminarPasajeroDeFactura(Guid pasajeID, Guid facturaID)
        {
            string[] sResult = new string[2];
            try
            {
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
        //public void Actualizar(Guid HotelId)
        //{
        //    MAT.Services.HabitacionService SHabitacion = new Services.HabitacionService();
        //    IList<MAT.Entities.Habitacion> EHabitacion = SHabitacion.GetByHotelId(HotelId);

        //    //Actualizo estado de habitacion
        //    foreach (var item in EHabitacion)
        //    {
        //        MAT.Services.VConsultaReservaHabitacionService SConsulta = new VConsultaReservaHabitacionService();
        //        IList<MAT.Entities.VConsultaReservaHabitacion> EConsulta = SConsulta.GetAll().Where(h => h.HabitacionId == item.HabitacionId && h.Expiro == false).ToList();

        //        //Recorro las habitaciones del hotel seleccionado
        //        foreach (var item1 in EConsulta)
        //        {
        //            DateTime FechaHoy = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        //            DateTime FechaHasta = Convert.ToDateTime(item1.Hasta.Value.ToShortDateString());

        //            if (FechaHoy > FechaHasta)
        //            {
        //                //Actualizar Estado de Habitacion
        //                MAT.Entities.Habitacion EHabitacionA = SHabitacion.GetByHabitacionId(item1.HabitacionId.Value);
        //                EHabitacionA.Ocupacion = EHabitacionA.Ocupacion - 1;
        //                SHabitacion.Update(EHabitacionA);

        //                if (EHabitacionA.Capacidad > EHabitacionA.Ocupacion)
        //                {
        //                    EHabitacionA.Estado = 0;//habitacion sin completar
        //                    SHabitacion.Update(EHabitacionA);
        //                }

        //                MAT.Services.ReservaHabitacionService SReserva = new ReservaHabitacionService();
        //                MAT.Entities.ReservaHabitacion EReserva = SReserva.GetByReservaHabitacionId(item1.ReservaHabitacionId);
        //                EReserva.Expiro = true;//cambia el estado de la reserva 
        //                SReserva.Update(EReserva);
        //            }
        //        }
        //    }
        //}

        //public bool ConfirmarCambioHabitacion(string anteriorid, string pasajeid, string viajeid, string nuevahabitacion)
        //public bool ConfirmarCambioHabitacion(string pasajeid, string viajeid, string nuevahabitacion)
        //{
        //    bool result = false;
        //    try
        //    {
        //        Guid _anteriorid = new Guid();
        //        Guid _pasajeid = new Guid();
        //        Guid _viajeid = new Guid();
        //        Guid _nuevahabitacion = new Guid();
        //        //if (anteriorid != null || anteriorid != "")
        //        //{
        //        //    _anteriorid = new Guid(anteriorid);
        //        //}
        //        if (pasajeid != null || pasajeid != "")
        //        {
        //            _pasajeid = new Guid(pasajeid);
        //        }
        //        if (viajeid != null || viajeid != "")
        //        {
        //            _viajeid = new Guid(viajeid);
        //        }
        //        if (nuevahabitacion != null || nuevahabitacion != "" )
        //        {
        //            _nuevahabitacion = new Guid(nuevahabitacion);
        //        }
                
        //        ReservaHabitacionService reservaService = new ReservaHabitacionService();
        //        //ReservaHabitacion reserva = reservaService.GetByPasajeId(_pasajeid).Where(re => re.ViajeId.Value == _viajeid).FirstOrDefault();
        //        ReservaHabitacion reserva = reservaService.GetByPasajeId(_pasajeid).Where(re => re.ViajeId.Value == _viajeid).Where(hab => hab.HabitacionId.Value == _anteriorid).FirstOrDefault();
        //        reserva.HabitacionId = _nuevahabitacion;
        //        reservaService.Update(reserva);
        //        result = true;
        //    }
        //    catch 
        //    {

        //        result = false;
        //    }
        //    return result;
        //}

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
                sResult = "Error: " + e.Message + "StackTrace: " + e.StackTrace;
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
                sResult = "Error: " + e.Message + "StackTrace: " + e.StackTrace;
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
