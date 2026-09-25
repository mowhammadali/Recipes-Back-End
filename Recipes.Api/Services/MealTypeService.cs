using Recipes.Api.Data.Repositories.Interfaces;
using Recipes.Api.Models.DTOs.MealTypes;
using Recipes.Api.Services.Interfaces;

namespace Recipes.Api.Services;

public class MealTypeService : IMealTypeService
{
    private readonly IUnitOfWork _unitOfWork;

    public MealTypeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<MealTypeResponse>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}