using AutoMapper;
using SocialWebsite.Models;
using SocialWebsite.Models.DTO;

namespace ShoppingWebsite.Services.Mapper;

public class MappingProfile : Profile
{
    
    public MappingProfile() {
        // Mapping (User, CreateUserDTO)
        CreateMap<User, CreateUserDTO>()
            .ReverseMap();

        // Mapping (Post, CreatePostDTO)
        CreateMap<Post, CreatePostDTO>()
            .ReverseMap();
    }

}
