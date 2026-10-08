using AutoMapper;
using Recipes.Api.Data.Repositories.Interfaces;
using Recipes.Api.Models.DTOs.MealTypes;
using Recipes.Api.Services.Interfaces;

namespace Recipes.Api.Services;

public sealed class MealTypeService : IMealTypeService
{
    private readonly IDapperMealTypeRepository _dapperMealTypeRepository;
    private readonly IMapper _mapper;

    public MealTypeService(IMapper mapper, IDapperMealTypeRepository dapperMealTypeRepository)
    {
        _mapper = mapper;
        _dapperMealTypeRepository = dapperMealTypeRepository;
    }

    public async Task<List<MealTypeResponse>> GetAllAsync()
    {
        var mealTypes = await _dapperMealTypeRepository.GetAllAsync();

        var response = _mapper.Map<List<MealTypeResponse>>(mealTypes);

        return response;
    }
}