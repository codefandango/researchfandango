using CodeFandango.Flamenco.DataAccess;
using CodeFandango.Flamenco.Web.Portal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Diagnostics;

namespace CodeFandango.Flamenco.Web.Portal.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataAccess data;

        public HomeController(ILogger<HomeController> logger, IDataAccess data)
        {
            _logger = logger;
            this.data = data;
        }

        public IActionResult Index()
        {
            if (!data.Clients.IsClientConfigured())
            {
                return RedirectToAction("ConfigureClient");
            }
            return View();
        }

        public IActionResult ConfigureClient()
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
