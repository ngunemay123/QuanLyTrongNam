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
    public class SensorsApiController : ControllerBase
    {
        private readonly QuanLyTrongNamContext _context;

        public SensorsApiController(QuanLyTrongNamContext context)
        {
            _context = context;
        }

        // GET: api/SensorsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sensor>>> GetSensors()
        {
          if (_context.Sensors == null)
          {
              return NotFound();
          }
            return await _context.Sensors.ToListAsync();
        }

        // GET: api/SensorsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sensor>> GetSensor(int id)
        {
          if (_context.Sensors == null)
          {
              return NotFound();
          }
            var sensor = await _context.Sensors.FindAsync(id);

            if (sensor == null)
            {
                return NotFound();
            }

            return sensor;
        }

        // PUT: api/SensorsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSensor(int id, Sensor sensor)
        {
            if (id != sensor.SensorId)
            {
                return BadRequest();
            }

            _context.Entry(sensor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SensorExists(id))
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

        // POST: api/SensorsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sensor>> PostSensor(Sensor sensor)
        {
          if (_context.Sensors == null)
          {
              return Problem("Entity set 'QuanLyTrongNamContext.Sensors'  is null.");
          }
            _context.Sensors.Add(sensor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSensor", new { id = sensor.SensorId }, sensor);
        }

        // DELETE: api/SensorsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensor(int id)
        {
            if (_context.Sensors == null)
            {
                return NotFound();
            }
            var sensor = await _context.Sensors.FindAsync(id);
            if (sensor == null)
            {
                return NotFound();
            }

            _context.Sensors.Remove(sensor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SensorExists(int id)
        {
            return (_context.Sensors?.Any(e => e.SensorId == id)).GetValueOrDefault();
        }
    }
}
