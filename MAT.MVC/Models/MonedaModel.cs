using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MAT.MVC.Models
{
    public class MonedaTipo
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
    }
    public static class MonedaMethod
    {
        public static List<MonedaTipo> GetMonedaTipoAll()
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                                               
                    };
            DataSet ds = DBHelper.ExecuteDataSet("dbo.usp_MAT_MonedaTipo_GetAll", dbParams);

            List<MonedaTipo> _list = new List<MonedaTipo>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                MonedaTipo _item = new MonedaTipo();
                _item.Id = Convert.ToInt32(row["Id"]);
                _item.Descripcion = row["Descripcion"].ToString();
                _item.Codigo = row["Codigo"].ToString();
                _list.Add(_item);
            }

            return _list;
        }
    }

}