using JobNest.Data;
using JobNest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JobNest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Index()
        {
            // Retrieve the current user
            ApplicationUser currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null)
            {
                // Save the user ID in TempData
                TempData["UserId"] = currentUser.Id;
            }

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
