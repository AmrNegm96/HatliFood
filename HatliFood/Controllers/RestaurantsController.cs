﻿using System;
using System.IO;
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
    public class RestaurantsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hosting;
        public RestaurantsController(ApplicationDbContext context, IWebHostEnvironment hosting)
        {
            _context = context;
            _hosting = hosting;

        }

        // GET: Restaurants
        public async Task<IActionResult> Index()
        {
            return _context.Restaurant != null ?
                        View(await _context.Restaurant.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Restaurant'  is null.");
        }

        // GET: Restaurants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Restaurant == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurant
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // GET: Restaurants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImgFile,ImgPath")] Restaurant restaurant)
        {
            restaurant.ImgPath = "dd";
            if (ModelState.IsValid)
            {

                string wwwRootPath = _hosting.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(restaurant.ImgFile.FileName);
                string extension = Path.GetExtension(restaurant.ImgFile.FileName);
                
                restaurant.ImgPath = fileName  + extension;
                
                string path = Path.Combine(wwwRootPath + "/Image/" + fileName + extension);

                using(var filestream = new FileStream(path, FileMode.Create))
                {
                    await restaurant.ImgFile.CopyToAsync(filestream);
                }

                _context.Add(restaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Restaurant == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurant.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImgFile,ImgPath")] Restaurant _restaurant)
        {
            var Restu = _context.Restaurant;

            if (id != _restaurant.Id)
            {
                return NotFound();
            }
            _restaurant.ImgPath = "dd";

            // get old Image Path to delete 
            string wwwRootPath = _hosting.WebRootPath;

            //var oldData = await Restu.FindAsync(id);

            var oldData = _context.Restaurant.AsNoTracking().Where(s => s.Id == id).FirstOrDefault();
            string oldPath = oldData?.ImgPath;

            if(System.IO.File.Exists(wwwRootPath + "/Image/" + oldPath))
            {
                System.IO.File.Delete(wwwRootPath + "/Image/" + oldPath);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = Path.GetFileNameWithoutExtension(_restaurant.ImgFile.FileName);
                    string extension = Path.GetExtension(_restaurant.ImgFile.FileName);

                    _restaurant.ImgPath = fileName + extension;

                    string path = Path.Combine(wwwRootPath + "/Image/" + fileName + extension);

                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await _restaurant.ImgFile.CopyToAsync(filestream);
                    }

                    Restu.Update(_restaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(_restaurant.Id))
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
            return View(_restaurant);
        }

        // GET: Restaurants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Restaurant == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurant
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Restaurant == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Restaurant'  is null.");
            }
            var restaurant = await _context.Restaurant.FindAsync(id);
            if (restaurant != null)
            {
                _context.Restaurant.Remove(restaurant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantExists(int id)
        {
            return (_context.Restaurant?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        string UploadFile(string filename)
        {
            if (filename != null)
            {
                string uploads = Path.Combine(_hosting.WebRootPath, "uploads");
                string fullPath = Path.Combine(uploads, filename);
                //filename.CopyTo(new FileStream(fullPath, FileMode.Create));

                return fullPath;
            }

            return null;
        }
    }
}