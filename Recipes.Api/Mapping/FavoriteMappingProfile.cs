using AutoMapper;
using Recipes.Api.Models.DTOs.Favorites;
using Recipes.Api.Models.Entities;

namespace Recipes.Api.Mapping;

public class FavoriteMappingProfile : Profile
{
    public FavoriteMappingProfile()
    {
        CreateMap<Favorite, FavoriteResponse>();
    }
}