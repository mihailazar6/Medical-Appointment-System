using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalAppointmentSystem.Domain.Entities;

namespace MedicalAppointmentSystem.Application.Observers
{
    public class AppointmentLogger : IAppointmentObserver
    {
        public void OnScheduled(Appointment appointment)
        {
            Console.WriteLine($"Appointment scheduled: {appointment.Id}");
        }
    }
}
