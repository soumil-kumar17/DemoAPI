using System.ComponentModel.DataAnnotations;

namespace DemoAPI.DTO;

public class CreateStudentDto
{
    [Required]
    public int StudentId { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [Required]
    public string? BranchName { get; set; }
    public string? Role { get; set; } = "Student";
}
