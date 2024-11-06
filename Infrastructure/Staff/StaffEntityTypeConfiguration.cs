using TodoApi.Models;
using TodoApi.Models.Shared;
using TodoApi.Models.Staff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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

        var userEmailConverter = new ValueConverter<UserEmail, string>(
            ue => ue.Value,
            s => new UserEmail(s)
        );

        builder.Property(s => s.UserId)
               .IsRequired();

        builder.Property(s => s.Email)
            .HasConversion(userEmailConverter);

        builder.OwnsOne(s => s.Specialization);
        builder.OwnsOne(s => s.FullName);

        builder.OwnsOne(s => s.AvailabilitySlots, availabilityBuilder =>
        {
        });

        builder.HasIndex(s => s.Email).IsUnique();
    }
}