using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MedicalAppointmentSystem.Application.Interfaces;
using MedicalAppointmentSystem.Domain.Entities;
using MedicalAppointmentSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MedicalAppointmentSystem.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MedicalDbContext _context;

        public UserRepository(MedicalDbContext context)
        {
            _context = context;
        }

        public User? GetByUsername(string username)
        {
            return _context.Users
                .Include(u => (u as DoctorUser)!.DoctorProfile)
                .FirstOrDefault(u => u.Username == username);
        }
    }
}
