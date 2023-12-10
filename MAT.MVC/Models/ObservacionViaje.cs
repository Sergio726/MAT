using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using MAT.Utilities;

namespace MAT.MVC.Models
{
    public class ObservacionViaje
    {

        public class ObsViaje 
        {
            public int? Id { get; set; }
            public string ViajeID { get; set; }
            public string Vendedor { get; set; }
            public string VendedorID { get; set; }
            public DateTime Fecha { get; set; }
            public List<ObsPasajero> Pasajeros { get; set; }
            public string PasajerosID { get; set; }
            public int CategoriaID { get; set; }
            public string Categoria { get; set; }
            public string Detalle { get; set; }
            public DateTime Update { get; set; }
            public string UpdateVendedor { get; set; }
            

        }

        public class ObsPasajero
        {
            public string Pasajero { get; set; }
            public string PasajeroID { get; set; }
        }

        public class ObservacionViajeCategoria
        {
            public int Id { get; set; }
            public string Categoria { get; set; }
        }

        public class Method {

            public static List<ObsViaje> GetObservaciones(string ViajeID)
            {
                List<ObsViaje> List = new List<ObsViaje>();

                SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, new Guid(ViajeID))
                       
                    };
                DataSet ds = DBHelper.ExecuteDataSet("dbo.usp_MAT_ObservacionViaje_GetByViaje", dbParams);

                DataTable dt = ds.Tables[0];

                foreach (DataRow item in dt.Rows)
                {
                    ObsViaje Ob = new ObsViaje();

                    Ob.Id = Convert.ToInt32(item["Id"]);
                    Ob.Vendedor = item["Vendedor"].ToString();
                    Ob.Fecha = Convert.ToDateTime(item["Fecha"]);
                    Ob.Detalle = item["Detalle"].ToString();

                    if (item["Update"].ToString() != "")
                    {
                        Ob.Update = Convert.ToDateTime(item["Update"].ToString());
                    }
                    Ob.UpdateVendedor = item["UpdateVendedor"].ToString();
                    Ob.Fecha = Convert.ToDateTime(item["Fecha"]);
                    Ob.Categoria = item["Categoria"].ToString();

                    //Load Pasajero 
                    List<ObsPasajero> LPasajero = new List<ObsPasajero>();
                    foreach (var ob in item["Pasajeros"].ToString().Split(','))
                    {
                        ObsPasajero Pasajero = new ObsPasajero();
                        if (ob.ToString() != "")
                        {
                            Pasajero.Pasajero = ob.ToString().Split('#')[0];
                            Pasajero.PasajeroID = ob.ToString().Split('#')[1];

                            LPasajero.Add(Pasajero);    
                        }
                        
                    }

                    Ob.Pasajeros = LPasajero;

                    List.Add(Ob);
                }

                return List;
            }

            public static ObsViaje GetObservacion(string sId = "")
            {
                ObsViaje OV = new ObsViaje();
                if (sId != "")
                {
                    SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@Id", SqlDbType.Int, 0, Convert.ToInt32(sId))
                       
                    };
                    DataSet ds = DBHelper.ExecuteDataSet("dbo.usp_MAT_ObservacionViaje_GetById", dbParams);

                    DataTable dt = ds.Tables[0];

                    foreach (DataRow item in dt.Rows)
                    {

                        OV.Id = Convert.ToInt32(item["Id"]);
                        OV.Vendedor = item["Vendedor"].ToString();
                        OV.Fecha = Convert.ToDateTime(item["Fecha"]);
                        OV.Detalle = item["Detalle"].ToString();

                        if (item["Update"].ToString() != "")
                        {
                            OV.Update = Convert.ToDateTime(item["Update"].ToString());
                        }
                        OV.UpdateVendedor = item["UpdateVendedor"].ToString();
                        OV.Fecha = Convert.ToDateTime(item["Fecha"]);
                        OV.Categoria = item["Categoria"].ToString();
                        OV.CategoriaID = Convert.ToInt32(item["CategoriaId"]);
                        //Load Pasajero 
                        List<ObsPasajero> LPasajero = new List<ObsPasajero>();
                        foreach (var ob in item["Pasajeros"].ToString().Split(','))
                        {
                            ObsPasajero Pasajero = new ObsPasajero();
                            if (ob.ToString() != "")
                            {
                                Pasajero.Pasajero = ob.ToString().Split('#')[0];
                                Pasajero.PasajeroID = ob.ToString().Split('#')[1];

                                LPasajero.Add(Pasajero);
                            }

                        }

                        OV.Pasajeros = LPasajero;

                    }
                }

