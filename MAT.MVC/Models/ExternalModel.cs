using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MAT.MVC.Models
{
    public class ExternalModel
    {
        public class PaqueteInfo
        {
            public string paqueteid { get; set; }
            public string PaqueteNombre { get; set; }
            public string ViajePrecioCama { get; set; }
            public string ViajePrecioSemiCama { get; set; }
            public string PaqueteServicios { get; set; }
            public string PaqueteDestino { get; set; }
            public string ViajeFechaSalida { get; set; }
            public string ViajeHoraSalida { get; set; }
            public string ViajeFechaRegreso { get; set; }
            public string ViajeHoraRegreso { get; set; }
            public string ViajeMedio { get; set; }
            public string ViajeDescripcion { get; set; }
            public string PaqueteHoteles { get; set; }
            public string PaqueteHotelesId { get; set; }
            public string ViajePrecioPromocional { get; set; }
            public string ViajeVencimientoPromocion { get; set; }
            public string ViajeNroDias { get; set; }
            public string ViajeNroNoches { get; set; }
            public string PaquetePublicWeb { get; set; }
            public string PaqueteModePublicity { get; set; }
            public string PaqueteLastUpdate { get; set; }
            public string PaqueteExcusionesIncluidas { get; set; }
            public string PaqueteExcusionesOpcionales { get; set; }
            public int MonedaTipoId { get; set; }

        }
    }

    public class ExternalMethod
    {
        public static List<MAT.MVC.Models.ExternalModel.PaqueteInfo> GetPackages (string Date1, string Date2, double Price1, double Price2)
        {
            List<MAT.MVC.Models.ExternalModel.PaqueteInfo> listPaquete = new List<ExternalModel.PaqueteInfo>();

            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@Date1", SqlDbType.VarChar, 0, Date1),
                    DBHelper.MakeParam("@Date2", SqlDbType.VarChar, 0, Date2),
                    DBHelper.MakeParam("@Price1", SqlDbType.Float, 0, Price1),
                    DBHelper.MakeParam("@Price2", SqlDbType.Float, 0, Price2)
                };
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_External_Packages_GetAll", dbParams))
            {
                while (_reader.Read())
                {
                    ExternalModel.PaqueteInfo item = new ExternalModel.PaqueteInfo();
                    item.paqueteid = _reader["paqueteid"].ToString();
                    item.PaqueteNombre = _reader["PaqueteNombre"].ToString();
                    item.ViajePrecioSemiCama = _reader["ViajePrecioSemiCama"].ToString();
                    item.ViajePrecioCama = _reader["ViajePrecioCama"].ToString();
                    item.ViajePrecioPromocional = _reader["ViajePrecioPromocional"].ToString();
                    item.PaqueteServicios = _reader["PaqueteServicios"].ToString();
                    item.PaqueteDestino = _reader["PaqueteDestino"].ToString();
                    item.ViajeFechaSalida = _reader["ViajeFechaSalida"].ToString();
                    item.ViajeHoraSalida = _reader["ViajeHoraSalida"].ToString();
                 item.ViajeFechaRegreso = _reader["ViajeFechaRegreso"].ToString();
                 item.ViajeHoraRegreso = _reader["ViajeHoraRegreso"].ToString();
                 item.ViajeMedio = _reader["ViajeMedio"].ToString();
                 item.ViajeDescripcion = _reader["ViajeDescripcion"].ToString();
                 item.PaqueteHoteles = _reader["PaqueteHoteles"].ToString();
                 item.PaqueteHotelesId = _reader["PaqueteHotelesId"].ToString();
                 item.ViajeVencimientoPromocion = _reader["ViajeVencimientoPromocion"].ToString();
                    item.ViajeNroDias = _reader["ViajeNroDias"].ToString();
                    item.ViajeNroNoches = _reader["ViajeNroNoches"].ToString();
                    item.PaquetePublicWeb = _reader["PaquetePublicWeb"].ToString();
                    item.PaqueteModePublicity = _reader["PaqueteModePublicity"].ToString();
                    item.PaqueteLastUpdate = String.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(_reader["PaqueteLastUpdate"]));
                    item.PaqueteExcusionesIncluidas = _reader["PaqueteExcusionesIncluidas"].ToString();
                    item.PaqueteExcusionesOpcionales = _reader["PaqueteExcusionesOpcionales"].ToString();
                    item.MonedaTipoId = Convert.ToInt32(_reader["MonedaTipoId"]);
                    listPaquete.Add(item);
                }
            }
            return listPaquete;
        }
    }

}