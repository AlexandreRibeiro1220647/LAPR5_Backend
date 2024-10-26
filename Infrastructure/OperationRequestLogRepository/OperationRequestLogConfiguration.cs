using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoApi.Models;
using TodoApi.Models.OperationRequest;

namespace TodoApi.Infrastructure.OperationRequest
{
    public class OperationRequestLogConfiguration : IEntityTypeConfiguration<RequestsLog>
    {
        public void Configure(EntityTypeBuilder<RequestsLog> builder)
        {
            builder.ToTable("OperationRequestLogs");

            // Definir a chave primária
            builder.HasKey(log => log.Id);

            // Conversor para OperationRequestLogID
            var logIdConverter = new ValueConverter<OperationRequestLogID, string>(
                id => id.AsString(),
                value => new OperationRequestLogID(value)
            );

            // Conversor para OperationRequestID
            var operationRequestIdConverter = new ValueConverter<OperationRequestID, string>(
                id => id.AsString(),
                value => new OperationRequestID(value)
            );

            // Configurar as propriedades e conversores
            builder.Property(log => log.Id)
                .IsRequired()
                .HasConversion(logIdConverter);

            builder.Property(log => log.OperationRequestId)
                .IsRequired()
                .HasConversion(operationRequestIdConverter);

            builder.Property(log => log.ChangeDate)
                .IsRequired();

            builder.Property(log => log.ChangeDescription)
                .IsRequired()
                .HasMaxLength(600); // Define um limite para o tamanho da descrição
        }
    }
}