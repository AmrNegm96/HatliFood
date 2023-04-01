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

namespace HatliFood.Controllers
{
    public class AddressesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserManager<IdentityUser> _UserManager;

        public AddressesController(ApplicationDbContext context
            , UserManager<IdentityUser> userManager)
        {
            _context = context;
            _UserManager = userManager;
        }

        // GET: Addresses
        public async Task<IActionResult> Index(string? id)
        {
            var applicationDbContext = _context.Addresss.Where(a => a.BuyerID == id);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.Addresss == null)
            {
                return NotFound();
            }

            var address = await _context.Addresss
                .Include(a => a.Buyer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        public IActionResult Create()
        {
            ViewData["BuyerID"] = new SelectList(_context.Buyers, "UserId", "UserId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("City,Street,BuildingNumber,Floor,ApartmentNumber,BuyerID")] Address address)
        {
            if (ModelState.IsValid)
            {
                _context.Add(address);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = address.BuyerID });
            }
            ViewData["BuyerID"] = new SelectList(_context.Buyers, "UserId", "UserId", address.BuyerID);
            return View(address);
        }

        // GET: Addresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Addresss == null)
            {
                return NotFound();
            }

            var address = await _context.Addresss.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            ViewData["BuyerID"] = new SelectList(_context.Buyers, "UserId", "UserId", address.BuyerID);
            return View(address);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,City,Street,BuildingNumber,Floor,ApartmentNumber,BuyerID")] Address address)
        {
            if (id != address.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = address.BuyerID });
            }
            ViewData["BuyerID"] = new SelectList(_context.Buyers, "UserId", "UserId", address.BuyerID);
            return View(address);
        }

        // GET: Addresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Addresss == null)
            {
                return NotFound();
            }

            var address = await _context.Addresss
                .Include(a => a.Buyer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.Addresss == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Addresss'  is null.");
            }
            var address = await _context.Addresss.FindAsync(id);
            if (address != null)
            {
                _context.Addresss.Remove(address);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = _UserManager.GetUserId(User) });
        }

        private bool AddressExists(int id)
        {
            return (_context.Addresss?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
