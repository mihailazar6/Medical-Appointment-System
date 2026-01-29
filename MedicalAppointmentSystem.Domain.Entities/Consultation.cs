

using System;

namespace MedicalAppointmentSystem.Domain.Entities
{
    public class Consultation
    {
        public Guid Id { get; private set; }
        public string Diagnosis { get; private set; }
        public string Treatment { get; private set; }
        public DateTime CreatedAt { get; private set; }

        // Make this constructor public so code in other assemblies (e.g., Application.Builders) can instantiate Consultation.
        public Consultation(string diagnosis, string treatment)
        {
            Id = Guid.NewGuid();
            Diagnosis = diagnosis ?? string.Empty;
            Treatment = treatment ?? string.Empty;
            CreatedAt = DateTime.UtcNow;
        }
    }
}