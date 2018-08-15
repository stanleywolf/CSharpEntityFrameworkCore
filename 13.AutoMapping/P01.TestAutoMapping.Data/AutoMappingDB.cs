using System;
using Microsoft.EntityFrameworkCore;
using P01.TestAutoMapping.Data.Models;

namespace P01.TestAutoMapping.Data
{
    public class AutoMappingDB:DbContext
    {
        public AutoMappingDB()
        {}

        public AutoMappingDB(DbContextOptions options)
        { }

        public DbSet<Product> Products { get; set; }

        public DbSet<Storage> Storages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=STANCHO-PC\SQLEXPRESS01;Database=AutoMappingDB;Integrated Security=True");
                optionsBuilder.UseLazyLoadingProxies(true);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductStock>()
                .HasKey(x => new {x.ProductId, x.StorageId});

            modelBuilder.Entity<Product>()
                .HasMany(x => x.ProductStocks)
                .WithOne(x => x.Product);

            modelBuilder.Entity<Storage>()
                .HasMany(x => x.ProductStocks)
                .WithOne(x => x.Storage);
        }
    }
}
