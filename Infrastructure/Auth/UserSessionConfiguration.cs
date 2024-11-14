using TodoApi.Models.Auth;
using TodoApi.Models.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoApi.Models;

public class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
{
    public void Configure(EntityTypeBuilder<UserSession> builder)
    {
        builder.HasKey(u => u.Id);

        // Value converter for UserID
        var userSessionIdConverter = new ValueConverter<UserSessionID, string>(
            id => id.AsString(),
            value => new UserSessionID(value)
        );

        builder.Property(u => u.Id)
            .HasConversion(userSessionIdConverter);

        builder.Property(u => u.SessionId)
            .IsRequired();

        builder.Property(u => u.AccessToken)
            .IsRequired();

        builder.Property(u => u.IsAuthenticated)
            .IsRequired();
    }
}