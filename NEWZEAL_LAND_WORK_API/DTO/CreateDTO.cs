namespace NEWZEAL_LAND_WORK_API.DTO
{
    public class CreateDTO
    {
        public required string Code { get; set; }

        public required string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
