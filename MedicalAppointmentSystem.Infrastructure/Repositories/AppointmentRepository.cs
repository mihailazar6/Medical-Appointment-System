using MedicalAppointmentSystem.Application.Interfaces;
using MedicalAppointmentSystem.Domain.Entities;
using MedicalAppointmentSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalAppointmentSystem.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly MedicalDbContext _context;

        public AppointmentRepository(MedicalDbContext context)
        {
            _context = context;
        }

        public void Add(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
        }

        public void Update(Appointment appointment)
        {
            // If there's a new consultation, add it to the context first
            if (appointment.Consultation != null)
            {
                var existingConsultation = _context.Consultations.Find(appointment.Consultation.Id);
                if (existingConsultation == null)
                {
                    _context.Consultations.Add(appointment.Consultation);
                }
            }
            _context.SaveChanges();
        }

        public Appointment? GetById(Guid id)
        {
            return _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Include(a => a.Consultation)
                .FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Appointment> GetAll()
        {
            return _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Include(a => a.Consultation)
                .ToList();
        }

        public IEnumerable<Appointment> GetByDoctor(Guid doctorId)
        {
            return _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Include(a => a.Consultation)
                .Where(a => a.Doctor.Id == doctorId)
                .ToList();
        }

        public IEnumerable<Appointment> GetByPatient(Guid patientId)
        {
            return _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Include(a => a.Consultation)
                .Where(a => a.Patient.Id == patientId)
                .ToList();
        }
    }
}
