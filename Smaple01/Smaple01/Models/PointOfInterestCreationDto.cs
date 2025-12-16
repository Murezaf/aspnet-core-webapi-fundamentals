using System.ComponentModel.DataAnnotations;

namespace Smaple01.Models
{
    public class PointOfInterestCreationDto
    {
        [Required(ErrorMessage = "CANT BE NULL")]
        [MaxLength(50)]//default message
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(200, ErrorMessage = "LESS THAN 200")]
        public string? Description { get; set; }
    }
}
