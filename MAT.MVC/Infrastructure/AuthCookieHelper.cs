using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MAT.MVC.Infrastructure
{
    /// <summary>
    /// Limpieza de cookies de autenticación/sesión para evitar acumulación
    /// (p. ej. .ASPXAUTH fragmentado tras reinicios de IIS Express).
    /// </summary>
    public static class AuthCookieHelper
    {
        private const int MaxAuthChunkSuffix = 8;

        private static readonly string[] LegacyCookieNames =
        {
            FormsAuthentication.FormsCookieName,
            ".ASPXAUTH",
            "__RequestVerificationToken",
            "ASP.NET_SessionId"
        };

        public static void ClearAuthenticationCookies(HttpContextBase context)
        {
            if (context == null)
            {
                return;
            }

            var expired = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var requestCookieNames = new List<string>();

            if (context.Request.Cookies != null)
            {
                var names = context.Request.Cookies.AllKeys;
                if (names != null)
                {
                    foreach (var name in names)
                    {
                        if (string.IsNullOrEmpty(name))
                        {
                            continue;
                        }

                        requestCookieNames.Add(name);
                        var existing = context.Request.Cookies[name];
                        var path = existing != null && !string.IsNullOrEmpty(existing.Path) ? existing.Path : "/";
                        ExpireCookie(context, expired, name, path);

                        if (path != "/")
                        {
                            ExpireCookie(context, expired, name, "/");
                        }
                    }
                }
            }

            foreach (var legacyName in LegacyCookieNames)
            {
                ExpireCookie(context, expired, legacyName, "/");
            }

            foreach (var name in requestCookieNames.Where(IsAuthCookieName))
            {
                ExpireAuthChunks(context, expired, GetAuthCookieBaseName(name));
            }

            foreach (var legacyName in LegacyCookieNames.Where(IsAuthCookieName))
            {
                ExpireAuthChunks(context, expired, legacyName);
            }
        }

        private static bool IsAuthCookieName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            return name.Equals(FormsAuthentication.FormsCookieName, StringComparison.OrdinalIgnoreCase)
                || name.Equals(".ASPXAUTH", StringComparison.OrdinalIgnoreCase)
                || name.Equals(".MATAuth", StringComparison.OrdinalIgnoreCase)
                || name.StartsWith(".ASPXAUTH", StringComparison.OrdinalIgnoreCase)
                || name.StartsWith(".MATAuth", StringComparison.OrdinalIgnoreCase);
        }

        private static string GetAuthCookieBaseName(string name)
        {
            if (string.Equals(name, FormsAuthentication.FormsCookieName, StringComparison.OrdinalIgnoreCase)
                || string.Equals(name, ".ASPXAUTH", StringComparison.OrdinalIgnoreCase)
                || string.Equals(name, ".MATAuth", StringComparison.OrdinalIgnoreCase))
            {
                return name;
            }

            if (name.StartsWith(".MATAuth", StringComparison.OrdinalIgnoreCase))
            {
                return ".MATAuth";
            }

            if (name.StartsWith(".ASPXAUTH", StringComparison.OrdinalIgnoreCase))
            {
                return ".ASPXAUTH";
            }

            return name;
        }

        private static void ExpireAuthChunks(HttpContextBase context, HashSet<string> expired, string baseName)
        {
            ExpireCookie(context, expired, baseName, "/");

            for (var i = 1; i <= MaxAuthChunkSuffix; i++)
            {
                ExpireCookie(context, expired, baseName + i, "/");
            }
        }

        private static void ExpireCookie(HttpContextBase context, HashSet<string> expired, string name, string path)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            var normalizedPath = string.IsNullOrEmpty(path) ? "/" : path;
            var key = name + "|" + normalizedPath;
            if (!expired.Add(key))
            {
                return;
            }

            var cookie = new HttpCookie(name, string.Empty)
            {
                Expires = DateTime.UtcNow.AddYears(-1),
                Path = normalizedPath
            };

            context.Response.Cookies.Add(cookie);
        }

        public static void AbandonSession(HttpContextBase context)
        {
            if (context?.Session == null)
            {
                return;
            }

            try
            {
                context.Session.Clear();
                context.Session.Abandon();
            }
            catch (Exception)
            {
                // La sesión puede no estar disponible en algunos requests
            }
        }
    }
}
