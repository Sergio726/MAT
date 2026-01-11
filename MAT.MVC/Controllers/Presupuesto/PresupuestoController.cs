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
        public JsonResult CreatePresupuesto(string dniCliente, Guid? viajeId, double montoPactado, string observaciones = "")
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

                // TODO: Integrar con WhatsApp si está disponible
                // Por ahora solo retornamos el código

                return Json(new 
                { 
                    success = true, 
                    codigoSeguimiento = codigoSeguimiento,
                    message = $"Presupuesto creado exitosamente. Código: {codigoSeguimiento}"
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
    }
}

