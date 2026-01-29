using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MedicalAppointmentSystem.Domain.Entities;

namespace MedicalAppointmentSystem.Application.Strategies
{
    public class NoOverlapStrategy : ISchedulingStrategy
    {
        public bool IsValid(Doctor doctor, DateTime date)
        {
            // aici vei verifica în DB ulterior
            return true;
        }
    }
}
