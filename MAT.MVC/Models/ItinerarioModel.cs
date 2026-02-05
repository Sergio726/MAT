using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MAT.MVC.Models
{
    public class ItinerarioModel
    {
        public string ItinerarioID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class ItinerarioViajeModel
    {
        public string ItinerarioViajeID { get; set; }
        public string ViajeID { get; set; }
        public string ItinerarioID { get; set; }
        public string Nombre { get; set; }
        public string ItinerarioDescripcion { get; set; }
        public int Orden { get; set; }
        public string HoraAprox { get; set; }
        public int? DuracionMin { get; set; }
        public string Observacion { get; set; }
    }

    public static class ItinerarioMethod
    {
        #region Itinerario CRUD

        public static string[] InsertItinerario(string nombre, string descripcion)
        {
            string[] sResult = new string[2];
            try
            {
                SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Nombre", SqlDbType.VarChar, 200, nombre),
                    DBHelper.MakeParam("@Descripcion", SqlDbType.VarChar, 500, descripcion ?? (object)DBNull.Value)
                };

                using (SqlDataReader reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Itinerario_Insert", dbParams))
                {
                    if (reader.Read())
                    {
                        sResult[0] = reader["ItinerarioID"].ToString();
                        sResult[1] = reader["Result"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                sResult[0] = "-1";
                sResult[1] = e.Message;
            }
            return sResult;
        }

        public static string[] UpdateItinerario(string itinerarioId, string nombre, string descripcion)
        {
            string[] sResult = new string[2];
            try
            {
                SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@ItinerarioID", SqlDbType.UniqueIdentifier, 0, new Guid(itinerarioId)),
                    DBHelper.MakeParam("@Nombre", SqlDbType.VarChar, 200, nombre),
                    DBHelper.MakeParam("@Descripcion", SqlDbType.VarChar, 500, descripcion ?? (object)DBNull.Value)
                };

                using (SqlDataReader reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Itinerario_Update", dbParams))
                {
                    if (reader.Read())
                    {
                        sResult[0] = reader["ItinerarioID"].ToString();
                        sResult[1] = reader["Result"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                sResult[0] = "-1";
                sResult[1] = e.Message;
            }
            return sResult;
        }

        public static string[] DeleteItinerario(string itinerarioId)
        {
            string[] sResult = new string[2];
            try
            {
                SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@ItinerarioID", SqlDbType.UniqueIdentifier, 0, new Guid(itinerarioId))
                };

                using (SqlDataReader reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Itinerario_Delete", dbParams))
                {
                    if (reader.Read())
                    {
                        sResult[0] = reader["ItinerarioID"].ToString();
                        sResult[1] = reader["Result"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                sResult[0] = "-1";
                sResult[1] = e.Message;
            }
            return sResult;
        }

        public static List<ItinerarioModel> GetAllItinerarios()
        {
            List<ItinerarioModel> list = new List<ItinerarioModel>();
            try
            {
                SqlParameter[] dbParams = new SqlParameter[] { };

                using (SqlDataReader reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Itinerario_GetAll", dbParams))
                {
                    while (reader.Read())
                    {
                        ItinerarioModel item = new ItinerarioModel
                        {
                            ItinerarioID = reader["ItinerarioID"].ToString(),
                            Nombre = reader["Nombre"].ToString(),
                            Descripcion = reader["Descripcion"].ToString()
                        };
                        list.Add(item);
                    }
                }
            }
            catch
            {
                // Return empty list on error
            }
            return list;
        }

        public static ItinerarioModel GetItinerarioById(string itinerarioId)
        {
            ItinerarioModel item = null;
            try
            {
                SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@ItinerarioID", SqlDbType.UniqueIdentifier, 0, new Guid(itinerarioId))
                };

                using (SqlDataReader reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Itinerario_GetById", dbParams))
                {
                    if (reader.Read())
                    {
                        item = new ItinerarioModel
                        {
                            ItinerarioID = reader["ItinerarioID"].ToString(),
                            Nombre = reader["Nombre"].ToString(),
                            Descripcion = reader["Descripcion"].ToString()
                        };
                    }
                }
            }
            catch
            {
                // Return null on error
            }
            return item;
        }

        #endregion

        #region ItinerarioViaje CRUD

        public static string[] InsertItinerarioViaje(string viajeId, string itinerarioId, int orden, string horaAprox, int? duracionMin, string observacion)
        {
            string[] sResult = new string[2];
            try
            {
                SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, new Guid(viajeId)),
                    DBHelper.MakeParam("@ItinerarioID", SqlDbType.UniqueIdentifier, 0, new Guid(itinerarioId)),
                    DBHelper.MakeParam("@Orden", SqlDbType.Int, 0, orden),
                    DBHelper.MakeParam("@HoraAprox", SqlDbType.VarChar, 10, horaAprox ?? (object)DBNull.Value),
                    DBHelper.MakeParam("@DuracionMin", SqlDbType.Int, 0, duracionMin.HasValue ? (object)duracionMin.Value : DBNull.Value),
                    DBHelper.MakeParam("@Observacion", SqlDbType.VarChar, 200, observacion ?? (object)DBNull.Value)
                };

                using (SqlDataReader reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_ItinerarioViaje_Insert", dbParams))
                {
                    if (reader.Read())
                    {
                        sResult[0] = reader["ItinerarioViajeID"].ToString();
                        sResult[1] = reader["Result"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                sResult[0] = "-1";
                sResult[1] = e.Message;
            }
            return sResult;
        }

        public static string[] UpdateItinerarioViaje(string itinerarioViajeId, string itinerarioId, int? orden, string horaAprox, int? duracionMin, string observacion)
        {
            string[] sResult = new string[2];
            try
            {
                SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@ItinerarioViajeID", SqlDbType.UniqueIdentifier, 0, new Guid(itinerarioViajeId)),
                    DBHelper.MakeParam("@ItinerarioID", SqlDbType.UniqueIdentifier, 0, string.IsNullOrEmpty(itinerarioId) ? DBNull.Value : (object)new Guid(itinerarioId)),
                    DBHelper.MakeParam("@Orden", SqlDbType.Int, 0, orden.HasValue ? (object)orden.Value : DBNull.Value),
                    DBHelper.MakeParam("@HoraAprox", SqlDbType.VarChar, 10, horaAprox ?? (object)DBNull.Value),
                    DBHelper.MakeParam("@DuracionMin", SqlDbType.Int, 0, duracionMin.HasValue ? (object)duracionMin.Value : DBNull.Value),
                    DBHelper.MakeParam("@Observacion", SqlDbType.VarChar, 200, observacion ?? (object)DBNull.Value)
                };

                using (SqlDataReader reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_ItinerarioViaje_Update", dbParams))
                {
                    if (reader.Read())
                    {
                        sResult[0] = reader["ItinerarioViajeID"].ToString();
                        sResult[1] = reader["Result"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                sResult[0] = "-1";
                sResult[1] = e.Message;
            }
            return sResult;
        }

        public static string[] DeleteItinerarioViaje(string itinerarioViajeId)
        {
            string[] sResult = new string[2];
            try
            {
                SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@ItinerarioViajeID", SqlDbType.UniqueIdentifier, 0, new Guid(itinerarioViajeId))
                };

                using (SqlDataReader reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_ItinerarioViaje_Delete", dbParams))
                {
                    if (reader.Read())
                    {
                        sResult[0] = reader["ItinerarioViajeID"].ToString();
                        sResult[1] = reader["Result"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                sResult[0] = "-1";
                sResult[1] = e.Message;
            }
            return sResult;
        }

        public static List<ItinerarioViajeModel> GetItinerarioByViajeID(string viajeId)
        {
            List<ItinerarioViajeModel> list = new List<ItinerarioViajeModel>();
            try
            {
                SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@ViajeID", SqlDbType.VarChar, 0, viajeId)
                };

                using (SqlDataReader reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_ItinerarioViaje_GetByViajeID", dbParams))
                {
                    while (reader.Read())
                    {
                        ItinerarioViajeModel item = new ItinerarioViajeModel
                        {
                            ItinerarioViajeID = reader["ItinerarioViajeID"].ToString(),
                            ViajeID = reader["ViajeID"].ToString(),
                            ItinerarioID = reader["ItinerarioID"].ToString(),
                            Orden = Convert.ToInt32(reader["Orden"]),
                            HoraAprox = reader["HoraAprox"].ToString(),
                            Nombre = reader["Nombre"].ToString(),
                            ItinerarioDescripcion = reader["ItinerarioDescripcion"].ToString(),
                            Observacion = reader["Observacion"].ToString()
                        };

                        if (reader["DuracionMin"] != DBNull.Value)
                        {
                            item.DuracionMin = Convert.ToInt32(reader["DuracionMin"]);
                        }

                        list.Add(item);
                    }
                }
            }
            catch
            {
                // Return empty list on error
            }
            return list;
        }

        public static string[] UpdateOrdenItinerarioViaje(string itinerarioViajeId, int newOrden)
        {
            string[] sResult = new string[2];
            try
            {
                SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@ItinerarioViajeID", SqlDbType.UniqueIdentifier, 0, new Guid(itinerarioViajeId)),
                    DBHelper.MakeParam("@NewOrden", SqlDbType.Int, 0, newOrden)
                };

                using (SqlDataReader reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_ItinerarioViaje_UpdateOrden", dbParams))
                {
                    if (reader.Read())
                    {
                        sResult[0] = reader["ItinerarioViajeID"].ToString();
                        sResult[1] = reader["Result"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                sResult[0] = "-1";
                sResult[1] = e.Message;
            }
            return sResult;
        }

        #endregion
    }
}
