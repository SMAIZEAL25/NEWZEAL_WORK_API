using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NEWZEAL_LAND_WORK_API.Domain_Models;
using NEWZEAL_LAND_WORK_API.DTO;
using NEWZEAL_LAND_WORK_API.Repositories;
using NEWZEAL_LAND_WORK_API.CustomActionModelState;


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


        // Create Walks
        [HttpPost("api/CreateWalks")]
        [ValidationModelState]
        public async Task<IActionResult> CreatePostAsync([FromBody] AddRequestWalksDTO addWalks)
        {
            
                var walksMapperResponse = _mapper.Map<Walk>(addWalks);
                await _repository.CreateWalksAsync(walksMapperResponse);

                return Ok(_mapper.Map<WalkDto>(walksMapperResponse));
            
        }

        // Gett ALL Walks
        [HttpGet("api/Getwalks")]
        public async Task <IActionResult> GetAllWalks([FromQuery] string? filteron, [FromQuery] string? filterQuery)
        {
            var response  = await _repository.GetAllWalksAsync(filteron, filterQuery);
            var walksMapperResponse = _mapper.Map<List<WalkDto>>(response);

            return Ok(walksMapperResponse);
        }

        // Getby Id walks 

        [HttpGet("api/Getwalks/{id:Guid}")]
        public async Task<IActionResult> GetWalksById([FromRoute] Guid id)
        {
            var responseWalksDomainModel = await _repository.GetByIdAsync(id);
            if (responseWalksDomainModel == null)
            {
               return NotFound();
            }
            var walksMapperResponse = _mapper.Map<WalkDto>(responseWalksDomainModel);
            return Ok(walksMapperResponse);
        }



        //update Walks
        [HttpPut("api/UpdateWalks/{id:Guid}")]

        public async Task<IActionResult> UpdateWalks([FromRoute] Guid id, [FromBody] UpdateWalksDTO updateWalksDTO)
        {
            if (ModelState.IsValid)
            {
                var walksMapperResponse = _mapper.Map<Walk>(updateWalksDTO);
                var walks = await _repository.UpdateRequestAsync(id, walksMapperResponse);
                if (walks == null)
                {
                    return NotFound();
                }
                var mapperResponse = _mapper.Map<WalkDto>(walksMapperResponse);

                return Ok(mapperResponse);
            }

            return BadRequest("Request not processed");
        }

        //Delete Walks

        [HttpDelete("api/DeleteWalks/{id:Guid}")]

        public async Task<IActionResult> DeleteWalks([FromRoute] Guid id)
        {
            var Deletedwalks = await _repository.DeleteAsync(id);
            if (Deletedwalks == null)
            {
                return NotFound(); 
            }
            var Mapper = _mapper.Map<WalkDto>(Deletedwalks);
            return Ok(Mapper);
        }

        
    }
}
