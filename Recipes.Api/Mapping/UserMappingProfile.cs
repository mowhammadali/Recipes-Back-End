using AutoMapper;
using Recipes.Api.Models.DTOs.Recipes;
using Recipes.Api.Models.DTOs.Users;
using Recipes.Api.Models.DTOs.UsersProfile;
using Recipes.Api.Models.Entities;

namespace Recipes.Api.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserProfile, UserProfileResponse>();

        CreateMap<User, UserResponse>()
            .ForMember(
                dest => dest.UserProfileResponse,
                opt => opt.MapFrom(src => src.UserProfile));

        CreateMap<User, RecipeUserResponse>();
    }
}