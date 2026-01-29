using MedicalAppointmentSystem.Application.Interfaces;
using MedicalAppointmentSystem.Domain.Entities;
using MedicalAppointmentSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalAppointmentSystem.Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly MedicalDbContext _context;

        public DoctorRepository(MedicalDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Doctor> GetAll()
        {
            return _context.Doctors.ToList();
        }

        public Doctor? GetById(Guid id)
        {
            return _context.Doctors.FirstOrDefault(d => d.Id == id);
        }

        public void Add(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
        }
    }
}
