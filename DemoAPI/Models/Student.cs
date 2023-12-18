using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoAPI.Models;

[Table("Student")]
public class Student
{
    public int Id { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [Required]
    public int StudentId { get; set; }
    [Required]
    public string? BranchName { get; set;}
    [Required]
    public string? Role { get; set; }
}
