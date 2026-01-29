using MedicalAppointmentSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalAppointmentSystem.Infrastructure.Configurations
{
    public class ConsultationConfiguration : IEntityTypeConfiguration<Consultation>
    {
        public void Configure(EntityTypeBuilder<Consultation> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Diagnosis).IsRequired();
            builder.Property(c => c.Treatment).IsRequired();
        }
    }
}
