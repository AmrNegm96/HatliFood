using Azure;
using HatliFood.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HatliFood.Controllers
{
    public class CartsController : Controller
    {
        public UserManager<IdentityUser> _UserManager;

        public CartsController(UserManager<IdentityUser> UserManager)
        {
            _UserManager = UserManager;
        }
        public IActionResult Index()
        {
            var cookies = Request.Cookies;
            List<string> allCookies = new List<string>();

            var id = _UserManager.GetUserId(User);


            foreach (var cookie in cookies)
            {
                if (cookie.Key.Contains("HatliFood-"))
                {
                    allCookies.Add(cookie.Value);
                }
            }


            ViewBag.AllCookies = allCookies;
            return View();
        }

        [HttpPost]
        public IActionResult AddCookie(int id, string name, decimal price)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(1);
            options.HttpOnly = true;
            options.Secure = true; // Only send the cookie over HTTPS

            Response.Cookies.Append("HatliFood-" + id, JsonSerializer.Serialize(new { Id = id, Name = name, Price = price }) , options);

            return Json(new { success = true });
        }
        public class CartProperties
        {
            public string Name { get; set; }
            public int Id { get; set; }
            public decimal Price { get; set; }  
        }
    }


}
