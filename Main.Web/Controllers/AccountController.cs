using Main.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RR.Migration;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Main.Web.Controllers
{
    public class AccountController : Controller
    {
        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;

        //public AccountController(ILoggerFactory loggerFactory, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        public AccountController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<AccountController>();
            //_userManager = userManager;
            //_signInManager = signInManager;

            //var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            //var testUser1 = new ApplicationUser
            //{
            //    UserName = "Abcdefg",
            //    Name = "Luke",
            //    Vorname = "Skywalker"
            //};

            //var result = _userManager.CreateAsync(testUser1, "12003").Result;

        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new UserVievModel());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Account");
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

                //var userIdentity = new ClaimsIdentity(claims, "Cookieauth");
                var userIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                userIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, loginModel.Name));
                userIdentity.AddClaim(new Claim(ClaimTypes.Name, loginModel.Name));
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                {
                    IsPersistent = true
                });

                _logger.LogInformation(1, loginModel.Name + " logged in.");
                //Just redirect to our index after logging in. 
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("LoginError", "Login nicht möglich!");

            return View(loginModel);


            //ViewData["ReturnUrl"] = returnUrl;
            //if (ModelState.IsValid)
            //{
            //    // This doesn't count login failures towards account lockout
            //    // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            //    var result = await _signInManager.PasswordSignInAsync(model.Name,
            //        model.Password, true, lockoutOnFailure: false);
            //    if (result.Succeeded)
            //    {
            //        _logger.LogInformation(1, "User logged in.");
            //        if (!string.IsNullOrEmpty(returnUrl))
            //        {
            //            return Redirect(returnUrl);
            //        }
            //        else
            //        {
            //            return RedirectToAction("Index", "Home");
            //        }
            //    }
            //    if (result.IsLockedOut)
            //    {
            //        _logger.LogWarning(2, "User account locked out.");
            //        return View("Lockout");
            //    }
            //    else
            //    {
            //        ModelState.AddModelError("LoginError", "Login nicht möglich!");
            //        return View(model);
            //    }
            //}

            // If we got this far, something failed, redisplay form

        }

        private bool LoginUser(string username, string password)
        {
            //As an example. This method would go to our data store and validate that the combination is correct. 
            //For now just return true. 
            return username == "Luke" && password == "123";
        }

    }
}
