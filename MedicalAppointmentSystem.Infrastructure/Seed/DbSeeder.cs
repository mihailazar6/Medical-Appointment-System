using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MedicalAppointmentSystem.Application.Security;
using MedicalAppointmentSystem.Domain.Entities;
using MedicalAppointmentSystem.Infrastructure.Data;

namespace MedicalAppointmentSystem.Infrastructure.Seed
{
    public static class DbSeeder
    {
        public static void Seed(MedicalDbContext context)
        {
            if (!context.Users.Any())
            {
                var admin = new AdminUser(
                    "admin",
                    PasswordHasher.Hash("admin123")
                );
                context.Users.Add(admin);
            }

            if (!context.Doctors.Any())
            {
                var doctor = new Doctor("Dr. House", "Diagnostician");
                context.Doctors.Add(doctor);
                
                var doctorUser = new DoctorUser("house", PasswordHasher.Hash("house123"), doctor);
                context.Users.Add(doctorUser);

                var patient = new Patient("John Doe", new DateOnly(1980, 1, 1));
                context.Patients.Add(patient);

                // Create patient user account
                var patientUser = new PatientUser("johndoe", PasswordHasher.Hash("patient123"), patient);
                context.Users.Add(patientUser);

                var appointment = new Appointment(doctor, patient, DateTime.UtcNow.AddDays(1));
                context.Appointments.Add(appointment);
            }

            context.SaveChanges();
        }
    }
}

