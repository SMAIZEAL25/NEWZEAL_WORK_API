using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NEWZEAL_LAND_WORK_API.Data;
using NEWZEAL_LAND_WORK_API.Domain_Models;
using NEWZEAL_LAND_WORK_API.DTO;
using NEWZEAL_LAND_WORK_API.MapConfig;

namespace NEWZEAL_LAND_WORK_API.Controllers
{
    [Route("https/Nzwalksresourse/")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly ILogger<RegionController> _logger;
        private readonly NZwalksDbcontext _nZwalksDbcontext;
        private readonly IMapper _mapper;

        public RegionController(ILogger<RegionController> logger, NZwalksDbcontext nZwalksDbcontext, IMapper mapper)
        {
            _logger = logger;
            _nZwalksDbcontext = nZwalksDbcontext;
            _mapper = mapper;
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
            var regionDto = _mapper.Map<RegionDTO>(regionsDomain);
            return Ok(regionDto);
        }

        [HttpPost("api/postrequest")]
        public async Task<IActionResult> Create([FromBody] CreateDTO createDTO)
        {
            var regionDomain = _mapper.Map<Region>(createDTO);

            await _nZwalksDbcontext.Regions.AddAsync(regionDomain);
            await _nZwalksDbcontext.SaveChangesAsync();

            var regionDto = _mapper.Map<RegionDTO>(regionDomain);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
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

            var regionDto = _mapper.Map<RegionDTO>(region);

            return Ok(regionDto);
        }


        [HttpPut("api/updateResource/{Id:Guid}")]
        public async Task<IActionResult> UpdateResource(Guid Id, [FromBody] UpdateResource updateDTO)
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

            var regionDto = _mapper.Map<RegionDTO>(region);

            return Ok(regionDto);
        }

    }
}
