using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RR.SoundService.Common;
using RR.SoundService.SoundServer.Models;
using Microsoft.Extensions.Configuration;

namespace RR.SoundService.SoundServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISoundService _soundService;
        private readonly IConfiguration _configuration;

        public HomeController(ISoundService soundService, IConfiguration configuration)
        {
            _soundService = soundService;
            _configuration = configuration;
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

    }
}