using Main.Web.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Main.Web.Controllers
{
    public class ErrorController : Controller
    {

        public IActionResult Index()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionFeature != null)
            {
                var errorViewModel = _getErrorViewModel(exceptionFeature);
                return View(errorViewModel);
            }

            return View();
        }

        private ErrorViewModel _getErrorViewModel(IExceptionHandlerPathFeature exceptionFeature)
        {
            return new ErrorViewModel()
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Message = exceptionFeature.Error.Message,
                Place = exceptionFeature.Error.TargetSite.DeclaringType.FullName + ": " + exceptionFeature.Error.TargetSite.Name
            };
        }
    }
}
