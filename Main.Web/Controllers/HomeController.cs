using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Main.Web.Models;
using Microsoft.Extensions.Logging;
using RR.Common_V1;
using RR.AttributeService_V1;

namespace Main.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAttributeService<ViewModelAttribute> _attributeService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IAttributeService<ViewModelAttribute> attributeService, ILoggerFactory loggerFactory)
        {
            _attributeService = attributeService;
            _logger = loggerFactory.CreateLogger<HomeController>();
            _logger.LogTrace("HomeController init");
        }

        public IActionResult Index()
        {
            var data0 = _attributeService.GetAllByType(typeof(UserVievModel));
            //var data1 = _attributeService.GetAllByType<UserVievModel>();
            //var data2 = _attributeService.GetAllByObj<UserVievModel>(new UserVievModel());
            //var data3 = _attributeService.GetByProperty<UserVievModel>(o => o.Name);
            //var data4 = _attributeService.GetByName<UserVievModel>("Vorname");

            _logger.LogDebug("Call Index");

            return View(new UserVievModel());
        }

        public IActionResult Login(UserVievModel userVievModel)
        {
            _logger.LogDebug("Post Login");
            return View("Index", userVievModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
