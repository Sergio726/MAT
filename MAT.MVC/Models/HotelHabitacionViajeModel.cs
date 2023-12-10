using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MAT.MVC.Models
{
    public class HotelHabitacionViajeModel
    {
        public int Id { get; set; }
        public string HotelID { get; set; }
        public string HabitacionID { get; set; }
        public string ViajeID { get; set; }
        public DateTime Fecha { get; set; }
        public string ViajeNombre { get; set; }
        public string HotelNombre { get; set; }
        public string Comentario { get; set; }
    }

    public class HotelHabitacionViajMethod
    {
        public static HotelHabitacionViajeModel GetEncabezado(Guid ViajeHotelID)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {             
                          DBHelper.MakeParam("@ViajeHotelID", SqlDbType.UniqueIdentifier, 0, ViajeHotelID)
                    };
            SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Viaje_ViajeHotel_GetById", dbParams);

            HotelHabitacionViajeModel item = new HotelHabitacionViajeModel();
            while (_reader.Read())
            {
                item.HotelID = _reader["HotelID"].ToString();
                item.ViajeID = _reader["ViajeID"].ToString();
                item.ViajeNombre = _reader["ViajeNombre"].ToString();
                item.HotelNombre = _reader["HotelNombre"].ToString();
                item.Fecha = Convert.ToDateTime(_reader["Desde"]);
                item.Comentario = _reader["Comentario"].ToString();
            }
            return item;
        }

        public static void Insert(Guid HotelID, Guid ViajeID, DateTime Fecha, string sTipo)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {             
                          DBHelper.MakeParam("@HotelID", SqlDbType.UniqueIdentifier, 0, HotelID),
                          DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, ViajeID),
                          DBHelper.MakeParam("@Fecha", SqlDbType.Date, 0, Fecha),
                          DBHelper.MakeTableParam("@Tipo",TableDataType.tvp_int,sTipo)
                    };
            DBHelper.ExecuteNonQuery("dbo.usp_TransHotelHabitacionViaje_Insert", dbParams);
        }

        public static void InsertByPlantilla(Guid HotelID, Guid ViajeID, DateTime Fecha, int iPlantillaId)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {             
                          DBHelper.MakeParam("@HotelID", SqlDbType.UniqueIdentifier, 0, HotelID),
                          DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, ViajeID),
                          DBHelper.MakeParam("@Fecha", SqlDbType.Date, 0, Fecha),
                          DBHelper.MakeParam("@PlantillaId", SqlDbType.Int, 0, iPlantillaId),
                    };
            DBHelper.ExecuteNonQuery("dbo.usp_TransHotelHabitacionViaje_InsertByPlantilla", dbParams);
        }

        public static DataSet GetDistribucionHab(Guid ViajeID, Guid HotelID, string Fecha)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {             
                          DBHelper.MakeParam("@HotelID", SqlDbType.UniqueIdentifier, 0, HotelID),
                          DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, ViajeID),
                          DBHelper.MakeParam("@Fecha", SqlDbType.Date, 0, Fecha)
                    };
            return DBHelper.ExecuteDataSet("dbo.usp_TransHotelHabitacionViaje_GetDistribucionHab", dbParams);
        }

        public static int Delete(int Id)
        {
            SqlParameter[] dbParams = new SqlParameter[] {
                  DBHelper.MakeParam("@TransHotelHabitacionViajeID", SqlDbType.Int, 50, Id)
            };
            return DBHelper.ExecuteNonQueryOutput("dbo.usp_MAT_TransHotelHabitacionViaje_Delete", dbParams, "@Status", SqlDbType.Int, 0);

        }

        public static int CreatePlantilla(string Nombre, Guid ViajeID, Guid HotelID)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {             
                          DBHelper.MakeParam("@Nombre", SqlDbType.VarChar, 50, Nombre),
                          DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, ViajeID),
                          DBHelper.MakeParam("@HotelID", SqlDbType.UniqueIdentifier, 0, HotelID)
                    };
            return DBHelper.ExecuteNonQueryOutput("dbo.usp_MAT_PlantillaDistribucion_Create", dbParams, "@Status", SqlDbType.Int, 0);

        }

        public static void deletePlantilla(int PlantillaId)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {             
                          DBHelper.MakeParam("@PlantillaId", SqlDbType.Int, 0, PlantillaId)
                    };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_PlantillaDistribucion_Delete", dbParams);

        }

        public static DataSet GetPlantillasDistribucion()
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {             
                    };
            return DBHelper.ExecuteDataSet("dbo.usp_MAT_PlantillaDistribucionHabitacion_GetAll", dbParams);
        }
    }
}