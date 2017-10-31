using Main.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RR.AttributeService_V1;
using RR.Common_V1;
using RR.Logger.Extension;
//using RR.SoundService.Common;
using System.Runtime.InteropServices;

namespace Main.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAttributeService<ViewModelAttribute> _attributeService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IAttributeService<ViewModelAttribute> attributeService, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HomeController>();
            _logger.Log_Controller_Start();

            _attributeService = attributeService;

            _logger.Log_Controller_End();
        }

        [AutoValidateAntiforgeryToken]
        public IActionResult Index()
        {
            return View(new HomeViewModel()
            {
            });
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