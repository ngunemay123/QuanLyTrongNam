using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyTrongNam.Models;

namespace QuanLyTrongNam.Controllers
{
    public class MushroomsController : Controller
    {
        private readonly QuanLyTrongNamContext _context;

        public MushroomsController(QuanLyTrongNamContext context)
        {
            _context = context;
        }

        // GET: Mushrooms
        public async Task<IActionResult> Index()
        {
            var quanLyTrongNamContext = _context.Mushrooms.Include(m => m.Farm);
            return View(await quanLyTrongNamContext.ToListAsync());
        }

        // GET: Mushrooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mushrooms == null)
            {
                return NotFound();
            }

            var mushroom = await _context.Mushrooms
                .Include(m => m.Farm)
                .FirstOrDefaultAsync(m => m.MushroomId == id);
            if (mushroom == null)
            {
                return NotFound();
            }

            return View(mushroom);
        }

        // GET: Mushrooms/Create
        public IActionResult Create()
        {
            ViewData["FarmId"] = new SelectList(_context.Farms, "FarmId", "FarmName");
            return View();
        }

        // POST: Mushrooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MushroomId,MushroomName,MushroomDescription,MushroomPrice,MushroomPicture,FarmId")] Mushroom mushroom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mushroom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FarmId"] = new SelectList(_context.Farms, "FarmId", "FarmId", mushroom.FarmId);
            return View(mushroom);
        }

        // GET: Mushrooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mushrooms == null)
            {
                return NotFound();
            }

            var mushroom = await _context.Mushrooms.FindAsync(id);
            if (mushroom == null)
            {
                return NotFound();
            }
            ViewData["FarmId"] = new SelectList(_context.Farms, "FarmId", "FarmName", mushroom.FarmId);
            return View(mushroom);
        }

        // POST: Mushrooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MushroomId,MushroomName,MushroomDescription,MushroomPrice,MushroomPicture,FarmId")] Mushroom mushroom)
        {
            if (id != mushroom.MushroomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mushroom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MushroomExists(mushroom.MushroomId))
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
            ViewData["FarmId"] = new SelectList(_context.Farms, "FarmId", "FarmName", mushroom.FarmId);
            return View(mushroom);
        }

        // GET: Mushrooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mushrooms == null)
            {
                return NotFound();
            }

            var mushroom = await _context.Mushrooms
                .Include(m => m.Farm)
                .FirstOrDefaultAsync(m => m.MushroomId == id);
            if (mushroom == null)
            {
                return NotFound();
            }

            return View(mushroom);
        }

        // POST: Mushrooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mushrooms == null)
            {
                return Problem("Entity set 'QuanLyTrongNamContext.Mushrooms'  is null.");
            }
            var mushroom = await _context.Mushrooms.FindAsync(id);
            if (mushroom != null)
            {
                _context.Mushrooms.Remove(mushroom);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MushroomExists(int id)
        {
          return (_context.Mushrooms?.Any(e => e.MushroomId == id)).GetValueOrDefault();
        }
    }
}
