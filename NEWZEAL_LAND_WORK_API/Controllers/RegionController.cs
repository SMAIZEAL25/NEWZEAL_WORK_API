using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using NEWZEAL_LAND_WORK_API.Data;
using NEWZEAL_LAND_WORK_API.Domain_Models;
using NEWZEAL_LAND_WORK_API.DTO;
using NEWZEAL_LAND_WORK_API.Repositories;


namespace NEWZEAL_LAND_WORK_API.Controllers
{
    [Route("https/Nzwalksresourse/")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly ILogger<RegionController> _logger;
        private readonly NZwalksDbcontext _nZwalksDbcontext;
        private readonly IMapper _mapper;
        private readonly IRepositoriesNZwalks _repositoriesNZwalks1;

        public RegionController(ILogger<RegionController> logger, IMapper mapper, IRepositoriesNZwalks repositoriesNZwalks)
        {
            _logger = logger;
            _mapper = mapper;
            _repositoriesNZwalks1 = repositoriesNZwalks;
        }

        [HttpGet]
        [Route("Api/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var regions = await _repositoriesNZwalks1.GetAllNZwalks();

            var regionDtoMapper = _mapper.Map<List<RegionDTO>>(regions);

            return Ok(regionDtoMapper);
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
        public async Task<IActionResult> Create([FromBody] CreateAddRequestRegionDTODTO createDTO)
        {
            if (ModelState.IsValid)
            {
                var regionDomain = _mapper.Map<Region>(createDTO);

                await _nZwalksDbcontext.Regions.AddAsync(regionDomain);
                await _nZwalksDbcontext.SaveChangesAsync();

                var regionDto = _mapper.Map<RegionDTO>(regionDomain);

                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
            }

            return BadRequest($"Request was not processed {ModelState}");
        }


        [HttpPut("api/updateResource/{Id:Guid}")]
        public async Task<IActionResult> UpdateResource([FromRoute] Guid Id, [FromBody] UpdateRegionDTO updateDTO)
        {
            if (ModelState.IsValid)
            {
                var region = await _repositoriesNZwalks1.UpdateAsync(Id, updateDTO);

                if (region == null)
                {
                    return NotFound();
                }

                var regionDto = _mapper.Map<RegionDTO>(region);

                return Ok(regionDto);
            } else
            {
                return BadRequest(ModelState);
            }
            
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

    }
}
