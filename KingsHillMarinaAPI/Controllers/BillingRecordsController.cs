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
    public class BillingRecordsController : ControllerBase
    {
        private readonly MarinaContext _context;
        private const decimal RatePerMeter = 51.85m;
        private const decimal VAT = 1.2m; // 20% VAT

        public BillingRecordsController(MarinaContext context)
        {
            _context = context;
        }

        // GET: api/billingrecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BillingRecord>>> GetBillingRecords()
        {
            return await _context.BillingRecords
                .Include(b => b.Boat)
                .ToListAsync();
        }

        // GET: api/billingrecords/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BillingRecord>> GetBillingRecord(int id)
        {
            var billingRecord = await _context.BillingRecords
                .Include(b => b.Boat)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (billingRecord == null)
            {
                return NotFound();
            }

            return billingRecord;
        }

        // POST: api/billingrecords/create-for-boat/{boatId}
        [HttpPost("create-for-boat/{boatId}")]
        public async Task<ActionResult<BillingRecord>> CreateBillingRecord(int boatId)
        {
            var boat = await _context.Boats.FindAsync(boatId);
            if (boat == null)
            {
                return NotFound("Boat not found.");
            }

            // Calculate billing amount based on boat length, rate, and VAT
            var billingAmount = CalculateBillingAmount(boat.Length);

            var billingRecord = new BillingRecord
            {
                BoatId = boatId,
                BillingDate = DateTime.Now,
                Amount = billingAmount
            };

            _context.BillingRecords.Add(billingRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBillingRecord), new { id = billingRecord.Id }, billingRecord);
        }

        // DELETE: api/billingrecords/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBillingRecord(int id)
        {
            var billingRecord = await _context.BillingRecords.FindAsync(id);
            if (billingRecord == null)
            {
                return NotFound();
            }

            _context.BillingRecords.Remove(billingRecord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Helper method to calculate billing amount
        private decimal CalculateBillingAmount(double length)
        {
            return (decimal)length * RatePerMeter * 12 * VAT; // Annual billing with VAT
        }
    }
}
