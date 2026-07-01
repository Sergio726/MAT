using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using MAT.Enums;

namespace MAT.Utilities
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }

        public static string GetDescription<TEnum>(this TEnum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }

        public static string GetDisplayName(this Enum value)
        {
            if (value == null)
            {
                return null;
            }

            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name == null)
            {
                return null;
            }

            FieldInfo field = type.GetField(name);
            if (field == null)
            {
                return null;
            }

            var display = Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) as DisplayAttribute;
            if (display != null && !string.IsNullOrEmpty(display.Name))
            {
                return display.Name;
            }

            return name;
        }

        /// <summary>
        /// SelectList con Value = Description (código BD) y Text = Display Name (UI).
        /// </summary>
        public static IEnumerable<SelectListItem> ToSelectListByDbValue<TEnum>(string selectedDbValue = null)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(e =>
                {
                    var enumValue = (Enum)(object)e;
                    var dbValue = enumValue.GetDescription();
                    return new SelectListItem
                    {
                        Text = enumValue.GetDisplayName() ?? dbValue,
                        Value = dbValue,
                        Selected = !string.IsNullOrEmpty(selectedDbValue)
                            && string.Equals(dbValue, selectedDbValue, StringComparison.OrdinalIgnoreCase)
                    };
                })
                .ToList();
        }

        public static bool TryParseTipoTransporteDbValue(string dbValue, out eTipoTransporte tipo)
        {
            tipo = default(eTipoTransporte);
            if (string.IsNullOrWhiteSpace(dbValue))
            {
                return false;
            }

            foreach (eTipoTransporte item in Enum.GetValues(typeof(eTipoTransporte)))
            {
                var code = ((Enum)(object)item).GetDescription();
                if (string.Equals(code, dbValue.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    tipo = item;
                    return true;
                }
            }

            return false;
        }

        public static string GetTipoTransporteDisplayName(string dbValue)
        {
            eTipoTransporte tipo;
            if (!TryParseTipoTransporteDbValue(dbValue, out tipo))
            {
                return string.IsNullOrWhiteSpace(dbValue) ? null : dbValue;
            }

            return ((Enum)(object)tipo).GetDisplayName();
        }
    }
}
