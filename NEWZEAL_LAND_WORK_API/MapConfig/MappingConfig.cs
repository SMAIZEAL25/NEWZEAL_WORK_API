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

            CreateMap<AddRequestRegionDTO, Region>().ReverseMap();

            CreateMap<UpdateRegionDTO, Region> ().ReverseMap();

            CreateMap<AddRequestWalksDTO, Walk>().ReverseMap();

            CreateMap<Walk, WalkDto>().ReverseMap(); 

            CreateMap<Difficulty, DifficultyDTO>().ReverseMap();

            CreateMap<UpdateWalksDTO, Walk>().ReverseMap();


        }
    }
}
