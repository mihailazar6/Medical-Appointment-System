using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MedicalAppointmentSystem.Domain.Entities;

namespace MedicalAppointmentSystem.Application.Strategies
{
    public class WorkingHoursStrategy : ISchedulingStrategy
    {
        public bool IsValid(Doctor doctor, DateTime date)
        {
            return date.Hour >= 9 && date.Hour <= 17;
        }
    }
}