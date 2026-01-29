using MedicalAppointmentSystem.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MedicalAppointmentSystem.Application.Interfaces
{
    public interface IAppointmentRepository
    {
        void Add(Appointment appointment);
        void Update(Appointment appointment);
        Appointment? GetById(Guid id);
        IEnumerable<Appointment> GetAll();
        IEnumerable<Appointment> GetByDoctor(Guid doctorId);
        IEnumerable<Appointment> GetByPatient(Guid patientId);
    }
}
