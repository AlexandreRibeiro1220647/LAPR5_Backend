using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoApi.Models.OperationRequest;


public class AppointmentSurgeryConfiguration : IEntityTypeConfiguration<AppointmentSurgery>
    {
        public void Configure(EntityTypeBuilder<AppointmentSurgery> builder)
        {
            builder.ToTable("AppointmentSurgeries");

            // Definir a chave primária
            builder.HasKey(a => a.Id);

            // Conversores para propriedades específicas
            var appointmentSurgeryIdConverter = new ValueConverter<AppointmentSurgeryID, string>(
                id => id.AsString(),
                value => new AppointmentSurgeryID(value)
            );

            var roomNumberConverter = new ValueConverter<RoomNumber, string>(
                id => id.AsString(),
                value => new RoomNumber(value)
            );

            var appointmentSurgeryNameConverter = new ValueConverter<AppointmentSurgeryName, string>(
                name => name.appointmentSurgeryName,
                value => new AppointmentSurgeryName(value)
            );

            var operationRequestIdConverter = new ValueConverter<OperationRequestID, string>(
                id => id.AsString(),
                value => new OperationRequestID(value)
            );

            var appointmentSurgeryDateConverter = new ValueConverter<AppointmentSurgeryDate, DateTime>(
                date => DateTime.SpecifyKind(date.date.ToDateTime(TimeOnly.MinValue) , DateTimeKind.Utc),
                value => new AppointmentSurgeryDate(DateOnly.FromDateTime(value.ToUniversalTime()))
            );

            // Configurações das propriedades
            builder.Property(a => a.Id)
                .IsRequired()
                .HasConversion(appointmentSurgeryIdConverter);

            builder.Property(a => a.RoomId)
                .IsRequired()
                .HasConversion(roomNumberConverter);

            builder.Property(a => a.AppointmentSurgeryName)
                .IsRequired()
                .HasConversion(appointmentSurgeryNameConverter);

            builder.Property(a => a.OperationRequestID)
                .IsRequired()
                .HasConversion(operationRequestIdConverter);

            builder.Property(a => a.AppointmentSurgeryDate)
                .IsRequired()
                .HasConversion(appointmentSurgeryDateConverter);

            builder.Property(a => a.AppointmentSurgeryStatus)
                .IsRequired()
                .HasConversion<int>(); // Enum armazenado como inteiro

            builder.Property(a => a.StartTime)
                .IsRequired();

            builder.Property(a => a.EndTime)
                .IsRequired();
        }
    }
