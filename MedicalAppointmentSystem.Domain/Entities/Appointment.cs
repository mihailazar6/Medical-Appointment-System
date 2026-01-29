using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Domain.Entities
{
    public class Appointment
    {
        public Guid Id { get; private set; }
        public Doctor Doctor { get; private set; }
        public Patient Patient { get; private set; }
        public DateTime ScheduledAt { get; private set; }
        public Consultation? Consultation { get; private set; }
        
        private Appointment() { }

        public Appointment(Doctor doctor, Patient patient, DateTime scheduledAt)
        {
            Id = Guid.NewGuid();
            Doctor = doctor;
            Patient = patient;
            ScheduledAt = scheduledAt;
        }

        public void AddConsultation(Consultation consultation)
        {
            Consultation = consultation;
        }
    }
}
