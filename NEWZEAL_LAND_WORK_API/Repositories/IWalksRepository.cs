using Microsoft.AspNetCore.Mvc;
using NEWZEAL_LAND_WORK_API.Domain_Models;
using NEWZEAL_LAND_WORK_API.DTO;

namespace NEWZEAL_LAND_WORK_API.Repositories
{
    public interface IWalksRepository
    {
        public Task<Walk> CreateWalksAsync(Walk walks);
        public Task<List<Walk>> GetAllWalksAsync(string? filteron = null, string? fillterQuery = null);
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk?> UpdateRequestAsync(Guid Id, Walk walk);
        Task<Walk?> DeleteAsync(Guid Id);
    }
}
