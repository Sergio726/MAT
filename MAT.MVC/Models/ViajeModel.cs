using MAT.MVC.Common;
using MAT.MVC.Infrastructure;
using MAT.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MAT.MVC.Models
{
    public class DDViaje
    {
        public string ViajeID { get; set; }
        public string Descripcion { get; set; }
    }
    public class ViajeModel
    {
        public string ViajeID { get; set; }
        public string PaqueteID { get; set; }
        public string Origen { get; set; }
        public string FechaSalida { get; set; }
        public string HoraSalida { get; set; }
        public string PaisOrigen { get; set; }
        public string PaisDestino { get; set; }
        public string Paso { get; set; }
        public string Medio { get; set; }
        public string BusID { get; set; }
        public string FechaRegreso { get; set; }
        public string HoraRegreso { get; set; }
        public string Descripcion { get; set; }
        public double PrecioSemicama { get; set; }
        public double PrecioCama { get; set; }
        public double PrecioPromocional { get; set; }
        public string FechaPromocion { get; set; }
        public int nDias { get; set; }
        public int nNoches { get; set; }
        public string PaqueteNombre { get; set; }
        public int TiempoConsentracion { get; set; }
        public string Observaciones { get; set; }
        public string DeleteOn { get; set; }
        public int MonedaTipo { get; set; }
        public Boolean IsPublicWeb { get; set; }

    }

    public class ViajePorVencerDto
    {
        public Guid ViajeID { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaSalida { get; set; }
        public int Disponibles { get; set; }
        public int? DaysToDeparture { get; set; }
    }
    
    public class ViajeHotel {
        public string ViajeHotelID { get; set; }
        public string ViajeID { get; set; }
        public string HotelID { get; set; }
        public string Desde { get; set; }
        public string Hasta { get; set; }
        public string HoraIngreso { get; set; }
        public string HoraSalida { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string ViajeNombre { get; set; }
        public string Comentario { get; set; }

    }

    public class DetalleViaje {
        public string ViajeID { get; set; }
        public string Descripcion { get; set; }
        public string Destino { get; set; }
        public string FechaSalida { get; set; }
        public string FechaRegreso { get; set; }
        public string HoraSalida { get; set; }
        public string HoraRegreso { get; set; }
        public int TiempoConsentracion { get; set; }
        public string NroCoche { get; set; }
        /// <summary>Patente o dominio del coche (transporte).</summary>
        public string TransportePatente { get; set; }
        public string PaqueteServicios { get; set; }
        public string PaqueteExcusionesIncluidas { get; set; }
        public string PaqueteExcusionesOpcionales { get; set; }
        public string Observaciones { get; set; }
        public List<ItinerarioViajeModel> Itinerario { get; set; }
    }

    public class PasajeroViaje {
        public string PasajeroID { get; set; }
        public string ViajeID { get; set; }
        public string PasajeroNombre { get; set; }
        public string PasajeroApellido { get; set; }
        public string PasajeroDocumento { get; set; }
        public string PasajeroTipoDocumento { get; set; }
        public string PasajeroFechaNacimiento { get; set; }
        public string PasajeroTelefono { get; set; }
        public string PasajeroEmail { get; set; }
        public string PasajeroSexo { get; set; }
    }
   
    public class ViajeMethod { 

        public static string[] InsertViaje(ViajeModel Viaje)
        {
            string[] sResult = new string[2];
            
            try
            {
                if (Viaje.PaqueteID != "" && Viaje.PaqueteID != null)
                {
                    SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@PaqueteID", SqlDbType.VarChar, 0, Viaje.PaqueteID),
                        DBHelper.MakeParam("@Origen", SqlDbType.VarChar, 0, Viaje.Origen),
                        DBHelper.MakeParam("@FechaSalida", SqlDbType.DateTime, 0, Viaje.FechaSalida),
                        DBHelper.MakeParam("@HoraSalida", SqlDbType.VarChar, 0, Viaje.HoraSalida),
                        DBHelper.MakeParam("@PaisOrigen", SqlDbType.VarChar, 0, Viaje.PaisOrigen),
                        DBHelper.MakeParam("@PaisDestino", SqlDbType.VarChar, 0, Viaje.PaisDestino),
                        DBHelper.MakeParam("@Paso", SqlDbType.VarChar, 0, Viaje.Paso),
                        DBHelper.MakeParam("@Medio", SqlDbType.VarChar, 0, Viaje.Medio),
                        DBHelper.MakeParam("@BusID", SqlDbType.VarChar, 0, Viaje.BusID),
                        DBHelper.MakeParam("@FechaRegreso", SqlDbType.DateTime, 0, Viaje.FechaRegreso),
                        DBHelper.MakeParam("@HoraRegreso", SqlDbType.VarChar, 0, Viaje.HoraRegreso),
                        DBHelper.MakeParam("@TiempoConsentracion", SqlDbType.Int, 0, Viaje.TiempoConsentracion),
                        DBHelper.MakeParam("@Descripcion", SqlDbType.VarChar, 0, Viaje.Descripcion),
                        DBHelper.MakeParam("@MonedaTipo", SqlDbType.Int, 1, Viaje.MonedaTipo),
                        DBHelper.MakeParam("@PrecioSemicama", SqlDbType.Float, 0, Viaje.PrecioSemicama),
                        DBHelper.MakeParam("@PrecioCama", SqlDbType.Float, 0, Viaje.PrecioCama),
                        DBHelper.MakeParam("@PrecioPromocional", SqlDbType.Float, 0, Viaje.PrecioPromocional),
                        DBHelper.MakeParam("@FechaPromocion", SqlDbType.DateTime, 0, Viaje.FechaPromocion),
                        DBHelper.MakeParam("@nDias", SqlDbType.Int, 0, Viaje.nDias),
                        DBHelper.MakeParam("@nNoches", SqlDbType.Int, 0, Viaje.nNoches),
                        DBHelper.MakeParam("@Observaciones", SqlDbType.VarChar, 500, Viaje.Observaciones),
                        DBHelper.MakeParam("@IsPublicWeb", SqlDbType.Bit, 0, Viaje.IsPublicWeb),
                        DBHelper.MakeParam("@VendedorID", SqlDbType.UniqueIdentifier, 0, MATContext.CurrentVendedor.VendedorId),
                        
                    };
                    using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Viaje_NewViaje", dbParams))
                    {
                        if (_reader.Read())
                        {
                            sResult[0] = _reader["ViajeID"].ToString();
                            sResult[1] = _reader["Result"].ToString();//Done.
                        }
                    }
                }
                
            }
            catch (Exception e)
            {
                sResult[0] = "-1";
                sResult[1] = ErrorUtil.LogAndGetPublicMessage(e, "ViajeModel");

            }
            return sResult;
        }

        public static void DeleteViaje(Guid ViajeID)
        {
            SqlParameter[] dbParams = new SqlParameter[]
            {
                DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, ViajeID)

            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Viaje_DeleteViaje", dbParams);
        }

        public static void CancelViaje(Guid ViajeID, Guid VendedorId, string DeleteDetalle)
        {
            SqlParameter[] dbParams = new SqlParameter[]
            {
                DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, ViajeID),
                DBHelper.MakeParam("@VendedorId", SqlDbType.UniqueIdentifier, 0, VendedorId),
                DBHelper.MakeParam("@DeleteDetalle", SqlDbType.VarChar, 500, DeleteDetalle)

            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Viaje_CancelViaje", dbParams);
        }

        public static string[] UpdateViaje(ViajeModel Viaje)
        {
            string[] sResult = new string[2];

            try
            {
                if (Viaje.PaqueteID != "" && Viaje.PaqueteID != null)
                {
                    SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@ViajeID", SqlDbType.VarChar, 0, Viaje.ViajeID),
                        DBHelper.MakeParam("@PaqueteID", SqlDbType.VarChar, 0, Viaje.PaqueteID),
                        DBHelper.MakeParam("@Origen", SqlDbType.VarChar, 0, Viaje.Origen),
                        DBHelper.MakeParam("@FechaSalida", SqlDbType.DateTime, 0, Viaje.FechaSalida),
                        DBHelper.MakeParam("@HoraSalida", SqlDbType.VarChar, 0, Viaje.HoraSalida),
                        DBHelper.MakeParam("@PaisOrigen", SqlDbType.VarChar, 0, Viaje.PaisOrigen),
                        DBHelper.MakeParam("@PaisDestino", SqlDbType.VarChar, 0, Viaje.PaisDestino),
                        DBHelper.MakeParam("@Paso", SqlDbType.VarChar, 0, Viaje.Paso),
                        DBHelper.MakeParam("@Medio", SqlDbType.VarChar, 0, Viaje.Medio),
                        DBHelper.MakeParam("@BusID", SqlDbType.VarChar, 0, Viaje.BusID),
                        DBHelper.MakeParam("@FechaRegreso", SqlDbType.DateTime, 0, Viaje.FechaRegreso),
                        DBHelper.MakeParam("@HoraRegreso", SqlDbType.VarChar, 0, Viaje.HoraRegreso),
                        DBHelper.MakeParam("@TiempoConsentracion", SqlDbType.Int, 0, Viaje.TiempoConsentracion),
                        DBHelper.MakeParam("@Descripcion", SqlDbType.VarChar, 0, Viaje.Descripcion),
                        DBHelper.MakeParam("@MonedaTipo", SqlDbType.Int, 1, Viaje.MonedaTipo),
                        DBHelper.MakeParam("@PrecioSemicama", SqlDbType.Float, 0, Viaje.PrecioSemicama),
                        DBHelper.MakeParam("@PrecioCama", SqlDbType.Float, 0, Viaje.PrecioCama),
                        DBHelper.MakeParam("@PrecioPromocional", SqlDbType.Float, 0, Viaje.PrecioPromocional),
                        DBHelper.MakeParam("@FechaPromocion", SqlDbType.DateTime, 0, Viaje.FechaPromocion),
                        DBHelper.MakeParam("@nDias", SqlDbType.Int, 0, Viaje.nDias),
                        DBHelper.MakeParam("@nNoches", SqlDbType.Int, 0, Viaje.nNoches),
                        DBHelper.MakeParam("@Observaciones", SqlDbType.VarChar, 500, Viaje.Observaciones),
                        DBHelper.MakeParam("@IsPublicWeb", SqlDbType.Bit, 0, Viaje.IsPublicWeb),
                    };
                    using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Viaje_Update", dbParams))
                    {
                        if (_reader.Read())
                        {
                            sResult[0] = _reader["ViajeID"].ToString();
                            sResult[1] = _reader["Result"].ToString();//Done.
                        }
                    }
                }

            }
            catch (Exception e)
            {
                sResult[0] = "-1";
                sResult[1] = ErrorUtil.LogAndGetPublicMessage(e, "ViajeModel");

            }
            return sResult;
        }

        public static List<ViajeModel> ListViajeByYear(string sDateYear)
        {
            List<ViajeModel> ListViajes = new List<ViajeModel>();
            try
            {
                 
                SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@DateYear", SqlDbType.VarChar, 0, sDateYear)
                };
                using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Viaje_GetViajesByDateYear", dbParams))
                {
                    while (_reader.Read())
                    {
                        ViajeModel Item = new ViajeModel();
                        Item.ViajeID = _reader["ViajeID"].ToString();
                        Item.PaqueteID = _reader["PaqueteID"].ToString();
                        Item.Origen = _reader["Origen"].ToString();
                        Item.FechaSalida = _reader["FechaSalida"].ToString();
                        Item.HoraSalida = _reader["HoraSalida"].ToString();
                        Item.PaisOrigen = _reader["PaisOrigen"].ToString();
                        Item.PaisDestino = _reader["PaisDestino"].ToString();
                        Item.MonedaTipo = Convert.ToInt32(_reader["MonedaTipo"]);
                        Item.Paso = _reader["Paso"].ToString();
                        Item.Medio = _reader["Medio"].ToString();
                        Item.BusID = _reader["BusID"].ToString();
                        Item.FechaRegreso = _reader["FechaRegreso"].ToString();
                        Item.HoraRegreso = _reader["HoraRegreso"].ToString();
                        Item.Descripcion = _reader["Descripcion"].ToString();
                        if (_reader["PrecioSemicama"].ToString() != "")
                        {
                            Item.PrecioSemicama = Convert.ToDouble(_reader["PrecioSemicama"]);    
                        }

                        if (_reader["PrecioCama"].ToString() != "")
                        {
                            Item.PrecioCama = Convert.ToDouble(_reader["PrecioCama"]);    
                        }

                        if (_reader["PrecioPromocional"].ToString() != "")
                        {
                            Item.PrecioPromocional = Convert.ToDouble(_reader["PrecioPromocional"]);    
                        }
                        Item.FechaPromocion = _reader["FechaPromocion"].ToString();
                        if (_reader["nDias"].ToString() != "")
                        {
                            Item.nDias = Convert.ToInt32(_reader["nDias"]);    
                        }
                        if (_reader["nNoches"].ToString() != "")
                        {
                            Item.nNoches = Convert.ToInt32(_reader["nNoches"]);    
                        }
                                            
                        Item.PaqueteNombre = _reader["PaqueteNombre"].ToString();

                        Item.IsPublicWeb = Convert.ToBoolean(_reader["IsPublicWeb"]);

                        ListViajes.Add(Item);
                    }
                }

                return ListViajes;
            }
            catch (Exception e)
            {
                return ListViajes;
            }
        }

        public static ViajeModel ViajeByViajeID(string sViajeID)
        {
            ViajeModel Item = new ViajeModel();
            try
            {

                SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@ViajeID", SqlDbType.VarChar, 0, sViajeID)
                };
                using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Viaje_GetViajeByViajeID", dbParams))
                {
                    if (_reader.Read())
                    {
                        Item.ViajeID = _reader["ViajeID"].ToString();
                        Item.PaqueteID = _reader["PaqueteID"].ToString();
                        Item.Origen = _reader["Origen"].ToString();
                        Item.FechaSalida = _reader["FechaSalida"].ToString();
                        Item.HoraSalida = _reader["HoraSalida"].ToString();
                        Item.PaisOrigen = _reader["PaisOrigen"].ToString();
                        Item.PaisDestino = _reader["PaisDestino"].ToString();
                        Item.Paso = _reader["Paso"].ToString();
                        Item.Medio = _reader["Medio"].ToString();
                        Item.BusID = _reader["BusID"].ToString();
                        Item.FechaRegreso = _reader["FechaRegreso"].ToString();
                        Item.HoraRegreso = _reader["HoraRegreso"].ToString();
                        Item.TiempoConsentracion = Convert.ToInt32(_reader["TiempoConsentracion"]);
                        Item.Descripcion = _reader["Descripcion"].ToString();
                        Item.MonedaTipo = Convert.ToInt32(_reader["MonedaTipo"]);
                        if (_reader["PrecioSemicama"].ToString() != "")
                        {
                            Item.PrecioSemicama = Convert.ToDouble(_reader["PrecioSemicama"]);
                        }

                        if (_reader["PrecioCama"].ToString() != "")
                        {
                            Item.PrecioCama = Convert.ToDouble(_reader["PrecioCama"]);
                        }

                        if (_reader["PrecioPromocional"].ToString() != "")
                        {
                            Item.PrecioPromocional = Convert.ToDouble(_reader["PrecioPromocional"]);
                        }
                        Item.FechaPromocion = _reader["FechaPromocion"].ToString();
                        Item.Observaciones = _reader["Observaciones"].ToString();
                        if (_reader["nDias"].ToString() != "")
                        {
                            Item.nDias = Convert.ToInt32(_reader["nDias"]);
                        }
                        if (_reader["nNoches"].ToString() != "")
                        {
                            Item.nNoches = Convert.ToInt32(_reader["nNoches"]);
                        }
                                            
                        Item.PaqueteNombre = _reader["PaqueteNombre"].ToString();
                        Item.IsPublicWeb = Convert.ToBoolean(_reader["IsPublicWeb"]);
                    }
                }
                return Item;
            }
            catch
            {

                return Item;;
            }
        }
        //public static List<ViajeModel> ViajeByDate(string sDate)
        //{
        //    List<ViajeModel> ListViajes = new List<ViajeModel>();
        //    try
        //    {
        //        ViajeModel Item = new ViajeModel();
        //        SqlParameter[] dbParams = new SqlParameter[]
        //        {
        //            DBHelper.MakeParam("@Date", SqlDbType.VarChar, 10, sDate)
        //        };
        //        SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Viaje_GetViajeByDate", dbParams);


        //        if (_reader.Read())
        //        {
        //            Item.ViajeID = _reader["ViajeID"].ToString();
        //            Item.PaqueteID = _reader["PaqueteID"].ToString();
        //            Item.Origen = _reader["Origen"].ToString();
        //            Item.FechaSalida = _reader["FechaSalida"].ToString();
        //            Item.HoraSalida = _reader["HoraSalida"].ToString();
        //            Item.PaisOrigen = _reader["PaisOrigen"].ToString();
        //            Item.PaisDestino = _reader["PaisDestino"].ToString();
        //            Item.Paso = _reader["Paso"].ToString();
        //            Item.Medio = _reader["Medio"].ToString();
        //            Item.BusID = _reader["BusID"].ToString();
        //            Item.FechaRegreso = _reader["FechaRegreso"].ToString();
        //            Item.HoraRegreso = _reader["HoraRegreso"].ToString();
        //            Item.TiempoConsentracion = Convert.ToInt32(_reader["TiempoConsentracion"]);
        //            Item.Descripcion = _reader["Descripcion"].ToString();
        //            Item.MonedaTipo = Convert.ToInt32(_reader["MonedaTipo"]);
        //            if (_reader["PrecioSemicama"].ToString() != "")
        //            {
        //                Item.PrecioSemicama = Convert.ToDouble(_reader["PrecioSemicama"]);
        //            }

        //            if (_reader["PrecioCama"].ToString() != "")
        //            {
        //                Item.PrecioCama = Convert.ToDouble(_reader["PrecioCama"]);
        //            }

        //            if (_reader["PrecioPromocional"].ToString() != "")
        //            {
        //                Item.PrecioPromocional = Convert.ToDouble(_reader["PrecioPromocional"]);
        //            }
        //            Item.FechaPromocion = _reader["FechaPromocion"].ToString();
        //            Item.Observaciones = _reader["Observaciones"].ToString();
        //            if (_reader["nDias"].ToString() != "")
        //            {
        //                Item.nDias = Convert.ToInt32(_reader["nDias"]);
        //            }
        //            if (_reader["nNoches"].ToString() != "")
        //            {
        //                Item.nNoches = Convert.ToInt32(_reader["nNoches"]);
        //            }

        //            Item.PaqueteNombre = _reader["PaqueteNombre"].ToString();
        //            Item.IsPublicWeb = Convert.ToBoolean(_reader["IsPublicWeb"]);
        //            ListViajes.Add(Item);

        //        }
        //        return ListViajes;
        //    }
        //    catch
        //    {

        //        return ListViajes; ;
        //    }
        //}

        public static List<ViajeModel> ViajeByDate(string sDate = null, int? year = null)
        {
            List<ViajeModel> ListViajes = new List<ViajeModel>();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();

                if (!string.IsNullOrEmpty(sDate))
                {
                    parameters.Add(DBHelper.MakeParam("@Date", SqlDbType.VarChar, 10, sDate));
                }
                else
                {
                    parameters.Add(DBHelper.MakeParam("@Date", SqlDbType.VarChar, 10, DBNull.Value));
                }

                if (year.HasValue)
                {
                    parameters.Add(DBHelper.MakeParam("@Year", SqlDbType.Int, 4, year.Value));
                }
                else
                {
                    parameters.Add(DBHelper.MakeParam("@Year", SqlDbType.Int, 4, DBNull.Value));
                }

                using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Viaje_GetViajeByDate", parameters.ToArray()))
                {
                    while (_reader.Read())
                    {
                        ViajeModel Item = new ViajeModel();
                        Item.ViajeID = _reader["ViajeID"].ToString();
                        Item.PaqueteID = _reader["PaqueteID"].ToString();
                        Item.Origen = _reader["Origen"].ToString();
                        Item.FechaSalida = _reader["FechaSalida"].ToString();
                        Item.HoraSalida = _reader["HoraSalida"].ToString();
                        Item.PaisOrigen = _reader["PaisOrigen"].ToString();
                        Item.PaisDestino = _reader["PaisDestino"].ToString();
                        Item.Paso = _reader["Paso"].ToString();
                        Item.Medio = _reader["Medio"].ToString();
                        Item.BusID = _reader["BusID"].ToString();
                        Item.FechaRegreso = _reader["FechaRegreso"].ToString();
                        Item.HoraRegreso = _reader["HoraRegreso"].ToString();
                        Item.TiempoConsentracion = Convert.ToInt32(_reader["TiempoConsentracion"]);
                        Item.Descripcion = _reader["Descripcion"].ToString();
                        Item.MonedaTipo = Convert.ToInt32(_reader["MonedaTipo"]);

                        if (!string.IsNullOrEmpty(_reader["PrecioSemicama"].ToString()))
                        {
                            Item.PrecioSemicama = Convert.ToDouble(_reader["PrecioSemicama"]);
                        }

                        if (!string.IsNullOrEmpty(_reader["PrecioCama"].ToString()))
                        {
                            Item.PrecioCama = Convert.ToDouble(_reader["PrecioCama"]);
                        }

                        if (!string.IsNullOrEmpty(_reader["PrecioPromocional"].ToString()))
                        {
                            Item.PrecioPromocional = Convert.ToDouble(_reader["PrecioPromocional"]);
                        }

                        Item.FechaPromocion = _reader["FechaPromocion"].ToString();
                        Item.Observaciones = _reader["Observaciones"].ToString();

                        if (!string.IsNullOrEmpty(_reader["nDias"].ToString()))
                        {
                            Item.nDias = Convert.ToInt32(_reader["nDias"]);
                        }

                        if (!string.IsNullOrEmpty(_reader["nNoches"].ToString()))
                        {
                            Item.nNoches = Convert.ToInt32(_reader["nNoches"]);
                        }

                        Item.PaqueteNombre = _reader["PaqueteNombre"].ToString();
                        Item.IsPublicWeb = Convert.ToBoolean(_reader["IsPublicWeb"]);
                        ListViajes.Add(Item);
                    }
                }
                return ListViajes;
            }
            catch
            {
                return ListViajes;
            }
        }

        public static List<DDViaje> DDViaje()
        {
            SqlParameter[] dbParams = new SqlParameter[] { };
            
            List<DDViaje> lViaje = new List<DDViaje>();
            
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Viaje_DDViaje", dbParams))
            {
                while (_reader.Read())
                {
                    DDViaje _item = new DDViaje();
                    _item.ViajeID = _reader["ViajeID"].ToString();
                    _item.Descripcion = _reader["Paquete"].ToString() + " - " + _reader["Descripcion"].ToString() + " - " + _reader["FechaSalida"].ToString();
                    lViaje.Add(_item);
                }
            }

            return lViaje;
        }

        public static DateTime GetFechaViajeByFacturaID(Guid FacturaID)
        {
            var (fecha, _) = GetDatosViajeByFacturaID(FacturaID);
            return fecha;
        }

        /// <summary>
        /// Obtiene fecha de salida y nombre/descripción del viaje asociado a una factura.
        /// </summary>
        public static (DateTime FechaSalida, string ViajeNombre) GetDatosViajeByFacturaID(Guid FacturaID)
        {
            SqlParameter[] dbParams = new SqlParameter[]
            {
                DBHelper.MakeParam("@FacturaID", SqlDbType.UniqueIdentifier, 0, FacturaID)
            };
            DateTime dFecha = default;
            string viajeNombre = null;
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Viaje_GetFechaViajeByFacturaID", dbParams))
            {
                if (_reader.Read())
                {
                    if (_reader["FechaSalida"] != DBNull.Value && _reader["FechaSalida"] != null)
                        dFecha = Convert.ToDateTime(_reader["FechaSalida"]);
                    if (_reader["ViajeNombre"] != DBNull.Value && _reader["ViajeNombre"] != null)
                        viajeNombre = _reader["ViajeNombre"].ToString();
                }
            }
            return (dFecha, viajeNombre ?? string.Empty);
        }

        public static DataSet GetAvailableHoteles(Guid ViajeID)
        {
            SqlParameter[] dbParams = new SqlParameter[]
            {                    
                DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, ViajeID)
                        
            };
            return DBHelper.ExecuteDataSet("dbo.usp_MAT_Viajes_GetListHotel", dbParams);
        }

        public static void DeleteHotel(Guid ViajeID, Guid HotelID)
        {
            SqlParameter[] dbParams = new SqlParameter[]
            {                    
                DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, ViajeID),
                DBHelper.MakeParam("@HotelID", SqlDbType.UniqueIdentifier, 0, HotelID)
                        
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_ViajeHotel_Delete", dbParams);
            
        }

        public static DataSet GetDetalleViaje(Guid ViajeID)
        {
            SqlParameter[] dbParams = new SqlParameter[]
            {                    
                DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, ViajeID)
                        
            };
            return DBHelper.ExecuteDataSet("dbo.usp_MAT_Reserva_GetDetalleViaje", dbParams);
        }

        /// <summary>
        /// Obtiene el detalle del viaje desde la base de datos (para fallback cuando la API no está disponible).
        /// </summary>
        public static DetalleViaje GetDetalleViajeModel(Guid ViajeID)
        {
            var oDetalleViaje = new DetalleViaje();
            DataSet ds = GetDetalleViaje(ViajeID);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return oDetalleViaje;
            DataRow item = ds.Tables[0].Rows[0];
            oDetalleViaje.Descripcion = item["Descripcion"].ToString();
            oDetalleViaje.Destino = item["Destino"].ToString();
            oDetalleViaje.FechaRegreso = item["FechaRegreso"].ToString();
            oDetalleViaje.FechaSalida = item["FechaSalida"].ToString();
            oDetalleViaje.HoraRegreso = item["HoraRegreso"].ToString();
            oDetalleViaje.HoraSalida = item["HoraSalida"].ToString();
            oDetalleViaje.TiempoConsentracion = Convert.ToInt32(item["TiempoConsentracion"]);
            oDetalleViaje.NroCoche = item["NroCoche"].ToString();
            oDetalleViaje.TransportePatente = ds.Tables[0].Columns.Contains("TransportePatente") && item["TransportePatente"] != DBNull.Value && item["TransportePatente"] != null
                ? item["TransportePatente"].ToString()
                : null;
            oDetalleViaje.PaqueteExcusionesIncluidas = item["PaqueteExcusionesIncluidas"].ToString();
            oDetalleViaje.PaqueteExcusionesOpcionales = item["PaqueteExcusionesOpcionales"].ToString();
            oDetalleViaje.PaqueteServicios = item["PaqueteServicios"].ToString();
            oDetalleViaje.Observaciones = item["Observaciones"].ToString();
            oDetalleViaje.ViajeID = ViajeID.ToString();
            oDetalleViaje.Itinerario = ItinerarioMethod.GetItinerarioByViajeID(ViajeID.ToString());
            return oDetalleViaje;
        }

        public static List<PasajeroViaje> GetListPasajerosByViajeID(Guid ViajeID)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@ViajeID", SqlDbType.VarChar, 0, Convert.ToString(ViajeID)),
                    };
            List<PasajeroViaje> EPasajeroViaje = new List<PasajeroViaje>();
            
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_PasajeroViaje_GetByViajeID", dbParams))
            {
                while (_reader.Read())
                {
                    PasajeroViaje item = new PasajeroViaje();
                    item.ViajeID =  _reader["ViajeID"].ToString();
                    item.PasajeroID = _reader["PersonaID"].ToString();
                    item.PasajeroApellido = _reader["Apellido"].ToString();
                    item.PasajeroNombre = _reader["Nombre"].ToString();
                    item.PasajeroDocumento = _reader["NroDocumento"].ToString();
                    item.PasajeroTipoDocumento = _reader["TipoDocumento"].ToString();
                    item.PasajeroFechaNacimiento = _reader["FechaNacimiento"].ToString();
                    item.PasajeroTelefono = _reader["Telefono"].ToString();
                    item.PasajeroEmail = _reader["Email"].ToString();
                    item.PasajeroSexo = _reader["Sexo"].ToString();

                    EPasajeroViaje.Add(item);
                }
            }

            return EPasajeroViaje;
        }

        public static string GetViajesPorVencer()
        {
            try
            {
                SqlParameter[] dbParams = new SqlParameter[] { };

                DataSet dataSet = DBHelper.ExecuteDataSet("dbo.usp_MAT_Dashboard_Viajes_LugaresDisponibles", dbParams);

                // Validar que tenga al menos una tabla y filas
                if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
                {
                    return JsonConvert.SerializeObject(new { message = "No se encontraron datos." });
                }

                string sJson = JsonConvert.SerializeObject(dataSet, Formatting.Indented);
                return sJson;
            }
            catch (Exception ex)
            {
                // Podés loguearlo o manejarlo como prefieras
                return JsonConvert.SerializeObject(new { error = ex.Message });
            }
        }

        public static List<ViajePorVencerDto> GetViajesPorVencerDto(int maxItems = 7)
        {
            SqlParameter[] dbParams = new SqlParameter[] { };
            DataSet ds = DBHelper.ExecuteDataSet("dbo.usp_MAT_Dashboard_Viajes_LugaresDisponibles", dbParams);

            var result = new List<ViajePorVencerDto>();
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0] == null) return result;

            var t = ds.Tables[0];
            Func<string, bool> hasCol = (name) => t.Columns.Contains(name);

            foreach (DataRow dr in t.Rows)
            {
                if (result.Count >= maxItems) break;

                // Columnas esperadas por el SP: ViajeID, descripcion, fechasalida, Disponibles (casing puede variar)
                object viajeIdObj = hasCol("ViajeID") ? dr["ViajeID"] : (hasCol("ViajeId") ? dr["ViajeId"] : null);
                if (viajeIdObj == null || viajeIdObj == DBNull.Value) continue;

                Guid viajeId;
                if (!Guid.TryParse(viajeIdObj.ToString(), out viajeId)) continue;

                string descripcion = "";
                if (hasCol("descripcion") && dr["descripcion"] != DBNull.Value) descripcion = dr["descripcion"].ToString();
                else if (hasCol("Descripcion") && dr["Descripcion"] != DBNull.Value) descripcion = dr["Descripcion"].ToString();

                int disponibles = 0;
                object dispObj = hasCol("Disponibles") ? dr["Disponibles"] : (hasCol("disponibles") ? dr["disponibles"] : null);
                if (dispObj != null && dispObj != DBNull.Value) int.TryParse(dispObj.ToString(), out disponibles);

                DateTime? fechaSalida = null;
                object fsObj = hasCol("fechasalida") ? dr["fechasalida"] : (hasCol("FechaSalida") ? dr["FechaSalida"] : null);
                if (fsObj != null && fsObj != DBNull.Value)
                {
                    DateTime fs;
                    if (DateTime.TryParse(fsObj.ToString(), out fs)) fechaSalida = fs;
                }

                int? days = null;
                if (fechaSalida.HasValue)
                {
                    var today = DateTime.Today;
                    var target = fechaSalida.Value.Date;
                    days = (int)Math.Ceiling((target - today).TotalDays);
                }

                result.Add(new ViajePorVencerDto
                {
                    ViajeID = viajeId,
                    Descripcion = descripcion ?? "",
                    FechaSalida = fechaSalida,
                    Disponibles = disponibles,
                    DaysToDeparture = days
                });
            }

            return result;
        }

    }
}