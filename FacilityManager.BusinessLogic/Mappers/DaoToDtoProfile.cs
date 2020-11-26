using AutoMapper;
using FacilityManager.BusinessLogic.Models;
using FacilityManager.EntityFramework.Models;
using System.Linq;

namespace FacilityManager.BusinessLogic.Mappers
{
    public class DaoToDtoProfile : Profile
    {
        public DaoToDtoProfile()
        {
            CreateMap<User, UserDto>()
                .AfterMap((dto, userDto) => userDto.Roles = dto.Roles.Split(';').ToList());
            CreateMap<UserDto, User>()
                .ForMember(x => x.Roles, opt => opt.MapFrom(src => string.Join(";", src.Roles)));

            CreateMap<Facility, FacilityDto>();
            CreateMap<FacilityDto, Facility>();
        }
    }
}