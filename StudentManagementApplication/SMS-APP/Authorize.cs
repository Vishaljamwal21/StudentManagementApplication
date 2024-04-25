using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SMS_APP
{
    public class Authorize : IAuthorizationFilter
    {
        private readonly ILogger<Authorize> _logger;

        public Authorize(ILogger<Authorize> logger)
        {
            _logger = logger;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var tokenFromSession = SessionHandler.GetToken(context.HttpContext);
            if (string.IsNullOrEmpty(tokenFromSession))
            {
                context.Result = new RedirectToActionResult("Login", "User", null);
                return;
            }
            // Define your JWT secret key
            var secretKey = "veryLongAndSecureSecretKeyWith32CharactersMin";
            // Initialize the security key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            // Validate the JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(tokenFromSession, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var roleClaim = claimsPrincipal.FindFirst(ClaimTypes.Role); 
                if (roleClaim == null)
                {
                    _logger.LogInformation("Role claim is missing in the token.");
                    context.Result = new ForbidResult(); // Return 403 Forbidden if role claim is missing
                    return;
                }
                var userRole = roleClaim.Value;
                // Check if the user role allows access to the requested controller
                if (userRole == "Admin" || userRole == "User" || userRole == "Teacher")
                {
                    // Allow access to all controllers for Admin, User, and Teacher roles
                    return;
                }
                else
                {
                    _logger.LogInformation("Unknown role found in the token.");
                    context.Result = new ForbidResult(); // Return 403 Forbidden for unknown roles
                    return;
                }
            }
            catch (Exception)
            {
                // Redirect to login in case of token validation failure
                context.Result = new RedirectToActionResult("Login", "User", null);
            }
        }
    }
}
