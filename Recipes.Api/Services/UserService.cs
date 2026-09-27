using AutoMapper;
using Recipes.Api.Data.Repositories.Interfaces;
using Recipes.Api.Exceptions;
using Recipes.Api.Models.DTOs.Users;
using Recipes.Api.Services.Interfaces;

namespace Recipes.Api.Services;

public sealed class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<UserService> _logger;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<UserResponse>> GetAllAsync()
    {
        var users = await _unitOfWork.Users.GetAllAsync();

        var response = _mapper.Map<List<UserResponse>>(users);

        return response;
    }

    public async Task<UserResponse> GetByIdAsync(Guid userId)
    {
        var user = await _unitOfWork.Users.GetByIdWithProfileAsync(userId);

        if (user is null)
        {
            _logger.LogWarning(
                "User not found. UserId: {UserId}",
                userId);

            throw new NotFoundException("User not found");
        }

        var response = _mapper.Map<UserResponse>(user);

        return response;
    }
}