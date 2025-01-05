using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using TodoApi.Models.Shared;
using TodoApi.Models.Specialization;
using TodoApi.Services.Specialization;
using TodoApi.DTOs.Specializations;
using Microsoft.AspNetCore.Authorization;

namespace DDDSample1.Controllers
{
    [Route("api/specializations")]
    [ApiController]
    public class SpecializationsController : ControllerBase
    {
        private readonly SpecializationService _service;

        public SpecializationsController(SpecializationService service)
        {
            _service = service;
        }

        // GET: api/specializations
        [HttpGet]
        [Authorize(Policy="AdminPolicy")]
        public async Task<ActionResult<IEnumerable<SpecializationDto>>> GetAll()
        {
            var specializations = await _service.GetAllAsync();
            return Ok(specializations);
        }

        // GET: api/specializations/{id}
        [Authorize(Policy="AdminPolicy")]
        [HttpGet("{id}")]
        public async Task<ActionResult<SpecializationDto>> GetById(string id)
        {
            var specialization = await _service.GetByIdAsync(new SpecializationId(id));

            if (specialization == null)
            {
                return NotFound();
            }

            return Ok(specialization);
        }

        // POST: api/specializations
        [Authorize(Policy="AdminPolicy")]
        [HttpPost]
        public async Task<ActionResult<SpecializationDto>> Create([FromBody] CreateSpecializationDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            var specialization = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = specialization.Id }, specialization);
        }

        // PUT: api/specializations/{id}
        [Authorize(Policy="AdminPolicy")]
        [HttpPut("{id}")]
        public async Task<ActionResult<SpecializationDto>> Update(string id, [FromBody] SpecializationDto dto)
        {
            if (id != dto.Id.ToString())
            {
                return BadRequest("ID mismatch.");
            }

            try
            {
                var updatedSpecialization = await _service.UpdateAsync(dto);

                if (updatedSpecialization == null)
                {
                    return NotFound();
                }

                return Ok(updatedSpecialization);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // DELETE: api/operationTypeSpecializations/{id}
        [Authorize(Policy="AdminPolicy")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<SpecializationDto>> HardDelete(string id)
        {
            try
            {
                var deletedSpecialization = await _service.DeleteAsync(new SpecializationId(id));

                if (deletedSpecialization == null)
                {
                    return NotFound();
                }

                return Ok(deletedSpecialization);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
[Authorize(Policy="AdminPolicy")]
[HttpGet("search")]
public async Task<IActionResult> SearchSpecializations([FromQuery] string designation, [FromQuery] string code, [FromQuery] string description)
{
    try
    {
        var searchDto = new SearchSpecializationDto
        {
            Designation = designation,
            Code = code,
            Description = description
        };

        var specializations = await _service.SearchSpecializationsAsync(searchDto);
        return Ok(specializations);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"An error occurred while searching: {ex.Message}");
    }
}
}}
