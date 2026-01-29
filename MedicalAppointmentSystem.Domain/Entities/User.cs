using MedicalAppointmentSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Domain.Entities
{
    public abstract class User
    {
        public Guid Id { get; protected set; }
        public string Username { get; protected set; }
        public string PasswordHash { get; protected set; }
        public UserRole Role { get; protected set; }

        protected User() { }

        protected User(string username, string passwordHash, UserRole role)
        {
            Id = Guid.NewGuid();
            Username = username;
            PasswordHash = passwordHash;
            Role = role;
        }
    }
}
