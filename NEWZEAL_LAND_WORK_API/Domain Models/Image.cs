using System.ComponentModel.DataAnnotations.Schema;

namespace NEWZEAL_LAND_WORK_API.Domain_Models
{
    public class Image
    {
        public Guid Id { get; set; }

        [NotMapped]
        public IFormFile file { get; set; }

        public string FileName { get; set; }

        public string? FileDescription { get; set; }

        public long FileSizeInBytes { get; set; }

        public string FilePath { get; set; }
    }
}
