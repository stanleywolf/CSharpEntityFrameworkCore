﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using P01.CarDealer.Data;

namespace P01.CarDealer.Data.Migrations
{
    [DbContext(typeof(CarDealerContext))]
    partial class CarDealerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("P01.CarDealer.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CustomerId");

                    b.Property<string>("Make");

                    b.Property<string>("Model");

                    b.Property<long>("TravelledDistance");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("P01.CarDealer.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate");

                    b.Property<bool>("IsYoungDriver");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("P01.CarDealer.Models.Part", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<int>("Quantity");

                    b.Property<int>("Supplier_Id");

                    b.HasKey("Id");

                    b.HasIndex("Supplier_Id");

                    b.ToTable("Parts");
                });

            modelBuilder.Entity("P01.CarDealer.Models.PartCar", b =>
                {
                    b.Property<int>("Part_Id");

                    b.Property<int>("Car_Id");

                    b.HasKey("Part_Id", "Car_Id");

                    b.HasIndex("Car_Id");

                    b.ToTable("PartCars");
                });

            modelBuilder.Entity("P01.CarDealer.Models.Sale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Car_Id");

                    b.Property<int>("Customer_Id");

                    b.Property<int>("Discount");

                    b.HasKey("Id");

                    b.HasIndex("Car_Id");

                    b.HasIndex("Customer_Id");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("P01.CarDealer.Models.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsImported");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("P01.CarDealer.Models.Car", b =>
                {
                    b.HasOne("P01.CarDealer.Models.Customer")
                        .WithMany("Cars")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("P01.CarDealer.Models.Part", b =>
                {
                    b.HasOne("P01.CarDealer.Models.Supplier", "Supplier")
                        .WithMany("Parts")
                        .HasForeignKey("Supplier_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("P01.CarDealer.Models.PartCar", b =>
                {
                    b.HasOne("P01.CarDealer.Models.Car", "Car")
                        .WithMany("PartCars")
                        .HasForeignKey("Car_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("P01.CarDealer.Models.Part", "Part")
                        .WithMany("PartCars")
                        .HasForeignKey("Part_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("P01.CarDealer.Models.Sale", b =>
                {
                    b.HasOne("P01.CarDealer.Models.Car", "Car")
                        .WithMany("Sales")
                        .HasForeignKey("Car_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("P01.CarDealer.Models.Customer", "Customer")
                        .WithMany("Sales")
                        .HasForeignKey("Customer_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}