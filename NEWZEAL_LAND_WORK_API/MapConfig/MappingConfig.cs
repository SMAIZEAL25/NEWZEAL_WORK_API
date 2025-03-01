using AutoMapper;
using NEWZEAL_LAND_WORK_API.Domain_Models;
using NEWZEAL_LAND_WORK_API.DTO;
using System.Runtime;

namespace NEWZEAL_LAND_WORK_API.MapConfig
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<Region, CreateDTO>().ReverseMap();
            CreateMap<Region, UpdateResource>().ReverseMap();

        }
    }
}
