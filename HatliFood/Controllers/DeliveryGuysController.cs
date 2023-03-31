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
using HatliFood.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace HatliFood.Controllers
{
    public class DeliveryGuysController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public DeliveryGuysController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _roleManager = roleManager;
        }

        // GET: DeliveryGuys
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return _context.DeliveryGuys != null ?
                    View(await _context.DeliveryGuys.ToListAsync()) :
                    Problem("Entity set 'ApplicationDbContext.DeliveryGuys'  is null.");
        }

        // GET: DeliveryGuys/Details/5
        [Authorize(Roles = "Admin ,Delivery")]

        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.DeliveryGuys == null)
            {
                return NotFound();
            }

            var deliveryGuy = await _context.DeliveryGuys
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (deliveryGuy == null)
            {
                return NotFound();
            }

            return View(deliveryGuy);
        }

        // GET: DeliveryGuys/Create

        //[Authorize(Roles = "Admin")]

        public IActionResult Create()
        {
            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]

        //public async Task<IActionResult> Create([Bind("UserId,Name,PhoneNumber")] DeliveryGuy deliveryGuy)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new ApplicationUser { UserName = deliveryGuy.PhoneNumber.ToString(), Email = deliveryGuy.PhoneNumber.ToString() };
        //        var result = await _userManager.CreateAsync(user, "Del@1234"); // Change the default password as needed

        //        if (result.Succeeded)
        //        {
        //            var roleExists = await _roleManager.RoleExistsAsync(UserRoles.Delivery);
        //            if (!roleExists)
        //            {
        //                var newRole = new IdentityRole(UserRoles.Delivery);
        //                await _roleManager.CreateAsync(newRole);
        //            }

        //            await _userManager.AddToRoleAsync(user, UserRoles.Delivery);

        //            deliveryGuy.UserId = user.Id;
        //            _context.Add(deliveryGuy);
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }

        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, error.Description);
        //        }
        //    }

        //    return View(deliveryGuy);
        //}

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(RegisterDelVM RegisterDelVM)
        {
            if (!ModelState.IsValid)
            {
                return View(RegisterDelVM);
            }

            var user = await _userManager.FindByEmailAsync(RegisterDelVM.PhoneNumber);
            if (user != null)
            {
                TempData["Error"] = "This Email address is already in use";
                return View(RegisterDelVM);
            }

            var newUser = new IdentityUser()
            {
                Email = RegisterDelVM.PhoneNumber,
                UserName = RegisterDelVM.PhoneNumber
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, "Del@1234");

            if (newUserResponse.Succeeded)
            {
                /////Error
                var roleExists = await _roleManager.RoleExistsAsync(UserRoles.Delivery);
                if (!roleExists)
                {
                    var newRole = new IdentityRole(UserRoles.Delivery);
                    await _roleManager.CreateAsync(newRole);
                }
                /////
                await _userManager.AddToRoleAsync(newUser, UserRoles.Delivery);

                var newDel = new DeliveryGuy()
                {
                    UserId = newUser.Id,
                    Name = RegisterDelVM.Name,
                    PhoneNumber = RegisterDelVM.PhoneNumber
                };
                _context.DeliveryGuys.Add(newDel);
                _context.SaveChanges();
            }

            return RedirectToAction("Index","DeliveryGuys");
        }



        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryGuy = await _context.DeliveryGuys.FindAsync(id);
            if (deliveryGuy == null)
            {
                return NotFound();
            }

            return View(deliveryGuy);
        }

        // POST: DeliveryGuys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(string id, [Bind("UserId,Name,PhoneNumber")] DeliveryGuy deliveryGuy)
        {
            if (id != deliveryGuy.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(deliveryGuy);
                await _context.SaveChangesAsync();

                var roleExists = await _roleManager.RoleExistsAsync(UserRoles.Delivery);
                if (!roleExists)
                {
                    var newRole = new IdentityRole(UserRoles.Delivery);
                    await _roleManager.CreateAsync(newRole);
                }

                var user = await _userManager.FindByIdAsync(deliveryGuy.UserId);
                if (user != null)
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.Delivery);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(deliveryGuy);
        }

        // GET: DeliveryGuys/Delete/5
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryGuy = await _context.DeliveryGuys
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (deliveryGuy == null)
            {
                return NotFound();
            }

            return View(deliveryGuy);
        }

        // POST: DeliveryGuys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var deliveryGuy = await _context.DeliveryGuys.FindAsync(id);
            if (deliveryGuy != null)
            {
                _context.DeliveryGuys.Remove(deliveryGuy);

                var user = await _userManager.FindByIdAsync(deliveryGuy.UserId);
                if (user != null)
                {
                    await _userManager.RemoveFromRoleAsync(user, UserRoles.Delivery);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryGuyExists(string id)
        {
            return (_context.DeliveryGuys?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
