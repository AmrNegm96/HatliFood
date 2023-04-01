using HatliFood.Data;
using HatliFood.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HatliFood.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger , ApplicationDbContext context)
        {
            _logger = logger;
            _context = context; 

        }

        public IActionResult Index()
        {
            ViewBag.Resturants = _context.Restaurant.ToList();
            ViewBag.MenuItems = _context.MenuItems.Include(m => m.CidNavigation).ToList();


            ViewBag.ResturantsCount = _context.Restaurant.ToList().Count();
            ViewBag.MenuItemsCount = _context.MenuItems.ToList().Count();



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