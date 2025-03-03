using NEWZEAL_LAND_WORK_API.Domain_Models;
using NEWZEAL_LAND_WORK_API.DTO;

namespace NEWZEAL_LAND_WORK_API.Repositories
{
    public interface IRepositoriesNZwalks 
    {
       
        public Task<List<Region>> GetAllNZwalks();
        public Task<Region?> UpdateAsync(Guid id, UpdateRegionDTO updateResource);
    }
}
