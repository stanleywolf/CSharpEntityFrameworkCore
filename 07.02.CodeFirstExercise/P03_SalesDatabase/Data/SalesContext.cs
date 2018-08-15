using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data
{
    public class SalesContext:DbContext
    {
        public SalesContext()
        { }

        public SalesContext(DbContextOptions options):base(options)
        {}

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Store> Stores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(x => x.ProductId);
                entity.Property(x => x.Name)
                    .HasMaxLength(50)
                    .IsUnicode();
                entity.HasMany(x => x.Sales)
                    .WithOne(x => x.Product)
                    .HasForeignKey(x => x.ProductId);
                entity.Property(x => x.Description)
                    .HasMaxLength(250)
                    .HasDefaultValue("No description");
            });
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(x => x.CustomerId);
                entity.Property(x => x.Name)
                    .HasMaxLength(100)
                    .IsUnicode();
                entity.Property(x => x.Email)
                    .HasMaxLength(80)
                    .IsUnicode(false);
                entity.HasMany(x => x.Sales)
                    .WithOne(x => x.Customer)
                    .HasForeignKey(x => x.CustomerId);
            });
            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(x => x.StoreId);
                entity.Property(x => x.Name)
                    .HasMaxLength(80)
                    .IsUnicode();
                entity.HasMany(x => x.Sales)
                    .WithOne(x => x.Store)
                    .HasForeignKey(x => x.StoreId);
            });
            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(c => c.SaleId);
                entity.Property(x => x.Date)
                    .HasDefaultValueSql("GETDATE()");
            });
        }
    }
}
