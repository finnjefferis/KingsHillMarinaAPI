using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KingsHillMarinaAPI.Data;
using KingsHillMarinaAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KingsHillMarinaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BerthsController : ControllerBase
    {
        private readonly MarinaContext _context;

        public BerthsController(MarinaContext context)
        {
            _context = context;
        }

        // GET: api/berths
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Berth>>> GetBerths()
        {
            return await _context.Berths.ToListAsync();
        }

        // GET: api/berths/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Berth>> GetBerth(int id)
        {
            var berth = await _context.Berths.FindAsync(id);

            if (berth == null)
            {
                return NotFound();
            }

            return berth;
        }

        // POST: api/berths
        [HttpPost]
        public async Task<ActionResult<Berth>> PostBerth(Berth berth)
        {
            _context.Berths.Add(berth);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBerth), new { id = berth.Id }, berth);
        }

        // PUT: api/berths/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBerth(int id, Berth berth)
        {
            if (id != berth.Id)
            {
                return BadRequest();
            }

            _context.Entry(berth).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Berths.Any(e => e.Id == id))
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

        // DELETE: api/berths/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBerth(int id)
        {
            var berth = await _context.Berths.FindAsync(id);
            if (berth == null)
            {
                return NotFound();
            }

            _context.Berths.Remove(berth);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/berths/{id}/assign-boat/{boatId}
        // PUT: api/berths/{id}/assign-boat/{boatId}
        [HttpPut("{id}/assign-boat/{boatId}")]
        public async Task<IActionResult> AssignBoatToBerth(int id, int boatId)
        {
            var berth = await _context.Berths.FindAsync(id);
            if (berth == null)
            {
                return NotFound("Berth not found.");
            }

            var boat = await _context.Boats.FindAsync(boatId);
            if (boat == null)
            {
                return NotFound("Boat not found.");
            }

            // Update the BerthId in the Boat and the BoatId in the Berth
            boat.BerthId = id;
            berth.BoatId = boatId;

            // Save changes
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
