using System;
using System.ComponentModel.DataAnnotations;
using TodoApi.Models.Shared;

namespace TodoApi.DTOs.Staff
{
   public class StaffSearchDto
    {
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string SpecializationId { get; set; }
    public bool? Active { get; set; }
    
    }
}
