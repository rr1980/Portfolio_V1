using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RR.SoundService.SoundServer.Models;
using RR.SoundService.Common;
using RR.AttributeService_V1;
using Microsoft.Extensions.Logging;
using RR.Common_V1;
using RR.Logger.Extension;
using Microsoft.Extensions.Configuration;

namespace RR.SoundService.SoundServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAttributeService<ViewModelAttribute> _attributeService;
        private readonly ILogger<HomeController> _logger;
        private readonly ISoundService _soundService;
        private readonly IConfiguration _configuration;

        public HomeController(ISoundService soundService, IAttributeService<ViewModelAttribute> attributeService, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _logger = loggerFactory.CreateLogger<HomeController>();
            _logger.Log_Controller_Start();

            _soundService = soundService;
            _attributeService = attributeService;
            _configuration = configuration;

            _logger.Log_Controller_End();
        }

        //[AutoValidateAntiforgeryToken]
        public IActionResult Index()
        {
            ViewData["Port"] = _configuration.GetSection("Port").Value;

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
            return _soundService.VolumeStepUp();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public (bool, int) ToggleMute()
        {
            return _soundService.ToggleMute();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public (bool, int) VolumeStepDown()
        {
            return _soundService.VolumeStepDown();
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