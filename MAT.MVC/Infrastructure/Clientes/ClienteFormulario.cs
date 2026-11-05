using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using MAT.Entities;
using MAT.Enums;
using MAT.MVC.Models.Clientes;
using MAT.Utilities;

namespace MAT.MVC.Infrastructure.Clientes
{
    /// <summary>
    /// Valores iniciales del formulario de cliente. Antes se ponían con jQuery al cargar la página.
    /// </summary>
    public static class ClienteFormDefaults
    {
        public const int ProvinciaSalta = 16;

        public static ClienteFormModel Nuevo()
        {
            return new ClienteFormModel
            {
                TipoDocumento = (int)eTipoDocumento.DNI,
                Nacionalidad = "ARG",
                PaisResidencia = "ARG",
                Provincia = ProvinciaSalta,
                CondicionIva = (int)eCondicionIVA.Consumidor_Final,
                FormaPago = (int)eFormaPago.Contado,
                TipoId = (int)eTipoCliente.Minorista,
                CuentaCorrienteHabilitada = true
            };
        }
    }

    /// <summary>
    /// Opciones de los combos del formulario, siempre con la descripción legible del enum
    /// (antes la edición mostraba el nombre interno, por ejemplo "Consumidor_Final").
    /// </summary>
    public sealed class ClienteFormOpciones
    {
        public IList<SelectListItem> Sexos { get; private set; }
        public IList<SelectListItem> TiposDocumento { get; private set; }
        public IList<SelectListItem> CondicionesIva { get; private set; }
        public IList<SelectListItem> FormasPago { get; private set; }
        public IList<SelectListItem> TiposCliente { get; private set; }
        public IList<SelectListItem> Provincias { get; private set; }
        public IList<SelectListItem> Ocupaciones { get; private set; }
        /// <summary>País del domicilio (solo los que tienen provincias cargadas): filtra las provincias.</summary>
        public IList<SelectListItem> PaisesDomicilio { get; private set; }
        /// <summary>Nacionalidad y país de residencia: valor = código ISO (ARG), texto = nombre.</summary>
        public IList<SelectListItem> Nacionalidades { get; private set; }
        public IList<SelectListItem> PaisesResidencia { get; private set; }
        /// <summary>Todas las provincias con su país, para que la pantalla filtre sin volver al servidor.</summary>
        public IList<ProvinciaCatalogo> TodasLasProvincias { get; private set; }

        /// <remarks>Completa <see cref="ClienteFormModel.PaisId"/> desde la provincia (o Argentina) si viene vacío.</remarks>
        public static ClienteFormOpciones Crear(ClienteFormModel modelo, ClienteCatalogos catalogos)
        {
            catalogos = catalogos ?? new ClienteCatalogos();

            var listaOcupaciones = catalogos.Ocupaciones
                .Where(o => !string.IsNullOrWhiteSpace(o))
                .Select(o => o.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
            // Un cliente viejo puede tener una ocupación que no está en el catálogo: se conserva.
            if (!string.IsNullOrWhiteSpace(modelo.Ocupacion) && !listaOcupaciones.Contains(modelo.Ocupacion.Trim(), StringComparer.OrdinalIgnoreCase))
                listaOcupaciones.Insert(0, modelo.Ocupacion.Trim());

            // País del domicilio: el de la provincia guardada; si no hay, Argentina.
            if (!modelo.PaisId.HasValue)
            {
                var provincia = catalogos.Provincias.FirstOrDefault(p => p.Id == modelo.Provincia);
                var argentina = catalogos.Paises.FirstOrDefault(p => p.Codigo == "ARG");
                modelo.PaisId = provincia != null ? provincia.PaisId : argentina?.PaisId;
            }

            var paisesConCodigo = catalogos.Paises.Where(p => !string.IsNullOrEmpty(p.Codigo)).ToList();
            return new ClienteFormOpciones
            {
                Sexos = ConVacio(DeEnum<eSexo>(), "Seleccioná…"),
                TiposDocumento = DeEnum<eTipoDocumento>(),
                CondicionesIva = DeEnum<eCondicionIVA>(),
                FormasPago = DeEnum<eFormaPago>(),
                TiposCliente = DeEnum<eTipoCliente>(),
                PaisesDomicilio = catalogos.Paises.Where(p => p.TieneProvincias)
                    .Select(p => new SelectListItem { Value = p.PaisId.ToString(), Text = p.Nombre }).ToList(),
                Provincias = ConVacio(catalogos.Provincias.Where(p => p.PaisId == modelo.PaisId)
                    .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Nombre }).ToList(), "Seleccioná la provincia…"),
                TodasLasProvincias = catalogos.Provincias,
                Nacionalidades = ConVacio(Paises(paisesConCodigo, modelo.Nacionalidad), "Seleccioná…"),
                PaisesResidencia = ConVacio(Paises(paisesConCodigo, modelo.PaisResidencia), "Seleccioná…"),
                Ocupaciones = ConVacio(listaOcupaciones.Select(o => new SelectListItem { Value = o, Text = o }).ToList(), "Sin especificar")
            };
        }

        /// <summary>Países por código; un valor viejo que no es un código (por ejemplo "ARGENTINA") se conserva para no perderlo.</summary>
        private static List<SelectListItem> Paises(IList<PaisCatalogo> paises, string valorActual)
        {
            var items = paises.Select(p => new SelectListItem { Value = p.Codigo, Text = p.Nombre }).ToList();
            if (!string.IsNullOrWhiteSpace(valorActual) && !paises.Any(p => string.Equals(p.Codigo, valorActual.Trim(), StringComparison.OrdinalIgnoreCase)))
                items.Insert(0, new SelectListItem { Value = valorActual.Trim(), Text = valorActual.Trim() + " (revisar)" });
            return items;
        }

