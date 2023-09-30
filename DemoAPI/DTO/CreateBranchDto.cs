using System.ComponentModel.DataAnnotations;

namespace DemoAPI.DTO
{
    public class CreateBranchDto
    {
        [Required(ErrorMessage = "Branch Id is required.")]
        public int BranchId { get; set; }

        [Required(ErrorMessage = "Name of branch is required.")]
        public string? BranchName { get; set; }
    }
}
