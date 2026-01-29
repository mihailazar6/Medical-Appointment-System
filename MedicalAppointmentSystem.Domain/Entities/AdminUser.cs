using MedicalAppointmentSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Domain.Entities
{
    public class AdminUser : User
    {
        private AdminUser() { }

        public AdminUser(string username, string passwordHash)
            : base(username, passwordHash, UserRole.Admin)
        {
        }
    }
}
