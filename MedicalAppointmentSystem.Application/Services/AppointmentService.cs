using MedicalAppointmentSystem.Application.Interfaces;
using MedicalAppointmentSystem.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MedicalAppointmentSystem.Application.Services
{
    public class AppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public event Action? AppointmentAdded;
        public event Action? ConsultationAdded;

        public void ScheduleAppointment(Doctor doctor, Patient patient, DateTime date)
        {
            // Business logic validation could go here (e.g. check availability)
            var appointment = new Appointment(doctor, patient, date);
            _appointmentRepository.Add(appointment);
            AppointmentAdded?.Invoke();
        }

        public void AddConsultation(Guid appointmentId, string diagnosis, string treatment)
        {
            var appointment = _appointmentRepository.GetById(appointmentId);
            if (appointment == null) return;

            var consultation = new Consultation(diagnosis, treatment);
            appointment.AddConsultation(consultation);
            _appointmentRepository.Update(appointment);
            ConsultationAdded?.Invoke();
        }

        public IEnumerable<Appointment> GetAppointmentsForDoctor(Guid doctorId)
        {
            return _appointmentRepository.GetByDoctor(doctorId);
        }

        public IEnumerable<Appointment> GetAllAppointments()
        {
            return _appointmentRepository.GetAll();
        }

        public IEnumerable<Appointment> GetAppointmentsForPatient(Guid patientId)
        {
            return _appointmentRepository.GetByPatient(patientId);
        }
    }
}
