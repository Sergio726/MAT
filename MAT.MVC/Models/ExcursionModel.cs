using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using MAT.Utilities;

namespace MAT.MVC.Models
{
    public class ExcursionModel
    {
        public Guid ExcursionID { get; set; }
        public string Descripcion { get; set; }
    }

    public static class ExcursionMethod {
        public static DataSet GetToAdd(Guid PaqueteID)
        {
            DataSet ds = new DataSet();
            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@PaqueteID", SqlDbType.UniqueIdentifier, 0, PaqueteID)
                };
            ds = DBHelper.ExecuteDataSet("dbo.usp_MAT_Excursiones_GetToAdd", dbParams);
            return ds;
        }
    }
}