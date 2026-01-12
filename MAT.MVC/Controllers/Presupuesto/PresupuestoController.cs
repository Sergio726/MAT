using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.MVC.Models;
using MAT.MVC.Common;
using MAT.Enums;
using MAT.Utilities;
using System.Data;
using System.Data.SqlClient;

namespace MAT.MVC.Controllers.Presupuesto
{
    [Authorize]
    public class PresupuestoController : Controller
    {
        /// <summary>
        /// Página principal del módulo de presupuestos
        /// </summary>
        public ActionResult Index()
        {
            try
            {
                // Obtener estadísticas de presupuestos
                var estadisticas = PresupuestoMethod.GetEstadisticas();

                ViewBag.Estadisticas = estadisticas;

                return View();
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error en PresupuestoController.Index: {ex.Message}", 1);
                ViewBag.Error = "Error al cargar el panel de presupuestos.";
                return View();
            }
        }

        /// <summary>
        /// Vista para crear un presupuesto rápido (Preventa telefónica)
        /// </summary>
        public ActionResult Create()
        {
            try
            {
                // Cargar viajes disponibles para el dropdown usando stored procedure
                var viajes = new List<SelectListItem>();
                
                try
                {
                    // Usar el método estático de ViajeModel para obtener viajes
                    var viajesList = ViajeMethod.ListViajeByYear(DateTime.Now.Year.ToString());
                    
                    // Filtrar y ordenar viajes disponibles
                    var viajesDisponibles = viajesList
                        .Where(v => 
                        {
                            if (string.IsNullOrEmpty(v.FechaSalida))
                                return false;
                            
                            DateTime fechaSalida;
                            if (DateTime.TryParse(v.FechaSalida, out fechaSalida))
                            {
                                return fechaSalida >= DateTime.Today;
                            }
                            return false;
                        })
                        .OrderBy(v => 
                        {
                            DateTime fechaSalida;
                            if (DateTime.TryParse(v.FechaSalida, out fechaSalida))
                            {
                                return fechaSalida;
                            }
                            return DateTime.MaxValue;
                        })
                        .ToList();

                    ViewBag.Viajes = viajesDisponibles.Select(v => 
                    {
                        string fechaFormateada = v.FechaSalida;
                        DateTime fechaSalida;
                        if (DateTime.TryParse(v.FechaSalida, out fechaSalida))
                        {
                            fechaFormateada = fechaSalida.ToString("dd/MM/yyyy");
                        }
                        
                        return new SelectListItem
                        {
                            Value = v.ViajeID,
                            Text = $"{v.Descripcion} - {fechaFormateada}"
                        };
                    }).ToList();
                }
                catch (Exception ex)
                {
                    MATLogger.Log($"Error al cargar viajes: {ex.Message}", 1);
                    ViewBag.Viajes = new List<SelectListItem>();
                }

                return View();
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error en PresupuestoController.Create: {ex.Message}", 1);
                ViewBag.Error = "Error al cargar la vista de presupuesto.";
                return View();
            }
        }

        /// <summary>
        /// Crea un nuevo presupuesto y genera el código único
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreatePresupuesto(string dniCliente, Guid? viajeId, double montoPactado, string observaciones = "", string telefonoCliente = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dniCliente))
                {
                    return Json(new { success = false, message = "El DNI del cliente es requerido." });
                }

                if (montoPactado <= 0)
                {
                    return Json(new { success = false, message = "El monto debe ser mayor a cero." });
                }

                // Obtener vendedor actual
                var vendedorActual = MATContext.CurrentVendedor;
                if (vendedorActual == null)
                {
                    return Json(new { success = false, message = "No se pudo identificar al vendedor actual." });
                }

                // Crear presupuesto
                PresupuestoStandard presupuesto = new PresupuestoStandard
                {
                    PresupuestoID = Guid.NewGuid(),
                    DniCliente = dniCliente.Trim(),
                    VendedorIdOrigen = vendedorActual.VendedorId,
                    MontoPactado = montoPactado,
                    ViajeId = viajeId,
                    FechaExpiracion = DateTime.Now.AddHours(48), // 48 horas por defecto
                    Observaciones = observaciones ?? string.Empty
                };

                // Generar código y guardar
                string codigoSeguimiento = PresupuestoMethod.CreatePresupuesto(presupuesto);

                // Construir mensaje de WhatsApp
                string mensajeWhatsApp = ConstruirMensajeWhatsApp(codigoSeguimiento, montoPactado, viajeId);
                
