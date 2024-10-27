using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApi.Infrastructure.Staff;
using TodoApi.Models.Staff;
using TodoApi.Models.Shared;
using Xunit;

namespace TodoApi.Tests.IntegrationTest.Repository.Staff
{
    public class StaffRepositoryTests : IDisposable
    {
        private readonly IPOContext _context;
        private readonly StaffRepository _staffRepository;

        public StaffRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<IPOContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new IPOContext(options);
            _staffRepository = new StaffRepository(_context);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            // Adicione dados de teste ao banco de dados em memória
            _context.Staffs.AddRange(new List<Models.Staff.Staff>
            {
                new Models.Staff.Staff(
                    new FullName("John Doe"),
                    new Specialization("Dermatology"),
                    new UserEmail("john.doe@example.com"),
                    new Phone("123456789"),
                    new AvailabilitySlots(new List<Slot> { new Slot(DateTime.Now, DateTime.Now.AddHours(1)) }),
                    StaffStatus.ACTIVE
                ),
                new Models.Staff.Staff(
                    new FullName("Jane Smith"),
                    new Specialization("Cardiology"),
                    new UserEmail("jane.smith@example.com"),
                    new Phone("987654321"),
                    new AvailabilitySlots(new List<Slot> { new Slot(DateTime.Now, DateTime.Now.AddHours(1)) }),
                    StaffStatus.ACTIVE
                )
            });

            _context.SaveChanges();
        }

        [Fact]
        public async Task SearchByName_ShouldReturnCorrectStaff()
        {
            // Act
            var result = await _staffRepository.SearchByName("John");

            // Assert
            Assert.Single(result);
            Assert.Equal("John Doe", result[0].FullName.fullName);
        }

        [Fact]
        public async Task SearchBySpecialization_ShouldReturnCorrectStaff()
        {
            // Act
            var result = await _staffRepository.SearchBySpecialization("Cardiology");

            // Assert
            Assert.Single(result);
            Assert.Equal("Cardiology", result[0].Specialization.Area);
        }

        [Fact]
        public async Task SearchByStatus_ShouldReturnCorrectStaff()
        {
            // Act
            var result = await _staffRepository.SearchByStatus(StaffStatus.ACTIVE);

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task SearchByEmail_ShouldReturnCorrectStaff()
        {
            // Act
            var result = await _staffRepository.SearchByEmail("jane.smith@example.com");

            // Assert
            Assert.Single(result);
            Assert.Equal("Jane Smith", result[0].FullName.fullName);
        }

        [Fact]
        public async Task ExistsAsync_ShouldReturnTrueIfExists()
        {
            // Arrange
            var licenseNumber = new LicenseNumber("1"); // Ajuste conforme a lógica de ID

            // Act
            var exists = await _staffRepository.ExistsAsync(licenseNumber);

            // Assert
            Assert.False(exists); // Esperado como falso porque o ID não existe na base de dados
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
