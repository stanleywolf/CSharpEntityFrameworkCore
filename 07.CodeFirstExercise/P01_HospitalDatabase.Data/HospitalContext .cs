﻿using System;
using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.EntityConfiguration;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data
{
    public class HospitalContext :DbContext
    {
        public HospitalContext()
        {   }

        public HospitalContext(DbContextOptions options):base(options)
        {  }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Diagnose> Diagnoses { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Visitation> Visitations { get; set; }
        public DbSet<PatientMedicament> PatientsMedicaments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DiagnoseConfig()); modelBuilder.ApplyConfiguration(new MedicamentCongig());
            modelBuilder.ApplyConfiguration(new PatientConfig());
            modelBuilder.ApplyConfiguration(new PatientsMedicamentsConfig());
            modelBuilder.ApplyConfiguration(new VisitationConfig());
            modelBuilder.ApplyConfiguration(new DoctorConfig());
        }
    }
}
