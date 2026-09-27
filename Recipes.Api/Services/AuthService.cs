using AutoMapper;
using Recipes.Api.Data.Repositories.Interfaces;
using Recipes.Api.Exceptions;
using Recipes.Api.Models.DTOs.Auth;
using Recipes.Api.Models.DTOs.Users;
using Recipes.Api.Models.Entities;
using Recipes.Api.Models.Enums;
using Recipes.Api.Services.Interfaces;

namespace Recipes.Api.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AuthService> _logger;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;
    protected readonly IMapper _mapper;

    public AuthService(IUnitOfWork unitOfWork, ILogger<AuthService> logger, IPasswordHasher passwordHasher,
        IJwtService jwtService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
        _mapper = mapper;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest registerRequest)
    {
        var emailExists = await _unitOfWork.Users.ExistsByEmailAsync(registerRequest.Email);

        if (emailExists)
        {
            _logger.LogError($"Email {registerRequest.Email} already exists");
            throw new ConflictException("Email already exists");
        }

        var nameExists = await _unitOfWork.Users.ExistsByUsernameAsync(registerRequest.Username);

        if (nameExists)
        {
            _logger.LogError($"Username {registerRequest.Username} already exists");
            throw new ConflictException("Username already exists");
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

        var tokenResponse = _jwtService.GenerateToken(user);

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation(
            "User registered successfully. UserId: {UserId}, Username: {Username}",
            user.Id,
            user.Username);

        return new AuthResponse()
        {
            AccessToken = tokenResponse.AccessToken,
            ExpiresAt = tokenResponse.ExpiresAt,
        };
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest loginRequest)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(loginRequest.Email);

        if (user is null)
        {
            _logger.LogWarning(
                "Login failed. User with email {Email} was not found.",
                loginRequest.Email);

            throw new UnauthorizedException("Invalid email or password");
        }

        var passwordValid = _passwordHasher.Verify(loginRequest.Password, user.PasswordHash);

        if (!passwordValid)
        {
            _logger.LogWarning(
                "Login failed. Invalid password for user {UserId}.",
                loginRequest.Password);

            throw new UnauthorizedException("Invalid email or password");
        }

        var token = _jwtService.GenerateToken(user);

        _logger.LogInformation(
            "User logged in successfully. UserId: {UserId}, Username: {Username}",
            user.Id,
            user.Username);

        return new AuthResponse()
        {
            AccessToken = token.AccessToken,
            ExpiresAt = token.ExpiresAt,
        };
    }

    public async Task<UserResponse> GetMe(Guid userId)
    {
        var user = await _unitOfWork.Users.GetByIdWithProfileAsync(userId);

        if (user is null)
        {
            _logger.LogWarning(
                "User profile requested but user was not found. UserId: {UserId}",
                userId);

            throw new NotFoundException("User not found");
        }

        var userResponse = _mapper.Map<UserResponse>(user);
        return userResponse;
    }
}