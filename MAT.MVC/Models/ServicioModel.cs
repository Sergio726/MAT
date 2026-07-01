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

        public static List<MAT.Entities.Servicio> GetAllEntities()
        {
            var list = new List<MAT.Entities.Servicio>();
            var ds = ServiciosGetAll();
            if (ds.Tables.Count == 0)
            {
                return list;
            }

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var id = row["ServicioID"].ToString();
                if (!string.IsNullOrEmpty(id))
                {
                    var entity = GetEntityById(new Guid(id));
                    if (entity != null)
                    {
                        list.Add(entity);
                    }
                }
            }

            return list;
        }

        public static MAT.Entities.Servicio GetEntityById(Guid servicioId)
        {
            return Infrastructure.Data.MaestrosDataAccess.GetServicioById(servicioId);
        }

        public static void UpdateServicio(MAT.Entities.Servicio servicio)
        {
            SqlParameter[] dbParams = new SqlParameter[]
            {
                DBHelper.MakeParam("@ServicioID", SqlDbType.UniqueIdentifier, 0, servicio.ServicioId),
                DBHelper.MakeParam("@Descripcion", SqlDbType.VarChar, 250, servicio.Descripcion),
                DBHelper.MakeParam("@Precio", SqlDbType.Float, 0, (object)servicio.Precio ?? DBNull.Value),
                DBHelper.MakeParam("@Moneda", SqlDbType.VarChar, 50, servicio.Moneda ?? (object)DBNull.Value),
                DBHelper.MakeParam("@Iva", SqlDbType.VarChar, 50, servicio.Iva ?? (object)DBNull.Value),
                DBHelper.MakeParam("@Alicuota", SqlDbType.Float, 0, (object)servicio.Alicuota ?? DBNull.Value),
                DBHelper.MakeParam("@Validez", SqlDbType.Date, 0, servicio.Validez.HasValue ? (object)servicio.Validez.Value : DBNull.Value),
                DBHelper.MakeParam("@VisibilidadTarifa", SqlDbType.Int, 0, (object)servicio.VisibilidadTarifa ?? DBNull.Value),
                DBHelper.MakeParam("@ProveedorID", SqlDbType.UniqueIdentifier, 0, (object)servicio.ProveedorId ?? DBNull.Value),
                DBHelper.MakeParam("@TransporteID", SqlDbType.UniqueIdentifier, 0, (object)servicio.TransporteId ?? DBNull.Value),
                DBHelper.MakeParam("@HotelID", SqlDbType.UniqueIdentifier, 0, (object)servicio.HotelId ?? DBNull.Value),
                DBHelper.MakeParam("@TipoServicio", SqlDbType.Int, 0, (object)servicio.TipoServicio ?? DBNull.Value)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Servicio_Update", dbParams);
        }

        public static void DeleteServicio(Guid servicioId)
        {
            SqlParameter[] dbParams = new SqlParameter[]
            {
                DBHelper.MakeParam("@ServicioID", SqlDbType.UniqueIdentifier, 0, servicioId)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Servicio_Delete", dbParams);
        }
    }
}