using MAT.Utilities;
using MAT.MVC.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace MAT.MVC.Models
{
    public class VoucherModel
    {
        public String Servicios { get; set; }
        public String Excursiones { get; set; }
        public Entities.PersonaPasajero Pasajero { get; set; }
        public Entities.Voucher Voucher { get; set; }
        public Entities.Paquete Paquete { get; set; }
        public Entities.Pasaje Pasaje { get; set; }
        public Entities.Viaje Viaje { get; set; }
        public Entities.Localidad Localidad { get; set; }
        public Entities.Localidad Destino { get; set; }
        public Entities.Butaca Butaca { get; set; }
        
        public List<ReservaVoucherModel>  Reserva { get; set; }
        public VoucherModel(Guid pasajeid)
        {
            Pasaje = PasajeDataAccess.GetById(pasajeid);
            Butaca = MaestrosDataAccess.GetButacaById(Pasaje.ButacaId.Value);
            Pasajero = PersonaPasajeroDataAccess.GetByPersonaId(Pasaje.PasajeroId.Value);
            Viaje = ViajeDataAccess.GetById(Pasaje.ViajeId.Value);
            Paquete = PaqueteDataAccess.GetPaqueteById(Viaje.PaqueteId.Value);
            Destino = GeoDataAccess.GetLocalidadById(Paquete.DestinoId);
            List<Entities.PaqueteServicio> _serviciospaquete = PaqueteDataAccess.GetPaqueteServiciosByPaqueteId(Paquete.PaqueteId);
            StringBuilder _servicios = new StringBuilder();
            foreach (var item in _serviciospaquete)
            {
                _servicios.Append(MaestrosDataAccess.GetServicioById(item.ServicioId.Value).Descripcion).Append(", ");
            }
            Servicios = _servicios.ToString();

            List<Entities.PaqueteExcursion> _excursionesPaquete = PaqueteDataAccess.GetPaqueteExcursionesByPaqueteId(Paquete.PaqueteId);
            StringBuilder _excursiones = new StringBuilder();
            foreach (var item in _excursionesPaquete)
            {
                _excursiones.Append(MaestrosDataAccess.GetExcursionById(item.ExcursionId).Descripcion).Append(", ");
            }
            Excursiones = _excursiones.ToString();

            //Reserva = new ReservaVoucherModel(pasajeid);
            Reserva = GetReservas.GetListReservaHabitacion(pasajeid);


            Voucher = VoucherDataAccess.GetVoucherById(Pasaje.VoucherId.Value);
            if (Pasajero != null && Pasajero.LocalidadId.HasValue) Localidad = GeoDataAccess.GetLocalidadById(Pasajero.LocalidadId.Value);
        }
    }

    public class GetReservas
    {
        public static List<ReservaVoucherModel> GetListReservaHabitacion(Guid pasajeid)
        {
            List<ReservaVoucherModel> ListaReserva = new List<ReservaVoucherModel>();

            Entities.Pasaje pasaje = PasajeDataAccess.GetById(pasajeid);
            List<Entities.ReservaHabitacion> reserva = ReservaHabitacionDataAccess.GetByPasajeId(pasajeid).OrderBy(L =>L.Desde).ToList();

            foreach (var item in reserva)
            {

                ReservaVoucherModel Reserva = new ReservaVoucherModel();

                Entities.Habitacion habitacion = item.HabitacionId.HasValue
                    ? MaestrosDataAccess.GetHabitacionById(item.HabitacionId.Value)
                    : null;
                Entities.Hotel hotel = habitacion != null && habitacion.HotelId.HasValue
                    ? MaestrosDataAccess.GetHotelById(habitacion.HotelId.Value)
                    : null;
                if (hotel == null || habitacion == null)
                {
                    continue;
                }
                Reserva.NombreHotel = hotel.Nombre;
                Reserva.Telefono = hotel.Telefono;
                Reserva.Direccion = hotel.Direccion;
                Reserva.NroHabitacion = habitacion.NroHabitacion.Value.ToString();
                //Reserva.TipoHabitacion = ((MAT.Enums.eTipoHabitacion)(habitacion.Tipo.Value)).ToString();
                Reserva.TipoHabitacion = HabitacionTipoMethod.GetAllHabitacionTipoByTipoId(habitacion.Tipo).Descripcion;          
                Reserva.DiasReserva = Reserva.GetFechasReservadas(habitacion.HabitacionId, pasaje.ViajeId.Value);
                Reserva.PasajeroID = item.PasajeroId.ToString();

                ListaReserva.Add(Reserva);
            }

            return ListaReserva;
            
        }
    }
       

    public class ReservaVoucherModel
    {
        public string NombreHotel { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string ServiciosHotel { get; set; }
        public string NroHabitacion { get; set; }
        public string TipoHabitacion { get; set; }
        public string DiasReserva { get; set; }
        public string PasajeroID { get; set; }
        
        public string GetFechasReservadas(Guid habitacionid, Guid viajeid)
        {
            List<Entities.ReservaHabitacion> reservas = ReservaHabitacionDataAccess.GetByHabitacionId(habitacionid).Where(rs => rs.ViajeId==viajeid).ToList();
            Entities.ReservaHabitacion reserva = ReservaHabitacionDataAccess.GetByHabitacionId(habitacionid).Where(rs => rs.ViajeId == viajeid).FirstOrDefault();
            StringBuilder arrayfechas = new StringBuilder();
            string mes = "";
            mes = string.Format("{0: 'de' MMMM}", reserva.Desde.Value);
            DateTime fecha_desde = reserva.Desde.Value;
            DateTime fecha_hasta = reserva.Hasta.Value;
            arrayfechas.Append(reserva.Desde.Value.Day);
            arrayfechas.Append(",");
            int rango = Convert.ToInt32((fecha_hasta - fecha_desde).TotalDays);
            for (int i = 0; i < rango; i++)
            {
                fecha_desde = fecha_desde.AddDays(1);
                arrayfechas.Append(fecha_desde.Day);
                arrayfechas.Append(",");
            }

            //foreach (var item in reservas)
            //{
            //    mes = string.Format("{0: 'de' MMMM}", item.Desde.Value);
            //    DateTime fecha_desde = item.Desde.Value;
            //    DateTime fecha_hasta = item.Hasta.Value;
            //    arrayfechas.Append(item.Desde.Value.Day);
            //    arrayfechas.Append(",");
            //    int rango = Convert.ToInt32((fecha_hasta - fecha_desde).TotalDays);
            //    for (int i = 0; i < rango; i++)
            //    {
            //        fecha_desde = fecha_desde.AddDays(1);
            //        arrayfechas.Append(fecha_desde.Day);
            //        arrayfechas.Append(",");
            //    }
            //    break;
            //}
            arrayfechas.Remove(arrayfechas.Length - 1, 1);
            arrayfechas.Append(" " + mes);
            return arrayfechas.ToString();
        }
    }

    public class VoucherStandard
    {
        public string PasajePasajeroID { get; set; }
        public string PasajePasajeID { get; set; }
        public string VoucherNroVoucher { get; set; }
        public string VoucherFechaEmision { get; set; }
        public string ButacaNroButaca { get; set; }
        public int ButacaTipo { get; set; }
        public string PersonaApellido { get; set; }
        public string PersonaNombre { get; set; }
        public string PersonaNroDocumento { get; set; }
        public string PersonaLocalidad { get; set; }
        public string PersonaEmail { get; set; }
        public string PersonaTelefono { get; set; }
        public string PersonaCelular { get; set; }
        public string PersonaDomicilio { get; set; }
        public string DestinoNombre { get; set; }
        public string ViajeHoraSalida { get; set; }
        public string ViajeHoraRegreso { get; set; }
        public string ViajeFechaSalida { get; set; }
        public string ViajeSalidaL { get; set; }
        public string ViajeRegresoL { get; set; }
        public string ViajeFechaRegreso { get; set; }
        public string ViajeMedio { get; set; }
        public string PaqueteServicios { get; set; }
        public string PaqueteExcusiones { get; set; }
        public string PaqueteExcusionesOpcionales { get; set; }
        public int    TiempoConsentracion { get; set; }
        public string ViajeObservaciones { get; set; }
        public string ViajeID { get; set; }

        public List<HotelStandard> Hoteles { get; set; }
        public List<ItinerarioViajeModel> Itinerario { get; set; }
    }

    public class HotelStandard
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string NroHabitacion { get; set; }
        public string NombreHabitacion { get; set; }
        public string Desde { get; set; }
        public string Hasta { get; set; }
        public string DiasEstadia { get; set; }
        public string HotelID { get; set; }
        public string CP { get; set; }
        public string Localidad { get; set; }
        public string Email { get; set; }
        public string Contacto { get; set; }
    }

    public class VoucherMethod {
        public static List<HotelStandard> GetHotelesByPasajeID(string PasajeID)
        {
            List<HotelStandard> ListHoteles = new List<HotelStandard>();

             SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@PasajeID", SqlDbType.VarChar, 0, PasajeID),
                };
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Voucher_GetHotelByPasajeID", dbParams))
            {
                while (_reader.Read())
                {
                    HotelStandard item = new HotelStandard();
                    item.Nombre = _reader["nombre"].ToString();
                    item.Telefono = _reader["telefono"].ToString();
                    item.Direccion = _reader["direccion"].ToString();
                    item.NroHabitacion = _reader["nrohabitacion"].ToString();
                    item.NombreHabitacion = _reader["NombreHabitacion"].ToString();
                    item.Desde = _reader["Desde"].ToString();
                    item.Hasta = _reader["Hasta"].ToString();


                    StringBuilder arrayfechas = new StringBuilder();
                    //DateTime date2 =  Convert.ToDateTime(item.Hasta);
                    //string mes = "";
                    //mes = "de " + date2.ToString("Y", CultureInfo.CreateSpecificCulture("es-ES"));

                    string mes = "";
                    if (Convert.ToDateTime(item.Hasta).Day > 1)
                    {
                        mes = "de " + Convert.ToDateTime(item.Hasta).ToString("Y", CultureInfo.CreateSpecificCulture("es-ES"));
                    }
                    else
                    {
                        mes = "de " + Convert.ToDateTime(item.Desde).ToString("Y", CultureInfo.CreateSpecificCulture("es-ES"));
                    }
                    DateTime fecha_ = Convert.ToDateTime(item.Desde);
                    DateTime fecha_desde = Convert.ToDateTime(item.Desde);
                    DateTime fecha_hasta = Convert.ToDateTime(item.Hasta);
                    arrayfechas.Append(fecha_desde.Day);
                    arrayfechas.Append(",");
                    int rango = Convert.ToInt32((fecha_hasta - fecha_desde).TotalDays);
                    for (int i = 0; i < (rango - 1); i++)
                    {
                        fecha_desde = fecha_desde.AddDays(1);
                        if (fecha_desde.Month > fecha_.Month)
                        {
                            string mm = "de " + fecha_.ToString("Y", CultureInfo.CreateSpecificCulture("es-ES"));
                            arrayfechas.Remove(arrayfechas.Length - 1, 1);
                            item.DiasEstadia = arrayfechas.Append(" " + mm + "; ").ToString();
                            fecha_ = fecha_desde;

                            arrayfechas.Append(fecha_desde.Day);
                            arrayfechas.Append(",");
                        }
                        else
                        {
                            arrayfechas.Append(fecha_desde.Day);
                            arrayfechas.Append(",");
                        }
                    }

                    arrayfechas.Remove(arrayfechas.Length - 1, 1);
                    item.DiasEstadia = arrayfechas.Append(" " + mes).ToString();

                    ListHoteles.Add(item);
                }
            }
            return ListHoteles;
        }

        public static List<Models.VoucherStandard> GetVoucherByFacturaID (string FacturaID)
        {
            List<Models.VoucherStandard> ListVoucher = new List<Models.VoucherStandard>();
             SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@FacturaID", SqlDbType.VarChar, 0, FacturaID),
                };
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Voucher_GetVoucherByFacturaID", dbParams))
            {
                while (_reader.Read())
                {
                    Models.VoucherStandard item = new VoucherStandard();
                    item.PasajePasajeroID = _reader["PasajePasajeroID"].ToString();
                    item.PasajePasajeID = _reader["PasajePasajeID"].ToString();
                    item.VoucherNroVoucher = _reader["VoucherNroVoucher"].ToString();
                    item.VoucherFechaEmision = _reader["VoucherFechaEmision"].ToString();
                    item.ButacaNroButaca = _reader["ButacaNroButaca"].ToString();
                    if (_reader["ButacaTipo"].ToString() != "")
                    {
                        item.ButacaTipo = Convert.ToInt32(_reader["ButacaTipo"].ToString());
                    }
                    item.PersonaApellido = _reader["PersonaApellido"].ToString();
                    item.PersonaNombre = _reader["PersonaNombre"].ToString();
                    item.PersonaNroDocumento = _reader["PersonaNroDocumento"].ToString();
                    item.PersonaLocalidad = _reader["PersonaLocalidad"].ToString();
                    item.PersonaEmail = _reader["PersonaEmail"].ToString();
                    item.PersonaTelefono = _reader["PersonaTelefono"].ToString();
                    item.PersonaCelular = _reader["PersonaCelular"].ToString();
                    item.PersonaDomicilio = _reader["PersonaDomicilio"].ToString();
                    item.DestinoNombre = _reader["DestinoNombre"].ToString();
                    item.ViajeHoraSalida = _reader["ViajeHoraSalida"].ToString();
                    item.ViajeHoraRegreso = _reader["ViajeHoraRegreso"].ToString();
                    item.TiempoConsentracion = Convert.ToInt32(_reader["TiempoConsentracion"]);
                    item.ViajeFechaSalida = _reader["ViajeFechaSalida"].ToString();
                    item.ViajeSalidaL = Convert.ToDateTime(_reader["ViajeFechaSalida"].ToString()).ToString("D", CultureInfo.CreateSpecificCulture("es-ES"));
                    item.ViajeFechaRegreso = _reader["ViajeFechaRegreso"].ToString();
                    item.ViajeRegresoL = Convert.ToDateTime(_reader["ViajeFechaRegreso"].ToString()).ToString("D", CultureInfo.CreateSpecificCulture("es-ES"));
                    item.ViajeMedio = _reader["ViajeMedio"].ToString();
                    item.ViajeObservaciones = _reader["Observaciones"].ToString();
                    item.PaqueteServicios = _reader["PaqueteServicios"].ToString();
                    item.PaqueteExcusiones = _reader["PaqueteExcusiones"].ToString();
                    item.PaqueteExcusionesOpcionales = _reader["PaqueteExcusionesOpcionales"].ToString();
                    item.ViajeID = _reader["ViajeID"].ToString();
                    ListVoucher.Add(item);
                }
            }

            foreach (Models.VoucherStandard item in ListVoucher)
	        {
		        item.Hoteles = GetHotelesByPasajeID(item.PasajePasajeID);
                item.Itinerario = ItinerarioMethod.GetItinerarioByViajeID(item.ViajeID);
	        }
             

            return ListVoucher;
        }

        public static byte GetNrPrintByFacturaID(string FacturaID)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@FacturaID", SqlDbType.VarChar, 0, FacturaID),
                };
            byte byteResult = Convert.ToByte(DBHelper.ExecuteScalar("usp_MAT_Voucher_GetNrPrintByFacturaID", dbParams));

            return byteResult;
        }
    }
}