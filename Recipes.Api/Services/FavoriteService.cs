using AutoMapper;
using Recipes.Api.Data.Repositories.Interfaces;
using Recipes.Api.Models.Entities;
using Recipes.Api.Services.Interfaces;

namespace Recipes.Api.Services;

public sealed class FavoriteService : IFavoriteService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FavoriteService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<Favorite>> GetAllAsync(Guid userId)
    {
        var favorites = await _unitOfWork.Favorites.GetAllByUserIdAsync(userId);

        var response = _mapper.Map<List<Favorite>>(favorites);

        return response;
    }
}