        private static List<SelectListItem> DeEnum<TEnum>() where TEnum : struct
        {
            return Enum.GetValues(typeof(TEnum)).Cast<Enum>()
                .Select(e => new SelectListItem { Value = Convert.ToInt32(e).ToString(), Text = e.GetDescription() })
                .ToList();
        }

        private static List<SelectListItem> ConVacio(List<SelectListItem> items, string texto)
        {
            items.Insert(0, new SelectListItem { Value = string.Empty, Text = texto });
            return items;
        }
    }

    /// <summary>
    /// Pasa del formulario a la base y vuelta, normalizando lo que se guarda.
    /// </summary>
    public static class ClienteFormMapper
    {
        private static readonly Regex Espacios = new Regex(@"\s+", RegexOptions.Compiled);

        public static ClienteFormModel DesdeEntidades(Persona persona, Cliente cliente, bool? cuentaHabilitada)
        {
            return new ClienteFormModel
            {
                PersonaId = persona.PersonaId,
                Apellido = persona.Apellido?.Trim(),
                Nombre = persona.Nombre?.Trim(),
                Sexo = persona.Sexo,
                TipoDocumento = persona.TipoDocumento ?? (int)eTipoDocumento.DNI,
                NroDocumento = persona.NroDocumento?.Trim(),
                FechaNacimiento = persona.FechaNacimiento.HasValue ? persona.FechaNacimiento.Value.ToString(ClienteValidacion.FormatoFecha) : null,
                Nacionalidad = persona.Nacionalidad?.Trim(),
                PaisResidencia = persona.PaisResidencia?.Trim(),
                Telefono = persona.Telefono?.Trim(),
                Celular = persona.Celular?.Trim(),
                Email = persona.Email?.Trim(),
                Domicilio = persona.Domicilio?.Trim(),
                Provincia = persona.Provincia,
                LocalidadId = persona.LocalidadId,
                Ocupacion = persona.Ocupacion?.Trim(),
                RazonSocial = cliente.RazonSocial?.Trim(),
                Cuit = cliente.Cuit?.Trim(),
                Empresa = cliente.Empresa?.Trim(),
                CondicionIva = cliente.CondicionIva,
                FormaPago = cliente.FormaPago,
                TipoId = cliente.TipoId,
                Fax = cliente.Fax?.Trim(),
                Web = cliente.Web?.Trim(),
                Idioma = cliente.Idioma?.Trim(),
                Promotor = cliente.Promotor?.Trim(),
                Observacion = cliente.Observacion?.Trim(),
                CuentaCorrienteHabilitada = cuentaHabilitada ?? true
            };
        }

        /// <summary>
        /// Deja el modelo como se guarda: textos sin espacios de sobra, apellido y razón social en mayúsculas
        /// (como hasta ahora), DNI con el formato de la base y opcionales vacíos como null.
        /// </summary>
        public static void Normalizar(ClienteFormModel m)
        {
            m.Apellido = Texto(m.Apellido)?.ToUpperInvariant();
            m.Nombre = Texto(m.Nombre);
            m.NroDocumento = NormalizarDocumento(m.TipoDocumento, m.NroDocumento);
            m.FechaNacimiento = Texto(m.FechaNacimiento);
            m.Nacionalidad = Texto(m.Nacionalidad)?.ToUpperInvariant();
            m.PaisResidencia = Texto(m.PaisResidencia)?.ToUpperInvariant();
            m.Telefono = Texto(m.Telefono);
            m.Celular = Texto(m.Celular);
            m.Email = Texto(m.Email)?.ToLowerInvariant();
            m.Fax = Texto(m.Fax);
            m.Web = Texto(m.Web);
            m.Domicilio = Texto(m.Domicilio);
            m.RazonSocial = Texto(m.RazonSocial)?.ToUpperInvariant();
            m.Cuit = Texto(m.Cuit);
            m.Empresa = Texto(m.Empresa);
            m.Ocupacion = Texto(m.Ocupacion);
            m.Idioma = Texto(m.Idioma);
            m.Promotor = Texto(m.Promotor);
            m.Observacion = Texto(m.Observacion);
        }

        /// <summary>
        /// DNI: solo números, sin ceros a la izquierda y con el formato que ya tienen los clientes cargados
        /// ("5787839" → "05.787.839", "32877563" → "32.877.563"). Otros documentos: sin espacios y en mayúsculas.
        /// </summary>
        public static string NormalizarDocumento(int? tipoDocumento, string numero)
        {
            var texto = Texto(numero);
            if (texto == null) return null;
            if (tipoDocumento != null && tipoDocumento != (int)eTipoDocumento.DNI)
                return texto.Replace(" ", string.Empty).ToUpperInvariant();

            var digitos = new string(texto.Where(char.IsDigit).ToArray()).TrimStart('0');
            if (digitos.Length == 0 || digitos.Length > 8) return texto;
            var relleno = digitos.PadLeft(8, '0');
            return relleno.Substring(0, 2) + "." + relleno.Substring(2, 3) + "." + relleno.Substring(5, 3);
        }

        private static string Texto(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor)) return null;
            return Espacios.Replace(valor.Trim(), " ");
        }
    }
}
