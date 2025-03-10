using NEWZEAL_LAND_WORK_API.Data;
using NEWZEAL_LAND_WORK_API.Domain_Models;

namespace NEWZEAL_LAND_WORK_API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZwalksDbcontext nZwalksDbcontext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, 
            IHttpContextAccessor httpContextAccessor, 
            NZwalksDbcontext nZwalksDbcontext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.nZwalksDbcontext = nZwalksDbcontext;
        }
        public async Task<Image> UploadImage(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "uploadImagesFloder",
                $"{image.FileName}{image.FileExtension}");


            //upload image to local path 
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.file.CopyToAsync(stream);

            // https//localhost:1233/image/emage.jpg

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}" +
                $"://{httpContextAccessor.HttpContext.Request.Host}" +
                $"{httpContextAccessor.HttpContext.Request.PathBase}" +
                $"/UploadImagesFloder/{image.FileName}{image.FileExtension}";

            // feeding the file path property in the filepath

            image.FilePath = urlFilePath;

            // Add image to the image table 
            await nZwalksDbcontext.images.AddAsync(image);
            await nZwalksDbcontext.SaveChangesAsync();

            return image;


        }
    }
}
