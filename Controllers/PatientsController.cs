using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.DTOs;
using TodoApi.Infrastructure;
using TodoApi.Models.Patient;
using TodoApi.Services;
using TodoApi.Services.Login;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase {
        private readonly IPatientService _patientService;
        private readonly ILoginService _loginService;

        public PatientsController(IPatientService patientService, ILoginService loginService) {
            _patientService = patientService;
            _loginService = loginService;
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

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDTO>>> GetAllPatients() {
            try {
                var patients = await _patientService.GetAllPatients();
                return Ok(patients);
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpGet("email/{email}")]
                public async Task<ActionResult<PatientDTO>> GetPatientByEmail(string email) {
                    try {
                        var patient = await _patientService.GetPatientByEmailAsync(email);
                        return Ok(patient);
                    } catch (Exception e) {
                        return BadRequest(e.Message);
                    }
                }
                
                [Authorize]
                [HttpGet("id/{id}")]
                public async Task<ActionResult<PatientDTO>> GetPatientById(Guid id) {
                    try {
                        var patient = await _patientService.GetPatientByIdAsync(id);
                        return Ok(patient);
                    } catch (Exception e) {
                        return BadRequest(e.Message);
                    }
                }

                [Authorize]
                [HttpGet("name/{name}")]
                public async Task<ActionResult<IEnumerable<PatientDTO>>> GetPatientsByName(string name) {
                    try {
                        var patients = await _patientService.GetPatientsByNameAsync(name);
                        return Ok(patients);
                    } catch (Exception e) {
                        return BadRequest(e.Message);
                    }
                }

                [Authorize]
                [HttpGet("contact/{contact}")]
                public async Task<ActionResult<IEnumerable<PatientDTO>>> GetPatientsByContactInformation(string contact) {
                    try {
                        var patients = await _patientService.GetPatientsByContactInformationAsync(contact);
                        return Ok(patients);
                    } catch (Exception e) {
                        return BadRequest(e.Message);
                    }
                }

                [Authorize]
                [HttpGet("gender/{gender}")]
                public async Task<ActionResult<IEnumerable<PatientDTO>>> GetPatientsByGender(Gender gender) {
                    try {
                        var patients = await _patientService.GetPatientsByGenderAsync(gender);
                        return Ok(patients);
                    } catch (Exception e) {
                        return BadRequest(e.Message);
                    }
                }

                [Authorize]
                [HttpGet("dob/{dateOfBirth}")]
                public async Task<ActionResult<IEnumerable<PatientDTO>>> GetPatientsByDateOfBirth(DateOnly dateOfBirth) {
                    try {
                        var patients = await _patientService.GetPatientsByDateOfBirthAsync(dateOfBirth);
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
