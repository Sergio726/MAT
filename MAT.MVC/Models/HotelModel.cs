using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MAT.MVC.Models
{
    public class HotelDropDown
    {
        public string HotelID { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        
    }

    public class tblHotel
    {
        public string HotelID { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string CP { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Contacto { get; set; }
        public int CantidadHabitaciones { get; set; }
        public int Categoria { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public string GoogleMapHtml { get; set; }
        public string Localidad { get; set; }
        
    }

    public class EsquemaDistribucion
    {
        public Guid HabitacionID { get; set; }
        public int NroHabitacion { get; set; }
        public string HabNombre { get; set; }
        public int HabTipo { get; set; }
        public string HabTipoDescripcion { get; set; }
        public string HotelNombre { get; set; }
        public List<HotelPersona> lHotelPersona { get; set; }
        public Guid ViajeID { get; set; }
        public string HotelIngreso { get; set; }
        public string HotelEgreso { get; set; }
    }

    public class HotelPersona
    {
        public Guid PersonaID { get; set; }
        public string PersonaApellido { get; set; }
        public string PersonaNombre { get; set; }
    }

    public class HotelMethod
    {
        public static List<HotelDropDown> GetHotelDropDown()
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                    };
            SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Hotel_DropDown", dbParams);

            List<HotelDropDown> ListHotel = new List<HotelDropDown>();
            while (_reader.Read())
            {
                HotelDropDown Hotel = new HotelDropDown();
                Hotel.HotelID = _reader["HotelID"].ToString();
                Hotel.Nombre = _reader["Nombre"].ToString();
               
                ListHotel.Add(Hotel);
            }
            return ListHotel;
        }

        public static List<HotelDropDown> GetHotelByViaje(string sViajeId)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    { 
                          DBHelper.MakeParam("@ViajeId", SqlDbType.UniqueIdentifier, 0, new Guid(sViajeId))
                    };
            SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Hotel_GetByViajeID", dbParams);

            List<HotelDropDown> ListHotel = new List<HotelDropDown>();
            while (_reader.Read())
            {
                HotelDropDown Hotel = new HotelDropDown();
                Hotel.HotelID = _reader["HotelID"].ToString();
                Hotel.Nombre = _reader["Nombre"].ToString();
                
                // Validar si el campo Fecha es NULL antes de convertir
                object fechaValue = _reader["Fecha"];
                if (fechaValue != null && fechaValue != DBNull.Value)
                {
                    Hotel.Fecha = Convert.ToDateTime(fechaValue);
                }
                else
                {
                    Hotel.Fecha = DateTime.MinValue; // Valor por defecto si es NULL
                }
                
                ListHotel.Add(Hotel);
            }
            return ListHotel;
        }

        public static List<HotelStandard> GetHotelAll()
        {
            SqlParameter[] dbParams = new SqlParameter[] {};
            SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Hotel_GetAll", dbParams);

            List<HotelStandard> ListHotel = new List<HotelStandard>();
            while (_reader.Read())
            {
                HotelStandard Hotel = new HotelStandard();
                Hotel.HotelID = _reader["HotelID"].ToString();
                Hotel.Nombre = _reader["Nombre"].ToString();
                Hotel.Direccion = _reader["Direccion"].ToString();
                Hotel.CP = _reader["CP"].ToString();
                Hotel.Localidad = _reader["Localidad"].ToString();
                Hotel.Telefono = _reader["Telefono"].ToString();
                Hotel.Email = _reader["Email"].ToString();
                Hotel.Contacto = _reader["Contacto"].ToString();

                ListHotel.Add(Hotel);
            }
            return ListHotel;
        }

        public static tblHotel GetHotelByID(Guid HotelID)
        {
            SqlParameter[] dbParams = new SqlParameter[] { 
                DBHelper.MakeParam("@HotelID", SqlDbType.UniqueIdentifier, 0, HotelID)
            };
            SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Hotel_GetById", dbParams);

            tblHotel Hotel = new tblHotel();
            while (_reader.Read())
            {
                
                Hotel.HotelID = _reader["HotelID"].ToString();
                Hotel.Nombre = _reader["Nombre"].ToString();
                Hotel.Direccion = _reader["Direccion"].ToString();
                Hotel.CP = _reader["CP"].ToString();
                Hotel.Localidad = _reader["Localidad"].ToString();
                Hotel.Telefono = _reader["Telefono"].ToString();
                Hotel.Email = _reader["Email"].ToString();
                Hotel.Contacto = _reader["Contacto"].ToString();

                if (_reader["CantidadHabitaciones"].ToString() != "")
                {
                    Hotel.CantidadHabitaciones = Convert.ToInt32(_reader["CantidadHabitaciones"]);    
                }
                if (_reader["Categoria"].ToString() != "")
                {
                    Hotel.Categoria = Convert.ToInt32(_reader["Categoria"]);    
                }
                
                Hotel.CheckIn = _reader["CheckIn"].ToString();
                Hotel.CheckOut = _reader["CheckOut"].ToString();
                Hotel.GoogleMapHtml = _reader["GoogleMapHtml"].ToString();
                
            }
            return Hotel;
        }

        public static List<EsquemaDistribucion> GetEsquemaDistribucion(Guid gViajeId, Guid gHotelId, string sFecha) 
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    { 
                          DBHelper.MakeParam("@ViajeId", SqlDbType.UniqueIdentifier, 0, gViajeId),
                          DBHelper.MakeParam("@HotelId", SqlDbType.UniqueIdentifier, 0, gHotelId),
                          DBHelper.MakeParam("@Fecha", SqlDbType.Date, 0, Convert.ToDateTime(sFecha)),
                          DBHelper.MakeParam("@FechaD", SqlDbType.VarChar, 10, sFecha)
                    };
            DataSet ds = DBHelper.ExecuteDataSet("dbo.usp_MAT_Hotel_EsquemaDistribucion", dbParams);

         

            List<EsquemaDistribucion> LDistribucion = new List<EsquemaDistribucion>();

            string sHabId = "";
            DataTable dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
			{
                EsquemaDistribucion _item = new EsquemaDistribucion();
                List<HotelPersona> lPersona = new List<HotelPersona>();

                if (sHabId != row["HabitacionID"].ToString())
                {
                    sHabId = row["HabitacionID"].ToString();

                    if (row["HabitacionID"].ToString() != "")
                    {
                        _item.HabitacionID = new Guid(row["HabitacionID"].ToString());    
                    }

                    if (row["NroHabitacion"].ToString() != "")
                    {
                        _item.NroHabitacion = Convert.ToInt32(row["NroHabitacion"].ToString());    
                    }
                    
                    _item.HabNombre = row["HabNombre"].ToString();

                    if (row["HabTipo"].ToString() != "")
                    {
                        _item.HabTipo = Convert.ToInt32(row["HabTipo"]);    
                    }
                    
                    _item.HabTipoDescripcion = row["HabTipoDescripcion"].ToString();
                    _item.HotelNombre = row["HotelNombre"].ToString();
                    _item.HotelIngreso = row["HotelIngreso"].ToString();
                    _item.HotelEgreso = row["HotelEgreso"].ToString();
                    
                    if (row["ViajeID"].ToString() != "")
                    {
                        _item.ViajeID = new Guid(row["ViajeID"].ToString());    
                    }
                    

                    LDistribucion.Add(_item);
                }
            }

            foreach (EsquemaDistribucion lDist in LDistribucion)
            {
                List<HotelPersona> lPersona = new List<HotelPersona>();
                foreach (DataRow row in dt.Rows)
                {
                    HotelPersona hPersona = new HotelPersona();

                    if (lDist.HabitacionID.ToString() == row["HabitacionID"].ToString())
                    {
                        if (row["PersonaID"].ToString() != "")
                        {
                            hPersona.PersonaID = new Guid(row["PersonaID"].ToString());
                        }
                        hPersona.PersonaApellido = row["PersonaApellido"].ToString();
                        hPersona.PersonaNombre = row["PersonaNombre"].ToString();

                        lPersona.Add(hPersona);
                    }
                    
                }

                lDist.lHotelPersona = lPersona;
            }

            
            return LDistribucion;
        }

        public static string CreateHotel(MVC.Models.tblHotel oHotel) {
            string HotelId = "";
            
            SqlParameter[] dbParams = new SqlParameter[] {
                  DBHelper.MakeParam("@Nombre", SqlDbType.VarChar, 50, oHotel.Nombre),
                  DBHelper.MakeParam("@Direccion", SqlDbType.VarChar, 50, oHotel.Direccion),
                  DBHelper.MakeParam("@CP", SqlDbType.VarChar, 50, oHotel.CP),
                  DBHelper.MakeParam("@Telefono", SqlDbType.VarChar, 50, oHotel.Telefono),
                  DBHelper.MakeParam("@Email", SqlDbType.VarChar, 50, oHotel.Email),
                  DBHelper.MakeParam("@Contacto", SqlDbType.VarChar, 50, oHotel.Contacto),
                  DBHelper.MakeParam("@CantidadHabitaciones", SqlDbType.Int, 0, oHotel.CantidadHabitaciones),
                  DBHelper.MakeParam("@Categoria", SqlDbType.Int, 0, oHotel.Categoria),
                  DBHelper.MakeParam("@CheckIn", SqlDbType.VarChar, 8, oHotel.CheckIn),
                  DBHelper.MakeParam("@CheckOut", SqlDbType.VarChar, 8, oHotel.CheckOut),
                  DBHelper.MakeParam("@GoogleMapHtml", SqlDbType.VarChar, 200, oHotel.GoogleMapHtml),
                  DBHelper.MakeParam("@Localidad", SqlDbType.VarChar, 50, oHotel.Localidad)

            };
            HotelId = DBHelper.ExecuteNonQueryOutputString("dbo.usp_MAT_Hotel_Insert", dbParams, "@HotelID", SqlDbType.UniqueIdentifier, 0);
            return HotelId;
        }

        public static void UpdateHotel(MVC.Models.tblHotel oHotel)
        {
            SqlParameter[] dbParams = new SqlParameter[] {
                  DBHelper.MakeParam("@HotelID", SqlDbType.UniqueIdentifier, 0, new Guid(oHotel.HotelID)),
                  DBHelper.MakeParam("@Nombre", SqlDbType.VarChar, 50, oHotel.Nombre),
                  DBHelper.MakeParam("@Direccion", SqlDbType.VarChar, 50, oHotel.Direccion),
                  DBHelper.MakeParam("@CP", SqlDbType.VarChar, 50, oHotel.CP),
                  DBHelper.MakeParam("@Telefono", SqlDbType.VarChar, 50, oHotel.Telefono),
                  DBHelper.MakeParam("@Email", SqlDbType.VarChar, 50, oHotel.Email),
                  DBHelper.MakeParam("@Contacto", SqlDbType.VarChar, 50, oHotel.Contacto),
                  DBHelper.MakeParam("@CantidadHabitaciones", SqlDbType.Int, 0, oHotel.CantidadHabitaciones),
                  DBHelper.MakeParam("@Categoria", SqlDbType.Int, 0, oHotel.Categoria),
                  DBHelper.MakeParam("@CheckIn", SqlDbType.VarChar, 8, oHotel.CheckIn),
                  DBHelper.MakeParam("@CheckOut", SqlDbType.VarChar, 8, oHotel.CheckOut),
                  DBHelper.MakeParam("@GoogleMapHtml", SqlDbType.VarChar, 200, oHotel.GoogleMapHtml),
                  DBHelper.MakeParam("@Localidad", SqlDbType.VarChar, 50, oHotel.Localidad)

            };
            
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Hotel_Update", dbParams);
            
        }

        public static int CheckNombreHotel(string Nombre)
        {
            SqlParameter[] dbParams = new SqlParameter[] {
                  DBHelper.MakeParam("@Nombre", SqlDbType.VarChar, 50, Nombre)
            };
            return DBHelper.ExecuteNonQueryOutput("dbo.usp_MAT_Hotel_CheckNombre", dbParams, "@IfExists", SqlDbType.Int, 0);
            
        }

    }
}