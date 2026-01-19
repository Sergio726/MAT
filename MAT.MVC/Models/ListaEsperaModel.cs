using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Security.Principal;

namespace MAT.MVC.Models
{
    public class ListaEsperaModel
    {
        public class ListaEspera
        {
            public int ID { get; set; }
            public string ViajeID { get; set; }
            public string ClienteID { get; set; }
            public int UsuarioID { get; set; }
            public DateTime Fecha { get; set; }
            public string Observacion { get; set; }
            public string PasajeroTemporal { get; set; }

        }

        public class ListaEsperaView
        {
            public int ID { get; set; }
            public string ViajeID { get; set; }
            public string ClienteID { get; set; }
            public string Cliente { get; set; }
            public string Vendedor { get; set; }
            public string Fecha { get; set; }
            public string Observacion { get; set; }

        }

        public class ResultSearchCliente
        {
            public string PersonaID { get; set; }
            public string Apellido { get; set; }
            public string Nombre { get; set; }
            public string NroDocumento { get; set; }
        }

        public class Method {

            public static string[] InsertListaEspera(ListaEspera Obj)
            {
                string[] sResult = new string[2];
                
                try
                {
                    List<ReservaStandard> Model = new List<ReservaStandard>();
                    SqlParameter[] dbParams = new SqlParameter[]
                            {
                        DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, new Guid(Obj.ViajeID)),
                        DBHelper.MakeParam("@ClienteID", SqlDbType.VarChar,36,  Obj.ClienteID),
                        DBHelper.MakeParam("@UsuarioID", SqlDbType.Int, 0, Obj.UsuarioID),
                        DBHelper.MakeParam("@Observacion", SqlDbType.VarChar, 300, Obj.Observacion),
                        DBHelper.MakeParam("@PasajeroTemporal", SqlDbType.VarChar, 100, Obj.PasajeroTemporal),
                            };
                    using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_ListaEspera_InsertNew", dbParams))
                    {
                        while (_reader.Read())
                        {
                            sResult[0] = _reader["ID"].ToString();
                            sResult[1] = _reader["Result"].ToString();
                        }
                    }
                }
                catch (Exception e)
                {

                    sResult[0] = "0";
                    sResult[1] = e.Message;
                }
               
                return sResult;
            }

            public static string[] DeleteListaEspera(int ID, int UsuarioID)
            {
                string[] sResult = new string[2];

                try
                {
                    List<ReservaStandard> Model = new List<ReservaStandard>();
                    SqlParameter[] dbParams = new SqlParameter[]
                    {
                        DBHelper.MakeParam("@ID", SqlDbType.Int, 0, ID),
                        DBHelper.MakeParam("@UsuarioID", SqlDbType.Int, 0,UsuarioID)
                    };
                    using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_ListaEspera_Delete_Soft", dbParams))
                    {
                        while (_reader.Read())
                        {
                            // Nuevo contrato SP:
                            // Estado: Done|Error
                            // Mensaje: texto para UI
                            var estado = _reader["Estado"] != null ? _reader["Estado"].ToString() : "";
                            var mensaje = _reader["Mensaje"] != null ? _reader["Mensaje"].ToString() : "";

                            sResult[0] = string.IsNullOrWhiteSpace(estado) ? "Error" : estado.Trim();
                            sResult[1] = mensaje ?? "";
                        }
                    }
                }
                catch (Exception e)
                {

                    sResult[0] = "";
                    sResult[1] = e.Message;
                }

                return sResult;
            }

            public static DataSet GetListaEspera(string ViajeID)
            {
                List<ListaEsperaView> _List = new List<ListaEsperaView>();
                // El SP usa UNIQUEIDENTIFIER: evitar conversiones implícitas (mejor plan/índices)
                SqlParameter[] dbParams = new SqlParameter[] {
                    DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, new Guid(ViajeID)),
                };
                DataSet _ds = DBHelper.ExecuteDataSet("dbo.usp_MAT_ListaEspera_SelectByViajeId_Active", dbParams);

                return _ds;
            }

            public static DataSet GetCountListaEsperaByViajeId(string ViajeID)
            {
                List<ListaEsperaView> _List = new List<ListaEsperaView>();
                SqlParameter[] dbParams = new SqlParameter[] {
                 DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0,new Guid(ViajeID)),
            };
                DataSet _ds = DBHelper.ExecuteDataSet("dbo.usp_MAT_CountListaEsperaByViajeId", dbParams);

                return _ds;
            }

            public static List<ResultSearchCliente> PersonaClienteSearchByNombreDNI(string sParam, string sViajeID)
            {
                List<ResultSearchCliente> _List = new List<ResultSearchCliente>();
                SqlParameter[] dbParams = new SqlParameter[] {
                 DBHelper.MakeParam("@param", SqlDbType.VarChar, 0, sParam),
                 DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, new Guid(sViajeID)),
                };
                using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_ListaEspera_SearchCliente", dbParams))
                {
                    while (_reader.Read())
                    {
                        ResultSearchCliente _item = new ResultSearchCliente();
                        _item.PersonaID = _reader["PersonaID"].ToString();
                        _item.Apellido = _reader["Apellido"].ToString().Trim();
                        _item.Nombre = _reader["Nombre"].ToString().Trim();
                        _item.NroDocumento = _reader["NroDocumento"].ToString();
                        _List.Add(_item);
                    }
                }

                return _List;
            }

            internal static string[] DeleteListaEspera(int v, IIdentity identity)
            {
                throw new NotImplementedException();
            }
        }
    }
}