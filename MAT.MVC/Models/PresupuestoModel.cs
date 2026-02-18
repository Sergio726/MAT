using MAT.Utilities;
using MAT.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;

namespace MAT.MVC.Models
{
    public class PresupuestoStandard
    {
        public Guid PresupuestoID { get; set; }
        public string DniCliente { get; set; }
        public string NombreCliente { get; set; }
        public string TelefonoCliente { get; set; }
        public string EmailCliente { get; set; }
        public Guid VendedorIdOrigen { get; set; }
        public string VendedorOrigenNombre { get; set; }
        public string CodigoSeguimiento { get; set; }
        public double MontoPactado { get; set; }
        public Guid? ViajeId { get; set; }
        public string ViajeDescripcion { get; set; }
        public string PaqueteDescripcion { get; set; }
        public eEstadoPresupuesto Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public Guid? FacturaId { get; set; }
        public Guid? VendedorIdCierre { get; set; }
        public string VendedorCierreNombre { get; set; }
        public string Observaciones { get; set; }
        public bool IsExpirado { get; set; }
    }

    public class PresupuestoMethod
    {
        /// <summary>
        /// Crea un nuevo presupuesto y genera el código único
        /// </summary>
        public static string CreatePresupuesto(PresupuestoStandard presupuesto)
        {
            SqlParameter[] dbParams = new SqlParameter[]
            {
                DBHelper.MakeParam("@PresupuestoId", SqlDbType.UniqueIdentifier, 0, presupuesto.PresupuestoID),
                DBHelper.MakeParam("@DniCliente", SqlDbType.VarChar, 50, string.IsNullOrWhiteSpace(presupuesto.DniCliente) ? (object)DBNull.Value : presupuesto.DniCliente),
                DBHelper.MakeParam("@NombreCliente", SqlDbType.VarChar, 200, string.IsNullOrWhiteSpace(presupuesto.NombreCliente) ? (object)DBNull.Value : presupuesto.NombreCliente),
                DBHelper.MakeParam("@TelefonoCliente", SqlDbType.VarChar, 50, string.IsNullOrWhiteSpace(presupuesto.TelefonoCliente) ? (object)DBNull.Value : presupuesto.TelefonoCliente),
                DBHelper.MakeParam("@EmailCliente", SqlDbType.VarChar, 100, string.IsNullOrWhiteSpace(presupuesto.EmailCliente) ? (object)DBNull.Value : presupuesto.EmailCliente),
                DBHelper.MakeParam("@VendedorIdOrigen", SqlDbType.UniqueIdentifier, 0, presupuesto.VendedorIdOrigen),
                DBHelper.MakeParam("@MontoPactado", SqlDbType.Float, 0, presupuesto.MontoPactado),
                DBHelper.MakeParam("@ViajeId", SqlDbType.UniqueIdentifier, 0, presupuesto.ViajeId.HasValue ? presupuesto.ViajeId.Value : (object)DBNull.Value),
                DBHelper.MakeParam("@FechaExpiracion", SqlDbType.DateTime, 0, presupuesto.FechaExpiracion),
                DBHelper.MakeParam("@Observaciones", SqlDbType.VarChar, 500, presupuesto.Observaciones ?? string.Empty)
            };

            // ExecuteNonQueryOutputString agrega automáticamente el parámetro OUTPUT
            return DBHelper.ExecuteNonQueryOutputString("dbo.usp_MAT_Presupuesto_Insert", dbParams, "@CodigoSeguimiento", SqlDbType.VarChar, 50);
        }

