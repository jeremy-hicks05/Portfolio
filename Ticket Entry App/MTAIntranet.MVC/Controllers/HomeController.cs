using Microsoft.AspNetCore.Mvc;
using MTAIntranet.MVC.Models;
using MTAIntranet.SQL.API;
using System.Diagnostics;

namespace MTAIntranetMVC.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly MtaticketsContext db;
        public HomeController(MtaticketsContext injectedContext)
        {
            db= injectedContext;
        }

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}