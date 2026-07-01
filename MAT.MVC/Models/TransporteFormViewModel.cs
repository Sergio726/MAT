using MAT.Enums;
using MAT.Utilities;
using System;
using System.ComponentModel.DataAnnotations;

namespace MAT.MVC.Models
{
    public class TransporteFormViewModel
    {
        public Guid? TransporteId { get; set; }

        [Required(ErrorMessage = "El número de coche es obligatorio.")]
        [StringLength(50, ErrorMessage = "El número de coche no puede superar 50 caracteres.")]
        [Display(Name = "Nro. coche")]
        public string NroCoche { get; set; }

        [Required(ErrorMessage = "La capacidad máxima es obligatoria.")]
        [Range(1, 999, ErrorMessage = "Ingrese un valor entre 1 y 999.")]
        [Display(Name = "Máx. pasajeros")]
        public int? MaxPasajeros { get; set; }

        [Required(ErrorMessage = "La matrícula es obligatoria.")]
        [StringLength(10, ErrorMessage = "La matrícula no puede superar 10 caracteres.")]
        [Display(Name = "Matrícula")]
        public string Matricula { get; set; }

        [Required(ErrorMessage = "Seleccione el tipo de transporte.")]
        [Display(Name = "Tipo de transporte")]
        public string Tipo { get; set; }

        public bool IsEdit
        {
            get { return TransporteId.HasValue && TransporteId.Value != Guid.Empty; }
        }

        public static TransporteFormViewModel FromReader(
            Guid transporteId,
            string nroCoche,
            int? maxPasajeros,
            string matricula,
            string tipo)
        {
            return new TransporteFormViewModel
            {
                TransporteId = transporteId,
                NroCoche = nroCoche,
                MaxPasajeros = maxPasajeros,
                Matricula = matricula,
                Tipo = tipo
            };
        }

        public void Normalize()
        {
            NroCoche = NroCoche != null ? NroCoche.Trim() : null;
            Matricula = Matricula != null ? Matricula.Trim() : null;
            Tipo = Tipo != null ? Tipo.Trim() : null;
        }

        public bool IsTipoValid()
        {
            eTipoTransporte parsed;
            return EnumExtensions.TryParseTipoTransporteDbValue(Tipo, out parsed);
        }
    }
}
