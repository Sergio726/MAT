using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MAT.MVC.Models
{
    public class AdminUsuarioRolItemModel
    {
        public string RoleName { get; set; }
        public bool Selected { get; set; }
    }

    public class AdminUsuarioEditModel
    {
        public int UserId { get; set; }

        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Display(Name = "Es administrador")]
        public bool EsAdministrador { get; set; }

        public IList<AdminUsuarioRolItemModel> Roles { get; set; }

        [Display(Name = "Cuenta aprobada (puede iniciar sesión)")]
        public bool IsApproved { get; set; }

        public bool IsLockedOut { get; set; }

        public AdminUsuarioEditModel()
        {
            Roles = new List<AdminUsuarioRolItemModel>();
        }
    }
}
