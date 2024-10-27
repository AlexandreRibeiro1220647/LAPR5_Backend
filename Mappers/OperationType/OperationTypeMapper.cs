
using TodoApi.DTOs.OperationType;

namespace TodoApi.Mappers.OperationType;    
public class OperationTypeMapper : IOperationTypeMapper
    {
        public Models.OperationType.OperationType ToEntity(OperationTypeDTO dto)
        {
            return new Models.OperationType.OperationType(
                dto.Name,
                dto.RequiredStaffBySpecialization,
                dto.EstimatedDuration
            );

        }

        public OperationTypeDTO ToDto(Models.OperationType.OperationType entity)
        {
            return new OperationTypeDTO(
                entity.Name,
                entity.RequiredStaffBySpecialization,
                entity.EstimatedDuration,
                entity.Id.AsString(),
                entity.IsActive
            )
            {
                IsActive = entity.IsActive
            };
        }

        public Models.OperationType.OperationType toEntity(CreateOperationTypeDTO createDto)
        {
            return new Models.OperationType.OperationType(
                createDto.Name,
                createDto.RequiredStaffBySpecialization,
                createDto.EstimatedDuration
            );
        }
    }
