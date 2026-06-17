using System;
using System.Web;
using System.Web.Security;

namespace MAT.MVC.Infrastructure
{
    public static class AdminAuthorizationHelper
    {
        public const string AdministratorRole = "Administrador";

        public static bool IsAuthenticated(HttpContextBase httpContext)
        {
            return httpContext != null
                && httpContext.User != null
                && httpContext.User.Identity != null
                && httpContext.User.Identity.IsAuthenticated;
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
    }
}
