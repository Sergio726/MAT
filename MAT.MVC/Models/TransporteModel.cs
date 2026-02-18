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
                using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Transporte_GetListTransporte", dbParams))
                {
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
                }

                return List;
        }

        /// <summary>
        /// Inserta un nuevo transporte en la base de datos.
        /// </summary>
        public static void InsertTransporte(string nroCoche, int maxPasajeros, int kmRecorridos, DateTime? ultimoService, string matricula)
        {
            SqlParameter[] dbParams = new SqlParameter[]
            {
                DBHelper.MakeParam("@NroCoche", SqlDbType.VarChar, 50, nroCoche ?? (object)DBNull.Value),
                DBHelper.MakeParam("@MaxPasajeros", SqlDbType.Int, 0, maxPasajeros),
                DBHelper.MakeParam("@KmRecorridos", SqlDbType.Int, 0, kmRecorridos),
                DBHelper.MakeParam("@UltimoService", SqlDbType.Date, 0, ultimoService.HasValue ? (object)ultimoService.Value : DBNull.Value),
                DBHelper.MakeParam("@Matricula", SqlDbType.VarChar, 10, matricula ?? (object)DBNull.Value)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Transporte_Insert", dbParams);
        }
    }
}