                // Intentar enviar WhatsApp si hay teléfono
                bool whatsappEnviado = false;
                string whatsappLink = null;
                string mensajeWhatsAppResultado = "";
                
                if (!string.IsNullOrWhiteSpace(telefonoCliente))
                {
                    try
                    {
                        whatsappEnviado = EnviarWhatsApp(telefonoCliente, mensajeWhatsApp, out whatsappLink);
                        if (whatsappEnviado)
                        {
                            mensajeWhatsAppResultado = " El mensaje de WhatsApp ha sido enviado exitosamente.";
                        }
                        else if (!string.IsNullOrEmpty(whatsappLink))
                        {
                            mensajeWhatsAppResultado = " Se abrirá WhatsApp para enviar el mensaje.";
                        }
                        else
                        {
                            mensajeWhatsAppResultado = " No se pudo generar el enlace de WhatsApp, pero el código está disponible.";
                        }
                    }
                    catch (Exception exWhatsApp)
                    {
                        MATLogger.Log($"Error al enviar WhatsApp: {exWhatsApp.Message}", 1);
                        mensajeWhatsAppResultado = " No se pudo generar el enlace de WhatsApp, pero el código está disponible.";
                    }
                }
                else
                {
                    mensajeWhatsAppResultado = " No se encontró teléfono del cliente. El código está disponible para compartir manualmente.";
                }

