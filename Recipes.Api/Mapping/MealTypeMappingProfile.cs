using AutoMapper;
using Recipes.Api.Models.DTOs.MealTypes;
using Recipes.Api.Models.Entities;

namespace Recipes.Api.Mapping;

public class MealTypeMappingProfile : Profile
{
    public MealTypeMappingProfile()
    {
        CreateMap<MealType, MealTypeResponse>();
    }
}