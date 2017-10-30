using Main.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RR.AttributeService_V1;
using RR.Common_V1;
using RR.Logger_V1.Extensions;
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
            //var data0 = _attributeService.GetAllByType(typeof(UserVievModel));
            //var data1 = _attributeService.GetAllByType<UserVievModel>();
            //var data2 = _attributeService.GetAllByObj<UserVievModel>(new UserVievModel());
            //var data3 = _attributeService.GetByProperty<UserVievModel>(o => o.Name);
            //var data4 = _attributeService.GetByName<UserVievModel>("Vorname");

            _logger.LogDebug("Call Index");

            return View(new UserVievModel());
        }

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/dd375731(v=vs.85).aspx

        //[AllowAnonymous]
        [AutoValidateAntiforgeryToken]
        public PostResult Test(string name, string location)
        {
            keybd_event(0xAD, 0, 0, 0);
            return new PostResult();
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