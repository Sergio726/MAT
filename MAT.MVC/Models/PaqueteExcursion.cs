using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MAT.MVC.Models
{
    public class PaqueteExcursionCustomModel
    {
        public Guid PaqueteExcursionID { get; set; }
        public Guid ExcursionID { get; set; }
        public Guid PaqueteID { get; set; }
        public bool IsOpcional { get; set; }
        public string Descripcion { get; set; }
    }

    public static class PaqueteExcursionMethod
    {
        public static void InsertNew(PaqueteExcursionCustomModel o)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@ExcursionID", SqlDbType.UniqueIdentifier, 0, o.ExcursionID),
                    DBHelper.MakeParam("@PaqueteID", SqlDbType.UniqueIdentifier, 0, o.PaqueteID),
                    DBHelper.MakeParam("@IsOpcional", SqlDbType.Bit, 0, o.IsOpcional)
                };
            DBHelper.ExecuteNonQuery("dbo.ups_MAT_PaqueteExcursion_InsertNew", dbParams);
            
        }

        public static DataSet GetByPaqueteID(Guid PaqueteID)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {   DBHelper.MakeParam("@PaqueteID", SqlDbType.UniqueIdentifier, 0, PaqueteID),
                };
            return DBHelper.ExecuteDataSet("dbo.ups_MAT_PaqueteExcursion_GetByPaqueteID", dbParams);

        }

        public static void Delete(Guid PaqueteExcursionID)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {   DBHelper.MakeParam("@PaqueteExcursionID", SqlDbType.UniqueIdentifier, 0, PaqueteExcursionID),
                };
            DBHelper.ExecuteNonQuery("dbo.ups_MAT_PaqueteExcursion_Delete", dbParams);

        }
    }
}