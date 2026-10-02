using AutoMapper;
using Recipes.Api.Data.Repositories.Interfaces;
using Recipes.Api.Exceptions;
using Recipes.Api.Models.DTOs.Favorites;
using Recipes.Api.Models.Entities;
using Recipes.Api.Services.Interfaces;

namespace Recipes.Api.Services;

public sealed class FavoriteService : IFavoriteService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<FavoriteService> _logger;

    public FavoriteService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<FavoriteService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<FavoriteResponse>> GetAllAsync(Guid userId)
    {
        var favorites = await _unitOfWork.Favorites.GetAllByUserIdAsync(userId);

        var response = _mapper.Map<List<FavoriteResponse>>(favorites);

        return response;
    }

    public async Task AddFavoriteAsync(Guid userId, Guid recipeId)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId);

        if (user is null)
        {
            _logger.LogWarning("User {userId} not found", userId);

            throw new NotFoundException("User not found");
        }

        var recipe = await _unitOfWork.Recipes.GetByIdAsync(recipeId);

        if (recipe is null)
        {
            _logger.LogWarning("Recipe {recipeId} not found", recipeId);

            throw new NotFoundException("Recipe not found");
        }

        var favorite = new Favorite
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            RecipeId = recipeId
        };

        await _unitOfWork.Favorites.AddAsync(favorite);
        await _unitOfWork.SaveChangesAsync();
    }
}