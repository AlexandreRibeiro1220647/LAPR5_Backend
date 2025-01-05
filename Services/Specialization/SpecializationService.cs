using System.Threading.Tasks;
using System.Collections.Generic;
using TodoApi.Models.Shared;
using System.Formats.Asn1;
using Microsoft.CodeAnalysis;
using System;
using System.Linq;
using TodoApi.Models.Specialization;
using TodoApi.DTOs.Specializations;
using TodoApi.Infrastructure.Specialization;

namespace TodoApi.Services.Specialization
{
    public class SpecializationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISpecializationRepository _repo;


        public SpecializationService(IUnitOfWork unitOfWork, ISpecializationRepository repo)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
        }

        public async Task<List<SpecializationDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();
            
            List<SpecializationDto> listDto = list.ConvertAll<SpecializationDto>(spec => 
                new SpecializationDto
                {
                    Id = spec.Id.AsGuid(),
                    SpecializationDesignation = spec.SpecializationDesignation.ToString(),
                    SpecializationCode = spec.SpecializationCode.ToString(),
                    SpecializationDescription = spec.SpecializationDescription.ToString(),
                });

            return listDto;
        }

        public async Task<SpecializationDto> GetByIdAsync(SpecializationId id)
        {
            var spec = await this._repo.GetByIdAsync(id);
            
            if(spec == null)
                return null;

            return new SpecializationDto
            {
                Id = spec.Id.AsGuid(),
                SpecializationDesignation = spec.SpecializationDesignation.ToString()
            };
        }

        public async Task<SpecializationDto> AddAsync(CreateSpecializationDto dto)
        {
            var specialization = new Models.Specialization.Specialization(
                new SpecializationDesignation(dto.SpecializationDesignation),
                new SpecializationCode(dto.SpecializationCode),
                new SpecializationDescription(dto.SpecializationDescription)
            );

            await this._repo.AddAsync(specialization);
            await this._unitOfWork.CommitAsync();

            return new SpecializationDto
            {
                Id = specialization.Id.AsGuid(),
                SpecializationDesignation = specialization.SpecializationDesignation.ToString(),
                SpecializationCode = specialization.SpecializationCode.ToString(),
                SpecializationDescription = specialization.SpecializationDescription.ToString(),
            };
        }

        public async Task<SpecializationDto> UpdateAsync(SpecializationDto dto)
        {
            var specialization = await this._repo.GetByIdAsync(new SpecializationId(dto.Id));

            if (specialization == null)
                return null;

            specialization.ChangeSpecializationDesignation(new SpecializationDesignation(dto.SpecializationDesignation));
            specialization.UpdateSpecializationDescription(new SpecializationDescription(dto.SpecializationDescription));
            
            await this._unitOfWork.CommitAsync();

            return new SpecializationDto
            {
                Id = specialization.Id.AsGuid(),
                SpecializationDesignation = specialization.SpecializationDesignation.ToString(),
                SpecializationDescription = specialization.SpecializationDescription.ToString(),

            };
        }

        public async Task<SpecializationDto> InactivateAsync(SpecializationId id)
        {
            var specialization = await this._repo.GetByIdAsync(id);

            if (specialization == null)
                return null;

            await this._unitOfWork.CommitAsync();

            return new SpecializationDto
            {
                Id = specialization.Id.AsGuid(),
                SpecializationDesignation = specialization.SpecializationDesignation.ToString(),
                SpecializationCode = specialization.SpecializationCode.ToString(),
                SpecializationDescription = specialization.SpecializationDescription.ToString(),
            };
        }

        public async Task<SpecializationDto> DeleteAsync(SpecializationId id)
        {
            var specialization = await this._repo.GetByIdAsync(id);

            if (specialization == null)
                return null;
            
            this._repo.Remove(specialization);
            await this._unitOfWork.CommitAsync();

            return new SpecializationDto
            {
                Id = specialization.Id.AsGuid(),
                SpecializationDesignation = specialization.SpecializationDesignation.ToString(),
                SpecializationCode = specialization.SpecializationCode.ToString(),
                SpecializationDescription = specialization.SpecializationDescription.ToString(),
            };
        }

        public async Task<IEnumerable<SpecializationDto>> SearchSpecializationsAsync(SearchSpecializationDto searchDto)
        {
            var specializations = await _repo.GetAllAsync();

            IEnumerable<Models.Specialization.Specialization> filteredSpecializations = specializations;

            if (!string.IsNullOrEmpty(searchDto.Designation))
            {
                filteredSpecializations = filteredSpecializations
                    .Where(o => o.SpecializationDesignation.ToString().IndexOf(searchDto.Designation, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (!string.IsNullOrEmpty(searchDto.Code))
            {
                filteredSpecializations = filteredSpecializations
                    .Where(o => o.SpecializationCode.ToString()
                    .Equals(searchDto.Code, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(searchDto.Description))
            {
                filteredSpecializations = filteredSpecializations
                    .Where(o => o.SpecializationDescription.ToString().IndexOf(searchDto.Description, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            return filteredSpecializations.Select(o => new SpecializationDto
            {
                Id = o.Id.AsGuid(),
                SpecializationDesignation = o.SpecializationDesignation.ToString(),
                SpecializationCode = o.SpecializationCode.ToString(),
                SpecializationDescription = o.SpecializationDescription.ToString()
            }).ToList();
        }
    }
}
