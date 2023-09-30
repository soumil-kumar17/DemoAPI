using System.ComponentModel.DataAnnotations;

namespace DemoAPI.DTO
{
    public class CreateElectiveDto
    {
        [Required]
        public int ElectiveId { get; set; }
        [Required]
        public string ElectiveName { get; set; }
    }
}
