using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NEWZEAL_LAND_WORK_API.Data;
using NEWZEAL_LAND_WORK_API.Domain_Models;
using NEWZEAL_LAND_WORK_API.DTO;
using System.Diagnostics.Contracts;

namespace NEWZEAL_LAND_WORK_API.Repositories
{
    public class RepositoriesNZwalksass : IRepositoriesNZwalks
    {
        private readonly NZwalksDbcontext _nZwalksDbcontext;
        public RepositoriesNZwalksass(NZwalksDbcontext nZwalksDbcontext)
        {
           this._nZwalksDbcontext = nZwalksDbcontext;
        }

      

        public async Task<List<Region>> GetAllNZwalks()
        {
            return await _nZwalksDbcontext.Regions.ToListAsync();
        }


        public async Task<Region?> UpdateAsync(Guid id, UpdateResource updatedRegion)
        {
            var region = await _nZwalksDbcontext.Regions.FirstOrDefaultAsync(i => i.Id == id);
            if (region == null)
            {
                return null;
            }

            region.Code = updatedRegion.code;
            region.Name = updatedRegion.Name;
            region.RegionImageUrl = updatedRegion.RegionImageUrl;

            await _nZwalksDbcontext.SaveChangesAsync();
            return region;
        }

     


    }
}
