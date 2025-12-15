using Microsoft.EntityFrameworkCore;
using MiniMarket.API.Models;

namespace MiniMarket.API.Data
{
    public class MiniMarketContext : DbContext
    {
        public MiniMarketContext(DbContextOptions<MiniMarketContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tutaj w przysz³oœci dodamy bardziej zaawansowane konfiguracje relacji
            base.OnModelCreating(modelBuilder);
        }
    }
}