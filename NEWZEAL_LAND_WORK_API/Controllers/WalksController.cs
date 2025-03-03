using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NEWZEAL_LAND_WORK_API.Domain_Models;
using NEWZEAL_LAND_WORK_API.DTO;
using NEWZEAL_LAND_WORK_API.Repositories;


namespace NEWZEAL_LAND_WORK_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalksRepository _repository;
        private readonly IMapper _mapper;
        public WalksController(IWalksRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpPost("api/CreateWalks")]
        public async Task<IActionResult> create([FromBody] AddRequestWalksDTO addWalks)
        {
            var walksMapperResponse = _mapper.Map<Walk>(addWalks);
            await _repository.AddWalks (walksMapperResponse);

            return Ok(_mapper.Map<WalkDto>(addWalks));
        }

        [HttpGet("api/Getwalks")]
        public async Task <IActionResult> GetAllWalks()
        {
            var response  = await _repository.GetWalksAsync();
            var walksMapperResponse = _mapper.Map<List<WalkDto>>(response);

            return Ok(walksMapperResponse);
        }

        [HttpGet("api/Getwalks/{id:Guid}")]
        public async Task<IActionResult> GetWalksById([FromRoute] Guid id)
        {
            var response = await _repository.GetByIdAsync(id);
            if (response == null)
            {
               return NotFound();
            }
            var walksMapperResponse = _mapper.Map<List<WalkDto>>(response);
            return Ok(walksMapperResponse);
        }

        [HttpPut("api/UpdateWalks/{id:Guid}")]

        public async Task<IActionResult> UpdateWalks([FromRoute] Guid id, [FromBody] UpdateResource updateRequestWalksDTO)
        {
            var walks = await _repository.GetByIdAsync(id);
            if (walks == null)
            {
                return NotFound();
            }
            var walksMapperResponse = _mapper.Map<Walk>(updateRequestWalksDTO);
            await _repository.AddWalks(walksMapperResponse);
            return Ok(_mapper.Map<WalkDto>(updateRequestWalksDTO));
        }


        [HttpDelete("api/DeleteWalks/{id:Guid}")]

        public async Task<IActionResult> DeleteWalks([FromRoute] Guid id)
        {
            var walks = await _repository.GetByIdAsync(id);
            if (walks == null)
            {
                return NotFound();
            }
            await _repository.DeleteAsync(id);
            return NoContent();
        }

        
    }
}
