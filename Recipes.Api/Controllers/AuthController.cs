using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Recipes.Api.Models.DTOs.Auth;
using Recipes.Api.Services.Interfaces;

namespace Recipes.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IValidator<RegisterRequest> _registerValidator;
    private readonly IAuthService _authService;

    public AuthController(IValidator<RegisterRequest> validator, IAuthService authService)
    {
        _registerValidator = validator;
        _authService = authService;
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest registerRequest)
    {
        var validationResult = await _registerValidator.ValidateAsync(registerRequest);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var response = await _authService.RegisterAsync(registerRequest);

        return Ok(response);
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest loginRequest)
    {
        var authResponse = await _authService.LoginAsync(loginRequest);

        return Ok(authResponse);
    }
}