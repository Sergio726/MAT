using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.MVC.Models;
using MAT.Services;
using MAT.Entities;
using System.Collections.Specialized;
using MAT.Utilities;
using MAT.Enums;
using MAT.MVC.Common;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Web.Security;
using WebMatrix.WebData;
using MAT.MVC.Integration;
using System.Threading.Tasks;
using AutoMapper;
using MAT.MVC.Integration.BackendApi.Models;
using MAT.MVC.Infrastructure;

namespace MAT.MVC.Controllers.Reserva
{
    public class ReservaController : Controller
    {
        private BackendAPI _backendAPI;

        public ReservaController()
        {
            _backendAPI = new BackendAPI();
        }
        
        [Authorize]
        public async Task<ActionResult> Index(Guid viajeid)
        {
            List<ReservaStandard> Model = new List<ReservaStandard>();

            try
            {
                Model = ReservaMethod.GetListOfPasajesByViajeID(viajeid.ToString());                
                ViewBag.PreReservas = ReservaMethod.GetPreReservaVencidas(viajeid.ToString());
                ViewBag.ListaEspera = ListaEsperaModel.Method.GetCountListaEsperaByViajeId(viajeid.ToString()).Tables[0].Rows[0]["CountListaEspera"];

            }
            catch (Exception e)
            {
                ViewBag.MsgError = e.Message;
            }

            return View(Model);
        }
        
        public ActionResult DistribucionCoche(string ViajeID)
        {
            ViewBag.ViajeID = ViajeID;

            List<Models.DistribucionCoche> DistCoche = new List<Models.DistribucionCoche>();
            List<FacturaDetalle> DetalleFactura = new List<FacturaDetalle>();
            
            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@ViajeID", SqlDbType.VarChar, 0, ViajeID),
                };
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Reserva_DistribucionCoche_GetByViajeID", dbParams))
            {
                while (_reader.Read())
                {
                    Models.DistribucionCoche Item = new Models.DistribucionCoche();
                    if (_reader["ButacaNro"].ToString() != "")
                    {
                        Item.ButacaNro = Convert.ToInt32(_reader["ButacaNro"].ToString());
                        Item.ButacaPosicion = _reader["ButacaPosicion"].ToString();
                        Item.ButacaCodigo = _reader["ButacaCodigo"].ToString();
                        Item.PasajeroID = _reader["PasajeroID"].ToString();
                        Item.PasajeroApellido = _reader["PasajeroApellido"].ToString();
                        Item.PasajeroNombre = _reader["PasajeroNombre"].ToString();
                        DistCoche.Add(Item);
                    }
                }

                _reader.NextResult();
                while (_reader.Read())
                {
                    ViewBag.NroCoche = _reader["NroCoche"].ToString();
                    ViewBag.TransporteTipo = _reader["TransporteTipo"].ToString();
                }
            }

            return PartialView(DistCoche);
        }

        public string QuickSearch()
        {
            SearchModel searchmodel = new SearchModel();
            searchmodel.FillGrid("PersonaPasajero");
            return searchmodel.HtmlGrid;
        }

