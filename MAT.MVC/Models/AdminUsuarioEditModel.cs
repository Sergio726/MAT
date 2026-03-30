using System.ComponentModel.DataAnnotations;

namespace MAT.MVC.Models
{
    public class AdminUsuarioEditModel
    {
        public int UserId { get; set; }

        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Display(Name = "Es administrador")]
        public bool EsAdministrador { get; set; }

        [Display(Name = "Cuenta aprobada (puede iniciar sesión)")]
        public bool IsApproved { get; set; }

        public bool IsLockedOut { get; set; }
    }
}
