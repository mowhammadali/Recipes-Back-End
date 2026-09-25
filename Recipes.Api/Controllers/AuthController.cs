using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Recipes.Api.Models.DTOs.Auth;

namespace Recipes.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IValidator<RegisterRequest> _registerValidator;

    public AuthController(IValidator<RegisterRequest> validator)
    {
        _registerValidator = validator;
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

        return Ok(registerRequest);
    }
}