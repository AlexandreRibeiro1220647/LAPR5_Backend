using TodoApi.Models;
using TodoApi.Models.OperationRequest;
using TodoApi.Models.OperationType;
using TodoApi.Models.Patient;
using TodoApi.Models.Staff;
using Xunit;

public class OperationRequestDomainTests
{
[Fact]
public void OperationRequest_Should_SetPropertiesCorrectly()
{

    var patientId = new MedicalRecordNumber(Guid.NewGuid());
    var doctorId = new LicenseNumber("D32");
    var operationTypeId = new OperationTypeID(Guid.NewGuid());
    var deadline = new Deadline(DateOnly.FromDateTime(DateTime.Today));
    var priority = Priority.EMERGENCY;


    var operationRequest = new OperationRequest(patientId, doctorId, operationTypeId, deadline, priority);


    Assert.Equal(patientId, operationRequest.PacientId);
    Assert.Equal(doctorId, operationRequest.DoctorId);
    Assert.Equal(operationTypeId, operationRequest.OperationTypeID);
    Assert.Equal(deadline, operationRequest.Deadline);
    Assert.Equal(priority, operationRequest.Priority);
}


[Fact]
public void UpdateDeadline_Should_Update_Deadline_Properly()
{

        var operationRequest = new OperationRequest(
            new MedicalRecordNumber(Guid.NewGuid()),
            new LicenseNumber("D23"),
            new OperationTypeID(Guid.NewGuid()),
            new Deadline(DateOnly.FromDateTime(DateTime.Now)),
            Priority.EMERGENCY);

        var newDeadline = DateOnly.FromDateTime(DateTime.Now.AddDays(5));


        operationRequest.UpdateDeadline(newDeadline);

        Assert.Equal(newDeadline, operationRequest.Deadline.deadline);
    }

[Fact]
public void UpdatePriority_Should_Update_Priority_Properly()
{

    var operationRequest = new OperationRequest(
            new MedicalRecordNumber(Guid.NewGuid()),
            new LicenseNumber("456"),
            new OperationTypeID(Guid.NewGuid()),
            new Deadline(DateOnly.FromDateTime(DateTime.Now)),
            Priority.ELECTIVE);

        var newPriority = Priority.EMERGENCY;


        operationRequest.UpdatePriority(newPriority);


        Assert.Equal(newPriority, operationRequest.Priority);
    }
}