using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

namespace WebApi.Security
{
    public class ReadableBodyStreamAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userName = context.HttpContext.User.Claims.First(Claim => Claim.Type == ClaimTypes.Name).Value;
            new ClaimsContext(userName);
        }
    }

    public class ClaimsContext
    {
        private static string _UserName;
        public ClaimsContext(string userName)
        {
            _UserName = userName;
        }

        public static string UserName()
        {
            return _UserName;
        }
    }
}
