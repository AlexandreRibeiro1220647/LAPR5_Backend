
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoApi.Models.OperationType;

namespace TodoApi.Infrastructure.OperationType
{
public class OperationTypeConfiguration :  IEntityTypeConfiguration<Models.OperationType.OperationType>
    {
        public void Configure(EntityTypeBuilder<Models.OperationType.OperationType> builder)
        {
            builder.HasKey(b => b.Id);

             var operationTypeIdConverter = new ValueConverter<OperationTypeID, Guid>(
                id => Guid.Parse(id.AsString()),
                guid => new OperationTypeID(guid)
            );

            // Value converter for TimeSpan
            var timeSpanConverter = new ValueConverter<TimeSpan, long>(
                timeSpan => timeSpan.Ticks,
                ticks => TimeSpan.FromTicks(ticks)
            );


            builder.Property(p => p.Id)
                .HasConversion(operationTypeIdConverter)
                .IsRequired();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(p => p.Name)
                .IsUnique();

            builder.Property(p => p.RequiredStaffBySpecialization)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.EstimatedDuration)
                .HasConversion(timeSpanConverter)
                .IsRequired();
        
        }
    }
}