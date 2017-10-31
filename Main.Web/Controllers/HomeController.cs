using Main.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RR.AttributeService_V1;
using RR.Common_V1;
using RR.Logger.Extension;
using System;
//using RR.SoundService.Common;
using System.Runtime.InteropServices;

namespace Main.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAttributeService<ViewModelAttribute> _attributeService;
        private readonly ILogger<HomeController> _logger;
        private readonly IOptions<AppSettings> _appSettings;

        public HomeController(IAttributeService<ViewModelAttribute> attributeService, ILoggerFactory loggerFactory, IOptions<AppSettings> appSettings)
        {
            _logger = loggerFactory.CreateLogger<HomeController>();
            _logger.Log_Controller_Start();

            _attributeService = attributeService;
            _appSettings = appSettings;


            _logger.Log_Controller_End();
        }

        [AutoValidateAntiforgeryToken]
        public IActionResult Index()
        {
            CookieOptions options = new CookieOptions();
            //options.Expires = DateTime.Now.AddDays(1);
            
            options.HttpOnly = false;
            options.SameSite = SameSiteMode.None;

            
            //settingValue.Ports.Add("Sound", 58157);
            var settingValue = new SettingsCookie();

            foreach (var p in _appSettings.Value.Ports)
            {
                settingValue.Ports.Add(p.Key, p.Value);
            }
            
            Response.Cookies.Append("SettingsCookie", JsonConvert.SerializeObject(settingValue), options);
            

            return View(new HomeViewModel());
        }
        
        [AutoValidateAntiforgeryToken]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [AutoValidateAntiforgeryToken]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public class PostResult
        {
            public string Name { get; set; } = "Sven";
        }
    }
}