using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MedicalAppointmentSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalAppointmentSystem.Infrastructure.Data
{
    public class MedicalDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<Consultation> Consultations => Set<Consultation>();

        public MedicalDbContext(DbContextOptions<MedicalDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MedicalDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

