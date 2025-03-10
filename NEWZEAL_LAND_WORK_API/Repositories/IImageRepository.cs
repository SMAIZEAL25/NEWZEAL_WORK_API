using NEWZEAL_LAND_WORK_API.Domain_Models;

namespace NEWZEAL_LAND_WORK_API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> UploadImage(Image image);
    }
}
