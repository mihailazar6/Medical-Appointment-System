using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MedicalAppointmentSystem.Domain.Entities;

namespace MedicalAppointmentSystem.Application.Interfaces
{
    public interface IUserRepository
    {
        User? GetByUsername(string username);
    }
}
