using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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

        builder.Property(p => p.Id)
            .HasConversion(staffScheduleIdConverter);

        // Configure JSON serialization for the `schedule` property
        var scheduleConverter = new ValueConverter<List<DailySchedule>, string>(
            schedule => JsonSerializer.Serialize(schedule, (JsonSerializerOptions)null), // Serialize to JSON
            json => JsonSerializer.Deserialize<List<DailySchedule>>(json, (JsonSerializerOptions)null) // Deserialize from JSON
        );

        builder.Property(p => p.schedule)
            .HasConversion(scheduleConverter) // Use the JSON converter
            .HasColumnType("text")
            .IsRequired();
    }
}
}