        /// <summary>
        /// Obtiene presupuestos activos por DNI del cliente
        /// </summary>
        public static List<PresupuestoStandard> GetByDni(string dniCliente)
        {
            List<PresupuestoStandard> lista = new List<PresupuestoStandard>();

            SqlParameter[] dbParams = new SqlParameter[]
            {
                DBHelper.MakeParam("@DniCliente", SqlDbType.VarChar, 50, dniCliente)
            };

            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Presupuesto_GetByDni", dbParams))
            {
                while (_reader.Read())
                {
                    PresupuestoStandard presupuesto = new PresupuestoStandard();
                    presupuesto.PresupuestoID = new Guid(_reader["PresupuestoID"].ToString());
                    presupuesto.DniCliente = _reader["DniCliente"]?.ToString();
                    presupuesto.NombreCliente = _reader["NombreCliente"]?.ToString();
                    presupuesto.TelefonoCliente = _reader["TelefonoCliente"]?.ToString();
                    presupuesto.EmailCliente = _reader["EmailCliente"]?.ToString();
                    presupuesto.VendedorIdOrigen = new Guid(_reader["VendedorIdOrigen"].ToString());
                    presupuesto.VendedorOrigenNombre = _reader["VendedorOrigenNombre"]?.ToString() ?? string.Empty;
                    presupuesto.CodigoSeguimiento = _reader["CodigoSeguimiento"].ToString();
                    presupuesto.MontoPactado = Convert.ToDouble(_reader["MontoPactado"]);
                    
                    if (_reader["ViajeId"] != DBNull.Value)
                        presupuesto.ViajeId = new Guid(_reader["ViajeId"].ToString());
                    
                    presupuesto.ViajeDescripcion = _reader["ViajeDescripcion"]?.ToString() ?? string.Empty;
                    presupuesto.PaqueteDescripcion = _reader["PaqueteDescripcion"]?.ToString() ?? string.Empty;
                    presupuesto.Estado = (eEstadoPresupuesto)Convert.ToInt32(_reader["Estado"]);
                    presupuesto.FechaCreacion = Convert.ToDateTime(_reader["FechaCreacion"]);
                    presupuesto.FechaExpiracion = Convert.ToDateTime(_reader["FechaExpiracion"]);
                    
                    if (_reader["FacturaId"] != DBNull.Value)
                        presupuesto.FacturaId = new Guid(_reader["FacturaId"].ToString());
                    
                    if (_reader["VendedorIdCierre"] != DBNull.Value)
                        presupuesto.VendedorIdCierre = new Guid(_reader["VendedorIdCierre"].ToString());
                    
                    presupuesto.Observaciones = _reader["Observaciones"]?.ToString() ?? string.Empty;
                    presupuesto.IsExpirado = Convert.ToBoolean(_reader["IsExpirado"]);

                    lista.Add(presupuesto);
                }
            }

            return lista;
        }

        /// <summary>
        /// Obtiene un presupuesto por código de seguimiento
        /// </summary>
        public static PresupuestoStandard GetByCodigo(string codigoSeguimiento)
        {
            PresupuestoStandard presupuesto = null;

            SqlParameter[] dbParams = new SqlParameter[]
            {
                DBHelper.MakeParam("@CodigoSeguimiento", SqlDbType.VarChar, 50, codigoSeguimiento)
            };

            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Presupuesto_GetByCodigo", dbParams))
            {
                if (_reader.Read())
                {
                    presupuesto = new PresupuestoStandard();
                    presupuesto.PresupuestoID = new Guid(_reader["PresupuestoID"].ToString());
                    presupuesto.DniCliente = _reader["DniCliente"]?.ToString();
                    presupuesto.NombreCliente = _reader["NombreCliente"]?.ToString();
                    presupuesto.TelefonoCliente = _reader["TelefonoCliente"]?.ToString();
                    presupuesto.EmailCliente = _reader["EmailCliente"]?.ToString();
                    presupuesto.VendedorIdOrigen = new Guid(_reader["VendedorIdOrigen"].ToString());
                    presupuesto.VendedorOrigenNombre = _reader["VendedorOrigenNombre"]?.ToString() ?? string.Empty;
                    presupuesto.CodigoSeguimiento = _reader["CodigoSeguimiento"].ToString();
                    presupuesto.MontoPactado = Convert.ToDouble(_reader["MontoPactado"]);
                    
                    if (_reader["ViajeId"] != DBNull.Value)
                        presupuesto.ViajeId = new Guid(_reader["ViajeId"].ToString());
                    
                    presupuesto.ViajeDescripcion = _reader["ViajeDescripcion"]?.ToString() ?? string.Empty;
                    presupuesto.PaqueteDescripcion = _reader["PaqueteDescripcion"]?.ToString() ?? string.Empty;
                    presupuesto.Estado = (eEstadoPresupuesto)Convert.ToInt32(_reader["Estado"]);
                    presupuesto.FechaCreacion = Convert.ToDateTime(_reader["FechaCreacion"]);
                    presupuesto.FechaExpiracion = Convert.ToDateTime(_reader["FechaExpiracion"]);
                    
                    if (_reader["FacturaId"] != DBNull.Value)
                        presupuesto.FacturaId = new Guid(_reader["FacturaId"].ToString());
                    
                    if (_reader["VendedorIdCierre"] != DBNull.Value)
                        presupuesto.VendedorIdCierre = new Guid(_reader["VendedorIdCierre"].ToString());
                    
                    presupuesto.VendedorCierreNombre = _reader["VendedorCierreNombre"]?.ToString() ?? string.Empty;
                    presupuesto.Observaciones = _reader["Observaciones"]?.ToString() ?? string.Empty;
                    presupuesto.IsExpirado = Convert.ToBoolean(_reader["IsExpirado"]);
                }
            }

            return presupuesto;
        }

