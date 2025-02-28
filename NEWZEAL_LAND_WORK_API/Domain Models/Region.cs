namespace NEWZEAL_LAND_WORK_API.Domain_Models
{
    public class Region
    {
        public Guid Id { get; set; }

        public required string Code { get; set; }

        public required string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}