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

    public async Task RemoveFavoriteAsync(Guid userId, Guid favoriteId)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId);

        if (user is null)
        {
            _logger.LogWarning("User {userId} not found", userId);

            throw new NotFoundException("User not found");
        }

        var favorite = await _unitOfWork.Favorites.GetFavoriteAsync(favoriteId);

        if (favorite is null)
        {
            _logger.LogWarning("Favorite {favoriteId} not found", favoriteId);

            throw new NotFoundException("Favorite not found");
        }

        if (favorite.UserId != user.Id)
        {
            _logger.LogWarning("User with {userId} could not remove favorite {favoriteId}",
                userId, favoriteId);

            throw new ForbiddenException("You only can remove your favorite.");
        }

        _unitOfWork.Favorites.Delete(favorite);
        await _unitOfWork.SaveChangesAsync();
    }
}