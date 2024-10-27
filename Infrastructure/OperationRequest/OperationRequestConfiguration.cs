
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoApi.Models.OperationRequest;

namespace TodoApi.Infrastructure.OperationRequest
{
    public class OperationRequestConfiguration : IEntityTypeConfiguration<Models.OperationRequest.OperationRequest>
    {
        public void Configure(EntityTypeBuilder<Models.OperationRequest.OperationRequest> builder)
        {
             builder.ToTable("OperationRequests");

            // Definir a chave primária
            builder.HasKey(o => o.Id);

            
            var operationIdConverter = new ValueConverter<Models.OperationRequest.OperationRequestID, string>(
                id => id.AsString(),
                value => new Models.OperationRequest.OperationRequestID(value)
            );

            
            var pacientIdConverter = new ValueConverter<Models.Patient.MedicalRecordNumber, Guid>(
                id => id.AsGuid(),
                value => new Models.Patient.MedicalRecordNumber(value)
            );

            
            var doctorIdConverter = new ValueConverter<Models.Staff.LicenseNumber, string>(
                id => id.AsString(),
                value => new Models.Staff.LicenseNumber(value)
            );

            
            var operationTypeIdConverter = new ValueConverter<Models.OperationType.OperationTypeID, string>(
                id => id.AsString(),
                value => new Models.OperationType.OperationTypeID(value)
            );

            
            var deadlineConverter = new ValueConverter<Deadline, DateTime>(
                deadline => DateTime.SpecifyKind(deadline.deadline.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc),
                value => new Deadline(DateOnly.FromDateTime(value.ToUniversalTime())) // Certifique-se de converter para UTC
            );

            // Configurações das propriedades

            builder.Property(o => o.Id)
                .IsRequired()
                .HasConversion(operationIdConverter); 

            builder.Property(o => o.PacientId)
                .IsRequired()
                .HasConversion(pacientIdConverter); 

            builder.Property(o => o.DoctorId)
                .IsRequired()
                .HasConversion(doctorIdConverter); 

            builder.Property(o => o.OperationTypeID)
                .IsRequired()
                .HasConversion(operationTypeIdConverter); 

            builder.Property(o => o.Deadline)
                .IsRequired()
                .HasConversion(deadlineConverter);
            builder.Property(o => o.Priority)
                .IsRequired()
                .HasConversion<int>(); // Enum armazenado como inteiro
        }
    }
    }
