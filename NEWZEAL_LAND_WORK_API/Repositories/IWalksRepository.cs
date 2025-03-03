using NEWZEAL_LAND_WORK_API.Domain_Models;
using NEWZEAL_LAND_WORK_API.DTO;

namespace NEWZEAL_LAND_WORK_API.Repositories
{
    public interface IWalksRepository
    {
        public Task<Walk> AddWalks(Walk walks);
        Task<Walk?> DeleteAsync(Guid Id);
        Task<Walk?> GetByIdAsync(Guid id);
        public Task<List<Walk>> GetWalksAsync();



    }
}
