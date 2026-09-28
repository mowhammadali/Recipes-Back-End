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
    private readonly IPasswordHasher _passwordHasher;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserService> logger,
        IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _passwordHasher = passwordHasher;
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

    public async Task DeleteAsync(Guid userId, Guid currentUserId)
    {
        if (userId == currentUserId)
        {
            _logger.LogWarning(
                "Admin attempted to delete their own account. AdminId: {AdminId}",
                currentUserId);

            throw new ForbiddenException("An admin cannot delete their own account.");
        }

        var isUserRemoved = await _unitOfWork.Users.DeleteAsync(userId);

        if (!isUserRemoved)
        {
            _logger.LogWarning(
                "Delete user failed. User not found. UserId: {UserId}",
                userId);

            throw new NotFoundException("User not found");
        }

        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation(
            "User deleted successfully. UserId: {UserId}",
            userId);
    }

    public async Task UpdateByAdminAsync(Guid userId, AdminUpdateUserRequest adminUpdateUserRequest)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId);

        if (user is null)
        {
            _logger.LogWarning(
                "Admin update failed. User not found. UserId: {UserId}",
                userId);

            throw new NotFoundException("User not found");
        }

        user.Username = adminUpdateUserRequest.Username;
        user.Email = adminUpdateUserRequest.Email;
        user.Role = adminUpdateUserRequest.Role;

        if (!string.IsNullOrWhiteSpace(adminUpdateUserRequest.Password))
        {
            user.PasswordHash = _passwordHasher.Hash(adminUpdateUserRequest.Password);
        }

        user.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation(
            "User updated successfully by admin. UserId: {UserId}",
            userId);
    }
}