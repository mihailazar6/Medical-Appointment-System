using MedicalAppointmentSystem.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MedicalAppointmentSystem.Application.Interfaces
{
    public interface IPatientRepository
    {
        IEnumerable<Patient> GetAll();
        Patient? GetById(Guid id);
        void Add(Patient patient);
    }
}
