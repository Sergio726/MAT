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
        public int? Sexo { get; set; }
        public string Nacionalidad { get; set; }
        public string FechaNacimiento { get; set; }
        public bool EsMenorVinculado { get; set; }
        public string ApellidoResponsable { get; set; }
        public string NombreResponsable { get; set; }
        public string NroDocResponsable { get; set; }
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
                    Item.Sexo = _reader["Sexo"] == DBNull.Value ? (int?)null : Convert.ToInt32(_reader["Sexo"]);
                    Item.Nacionalidad = _reader["Nacionalidad"] == DBNull.Value ? "" : _reader["Nacionalidad"].ToString();
                    Item.FechaNacimiento = _reader["FechaNacimiento"].ToString();
                    Item.EsMenorVinculado = Convert.ToBoolean(_reader["EsMenorVinculado"]);
                    Item.ApellidoResponsable = _reader["ApellidoResponsable"] == DBNull.Value ? "" : _reader["ApellidoResponsable"].ToString();
                    Item.NombreResponsable = _reader["NombreResponsable"] == DBNull.Value ? "" : _reader["NombreResponsable"].ToString();
                    Item.NroDocResponsable = _reader["NroDocResponsable"] == DBNull.Value ? "" : _reader["NroDocResponsable"].ToString();

                    ListPasajeroViaje.Add(Item);
                }
            }
            return ListPasajeroViaje;
        }
    }
}
