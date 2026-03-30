using Microsoft.EntityFrameworkCore;
using TakeANumberApi.Data.Mappings;
using TakeANumberApi.Models;

namespace TakeANumberApi.Data
{
    public class TakeANumberDataContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Spot> Spots { get; set; }
        public DbSet<TicketGroup> TicketGroups { get; set; }
        public DbSet<TicketNumber> TicketNumbers { get; set; }

        public TakeANumberDataContext(DbContextOptions<TakeANumberDataContext> options)
            : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyMap());
            modelBuilder.ApplyConfiguration(new SpotMap());
            modelBuilder.ApplyConfiguration(new TicketGroupMap());
            modelBuilder.ApplyConfiguration(new TicketGroupMap());
        }

    }
}
