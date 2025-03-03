using System.ComponentModel.DataAnnotations;

namespace NEWZEAL_LAND_WORK_API.DTO
{
    public class CreateAddRequestRegionDTODTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be a minimium of 3 Characters")]
        [MaxLength(3, ErrorMessage = "Code has to be a minimium of 3 Characters")]
        public required string Code { get; set; }

        [Required]
        [MaxLength (100, ErrorMessage ="Name has to be a Maximium of 100 characters")]
        public required string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
