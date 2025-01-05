using System.Collections.Generic;
using TodoApi.Models.Specialization;
namespace TodoApi.DTOs.Specializations
{
    public class CreateSpecializationDto
    {
        public string SpecializationDesignation { get; set; }
        public string SpecializationCode { get; set; }
        public string SpecializationDescription { get; set; }

        public CreateSpecializationDto(string specializationDesignation, string specializationCode, string specializationDescription)
        {
            this.SpecializationDesignation = specializationDesignation;
            this.SpecializationCode = specializationCode;
            this.SpecializationDescription = specializationDescription;
        }
    }
}