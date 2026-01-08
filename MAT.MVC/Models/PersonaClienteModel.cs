using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MAT.MVC.Models
{
    public class PersonaClienteModel
    {
        public string PersonaId { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string NroDocumento { get; set; }
        public int TipoDocumento { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public int LocalidadID { get; set; }
        public string LocalidadNombre { get; set; }
        public string Email { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Domicilio { get; set; }
        public int Sexo { get; set; }
        public string RazonSocial { get; set; }
        public string Nacionalidad { get; set; }
        public string PaisResidencia { get; set; }
        public int Provincia { get; set; }
        public string Cuit { get; set; }
        public string Empresa { get; set; }
        public string Ocupacion { get; set; }
        public int FormaPago { get; set; }
        public int CondicionIva { get; set; }
        public string VendedorID { get; set; }
        public string Fax { get; set; }
        public string Web { get; set; }
        public string Idioma { get; set; }
        public string Promotor { get; set; }
        public string Observacion { get; set; }
        public int TipoID { get; set; }
        public byte IsTituarFactura { get; set; }

    }

    public class PersonaCliente_CreditoClienteModel 
    {
        public Guid ClienteID { get; set; }
        public string FullName { get; set; }
        public decimal Monto { get; set; }
    }

    public class PersonaClienteMethod
    {
        public static void CreatePersonaCliente(PersonaClienteModel model)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                   {
                        DBHelper.MakeParam("@Apellido", SqlDbType.VarChar, 0, model.Apellido),
                        DBHelper.MakeParam("@Nombre", SqlDbType.VarChar, 0, model.Nombre),
                        DBHelper.MakeParam("@NroDocumento", SqlDbType.VarChar, 0, model.NroDocumento),
                        DBHelper.MakeParam("@TipoDocumento", SqlDbType.Int, 0, model.TipoDocumento),
                        DBHelper.MakeParam("@Celular", SqlDbType.VarChar, 0, model.Celular),
                        DBHelper.MakeParam("@Telefono", SqlDbType.VarChar, 0, model.Telefono),
                        DBHelper.MakeParam("@Email", SqlDbType.VarChar, 0, model.Email),
                        DBHelper.MakeParam("@FechaNacimiento", SqlDbType.Date, 0, model.FechaNacimiento.ToShortDateString()),
                        DBHelper.MakeParam("@LocalidadID", SqlDbType.Int, 0, model.LocalidadID),
                        DBHelper.MakeParam("@Domicilio", SqlDbType.VarChar, 0, model.Domicilio),
                        DBHelper.MakeParam("@Sexo", SqlDbType.Int, 0, model.Sexo),
                        DBHelper.MakeParam("@Nacionalidad", SqlDbType.VarChar, 0, model.Nacionalidad),
                        DBHelper.MakeParam("@PaisResidencia", SqlDbType.VarChar, 0, model.PaisResidencia),
                        DBHelper.MakeParam("@Provincia", SqlDbType.Int, 0, model.Provincia),
                        DBHelper.MakeParam("@RazonSocial", SqlDbType.VarChar, 0, model.RazonSocial),
                        DBHelper.MakeParam("@Cuit", SqlDbType.VarChar, 0, model.Cuit),
                        DBHelper.MakeParam("@Empresa", SqlDbType.VarChar, 0, model.Empresa),
                        DBHelper.MakeParam("@Ocupacion", SqlDbType.VarChar, 0, model.Ocupacion),
                        DBHelper.MakeParam("@FormaPago", SqlDbType.Int, 0, model.FormaPago),
                        DBHelper.MakeParam("@CondicionIva", SqlDbType.Int, 0, model.CondicionIva),
                        DBHelper.MakeParam("@VendedorID", SqlDbType.UniqueIdentifier, 0,new Guid(model.VendedorID)),
                        DBHelper.MakeParam("@Fax", SqlDbType.VarChar, 0, model.Fax),
                        DBHelper.MakeParam("@Web", SqlDbType.VarChar, 0, model.Web),
                        DBHelper.MakeParam("@Idioma", SqlDbType.VarChar, 0, model.Idioma),
                        DBHelper.MakeParam("@Promotor", SqlDbType.VarChar, 0, model.Promotor),
                        DBHelper.MakeParam("@Observacion", SqlDbType.VarChar, 0, model.Observacion),
                        DBHelper.MakeParam("@TipoID", SqlDbType.Int, 0, model.TipoID)

                    };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_PersonaCliente_Create", dbParams);

        }

        public static List<PersonaClienteModel> PersonaClienteGetAll()
        {
            List<PersonaClienteModel> _List = new List<PersonaClienteModel>();
            SqlParameter[] dbParams = new SqlParameter[]{};
            SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_PersonaCliente_GetAll", dbParams);


            while (_reader.Read())
            {
                PersonaClienteModel _item = new PersonaClienteModel();
                _item.PersonaId = _reader["PersonaID"].ToString();
                _item.Apellido = _reader["Apellido"].ToString().Trim();
                _item.Nombre = _reader["Nombre"].ToString().Trim();
                _item.NroDocumento = _reader["NroDocumento"].ToString();
                _item.Telefono = _reader["Telefono"].ToString();
                _item.Celular = _reader["Celular"].ToString();
                _item.LocalidadNombre = _reader["LocalidadNombre"].ToString();
                _item.Nacionalidad = _reader["Nacionalidad"].ToString();
                _item.PaisResidencia = _reader["PaisResidencia"].ToString();
                _item.IsTituarFactura = Convert.ToByte(_reader["IsTituarFactura"]);
                _List.Add(_item);
            }

            return _List;
        }

        /// <summary>
        /// Obtiene los TOP N clientes con búsqueda optimizada
        /// </summary>
        /// <param name="searchTerm">Término de búsqueda (puede estar vacío)</param>
        /// <param name="topCount">Cantidad de resultados a devolver (default: 10, máximo: 100)</param>
        /// <returns>Lista de clientes</returns>
        public static List<PersonaClienteModel> PersonaClienteGetTop(string searchTerm = "", int topCount = 10)
        {
            List<PersonaClienteModel> _List = new List<PersonaClienteModel>();
            
            // Validar topCount
            if (topCount <= 0 || topCount > 100)
                topCount = 10;

            SqlParameter[] dbParams = new SqlParameter[]
            {
                DBHelper.MakeParam("@SearchTerm", SqlDbType.NVarChar, 200, searchTerm ?? ""),
                DBHelper.MakeParam("@TopCount", SqlDbType.Int, 0, topCount)
            };
            
            SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_PersonaCliente_GetTop", dbParams);

            while (_reader.Read())
            {
                PersonaClienteModel _item = new PersonaClienteModel();
                _item.PersonaId = _reader["PersonaID"].ToString();
                _item.Apellido = _reader["Apellido"] != DBNull.Value ? _reader["Apellido"].ToString().Trim() : "";
                _item.Nombre = _reader["Nombre"] != DBNull.Value ? _reader["Nombre"].ToString().Trim() : "";
                _item.NroDocumento = _reader["NroDocumento"] != DBNull.Value ? _reader["NroDocumento"].ToString() : "";
                _item.Telefono = _reader["Telefono"] != DBNull.Value ? _reader["Telefono"].ToString() : "";
                _item.Celular = _reader["Celular"] != DBNull.Value ? _reader["Celular"].ToString() : "";
                _item.LocalidadNombre = _reader["LocalidadNombre"] != DBNull.Value ? _reader["LocalidadNombre"].ToString() : "";
                _item.Nacionalidad = _reader["Nacionalidad"] != DBNull.Value ? _reader["Nacionalidad"].ToString() : "";
                _item.PaisResidencia = _reader["PaisResidencia"] != DBNull.Value ? _reader["PaisResidencia"].ToString() : "";
                _item.IsTituarFactura = _reader["IsTituarFactura"] != DBNull.Value ? Convert.ToByte(_reader["IsTituarFactura"]) : (byte)0;
                _List.Add(_item);
            }

            return _List;
        }

        public static PersonaClienteModel PersonaClienteGetByPersonaID(Guid PeronsaID)
        {
            PersonaClienteModel _item = new PersonaClienteModel();

            SqlParameter[] dbParams = new SqlParameter[] { 
                DBHelper.MakeParam("@PersonaID", SqlDbType.UniqueIdentifier, 0, PeronsaID)
            };
            SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_PersonaCliente_GetByPersonaID", dbParams);


            if (_reader.Read())
            {
                _item.PersonaId = _reader["PersonaID"].ToString();
                _item.Apellido = _reader["Apellido"].ToString().Trim();
                _item.Nombre = _reader["Nombre"].ToString().Trim();
                _item.NroDocumento = _reader["NroDocumento"].ToString();
                _item.Telefono = _reader["Telefono"].ToString();
                _item.Celular = _reader["Celular"].ToString();
                _item.LocalidadNombre = _reader["LocalidadNombre"].ToString();
                _item.Nacionalidad = _reader["Nacionalidad"].ToString();
                _item.PaisResidencia = _reader["PaisResidencia"].ToString();
                
            }

            return _item;
        }


        public static PersonaClienteModel PersonaClienteGetByDNI(string DNI)
        {
            PersonaClienteModel _item = new PersonaClienteModel();

            SqlParameter[] dbParams = new SqlParameter[] {
                DBHelper.MakeParam("@DNI", SqlDbType.VarChar, 50, DNI)
            };
            SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Persona_GetByDNI", dbParams);


            if (_reader.Read())
            {
                _item.PersonaId = _reader["PersonaID"].ToString();
                _item.Apellido = _reader["Apellido"].ToString().Trim();
                _item.Nombre = _reader["Nombre"].ToString().Trim();
                _item.NroDocumento = _reader["NroDocumento"].ToString();
                _item.Telefono = _reader["Telefono"].ToString();
                _item.Celular = _reader["Celular"].ToString();
                _item.LocalidadNombre = _reader["LocalidadNombre"].ToString();
                _item.Nacionalidad = _reader["Nacionalidad"].ToString();
                _item.PaisResidencia = _reader["PaisResidencia"].ToString();

            }

            return _item;
        }

        public static int IfExistDNI(string DNI)
        {
            SqlParameter[] dbParams = new SqlParameter[] {
                DBHelper.MakeParam("@DNI", SqlDbType.VarChar, 50, DNI)
            };
            SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Persona_GetByDNI", dbParams);

            while (_reader.Read())
            {
                return 1;
            }

            return 0;
        }

        public static List<PersonaClienteModel> PersonaClienteSearchByNombreDNI(string sParam)
        {
            List<PersonaClienteModel> _List = new List<PersonaClienteModel>();
            SqlParameter[] dbParams = new SqlParameter[] { 
                 DBHelper.MakeParam("@param", SqlDbType.VarChar, 0, sParam),
            };
            SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_PersonaCliente_Search", dbParams);


            while (_reader.Read())
            {
                PersonaClienteModel _item = new PersonaClienteModel();
                _item.PersonaId = _reader["PersonaID"].ToString();
                _item.Apellido = _reader["Apellido"].ToString().Trim();
                _item.Nombre = _reader["Nombre"].ToString().Trim();
                _item.NroDocumento = _reader["NroDocumento"].ToString();
                _item.Telefono = _reader["Telefono"].ToString();
                _item.Celular = _reader["Celular"].ToString();
                _item.LocalidadNombre = _reader["LocalidadNombre"].ToString();
                _item.Nacionalidad = _reader["Nacionalidad"].ToString();
                _item.PaisResidencia = _reader["PaisResidencia"].ToString();
                _List.Add(_item);
            }

            return _List;
        }

        public static List<PersonaCliente_CreditoClienteModel> PersonaClienteCreditoClienteGetAll()
        {
            List<PersonaCliente_CreditoClienteModel> _List = new List<PersonaCliente_CreditoClienteModel>();
            SqlParameter[] dbParams = new SqlParameter[] {};
            SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_PersonaCliente_CreditoCliente_GetAll", dbParams);


            while (_reader.Read())
            {
                PersonaCliente_CreditoClienteModel _item = new PersonaCliente_CreditoClienteModel();
                _item.ClienteID = new Guid(_reader["ClienteID"].ToString());
                _item.FullName = _reader["FullName"].ToString();
                _item.Monto = Convert.ToDecimal(_reader["Monto"].ToString());
                
                _List.Add(_item);
            }

            return _List;
        }
    }
}