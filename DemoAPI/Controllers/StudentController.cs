using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using DemoAPI.Contracts;
using DemoAPI.DTO;
using DemoAPI.Models;

namespace DemoAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentController> _logger;
        public StudentController(IRepositoryWrapper wrapper, IMapper mapper, ILogger<StudentController> logger)
        {
            _mapper = mapper;
            _wrapper = wrapper;
            _logger = logger;
        }

        [HttpGet("student/{studentId:int}")]
        public async Task<IActionResult> GetStudentByStudentId(int studentId)
        {
            try
            {
                _logger.LogInformation("Calling GetStudentByStudentId.");
                if (studentId is 0)
                {
                    return BadRequest(ModelState);
                }
                _logger.LogInformation("Calling StudentRepository method.");
                var temp = await _wrapper.Student.GetStudentByStudentId(studentId);
                var studentResult = _mapper.Map<IEnumerable<CreateStudentDto>>(temp);
                if (temp is null)
                {
                    _logger.LogInformation($"No data found for StudentId {studentId}.");
                    return NotFound();
                }
                _logger.LogInformation($"Student found for the Id {studentId}.");
                return Ok(studentResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetStudentByStudentId action : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("student")]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                _logger.LogInformation("Calling GetAllStudents method.");
                IEnumerable<Student> list_std = await _wrapper.Student.GetStudents();
                var studentResult = _mapper.Map<IEnumerable<CreateStudentDto>>(list_std);
                return Ok(studentResult);
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong inside GetAllStudents action : {e}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("student")]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentDto studentCreate)
        {
            try
            {
                _logger.LogInformation("Calling CreateStudent method.");
                if (studentCreate is null)
                {
                    _logger.LogError("No information in request body.");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                IEnumerable<Student> student = await _wrapper.Student.GetStudentByStudentId(studentCreate.StudentId);
                if (student.Count() > 0)
                {
                    ModelState.AddModelError("", "Student already exists.");
                    return StatusCode(422, ModelState);
                }
                _logger.LogInformation("Mapping student.");
                var student_map = _mapper.Map<Student>(studentCreate);
                bool created = await _wrapper.Student.CreateStudent(student_map);

                if (created)
                    return Ok("Successfully created.");
                else return StatusCode(500, "Some error occurred while creating student.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateStudent action : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("student")]
        public async Task<IActionResult> UpdateStudent([FromBody] CreateStudentDto studentUpdate)
        {
            try
            {
                _logger.LogInformation("Calling UpdateStudent method.");
                if (studentUpdate is null)
                {
                    _logger.LogInformation("No information in request body.");
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                IEnumerable<Student> student = await _wrapper.Student.GetStudentByStudentId(studentUpdate.StudentId);
                if (student.Count() > 0)
                {
                    _logger.LogInformation("Updating student details.");
                    var student_map = _mapper.Map<Student>(studentUpdate);
                    bool updated = await _wrapper.Student.UpdateStudent(student_map);
                    if (updated)
                        return Ok("Successfully updated.");
                }
                return NotFound("Student does not exist.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateStudent action : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("student/{studentId:int}")]
        public async Task<IActionResult> DeleteStudent(int studentId)
        {
            try
            {
                _logger.LogInformation("Calling DeleteStudent method.");
                if (studentId is 0)
                {
                    return BadRequest(ModelState);
                }
                IEnumerable<Student> student = await _wrapper.Student.GetStudentByStudentId(studentId);
                if (student.Count() > 0)
                {
                    _logger.LogInformation("Deleting student details.");
                    bool deleted = await _wrapper.Student.DeleteStudent(student.First());
                    if (deleted)
                        return Ok("Deleted successfully");
                }
                return NotFound("Student does not exist.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteStudent action : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("students")]
        public async Task<IActionResult> CreateStudents([FromBody] IEnumerable<CreateStudentDto> students)
        {
            try
            {
                _logger.LogInformation("Calling CreateStudents method.");
                bool foundStudent = false;
                if (students is null)
                {
                    _logger.LogInformation("No data in request body.");
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                foreach (var j in students)
                {
                    IEnumerable<Student> student = await _wrapper.Student.GetStudentByStudentId(j.StudentId);
                    if (student.Count() > 0)
                    {
                        foundStudent = true;
                        return StatusCode(500, $"Student with First name {j.FirstName} and Last name {j.LastName} with StudentId {j.StudentId} already exists.");
                    }
                }
                if (foundStudent is false)
                {
                    _logger.LogInformation("Mapping the details of the students.");
                    var student_map = _mapper.Map<List<Student>>(students);
                    bool created = await _wrapper.Student.CreateStudents(student_map);
                    if (created)
                        return Ok("Successfully created students.");
                }
                return StatusCode(500, "Unable to create the students in the request body.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateStudents action : {ex}");
                return StatusCode(500, "Internal server error");
            }    
        }
    }
}
