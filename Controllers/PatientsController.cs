using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.DTOs;
using TodoApi.Infrastructure;
using TodoApi.Models.Patient;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService) {
            _patientService = patientService;
        }

        [HttpPost]
        public async Task<ActionResult<PatientDTO>> RegisterPatient([FromBody] RegisterPatientDTO dto) {
            try {
                var patient = await _patientService.RegisterPatient(dto);
                return Ok(patient);
            } catch (Exception e) {
                return BadRequest(e.Message);
            }        
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PatientDTO>> UpdatePatient(Guid id, [FromBody] UpdatePatientDTO dto) {
            try {
                var newPatient = await _patientService.UpdatePatientAsync(id, dto);
                return Ok(newPatient);
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDTO>>> GetAllPatients() {
            try {
                var patients = await _patientService.GetAllPatients();
                return Ok(patients);
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> DeletePatientByEmail(string email) {
            try {
                var result = await _patientService.DeletePatientByEmailAsync(email);
                return Ok();
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }
    }
}
