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
using RR.Logger_V1.Extensions;
using Microsoft.AspNetCore.Authorization;
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

        //[AutoValidateAntiforgeryToken]
        //public IActionResult Login(UserVievModel userVievModel)
        //{
        //    List<TestClass> ttt = new List<TestClass>() { new TestClass() };
        //    _logger.Log_Controller_Call("Post Login", userVievModel, ttt);

        //    //try
        //    //{
        //    //    throw new Exception("Post war falsch!");
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    throw new Exception("HomeController hat Fehler!", ex);
        //    //}

        //    return View("Index", userVievModel);
        //}

        //private const int APPCOMMAND_VOLUME_MUTE = 0x80000;
        //private const int APPCOMMAND_VOLUME_UP = 0xA0000;
        //private const int APPCOMMAND_VOLUME_DOWN = 0x90000;
        //private const int WM_APPCOMMAND = 0x319;

        //[DllImport("user32.dll")]
        //public static extern IntPtr SendMessageW(IntPtr hWnd, int Msg,
        //IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/dd375731(v=vs.85).aspx

        [AllowAnonymous]
        [AutoValidateAntiforgeryToken]
        public PostResult Test(string name, string location)
        {
            keybd_event(0xAD, 0, 0, 0);
            //SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle,
            //    (IntPtr)APPCOMMAND_VOLUME_MUTE);

            //try
            //{
            //    throw new Exception("Post war falsch!");
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("HomeController hat Fehler!", ex);
            //}

            return new PostResult();
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

        public class PostResult
        {
            public string Name { get; set; } = "Sven";
        }
    }
}
