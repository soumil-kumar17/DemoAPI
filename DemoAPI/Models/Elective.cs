using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoAPI.Models;

[Table("Elective")]
public class Elective
{
    public int Id { get; set; }
    [Required]
    public string? ElectiveName { get; set; }
    [Required]
    public int ElectiveId { get; set; }
}
