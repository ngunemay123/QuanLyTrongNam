﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyTrongNam.Models;

namespace QuanLyTrongNam.Controllers
{
    public class FarmsController : Controller
    {
        private readonly QuanLyTrongNamContext _context;

        public FarmsController(QuanLyTrongNamContext context)
        {
            _context = context;
        }

        // GET: Farms
        public async Task<IActionResult> Index()
        {
              return _context.Farms != null ? 
                          View(await _context.Farms.ToListAsync()) :
                          Problem("Entity set 'QuanLyTrongNamContext.Farms'  is null.");
        }

        // GET: Farms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Farms == null)
            {
                return NotFound();
            }
           
            var farm = await _context.Farms
                .Include(f => f.Farmers)
                .Include(f => f.Sensors)
                .Include(f => f.Mushrooms)
                .FirstOrDefaultAsync(m => m.FarmId == id);
            if (farm == null)
            {
                return NotFound();
            }

            return View(farm);
        }

        // GET: Farms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Farms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FarmId,FarmName,FarmLocation,FarmPicture")] Farm farm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(farm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(farm);
        }

        // GET: Farms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Farms == null)
            {
                return NotFound();
            }

            var farm = await _context.Farms.FindAsync(id);
            if (farm == null)
            {
                return NotFound();
            }
            return View(farm);
        }

        // POST: Farms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FarmId,FarmName,FarmLocation,FarmPicture")] Farm farm)
        {
            if (id != farm.FarmId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(farm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FarmExists(farm.FarmId))
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
            return View(farm);
        }

        // GET: Farms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Farms == null)
            {
                return NotFound();
            }

            var farm = await _context.Farms
                .FirstOrDefaultAsync(m => m.FarmId == id);
            if (farm == null)
            {
                return NotFound();
            }

            return View(farm);
        }

        // POST: Farms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Farms == null)
            {
                return Problem("Entity set 'QuanLyTrongNamContext.Farms'  is null.");
            }
            var farm = await _context.Farms.FindAsync(id);
            if (farm != null)
            {
                _context.Farms.Remove(farm);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FarmExists(int id)
        {
          return (_context.Farms?.Any(e => e.FarmId == id)).GetValueOrDefault();
        }
    }
}
