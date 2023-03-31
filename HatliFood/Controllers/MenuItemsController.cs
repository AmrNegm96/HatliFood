using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HatliFood.Data;
using HatliFood.Models;

namespace HatliFood.Controllers
{
    public class MenuItemsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hosting;

        public MenuItemsController(ApplicationDbContext context, IWebHostEnvironment hosting)
        {
            _context = context;
            _hosting = hosting;

        }

        // GET: MenuItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MenuItems.Include(m => m.CidNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MenuItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MenuItems == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItems
                .Include(m => m.CidNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuItem == null)
            {
                return NotFound();
            }

            return View(menuItem);
        }

        // GET: MenuItems/Create
        public IActionResult Create()
        {
            ViewData["Cid"] = new SelectList(_context.Categorys, "Id", "Name");
            return View();
        }

        // POST: MenuItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Price,ImgPath,ImgFile,Name,Description,Cid")] MenuItem menuItem)
        {

            menuItem.ImgPath = "dd";
            if (ModelState.IsValid)
            {

                string wwwRootPath = _hosting.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(menuItem.ImgFile.FileName);
                string extension = Path.GetExtension(menuItem.ImgFile.FileName);

                menuItem.ImgPath = fileName + extension;

                string path = Path.Combine(wwwRootPath + "/Image/Menus" + fileName + extension);

                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    await menuItem.ImgFile.CopyToAsync(filestream);
                }

                _context.Add(menuItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

                ViewData["Cid"] = new SelectList(_context.Categorys, "Id", "Name", menuItem.Cid);
            }
            return View(menuItem);
        }

        // GET: MenuItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MenuItems == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            ViewData["Cid"] = new SelectList(_context.Categorys, "Id", "Name", menuItem.Cid);
            return View(menuItem);
        }

        // POST: MenuItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Price,ImgFile,ImgPath,Name,Description,Cid")] MenuItem menuItem)
        {
            var Menu = _context.MenuItems;

            if (id != menuItem.Id)
            {
                return NotFound();
            }
            menuItem.ImgPath = "dd";



            if (ModelState.IsValid)
            {
                try
                {
                    // get old Image Path to delete 
                    string wwwRootPath = _hosting.WebRootPath;

                    //var oldData = await Restu.FindAsync(id);

                    var oldData = _context.MenuItems.AsNoTracking().Where(s => s.Id == id).FirstOrDefault();
                    string oldPath = oldData?.ImgPath;

                    if (System.IO.File.Exists(wwwRootPath + "/Image/Menus" + oldPath))
                    {
                        System.IO.File.Delete(wwwRootPath + "/Image/Menus" + oldPath);
                    }

                    string fileName = Path.GetFileNameWithoutExtension(menuItem.ImgFile.FileName);
                    string extension = Path.GetExtension(menuItem.ImgFile.FileName);

                    menuItem.ImgPath = fileName + extension;

                    string path = Path.Combine(wwwRootPath + "/Image/Menus" + fileName + extension);

                    using (var filestream = new FileStream(path, FileMode.Create))
                    {

                        await menuItem.ImgFile.CopyToAsync(filestream);
                    }
                    ViewData["Cid"] = new SelectList(_context.Categorys, "Id", "Name", menuItem.Cid);

                    Menu.Update(menuItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuItemExists(menuItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));

                
                }
            return View(menuItem);
        }

        // GET: MenuItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MenuItems == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItems
                .Include(m => m.CidNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuItem == null)
            {
                return NotFound();
            }

            return View(menuItem);
        }

        // POST: MenuItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string wwwRootPath = _hosting.WebRootPath;

            if (_context.MenuItems == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MenuItems'  is null.");
            }
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem != null)
            {
                _context.MenuItems.Remove(menuItem);
            }
            var oldData = _context.Restaurant.AsNoTracking().Where(s => s.Id == id).FirstOrDefault();
            string oldPath = oldData?.ImgPath;

            if (System.IO.File.Exists(wwwRootPath + "/Image/Menus" + oldPath))
            {
                System.IO.File.Delete(wwwRootPath + "/Image/Menus" + oldPath);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuItemExists(int id)
        {
          return (_context.MenuItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
