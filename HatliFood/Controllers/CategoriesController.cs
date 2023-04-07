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
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CategoriesController(ApplicationDbContext context , UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Categorys.Include(c => c.RidNavigation);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categorys == null)
            {
                return NotFound();
            }

            string resturantId = _context.Categorys.AsNoTracking().Where(c=>c.Id == id).Select(c => c.Rid).FirstOrDefault();
            ViewBag.Resturant = _context.Restaurant.AsNoTracking().Where(res => res.Id == resturantId).FirstOrDefault();

            ViewBag.Menus = _context.MenuItems.AsNoTracking().Where(m => m.Cid == id).ToList();
            ViewBag.CountOfItems = _context.MenuItems.AsNoTracking().Where(m => m.Cid == id).ToList().Count();


            var category = await _context.Categorys
                .Include(c => c.RidNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public async Task<IActionResult> Create()
        {
            ViewData["Rid"] = new SelectList(_context.Restaurant, "Id", "Name");
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;
            ViewData["Cid"] = new SelectList(_context.Restaurant.Where(s => s.Id == userId).ToList(), "Id", "Name");

            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Rid")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(actionName: "RestaurantDetails", controllerName: "Restaurants" , new {id=category.Rid});
            }
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;
            ViewData["Cid"] = new SelectList(_context.Restaurant.Where(s => s.Id == userId).ToList(), "Id", "Name");
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categorys == null)
            {
                return NotFound();
            }

            var category = await _context.Categorys.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;
            ViewData["Cid"] = new SelectList(_context.Restaurant.Where(s => s.Id == userId).ToList(), "Id", "Name");

            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Rid")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(actionName: "RestaurantDetails", controllerName: "Restaurants", new { id = category.Rid });
            }
            ViewData["Rid"] = new SelectList(_context.Restaurant, "Id", "Name", category.Rid);
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categorys == null)
            {
                return NotFound();
            }

            var category = await _context.Categorys
                .Include(c => c.RidNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categorys == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Categorys'  is null.");
            }
            var category = await _context.Categorys.FindAsync(id);
            if (category != null)
            {
                _context.Categorys.Remove(category);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
          return (_context.Categorys?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
