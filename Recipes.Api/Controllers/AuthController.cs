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
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        var validationResult = await _registerValidator.ValidateAsync(registerRequest);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        await _authService.RegisterAsync(registerRequest);

        return Ok();
    }
}