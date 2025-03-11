using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NEWZEAL_LAND_WORK_API.Domain_Models;
using NEWZEAL_LAND_WORK_API.DTO;
using NEWZEAL_LAND_WORK_API.Repositories;
using NEWZEAL_LAND_WORK_API.CustomActionModelState;
using Microsoft.AspNetCore.Authorization;


namespace NEWZEAL_LAND_WORK_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Writer, Reader")]
    public class WalksController : ControllerBase
    {
        private readonly IWalksRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<WalksController> _logger;

        public WalksController(IWalksRepository repository, IMapper mapper, ILogger<WalksController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }


        // Create Walks
        [HttpPost("api/CreateWalks")]
        [ValidationModelState]
        [Authorize(Roles = "Writer, Reader")]
        public async Task<IActionResult> CreatePostAsync([FromBody] AddRequestWalksDTO addWalks)
        {
           
                var walksMapperResponse = _mapper.Map<Walk>(addWalks);
                await _repository.CreateWalksAsync(walksMapperResponse);

                return Ok(_mapper.Map<WalkDto>(walksMapperResponse));   
            
        }

        // Gett ALL Walks
        // GET:/api/WWalks?filteron=Name$filterQury=Track&sortby=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet("api/Getwalks")]
        public async Task <IActionResult> GetAllWalks([FromQuery] string? filteron, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, 
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
                var response = await _repository.GetAllWalksAsync(filteron, filterQuery, sortBy, 
                    isAscending ?? true, pageNumber, pageSize);

            // Create a new Exception for the case when the response is empty
            //throw new Exception("No walks found.");
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
            else
            {
                return BadRequest(ModelState);
            }
        }
           

        //Delete Walks

        [HttpDelete("api/DeleteWalks/{id:Guid}")]
        [Authorize(Roles = "Writer")] 
        public async Task<IActionResult> DeleteWalks([FromRoute] Guid id)
        {
            _logger.LogInformation($"DeleWalks method with {id} was invoked");
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
