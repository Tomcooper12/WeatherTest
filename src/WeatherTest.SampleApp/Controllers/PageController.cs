using Microsoft.AspNetCore.Mvc;

namespace WeatherTest.SampleApp.Controllers
{
    public class PageController : Controller
    {
        public IActionResult Index()
        {
            return View("~/View/Index.cshtml");
        }
    }
}