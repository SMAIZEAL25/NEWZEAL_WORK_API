using System.ComponentModel.DataAnnotations;

namespace NEWZEAL_LAND_WORK_API.DTO
{
    public class AddRequestWalksDTO
    {


        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }
        [Required]
        [MaxLength(1000)]
        public required string Description { get; set; }
        [Required]
        [Range(0,50)]
        public double LengthInkm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        // relation of walk model to diffculty table 
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
 