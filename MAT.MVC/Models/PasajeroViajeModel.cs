using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAT.MVC.Models
{
    public class PasajeroViajeModel
    {
        public string ViajeID { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public int TipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public string CUIT { get; set; }
        public string Telefono { get; set; }
        public string FechaNacimiento { get; set; }
    }

    public class PasajeroViajeMethod
    {
        public static List<PasajeroViajeModel> GetPasajeroViajeByViajeID(string ViajeID)
        {
            List<PasajeroViajeModel> ListPasajeroViaje = new List<PasajeroViajeModel>();

            SqlParameter[] dbParams = new SqlParameter[]
                  {
                        DBHelper.MakeParam("@ViajeID", SqlDbType.VarChar, 0, ViajeID),
                  };
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_PasajeroViaje_GetByViajeID", dbParams))
            {
                while (_reader.Read())
                {
                    PasajeroViajeModel Item = new PasajeroViajeModel();
                    Item.ViajeID = _reader["ViajeID"].ToString();
                    Item.Apellido = _reader["Apellido"].ToString();
                    Item.Nombre = _reader["Nombre"].ToString();
                    Item.TipoDocumento = Convert.ToInt32(_reader["TipoDocumento"]);
                    Item.NroDocumento = _reader["NroDocumento"].ToString();
                    Item.CUIT = _reader["CUIT"].ToString();
                    Item.Telefono = _reader["Telefono"].ToString();
                    Item.FechaNacimiento = _reader["FechaNacimiento"].ToString();

                    ListPasajeroViaje.Add(Item);
                }
            }
            return ListPasajeroViaje;
        }
    }
}
