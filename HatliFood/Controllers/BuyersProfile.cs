using HatliFood.Data;
using HatliFood.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HatliFood.Controllers
{
    public class BuyersProfile : Controller
    {

        public UserManager<IdentityUser> _UserManager;
        private readonly ApplicationDbContext _context;

        public BuyersProfile(UserManager<IdentityUser> UserManager, ApplicationDbContext context)
        {
            _UserManager = UserManager;
            _context = context;
        }

        public IActionResult Index()
        {
            var userID =  _UserManager.GetUserId(User);
            Buyer? buyer = _context.Buyers.FirstOrDefault(b=>b.UserId == userID);

            ViewBag.Buyer = buyer;
            return View();
        }

        public IActionResult UpdatePhone()
        {
            var userID = _UserManager.GetUserId(User);
            Buyer? buyer = _context.Buyers.FirstOrDefault(b => b.UserId == userID);

            ViewBag.Buyer = buyer;
            return View();
        }

        public IActionResult UpdatePassword(string password)
        {
            var userID = _UserManager.GetUserId(User);
            Buyer? buyer = _context.Buyers.FirstOrDefault(b => b.UserId == userID);

            ViewBag.Buyer = buyer;
            return View();
        }
    }
}
