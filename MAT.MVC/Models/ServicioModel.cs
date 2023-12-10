using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MAT.MVC.Models
{
    public class ServicioModel
    {
        public string ServicioID { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Moneda { get; set; }
        public string Iva { get; set; }
        public double Alicuota { get; set; }
        public string Validez { get; set; }
        public int VisibilidadTarifa { get; set; }
        public string ProveedorID { get; set; }
        public string TransporteID { get; set; }
        public string HotelID { get; set; }
        public int TipoServicio { get; set; }
    }

    public class ServicioMethod
    {
        public static void ServicioInsert(ServicioModel servicio) {
            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@Descripcion", SqlDbType.VarChar, 0, servicio.Descripcion),
                        DBHelper.MakeParam("@Precio", SqlDbType.Float, 0, servicio.Precio),
                        DBHelper.MakeParam("@Moneda", SqlDbType.VarChar, 0, servicio.Moneda),
                        DBHelper.MakeParam("@Iva", SqlDbType.VarChar, 0, servicio.Iva),
                        DBHelper.MakeParam("@Alicuota", SqlDbType.Float, 0, servicio.Alicuota),
                        DBHelper.MakeParam("@Validez", SqlDbType.Date, 0, servicio.Validez),
                        DBHelper.MakeParam("@VisibilidadTarifa", SqlDbType.Int, 0, servicio.VisibilidadTarifa),
                        DBHelper.MakeParam("@ProveedorID", SqlDbType.VarChar, 0, servicio.ProveedorID),
                        DBHelper.MakeParam("@TransporteID", SqlDbType.VarChar, 0, servicio.TransporteID),
                        DBHelper.MakeParam("@HotelID", SqlDbType.VarChar, 0, servicio.HotelID),
                        DBHelper.MakeParam("@TipoServicio", SqlDbType.VarChar, 0, servicio.TipoServicio)
                        
                    };
            DBHelper.ExecuteNonQuery("usp_MAT_Servicio_Insert", dbParams);

        }

        public static DataSet ServiciosGetAll()
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                    };
            return DBHelper.ExecuteDataSet("dbo.usp_MAT_Servicios_GetAll", dbParams);

        }
    }
}