using MedicalAppointmentSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalAppointmentSystem.Infrastructure.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.Doctor)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Patient)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Consultation)
                .WithOne()
                .HasForeignKey<Consultation>("AppointmentId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
