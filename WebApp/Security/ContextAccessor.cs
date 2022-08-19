using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApp.Security
{
    public class ContextAccessor
    {
        public static IHttpContextAccessor _accessor { get; set; }
        public ContextAccessor(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public static bool IsAuthenticated()
        {
            bool result = false;
            if (_accessor != null)
            {
                var ci = _accessor.HttpContext.User.Identity as ClaimsIdentity;
                result = ci.IsAuthenticated;
            }
            return result;
        }

        public static string GetUserName()
        {
            return GetBy("UserName");
        }

        public static string GetFullName()
        {
            return GetBy("FullName");
        }

        public static List<string> GetMenuRoles()
        {
            List<string> menus = GetBy("MenuRoles").Split(',').ToList();
            if (menus[0] == "")
                return new List<string>();
            return menus;
        }

        public static string GetToken()
        {
            return GetBy("Token");
        }

        private static string GetBy(string type)
        {
            string result = string.Empty;
            if (_accessor != null)
            {
                var ci = _accessor.HttpContext.User.Identity as ClaimsIdentity;
                if (ci.Claims.Count() > 0)
                {
                    result = ci.Claims.First(o => o.Type == (type == "UserName" ? ClaimTypes.Name : type)).Value;
                }
            }
            return result;
        }
    }
}

