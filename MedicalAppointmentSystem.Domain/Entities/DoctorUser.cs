using MedicalAppointmentSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Domain.Entities
{
    public class DoctorUser : User
    {
        public Doctor DoctorProfile { get; private set; }
        
        private DoctorUser() { }

        public DoctorUser(string username, string passwordHash, Doctor doctor)
            : base(username, passwordHash, UserRole.Doctor)
        {
            DoctorProfile = doctor;
        }
    }
}
