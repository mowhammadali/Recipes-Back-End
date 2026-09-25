using Recipes.Api.Data.Repositories.Interfaces;
using Recipes.Api.Models.DTOs.Auth;
using Recipes.Api.Models.Entities;
using Recipes.Api.Models.Enums;
using Recipes.Api.Services.Interfaces;

namespace Recipes.Api.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AuthService> _logger;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(IUnitOfWork unitOfWork, ILogger<AuthService> logger, IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest registerRequest)
    {
        var emailExists = await _unitOfWork.Users.ExistsByEmailAsync(registerRequest.Email);

        if (emailExists)
        {
            _logger.LogError($"Email {registerRequest.Email} already exists");
            throw new InvalidOperationException("Email already exists");
        }

        var nameExists = await _unitOfWork.Users.ExistsByUsernameAsync(registerRequest.Username);

        if (nameExists)
        {
            _logger.LogError($"Username {registerRequest.Username} already exists");
            throw new InvalidOperationException("Username already exists");
        }

        User user = new()
        {
            Id = Guid.NewGuid(),
            Username = registerRequest.Username,
            Email = registerRequest.Email,
            PasswordHash = _passwordHasher.Hash(registerRequest.Password),
            CreatedAt = DateTime.UtcNow,
            Role = UserRole.User,
            UserProfile = new UserProfile
            {
                Id = Guid.NewGuid()
            }
        };

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation(
            "User registered successfully. UserId: {UserId}, Username: {Username}",
            user.Id,
            user.Username);

        return new AuthResponse()
        {
            AccessToken = string.Empty,
            ExpiresAt = DateTime.UtcNow.AddHours(24)
        };
    }
}