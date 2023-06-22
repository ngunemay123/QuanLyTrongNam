using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyTrongNam.Models;

namespace QuanLyTrongNam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MushroomsApiController : ControllerBase
    {
        private readonly QuanLyTrongNamContext _context;

        public MushroomsApiController(QuanLyTrongNamContext context)
        {
            _context = context;
        }

        // GET: api/MushroomsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mushroom>>> GetMushrooms()
        {
          if (_context.Mushrooms == null)
          {
              return NotFound();
          }
            return await _context.Mushrooms.ToListAsync();
        }

        // GET: api/MushroomsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mushroom>> GetMushroom(int id)
        {
          if (_context.Mushrooms == null)
          {
              return NotFound();
          }
            var mushroom = await _context.Mushrooms.FindAsync(id);

            if (mushroom == null)
            {
                return NotFound();
            }

            return mushroom;
        }

        // PUT: api/MushroomsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMushroom(int id, Mushroom mushroom)
        {
            if (id != mushroom.MushroomId)
            {
                return BadRequest();
            }

            _context.Entry(mushroom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MushroomExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MushroomsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Mushroom>> PostMushroom(Mushroom mushroom)
        {
          if (_context.Mushrooms == null)
          {
              return Problem("Entity set 'QuanLyTrongNamContext.Mushrooms'  is null.");
          }
            _context.Mushrooms.Add(mushroom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMushroom", new { id = mushroom.MushroomId }, mushroom);
        }

        // DELETE: api/MushroomsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMushroom(int id)
        {
            if (_context.Mushrooms == null)
            {
                return NotFound();
            }
            var mushroom = await _context.Mushrooms.FindAsync(id);
            if (mushroom == null)
            {
                return NotFound();
            }

            _context.Mushrooms.Remove(mushroom);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MushroomExists(int id)
        {
            return (_context.Mushrooms?.Any(e => e.MushroomId == id)).GetValueOrDefault();
        }
    }
}
