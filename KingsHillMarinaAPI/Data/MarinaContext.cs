using Microsoft.EntityFrameworkCore;
using KingsHillMarinaAPI.Models;

namespace KingsHillMarinaAPI.Data
{
    public class MarinaContext : DbContext
    {
        public MarinaContext(DbContextOptions<MarinaContext> options) : base(options) { }

        public DbSet<Boat> Boats { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Berth> Berths { get; set; }
        public DbSet<BillingRecord> BillingRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
        }
    }
}
