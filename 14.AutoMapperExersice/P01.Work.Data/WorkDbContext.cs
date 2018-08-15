using System;
using Microsoft.EntityFrameworkCore;
using P01.Work.Models;

namespace P01.Work.Data
{
    public class WorkDbContext:DbContext
    {
        public WorkDbContext()
        {
            
        }

        public WorkDbContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasOne(x => x.Manager)
                    .WithMany(x => x.ManagerEmployee)
                    .HasForeignKey(x => x.ManagerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
