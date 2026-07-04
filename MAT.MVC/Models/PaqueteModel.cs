using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAT.Entities;
using MAT.Services;
using System.Text;
using MAT.Enums;
using System.Data.SqlClient;
using System.Data;
using MAT.Utilities;
using MAT.MVC.Infrastructure.Data;

namespace MAT.MVC.Models
{
    public class PaquetePrecio
    {
        public Guid PrecioID { get; set; }
        public double Monto { get; set; }
        public string Descripcion { get; set; }
    }
    public class PaqueteViculosModel
    {
        public string PaqueteID { get; set; }
        public string ID { get; set; }
        // PK de la tabla de vinculo (PaqueteServicioID/PaqueteExcursionID/etc.).
        // Necesario para desvincular excursiones sin una segunda consulta.
        public string VinculoRowId { get; set; }
        public bool? IsOpcional { get; set; }
        public string Tipo { get; set; }
        public string Precio { get; set; }
        public string Descripcion { get; set; }


    }
    // Fila para los grids de vinculacion (modales BS5): item disponible para vincular.
    public class PaqueteVincularItem
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }
    }

    public class PaqueteModel
    {
        public Viaje Viaje { get; set; }
        public Paquete Paquete { get; set; }
        public List<Servicio> Servicios { get; set; }
        public List<Excursion> Excursiones { get; set; }
        public Localidad Destino { get; set; }
        public Transporte Bus { get; set; }
        #region Constructores
        public PaqueteModel()
        {
        }
        public PaqueteModel(Guid viajeID)
        {
            List<PaqueteServicio> listpaqueteservicio = new List<PaqueteServicio>();
            List<PaqueteExcursion> listpaqueteexcursion = new List<PaqueteExcursion>();
            List<Servicio> _servicios = new List<Servicio>();
            List<Excursion> _excursiones = new List<Excursion>();
            try
            {
                Viaje = ViajeDataAccess.GetById(viajeID);
                Paquete = PaqueteDataAccess.GetPaqueteById(Viaje.PaqueteId.Value);
                listpaqueteservicio = PaqueteDataAccess.GetPaqueteServiciosByPaqueteId(Paquete.PaqueteId);
                listpaqueteexcursion = PaqueteDataAccess.GetPaqueteExcursionesByPaqueteId(Paquete.PaqueteId);
                Servicios = new List<Servicio>();
                Destino = GeoDataAccess.GetLocalidadById(Paquete.DestinoId);
                foreach (PaqueteServicio ps in listpaqueteservicio)
                {
                    _servicios.Add(MaestrosDataAccess.GetServicioById(ps.ServicioId.Value));
                }
                foreach (PaqueteExcursion excursion in listpaqueteexcursion)
                {
                    _excursiones.Add(MaestrosDataAccess.GetExcursionById(excursion.ExcursionId));
                }
                Servicios = _servicios;
                Excursiones = _excursiones;
                Servicio servicioTransporte = _servicios.Where(s => s.TransporteId.HasValue).FirstOrDefault();
                if (servicioTransporte !=null) Bus = MaestrosDataAccess.GetTransporteById(servicioTransporte.TransporteId.Value);
            }
            catch (Exception ex)
            {
                StringBuilder excepcion = new StringBuilder();
                excepcion.AppendLine(viajeID.ToString());
                excepcion.AppendLine(listpaqueteservicio.Count.ToString());
                excepcion.AppendLine(ex.Message);
                excepcion.AppendLine(ex.Source);
                excepcion.AppendLine(ex.StackTrace);
                HttpContext.Current.Response.Write(excepcion.ToString());
            }
        }
        #endregion


        #region Metodos Publicos
        public bool GenerarPasajes()
        {
            // NetTiers F4: la transacción vive en el SP (set-based); reemplaza al
            // loop PasajeService.Insert + TransactionManager de DataRepository.
            try
            {
                int generados = PasajeDataAccess.GenerarPasajesByViaje(Viaje.ViajeId, Bus.TransporteId, (int)eEstadoPasaje.Disponible);
                return generados > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

    }

    public class PaqueteStandard
    {
        public string PaqueteID { get; set; }
        public string Descripcion { get; set; }
        public int Moneda { get; set; }
        public int Iva { get; set; }
        public int Alicuota { get; set; }
        public string Temporada { get; set; }
        public double Cotizacion { get; set; }
        public string Codigo { get; set; }
        public int DestinoID { get; set; }
        public string Foto { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime LastUpdate { get; set; }
        public string MonedaDescripcion { get; set; }
        public string MonedaCodigo { get; set; }
        public string Destino { get; set; }

    }

    public class PaqueteDestino
    {
        public string PaqueteID { get; set; }
        public int LocalidadID { get; set; }
        public int DepartamentoID { get; set; }
        public int ProvinciaID { get; set; }
        public string PaisID { get; set; }
        public string Destino { get; set; }
    }

    public class PaqueteVinculos
    {
        public static List<PaqueteStandard> ListPaqueteByYear(string sDateYear = "", string search = null, int? temporada = null, int? moneda = null)
        {
            List<PaqueteStandard> LResult = new List<PaqueteStandard>();

            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@DateYear", SqlDbType.VarChar, 4, sDateYear ?? ""),
                    DBHelper.MakeParam("@Search", SqlDbType.NVarChar, 100, (object)search ?? DBNull.Value),
                    DBHelper.MakeParam("@Temporada", SqlDbType.Int, 0, (object)temporada ?? DBNull.Value),
                    DBHelper.MakeParam("@Moneda", SqlDbType.Int, 0, (object)moneda ?? DBNull.Value)
                };
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Paquete_GetPaquetes", dbParams))
            {
                while (_reader.Read())
                {
                    PaqueteStandard Item = new PaqueteStandard();
                    Item.PaqueteID = _reader["PaqueteID"].ToString();
                    Item.Descripcion = _reader["Descripcion"].ToString();
                    
                    if (_reader["Iva"].ToString() != "")
                    {
                        Item.Iva = Convert.ToInt32(_reader["Iva"]);
                    }
                    
                    if (_reader["Alicuota"].ToString() != "")
	                {
                        Item.Alicuota = Convert.ToInt32(_reader["Alicuota"]);
	                }
                    
                    if (_reader["Temporada"].ToString() != "")
                    {
                        Item.Temporada = _reader["Temporada"].ToString();
                    }
                    if (_reader["Cotizacion"].ToString() != "")
                    {
                        Item.Cotizacion = Convert.ToDouble(_reader["Cotizacion"]);
                    }
                    Item.Codigo = _reader["Codigo"].ToString();
                    if (_reader["DestinoID"].ToString() != "")
                    {
                        Item.DestinoID = Convert.ToInt32(_reader["DestinoID"]);
                    }
                    Item.Foto = _reader["Foto"].ToString();
                    if (_reader["FechaCreacion"].ToString() != "")
                    {
                        Item.FechaCreacion = Convert.ToDateTime(_reader["FechaCreacion"]);
                    }
                    if (_reader["LastUpdate"].ToString() != "")
                    {
                        Item.LastUpdate = Convert.ToDateTime(_reader["LastUpdate"]);
                    }
                    
                    Item.MonedaCodigo = _reader["MonedaCodigo"].ToString();
                    Item.Destino = _reader["Destino"].ToString();
                    LResult.Add(Item);
                }
            }

            return LResult;
        }

        public static PaqueteStandard GetPaqueteByID(string PaqueteID)
        {
            PaqueteStandard Paquete = new PaqueteStandard();

            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@PaqueteID", SqlDbType.VarChar, 0, PaqueteID)
                };
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Reserva_GetPaqueteByPaqueteID", dbParams))
            {
                if (_reader.Read())
                {
                    Paquete.PaqueteID = _reader["PaqueteID"].ToString();
                    Paquete.Descripcion = _reader["Descripcion"].ToString();
                    if (_reader["Moneda"].ToString() != "")
                    {
                        Paquete.Moneda = Convert.ToInt32(_reader["Moneda"]);
                    }
                    if (_reader["Iva"].ToString() != "")
                    {
                        Paquete.Iva = Convert.ToInt32(_reader["Iva"]);    
                    }

                    if (_reader["Alicuota"].ToString() != "")
                    {
                        Paquete.Alicuota = Convert.ToInt32(_reader["Alicuota"]);    
                    }
                    
                    if (_reader["Temporada"].ToString() != "")
                    {
                        Paquete.Temporada = _reader["Temporada"].ToString();
                    }
                    if (_reader["Cotizacion"].ToString() != "")
                    {
                        Paquete.Cotizacion = Convert.ToDouble(_reader["Cotizacion"]);
                    }
                    Paquete.Codigo = _reader["Codigo"].ToString();
                    if (_reader["DestinoID"].ToString() != "")
                    {
                        Paquete.DestinoID = Convert.ToInt32(_reader["DestinoID"]);
                    }
                    Paquete.Foto = _reader["Foto"].ToString();
                    if (_reader["FechaCreacion"].ToString() != "")
                    {
                        Paquete.FechaCreacion = Convert.ToDateTime(_reader["FechaCreacion"]);
                    }
                }
            }

            return Paquete;
        }

        public static string[] PaqueteInsert(PaqueteStandard Paquete)
        {
            string[] sResult = new string[2];

            try
            {
                SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@Descripcion", SqlDbType.VarChar, 0, Paquete.Descripcion),
                        DBHelper.MakeParam("@Moneda", SqlDbType.Int, 0, Paquete.Moneda),
                        DBHelper.MakeParam("@Iva", SqlDbType.VarChar, 0, Convert.ToString(Paquete.Iva)),
                        DBHelper.MakeParam("@Alicuota", SqlDbType.VarChar, 0,Convert.ToString(Paquete.Alicuota)),
                        DBHelper.MakeParam("@Temporada", SqlDbType.Int, 0, Paquete.Temporada),
                        DBHelper.MakeParam("@Cotizacion", SqlDbType.Float, 0, Paquete.Cotizacion),
                        DBHelper.MakeParam("@Codigo", SqlDbType.VarChar, 0, Paquete.Codigo),
                        DBHelper.MakeParam("@DestinoID", SqlDbType.Int, 0, Paquete.DestinoID),
                        DBHelper.MakeParam("@Foto", SqlDbType.VarChar, 0, Paquete.Foto),
                       
                    };
                SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Paquete_NewPaquete", dbParams);


                if (_reader.Read())
                {
                    sResult[0] = _reader["PaqueteID"].ToString();
                    sResult[1] = _reader["Result"].ToString();//Done.

                }

            }
            catch (Exception e)
            {
                sResult[0] = "-1";
                sResult[1] = e.Message;

            }
            return sResult;
        }

        public static string[] PaqueteUpdate(PaqueteStandard Paquete)
        {
            string[] sResult = new string[2];

            try
            {
                SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@PaqueteID", SqlDbType.VarChar, 0, Paquete.PaqueteID),
                        DBHelper.MakeParam("@Descripcion", SqlDbType.VarChar, 0, Paquete.Descripcion),
                        DBHelper.MakeParam("@Moneda", SqlDbType.Int, 0, Paquete.Moneda),
                        DBHelper.MakeParam("@Iva", SqlDbType.VarChar, 0, Convert.ToString(Paquete.Iva)),
                        DBHelper.MakeParam("@Alicuota", SqlDbType.VarChar, 0,Convert.ToString(Paquete.Alicuota)),
                        DBHelper.MakeParam("@Temporada", SqlDbType.Int, 0, Paquete.Temporada),
                        DBHelper.MakeParam("@Cotizacion", SqlDbType.Float, 0, Paquete.Cotizacion),
                        DBHelper.MakeParam("@Codigo", SqlDbType.VarChar, 0, Paquete.Codigo),
                        DBHelper.MakeParam("@DestinoID", SqlDbType.Int, 0, Paquete.DestinoID),
                        DBHelper.MakeParam("@Foto", SqlDbType.VarChar, 0, Paquete.Foto),
                       
                    };
                using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Paquete_Update", dbParams))
                {
                    if (_reader.Read())
                    {
                        sResult[0] = _reader["PaqueteID"].ToString();
                        sResult[1] = _reader["Result"].ToString();//Done.
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

        public static PaqueteDestino GetPaqueteDestino(string PaqueteID)
        {
         PaqueteDestino PaqDestino = new PaqueteDestino();

            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@PaqueteID", SqlDbType.VarChar, 0, PaqueteID)
                };
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_Paquete_GetDestinoByPaqueteID", dbParams))
            {
                if (_reader.Read())
                {
                    PaqDestino.PaqueteID = _reader["PaqueteID"].ToString();
                    if (_reader["LocalidadID"].ToString() != "")
                    {
                        PaqDestino.LocalidadID = Convert.ToInt32(_reader["LocalidadID"].ToString());    
                    }
                    if (_reader["DepartamentoID"].ToString() != "")
                    {
                        PaqDestino.DepartamentoID = Convert.ToInt32(_reader["DepartamentoID"].ToString());    
                    }

                    if (_reader["ProvinciaID"].ToString() != "")
                    {
                        PaqDestino.ProvinciaID = Convert.ToInt32(_reader["ProvinciaID"].ToString());
                    }
                    
                    PaqDestino.PaisID = _reader["PaisID"].ToString();
                    PaqDestino.Destino = _reader["Destino"].ToString();
                }
            }

            return PaqDestino;
        }

        public static List<PaquetePrecio> GetPaquetePrecioByFacturaId(string FacturaId)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@FacturaID", SqlDbType.UniqueIdentifier, 0, new Guid(FacturaId))
                };
            DataSet ds = DBHelper.ExecuteDataSet("usp_MAT_PaquetePrecio_GetAllByFacturaId", dbParams);

            List<PaquetePrecio> lPaquetePrecio = new List<PaquetePrecio>();
            foreach (DataRow dr in ds.Tables[0].Rows)
	        {
		        PaquetePrecio item = new PaquetePrecio();
                item.PrecioID = new Guid(dr["PrecioId"].ToString());
                item.Monto = Convert.ToDouble(dr["Monto"]);
                item.Descripcion = dr["Descripcion"].ToString();
                lPaquetePrecio.Add(item);
	        }

            return lPaquetePrecio;
        }

        public static void DetalleFacturaSetPrecio(int DetalleFacturaId, string PrecioId)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@DetalleFacturaId", SqlDbType.Int, 0, DetalleFacturaId),
                    DBHelper.MakeParam("@PrecioId", SqlDbType.UniqueIdentifier, 0, new Guid(PrecioId))
                };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_DetalleFactura_SetPrecio", dbParams);

        }

    }

    

}