using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Recipes.Api.Data.Queries;
using Recipes.Api.Data.Repositories.Interfaces;
using Recipes.Api.Exceptions;
using Recipes.Api.Models.DTOs.Common;
using Recipes.Api.Models.DTOs.Recipes;
using Recipes.Api.Models.Entities;
using Recipes.Api.Models.ValueObjects;
using Recipes.Api.Services.Interfaces;

namespace Recipes.Api.Services;

public sealed class RecipeService : IRecipeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<RecipeService> _logger;

    public RecipeService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<RecipeService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PagedResponse<RecipeResponse>> GetAllAsync(RecipeQueryParameters queryParameters)
    {
        var query = _unitOfWork.Recipes.Query();

        query = query.ApplyFiltering(queryParameters);
        query = query.ApplySorting(queryParameters);

        var totalItems = await query.CountAsync();

        query = query.ApplyPagination(queryParameters);

        var recipes = await query.ToListAsync();

        var items = _mapper.Map<List<RecipeResponse>>(recipes);

        var page = queryParameters.Page ?? 1;
        var pageSize = queryParameters.PageSize ?? 10;

        var totalPages = (int)Math.Ceiling(
            (double)totalItems / pageSize);

        return new PagedResponse<RecipeResponse>()
        {
            Items = items,
            TotalItems = totalItems,
            Page = page,
            PageSize = pageSize,
            TotalPages = totalPages,
            HasPreviousPage = page > 1,
            HasNextPage = page < totalPages
        };
    }

    public async Task<RecipeResponse> GetByIdAsync(Guid id)
    {
        var recipe = await _unitOfWork.Recipes.GetByIdAsync(id);

        if (recipe is null)
        {
            _logger.LogWarning($"Recipe with id {id} not found", id);

            throw new NotFoundException("Recipe not found");
        }

        var response = _mapper.Map<RecipeResponse>(recipe);
        return response;
    }

    public async Task<RecipeResponse> AddAsync(Guid userId, CreateRecipeRequest createRecipeRequest)
    {
        var mealTypes = await _unitOfWork.Recipes.GetMealTypesByIdsAsync(createRecipeRequest.MealTypeIds);

        if (mealTypes.Count != createRecipeRequest.MealTypeIds.Distinct().Count())
        {
            throw new BadRequestException(
                "One or more meal types do not exist.");
        }

        var recipe = new Recipe()
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = createRecipeRequest.Name,
            Description = createRecipeRequest.Description,
            PrepTimeMinutes = createRecipeRequest.PrepTimeMinutes,
            CookTimeMinutes = createRecipeRequest.CookTimeMinutes,
            Serving = createRecipeRequest.Serving,
            Difficulty = createRecipeRequest.Difficulty,
            CreatedAt = DateTime.UtcNow,
            Ingredients = createRecipeRequest.Ingredients
                .Select(x => new Ingredient(x.Name, x.Quantity, x.Unit))
                .ToList(),
            Instructions = createRecipeRequest.Instructions
                .Select((x, index) => new Instruction(
                    index + 1,
                    x.Description))
                .ToList(),
            MealTypes = mealTypes
        };

        await _unitOfWork.Recipes.AddAsync(recipe);
        await _unitOfWork.SaveChangesAsync();

        var createdRecipe = await _unitOfWork.Recipes.GetByIdAsync(recipe.Id);

        var response = _mapper.Map<RecipeResponse>(createdRecipe);
        return response;
    }
}