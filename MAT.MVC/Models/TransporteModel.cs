using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MAT.MVC.Models
{
    public class TransporteModel
    {
        public string TransporteID { get; set; }
        public string NroCoche {get;set;}
        public int MaxPasajeros {get;set;}
        public int KmRecorridos {get;set;}    
        public DateTime UltimoService {get;set;}
        public string Matricula { get; set; }
	
    }

    public class TransporteMethod 
    {
        public static List<TransporteModel> GetListTransporte(string TrasnporteID = "")
        {
            List<TransporteModel> List = new List<TransporteModel>();
           
                SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@TransporteID", SqlDbType.VarChar, 0, TrasnporteID)
                        
                    };
                SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Transporte_GetListTransporte", dbParams);


                while (_reader.Read())
                {
                    TransporteModel Transporte = new TransporteModel();
                    Transporte.TransporteID = _reader["TransporteID"].ToString();
                    Transporte.NroCoche = _reader["NroCoche"].ToString();
                    if (_reader["MaxPasajeros"].ToString() != "")
                    {
                        Transporte.MaxPasajeros = Convert.ToInt32(_reader["MaxPasajeros"]);
                    }
                    if (_reader["KmRecorridos"].ToString() != "")
                    {
                        Transporte.KmRecorridos = Convert.ToInt32(_reader["KmRecorridos"]);
                    }
                    if (_reader["UltimoService"].ToString() != "")
                    {
                        Transporte.UltimoService = Convert.ToDateTime(_reader["UltimoService"]);
                    }
                    Transporte.Matricula = _reader["Matricula"].ToString();

                    List.Add(Transporte);
                }

                return List;
        }
    }
}