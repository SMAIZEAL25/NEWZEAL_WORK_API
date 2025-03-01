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
        [Route("Api/GetById/{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionsDomain = await _nZwalksDbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionsDomain == null)
            {
                return NotFound();
            }

            // map domain model to dto
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


        [HttpPost("api/postrequest")]
        public IActionResult Create([FromBody] CreateDTO createDTO)
        {
            // Map Dto to Domain model
            var regionDomain = new Region
            {
                Code = createDTO.Code,
                Name = createDTO.Name,
                RegionImageUrl = createDTO.RegionImageUrl,
            };

            _nZwalksDbcontext.Regions.Add(regionDomain);
            _nZwalksDbcontext.SaveChanges();

            // Map Domain model back into Dto
            var regiondto = new RegionDTO
            {
                Id = regionDomain.Id,
                code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };

            return CreatedAtAction(nameof(GetById),new { id = regiondto.Id}, regiondto);

        }

        [HttpDelete("api/delete/Id:Guid")]
        public async Task<IActionResult> DeleteResource(Guid Id)
        {
            var region = await _nZwalksDbcontext.Regions.FindAsync(Id);
            if (region == null)
            {
                return NotFound();
            }
            _nZwalksDbcontext.Regions.Remove(region);
            await _nZwalksDbcontext.SaveChangesAsync();

            return Ok();
        }

       

        [HttpPut("api/updateResource/{Id:Guid}")]
        public async Task<IActionResult> UpdateResource( Guid Id, [FromBody] UpdateResource updateDTO)
        {
            var region = await _nZwalksDbcontext.Regions.FirstOrDefaultAsync(i => i.Id == Id);
            if (region == null)
            {
                return NotFound();
            }

            // Update the region properties
            region.Name = updateDTO.Name;
            region.Code = updateDTO.code;
            region.RegionImageUrl = updateDTO.RegionImageUrl;

            await _nZwalksDbcontext.SaveChangesAsync();

            return Ok(region);
        }


    }
}
