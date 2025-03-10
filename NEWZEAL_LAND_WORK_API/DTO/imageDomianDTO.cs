using System.ComponentModel.DataAnnotations;

namespace NEWZEAL_LAND_WORK_API.DTO
{
    public class ImageDomianDTO
    {
        public IFormFile File { get; set; }

        [Required]
        public string FileName { get; set; }

        public string Description { get; set; }
    }
}
