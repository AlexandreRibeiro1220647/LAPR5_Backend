using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApi.Models.Specialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace TodoApi.Infrastructure.Specialization;

internal class SpecializationEntityTypeConfiguration : IEntityTypeConfiguration<Models.Specialization.Specialization>
{
    public void Configure(EntityTypeBuilder<Models.Specialization.Specialization> builder)
    {
        builder.HasKey(b => b.Id);

        var specializationIdConverter = new ValueConverter<SpecializationId, string>(
            id => id.AsString(),
            value => new SpecializationId(value)
        );

        builder.Property(p => p.Id)
            .HasConversion(specializationIdConverter);

        builder.Property(b => b.SpecializationDesignation)
               .HasConversion(
                   b => b.Designation,
                   b => new SpecializationDesignation(b)
               )
               .IsRequired();

        builder.HasIndex(p => p.SpecializationCode)
               .IsUnique();

        builder.Property(b => b.SpecializationCode)
               .HasConversion(
                   b => b.Code,
                   b => new SpecializationCode(b)
               )
               .IsRequired();

        builder.Property(b => b.SpecializationDescription)
               .HasConversion(
                   b => b.Description,
                   b => new SpecializationDescription(b)
               )
               .IsRequired(false);
   }
}