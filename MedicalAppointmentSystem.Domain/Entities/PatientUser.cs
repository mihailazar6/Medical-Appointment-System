using MedicalAppointmentSystem.Domain.Enums;

namespace MedicalAppointmentSystem.Domain.Entities
{
    public class PatientUser : User
    {
        public Patient PatientProfile { get; private set; }

        private PatientUser() { }

        public PatientUser(string username, string passwordHash, Patient patient)
            : base(username, passwordHash, UserRole.Patient)
        {
            PatientProfile = patient;
        }
    }
}
