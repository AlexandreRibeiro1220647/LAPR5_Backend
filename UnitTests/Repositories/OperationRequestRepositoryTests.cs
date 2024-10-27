using Microsoft.EntityFrameworkCore;
using Moq;
using TodoApi.Infrastructure;
using TodoApi.Infrastructure.OperationRequest;
using TodoApi.Infrastructure.Patient;
using TodoApi.Models;
using TodoApi.Models.OperationRequest;
using TodoApi.Models.OperationType;
using TodoApi.Models.Patient;
using TodoApi.Models.Staff;

public class OperationRequestRepositoryIntegrationTests : IDisposable
{
    private readonly IPOContext
     _context;
    private readonly OperationRequestRepository _repository;

    public OperationRequestRepositoryIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<IPOContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;
        _context = new IPOContext(options);
        _repository = new OperationRequestRepository(_context, new Mock<IPatientRepository>().Object);

        // Seed any necessary test data
    }

    [Fact]
    public async Task ExistsAsync_ReturnsTrue_WhenOperationExists()
    {
        // Arrange
        var patientId = new MedicalRecordNumber(Guid.NewGuid());
        var operationTypeId = new OperationTypeID(Guid.NewGuid());
        await _context.OperationRequests.AddAsync(new TodoApi.Models.OperationRequest.OperationRequest
        ( patientId, new LicenseNumber(Guid.NewGuid().ToString()), operationTypeId, new Deadline(DateOnly.Parse("2024-12-14")), Priority.EMERGENCY 
        ));
        await _context.SaveChangesAsync();

        // Act
        var exists = await _repository.ExistsAsync(patientId, operationTypeId);

        Assert.True(exists);
    }
/*
    [Fact]
    public async Task SearchAsync_ReturnsCorrectData_OnValidSearchParameters()
    {
        var patientId = new MedicalRecordNumber(Guid.NewGuid());
        await _context.OperationRequests.AddAsync(new TodoApi.Models.OperationRequest.OperationRequest
        ( patientId, new LicenseNumber(Guid.NewGuid().ToString()), new OperationTypeID(Guid.NewGuid()), new Deadline(DateOnly.Parse("2024-12-14")), Priority.EMERGENCY 
        ));
        await _context.SaveChangesAsync();

        var results = await _repository.SearchAsync("", patientId.ToString(""), "", "", "");

        Assert.NotEmpty(results);
    }
*/

    [Fact]
    public async Task SearchAsync_ReturnsEmptyList_WhenNoMatchingData()
    {
        
        var results = await _repository.SearchAsync("","", "", "", "");

        Assert.Empty(results);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}

