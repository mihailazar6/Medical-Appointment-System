using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Domain.Entities
{
    public class Doctor
    {
        public Guid Id { get; private set; }
        public string FullName { get; private set; }
        public string Specialty { get; private set; }
        
        private Doctor() { }

        public Doctor(string fullName, string specialty)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            Specialty = specialty;
        }
    }
}
