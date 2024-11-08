using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KingsHillMarinaAPI.Data;
using KingsHillMarinaAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingsHillMarinaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnersController : ControllerBase
    {
        private readonly MarinaContext _context;

        public OwnersController(MarinaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetOwners()
        {
            var owners = await _context.Owners.ToListAsync();

            var result = owners.Select(owner => new {
                owner.Id,
                owner.Name,
                owner.ContactInfo,
                BoatIds = _context.Boats
                    .Where(b => b.OwnerId == owner.Id)
                    .Select(b => b.Id)
                    .ToList()
            });

            return Ok(result);
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetOwner(int id)
        {
            var owner = await _context.Owners.FindAsync(id);

            if (owner == null)
            {
                return NotFound();
            }

            var ownerWithBoats = new
            {
                owner.Id,
                owner.Name,
                owner.ContactInfo,
                BoatIds = await _context.Boats
                    .Where(b => b.OwnerId == owner.Id)
                    .Select(b => b.Id)
                    .ToListAsync()
            };

            return Ok(ownerWithBoats);
        }

        [HttpPost]
        public async Task<ActionResult<Owner>> PostOwner(Owner owner)
        {
            _context.Owners.Add(owner);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOwner), new { id = owner.Id }, owner);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOwner(int id, Owner owner)
        {
            if (id != owner.Id)
            {
                return BadRequest();
            }

            _context.Entry(owner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Owners.Any(e => e.Id == id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOwner(int id)
        {
            var owner = await _context.Owners.FindAsync(id);
            if (owner == null)
            {
                return NotFound();
            }

            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
