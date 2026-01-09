using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MAT.MVC.Models
{
    public class HabitacionStandard
    {
        public string HabitacionID { get; set; }
        public int NroHabitacion { get; set; }
        public int Tipo { get; set; }
        public string HabitacionTipo { get; set; }
        public string HotelID { get; set; }
        public int Estado { get; set; }
        public int Capacidad { get; set; }
        public int Ocupacion { get; set; }
        public string Nombre { get; set; }
        public string Hotel { get; set; }
        public decimal HabitacionPrecio { get; set; }
        public string HabitacionDescripcion { get; set; }
    }

    public class HabitacionDisponibilidad
    {
        public string HabitacionID { get; set; }
        public int NroHabitacion { get; set; }
        public int Tipo { get; set; }
        public string HabitacionTipo { get; set; }
        public string HabitacionNombre { get; set; }
        public string HotelNombre { get; set; }
        public int Disponibilidad { get; set; }
    }

    public class HabitacionMethod
    {
        public static string[] NewHabitacion(HabitacionStandard Hab)
        {
            string [] sResult = new string[2];
            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@NroHabitacion", SqlDbType.Int, 0, Hab.NroHabitacion),
                    DBHelper.MakeParam("@Tipo", SqlDbType.Int, 0, Hab.Tipo),
                    DBHelper.MakeParam("@HotelID", SqlDbType.UniqueIdentifier, 0,new Guid(Hab.HotelID)),
                    DBHelper.MakeParam("@Estado", SqlDbType.Int, 0, Hab.Estado),
                    DBHelper.MakeParam("@Capacidad", SqlDbType.Int, 0, Hab.Capacidad),
                    DBHelper.MakeParam("@Ocupacion", SqlDbType.Int, 0, Hab.Ocupacion),
                    DBHelper.MakeParam("@Nombre", SqlDbType.VarChar, 50, Hab.Nombre)
                };
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Habitacion_NewHabitacion", dbParams))
            {
                if (_reader.Read())
                {
                    if (_reader["Result"].ToString() == "Done.")
                    {
                        sResult[0] = "Done.";
                        sResult[1] = "";
                    }
                    else
                    {
                        sResult[0] = "";
                        sResult[1] = _reader["Result"].ToString();
                    }
                }
            }

            return sResult;
        }

        public static string[] EditHabitacion(HabitacionStandard Hab)
        {
            string[] sResult = new string[2];
            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@HabitacionID", SqlDbType.UniqueIdentifier, 0, new Guid(Hab.HabitacionID)),
                    DBHelper.MakeParam("@NroHabitacion", SqlDbType.Int, 0, Hab.NroHabitacion),
                    DBHelper.MakeParam("@Tipo", SqlDbType.Int, 0, Hab.Tipo),
                    DBHelper.MakeParam("@Estado", SqlDbType.Int, 0, Hab.Estado),
                    DBHelper.MakeParam("@Capacidad", SqlDbType.Int, 0, Hab.Capacidad),
                    DBHelper.MakeParam("@Nombre", SqlDbType.VarChar, 100, Hab.Nombre),
                    DBHelper.MakeParam("@HabitacionPrecio", SqlDbType.Decimal, 0, Hab.HabitacionPrecio),
                    DBHelper.MakeParam("@HabitacionDescripcion", SqlDbType.VarChar, 100, Hab.HabitacionDescripcion),
                };
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Habitacion_EditHabitacion", dbParams))
            {
                if (_reader.Read())
                {
                    if (_reader["Result"].ToString() == "Done.")
                    {
                        sResult[0] = "Done.";
                        sResult[1] = "";
                    }
                    else
                    {
                        sResult[0] = "";
                        sResult[1] = _reader["Result"].ToString();
                    }
                }
            }

            return sResult;
        }

        public static void DeleteHabitacion(string HabitacionID)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@HabitacionID", SqlDbType.UniqueIdentifier, 0, new Guid(HabitacionID)),
                };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Habitacion_Delete", dbParams);
        }

        public static HabitacionStandard GetHabitacionById(string sHabitacionId)
        { 
            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@HabitacionID", SqlDbType.UniqueIdentifier, 0, new Guid(sHabitacionId))
                       
                    };
            HabitacionStandard Hab = new HabitacionStandard();
            
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Habitacion_GetById", dbParams))
            {
                if (_reader.Read())
                {
                    Hab.HabitacionID = _reader["habitacionid"].ToString();
                    Hab.NroHabitacion = Convert.ToInt32(_reader["nrohabitacion"]);
                    Hab.Tipo = Convert.ToInt32(_reader["tipo"]);
                    Hab.HabitacionTipo = _reader["HabitacionTipo"].ToString();
                    //Hab.HotelID = _reader["hotelid"].ToString();
                    //Hab.Estado = Convert.ToInt32(_reader["estado"]);
                    Hab.Capacidad = Convert.ToInt32(_reader["capacidad"]);
                    //Hab.Ocupacion = Convert.ToInt32(_reader["ocupacion"]);
                    Hab.Nombre = _reader["nombre"].ToString();
                    //Hab.Hotel = _reader["Hotel"].ToString();
                    Hab.HabitacionPrecio = Convert.ToDecimal(_reader["HabPrecio"]);
                    Hab.HabitacionDescripcion = _reader["HabDescripcion"].ToString();
                }
            }
            return Hab;
        }

        public static DataSet GetHabitacionByHotelId(string sHotelId)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@HotelId", SqlDbType.UniqueIdentifier, 0, new Guid(sHotelId))
                       
                    };
            return DBHelper.ExecuteDataSet("dbo.usp_MAT_Habitacion_GetByHotelId", dbParams);

            //List<HabitacionStandard> ListHab = new List<HabitacionStandard>();
            //while (_reader.Read())
            //{
            //    HabitacionStandard Hab = new HabitacionStandard();
            //    Hab.HabitacionID = _reader["habitacionid"].ToString();
            //    Hab.NroHabitacion = Convert.ToInt32(_reader["nrohabitacion"]);
            //    Hab.Tipo = Convert.ToInt32(_reader["tipo"]);
            //    Hab.HabitacionTipo = _reader["HabitacionTipo"].ToString();
            //    Hab.HotelID = _reader["hotelid"].ToString();
            //    Hab.Estado = Convert.ToInt32(_reader["estado"]);
            //    Hab.Capacidad = Convert.ToInt32(_reader["capacidad"]);
            //    Hab.Ocupacion = Convert.ToInt32(_reader["ocupacion"]);
            //    Hab.Nombre = _reader["nombre"].ToString();
            //    Hab.Hotel = _reader["Hotel"].ToString();
            //    ListHab.Add(Hab);
            //}
            //return ListHab;
        }

        public static List<HabitacionDisponibilidad> GetHabitacionDisponibilidad(string sViajeId, string sHotelId, string Fecha)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, new Guid(sViajeId)),
                        DBHelper.MakeParam("@HotelID", SqlDbType.UniqueIdentifier, 0, new Guid(sHotelId)),
                        DBHelper.MakeParam("@Fecha", SqlDbType.Date, 0, Convert.ToDateTime(Fecha))
                       
                    };
            List<HabitacionDisponibilidad> ListHab = new List<HabitacionDisponibilidad>();
            
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Habitacion_GetHabitacionDisponibilidad", dbParams))
            {
                while (_reader.Read())
                {
                    HabitacionDisponibilidad Hab = new HabitacionDisponibilidad();
                    Hab.HabitacionID = _reader["HabitacionID"].ToString();
                    Hab.NroHabitacion = Convert.ToInt32(_reader["NroHabitacion"]);
                    Hab.Tipo = Convert.ToInt32(_reader["Tipo"]);
                    Hab.HabitacionTipo = _reader["HabitacionTipo"].ToString();
                    Hab.HabitacionNombre = _reader["HabitacionNombre"].ToString();
                    Hab.HotelNombre = _reader["HotelNombre"].ToString();
                    Hab.Disponibilidad = Convert.ToInt16(_reader["Disponibilidad"]);
                    ListHab.Add(Hab);
                }
            }
            return ListHab;
        }

        public static string[] HotelHabitacionReserva(string sPasajeroID, string sViajeID, string sFecha)
        {
            string[] sResult = new string[3];

            try
            {
                if (sPasajeroID != "" && sFecha != "" && sViajeID != "")
                {
                    SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@PasajeroID", SqlDbType.UniqueIdentifier, 0, new Guid(sPasajeroID)),
                        DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, new Guid(sViajeID)),
                        DBHelper.MakeParam("@Fecha", SqlDbType.Date, 0, Convert.ToDateTime(sFecha)),

                    };
                    using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_ReservaHabitacion_GetByPasajeroID", dbParams))
                    {
                        while (_reader.Read())
                        {
                            sResult[0] = _reader["HabitacionID"].ToString();
                            sResult[1] = _reader["hotelid"].ToString();
                        }
                    }
                    sResult[2] = "Done.";

                }

                return sResult;
            }
            catch (Exception e)
            {
                sResult[0] = "";
                sResult[1] = "Error: " + e.Message + "StackTrace: " + e.StackTrace;
                sResult[2] = "Error.";

                return sResult;
            }

        }
    }
}