using System;
using Microsoft.EntityFrameworkCore;
using P01.CarDealer.Models;

namespace P01.CarDealer.Data
{
    public class CarDealerContext:DbContext
    {
        public CarDealerContext()
        { }

        public CarDealerContext(DbContextOptions options):base(options)
        {}

        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<PartCar> PartCars { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
                optionsBuilder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PartCar>(entity =>
            {
                entity.HasKey(x => new {x.Part_Id, x.Car_Id});

                entity.HasOne(x => x.Part)
                    .WithMany(x => x.PartCars)
                    .HasForeignKey(x => x.Part_Id);

                entity.HasOne(x => x.Car)
                    .WithMany(x => x.PartCars)
                    .HasForeignKey(x => x.Car_Id);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasMany(x => x.Parts)
                    .WithOne(x => x.Supplier)
                    .HasForeignKey(x => x.Supplier_Id);
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasOne(x => x.Car)
                    .WithMany(x => x.Sales)
                    .HasForeignKey(x => x.Car_Id);

                entity.HasOne(x => x.Customer)
                    .WithMany(x => x.Sales)
                    .HasForeignKey(x => x.Customer_Id);
            });
        }
    }
}
