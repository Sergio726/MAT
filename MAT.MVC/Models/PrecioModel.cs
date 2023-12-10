using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MAT.MVC.Models
{
    public class PrecioModel
    {
       public Guid PrecioID {get;set;}  
       public decimal Monto {get;set;} 
       public DateTime Vigencia {get;set;}
       public string Descripcion {get;set;}
       public string Mes {get;set;}
       public string DescripcionVoucher { get; set; }
    
    }

    public static class PrecioMethod
    {
        public static DataSet GetAll()
        {
            DataSet ds = new DataSet();
            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    
                };
            ds = DBHelper.ExecuteDataSet("dbo.usp_MAT_Precio_GetAll", dbParams);
            return ds;
        }

        public static DataSet GetById(Guid PrecioID)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@PrecioID", SqlDbType.UniqueIdentifier, 0, PrecioID)
                };
            return DBHelper.ExecuteDataSet("dbo.usp_Precio_GetById", dbParams);
        }

        public static void Insert(PrecioModel Precio)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@Monto", SqlDbType.Float, 53, Precio.Monto),
                    DBHelper.MakeParam("@Vigencia", SqlDbType.DateTime, 0, (Precio.Vigencia.ToShortDateString() == "1/1/0001" ? null : Precio.Vigencia.ToString())),
                    DBHelper.MakeParam("@Descripcion", SqlDbType.VarChar, 100, Precio.Descripcion),
                    DBHelper.MakeParam("@Mes", SqlDbType.VarChar, 100, Precio.Mes),
                    DBHelper.MakeParam("@DescripcionVoucher", SqlDbType.VarChar, 500, Precio.DescripcionVoucher)
                };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Precio_Insert", dbParams);
        }

        public static void Delete(Guid PrecioID)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@PrecioID", SqlDbType.UniqueIdentifier, 0, PrecioID)
                };
            DBHelper.ExecuteNonQuery("dbo.usp_Precio_Delete", dbParams);
        }

        public static void Update(PrecioModel Precio)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@PrecioID", SqlDbType.UniqueIdentifier, 0, Precio.PrecioID),
                    DBHelper.MakeParam("@Monto", SqlDbType.Float, 53, Precio.Monto),
                    DBHelper.MakeParam("@Vigencia", SqlDbType.DateTime, 0, (Precio.Vigencia.ToShortDateString() == "1/1/0001" ? null : Precio.Vigencia.ToString())),
                    DBHelper.MakeParam("@Descripcion", SqlDbType.VarChar, 100, Precio.Descripcion),
                    DBHelper.MakeParam("@Mes", SqlDbType.VarChar, 100, Precio.Mes),
                    DBHelper.MakeParam("@DescripcionVoucher", SqlDbType.VarChar, 500, Precio.DescripcionVoucher)
                };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Precio_Update", dbParams);
        }
    }
}