﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoApi.Models;
using TodoApi.Models.Patient;
using TodoApi.Models.Shared;

namespace TodoApi.Infrastructure.Patient
{
    public class PatientConfiguration : IEntityTypeConfiguration<Models.Patient.Patient>
    {
        public void Configure(EntityTypeBuilder<Models.Patient.Patient> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(p => p.Id)
                .HasConversion(new ValueConverter<MedicalRecordNumber, Guid>(
                    m => m.AsGuid(),
                    g => new MedicalRecordNumber(g)));

            builder.Property(p => p.dateOfBirth)
                .HasConversion(new ValueConverter<DateOfBirth, DateOnly>(
                    b => b.dateOfBirth,
                    d => new DateOfBirth(d)));

            builder.HasIndex(p => p.dateOfBirth);
            builder.HasIndex(p => p.gender);

            builder.OwnsOne(p => p.contactInformation, contactInfoBuilder =>
            {
                contactInfoBuilder.Property(c => c.contactInformation)
                    .HasConversion(new ValueConverter<Phone, string>(
                        p => p.phoneNumber,
                        s => new Phone(s)));
            });

            builder.OwnsOne(p => p.medicalRecord, medicalRecordBuilder =>
            {
                medicalRecordBuilder.OwnsMany(m => m.Allergies, allergyBuilder =>
                {
                    allergyBuilder.HasKey(a => a.Id);
                    allergyBuilder.Property(a => a.Id)
                        .HasConversion(new ValueConverter<AllergyId, Guid>(
                            id => id.Value,
                            value => new AllergyId(value)));
                    allergyBuilder.WithOwner().HasForeignKey("PatientId");

                    allergyBuilder.Property(a => a.Code).IsRequired();
                    allergyBuilder.Property(a => a.Designation).IsRequired();
                    allergyBuilder.Property(a => a.Description);
                });

                medicalRecordBuilder.OwnsMany(m => m.MedicalConditions, conditionBuilder =>
                {
                    conditionBuilder.HasKey(c => c.Id);
                    conditionBuilder.Property(c => c.Id)
                        .HasConversion(new ValueConverter<MedicalConditionId, Guid>(
                            id => id.Value,
                            value => new MedicalConditionId(value)));
                    conditionBuilder.WithOwner().HasForeignKey("PatientId");

                    conditionBuilder.Property(c => c.Code).IsRequired();
                    conditionBuilder.Property(c => c.Designation).IsRequired();
                    conditionBuilder.Property(c => c.Description).IsRequired();
                    conditionBuilder.Property(c => c.CommonSymptoms).IsRequired();
                });
            });

            builder.OwnsOne(p => p.emergencyContact, eContactInfoBuilder =>
            {
                eContactInfoBuilder.Property(c => c.emergencyContact)
                    .HasConversion(new ValueConverter<Phone, string>(
                        p => p.phoneNumber,
                        s => new Phone(s)));
            });

            builder.OwnsOne(p => p.appointmentHistory);

            builder.OwnsOne(p => p.user, userBuilder =>
            {
                userBuilder.Property(u => u.Id).HasColumnName("UserId");
                userBuilder.Property(u => u.Name).HasColumnName("UserName").HasMaxLength(100);
                userBuilder.Property(u => u.Email).HasColumnName("UserEmail").HasMaxLength(200)
                    .HasConversion(
                        email => email.Value,
                        value => new UserEmail(value));
            });
        }
    }
}