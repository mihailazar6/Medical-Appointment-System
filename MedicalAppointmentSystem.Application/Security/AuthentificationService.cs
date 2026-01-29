using MedicalAppointmentSystem.Application.Interfaces;
using MedicalAppointmentSystem.Application.Security;
using MedicalAppointmentSystem.Application.Singleton;

using MedicalAppointmentSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Application.Services
{
    public class AuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Login(string username, string password)
        {
            User? user = _userRepository.GetByUsername(username);
            if (user == null)
                return false;

            string hashed = PasswordHasher.Hash(password);
            if (user.PasswordHash != hashed)
                return false;

            UserSession.Instance.Login(user);
            return true;
        }
    }
}

