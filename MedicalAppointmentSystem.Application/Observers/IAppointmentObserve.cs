using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MedicalAppointmentSystem.Domain.Entities;

namespace MedicalAppointmentSystem.Application.Observers
{
    public interface IAppointmentObserver
    {
        void OnScheduled(Appointment appointment);
    }
}

