using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using _01.CodeFirst.Models;

namespace _01.CodeFirst
{
    public class AwesomHotelDbContext:DbContext
    {
        public DbSet<Room> Rooms { get; set; }

        public DbSet<KeyCard> KeyCards { get; set; }

        public DbSet<RoomsKeyCards> RoomsKeyCardses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoomsKeyCards>().HasKey(x => new {x.RoomId, x.KeyCardId});

            modelBuilder.Entity<RoomsKeyCards>()
                .HasOne(x => x.Room)
                .WithMany(x => x.RoomsKeyCards);

            modelBuilder.Entity<RoomsKeyCards>()
                .HasOne(x => x.KeyCard)
                .WithMany(x => x.RoomsKeyCards);

            base.OnModelCreating(modelBuilder);
        }
    }
}
