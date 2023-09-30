using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoAPI.Models
{
    [Table("Branch")]
    public class Branch
    {
        public int Id { get; set; }
        [Required]
        public string? BranchName { get; set; }
        [Required]
        public int BranchId { get; set; }
    }
}
