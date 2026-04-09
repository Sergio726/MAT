namespace MAT.MVC.Models
{
    public class AdminUserListItem
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string RolesSummary { get; set; }
        public bool IsApproved { get; set; }
        public bool IsLockedOut { get; set; }

        /// <summary>Muestra botón eliminar: no es el usuario actual ni el último administrador.</summary>
        public bool MostrarEliminar { get; set; }
    }
}
