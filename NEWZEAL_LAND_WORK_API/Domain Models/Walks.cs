namespace NEWZEAL_LAND_WORK_API.Domain_Models
{
    public class Walk
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public double LengthInkm { get; set; }

        public string? WalkImageUrl { get; set; }    

        // relation of walk model to diffculty table 
        public Guid DifficultyId { get; set; }

        public Guid RegionId { get; set; }

        // nagivation property
        public required Difficulty Difficulty { get; set; }

        public required Region Region { get; set; }
    }
}
