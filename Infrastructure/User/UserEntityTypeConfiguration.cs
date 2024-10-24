using TodoApi.Models.User;
using TodoApi.Models.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoApi.Models;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        // Value converter for UserID
        var userIdConverter = new ValueConverter<UserID, string>(
            id => id.AsString(),
            value => new UserID(value)
        );

        // Value converter for UserEmail
        var userEmailConverter = new ValueConverter<UserEmail, string>(
            email => email.Value,
            value => new UserEmail(value)
        );

        // Value converter for Password
        var passwordConverter = new ValueConverter<Password, string>(
            password => password.Value,
            value => new Password(value)
        );

        builder.Property(u => u.Id)
            .HasConversion(userIdConverter);

        builder.Property(u => u.Email)
            .HasConversion(userEmailConverter)
            .IsRequired()
            .HasMaxLength(60);

        builder.Property(u => u.Password)
            .HasConversion(passwordConverter)
            .IsRequired()
            .HasMaxLength(60);

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(60);

        builder.Property(u => u.Role)
            .IsRequired()
            .HasMaxLength(30);
    }
}