using MAT.MVC.Common;
using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
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
        public string Descripcion { get; set; }
        public string Destino { get; set; }
        public string FechaSalida { get; set; }
        public string FechaRegreso { get; set; }
        public string HoraSalida { get; set; }
        public string HoraRegreso { get; set; }
        public int TiempoConsentracion { get; set; }
        public string NroCoche { get; set; }
        public string PaqueteServicios { get; set; }
        public string PaqueteExcusionesIncluidas { get; set; }
        public string PaqueteExcusionesOpcionales { get; set; }
        public string Observaciones { get; set; }
    }

    public class PasajeroViaje {
        public string PasajeroID { get; set; }
        public string ViajeID { get; set; }
        public string PasajeroNombre { get; set; }
        public string PasajeroApellido { get; set; }
        public string PasajeroDocumento { get; set; }
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
                        DBHelper.MakeParam("@PrecioSemicama", SqlDbType.Float, 0, Viaje.PrecioSemicama),
                        DBHelper.MakeParam("@PrecioCama", SqlDbType.Float, 0, Viaje.PrecioCama),
                        DBHelper.MakeParam("@PrecioPromocional", SqlDbType.Float, 0, Viaje.PrecioPromocional),
                        DBHelper.MakeParam("@FechaPromocion", SqlDbType.DateTime, 0, Viaje.FechaPromocion),
                        DBHelper.MakeParam("@nDias", SqlDbType.Int, 0, Viaje.nDias),
                        DBHelper.MakeParam("@nNoches", SqlDbType.Int, 0, Viaje.nNoches),
                        DBHelper.MakeParam("@Observaciones", SqlDbType.VarChar, 500, Viaje.Observaciones),
                        DBHelper.MakeParam("@VendedorID", SqlDbType.UniqueIdentifier, 0, MATContext.CurrentVendedor.VendedorId)
                    };
                    SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Viaje_NewViaje", dbParams);


                    if (_reader.Read())
                    {
                        sResult[0] = _reader["ViajeID"].ToString();
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
                        DBHelper.MakeParam("@PrecioSemicama", SqlDbType.Float, 0, Viaje.PrecioSemicama),
                        DBHelper.MakeParam("@PrecioCama", SqlDbType.Float, 0, Viaje.PrecioCama),
                        DBHelper.MakeParam("@PrecioPromocional", SqlDbType.Float, 0, Viaje.PrecioPromocional),
                        DBHelper.MakeParam("@FechaPromocion", SqlDbType.DateTime, 0, Viaje.FechaPromocion),
                        DBHelper.MakeParam("@nDias", SqlDbType.Int, 0, Viaje.nDias),
                        DBHelper.MakeParam("@nNoches", SqlDbType.Int, 0, Viaje.nNoches),
                        DBHelper.MakeParam("@Observaciones", SqlDbType.VarChar, 500, Viaje.Observaciones)
                    };
                    SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Viaje_Update", dbParams);


                    if (_reader.Read())
                    {
                        sResult[0] = _reader["ViajeID"].ToString();
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

        public static List<ViajeModel> ListViajeByYear(string sDateYear)
        {
            List<ViajeModel> ListViajes = new List<ViajeModel>();
            try
            {
                 
                SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@DateYear", SqlDbType.VarChar, 0, sDateYear)
                };
                SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Viaje_GetViajesByDateYear", dbParams);


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

                    //Item.DeleteOn = _reader["DeleteOn"].ToString();

                    ListViajes.Add(Item);


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
                SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Viaje_GetViajeByViajeID", dbParams);


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


                }
                return Item;
            }
            catch
            {

                return Item;;
            }
        }

        public static List<DDViaje> DDViaje()
        {
            SqlParameter[] dbParams = new SqlParameter[] { };
            SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Viaje_DDViaje", dbParams);

            List<DDViaje> lViaje = new List<DDViaje>();
            while (_reader.Read())
            {
                DDViaje _item = new DDViaje();
                _item.ViajeID = _reader["ViajeID"].ToString();
                _item.Descripcion = _reader["Paquete"].ToString() + " - " + _reader["Descripcion"].ToString() + " - " + _reader["FechaSalida"].ToString();
                lViaje.Add(_item);
            }

            return lViaje;
        }

        public static DateTime GetFechaViajeByFacturaID(Guid FacturaID)
        {
            SqlParameter[] dbParams = new SqlParameter[]
            {                    
                DBHelper.MakeParam("@FacturaID", SqlDbType.UniqueIdentifier, 0, FacturaID)
                        
            };
            SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Viaje_GetFechaViajeByFacturaID", dbParams);

            DateTime dFecha = new DateTime();
            while (_reader.Read())
            {
                dFecha = Convert.ToDateTime(_reader["FechaSalida"]);
                break;
            }
            return dFecha;
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

        public static List<PasajeroViaje> GetListPasajerosByViajeID(Guid ViajeID)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@ViajeID", SqlDbType.VarChar, 0, Convert.ToString(ViajeID)),
                    };
            SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_PasajeroViaje_GetByViajeID", dbParams);


            List<PasajeroViaje> EPasajeroViaje = new List<PasajeroViaje>();
            while (_reader.Read())
            {
                PasajeroViaje item = new PasajeroViaje();
                item.ViajeID =  _reader["ViajeID"].ToString();
                item.PasajeroID = _reader["PersonaID"].ToString();
                item.PasajeroApellido = _reader["Apellido"].ToString();
                item.PasajeroNombre = _reader["Nombre"].ToString();
                item.PasajeroDocumento = _reader["NroDocumento"].ToString();
                
                EPasajeroViaje.Add(item);

            }

            return EPasajeroViaje;
        }
        
    }
}