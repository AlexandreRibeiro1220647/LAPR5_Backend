using TodoApi.Models;
using TodoApi.Models.Shared;
using TodoApi.Models.Staff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoApi.Models.Specialization;

namespace TodoApi.Infrastructure.Staff;

public class StaffEntityTypeConfiguration : IEntityTypeConfiguration<Models.Staff.Staff>
{
    public void Configure(EntityTypeBuilder<Models.Staff.Staff> builder)
    {
        builder.HasKey(s => s.Id);

        builder.OwnsOne(s => s.Phone);

        var licenseNumberConverter = new ValueConverter<LicenseNumber, string>(
            licenseNumber => licenseNumber.Value,
            value => new LicenseNumber(value)
        );

        builder.Property(s => s.Id)
               .HasConversion(licenseNumberConverter);

        builder.Property(s => s.SpecializationId)
               .HasConversion(
                   specializationId => specializationId.Value,
                   value => new SpecializationId(value)
               );

        builder.OwnsOne(s => s.AvailabilitySlots, availabilityBuilder =>
        {
        });

        builder.OwnsOne(p => p.user, userBuilder =>
        {
            userBuilder.Property(u => u.Id).HasColumnName("UserId");
            userBuilder.Property(u => u.Name).HasColumnName("UserName").HasMaxLength(100);
            userBuilder.Property(u => u.Email).HasColumnName("UserEmail").HasMaxLength(200).HasConversion(
                email => email.Value,
                value => new UserEmail(value)
            );
        });
    }
}
