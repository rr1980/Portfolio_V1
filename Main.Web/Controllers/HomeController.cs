using Main.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RR.AttributeService_V1;
using RR.Common_V1;
using RR.Logger.Extension;
using System.Runtime.InteropServices;

namespace Main.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAttributeService<ViewModelAttribute> _attributeService;
        private readonly ILogger<HomeController> _logger;
        private readonly ISoundService _soundService;

        public HomeController(ISoundService soundService, IAttributeService<ViewModelAttribute> attributeService, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HomeController>();
            _logger.Log_Controller_Start();

            _soundService = soundService;

            _attributeService = attributeService;

            _logger.Log_Controller_End();
        }

        [AutoValidateAntiforgeryToken]
        public IActionResult Index()
        {
            var volP = _soundService.GetVolumeInPercent();

            return View(new HomeViewModel()
            {
                Mute = volP.Item1,
                VolumnPercent = volP.Item2
            });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public (bool, int) GetVolume()
        {
            return _soundService.GetVolumeInPercent();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public (bool, int) VolumeStepUp()
        {
            return  _soundService.VolumeStepUp();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public (bool, int) ToggleMute()
        {
            return  _soundService.ToggleMute();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public (bool, int) VolumeStepDown()
        {
            return  _soundService.VolumeStepDown();
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



//[DllImport("user32.dll")]
//private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

//// https://msdn.microsoftcom/en-us/library/windows/desktop/dd375731(v=vs.85).aspx

//keybd_event(0xAD, 0, 0, 0);