                return Json(new 
                { 
                    success = true, 
                    codigoSeguimiento = codigoSeguimiento,
                    whatsappEnviado = whatsappEnviado,
                    whatsappLink = whatsappLink,
                    mensajeWhatsApp = mensajeWhatsApp,
                    message = $"Presupuesto creado exitosamente. Código: {codigoSeguimiento}.{mensajeWhatsAppResultado}"
                });
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error en PresupuestoController.CreatePresupuesto: {ex.Message} - {ex.StackTrace}", 1);
                return Json(new { success = false, message = "Error al crear el presupuesto. Por favor, intente nuevamente." });
            }
        }

        /// <summary>
        /// Busca presupuestos activos por DNI del cliente
        /// </summary>
        [HttpPost]
        public JsonResult GetByDni(string dniCliente)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dniCliente))
                {
                    return Json(new { success = false, message = "El DNI es requerido." });
                }

                var presupuestos = PresupuestoMethod.GetByDni(dniCliente.Trim());

                if (presupuestos == null || presupuestos.Count == 0)
                {
                    return Json(new { success = true, hasPresupuesto = false, presupuestos = new List<object>() });
                }

                var resultado = presupuestos.Select(p => new
                {
                    presupuestoId = p.PresupuestoID.ToString(),
                    codigoSeguimiento = p.CodigoSeguimiento,
                    montoPactado = p.MontoPactado,
                    vendedorOrigenNombre = p.VendedorOrigenNombre,
                    viajeDescripcion = p.ViajeDescripcion,
                    paqueteDescripcion = p.PaqueteDescripcion,
                    fechaCreacion = p.FechaCreacion.ToString("dd/MM/yyyy HH:mm"),
                    fechaExpiracion = p.FechaExpiracion.ToString("dd/MM/yyyy HH:mm"),
                    isExpirado = p.IsExpirado
                }).ToList();

                return Json(new { success = true, hasPresupuesto = true, presupuestos = resultado });
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error en PresupuestoController.GetByDni: {ex.Message} - {ex.StackTrace}", 1);
                return Json(new { success = false, message = "Error al buscar presupuestos." });
            }
        }

        /// <summary>
        /// Busca un presupuesto por código de seguimiento
        /// </summary>
        [HttpPost]
        public JsonResult GetByCodigo(string codigoSeguimiento)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(codigoSeguimiento))
                {
                    return Json(new { success = false, message = "El código de seguimiento es requerido." });
                }

                var presupuesto = PresupuestoMethod.GetByCodigo(codigoSeguimiento.Trim().ToUpper());

                if (presupuesto == null)
                {
                    return Json(new { success = true, hasPresupuesto = false });
                }

                var resultado = new
                {
                    presupuestoId = presupuesto.PresupuestoID.ToString(),
                    codigoSeguimiento = presupuesto.CodigoSeguimiento,
                    dniCliente = presupuesto.DniCliente,
                    montoPactado = presupuesto.MontoPactado,
                    vendedorOrigenNombre = presupuesto.VendedorOrigenNombre,
                    vendedorIdOrigen = presupuesto.VendedorIdOrigen.ToString(),
                    viajeId = presupuesto.ViajeId?.ToString(),
                    viajeDescripcion = presupuesto.ViajeDescripcion,
                    paqueteDescripcion = presupuesto.PaqueteDescripcion,
                    estado = presupuesto.Estado.ToString(),
                    fechaCreacion = presupuesto.FechaCreacion.ToString("dd/MM/yyyy HH:mm"),
                    fechaExpiracion = presupuesto.FechaExpiracion.ToString("dd/MM/yyyy HH:mm"),
                    isExpirado = presupuesto.IsExpirado,
                    observaciones = presupuesto.Observaciones
                };

                return Json(new { success = true, hasPresupuesto = true, presupuesto = resultado });
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error en PresupuestoController.GetByCodigo: {ex.Message} - {ex.StackTrace}", 1);
                return Json(new { success = false, message = "Error al buscar el presupuesto." });
            }
        }

        /// <summary>
        /// Obtiene el precio base y adicionales de un viaje
        /// </summary>
        [HttpPost]
        public JsonResult GetViajeInfo(string viajeId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(viajeId))
                {
                    return Json(new { success = false, message = "El ID del viaje es requerido." });
                }

                // Obtener información del viaje usando ViajeModel
                var viaje = ViajeMethod.ViajeByViajeID(viajeId);

                if (viaje == null || string.IsNullOrEmpty(viaje.ViajeID))
                {
                    return Json(new { success = false, message = "Viaje no encontrado." });
                }

                // Obtener precio base del viaje (usar PrecioSemicama como base, o PrecioPromocional si está disponible)
                decimal precioBase = 0;
                if (viaje.PrecioSemicama > 0)
                {
                    precioBase = (decimal)viaje.PrecioSemicama;
                }
                else if (viaje.PrecioPromocional > 0)
                {
                    precioBase = (decimal)viaje.PrecioPromocional;
                }
                else if (viaje.PrecioCama > 0)
                {
                    precioBase = (decimal)viaje.PrecioCama;
                }

                // Obtener adicionales del viaje a través del paquete
                var adicionales = new List<object>();
                try
                {
                    if (!string.IsNullOrEmpty(viaje.PaqueteID))
                    {
                        Guid paqueteId = new Guid(viaje.PaqueteID);
                        
                        // Obtener PaqueteAdicional por PaqueteId
                        SqlParameter[] dbParams = new SqlParameter[]
                        {
                            DBHelper.MakeParam("@PaqueteId", SqlDbType.UniqueIdentifier, 0, paqueteId)
                        };

                        using (SqlDataReader reader = DBHelper.ExecuteDataReader("dbo._ProcedurePaqueteAdicional_GetByPaqueteId", dbParams))
                        {
                            var adicionalIds = new List<Guid>();
                            while (reader.Read())
                            {
                                if (reader["AdicionalID"] != DBNull.Value)
                                {
                                    adicionalIds.Add(new Guid(reader["AdicionalID"].ToString()));
                                }
                            }

                            // Obtener detalles de cada adicional
                            foreach (var adicionalId in adicionalIds)
                            {
                                try
                                {
                                    SqlParameter[] adicionalParams = new SqlParameter[]
                                    {
                                        DBHelper.MakeParam("@AdicionalId", SqlDbType.UniqueIdentifier, 0, adicionalId)
                                    };

                                    using (SqlDataReader adicionalReader = DBHelper.ExecuteDataReader("dbo._ProcedureAdicional_GetByAdicionalId", adicionalParams))
                                    {
                                        if (adicionalReader.Read())
                                        {
                                            decimal precio = 0;
                                            if (adicionalReader["Monto"] != DBNull.Value)
                                            {
                                                decimal.TryParse(adicionalReader["Monto"].ToString(), out precio);
                                            }

                                            // Por defecto, todos los adicionales son opcionales
                                            bool esOpcional = true;

                                            adicionales.Add(new
                                            {
                                                adicionalId = adicionalId.ToString(),
                                                descripcion = adicionalReader["Descripcion"]?.ToString() ?? "Sin descripción",
                                                precio = precio,
                                                esOpcional = esOpcional
                                            });
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MATLogger.Log($"Error al obtener adicional {adicionalId}: {ex.Message}", 1);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MATLogger.Log($"Error al obtener adicionales: {ex.Message}", 1);
                }

                return Json(new
                {
                    success = true,
                    precioBase = precioBase,
                    adicionales = adicionales,
                    descripcion = viaje.Descripcion ?? ""
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error en PresupuestoController.GetViajeInfo: {ex.Message} - {ex.StackTrace}", 1);
                return Json(new { success = false, message = "Error al obtener información del viaje." });
            }
        }

        /// <summary>
        /// Actualiza el estado de un presupuesto cuando se cierra una venta
        /// </summary>
        [HttpPost]
        public JsonResult UpdateEstado(Guid presupuestoId, int estado, Guid? facturaId = null, Guid? vendedorIdCierre = null)
        {
            try
            {
                PresupuestoMethod.UpdateEstado(
                    presupuestoId, 
                    (eEstadoPresupuesto)estado, 
                    facturaId, 
                    vendedorIdCierre
                );

                return Json(new { success = true, message = "Estado actualizado correctamente." });
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error en PresupuestoController.UpdateEstado: {ex.Message} - {ex.StackTrace}", 1);
                return Json(new { success = false, message = "Error al actualizar el estado del presupuesto." });
            }
        }

        /// <summary>
        /// Construye el mensaje de WhatsApp con el código de seguimiento
        /// </summary>
        private string ConstruirMensajeWhatsApp(string codigoSeguimiento, double montoPactado, Guid? viajeId)
        {
            var vendedorActual = MATContext.CurrentVendedor;
            string nombreVendedor = vendedorActual?.Descripcion ?? "Marco Antonio Tours";
            
            string mensaje = $"¡Hola! 👋\n\n";
            mensaje += $"Te acabo de generar un *Código de Reserva Preferencial*:\n";
            mensaje += $"*{codigoSeguimiento}*\n\n";
            mensaje += $"💰 Monto acordado: ${montoPactado:N2}\n\n";
            mensaje += $"📋 *Instrucciones:*\n";
            mensaje += $"Cuando vengas a la agencia, presenta este código y quien esté en caja te atenderá directamente con tu ficha lista.\n\n";
            mensaje += $"⏰ Este código es válido por 48 horas.\n\n";
            mensaje += $"Saludos,\n{nombreVendedor}\nMarco Antonio Tours";

            return mensaje;
        }

        /// <summary>
        /// Envía un mensaje de WhatsApp al cliente o genera un enlace directo
        /// </summary>
        private bool EnviarWhatsApp(string telefono, string mensaje, out string whatsappLink)
        {
            whatsappLink = null;
            
            try
            {
                // Limpiar teléfono (remover caracteres especiales)
                string telefonoLimpio = telefono.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "").Replace("+", "");
                
                // Verificar si hay una API de WhatsApp configurada
                string whatsappApiUrl = System.Configuration.ConfigurationManager.AppSettings["WhatsApp_API_URL"]?.ToString();
                string enableDirectLink = System.Configuration.ConfigurationManager.AppSettings["WhatsApp_EnableDirectLink"]?.ToString() ?? "true";
                
                // Si hay API configurada, intentar usar la API
                if (!string.IsNullOrEmpty(whatsappApiUrl))
                {
                    // Aquí iría la implementación real de la API de WhatsApp
                    // Ejemplo con HttpClient:
                    /*
                    using (var client = new System.Net.Http.HttpClient())
                    {
                        var payload = new
                        {
                            phone = telefonoLimpio,
                            message = mensaje
                        };
                        
                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                        var content = new System.Net.Http.StringContent(json, System.Text.Encoding.UTF8, "application/json");
                        
                        var response = await client.PostAsync(whatsappApiUrl, content);
                        return response.IsSuccessStatusCode;
                    }
                    */
                    
                    // Por ahora retornamos false para indicar que no se envió por API
                    // y generamos el enlace directo como fallback
                }
                
                // Si no hay API o está habilitado el enlace directo, generar enlace de WhatsApp
                if (enableDirectLink.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    // Formatear teléfono para WhatsApp (debe incluir código de país sin +)
                    // Si no tiene código de país, asumimos Argentina (54)
                    if (!telefonoLimpio.StartsWith("54") && telefonoLimpio.Length <= 10)
                    {
                        telefonoLimpio = "54" + telefonoLimpio;
                    }
                    
                    // Codificar el mensaje para URL
                    string mensajeCodificado = System.Web.HttpUtility.UrlEncode(mensaje);
                    
                    // Generar enlace de WhatsApp
                    whatsappLink = $"https://wa.me/{telefonoLimpio}?text={mensajeCodificado}";
                    
                    MATLogger.Log($"Enlace de WhatsApp generado: {whatsappLink}", 2);
                    return false; // Retornamos false porque no se envió automáticamente, pero tenemos el enlace
                }
                
                return false;
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error en EnviarWhatsApp: {ex.Message}", 1);
                return false;
            }
        }
    }
}

