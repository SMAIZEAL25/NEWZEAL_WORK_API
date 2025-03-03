using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NEWZEAL_LAND_WORK_API.Data;
using NEWZEAL_LAND_WORK_API.Domain_Models;
using NEWZEAL_LAND_WORK_API.DTO;


namespace NEWZEAL_LAND_WORK_API.Repositories
{
    public class WalksRepository : IWalksRepository
    {
        private readonly NZwalksDbcontext _nZwalksDbcontext;
        public WalksRepository(NZwalksDbcontext nZwalksDbcontext)
        {
            this._nZwalksDbcontext = nZwalksDbcontext;
        }

        public async Task<Walk> AddWalks(Walk walks)
        {
            var response = await _nZwalksDbcontext.Walks.AddAsync(walks);
            await _nZwalksDbcontext.SaveChangesAsync();
            return response.Entity;
        }

        public async Task<List<Walk>> GetWalksAsync()
        {
            return await _nZwalksDbcontext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            var region = await _nZwalksDbcontext.Walks.Include("Difficulty")
                .Include("Region").FirstOrDefaultAsync(i => i.Id == id);
            return region;
        }

        public async Task<Walk?> DeleteAsync(Guid Id)
        {
            var response = await _nZwalksDbcontext.Walks.FindAsync(Id);
            if (response == null)
            {
                return null;
            }

            _nZwalksDbcontext.Walks.Remove(response);
            await _nZwalksDbcontext.SaveChangesAsync();
            return response;
        }

    }
}
