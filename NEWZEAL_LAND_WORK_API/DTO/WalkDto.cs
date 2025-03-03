namespace NEWZEAL_LAND_WORK_API.DTO
{
    public class WalkDto
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public double LengthInkm { get; set; }

        public string? WalkImageUrl { get; set; }

        // relation of walk model to diffculty table 
        public Guid DifficultyId { get; set; }

        public Guid RegionId { get; set; }

        public RegionDTO? Region { get; set; }

        public DifficultyDTO? Difficulty { get; set; }

    }
}
