using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HatliFood.Data;
using HatliFood.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace HatliFood.Controllers
{
    //[Authorize(Roles ="Delivery")]
    public class OrdersDelController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public OrdersDelController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: OrdersDel
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Orders.Include(o => o.Buyer).Include(o => o.DeliveryGuyUser).Include(o => o.Restaurant).Where(or=>or.OrderState==OrderStatus.Pending
            
            );
            return View(await applicationDbContext.ToListAsync());
        } 
        public async Task<IActionResult> MyOrders()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;
            var applicationDbContext = _context.Orders.Include(o => o.Buyer).Include(o => o.DeliveryGuyUser).Include(o => o.Restaurant).Where(or=>or.DeliveryGuyUserId== userId

            );
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Stage1()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;
            var applicationDbContext = _context.Orders.Include(o => o.Buyer).Include(o => o.DeliveryGuyUser).Include(o => o.Restaurant).Where(or=>or.DeliveryGuyUserId== userId

            ).Where(or => or.OrderState == OrderStatus.Prepering);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Stage2()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;
            var applicationDbContext = _context.Orders.Include(o => o.Buyer).Include(o => o.DeliveryGuyUser).Include(o => o.Restaurant).Where(or=>or.DeliveryGuyUserId== userId

            ).Where(or => or.OrderState == OrderStatus.Delivering);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: OrdersDel/Details/5
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


        [HttpGet]

        public async Task<IActionResult> Edit1(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;
            Order orderDelAcc = await _context.Orders.FirstOrDefaultAsync(i => i.Id == id);

            if (orderDelAcc == null)
            {
                return NotFound();
            }

            try
            {
                orderDelAcc.DeliveryGuyUserId = userId;
                orderDelAcc.OrderState = OrderStatus.Prepering;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle the exception here, e.g. log it or return an error message to the user
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return RedirectToAction("index","OrdersDel");
        }
        
        [HttpGet]

        public async Task<IActionResult> Edit2(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;
            Order orderDelAcc = await _context.Orders.FirstOrDefaultAsync(i => i.Id == id);

            if (orderDelAcc == null)
            {
                return NotFound();
            }

            try
            {
                orderDelAcc.DeliveryGuyUserId = userId;
                orderDelAcc.OrderState = OrderStatus.Delivering;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle the exception here, e.g. log it or return an error message to the user
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return RedirectToAction("index","OrdersDel");
        }
                
        [HttpGet]

        public async Task<IActionResult> Edit3(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;
            Order orderDelAcc = await _context.Orders.FirstOrDefaultAsync(i => i.Id == id);

            if (orderDelAcc == null)
            {
                return NotFound();
            }

            try
            {
                orderDelAcc.DeliveryGuyUserId = userId;
                orderDelAcc.OrderState = OrderStatus.Delivered;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle the exception here, e.g. log it or return an error message to the user
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return RedirectToAction("index","OrdersDel");
        }



        private bool OrderExists(int id)
        {
          return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
