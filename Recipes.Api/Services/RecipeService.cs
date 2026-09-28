using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Recipes.Api.Data.Queries;
using Recipes.Api.Data.Repositories.Interfaces;
using Recipes.Api.Models.DTOs.Common;
using Recipes.Api.Models.DTOs.Recipes;
using Recipes.Api.Services.Interfaces;

namespace Recipes.Api.Services;

public sealed class RecipeService : IRecipeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RecipeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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
}