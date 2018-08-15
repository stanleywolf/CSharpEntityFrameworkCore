using System;
using Microsoft.EntityFrameworkCore;
using P01.Test.Data.Models;
namespace P01.Test.Data
{
    public class TestDbContext:DbContext
    {
        public TestDbContext()
        {
            
        }

        public TestDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies(true);
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasMany(x => x.ManagerOfProject)
                .WithOne(x => x.Employee)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
