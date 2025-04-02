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

        public static SqlDataReader GetByIds(List<Guid> precioIds)
        {
            // Contar la cantidad de veces que aparece cada precioId
            var cantidadPorId = precioIds
                .GroupBy(id => id)
                .Select(g => new { Id = g.Key, Cantidad = g.Count() })
                .ToList();

            // Crear un DataTable para pasar los datos como parámetro estructurado
            DataTable precioIdsTable = new DataTable();
            precioIdsTable.Columns.Add("Id", typeof(Guid));
            precioIdsTable.Columns.Add("Cantidad", typeof(int));

            foreach (var item in cantidadPorId)
            {
                precioIdsTable.Rows.Add(item.Id, item.Cantidad);
            }

            // Crear el parámetro para SQL Server
            SqlParameter[] dbParams = new SqlParameter[]
            {
        new SqlParameter
        {
            ParameterName = "@PrecioIDs",
            SqlDbType = SqlDbType.Structured,
            TypeName = "dbo.tvp_PrecioIDTableType", // Debe coincidir con el tipo de tabla en SQL Server
            Value = precioIdsTable
        }
            };

            // Ejecutar la consulta
            return DBHelper.ExecuteDataReader("dbo.usp_MAT_Precio_GetByIds", dbParams);
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