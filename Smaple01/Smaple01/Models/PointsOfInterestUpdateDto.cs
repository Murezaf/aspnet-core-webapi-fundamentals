using System.ComponentModel.DataAnnotations;

namespace Smaple01.Models
{
    public class PointsOfInterestUpdateDto
    {
        [Required(ErrorMessage = "CANT BE NULL")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200, ErrorMessage = "LESS THAN 200")]
        public string? Description { get; set; }
    }
}