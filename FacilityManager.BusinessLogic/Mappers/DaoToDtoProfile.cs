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
            CreateMap<Account, AccountDto>()
                .AfterMap((dto, accountDto) => accountDto.Roles = dto.Roles.Split(';').ToList());
            CreateMap<AccountDto, Account>()
                .ForMember(x => x.Roles, opt => opt.MapFrom(src => string.Join(";", src.Roles)));

            CreateMap<Facility, FacilityDto>();
            CreateMap<FacilityDto, Facility>();
        }
    }
}