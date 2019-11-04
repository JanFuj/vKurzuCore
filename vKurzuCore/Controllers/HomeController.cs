using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using vKurzuCore.Data;
using vKurzuCore.Models;

namespace vKurzuCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly vKurzuDbContext applicationDbContext;

        public HomeController(ILogger<HomeController> logger, vKurzuDbContext applicationDbContext)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
           
          //  applicationDbContext.AdminNotes.
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
