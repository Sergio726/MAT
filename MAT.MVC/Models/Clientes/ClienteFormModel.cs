using System;
using System.ComponentModel.DataAnnotations;
using MAT.MVC.Infrastructure.Validation;

namespace MAT.MVC.Models.Clientes
{
    /// <summary>
    /// Datos del formulario de cliente (alta, edición y alta desde presupuesto). Las etiquetas y reglas viven acá:
    /// la vista <c>_ClienteForm</c> las toma de los atributos y el navegador valida con las mismas reglas que el servidor.
    /// Los largos máximos coinciden con las columnas de Persona y Cliente.
    /// </summary>
    public sealed class ClienteFormModel
    {
        private const string Obligatorio = "Completá {0}.";
        private const string Largo = "{0} admite hasta {1} caracteres.";

        /// <summary>null en el alta.</summary>
        public Guid? PersonaId { get; set; }

        public bool EsAlta
        {
            get { return !PersonaId.HasValue; }
        }

        // Datos personales
        [Display(Name = "Apellido")]
        [Required(ErrorMessage = Obligatorio), StringLength(100, ErrorMessage = Largo)]
        public string Apellido { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = Obligatorio), StringLength(100, ErrorMessage = Largo)]
        public string Nombre { get; set; }

        [Display(Name = "Sexo")]
        public int? Sexo { get; set; }

        [Display(Name = "Tipo de documento")]
        [Required(ErrorMessage = "Elegí el tipo de documento.")]
        public int? TipoDocumento { get; set; }

        [Display(Name = "N° de documento")]
        [Required(ErrorMessage = Obligatorio), StringLength(50, ErrorMessage = Largo)]
        [Documento("TipoDocumento")]
        public string NroDocumento { get; set; }

        /// <summary>Texto "dd/mm/aaaa": así el formato y los mensajes los controla la validación propia.</summary>
        [Display(Name = "Fecha de nacimiento")]
        [Required(ErrorMessage = Obligatorio)]
        [FechaNacimiento]
        public string FechaNacimiento { get; set; }

        [Display(Name = "Nacionalidad")]
        [Required(ErrorMessage = Obligatorio), StringLength(50, ErrorMessage = Largo)]
        public string Nacionalidad { get; set; }

        [Display(Name = "País de residencia")]
        [Required(ErrorMessage = Obligatorio), StringLength(50, ErrorMessage = Largo)]
        public string PaisResidencia { get; set; }

        // Contacto
        [Display(Name = "Teléfono fijo")]
        [StringLength(50, ErrorMessage = Largo)]
        [TelefonoCompleto]
        public string Telefono { get; set; }

        [Display(Name = "Celular")]
        [StringLength(50, ErrorMessage = Largo)]
        [TelefonoCompleto]
        [TelefonoOCelular("Telefono")]
        public string Celular { get; set; }

        [Display(Name = "Email")]
        [StringLength(50, ErrorMessage = Largo)]
        [EmailAddress(ErrorMessage = "El email no es válido.")]
        public string Email { get; set; }

        [Display(Name = "Fax")]
        [StringLength(50, ErrorMessage = Largo)]
        public string Fax { get; set; }

        [Display(Name = "Sitio web")]
        [StringLength(50, ErrorMessage = Largo)]
        public string Web { get; set; }

        // Domicilio
        [Display(Name = "Domicilio")]
        [Required(ErrorMessage = Obligatorio), StringLength(100, ErrorMessage = Largo)]
        public string Domicilio { get; set; }

        [Display(Name = "Provincia")]
        [Required(ErrorMessage = "Elegí la provincia.")]
        public int? Provincia { get; set; }

        /// <summary>Solo para la cascada de la pantalla; no se guarda (sale de la localidad).</summary>
        [Display(Name = "Departamento")]
        public int? DepartamentoId { get; set; }

        [Display(Name = "Localidad")]
        [Required(ErrorMessage = "Elegí la localidad.")]
        [Range(1, int.MaxValue, ErrorMessage = "Elegí la localidad.")]
        public int? LocalidadId { get; set; }

        // Datos comerciales
        [Display(Name = "Razón social")]
        [StringLength(50, ErrorMessage = Largo)]
        public string RazonSocial { get; set; }

        [Display(Name = "CUIT / CUIL")]
        [StringLength(50, ErrorMessage = Largo)]
        public string Cuit { get; set; }

        [Display(Name = "Empresa")]
        [StringLength(50, ErrorMessage = Largo)]
        public string Empresa { get; set; }

        [Display(Name = "Condición IVA")]
        public int? CondicionIva { get; set; }

        [Display(Name = "Forma de pago")]
        public int? FormaPago { get; set; }

        [Display(Name = "Tipo de cliente")]
        [Required(ErrorMessage = "Elegí el tipo de cliente.")]
        public int? TipoId { get; set; }

        [Display(Name = "Cuenta corriente habilitada")]
        public bool CuentaCorrienteHabilitada { get; set; }

        // Otros
        [Display(Name = "Ocupación")]
        [StringLength(50, ErrorMessage = Largo)]
        public string Ocupacion { get; set; }

        [Display(Name = "Idioma")]
        [StringLength(50, ErrorMessage = Largo)]
        public string Idioma { get; set; }

        [Display(Name = "Promotor")]
        [StringLength(50, ErrorMessage = Largo)]
        public string Promotor { get; set; }

        [Display(Name = "Observaciones")]
        [StringLength(250, ErrorMessage = Largo)]
        public string Observacion { get; set; }

        /// <summary>Adónde volver después de guardar (por ejemplo, el presupuesto que originó el alta).</summary>
        public string ReturnUrl { get; set; }
    }
}
