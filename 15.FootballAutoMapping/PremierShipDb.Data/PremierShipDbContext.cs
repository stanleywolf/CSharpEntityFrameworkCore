using System;
using Microsoft.EntityFrameworkCore;
using PremierShipDb.Models;

namespace PremierShipDb.Data
{
    public class PremierShipDbContext:DbContext
    {
        public PremierShipDbContext()
        { }

        public PremierShipDbContext(DbContextOptions options):base(options)
        { }

        public DbSet<Team> Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
        }
    }
}
