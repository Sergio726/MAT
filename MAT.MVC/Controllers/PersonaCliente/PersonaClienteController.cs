using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.Entities;
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
using MAT.MVC.Infrastructure.Clientes;
using MAT.MVC.Infrastructure.Data;
using MAT.MVC.Models.Clientes;
using System.Web.Security;
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
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController.RenderGridClientes");
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
                var msg = ErrorUtil.LogAndGetPublicMessage(ex, "PersonaClienteController.GetClientesTop");
                return Json(new
                {
                    success = false,
                    message = msg,
                    data = new List<PersonaClienteModel>(),
                    count = 0
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #region Listado y Altas por período (paginados en la base)

        private static readonly string[] OrdenesListado = { "apellido", "alta", "documento" };
        private static readonly int[] TamaniosPagina = { 25, 50, 100 };

        /// <summary>
        /// Listado de clientes paginado: palabras en cualquier orden, documento y teléfonos por dígitos,
        /// filtros por tipo de cliente y categoría de Seguimiento. Antes el listado usaba el SP de
        /// autocompletado y se cortaba en 10 clientes sin total ni paginación.
        /// </summary>
        [Authorize]
        [HttpGet]
        public ActionResult Buscar(string q = null, int? tipo = null, string categoria = null, string orden = "apellido", int pagina = 1, int tamanio = 25)
        {
            try
            {
                q = string.IsNullOrWhiteSpace(q) ? null : q.Trim();
                if (q != null && q.Length > 200) q = q.Substring(0, 200);
                categoria = SeguimientoCatalog.EsCategoriaValida(categoria) ? categoria : null;
                orden = OrdenesListado.Contains(orden) ? orden : "apellido";
                tamanio = TamaniosPagina.Contains(tamanio) ? tamanio : 25;
                pagina = Math.Max(1, pagina);

                var resultado = ClienteListadoDataAccess.Buscar(q, tipo, categoria, orden, pagina, tamanio);
                var categorias = SeguimientoCatalog.GetCategorias().ToDictionary(c => c.Codigo, c => c.Nombre);
                foreach (var c in resultado.Items)
                {
                    string nombreCategoria;
                    c.CategoriaNombre = c.Categoria != null && categorias.TryGetValue(c.Categoria, out nombreCategoria) ? nombreCategoria : null;
                    c.TipoNombre = Enum.IsDefined(typeof(eTipoCliente), c.TipoId) ? ((eTipoCliente)c.TipoId).GetDescription() : null;
                    var telefono = WhatsAppHelper.ElegirTelefono(c.Celular, c.Telefono);
                    c.TieneWhatsApp = telefono != null;
                    c.WhatsAppUrl = telefono == null ? null : WhatsAppHelper.ConstruirEnlace(telefono, null);
                }
                return JsonCamelCaseHelper.Serialize(new { success = true, resultado.Total, resultado.Pagina, resultado.Tamanio, data = resultado.Items });
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController.Buscar");
                return JsonCamelCaseHelper.Serialize(new { success = false, message = msg });
            }
        }

        /// <summary>
        /// Clientes dados de alta en un rango de fechas, paginado (modal "Altas por período" del listado y del inicio).
        /// Antes devolvía todas las filas de una vez (3.808 entre 2020 y hoy).
        /// </summary>
        [Authorize]
        public ActionResult GetClientesNuevosDetalle(string fechaDesde = null, string fechaHasta = null, int pagina = 1, int tamanio = 25)
        {
            try
            {
                DateTime desde, hasta;
                LeerPeriodo(fechaDesde, fechaHasta, out desde, out hasta);
                tamanio = TamaniosPagina.Contains(tamanio) ? tamanio : 25;
                var resultado = ClienteListadoDataAccess.ClientesNuevos(desde, hasta, Math.Max(1, pagina), tamanio);
                return JsonCamelCaseHelper.Serialize(new
                {
                    success = true,
                    resultado.Total,
                    resultado.Pagina,
                    resultado.Tamanio,
                    desde = desde.ToString("dd/MM/yyyy"),
                    hasta = hasta.ToString("dd/MM/yyyy"),
                    data = resultado.Items
                });
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController.GetClientesNuevosDetalle");
                return JsonCamelCaseHelper.Serialize(new { success = false, message = msg });
            }
        }

        /// <summary>Excel con todas las altas del período (para eso se usa el reporte).</summary>
        [Authorize]
        public ActionResult ClientesNuevosExcel(string fechaDesde = null, string fechaHasta = null)
        {
            try
            {
                DateTime desde, hasta;
                LeerPeriodo(fechaDesde, fechaHasta, out desde, out hasta);
                var todas = ClienteListadoDataAccess.ClientesNuevos(desde, hasta, 1, 0).Items;
                var bytes = ClientesExcelExport.BuildAltas(todas);
                return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    string.Format("altas-clientes-{0:yyyy-MM-dd}-a-{1:yyyy-MM-dd}.xlsx", desde, hasta));
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController.ClientesNuevosExcel");
                return new HttpStatusCodeResult(500, msg);
            }
        }

        /// <summary>"dd/MM/yyyy"; si falta o es inválida, el mes actual (hora argentina). Desde ≤ hasta.</summary>
        private static void LeerPeriodo(string fechaDesde, string fechaHasta, out DateTime desde, out DateTime hasta)
        {
            var hoy = ClienteValidacion.HoyArgentina();
            if (!ClienteValidacion.TryParseFecha(fechaDesde, out desde)) desde = new DateTime(hoy.Year, hoy.Month, 1);
            if (!ClienteValidacion.TryParseFecha(fechaHasta, out hasta)) hasta = new DateTime(hoy.Year, hoy.Month, 1).AddMonths(1).AddDays(-1);
            if (hasta < desde)
            {
                var aux = desde; desde = hasta; hasta = aux;
            }
        }

        #endregion


        #region Formulario de cliente (alta, alta desde presupuesto y edición)

        // Un solo formulario (_ClienteForm) con ClienteFormModel: etiquetas, reglas y mensajes salen de los
        // atributos del modelo y el navegador valida con las mismas reglas. Antes Create devolvía View() sin
        // modelo (45 helpers lanzaban NullReferenceException) y alta y edición eran dos formularios distintos.

        [Authorize]
        public ActionResult Create()
        {
            var modelo = ClienteFormDefaults.Nuevo();
            PrepararFormulario(modelo);
            return View(modelo);
        }

        /// <summary>
        /// Alta con los datos de un presupuesto (prospecto que decide comprar); al guardar vuelve a <paramref name="returnUrl"/>.
        /// </summary>
        [Authorize]
        public ActionResult CreateFromPresupuesto(Guid? presupuestoId, string returnUrl = null)
        {
            var modelo = ClienteFormDefaults.Nuevo();
            modelo.ReturnUrl = returnUrl;

            var presupuesto = presupuestoId.HasValue ? MAT.MVC.Models.PresupuestoMethod.GetById(presupuestoId.Value) : null;
            if (presupuesto == null)
            {
                ModelState.AddModelError(string.Empty, presupuestoId.HasValue ? "No se encontró el presupuesto." : "No se indicó el presupuesto.");
            }
            else
            {
                var nombreCompleto = (presupuesto.NombreCliente ?? string.Empty).Trim();
                if (nombreCompleto.Length > 0)
                {
                    var partes = nombreCompleto.Contains(",")
                        ? nombreCompleto.Split(new[] { ',' }, 2)
                        : nombreCompleto.Split(new[] { ' ' }, 2);
                    modelo.Apellido = partes[0].Trim();
                    modelo.Nombre = partes.Length > 1 ? partes[1].Trim() : null;
                }
                modelo.Celular = presupuesto.TelefonoCliente?.Trim();
                modelo.Email = presupuesto.EmailCliente?.Trim();
                modelo.NroDocumento = presupuesto.DniCliente?.Trim();
            }

            PrepararFormulario(modelo);
            return View("Create", modelo);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "PersonaId")] ClienteFormModel modelo, string accion)
        {
            modelo.PersonaId = null;
            var id = Guardar(modelo, FirmaVendedor.VendedorIdActual());
            if (!id.HasValue)
            {
                PrepararFormulario(modelo);
                return View(modelo);
            }

            var nombre = modelo.Apellido + ", " + modelo.Nombre;
            if (accion == "guardarYOtro")
            {
                TempData["ok"] = "Se guardó el cliente " + nombre + ". Podés cargar el siguiente.";
                return RedirectToAction("Create");
            }
            if (!string.IsNullOrWhiteSpace(modelo.ReturnUrl) && Url.IsLocalUrl(modelo.ReturnUrl))
                return Redirect(modelo.ReturnUrl);

            TempData["ok"] = "Se guardó el cliente " + nombre + ".";
            return RedirectToAction("Details", new { id = id.Value });
        }

        [Authorize]
        public ActionResult Edit(Guid id)
        {
            ClienteFormModel modelo;
            try
            {
                modelo = ClienteFormDataAccess.Obtener(id);
            }
            catch (Exception e)
            {
                ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController.Edit");
                return View("Error", e);
            }
            if (modelo == null) return HttpNotFound("No se encontró el cliente.");

            PrepararFormulario(modelo);
            return View(modelo);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, ClienteFormModel modelo)
        {
            modelo.PersonaId = id; // el id sale de la URL, no del formulario
            if (!Guardar(modelo, null).HasValue)
            {
                PrepararFormulario(modelo);
                return View(modelo);
            }

            TempData["ok"] = "Se guardaron los cambios.";
            return RedirectToAction("Details", new { id });
        }

        /// <summary>
        /// Aviso del formulario al salir del campo de documento: si otra persona ya lo tiene, devuelve su nombre
        /// y su id para enlazar la ficha. En la edición excluye al propio cliente.
        /// </summary>
        [Authorize]
        [HttpGet]
        public JsonResult DocumentoDisponible(string nroDocumento, Guid? personaId = null)
        {
            try
            {
                var existente = ClienteFormDataAccess.BuscarPorDocumento(nroDocumento, personaId);
                if (existente == null) return Json(new { disponible = true }, JsonRequestBehavior.AllowGet);
                return Json(new
                {
                    disponible = false,
                    personaId = existente.PersonaId,
                    nombre = existente.NombreCompleto,
                    esCliente = existente.EsCliente
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                // El aviso es una ayuda: si falla, el servidor vuelve a controlar al guardar.
                ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController.DocumentoDisponible");
                return Json(new { disponible = true }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Valida (atributos + documento repetido), normaliza y guarda. Devuelve el id, o null si hay errores
        /// (quedan en ModelState para mostrarlos en cada campo).
        /// </summary>
        private Guid? Guardar(ClienteFormModel modelo, Guid? vendedorId)
        {
            if (ModelState.IsValid)
            {
                var existente = ClienteFormDataAccess.BuscarPorDocumento(modelo.NroDocumento, modelo.PersonaId);
                if (existente != null)
                {
                    ModelState.AddModelError("NroDocumento", (existente.EsCliente ? "Ya existe un cliente con este documento: " : "Ya existe una persona con este documento: ")
                        + existente.NombreCompleto + ".");
                    ViewBag.DocumentoExistente = existente;
                }
            }
            if (!ModelState.IsValid) return null;

            try
            {
                ClienteFormMapper.Normalizar(modelo);
                return ClienteFormDataAccess.Guardar(modelo, vendedorId);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController.Guardar"));
                return null;
            }
        }

        private void PrepararFormulario(ClienteFormModel modelo)
        {
            ViewBag.Opciones = ClienteFormOpciones.Crear(modelo, GetAllProvincia(), GetAllOcupacion());
        }

        #endregion


        [Authorize]
        public ActionResult Details(Guid Id)
        {
            MAT.Entities.PersonaCliente PersonaCliente = new MAT.Entities.PersonaCliente();
            MAT.Entities.Persona Persona = Infrastructure.Data.PersonaDataAccess.GetById(Id);
            MAT.Entities.Cliente Cliente = Infrastructure.Data.ClienteDataAccess.GetById(Id);

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
            Infrastructure.Data.ClienteDataAccess.Delete(Id);
            Infrastructure.Data.PersonaDataAccess.Delete(Id);

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
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController");
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
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController");
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
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController");
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
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController");
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
                ViewBag.ListMenores = FacturaMetod.GetPasajeroMenorByFacturaID(facturaid);
                ViewBag.ExtendFacturaDetalle = FacturaMetod.GetDetalleByFacturaID(facturaid);
                return PartialView(factura);
            }
            catch (Exception e)
            {

                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController");
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

                var primerPasaje = data.FacturaDetalle?.FirstOrDefault();
                ViewBag.ViajeID = primerPasaje?.ViajeID.ToString() ?? "";

                // Sumar Cantidad de todos los renglones de seguro menor (puede haber varios ítems con Cantidad 1)
                ViewBag.CantMenores = data.Items?
                    .Where(x => x.Detalle != null && x.Detalle.ToUpper().Contains("MENOR"))
                    .Sum(x => x.Cantidad) ?? 0;

                // Reutilizamos la misma vista parcial para minimizar cambios UI.
                return PartialView("DetalleFactura", data.Factura ?? new FacturaStandard());
            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController");
                return View("Error", e);
            }
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
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController");
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
                sResult[1] = ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController");
            }
            return Json(sResult, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult Voucher(Guid facturaid, string sTipoVoucher = "Individual", bool bInfoAdicional = false)
        {
            List<Models.VoucherStandard> ListVoucher = new List<Models.VoucherStandard>();
            ViewBag.ListMenores = new List<PasajeroMenorModel>();
            try
            {
                ListVoucher = Models.VoucherMethod.GetVoucherByFacturaID(facturaid.ToString());
                ViewBag.ListMenores = FacturaMetod.GetPasajeroMenorByFacturaID(facturaid);
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
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController.Voucher");
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
                sResult[1] = ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController");
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
            if (!SistemaParametroHelper.ValidarCodigo(code))
            {
                return false;
            }
                    
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
                if (!SistemaParametroHelper.ValidarCodigo(code))
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
                sResult[1] = ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController.EliminarPasajeroDePasaje");
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
                sResult[1] = ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController.CambiarPasajeroDePasaje");
            }
            
            return Json(sResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ElegirNuevaButaca(Guid anteriorid, Guid pasajeid, bool shell = false)
        {
            ViewData["anteriorid"] = anteriorid;
            ViewData["pasajeid"] = pasajeid;
            ViewBag.Shell = shell;
            List<PasajeModel> pasajes = new List<PasajeModel>();
            Guid viajeid = Infrastructure.Data.PasajeDataAccess.GetById(pasajeid).ViajeId.Value;
            
            List<ReservaStandard> Model = new List<ReservaStandard>();

            try
            {
                Model = ReservaMethod.GetListOfPasajesByViajeID(viajeid.ToString());
                ViewBag.PreReservas = ReservaMethod.GetPreReservaVencidas(viajeid.ToString());
            }
            catch (Exception e)
            {
                ViewBag.MsgError = ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController.ElegirNuevaButaca");
            }

            return shell ? (ActionResult)View(Model) : PartialView(Model);
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
        [HttpPost]
        public JsonResult ConfirmarCambioButaca(Guid pasajeid, string adicional, Guid nuevopasaje)
        {
            try
            {
                MVC.Models.ReservaMethod.CambioButacas(adicional ?? string.Empty, pasajeid, nuevopasaje);
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController.ConfirmarCambioButaca");
                return Json(new { success = false, message = msg });
            }
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
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController");
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

                var reserva = Infrastructure.Data.ReservaHabitacionDataAccess.GetByPasajeId(_pasajeid)
                    .Where(re => re.ViajeId.HasValue && re.ViajeId.Value == _viajeid)
                    .FirstOrDefault();
                if (reserva != null)
                {
                    reserva.HabitacionId = _nuevahabitacion;
                    Infrastructure.Data.ReservaHabitacionDataAccess.Update(reserva);
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
            try
            {
                if (string.IsNullOrWhiteSpace(sFacturaID)
                    || string.IsNullOrWhiteSpace(sMonto)
                    || string.IsNullOrWhiteSpace(sDetalle)
                    || string.IsNullOrWhiteSpace(sDescuentoIncremento))
                {
                    return Json(new
                    {
                        success = false,
                        Estado = "Validation",
                        message = "Complete monto, detalle y tipo de ajuste."
                    }, JsonRequestBehavior.AllowGet);
                }

                Guid facturaId;
                if (!Guid.TryParse(sFacturaID.Trim(), out facturaId) || facturaId == Guid.Empty)
                {
                    return Json(new
                    {
                        success = false,
                        Estado = "Validation",
                        message = "La factura indicada no es válida."
                    }, JsonRequestBehavior.AllowGet);
                }

                decimal monto;
                if (!TryParseMoneyInput(sMonto, out monto) || monto <= 0m)
                {
                    return Json(new
                    {
                        success = false,
                        Estado = "Validation",
                        message = "El monto debe ser un número mayor a cero."
                    }, JsonRequestBehavior.AllowGet);
                }

                var detalle = sDetalle.Trim();
                if (detalle.Length == 0 || detalle.Length > 200)
                {
                    return Json(new
                    {
                        success = false,
                        Estado = "Validation",
                        message = "Ingrese un detalle válido (máximo 200 caracteres)."
                    }, JsonRequestBehavior.AllowGet);
                }

                bool isDescuento = string.Equals(sDescuentoIncremento.Trim(), "Descuento", StringComparison.OrdinalIgnoreCase);
                if (!isDescuento && !string.Equals(sDescuentoIncremento.Trim(), "Incremento", StringComparison.OrdinalIgnoreCase))
                {
                    return Json(new
                    {
                        success = false,
                        Estado = "Validation",
                        message = "El tipo de ajuste no es válido."
                    }, JsonRequestBehavior.AllowGet);
                }

                FacturaMetod.AgregarDescuento_Recargo(facturaId.ToString(), detalle, monto, isDescuento);

                return Json(new
                {
                    success = true,
                    Estado = "Done.",
                    message = isDescuento ? "Descuento aplicado correctamente." : "Incremento aplicado correctamente."
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController.AddDescuentoRecargo");
                return Json(new
                {
                    success = false,
                    Estado = "Error",
                    message = msg
                }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Normaliza montos con formato AR (1.234,56) o invariante (1234.56).
        /// </summary>
        private static bool TryParseMoneyInput(string input, out decimal value)
        {
            value = 0m;
            if (string.IsNullOrWhiteSpace(input))
                return false;

            var s = input.Trim().Replace(" ", "");

            // Prefijos de moneda antes de quitar "$" (U$D contiene el símbolo)
            if (s.StartsWith("U$D", StringComparison.OrdinalIgnoreCase))
                s = s.Substring(3);
            else if (s.StartsWith("USD", StringComparison.OrdinalIgnoreCase))
                s = s.Substring(3);

            s = s.Replace("$", "").Trim();

            if (s.Contains(",") && s.Contains("."))
            {
                if (s.LastIndexOf(',') > s.LastIndexOf('.'))
                    s = s.Replace(".", "").Replace(",", ".");
                else
                    s = s.Replace(",", "");
            }
            else if (s.Contains(","))
            {
                s = s.Replace(",", ".");
            }

            return decimal.TryParse(
                s,
                System.Globalization.NumberStyles.Number,
                System.Globalization.CultureInfo.InvariantCulture,
                out value);
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
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController");
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
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController");
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
                sResult[1] = ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController");
            }
            return Json(sResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult DetalleFacturaEliminarItem(int detalleFacturaId, Guid facturaId)
        {
            try
            {
                if (MATContext.CurrentVendedor == null)
                {
                    return Json(new { success = false, message = "No se pudo identificar al vendedor en sesión." }, JsonRequestBehavior.AllowGet);
                }

                FacturaMetod.DeleteDetalleFacturaLine(detalleFacturaId, facturaId, MATContext.CurrentVendedor.VendedorId);
                return Json(new { success = true, message = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "PersonaClienteController.DetalleFacturaEliminarItem");
                return Json(new { success = false, message = msg }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
