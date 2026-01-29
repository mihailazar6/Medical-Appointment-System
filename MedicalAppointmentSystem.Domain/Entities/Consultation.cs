using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Domain.Entities
{
    public class Consultation
    {
        public Guid Id { get; private set; }
        public string Diagnosis { get; private set; }
        public string Treatment { get; private set; }
        public DateTime CreatedAt { get; private set; }
        
        private Consultation() { }

        public Consultation(
            string diagnosis,
            string treatment)
        {
            Id = Guid.NewGuid();
            Diagnosis = diagnosis;
            Treatment = treatment;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
