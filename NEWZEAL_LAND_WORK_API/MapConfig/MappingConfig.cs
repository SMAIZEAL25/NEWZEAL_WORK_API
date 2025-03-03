using AutoMapper;
using NEWZEAL_LAND_WORK_API.Domain_Models;
using NEWZEAL_LAND_WORK_API.DTO;

namespace NEWZEAL_LAND_WORK_API.MapConfig
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<Region, CreateAddRequestRegionDTODTO>().ReverseMap();
            CreateMap<Region, UpdateRegionDTO> ().ReverseMap();
            CreateMap<Walk, AddRequestWalksDTO>().ReverseMap();
            CreateMap<WalkDto, Walk>().ReverseMap();
            CreateMap<Difficulty, DifficultyDTO>().ReverseMap();
            CreateMap<UpdateWalksDTO, Walk>().ReverseMap();


        }
    }
}
