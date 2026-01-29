using MedicalAppointmentSystem.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MedicalAppointmentSystem.Application.Interfaces
{
    public interface IDoctorRepository
    {
        IEnumerable<Doctor> GetAll();
        Doctor? GetById(Guid id);
        void Add(Doctor doctor);
    }
}
