using MedicalAppointmentSystem.Application.Interfaces;
using MedicalAppointmentSystem.Domain.Entities;
using MedicalAppointmentSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalAppointmentSystem.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly MedicalDbContext _context;

        public PatientRepository(MedicalDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Patient> GetAll()
        {
            return _context.Patients.ToList();
        }

        public Patient? GetById(Guid id)
        {
            return _context.Patients.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();
        }
    }
}
