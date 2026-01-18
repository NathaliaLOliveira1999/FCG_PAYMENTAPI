using Microsoft.EntityFrameworkCore;

namespace FCG_PAYMENTAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Sales> Sales { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sales>().HasKey(s => s.IdSales);

            base.OnModelCreating(modelBuilder);
        }
    }
}