using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NEWZEAL_LAND_WORK_API.Data;
using NEWZEAL_LAND_WORK_API.DTO;

namespace NEWZEAL_LAND_WORK_API.Controllers
{
    [Route("https/Nzwalksresourse/")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly ILogger<RegionController> _logger;
        private readonly NZwalksDbcontext _nZwalksDbcontext;

        public RegionController(ILogger<RegionController> logger, NZwalksDbcontext nZwalksDbcontext)
        {
            _logger = logger;
            _nZwalksDbcontext = nZwalksDbcontext;
        }

        [HttpGet]
        [Route("Api/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var regions = await _nZwalksDbcontext.Regions.ToListAsync();
            return Ok(regions);
        }

        [HttpGet]
        [Route("id:Guid")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionsDomain = await _nZwalksDbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionsDomain == null)
            {
                return NotFound();
            }

            // map dto to domain model
            var regionDto = new RegionDTO
            {
                Id = regionsDomain.Id,
                code = regionsDomain.Code,
                Name = regionsDomain.Name,
                RegionImageUrl = regionsDomain.RegionImageUrl,
            };

            // return Dto
            return Ok(regionDto);
        }
    }
}
