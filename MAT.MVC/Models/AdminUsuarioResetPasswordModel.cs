using System.ComponentModel.DataAnnotations;

namespace MAT.MVC.Models
{
    public class AdminUsuarioResetPasswordModel
    {
        public int UserId { get; set; }

        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos {2} caracteres.")]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva contraseña")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }
    }
}
