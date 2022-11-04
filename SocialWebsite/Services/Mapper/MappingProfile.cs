using AutoMapper;
using SocialWebsite.Model;
using SocialWebsite.Model.DTO;

namespace ShoppingWebsite.Services.Mapper;

public class MappingProfile : Profile
{
    
    public MappingProfile() {
        // Mapping (User, CreateUserDTO)
        CreateMap<User, CreateUserDTO>()
            .ReverseMap();
    }

}
