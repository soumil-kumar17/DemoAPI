using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DemoAPI.Contracts;
using DemoAPI.DTO;
using DemoAPI.Models;

namespace DemoAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;
        private readonly ILogger<BranchController> _logger;
        public BranchController(IRepositoryWrapper wrapper, IMapper mapper, ILogger<BranchController> logger)
        {
             _wrapper = wrapper;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("branch/{branchId:int}")]
        public async Task<IActionResult> GetBranchByBranchId(int branchId)
        {
            try
            {
                _logger.LogInformation("Calling method GetBranchByBranchId.");
                if (branchId is 0)
                {
                    return BadRequest(ModelState);
                }
                Branch? branch = await _wrapper.Branch.GetBranch(branchId);
                _logger.LogInformation("Mapping branch data to CreateBranchDto.");
                CreateBranchDto branchResult = _mapper.Map<CreateBranchDto>(branch);
                if (branch is null)
                {
                    _logger.LogError($"The branch having branchID {branchId} does not exist.");
                    return NotFound();
                }
                _logger.LogInformation($"Branch having branchId {branchId} is found.");
                return Ok(branchResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetBranchByBranchId action : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("branch")]
        public async Task<IActionResult> GetAllBranches()
        {
            try
            {
                _logger.LogInformation("Calling the method GetAllBranches.");
                IEnumerable<Branch> branch_list = await _wrapper.Branch.GetBranches();
                if (!branch_list.Any())
                {
                    _logger.LogError($"GetAllBranches - no Branch data found");
                    return NotFound();
                }
                _logger.LogInformation("Mapping the data of all branches to an IEnumerable of CreateBranchDto.");
                var branchResult = _mapper.Map<IEnumerable<CreateBranchDto>>(branch_list);
                _logger.LogInformation("Found all branches.");
                return Ok(branchResult);
            }
            catch(Exception ex)
            {
                _logger.LogError($"GetAllBranches - Something went wrong inside GetAllBranches action: {ex}");
                return StatusCode(500, "GetAllBranches - Internal server error");
            }
        }

        [HttpPost("branch")]
        public async Task<IActionResult> CreateBranch([FromBody] CreateBranchDto branchCreate)
        {
            try
            {
                _logger.LogInformation("Calling create branch method.");
                if (branchCreate is null)
                {
                    _logger.LogInformation("Branch has no information in the body");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _logger.LogInformation("Creating branch.");
                var branch = await _wrapper.Branch.GetBranch(branchCreate.BranchId);
                if (branch is not null)
                {
                    ModelState.AddModelError("", "Branch already exists.");
                    return StatusCode(422, ModelState);
                }
                _logger.LogInformation("Mapping branch.");
                var branch_map = _mapper.Map<Branch>(branchCreate);
                bool created = await _wrapper.Branch.CreateBranch(branch_map);
                if (created)
                {
                    return Ok("Successfully created branch.");
                }
                return StatusCode(500, "Unable to create branch.");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateBranch action: {ex}");
                return StatusCode(500, "Internal server error");
            }   
        }

        [HttpPut("branch")]
        public async Task<IActionResult> UpdateBranch([FromBody] CreateBranchDto branchUpdate)
        {
            try
            {
                _logger.LogInformation("Calling UpdateBranch method.");
                if (branchUpdate is null)
                {
                    _logger.LogInformation("Unable to update branch as no data given in request body.");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return UnprocessableEntity(ModelState);
                }
                var branch = await _wrapper.Branch.GetBranch(branchUpdate.BranchId);
                if (branch is not null)
                {
                    _logger.LogInformation("Updating branch.");
                    var branch_map = _mapper.Map<Branch>(branchUpdate);
                    bool updated = await _wrapper.Branch.UpdateBranch(branch_map);
                    if (updated)
                    {
                        return Ok($"Successfully updated branch with id : {branchUpdate.BranchId}.");
                    }
                }
                return NotFound("Branch does not exist.");
            }
            catch(Exception e)
            {
                _logger.LogError($"Something went wrong inside UpdateBranch action: {e}");
                return StatusCode(500, "UpdateBranch - Internal server error");
            }
        }

        [HttpDelete("branch/{branchId:int}")]
        public async Task<IActionResult> DeleteBranch(int branchId)
        {
            _logger.LogInformation("Calling DeleteBranch method.");
            if(branchId is 0)
            {
                return BadRequest(ModelState);
            }
            _logger.LogInformation("Getting branch to update by it's ID.");
            var branch = await _wrapper.Branch.GetBranch(branchId);
            if (branch is not null)
            {
                _logger.LogInformation($"Deleting branch with ID {branchId}.");
                bool deleted = await _wrapper.Branch.DeleteBranch(branchId);
                if (deleted)
                {
                    return Ok("Successfully deleted.");
                }
            }
            return NotFound($"Branch with id {branchId} does not exist.");
        }
    }
}