                return OV;
            }

            public static List<ObservacionViajeCategoria> GetObservacionViajeCategoria()
            {
                SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                                              
                    };
               DataSet ds = DBHelper.ExecuteDataSet("dbo.usp_MAT_ObservacionViajeCategoria_GetAll", dbParams);

               List<ObservacionViajeCategoria> lCategorias = new List<ObservacionViajeCategoria>();

               foreach (DataRow item in ds.Tables[0].Rows)
               {
                   ObservacionViajeCategoria obj = new ObservacionViajeCategoria();
                   obj.Id = Convert.ToInt32(item["Id"].ToString());
                   obj.Categoria = item["Categoria"].ToString();
                   lCategorias.Add(obj);
               }

               return lCategorias;
            }

            public static Int32 ObservacionViaje_Insert(MAT.MVC.Models.ObservacionViaje.ObsViaje ObsViaje)
            {
                Int32 ID = new Int32();
                var returnParameter = DBHelper.MakeParamOutput("@ObservacionViajeID", SqlDbType.Int, 0);
                SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, new Guid(ObsViaje.ViajeID)),
                    DBHelper.MakeParam("@VendedorID", SqlDbType.Int, 0,Convert.ToInt32(ObsViaje.VendedorID)),
                    DBHelper.MakeParam("@Fecha", SqlDbType.DateTime, 0,ObsViaje.Fecha),
                    DBHelper.MakeParam("@PasajerosID", SqlDbType.VarChar, 0,ObsViaje.PasajerosID),
                    DBHelper.MakeParam("@Detalle", SqlDbType.VarChar, 0,ObsViaje.Detalle),
                    DBHelper.MakeParam("@CategoriaID", SqlDbType.Int, 0,ObsViaje.CategoriaID),
                    returnParameter
                       
                };
             
                DBHelper.ExecuteNonQuery("dbo.usp_MAT_ObservacionViaje_Insert", dbParams);

                ID = Convert.ToInt32(returnParameter.Value);
                return ID;
                                                             

            }

            public static void ObservacionViaje_Update(MAT.MVC.Models.ObservacionViaje.ObsViaje ObsViaje)
            {
                SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@ObservacionViajeID", SqlDbType.Int, 0, ObsViaje.Id),
                    DBHelper.MakeParam("@VendedorID", SqlDbType.Int, 0,Convert.ToInt32(ObsViaje.VendedorID)),
                    DBHelper.MakeParam("@PasajerosID", SqlDbType.VarChar, 0,ObsViaje.PasajerosID),
                    DBHelper.MakeParam("@Detalle", SqlDbType.VarChar, 0,ObsViaje.Detalle),
                    DBHelper.MakeParam("@CategoriaID", SqlDbType.Int, 0,ObsViaje.CategoriaID),
                       
                };

                DBHelper.ExecuteNonQuery("dbo.usp_MAT_ObservacionViaje_Update", dbParams);
                               

            }

            public static string ObservacionViaje_Delete(Int32 Id, Int32 VendedorId)
            {
                string sReturn = "";
                SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@ObservacionViajeID", SqlDbType.Int, 0, Id),
                    DBHelper.MakeParam("@VendedorID", SqlDbType.Int, 0,VendedorId)
                };

                DataSet ds = DBHelper.ExecuteDataSet("dbo.usp_MAT_ObservacionViaje_Delete", dbParams);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    sReturn = dr["msj"].ToString();
                }

                return sReturn;

            }

        }
    }
}