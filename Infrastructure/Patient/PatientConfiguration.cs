﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoApi.Models;
using TodoApi.Models.Patient;

namespace TodoApi.Infrastructure.Patient {
    public class PatientConfiguration : IEntityTypeConfiguration<Models.Patient.Patient> {
        public void Configure(EntityTypeBuilder<Models.Patient.Patient> builder) {
            builder.HasKey(b => b.Id);

            builder.Property(p => p.Id).HasConversion(new ValueConverter<MedicalRecordNumber, Guid>(m => m.AsGuid(), g => new MedicalRecordNumber(g)));

            builder.Property(p => p.dateOfBirth).HasConversion(new ValueConverter<DateOfBirth, DateOnly>(b => b.dateOfBirth, d => new DateOfBirth(d)));

            builder.Property(p => p.email).HasConversion(new ValueConverter<UserEmail, string>(e => e.Value, s => new UserEmail(s)));

            builder.Property(p => p.contactInformation).HasConversion(new ValueConverter<ContactInformation, string>(c => c.contactInformation.ToString(), s => new ContactInformation(new Phone(s))));

            builder.Property(p => p.emergencyContact).HasConversion(new ValueConverter<EmergencyContact, string>(c => c.emergencyContact.ToString(), s => new EmergencyContact(new Phone(s))));


            builder.OwnsOne(p => p.fullName);
            builder.HasIndex(p => p.dateOfBirth);
            builder.HasIndex(p => p.gender);
            builder.HasIndex(p => p.contactInformation).IsUnique();
            builder.HasIndex(p => p.email).IsUnique();
            builder.OwnsOne(p => p.medicalConditions);
            builder.OwnsOne(p => p.emergencyContact);
            builder.OwnsOne(p => p.appointmentHistory);
        }
    }
}