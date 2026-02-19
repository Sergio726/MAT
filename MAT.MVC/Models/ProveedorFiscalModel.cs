using MAT.Utilities;
using MAT.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MAT.MVC.Models
{
    public class ProveedorFiscalStandard
    {
        public Guid ProveedorID { get; set; }
        public string RazonSocial { get; set; }
        public string Telefono { get; set; }
        public string Fax { get; set; }
        public string Web { get; set; }
        public string Email { get; set; }
        public string Idioma { get; set; }
        public int? CondicionIva { get; set; }
        public string Cuit { get; set; }
        public int? FormaPago { get; set; }
        public int? LocalidadID { get; set; }
        public string Domicilio { get; set; }
        public int Estado { get; set; }
    }

    public class ProveedorFiscalMethod
    {
        public static List<ProveedorFiscalStandard> GetAll(int? estado = null)
        {
            var lista = new List<ProveedorFiscalStandard>();
            SqlParameter[] dbParams = new SqlParameter[]
            {
                DBHelper.MakeParam("@Estado", SqlDbType.Int, 0, estado.HasValue ? (object)estado.Value : DBNull.Value)
            };
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Proveedor_GetAll_Fiscal", dbParams))
            {
                while (_reader.Read())
                {
                    var p = new ProveedorFiscalStandard();
                    p.ProveedorID = new Guid(_reader["ProveedorID"].ToString());
                    p.RazonSocial = _reader["RazonSocial"]?.ToString() ?? string.Empty;
                    p.Telefono = _reader["Telefono"]?.ToString() ?? string.Empty;
                    p.Fax = _reader["Fax"]?.ToString() ?? string.Empty;
                    p.Web = _reader["Web"]?.ToString() ?? string.Empty;
                    p.Email = _reader["Email"]?.ToString() ?? string.Empty;
                    p.Idioma = _reader["Idioma"]?.ToString() ?? string.Empty;
                    p.CondicionIva = _reader["CondicionIva"] != DBNull.Value ? Convert.ToInt32(_reader["CondicionIva"]) : (int?)null;
                    p.Cuit = _reader["Cuit"]?.ToString() ?? string.Empty;
                    p.FormaPago = _reader["FormaPago"] != DBNull.Value ? Convert.ToInt32(_reader["FormaPago"]) : (int?)null;
                    p.LocalidadID = _reader["LocalidadID"] != DBNull.Value ? Convert.ToInt32(_reader["LocalidadID"]) : (int?)null;
                    p.Domicilio = _reader["Domicilio"]?.ToString() ?? string.Empty;
                    p.Estado = _reader["Estado"] != DBNull.Value ? Convert.ToInt32(_reader["Estado"]) : 1;
                    lista.Add(p);
                }
            }
            return lista;
        }

        public static ProveedorFiscalStandard GetById(Guid proveedorId)
        {
            ProveedorFiscalStandard prov = null;
            SqlParameter[] dbParams = new SqlParameter[]
            {
                DBHelper.MakeParam("@ProveedorID", SqlDbType.UniqueIdentifier, 0, proveedorId)
            };
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Proveedor_GetById_Fiscal", dbParams))
            {
                if (_reader.Read())
                {
                    prov = new ProveedorFiscalStandard();
                    prov.ProveedorID = new Guid(_reader["ProveedorID"].ToString());
                    prov.RazonSocial = _reader["RazonSocial"]?.ToString() ?? string.Empty;
                    prov.Telefono = _reader["Telefono"]?.ToString() ?? string.Empty;
                    prov.Fax = _reader["Fax"]?.ToString() ?? string.Empty;
                    prov.Web = _reader["Web"]?.ToString() ?? string.Empty;
                    prov.Email = _reader["Email"]?.ToString() ?? string.Empty;
                    prov.Idioma = _reader["Idioma"]?.ToString() ?? string.Empty;
                    prov.CondicionIva = _reader["CondicionIva"] != DBNull.Value ? Convert.ToInt32(_reader["CondicionIva"]) : (int?)null;
                    prov.Cuit = _reader["Cuit"]?.ToString() ?? string.Empty;
                    prov.FormaPago = _reader["FormaPago"] != DBNull.Value ? Convert.ToInt32(_reader["FormaPago"]) : (int?)null;
                    prov.LocalidadID = _reader["LocalidadID"] != DBNull.Value ? Convert.ToInt32(_reader["LocalidadID"]) : (int?)null;
                    prov.Domicilio = _reader["Domicilio"]?.ToString() ?? string.Empty;
                    prov.Estado = _reader["Estado"] != DBNull.Value ? Convert.ToInt32(_reader["Estado"]) : 1;
                }
            }
            return prov;
        }

        public static void Insert(ProveedorFiscalStandard prov)
        {
            SqlParameter[] dbParams = new SqlParameter[]
            {
                DBHelper.MakeParam("@ProveedorID", SqlDbType.UniqueIdentifier, 0, prov.ProveedorID),
                DBHelper.MakeParam("@RazonSocial", SqlDbType.VarChar, 50, prov.RazonSocial ?? string.Empty),
                DBHelper.MakeParam("@Cuit", SqlDbType.VarChar, 50, prov.Cuit ?? string.Empty),
                DBHelper.MakeParam("@CondicionIva", SqlDbType.Int, 0, prov.CondicionIva.HasValue ? (object)prov.CondicionIva.Value : DBNull.Value),
                DBHelper.MakeParam("@Domicilio", SqlDbType.VarChar, 200, string.IsNullOrEmpty(prov.Domicilio) ? (object)DBNull.Value : prov.Domicilio),
                DBHelper.MakeParam("@Email", SqlDbType.VarChar, 50, string.IsNullOrEmpty(prov.Email) ? (object)DBNull.Value : prov.Email),
                DBHelper.MakeParam("@Telefono", SqlDbType.VarChar, 50, string.IsNullOrEmpty(prov.Telefono) ? (object)DBNull.Value : prov.Telefono),
                DBHelper.MakeParam("@Estado", SqlDbType.Int, 0, prov.Estado)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Proveedor_Insert_Fiscal", dbParams);
        }

        public static void Update(ProveedorFiscalStandard prov)
        {
            SqlParameter[] dbParams = new SqlParameter[]
            {
                DBHelper.MakeParam("@ProveedorID", SqlDbType.UniqueIdentifier, 0, prov.ProveedorID),
                DBHelper.MakeParam("@RazonSocial", SqlDbType.VarChar, 50, prov.RazonSocial ?? string.Empty),
                DBHelper.MakeParam("@Cuit", SqlDbType.VarChar, 50, prov.Cuit ?? string.Empty),
                DBHelper.MakeParam("@CondicionIva", SqlDbType.Int, 0, prov.CondicionIva.HasValue ? (object)prov.CondicionIva.Value : DBNull.Value),
                DBHelper.MakeParam("@Domicilio", SqlDbType.VarChar, 200, string.IsNullOrEmpty(prov.Domicilio) ? (object)DBNull.Value : prov.Domicilio),
                DBHelper.MakeParam("@Email", SqlDbType.VarChar, 50, string.IsNullOrEmpty(prov.Email) ? (object)DBNull.Value : prov.Email),
                DBHelper.MakeParam("@Telefono", SqlDbType.VarChar, 50, string.IsNullOrEmpty(prov.Telefono) ? (object)DBNull.Value : prov.Telefono),
                DBHelper.MakeParam("@Estado", SqlDbType.Int, 0, prov.Estado),
                DBHelper.MakeParam("@Web", SqlDbType.VarChar, 50, string.IsNullOrEmpty(prov.Web) ? (object)DBNull.Value : prov.Web),
                DBHelper.MakeParam("@Fax", SqlDbType.VarChar, 50, string.IsNullOrEmpty(prov.Fax) ? (object)DBNull.Value : prov.Fax)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Proveedor_Update_Fiscal", dbParams);
        }
    }
}
