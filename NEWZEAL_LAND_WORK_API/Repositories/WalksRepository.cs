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

        public async Task<Walk> CreateWalksAsync (Walk walks)
        {
            var difficultyExists = await _nZwalksDbcontext.Walks.AddAsync(walks);
            var response = await _nZwalksDbcontext.Walks.AddAsync(walks);
            await _nZwalksDbcontext.SaveChangesAsync();
            return walks;

            
        }


        public async Task<List<Walk>> GetAllWalksAsync (string? filteron = null, string? fillterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var Walks = _nZwalksDbcontext.Walks.Include("Difficulty").Include("Region").AsQueryable();
            if (string.IsNullOrWhiteSpace(filteron) == false && string.IsNullOrWhiteSpace(fillterQuery) == false)
            {
                if (filteron.Equals ("Name", StringComparison.OrdinalIgnoreCase))
                {
                    Walks = Walks.Where(x => x.Name.Contains(fillterQuery));
                }
              
            }

            // Sorting 
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    Walks = isAscending ? Walks.OrderBy(x => x.Name) : Walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Lenght", StringComparison.OrdinalIgnoreCase))
                {
                    Walks = isAscending ? Walks.OrderBy(x => x.LengthInkm) : Walks.OrderByDescending(X => X.LengthInkm);

                }
            }

            // Pagination 
            var skipResult = (pageNumber - 1) * pageSize;
            //Walks = Walks.Skip(skipResult).Take(pageSize);

            return await Walks.Skip(skipResult).Take(pageSize).ToListAsync();
            //initial way of retriving records from the database
            //return await _nZwalksDbcontext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            var region = await _nZwalksDbcontext.Walks.Include("Difficulty")
                .Include("Region").FirstOrDefaultAsync(i => i.Id == id);
            return region;
        }


        

        public async Task<Walk?> UpdateRequestAsync(Guid Id, Walk walk)
        {
            var reponse = await _nZwalksDbcontext.Walks.FirstOrDefaultAsync(x => x.Id == Id);

            if (reponse == null)
            {
                return null;
            }

            reponse.Name = walk.Name;
            reponse.Description = walk.Description;
            reponse.RegionId = walk.RegionId;
            reponse.DifficultyId = walk.DifficultyId;
            reponse.WalkImageUrl = walk.WalkImageUrl;
            reponse.LengthInkm = walk.LengthInkm;

            await _nZwalksDbcontext.SaveChangesAsync();

            return reponse;


        }

        public async Task<Walk?> DeleteAsync(Guid Id)
        {
            var response = await _nZwalksDbcontext.Walks.FirstOrDefaultAsync(x => x.Id == Id);
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
