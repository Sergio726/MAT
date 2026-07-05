using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using TB.ComponentModel;
using System.Web.Mvc;
using MAT.Entities;
using MAT.Enums;
using System.Web;

namespace MAT.Utilities
{
    public static class Helper
    {
        public static void FillEntity<T>(ref T entity, NameValueCollection datos)
        {
            PropertyInfo[] entityProperties = typeof(T).GetProperties();
            foreach (PropertyInfo item in entityProperties)
            {
                string propName = item.Name;
                var bindingFlags = BindingFlags.Public | BindingFlags.Instance;
                var propertyInfo = entity.GetType().GetProperty(propName, bindingFlags);
                if (propertyInfo == null) { continue; }
                Type propertyType = propertyInfo.PropertyType;
                if (datos[propName] != null)
                {
                    object obj;
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
            PropertyInfo[] entityProperties = typeof(T).GetProperties();
            foreach (PropertyInfo item in entityProperties)
            {
                string propName = item.Name;
                var bindingFlags = BindingFlags.Public | BindingFlags.Instance;
                var propertyInfo = entity.GetType().GetProperty(propName, bindingFlags);
                if (propertyInfo == null) { continue; }
                Type propertyType = propertyInfo.PropertyType;
                if (!string.IsNullOrEmpty(datos[propName]))
                {
                    object obj;
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

        private static bool IsGenericNullable(Type type)
        {
            return type.IsGenericType &&
                type.GetGenericTypeDefinition() == typeof(Nullable<>).GetGenericTypeDefinition();
        }

        public static IEnumerable<SelectListItem> ToSelectList<TEnum>(this TEnum enumObj)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            IEnumerable<TEnum> tipos = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
            return tipos.Select(e => new SelectListItem
            {
                Text = e.GetDescription(),
                Value = Convert.ToInt32(e).ToString()
            });
        }

        public static IEnumerable<SelectListItem> ToSelectList<TEnum>(this TEnum enumObj, string selectvalue)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            IEnumerable<TEnum> tipos = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
            var listitem = tipos.Select(e => new SelectListItem
            {
                Text = e.ToString(),
                Value = Convert.ToInt32(e).ToString()
            }).ToList();
            var selected = listitem.FirstOrDefault(e => e.Value == selectvalue);
            if (selected != null)
            {
                selected.Selected = true;
            }
            return listitem;
        }

        public static IEnumerable<SelectListItem> ToSelectEntities(string Entidad)
        {
            return ToSelectEntities(Entidad, null);
        }

        public static IEnumerable<SelectListItem> ToSelectEntities(string Entidad, Guid? selected)
        {
            List<LookupDataAccess.LookupItem> items;
            switch (Entidad)
            {
                case "Localidad":
                    return Enumerable.Empty<SelectListItem>();
                case "Paquete":
                    items = LookupDataAccess.GetPaqueteSelectItems();
                    break;
                case "Transporte":
                    items = LookupDataAccess.GetTransporteSelectItems();
                    break;
                case "Hotel":
                    items = LookupDataAccess.GetHotelSelectItems();
                    break;
                case "Proveedor":
                    items = LookupDataAccess.GetProveedorSelectItems();
                    break;
                case "Provincia":
                    return GeoDataAccess.GetAllProvincias().Select(e => new SelectListItem
                    {
                        Text = e.Nombre,
                        Value = Convert.ToString(e.Id),
                        Selected = selected.HasValue && e.Id.ToString() == selected.Value.ToString()
                    });
                case "Viaje":
                    items = LookupDataAccess.GetViajeSelectItems();
                    break;
                default:
                    return Enumerable.Empty<SelectListItem>();
            }

            return items.Select(e => new SelectListItem
            {
                Text = e.Text,
                Value = e.Value,
                Selected = selected.HasValue && e.Value == selected.Value.ToString()
            });
        }

        public static IEnumerable<SelectListItem> ToSelectEntitiesLocalidades(string Entidad, int? selected)
        {
            switch (Entidad)
            {
                case "Provincia":
                    var listitem = GeoDataAccess.GetAllProvincias().Select(e => new SelectListItem
                    {
                        Text = e.Nombre,
                        Value = Convert.ToString(e.Id)
                    }).ToList();
                    if (selected.HasValue)
                    {
                        var match = listitem.FirstOrDefault(it => it.Value == selected.Value.ToString());
                        if (match != null)
                        {
                            match.Selected = true;
                        }
                    }
                    return listitem;
                default:
                    return Enumerable.Empty<SelectListItem>();
            }
        }

        public static string ToSelectItem(string Entidad, string selectvalue)
        {
            if (string.IsNullOrEmpty(selectvalue))
            {
                return "Sin Definir";
            }

            switch (Entidad)
            {
                case "Localidad":
                    int localidadId;
                    if (!int.TryParse(selectvalue, out localidadId))
                    {
                        return "Sin Definir";
                    }
                    var localidad = GeoDataAccess.GetLocalidadById(localidadId);
                    return localidad != null ? localidad.Nombre : "Sin Definir";
                case "Transporte":
                    Guid transporteId;
                    if (!Guid.TryParse(selectvalue, out transporteId))
                    {
                        return "Sin Definir";
                    }
                    return LookupDataAccess.GetTransporteDisplayText(transporteId);
                case "Paquete":
                    Guid paqueteId;
                    if (!Guid.TryParse(selectvalue, out paqueteId))
                    {
                        return "Sin Definir";
                    }
                    return LookupDataAccess.GetPaqueteDisplayText(paqueteId);
                case "Hotel":
                    Guid hotelId;
                    if (!Guid.TryParse(selectvalue, out hotelId))
                    {
                        return "Sin Definir";
                    }
                    return LookupDataAccess.GetHotelDisplayText(hotelId);
                case "Provincia":
                    int provinciaId;
                    if (!int.TryParse(selectvalue, out provinciaId))
                    {
                        return "Sin Definir";
                    }
                    var provincia = GeoDataAccess.GetProvinciaById(provinciaId);
                    return provincia != null ? provincia.Nombre : "Sin Definir";
                default:
                    return "Sin Definir";
            }
        }

        public static bool IsNumeric(object expression)
        {
            double retNum;
            return double.TryParse(Convert.ToString(expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        }

        public static string GetLocalidadName(int localidadId)
        {
            var localidad = GeoDataAccess.GetLocalidadById(localidadId);
            return localidad != null ? localidad.Nombre : "Sin Definir";
        }

        public static void AlertMessage(string message, System.Web.UI.Page ctxpage)
        {
            var safeMessage = System.Web.HttpUtility.JavaScriptStringEncode(message ?? string.Empty);
            var sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(safeMessage);
            sb.Append("')};");
            sb.Append("</script>");
            ctxpage.ClientScript.RegisterClientScriptBlock(ctxpage.GetType(), "alert", sb.ToString());
        }

        public static void GenerarVoucher(ref Pasaje pasaje)
        {
            LookupDataAccess.GenerarVoucher(ref pasaje);
        }

        public static string GetVendedorName(Guid id)
        {
            return LookupDataAccess.GetVendedorDisplayName(id);
        }

        public static string GetDisponibilidad(Guid habitacionid, Guid viajeid)
        {
            return LookupDataAccess.GetDisponibilidad(habitacionid, viajeid);
        }

        public static bool IsHabitacionDisponible(Guid habitacionid, Guid viajeid)
        {
            return LookupDataAccess.IsHabitacionDisponible(habitacionid, viajeid);
        }

        public static string GetServicioDescripcion(Guid id)
        {
            return LookupDataAccess.GetServicioDescripcion(id);
        }

        public static double GetPrecioServicio(Guid id)
        {
            return LookupDataAccess.GetPrecioServicio(id);
        }

        public static string GetViajeDescripcion(Guid id)
        {
            return LookupDataAccess.GetViajeDescripcion(id);
        }
    }
}
