using System;
using System.Collections.Generic;
using TodoApi.Models.Specialization;

namespace TodoApi.DTOs.Specializations
{
    public class SpecializationDto
    {
        public Guid Id { get; set; }

        public string SpecializationDesignation { get; set; }
        public string SpecializationCode { get; set; }
        public string SpecializationDescription { get; set; }

    }
}