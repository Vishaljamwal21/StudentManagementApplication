using Microsoft.AspNetCore.Http;

namespace SMS_APP
{
    public class SessionHandler
    {
        private const string TokenKey = "Token";
        private const string UserEmailKey = "UserName";
        private const string UserRoleKey = "UserRole";

        public static void SetToken(HttpContext context, string token)
        {
            context.Session.SetString(TokenKey, token);
        }

        public static string GetToken(HttpContext context)
        {
            return context.Session.GetString(TokenKey);
        }

        public static void RemoveToken(HttpContext context)
        {
            context.Session.Remove(TokenKey);
        }

        public static void SetUserEmail(HttpContext context, string Email)
        {
            context.Session.SetString(UserEmailKey, Email);
        }

        public static string GetUserEmail(HttpContext context)
        {
            return context.Session.GetString(UserEmailKey);
        }

        public static void RemoveUserEmail(HttpContext context)
        {
            context.Session.Remove(UserEmailKey);
        }

        public static void SetUserRole(HttpContext context, string role)
        {
            context.Session.SetString(UserRoleKey, role);
        }

        public static string GetUserRole(HttpContext context)
        {
            return context.Session.GetString(UserRoleKey);
        }

        public static void RemoveUserRole(HttpContext context)
        {
            context.Session.Remove(UserRoleKey);
        }
    }
}
