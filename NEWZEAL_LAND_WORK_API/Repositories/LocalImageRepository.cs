using NEWZEAL_LAND_WORK_API.Domain_Models;

namespace NEWZEAL_LAND_WORK_API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<Image> UploadImage(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",
                image.FileName, image.FilePath);


            //upload image to local path 
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.file.CopyToAsync(stream);

            // https//localhost:1233/image/emage.jpg

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}" +
                $"://{httpContextAccessor.HttpContext.Request.Host}" +
                $"{httpContextAccessor.HttpContext.Request.PathBase}" +
                $"/UploadImagesFloder/{image.FileName}{image.FilePath}";
  
            image.FilePath = 
        }
    }
}
