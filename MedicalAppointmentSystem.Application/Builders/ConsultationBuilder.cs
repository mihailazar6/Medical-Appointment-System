using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MedicalAppointmentSystem.Domain.Entities;

namespace MedicalAppointmentSystem.Application.Builders
{
    public class ConsultationBuilder
    {
        private string _diagnosis = string.Empty;
        private string _treatment = string.Empty;

        public ConsultationBuilder WithDiagnosis(string diagnosis)
        {
            _diagnosis = diagnosis;
            return this;
        }

        public ConsultationBuilder WithTreatment(string treatment)
        {
            _treatment = treatment;
            return this;
        }

        public Consultation Build()
        {
            return new Consultation(_diagnosis, _treatment);
        }
    }
}

