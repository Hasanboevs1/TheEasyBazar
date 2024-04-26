using AutoMapper;
using TheEasyBazar.Domain.Entites;
using TheEasyBazar.Service.DTOs.Users;

namespace TheEasyBazar.Service.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserForResultDto>().ReverseMap();
        CreateMap<User, UserForCreationDto>().ReverseMap();
        CreateMap<User, UserForUpdateDto>().ReverseMap();
    }
}
