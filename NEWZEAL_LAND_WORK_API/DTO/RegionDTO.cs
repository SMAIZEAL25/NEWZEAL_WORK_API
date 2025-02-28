namespace NEWZEAL_LAND_WORK_API.DTO
{
    public class RegionDTO
    {
        public Guid Id { get; set; }

        public required string code { get; set; }

        public required string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
