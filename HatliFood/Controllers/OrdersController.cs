using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HatliFood.Data;
using HatliFood.Models;
using Microsoft.AspNetCore.Identity;
using static HatliFood.Controllers.CartsController;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace HatliFood.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserManager<IdentityUser> _userManager;

        public OrdersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string? id)
        {
            var applicationDbContext = _context.Orders
                .Include(o => o.Buyer)
                .Include(o => o.Restaurant)
                .Where(o => o.BuyerId == id);
            return View(await applicationDbContext.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Buyer)
                .Include(o => o.DeliveryGuyUser)
                .Include(o => o.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [Authorize(Roles = "User")]
        public IActionResult PlaceOrder()
        {
            var cookies = Request.Cookies;

            List<CartProperties> orderItems = new List<CartProperties>();
            foreach (var cookie in cookies)
            {
                if (cookie.Key.Contains("HatliFood-"))
                {
                    CartProperties cookieValue = JsonSerializer.Deserialize<CartProperties>(cookie.Value);
                    orderItems.Add(cookieValue);
                }
            }

            if (orderItems.Count() == 0)
            {
                return NotFound();
            }

            var id = _userManager.GetUserId(User);
            Order newOrder = new Order()
            {
                BuyerId = id,
                OrderDate = DateTime.Now,
                OrderState = OrderStatus.Pending,
                RestaurantId = orderItems[0].RestaurantId
            };
            _context.Orders.Add(newOrder);
            _context.SaveChanges();

            int orderID = _context.Orders.Where(o => o.BuyerId == id).OrderByDescending(o => o.OrderDate).Select(o => o.Id).FirstOrDefault();
            foreach (var orderItem in orderItems)
            {
                OrderItem item = new OrderItem()
                {
                    OrderId = orderID,
                    MenuItemId = orderItem.Id,
                    Quantity = orderItem.Quantity
                };
                _context.OrderItems.Add(item);
            }
            _context.SaveChanges();
            return View("OrderCompleted");
        }
    }
}
