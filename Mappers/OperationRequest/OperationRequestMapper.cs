using TodoApi.Models.OperationRequest;
using TodoApi.Models.OperationType;
using TodoApi.Models.Patient;
using TodoApi.Models.Staff;

namespace TodoApi.Mappers.OperationRequest;

public class OperationRequestMapper : IOperationRequestMapper
{
  
    public OperationRequestMapper() {}

     public Models.OperationRequest.OperationRequest ToEntity(OperationRequestDTO dto)
    {
        throw new NotImplementedException();
    }

    public OperationRequestDTO ToDto(Models.OperationRequest.OperationRequest entity)
    {
        return new OperationRequestDTO(entity.Id.AsString(), entity.PacientId.AsString(), entity.DoctorId.AsString(),
            entity.OperationTypeID.AsString(), entity.Deadline.deadline.ToString(), entity.Priority.ToString());
    }

    public Models.OperationRequest.OperationRequest toEntity(CreateOperationRequestDTO createDto)
    {
        return new Models.OperationRequest.OperationRequest(new MedicalRecordNumber(createDto.pacientid),
            new LicenseNumber(createDto.doctorid), new OperationTypeID(createDto.operationTypeId),new Deadline(DateOnly.Parse(createDto.deadline)), createDto.priority);
    }

}