        public JsonResult QuickPasajeroSearch(string query)
        {
            PersonaPasajeroService pasajeroService = new PersonaPasajeroService();
            List<Entities.PersonaPasajero> pasajeros = pasajeroService.GetAll().Where(p => p.NroDocumento.Contains(query) || p.Nombre.ToUpper().Contains(query.ToUpper()) || p.Apellido.ToUpper().Contains(query.ToUpper())).ToList();
            return Json(pasajeros, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Búsqueda rápida de clientes por nombre, apellido o DNI.
        /// Optimizado: usa stored procedure dbo.usp_MAT_PersonaCliente_Search en lugar de cargar todos en memoria.
        /// </summary>
        public JsonResult QuickClienteSearch(string query)
        {
            var result = new List<object>();
            
            if (string.IsNullOrWhiteSpace(query) || query.Length < 2)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            try
            {
                // Usar el stored procedure optimizado
                var clientes = MAT.MVC.Models.PersonaClienteMethod.PersonaClienteSearchByNombreDNI(query);
                
                // Limitar resultados para autocomplete (máximo 20)
                result = clientes.Take(20).Select(c => new
                {
                    ClienteId = c.PersonaId,
                    Nombre = c.Nombre,
                    Apellido = c.Apellido,
                    NroDocumento = c.NroDocumento,
                    Telefono = c.Telefono
                }).ToList<object>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en QuickClienteSearch: {ex.Message}");
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult jRenovarPreReserva(string FacturaID)
        {
            string sResult = ReservaMethod.RenovarPreReserva(FacturaID);
            return Json(new
                        {
                            sResult = sResult
                        },JsonRequestBehavior.AllowGet);
        }
        public ActionResult SeleccionarPasajero(Guid paqueteid, string piso)
        {
            try
            {
                if (!string.IsNullOrEmpty(piso))
                    ViewData["piso"] = piso;

                SeleccionarPasajeroModel seleccionarpasajeromodel = new SeleccionarPasajeroModel(paqueteid);
                return PartialView(seleccionarpasajeromodel);
            }
            catch (Exception ex)
            {
                return PartialView();
            }
        }


        public ActionResult RenderGridPasajeros(Guid viajeid, string filter = "", int pageSize = 50, int pageIndex = 0)
        {
            List<MAT.Entities.PersonaCliente> LPersonaCliente = new List<MAT.Entities.PersonaCliente>();
            try
            {
                SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@ViajeID", SqlDbType.VarChar, 0, Convert.ToString(viajeid)),
                    };
                using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Reserva_GetClientesDisponibles", dbParams))
                {
                    int currentIndex = 0;
                    int skipCount = pageIndex * pageSize;
                    int takeCount = 0;

                    while (_reader.Read())
                    {
                        // Aplicar filtro si existe
                        string apellido = _reader["Apellido"].ToString().ToLower();
                        string nombre = _reader["Nombre"].ToString().ToLower();
                        string nroDoc = _reader["NroDocumento"].ToString().ToLower();
                        string searchTerm = string.IsNullOrEmpty(filter) ? "" : filter.ToLower();

                        bool matchesFilter = string.IsNullOrEmpty(filter) || 
                                            apellido.Contains(searchTerm) || 
                                            nombre.Contains(searchTerm) || 
                                            nroDoc.Contains(searchTerm);

                        if (!matchesFilter)
                            continue;

                        // Paginación: saltar registros hasta llegar a la página solicitada
                        if (currentIndex < skipCount)
                        {
                            currentIndex++;
                            continue;
                        }

                        // Tomar solo los registros de la página actual
                        if (takeCount >= pageSize)
                            break;

                        MAT.Entities.PersonaCliente item = new MAT.Entities.PersonaCliente();
                        item.ClienteId = new Guid(_reader["ClienteID"].ToString());
                        item.Apellido = _reader["Apellido"].ToString();
                        item.Nombre = _reader["Nombre"].ToString();
                        item.TipoDocumento = Convert.ToInt32(_reader["TipoDocumento"].ToString());
                        item.NroDocumento = _reader["NroDocumento"].ToString();
                        item.Telefono = _reader["Telefono"].ToString();
                        item.Email = _reader["Email"].ToString();
                        LPersonaCliente.Add(item);

                        currentIndex++;
                        takeCount++;
                    }
                }
            }
            catch
            { 
            
            }

            return PartialView(LPersonaCliente);
        }

        // Endpoint para autocomplete - devuelve solo sugerencias (máximo 20)
        [HttpPost]
        public JsonResult BuscarPasajerosAutocomplete(Guid viajeid, string term)
        {
            List<object> sugerencias = new List<object>();
            try
            {
                if (string.IsNullOrWhiteSpace(term) || term.Length < 2)
                {
                    return Json(sugerencias, JsonRequestBehavior.AllowGet);
                }

                SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@ViajeID", SqlDbType.VarChar, 0, Convert.ToString(viajeid)),
                };
                using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Reserva_GetClientesDisponibles", dbParams))
                {
                    string searchTerm = term.ToLower();
                    int count = 0;
                    int maxResults = 20;

                    while (_reader.Read() && count < maxResults)
                    {
                        string apellido = _reader["Apellido"].ToString().ToLower();
                        string nombre = _reader["Nombre"].ToString().ToLower();
                        string nroDoc = _reader["NroDocumento"].ToString().ToLower();
                        string nombreCompleto = _reader["Apellido"].ToString() + ", " + _reader["Nombre"].ToString();

                        // Buscar en apellido, nombre o documento
                        if (apellido.Contains(searchTerm) || 
                            nombre.Contains(searchTerm) || 
                            nroDoc.Contains(searchTerm) ||
                            nombreCompleto.ToLower().Contains(searchTerm))
                        {
                            sugerencias.Add(new
                            {
                                id = _reader["ClienteID"].ToString(),
                                label = nombreCompleto + " - DNI: " + _reader["NroDocumento"].ToString(),
                                value = nombreCompleto,
                                apellido = _reader["Apellido"].ToString(),
                                nombre = _reader["Nombre"].ToString(),
                                nroDocumento = _reader["NroDocumento"].ToString(),
                                tipoDocumento = _reader["TipoDocumento"].ToString(),
                                telefono = _reader["Telefono"].ToString(),
                                email = _reader["Email"].ToString()
                            });
                            count++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error si es necesario
            }

            return Json(sugerencias, JsonRequestBehavior.AllowGet);
        }

        
        [HttpPost]
        public ActionResult ReservarPasajes(List<PasajeInputModel> pasajes, string montoSeguroMenor = "")
        {
            try
            {
                string jsonobject = JsonConvert.SerializeObject(pasajes);
                ViewBag.Pasajes = jsonobject;
                ViewBag.CantPasajes = pasajes.Count();

                
                //MATContext.Reserva = new ReservaModel(pasajes);
                //MontoFactura = MATContext.Reserva.CalcularMontoTotal();

                List<Guid> precioIds = pasajes
                    .Where(p => Guid.TryParse(p.precioid, out _)) // Filtra los valores válidos
                    .Select(p => Guid.Parse(p.precioid)) // Convierte los valores a Guid
                    .ToList();

                //List<Guid> adicionalesIds = pasajes
                //    .Where(p => !string.IsNullOrWhiteSpace(p.adicionalesid ?? "") && Guid.TryParse(p.adicionalesid, out _))
                //    .Select(p => Guid.Parse(p.adicionalesid))
                //    .ToList();

                List<Guid> adicionalesIds = pasajes
                            .Select(p => string.IsNullOrWhiteSpace(p.adicionalesid) ? Guid.Empty : Guid.Parse(p.adicionalesid))
                            .ToList();


                double MontoFactura = 0;
                MontoFactura = ReservaModel.CalcularMontoTotal(precioIds, adicionalesIds);
                ViewBag.MontoFactura = MontoFactura;
                //return PartialView("FormReserva", MATContext.Reserva);
                return PartialView("FormReserva");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return PartialView("FormReserva");
            }
        }

        public ActionResult VinculacionMenor(string sViajeId = "")
        {
            ViewBag.ViajeID = sViajeId;
            return PartialView();
            
        }

        public ActionResult GetOffListPassengers(string sViajeId = "")
        {
            ViewBag.ViajeID = sViajeId;
            return PartialView();

        }
        [Authorize]
        public JsonResult GetWatinList(string ViajeID)
        {
            string[] sResult = new string[2];
            DataSet ds = new DataSet();
            string json = "";
            try
            {
                ds = MVC.Models.ListaEsperaModel.Method.GetListaEspera(ViajeID);
                json = JsonConvert.SerializeObject(ds, Formatting.Indented);
                
                sResult[0] = json;
                sResult[1] = "";
            }
            catch (Exception e)
            {
                sResult[0] = "";
                sResult[1] = "Error: " + ErrorUtil.LogAndGetPublicMessage(e, "ReservaController.GetWatinList");
            }


            return Json(new
            {
                jsTable = sResult[0],
                Error = sResult[1]
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PersonaAutocomplete(string sParam, string sViajeID)
        {
            List<ListaEsperaModel.ResultSearchCliente> lSearch = new List<ListaEsperaModel.ResultSearchCliente>();
            lSearch = ListaEsperaModel.Method.PersonaClienteSearchByNombreDNI(sParam,sViajeID);

            return Json(lSearch, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertPersonToWatingList(string sPersonaID, string sViajeID, string sObservacion, string sPasajeroTemporal)
        {
            ListaEsperaModel.ListaEspera objWatingList = new ListaEsperaModel.ListaEspera();
            objWatingList.ClienteID = sPersonaID;
            objWatingList.ViajeID = sViajeID;
            objWatingList.UsuarioID = Convert.ToInt32(Membership.GetUser(User.Identity.Name).ProviderUserKey);
            objWatingList.Observacion = sObservacion;
            objWatingList.PasajeroTemporal = sPasajeroTemporal;

            string[] sResult = new string[2];
            sResult = ListaEsperaModel.Method.InsertListaEspera(objWatingList);

            return Json(new
            {
                Mensaje = sResult[0],
                Error = sResult[1]
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteToWatingList(string Id)
        {
            string[] sResult = new string[2];
           
            
            try
            {
                sResult = MVC.Models.ListaEsperaModel.Method.DeleteListaEspera(Convert.ToInt32(Id), WebSecurity.GetUserId(User.Identity.Name));
               
            }
            catch (Exception e)
            {
                sResult[0] = "";
                sResult[1] = "Error: " + ErrorUtil.LogAndGetPublicMessage(e, "ReservaController.DeleteToWatingList");
            }


            return Json(new
            {
                // Mensaje: "Done" | "Error"
                // Error: texto humano para UI (vacío si Done)
                Mensaje = string.IsNullOrWhiteSpace(sResult[0]) ? "Error" : sResult[0],
                Error = (string.Equals(sResult[0], "Done", StringComparison.OrdinalIgnoreCase) ? "" : (sResult[1] ?? ""))
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListTutoresByViajeID(string sViajeId = "")
        { 
            List<PasajeroMenorModel> model = new List<PasajeroMenorModel>();
            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@ViajeID", SqlDbType.VarChar, 0, Convert.ToString(sViajeId)),
                };
            List<string> listTutores = new List<string>();
            
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Reserva_GetPasajeroMenor", dbParams))
            {
                while (_reader.Read())
                {
                    string item = _reader["MayorID"].ToString();
                    if (!listTutores.Contains(item))
                    {
                        listTutores.Add(item);
                    }
                }
            }

            var jsonPatientList = JsonConvert.SerializeObject(listTutores);

            return Json(new 
                        { 
                            Tutores = jsonPatientList 
                        },
                        JsonRequestBehavior.AllowGet);
        }

        public ActionResult PartialVinculacionMenor(string sViajeId = "", string sPrint = "")
        {
            List<PasajeroMenorModel> model = new List<PasajeroMenorModel>();
            ViewBag.ViajeID = sViajeId;
            try
            {
                SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@ViajeID", SqlDbType.VarChar, 0, Convert.ToString(sViajeId)),
                    };
                using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Reserva_GetPasajeroMenor", dbParams))
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

                    _reader.NextResult();
                    if (_reader.Read())
                    {
                        ViewBag.NombrePaquete = _reader["NombrePaquete"].ToString();
                        ViewBag.FechaSalida = Convert.ToDateTime(_reader["FechaSalida"]).ToShortDateString();
                        ViewBag.FechaRegreso = Convert.ToDateTime(_reader["FechaRegreso"]).ToShortDateString();
                    }
                }


            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }

            if (sPrint != "Print")
            {
                return PartialView("PartialVinculacionMenor", model);
            }
            else
            {
                return PartialView("PrintVinculacionMenor", model);
            }

            

        }

        public ActionResult Observaciones(string sViajeId = "")
        {
            ViewBag.ViajeID = sViajeId;
            return PartialView();
            
        }

        public ActionResult ObservacionesGenerales(string sViajeId = "")
        {
            List<MAT.MVC.Models.ObservacionViaje.ObsViaje> Observaciones = new List<MAT.MVC.Models.ObservacionViaje.ObsViaje>();
            try
            {
                Observaciones = ObservacionViaje.Method.GetObservaciones(sViajeId);
                ViewBag.ViajeId = sViajeId;
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }

            return PartialView(Observaciones);
        }

        public ActionResult ObservacionABM(string sViajeID, string sId = "")
        {
            MAT.MVC.Models.ObservacionViaje.ObsViaje Observaciones = new MAT.MVC.Models.ObservacionViaje.ObsViaje();
            try
            {

                if (sId != "")
                {
                    Observaciones = ObservacionViaje.Method.GetObservacion(sId);
                }
                else {
                    Observaciones.Id = 0;
                    Observaciones.Fecha = DateTime.Now;
                    Observaciones.Vendedor = User.Identity.Name;
                    Observaciones.VendedorID = Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString();
                    Observaciones.Categoria = "DETALLES-OTROS";
                    Observaciones.CategoriaID = 3;
                    Observaciones.ViajeID = sViajeID;
                }

                //load Lista Pasajeros
                List<MAT.MVC.Models.PasajeroViaje> ListPasajeros = ViajeMethod.GetListPasajerosByViajeID(new Guid(sViajeID));

                var json = "";
                var jsonSerialiser = new JavaScriptSerializer();
                json = jsonSerialiser.Serialize(ListPasajeros);
                ViewBag.jDDPasajeros = json;

                //load Categorias
                List<MAT.MVC.Models.ObservacionViaje.ObservacionViajeCategoria> LCategorias = ObservacionViaje.Method.GetObservacionViajeCategoria();

                var json2 = "";
                var jsonSerialiser2 = new JavaScriptSerializer();
                json2 = jsonSerialiser2.Serialize(LCategorias);
                ViewBag.jDDCategorias = json2;
            
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }

            return PartialView(Observaciones);
        }

        [Authorize]
        public JsonResult ObservacionesGeneralesEdit(MAT.MVC.Models.ObservacionViaje.ObsViaje ObsViaje)
        {
            string[] sResult = new string[2];
            try 
            {
                if (ObsViaje.Id == 0)
                {
                    #region insert
                    ObsViaje.Id = MAT.MVC.Models.ObservacionViaje.Method.ObservacionViaje_Insert(ObsViaje);

                    #endregion
                }
                else
                {
                    #region update
                    ObsViaje.VendedorID = Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString();
                    MAT.MVC.Models.ObservacionViaje.Method.ObservacionViaje_Update(ObsViaje);

                    #endregion

                }

                sResult[0] = ObsViaje.Id.ToString();
                sResult[1] = "Done.";
            }
            catch (Exception e)
            {
                sResult[0] = "";
                sResult[1] = e.Message;
            }

            return Json(new
            {
                Id = sResult[0],
                Msj = sResult[1]
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult ObservacionesGeneralesDelete(Int32 Id)
        {
            string[] sResult = new string[2];
            try
            {
                sResult[0] = MAT.MVC.Models.ObservacionViaje.Method.ObservacionViaje_Delete(Id, Convert.ToInt32(Membership.GetUser(User.Identity.Name).ProviderUserKey));
                sResult[1] = "Done.";
            }
            catch (Exception e)
            {
                sResult[0] = "";
                sResult[1] = e.Message;
            }

            return Json(new
            {
                Msj = sResult[0],
                Result = sResult[1]
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult PrintObservaciones(string sViajeId)
        {
            List<MAT.MVC.Models.ObservacionViaje.ObsViaje> Observaciones = new List<MAT.MVC.Models.ObservacionViaje.ObsViaje>();

            try
            {
                Observaciones = ObservacionViaje.Method.GetObservaciones(sViajeId);
                ViewBag.ViajeId = sViajeId;

                DataSet ds = MAT.MVC.Models.ViajeMethod.GetDetalleViaje(new Guid(sViajeId));

               string[] sViaje = new string[4];

                foreach (DataRow dt in ds.Tables[0].Rows)
                {
                    sViaje[0] = dt["Descripcion"].ToString();
                    sViaje[1] = dt["Destino"].ToString();
                    sViaje[2] = dt["FechaSalida"].ToString();
                    sViaje[3] = dt["FechaRegreso"].ToString();
                }

                ViewBag.DetalleViaje = sViaje;

            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }

            return PartialView(Observaciones);
        }

        public ActionResult FormListaMayor(string sViajeID = "")
        {
            List<PasajeroMayor> LPersonaMayor = new List<PasajeroMayor>();
            try
            {
                SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@ViajeID", SqlDbType.VarChar, 0, sViajeID),
                    };
                using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Reserva_GetMayoresDisponibles", dbParams))
                {
                    while (_reader.Read())
                    {
                        PasajeroMayor item = new PasajeroMayor();
                        item.PasajeID = _reader["PasajeID"].ToString();
                        item.ClienteId = _reader["ClienteID"].ToString();
                        item.Apellido = _reader["Apellido"].ToString();
                        item.Nombre = _reader["Nombre"].ToString();
                        item.TipoDocumento = Convert.ToInt32(_reader["TipoDocumento"].ToString());
                        item.NroDocumento = _reader["NroDocumento"].ToString();
                        item.Telefono = _reader["Telefono"].ToString();
                        item.Email = _reader["Email"].ToString();
                        LPersonaMayor.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return PartialView(LPersonaMayor);
        }

        public ActionResult FormListaMenor(string sViajeID = "")
        {
            List<MAT.Entities.PersonaCliente> LPersonaCliente = new List<MAT.Entities.PersonaCliente>();
            try
            {
                SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@ViajeID", SqlDbType.VarChar, 0, sViajeID),
                    };
                using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Reserva_GetMenoresDisponibles", dbParams))
                {
                    while (_reader.Read())
                    {
                        MAT.Entities.PersonaCliente item = new MAT.Entities.PersonaCliente();
                        item.ClienteId = new Guid(_reader["PersonaID"].ToString());
                        item.Apellido = _reader["Apellido"].ToString();
                        item.Nombre = _reader["Nombre"].ToString();
                        //item.TipoDocumento = Convert.ToInt32(_reader["TipoDocumento"].ToString());
                        item.NroDocumento = _reader["NroDocumento"].ToString();
                        //item.Telefono = _reader["Telefono"].ToString();
                        //item.Email = _reader["Email"].ToString();
                        LPersonaCliente.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return PartialView(LPersonaCliente);
        }

        public JsonResult VincularMenor(string sPasajeID = "", string sMayorID = "", string sMenorID = "" )
        {
            string[] sResult = new string[2];
            
            try
            {
                if (sPasajeID != "" && sMayorID != "" && sMenorID != "")
                {
                    SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@PasajeID", SqlDbType.VarChar, 0, sPasajeID),
                        DBHelper.MakeParam("@PasajeroID", SqlDbType.VarChar, 0, sMayorID),
                        DBHelper.MakeParam("@MenorID", SqlDbType.VarChar, 0, sMenorID)
                    };
                    SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Reserva_VincularMenor", dbParams);


                    if (_reader.Read())
                    {
                        sResult[0] = _reader["Id"].ToString();
                        sResult[1] = _reader["ErrorMsg"].ToString();

                    }
                }
                
            }
            catch (Exception e)
            {
                sResult[0] = "-1";
                sResult[1] = e.Message;

            }
            return Json(new
            {
                Id = sResult[0],
                Msj = sResult[1]
            }, JsonRequestBehavior.AllowGet);
        }


        public string[] VincularMenorByViaje(string sViaje = "", string sMayorID = "", string sMenorID = "")
        {
            string[] sResult = new string[2];

            try
            {
                if (sViaje != "" && sMayorID != "" && sMenorID != "")
                {
                    SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@ViajeID", SqlDbType.VarChar, 0, sViaje),
                        DBHelper.MakeParam("@PasajeroID", SqlDbType.VarChar, 0, sMayorID),
                        DBHelper.MakeParam("@MenorID", SqlDbType.VarChar, 0, sMenorID)
                    };
                    using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Reserva_VincularMenorByViajeID", dbParams))
                    {
                        if (_reader.Read())
                        {
                            sResult[0] = _reader["Id"].ToString();
                            sResult[1] = _reader["ErrorMsg"].ToString();
                        }
                    }
                }

            }
            catch (Exception e)
            {
                sResult[0] = "-1";
                sResult[1] = e.Message;

            }
            return sResult;
        }

        //26-03-2017: nuevo proceso para la reserva de pasajes
        [HttpPost]
        public string FormReserva(string cliente, string tipopago, string condicion, string recibo, string TransaccionId, string nroFactura, 
                                  string observaciones, string jsonobject, string descuento = "", string monto = "0", string montoFactura = "0", string listmenores = "",
                                  string tutormenor = "", string viajeid = "", string detalledescuento = "",
                                  string MontoRecibido = "", string MontoRecibidoMonedaTipo = "1",string MontoEquivalente = "", string MontoEquivalenteMonedaTipo = "", string MontoEquivalenteCotizacion = "", string ViajeMonedaTipo = "1")
        {
            try
            {
                

                if (descuento == "")
                {
                    descuento = "0";
                }
                if (monto == "")
                {
                    monto = "0";
                }
                if (montoFactura == "")
                {
                    montoFactura = "0";
                }

                /*
                 1. registrar factura como en cuenta corriente
                 2. registrar detalle de factura
                 3. registrar pasajeros
                 4. registrar pago
                 5. vincular menor
                 */

                List<PasajeInputModel> _pasajes = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<PasajeInputModel>>(jsonobject);

                string vendedorId = MATContext.CurrentVendedor.VendedorId.ToString();
                DataSet dsRegFactura = MVC.Models.ReservaMethod.RegistrarFactura(cliente, vendedorId, observaciones, condicion, Convert.ToInt32(ViajeMonedaTipo));

                string sFacturaID = dsRegFactura.Tables[0].Rows[0]["FacturaID"].ToString();

                if (sFacturaID != "")
                {                    
                    foreach (PasajeInputModel pasaje in _pasajes)
                    {
                        string _adicionalesid = "";
                        if (!string.IsNullOrEmpty(pasaje.adicionalesid))
                        {
                            _adicionalesid = string.Join(",", pasaje.adicionalesid);
                        }
                        //registrar pasajeros como prereserva
                        MVC.Models.ReservaMethod.UpdatePasajeAdicionalesVoucher(pasaje.pasajeid, sFacturaID, pasaje.pasajeroid, _adicionalesid,5, vendedorId);

                        //registrar detalles de factura

                        MVC.Models.ReservaMethod.AddDetalleFactura(sFacturaID, _adicionalesid, pasaje.precioid);

                    }
                }

                //agregar descuento
                if (Convert.ToDecimal(descuento) > 0)
                {
                    MVC.Models.FacturaMetod.AgregarDescuento_Recargo(sFacturaID, detalledescuento, Convert.ToDecimal(descuento), true);
                }

                string sEstadoFactura = "";    

                //registrar pago 
                if (Convert.ToDecimal(monto) > 0)
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
                   


                    DataSet ds = MVC.Models.PagoMethod.NuevoPago(Convert.ToDecimal(monto), cliente, recibo, TransaccionId, Convert.ToInt32(tipopago), sFacturaID, nroFactura, dMontoRecibido, Convert.ToInt32(MontoRecibidoMonedaTipo), dMontoEquivalente,iMontoEquivalenteMonedaTipo, dMontoEquivalenteCotizacion);
                
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        if (item["Result"].ToString() == "Done.")
                        {
                            sEstadoFactura = item["EstadoFactura"].ToString();
                            break;
                        }
                    }

                }else if (monto == "0"){
                    sEstadoFactura = "Pre-reserva";
                }
                //Vincular Menor
                VincularMenorByViaje(viajeid, tutormenor, listmenores);

                return sEstadoFactura;
            }
            catch (Exception ex)
            {
                MATLogger.Log(String.Format("{0} {1}", ex.Message, ex.StackTrace), 1);
                return "Error";
            }
        }

        public JsonResult jSenasIncompletasByViajeID(string ViajeID)
        {
            List<MVC.Models.PFC> lPasajes = new List<MVC.Models.PFC>();
            var sResult = "";
            string Msg = "Done.";
            try
            {
                DataSet ds = MAT.MVC.Models.ReservaMethod.GetSenasByViajeID(new Guid(ViajeID));

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    double dTotalFactura = Convert.ToDouble(dr["TotalFactura"]);
                    double dTotalPagos = Convert.ToDouble(dr["TotalPagos"]);
                    double percent = (dTotalPagos * 100) / dTotalFactura;
                    if (percent < 30)
                    {
                        MVC.Models.PFC item = new MVC.Models.PFC();
                        item.PasajeID = dr["PasajeID"].ToString();
                        item.FacturaID = dr["FacturaID"].ToString();
                        item.ClienteID = dr["ClienteID"].ToString();
                        lPasajes.Add(item);
                    }
                }

                sResult = JsonConvert.SerializeObject(lPasajes);
            }
            catch (Exception e) { 
                sResult = ""; 
                Msg = e.Message; 
            };

            return Json(new
            {
                sResult = sResult,
                sMsg = Msg
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DetalleViaje(string sViajeID = "")
        {
            MAT.MVC.Models.DetalleViaje oDetalleViaje = new DetalleViaje();
            try
            {
                DataSet ds = MAT.MVC.Models.ViajeMethod.GetDetalleViaje(new Guid(sViajeID));

                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    oDetalleViaje.Descripcion = item["Descripcion"].ToString();
                    oDetalleViaje.Destino = item["Destino"].ToString();
                    oDetalleViaje.FechaRegreso = item["FechaRegreso"].ToString();
                    oDetalleViaje.FechaSalida = item["FechaSalida"].ToString();
                    oDetalleViaje.HoraRegreso = item["HoraRegreso"].ToString();
                    oDetalleViaje.HoraSalida = item["HoraSalida"].ToString();
                    oDetalleViaje.TiempoConsentracion = Convert.ToInt32(item["TiempoConsentracion"]);
                    oDetalleViaje.NroCoche = item["NroCoche"].ToString();
                    oDetalleViaje.PaqueteExcusionesIncluidas = item["PaqueteExcusionesIncluidas"].ToString();
                    oDetalleViaje.PaqueteExcusionesOpcionales = item["PaqueteExcusionesOpcionales"].ToString();
                    oDetalleViaje.PaqueteServicios = item["PaqueteServicios"].ToString();
                    oDetalleViaje.Observaciones = item["Observaciones"].ToString();
                    
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }

            return PartialView(oDetalleViaje);
        }


    }

    public class PersonaPasajeroComparer : IEqualityComparer<Entities.PersonaPasajero> 
    {
        public bool Equals(Entities.PersonaPasajero x, Entities.PersonaPasajero y)
        {
            if (x == null || y == null) return false;

            return ReferenceEquals(x, y) || (x.PersonaId == y.PersonaId); // In this example, treat the items as equal if they have the same Id
        }

        public int GetHashCode(Entities.PersonaPasajero obj)
        {
            return this.GetHashCode();
        }
    }
}
