using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs;
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

        [HttpPost("signup")]
        public async Task<ActionResult<string>> SignUpPatient()
        {            
            var sessionId = Guid.NewGuid().ToString();
            await _loginService.SignUpPatient(sessionId);

            return Ok(new { message = "Sign up sucessfull", sessionId = sessionId } );
        }

        //[Authorize(Policy = "PatientPolicy")]
        [HttpPost("register/patient")]
        public async Task<ActionResult<PatientDTO>> RegisterPatient([FromBody] RegisterPatientDTO dto) {
                Console.WriteLine("Received RegisterPatient request: " + dto.Email);
            try {
                var patient = await _patientService.RegisterPatient(dto);

                HttpContext.Session.SetString("patient_email", dto.Email);

                return Ok(new { message = "Patient Registered!!", patient });
            } catch (Exception e) {
                return BadRequest(e.Message);
            }        
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("register/admin")]
        public async Task<ActionResult<PatientDTO>> RegisterPatientAdmin([FromBody] RegisterPatientDTO dto) {
            try {
                var patient = await _patientService.RegisterPatient(dto);

                return Ok();
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


                
        [Authorize(Policy = "AdminPolicy")]
                [HttpGet("search")]
                public async Task<IActionResult> SearchPatients(
                    [FromQuery] string? contact = null,
                    [FromQuery] Gender? gender = null,
                    [FromQuery] DateOnly? dateOfBirth = null)
                {
                    try
                    {
                        var results = await _patientService.SearchPatients(contact, gender, dateOfBirth);
                        return Ok(results);
                    }
                    catch (Exception e)
                    {
                        return StatusCode(500, $"Internal server error: {e.Message}");
                    }
                }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatientByID(Guid id) {
            try {
                var result = await _patientService.DeletePatientByIDAsync(id);
                return Ok();
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }
    }
}
