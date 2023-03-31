using Azure;
using HatliFood.Models;
using Microsoft.AspNetCore.Mvc;

namespace HatliFood.Controllers
{
    public class CartsController : Controller
    {
        public IActionResult Index()
        {
            var cookies = Request.Cookies;
            List<string> allCookies = new List<string>();

            foreach (var cookie in cookies)
            {
                if (cookie.Key.Contains("HatliFood-"))
                {
                    allCookies.Add(cookie.Value);
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddCookie(int id, string name, decimal price)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(1);
            options.HttpOnly = true;
            options.Secure = true; // Only send the cookie over HTTPS

            Response.Cookies.Append("HatliFood-" + id, name, options);
            return Json(new { success = true });
        }
    }


}
