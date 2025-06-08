using Microsoft.AspNetCore.Mvc;
using Preacepta.UI.Models;
using System.Diagnostics;

namespace Praecepta.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

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

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Attorneys()
        {
            return View();
        }

        public IActionResult Testimonials()
        {
            return View();
        }

        public IActionResult TestimonialForm()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }


        public IActionResult CaseStudyDetails()
        {
            return View("Practice/CaseStudyDetails");
        }

        public IActionResult AttorneyDetails()
        {
            return View("AttorneyDetails/AttorneyDetails");
        }





    }
}
