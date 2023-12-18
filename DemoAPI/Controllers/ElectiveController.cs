using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using DemoAPI.Contracts;
using DemoAPI.DTO;
using DemoAPI.Models;

namespace DemoAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ElectiveController : ControllerBase
{
    private readonly IRepositoryWrapper _wrapper;
    private readonly IMapper _mapper;
    private readonly ILogger<ElectiveController> _logger;
    public ElectiveController(IRepositoryWrapper wrapper, IMapper mapper, ILogger<ElectiveController> logger)
    {
        _wrapper = wrapper;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("elective/{electiveId}")]
    public async Task<IActionResult> GetElectiveByElectiveId(int electiveId)
    {
        try
        {
            _logger.LogInformation("Calling GetElectiveByElectiveId method.");
            if (electiveId is 0)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var elective = await _wrapper.Elective.GetElective(electiveId);
            var result = _mapper.Map<CreateElectiveDto>(elective);
            if (elective is null)
            {
                _logger.LogInformation("Elective not found.");
                return NotFound();
            }
            _logger.LogInformation($"Elective with ID {electiveId} successfully found.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError("Something went wrong inside GetElectiveByElectiveId action : {0}",ex);
            return StatusCode(500, "Internal server error");
        }  
    }

    [HttpGet("elective")]
    public async Task<IActionResult> GetAllElectives()
    {
        try
        {
            _logger.LogInformation("Calling GetAllElectives method.");
            IEnumerable<Elective> elective_list = await _wrapper.Elective.GetAllElectives();
            var result = _mapper.Map<IEnumerable<CreateElectiveDto>>(elective_list);
            _logger.LogInformation("Successfully retrieved information of all electives.");
            return Ok(result);
        }
        catch(Exception ex)
        {
            _logger.LogError($"Something went wrong inside GetAllElectives action : {ex}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("elective")]
    public async Task<IActionResult> CreateElective([FromBody] CreateElectiveDto electiveCreate)
    {
        try
        {
            _logger.LogInformation("Calling CreateElective method.");
            if (electiveCreate is null)
            {
                _logger.LogError("No data in request body.");
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var elective = await _wrapper.Elective.GetElective(electiveCreate.ElectiveId);
            if (elective is not null)
            {
                ModelState.AddModelError("", "Elective already exists.");
                return StatusCode(422, ModelState);
            }
            var elective_map = _mapper.Map<Elective>(electiveCreate);
            bool created = await _wrapper.Elective.CreateElective(elective_map);
            if (created)
            {
                return Ok("Successfully created elective.");
            }
            return StatusCode(500, "Unable to create elective.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside CreateElective action : {ex}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("elective")]
    public async Task<IActionResult> UpdateElective([FromBody] CreateElectiveDto electiveUpdate)
    {
        try
        {
            _logger.LogInformation("Calling UpdateElective method.");
            if (electiveUpdate is null)
            {
                _logger.LogInformation("No data in request body.");
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var elective = await _wrapper.Elective.GetElective(electiveUpdate.ElectiveId);
            if (elective is not null)
            {
                var elective_map = _mapper.Map<Elective>(electiveUpdate);
                bool updated = await _wrapper.Elective.UpdateElective(elective_map);
                if (updated)
                {
                    return Ok("Successfully updated elective.");
                }
            }
            return NotFound("Elective does not exist.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside UpdateElective action : {ex}");
            return StatusCode(500, "Internal server error");
        }   
    }

    [HttpDelete("elective/{electiveId:int}")]
    public async Task<IActionResult> DeleteElective(int electiveId)
    {
        try
        {
            _logger.LogInformation("Calling DeleteElective method.");
            if (electiveId is 0)
            {
                _logger.LogInformation($"Elective with ID {electiveId} does not exist.");
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var elective = await _wrapper.Elective.GetElective(electiveId);
            if (elective is not null)
            {
                bool deleted = await _wrapper.Elective.DeleteElective(elective);
                if (deleted)
                    return Ok("Successfully deleted.");
            }
            return NotFound("Elective does not exist.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside DeleteElective action : {ex}");
            return StatusCode(500, "Internal server error");
        }
    }
}   