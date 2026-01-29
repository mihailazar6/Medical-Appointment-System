using MedicalAppointmentSystem.Domain;
using MedicalAppointmentSystem.Domain.Entities;
using MedicalAppointmentSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Application.Factories
{
    public class UserFactory
    {
        public User Create(
            UserRole role,
            string username,
            string passwordHash,
            Doctor? doctor = null)
        {
            return role switch
            {
                UserRole.Admin => new AdminUser(username, passwordHash),
                UserRole.Doctor when doctor != null => new DoctorUser(username, passwordHash, doctor),
                _ => throw new ArgumentException("Invalid user configuration")
            };
        }
    }
}
