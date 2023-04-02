using HatliFood.Data;
using HatliFood.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using RestSharp;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Linq;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using NuGet.Protocol;

namespace HatliFood.Controllers
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
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
            
            var cookies = Response.Cookies;
            cookies.Append("isAuth", User.Identity.IsAuthenticated.ToString());
            cookies.Append("UserRole", User.IsInRole("User").ToString());


                var AllResturant = _context.Restaurant;
                ViewBag.Resturants = AllResturant.ToList();
                ViewBag.MenuItems = _context.MenuItems.Include(m => m.CidNavigation).ToList();


                ViewBag.ResturantsCount = _context.Restaurant.ToList().Count();
                ViewBag.MenuItemsCount = _context.MenuItems.ToList().Count();
                return View();

         
        }

        [Route("/GetSearchedData/{id:alpha}")]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public JsonResult GetSearchedData(string id)
        {
            var restru = _context.Restaurant.Where(s=>s.Name.ToLower().Contains(id.ToLower())).ToArray();
            return Json( restru);
        }



        public IActionResult AdminHome()
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