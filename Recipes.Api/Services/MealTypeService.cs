using AutoMapper;
using Recipes.Api.Data.Repositories.Interfaces;
using Recipes.Api.Models.DTOs.MealTypes;
using Recipes.Api.Services.Interfaces;

namespace Recipes.Api.Services;

public class MealTypeService : IMealTypeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MealTypeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<MealTypeResponse>> GetAllAsync()
    {
        var mealTypes = await _unitOfWork.MealTypes.GetAllAsync();

        var response = _mapper.Map<List<MealTypeResponse>>(mealTypes);

        return response;
    }
}