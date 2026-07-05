using System;
using System.Web;
using System.Web.Security;

namespace MAT.MVC.Infrastructure
{
    public static class AdminAuthorizationHelper
    {
        public const string AdministratorRole = "Administrador";
        public const string AdminDevUserName = "admindev";

        public static bool IsAuthenticated(HttpContextBase httpContext)
        {
            return httpContext != null
                && httpContext.User != null
                && httpContext.User.Identity != null
                && httpContext.User.Identity.IsAuthenticated;
        }

        public static bool IsAdminDevAccount(string userName)
        {
            return !string.IsNullOrWhiteSpace(userName)
                && userName.Equals(AdminDevUserName, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsAdministrator(HttpContextBase httpContext)
        {
            if (!IsAuthenticated(httpContext))
                return false;

            try
            {
                return Roles.IsUserInRole(httpContext.User.Identity.Name, AdministratorRole);
            }
            catch (Exception ex)
            {
                ErrorUtil.LogAndGetPublicMessage(ex, "AdminAuthorizationHelper.IsAdministrator");
                return false;
            }
        }

        /// <summary>
        /// Historial de pagos sin filtro por vendedor: rol Administrador o cuenta dev <c>admindev</c>.
        /// </summary>
        public static bool CanViewAllHistorialPagos(HttpContextBase httpContext)
        {
            if (!IsAuthenticated(httpContext))
                return false;
            if (IsAdministrator(httpContext))
                return true;
            return IsAdminDevAccount(httpContext.User.Identity.Name);
        }
    }
}
