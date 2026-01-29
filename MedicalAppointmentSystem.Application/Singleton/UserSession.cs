using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalAppointmentSystem.Domain.Entities;


namespace MedicalAppointmentSystem.Application.Singleton
{
    public sealed class UserSession
    {
        private static readonly Lazy<UserSession> _instance =
            new(() => new UserSession());

        public static UserSession Instance => _instance.Value;

        public User? CurrentUser { get; private set; }

        private UserSession() { }

        public void Login(User user)
        {
            CurrentUser = user;
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}