        /// <summary>
        /// Obtiene un presupuesto por ID
        /// </summary>
        public static PresupuestoStandard GetById(Guid presupuestoId)
        {
            var lista = GetAll(null, null, null, null, null, null);
            return lista.FirstOrDefault(p => p.PresupuestoID == presupuestoId);
        }

        /// <summary>
        /// Actualiza el estado de un presupuesto y lo vincula con una factura
        /// </summary>
        public static void UpdateEstado(Guid presupuestoId, eEstadoPresupuesto estado, Guid? facturaId = null, Guid? vendedorIdCierre = null)
        {
            SqlParameter[] dbParams = new SqlParameter[]
            {
                DBHelper.MakeParam("@PresupuestoId", SqlDbType.UniqueIdentifier, 0, presupuestoId),
                DBHelper.MakeParam("@Estado", SqlDbType.Int, 0, (int)estado),
                DBHelper.MakeParam("@FacturaId", SqlDbType.UniqueIdentifier, 0, facturaId.HasValue ? facturaId.Value : (object)DBNull.Value),
                DBHelper.MakeParam("@VendedorIdCierre", SqlDbType.UniqueIdentifier, 0, vendedorIdCierre.HasValue ? vendedorIdCierre.Value : (object)DBNull.Value)
            };

            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Presupuesto_UpdateEstado", dbParams);
        }

        /// <summary>
        /// Extiende la fecha de expiración de un presupuesto pendiente
        /// </summary>
        public static bool ExtenderExpiracion(Guid presupuestoId, int horasAdicionales = 24)
        {
            SqlParameter[] dbParams = new SqlParameter[]
            {
                DBHelper.MakeParam("@PresupuestoId", SqlDbType.UniqueIdentifier, 0, presupuestoId),
                DBHelper.MakeParam("@HorasAdicionales", SqlDbType.Int, 0, horasAdicionales > 0 ? horasAdicionales : 24)
            };

            using (SqlDataReader reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Presupuesto_ExtenderExpiracion", dbParams))
            {
                if (reader.Read() && reader["RowsAffected"] != DBNull.Value)
                {
                    return Convert.ToInt32(reader["RowsAffected"]) > 0;
                }
            }
            return false;
        }

        /// <summary>
        /// Marca presupuestos expirados como tal
        /// </summary>
        public static int MarcarExpirados()
        {
            SqlParameter[] dbParams = new SqlParameter[] { };
            
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Presupuesto_Expirados", dbParams))
            {
                if (_reader.Read())
                {
                    return Convert.ToInt32(_reader["PresupuestosExpirados"]);
                }
            }

            return 0;
        }

        /// <summary>
        /// Obtiene estadísticas de presupuestos
        /// </summary>
        public static Dictionary<string, int> GetEstadisticas()
        {
            var estadisticas = new Dictionary<string, int>
            {
                { "Pendientes", 0 },
                { "Cerrados", 0 },
                { "Expirados", 0 },
                { "Total", 0 }
            };

            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MAT.Data.ConnectionString"]?.ConnectionString;
                if (string.IsNullOrEmpty(connectionString))
                {
                    MATLogger.Log("No se encontró la cadena de conexión para obtener estadísticas", 1);
                    return estadisticas;
                }

                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();

                    // Pendientes
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) as Total FROM [dbo].[Presupuesto] WHERE [Estado] = 1", cn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            estadisticas["Pendientes"] = Convert.ToInt32(result);
                        }
                    }

