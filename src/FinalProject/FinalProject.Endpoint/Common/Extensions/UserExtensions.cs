using System.Security.Claims;

namespace FinalProject.Endpoint.Common.Extensions
{
    public static class UserExtensions
    {
        public static bool IsExpert(this ClaimsPrincipal user)
        {
            return user.HasClaim("IsExpert", true.ToString());
        }

        public static bool IsCustomer(this ClaimsPrincipal user)
        {
            return user.HasClaim("IsCustomer", true.ToString());
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.HasClaim("IsAdmin", true.ToString());
        }
    }
}
