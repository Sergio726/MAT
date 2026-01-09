using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MAT.MVC.Models
{
    public class HabitacionTipo
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int CapacidadNormal { get; set; }
    }

    public class HabitacionTipoMethod
    {

        public static List<HabitacionTipo> GetAllHabitacionTipo()
        {
            List<HabitacionTipo> ListHabTipo = new List<HabitacionTipo>();

            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                };
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_HabitacionTipo_GetAll", dbParams))
            {
                while (_reader.Read())
                {
                    HabitacionTipo Item = new HabitacionTipo();
                    Item.Id = Convert.ToInt32(_reader["Id"].ToString());
                    Item.Descripcion = _reader["Descripcion"].ToString();
                    Item.CapacidadNormal = Convert.ToInt32(_reader["CapacidadNormal"]);
                    ListHabTipo.Add(Item);
                }
            }

            return ListHabTipo;
        }

        public static HabitacionTipo GetAllHabitacionTipoByTipoId(int idTipoId)
        {
            HabitacionTipo HabTipo = new HabitacionTipo();

            SqlParameter[] dbParams = new SqlParameter[]
                {               
                    DBHelper.MakeParam("@TipoId", SqlDbType.Int, 0, idTipoId)
                };
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_HabitacionTipo_GetByTipoId", dbParams))
            {
                if (_reader.Read())
                {
                    HabTipo.Id = Convert.ToInt32(_reader["Id"].ToString());
                    HabTipo.Descripcion = _reader["Descripcion"].ToString();
                }
            }

            return HabTipo;
        }
    }
}