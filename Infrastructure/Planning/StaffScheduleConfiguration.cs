using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Text.Json;
using TodoApi.Models;

namespace TodoApi.Infrastructure
{
public class StaffScheduleConfiguration : IEntityTypeConfiguration<StaffSchedule>
{
    public void Configure(EntityTypeBuilder<StaffSchedule> builder)
    {
        builder.HasKey(b => b.Id);

        var staffScheduleIdConverter = new ValueConverter<StaffScheduleID, string>(
            id => id.AsString(),
            value => new StaffScheduleID(value)
        );
            
        var doctorIdConverter = new ValueConverter<Models.Staff.LicenseNumber, string>(
            id => id.AsString(),
            value => new Models.Staff.LicenseNumber(value)
        );

        builder.Property(p => p.Id)
            .HasConversion(staffScheduleIdConverter)
            .IsRequired();

        builder.Property(p => p.DoctorId)
            .HasConversion(doctorIdConverter)
            .IsRequired();

        // Configure JSON serialization for the `schedule` property
        var scheduleConverter = new ValueConverter<List<DailySchedule>, string>(
            schedule => JsonSerializer.Serialize(schedule, (JsonSerializerOptions)null), // Serialize to JSON
            json => JsonSerializer.Deserialize<List<DailySchedule>>(json, (JsonSerializerOptions)null) // Deserialize from JSON
        );

        var scheduleComparer = new ValueComparer<List<DailySchedule>>(
            (c1, c2) => c1.SequenceEqual(c2), // Compares two collections for equality
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), // Hashes the collection
            c => c.ToList() // Makes a snapshot of the collection
        );

        builder.Property(p => p.schedule)
            .HasConversion(scheduleConverter) // Use the JSON converter
            .HasColumnType("text")
            .IsRequired()
            .Metadata.SetValueComparer(scheduleComparer);
    }
}
}