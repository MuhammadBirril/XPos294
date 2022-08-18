using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using ViewModel;
using WebApp.Security;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountViewModel> _logger;
        private readonly AccountService _accountService;

        public AccountController(IConfiguration configuration, ILogger<AccountViewModel> logger, IHttpContextAccessor accessor)
        {
            _logger = logger;
            _accountService = new AccountService(configuration);
            new ContextAccessor(accessor);
        }

        public IActionResult Index(string returnUrl = "")
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        public IActionResult Login(string returnUrl = "")
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin model, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                AccountViewModel account = await _accountService.Authentication(model);
                if (account.Token != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, account.UserName),
                        new Claim("FullName", account.FullName),
                        new Claim("Token", account.Token),
                        new Claim("MenuRoles", String.Join(',', account.Role))
                        //Category, Variant, Product, Order, ...
                    };

                    foreach (var item in account.Role)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, item));
                    }

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));

                    if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["ErrorMessage"] = "Invalid login";
                }
            }
            return View("Index", model);
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Account");
        }

        public IActionResult AccessDenied(string returnUrl = "")
        {
            return View();
        }
    }
}
