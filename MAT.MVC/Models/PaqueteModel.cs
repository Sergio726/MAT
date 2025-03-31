using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAT.Entities;
using MAT.Services;
using System.Text;
using MAT.Enums;
using MAT.Data;
using System.Data.SqlClient;
using System.Data;
using MAT.Utilities;

namespace MAT.MVC.Models
{
    public class PaquetePrecio
    {
        public Guid PrecioID { get; set; }
        public double Monto { get; set; }
        public string Descripcion { get; set; }
    }
    public class PaqueteModel
    {
        ViajeService vServ;
        PaqueteService pServ;
        LocalidadService lServ;
        PaqueteServicioService psServ;
        ServicioService sServ;
        TransporteService tServ;
        ButacaService bServ;
        PasajeService pasajeServ;
        ExcursionService excursionServ;
        PaqueteExcursionService paqueteExcursionServ;
        public Viaje Viaje { get; set; }
        public Paquete Paquete { get; set; }
        public List<Servicio> Servicios { get; set; }
        public List<Excursion> Excursiones { get; set; }
        public Localidad Destino { get; set; }
        public Transporte Bus { get; set; }
        #region Constructores
        public PaqueteModel()
        {
            vServ = new ViajeService();
            pServ = new PaqueteService();
            paqueteExcursionServ = new PaqueteExcursionService();
            lServ = new LocalidadService();
            psServ = new PaqueteServicioService();
            sServ = new ServicioService();
            tServ = new TransporteService();
            bServ = new ButacaService();
            pasajeServ = new PasajeService();
            excursionServ = new ExcursionService();
        }
        public PaqueteModel(Guid viajeID)
        {
            List<PaqueteServicio> listpaqueteservicio = new List<PaqueteServicio>();
            List<PaqueteExcursion> listpaqueteexcursion = new List<PaqueteExcursion>();
            List<Servicio> _servicios = new List<Servicio>();
            List<Excursion> _excursiones = new List<Excursion>();
            try
            {
                vServ = new ViajeService();
                pServ = new PaqueteService();
                lServ = new LocalidadService();
                psServ = new PaqueteServicioService();
                sServ = new ServicioService();
                tServ = new TransporteService();
                bServ = new ButacaService();
                pasajeServ = new PasajeService();
                paqueteExcursionServ = new PaqueteExcursionService();
                excursionServ = new ExcursionService();
                Viaje = vServ.GetByViajeId(viajeID);
                Paquete = pServ.GetByPaqueteId(Viaje.PaqueteId.Value);
                listpaqueteservicio = psServ.GetByPaqueteId(Paquete.PaqueteId).ToList();
                listpaqueteexcursion = paqueteExcursionServ.GetByPaqueteId(Paquete.PaqueteId).ToList();
                Servicios = new List<Servicio>();
                Destino = lServ.GetById(Paquete.DestinoId);
                foreach (PaqueteServicio ps in listpaqueteservicio)
                {
                    _servicios.Add(sServ.GetByServicioId(ps.ServicioId.Value));
                }
                foreach (PaqueteExcursion excursion in listpaqueteexcursion)
                {
                    _excursiones.Add(excursionServ.GetByExcursionId(excursion.ExcursionId));
                }
                Servicios = _servicios;
                Excursiones = _excursiones;
                Servicio servicioTransporte = _servicios.Where(s => s.TransporteId.HasValue).FirstOrDefault();
                if (servicioTransporte !=null) Bus = tServ.GetByTransporteId(servicioTransporte.TransporteId.Value);
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
            TransactionManager transaction = DataRepository.Provider.CreateTransaction();
            transaction.BeginTransaction();
            bool success = false;
            try
            {
                List<Butaca> butacas = bServ.GetByTransporteId(Bus.TransporteId).ToList();
                foreach (Butaca butaca in butacas)
                {
                    Pasaje pasaje = new Pasaje()
                    {
                        PasajeId = Guid.NewGuid(),
                        ButacaId = butaca.ButacaId,
                        ViajeId = Viaje.ViajeId,
                        EstadoPasaje = (int)eEstadoPasaje.Disponible
                    };
                    pasajeServ.Insert(pasaje);
                    success = true;
                }
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                success = false;
            }
            finally
            {
                if (success)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }
            }
            return success;
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

    public class PaqueteMethod
    {
        public static List<PaqueteStandard> ListPaqueteByYear(string sDateYear = "")
        {
            List<PaqueteStandard> LResult = new List<PaqueteStandard>();

            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@DateYear", SqlDbType.VarChar, 0, sDateYear)
                };
            SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Paquete_GetPaquetes", dbParams);


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
                    LResult.Add(Item);
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
            SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Reserva_GetPaqueteByPaqueteID", dbParams);


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
                SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Paquete_Update", dbParams);


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

        public static PaqueteDestino GetPaqueteDestino(string PaqueteID)
        {
         PaqueteDestino PaqDestino = new PaqueteDestino();

            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@PaqueteID", SqlDbType.VarChar, 0, PaqueteID)
                };
            SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_Paquete_GetDestinoByPaqueteID", dbParams);


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