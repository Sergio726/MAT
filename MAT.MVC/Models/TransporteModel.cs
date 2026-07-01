using MAT.MVC.Infrastructure.Data;
using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MAT.MVC.Models
{
    public class TransporteModel
    {
        public string TransporteID { get; set; }
        public string NroCoche { get; set; }
        public int MaxPasajeros { get; set; }
        public int KmRecorridos { get; set; }
        public DateTime UltimoService { get; set; }
        public string Matricula { get; set; }
        public string Tipo { get; set; }
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
                    if (HasColumn(_reader, "Tipo"))
                    {
                        Transporte.Tipo = _reader["Tipo"].ToString();
                    }

                    List.Add(Transporte);
                }
            }

            return List;
        }

        public static void InsertTransporte(TransporteFormViewModel model)
        {
            SqlParameter[] dbParams = new SqlParameter[]
            {
                DBHelper.MakeParam("@NroCoche", SqlDbType.VarChar, 50, model.NroCoche ?? (object)DBNull.Value),
                DBHelper.MakeParam("@MaxPasajeros", SqlDbType.Int, 0, model.MaxPasajeros ?? (object)DBNull.Value),
                DBHelper.MakeParam("@KmRecorridos", SqlDbType.Int, 0, DBNull.Value),
                DBHelper.MakeParam("@UltimoService", SqlDbType.Date, 0, DBNull.Value),
                DBHelper.MakeParam("@Matricula", SqlDbType.VarChar, 10, model.Matricula ?? (object)DBNull.Value),
                DBHelper.MakeParam("@Tipo", SqlDbType.NVarChar, 50, model.Tipo ?? (object)DBNull.Value)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Transporte_Insert", dbParams);
        }

        public static TransporteFormViewModel GetFormById(Guid transporteId)
        {
            return MaestrosDataAccess.GetTransporteFormById(transporteId);
        }

        public static MAT.Entities.Transporte GetEntityById(Guid transporteId)
        {
            return MaestrosDataAccess.GetTransporteById(transporteId);
        }

        public static void UpdateTransporte(TransporteFormViewModel model)
        {
            var existing = GetEntityById(model.TransporteId.Value);
            if (existing == null)
            {
                throw new InvalidOperationException("Transporte no encontrado.");
            }

            SqlParameter[] dbParams = new SqlParameter[]
            {
                DBHelper.MakeParam("@TransporteID", SqlDbType.UniqueIdentifier, 0, model.TransporteId.Value),
                DBHelper.MakeParam("@NroCoche", SqlDbType.VarChar, 50, model.NroCoche ?? (object)DBNull.Value),
                DBHelper.MakeParam("@MaxPasajeros", SqlDbType.Int, 0, model.MaxPasajeros ?? (object)DBNull.Value),
                DBHelper.MakeParam("@KmRecorridos", SqlDbType.Int, 0, (object)existing.KmRecorridos ?? DBNull.Value),
                DBHelper.MakeParam("@UltimoService", SqlDbType.Date, 0, existing.UltimoService.HasValue ? (object)existing.UltimoService.Value : DBNull.Value),
                DBHelper.MakeParam("@Matricula", SqlDbType.VarChar, 10, model.Matricula ?? (object)DBNull.Value),
                DBHelper.MakeParam("@Tipo", SqlDbType.NVarChar, 50, model.Tipo ?? (object)DBNull.Value)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Transporte_Update", dbParams);
        }

        public static void DeleteTransporte(Guid transporteId)
        {
            SqlParameter[] dbParams = new SqlParameter[]
            {
                DBHelper.MakeParam("@TransporteID", SqlDbType.UniqueIdentifier, 0, transporteId)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Transporte_Delete", dbParams);
        }

        private static bool HasColumn(IDataRecord reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (string.Equals(reader.GetName(i), columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
