using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MAT.MVC.Infrastructure.Validation
{
    /// <summary>
    /// Fecha de nacimiento escrita como "dd/mm/aaaa": formato real, no futura y desde 1910.
    /// Emite tres reglas al navegador para mostrar el mensaje justo (formato / futura / mínima);
    /// las implementa <c>Scripts/mat.clientes.form.js</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class FechaNacimientoAttribute : ValidationAttribute, IClientValidatable
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var error = ClienteValidacion.ValidarFechaNacimiento(value as string, ClienteValidacion.HoyArgentina());
            return error == null ? ValidationResult.Success : new ValidationResult(error, new[] { validationContext.MemberName });
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule { ValidationType = "fechavalida", ErrorMessage = ClienteValidacion.MensajeFechaFormato };
            yield return new ModelClientValidationRule { ValidationType = "fechanofutura", ErrorMessage = ClienteValidacion.MensajeFechaFutura };
            var minima = new ModelClientValidationRule { ValidationType = "fechaminima", ErrorMessage = ClienteValidacion.MensajeFechaMinima };
            minima.ValidationParameters["anio"] = ClienteValidacion.AnioMinimoNacimiento;
            yield return minima;
        }
    }

    /// <summary>
    /// Si el teléfono está cargado, tiene que tener al menos 7 números (evita "387" o "0387").
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class TelefonoCompletoAttribute : ValidationAttribute, IClientValidatable
    {
        public TelefonoCompletoAttribute() : base(ClienteValidacion.MensajeTelefonoIncompleto) { }

        public override bool IsValid(object value)
        {
            return ClienteValidacion.TelefonoCompletoOVacio(value as string);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var regla = new ModelClientValidationRule { ValidationType = "telefonocompleto", ErrorMessage = ErrorMessageString };
            regla.ValidationParameters["minimo"] = ClienteValidacion.MinimoDigitosTelefono;
            yield return regla;
        }
    }

    /// <summary>
    /// Al menos uno de los dos teléfonos completo. Se pone sobre Celular e indica la otra propiedad.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class TelefonoOCelularAttribute : ValidationAttribute, IClientValidatable
    {
        public string OtraPropiedad { get; private set; }

        public TelefonoOCelularAttribute(string otraPropiedad) : base(ClienteValidacion.MensajeTelefonoOCelular)
        {
            OtraPropiedad = otraPropiedad;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var otra = validationContext.ObjectType.GetProperty(OtraPropiedad);
            var otroValor = otra == null ? null : otra.GetValue(validationContext.ObjectInstance) as string;
            return ClienteValidacion.TieneAlgunTelefono(otroValor, value as string)
                ? ValidationResult.Success
                : new ValidationResult(ErrorMessageString, new[] { validationContext.MemberName });
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var regla = new ModelClientValidationRule { ValidationType = "telefonoocelular", ErrorMessage = ErrorMessageString };
            regla.ValidationParameters["otro"] = OtraPropiedad;
            regla.ValidationParameters["minimo"] = ClienteValidacion.MinimoDigitosTelefono;
            yield return regla;
        }
    }

    /// <summary>
    /// Número de documento según el tipo (DNI 7–8 números, pasaporte 5–20 alfanuméricos, otros 6–10 números).
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DocumentoAttribute : ValidationAttribute, IClientValidatable
    {
        public string PropiedadTipo { get; private set; }

        public DocumentoAttribute(string propiedadTipo) : base("El número de documento no es válido.")
        {
            PropiedadTipo = propiedadTipo;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propiedad = validationContext.ObjectType.GetProperty(PropiedadTipo);
            var tipo = propiedad == null ? null : propiedad.GetValue(validationContext.ObjectInstance) as int?;
            var error = ClienteValidacion.ValidarDocumento(tipo, value as string);
            return error == null ? ValidationResult.Success : new ValidationResult(error, new[] { validationContext.MemberName });
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var regla = new ModelClientValidationRule { ValidationType = "documento", ErrorMessage = ErrorMessageString };
            regla.ValidationParameters["tipo"] = PropiedadTipo;
            yield return regla;
        }
    }
}
