using MAT.Services;
using MAT.Utilities;
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
        private PasajeService pasajeService;
        private PersonaPasajeroService pasajeroService;
        private FacturaService facturaService;
        private ServicioService servicioService;
        private ExcursionService excursionService;
        private PaqueteServicioService paqueteservicioService;
        private PaqueteExcursionService paqueteexcursionService;
        private PaqueteService paqueteService;
        private ViajeService viajeService;
        private VoucherService voucherService;
        private LocalidadService localidadService;

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
            pasajeService = new PasajeService();
            pasajeroService = new PersonaPasajeroService();
            facturaService = new FacturaService();
            servicioService = new ServicioService();
            excursionService = new ExcursionService();
            paqueteservicioService = new PaqueteServicioService();
            paqueteexcursionService = new PaqueteExcursionService();
            paqueteService = new PaqueteService();
            viajeService = new ViajeService();
            voucherService = new VoucherService();
            localidadService = new LocalidadService();

            Pasaje = pasajeService.GetByPasajeId(pasajeid);
            Butaca = new ButacaService().GetByButacaId(Pasaje.ButacaId.Value);
            Pasajero = pasajeroService.GetAll().Where(psj => psj.PasajeroId == Pasaje.PasajeroId.Value).FirstOrDefault();
            Viaje = viajeService.GetByViajeId(Pasaje.ViajeId.Value);
            Paquete = paqueteService.GetByPaqueteId(Viaje.PaqueteId.Value);
            Destino = localidadService.GetById(Paquete.DestinoId);
            List<Entities.PaqueteServicio> _serviciospaquete = paqueteservicioService.GetByPaqueteId(Paquete.PaqueteId).ToList();
            StringBuilder _servicios = new StringBuilder();
            foreach (var item in _serviciospaquete)
            {
                _servicios.Append(servicioService.GetByServicioId(item.ServicioId.Value).Descripcion).Append(", ");
            }
            Servicios = _servicios.ToString();

            List<Entities.PaqueteExcursion> _excursionesPaquete = paqueteexcursionService.GetByPaqueteId(Paquete.PaqueteId).ToList();
            StringBuilder _excursiones = new StringBuilder();
            foreach (var item in _excursionesPaquete)
            {
                _excursiones.Append(excursionService.GetByExcursionId(item.ExcursionId).Descripcion).Append(", ");
            }
            Excursiones = _excursiones.ToString();

            //Reserva = new ReservaVoucherModel(pasajeid);
            Reserva = GetReservas.GetListReservaHabitacion(pasajeid);


            Voucher = voucherService.GetByVoucherId(Pasaje.VoucherId.Value);
            if (Pasajero.LocalidadId.HasValue) Localidad = localidadService.GetById(Pasajero.LocalidadId.Value);
        }
    }

    public class GetReservas
    {
        public static List<ReservaVoucherModel> GetListReservaHabitacion(Guid pasajeid)
        {
            List<ReservaVoucherModel> ListaReserva = new List<ReservaVoucherModel>();

            Services.PasajeService pasajeService = new PasajeService();
            Entities.Pasaje pasaje = pasajeService.GetByPasajeId(pasajeid);
            Services.ReservaHabitacionService reservaService = new ReservaHabitacionService();
            List<Entities.ReservaHabitacion> reserva = reservaService.GetByPasajeId(pasajeid).OrderBy(L =>L.Desde).ToList();

            foreach (var item in reserva)
            {

                ReservaVoucherModel Reserva = new ReservaVoucherModel();

                Entities.Habitacion habitacion = item.HabitacionId.HasValue ? new HabitacionService().GetByHabitacionId(item.HabitacionId.Value) : null;
                HotelService hotelService = new HotelService();
                Entities.Hotel hotel = hotelService.GetByHotelId(habitacion.HotelId.Value);
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
            ReservaHabitacionService reservaService = new ReservaHabitacionService();
            List<Entities.ReservaHabitacion> reservas = reservaService.GetByHabitacionId(habitacionid).Where(rs => rs.ViajeId==viajeid).ToList();
            Entities.ReservaHabitacion reserva = reservaService.GetByHabitacionId(habitacionid).Where(rs => rs.ViajeId == viajeid).FirstOrDefault();
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

        public List<HotelStandard> Hoteles { get; set; }
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
                    ListVoucher.Add(item);
                }
            }

            foreach (Models.VoucherStandard item in ListVoucher)
	        {
		        item.Hoteles = GetHotelesByPasajeID(item.PasajePasajeID);
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