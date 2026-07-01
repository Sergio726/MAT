using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TB.ComponentModel;
using System.Web.Mvc;
using MAT.Entities;
using MAT.Services;
using MAT.Enums;
using System.Web;

namespace MAT.Utilities
{
    public static class Helper
    {

        /// <summary>
        /// Metodo estatico para actualizar por Reflection todos los valores de las propiedades de un Entidad
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="datos"></param>
        public static void FillEntity<T>(ref T entity, NameValueCollection datos)
        {

            // Get all properties of the Type T
            PropertyInfo[] entityProperties = typeof(T).GetProperties();
            // Loop through the properties defined in the 
            // entityList entity object and mapped the value
            foreach (System.Reflection.PropertyInfo item in entityProperties)
            {
                string propName = string.Empty;
                if (propName.Equals(string.Empty)) propName = item.Name;

                var bindingFlags = BindingFlags.Public | BindingFlags.Instance;
                var propertyInfo = entity.GetType().GetProperty(propName, bindingFlags);
                Type propertyType = propertyInfo.PropertyType;
                if (propertyInfo == null) { continue; }
                if (datos[propName] != null)
                {
                    object obj = new object();
                    Type normalize = propertyType;
                    if (IsGenericNullable(propertyType))
                    {
                        normalize = Nullable.GetUnderlyingType(propertyType);
                    }
                    UniversalTypeConverter.TryConvert(datos[propName], normalize, out obj);
                    propertyInfo.SetValue(entity, obj, null);
                }
            }
        }

        public static void FillEntity<T>(ref T entity, FormCollection datos)
        {

            // Get all properties of the Type T
            PropertyInfo[] entityProperties = typeof(T).GetProperties();
            // Loop through the properties defined in the 
            // entityList entity object and mapped the value
            foreach (System.Reflection.PropertyInfo item in entityProperties)
            {
                string propName = string.Empty;
                if (propName.Equals(string.Empty)) propName = item.Name;

                var bindingFlags = BindingFlags.Public | BindingFlags.Instance;
                var propertyInfo = entity.GetType().GetProperty(propName, bindingFlags);
                Type propertyType = propertyInfo.PropertyType;
                if (propertyInfo == null) { continue; }
                if (!string.IsNullOrEmpty(datos[propName]))
                {
                    object obj = new object();
                    Type normalize = propertyType;
                    if (IsGenericNullable(propertyType))
                    {
                        normalize = Nullable.GetUnderlyingType(propertyType);
                    }
                    UniversalTypeConverter.TryConvert(datos[propName], normalize, out obj);
                    propertyInfo.SetValue(entity, obj, null);
                }
            }
        }

        /// <summary>
        /// Metodo para consulta si el tipo es un tipo nulo
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsGenericNullable(Type type)
        {
            return type.IsGenericType &&
        type.GetGenericTypeDefinition() == typeof(Nullable<>).GetGenericTypeDefinition();
        }

        /// <summary>
        /// Metodo para Generar un SelectList a partir de un Enumerador
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="enumObj"></param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> ToSelectList<TEnum>(this TEnum enumObj)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            IEnumerable<TEnum> tipos = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
            IEnumerable<SelectListItem> listitem = from e in tipos
                                                   select new SelectListItem
                                                   {
                                                       Text = e.GetDescription(),
                                                       Value = (Convert.ToInt32(e)).ToString()
                                                   };

            return listitem;
        }

