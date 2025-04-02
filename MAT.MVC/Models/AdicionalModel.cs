using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAT.MVC.Models
{
    public static class AdicionalMethod
    {
        //
        // GET: /AdicionalModel/

        public static SqlDataReader GetByIds(List<Guid> adicionalIds)
        {
            // Contar la cantidad de veces que aparece cada adicionalId
            var cantidadPorId = adicionalIds
                .GroupBy(id => id)
                .Select(g => new { Id = g.Key, Cantidad = g.Count() })
                .ToList();

            // Crear un DataTable para pasar los datos como parámetro estructurado
            DataTable adicionalIdsTable = new DataTable();
            adicionalIdsTable.Columns.Add("Id", typeof(Guid));
            adicionalIdsTable.Columns.Add("Cantidad", typeof(int));

            foreach (var item in cantidadPorId)
            {
                adicionalIdsTable.Rows.Add(item.Id, item.Cantidad);
            }

            // Crear el parámetro para SQL Server
            SqlParameter[] dbParams = new SqlParameter[]
            {
                new SqlParameter
                {
                    ParameterName = "@AdicionalIDs",
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.tvp_AdicionalIDTableType", // Debe coincidir con el tipo de tabla en SQL Server
                    Value = adicionalIdsTable
                }
            };

            // Ejecutar la consulta
            return DBHelper.ExecuteDataReader("dbo.usp_MAT_Adicional_GetByIds", dbParams);
        }


    }
}
