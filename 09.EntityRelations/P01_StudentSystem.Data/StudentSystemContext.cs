using System;
using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data
{
    public class StudentSystemContext:DbContext
    {
        public StudentSystemContext()
        { }

        public StudentSystemContext(DbContextOptions options):base(options)
        { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Homework> HomeworkSubmissions { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resource>(entity =>
            {
                entity.Property(x => x.Name)
                    .HasMaxLength(50)
                    .IsRequired()
                    .IsUnicode();

                entity.Property(x => x.Url)
                    .IsRequired()
                    .IsUnicode(false);
                
            });

            modelBuilder.Entity<Homework>(entity =>
            {
                entity.Property(x => x.Content)
                    .IsUnicode(false);                
                });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(x => new {x.CourseId, x.StudentId});

                entity.HasOne(x => x.Course)
                    .WithMany(x => x.StudentsEnrolled)
                    .HasForeignKey(x => x.CourseId);

                entity.HasOne(x => x.Student)
                    .WithMany(x => x.CourseEnrollments)
                    .HasForeignKey(x => x.StudentId);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(x => x.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(x => x.PhoneNumber)
                    .HasMaxLength(10)
                    .IsRequired(false)
                    .IsUnicode(false);

                entity.HasMany(x => x.HomeworkSubmissions)
                    .WithOne(x => x.Student)
                    .HasForeignKey(x => x.StudentId);

                entity.HasMany(x => x.CourseEnrollments)
                    .WithOne(x => x.Student)
                    .HasForeignKey(x => x.StudentId);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(x => x.Name)
                    .HasMaxLength(80)
                    .IsRequired()
                    .IsUnicode();

                entity.Property(x => x.Description)
                    .IsRequired(false);

                entity.HasMany(x => x.Resources)
                    .WithOne(x => x.Course)
                    .HasForeignKey(x => x.CourseId);

                entity.HasMany(x => x.HomeworkSubmissions)
                    .WithOne(x => x.Course)
                    .HasForeignKey(x => x.CourseId);

                entity.HasMany(x => x.StudentsEnrolled)
                    .WithOne(x => x.Course)
                    .HasForeignKey(x => x.CourseId);

            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
