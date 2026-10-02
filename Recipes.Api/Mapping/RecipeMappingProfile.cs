using AutoMapper;
using Recipes.Api.Models.DTOs.Recipes;
using Recipes.Api.Models.Entities;
using Recipes.Api.Models.ValueObjects;

namespace Recipes.Api.Mapping;

public class RecipeMappingProfile : Profile
{
    public RecipeMappingProfile()
    {
        CreateMap<Recipe, RecipeResponse>()
            .ForMember(
                dest => dest.Author,
                opt => opt.MapFrom(src => src.User));

        CreateMap<Ingredient, IngredientResponse>();

        CreateMap<Instruction, InstructionResponse>();
    }
}