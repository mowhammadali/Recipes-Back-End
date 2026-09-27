using AutoMapper;
using Recipes.Api.Data.Repositories.Interfaces;
using Recipes.Api.Models.DTOs.Users;
using Recipes.Api.Services.Interfaces;

namespace Recipes.Api.Services;

public sealed class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<UserResponse>> GetAllAsync()
    {
        var users = await _unitOfWork.Users.GetAllAsync();

        var response = _mapper.Map<List<UserResponse>>(users);

        return response;
    }
}