                    // Cerrados
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) as Total FROM [dbo].[Presupuesto] WHERE [Estado] = 3", cn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            estadisticas["Cerrados"] = Convert.ToInt32(result);
                        }
                    }

                    // Expirados
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) as Total FROM [dbo].[Presupuesto] WHERE [Estado] = 2", cn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            estadisticas["Expirados"] = Convert.ToInt32(result);
                        }
                    }

                    // Total
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) as Total FROM [dbo].[Presupuesto]", cn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            estadisticas["Total"] = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error al obtener estadísticas de presupuestos: {ex.Message}", 1);
            }

            return estadisticas;
        }

        /// <summary>
        /// Obtiene todos los presupuestos con filtros opcionales
        /// </summary>
        public static List<PresupuestoStandard> GetAll(int? estado = null, Guid? vendedorIdOrigen = null, string dniCliente = null, string codigoSeguimiento = null, DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            List<PresupuestoStandard> lista = new List<PresupuestoStandard>();

            SqlParameter[] dbParams = new SqlParameter[]
            {
                DBHelper.MakeParam("@Estado", SqlDbType.Int, 0, estado.HasValue ? (object)estado.Value : DBNull.Value),
                DBHelper.MakeParam("@VendedorIdOrigen", SqlDbType.UniqueIdentifier, 0, vendedorIdOrigen.HasValue ? (object)vendedorIdOrigen.Value : DBNull.Value),
                DBHelper.MakeParam("@DniCliente", SqlDbType.VarChar, 50, string.IsNullOrWhiteSpace(dniCliente) ? (object)DBNull.Value : dniCliente),
                DBHelper.MakeParam("@CodigoSeguimiento", SqlDbType.VarChar, 50, string.IsNullOrWhiteSpace(codigoSeguimiento) ? (object)DBNull.Value : codigoSeguimiento),
                DBHelper.MakeParam("@FechaDesde", SqlDbType.DateTime, 0, fechaDesde.HasValue ? (object)fechaDesde.Value : DBNull.Value),
                DBHelper.MakeParam("@FechaHasta", SqlDbType.DateTime, 0, fechaHasta.HasValue ? (object)fechaHasta.Value : DBNull.Value)
            };

            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Presupuesto_GetAll", dbParams))
            {
                while (_reader.Read())
                {
                    PresupuestoStandard presupuesto = new PresupuestoStandard();
                    presupuesto.PresupuestoID = new Guid(_reader["PresupuestoID"].ToString());
                    presupuesto.DniCliente = _reader["DniCliente"]?.ToString();
                    presupuesto.NombreCliente = _reader["NombreCliente"]?.ToString();
                    presupuesto.TelefonoCliente = _reader["TelefonoCliente"]?.ToString();
                    presupuesto.EmailCliente = _reader["EmailCliente"]?.ToString();
                    presupuesto.VendedorIdOrigen = new Guid(_reader["VendedorIdOrigen"].ToString());
                    presupuesto.VendedorOrigenNombre = _reader["VendedorOrigenNombre"]?.ToString() ?? string.Empty;
                    presupuesto.CodigoSeguimiento = _reader["CodigoSeguimiento"].ToString();
                    presupuesto.MontoPactado = Convert.ToDouble(_reader["MontoPactado"]);
                    
                    if (_reader["ViajeId"] != DBNull.Value)
                        presupuesto.ViajeId = new Guid(_reader["ViajeId"].ToString());
                    
                    presupuesto.ViajeDescripcion = _reader["ViajeDescripcion"]?.ToString() ?? string.Empty;
                    presupuesto.PaqueteDescripcion = _reader["PaqueteDescripcion"]?.ToString() ?? string.Empty;
                    presupuesto.Estado = (eEstadoPresupuesto)Convert.ToInt32(_reader["Estado"]);
                    presupuesto.FechaCreacion = Convert.ToDateTime(_reader["FechaCreacion"]);
                    presupuesto.FechaExpiracion = Convert.ToDateTime(_reader["FechaExpiracion"]);
                    
                    if (_reader["FacturaId"] != DBNull.Value)
                        presupuesto.FacturaId = new Guid(_reader["FacturaId"].ToString());
                    
                    if (_reader["VendedorIdCierre"] != DBNull.Value)
                        presupuesto.VendedorIdCierre = new Guid(_reader["VendedorIdCierre"].ToString());
                    
                    presupuesto.VendedorCierreNombre = _reader["VendedorCierreNombre"]?.ToString() ?? string.Empty;
                    presupuesto.Observaciones = _reader["Observaciones"]?.ToString() ?? string.Empty;
                    presupuesto.IsExpirado = Convert.ToBoolean(_reader["IsExpirado"]);

                    lista.Add(presupuesto);
                }
            }

            return lista;
        }
    }
}

