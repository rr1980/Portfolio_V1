using Main.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Main.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        [HttpGet]
        [AllowAnonymous]
        [AutoValidateAntiforgeryToken]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new UserVievModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(UserVievModel loginModel, string returnUrl = null)
        {
            if (LoginUser(loginModel.Name, loginModel.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, loginModel.Name)
                };

                var userIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                userIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, loginModel.Name));
                userIdentity.AddClaim(new Claim(ClaimTypes.Name, loginModel.Name));
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                {
                    IsPersistent = true
                });

                _logger.LogInformation(1, loginModel.Name + " logged in...");
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            _logger.LogWarning(1, loginModel.Name + " Login nicht möglich!");
            ModelState.AddModelError("LoginError", "Login nicht möglich!");

            return View(loginModel);
        }

        [HttpPost]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Logout()
        {
            var ttt = User;
            _logger.LogInformation(1, User.Identity.Name + " logged out...");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        private bool LoginUser(string username, string password)
        {
            return username == "Luke" && password == "123";
        }
    }
}