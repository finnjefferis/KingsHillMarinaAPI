using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KingsHillMarinaAPI.Data;
using KingsHillMarinaAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class BoatsController : ControllerBase
{
    private readonly MarinaContext _context;

    public BoatsController(MarinaContext context)
    {
        _context = context;
    }

    // GET: api/boats
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetBoats()
    {
        // Select only required properties to avoid nested relationships
        return await _context.Boats
            .Select(b => new {
                b.Id,
                b.Name,
                b.Length,
                b.Make,
                b.Type,
                b.OwnerId,
                b.BerthId
            })
            .ToListAsync();
    }

    // GET: api/boats/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetBoat(int id)
    {
        var boat = await _context.Boats
            .Where(b => b.Id == id)
            .Select(b => new {
                b.Id,
                b.Name,
                b.Length,
                b.Make,
                b.Type,
                b.OwnerId,
                b.BerthId
            })
            .FirstOrDefaultAsync();

        if (boat == null)
        {
            return NotFound();
        }

        return boat;
    }

    // POST: api/boats
    [HttpPost]
    public async Task<ActionResult<Boat>> PostBoat(Boat boat)
    {
        _context.Boats.Add(boat);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBoat), new { id = boat.Id }, boat);
    }

    // PUT: api/boats/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBoat(int id, Boat boat)
    {
        if (id != boat.Id)
        {
            return BadRequest();
        }

        _context.Entry(boat).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Boats.Any(e => e.Id == id))
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

    // DELETE: api/boats/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBoat(int id)
    {
        var boat = await _context.Boats.FindAsync(id);
        if (boat == null)
        {
            return NotFound();
        }

        _context.Boats.Remove(boat);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