        public static IEnumerable<SelectListItem> ToSelectList<TEnum>(this TEnum enumObj, string selectvalue)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            IEnumerable<TEnum> tipos = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
            IEnumerable<SelectListItem> listitem = from e in tipos
                                                   select new SelectListItem
                                                   {
                                                       Text = e.ToString(),
                                                       Value = (Convert.ToInt32(e)).ToString(),
                                                   };
            listitem.Where(e => e.Value == selectvalue).FirstOrDefault().Selected = true;
            return listitem;
        }

        public static IEnumerable<SelectListItem> ToSelectEntities(string Entidad)
        {
            return ToSelectEntities(Entidad, null);
        }

        public static IEnumerable<SelectListItem> ToSelectEntities(string Entidad, Guid? selected)
        {
            IEnumerable<SelectListItem> listitem = null;
            switch (Entidad)
            {

                case "Localidad":
                    listitem = GeoDataAccess.GetAllLocalidades().Select(e => new SelectListItem()
                    {
                        Text = e.Nombre,
                        Value = Convert.ToString(e.Id)
                    });
                    break;

                case "Paquete":
                    MAT.Services.PaqueteService SPaquete = new MAT.Services.PaqueteService();
                    IEnumerable<Paquete> LPaquete = SPaquete.GetAll();
                    listitem = LPaquete.Select(e => new SelectListItem() 
                    {
                        Text = new MAT.Services.PaqueteService().GetByPaqueteId(e.PaqueteId).Descripcion.ToString(),
                        Value = Convert.ToString(e.PaqueteId)   
                    });
                    break;
                case "Transporte":
                    MAT.Services.TransporteService STransporte = new MAT.Services.TransporteService();
                    IEnumerable<Transporte> LTransporte = STransporte.GetAll();
                    listitem = LTransporte.Select(e => new SelectListItem()
                    {
                        Text = new MAT.Services.TransporteService().GetByTransporteId(e.TransporteId).NroCoche.ToString(),
                        Value = Convert.ToString(e.TransporteId)
                    });
                    break;
                case "Hotel":
                    MAT.Services.HotelService SHotel = new HotelService();
                    IEnumerable<Hotel> LHotel = SHotel.GetAll();
                    listitem = LHotel.Select(e => new SelectListItem()
                    {
                        Text = new MAT.Services.HotelService().GetByHotelId(e.HotelId).Nombre.ToString(),
                        Value = Convert.ToString(e.HotelId)
                    });
                    if (selected.HasValue) listitem.Where(it => it.Value == selected.Value.ToString()).FirstOrDefault().Selected = true;
                    break ;
                case "Proveedor":
                    MAT.Services.ProveedorService servicioService = new ProveedorService();
                    IEnumerable<Proveedor> enumServicio = servicioService.GetAll();
                    listitem = enumServicio.Select(e => new SelectListItem()
                    {
                        Text = new MAT.Services.ProveedorService().GetByProveedorId(e.ProveedorId).RazonSocial.ToString(),
                        Value = Convert.ToString(e.ProveedorId)
                    });
                    break;
                case "Provincia":
                    listitem = GeoDataAccess.GetAllProvincias().Select(e => new SelectListItem()
                    {
                        Text = e.Nombre,
                        Value = Convert.ToString(e.Id)
                    });
                    if (selected.HasValue) listitem.Where(it => it.Value == selected.Value.ToString()).FirstOrDefault().Selected = true;
                    break;
                case "HotelPorViaje":
                    MAT.Services.HotelService hotelService = new HotelService();
                    MAT.Services.ViajeHotelService viajehotelService = new ViajeHotelService();
                    List<ViajeHotel> viajehotelList = viajehotelService.GetByViajeId(selected.Value).ToList();
                    List<Hotel> hotelList = new List<Entities.Hotel>();
                    foreach (var item in viajehotelList)
                    {
                        hotelList.Add(hotelService.GetByHotelId(item.HotelId));
                    }
                    listitem = hotelList.Select(e => new SelectListItem()
                    {
                        Text = new MAT.Services.HotelService().GetByHotelId(e.HotelId).Nombre.ToString(),
                        Value = Convert.ToString(e.HotelId)
                    });
                    break;
                case "Viaje":
                    MAT.Services.ViajeService viajeService = new MAT.Services.ViajeService();
                    IEnumerable<Viaje> viajes = viajeService.GetAll();
                    listitem = viajes.Select(e => new SelectListItem() 
                    {
                        Text = string.Format("{0} - Salida {1}", new PaqueteService().GetByPaqueteId(e.PaqueteId.Value).Descripcion, e.FechaSalida.Value.ToShortDateString()),
                        Value = Convert.ToString(e.ViajeId)   
                    });
                    break;
            }
            return listitem;
        }

        public static IEnumerable<SelectListItem> ToSelectEntitiesLocalidades(string Entidad, int? selected)
        {
            IEnumerable<SelectListItem> listitem = null;
            switch (Entidad)
            {                
                case "Provincia":
                    listitem = GeoDataAccess.GetAllProvincias().Select(e => new SelectListItem()
                    {
                        Text = e.Nombre,
                        Value = Convert.ToString(e.Id)
                    });
                    if (selected.HasValue) listitem.Where(it => it.Value == selected.Value.ToString()).FirstOrDefault().Selected = true;
                    break;
            }
            return listitem;
        }
               
        public static string ToSelectItem(string Entidad, string selectvalue)
        {
            string Item = null;
            if (selectvalue != "")
            { 
            
           
            switch (Entidad)
            {


                case "Localidad":
                    var localidad = GeoDataAccess.GetLocalidadById(Convert.ToInt32(selectvalue));
                    Item = localidad != null ? localidad.Nombre : "Sin Definir";
                    break;

                case "Transporte":
                    Guid TransporteId = new Guid(selectvalue);
                    MAT.Services.TransporteService STransporte = new MAT.Services.TransporteService();
                    MAT.Entities.Transporte ETransporte = STransporte.Get(new TransporteKey(TransporteId));
                    Item = ETransporte.NroCoche;
                    break;

                case "Paquete":
                    Guid PaqueteId = new Guid(selectvalue);
                    MAT.Services.PaqueteService SPaquete = new MAT.Services.PaqueteService();
                    MAT.Entities.Paquete EPaquete = SPaquete.Get(new PaqueteKey(PaqueteId));
                    Item = EPaquete.Descripcion;
                    break;
                case "Hotel":
                    Guid HotelId = new Guid(selectvalue);
                    MAT.Services.HotelService SHotel = new MAT.Services.HotelService();
                    MAT.Entities.Hotel EHotel = SHotel.Get(new HotelKey(HotelId));
                    Item = EHotel.Nombre;
                    break;
                case "Provincia":
                    int ProvinciaId = Convert.ToInt32(selectvalue);
                    var provincia = GeoDataAccess.GetProvinciaById(ProvinciaId);
                    Item = provincia != null ? provincia.Nombre : "Sin Definir";
                    break;
            }
            }
            else
            { Item = "Sin Definir"; }
            
            return Item;
        }

        public static bool IsNumeric(object Expression)
        {

            bool isNum;

            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

            return isNum;

        }

        public static string GetLocalidadName(int localidadId)
        {
            var localidad = GeoDataAccess.GetLocalidadById(localidadId);
            return localidad != null ? localidad.Nombre : "Sin Definir";
        }

        public static void AlertMessage(string message, System.Web.UI.Page ctxpage)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("')};");
            sb.Append("</script>");
            ctxpage.ClientScript.RegisterClientScriptBlock(ctxpage.GetType(), "alert", sb.ToString());
        }

        public static void GenerarVoucher(ref Entities.Pasaje pasaje)
        {
            VoucherService voucherService = new VoucherService();
            PasajeService pasajeService = new PasajeService();
            Voucher voucher = new Voucher()
            {
                VoucherId = Guid.NewGuid(),
                FechaEmision = DateTime.Now
            };
            voucherService.Insert(voucher);
            pasaje.VoucherId = voucher.VoucherId;
            pasajeService.Update(pasaje);
        }

        public static string GetVendedorName(Guid id)
        {
            PersonaVendedorService vendedorService = new PersonaVendedorService();
            PersonaVendedor vendedor = vendedorService.GetAll().Where(vd => vd.PersonaId == id).FirstOrDefault();
            return vendedor.Nombre + vendedor.Apellido;
        }

        public static string GetDisponibilidad(Guid habitacionid, Guid viajeid)
        {
            Services.ReservaHabitacionService reshabService = new ReservaHabitacionService();
            List<Entities.ReservaHabitacion> reservas = reshabService.GetByHabitacionId(habitacionid).Where(vj => vj.ViajeId == viajeid).ToList();
            Entities.Habitacion habitacion = new HabitacionService().GetByHabitacionId(habitacionid);
            int capacidad = 0;

            capacidad = Convert.ToInt32(habitacion.Capacidad.ToString() ?? "0");

            //if ((eTipoHabitacion)habitacion.Tipo == eTipoHabitacion.Triple || (eTipoHabitacion)habitacion.Tipo == eTipoHabitacion.Cuadruple)
            //{
            //    capacidad = Convert.ToInt32(habitacion.Capacidad.ToString() ?? "0");
            //}
            //else
            //{
            //    capacidad = GetCapacidadHabitacion((eTipoHabitacion)habitacion.Tipo);
            //}
            return (capacidad - reservas.Count).ToString();
        }

        public static bool IsHabitacionDisponible(Guid habitacionid, Guid viajeid)
        {
            Services.ReservaHabitacionService reshabService = new ReservaHabitacionService();
            List<Entities.ReservaHabitacion> reservas = reshabService.GetByHabitacionId(habitacionid).Where(vj => vj.ViajeId == viajeid).ToList();
            Entities.Habitacion habitacion = new HabitacionService().GetByHabitacionId(habitacionid);
            int capacidad = 0;

            capacidad = Convert.ToInt32(habitacion.Capacidad.ToString() ?? "0");

            //if ((eTipoHabitacion)habitacion.Tipo == eTipoHabitacion.Triple || (eTipoHabitacion)habitacion.Tipo == eTipoHabitacion.Cuadruple)
            //{
            //    capacidad = Convert.ToInt32(habitacion.Capacidad.ToString() ?? "0");
            //}
            //else
            //{
            //    capacidad = GetCapacidadHabitacion((eTipoHabitacion)habitacion.Tipo);
            //}
            
            return (capacidad - reservas.Count > 0 ? true : false);
        }

        //public static int GetCapacidadHabitacion(eTipoHabitacion tipo)
        //{
        //    switch (tipo)
        //    {
        //        case eTipoHabitacion.Single:
        //            return 1;
        //            break;
        //        case eTipoHabitacion.Doble:
        //            return 2;
        //            break;
        //        case eTipoHabitacion.Matrimonial:
        //            return 2;
        //            break;
        //        case eTipoHabitacion.Triple:
        //            return 3;
        //            break;
        //        case eTipoHabitacion.Cuadruple:
        //            return 4;
        //            break;
        //        default:
        //            return 0;
        //            break;
        //    }
        //}

        public static string GetServicioDescripcion(Guid id)
        {
            return new Services.ServicioService().GetByServicioId(id).Descripcion;
        }

        public static double GetPrecioServicio(Guid id)
        {
            return new Services.PrecioServicioService().GetAll().Where(pr => pr.Activo && pr.ServicioId == id).FirstOrDefault().Precio;
        }

        public static string GetViajeDescripcion(Guid id)
        {
            StringBuilder result = new StringBuilder();
            Services.PaqueteService paqueteService = new PaqueteService();
            Services.ViajeService viajeService = new ViajeService();
            Entities.Viaje viaje = viajeService.GetByViajeId(id);
            return string.Format("{0} - Salida {1}",paqueteService.GetByPaqueteId(viaje.PaqueteId.Value).Descripcion,viaje.FechaSalida.Value.ToShortDateString());
        }
    }
}
