using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NEWZEAL_LAND_WORK_API.Data;
using NEWZEAL_LAND_WORK_API.Domain_Models;
using NEWZEAL_LAND_WORK_API.DTO;

namespace NEWZEAL_LAND_WORK_API.Controllers
{
    [Route("https/Nzwalksresourse/")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly NZwalksDbcontext _nZwalksDbcontext;
        public RegionController(ILogger logger, NZwalksDbcontext nZwalksDbcontext)
        {
            _logger = logger;
            _nZwalksDbcontext = nZwalksDbcontext;
        }

        [HttpGet]
        [Route("Api/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var regions = await _nZwalksDbcontext.Regions.ToListAsync();

            // Map domain models to DTOs manual mapping 
            var regionDto = new List<RegionDTO>();

            foreach (var region in regions)
            {
                regionDto.Add(new RegionDTO()
                {
                    Id = region.Id,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl,
                    code = region.Code,
                });
            }

            return Ok(regions);
        }



        [HttpGet]
        [Route("id:Guid")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = await _nZwalksDbcontext.Regions.FindAsync(id);

            var regionsDomain = _nZwalksDbcontext.Regions.FirstOrDefault(x => x.Id == id);

            if (regionsDomain == null)
            {
                return NotFound(); 
            }
            //map dto to  domain model
            var regionDto = new RegionDTO
            {
                Id = regionsDomain.Id,
                code = regionsDomain.Code,
                Name = regionsDomain.Name,
                RegionImageUrl = regionsDomain.RegionImageUrl,
            };
             
            //return Dto
            return Ok(regionsDomain);
        }
    }
}
